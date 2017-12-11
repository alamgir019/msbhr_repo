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
using System.Collections;



/// <summary>
/// Summary description for MailManager
/// </summary>
public class MailManager
{
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
    LeaveApplicationManager objLeaveMgr = new LeaveApplicationManager();
    DBConnector objDC = new DBConnector();

    string strFromAddr = "";
    string strToEmpId = "";
    string strToAddr = "";
    string strSubject = "";
    string strBody = "";
    string strErrText = "";
    string MailServer = "";
    int MailPort ;
	public MailManager()
	{
		//
		// TODO: Add constructor logic here
		//
    }


    # region LEAVE MAIL CONFIGURATION
    // Leave Application Mail
    public string RequestForApproval(string strEmpID,string strLvAppID,string strLvPackStartDate,
        string strLvPackEndDate,string strUserEmpId,string strUserName,string strDesig,string strLocation,string strIsSysAdmin,
        string strSpvID,string strSpvEmail)
    {
        //MailServer = ConfigurationManager.AppSettings["MyMailServer"].ToString();
        //MailPort = Convert.ToInt32(ConfigurationManager.AppSettings["MyMailServerPort"]);

        strErrText = "Application is mailed to the supervisor";
        // Requesting Employee Info
        string strFwdBy = "";
        DataTable dtFromEmp = new DataTable();
        dtFromEmp = objEmpInfoMgr.SelectEmpInfo(strEmpID);

        if (dtFromEmp.Rows.Count > 0)
        {
            strFromAddr = dtFromEmp.Rows[0]["OfficeEmail"].ToString().Trim();
            //strToEmpId = dtFromEmp.Rows[0]["reportingTo"].ToString().Trim();
            strToEmpId = strSpvID;
        }
        if (strIsSysAdmin == "N")
        {
            if (strEmpID.Trim().ToLower() == strUserEmpId.Trim().ToLower())
            {
                strUserName = "";
                strDesig = "";
                strLocation = "";
            }
            else
            {
                strFwdBy = strUserName + ", " + strDesig + ", " + strLocation;
            }
        }
        else
        {
            strFwdBy = strUserName + "(SysAdmin), " + strDesig + ", " + strLocation;
        }




        strToAddr = strSpvEmail;
        DataTable dtLeaveApp = new DataTable();
        
        dtLeaveApp = objLeaveMgr.SelectRequestLeaveAppMst(Convert.ToInt16(strLvAppID), strEmpID, "R", strLvPackStartDate, strLvPackEndDate, "");

        // Get COPY TO EMAIL Address
        string strCopyToName = "";
        string strCopyAddr = "";
        DataTable dtLvCopyTo = new DataTable();
        LeaveApplicationManager objLvMgr = new LeaveApplicationManager();
        dtLvCopyTo = objLvMgr.SelectLeaveCopyTo(strLvAppID);
        foreach (DataRow dRow in dtLvCopyTo.Rows)
        {
            if (strCopyToName == "")
            {
                strCopyToName = dRow["SPVFULLNAME"].ToString();
                strCopyAddr = dRow["CopyToEmail"].ToString();
            }
            else
            {
                strCopyToName = strCopyToName + ", " + dRow["SPVFULLNAME"].ToString();
                strCopyAddr = strCopyAddr + ";" + dRow["CopyToEmail"].ToString();

            }
        }  



        if (dtLeaveApp.Rows.Count > 0)
        {

            DateTime ResumeDate = Convert.ToDateTime(dtLeaveApp.Rows[0]["ResumeDate"].ToString());
           // Holiday and Weekend Issue Exist. Need to Solve
           // ResumeDate = ResumeDate.AddDays(1);
            string strVPath = "http://10.48.32.16/hr";
            strSubject = "Request for approving leave.";
            strBody = " Leave applicant: " + dtFromEmp.Rows[0]["FullName"].ToString() + ", "
                    + dtFromEmp.Rows[0]["JobTitle"].ToString() + ", " + dtFromEmp.Rows[0]["PostingPlaceName"].ToString()
                    + " \n\n "
                    + "Request forwarded by: " + strFwdBy
                    + " \n\n "
                    + "Copied to: " + strCopyToName
                    + " \n\n "
                    + "Please approve the following leave request: "
                   + " \n "
                    + "Leave type: " + dtLeaveApp.Rows[0]["LTypeTitle"].ToString()
                  + " \n "
                    + "From: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveStart"].ToString())
                  + " \n "
                    + "To: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveEnd"].ToString())
                 + " \n "
                    + "Resume office on: " + Common.DisplayDate(ResumeDate.ToShortDateString())
                 + " \n "
                    + "Reason for leave: " + dtLeaveApp.Rows[0]["LTreason"].ToString()
                 + " \n "
                    + "Contact number: " + dtLeaveApp.Rows[0]["PhoneNo"].ToString()
                  + " \n\n "
                    + "With thanks "
                   + " \n\n "
                    + dtFromEmp.Rows[0]["FullName"].ToString()
                  + " \n "
                    + dtFromEmp.Rows[0]["JobTitle"].ToString()
                  + " \n "

                   + "======================================"
                   + " \n\n "
                   + " Click here to login for approval: " + strVPath;
        }
        try
        {
            if (strFromAddr != "" && strToAddr != "")
            {
                //SmtpClient MySmtpClient = new SmtpClient(MailServer, MailPort);
               // MySmtpClient.UseDefaultCredentials = true;
                System.Web.Mail.MailMessage objMsg = new System.Web.Mail.MailMessage();
                objMsg.From = strFromAddr;
                objMsg.To = strToAddr;
                objMsg.Cc = strCopyAddr;
                objMsg.Subject = strSubject;
                objMsg.Body = strBody;
                //MySmtpClient.Send(
                SmtpMail.Send(objMsg);
            }
        }
        catch
        {
            strErrText = "Mail is not send. Server SMTP is not configured.";
        }


        //dtToAdd.Rows.Clear();
        //dtToAdd.Dispose();

        dtFromEmp.Rows.Clear();
        dtFromEmp.Dispose();

        return strErrText;
    }

    // Leave Approve Mail 
    public string LeaveApproval(string strEmpID, string strLvAppID,
     string strUserEmpId, string strUserName, string strDesig, string strLocation, string strIsSysAdmin,string strUserEmail)
    {

        // Requesting Employee Info
        strErrText = "";
        string strApvBy = "";
        DataTable dtToEmp = new DataTable();
        dtToEmp = objEmpInfoMgr.SelectEmpInfo(strEmpID);

        if (dtToEmp.Rows.Count > 0)
        {
            strToAddr = dtToEmp.Rows[0]["OfficeEmail"].ToString().Trim();
            //strToEmpId = dtFromEmp.Rows[0]["reportingTo"].ToString().Trim();
           // strToEmpId = strSpvID;
        }
        if (strIsSysAdmin == "N")
        {
            if (strEmpID.Trim().ToLower() == strUserEmpId.Trim().ToLower())
            {
                strUserName = "";
                strDesig = "";
                strLocation = "";
            }
            else
            {
                strApvBy = strUserName + ", " + strDesig + ", " + strLocation;
            }
        }
        else
        {
            strApvBy = strUserName + "(SysAdmin), " + strDesig + ", " + strLocation;
        }
        strFromAddr = strUserEmail;
        DataTable dtLeaveApp = new DataTable();
        dtLeaveApp = objLeaveMgr.SelectLeaveAppMstStatus(Convert.ToInt16(strLvAppID), strEmpID, "A");



        // Get COPY TO EMAIL Address
        string strCopyToName = "";
        string strCopyAddr = "";
        DataTable dtLvCopyTo = new DataTable();
        LeaveApplicationManager objLvMgr = new LeaveApplicationManager();
        dtLvCopyTo = objLvMgr.SelectLeaveCopyTo(strLvAppID);
        foreach (DataRow dRow in dtLvCopyTo.Rows)
        {
            if (strCopyToName == "")
            {
                strCopyToName = dRow["SPVFULLNAME"].ToString();
                strCopyAddr = dRow["CopyToEmail"].ToString();
            }
            else
            {
                strCopyToName = strCopyToName + ", " + dRow["SPVFULLNAME"].ToString();
                strCopyAddr = strCopyAddr + ";" + dRow["CopyToEmail"].ToString();

            }
        }  


        if (dtLeaveApp.Rows.Count > 0)
        {
            DateTime ResumeDate = Convert.ToDateTime(dtLeaveApp.Rows[0]["ResumeDate"].ToString());
            // Holiday and Weekend Issue Exist. Need to Solve
           // ResumeDate = ResumeDate.AddDays(1);
            string strVPath = "http://10.48.32.16/hr";
            strSubject = "Approved as requested.";
            strBody = " Your leave application is approved. "
                    + " \n\n "
                    + "Thanks, "
                    + " \n "
                    + strApvBy
                    + " \n "
                    + "===================================================="
                    + " \n "
                    + "Leave Details:"
                    + " \n "
                    + "--------------"
                    + " \n "
                    + "Leave Applicant: " + dtToEmp.Rows[0]["FullName"].ToString() + ", "
                    + dtToEmp.Rows[0]["JobTitle"].ToString() + ", " + dtToEmp.Rows[0]["PostingPlaceName"].ToString()
                    + " \n "
                    + "Date of Application:  " + dtLeaveApp.Rows[0]["AppDate"].ToString()
                    + " \n "
                    + "Leave type: " + dtLeaveApp.Rows[0]["LTypeTitle"].ToString()
                    + " \n "
                    + "From: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveStart"].ToString())
                    + " \n "
                    + "To: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveEnd"].ToString())
                    + " \n "
                    + "Back in office: " + Common.DisplayDate(ResumeDate.ToShortDateString())
                    + " \n "
                    + "Reason for leave: " + dtLeaveApp.Rows[0]["LTreason"].ToString()
                    + " \n "
                    + "Contact number: " + dtLeaveApp.Rows[0]["PhoneNo"].ToString()
                    + " \n "
                   + "===================================================="
                   + " \n\n ";
        }
        try
        {

            if (strFromAddr != "" && strToAddr != "")
            {
                System.Web.Mail.MailMessage objMsg = new System.Web.Mail.MailMessage();
                objMsg.From = strFromAddr;
                objMsg.To = strToAddr;
                objMsg.Cc = strCopyAddr;
                objMsg.Subject = strSubject;
                objMsg.Body = strBody;
                SmtpMail.Send(objMsg);
            }

            //System.Net.Mail.SmtpClient;
        }
        catch
        {
            strErrText = "Mail is not send. Please configure the internet.";
        }


        //dtToAdd.Rows.Clear();
        //dtToAdd.Dispose();

        dtToEmp.Rows.Clear();
        dtToEmp.Dispose();

        return strErrText;
    }

    # endregion

   
    protected string GetCurrency(string strCurrID)
    {
        string strCurrName="";
        switch (strCurrID)
        {
            case "B":
                strCurrName = "BDT";
                break;
            case "D":
                strCurrName = "Dollar";
                break;
            case "P":
                strCurrName = "Pound";
                break;
            case "E":
                strCurrName = "Euro";
                break;
            case "R":
                strCurrName = "Rupees";
                break;

        }
        return strCurrName;
    }



    protected string GetCountryDirectorEmail()
    {
        string strSQL = "SELECT OfficeEmail FROM EMPINFO WHERE ISCOUNTRYDIRECTOR='Y' AND STATUS='A' AND ISDELETED='N' ";
        SqlCommand cmd = new SqlCommand(strSQL);
        strSQL = objDC.GetScalarVal(cmd);
        return strSQL;
    }

    protected string GetEmployeeEmail(string strEmpID)
    {
        string strSQL = "SELECT OfficeEmail FROM EMPINFO WHERE EMPID=@EMPID ";
        SqlCommand cmd = new SqlCommand(strSQL);

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;
        strSQL = objDC.GetScalarVal(cmd);
        return strSQL;
    }

 
}
