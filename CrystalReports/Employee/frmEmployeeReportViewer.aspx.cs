using System;
using System.Data;
using System.IO;
using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Configuration;  

public partial class frmEmployeeReportViewer : System.Web.UI.Page
{
    private ReportDocument ReportDoc;

    private string ReportPath = "";
    ReportManager rptManager = new ReportManager();
    PayrollReportManager objPayRptMgr = new PayrollReportManager();
    DataTable MyDataTable = new DataTable();
    dsTimeSheet ds = new dsTimeSheet();

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
        Page.ClientScript.RegisterForEventValidation(CRVT.UniqueID);
        base.Render(writer);
    }

    private decimal GetZeroIfNull(string strData)
    {
        if (string.IsNullOrEmpty(strData) == true)
        {
            return 0;
        }
        else
        {
            return Convert.ToDecimal(strData);
        }
    }

    private void ConfigureCrystalReports()
    {
        ReportDoc = new ReportDocument();

        switch (Session["REPORTID"].ToString())
        {
            #region Employee
            case "EL":
                if (Session["Basic"].ToString() == "B")
                    ReportPath = Server.MapPath("~/CrystalReports/Employee/rptEmployeeList.rpt");
                else
                    ReportPath = Server.MapPath("~/CrystalReports/Employee/rptEmployeeListWithoutBasic.rpt");

                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetEmpoyeeList(Session["GradeId"].ToString(), Session["EmpId"].ToString(),Session["FullName"].ToString(),
                    Session["Gender"].ToString(),Session["SectorId"].ToString(), Session["DeptId"].ToString(),Session["UnitId"].ToString(),
                    Session["PostingDivId"].ToString(),Session["PostingDistId"].ToString(), Session["DesigId"].ToString(), 
                    Session["PosByFuncId"].ToString(), Session["ReligionId"].ToString(),Session["EmpType"].ToString(),Session["TNTPosition"].ToString(),
                    Session["FromDate"].ToString(), Session["ToDate"].ToString(), Session["EmpStatus"].ToString(), Session["Basic"].ToString());
                ReportDoc.SetDataSource(MyDataTable); 
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("ComLogo", LogoPath);                           
                ReportDoc.SetParameterValue("pHeader", "Employee List");
                CRVT.ReportSource = ReportDoc;
                //string fileName = "D://Development//VS-2010//MSBHR//CrystalReports//Employee//Employee.pdf";
                string fileName = ConfigurationManager.AppSettings["EmployeePrintPath"];
                ReportDoc.ExportToDisk(ExportFormatType.PortableDocFormat, fileName);
                break;

//                ReportDoc.Load(ReportPath);
//                MyDataTable = rptManager.GetEmpoyeeList(Session["GradeId"].ToString(), Session["EmpId"].ToString(),Session["FullName"].ToString(),
//                    Session["Gender"].ToString(),Session["SectorId"].ToString(), Session["DeptId"].ToString(),Session["UnitId"].ToString(),
//                    Session["PostingDivId"].ToString(),Session["PostingDistId"].ToString(), Session["DesigId"].ToString(), 
//                    Session["PosByFuncId"].ToString(), Session["ReligionId"].ToString(),Session["EmpType"].ToString(),Session["TNTPosition"].ToString(),
//                    Session["FromDate"].ToString(), Session["ToDate"].ToString(), Session["EmpStatus"].ToString(), Session["Basic"].ToString());
//                ReportDoc.SetDataSource(MyDataTable);              

////                System.Windows.Controls.PrintDialog dialogue = new System.Windows.Controls.PrintDialog();
 
//                PrinterSettings printerSettings = new PrinterSettings();
//                PrintDialog printDialog = new PrintDialog();
//                printDialog.PrinterSettings = printerSettings;
//                printDialog.AllowPrintToFile = false;
//                printDialog.AllowSomePages = true;
//                printDialog.UseEXDialog = true;
//                printDialog.ShowDialog();
                

                

//                //DialogResult dr = printDialog.ShowDialog();
//                //if (printDialog.ShowDialog() == DialogResult.OK)

//                if (printDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
//                {
//                    // Do something
//                }

//                //this.ReportDoc.PrintOptions.PrinterName = printerSettings.PrinterName;
//                //this.ReportDoc.PrintToPrinter(printerSettings.Copies, false, 0, 0);





//                ReportDoc.SetParameterValue("ComLogo", LogoPath);                           
//                ReportDoc.SetParameterValue("pHeader", "Employee List");
//                CRVT.ReportSource = ReportDoc;

//                break;
            case "ELNB":
                ReportPath = Server.MapPath("~/CrystalReports/Employee/rptEmpoyeeNGOforBureau.rpt");
                ReportDoc.Load(ReportPath);
               
                MyDataTable = rptManager.GetEmpoyeeNGOforBureau(Session["SectorId"].ToString(), Session["DeptId"].ToString(),
                                                        Session["PostingDivId"].ToString(),
                                                       Session["PostingDistId"].ToString(), Session["IsActive"].ToString(),Session["EmpTypeID"].ToString());
              
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Employee List for NGO Bureau");
                ReportDoc.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRVT.ReportSource = ReportDoc;

                //Report to be landscape
                // ReportDoc.PrintOptions.PrinterName = printerName;
                ReportDoc.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
               // ReportDoc.PrintToPrinter(0, false, 0, 1);
               // System.Drawing.Printing.PrinterSettings printersettings = new System.Drawing.Printing.PrinterSettings();
               //printersettings.DefaultPageSettings.Landscape = true;
               // System.Drawing.Printing.PageSettings pageSettings = new System.Drawing.Printing.PageSettings();
               //  ReportDoc.PrintToPrinter(printersettings, pageSettings, false);
               //End of Report                                   
                break;

            case "GWEL":
                ReportPath = Server.MapPath("~/CrystalReports/Employee/rptGroupWiseEmpList.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetGroupWiseEmpList(Session["RPRTID"].ToString(), Session["GROUPID"].ToString(), Session["EmpTypeID"].ToString());

                CRVT.Width = 10;
                ReportDoc.SetDataSource(MyDataTable);

                if (Session["RPRTID"].ToString() == "1")
                    ReportDoc.SetParameterValue("pHeader", "Grade Wise Employee List");                    
                if (Session["RPRTID"].ToString() == "2")
                    ReportDoc.SetParameterValue("pHeader", "Clinic Wise Employee List");   
                if (Session["RPRTID"].ToString() == "3")
                    ReportDoc.SetParameterValue("pHeader", "Organization Wise Employee List");   
                if (Session["RPRTID"].ToString() == "4")
                    ReportDoc.SetParameterValue("pHeader", "Project Wise Employee List");   
                if (Session["RPRTID"].ToString() == "9")
                   ReportDoc.SetParameterValue("pHeader", "Designation Wise Employee List");   
                if (Session["RPRTID"].ToString() == "10")
                    ReportDoc.SetParameterValue("pHeader", "Religion Wise Employee List");   
                if (Session["RPRTID"].ToString() == "11")
                    ReportDoc.SetParameterValue("pHeader", "Department Wise Employee List");
                if (Session["RPRTID"].ToString() == "8")
                    ReportDoc.SetParameterValue("pHeader", "Tech. /Non Tech. Wise Employee List"); 
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRVT.ReportSource = ReportDoc;
                break;
            case "BAI":
                ReportPath = Server.MapPath("~/CrystalReports/Employee/rptBankAccountInfo.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetBankAccountInfo(Session["EmpID"].ToString(), Session["SalLocId"].ToString(), Session["PostingDivId"].ToString(), Session["BankCode"].ToString(), Session["IsActive"].ToString(), Session["EmpType"].ToString());                
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Bank Account Information");
                CRVT.Width = 10;
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRVT.ReportSource = ReportDoc;                
                break;
            case "SSL":

                ReportPath = Server.MapPath("~/CrystalReports/Employee/rptSeparationStaffList.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetSeparationStaffList(Session["GradeId"].ToString(), Session["EmpId"].ToString(), Session["DeptId"].ToString(), Session["PostingDivId"].ToString(), Session["SeparationType"].ToString(), Session["EmpTypeID"].ToString(), Session["Rehireable"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString(), Session["ServiceLengthFrom"].ToString(), Session["ServiceLengthTo"].ToString(), Session["IsActive"].ToString());                
                
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Separation Staff List");
                //ReportDoc.SetParameterValue("pFromDate", Session["FromDate"].ToString());
               // ReportDoc.SetParameterValue("pToDate", Session["ToDate"].ToString());  
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRVT.ReportSource = ReportDoc;
                break;
            case "EECL":
                //Report no 2 : LEAVE Report
                ReportPath = Server.MapPath("~/CrystalReports/Employee/rptEmpEmergencyContactList.rpt");
              //  Label1.Text = ReportPath;
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetEmpEmergencyContactList(Session["EmpId"].ToString(), Session["FullName"].ToString(), Session["DesigId"].ToString(), Session["DeptId"].ToString(), Session["SectorId"].ToString(),Session["EmpTypeID"].ToString());
                CRVT.Width = 10;
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Employee Emergency Contact List ");
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRVT.ReportSource = ReportDoc;
                break;
            case "EEI":              
                ReportPath = Server.MapPath("~/CrystalReports/Employee/rptEmpExperianceInfo.rpt");             
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetEmpExperienceInfo(Session["EmpId"].ToString(), Session["SectorId"].ToString(), Session["SalLocId"].ToString(), Session["EmpTypeID"].ToString());
              
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Employee Experience Information ");
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRVT.ReportSource = ReportDoc;
                break;
            case "EI": 
                ReportPath = Server.MapPath("~/CrystalReports/Employee/rptEmployeeInfo.rpt");
               // Label1.Text = ReportPath;
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetEmployeeInfo(Session["EmpId"].ToString(), Session["PostingDistId"].ToString(), Session["BloodGroupId"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString(), Session["IsActive"].ToString(), Session["EmpTypeID"].ToString());                
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Employee Information");
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRVT.ReportSource = ReportDoc;
                break;
            case "EWI":

                //Session["FullName"] = txtEmpName.Text.Trim();
                //    Session["SectorId"] = ddlSector.SelectedValue;
                //    Session["DesigId"] = ddlDesignation.SelectedValue;
                //    Session["DeptId"] = ddlDepartment.SelectedValue;
                //Report no 2 : LEAVE Report
                ReportPath = Server.MapPath("~/CrystalReports/Employee/rptEmpWitnessInfo.rpt");
               // Label1.Text = ReportPath;
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetEmpWitnessInfo(Session["EmpId"].ToString(), Session["FullName"].ToString(), Session["SectorId"].ToString(), Session["DesigId"].ToString(), Session["DeptId"].ToString(), Session["EmpTypeID"].ToString());

                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Employee Witness Information ");
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRVT.ReportSource = ReportDoc;
                break;
            case "ELWS":
                ReportPath = Server.MapPath("~/CrystalReports/Employee/rptEmployeeListWithSupervisor.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetEmpoyeeListWithSupervisor(Session["FullName"].ToString(), Session["SectorId"].ToString(), Session["DeptId"].ToString(), "", Session["EmpTypeID"].ToString());
                
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Employee List With Supervisor ");
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRVT.ReportSource = ReportDoc;
                break;
            case "ELWA":               
                ReportPath = Server.MapPath("~/CrystalReports/Employee/rptEmployeeListWithAddress.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetEmployeeListWithAddress(Session["EmpId"].ToString(), Session["HomeDistId"].ToString(), Session["BloodGroupId"].ToString(), Session["IsActive"].ToString(), Session["EmpType"].ToString());               
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Employee List With Address ");
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRVT.ReportSource = ReportDoc;
                break;
            case "EDA":
                ReportPath = Server.MapPath("~/CrystalReports/Employee/rptEmpDisciplinaryAction.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetEmpDisciplinaryAction(Session["EmpID"].ToString(), Session["SalLocId"].ToString(), Session["PostingDivId"].ToString(), Session["ReasonOfAction"].ToString(), Session["ActionType"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString(),Session["EmpTypeID"].ToString());// Session["VMonth"].ToString(), Session["VYear"].ToString(),
               
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Employees Disciplinary Action ");
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRVT.ReportSource = ReportDoc;
                break;
            case "CL":
                ReportPath = Server.MapPath("~/CrystalReports/Employee/rptConfirmationList.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetConfirmationList(Session["DeptId"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString(), Session["EmpTypeID"].ToString());
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Confirmation List ");
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRVT.ReportSource = ReportDoc;
                break;
            case "EEID":
                ReportPath = Server.MapPath("~/CrystalReports/Employee/rptEmpEduInfoDetails.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetEmpEduInfoDetails(Session["EmpID"].ToString(), Session["DeptId"].ToString(), Session["SalLocId"].ToString(), Session["EmpTypeID"].ToString());
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Employee Education Informatin(Details) ");
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRVT.ReportSource = ReportDoc;
                break;
            case "EEIIB":
                ReportPath = Server.MapPath("~/CrystalReports/Employee/rptEmpEduInfoInBrief.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetEmpEduInfoInBrief(Session["EmpID"].ToString(), Session["DeptId"].ToString(), Session["SalLocId"].ToString(), Session["EmpTypeID"].ToString());
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Employee Education Informatin(In Brief)");
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRVT.ReportSource = ReportDoc;
                break;
            case "ENI":
                ReportPath = Server.MapPath("~/CrystalReports/Employee/rptEmpNomineeInfo.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetEmpNomineeInfo(Session["EmpID"].ToString(), Session["DeptId"].ToString(), Session["SalLocId"].ToString(), Session["EmpTypeID"].ToString());
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Employee Nominee Informatin");
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRVT.ReportSource = ReportDoc;
                break;
            case "EJF":
                ReportPath = Server.MapPath("~/CrystalReports/Employee/rptEmpJoiningInfo.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetEmpJoiningInfo(Session["DeptId"].ToString(), Session["SectorId"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString(), Session["EmpTypeID"].ToString());
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Employee Joining Info");
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRVT.ReportSource = ReportDoc;
                break;
            case "ETDYI":
                ReportPath = Server.MapPath("~/CrystalReports/Employee/rptEmpTDYInfo.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetEmpTDYInfo(Session["EmpID"].ToString(),Session["DeptId"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString());
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Employee TDY Information");
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRVT.ReportSource = ReportDoc;
                break;
            case "1":
                ReportPath = Server.MapPath("~/CrystalReports/Employee/rptEmpServiceLength.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetEmpServiceLengthOfSeparatedEmp( Session["EmpID"].ToString(),Session["PostingDivId"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString(),Session["EmpTypeID"].ToString());//, Session["IsActive"].ToString()
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Service Length Of Separated Employee");
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRVT.ReportSource = ReportDoc;
                break;
            case "2":
                ReportPath = Server.MapPath("~/CrystalReports/Employee/rptEmpServiceLength.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetEmpServiceLengthAsPerJoining( Session["EmpID"].ToString(), Session["PostingDivId"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString(),Session["EmpTypeID"].ToString());//, Session["IsActive"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString()
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Employee Service Length and Retirement as per Joining");
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRVT.ReportSource = ReportDoc;
                break;
            case "3":
                ReportPath = Server.MapPath("~/CrystalReports/Employee/rptEmpServiceLength.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetEmpServiceLengthAsDOB(Session["EmpID"].ToString(), Session["PostingDivId"].ToString(), Session["ServiceLengthFrom"].ToString(), Session["ServiceLengthTo"].ToString(), Session["EmpTypeID"].ToString());
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Employee Service and Retirement as per Date Of Birth ");
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRVT.ReportSource = ReportDoc;
                break;
            case "ER":
                ReportPath = Server.MapPath("~/CrystalReports/Employee/rptEmpResume.rpt");
                ReportDoc.Load(ReportPath);
                DataSet dsEmpResume = new DataSet();
                dsEmpResume = rptManager.GetEmpResume(dsEmpResume, Session["EmpID"].ToString());             
                //Add Image from Folder
                //AddImageColumn(dsEmpResume.Tables[0], "EmpPic");

                //for (int index = 0; index < dsEmpResume.Tables[0].Rows.Count; index++)
                //{
                //    if (dsEmpResume.Tables[0].Rows[index]["EmpPicLoc"].ToString() != "")
                //    {
                //        LoadImage(dsEmpResume.Tables[0].Rows[index], "EmpPic", "D:/SCB/SaveTheChildren/EmpImage/" + Session["EmpID"].ToString().Trim() + ".jpg");
                //    }
                //    else
                //    {
                //        LoadImage(dsEmpResume.Tables[0].Rows[index], "EmpPic", "D:/SCB/SaveTheChildren/EmpImage/NoImage.jpg");
                //    }
                //}
               
                ReportDoc.SetDataSource(dsEmpResume.Tables[0]);
                //Previous Experience
                ReportDoc.OpenSubreport("rptSubEmpPreviousExpP2.rpt").SetDataSource(dsEmpResume.Tables[1]);
                //EmergencyContact
                ReportDoc.OpenSubreport("rptSubEmpResumeEmergContact.rpt").SetDataSource(dsEmpResume.Tables[2]);
                //EmpResumeAddress 
                ReportDoc.OpenSubreport("rptSubResumeEmpAddress.rpt").SetDataSource(dsEmpResume.Tables[3]);
                ReportDoc.SetParameterValue("pHeader", "Employee Resume");
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRVT.ReportSource = ReportDoc;
                break;
            //case "DP":
            //    //Report no 2 : LEAVE Report
            //    ReportPath = Server.MapPath("~/CrystalReports/Employee/rptDevelopmentPlanOrPriorityList.rpt");
            //    // Label1.Text = ReportPath;
            //    ReportDoc.Load(ReportPath);
            //    MyDataTable = rptManager.GetDevelopmentPlan(Session["EmpId"].ToString(), Session["GradeId"].ToString(), Session["LAreaId"].ToString(), Session["DeptId"].ToString(), Session["FiscalYrId"].ToString());
            //   // this.PassParameterHeader("Development plan( Priority List)", Session["FiscalYr"].ToString());
     
            //    ReportDoc.SetDataSource(MyDataTable);
            //    ReportDoc.SetParameterValue("pHeader", "Development plan( Priority List)");
            //    ReportDoc.SetParameterValue("pFiscalYr", Session["FiscalYr"].ToString());
             
            //    CRVT.ReportSource = ReportDoc;
            //    break;

            case "ESI":
                ReportPath = Server.MapPath("~/CrystalReports/Employee/rptEmpSalaryInfo.rpt");             
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetEmpSalaryInfo(Session["EmpId"].ToString(), Session["GradeId"].ToString(), Session["ClinicId"].ToString(), Session["DeptId"].ToString(), Session["EmpTypeID"].ToString());
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Employee Salary Information");
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRVT.ReportSource = ReportDoc;
                break;
            case "LSAE":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Employee/rptLongServiceAwardeeEmp.rpt");
                    // Label1.Text = ReportPath;
                    ReportDoc.Load(ReportPath);
                    MyDataTable = rptManager.GetLongServiceAwardeeEmp(Session["VMonth"].ToString(), Session["VYearName"].ToString(), Session["EmpStatus"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("pHeader", " Long Service Awardee Employee List on");
                    ReportDoc.SetParameterValue("pMonth", Session["VMonthName"].ToString());
                    ReportDoc.SetParameterValue("pYear", Session["VYearName"].ToString());
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRVT.ReportSource = ReportDoc;

                    break;
                }
            //case "IR":
            //    //Report no 2 : LEAVE Report
            //    ReportPath = Server.MapPath("~/CrystalReports/Employee/rptIncrementReport.rpt");
            //    //  Label1.Text = ReportPath;
            //    ReportDoc.Load(ReportPath);
            //    MyDataTable = rptManager.GetIncrementReport(Session["EmpID"].ToString(), Session["SalLocId"].ToString(), Session["PostingDivId"].ToString(), Session["FiscalYrId"].ToString());
            //    // PassParameterHeader("Increment Report",Session["FiscalYr"].ToString());
            //    // PassParameterHeader("Increment Report", Session["SalLoc"].ToString(), Session["PostingDiv"].ToString(), Session["FiscalYr"].ToString());
            //    CRVT.Width = 10;
            //    ReportDoc.SetDataSource(MyDataTable);
            //    ReportDoc.SetParameterValue("pHeader", "Increment Report");
            //    //ReportDoc.SetParameterValue("pSalLoc", Session["SalLoc"].ToString());
            //    //ReportDoc.SetParameterValue("pPostingDiv", Session["PostingDiv"].ToString());
            //    //ReportDoc.SetParameterValue("pFiscalYr", Session["FiscalYr"].ToString());
            //    CRVT.ReportSource = ReportDoc;
            //    break;

            //case "OTC":
            //    //Report no 2 : LEAVE Report
            //    ReportPath = Server.MapPath("~/CrystalReports/Employee/rptOTCalculation.rpt");
            //    //  Label1.Text = ReportPath;
            //    ReportDoc.Load(ReportPath);
            //    MyDataTable = rptManager.GetOTCalculation(Session["EmpID"].ToString(), Session["SalLocId"].ToString(), Session["PostingDivId"].ToString(), Session["VMonth"].ToString(),Session["VYear"].ToString());
            //    // PassParameterHeader("Increment Report",Session["FiscalYr"].ToString());
            //   // PassParameterHeader("OT Calculation For the Month Of ",Session["MonthName"].ToString());               
            //    CRVT.Width = 10;
            //    ReportDoc.SetDataSource(MyDataTable);
            //    ReportDoc.SetParameterValue("pHeader", "OT Calculation For the Month Of ");
            //    ReportDoc.SetParameterValue("pFiscalYr", Session["MonthName"].ToString());
            //    CRVT.ReportSource = ReportDoc;
            //    break;
            #endregion


            case "EPHR":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptEmpPromotionHistory.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_EmpPromotionHistory(Session["SalDiv"].ToString(),
                                                                           Session["VMonth"].ToString(),
                                                                           Session["VYear"].ToString(),
                                                                           Session["Grade"].ToString(),
                                                                           Session["Desig"].ToString(),
                                                                           Session["FDate"].ToString(),
                                                                           Session["TDate"].ToString(),
                                                                           Session["EmpID"].ToString());

                    //MyDataTable = objPayRptMgr.Get_Rpt_AddRequirementAllow(Session["VMonth"].ToString(), Session["VYear"].ToString());
                    //DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", " Employee Promotion History ");
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRVT.ReportSource = ReportDoc;
                    break;
                }
            case "ETR":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptEmpTransfer.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_EmpTransferReport(Session["SalDiv"].ToString(),
                                                                           Session["VMonth"].ToString(),
                                                                           Session["VYear"].ToString(),
                                                                           Session["Grade"].ToString(),
                                                                           Session["Desig"].ToString(),
                                                                           Session["FDate"].ToString(),
                                                                           Session["TDate"].ToString(),
                                                                           Session["EmpID"].ToString());

                    //MyDataTable = objPayRptMgr.Get_Rpt_AddRequirementAllow(Session["VMonth"].ToString(), Session["VYear"].ToString());
                    //DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Employee Transfer Report");
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRVT.ReportSource = ReportDoc;
                    break;
                }
            case "ECSR":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptEmpChangeStatus.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_EmpChangeStatusReport(Session["SalDiv"].ToString(),
                                                                           Session["VMonth"].ToString(),
                                                                           Session["VYear"].ToString(),
                                                                           Session["Grade"].ToString(),
                                                                           Session["Desig"].ToString(),
                                                                           Session["FDate"].ToString(),
                                                                           Session["TDate"].ToString(),
                                                                           Session["EmpID"].ToString());

                    //MyDataTable = objPayRptMgr.Get_Rpt_AddRequirementAllow(Session["VMonth"].ToString(), Session["VYear"].ToString());
                    //DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Employee Change Status Report");
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRVT.ReportSource = ReportDoc;
                    break;
                }
            case "ESCHR":
                {

                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptEmpSalChanHistory.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_EmpSalChanHistoryRpt(Session["FDate"].ToString(),
                                                                               Session["TDate"].ToString(),
                                                                               Session["EmpID"].ToString(),
                                                                               Session["Sector"].ToString(),
                                                                               Session["Dept"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Employee Salary Change History " + Session["FDate"].ToString() + " To " + Session["TDate"].ToString());
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    // ReportDoc.SetParameterValue("SHEAD", Session["SalHeadText"].ToString());
                    CRVT.ReportSource = ReportDoc;
                    break;
                }

            #region System Rport
            //User Security Audit
            case "UHR":
                ReportPath = Server.MapPath("~/CrystalReports/Employee/rptUserSecurityAudit.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.Select_User_InOutHistory(Session["EmpId"].ToString(),Session["FromDate"].ToString(), Session["ToDate"].ToString());

                this.PassParameterHeader("User Security Audit Report",Session["FromDate"].ToString(), Session["ToDate"].ToString());

                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRVT.ReportSource = ReportDoc;
                break;
             #endregion


        }
    }

    //Image Display
    private void AddImageColumn(DataTable objDataTable, string strFieldName)
    {
        try
        {
            DataColumn objDataColumn = new DataColumn(strFieldName, Type.GetType("System.Byte[]"));
            objDataTable.Columns.Add(objDataColumn);
        }
        catch (Exception ex)
        {
            Response.Write("<fontlor=red>" + ex.Message + "</font>");
        }
    }

    //Load Image
    private void LoadImage(DataRow objDataRow, string strImageField, string FilePath)
    {
        try
        {
            FileStream fs = new FileStream(FilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] Image = new byte[fs.Length];
            fs.Read(Image, 0, Convert.ToInt32(fs.Length));
            fs.Close();
            objDataRow[strImageField] = Image;
        }
        catch (Exception ex)
        {
            Response.Write("<fontlor=red>" + ex.Message + "</font>");
        }
    }

    //End image
    public void PassParameterHeader(string ReportName, string FiscalYr)
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
        CRVT.ParameterFieldInfo = pFields;

    }
    
    public void PassParameterHeader(string ReportName, string SalLoc, string PostingDiv, string FiscalYr)
    {
        ParameterFields pFields = new ParameterFields();
        ParameterField pfHeader = new ParameterField();
        ParameterField pfSalLoc = new ParameterField();
        ParameterField pfPostingDiv = new ParameterField();
        ParameterField pfFiscalYr = new ParameterField();

        //Generate ParameterDiscreteValue
        ParameterDiscreteValue dvHeader = new ParameterDiscreteValue();
        ParameterDiscreteValue dvSalLoc = new ParameterDiscreteValue();
        ParameterDiscreteValue dvPostingDiv = new ParameterDiscreteValue();
        ParameterDiscreteValue dvFiscalYr = new ParameterDiscreteValue();

        //Adding ParameterDiscreteValue to ParameterField
        pfHeader.Name = "pHeader";
        dvHeader.Value = ReportName;
        pfHeader.CurrentValues.Add(dvHeader);

        pfSalLoc.Name = "pSalLoc";
        dvSalLoc.Value = SalLoc;
        pfSalLoc.CurrentValues.Add(dvSalLoc);

        pfPostingDiv.Name = "pPostingDiv";
        dvPostingDiv.Value = PostingDiv;
        pfPostingDiv.CurrentValues.Add(dvPostingDiv);

        pfFiscalYr.Name = "pFiscalYr";
        dvFiscalYr.Value = FiscalYr;
        pfFiscalYr.CurrentValues.Add(dvFiscalYr);

        //Adding Parameters to ParameterFields
        pFields.Add(pfHeader);
        pFields.Add(pfSalLoc);
        pFields.Add(pfPostingDiv);
        pFields.Add(pfFiscalYr);

        //Passing ParameterFields to CrystalReportViewer
        CRVT.ParameterFieldInfo = pFields;

    }

    //
    public void PassParameterHeader(string ReportName)
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
        CRVT.ParameterFieldInfo = pFields;

    }
    public void PassParameterHeader(string ReportName, string FromDate, string ToDate)
    {
        ParameterFields pFields = new ParameterFields();

        ParameterField pfHeader = new ParameterField();
        ParameterField pfFromDate = new ParameterField();
        ParameterField pfToDate = new ParameterField();


        //Generate ParameterDiscreteValue
        ParameterDiscreteValue dvHeader = new ParameterDiscreteValue();
        ParameterDiscreteValue dvFromDate = new ParameterDiscreteValue();
        ParameterDiscreteValue dvToDate = new ParameterDiscreteValue();

        //Adding ParameterDiscreteValue to ParameterField
        pfHeader.Name = "pHeader";
        dvHeader.Value = ReportName;
        pfHeader.CurrentValues.Add(dvHeader);

        pfFromDate.Name = "pFromDate";
        dvFromDate.Value = FromDate;
        pfFromDate.CurrentValues.Add(dvFromDate);



        pfToDate.Name = "pToDate";
        dvToDate.Value = ToDate;
        pfToDate.CurrentValues.Add(dvToDate);




        //Adding Parameters to ParameterFields
        pFields.Add(pfHeader);
        pFields.Add(pfFromDate);
        pFields.Add(pfToDate);

        //Passing ParameterFields to CrystalReportViewer
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

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string filePathReceipt = Path.GetTempFileName();

        try
        {
            ReportDoc.ExportToDisk(ExportFormatType.PortableDocFormat, filePathReceipt);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        //Printing receipt: start
        System.Diagnostics.Process printjob = new System.Diagnostics.Process();
        printjob.StartInfo.FileName = filePathReceipt;
        //printjob.StartInfo.Verb = string.Empty;
        printjob.StartInfo.Verb = "PrintTo";
        printjob.StartInfo.UseShellExecute = true;
        printjob.StartInfo.CreateNoWindow = true;

        printjob.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        
        PrinterSettings setting = new PrinterSettings();       
        setting.DefaultPageSettings.Landscape = true;

        try
        {
            printjob.Start();
            ll.Text = "4";
        }
        catch (Exception ex)
        {
            ll.Text=ex.Message;
        }
        //Printing receipt: end

        if (File.Exists(filePathReceipt))
        {
            //Delete the file after it has been printed
            File.Delete(filePathReceipt);
        }
    }

}