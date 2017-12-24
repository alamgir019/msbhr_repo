using System;
using System.Data;
using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


public partial class frmTrainingReportViewer : System.Web.UI.Page
{
    private ReportDocument ReportDoc;
    private string ReportPath = "";
    private string LogoPath = System.Web.Configuration.WebConfigurationManager.AppSettings["LogoPath"];
    //test change
    ReportManager rptManager = new ReportManager();

    DataTable MyDataTable = new DataTable();
    
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
        Page.ClientScript.RegisterForEventValidation(CRVT.UniqueID);
        base.Render(writer);
    }

    private void ConfigureCrystalReports()
    {
        ReportDoc = new ReportDocument();
        switch (Session["REPORTID"].ToString())
        {
            case "ETD":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Training/rptEmployeeTrainingDetails.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = rptManager.GetEmployeeTrainingDetails(Session["TrainingID"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("PageHeader", "Employee Training Details");
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRVT.ReportSource = ReportDoc;
                    break;
                }
            case "TSI":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Training/rptTrainingScheduleDetails.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = rptManager.GetTrainingScheduleDetails(Session["TrainingID"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("PageHeader", "Training Schedule Details");
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRVT.ReportSource = ReportDoc;
                    break;
                }
            case "DTR":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Training/rptDeptWiseTrainingDetails.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = rptManager.GetDeptWiseTrainingDetails(Session["SalLocId"].ToString(), Session["TrainingID"].ToString(), Session["FundedBy"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("PageHeader", "Clinic/Department Wise Employee Training Details");
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRVT.ReportSource = ReportDoc;
                    break;
                }
            case "EER":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Training/rptEmpEligibleTrainDetails.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = rptManager.proc_Get_EmpEligibleTrainDetails(Session["SalLocId"].ToString(), Session["EmployeeName"].ToString(), Session["TrainingID"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("PageHeader", "Employee Training Eligible Details");
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRVT.ReportSource = ReportDoc;
                    break;
                }
            case "TBR":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Training/rptTrainingBudgetDetails.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = rptManager.GetTrainingBudgetDetails(Session["TrainingID"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("PageHeader", "Training Budget Details");
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRVT.ReportSource = ReportDoc;
                    break;
                }
            case "YPR":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Training/rptTrainingYearlyPlan.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = rptManager.GetTrainingYearlyPlan(Session["TrainingID"].ToString(), Session["ProjectID"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("PageHeader", "Training Plan and Budget");
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRVT.ReportSource = ReportDoc;
                    break;
                }
            case "TCR":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Training/rptTrainingCertificate.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = rptManager.GetCertificate(Session["TrainingID"].ToString(), Session["EmpId"].ToString());//Session["TrainingID"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("PageHeader", "Training Certificate");
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRVT.ReportSource = ReportDoc;
                    break;
                }
            case "TRR":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Training/rptTrainingRequisition.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = rptManager.GetTrainingRequisition(Session["TrainingID"].ToString(), Session["ProjectID"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("PageHeader", "Training Requisition Form for Project Staff");
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRVT.ReportSource = ReportDoc;
                    break;
                }
            case "INL":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Training/rptInvitationLetter.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = rptManager.GetInvitationLetter(Session["ScheduleID"].ToString(), Session["TrainingID"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("PageHeader", "INVITATION");
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    ReportDoc.SetParameterValue("MemoNo", Session["MemoNO"].ToString());
                    ReportDoc.SetParameterValue("fromTime", Session["FromTime"].ToString());
                    ReportDoc.SetParameterValue("toTime", Session["ToTime"].ToString());
                    ReportDoc.SetParameterValue("costProvided", Session["ProvCost"].ToString());
                    ReportDoc.SetParameterValue("informDate", Session["InformDate"].ToString());
                    ReportDoc.SetParameterValue("attendDate", Session["AttendDate"].ToString());
                    ReportDoc.SetParameterValue("attendTime", Session["Time"].ToString());
                    ReportDoc.SetParameterValue("dormitoryAddress", Session["Dormitory"].ToString());
                    CRVT.ReportSource = ReportDoc;
                    break;
                }
            case "PRL":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Training/rptParticipantList.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = rptManager.GetParticipantList(Session["ScheduleID"].ToString(), Session["TrainingID"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("PageHeader", "INVITATION");
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRVT.ReportSource = ReportDoc;
                    break;
                }
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

    public void PassParameterHeader(string ReportName,string FiscalYr)
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
