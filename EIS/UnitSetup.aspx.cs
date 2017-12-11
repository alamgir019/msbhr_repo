using System;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class SBUSetup : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    DataTable dtDivision = new DataTable();
    DataTable dtSBU = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpadate.Value = "N";
            dtDivision.Rows.Clear();
            dtDivision.Dispose();

            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.EntryMode(false);
            this.OpenRecord(0);
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

   /* private void OpenRecord()
    {
        grUnitList.DataSource = objMasMgr.SelectUnit(0);
        grUnitList.DataBind();
    }*/
    private void OpenRecord(int UnitId)
    {
        grDepartment.DataSource = objMasMgr.SelectUnitWiseDepartment(UnitId);
        grDepartment.DataBind();

        foreach (GridViewRow gRow in grDepartment.Rows)
        {
            CheckBox chkBox = (CheckBox)gRow.FindControl("chkBox");

            if (Convert.ToDecimal(grDepartment.DataKeys[gRow.RowIndex].Values[1]) > 0)
            {
                chkBox.Checked = true;
            }
            else
            {
                chkBox.Checked = false;
            }
        }

        grUnitList.DataSource = objMasMgr.SelectUnit(0);
        grUnitList.DataBind();
    }

    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        try
        {
            StringBuilder strDivisionID = new StringBuilder();
            MasterTablesManager MasMgr = new MasterTablesManager();

            if (hfIsUpadate.Value == "N")
                lngID = objDB.GerMaxIDNumber("UnitList", "UnitId");
            else
                lngID = Convert.ToInt32(hfID.Value);
            Desigation objCnt = new Desigation(lngID.ToString(), txtSbu.Text.Trim(), "N", Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "HRD", "N", "N", (chkInActive.Checked == true ? "N" : "Y"));

            MasMgr.InsertUnit(grDepartment, objCnt, hfIsUpadate.Value, IsDelete, (chkInActive.Checked == true ? "N" : "Y"));


           /* Division objDivision = new Division(lngID.ToString(), txtSbu.Text.Trim(), "N", Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "N/A", "N", "N", (chkInActive.Checked == true ? "N" : "Y"));

           objMasMgr.InsertUnit(objDivision, hfIsUpadate.Value, IsDelete, (chkInActive.Checked == true ? "N" : "Y"));

            MasMgr.InsertUnit(grDepartment, objDivision, hfIsUpadate.Value, IsDelete, (chkInActive.Checked == true ? "N" : "Y"));*/
            if (hfIsUpadate.Value == "N")
                lblMsg.Text = "Record Saved Successfully";
            else
                lblMsg.Text = "Record Updated Successfully";
            Common.EmptyTextBoxValues(this);
            this.EntryMode(false);
            this.OpenRecord(0);
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
        this.OpenRecord(0);
        lblMsg.Text = "";
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

    protected void grSbu_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                txtSbu.Text = grUnitList.SelectedRow.Cells[1].Text;
                hfID.Value = grUnitList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                chkInActive.Checked = grUnitList.DataKeys[_gridView.SelectedIndex].Values[1].ToString() == "N" ? true : false;
                this.EntryMode(true);
                this.OpenRecord(Convert.ToInt32(grUnitList.DataKeys[_gridView.SelectedIndex].Values[0].ToString()));
                TabContainer1.ActiveTabIndex = 0;
                break;
               /* hfID.Value = grUnitList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                txtSbu.Text = grUnitList.SelectedRow.Cells[1].Text;
                //txtDescription.Text = grSbu.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                chkInActive.Checked = grUnitList.DataKeys[_gridView.SelectedIndex].Values[1].ToString() == "N" ? true : false;
                //chkInActive.Checked = (grUnitList.SelectedRow.Cells[1].Text.Trim() == "N" ? true : false);
                //CheckSBUWiseDivision( Convert.ToInt32(hfID.Value));
                this.EntryMode(true);
                break;*/
        }
    }
}
