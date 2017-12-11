using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for LeaveApplicationManager
/// </summary>
public class LeaveApplicationManager
{
    DBConnector objDC = new DBConnector();
    #region Insert Update Delete From Leave Application By Store procedure
    public void InsertLeaveAppMst(LeaveApp Lv, string IsUpdate, string IsDelete, string LeaveStatus,
       string LeaveEnjoyed, string LeaveDates, string LeaveAbbrName, double totLeaveDays,
       string PreLTypeId, string PreLeaveEnjoyed, string PreLeaveDates, string LvTypeNature,
       string strIsPostApprove)
    {
        int i = 0;
        char[] splitter = { ',' };
        decimal dclLDuration = 0;

        string[] arinfo = new string[10];
        arinfo = Common.str_split(LeaveDates, splitter);

        string[] arinfo2 = new string[10];
        arinfo2 = Common.str_split(LeaveDates, splitter);

        string[] arinfoPre = new string[10];
        arinfoPre = Common.str_split(PreLeaveDates, splitter);
               
        SqlCommand[] cmd;
        if (PreLeaveDates != "")            
            cmd = new SqlCommand[4 + (arinfo.Length * 2) + (arinfoPre.Length * 2)];
        else            
            cmd = new SqlCommand[4 + (arinfo.Length * 2)];

        //DELETE FROM DETAILS TABLE        
        cmd[0] = new SqlCommand("proc_Delete_LeaveAppDet");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_LvAppID = cmd[0].Parameters.Add("LvAppID", SqlDbType.BigInt);
        p_LvAppID.Direction = ParameterDirection.Input;
        p_LvAppID.Value = Convert.ToInt32(Lv.LvAppID);

        cmd[1] = new SqlCommand("proc_Insert_LeaveAppMst");
        cmd[1].CommandType = CommandType.StoredProcedure;

        p_LvAppID = cmd[1].Parameters.Add("LvAppID", SqlDbType.BigInt);
        p_LvAppID.Direction = ParameterDirection.Input;
        p_LvAppID.Value = Convert.ToInt32(Lv.LvAppID);

        SqlParameter p_EmpID = cmd[1].Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = Lv.EmpID;

        SqlParameter p_Ltype = cmd[1].Parameters.Add("LTypeId", SqlDbType.BigInt);
        p_Ltype.Direction = ParameterDirection.Input;
        p_Ltype.Value = Convert.ToInt32(Lv.LTypeId);

        SqlParameter p_AppDate = cmd[1].Parameters.Add("AppDate", SqlDbType.VarChar);
        p_AppDate.Direction = ParameterDirection.Input;
        p_AppDate.Value = Common.ReturnDate(Lv.AppDate);

        SqlParameter p_LeaveStart = cmd[1].Parameters.Add("LeaveStart", SqlDbType.VarChar);
        p_LeaveStart.Direction = ParameterDirection.Input;
        p_LeaveStart.Value = Common.ReturnDate(Lv.LeaveStart);

        SqlParameter p_LeaveEnd = cmd[1].Parameters.Add("LeaveEnd", SqlDbType.VarChar);
        p_LeaveEnd.Direction = ParameterDirection.Input;
        p_LeaveEnd.Value = Common.ReturnDate(Lv.LeaveEnd);

        SqlParameter p_LDurInDays = cmd[1].Parameters.Add("LDurInDays", SqlDbType.Decimal);
        p_LDurInDays.Direction = ParameterDirection.Input;
        p_LDurInDays.Value = Convert.ToDecimal(Lv.LDurInDays);

        SqlParameter p_LTReason = cmd[1].Parameters.Add("LTReason", SqlDbType.VarChar);
        p_LTReason.Direction = ParameterDirection.Input;
        p_LTReason.Value = Lv.LTReason;

        SqlParameter p_AddrAtLeave = cmd[1].Parameters.Add("AddrAtLeave", SqlDbType.VarChar);
        p_AddrAtLeave.Direction = ParameterDirection.Input;
        p_AddrAtLeave.Value = Lv.AddrAtLeave;

        SqlParameter p_PhoneNo = cmd[1].Parameters.Add("PhoneNo", SqlDbType.Char);
        p_PhoneNo.Direction = ParameterDirection.Input;
        p_PhoneNo.Value = Lv.PhoneNo;

        SqlParameter p_AppStatus = cmd[1].Parameters.Add("AppStatus", SqlDbType.Char);
        p_AppStatus.Direction = ParameterDirection.Input;
        p_AppStatus.Value = Lv.AppStatus;

        SqlParameter p_IsHalfDay = cmd[1].Parameters.Add("IsHalfDay", SqlDbType.Char);
        p_IsHalfDay.Direction = ParameterDirection.Input;
        p_IsHalfDay.Value = Lv.IsHalfDay;

        SqlParameter p_ResumeDate = cmd[1].Parameters.Add("ResumeDate", SqlDbType.DateTime);
        p_ResumeDate.Direction = ParameterDirection.Input;
        p_ResumeDate.Value = Common.ReturnDate(Lv.ResumeDate);

        SqlParameter p_ResponsiveEmpId = cmd[1].Parameters.Add("ResponsiveEmpId", SqlDbType.VarChar);
        p_ResponsiveEmpId.Direction = ParameterDirection.Input;
        p_ResponsiveEmpId.Value = Lv.ResponsiveEmpId;

        SqlParameter p_InsertedBy = cmd[1].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = Lv.InsertedBy;

        SqlParameter p_InsertedDate = cmd[1].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = Lv.InsertedDate;

        SqlParameter p_FiscalYrId = cmd[1].Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = Lv.FiscalYrId;

        SqlParameter p_IsUpdate = cmd[1].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = Lv.IsUpdate;

        SqlParameter p_IsDelete = cmd[1].Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = Lv.IsDelete;

        //Insert Into Leave Application Details Table        

        for (i = 2; i < arinfo.Length + 2; i++)
        {
            if (arinfo[i - 2] != "")
            {
                cmd[i] = new SqlCommand("proc_Insert_LeaveAppDet");
                cmd[i].CommandType = CommandType.StoredProcedure;

                p_LvAppID = cmd[i].Parameters.Add("LvAppID", SqlDbType.BigInt);
                p_LvAppID.Direction = ParameterDirection.Input;
                p_LvAppID.Value = Convert.ToInt32(Lv.LvAppID);

                p_EmpID = cmd[i].Parameters.Add("EmpID", SqlDbType.Char);
                p_EmpID.Direction = ParameterDirection.Input;
                p_EmpID.Value = Lv.EmpID;

                SqlParameter p_LevDate = cmd[i].Parameters.Add("LevDate", SqlDbType.DateTime);
                p_LevDate.Direction = ParameterDirection.Input;
                p_LevDate.Value = arinfo[i - 2];

                p_Ltype = cmd[i].Parameters.Add("LTypeId", SqlDbType.BigInt);
                p_Ltype.Direction = ParameterDirection.Input;
                p_Ltype.Value = Convert.ToInt32(Lv.LTypeId);

                SqlParameter p_Duration = cmd[i].Parameters.Add("Duration", SqlDbType.Decimal);
                p_Duration.Direction = ParameterDirection.Input;
                if (dclLDuration == 0)
                    dclLDuration = Convert.ToDecimal(Lv.Duration);
                else
                    dclLDuration = dclLDuration - 1;

                if (dclLDuration >= 1)
                    p_Duration.Value = "1";
                else                
                    p_Duration.Value = "0.50";                          

                p_InsertedBy = cmd[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                p_InsertedBy.Direction = ParameterDirection.Input;
                p_InsertedBy.Value = Lv.InsertedBy;

                p_InsertedDate = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                p_InsertedDate.Direction = ParameterDirection.Input;
                p_InsertedDate.Value = Lv.InsertedDate;
            }
        }
        int j = 0;
        if (strIsPostApprove == "Y")
        {
            //Update Leave Profile            
            cmd[i] = UpdateLeaveProfile("N", Lv.EmpID, Lv.LTypeId, LeaveEnjoyed, Lv.InsertedBy, Lv.InsertedDate);
            i++;         

            //Insert Or Update Leave Flag & Status in Attandance Table 
            for (j = 0; j < arinfo.Length; j++)
            {
                cmd[i] = UpdateAttendanceForLeave(Lv.EmpID, arinfo[j], LeaveAbbrName, Lv.InsertedBy, Lv.InsertedDate);
                i++;
            }
        }
        try
        {
            objDC.MakeTransaction(cmd);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            cmd = null;
        }
    }

    public SqlCommand UpdateLeaveProfile(string strPreYrLv, string strEmpId, string strLTypeId, string strLvEnjoyed, string strInsBy, string strInsDate)
    {
        SqlCommand cmd;
        cmd = new SqlCommand("proc_Update_EmpLeaveProfile");

        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpId;

        SqlParameter p_Ltype = cmd.Parameters.Add("LTypeID", SqlDbType.BigInt);
        p_Ltype.Direction = ParameterDirection.Input;
        p_Ltype.Value = Convert.ToInt32(strLTypeId);

        SqlParameter p_LeaveEnjoyed1 = cmd.Parameters.Add("LeaveEnjoyed", SqlDbType.Decimal);
        p_LeaveEnjoyed1.Direction = ParameterDirection.Input;
        p_LeaveEnjoyed1.Value = Convert.ToDecimal(strLvEnjoyed);

        SqlParameter p_InsertedBy2 = cmd.Parameters.Add("UpdatedBy", SqlDbType.VarChar);
        p_InsertedBy2.Direction = ParameterDirection.Input;
        p_InsertedBy2.Value = strInsBy;

        SqlParameter p_InsertedDate2 = cmd.Parameters.Add("UpdatedDate", SqlDbType.DateTime);
        p_InsertedDate2.Direction = ParameterDirection.Input;
        p_InsertedDate2.Value = strInsDate;

        return cmd;
    }
    public SqlCommand UpdateAttendanceForLeave(string strEmpId, string strAttndDate, string strLeaveAbbrName, string strInsBy, string strInsDate)
    {
        SqlCommand cmd = new SqlCommand("proc_Update_Attandance_LeaveStatus");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpId;

        SqlParameter p_AttndDate = cmd.Parameters.Add("AttndDate", SqlDbType.DateTime);
        p_AttndDate.Direction = ParameterDirection.Input;
        p_AttndDate.Value = strAttndDate;

        SqlParameter p_Status = cmd.Parameters.Add("Status", SqlDbType.Char);
        p_Status.Direction = ParameterDirection.Input;
        p_Status.Value = "LV";

        SqlParameter p_LeaveFlag = cmd.Parameters.Add("LeaveFlag", SqlDbType.Char);
        p_LeaveFlag.Direction = ParameterDirection.Input;
        p_LeaveFlag.Value = strLeaveAbbrName;

        SqlParameter p_Remarks = cmd.Parameters.Add("Remarks", SqlDbType.VarChar);
        p_Remarks.Direction = ParameterDirection.Input;
        p_Remarks.Value = "";

        SqlParameter p_InsertedBy4 = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy4.Direction = ParameterDirection.Input;
        p_InsertedBy4.Value = strInsBy;

        SqlParameter p_InsertedDate5 = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate5.Direction = ParameterDirection.Input;
        p_InsertedDate5.Value = Convert.ToDateTime(strInsDate);

        return cmd;
    }

    public void UpdateLeaveAppMstForDeny(string strLvAppId, string strEmpId, string IsUpdate, string IsDelete,
        string AppStatus, string strInsBy, string strInsDate)
    {

        SqlCommand[] cmd;
        cmd = new SqlCommand[1];

        cmd[0] = new SqlCommand("proc_Update_LeaveAppMst");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_LvAppID = cmd[0].Parameters.Add("LvAppID", SqlDbType.BigInt);
        p_LvAppID.Direction = ParameterDirection.Input;
        p_LvAppID.Value = strLvAppId;

        SqlParameter p_EmpID = cmd[0].Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpId;

        SqlParameter p_AppStatus = cmd[0].Parameters.Add("AppStatus", SqlDbType.Char);
        p_AppStatus.Direction = ParameterDirection.Input;
        p_AppStatus.Value = AppStatus;

        SqlParameter p_RecommendBy = cmd[0].Parameters.Add("RecommendBy", SqlDbType.Char);
        p_RecommendBy.Direction = ParameterDirection.Input;
        p_RecommendBy.Value = "";

        SqlParameter p_ApprovedBy = cmd[0].Parameters.Add("ApprovedBy", SqlDbType.Char);
        p_ApprovedBy.Direction = ParameterDirection.Input;
        p_ApprovedBy.Value = "";

        SqlParameter p_InsertedBy = cmd[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = cmd[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        SqlParameter p_IsUpdate = cmd[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = "Y";

        try
        {
            objDC.MakeTransaction(cmd);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            cmd = null;
        }
    }

    public void CancelLeaveApp(string strLvAppId, string strEmpId, string IsUpdate, string IsDelete,
        string AppStatus, string strInsBy, string strInsDate)
    {

        SqlCommand[] cmd;
        cmd = new SqlCommand[1];

        cmd[0] = new SqlCommand("proc_Update_LeaveAppMst");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_LvAppID = cmd[0].Parameters.Add("LvAppID", SqlDbType.BigInt);
        p_LvAppID.Direction = ParameterDirection.Input;
        p_LvAppID.Value = strLvAppId;

        SqlParameter p_EmpID = cmd[0].Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpId;

        SqlParameter p_AppStatus = cmd[0].Parameters.Add("AppStatus", SqlDbType.Char);
        p_AppStatus.Direction = ParameterDirection.Input;
        p_AppStatus.Value = AppStatus;

        SqlParameter p_RecommendBy = cmd[0].Parameters.Add("RecommendBy", SqlDbType.Char);
        p_RecommendBy.Direction = ParameterDirection.Input;
        p_RecommendBy.Value = "";

        SqlParameter p_ApprovedBy = cmd[0].Parameters.Add("ApprovedBy", SqlDbType.Char);
        p_ApprovedBy.Direction = ParameterDirection.Input;
        p_ApprovedBy.Value = "";

        SqlParameter p_InsertedBy = cmd[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = cmd[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        SqlParameter p_IsUpdate = cmd[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = "Y";
        try
        {
            objDC.MakeTransaction(cmd);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            cmd = null;
        }
    }

    public void UpdateLeaveAppMstForCancel(LeaveApp Lv, string IsUpdate, string LeaveStatus, string LeaveEnjoyed, string LeaveDates)
    {
        int i = 0;
        string[] arinfo = new string[10];
        char[] splitter = { ',' };
        arinfo = Common.str_split(LeaveDates, splitter);

        SqlCommand[] cmd;
        cmd = new SqlCommand[4 + arinfo.Length];
        //DELETE FROM DETAILS TABLE        
        cmd[0] = new SqlCommand("proc_Delete_LeaveAppDet");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_LvAppID = cmd[0].Parameters.Add("LvAppID", SqlDbType.BigInt);
        p_LvAppID.Direction = ParameterDirection.Input;
        p_LvAppID.Value = Convert.ToInt32(Lv.LvAppID);

        cmd[1] = new SqlCommand("proc_Update_LeaveAppMst");
        cmd[1].CommandType = CommandType.StoredProcedure;

        p_LvAppID = cmd[1].Parameters.Add("LvAppID", SqlDbType.BigInt);
        p_LvAppID.Direction = ParameterDirection.Input;
        p_LvAppID.Value = Convert.ToInt32(Lv.LvAppID);

        SqlParameter p_EmpID = cmd[1].Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = Lv.EmpID;

        SqlParameter p_AppStatus = cmd[1].Parameters.Add("AppStatus", SqlDbType.Char);
        p_AppStatus.Direction = ParameterDirection.Input;
        p_AppStatus.Value = Lv.AppStatus;

        SqlParameter p_RecommendBy = cmd[1].Parameters.Add("RecommendBy", SqlDbType.Char);
        p_RecommendBy.Direction = ParameterDirection.Input;
        p_RecommendBy.Value = "";

        SqlParameter p_ApprovedBy = cmd[1].Parameters.Add("ApprovedBy", SqlDbType.Char);
        p_ApprovedBy.Direction = ParameterDirection.Input;
        p_ApprovedBy.Value = "";

        SqlParameter p_InsertedBy = cmd[1].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = Lv.InsertedBy;

        SqlParameter p_InsertedDate = cmd[1].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = Lv.InsertedDate;

        SqlParameter p_IsUpdate = cmd[1].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = "Y";        

        //Update Leave Profile
        cmd[2] = new SqlCommand("proc_Update_EmpLeaveProfile");
        cmd[2].CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpId = cmd[2].Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = Lv.EmpID;

        SqlParameter p_LTypeID = cmd[2].Parameters.Add("LTypeID", SqlDbType.BigInt);
        p_LTypeID.Direction = ParameterDirection.Input;
        p_LTypeID.Value = Convert.ToInt32(Lv.LTypeId);

        SqlParameter p_LeaveEnjoyed = cmd[2].Parameters.Add("LeaveEnjoyed", SqlDbType.Decimal);
        p_LeaveEnjoyed.Direction = ParameterDirection.Input;
        p_LeaveEnjoyed.Value = Convert.ToDecimal(LeaveEnjoyed);

        p_InsertedBy = cmd[2].Parameters.Add("UpdatedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = Lv.InsertedBy;

        p_InsertedDate = cmd[2].Parameters.Add("UpdatedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = Lv.InsertedDate;

        //Insert Or Update Leave Flag & Status in Attandance Table 
        int j;
        int row = 3;

        DataTable dtEmpLevDateDet = new DataTable();
        for (j = row; j < arinfo.Length + row; j++)
        {
            cmd[j] = new SqlCommand("proc_Update_Attandance_LeaveStatus");
            cmd[j].CommandType = CommandType.StoredProcedure;

            p_EmpID = cmd[j].Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = Lv.EmpID;

            SqlParameter p_AttndDate = cmd[j].Parameters.Add("AttndDate", SqlDbType.DateTime);
            p_AttndDate.Direction = ParameterDirection.Input;
            p_AttndDate.Value = arinfo[j - row];

            SqlParameter p_Status = cmd[j].Parameters.Add("Status", SqlDbType.Char);
            p_Status.Direction = ParameterDirection.Input;
            p_Status.Value = "A";

            SqlParameter p_LeaveFlag = cmd[j].Parameters.Add("LeaveFlag", SqlDbType.Char);
            p_LeaveFlag.Direction = ParameterDirection.Input;
            p_LeaveFlag.Value = "";

            SqlParameter p_Remarks = cmd[j].Parameters.Add("Remarks", SqlDbType.VarChar);
            p_Remarks.Direction = ParameterDirection.Input;
            p_Remarks.Value = "";

            SqlParameter p_LateTimeAmt = cmd[j].Parameters.Add("LateTimeAmt", SqlDbType.BigInt);
            p_LateTimeAmt.Direction = ParameterDirection.Input;
            p_LateTimeAmt.Value = 0;           

            p_InsertedBy = cmd[j].Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = Lv.InsertedBy;

            p_InsertedDate = cmd[j].Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate.Direction = ParameterDirection.Input;
            p_InsertedDate.Value = Lv.InsertedDate;
        }

        try
        {
            objDC.MakeTransaction(cmd);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            cmd = null;
        }
    }

    public void UpdateLeaveAppMstForApprove(string strLvAppId, string strEmpId, string IsUpdate, string IsDelete, 
        string AppStatus, string LeaveEnjoyed, string LeaveDates, string LeaveAbbrName, string LTypeId,
        string LTReason,string strInsBy, string strInsDate, string strPreYrLv,string strDuration)
    {

        int i = 0;
        char[] splitter = { ',' };

        string[] arinfo2 = new string[10];
        arinfo2 = Common.str_split(LeaveDates, splitter);

        SqlCommand[] cmd;
        cmd = new SqlCommand[3 + (arinfo2.Length)];

        //Update Leave Application Mst For Approve
        cmd[0] = new SqlCommand("proc_Update_LeaveAppMst");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_LvAppID = cmd[0].Parameters.Add("LvAppID", SqlDbType.BigInt);
        p_LvAppID.Direction = ParameterDirection.Input;
        p_LvAppID.Value = Convert.ToInt32(strLvAppId);

        SqlParameter p_EmpID = cmd[0].Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpId;

        SqlParameter p_AppStatus = cmd[0].Parameters.Add("AppStatus", SqlDbType.Char);
        p_AppStatus.Direction = ParameterDirection.Input;
        p_AppStatus.Value = AppStatus;

        SqlParameter p_RecommendBy = cmd[0].Parameters.Add("RecommendBy", SqlDbType.Char);
        p_RecommendBy.Direction = ParameterDirection.Input;
        p_RecommendBy.Value = strInsBy;

        SqlParameter p_ApprovedBy = cmd[0].Parameters.Add("ApprovedBy", SqlDbType.Char);
        p_ApprovedBy.Direction = ParameterDirection.Input;
        p_ApprovedBy.Value = strInsBy;

        SqlParameter p_InsertedBy = cmd[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = cmd[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        SqlParameter p_IsUpdate = cmd[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = "Y";

        //Update Leave Profile
        cmd[1] = UpdateLeaveProfile(strPreYrLv, strEmpId, LTypeId, LeaveEnjoyed, strInsBy, strInsDate);

        ////Insert Or Update Leave Flag & Status in Attandance Table 
        int row = 2;
        int j = row;

        row = j;
        
        decimal dclTotDuration=Convert.ToDecimal(strDuration);
        decimal dclDuration=0;
        for (j = row; j < arinfo2.Length + row; j++)
        {
            if (dclTotDuration >= 1)
                dclDuration = 1;
            else
                dclDuration = dclTotDuration;
            
            cmd[j] = UpdateAttendanceForLeave(strEmpId, arinfo2[j - row], LeaveAbbrName, LTReason, dclDuration.ToString() ,strInsBy, strInsDate);

            dclTotDuration = dclTotDuration - dclDuration;
        }

        try
        {
            objDC.MakeTransaction(cmd);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            cmd = null;
        }
    }

    public void UpdateLeaveAppMstForRecommendation(string strLvAppId, string strEmpId, string AppStatus, string strApprovedBy, 
        string strInsBy, string strInsDate)
    {
        SqlCommand[] cmd;
        cmd = new SqlCommand[1];

        //Update Leave Application Mst For Recommendation
        cmd[0] = new SqlCommand("proc_Update_LeaveAppMst");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_LvAppID = cmd[0].Parameters.Add("LvAppID", SqlDbType.BigInt);
        p_LvAppID.Direction = ParameterDirection.Input;
        p_LvAppID.Value = Convert.ToInt32(strLvAppId);

        SqlParameter p_EmpID = cmd[0].Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpId;

        SqlParameter p_RecommendBy = cmd[0].Parameters.Add("RecommendBy", SqlDbType.Char);
        p_RecommendBy.Direction = ParameterDirection.Input;
        p_RecommendBy.Value = strInsBy;

        SqlParameter p_ApprovedBy = cmd[0].Parameters.Add("ApprovedBy", SqlDbType.Char);
        p_ApprovedBy.Direction = ParameterDirection.Input;
        p_ApprovedBy.Value = strApprovedBy;
               
        SqlParameter p_AppStatus = cmd[0].Parameters.Add("AppStatus", SqlDbType.Char);
        p_AppStatus.Direction = ParameterDirection.Input;
        p_AppStatus.Value = AppStatus;

        SqlParameter p_InsertedBy = cmd[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = cmd[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        SqlParameter p_IsUpdate = cmd[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = "Y";

        try
        {
            objDC.MakeTransaction(cmd);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            cmd = null;
        }
    }

    public SqlCommand UpdateAttendanceForLeave(string strEmpId, string strAttndDate, string strLeaveAbbrName,
        string LTReason,string strDuration, string strInsBy, string strInsDate)
    {
        SqlCommand cmd = new SqlCommand("proc_Update_Attandance_LeaveStatus");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpId;

        SqlParameter p_AttndDate = cmd.Parameters.Add("AttndDate", SqlDbType.DateTime);
        p_AttndDate.Direction = ParameterDirection.Input;
        p_AttndDate.Value = strAttndDate;

        SqlParameter p_Status = cmd.Parameters.Add("Status", SqlDbType.Char);
        p_Status.Direction = ParameterDirection.Input;
        p_Status.Value = "LV";
        
        SqlParameter p_LeaveFlag = cmd.Parameters.Add("LeaveFlag", SqlDbType.Char);
        p_LeaveFlag.Direction = ParameterDirection.Input;
        p_LeaveFlag.Value = strLeaveAbbrName;

        SqlParameter p_Remarks = cmd.Parameters.Add("Remarks", SqlDbType.VarChar);
        p_Remarks.Direction = ParameterDirection.Input;
        p_Remarks.Value = LTReason;

        SqlParameter p_LateTimeAmt = cmd.Parameters.Add("LateTimeAmt", SqlDbType.BigInt);
        p_LateTimeAmt.Direction = ParameterDirection.Input;
        if (strDuration == "1")
            p_LateTimeAmt.Value = 8;
        else
            p_LateTimeAmt.Value = 4;

        SqlParameter p_InsertedBy2 = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy2.Direction = ParameterDirection.Input;
        p_InsertedBy2.Value = strInsBy;

        SqlParameter p_InsertedDate5 = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate5.Direction = ParameterDirection.Input;
        p_InsertedDate5.Value = strInsDate;

        return cmd;
    }

    //public SqlCommand UpdateLeaveProfile(string strPreYrLv, string strEmpId, string strLTypeId, string strLvEnjoyed, string strInsBy, string strInsDate)
    //{
    //    SqlCommand cmd;
    //    if (strPreYrLv == "N")
    //        cmd = new SqlCommand("proc_Update_EmpLeaveProfile");
    //    else
    //        cmd = new SqlCommand("proc_Update_EmpLeaveHisProfile");

    //    cmd.CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_EmpID = cmd.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpID.Direction = ParameterDirection.Input;
    //    p_EmpID.Value = strEmpId;

    //    SqlParameter p_Ltype = cmd.Parameters.Add("LTypeID", SqlDbType.BigInt);
    //    p_Ltype.Direction = ParameterDirection.Input;
    //    p_Ltype.Value = Convert.ToInt32(strLTypeId);

    //    SqlParameter p_LeaveEnjoyed1 = cmd.Parameters.Add("LeaveEnjoyed", SqlDbType.Decimal);
    //    p_LeaveEnjoyed1.Direction = ParameterDirection.Input;
    //    p_LeaveEnjoyed1.Value = Convert.ToDecimal(strLvEnjoyed);

    //    SqlParameter p_InsertedBy2 = cmd.Parameters.Add("UpdatedBy", SqlDbType.VarChar);
    //    p_InsertedBy2.Direction = ParameterDirection.Input;
    //    p_InsertedBy2.Value = strInsBy;

    //    SqlParameter p_InsertedDate2 = cmd.Parameters.Add("UpdatedDate", SqlDbType.DateTime);
    //    p_InsertedDate2.Direction = ParameterDirection.Input;
    //    p_InsertedDate2.Value = Convert.ToDateTime(strInsDate);

    //    return cmd;
    //}
        
	public LeaveApplicationManager()
	{
		//
		// TODO: Add constructor logic here
		//
    }
    #endregion

    #region Select From Leave Application By Store procedure
    // Select Data from the Location table
    public DataTable SelectRequestLeaveAppMst(Int32 LvAppID, string EmpId, string AppStatus,
        string LeaveStart, string LeaveEnd, string reportingTo)
    {
        if (objDC.ds.Tables["LeavAppMstReq"] != null)
        {
            objDC.ds.Tables["LeavAppMstReq"].Rows.Clear();
            objDC.ds.Tables["LeavAppMstReq"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_Select_LeaveAppMst");
        SqlParameter p_LvAppID = command.Parameters.Add("LvAppID", SqlDbType.BigInt);
        p_LvAppID.Direction = ParameterDirection.Input;
        p_LvAppID.Value = LvAppID;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_AppStatus = command.Parameters.Add("AppStatus", SqlDbType.Char);
        p_AppStatus.Direction = ParameterDirection.Input;
        p_AppStatus.Value = AppStatus;

        SqlParameter p_LeaveStart = command.Parameters.Add("LeaveStart", SqlDbType.DateTime);
        p_LeaveStart.Direction = ParameterDirection.Input;
        p_LeaveStart.Value = LeaveStart;

        SqlParameter p_LeaveEnd = command.Parameters.Add("LeaveEnd", SqlDbType.DateTime);
        p_LeaveEnd.Direction = ParameterDirection.Input;
        p_LeaveEnd.Value = LeaveEnd;

        SqlParameter p_reportingTo = command.Parameters.Add("SupervisorId", SqlDbType.VarChar);
        p_reportingTo.Direction = ParameterDirection.Input;
        p_reportingTo.Value = reportingTo;

        objDC.CreateDSFromProc(command, "LeavAppMstReq");
        return objDC.ds.Tables["LeavAppMstReq"];
    }

    public DataTable SelectRequestLeaveAppMstForHR(string AppStatus, string LeaveStart, string LeaveEnd)
    {
        if (objDC.ds.Tables["LeavAppMstReq"] != null)
        {
            objDC.ds.Tables["LeavAppMstReq"].Rows.Clear();
            objDC.ds.Tables["LeavAppMstReq"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_Select_LeavAppMstForHR");

        SqlParameter p_AppStatus = command.Parameters.Add("AppStatus", SqlDbType.Char);
        p_AppStatus.Direction = ParameterDirection.Input;
        p_AppStatus.Value = AppStatus;

        SqlParameter p_LeaveStart = command.Parameters.Add("LeaveStart", SqlDbType.DateTime);
        p_LeaveStart.Direction = ParameterDirection.Input;
        p_LeaveStart.Value = LeaveStart;

        SqlParameter p_LeaveEnd = command.Parameters.Add("LeaveEnd", SqlDbType.DateTime);
        p_LeaveEnd.Direction = ParameterDirection.Input;
        p_LeaveEnd.Value = LeaveEnd;

        objDC.CreateDSFromProc(command, "LeavAppMstReq");
        return objDC.ds.Tables["LeavAppMstReq"];
    }

    public DataTable SelectRequestLeaveAppMstForAdminUsers(Int32 LvAppID, string EmpId, string AppStatus,
       string LeaveStart, string LeaveEnd, string DivisionID)
    {
        if (objDC.ds.Tables["LeavAppMstReq"] != null)
        {
            objDC.ds.Tables["LeavAppMstReq"].Rows.Clear();
            objDC.ds.Tables["LeavAppMstReq"].Dispose();
        }
        
        SqlCommand command = new SqlCommand("proc_Select_LeavAppMstForAdminUsers");
        SqlParameter p_LvAppID = command.Parameters.Add("LvAppID", SqlDbType.BigInt);
        p_LvAppID.Direction = ParameterDirection.Input;
        p_LvAppID.Value = LvAppID;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_AppStatus = command.Parameters.Add("AppStatus", SqlDbType.Char);
        p_AppStatus.Direction = ParameterDirection.Input;
        p_AppStatus.Value = AppStatus;

        SqlParameter p_LeaveStart = command.Parameters.Add("LeaveStart", SqlDbType.DateTime);
        p_LeaveStart.Direction = ParameterDirection.Input;
        p_LeaveStart.Value = LeaveStart;

        SqlParameter p_LeaveEnd = command.Parameters.Add("LeaveEnd", SqlDbType.DateTime);
        p_LeaveEnd.Direction = ParameterDirection.Input;
        p_LeaveEnd.Value = LeaveEnd;

        SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = DivisionID;

        objDC.CreateDSFromProc(command, "LeavAppMstReq");
        return objDC.ds.Tables["LeavAppMstReq"];
    }

    public DataTable SelectLeaveAppMstStatus(Int32 LvAppID, string EmpId, string AppStatus)
    {
        if (objDC.ds.Tables["LeaveAppMstStatus"] != null)
        {
            objDC.ds.Tables["LeaveAppMstStatus"].Rows.Clear();
            objDC.ds.Tables["LeaveAppMstStatus"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_Select_LeaveAppMstStatus");

        SqlParameter p_LvAppID = command.Parameters.Add("LvAppID", SqlDbType.BigInt);
        p_LvAppID.Direction = ParameterDirection.Input;
        p_LvAppID.Value = LvAppID;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_AppStatus = command.Parameters.Add("AppStatus", SqlDbType.Char);
        p_AppStatus.Direction = ParameterDirection.Input;
        p_AppStatus.Value = AppStatus;

        objDC.CreateDSFromProc(command, "LeaveAppMstStatus");
        return objDC.ds.Tables["LeaveAppMstStatus"];
    }

    public DataTable SelectRequestDenyLeaveAppMst(Int32 LvAppID, string EmpId, string AppStatus,
        string LeaveStart, string LeaveEnd, string reportingTo)
    {
        if (objDC.ds.Tables["RequestDenyLeavAppMst"] != null)
        {
            objDC.ds.Tables["RequestDenyLeavAppMst"].Rows.Clear();
            objDC.ds.Tables["RequestDenyLeavAppMst"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_Select_LeaveAppMst");
        SqlParameter p_LvAppID = command.Parameters.Add("LvAppID", SqlDbType.BigInt);
        p_LvAppID.Direction = ParameterDirection.Input;
        p_LvAppID.Value = LvAppID;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_AppStatus = command.Parameters.Add("AppStatus", SqlDbType.Char);
        p_AppStatus.Direction = ParameterDirection.Input;
        p_AppStatus.Value = AppStatus;

        SqlParameter p_LeaveStart = command.Parameters.Add("LeaveStart", SqlDbType.DateTime);
        p_LeaveStart.Direction = ParameterDirection.Input;
        p_LeaveStart.Value = LeaveStart;

        SqlParameter p_LeaveEnd = command.Parameters.Add("LeaveEnd", SqlDbType.DateTime);
        p_LeaveEnd.Direction = ParameterDirection.Input;
        p_LeaveEnd.Value = LeaveEnd;

        SqlParameter p_reportingTo = command.Parameters.Add("SupervisorId", SqlDbType.VarChar);
        p_reportingTo.Direction = ParameterDirection.Input;
        p_reportingTo.Value = reportingTo;

        objDC.CreateDSFromProc(command, "RequestDenyLeavAppMst");
        return objDC.ds.Tables["RequestDenyLeavAppMst"];
    }

    public DataTable SelectLeaveAppMst(Int32 LvAppID,string UserId,string EmpId,string LeaveStart,string LeaveEnd)
    {
        SqlCommand command = new SqlCommand("proc_Select_LeavAppMst");
        SqlParameter p_LvAppID = command.Parameters.Add("LvAppID", SqlDbType.BigInt);
        p_LvAppID.Direction = ParameterDirection.Input;
        p_LvAppID.Value = LvAppID;

        SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
        p_UserId.Direction = ParameterDirection.Input;
        p_UserId.Value = UserId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_LeaveStart = command.Parameters.Add("LeaveStart", SqlDbType.DateTime);
        p_LeaveStart.Direction = ParameterDirection.Input;
        p_LeaveStart.Value = LeaveStart;

        SqlParameter p_LeaveEnd = command.Parameters.Add("LeaveEnd", SqlDbType.DateTime);
        p_LeaveEnd.Direction = ParameterDirection.Input;
        p_LeaveEnd.Value = LeaveEnd;

        objDC.CreateDSFromProc(command, "LeaveAppMst");
        return objDC.ds.Tables["LeaveAppMst"];
    }

    public DataTable SelectLeaveAppDet(Int32 LvAppID)
    {
        SqlCommand command = new SqlCommand("proc_Select_LeavAppDets");
        SqlParameter p_LvAppID = command.Parameters.Add("LvAppID", SqlDbType.BigInt);
        p_LvAppID.Direction = ParameterDirection.Input;
        p_LvAppID.Value = LvAppID;
        objDC.CreateDSFromProc(command, "LeavAppDets");
        return objDC.ds.Tables["LeavAppDets"];
    }

    public DataTable SelectEmpLeaveProfile(string EmpId, string LTypeID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpLeaveProfile");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_LTypeID = command.Parameters.Add("LTypeID", SqlDbType.BigInt);
        p_LTypeID.Direction = ParameterDirection.Input;
        p_LTypeID.Value = Convert.ToInt32(LTypeID);

        objDC.CreateDSFromProc(command, "EmpLeaveProfile");
        return objDC.ds.Tables["EmpLeaveProfile"];
    }

    public DataTable SelectEmpLeaveProfileEXCPL(string EmpId, string LTypeID,  string Sex)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpLeaveProfileEXCPL");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_LTypeID = command.Parameters.Add("LTypeID", SqlDbType.BigInt);
        p_LTypeID.Direction = ParameterDirection.Input;
        p_LTypeID.Value = Convert.ToInt32(LTypeID);

        SqlParameter p_Sex = command.Parameters.Add("Gender", SqlDbType.Char);
        p_Sex.Direction = ParameterDirection.Input;
        p_Sex.Value = Sex;

        objDC.CreateDSFromProc(command, "EmpLeaveProfileEXCPL");
        return objDC.ds.Tables["EmpLeaveProfileEXCPL"];
    }

    public DataTable SelectEmpLeaveProfileEXCPL_History(string EmpId, string LTypeID, string LeaveYear)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpLeaveProfileEXCPL_History");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_LTypeID = command.Parameters.Add("LTypeID", SqlDbType.BigInt);
        p_LTypeID.Direction = ParameterDirection.Input;
        p_LTypeID.Value = Convert.ToInt32(LTypeID);

        SqlParameter p_LeaveYear = command.Parameters.Add("LeaveYear", SqlDbType.DateTime);
        p_LeaveYear.Direction = ParameterDirection.Input;
        p_LeaveYear.Value = LeaveYear;

        objDC.CreateDSFromProc(command, "EmpLeaveProfileEXCPLHistory");
        return objDC.ds.Tables["EmpLeaveProfileEXCPLHistory"];
    }

    //public DataTable SelectEmpLeaveProfileHistory(string EmpId, string LeaveStart,string LeaveEnd)
    //{
    //    SqlCommand command = new SqlCommand("[proc_Select_EmpLeaveProfileHistory]");

    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = EmpId;

    //    SqlParameter p_LeaveStartPeriod = command.Parameters.Add("LeaveStartPeriod", SqlDbType.DateTime);
    //    p_LeaveStartPeriod.Direction = ParameterDirection.Input;
    //    p_LeaveStartPeriod.Value = LeaveStart;

    //    SqlParameter p_LeaveEndPeriod = command.Parameters.Add("LeaveEndPeriod", SqlDbType.DateTime);
    //    p_LeaveEndPeriod.Direction = ParameterDirection.Input;
    //    p_LeaveEndPeriod.Value = LeaveEnd;

    //    objDC.CreateDSFromProc(command, "SelectEmpLeaveProfileHistory");
    //    return objDC.ds.Tables["SelectEmpLeaveProfileHistory"];
    //}    

    public DataTable SelectEmpLeaveProfile2(string EmpId, string LTypeID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpLeaveProfile");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_LTypeID = command.Parameters.Add("LTypeID", SqlDbType.BigInt);
        p_LTypeID.Direction = ParameterDirection.Input;
        p_LTypeID.Value = Convert.ToInt32(LTypeID);

        objDC.CreateDSFromProc(command, "EmpLeaveProfile2");
        return objDC.ds.Tables["EmpLeaveProfile2"];
    }

    public DataTable SelectEmpLeaveDetails(string EmpId, string LeaveStart, string LeaveEnd)
    {
        SqlCommand command = new SqlCommand("proc_Select_Emp_Ws_Leave_Det");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_LeaveStart = command.Parameters.Add("LeaveStart", SqlDbType.DateTime);
        p_LeaveStart.Direction = ParameterDirection.Input;
        p_LeaveStart.Value = LeaveStart;

        SqlParameter p_LeaveEnd = command.Parameters.Add("LeaveEnd", SqlDbType.DateTime);
        p_LeaveEnd.Direction = ParameterDirection.Input;
        p_LeaveEnd.Value = LeaveEnd;

        objDC.CreateDSFromProc(command, "LeaveAppMst");
        return objDC.ds.Tables["LeaveAppMst"];
    }

    public DataTable SelectEmpPreLeaveDetails(string EmpId, string LeaveStart, string LeaveEnd)
    {
        SqlCommand command = new SqlCommand("proc_Select_Emp_Ws_Leave_Det");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_LeaveStart = command.Parameters.Add("LeaveStart", SqlDbType.DateTime);
        p_LeaveStart.Direction = ParameterDirection.Input;
        p_LeaveStart.Value = LeaveStart;

        SqlParameter p_LeaveEnd = command.Parameters.Add("LeaveEnd", SqlDbType.DateTime);
        p_LeaveEnd.Direction = ParameterDirection.Input;
        p_LeaveEnd.Value = LeaveEnd;

        objDC.CreateDSFromProc(command, "PreLeavAppMst");
        return objDC.ds.Tables["PreLeavAppMst"];
    }

    public DataTable SelectEmpWiseLeaveType(Int32 LPakId, string EmpId,string Sex)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpWiseLeavePak");
        SqlParameter p_LPakId = command.Parameters.Add("LeavePakId", SqlDbType.BigInt);
        p_LPakId.Direction = ParameterDirection.Input;
        p_LPakId.Value = LPakId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_Sex = command.Parameters.Add("Gender", SqlDbType.Char);
        p_Sex.Direction = ParameterDirection.Input;
        p_Sex.Value = Sex;

        objDC.CreateDSFromProc(command, "LeaveType");
        return objDC.ds.Tables["LeaveType"];
    }

    public DataTable SelectEmpWiseLeaveReuested(Int32 LTypeID, string EmpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_LeaveRequested");
        SqlParameter p_LTypeID = command.Parameters.Add("LTypeID", SqlDbType.BigInt);
        p_LTypeID.Direction = ParameterDirection.Input;
        p_LTypeID.Value = LTypeID;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDSFromProc(command, "LeaveType");
        return objDC.ds.Tables["LeaveType"];
    }

    public DataTable SelectDivisionWiseEmp(string EmpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_DivisionWiseEmp");
        command.CommandType = CommandType.StoredProcedure;   

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDSFromProc(command, "DivWiseEmp");
        return objDC.ds.Tables["DivWiseEmp"];
    }
    public DataTable SelectAdminEmp(string EmpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_AdminDeptEmp2");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDSFromProc(command, "AdminEmp");
        return objDC.ds.Tables["AdminEmp"];
    }
    // Copy To Concern Person
    public DataTable SelectDeptWiseEmp(string EmpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_DeptWiseEmp");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDSFromProc(command, "DeptWiseEmp");
        return objDC.ds.Tables["DeptWiseEmp"];
    }
    public DataTable SelectEmpWiseWeekend(string EmpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_Emp_Ws_Weekend");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDSFromProc(command, "EmpInfo");
        return objDC.ds.Tables["EmpInfo"];        
    }

    public DataTable SelectEmpWiseLeaveDateDets(string EmpId, string LvAppID)
    {
        SqlCommand command = new SqlCommand("proc_Select_Emp_Ws_Leave_Date_Det");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_LvAppID = command.Parameters.Add("LvAppID", SqlDbType.BigInt);
        p_LvAppID.Direction = ParameterDirection.Input;
        p_LvAppID.Value = Convert.ToInt32(LvAppID);

        objDC.CreateDSFromProc(command, "LeaveAppMst");
        return objDC.ds.Tables["LeaveAppMst"];
    }

    public DataTable SelectEmpWsAttnd(string EmpId, string AttndDate)
    {
        SqlCommand command = new SqlCommand("proc_Select_Emp_Ws_Attandance");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_AttndDate = command.Parameters.Add("AttndDate", DBNull.Value);
        p_AttndDate.Direction = ParameterDirection.Input;
        p_AttndDate.IsNullable = true;
        if (AttndDate != "")
            p_AttndDate.Value = AttndDate;

        objDC.CreateDSFromProc(command, "Attandance");
        return objDC.ds.Tables["Attandance"];
    }

    //public DataTable SelectDivisionWiseEmp(string EmpId)
    //{
    //    SqlCommand command = new SqlCommand("proc_Select_DivisionWiseEmp");

    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = EmpId;

    //    objDC.CreateDSFromProc(command, "DivisionWiseEmp");
    //    return objDC.ds.Tables["DivisionWiseEmp"];
    //}

    public DataTable SelectLastAvailLeave(string EmpId, string LeaveStart, string LeaveEnd)
    {
        SqlCommand command = new SqlCommand("proc_Select_LastLeaveAvailDate");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_LeaveStart = command.Parameters.Add("LeaveStart", SqlDbType.DateTime);
        p_LeaveStart.Direction = ParameterDirection.Input;
        p_LeaveStart.Value = LeaveStart;

        SqlParameter p_LeaveEnd = command.Parameters.Add("LeaveEnd", SqlDbType.DateTime);
        p_LeaveEnd.Direction = ParameterDirection.Input;
        p_LeaveEnd.Value = LeaveEnd;

        objDC.CreateDSFromProc(command, "LeaveAppMstApprove");
        return objDC.ds.Tables["LeaveAppMstApprove"];
    }

    public DataTable SelectResponsePerson(Int32 LvAppID, string EmpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_LeaveResponsivePerson");
        SqlParameter p_LvAppID = command.Parameters.Add("LvAppID", SqlDbType.BigInt);
        p_LvAppID.Direction = ParameterDirection.Input;
        p_LvAppID.Value = LvAppID;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDSFromProc(command, "LeaveResponsivePerson");
        return objDC.ds.Tables["LeaveResponsivePerson"];
    }

    public DataTable SelectSectionLevel(string EmpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_SectionLevel");
        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDSFromProc(command, "SectionLevel");
        return objDC.ds.Tables["SectionLevel"];
    }
    public DataTable SelectLeaveAppMstRpt(Int32 LvAppID, string EmpId, string AppStatus, string LeaveStart, string LeaveEnd)
    {
        SqlCommand command = new SqlCommand("proc_Select_LeaveAppMstRpt");
        SqlParameter p_LvAppID = command.Parameters.Add("LvAppID", SqlDbType.BigInt);
        p_LvAppID.Direction = ParameterDirection.Input;
        p_LvAppID.Value = LvAppID;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_AppStatus = command.Parameters.Add("AppStatus", SqlDbType.Char);
        p_AppStatus.Direction = ParameterDirection.Input;
        p_AppStatus.Value = AppStatus;

        SqlParameter p_LeaveStart = command.Parameters.Add("LeaveStart", SqlDbType.DateTime);
        p_LeaveStart.Direction = ParameterDirection.Input;
        p_LeaveStart.Value = LeaveStart;

        SqlParameter p_LeaveEnd = command.Parameters.Add("LeaveEnd", SqlDbType.DateTime);
        p_LeaveEnd.Direction = ParameterDirection.Input;
        p_LeaveEnd.Value = LeaveEnd;

        if (AppStatus == "A")
        {
            objDC.CreateDSFromProc(command, "LeavAppMstApprove");
            return objDC.ds.Tables["LeavAppMstApprove"];
        }
        else if (AppStatus == "R")
        {
            objDC.CreateDSFromProc(command, "LeavAppMstReq");
            return objDC.ds.Tables["LeavAppMstReq"];
        }
        else if (AppStatus == "D")
        {
            objDC.CreateDSFromProc(command, "LeavAppMstDeny");
            return objDC.ds.Tables["LeavAppMstDeny"];
        }
        else if (AppStatus == "RD")
        {
            objDC.CreateDSFromProc(command, "LeavAppMstReqDeny");
            return objDC.ds.Tables["LeavAppMstReqDeny"];
        }
        else
        {
            objDC.CreateDSFromProc(command, "LeavAppMstReqDeny");
            return objDC.ds.Tables["LeavAppMstReqDeny"];
        }
    }

    public DataTable SelectLeaveCopyTo(string strLvAppId)
    {
        SqlCommand command = new SqlCommand("proc_Select_LeaveCOPYTO");

        SqlParameter p_LvAppId = command.Parameters.Add("LvAppId", SqlDbType.BigInt);
        p_LvAppId.Direction = ParameterDirection.Input;
        p_LvAppId.Value = strLvAppId;

        objDC.CreateDSFromProc(command, "LeaveCOPYTO");
        return objDC.ds.Tables["LeaveCOPYTO"];
    }

    public string SelectUpdatedByUserName(string strUserID)
    {
        string strSQL = "SELECT E.FULLNAME + ', ' + J.DesigName FROM EMPINFO E, USERINFO U, Designation J "
                        + " WHERE E.EMPID=U.EMPID AND E.DESIGID=J.DESIGID AND U.USERID=@USERID";

        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;
        SqlParameter p_USERID = command.Parameters.Add("USERID", SqlDbType.Char);
        p_USERID.Direction = ParameterDirection.Input;
        p_USERID.Value = strUserID;
        strSQL = objDC.GetScalarVal(command);
        return strSQL;
    }


    public DataTable SelectEmployeeOnLeaveReport(Int32 LvAppID, string EmpId, string AppStatus, string LeaveStart, string LeaveEnd,string strDivId,string strDeptID)
    {
        SqlCommand command = new SqlCommand("proc_Get_EmpOnLeaveReport");
        SqlParameter p_LvAppID = command.Parameters.Add("LvAppID", SqlDbType.BigInt);
        p_LvAppID.Direction = ParameterDirection.Input;
        p_LvAppID.Value = LvAppID;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_AppStatus = command.Parameters.Add("AppStatus", SqlDbType.Char);
        p_AppStatus.Direction = ParameterDirection.Input;
        p_AppStatus.Value = AppStatus;

        SqlParameter p_LeaveStart = command.Parameters.Add("LeaveStart", SqlDbType.DateTime);
        p_LeaveStart.Direction = ParameterDirection.Input;
        p_LeaveStart.Value = LeaveStart;

        SqlParameter p_LeaveEnd = command.Parameters.Add("LeaveEnd", SqlDbType.DateTime);
        p_LeaveEnd.Direction = ParameterDirection.Input;
        p_LeaveEnd.Value = LeaveEnd;

        SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = strDivId;

        SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
        p_DeptID.Direction = ParameterDirection.Input;
        p_DeptID.Value = strDeptID;

        if (AppStatus == "A")
        {
            objDC.CreateDSFromProc(command, "EmpOnLeaveReportApp");
            return objDC.ds.Tables["EmpOnLeaveReportApp"];
        }
        else if (AppStatus == "R")
        {
            objDC.CreateDSFromProc(command, "EmpOnLeaveReportReq");
            return objDC.ds.Tables["EmpOnLeaveReportReq"];
        }
        else if (AppStatus == "D")
        {
            objDC.CreateDSFromProc(command, "EmpOnLeaveReportDeny");
            return objDC.ds.Tables["EmpOnLeaveReportDeny"];
        }
        else if (AppStatus == "RD")
        {
            objDC.CreateDSFromProc(command, "EmpOnLeaveReportReqDeny");
            return objDC.ds.Tables["EmpOnLeaveReportReqDeny"];
        }
        else
        {
            objDC.CreateDSFromProc(command, "EmpOnLeaveReport");
            return objDC.ds.Tables["EmpOnLeaveReport"];
        }
    }

    public DataTable CheckLvDateBetweenLeavePeriod(string strLvPakId, string strLvStartDate)
    {
        string strSQL = "SELECT * FROM LeavePeriod WHERE '" + strLvStartDate + "' NOT BETWEEN"
            + " LeaveStartPeriod AND LeaveEndPeriod AND LeavePakId=" + strLvPakId;

        SqlCommand command = new SqlCommand(strSQL);

        SqlParameter p_LvPakId = command.Parameters.Add("LvPakId", SqlDbType.BigInt);
        p_LvPakId.Direction = ParameterDirection.Input;
        p_LvPakId.Value = strLvPakId;

        SqlParameter p_LvStartDate = command.Parameters.Add("LeaveStartPeriod", SqlDbType.VarChar);
        p_LvStartDate.Direction = ParameterDirection.Input;
        p_LvStartDate.Value = strLvStartDate;

        objDC.CreateDT(command, "ChkLeavePeriod");
        return objDC.ds.Tables["ChkLeavePeriod"];
    }

    public DataTable CheckLvDateWithHoliDate(string strLvStartDate, string strLvEndDate, string strYear)
    {
        string strSQL = "SELECT * FROM HolidaysDetls WHERE HoliDate BETWEEN '" + strLvStartDate
            + "' AND '" + strLvEndDate + "' AND HoliDayYear='" + strYear + "'";

        SqlCommand command = new SqlCommand(strSQL);

        SqlParameter p_LvStartDate = command.Parameters.Add("StartHoliDate", SqlDbType.VarChar);
        p_LvStartDate.Direction = ParameterDirection.Input;
        p_LvStartDate.Value = strLvStartDate;

        SqlParameter p_LvEndDate = command.Parameters.Add("EndHoliDate", SqlDbType.VarChar);
        p_LvEndDate.Direction = ParameterDirection.Input;
        p_LvEndDate.Value = strLvEndDate;

        SqlParameter p_LvYear = command.Parameters.Add("HoliDayYear", SqlDbType.Char);
        p_LvYear.Direction = ParameterDirection.Input;
        p_LvYear.Value = strYear;

        objDC.CreateDT(command, "HolidayDetls");
        return objDC.ds.Tables["HolidayDetls"];
    }

    public DataTable GetLeaveDates(string strLvAppId)
    {
        string strSQL = "SELECT LevDate FROM LeaveAppDet WHERE LvAppId=" + strLvAppId;

        SqlCommand command = new SqlCommand(strSQL);

        SqlParameter p_LvAppId = command.Parameters.Add("LvAppId", SqlDbType.BigInt);
        p_LvAppId.Direction = ParameterDirection.Input;
        p_LvAppId.Value = Convert.ToInt32(strLvAppId);

        objDC.CreateDT(command, "LevAppDetDate");
        return objDC.ds.Tables["LevAppDetDate"];
    }

    public DataTable SelectEmpLeaveDateDetails(Int32 LvAppID, string EmpId, string LevDate)
    {
        if (objDC.ds.Tables["LeaveAppDet2"] != null)
        {
            objDC.ds.Tables["LeaveAppDet2"].Rows.Clear();
            objDC.ds.Tables["LeaveAppDet2"].Dispose(); 
        }
        SqlCommand command = new SqlCommand("proc_Select_LeaveAppDet");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LvAppID = command.Parameters.Add("LvAppID", SqlDbType.BigInt);
        p_LvAppID.Direction = ParameterDirection.Input;
        p_LvAppID.Value = LvAppID;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_LevDate = command.Parameters.Add("LevDate", SqlDbType.DateTime);
        p_LevDate.Direction = ParameterDirection.Input;
        p_LevDate.Value = LevDate;

        objDC.CreateDSFromProc(command, "LeaveAppDet2");
        return objDC.ds.Tables["LeaveAppDet2"];
    }

    public DataTable SelectGovtHolidays(string strFromDate, string strToDate)
    {
        SqlCommand command = new SqlCommand("proc_Select_Govt_Holidays");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
        p_FromDate.Direction = ParameterDirection.Input;
        p_FromDate.Value = strFromDate;

        SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
        p_ToDate.Direction = ParameterDirection.Input;
        p_ToDate.Value = strToDate;

        objDC.CreateDSFromProc(command, "tblGovtHolidays");
        return objDC.ds.Tables["tblGovtHolidays"];
    }

    public DataTable GetEarnLeaveEmp()
    {
        SqlCommand command = new SqlCommand("proc_Get_Earn_Leave_Employee");
        command.CommandType = CommandType.StoredProcedure;

        //SqlParameter p_LvAppID = command.Parameters.Add("LvAppID", SqlDbType.BigInt);
        //p_LvAppID.Direction = ParameterDirection.Input;
        //p_LvAppID.Value = LvAppID;

        //SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        //p_EmpId.Direction = ParameterDirection.Input;
        //p_EmpId.Value = EmpId;

        //SqlParameter p_LevDate = command.Parameters.Add("LevDate", SqlDbType.DateTime);
        //p_LevDate.Direction = ParameterDirection.Input;
        //p_LevDate.Value = LevDate;

        objDC.CreateDSFromProc(command, "tblProbationaryEmployee");
        return objDC.ds.Tables["tblProbationaryEmployee"];
    }

    //Check for Maternity Leave
    public Int16 GetMaxLvDays(string strEmpId, string strLTypeId)
    {
        Int16 iMaxDays;
        string strSQL = "SELECT LeaveTTL FROM LeaveTypeList WHERE LTypeId=" + strLTypeId;

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        iMaxDays = Convert.ToInt16(objDC.GetScalarVal(cmd));
        return iMaxDays;
    }

    //public Int16 GetPrevDayLeave(string strEmpId, string strLeaveStart)
    //{
    //    Int16 iLTypeId;
    //    string strSQL = "SELECT LTypeId FROM LeaveAppDet WHERE LevDate='" + strLeaveStart + "' AND EmpId='" + strEmpId + "'";

    //    SqlCommand command = new SqlCommand(strSQL);
    //    command.CommandType = CommandType.Text;

    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = strEmpId;

    //    SqlParameter p_LvStartDate = command.Parameters.Add("LevDate", SqlDbType.VarChar);
    //    p_LvStartDate.Direction = ParameterDirection.Input;
    //    p_LvStartDate.Value =Common.ReturnDate(strLeaveStart);

    //    string s=objDC.GetScalarVal(command);
    //    iLTypeId=Convert.ToInt16(s);

    //    return iLTypeId;
    //}

    public DataTable CheckLvTakenBarrier()
    {
        string strSQL = "SELECT LTB.*,LT.LTypeTitle FROM LeaveTakenBarrier LTB, LeaveTypeList LT WHERE LTB.PLTypeId=LT.LTypeId";

        SqlCommand command = new SqlCommand(strSQL);

        objDC.CreateDT(command, "CheckLvTakenBarrier");
        return objDC.ds.Tables["CheckLvTakenBarrier"];
    }
    
    #endregion
}
