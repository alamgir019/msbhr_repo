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

public partial class EIS_Action : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    MasterTablesManager objMasMgr = new MasterTablesManager();    
    DataTable dtAction = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
           
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
        dtAction = objMasMgr.SelectAction(0, "");
        grAction.DataSource = dtAction;
        grAction.DataBind();

        if (dtAction.Rows.Count > 0)
        {
            foreach (GridViewRow gRow in grAction.Rows)
            {

                if (gRow.Cells[4].Text == "A")
                    gRow.Cells[4].Text = "Advice";
                else if (gRow.Cells[4].Text == "R")
                    gRow.Cells[4].Text = "Additional Responsibility";
                else if (gRow.Cells[4].Text == "C")
                    gRow.Cells[4].Text = "Confirmation";
                //else if (gRow.Cells[4].Text == "N")
                //    gRow.Cells[4].Text = "Separation";
                else if (gRow.Cells[4].Text == "D")
                    gRow.Cells[4].Text = "Disciplinary";
                else if (gRow.Cells[4].Text == "P")
                    gRow.Cells[4].Text = "Promotion/Transfer/Re-designation";
                else  if (gRow.Cells[4].Text == "T")
                    gRow.Cells[4].Text = "TDY/Acting";
                else if (gRow.Cells[4].Text == "S")
                    gRow.Cells[4].Text = "Separation";
                else if (gRow.Cells[4].Text == "M")
                    gRow.Cells[4].Text = "Amendment of Salary";
            }
        }
    }


    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        try
        {
            //Filling Class Properties with values
            if (hfIsUpdate.Value == "N")
                lngID = objDB.GerMaxIDNumber("ActionList", "ActionId");
            else
                lngID = Convert.ToInt32(hfID.Value);

            clsAction objAction = new clsAction(lngID.ToString(), txtAction.Text.Trim(), txtDescription.Text.Trim(), txtActionType.Text.Trim(),
                ddlActionNature.SelectedValue.ToString(), (chkIsActive.Checked == true ? "N" : "Y"), "N", Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));

            objMasMgr.InsertAction(objAction, hfIsUpdate.Value, IsDelete);

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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.SaveData("N");
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfID.Value) == false)
        {
            this.SaveData("Y");
        }
        else
        {
            lblMsg.Text = "Select an Action first from the list then try to delete.";
        }

        this.EntryMode(false);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
        lblMsg.Text = ""; 
    }
    protected void grAction_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                txtAction.Text = Common.CheckNullString(grAction.SelectedRow.Cells[1].Text.Trim() );
                hfID.Value = grAction.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                txtDescription.Text = Common.CheckNullString(grAction.SelectedRow.Cells[2].Text.Trim());
                txtActionType.Text = Common.CheckNullString(grAction.SelectedRow.Cells[3].Text.Trim());
                ddlActionNature.SelectedValue = grAction.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                chkIsActive.Checked = (grAction.SelectedRow.Cells[5].Text == "Y" ? false : true);
                this.EntryMode(true);
                break;
        }
    }
}
