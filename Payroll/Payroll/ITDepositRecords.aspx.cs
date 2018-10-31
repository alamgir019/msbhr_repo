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

public partial class Payroll_Payroll_ITDepositRecords : System.Web.UI.Page
{
    MasterTablesManager objMastMg = new MasterTablesManager();
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    PayrollReportManager objPayRptMgr = new PayrollReportManager();
    MailManagerSmtpClient objMail = new MailManagerSmtpClient();
    Payroll_ITDepositRecords objITMgr = new Payroll_ITDepositRecords();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0, "T"), ddlFinYear, "FISCALYRTITLE", "FISCALYRID", false);
            Common.FillDropDownList_All(objMastMg.SelectDivision(0), ddlGenerateValue);
        }
    }

    protected void OpenRecord()
    {
        string strGenerateValue = ddlGenerateValue.SelectedValue.ToString().Trim();
        DataTable dtITDeposit = objITMgr.GetExistingData(ddlGenerateValue.SelectedValue.Trim(), ddlMonth.SelectedValue.Trim(),
            ddlYear.SelectedValue.Trim(), ddlFinYear.SelectedValue.Trim());

        if (dtITDeposit.Rows.Count > 0)
        {
            txtChallanNo.Text = dtITDeposit.Rows[0]["CHALLANNO"].ToString().Trim();
            txtBank.Text = dtITDeposit.Rows[0]["BANKNAME"].ToString().Trim();
            txtDepositDate.Text = Common.DisplayDate(dtITDeposit.Rows[0]["CHALLANDATE"].ToString().Trim());
            btnSave.Text = "Update";
        }
        else
        {
            txtChallanNo.Text = "";
            txtBank.Text = "";
            txtDepositDate.Text = "";
            btnSave.Text = "Save";
        }

        DataTable dtEmp = objITMgr.GetEmployeeITData("D", strGenerateValue, ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString());
        grEmployee.DataSource = dtEmp;
        grEmployee.DataBind();
        decimal decTotal = 0;
        for (int i = 1; i <= grEmployee.Rows.Count; i++)
        {
            grEmployee.Rows[i - 1].Cells[0].Text = i.ToString();
            decTotal = decTotal + Common.RoundDecimal(grEmployee.Rows[i - 1].Cells[6].Text, 2);
        }
        if (grEmployee.Rows.Count > 0)
        {
            grEmployee.FooterRow.Cells[5].Text = "Total";
            grEmployee.FooterRow.Cells[6].Text = decTotal.ToString();
            grEmployee.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
        }
    }

    protected void btnGetEmployee_Click(object sender, EventArgs e)
    {
        this.OpenRecord();
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
                string strURL = "MonthlyPaySlipReport.aspx?params=" + ddlMonth.SelectedValue.ToString() + "," + ddlMonth.SelectedItem.Text.Trim() + "," + ddlYear.SelectedValue.ToString() + "," + grEmployee.SelectedRow.Cells[1].Text.Trim() + "," + "S";
                sb.Append("<script>");               
                sb.Append("window.open('" + strURL + "', '', '');");
                sb.Append("</script>");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                         sb.ToString(), false);
                ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
                break;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (grEmployee.Rows.Count == 0)
        {
            lblMsg.Text = "No record to save";
            return;
        }
        objITMgr.InsertData(grEmployee, ddlGenerateValue.SelectedValue.Trim(), ddlMonth.SelectedValue.Trim(),
            ddlYear.SelectedValue.Trim(), ddlFinYear.SelectedValue.Trim(), txtChallanNo.Text.Trim(), txtBank.Text.Trim(),
           Common.ReturnDate(txtDepositDate.Text.Trim()), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));
        lblMsg.Text = "Records saved successfully";
        btnSave.Text = "Update";
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        grEmployee.Columns[7].Visible = false;
        string attachment = "attachment; filename=ITDeposit.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        grEmployee.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.Flush();
        Response.End();
        grEmployee.Columns[7].Visible = true;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

        // Confirms that an HtmlForm control is rendered for the
        // specified ASP.NET server control at run time.

    }
}
