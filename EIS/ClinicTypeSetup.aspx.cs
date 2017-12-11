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

public partial class EIS_ClinicTypeSetup : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    DataTable dtCompany = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpadate.Value = "N";
            dtCompany.Rows.Clear();
            dtCompany.Dispose();
            grClinicType.DataSource = null;
            grClinicType.DataBind();
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
        dtCompany = objMasMgr.GetClinicTypeList();
        grClinicType.DataSource = dtCompany;
        grClinicType.DataBind();
    }

    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        try
        {
            MasterTablesManager MasMgr = new MasterTablesManager();
            //Filling Class Properties with values
            if (hfIsUpadate.Value == "N")
                lngID = objDB.GerMaxIDNumber("ClinicTypeList", "ClinicTypeId");
            else
                lngID = Convert.ToInt32(hfID.Value);

            clsClinicType objCliType = new clsClinicType(lngID.ToString(), txtClinicType.Text.Trim(), (ChkIsActive.Checked == true ? "N" : "Y"), "N",
                    Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "MSB");


            MasMgr.InsertUpClinicTypeList(objCliType, hfIsUpadate.Value, IsDelete);

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
        lblMsg.Text = "";
    }

    protected void grClinic_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grClinicType.PageIndex = e.NewPageIndex;
        this.OpenRecord();
    }

    protected void grClinicType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                txtClinicType.Text = grClinicType.SelectedRow.Cells[1].Text;
                ChkIsActive.Checked = grClinicType.DataKeys[_gridView.SelectedIndex].Values[1].ToString() == "N" ? true : false;
                hfID.Value = grClinicType.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
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
            lblMsg.Text = "Select a District first from the list then try to delete.";
        }

        this.EntryMode(false);
    }
}
