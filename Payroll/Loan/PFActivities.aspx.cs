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

public partial class Payroll_Loan_PFActivities : System.Web.UI.Page
{
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    Payroll_LoanAppManager objLoanMgr = new Payroll_LoanAppManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0, "P"), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);
            TabContainer1.ActiveTabIndex = 0;
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        DataSet ds = objLoanMgr.GetPFMonthlyActivities(ddlMonth.SelectedValue.Trim(), ddlFiscalYear.SelectedValue.Trim());
        grLoan.DataSource = ds.Tables[0];
        grLoan.DataBind();

        grAdjust.DataSource = ds.Tables[1];
        grAdjust.DataBind();

        grFinalPayment.DataSource = ds.Tables[2];
        grFinalPayment.DataBind();
        
    }
    protected void ExportToExcel(GridView grv)
    {
        string attachment = "attachment; filename=Export.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        grv.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

        // Confirms that an HtmlForm control is rendered for the
        // specified ASP.NET server control at run time.

    }
    protected void btnExport1_Click(object sender, EventArgs e)
    {
        this.ExportToExcel(grLoan);
    }
    protected void btnExport2_Click(object sender, EventArgs e)
    {
        this.ExportToExcel(grAdjust);
    }
    protected void btnExport3_Click(object sender, EventArgs e)
    {
        this.ExportToExcel(grFinalPayment);
    }
}
