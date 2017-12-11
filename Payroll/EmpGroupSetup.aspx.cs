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

public partial class Payroll_EmpGroupSetup : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    DataTable dtEmpType = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
            dtEmpType.Rows.Clear();
            dtEmpType.Dispose();
            grEmpTypeStatus.DataSource = null;
            grEmpTypeStatus.DataBind();
            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.EntryMode(false);
            this.OpenRecord();
            this.GetEmpGroup();
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
            txtDesc.Text = "";
        }
    }

    private void OpenRecord()
    {
        dtEmpType = objMasMgr.SelectEmpTypeForEmpGroup(0);
        grEmpTypeStatus.DataSource = dtEmpType;
        grEmpTypeStatus.DataBind();
    }

    private void GetEmpGroup()
    {
        grEmpGroup.DataSource = objMasMgr.SelectEmpGroup(0);
        grEmpGroup.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.SaveData("N");
    }

    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        try
        {
            MasterTablesManager MasMgr = new MasterTablesManager();

            if (hfIsUpdate.Value == "N")
                lngID = objDB.GerMaxIDNumber("EmpGroupList", "EmpGrpId");
            else
                lngID = Convert.ToInt32(hfID.Value);

            clsEmpType objEmpType = new clsEmpType(lngID.ToString(), txtEmpType.Text.Trim(), txtDesc.Text.Trim(), (chkInActive.Checked == true ? "N" : "Y"),
                "N", Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "N", "N");

            MasMgr.InsertEmpGroup(objEmpType, grEmpTypeStatus, hfIsUpdate.Value, IsDelete);

            if (hfIsUpdate.Value == "N")
                lblMsg.Text = "Record Saved Successfully";
            else
                lblMsg.Text = "Record Updated Successfully";
            Common.EmptyTextBoxValues(this);
            this.EntryMode(false);
            this.OpenRecord();
            this.GetEmpGroup();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
    }

    protected void grEmpTypeStatus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                txtEmpType.Text = grEmpGroup.SelectedRow.Cells[1].Text;
                hfID.Value = grEmpGroup.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                txtDesc.Text = grEmpGroup.DataKeys[_gridView.SelectedIndex].Values[2].ToString();
                chkInActive.Checked = (grEmpGroup.DataKeys[_gridView.SelectedIndex].Values[1].ToString() == "Y" ? false : true);
                this.EntryMode(true);

                dtEmpType = objMasMgr.SelectEmpTypeForEmpGroup(Convert.ToInt32(grEmpGroup.DataKeys[_gridView.SelectedIndex].Values[0].ToString()));

                grEmpTypeStatus.DataSource = dtEmpType;
                grEmpTypeStatus.DataBind();

                foreach (GridViewRow gRow in grEmpTypeStatus.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gRow.FindControl("chkSelect");
                    for (int i = 0; i < dtEmpType.Rows.Count; i++)
                    {
                        if (grEmpTypeStatus.DataKeys[gRow.RowIndex].Values[2].ToString().Trim() == dtEmpType.Rows[i]["EmpGrpId"].ToString().Trim() && string.IsNullOrEmpty(dtEmpType.Rows[i]["EmpGrpId"].ToString().Trim()) == false)
                        {
                            chkSelect.Checked = true;
                        }
                    }
                }
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
