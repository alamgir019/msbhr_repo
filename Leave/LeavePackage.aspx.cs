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

public partial class Attendance_LeavePackage : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    // MasterTablesManager objMasMgr = new MasterTablesManager();
    LeaveManager objLeaveManager = new LeaveManager();
    DataTable dtLeaveList = new DataTable();
    DataTable dtLeavePakMst = new DataTable();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    static string strStartDate = "";
    static string strEndDate = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
            this.EntryMode(false);
            this.OpenRecord();
            Common.FillDropDownList_Nil(objMasMgr.SelectEmpType(0,"Y"), ddlEmploymentType); 
            this.TabContainer1.ActiveTabIndex = 0;
            this.TabContainer1.TabIndex = 0;
        }
    }

    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
        {
            btnSave.Text = "Update";
            hfIsUpdate.Value = "Y";
            lblMsg.Text = ""; 
        }

        else
        {
            btnSave.Text = "Save";
            hfIsUpdate.Value = "N";
        }
    }
    private void OpenRecord()
    {
        dtLeaveList.Rows.Clear();
        dtLeaveList = objLeaveManager.SelectLeaveType(0);
        grLeaveList.DataSource = dtLeaveList;
        grLeaveList.DataBind();
        dtLeavePakMst.Rows.Clear();

        dtLeavePakMst = objLeaveManager.SelectLeavePakMst(0);
        grLeavePakMst.DataSource = dtLeavePakMst;
        grLeavePakMst.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
        {
            this.SaveData("N");
        }
    }

    protected bool ValidateAndSave()
    {
        try
        {
            //Leave Start & End Period Selection                 
            this.GetLeavePeriod(ddlMonthFrom.SelectedValue.ToString(), ddlMonthTo.SelectedValue.ToString());
            Int32 iMonthDiff = 0;
            iMonthDiff = (Convert.ToInt32(ddlMonthTo.SelectedValue.ToString()) - Convert.ToInt32(ddlMonthFrom.SelectedValue.ToString()));
            if (iMonthDiff < 0)
                iMonthDiff = iMonthDiff + 12;
            int i = 0;
            foreach (GridViewRow gRow in grLeaveList.Rows)
            {
                CheckBox chBox = new CheckBox();
                chBox = (CheckBox)gRow.Cells[0].FindControl("chkSelect");
                if (chBox.Checked == true)
                {
                    DataTable dtLvTyCarryOverNature = objLeaveManager.GetLvTyCarryOverNature(grLeaveList.DataKeys[i].Values[0].ToString());
                    if ((dtLvTyCarryOverNature.Rows.Count > 0) && (iMonthDiff != 11))
                    {
                        lblMsg.Text = gRow.Cells[1].Text +  " can not assign for this package. Because carry over type leave can assign only for 1 year package.";
                        return false;
                    }
                    dtLvTyCarryOverNature.Rows.Clear();
                    dtLvTyCarryOverNature.Dispose(); 
                }
                i++;
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
        long lngID = 0;
        try
        {            
            if (hfIsUpdate.Value == "N")
                lngID = objDB.GerMaxIDNumber("LeavePakMst", "LeavePakID");
            else
                lngID =Convert.ToInt64(hfID.Value);

            LeavePakMst objLeavePakMst = new LeavePakMst(lngID.ToString(), txtLeavePakName.Text, txtDescription.Text, (chkIsOffdayCounted.Checked == true ? "Y" : "N"),
                (chkLeavCalcOnJoining.Checked == true ? "Y" : "N"),  (chkInActive.Checked == true ? "N" : "Y"), ddlEmploymentType.SelectedValue.ToString(), (chkIsDefault.Checked == true ? "Y" : "N"), Session["USERID"].ToString(),
                Common.SetDateTime(DateTime.Now.ToString()), "N", ddlMonthFrom.SelectedValue.ToString(), ddlMonthTo.SelectedValue.ToString(), (chkIsNextYear.Checked == true ? "Y" : "N"), strStartDate, strEndDate);

            objLeaveManager.InsertLeavePakMst(objLeavePakMst, hfIsUpdate.Value, IsDelete, (chkInActive.Checked == true ? "N" : "Y"), grLeaveList);

            //if (hfIsUpdate.Value == "Y")
            //{
            //    LeaveManager objLeaveMgr = new LeaveManager();               
            //    objLeaveMgr.InsertLeaveProfile(ddlEmploymentType.SelectedValue, lngID.ToString(), Session["USERID"].ToString());
            //}

            if ((hfIsUpdate.Value == "N") && (IsDelete=="N"))
                lblMsg.Text = "Record Saved Successfully";
            else if ((hfIsUpdate.Value == "Y") && (IsDelete=="N"))
                lblMsg.Text = "Record Updated Successfully";
            else if (IsDelete == "Y")
                lblMsg.Text = "Record Deleted Successfully";
            this.OpenRecord();

            Common.EmptyTextBoxValues(this);
            this.EntryMode(false);
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    private void GetLeavePeriod(string strFromMonth, string strToMonth)
    {
        if (strFromMonth.Length == 1)
            strFromMonth = "0" + ddlMonthFrom.SelectedValue.ToString();
        if (strToMonth.Length == 1)
            strToMonth = "0" + ddlMonthTo.SelectedValue.ToString();

        if (strFromMonth != "" && strToMonth != "")
        {
            strStartDate = DateTime.Now.Year.ToString();
            string strStartMonth = DateTime.Now.Month.ToString();
            if (chkIsNextYear.Checked == false)
            {
                strEndDate = Convert.ToString(Convert.ToInt32(strStartDate));
                strStartDate = Convert.ToString(Convert.ToInt32(strStartDate));
                strStartDate = strStartDate + "-" + strFromMonth + "-" + "01";
                strEndDate = strEndDate + "-" + strToMonth + "-" + Common.GetMonthDay(strToMonth, "");
            }
            else
            {
                strEndDate = Convert.ToString(Convert.ToInt32(strStartDate) + 1);
                strStartDate = Convert.ToString(Convert.ToInt32(strStartDate));
                strStartDate = strStartDate + "-" + strFromMonth + "-" + "01";
                strEndDate = strEndDate + "-" + strToMonth + "-" + Common.GetMonthDay(strToMonth, "");
            }
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);        
        this.OpenRecord();
        lblMsg.Text = "";
    }

    protected void grLeavePakMst_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                this.OpenRecord();
                hfID.Value = grLeavePakMst.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                txtLeavePakName.Text = grLeavePakMst.SelectedRow.Cells[1].Text;

                HiddenField hfPDesc = (HiddenField)grLeavePakMst.SelectedRow.Cells[2].FindControl("HfDesc");
                txtDescription.Text = hfPDesc.Value;
 
                HiddenField hfCalOnJD = (HiddenField)grLeavePakMst.SelectedRow.Cells[2].FindControl("HfCalOnJoinDate");                
                chkLeavCalcOnJoining.Checked = (hfCalOnJD.Value == "Y" ? true : false);

                HiddenField hfIsOffDC = (HiddenField)grLeavePakMst.SelectedRow.Cells[2].FindControl("hfIsOffdayCounted");
                chkIsOffdayCounted.Checked = (hfIsOffDC.Value == "Y" ? true : false);

                string strEmpTypeStatus = grLeavePakMst.DataKeys[_gridView.SelectedIndex].Values[2].ToString();
                ddlEmploymentType.SelectedValue = string.IsNullOrEmpty(strEmpTypeStatus) == true ? "0" : strEmpTypeStatus;

                string strMonthFrom = grLeavePakMst.DataKeys[_gridView.SelectedIndex].Values[3].ToString();
                ddlMonthFrom.SelectedValue = string.IsNullOrEmpty(strMonthFrom) == true ? "0" : strMonthFrom;

                string strMonthTo = grLeavePakMst.DataKeys[_gridView.SelectedIndex].Values[4].ToString();
                ddlMonthTo.SelectedValue = string.IsNullOrEmpty(strMonthTo) == true ? "0" : strMonthTo;

                HiddenField hfIsActive = (HiddenField)grLeavePakMst.SelectedRow.Cells[2].FindControl("hfIsActive");
                chkInActive.Checked = (hfIsActive.Value == "Y" ? false : true);

                HiddenField hfISDefault = (HiddenField)grLeavePakMst.SelectedRow.Cells[2].FindControl("hfISDefault");
                chkIsDefault.Checked = (hfISDefault.Value == "Y" ? true : false);

                chkIsNextYear.Checked = (grLeavePakMst.DataKeys[_gridView.SelectedIndex].Values[5].ToString() == "Y" ? true : false );

                DataTable dtLeaveDet = objLeaveManager.SelectLeavePakDet(Convert.ToInt32(hfID.Value));
                if (dtLeaveDet.Rows.Count > 0)
                {
                    grLeaveList.DataSource = dtLeaveDet;
                    grLeaveList.DataBind();
                    this.CheckSelectedGridField();
                }

                else
                {
                    dtLeaveList = objLeaveManager.SelectLeaveType(0);
                    grLeaveList.DataSource = dtLeaveList;
                    grLeaveList.DataBind();
                }

                this.EntryMode(true);
                this.TabContainer1.ActiveTabIndex = 0;
                this.TabContainer1.TabIndex = 0;
                break;
        }
    }

    protected void CheckSelectedGridField()
    {
        
        foreach (GridViewRow gRow in grLeaveList.Rows)
        {
            CheckBox chkBox = new CheckBox();
            chkBox = (CheckBox)gRow.Cells[1].FindControl("chkSelect");

            HiddenField hfLPID = new HiddenField();
            hfLPID = (HiddenField)gRow.Cells[2].FindControl("hfLPakId");
            if (string.IsNullOrEmpty(hfLPID.Value) == false)
            {
                chkBox.Checked = true;
            }
            else
            {
                chkBox.Checked = false;
            }
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfID.Value) == false)
        {
            this.SaveData("Y");
        }
        else
        {
            lblMsg.Text = "Select a Leave Package Name first from the list then try to delete.";
        }

        this.EntryMode(false);
    }
    protected void grLeavePakMst_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}

