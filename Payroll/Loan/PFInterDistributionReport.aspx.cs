using System;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public partial class Payroll_Loan_PFInterDistributionReport : System.Web.UI.Page
{
    MasterTablesManager MasMgr = new MasterTablesManager();
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0, "P"), ddlYear, "FISCALYRTITLE", "FISCALYRID", false);
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        string strURL = "PFInterDistributionViewer.aspx?params=" + ddlYear.SelectedValue.Trim() + ","
                                                                 + ddlYear.SelectedItem.Text.Trim();
        sb.Append("<script>");
        //sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
        sb.Append("window.open('" + strURL + "', '', '');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                 sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
    }
    protected void btnShowCrystalRpt_Click(object sender, EventArgs e)
    {        
        string strYear = ddlYear.SelectedItem.Text == "Nil" ? "0" : ddlYear.SelectedValue.Trim();
        string strFiscalYear = ddlYear.SelectedItem.Text == "Nil" ? "0" : ddlYear.SelectedValue.Trim();        

        Session["REPORTID"] = "PFINDISRPT";
        Session["FiscalYr"] = strFiscalYear;
        Session["FiscalYrValue"] = ddlYear.SelectedItem.Text == "Nil" ? "0" : ddlYear.SelectedItem.Text;

        Session["InterestRate"] = txtPFRate.Text; 

        StringBuilder sb = new StringBuilder();

        sb.Append("<script>");
        sb.Append("window.open('../../CrystalReports/Payroll/PFInterDistributionReportViewer.aspx', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                 sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
    }
}
