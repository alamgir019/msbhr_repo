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
/// Summary description for AttnManager
/// </summary>
public class AttnManager
{
    // Device Database Connectionstring
    private string strConnection = ConfigurationManager.ConnectionStrings["dbattnconn"].ConnectionString;// works fine
    private SqlConnection sqlConn;
    // SqlCommand command;
    SqlDataAdapter adapter = new SqlDataAdapter();
    DataSet ds = new DataSet();
    DBConnector objDC = new DBConnector();

    // Read Attendance Record from Device database
    public DataTable GetReaderData(string strAttDateFrom, string strAttDateTo)
    {
        string strSQL = " SELECT USERNAME AS EMPID,CONVERT(VARCHAR(10),LOGINTIME,111) AS ATTNDDATE,  LOGINTIME AS SIGNINTIME, LOGOUTTIME AS SIGNOUTTIME "
                    + " FROM LoginLogout "
                    + " WHERE LOGINTIME BETWEEN  @STARTDATE AND @ENDDATE"
                    + " ORDER BY ATTNDDATE,EMPID";
        //string strSQL = "SELECT * FROM PLAN_ATTN_RECORD WHERE SIGNINTIME BETWEEN  @STARTDATE AND @ENDDATE ORDER BY ATTNDDATE,EMPID";
        // StartDate: '2006-06-04 00:00:00.000'
        // EndDate: '2006-06-04 23:59:00.000'

        SqlCommand cmd = new SqlCommand(strSQL);

        SqlParameter p_STARTDATE = cmd.Parameters.Add("STARTDATE", SqlDbType.DateTime);
        p_STARTDATE.Direction = ParameterDirection.Input;
        p_STARTDATE.Value = strAttDateFrom;

        SqlParameter p_ENDDATE = cmd.Parameters.Add("ENDDATE", SqlDbType.DateTime);
        p_ENDDATE.Direction = ParameterDirection.Input;
        p_ENDDATE.Value = strAttDateTo;


        this.CreateDT(cmd, "AttnReader");
        return ds.Tables["AttnReader"];

    }

    public DataTable CreateDT(SqlCommand command, string TableName)//, SqlCommand com, SqlDataAdapter da)
    {
        try
        {
            //OraConnection.Open(); 
            sqlConn = new SqlConnection(strConnection);
            sqlConn.Open();
            command.CommandType = CommandType.Text;
            command.Connection = sqlConn;
            adapter.SelectCommand = command;
            adapter.Fill(ds, TableName);

            sqlConn.Close();
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            if (sqlConn.State == ConnectionState.Open)
            {
                sqlConn.Close();
            }
        }

        return ds.Tables[TableName];
    }

    public DataTable GetDistinctEmpData(string strLocId,string strStatus)
    {
        string strSQL = "SELECT UPPER(E.EMPID) AS EMPID,E.FULLNAME,E.AttnPolicyID,E.LocID,E.weekendid,E.Status as EmpStatus "
                        + " FROM EMPINFO E  WHERE E.STATUS=@STATUS";
        string strCond = "";
        if (strLocId != "99999")
            strCond = " AND E.LocID=" + strLocId;
        else
            strCond = "";

        strSQL = strSQL + strCond; // +" ORDER BY A.AttndDate,E.EmpId";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_STATUS = cmd.Parameters.Add("STATUS", SqlDbType.Char);
        p_STATUS.Direction = ParameterDirection.Input;
        p_STATUS.Value = strStatus;

        return objDC.CreateDT(cmd, "DistinctEmpInfo");
    }


    public DataTable GetEmpData(string strDateFrom, string strDateTo,string strLocId)
    {
        string strSQL = "SELECT UPPER(E.EMPID) AS EMPID,E.FULLNAME,E.AttnPolicyID,E.LocId,E.weekendid,'' AttnExist,A.Status,A.attnddate,E.Status as EmpStatus "
                        + " FROM EMPINFO E LEFT OUTER JOIN Attandance A ON UPPER(E.EMPID)=UPPER(A.EMPID) "
                        + " AND A.ATTNDDATE BETWEEN  @STARTDATE and @ENDDATE ";
        string strCond = "";
        if (strLocId != "99999")
            strCond = " AND E.LocId=" + strLocId;
        else
            strCond="";

        strSQL = strSQL + strCond; // +" ORDER BY A.AttndDate,E.EmpId";


        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_STARTDATE = cmd.Parameters.Add("STARTDATE", SqlDbType.DateTime);
        p_STARTDATE.Direction = ParameterDirection.Input;
        p_STARTDATE.Value = strDateFrom;

        SqlParameter p_ENDDATE = cmd.Parameters.Add("ENDDATE", SqlDbType.DateTime);
        p_ENDDATE.Direction = ParameterDirection.Input;
        p_ENDDATE.Value = strDateTo;

        return objDC.CreateDT(cmd, "EmpInfoReader");
    }

    public DataTable GetWeekEndData()
    {
        string strSQL = "SELECT * FROM WeekEndPackmst WHERE ISACTIVE='Y'";
        return objDC.CreateDT(strSQL, "WeekEndReader");
    }

    public DataTable GetHolidayData(string strYear,string strStartDate,string strEndDate)
    {
        string strSQL = "SELECT * FROM Holidaysdetls WHERE holidayyear=@holidayyear and holidate BETWEEN  @STARTDATE AND @ENDDATE ";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_holidayyear = cmd.Parameters.Add("holidayyear", SqlDbType.Char);
        p_holidayyear.Direction = ParameterDirection.Input;
        p_holidayyear.Value = strYear;

        SqlParameter p_STARTDATE = cmd.Parameters.Add("STARTDATE", SqlDbType.DateTime);
        p_STARTDATE.Direction = ParameterDirection.Input;
        p_STARTDATE.Value = strStartDate;

        SqlParameter p_ENDDATE = cmd.Parameters.Add("ENDDATE", SqlDbType.DateTime);
        p_ENDDATE.Direction = ParameterDirection.Input;
        p_ENDDATE.Value = strEndDate;

        return objDC.CreateDT(cmd, "HolidayReader");
    }

    public DataTable GetLeaveDateData(string strStartDate, string strEndDate)
    {
        if (objDC.ds.Tables["LeaveDateData"] != null)
        {
            objDC.ds.Tables["LeaveDateData"].Rows.Clear();
            objDC.ds.Tables["LeaveDateData"].Dispose();
        }

        string strSQL = "SELECT * FROM levappdetdate LDD,LEAVAPPMST LM "
        + " WHERE LDD.LVAPPID=LM.LVAPPID AND LM.APPSTATUS='A' AND LevDate BETWEEN  @STARTDATE AND @ENDDATE ";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_STARTDATE = cmd.Parameters.Add("STARTDATE", SqlDbType.DateTime);
        p_STARTDATE.Direction = ParameterDirection.Input;
        p_STARTDATE.Value = strStartDate;

        SqlParameter p_ENDDATE = cmd.Parameters.Add("ENDDATE", SqlDbType.DateTime);
        p_ENDDATE.Direction = ParameterDirection.Input;
        p_ENDDATE.Value = strEndDate;

        return objDC.CreateDT(cmd, "LeaveDateData");
    }

    public DataTable SelectDivision()
    {
        string strSQL = "SELECT DivisionID,DivisionName FROM DivisionList WHERE ISACTIVE='Y'";
        return objDC.CreateDT(strSQL, "DivisionReader");
    }

    public DataTable SelectLocation(Int32 LocationID)
    {
        SqlCommand command = new SqlCommand("proc_Select_Location");
        SqlParameter p_LocationID = command.Parameters.Add("LocationID", SqlDbType.BigInt);
        p_LocationID.Direction = ParameterDirection.Input;
        p_LocationID.Value = LocationID;
        objDC.CreateDSFromProc(command, "Location");
        return objDC.ds.Tables["Location"];
    }

    public DataTable GetAttnPlicy()
    {
        string strSQL = "SELECT attnpolicyid,policyname,otstartgrace,arvlgrace,lunchbreak,sunin,sunout,issunnextday FROM attdnpolicy";
        return objDC.CreateDT(strSQL, "AttnPlcyReader");
    }

    public string InsertAttendance(DataTable dtAttn,string strLocId,string strFromDate,string strToDate)
    {
        string str="";
        int i=0;
        SqlCommand[] cmd;
        cmd = new SqlCommand[dtAttn.Rows.Count + 1];
        string strSQL = "";
        foreach (DataRow dRow in dtAttn.Rows)
        {
            if (dRow["IsAttnExist"].ToString().Trim() == "N")
            {
                strSQL = "INSERT INTO Attendance(EMPID,AttndDate,SignInTime,SignOutTime,SingOutTimeF,Status,LateTimeAmt,AttnPolicyId, "
                    + " WeekEndID,InLocation,OutLocation,LaunchMinutes) "
                    + " VALUES(@EMPID,@AttndDate,@SignInTime,@SignOutTime,@SingOutTimeF,@Status,@LateTimeAmt,@AttnPolicyId, "
                    + " @WeekEndID,@InLocation,@OutLocation,@LaunchMinutes)";
            }
            else
            {
                strSQL = "UPDATE Attendance SET "
                    + " SignInTime=@SignInTime, "
                    + " SignOutTime=@SignOutTime, "
                    + " SingOutTimeF=@SingOutTimeF, "
                    + " Status=@Status, "
                    + " LateTimeAmt=@LateTimeAmt, "
                    + " AttnPolicyId=@AttnPolicyId, "
                    + " WeekEndID=@WeekEndID, "
                    + " InLocation=@InLocation, "
                    + " OutLocation=@OutLocation, "
                    + " LaunchMinutes=@LaunchMinutes "
                    + " WHERE EMPID=@EMPID AND AttndDate=@AttndDate";
            }

            cmd[i] = new SqlCommand(strSQL);
            cmd[i].CommandType = CommandType.Text;

            SqlParameter p_EMPID = cmd[i].Parameters.Add("EMPID", SqlDbType.Char);
            p_EMPID.Direction = ParameterDirection.Input;
            p_EMPID.Value = dRow["EMPID"].ToString().Trim();

            SqlParameter p_AttndDate = cmd[i].Parameters.Add("AttndDate", SqlDbType.DateTime);
            p_AttndDate.Direction = ParameterDirection.Input;
            p_AttndDate.Value = Common.SetDate(dRow["AttndDate"].ToString().Trim());

            //str= p_AttndDate.Value.ToString();
            //break;

            SqlParameter p_SignInTime = cmd[i].Parameters.Add("SignInTime", DBNull.Value);
            p_SignInTime.Direction = ParameterDirection.Input;
            p_SignInTime.IsNullable = true;
            if (dRow["SignInTime"].ToString().Trim() != "")
                p_SignInTime.Value = Common.SetDateTime(dRow["SignInTime"].ToString().Trim());

            SqlParameter p_SignOutTime = cmd[i].Parameters.Add("SignOutTime", DBNull.Value);
            p_SignOutTime.Direction = ParameterDirection.Input;
            p_SignOutTime.IsNullable = true;
            if (dRow["SignOutTime"].ToString().Trim() != "")
                p_SignOutTime.Value = Common.SetDateTime(dRow["SignOutTime"].ToString().Trim());

            SqlParameter p_SingOutTimeF = cmd[i].Parameters.Add("SingOutTimeF", DBNull.Value);
            p_SingOutTimeF.Direction = ParameterDirection.Input;
            p_SingOutTimeF.IsNullable = true;
            if (dRow["SingOutTimeF"].ToString().Trim() != "")
                p_SingOutTimeF.Value = Common.SetDateTime(dRow["SingOutTimeF"].ToString().Trim());

            SqlParameter p_Status = cmd[i].Parameters.Add("Status", DBNull.Value);
            p_Status.Direction = ParameterDirection.Input;
            p_Status.IsNullable = true;
            if (dRow["Status"].ToString().Trim() != "")
                p_Status.Value = dRow["Status"].ToString().Trim();

            SqlParameter p_LateTimeAmt = cmd[i].Parameters.Add("LateTimeAmt", DBNull.Value);
            p_LateTimeAmt.Direction = ParameterDirection.Input;
            p_LateTimeAmt.IsNullable = true;
            if (dRow["LateTimeAmt"].ToString().Trim() != "")
                p_LateTimeAmt.Value = dRow["LateTimeAmt"].ToString().Trim();

            SqlParameter p_AttnPolicyId = cmd[i].Parameters.Add("AttnPolicyId", DBNull.Value);
            p_AttnPolicyId.Direction = ParameterDirection.Input;
            p_AttnPolicyId.IsNullable = true;
            if (dRow["AttnPolicyId"].ToString().Trim() != "")
                p_AttnPolicyId.Value = dRow["AttnPolicyId"].ToString().Trim();

            SqlParameter p_WeekEndID = cmd[i].Parameters.Add("WeekEndID", DBNull.Value);
            p_WeekEndID.Direction = ParameterDirection.Input;
            p_WeekEndID.IsNullable = true;
            if (dRow["WeekEndID"].ToString().Trim() != "")
                p_WeekEndID.Value = dRow["WeekEndID"].ToString().Trim();

            SqlParameter p_InLocation = cmd[i].Parameters.Add("InLocation", DBNull.Value);
            p_InLocation.Direction = ParameterDirection.Input;
            p_InLocation.IsNullable = true;
            if (dRow["InLocation"].ToString().Trim() != "")
                p_InLocation.Value = dRow["InLocation"].ToString().Trim();

            SqlParameter p_OutLocation = cmd[i].Parameters.Add("OutLocation", DBNull.Value);
            p_OutLocation.Direction = ParameterDirection.Input;
            p_OutLocation.IsNullable = true;
            if (dRow["OutLocation"].ToString().Trim() != "")
                p_OutLocation.Value = dRow["OutLocation"].ToString().Trim();

            SqlParameter p_LaunchMinutes = cmd[i].Parameters.Add("LaunchMinutes", DBNull.Value);
            p_LaunchMinutes.Direction = ParameterDirection.Input;
            p_LaunchMinutes.IsNullable = true;
            if (dRow["LaunchMinutes"].ToString().Trim() != "")
                p_LaunchMinutes.Value = dRow["LaunchMinutes"].ToString().Trim();
            i++;
        }
        // return str;
        cmd[i] = new SqlCommand("INSERT INTO AttnImportLog(LogId,LocationID,FromDate,ToDate) VALUES(@LogId,@LocationID,@FromDate,@ToDate)");
        cmd[i].CommandType = CommandType.Text;

        SqlParameter p_LogId = cmd[i].Parameters.Add("LogId", SqlDbType.BigInt);
        p_LogId.Direction = ParameterDirection.Input;
        p_LogId.Value = Common.getMaxId("AttnImportLog", "LogId");

        SqlParameter p_LocationID = cmd[i].Parameters.Add("LocationID", SqlDbType.BigInt);
        p_LocationID.Direction = ParameterDirection.Input;
        p_LocationID.Value = strLocId;

        SqlParameter p_FromDate = cmd[i].Parameters.Add("FromDate", SqlDbType.DateTime);
        p_FromDate.Direction = ParameterDirection.Input;
        p_FromDate.Value = strFromDate;

        SqlParameter p_ToDate = cmd[i].Parameters.Add("ToDate", SqlDbType.DateTime);
        p_ToDate.Direction = ParameterDirection.Input;
        p_ToDate.Value = strToDate;

        objDC.MakeTransaction(cmd);
        return "Attendance Record Imported Successfully.";
    }


    public DataTable GetImporterLog()
    {
        string strSQL = "SELECT MAX(TODATE) FROM AttnImportLog";
        SqlCommand cmd = new SqlCommand(strSQL);
        string MaxLogDate = objDC.GetScalarVal(cmd);
        if (MaxLogDate != "")
            strSQL = "SELECT * FROM AttnImportLog WHERE Todate='" + MaxLogDate + "'";
        else
            strSQL = "SELECT * FROM AttnImportLog WHERE 1<>2";
        return objDC.CreateDT(strSQL, "AttnImportLog");
    }
	public AttnManager()
	{
		//
		// TODO: Add constructor logic here
		//
    }

    #region AttendanceLog
    public void UpdateAttandance(clsCommonSetup clsCommon, string strRemarkType, string remarks, string strFdate, string strTdate, string empID)
    {
        int i = 0;
        
        SqlCommand[] command = new SqlCommand[2];

        command[i] = new SqlCommand("proc_Update_Attandance");
        command[i].CommandType = CommandType.StoredProcedure;

        SqlParameter p_RType = command[i].Parameters.Add("RemarksType", SqlDbType.Char);
        p_RType.Direction = ParameterDirection.Input;
        p_RType.Value = strRemarkType;

        SqlParameter p_Remarks = command[i].Parameters.Add("Remarks", SqlDbType.VarChar);
        p_Remarks.Direction = ParameterDirection.Input;
        p_Remarks.Value = remarks;

        SqlParameter p_Fdate = command[i].Parameters.Add("FromDate", SqlDbType.DateTime);
        p_Fdate.Direction = ParameterDirection.Input;
        p_Fdate.Value = Common.ReturnDate(strFdate);

        SqlParameter p_Tdate = command[i].Parameters.Add("ToDate", SqlDbType.DateTime);
        p_Tdate.Direction = ParameterDirection.Input;
        p_Tdate.Value = Common.ReturnDate(strTdate);

        SqlParameter p_InsertedBy = command[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = clsCommon.InsertedBy;

        SqlParameter p_empID = command[i].Parameters.Add("EmployeeID", SqlDbType.VarChar);
        p_empID.Direction = ParameterDirection.Input;
        p_empID.Value = empID;

        i++;
        command[i] = new SqlCommand("proc_Update_Attandance");
        command[i].CommandType = CommandType.StoredProcedure;

        SqlParameter p_AttRType = command[i].Parameters.Add("RemarksType", SqlDbType.Char);
        p_AttRType.Direction = ParameterDirection.Input;
        p_AttRType.Value = strRemarkType;

        SqlParameter p_AttRemarks = command[i].Parameters.Add("Remarks", SqlDbType.VarChar);
        p_AttRemarks.Direction = ParameterDirection.Input;
        p_AttRemarks.Value = remarks;

        SqlParameter p_AttFdate = command[i].Parameters.Add("FromDate", SqlDbType.DateTime);
        p_AttFdate.Direction = ParameterDirection.Input;
        p_AttFdate.Value = Common.ReturnDate(strFdate);

        SqlParameter p_AttTdate = command[i].Parameters.Add("ToDate", SqlDbType.DateTime);
        p_AttTdate.Direction = ParameterDirection.Input;
        p_AttTdate.Value = Common.ReturnDate(strTdate);

        SqlParameter p_AttInsertedBy = command[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_AttInsertedBy.Direction = ParameterDirection.Input;
        p_AttInsertedBy.Value = clsCommon.InsertedBy;

        SqlParameter p_AttempID = command[i].Parameters.Add("EmployeeID", SqlDbType.VarChar);
        p_AttempID.Direction = ParameterDirection.Input;
        p_AttempID.Value = empID;
        
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
    #endregion
}
