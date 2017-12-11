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
public partial class CountrySetup : System.Web.UI.Page
{
    //String strName;
    DBConnector objDB = new DBConnector();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    DataTable dtCountry = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpadate.Value = "N";
            dtCountry.Rows.Clear();
            dtCountry.Dispose();
            grCountry.DataSource = null;
            grCountry.DataBind();
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
        dtCountry = objMasMgr.SelectCountry(0);
        //DataRow Myrow = dtCountry.NewRow();
        //Myrow["CountryID"] = "1";
        //Myrow["CountryName"] = "CC";
        //dtCountry.Rows.Add(Myrow);
        //dtCountry.AcceptChanges();
        grCountry.DataSource = dtCountry;
        grCountry.DataBind();
    }

    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        try
        {
            MasterTablesManager MasMgr = new MasterTablesManager();
            //Filling Class Properties with values
            if (hfIsUpadate.Value == "N")
                lngID = objDB.GerMaxIDNumber("CountryList", "CountryID");
            else
                lngID = Convert.ToInt32(hfID.Value);

            Country objCnt = new Country(lngID.ToString(), txtCountry.Text.Trim(), "N", Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "HRD", "N", "N");

            MasMgr.InsertCountry(objCnt, hfIsUpadate.Value, IsDelete);

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

    protected void grCountry_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grCountry.PageIndex = e.NewPageIndex;
        this.OpenRecord();
    }

    protected void grCountry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                txtCountry.Text = grCountry.SelectedRow.Cells[1].Text;
                hfID.Value = grCountry.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                this.EntryMode(true);
                break;
        }
    }

    protected void grCountry_SelectedIndexChanged(object sender, EventArgs e)
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
            lblMsg.Text = "Select a Country first from the list then try to delete.";
        }

        this.EntryMode(false);
    }

    protected void grCountry_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
}
