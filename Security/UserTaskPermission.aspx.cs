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

public partial class UserTaskPermission : System.Web.UI.Page
{
    UserManager objUserMgr = new UserManager();
  
    DataTable dtEmpInfo = new DataTable();
    DataTable dtTaskList = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList_Nil(objUserMgr.SelectUserInfo("","A"), ddlUserId);
            Common.FillDropDownList_Nil(objUserMgr.SelectScreenInfo(), ddlScreen);
            //Common.FillDropDownList_Nil(objUserMgr.SelectTaskPermission(), ddlTask);

            this.EntryMode(false);          
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
        dtTaskList = objUserMgr.GetUserTaskPermission("","","");
        grTaskList.DataSource = dtTaskList;
        grTaskList.DataBind();
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
        {
            this.SaveData();
        }
    }

    protected bool ValidateAndSave()
    {
        try
        {
            if (ddlUserId.SelectedIndex == 0)
            {
                lblMsg.Text = "Please Select The User From The List.";
                ddlUserId.Focus();
                return false;
            }

            if (ddlScreen.SelectedIndex == 0)
            {
                lblMsg.Text = "Please Select The Screen From The List.";
                ddlScreen.Focus();
                return false;
            }

            if (ddlTask.SelectedIndex == 0)
            {
                lblMsg.Text = "Please Select The Task From The List.";
                ddlTask.Focus();
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

    protected void SaveData()
    {        
        //if (hfIsUpdate.Value == "Y")
        //    hfId.Value = hfId.Value;
        //else
        //    hfId.Value = Common.getMaxId("UserTaskPermission", "Id");

        //objEmpMgr.InsertDisciplinary(hfId.Value.ToString(), txtEmpID.Text.Trim(), strEntryDate, ddlAction.SelectedValue.ToString(), ddlReasonList.SelectedValue.ToString(), strActionDate, strReviewDate, chkIsReviewed.Checked == true ? "Y" : "N", 
        //    chkIsSuspendInc.Checked ==true ? "Y":"N",txtRemarks.Text.Trim(), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), hfIsUpdate.Value.ToString());

        if (hfIsUpdate.Value == "N")
            lblMsg.Text = "Record Saved Successfully";
        else
            lblMsg.Text = "Record Updated Successfully";
        this.OpenRecord();
        this.EntryMode(false);
    }    
  
    //protected void grDisciplinary_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    GridView _gridView = (GridView)sender;
    //    // Get the selected index and the command name
    //    int _selectedIndex = int.Parse(e.CommandArgument.ToString());
    //    string _commandName = e.CommandName;
    //    _gridView.SelectedIndex = _selectedIndex;
    //    switch (_commandName)
    //    {
    //        case ("DoubleClick"):

    //            ddlAction.SelectedValue = grDisciplinary.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
    //            hfId.Value = grDisciplinary.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
    //            txtEntryDate.Text = Common.CheckNullString(grDisciplinary.SelectedRow.Cells[1].Text.Trim());
    //            ddlReasonList.SelectedValue = grDisciplinary.DataKeys[_gridView.SelectedIndex].Values[2].ToString();
    //            txtActionDate.Text = Common.CheckNullString(grDisciplinary.SelectedRow.Cells[3].Text.Trim());
    //            txtReviewDate.Text = Common.CheckNullString(grDisciplinary.SelectedRow.Cells[4].Text.Trim());
    //            chkIsReviewed.Checked = Common.CheckNullString(grDisciplinary.SelectedRow.Cells[5].Text.Trim())=="Y"?true:false ;
    //            chkIsSuspendInc.Checked = Common.CheckNullString(grDisciplinary.SelectedRow.Cells[6].Text.Trim()) == "Y" ? true : false;
    //            txtRemarks.Text = Common.CheckNullString(grDisciplinary.SelectedRow.Cells[7].Text.Trim());
    //            this.EntryMode(true);
    //            lblMsg.Text = "";
    //            break;
    //    }
    //}
    
}
