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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Payroll_Loan_PFLoanLedger : System.Web.UI.Page
{
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    PayrollReportManager objPayRptMgr = new PayrollReportManager();
    DataTable MyDataTable = new DataTable();
    ReportDocument ReportDoc = new ReportDocument();
    string ReportPath = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //this.EntryMode(false);
            Common.FillMonthList(ddlMonth);
            //Common.FillYearList(5, ddlYear);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
           // ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0, "P"), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);
        }
    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        string fileName = "";
        ReportDoc = new ReportDocument();
        ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptPFLoanLedger.rpt");
        DataTable MyDataTable = objPayRptMgr.GetPFLoanLedgerData(ddlMonth.SelectedValue.ToString()  , ddlFiscalYear.SelectedValue.ToString()  , "");
        ReportDoc.Load(ReportPath);
        ReportDoc.SetDataSource(MyDataTable);

        ReportDoc.SetParameterValue("pMonthName", Common.ReturnFullMonthName(ddlMonth.SelectedValue.ToString()));
        fileName = Session["USERID"].ToString() + "_" + "PF Loan Ledger" + ".pdf";
        this.ExPortReport(ReportDoc, fileName);

        StringBuilder sb = new StringBuilder();

        sb.Append("<script>");
        sb.Append("window.open('~/CrystalReports/Payroll/VirtualReport/" + fileName + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");
        //sb.Append("window.open('PayrollReportViewer.aspx', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit", sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
        //StringBuilder sb = new StringBuilder();
        //string strURL = "../../CrystalReports/Payroll/PFLoanLedgerViewer.aspx?params=" + ddlMonth.SelectedValue.ToString() + "," + DateTime.Today.Year.ToString() + "," + ddlFiscalYear.SelectedValue.ToString();
        //sb.Append("<script>");
        ////sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
        //sb.Append("window.open('" + strURL + "', '', '');");
        //sb.Append("</script>");
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
        //                         sb.ToString(), false);
        //ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
    }
    private void ExPortReport(ReportDocument ReportDoc, string rptPath)
    {

        CrystalDecisions.Shared.ExportOptions CrExportOptions;
        DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
        PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
        CrDiskFileDestinationOptions.DiskFileName = Server.MapPath("~/CrystalReports/Payroll/VirtualReport/" + rptPath);
        CrExportOptions = ReportDoc.ExportOptions;
        {
            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            CrExportOptions.FormatOptions = CrFormatTypeOptions;
        }
        ReportDoc.Export();
    }
}
