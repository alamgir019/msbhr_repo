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
using System.Data.SqlClient;
using System.Web.Mail;
using System.Text; 

public partial class Leave_LeaveRecommendation : System.Web.UI.Page
{   
    LeaveManager objLMgr = new LeaveManager();
    LeaveApplicationManager objLeaveMgr = new LeaveApplicationManager();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    MailManagerSmtpClient objMail = new MailManagerSmtpClient();

    DataTable dtLeaveApp = new DataTable();
    DataTable dtEmpInfo=new DataTable ();
    static decimal PreYrLVAvail = 0;
    static double TotWeekedDay = 0;
    static string strStartDate = "";
    static string strEndDate = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.GetRecord();
        }
    }

    protected void GetRecord()
    {
        this.OpenRecord();       
    }

    private void OpenRecord()
    {
        grLeaveApp.DataSource = null;
        grLeaveApp.DataBind();
        dtLeaveApp.Rows.Clear();
        dtLeaveApp.Dispose();

        string strStartYear = DateTime.Now.Year.ToString();
        string strStartMonth = DateTime.Now.Month.ToString();

        if (Convert.ToInt32(strStartMonth) >= 1)
        {
            strStartDate = Convert.ToString(Convert.ToInt32(strStartYear) - 1);
            strEndDate = Convert.ToString(Convert.ToInt32(strStartDate) + 1);
            strStartDate = strStartDate + "-" + "07" + "-" + "01";
            strEndDate = strEndDate + "-" + "12" + "-" + "31";
        }

        dtLeaveApp = objLeaveMgr.SelectRequestLeaveAppMstForHR("P", strStartDate, strEndDate);

        grLeaveApp.DataSource = dtLeaveApp;
        grLeaveApp.DataBind();
        this.FormatGridDate();
        dtLeaveApp.Dispose();
    }

    protected void FormatGridDate()
    {
        int SlNo = 0;
        foreach (GridViewRow gRow in grLeaveApp.Rows)
        {
            SlNo = SlNo + 1;
            gRow.Cells[0].Text = SlNo.ToString();
            gRow.Cells[1].Text = gRow.Cells[1].Text.ToUpper() + " [" + grLeaveApp.DataKeys[SlNo - 1].Values[12].ToString() + "]";
            gRow.Cells[2].Text = Common.DisplayDate(gRow.Cells[2].Text);
            gRow.Cells[4].Text = Common.DisplayDate(gRow.Cells[4].Text);
            gRow.Cells[5].Text = Common.DisplayDate(gRow.Cells[5].Text);
            gRow.Cells[6].Text = Convert.ToString(Math.Round(Convert.ToDouble(gRow.Cells[6].Text), 1));
        }
        SlNo = 0;
    }   

    private void AvailableLeave(string gridStatus, string strEmpID, string strLTypeID)
    {
        DataTable dtLeaveProfile = new DataTable();
        if (gridStatus == "A")
        {
            dtLeaveProfile = objLeaveMgr.SelectEmpLeaveProfile(strEmpID, strLTypeID);
            if (dtLeaveProfile.Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["LeaveEnjoyed"].ToString()) == false)
                    hfLEnjoyed.Value = Convert.ToString(Convert.ToDecimal(dtLeaveProfile.Rows[0]["LeaveEnjoyed"].ToString()) + Convert.ToDecimal(grLeaveApp.SelectedRow.Cells[6].Text.Trim()));
                else
                    hfLEnjoyed.Value = grLeaveApp.SelectedRow.Cells[6].Text.Trim();
            }
        }        
    }

    protected void grLeaveApp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        string strPreYrLv = "";
        switch (_commandName)
        {
            case ("ViewClick"):
                //Open New Window
                StringBuilder sb = new StringBuilder();
                string strURL = "LeaveApplicationRpt.aspx?params=" + grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[11].ToString() + "," + grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim() + ", X";
                sb.Append("<script>");
                //sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
                sb.Append("window.open('" + strURL + "', '', '');");
                sb.Append("</script>");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                         sb.ToString(), false);
                ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
                break;

            case ("RecommendClick"):
                string strApprovedBy = "";
                TextBox txtApprovedBy = (TextBox)(grLeaveApp.SelectedRow.Cells[1].FindControl("txtApproverId"));
                strApprovedBy = txtApprovedBy.Text.Trim ();

                dtEmpInfo = objEmpInfoMgr.SelectEmpInfoHRAction(strApprovedBy);
                if (dtEmpInfo.Rows.Count == 0)
                {
                    lblMsg.Text = "Invalid Employee No.";
                    return;
                }
                else
                    lblMsg.Text = "";
                
                objLeaveMgr.UpdateLeaveAppMstForRecommendation(grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[0].ToString(),
                    grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim(), "R", strApprovedBy,Session["USERID"].ToString(),
                     Common.SetDateTime(DateTime.Now.ToString()));

                //////Email Notification
                ////lblMsg.Text = objMail.LeaveApprovalByHR(grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim(),
                ////    grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[0].ToString(), Session["EMPID"].ToString(), 
                ////    Session["USERNAME"].ToString(), Session["DESIGNATION"].ToString(), Session["LOCATION"].ToString(),
                ////      Session["USERID"].ToString().Trim().ToUpper() == "ADMIN" ? "Y" : "N", Session["EMAILID"].ToString());
                if (lblMsg.Text == "")
                    lblMsg.Text = "Leave has been recommended successfully." ;// and mailed successfully";
                break;

            case ("DenyClick"):
                objLeaveMgr.UpdateLeaveAppMstForDeny(grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[0].ToString(),
                    grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim(), "Y", "N", "D",
                    Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));

                //Email Notification
                //lblMsg.Text = objMail.LeaveRegretByHR(grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim(),
                //    grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[0].ToString(), Session["EMPID"].ToString(),
                //    Session["USERNAME"].ToString(), Session["DESIGNATION"].ToString(), Session["LOCATION"].ToString(),
                //      Session["USERID"].ToString().Trim().ToUpper() == "ADMIN" ? "Y" : "N", Session["EMAILID"].ToString());
                if (lblMsg.Text == "")
                    lblMsg.Text = "Leave has been regreted successfully.";// and Mailed Successfully";
                break;          
        }

        this.GetRecord();
        strPreYrLv = "";
    }


    protected void CalculateLeaveDates(string gridStatus, string strDateFrom, string strDateTo)
    {
        double TotDay = 0;
        DateTime dtFrom = new DateTime();
        DateTime dtTo = new DateTime();

        string strFromDate = "";
        string strToDate = "";
        if (gridStatus == "A")
        {
            strFromDate = strDateFrom;// grLeaveApp.SelectedRow.Cells[4].Text;
            strToDate = strDateTo;// grLeaveApp.SelectedRow.Cells[5].Text;
        }
        else if (gridStatus == "D")
        {
            strFromDate = strDateFrom;// grLeaveDeny.SelectedRow.Cells[4].Text;
            strToDate = strDateTo;// grLeaveDeny.SelectedRow.Cells[5].Text;
        }
        else if (gridStatus == "AC")
        {
            strFromDate = strDateFrom;// grLeaveApprove.SelectedRow.Cells[4].Text;
            strToDate = strDateTo;// grLeaveApprove.SelectedRow.Cells[5].Text;
        }

        if (string.IsNullOrEmpty(strFromDate) == false
            && string.IsNullOrEmpty(strToDate) == false)
        {
            char[] splitter = { '/' };
            string[] arinfo = Common.str_split(strFromDate, splitter);
            if (arinfo.Length == 3)
            {
                dtFrom = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                arinfo = null;
            }
            arinfo = Common.str_split(strToDate, splitter);
            if (arinfo.Length == 3)
            {
                dtTo = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                arinfo = null;
            }
            TimeSpan Dur = dtTo.Subtract(dtFrom);
            TotDay = Math.Round(Convert.ToDouble(Dur.Days), 0) + 1;
        }
        DateTime LDate = dtFrom;
    }
   
    protected void grLeaveApp_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
