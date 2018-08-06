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
/// Summary description for UserManager
/// </summary>
public class UserManager
{
    DBConnector objDC = new DBConnector();

    #region Insert Update Delete From Tables By Store procedure
    //*Sulata
    public void InsertUser(UserCreation User, string IsUpdate, string IsDelete,DataTable dt,string strPrivPackID)
    {
        SqlCommand[] command;
        command = new SqlCommand[dt.Rows.Count + 2];
        // sproc functionality
        command[0] = new SqlCommand("proc_Insert_User");
        command[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_USERID = command[0].Parameters.Add("USERID", SqlDbType.Char);
        p_USERID.Direction = ParameterDirection.Input;
        p_USERID.Value = User.USERID;

        SqlParameter p_PASSWORD = command[0].Parameters.Add("PASSWORD", SqlDbType.Char);
        p_PASSWORD.Direction = ParameterDirection.Input;
        p_PASSWORD.Value = User.PASSWORD;

        SqlParameter p_AccountDisabled = command[0].Parameters.Add("AccountDisabled", SqlDbType.Char);
        p_AccountDisabled.Direction = ParameterDirection.Input;
        p_AccountDisabled.Value = User.AccountDisabled;

        SqlParameter p_ChangePassword = command[0].Parameters.Add("ChangePassword", SqlDbType.Char);
        p_ChangePassword.Direction = ParameterDirection.Input;
        p_ChangePassword.Value = User.ChangePassword;

        SqlParameter p_EmpId = command[0].Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = User.EmpId;

        SqlParameter p_IsAdmin = command[0].Parameters.Add("IsAdmin", SqlDbType.Char);
        p_IsAdmin.Direction = ParameterDirection.Input;
        p_IsAdmin.Value = User.IsAdmin;

        SqlParameter p_InsertedBy = command[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = User.InsertedBy;

        SqlParameter p_InsertedDate = command[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = User.InsertedDate;

        SqlParameter p_IsUpdate = command[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command[0].Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_PrivPackID = command[0].Parameters.Add("PrivPackID", SqlDbType.VarChar);
        p_PrivPackID.Direction = ParameterDirection.Input;
        p_PrivPackID.Value = strPrivPackID;

        // DELETE USER PERMISSSION
        command[1] = new SqlCommand("proc_Delete_UserPrivs");
        command[1].CommandType = CommandType.StoredProcedure;

        SqlParameter pm_USERID = command[1].Parameters.Add("USERID", SqlDbType.Char);
        pm_USERID.Direction = ParameterDirection.Input;
        pm_USERID.Value = User.USERID;
        
        // INSERT USER PERMISSION
        int i = 2;
        foreach (DataRow dRow in dt.Rows)
        {
            
            command[i] = InsertUserPrevDetails(User.USERID, dRow["VIEWID"].ToString(), "Y", User.InsertedBy, User.InsertedDate);
            i++;
        }

        try
        {
            objDC.MakeTransaction(command);
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

    //public void InsertUserPrevilege(UserPrevilege User, string UserId, GridView grPriv, string strInsBy, string strInsDate)
    //{
    //    SqlCommand[] cmd;
    //    cmd = new SqlCommand[grPriv.Rows.Count + 1];

    //    //DELETE FROM DETAILS TABLE        
    //    cmd[0] = new SqlCommand("proc_Delete_UserPrivs");
    //    cmd[0].CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_USERID = cmd[0].Parameters.Add("USERID", SqlDbType.Char);
    //    p_USERID.Direction = ParameterDirection.Input;
    //    p_USERID.Value = UserId;

    //    int i = 1;

    //    foreach (GridViewRow tt in grPriv.Rows)
    //    {
    //        Boolean chkAll = Convert.ToBoolean(((CheckBox)tt.Cells[1].FindControl("chkAll")).Checked);
    //        string All = "";
    //        if (chkAll == true)
    //            All = "Y";
    //        else
    //            All = "N";

    //        string pagviewid = Convert.ToString(grPriv.DataKeys[tt.RowIndex].Value);
    //        cmd[i] = InsertUserPrevDetails(User, tt, UserId, pagviewid, All, strInsBy, strInsDate);
    //        i++;
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
    public SqlCommand InsertUserPrevDetails(string UserId, string ScreenId, string A, string strInsBy, string strInsDate)
    {
        //INSERT INTO DETAILS TABLE
        SqlCommand cmd = new SqlCommand("proc_INSERT_UserPrivs");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_USERID = cmd.Parameters.Add("USERID", SqlDbType.Char);
        p_USERID.Direction = ParameterDirection.Input;
        p_USERID.Value = UserId;

        SqlParameter p_ScreenId = cmd.Parameters.Add("Screen_Id", SqlDbType.Char);
        p_ScreenId.Direction = ParameterDirection.Input;
        p_ScreenId.Value = ScreenId;

        SqlParameter p_A = cmd.Parameters.Add("A", SqlDbType.Char);
        p_A.Direction = ParameterDirection.Input;
        p_A.Value = A;

        SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        return cmd;
    }

    public void InsertPrivPackage(GridView grPriv,string strID,string strName, string strInsBy, string strInsDate)
    {
        SqlCommand[] cmd;
        cmd = new SqlCommand[grPriv.Rows.Count + 2];
        int i = 0;
        int j = 0;
        //DELETE FROM DETAILS TABLE        
        cmd[i] = new SqlCommand("proc_Delete_PrivPack");
        cmd[i].CommandType = CommandType.StoredProcedure;
        SqlParameter pm_PrivPackID = cmd[i].Parameters.Add("PrivPackID", SqlDbType.Int);
        pm_PrivPackID.Direction = ParameterDirection.Input;
        pm_PrivPackID.Value = Convert.ToInt32(strID);
        i++;
        foreach (GridViewRow tt in grPriv.Rows)
        {
            CheckBox chk = (CheckBox)tt.Cells[1].FindControl("chkAll");

            if (chk.Checked == true)
            {
                //INSERT INTO DETAILS TABLE
                cmd[i] = new SqlCommand("proc_INSERT_PrivPack");
                cmd[i].CommandType = CommandType.StoredProcedure;

                SqlParameter p_PrivPackID = cmd[i].Parameters.Add("PrivPackID", SqlDbType.Int);
                p_PrivPackID.Direction = ParameterDirection.Input;
                p_PrivPackID.Value = Convert.ToInt32(strID);

                SqlParameter p_PrivPackName = cmd[i].Parameters.Add("PrivPackName", SqlDbType.VarChar);
                p_PrivPackName.Direction = ParameterDirection.Input;
                p_PrivPackName.Value = strName;

                SqlParameter p_ViewID = cmd[i].Parameters.Add("ViewID", SqlDbType.Int);
                p_ViewID.Direction = ParameterDirection.Input;
                p_ViewID.Value = Convert.ToInt32(grPriv.DataKeys[j].Values[0].ToString());

                SqlParameter p_InsertedBy = cmd[i].Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
                p_InsertedBy.Direction = ParameterDirection.Input;
                p_InsertedBy.Value = strInsBy;

                SqlParameter p_InsertedDate = cmd[i].Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
                p_InsertedDate.Direction = ParameterDirection.Input;
                p_InsertedDate.Value = strInsDate;
            }
            i++;
            j++;
        }


        objDC.MakeTransaction(cmd);
    }

    public DataTable GetPrivPackWiseUser(string strPrivPackID)
    {
        string strSQL = "SELECT DISTINCT(USERID) FROM USERINFO WHERE PrivPackID=@PrivPackID";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_PrivPackID = cmd.Parameters.Add("PrivPackID", SqlDbType.Char);
        p_PrivPackID.Direction = ParameterDirection.Input;
        p_PrivPackID.Value = strPrivPackID;

        return objDC.CreateDT(cmd, "PrivPackWiseUser");
    }

    public void SynchronizeUserPrivelege(DataTable dtUser,string strPrivPackID)
    {
        int i = 0;
        try
        {
            DataTable dtPrivs = this.SelectPrivPack(Convert.ToInt32(strPrivPackID));
            SqlCommand[] command = new SqlCommand[dtUser.Rows.Count * dtPrivs.Rows.Count + dtUser.Rows.Count];
            if (dtPrivs.Rows.Count > 0)
            {
                foreach (DataRow dRow in dtUser.Rows)
                {
                    // Delete the user privs data

                    command[i] = new SqlCommand();
                    command[i] = this.DeleteUserWisePrivsData(dRow["USERID"].ToString().Trim());
                    i++;

                    // Insert data
                    foreach (DataRow dPRow in dtPrivs.Rows)
                    {
                        command[i] = new SqlCommand();
                        command[i] = this.InsertUserWisePrivsData(dRow["USERID"].ToString().Trim(), dPRow["VIEWID"].ToString().Trim());
                        i++;
                    }
                }
            }
            objDC.MakeTransaction(command);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }

    public SqlCommand DeleteUserWisePrivsData(string strUserId)
    {
        string strSQLDelete = "";
        strSQLDelete = "DELETE FROM USERPRIVS WHERE USERID=@USERID";
        SqlCommand  cmd = new SqlCommand(strSQLDelete);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_USERID = cmd.Parameters.Add("USERID", SqlDbType.Char);
        p_USERID.Direction = ParameterDirection.Input;
        p_USERID.Value = strUserId;

        return cmd;
    }

    public SqlCommand InsertUserWisePrivsData(string strUserId,string strScrID)
    {
        string strSQL = "";
        strSQL = "INSERT INTO USERPRIVS(USERID,VIEWID,A) VALUES(@USERID,@VIEWID,@A)";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_USERID = cmd.Parameters.Add("USERID", SqlDbType.Char);
        p_USERID.Direction = ParameterDirection.Input;
        p_USERID.Value = strUserId;

        SqlParameter p_SCREEN_ID = cmd.Parameters.Add("VIEWID", SqlDbType.BigInt);
        p_SCREEN_ID.Direction = ParameterDirection.Input;
        p_SCREEN_ID.Value = strScrID;

        SqlParameter p_A = cmd.Parameters.Add("A", SqlDbType.Char);
        p_A.Direction = ParameterDirection.Input;
        p_A.Value = "Y";

        return cmd;
    }
    
    public void UpdatePassword(UserCreation User)
    {
        // sproc functionality
        SqlCommand command = new SqlCommand("proc_UPDATE_Password");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_USERID = command.Parameters.Add("USERID", SqlDbType.Char);
        p_USERID.Direction = ParameterDirection.Input;
        p_USERID.Value = User.USERID;

        SqlParameter p_PASSWORD = command.Parameters.Add("PASSWORD", SqlDbType.Char);
        p_PASSWORD.Direction = ParameterDirection.Input;
        p_PASSWORD.Value = User.PASSWORD;

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

   
    #endregion 

    #region Select Queries From Tables By store procedure
    //*Sulata
    public DataTable SelectUserInfo(string UserId, string AccountDisabled)
    {
        SqlCommand command = new SqlCommand("proc_Select_User");

        SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
        p_UserId.Direction = ParameterDirection.Input;
        p_UserId.Value = UserId;

        SqlParameter p_AccountDisabled = command.Parameters.Add("AccountDisabled", SqlDbType.Char);
        p_AccountDisabled.Direction = ParameterDirection.Input;
        p_AccountDisabled.Value = AccountDisabled;

        objDC.CreateDSFromProc(command, "userInfo");
        return objDC.ds.Tables["userInfo"];
    }

    public DataTable SelectPrivPack(int intID)
    {
        SqlCommand command = new SqlCommand("proc_Select_PrivPackage");
        SqlParameter p_PrivPackID = command.Parameters.Add("PrivPackID", SqlDbType.Int);
        p_PrivPackID.Direction = ParameterDirection.Input;
        p_PrivPackID.Value = intID;

        objDC.CreateDSFromProc(command, "PrivPack");
        return objDC.ds.Tables["PrivPack"];
    }

    public DataTable SelectUserPriv(string UserId)
    {
        SqlCommand command = new SqlCommand("proc_Select_UserPrivs");
        SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
        p_UserId.Direction = ParameterDirection.Input;
        p_UserId.Value = UserId;

        objDC.CreateDSFromProc(command, "UserPrivs");
        return objDC.ds.Tables["UserPrivs"];
    }
    public DataTable SelectScreenInfo()
    {
        SqlCommand command = new SqlCommand("proc_SELECT_Screen");
        objDC.CreateDSFromProc(command, "screeninfo");
        return objDC.ds.Tables["screeninfo"];
    }

    public DataTable SelectUserAssignment(string UserId, Int32 DivisionId, Int32 SBUId)
    {
        SqlCommand command = new SqlCommand("proc_Select_UserAssignment");
        SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
        p_UserId.Direction = ParameterDirection.Input;
        p_UserId.Value = UserId;

        SqlParameter p_DivisionId = command.Parameters.Add("DivisionId", SqlDbType.BigInt);
        p_DivisionId.Direction = ParameterDirection.Input;
        p_DivisionId.Value = DivisionId;

        SqlParameter p_SBUId = command.Parameters.Add("SBUId", SqlDbType.BigInt);
        p_SBUId.Direction = ParameterDirection.Input;
        p_SBUId.Value = SBUId;

        objDC.CreateDSFromProc(command, "UserAssignment");
        return objDC.ds.Tables["UserAssignment"];
    }

    //Insert into user in out date time log history table
    public void InsertUserInOutHistory(string strLogInId, string strUserId, string strLogInDate, string strLogOutDate, string strStatus,string strIsUpdate)
    {
        SqlCommand command = new SqlCommand("proc_INSERT_UserInOutHistory");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LOGINID = command.Parameters.Add("LogInId", SqlDbType.BigInt);
        p_LOGINID.Direction = ParameterDirection.Input;
        p_LOGINID.Value = strLogInId;

        SqlParameter p_USERID = command.Parameters.Add("UserId", SqlDbType.Char);
        p_USERID.Direction = ParameterDirection.Input;
        p_USERID.Value = strUserId;

        SqlParameter p_InOutDate = command.Parameters.Add("LogInDate", SqlDbType.Char);
        p_InOutDate.Direction = ParameterDirection.Input;
        p_InOutDate.Value = strLogInDate;

        SqlParameter p_LogOutDate = command.Parameters.Add("LogOutDate", SqlDbType.Char);
        p_LogOutDate.Direction = ParameterDirection.Input;
        p_LogOutDate.Value = strLogOutDate;

        SqlParameter p_Status = command.Parameters.Add("Status", SqlDbType.Char);
        p_Status.Direction = ParameterDirection.Input;
        p_Status.Value = strStatus;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = strIsUpdate;

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

    public DataTable GetUserTaskPermission(string sUserId,string sViewId,string sTaskId)
    {
        string strSQL="";
        SqlCommand cmd = new SqlCommand();
        if (objDC.ds.Tables["UserTaskPermission"] != null)
        {
            objDC.ds.Tables["UserTaskPermission"].Rows.Clear();
            objDC.ds.Tables["UserTaskPermission"].Dispose();
        }
        if (sUserId != "" && sViewId != "" && sTaskId != "")
            strSQL = "SELECT * FROM UserTaskPermission WHERE UserId='" + @sUserId + "' AND ViewId=" + sViewId
               + " AND IsAuthorize='Y' AND TaskId='" + sTaskId + "'";
        else
            strSQL = "SELECT * FROM UserTaskPermission";

        cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_UserId = cmd.Parameters.Add("UserId", SqlDbType.Char);
        p_UserId.Direction = ParameterDirection.Input;
        p_UserId.Value = sUserId;

        SqlParameter p_ViewId = cmd.Parameters.Add("ViewId", SqlDbType.BigInt);
        p_ViewId.Direction = ParameterDirection.Input;
        p_ViewId.Value = sViewId;

        SqlParameter p_TaskId = cmd.Parameters.Add("TaskId", SqlDbType.VarChar);
        p_TaskId.Direction = ParameterDirection.Input;
        p_TaskId.Value = sTaskId;

        return objDC.CreateDT(cmd, "UserTaskPermission");
    }

    public DataTable SelectTaskPermission()
    {
        string strSQL = "SELECT DISTINCT TaskDesc FROM UserTaskPermission";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
       
        return objDC.CreateDT(cmd, "TaskPermission");
    }
    #endregion
    public UserManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
