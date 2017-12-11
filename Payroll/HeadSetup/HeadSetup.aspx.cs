using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Mail;
using System.Net.Mail;
public partial class Payroll_HeadSetup_HeadSetup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //SendViaSMTPClient();
    }

    protected void SendViaSMTPMail()
    {
        string strFromAddr = TextBox1.Text.Trim();//"Debashish.Saha@plan-international.org";
        string strToAddr = TextBox2.Text.Trim();//"Debashish.Saha@plan-international.org";
        string strSubject = "Test Mail for same From and To Address";
        string strBody = "Mail has been reached";

        if (strFromAddr != "" && strToAddr != "")
        {
            System.Web.Mail.MailMessage objMsg = new System.Web.Mail.MailMessage();
            objMsg.From = strFromAddr;
            objMsg.To = strToAddr;
            //objMsg.Cc = strCopyAddr;
            objMsg.Subject = strSubject;
            objMsg.Body = strBody;
            SmtpMail.Send(objMsg);
        }
    }
    protected void SendViaSMTPClient()
    {
        string mTo = TextBox2.Text.Trim();
        string mFrom = TextBox1.Text.Trim();
        string mSubject = "Test Mail for same From and To Address";
        string mMsg = "Mail has been reached";
        string mMailServer = ConfigurationManager.AppSettings["MyMailServer"].ToString();
        int mPort = Convert.ToInt32(ConfigurationManager.AppSettings["MyMailServerPort"].ToString());
        //mCC = Trim(txtCC.Text)
        System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(mFrom, mTo, mSubject, mMsg);
        SmtpClient mySmtpClient = new SmtpClient(mMailServer, mPort);
        mySmtpClient.UseDefaultCredentials = true;
        mySmtpClient.Send(message);
        Label1.Text = "The mail message has been sent";
    }

}
