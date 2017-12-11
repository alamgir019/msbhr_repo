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

public partial class ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtUserId.Text = HttpContext.Current.Session["USERID"].ToString();
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
        if (string.IsNullOrEmpty(txtUserId.Text) == false)
        {
            string password = txtOldPass.Text.ToString();
            string strInputPwd = Common.getHashValue(password);
            DataTable dtUser = new DataTable();
            UserManager objUserMgr = new UserManager();
            dtUser = objUserMgr.SelectUserInfo(txtUserId.Text.Trim(), "Y");

            if (dtUser.Rows.Count > 0)
            {
                foreach (DataRow row in dtUser.Rows)
                {
                    if (string.Compare(row["Password"].ToString().Trim(), strInputPwd) != 0)
                    {
                        lblMsg.Text = "Old Password is not valid. Please check the password.";
                        txtOldPass.Focus();
                        return false;
                    }
                }
            }

            if (string.Compare(Common.getHashValue(txtNewPass.Text.Trim()), Common.getHashValue(txtConfNewPass.Text.Trim())) != 0)
            {
                lblMsg.Text = "New Password fields dont match with each other.";
                txtNewPass.Focus();
                return false;
            }
        }
        return true;
    }

    private void SaveData(string IsDelete)
    {
        try
        {
            UserManager objUserMgr = new UserManager(); 
            UserCreation objUser = new UserCreation(txtUserId.Text.Trim(), Common.getHashValue(txtNewPass.Text.Trim()));

            objUserMgr.UpdatePassword(objUser);

            lblMsg.Text = "Password Updated Successfully";
            Common.EmptyTextBoxValues(this);            
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }
}
