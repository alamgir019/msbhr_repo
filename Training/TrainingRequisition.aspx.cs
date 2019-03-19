using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

public partial class Training_TrainingRequisition : System.Web.UI.Page
{
    TrainingManager objEmpMgr = new TrainingManager();
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

            DataTable dtEmp = objEmp.SelectEmpNameWithID("A");
            Common.FillDropDownList(objEmpMgr.SelectScheduleList("A"), ddlSchedule, "ScheDate", "ScheduleID", true);
            Common.FillDropDownList(objSOFMgr.SelectProjectList(0), ddlProject, "ProjectName", "ProjectId", true);

            Common.FillDropDownList(dtEmp, ddlSigenBy1, "EmpName", "EmpID", true);
            Common.FillDropDownList(dtEmp, ddlSigenBy2, "EmpName", "EmpID", true);
            Common.FillDropDownList(dtEmp, ddlSeenBy, "EmpName", "EmpID", true);
          
            this.CreateTable();
        }

    }
    protected void CreateTable()
    {
        personTable = new DataTable();
        personTable.Columns.AddRange(new DataColumn[4] 
                            { 
                              new DataColumn("EmpID", typeof(string)),
                              new DataColumn("EmpName", typeof(string)),
                               new DataColumn("ProjectId", typeof(string)),
                              new DataColumn("ProjectName", typeof(string))
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

    private void OpenRecord()
    {
        //dtList = objEmpMgr.SelectTraining();
        grList.DataSource = null;
        grList.DataBind();
        grRequisition.DataSource=objEmpMgr.SelectTrainingRequisitionList();
        grRequisition.DataBind();
    }

    private void SaveData(string IsDelete)
    {
        try
        {
            if (hfIsUpdate.Value == "Y")
                hfId.Value = hfId.Value;
            else
                hfId.Value = Common.getMaxId("TrRequisition", "ReqID");

            var match = Regex.Match(txtReviewBy.Text.Trim(), "(^(\\w+(.)*\\s)+\\[)*(\\w+)");
            string empidRev = match.Groups[match.Groups.Count - 1].Value;

            match = Regex.Match(txtRecomandedBy.Text.Trim(), "(^(\\w+(.)*\\s)+\\[)*(\\w+)");
            string empidRec = match.Groups[match.Groups.Count - 1].Value;

            match = Regex.Match(txtApproveBy.Text.Trim(), "(^(\\w+(.)*\\s)+\\[)*(\\w+)");
            string empidApp = match.Groups[match.Groups.Count - 1].Value;
            clsTrRequisition objTrReq = new clsTrRequisition(
                hfId.Value.ToString(),
                ddlSchedule.SelectedValue.ToString(),
                hfTrainingId.Value,
                this.ddlSigenBy1.SelectedValue.ToString().Trim(),
                this.ddlSigenBy2.SelectedValue.ToString().Trim(),
                this.ddlSeenBy.SelectedValue.ToString().Trim(),
                empidRev,empidRec,empidApp,
                (chkInActive.Checked == true ? "N" : "Y"),
                txtRemark.Text.Trim()   
                );

            objEmpMgr.InsertTrRequisition(grList, objTrReq, Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()), hfIsUpdate.Value, IsDelete);

            lblMsg.Text = Common.GetMessage(hfIsUpdate.Value.ToString(), IsDelete);
            Common.EmptyTextBoxValues(this);
            this.EntryMode(false);
            this.OpenRecord();
            this.CreateTable();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error : "+ ex;
            throw (ex);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave("Save") == false)
        {
            return;
        }
        this.SaveData("N");
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
        this.CreateTable();
        lblMsg.Text = "";
    }
    protected void ddlSchedule_SelectedIndexChanged(object sender, EventArgs e) 
    {
        hfTrainingId.Value = "";
        txtTrainingName.Text = "";
        txtTrainingLocation.Text = "";
        if (Common.CheckNullString(ddlSchedule.SelectedIndex.ToString()) != "")
        {
            DataTable dt= objEmpMgr.SelectTrainingNameLocUsingSchedule(ddlSchedule.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                hfTrainingId.Value = dt.Rows[0]["TrainId"].ToString().Trim();
                txtTrainingName.Text = dt.Rows[0]["TrainName"].ToString().Trim();
                if (string.IsNullOrEmpty(dt.Rows[0]["TrainLocation"].ToString().Trim()) == false)
                    txtTrainingLocation.Text = dt.Rows[0]["TrainLocation"].ToString().Trim();
                else
                    txtTrainingLocation.Text = dt.Rows[0]["VenueName"].ToString().Trim();
            }
        }        
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
                {
                    personTable = ViewState["dt"] as DataTable;
                    ddlProject.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[2].ToString();// Common.CheckNullString(grList.SelectedRow.Cells[1].Text.Trim());
                    txtTraineeName.Text = grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim();
                    DataRow[] drr = personTable.Select("EmpID='" + grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim() + "'");
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
                    DataRow[] drr = personTable.Select("EmpID='" + grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim() + "'");
                    for (int i = 0; i < drr.Length; i++)
                    {
                        drr[i].Delete();
                    }
                    personTable.AcceptChanges();
                
                    grList.DataSource = null;
                    grList.DataBind();

                    grList.DataSource = personTable;
                   // grList.DataSource = deleteGridRow("EmpID='" + grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString() + "'");
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
    protected void grRequisition_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        lblMsg.Text = "";
        switch (_commandName)
        {
            case ("DoubleClick"):
                try
                {
                    Common.EmptyTextBoxValues(this);
                    hfId.Value = grRequisition.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim();
                     ddlSchedule.SelectedValue = grRequisition.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim();
                    hfTrainingId.Value = grRequisition.DataKeys[_gridView.SelectedIndex].Values[3].ToString();
                    txtTrainingName.Text = grRequisition.DataKeys[_gridView.SelectedIndex].Values[4].ToString().Trim();
                    txtTrainingLocation.Text = grRequisition.DataKeys[_gridView.SelectedIndex].Values[5].ToString().Trim();
                    ddlSigenBy1.SelectedValue=grRequisition.DataKeys[_gridView.SelectedIndex].Values[6].ToString().Trim();
                    ddlSigenBy2.SelectedValue = grRequisition.DataKeys[_gridView.SelectedIndex].Values[7].ToString().Trim();
                    ddlSeenBy.SelectedValue= grRequisition.DataKeys[_gridView.SelectedIndex].Values[8].ToString().Trim();
                    txtReviewBy.Text = grRequisition.DataKeys[_gridView.SelectedIndex].Values[9].ToString().Trim();
                    txtRecomandedBy.Text = grRequisition.DataKeys[_gridView.SelectedIndex].Values[10].ToString().Trim();
                    txtApproveBy.Text = grRequisition.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim();
                    if (grRequisition.DataKeys[_gridView.SelectedIndex].Values[12].ToString().Trim() == "Y")
                        chkInActive.Checked = false;
                    else
                        chkInActive.Checked = true;
                    txtRemark.Text =Common.CheckNullString(grRequisition.SelectedRow.Cells[8].Text.Trim());
                    grList.DataSource = null;
                    grList.DataBind();

                    personTable = objEmpMgr.SelectTrainingReqDtlsList(grRequisition.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim());
                    if (personTable.Rows.Count > 0)
                    {
                        grList.DataSource = personTable;
                        grList.DataBind();
                    }
                    ViewState["dt"] = personTable;
                    this.EntryMode(true);
                }
                catch (Exception ex)
                {
                    lblMsg.Text = "";
                    throw (ex);
                }
                break;
        }
    }
    protected void grList_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {

    }
    private DataTable deleteGridRow(string row) 
    {
        personTable = ViewState["dt"] as DataTable;
        DataRow[] drr = personTable.Select(row);
        for (int i = 0; i < drr.Length; i++)
        {
            drr[i].Delete();
        }
        personTable.AcceptChanges();
        return personTable;
    }
    private bool ValidateAndSave(string Flag)
    {
        try
        {
            lblMsg.Text = "";
            switch (Flag)
            {
                case "Save":
                    if (ddlSchedule.SelectedIndex <= 0)
                    {
                        lblMsg.Text = "Please select Training Schedule";
                        return false;
                    }
                    else if (grList.Rows.Count == 0)
                    {
                        lblMsg.Text = "Please add Employee";
                        return false;
                    }
                    break;
                case "Add":
                    if (ddlSchedule.SelectedIndex <= 0)
                    {
                        lblMsg.Text = "Please select Training Schedule";
                        return false;
                    }
                    //else if (txtprojectName.Text == "")
                    //{
                    //    lblMsg.Text = "Please enter Project name";
                    //    return false;
                    //}
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //if (ValidateAndSave("Add") == false)
        //{
        //    return;
        //}
        DataTable dt = ViewState["dt"] as DataTable;
        var match = Regex.Match(txtTraineeName.Text.Trim(), "(^(\\w+(.)*\\s)+\\[)*(\\w+)");
        string empid = match.Groups[match.Groups.Count - 1].Value;
        DataRow[] drr = dt.Select("EmpID='" + empid + "'");
        for (int i = 0; i < drr.Length; i++)
        {
            drr[i].Delete();
        }
        dt.AcceptChanges();
        var matchName = Regex.Match(txtTraineeName.Text.Trim(), "^(\\w+(.)*\\s)+[^\\[]");
        string empName = match.Groups[match.Groups.Count - 3].Value;
        dt.Rows.Add(empid, empName, ddlProject.SelectedValue.ToString().Trim(), ddlProject.SelectedItem.ToString().Trim());
        grList.DataSource = dt;
        grList.DataBind();
    }
    protected void grList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
