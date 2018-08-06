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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing.Printing;
using System.IO;
using System.Net;


public partial class CrystalReports_Payroll_PFLoanLedgerViewer : System.Web.UI.Page
{
    PayrollReportManager objPayRptMgr = new PayrollReportManager();

    private ReportDocument ReportDoc;
    private PrintDocument printDoc = new PrintDocument();
    private string ReportPath = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        string strParams = Request.QueryString["params"];
        string[] strVal = strParams.Split(',');
        this.GenerateReport(strVal[0], strVal[1], strVal[2]);
    }

    protected void GenerateReport(string strMonth, string strYear, string strFinYear)
    {
        ReportDoc = new ReportDocument();
        ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptPFLoanLedger.rpt");
        //this.PassParameter(Common.ReturnFullMonthName(strMonth));
        ReportDoc.Load(ReportPath);

        
        ReportDoc.SetDataSource(objPayRptMgr.GetPFLoanLedgerData(strMonth, strFinYear, "M"));

        ReportDoc.SetParameterValue("pMonthName",Common.ReturnFullMonthName(strMonth));
        CRV.ReportSource = ReportDoc;
    }

    public void PassParameter(string strMonthName)
    {
        ParameterFields pFields = new ParameterFields();        
        ParameterField pMonthName = new ParameterField();
       
        //Generate ParameterDiscreteValue        
        ParameterDiscreteValue MonthName = new ParameterDiscreteValue();
        ParameterDiscreteValue dvHeader = new ParameterDiscreteValue();

        //Adding ParameterDiscreteValue to ParameterField        
        pMonthName.Name = "pMonthName";
        MonthName.Value = strMonthName;
        pMonthName.CurrentValues.Add(MonthName);

        //Adding Parameters to ParameterFields 
        pFields.Add(pMonthName);
        
        //Passing ParameterFields to CrystalReportViewer
        CRV.ParameterFieldInfo = pFields;
    }

    protected void CRV_Unload(object sender, EventArgs e)
    {
        ReportDoc.Close();
        ReportDoc.Dispose();
        ReportDoc = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
    protected void CRV_BeforeRender(object source, CrystalDecisions.Web.HtmlReportRender.BeforeRenderEvent e)
    {
        Page.ClientScript.RegisterForEventValidation(CRV.UniqueID);
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        //string strParams = Request.QueryString["params"];
        //string[] strVal = strParams.Split(',');
        //DataTable dtLedger = objPayRptMgr.GetPFLoanLedgerDataForExcel(strVal[0], strVal[2], "M");
        //grExport.Visible = true;
        //grExport.DataSource = dtLedger;
        //grExport.DataBind();
        //foreach (GridViewRow gRow in grExport.Rows)
        //{
        //    gRow.Cells[19].Text = Common.ReturnFullMonthName(gRow.Cells[19].Text.Trim());
        //    if (Common.CheckNullString(gRow.Cells[4].Text) != "")
        //        gRow.Cells[4].Text = Common.SetDate(gRow.Cells[4].Text.Trim());
        //    if (Common.CheckNullString(gRow.Cells[11].Text) != "")
        //        gRow.Cells[11].Text = Common.SetDate(gRow.Cells[11].Text.Trim());
        //}
        //string attachment = "attachment; filename=PFLoanLedger.xls";
        //Response.ClearContent();
        //Response.AddHeader("content-disposition", attachment);
        //Response.ContentType = "application/ms-excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);
        //grExport.RenderControl(htw);
        //Response.Write(sw.ToString());
        //Response.Flush();
        //Response.End();
        //grExport.DataSource = null;
        //grExport.DataBind();
        //grExport.Visible = true;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

        // Confirms that an HtmlForm control is rendered for the
        // specified ASP.NET server control at run time.

    }
    
}
