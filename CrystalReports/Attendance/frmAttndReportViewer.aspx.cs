using System;
using System.Data;
using System.IO;
using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class frmAttndReportViewer : System.Web.UI.Page
{
    private ReportDocument ReportDoc;
    private string ReportPath = "";
    ReportManager rptManager = new ReportManager();
    DataTable MyDataTable = new DataTable();
    dsTimeSheet ds = new dsTimeSheet();

    private string LogoPath = System.Web.Configuration.WebConfigurationManager.AppSettings["LogoPath"];

    protected void Page_Init(object sender, EventArgs e)
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
            #region TimeSheet
            case "TSL":
                bool blnIsRound = false;
                string strEmpIdTS = Session["EmpId"].ToString().Trim(); 
                string strMonth = Session["Month"].ToString().Trim();
                string strYear = Session["Year"].ToString().Trim();
                if (blnIsRound == false)
                    ReportPath = Server.MapPath("~/CrystalReports/Attendance/rptTimeSheet.rpt");
                else
                    ReportPath = Server.MapPath("~/CrystalReports/Attendance/rptTimeSheetRound.rpt");
                ReportDoc.Load(ReportPath);

                DataTable dtTimeSheetEmpInfo = rptManager.Get_TimeSheetEmpInfo(strEmpIdTS, strMonth, strYear);

                DataTable dtTimeSheet = rptManager.Get_TimeSheetReport(strEmpIdTS, strMonth, strYear, blnIsRound);

                ReportManager objRM1 = new ReportManager();
                DataTable dtTimeSheetHoliday = objRM1.Get_TimeSheetReportForAbsent(strEmpIdTS, strMonth, strYear, "H");

                ReportManager objRM2 = new ReportManager();
                DataTable dtTimeSheetSick = objRM2.Get_TimeSheetReportForAbsent(strEmpIdTS, strMonth, strYear, "SL");

                ReportManager objRM3 = new ReportManager();
                DataTable dtTimeSheetUnPaid = objRM3.Get_TimeSheetReportForAbsent(strEmpIdTS, strMonth, strYear, "LW");

                ReportManager objRM4 = new ReportManager();
                DataTable dtTimeSheetVacation = objRM4.Get_TimeSheetReportForAbsent(strEmpIdTS, strMonth, strYear, "V");

                ReportManager objRM5 = new ReportManager();
                DataTable dtTimeSheetWH = objRM5.Get_TimeSheetReportForAbsent(strEmpIdTS, strMonth, strYear, "WH");

                if (dtTimeSheetEmpInfo.Rows.Count > 0)
                {
                    foreach (DataRow dRow in dtTimeSheetEmpInfo.Rows)
                    {
                        DataRow nRow = ds.dtTimeSheet.NewRow();

                        nRow["TIME_CODE"] = dRow["TIME_CODE"];
                        nRow["SOF_CODE"] = dRow["SOF_CODE"];
                        nRow["PROJECT_CODE"] = dRow["PROJECT_CODE"];
                        nRow["EmpId"] = dRow["EmpId"];
                        nRow["VYear"] = dRow["VYear"];
                        nRow["VMonth"] = dRow["VMonth"];
                        nRow["FullName"] = dRow["FullName"];
                        nRow["DesigName"] = dRow["DesigName"];
                        nRow["PostingPlaceName"] = dRow["PostingPlaceName"];

                        DataRow[] foundRows = dtTimeSheet.Select("EmpId='" + dRow["EmpId"].ToString().Trim() + "' AND SalarySourceId=" + dRow["SalarySourceId"].ToString().Trim());
                        if (foundRows.Length > 0)
                        {
                            nRow["1"] = GetZeroIfNull(foundRows[0]["1"].ToString());
                            nRow["2"] = GetZeroIfNull(foundRows[0]["2"].ToString());
                            nRow["3"] = GetZeroIfNull(foundRows[0]["3"].ToString());
                            nRow["4"] = GetZeroIfNull(foundRows[0]["4"].ToString());
                            nRow["5"] = GetZeroIfNull(foundRows[0]["5"].ToString());
                            nRow["6"] = GetZeroIfNull(foundRows[0]["6"].ToString());
                            nRow["7"] = GetZeroIfNull(foundRows[0]["7"].ToString());
                            nRow["8"] = GetZeroIfNull(foundRows[0]["8"].ToString());
                            nRow["9"] = GetZeroIfNull(foundRows[0]["9"].ToString());
                            nRow["10"] = GetZeroIfNull(foundRows[0]["10"].ToString());
                            nRow["11"] = GetZeroIfNull(foundRows[0]["11"].ToString());
                            nRow["12"] = GetZeroIfNull(foundRows[0]["12"].ToString());
                            nRow["13"] = GetZeroIfNull(foundRows[0]["13"].ToString());
                            nRow["14"] = GetZeroIfNull(foundRows[0]["14"].ToString());
                            nRow["15"] = GetZeroIfNull(foundRows[0]["15"].ToString());
                            nRow["16"] = GetZeroIfNull(foundRows[0]["16"].ToString());
                            nRow["17"] = GetZeroIfNull(foundRows[0]["17"].ToString());
                            nRow["18"] = GetZeroIfNull(foundRows[0]["18"].ToString());
                            nRow["19"] = GetZeroIfNull(foundRows[0]["19"].ToString());
                            nRow["20"] = GetZeroIfNull(foundRows[0]["20"].ToString());
                            nRow["21"] = GetZeroIfNull(foundRows[0]["21"].ToString());
                            nRow["22"] = GetZeroIfNull(foundRows[0]["22"].ToString());
                            nRow["23"] = GetZeroIfNull(foundRows[0]["23"].ToString());
                            nRow["24"] = GetZeroIfNull(foundRows[0]["24"].ToString());
                            nRow["25"] = GetZeroIfNull(foundRows[0]["25"].ToString());
                            nRow["26"] = GetZeroIfNull(foundRows[0]["26"].ToString());
                            nRow["27"] = GetZeroIfNull(foundRows[0]["27"].ToString());
                            nRow["28"] = GetZeroIfNull(foundRows[0]["28"].ToString());
                            nRow["29"] = GetZeroIfNull(foundRows[0]["29"].ToString());
                            nRow["30"] = GetZeroIfNull(foundRows[0]["30"].ToString());
                            nRow["31"] = GetZeroIfNull(foundRows[0]["31"].ToString());
                            ds.dtTimeSheet.Rows.Add(nRow);
                        }
                        else
                        {
                            nRow["1"] = "0";
                            nRow["2"] = "0";
                            nRow["3"] = "0";
                            nRow["4"] = "0";
                            nRow["5"] = "0";
                            nRow["6"] = "0";
                            nRow["7"] = "0";
                            nRow["8"] = "0";
                            nRow["9"] = "0";
                            nRow["10"] = "0";
                            nRow["11"] = "0";
                            nRow["12"] = "0";
                            nRow["13"] = "0";
                            nRow["14"] = "0";
                            nRow["15"] = "0";
                            nRow["16"] = "0";
                            nRow["17"] = "0";
                            nRow["18"] = "0";
                            nRow["19"] = "0";
                            nRow["20"] = "0";
                            nRow["21"] = "0";
                            nRow["22"] = "0";
                            nRow["23"] = "0";
                            nRow["24"] = "0";
                            nRow["25"] = "0";
                            nRow["26"] = "0";
                            nRow["27"] = "0";
                            nRow["28"] = "0";
                            nRow["29"] = "0";
                            nRow["30"] = "0";
                            nRow["31"] = "0";
                            ds.dtTimeSheet.Rows.Add(nRow);
                        }
                    }
                }

                ds.dtTimeSheet.AcceptChanges();

                //Holiday
                this.GetHoliday(dtTimeSheetHoliday, ds.dtTimeSheetHoliday);

                //Sick
                this.GetHoliday(dtTimeSheetSick, ds.dtTimeSheetSick);

                //Un Paid
                this.GetHoliday(dtTimeSheetUnPaid, ds.dtTimeSheetUnPaid);

                //Vacation
                this.GetHoliday(dtTimeSheetVacation, ds.dtTimeSheetVacation);

                //Work Home
                this.GetHoliday(dtTimeSheetWH, ds.dtTimeSheetWH);

                ReportDoc.SetDataSource(ds);
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRV.ReportSource = ReportDoc;
                break;
            #endregion
            case "DA":
                //Report no 1 : Attendance Report
                ReportPath = Server.MapPath("~/CrystalReports/Attendance/rptAttandance.rpt");
                //Label1.Text = ReportPath;
               
                ReportDoc.Load(ReportPath);

                MyDataTable = rptManager.Get_Attandance(Session["Flag"].ToString(), Session["USERID"].ToString(), Session["ISADMIN"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString(),
                    Session["DivisionId"].ToString(), Session["SBUId"].ToString(), Session["DeptId"].ToString(), Session["EmpId"].ToString(),
                    Session["ShiftID"].ToString(), Session["isClosed"].ToString());

                //if (Session["Flag"].ToString() == "E")
                //{
                //    DataTable dtDivSbuDept = rptManager.Select_DivSbuDept(Session["EmpId"].ToString());
                //    if (dtDivSbuDept.Rows.Count > 0)
                //    {
                //        foreach (DataRow tt in dtDivSbuDept.Rows)
                //        {
                //            this.PassParameter6(tt["DivisionName"].ToString(), tt["SBUName"].ToString(), tt["DeptName"].ToString().Trim(), Session["FromDate"].ToString(), Session["ToDate"].ToString(), "Attendance Report", Session["SRCSBU"].ToString(), tt["TypeStatus"].ToString().Trim(), tt["EmpSubTypeStatus"].ToString().Trim());
                //        }
                //    }
                //    else
                //        this.PassParameter6(Session["Div"].ToString(), Session["Sbu"].ToString(), Session["Dep"].ToString().Trim(), Session["FromDate"].ToString(), Session["ToDate"].ToString(), "Attendance Report", Session["SRCSBU"].ToString(), Session["EmpTypeStatusS"].ToString(), Session["EmpSubTypeStatusS"].ToString());
                //}
                //else
                //this.PassParameter6(Session["Div"].ToString(), Session["Sbu"].ToString(), Session["Dep"].ToString().Trim(), Session["FromDate"].ToString(), Session["ToDate"].ToString(), "Attendance Report", Session["SRCSBU"].ToString(), Session["EmpTypeStatusS"].ToString(), Session["EmpSubTypeStatusS"].ToString());
                //this.PassParameterPLAN(Session["Division"].ToString(), Session["SBU"].ToString(), Session["Dep"].ToString().Trim(), Session["FromDate"].ToString(), Session["ToDate"].ToString(), "");
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("pDIV", Session["Division"].ToString());
                ReportDoc.SetParameterValue("pSBU", Session["SBU"].ToString());
                ReportDoc.SetParameterValue("pDEP", Session["Dep"].ToString().Trim());
                ReportDoc.SetParameterValue("FromDate", Session["FromDate"].ToString());
                ReportDoc.SetParameterValue("ToDate", Session["ToDate"].ToString());
                ReportDoc.SetParameterValue("pHeader", "Attendance Report");
                CRV.Width = 10;                
                
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRV.ReportSource = ReportDoc;                
                break;
            case "AE":
                //Report no 2 : Employee wise attendance
                //Anol                
                ReportPath = Server.MapPath("~/CrystalReports/Attendance/rptAttndEmpWise.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.Get_MonthlyAttnd(Session["Flag"].ToString(), Session["USERID"].ToString(), Session["ISADMIN"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString(),
                     Session["DivisionId"].ToString(), Session["SBUId"].ToString(), Session["DeptId"].ToString(), Session["EmpId"].ToString(), 
                    Session["ShiftID"].ToString(), Session["isClosed"].ToString());

                string strPresent = CountStatus("P", MyDataTable);
                string strAbsent = CountStatus("A", MyDataTable);
                string strLeave = CountStatus("LV", MyDataTable);
                string strDelay = CountStatus("L", MyDataTable);
                string strWeekend = CountStatus("W", MyDataTable);
                string strHoliday = CountStatus("H", MyDataTable);

                //if (Session["Flag"].ToString() == "E")
                //{
                //    DataTable dtDivSbuDept = rptManager.Select_DivSbuDept(Session["EmpId"].ToString());
                //    if (dtDivSbuDept.Rows.Count > 0)
                //    {
                //        foreach (DataRow tt in dtDivSbuDept.Rows)
                //        {
                //            this.PassParameterAttndEmpWiseDBBL(tt["BranchName"].ToString(),tt["DivisionName"].ToString(),   Session["FromDate"].ToString(), Session["ToDate"].ToString(), "Attendance Employee Wise", strPresent, strAbsent, strLeave, strDelay, strWeekend, strHoliday);
                //        }
                //    }
                //    else
                //this.PassParameterAttndEmpWiseDBBL(Session["Branch"].ToString(), Session["Div"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString(), "Attendance Employee Wise", strPresent, strAbsent, strLeave, strDelay, strWeekend, strHoliday);
                //}
                //else
                this.PassParameterAttndEmpWisePLAN(Session["Division"].ToString(), Session["SBU"].ToString(), Session["Dep"].ToString().Trim(), Session["FromDate"].ToString(), Session["ToDate"].ToString(), "Attendance Employee Wise", strPresent, strAbsent, strLeave, strDelay, strWeekend, strHoliday);
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRV.ReportSource = ReportDoc;

                break;
            case "SumAttnd":
                //Report no 3 : Summery attendance
                //Anol
                string strEmpId = "";
                ReportPath = Server.MapPath("~/CrystalReports/Attendance/rptSummeryAttnd.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.Get_MonthlyAttnd(Session["Flag"].ToString(), Session["USERID"].ToString(), Session["ISADMIN"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString(),
                     Session["DivisionId"].ToString(), Session["SBUId"].ToString(), Session["DeptId"].ToString(), Session["EmpId"].ToString(),
                    Session["ShiftID"].ToString(), Session["isClosed"].ToString());

                //MyDataTable = rptManager.Get_MonthlyAttnd(Session["Flag"].ToString(), Session["USERID"].ToString(), Session["ISADMIN"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString(),
                //    Session["DivisionId"].ToString(), Session["SbuId"].ToString(), Session["DeptId"].ToString(), Session["SectionId"].ToString(), Session["EmpId"].ToString(), Session["LocId"].ToString(),
                //    Session["EmpTypeStatus"].ToString(), Session["EmpSubTypeStatus"].ToString(), Session["ShiftID"].ToString(), Session["isClosed"].ToString());

                dsAttndSum objDS = new dsAttndSum();
                foreach (DataRow dRow in MyDataTable.Rows)
                {
                    if (strEmpId != dRow["EMPID"].ToString().Trim())
                    {
                        strEmpId = dRow["EMPID"].ToString().Trim();
                        DataRow nRow = objDS.dtAttndSum.NewRow();
                        nRow["EMPID"] = dRow["EMPID"].ToString().Trim();
                        nRow["EMPNAME"] = dRow["FullName"].ToString().Trim();
                        nRow["Designation"] = dRow["JobTitle"].ToString().Trim();
                        nRow["Department"] = dRow["DeptName"].ToString().Trim();
                        nRow["Weekend"] = dRow["WEPackName"].ToString().Trim();
                        nRow["NoofDayAttended"] = CountStatusPresent(strEmpId, MyDataTable);
                        nRow["AttndOnWeekend"] = CountAttndOnWeekend("W", strEmpId, MyDataTable);
                        nRow["AppliedforLeave"] = CountStatusEmpWise("LV", strEmpId, MyDataTable);
                        nRow["NoofDayAbsent"] = CountStatusEmpWise("A", strEmpId, MyDataTable);
                        nRow["CPL"] = CountStatusEmpWiseleave("CPL", strEmpId, MyDataTable);
                        nRow["LWP"] = CountStatusEmpWiseleave("LWP", strEmpId, MyDataTable);
                        nRow["NoofWeekend"] = CountStatusEmpWise("W", strEmpId, MyDataTable);
                        nRow["Holiday"] = CountStatusEmpWise("H", strEmpId, MyDataTable);
                        objDS.dtAttndSum.Rows.Add(nRow);
                        objDS.dtAttndSum.AcceptChanges();
                    }
                }              
                this.PassParameterPLAN(Session["Division"].ToString(), Session["SBU"].ToString(), Session["Dep"].ToString().Trim(), Session["FromDate"].ToString(), Session["ToDate"].ToString(), "Summary Attendance Report");
                ReportDoc.SetDataSource(objDS);
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRV.ReportSource = ReportDoc;
                break;
            
            case "LR":
                //Report no 4 : late report
                ReportPath = Server.MapPath("~/CrystalReports/Attendance/rptAttandance.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.Get_LateReport(Session["Flag"].ToString(), Session["USERID"].ToString(), Session["ISADMIN"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString(),
                     Session["DivisionId"].ToString(), Session["SBUId"].ToString(), Session["DeptId"].ToString(), Session["EmpId"].ToString(), Session["ShiftID"].ToString(), Session["isClosed"].ToString());      
                this.PassParameterPLAN(Session["Division"].ToString(), Session["SBU"].ToString(), Session["Dep"].ToString().Trim(), Session["FromDate"].ToString(), Session["ToDate"].ToString(), "Late Report");

                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRV.ReportSource = ReportDoc;
                break;


            case "AR":
                //Report no 5 : Absent report
                ReportPath = Server.MapPath("~/CrystalReports/Attendance/rptAttandance.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.Get_AbsentReport(Session["Flag"].ToString(), Session["USERID"].ToString(), Session["ISADMIN"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString(),
                     Session["DivisionId"].ToString(), Session["SBUId"].ToString(), Session["DeptId"].ToString(), Session["EmpId"].ToString(), Session["ShiftID"].ToString(), Session["isClosed"].ToString());
              
                this.PassParameterPLAN(Session["Division"].ToString(), Session["SBU"].ToString(), Session["Dep"].ToString().Trim(), Session["FromDate"].ToString(), Session["ToDate"].ToString(), "Absent report");

                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRV.ReportSource = ReportDoc;

                break;
            case "IR":
                //Report no 6 : Incomplete report
                ReportPath = Server.MapPath("~/CrystalReports/Attendance/rptAttandance.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.Get_IncompleteReport(Session["Flag"].ToString(), Session["USERID"].ToString(), Session["ISADMIN"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString(),
                     Session["DivisionId"].ToString(), Session["SBUId"].ToString(), Session["DeptId"].ToString(), Session["EmpId"].ToString(), Session["ShiftID"].ToString(), Session["isClosed"].ToString());

                if (Session["Flag"].ToString() == "D")
                {
                    foreach (DataRow drow in MyDataTable.Rows)
                    {
                        if ((string.IsNullOrEmpty(drow["INTIME"].ToString()) == true) && (string.IsNullOrEmpty(drow["OUTTIME"].ToString()) == true))
                        {
                            drow.Delete();                            
                        }
                        else if ((string.IsNullOrEmpty(drow["INTIME"].ToString()) == false) && (string.IsNullOrEmpty(drow["OUTTIME"].ToString()) == false))
                        {
                            drow.Delete();                            
                        }
                    }
                    MyDataTable.AcceptChanges();
                }
                
                //if (Session["Flag"].ToString() == "E")
                //{
                //    DataTable dtDivSbuDept = rptManager.Select_DivSbuDept(Session["EmpId"].ToString());
                //    if (dtDivSbuDept.Rows.Count > 0)
                //    {
                //        foreach (DataRow tt in dtDivSbuDept.Rows)
                //        {
                //            this.PassParameter6(tt["DivisionName"].ToString(), tt["SBUName"].ToString(), tt["DeptName"].ToString().Trim(), Session["FromDate"].ToString(), Session["ToDate"].ToString(), "Incomplete report", Session["Sbu"].ToString(), tt["TypeStatus"].ToString().Trim(), tt["EmpSubTypeStatus"].ToString().Trim());
                //        }
                //    }
                //    else
                //        this.PassParameter6(Session["Div"].ToString(), Session["Sbu"].ToString(), Session["Dep"].ToString().Trim(), Session["FromDate"].ToString(), Session["ToDate"].ToString(), "Incomplete report", Session["Sbu"].ToString(), Session["EmpTypeStatusS"].ToString(), Session["EmpSubTypeStatusS"].ToString());
                //}
                //else
                this.PassParameterPLAN(Session["Division"].ToString(), Session["SBU"].ToString(), Session["Dep"].ToString().Trim(), Session["FromDate"].ToString(), Session["ToDate"].ToString(), "Incomplete report");

                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRV.ReportSource = ReportDoc;
                break;
            case "ED":
                //Report no 7 : Early Departure
                ReportPath = Server.MapPath("~/CrystalReports/Attendance/rptAttandance.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.Get_EarlyDepartureReport(Session["Flag"].ToString(), Session["USERID"].ToString(), Session["ISADMIN"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString(),
                    Session["DivisionId"].ToString(), Session["SBUId"].ToString(), Session["DeptId"].ToString(), Session["EmpId"].ToString(), Session["ShiftID"].ToString(), Session["isClosed"].ToString());
                
                this.PassParameterPLAN(Session["Division"].ToString(), Session["SBU"].ToString(), Session["Dep"].ToString().Trim(), Session["FromDate"].ToString(), Session["ToDate"].ToString(), "Early Departure Report");

                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRV.ReportSource = ReportDoc;
                break;
            case "DailyA":
                //Report no 1 : 4 Days Attendance Report
                ReportPath = Server.MapPath("~/CrystalReports/Attendance/rptDailyAttandance.rpt");
                strEmpId = "";
                ReportDoc.Load(ReportPath);

                DateTime dtFromDate = new DateTime();
                dtFromDate = Convert.ToDateTime(Common.ReturnDate(Session["FromDate"].ToString()));
                dtFromDate = dtFromDate.AddDays(-3);
                string strFromDate = Common.DisplayDate(dtFromDate.ToString()); //Common.DisplayDate(DateTime.Now.AddDays(-3).ToString());// 

                MyDataTable = rptManager.Get_Daily_Attandance(Session["Flag"].ToString(), strFromDate, Session["ToDate"].ToString(),
                     Session["DivisionId"].ToString(), Session["SBUId"].ToString(), Session["EmpId"].ToString(), 
                    Session["ShiftID"].ToString(), Session["isClosed"].ToString());

                ds4DayAttendance objDS2 = new ds4DayAttendance();
                DateTime dtDate = new DateTime();

                foreach (DataRow dRow in MyDataTable.Rows)
                {
                    if (strEmpId != dRow["EMPID"].ToString().Trim())
                    {
                        strEmpId = dRow["EMPID"].ToString().Trim();
                        DataRow nRow = objDS2.tblAttandance.NewRow();
                        nRow["EMPID"] = dRow["EMPID"].ToString().Trim();
                        nRow["Name"] = dRow["Name"].ToString().Trim();
                        nRow["Designation"] = dRow["Designation"].ToString().Trim();
                        nRow["DivisionName"] = dRow["DivisionName"].ToString().Trim();

                        if (GetTime(Common.DisplayDate(Common.ReturnDate(Session["FromDate"].ToString())), strEmpId, MyDataTable, "Y") != "")
                            nRow["InTime"] = GetTime(Common.DisplayDate(Common.ReturnDate(Session["FromDate"].ToString())), strEmpId, MyDataTable, "Y");
                        else
                            nRow["InTime"] = DBNull.Value;
                        nRow["InLocation"] = GetLocation(Common.DisplayDate(Common.ReturnDate(Session["FromDate"].ToString())), strEmpId, MyDataTable, "Y");

                        if (GetTime(Common.DisplayDate(Common.ReturnDate(Session["FromDate"].ToString())), strEmpId, MyDataTable, "N") != "")
                            nRow["OutTime"] = GetTime(Common.DisplayDate(Common.ReturnDate(Session["FromDate"].ToString())), strEmpId, MyDataTable, "N");
                        else
                            nRow["OutTime"] = DBNull.Value;
                        nRow["Status"] = GetAttnStatus(Common.DisplayDate(Common.ReturnDate(Session["FromDate"].ToString())), strEmpId, MyDataTable);
                        nRow["OutLocation"] = GetLocation(Common.DisplayDate(Common.ReturnDate(Session["FromDate"].ToString())), strEmpId, MyDataTable, "N");
                        nRow["LateHour"] = GetLtHour(Common.DisplayDate(Common.ReturnDate(Session["FromDate"].ToString())), strEmpId, MyDataTable);
                        nRow["Manual"] = GetIsManual(Common.DisplayDate(Common.ReturnDate(Session["FromDate"].ToString())), strEmpId, MyDataTable);

                        nRow["AttanDate"] = Common.DisplayDate(Common.ReturnDate(Session["FromDate"].ToString()));

                        dtDate = Convert.ToDateTime(Common.ReturnDate(Session["FromDate"].ToString())).AddDays(-1);
                        if (GetTime(Common.DisplayDate(dtDate.ToString()), strEmpId, MyDataTable, "Y") != "")
                            nRow["2InTime"] = GetTime(Common.DisplayDate(dtDate.ToString()), strEmpId, MyDataTable, "Y");
                        else
                            nRow["2InTime"] = DBNull.Value;

                        if (GetTime(Common.DisplayDate(dtDate.ToString()), strEmpId, MyDataTable, "N") != "")
                            nRow["2OutTime"] = GetTime(Common.DisplayDate(dtDate.ToString()), strEmpId, MyDataTable, "N");
                        else
                            nRow["2OutTime"] = DBNull.Value;
                        nRow["2Status"] = GetAttnStatus(Common.DisplayDate(dtDate.ToString()), strEmpId, MyDataTable);
                        nRow["2AttanDate"] = Common.DisplayDate(dtDate.ToString());

                        dtDate = dtDate.AddDays(-1);
                        if (GetTime(Common.DisplayDate(dtDate.ToString()), strEmpId, MyDataTable, "Y") != "")
                            nRow["3InTime"] = GetTime(Common.DisplayDate(dtDate.ToString()), strEmpId, MyDataTable, "Y");
                        else
                            nRow["3InTime"] = DBNull.Value;
                        if (GetTime(Common.DisplayDate(dtDate.ToString()), strEmpId, MyDataTable, "N") != "")
                            nRow["3OutTime"] = GetTime(Common.DisplayDate(dtDate.ToString()), strEmpId, MyDataTable, "N");
                        else
                            nRow["3OutTime"] = DBNull.Value;
                        nRow["3Status"] = GetAttnStatus(Common.DisplayDate(dtDate.ToString()), strEmpId, MyDataTable);
                        nRow["3AttanDate"] = Common.DisplayDate(dtDate.ToString());

                        dtDate = dtDate.AddDays(-1);
                        if (GetTime(Common.DisplayDate(dtDate.ToString()), strEmpId, MyDataTable, "Y") != "")
                            nRow["4InTime"] = GetTime(Common.DisplayDate(dtDate.ToString()), strEmpId, MyDataTable, "Y");
                        else
                            nRow["4InTime"] = DBNull.Value;
                        if (GetTime(Common.DisplayDate(dtDate.ToString()), strEmpId, MyDataTable, "N") != "")
                            nRow["4OutTime"] = GetTime(Common.DisplayDate(dtDate.ToString()), strEmpId, MyDataTable, "N");
                        else
                            nRow["4OutTime"] = DBNull.Value;
                        nRow["4Status"] = GetAttnStatus(Common.DisplayDate(dtDate.ToString()), strEmpId, MyDataTable);

                        nRow["4AttanDate"] = Common.DisplayDate(dtDate.ToString());

                        objDS2.tblAttandance.Rows.Add(nRow);
                        objDS2.tblAttandance.AcceptChanges();
                    }
                }
     CRV.Width = 10;
                ReportDoc.SetDataSource(objDS2);
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRV.ReportSource = ReportDoc;
                break;
            case "MonthlyA":
                //Report no 1 : Monthly Attendance Report
                ReportPath = Server.MapPath("~/CrystalReports/Attendance/rptMonthlyAttandance.rpt");
                strEmpId = "";
                ReportDoc.Load(ReportPath);
                DateTime dtFromDate2 = new DateTime();
                dtFromDate = Convert.ToDateTime(Common.ReturnDate(Session["FromDate"].ToString()));
                string strFromDate2 = Common.DisplayDate(dtFromDate2.ToString()); //Common.DisplayDate(DateTime.Now.AddDays(-3).ToString());// 

                MyDataTable = rptManager.Get_Monthly_Attandance(Session["Flag"].ToString(), strFromDate2, Session["ToDate"].ToString(),
                     Session["DivisionId"].ToString(), Session["SBUId"].ToString(), Session["EmpId"].ToString(), Session["ShiftID"].ToString(), Session["isClosed"].ToString());

                dsMonthlyAttandance objDS3 = new dsMonthlyAttandance();
                DateTime dtDate2 = new DateTime();

                foreach (DataRow dRow in MyDataTable.Rows)
                {
                    if (strEmpId != dRow["EMPID"].ToString().Trim())
                    {
                        strEmpId = dRow["EMPID"].ToString().Trim();
                        DataRow nRow = objDS3.tblMonthlyAttandance.NewRow();
                        nRow["EMPID"] = dRow["EMPID"].ToString().Trim();
                        nRow["Name"] = dRow["Name"].ToString().Trim();                        
                        nRow["DivisionName"] = dRow["DivisionName"].ToString().Trim();
                        nRow["St1"] = GetAttnStatus(Common.DisplayDate(Common.ReturnDate(Session["FromDate"].ToString())), strEmpId, MyDataTable);

                        dtDate2 = Convert.ToDateTime(Common.ReturnDate(Session["FromDate"].ToString())).AddDays(1);
                        nRow["St2"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St3"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St4"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St5"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St6"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St7"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St8"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St9"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St10"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St11"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St12"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St13"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St14"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St15"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St16"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St17"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St18"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St19"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St20"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St21"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St22"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St23"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St24"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St25"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St26"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St27"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St28"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St29"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St30"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);
                        dtDate2 = dtDate2.AddDays(1);
                        nRow["St31"] = GetAttnStatus(Common.DisplayDate(dtDate2.ToString()), strEmpId, MyDataTable);

                        objDS3.tblMonthlyAttandance.Rows.Add(nRow);
                        objDS3.tblMonthlyAttandance.AcceptChanges();
                    }
                }

                CRV.Width = 10;
                ReportDoc.SetDataSource(objDS3);
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRV.ReportSource = ReportDoc;
                break;

            case "InvOT":
                //Report no 8 :Overtime Sheet
                ReportPath = Server.MapPath("~/CrystalReports/Attendance/rptEmpWsOT.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.Get_OverTimeReport(Session["Flag"].ToString(), Session["USERID"].ToString(), Session["ISADMIN"].ToString(),
                    Session["FromDate"].ToString(), Session["ToDate"].ToString(), Session["BranchId"].ToString(), Session["DivisionId"].ToString(),
                    Session["EmpId"].ToString(), Session["isClosed"].ToString());
                
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRV.ReportSource = ReportDoc;
                break;
            case "EWOS":
                //Report No 15 : Employee wise OT summery
                ReportPath = Server.MapPath("~/CrystalReports/Attendance/rptEmpWiseOtSum.rpt");
                ReportDoc.Load(ReportPath);
                MyDataTable = rptManager.Get_OtEmpWise(Session["Flag"].ToString(), Session["USERID"].ToString(), Session["ISADMIN"].ToString(), Session["FromDate"].ToString(), Session["ToDate"].ToString(), Session["BranchId"].ToString(), Session["DivisionId"].ToString(),
                    Session["isClosed"].ToString(), Session["EmpId"].ToString());
                ReportDoc.SetDataSource(MyDataTable);
                ReportDoc.SetParameterValue("ComLogo", LogoPath);
                CRV.ReportSource = ReportDoc;
                break;
        }
    }
    

    public void PassParameterPLAN(string Div, string SBU, string dep, string FromDate, string ToDate, string ReportName)
    {
        ParameterFields pFields = new ParameterFields();
        ParameterField pfDiv = new ParameterField();
        ParameterField pfSBU = new ParameterField();
        ParameterField pfDep = new ParameterField();
        ParameterField pfFromDate = new ParameterField();
        ParameterField pfToDate = new ParameterField();
        ParameterField pfHeader = new ParameterField();
        //ParameterField pfEtype = new ParameterField();

        //Generate ParameterDiscreteValue
        ParameterDiscreteValue dvDiv = new ParameterDiscreteValue();
        ParameterDiscreteValue dvSBU = new ParameterDiscreteValue();
        ParameterDiscreteValue dvDep = new ParameterDiscreteValue();
        ParameterDiscreteValue dvFromDate = new ParameterDiscreteValue();
        ParameterDiscreteValue dvToDate = new ParameterDiscreteValue();
        ParameterDiscreteValue dvHeader = new ParameterDiscreteValue();
        //ParameterDiscreteValue dvEtype = new ParameterDiscreteValue();

        //Adding ParameterDiscreteValue to ParameterField

        pfDiv.Name = "pDiv";
        dvDiv.Value = Div;
        pfDiv.CurrentValues.Add(dvDiv);

        pfSBU.Name = "pSBU";
        dvSBU.Value = SBU;
        pfSBU.CurrentValues.Add(dvSBU);

        pfDep.Name = "pDEP";
        dvDep.Value = dep;
        pfDep.CurrentValues.Add(dvDep);

        pfFromDate.Name = "FromDate";
        dvFromDate.Value = FromDate;
        pfFromDate.CurrentValues.Add(dvFromDate);

        pfToDate.Name = "ToDate";
        dvToDate.Value = ToDate;
        pfToDate.CurrentValues.Add(dvToDate);

        pfHeader.Name = "pHeader";
        dvHeader.Value = ReportName;
        pfHeader.CurrentValues.Add(dvHeader);

        //pfEtype.Name = "pEType";
        //dvEtype.Value = empType;
        //pfEtype.CurrentValues.Add(dvEtype);

        //Adding Parameters to ParameterFields         
        pFields.Add(pfDiv);
        pFields.Add(pfSBU);
        pFields.Add(pfDep);
        pFields.Add(pfFromDate);
        pFields.Add(pfToDate);
        pFields.Add(pfHeader);
        //pFields.Add(pfEtype);

        //Passing ParameterFields to CrystalReportViewer
        CRV.ParameterFieldInfo = pFields;

    }

   
    public void PassParameterAttndEmpWisePLAN(string div, string SBU, string dep, string FromDate, string ToDate, string ReportName, string strPresent, string strAbsent, string strLeave, string strDelay, string strWeekend, string strHoliday)
    {
        //Generate ParameterFields 
        ParameterFields pFields = new ParameterFields();
        //Generate ParameterField        
        ParameterField pfDiv = new ParameterField();
        ParameterField pfSBU = new ParameterField();
        ParameterField pfDep = new ParameterField();
        ParameterField pfHeader = new ParameterField();
             
        ParameterField pfFromDate = new ParameterField();
        ParameterField pfToDate = new ParameterField();
        

        ParameterField pfP = new ParameterField();
        ParameterField pfA = new ParameterField();
        ParameterField pfL = new ParameterField();
        ParameterField pfD = new ParameterField();
        ParameterField pfW = new ParameterField();
        ParameterField pfH = new ParameterField();
        
        //Generate ParameterDiscreteValue        
        ParameterDiscreteValue dvDiv = new ParameterDiscreteValue();
        ParameterDiscreteValue dvSBU = new ParameterDiscreteValue();
        ParameterDiscreteValue dvDep = new ParameterDiscreteValue();
        ParameterDiscreteValue dvFromDate = new ParameterDiscreteValue();
        ParameterDiscreteValue dvToDate = new ParameterDiscreteValue();
        ParameterDiscreteValue dvHeader = new ParameterDiscreteValue();
        
        ParameterDiscreteValue dvP = new ParameterDiscreteValue();
        ParameterDiscreteValue dvA = new ParameterDiscreteValue();
        ParameterDiscreteValue dvL = new ParameterDiscreteValue();
        ParameterDiscreteValue dvD = new ParameterDiscreteValue();
        ParameterDiscreteValue dvW = new ParameterDiscreteValue();
        ParameterDiscreteValue dvH = new ParameterDiscreteValue();

        //Adding ParameterDiscreteValue to ParameterField
        pfDiv.Name = "pDIV";
        dvDiv.Value = div;
        pfDiv.CurrentValues.Add(dvDiv);

        pfSBU.Name = "pSBU";
        dvSBU.Value = SBU;
        pfSBU.CurrentValues.Add(dvSBU);

        pfDep.Name = "pDEP";
        dvDep.Value = dep;
        pfDep.CurrentValues.Add(dvDep);

        pfFromDate.Name = "FromDate";
        dvFromDate.Value = FromDate;
        pfFromDate.CurrentValues.Add(dvFromDate);

        pfToDate.Name = "ToDate";
        dvToDate.Value = ToDate;
        pfToDate.CurrentValues.Add(dvToDate);

        pfHeader.Name = "pHeader";
        dvHeader.Value = ReportName;
        pfHeader.CurrentValues.Add(dvHeader);

        pfP.Name = "pP";
        dvP.Value = strPresent;
        pfP.CurrentValues.Add(dvP);
        pfA.Name = "pA";
        dvA.Value = strAbsent;
        pfA.CurrentValues.Add(dvA);
        pfL.Name = "pL";
        dvL.Value = strLeave;
        pfL.CurrentValues.Add(dvL);
        pfD.Name = "pD";
        dvD.Value = strDelay;
        pfD.CurrentValues.Add(dvD);
        pfW.Name = "pW";
        dvW.Value = strWeekend;
        pfW.CurrentValues.Add(dvW);

        pfH.Name = "pH";
        dvH.Value = strHoliday;
        pfH.CurrentValues.Add(dvH);

        //Adding Parameters to ParameterFields     
        pFields.Add(pfDiv);
        pFields.Add(pfSBU);
        pFields.Add(pfDep);
        pFields.Add(pfFromDate);
        pFields.Add(pfToDate);
        pFields.Add(pfHeader);
        pFields.Add(pfP);
        pFields.Add(pfA);
        pFields.Add(pfL);
        pFields.Add(pfD);
        pFields.Add(pfW);
        pFields.Add(pfH);
        
        //Passing ParameterFields to CrystalReportViewer
        CRV.ParameterFieldInfo = pFields;

    }   
   
    protected void btnPrint_Click(object sender, EventArgs e)
    {

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

    protected string CountStatus(string strStatus, DataTable dt)

    {
        //useless function
        string strExpr = "Status='" + strStatus + "'";
        DataRow[] foundRows = dt.Select(strExpr);
        return Convert.ToString (foundRows.Length);
    }    
    protected string CountStatusPresent( string strEmpId,DataTable dt)
    {
        string strExpr = "Status in ('P','L','X') AND EMPID='" + strEmpId + "'";
        DataRow[] foundRows = dt.Select(strExpr);
        return Convert.ToString(foundRows.Length);
    }
    protected string CountAttndOnWeekend(string strStatus, string strEmpId, DataTable dt)
    {
        string strExpr = "Status in ('W','H') AND (SignInTime IS NOT NULL OR SignOutTime IS NOT NULL) AND SignInTime<>'1900-01-01' AND EMPID='" + strEmpId + "'";
        DataRow[] foundRows = dt.Select(strExpr);
        return Convert.ToString(foundRows.Length);
    }
    
    private Int32 CountRecord(string strExpr, DataTable dt)
    {
        DataRow[] foundRows = dt.Select(strExpr);
        return Convert.ToInt32(foundRows.Length );
    }
    private Int32 CountLeaveTypeRecord(string strExpr, DataTable dt)
    {
        DataRow[] foundRows = dt.Select(strExpr);
        return (Convert.ToInt32(foundRows[0]["LCarryOverd"]==DBNull.Value ? "0":foundRows[0]["LCarryOverd"])
            + Convert.ToInt32(foundRows[0]["lvPrevYearCarry"] == DBNull.Value ? "0" : foundRows[0]["lvPrevYearCarry"])
            + Convert.ToInt32( foundRows[0]["LEntitled"]==DBNull.Value ? "0":foundRows[0]["LEntitled"])
            - Convert.ToInt32(foundRows[0]["LeaveEnjoyed"]==DBNull.Value ? "0":foundRows[0]["LeaveEnjoyed"])
            - Convert.ToInt32(foundRows[0]["lvOpening"]==DBNull.Value ? "0":foundRows[0]["lvOpening"] )
            );
    }
    protected string CountStatusEmpWise(string strStatus, string strEmpId, DataTable dt)
    {
        string strExpr = "Status='" + strStatus + "' AND EMPID='" + strEmpId + "'";
        DataRow[] foundRows = dt.Select(strExpr); 
        return Convert.ToString(foundRows.Length);
    }
    protected string CountStatusEmpWiseleave(string strStatus, string strEmpId, DataTable dt)
    {
        string strExpr = "Status='LV' and LeaveFlag='" + strStatus + "' AND EMPID='" + strEmpId + "'";
        DataRow[] foundRows = dt.Select(strExpr);
        return Convert.ToString(foundRows.Length);
    }
    protected Int32 CountStregth(string strsbuname, string strdeptname, string strGrade, DataTable dt)
    {
        string strExpr = "sbuname='" + strsbuname + "' AND Deptname='" + strdeptname + "' AND GradeId='" + strGrade + "'";
        DataRow[] foundRows = dt.Select(strExpr);
        return Convert.ToInt32(foundRows.Length);
    }
    protected Int32 CountStregthTemp(string strsbuname, string strdeptname, DataTable dt)
    {
        string strExpr = "sbuname='" + strsbuname + "' AND Deptname='" + strdeptname + "' AND EmpStatus in('MT','NT')";
        DataRow[] foundRows = dt.Select(strExpr);
        return Convert.ToInt32(foundRows.Length);
    }
    protected Int32 CountStregthContrac(string strsbuname, string strdeptname, DataTable dt)
    {
        string strExpr = "sbuname='" + strsbuname + "' AND Deptname='" + strdeptname + "' AND EmpStatus in('MC','NC')";
        DataRow[] foundRows = dt.Select(strExpr);
        return Convert.ToInt32(foundRows.Length);
    }
    protected Int32 CountStregthMan(string strsbuname, string strdeptname, DataTable dt)
    {
        string strExpr = "sbuname='" + strsbuname + "' AND Deptname='" + strdeptname + "' AND Emptype in('M')";
        DataRow[] foundRows = dt.Select(strExpr);
        return Convert.ToInt32(foundRows.Length);
    }
    protected Int32 CountStregthNonMan(string strsbuname, string strdeptname, DataTable dt)
    {
        string strExpr = "sbuname='" + strsbuname + "' AND Deptname='" + strdeptname + "' AND  Emptype in('N')";
        DataRow[] foundRows = dt.Select(strExpr);
        return Convert.ToInt32(foundRows.Length);
    }

    protected Int32 CountStregthTotal(string strsbuname, string strdeptname, DataTable dt)
    {
        string strExpr = "sbuname='" + strsbuname + "' AND Deptname='" + strdeptname + "'";
        DataRow[] foundRows = dt.Select(strExpr);
        return Convert.ToInt32(foundRows.Length);
    }

    protected string GetTime(string FromDate, string strEmpId, DataTable dt, string InTime)
    {
        string strExpr = "AttanDate ='" + Common.ReturnDate(FromDate) + "' AND EMPID='" + strEmpId + "'";
        DataRow[] foundRows = dt.Select(strExpr);

        if (InTime == "Y" && foundRows.Length > 0)
        {
            if (foundRows[0]["InTime"].ToString() != "")
                return Convert.ToString(foundRows[0]["InTime"]);
            else
                return "";
        }
        else if (InTime == "N" && foundRows.Length > 0)
        {
            if (foundRows[0]["OutTime"].ToString() != "")
                return Convert.ToString(foundRows[0]["OutTime"]);
            else
                return "";
        }
        else
            return "";
    }

    protected string GetAttnStatus(string FromDate, string strEmpId, DataTable dt)
    {
        //DateTime dt1 = new DateTime();
        string strExpr = "AttanDate ='" + Common.ReturnDate(FromDate) + "' AND EMPID='" + strEmpId + "'";
        DataRow[] foundRows = dt.Select(strExpr);

        if (foundRows.Length > 0)
            return foundRows[0]["Status"].ToString();
        else
            return "";
    }
    protected string GetLocation(string FromDate, string strEmpId, DataTable dt, string InLoc)
    {
        string strExpr = "AttanDate ='" + Common.ReturnDate(FromDate) + "' AND EMPID='" + strEmpId + "'";
        DataRow[] foundRows = dt.Select(strExpr);

        if (InLoc == "Y" && foundRows.Length > 0)
        {
            if (foundRows[0]["InLocation"].ToString() != "")
                return foundRows[0]["InLocation"].ToString();
            else
                return "";
        }
        else if (InLoc == "N" && foundRows.Length > 0)
        {
            if (foundRows[0]["OutLocation"].ToString() != "")
                return foundRows[0]["OutLocation"].ToString();
            else
                return "";
        }
        else
            return "";
    }
    protected string GetLtHour(string FromDate, string strEmpId, DataTable dt)
    {
        //DateTime dt1 = new DateTime();
        string strExpr = "AttanDate ='" + Common.ReturnDate(FromDate) + "' AND EMPID='" + strEmpId + "'";
        DataRow[] foundRows = dt.Select(strExpr);

        if (foundRows.Length > 0)
            return foundRows[0]["LateHour"].ToString();
        else
            return "";
    }
    protected string GetIsManual(string FromDate, string strEmpId, DataTable dt)
    {
        //DateTime dt1 = new DateTime();
        string strExpr = "AttanDate ='" + Common.ReturnDate(FromDate) + "' AND EMPID='" + strEmpId + "'";
        DataRow[] foundRows = dt.Select(strExpr);

        if (foundRows.Length > 0 && foundRows[0]["Manual"].ToString() == "Y")

            return foundRows[0]["Manual"].ToString();
        else
            return "N";
    }
    private Int32 CountLeaveEntitled(string strExpr, DataTable dt)
    {
        DataRow[] foundRows = dt.Select(strExpr);
        if (foundRows.Length > 0)
            return (Convert.ToInt32(foundRows[0]["LCarryOverd"] == DBNull.Value ? "0" : foundRows[0]["LCarryOverd"])
                + Convert.ToInt32(foundRows[0]["lvPrevYearCarry"] == DBNull.Value ? "0" : foundRows[0]["lvPrevYearCarry"])
                + Convert.ToInt32(foundRows[0]["LEntitled"] == DBNull.Value ? "0" : foundRows[0]["LEntitled"]));
        else
            return 0;
    }
    private Int32 CountLeaveEnjoyed(string strExpr, DataTable dt)
    {
        DataRow[] foundRows = dt.Select(strExpr);
        if (foundRows.Length > 0)
            return (Convert.ToInt32(foundRows[0]["LeaveEnjoyed"] == DBNull.Value ? "0" : foundRows[0]["LeaveEnjoyed"])
                + Convert.ToInt32(foundRows[0]["lvOpening"] == DBNull.Value ? "0" : foundRows[0]["lvOpening"]));
        else
            return 0;
    }
    private Int32 CountLeaveBalance(string strExpr, DataTable dt)
    {
        DataRow[] foundRows = dt.Select(strExpr);
        if (foundRows.Length > 0)
            return (Convert.ToInt32(foundRows[0]["LCarryOverd"] == DBNull.Value ? "0" : foundRows[0]["LCarryOverd"])
                + Convert.ToInt32(foundRows[0]["lvPrevYearCarry"] == DBNull.Value ? "0" : foundRows[0]["lvPrevYearCarry"])
                + Convert.ToInt32(foundRows[0]["LEntitled"] == DBNull.Value ? "0" : foundRows[0]["LEntitled"])
                - Convert.ToInt32(foundRows[0]["LeaveEnjoyed"] == DBNull.Value ? "0" : foundRows[0]["LeaveEnjoyed"])
                - Convert.ToInt32(foundRows[0]["lvOpening"] == DBNull.Value ? "0" : foundRows[0]["lvOpening"])
                );
        else
            return 0;
    }


    protected void GenerateReport(string strEmpId, string strMonth, string strYear, bool blnIsRound)
    {
        ReportDoc = new ReportDocument();
        if (blnIsRound == false)
            ReportPath = Server.MapPath("~/CrystalReports/Attendance/rptTimeSheet.rpt");
        else
            ReportPath = Server.MapPath("~/CrystalReports/Attendance/rptTimeSheetRound.rpt");
        ReportDoc.Load(ReportPath);

        DataTable dtTimeSheetEmpInfo = rptManager.Get_TimeSheetEmpInfo(strEmpId, strMonth, strYear);

        DataTable dtTimeSheet = rptManager.Get_TimeSheetReport(strEmpId, strMonth, strYear, blnIsRound);

        ReportManager objRM1 = new ReportManager();
        DataTable dtTimeSheetHoliday = objRM1.Get_TimeSheetReportForAbsent(strEmpId, strMonth, strYear, "H");

        ReportManager objRM2 = new ReportManager();
        DataTable dtTimeSheetSick = objRM2.Get_TimeSheetReportForAbsent(strEmpId, strMonth, strYear, "SL");

        ReportManager objRM3 = new ReportManager();
        DataTable dtTimeSheetUnPaid = objRM3.Get_TimeSheetReportForAbsent(strEmpId, strMonth, strYear, "LW");

        ReportManager objRM4 = new ReportManager();
        DataTable dtTimeSheetVacation = objRM4.Get_TimeSheetReportForAbsent(strEmpId, strMonth, strYear, "V");

        ReportManager objRM5 = new ReportManager();
        DataTable dtTimeSheetWH = objRM5.Get_TimeSheetReportForAbsent(strEmpId, strMonth, strYear, "WH");

        if (dtTimeSheetEmpInfo.Rows.Count > 0)
        {
            foreach (DataRow dRow in dtTimeSheetEmpInfo.Rows)
            {
                DataRow nRow = ds.dtTimeSheet.NewRow();

                nRow["TIME_CODE"] = dRow["TIME_CODE"];
                nRow["SOF_CODE"] = dRow["SOF_CODE"];
                nRow["PROJECT_CODE"] = dRow["PROJECT_CODE"];
                nRow["EmpId"] = dRow["EmpId"];
                nRow["VYear"] = dRow["VYear"];
                nRow["VMonth"] = dRow["VMonth"];
                nRow["FullName"] = dRow["FullName"];
                nRow["DesigName"] = dRow["DesigName"];
                nRow["PostingPlaceName"] = dRow["PostingPlaceName"];

                DataRow[] foundRows = dtTimeSheet.Select("EmpId='" + dRow["EmpId"].ToString().Trim() + "' AND SalarySourceId=" + dRow["SalarySourceId"].ToString().Trim());
                if (foundRows.Length > 0)
                {
                    nRow["1"] = GetZeroIfNull(foundRows[0]["1"].ToString());
                    nRow["2"] = GetZeroIfNull(foundRows[0]["2"].ToString());
                    nRow["3"] = GetZeroIfNull(foundRows[0]["3"].ToString());
                    nRow["4"] = GetZeroIfNull(foundRows[0]["4"].ToString());
                    nRow["5"] = GetZeroIfNull(foundRows[0]["5"].ToString());
                    nRow["6"] = GetZeroIfNull(foundRows[0]["6"].ToString());
                    nRow["7"] = GetZeroIfNull(foundRows[0]["7"].ToString());
                    nRow["8"] = GetZeroIfNull(foundRows[0]["8"].ToString());
                    nRow["9"] = GetZeroIfNull(foundRows[0]["9"].ToString());
                    nRow["10"] = GetZeroIfNull(foundRows[0]["10"].ToString());
                    nRow["11"] = GetZeroIfNull(foundRows[0]["11"].ToString());
                    nRow["12"] = GetZeroIfNull(foundRows[0]["12"].ToString());
                    nRow["13"] = GetZeroIfNull(foundRows[0]["13"].ToString());
                    nRow["14"] = GetZeroIfNull(foundRows[0]["14"].ToString());
                    nRow["15"] = GetZeroIfNull(foundRows[0]["15"].ToString());
                    nRow["16"] = GetZeroIfNull(foundRows[0]["16"].ToString());
                    nRow["17"] = GetZeroIfNull(foundRows[0]["17"].ToString());
                    nRow["18"] = GetZeroIfNull(foundRows[0]["18"].ToString());
                    nRow["19"] = GetZeroIfNull(foundRows[0]["19"].ToString());
                    nRow["20"] = GetZeroIfNull(foundRows[0]["20"].ToString());
                    nRow["21"] = GetZeroIfNull(foundRows[0]["21"].ToString());
                    nRow["22"] = GetZeroIfNull(foundRows[0]["22"].ToString());
                    nRow["23"] = GetZeroIfNull(foundRows[0]["23"].ToString());
                    nRow["24"] = GetZeroIfNull(foundRows[0]["24"].ToString());
                    nRow["25"] = GetZeroIfNull(foundRows[0]["25"].ToString());
                    nRow["26"] = GetZeroIfNull(foundRows[0]["26"].ToString());
                    nRow["27"] = GetZeroIfNull(foundRows[0]["27"].ToString());
                    nRow["28"] = GetZeroIfNull(foundRows[0]["28"].ToString());
                    nRow["29"] = GetZeroIfNull(foundRows[0]["29"].ToString());
                    nRow["30"] = GetZeroIfNull(foundRows[0]["30"].ToString());
                    nRow["31"] = GetZeroIfNull(foundRows[0]["31"].ToString());
                    ds.dtTimeSheet.Rows.Add(nRow);
                }
                else
                {
                    nRow["1"] = "0";
                    nRow["2"] = "0";
                    nRow["3"] = "0";
                    nRow["4"] = "0";
                    nRow["5"] = "0";
                    nRow["6"] = "0";
                    nRow["7"] = "0";
                    nRow["8"] = "0";
                    nRow["9"] = "0";
                    nRow["10"] = "0";
                    nRow["11"] = "0";
                    nRow["12"] = "0";
                    nRow["13"] = "0";
                    nRow["14"] = "0";
                    nRow["15"] = "0";
                    nRow["16"] = "0";
                    nRow["17"] = "0";
                    nRow["18"] = "0";
                    nRow["19"] = "0";
                    nRow["20"] = "0";
                    nRow["21"] = "0";
                    nRow["22"] = "0";
                    nRow["23"] = "0";
                    nRow["24"] = "0";
                    nRow["25"] = "0";
                    nRow["26"] = "0";
                    nRow["27"] = "0";
                    nRow["28"] = "0";
                    nRow["29"] = "0";
                    nRow["30"] = "0";
                    nRow["31"] = "0";
                    ds.dtTimeSheet.Rows.Add(nRow);
                }
            }
        }
       
        ds.dtTimeSheet.AcceptChanges();

        //Holiday
        this.GetHoliday(dtTimeSheetHoliday, ds.dtTimeSheetHoliday);

        //Sick
        this.GetHoliday(dtTimeSheetSick, ds.dtTimeSheetSick);

        //Un Paid
        this.GetHoliday(dtTimeSheetUnPaid, ds.dtTimeSheetUnPaid);

        //Vacation
        this.GetHoliday(dtTimeSheetVacation, ds.dtTimeSheetVacation);

        //Work Home
        this.GetHoliday(dtTimeSheetWH, ds.dtTimeSheetWH);

        ReportDoc.SetDataSource(ds);
        CRV.ReportSource = ReportDoc;
    }

    private void GetHoliday(DataTable dtRecord, DataTable dtDS)
    {
        foreach (DataRow dRow in dtRecord.Rows)
        {
            DataRow nRow = dtDS.NewRow();
            nRow["EmpId"] = dRow["EmpId"];
            nRow["1"] = GetZeroIfNull(dRow["1"].ToString());
            nRow["2"] = GetZeroIfNull(dRow["2"].ToString());
            nRow["3"] = GetZeroIfNull(dRow["3"].ToString());
            nRow["4"] = GetZeroIfNull(dRow["4"].ToString());
            nRow["5"] = GetZeroIfNull(dRow["5"].ToString());
            nRow["6"] = GetZeroIfNull(dRow["6"].ToString());
            nRow["7"] = GetZeroIfNull(dRow["7"].ToString());
            nRow["8"] = GetZeroIfNull(dRow["8"].ToString());
            nRow["9"] = GetZeroIfNull(dRow["9"].ToString());
            nRow["10"] = GetZeroIfNull(dRow["10"].ToString());
            nRow["11"] = GetZeroIfNull(dRow["11"].ToString());
            nRow["12"] = GetZeroIfNull(dRow["12"].ToString());
            nRow["13"] = GetZeroIfNull(dRow["13"].ToString());
            nRow["14"] = GetZeroIfNull(dRow["14"].ToString());
            nRow["15"] = GetZeroIfNull(dRow["15"].ToString());
            nRow["16"] = GetZeroIfNull(dRow["16"].ToString());
            nRow["17"] = GetZeroIfNull(dRow["17"].ToString());
            nRow["18"] = GetZeroIfNull(dRow["18"].ToString());
            nRow["19"] = GetZeroIfNull(dRow["19"].ToString());
            nRow["20"] = GetZeroIfNull(dRow["20"].ToString());
            nRow["21"] = GetZeroIfNull(dRow["21"].ToString());
            nRow["22"] = GetZeroIfNull(dRow["22"].ToString());
            nRow["23"] = GetZeroIfNull(dRow["23"].ToString());
            nRow["24"] = GetZeroIfNull(dRow["24"].ToString());
            nRow["25"] = GetZeroIfNull(dRow["25"].ToString());
            nRow["26"] = GetZeroIfNull(dRow["26"].ToString());
            nRow["27"] = GetZeroIfNull(dRow["27"].ToString());
            nRow["28"] = GetZeroIfNull(dRow["28"].ToString());
            nRow["29"] = GetZeroIfNull(dRow["29"].ToString());
            nRow["30"] = GetZeroIfNull(dRow["30"].ToString());
            nRow["31"] = GetZeroIfNull(dRow["31"].ToString());
            dtDS.Rows.Add(nRow);
        }
        dtDS.AcceptChanges();
    }

    private decimal GetZeroIfNull(string strData)
    {
        if (string.IsNullOrEmpty(strData) == true)
            return 0;
        else
            return Convert.ToDecimal(strData);
    }

}
