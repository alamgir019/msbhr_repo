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

public partial class SOF_Project : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    SOFManager objSOFMgr = new SOFManager();

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
        dtList = objSOFMgr.SelectProjectList(0);
        grList.DataSource = dtList;
        grList.DataBind();
    }

    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        try
        {
            if (hfIsUpdate.Value == "N")
                lngID = objDB.GerMaxIDNumber("ProjectList", "ProjectId");
            else
                lngID = Convert.ToInt32(hfID.Value);

            clsCommonSetup objCommonSetup = new clsCommonSetup(lngID.ToString(),txtCode.Text.Trim(),   txtName.Text.Trim(), (chkInActive.Checked == true ? "N" : "Y"), "N", 
                Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()),  "N", "N" );

            objSOFMgr.InsertProjectList(objCommonSetup, hfIsUpdate.Value, IsDelete);

            lblMsg.Text = Common.GetMessage(hfIsUpdate.Value.ToString(), IsDelete);
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
        lblMsg.Text = "";
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfID.Value) == false)
        {
            this.SaveData("Y");
        }
        else
        {
            lblMsg.Text = "Select a project first from the list then try to delete.";
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

                txtCode.Text = grList.SelectedRow.Cells[1].Text;
                hfID.Value = grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                txtName.Text = grList.SelectedRow.Cells[2].Text;
                chkInActive.Checked = (grList.DataKeys[_gridView.SelectedIndex].Values[1].ToString() == "Y" ? false : true);
                this.EntryMode(true);
                break;
        }
    }
}
