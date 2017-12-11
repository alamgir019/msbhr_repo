using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class KPI_KPIScoringSetup : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    DataTable dtScoring = new DataTable();
    KPIManager objKpi = new KPIManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
            dtScoring.Rows.Clear();
            dtScoring.Dispose();
            grScoring.DataSource = null;
            grScoring.DataBind();
            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            Common.FillDropDownList(objKpi.SelectQuarter(0), ddlQuarter, "QuarterName", "QuarterId", false);
            Common.FillDropDownList(objKpi.SelectGroup(0), ddlGroup, "GroupName", "GroupId", false);
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
        dtScoring = objKpi.SelectScoring(0);
        grScoring.DataSource = dtScoring;
        grScoring.DataBind();
    }

    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        try
        {
            if (hfIsUpdate.Value == "N")
                lngID = objDB.GerMaxIDNumber("KPIScoring", "ScoringID");
            else
                lngID = Convert.ToInt32(hfId.Value);

            clsCommonSetup objCommonSetup = new clsCommonSetup(lngID.ToString(), txtRating.Text.Trim(), (chkInActive.Checked == true ? "N" : "Y"), "N",
                Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "N", IsDelete);

            objKpi.InsertScoring(objCommonSetup, ddlGroup.SelectedValue.ToString(),ddlQuarter.SelectedValue.ToString(),NullOrEmpty(txtYear.Text.ToString()),
                NullOrEmpty(txtMFrom.Text.ToString().Trim()),NullOrEmpty(txtMTo.Text.ToString().Trim()),txtScore.Text.Trim(), hfIsUpdate.Value, IsDelete);

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

    public String NullOrEmpty(string s)
    {
        if (String.IsNullOrEmpty(s)) 
            return "0";
        else
            return  s;
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

    protected void grScoring_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grScoring.PageIndex = e.NewPageIndex;
        this.OpenRecord();
    }

    protected void grScoring_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                //ScoringID,GroupId,QuarterId,MarksF,MarksTo

                txtRating.Text = grScoring.SelectedRow.Cells[5].Text;
                hfId.Value = grScoring.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                ddlGroup.SelectedValue = grScoring.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim();
                ddlQuarter.SelectedValue = grScoring.DataKeys[_gridView.SelectedIndex].Values[2].ToString().Trim();
                txtMFrom.Text = grScoring.DataKeys[_gridView.SelectedIndex].Values[3].ToString().Trim();
                txtMTo.Text = grScoring.DataKeys[_gridView.SelectedIndex].Values[4].ToString().Trim();
                txtYear.Text = grScoring.SelectedRow.Cells[3].Text;
                txtScore.Text = grScoring.DataKeys[_gridView.SelectedIndex].Values[5].ToString().Trim();
                this.EntryMode(true);
                break;
        }
    }

    protected void grScoring_SelectedIndexChanged(object sender, EventArgs e)
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
            lblMsg.Text = "Select a Scoring first from the list then try to delete.";
        }

        this.EntryMode(false);
    }


}
