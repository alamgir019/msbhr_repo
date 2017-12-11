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

public partial class EIS_FestivalSetup : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    DataTable dtDesigation = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList_All(objMasMgr.SelectReligionList(0), ddlReligion);
            hfIsUpadate.Value = "N";
            dtDesigation.Rows.Clear();
            dtDesigation.Dispose();
            grFestival.DataSource = null;
            grFestival.DataBind();
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
        dtDesigation = objMasMgr.SelectFestivalList(0);
        grFestival.DataSource = dtDesigation;
        grFestival.DataBind();
    }

    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        try
        {
            MasterTablesManager MasMgr = new MasterTablesManager();

            if (hfIsUpadate.Value == "N")
                lngID = objDB.GerMaxIDNumber("Festival", "FestivalID");
            else
                lngID = Convert.ToInt32(hfID.Value);

            Desigation objCnt = new Desigation(lngID.ToString(), txtFestival.Text.Trim(), "N", Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "HRD", "N", "N", (ChkIsActive.Checked == true ? "N" : "Y"));

           // MasMgr.InsertReligionList(objCnt, hfIsUpadate.Value, IsDelete, (ChkIsActive.Checked == true ? "N" : "Y"));
            MasMgr.InsertFastivalList(objCnt, hfIsUpadate.Value, IsDelete, (ChkIsActive.Checked == true ? "N" : "Y"),ddlReligion.SelectedValue.ToString());

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

    protected void grFestival_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grFestival.PageIndex = e.NewPageIndex;
        this.OpenRecord();
    }

    protected void grFestival_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                txtFestival.Text = grFestival.SelectedRow.Cells[1].Text;
                if (string.IsNullOrEmpty(grFestival.DataKeys[_gridView.SelectedIndex].Values[1].ToString()) == false)
                    ddlReligion.SelectedValue = grFestival.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                hfID.Value = grFestival.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                ChkIsActive.Checked = grFestival.DataKeys[_gridView.SelectedIndex].Values[2].ToString() == "N" ? true : false;
                this.EntryMode(true);
                break;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfID.Value) == false)
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
}
