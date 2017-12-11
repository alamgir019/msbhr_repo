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

public partial class Payroll_Loan_TotalPF : System.Web.UI.Page
{
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //this.EntryMode(false);
           // Common.FillMonthList(ddlMonth);
            //Common.FillYearList(5, ddlYear);
           // ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
           // ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0, "P"), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);
        }
    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        string strURL = "../../CrystalReports/Payroll/TotalPFViewer.aspx?params=" + ddlFiscalYear.SelectedValue.ToString() + "," + ddlFiscalYear.SelectedItem.Text.Trim();
        sb.Append("<script>");
        //sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
        sb.Append("window.open('" + strURL + "', '', '');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                 sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
    }
}
