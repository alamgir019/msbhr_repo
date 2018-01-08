using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class Payroll_Payroll_GratuityLedger : System.Web.UI.Page
{
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    Payroll_GratuityLedgerManager objGrMgr = new Payroll_GratuityLedgerManager();
    dsPayroll_Loan objDs = new dsPayroll_Loan();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.Items.Add("Nil");
            ddlYear.Items.Add("Nil");
            ddlMonth.SelectedValue = DateTime.Today.Month.ToString();
            ddlYear.SelectedValue = DateTime.Today.Year.ToString();
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0, "F"), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);
        }
    }

    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        string strMonth = "";
        string strYear = "";
        string strFiscalYear = "";
        string strEmpID = "";
        DateTime dtJoinDate = new DateTime();
        DateTime dtGratuityUpto = new DateTime();
        DateTime dtAccureDate = Convert.ToDateTime(Common.ReturnDate(txtAccrueDate.Text.Trim()));
        dtAccureDate = dtAccureDate.AddDays(1);
        dtAccureDate = dtAccureDate.AddYears(-1);

        DateTime dtJoinFrom = Convert.ToDateTime(Common.ReturnDate(txtLastAccrueDate.Text.Trim()));
        dtJoinFrom = dtJoinFrom.AddDays(1);
        dtJoinFrom = dtJoinFrom.AddYears(-1);
        DateTime dtJoinTo=Convert.ToDateTime(Common.ReturnDate(txtAccrueDate.Text.Trim()));
        dtJoinTo=dtJoinTo.AddYears(-1);
        int inJobDur = 0;
        TimeSpan ts;

        strMonth = ddlMonth.SelectedItem.Text == "Nil" ? "0" : ddlMonth.SelectedValue.Trim();
        strYear = ddlYear.SelectedItem.Text == "Nil" ? "0" : ddlYear.SelectedValue.Trim();
        strFiscalYear = ddlFiscalYear.SelectedItem.Text == "Nil" ? "0" : ddlFiscalYear.SelectedValue.Trim();
        strEmpID = "";
        if (strMonth == "0")
            lblPeriod.Text = ddlFiscalYear.SelectedItem.Text;
        else
            lblPeriod.Text = ddlMonth.SelectedItem.Text + " " + ddlFiscalYear.SelectedItem.Text;

        lblPrintDate.Text = Common.DisplayDateTime(DateTime.Now.ToString());

        //Get Employee Info
        string strBasicSal = "";
        string strLedgerID = "";
        string strPrevMonth = Common.GetPreviousMonth(strMonth);
        
        string strPrevYear = Common.GetPreviousYear(strMonth, strPrevMonth, strYear);
        bool IsCurrLedgerExist = false;
        long lngLedgerID = 0;

        DataTable dtNewLedger = new DataTable();
        
        DataTable dtEmpInfo = objGrMgr.GetEmployeeForGratuity(strMonth, strFiscalYear, Common.SetDate(dtAccureDate.ToShortDateString()));        
        DataTable dtNewEmp = objGrMgr.GetNewEmployeeForGratuity(strMonth, strFiscalYear, Common.SetDate(dtJoinFrom.ToShortDateString()), Common.SetDate(dtJoinTo.ToShortDateString()));       
        DataTable dtEmpWithNullGrDate = objGrMgr.GetEmployeeWithNullGratuityDateExceptNewJoiner(strMonth, strFiscalYear, Common.SetDate(dtAccureDate.ToShortDateString()), Common.SetDate(dtJoinFrom.ToShortDateString()), Common.SetDate(dtJoinTo.ToShortDateString()));
        DataTable dtGrPayment = objGrMgr.GetGrPaymentList(ddlMonth.SelectedValue.Trim(), ddlFiscalYear.SelectedValue.Trim(), "");

        DataRow[] foundRowsNew;
        DataRow[] foundRowsGrNull;
        DataRow[] foundPaymentRows;
        string strGratuityFrom = "";
        foreach (DataRow dRow in dtEmpInfo.Rows)
        {
            foundRowsNew = null;
            foundRowsGrNull = null;
            foundPaymentRows = null;
            foundPaymentRows = dtGrPayment.Select("EMPID='" + dRow["EMPID"].ToString().Trim() + "'");
            if (foundPaymentRows.Length > 0)
                continue;

            strGratuityFrom = "";
            // Joining Date Validation
            //dtJoinDate = Convert.ToDateTime(dRow["JOININGDATE"].ToString());
            dtGratuityUpto = Convert.ToDateTime(strYear + "-" + strMonth + "-" + Common.GetMonthDay(strMonth, strYear));
            //ts = dtGratuityUpto - dtJoinDate;
            //inJobDur = ts.Days + 1;
            //if (inJobDur < 365)
            //    return;

            // Validate Gratuity From
            foundRowsNew = dtNewEmp.Select("EMPID='" + dRow["EMPID"].ToString().Trim() + "'");
            if (foundRowsNew.Length > 0)
            {
                strGratuityFrom = foundRowsNew[0]["JOININGDATE"].ToString().Trim();
            }
            else
            {
                foundRowsGrNull = dtEmpWithNullGrDate.Select("EMPID='" + dRow["EMPID"].ToString().Trim() + "'");
                if (foundRowsGrNull.Length > 0)
                {
                    strGratuityFrom = foundRowsGrNull[0]["JOININGDATE"].ToString().Trim();
                }
                else
                {
                    strGratuityFrom = dRow["GRATUITYFROM"].ToString().Trim();
                }
            }

            //DataTable dtLedgerCurr = objGrMgr.GetLedgerData(strFiscalYear, strMonth, dRow["EMPID"].ToString().Trim());
            //if (dtLedgerCurr.Rows.Count > 0)
            //    IsCurrLedgerExist = true;
            //Edited By Amit 02.08.2015
            DataTable dtLedger=new DataTable ();
            if (strPrevMonth == "6")
            {
                int iFiscalYr = Convert.ToInt16(strFiscalYear);
                iFiscalYr = iFiscalYr - 1;
                dtLedger = objGrMgr.GetLedgerData(iFiscalYr.ToString(), strPrevMonth, dRow["EMPID"].ToString().Trim());
            }
            else
            {
                dtLedger = objGrMgr.GetLedgerData(strFiscalYear, strPrevMonth, dRow["EMPID"].ToString().Trim());
            }
            //DataTable dtLedger = objGrMgr.GetLedgerData(strFiscalYear, strPrevMonth, dRow["EMPID"].ToString().Trim());
            //DataTable dtLedger = objGrMgr.GetLedgerData("4", strPrevMonth, dRow["EMPID"].ToString().Trim());          

            if (dtLedger.Rows.Count > 0)
            {
                //if (IsCurrLedgerExist == true)
                //    lngLedgerID = Convert.ToInt64(dtLedgerCurr.Rows[0]["LEDGERID"].ToString().Trim());
                //else
                //{
                    if (lngLedgerID == 0)
                        lngLedgerID = Convert.ToInt64(Common.getMaxId("GratuityLedger", "LEDGERID"));
                    else
                        lngLedgerID++;
                //}
                DataRow nRow = objDs.dtGrLedger.NewRow();
                nRow["LEDGERID"] = lngLedgerID.ToString();
                nRow["EMPID"] = dRow["EMPID"].ToString();
                nRow["DESGID"] = dRow["DESIGID"].ToString();
                nRow["VMONTH"] = strMonth;
                nRow["VYEAR"] = strYear;
                nRow["FISCALYRID"] = strFiscalYear;
                nRow["GRATUITYFROM"] = strGratuityFrom;
                nRow["GRATUITYUPTO"] = Common.DisplayDateIIS(dtGratuityUpto.ToString());

                nRow["BASIC"] = dRow["BASICSALARY"].ToString();
                nRow["PMONTH"] = dtLedger.Rows[0]["CMONTH"].ToString();
                nRow["PYEAR"] = dtLedger.Rows[0]["CYEAR"].ToString();
                nRow["PMONTHAMT"] = dtLedger.Rows[0]["CMONTHAMT"].ToString();
                nRow["CMONTH"] = strMonth;
                nRow["CYEAR"] = strYear;

                nRow["CMONTHAMT"] = "0";
                nRow["CHARGINGAMT"] = "0";
                // Display Text
                nRow["FULLNAME"] = dRow["FULLNAME"].ToString();
                nRow["JOBTITLE"] = dRow["DESIGNAME"].ToString();
                nRow["JOININGDATE"] = dRow["JOININGDATE"].ToString();

                nRow["ISEXIST"] = IsCurrLedgerExist == true ? "Y" : "N";
                objDs.dtGrLedger.Rows.Add(nRow);
                objDs.dtGrLedger.AcceptChanges();

            }
            else
            {
                //if (IsCurrLedgerExist == true)
                //    lngLedgerID = Convert.ToInt64(dtLedgerCurr.Rows[0]["LEDGERID"].ToString().Trim());
                //else
                //{
                    if (lngLedgerID == 0)
                        lngLedgerID = Convert.ToInt64(Common.getMaxId("GratuityLedger", "LEDGERID"));
                    else
                        lngLedgerID++;
                //}
                DataRow nRow = objDs.dtGrLedger.NewRow();
                nRow["LEDGERID"] = lngLedgerID.ToString();
                nRow["EMPID"] = dRow["EMPID"].ToString();
                nRow["DESGID"] = dRow["DESIGID"].ToString();
                nRow["VMONTH"] = strMonth;
                nRow["VYEAR"] = strYear;
                nRow["FISCALYRID"] = strFiscalYear;
                nRow["GRATUITYFROM"] = strGratuityFrom;
                nRow["GRATUITYUPTO"] = Common.DisplayDateIIS(dtGratuityUpto.ToString());

                nRow["BASIC"] = dRow["BASICSALARY"].ToString();
                nRow["PMONTH"] = strPrevMonth;
                nRow["PYEAR"] = strPrevYear;
                nRow["PMONTHAMT"] = "0";
                nRow["CMONTH"] = strMonth;
                nRow["CYEAR"] = strYear;

                nRow["CMONTHAMT"] = "0";
                nRow["CHARGINGAMT"] = "0";
                // Display Text
                nRow["FULLNAME"] = dRow["FULLNAME"].ToString();
                nRow["JOBTITLE"] = dRow["DESIGNAME"].ToString();
                nRow["JOININGDATE"] = dRow["JOININGDATE"].ToString();

                nRow["ISEXIST"] = IsCurrLedgerExist == true ? "Y" : "N";
                objDs.dtGrLedger.Rows.Add(nRow);
                objDs.dtGrLedger.AcceptChanges();

            }

            
        }

        if (objDs.dtGrLedger.Rows.Count > 0)
        {
            grLedger.DataSource = objDs.dtGrLedger;
            grLedger.DataBind();
            this.FormatGridView(strYear + "-" + strMonth + "-" + Common.GetMonthDay(strMonth, strYear));
        }
    }

    protected void FormatGridView(string strGraUpto)
    {
        Decimal dclLengthDays = 0;
        Decimal dclLengthYear = 0;
        Decimal dclPolicy = Convert.ToDecimal("1.5");
        Decimal dclGraBasic = 0;
        Decimal dclCurrGratuityAmt = 0;
        Decimal dclPrevGratuityAmt = 0;
        Decimal dclDiff = 0;
        TimeSpan ts;
        DateTime GraFrom = new DateTime();
        DateTime GraUpto = new DateTime();
        GraUpto = Convert.ToDateTime(strGraUpto);
        foreach (GridViewRow gRow in grLedger.Rows)
        {
            dclLengthDays = 0;
            dclLengthYear = 0;dclGraBasic = 0;
            dclCurrGratuityAmt = 0;
            dclPrevGratuityAmt = 0;
            dclDiff = 0;
            //if (gRow.Cells[0].Text.Trim()=="3018")
            //    dclDiff = 0;

            if (Common.CheckNullString(gRow.Cells[3].Text.Trim()) != "")
            {
                gRow.Cells[3].Text = Common.DisplayDateIIS(gRow.Cells[3].Text.Trim());
            }
            if (Common.CheckNullString(gRow.Cells[4].Text.Trim()) != "")
            {
                GraFrom = Convert.ToDateTime(gRow.Cells[4].Text);
                gRow.Cells[4].Text = Common.DisplayDateIIS(gRow.Cells[4].Text.Trim());
            }
            
            // Month Name
            gRow.Cells[11].Text = Common.ReturnFullMonthName(gRow.Cells[11].Text.Trim());

            gRow.Cells[6].Text = Convert.ToString(Common.RoundDecimal(gRow.Cells[6].Text.Trim(),0));
            // Amount
            dclGraBasic = Common.RoundDecimal(gRow.Cells[6].Text, 0);
            dclGraBasic = dclGraBasic * dclPolicy;
            //dclGraBasic = Common.RoundDecimal(dclGraBasic.ToString(), 0);
            dclGraBasic = Math.Round(dclGraBasic, MidpointRounding.AwayFromZero);
            gRow.Cells[7].Text = dclGraBasic.ToString();
            // Length
            ts = GraUpto - GraFrom;
            dclLengthDays = Convert.ToDecimal(ts.Days + 1);
            gRow.Cells[8].Text = dclLengthDays.ToString();
            // Yrs
            dclLengthYear = dclLengthDays / 365;
            dclLengthYear = Common.RoundDecimal(dclLengthYear.ToString(), 10);
            gRow.Cells[9].Text = dclLengthYear.ToString();

            dclCurrGratuityAmt = dclGraBasic * dclLengthYear;
            dclCurrGratuityAmt = Common.RoundDecimal(dclCurrGratuityAmt.ToString(), 0);

            //dclPrevGratuityAmt = Common.RoundDecimal(grLedger.DataKeys[gRow.DataItemIndex].Values[9].ToString().Trim(), 0);
            //dclDiff = dclCurrGratuityAmt - dclPrevGratuityAmt;

            gRow.Cells[10].Text = dclCurrGratuityAmt.ToString();
            //gRow.Cells[12].Text = dclDiff.ToString();
           // grLedger.DataKeys[gRow.DataItemIndex].Values[8] = dclDiff.ToString();
            //grLedger.DataKeys[gRow.DataItemIndex].Values[9] = dclPrevGratuityAmt.ToString();
        }
        if (grLedger.Rows.Count > 0)
        {
            //grLedger.HeaderRow.Cells[10].Text = "As of " + Common.ReturnFullMonthName(grLedger.DataKeys[0].Values[2].ToString().Trim()) + " " + grLedger.DataKeys[0].Values[3].ToString().Trim();
            grLedger.HeaderRow.Cells[10].Text = "As of " + Common.ReturnFullMonthName(grLedger.DataKeys[0].Values[4].ToString().Trim()) + " " + grLedger.DataKeys[0].Values[5].ToString().Trim();
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (grLedger.Rows.Count > 0)
        {
            objGrMgr.GratuityAccrued(grLedger, Session["USERID"].ToString().Trim(), 
                Common.SetDateTime(DateTime.Now.ToString()), ddlFiscalYear.SelectedValue.Trim(),"Y",ddlMonth.SelectedValue.Trim());
            lblMsg.Text = "Gratuity Ledger Saved Successfully.";
        }
        else
        {
            lblMsg.Text = "No record to save.";
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=Gratuity.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        grLedger.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

        // Confirms that an HtmlForm control is rendered for the
        // specified ASP.NET server control at run time.

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
}
