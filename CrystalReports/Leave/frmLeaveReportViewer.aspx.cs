using System;
using System.Data;
using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


public partial class frmLeaveReportViewer : System.Web.UI.Page
{
    private ReportDocument ReportDoc; 
    private string ReportPath = "";
    ReportManager rptManager = new ReportManager();
    DataTable MyDataTable = new DataTable();

    private string LogoPath = System.Web.Configuration.WebConfigurationManager.AppSettings["LogoPath"];

    protected void Page_Load(object sender, EventArgs e)
    {
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
        Page.ClientScript.RegisterForEventValidation(CRV.UniqueID);
        base.Render(writer);
    }

    private void ConfigureCrystalReports()
    {
       
        MyDataTable = new DataTable();
        ReportDoc = new ReportDocument();
      
        switch (Session["REPORTID"].ToString())
        {
            case "ELBR":
                ReportPath = Server.MapPath("~/CrystalReports/Leave/rptEmployeLeaveBalance.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetEmpLeaveBalance(Session["Flag"].ToString(), Session["PostingDivId"].ToString(), Session["EmpId"].ToString(), Session["FiscalYrId"].ToString(), Session["EmpTypeId"].ToString());
               
                CRV.Width = 10;
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Employee Leave Balance Report");
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRV.ReportSource = ReportDoc;
                break;
            case "EWLIR":
                ReportPath = Server.MapPath("~/CrystalReports/Leave/rptEmpWiseLeave.rpt");
               
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetEmpLeaveDetails(Session["EmpId"].ToString(), Session["PostingDivId"].ToString(), Session["FiscalYrId"].ToString(), Session["LeaveType"].ToString(), Session["SectorId"].ToString(), Session["DeptId"].ToString(), Session["EmpStatus"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString(), Session["EmpTypeId"].ToString());

                CRV.Width = 10;
                ReportDoc.SetDataSource(MyDataTable);
                 ReportDoc.SetParameterValue("pHeader", "Employee Wise Leave Information for the Year");
                ReportDoc.SetParameterValue("pFiscalYr", Session["FiscalYr"].ToString());
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRV.ReportSource = ReportDoc;
                break;
            case "EMWLR":
                ReportPath = Server.MapPath("~/CrystalReports/Leave/rptEmpMonthWiseLeave.rpt");
               
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetEmpMonthWiseLeave(Session["PostingDivId"].ToString(), Session["EmpId"].ToString(), Session["FiscalYrId"].ToString(), Session["LeaveType"].ToString(), Session["EmpStatus"].ToString(), Session["EmpTypeId"].ToString());

                CRV.Width = 10;
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Leave Balance");
                ReportDoc.SetParameterValue("pFiscalYr", Session["FiscalYr"].ToString());
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRV.ReportSource = ReportDoc;
                break;
            case "EILR":
                ReportPath = Server.MapPath("~/CrystalReports/Leave/rptEmpIndividualLeaveBalance.rpt");
               
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetEmpIndividualLeaveBalance(Session["PostingDivId"].ToString(), Session["EmpId"].ToString(), Session["FiscalYrId"].ToString(), Session["EmpStatus"].ToString(), Session["EmpTypeId"].ToString());              
                CRV.Width = 10;
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Employee Individual Leave Balance");
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRV.ReportSource = ReportDoc;
                break;
        }
    }

    public void PassParameterLeaveHeader(string ReportName)
    {
        ParameterFields pFields = new ParameterFields();
        ParameterField pfHeader = new ParameterField();   
        //Generate ParameterDiscreteValue
        ParameterDiscreteValue dvHeader = new ParameterDiscreteValue();
        //Adding ParameterDiscreteValue to ParameterField
        pfHeader.Name = "pHeader";
        dvHeader.Value = ReportName;
        pfHeader.CurrentValues.Add(dvHeader);
        //Adding Parameters to ParameterFields
        pFields.Add(pfHeader);      
        //Passing ParameterFields to CrystalReportViewer
        CRV.ParameterFieldInfo = pFields;
    }   
    
    public void PassParameterLeaveHeader( string ReportName,string FiscalYr)
    {
        ParameterFields pFields = new ParameterFields();        
        ParameterField pfHeader = new ParameterField();
        ParameterField pfFiscalYr = new ParameterField(); 
        //Generate ParameterDiscreteValue
         ParameterDiscreteValue dvHeader = new ParameterDiscreteValue();
         ParameterDiscreteValue dvFiscalYr = new ParameterDiscreteValue();
        //Adding ParameterDiscreteValue to ParameterField
        pfHeader.Name = "pHeader";
        dvHeader.Value = ReportName;
        pfHeader.CurrentValues.Add(dvHeader);

        pfFiscalYr.Name = "pFiscalYr";
        dvFiscalYr.Value = FiscalYr;
        pfFiscalYr.CurrentValues.Add(dvFiscalYr);
        //Adding Parameters to ParameterFields
         pFields.Add(pfHeader);
        pFields.Add(pfFiscalYr);
        //Passing ParameterFields to CrystalReportViewer
        CRV.ParameterFieldInfo = pFields;
    }

    public void PassParameterLeaveHeader(string ReportName, string FiscalYr, string LeaveTypeName)
    {
        ParameterFields pFields = new ParameterFields();
        ParameterField pfHeader = new ParameterField();
        ParameterField pfFiscalYr = new ParameterField();
        ParameterField pfLeaveTypeName = new ParameterField();
        //Generate ParameterDiscreteValue
        ParameterDiscreteValue dvHeader = new ParameterDiscreteValue();
        ParameterDiscreteValue dvFiscalYr = new ParameterDiscreteValue();
        ParameterDiscreteValue dvLeaveTypeName = new ParameterDiscreteValue();
        //Adding ParameterDiscreteValue to ParameterField
        pfHeader.Name = "pHeader";
        dvHeader.Value = ReportName;
        pfHeader.CurrentValues.Add(dvHeader);

        pfFiscalYr.Name = "pFiscalYr";
        dvFiscalYr.Value = FiscalYr;
        pfFiscalYr.CurrentValues.Add(dvFiscalYr);

        pfLeaveTypeName.Name = "pLeaveTypeName";
        dvLeaveTypeName.Value = LeaveTypeName;
        pfLeaveTypeName.CurrentValues.Add(dvLeaveTypeName);

        //Adding Parameters to ParameterFields
        pFields.Add(pfHeader);
        pFields.Add(pfFiscalYr);
        pFields.Add(pfLeaveTypeName);

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
}
