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

public partial class GradeSetup : System.Web.UI.Page
{    
    MasterTablesManager objMasMgr = new MasterTablesManager();
    DataTable dtGrade = new DataTable();
    DataTable dtDesig = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpadate.Value = "N";
            dtGrade.Rows.Clear();
            dtGrade.Dispose();
            grGrade.DataSource = null;
            grGrade.DataBind();
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

    private void OpenRecord(Int32 iGradeId)
    {
        dtDesig= objMasMgr.SelectDesignation(0);
        grDesig.DataSource = dtDesig;
        grDesig.DataBind();

        dtGrade = objMasMgr.SelectGrade(0);
        grGrade.DataSource = dtGrade;
        grGrade.DataBind();
    }

    private void SaveData(string IsDelete)
    {
        string sId = "";
        try
        {            
            if (hfIsUpadate.Value == "N")
                sId = Common.getMaxId("GradeList", "GradeID");
            else
                sId = hfID.Value.ToString(); 

            GradeEquiv objGrd = new GradeEquiv(sId, txtGrade.Text.Trim(), "N", Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), 
                "N", "N", (chkIsActive.Checked == true ? "N" : "Y"), (chkIsOTEntitle.Checked == true ? "Y" : "N"));

            objMasMgr.InsertGrade(grDesig,objGrd, hfIsUpadate.Value, IsDelete, (chkIsActive.Checked == true ? "N" : "Y"), "0", txtBasicMin.Text.Trim(), txtBasicMax.Text.Trim());

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
        try
        {
            this.SaveData("N");
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error : " + ex.Message.ToString();
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord(0);
    }

    protected void grGrade_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grGrade.PageIndex = e.NewPageIndex;
        this.OpenRecord(0);
    }

    protected void grGrade_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;       
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                txtGrade.Text = Common.CheckNullString(grGrade.SelectedRow.Cells[1].Text.Trim());
                hfID.Value = grGrade.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                chkIsOTEntitle.Checked = (grGrade.SelectedRow.Cells[2].Text == "Y" ? true  : false);
                chkIsActive.Checked = (grGrade.DataKeys[_gridView.SelectedIndex].Values[1].ToString() == "Y" ? false : true);
                txtBasicMin.Text = Common.CheckNullString(grGrade.SelectedRow.Cells[4].Text);
                txtBasicMax.Text = Common.CheckNullString(grGrade.SelectedRow.Cells[5].Text);
                this.EntryMode(true);
                this.OpenRecord(Convert.ToInt32(grGrade.DataKeys[_gridView.SelectedIndex].Values[0].ToString()));
                this.CheckGradeWsDesig(Convert.ToInt32(hfID.Value));
                TabContainer1.ActiveTabIndex = 0;
                break;
        }
    }

    protected void CheckGradeWsDesig(Int32 strGradeId)
    {
        string strDesigId = "";
        int i = 0;
        this.UnCheckAllDesig();
        DataTable dtGradeWsDesig = objMasMgr.SelectGradeWsDesignation(strGradeId);
        foreach (GridViewRow gRow in grDesig.Rows)
        {
            strDesigId = grDesig.DataKeys[i].Values[0].ToString();
            foreach (DataRow dRow in dtGradeWsDesig.Rows)
            {
                if (string.Compare(strDesigId, dRow["DesigId"].ToString()) == 0)
                {
                    CheckBox chkSel = new CheckBox();
                    chkSel = (CheckBox)gRow.Cells[0].FindControl("chkBox");
                    chkSel.Checked = true;
                    break;
                }
            }
            i++;
        }
    }

    protected void UnCheckAllDesig()
    {
        foreach (GridViewRow gRow in grDesig.Rows)
        {
            CheckBox chkSel = new CheckBox();
            chkSel = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            chkSel.Checked = false;
            continue;
        }
    }

    protected void grGrade_SelectedIndexChanged(object sender, EventArgs e)
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
}
