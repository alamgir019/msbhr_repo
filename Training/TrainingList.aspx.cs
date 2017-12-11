using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Training_TrainingList : System.Web.UI.Page
{
    TrainingManager objTrMgr = new TrainingManager();
    MasterTablesManager objTblmast = new MasterTablesManager();
    EmpInfoManager objEmp = new EmpInfoManager();
    SOFManager objSOFMgr = new SOFManager();
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
            DataTable dtEmp = objEmp.SelectEmpNameWithID("A");
            Common.FillDropDownList(objTrMgr.SelectTrainingList("0"), ddlTrainingName, "TrainName", "TrainId", true);
            //Common.FillDropDownList_Nil(objTrMgr.SelectTrainingList("0"), ddlTrainingName);
            Common.FillDropDownList(dtEmp, ddlTraineeName, "EmpName", "EmpID", true);
            Common.FillDropDownList(dtEmp, ddlOrganisedBy, "EmpName", "EmpID", true);
            Common.FillDropDownList(dtEmp, ddlSignBy1, "EmpName", "EmpID", true);
            Common.FillDropDownList(dtEmp, ddlSignBy2, "EmpName", "EmpID", true);
            Common.FillDropDownList(dtEmp, ddlSignBy3, "EmpName", "EmpID", true);
            //Common.FillDropDownList(objTrMgr.SelectLocation("0"), ddlLocation,"ClinicName","ClinicId",true );
            Common.FillDropDownList(objTrMgr.SelectLocation("0"), ddlLocation, "SalLocName", "SalLocId", true);
            Common.FillDropDownList(objSOFMgr.SelectProjectList(0), ddlFundedby, "ProjectName", "ProjectId", true);
            Common.FillDropDownList(objTrMgr.SelectTrainingVenue("A"), ddlVenue, "VenueName", "VenueId", true);
           // Common.FillDropDownList(objTrMgr.SelectScheduleList(), ddlSchedule, "ScheDate", "ScheduleID", true);
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
        //    dtList = objEmpMgr.SelectTraining();
            grList.DataSource = null;
            grList.DataBind();

            grTrainingListSetup.DataSource = objTrMgr.SelectTrainingSetupList("A");
            grTrainingListSetup.DataBind();
    }

    private void SaveData(string cmdType)
    {

        dsTraining objDS = new dsTraining();
        if (cmdType == "I")
        {
            hfId.Value = Common.getMaxId("TrTrainingListSetup", "TrainListId");
        }

        List<DataTable> tblList = new List<DataTable>();

        DataTable dtMst = objDS.Tables["TrTrainingListSetup"];
        DataRow nRow = dtMst.NewRow();

        nRow["TrainListId"] = Common.RoundDecimal(hfId.Value, 0);
        nRow["ScheduleID"] = Common.RoundDecimal(ddlSchedule.SelectedValue.ToString().Trim(), 0);
        nRow["VenueId"] = ddlVenue.SelectedValue.ToString().Trim();        
        nRow["onDate"] = Common.ReturnDate(txtDate.Text.Trim());
        nRow["onTime"] =  tsTime.Hour + ":" + tsTime.Minute + " " + tsTime.AmPm;

        nRow["SignBy1"] = ddlSignBy1.SelectedValue.ToString().Trim();
        nRow["SignBy2"] = ddlSignBy2.SelectedValue.ToString().Trim();
        nRow["SignBy3"] = ddlSignBy3.SelectedValue.ToString().Trim();
        nRow["CC"] = txtCC.Text.Trim();
        nRow["AdminGuideline"] = txtAdminGuideline.Text.Trim();
        nRow["OrganizedBy"] = ddlOrganisedBy.SelectedValue.ToString().Trim();
        nRow["Remarks"] = txtRemark.Text.Trim(); 
        if (cmdType == "I")
        {
            nRow["InsertedBy"] = Session["USERID"].ToString().Trim();
            nRow["InsertedDate"] =DateTime.Now;
            
        }
        else if (cmdType == "U")
        {
            nRow["UpdatedBy"] = Session["USERID"].ToString().Trim();
            nRow["UpdatedDate"] = DateTime.Now;

        }
        nRow["IsDeleted"] = (cmdType == "D" ? "Y" : "N");
        nRow["IsActive"] = (chkInActive.Checked == true ? "N" : "Y");

        dtMst.Rows.Add(nRow);
        dtMst.AcceptChanges();

        tblList.Add(dtMst);

        //detail table
        DataTable dtDtl = objDS.Tables["TrTrainingListSetupDtl"];
           
        DataTable dtDtlInput = ViewState["dt"] as DataTable;
        int TrainListDtlId = Int32.Parse(Common.getMaxId("TrTrainingListSetupDtl", "TrainListDtlId"));
        
        foreach(DataRow row in dtDtlInput.Rows)
        {
            DataRow nRowdtl = dtDtl.NewRow(); 
            nRowdtl["TrainListDtlId"] = TrainListDtlId;
            nRowdtl["TrainListId"] = Common.RoundDecimal(hfId.Value, 0); ;
            nRowdtl["TraineeId"] = row["TraineeId"].ToString().Trim();
            nRowdtl["FundedBy"] = row["Fundedby"].ToString().Trim();
            nRowdtl["IsResidential"] = row["IsResidential"].ToString().Trim();
            nRowdtl["InsertedBy"] = Session["USERID"].ToString().Trim();
            nRowdtl["InsertedDate"] = DateTime.Now;

            TrainListDtlId++;
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
           lblMsg.Text= ex.ToString();
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
        if (string.IsNullOrEmpty(hfId.Value) ==false)
        {
            this.SaveData("D");
        }
        else
        {
            lblMsg.Text = "Select a item first from the list then try to delete.";
        }

        this.EntryMode(false);
    }

    protected void grTrainingListSetup_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    // grid serial TrainListId,TrainId,TrainName,ScheduleID,ScheduleName,VenueId,VenueName,VenueAddress,onDate,onTime,SignBy1,SignBy1Name,
                    //SignBy2,SignBy2Name,SignBy3,SignBy3Name,CC,AdminGuideline,OrganizedBy,OrganizedByName,IsActive
                    //Common.EmptyTextBoxValues(this);
                    hfId.Value = grTrainingListSetup.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                   
                    ddlVenue.SelectedValue = grTrainingListSetup.DataKeys[_gridView.SelectedIndex].Values[5].ToString().Trim();
                    txtAddress.Text = grTrainingListSetup.DataKeys[_gridView.SelectedIndex].Values[7].ToString().Trim();
                    txtDate.Text = grTrainingListSetup.DataKeys[_gridView.SelectedIndex].Values[8].ToString().Trim();
                   // tsTime.Text = grList.DataKeys[_gridView.SelectedIndex].Values[5].ToString().Trim();
                    string time = grTrainingListSetup.DataKeys[_gridView.SelectedIndex].Values[9].ToString();
                    tsTime.Hour = Int32.Parse(time.Substring(0, time.IndexOf(":")));
                    tsTime.Minute = Int32.Parse(time.Substring((time.IndexOf(":") + 1),2));

                    if (time.Substring(time.Length-2,2) == "AM")
                        tsTime.AmPm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                    else
                        tsTime.AmPm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;

                    ddlSignBy1.SelectedValue = grTrainingListSetup.DataKeys[_gridView.SelectedIndex].Values[10].ToString().Trim();
                    ddlSignBy2.SelectedValue = grTrainingListSetup.DataKeys[_gridView.SelectedIndex].Values[12].ToString().Trim();
                    ddlSignBy3.SelectedValue = grTrainingListSetup.DataKeys[_gridView.SelectedIndex].Values[14].ToString().Trim();
                    txtCC.Text = grTrainingListSetup.DataKeys[_gridView.SelectedIndex].Values[16].ToString().Trim();
                    txtAdminGuideline.Text = grTrainingListSetup.DataKeys[_gridView.SelectedIndex].Values[17].ToString().Trim();
                    ddlOrganisedBy.SelectedValue= grTrainingListSetup.DataKeys[_gridView.SelectedIndex].Values[18].ToString().Trim();
                    if (grTrainingListSetup.DataKeys[_gridView.SelectedIndex].Values[20].ToString().Trim() == "N")
                        chkInActive.Checked = true;
                    else
                        chkInActive.Checked = false;
                    txtRemark.Text = grTrainingListSetup.DataKeys[_gridView.SelectedIndex].Values[21].ToString().Trim();
                    DataTable dt = objTrMgr.SelectTrainingScheduleInfo(grTrainingListSetup.DataKeys[_gridView.SelectedIndex].Values[3].ToString());
                    if (dt.Rows.Count > 0) 
                    {
                        ddlTrainingName.SelectedValue = dt.Rows[0]["TrainId"].ToString().Trim();
                        Common.FillDropDownList(objTrMgr.SelectScheduleList(dt.Rows[0]["TrainId"].ToString().Trim()), ddlSchedule, "ScheDate", "ScheduleID", true);
                        ddlSchedule.SelectedValue = grTrainingListSetup.DataKeys[_gridView.SelectedIndex].Values[3].ToString().Trim();
                        txtStrDate.Text = dt.Rows[0]["StrDate"].ToString().Trim();
                        txtEndDate.Text = dt.Rows[0]["EndDate"].ToString().Trim();
                        txtDuration.Text = Math.Round(Convert.ToDecimal(Common.ReturnZeroForNull(dt.Rows[0]["Duration"].ToString().Trim()))).ToString();
                            
                        txtNoOfPerson.Text = Math.Round(Convert.ToDecimal(Common.ReturnZeroForNull(dt.Rows[0]["NoofPerson"].ToString().Trim()))).ToString();
                        if (string.IsNullOrEmpty(dt.Rows[0]["SalLocId"].ToString().Trim()) == false)
                            ddlLocation.SelectedValue = dt.Rows[0]["SalLocId"].ToString().Trim();
                        chkIsSchedule.Checked = true;
                        // txtOrganisedby.Text = dt.Rows[0]["StrDate"].ToString().Trim(); IsActive
                    }
                    grList.DataSource = null;
                    grList.DataBind();

                    //Detail
                    this.CreateTable();
                    personTable= objTrMgr.SelectTrainingSetupListDtl(grTrainingListSetup.DataKeys[_gridView.SelectedIndex].Values[0].ToString());
                    grList.DataSource = personTable;
                    grList.DataBind();
                    ViewState["dt"] = personTable;

                    this.EntryMode(true);
                    lblMsg.Text = "";
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
                    //  //TraineeId,TraineeName,Designation,Dept,Fundedby,ProjectName,IsResidential
                    personTable = ViewState["dt"] as DataTable;          
                    ddlTraineeName.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                    txtDesignation.Text=grList.DataKeys[_gridView.SelectedIndex].Values[2].ToString();
                    txtDept.Text = grList.DataKeys[_gridView.SelectedIndex].Values[3].ToString();
                    if (grList.DataKeys[_gridView.SelectedIndex].Values[6].ToString() == "Y")
                        chkIsResidential.Checked = false;
                    else
                        chkIsResidential.Checked = true;   
                   ddlFundedby.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[4].ToString();
                    DataRow[] drr = personTable.Select("TraineeId='" + grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString() + "'");
                    for (int i = 0; i < drr.Length; i++)
                    {
                        drr[i].Delete();
                    }
                    personTable.AcceptChanges();
                    ViewState["dt"] = personTable;
                    //ViewState["dt"]=deleteGridRow("EmpID='" + grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString() + "'");
                }
                break;

            case ("Delete"):
                try
                {
                    personTable = ViewState["dt"] as DataTable;
                    DataRow[] drr = personTable.Select("TraineeId='" + grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString() + "'");
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

    protected void grList_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {

    }

    private void CreateTable()
    {
        //TraineeId,TraineeName,Designation,Dept,Fundedby,ProjectName,IsResidential
        personTable = new DataTable();
        personTable.Columns.AddRange(new DataColumn[7] 
                            { 
                              new DataColumn("TraineeId", typeof(string)),
                              new DataColumn("TraineeName", typeof(string)),
                              new DataColumn("Designation", typeof(string)),
                              new DataColumn("Dept", typeof(string)),
                              new DataColumn("Fundedby", typeof(string)),
                              new DataColumn("ProjectName", typeof(string)),
                              new DataColumn("IsResidential", typeof(string))
                            });
        ViewState["dt"] = personTable;
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
                        lblMsg.Text = "Please select Training Name";
                        return false;
                    }
                    if (ddlSchedule.SelectedIndex <= 0)
                    {
                        lblMsg.Text = "Please select Training Schedule";
                        return false;
                    }
                    if (ddlOrganisedBy.SelectedIndex <= 0)
                    {
                        lblMsg.Text = "Please select Organized By";
                        return false;
                    }
                    if (ddlVenue.SelectedIndex <= 0)
                    {
                        lblMsg.Text = "Please select Training Venue";
                        return false;
                    }

                    if (grList.Rows.Count == 0)
                    {
                        lblMsg.Text = "Please add Employee";
                        return false;
                    }
                    if (ddlSignBy1.SelectedIndex <= 0)
                    {
                        lblMsg.Text = "Please select Sign By1";
                        return false;
                    }
                    if (ddlSignBy2.SelectedIndex <= 0)
                    {
                        lblMsg.Text = "Please select Sign By2";
                        return false;
                    }
                    if (ddlSignBy3.SelectedIndex <= 0)
                    {
                        lblMsg.Text = "Please select Sign By3";
                        return false;
                    }
                    break;
                case "Add":
                    if (ddlTraineeName.SelectedIndex <= 0)
                    {
                        lblMsg.Text = "Please select Employee";
                        return false;
                    }
                    else if (ddlFundedby.SelectedIndex <=0)
                    {
                        lblMsg.Text = "Please Select Funded By.";
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

    private void ClearControl(string strFlag)
    {
        switch (strFlag)
        {
            case "ddlSchedule":
                {
                    txtStrDate.Text = "";
                    txtEndDate.Text = "";
                    txtDuration.Text = "";
                    txtNoOfPerson.Text = "";
                }
                break;
            case "ddlTrainee":
                {
                    txtDesignation.Text = "";
                    txtDept.Text = "";
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
        DataRow[] drr = dt.Select("TraineeId='" + ddlTraineeName.SelectedValue.ToString() + "'");
        for (int i = 0; i < drr.Length; i++)
        {
            drr[i].Delete();
        }
        dt.AcceptChanges();
        dt.Rows.Add(ddlTraineeName.SelectedValue.ToString(), ddlTraineeName.SelectedItem.Text.ToString(), txtDesignation.Text.ToString(), txtDept.Text.ToString(), ddlFundedby.SelectedValue.ToString(), ddlFundedby.SelectedItem.ToString(), chkIsResidential.Checked == true ? "N" : "Y");
        grList.DataSource = dt;
        grList.DataBind();

        ddlTraineeName.SelectedIndex = 0;
        txtDesignation.Text = "";
        txtDept.Text = ""; 
        ddlFundedby.SelectedIndex = 0;
        chkIsResidential.Checked = false; 

    }

    protected void ddlTraineeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTraineeName.SelectedIndex <= 0)
            ClearControl("ddlTrainee");
        if (Common.CheckNullString(ddlTraineeName.SelectedValue.ToString().Trim()) != "")
        {

            DataTable dt = objTrMgr.SelectEmployeeDetail(ddlTraineeName.SelectedValue.ToString().Trim());
            if (dt.Rows.Count > 0)
            {
                txtDesignation.Text = dt.Rows[0]["DesigName"].ToString().Trim();
                txtDept.Text = dt.Rows[0]["ClinicName"].ToString().Trim();
            }
        }
    }

    protected void ddlTrainingName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTrainingName.SelectedIndex <= 0)
            return;
        if (Common.CheckNullString(ddlTrainingName.SelectedValue.ToString().Trim()) != "")
        {
            Common.FillDropDownList(objTrMgr.SelectScheduleList(ddlTrainingName.SelectedValue.ToString().Trim()), ddlSchedule, "ScheDate", "ScheduleID", true);

            if (ddlSchedule.Items.Count > 1)
            {
                chkIsSchedule.Checked = true;
            }
            else
            { 
                chkIsSchedule.Checked = false; 
            }
        }  
    }

    protected void ddlVenue_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtAddress.Text = "";
        if (ddlVenue.SelectedIndex <= 0)
            return;

        DataTable dtVenue = objTrMgr.SelectTrainingVenue(ddlVenue.SelectedValue.ToString().Trim());
        if (dtVenue.Rows.Count > 0) 
        {
            txtAddress.Text = dtVenue.Rows[0]["VenueAddress"].ToString().Trim();
        }

    }

    protected void ddlSchedule_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSchedule.SelectedIndex <= 0)
            this.ClearControl("ddlSchedule");

        if (Common.CheckNullString(ddlSchedule.SelectedValue.ToString().Trim()) != "") 
        {
            DataTable dt = objTrMgr.SelectTrainingScheduleInfo(ddlSchedule.SelectedValue.ToString().Trim());
            if (dt.Rows.Count > 0)
            {
                txtStrDate.Text = dt.Rows[0]["StrDate"].ToString().Trim();
                txtEndDate.Text = dt.Rows[0]["EndDate"].ToString().Trim(); 
                txtDuration.Text = Math.Round(Convert.ToDecimal(Common.ReturnZeroForNull(dt.Rows[0]["Duration"].ToString().Trim()))).ToString();
                txtNoOfPerson.Text = Math.Round(Convert.ToDecimal(Common.ReturnZeroForNull(dt.Rows[0]["NoofPerson"].ToString().Trim()))).ToString();
                if (string.IsNullOrEmpty(dt.Rows[0]["SalLocId"].ToString().Trim()) == false)
                    ddlLocation.SelectedValue = dt.Rows[0]["SalLocId"].ToString().Trim();
            }
        }
    }

    protected void grList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

}
