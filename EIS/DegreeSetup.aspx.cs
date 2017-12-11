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
public partial class EIS_DegreeSetup : System.Web.UI.Page
{
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    DataTable dtList = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
            dtList.Rows.Clear();
            dtList.Dispose();
            grList.DataSource = null;
            grList.DataBind();
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
        dtList = objEmpMgr.SelectDegree(0,"A","");
        grList.DataSource = dtList;
        grList.DataBind();
    }

    private void SaveData(string IsDelete)
    {
        try
        {
            if (hfIsUpdate.Value == "Y")
                hfId.Value = hfId.Value;
            else
                hfId.Value = Common.getMaxId("DegreeList", "DegreeId");

            clsCommonSetup objCommonSetup = new clsCommonSetup(hfId.Value.ToString(), txtName.Text.Trim(), (chkInActive.Checked == true ? "N" : "Y"), "N",
                Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "N", IsDelete);

            objEmpMgr.InsertDegree(objCommonSetup, hfIsUpdate.Value, IsDelete);

            if (hfIsUpdate.Value == "N")
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
        this.SaveData("N");
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfId.Value) == false)
        {
            this.SaveData("Y");
        }
        else
        {
            lblMsg.Text = "Select a item first from the list then try to delete.";
        }

        this.EntryMode(false);
    }

    protected void grList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                txtName.Text = Common.CheckNullString(grList.SelectedRow.Cells[1].Text.Trim ());
                hfId.Value = grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                if (Common.CheckNullString(grList.SelectedRow.Cells[2].Text.Trim()) == "Y")
                    chkInActive.Checked = false;
                else
                    chkInActive.Checked = true;
                this.EntryMode(true);
                break;
        }
    }
}
