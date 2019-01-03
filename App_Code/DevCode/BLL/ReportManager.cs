using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for ReportManager
/// </summary>
public class ReportManager
{
    //DBConnector dbConn;
    //public ReportManager()
    //{
    //    dbConn = new DBConnector();
    //}

    DBConnector objDC = new DBConnector();

    #region Time Sheet Report

    public DataTable Get_TimeSheetEmpInfo(string strEmpId, string strMonth, string strYear)
    {
        SqlCommand command = new SqlCommand();
        command = new SqlCommand("proc_TimeSheetEmpInfo");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_Month = command.Parameters.Add("Month", SqlDbType.BigInt);
        p_Month.Direction = ParameterDirection.Input;
        p_Month.Value = strMonth;

        SqlParameter p_Year = command.Parameters.Add("Year", SqlDbType.BigInt);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = strYear;

        objDC.CreateDSFromProc(command, "TimeSheetEmpInfo");
        return objDC.ds.Tables["TimeSheetEmpInfo"];
    }
    public DataTable Get_TimeSheetReport(string strEmpId, string strMonth, string strYear, bool blnIsRound)
    {
        SqlCommand command = new SqlCommand();
        if (blnIsRound == false)
            command = new SqlCommand("proc_TimeSheet");
        else
            command = new SqlCommand("proc_TimeSheetRound");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_Month = command.Parameters.Add("Month", SqlDbType.BigInt);
        p_Month.Direction = ParameterDirection.Input;
        p_Month.Value = strMonth;

        SqlParameter p_Year = command.Parameters.Add("Year", SqlDbType.BigInt);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = strYear;

        objDC.CreateDSFromProc(command, "tblTimeSheet");
        return objDC.ds.Tables["tblTimeSheet"];
    }

    public DataTable GetOtherTrainingDetails(string TrainingID, string FromDate, string ToDate)
    {
        SqlCommand command = new SqlCommand("proc_Get_OtherTrainingDetails");

        SqlParameter p_TrainingID = command.Parameters.Add("TrainingId", SqlDbType.Int);
        p_TrainingID.Direction = ParameterDirection.Input;
        p_TrainingID.Value = Convert.ToInt32(TrainingID);
        
        if (string.IsNullOrEmpty(FromDate) == true)
        {
            FromDate = "01-01-1900";
        }

        SqlParameter p_FromDate = command.Parameters.Add("StartDate", SqlDbType.DateTime);
        p_FromDate.Direction = ParameterDirection.Input;
        p_FromDate.Value = Common.ReturnDate(FromDate);

        if (string.IsNullOrEmpty(ToDate) == true)
        {
            ToDate = "01-01-2200";
        }
        SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
        p_ToDate.Direction = ParameterDirection.Input;
        p_ToDate.Value = Common.ReturnDate(ToDate);

        objDC.CreateDSFromProc(command, "tblOtherTrainingDetails");
        return objDC.ds.Tables["tblOtherTrainingDetails"];
    }

public DataTable Get_TimeSheetReportForAbsent(string strEmpId, string strMonth, string strYear, string strStatus)
    {
        if (objDC.ds.Tables["tblTimeSheetReportForAbsent"] != null)
        {
            objDC.ds.Tables["tblTimeSheetReportForAbsent"].Rows.Clear();
            objDC.ds.Tables["tblTimeSheetReportForAbsent"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_TimeSheetReportForAbsent");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_Month = command.Parameters.Add("Month", SqlDbType.BigInt);
        p_Month.Direction = ParameterDirection.Input;
        p_Month.Value = strMonth;

        SqlParameter p_Year = command.Parameters.Add("Year", SqlDbType.BigInt);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = strYear;

        SqlParameter p_Status = command.Parameters.Add("Status", SqlDbType.Char);
        p_Status.Direction = ParameterDirection.Input;
        p_Status.Value = strStatus;

        objDC.CreateDSFromProc(command, "tblTimeSheetReportForAbsent");
        return objDC.ds.Tables["tblTimeSheetReportForAbsent"];
    }

    #endregion
    public DataTable Get_EmpStregth( string UserId, string IsAdmin, 
        string divisionId, string sbuId, string deptId, string SectionId,  string LocId, 
   string EmpTypeStatus, string EmpSubTypeStatus, string isClosed)
    {
        SqlCommand command = new SqlCommand("proc_Get_EmpStregth");

        SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
        p_UserId.Direction = ParameterDirection.Input;        
        p_UserId.Value = UserId;

        SqlParameter p_IsAdmin = command.Parameters.Add("IsAdmin", SqlDbType.Char);
        p_IsAdmin.Direction = ParameterDirection.Input;
        p_IsAdmin.Value = IsAdmin;

        SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = Convert.ToInt32(divisionId);

        SqlParameter p_SbuID = command.Parameters.Add("SbuID", SqlDbType.BigInt);
        p_SbuID.Direction = ParameterDirection.Input;
        p_SbuID.Value = Convert.ToInt32(sbuId);

        SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
        p_DeptID.Direction = ParameterDirection.Input;
        p_DeptID.Value = Convert.ToInt32(deptId);

        SqlParameter p_SectionID = command.Parameters.Add("SectionID", SqlDbType.BigInt);
        p_SectionID.Direction = ParameterDirection.Input;
        p_SectionID.Value = Convert.ToInt32(SectionId);

        
        SqlParameter p_LocId = command.Parameters.Add("LocId", SqlDbType.BigInt);
        p_LocId.Direction = ParameterDirection.Input;
        p_LocId.Value = LocId;

        SqlParameter p_EmpTypeStatus = command.Parameters.Add("EmpTypeStatus", SqlDbType.Char);
        p_EmpTypeStatus.Direction = ParameterDirection.Input;
        p_EmpTypeStatus.Value = EmpTypeStatus;

        SqlParameter p_EmpSubTypeStatus = command.Parameters.Add("EmpSubTypeStatus", SqlDbType.Char);
        p_EmpSubTypeStatus.Direction = ParameterDirection.Input;
        p_EmpSubTypeStatus.Value = EmpSubTypeStatus;

       
        SqlParameter p_isClosed = command.Parameters.Add("isClosed", SqlDbType.Char);
        p_isClosed.Direction = ParameterDirection.Input;
        p_isClosed.Value = isClosed;

        objDC.CreateDSFromProc(command, "Get_EmpStregth");
        return objDC.ds.Tables["Get_EmpStregth"];
    }
    public DataTable Get_EmpStregth0()
    {
        SqlCommand command = new SqlCommand("proc_Get_EmpStregth0");
        objDC.CreateDSFromProc(command, "Get_EmpStregth0");
        return objDC.ds.Tables["Get_EmpStregth0"];
    }

    public DataTable Get_Attandance(string flag, string UserId, string IsAdmin, string fromDate, string toDate,
        string DivisionId, string SBUId, string DeptId, string empId, string AttnPolicyId, string isClosed)
    {
        SqlCommand command = new SqlCommand("proc_Get_AttendanceReport");

        SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
        p_Flag.Direction = ParameterDirection.Input;
        p_Flag.Value = flag;

        SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
        p_UserId.Direction = ParameterDirection.Input;
        if (string.IsNullOrEmpty(empId) == true)
            p_UserId.Value = "'" + UserId + "'";
        else
            p_UserId.Value = UserId;

        SqlParameter p_IsAdmin = command.Parameters.Add("IsAdmin", SqlDbType.Char);
        p_IsAdmin.Direction = ParameterDirection.Input;
        p_IsAdmin.Value = IsAdmin;

        SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
        p_FromDate.Direction = ParameterDirection.Input;
        p_FromDate.Value = Common.ReturnDateFormat_ddmmyyyy(fromDate, false);

        SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
        p_ToDate.Direction = ParameterDirection.Input;
        p_ToDate.Value = Common.ReturnDateFormat_ddmmyyyy(toDate, true);

        SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = DivisionId;

        SqlParameter p_SbuId = command.Parameters.Add("SBUId", SqlDbType.BigInt);
        p_SbuId.Direction = ParameterDirection.Input;
        p_SbuId.Value = SBUId;

        SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
        p_DeptID.Direction = ParameterDirection.Input;
        p_DeptID.Value = Convert.ToInt32(DeptId);

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = empId;
                
        SqlParameter p_ShiftId = command.Parameters.Add("AttnPolicyId", SqlDbType.BigInt);
        p_ShiftId.Direction = ParameterDirection.Input;
        p_ShiftId.Value = AttnPolicyId;

        SqlParameter p_isClosed = command.Parameters.Add("isClosed", SqlDbType.Char);
        p_isClosed.Direction = ParameterDirection.Input;
        p_isClosed.Value = isClosed;

        objDC.CreateDSFromProc(command, "tblAttandance");
        return objDC.ds.Tables["tblAttandance"];
    }

    //public DataTable Get_LateReport(string flag, string UserId, string IsAdmin, string fromDate, string toDate,
    //     string divisionId, string SBUId, string DeptId, string empId, string AttnPolicyId, string isClosed)
    //{
    //    SqlCommand command = new SqlCommand("proc_Get_LateReport");

    //    SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
    //    p_Flag.Direction = ParameterDirection.Input;
    //    p_Flag.Value = flag;

    //    SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
    //    p_UserId.Direction = ParameterDirection.Input;
    //    if (string.IsNullOrEmpty(empId) == true)
    //        p_UserId.Value = "'" + UserId + "'";
    //    else
    //        p_UserId.Value = UserId;

    //    SqlParameter p_IsAdmin = command.Parameters.Add("IsAdmin", SqlDbType.Char);
    //    p_IsAdmin.Direction = ParameterDirection.Input;
    //    p_IsAdmin.Value = IsAdmin;

    //    SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
    //    p_FromDate.Direction = ParameterDirection.Input;
    //    p_FromDate.Value = Common.ReturnDateFormat(fromDate, false);

    //    SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
    //    p_ToDate.Direction = ParameterDirection.Input;
    //    p_ToDate.Value = Common.ReturnDateFormat(toDate, true); 

    //    SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
    //    p_DivisionID.Direction = ParameterDirection.Input;
    //    p_DivisionID.Value = divisionId;

    //    SqlParameter p_SBUId = command.Parameters.Add("SBUId", SqlDbType.BigInt);
    //    p_SBUId.Direction = ParameterDirection.Input;
    //    p_SBUId.Value = SBUId;

    //    SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
    //    p_DeptID.Direction = ParameterDirection.Input;
    //    p_DeptID.Value = Convert.ToInt32(DeptId);
        
    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = empId;

    //    SqlParameter p_ShiftId = command.Parameters.Add("AttnPolicyId", SqlDbType.BigInt);
    //    p_ShiftId.Direction = ParameterDirection.Input;
    //    p_ShiftId.Value = AttnPolicyId;

    //    SqlParameter p_isClosed = command.Parameters.Add("isClosed", SqlDbType.Char);
    //    p_isClosed.Direction = ParameterDirection.Input;
    //    p_isClosed.Value = isClosed;

    //    objDC.CreateDSFromProc(command, "tblLateReport");
    //    return objDC.ds.Tables["tblLateReport"];
    //}

    //public DataTable Get_AbsentReport(string flag, string UserId, string IsAdmin, string fromDate, string toDate,
    //     string divisionId, string SBUId, string DeptId, string empId, string AttnPolicyId, string isClosed)
    //{
    //    SqlCommand command = new SqlCommand("proc_Get_AbsentReport");

    //    SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
    //    p_Flag.Direction = ParameterDirection.Input;
    //    p_Flag.Value = flag;

    //    SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
    //    p_UserId.Direction = ParameterDirection.Input;
    //    if (string.IsNullOrEmpty(empId) == true)
    //        p_UserId.Value = "'" + UserId + "'";
    //    else
    //        p_UserId.Value = UserId;

    //    SqlParameter p_IsAdmin = command.Parameters.Add("IsAdmin", SqlDbType.Char);
    //    p_IsAdmin.Direction = ParameterDirection.Input;
    //    p_IsAdmin.Value = IsAdmin;

    //    SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
    //    p_FromDate.Direction = ParameterDirection.Input;
    //    p_FromDate.Value = Common.ReturnDateFormat_ddmmyyyy(fromDate, false);//fromDate;

    //    SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
    //    p_ToDate.Direction = ParameterDirection.Input;
    //    p_ToDate.Value = Common.ReturnDateFormat_ddmmyyyy(toDate, true);

    //    SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
    //    p_DivisionID.Direction = ParameterDirection.Input;
    //    p_DivisionID.Value = divisionId;

    //    SqlParameter p_SBUId = command.Parameters.Add("SBUId", SqlDbType.BigInt);
    //    p_SBUId.Direction = ParameterDirection.Input;
    //    p_SBUId.Value = SBUId;

    //    SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
    //    p_DeptID.Direction = ParameterDirection.Input;
    //    p_DeptID.Value = Convert.ToInt32(DeptId);        

    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = empId;

    //    SqlParameter p_ShiftId = command.Parameters.Add("AttnPolicyId", SqlDbType.BigInt);
    //    p_ShiftId.Direction = ParameterDirection.Input;
    //    p_ShiftId.Value = AttnPolicyId;

    //    SqlParameter p_isClosed = command.Parameters.Add("isClosed", SqlDbType.Char);
    //    p_isClosed.Direction = ParameterDirection.Input;
    //    p_isClosed.Value = isClosed;

    //    objDC.CreateDSFromProc(command, "tblAbsentReport");
    //    return objDC.ds.Tables["tblAbsentReport"];
    //}

    //public DataTable Get_IncompleteReport(string flag, string UserId, string IsAdmin, string fromDate, string toDate,
    //     string divisionId, string SBUId, string DeptId, string empId, string AttnPolicyId, string isClosed)
    //{
    //    SqlCommand command = new SqlCommand("proc_Get_IncompleteReport");

    //    SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
    //    p_Flag.Direction = ParameterDirection.Input;
    //    p_Flag.Value = flag;

    //    SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
    //    p_UserId.Direction = ParameterDirection.Input;
    //    if (string.IsNullOrEmpty(empId) == true)
    //        p_UserId.Value = "'" + UserId + "'";
    //    else
    //        p_UserId.Value = UserId;

    //    SqlParameter p_IsAdmin = command.Parameters.Add("IsAdmin", SqlDbType.Char);
    //    p_IsAdmin.Direction = ParameterDirection.Input;
    //    p_IsAdmin.Value = IsAdmin;

    //    SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
    //    p_FromDate.Direction = ParameterDirection.Input;
    //    p_FromDate.Value = Common.ReturnDateFormat_ddmmyyyy(fromDate, false);//fromDate;

    //    SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
    //    p_ToDate.Direction = ParameterDirection.Input;
    //    p_ToDate.Value = Common.ReturnDateFormat_ddmmyyyy(toDate, true); ;

    //    SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
    //    p_DivisionID.Direction = ParameterDirection.Input;
    //    p_DivisionID.Value = divisionId;

    //    SqlParameter p_SBUId = command.Parameters.Add("SBUId", SqlDbType.BigInt);
    //    p_SBUId.Direction = ParameterDirection.Input;
    //    p_SBUId.Value = SBUId;

    //    SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
    //    p_DeptID.Direction = ParameterDirection.Input;
    //    p_DeptID.Value = Convert.ToInt32(DeptId);
                
    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = empId;
                
    //    SqlParameter p_ShiftId = command.Parameters.Add("AttnPolicyId", SqlDbType.BigInt);
    //    p_ShiftId.Direction = ParameterDirection.Input;
    //    p_ShiftId.Value = AttnPolicyId;

    //    SqlParameter p_isClosed = command.Parameters.Add("isClosed", SqlDbType.Char);
    //    p_isClosed.Direction = ParameterDirection.Input;
    //    p_isClosed.Value = isClosed;

    //    objDC.CreateDSFromProc(command, "tblIncompleteReport");
    //    return objDC.ds.Tables["tblIncompleteReport"];
    //}
    
    //public DataTable Get_EarlyDepartureReport(string flag, string UserId, string IsAdmin, string fromDate, string toDate,
    //      string divisionId, string SBUId, string DeptId, string empId, string AttnPolicyId, string isClosed)
    //{
    //    SqlCommand command = new SqlCommand("proc_Get_EarlyDepartureReport");

    //    SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
    //    p_Flag.Direction = ParameterDirection.Input;
    //    p_Flag.Value = flag;

    //    SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
    //    p_UserId.Direction = ParameterDirection.Input;
    //    if (string.IsNullOrEmpty(empId) == true)
    //        p_UserId.Value = "'" + UserId + "'";
    //    else
    //        p_UserId.Value = UserId;

    //    SqlParameter p_IsAdmin = command.Parameters.Add("IsAdmin", SqlDbType.Char);
    //    p_IsAdmin.Direction = ParameterDirection.Input;
    //    p_IsAdmin.Value = IsAdmin;

    //    SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
    //    p_FromDate.Direction = ParameterDirection.Input;
    //    p_FromDate.Value = Common.ReturnDateFormat_ddmmyyyy(fromDate, false);

    //    SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
    //    p_ToDate.Direction = ParameterDirection.Input;
    //    p_ToDate.Value = Common.ReturnDateFormat_ddmmyyyy(toDate, true);
        
    //    SqlParameter p_SBUId = command.Parameters.Add("SBUId", SqlDbType.BigInt);
    //    p_SBUId.Direction = ParameterDirection.Input;
    //    p_SBUId.Value = SBUId;

    //    SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
    //    p_DeptID.Direction = ParameterDirection.Input;
    //    p_DeptID.Value = Convert.ToInt32(DeptId);
        
    //    SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
    //    p_DivisionID.Direction = ParameterDirection.Input;
    //    p_DivisionID.Value = divisionId;

    //    SqlParameter p_ShiftId = command.Parameters.Add("AttnPolicyId", SqlDbType.BigInt);
    //    p_ShiftId.Direction = ParameterDirection.Input;
    //    p_ShiftId.Value = AttnPolicyId;

    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = empId;

    //    SqlParameter p_isClosed = command.Parameters.Add("isClosed", SqlDbType.Char);
    //    p_isClosed.Direction = ParameterDirection.Input;
    //    p_isClosed.Value = isClosed;

    //    objDC.CreateDSFromProc(command, "tblEDReport");
    //    return objDC.ds.Tables["tblEDReport"];
    //}

    //public DataTable Get_ShiftWiseEmpSum(string flag, string UserId, string IsAdmin, string fromDate, string toDate,
    //    string divisionId, string sbuId, string deptId, string SectionId, string empId, string LocId, string EmpTypeStatus, string EmpSubTypeStatus,string shift,string isClosed)
    //{
    //    SqlCommand command = new SqlCommand("proc_Get_ShiftWiseEmpSum");

    //    SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
    //    p_Flag.Direction = ParameterDirection.Input;
    //    p_Flag.Value = flag;

    //    SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
    //    p_UserId.Direction = ParameterDirection.Input;
    //    if (string.IsNullOrEmpty(empId) == true)
    //        p_UserId.Value = "'" + UserId + "'";
    //    else
    //        p_UserId.Value = UserId;

    //    SqlParameter p_IsAdmin = command.Parameters.Add("IsAdmin", SqlDbType.Char);
    //    p_IsAdmin.Direction = ParameterDirection.Input;
    //    p_IsAdmin.Value = IsAdmin;

    //    SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
    //    p_FromDate.Direction = ParameterDirection.Input;
    //    p_FromDate.Value = Common.ReturnDateFormat_ddmmyyyy(fromDate, false);

    //    SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
    //    p_ToDate.Direction = ParameterDirection.Input;
    //    p_ToDate.Value = Common.ReturnDateFormat_ddmmyyyy(toDate, true);

    //    SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
    //    p_DivisionID.Direction = ParameterDirection.Input;
    //    p_DivisionID.Value = Convert.ToInt32(divisionId);

    //    SqlParameter p_SbuID = command.Parameters.Add("SbuID", SqlDbType.BigInt);
    //    p_SbuID.Direction = ParameterDirection.Input;
    //    p_SbuID.Value = Convert.ToInt32(sbuId);

    //    SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
    //    p_DeptID.Direction = ParameterDirection.Input;
    //    p_DeptID.Value = Convert.ToInt32(deptId);

    //    SqlParameter p_SectionID = command.Parameters.Add("SectionID", SqlDbType.BigInt);
    //    p_SectionID.Direction = ParameterDirection.Input;
    //    p_SectionID.Value = Convert.ToInt32(SectionId);

    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = empId;

    //    SqlParameter p_LocId = command.Parameters.Add("LocId", SqlDbType.BigInt);
    //    p_LocId.Direction = ParameterDirection.Input;
    //    p_LocId.Value = LocId;

    //    SqlParameter p_EmpTypeStatus = command.Parameters.Add("EmpTypeStatus", SqlDbType.Char);
    //    p_EmpTypeStatus.Direction = ParameterDirection.Input;
    //    p_EmpTypeStatus.Value = EmpTypeStatus;

    //    SqlParameter p_EmpSubTypeStatus = command.Parameters.Add("EmpSubTypeStatus", SqlDbType.Char);
    //    p_EmpSubTypeStatus.Direction = ParameterDirection.Input;
    //    p_EmpSubTypeStatus.Value = EmpSubTypeStatus;

    //    SqlParameter p_shift = command.Parameters.Add("ShiftID", SqlDbType.Char);
    //    p_shift.Direction = ParameterDirection.Input;
    //    p_shift.Value = shift;

    //    SqlParameter p_isClosed = command.Parameters.Add("isClosed", SqlDbType.Char);
    //    p_isClosed.Direction = ParameterDirection.Input;
    //    p_isClosed.Value = isClosed;

    //    dbConn.CreateDSFromProc(command, "tblAttandance");
    //    return dbConn.ds.Tables["tblAttandance"];
    //}
    //public DataTable Get_MonthlyAttnd(string flag, string UserId, string IsAdmin, string fromDate, string toDate,
    //     string divisionId, string SBUId, string DeptId,string empId, string shift, string isClosed)
    //{
    //    SqlCommand command = new SqlCommand("proc_Get_AttndEmpWise");

    //    SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
    //    p_Flag.Direction = ParameterDirection.Input;
    //    p_Flag.Value = flag;

    //    SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
    //    p_UserId.Direction = ParameterDirection.Input;
    //    if (string.IsNullOrEmpty(empId) == true)
    //        p_UserId.Value = "'" + UserId + "'";
    //    else
    //        p_UserId.Value = UserId;

    //    SqlParameter p_IsAdmin = command.Parameters.Add("IsAdmin", SqlDbType.Char);
    //    p_IsAdmin.Direction = ParameterDirection.Input;
    //    p_IsAdmin.Value = IsAdmin;

    //    SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
    //    p_FromDate.Direction = ParameterDirection.Input;
    //    p_FromDate.Value = Common.ReturnDateFormat_ddmmyyyy(fromDate, false);

    //    SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
    //    p_ToDate.Direction = ParameterDirection.Input;
    //    p_ToDate.Value = Common.ReturnDateFormat_ddmmyyyy(toDate, true);

    //    SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
    //    p_DivisionID.Direction = ParameterDirection.Input;
    //    p_DivisionID.Value = divisionId;

    //    SqlParameter p_SBUID = command.Parameters.Add("SBUId", SqlDbType.BigInt);
    //    p_SBUID.Direction = ParameterDirection.Input;
    //    p_SBUID.Value = SBUId;

    //    SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
    //    p_DeptID.Direction = ParameterDirection.Input;
    //    p_DeptID.Value = Convert.ToInt32(DeptId);

    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = empId;

    //    SqlParameter p_ShiftId = command.Parameters.Add("AttnPolicyId", SqlDbType.BigInt);
    //    p_ShiftId.Direction = ParameterDirection.Input;
    //    p_ShiftId.Value = shift;

    //    SqlParameter p_isClosed = command.Parameters.Add("isClosed", SqlDbType.Char);
    //    p_isClosed.Direction = ParameterDirection.Input;
    //    p_isClosed.Value = isClosed;

    //    objDC.CreateDSFromProc(command, "tblMonthlyAttnd");
    //    return objDC.ds.Tables["tblMonthlyAttnd"];
    //}
    
    //public DataTable Get_Daily_Attandance(string flag, string fromDate, string toDate,
    //    string SBUId, string DivisionId, string empId, string AttnPolicyId, string isClosed)
    //{
    //    SqlCommand command = new SqlCommand("proc_Get_Daily_AttandanceReport");

    //    SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
    //    p_Flag.Direction = ParameterDirection.Input;
    //    p_Flag.Value = flag;

    //    SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
    //    p_FromDate.Direction = ParameterDirection.Input;
    //    p_FromDate.Value = Common.ReturnDateFormat_ddmmyyyy(fromDate, false);

    //    SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
    //    p_ToDate.Direction = ParameterDirection.Input;
    //    p_ToDate.Value = Common.ReturnDateFormat_ddmmyyyy(toDate, true);

    //    SqlParameter p_DivisionId = command.Parameters.Add("DivisionId", SqlDbType.BigInt);
    //    p_DivisionId.Direction = ParameterDirection.Input;
    //    p_DivisionId.Value = DivisionId;

    //    SqlParameter p_SBUId = command.Parameters.Add("SBUId", SqlDbType.BigInt);
    //    p_SBUId.Direction = ParameterDirection.Input;
    //    p_SBUId.Value = SBUId;

    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = empId;

    //    SqlParameter p_ShiftId = command.Parameters.Add("AttnPolicyId", SqlDbType.BigInt);
    //    p_ShiftId.Direction = ParameterDirection.Input;
    //    p_ShiftId.Value = AttnPolicyId;

    //    SqlParameter p_isClosed = command.Parameters.Add("isClosed", SqlDbType.Char);
    //    p_isClosed.Direction = ParameterDirection.Input;
    //    p_isClosed.Value = isClosed;

    //    objDC.CreateDSFromProc(command, "tbl4DayAttandance");
    //    return objDC.ds.Tables["tbl4DayAttandance"];
    //}

    //public DataTable Get_Monthly_Attandance(string flag, string fromDate, string toDate,
    //    string DivisionId, string SBUId, string empId, string AttnPolicyId, string isClosed)
    //{
    //    SqlCommand command = new SqlCommand("proc_Get_MonthlyAttandanceReport");

    //    SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
    //    p_Flag.Direction = ParameterDirection.Input;
    //    p_Flag.Value = flag;

    //    SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
    //    p_FromDate.Direction = ParameterDirection.Input;
    //    p_FromDate.Value = Common.ReturnDateFormat_ddmmyyyy(fromDate, false);

    //    SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
    //    p_ToDate.Direction = ParameterDirection.Input;
    //    p_ToDate.Value = Common.ReturnDateFormat_ddmmyyyy(toDate, true);

    //    SqlParameter p_DivisionId = command.Parameters.Add("DivisionId", SqlDbType.BigInt);
    //    p_DivisionId.Direction = ParameterDirection.Input;
    //    p_DivisionId.Value = DivisionId;

    //    SqlParameter p_SBUId = command.Parameters.Add("SBUId", SqlDbType.BigInt);
    //    p_SBUId.Direction = ParameterDirection.Input;
    //    p_SBUId.Value = SBUId;
       
    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = empId;

    //    SqlParameter p_ShiftId = command.Parameters.Add("AttnPolicyId", SqlDbType.BigInt);
    //    p_ShiftId.Direction = ParameterDirection.Input;
    //    p_ShiftId.Value = AttnPolicyId;

    //    SqlParameter p_isClosed = command.Parameters.Add("isClosed", SqlDbType.Char);
    //    p_isClosed.Direction = ParameterDirection.Input;
    //    p_isClosed.Value = isClosed;

    //    objDC.CreateDSFromProc(command, "tblMonthlyAttandance");
    //    return objDC.ds.Tables["tblMonthlyAttandance"];
    //}

    //public DataTable Get_MAmst(string flag, string UserId, string IsAdmin, string fromDate, string toDate, string divisionId, string sbuId, string deptId, string LocID, string EmpTypeStatus, string empId)
    //{
    //    SqlCommand command = new SqlCommand("proc_Get_MonthlyAttndMst");

    //    SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
    //    p_Flag.Direction = ParameterDirection.Input;
    //    p_Flag.Value = flag;

    //    SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
    //    p_UserId.Direction = ParameterDirection.Input;
    //    if (string.IsNullOrEmpty(empId) == true)
    //        p_UserId.Value = "'" + UserId + "'";
    //    else
    //        p_UserId.Value = UserId;

    //    SqlParameter p_IsAdmin = command.Parameters.Add("IsAdmin", SqlDbType.Char);
    //    p_IsAdmin.Direction = ParameterDirection.Input;
    //    p_IsAdmin.Value = IsAdmin;
        
    //    SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
    //    p_FromDate.Direction = ParameterDirection.Input;
    //    p_FromDate.Value = Common.ReturnDateFormat(fromDate, false);//fromDate;

    //    SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
    //    p_ToDate.Direction = ParameterDirection.Input;
    //    p_ToDate.Value = Common.ReturnDateFormat(toDate, true);

    //    SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
    //    p_DivisionID.Direction = ParameterDirection.Input;
    //    p_DivisionID.Value = Convert.ToInt32(divisionId);

    //    SqlParameter p_SbuID = command.Parameters.Add("SbuID", SqlDbType.BigInt);
    //    p_SbuID.Direction = ParameterDirection.Input;
    //    p_SbuID.Value = Convert.ToInt32(sbuId);

    //    SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
    //    p_DeptID.Direction = ParameterDirection.Input;
    //    p_DeptID.Value = Convert.ToInt32(deptId);

    //    SqlParameter p_LocID = command.Parameters.Add("LocID", SqlDbType.BigInt);
    //    p_LocID.Direction = ParameterDirection.Input;
    //    p_LocID.Value = Convert.ToInt32(LocID);

    //    SqlParameter p_EmpTypeStatus = command.Parameters.Add("EmpTypeStatus", SqlDbType.Char);
    //    p_EmpTypeStatus.Direction = ParameterDirection.Input;
    //    p_EmpTypeStatus.Value = EmpTypeStatus;

    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = empId;

    //    dbConn.CreateDSFromProc(command, "tblMAmst");
    //    return dbConn.ds.Tables["tblMAmst"];
    //}

    ////ashik 26.11
    //public DataTable Get_MAdet(string flag, string empId, string fromDate, string toDate)//, string divisionId, string sbuId, string deptId, 
    //{
    //    SqlCommand command = new SqlCommand("proc_Get_MonthlyAttndDet");

    //    SqlParameter p_Flag = command.Parameters.Add("p_Flag", SqlDbType.Char);
    //    p_Flag.Direction = ParameterDirection.Input;
    //    p_Flag.Value = flag;

    //    SqlParameter p_FromDate = command.Parameters.Add("p_FromDate", SqlDbType.DateTime);
    //    p_FromDate.Direction = ParameterDirection.Input;
    //    p_FromDate.Value = Common.ReturnDateFormat(fromDate, false);//fromDate;

    //    SqlParameter p_ToDate = command.Parameters.Add("p_ToDate", SqlDbType.DateTime);
    //    p_ToDate.Direction = ParameterDirection.Input;
    //    p_ToDate.Value = Common.ReturnDateFormat(toDate, true);

    //    SqlParameter p_EmpId = command.Parameters.Add("p_EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = empId;

    //    dbConn.CreateDSFromProc(command, "tblMAdet");
    //    return dbConn.ds.Tables["tblMAdet"];
    //}
    //*Sulata 12.11
    //public DataTable Get_OverTimeReport(string flag,string UserId, string IsAdmin,
    //    string fromDate, string toDate, string BranchId, string divisionId, 
    //    string empId, string isClosed)
    //{
    //    SqlCommand command = new SqlCommand("proc_Get_OverTime");

    //    SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
    //    p_Flag.Direction = ParameterDirection.Input;
    //    p_Flag.Value = flag;

    //    SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
    //    p_UserId.Direction = ParameterDirection.Input;
    //    if (string.IsNullOrEmpty(empId)==true )
    //        p_UserId.Value = "'" + UserId + "'";
    //    else
    //        p_UserId.Value = UserId;

    //    SqlParameter p_IsAdmin = command.Parameters.Add("IsAdmin", SqlDbType.Char);
    //    p_IsAdmin.Direction = ParameterDirection.Input;
    //    p_IsAdmin.Value = IsAdmin;

    //    SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
    //    p_FromDate.Direction = ParameterDirection.Input;        
    //    p_FromDate.Value = Common.ReturnDateFormat(fromDate, false);//fromDate;

    //    SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
    //    p_ToDate.Direction = ParameterDirection.Input;
    //    p_ToDate.Value = Common.ReturnDateFormat(toDate, true);

    //    SqlParameter p_BranchId = command.Parameters.Add("BranchId", SqlDbType.VarChar);
    //    p_BranchId.Direction = ParameterDirection.Input;
    //    p_BranchId.Value = BranchId;

    //    SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.VarChar);
    //    p_DivisionID.Direction = ParameterDirection.Input;
    //    p_DivisionID.Value = divisionId;

    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = empId;
    //    SqlParameter p_isClosed = command.Parameters.Add("isClosed", SqlDbType.Char);
    //    p_isClosed.Direction = ParameterDirection.Input;
    //    p_isClosed.Value = isClosed;

    //    objDC.CreateDSFromProc(command, "tblOverTime");
    //    return objDC.ds.Tables["tblOverTime"];
    //}
    //public DataTable Get_OverTimeSummaryReport(string flag, string UserId, string IsAdmin, string fromDate, string toDate,
    //    string divisionId, string sbuId, string deptId, string SectionId, string LocId, string EmpTypeStatus,string EmpSubTypeStatus)
    //{
    //    SqlCommand command = new SqlCommand("proc_Get_OverTimeSummary");

    //    SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
    //    p_Flag.Direction = ParameterDirection.Input;
    //    p_Flag.Value = flag;

    //    SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
    //    p_UserId.Direction = ParameterDirection.Input;
    //    p_UserId.Value = "'" + UserId + "'";        

    //    SqlParameter p_IsAdmin = command.Parameters.Add("IsAdmin", SqlDbType.Char);
    //    p_IsAdmin.Direction = ParameterDirection.Input;
    //    p_IsAdmin.Value = IsAdmin;

    //    SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
    //    p_FromDate.Direction = ParameterDirection.Input;
    //    p_FromDate.Value = Common.ReturnDateFormat(fromDate, false);//fromDate;

    //    SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
    //    p_ToDate.Direction = ParameterDirection.Input;
    //    p_ToDate.Value = Common.ReturnDateFormat(toDate, true); 

    //    //string strFromMon = GetMonth(fromDate); 
    //    //SqlParameter p_FromMon = command.Parameters.Add("FromMon", SqlDbType.BigInt);
    //    //p_FromMon.Direction = ParameterDirection.Input;
    //    //p_FromMon.Value = strFromMon;

    //    //string strFromYear = GetYear(fromDate);
    //    //SqlParameter p_FromYear = command.Parameters.Add("FromYear", SqlDbType.BigInt);
    //    //p_FromYear.Direction = ParameterDirection.Input;
    //    //p_FromYear.Value = strFromYear;

    //    //string strToMon = GetMonth(toDate);
    //    //SqlParameter p_ToMon = command.Parameters.Add("ToMon", SqlDbType.BigInt);
    //    //p_ToMon.Direction = ParameterDirection.Input;
    //    //p_ToMon.Value = strToMon;

    //    //string strToYear = GetYear(toDate); 
    //    //SqlParameter p_ToYear = command.Parameters.Add("ToYear", SqlDbType.BigInt);
    //    //p_ToYear.Direction = ParameterDirection.Input;
    //    //p_ToYear.Value = strToYear;

    //    SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
    //    p_DivisionID.Direction = ParameterDirection.Input;
    //    p_DivisionID.Value = Convert.ToInt32(divisionId);

    //    SqlParameter p_SbuID = command.Parameters.Add("SbuID", SqlDbType.BigInt);
    //    p_SbuID.Direction = ParameterDirection.Input;
    //    p_SbuID.Value = Convert.ToInt32(sbuId);

    //    SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
    //    p_DeptID.Direction = ParameterDirection.Input;
    //    p_DeptID.Value = Convert.ToInt32(deptId);

    //    SqlParameter p_SectionID = command.Parameters.Add("SectionID", SqlDbType.BigInt);
    //    p_SectionID.Direction = ParameterDirection.Input;
    //    p_SectionID.Value = Convert.ToInt32(SectionId);

    //    SqlParameter p_LocId = command.Parameters.Add("LocId", SqlDbType.BigInt);
    //    p_LocId.Direction = ParameterDirection.Input;
    //    p_LocId.Value = Convert.ToInt32(LocId);

    //    SqlParameter p_EmpTypeStatus = command.Parameters.Add("EmpTypeStatus", SqlDbType.Char);
    //    p_EmpTypeStatus.Direction = ParameterDirection.Input;
    //    p_EmpTypeStatus.Value = EmpTypeStatus;

    //    SqlParameter p_EmpSubTypeStatus = command.Parameters.Add("EmpSubTypeStatus", SqlDbType.Char);
    //    p_EmpSubTypeStatus.Direction = ParameterDirection.Input;
    //    p_EmpSubTypeStatus.Value = EmpSubTypeStatus;

    //    objDC.CreateDSFromProc(command, "tblOverTime");
    //    return objDC.ds.Tables["tblOverTime"];
    //}
    //public DataTable Get_OtEmpWise(string flag, string UserId, string IsAdmin, string fromDate, string toDate,
    //    string BranchId, string divisionId, string isClosed, string strempid)
    //{
    //    SqlCommand command = new SqlCommand("proc_Get_OtEmpWise");

    //    SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
    //    p_Flag.Direction = ParameterDirection.Input;
    //    p_Flag.Value = flag;

    //    SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
    //    p_UserId.Direction = ParameterDirection.Input;
    //    p_UserId.Value = "'" + UserId + "'";

    //    SqlParameter p_IsAdmin = command.Parameters.Add("IsAdmin", SqlDbType.Char);
    //    p_IsAdmin.Direction = ParameterDirection.Input;
    //    p_IsAdmin.Value = IsAdmin;

    //    SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
    //    p_FromDate.Direction = ParameterDirection.Input;
    //    p_FromDate.Value = Common.ReturnDateFormat(fromDate, false);//fromDate;

    //    SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
    //    p_ToDate.Direction = ParameterDirection.Input;
    //    p_ToDate.Value = Common.ReturnDateFormat(toDate, true);

    //    SqlParameter p_BranchId = command.Parameters.Add("BranchId", SqlDbType.VarChar);
    //    p_BranchId.Direction = ParameterDirection.Input;
    //    p_BranchId.Value = BranchId;

    //    SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.VarChar);
    //    p_DivisionID.Direction = ParameterDirection.Input;
    //    p_DivisionID.Value = divisionId;               

    //    SqlParameter p_isClosed = command.Parameters.Add("isClosed", SqlDbType.Char);
    //    p_isClosed.Direction = ParameterDirection.Input;
    //    p_isClosed.Value = isClosed;

    //    SqlParameter p_empid = command.Parameters.Add("empid", SqlDbType.Char);
    //    p_empid.Direction = ParameterDirection.Input;
    //    p_empid.Value = strempid;

    //    objDC.CreateDSFromProc(command, "tblOverTime");
    //    return objDC.ds.Tables["tblOverTime"];
    //}
    private string GetMonth(string strFrom)
    {
        string[] arInfo = new string[4];
        string[] arInfo2 = new string[4];
        string strMon;

        char[] splitter = { ' ' };
        arInfo = Common.str_split(strFrom, splitter);

        char[] splitter2 = { '/' };
        arInfo2 = Common.str_split(arInfo[0], splitter2);

        strMon = arInfo2[1];
        return strMon;
    }

    private string GetYear(string strFrom)
    {
        string[] arInfo = new string[4];
        string[] arInfo2 = new string[4];
        string strYear;

        char[] splitter = { ' ' };
        arInfo = Common.str_split(strFrom, splitter);

        char[] splitter2 = { '/' };
        arInfo2 = Common.str_split(arInfo[0], splitter2);

        strYear = arInfo2[2];
        return strYear;
    }

    #region Leave Reports
    //======================LeaveApp========================
    public DataTable Get_EmpLeaveBalance(string flag, string UserId, string IsAdmin,
    string BranchId, string divisionId, string empId, string isClosed)
    {
        SqlCommand command = new SqlCommand("proc_Get_EmpLeaveProfile");

        SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
        p_Flag.Direction = ParameterDirection.Input;
        p_Flag.Value = flag;

        SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
        p_UserId.Direction = ParameterDirection.Input;
        if (string.IsNullOrEmpty(empId) == true)
            p_UserId.Value = "'" + UserId + "'";
        else
            p_UserId.Value = UserId;

        SqlParameter p_IsAdmin = command.Parameters.Add("IsAdmin", SqlDbType.Char);
        p_IsAdmin.Direction = ParameterDirection.Input;
        p_IsAdmin.Value = IsAdmin;

        SqlParameter p_BranchId = command.Parameters.Add("BranchId", SqlDbType.VarChar);
        p_BranchId.Direction = ParameterDirection.Input;
        p_BranchId.Value = BranchId;

        SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.VarChar);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = divisionId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = empId;

        SqlParameter p_isClosed = command.Parameters.Add("isClosed", SqlDbType.Char);
        p_isClosed.Direction = ParameterDirection.Input;
        p_isClosed.Value = isClosed;

        objDC.CreateDSFromProc(command, "tblEmpLeaveProfile");
        return objDC.ds.Tables["tblEmpLeaveProfile"];
    }    
    
    public DataTable GetEmpLeaveBalance(string flag, string postingDivId, string empId, string fiscalYrId, string empTypeId)
    {
        SqlCommand command = new SqlCommand("proc_Get_EmpLeaveBalance");

        SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
        p_Flag.Direction = ParameterDirection.Input;
        p_Flag.Value = flag;

        SqlParameter p_DivisionID = command.Parameters.Add("PostingDivId", SqlDbType.BigInt);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = Convert.ToInt32(postingDivId);
        
        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = empId;

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = Convert.ToInt32(fiscalYrId);

        SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EmpTypeID.Direction = ParameterDirection.Input;
        p_EmpTypeID.Value = Convert.ToInt32(empTypeId);


        objDC.CreateDSFromProc(command, "tblEmpLeaveBalance");
        return objDC.ds.Tables["tblEmpLeaveBalance"];
    }


    public DataTable GetEmpLeaveDetails(string empId, string postingDivId, string fiscalYrId, string ltypeid, string sectorId, string deptId, string empStatus, string fromDate, string toDate, string empTypeId)
    {
        SqlCommand command = new SqlCommand("proc_Get_EmpLeaveDetails");

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = empId;

        SqlParameter p_DivisionID = command.Parameters.Add("PostingDivId", SqlDbType.BigInt);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = Convert.ToInt32(postingDivId);

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = Convert.ToInt32(fiscalYrId);

        SqlParameter p_LTypeId = command.Parameters.Add("LTypeId", SqlDbType.BigInt);
        p_LTypeId.Direction = ParameterDirection.Input;
        p_LTypeId.Value = Convert.ToInt32(ltypeid);

        SqlParameter p_SectorId = command.Parameters.Add("SectorId", SqlDbType.BigInt);
        p_SectorId.Direction = ParameterDirection.Input;
        p_SectorId.Value = Convert.ToInt32(sectorId);

        SqlParameter p_DeptId = command.Parameters.Add("DeptId", SqlDbType.BigInt);
        p_DeptId.Direction = ParameterDirection.Input;
        p_DeptId.Value = Convert.ToInt32(deptId);

        SqlParameter p_EmpStatus = command.Parameters.Add("EmpStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = empStatus;

        SqlParameter p_FromDate = command.Parameters.Add("FromDate", DBNull.Value);
        p_FromDate.Direction = ParameterDirection.Input;
        p_FromDate.IsNullable = true;
        if (string.IsNullOrEmpty(fromDate) == false)
            p_FromDate.Value = Common.ReturnDateFormat_ddmmyyyy(fromDate, false);
        else
            p_FromDate.Value = "";

        SqlParameter p_ToDate = command.Parameters.Add("ToDate", DBNull.Value);
        p_ToDate.Direction = ParameterDirection.Input;
        p_ToDate.IsNullable = true;
        if (string.IsNullOrEmpty(toDate) == false)
            p_ToDate.Value = Common.ReturnDateFormat_ddmmyyyy(toDate, true);
        else
            p_ToDate.Value = "";
        SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EmpTypeID.Direction = ParameterDirection.Input;
        p_EmpTypeID.Value = Convert.ToInt32(empTypeId);

        objDC.CreateDSFromProc(command, "tblEmpLeaveBalance");
        return objDC.ds.Tables["tblEmpLeaveBalance"];
    }

    public DataTable GetEmpMonthWiseLeave(string postingDivId, string empId, string fiscalYrId, string ltypeid, string empStatus, string empTypeId)
    {
        SqlCommand command = new SqlCommand("proc_Get_EmpMonthWiseLeaveEnjoyment");

        SqlParameter p_DivisionID = command.Parameters.Add("PostingDivId", SqlDbType.BigInt);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = Convert.ToInt32(postingDivId);


        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = empId;

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = Convert.ToInt32(fiscalYrId);


        SqlParameter p_LTypeId = command.Parameters.Add("LTypeId", SqlDbType.BigInt);
        p_LTypeId.Direction = ParameterDirection.Input;
        p_LTypeId.Value = Convert.ToInt32(ltypeid);

        SqlParameter p_EmpStatus = command.Parameters.Add("EmpStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = empStatus;

        SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EmpTypeID.Direction = ParameterDirection.Input;
        p_EmpTypeID.Value = Convert.ToInt32(empTypeId);


        objDC.CreateDSFromProc(command, "tblEmpMonthWiseLeaveBalance");
        return objDC.ds.Tables["tblEmpMonthWiseLeaveBalance"];
    }

    public DataTable GetEmpIndividualLeaveBalance(string postingDivId, string empId, string fiscalYrId, string empStatus, string empTypeId)
    {
        SqlCommand command = new SqlCommand("proc_Get_EmpIndividualLeaveBalance");

        SqlParameter p_DivisionID = command.Parameters.Add("PostingDivId", SqlDbType.BigInt);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = Convert.ToInt32(postingDivId);

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = empId;

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = Convert.ToInt32(fiscalYrId);

        SqlParameter p_EmpStatus = command.Parameters.Add("EmpStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = empStatus;

        SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EmpTypeID.Direction = ParameterDirection.Input;
        p_EmpTypeID.Value = Convert.ToInt32(empTypeId);

        objDC.CreateDSFromProc(command, "tblEmpIndividualLeaveBalance");
        return objDC.ds.Tables["tblEmpIndividualLeaveBalance"];
    }
    #endregion
    
    #region Training Reports
    public DataTable GetTrainingList(string fiscalYrId, string TrainingID, string LAreaId)
    {
        SqlCommand command = new SqlCommand("proc_Get_TrainingList");

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = Convert.ToInt32(fiscalYrId);

        SqlParameter p_LAreaId = command.Parameters.Add("LAreaId", SqlDbType.BigInt);
        p_LAreaId.Direction = ParameterDirection.Input;
        p_LAreaId.Value = Convert.ToInt32(LAreaId);

        SqlParameter p_TrainingID = command.Parameters.Add("TrainingID", SqlDbType.BigInt);
        p_TrainingID.Direction = ParameterDirection.Input;
        p_TrainingID.Value = Convert.ToInt32(TrainingID);

        objDC.CreateDSFromProc(command, "tblTrainingList");
        return objDC.ds.Tables["tblTrainingList"];
    }
    
    public DataTable GetTrainingListProgramAndLearningAreaWise(string DeptId, string LAreaId, string TrainingID)
    {
        SqlCommand command = new SqlCommand("proc_Get_TrainingListProgramAndLearningAreaWise");

        SqlParameter p_DeptId = command.Parameters.Add("DeptId", SqlDbType.BigInt);
        p_DeptId.Direction = ParameterDirection.Input;
        p_DeptId.Value = Convert.ToInt32(DeptId);

        SqlParameter p_LAreaId = command.Parameters.Add("LAreaId", SqlDbType.BigInt);
        p_LAreaId.Direction = ParameterDirection.Input;
        p_LAreaId.Value = Convert.ToInt32(LAreaId);
        
        SqlParameter p_TrainingID = command.Parameters.Add("TrainingID", SqlDbType.BigInt);
        p_TrainingID.Direction = ParameterDirection.Input;
        p_TrainingID.Value = Convert.ToInt32(TrainingID);

        objDC.CreateDSFromProc(command, "tblTrainingListProgramAndLearningAreaWise");
        return objDC.ds.Tables["tblTrainingListProgramAndLearningAreaWise"];
    }

    public DataTable GetMaleFemaleInTraining(string fiscalYrId, string TrainingID, string empStatus)
    {
        SqlCommand command = new SqlCommand("proc_Get_MaleFemaleInTraining");

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = Convert.ToInt32(fiscalYrId);


        SqlParameter p_TrainingID = command.Parameters.Add("TrainingID", SqlDbType.BigInt);
        p_TrainingID.Direction = ParameterDirection.Input;
        p_TrainingID.Value = Convert.ToInt32(TrainingID);

        SqlParameter p_EmpStatus = command.Parameters.Add("EmpStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = empStatus;

        objDC.CreateDSFromProc(command, "tblMaleFemaleInTraining");
        return objDC.ds.Tables["tblMaleFemaleInTraining"];
    }

    public DataTable GetTrainingCriteria(string empId, string fiscalYrId, string TrainingID, string empStatus)
    {
        SqlCommand command = new SqlCommand("proc_Get_NoOfTrainingCriteria");

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = empId;

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = Convert.ToInt32(fiscalYrId);


        SqlParameter p_TrainingID = command.Parameters.Add("TrainingID", SqlDbType.BigInt);
        p_TrainingID.Direction = ParameterDirection.Input;
        p_TrainingID.Value = Convert.ToInt32(TrainingID);

        SqlParameter p_EmpStatus = command.Parameters.Add("EmpStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = empStatus;

        objDC.CreateDSFromProc(command, "tblTrainingCriteria");
        return objDC.ds.Tables["tblTrainingCriteria"];
    }

    public DataTable GetOrientationTraining(string reportId, string empStatus, string sOTType,string fromDate, string toDate)
    {

        if (objDC.ds.Tables["tblOrientationTraining"] != null)
        {
            objDC.ds.Tables["tblOrientationTraining"].Clear();
            objDC.ds.Tables["tblOrientationTraining"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_GetOrientationTraining");

        SqlParameter p_DeptId = command.Parameters.Add("ReportId", SqlDbType.BigInt);
        p_DeptId.Direction = ParameterDirection.Input;
        p_DeptId.Value = Convert.ToInt32(reportId);

        SqlParameter p_EmpStatus = command.Parameters.Add("EmpStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = empStatus;

        SqlParameter p_OTType = command.Parameters.Add("OTType", SqlDbType.Char);
        p_OTType.Direction = ParameterDirection.Input;
        p_OTType.Value = sOTType;

        SqlParameter p_FromDate = command.Parameters.Add("FromDate", DBNull.Value);
        p_FromDate.Direction = ParameterDirection.Input;
        p_FromDate.IsNullable = true;
        if (string.IsNullOrEmpty(fromDate) == false)
            p_FromDate.Value = Common.ReturnDateFormat_ddmmyyyy(fromDate, false);
        else
            p_FromDate.Value = "";

        SqlParameter p_ToDate = command.Parameters.Add("ToDate", DBNull.Value);
        p_ToDate.Direction = ParameterDirection.Input;
        p_ToDate.IsNullable = true;
        if (string.IsNullOrEmpty(toDate) == false)
            p_ToDate.Value = Common.ReturnDateFormat_ddmmyyyy(toDate, true);
        else
            p_ToDate.Value = "";

        objDC.CreateDSFromProc(command, "tblOrientationTraining");
        return objDC.ds.Tables["tblOrientationTraining"];
    }

    public DataTable GetGradeWiseParticipants(string fiscalYrId, string empStatus)
    {
        SqlCommand command = new SqlCommand("proc_Get_GradeWiseParticipants");

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = Convert.ToInt32(fiscalYrId);

        SqlParameter p_EmpStatus = command.Parameters.Add("EmpStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = empStatus;

        objDC.CreateDSFromProc(command, "tblGradeWiseParticipants");
        return objDC.ds.Tables["tblGradeWiseParticipants"];
    }

    public DataTable GetMonthWiseTrainingParticipants(string fiscalYrId, string empStatus)
    {
        SqlCommand command = new SqlCommand("proc_Get_MonthWsTrainingParticipants");

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = Convert.ToInt32(fiscalYrId);

        SqlParameter p_EmpStatus = command.Parameters.Add("EmpStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = empStatus;


        objDC.CreateDSFromProc(command, "tblMonthWsTrainingParticipants");
        return objDC.ds.Tables["tblMonthWsTrainingParticipants"];
    }

    public DataTable GetIndividualdetailsOfParticipants(string fiscalYrId, string empId, string empStatus)   
    {
        SqlCommand command = new SqlCommand("proc_Get_IndividualDetailsOfParticipants");

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = Convert.ToInt32(fiscalYrId);

           SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = empId;

        SqlParameter p_EmpStatus = command.Parameters.Add("EmpStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = empStatus;


        objDC.CreateDSFromProc(command, "tblIndividualDetailsOfParticipants");
        return objDC.ds.Tables["tblIndividualDetailsOfParticipants"];
    }

    public DataTable GetLearningAreaWiseParticipants(string LAreaId, string fiscalYrId, string empStatus)
    {
        SqlCommand command = new SqlCommand("proc_Get_LearningAreaWiseParticipants");

        SqlParameter p_LAreaId = command.Parameters.Add("LAreaId", SqlDbType.BigInt);
        p_LAreaId.Direction = ParameterDirection.Input;
        p_LAreaId.Value = Convert.ToInt32(LAreaId);
        
        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = Convert.ToInt32(fiscalYrId);


        SqlParameter p_EmpStatus = command.Parameters.Add("EmpStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = empStatus;

        objDC.CreateDSFromProc(command, "tblLearningAreaWiseParticipants");
        return objDC.ds.Tables["tblLearningAreaWiseParticipants"];
    }

    public DataTable GetTrainingSlip(string fiscalYrId, string trainingId, string empId, string deptId, string trainingType, string empStatus)
    {

         if (objDC.ds.Tables["tblTrainingSlip"] != null)
        {
            objDC.ds.Tables["tblTrainingSlip"].Clear();
            objDC.ds.Tables["tblTrainingSlip"].Dispose();
        }
   
        SqlCommand command = new SqlCommand("proc_Get_TrainingSlip");

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = Convert.ToInt32(fiscalYrId);

        SqlParameter p_TrainingID = command.Parameters.Add("TrainingID", SqlDbType.BigInt);
        p_TrainingID.Direction = ParameterDirection.Input;
        p_TrainingID.Value = Convert.ToInt32(trainingId);


        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = empId;

        SqlParameter p_DeptId = command.Parameters.Add("DeptId", SqlDbType.BigInt);
        p_DeptId.Direction = ParameterDirection.Input;
        p_DeptId.Value = Convert.ToInt32(deptId);


        SqlParameter p_TrainType = command.Parameters.Add("TrainType", SqlDbType.VarChar);
        p_TrainType.Direction = ParameterDirection.Input;
        p_TrainType.Value = trainingType;

        SqlParameter p_EmpStatus = command.Parameters.Add("EmpStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = empStatus;

        objDC.CreateDSFromProc(command, "tblTrainingSlip");
        return objDC.ds.Tables["tblTrainingSlip"];
    }
    
    public DataTable GetTrainingBudgetDetails(string TrainingID, string FromDate, string ToDate)
    {

        SqlCommand command = new SqlCommand("proc_Get_TrainingBudgetDetails");

        SqlParameter p_TrainingID = command.Parameters.Add("TrainingId", SqlDbType.Int);
        p_TrainingID.Direction = ParameterDirection.Input;
        p_TrainingID.Value = Convert.ToInt32(TrainingID);

        if (string.IsNullOrEmpty(FromDate) == true)
        {
            FromDate = "01-01-1900";
        }

        SqlParameter p_FromDate = command.Parameters.Add("StartDate", SqlDbType.DateTime);
        p_FromDate.Direction = ParameterDirection.Input;
        p_FromDate.Value = Common.ReturnDate(FromDate);

        if (string.IsNullOrEmpty(ToDate) == true)
        {
            ToDate = "01-01-2200";
        }
        SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
        p_ToDate.Direction = ParameterDirection.Input;
        p_ToDate.Value = Common.ReturnDate(ToDate);

        objDC.CreateDSFromProc(command, "tblTrainingBudgetDetails");
        return objDC.ds.Tables["tblTrainingBudgetDetails"];
    }

    public DataTable GetTrainingYearlyPlan(string TrainingID, string ProjectID, string FromDate, string ToDate)
    {
        SqlCommand command = new SqlCommand("proc_get_TrainingYearlyPlan");

        SqlParameter p_TrainingID = command.Parameters.Add("TrainingId", SqlDbType.Int);
        p_TrainingID.Direction = ParameterDirection.Input;
        p_TrainingID.Value = Convert.ToInt32(TrainingID);


        SqlParameter p_ProjectID = command.Parameters.Add("ProjectId", SqlDbType.Int);
        p_ProjectID.Direction = ParameterDirection.Input;
        p_ProjectID.Value = Convert.ToInt32(ProjectID);

        if (string.IsNullOrEmpty(FromDate) == true)
        {
            FromDate = "01-01-1900";
        }

        SqlParameter p_FromDate = command.Parameters.Add("StartDate", SqlDbType.DateTime);
        p_FromDate.Direction = ParameterDirection.Input;
        p_FromDate.Value = Common.ReturnDate(FromDate);

        if (string.IsNullOrEmpty(ToDate) == true)
        {
            ToDate = "01-01-2200";
        }
        SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
        p_ToDate.Direction = ParameterDirection.Input;
        p_ToDate.Value = Common.ReturnDate(ToDate);

        objDC.CreateDSFromProc(command, "tblTrainingYearlyPlan");
        return objDC.ds.Tables["tblTrainingYearlyPlan"];
    }

    public DataTable GetCertificate(string trainId, string empId)
    {
        string strSQL = "select trl.TrainId,trl.TrainName," +
  " cast(day(tsc.StrDate) as char(2))+CASE WHEN datename(m,tsc.StrDate) = datename(m,tsc.EndDate)"+
   "and day(tsc.StrDate) = day(tsc.EndDate) THEN ' '+datename(m,tsc.EndDate)"+
   "WHEN datename(m,tsc.StrDate) = datename(m,tsc.EndDate)"+
   "and day(tsc.StrDate) != day(tsc.EndDate) THEN '-'+cast(day(tsc.EndDate) as char(2))+' '+datename(m,tsc.EndDate)"+
   "ELSE ' '+datename(m,tsc.StrDate)+'-'+cast(day(tsc.EndDate) as char(2))+' '+datename(m,tsc.EndDate) END +' '+cast(YEAR(tsc.EndDate) as char(4)) as ConductedFrom" +
    " ,pl.ProjectName,Upper(emp.FullName) as FullName,empsign1.FullName as SignName1,desigsign1.DesigName as DesigName1,empsign2.FullName as SignName2,desigsign2.DesigName as DesigName2,empsign3.FullName as SignName3,desigsign3.DesigName as DesigName3,empsign4.FullName as SignName4,desigsign4.DesigName as DesigName4 from TrTrainingList trl" +
  " inner join TrSchedule tsc on trl.TrainId=tsc.TrainId" +
  " inner join TrTrainResult trs on trs.ScheduleID=tsc.ScheduleID" +
  " inner join TrTrainResultDtls trd on trs.ResultId=trd.ResultId" +
  " inner join EmpInfo emp on trd.TraineeID=emp.EmpId" +
  " left join EmpInfo empsign1 on trs.SignID1=empsign1.EmpId" +
  " left join Designation desigsign1 on empsign1.DesigId=desigsign1.DesigId" +
  " left join EmpInfo empsign2 on trs.SignID2=empsign2.EmpId" +
  " left join Designation desigsign2 on empsign2.DesigId=desigsign2.DesigId" +
  " left join EmpInfo empsign3 on trs.SignID3=empsign3.EmpId" +
  " left join Designation desigsign3 on empsign3.DesigId=desigsign3.DesigId" +
  " left join EmpInfo empsign4 on trs.SignID4=empsign4.EmpId" +
  " left join Designation desigsign4 on empsign4.DesigId=desigsign4.DesigId" +
  " left join ProjectList pl on emp.ProjectId=pl.ProjectId" +
  " where trl.TrainId=@TrainId and trd.TraineeID=@EmpId";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_TrainId = command.Parameters.Add("TrainId", SqlDbType.Char);
        p_TrainId.Direction = ParameterDirection.Input;
        p_TrainId.Value = trainId;
        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = empId;

        objDC.CreateDT(command, "tblCertificate");
        return objDC.ds.Tables["tblCertificate"];
    }
    
    public DataTable GetTrainingMatrix()
    {
        SqlCommand command = new SqlCommand("proc_Rpt_TrainingMatrix");
        objDC.CreateDSFromProc(command, "tblTrainingMatrix");
        return objDC.ds.Tables["tblTrainingMatrix"];
    }

    public DataTable GetTrainingRequisition(string TrainingID, string ProjectID, string FromDate, string ToDate)
    {
        SqlCommand command = new SqlCommand("proc_Rpt_TrainingRequisition");

        SqlParameter p_TrainingID = command.Parameters.Add("TrainingId", SqlDbType.Int);
        p_TrainingID.Direction = ParameterDirection.Input;
        p_TrainingID.Value = Convert.ToInt32(TrainingID);


        SqlParameter p_ProjectID = command.Parameters.Add("ProjectId", SqlDbType.Int);
        p_ProjectID.Direction = ParameterDirection.Input;
        p_ProjectID.Value = Convert.ToInt32(ProjectID);

        if (string.IsNullOrEmpty(FromDate) == true)
        {
            FromDate = "01-01-1900";
        }

        SqlParameter p_FromDate = command.Parameters.Add("StartDate", SqlDbType.DateTime);
        p_FromDate.Direction = ParameterDirection.Input;
        p_FromDate.Value = Common.ReturnDate(FromDate);

        if (string.IsNullOrEmpty(ToDate) == true)
        {
            ToDate = "01-01-2200";
        }
        SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
        p_ToDate.Direction = ParameterDirection.Input;
        p_ToDate.Value = Common.ReturnDate(ToDate);

        objDC.CreateDSFromProc(command, "tblTrainingRequisition");
        return objDC.ds.Tables["tblTrainingRequisition"];
    }
    public DataTable GetInvitationLetter(string ScheduleId,string TrainId)
    {
        SqlCommand command = new SqlCommand("proc_Rpt_InvitationLetter");

        SqlParameter p_ScheduleId = command.Parameters.Add("ScheduleId", SqlDbType.Int);
        p_ScheduleId.Direction = ParameterDirection.Input;
        p_ScheduleId.Value = Convert.ToInt32(ScheduleId);

        SqlParameter p_TrainId = command.Parameters.Add("TrainId", SqlDbType.Int);
        p_TrainId.Direction = ParameterDirection.Input;
        p_TrainId.Value = Convert.ToInt32(TrainId);
        
        objDC.CreateDSFromProc(command, "tblInvitationLetter");
        return objDC.ds.Tables["tblInvitationLetter"];
    }
    public DataTable GetParticipantList(string ScheduleId, string TrainId)
    {
        SqlCommand command = new SqlCommand("proc_Rpt_ParticipantList");

        SqlParameter p_ScheduleId = command.Parameters.Add("ScheduleId", SqlDbType.Int);
        p_ScheduleId.Direction = ParameterDirection.Input;
        p_ScheduleId.Value = Convert.ToInt32(ScheduleId);

        SqlParameter p_TrainId = command.Parameters.Add("TrainId", SqlDbType.Int);
        p_TrainId.Direction = ParameterDirection.Input;
        p_TrainId.Value = Convert.ToInt32(TrainId);

        objDC.CreateDSFromProc(command, "tblParticipantList");
        return objDC.ds.Tables["tblParticipantList"];
    }
    #endregion

    #region Employee Reports
    public DataTable GetStateWiseParticipants(string fiscalYrId, string empStatus)
    {


        SqlCommand command = new SqlCommand("proc_Get_StateWiseParticipants");

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = Convert.ToInt32(fiscalYrId);

        SqlParameter p_EmpStatus = command.Parameters.Add("EmpStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = empStatus;


        objDC.CreateDSFromProc(command, "tblStateWiseParticipants");
        return objDC.ds.Tables["tblStateWiseParticipants"];
    }

    public DataTable proc_Get_EmpEligibleTrainDetails(string SalLocId, string EmpId, string TrainingID)
    {
        SqlCommand command = new SqlCommand("proc_Get_EmpEligibleTrainDetails");

        SqlParameter p_SalLocId = command.Parameters.Add("SalLocId", SqlDbType.Int);
        p_SalLocId.Direction = ParameterDirection.Input;
        p_SalLocId.Value = Convert.ToInt32(SalLocId);

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_TrainingID = command.Parameters.Add("TrainingId", SqlDbType.Int);
        p_TrainingID.Direction = ParameterDirection.Input;
        p_TrainingID.Value = Convert.ToInt32(TrainingID);

        objDC.CreateDSFromProc(command, "tblEmpEligibleTrainDetails");
        return objDC.ds.Tables["tblEmpEligibleTrainDetails"];
    }
    public DataTable GetDeptWiseTrainingDetails(string SalLocId, string TrainingID, string FundedBy,string FromDate, string ToDate)
    {
        SqlCommand command = new SqlCommand("proc_Get_DeptWiseTrainingDetails");

        SqlParameter p_SalLocId = command.Parameters.Add("SalLocId", SqlDbType.Int);
        p_SalLocId.Direction = ParameterDirection.Input;
        p_SalLocId.Value = Convert.ToInt32(SalLocId);

        SqlParameter p_TrainingID = command.Parameters.Add("TrainingId", SqlDbType.Int);
        p_TrainingID.Direction = ParameterDirection.Input;
        p_TrainingID.Value = Convert.ToInt32(TrainingID);

        SqlParameter p_FundedBy = command.Parameters.Add("FundedBy", SqlDbType.Int);
        p_FundedBy.Direction = ParameterDirection.Input;
        p_FundedBy.Value = Convert.ToInt32(FundedBy);

        if (string.IsNullOrEmpty(FromDate) == true)
        {
            FromDate = "01-01-1900";
        }
       
        SqlParameter p_FromDate = command.Parameters.Add("StartDate", SqlDbType.DateTime);
        p_FromDate.Direction = ParameterDirection.Input;
        p_FromDate.Value = Common.ReturnDate(FromDate);

        if(string.IsNullOrEmpty(ToDate)==true)
        {
            ToDate="01-01-2200";
        }

        SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
        p_ToDate.Direction = ParameterDirection.Input;
        p_ToDate.Value = Common.ReturnDate(ToDate);

        objDC.CreateDSFromProc(command, "tblDeptWiseTrainingDetails");
        return objDC.ds.Tables["tblDeptWiseTrainingDetails"];
    }
    public DataTable GetTrainingScheduleDetails(string TrainingID, string FromDate, string ToDate)
    {
        SqlCommand command = new SqlCommand("proc_Get_TrainingScheduleDetails");

        SqlParameter p_TrainingID = command.Parameters.Add("TrainingId", SqlDbType.Int);
        p_TrainingID.Direction = ParameterDirection.Input;
        p_TrainingID.Value = Convert.ToInt32(TrainingID);

        if (string.IsNullOrEmpty(FromDate) == true)
        {
            FromDate = "01-01-1900";
        }
       
        SqlParameter p_FromDate = command.Parameters.Add("StartDate", SqlDbType.DateTime);
        p_FromDate.Direction = ParameterDirection.Input;
        p_FromDate.Value = Common.ReturnDate(FromDate);

        if(string.IsNullOrEmpty(ToDate)==true)
        {
            ToDate="01-01-2200";
        }
        SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
        p_ToDate.Direction = ParameterDirection.Input;
        p_ToDate.Value = Common.ReturnDate(ToDate);

        objDC.CreateDSFromProc(command, "tblTrainingScheduleDetails");
        return objDC.ds.Tables["tblTrainingScheduleDetails"];
    }

    public DataTable GetEmployeeTrainingDetails(string empId, string TrainingID, string FromDate, string ToDate)
    {

        SqlCommand command = new SqlCommand("proc_Get_EmployeeTrainingDetails");

        SqlParameter p_TrainingID = command.Parameters.Add("TrainingId", SqlDbType.Int);
        p_TrainingID.Direction = ParameterDirection.Input;
        p_TrainingID.Value = Convert.ToInt32(TrainingID);

        SqlParameter p_EmpID = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = empId;

        if (string.IsNullOrEmpty(FromDate) == true)
        {
            FromDate = "01-01-1900";
        }

        SqlParameter p_FromDate = command.Parameters.Add("StartDate", SqlDbType.DateTime);
        p_FromDate.Direction = ParameterDirection.Input;
        p_FromDate.Value = Common.ReturnDate(FromDate);

        if (string.IsNullOrEmpty(ToDate) == true)
        {
            ToDate = "01-01-2200";
        }
        SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
        p_ToDate.Direction = ParameterDirection.Input;
        p_ToDate.Value = Common.ReturnDate(ToDate);

        objDC.CreateDSFromProc(command, "tblEmployeeTrainingDetails");
        return objDC.ds.Tables["tblEmployeeTrainingDetails"];
    }

    public DataTable GetQuarterWiseParticipants(string fiscalYrId, string empStatus)
    {


        SqlCommand command = new SqlCommand("proc_Get_QuarterWiseParticipants");

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = Convert.ToInt32(fiscalYrId);

        SqlParameter p_EmpStatus = command.Parameters.Add("EmpStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = empStatus;


        objDC.CreateDSFromProc(command, "tblQuarterWiseParticipants");
        return objDC.ds.Tables["tblQuarterWiseParticipants"];
    }


    public DataTable GetEmpoyeeList(string gradeId, string empId, string fullName, string gender, string sectorId, string deptId,
         string unitId, string postingDivId, string postingDistId, string desigId, string posByFuncId, string religionId, string empTypeID,
         string strTNTPosition, string fromDate, string toDate, string empStatus, string basicSal)
    {
        SqlCommand command = new SqlCommand("proc_Get_EmployeeList");

        SqlParameter p_GradeId = command.Parameters.Add("GradeId", SqlDbType.BigInt);
        p_GradeId.Direction = ParameterDirection.Input;
        p_GradeId.Value = Convert.ToInt32(gradeId);

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = empId;

        SqlParameter p_FullName = command.Parameters.Add("FullName", SqlDbType.VarChar);
        p_FullName.Direction = ParameterDirection.Input;
        p_FullName.Value = fullName;

        SqlParameter p_Gender = command.Parameters.Add("Gender", SqlDbType.Char);
        p_Gender.Direction = ParameterDirection.Input;
        p_Gender.Value = gender;

        SqlParameter p_DeptId = command.Parameters.Add("DeptId", SqlDbType.BigInt);
        p_DeptId.Direction = ParameterDirection.Input;
        p_DeptId.Value = Convert.ToInt32(deptId);

        SqlParameter p_PostingDivId = command.Parameters.Add("PostingDivId", SqlDbType.BigInt);
        p_PostingDivId.Direction = ParameterDirection.Input;
        p_PostingDivId.Value = Convert.ToInt32(postingDivId);

        SqlParameter p_DesigId = command.Parameters.Add("DesigId", SqlDbType.BigInt);
        p_DesigId.Direction = ParameterDirection.Input;
        p_DesigId.Value = Convert.ToInt32(desigId);

        SqlParameter p_ProjectId = command.Parameters.Add("ProjectID", SqlDbType.BigInt);
        p_ProjectId.Direction = ParameterDirection.Input;
        p_ProjectId.Value = Convert.ToInt32(posByFuncId);

        SqlParameter p_ReligionId = command.Parameters.Add("ReligionId", SqlDbType.BigInt);
        p_ReligionId.Direction = ParameterDirection.Input;
        p_ReligionId.Value = Convert.ToInt32(religionId);

        SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EmpTypeID.Direction = ParameterDirection.Input;
        p_EmpTypeID.Value = Convert.ToInt32(empTypeID);

        SqlParameter p_TNTPosition = command.Parameters.Add("TNTPosition", SqlDbType.Char);
        p_TNTPosition.Direction = ParameterDirection.Input;
        p_TNTPosition.Value = strTNTPosition;

        SqlParameter p_FromDate = command.Parameters.Add("FromDate", DBNull.Value);
        p_FromDate.Direction = ParameterDirection.Input;
        p_FromDate.IsNullable = true;
        if (string.IsNullOrEmpty(fromDate) == false)
            p_FromDate.Value = Common.ReturnDateFormat_ddmmyyyy(fromDate, false);
        else
            p_FromDate.Value = "";

        SqlParameter p_ToDate = command.Parameters.Add("ToDate", DBNull.Value);
        p_ToDate.Direction = ParameterDirection.Input;
        p_ToDate.IsNullable = true;
        if (string.IsNullOrEmpty(toDate) == false)
            p_ToDate.Value = Common.ReturnDateFormat_ddmmyyyy(toDate, true);
        else
            p_ToDate.Value = "";

        SqlParameter p_EmpStatus = command.Parameters.Add("EmpStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = empStatus;

        SqlParameter p_RbtnBasicValue = command.Parameters.Add("RbtnBasicValue", SqlDbType.Char);
        p_RbtnBasicValue.Direction = ParameterDirection.Input;
        p_RbtnBasicValue.Value = basicSal;

        objDC.CreateDSFromProc(command, "tblEmployeeList");
        return objDC.ds.Tables["tblEmployeeList"];
    }

        public DataTable GetEmpoyeeNGOforBureau(string sectorId, string deptId, string postingDivId, string postingDistId, string empStatus, string empTypeId)
        {
            SqlCommand command = new SqlCommand("proc_Get_EmployeeNGOBureau");

            SqlParameter p_DeptId = command.Parameters.Add("DeptId", SqlDbType.BigInt);
            p_DeptId.Direction = ParameterDirection.Input;
            p_DeptId.Value = Convert.ToInt32(deptId);

            SqlParameter p_PostingDivId = command.Parameters.Add("PostingDivId", SqlDbType.BigInt);
            p_PostingDivId.Direction = ParameterDirection.Input;
            p_PostingDivId.Value = Convert.ToInt32(postingDivId);

            SqlParameter p_EmpStatus = command.Parameters.Add("EmpStatus", SqlDbType.Char);
            p_EmpStatus.Direction = ParameterDirection.Input;
            p_EmpStatus.Value = empStatus;

            SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
            p_EmpTypeID.Direction = ParameterDirection.Input;
            p_EmpTypeID.Value = Convert.ToInt32(empTypeId);

            objDC.CreateDSFromProc(command, "tblEmployeeNGOBureau");
            return objDC.ds.Tables["tblEmployeeNGOBureau"];
        }

        public DataTable GetGroupWiseEmpList(string reportId, string groupId, string empTypeId)
    {

        if (objDC.ds.Tables["tblGetOrientationTraining"] != null)
        {
            objDC.ds.Tables["tblGetOrientationTraining"].Clear();
            objDC.ds.Tables["tblGetOrientationTraining"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_Get_GroupWiseEmpCount");

        SqlParameter p_DeptId = command.Parameters.Add("ReportId", SqlDbType.BigInt);
        p_DeptId.Direction = ParameterDirection.Input;
        p_DeptId.Value = Convert.ToInt32(reportId);


        SqlParameter p_GroupId = command.Parameters.Add("GroupId", SqlDbType.VarChar);
        p_GroupId.Direction = ParameterDirection.Input;
        p_GroupId.Value = groupId;

        SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EmpTypeID.Direction = ParameterDirection.Input;
        p_EmpTypeID.Value = Convert.ToInt32(empTypeId);



        objDC.CreateDSFromProc(command, "tblGetOrientationTraining");
        return objDC.ds.Tables["tblGetOrientationTraining"];
    }

        public DataTable GetBankAccountInfo(string empId, string salLocId, string postingDivId, string bankCode, string empStatus, string empTypeId)
        {

            SqlCommand command = new SqlCommand("proc_Get_BankAccountInfo");

            SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = empId;

            SqlParameter p_SalLocId = command.Parameters.Add("SalLocId", SqlDbType.BigInt);
            p_SalLocId.Direction = ParameterDirection.Input;
            p_SalLocId.Value = Convert.ToInt32(salLocId);

            SqlParameter p_PostingDivId = command.Parameters.Add("PostingDivId", SqlDbType.BigInt);
            p_PostingDivId.Direction = ParameterDirection.Input;
            p_PostingDivId.Value = Convert.ToInt32(postingDivId);

            SqlParameter p_BankCode = command.Parameters.Add("BankCode", SqlDbType.Char);
            p_BankCode.Direction = ParameterDirection.Input;
            p_BankCode.Value = bankCode;

            SqlParameter p_EmpStatus = command.Parameters.Add("EmpStatus", SqlDbType.Char);
            p_EmpStatus.Direction = ParameterDirection.Input;
            p_EmpStatus.Value = empStatus;

            SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
            p_EmpTypeID.Direction = ParameterDirection.Input;
            p_EmpTypeID.Value = Convert.ToInt32(empTypeId);

            objDC.CreateDSFromProc(command, "tblBankAccountInfo");
            return objDC.ds.Tables["tblBankAccountInfo"];
        }


        public DataTable GetSeparationStaffList(string gradeId, string empId, string deptId, string postingDivId, string separationType, string empTypeId, string rehireable, string fromDate, string toDate, string servicelengthFrom, string serviceLengthTo, string empStatus)
        {
            SqlCommand command = new SqlCommand("proc_Get_SeparationStaffList");

            SqlParameter p_GradeId = command.Parameters.Add("GradeId", SqlDbType.BigInt);
            p_GradeId.Direction = ParameterDirection.Input;
            p_GradeId.Value = Convert.ToInt32(gradeId);

            SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = empId;        

            SqlParameter p_DeptId = command.Parameters.Add("DeptId", SqlDbType.BigInt);
            p_DeptId.Direction = ParameterDirection.Input;
            p_DeptId.Value = Convert.ToInt32(deptId);          
         
            SqlParameter p_PostingDivId = command.Parameters.Add("PostingDivId", SqlDbType.BigInt);
            p_PostingDivId.Direction = ParameterDirection.Input;
            p_PostingDivId.Value = Convert.ToInt32(postingDivId);

            SqlParameter p_SeparateTypeId = command.Parameters.Add("SeparateTypeId", SqlDbType.BigInt);
            p_SeparateTypeId.Direction = ParameterDirection.Input;
            p_SeparateTypeId.Value = Convert.ToInt32(separationType);

            SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
            p_EmpTypeID.Direction = ParameterDirection.Input;
            p_EmpTypeID.Value = Convert.ToInt32(empTypeId);

            SqlParameter p_IsNotRehirable = command.Parameters.Add("IsNotRehirable", SqlDbType.Char);
            p_IsNotRehirable.Direction = ParameterDirection.Input;
            p_IsNotRehirable.Value = rehireable;

            SqlParameter p_FromDate = command.Parameters.Add("FromDate", DBNull.Value);
            p_FromDate.Direction = ParameterDirection.Input;
            p_FromDate.IsNullable = true;
            if (string.IsNullOrEmpty(fromDate) == false)
                p_FromDate.Value = Common.ReturnDateFormat_ddmmyyyy(fromDate, false);
            else
                p_FromDate.Value = "";

            SqlParameter p_ToDate = command.Parameters.Add("ToDate", DBNull.Value);
            p_ToDate.Direction = ParameterDirection.Input;
            p_ToDate.IsNullable = true;
            if (string.IsNullOrEmpty(toDate) == false)
                p_ToDate.Value = Common.ReturnDateFormat_ddmmyyyy(toDate, true);
            else
                p_ToDate.Value = "";

            SqlParameter p_ServiceLengthFrom = command.Parameters.Add("FromServiceLength", SqlDbType.Decimal);
            p_ServiceLengthFrom.Direction = ParameterDirection.Input;
            p_ServiceLengthFrom.Value = Convert.ToDecimal(servicelengthFrom);

            SqlParameter p_ServiceLengthTo = command.Parameters.Add("ToServiceLength", SqlDbType.Decimal);
            p_ServiceLengthTo.Direction = ParameterDirection.Input;
            p_ServiceLengthTo.Value = Convert.ToDecimal(serviceLengthTo);

            SqlParameter p_EmpStatus = command.Parameters.Add("EmpStatus", SqlDbType.Char);
            p_EmpStatus.Direction = ParameterDirection.Input;
            p_EmpStatus.Value = empStatus;

            objDC.CreateDSFromProc(command, "tblSeparationStaffList");
            return objDC.ds.Tables["tblSeparationStaffList"];
        }

        public DataTable GetEmpEmergencyContactList(string empId, string fullName, string desigId, string deptId, string sectorId, string empTypeId)
        {

            SqlCommand command = new SqlCommand("proc_Get_EmpEmergencyContactList");

            SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = empId;

            SqlParameter p_FullName = command.Parameters.Add("FullName", SqlDbType.VarChar);
            p_FullName.Direction = ParameterDirection.Input;
            p_FullName.Value = fullName;

            SqlParameter p_DesigId = command.Parameters.Add("DesigId", SqlDbType.BigInt);
            p_DesigId.Direction = ParameterDirection.Input;
            p_DesigId.Value = Convert.ToInt32(desigId);

            SqlParameter p_DeptId = command.Parameters.Add("DeptId", SqlDbType.BigInt);
            p_DeptId.Direction = ParameterDirection.Input;
            p_DeptId.Value = Convert.ToInt32(deptId);

            SqlParameter p_SectorId = command.Parameters.Add("SectorId", SqlDbType.BigInt);
            p_SectorId.Direction = ParameterDirection.Input;
            p_SectorId.Value = Convert.ToInt32(sectorId);

            SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
            p_EmpTypeID.Direction = ParameterDirection.Input;
            p_EmpTypeID.Value = Convert.ToInt32(empTypeId);

            objDC.CreateDSFromProc(command, "tblEmpEmergencyContactList");
            return objDC.ds.Tables["tblEmpEmergencyContactList"];
        }
    
        public DataTable GetLongServiceAwardeeEmp(string vMonth, string vYear, string empStatus)
        {

            SqlCommand command = new SqlCommand("proc_Get_LongServiceAwardeeEmp");


            SqlParameter p_VMonth = command.Parameters.Add("VMonth", SqlDbType.BigInt);
            p_VMonth.Direction = ParameterDirection.Input;
            p_VMonth.Value = Convert.ToInt32(vMonth);

            SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.BigInt);
            p_VYear.Direction = ParameterDirection.Input;
            p_VYear.Value = Convert.ToInt32(vYear);

             SqlParameter p_EmpStatus = command.Parameters.Add("EmpStatus", SqlDbType.Char);
            p_EmpStatus.Direction = ParameterDirection.Input;
            p_EmpStatus.Value = empStatus;

            objDC.CreateDSFromProc(command, "tblGetLongServiceAwardeeEmp");
            return objDC.ds.Tables["tblGetLongServiceAwardeeEmp"];
        }

        public DataTable GetOTCalculation(string empId, string salLocId, string postingDivId, string vMonth,string vYear)
        {

            SqlCommand command = new SqlCommand("proc_Get_OTCalculation");

            SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = empId;

            SqlParameter p_SalLocId = command.Parameters.Add("SalLocId", SqlDbType.BigInt);
            p_SalLocId.Direction = ParameterDirection.Input;
            p_SalLocId.Value = Convert.ToInt32(salLocId);

            SqlParameter p_PostingDivId = command.Parameters.Add("PostingDivId", SqlDbType.BigInt);
            p_PostingDivId.Direction = ParameterDirection.Input;
            p_PostingDivId.Value = Convert.ToInt32(postingDivId);

            SqlParameter p_VMonth = command.Parameters.Add("VMonth", SqlDbType.BigInt);
            p_VMonth.Direction = ParameterDirection.Input;
            p_VMonth.Value = Convert.ToInt32(vMonth);

            SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.BigInt);
            p_VYear.Direction = ParameterDirection.Input;
            p_VYear.Value = Convert.ToInt32(vYear);

            objDC.CreateDSFromProc(command, "tblOTCalculation");
            return objDC.ds.Tables["tblOTCalculation"];
        }

        public DataTable GetDevelopmentPlan(string empId, string gradeId, string lAreaId, string deptId, string fiscalYrId)
        {
            SqlCommand command = new SqlCommand("proc_Get_DevelopmentPlan");

            SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = empId;

            SqlParameter p_GradeId = command.Parameters.Add("GradeId", SqlDbType.BigInt);
            p_GradeId.Direction = ParameterDirection.Input;
            p_GradeId.Value = Convert.ToInt32(gradeId);

            SqlParameter p_LAreaId = command.Parameters.Add("LAreaId", SqlDbType.BigInt);
            p_LAreaId.Direction = ParameterDirection.Input;
            p_LAreaId.Value = Convert.ToInt32(gradeId);


            SqlParameter p_DeptId = command.Parameters.Add("DeptId", SqlDbType.BigInt);
            p_DeptId.Direction = ParameterDirection.Input;
            p_DeptId.Value = Convert.ToInt32(deptId);

            SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
            p_FiscalYrId.Direction = ParameterDirection.Input;
            p_FiscalYrId.Value = Convert.ToInt32(fiscalYrId);

            objDC.CreateDSFromProc(command, "tblDevelopmentPlan");
            return objDC.ds.Tables["tblDevelopmentPlan"];
        }

        public DataTable GetEmpSalaryInfo(string empId, string gradeId, string ClinicId, string deptId, string empTypeId)
        {
            SqlCommand command = new SqlCommand("proc_Get_EmpSalaryInfo");

            SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = empId;

            SqlParameter p_GradeId = command.Parameters.Add("GradeId", SqlDbType.BigInt);
            p_GradeId.Direction = ParameterDirection.Input;
            p_GradeId.Value = Convert.ToInt32(gradeId);

            SqlParameter p_SectorId = command.Parameters.Add("ClinicId", SqlDbType.BigInt);
            p_SectorId.Direction = ParameterDirection.Input;
            p_SectorId.Value = Convert.ToInt32(ClinicId);

            SqlParameter p_DeptId = command.Parameters.Add("DeptId", SqlDbType.BigInt);
            p_DeptId.Direction = ParameterDirection.Input;
            p_DeptId.Value = Convert.ToInt32(deptId);

            SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
            p_EmpTypeID.Direction = ParameterDirection.Input;
            p_EmpTypeID.Value = Convert.ToInt32(empTypeId);

            objDC.CreateDSFromProc(command, "tblEmpSalaryInfo");
            return objDC.ds.Tables["tblEmpSalaryInfo"];
        }

        public DataTable GetEmpSalaryHistoryInfo(string empId, string gradeId, string ClinicId, string deptId, string empTypeId)
        {
            SqlCommand command = new SqlCommand("proc_Get_EmpSalaryHistoryInfo");

            SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = empId;

            SqlParameter p_GradeId = command.Parameters.Add("GradeId", SqlDbType.BigInt);
            p_GradeId.Direction = ParameterDirection.Input;
            p_GradeId.Value = Convert.ToInt32(gradeId);

            SqlParameter p_SectorId = command.Parameters.Add("ClinicId", SqlDbType.BigInt);
            p_SectorId.Direction = ParameterDirection.Input;
            p_SectorId.Value = Convert.ToInt32(ClinicId);

            SqlParameter p_DeptId = command.Parameters.Add("DeptId", SqlDbType.BigInt);
            p_DeptId.Direction = ParameterDirection.Input;
            p_DeptId.Value = Convert.ToInt32(deptId);

            SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
            p_EmpTypeID.Direction = ParameterDirection.Input;
            p_EmpTypeID.Value = Convert.ToInt32(empTypeId);

            objDC.CreateDSFromProc(command, "tblEmpSalaryHistoryInfo");
            return objDC.ds.Tables["tblEmpSalaryHistoryInfo"];
        }

        public DataTable GetEmpSalaryExceptionCase(string vMonth,string vYear)
        {
            SqlCommand command = new SqlCommand("proc_Get_EmpSalaryExceptionCase");

             SqlParameter p_VMonth = command.Parameters.Add("VMonth", SqlDbType.BigInt);
            p_VMonth.Direction = ParameterDirection.Input;
            p_VMonth.Value = Convert.ToInt32(vMonth);

            SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.BigInt);
            p_VYear.Direction = ParameterDirection.Input;
            p_VYear.Value = Convert.ToInt32(vYear);

            objDC.CreateDSFromProc(command, "tblEmpSalaryExceptionCase");
            return objDC.ds.Tables["tblEmpSalaryExceptionCase"];           
        }

        public DataTable GetEmpExperienceInfo(string empId, string sectorId, string salLocId, string empTypeId)
        {

            SqlCommand command = new SqlCommand("proc_Get_EmpExperienceInfo");

            SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = empId;

            SqlParameter p_SectorId = command.Parameters.Add("SectorId", SqlDbType.BigInt);
            p_SectorId.Direction = ParameterDirection.Input;
            p_SectorId.Value = Convert.ToInt32(sectorId);

            SqlParameter p_SalLocId = command.Parameters.Add("SalLocId", SqlDbType.BigInt);
            p_SalLocId.Direction = ParameterDirection.Input;
            p_SalLocId.Value = Convert.ToInt32(salLocId);

            SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
            p_EmpTypeID.Direction = ParameterDirection.Input;
            p_EmpTypeID.Value = Convert.ToInt32(empTypeId);

            objDC.CreateDSFromProc(command, "tblEmpExperienceInfo");
            return objDC.ds.Tables["tblEmpExperienceInfo"];
        }

        public DataTable GetEmployeeInfo(string empId, string postingDistId, string bloodGroupId, string fromDate, string toDate, string empStatus, string empTypeId)
        {

            SqlCommand command = new SqlCommand("proc_Get_EmployeeInfo");

            SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = empId;

            SqlParameter p_PostingDistId = command.Parameters.Add("PostingDistId", SqlDbType.BigInt);
            p_PostingDistId.Direction = ParameterDirection.Input;
            p_PostingDistId.Value = Convert.ToInt32(postingDistId); 

            SqlParameter p_BloodGroupId = command.Parameters.Add("BloodGroupId", SqlDbType.BigInt);
            p_BloodGroupId.Direction = ParameterDirection.Input;
            p_BloodGroupId.Value = Convert.ToInt32(bloodGroupId);

            SqlParameter p_FromDate = command.Parameters.Add("FromDate", DBNull.Value);
            p_FromDate.Direction = ParameterDirection.Input;
            p_FromDate.IsNullable = true;
            if (string.IsNullOrEmpty(fromDate) == false)
                p_FromDate.Value = Common.ReturnDateFormat_ddmmyyyy(fromDate, false);
            else
                p_FromDate.Value = "";

            SqlParameter p_ToDate = command.Parameters.Add("ToDate", DBNull.Value);
            p_ToDate.Direction = ParameterDirection.Input;
            p_FromDate.IsNullable = true;
            if (string.IsNullOrEmpty(toDate) == false)
            p_ToDate.Value = Common.ReturnDateFormat_ddmmyyyy(toDate, true);
            else
            p_ToDate.Value = "";

           SqlParameter p_IsActive=command.Parameters.Add("EmpStatus",SqlDbType.Char);
           p_IsActive.Direction=ParameterDirection.Input;
           p_IsActive.Value=empStatus;

           SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
           p_EmpTypeID.Direction = ParameterDirection.Input;
           p_EmpTypeID.Value = Convert.ToInt32(empTypeId);

        objDC.CreateDSFromProc(command, "tblEmployeeInfo");
            return objDC.ds.Tables["tblEmployeeInfo"];
        }
        public DataTable GetEmpWitnessInfo(string empId, string fullName, string sectorId, string desigId, string deptId, string empTypeId)
        {

            SqlCommand command = new SqlCommand("proc_Get_EmpWitnessInfo");

            SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = empId;

            SqlParameter p_FullName = command.Parameters.Add("FullName", SqlDbType.VarChar);
            p_FullName.Direction = ParameterDirection.Input;
            p_FullName.Value = fullName;

            SqlParameter p_SectorId = command.Parameters.Add("SectorId", SqlDbType.BigInt);
            p_SectorId.Direction = ParameterDirection.Input;
            p_SectorId.Value = Convert.ToInt32(sectorId);

            SqlParameter p_DesigId = command.Parameters.Add("DesigId", SqlDbType.BigInt);
            p_DesigId.Direction = ParameterDirection.Input;
            p_DesigId.Value = Convert.ToInt32(desigId);

            SqlParameter p_DeptId = command.Parameters.Add("DeptId", SqlDbType.BigInt);
            p_DeptId.Direction = ParameterDirection.Input;
            p_DeptId.Value = Convert.ToInt32(deptId);

            SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
            p_EmpTypeID.Direction = ParameterDirection.Input;
            p_EmpTypeID.Value = Convert.ToInt32(empTypeId);
            

            objDC.CreateDSFromProc(command, "tblEmpWitnessInfo");
            return objDC.ds.Tables["tblEmpWitnessInfo"];
        }

        public DataTable GetEmpoyeeListWithSupervisor(string fullName, string sectorId, string deptId, string postingDistId, string empTypeId)
        {

            SqlCommand command = new SqlCommand("proc_Get_EmployeeListWithSuperVisor");


            SqlParameter p_FullName = command.Parameters.Add("FullName", SqlDbType.VarChar);
            p_FullName.Direction = ParameterDirection.Input;
            p_FullName.Value = fullName;


            SqlParameter p_SectorId = command.Parameters.Add("SectorId", SqlDbType.BigInt);
            p_SectorId.Direction = ParameterDirection.Input;
            p_SectorId.Value = Convert.ToInt32(sectorId);

            SqlParameter p_DeptId = command.Parameters.Add("DeptId", SqlDbType.BigInt);
            p_DeptId.Direction = ParameterDirection.Input;
            p_DeptId.Value = Convert.ToInt32(deptId);

            SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
            p_EmpTypeID.Direction = ParameterDirection.Input;
            p_EmpTypeID.Value = Convert.ToInt32(empTypeId);

            objDC.CreateDSFromProc(command, "tblEmployeeListWithSuperVisor");
            return objDC.ds.Tables["tblEmployeeListWithSuperVisor"];
        }



        public DataTable GetEmployeeListWithAddress(string empId, string homeDistId, string bloodGroupId, string empStatus, string empTypeId )
        {

            SqlCommand command = new SqlCommand("proc_Get_EmployeeListWithAddress");

            SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = empId;

            SqlParameter p_PostingDistId = command.Parameters.Add("DistId", SqlDbType.BigInt);
            p_PostingDistId.Direction = ParameterDirection.Input;
            p_PostingDistId.Value = Convert.ToInt32(homeDistId);

            SqlParameter p_BloodGroupId = command.Parameters.Add("BloodGroupId", SqlDbType.BigInt);
            p_BloodGroupId.Direction = ParameterDirection.Input;
            p_BloodGroupId.Value = Convert.ToInt32(bloodGroupId);

            SqlParameter p_IsActive = command.Parameters.Add("EmpStatus", SqlDbType.Char);
            p_IsActive.Direction = ParameterDirection.Input;
            p_IsActive.Value = empStatus;

            SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
            p_EmpTypeID.Direction = ParameterDirection.Input;
            p_EmpTypeID.Value = Convert.ToInt32(empTypeId);

            objDC.CreateDSFromProc(command, "tblEmployeeListWithAddress");
            return objDC.ds.Tables["tblEmployeeListWithAddress"];
        }

        public DataTable GetEmpDisciplinaryAction(string empId, string salLocId, string postingDivId, string reasonOfActoionId, string actionTypeId, string fromDate, string toDate, string empTypeId)// string vMonth, string vYear,
        {

            SqlCommand command = new SqlCommand("proc_Get_EmpDisciplinaryAction");

            SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = empId;

            SqlParameter p_SalLocId = command.Parameters.Add("SalLocId", SqlDbType.BigInt);
            p_SalLocId.Direction = ParameterDirection.Input;
            p_SalLocId.Value = Convert.ToInt32(salLocId);

            SqlParameter p_PostingDivId = command.Parameters.Add("PostingDivId", SqlDbType.BigInt);
            p_PostingDivId.Direction = ParameterDirection.Input;
            p_PostingDivId.Value = Convert.ToInt32(postingDivId);

            SqlParameter p_ReasonOfActionId = command.Parameters.Add("ReasonOfActionId", SqlDbType.BigInt);
            p_ReasonOfActionId.Direction = ParameterDirection.Input;
            p_ReasonOfActionId.Value = Convert.ToInt32(reasonOfActoionId);

            SqlParameter p_ActionType = command.Parameters.Add("ActionTypeId", SqlDbType.BigInt);
            p_ActionType.Direction = ParameterDirection.Input;
            p_ActionType.Value = Convert.ToInt32(actionTypeId);

            SqlParameter p_FromDate = command.Parameters.Add("FromDate", DBNull.Value);
            p_FromDate.Direction = ParameterDirection.Input;
            p_FromDate.IsNullable = true;
            if (string.IsNullOrEmpty(fromDate) == false)
                p_FromDate.Value = Common.ReturnDateFormat_ddmmyyyy(fromDate, false);
            else
                p_FromDate.Value = "";

            SqlParameter p_ToDate = command.Parameters.Add("ToDate", DBNull.Value);
            p_ToDate.Direction = ParameterDirection.Input;
            p_FromDate.IsNullable = true;
            if (string.IsNullOrEmpty(toDate) == false)
                p_ToDate.Value = Common.ReturnDateFormat_ddmmyyyy(toDate, true);
            else
                p_ToDate.Value = "";

            SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
            p_EmpTypeID.Direction = ParameterDirection.Input;
            p_EmpTypeID.Value = Convert.ToInt32(empTypeId);

            objDC.CreateDSFromProc(command, "tblEmpDisciplinaryAction");
            return objDC.ds.Tables["tblEmpDisciplinaryAction"];
        }

        public DataTable GetConfirmationList(string deptId, string fromDate, string toDate, string empTypeId)
        {

            SqlCommand command = new SqlCommand("proc_Get_ConfirmationList");

            SqlParameter p_DeptId = command.Parameters.Add("DeptId", SqlDbType.BigInt);
            p_DeptId.Direction = ParameterDirection.Input;
            p_DeptId.Value = Convert.ToInt32(deptId);

            SqlParameter p_FromDate = command.Parameters.Add("FromDate", DBNull.Value);
            p_FromDate.Direction = ParameterDirection.Input;
            p_FromDate.IsNullable = true;
            if (string.IsNullOrEmpty(fromDate) == false)
                p_FromDate.Value = Common.ReturnDateFormat_ddmmyyyy(fromDate, false);
            else
                p_FromDate.Value = "";

            SqlParameter p_ToDate = command.Parameters.Add("ToDate", DBNull.Value);
            p_ToDate.Direction = ParameterDirection.Input;
            p_FromDate.IsNullable = true;
            if (string.IsNullOrEmpty(toDate) == false)
                p_ToDate.Value = Common.ReturnDateFormat_ddmmyyyy(toDate, true);
            else
                p_ToDate.Value = "";

            SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
            p_EmpTypeID.Direction = ParameterDirection.Input;
            p_EmpTypeID.Value = Convert.ToInt32(empTypeId);

            objDC.CreateDSFromProc(command, "tblConfirmationList");
            return objDC.ds.Tables["tblConfirmationList"];
        }

        public DataTable GetEmpEduInfoDetails(string empId, string deptId, string salLocId, string empTypeId)
        {

            SqlCommand command = new SqlCommand("proc_Get_EmpEduInfoDetails");

            SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = empId;

            SqlParameter p_DeptId = command.Parameters.Add("DeptId", SqlDbType.BigInt);
            p_DeptId.Direction = ParameterDirection.Input;
            p_DeptId.Value = Convert.ToInt32(deptId);

            SqlParameter p_SalLocId = command.Parameters.Add("SalLocId", SqlDbType.BigInt);
            p_SalLocId.Direction = ParameterDirection.Input;
            p_SalLocId.Value = Convert.ToInt32(salLocId);

            SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
            p_EmpTypeID.Direction = ParameterDirection.Input;
            p_EmpTypeID.Value = Convert.ToInt32(empTypeId);

            objDC.CreateDSFromProc(command, "tblEmpEduInfoDetails");
            return objDC.ds.Tables["tblEmpEduInfoDetails"];
        }

        public DataTable GetEmpEduInfoInBrief(string empId, string deptId, string salLocId, string empTypeId)
        {
            SqlCommand command = new SqlCommand("proc_Get_EmpEduInfoInBrief");

            SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = empId;

            SqlParameter p_DeptId = command.Parameters.Add("DeptId", SqlDbType.BigInt);
            p_DeptId.Direction = ParameterDirection.Input;
            p_DeptId.Value = Convert.ToInt32(deptId);

            SqlParameter p_SalLocId = command.Parameters.Add("SalLocId", SqlDbType.BigInt);
            p_SalLocId.Direction = ParameterDirection.Input;
            p_SalLocId.Value = Convert.ToInt32(salLocId);

            SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
            p_EmpTypeID.Direction = ParameterDirection.Input;
            p_EmpTypeID.Value = Convert.ToInt32(empTypeId);

            objDC.CreateDSFromProc(command, "tblEmpEduInfoInBrief");
            return objDC.ds.Tables["tblEmpEduInfoInBrief"];
        }
        public DataTable GetEmpNomineeInfo(string empId, string deptId, string salLocId, string empTypeId)
        {

            SqlCommand command = new SqlCommand("proc_Get_EmpNomineeInfo");

            SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = empId;

            SqlParameter p_DeptId = command.Parameters.Add("DeptId", SqlDbType.BigInt);
            p_DeptId.Direction = ParameterDirection.Input;
            p_DeptId.Value = Convert.ToInt32(deptId);

            SqlParameter p_SalLocId = command.Parameters.Add("SalLocId", SqlDbType.BigInt);
            p_SalLocId.Direction = ParameterDirection.Input;
            p_SalLocId.Value = Convert.ToInt32(salLocId);

            SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
            p_EmpTypeID.Direction = ParameterDirection.Input;
            p_EmpTypeID.Value = Convert.ToInt32(empTypeId);

            objDC.CreateDSFromProc(command, "tblEmpNomineeInfo");
            return objDC.ds.Tables["tblEmpNomineeInfo"];
        }

        public DataTable GetEmpJoiningInfo(string deptId, string sectorId, string fromDate, string toDate, string empTypeId)
        {

            SqlCommand command = new SqlCommand("proc_Get_EmpJoiningInfo");

            SqlParameter p_DeptId = command.Parameters.Add("DeptId", SqlDbType.BigInt);
            p_DeptId.Direction = ParameterDirection.Input;
            p_DeptId.Value = Convert.ToInt32(deptId);

            SqlParameter p_SectorId = command.Parameters.Add("SectorId", SqlDbType.BigInt);
            p_SectorId.Direction = ParameterDirection.Input;
            p_SectorId.Value = Convert.ToInt32(sectorId);

            SqlParameter p_FromDate = command.Parameters.Add("FromDate", DBNull.Value);
            p_FromDate.Direction = ParameterDirection.Input;
            p_FromDate.IsNullable = true;
            if (string.IsNullOrEmpty(fromDate) == false)
                p_FromDate.Value = Common.ReturnDateFormat_ddmmyyyy(fromDate, false);
            else
                p_FromDate.Value = "";

            SqlParameter p_ToDate = command.Parameters.Add("ToDate", DBNull.Value);
            p_ToDate.Direction = ParameterDirection.Input;
            p_FromDate.IsNullable = true;
            if (string.IsNullOrEmpty(toDate) == false)
                p_ToDate.Value = Common.ReturnDateFormat_ddmmyyyy(toDate, true);
            else
                p_ToDate.Value = "";

            SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
            p_EmpTypeID.Direction = ParameterDirection.Input;
            p_EmpTypeID.Value = Convert.ToInt32(empTypeId);

            objDC.CreateDSFromProc(command, "tblEmpJoiningInfo");
            return objDC.ds.Tables["tblEmpJoiningInfo"];
        }
        public DataTable GetEmpTDYInfo(string empId, string deptId, string fromDate, string toDate)
        {

            SqlCommand command = new SqlCommand("proc_Get_EmpTDYInfo");

            SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = empId;

            SqlParameter p_DeptId = command.Parameters.Add("DeptId", SqlDbType.BigInt);
            p_DeptId.Direction = ParameterDirection.Input;
            p_DeptId.Value = Convert.ToInt32(deptId);

            SqlParameter p_FromDate = command.Parameters.Add("FromDate", DBNull.Value);
            p_FromDate.Direction = ParameterDirection.Input;
            p_FromDate.IsNullable = true;
            if (string.IsNullOrEmpty(fromDate) == false)
                p_FromDate.Value = Common.ReturnDateFormat_ddmmyyyy(fromDate, false);
            else
                p_FromDate.Value = "";

            SqlParameter p_ToDate = command.Parameters.Add("ToDate", DBNull.Value);
            p_ToDate.Direction = ParameterDirection.Input;
            p_FromDate.IsNullable = true;
            if (string.IsNullOrEmpty(toDate) == false)
                p_ToDate.Value = Common.ReturnDateFormat_ddmmyyyy(toDate, true);
            else
                p_ToDate.Value = "";


            objDC.CreateDSFromProc(command, "tblEmpTDYInfo");
            return objDC.ds.Tables["tblEmpTDYInfo"];
        }

        public DataTable GetEmpServiceLengthOfSeparatedEmp(string empId, string postingDivId, string fromDate, string toDate, string empTypeId)  //, string empStatusActiveOrInactive,string fromDate,string toDate
        {

            if (objDC.ds.Tables["tblServiceLengthOfSeparatedEmp"] != null)
            {
                objDC.ds.Tables["tblServiceLengthOfSeparatedEmp"].Clear();
                objDC.ds.Tables["tblServiceLengthOfSeparatedEmp"].Dispose();
            }
            SqlCommand command = new SqlCommand("proc_Get_ServiceLengthOfSeparatedEmp");

            SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = empId;

            SqlParameter p_PostingDivId = command.Parameters.Add("PostingDivId", SqlDbType.BigInt);
            p_PostingDivId.Direction = ParameterDirection.Input;
            p_PostingDivId.Value = Convert.ToInt32(postingDivId);


            SqlParameter p_FromDate = command.Parameters.Add("FromDate", DBNull.Value);
            p_FromDate.Direction = ParameterDirection.Input;
            p_FromDate.IsNullable = true;
            if (string.IsNullOrEmpty(fromDate) == false)
                p_FromDate.Value = Common.ReturnDateFormat_ddmmyyyy(fromDate, false);
            else
                p_FromDate.Value = "";

            SqlParameter p_ToDate = command.Parameters.Add("ToDate", DBNull.Value);
            p_ToDate.Direction = ParameterDirection.Input;
            p_FromDate.IsNullable = true;
            if (string.IsNullOrEmpty(toDate) == false)
                p_ToDate.Value = Common.ReturnDateFormat_ddmmyyyy(toDate, true);
            else
                p_ToDate.Value = "";

            SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
            p_EmpTypeID.Direction = ParameterDirection.Input;
            p_EmpTypeID.Value = Convert.ToInt32(empTypeId);

            objDC.CreateDSFromProc(command, "tblServiceLengthOfSeparatedEmp");
            return objDC.ds.Tables["tblServiceLengthOfSeparatedEmp"];
        }

        public DataTable GetEmpServiceLengthAsPerJoining(string empId, string postingDivId, string fromDate, string toDate, string empTypeId)  //, string empStatusActiveOrInactive,string fromDate,string toDate
        {

            if (objDC.ds.Tables["tblServiceLengthAsDOB"] != null)
            {
                objDC.ds.Tables["tblServiceLengthAsDOB"].Clear();
                objDC.ds.Tables["tblServiceLengthAsDOB"].Dispose();
            }
            SqlCommand command = new SqlCommand("proc_Get_ServiceLengthAsPerJoining");

            SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = empId;

            SqlParameter p_PostingDivId = command.Parameters.Add("PostingDivId", SqlDbType.BigInt);
            p_PostingDivId.Direction = ParameterDirection.Input;
            p_PostingDivId.Value = Convert.ToInt32(postingDivId);


            SqlParameter p_FromDate = command.Parameters.Add("FromDate", DBNull.Value);
            p_FromDate.Direction = ParameterDirection.Input;
            p_FromDate.IsNullable = true;
            if (string.IsNullOrEmpty(fromDate) == false)
                p_FromDate.Value = Common.ReturnDateFormat_ddmmyyyy(fromDate, false);
            else
                p_FromDate.Value = "";

            SqlParameter p_ToDate = command.Parameters.Add("ToDate", DBNull.Value);
            p_ToDate.Direction = ParameterDirection.Input;
            p_FromDate.IsNullable = true;
            if (string.IsNullOrEmpty(toDate) == false)
                p_ToDate.Value = Common.ReturnDateFormat_ddmmyyyy(toDate, true);
            else
                p_ToDate.Value = "";

            SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
            p_EmpTypeID.Direction = ParameterDirection.Input;
            p_EmpTypeID.Value = Convert.ToInt32(empTypeId);

            objDC.CreateDSFromProc(command, "tblServiceLengthAsDOB");
            return objDC.ds.Tables["tblServiceLengthAsDOB"];
        }


        public DataTable GetEmpServiceLengthAsDOB(string empId, string postingDivId, string servicelengthFrom, string serviceLengthTo, string empTypeId)  //, string empStatusActiveOrInactive,string fromDate,string toDate
        {

            if (objDC.ds.Tables["tblEmpServiceLengthAsDOB"] != null)
            {
                objDC.ds.Tables["tblEmpServiceLengthAsDOB"].Clear();
                objDC.ds.Tables["tblEmpServiceLengthAsDOB"].Dispose();
            }
            SqlCommand command = new SqlCommand("proc_Get_ServiceLengthAsDOB");


            SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = empId;

            SqlParameter p_PostingDivId = command.Parameters.Add("PostingDivId", SqlDbType.BigInt);
            p_PostingDivId.Direction = ParameterDirection.Input;
            p_PostingDivId.Value = Convert.ToInt32(postingDivId);

            SqlParameter p_ServiceLengthFrom = command.Parameters.Add("FromServiceLength", SqlDbType.Decimal);
            p_ServiceLengthFrom.Direction = ParameterDirection.Input;
            p_ServiceLengthFrom.Value = Convert.ToDecimal(servicelengthFrom);

            SqlParameter p_ServiceLengthTo = command.Parameters.Add("ToServiceLength", SqlDbType.Decimal);
            p_ServiceLengthTo.Direction = ParameterDirection.Input;
            p_ServiceLengthTo.Value = Convert.ToDecimal(serviceLengthTo);

            SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
            p_EmpTypeID.Direction = ParameterDirection.Input;
            p_EmpTypeID.Value = Convert.ToInt32(empTypeId);

            objDC.CreateDSFromProc(command, "tblEmpServiceLengthAsDOB");
            return objDC.ds.Tables["tblEmpServiceLengthAsDOB"];
        }


        public DataSet GetEmpResume(DataSet dsRpt, string empId)
        {
         
            SqlCommand command = new SqlCommand("proc_Get_EmpResume");
         
            SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = empId;

            return objDC.Get_Rpt_SalReconDetails(command, dsRpt);
        }
    #endregion

    #region Consultant
        public DataSet GetConsultantInfoWithPayment(DataSet dsRpt,string consId)
        {
            if (objDC.ds.Tables["tblConsultantInfoWithPayment"] != null)
            {
                objDC.ds.Tables["tblConsultantInfoWithPayment"].Clear();
                objDC.ds.Tables["tblConsultantInfoWithPayment"].Dispose();
            }

            SqlCommand command = new SqlCommand("proc_Get_ConsultantInfo");

            SqlParameter p_ConsId = command.Parameters.Add("ConsId", SqlDbType.VarChar);
            p_ConsId.Direction = ParameterDirection.Input;
            p_ConsId.Value = consId;
            return objDC.Get_Rpt_SalReconDetails(command, dsRpt);
        }
        #endregion

    #region Security Report
        //User Security Audit Report
        public DataTable Select_User_InOutHistory(string empId, string FromDate, string ToDate)
        {
            SqlCommand command = new SqlCommand("PROC_Select_User_InOutHistory");

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = empId;

            SqlParameter p_StartDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
            p_StartDate.Direction = ParameterDirection.Input;
            p_StartDate.Value = Common.ReturnDateFormat_ddmmyyyy(FromDate, false);

            SqlParameter p_EndDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
            p_EndDate.Direction = ParameterDirection.Input;
            p_EndDate.Value = Common.ReturnDateFormat_ddmmyyyy(ToDate, true);

            objDC.CreateDSFromProc(command, "tblUserSecurityAudit");
            return objDC.ds.Tables["tblUserSecurityAudit"];
        }
    #endregion

    #region KPI
       
        public DataSet Select_KPIReview(string empId, string sYear, string sQuarter, string sGroup)
        {
            DataSet myDs = new DataSet();
            SqlCommand command = new SqlCommand("PROC_Rpt_Select_KPIReview");

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = empId;
            SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.BigInt);
            p_VYear.Direction = ParameterDirection.Input;
            p_VYear.Value = Convert.ToInt32(sYear);

            SqlParameter p_QuarterId = command.Parameters.Add("QuarterId", SqlDbType.BigInt);
            p_QuarterId.Direction = ParameterDirection.Input;
            p_QuarterId.Value = Convert.ToInt32(sQuarter);

            SqlParameter p_GroupId = command.Parameters.Add("GroupId", SqlDbType.BigInt);
            p_GroupId.Direction = ParameterDirection.Input;
            p_GroupId.Value = Convert.ToInt32(sGroup);

   
            //objDC.CreateDSFromProc(command, "tblKPIReview");
            myDs =(DataSet)objDC.Get_Rpt_SalReconDetails(command);
            return myDs;// objDC.ds.Tables["tblKPIReview"];
        }
    #endregion

    #region PF Reports
        public DataTable GetPFInterDistribution(string strFinYear)
        {
            string DivId = "";
            dsPFInterDistribution objPFIntDis = new dsPFInterDistribution();
            DataTable dtPFInDis = new DataTable();
            DataTable dtPFCurrYrCon = new DataTable();
            DataTable dtPFMidTermInt = new DataTable();
            DataTable dtOPPFLedger = new DataTable();
            decimal dclCurrYrCont = 0;

            string strExpr = "";
            string strExpr2 = "";
            dtPFInDis = GetPFInterDistributionForJune(strFinYear);
            dtPFCurrYrCon = GetPFCurrYrContribution(strFinYear);
            dtPFMidTermInt = GetPFMidTermInterest(strFinYear);
            dtOPPFLedger = GetOPPFLedger(strFinYear);

            if (dtPFInDis.Rows.Count > 0)
            {
                foreach (DataRow row in dtPFInDis.Rows)
                {
                    strExpr = "";
                    strExpr2 = "";
                    dclCurrYrCont = 0;
                    if (row["EMPID"].ToString().Trim() == "3018")
                        strExpr2 = "";
                    DataRow[] foundOPRows = dtOPPFLedger.Select("EMPID ='" + row["EMPID"].ToString().Trim() + "'");

                    DataRow FinalRow = objPFIntDis.dtPFInterDistribution.NewRow();

                    FinalRow["EMPID"] = row["EMPID"];
                    FinalRow["FULLNAME"] = row["FULLNAME"];
                    if (foundOPRows.Length > 0)
                    {
                        FinalRow["CMPFCARE"] = Convert.ToString(Common.RoundDecimal(foundOPRows[0]["OPPFCARE"].ToString(), 0) * 2);
                        FinalRow["CUPFINTREST"] = foundOPRows[0]["OPPFINTREST"];
                        dclCurrYrCont = Common.RoundDecimal(foundOPRows[0]["OPPFCARE"].ToString(), 0);
                    }
                    else
                    {
                        FinalRow["CMPFCARE"] = "0";
                        FinalRow["CUPFINTREST"] = "0";
                        dclCurrYrCont = 0;
                    }
                    strExpr = "EMPID='" + row["EMPID"].ToString().Trim() + "'";
                    DataRow[] foundRows = dtPFCurrYrCon.Select(strExpr);
                    if (foundRows.Length > 0)
                    {
                        dclCurrYrCont = Common.RoundDecimal(foundRows[0]["CUPFCARE"].ToString(), 0) - dclCurrYrCont;
                        dclCurrYrCont = dclCurrYrCont * 2;
                        FinalRow["CMTOTAL"] = dclCurrYrCont.ToString();
                    }
                    else
                    {
                        FinalRow["CMTOTAL"] = 0;
                    }
                    strExpr2 = "EMPID='" + row["EMPID"].ToString().Trim() + "'";
                    DataRow[] foundRows2 = dtPFMidTermInt.Select(strExpr);
                    if (foundRows2.Length > 0)
                        FinalRow["MIDTERMCMPFINTREST"] = foundRows2[0]["LASTPFINT"];
                    else
                        FinalRow["MIDTERMCMPFINTREST"] = 0;

                    FinalRow["CMPFINTREST"] = row["CMPFINTREST"];
                    FinalRow["TOTALBALANCE"] = Convert.ToDecimal(row["CUTOTAL"]);
                    FinalRow["TOTALPAY"] = row["TOTALPAY"];
                    if (Convert.ToDecimal(FinalRow["TOTALBALANCE"]) > Convert.ToDecimal(row["TOTALPAY"]))
                        FinalRow["NETBALANCE"] = Convert.ToDecimal(FinalRow["TOTALBALANCE"]) - Convert.ToDecimal(row["TOTALPAY"]);
                    else
                        FinalRow["NETBALANCE"] = 0;
                    objPFIntDis.dtPFInterDistribution.Rows.Add(FinalRow);
                    foundOPRows = null;
                }
            }

            objPFIntDis.dtPFInterDistribution.AcceptChanges();
            return objPFIntDis.dtPFInterDistribution;
        }

        private DataTable GetPFInterDistributionForJune(string strFinYear)
        {
            SqlCommand command = new SqlCommand("proc_Get_PFInterDistribution");

            SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.BigInt);
            p_VYear.Direction = ParameterDirection.Input;
            p_VYear.Value = strFinYear;

            objDC.CreateDSFromProc(command, "PFInterDistribution");
            return objDC.ds.Tables["PFInterDistribution"];
        }

        private DataTable GetPFCurrYrContribution(string strFinYear)
        {
            string strSQL = "SELECT EMPID,CUPFCARE FROM PFLedger"
                    + " WHERE FiscalYrID =" + strFinYear + " AND VMONTH=6 ORDER BY CAST(EMPID as Numeric)";

            SqlCommand command = new SqlCommand(strSQL);

            SqlParameter p_FiscalYrID = command.Parameters.Add("FiscalYrID", SqlDbType.BigInt);
            p_FiscalYrID.Direction = ParameterDirection.Input;
            p_FiscalYrID.Value = strFinYear;

            return objDC.CreateDT(strSQL, "tblPFCurrYrContribution");
        }

        //private DataTable GetPFMidTermInterest(string strFinYear)
        //{
        //    string strSQL = "SELECT EMPID,ISNULL(CMPFINTREST,0) AS MIDTERMCMPFINTREST FROM PFLedger"
        //            + " WHERE VMonth<>6 AND CMPFINTREST>0 AND FiscalYrID =" + strFinYear + " ORDER BY CAST(EMPID as Numeric) ";

        //    SqlCommand command = new SqlCommand(strSQL);

        //    SqlParameter p_FiscalYrID = command.Parameters.Add("FiscalYrID", SqlDbType.BigInt);
        //    p_FiscalYrID.Direction = ParameterDirection.Input;
        //    p_FiscalYrID.Value = strFinYear;

        //    return dbConn.CreateDT(strSQL, "tblPFMidTermInterest");
        //}

        private DataTable GetPFMidTermInterest(string strFinYear)
        {
            string strSQL = "select * from FinalPaymentPF where LASTPFINT>0 "
                          + " AND FiscalYrID =" + strFinYear + " ORDER BY CAST(EMPID as Numeric) ";

            SqlCommand command = new SqlCommand(strSQL);

            SqlParameter p_FiscalYrID = command.Parameters.Add("FiscalYrID", SqlDbType.BigInt);
            p_FiscalYrID.Direction = ParameterDirection.Input;
            p_FiscalYrID.Value = strFinYear;

            return objDC.CreateDT(strSQL, "tblPFMidTermInterest");
        }

        private DataTable GetOPPFLedger(string strFinYear)
        {
            string strSQL = "SELECT * FROM PFLedger"
                    + " WHERE FiscalYrID =" + strFinYear + " AND VMONTH=7 ORDER BY CAST(EMPID as Numeric)";

            SqlCommand command = new SqlCommand(strSQL);

            SqlParameter p_FiscalYrID = command.Parameters.Add("FiscalYrID", SqlDbType.BigInt);
            p_FiscalYrID.Direction = ParameterDirection.Input;
            p_FiscalYrID.Value = strFinYear;

            return objDC.CreateDT(strSQL, "GetOPPFLedger");
        }
    #endregion

    #region Attendance Reports
        public DataTable Get_OtEmpWise(string flag, string UserId, string IsAdmin, string fromDate, string toDate,
           string BranchId, string divisionId, string isClosed, string strempid)
        {
            SqlCommand command = new SqlCommand("proc_Get_OtEmpWise");

            SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
            p_Flag.Direction = ParameterDirection.Input;
            p_Flag.Value = flag;

            SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
            p_UserId.Direction = ParameterDirection.Input;
            p_UserId.Value = "'" + UserId + "'";

            SqlParameter p_IsAdmin = command.Parameters.Add("IsAdmin", SqlDbType.Char);
            p_IsAdmin.Direction = ParameterDirection.Input;
            p_IsAdmin.Value = IsAdmin;

            SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
            p_FromDate.Direction = ParameterDirection.Input;
            p_FromDate.Value = Common.ReturnDateFormat(fromDate, false);//fromDate;

            SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
            p_ToDate.Direction = ParameterDirection.Input;
            p_ToDate.Value = Common.ReturnDateFormat(toDate, true);

            SqlParameter p_BranchId = command.Parameters.Add("BranchId", SqlDbType.VarChar);
            p_BranchId.Direction = ParameterDirection.Input;
            p_BranchId.Value = BranchId;

            SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.VarChar);
            p_DivisionID.Direction = ParameterDirection.Input;
            p_DivisionID.Value = divisionId;

            SqlParameter p_isClosed = command.Parameters.Add("isClosed", SqlDbType.Char);
            p_isClosed.Direction = ParameterDirection.Input;
            p_isClosed.Value = isClosed;

            SqlParameter p_empid = command.Parameters.Add("empid", SqlDbType.Char);
            p_empid.Direction = ParameterDirection.Input;
            p_empid.Value = strempid;

            objDC.CreateDSFromProc(command, "tblOverTime");
            return objDC.ds.Tables["tblOverTime"];
        }

        public DataTable Get_MonthlyAttnd(string flag, string UserId, string IsAdmin, string fromDate, string toDate,
             string divisionId, string SBUId, string DeptId, string empId, string shift, string isClosed)
        {
            SqlCommand command = new SqlCommand("proc_Get_AttndEmpWise");

            SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
            p_Flag.Direction = ParameterDirection.Input;
            p_Flag.Value = flag;

            SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
            p_UserId.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(empId) == true)
                p_UserId.Value = "'" + UserId + "'";
            else
                p_UserId.Value = UserId;

            SqlParameter p_IsAdmin = command.Parameters.Add("IsAdmin", SqlDbType.Char);
            p_IsAdmin.Direction = ParameterDirection.Input;
            p_IsAdmin.Value = IsAdmin;

            SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
            p_FromDate.Direction = ParameterDirection.Input;
            p_FromDate.Value = Common.ReturnDateFormat_ddmmyyyy(fromDate, false);

            SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
            p_ToDate.Direction = ParameterDirection.Input;
            p_ToDate.Value = Common.ReturnDateFormat_ddmmyyyy(toDate, true);

            SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
            p_DivisionID.Direction = ParameterDirection.Input;
            p_DivisionID.Value = divisionId;

            SqlParameter p_SBUID = command.Parameters.Add("SBUId", SqlDbType.BigInt);
            p_SBUID.Direction = ParameterDirection.Input;
            p_SBUID.Value = SBUId;

            SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
            p_DeptID.Direction = ParameterDirection.Input;
            p_DeptID.Value = Convert.ToInt32(DeptId);

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = empId;

            SqlParameter p_ShiftId = command.Parameters.Add("AttnPolicyId", SqlDbType.BigInt);
            p_ShiftId.Direction = ParameterDirection.Input;
            p_ShiftId.Value = shift;

            SqlParameter p_isClosed = command.Parameters.Add("isClosed", SqlDbType.Char);
            p_isClosed.Direction = ParameterDirection.Input;
            p_isClosed.Value = isClosed;

            objDC.CreateDSFromProc(command, "tblMonthlyAttnd");
            return objDC.ds.Tables["tblMonthlyAttnd"];
        }

        public DataTable Get_IncompleteReport(string flag, string UserId, string IsAdmin, string fromDate, string toDate,
             string divisionId, string SBUId, string DeptId, string empId, string AttnPolicyId, string isClosed)
        {
            SqlCommand command = new SqlCommand("proc_Get_IncompleteReport");

            SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
            p_Flag.Direction = ParameterDirection.Input;
            p_Flag.Value = flag;

            SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
            p_UserId.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(empId) == true)
                p_UserId.Value = "'" + UserId + "'";
            else
                p_UserId.Value = UserId;

            SqlParameter p_IsAdmin = command.Parameters.Add("IsAdmin", SqlDbType.Char);
            p_IsAdmin.Direction = ParameterDirection.Input;
            p_IsAdmin.Value = IsAdmin;

            SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
            p_FromDate.Direction = ParameterDirection.Input;
            p_FromDate.Value = Common.ReturnDateFormat_ddmmyyyy(fromDate, false);//fromDate;

            SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
            p_ToDate.Direction = ParameterDirection.Input;
            p_ToDate.Value = Common.ReturnDateFormat_ddmmyyyy(toDate, true); ;

            SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
            p_DivisionID.Direction = ParameterDirection.Input;
            p_DivisionID.Value = divisionId;

            SqlParameter p_SBUId = command.Parameters.Add("SBUId", SqlDbType.BigInt);
            p_SBUId.Direction = ParameterDirection.Input;
            p_SBUId.Value = SBUId;

            SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
            p_DeptID.Direction = ParameterDirection.Input;
            p_DeptID.Value = Convert.ToInt32(DeptId);

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = empId;

            SqlParameter p_ShiftId = command.Parameters.Add("AttnPolicyId", SqlDbType.BigInt);
            p_ShiftId.Direction = ParameterDirection.Input;
            p_ShiftId.Value = AttnPolicyId;

            SqlParameter p_isClosed = command.Parameters.Add("isClosed", SqlDbType.Char);
            p_isClosed.Direction = ParameterDirection.Input;
            p_isClosed.Value = isClosed;

            objDC.CreateDSFromProc(command, "tblIncompleteReport");
            return objDC.ds.Tables["tblIncompleteReport"];
        }

        public DataTable Get_Daily_Attandance(string flag, string fromDate, string toDate,
            string SBUId, string DivisionId, string empId, string AttnPolicyId, string isClosed)
        {
            SqlCommand command = new SqlCommand("proc_Get_Daily_AttandanceReport");

            SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
            p_Flag.Direction = ParameterDirection.Input;
            p_Flag.Value = flag;

            SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
            p_FromDate.Direction = ParameterDirection.Input;
            p_FromDate.Value = Common.ReturnDateFormat_ddmmyyyy(fromDate, false);

            SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
            p_ToDate.Direction = ParameterDirection.Input;
            p_ToDate.Value = Common.ReturnDateFormat_ddmmyyyy(toDate, true);

            SqlParameter p_DivisionId = command.Parameters.Add("DivisionId", SqlDbType.BigInt);
            p_DivisionId.Direction = ParameterDirection.Input;
            p_DivisionId.Value = DivisionId;

            SqlParameter p_SBUId = command.Parameters.Add("SBUId", SqlDbType.BigInt);
            p_SBUId.Direction = ParameterDirection.Input;
            p_SBUId.Value = SBUId;

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = empId;

            SqlParameter p_ShiftId = command.Parameters.Add("AttnPolicyId", SqlDbType.BigInt);
            p_ShiftId.Direction = ParameterDirection.Input;
            p_ShiftId.Value = AttnPolicyId;

            SqlParameter p_isClosed = command.Parameters.Add("isClosed", SqlDbType.Char);
            p_isClosed.Direction = ParameterDirection.Input;
            p_isClosed.Value = isClosed;

            objDC.CreateDSFromProc(command, "tbl4DayAttandance");
            return objDC.ds.Tables["tbl4DayAttandance"];
        }

        public DataTable Get_AbsentReport(string flag, string UserId, string IsAdmin, string fromDate, string toDate,
             string divisionId, string SBUId, string DeptId, string empId, string AttnPolicyId, string isClosed)
        {
            SqlCommand command = new SqlCommand("proc_Get_AbsentReport");

            SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
            p_Flag.Direction = ParameterDirection.Input;
            p_Flag.Value = flag;

            SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
            p_UserId.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(empId) == true)
                p_UserId.Value = "'" + UserId + "'";
            else
                p_UserId.Value = UserId;

            SqlParameter p_IsAdmin = command.Parameters.Add("IsAdmin", SqlDbType.Char);
            p_IsAdmin.Direction = ParameterDirection.Input;
            p_IsAdmin.Value = IsAdmin;

            SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
            p_FromDate.Direction = ParameterDirection.Input;
            p_FromDate.Value = Common.ReturnDateFormat_ddmmyyyy(fromDate, false);//fromDate;

            SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
            p_ToDate.Direction = ParameterDirection.Input;
            p_ToDate.Value = Common.ReturnDateFormat_ddmmyyyy(toDate, true);

            SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
            p_DivisionID.Direction = ParameterDirection.Input;
            p_DivisionID.Value = divisionId;

            SqlParameter p_SBUId = command.Parameters.Add("SBUId", SqlDbType.BigInt);
            p_SBUId.Direction = ParameterDirection.Input;
            p_SBUId.Value = SBUId;

            SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
            p_DeptID.Direction = ParameterDirection.Input;
            p_DeptID.Value = Convert.ToInt32(DeptId);

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = empId;

            SqlParameter p_ShiftId = command.Parameters.Add("AttnPolicyId", SqlDbType.BigInt);
            p_ShiftId.Direction = ParameterDirection.Input;
            p_ShiftId.Value = AttnPolicyId;

            SqlParameter p_isClosed = command.Parameters.Add("isClosed", SqlDbType.Char);
            p_isClosed.Direction = ParameterDirection.Input;
            p_isClosed.Value = isClosed;

            objDC.CreateDSFromProc(command, "tblAbsentReport");
            return objDC.ds.Tables["tblAbsentReport"];
        }

        public DataTable Get_LateReport(string flag, string UserId, string IsAdmin, string fromDate, string toDate,
             string divisionId, string SBUId, string DeptId, string empId, string AttnPolicyId, string isClosed)
        {
            SqlCommand command = new SqlCommand("proc_Get_LateReport");

            SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
            p_Flag.Direction = ParameterDirection.Input;
            p_Flag.Value = flag;

            SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
            p_UserId.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(empId) == true)
                p_UserId.Value = "'" + UserId + "'";
            else
                p_UserId.Value = UserId;

            SqlParameter p_IsAdmin = command.Parameters.Add("IsAdmin", SqlDbType.Char);
            p_IsAdmin.Direction = ParameterDirection.Input;
            p_IsAdmin.Value = IsAdmin;

            SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
            p_FromDate.Direction = ParameterDirection.Input;
            p_FromDate.Value = Common.ReturnDateFormat(fromDate, false);

            SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
            p_ToDate.Direction = ParameterDirection.Input;
            p_ToDate.Value = Common.ReturnDateFormat(toDate, true);

            SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
            p_DivisionID.Direction = ParameterDirection.Input;
            p_DivisionID.Value = divisionId;

            SqlParameter p_SBUId = command.Parameters.Add("SBUId", SqlDbType.BigInt);
            p_SBUId.Direction = ParameterDirection.Input;
            p_SBUId.Value = SBUId;

            SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
            p_DeptID.Direction = ParameterDirection.Input;
            p_DeptID.Value = Convert.ToInt32(DeptId);

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = empId;

            SqlParameter p_ShiftId = command.Parameters.Add("AttnPolicyId", SqlDbType.BigInt);
            p_ShiftId.Direction = ParameterDirection.Input;
            p_ShiftId.Value = AttnPolicyId;

            SqlParameter p_isClosed = command.Parameters.Add("isClosed", SqlDbType.Char);
            p_isClosed.Direction = ParameterDirection.Input;
            p_isClosed.Value = isClosed;

            objDC.CreateDSFromProc(command, "tblLateReport");
            return objDC.ds.Tables["tblLateReport"];
        }

        public DataTable Get_OverTimeReport(string flag, string UserId, string IsAdmin,
            string fromDate, string toDate, string BranchId, string divisionId,
            string empId, string isClosed)
        {
            SqlCommand command = new SqlCommand("proc_Get_OverTime");

            SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
            p_Flag.Direction = ParameterDirection.Input;
            p_Flag.Value = flag;

            SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
            p_UserId.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(empId) == true)
                p_UserId.Value = "'" + UserId + "'";
            else
                p_UserId.Value = UserId;

            SqlParameter p_IsAdmin = command.Parameters.Add("IsAdmin", SqlDbType.Char);
            p_IsAdmin.Direction = ParameterDirection.Input;
            p_IsAdmin.Value = IsAdmin;

            SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
            p_FromDate.Direction = ParameterDirection.Input;
            p_FromDate.Value = Common.ReturnDateFormat(fromDate, false);//fromDate;

            SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
            p_ToDate.Direction = ParameterDirection.Input;
            p_ToDate.Value = Common.ReturnDateFormat(toDate, true);

            SqlParameter p_BranchId = command.Parameters.Add("BranchId", SqlDbType.VarChar);
            p_BranchId.Direction = ParameterDirection.Input;
            p_BranchId.Value = BranchId;

            SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.VarChar);
            p_DivisionID.Direction = ParameterDirection.Input;
            p_DivisionID.Value = divisionId;

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = empId;
            SqlParameter p_isClosed = command.Parameters.Add("isClosed", SqlDbType.Char);
            p_isClosed.Direction = ParameterDirection.Input;
            p_isClosed.Value = isClosed;

            objDC.CreateDSFromProc(command, "tblOverTime");
            return objDC.ds.Tables["tblOverTime"];
        }

        public DataTable Get_Monthly_Attandance(string flag, string fromDate, string toDate,
            string DivisionId, string SBUId, string empId, string AttnPolicyId, string isClosed)
        {
            SqlCommand command = new SqlCommand("proc_Get_MonthlyAttandanceReport");

            SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
            p_Flag.Direction = ParameterDirection.Input;
            p_Flag.Value = flag;

            SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
            p_FromDate.Direction = ParameterDirection.Input;
            p_FromDate.Value = Common.ReturnDateFormat_ddmmyyyy(fromDate, false);

            SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
            p_ToDate.Direction = ParameterDirection.Input;
            p_ToDate.Value = Common.ReturnDateFormat_ddmmyyyy(toDate, true);

            SqlParameter p_DivisionId = command.Parameters.Add("DivisionId", SqlDbType.BigInt);
            p_DivisionId.Direction = ParameterDirection.Input;
            p_DivisionId.Value = DivisionId;

            SqlParameter p_SBUId = command.Parameters.Add("SBUId", SqlDbType.BigInt);
            p_SBUId.Direction = ParameterDirection.Input;
            p_SBUId.Value = SBUId;

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = empId;

            SqlParameter p_ShiftId = command.Parameters.Add("AttnPolicyId", SqlDbType.BigInt);
            p_ShiftId.Direction = ParameterDirection.Input;
            p_ShiftId.Value = AttnPolicyId;

            SqlParameter p_isClosed = command.Parameters.Add("isClosed", SqlDbType.Char);
            p_isClosed.Direction = ParameterDirection.Input;
            p_isClosed.Value = isClosed;

            objDC.CreateDSFromProc(command, "tblMonthlyAttandance");
            return objDC.ds.Tables["tblMonthlyAttandance"];
        }

        public DataTable Get_EarlyDepartureReport(string flag, string UserId, string IsAdmin, string fromDate, string toDate,
              string divisionId, string SBUId, string DeptId, string empId, string AttnPolicyId, string isClosed)
        {
            SqlCommand command = new SqlCommand("proc_Get_EarlyDepartureReport");

            SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
            p_Flag.Direction = ParameterDirection.Input;
            p_Flag.Value = flag;

            SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
            p_UserId.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(empId) == true)
                p_UserId.Value = "'" + UserId + "'";
            else
                p_UserId.Value = UserId;

            SqlParameter p_IsAdmin = command.Parameters.Add("IsAdmin", SqlDbType.Char);
            p_IsAdmin.Direction = ParameterDirection.Input;
            p_IsAdmin.Value = IsAdmin;

            SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
            p_FromDate.Direction = ParameterDirection.Input;
            p_FromDate.Value = Common.ReturnDateFormat_ddmmyyyy(fromDate, false);

            SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
            p_ToDate.Direction = ParameterDirection.Input;
            p_ToDate.Value = Common.ReturnDateFormat_ddmmyyyy(toDate, true);

            SqlParameter p_SBUId = command.Parameters.Add("SBUId", SqlDbType.BigInt);
            p_SBUId.Direction = ParameterDirection.Input;
            p_SBUId.Value = SBUId;

            SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
            p_DeptID.Direction = ParameterDirection.Input;
            p_DeptID.Value = Convert.ToInt32(DeptId);

            SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
            p_DivisionID.Direction = ParameterDirection.Input;
            p_DivisionID.Value = divisionId;

            SqlParameter p_ShiftId = command.Parameters.Add("AttnPolicyId", SqlDbType.BigInt);
            p_ShiftId.Direction = ParameterDirection.Input;
            p_ShiftId.Value = AttnPolicyId;

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = empId;

            SqlParameter p_isClosed = command.Parameters.Add("isClosed", SqlDbType.Char);
            p_isClosed.Direction = ParameterDirection.Input;
            p_isClosed.Value = isClosed;

            objDC.CreateDSFromProc(command, "tblEDReport");
            return objDC.ds.Tables["tblEDReport"];
        }
    #endregion


}
