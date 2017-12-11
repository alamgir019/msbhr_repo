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

public partial class Payroll_SalarySubLocationSetup : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    DataTable dtDesigation = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpadate.Value = "N";
            dtDesigation.Rows.Clear();
            dtDesigation.Dispose();
            grDesigation.DataSource = null;
            grDesigation.DataBind();
            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.EntryMode(false);
            //this.OpenRecord();
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
        dtDesigation = objMasMgr.SelectDesignation(0);
        grDesigation.DataSource = dtDesigation;
        grDesigation.DataBind();
    }

    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        try
        {
            MasterTablesManager MasMgr = new MasterTablesManager();
            //Filling Class Properties with values
            //if (EntryMode(true))
            //    hfIsUpadate.Value="N";
            if (hfIsUpadate.Value == "N")
                lngID = objDB.GerMaxIDNumber("JobTitle", "JbTlId");
            else
                lngID = Convert.ToInt32(hfID.Value);

            Desigation objCnt = new Desigation(lngID.ToString(), txtDesigation.Text.Trim(), "N", Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "HRD", "N", "N", (ChkIsActive.Checked == true ? "N" : "Y"));

            MasMgr.InsertDesignation(objCnt, hfIsUpadate.Value, IsDelete, (ChkIsActive.Checked == true ? "N" : "Y"));

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
        this.SaveData("N");
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
    }

    protected void grDesigation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grDesigation.PageIndex = e.NewPageIndex;
        this.OpenRecord();
    }

    protected void grDesigation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                txtDesigation.Text = grDesigation.SelectedRow.Cells[1].Text;
                hfID.Value = grDesigation.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                ChkIsActive.Checked = grDesigation.DataKeys[_gridView.SelectedIndex].Values[1].ToString() == "N" ? true : false;
                this.EntryMode(true);
                break;
        }
    }

    protected void grDesigation_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfID.Value) == false)
        {
            this.SaveData("Y");
        }
        else
        {
            lblMsg.Text = "Select a Desigation first from the list then try to delete.";
        }

        this.EntryMode(false);
    }
}
