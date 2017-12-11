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

public partial class Payroll_EmailNotificationSetup : System.Web.UI.Page
{
    MasterTablesManager MasMgr = new MasterTablesManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.GetEmailNotification();
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtApprover.Text = "";
        txtDirFinance.Text = "";
        txtDisburse.Text = "";
        txtFinance.Text = "";
        this.GetEmailNotification();
    }

    private void GetEmailNotification()
    {
        DataTable dt = MasMgr.GetEmailNotification();
        if (dt.Rows.Count > 0)
        {
            txtHR.Text = dt.Rows[0]["NEmpId"].ToString().Trim();
            txtApprover.Text = dt.Rows[0]["AEmpId"].ToString().Trim();
            txtDirFinance.Text = dt.Rows[0]["REmpId"].ToString().Trim();
            txtDisburse.Text = dt.Rows[0]["DEmpId"].ToString().Trim();
            txtFinance.Text = dt.Rows[0]["VEmpId"].ToString().Trim();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        MasMgr.InsertEmailNotification(txtHR.Text.Trim(), txtFinance.Text.Trim(), txtDirFinance.Text.Trim(), txtApprover.Text.Trim(),
            txtDisburse.Text.Trim(), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));

            lblMsg.Text = "Record Saved Successfully";
    }
}
