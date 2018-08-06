using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Training_TrainingScheduleDtl : System.Web.UI.Page
{
    TrainingManager objTrMgr = new TrainingManager();
    MasterTablesManager objTblmast = new MasterTablesManager();
    SOFManager objSOFMgr = new SOFManager();
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
            grDtlsList.DataSource = null;
            grDtlsList.DataBind();
            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            lblValidMsg.Text = "";
            this.EntryMode(false);
            this.OpenRecord();
            this.CreateTable();
            //Common.FillDropDownList_Nil(objEmpMgr.SelectTrainingList("0"), ddlTrName);
            Common.FillDropDownList(objTrMgr.SelectTrainingList("0"), ddlTrName, "TrainName", "TrainId", true);
            //Common.FillDropDownList_Nil(objEmpMgr.SelectLocation("0"), ddlLocation);
            Common.FillDropDownList(objTrMgr.SelectLocation("0"), ddlLocation, "SalLocName", "SalLocId", true);
            //Common.FillDropDownList(objTblmast.SelectClinic(), ddlLocation, "ClinicName", "ClinicId", true);
            Common.FillDropDownList(objTrMgr.SelectTrainingVenue("A"), ddlVenue, "VenueName", "VenueId", true);
            Common.FillDropDownList(objSOFMgr.SelectProjectList(0), ddlFundedby, "ProjectName", "ProjectId", true);
            Common.FillDropDownList(objEmp.SelectEmpNameWithID("A"), ddlCourseCordinator, "EmpName", "EmpID", true);
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

            grDtlsList.DataSource = null;
            grDtlsList.DataBind(); 
        }
    }

    private void OpenRecord()
    {
        dtList = objTrMgr.SelectScheduleList("A");
        grList.DataSource = dtList;
        grList.DataBind();
    }

    private void CreateTable()
    {
        //Fundedby,ParticipantNo
        personTable = new DataTable();
        personTable.Columns.AddRange(new DataColumn[3] 
                            {                               
                              new DataColumn("Fundedby", typeof(string)),
                               new DataColumn("ProjectName", typeof(string)),
                              new DataColumn("ParticipantNo", typeof(string)),                              
                            });
        ViewState["dtDtls"] = personTable;
    }
    private Boolean getDateDiff(string strDate, string endDate)
    {
        double TotDay = 0;
        char[] splitter = { '/' };
        DateTime dtFrom = new DateTime();
        DateTime dtTo = new DateTime();
        lblMsg.Text = "";
        lblValidMsg.Text = "";
        string[] arinfo = Common.str_split(strDate.Trim(), splitter);

        if (arinfo.Length == 3)
        {
            dtFrom = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
            arinfo = null;
        }
        arinfo = Common.str_split(endDate.Trim(), splitter);
        if (arinfo.Length == 3)
        {
            dtTo = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
            arinfo = null;
        }

        TimeSpan Dur = dtTo.Subtract(dtFrom);

        TotDay =Math.Round(Convert.ToDouble(Dur.Days), 0) + 1;
        if (TotDay < 0)
        {
            lblValidMsg.Text = "Start Date can not be greater than end date";
            txtDuration.Text = "";
            return false;
        }
        else
        {
            txtDuration.Text = TotDay.ToString();
            return true;
        }
    }
    private bool ValidateAndSave(string Flag)
    {
        try
        {
            double TotDay = 0;
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();
            lblValidMsg.Text = "";
            switch (Flag)
            {
                case "Save":
                    if (ddlTrName.SelectedIndex <= 0)
                    {
                        lblValidMsg.Text = "Please Select Training Name";
                        return false;
                    }
                    else if ((ddlLocation.SelectedIndex <= 0)&& (ddlVenue.SelectedIndex <= 0))
                    {
                        lblValidMsg.Text = "Please Select Training Location or Venue.";
                        return false;
                    }

                    else if (string.IsNullOrEmpty(txtStrDate.Text) == false && string.IsNullOrEmpty(txtEndDate.Text) == false)
                    {
                        txtDuration.Text = "";
                        char[] splitter = { '/' };
                        string[] arinfo = Common.str_split(txtStrDate.Text.Trim(), splitter);
                        if (arinfo.Length == 3)
                        {
                            dtFrom = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                            arinfo = null;
                        }
                        arinfo = Common.str_split(txtEndDate.Text.Trim(), splitter);
                        if (arinfo.Length == 3)
                        {
                            dtTo = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                            arinfo = null;
                        }

                        TimeSpan Dur = dtTo.Subtract(dtFrom);

                        TotDay = Math.Round(Convert.ToDouble(Dur.Days), 0)+1;
                        if (TotDay < 0)
                        {
                            lblValidMsg.Text = "Start Date can not be greater than end date";
                            return false;
                        }
                        else
                        {
                            txtDuration.Text = TotDay.ToString();
                            return true;
                        }
                    }
                    break;
                case "Add":
                    if (ddlFundedby.SelectedIndex <= 0)
                    {
                        lblValidMsg.Text = "Please Select Funded By.";
                        return false;
                    }
                    break;
            }
            return true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            lblValidMsg.Text = "";
            throw (ex);
        }
    }
    private void SaveData(string cmdType)
    {
        dsTraining objDS = new dsTraining();
        if (cmdType == "I")
        {
            hfId.Value = Common.getMaxId("TrSchedule", "ScheduleID");
        }

        List<DataTable> tblList = new List<DataTable>();

        DataTable dtMst = objDS.Tables["TrSchedule"];
        DataRow nRow = dtMst.NewRow();

        nRow["ScheduleID"] = Common.RoundDecimal(hfId.Value, 0);
        nRow["TrainId"] = Common.RoundDecimal(ddlTrName.SelectedValue.ToString().Trim(), 0);
        nRow["SalLocId"] = ddlLocation.SelectedValue.ToString().Trim();
        nRow["VenueId"] = ddlVenue.SelectedValue.ToString().Trim();
        nRow["StrDate"] = Common.ReturnDate(txtStrDate.Text.Trim());
        nRow["EndDate"] = Common.ReturnDate(txtEndDate.Text.Trim());

        nRow["Duration"] = txtDuration.Text.Trim();
        nRow["NoofPerson"] = txtNoOfPerson.Text.Trim();
        nRow["CoordinatorName"] = ddlCourseCordinator.SelectedValue.ToString();
        nRow["Residential"] = ddlResidential.SelectedValue.ToString().Trim();
      
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
        nRow["IsDeleted"] = (cmdType == "D" ? "Y" : "N");
        nRow["IsActive"] = (chkInActive.Checked == true ? "N" : "Y");

        dtMst.Rows.Add(nRow);
        dtMst.AcceptChanges();

        tblList.Add(dtMst);

        //detail table
        DataTable dtDtl = objDS.Tables["TrScheduleDtls"];

        DataTable dtDtlInput = ViewState["dtDtls"] as DataTable;
        int TrainListDtlId = Int32.Parse(Common.getMaxId("TrScheduleDtls", "ScheduleID"));

        foreach (DataRow row in dtDtlInput.Rows)
        {
            DataRow nRowdtl = dtDtl.NewRow();
            nRowdtl["ScheduleID"] = Common.RoundDecimal(hfId.Value, 0);
            nRowdtl["FundedBy"] = row["Fundedby"].ToString().Trim();
            nRowdtl["ParticipantNo"] = row["ParticipantNo"].ToString().Trim();
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
    protected void txtDuration_onclick(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtStrDate.Text.ToString().Trim()) == false)
        {
            lblMsg.Text = "Start Date is Empty";
            txtDuration.Text = "";
            return;
        }
        else if (string.IsNullOrEmpty(txtEndDate.Text.ToString().Trim()) == false)
        {
            lblMsg.Text = "End Date is Empty";
            txtDuration.Text = "";
            return;
        }
        if (this.getDateDiff(txtStrDate.Text.ToString().Trim(), txtEndDate.Text.ToString().Trim()) == false)
        {
            txtStrDate.Focus();
        }
    }
    protected void txtEndDate_OnTextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtStrDate.Text.ToString().Trim()) == false && string.IsNullOrEmpty(txtEndDate.Text.ToString().Trim()) == false)
        {
            if (this.getDateDiff(txtStrDate.Text.ToString().Trim(), txtEndDate.Text.ToString().Trim()) == false)
            {
                txtEndDate.Focus();
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
                //ScheduleID,TrainId,TrainName,SalLocId,SalLocName,StrDate,EndDate,Duration,NoofPerson,CoordinatorId,CoordinatorName,FundedBy,ProjectName,Residential,IsActive
                hfId.Value = grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                //txtName.Text = Common.CheckNullString(grList.SelectedRow.Cells[1].Text.Trim());
                ddlTrName.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                if (string.IsNullOrEmpty(grList.DataKeys[_gridView.SelectedIndex].Values[3].ToString()) == false)
                    ddlLocation.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[3].ToString();
                if (string.IsNullOrEmpty(grList.DataKeys[_gridView.SelectedIndex].Values[14].ToString()) == false)
                    ddlVenue.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[14].ToString();
                txtStrDate.Text = grList.DataKeys[_gridView.SelectedIndex].Values[5].ToString();
                txtEndDate.Text = grList.DataKeys[_gridView.SelectedIndex].Values[6].ToString();
                txtDuration.Text = grList.DataKeys[_gridView.SelectedIndex].Values[7].ToString();
                txtNoOfPerson.Text = grList.DataKeys[_gridView.SelectedIndex].Values[8].ToString();
                ddlCourseCordinator.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[9].ToString();               
                ddlResidential.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[11].ToString();
                if (grList.DataKeys[_gridView.SelectedIndex].Values[13].ToString() == "Y")
                    chkInActive.Checked = false;
                else
                    chkInActive.Checked = true;
                txtRemarks.Text = grList.DataKeys[_gridView.SelectedIndex].Values[15].ToString();
                //Detail
                this.CreateTable();
                personTable = objTrMgr.SelectScheduleDtls(grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString());
                grDtlsList.DataSource = personTable;
                grDtlsList.DataBind();
                ViewState["dtDtls"] = personTable;

                this.EntryMode(true);
                lblMsg.Text = "";
                break;
        }
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedIndex != 0)
            ddlVenue.SelectedIndex = 0;
        else
            ddlLocation.SelectedIndex = 0;
    }
    protected void ddlVenue_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVenue.SelectedIndex != 0)
            ddlLocation.SelectedIndex = 0;
        else
            ddlVenue.SelectedIndex = 0;
    }

    protected void grDtlsList_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    personTable = ViewState["dtDtls"] as DataTable;
                    ddlFundedby.SelectedValue = grDtlsList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                    txtPartNo.Text = grDtlsList.SelectedRow.Cells[3].Text.Trim();    

                    DataRow[] drr = personTable.Select("Fundedby='" + grDtlsList.DataKeys[_gridView.SelectedIndex].Values[0].ToString() + "'");
                    for (int i = 0; i < drr.Length; i++)
                    {
                        drr[i].Delete();
                    }
                    personTable.AcceptChanges();
                    ViewState["dtDtls"] = personTable;                    
                }
                break;

            case ("Delete"):
                try
                {
                    personTable = ViewState["dtDtls"] as DataTable;
                    DataRow[] drr = personTable.Select("Fundedby='" + grDtlsList.DataKeys[_gridView.SelectedIndex].Values[0].ToString() + "'");
                    for (int i = 0; i < drr.Length; i++)
                    {
                        drr[i].Delete();
                    }
                    personTable.AcceptChanges();

                    grDtlsList.DataSource = null;
                    grDtlsList.DataBind();

                    grDtlsList.DataSource = personTable;
                    grDtlsList.DataBind();

                }
                catch (Exception ex)
                {
                    lblMsg.Text = "Error : " + ex;
                    throw (ex);
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
        DataTable dtDtls = ViewState["dtDtls"] as DataTable;
        DataRow[] drr = dtDtls.Select("FundedBy='" + ddlFundedby.SelectedValue.ToString() + "'");
        for (int i = 0; i < drr.Length; i++)
        {
            drr[i].Delete();
        }
        dtDtls.AcceptChanges();
        dtDtls.Rows.Add(ddlFundedby.SelectedValue.ToString(), ddlFundedby.SelectedItem.Text.ToString(), txtPartNo.Text.ToString());
        grDtlsList.DataSource = dtDtls;
        grDtlsList.DataBind();

        ddlFundedby.SelectedIndex = 0;
        txtPartNo.Text = "";        
    }
    protected void grDtlsList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
