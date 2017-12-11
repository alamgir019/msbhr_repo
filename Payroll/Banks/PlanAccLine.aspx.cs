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

public partial class Payroll_Banks_PlanAccLine : System.Web.UI.Page
{
    //DataTable dtAccLine = new DataTable();
    PlanAccLineManager objAccMgr = new PlanAccLineManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
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
            hfIsUpdate.Value = "Y";
        }
        else
        {
            btnSave.Text = "Save";
            hfIsUpdate.Value = "N";
        }
    }

    private void OpenRecord()
    {
        grAccLine.DataSource = objAccMgr.SelectAccLineData("0", "");
        grAccLine.DataBind();
    }

    protected void grAccLine_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                txtAccLine.Text = grAccLine.SelectedRow.Cells[1].Text.Trim();
                txtDesc.Text = grAccLine.SelectedRow.Cells[2].Text;
                chkMakeInactive.Checked = (grAccLine.SelectedRow.Cells[3].Text == "Y" ? false : true);
                hfID.Value = grAccLine.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                hfIsUpdate.Value = "Y";
                this.EntryMode(true);
                break;
        }
    }

    private void SaveData(string IsDelete)
    {
        string  strID = "";
        try
        {
            //Filling Class Properties with values
            if (hfIsUpdate.Value == "N")
                strID = Common.getMaxId("PlanAccLine", "ACCLINEID");
            else
                strID = hfID.Value;
            if (IsDelete == "N")
            {
                objAccMgr.InsertData(strID, txtAccLine.Text.Trim(), txtDesc.Text, chkMakeInactive.Checked == true ? "N" : "Y", hfIsUpdate.Value.ToString(), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));

                if (hfIsUpdate.Value == "N")
                    lblMsg.Text = "Record Saved Successfully";
                else
                    lblMsg.Text = "Record Updated Successfully";
            }
            else
            {
                objAccMgr.DeleteData(strID);
                lblMsg.Text = "Record Deleted Successfully";
            }
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
        this.SaveData("N");
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        this.SaveData("Y");
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
    }
}
