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
/// Summary description for LeaveManager
/// </summary>
public class LeaveManager
{

    DBConnector objDC = new DBConnector();
    #region Insert Update Delete From Tables By Store procedure
     //Insert or Update  or Delete Data of Location table

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

    public DataTable SelectEmpLeaveProfileHistory(string EmpId, string LeaveStart, string LeaveEnd)
    {
        SqlCommand command = new SqlCommand("[proc_Select_EmpLeaveProfileHistory]");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_LeaveStartPeriod = command.Parameters.Add("LeaveStartPeriod", SqlDbType.DateTime);
        p_LeaveStartPeriod.Direction = ParameterDirection.Input;
        p_LeaveStartPeriod.Value = LeaveStart;

        SqlParameter p_LeaveEndPeriod = command.Parameters.Add("LeaveEndPeriod", SqlDbType.DateTime);
        p_LeaveEndPeriod.Direction = ParameterDirection.Input;
        p_LeaveEndPeriod.Value = LeaveEnd;

        objDC.CreateDSFromProc(command, "SelectEmpLeaveProfileHistory");
        return objDC.ds.Tables["SelectEmpLeaveProfileHistory"];
    }    
    public void InsertLeaveProfile(string strTypeId, string strLPakID, string Userid)
    {
        long i = 1;
        SqlCommand[] cmd1;
        EmpLeaveProfileManager objLevProMgr = new EmpLeaveProfileManager();
        DataTable dtEmp = new DataTable();
        dtEmp = this.SelectEmpLvProfile(strTypeId);

        SqlCommand[] cmdM = new SqlCommand[10 * dtEmp.Rows.Count];
        foreach (DataRow row in dtEmp.Rows)
        {
            cmd1 = objLevProMgr.UpdatedEmpLevProfile(string.IsNullOrEmpty(row["EmpId"].ToString()) == true ? "" : row["EmpId"].ToString(),
                strLPakID, string.IsNullOrEmpty(row["JoiningDate"].ToString()) == true ? "" : row["JoiningDate"].ToString(), Userid, strTypeId);
            foreach (SqlCommand cmdTemp in cmd1)
            {
                if (cmdTemp != null)
                {
                    cmdM[i] = cmdTemp;
                    i++;
                }

            }
            cmd1 = null;
        }
        try
        {
            objDC.MakeTransaction(cmdM);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            cmdM = null;
        }

    }
    public DataTable SelectEmpLvProfile(string strTypeId)
    {
        SqlCommand cmd5 = new SqlCommand("proc_Select_EmpInfo_leaveProfile");
        cmd5.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpTypeStatus = cmd5.Parameters.Add("EmpTypeStatus", SqlDbType.Char);
        p_EmpTypeStatus.Direction = ParameterDirection.Input;
        p_EmpTypeStatus.Value = strTypeId;

        objDC.CreateDSFromProc(cmd5, "SelectEmpLvProfile");
        return objDC.ds.Tables["SelectEmpLvProfile"];
    }
    public DataTable SelectEmp()
    {
        SqlCommand cmd5 = new SqlCommand("proc_Select_EmpInfo_leaveProfile");
        cmd5.CommandType = CommandType.StoredProcedure;

        objDC.CreateDSFromProc(cmd5, "SelectEmp");
        return objDC.ds.Tables["SelectEmp"];
    }
    public void InsertLeaveType(LeaveType Lvt, string IsUpdate, string IsDelete, string IsActive)
    {
        SqlCommand command = new SqlCommand("proc_Insert_LeaveTypeList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LTypeID = command.Parameters.Add("LTypeID", SqlDbType.Decimal);
        p_LTypeID.Direction = ParameterDirection.Input;
        p_LTypeID.Value = Lvt.LTypeID;

        SqlParameter p_LTypeTitle = command.Parameters.Add("LTypeTitle", SqlDbType.VarChar);
        p_LTypeTitle.Direction = ParameterDirection.Input;
        p_LTypeTitle.Value = Lvt.LTypeTitle;

        SqlParameter p_LeaveDesc = command.Parameters.Add("LeaveDesc", SqlDbType.VarChar);
        p_LeaveDesc.Direction = ParameterDirection.Input;
        p_LeaveDesc.Value = Lvt.LeaveDesc;

        SqlParameter p_LAbbrName = command.Parameters.Add("LAbbrName", SqlDbType.VarChar);
        p_LAbbrName.Direction = ParameterDirection.Input;
        p_LAbbrName.Value = Lvt.LAbbrName;
        
        SqlParameter p_LMunit = command.Parameters.Add("LMunit", SqlDbType.Char);
        p_LMunit.Direction = ParameterDirection.Input;
        p_LMunit.Value = Lvt.LMunit;

        SqlParameter p_LNature = command.Parameters.Add("LNature", SqlDbType.Char);
        p_LNature.Direction = ParameterDirection.Input;
        p_LNature.Value = Lvt.LNature;

        SqlParameter p_LeaveTTL = command.Parameters.Add("LeaveTTL", SqlDbType.Char);
        p_LeaveTTL.Direction = ParameterDirection.Input;
        p_LeaveTTL.Value = Lvt.LeaveTTL;

        SqlParameter p_maxCarryLimit = command.Parameters.Add("CarryToNextYear", SqlDbType.VarChar);
        p_maxCarryLimit.Direction = ParameterDirection.Input;
        p_maxCarryLimit.Value = Lvt.maxCarryLimit;

        SqlParameter p_LCalcType = command.Parameters.Add("NumberofDaysInCashAble", SqlDbType.Char);
        p_LCalcType.Direction = ParameterDirection.Input;
        p_LCalcType.Value = Lvt.MaxCarryCashDay;

        SqlParameter p_Eligibility = command.Parameters.Add("Eligibility", SqlDbType.Char);
        p_Eligibility.Direction = ParameterDirection.Input;
        p_Eligibility.Value = Lvt.Eligibility;

        SqlParameter p_NextLevInterval = command.Parameters.Add("NextLevInterval", SqlDbType.Char);
        p_NextLevInterval.Direction = ParameterDirection.Input;
        p_NextLevInterval.Value = Lvt.NextLevInterval;

        SqlParameter p_TotalMatLev = command.Parameters.Add("TotalMatLev", SqlDbType.Char);
        p_TotalMatLev.Direction = ParameterDirection.Input;
        p_TotalMatLev.Value = Lvt.TotalMatLev;

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = Lvt.FiscalYrId;

        SqlParameter p_IsOffdayCounted = command.Parameters.Add("IsOffdayCounted", SqlDbType.Char);
        p_IsOffdayCounted.Direction = ParameterDirection.Input;
        p_IsOffdayCounted.Value = Lvt.IsOffdayCounted;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;

        SqlParameter p_IsDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_IsDeleted.Direction = ParameterDirection.Input;
        p_IsDeleted.Value = Lvt.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = Lvt.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = Lvt.InsertedDate;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        try
        {
            objDC.ExecuteQuery(command);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            command = null;
        }
    }    

    public void UpdateEmpLeaveProfileM(GridView grv, string strEmpId,string strUserId)
    {
        SqlCommand[] cmd;
        cmd = new SqlCommand[(grv.Rows.Count * 2) + 2];
        string LTypeID;
        string lvOpening;
        string lvPrevYearCarry;
        string LCarryOverd;
        string LEntitled;
        string LeaveEnjoyed;
        int i = -1;
        int j = 1;
        string strLogId = "";
        strLogId = Common.getMaxId("EmpLeaveProfilelog", "LogId");
            
        foreach (GridViewRow tt in grv.Rows)
        {
            CheckBox chBox = new CheckBox();
            chBox = (CheckBox)tt.Cells[0].FindControl("chkBox");
            if (chBox.Checked == true)
            {
                HiddenField hfLTypeID = (HiddenField)tt.Cells[3].FindControl("hfLTypeID");
                LTypeID = hfLTypeID.Value.ToString();

                TextBox txtOpening = (TextBox)tt.Cells[4].FindControl("txtOpening");
                lvOpening = Common.ReturnZeroForNull(txtOpening.Text);

                TextBox txtPrevYearCarry = (TextBox)tt.Cells[2].FindControl("txtPrevYearCarry");
                lvPrevYearCarry = Common.ReturnZeroForNull(txtPrevYearCarry.Text);

                TextBox txtLCarryOverd = (TextBox)tt.Cells[3].FindControl("txtLCarryOverd");
                LCarryOverd = Common.ReturnZeroForNull(txtLCarryOverd.Text);

                TextBox txtLEntitled = (TextBox)tt.Cells[4].FindControl("txtLEntitled");
                LEntitled = Common.ReturnZeroForNull(txtLEntitled.Text);

                TextBox txtLeaveEnjoyed = (TextBox)tt.Cells[5].FindControl("txtLeaveEnjoyed");
                LeaveEnjoyed = Common.ReturnZeroForNull(txtLeaveEnjoyed.Text);

                i++;
                cmd[i] = this.UpdateEmpLeaveProfile(strEmpId, LTypeID, lvOpening, LCarryOverd, LEntitled, LeaveEnjoyed, strUserId, lvPrevYearCarry);
                //i++;

                j = i + 1;
                i++;
                cmd[j] = this.InsertEmpLeaveProfileLog(strLogId, strEmpId, LTypeID, lvOpening, LCarryOverd, LEntitled, LeaveEnjoyed, strUserId, lvPrevYearCarry);
                int LogId = Convert.ToInt32(strLogId) + 1;
                strLogId = LogId.ToString();
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

    public SqlCommand UpdateEmpLeaveProfile(string EmpId, string LTypeID, string lvOpening,
        string strLCarryOverd, string strLEntitled, string strLeaveEnjoyed,
        string strUserId, string lvPrevYearCarry)
    {
        SqlCommand cmd = new SqlCommand("proc_Update_EmpLeaveProfile_opening");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_LTypeID = cmd.Parameters.Add("LTypeID", SqlDbType.BigInt);
        p_LTypeID.Direction = ParameterDirection.Input;
        p_LTypeID.Value = LTypeID;

        SqlParameter p_lvOpening = cmd.Parameters.Add("lvOpening", SqlDbType.Decimal);
        p_lvOpening.Direction = ParameterDirection.Input;
        p_lvOpening.Value = lvOpening;

        SqlParameter p_LCarryOverd = cmd.Parameters.Add("LCarryOverd", SqlDbType.Decimal);
        p_LCarryOverd.Direction = ParameterDirection.Input;
        p_LCarryOverd.Value = Convert.ToDecimal(strLCarryOverd);

        SqlParameter p_LEntitled = cmd.Parameters.Add("LEntitled", SqlDbType.Decimal);
        p_LEntitled.Direction = ParameterDirection.Input;
        p_LEntitled.Value = Convert.ToDecimal(strLEntitled);

        SqlParameter p_LCashed = cmd.Parameters.Add("LCashed", SqlDbType.Decimal);
        p_LCashed.Direction = ParameterDirection.Input;
        p_LCashed.Value = 0;

        SqlParameter p_LeaveEnjoyed = cmd.Parameters.Add("LeaveEnjoyed", SqlDbType.Decimal);
        p_LeaveEnjoyed.Direction = ParameterDirection.Input;
        p_LeaveEnjoyed.Value = Convert.ToDecimal(strLeaveEnjoyed);

        SqlParameter p_LeaveElapsed = cmd.Parameters.Add("LeaveElapsed", SqlDbType.Decimal);
        p_LeaveElapsed.Direction = ParameterDirection.Input;
        p_LeaveElapsed.Value = 0;

        SqlParameter p_lvPrevYearCarry = cmd.Parameters.Add("lvPrevYearCarry", SqlDbType.Decimal);
        p_lvPrevYearCarry.Direction = ParameterDirection.Input;
        p_lvPrevYearCarry.Value = lvPrevYearCarry;

        SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strUserId;

        SqlParameter p_UpdatedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_UpdatedDate.Direction = ParameterDirection.Input;
        p_UpdatedDate.Value = Common.SetDateTime(DateTime.Now.ToString());

        return cmd;

    }

    public SqlCommand InsertEmpLeaveProfileLog(string strLogId, string EmpId, string LTypeID, string lvOpening,
        string strLCarryOverd, string strLEntitled, string strLeaveEnjoyed, string strUserId, string lvPrevYearCarry)
    {
        SqlCommand cmd = new SqlCommand("proc_Insert_EmpLeaveProfilelog");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LogId = cmd.Parameters.Add("LogId", SqlDbType.BigInt);
        p_LogId.Direction = ParameterDirection.Input;
        p_LogId.Value = Convert.ToInt32(strLogId);

        SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_LTypeID = cmd.Parameters.Add("LTypeID", SqlDbType.BigInt);
        p_LTypeID.Direction = ParameterDirection.Input;
        p_LTypeID.Value = LTypeID;

        SqlParameter p_lvOpening = cmd.Parameters.Add("lvOpening", SqlDbType.Decimal);
        p_lvOpening.Direction = ParameterDirection.Input;
        p_lvOpening.Value = lvOpening;

        SqlParameter p_LCarryOverd = cmd.Parameters.Add("LCarryOverd", SqlDbType.Decimal);
        p_LCarryOverd.Direction = ParameterDirection.Input;
        p_LCarryOverd.Value = Convert.ToDecimal(strLCarryOverd);

        SqlParameter p_LEntitled = cmd.Parameters.Add("LEntitled", SqlDbType.Decimal);
        p_LEntitled.Direction = ParameterDirection.Input;
        p_LEntitled.Value = Convert.ToDecimal(strLEntitled);

        SqlParameter p_LCashed = cmd.Parameters.Add("LCashed", SqlDbType.Decimal);
        p_LCashed.Direction = ParameterDirection.Input;
        p_LCashed.Value = 0;

        SqlParameter p_LeaveEnjoyed = cmd.Parameters.Add("LeaveEnjoyed", SqlDbType.Decimal);
        p_LeaveEnjoyed.Direction = ParameterDirection.Input;
        p_LeaveEnjoyed.Value = Convert.ToDecimal(strLeaveEnjoyed);

        SqlParameter p_LeaveElapsed = cmd.Parameters.Add("LeaveElapsed", SqlDbType.Decimal);
        p_LeaveElapsed.Direction = ParameterDirection.Input;
        p_LeaveElapsed.Value = 0;

        SqlParameter p_lvPrevYearCarry = cmd.Parameters.Add("lvPrevYearCarry", SqlDbType.Decimal);
        p_lvPrevYearCarry.Direction = ParameterDirection.Input;
        p_lvPrevYearCarry.Value = lvPrevYearCarry;

        SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strUserId;

        SqlParameter p_UpdatedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_UpdatedDate.Direction = ParameterDirection.Input;
        p_UpdatedDate.Value = Common.SetDateTime(DateTime.Now.ToString());

        return cmd;

    }
    public void InsertLeavePakMst(LeavePakMst Lpm, string IsUpdate, string IsDelete, string IsActive, GridView grLeaveList)
    {
        SqlCommand[] cmd;
        cmd = new SqlCommand[grLeaveList.Rows.Count + 3];

        cmd[0] = new SqlCommand("proc_Delete_LeavePakDet");
        cmd[0].CommandType = CommandType.StoredProcedure;
        SqlParameter p_LPakIDD = cmd[0].Parameters.Add("LeavePakID", SqlDbType.Decimal);
        p_LPakIDD.Direction = ParameterDirection.Input;
        p_LPakIDD.Value = Lpm.LPakID;

        cmd[1] = new SqlCommand("proc_Insert_LeavePakMst");
        cmd[1].CommandType = CommandType.StoredProcedure;

        SqlParameter p_LPakID = cmd[1].Parameters.Add("LeavePakID", SqlDbType.BigInt);
        p_LPakID.Direction = ParameterDirection.Input;
        p_LPakID.Value = Lpm.LPakID;

        SqlParameter p_LPackName = cmd[1].Parameters.Add("LPackName", SqlDbType.VarChar);
        p_LPackName.Direction = ParameterDirection.Input;
        p_LPackName.Value = Lpm.LPackName;

        SqlParameter p_LPdesc = cmd[1].Parameters.Add("LPdesc", SqlDbType.VarChar);
        p_LPdesc.Direction = ParameterDirection.Input;
        p_LPdesc.Value = Lpm.LPdesc;

        SqlParameter p_IsOffdayCounted = cmd[1].Parameters.Add("IsOffdayCounted", SqlDbType.Char);
        p_IsOffdayCounted.Direction = ParameterDirection.Input;
        p_IsOffdayCounted.Value = Lpm.IsOffdayCounted;

        SqlParameter p_IsLCalOnJoinDate = cmd[1].Parameters.Add("IsLCalOnJoinDate", SqlDbType.Char);
        p_IsLCalOnJoinDate.Direction = ParameterDirection.Input;
        p_IsLCalOnJoinDate.Value = Lpm.IsLCalOnJoinDate;

        SqlParameter p_IsActive = cmd[1].Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = Lpm.IsActive;

        SqlParameter p_EmpTypeStatus = cmd[1].Parameters.Add("EmpTypeId", DBNull.Value);
        p_EmpTypeStatus.Direction = ParameterDirection.Input;
        p_EmpTypeStatus.IsNullable = true;
        if (Lpm.EmpTypeStatus!="")
            p_EmpTypeStatus.Value = Lpm.EmpTypeStatus;

        SqlParameter p_FromMonth = cmd[1].Parameters.Add("FromMonth", DBNull.Value);
        p_FromMonth.Direction = ParameterDirection.Input;
        p_FromMonth.IsNullable = true;
        if (Lpm.FromMonth != "")
            p_FromMonth.Value = Lpm.FromMonth;

        SqlParameter p_ToMonth = cmd[1].Parameters.Add("ToMonth", DBNull.Value);
        p_ToMonth.Direction = ParameterDirection.Input;
        p_ToMonth.IsNullable = true;
        if (Lpm.ToMonth != "")
            p_ToMonth.Value = Lpm.ToMonth;

        SqlParameter p_IsNextYear = cmd[1].Parameters.Add("IsNextYear", SqlDbType.Char);
        p_IsNextYear.Direction = ParameterDirection.Input;
        p_IsNextYear.Value = Lpm.IsNextYear;

        SqlParameter p_IsDeleted = cmd[1].Parameters.Add("IsDeleted", SqlDbType.Char);
        p_IsDeleted.Direction = ParameterDirection.Input;
        p_IsDeleted.Value = Lpm.IsDeleted;

        SqlParameter p_ISDefault = cmd[1].Parameters.Add("ISDefault", SqlDbType.Char);
        p_ISDefault.Direction = ParameterDirection.Input;
        p_ISDefault.Value = Lpm.ISDefault;

        SqlParameter p_InsertedDate = cmd[1].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = Lpm.InsertedDate;

        SqlParameter p_InsertedBy = cmd[1].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = Lpm.InsertedBy;

        SqlParameter p_IsUpdate = cmd[1].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = cmd[1].Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        long i = 2;

        foreach (GridViewRow tt in grLeaveList.Rows)
        {
            string LPakID;
            string LTypeID;
            string MaxLAmt;

            Boolean chkAll = Convert.ToBoolean(((CheckBox)tt.Cells[1].FindControl("chkSelect")).Checked);

            if (chkAll == true)
            {
                HiddenField hfLTypeID = (HiddenField)tt.Cells[2].FindControl("hfLTypeID");
                LTypeID = hfLTypeID.Value.ToString();

                LPakID = Lpm.LPakID;

                TextBox txtEntitled = (TextBox)tt.Cells[2].FindControl("txtEntitled");
                MaxLAmt = txtEntitled.Text;
                cmd[i] = InsertLeavePakDet(LPakID, LTypeID, MaxLAmt, Lpm.InsertedBy, Lpm.InsertedDate);
            }
            i++;
        }

        if (Lpm.LeaveStartPeriod != "" && Lpm.LeaveEndPeriod != "")
        {
            cmd[i] = new SqlCommand("proc_Insert_LeavePeriod");
            cmd[i].CommandType = CommandType.StoredProcedure;

            p_LPakID = cmd[i].Parameters.Add("LeavePakID", SqlDbType.BigInt);
            p_LPakID.Direction = ParameterDirection.Input;
            p_LPakID.Value = Lpm.LPakID;

            SqlParameter p_LeaveStartPeriod = cmd[i].Parameters.Add("LeaveStartPeriod", DBNull.Value);
            p_LeaveStartPeriod.Direction = ParameterDirection.Input;
            p_LeaveStartPeriod.IsNullable = true;
            if (Lpm.LeaveStartPeriod != "")
                p_LeaveStartPeriod.Value = Lpm.LeaveStartPeriod;

            SqlParameter p_LeaveEndPeriod = cmd[i].Parameters.Add("LeaveEndPeriod", DBNull.Value);
            p_LeaveEndPeriod.Direction = ParameterDirection.Input;
            p_LeaveEndPeriod.IsNullable = true;
            if (Lpm.LeaveEndPeriod != "")
                p_LeaveEndPeriod.Value = Lpm.LeaveEndPeriod;
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

    public SqlCommand InsertLeavePakDet(string LPakID, string LTypeID, string MaxLAmt,string strInsBy,string strInsDate)
    {
        SqlCommand cmd = new SqlCommand("proc_Insert_LeavePakDet");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LPakID = cmd.Parameters.Add("LeavePakID", SqlDbType.BigInt);
        p_LPakID.Direction = ParameterDirection.Input;
        p_LPakID.Value = LPakID;

        SqlParameter p_LTypeID = cmd.Parameters.Add("LTypeID", SqlDbType.BigInt);
        p_LTypeID.Direction = ParameterDirection.Input;
        p_LTypeID.Value = LTypeID;

        SqlParameter p_MaxLAmt = cmd.Parameters.Add("MaxLAmt", SqlDbType.Decimal);
        p_MaxLAmt.Direction = ParameterDirection.Input;
        p_MaxLAmt.Value = MaxLAmt;

        SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy; 

        SqlParameter p_UpdatedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_UpdatedDate.Direction = ParameterDirection.Input;
        p_UpdatedDate.Value = strInsDate; 

        return cmd;
    }

    #endregion    
    
    #region Select Queries From Tables By store procedure
    //Select LeaveType
    
    public DataTable SelectLeaveType(Int32 LTypeID)
    {
        if (objDC.ds.Tables["LeaveType"] != null)
        {
            objDC.ds.Tables["LeaveType"].Rows.Clear();
            objDC.ds.Tables["LeaveType"].Dispose();
        }
        SqlCommand command = new SqlCommand("proc_Select_LeaveType");
        SqlParameter p_LTypeID = command.Parameters.Add("LTypeID", SqlDbType.BigInt);
        p_LTypeID.Direction = ParameterDirection.Input;
        p_LTypeID.Value = LTypeID;
        objDC.CreateDSFromProc(command, "LeaveType");
        return objDC.ds.Tables["LeaveType"];
    }
    public DataTable SelectLeavePakMst(Int32 LPakID)
    {
        SqlCommand command = new SqlCommand("proc_Select_LeavePakMst");
        SqlParameter p_LTypeID = command.Parameters.Add("LeavePakID", SqlDbType.BigInt);
        p_LTypeID.Direction = ParameterDirection.Input;
        p_LTypeID.Value = LPakID;
        objDC.CreateDSFromProc(command, "LeavePakMst");
        return objDC.ds.Tables["LeavePakMst"];
    }

    public DataTable SelectLeavePakDet(Int32 LPakID)
    {
        SqlCommand command = new SqlCommand("proc_Select_LeavePakDet");
        SqlParameter p_LTypeID = command.Parameters.Add("LeavePakID", SqlDbType.BigInt);
        p_LTypeID.Direction = ParameterDirection.Input;
        p_LTypeID.Value = LPakID;
        objDC.CreateDSFromProc(command, "LeavePakDet");
        return objDC.ds.Tables["LeavePakDet"];
    }

    public DataTable GetLvTyCarryOverNature(string strLTypeId)
    {
        string strSQL = "SELECT * FROM LeaveTypeList WHERE LTypeId=" + Convert.ToInt32(strLTypeId)
            + " AND LNature IN('1','3')";
        SqlCommand cmd = new SqlCommand(strSQL);
        //cmd2.CommandType = CommandType.StoredProcedure;

        objDC.CreateDT(cmd, "LvTyCarryOverNature");
        return objDC.ds.Tables["LvTyCarryOverNature"];
    }
    #endregion

    #region Accrued Vacation Upload
   
    public DataTable Get_AccruedVacation()
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpAccruedVacation");

        objDC.CreateDT(command, "dtAccruedVacation");
        return objDC.ds.Tables["dtAccruedVacation"];
    }

    public void InsertAccruedVacationList(GridView grAccruedVList, string strVMonth, string strVYear,  string strFisYrId, string strInsBy, string strInsDate)
    {
        long i = 0;
        SqlCommand[] cmd;
        cmd = new SqlCommand[grAccruedVList.Rows.Count + 1];

        Double  dclAccrueId = 0;

        dclAccrueId = Convert.ToDouble(Common.getMaxId("EmpAccruedVacation", "AccrueId"));

        if (CheckDuplicateAccruedVacation(strVMonth, strVYear, strFisYrId) == true)
        {
            cmd[i] = DeleteAccruedVacationList(strVMonth, strVYear, strFisYrId);
            i++;
        }

        foreach (GridViewRow gRow in grAccruedVList.Rows)
        {
            cmd[i] = this.InsertAccruedVacation(dclAccrueId.ToString() , gRow.Cells[1].Text.Trim(), strVMonth, strVYear, strFisYrId, gRow.Cells[3].Text.Trim(), 
                gRow.Cells[4].Text.Trim(), gRow.Cells[5].Text.Trim(), gRow.Cells[6].Text.Trim(), gRow.Cells[7].Text.Trim(), strInsBy, strInsDate);
            i++;
            dclAccrueId++;
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

    public SqlCommand InsertAccruedVacation(string sAccruedId,string sEmpId, string sMonth, string sYear, string sFiscalYrId, string sAccruedNo,string sMonthlySalary,
        string sDailyRate, string sTotalAccrued,string sTotalAccruedUSD, string strInsBy, string strInsDate)
    {
        SqlCommand cmd = new SqlCommand("proc_Insert_EmpAccruedVacation");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_AccrueId = cmd.Parameters.Add("AccrueId", SqlDbType.BigInt);
        p_AccrueId.Direction = ParameterDirection.Input;
        p_AccrueId.Value = sAccruedId;

        SqlParameter p_LTypeID = cmd.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_LTypeID.Direction = ParameterDirection.Input;
        p_LTypeID.Value = sEmpId;

        SqlParameter p_VMonth = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = sMonth;

        SqlParameter p_VYear = cmd.Parameters.Add("VYear", SqlDbType.BigInt);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = sYear;

        SqlParameter p_FiscalYrId = cmd.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = sFiscalYrId;

        SqlParameter p_AccruedNo = cmd.Parameters.Add("AccruedNo", SqlDbType.Decimal);
        p_AccruedNo.Direction = ParameterDirection.Input;
        p_AccruedNo.Value = Convert.ToDecimal(sAccruedNo);

        SqlParameter p_MonthlySalary = cmd.Parameters.Add("MonthlySalary", SqlDbType.Decimal);
        p_MonthlySalary.Direction = ParameterDirection.Input;
        p_MonthlySalary.Value = Convert.ToDecimal(sMonthlySalary);

        SqlParameter p_DailyRate = cmd.Parameters.Add("DailyRate", SqlDbType.Decimal);
        p_DailyRate.Direction = ParameterDirection.Input;
        p_DailyRate.Value =  Convert.ToDecimal(sDailyRate);

        SqlParameter p_TotalAccrued = cmd.Parameters.Add("TotalAccrued", SqlDbType.Decimal);
        p_TotalAccrued.Direction = ParameterDirection.Input;
        p_TotalAccrued.Value =  Convert.ToDecimal(sTotalAccrued);

        SqlParameter p_TotalAccruedUSD = cmd.Parameters.Add("TotalAccruedUSD", SqlDbType.Decimal);
        p_TotalAccruedUSD.Direction = ParameterDirection.Input;
        p_TotalAccruedUSD.Value =  Convert.ToDecimal(sTotalAccruedUSD);

        SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_UpdatedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_UpdatedDate.Direction = ParameterDirection.Input;
        p_UpdatedDate.Value = strInsDate;

        return cmd;
    }

    public bool CheckDuplicateAccruedVacation(string strVMonth, string strVYear, string strFisYrId)
    {
        string strSQL = "SELECT VMonth,VYear,FiscalYrId FROM EmpAccruedVacation WHERE " +
            " VMonth=" + strVMonth + " AND VYear=" + strVYear + " AND FiscalYrId=" + strFisYrId;

        DataTable dtDupFinLv = objDC.CreateDT(strSQL, "DupEmpAccruedVacation");
        if (dtDupFinLv.Rows.Count > 0)
            return true;
        else
            return false;

    }

    public SqlCommand DeleteAccruedVacationList(string strMonth, string strYear, string strFisYrId)
    {
        SqlCommand command = new SqlCommand("proc_Delete_EmpAccruedVacation");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMonth = command.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = strMonth;

        SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.BigInt);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = strYear;

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = strFisYrId;

        return command;
    }
    #endregion

    #region Weekly Leave Accural

    public DataTable SelectEmpTypeWSLeaveProfile(string EmpTypeID,string strLeaveTypeId)
    {
        string strEmpLeavePro="";
        strEmpLeavePro = "SELECT elp.*,lt.LTypeTitle,lt.LAbbrName,e.FullName,e.JoiningDate,d.DesigName" +
            " FROM LeaveTypeList lt INNER JOIN EmpLeaveProfile elp ON lt.LTypeID=elp.LTypeID" +
            " INNER JOIN EmpInfo e ON elp.EmpId=e.EmpId" +
            " LEFT OUTER JOIN Designation d ON e.DesigId=d.DesigId" +
            " WHERE lt.LTypeId=@LTypeID AND e.JoiningDate<GETDATE() AND e.EmpTypeId=@EmpTypeId AND e.EmpStatus='A' AND e.IsDeleted='N' ORDER BY e.EmpId";

        SqlCommand sqlCmd = new SqlCommand(strEmpLeavePro);
        sqlCmd.CommandType = CommandType.Text;

        SqlParameter p_EmpTypeID = sqlCmd.Parameters.Add("EmpTypeId", SqlDbType.BigInt);
        p_EmpTypeID.Direction = ParameterDirection.Input;
        p_EmpTypeID.Value = Convert.ToInt32(EmpTypeID);

        SqlParameter p_LTypeID = sqlCmd.Parameters.Add("LTypeID", SqlDbType.BigInt);
        p_LTypeID.Direction = ParameterDirection.Input;
        p_LTypeID.Value = Convert.ToInt32(strLeaveTypeId);

        objDC.CreateDT(sqlCmd, "EmpTypeWSLeaveProfile");
        return objDC.ds.Tables["EmpTypeWSLeaveProfile"];
    }
    //public void UpdateEmployeeEarnLeave(GridView grEmp, string strLTypeId, string Week, string Month, string Year, string InsertedBy, string InsertedDate)
    //{
    //    long TransId = objDC.GerMaxIDNumber("EmpLeaveAccrual", "TransId");
    //    int i = 0;
    //    SqlCommand[] cmd = new SqlCommand[grEmp.Rows.Count * 3];
    //    foreach (GridViewRow gRow in grEmp.Rows)
    //    {
    //        CheckBox chkBox = (CheckBox)gRow.FindControl("chkBox");
    //        if (chkBox.Checked == true)
    //        {
    //            //Update EL to table EmpLeaveProfile
    //            cmd[i] = new SqlCommand("proc_Update_EmpLeaveProfileForAccure");
    //            cmd[i].CommandType = CommandType.StoredProcedure;

    //            SqlParameter p_EmpId = cmd[i].Parameters.Add("EmpId", SqlDbType.Char);
    //            p_EmpId.Direction = ParameterDirection.Input;
    //            p_EmpId.Value = gRow.Cells[2].Text.Trim();

    //            SqlParameter p_LTypeId = cmd[i].Parameters.Add("LTypeId", SqlDbType.BigInt);
    //            p_LTypeId.Direction = ParameterDirection.Input;
    //            p_LTypeId.Value = strLTypeId;

    //            SqlParameter p_LEntitled = cmd[i].Parameters.Add("LEntitled", SqlDbType.BigInt);
    //            p_LEntitled.Direction = ParameterDirection.Input;
    //            p_LEntitled.Value = gRow.Cells[8].Text.Trim();

    //            i++;

    //            //Save record to table EmpLeaveAccrual
    //            cmd[i] = new SqlCommand("proc_Insert_EmpLeaveAccrual");
    //            cmd[i].CommandType = CommandType.StoredProcedure;

    //            SqlParameter p_TransId = cmd[i].Parameters.Add("LogId", SqlDbType.BigInt);
    //            p_TransId.Direction = ParameterDirection.Input;
    //            p_TransId.Value = TransId;

    //            SqlParameter p_EmpId2 = cmd[i].Parameters.Add("EmpId", SqlDbType.Char);
    //            p_EmpId2.Direction = ParameterDirection.Input;
    //            p_EmpId2.Value = gRow.Cells[2].Text.Trim();

    //            SqlParameter p_VWeek = cmd[i].Parameters.Add("VWeek", SqlDbType.BigInt);
    //            p_VWeek.Direction = ParameterDirection.Input;
    //            p_VWeek.Value = Week;

    //            SqlParameter p_VMonth = cmd[i].Parameters.Add("VMonth", SqlDbType.BigInt);
    //            p_VMonth.Direction = ParameterDirection.Input;
    //            p_VMonth.Value = Month;

    //            p_LTypeId = cmd[i].Parameters.Add("LTypeId", SqlDbType.BigInt);
    //            p_LTypeId.Direction = ParameterDirection.Input;
    //            p_LTypeId.Value = strLTypeId;

    //            SqlParameter p_VYear = cmd[i].Parameters.Add("VYear", SqlDbType.BigInt);
    //            p_VYear.Direction = ParameterDirection.Input;
    //            p_VYear.Value = Year;

    //            SqlParameter p_ALAccrue = cmd[i].Parameters.Add("Accrue", SqlDbType.Decimal);
    //            p_ALAccrue.Direction = ParameterDirection.Input;
    //            p_ALAccrue.Value = gRow.Cells[7].Text.Trim();

    //            SqlParameter p_SLAccrue = cmd[i].Parameters.Add("LEntitled", SqlDbType.Decimal);
    //            p_SLAccrue.Direction = ParameterDirection.Input;
    //            p_SLAccrue.Value = gRow.Cells[8].Text.Trim();

    //            SqlParameter p_InsertedBy = cmd[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //            p_InsertedBy.Direction = ParameterDirection.Input;
    //            p_InsertedBy.Value = InsertedBy;

    //            SqlParameter p_InsertedDate = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //            p_InsertedDate.Direction = ParameterDirection.Input;
    //            p_InsertedDate.Value = InsertedDate;
    //            i++;
    //            TransId++;
    //        }
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

    public void InsertEmpLeaveAccrueLog(string strLogId, GridView grLvBalance, string strLTypeId,
         string strMonth, string strYear, string strInsBy, string strInsDate)
    {
        SqlCommand[] cmd;
        cmd = new SqlCommand[grLvBalance.Rows.Count * 2];
        int i = 0;
        foreach (GridViewRow gRow in grLvBalance.Rows)
        {
            cmd[i] = new SqlCommand("proc_Insert_EmpLeaveAccrual");
            cmd[i].CommandType = CommandType.StoredProcedure;

            SqlParameter p_LogId = cmd[i].Parameters.Add("LogId", SqlDbType.BigInt);
            p_LogId.Direction = ParameterDirection.Input;
            p_LogId.Value = strLogId;

            SqlParameter p_EmpId = cmd[i].Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = gRow.Cells[2].Text.Trim();

            SqlParameter p_VMONTH = cmd[i].Parameters.Add("VMONTH", SqlDbType.BigInt);
            p_VMONTH.Direction = ParameterDirection.Input;
            p_VMONTH.Value = strMonth;

            SqlParameter p_VYEAR = cmd[i].Parameters.Add("VYEAR", SqlDbType.BigInt);
            p_VYEAR.Direction = ParameterDirection.Input;
            p_VYEAR.Value = strYear;

            SqlParameter p_LTypeId = cmd[i].Parameters.Add("LTypeId", SqlDbType.BigInt);
            p_LTypeId.Direction = ParameterDirection.Input;
            p_LTypeId.Value = strLTypeId;

            SqlParameter p_OpeningBal = cmd[i].Parameters.Add("Accrue", SqlDbType.Decimal);
            p_OpeningBal.Direction = ParameterDirection.Input;
            p_OpeningBal.Value = gRow.Cells[7].Text.Trim();

            SqlParameter p_LEntitled = cmd[i].Parameters.Add("LEntitled", SqlDbType.Decimal);
            p_LEntitled.Direction = ParameterDirection.Input;
            p_LEntitled.Value = gRow.Cells[8].Text.Trim();

            SqlParameter p_InsertedBy = cmd[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = strInsBy;

            SqlParameter p_InsertedDate = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate.Direction = ParameterDirection.Input;
            p_InsertedDate.Value = strInsDate;

            i++;

            cmd[i] = UpdateEmpMonthlyLeaveEntitlement(gRow.Cells[2].Text.Trim(), strLTypeId, gRow.Cells[8].Text.Trim(), strInsBy, strInsDate);

            i++;
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

    private SqlCommand UpdateEmpMonthlyLeaveEntitlement(string strEmpId, string strLTypeId, string strEntitle, string strInsertedBy, string strInsertedDate)
    {
        string strSQL = "UPDATE EmpLeaveProfile SET LEntitled=@LEntitled,UpdatedBy=@UpdatedBy, "
                    + "UpdatedDate=@UpdatedDate WHERE EmpId=@EmpId AND LTypeId=@LTypeId ";

        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_LTypeId = command.Parameters.Add("LTypeId", SqlDbType.BigInt);
        p_LTypeId.Direction = ParameterDirection.Input;
        p_LTypeId.Value = strLTypeId;

        SqlParameter p_LEntitled = command.Parameters.Add("LEntitled", DBNull.Value);
        p_LEntitled.Direction = ParameterDirection.Input;
        p_LEntitled.IsNullable = true;
        if (strEntitle != "")
            p_LEntitled.Value = strEntitle;

        SqlParameter p_UpdatedBy = command.Parameters.Add("UpdatedBy", SqlDbType.VarChar);
        p_UpdatedBy.Direction = ParameterDirection.Input;
        p_UpdatedBy.Value = strInsertedBy;

        SqlParameter p_UpdatedDate = command.Parameters.Add("UpdatedDate", SqlDbType.DateTime);
        p_UpdatedDate.Direction = ParameterDirection.Input;
        p_UpdatedDate.Value = strInsertedDate;

        return command;
    }
    //Check for Weekly leave accure
    public bool CheckForMultipleEntry(string strLTypeId,  string strMonth, string strYear)
    {
        string strSQL = "SELECT * FROM EmpLeaveAccrual WHERE LTypeId=" + Convert.ToInt32(strLTypeId)
                + " AND VMonth=" + Convert.ToInt32(strMonth) + " AND VYear=" + Convert.ToInt32(strYear);
        SqlCommand cmd = new SqlCommand(strSQL);

        DataTable dtLvLog = new DataTable();
        dtLvLog = objDC.CreateDT(cmd, "EmpLeavAccrual");

        if (dtLvLog.Rows.Count > 0)
            return true;
        else
            return false;
    }

    public DataTable SelectEmpLeaveProfileForLeaveAccrue(string EmpId, string LTypeID, string vDate)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpLeaveProfileForLeaveAccrue");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_LTypeID = command.Parameters.Add("LTypeID", SqlDbType.BigInt);
        p_LTypeID.Direction = ParameterDirection.Input;
        p_LTypeID.Value = Convert.ToInt32(LTypeID);

        SqlParameter p_vDate = command.Parameters.Add("vDate", SqlDbType.DateTime);
        p_vDate.Direction = ParameterDirection.Input;
        p_vDate.Value = vDate;

        objDC.CreateDSFromProc(command, "LeaveProfileForLeaveAccrue");
        return objDC.ds.Tables["LeaveProfileForLeaveAccrue"];
    }
    #endregion

    #region LeaveTakenMatrix



    public void InsertLeaveTakenMatrix(string sId, string sPLTypeId, string sNPTypeId,string sInsBy, string sInsDate, string IsUpdate, string IsDeleted )
    {
        SqlCommand command = new SqlCommand("proc_Insert_LeaveTakenBarrier");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("Id", SqlDbType.Decimal);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = Convert.ToDecimal(sId);

        SqlParameter p_PTypeId = command.Parameters.Add("PLTypeId", SqlDbType.Decimal);
        p_PTypeId.Direction = ParameterDirection.Input;
        p_PTypeId.Value = Convert.ToDecimal(sPLTypeId);

        SqlParameter p_NLTypeId = command.Parameters.Add("NLTypeId", SqlDbType.Decimal);
        p_NLTypeId.Direction = ParameterDirection.Input;
        p_NLTypeId.Value =Convert.ToDecimal( sNPTypeId);
      
        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = sInsBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = sInsDate;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_IsDeleted.Direction = ParameterDirection.Input;
        p_IsDeleted.Value = IsDeleted;

        try
        {
            objDC.ExecuteQuery(command);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            command = null;
        }
    }

    public DataTable SelectLeaveTakenMatrix()
    {
        if (objDC.ds.Tables["LeaveTakenBarrier"] != null)
        {
            objDC.ds.Tables["LeaveTakenBarrier"].Rows.Clear();
            objDC.ds.Tables["LeaveTakenBarrier"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_Select_LeaveTakenBarrier");

        objDC.CreateDSFromProc(command, "LeaveTakenBarrier");
        return objDC.ds.Tables["LeaveTakenBarrier"];
    }
    #endregion
}