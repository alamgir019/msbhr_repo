using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Training_TrainingScheduleSummery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
        //    dtList = objEmpMgr.SelectTraining();
        //    grList.DataSource = dtList;
        //    grList.DataBind();
    }

    private void SaveData(string IsDelete)
    {
        //try
        //{
        //    if (hfIsUpdate.Value == "Y")
        //        hfId.Value = hfId.Value;
        //    else
        //        hfId.Value = Common.getMaxId("TrainingList", "TrainingId");

        //    clsCommonSetup objCommonSetup = new clsCommonSetup(hfId.Value.ToString(), txtName.Text.Trim(), (chkInActive.Checked == true ? "N" : "Y"), "N",
        //        Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "N", IsDelete);

        //    objEmpMgr.InsertTraining(objCommonSetup, hfIsUpdate.Value, IsDelete);

        //    lblMsg.Text = Common.GetMessage(hfIsUpdate.Value.ToString(), IsDelete);
        //    Common.EmptyTextBoxValues(this);
        //    this.EntryMode(false);
        //    this.OpenRecord();
        //}
        //catch (Exception ex)
        //{
        //    lblMsg.Text = "";
        //    throw (ex);
        //}
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfId.Value) == false)
        {
            this.SaveData("Y");
        }
        else
        {
            lblMsg.Text = "Select a item first from the list then try to delete.";
        }

        this.EntryMode(false);
    }

    protected void grList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                //txtName.Text = Common.CheckNullString(grList.SelectedRow.Cells[1].Text.Trim());
                hfId.Value = grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                if (Common.CheckNullString(grList.SelectedRow.Cells[2].Text.Trim()) == "Y")
                    chkInActive.Checked = false;
                else
                    chkInActive.Checked = true;
                this.EntryMode(true);
                break;
        }
    }
}
