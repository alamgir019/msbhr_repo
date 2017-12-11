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
public partial class CrystalReports_Payroll_GratuityReportViewer : System.Web.UI.Page
{
    Payroll_GratuityLedgerManager objGrMgr = new Payroll_GratuityLedgerManager();

    private ReportDocument ReportDoc;
    private PrintDocument printDoc = new PrintDocument();
    private string ReportPath = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string strParams = Request.QueryString["params"];
        string[] strVal = strParams.Split(',');
        this.GenerateReport(strVal[0], strVal[1], strVal[2]);
    }

    protected void GenerateReport(string strMonth,string strFinYear,string strEmpID)
    {
        ReportDoc = new ReportDocument();
        ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptGratuitySlip.rpt");
        this.PassParameter(Common.ReturnFullMonthName(strMonth));
        ReportDoc.Load(ReportPath);
        ReportDoc.SetDataSource(objGrMgr.GetGrPaymentList(strMonth, strFinYear, strEmpID));
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
   
    public override void VerifyRenderingInServerForm(Control control)
    {

        // Confirms that an HtmlForm control is rendered for the
        // specified ASP.NET server control at run time.

    }
}
