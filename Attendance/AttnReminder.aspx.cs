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
using System.Text; 

public partial class AttnReminder : System.Web.UI.Page
{
    AttnRemindManager objAttnRmdMgr = new AttnRemindManager();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();

    DataTable dtEmpAttn = new DataTable();
    DataTable dtAbsentList;
    DataTable dtEmp;
    string strLeaveRemarks = "";
    string strTravelRemarks = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // this.InitializeDataTableAbsentList();
            rdbRmdType.Checked = true;
            Common.FillDropDownList_Nil(objAttnRmdMgr.SelectLocation(0), ddlOffice);
            this.GetEmpoyeeRecords();
        }
    }

    protected void InitializeDataTableAbsentList()
    {
        dtAbsentList = new DataTable();
        dtAbsentList.Columns.Add("EmpID");
        dtAbsentList.Columns.Add("EMPNAME");
        dtAbsentList.Columns.Add("AbsentDates");
        dtAbsentList.Columns.Add("LeaveRemarks");
        dtAbsentList.Columns.Add("TravelRemarks");
        dtAbsentList.Columns.Add("EmailID");
        dtAbsentList.AcceptChanges();
    }
    protected void GetEmpoyeeRecords()
    {
        if (Session["USERID"].ToString().Trim().ToUpper() != "ADMIN")
        {
           // dtEmp = objAttnRmdMgr.SelectSupervisorWiseEmp(Session["EMPID"].ToString().Trim());
            ddlOffice.Enabled = false;
            lblOffice.Visible = false;
            grEmpList.DataSource = objEmpInfoMgr.GetSuperVisiorWiseEmp(Session["EMPID"].ToString().ToUpper(), Session["OFFICEID"].ToString());
            grEmpList.DataBind();
        }
        else
        {
            dtEmp = objAttnRmdMgr.SelectLocationWiseEmp(ddlOffice.SelectedValue.ToString());
            ddlOffice.Enabled = true;
            lblOffice.Visible = true;
            grEmpList.DataSource = dtEmp;
            grEmpList.DataBind();
        }

       
    }


    

    protected bool GetEmployeeAbsentRecords()
    {
        this.InitializeDataTableAbsentList();
        string strEmpIDs = "";
        string strLvRemarks = "";
        int i=0;
        DataTable dtPendingLV = objAttnRmdMgr.GetPendingLeaveRecord(Common.ReturnDate(txtFrom.Text.Trim()), Common.ReturnDate(txtTo.Text.Trim()));
        DataTable dtPendingTV = objAttnRmdMgr.GetPendingTravelRecord(Common.ReturnDate(txtFrom.Text.Trim()), Common.ReturnDate(txtTo.Text.Trim()));
        DataRow[] foundPendingLv;
        DataRow[] foundPendingTv;
        foreach (GridViewRow gRow in grEmpList.Rows)
        {
            CheckBox chk = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            if (chk.Checked == true)
            {

                if (strEmpIDs == "")
                    strEmpIDs = "'" + grEmpList.DataKeys[i].Values[0].ToString().Trim() + "'";
                else
                    strEmpIDs = strEmpIDs + ",'" + grEmpList.DataKeys[i].Values[0].ToString().Trim() + "'";
                // Adding to Absent List
                DataRow nRow = dtAbsentList.NewRow();
                nRow["EMPID"] = grEmpList.DataKeys[i].Values[0].ToString();
                nRow["EmailID"] = grEmpList.DataKeys[i].Values[2].ToString();
                nRow["EMPNAME"] = gRow.Cells[1].Text.Trim();
                dtAbsentList.Rows.Add(nRow);
                
                // Leave Remarks
                strLeaveRemarks = "";
                foundPendingLv = null;
                foundPendingLv = dtPendingLV.Select("EMPID='" + grEmpList.DataKeys[i].Values[0].ToString().Trim() + "'");
                foreach (DataRow lvRows in foundPendingLv)
                {
                    if (strLeaveRemarks == "")
                    {
                        strLeaveRemarks = Common.DisplayDate(lvRows["LeaveStart"].ToString().Trim()) + "-" + Common.DisplayDate(lvRows["LeaveEnd"].ToString().Trim()) + ": Leave Request Pending";
                    }
                    else
                    {
                        strLeaveRemarks = strLeaveRemarks + " \n " + Common.DisplayDate(lvRows["LeaveStart"].ToString().Trim()) + "-" + Common.DisplayDate(lvRows["LeaveEnd"].ToString().Trim()) + ": Leave Request Pending";
                    }
                }
                nRow["LeaveRemarks"] = strLeaveRemarks;
                //Travel Remarks
                strTravelRemarks = "";
                foundPendingTv = null;
                foundPendingTv = dtPendingTV.Select("EMPID='" + grEmpList.DataKeys[i].Values[0].ToString().Trim() + "'");
                foreach (DataRow tvRows in foundPendingTv)
                {
                    if (strTravelRemarks == "")
                    {
                        strTravelRemarks = Common.DisplayDate(tvRows["DepartureDate"].ToString().Trim()) + "-" + Common.DisplayDate(tvRows["ReturnDate"].ToString().Trim()) + ": Travel Request Pending";
                    }
                    else
                    {
                        strTravelRemarks = strTravelRemarks + " \n " + Common.DisplayDate(tvRows["DepartureDate"].ToString().Trim()) + "-" + Common.DisplayDate(tvRows["ReturnDate"].ToString().Trim()) + ": Travel Request Pending";
                    }
                }
                nRow["TravelRemarks"] = strTravelRemarks;
            }
            i++;
        }
        dtAbsentList.AcceptChanges();

        // Get Employee Wise Absent Records
        if (dtAbsentList.Rows.Count > 0)
        {

            dtEmpAttn = objAttnRmdMgr.GetAbsentRecord(strEmpIDs, Common.ReturnDate(txtFrom.Text.Trim()), Common.ReturnDate(txtTo.Text.Trim()));
            
            if (dtEmpAttn.Rows.Count > 0)
            {

                foreach (DataRow dRow in dtAbsentList.Rows)
                {
                    string strDates = "";
                    DataRow[] foundRows = dtEmpAttn.Select("EMPID='" + dRow["EMPID"].ToString() + "'");
                    
                    if (foundRows.Length > 0)
                    {
                        foreach (DataRow dRow2 in foundRows)
                        {
                            if (strDates == "")
                            {
                                strDates = "Date: " + Common.DisplayDate(dRow2["ATTNDDATE"].ToString());
                            }
                            else
                            {
                                strDates = strDates + ", " + " \n Date: " + Common.DisplayDate(dRow2["ATTNDDATE"].ToString());
                            }
                            
                        }
                    }
                    dRow["AbsentDates"] = strDates;
                    foundRows = null;

                }
                dtAbsentList.AcceptChanges();
            }
        }
        else
        {
            lblMsg.Text = "No employee has been selected";
            return false;
        }
        return true;

    }

    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.GetEmpoyeeRecords();
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {

        if (this.GetEmployeeAbsentRecords() == true)
        {
            lblMsg.Text = objAttnRmdMgr.SendReminderMail(Session["EMPID"].ToString(), Session["USERNAME"].ToString(),
                   Session["DESIGNATION"].ToString(), Session["LOCATION"].ToString(),
                   Session["USERID"].ToString().Trim().ToUpper() == "ADMIN" ? "Y" : "N", Session["EMAILID"].ToString(), dtAbsentList);
            if (lblMsg.Text == "")
            {
                lblMsg.Text = "Reminder has been sent to selected employee(s).";
            }
        }
    }


    protected void grEmpList_RowCommand(object sender, GridViewCommandEventArgs e)
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
                string strURL = "AttendanceViewerRpt.aspx?params=" + grEmpList.SelectedRow.Cells[2].Text.Trim() + "," + Common.ReturnDate(txtFrom.Text.Trim()) + ", " + Common.ReturnDate(txtTo.Text.Trim());
                sb.Append("<script>");
                //sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
                sb.Append("window.open('" + strURL + "', '', '');");
                sb.Append("</script>");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                         sb.ToString(), false);
                ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
                break;
           
        }
    }

    //private string GetPendingLeaveRecords(string strEmpID, string strDate)
    //{

    //}
}
