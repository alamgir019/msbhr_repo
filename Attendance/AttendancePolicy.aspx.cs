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

public partial class Attendance_AttendancePolicy : System.Web.UI.Page
{
    AttnPolicyTableManager objAttnMgr = new AttnPolicyTableManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           
            this.TabContainer1.ActiveTabIndex = 0;
            this.TabContainer1.TabIndex = 0;
            this.EntryMode(false);
            this.OpenRecord();
        }
    }

    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
        {
            btnSave.Text = "Update";
            hfIsUpdate.Value = "Y";
        }
        else
        {
            btnSave.Text = "Save";
            hfIsUpdate.Value = "N";

            txtOTGrace.Text = "0";
            txtArrGrace.Text = "0";
            txtLunch.Text = "0";
            txtWorking.Text = "0";
        }
    }   

    private void OpenRecord()
    {
        DataTable dtData=new DataTable();
        grAttnPolicy.DataSource = null;
        grAttnPolicy.DataBind();
        dtData=objAttnMgr.GetDataSBUwise("0");
        grAttnPolicy.DataSource = dtData;
        grAttnPolicy.DataBind();
        FormatGridDate();
    }
    public void FormatGridDate()
    {
        foreach (GridViewRow gRow in grAttnPolicy.Rows)
        {
            gRow.Cells[5].Text = Common.DisplayTime(gRow.Cells[5].Text);
            gRow.Cells[6].Text = Common.DisplayTime(gRow.Cells[6].Text);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
        {
            this.SaveData("N");
        }
        this.TabContainer1.ActiveTabIndex = 0;
        this.TabContainer1.TabIndex = 0;
    }

    protected bool ValidateAndSave()
    {
        try
        {            
            if (chkIsDefault.Checked == true)
            {
               
                    DataTable dtAttnP = new DataTable();
                    dtAttnP = objAttnMgr.GetData("0");
                    if (dtAttnP.Rows.Count > 0)
                    {
                        DataRow[] foundRows;
                        string strExpr = "";
                        strExpr = "IsDefault='Y' " + " AND AttnPolicyId <> " + ((hfAttnPolicyId.Value=="") ?"0":hfAttnPolicyId.Value);
                        foundRows = dtAttnP.Select(strExpr);
                        if (foundRows.Length > 0)
                        {
                            lblMsg.Text = "General shift is already assigned.";
                            return false;
                        }
                    }
               
            }

            if (chkIsDefault.Checked == true && chkIsNextDay.Checked == true)
            {
                lblMsg.Text = "General shift can not be assigned as next day. Please unchceked any one checkbox.";
                return false; 
            }
            return true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    private void SaveData(string IsDelete)
    {
        try
        {
            string strAttnPolicyId = "";
            string IsUpdate = hfIsUpdate.Value;
        
            string strInTime = ddlArrivalHour.SelectedValue.ToString() + ":" + ddlArrivalMin.SelectedValue.ToString() + ":00 " ;
            string strOutTime = ddlDeptHour.SelectedValue.ToString() + ":" + ddlDeptMin.SelectedValue.ToString() + ":00 ";
            string strLunchTime = "";
            if (chkIsLunchTime.Checked == true)
                strLunchTime = ddlLunchHour.SelectedValue.ToString() + ":" + ddlLunchMin.SelectedValue.ToString() + ":00 ";
            else
                strLunchTime = "";
            string strIsNextDay = chkIsNextDay.Checked == true ? "Y" : "N";
            string strIsActive = chkInActive.Checked == true ? "N" : "Y";
            string strIsDefault = chkIsDefault.Checked == true ? "Y" : "N";
            if ((IsUpdate == "N") && (IsDelete == "N"))            
                strAttnPolicyId = Common.getMaxId("AttdnPolicy", "AttnPolicyId");            
            else            
                strAttnPolicyId = hfAttnPolicyId.Value;            

            AttendancePolicy objAttn = new AttendancePolicy(strAttnPolicyId, txtPolicyTitle.Text.Trim(), txtPolicyDesc.Text.Trim(), txtOTGrace.Text.Trim(), txtArrGrace.Text.Trim(),
                txtLunch.Text.Trim(), strInTime, strOutTime, strIsNextDay, strIsActive, strIsDefault, Session["USERID"].ToString(), Common.SetDate(DateTime.Today.ToString()), strLunchTime, txtWorking.Text.Trim(),
                "","");

            objAttnMgr.InsertAttnPolicy(IsUpdate, IsDelete, objAttn);

            if ((IsUpdate == "N") && (IsDelete == "N"))
                lblMsg.Text = "Record Saved Successfully";
            else if ((IsUpdate == "Y") && (IsDelete == "N"))
                lblMsg.Text = "Record Updated Successfully";
            else if ((IsUpdate == "Y") && (IsDelete == "Y"))
                lblMsg.Text = "Record Deleted Successfully";
            Common.EmptyTextBoxValues(this);
            this.EntryMode(false);
            this.OpenRecord();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }
   
    protected void grAttnPolicy_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        string[] arInfo = new string[3];
        switch (_commandName)
        {
            case ("DoubleClick"):
                hfAttnPolicyId.Value = grAttnPolicy.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                txtPolicyTitle.Text = grAttnPolicy.SelectedRow.Cells[1].Text;
                txtOTGrace.Text = grAttnPolicy.SelectedRow.Cells[2].Text;
                txtArrGrace.Text = grAttnPolicy.SelectedRow.Cells[3].Text;
                txtLunch.Text = grAttnPolicy.SelectedRow.Cells[4].Text;

                HiddenField hfDesc = new HiddenField();
                hfDesc = (HiddenField)grAttnPolicy.SelectedRow.Cells[9].FindControl("hfDescription");
                txtPolicyDesc.Text = hfDesc.Value;

                HiddenField hfWHr = new HiddenField();
                hfWHr = (HiddenField)grAttnPolicy.SelectedRow.Cells[9].FindControl("hfWorkingHr");
                txtWorking.Text = hfWHr.Value;

                arInfo = Common.SpllitedTime(grAttnPolicy.SelectedRow.Cells[5].Text);
                if (arInfo[2].ToUpper() == "AM")
                {
                    if (arInfo[0] == "12")
                    {
                        ddlArrivalHour.SelectedValue = "00";
                    }
                    else
                    {
                        ddlArrivalHour.SelectedValue = arInfo[0];
                    }

                }
                else if (arInfo[2].ToUpper() == "PM")
                    ddlArrivalHour.SelectedValue = Convert.ToString(Convert.ToInt32(arInfo[0]) + 12);
                ddlArrivalMin.SelectedValue = arInfo[1];                
                arInfo = null;

                arInfo = Common.SpllitedTime(grAttnPolicy.SelectedRow.Cells[6].Text);
                if (arInfo[2].ToUpper() == "AM")
                {
                    if (arInfo[0] == "12")
                    {
                        ddlDeptHour.SelectedValue = "00";
                    }
                    else
                    {
                        ddlDeptHour.SelectedValue = arInfo[0];
                    }
                }
                else if (arInfo[2].ToUpper() == "PM")
                    ddlDeptHour.SelectedValue = Convert.ToString(Convert.ToInt32(arInfo[0]) + 12);
                ddlDeptMin.SelectedValue = arInfo[1];                
                arInfo = null;

                HiddenField hfLTime = new HiddenField();
                hfLTime = (HiddenField)grAttnPolicy.SelectedRow.Cells[9].FindControl("hfLunchTime");
                if (string.IsNullOrEmpty(hfLTime.Value.ToString()) == false)
                {
                    hfLTime.Value = Common.DisplayTime(hfLTime.Value);
                    arInfo = Common.SpllitedTime(hfLTime.Value);
                    if (arInfo[2].ToUpper() == "AM")
                    {
                        if (arInfo[0] == "12")
                        {
                            ddlLunchHour.SelectedValue = "00";
                        }
                        else
                        {
                            ddlLunchHour.SelectedValue = arInfo[0];
                        }
                    }
                    else if (arInfo[2].ToUpper() == "PM")
                        ddlLunchHour.SelectedValue = Convert.ToString(Convert.ToInt32(arInfo[0]) + 12);   
                    ddlLunchMin.SelectedValue = arInfo[1];                    
                    chkIsLunchTime.Checked = true;
                    arInfo = null;
                }
                else
                {
                    ddlLunchHour.SelectedIndex = 0;
                    ddlLunchMin.SelectedIndex = 0;
                    //ddlLunchAMPM.SelectedIndex = 0;
                    chkIsLunchTime.Checked = false;
                }
                chkInActive.Checked = grAttnPolicy.SelectedRow.Cells[7].Text == "Y" ? false : true;
                chkIsDefault.Checked = grAttnPolicy.SelectedRow.Cells[8].Text == "Y" ? true : false;
                HiddenField hfIsNDay = new HiddenField();
                hfIsNDay = (HiddenField)grAttnPolicy.SelectedRow.Cells[9].FindControl("hfIsNextDay");
                chkIsNextDay.Checked = hfIsNDay.Value == "Y" ? true : false;
                this.TabContainer1.ActiveTabIndex = 0;
                this.TabContainer1.TabIndex = 0;
                this.EntryMode(true);
                break;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        this.SaveData("Y");
        this.TabContainer1.ActiveTabIndex = 0;
        this.TabContainer1.TabIndex = 0;
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
    }
    protected void grAttnPolicy_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
