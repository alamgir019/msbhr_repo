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

public partial class EIS_SalaryLocationSetup : System.Web.UI.Page
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
            grLocation.DataSource = null;
            grLocation.DataBind();
            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.EntryMode(false);
            this.OpenRecord(0);
            Common.FillDropDownList_Nil(objMasMgr.SelectCostCenterCode(0), ddlCostCenterCode);
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

    private void OpenRecord(int SalLocId)
    {       
        grLocation.DataSource = objMasMgr.SelectSalaryLocation(0);
        grLocation.DataBind();
    }

    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        try
        {
            MasterTablesManager MasMgr = new MasterTablesManager();

            if (hfIsUpadate.Value == "N")
                lngID = objDB.GerMaxIDNumber("SalaryLocation", "SalLocId");
            else
                lngID = Convert.ToInt32(hfID.Value);

            Desigation objCnt = new Desigation(lngID.ToString(), txtDesigation.Text.Trim(),ddlCostCenterCode.SelectedItem.ToString().Trim(),"N", Session["USERID"].ToString(), 
                Common.SetDateTime(DateTime.Now.ToString()), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "HRD", "N", "N", (ChkIsActive.Checked == true ? "N" : "Y"));

            MasMgr.InsertSalaryLocation(objCnt, hfIsUpadate.Value, IsDelete, (ChkIsActive.Checked == true ? "N" : "Y"));

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
    }

    protected void grDesigation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grLocation.PageIndex = e.NewPageIndex;
        this.OpenRecord(0);
    }

    protected void grDesigation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
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
    protected void grLocation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                txtDesigation.Text = grLocation.SelectedRow.Cells[1].Text;
                hfID.Value = grLocation.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                ChkIsActive.Checked = grLocation.SelectedRow.Cells[2].Text == "N" ? true : false;
                //if (string.IsNullOrEmpty(grLocation.DataKeys[_gridView.SelectedIndex].Values[1].ToString()) == false)
                //    ddlCostCenterCode.SelectedValue = grLocation.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                //else
                //    ddlCostCenterCode.SelectedIndex = 0;
                this.EntryMode(true);
                this.OpenRecord(Convert.ToInt32(grLocation.DataKeys[_gridView.SelectedIndex].Values[0].ToString()));
                break;
        }
    }
    protected void grLocation_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
