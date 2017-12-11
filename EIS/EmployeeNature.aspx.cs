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

public partial class EIS_EmployeeNature : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    DataTable dtEmpNature = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
            dtEmpNature.Rows.Clear();
            dtEmpNature.Dispose();
            grEmpTypeStatus.DataSource = null;
            grEmpTypeStatus.DataBind();
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
            //txtDesc.Text = "";
        }
    }

    private void OpenRecord()
    {
        dtEmpNature = objMasMgr.SelectEmpNature(0);
        grEmpTypeStatus.DataSource = dtEmpNature;
        grEmpTypeStatus.DataBind();
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
                lngID = objDB.GerMaxIDNumber("EmpNatureList", "EmpNatureID");
            else
                lngID = Convert.ToInt32(hfID.Value);

            clsCommonSetup objEmpType = new clsCommonSetup(lngID.ToString(), "",txtEmpNature.Text.Trim(), (chkInActive.Checked == true ? "N" : "Y"),
                "N", Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "N","N" );

            MasMgr.InsertEmpNature(objEmpType, hfIsUpdate.Value, IsDelete);

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

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
        lblMsg.Text = "";
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

                txtEmpNature.Text = grEmpTypeStatus.SelectedRow.Cells[1].Text;
                hfID.Value = grEmpTypeStatus.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                //txtDesc.Text = grEmpTypeStatus.DataKeys[_gridView.SelectedIndex].Values[2].ToString();
                chkInActive.Checked = (grEmpTypeStatus.DataKeys[_gridView.SelectedIndex].Values[1].ToString() == "Y" ? false : true);
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
