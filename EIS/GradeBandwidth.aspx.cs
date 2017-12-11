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

public partial class EIS_GradeBandwidth : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    DataTable dtSubGrade = new DataTable();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList_Nil(objMasMgr.SelectGradeddl(0), ddlGrade);
            hfIsUpadate.Value = "N";
            grSubGrade.DataSource = null;
            grSubGrade.DataBind();
            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.EntryMode(false);
            this.OpenRecord();
        }
    }

    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
        {
            btnSave.Text = "Update";
            hfIsUpadate.Value = "Y";
        }
        else
        {
            btnSave.Text = "Save";
            hfIsUpadate.Value = "N";            
        }
    }

    private void OpenRecord()
    {
        dtSubGrade = objMasMgr.SelectSubGrade(0);
        grSubGrade.DataSource = dtSubGrade;
        grSubGrade.DataBind();
    }

    private void SaveData(string IsDelete)
    {
        try
        {
            objMasMgr.InsertGradeWiseSubGrade(grSubGrade, ddlGrade.SelectedValue, Session["USERID"].ToString(), DateTime.Now.ToString());

            if (hfIsUpadate.Value == "N")
                lblMsg.Text = "Record Saved Successfully";
            else
                lblMsg.Text = "Record Updated Successfully";
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (CheckField())
        {
            this.SaveData("N");
        }
    }

    private bool CheckField()
    {
        if (ddlGrade.SelectedItem.Text.Trim() == "Nil")
        {
            lblMsg.Text = "Please select Grade Name";
            return false;
        }
        return true;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
    }

    protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        grSubGrade.DataSource = null;
        grSubGrade.DataBind();

        DataTable dt = objMasMgr.SelectGradeWiseSubGrade(Convert.ToInt32(ddlGrade.SelectedValue));
        if (dt.Rows.Count > 0)
        {
            grSubGrade.DataSource = dt;
            grSubGrade.DataBind();
        }
        else
        {
            this.OpenRecord();
        }
    }
}
