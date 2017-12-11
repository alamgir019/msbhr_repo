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
using System.Web.Mail;
using System.Net.Mail;

/// <summary>
/// Summary description for AttnRemindManager
/// </summary>
public class AttnRemindManager
{
    DBConnector objDC = new DBConnector();

    int MailPort;
    string MailServer = "";

    public DataTable SelectDivisionddl(Int32 DivisionID)
    {
        SqlCommand command = new SqlCommand("proc_Select_Divisionddl");
        SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = DivisionID;
        objDC.CreateDSFromProc(command, "Division");
        return objDC.ds.Tables["Division"];
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

    public DataTable SelectSupervisorWiseEmp(string EmpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_SupervisorWiseEmp");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDSFromProc(command, "SupervisorWiseEmp");
        return objDC.ds.Tables["SupervisorWiseEmp"];
    }

    public DataTable SelectLocationWiseEmp(string strLocID)
    {
        SqlCommand command = new SqlCommand("proc_Select_LocationWiseEmp");

        SqlParameter p_LocId = command.Parameters.Add("SalLocId", SqlDbType.BigInt);
        p_LocId.Direction = ParameterDirection.Input;
        p_LocId.Value = strLocID;

        objDC.CreateDSFromProc(command, "LocationWiseEmp");
        return objDC.ds.Tables["LocationWiseEmp"];
    }

    public DataTable SelectDivisionWiseEmp(string strDivID)
    {
        SqlCommand command = new SqlCommand("proc_Select_DivisionWiseEmp2");

        SqlParameter p_DivisionId = command.Parameters.Add("DivisionId", SqlDbType.BigInt);
        p_DivisionId.Direction = ParameterDirection.Input;
        p_DivisionId.Value = strDivID;

        objDC.CreateDSFromProc(command, "DivisionWiseEmp2");
        return objDC.ds.Tables["DivisionWiseEmp2"];
    }

    public DataTable GetAbsentRecord(string strEmpIDs,string strFromDate,string strToDate)
    {
        string strSQL = "SELECT EMPID,ATTNDDATE,STATUS FROM Attendance "
                     + " WHERE EMPID IN("+ strEmpIDs +") AND STATUS='A' AND "
                     + " ATTNDDATE BETWEEN '" + strFromDate + "' AND '" + strToDate + "' "  ;
        return objDC.CreateDT(strSQL, "EmpWiseAttn");
    }

    public DataTable GetPendingSplittedLeaveRecord()
    {
        string strSQL = "SELECT LDD.EmpID,LevDate from LevAppDetDate LDD,LeavAppMst LM where LDD.LvAppID=LM.LvAppID AND LM.AppStatus='R'";
        return objDC.CreateDT(strSQL, "GetPendingLeaveRecord");
    }

    public DataTable GetPendingLeaveRecord(string strStartDate,string strEndDate)
    {
        string strSQL = " select LM.EmpID,LD.LeaveStart,ld.LeaveEnd "
                       + " from LeavAppDets LD, LeavAppMst LM "
                       + " where LD.LvAppID=LM.LvAppID AND LM.AppStatus='R' and "
                       + " (ld.LeaveStart between @StartDate and @EndDate "
                       + " or ld.LeaveEnd between @StartDate and @EndDate)";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_StartDate = cmd.Parameters.Add("StartDate", SqlDbType.DateTime);
        p_StartDate.Direction = ParameterDirection.Input;
        p_StartDate.Value = strStartDate;

        SqlParameter p_EndDate = cmd.Parameters.Add("EndDate", SqlDbType.DateTime);
        p_EndDate.Direction = ParameterDirection.Input;
        p_EndDate.Value = strEndDate;

        return objDC.CreateDT(cmd, "GetPendingLeaveRecord2");
    }

    public DataTable GetPendingTravelRecord(string strStartDate, string strEndDate)
    {
        string strSQL = "  select EmpID,DepartureDate,ReturnDate "
                        + " from EmpTravel "
                        + " where TravelStatus='R' and "
                        + " (DepartureDate between @StartDate and @EndDate "
                        + " or ReturnDate between @StartDate and @EndDate)";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_StartDate = cmd.Parameters.Add("StartDate", SqlDbType.DateTime);
        p_StartDate.Direction = ParameterDirection.Input;
        p_StartDate.Value = strStartDate;

        SqlParameter p_EndDate = cmd.Parameters.Add("EndDate", SqlDbType.DateTime);
        p_EndDate.Direction = ParameterDirection.Input;
        p_EndDate.Value = strEndDate;

        return objDC.CreateDT(cmd, "PendingTravelRecord");
    }


    //public DataTable GetPendingLeaveRecords(string strEmpID, string strDate)
    //{
    //    string strSQL = "select LD.* from LeavAppMst LM,LeavAppDets LD where LM.LvAppID=LD.LvAppID AND LM.AppStatus='A' AND "
    //                        + " LM.EmpID='pib00306' AND '2011-10-18' between LeaveStart and LeaveEnd ";

    //}

    public string SendReminderMail(string strUserEmpId, string strUserName,
       string strDesig, string strLocation, string strIsSysAdmin,string strUserMail, DataTable dtAbsent)
       
    {
        string strErrText = "";

        // Supervisor Info
        string strSubject = "";
        string strBody = "";
        string strFromAddr = strUserMail;
        string strFwdBy = "";
        if (strIsSysAdmin == "N")
        {
            strFwdBy = strUserName + ", " + strDesig + ", " + strLocation;
        }
        else
        {
            strFwdBy = strUserName + "(SysAdmin), " + strDesig + ", " + strLocation;
        }

        
        string strVPath = "www.planbgd.org/hr";

        // sending individual employee email
        try
        {
            foreach (DataRow dRow in dtAbsent.Rows)
            {
                strSubject = "Please update your attendance record(s).";
                strBody = " To: " + dRow["EMPNAME"].ToString().Trim()
                        + " \n\n "
                        + "From: " + strFwdBy
                        + " \n\n "
                        + "The Attendance record system has identified that your attendance is not recorded on: "
                        + " \n "
                        + dRow["AbsentDates"].ToString().Trim()
                        + " \n\n "
                        + dRow["LeaveRemarks"].ToString().Trim()
                        + " \n\n "
                        + dRow["TravelRemarks"].ToString().Trim()
                        + " \n\n "
                        + "Please update your attendance record(s). "
                        + " \n "
                        + "For any assistance, kindly contact the People & Culture Department. "
                        + " \n\n "
                        + " \n "
                        + "Thanks";

                if (strFromAddr != "" && dRow["EmailID"].ToString().Trim() != "")
                {
                    if (Common.CheckNullString(dRow["AbsentDates"].ToString().Trim()) != "")
                    {
                        SmtpClient MySmtpClient = new SmtpClient(MailServer, MailPort);
                        MySmtpClient.UseDefaultCredentials = true;
                        System.Net.Mail.MailMessage objMsg = new System.Net.Mail.MailMessage(strFromAddr, dRow["EmailID"].ToString().Trim(), strSubject, strBody);
                        MySmtpClient.Send(objMsg);
                    }
                }
                //System.Net.Mail.SmtpClient;
            }
        }
        catch
        {
            strErrText = "Mail is not send. Please configure the internet.";
        }
        //dtToAdd.Rows.Clear();
        //dtToAdd.Dispose();
        return strErrText;
    }
	public AttnRemindManager()
	{
		//
		// TODO: Add constructor logic here
		//
        MailServer = ConfigurationManager.AppSettings["MyMailServer"].ToString();
        MailPort = Convert.ToInt32(ConfigurationManager.AppSettings["MyMailServerPort"]);
	}
}
