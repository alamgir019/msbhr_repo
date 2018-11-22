using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Training_OtherTraining : System.Web.UI.Page
{
    TrainingManager objTrMgr = new TrainingManager();
    MasterTablesManager objTblmast = new MasterTablesManager();
    EmpInfoManager objEmp = new EmpInfoManager();
    DataTable dtList = new DataTable();
    DataTable personTable = new DataTable();

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
            this.CreateTable();
            Common.FillDropDownList(objTrMgr.SelectTrainingList("0"), ddlTrainingName, "TrainName", "TrainId", true);
            //Common.FillDropDownList_Nil(objTrMgr.SelectTrainingList("0"), ddlTrainingName);
            DataTable dtEmp = objEmp.SelectEmpNameWithID("A");           
            Common.FillDropDownList(dtEmp, ddlOrganizedBy, "EmpName", "EmpID", true);
        }
    }
    private void CreateTable()
    {
        //TraineeId,TraineeName,Designation,Dept
        personTable = new DataTable();
        personTable.Columns.AddRange(new DataColumn[2] 
                            { 
                              new DataColumn("ParticipantName", typeof(string)),
                              new DataColumn("Designation", typeof(string)),                              
                            });
        ViewState["dt"] = personTable;
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
    private bool ValidateAndSave(string Flag)
    {
        try
        {
            lblMsg.Text = "";
            switch (Flag)
            {
                case "Save":
                    if (ddlTrainingName.SelectedIndex <= 0)
                    {
                        lblMsg.Text = "Please Select Training Name.";
                        return false;
                    }

                    else if (ddlOrganizedBy.SelectedIndex <= 0)
                    {
                        lblMsg.Text = "Please Select Organized By.";
                        return false;
                    }

                    else if (grList.Rows.Count <= 0)
                    {
                        lblMsg.Text = "Please add participant.";
                        return false;
                    }
                    break;
                case "Add":
                    if (string.IsNullOrEmpty(txtPName.Text.Trim()) == true)
                    {
                        lblMsg.Text = "Please enter participant name.";
                        return false;
                    }
                    else if (string.IsNullOrEmpty(txtLocation.Text.Trim()) == true)
                    {
                        lblMsg.Text = "Please enter location name.";
                        return false;
                    }
                    else if (string.IsNullOrEmpty(txtVenue.Text.Trim()) == true)
                    {
                        lblMsg.Text = "Please enter venue name.";
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
    private void OpenRecord()
    {
            grList.DataSource = null;
            grList.DataBind();
            grOtherTraining.DataSource= objTrMgr.SelectOtherTrainingList("A");
            grOtherTraining.DataBind();
    }

    private void SaveData(string cmdType)
    {
        dsTraining objDS = new dsTraining();
        if (cmdType == "I")
        {
            hfId.Value = Common.getMaxId("TrOtherTrain", "OtherTrainId");
        }

        List<DataTable> tblList = new List<DataTable>();

        DataTable dtMst = objDS.Tables["TrOtherTrain"];
        DataRow nRow = dtMst.NewRow();

        nRow["OtherTrainId"] = Common.RoundDecimal(hfId.Value, 0);
        nRow["TrainId"] = ddlTrainingName.SelectedValue.ToString().Trim();
        nRow["StartDate"] = Common.ReturnDate(txtStrDate.Text.Trim());
        nRow["EndDate"] = Common.ReturnDate(txtEndDate.Text.Trim());
        nRow["Duration"] = Common.RoundDecimal(txtDuration.Text.Trim(),0);
        nRow["OrganizedBy"] = ddlOrganizedBy.SelectedValue.ToString().Trim();

        nRow["Remarks"] = txtRemarks.Text.ToString().Trim();

        nRow["Location"] = txtLocation.Text.ToString().Trim();
        nRow["Venue"] = txtVenue.Text.ToString().Trim();

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
        nRow["IsCertificate"] = chkInActive.Checked == true ? "Y" : "N";
        nRow["IsActive"] = chkInActive.Checked == true ? "N" : "Y";

        dtMst.Rows.Add(nRow);
        dtMst.AcceptChanges();

        tblList.Add(dtMst);

        //detail table
        DataTable dtDtl = objDS.Tables["TrOtherTrainDtls"];

        DataTable dtDtlInput = ViewState["dt"] as DataTable;
        int DtlId = Int32.Parse(Common.getMaxId("TrOtherTrainDtls", "OtherTrainDtlId"));
        // //TraineeId,TraineeName
        foreach (DataRow row in dtDtlInput.Rows)
        {
            DataRow nRowdtl = dtDtl.NewRow();
            nRowdtl["OtherTrainDtlId"] = DtlId;
            nRowdtl["OtherTrainId"] = Common.RoundDecimal(hfId.Value, 0); ;
            nRowdtl["ParticipantName"] = row["ParticipantName"].ToString().Trim();
            nRowdtl["Designation"] = row["Designation"].ToString().Trim();
            nRowdtl["InsertedBy"] = Session["USERID"].ToString().Trim();
            nRowdtl["InsertedDate"] = DateTime.Now;

            DtlId++;
            dtDtl.Rows.Add(nRowdtl);
        }

        dtDtl.AcceptChanges();
        tblList.Add(dtDtl);

        try
        {
            objTrMgr.SaveMultiTableData(tblList, cmdType == "D" ? "U" : cmdType);
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave("Save") == false)
        {
            return;
        }

        if (hfIsUpdate.Value == "N")
        {
            this.SaveData("I");
        }
        else
        {
            this.SaveData("U");
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
        this.CreateTable();
        lblMsg.Text = "";
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfId.Value) == false)
        {
            this.SaveData("D");
        }
        else
        {
            lblMsg.Text = "Select a item first from the list then try to delete.";
        }

        this.EntryMode(false);
    }
   
    protected void grList_RowDelete(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {
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
                    //TraineeId,TraineeName,Designation,Dept,IsResidential,Fundedby
                    personTable = ViewState["dt"] as DataTable;
                    txtPName.Text = Common.CheckNullString(grList.SelectedRow.Cells[2].Text.Trim());
                    txtDesignation.Text = Common.CheckNullString(grList.SelectedRow.Cells[3].Text.Trim());


                    DataRow[] drr = personTable.Select("ParticipantName='" + Common.CheckNullString(grList.SelectedRow.Cells[2].Text.Trim()) + "'");
                    for (int i = 0; i < drr.Length; i++)
                    {
                        drr[i].Delete();
                    }
                    personTable.AcceptChanges();
                    ViewState["dt"] = personTable;
                   
                }
                break;

            case ("Delete"):
                try
                {
                    personTable = ViewState["dt"] as DataTable;
                    DataRow[] drr = personTable.Select("ParticipantName='" + Common.CheckNullString(grList.SelectedRow.Cells[2].Text.Trim()) + "'");
                    for (int i = 0; i < drr.Length; i++)
                    {
                        drr[i].Delete();
                    }
                    personTable.AcceptChanges();

                    grList.DataSource = null;
                    grList.DataBind();

                    grList.DataSource = personTable;
                    grList.DataBind();

                }
                catch (Exception ex)
                {
                    lblMsg.Text = "Error : " + ex;
                    throw (ex);
                }
                break;
        }
    }
    protected void grOtherTraining_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    //OtherTrainId,TrainId,TrainName,StartDate,EndDate,Duration,OrganizedBy,OrganizedByName,Remarks,IsCertificate,IsActive
                    Common.EmptyTextBoxValues(this);
                    hfId.Value = grOtherTraining.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                    ddlTrainingName.SelectedValue = grOtherTraining.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim();
                    txtStrDate.Text = grOtherTraining.DataKeys[_gridView.SelectedIndex].Values[3].ToString().Trim();
                    txtEndDate.Text = grOtherTraining.DataKeys[_gridView.SelectedIndex].Values[4].ToString().Trim();
                    txtDuration.Text = grOtherTraining.DataKeys[_gridView.SelectedIndex].Values[5].ToString().Trim();
                    txtLocation.Text = grOtherTraining.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim();
                    txtVenue.Text = grOtherTraining.DataKeys[_gridView.SelectedIndex].Values[12].ToString().Trim();
                    ddlOrganizedBy.SelectedValue = grOtherTraining.DataKeys[_gridView.SelectedIndex].Values[6].ToString().Trim();
                    txtRemarks.Text = grOtherTraining.DataKeys[_gridView.SelectedIndex].Values[8].ToString();

                    if (grOtherTraining.DataKeys[_gridView.SelectedIndex].Values[9].ToString().Trim() == "Y")
                        chkCertificate.Checked = true;
                    else
                        chkCertificate.Checked = false;

                    if (grOtherTraining.DataKeys[_gridView.SelectedIndex].Values[10].ToString().Trim() == "Y")
                        chkInActive.Checked = false;
                    else
                        chkInActive.Checked = true;

                    this.CreateTable();
                    personTable = objTrMgr.SelectOtherTrainingDtlList(grOtherTraining.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim());
                    ViewState["dt"] = personTable;
                    grList.DataSource = null;
                    grList.DataBind();

                    grList.DataSource = personTable;
                    grList.DataBind();
                    this.EntryMode(true);
                    lblMsg.Text = "";
                }
                break;
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave("Add") == false)
        {
            return;
        }
        DataTable dt = ViewState["dt"] as DataTable;
        DataRow[] drr = dt.Select("ParticipantName='" + txtPName.Text.Trim()   +"'");
        for (int i = 0; i < drr.Length; i++)
        {
            drr[i].Delete();
        }
        dt.AcceptChanges();
        dt.Rows.Add(txtPName.Text.Trim (), txtDesignation.Text.ToString());
        grList.DataSource = dt;
        grList.DataBind();
    }
}
