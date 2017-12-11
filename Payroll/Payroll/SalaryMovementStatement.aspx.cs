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
public partial class Payroll_Payroll_SalaryMovementStatement : System.Web.UI.Page
{
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
           // Common.FillDropDownList(objMastMg.SelectLocation(0), ddlGenerateValue, "LocationName", "LocationID", false);
            Common.FillDropDownList(objPayrollMgr.SelectBankAndBranchList(), ddlBank, "BANKBRANCH", "RoutingNo", true, "Nil");
        }
    }

    protected void dtnPrint_Click(object sender, EventArgs e)
    {
       
    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        string strGenerateValue = "";
        string strGenerateText = "";
        switch (ddlGeneratefor.SelectedValue.ToString())
        {
            case "B":
                strGenerateValue = ddlBank.SelectedValue.ToString();
                strGenerateText = ddlBank.SelectedItem.Text.Trim();
                break;
            case "A":
                strGenerateValue = "";
                break;
        }

        StringBuilder sb = new StringBuilder();
        string strURL = "SalaryMovementStatementReport.aspx?params=" + ddlMonth.SelectedValue.ToString().Trim() + ","
                                                                     + ddlYear.SelectedValue.Trim() + ","
                                                                     + ddlGeneratefor.SelectedValue.Trim() + ","
                                                                     + strGenerateValue + ","
                                                                     + strGenerateText; 
                                                                                                                                            
        sb.Append("<script>");
        //sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
        sb.Append("window.open('" + strURL + "', '', '');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                 sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
    }
}
