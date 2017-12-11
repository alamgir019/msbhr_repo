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
// For Exchange Server
using System.Net;
using System.Collections.Generic;
using System.Text; 

public class MailManagerSmtpClient
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
    string AppURL = "";
    string SystemEmail = "";
    int MailPort;

        string SystemEmailUserName = "";
    string SystemEmailPwd = "";

    public MailManagerSmtpClient()
    {
        MailServer = ConfigurationManager.AppSettings["MyMailServer"].ToString();
        MailPort = Convert.ToInt32(ConfigurationManager.AppSettings["MyMailServerPort"]);
        AppURL = ConfigurationManager.AppSettings["AppURL"].ToString();

       // MailServer = ConfigurationManager.AppSettings["MyMailServer"].ToString();
       // MailPort = Convert.ToInt32(ConfigurationManager.AppSettings["MyMailServerPort"]);

        SystemEmail = ConfigurationManager.AppSettings["MySystemEmail"].ToString();

        SystemEmailUserName = ConfigurationManager.AppSettings["MyEmailUserName"].ToString();
        SystemEmailPwd = ConfigurationManager.AppSettings["MyEmailPwd"].ToString();
    }


    #region PAYROLL MAIL CONFIGURATION

    private void SendMail(string strFromAddr, string strToAddr, string strSubject, string strBody, string strCC)
    {
        if (strFromAddr != "" && strToAddr != "")
        {
            SmtpClient MySmtpClient = new SmtpClient(MailServer, MailPort);
            MySmtpClient.UseDefaultCredentials = true;

            MySmtpClient.EnableSsl = true;
            MySmtpClient.Credentials = new System.Net.NetworkCredential("Bangpay@wateraid.org.uk", "Wateraid99");

            System.Net.Mail.MailMessage objMsg = new System.Net.Mail.MailMessage("Bangpay@wateraid.org.uk", strToAddr, strSubject, strBody);

            if (strCC != "")
            {
                string[] CC = strCC.Split(';');
                foreach (string str in CC)
                {
                    if (string.IsNullOrEmpty(str) == false)
                        objMsg.CC.Add(str);
                }
            }

            objMsg.IsBodyHtml = true;
            MySmtpClient.Send(objMsg);
        }
    }

    public string PayslipEmail(string strFromAddr, string strToAddr, string strSubject, string strBody, string strCC)
    {
        strErrText = "Y";
        try
        {
            this.SendMail(strFromAddr, strToAddr, strSubject, strBody, strCC);
        }
        catch
        {
            strErrText = "N";
        }

        return strErrText;
    }

    #endregion

    # region LEAVE MAIL CONFIGURATION

    private void SendMailLeave(string strFromAddr, string strToAddr, string strSubject, string strBody, string strCC)
    {
        if (strFromAddr != "" && strToAddr != "")
        {
            //To notify HR and HR Assistant
            MasterTablesManager MasMgr = new MasterTablesManager();
            EmpInfoManager objEmpInfoManager = new EmpInfoManager();

            DataTable dtHR = MasMgr.GetEmailNotification();
            DataTable dtHRAssistant = objEmpInfoManager.SelectEmpInfoForLeave("WAB0019");

            if (strFromAddr != dtHR.Rows[0]["Notify"].ToString().Trim() && strToAddr != dtHR.Rows[0]["Notify"].ToString().Trim())
            {
                if (string.IsNullOrEmpty(strCC) == true)
                {
                    strCC = dtHR.Rows[0]["Notify"].ToString().Trim();
                }
                else
                {
                    strCC = strCC + ";" + dtHR.Rows[0]["Notify"].ToString().Trim();
                }
            }

            if (strFromAddr != dtHRAssistant.Rows[0]["OfficeEmail"].ToString().Trim() && strToAddr != dtHRAssistant.Rows[0]["OfficeEmail"].ToString().Trim())
            {
                if (string.IsNullOrEmpty(strCC) == true)
                {
                    strCC = dtHRAssistant.Rows[0]["OfficeEmail"].ToString().Trim();
                }
                else
                {
                    strCC = strCC + ";" + dtHRAssistant.Rows[0]["OfficeEmail"].ToString().Trim();
                }
            }

            SmtpClient MySmtpClient = new SmtpClient(MailServer, MailPort);
            MySmtpClient.UseDefaultCredentials = true;

            MySmtpClient.EnableSsl = true;
            MySmtpClient.Credentials = new System.Net.NetworkCredential("Bangpay@wateraid.org.uk", "Wateraid99");

            System.Net.Mail.MailMessage objMsg = new System.Net.Mail.MailMessage("Bangpay@wateraid.org.uk", strToAddr, strSubject, strBody);

            if (strCC != "")
            {
                string[] CC = strCC.Split(';');
                foreach (string str in CC)
                {
                    if (string.IsNullOrEmpty(str) == false)
                        objMsg.CC.Add(str);
                }
            }

            objMsg.IsBodyHtml = true;
            //MySmtpClient.Send(objMsg);
        }
    }

    // Leave Request Mail
    public string LeaveRequest(string strEmpID, string strLvAppID, string strLvPackStartDate,
        string strLvPackEndDate, string strUserEmpId, string strUserName, string strDesig,
        string strLocation, string strIsSysAdmin, string strSpvID, string strSpvEmail)
    {
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

        dtLeaveApp = objLeaveMgr.SelectRequestLeaveAppMst(Convert.ToInt16(strLvAppID), strEmpID, "X", strLvPackStartDate, strLvPackEndDate, "");

        ////// Get COPY TO EMAIL Address
        ////string strCopyToName = "";
        ////string strCopyAddr = "";
        ////DataTable dtLvCopyTo = new DataTable();
        ////LeaveApplicationManager objLvMgr = new LeaveApplicationManager();
        ////dtLvCopyTo = objLvMgr.SelectLeaveCopyTo(strLvAppID);

        ////foreach (DataRow dRow in dtLvCopyTo.Rows)
        ////{
        ////    if (strCopyToName == "")
        ////    {
        ////        strCopyToName = dRow["SPVFULLNAME"].ToString();
        ////        strCopyAddr = dRow["CopyToEmail"].ToString();
        ////    }
        ////    else
        ////    {
        ////        strCopyToName = strCopyToName + ", " + dRow["SPVFULLNAME"].ToString();
        ////        strCopyAddr = strCopyAddr + ";" + dRow["CopyToEmail"].ToString();
        ////    }
        ////}

        if (dtLeaveApp.Rows.Count > 0)
        {
            DateTime ResumeDate = Convert.ToDateTime(dtLeaveApp.Rows[0]["ResumeDate"].ToString());
            // Holiday and Weekend Issue Exist. Need to Solve
            // ResumeDate = ResumeDate.AddDays(1);
            string strVPath = AppURL.Trim();
            strSubject = "Request for approving leave.";
            strBody = " Leave applicant: " + dtFromEmp.Rows[0]["FullName"].ToString() + ", "
                    + dtFromEmp.Rows[0]["DesigName"].ToString() + ", " + dtFromEmp.Rows[0]["DeptName"].ToString()
                    + " <br /><br /> "
                //+ "Request forwarded by: " + strFwdBy
                //+ " <br /><br /> "
                //+ "Copied to: " + strCopyToName
                //+ " <br /><br /> "
                    + "Please recommend the following leave request: "
                    + " <br /> "
                    + "Leave type: " + dtLeaveApp.Rows[0]["LTypeTitle"].ToString()
                    + " <br /> "
                    + "From: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveStart"].ToString())
                    + " <br /> "
                    + "To: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveEnd"].ToString())
                    + " <br /> "
                    + "Resume office on: " + Common.DisplayDate(ResumeDate.ToShortDateString())
                    + " <br /> "
                    + "Reason for leave: " + dtLeaveApp.Rows[0]["LTreason"].ToString()
                    + " <br /> "
                    + "Contact number: " + dtLeaveApp.Rows[0]["PhoneNo"].ToString()
                    + " <br /><br /> "
                    + "With thanks "
                    + " <br /><br /> "
                    + dtFromEmp.Rows[0]["FullName"].ToString()
                    + " <br /> "
                    + dtFromEmp.Rows[0]["DesigName"].ToString()
                    + " <br /> "
                    + "======================================"
                    + " <br /><br /> "
                    + " Click here to login for approval: " + strVPath;
        }
        try
        {
            if (strFromAddr != "" && strToAddr != "")
            {
                SmtpClient MySmtpClient = new SmtpClient(MailServer, MailPort);
                //MySmtpClient.UseDefaultCredentials = true;

                ////Omit these 2 line
                //MySmtpClient.EnableSsl = true;
                MySmtpClient.UseDefaultCredentials = false;
                MySmtpClient.Credentials = new System.Net.NetworkCredential(SystemEmailUserName, SystemEmailPwd);

                System.Net.Mail.MailMessage objMsg = new System.Net.Mail.MailMessage(strFromAddr, strToAddr, strSubject, strBody);
                MySmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //string[] strCC = strCopyAddr.Split(';');
                //foreach (string str in strCC)
                //{
                //    if (string.IsNullOrEmpty(str) == false)
                //        objMsg.CC.Add(str);
                //}
                MySmtpClient.Send(objMsg);
                //this.SendMailLeave(strFromAddr, strToAddr, strSubject, strBody, "");
            }
        }
        catch (Exception ex)
        {
            strErrText = ex.ToString();
        }
        catch
        {
            strErrText = "Mail is not send. Please configure the internet.";
        }

        //dtToAdd.Rows.Clear();
        //dtToAdd.Dispose();

        dtFromEmp.Rows.Clear();
        dtFromEmp.Dispose();

        return strErrText;
    }

    // Leave Approve Mail 
    public string LeaveApprovalByHR(string strEmpID, string strLvAppID, string strUserEmpId, string strUserName, 
        string strDesig, string strLocation, string strIsSysAdmin, string strUserEmail)
    {
        //Requesting Employee Info
        string strCC = "";
        strErrText = "";
        string strApvBy = "";
        
        //CC Employee
        DataTable dtCCEmp = objEmpInfoMgr.SelectEmpInfo(strEmpID);
        if (dtCCEmp.Rows.Count > 0)
        {
            strCC = dtCCEmp.Rows[0]["OfficeEmail"].ToString().Trim();
        }

        //To Employee
        DataTable dtToEmp = objEmpInfoMgr.SelectEmpInfo(dtCCEmp.Rows[0]["SupervisorId"].ToString().Trim());
        if (dtToEmp.Rows.Count > 0)
        {
            strToAddr = dtToEmp.Rows[0]["OfficeEmail"].ToString().Trim();
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
        dtLeaveApp = objLeaveMgr.SelectLeaveAppMstStatus(Convert.ToInt16(strLvAppID), strEmpID, "R");

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
            string strVPath = AppURL.Trim();
            strSubject = "Leave Certified by HRD as requested.";
            strBody = "Leave Application is Certified by HRD. You may approve it."
                    + " <br /><br /> "
                    + "Thanks, "
                    + " <br /> "
                    + strApvBy
                    + " <br /> "
                    + "===================================================="
                    + " <br /> "
                    + "Leave Details:"
                    + " <br /> "
                    + "--------------"
                    + " <br /> "
                    + "Leave Applicant: " + dtCCEmp.Rows[0]["FullName"].ToString() + ", "
                    + dtCCEmp.Rows[0]["JobTitle"].ToString() + ", " + dtCCEmp.Rows[0]["PostingPlaceName"].ToString()
                    + " <br /> "
                    + "Date of Application:  " + dtLeaveApp.Rows[0]["AppDate"].ToString()
                    + " <br /> "
                    + "Leave type: " + dtLeaveApp.Rows[0]["LTypeTitle"].ToString()
                    + " <br /> "
                    + "From: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveStart"].ToString())
                    + " <br /> "
                    + "To: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveEnd"].ToString())
                    + " <br /> "
                    + "Back in office: " + Common.DisplayDate(ResumeDate.ToShortDateString())
                    + " <br /> "
                    + "Reason for leave: " + dtLeaveApp.Rows[0]["LTreason"].ToString()
                    + " <br /> "
                    + "Contact number: " + dtLeaveApp.Rows[0]["PhoneNo"].ToString()
                    + " <br /> "
                    + "===================================================="
                    + " <br /><br /> ";
        }
        try
        {
            this.SendMailLeave(strFromAddr, strToAddr, strSubject, strBody, "");
        }
        catch
        {
            strErrText = "Mail is not send. Please configure the Internet.";
        }

        return strErrText;
    }

    // Leave Approve Mail 
    public string LeaveApprovalBySupervisor(string strEmpID, string strLvAppID, string strUserEmpId, string strUserName,
        string strDesig, string strLocation, string strIsSysAdmin, string strUserEmail)
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
            string strVPath = AppURL.Trim();
            strSubject = "Leave Approved as requested.";
            strBody = "Your leave application is Approved. "
                    + " <br /><br /> "
                    + "Thanks, "
                    + " <br /> "
                    + strApvBy
                    + " <br /> "
                    + "===================================================="
                    + " <br /> "
                    + "Leave Details:"
                    + " <br /> "
                    + "--------------"
                    + " <br /> "
                    + "Leave Applicant: " + dtToEmp.Rows[0]["FullName"].ToString() + ", "
                    + dtToEmp.Rows[0]["JobTitle"].ToString() + ", " + dtToEmp.Rows[0]["PostingPlaceName"].ToString()
                    + " <br /> "
                    + "Date of Application:  " + dtLeaveApp.Rows[0]["AppDate"].ToString()
                    + " <br /> "
                    + "Leave type: " + dtLeaveApp.Rows[0]["LTypeTitle"].ToString()
                    + " <br /> "
                    + "From: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveStart"].ToString())
                    + " <br /> "
                    + "To: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveEnd"].ToString())
                    + " <br /> "
                    + "Back in office: " + Common.DisplayDate(ResumeDate.ToShortDateString())
                    + " <br /> "
                    + "Reason for leave: " + dtLeaveApp.Rows[0]["LTreason"].ToString()
                    + " <br /> "
                    + "Contact number: " + dtLeaveApp.Rows[0]["PhoneNo"].ToString()
                    + " <br /> "
                   + "===================================================="
                   + " <br /><br /> ";
        }
        try
        {
            this.SendMailLeave(strFromAddr, strToAddr, strSubject, strBody, "");
        }
        catch
        {
            strErrText = "Mail is not send. Please configure the Internet.";
        }


        //dtToAdd.Rows.Clear();
        //dtToAdd.Dispose();

        dtToEmp.Rows.Clear();
        dtToEmp.Dispose();

        return strErrText;
    }

    // Leave Regret Mail 
    public string LeaveRegretByHR(string strEmpID, string strLvAppID, string strUserEmpId, string strUserName, 
        string strDesig, string strLocation, string strIsSysAdmin, string strUserEmail)
    {
        //Requesting Employee Info
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
        dtLeaveApp = objLeaveMgr.SelectLeaveAppMstStatus(Convert.ToInt16(strLvAppID), strEmpID, "D");

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
            string strVPath = AppURL.Trim();
            strSubject = "Leave Regreted as requested.";
            strBody = "Your leave application is Regreted. Change your application and re apply."
                    + " <br /><br /> "
                    + "Thanks, "
                    + " <br /> "
                    + strApvBy
                    + " <br /> "
                    + "===================================================="
                    + " <br /> "
                    + "Leave Details:"
                    + " <br /> "
                    + "--------------"
                    + " <br /> "
                    + "Leave Applicant: " + dtToEmp.Rows[0]["FullName"].ToString() + ", "
                    + dtToEmp.Rows[0]["JobTitle"].ToString() + ", " + dtToEmp.Rows[0]["PostingPlaceName"].ToString()
                    + " <br /> "
                    + "Date of Application:  " + dtLeaveApp.Rows[0]["AppDate"].ToString()
                    + " <br /> "
                    + "Leave type: " + dtLeaveApp.Rows[0]["LTypeTitle"].ToString()
                    + " <br /> "
                    + "From: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveStart"].ToString())
                    + " <br /> "
                    + "To: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveEnd"].ToString())
                    + " <br /> "
                    + "Back in office: " + Common.DisplayDate(ResumeDate.ToShortDateString())
                    + " <br /> "
                    + "Reason for leave: " + dtLeaveApp.Rows[0]["LTreason"].ToString()
                    + " <br /> "
                    + "Contact number: " + dtLeaveApp.Rows[0]["PhoneNo"].ToString()
                    + " <br /> "
                    + "===================================================="
                    + " <br /><br /> ";
        }
        try
        {
            this.SendMailLeave(strFromAddr, strToAddr, strSubject, strBody, "");
        }
        catch
        {
            strErrText = "Mail is not send. Please configure the Internet.";
        }
      
        dtToEmp.Rows.Clear();
        dtToEmp.Dispose();

        return strErrText;
    }

    // Leave Regret Mail 
    public string LeaveRegretBySupervisor(string strEmpID, string strLvAppID, string strUserEmpId, string strUserName,
        string strDesig, string strLocation, string strIsSysAdmin, string strUserEmail)
    {
        //Requesting Employee Info
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
        dtLeaveApp = objLeaveMgr.SelectLeaveAppMstStatus(Convert.ToInt16(strLvAppID), strEmpID, "D");

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
            string strVPath = AppURL.Trim();
            strSubject = "Leave Regreted as requested.";
            strBody = "Your leave application is Regreted. Change your application and re apply."
                    + " <br /><br /> "
                    + "Thanks, "
                    + " <br /> "
                    + strApvBy
                    + " <br /> "
                    + "===================================================="
                    + " <br /> "
                    + "Leave Details:"
                    + " <br /> "
                    + "--------------"
                    + " <br /> "
                    + "Leave Applicant: " + dtToEmp.Rows[0]["FullName"].ToString() + ", "
                    + dtToEmp.Rows[0]["JobTitle"].ToString() + ", " + dtToEmp.Rows[0]["PostingPlaceName"].ToString()
                    + " <br /> "
                    + "Date of Application:  " + dtLeaveApp.Rows[0]["AppDate"].ToString()
                    + " <br /> "
                    + "Leave type: " + dtLeaveApp.Rows[0]["LTypeTitle"].ToString()
                    + " <br /> "
                    + "From: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveStart"].ToString())
                    + " <br /> "
                    + "To: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveEnd"].ToString())
                    + " <br /> "
                    + "Back in office: " + Common.DisplayDate(ResumeDate.ToShortDateString())
                    + " <br /> "
                    + "Reason for leave: " + dtLeaveApp.Rows[0]["LTreason"].ToString()
                    + " <br /> "
                    + "Contact number: " + dtLeaveApp.Rows[0]["PhoneNo"].ToString()
                    + " <br /> "
                    + "===================================================="
                    + " <br /><br /> ";
        }
        try
        {
            this.SendMailLeave(strFromAddr, strToAddr, strSubject, strBody, "");
        }
        catch
        {
            strErrText = "Mail is not send. Please configure the Internet.";
        }


        //dtToAdd.Rows.Clear();
        //dtToAdd.Dispose();

        dtToEmp.Rows.Clear();
        dtToEmp.Dispose();

        return strErrText;
    }

    // Leave Cancel Mail 
    public string LeaveCancel(string strEmpID, string strLvAppID, string strUserEmpId, string strUserName,
        string strDesig, string strLocation, string strIsSysAdmin, string strUserEmail)
    {
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
        dtLeaveApp = objLeaveMgr.SelectLeaveAppMstStatus(Convert.ToInt16(strLvAppID), strEmpID, "C");

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
            string strVPath = AppURL.Trim();
            strSubject = "Leave Cancelled as requested.";
            strBody = "Your leave application is Cancelled. "
                    + " <br /><br /> "
                    + "Thanks, "
                    + " <br /> "
                    + strApvBy
                    + " <br /> "
                    + "===================================================="
                    + " <br /> "
                    + "Leave Details:"
                    + " <br /> "
                    + "--------------"
                    + " <br /> "
                    + "Leave Applicant: " + dtToEmp.Rows[0]["FullName"].ToString() + ", "
                    + dtToEmp.Rows[0]["JobTitle"].ToString() + ", " + dtToEmp.Rows[0]["PostingPlaceName"].ToString()
                    + " <br /> "
                    + "Date of Application:  " + dtLeaveApp.Rows[0]["AppDate"].ToString()
                    + " <br /> "
                    + "Leave type: " + dtLeaveApp.Rows[0]["LTypeTitle"].ToString()
                    + " <br /> "
                    + "From: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveStart"].ToString())
                    + " <br /> "
                    + "To: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveEnd"].ToString())
                    + " <br /> "
                    + "Back in office: " + Common.DisplayDate(ResumeDate.ToShortDateString())
                    + " <br /> "
                    + "Reason for leave: " + dtLeaveApp.Rows[0]["LTreason"].ToString()
                    + " <br /> "
                    + "Contact number: " + dtLeaveApp.Rows[0]["PhoneNo"].ToString()
                    + " <br /> "
                    + "===================================================="
                    + " <br /><br /> ";
        }
        try
        {
            this.SendMailLeave(strFromAddr, strToAddr, strSubject, strBody, "");
        }
        catch
        {
            strErrText = "Mail is not send. Please configure the Internet.";
        }


        //dtToAdd.Rows.Clear();
        //dtToAdd.Dispose();

        dtToEmp.Rows.Clear();
        dtToEmp.Dispose();

        return strErrText;
    }

    // Leave Cancel Mail 
    public string LeaveCancelAfterApproval(string strEmpID, string strLvAppID, string strUserEmpId, string strUserName,
        string strDesig, string strLocation, string strIsSysAdmin, string strUserEmail)
    {
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
            string strVPath = AppURL.Trim();
            strSubject = "Leave Cancelled as requested.";
            strBody = "Your leave application is Cancelled. "
                    + " <br /><br /> "
                    + "Thanks, "
                    + " <br /> "
                    + strApvBy
                    + " <br /> "
                    + "===================================================="
                    + " <br /> "
                    + "Leave Details:"
                    + " <br /> "
                    + "--------------"
                    + " <br /> "
                    + "Leave Applicant: " + dtToEmp.Rows[0]["FullName"].ToString() + ", "
                    + dtToEmp.Rows[0]["JobTitle"].ToString() + ", " + dtToEmp.Rows[0]["PostingPlaceName"].ToString()
                    + " <br /> "
                    + "Date of Application:  " + dtLeaveApp.Rows[0]["AppDate"].ToString()
                    + " <br /> "
                    + "Leave type: " + dtLeaveApp.Rows[0]["LTypeTitle"].ToString()
                    + " <br /> "
                    + "From: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveStart"].ToString())
                    + " <br /> "
                    + "To: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveEnd"].ToString())
                    + " <br /> "
                    + "Back in office: " + Common.DisplayDate(ResumeDate.ToShortDateString())
                    + " <br /> "
                    + "Reason for leave: " + dtLeaveApp.Rows[0]["LTreason"].ToString()
                    + " <br /> "
                    + "Contact number: " + dtLeaveApp.Rows[0]["PhoneNo"].ToString()
                    + " <br /> "
                    + "===================================================="
                    + " <br /><br /> ";
        }
        try
        {
            this.SendMailLeave(strFromAddr, strToAddr, strSubject, strBody, "");
        }
        catch
        {
            strErrText = "Mail is not send. Please configure the Internet.";
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
        string strCurrName = "";
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

    protected string GetEmployeeEmailMn(string strEmpID)
    {
        string strSQL = "SELECT OfficeEmail FROM EMPINFO WHERE EMPID=@EMPID ";
        SqlCommand cmd = new SqlCommand(strSQL);

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;
        strSQL = objDC.GetScalarVal(cmd);
        return strSQL;
    }




    #region Pending Leave Request Reminder
    public void SendLeavePendingReminder(string[] strSupervisors, GridView grd, string strFromEmail, Label lblMsg,
        string strUserName, string strDesig, string strLocation)
    {
        int i = 0;
        int inSL = 0;
        string strFrom = strFromEmail;
        string strTo = "";
        string strSubject = "Pending Approval Request(s) for Leave";
        string strBody = "";
        string strVPath = AppURL.Trim();
        foreach (string strSPV in strSupervisors)
        {
            strBody = "";
            strTo = "";
            strTo = this.GetEmployeeEmail(strSPV);


            strBody = " <pre style=" + "\"font-size:12px;font-family:Arial;\"" + "> " + "You have following pending leave request(s) for approval." + " </pre>  <br> ";
            strBody = strBody
                            + " <table style=" + "\"width:100%;border:solid 1px gray;border-collapse:collapse;font-size:12px;font-family:Arial;\"" + "> "
                            + " \n "
                            + " <tr> "
                            + " \n "
                            + " <td style=" + "\"width:5%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + " Sl"
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:35%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + " Employee ID and Name "
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + " Application Date "
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:20%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + " Leave Name "
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + " Leave From "
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + " Leave To "
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + " Total Days "
                            + " </td> "
                            + " </tr> ";
            inSL = 1;
            for (i = 0; i < grd.Rows.Count; i++)
            {
                CheckBox chkB = (CheckBox)grd.Rows[i].Cells[12].FindControl("chkBox");
                if (chkB.Checked == true)
                {
                    if (grd.DataKeys[i].Values[13].ToString().Trim() == strSPV)
                    {
                        strBody = strBody
                            + " \n "
                            + " <tr> "
                            + " \n "
                            + " <td style=" + "\"width:5%;border:solid 1px gray;\"" + "> "
                            + inSL
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:35%;border:solid 1px gray;\"" + "> "
                            + grd.Rows[i].Cells[1].Text.Trim()
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;\"" + "> "
                            + grd.Rows[i].Cells[2].Text.Trim()
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:20%;border:solid 1px gray;\"" + "> "
                            + grd.Rows[i].Cells[3].Text.Trim()
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;\"" + "> "
                            + grd.Rows[i].Cells[4].Text.Trim()
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;\"" + "> "
                            + grd.Rows[i].Cells[5].Text.Trim()
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;\"" + "> "
                            + grd.Rows[i].Cells[6].Text.Trim()
                            + " </td> "
                            + " </tr> ";

                        inSL++;
                    }
                }
            }
            strBody = strBody
                + " \n "
                + " </table> "
                + " <br> <br>"
                + " <pre style=" + "\"font-size:12px;font-family:Arial;\"" + "> " + " Click here to login for approval: " + strVPath + "</pre>"
                + " <br> <br> "
                + " <pre style=" + "\"font-size:12px;font-family:Arial;\"" + "> " + " Thanks " + "</pre>"
                + " <br> "
                + " <pre style=" + "\"font-size:12px;font-family:Arial;\"" + "> " + strUserName + "(SysAdmin), " + strDesig + ", " + strLocation + "</pre>";
            // Sending Mail
            try
            {
                this.SendMailLeave(strFromAddr, strToAddr, strSubject, strBody, "");
            }
            catch
            {
                strErrText = "Mail is not send. Please configure the internet.";
            }
        }
        lblMsg.Text = "Reminder has been successfully send to respected supervisor of selected leave request.";
    }
    # endregion

    #region TimeSheet
    public string TSRequestForApproval(string strEmpID, string strUserEmpId, string strUserName, string strDesig, string strLocation, string strIsSysAdmin,
        string strSpvID, string strSpvEmail, string strMonth, string strYear)
    {
        //MailServer = ConfigurationManager.AppSettings["MyMailServer"].ToString();
        //MailPort = Convert.ToInt32(ConfigurationManager.AppSettings["MyMailServerPort"]);

        strErrText = "Time Sheet request for approval is mailed to the supervisor";
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

        //dtLeaveApp = objLeaveMgr.SelectRequestLeaveAppMst(Convert.ToInt16(strLvAppID), strEmpID, "R", strLvPackStartDate, strLvPackEndDate, "");

        //// Get COPY TO EMAIL Address
        //string strCopyToName = "";
        //string strCopyAddr = "";
        //DataTable dtLvCopyTo = new DataTable();
        //LeaveApplicationManager objLvMgr = new LeaveApplicationManager();
        //dtLvCopyTo = objLvMgr.SelectLeaveCopyTo(strLvAppID);
        //foreach (DataRow dRow in dtLvCopyTo.Rows)
        //{
        //    if (strCopyToName == "")
        //    {
        //        strCopyToName = dRow["SPVFULLNAME"].ToString();
        //        strCopyAddr = dRow["CopyToEmail"].ToString();
        //    }
        //    else
        //    {
        //        strCopyToName = strCopyToName + ", " + dRow["SPVFULLNAME"].ToString();
        //        strCopyAddr = strCopyAddr + ";" + dRow["CopyToEmail"].ToString();
        //    }

        //}

        //if (dtLeaveApp.Rows.Count > 0)
        //{

        //DateTime ResumeDate = Convert.ToDateTime(dtLeaveApp.Rows[0]["ResumeDate"].ToString());
        // Holiday and Weekend Issue Exist. Need to Solve
        // ResumeDate = ResumeDate.AddDays(1);
        string strVPath = AppURL.Trim();
        strSubject = "Request for approving Time Sheet.";
        strBody = " Time Sheet Record: " + dtFromEmp.Rows[0]["FullName"].ToString() + ", "
                + dtFromEmp.Rows[0]["JobTitle"].ToString() + ", " + dtFromEmp.Rows[0]["PostingPlaceName"].ToString()
                + " \n\n "
                + "Request forwarded by: " + strFwdBy
                + " \n\n "
                + " \n\n "
                + "Please approve the Time Sheet of " + strMonth + "," + strYear
               + " \n "
                + "With thanks "
               + " \n\n "
                + dtFromEmp.Rows[0]["FullName"].ToString()
              + " \n "
                + dtFromEmp.Rows[0]["JobTitle"].ToString()
              + " \n "

               + "======================================"
               + " \n\n "
               + " Click here to login for approval: " + strVPath;
        //}
        try
        {
            this.SendMailLeave(strFromAddr, strToAddr, strSubject, strBody, "");
        }
        catch
        {
            strErrText = "Mail is not send. Please configure the internet.";
        }

        //dtToAdd.Rows.Clear();
        //dtToAdd.Dispose();

        dtFromEmp.Rows.Clear();
        dtFromEmp.Dispose();

        return strErrText;
    }
    #endregion

    #region Contract Extension Reminder By MN

    public string ContractExtensionReminder(string empId, string empName, string supervisorId, string senderId, string designation, string contractEndDate, Label lblContractExtension)
    {


       // lblContractExtension.Text = "Email has been sent to the supervisor of this Employee";
        // Requesting Employee Info
        string strFwdBy = "";
        string strCC = "";
        string strToAddr = "";
        string strDriverCell = "";
        string strSupervisorName="";
        string strGender = "";
        string genderPC = "";
        string strJoiningDate = "";
        string strContractEndDate = "";
        string strSeparateDate = "";

        DataTable dtFromEmp = new DataTable();
        dtFromEmp = objEmpInfoMgr.SelectEmpEmailAddress(senderId);//mn

        if (dtFromEmp.Rows.Count > 0)
        {
            strFromAddr = dtFromEmp.Rows[0]["OfficeEmail"].ToString().Trim();
            //strToEmpId = dtFromEmp.Rows[0]["reportingTo"].ToString().Trim();
            strToEmpId = supervisorId;
        }
        else
        {
            strFromAddr = SystemEmail;
            strToEmpId = supervisorId;
        }


        DataTable dtToEmp = new DataTable();
        dtToEmp = objEmpInfoMgr.SelectEmpEmailAddress(empId);//mn
        if (dtToEmp.Rows.Count > 0)
        {
            strToAddr = dtToEmp.Rows[0]["ESOfficeEmail"].ToString().Trim();//[0] for first row and ["PersEmail"] column 
            // or strToAddr = dtApp.Rows[0].ItemArray["OfficeEmail"].ToString().Trim();
            if (dtToEmp.Rows[0]["Supervisor"].ToString().Trim() != "")
                strSupervisorName = dtToEmp.Rows[0]["Supervisor"].ToString().Trim();
            strJoiningDate = Convert.ToString(Common.DisplayDate(dtToEmp.Rows[0]["JoiningDate"].ToString().Trim()));
            strSeparateDate = Convert.ToString(Common.DisplayDate(dtToEmp.Rows[0]["SeparateDate"].ToString().Trim()));
            strGender = dtToEmp.Rows[0]["EmpGender"].ToString().Trim();
            if (strGender == "M")
                genderPC = "his";
            else if (strGender == "F")
                genderPC = "her";
            else
                genderPC = "";
        }
        

     //   string strVPath = AppURL.Trim();

        strSubject = "Contract Extension Date Reminder";


        strBody = " Dear " + strSupervisorName + ", "
                + " \n "
                +"<br>"
                + "Please be informed that, the below mentioned employee is going to complete " + genderPC + " contract period in the "
                + "\n below specified date-"
                 + "<table style=" + "\"width:100%;border:solid 1px gray;border-collapse:collapse;font-size:12px;font-family:Arial;\"" + "> "
                  + " <tr> "
                            + " \n "
                            + " <td style=" + "\"width:5%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + " Employee ID"
                            + " </td> "
                            + " \n "                           
                            + " <td style=" + "\"width:10%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + "Name "
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:20%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + " Designation "
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + " Joining Date "
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + " Contract End Date"
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + " Separation Date "
                            + " </td> "
                            + " </tr> "

                            + " <tr> "
                            + " \n "
                            + " <td style=" + "\"width:5%;border:solid 1px gray;font-weight:normal;\"" + "> "
                            + empId
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;font-weight:normal;\"" + "> "
                            + empName
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:20%;border:solid 1px gray;font-weight:normal;\"" + "> "
                            + designation
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;font-weight:normal;\"" + "> "
                            + strJoiningDate
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;font-weight:normal;\"" + "> "
                            + 858
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;font-weight:normal;\"" + "> "
                            + strSeparateDate
                            + " </td> "
                            + " </tr> "
                 +"</table>"   
                 +"<br>"
                + "\n You are requested to take necessary steps ASAP. If you don’t extend the contract then please send the "
                + "\n signed contract appraisal to me. In case of no extension please ask your supervisee to submit the exit "
                + "\n interview and exit clearance to HR before leaving SC."
                + " \n\n "
                + " <br> <br>"
                //+ "Thanks and regards" + " <br> " + dtFromEmp.Rows[0]["FullName"].ToString() + ", "
                //+ "<br>"
                //+ dtFromEmp.Rows[0]["JobTitle"].ToString() + ", "
                //+ "<br>"
                //+ dtFromEmp.Rows[0]["DeptName"].ToString()
                + " \n ";     
                   
        try
        {
            if (strFromAddr != "" & strToAddr != "")
            {
                SmtpClient MySmtpClient = new SmtpClient(MailServer, MailPort);
                //MySmtpClient.UseDefaultCredentials = true;
                ////Omit these 2 line
                MySmtpClient.EnableSsl = true;
                MySmtpClient.Credentials = new System.Net.NetworkCredential("mamunek56@gmail.com", "");
                System.Net.Mail.MailMessage objMsg = new System.Net.Mail.MailMessage(SystemEmail, strToAddr, strSubject, strBody);
                //CC (Authorizer)
                objMsg.CC.Add(strFromAddr);
                objMsg.IsBodyHtml = true;
                //Sending email
                MySmtpClient.Send(objMsg);
                lblContractExtension.Text = "Email has been sent. Please Contract with the administrator.";
            }
            else
            lblContractExtension.Text = "Email has not been sent. Please Contract with the administrator."; 
        }
        catch
        {
            lblContractExtension.Text = "Email has not been sent. Please Contract with the administrator.";
        }


     

        dtFromEmp.Rows.Clear();
        dtFromEmp.Dispose();

        return lblContractExtension.Text;
    }

    public void SendContractExtensionReminder(string[] strSupervisors, GridView grd, string strFromEmail, Label lblMsg,string strUserName, string strDesig,
        string strLocation) 
    
    {

        int i = 0;
        int inSL = 0;
        string strFrom = SystemEmail;//"mamunek56@gmail.com";//strFromEmail;
        string strTo = "";
        string strSubject = "Contract Extension Date Reminder";
        string strBody = "";
        string strVPath = AppURL.Trim();
        foreach (string strSPV in strSupervisors)
        {
            strBody = "";
            strTo = "";
            strTo = this.GetEmployeeEmailMn(strSPV);


            strBody = " <pre style=" + "\"font-size:12px;font-family:Arial;\"" + "> " + "You have following pending leave request(s) for approval." + " </pre>  <br> ";
            strBody = strBody
                            + " <table style=" + "\"width:100%;border:solid 1px gray;border-collapse:collapse;font-size:12px;font-family:Arial;\"" + "> "
                            + " \n "
                            + " <tr> "
                            + " \n "
                            + " <td style=" + "\"width:5%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + " Sl"
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:35%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + " Employee ID"
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + "Name "
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:20%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + " Designation "
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + " Joining Date "
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + " Contract End Date"
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + " Separation Date "
                            + " </td> "
                            + " </tr> ";
            inSL = 1;
            for (i = 0; i < grd.Rows.Count; i++)
            {
                CheckBox chkB = (CheckBox)grd.Rows[i].Cells[0].FindControl("chkBoxCESelect");
                if (chkB.Checked == true)
                {
                    if (grd.Rows[i].Cells[5].Text.Trim() == strSPV)
                    {
                        strBody = strBody
                            + " \n "
                            + " <tr> "
                            + " \n "
                            + " <td style=" + "\"width:5%;border:solid 1px gray;\"" + "> "
                            + inSL
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:35%;border:solid 1px gray;\"" + "> "
                            + grd.Rows[i].Cells[1].Text.Trim()
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;\"" + "> "
                            + grd.Rows[i].Cells[2].Text.Trim()
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:20%;border:solid 1px gray;\"" + "> "
                            + grd.Rows[i].Cells[3].Text.Trim()
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;\"" + "> "
                           // + grd.Rows[i].Cells[4].Text.Trim()
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;\"" + "> "
                            + grd.Rows[i].Cells[6].Text.Trim()
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;\"" + "> "
                           // + grd.Rows[i].Cells[2].Text.Trim()
                            + " </td> "
                            + " </tr> ";

                        inSL++;
                    }
                }
            }
            strBody = strBody
                + " \n "
                + " </table> "
                + " <br> <br>"
                + " <pre style=" + "\"font-size:12px;font-family:Arial;\"" + "> " + " Click here to login for approval: " + strVPath + "</pre>"
                + " <br> <br> "
                + " <pre style=" + "\"font-size:12px;font-family:Arial;\"" + "> " + " Thanks " + "</pre>"
                + " <br> "
                + " <pre style=" + "\"font-size:12px;font-family:Arial;\"" + "> " + strUserName + "(SysAdmin), " + strDesig + ", " + strLocation + "</pre>";
            // Sending Mail
            try
            {
               // this.SendMailLeave(strFromAddr, strToAddr, strSubject, strBody, "");

                if (strFrom != "" && strTo != "")
                {
                    SmtpClient MySmtpClient = new SmtpClient(MailServer, MailPort);
                    //MySmtpClient.UseDefaultCredentials = true;
                    ////Omit these 2 line
                    MySmtpClient.EnableSsl = true;
                    MySmtpClient.Credentials = new System.Net.NetworkCredential(
                                                 "mamunek56@gmail.com", "");
                    System.Net.Mail.MailMessage objMsg = new System.Net.Mail.MailMessage(SystemEmail, strTo, strSubject, strBody);

                    //CC (Authorizer)
                   // objMsg.CC.Add(strFromAddr);

                    //Sending email
                    MySmtpClient.Send(objMsg);
                }
            }
            catch
            {
                strErrText = "Mail is not send. Please configure the internet.";
            }
        }
        lblMsg.Text = "Reminder has been successfully sent to the respected supervisor of the Employee.";   
        
    }
    #endregion

#region Confirmation Date Remainder

    public string ConfirmationDateReminder(string empId,string empName,string supervisorId,string senderId,string designation,string ConfirmationDate,Label lblConfirmation)
    {
        // Requesting Employee Info
        string strFwdBy = "";
        string strCC = "";
        string strToAddr = "";
        string strDriverCell = "";
        string strSupervisorName = "";
        string strGender = "";
        string genderPC = "";
        string strJoiningDate = "";
        // string strConfirmationDate= "";
        string strSeparateDate = "";

        DataTable dtFromEmp = new DataTable();
        dtFromEmp = objEmpInfoMgr.SelectEmpEmailAddress(senderId);//mn

        if (dtFromEmp.Rows.Count > 0)
        {
            strFromAddr = dtFromEmp.Rows[0]["OfficeEmail"].ToString().Trim();
            //strToEmpId = dtFromEmp.Rows[0]["reportingTo"].ToString().Trim();
            strToEmpId = supervisorId;
        }
        else
        {
            strFromAddr = SystemEmail;
            strToEmpId = supervisorId;
        }


        DataTable dtToEmp = new DataTable();
        dtToEmp = objEmpInfoMgr.SelectEmpEmailAddress(empId);//mn
        if (dtToEmp.Rows.Count > 0)
        {
            strToAddr = dtToEmp.Rows[0]["ESOfficeEmail"].ToString().Trim();//[0] for first row and ["PersEmail"] column 
            // or strToAddr = dtApp.Rows[0].ItemArray["OfficeEmail"].ToString().Trim();
            if (dtToEmp.Rows[0]["Supervisor"].ToString().Trim() != "")
                strSupervisorName = dtToEmp.Rows[0]["Supervisor"].ToString().Trim();
            strJoiningDate = Convert.ToString(Common.DisplayDate(dtToEmp.Rows[0]["JoiningDate"].ToString().Trim()));
            strSeparateDate = Convert.ToString(Common.DisplayDate(dtToEmp.Rows[0]["SeparateDate"].ToString().Trim()));
            strGender = dtToEmp.Rows[0]["EmpGender"].ToString().Trim();
            if (strGender == "M")
                genderPC = "his";
            else if (strGender == "F")
                genderPC = "her";
            else
                genderPC = "";
        }


        //   string strVPath = AppURL.Trim();

        strSubject = "Confirmation Date Reminder";


        strBody = " Dear " + strSupervisorName + ", "
                + " \n "
                + "<br>"
                + "Please be informed that, the below mentioned employee is going to complete " + genderPC + " probation period in the "
                + "\n below specified date-"
                 + "<table style=" + "\"width:100%;border:solid 1px gray;border-collapse:collapse;font-size:12px;font-family:Arial;\"" + "> "
                  + " <tr> "
                            + " \n "
                            + " <td style=" + "\"width:5%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + " Employee ID"
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + "Name "
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:20%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + " Designation "
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + " Joining Date "
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + " Probation Completion Date"
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;font-weight:bold;\"" + "> "
                            + " Confirmation Date"
                            + " </td> "
                            + " </tr> "

                            + " <tr> "
                            + " \n "
                            + " <td style=" + "\"width:5%;border:solid 1px gray;font-weight:normal;\"" + "> "
                            + empId
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;font-weight:normal;\"" + "> "
                            + empName
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:20%;border:solid 1px gray;font-weight:normal;\"" + "> "
                            + designation
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;font-weight:normal;\"" + "> "
                            + strJoiningDate
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;font-weight:normal;\"" + "> "
                            + strSeparateDate//*** Change Here  for Probation Completion Date***
                            + " </td> "
                            + " \n "
                            + " <td style=" + "\"width:10%;border:solid 1px gray;font-weight:normal;\"" + "> "
                            + ConfirmationDate
                            + " </td> "
                            + " </tr> "
                 + "</table>"
                 + "<br>"
                + "\n You are requested to do the performance assessment as per attached form and send your "
                + "\n recommendation in hard copy at least 03 working days before completion of the probation period. "               
                + " \n\n "
                + " <br> <br>"
                //+ "Thanks and regards" + " <br> " + dtFromEmp.Rows[0]["FullName"].ToString() + ", "
                //+ "<br>"
                //+ dtFromEmp.Rows[0]["JobTitle"].ToString() + ", "
                //+ "<br>"
                //+ dtFromEmp.Rows[0]["DeptName"].ToString()
                + " \n ";

        try
        {
            if (strFromAddr != "" & strToAddr != "")
            {
                SmtpClient MySmtpClient = new SmtpClient(MailServer, MailPort);
                //MySmtpClient.UseDefaultCredentials = true;
                ////Omit these 2 line
                MySmtpClient.EnableSsl = true;
                MySmtpClient.Credentials = new System.Net.NetworkCredential("mamunek56@gmail.com", "");
                System.Net.Mail.MailMessage objMsg = new System.Net.Mail.MailMessage(SystemEmail, strToAddr, strSubject, strBody);
                //CC (Authorizer)
                //objMsg.CC.Add(strFromAddr);
               
                objMsg.IsBodyHtml = true;
                //Sending email
                MySmtpClient.Send(objMsg);
                lblConfirmation.Text = "Email has been sent. Please Contract with the administrator.";
            }
            else
            lblConfirmation.Text = "Email has not been sent. Please Contract with the administrator.";
        }
        catch
        {
            lblConfirmation.Text = "Email has not been sent. Please Contract with the administrator.";
        }
        dtFromEmp.Rows.Clear();
        dtFromEmp.Dispose();

        return lblConfirmation.Text;
    }
#endregion
}
