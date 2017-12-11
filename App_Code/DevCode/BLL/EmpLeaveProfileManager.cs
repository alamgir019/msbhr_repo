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
/// Summary description for EmpLeaveProfileManager
/// </summary>
public class EmpLeaveProfileManager
{   
    DBConnector objDC = new DBConnector();
    DataTable dtLeavePakMst = new DataTable();
    DataTable dtLevProfile = new DataTable();

    LeaveManager objLeaveMgr = new LeaveManager();
    LeaveApplicationManager objLeaveAppMgr = new LeaveApplicationManager();
    int i = 0;
    public EmpLeaveProfileManager()
    {

        //
        // TODO: Add constructor logic here
        //
    }

   
    public DataTable GET_LeaveEntitleMaxMonth(string EmpId)
    {
        //SqlCommand command = new SqlCommand("PROC_UPDATE_RemainMonth");

        //SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        //p_EmpId.Direction = ParameterDirection.Input;
        //p_EmpId.Value = EmpId;

        //objDC.CreateDSFromProc(command, "tblRemainMonth");
        //return objDC.ds.Tables["tblRemainMonth"];

        string strSql = "SELECT ISNULL(MAX(VMONTH),0) AS MaxMonth, ISNULL(MAX(VYEAR),0) AS MaxYear FROM EmpLeaveAccrual"
                + " WHERE EMPID='" + EmpId + "'";

        SqlCommand cmd = new SqlCommand(strSql);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDT(cmd, "tblMaxEntitleMonth");
        return objDC.ds.Tables["tblMaxEntitleMonth"];
    }

    public DataTable SelectLeavePeriod(string LPakId)
    {
        SqlCommand cmd4 = new SqlCommand("proc_Select_Leave_period");
        cmd4.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LPakId = cmd4.Parameters.Add("LeavePakId", SqlDbType.BigInt);
        p_LPakId.Direction = ParameterDirection.Input;
        p_LPakId.Value = Convert.ToInt32(LPakId);

        objDC.CreateDSFromProc(cmd4, "Leaveperiod");
        return objDC.ds.Tables["Leaveperiod"];
    }

    public DataTable DeleteEmpLeaveProfile(string EmpId, string LPakId)
    {
        // sproc functionality
        SqlCommand cmd1 = new SqlCommand("proc_1_Delete_EmpLeaveProfile");
        cmd1.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpID = cmd1.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpId;

        SqlParameter p_LPakId = cmd1.Parameters.Add("LeavePakId", SqlDbType.Char);
        p_LPakId.Direction = ParameterDirection.Input;
        p_LPakId.Value = Convert.ToInt32 (  LPakId );

        objDC.CreateDSFromProc(cmd1, "EmpLeaveProfile");
        return objDC.ds.Tables["EmpLeaveProfile"];
    }
    public DataTable SelectLeavePakMst(string LPakId)
    {
        SqlCommand cmd2 = new SqlCommand("proc_Select_LeavePakMst");
        cmd2.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LPakId = cmd2.Parameters.Add("LeavePakId", SqlDbType.BigInt);
        p_LPakId.Direction = ParameterDirection.Input;
        p_LPakId.Value = Convert.ToInt32(LPakId);

        objDC.CreateDSFromProc(cmd2, "LeavePakMst");
        return objDC.ds.Tables["LeavePakMst"];        
    }

    public DataTable SelectLeavePeriod()
    {
        SqlCommand cmd4 = new SqlCommand("Sel_Leave_period");
        cmd4.CommandType = CommandType.StoredProcedure;

        objDC.CreateDSFromProc(cmd4, "Leaveperiod");
        return objDC.ds.Tables["Leaveperiod"];
    }

  

    public DataTable SelectLPakDetls(string LPakId)
    {
        
        SqlCommand cmd4 = new SqlCommand("proc_Select_LPakDetls_For_EmpLevProfile");
        cmd4.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LPakId = cmd4.Parameters.Add("LeavePakId", SqlDbType.BigInt);
        p_LPakId.Direction = ParameterDirection.Input;
        p_LPakId.Value = Convert.ToInt32(LPakId);

        objDC.CreateDSFromProc(cmd4, "LPakDetls");
        return objDC.ds.Tables["LPakDetls"];
    }

    private  SqlCommand  UpdateEmpLevPak(string empID, string LPakId)
    {
        
        SqlCommand cmd4 = new SqlCommand("proc_Update_Empleavepak");
        cmd4.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LPakId = cmd4.Parameters.Add("LeavePakId", SqlDbType.BigInt);
        p_LPakId.Direction = ParameterDirection.Input;
        p_LPakId.Value = LPakId;

        SqlParameter p_empID = cmd4.Parameters.Add("EmpId", SqlDbType.Char);
        p_empID.Direction = ParameterDirection.Input;
        p_empID.Value = empID;

        SqlParameter p_UpdatedBy = cmd4.Parameters.Add("UpdatedBy", SqlDbType.VarChar);
        p_UpdatedBy.Direction = ParameterDirection.Input;
        p_UpdatedBy.Value = "admin/LevProfUpdt";

        SqlParameter p_UpdatedDate = cmd4.Parameters.Add("UpdatedDate", SqlDbType.DateTime);
        p_UpdatedDate.Direction = ParameterDirection.Input;
        p_UpdatedDate.Value = Common.SetDateTime(DateTime.Now.ToString());

        return cmd4;
    }

    #region LeaveProfile Balance Update

    public SqlCommand[] InsertIntoEmpLevProfile(string EmpId, string LPakId, string JoiningDate, string userId, string strEmpTypeId)
    {
        try
        {
            i = 0;
           
            decimal LEntitled = 0;
            string IsLeaveOnJoinDate = "";

            DateTime dtLeaveStDate = DateTime.Now;
            clsEmpLeaveProfile objEmpLevPro = new clsEmpLeaveProfile();
            SqlCommand[] cmd = new SqlCommand[15];

            if (dtLeavePakMst.Rows.Count == 0)
                dtLeavePakMst = SelectLeavePakMst(LPakId);

            DataTable dtLeavePeriod = SelectLeavePeriod(LPakId);

            if (dtLeavePeriod.Rows.Count > 0)
                dtLeaveStDate = Convert.ToDateTime(dtLeavePeriod.Rows[0]["LeaveStartPeriod"]);

            DataTable dtLevProfile = objLeaveAppMgr.SelectEmpLeaveProfile(EmpId, "0");

            DataTable dtLeaveType = objLeaveMgr.SelectLeavePakDet(Convert.ToInt32(LPakId));

            if (dtLeavePakMst.Rows.Count > 0)
                IsLeaveOnJoinDate = dtLeavePakMst.Rows[0]["IsLCalOnJoinDate"].ToString();

            if (dtLeaveType.Rows.Count > 0)
            {
                if (dtLevProfile.Rows.Count > 0)
                {
                    foreach (DataRow LTRow in dtLeaveType.Rows)
                    {
                        LEntitled = 0;
                        objEmpLevPro = new clsEmpLeaveProfile(EmpId, LTRow["LTypeId"].ToString().Trim(), Math.Round(LEntitled, 2).ToString(),
                               userId, Common.SetDateTime(DateTime.Now.ToString()));

                        cmd[i] = InsertIntoEmpLevPro(objEmpLevPro, "Y", "N");
                        i++;
                    }
                }
                else
                {
                    foreach (DataRow LTRow in dtLeaveType.Rows)
                    {
                        if (IsLeaveOnJoinDate == "Y")
                        {
                            LEntitled = 0;
                        }

                        objEmpLevPro = new clsEmpLeaveProfile(EmpId, LTRow["LTypeId"].ToString().Trim(), Math.Round(LEntitled, 2).ToString(),
                               userId, Common.SetDateTime(DateTime.Now.ToString()));

                        cmd[i] = InsertIntoEmpLevPro(objEmpLevPro, "N", "N");
                        i++;
                    }
                }
            }

            dtLeaveType.Rows.Clear();
            dtLevProfile.Rows.Clear();
            return cmd;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    public SqlCommand[] UpdateConfirmLevProfile(string EmpId, string LPakId, string ConfirmDate, string JoiningDate, string userId)
    {
        try
        {
            i = 0;
            DataTable dtLeavePeriod = new DataTable();

            decimal LeaveAvail = 0;
            decimal LEntitled = 0;
            decimal tempValue = 0;
            string IsLeaveOnJoinDate = "";

            DateTime dtJoinDate;
            DateTime dtLeaveStDate = DateTime.Now;

            if (dtLeavePakMst.Rows.Count == 0)
            {
                dtLeavePakMst = SelectLeavePakMst(LPakId);
            }
            if (dtLeavePeriod.Rows.Count == 0)
            {
                dtLeavePeriod = SelectLeavePeriod(LPakId);
            }

            dtLevProfile = objLeaveMgr.SelectEmpLeaveProfile(EmpId, "0");

            DataTable dtLeaveType = new DataTable();
            if (dtLeaveType.Rows.Count == 0)
            {
                dtLeaveType = SelectLPakDetls(LPakId);
            }

            DateTime ConfirmYear = new DateTime();
            DateTime CurrYear = DateTime.Now;
            DateTime dtLeaveEndPeriod = new DateTime();
            clsEmpLeaveProfile objEmpLevPro = new clsEmpLeaveProfile();
            SqlCommand[] cmd = new SqlCommand[11];

            if (dtLeavePakMst.Rows.Count > 0)
            {
                IsLeaveOnJoinDate = dtLeavePakMst.Rows[0]["IsLCalOnJoinDate"].ToString();
            }

            if (dtLeaveType.Rows.Count > 0)
            {
                if (dtLevProfile.Rows.Count > 0)
                {
                    foreach (DataRow LTRow in dtLeaveType.Rows)
                    {
                        //foreach (DataRow LevProRow in dtLevProfile.Rows)
                        //{
                        DataRow[] foundProfileRows = dtLevProfile.Select("LTypeId=" + LTRow["LTypeId"].ToString());
                        foreach (DataRow dRow in dtLeavePeriod.Rows)
                        {
                            dtLeaveEndPeriod = Convert.ToDateTime(dRow["LeaveEndPeriod"]);
                            ConfirmYear = Convert.ToDateTime(ConfirmDate);
                        }

                        Int32 MaxLogConfirmMonth = 0;
                        Int32 MaxLogConfirmYear = 0;
                        Int32 iRemainMonth = 0;

                        DataTable dtRemainMonth = new DataTable();
                        dtRemainMonth = GET_LeaveEntitleMaxMonth(EmpId);
                        foreach (DataRow dataRow in dtRemainMonth.Rows)
                        {
                            MaxLogConfirmMonth = Convert.ToInt32(dataRow["MaxMonth"].ToString());
                            MaxLogConfirmYear = Convert.ToInt32(dataRow["MaxYear"].ToString());
                        }

                        Int32 iConfirmMonth = ConfirmYear.Month;
                        Int32 iEndMonth = dtLeaveEndPeriod.Month;

                        if (ConfirmYear.Year > MaxLogConfirmYear)
                        {
                            iRemainMonth = (iEndMonth - iConfirmMonth) + 1;
                        }
                        else
                        {
                            if (iConfirmMonth < MaxLogConfirmMonth)
                                iRemainMonth = (iEndMonth - MaxLogConfirmMonth) + 1;
                            else
                                iRemainMonth = (iEndMonth - iConfirmMonth) + 1;
                        }

                        dtLeaveStDate = Convert.ToDateTime(dtLeavePeriod.Rows[0]["LeaveStartPeriod"]);
                        if (JoiningDate == "")
                            dtJoinDate = dtLeaveStDate;
                        else
                            dtJoinDate = Convert.ToDateTime(JoiningDate);

                        if (dtJoinDate > dtLeaveStDate)
                        {
                            TimeSpan DateDiff = dtJoinDate - dtLeaveStDate;
                            string strTotDay = ReturnTotalDay(DateDiff.ToString());
                            tempValue = (Convert.ToDecimal(LTRow["MaxLAmt"]) / 366);
                            tempValue = (tempValue * (366 - Convert.ToInt32(strTotDay)));
                            LeaveAvail = Convert.ToDecimal(tempValue);
                            LeaveAvail = Math.Round(LeaveAvail, 2);

                            //char[] strSpliter = { '.' };
                            //string[] arinfo = new string[2];

                            //arinfo = LeaveAvail.ToString().Split(strSpliter);
                            LEntitled = LeaveAvail + Convert.ToDecimal(foundProfileRows[0]["LEntitled"]);
                        }
                        objEmpLevPro = new clsEmpLeaveProfile(EmpId, LTRow["LTypeId"].ToString().Trim(), LEntitled.ToString(),
                        userId, Common.SetDateTime(DateTime.Now.ToString()));

                        cmd[i] = InsertIntoEmpLevPro(objEmpLevPro, "Y", "N");
                        //}
                        i++;
                    }
                }
            }
            dtLeaveType.Rows.Clear();
            dtLevProfile.Rows.Clear();

            return cmd;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    public SqlCommand[] UpdatedEmpLevProfile(string EmpId, string LPakId, string JoiningDate, string userId, string strEmpTypeId)
    {
        try
        {
            i = 0;
            decimal LeaveAvail = 0;
            decimal LEntitled = 0;
            decimal tempValue = 0;
            string IsLeaveOnJoinDate = "";

            DateTime dtJoinDate;
            DateTime dtLeaveStDate = DateTime.Now;
            clsEmpLeaveProfile objEmpLevPro = new clsEmpLeaveProfile();
            SqlCommand[] cmd = new SqlCommand[15];

            if (dtLeavePakMst.Rows.Count == 0)
                dtLeavePakMst = SelectLeavePakMst(LPakId);

            DataTable dtLeavePeriod = SelectLeavePeriod(LPakId);

            if (dtLeavePeriod.Rows.Count > 0)
                dtLeaveStDate = Convert.ToDateTime(dtLeavePeriod.Rows[0]["LeaveStartPeriod"]);

            DataTable dtLevProfile = objLeaveAppMgr.SelectEmpLeaveProfile(EmpId, "0");

            DataTable dtLeaveType = objLeaveMgr.SelectLeavePakDet(Convert.ToInt32(LPakId));

            if (dtLeavePakMst.Rows.Count > 0)
                IsLeaveOnJoinDate = dtLeavePakMst.Rows[0]["IsLCalOnJoinDate"].ToString();

            if (dtLeaveType.Rows.Count > 0)
            {
                if (dtLevProfile.Rows.Count > 0)
                {
                    foreach (DataRow LTRow in dtLeaveType.Rows)
                    {
                        DataRow[] foundRows;
                        string strExpr = "";
                        strExpr = "LTypeId='" + LTRow["LTypeId"].ToString().Trim() + "'";
                        foundRows = dtLevProfile.Select(strExpr);
                        if (foundRows.Length > 0)
                        {
                            if (IsLeaveOnJoinDate == "Y")
                            {
                                LeaveAvail = 0;
                                if (LTRow["MaxLAmt"].ToString() != "0")
                                {
                                    dtLeaveStDate = Convert.ToDateTime(dtLeavePeriod.Rows[0]["LeaveStartPeriod"]);
                                    if (JoiningDate == "")
                                        dtJoinDate = dtLeaveStDate;
                                    else
                                        dtJoinDate = Convert.ToDateTime(JoiningDate);

                                    if (dtJoinDate > dtLeaveStDate)
                                    {
                                        TimeSpan DateDiff = dtJoinDate - dtLeaveStDate;
                                        string strTotDay = ReturnTotalDay(DateDiff.ToString());
                                        tempValue = (Convert.ToDecimal(LTRow["MaxLAmt"]) / 366);
                                        tempValue = (tempValue * (366 - Convert.ToInt32(strTotDay)));
                                        LeaveAvail = Convert.ToDecimal(tempValue);
                                        LeaveAvail = Math.Round(LeaveAvail, 2);

                                        char[] strSpliter = { '.' };
                                        string[] arinfo = new string[2];

                                        arinfo = LeaveAvail.ToString().Split(strSpliter);
                                        LEntitled = Convert.ToDecimal(LTRow["LEntitled"]) + LeaveAvail;
                                    }
                                    else
                                        LEntitled = Convert.ToDecimal(LTRow["LEntitled"]) + Convert.ToDecimal(LTRow["MaxLAmt"]);
                                }
                                else
                                    LEntitled = Convert.ToDecimal(LTRow["LEntitled"]) + Convert.ToDecimal(LTRow["MaxLAmt"]);
                            }
                            else
                                LEntitled = Convert.ToDecimal(LTRow["LEntitled"]) + Convert.ToDecimal(LTRow["MaxLAmt"]);

                            objEmpLevPro = new clsEmpLeaveProfile(EmpId, LTRow["LTypeId"].ToString().Trim(), LEntitled.ToString(),
                                userId, Common.SetDateTime(DateTime.Now.ToString()));

                            cmd[i] = InsertIntoEmpLevPro(objEmpLevPro, "Y", "N");
                            i++;
                        }
                    }
                }
            }
            dtLeaveType.Rows.Clear();
            dtLevProfile.Rows.Clear();
            return cmd;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    #endregion

    public string ReturnTotalDay(string Day)
    {
        string[] arInfo = new string[2];
        string strDay;

        char[] splitter = { '.' };
        arInfo = Common.str_split(Day, splitter);

        strDay = arInfo[0];

        return strDay;
    }

    public SqlCommand InsertIntoEmpLevPro(clsEmpLeaveProfile clEmp,string strIsUpdate,string strIsDelete)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_Insert_EmpLeaveProfile");           
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = clEmp.EmpId;

            SqlParameter p_LTypeID = cmd.Parameters.Add("LTypeID", SqlDbType.BigInt);
            p_LTypeID.Direction = ParameterDirection.Input;
            p_LTypeID.Value = clEmp.LTypeID;

            SqlParameter p_LEntitled = cmd.Parameters.Add("LEntitled", SqlDbType.Decimal);
            p_LEntitled.Direction = ParameterDirection.Input;
            p_LEntitled.Value =clEmp.LEntitled;

            SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = clEmp.InsertedBy;

            SqlParameter p_InsertedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate.Direction = ParameterDirection.Input;
            p_InsertedDate.Value = clEmp.InsertedDate;

            SqlParameter p_IsUpdate = cmd.Parameters.Add("IsUpdate", SqlDbType.Char);
            p_IsUpdate.Direction = ParameterDirection.Input;
            p_IsUpdate.Value = strIsUpdate;

            SqlParameter p_IsDelete = cmd.Parameters.Add("IsDelete", SqlDbType.Char);
            p_IsDelete.Direction = ParameterDirection.Input;
            p_IsDelete.Value = strIsDelete;

            return cmd;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }
    public DataTable SelectEmpLeaveBalanceRpt(string EmpId, string strDivId, string strDeptID)
    {
        SqlCommand command = new SqlCommand("proc_Get_EmpLeaveProfile");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_DivisionID = command.Parameters.Add("PostingDivId", SqlDbType.BigInt);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = strDivId;

        SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
        p_DeptID.Direction = ParameterDirection.Input;
        p_DeptID.Value = strDeptID;

        objDC.CreateDSFromProc(command, "EmpLeaveProfile");
        return objDC.ds.Tables["EmpLeaveProfile"];
    }

    public DataTable SelectEmpLeaveSummaryRpt(string EmpId, string strDivId, string strDeptID,string strStatus)
    {
        SqlCommand command = new SqlCommand("proc_Get_EmpLeaveProfileSummary");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_DivisionID = command.Parameters.Add("PostingDivId", SqlDbType.BigInt);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = strDivId;

        SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
        p_DeptID.Direction = ParameterDirection.Input;
        p_DeptID.Value = strDeptID;

        SqlParameter p_EmpStatus = command.Parameters.Add("EmpStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = strStatus;

        objDC.CreateDSFromProc(command, "EmpLeaveProfileSummary");
        return objDC.ds.Tables["EmpLeaveProfileSummary"];
    }

    public DataTable SelectEmpWiseLeaveStats(string strEmpID,string strLTypeId)
    {
        string strSQL = "SELECT elp.*,lt.LTypeTitle,lt.LAbbrName   "
                    + " FROM EmpLeaveProfile elp,LeaveTypeList lt       "
                    + " WHERE elp.LTypeID=lt.LTypeID AND elp.EmpID=@EmpID AND elp.LTypeID in (" + strLTypeId + ") ";

        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_EmpId = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpID;

        return objDC.CreateDT(command, "EmpWiseLeaveStats");
    }

    public DataTable SelectEnjoyedLeaveRecordsRpt(string EmpId, string strDivId, string strDeptID, string strStatus,
        string strFromDate,string strToDate,string strLTypeId,string strEmpType)
    {
        SqlCommand command = new SqlCommand("proc_Get_EnjoyedLeaveRecords");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_DivisionID = command.Parameters.Add("PostingDivID", SqlDbType.BigInt);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = strDivId;

        SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
        p_DeptID.Direction = ParameterDirection.Input;
        p_DeptID.Value = strDeptID;

        SqlParameter p_EmpStatus = command.Parameters.Add("EmpStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = strStatus;

        SqlParameter p_LTypeID = command.Parameters.Add("LTypeID", SqlDbType.Char);
        p_LTypeID.Direction = ParameterDirection.Input;
        p_LTypeID.Value = strLTypeId;

        SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
        p_FromDate.Direction = ParameterDirection.Input;
        p_FromDate.Value = strFromDate;

        SqlParameter p_strToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
        p_strToDate.Direction = ParameterDirection.Input;
        p_strToDate.Value = strToDate;

        SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EmpTypeID.Direction = ParameterDirection.Input;
        p_EmpTypeID.Value = strEmpType;

        objDC.CreateDSFromProc(command, "EnjoyedLeaveRecordsRpt");
        return objDC.ds.Tables["EnjoyedLeaveRecordsRpt"];
    }

    public DataTable SelectEnjoyedLeaveRecordsRpt_CarryOverFromCurrentProfile(string EmpId, string strDivId, string strDeptID, string strStatus,
        string strFromDate, string strToDate, string strLTypeId, string strEmpType)
    {
        SqlCommand command = new SqlCommand("proc_Get_EnjoyedLeaveRecords_CarryOverFromCurrentProfile");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_DivisionID = command.Parameters.Add("PostingDivID", SqlDbType.BigInt);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = strDivId;

        SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
        p_DeptID.Direction = ParameterDirection.Input;
        p_DeptID.Value = strDeptID;

        SqlParameter p_EmpStatus = command.Parameters.Add("EmpStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = strStatus;

        SqlParameter p_LTypeID = command.Parameters.Add("LTypeID", SqlDbType.Char);
        p_LTypeID.Direction = ParameterDirection.Input;
        p_LTypeID.Value = strLTypeId;

        SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
        p_FromDate.Direction = ParameterDirection.Input;
        p_FromDate.Value = strFromDate;

        SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EmpTypeID.Direction = ParameterDirection.Input;
        p_EmpTypeID.Value = strEmpType;

        objDC.CreateDSFromProc(command, "EnjoyedLeaveRecordsRpt_CarryOverFromCurrentProfile");
        return objDC.ds.Tables["EnjoyedLeaveRecordsRpt_CarryOverFromCurrentProfile"];
    }

    public DataTable SelectEnjoyedLeaveRecordsRpt_CarryOverFromHistoryProfile(string EmpId, string strDivId, string strDeptID, string strStatus,
        string strFromDate, string strToDate, string strLTypeId, string strEmpType)
    {
        SqlCommand command = new SqlCommand("proc_Get_EnjoyedLeaveRecords_CarryOverFromHistoryProfile");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_DivisionID = command.Parameters.Add("PostingDivId", SqlDbType.BigInt);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = strDivId;

        SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
        p_DeptID.Direction = ParameterDirection.Input;
        p_DeptID.Value = strDeptID;

        SqlParameter p_EmpStatus = command.Parameters.Add("EmpStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = strStatus;

        SqlParameter p_LTypeID = command.Parameters.Add("LTypeID", SqlDbType.Char);
        p_LTypeID.Direction = ParameterDirection.Input;
        p_LTypeID.Value = strLTypeId;

        SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
        p_FromDate.Direction = ParameterDirection.Input;
        p_FromDate.Value = strFromDate;

        SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EmpTypeID.Direction = ParameterDirection.Input;
        p_EmpTypeID.Value = strEmpType;

        objDC.CreateDSFromProc(command, "EnjoyedLeaveRecordsRpt_CarryOverFromHistoryProfile");
        return objDC.ds.Tables["EnjoyedLeaveRecordsRpt_CarryOverFromHistoryProfile"];
    }

    public DataTable SelectEnjoyedLeaveRecordsDays(string strEmpID, string strLTypeID, string strFrom, string strTo)
    {
        string strSQL="SELECT LDD.LevDate,LM.LTReason,LM.AddrAtLeave,LM.ApprovedBy,LM.approveDate,LM.AppDate "
                    + " FROM LevAppDetDate LDD,LeaveAppMst LM "
                    + " WHERE LDD.LvAppID=LM.LvAppID "
                    + " AND LDD.Ltype=@Ltype AND LM.AppStatus='A' AND LDD.EmpID=@EmpID AND LM.AppStatus='A' "
                    + " AND LDD.LevDate BETWEEN @FromDate AND @ToDate "
                    + " ORDER BY LDD.EmpId ";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_EmpId = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpID;

        SqlParameter p_Ltype = command.Parameters.Add("Ltype", SqlDbType.Char);
        p_Ltype.Direction = ParameterDirection.Input;
        p_Ltype.Value = strLTypeID;

        SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
        p_FromDate.Direction = ParameterDirection.Input;
        p_FromDate.Value = strFrom;

        SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
        p_ToDate.Direction = ParameterDirection.Input;
        p_ToDate.Value = strTo;

        return objDC.CreateDT(command, "EnjoyedLeaveRecordsDays");

    }

}
