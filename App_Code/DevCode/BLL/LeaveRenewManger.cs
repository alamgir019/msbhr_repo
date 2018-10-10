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
/// Summary description for LeaveRenewManger
/// </summary>
public class LeaveRenewManger
{
    DBConnector objDC = new DBConnector();

    clsEmpLeaveProfile objEmpLevPro = new clsEmpLeaveProfile();

    public DataTable SelectOldLeavePeriod(string LPakId)
    {
        SqlCommand cmd = new SqlCommand("proc_select_leave_period");

        SqlParameter p_LPakId = cmd.Parameters.Add("LeavePakId", SqlDbType.BigInt);
        p_LPakId.Direction = ParameterDirection.Input;
        p_LPakId.Value = Convert.ToInt32(LPakId);

        objDC.CreateDSFromProc(cmd, "leave_period");
        return objDC.ds.Tables["leave_period"];
    }
    ////Actual Code
    //public void UpdateLeaveProfile(GridView grEmployee, string strLPakId)
    //{
    //    SqlCommand[] cmd = new SqlCommand[grEmployee.Rows.Count];
    //    int i = 0;
    //    //foreach (GridViewRow gRow in grEmployee.Rows)
    //    //{
    //    //    CheckBox chBox = new CheckBox();
    //    //    chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
    //    //    if (chBox.Checked == true)
    //    //    {
    //    cmd[i] = new SqlCommand("Leave_renew");
    //    cmd[i].CommandType = CommandType.StoredProcedure;

    //    //SqlParameter p_EmpId = cmd[i].Parameters.Add("ProfileEmpId", SqlDbType.Char);
    //    //p_EmpId.Direction = ParameterDirection.Input;
    //    //p_EmpId.Value = gRow.Cells[2].Text.Trim();

    //    SqlParameter p_LPakId = cmd[i].Parameters.Add("LPakId", SqlDbType.BigInt);
    //    p_LPakId.Direction = ParameterDirection.Input;
    //    p_LPakId.Value = strLPakId;
    //    //    }
    //    //    i++;
    //    //}
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
    //Actual Code
    public void UpdateLeaveProfile(GridView grEmployee, string strLPakId)
    {
        SqlCommand cmd = new SqlCommand("Leave_renew");
        cmd.CommandType = CommandType.StoredProcedure;

        //SqlParameter p_EmpId = cmd[i].Parameters.Add("ProfileEmpId", SqlDbType.Char);
        //p_EmpId.Direction = ParameterDirection.Input;
        //p_EmpId.Value = gRow.Cells[2].Text.Trim();

        SqlParameter p_LPakId = cmd.Parameters.Add("LeavePakId", SqlDbType.BigInt);
        p_LPakId.Direction = ParameterDirection.Input;
        p_LPakId.Value = strLPakId;

        try
        {
            objDC.ExecuteQuery(cmd);
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

    public void UpdateLeaveEntitlement(string UserId)
    {
        try
        {
            int i = 0;
            DataTable dtLeavePeriod = new DataTable();
            DataTable dtLevProfile = new DataTable();
            DataTable dtLevPakMst = new DataTable();

            int LeaveAvail = 0;
            int LEntitled = 0;
            decimal tempValue = 0;
            string IsLeaveOnJoinDate = "";

            DateTime JoinYear;
            DateTime CurrYear = DateTime.Now;
            clsEmpLeaveProfile objEmpLevPro = new clsEmpLeaveProfile();            

            if (dtLeavePeriod.Rows.Count == 0)
            {
                dtLeavePeriod = SelectLeavePeriod();
            }

            dtLevProfile = SelectEmpLeaveProfile();

            SqlCommand[] cmd = new SqlCommand[dtLevProfile.Rows.Count];

            if (dtLevProfile.Rows.Count > 0)
            {
                IsLeaveOnJoinDate = dtLevProfile.Rows[0]["IsLCalOnJoinDate"].ToString();
            }

            if (dtLevProfile.Rows.Count > 0)
            {
                foreach (DataRow LTRow in dtLevProfile.Rows)
                {
                    DataRow[] foundRows;
                    string strExpr = "";
                    strExpr = "LTypeId='" + LTRow["LTypeId"].ToString().Trim() + "'";

                    foundRows = dtLevProfile.Select(strExpr);
                    if (foundRows.Length > 0)//data found in profile so update it with new entitled amount
                    {
                        if (IsLeaveOnJoinDate == "Y")
                        {
                            LeaveAvail = 0;
                            if (LTRow["MaxLAmt"].ToString() != "0")
                            {
                                if (LTRow["LAbbrName"].ToString().Trim() != "AL")// All Leave Entitled Except Annual Leave
                                {
                                    LEntitled = Convert.ToInt32(LTRow["MaxLAmt"]);
                                }                                
                                else// "AL"
                                {
                                    foreach (DataRow dRow in dtLeavePeriod.Rows)
                                    {
                                        CurrYear = Convert.ToDateTime(dRow["LeaveStartPeriod"]);
                                    }
                                    CurrYear = CurrYear.AddYears(-1);
                                    
                                    if (LTRow["JoiningDate"].ToString() == "")
                                    {
                                        JoinYear = CurrYear;
                                    }
                                    else
                                    {
                                        JoinYear = Convert.ToDateTime(LTRow["JoiningDate"].ToString());
                                    }
                                    if (JoinYear > CurrYear.AddYears(1))
                                    {
                                        LEntitled = 0;
                                    }
                                    else if (JoinYear > CurrYear && (JoinYear < CurrYear.AddYears(1)))
                                    {
                                        TimeSpan DateDiff = JoinYear - CurrYear;
                                        string strTotDay = ReturnTotalDay(DateDiff.ToString());
                                        tempValue = (Convert.ToDecimal(LTRow["MaxLAmt"]) / 365);
                                        tempValue = (tempValue * (365 - Convert.ToInt32(strTotDay)));
                                        LeaveAvail = Convert.ToInt32(tempValue);
                                        LEntitled = LeaveAvail;
                                    }
                                    else
                                        LEntitled = Convert.ToInt32(LTRow["MaxLAmt"]);
                                }
                            }
                            else
                                LEntitled = Convert.ToInt32(LTRow["MaxLAmt"]);
                        }
                        else
                        {
                            LEntitled = Convert.ToInt32(LTRow["MaxLAmt"]);
                        }
                        objEmpLevPro = new clsEmpLeaveProfile(LTRow["EmpId"].ToString(), LTRow["LTypeId"].ToString().Trim(), LEntitled.ToString(),
                            UserId, Common.SetDateTime(DateTime.Now.ToString()));

                        cmd[i] = InsertIntoEmpLevPro(objEmpLevPro, "Y", "N");
                        i++;
                    }
                }
            }            
            
            dtLevProfile.Rows.Clear();
            i++;
            objDC.MakeTransaction(cmd);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    public SqlCommand InsertIntoEmpLevPro(clsEmpLeaveProfile clEmp, string strIsUpdate, string strIsDelete)
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

            SqlParameter p_LEntitled = cmd.Parameters.Add("LEntitled", SqlDbType.BigInt);
            p_LEntitled.Direction = ParameterDirection.Input;
            p_LEntitled.Value = clEmp.LEntitled;

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


    public void UpdateMedicalProfile(GridView grEmployee, string strLPakId)
    {
        SqlCommand cmd = new SqlCommand("Leave_renew");
        cmd.CommandType = CommandType.StoredProcedure;

        //SqlParameter p_EmpId = cmd[i].Parameters.Add("ProfileEmpId", SqlDbType.Char);
        //p_EmpId.Direction = ParameterDirection.Input;
        //p_EmpId.Value = gRow.Cells[2].Text.Trim();

        SqlParameter p_LPakId = cmd.Parameters.Add("LeavePakId", SqlDbType.BigInt);
        p_LPakId.Direction = ParameterDirection.Input;
        p_LPakId.Value = strLPakId;

        try
        {
            objDC.ExecuteQuery(cmd);
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
    public SqlCommand InsertLeaveTypeHistory()
    {
        string strTransId = Common.getMaxId("LeaveTypeHisList", "TransId");
        string strSQL = "INSERT INTO LeaveTypeHisList SELECT " + strTransId + ",LTypeID, LTypeTitle, LeaveDesc,LAbbrName, LMunit," +
            "LNature,LeaveTTL,CarryToNextYear,NumberofDaysInCashAble,MaxDaysAllowablePerYear,MinValidationPeriodInMonth,MaxTotalAllowable,"+
            "MaximumAllowed,MinimumAllowed,FiscalYrId,IsActive,IsDeleted,InsertedBy,InsertedDate,UpdatedBy,UpdatedDate";

        SqlCommand command = new SqlCommand(strSQL);

        return command;
    }

    public DataTable SelectEmpLeaveProfile()
    {
        SqlCommand cmd3 = new SqlCommand("proc_4_Select_EmpLeaveProfile");
        cmd3.CommandType = CommandType.StoredProcedure;

        objDC.CreateDSFromProc(cmd3, "EmpLeaveProfile");
        return objDC.ds.Tables["EmpLeaveProfile"];
    }

    public string ReturnTotalDay(string Day)
    {
        string[] arInfo = new string[2];
        string strDay;

        char[] splitter = { '.' };
        arInfo = Common.str_split(Day, splitter);

        strDay = arInfo[0];

        return strDay;
    }

    public DataTable SelectLeavePeriod()
    {
        SqlCommand cmd4 = new SqlCommand("Sel_Leave_period");
        cmd4.CommandType = CommandType.StoredProcedure;

        objDC.CreateDSFromProc(cmd4, "Leaveperiod");
        return objDC.ds.Tables["Leaveperiod"];
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
    public DataTable SelectLvPackageWsEmp(string LPakId)
    {
        string strCond = "";
        if (LPakId != "-1")
            strCond = "AND E.LeavePakId=@LPakID";
        else
            strCond = "";

        string strSql = "SELECT E.EmpId,E.FullName,E.DeptId,DP.DeptName,E.DesigId,J.DesigName AS JobTitle,L.ClinicName,"
            + " E.CardNo,E.WeekEndID,E.AttnPolicyID,E.SupervisorId,E.JoiningDate,E.ConfirmationDate,"
            + " E.EmpStatus,E.BankAccNo,E.IsRoaster,E.DivisionID,E.ClinicID,DV.DivisionName,E.GradeId,"
            + " E.LeavePakId,et.TypeName,Lp.LPackName FROM EmpInfo E INNER JOIN DepartmentList DP ON E.DeptId=DP.DeptId"
            + " LEFT OUTER JOIN DivisionList DV ON E.DivisionID=DV.DivisionID LEFT OUTER JOIN GradeList G ON E.GradeId=G.GradeID"
            + " LEFT OUTER JOIN Designation J ON E.DesigId=J.DesigId LEFT OUTER JOIN ClinicList L ON E.ClinicID=L.ClinicID"
            + " LEFT OUTER JOIN EmpTypeList et ON E.EmpTypeId=et.EmpTypeId LEFT OUTER JOIN LeavePakMst lp ON E.LeavePakId=lp.LeavePakId"
            + " WHERE E.IsDeleted='N' AND E.EmpSTATUS='A'" + strCond;

        SqlCommand cmd = new SqlCommand(strSql);
        //cmd2.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LPakId = cmd.Parameters.Add("LPakId", SqlDbType.BigInt);
        p_LPakId.Direction = ParameterDirection.Input;
        p_LPakId.Value = Convert.ToInt32(LPakId);

        objDC.CreateDT(cmd, "LeavePakWsEmp");
        return objDC.ds.Tables["LeavePakWsEmp"];
        //objDC.CreateDSFromProc(cmd2, "LeavePakWsEmp");
        //return objDC.ds.Tables["LeavePakWsEmp"];
    }
	public LeaveRenewManger()
	{
		//
		// TODO: Add constructor logic here
		//
    }


    #region Update LeaveProfile Before 1st July 2010
    public DataTable SelectEmpLeaveProfileAfter1stJuly()
    {
        SqlCommand cmd3 = new SqlCommand("proc_5_Select_EmpLeaveProfile_After_1st_July_2010");
        cmd3.CommandType = CommandType.StoredProcedure;

        objDC.CreateDSFromProc(cmd3, "EmpLeaveProfile");
        return objDC.ds.Tables["EmpLeaveProfile"];
    }
    public void UpdateLeaveProfileAfterJuly1st()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SelectEmpLeaveProfileAfter1stJuly();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    decimal LvEnjoyed = 0;
                    DBConnector objDC = new DBConnector();
                    LvEnjoyed = Convert.ToDecimal(row["LeaveEnjoyed"].ToString()) + Convert.ToDecimal(row["Duration"].ToString());

                    SqlCommand cmd = new SqlCommand("Proc_Update_Leave_Profile_After_1st_July_2010");
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.Char);
                    p_EmpId.Direction = ParameterDirection.Input;
                    p_EmpId.Value = row["EmpId"].ToString().Trim();

                    SqlParameter p_LTypeID = cmd.Parameters.Add("LTypeID", SqlDbType.BigInt);
                    p_LTypeID.Direction = ParameterDirection.Input;
                    p_LTypeID.Value = row["LTypeId"].ToString();

                    SqlParameter p_LeaveEnjoyed = cmd.Parameters.Add("LeaveEnjoyed", SqlDbType.BigInt);
                    p_LeaveEnjoyed.Direction = ParameterDirection.Input;
                    p_LeaveEnjoyed.Value = LvEnjoyed;

                    objDC.ExecuteQuery(cmd);
                    cmd = null;                    
                }
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            //cmd = null;
        }
    }


    public DataTable SelectEmpLeaveProfileBefore1stJuly()
    {
        SqlCommand cmd3 = new SqlCommand("proc_6_Select_EmpLeaveHisProfile_Before_1st_July_2010");
        cmd3.CommandType = CommandType.StoredProcedure;

        objDC.CreateDSFromProc(cmd3, "EmpLeaveProfile6");
        return objDC.ds.Tables["EmpLeaveProfile6"];
    }

    public void UpdateLeaveProfileBeforeJuly1st()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SelectEmpLeaveProfileBefore1stJuly();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    decimal LvEnjoyed = 0;
                    DBConnector objDC = new DBConnector();
                    LvEnjoyed = Convert.ToDecimal(row["LeaveEnjoyed"].ToString()) - Convert.ToDecimal(row["Duration"].ToString());

                    if (Convert.ToDecimal(row["LeaveEnjoyed"].ToString()) >= Convert.ToDecimal(row["Duration"].ToString()))
                    {
                        SqlCommand cmd = new SqlCommand("Proc_Update_Leave_Profile_Before_1st_July_2010");
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.Char);
                        p_EmpId.Direction = ParameterDirection.Input;
                        p_EmpId.Value = row["EmpId"].ToString().Trim();

                        SqlParameter p_LTypeID = cmd.Parameters.Add("LTypeID", SqlDbType.BigInt);
                        p_LTypeID.Direction = ParameterDirection.Input;
                        p_LTypeID.Value = row["LTypeId"].ToString();

                        SqlParameter p_LeaveEnjoyed = cmd.Parameters.Add("LeaveEnjoyed", SqlDbType.BigInt);
                        p_LeaveEnjoyed.Direction = ParameterDirection.Input;
                        p_LeaveEnjoyed.Value = LvEnjoyed;

                        objDC.ExecuteQuery(cmd);
                        cmd = null;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            //cmd = null;
        }
    }
    #endregion


}
