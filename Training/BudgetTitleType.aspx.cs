using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Training_BudgetTitleType : System.Web.UI.Page
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
        dtList = objTrMgr.SelectBudgetTitleType("A");
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
                    //BTitleTypeId,BTitleTypeName,InActive
                    hfBTitleTypeId.Value = grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                    txtBTitleType.Text = grList.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                    if (grList.DataKeys[_gridView.SelectedIndex].Values[2].ToString() == "Y")
                        chkInActive.Checked = true;
                    else
                        chkInActive.Checked = false;
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
                    if (string.IsNullOrWhiteSpace(txtBTitleType.Text))
                    {
                        lblMsg.Text = "Please enter Budget Title Type Name";
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
        if (string.IsNullOrEmpty(hfBTitleTypeId.Value) == false)
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
            hfBTitleTypeId.Value = Common.getMaxId("TrBudgetTitleType", "BTitleTypeId");
        }

        DataTable dtMst = objDS.Tables["TrBudgetTitleType"];
        DataRow nRow = dtMst.NewRow();

        nRow["BTitleTypeId"] = Common.RoundDecimal(hfBTitleTypeId.Value, 0);
        nRow["BTitleTypeName"] = txtBTitleType.Text.Trim();


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
        nRow["InActive"] = chkInActive.Checked == true ? "Y" : "N";

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