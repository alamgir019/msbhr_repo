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

public partial class EIS_Discipline : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    DataTable dtDivision = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpadate.Value = "N";
            dtDivision.Rows.Clear();
            dtDivision.Dispose();
            grDivision.DataSource = null;
            grDivision.DataBind();
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
        dtDivision = objMasMgr.SelectDisicipline(0);
        grDivision.DataSource = dtDivision;
        grDivision.DataBind();
    }
    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        try
        {
            MasterTablesManager MasMgr = new MasterTablesManager();

            if (hfIsUpadate.Value == "N")
                lngID = objDB.GerMaxIDNumber("Disiciplineinfo", "DisiciplineId");
            else
                lngID = Convert.ToInt32(hfID.Value);

            Disicipline objDiv = new Disicipline(lngID.ToString(), txttitel.Text.Trim(), "N", Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "HRD", "N", "N", (chkInActive.Checked == true ? "Y" : "N"));

            MasMgr.InsertDisicipline(objDiv, hfIsUpadate.Value, IsDelete, (chkInActive.Checked == true ? "N" : "Y"));

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
    protected void grDivision_PageIndexChanged(object sender, EventArgs e)
    {
        //grDivision.PageIndex = e.NewPageIndex;
        //this.OpenRecord();
    }
    protected void grDivision_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                txttitel.Text = grDivision.SelectedRow.Cells[1].Text;
                hfID.Value = grDivision.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                chkInActive.Checked = (grDivision.DataKeys[_gridView.SelectedIndex].Values[1].ToString() == "Y" ? false : true);
                this.EntryMode(true);
                break;
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
            lblMsg.Text = "Select a Division first from the list then try to delete.";
        }

        this.EntryMode(false);
    }
}
