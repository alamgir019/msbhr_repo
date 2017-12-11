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
/// Summary description for EmailTaskAlertMgr
/// </summary>
public class TaskAlertMgr
{
    DBConnector objDC = new DBConnector();
    #region Insert Update Delete From Tables By Store procedure

    public void InsertEmailTaskAlert(ETaskAlert ETA, string IsUpdate, string IsDelete,string strSlNo)
    {
        long intSLNo = Convert.ToInt64(Common.getMaxId("EMAILTASKALERT", "SLNO"));
        
        SqlCommand cmd = new SqlCommand("proc_Insert_EmailTaskAlert");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_TRANSID = cmd.Parameters.Add("TRANSID", SqlDbType.BigInt);
        p_TRANSID.Direction = ParameterDirection.Input;
        p_TRANSID.Value = ETA.TRANSID;

        SqlParameter p_SLNO = cmd.Parameters.Add("SLNO", SqlDbType.BigInt);
        p_SLNO.Direction = ParameterDirection.Input;
        if(IsUpdate=="Y")
            p_SLNO.Value = strSlNo;
        else
            p_SLNO.Value = intSLNo;

        SqlParameter p_FROMEMPID = cmd.Parameters.Add("FROMEMPID", SqlDbType.Char);
        p_FROMEMPID.Direction = ParameterDirection.Input;
        p_FROMEMPID.Value = ETA.FROMEMPID;

        SqlParameter p_TOEMPID = cmd.Parameters.Add("TOEMPID", SqlDbType.VarChar);
        p_TOEMPID.Direction = ParameterDirection.Input;
        p_TOEMPID.Value = ETA.TOEMPID;

        SqlParameter p_FROMADDR = cmd.Parameters.Add("FROMADDR", SqlDbType.VarChar);
        p_FROMADDR.Direction = ParameterDirection.Input;
        p_FROMADDR.Value = ETA.FROMADDR;

        SqlParameter p_TOADDR = cmd.Parameters.Add("TOADDR", SqlDbType.VarChar);
        p_TOADDR.Direction = ParameterDirection.Input;
        p_TOADDR.Value = ETA.TOADDR;

        SqlParameter p_CCADDR = cmd.Parameters.Add("CCADDR", SqlDbType.VarChar);
        p_CCADDR.Direction = ParameterDirection.Input;
        p_CCADDR.Value = ETA.CCADDR;

        SqlParameter p_BCCADDR = cmd.Parameters.Add("BCCADDR", SqlDbType.VarChar);
        p_BCCADDR.Direction = ParameterDirection.Input;
        p_BCCADDR.Value = ETA.BCCADDR;

        SqlParameter p_SUBJECT = cmd.Parameters.Add("SUBJECT", SqlDbType.VarChar);
        p_SUBJECT.Direction = ParameterDirection.Input;
        p_SUBJECT.Value = ETA.SUBJECT;

        SqlParameter p_ATTACHMENT = cmd.Parameters.Add("ATTACHMENT", SqlDbType.VarChar);
        p_ATTACHMENT.Direction = ParameterDirection.Input;
        p_ATTACHMENT.Value = ETA.ATTACHMENT;

        SqlParameter p_BODY = cmd.Parameters.Add("BODY", SqlDbType.VarChar);
        p_BODY.Direction = ParameterDirection.Input;
        p_BODY.Value = ETA.BODY;

        SqlParameter p_SCHDATETIME = cmd.Parameters.Add("SCHDATETIME", DBNull.Value);
        p_SCHDATETIME.Direction = ParameterDirection.Input;
        p_SCHDATETIME.IsNullable = true;
        if (ETA.SCHDATETIME != "")
            p_SCHDATETIME.Value = ETA.SCHDATETIME;

        SqlParameter p_STATUS = cmd.Parameters.Add("STATUS", SqlDbType.Char);
        p_STATUS.Direction = ParameterDirection.Input;
        p_STATUS.Value = ETA.STATUS;

        SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = ETA.InsertedBy;

        SqlParameter p_InsertedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = ETA.InsertedDate;

        SqlParameter p_IsUpdate = cmd.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = cmd.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

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

    // Insert Group Email Task Alert
    public void InsertGroupEmailTaskAlert(ETaskAlert[] ETA, string IsUpdate, string IsDelete)
    {

        SqlCommand[] cmd=new SqlCommand[ETA.Length];
        long intSLNo = Convert.ToInt64(Common.getMaxId("EMAILTASKALERT","SLNO"));
       // long lnTransID=Convert.ToInt64(ETA[i].TRANSID);

        for (int i = 0; i < ETA.Length; i++)
        {
            cmd[i] = new SqlCommand("proc_Insert_EmailTaskAlert");
            cmd[i].CommandType = CommandType.StoredProcedure;

            SqlParameter p_TRANSID = cmd[i].Parameters.Add("TRANSID", SqlDbType.BigInt);
            p_TRANSID.Direction = ParameterDirection.Input;
            p_TRANSID.Value = ETA[i].TRANSID;

            SqlParameter p_SLNO = cmd[i].Parameters.Add("SLNO", SqlDbType.BigInt);
            p_SLNO.Direction = ParameterDirection.Input;
            p_SLNO.Value = intSLNo;

            SqlParameter p_FROMEMPID = cmd[i].Parameters.Add("FROMEMPID", SqlDbType.Char);
            p_FROMEMPID.Direction = ParameterDirection.Input;
            p_FROMEMPID.Value = ETA[i].FROMEMPID;

            SqlParameter p_TOEMPID = cmd[i].Parameters.Add("TOEMPID", SqlDbType.VarChar);
            p_TOEMPID.Direction = ParameterDirection.Input;
            p_TOEMPID.Value = ETA[i].TOEMPID;

            SqlParameter p_FROMADDR = cmd[i].Parameters.Add("FROMADDR", SqlDbType.VarChar);
            p_FROMADDR.Direction = ParameterDirection.Input;
            p_FROMADDR.Value = ETA[i].FROMADDR;

            SqlParameter p_TOADDR = cmd[i].Parameters.Add("TOADDR", SqlDbType.VarChar);
            p_TOADDR.Direction = ParameterDirection.Input;
            p_TOADDR.Value = ETA[i].TOADDR;

            SqlParameter p_CCADDR = cmd[i].Parameters.Add("CCADDR", SqlDbType.VarChar);
            p_CCADDR.Direction = ParameterDirection.Input;
            p_CCADDR.Value = ETA[i].CCADDR;

            SqlParameter p_BCCADDR = cmd[i].Parameters.Add("BCCADDR", SqlDbType.VarChar);
            p_BCCADDR.Direction = ParameterDirection.Input;
            p_BCCADDR.Value = ETA[i].BCCADDR;

            SqlParameter p_SUBJECT = cmd[i].Parameters.Add("SUBJECT", SqlDbType.VarChar);
            p_SUBJECT.Direction = ParameterDirection.Input;
            p_SUBJECT.Value = ETA[i].SUBJECT;

            SqlParameter p_ATTACHMENT = cmd[i].Parameters.Add("ATTACHMENT", SqlDbType.VarChar);
            p_ATTACHMENT.Direction = ParameterDirection.Input;
            p_ATTACHMENT.Value = ETA[i].ATTACHMENT;

            SqlParameter p_BODY = cmd[i].Parameters.Add("BODY", SqlDbType.VarChar);
            p_BODY.Direction = ParameterDirection.Input;
            p_BODY.Value = ETA[i].BODY;

            SqlParameter p_SCHDATETIME = cmd[i].Parameters.Add("SCHDATETIME", DBNull.Value);
            p_SCHDATETIME.Direction = ParameterDirection.Input;
            p_SCHDATETIME.IsNullable = true;
            if (ETA[i].SCHDATETIME != "")
                p_SCHDATETIME.Value = ETA[i].SCHDATETIME;

            SqlParameter p_STATUS = cmd[i].Parameters.Add("STATUS", SqlDbType.Char);
            p_STATUS.Direction = ParameterDirection.Input;
            p_STATUS.Value = ETA[i].STATUS;

            SqlParameter p_InsertedBy = cmd[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = ETA[i].InsertedBy;

            SqlParameter p_InsertedDate = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate.Direction = ParameterDirection.Input;
            p_InsertedDate.Value = ETA[i].InsertedDate;

            SqlParameter p_IsUpdate = cmd[i].Parameters.Add("IsUpdate", SqlDbType.Char);
            p_IsUpdate.Direction = ParameterDirection.Input;
            p_IsUpdate.Value = IsUpdate;

            SqlParameter p_IsDelete = cmd[i].Parameters.Add("IsDelete", SqlDbType.Char);
            p_IsDelete.Direction = ParameterDirection.Input;
            p_IsDelete.Value = IsDelete;
            //i++;

            //cmd[i]= this.GetTaskAlertCommand(lnTransID.ToString(),ETA[i].SUBJECT,ETA[i].SCHDATETIME,
            //    ETA[i].SCHDATETIME,ETA[i].FROMADDR,"0",ETA[i].InsertedBy,ETA[i].InsertedDate,"N");

            intSLNo++;
            //lnTransID++;
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
    //Insert Task Alert
    public void InsertTaskAlert(string strTransId,string strTitle,string strReminderDate,string strCompletedDate,
        string strEmpId,string strStatus,string strInsBy,string strInsDate, string strIsUpdate)
    {
        SqlCommand cmd = new SqlCommand("proc_Insert_TaskAlert");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_TransId = cmd.Parameters.Add("TransId", SqlDbType.BigInt);
        p_TransId.Direction = ParameterDirection.Input;
        p_TransId.Value = strTransId;

        SqlParameter p_TaskTitle = cmd.Parameters.Add("TaskTitle", SqlDbType.VarChar);
        p_TaskTitle.Direction = ParameterDirection.Input;
        p_TaskTitle.Value = strTitle;

        SqlParameter p_ReminderDate = cmd.Parameters.Add("ReminderDate", DBNull.Value);
        p_ReminderDate.Direction = ParameterDirection.Input;
        p_ReminderDate.IsNullable = true;
        if (strReminderDate != "")
            p_ReminderDate.Value = strReminderDate;

        SqlParameter p_CompletedDate = cmd.Parameters.Add("CompletedDate", DBNull.Value);
        p_CompletedDate.Direction = ParameterDirection.Input;
        p_CompletedDate.IsNullable = true;
        if (strCompletedDate != "")
            p_CompletedDate.Value = strCompletedDate;

        SqlParameter p_ToEmpId = cmd.Parameters.Add("ToEmpId", SqlDbType.Char);
        p_ToEmpId.Direction = ParameterDirection.Input;
        p_ToEmpId.Value = strEmpId;

        SqlParameter p_Status = cmd.Parameters.Add("Status", SqlDbType.Char);
        p_Status.Direction = ParameterDirection.Input;
        p_Status.Value = strStatus;

        SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        SqlParameter p_IsUpdate = cmd.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = strIsUpdate;


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

    //Insert Task Alert
    public SqlCommand GetTaskAlertCommand(string strTransId, string strTitle, string strReminderDate, string strCompletedDate,
        string strEmpId, string strStatus, string strInsBy, string strInsDate, string strIsUpdate)
    {
        SqlCommand cmd = new SqlCommand("proc_Insert_TaskAlert");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_TransId = cmd.Parameters.Add("TransId", SqlDbType.BigInt);
        p_TransId.Direction = ParameterDirection.Input;
        p_TransId.Value = strTransId;

        SqlParameter p_TaskTitle = cmd.Parameters.Add("TaskTitle", SqlDbType.VarChar);
        p_TaskTitle.Direction = ParameterDirection.Input;
        p_TaskTitle.Value = strTitle;

        SqlParameter p_ReminderDate = cmd.Parameters.Add("ReminderDate", DBNull.Value);
        p_ReminderDate.Direction = ParameterDirection.Input;
        p_ReminderDate.IsNullable = true;
        if (strReminderDate != "")
            p_ReminderDate.Value = strReminderDate;

        SqlParameter p_CompletedDate = cmd.Parameters.Add("CompletedDate", DBNull.Value);
        p_CompletedDate.Direction = ParameterDirection.Input;
        p_CompletedDate.IsNullable = true;
        if (strCompletedDate != "")
            p_CompletedDate.Value = strCompletedDate;

        SqlParameter p_ToEmpId = cmd.Parameters.Add("ToEmpId", SqlDbType.Char);
        p_ToEmpId.Direction = ParameterDirection.Input;
        p_ToEmpId.Value = strEmpId;

        SqlParameter p_Status = cmd.Parameters.Add("Status", SqlDbType.Char);
        p_Status.Direction = ParameterDirection.Input;
        p_Status.Value = strStatus;

        SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        SqlParameter p_IsUpdate = cmd.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = strIsUpdate;


        return cmd;
    }
    #endregion
    #region Select Queries From Tables By store procedure

    //Select Email task alert
    public DataTable SelectEmailTaskAlert(Int64 TRANSID,string strSTATUS,string strListStatus)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmailTaskAlert");
        SqlParameter p_TRANSID = command.Parameters.Add("TRANSID", SqlDbType.BigInt);
        p_TRANSID.Direction = ParameterDirection.Input;
        p_TRANSID.Value = TRANSID;

        SqlParameter p_STATUS = command.Parameters.Add("STATUS", SqlDbType.Char);
        p_STATUS.Direction = ParameterDirection.Input;
        p_STATUS.Value = strSTATUS;

        if (strListStatus == "P")
        {
            objDC.CreateDSFromProc(command, "EmailPendingAlert");
            return objDC.ds.Tables["EmailPendingAlert"];
        }
        else
        {
            objDC.CreateDSFromProc(command, "EmailCompletedAlert");
            return objDC.ds.Tables["EmailCompletedAlert"];
        }
    }
    //Select task alert
    public DataTable SelectTaskAlert(Int64 TRANSID,string Status)
    {
        SqlCommand command = new SqlCommand("proc_Select_TaskAlert");
        SqlParameter p_TRANSID = command.Parameters.Add("TRANSID", SqlDbType.BigInt);
        p_TRANSID.Direction = ParameterDirection.Input;
        p_TRANSID.Value = TRANSID;

        SqlParameter p_Status = command.Parameters.Add("Status", SqlDbType.Char);
        p_Status.Direction = ParameterDirection.Input;
        p_Status.Value = Status;

        objDC.CreateDSFromProc(command, "TaskAlert");
        return objDC.ds.Tables["TaskAlert"];
        
    }

    public string GetTaskAlert(string strSLNO)
    {
        string strRetValue = "";
        string strSQL = "SELECT SLNO FROM EmailTaskAlert WHERE SLNO=@SLNO";
        SqlCommand command = new SqlCommand(strSQL);

        SqlParameter p_SLNO = command.Parameters.Add("SLNO", SqlDbType.BigInt);
        p_SLNO.Direction = ParameterDirection.Input;
        p_SLNO.Value = strSLNO;

        strRetValue = objDC.GetScalarVal(command);
        if (string.IsNullOrEmpty(strRetValue) == false)
            return "Y";
        else
            return "N";
    }
    #endregion
}
