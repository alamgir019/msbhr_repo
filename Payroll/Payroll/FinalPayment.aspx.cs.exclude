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

public partial class Payroll_Payroll_FinalPayment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        string strURL = "../../CrystalReports/Payroll/FinalPaymentReportViewer.aspx?params=" + ddlMonth.SelectedValue.ToString() + "," + ddlYear.SelectedValue.ToString() + "," + txtEmpId.Text.Trim() + "," + (chkIsFromPayroll.Checked == true ? "Y" : "N");
        sb.Append("<script>");
        sb.Append("window.open('" + strURL + "', '', '');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit", sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
        ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
        txtEmpId.Text = "";
    }
}
