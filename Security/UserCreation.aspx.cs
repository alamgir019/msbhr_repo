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
using System.Security.Principal;
using System.Security.Cryptography;


public partial class UserCreationSetup : System.Web.UI.Page
{    
    DBConnector objDB = new DBConnector();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    UserManager objUserMgr = new UserManager(); 
    DataTable dtUser = new DataTable();
    DataTable dtEmp = new DataTable();
    DataTable dtBranch = new DataTable();
    DataTable dtDivision = new DataTable();
    
    DataTable dtBranchWiseDiv = new DataTable(); 
    protected static string userid;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpadate.Value = "N";
            dtUser.Rows.Clear();
            dtUser.Dispose();
            grUser.DataSource = null;
            grUser.DataBind();
            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.EntryMode(false);
            this.OpenRecord();
            this.Bind_ddlPrivs();
        }
    }

    private void Bind_ddlPrivs()
    {
        Common.FillDropDownList(objUserMgr.SelectPrivPack(0), ddlPrivs, true);
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.EntryMode(false);
        this.OpenRecord();
    }

    private void OpenRecord()
    {
        dtUser = objUserMgr.SelectUserInfo("", "A");
        grUser.DataSource = dtUser;
        grUser.DataBind();
    }

    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
        {
            btnSave.Text = "Update";
            hfIsUpadate.Value = "Y";
            txtUserId.Enabled = false; 
        }
        else
        {
            btnSave.Text = "Save";
            hfIsUpadate.Value = "N";
            txtUserId.Enabled = true;
            txtUserId.Text = "";
            txtUserName.Text = "";
            txtPass.Text = "";
            txtConfirmPass.Text = "";
            txtEmpId.Text = "";
            chkIsActive.Checked = false;
            chkIsAdmin.Checked = false;  
        }
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
            if (string.IsNullOrEmpty(txtUserId.Text.Trim()) == true)
            {
                lblMsg.Text = "Plase Enter the UserId";
                txtUserId.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtUserName.Text.Trim()) == true)
            {
                lblMsg.Text = "Plase Enter the User Name";
                txtUserName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPass.Text.Trim()) == true)
            {
                lblMsg.Text = "Plase Enter the Password";
                txtPass.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtConfirmPass.Text.Trim()) == true)
            {
                lblMsg.Text = "Plase Enter the Confirm Password";
                txtConfirmPass.Focus();
                return false;
            }

            if (Common.CompareHashValues(txtPass.Text.Trim(), txtConfirmPass.Text.Trim()) == false)
            {
                lblMsg.Text = "Password and Confirm Passowrd does not Match";
                txtConfirmPass.Focus();
                return false;
            }

            if (hfIsUpadate.Value == "N")
            {
                if (Common.CheckDuplicate("UserInfo", "UserId", txtUserId.Text.Trim(), "", "", false) == true)
                {
                    lblMsg.Text = "This User is Already Exist.";
                    txtUserId.Focus();
                    return false;
                }
            }

            else
            {
                if (Common.CheckDuplicate("UserInfo", "UserId", txtUserId.Text.Trim(), "UserId", hfID.Value , true) == true)
                {
                    lblMsg.Text = "This User is Already Exist.";
                    txtUserId.Focus();
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtEmpId.Text.Trim()) == false)
            {
                dtEmp = objMasMgr.SelectEmployee(txtEmpId.Text.Trim());
                if (dtEmp.Rows.Count == 0)
                {
                    lblMsg.Text = "Employee Id is not valid.";
                    txtEmpId.Focus();
                    return false;
                }
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
            DataTable dtPrivPack = objUserMgr.SelectPrivPack(Convert.ToInt32(ddlPrivs.SelectedValue.ToString()));
            UserCreation objUser = new UserCreation(txtUserId.Text.Trim(), Common.getHashValue(txtPass.Text.Trim()), txtUserName.Text.Trim(),
                (chkIsActive.Checked == true ? "N" : "Y"), Common.getHashValue(txtConfirmPass.Text.Trim()), txtEmpId.Text.Trim(),
                (chkIsAdmin.Checked == true ? "Y" : "N"),hfDivision.Value.ToString(), hfSBU.Value.ToString(), 
                Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "N", "N",hfDept.Value.ToString());
            objUserMgr.InsertUser(objUser, hfIsUpadate.Value.ToString(), IsDelete, dtPrivPack, ddlPrivs.SelectedValue.ToString());
            
            if (hfIsUpadate.Value == "N")
                lblMsg.Text = "Record Saved Successfully";
            else
                lblMsg.Text = "Record Updated Successfully";

            this.EntryMode(false);

            this.OpenRecord();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
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
            lblMsg.Text = "Select a User first from the list then try to delete.";
        }
        this.EntryMode(false);
    }

    protected void grUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("OnClick"):
                userid = grUser.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim();
                _gridView.SelectedIndex = _selectedIndex;
                txtUserId.Text = userid;
                hfID.Value = txtUserId.Text.Trim();
                txtUserName.Text = grUser.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim();
                chkIsActive.Checked = grUser.DataKeys[_gridView.SelectedIndex].Values[2].ToString() == "N" ? true : false;
                txtEmpId.Text = grUser.DataKeys[_gridView.SelectedIndex].Values[3].ToString().Trim();
                chkIsAdmin.Checked = grUser.DataKeys[_gridView.SelectedIndex].Values[4].ToString() == "Y" ? true : false;
                hfSBU.Value = grUser.DataKeys[_gridView.SelectedIndex].Values[5].ToString().Trim();
                hfDivision.Value = grUser.DataKeys[_gridView.SelectedIndex].Values[6].ToString().Trim();
                hfDept.Value = grUser.DataKeys[_gridView.SelectedIndex].Values[5].ToString().Trim();
                if ((grUser.DataKeys[_gridView.SelectedIndex].Values[6].ToString().Trim() != "-1")&&(grUser.DataKeys[_gridView.SelectedIndex].Values[6].ToString().Trim() != ""))
                    ddlPrivs.SelectedValue = grUser.DataKeys[_gridView.SelectedIndex].Values[6].ToString().Trim();            
                this.EntryMode(true);
                break;
        }
    }

    protected void grUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grUser.PageIndex = e.NewPageIndex;
        this.OpenRecord();
    }

    protected void txtEmpId_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtEmpId.Text.Trim()) == false)
        {
            EmpInfoManager objEmp = new EmpInfoManager();
            DataTable dtEmpInfoHr = objEmp.SelectEmpInfoHR(txtEmpId.Text.Trim());
            if (dtEmpInfoHr.Rows.Count > 0)
            {
                txtUserName.Text = dtEmpInfoHr.Rows[0]["FullName"].ToString().Trim();
            }
            else
            {
                txtUserName.Text = "";
                lblMsg.Text = "Invalid User ID.";
            }
        }
    }
}
