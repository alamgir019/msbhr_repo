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

public partial class EIS_EmergencyContract : System.Web.UI.Page
{
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    UserManager objUserMgr = new UserManager();

    DataTable dtEmpInfo = new DataTable();
    DataTable dtContact = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList_Nil(objMasMgr.SelectRelationList(0), ddlRelation);
            this.EntryMode(false);
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
            this.ClearControl();
            btnSave.Text = "Save";
            hfIsUpdate.Value = "N";
        }
    }
   
    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (txtEmpID.Text.Trim() == "")
            return;

        dtEmpInfo = objEmpMgr.SelectEmpInfoHRAction(txtEmpID.Text.Trim());
        if (dtEmpInfo.Rows.Count == 0)
        {
            lblMsg.Text = "Invalid Employee No.";
            return;
        }
        else
        {
            if (GetTaskPermission() == false)
            {
                this.RefreshControl();
                lblMsg.Text = "Please mention contractual & intern staff's id.";
                btnSave.Enabled = false;
                return;
            }
            else
            {
                lblMsg.Text = "";
                btnSave.Enabled = true;
            }
            foreach (DataRow dRow in dtEmpInfo.Rows)
            {
                lblName.Text = dRow["FullName"].ToString();
                lblDesignation.Text = dRow["DesigName"].ToString().Trim();
                lblCompany.Text = dRow["CompanyName"].ToString().Trim();
                lblProject.Text = dRow["ProjectName"].ToString().Trim();
                lblDept.Text = dRow["DeptName"].ToString().Trim();
                lblSubDept.Text = dRow["SubDeptName"].ToString().Trim();
                lblSuncode.Text = dRow["ClinicName"].ToString().Trim();
            }
            this.OpenRecord();
        }
    }

    private void OpenRecord()
    {
        dtContact = objEmpMgr.SelectEmergencyContact( txtEmpID.Text.Trim());
        grContactList.DataSource = dtContact;
        grContactList.DataBind();
    }

    protected void RefreshControl()
    {
        lblName.Text = "";
        lblDesignation.Text = "";
        lblDept.Text = "";
        lblCompany.Text = "";
        lblSubDept.Text = "";
        lblSuncode.Text = "";
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
        grContactList.DataSource = null;
        grContactList.DataBind();
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        this.RefreshControl();        
    }

    protected void ClearControl()
    {
        txtName.Text = "";       
        txtAdd.Text = "";
        txtPhone.Text = "";
        ddlRelation.SelectedIndex = -1;
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
            if (txtName.Text   == "")
            {
                lblMsg.Text = "Please enter conatct name.";
                txtName.Focus();
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
    private void SaveData(string strIsDelete)
    {        
        try
        {
            if (hfIsUpdate.Value == "Y")
                hfId.Value = hfId.Value;
            else
                hfId.Value = Common.getMaxId("EmpEmergencyContact", "ContactId");

            objEmpMgr.InsertEmergencyContact(hfId.Value.ToString(), txtEmpID.Text.Trim(), txtName.Text.Trim(), txtAdd.Text.Trim(), txtPhone.Text.Trim (),
                ddlRelation.SelectedValue.ToString(), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), hfIsUpdate.Value.ToString(), strIsDelete);

            if (hfIsUpdate.Value == "N")
                lblMsg.Text = "Record Saved Successfully";
            else
                lblMsg.Text = "Record Updated Successfully";
            this.OpenRecord();
            this.EntryMode(false);
            this.ClearControl();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
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
        this.OpenRecord();
        this.EntryMode(false);
    }
    protected void grContactList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                txtName.Text = Common.CheckNullString(grContactList.SelectedRow.Cells[1].Text.Trim());
                hfId.Value = grContactList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                txtAdd.Text = Common.CheckNullString(grContactList.SelectedRow.Cells[2].Text.Trim());
                txtPhone.Text = Common.CheckNullString(grContactList.SelectedRow.Cells[3].Text.Trim());
                ddlRelation.SelectedValue = grContactList.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim();

                this.EntryMode(true);
                break;

        }
    }

    private bool GetTaskPermission()
    {
        string strEmpType = "";
        DataTable dtConsTaskPermission = objUserMgr.GetUserTaskPermission(Session["USERID"].ToString(), "307", "T102");
        if (dtConsTaskPermission.Rows.Count > 0)
        {
            strEmpType = objEmpMgr.SelectEmpWiseContractType(txtEmpID.Text.Trim());
            if (strEmpType != "")
                return true;
            else
                return false;
        }
        return true;
    }
}
