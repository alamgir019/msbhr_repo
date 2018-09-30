using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

public partial class Training_TrainingPlan : System.Web.UI.Page
{
    TrainingManager objTrMgr = new TrainingManager();
    MasterTablesManager objTblmast = new MasterTablesManager();
    EmpInfoManager objEmp = new EmpInfoManager();
    SOFManager objSOFMgr = new SOFManager();

    DataTable personTable=new DataTable();
    DataTable LevelTable = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //hfIsUpdate.Value = "N";
           // dtList.Rows.Clear();
            //dtList.Dispose();
           
            lblMsg.Text = "";
            this.EntryMode(false);
            this.OpenRecord();
            this.CreateTable();
            this.CreateTableLevel();
            //DataTable dtEmp = objEmp.SelectEmpNameWithID("A");
            Common.FillDropDownList(objTrMgr.SelectTrainingList("0"), ddlTrainingName, "TrainName", "TrainId", true);
            //Common.FillDropDownList_Nil(objTrMgr.SelectTrainingList("0"), ddlTrainingName);
            //Common.FillDropDownList(dtEmp, ddlCourseCoordinator, "EmpName", "EmpID", true);
            //Common.FillDropDownList_Nil(dtEmp, ddlRespectiveResource);
            //Common.FillDropDownList(dtEmp, ddlRespectiveResource, "EmpName", "EmpID", true);
            Common.FillDropDownList(objTrMgr.SelectTrainingVenue("A"), ddlVenue, "VenueName", "VenueId", true);
            Common.FillDropDownList(objEmp.GetEmpDesignation("A"), ddlParticipantLevel, "DesigName", "DesigId", true);
            Common.FillDropDownList(objSOFMgr.SelectProjectList(0), ddlFundedby, "ProjectName", "ProjectId", true);
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
            Common.EmptyTextBoxValues(this);
            btnSave.Text = "Save";
            hfIsUpdate.Value = "N";
            TabContainer1.ActiveTabIndex = 0;

            grList.DataSource = null;
            grList.DataBind();

            grLevelList.DataSource = null;
            grLevelList.DataBind();
            
        }
    }
    private void CreateTable()
    {
        personTable = new DataTable();
        personTable.Columns.AddRange(new DataColumn[2] 
                            { 
                              new DataColumn("RespectiveResourceId", typeof(string)),
                              new DataColumn("RespectiveResourceName", typeof(string))
                                                           
                            });
        ViewState["dt"] = personTable;
    }
    private void CreateTableLevel()
    {
        LevelTable = new DataTable();
        LevelTable.Columns.AddRange(new DataColumn[3] 
                            { 
                              new DataColumn("FundedBy", typeof(string)),
                              new DataColumn("ProjectName", typeof(string)),
                              new DataColumn("NoOfParticipants", typeof(string))
                                                           
                            });
        ViewState["dtLevel"] = LevelTable;
    }
    private void OpenRecord()
    {
        //    dtList = objEmpMgr.SelectTraining();
        this.CreateTable();
        grList.DataSource = null;
        grList.DataBind();

        this.CreateTableLevel();
        grLevelList.DataSource = null;
        grLevelList.DataBind();

        grTrainingPlan.DataSource = null;
        grTrainingPlan.DataBind();
        grTrainingPlan.DataSource=objTrMgr.SelectTrainingPlanList("A");
        grTrainingPlan.DataBind();
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
                        lblMsg.Text = "Please select Training Name.";
                        return false;
                    }
                    //else if (ddlParticipantLevel.SelectedIndex<= 0)
                    //{
                    //    lblMsg.Text = "Please Select Participant Level.";
                    //    return false;
                    //}
                    //else if (ddlCourseCoordinator.SelectedIndex <= 0)
                    //{
                    //    lblMsg.Text = "Please Select Course Coordinator.";
                    //    return false;
                    //}
                    else if (grList.Rows.Count == 0)
                    {
                        lblMsg.Text = "Please add Respective Resource.";
                        return false;
                    }
                    break;
            }
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void SaveData(string cmdType)
    {
        dsTraining objDS = new dsTraining();
        if (cmdType == "I")
            hfId.Value = Common.getMaxId("TrTrainingPlan", "TrainingPlanId");
        else
            hfId.Value = hfId.Value;

        List<DataTable> tblList = new List<DataTable>();

        DataTable dtMst = objDS.Tables["TrTrainingPlan"];
        DataRow nRow = dtMst.NewRow();

        nRow["TrainingPlanId"] = Common.RoundDecimal(hfId.Value, 0);
        nRow["TrainId"] = Common.RoundDecimal(ddlTrainingName.SelectedValue.ToString().Trim(), 0);
        nRow["TentativeDate"] = Common.ReturnDate(txtTentativeDate.Text.Trim());
        nRow["ParticipantLevel"] = Common.RoundDecimal(ddlParticipantLevel.SelectedValue.ToString().Trim(),0);
        nRow["VenueId"] = Common.RoundDecimal(ddlVenue.SelectedValue.ToString().Trim(),0);


        nRow["NoofParticipant"] = txtNoofParticipant.Text.Trim();
        nRow["ParticipantMatrix"] = txtParticipantMatrix.Text.Trim();
        nRow["Remarks"] = txtRemarks.Text.Trim();
        var match = Regex.Match(txtCourseCoordinator.Text.Trim(), "(^(\\w+(.)*\\s)+\\[)*(\\w+)");
        string empid = match.Groups[match.Groups.Count - 1].Value;
        nRow["CourseCoordinator"] = empid;       

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
        nRow["IsActive"] = chkInActive.Checked == true ? "N" : "Y";

        dtMst.Rows.Add(nRow);
        dtMst.AcceptChanges();

        tblList.Add(dtMst);

        //detail table
        DataTable dtDtl = objDS.Tables["TrTrainingPlanDtls"];

        DataTable dtDtlInput = ViewState["dt"] as DataTable;
        int DtlId = Int32.Parse(Common.getMaxId("TrTrainingPlanDtls", "TrainingPlanDtlId"));
        // //TraineeId,TraineeName,Designation,Dept,IsResidential,Fundedby
        foreach (DataRow row in dtDtlInput.Rows)
        {
            DataRow nRowdtl = dtDtl.NewRow();
            nRowdtl["TrainingPlanDtlId"] = DtlId;
            nRowdtl["TrainingPlanId"] = Common.RoundDecimal(hfId.Value, 0);
            nRowdtl["RespectiveResource"] = row["RespectiveResourceId"].ToString().Trim();
           
            nRowdtl["InsertedBy"] = Session["USERID"].ToString().Trim();
            nRowdtl["InsertedDate"] = DateTime.Now;

            DtlId++;
            dtDtl.Rows.Add(nRowdtl);
        }

        dtDtl.AcceptChanges();
        tblList.Add(dtDtl);

        DataTable dtDtlLevel = objDS.Tables["TrTrainingPlanLevelDtls"];

        DataTable dtDtlLevelInput = ViewState["dtLevel"] as DataTable;
        int DtlLevelId = Int32.Parse(Common.getMaxId("TrTrainingPlanLevelDtls", "TrainingPlanLevelDtlId"));
        // //TraineeId,TraineeName,Designation,Dept,IsResidential,Fundedby
        foreach (DataRow row in dtDtlLevelInput.Rows)
        {
            DataRow nRowdtlLevel = dtDtlLevel.NewRow();
            nRowdtlLevel["TrainingPlanLevelDtlId"] = DtlLevelId;
            nRowdtlLevel["TrainingPlanId"] = Common.RoundDecimal(hfId.Value, 0);
            nRowdtlLevel["NoOfParticipants"] = row["NoOfParticipants"].ToString().Trim();
            nRowdtlLevel["FundedBy"] = row["FundedBy"].ToString().Trim();

            nRowdtlLevel["InsertedBy"] = Session["USERID"].ToString().Trim();
            nRowdtlLevel["InsertedDate"] = DateTime.Now;

            DtlLevelId++;
            dtDtlLevel.Rows.Add(nRowdtlLevel);
        }

        dtDtlLevel.AcceptChanges();
        tblList.Add(dtDtlLevel);

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
    protected void grTrainingPlan_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    // TrainingPlanId,TrainId,TrainName,TentativeDate,ParticipantLevel,DesigName,VenueId,VenueName,NoofParticipant,ParticipantMatrix,Remarks,CourseCoordinator,EmpName,IsActive
                    //Common.EmptyTextBoxValues(this);
                    hfId.Value = grTrainingPlan.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim();
                    ddlTrainingName.SelectedValue = grTrainingPlan.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim();
                    
                    txtTentativeDate.Text = grTrainingPlan.DataKeys[_gridView.SelectedIndex].Values[3].ToString().Trim();
                   ddlParticipantLevel.SelectedValue = grTrainingPlan.DataKeys[_gridView.SelectedIndex].Values[4].ToString().Trim();
                   ddlVenue.SelectedValue = grTrainingPlan.DataKeys[_gridView.SelectedIndex].Values[6].ToString().Trim();

                    txtNoofParticipant.Text= grTrainingPlan.DataKeys[_gridView.SelectedIndex].Values[8].ToString().Trim();
                    txtParticipantMatrix.Text = grTrainingPlan.DataKeys[_gridView.SelectedIndex].Values[9].ToString().Trim();
                    txtRemarks.Text = grTrainingPlan.DataKeys[_gridView.SelectedIndex].Values[10].ToString().Trim();
                    txtCourseCoordinator.Text = grTrainingPlan.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim();

                    
                    if (grTrainingPlan.DataKeys[_gridView.SelectedIndex].Values[13].ToString().Trim() == "N")
                        chkInActive.Checked = true;
                    else
                        chkInActive.Checked = false;

                    grList.DataSource = null;
                    grList.DataBind();

                    //Detail1
                    this.CreateTable();
                    personTable = objTrMgr.SelectTrainingPlanDtlsList(grTrainingPlan.DataKeys[_gridView.SelectedIndex].Values[0].ToString());
                    grList.DataSource = personTable;
                    grList.DataBind();
                    ViewState["dt"] = personTable;

                    //Detail2
                    this.CreateTableLevel();
                    LevelTable = objTrMgr.SelectTrainingPlanLevelDtlsList(grTrainingPlan.DataKeys[_gridView.SelectedIndex].Values[0].ToString());
                    grLevelList.DataSource = LevelTable;
                    grLevelList.DataBind();
                    ViewState["dtLevel"] = LevelTable;

                    this.EntryMode(true);
                    lblMsg.Text = "";
                    TabContainer1.ActiveTabIndex = 0; 
                }
                break;
        }
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
                    personTable = ViewState["dt"] as DataTable;     
                    txtRespectiveResource.Text = grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim();

                    DataRow[] drr = personTable.Select("RespectiveResourceId='" + grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim() + "'");
                    for (int i = 0; i < drr.Length; i++)
                    {
                        drr[i].Delete();
                    }
                    personTable.AcceptChanges();
                    ViewState["dt"] = personTable;
                }
                break;

            case ("Delete"):
               
                {
                    personTable = ViewState["dt"] as DataTable;
                    DataRow[] drr = personTable.Select("RespectiveResourceId='" + grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim() + "'");
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
                break;
        }
    }
    protected void grList_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable dt = ViewState["dt"] as DataTable;
        var match = Regex.Match(txtRespectiveResource.Text.Trim(), "(^(\\w+(.)*\\s)+\\[)*(\\w+)");
        string empid = match.Groups[match.Groups.Count - 1].Value;
        DataRow[] drr = dt.Select("RespectiveResourceId='" + empid + "'");
        for (int i = 0; i < drr.Length; i++)
        {
            drr[i].Delete();
        }
        dt.AcceptChanges();
        var matchName = Regex.Match(txtRespectiveResource.Text.Trim(), "^(\\w+(.)*\\s)+[^\\[]");
        string empName = match.Groups[match.Groups.Count - 3].Value;
        dt.Rows.Add(empid, empName);
        grList.DataSource = dt;
        grList.DataBind();

        txtRespectiveResource.Text = ""; 
    }
    protected void btnAddLevel_Click(object sender, EventArgs e)
    {
        DataTable dtLevel = ViewState["dtLevel"] as DataTable;
        DataRow[] drr = dtLevel.Select("FundedBy=" + ddlFundedby.SelectedValue.ToString().Trim());
        for (int i = 0; i < drr.Length; i++)
        {
            drr[i].Delete();
        }
        dtLevel.AcceptChanges();
        dtLevel.Rows.Add(ddlFundedby.SelectedValue.ToString().Trim(), ddlFundedby.SelectedItem.ToString().Trim(), txtPartNo.Text.Trim());
        grLevelList.DataSource = dtLevel;
        grLevelList.DataBind();

        ddlFundedby.SelectedIndex = 0;
        txtPartNo.Text = "";
    }
    protected void grLevelList_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    LevelTable = ViewState["dtLevel"] as DataTable;
                    ddlFundedby.SelectedValue = grLevelList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim();
                    txtPartNo.Text = grLevelList.DataKeys[_gridView.SelectedIndex].Values[2].ToString().Trim();
                    DataRow[] drr = LevelTable.Select("FundedBy='" + grLevelList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim() + "'");
                    for (int i = 0; i < drr.Length; i++)
                    {
                        drr[i].Delete();
                    }
                    LevelTable.AcceptChanges();
                    ViewState["dtLevel"] = LevelTable;
                }
                break;

            case ("DeleteClick"):
                {
                    LevelTable = ViewState["dtLevel"] as DataTable;
                    DataRow[] drr = LevelTable.Select("FundedBy='" + grLevelList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim() + "'");
                    for (int i = 0; i < drr.Length; i++)
                    {
                        drr[i].Delete();
                    }
                    LevelTable.AcceptChanges();

                    grLevelList.DataSource = null;
                    grLevelList.DataBind();

                    grLevelList.DataSource = LevelTable;
                    grLevelList.DataBind();
                }
                break;
        }
    }
}
