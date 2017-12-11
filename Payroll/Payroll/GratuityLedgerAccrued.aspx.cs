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

public partial class Payroll_Payroll_GratuityLedgerAccrued : System.Web.UI.Page
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
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);
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
        int inJobDur = 0;
        TimeSpan ts;

        strMonth = ddlMonth.SelectedItem.Text == "Nil" ? "0" : ddlMonth.SelectedValue.Trim();
        strYear = ddlYear.SelectedItem.Text == "Nil" ? "0" : ddlYear.SelectedValue.Trim();
        strFiscalYear = ddlFiscalYear.SelectedItem.Text == "Nil" ? "0" : ddlFiscalYear.SelectedValue.Trim();
        strEmpID = txtEmpID.Text.Trim() == "" ? "" : txtEmpID.Text.Trim();
        if (strMonth == "0")
            lblPeriod.Text = ddlFiscalYear.SelectedItem.Text;
        else
            lblPeriod.Text = ddlMonth.SelectedItem.Text + " " + ddlFiscalYear.SelectedItem.Text;

        lblPrintDate.Text = Common.DisplayDateTime(DateTime.Now.ToString());

        //Get Employee Info
        string strBasicSal = "";
        string strLedgerID = "";
        string strPrevMonth = objGrMgr.GetLastGLMonth(strFiscalYear);
        string strPrevYear = Common.GetPreviousYear(strMonth, strPrevMonth, strYear);
        bool IsCurrLedgerExist = false;
        long lngLedgerID = 0;

        DataTable dtNewLedger = new DataTable();

        DataTable dtEmpInfo = objGrMgr.GetEmpInfo(txtEmpID.Text,strMonth,strFiscalYear);
        foreach (DataRow dRow in dtEmpInfo.Rows)
        {
            // Joining Date Validation
            dtJoinDate = Convert.ToDateTime(dRow["JOININGDATE"].ToString());
            dtGratuityUpto = Convert.ToDateTime(strYear + "-" + strMonth + "-" + Common.GetMonthDay(strMonth, strYear));
            ts = dtGratuityUpto - dtJoinDate;
            inJobDur = ts.Days + 1;
            //if (inJobDur < 365)
            //    return;

            DataTable dtLedgerCurr = objGrMgr.GetLedgerData(strFiscalYear, strMonth, dRow["EMPID"].ToString().Trim());
            if (dtLedgerCurr.Rows.Count > 0)
                IsCurrLedgerExist = true;

            DataTable dtLedger = objGrMgr.GetLedgerData(strFiscalYear, strPrevMonth, dRow["EMPID"].ToString().Trim());

            if (dtLedger.Rows.Count > 0)
            {
                if (IsCurrLedgerExist == true)
                    lngLedgerID = Convert.ToInt64(dtLedgerCurr.Rows[0]["LEDGERID"].ToString().Trim());
                else
                {
                    if (lngLedgerID == 0)
                        lngLedgerID = Convert.ToInt64(Common.getMaxId("GratuityLedger", "LEDGERID"));
                    else
                        lngLedgerID++;
                }
                DataRow nRow = objDs.dtGrLedger.NewRow();
                nRow["LEDGERID"] = lngLedgerID.ToString();
                nRow["EMPID"] = dRow["EMPID"].ToString();
                nRow["DESGID"] = dRow["DESGID"].ToString();
                nRow["VMONTH"] = strMonth;
                nRow["VYEAR"] = strYear;
                nRow["FISCALYRID"] = strFiscalYear;
                nRow["GRATUITYFROM"] = dRow["GRATUITYFROM"].ToString();
                nRow["BASIC"] = dRow["BASICSAL"].ToString();
                nRow["PMONTH"] = dtLedger.Rows[0]["CMONTH"].ToString();
                nRow["PYEAR"] = dtLedger.Rows[0]["CYEAR"].ToString();
                nRow["PMONTHAMT"] = dtLedger.Rows[0]["CMONTHAMT"].ToString();
                nRow["CMONTH"] = strMonth;
                nRow["CYEAR"] = strYear;

                nRow["CMONTHAMT"] = "0";
                nRow["CHARGINGAMT"] = "0";
                // Display Text
                nRow["FULLNAME"] = dRow["FULLNAME"].ToString();
                nRow["JOBTITLE"] = dRow["JOBTITLE"].ToString();
                nRow["JOININGDATE"] = dRow["JOININGDATE"].ToString();

                nRow["ISEXIST"] = IsCurrLedgerExist == true ? "Y" : "N";
                objDs.dtGrLedger.Rows.Add(nRow);
                objDs.dtGrLedger.AcceptChanges();

            }
            else
            {
                if (IsCurrLedgerExist == true)
                    lngLedgerID = Convert.ToInt64(dtLedgerCurr.Rows[0]["LEDGERID"].ToString().Trim());
                else
                {
                    if (lngLedgerID == 0)
                        lngLedgerID = Convert.ToInt64(Common.getMaxId("GratuityLedger", "LEDGERID"));
                    else
                        lngLedgerID++;
                }
                DataRow nRow = objDs.dtGrLedger.NewRow();
                nRow["LEDGERID"] = lngLedgerID.ToString();
                nRow["EMPID"] = dRow["EMPID"].ToString();
                nRow["DESGID"] = dRow["DESGID"].ToString();
                nRow["VMONTH"] = strMonth;
                nRow["VYEAR"] = strYear;
                nRow["FISCALYRID"] = strFiscalYear;
                nRow["GRATUITYFROM"] = dRow["GRATUITYFROM"].ToString();
                nRow["BASIC"] = dRow["BASICSAL"].ToString();
                nRow["PMONTH"] = strPrevMonth;
                nRow["PYEAR"] = strPrevYear;
                nRow["PMONTHAMT"] = "0";
                nRow["CMONTH"] = strMonth;
                nRow["CYEAR"] = strYear;

                nRow["CMONTHAMT"] = "0";
                nRow["CHARGINGAMT"] = "0";
                // Display Text
                nRow["FULLNAME"] = dRow["FULLNAME"].ToString();
                nRow["JOBTITLE"] = dRow["JOBTITLE"].ToString();
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
        decimal dclLengthDays = 0;
        decimal dclLengthYear = 0;
        decimal dclPolicy = Convert.ToDecimal("1.25");
        decimal dclGraBasic = 0;
        decimal dclCurrGratuityAmt = 0;
        decimal dclPrevGratuityAmt = 0;
        decimal dclDiff = 0;
        TimeSpan ts;
        DateTime GraFrom = new DateTime();
        DateTime GraUpto = new DateTime();
        GraUpto = Convert.ToDateTime(strGraUpto);
        foreach (GridViewRow gRow in grLedger.Rows)
        {
            dclLengthDays = 0;
            dclLengthYear = 0;
            dclGraBasic = 0;
            dclCurrGratuityAmt = 0;
            dclPrevGratuityAmt = 0;
            dclDiff = 0;

            if (Common.CheckNullString(gRow.Cells[3].Text.Trim()) != "")
            {
                gRow.Cells[3].Text = Common.DisplayDate(gRow.Cells[3].Text.Trim());
            }
            if (Common.CheckNullString(gRow.Cells[4].Text.Trim()) != "")
            {
                GraFrom = Convert.ToDateTime(gRow.Cells[4].Text);
                gRow.Cells[4].Text = Common.DisplayDate(gRow.Cells[4].Text.Trim());
            }
            
            // Month Name
            gRow.Cells[9].Text = Common.ReturnFullMonthName(gRow.Cells[9].Text.Trim());

            gRow.Cells[5].Text = Convert.ToString(Common.RoundDecimal(gRow.Cells[5].Text.Trim(),0));
            // Amount
            dclGraBasic = Common.RoundDecimal(gRow.Cells[5].Text, 0) * dclPolicy;
            dclGraBasic = Common.RoundDecimal(dclGraBasic.ToString(), 0);
            ts = GraUpto - GraFrom;
            dclLengthDays = Convert.ToDecimal(ts.Days + 1);
            dclLengthYear = dclLengthDays / 365;
            dclLengthYear = Common.RoundDecimal(dclLengthYear.ToString(), 4);
            dclCurrGratuityAmt = dclGraBasic * dclLengthYear;
            dclCurrGratuityAmt = Common.RoundDecimal(dclCurrGratuityAmt.ToString(), 0);

            dclPrevGratuityAmt = Convert.ToDecimal(gRow.Cells[6].Text.Trim());
            dclDiff = dclCurrGratuityAmt - dclPrevGratuityAmt;

            gRow.Cells[7].Text = dclCurrGratuityAmt.ToString();
            gRow.Cells[8].Text = dclDiff.ToString();
        }
        if (grLedger.Rows.Count > 0)
        {
            grLedger.HeaderRow.Cells[6].Text = "As of " + Common.ReturnFullMonthName(grLedger.DataKeys[0].Values[2].ToString().Trim()) + " " + grLedger.DataKeys[0].Values[3].ToString().Trim();
            grLedger.HeaderRow.Cells[7].Text = "As of " + Common.ReturnFullMonthName(grLedger.DataKeys[0].Values[4].ToString().Trim()) + " " + grLedger.DataKeys[0].Values[5].ToString().Trim();
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (grLedger.Rows.Count > 0)
        {
            objGrMgr.GratuityAccrued(grLedger, Session["USERID"].ToString().Trim(),
                Common.SetDateTime(DateTime.Now.ToString()), ddlFiscalYear.SelectedValue.Trim(), "Y", ddlMonth.SelectedValue.Trim());
            lblMsg.Text = "Gratuity Accrued Successfully";
        }
        else
        {
            lblMsg.Text = "No record to accrued";
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
}
