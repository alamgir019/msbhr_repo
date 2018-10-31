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
using cashword.BLL;

public partial class CrystalReports_Payroll_PFLoanLedgerViewer : System.Web.UI.Page
{
    PayrollReportManager objPayRptMgr = new PayrollReportManager();
    ReportManager rptManager = new ReportManager();
    DataTable MyDataTable = new DataTable();
    private ReportDocument ReportDoc;
    private PrintDocument printDoc = new PrintDocument();
    private string ReportPath = "";
    DataTable dtPayrollSummary;
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    DataTable dtGrossSalHead = new DataTable();
    DataTable dtEmpPayroll = new DataTable();
    decimal dclEmpBenefits = 0;
    decimal dclEmpDeduct = 0;
    decimal dclTotalSalary = 0;
    string DEAColl = "";
    string SalHead = "";
    string AccNo = "";

    clscashword InWord = new clscashword();

    private string LogoPath = System.Web.Configuration.WebConfigurationManager.AppSettings["LogoPath"];

    protected void Page_Init(object sender, EventArgs e)
    {        
        ConfigureCrystalReports();
    }

    private void ConfigureCrystalReports()
    {
        MyDataTable = new DataTable();
        ReportDoc = new ReportDocument();

       // dsMainShftReport dsMSR = new dsMainShftReport();

        switch (Session["REPORTID"].ToString())
        {
            #region Salary   
            case "ESPS":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptSalPaySlipAll.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_PayslipMonthlyAll(Session["FisYear"].ToString(), Session["VMonth"].ToString(), Session["VYear"].ToString(), Session["EmpID"].ToString(), Session["Desig"].ToString(), Session["SalDiv"].ToString());
                    DataTable dtPaySlipAll = (DataTable)MyDataTable;
                    if (dtPaySlipAll.Rows.Count > 0)
                    {
                        dtPaySlipAll.Columns.Add(new DataColumn("TakaInWord", typeof(string)));
                        foreach (DataRow dRow in dtPaySlipAll.Rows)
                        {
                            dRow["TakaInWord"] = InWord.getCashWord(dRow["NetSal"].ToString().Trim());
                        }
                    }
                    ReportDoc.SetDataSource(dtPaySlipAll);
                    ReportDoc.SetParameterValue("P_Header", "Salary/Wages for the month of-- " + Common.ReturnFullMonthName(Session["VMonth"].ToString())+", " + Session["VYear"].ToString());
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRV.ReportSource = ReportDoc;
                    //if (string.Equals( Session["ReportFormat"].ToString(),"excel"))
                    //{
                    ReportDoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "ReortDetails");
                    //}
                    //else if (string.Equals(Session["ReportFormat"].ToString(), "pdf"))
                    //{
                    //    ReportDoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "ReortDetails");
                    //}
                    break;
                }
            case "BSFF":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptBankStatement.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_BankStatement(Session["FisYear"].ToString(), Session["VMonth"].ToString(), Session["VYear"].ToString(), Session["DivisionId"].ToString(), Session["EmpTypeId"].ToString(), Session["SalType"].ToString ());
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));
                    ReportDoc.SetDataSource(MyDataTable);
                    //ReportDoc.SetParameterValue("P_Header", "Bank Statement for the Month of - " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    ReportDoc.SetParameterValue("p_Month", Common.ReturnFullMonthName(Session["VMonth"].ToString()));
                    ReportDoc.SetParameterValue("p_Year",  Session["VYear"].ToString());
                    ReportDoc.SetParameterValue("p_SalaryType", Session["SalType"].ToString());                    
                    CRV.ReportSource = ReportDoc;
                    ReportDoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "ReortDetails");
                    break;
                }
                #region Salary Certificate
            case "SC":
                {
                    string rptType = Session["rptType"].ToString();
                    string strGender = "";
                    string strHeShe = "";
                    decimal dclFestivalBonus = 0;
                    decimal dclGratuity = 0;
                    decimal dclGrandTotal = 0;
                    decimal dclNetPay = 0;
                    decimal dclPF = 0;
                    if (rptType == "1")
                    {
                        ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptPaySleepWithTax.rpt");
                        ReportDoc.Load(ReportPath);
                    }
                    else
                    {
                        ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptPaySleepWithoutTax.rpt");
                        ReportDoc.Load(ReportPath);
                    }
                    MyDataTable = objPayRptMgr.Get_PaySleepWithTax(Session["VMonth"].ToString(), Session["VYear"].ToString(), Session["EmpID"].ToString());//, Session["SectorId"].ToString(), Session["PostingDistId"].ToString()
                    DateTime now = System.DateTime.Now;
                    DataTable dt1 = (DataTable)MyDataTable;
                    if (dt1.Rows.Count > 0)
                    {
                        string EmpTypeID = (dt1.Rows[0]["EmpTypeID"]).ToString();
                        ReportDoc.SetParameterValue("P_Name", dt1.Rows[0]["FullName"]);
                        //CRV.ReportSource = ReportDoc;
                        ReportDoc.SetParameterValue("P_Desig", dt1.Rows[0]["DesigName"]);
                        //CRV.ReportSource = ReportDoc;
                        ReportDoc.SetParameterValue("P_Basic", String.Format("{0:0,0}", dt1.Rows[0]["P_Basic"]));
                        //CRV.ReportSource = ReportDoc;
                        ReportDoc.SetParameterValue("P_HouseRent", String.Format("{0:0,0}", dt1.Rows[0]["P_HouseRent"]));
                        //CRV.ReportSource = ReportDoc;
                        ReportDoc.SetParameterValue("P_Medical", String.Format("{0:0,0}", dt1.Rows[0]["P_Medical"]));
                        //CRV.ReportSource = ReportDoc;
                        ReportDoc.SetParameterValue("P_Other", String.Format("{0:0,0}", dt1.Rows[0]["P_Other"]));
                        //CRV.ReportSource = ReportDoc;
                        ReportDoc.SetParameterValue("P_Gross", String.Format("{0:0,0}", dt1.Rows[0]["P_Gross"]));
                        //CRV.ReportSource = ReportDoc;
                        ReportDoc.SetParameterValue("P_PF", String.Format("{0:0,0}", (EmpTypeID == "2" ? 0 : dt1.Rows[0]["P_PF"])));
                        //CRV.ReportSource = ReportDoc;
                        ReportDoc.SetParameterValue("P_TotalLoan", String.Format("{0:0,0}", (EmpTypeID == "2" ? 0 : dt1.Rows[0]["P_TotalLoan"])));
                        //CRV.ReportSource = ReportDoc;

                        //dclFestivalBonus = Math.Round(Convert.ToDecimal(dt1.Rows[0]["P_Basic"]) / 6, 0);


                        //dclFestivalBonus = Math.Round((EmpTypeID == "2" ? 0 : Convert.ToDecimal(dt1.Rows[0]["P_Basic"])) / 6, 0);
                        dclPF = Math.Round((EmpTypeID == "2" ? 0 : Convert.ToDecimal(dt1.Rows[0]["P_PF"])));
                        ReportDoc.SetParameterValue("P_FBonus", String.Format("{0:0,0}", dclFestivalBonus));
                        //CRV.ReportSource = ReportDoc;

                        //Joining Date to Current Date Calculation
                        DateTime dtJoiningDate = Convert.ToDateTime(dt1.Rows[0]["JoiningDate"]);
                        DateTime dtCurrDate = Convert.ToDateTime(DateTime.Now);

                        DateTime dtFrom = new DateTime();
                        DateTime dtTo = new DateTime();
                        double iTotDay = 0;
                        char[] splitter = { '/' };
                        string[] arinfo = Common.str_split(Common.DisplayDate(dtJoiningDate.ToString()), splitter);
                        if (arinfo.Length == 3)
                        {
                            dtFrom = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                            arinfo = null;
                        }
                        arinfo = Common.str_split(Common.DisplayDate(dtCurrDate.ToString()), splitter);
                        if (arinfo.Length == 3)
                        {
                            dtTo = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                            arinfo = null;
                        }

                        TimeSpan Dur = dtTo.Subtract(dtFrom);

                        iTotDay = Math.Round(Convert.ToDouble(Dur.Days), 0) + 1;
                        //if (iTotDay >= 365)
                        //    dclGratuity = Math.Round((EmpTypeID == "2" ? 0 : (Convert.ToDecimal(dt1.Rows[0]["P_Basic"]) / 12)), 0);
                        //else
                        //    dclGratuity = 0;

                        ReportDoc.SetParameterValue("P_Gratuaty", String.Format("{0:0,0}", dclGratuity));
                        //CRV.ReportSource = ReportDoc;

                        dclGrandTotal = Convert.ToDecimal(dt1.Rows[0]["P_Gross"])  + dclFestivalBonus + dclGratuity;
                        ReportDoc.SetParameterValue("P_GrandTotal", String.Format("{0:0,0}", dclGrandTotal));
                        //CRV.ReportSource = ReportDoc;

                        if (rptType == "1")
                        {
                            ReportDoc.SetParameterValue("P_IT", String.Format("{0:0,0}", dt1.Rows[0]["P_IT"]));
                            //CRV.ReportSource = ReportDoc;
                            dclNetPay = dclGrandTotal - Convert.ToDecimal(dt1.Rows[0]["P_IT"]) - (dclPF * 2) - Math.Round((EmpTypeID == "2" ? 0 : Convert.ToDecimal(dt1.Rows[0]["P_TotalLoan"])));
                            ReportDoc.SetParameterValue("P_NetPay", String.Format("{0:0,0}", dclNetPay));
                            //CRV.ReportSource = ReportDoc;
                        }
                        strGender = dt1.Rows[0]["Gender"].ToString();
                        if (strGender == "M")
                        {
                            strGender = "Mr. ";
                            strHeShe = " He";
                        }
                        else
                        {
                            strGender = "Ms. ";
                            strHeShe = " She";
                        }
                        ReportDoc.SetParameterValue("P_Body", "This is to certify that " + strGender + dt1.Rows[0]["FullName"] + ", " + dt1.Rows[0]["JobTitleName"] + " of " +
                            dt1.Rows[0]["DivisionName"].ToString() + "," + dt1.Rows[0]["SectorName"].ToString() + " has been working in this organization since " +
                            dtJoiningDate.ToString("dd") + " "+ dtJoiningDate.ToString("MMMM") + " " + dtJoiningDate.ToString("yyyy") + "." +
                            strHeShe + " is a "  + dt1.Rows[0]["TypeName"].ToString()  + " employee of the organization. As per our service rule/terms of employment his date of retirement in N/A." +
                            strHeShe + " is working in our clinic division/department as a " + dt1.Rows[0]["JobTitleName"] +".") ;

                        if (dt1.Rows[0]["Gender"].ToString() == "M")
                            strGender = "His ";
                        else
                            strGender = "Her ";

                        ReportDoc.SetParameterValue("P_SalaryTitle", strGender + "current salary (monthly) statement is as follows:");

                        //CRV.ReportSource = ReportDoc;
                        ReportDoc.SetParameterValue("P_date", now.ToString("MMMM") + " " + now.ToString("dd") + ", " + now.ToString("yyyy"));
                        ReportDoc.SetParameterValue("p_He_She", strHeShe);
                        //CRV.ReportSource = ReportDoc;
                    }
                    CRV.ReportSource = ReportDoc;
                    ReportDoc.ExportToHttpResponse(ExportFormatType.Excel, Response, true, "ReortDetails");
                    break;
                }
                #endregion
            //Salary Sheet Summery Emp Wise
            case "SSS":
                {

                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptSalSheetSummeryEmpWise.rpt");
                    ReportDoc.Load(ReportPath);
//                    MyDataTable = objPayRptMgr.Get_Salary_SheetEmpWise(Session["VMonth"].ToString(), Session["EmpID"].ToString(), Session["FisYear"].ToString(), Session["EmpTypeId"].ToString());
                    MyDataTable = objPayRptMgr.Get_Salary_SheetEmpWise(Session["VMonth"].ToString(), Session["FisYear"].ToString(), Session["SalDiv"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));
                    ReportDoc.SetParameterValue("P_Header", "Salary Sheet for The Month of " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));

                    ReportDoc.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRV.ReportSource = ReportDoc;
                    ReportDoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "ReortDetails");
                    break;
                }
            case "SSSum":
                {

                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptSalSheetSummery.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Salary_SheetSummary(Session["VMonth"].ToString(), Session["FisYear"].ToString(), Session["SalDiv"].ToString());

                    ReportDoc.SetDataSource(MyDataTable);
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));
                    ReportDoc.SetParameterValue("P_Header", "Salary Sheet Summary for The Month of " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));

                    ReportDoc.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRV.ReportSource = ReportDoc;
                    ReportDoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "ReortDetails");
                    break;
                }
            case "ESI":               
                ReportPath = Server.MapPath("~/CrystalReports/Employee/rptEmpSalaryInfo.rpt");               
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetEmpSalaryInfo(Session["EmpId"].ToString(), Session["GradeId"].ToString(), Session["ClinicId"].ToString(), Session["DeptId"].ToString(), Session["EmpTypeID"].ToString());
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Employee Salary Information");
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRV.ReportSource = ReportDoc;
                ReportDoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "ReortDetails");
                break;
            case "SEC":
                ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptEmpSalaryExceptionCase.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetEmpSalaryExceptionCase(Session["Vmonth"].ToString(), Session["VYear"].ToString());
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Employee Salary Information");
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRV.ReportSource = ReportDoc;
                ReportDoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "ReortDetails");
                break;
            case "SCH":
                ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptEmpSalaryHistory.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.GetEmpSalaryHistoryInfo(Session["EmpId"].ToString(), Session["GradeId"].ToString(), Session["ClinicId"].ToString(), Session["DeptId"].ToString(), Session["EmpTypeID"].ToString());
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Employee Salary History");
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRV.ReportSource = ReportDoc;
                ReportDoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "ReortDetails");
                break;
            //Salary Statement
            case "SS":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptSalaryStatement.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_SalaryStatement(Session["FisYear"].ToString(), Session["VMonth"].ToString(), Session["VYear"].ToString(),
                        Session["EmpID"].ToString(), Session["SalDiv"].ToString(), Session["SalType"].ToString(),Session["EmpTypeId"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));                    
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    ReportDoc.SetParameterValue("RptHeader", "Salary Statement for The Month of " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));
                    CRV.ReportSource = ReportDoc;
                    break;
                }           
            //Salary Summary Reports on Regular Staff
            case "SalSum":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptSalStatSummery.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_SalaryStatementSummery(Session["FisYear"].ToString(), Session["VMonth"].ToString(), Session["VYear"].ToString(),
                        Session["SalLoc"].ToString(),Session["SalType"].ToString(), Session["EmpTypeId"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));
                    ReportDoc.SetParameterValue("RptHeader", "Salary For The Month of " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            //Salary Sheet
            case "SSSEW":
                {
                    string usdRate = objPayrollMgr.SelectUSDRate(Session["VMonth"].ToString(), Session["VYear"].ToString());
                           usdRate = usdRate == "" ? Session["USDRATE"].ToString() : usdRate;

                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptSalarySSourandEmpWise.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_SalarySSourandEmpWise(Session["FisYear"].ToString(), Session["VMonth"].ToString(), Session["VYear"].ToString(), Session["SalDiv"].ToString(),
                        Session["EmpID"].ToString(), Session["SalSourceID"].ToString(),Session["EmpTypeId"].ToString(), usdRate);
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Salary Sheet for The Month of " + Common.ReturnFullMonthName(Session["VMonth"].ToString()) + ", "+ Session["VYear"].ToString());
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRV.ReportSource = ReportDoc;
                    break;
                }
           
            //Salary Statement
            case "SSS01":
                {                  
                    if (Session["EmpID"].ToString() != "")
                    {
                        ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptSalSheetSummeryEmpWise.rpt");
                        ReportDoc.Load(ReportPath);
                        MyDataTable = objPayRptMgr.Get_Salary_SheetSummaryEmpWise(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()), Session["EmpID"].ToString(), Session["FisYear"].ToString(), Session["EmpTypeId"].ToString());
                    }
                    else 
                    {
                        ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptSalSheetSummery.rpt");
                        ReportDoc.Load(ReportPath);
                        MyDataTable = objPayRptMgr.Get_Salary_SheetSummary01(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()), Session["FisYear"].ToString(), Session["SalDiv"].ToString());
                    }
                    ReportDoc.SetDataSource(MyDataTable);
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));
                    ReportDoc.SetParameterValue("P_Header", "Salary Sheet Summary for The Month of " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            case "PRLW":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptPayrollRprLocWise.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_PayrollReportLocWise(Session["VMonth"].ToString(), Session["VYear"].ToString(), Session["SalDiv"].ToString(), Session["PostDist"].ToString(), Session["EmpID"].ToString());
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Payroll for the Month of " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRV.ReportSource = ReportDoc;
                    ReportDoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "ReortDetails");
                    break;
                }
            case "ER":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptEffortReport.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_EffortReport(Session["VMonth"].ToString(), Session["VYear"].ToString(), Session["SalDiv"].ToString(), Session["PostDist"].ToString(), Session["EmpID"].ToString());
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Effort Report for the Month of " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            case "PBWC":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptBasicWiseCharging.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_PayrollBasicWChargiong(Session["VMonth"].ToString(), Session["VYear"].ToString(), Session["SalDiv"].ToString(), Session["PostDist"].ToString(), Session["EmpID"].ToString());
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Payroll for the Month of - " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            case "SSWSD":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptSalSourWSalDist.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_SalarySourceWiseSalDisReport(Session["VMonth"].ToString(), Session["VYear"].ToString(), Session["SalDiv"].ToString(), Session["PostDist"].ToString(), Session["EmpID"].ToString(), Session["SalSourceID"].ToString());
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", " Grant wise Salary Distribution for the Month of - " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            

            case "NSWSD":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptSalSourceWsNetSalDist.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_NetSalarySourceWiseSalDisReport(Session["VMonth"].ToString(), Session["VYear"].ToString(), Session["SalDiv"].ToString());
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", " Grant wise Net Salary Distribution for the Month of - " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            case "ADR":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptAddDeduction.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_AddDeductMonthRpt(Session["VMonth"].ToString(), Session["VYear"].ToString(), Session["SalHead"].ToString(), Session["EmpTypeId"].ToString());

                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Add/Deduction Report for the Month of " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));
                    ReportDoc.SetParameterValue("SHEAD", Session["SalHeadText"].ToString());
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            
            //Salary Sheet SOF Wise
            case "SSSOF":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptSalSheetSOFWise.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Salary_SheetSOFWise(Session["FisYear"].ToString(), Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()),
                       Session["EmpID"].ToString(), Session["SalDiv"].ToString());

                    ReportDoc.SetDataSource(MyDataTable);
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));

                    ReportDoc.SetParameterValue("P_Header", "Salary Statement for The Month of " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            //Salary Reconciliation 1
            case "SRDTL":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptSalReconDetail.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Salary_ReconDetail(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()),Session["EmpTypeId"].ToString());
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));
                    DateTime nowpre = now.AddMonths(-1);
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("RptHeader", "Salary Reconciliation Statement for The Month of " + now.ToString("MMMM") + "," + now.ToString("yyyy"));
                    CRV.ReportSource = ReportDoc;
                    ReportDoc.SetParameterValue("CMHead", "Current Month: " + now.ToString("MMMM") + "," + now.ToString("yyyy"));
                    CRV.ReportSource = ReportDoc;
                    ReportDoc.SetParameterValue("PMHead", "Previous Month: " + nowpre.ToString("MMMM") + ", " + nowpre.ToString("yyyy"));
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            //Salary Reconcilation Statement
            case "SR":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptSalRecon.rpt");
                    ReportDoc.Load(ReportPath);
                    DataSet ds = new DataSet();
                    ds = objPayRptMgr.Get_Salary_ReconAll(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()), Session["EmpTypeId"].ToString(), ds);
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));
                    DateTime nowpre = now.AddMonths(-1);
                    ReportDoc.SetDataSource(ds.Tables[0]);
                    ReportDoc.OpenSubreport("rptSReconSub.rpt").SetDataSource(ds.Tables[1]);
                    CRV.ReportSource = ReportDoc;
                    ReportDoc.SetParameterValue("RptHeader", "Salary Reconciliation Report " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));
                    CRV.ReportSource = ReportDoc;
                    ReportDoc.SetParameterValue("CMHead", now.ToString("MMMM") + "," + now.ToString("yyyy"));
                    CRV.ReportSource = ReportDoc;
                    ReportDoc.SetParameterValue("PMHead", nowpre.ToString("MMMM") + "," + nowpre.ToString("yyyy"));
                    CRV.ReportSource = ReportDoc;
                    ReportDoc.SetParameterValue("DfferMonth", "Diff (" + now.ToString("MMMM") + "-" + nowpre.ToString("MMMM") + ")");
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            //Salary Reconcilation 2
            case "SRR":
                {
                    string usdRate = objPayrollMgr.SelectUSDRate(Session["VMonth"].ToString(), Session["VYear"].ToString());
                    usdRate = usdRate == "" ? Session["USDRATE"].ToString() : usdRate;

                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptSalReconcilation.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Salary_Reconcilation(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()), usdRate, Session["EmpTypeId"].ToString());
                    DataTable dt1 = objPayRptMgr.Get_Salary_Reconcilation_Param(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()), Session["EmpTypeId"].ToString());
                    //PassParamValue(dt1);
                    ReportDoc.SetDataSource(MyDataTable);
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));// Convert.ToDateTime(Session["FromDate"].ToString());
                    if (dt1.Rows.Count > 0)
                    {
                        ReportDoc.SetParameterValue("NewJoindeEmpID", Common.ReturnZeroForNull(dt1.Rows[0]["NJEmpID"].ToString()));
                        ReportDoc.SetParameterValue("SeparatedEmpID", Common.ReturnZeroForNull(dt1.Rows[0]["SeprmpID"].ToString()));
                        ReportDoc.SetParameterValue("TNewJoin", Common.ReturnZeroForNull(dt1.Rows[0]["TNJ"].ToString()));
                        ReportDoc.SetParameterValue("LMTSN", Common.ReturnZeroForNull(dt1.Rows[0]["LMTSN"].ToString()));
                        ReportDoc.SetParameterValue("TNOS",Common.ReturnZeroForNull( dt1.Rows[0]["TNS"].ToString()));
                        ReportDoc.SetParameterValue("SS", Common.ReturnZeroForNull(dt1.Rows[0]["SS"].ToString()));
                        ReportDoc.SetParameterValue("PSN", Common.ReturnZeroForNull(dt1.Rows[0]["PSN"].ToString()));
                        ReportDoc.SetParameterValue("P_Header", "Salary Reconciliation Report " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));
                    }
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            
            #endregion
            #region Salary Certificate
            
            #endregion
            #region PF
            case "MPFC":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptMonthlyPFContribution.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_MonthlyPFContribution(Session["FisYear"].ToString(), Session["VMonth"].ToString(), Session["FisYearP"].ToString(),
                            Session["VMonthP"].ToString(), Session["SalDiv"].ToString(), Session["EmpID"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Monthly PF Contribution For The Month of  - " + Common.ReturnFullMonthName(Session["VMonth"].ToString()) + " Fiscal Year -" + Session["FisYearText"].ToString());
                    ReportDoc.SetParameterValue("P_Month", Common.ReturnFullMonthName(Session["VMonthP"].ToString()));
                    ReportDoc.SetParameterValue("C_Month", Common.ReturnFullMonthName(Session["VMonth"].ToString()));
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRV.ReportSource = ReportDoc;
                    ReportDoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "ReortDetails");
                    break;
                }
            case "YPFC":          
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptYearlyPFContribution.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_AnnualReport(Session["FisYear"].ToString(), Session["SalDiv"].ToString(), Session["EmpID"].ToString(), "YPFC", Session["EmpTypeId"].ToString()); //Session["YearlyType"].ToString()
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Yearly PF Contribution For The Fiscal Year " + Session["FisYearText"].ToString());
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRV.ReportSource = ReportDoc;
                    ReportDoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "ReortDetails");
                    break;
                }           
            case "YPFB":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptYearlyPFBalance.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_AnnualReport(Session["FisYear"].ToString(), Session["SalDiv"].ToString(), Session["EmpID"].ToString(), "YPFC", Session["EmpTypeId"].ToString()); //Session["YearlyType"].ToString()
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Yearly PF Contribution For The Fiscal Year " + Session["FisYearText"].ToString());
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRV.ReportSource = ReportDoc;
                    ReportDoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "ReortDetails");
                    break;
                }
           
            case "YPFLD":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptYearlyPFLoanDeduct.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_AnnualReport(Session["FisYear"].ToString(), Session["SalDiv"].ToString(), Session["EmpID"].ToString(), "YPFLD", Session["EmpTypeId"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "PF Loan Deduction For The Fiscal Year " + Session["FisYearText"].ToString());
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRV.ReportSource = ReportDoc;
                    ReportDoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "ReortDetails");
                    break;
                }
            case "PFLL":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptPFLoanLedger.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.GetPFLoanLedgerData(Session["VMonth"].ToString(), Session["FisYear"].ToString(), "M");
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("pMonthName", Common.ReturnFullMonthName(Session["VMonth"].ToString()));
                   
                    CRV.ReportSource = ReportDoc;
                    ReportDoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "ReortDetails");
                    break;
                }
            #endregion
            #region Final Payment
            case "FP":
                {
                    DataSet ds = new DataSet();
                    ds = objPayRptMgr.Get_Rpt_FaynalPaymentList(Session["VMonth"].ToString(), Session["VYear"].ToString(), Session["EmpID"].ToString(), ds);

                    DataTable tableA = ds.Tables[0].Copy();
                    DataTable tableB = ds.Tables[1].Copy();
                    DataSet dso2 = new DataSet();

                    tableA.TableName = "dtFaynalPaymentList";
                    dso2.Tables.Add(tableA);
                    tableB.TableName = "dtYearlyPFContribution";
                    dso2.Tables.Add(tableB);

                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptFinalPayment.rpt");
                    ReportDoc.Load(ReportPath);

                    ReportDoc.SetDataSource(dso2);
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            case "FPDL":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptFaynalPaymentList.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_FaynalPaymentDueList(Session["VMonth"].ToString(), Session["VYear"].ToString(), Session["EmpID"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Final Payment Due List");
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            #endregion
            #region Voucher
            case "AV":
                {
                    string vouType = Session["VoucherType"].ToString();
                    if (vouType == "13")
                        ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptCDV_Gratuaty.rpt");
                    else
                        ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptCDV01.rpt");
                    ReportDoc.Load(ReportPath);
                    string vouHead = GetVHead(vouType);

                    MyDataTable = objPayRptMgr.Get_CD_Voucher(Session["FisYear"].ToString(), Session["VMonth"].ToString(), Session["VYear"].ToString(),
                                Session["BankAccNo"].ToString(), SalHead, Session["SalDiv"].ToString(), DEAColl, AccNo, vouType, Session["EmpTypeId"].ToString());
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));                   
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Head", vouHead + " " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            case "BV":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptCDV01.rpt");
                    ReportDoc.Load(ReportPath);
                    AccNo = "4011";
                    MyDataTable = objPayRptMgr.Get_Bonus_Voucher(Session["FisYear"].ToString(), Session["VMonth"].ToString(), Session["Festival"].ToString(),
                                Session["SalDiv"].ToString(),AccNo,Session["EmpTypeId"].ToString());
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Head", "Bonus Voucher For The Month - " + Common.ReturnFullMonthName(Session["VMonth"].ToString()));
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            #endregion
            #region HR Action
            case "PRECC":  
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptEmpCurrCharging.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_PayrollReportEmpCurrCharging(Session["VMonth"].ToString(), Session["VYear"].ToString(), Session["SalDiv"].ToString(), Session["PostDist"].ToString(), Session["EmpID"].ToString());
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Payroll Peport Employee Current Charging for the Month of - " + now.ToString("MMMM") + ", " + now.ToString("yyyy"));
                    CRV.ReportSource = ReportDoc;
                    break;
                }   
            case "ARA":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptAddRequirementAllow.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_AddRequirementAllow(Session["VMonth"].ToString(), Session["VYear"].ToString());
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", " Additional Requirement Allowance " + now.ToString("MMMM") + " of " + now.ToString("yyyy"));
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            case "EPHR":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptEmpPromotionHistory.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_EmpPromotionHistory(Session["SalDiv"].ToString(), Session["VMonth"].ToString(), Session["VYear"].ToString(),
                        Session["Grade"].ToString(), Session["Desig"].ToString(), Session["FDate"].ToString(), Session["TDate"].ToString(), Session["EmpID"].ToString());
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", " Employee Promotion History ");
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            case "ETR":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptEmpTransfer.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_EmpTransferReport(Session["SalDiv"].ToString(), Session["VMonth"].ToString(), Session["VYear"].ToString(),
                        Session["Grade"].ToString(), Session["Desig"].ToString(), Session["FDate"].ToString(), Session["TDate"].ToString(), Session["EmpID"].ToString());
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Employee Transfer Report");
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            case "ECSR":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptEmpChangeStatus.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_EmpChangeStatusReport(Session["SalDiv"].ToString(), Session["VMonth"].ToString(), Session["VYear"].ToString(),
                        Session["Grade"].ToString(), Session["Desig"].ToString(), Session["FDate"].ToString(), Session["TDate"].ToString(), Session["EmpID"].ToString());

                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Employee Change Status Report");
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            
            case "ESCHR":
                {

                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptEmpSalChanHistory.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_EmpSalChanHistoryRpt(Session["FDate"].ToString(), Session["TDate"].ToString(), Session["EmpID"].ToString(),
                        Session["Sector"].ToString(), Session["Dept"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Employee Salary Change History " + Session["FDate"].ToString() + " To " + Session["TDate"].ToString());
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            #endregion
            #region Medical
            case "MR":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptMedicalHospitlity.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_MedicalReport(Session["FisYear"].ToString(), Session["EmpTypeId"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", " Medical Report - " + Session["FisYearText"].ToString());
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            case "MBB":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptMedicalBenefitsBalance.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_MedicalBenefitsBalance(Session["FisYear"].ToString(), Session["SalSunLocID"].ToString(), Session["EmpID"].ToString(), Session["EmpTypeId"].ToString());

                    DataTable dtMedicalBalance = new DataTable();
                    dtMedicalBalance.Columns.Add("EmpId");
                    dtMedicalBalance.Columns.Add("FullName");
                    dtMedicalBalance.Columns.Add("BenefitType");
                    dtMedicalBalance.Columns.Add("MEJan");
                    dtMedicalBalance.Columns.Add("MEFeb");
                    dtMedicalBalance.Columns.Add("MEMar");
                    dtMedicalBalance.Columns.Add("MEApr");
                    dtMedicalBalance.Columns.Add("MEMay");
                    dtMedicalBalance.Columns.Add("MEJun");
                    dtMedicalBalance.Columns.Add("MEJul");
                    dtMedicalBalance.Columns.Add("MEAug");
                    dtMedicalBalance.Columns.Add("MESep");
                    dtMedicalBalance.Columns.Add("MEOct");
                    dtMedicalBalance.Columns.Add("MENov");
                    dtMedicalBalance.Columns.Add("MEDec");
                    dtMedicalBalance.Columns.Add("Entitled");

                    foreach (DataRow dRow in MyDataTable.Rows)
                    {
                        string strExpr = "EmpId='" + dRow["EmpId"].ToString().Trim() + "'";
                        DataRow[] foundRows = MyDataTable.Select(strExpr);

                        DataRow nRow = dtMedicalBalance.NewRow();
                        nRow["EmpId"] = dRow["EmpId"].ToString().Trim();
                        nRow["FullName"] = dRow["FullName"].ToString().Trim();
                        if (dRow["BenefitType"].ToString() == "M")
                        {
                            nRow["BenefitType"] = "Medical";
                            nRow["Entitled"] = "35000";
                        }
                        else
                        {
                            nRow["BenefitType"] = "Hospital";
                            nRow["Entitled"] = "40000";
                        }
                        nRow["MEJan"] =dRow["MEJan"].ToString().Trim();
                        nRow["MEFeb"] = dRow["MEFeb"].ToString().Trim();
                        nRow["MEMar"] = dRow["MEMar"].ToString().Trim();
                        nRow["MEApr"] = dRow["MEApr"].ToString().Trim();
                        nRow["MEMay"] = dRow["MEMay"].ToString().Trim();
                        nRow["MEJun"] = dRow["MEJun"].ToString().Trim();
                        nRow["MEJul"] = dRow["MEJul"].ToString().Trim();
                        nRow["MEAug"] = dRow["MEAug"].ToString().Trim();
                        nRow["MESep"] = dRow["MESep"].ToString().Trim();
                        nRow["MEOct"] = dRow["MEOct"].ToString().Trim();
                        nRow["MENov"] = dRow["MENov"].ToString().Trim();
                        nRow["MEDec"] = dRow["MEDec"].ToString().Trim();
                        
                        dtMedicalBalance.Rows.Add(nRow);
                        dtMedicalBalance.AcceptChanges();

                        if (foundRows.Length == 1)
                        {
                            nRow = dtMedicalBalance.NewRow();                           
                            if (dRow["BenefitType"].ToString() == "M")
                            {
                                nRow["BenefitType"] = "Hospital";
                                nRow["Entitled"] = "40000";
                            }
                            else
                            {
                                nRow["BenefitType"] = "Medical";
                                nRow["Entitled"] = "35000";
                            }
                            nRow["MEJan"] = "0";
                            nRow["MEFeb"] = "0";
                            nRow["MEMar"] = "0";
                            nRow["MEApr"] = "0";
                            nRow["MEMay"] = "0";
                            nRow["MEJun"] = "0";
                            nRow["MEJul"] = "0";
                            nRow["MEAug"] = "0";
                            nRow["MESep"] = "0";
                            nRow["MEOct"] = "0";
                            nRow["MENov"] = "0";
                            nRow["MEDec"] = "0";                            
                            dtMedicalBalance.Rows.Add(nRow);
                            dtMedicalBalance.AcceptChanges();
                        }
                    }
                    
                    ReportDoc.SetDataSource(dtMedicalBalance);
                    ReportDoc.SetParameterValue("P_Header", "Medical Benefits Balance");                      
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            case "MBR":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptMedicalBenefitsReceive.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_MedicalBenefitsBalance(Session["FisYear"].ToString(), Session["SalSunLocID"].ToString(), Session["EmpID"].ToString(), Session["EmpTypeId"].ToString());

                    DataTable dtMedicalReceive = new DataTable();
                    dtMedicalReceive.Columns.Add("EmpId");
                    dtMedicalReceive.Columns.Add("FullName");
                    dtMedicalReceive.Columns.Add("BenefitType");
                    dtMedicalReceive.Columns.Add("MEJan");
                    dtMedicalReceive.Columns.Add("MEFeb");
                    dtMedicalReceive.Columns.Add("MEMar");
                    dtMedicalReceive.Columns.Add("MEApr");
                    dtMedicalReceive.Columns.Add("MEMay");
                    dtMedicalReceive.Columns.Add("MEJun");
                    dtMedicalReceive.Columns.Add("MEJul");
                    dtMedicalReceive.Columns.Add("MEAug");
                    dtMedicalReceive.Columns.Add("MESep");
                    dtMedicalReceive.Columns.Add("MEOct");
                    dtMedicalReceive.Columns.Add("MENov");
                    dtMedicalReceive.Columns.Add("MEDec");   
                 

                    foreach (DataRow dRow in MyDataTable.Rows)
                    {
                        string strExpr = "EmpId='" + dRow["EmpId"].ToString().Trim() + "'";
                        DataRow[] foundRows = MyDataTable.Select(strExpr);

                        DataRow nRow = dtMedicalReceive.NewRow();
                        nRow["EmpId"] = dRow["EmpId"].ToString().Trim();
                        nRow["FullName"] = dRow["FullName"].ToString().Trim();
                        if (dRow["BenefitType"].ToString() == "M")                       
                            nRow["BenefitType"] = "Medical";                            
                        
                        else                       
                            nRow["BenefitType"] = "Hospital";                           
                        
                        nRow["MEJan"] = dRow["MEJan"].ToString().Trim();
                        nRow["MEFeb"] = dRow["MEFeb"].ToString().Trim();
                        nRow["MEMar"] = dRow["MEMar"].ToString().Trim();
                        nRow["MEApr"] = dRow["MEApr"].ToString().Trim();
                        nRow["MEMay"] = dRow["MEMay"].ToString().Trim();
                        nRow["MEJun"] = dRow["MEJun"].ToString().Trim();
                        nRow["MEJul"] = dRow["MEJul"].ToString().Trim();
                        nRow["MEAug"] = dRow["MEAug"].ToString().Trim();
                        nRow["MESep"] = dRow["MESep"].ToString().Trim();
                        nRow["MEOct"] = dRow["MEOct"].ToString().Trim();
                        nRow["MENov"] = dRow["MENov"].ToString().Trim();
                        nRow["MEDec"] = dRow["MEDec"].ToString().Trim();

                        dtMedicalReceive.Rows.Add(nRow);
                        dtMedicalReceive.AcceptChanges();

                        if (foundRows.Length == 1)
                        {
                            nRow = dtMedicalReceive.NewRow();
                            if (dRow["BenefitType"].ToString() == "M")                            
                                nRow["BenefitType"] = "Hospital";                               
                            else                           
                                nRow["BenefitType"] = "Medical";                             
                            nRow["MEJan"] = "0";
                            nRow["MEFeb"] = "0";
                            nRow["MEMar"] = "0";
                            nRow["MEApr"] = "0";
                            nRow["MEMay"] = "0";
                            nRow["MEJun"] = "0";
                            nRow["MEJul"] = "0";
                            nRow["MEAug"] = "0";
                            nRow["MESep"] = "0";
                            nRow["MEOct"] = "0";
                            nRow["MENov"] = "0";
                            nRow["MEDec"] = "0";
                            dtMedicalReceive.Rows.Add(nRow);
                            dtMedicalReceive.AcceptChanges();
                        }
                    }
                    ReportDoc.SetDataSource(dtMedicalReceive);
                    ReportDoc.SetParameterValue("P_Header", "Medical Benefits Received");                    
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            case "MMRR":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptMonthlyMHReceivedBalance.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_MonthlyMHReceivedBalance(Session["VMonth"].ToString(), Session["FisYear"].ToString(), Session["SalSunLocID"].ToString(), Session["EmpID"].ToString(), Session["BenefitType"].ToString(), Session["EmpTypeId"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Monthly Medicine Received Report for " + Common.ReturnFullMonthName(Session["VMonth"].ToString()).ToString() + " - " + Session["VYear"].ToString());
                    ReportDoc.SetParameterValue("P_HeadMH", "Medicine Cost Limit");                    
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            case "MHRR":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptMonthlyMHReceivedBalance.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_MonthlyMHReceivedBalance(Session["VMonth"].ToString(), Session["FisYear"].ToString(), Session["SalSunLocID"].ToString(), Session["EmpID"].ToString(), Session["BenefitType"].ToString(), Session["EmpTypeId"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Monthly Hospital Received Report for " + Common.ReturnFullMonthName(Session["VMonth"].ToString()).ToString() + " - " + Session["VYear"].ToString());
                    ReportDoc.SetParameterValue("P_HeadMH", "Hospital Cost Limit");                    
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            #endregion
            #region Bonus
            case "BST":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptdtBonusStatFastival.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_BonusStatementFastival(Session["FisYear"].ToString(), Session["VMonth"].ToString(),
                        Session["SalLoc"].ToString(),  Session["Religion"].ToString(), Session["Festival"].ToString(), Session["EmpTypeId"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Bonus Statement For the Festival of " + Session["FestivalName"].ToString() + "  For The Month - " + Common.ReturnFullMonthName(Session["VMonth"].ToString()));
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRV.ReportSource = ReportDoc;

                    ReportDoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "ReortDetails");
                    break;
                }
            case "EBPS":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptBonusPaySlipAll.rpt");
                    ReportDoc.Load(ReportPath);

                    MyDataTable = objPayRptMgr.Get_BonusPayslipMonthlyAll(Session["FisYear"].ToString(), Session["EmpID"].ToString(), Session["SalLoc"].ToString(), Session["SalSubLoc"].ToString(), Session["EmpTypeId"].ToString());

                    DataTable dtPaySlipAll = (DataTable)MyDataTable;
                    if (dtPaySlipAll.Rows.Count > 0)
                    {
                        dtPaySlipAll.Columns.Add(new DataColumn("TakaInWord", typeof(string)));
                        foreach (DataRow dRow in dtPaySlipAll.Rows)
                        {
                            dRow["TakaInWord"] = InWord.getCashWord(dRow["PayAmt"].ToString().Trim());
                        }
                    }

                    ReportDoc.SetDataSource(dtPaySlipAll);
                    ReportDoc.SetParameterValue("P_Header", "Festival Bonus Pay Slip for " + Session["FisYearText"].ToString());
                   
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            case "FBS":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptdtFastivalBSummery.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_FastivalBonusSummery(Session["FisYear"].ToString(), Session["VMonth"].ToString(), Session["Festival"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);                   
                    ReportDoc.SetParameterValue("P_Header", " Festival Bonus Summery  For The Month of - " + Common.ReturnFullMonthName(Session["VMonth"].ToString()));
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            case "FBSW":
                {
                    DateTime dtCurrDate = DateTime.Now;
                    string strYear = dtCurrDate.Year.ToString();

                    string usdRate = objPayrollMgr.SelectUSDRate(Session["VMonth"].ToString(), strYear);
                    usdRate = usdRate == "" ? Session["USDRATE"].ToString() : usdRate;

                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptEmpBonusCharging.rpt");
                    ReportDoc.Load(ReportPath);

                    MyDataTable = objPayRptMgr.Get_Rpt_FestivalBonusCharging(Session["VMonth"].ToString(), Session["FisYear"].ToString(), Session["SalLoc"].ToString(),
                            Session["SalSubLoc"].ToString(), Session["EmpID"].ToString(), Session["Festival"].ToString(), Session["SalSourceID"].ToString(), usdRate,Session["EmpTypeId"].ToString());

                    string year = Session["Year"].ToString();
                    int strlength = year.Length;

                    string year01 = year.Substring(strlength - 4);
                    DateTime now = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + year01));                   
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Festival Bonus for " + Session["FestivalName"].ToString() + " the Month of - " + now.ToString("MMMM") + "," + now.ToString("yyyy"));
                    CRV.ReportSource = ReportDoc;
                    ReportDoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "ReortDetails");
                    break;
                }
            case "BSR":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptBonusStatReport.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_BonusStatementFastival(Session["FisYear"].ToString(), Session["VMonth"].ToString(),
                        Session["SalLoc"].ToString(),  Session["Religion"].ToString(), Session["Festival"].ToString(), Session["EmpTypeId"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Bonus Statement For the Festival of " + Session["FestivalName"].ToString() + "  For The Month - " + Common.ReturnFullMonthName(Session["VMonth"].ToString()));
                    CRV.ReportSource = ReportDoc;
                    break;
                }
                #endregion
            #region Gratuity
            case "SBSR":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptGratuityBenefitsSummery.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_GratuityBenefitsSummery(Session["VMonth"].ToString(), Session["VYear"].ToString(),Session["Quarter"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Severance Benefits Summery For the Quarter Ended   - " + Common.ReturnFullMonthName(Session["VMonth"].ToString()));
                    CRV.ReportSource = ReportDoc;                
                    break;
                }
            case "SBR":
                {                  
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptGratuityBenefits.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_GratuityBenefits(Session["VMonth"].ToString(), Session["VYear"].ToString(), Session["Quarter"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Employee Wise Gratuity Statement for  - " + Common.ReturnFullMonthName(Session["VMonth"].ToString()) + " Quarter : " + Session["Quarter"].ToString());
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            #endregion
            #region PF
            case "IPFC":
                {

                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptIndividuallyPFContribution.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_AnnualReport(Session["FisYear"].ToString(), Session["SalDiv"].ToString(), Session["EmpID"].ToString(), "IPFC", Session["EmpTypeId"].ToString());
                    MyDataTable.Columns.Add("ServiceLength");
                    string strServiceLength = "";
                    foreach (DataRow dRow in MyDataTable.Rows)
                    {
                        strServiceLength = Common.CalculateYearMonthDay(Common.DisplayDate(dRow["JoiningDate"].ToString()));
                        dRow["ServiceLength"] = strServiceLength;
                        MyDataTable.AcceptChanges();
                    }
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Individual Employee's PF Contribution");
                    CRV.ReportSource = ReportDoc;

                    ReportDoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "ReortDetails");
                    break;
                }        
           
           
          
                    
            case "AI":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptAnnualIncome.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_AnnualReport(Session["FisYear"].ToString(), Session["SalDiv"].ToString(), Session["EmpID"].ToString(), "AI",Session["EmpTypeId"].ToString());

                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Employee Annual Income For The Fiscal Year -" + Session["FisYearText"].ToString());
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            #endregion
            #region Tax
            case "AITD":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptYearlyITDeduct.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_AnnualReport(Session["FisYear"].ToString(), Session["SalDiv"].ToString(), Session["EmpID"].ToString(), "AITD", Session["EmpTypeId"].ToString());

                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Staff Salary Tax Deduction for The Fiscal Year -" + Session["FisYearText"].ToString());
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            case "TDR":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptTaxDeduction.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_AnnualReport(Session["FisYear"].ToString(), Session["SalDiv"].ToString(), Session["EmpID"].ToString(), "TDR", Session["EmpTypeId"].ToString());

                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Tax Deduction for The Fiscal Year -" + Session["FisYearText"].ToString());
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            case "ITC":
                {
                    
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptITComputation.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_ITComputation(Session["FisYear"].ToString(), Session["VMonth"].ToString(), Session["EmpID"].ToString());

                    ReportDoc.SetDataSource(MyDataTable);
                   // ReportDoc.SetParameterValue("P_EmpId", MyDataTable.Rows[0]["EmpId"].ToString().Trim());
                    ReportDoc.SetParameterValue("P_Header", "Computation of Income Tax");
                    ReportDoc.SetParameterValue("P_FiscalYear", "Income Year :" + Session["FisYearText"].ToString());
                    ReportDoc.SetParameterValue("P_HouseRentEx", "300000");                   
                    ReportDoc.SetParameterValue("P_TransportEx", "30000");
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            case "ITA":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptITAssessment.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_ITAssessment(Session["FisYear"].ToString(), Session["VMonth"].ToString(), Session["EmpID"].ToString());
                    ReportDoc.SetDataSource(MyDataTable);
                    //ReportDoc.SetParameterValue("P_EmpId", MyDataTable.Rows[0]["EmpId"].ToString().Trim());
                    ReportDoc.SetParameterValue("P_Header", "Income Tax Assessment");
                    ReportDoc.SetParameterValue("P_FiscalYear", "Income Year :" + Session["FisYearText"].ToString());
                    ReportDoc.SetParameterValue("P_HouseRentEx", "300000");
                    ReportDoc.SetParameterValue("P_MedicalEx", "120000");
                    ReportDoc.SetParameterValue("P_TransportEx", "30000");
                    ReportDoc.SetParameterValue("ComLogo", LogoPath);
                    CRV.ReportSource = ReportDoc;
                    break;
                }

            case "TC":
                {
                    dsITStatement ds01 = new dsITStatement();
                    ds01.Tables.Remove("dtEMPINFO");
                    ds01.Tables.Remove("dtITDEPOSITRECORDS");

                    DataSet ItStatementds = new DataSet();

                    ItStatementds = objPayRptMgr.Get_Rpt_ITStatement(Session["FisYear"].ToString(), Session["EmpTypeId"].ToString(), Session["EmpID"].ToString(), Session["SalLocId"].ToString(), Session["SalSubLocId"].ToString());

                    DataTable destinationTable = CopyDataTable(ItStatementds.Tables[0], ItStatementds.Tables[0].Rows.Count);
                    destinationTable.TableName = "dtEMPINFO";
                    ds01.Tables.Add(destinationTable.Copy());

                    DataTable destinationTable1 = CopyDataTable(ItStatementds.Tables[1], ItStatementds.Tables[1].Rows.Count);
                    destinationTable1.TableName = "dtITDEPOSITRECORDS";
                    ds01.Tables.Add(destinationTable1.Copy());


                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptITStatement.rpt");
                    ReportDoc.Load(ReportPath);

                    ReportDoc.SetDataSource(ds01);

                    ReportDoc.SetParameterValue("P_Header", "Annual salary certificate and advance tax deduction for the financial year" + Session["FisYearTxt"].ToString());
                    ReportDoc.SetParameterValue("P_FiscalYear", Session["FisYearTxt"].ToString());
                    ReportDoc.SetParameterValue("P_AssessYear", Session["AssessYear"].ToString());

                    CRV.ReportSource = ReportDoc;

                    break;
                }
				
            #endregion
            #region Increment/COLA
            case "CPIL":
                {
                    string LetterType = Session["LetterType"].ToString();
                    string Subject = "";
                    DateTime dt = Convert.ToDateTime(Session["PrintDate"].ToString());
                    DateTime EFDate = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));

                    MyDataTable = objPayRptMgr.Get_COLA_PerformanceIncrementLetter(Session["LetterType"].ToString(), Session["VMonth"].ToString(),
                                                                            Session["VYear"].ToString(), Session["PostingDist"].ToString());
                    if (LetterType == "C")
                    {
                        ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptCOLAIncrementLetter.rpt");

                        Subject = "Cost of Living Adjustment (COLA)";
                    }
                    else
                    {
                        ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptPerformanceIncrementLetter.rpt");
                        Subject = "Annual Increment";
                    }

                    ReportDoc.Load(ReportPath);
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Subject", Subject);
                    ReportDoc.SetParameterValue("P_PDate", dt.Day.ToString() + " " + String.Format("{0:y}", dt));
                    ReportDoc.SetParameterValue("P_EFDate", EFDate.ToString("dd MMMM yyyy"));
                    CRV.ReportSource = ReportDoc;

                    break;
                }
            case "IR":

                ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptIncrementReport.rpt");              
                ReportDoc.Load(ReportPath);
                MyDataTable = objPayRptMgr.GetIncrementReport(Session["EmpID"].ToString(), Session["SalLocId"].ToString(), Session["SalSubLocId"].ToString(),
                    Session["LetterType"].ToString(), Session["VMonth"].ToString(), Session["VYear"].ToString());
                CRV.Width = 10;
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Increment Report");
                CRV.ReportSource = ReportDoc;
                break;
            #endregion
            #region Budget
            case "MBP":
                {
                    ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptMonthlyBudgetProjection.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_MonthlyBudgetProjection(Session["VMonth"].ToString(), Session["VYear"].ToString());

                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "Monthly Budget for the Month " + Common.ReturnFullMonthName(Session["VMonth"].ToString()) + " , " + Session["VYear"].ToString());
                    CRV.ReportSource = ReportDoc;
                    break;
                }
            #endregion
            #region OT
            case "OTC":
                ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptOTCalculation.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = objPayRptMgr.GetOTCalculation(Session["EmpID"].ToString(), Session["SalLocId"].ToString(), Session["SalSubLocId"].ToString(), Convert.ToInt32(Session["VMonth"].ToString()), Convert.ToInt32(Session["VYear"].ToString()), Session["EmpTypeId"].ToString());
                CRV.Width = 10;
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "OT Calculation For the Month Of " + Common.ReturnFullMonthName(Session["VMonth"].ToString()));                
                CRV.ReportSource = ReportDoc;
                break;
            #endregion
            #region SAV
            case "AVL":
                ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptAccuredVacationSchedule.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = objPayRptMgr.GetAccuredVacationSchedule(Session["VMonth"].ToString(), Session["VYear"].ToString(), Session["FisYear"].ToString(), Session["EmpTypeId"].ToString());              
                CRV.Width = 10;
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pHeader", "Schedule of Accured Vacation for Country Office Paid Staff ");
                CRV.ReportSource = ReportDoc;
                break;
            #endregion 
            #region NGO
            case "NGOBSR":
                ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptNGOBureauSalaryRpt.rpt");
                    ReportDoc.Load(ReportPath);
                    MyDataTable = objPayRptMgr.Get_Rpt_NGOBureauSalaryRpt(Session["VMonth"].ToString(), Session["VYear"].ToString(), Session["SalSourceID"].ToString());
                    DateTime nowdt = Convert.ToDateTime(Common.ReturnDate("01/" + Session["VMonth"].ToString() + "/" + Session["VYear"].ToString()));
                    ReportDoc.SetDataSource(MyDataTable);
                    ReportDoc.SetParameterValue("P_Header", "NGO Bureau Salary Report for the Month of - " + nowdt.ToString("MMMM") + ", " + nowdt.ToString("yyyy"));
                    CRV.ReportSource = ReportDoc;
               break;
            #endregion
        }
    }

    private string GetFunctionSumValue() {
        return "";
    }

    protected string GetVHead(string ht)
    {

        string head = "";
        switch (ht)
        {
            case "01":
                {
                    AccNo = "4010";
                    SalHead = "1,2";
                    DEAColl = "Salary";
                    head = "Gross Salary Voucher For the Month of";
                    break;
                }
            case "02":
                {
                    AccNo = "4011";
                    SalHead = "3,4";
                    DEAColl = "Medical";
                    head = "Medical & Hospital Benefit Voucher For the Month of";
                    break;
                }
            case "03":
                {
                    AccNo = "4010";
                    SalHead = "5";
                    DEAColl = "Salary";
                    head = "Arrear Plus Voucher For the Month of";
                    break;
                }
            case "04":
                {
                    AccNo = "4010";
                    SalHead = "17";
                    DEAColl = "Salary";
                    head = "Arrear Minus Voucher For the Month of";
                    break;
                }
            case "05":
                {
                    AccNo = "4010";
                    SalHead = "11";
                    DEAColl = "Salary";
                    head = "Remote Allowance Voucher For the Month of";
                    break;
                }
            case "06":
                {
                    AccNo = "4010";
                    SalHead = "10";
                    DEAColl = "Salary";
                    head = "Additional Responsibilities Allowance Voucher For the Month of";
                    break;
                }
            case "07": 
                {
                    AccNo = "4011";
                    SalHead = "8";
                    DEAColl = "Salary";
                    head = "Overtime Voucher For the Month of";
                    break;
                }
            case "08":
                {
                    AccNo = "4011";
                    SalHead = "7";
                    DEAColl = "Salary";
                    head = "Child Education Allowance For the Month of";
                    break;
                }
            case "09":
                {
                    AccNo = "4011";
                    SalHead = "13,23";
                    DEAColl = "PF";
                    head = "PF Voucher For the Month of";
                    break;
                }
            case "10":
                {
                    AccNo = "4011";
                    SalHead = "16";
                    DEAColl = "PFLoan";
                    head = "PF Loan Voucher For the Month of";
                    break;
                }
            case "11":
                {
                    AccNo = "4010";
                    SalHead = "15";
                    DEAColl = "IT";
                    head = "Tax Voucher For the Month of";
                    break;
                }
            case "12":
                {
                    AccNo = "4010";
                    SalHead = "18";
                    DEAColl = "Salary";
                    head = "Other Deduction  Voucher For the Month of";
                    break;
                }

            case "13":
                {
                    AccNo = "4011";
                    SalHead = "0";
                    DEAColl = "Gratuity";
                    head = "Gratuity Process & Gratuity Distributions Voucher for Finance ";
                    break;
                }
            case "14":
                {
                    AccNo = "4011";
                    SalHead = "6";
                    DEAColl = "Salary";
                    head = "Other Allowance Voucher For the Month of ";
                    break;
                }
            case "15":
                {
                    AccNo = "4011";
                    SalHead = "14";
                    DEAColl = "L.W.P.";
                    head = "L.W.P. Voucher For the Month of ";
                    break;
                }
        }
        return head;
    }

    protected void GenerateReport()
    {
        dsPayroll_CrystalReport objDSCR = new dsPayroll_CrystalReport();
        ReportDoc = new ReportDocument();
        ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptPayroll.rpt");
        this.PassParameter(Common.ReturnFullMonthName(Session["Month"].ToString()), Session["Year"].ToString());
        ReportDoc.Load(ReportPath);
        DataTable dtTmp = GetPayrollReport();
        foreach (DataRow dRow in dtTmp.Rows)
        {
            DataRow nRow = objDSCR.dtPayslipPreparation.NewRow();
            for (int i = 0; i < 26; i++)
            {
                nRow[i] = dRow[i];
            }
            
            objDSCR.dtPayslipPreparation.Rows.Add(nRow);
        }
        objDSCR.dtPayslipPreparation.AcceptChanges();

        ReportDoc.SetDataSource(objDSCR.Tables["dtPayslipPreparation"]);
        CRV.ReportSource = ReportDoc;
    }


    public void PassParameter(string strMonthName, string strYear)
    {
        ParameterFields pFields = new ParameterFields();        
        ParameterField pMonthName = new ParameterField();
        ParameterField pYear = new ParameterField();
       
        //Generate ParameterDiscreteValue        
        ParameterDiscreteValue dvMonthName = new ParameterDiscreteValue();
        ParameterDiscreteValue dvYear = new ParameterDiscreteValue();

        //Adding ParameterDiscreteValue to ParameterField        
        pMonthName.Name = "pMonthName";
        dvMonthName.Value = strMonthName;
        pMonthName.CurrentValues.Add(dvMonthName);

        pYear.Name = "pYear";
        dvYear.Value = strYear;
        pYear.CurrentValues.Add(dvYear);

        //Adding Parameters to ParameterFields 
        pFields.Add(pMonthName);
        pFields.Add(pYear);

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

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the
        // specified ASP.NET server control at run time.
    }

    protected DataTable GetPayrollReport()
    {
        DataRow[] foundEmpSalHeadRow;
        string strEmpID = "";
        string strGenerateValue = "";

        int inGrossHeadCount = 0;
        bool EmpGrossColAdded = false;
        int inBenefitHeadCount = 0;
        int inDeductCount = 0;
        decimal dclSalHeadAmt = 0;

        DataTable dtSalaryHead = objPayrollMgr.SelectTotalSalHeadWithSeq(0);
        DataTable dtHeadCount = objPayRptMgr.GetHeadCount();
        DataRow[] founHCRows = dtHeadCount.Select("DISPLAYTYPE='B'");
        inBenefitHeadCount = Convert.ToInt32(founHCRows[0]["HEADCOUNT"]);
        founHCRows = null;
        founHCRows = dtHeadCount.Select("DISPLAYTYPE='D'");
        inDeductCount = Convert.ToInt32(founHCRows[0]["HEADCOUNT"]);

        dtGrossSalHead = objPayrollMgr.SelectGrossSalHead(0);
        dtEmpPayroll = objPayRptMgr.GetPayrollDataBankWise(Session["GenerateFor"].ToString(), Session["GenerateValue"].ToString(),
            Session["Month"].ToString(), Session["Year"].ToString(), Session["Bank"].ToString(),
            Session["SalaryType"].ToString(), Session["Group"].ToString());
        this.InitializeSummaryTable(dtSalaryHead.Rows.Count + 10);

        int i = 4;
        int j = 1;
        foreach (DataRow dEmpRow in dtEmpPayroll.Rows)
        {
            dclEmpBenefits = 0;
            dclEmpDeduct = 0;
            dclTotalSalary = 0;
            this.GetEmpBenefitsAmount(dtSalaryHead, dEmpRow["EMPID"].ToString().Trim(), dEmpRow["GROSSAMNT"].ToString());
            i = 4;
            if (strEmpID == dEmpRow["EMPID"].ToString().Trim())
            {
                continue;
            }
            DataRow nRow = dtPayrollSummary.NewRow();
            nRow[0] = Convert.ToString(j);
            nRow[1] = dEmpRow["EMPID"].ToString().Trim();
            nRow[2] = dEmpRow["FULLNAME"].ToString().Trim();
            nRow[3] = dEmpRow["JobTitle"].ToString().Trim();
            
            foreach (DataRow dSalRow in dtSalaryHead.Rows)
            {
                if (i - 4 == dtGrossSalHead.Rows.Count)
                {
                    nRow[i] = Common.RoundDecimal(dEmpRow["GROSSAMNT"].ToString(), 0);
                    i++;
                }
                if ((i - 4) - dtGrossSalHead.Rows.Count == inBenefitHeadCount + 1)
                {
                    nRow[i] = dclEmpBenefits.ToString();
                    i++;
                }
                if ((i - 4) - dtGrossSalHead.Rows.Count == inBenefitHeadCount + 2)
                {
                    nRow[i] = dclTotalSalary.ToString();
                    i++;

                    dclSalHeadAmt = 0;
                    dclSalHeadAmt = this.GetSalHeadAmt(dEmpRow["EMPID"].ToString().Trim(), dSalRow["SHEADID"].ToString().Trim());
                    if (dSalRow["DISPLAYTYPE"].ToString().Trim() == "D")
                    {
                        if (dclSalHeadAmt > 0)
                            dclSalHeadAmt = dclSalHeadAmt * -1;
                    }

                    nRow[i] = dclSalHeadAmt.ToString();
                    i++;
                }
                else
                {
                    dclSalHeadAmt = 0;
                    dclSalHeadAmt = this.GetSalHeadAmt(dEmpRow["EMPID"].ToString().Trim(), dSalRow["SHEADID"].ToString().Trim());
                    if (dSalRow["DISPLAYTYPE"].ToString().Trim() == "D")
                    {
                        if (dclSalHeadAmt > 0)
                            dclSalHeadAmt = dclSalHeadAmt * -1;
                    }

                    nRow[i] = dclSalHeadAmt.ToString();
                    i++;
                }
            }

            nRow[i] = dclEmpDeduct.ToString();
            i++;

            nRow[i] = Common.RoundDecimal(dEmpRow["NETPAY"].ToString(), 0);
            i++;

            nRow[i] = dEmpRow["BankName"].ToString().Trim();

            dtPayrollSummary.Rows.Add(nRow);
            dtPayrollSummary.AcceptChanges();
            strEmpID = dEmpRow["EMPID"].ToString().Trim();
            j++;
        }
        return dtPayrollSummary;
    }

    protected void InitializeSummaryTable(int inCol)
    {
        int i = 0;
        dtPayrollSummary = new DataTable();
        for (i = 0; i < inCol; i++)
        {
            dtPayrollSummary.Columns.Add(i.ToString());
        }
    }

    protected decimal GetSalHeadAmt(string strEmpID, string strSHeadID)
    {
        decimal dclSalHeadAmt = 0;
        DataRow[] foundRows = dtEmpPayroll.Select("EMPID='" + strEmpID + "' AND SHEADID=" + strSHeadID);
        if (foundRows.Length > 0)
        {
            dclSalHeadAmt = Convert.ToDecimal(foundRows[0]["PAYAMT"].ToString());
        }
        dclSalHeadAmt = Common.RoundDecimal(dclSalHeadAmt.ToString(), 0);
        return dclSalHeadAmt;
    }

    protected void GetEmpBenefitsAmount(DataTable dtSalHead, string strEmpID, string strGrossSal)
    {
        dclTotalSalary = Convert.ToDecimal(strGrossSal);
        decimal dclSalHeadAmt = 0;
        foreach (DataRow dRow in dtSalHead.Rows)
        {
            switch (dRow["DISPLAYTYPE"].ToString())
            {
                case "B":
                    dclEmpBenefits = dclEmpBenefits + this.GetSalHeadAmt(strEmpID, dRow["SHEADID"].ToString());
                    break;
                case "D":
                    dclSalHeadAmt = this.GetSalHeadAmt(strEmpID, dRow["SHEADID"].ToString());

                    if (dclSalHeadAmt > 0)
                        dclSalHeadAmt = dclSalHeadAmt * -1;

                    dclEmpDeduct = dclEmpDeduct + dclSalHeadAmt;
                    break;
            }
        }
        dclTotalSalary = dclTotalSalary + dclEmpBenefits;
        dclTotalSalary = Common.RoundDecimal(dclTotalSalary.ToString(), 0);
        dclEmpBenefits = Common.RoundDecimal(dclEmpBenefits.ToString(), 0);
        dclEmpDeduct = Common.RoundDecimal(dclEmpDeduct.ToString(), 0);
    }

    public DataTable CopyDataFromDt(DataTable dtDest, DataTable dtSource)
    {
        if (dtSource.Rows.Count > 0)
        {
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                dtDest.ImportRow(dtSource.Rows[i]);
            }
        }
        return dtDest;
    }

    public DataTable CopyDataTable(DataTable dtSource, int iRowsNeeded)
    {

        if (dtSource.Rows.Count >= iRowsNeeded)
        {
            // cloned to get the structure of source
            DataTable dtDestination = dtSource.Clone();
            for (int i = 0; i < iRowsNeeded; i++)
            {
                dtDestination.ImportRow(dtSource.Rows[i]);
            }
            return dtDestination;
        }
        else
            return dtSource;
    }
}
