using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
public partial class Training_TrainingSetup : System.Web.UI.Page
{   
    TrainingManager objEmpMgr = new TrainingManager();
    MasterTablesManager objTblmast = new MasterTablesManager();
    DataTable dtList = new DataTable();
    DataTable personTable= new DataTable();
    
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
            this.LoadTrainingGrid();
            Common.FillDropDownList_Nil(objEmpMgr.SelectTrainingCategory("0"), ddlCategoty);
            Common.FillDropDownList_Nil(objTblmast.SelectDesignation(0), ddlDesig);
            this.CreateTable();
        }
       
    }

    protected void CreateTable()
    {
        personTable = new DataTable();
        personTable.Columns.AddRange(new DataColumn[3] 
                            { new DataColumn("DesigId", typeof(string)),
                              new DataColumn("Designame", typeof(string)),
                              new DataColumn("PeriodMM", typeof(string)) });
        ViewState["dt"] = personTable;
    }
    protected void ClearControl()
    {
        ddlDesig.SelectedIndex = -1;
        txtPeriod.Text = "";
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
    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (string.IsNullOrEmpty(txtName.Text.Trim()) == false)
        {
            this.FillEmpInfoMst(txtName.Text.Trim());
            this.FillEmpInfo(txtName.Text.Trim());
            //this.OpenRecord();
            lblMsg.Text = "";
            this.EntryMode(true);
        }
    }

    private void FillEmpInfo(string p_TrName)
    {
        dtList = objEmpMgr.SelectTrainingListDetail(p_TrName);
        this.CreateTable();
        ViewState["dt"] = dtList;
        this.OpenRecord();
    }
    private void FillEmpInfoMst(string p_TrName)
    {
        DataTable dt1 = (DataTable)objEmpMgr.SelectTrainingListMst(p_TrName);

        ddlCategoty.SelectedValue= dt1.Rows[0]["TrCategoryId"].ToString();
        ddlInOut.SelectedValue = dt1.Rows[0]["IsInHouse"].ToString();
        ddlMedicos.SelectedValue = dt1.Rows[0]["IsMedicos"].ToString();
        txtTentativeDays.Text =  Convert.ToInt32( "0"+dt1.Rows[0]["TentativeDay"].ToString()).ToString();
        txtCostPP.Text = Decimal.ToInt32(  Convert.ToDecimal(dt1.Rows[0]["IndvCost"].ToString())).ToString();
        txtIncomePP.Text = Decimal.ToInt32( Convert.ToDecimal(dt1.Rows[0]["IndvIncome"].ToString())).ToString();

        hfId.Value = dt1.Rows[0]["TrainId"].ToString();
        
    }
    private void LoadTrainingGrid() {
        grTrainingList.DataSource = null;
        grTrainingList.DataBind();

        try
        {
            dtList = objEmpMgr.SelectTrainingList("0");
            grTrainingList.DataSource = dtList;
            grTrainingList.DataBind();
        }
        catch (Exception ex)
        {
            throw (ex);
            lblMsg.Text = "Error :";
        }

    }
    private void OpenRecord()
    {

        grList.DataSource = null;
        grList.DataBind();

        try
        {
            personTable = ViewState["dt"] as DataTable;
            grList.DataSource = personTable;
            grList.DataBind();
        }
        catch (Exception ex)
        {
            throw (ex);
            lblMsg.Text = "Error :";
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
                    if (ddlCategoty.SelectedIndex == 0)
                    {
                        lblMsg.Text = "Please select Training Category";
                        return false;
                    }
                    else if (grList.Rows.Count==0)
                    {
                        lblMsg.Text = "Please add designation";
                        return false;
                    }
                    break;
                case "Add":
                    if (ddlDesig.SelectedIndex == 0)
                    {
                        lblMsg.Text = "Please select Designation";
                        return false;
                    }
                    else if(txtPeriod.Text=="")
                    {
                        lblMsg.Text = "Please enter Period(MM)";
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
    private void SaveData(string IsDelete)
    {
        try
        {
            if (ValidateAndSave("Save") == false)
            {
                return;
            }
            if (hfIsUpdate.Value == "Y")
                hfId.Value = hfId.Value;
            else
                hfId.Value = Common.getMaxId("TrTrainingList", "TrainId");

            clsTrTrainingList objCommonSetup = new clsTrTrainingList(
                                                                hfId.Value.ToString(),
                                                                txtName.Text.Trim(),
                                                                ddlCategoty.SelectedValue.ToString(),
                                                                txtTentativeDays.Text.ToString(),
                                                                ddlInOut.SelectedValue.ToString(),
                                                                ddlMedicos.SelectedValue.ToString(),
                                                                txtCostPP.Text.Trim(),
                                                                txtIncomePP.Text.Trim(),
                                                                Session["USERID"].ToString(),
                                                                Common.SetDateTime(DateTime.Now.ToString())
                                                                );

            objEmpMgr.InsertTrainingList(grList, objCommonSetup, (chkInActive.Checked == true ? "N" : "Y"), hfIsUpdate.Value, IsDelete);

            lblMsg.Text = Common.GetMessage(hfIsUpdate.Value.ToString(), IsDelete);
            Common.EmptyTextBoxValues(this);
            this.EntryMode(false);
            this.OpenRecord();
            grList.DataSource = null;
            grList.DataBind();
            this.LoadTrainingGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.SaveData("N");
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Common.EmptyTextBoxValues(this);
        grList.DataSource = null;
        grList.DataBind();
        this.EntryMode(false);
        //this.OpenRecord();
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
    protected void grTrainingList_RowCommand(object sender, GridViewCommandEventArgs e)
    { 
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
       
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
               
                Common.EmptyTextBoxValues(this);
                hfId.Value = grTrainingList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                DataTable dtTraining = objEmpMgr.SelectTrainingEditList(grTrainingList.DataKeys[_gridView.SelectedIndex].Values[0].ToString());
                if (dtTraining.Rows.Count > 0)
                {
                    txtName.Text = dtTraining.Rows[0]["TrainName"].ToString();
                    ddlCategoty.SelectedValue = dtTraining.Rows[0]["TrCategoryId"].ToString();
                    txtTentativeDays.Text = dtTraining.Rows[0]["TentativeDay"].ToString();
                    ddlInOut.SelectedValue = dtTraining.Rows[0]["IsInHouse"].ToString();
                    ddlMedicos.SelectedValue = dtTraining.Rows[0]["IsMedicos"].ToString();
                    txtCostPP.Text =dtTraining.Rows[0]["IndvCost"].ToString();
                    txtIncomePP.Text =dtTraining.Rows[0]["IndvIncome"].ToString();
                    if(dtTraining.Rows[0]["IsActive"].ToString()=="Y")
                        chkInActive.Checked = false;
                    else
                        chkInActive.Checked = true;
                }
               
                DataTable dtTrainingDtl = objEmpMgr.SelectTrainingDtlList(grTrainingList.DataKeys[_gridView.SelectedIndex].Values[0].ToString());
                if (dtTrainingDtl.Rows.Count > 0) 
                {
                    grList.DataSource = null;
                    grList.DataBind();
                    grList.DataSource = dtTrainingDtl;
                    grList.DataBind();
                }
                this.EntryMode(true);
                lblMsg.Text = "";
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
                ddlDesig.SelectedValue = grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                txtPeriod.Text = grList.DataKeys[_gridView.SelectedIndex].Values[2].ToString();
                break;

            case ("Delete"):
                personTable = ViewState["dt"] as DataTable;
                try
                {
                    personTable.Clear();
                    personTable = Common.AddGridRow(grList, ddlDesig.SelectedValue.ToString(), ddlDesig.SelectedItem.Text.ToString(), txtPeriod.Text.ToString(), "Copy", personTable);
                    DataRow[] drr = personTable.Select("DesigId=" + grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString());
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
                    lblMsg.Text = "Delete Error :" + ex.ToString();
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
        {
            return;
        }
        DataTable dt = ViewState["dt"] as DataTable;
        dt.Clear();
        dt = Common.AddGridRow(grList, ddlDesig.SelectedValue.ToString(), ddlDesig.SelectedItem.Text.ToString(), txtPeriod.Text.ToString(),"Add", dt);
        grList.DataSource = dt;
        grList.DataBind();
        this.ClearControl();
    }
}
