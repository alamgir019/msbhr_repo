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

public partial class Payroll_Payroll_MonthlyBankStatement : System.Web.UI.Page
{
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    MasterTablesManager objMastMg = new MasterTablesManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            Common.FillDropDownList_Nil(objMastMg.SelectClinic(), ddlClinic);
            Common.FillDropDownList(objPayrollMgr.SelectBankAndBranchList(), ddlBank, "BANKBRANCH", "RoutingNo", true, "Nil");
            //Common.FillDropDownList(objMastMg.SelectEmpGroup(0), ddlGroup, "GrpName", "EmpGrpID", false);  
        }
    }

    protected void dtnPrint_Click(object sender, EventArgs e)
    {
        string strURL="";
        StringBuilder sb = new StringBuilder();
        if (rdbReportType.SelectedValue.Trim() == "D")
            strURL = "BankStatementDetailsReport.aspx?params=" + ddlMonth.SelectedValue.ToString().Trim() + "," + ddlYear.SelectedValue.ToString().Trim() +  ","  + ddlBank.SelectedValue.Trim() +"," + ddlClinic.SelectedValue.Trim()+  "," + rdbSalaryType.SelectedValue.Trim();
        else
            strURL = "BankStatementShortReport.aspx?params=" + ddlMonth.SelectedValue.ToString().Trim() + "," + ddlYear.SelectedValue.ToString().Trim() + "," + ddlBank.SelectedValue.Trim() + "," + ddlClinic.SelectedValue.Trim() + "," + ddlBank.SelectedItem.Text.Trim() + "," + rdbSalaryType.SelectedValue.Trim();

        sb.Append("<script>");
        //sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
        sb.Append("window.open('" + strURL + "', '', '');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                 sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
    }
}
