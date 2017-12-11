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
public partial class Training_TrainingNeedTypeSetup : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    TrainingManager objTM = new TrainingManager();
    DataTable dtDesigation = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpadate.Value = "N";
            dtDesigation.Rows.Clear();
            dtDesigation.Dispose();
            grDivision.DataSource = null;
            grDivision.DataBind();
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
            TabContainer1.ActiveTabIndex = 1;
        }
    }

    private void OpenRecord(int divisionId)
    {
        grLocation.DataSource = objTM.SelectTypeWiseSubType(divisionId);
        grLocation.DataBind();

        foreach (GridViewRow gRow in grLocation.Rows)
        {
            CheckBox chkBox = (CheckBox)gRow.FindControl("chkBox");

            if (Convert.ToDecimal(grLocation.DataKeys[gRow.RowIndex].Values[1]) > 0)
            {
                chkBox.Checked = true;
            }
            else
            {
                chkBox.Checked = false;
            }
        }

        grDivision.DataSource = objTM.SelectTrnNeedTypeList(0);
        grDivision.DataBind();
    }

    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        try
        {
            MasterTablesManager MasMgr = new MasterTablesManager();

            if (hfIsUpadate.Value == "N")
               // lngID = objDB.GerMaxIDNumber("PostingDivisionList", "PostingDivId");TrainingNeedType
                     lngID = objDB.GerMaxIDNumber("TrainingNeedType", "TrainingNeedTypeID");
            else
                lngID = Convert.ToInt32(hfID.Value);



            Desigation objCnt = new Desigation(lngID.ToString(), txtTraniniType.Text.Trim(), "N", Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "HRD", "N", "N", (ChkIsActive.Checked == true ? "N" : "Y"));

            objTM.InsTainingTypeWiseSubtype(grLocation, objCnt, hfIsUpadate.Value, IsDelete, (ChkIsActive.Checked == true ? "N" : "Y"));

            lblMsg.Text = Common.GetMessage(hfIsUpadate.Value.ToString(), IsDelete);
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

    protected void grDesigation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grDivision.PageIndex = e.NewPageIndex;
        this.OpenRecord(0);
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

                txtTraniniType.Text = grDivision.SelectedRow.Cells[1].Text;
                hfID.Value = grDivision.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                ChkIsActive.Checked = grDivision.DataKeys[_gridView.SelectedIndex].Values[1].ToString() == "N" ? true : false;
                this.EntryMode(true);
                this.OpenRecord(Convert.ToInt32(grDivision.DataKeys[_gridView.SelectedIndex].Values[0].ToString()));
                TabContainer1.ActiveTabIndex = 0;
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
