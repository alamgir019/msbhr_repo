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
/// Summary description for SupervisorMovementManager
/// </summary>
public class SupervisorMovementManager
{
    DBConnector objDB = new DBConnector();

    public DataTable GetMovementData()
    {
        string strSQL = "SELECT b.FullName + ' [' + a.FromSPVID + ']' as FromSupervisor,c.FullName + ' [' + a.ToSPVID + ']' as ToSupervisor,a.Status,a.FromSPVID, "
                    + " a.ToSPVID,a.TransID,a.FromDate,a.ToDate,IsParmanent,Remarks "
                    + " FROM SupervisorMovementHistory a,EmpInfo b,EmpInfo c WHERE a.FromSPVID=b.EmpID AND a.ToSPVID=c.EmpID AND IsParmanent='N'";
        return objDB.CreateDT(strSQL, "SupervisorMovementHistory");
    }

    public DataTable GetDetailsData(string strTransID)
    {
        string strSQL = "SELECT *  "
                    + " FROM SupervisorMovementHistoryDetls WHERE TransID=@TransID";
        SqlCommand cmd = new SqlCommand(strSQL);

        SqlParameter p_TransID = cmd.Parameters.Add("TransID", SqlDbType.BigInt);
        p_TransID.Direction = ParameterDirection.Input;
        p_TransID.Value = strTransID;

        return objDB.CreateDT(cmd, "SupervisorMovementHistoryDetls");
    }

    public void InsertData(string strFromID, string strToID, string strStatus,string strInsBy,string strInsDate,string strEmpArray,
        string strRefTransID,string strFromDate,string strToDate,string strIsParmanent,string strRemarks, string strIsRollBack,string strTransID)
    {
        string strSQL="";
        string[] strArray = strEmpArray.Split(',');
        
        if (strRefTransID == "")
            strSQL = "INSERT INTO SupervisorMovementHistory(TransID,FromSPVID,ToSPVID,Status,InsertedBy,InsertedDate,FromDate,ToDate,IsParmanent,Remarks) "
                        + " VALUES(@TransID,@FromSPVID,@ToSPVID,@Status,@InsertedBy,@InsertedDate,@FromDate,@ToDate,@IsParmanent,@Remarks)";
        else
            strSQL = "UPDATE SupervisorMovementHistory SET UpdatedBy=@UpdatedBy,UpdatedDate=@UpdatedDate,FromDate=@FromDate,ToDate=@ToDate,Remarks=@Remarks "
                        + " WHERE TransID=@TransID ";
        SqlCommand[] command = new SqlCommand[strArray.Length + 3];

        command[0] = new SqlCommand(strSQL);
        command[0].CommandType = CommandType.Text;
       
            SqlParameter p_TransID = command[0].Parameters.Add("TransID", SqlDbType.BigInt);
            p_TransID.Direction = ParameterDirection.Input;
            if (strRefTransID == "")
                p_TransID.Value = strTransID;
            else
                p_TransID.Value = strRefTransID;


            if (strRefTransID == "")
            {
                SqlParameter p_FromSPVID = command[0].Parameters.Add("FromSPVID", SqlDbType.Char);
                p_FromSPVID.Direction = ParameterDirection.Input;
                p_FromSPVID.Value = strFromID;

                SqlParameter p_ToSPVID = command[0].Parameters.Add("ToSPVID", SqlDbType.Char);
                p_ToSPVID.Direction = ParameterDirection.Input;
                p_ToSPVID.Value = strToID;

                SqlParameter p_Status = command[0].Parameters.Add("Status", SqlDbType.BigInt);
                p_Status.Direction = ParameterDirection.Input;
                if (strIsParmanent == "N")
                    p_Status.Value = strStatus;
                else
                    p_Status.Value = "1";

                SqlParameter p_InsertedBy = command[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                p_InsertedBy.Direction = ParameterDirection.Input;
                p_InsertedBy.Value = strInsBy;

                SqlParameter p_InsertedDate = command[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                p_InsertedDate.Direction = ParameterDirection.Input;
                p_InsertedDate.Value = strInsDate;

                SqlParameter p_IsParmanent = command[0].Parameters.Add("IsParmanent", SqlDbType.Char);
                p_IsParmanent.Direction = ParameterDirection.Input;
                p_IsParmanent.Value = strIsParmanent;
            }
            else
            {
                SqlParameter p_UpdatedBy = command[0].Parameters.Add("UpdatedBy", SqlDbType.VarChar);
                p_UpdatedBy.Direction = ParameterDirection.Input;
                p_UpdatedBy.Value = strInsBy;

                SqlParameter p_UpdatedDate = command[0].Parameters.Add("UpdatedDate", SqlDbType.DateTime);
                p_UpdatedDate.Direction = ParameterDirection.Input;
                p_UpdatedDate.Value = strInsDate;
            }

        SqlParameter p_FromDate = command[0].Parameters.Add("FromDate", DBNull.Value);
        p_FromDate.Direction = ParameterDirection.Input;
        p_FromDate.IsNullable = true;
        if (string.IsNullOrEmpty(strFromDate) == false)
            p_FromDate.Value = Common.ReturnDate(strFromDate);

        SqlParameter p_ToDate = command[0].Parameters.Add("ToDate", DBNull.Value);
        p_ToDate.Direction = ParameterDirection.Input;
        p_ToDate.IsNullable = true;
        if (string.IsNullOrEmpty(strToDate)==false)
            p_ToDate.Value = Common.ReturnDate(strToDate);

        SqlParameter p_Remarks = command[0].Parameters.Add("Remarks", SqlDbType.VarChar);
        p_Remarks.Direction = ParameterDirection.Input;
        p_Remarks.Value = strRemarks;

        string strSQL2 = "";

        // Update Empinfo Table Supervisor
        //if (strStatus == "0")
        //{
            
        //    strSQL2 = "UPDATE EMPINFO SET REPORTINGTO=@REPORTINGTO1 WHERE REPORTINGTO=@REPORTINGTO2";


        //    command[1] = new SqlCommand(strSQL2);
        //    command[1].CommandType = CommandType.Text;

        //    SqlParameter p_REPORTINGTO1 = command[1].Parameters.Add("REPORTINGTO1", SqlDbType.Char);
        //    p_REPORTINGTO1.Direction = ParameterDirection.Input;
        //    p_REPORTINGTO1.Value = strToID;

        //    SqlParameter p_REPORTINGTO2 = command[1].Parameters.Add("REPORTINGTO2", SqlDbType.Char);
        //    p_REPORTINGTO2.Direction = ParameterDirection.Input;
        //    p_REPORTINGTO2.Value = strFromID;
        //}
        //else
        //{
            
            string strEmp = "";
            foreach (string str in strArray)
            {
                if (string.IsNullOrEmpty(str) == false)
                {
                    if (strEmp == "")
                    {
                        strEmp = "'" + str + "'";
                    }
                    else
                    {
                        strEmp = strEmp + ",'" + str + "'";
                    }
                }
             }

            strSQL2 = "UPDATE EMPINFO SET REPORTINGTO=@REPORTINGTO1 WHERE EMPID IN("+ strEmp +")";
            command[1] = new SqlCommand(strSQL2);
            command[1].CommandType = CommandType.Text;
            
            SqlParameter p_REPORTINGTO1 = command[1].Parameters.Add("REPORTINGTO1", SqlDbType.Char);
            p_REPORTINGTO1.Direction = ParameterDirection.Input;
            p_REPORTINGTO1.Value = strToID;
       // }
        int i = 2;
        // If Old Record found
        if (strIsRollBack == "Y")
        {
            command[i] = new SqlCommand("UPDATE SupervisorMovementHistory SET STATUS=1 WHERE TRANSID=@TRANSID");
            SqlParameter p_REFTRANSID = command[i].Parameters.Add("TRANSID", SqlDbType.BigInt);
            p_REFTRANSID.Direction = ParameterDirection.Input;
            p_REFTRANSID.Value = strRefTransID;
            i++;
        }
        // Insert Details Data
            foreach (string str in strArray)
            {
                if (string.IsNullOrEmpty(str) == false)
                {
                    if (strIsRollBack == "N")
                    {
                        if (strRefTransID == "")
                            command[i] = InsertDetailsData(strTransID, str);
                        else
                            command[i] = InsertDetailsData(strRefTransID, str);
                    }
                    else if(strIsRollBack == "P")
                    {
                        if (strRefTransID != "")
                            command[i] = DeleteDetailsData(strRefTransID, str);
                    }
                    i++;
                }
            }
       

        objDB.MakeTransaction(command);

    }

    private SqlCommand InsertDetailsData(string strTransID, string strEmpID)
    {
        string strSQL = "INSERT INTO SupervisorMovementHistoryDetls(TransID,EmpID) VALUES(@TransID,@EmpID)";
        SqlCommand cmd = new SqlCommand(strSQL);
        SqlParameter p_TransID = cmd.Parameters.Add("TransID", SqlDbType.BigInt);
        p_TransID.Direction = ParameterDirection.Input;
        p_TransID.Value = strTransID;

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        return cmd;
    }

    private SqlCommand DeleteDetailsData(string strTransID, string strEmpID)
    {
        string strSQL = "DELETE FROM SupervisorMovementHistoryDetls WHERE TransID=@TransID AND EmpID=@EmpID ";
        SqlCommand cmd = new SqlCommand(strSQL);
        SqlParameter p_TransID = cmd.Parameters.Add("TransID", SqlDbType.BigInt);
        p_TransID.Direction = ParameterDirection.Input;
        p_TransID.Value = strTransID;

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        return cmd;
    }


    public DataTable GetMasterData(string strFromID, string strToID)
    {
        string strSQL = "SELECT * FROM SupervisorMovementHistory WHERE FromSPVID=@FromSPVID AND ToSPVID=@ToSPVID AND Status=0";
        SqlCommand cmd = new SqlCommand(strSQL);

        SqlParameter p_FromSPVID = cmd.Parameters.Add("FromSPVID", SqlDbType.Char);
        p_FromSPVID.Direction = ParameterDirection.Input;
        p_FromSPVID.Value = strToID;

        SqlParameter p_ToSPVID = cmd.Parameters.Add("ToSPVID", SqlDbType.Char);
        p_ToSPVID.Direction = ParameterDirection.Input;
        p_ToSPVID.Value = strFromID;

        return objDB.CreateDT(cmd, "GetMasterData");
    }
	public SupervisorMovementManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
