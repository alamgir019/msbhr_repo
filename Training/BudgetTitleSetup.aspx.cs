using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Training_BudgetTitleSetup : System.Web.UI.Page
{
    TrainingManager objTrMgr = new TrainingManager();
    DataTable dtList = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
            dtList.Rows.Clear();
            dtList.Dispose();
            grList.DataSource = null;
            grList.DataBind();
            Common.EmptyTextBoxValues(this);
            Common.FillDropDownList_Nil(objTrMgr.SelectBudgetTitleType("A"), ddlBTitleType);
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
        dtList = objTrMgr.SelectBudgetTitleList(0,0);
        grList.DataSource = dtList;
        grList.DataBind();

        //if (grList.Rows.Count > 0)
        //{
        //    foreach (GridViewRow gRow in grList.Rows)
        //    {
        //        if (gRow.Cells[1].Text.Trim() == "R")
        //            gRow.Cells[1].Text = "Residential Cost";
        //        else if (gRow.Cells[1].Text.Trim() == "N")
        //            gRow.Cells[1].Text = "Non-Residential Cost";
        //        else if (gRow.Cells[1].Text.Trim() == "C")
        //            gRow.Cells[1].Text = "Training Course Fee";
        //    }
        //}
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
                {
                    //BTitleId,BTitleType,BTitle,IsActive
                    hfBTitleId.Value = grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                    ddlBTitleType.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                    txtBTitle.Text = grList.DataKeys[_gridView.SelectedIndex].Values[2].ToString();
                    if (grList.DataKeys[_gridView.SelectedIndex].Values[3].ToString() == "Y")
                        chkIsActive.Checked = true;
                    else
                        chkIsActive.Checked = false;
                    this.EntryMode(true);
                    lblMsg.Text = "";
                }
                break;

        }
    }

    private bool ValidateAndSave(string Flag)
    {
        try
        {
            lblMsg.Text = "";
            switch (Flag)
            {
                case "Save":
                    if (ddlBTitleType.SelectedIndex == 0)
                    {
                        lblMsg.Text = "Please select Budget Title";
                        return false;
                    }
                    else if (string.IsNullOrWhiteSpace(txtBTitle.Text))
                    {
                        lblMsg.Text = "Please enter Budget Title Name";
                        return false;
                    }
                    break;
            }
            return true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave("Save"))
        {
            if (hfIsUpdate.Value == "N")
            {
                this.SaveData("I");
            }
            else
            {
                this.SaveData("U");
            }
        }
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
        if (string.IsNullOrEmpty(hfBTitleId.Value) == false)
        {
            this.SaveData("D");
        }
        else
        {
            lblMsg.Text = "Select a item first from the list then try to delete.";
        }

        this.EntryMode(false);
    }
    private void SaveData(string cmdType)
    {
        dsTraining objDS = new dsTraining();
        if (cmdType == "I")
        {
            hfBTitleId.Value = Common.getMaxId("TrBudgetTitle", "BTitleId");
        }

        DataTable dtMst = objDS.Tables["TrBudgetTitle"];
        DataRow nRow = dtMst.NewRow();

        nRow["BTitleId"] = Common.RoundDecimal(hfBTitleId.Value, 0);
        nRow["BTitleType"] = ddlBTitleType.SelectedValue;
        nRow["BTitle"] = txtBTitle.Text.ToString().Trim();


        if (cmdType == "I")
        {
            nRow["InsertedBy"] = Session["USERID"].ToString().Trim();
            nRow["InsertedDate"] = DateTime.Now;

        }
        else if (cmdType == "U")
        {
            nRow["UpdatedBy"] = Session["USERID"].ToString().Trim();
            nRow["UpdatedDate"] = DateTime.Now;

        }
        nRow["IsDeleted"] = cmdType == "D" ? "Y" : "N";
        nRow["IsActive"] = chkIsActive.Checked == true ? "Y" : "N";

        dtMst.Rows.Add(nRow);
        dtMst.AcceptChanges();

        try
        {
            objTrMgr.SaveData(dtMst, cmdType == "D" ? "U" : cmdType);
            lblMsg.Text = Common.GetMessage(cmdType);
            Common.EmptyTextBoxValues(this);
            this.EntryMode(false);
            this.OpenRecord();
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.ToString();
        }
    }
}