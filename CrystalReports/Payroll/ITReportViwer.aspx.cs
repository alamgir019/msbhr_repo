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

public partial class CrystalReports_Payroll_ITReportViwer : System.Web.UI.Page
{
    private ReportDocument ReportDoc;
    private PrintDocument printDoc = new PrintDocument();
    private string ReportPath = "";
    Payroll_ITDepositRecords objITMgr = new Payroll_ITDepositRecords();
    DataTable MyDataTable;
    protected void Page_Load(object sender, EventArgs e)
    {
        string strParams = Request.QueryString["params"];
        string[] strVal = strParams.Split(',');

        ConfigureCrystalReports(strVal[0], strVal[1], strVal[2]);
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        if (null != MyDataTable)
            MyDataTable.Dispose();
        if (ReportDoc != null)
        {
            ReportDoc.Close();
            ReportDoc.Dispose();
        }
    }

    protected override void Render(HtmlTextWriter writer)
    {
        Page.ClientScript.RegisterForEventValidation(CRV.UniqueID);
        base.Render(writer);
    }

    private void ConfigureCrystalReports(string strEmpID, string strMonth, string strFiscal)
    {
        MyDataTable = new DataTable();
        ReportDoc = new ReportDocument();
        ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptIncomeTax.rpt");
        ReportDoc.Load(ReportPath);
        MyDataTable = objITMgr.GetITCalculationReportData(strEmpID, strMonth, strFiscal);
        //this.PassParamMonthFinYr(Session["MonthValue"].ToString(), Session["FiscalYrValue"].ToString());
        ReportDoc.SetDataSource(MyDataTable);
        CRV.ReportSource = ReportDoc;
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
}