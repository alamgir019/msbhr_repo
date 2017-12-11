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

public partial class Leave_LeaveTakenValidation : System.Web.UI.Page
{
    LeaveManager objLeaveMgr = new LeaveManager();
    DataTable dtPLTypeList = new DataTable();
    DataTable dtNLTypeList = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList(objLeaveMgr.SelectLeaveType(0), ddlPLeaveType, "LTypeTitle", "LTypeID", true);
            Common.FillDropDownList(objLeaveMgr.SelectLeaveType(0), ddlNLeaveType, "LTypeTitle", "LTypeID", true);
            this.EntryMode(false);
            this.OpenRecord();
        }
    }

    private void OpenRecord()
    {
        dtNLTypeList = objLeaveMgr.SelectLeaveTakenMatrix();

        grLeave.DataSource = dtNLTypeList;
        grLeave.DataBind();
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
        }
    }
 
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
            this.SaveData("N");
    }

    private bool ValidateAndSave()
    {
        if (ddlPLeaveType.SelectedValue.ToString() == ddlNLeaveType.SelectedValue.ToString())
        {
            lblMsg.Text = "Both leave type should not same.";
            return false;
        }
        return true;
    }
    private void SaveData(string IsDelete)
    {
        string sId = "";
        try
        {
            
            if (hfIsUpdate.Value == "N")
                sId = Common.getMaxId("LeaveTakenBarrier", "Id");
            else
                sId = hfId.Value.ToString();

            objLeaveMgr.InsertLeaveTakenMatrix(sId, ddlPLeaveType.SelectedValue.ToString(), ddlNLeaveType.SelectedValue.ToString(),
                 Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), hfIsUpdate.Value.ToString(),IsDelete);

            Common.GetMessage(hfIsUpdate.Value, IsDelete);
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
    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.EntryMode(false);
        ddlPLeaveType.SelectedIndex = -1;
        ddlNLeaveType.SelectedIndex = -1;
        lblMsg.Text = "";
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfId.Value) == false)
        {
            this.SaveData("Y");
            lblMsg.Text = "Record Deleted Successfully";
        }
        else
        {
            lblMsg.Text = "Select a record first then try to delete.";
        }

        this.EntryMode(false);
    }

    protected void grLeave_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                ddlPLeaveType.SelectedValue = grLeave.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                ddlNLeaveType.SelectedValue = grLeave.DataKeys[_gridView.SelectedIndex].Values[2].ToString();
                hfId.Value = grLeave.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                this.EntryMode(true);              
                break;
        }
    }
}
