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
public partial class Payroll_Payroll_ITCalculationReport : System.Web.UI.Page
{
    Payroll_ITDepositRecords objITMgr = new Payroll_ITDepositRecords();
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0, "F"), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);
            lblMsg.Text = "Saved...";
        }
    }


    protected void OpenRecord()
    {
        string strEmpID="0";
        if(txtEmpID.Text.Trim() !="")
            strEmpID=txtEmpID.Text.Trim();
        DataTable dtEmployee = objITMgr.GetITCalculationReportData(strEmpID, ddlMonth.SelectedValue.Trim(), ddlFiscalYear.SelectedValue.Trim());
        grEmployee.DataSource = dtEmployee;
        grEmployee.DataBind();
        lblRecordCount.Text = grEmployee.Rows.Count.ToString();
        int i = 1;

        foreach (GridViewRow gRow in grEmployee.Rows)
        {
            gRow.Cells[0].Text = i.ToString();
            if (Common.CheckNullString(gRow.Cells[7].Text) != "")
                gRow.Cells[7].Text = Common.DisplayDate(gRow.Cells[7].Text.Trim());
            i++;
        }
    }

    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        this.OpenRecord();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {  
        string attachment = "attachment; filename=ITReport.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        grEmployee.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

        // Confirms that an HtmlForm control is rendered for the
        // specified ASP.NET server control at run time.

    }
    protected void btnIndReport_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        string EId = "";
        if (txtEmpID.Text.Trim() == "")
            EId = "0";
        else
            EId = txtEmpID.Text.Trim();

        StringBuilder sb = new StringBuilder();
       // string strURL = "../../CrystalReports/Payroll/ITReportViwer.aspx?params=" + txtEmpID.Text.Trim() + "," + ddlMonth.SelectedValue.Trim() + "," + ddlFiscalYear.SelectedValue.Trim();
        string strURL = "../../CrystalReports/Payroll/ITReportViwer.aspx?params=" + EId + "," + ddlMonth.SelectedValue.Trim() + "," + ddlFiscalYear.SelectedValue.Trim();
        sb.Append("<script>");
        //sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
        sb.Append("window.open('" + strURL + "', '', '');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                 sb.ToString(), false);
    }
}
