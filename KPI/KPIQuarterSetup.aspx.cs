using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class KPI_KPIQuarterSetup : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
   // MasterTablesManager objMasMgr = new MasterTablesManager();
    DataTable dtQuarter = new DataTable();
    KPIManager objKpi = new KPIManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
            dtQuarter.Rows.Clear();
            dtQuarter.Dispose();
            grQuarter.DataSource = null;
            grQuarter.DataBind();
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
        dtQuarter = objKpi.SelectQuarter(0);
        grQuarter.DataSource = dtQuarter;
        grQuarter.DataBind();
    }

    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        try
        {
  
            if (hfIsUpdate.Value == "N")
                lngID = objDB.GerMaxIDNumber("KPIQuarter", "QuarterId");
            else
                lngID = Convert.ToInt32(hfId.Value);

            clsCommonSetup objCommonSetup = new clsCommonSetup(lngID.ToString(), txtQuarter.Text.Trim(), (chkInActive.Checked == true ? "N" : "Y"), "N",
                Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "N", IsDelete);

            objKpi.InsertQuarter(objCommonSetup, TestNull(txtFrom.Text.ToString()), TestNull(txtTo.Text.ToString()), hfIsUpdate.Value, IsDelete);

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

    private String TestNull(string s)
    {
        if (String.IsNullOrEmpty(s))
            return  "0";
        else
        {
            if (s.Length > 2)
                s = s.Substring(0, 2);
            return s;
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

    protected void grQuarter_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grQuarter.PageIndex = e.NewPageIndex;
        this.OpenRecord();
    }

    protected void grQuarter_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                txtQuarter.Text = grQuarter.SelectedRow.Cells[1].Text;
                hfId.Value = grQuarter.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                txtFrom.Text = grQuarter.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                txtTo.Text = grQuarter.DataKeys[_gridView.SelectedIndex].Values[2].ToString();
                this.EntryMode(true);
                break;
        }
    }

    protected void grQuarter_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfId.Value) == false)
        {
            this.SaveData("Y");
        }
        else
        {
            lblMsg.Text = "Select a Country first from the list then try to delete.";
        }

        this.EntryMode(false);
    }

    
}
