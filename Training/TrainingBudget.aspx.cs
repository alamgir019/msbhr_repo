using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

public partial class Training_TrainingBudget : System.Web.UI.Page
{
    TrainingManager objTrMgr = new TrainingManager();
    MasterTablesManager objTblmast = new MasterTablesManager();
    EmpInfoManager objEmp = new EmpInfoManager();
    SOFManager objSOFMgr = new SOFManager();
    DataTable budgetTable = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
            grList.DataSource = null;
            grList.DataBind();
            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.EntryMode(false);
            this.OpenRecord();

            Common.FillDropDownList_Nil(objTrMgr.SelectTrainingList("0"), ddlTrainingName);
            Common.FillDropDownList_Nil(objTrMgr.SelectTrainingVenue("A"), ddlVenue);


            //DataTable dtEmp = objEmp.SelectEmpNameWithID("A");
            //dtEmp = objEmp.SelectIntEmpWithID();
            //Common.FillDropDownList(dtEmp, ddlPreparedBy, "EmpName", "EmpID", true);
            //Common.FillDropDownList(dtEmp, ddlReviewedBy, "EmpName", "EmpID", true);
            //Common.FillDropDownList(dtEmp, ddlRecommend1, "EmpName", "EmpID", true);
            //Common.FillDropDownList(dtEmp, ddlRecommend2, "EmpName", "EmpID", true);
            //Common.FillDropDownList(dtEmp, ddlRecommend3, "EmpName", "EmpID", true);
            //Common.FillDropDownList(dtEmp, ddlApprovedBy, "EmpName", "EmpID", true);
            this.CreateTable();
        }
    }
    protected void CreateTable()
    {
        budgetTable = new DataTable();
        budgetTable.Columns.AddRange(new DataColumn[9] 
                            { 
                              new DataColumn("BudgetId", typeof(string)),
                              new DataColumn("BTitleType", typeof(string)),
                              new DataColumn("BudgetType", typeof(string)),
                              new DataColumn("BTitleId", typeof(string)),
                              new DataColumn("BTitleName", typeof(string)),
                              new DataColumn("UnitCost", typeof(string)),
                              new DataColumn("ParticipantNo", typeof(string)),
                              new DataColumn("DaysNo", typeof(string)),
                              new DataColumn("TotalTaka", typeof(string)) });
        ViewState["dt"] = budgetTable;
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
        Common.FillDropDownList_Nil(objTrMgr.SelectBudgetTitleType("A"), ddlBTitleType);
        grList.DataSource = null;
        grList.DataBind();
        grTrainingBudget.DataSource = objTrMgr.SelectTrainingBudgetList("A");
        grTrainingBudget.DataBind();
    }
    private void ClearControl(string strFlag)
    {
        switch (strFlag)
        {
            case "ddlBTitleType":
                {
                    //txtStrDate.Text = "";
                    //txtEndDate.Text = "";
                    //txtDuration.Text = "";
                    //txtNoOfPerson.Text = "";
                    //txtFundedBy.Text = "";
                    //txtCourseCoordinator.Text = "";
                    //txtResidential.Text = "";
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
                    if (ddlTrainingName.SelectedIndex <= 0)
                    {
                        lblMsg.Text = "Please Select Training Name.";
                        return false;
                    }
                    else if (txtCreateDate.Text=="")
                    {
                        lblMsg.Text = "Please Enter Date";
                        return false;
                    }
                    else if (ddlVenue.SelectedIndex<=0)
                    {
                        lblMsg.Text = "Please Select Venue";
                        return false;
                    }
                    else if (txtParticipentLevel.Text=="")
                    {
                        lblMsg.Text = "Please Enter Participant Level";
                        return false;
                    }
                    else if (txtPreparedBy.Text.Trim()=="")
                    {
                        lblMsg.Text = "Please Select Prepared By";
                        return false;
                    }
                    else if (txtReviewedBy.Text.Trim() == "")
                    {
                        lblMsg.Text = "Please Select Reviewed By";
                        return false;
                    }
                    else if (txtRecommend1.Text.Trim() == "")
                    {
                        lblMsg.Text = "Please Select Remommend By";
                        return false;
                    }
                    //else if (ddlRecommend2.SelectedIndex <= 0)
                    //{
                    //    lblMsg.Text = "Please Select Recommend By";
                    //    return false;
                    //}
                    //else if (ddlRecommend3.SelectedIndex <= 0)
                    //{
                    //    lblMsg.Text = "Please Select Recommend By";
                    //    return false;
                    //}
                    else if (txtApprovedBy.Text.Trim() == "")
                    {
                        lblMsg.Text = "Please Select Approved By";
                        return false;
                    }
                    else if (grList.Rows.Count == 0)
                    {
                        lblMsg.Text = "Please add Budget Title";
                        return false;
                    }
                    else if (ddlTrainingSchedule.SelectedIndex<=0)
                    {
                        lblMsg.Text = "Please Select Training Schedule";
                        return false;
                    }
                    break;

                case "Add":
                    if (ddlBTitleType.SelectedIndex <= 0)
                    {
                        lblMsg.Text = "Please select Budget Title Type";
                        return false;
                    }
                    else if (ddlBTitle.SelectedIndex<=0)
                    {
                        lblMsg.Text = "Please select Budget";
                    }
                    else if (txtUnitCost.Text == "")
                    {
                        lblMsg.Text = "Please Enter Unit Cost";
                        return false;
                    }
                    else if (txtParticipantNo.Text == "")
                    {
                        lblMsg.Text = "Please Enter Number of participents";
                        return false;
                    }
                    else if (txtDaysNo.Text=="")
                    {
                        lblMsg.Text = "Please Enter Number of days";
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
        {
            hfId.Value = Common.getMaxId("TrTrainingBudget", "BudgetId");
        }
        List<DataTable> tblList = new List<DataTable>();

        DataTable dtMst = objDS.Tables["TrTrainingBudget"];
        DataRow nRow = dtMst.NewRow();
        

        nRow["BudgetId"] = Common.RoundDecimal(hfId.Value, 0);
        nRow["TrainId"] = Common.RoundDecimal(ddlTrainingName.SelectedValue.ToString().Trim(), 0);
        nRow["ScheduleID"] = Common.RoundDecimal(ddlTrainingSchedule.SelectedValue.ToString(), 0);
        nRow["VenueId"] = Common.RoundDecimal(ddlVenue.SelectedValue.ToString().Trim(), 0);
        nRow["CreateDate"] = Convert.ToDateTime(txtCreateDate.Text);
        //nRow["FundedBy"] = Common.RoundDecimal(txtFundedBy.Text.Trim(), 0);
        nRow["ParticipantLevel"] = txtParticipentLevel.Text.Trim();
        nRow["TotalParticipant"] = Common.RoundDecimal(txtNoOfPerson.Text.Trim(), 0);
        nRow["Period"] = Common.RoundDecimal(hfDuration.Value, 0);

        //nRow["PreparedBy"] = txtPreparedBy.Text.Trim();
        var match = Regex.Match(txtPreparedBy.Text.Trim(), "(^(\\w+(.)*\\s)+\\[)*(\\w+)");
        string empid = match.Groups[match.Groups.Count - 1].Value;
        nRow["PreparedBy"] = empid;
        //nRow["ReviewedBy"] = txtReviewedBy.Text.Trim();
        match = Regex.Match(txtReviewedBy.Text.Trim(), "(^(\\w+(.)*\\s)+\\[)*(\\w+)");
        empid = match.Groups[match.Groups.Count - 1].Value;
        nRow["ReviewedBy"] = empid;
        //nRow["RecomBy1"] = txtRecommend1.Text.Trim();
        match = Regex.Match(txtRecommend1.Text.Trim(), "(^(\\w+(.)*\\s)+\\[)*(\\w+)");
        empid = match.Groups[match.Groups.Count - 1].Value;
        nRow["RecomBy1"] = empid;
        //nRow["RecomBy2"] = txtRecommend2.Text.Trim();
        match = Regex.Match(txtRecommend2.Text.Trim(), "(^(\\w+(.)*\\s)+\\[)*(\\w+)");
        empid = match.Groups[match.Groups.Count - 1].Value;
        nRow["RecomBy2"] = empid;
        //nRow["RecomBy3"] = txtRecommend3.Text.Trim();
        match = Regex.Match(txtRecommend3.Text.Trim(), "(^(\\w+(.)*\\s)+\\[)*(\\w+)");
        empid = match.Groups[match.Groups.Count - 1].Value;
        nRow["RecomBy3"] = empid;
        //nRow["ApprovedBy"] = txtApprovedBy.Text.Trim();
        match = Regex.Match(txtApprovedBy.Text.Trim(), "(^(\\w+(.)*\\s)+\\[)*(\\w+)");
        empid = match.Groups[match.Groups.Count - 1].Value;
        nRow["ApprovedBy"] = empid;


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
        DataTable dtDtl = objDS.Tables["TrTrainingBudgetDtls"];

        DataTable dtDtlInput = ViewState["dt"] as DataTable;
        //int ResultDtlId = Int32.Parse(Common.getMaxId("TrTrainingBudgetDtls", "ResultDtlId"));
        //TraineeId,TraineeName,Designation,Dept,IsResidential,Fundedby
        foreach (DataRow row in dtDtlInput.Rows)
        {
            DataRow nRowdtl = dtDtl.NewRow();
            nRowdtl["BudgetId"] = Common.RoundDecimal(hfId.Value, 0); ;
            nRowdtl["BTitleId"] = row["BTitleId"].ToString().Trim();
            nRowdtl["UnitCost"] = row["UnitCost"].ToString().Trim();
            nRowdtl["ParticipantNo"] = row["ParticipantNo"].ToString().Trim();
            nRowdtl["DaysNo"] = row["DaysNo"].ToString().Trim();
            nRowdtl["InsertedBy"] = Session["USERID"].ToString().Trim();
            nRowdtl["InsertedDate"] = DateTime.Now;

            dtDtl.Rows.Add(nRowdtl);
        }

        dtDtl.AcceptChanges();
        tblList.Add(dtDtl);


        try
        {
            objTrMgr.SaveMultiTableData(tblList, cmdType == "D" ? "U" : cmdType);
            lblMsg.Text = Common.GetMessage(cmdType);
            Common.EmptyTextBoxValues(this);
            //ddlTrainingName.Items.Clear();
            //ddlVenue.Items.Clear();
            //ddlBTitleType.Items.Clear();
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
    protected void ddlTrainingName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTrainingName.SelectedIndex <= 0)
            return;
        if (Common.CheckNullString(ddlTrainingName.SelectedValue.ToString().Trim()) != "")
        {
            Common.FillDropDownList(objTrMgr.SelectScheduleList(ddlTrainingName.SelectedValue.ToString().Trim()), ddlTrainingSchedule, "ScheDate", "ScheduleID", true);

        }  

        //if (ddlTrainingName.SelectedIndex <= 0)
        //    return;
        //DataTable dt = objTrMgr.SelectScheduleList(ddlTrainingName.SelectedValue.ToString().Trim());
        //if (dt.Rows.Count > 0)
        //    {
        //        //txtFundedBy.Text = dt.Rows[0]["ProjectName"].ToString().Trim();
        //        txtNoOfPerson.Text = Math.Round(Convert.ToDecimal(Common.ReturnZeroForNull(dt.Rows[0]["NoofPerson"].ToString().Trim()))).ToString();
        //        txtStrDate.Text = dt.Rows[0]["StrDate"].ToString().Trim();
        //        txtEndDate.Text = dt.Rows[0]["EndDate"].ToString().Trim();
        //        hfScheduleID.Value = dt.Rows[0]["ScheduleID"].ToString().Trim();
        //        hfDuration.Value = dt.Rows[0]["Duration"].ToString().Trim();
        //    }
    }
    protected void ddlTrainingSchedule_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTrainingSchedule.SelectedIndex <= 0)
            this.ClearControl("ddlTrainingSchedule");

        if (Common.CheckNullString(ddlTrainingSchedule.SelectedValue.ToString().Trim()) != "")
        {
            DataTable dt = objTrMgr.SelectTrainingScheduleInfo(ddlTrainingSchedule.SelectedValue.ToString().Trim());
            if (dt.Rows.Count > 0)
            {
                txtFundedBy.Text = dt.Rows[0]["ProjectName"].ToString().Trim();
                txtNoOfPerson.Text = Math.Round(Convert.ToDecimal(Common.ReturnZeroForNull(dt.Rows[0]["NoofPerson"].ToString().Trim()))).ToString();
                txtStrDate.Text = dt.Rows[0]["StrDate"].ToString().Trim();
                txtEndDate.Text = dt.Rows[0]["EndDate"].ToString().Trim();
                //hfScheduleID.Value = dt.Rows[0]["ScheduleID"].ToString().Trim();
                hfDuration.Value = dt.Rows[0]["Duration"].ToString().Trim();

                //txtStrDate.Text = dt.Rows[0]["StrDate"].ToString().Trim();
                //txtEndDate.Text = dt.Rows[0]["EndDate"].ToString().Trim();
                //txtDuration.Text = Math.Round(Convert.ToDecimal(Common.ReturnZeroForNull(dt.Rows[0]["Duration"].ToString().Trim()))).ToString();
                //txtNoOfPerson.Text = Math.Round(Convert.ToDecimal(Common.ReturnZeroForNull(dt.Rows[0]["NoofPerson"].ToString().Trim()))).ToString();
                //ddlLocation.SelectedValue = dt.Rows[0]["SalLocId"].ToString().Trim();
            }
        }
    }
    protected void ddlBTitleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.FillBudgetTitle();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave("Add") == false)
        {
            return;
        }
        DataTable dt = ViewState["dt"] as DataTable;
        DataRow[] drr = dt.Select("BTitleId='" + ddlBTitle.SelectedValue.ToString() + "' and BTitleType='"+ddlBTitleType.SelectedValue.ToString()+"'");
        for (int i = 0; i < drr.Length; i++)
        {
            drr[i].Delete();
        }
        dt.AcceptChanges();
        double totalTaka = Convert.ToDouble(txtUnitCost.Text) * Convert.ToDouble(txtParticipantNo.Text) * Convert.ToDouble(txtDaysNo.Text);
        //new DataColumn("BudgetId", typeof(string)),
        //                      new DataColumn("BTitleType", typeof(string)),
        //                      new DataColumn("BudgetType", typeof(string)),
        //                      new DataColumn("BTitleId", typeof(string)),
        //                      new DataColumn("BTitleName", typeof(string)),
        //                      new DataColumn("UnitCost", typeof(string)),
        //                      new DataColumn("ParticipantNo", typeof(string)),
        //                      new DataColumn("DaysNo", typeof(string)),
        //                      new DataColumn("TotalTaka", typeof(string)) });
        dt.Rows.Add(0, ddlBTitleType.SelectedValue.ToString(),ddlBTitleType.SelectedItem.Text.ToString(), ddlBTitle.SelectedValue.ToString(), ddlBTitle.SelectedItem.Text.ToString(),
             txtUnitCost.Text.ToString(), txtParticipantNo.Text.ToString(), txtDaysNo.Text.ToString(), totalTaka.ToString());

        //dt.Rows.Add(0, ddlBTitle.SelectedValue.ToString(), ddlBTitleType.SelectedItem.Text.ToString(), ddlBTitle.SelectedItem.Text.ToString(),
        //     txtUnitCost.Text.ToString(), txtParticipantNo.Text.ToString(), txtDaysNo.Text.ToString(), ddlBTitleType.SelectedValue.ToString(), totalTaka.ToString());
        grList.DataSource = dt;
        grList.DataBind();
    }
    //protected void ddlPreparedBy_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtDesigPrep.Text = "";
    //    txtDeptPrep.Text = "";
    //    if (Common.CheckNullString(txtPreparedBy.Text.Trim()) != "")
    //    {
    //        this.FillEmployeeInfo(txtPreparedBy.Text.Trim(), "1");
    //    }
    //}
    //protected void ddlReviewedBy_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtDesigReview.Text = "";
    //    txtDeptReview.Text = "";
    //    if (Common.CheckNullString(ddlReviewedBy.SelectedValue.ToString().Trim()) != "")
    //    {
    //        this.FillEmployeeInfo(ddlReviewedBy.SelectedValue.ToString().Trim(), "2");
    //    }
    //}
    //protected void ddlRecommend1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtDesigRec1.Text = "";
    //    txtDeptRec1.Text = "";
    //    if (Common.CheckNullString(ddlRecommend1.SelectedValue.ToString().Trim()) != "")
    //    {
    //        this.FillEmployeeInfo(ddlRecommend1.SelectedValue.ToString().Trim(), "3");
    //    }
    //}
    //protected void ddlRecommend2_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtDesigRec2.Text = "";
    //    txtDeptRec2.Text = "";
    //    if (Common.CheckNullString(ddlRecommend2.SelectedValue.ToString().Trim()) != "")
    //    {
    //        this.FillEmployeeInfo(ddlRecommend2.SelectedValue.ToString().Trim(), "4");
    //    }
    //}
    //protected void ddlRecommend3_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtDesigRec3.Text = "";
    //    txtDeptRec3.Text = "";
    //    if (Common.CheckNullString(ddlRecommend3.SelectedValue.ToString().Trim()) != "")
    //    {
    //        this.FillEmployeeInfo(ddlRecommend3.SelectedValue.ToString().Trim(), "5");
    //    }
    //}
    //protected void ddlApprovedBy_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtDesigApp.Text = "";
    //    txtDeptApp.Text = "";
    //    if (Common.CheckNullString(ddlApprovedBy.SelectedValue.ToString().Trim()) != "")
    //    {
    //        this.FillEmployeeInfo(ddlApprovedBy.SelectedValue.ToString().Trim(), "6");
    //    }
    //}
    private void FillEmployeeInfo(string strEmpId, string ddlId)
    {
        DataRow[] dr = objTrMgr.SelectEmployeeDetail(strEmpId).Select("EmpId='" + strEmpId+"'");

        if (dr.Length > 0)
        {
            switch (ddlId)
            {
                case "1":
                    {
                        txtDesigPrep.Text = dr[0]["DesigName"].ToString().Trim();
                        txtDeptPrep.Text = dr[0]["DeptName"].ToString().Trim();
                    }
                    break;
                case "2":
                    {
                        txtDesigReview.Text = dr[0]["DesigName"].ToString().Trim();
                        txtDeptReview.Text = dr[0]["DeptName"].ToString().Trim();
                    }
                    break;
                case "3":
                    {
                        txtDesigRec1.Text = dr[0]["DesigName"].ToString().Trim();
                        txtDeptRec1.Text = dr[0]["DeptName"].ToString().Trim();
                    }
                    break;
                case "4":
                    {
                        txtDesigRec2.Text = dr[0]["DesigName"].ToString().Trim();
                        txtDeptRec2.Text = dr[0]["DeptName"].ToString().Trim();
                    }
                    break;
                case "5":
                    {
                        txtDesigRec3.Text = dr[0]["DesigName"].ToString().Trim();
                        txtDeptRec3.Text = dr[0]["DeptName"].ToString().Trim();
                    }
                    break;
                case "6":
                    {
                        txtDesigApp.Text = dr[0]["DesigName"].ToString().Trim();
                        txtDeptApp.Text = dr[0]["DeptName"].ToString().Trim();
                    }
                    break;
            }

        }
    }
    protected void grList_RowDelete(object sender, GridViewDeleteEventArgs e)
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
                    //BudgetId,BudgetType,BTitleId,BTitleType,BTitleName,UnitCost,ParticipantNo,DaysNo,TotalTaka
                    budgetTable = ViewState["dt"] as DataTable;
                    ddlBTitleType.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[3].ToString();
                    this.FillBudgetTitle();
                    txtUnitCost.Text = grList.DataKeys[_gridView.SelectedIndex].Values[5].ToString();
                    txtParticipantNo.Text = grList.DataKeys[_gridView.SelectedIndex].Values[6].ToString();
                    txtDaysNo.Text = grList.DataKeys[_gridView.SelectedIndex].Values[7].ToString();
                    ddlBTitle.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[2].ToString();

                    DataRow[] drr = budgetTable.Select("BudgetId='" + grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString() + "' and BTitleId='"+ grList.DataKeys[_gridView.SelectedIndex].Values[2].ToString() + "'");
                    for (int i = 0; i < drr.Length; i++)
                    {
                        drr[i].Delete();
                    }
                    budgetTable.AcceptChanges();
                    ViewState["dt"] = budgetTable;
                }
                break;

            case ("Delete"):
                try
                {
                    budgetTable = ViewState["dt"] as DataTable;
                    DataRow[] drr = budgetTable.Select("BudgetId='" + grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString() + "' and BTitleId='"+ grList.DataKeys[_gridView.SelectedIndex].Values[2].ToString() + "'");
                    for (int i = 0; i < drr.Length; i++)
                    {
                        drr[i].Delete();
                    }
                    budgetTable.AcceptChanges();

                    grList.DataSource = null;
                    grList.DataBind();

                    grList.DataSource = budgetTable;
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
    private void FillBudgetTitle()
    {
        this.ClearControl("ddlBTitleType");
        if (ddlBTitleType.SelectedIndex <= 0)
            return;
        if (Common.CheckNullString(ddlBTitleType.SelectedValue.ToString().Trim()) != "")
        {
            DataTable dt = objTrMgr.SelectBudgetTitleList(0,Convert.ToInt32(ddlBTitleType.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                Common.FillDropDownList_Nil(dt, ddlBTitle);
                txtUnitCost.Text = "";
                txtParticipantNo.Text = "";
                txtDaysNo.Text = "";
            }
        }
    }
    private void FillSchedule()
    {
        this.ClearControl("ddlTrainingSchedule");
        if (ddlTrainingName.SelectedIndex <= 0)
        {
            return;          
        }
        if (Common.CheckNullString(ddlTrainingName.SelectedValue.ToString().Trim())!="")
        {
            DataTable dt = objTrMgr.SelectScheduleList(ddlTrainingName.SelectedValue.ToString().Trim());
            if (dt.Rows.Count>0)
            {
                Common.FillDropDownList(dt, ddlTrainingSchedule, "ScheDate", "ScheduleID", true);
            }
        }
    }
    protected void grTrainingBudget_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    // BudgetId,TrainId,TrainName,ScheduleID,StrDate,EndDate,Duration,NoofPerson,PreparedByName,PreparedBy,ReviewedBy,RecomBy1,RecomBy2,
                        //RecomBy3,ApprovedBy,FundedBy,CreateDate,ParticipantLevel,VenueId,IsActive

                    hfId.Value = grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                    hfDuration.Value = grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[6].ToString().Trim();
                    txtCreateDate.Text=grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[16].ToString();
                    ddlTrainingName.SelectedValue = grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                    this.FillSchedule();
                    ddlTrainingSchedule.SelectedValue = grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[3].ToString().Trim();
                    //ddlTrainingName.SelectedValue=objTrMgr.SelectTrainingList(grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[1].ToString());

                    txtStrDate.Text = grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[4].ToString();
                    txtEndDate.Text= grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[5].ToString();

                    txtNoOfPerson.Text = grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[7].ToString();
                    txtFundedBy.Text = grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[15].ToString();
                    txtParticipentLevel.Text = grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[17].ToString();
                    ddlVenue.SelectedValue=grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[18].ToString();

                    txtPreparedBy.Text = grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[9].ToString();
                    this.FillEmployeeInfo(grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[9].ToString().Trim(), "1");

                    txtReviewedBy.Text = grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[10].ToString();
                    this.FillEmployeeInfo(grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[10].ToString().Trim(), "2");

                    txtRecommend1.Text = grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[11].ToString();
                    this.FillEmployeeInfo(grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim(), "3");
                    
                    txtRecommend2.Text = grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[12].ToString();
                    this.FillEmployeeInfo(grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[12].ToString().Trim(), "4");
                    
                    txtRecommend3.Text = grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[13].ToString();
                    this.FillEmployeeInfo(grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[13].ToString().Trim(), "5");
                    
                    txtApprovedBy.Text = grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[14].ToString();
                    this.FillEmployeeInfo(grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[14].ToString().Trim(), "6");

                    if (grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[19].ToString().Trim() == "N")
                        chkInActive.Checked = true;
                    else
                        chkInActive.Checked = false;



                    this.CreateTable();
                    budgetTable = objTrMgr.SelectTrainingBudgetDtlList(grTrainingBudget.DataKeys[_gridView.SelectedIndex].Values[0].ToString());
                    ViewState["dt"] = budgetTable;
                    grList.DataSource = null;
                    grList.DataBind();

                    grList.DataSource = budgetTable;
                    grList.DataBind();



                    this.EntryMode(true);
                    lblMsg.Text = "";
                }
                break;
        }
    }
}