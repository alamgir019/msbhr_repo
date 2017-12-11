using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class EIS_SpecialSkillSetup : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    DataTable dtSBU = new DataTable();
    DataTable dtDepartment = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpadate.Value = "N";
            dtSBU.Rows.Clear();
            dtSBU.Dispose();
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
        dtDepartment = objMasMgr.SelectSpecialSkill(0);
        grDepartment.DataSource = dtDepartment;
        grDepartment.DataBind();
    }

    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        try
        {
            MasterTablesManager MasMgr = new MasterTablesManager();

            if (hfIsUpadate.Value == "N")
                lngID = objDB.GerMaxIDNumber("SpecialSkillList", "SpecSkillId");
            else
                lngID = Convert.ToInt32(hfID.Value);

            Department objDepartment = new Department(lngID.ToString(), txtDept.Text.Trim(), "", "N", Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "N/A", "N", "N", "", (chkIsActive.Checked == true ? "N" : "Y"));

            MasMgr.InsertSpecialSkill(objDepartment, hfIsUpadate.Value, IsDelete, "", (chkIsActive.Checked == true ? "N" : "Y"));

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
        lblMsg.Text = "";
        this.OpenRecord();
    }

    protected void grSBU_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        // grSBU.PageIndex = e.NewPageIndex;
        this.OpenRecord();
    }

    protected void grDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grDepartment.PageIndex = e.NewPageIndex;
        this.OpenRecord();
    }

    protected void grDepartment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                txtDept.Text = grDepartment.SelectedRow.Cells[1].Text;
                chkIsActive.Checked = (grDepartment.DataKeys[_gridView.SelectedIndex].Values[1].ToString() == "Y" ? false : true);
                hfID.Value = grDepartment.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                this.EntryMode(true);
                //this.CheckDeptWiseSBU(Convert.ToInt32(hfID.Value));                  
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
