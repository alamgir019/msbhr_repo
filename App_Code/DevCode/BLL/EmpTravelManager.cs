using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public class EmpTravelManager
{
    //DBConnector objDC = new DBConnector();  
    //#region Insert for Employee Search Screen
    //public void InsertEmpTravel(clsEmpTravel clTravel, string strIsUpdate, string strIsDelete, GridView grCopyTo, int CopyToCount)
    //{
    //    SqlCommand[] cmd = new SqlCommand[CopyToCount + 2];
    //    int i = 0;
    //    cmd[0] = new SqlCommand("proc_Insert_EmpTravel");
    //    cmd[0].CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_EmpId = cmd[0].Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = clTravel.EmpId;

    //    SqlParameter p_TravelId = cmd[0].Parameters.Add("TravelId", SqlDbType.BigInt);
    //    p_TravelId.Direction = ParameterDirection.Input;
    //    p_TravelId.Value = clTravel.TravelId;

    //    SqlParameter p_AppDate = cmd[0].Parameters.Add("AppDate", DBNull.Value);
    //    p_AppDate.Direction = ParameterDirection.Input;
    //    p_AppDate.IsNullable = true;
    //    if (clTravel.AppDate != "")
    //        p_AppDate.Value = Common.ReturnDate(clTravel.AppDate);

    //    SqlParameter p_VisitTo = cmd[0].Parameters.Add("VisitTo", SqlDbType.VarChar);
    //    p_VisitTo.Direction = ParameterDirection.Input;
    //    p_VisitTo.Value = clTravel.VisitTo;

    //    SqlParameter p_Purpose = cmd[0].Parameters.Add("Purpose", SqlDbType.VarChar);
    //    p_Purpose.Direction = ParameterDirection.Input;
    //    p_Purpose.Value = clTravel.Purpose;

    //    SqlParameter p_TravelType = cmd[0].Parameters.Add("TravelTypeId", SqlDbType.BigInt);
    //    p_TravelType.Direction = ParameterDirection.Input;
    //    p_TravelType.Value = clTravel.TravelType;

    //    SqlParameter p_DepartureDate = cmd[0].Parameters.Add("DepartureDate", DBNull.Value);
    //    p_DepartureDate.Direction = ParameterDirection.Input;
    //    p_DepartureDate.IsNullable = true;
    //    if (clTravel.DepartureDate != "")
    //        p_DepartureDate.Value = Common.ReturnDate(clTravel.DepartureDate);

    //    SqlParameter p_ReturnDate = cmd[0].Parameters.Add("ReturnDate", DBNull.Value);
    //    p_ReturnDate.Direction = ParameterDirection.Input;
    //    p_ReturnDate.IsNullable = true;
    //    if (clTravel.ReturnDate != "")
    //        p_ReturnDate.Value = Common.ReturnDate(clTravel.ReturnDate);

    //    SqlParameter p_OfficeJoinDate = cmd[0].Parameters.Add("OfficeJoinDate", DBNull.Value);
    //    p_OfficeJoinDate.Direction = ParameterDirection.Input;
    //    p_OfficeJoinDate.IsNullable = true;
    //    if (clTravel.OfficeJoinDate != "")
    //        p_OfficeJoinDate.Value = Common.ReturnDate(clTravel.OfficeJoinDate);

    //    SqlParameter p_TotalDays = cmd[0].Parameters.Add("TotalDays", SqlDbType.Decimal);
    //    p_TotalDays.Direction = ParameterDirection.Input;
    //    if (clTravel.TotalDays != "")
    //        p_TotalDays.Value = clTravel.TotalDays;
    //    else
    //        p_TotalDays.Value = "0";

    //    SqlParameter p_TravelMode = cmd[0].Parameters.Add("TarvelModeId", SqlDbType.BigInt);
    //    p_TravelMode.Direction = ParameterDirection.Input;
    //    p_TravelMode.Value = clTravel.TravelMode;

    //    SqlParameter p_AdvAmount = cmd[0].Parameters.Add("AdvAmount", SqlDbType.Decimal);
    //    p_AdvAmount.Direction = ParameterDirection.Input;
    //    if (clTravel.AdvAmount != "")
    //        p_AdvAmount.Value = clTravel.AdvAmount;
    //    else
    //        p_AdvAmount.Value = "0";

    //    SqlParameter p_Currency = cmd[0].Parameters.Add("Currency", SqlDbType.Char);
    //    p_Currency.Direction = ParameterDirection.Input;
    //    p_Currency.Value = clTravel.Currency;

    //    SqlParameter p_AdvAdjustDate = cmd[0].Parameters.Add("AdvAdjustDate", DBNull.Value);
    //    p_AdvAdjustDate.Direction = ParameterDirection.Input;
    //    p_AdvAdjustDate.IsNullable = true;
    //    if (clTravel.AdvAdjustDate != "")
    //        p_AdvAdjustDate.Value = Common.ReturnDate(clTravel.AdvAdjustDate);

    //    SqlParameter p_TravelInstruction = cmd[0].Parameters.Add("TravelInstruction", SqlDbType.VarChar);
    //    p_TravelInstruction.Direction = ParameterDirection.Input;
    //    p_TravelInstruction.Value = clTravel.TravelInstruction;

    //    SqlParameter p_TravelStatus = cmd[0].Parameters.Add("TravelStatus", SqlDbType.Char);
    //    p_TravelStatus.Direction = ParameterDirection.Input;
    //    p_TravelStatus.Value = clTravel.TravelStatus;

    //    SqlParameter p_AccountLine = cmd[0].Parameters.Add("AccountLine", SqlDbType.VarChar);
    //    p_AccountLine.Direction = ParameterDirection.Input;
    //    p_AccountLine.Value = clTravel.AccountLine;

    //    SqlParameter p_InsertedBy = cmd[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy.Direction = ParameterDirection.Input;
    //    p_InsertedBy.Value = clTravel.InsertedBy;

    //    SqlParameter p_InsertedDate = cmd[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.Value = clTravel.InsertedDate;

    //    SqlParameter p_IsUpdate = cmd[0].Parameters.Add("IsUpdate", SqlDbType.Char);
    //    p_IsUpdate.Direction = ParameterDirection.Input;
    //    p_IsUpdate.Value = strIsUpdate;

    //    SqlParameter p_IsDelete = cmd[0].Parameters.Add("IsDelete", SqlDbType.Char);
    //    p_IsDelete.Direction = ParameterDirection.Input;
    //    p_IsDelete.Value = strIsDelete;

    //    int j = 0;

    //    // Delete from CopyTo Table
    //    cmd[1] = new SqlCommand("proc_Delete_EMPTRAVELCOPYTO");
    //    cmd[1].CommandType = CommandType.StoredProcedure;
    //    SqlParameter pm_TravelID = cmd[1].Parameters.Add("TravelID", SqlDbType.BigInt);
    //    pm_TravelID.Direction = ParameterDirection.Input;
    //    pm_TravelID.Value = clTravel.TravelId;
    //    i = 2;

    //    foreach (GridViewRow gRow in grCopyTo.Rows)
    //    {
    //        // Insert Into CopyTo Table
    //        CheckBox chk = (CheckBox)gRow.Cells[0].FindControl("chkBox");
    //        if (chk.Checked == true)
    //        {
    //            cmd[i] = new SqlCommand("proc_Insert_EMPTRAVELCOPYTO");
    //            cmd[i].CommandType = CommandType.StoredProcedure;

    //            SqlParameter pm_EMPID = cmd[i].Parameters.Add("EMPID", SqlDbType.Char);
    //            pm_EMPID.Direction = ParameterDirection.Input;
    //            pm_EMPID.Value = clTravel.EmpId;

    //            SqlParameter pmm_TravelID = cmd[i].Parameters.Add("TravelID", SqlDbType.BigInt);
    //            pmm_TravelID.Direction = ParameterDirection.Input;
    //            pmm_TravelID.Value = clTravel.TravelId;

    //            SqlParameter pm_CopyToID = cmd[i].Parameters.Add("CopyToID", SqlDbType.Char);
    //            pm_CopyToID.Direction = ParameterDirection.Input;
    //            pm_CopyToID.Value = grCopyTo.DataKeys[j].Values[0].ToString().Trim();

    //            SqlParameter pm_CopyToEmail = cmd[i].Parameters.Add("CopyToEmail", SqlDbType.VarChar);
    //            pm_CopyToEmail.Direction = ParameterDirection.Input;
    //            pm_CopyToEmail.Value = grCopyTo.DataKeys[j].Values[2].ToString().Trim();
    //            i++;
    //        }
    //        j++;
    //    }

    //    try
    //    {
    //        objDC.MakeTransaction(cmd);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw (ex);
    //    }
    //    finally
    //    {
    //        cmd = null;
    //    }
    //}

    //public void UpdateEmpTravel(string strTravelId, string strEmpId, string strTravelStatus, string UpdatedBy, string UpdatedDate)
    //{
    //    SqlCommand cmd = new SqlCommand("proc_Update_EmpTravel");
    //    cmd.CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_TravelId = cmd.Parameters.Add("TravelId", SqlDbType.BigInt);
    //    p_TravelId.Direction = ParameterDirection.Input;
    //    p_TravelId.Value = strTravelId;

    //    SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = strEmpId;

    //    SqlParameter p_TravelStatus = cmd.Parameters.Add("TravelStatus", SqlDbType.Char);
    //    p_TravelStatus.Direction = ParameterDirection.Input;
    //    p_TravelStatus.Value = strTravelStatus;

    //    SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy.Direction = ParameterDirection.Input;
    //    p_InsertedBy.Value = UpdatedBy;

    //    SqlParameter p_InsertedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.Value = UpdatedDate;

    //    try
    //    {
    //        objDC.ExecuteQuery(cmd);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw (ex);
    //    }
    //    finally
    //    {
    //        cmd = null;
    //    }
    //}

    //public void UpdateTravelAppMstForApprove(string strTravelId, string strEmpId, string IsUpdate, string IsDelete, string TravelStatus,
    //    string TravelDates, string LTReason, string strInsBy, string strInsDate)
    //{

    //    int i = 0;
    //    char[] splitter = { ',' };

    //    string[] arinfo2 = new string[10];
    //    arinfo2 = Common.str_split(TravelDates, splitter);

    //    SqlCommand[] cmd;
    //    cmd = new SqlCommand[3 + (arinfo2.Length)];

    //    //Update Leave Application Mst For Approve
    //    cmd[0] = new SqlCommand("proc_Update_TravelAppMst");
    //    cmd[0].CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_TravelID = cmd[0].Parameters.Add("TravelID", SqlDbType.BigInt);
    //    p_TravelID.Direction = ParameterDirection.Input;
    //    p_TravelID.Value = Convert.ToInt32(strTravelId);

    //    SqlParameter p_EmpID = cmd[0].Parameters.Add("EmpID", SqlDbType.Char);
    //    p_EmpID.Direction = ParameterDirection.Input;
    //    p_EmpID.Value = strEmpId;

    //    SqlParameter p_TravelStatus = cmd[0].Parameters.Add("TravelStatus", SqlDbType.Char);
    //    p_TravelStatus.Direction = ParameterDirection.Input;
    //    p_TravelStatus.Value = TravelStatus;

    //    SqlParameter p_InsertedBy = cmd[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy.Direction = ParameterDirection.Input;
    //    p_InsertedBy.Value = strInsBy;

    //    SqlParameter p_InsertedDate = cmd[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.Value = strInsDate;

    //    SqlParameter p_IsUpdate = cmd[0].Parameters.Add("IsUpdate", SqlDbType.Char);
    //    p_IsUpdate.Direction = ParameterDirection.Input;
    //    p_IsUpdate.Value = "Y";

    //    int row = 1;
    //    int j = row;
    //    row = j;

    //    //Update Attendance for travel

    //    for (j = row; j < arinfo2.Length + row; j++)
    //    {
    //        cmd[j] = UpdateAttendanceForTravel(strEmpId, arinfo2[j - row], "TV", "", strInsBy, strInsDate);
    //    }

    //    try
    //    {
    //        objDC.MakeTransaction(cmd);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw (ex);
    //    }
    //    finally
    //    {
    //        cmd = null;
    //    }
    //}

    //public SqlCommand UpdateAttendanceForTravel(string strEmpId, string strAttndDate, string TravelStatus,
    //    string LTReason, string strInsBy, string strInsDate)
    //{
    //    SqlCommand cmd = new SqlCommand("proc_In_Or_Up_Attandance_TravelStatus");
    //    cmd.CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_EmpID = cmd.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpID.Direction = ParameterDirection.Input;
    //    p_EmpID.Value = strEmpId;

    //    SqlParameter p_AttndDate = cmd.Parameters.Add("AttndDate", SqlDbType.DateTime);
    //    p_AttndDate.Direction = ParameterDirection.Input;
    //    p_AttndDate.Value = strAttndDate;

    //    SqlParameter p_Status = cmd.Parameters.Add("Status", SqlDbType.Char);
    //    p_Status.Direction = ParameterDirection.Input;
    //    p_Status.Value = "TV";

    //    SqlParameter p_Remarks = cmd.Parameters.Add("Remarks", SqlDbType.VarChar);
    //    p_Remarks.Direction = ParameterDirection.Input;
    //    p_Remarks.Value = LTReason;

    //    SqlParameter p_InsertedBy2 = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy2.Direction = ParameterDirection.Input;
    //    p_InsertedBy2.Value = strInsBy;

    //    SqlParameter p_InsertedDate5 = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate5.Direction = ParameterDirection.Input;
    //    p_InsertedDate5.Value = Convert.ToDateTime(strInsDate);

    //    return cmd;
    //}

    //public void UpdateTravelAppMstForDeny(string strTravelId, string strEmpId, string IsUpdate, string IsDelete,
    //    string strTravelStatus, string strInsBy, string strInsDate)
    //{

    //    SqlCommand[] cmd;
    //    cmd = new SqlCommand[1];

    //    cmd[0] = new SqlCommand("proc_Update_TravelAppMst");
    //    cmd[0].CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_TravelID = cmd[0].Parameters.Add("TravelID", SqlDbType.BigInt);
    //    p_TravelID.Direction = ParameterDirection.Input;
    //    p_TravelID.Value = Convert.ToInt32(strTravelId);

    //    SqlParameter p_EmpID = cmd[0].Parameters.Add("EmpID", SqlDbType.Char);
    //    p_EmpID.Direction = ParameterDirection.Input;
    //    p_EmpID.Value = strEmpId;

    //    SqlParameter p_TravelStatus = cmd[0].Parameters.Add("TravelStatus", SqlDbType.Char);
    //    p_TravelStatus.Direction = ParameterDirection.Input;
    //    p_TravelStatus.Value = strTravelStatus;

    //    SqlParameter p_InsertedBy = cmd[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy.Direction = ParameterDirection.Input;
    //    p_InsertedBy.Value = strInsBy;

    //    SqlParameter p_InsertedDate = cmd[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.Value = strInsDate;

    //    SqlParameter p_IsUpdate = cmd[0].Parameters.Add("IsUpdate", SqlDbType.Char);
    //    p_IsUpdate.Direction = ParameterDirection.Input;
    //    p_IsUpdate.Value = "Y";
    //    try
    //    {
    //        objDC.MakeTransaction(cmd);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw (ex);
    //    }
    //    finally
    //    {
    //        cmd = null;
    //    }
    //}

    //public void CancelTravelApp(string strTravelId, string strEmpId, string IsUpdate, string IsDelete,
    //    string strTravelStatus, string strInsBy, string strInsDate)
    //{

    //    SqlCommand[] cmd;
    //    cmd = new SqlCommand[1];

    //    cmd[0] = new SqlCommand("proc_Update_TravelAppMst");
    //    cmd[0].CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_TravelId = cmd[0].Parameters.Add("TravelId", SqlDbType.BigInt);
    //    p_TravelId.Direction = ParameterDirection.Input;
    //    p_TravelId.Value = strTravelId;

    //    SqlParameter p_EmpID = cmd[0].Parameters.Add("EmpID", SqlDbType.Char);
    //    p_EmpID.Direction = ParameterDirection.Input;
    //    p_EmpID.Value = strEmpId;

    //    SqlParameter p_TravelStatus = cmd[0].Parameters.Add("TravelStatus", SqlDbType.Char);
    //    p_TravelStatus.Direction = ParameterDirection.Input;
    //    p_TravelStatus.Value = strTravelStatus;

    //    SqlParameter p_InsertedBy = cmd[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy.Direction = ParameterDirection.Input;
    //    p_InsertedBy.Value = strInsBy;

    //    SqlParameter p_InsertedDate = cmd[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.Value = strInsDate;

    //    SqlParameter p_IsUpdate = cmd[0].Parameters.Add("IsUpdate", SqlDbType.Char);
    //    p_IsUpdate.Direction = ParameterDirection.Input;
    //    p_IsUpdate.Value = "Y";
    //    try
    //    {
    //        objDC.MakeTransaction(cmd);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw (ex);
    //    }
    //    finally
    //    {
    //        cmd = null;
    //    }
    //}

    //public void UpdateTravelAppMstForCancel(clsEmpTravel Tra, string IsUpdate, string TravelStatus, string TravelDates)
    //{
    //    int i = 0;
    //    string[] arinfo = new string[10];
    //    char[] splitter = { ',' };
    //    arinfo = Common.str_split(TravelDates, splitter);

    //    SqlCommand[] cmd;
    //    cmd = new SqlCommand[3 + arinfo.Length];
    //    //DELETE FROM DETAILS TABLE                

    //    cmd[0] = new SqlCommand("proc_Update_EmpTravel_For_Cancel");
    //    cmd[0].CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_TravelId = cmd[0].Parameters.Add("TravelId", SqlDbType.BigInt);
    //    p_TravelId.Direction = ParameterDirection.Input;
    //    p_TravelId.Value = Convert.ToInt32(Tra.TravelId);

    //    SqlParameter p_EmpID = cmd[0].Parameters.Add("EmpID", SqlDbType.Char);
    //    p_EmpID.Direction = ParameterDirection.Input;
    //    p_EmpID.Value = Tra.EmpId;

    //    SqlParameter p_TravelStatus = cmd[0].Parameters.Add("TravelStatus", SqlDbType.Char);
    //    p_TravelStatus.Direction = ParameterDirection.Input;
    //    p_TravelStatus.Value = Tra.TravelStatus;

    //    SqlParameter p_InsertedBy = cmd[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy.Direction = ParameterDirection.Input;
    //    p_InsertedBy.Value = Tra.InsertedBy;

    //    SqlParameter p_InsertedDate = cmd[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.Value = Tra.InsertedDate;

    //    SqlParameter p_IsUpdate = cmd[0].Parameters.Add("IsUpdate", SqlDbType.Char);
    //    p_IsUpdate.Direction = ParameterDirection.Input;
    //    p_IsUpdate.Value = "Y";

    //    //Insert Or Update Travel Flag & Status in Attandance Table 
    //    int j;
    //    int row = 1;

    //    DataTable dtEmpLevDateDet = new DataTable();
    //    for (j = row; j < arinfo.Length + row; j++)
    //    {
    //        cmd[j] = new SqlCommand("proc_In_Or_Up_Attandance_TravelStatus");
    //        cmd[j].CommandType = CommandType.StoredProcedure;

    //        p_EmpID = cmd[j].Parameters.Add("EmpId", SqlDbType.Char);
    //        p_EmpID.Direction = ParameterDirection.Input;
    //        p_EmpID.Value = Tra.EmpId;

    //        SqlParameter p_AttndDate = cmd[j].Parameters.Add("AttndDate", SqlDbType.DateTime);
    //        p_AttndDate.Direction = ParameterDirection.Input;
    //        p_AttndDate.Value = arinfo[j - row];

    //        SqlParameter p_Status = cmd[j].Parameters.Add("Status", SqlDbType.Char);
    //        p_Status.Direction = ParameterDirection.Input;
    //        p_Status.Value = "A";

    //        SqlParameter p_Remarks = cmd[j].Parameters.Add("Remarks", SqlDbType.VarChar);
    //        p_Remarks.Direction = ParameterDirection.Input;
    //        p_Remarks.Value = "";

    //        p_InsertedBy = cmd[j].Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //        p_InsertedBy.Direction = ParameterDirection.Input;
    //        p_InsertedBy.Value = Tra.InsertedBy;

    //        p_InsertedDate = cmd[j].Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //        p_InsertedDate.Direction = ParameterDirection.Input;
    //        p_InsertedDate.Value = Tra.InsertedDate;
    //    }

    //    try
    //    {
    //        objDC.MakeTransaction(cmd);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw (ex);
    //    }
    //    finally
    //    {
    //        cmd = null;
    //    }
    //}
    //#endregion


    //#region Select Method for Employee Travel Screen

    //public DataTable SelectRequestTravelAppMst(Int32 TravelId, string EmpId, string TravelStatus,
    //    string reportingTo,string strIsCDir)
    //{
    //    SqlCommand command = new SqlCommand("proc_Select_TravelAppMst");
    //    SqlParameter p_TravelId = command.Parameters.Add("TravelId", SqlDbType.BigInt);
    //    p_TravelId.Direction = ParameterDirection.Input;
    //    p_TravelId.Value = TravelId;

    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = EmpId;

    //    SqlParameter p_TravelStatus = command.Parameters.Add("TravelStatus", SqlDbType.Char);
    //    p_TravelStatus.Direction = ParameterDirection.Input;
    //    p_TravelStatus.Value = TravelStatus;

    //    SqlParameter p_reportingTo = command.Parameters.Add("reportingTo", SqlDbType.Char);
    //    p_reportingTo.Direction = ParameterDirection.Input;
    //    p_reportingTo.Value = reportingTo;

    //    SqlParameter p_IsCountryDirector = command.Parameters.Add("IsCountryDirector", SqlDbType.Char);
    //    p_IsCountryDirector.Direction = ParameterDirection.Input;
    //    p_IsCountryDirector.Value = strIsCDir;

    //    if (TravelStatus == "A")
    //    {
    //        objDC.CreateDSFromProc(command, "TravelAppMstApprove");
    //        return objDC.ds.Tables["TravelAppMstApprove"];
    //    }
    //    else if (TravelStatus == "R")
    //    {
    //        objDC.CreateDSFromProc(command, "TravelAppMstReq");
    //        return objDC.ds.Tables["TravelAppMstReq"];
    //    }
    //    else if (TravelStatus == "D")
    //    {
    //        objDC.CreateDSFromProc(command, "TravelAppMstDeny");
    //        return objDC.ds.Tables["TravelAppMstDeny"];
    //    }
    //    else if (TravelStatus == "RD")
    //    {
    //        objDC.CreateDSFromProc(command, "TravelAppMstReqDeny");
    //        return objDC.ds.Tables["TravelAppMstReqDeny"];
    //    }
    //    else
    //    {
    //        objDC.CreateDSFromProc(command, "TravelAppMstReqDeny");
    //        return objDC.ds.Tables["TravelAppMstReqDeny"];
    //    }
    //}

    //public DataTable SelectRequestTravelAppMstYearWise(Int32 TravelId, string EmpId, string TravelStatus,
    //    string reportingTo, string strIsCDir,string strYear)
    //{
    //    SqlCommand command = new SqlCommand("proc_Select_TravelAppMst_Year_Wise");
    //    SqlParameter p_TravelId = command.Parameters.Add("TravelId", SqlDbType.BigInt);
    //    p_TravelId.Direction = ParameterDirection.Input;
    //    p_TravelId.Value = TravelId;

    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = EmpId;

    //    SqlParameter p_TravelStatus = command.Parameters.Add("TravelStatus", SqlDbType.Char);
    //    p_TravelStatus.Direction = ParameterDirection.Input;
    //    p_TravelStatus.Value = TravelStatus;

    //    SqlParameter p_reportingTo = command.Parameters.Add("reportingTo", SqlDbType.Char);
    //    p_reportingTo.Direction = ParameterDirection.Input;
    //    p_reportingTo.Value = reportingTo;

    //    SqlParameter p_IsCountryDirector = command.Parameters.Add("IsCountryDirector", SqlDbType.Char);
    //    p_IsCountryDirector.Direction = ParameterDirection.Input;
    //    p_IsCountryDirector.Value = strIsCDir;

    //    SqlParameter p_Year = command.Parameters.Add("Year", SqlDbType.BigInt);
    //    p_Year.Direction = ParameterDirection.Input;
    //    p_Year.Value = strYear;

    //    if (TravelStatus == "A")
    //    {
    //        objDC.CreateDSFromProc(command, "TravelAppMstApprove");
    //        return objDC.ds.Tables["TravelAppMstApprove"];
    //    }
    //    else if (TravelStatus == "R")
    //    {
    //        objDC.CreateDSFromProc(command, "TravelAppMstReq");
    //        return objDC.ds.Tables["TravelAppMstReq"];
    //    }
    //    else if (TravelStatus == "D")
    //    {
    //        objDC.CreateDSFromProc(command, "TravelAppMstDeny");
    //        return objDC.ds.Tables["TravelAppMstDeny"];
    //    }
    //    else if (TravelStatus == "RD")
    //    {
    //        objDC.CreateDSFromProc(command, "TravelAppMstReqDeny");
    //        return objDC.ds.Tables["TravelAppMstReqDeny"];
    //    }
    //    else
    //    {
    //        objDC.CreateDSFromProc(command, "TravelAppMstReqDeny");
    //        return objDC.ds.Tables["TravelAppMstReqDeny"];
    //    }
    //}

    //public DataTable SelectDivisionWiseEmp(string EmpId)
    //{
    //    SqlCommand command = new SqlCommand("proc_Select_DivisionWiseEmp");

    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = EmpId;

    //    objDC.CreateDSFromProc(command, "DivisionWiseEmp");
    //    return objDC.ds.Tables["DivisionWiseEmp"];
    //}

    //public DataTable SelectAdminEmp(string EmpId)
    //{
    //    SqlCommand command = new SqlCommand("proc_Select_AdminDeptEmp2");

    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = EmpId;

    //    objDC.CreateDSFromProc(command, "AdminEmp");
    //    return objDC.ds.Tables["AdminEmp"];
    //}

    //public DataTable SelectEmpTravel(string TravelId, string EmpId, string TravelStatus)
    //{
    //    SqlCommand command = new SqlCommand("proc_Select_EmpTravel");

    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = EmpId;

    //    SqlParameter p_TravelId = command.Parameters.Add("TravelId", SqlDbType.BigInt);
    //    p_TravelId.Direction = ParameterDirection.Input;
    //    p_TravelId.Value = Convert.ToInt32(TravelId);

    //    SqlParameter p_TravelStatus = command.Parameters.Add("TravelStatus", SqlDbType.Char);
    //    p_TravelStatus.Direction = ParameterDirection.Input;
    //    p_TravelStatus.Value = TravelStatus;

    //    objDC.CreateDSFromProc(command, "EmpTravel");
    //    return objDC.ds.Tables["EmpTravel"];
    //}

    //public DataTable SelectEmpTravelRpt(string TravelId, string EmpId, string TravelStatus)
    //{
    //    SqlCommand command = new SqlCommand("proc_Select_EmpTravelRpt");

    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = EmpId;

    //    SqlParameter p_TravelId = command.Parameters.Add("TravelId", SqlDbType.BigInt);
    //    p_TravelId.Direction = ParameterDirection.Input;
    //    p_TravelId.Value = Convert.ToInt32(TravelId);

    //    SqlParameter p_TravelStatus = command.Parameters.Add("TravelStatus", SqlDbType.Char);
    //    p_TravelStatus.Direction = ParameterDirection.Input;
    //    p_TravelStatus.Value = TravelStatus;

    //    objDC.CreateDSFromProc(command, "EmpTravel");
    //    return objDC.ds.Tables["EmpTravel"];
    //}


    //public DataTable SelectEmpTravelDateRangeRpt(string TravelId, string EmpId, string TravelStatus,string strStart,string strEnd,string strDivId,string strDeptId)
    //{
    //    SqlCommand command = new SqlCommand("proc_Select_EmpTravelDateRangeRpt");

    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = EmpId;

    //    SqlParameter p_TravelId = command.Parameters.Add("TravelId", SqlDbType.BigInt);
    //    p_TravelId.Direction = ParameterDirection.Input;
    //    p_TravelId.Value = Convert.ToInt32(TravelId);

    //    SqlParameter p_TravelStatus = command.Parameters.Add("TravelStatus", SqlDbType.Char);
    //    p_TravelStatus.Direction = ParameterDirection.Input;
    //    p_TravelStatus.Value = TravelStatus;

    //    SqlParameter p_TravelStart = command.Parameters.Add("TravelStart", SqlDbType.DateTime);
    //    p_TravelStart.Direction = ParameterDirection.Input;
    //    p_TravelStart.Value = strStart;

    //    SqlParameter p_TravelEnd = command.Parameters.Add("TravelEnd", SqlDbType.DateTime);
    //    p_TravelEnd.Direction = ParameterDirection.Input;
    //    p_TravelEnd.Value = strEnd;

    //    SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
    //    p_DivisionID.Direction = ParameterDirection.Input;
    //    p_DivisionID.Value =strDivId;

    //    SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
    //    p_DeptID.Direction = ParameterDirection.Input;
    //    p_DeptID.Value = strDeptId;

    //    objDC.CreateDSFromProc(command, "EmpTravelDateRange");
    //    return objDC.ds.Tables["EmpTravelDateRange"];
    //}


    //public DataTable SelectEmpTravelCopyTo(string strTravelId)
    //{
    //    SqlCommand command = new SqlCommand("proc_Select_EMPTRAVELCOPYTO");

    //    SqlParameter p_TRAVELID = command.Parameters.Add("TRAVELID", SqlDbType.BigInt);
    //    p_TRAVELID.Direction = ParameterDirection.Input;
    //    p_TRAVELID.Value = strTravelId;

    //    objDC.CreateDSFromProc(command, "EmpTravelCopyTo");
    //    return objDC.ds.Tables["EmpTravelCopyTo"];
    //}
    //public DataTable SelectEmpTravelDateDetails(Int32 TravelId, string EmpId, string TravelDate)
    //{
    //    SqlCommand command = new SqlCommand("proc_Select_EmpTravelDate");
    //    command.CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_TravelId = command.Parameters.Add("TravelId", SqlDbType.BigInt);
    //    p_TravelId.Direction = ParameterDirection.Input;
    //    p_TravelId.Value = TravelId;

    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = EmpId;

    //    SqlParameter p_TravelDate = command.Parameters.Add("TravelDate", SqlDbType.DateTime);
    //    p_TravelDate.Direction = ParameterDirection.Input;
    //    p_TravelDate.Value = TravelDate;

    //    objDC.CreateDSFromProc(command, "TravelDetDate");
    //    return objDC.ds.Tables["TravelDetDate"];
    //}

    //public string getApproverDetails(string strUserId)
    //{
    //    string strSQL = "SELECT E.FULLNAME + ', ' + J.JOBTITLE AS APPROVER "
    //                + "  FROM USERINFO U,EMPINFO E, JOBTITLE J "
    //                + " WHERE U.EMPID=E.EMPID AND E.DESGID=J.JBTLID AND UPPER(U.USERID)=@USERID ";
    //    SqlCommand cmd = new SqlCommand(strSQL);
    //    cmd.CommandType = CommandType.Text;

    //    SqlParameter p_USERID = cmd.Parameters.Add("USERID", SqlDbType.Char);
    //    p_USERID.Direction = ParameterDirection.Input;
    //    p_USERID.Value = strUserId.ToUpper();

    //    return objDC.GetScalarVal(cmd);

    //}

    //public DataTable GetDistinctYearForApproval()
    //{
    //    string strSQL = "SELECT DISTINCT YEAR(AppDate) AS APPYEAR FROM EmpTravel ORDER BY YEAR(AppDate) ";
    //   return objDC.CreateDT(strSQL, "GetDistinctYearForApproval");
    //}
    //#endregion
}
