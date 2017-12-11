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
using System.Text;
public partial class Payroll_Payroll_GratuityPayment : System.Web.UI.Page
{
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    Payroll_MasterMgr objPayrollMgr2 = new Payroll_MasterMgr();
    Payroll_MasterMgr objPayrollMgr3 = new Payroll_MasterMgr();
    Payroll_GratuityLedgerManager objGrMgr = new Payroll_GratuityLedgerManager();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.EntryMode(false);
            Common.FillMonthList(ddlMonth);
            Common.FillMonthList(ddlMonth2);
            Common.FillMonthList(ddlPayrollMonth);
            Common.FillYearList(5, ddlYear);

            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlMonth2.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlPayrollMonth.SelectedValue = Convert.ToString(DateTime.Today.Month-1);

            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0,"F"), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);
            Common.FillDropDownList(objPayrollMgr2.SelectFiscalYear(0, "F"), ddlFiscalYear2, "FISCALYRTITLE", "FISCALYRID", false);
            Common.FillDropDownList(objPayrollMgr3.SelectFiscalYear(0, "F"), ddlPayrollFinYear, "FISCALYRTITLE", "FISCALYRID", false);
            TabContainer1.ActiveTabIndex = 0;
        }
    }


    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
        {
            btnSave.Enabled = false;
            btnDelete.Enabled = true;
            imgBtnSearch.Visible = false;
        }
        else
        {
            btnSave.Enabled = true ;
            btnDelete.Enabled = false;
            imgBtnSearch.Visible = true;
            lblEmpName.Text = "";
            lblEmpName.ToolTip = "";
            lblDesig.ToolTip = "";
            lblDesig.Text = "";
            lblJoiningDate.Text = "";
            lblGratuityFrom.Text = "";
            lblGrauityUpto.Text = "";
            txtBasicSal.Text = "0";
            txtCurrMonthGratuity.Text = "0";
            txtPrevMonthGratuity.Text = "0";
            txtCharging.Text = "0";
            txtBalance.Text = "0";
            txtPayDate.Text = Common.DisplayDate(DateTime.Today.ToShortDateString());
            txtRemarks.Text = "";
            hfIsUpdate.Value = "N";
            hfLedgerID.Value = "";


        }
    }

    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        string strBasicSal = "";
        string strPrevMonth = Common.GetPreviousMonth(ddlMonth.SelectedValue.Trim());
        string strPrevYear="";
        DateTime dtJoinDate = new DateTime();
        DateTime dtGratuityFrom = new DateTime();
        DateTime dtGratuityUpto = new DateTime();
        
        // Gratuity Calculation
        decimal dclLengthDays = 0;
        decimal dclLengthYear = 0;
        decimal dclPolicy = Convert.ToDecimal("1.5");
        decimal dclGraBasic = 0;
        decimal dclCurrGratuityAmt = 0;
        decimal dclPrevGratuityAmt = 0;
        decimal dclDiff = 0;
        TimeSpan ts;
        
        if (strPrevMonth == "12")
            strPrevYear = Convert.ToString(Convert.ToInt32(ddlYear.SelectedValue.Trim()) - 1);
        else
            strPrevYear = ddlYear.SelectedValue.Trim();

        DataTable dtEmpInfo = objGrMgr.GetEmpInfo(txtEmpID.Text, ddlPayrollMonth.SelectedValue.Trim(), ddlPayrollFinYear.SelectedValue.Trim());

        if(dtEmpInfo.Rows.Count>0)
        {
            lblEmpName.Text = dtEmpInfo.Rows[0]["FULLNAME"].ToString().Trim();
            lblEmpName.ToolTip = dtEmpInfo.Rows[0]["EMPID"].ToString().Trim();

            if(string.IsNullOrEmpty(dtEmpInfo.Rows[0]["JOININGDATE"].ToString().Trim()) == false)
            {
                lblJoiningDate.Text=Common.DisplayDate(dtEmpInfo.Rows[0]["JOININGDATE"].ToString().Trim());
                dtJoinDate=Convert.ToDateTime(dtEmpInfo.Rows[0]["JOININGDATE"].ToString().Trim());
            }
            //lblJoiningDate.Text = string.IsNullOrEmpty(dtEmpInfo.Rows[0]["JOININGDATE"].ToString().Trim()) == false ? Common.DisplayDate(dtEmpInfo.Rows[0]["JOININGDATE"].ToString().Trim()) : "";
            if (string.IsNullOrEmpty(txtGrFrom.Text) == false)
            {
                lblGratuityFrom.Text = txtGrFrom.Text;
                dtGratuityFrom = Convert.ToDateTime(Common.ReturnDate(txtGrFrom.Text.Trim()));
            }
            else if (string.IsNullOrEmpty(dtEmpInfo.Rows[0]["GRATUITYFROM"].ToString().Trim()) == false)
            {
                dtGratuityFrom = Convert.ToDateTime(dtEmpInfo.Rows[0]["GRATUITYFROM"].ToString().Trim());
                if (dtJoinDate > dtGratuityFrom)
                {
                    lblGratuityFrom.Text = Common.DisplayDate(dtEmpInfo.Rows[0]["JOININGDATE"].ToString().Trim());
                    dtGratuityFrom = Convert.ToDateTime(dtEmpInfo.Rows[0]["JOININGDATE"].ToString().Trim());
                }
                else
                {
                    lblGratuityFrom.Text = Common.DisplayDate(dtEmpInfo.Rows[0]["GRATUITYFROM"].ToString().Trim());
                }
            }
            else
            {
                lblGratuityFrom.Text = lblJoiningDate.Text;
                dtGratuityFrom = Convert.ToDateTime(Common.ReturnDate(lblJoiningDate.Text));
            }

            lblGrauityUpto.Text = txtGrUpto.Text;
            dtGratuityUpto = Convert.ToDateTime(Common.ReturnDate(txtGrUpto.Text.Trim()));
            

            //lblGratuityFrom.Text = string.IsNullOrEmpty(dtEmpInfo.Rows[0]["GRATUITYFROM"].ToString().Trim()) == false ? Common.DisplayDate(dtEmpInfo.Rows[0]["GRATUITYFROM"].ToString().Trim()) : "";
           
            //// Months
            //lblPrevMonth.Text ="As of " + Common.ReturnFullMonthName(strPrevMonth) + " " + strPrevYear;
            //lblPrevMonth.ToolTip = strPrevMonth;

            //lblCurrMonth.Text = "As of " + Common.ReturnFullMonthName(ddlMonth.SelectedValue.Trim()) + " " + ddlYear.SelectedValue.Trim();
            //lblCurrMonth.ToolTip = ddlMonth.SelectedValue.Trim();

            strBasicSal = Convert.ToString(Common.RoundDecimal(dtEmpInfo.Rows[0]["BASICSAL"].ToString().Trim(), 0));
            DataTable dtCULedger = objGrMgr.GetEmpWiseLedgerData(txtEmpID.Text.Trim());
            if (dtCULedger.Rows.Count > 0)
            {
                dclLengthDays = 0;
                dclLengthYear = 0;
                dclGraBasic = 0;
                dclCurrGratuityAmt = 0;
                dclPrevGratuityAmt = 0;
                dclDiff = 0;

                txtBasicSal.Text = strBasicSal;
                //// Last Accrued
                txtPrevMonthGratuity.Text = dtCULedger.Rows[0]["CMONTHAMT"].ToString().Trim();
                //// Current Accure
                // Amount
                dclGraBasic = Common.RoundDecimal(txtBasicSal.Text, 0) * dclPolicy;
                dclGraBasic = Common.RoundDecimal(dclGraBasic.ToString(), 0);
                // Length
                ts = dtGratuityUpto - dtGratuityFrom;
                dclLengthDays = Convert.ToDecimal(ts.Days);
                // Yrs
                dclLengthYear = dclLengthDays / 365;
                dclLengthYear = Common.RoundDecimal(dclLengthYear.ToString(), 10);

                dclCurrGratuityAmt = dclGraBasic * dclLengthYear;
                dclCurrGratuityAmt = Common.RoundDecimal(dclCurrGratuityAmt.ToString(), 0);
                txtCurrMonthGratuity.Text = dclCurrGratuityAmt.ToString();

                //// Difference
                dclPrevGratuityAmt = Convert.ToDecimal(txtPrevMonthGratuity.Text);
                dclDiff = dclCurrGratuityAmt - dclPrevGratuityAmt;
                txtCharging.Text = dclDiff.ToString();

                //// Net Balancce
                txtBalance.Text = Convert.ToString(dclPrevGratuityAmt + dclDiff);

                //txtCurrMonthGratuity.Text = dtCULedger.Rows[0]["CMONTHAMT"].ToString().Trim();
                //txtCharging.Text = dtCULedger.Rows[0]["CHARGINGAMT"].ToString().Trim();

                lblDesig.Text = dtEmpInfo.Rows[0]["JOBTITLE"].ToString().Trim();
                lblDesig.ToolTip = dtEmpInfo.Rows[0]["DESGID"].ToString().Trim();    


                hfIsUpdate.Value = "N";
                hfLedgerID.Value = dtCULedger.Rows[0]["LEDGERID"].ToString().Trim();
                lblMsg.Text = "";
            }
            else
            {
                dclLengthDays = 0;
                dclLengthYear = 0;
                dclGraBasic = 0;
                dclCurrGratuityAmt = 0;
                dclPrevGratuityAmt = 0;
                dclDiff = 0;

                txtBasicSal.Text = strBasicSal;
                //// Last Accrued
                txtPrevMonthGratuity.Text = "0";
                //// Current Accure
                // Amount
                dclGraBasic = Common.RoundDecimal(txtBasicSal.Text, 0) * dclPolicy;
                dclGraBasic = Common.RoundDecimal(dclGraBasic.ToString(), 0);
                // Length
                ts = dtGratuityUpto - dtGratuityFrom;
                dclLengthDays = Convert.ToDecimal(ts.Days);
                // Yrs
                dclLengthYear = dclLengthDays / 365;
                dclLengthYear = Common.RoundDecimal(dclLengthYear.ToString(), 10);

                dclCurrGratuityAmt = dclGraBasic * dclLengthYear;
                dclCurrGratuityAmt = Common.RoundDecimal(dclCurrGratuityAmt.ToString(), 0);
                txtCurrMonthGratuity.Text = dclCurrGratuityAmt.ToString();

                //// Difference
                dclPrevGratuityAmt = Convert.ToDecimal(txtPrevMonthGratuity.Text);
                dclDiff = dclCurrGratuityAmt - dclPrevGratuityAmt;
                txtCharging.Text = dclDiff.ToString();

                //// Net Balancce
                txtBalance.Text = Convert.ToString(dclPrevGratuityAmt + dclDiff);

                //txtCurrMonthGratuity.Text = dtCULedger.Rows[0]["CMONTHAMT"].ToString().Trim();
                //txtCharging.Text = dtCULedger.Rows[0]["CHARGINGAMT"].ToString().Trim();

                lblDesig.Text = dtEmpInfo.Rows[0]["DESIGNAME"].ToString().Trim();
                lblDesig.ToolTip = dtEmpInfo.Rows[0]["DESIGID"].ToString().Trim();  

               // txtBasicSal.Text = strBasicSal;
                //txtPrevMonthGratuity.Text = "0";
                //txtCurrMonthGratuity.Text = "0";
                //txtCharging.Text = "0";
                lblDesig.Text = dtEmpInfo.Rows[0]["DESIGNAME"].ToString().Trim();
                lblDesig.ToolTip = dtEmpInfo.Rows[0]["DESIGID"].ToString().Trim();

                btnSave.Text = "Save";
                hfIsUpdate.Value = "N";
                hfLedgerID.Value = "";
                lblMsg.Text = "";
            }
        }
        else
        {
            lblEmpName.Text = "";
            lblJoiningDate.Text = "";
            lblGratuityFrom.Text = "";
            lblDesig.Text = "";
            lblDesig.ToolTip = "";

            lblMsg.Text = "Data not available.";
        }
    }
    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    string strLedgerID = "";
    //    string strPrevYear="";
    //    if (lblEmpName.Text == "")
    //    {
    //        lblMsg.Text = "No Record to save.";
    //        return;
    //    }
    //    if (hfIsUpdate.Value == "Y")
    //        strLedgerID = hfLedgerID.Value.Trim();
    //    else
    //        strLedgerID = Common.getMaxId("GratuityLedger", "LEDGERID");

    //    if (lblPrevMonth.ToolTip.Trim() == "12")
    //        strPrevYear = Convert.ToString(Convert.ToInt32(ddlYear.SelectedValue.Trim()) - 1);
    //    else
    //        strPrevYear = ddlYear.SelectedValue.Trim();

    //    objGrMgr.InsertGratuityLedger(strLedgerID,
    //                            txtEmpID.Text.Trim(),
    //                            ddlMonth.SelectedValue.Trim(),
    //                            ddlYear.SelectedValue.Trim(),
    //                            ddlFiscalYear.SelectedValue.Trim(),
    //                            lblDesig.ToolTip.Trim(),
    //                            Common.ReturnDate(lblGratuityFrom.Text.Trim()),
    //                            txtBasicSal.Text.Trim(),
    //                            lblPrevMonth.ToolTip.Trim(),
    //                            strPrevYear,
    //                            txtPrevMonthGratuity.Text.Trim(),
    //                            ddlMonth.SelectedValue.Trim(),
    //                            ddlYear.SelectedValue.Trim(),
    //                            txtCurrMonthGratuity.Text.Trim(),
    //                            txtCharging.Text.Trim(),
    //                            Session["USERID"].ToString().Trim(),
    //                            Common.SetDateTime(DateTime.Now.ToString()),
    //                            hfIsUpdate.Value);
    //    lblMsg.Text = "Record Saved Successfully";
    //    hfIsUpdate.Value = "N";
    //    hfLedgerID.Value = "";
    //    btnSave.Text = "Save";

    //}

    protected bool ValidateAndSave()
    {
        if (hfIsUpdate.Value == "N")
        {
            if (objGrMgr.IsCurrentMonthPaymentExist(txtEmpID.Text.Trim(), ddlMonth.SelectedValue.Trim(), ddlFiscalYear.SelectedValue.Trim()) == true)
            {
                lblMsg.Text = "Current month payment already exist.Operation cannot be processed.";
                return false;
            }
        }

        return true;
    }

    protected void SaveData()
    {
        string strLedgerID = "";
        string strPrevYear = "";
        DateTime dtNextGrFrom = new DateTime();
        dtNextGrFrom = Convert.ToDateTime(Common.ReturnDate(lblGrauityUpto.Text.Trim()));
        dtNextGrFrom = dtNextGrFrom.AddDays(1);



        if (lblEmpName.Text == "")
        {
            lblMsg.Text = "No Record to save.";
            return;
        }
        if (hfIsUpdate.Value == "Y")
            strLedgerID = hfLedgerID.Value.Trim();
        else
            strLedgerID = Common.getMaxId("GratuityPayment", "TransID");

        objGrMgr.InsertGratuityPayment(strLedgerID,
                                        txtEmpID.Text.Trim(),
                                        lblEmpName.ToolTip.Trim(),
                                        lblDesig.ToolTip.Trim(),
                                        Common.ReturnDate(lblJoiningDate.Text.Trim()),
                                        Common.ReturnDate(lblGratuityFrom.Text.Trim()),
                                        Common.ReturnDate(lblGrauityUpto.Text.Trim()),
                                        Common.SetDate(dtNextGrFrom.ToString()),
                                        ddlMonth.SelectedValue.Trim(),
                                        ddlYear.SelectedValue.Trim(),
                                        ddlFiscalYear.SelectedValue.Trim(),
                                        txtBasicSal.Text.Trim(),
                                        txtPrevMonthGratuity.Text.Trim(),
                                        txtCurrMonthGratuity.Text.Trim(),
                                        txtCharging.Text.Trim(),
                                        Common.ReturnDate(txtPayDate.Text.Trim()),
                                        txtBalance.Text.Trim(),
                                        txtRemarks.Text.Trim(),
                                        Session["USERID"].ToString().Trim(),
                                        Common.SetDateTime(DateTime.Now.ToString()));
        lblMsg.Text = "Record Saved Successfully";
        hfIsUpdate.Value = "N";
        hfLedgerID.Value = "";
        btnSave.Text = "Save";
        this.EntryMode(false);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.ValidateAndSave() == true)
        {
            this.SaveData();
            this.OpenRecord();
        }
    }

    protected void OpenRecord()
    {
        DataTable dtPaymentList = objGrMgr.GetGrPaymentList(ddlMonth2.SelectedValue.Trim(), ddlFiscalYear2.SelectedValue.Trim(), "");
        grPayment.DataSource = dtPaymentList;
        grPayment.DataBind();
        foreach (GridViewRow gRow in grPayment.Rows)
        {
            // Join Date
            if (Common.CheckNullString(gRow.Cells[5].Text.Trim()) != "")
                gRow.Cells[5].Text = Common.DisplayDate(gRow.Cells[5].Text.Trim());
            //Gr From
            if (Common.CheckNullString(gRow.Cells[6].Text.Trim()) != "")
                gRow.Cells[6].Text = Common.DisplayDate(gRow.Cells[6].Text.Trim());
            //Gr To
            if (Common.CheckNullString(gRow.Cells[7].Text.Trim()) != "")
                gRow.Cells[7].Text = Common.DisplayDate(gRow.Cells[7].Text.Trim());
            //Gr Pay Date
            if (Common.CheckNullString(gRow.Cells[12].Text.Trim()) != "")
                gRow.Cells[12].Text = Common.DisplayDate(gRow.Cells[12].Text.Trim());
        }
    }
    protected void imgSearch2_Click(object sender, ImageClickEventArgs e)
    {
        this.OpenRecord();
    }

    protected void grPayment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                hfIsUpdate.Value = "Y";
                hfLedgerID.Value = grPayment.SelectedRow.Cells[1].Text.Trim();
                lblEmpName.Text = grPayment.SelectedRow.Cells[3].Text.Trim();
                lblJoiningDate.Text = grPayment.SelectedRow.Cells[5].Text.Trim();
                lblDesig.Text = grPayment.SelectedRow.Cells[4].Text.Trim();
                lblGratuityFrom.Text =grPayment.SelectedRow.Cells[6].Text.Trim();
                lblGrauityUpto.Text = grPayment.SelectedRow.Cells[7].Text.Trim();
                txtBasicSal.Text = grPayment.SelectedRow.Cells[8].Text.Trim();
                txtPrevMonthGratuity.Text = grPayment.SelectedRow.Cells[9].Text.Trim();
                txtCurrMonthGratuity.Text = grPayment.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim();
                txtCharging.Text = grPayment.SelectedRow.Cells[10].Text.Trim();
                txtBalance.Text = grPayment.SelectedRow.Cells[11].Text.Trim();
                txtPayDate.Text = grPayment.SelectedRow.Cells[12].Text.Trim();
                txtRemarks.Text = Common.CheckNullString(grPayment.SelectedRow.Cells[13].Text.Trim());
                this.EntryMode(true);
                TabContainer1.ActiveTabIndex = 0;
                break;
            case ("RowDeleting"):
                StringBuilder sb = new StringBuilder();
                //string strEmpDivID = "";
                string strURL = "../../CrystalReports/Payroll/GratuityReportViewer.aspx?params=" + ddlMonth2.SelectedValue.ToString() + "," + ddlFiscalYear2.SelectedValue.ToString() + "," + grPayment.SelectedRow.Cells[2].Text.Trim();
                sb.Append("<script>");
                //sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
                sb.Append("window.open('" + strURL + "', '', '');");
                sb.Append("</script>");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                         sb.ToString(), false);
                ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
                TabContainer1.ActiveTabIndex = 1;
                break;
                

        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objGrMgr.DeleteGrPaymentData(hfLedgerID.Value.Trim());
        lblMsg.Text = "Record Deleted Successfully";
        this.EntryMode(false);
        this.OpenRecord();
        TabContainer1.ActiveTabIndex = 0;
    }

    protected void btnPrint2_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        //string strEmpDivID = "";
        string strURL = "../../CrystalReports/Payroll/GratuityReportViewer.aspx?params=" + ddlMonth2.SelectedValue.ToString() + "," + ddlFiscalYear2.SelectedValue.ToString() + "," + "";
        sb.Append("<script>");
        //sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
        sb.Append("window.open('" + strURL + "', '', '');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                 sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
        TabContainer1.ActiveTabIndex = 1;
    }
   

   

    protected void btnExport1_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=GratuityPaymentList.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        grPayment.Columns[0].Visible = false;
        grPayment.Columns[grPayment.Columns.Count - 1].Visible = false;
        grPayment.RenderControl(htw);
        Response.Write(sw.ToString());
        grPayment.Columns[0].Visible = true;
        grPayment.Columns[grPayment.Columns.Count - 1].Visible = true;
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

        // Confirms that an HtmlForm control is rendered for the
        // specified ASP.NET server control at run time.

    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {

    }
}
