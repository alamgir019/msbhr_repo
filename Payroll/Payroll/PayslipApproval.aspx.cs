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

public partial class Payroll_Payroll_PayslipApproval : System.Web.UI.Page
{
    Payroll_PayslipApprovalManager objPSAppMgr = new Payroll_PayslipApprovalManager();
    MasterTablesManager objMastMg = new MasterTablesManager();
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            Common.FillDropDownList_All(objMastMg.SelectClinic(), ddlGenerateValue);
            Common.FillDropDownList(objPayrollMgr.SelectBankAndBranchList(), ddlBank, "BANKBRANCH", "RoutingNo", true, "Nil");
            Common.FillDropDownList(objMastMg.SelectEmpType(0,"Y"), ddlEmpType, "TypeName", "EmpTypeID", false);
            //Common.FillDropDownList(objMastMg.SelectEmpGroup(0), ddlGroup, "GrpName", "EmpGrpID", false);
            //this.GetPendingPaySlipData();
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        // this.OpenRecord();
        this.GetPendingPaySlipData();

        //grPayslipMst.DataSource = objPayslip.Tables["dtPaySlipMst"];
        // grPayslipMst.DataBind();

        // this.WritePaySlipDetailsToXmlFile();
    }

    protected void GetPendingPaySlipData()
    {
        string strGenerateValue = "";
        switch (ddlGeneratefor.SelectedValue.ToString())
        {
            case "O":
                strGenerateValue = ddlGenerateValue.SelectedValue.ToString();
                break;
            case "B":
                strGenerateValue = ddlBank.SelectedValue.ToString();
                break;
            case "E":
                strGenerateValue = txtTextValue.Text.Trim();
                break;
        }

        DataTable dtEmpPayroll = objPSAppMgr.GetPayslipPreparedData(ddlGeneratefor.SelectedValue.ToString(), strGenerateValue,
            ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), ddlBank.SelectedValue.Trim(),ddlEmpType.SelectedValue.ToString());
        grPayslipMst.DataSource = dtEmpPayroll;
        grPayslipMst.DataBind();
        this.FormatGridView();
    }

    protected void FormatGridView()
    {
        int i = 1;
        foreach (GridViewRow gRow in grPayslipMst.Rows)
        {
            gRow.Cells[1].Text = i.ToString();
            if (Common.CheckNullString(gRow.Cells[6].Text) != "")
                gRow.Cells[6].Text = Common.DisplayDate(gRow.Cells[6].Text);
            if (Common.CheckNullString(gRow.Cells[7].Text) != "")
                gRow.Cells[7].Text = Common.DisplayDate(gRow.Cells[7].Text);
            if (Common.CheckNullString(gRow.Cells[14].Text) != "")
                gRow.Cells[14].Text = Common.DisplayDate(gRow.Cells[14].Text);
            i++;
        }
    }

    protected void grPayslipMst_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;

        switch (_commandName)
        {
            case ("DoubleClick"):
                ModalPopupTree.Show();

                hfPSBID.Value = grPayslipMst.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim();
                hfPayID.Value = grPayslipMst.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim();
                hfEmpID.Value = grPayslipMst.DataKeys[_gridView.SelectedIndex].Values[2].ToString().Trim();

                lblEmpID.Text = grPayslipMst.SelectedRow.Cells[2].Text.Trim();
                lblEmpName.Text = grPayslipMst.SelectedRow.Cells[3].Text.Trim();
                lblLeaveTitle.Text = grPayslipMst.SelectedRow.Cells[11].Text.Trim();
                //lblFromTo.Text = lblFrom.Text;
                DataTable dtPaySlipDets = new DataTable();
                dtPaySlipDets = objPSAppMgr.GetPayslipDetailsData(hfPSBID.Value.ToString(), hfEmpID.Value.ToString());
                grPaySlipDetls.DataSource = dtPaySlipDets;
                grPaySlipDetls.DataBind();

                int i = 1;
                decimal total = 0;
                foreach (GridViewRow gRow in grPaySlipDetls.Rows)
                {
                    gRow.Cells[0].Text = i.ToString();
                    i++;

                    TextBox txtPayAmnt = (TextBox)gRow.FindControl("txtPayAmnt");
                    if (string.IsNullOrEmpty(txtPayAmnt.Text.Trim()) == false)
                    {
                        total = total + Convert.ToDecimal(txtPayAmnt.Text.Trim());
                    }
                }

                grPaySlipDetls.FooterRow.Cells[1].Text = "Total Amount :";
                grPaySlipDetls.FooterRow.Cells[2].Text = total.ToString();
                break;
        }
    }

    protected void btnSaveAndApprove_Click(object sender, EventArgs e)
    {
        string strRetValue = objPSAppMgr.SaveAndApproveData(grPaySlipDetls, hfPSBID.Value, hfEmpID.Value, hfPayID.Value, Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()));
        string[] strArr = strRetValue.Split(',');
        grPayslipMst.SelectedRow.Cells[9].Text = strArr[0];
        grPayslipMst.SelectedRow.Cells[8].Text = strArr[1];
        CheckBox chkB = (CheckBox)grPayslipMst.SelectedRow.FindControl("chkBox");
        grPayslipMst.SelectedRow.BackColor = System.Drawing.Color.Green;
    }

    protected void DeleteData()
    {
        if (objPSAppMgr.DeleteData(grPayslipMst) == true)
        {
            lblMsg.Text = "Selected data deleted successfully";
            this.GetPendingPaySlipData();
        }
        else
        {
            lblMsg.Text = "no data data has been selected to delete";
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        this.DeleteData();
    }

    protected void imgbtnSaveAndApprove_Click(object sender, ImageClickEventArgs e)
    {
        string strRetValue = objPSAppMgr.SaveAndApproveData(grPaySlipDetls, hfPSBID.Value, hfEmpID.Value, hfPayID.Value, Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()));
        string[] strArr = strRetValue.Split(',');
        grPayslipMst.SelectedRow.Cells[9].Text = strArr[0];
        grPayslipMst.SelectedRow.Cells[8].Text = strArr[1];
        CheckBox chkB = (CheckBox)grPayslipMst.SelectedRow.FindControl("chkBox");
        grPayslipMst.SelectedRow.BackColor = System.Drawing.Color.Green;
    }

    private void SendEmail()
    {
        MasterTablesManager MasMgr = new MasterTablesManager();
        MailManagerSmtpClient objMail = new MailManagerSmtpClient();

        DataTable dt = MasMgr.GetEmailNotification();

        string strRetText = "";
        string strToAddr = dt.Rows[0]["Verify"].ToString().Trim();
        string strSubject = "Salary of " + ddlMonth.SelectedItem.ToString().Trim() + " has been waiting for your verification";
        string strBody = "Hi,<br />The salary has been prepared by HR. Please verify and forword to Dir finance & Admin.";
        string strFromAddr = Session["EMAILID"].ToString().Trim();

        strRetText = objMail.PayslipEmail(strFromAddr, strToAddr, strSubject, strBody, "");

        if (strRetText == "Y")
            lblMsg.Text = lblMsg.Text + "Email has been sent to Verify by Finance.";
        else
            lblMsg.Text = lblMsg.Text + "Email sending failed.";
    }

    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        if (grPayslipMst.Rows.Count > 0)
        {
            //Send Email
            this.SendEmail();
        }
        else
        {
            lblMsg.Text = "No Record Found to Verify";
        }
    }
    protected void ddlGeneratefor_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
