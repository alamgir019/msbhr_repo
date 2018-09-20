using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Training_TrainingYearlyPlan : System.Web.UI.Page
{
    TrainingManager objTrMgr = new TrainingManager();
    MasterTablesManager objTblmast = new MasterTablesManager();
    EmpInfoManager objEmp = new EmpInfoManager();

    DataTable personTable = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            hfIsUpdate.Value = "N";
            grList.DataSource = null;
            grList.DataBind();
            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            this.EntryMode(false);
            this.OpenRecord();
            this.CreateTable();

            Common.FillDropDownList(objTrMgr.SelectTrainingList("0"), ddlTrainingName,true);
            Common.FillDropDownList(objEmp.SelectProjectList(0), ddlFundedBy, "ProjectName", "ProjectId", true);
            Common.FillYearList(4, ddlYear);
            Common.FillDropDownList(objTrMgr.SelectTrainingVenue("A"), ddlVenue, "VenueName", "VenueId", true);
            Common.FillDropDownList(objTrMgr.SelectLocation("0"), ddlLocation, "SalLocName", "SalLocId", true);

            DataTable dtEmp = objEmp.SelectEmpNameWithID("A");
            dtEmp = objEmp.SelectIntEmpWithID();
            //Common.FillDropDownList(dtEmp, ddlSubtitledBy, "EmpName", "EmpID", true);
            Common.FillDropDownList(dtEmp, ddlReviewedBy, "EmpName", "EmpID", true);
            Common.FillDropDownList(dtEmp, ddlRecommend1, "EmpName", "EmpID", true);
            Common.FillDropDownList(dtEmp, ddlRecommend2, "EmpName", "EmpID", true);
            Common.FillDropDownList(dtEmp, ddlApprovedBy, "EmpName", "EmpID", true);
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
    private void CreateTable()
    {
        personTable = new DataTable();
        personTable.Columns.AddRange(new DataColumn[9] 
                            { 
                              new DataColumn("DesigId", typeof(string)),
                              new DataColumn("DesigName", typeof(string)),
                              new DataColumn("FundedBy", typeof(string)),
                              new DataColumn("FundedByName", typeof(string)),
                              new DataColumn("NoOfPerson", typeof(string)),
                              new DataColumn("VenueId", typeof(string)),
                              new DataColumn("VenueName", typeof(string)),
                              new DataColumn("LocationId", typeof(string)),
                              new DataColumn("LocationName", typeof(string))                                                           
                            });
        ViewState["dt"] = personTable;
    }
    private void OpenRecord()
    {
        this.CreateTable();
        grList.DataSource = null;
        grList.DataBind();
        grTrainingYearlyPlan.DataSource = objTrMgr.SelectTrainingYrPlanList("0");
        grTrainingYearlyPlan.DataBind();
    }
    private void SaveData(string cmdType)
    {
        dsTraining objDS = new dsTraining();
        if (cmdType == "I")
        {
            hfId.Value = Common.getMaxId("TrTrainingYrPlan", "YrPlanId");
        }

        List<DataTable> tblList = new List<DataTable>();

        DataTable dtMst = objDS.Tables["TrTrainingYrPlan"];
        DataRow nRow = dtMst.NewRow();

        nRow["YrPlanId"] = Common.RoundDecimal(hfId.Value, 0);
        nRow["YrPlanName"] = txtPlanName.Text.Trim();
        nRow["TrainId"] = Common.RoundDecimal(ddlTrainingName.SelectedValue.ToString().Trim(), 0);
        nRow["TrainType"] = txtTrainingType.Text.Trim();
        nRow["TotalParticipant"] = Common.RoundDecimal(txtTotalParticipant.Text.Trim(),0);
        nRow["Year"] = ddlYear.SelectedValue.ToString().Trim();
        nRow["CourseFee"] = Common.RoundDecimal(txtCourseFee.Text.Trim(),2);
        nRow["Perdiem"] = Common.RoundDecimal(txtPerdiem.Text.Trim(),2);
        nRow["FAOthers"] = Common.RoundDecimal(txtFAOthers.Text.Trim(),2);
        nRow["Transport"] = Common.RoundDecimal(txtTransport.Text.Trim(),2);
        nRow["LocalTransport"] = Common.RoundDecimal(txtLocalTransport.Text.Trim(), 2);
        nRow["PracticalCost"] = Common.RoundDecimal(txtPracticalCost.Text.Trim(), 2);
        nRow["Miscellaneous"] = Common.RoundDecimal(txtMiscellaneous.Text.Trim(), 2);
        nRow["StrDate"] = Common.ReturnDate(txtStartDate.Text.Trim());
        nRow["EndDate"] = Common.ReturnDate(txtEndDate.Text.Trim());
        nRow["Duration"] = Common.RoundDecimal(txtDuration.Text.Trim(), 0);
        nRow["Remarks"] = txtRemarks.InnerText;
        //nRow["SubtitledBy"] = ddlSubtitledBy.SelectedValue.ToString().Trim();
        nRow["ReviewedBy"] = ddlReviewedBy.SelectedValue.ToString().Trim();
        nRow["RecommBy1"] = ddlRecommend1.SelectedValue.ToString().Trim();
        nRow["RecommBy2"] = ddlRecommend2.SelectedValue.ToString().Trim();
        nRow["ApprovedBy"] = ddlApprovedBy.SelectedValue.ToString().Trim();

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
        DataTable dtDtl = objDS.Tables["TrTrainingYrPlanDtls"];

        DataTable dtDtlInput = ViewState["dt"] as DataTable;
        int DtlId = Int32.Parse(Common.getMaxId("TrTrainingYrPlanDtls", "YrPlanDtlsId"));
       
        foreach (DataRow row in dtDtlInput.Rows)
        {
            DataRow nRowdtl = dtDtl.NewRow();

            nRowdtl["YrPlanDtlsId"] = DtlId;
            nRowdtl["YrPlanId"] = Common.RoundDecimal(hfId.Value, 0);
            nRowdtl["DesigId"] = row["DesigId"].ToString().Trim();
            nRowdtl["FundedBy"]=row["FundedBy"].ToString().Trim();
            nRowdtl["NoOfPerson"] = row["NoOfPerson"].ToString().Trim();
            nRowdtl["VenueId"]=row["VenueId"].ToString().Trim();
            nRowdtl["LocationId"]=row["LocationId"].ToString().Trim();
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
    private bool ValidateAndSave(string Flag)
    {
        try
        {
            lblMsg.Text = "";
            switch (Flag)
            {
                case "Save":
                    if (ddlYear.SelectedIndex<0)
                    {
                        lblMsg.Text = "Please select Year.";
                        return false;
                    }
                    else if (txtPlanName.Text.Trim()=="")
                    {
                        lblMsg.Text = "Please enter Plan Name.";
                        return false;
                    }
                    else if (ddlTrainingName.SelectedIndex <= 0)
                    {
                        lblMsg.Text = "Please select Training Name.";
                        return false;
                    }
                    else if (txtTotalParticipant.Text.Trim()=="")
                    {
                        lblMsg.Text = "Please enter Total Participant Number.";
                        return false;
                    }
                    else if (string.IsNullOrEmpty(txtStartDate.Text))
                    {
                        lblMsg.Text = "Please enter Start Date.";
                        return false;
                    }
                    else if (string.IsNullOrEmpty(txtEndDate.Text))
                    {
                        lblMsg.Text = "Please enter End Date.";
                        return false;
                    }
                    else if (txtDuration.Text.Trim()=="")
                    {
                        lblMsg.Text = "Please enter Duration.";
                        return false;
                    }
                    else if (grList.Rows.Count == 0)
                    {
                        lblMsg.Text = "Please add Designation.";
                        return false;
                    }
                    //else if (ddlSubtitledBy.SelectedIndex <= 0)
                    //{
                    //    lblMsg.Text = "Please Select Subtitled By";
                    //    return false;
                    //}
                    else if (ddlReviewedBy.SelectedIndex <= 0)
                    {
                        lblMsg.Text = "Please Select Reviewed By";
                        return false;
                    }
                    else if (ddlRecommend1.SelectedIndex <= 0)
                    {
                        lblMsg.Text = "Please Select Remommend By";
                        return false;
                    }
                    else if (ddlRecommend2.SelectedIndex <= 0)
                    {
                        lblMsg.Text = "Please Select Recommend By";
                        return false;
                    }
                    else if (ddlApprovedBy.SelectedIndex <= 0)
                    {
                        lblMsg.Text = "Please Select Approved By";
                        return false;
                    }
                    break;
                case "Add":
                    if (ddlDesignation.SelectedIndex<=0)
                    {
                        lblMsg.Text = "Please select Designation.";
                        return false;
                    }
                    else if (ddlFundedBy.SelectedIndex<=0)
                    {
                        lblMsg.Text = "Please select Funded By";
                        return false;
                    }
                    else if (txtNoOfPerson.Text.Trim() == "")
                    {
                        lblMsg.Text = "Please enter No of Person.";
                        return false;
                    }
                    else if (ddlVenue.SelectedIndex<=0 && ddlLocation.SelectedIndex<=0)
                    {
                        lblMsg.Text = "Please select Venue or Location";
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
    protected void grTrainingYearlyPlan_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;

        switch (_commandName)
        {
            case ("DoubleClick"):
                {//"YrPlanId,YrPlanName,TrainId,TrainName,Year,StrDate,EndDate,Duration,IsActive,Remarks,CourseFee,Perdiem,FAOthers,Transport,LocalTransport,TrainType"
                    Common.EmptyTextBoxValues(this);
                    hfId.Value = grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim();
                    txtPlanName.Text = grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim();
                    ddlTrainingName.SelectedValue = grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[2].ToString().Trim();
                    ddlYear.SelectedValue = grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[4].ToString().Trim();
                    txtStartDate.Text = grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[5].ToString().Trim();
                    txtEndDate.Text = grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[6].ToString().Trim();
                    txtDuration.Text = grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[7].ToString().Trim();
                    txtRemarks.InnerText = grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[9].ToString().Trim();
                    txtCourseFee.Text = grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[10].ToString().Trim();
                    txtPerdiem.Text = grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim();
                    txtFAOthers.Text = grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[12].ToString().Trim();
                    txtTransport.Text = grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[13].ToString().Trim();
                    txtLocalTransport.Text = grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[14].ToString().Trim();
                    txtPracticalCost.Text = grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[17].ToString().Trim();
                    txtMiscellaneous.Text = grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[18].ToString().Trim();
                    txtTrainingType.Text = grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[15].ToString().Trim();
                    txtTotalParticipant.Text = grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[16].ToString().Trim();


                    //ddlSubtitledBy.SelectedValue = grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[19].ToString();
                    this.FillEmployeeInfo(grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[19].ToString().Trim(), "1");

                    ddlReviewedBy.SelectedValue = grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[20].ToString();
                    this.FillEmployeeInfo(grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[20].ToString().Trim(), "2");

                    ddlRecommend1.SelectedValue = grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[21].ToString();
                    this.FillEmployeeInfo(grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[21].ToString().Trim(), "3");

                    ddlRecommend2.SelectedValue = grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[22].ToString();
                    this.FillEmployeeInfo(grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[22].ToString().Trim(), "4");

                    ddlApprovedBy.SelectedValue = grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[23].ToString();
                    this.FillEmployeeInfo(grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[23].ToString().Trim(), "5");


                    if (grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[8].ToString().Trim() == "Y")
                        chkInActive.Checked = false;
                    else
                        chkInActive.Checked = true;

                    grList.DataSource = null;
                    grList.DataBind();
                    this.CreateTable();
                    personTable = objTrMgr.SelectTrainingYrPlanDtlList(grTrainingYearlyPlan.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim());
                    grList.DataSource = personTable;
                    grList.DataBind();
                    ViewState["dt"] = personTable;
                    this.EntryMode(true);
                    lblMsg.Text = "";

                    if (Common.CheckNullString(ddlTrainingName.SelectedValue.ToString().Trim()) != "")
                    {
                        DataTable dt = objTrMgr.SelectTrainingDtlWithDesig(ddlTrainingName.SelectedValue.ToString().Trim());
                        Common.FillDropDownList(dt, ddlDesignation, "DesigName", "DesigId", true);

                    }

                    break;
                }
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
                    ddlDesignation.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim();

                    DataRow[] drr = personTable.Select("DesigId='" + grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim() + "'");
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
                    DataRow[] drr = personTable.Select("DesigId='" + grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim() + "'");
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
        if (ValidateAndSave("Add") == false)
            return;
       
        DataTable dt = ViewState["dt"] as DataTable;
        DataRow[] drr = dt.Select("DesigId='" + ddlDesignation.SelectedValue.ToString().Trim() + "' and FundedBy='"+ddlFundedBy.SelectedValue.ToString().Trim()+"'");
        for (int i = 0; i < drr.Length; i++)
        {
            drr[i].Delete();
        }
        dt.AcceptChanges();
        string locName = "";
        string venueName = "";
        if (ddlVenue.SelectedIndex>0)
        {
            venueName = ddlVenue.SelectedItem.Text.Trim();
        }
        else if (ddlLocation.SelectedIndex>0)
        {
            locName = ddlLocation.SelectedItem.Text.Trim();
        }
        dt.Rows.Add(ddlDesignation.SelectedValue.ToString().Trim(), ddlDesignation.SelectedItem.ToString().Trim(), ddlFundedBy.SelectedValue.ToString().Trim(), ddlFundedBy.SelectedItem.Text.Trim(),
            txtNoOfPerson.Text.Trim(),ddlVenue.SelectedValue.ToString().Trim(),venueName,ddlLocation.SelectedValue.ToString().Trim(),locName);
        grList.DataSource = dt;
        grList.DataBind();
    }
    protected void ddlTrainingName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //txtTentativeDays.Text = "";
        if (ddlTrainingName.SelectedIndex <= 0)
            return;
        if (Common.CheckNullString(ddlTrainingName.SelectedValue.ToString().Trim()) != "")
        {
            DataTable dt = objTrMgr.SelectTrainingDtlWithDesig(ddlTrainingName.SelectedValue.ToString().Trim());
            if (dt.Rows.Count > 0)
                //txtTentativeDays.Text = dt.Rows[0]["TentativeDay"].ToString().Trim();
            Common.FillDropDownList(dt, ddlDesignation, "DesigName", "DesigId", true);

        }
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        //var dd= ddlDesignation.Items.Count;
        if (ddlLocation.SelectedIndex != 0)
            ddlVenue.SelectedIndex = 0;
        else
            ddlLocation.SelectedIndex = 0;
        var yy = Request.Form[ddlDesignation.UniqueID];
    }
    protected void ddlVenue_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVenue.SelectedIndex != 0)
            ddlLocation.SelectedIndex = 0;
        else
            ddlVenue.SelectedIndex = 0;
    }

    private void FillEmployeeInfo(string strEmpId, string ddlId)
    {
        DataRow[] dr = objTrMgr.SelectEmployeeDetail(strEmpId).Select("EmpId='" + strEmpId + "'");

        if (dr.Length > 0)
        {
            switch (ddlId)
            {
                case "1":
                    {
                        txtDesigSub.Text = dr[0]["DesigName"].ToString().Trim();
                        txtDeptSub.Text = dr[0]["DeptName"].ToString().Trim();
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
                        txtDesigApp.Text = dr[0]["DesigName"].ToString().Trim();
                        txtDeptApp.Text = dr[0]["DeptName"].ToString().Trim();
                    }
                    break;
            }

        }
    }

    protected void ddlSubtitledBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDesigSub.Text = "";
        txtDeptSub.Text = "";
        //if (Common.CheckNullString(ddlSubtitledBy.SelectedValue.ToString().Trim()) != "")
        //{
        //    this.FillEmployeeInfo(ddlSubtitledBy.SelectedValue.ToString().Trim(), "1");
        //}
    }
    protected void ddlReviewedBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDesigReview.Text = "";
        txtDeptReview.Text = "";
        if (Common.CheckNullString(ddlReviewedBy.SelectedValue.ToString().Trim()) != "")
        {
            this.FillEmployeeInfo(ddlReviewedBy.SelectedValue.ToString().Trim(), "2");
        }
    }
    protected void ddlRecommend1_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDesigRec1.Text = "";
        txtDeptRec1.Text = "";
        if (Common.CheckNullString(ddlRecommend1.SelectedValue.ToString().Trim()) != "")
        {
            this.FillEmployeeInfo(ddlRecommend1.SelectedValue.ToString().Trim(), "3");
        }
    }
    protected void ddlRecommend2_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDesigRec2.Text = "";
        txtDeptRec2.Text = "";
        if (Common.CheckNullString(ddlRecommend2.SelectedValue.ToString().Trim()) != "")
        {
            this.FillEmployeeInfo(ddlRecommend2.SelectedValue.ToString().Trim(), "4");
        }
    }
    protected void ddlApprovedBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDesigApp.Text = "";
        txtDeptApp.Text = "";
        if (Common.CheckNullString(ddlApprovedBy.SelectedValue.ToString().Trim()) != "")
        {
            this.FillEmployeeInfo(ddlApprovedBy.SelectedValue.ToString().Trim(), "5");
        }
    }

}