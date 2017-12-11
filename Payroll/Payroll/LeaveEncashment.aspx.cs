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

public partial class Payroll_Payroll_LeaveEncashment : System.Web.UI.Page
{
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    Payroll_GratuityLedgerManager objGrMgr = new Payroll_GratuityLedgerManager();
    LeaveApplicationManager objLeaveMgr = new LeaveApplicationManager();

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
            ddlPayrollMonth.SelectedValue = Convert.ToString(DateTime.Today.Month - 1);

            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0), ddlFiscalYear2, "FISCALYRTITLE", "FISCALYRID", false);
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0), ddlPayrollFinYear, "FISCALYRTITLE", "FISCALYRID", false);
            TabContainer1.ActiveTabIndex = 0;

            txtTimes.Text = "1";
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
            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            imgBtnSearch.Visible = true;
            lblEmpName.Text = "";
            lblEmpName.ToolTip = "";
            lblDesig.ToolTip = "";
            lblDesig.Text = "";
            lblJoiningDate.Text = "";
            //lblGratuityFrom.Text = "";
            //lblGrauityUpto.Text = "";
            txtBasicSal.Text = "0";
            txtLeaveAmt.Text = "0";
            //txtPrevMonthGratuity.Text = "0";
            //txtCharging.Text = "0";
            txtBalance.Text = "0";
            txtPayDate.Text = Common.DisplayDate(DateTime.Today.ToShortDateString());
            txtRemarks.Text = "";
            hfIsUpdate.Value = "N";
            hfLedgerID.Value = "";
            txtLevBal.Text = "0";
            //txtEmpID.Text = "";
            txtUnitDaySal.Text = "0";
            txtBonusAllowance.Text = "0";
            lblDateDuration.Text = "";
            //txtTimes.Text = "1";
            //lblDuration.Text = "";
        }
    }

    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.EntryMode(false);
            lblMsg.Text = "";
            //strBasicSal = Convert.ToString(Common.RoundDecimal(dtEmpInfo.Rows[0]["BASICSAL"].ToString().Trim(), 0));
            string strPrevMonth = Common.GetPreviousMonth(ddlMonth.SelectedValue.Trim());
            string strPrevYear = "";
            DateTime dtJoinDate = new DateTime();

            // Gratuity Calculation

            decimal dclPolicy = Convert.ToDecimal("2");
            decimal dclBasic = 0;
            decimal dclAmount = 0;
            decimal dclDiff = 0;
            TimeSpan ts;

            if (strPrevMonth == "12")
                strPrevYear = Convert.ToString(Convert.ToInt32(ddlYear.SelectedValue.Trim()) - 1);
            else
                strPrevYear = ddlYear.SelectedValue.Trim();

            DataTable dtEmpInfo = objGrMgr.GetEmpInfo(txtEmpID.Text, ddlPayrollMonth.SelectedValue.Trim(), ddlPayrollFinYear.SelectedValue.Trim());

            if (dtEmpInfo.Rows.Count > 0)
            {
                lblEmpName.Text = dtEmpInfo.Rows[0]["FULLNAME"].ToString().Trim();
                lblEmpName.ToolTip = dtEmpInfo.Rows[0]["EMPID"].ToString().Trim();

                if (string.IsNullOrEmpty(dtEmpInfo.Rows[0]["JOININGDATE"].ToString().Trim()) == false)
                {
                    lblJoiningDate.Text = Common.DisplayDate(dtEmpInfo.Rows[0]["JOININGDATE"].ToString().Trim());
                    dtJoinDate = Convert.ToDateTime(dtEmpInfo.Rows[0]["JOININGDATE"].ToString().Trim());
                }
                lblDesig.Text = dtEmpInfo.Rows[0]["JOBTITLE"].ToString().Trim();
                lblDesig.ToolTip = dtEmpInfo.Rows[0]["DESGID"].ToString().Trim();
                txtBasicSal.Text = dtEmpInfo.Rows[0]["BASICSAL"].ToString().Trim();
                this.AvailableLeave("1", txtEmpID.Text.Trim());
                dclAmount = (Convert.ToDecimal(dtEmpInfo.Rows[0]["BASICSAL"].ToString().Trim()) * Convert.ToDecimal(txtLevBal.Text.Trim()) * dclPolicy) / 22;
                txtBalance.Text = Common.RoundDecimal(dclAmount.ToString(), 0).ToString();
                txtLeaveAmt.Text = Common.RoundDecimal(dclAmount.ToString(), 0).ToString();
                txtUnitDaySal.Text = (Math.Round(Convert.ToDecimal(txtLeaveAmt.Text) / Convert.ToDecimal(txtLevBal.Text), 3)).ToString();

                //Get Prorata Bonus
                this.GetFestivalBonus();
            }
            else
            {
                lblEmpName.Text = "";
                lblJoiningDate.Text = "";
                lblDesig.Text = "";
                lblDesig.ToolTip = "";
                txtBalance.Text = "";
                this.EntryMode(false);

                lblMsg.Text = "No Employee Found.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }
    }

    private void GetFestivalBonus()
    {
        decimal dclTotalBonus = 0;
        TimeSpan ts;
        DataTable dt = objGrMgr.GetLastFestivalDate(txtEmpID.Text.Trim());
        if (string.IsNullOrEmpty(dt.Rows[0]["leavingDate"].ToString()) == false)
        {
            DateTime dtToDate = Convert.ToDateTime(dt.Rows[0]["leavingDate"].ToString());
            DateTime dtFromDate = Convert.ToDateTime(Common.ReturnDate(txtLastFestival.Text.Trim()));

            ts = dtToDate - dtFromDate;
            decimal days = Convert.ToDecimal(ts.TotalDays);

            lblDateDuration.Text = " (Date From : " + txtLastFestival.Text.Trim() + " To Date : " + Common.DisplayDate(dt.Rows[0]["leavingDate"].ToString()) + " " + days + " days.)";
            dclTotalBonus = (Convert.ToDecimal(txtBasicSal.Text.Trim()) * days / 365) * Convert.ToDecimal(txtTimes.Text.Trim());
            txtBonusAllowance.Text = Math.Round(dclTotalBonus).ToString();
        }        
    }

    //protected double GetTotalMonths(DateTime dtStart, DateTime dtEnd)
    //{
    //    DateTime start = dtStart;
    //    DateTime end = dtEnd;
    //    int compMonth = (end.Month + end.Year * 12) - (start.Month + start.Year * 12);
    //    double daysInEndMonth = (end - end.AddMonths(1)).Days;
    //    double months = compMonth + (start.Day - end.Day) / daysInEndMonth;
    //    return months;
    //}

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
            if (objGrMgr.IsCurrentMonthLeaveEncashmentPaymentExist(txtEmpID.Text.Trim(), ddlMonth.SelectedValue.Trim(), ddlFiscalYear.SelectedValue.Trim()) == true)
            {
                lblMsg.Text = "Current month payment already exist. Operation cannot be processed.";
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
        dtNextGrFrom = Convert.ToDateTime(Common.ReturnDate(lblJoiningDate.Text.Trim()));
        dtNextGrFrom = dtNextGrFrom.AddDays(1);

        if (lblEmpName.Text == "")
        {
            lblMsg.Text = "No Record to save.";
            return;
        }
        if (hfIsUpdate.Value == "Y")
            strLedgerID = hfLedgerID.Value.Trim();
        else
            strLedgerID = Common.getMaxId("LeaveEncashment", "TransID");

        objGrMgr.InsertLeaveEncashment(strLedgerID,
                                        txtEmpID.Text.Trim(),
                                        lblEmpName.ToolTip.Trim(),
                                        lblDesig.ToolTip.Trim(),
                                        Common.ReturnDate(lblJoiningDate.Text.Trim()),
                                        "",
                                        "",
                                        Common.SetDate(dtNextGrFrom.ToString()),
                                        ddlMonth.SelectedValue.Trim(),
                                        ddlYear.SelectedValue.Trim(),
                                        ddlFiscalYear.SelectedValue.Trim(),
                                        txtBasicSal.Text.Trim(),
                                        "0",
                                        txtLeaveAmt.Text.Trim(),
                                        "0",
                                        Common.ReturnDate(txtPayDate.Text.Trim()),
                                        txtBalance.Text.Trim(),
                                        txtRemarks.Text.Trim(),
                                        Session["USERID"].ToString().Trim(),
                                        Common.SetDateTime(DateTime.Now.ToString()),
                                        txtLevBal.Text.Trim(),
                                        txtUnitDaySal.Text.Trim(),
                                        txtBonusAllowance.Text.Trim());
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
        DataTable dtPaymentList = objGrMgr.GetLeaveEncashmentList(ddlMonth2.SelectedValue.Trim(), ddlFiscalYear2.SelectedValue.Trim(), "");
        grPayment.DataSource = dtPaymentList;
        grPayment.DataBind();
        foreach (GridViewRow gRow in grPayment.Rows)
        {
            // Join Date
            if (Common.CheckNullString(gRow.Cells[5].Text.Trim()) != "")
                gRow.Cells[5].Text = Common.DisplayDate(gRow.Cells[5].Text.Trim());
            if (Common.CheckNullString(gRow.Cells[6].Text.Trim()) != "")
                gRow.Cells[6].Text = (Math.Round(Convert.ToDecimal(gRow.Cells[6].Text.Trim()), 1)).ToString();
            if (Common.CheckNullString(gRow.Cells[10].Text.Trim()) != "")
                gRow.Cells[10].Text = Common.DisplayDate(gRow.Cells[10].Text.Trim());
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
                txtEmpID.Text = grPayment.SelectedRow.Cells[2].Text.Trim();
                lblEmpName.Text = grPayment.SelectedRow.Cells[3].Text.Trim();

                lblDesig.Text = grPayment.SelectedRow.Cells[4].Text.Trim();
                lblJoiningDate.Text = grPayment.SelectedRow.Cells[5].Text.Trim();
                txtLevBal.Text = grPayment.SelectedRow.Cells[6].Text.Trim();

                txtBasicSal.Text = grPayment.SelectedRow.Cells[7].Text.Trim();
                txtUnitDaySal.Text = grPayment.SelectedRow.Cells[8].Text.Trim();
                txtBalance.Text = grPayment.SelectedRow.Cells[9].Text.Trim();
                
                txtPayDate.Text = grPayment.SelectedRow.Cells[10].Text.Trim();
                txtRemarks.Text = Common.CheckNullString(grPayment.SelectedRow.Cells[11].Text.Trim());
                txtLeaveAmt.Text = grPayment.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim();

                this.EntryMode(true);
                TabContainer1.ActiveTabIndex = 0;
                break;

            case ("RowDeleting"):
                StringBuilder sb = new StringBuilder();
                //string strEmpDivID = "";
                string strURL = "../../CrystalReports/Payroll/LeaveEncashReportViewer.aspx?params=" + ddlMonth2.SelectedValue.ToString() + "," + ddlFiscalYear2.SelectedValue.ToString() + "," + grPayment.SelectedRow.Cells[2].Text.Trim();
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
        objGrMgr.DeleteLeaveEncashmentData(hfLedgerID.Value.Trim());
        lblMsg.Text = "Record Deleted Successfully";
        this.EntryMode(false);
        this.OpenRecord();
        TabContainer1.ActiveTabIndex = 0;
    }

    protected void btnPrint2_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        //string strEmpDivID = "";
        string strURL = "../../CrystalReports/Payroll/LeaveEncashReportViewer.aspx?params=" + ddlMonth2.SelectedValue.ToString() + "," + ddlFiscalYear2.SelectedValue.ToString() + "," + "";
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
        this.EntryMode(false);
        lblMsg.Text = "";
        txtEmpID.Text = "";
    }

    private void AvailableLeave(string strLType, string strEmpID)
    {
        DataTable dtLeaveProfile = objLeaveMgr.SelectEmpLeaveProfile(strEmpID, strLType);
        decimal intAvailable = 0;
        decimal LCarryOverd = 0;
        decimal LEntitled = 0;
        //decimal LCashed = 0;
        decimal LEnjoyed = 0;
        decimal LeaveElapsed = 0;
        if (dtLeaveProfile.Rows.Count > 0)
        {
            if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["LCarryOverd"].ToString()) == false)
                LCarryOverd = LCarryOverd + Convert.ToDecimal(dtLeaveProfile.Rows[0]["LCarryOverd"].ToString());
            else
                LCarryOverd = 0;

            if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["LEntitled"].ToString()) == false)
                LEntitled = LEntitled + Convert.ToDecimal(dtLeaveProfile.Rows[0]["LEntitled"].ToString());
            else
                LEntitled = 0;

            if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["LeaveEnjoyed"].ToString()) == false)
                LEnjoyed =Convert.ToDecimal(dtLeaveProfile.Rows[0]["LeaveEnjoyed"].ToString());
            else
                LEnjoyed = 0;


            intAvailable = (LCarryOverd + LEntitled) - LEnjoyed;
            txtLevBal.Text = Convert.ToString(Math.Round(intAvailable, 1));
            if (Convert.ToDecimal(txtLevBal.Text) < 0)
            {
                txtLevBal.Text = "0";
            }
        }
    }

    protected void txtTimes_TextChanged(object sender, EventArgs e)
    {
        txtBonusAllowance.Text = (Convert.ToDecimal(txtBonusAllowance.Text.Trim()) * Convert.ToDecimal(txtTimes.Text.Trim())).ToString();
    }
}


