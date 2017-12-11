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
using System.Text;
using System.IO;

public partial class Payroll_Payroll_EmailMonthlyPayslip : System.Web.UI.Page
{
    MasterTablesManager objMastMg = new MasterTablesManager();
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    PayrollReportManager objPayRptMgr = new PayrollReportManager();
    MailManagerSmtpClient objMail = new MailManagerSmtpClient();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            Common.FillDropDownList(objMastMg.SelectLocation(0), ddlGenerateValue, "PostingPlaceName", "PostingPlaceId", false);
            Common.FillDropDownList(objPayrollMgr.SelectBankAndBranchList(), ddlBank, "BANKBRANCH", "RoutingNo", true, "Nil");
        }
    }

    protected void OpenRecord()
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

        DataTable dtEmp = objPayRptMgr.GetPayslipEmployeeData(ddlGeneratefor.SelectedValue.ToString(), strGenerateValue, ddlMonth.SelectedValue.ToString(),
            ddlYear.SelectedValue.ToString(), ddlBank.SelectedValue.Trim(), chkBonus.Checked == true ? "B" : "S");
        grEmployee.DataSource = dtEmp;
        grEmployee.DataBind();

        //for (int i = 1; i <= grEmployee.Rows.Count; i++)
        //{
        //    CheckBox chkB = (CheckBox)grEmployee.Rows[i - 1].Cells[0].FindControl("chkBox");
        //    grEmployee.Rows[i - 1].Cells[1].Text = i.ToString();
        //    if (Common.CheckNullString(grEmployee.Rows[i - 1].Cells[7].Text) == "")
        //    {
        //        grEmployee.Rows[i - 1].Enabled = false;
        //        chkB.Checked = false;
        //    }
        //}
    }

    protected void btnGetEmployee_Click(object sender, EventArgs e)
    {
        this.OpenRecord();
    }

    protected void ResetGridView(GridView gr, bool IsVisible)
    {
        gr.Visible = IsVisible;
        gr.DataSource = null;
        gr.DataBind();
    }

    protected String GetGridViewHTML(GridView gr)
    {
        StringBuilder SB = new StringBuilder();
        StringWriter SW = new StringWriter(SB);
        HtmlTextWriter htmlTW = new HtmlTextWriter(SW);
        gr.RenderControl(htmlTW);
        String grHTML = SB.ToString();
        return grHTML;
    }

    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        string strRetText = "";
        string strToAddr = "";
        string strSubject = "";
        string strBody = "";
        string strFromAddr = Session["EMAILID"].ToString().Trim();
        string strPayslipMonth = ddlMonth.SelectedItem.Text.Trim() + " " + ddlYear.SelectedValue.Trim();
        string strSalType = chkBonus.Checked == true ? "B" : "S";

        foreach (GridViewRow gRow in grEmployee.Rows)
        {
            CheckBox chkB = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            if (chkB.Checked == true)
            {
                this.ResetGridView(grGrossandBenefits, true);
                this.ResetGridView(grDeduct, true);
                this.ResetGridView(grNetPay, true);

                grGrossandBenefits.DataSource = objPayRptMgr.GetPayslipMonthlyGrossAndBenefits(gRow.Cells[2].Text.Trim(), ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), strSalType);
                grGrossandBenefits.DataBind();

                //grDeduct.DataSource = objPayRptMgr.GetPayslipMonthlyDeductions(gRow.Cells[2].Text.Trim(), ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), strSalType);
                //grDeduct.DataBind();

                //grNetPay.DataSource = objPayRptMgr.GetPayslipMonthlyNetPay(gRow.Cells[2].Text.Trim(), ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), strSalType);
                //grNetPay.DataBind();

                lblRemarks.Text = objPayRptMgr.GetPayrollRemarksForPayslip(gRow.Cells[2].Text.Trim(), ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), strSalType);
                if (string.IsNullOrEmpty(lblRemarks.Text) == false)
                    lblRemarks.Text = "* " + lblRemarks.Text;

                this.FormatGrossAndBenefitsGrid();
                this.FormatDeductionGrid();

                strToAddr = gRow.Cells[7].Text.Trim();
                if (chkBonus.Checked == true)
                    strSubject = "Festival Allowance - " + strPayslipMonth;
                else
                    strSubject = "Payslip for the Month of " + strPayslipMonth;
                strBody = " <pre style=" + "\"font-weight:bold;font-size:14px;font-family:Arial;\"" + "> " + "Marie Stopes " + " </pre> "
                + " <pre style=" + "\"font-size:12px;font-family:Arial;\"" + "> " + gRow.Cells[3].Text.Trim() + " </pre> "
                + " <pre style=" + "\"font-size:12px;font-family:Arial;\"" + "> " + gRow.Cells[5].Text.Trim() + " </pre> "
                + " <pre style=" + "\"font-size:12px;font-family:Arial;\"" + "> " + gRow.Cells[4].Text.Trim() + " </pre> "
                + " <pre style=" + "\"font-size:12px;font-family:Arial;\"" + "> " + "Employee ID: " + gRow.Cells[2].Text.Trim() + " </pre> "
                + " <pre> </pre>"
                + " <pre style=" + "\"font-size:12px;font-family:Arial;\"" + "> " + "Date of Issue : " + DateTime.Today.ToLongDateString() + " </pre> "
                + " <pre> </pre>"
                + " <pre style=" + "\"font-weight:bold;font-size:14px;font-family:Arial;\"" + "> " + strSubject + " </pre> "
                + " <pre> </pre>"
                + " <table style=" + "\"width:100%;\"" + "> "
                + " \n "
                + " <tr> "
                + " \n "
                + " <td style=" + "\"width:49%;vertical-align:top;\"" + "> "
                + " \n "
                + this.GetGridViewHTML(grGrossandBenefits)
                + " \n "
                + " </td> "
                + " \n "
                + " <td style=" + "\"width:49%;vertical-align:top;\"" + "> "
                + " \n "
                + this.GetGridViewHTML(grDeduct)
                + " <pre> </pre>"
                + this.GetGridViewHTML(grNetPay)
                + " <pre> </pre>"
                + lblRemarks.Text.Trim()
                + " </td> "
                + " \n "
                + " </tr> "
                + " \n "
                + " <tr> "
                + " \n "
                + " <td colspan=" + "\"2\"" + ">"
                + " \n "
                + " <pre> </pre>"
                + " <pre style=" + "\"font-size:12px;font-family:Arial;\"" + "> "
                + " Note: This is a computer generated payslip and does not require any signature. If any discrepancy is found please "
                + " inform HR Team within 7 days of the issuance. "
                + "</pre> "
                + " </td> "
                + " </tr> "
                + "\n "
                + " </table> "
                + " \n ";

                strRetText = objMail.PayslipEmail(strFromAddr, strToAddr, strSubject, strBody, "");
                if (strRetText == "N")
                {
                    gRow.Cells[1].ForeColor = System.Drawing.Color.Red;
                    gRow.Cells[2].ForeColor = System.Drawing.Color.Red;
                    gRow.Cells[3].ForeColor = System.Drawing.Color.Red;
                    gRow.Cells[4].ForeColor = System.Drawing.Color.Red;
                    gRow.Cells[5].ForeColor = System.Drawing.Color.Red;
                    gRow.Cells[6].ForeColor = System.Drawing.Color.Red;
                    gRow.Cells[7].ForeColor = System.Drawing.Color.Red;
                    break;
                }
                chkB.Checked = false;
                gRow.Cells[1].ForeColor = System.Drawing.Color.Green;
                gRow.Cells[2].ForeColor = System.Drawing.Color.Green;
                gRow.Cells[3].ForeColor = System.Drawing.Color.Green;
                gRow.Cells[4].ForeColor = System.Drawing.Color.Green;
                gRow.Cells[5].ForeColor = System.Drawing.Color.Green;
                gRow.Cells[6].ForeColor = System.Drawing.Color.Green;
                gRow.Cells[7].ForeColor = System.Drawing.Color.Green;
            }
        }
        this.ResetGridView(grGrossandBenefits, false);
        this.ResetGridView(grDeduct, false);
        this.ResetGridView(grNetPay, false);
        lblRemarks.Text = "";
        if (strRetText == "N")
            lblMsg.Text = "Failure in Sending Mail";
        if (strRetText == "Y")
            lblMsg.Text = "Payslip email has been sent successfully";
    }

    protected void FormatGrossAndBenefitsGrid()
    {
        foreach (GridViewRow gRow in grGrossandBenefits.Rows)
        {
            if (grGrossandBenefits.DataKeys[gRow.DataItemIndex].Values[0].ToString() == "A")
            {
                gRow.Font.Bold = true;
            }
            if (grGrossandBenefits.DataKeys[gRow.DataItemIndex].Values[0].ToString() == "B")
            {
                gRow.Font.Bold = true;
            }
            if (grGrossandBenefits.DataKeys[gRow.DataItemIndex].Values[0].ToString() == "Y")
            {
                gRow.Cells[1].Text = "";
            }
        }
    }

    protected void FormatDeductionGrid()
    {
        foreach (GridViewRow gRow in grDeduct.Rows)
        {
            if (grDeduct.DataKeys[gRow.DataItemIndex].Values[0].ToString() == "C")
            {
                gRow.Font.Bold = true;
            }
        }
    }

    protected void grEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("ViewClick"):
                //Open New Window
                StringBuilder sb = new StringBuilder();
                string strURL = "MonthlyPaySlipReport.aspx?params=" + ddlMonth.SelectedValue.ToString() + "," + ddlMonth.SelectedItem.Text.Trim() + "," + ddlYear.SelectedValue.ToString() + ","
                    + grEmployee.SelectedRow.Cells[2].Text.Trim() + "," + (chkBonus.Checked == true ? "B" : "S");
                sb.Append("<script>");
                //sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
                sb.Append("window.open('" + strURL + "', '', '');");
                sb.Append("</script>");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                         sb.ToString(), false);
                ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
                break;
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the
        // specified ASP.NET server control at run time.
        // No code required here.
    }
}
