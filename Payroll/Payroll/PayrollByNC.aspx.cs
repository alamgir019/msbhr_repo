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

public partial class Payroll_Payroll_PayrollByNC : System.Web.UI.Page
{
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            Common.FillDropDownList(objPayrollMgr.SelectBankAndBranchList(), ddlBank, "BANKBRANCH", "RoutingNo", true, "Nil");
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0), ddlFisYear, "FISCALYRTITLE", "FISCALYRID", false);
            ddlFisYear.SelectedValue = Session["FISCALYRID"].ToString().Trim();

            ddlMonth.Enabled = true;
            ddlYear.Enabled = true;
            rdbSalaryType.Enabled = true;
            ddlFisYear.Enabled = true;

        }
    }

    protected void dtnPrint_Click(object sender, EventArgs e)
    {
        string strURL="";
        string strFiscalYearID = "";
        if (rdbReportType.SelectedValue.Trim() == "0")
            strFiscalYearID = "0";
        else
            strFiscalYearID = ddlFisYear.SelectedValue.Trim();
        StringBuilder sb = new StringBuilder();
        strURL = "PayrollByNCReport.aspx?params=" + ddlMonth.SelectedValue.ToString().Trim() + ","
                                                    + ddlYear.SelectedValue.ToString().Trim() + ","
                                                    + ddlBank.SelectedValue.Trim() + ","
                                                    + ddlBank.SelectedItem.Text.Trim() + ","
                                                    + rdbSalaryType.SelectedValue.Trim() + ","
                                                    + strFiscalYearID + ","
                                                    + ddlFisYear.SelectedItem.Text.Trim();
                                          
                                                   

        sb.Append("<script>");
        //sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
        sb.Append("window.open('" + strURL + "', '', '');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                 sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
    }
    protected void rdbReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbReportType.SelectedValue == "0")
        {
            ddlMonth.Enabled = true;
            ddlYear.Enabled = true;
            rdbSalaryType.Enabled = true;
            ddlFisYear.Enabled = true;
        }
        else
        {
            ddlMonth.Enabled = false;
            ddlYear.Enabled = false;
            rdbSalaryType.Enabled = false;
            ddlFisYear.Enabled = true;
        }


    }
}
