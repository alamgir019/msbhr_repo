using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;
using System.IO;


public partial class CrystalReports_KPI_KPIReportsViewer : System.Web.UI.Page
{
    private ReportDocument ReportDoc;
    private string ReportPath = "";
    ReportManager rptManager = new ReportManager();
    DataTable MyDataTable = new DataTable();
    DataSet myDs = new DataSet();

    string LogoPath = "";
    string directoryPath = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //directoryPath = System.Web.Configuration.WebConfigurationManager.AppSettings["LogoPath"];
        //DirectoryInfo info = new DirectoryInfo(Server.MapPath(directoryPath));
        LogoPath = System.Web.Configuration.WebConfigurationManager.AppSettings["LogoPath"];
        
        ConfigureCrystalReports();
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
        Page.ClientScript.RegisterForEventValidation(CRVT.UniqueID);
        base.Render(writer);
    }

    private void ConfigureCrystalReports()
    {

        ReportDoc = new ReportDocument();
        switch (Session["REPORTID"].ToString())
        {
            case "KPI":
                ReportPath = Server.MapPath("~/CrystalReports/KPI/rptEmpKPIReview.rpt");
                ReportDoc.Load(ReportPath);
                myDs = rptManager.Select_KPIReview(Session["EmpId"].ToString(), Session["VYear"].ToString(), Session["Quarter"].ToString(), Session["Group"].ToString());
                ReportDoc.SetDataSource(myDs.Tables[0]);
                ReportDoc.SetParameterValue("Year",         Session["VYear"].ToString());
                ReportDoc.SetParameterValue("E_Name",       myDs.Tables[1].Rows[0]["FullName"].ToString());
                ReportDoc.SetParameterValue("LinMgr",       myDs.Tables[1].Rows[0]["DesigName"].ToString()+", "+myDs.Tables[1].Rows[0]["SDeptName"].ToString());
                ReportDoc.SetParameterValue("Quarter",      Session["QuarterN"].ToString());
                ReportDoc.SetParameterValue("Department",   myDs.Tables[1].Rows[0]["DeptName"].ToString());
                ReportDoc.SetParameterValue("LinMgr_Name",  myDs.Tables[1].Rows[0]["SFullName"].ToString());
                ReportDoc.SetParameterValue("Position",     "");
                ReportDoc.SetParameterValue("Location",     myDs.Tables[1].Rows[0]["SalLocName"].ToString());
                ReportDoc.SetParameterValue("AVGScore",     myDs.Tables[1].Rows[0]["AvgScore"].ToString());
                ReportDoc.SetParameterValue("OVScore",      myDs.Tables[1].Rows[0]["OVScore"].ToString());
                ReportDoc.SetParameterValue("OVRating",     myDs.Tables[1].Rows[0]["Rating"].ToString());
                ReportDoc.SetParameterValue("ComLogo",      LogoPath);

                CRVT.ReportSource = ReportDoc;
                break;
        }
    }

    public void PassParameterHeader(string ReportName)
    {
        ParameterFields pFields = new ParameterFields();
        ParameterField pfHeader = new ParameterField();
        ParameterDiscreteValue dvHeader = new ParameterDiscreteValue();

        pfHeader.Name = "pHeader";
        dvHeader.Value = ReportName;
        pfHeader.CurrentValues.Add(dvHeader);

        pFields.Add(pfHeader);

        CRVT.ParameterFieldInfo = pFields;
    }

    public void PassParameterHeader(string ReportName, string FiscalYr)
    {
        ParameterFields pFields = new ParameterFields();
        ParameterField pfHeader = new ParameterField();
        ParameterField pfFiscalYr = new ParameterField();

        ParameterDiscreteValue dvHeader = new ParameterDiscreteValue();
        ParameterDiscreteValue dvFiscalYr = new ParameterDiscreteValue();

        pfHeader.Name = "pHeader";
        dvHeader.Value = ReportName;
        pfHeader.CurrentValues.Add(dvHeader);

        pfFiscalYr.Name = "pFiscalYr";
        dvFiscalYr.Value = FiscalYr;
        pfFiscalYr.CurrentValues.Add(dvFiscalYr);

        pFields.Add(pfHeader);
        pFields.Add(pfFiscalYr);

        CRVT.ParameterFieldInfo = pFields;
    }


    public void PassParameterHeader(string ReportName, string FromDate, string ToDate)
    {
        ParameterFields pFields = new ParameterFields();
        ParameterField pfHeader = new ParameterField();
        ParameterField pfFromDate = new ParameterField();
        ParameterField pfToDate = new ParameterField();

        ParameterDiscreteValue dvHeader = new ParameterDiscreteValue();
        ParameterDiscreteValue dvFromDate = new ParameterDiscreteValue();
        ParameterDiscreteValue dvToDate = new ParameterDiscreteValue();

        pfHeader.Name = "pHeader";
        dvHeader.Value = ReportName;
        pfHeader.CurrentValues.Add(dvHeader);

        pfFromDate.Name = "pFromDate";
        dvFromDate.Value = FromDate;
        pfFromDate.CurrentValues.Add(dvFromDate);

        pfToDate.Name = "pToDate";
        dvToDate.Value = ToDate;
        pfToDate.CurrentValues.Add(dvToDate);

        pFields.Add(pfHeader);
        pFields.Add(pfFromDate);
        pFields.Add(pfToDate);

        CRVT.ParameterFieldInfo = pFields;
    }

    protected void CRVT_Unload(object sender, EventArgs e)
    {
        ReportDoc.Close();
        ReportDoc.Dispose();
        ReportDoc = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
    protected void CRVT_BeforeRender(object source, CrystalDecisions.Web.HtmlReportRender.BeforeRenderEvent e)
    {
        Page.ClientScript.RegisterForEventValidation(CRVT.UniqueID);
    }
}
