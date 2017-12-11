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

public partial class EIS_HRAction_Confirmation : System.Web.UI.Page
{
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    
    DataTable dtEmpInfo = new DataTable();
    DataTable dtConfrim = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList_Nil(objEmpInfoMgr.SelectNatureWiseAction("C"), ddlAction);
            Common.FillDropDownList_Nil(objMasMgr.SelectEmpType(0, "Y"), ddlEmpType);
            this.EntryMode(false);
        }
    }

    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
        {
            btnSave.Text = "Update";
            hfIsUpdate.Value = "Y";
            lblMsg.Text = "";
        }
        else
        {
            this.ClearControl();            
            btnSave.Text = "Save";
            hfIsUpdate.Value = "N";
            txtEntryDate.Text = Common.DisplayDate(Common.SetDate(DateTime.Today.ToShortDateString()));   
        }
    }
    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (txtEmpID.Text.Trim() == "")
            return;

        dtEmpInfo = objEmpInfoMgr.SelectEmpInfoHRAction(txtEmpID.Text.Trim());
        if (dtEmpInfo.Rows.Count == 0)
        {
            lblMsg.Text = "Invalid Employee No.";
            return;
        }
        else
        {
            if (Common.CheckNullString(dtEmpInfo.Rows[0]["EmpStatus"].ToString()) == "I")
            {
                lblMsg.Text = "This Staff Has Been Separated.";
                return;
            }
            else
            {
                lblMsg.Text = "";
                foreach (DataRow dRow in dtEmpInfo.Rows)
                {
                    lblName.Text = dRow["FullName"].ToString();
                    lblName.ToolTip = dRow["EmpTypeId"].ToString();
                    lblEmpType.Text = dRow["TypeName"].ToString();
                    lblDesignation.Text = dRow["DesigName"].ToString().Trim();
                    lblSector.Text = dRow["ProjectName"].ToString().Trim();
                    lblDept.Text = dRow["DeptName"].ToString().Trim();
                    lblJoinDate.Text = Common.DisplayDate(dRow["JoiningDate"].ToString());
                    lblJoinDate.ToolTip = dRow["LeavePakId"].ToString();
                    txtProbationPeriod.Text = dRow["ProbationPeriod"].ToString().Trim();
                    if (string.IsNullOrEmpty(dRow["JoiningDate"].ToString().Trim()) == false)
                        txtStartDate.Text = Common.DisplayDate(dRow["JoiningDate"].ToString().Trim());
                    if (string.IsNullOrEmpty(dRow["ConfirmationDate"].ToString().Trim()) == false)
                        txtConfirmDate.Text = Common.DisplayDate(dRow["ConfirmationDate"].ToString().Trim());
                    lblBasicSalary.Text = dRow["GrossSalary"].ToString().Trim();
                    lblBasicSalary.ToolTip = dRow["SalPakId"].ToString().Trim();
                    txtNewGrossSalary.Text = dRow["GrossSalary"].ToString().Trim();
                }
                this.OpenRecord();
            }
        }
    }

    private void OpenRecord()
    {
        dtConfrim = objEmpInfoMgr.SelectConfirmation(0, txtEmpID.Text.Trim());
        grConfirmation.DataSource = dtConfrim;
        grConfirmation.DataBind();

        if (dtConfrim.Rows.Count > 0)
        {
            foreach (GridViewRow gRow in grConfirmation.Rows)
            {
                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[2].Text)) == false)
                    gRow.Cells[2].Text = Common.DisplayDate(gRow.Cells[2].Text);                
                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[3].Text)) == false)
                    gRow.Cells[3].Text = Common.DisplayDate(gRow.Cells[3].Text);
                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[4].Text)) == false)
                    gRow.Cells[4].Text = Common.DisplayDate(gRow.Cells[4].Text);
            }
        }
    }
   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
        {
            this.SaveData();
        }
    }

    protected bool ValidateAndSave()
    {
        try
        {
            if (ddlAction.SelectedIndex == 0)
            {
                lblMsg.Text = "Please Select The Action From The List.";
                ddlAction.Focus();
                return false;
            }

            if (txtConfirmDate.Text.Trim()   == "")
            {
                lblMsg.Text = "Please enter confirmation date.";
                txtConfirmDate.Focus();  
                return false;
            }
           
            return true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }
    
    protected void SaveData()
    {
        dsPayroll_SalaryPackage objDs = new dsPayroll_SalaryPackage();
        Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();

        string strEntryDate = "";
        string strConfirmDueDate = "";
        string strExtensionDate = "";
        string strConfirmDate = "";
        decimal dclNewBasicSal = 0;

        if (string.IsNullOrEmpty(txtEntryDate.Text.Trim()) == false)
            strEntryDate = Common.ReturnDate(txtEntryDate.Text.Trim());

        if (string.IsNullOrEmpty(txtConfirmDueDate.Text.Trim()) == false)
            strConfirmDueDate = Common.ReturnDate(txtConfirmDueDate.Text.Trim());

        if (string.IsNullOrEmpty(txtExtensionDate.Text.Trim()) == false)
            strExtensionDate = Common.ReturnDate(txtExtensionDate.Text.Trim());

        if (string.IsNullOrEmpty(txtConfirmDate.Text.Trim()) == false)
            strConfirmDate = Common.ReturnDate(txtConfirmDate.Text.Trim());

        
            DataTable dtBfPlc = objOptMgr.SelectPayrollBenefitsPolicyData("0", "1");
            DataRow[] foundPlcRow;
            foundPlcRow = null;

            //Basic
            foundPlcRow = dtBfPlc.Select("SHEADID=1");
            if (foundPlcRow.Length > 0)
            {
                DataRow nRow1 = objDs.dtSalPackUpdate.NewRow();
                nRow1["SHEADID"] = 1;
                nRow1["PAYAMT"] = objEmpInfoMgr.GetHeadAmount(Common.RoundDecimal(txtNewGrossSalary.Text, 0), Common.RoundDecimal(foundPlcRow[0]["Value"].ToString(), 2));
                dclNewBasicSal = Convert.ToDecimal(nRow1["PAYAMT"].ToString());
                objDs.dtSalPackUpdate.Rows.Add(nRow1);
            }
            //House Rent
            foundPlcRow = dtBfPlc.Select("SHEADID=2");
            if (foundPlcRow.Length > 0)
            {
                DataRow nRow2 = objDs.dtSalPackUpdate.NewRow();
                nRow2["SHEADID"] = 2;
                nRow2["PAYAMT"] = objEmpInfoMgr.GetHeadAmount(Common.RoundDecimal(txtNewGrossSalary.Text, 0), Common.RoundDecimal(foundPlcRow[0]["Value"].ToString(), 2));
                objDs.dtSalPackUpdate.Rows.Add(nRow2);
            }

            //Medical
            foundPlcRow = dtBfPlc.Select("SHEADID=3");
            if (foundPlcRow.Length > 0)
            {
                DataRow nRow3 = objDs.dtSalPackUpdate.NewRow();
                nRow3["SHEADID"] = 3;
                nRow3["PAYAMT"] = objEmpInfoMgr.GetHeadAmount(Common.RoundDecimal(txtNewGrossSalary.Text, 0), Common.RoundDecimal(foundPlcRow[0]["Value"].ToString(), 2));
                objDs.dtSalPackUpdate.Rows.Add(nRow3);
            }
            if (ddlEmpType.SelectedValue.ToString() == "1")
            {
                //PF Allowance 
                foundPlcRow = dtBfPlc.Select("SHEADID=8");
                if (dtBfPlc.Rows.Count > 0)
                {
                    DataRow nRow4 = objDs.dtSalPackUpdate.NewRow();
                    nRow4["SHEADID"] = 8;
                    nRow4["PAYAMT"] = objEmpInfoMgr.GetHeadAmount(Common.RoundDecimal(dclNewBasicSal.ToString(), 0), Common.RoundDecimal(foundPlcRow[0]["Value"].ToString(), 2));
                    objDs.dtSalPackUpdate.Rows.Add(nRow4);
                }

                objDs.dtSalPackUpdate.AcceptChanges();
            }

         if (hfIsUpdate.Value == "Y") 
            hfId.Value = hfId.Value;
        else
            hfId.Value = Common.getMaxId("EmpConfirmationLog", "ConfirmId");
            objEmpInfoMgr.InsertConfirmation(hfId.Value.ToString(), txtEmpID.Text.Trim(), ddlAction.SelectedValue.ToString(), strEntryDate, txtProbationPeriod.Text.Trim(), strConfirmDueDate, strExtensionDate,
            strConfirmDate,txtExtensionMonth.Text.Trim(),txtRemarks.Text.Trim(),Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), hfIsUpdate.Value.ToString(),lblBasicSalary.ToolTip.ToString(),
            objDs.dtSalPackUpdate, "Y", lblJoinDate.Text.Trim(), lblJoinDate.ToolTip.Trim(), ddlEmpType.SelectedValue.ToString(), txtNewGrossSalary.Text, dclNewBasicSal.ToString() );

        if (hfIsUpdate.Value == "N")
            lblMsg.Text = "Record Saved Successfully";
        else
            lblMsg.Text = "Record Updated Successfully";
        this.OpenRecord();
        this.EntryMode(false);
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {        
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
        grConfirmation.DataSource = null;
        grConfirmation.DataBind();
        lblMsg.Text = "";
    }

    protected void ClearControl()
    {
        lblName.Text = "";
        lblEmpType.Text = "";
        lblDesignation.Text = "";
        lblDept.Text = "";
        lblSector.Text = "";
        txtEntryDate.Text = "";
        lblJoinDate.Text = "";
        lblBasicSalary.Text = "";
        txtEntryDate.Text = "";
 
        ddlAction.SelectedIndex = -1;
        ddlEmpType.SelectedIndex = 0;
        txtNewGrossSalary.Text = "0"; 
        txtProbationPeriod.Text = "";
        txtStartDate.Text = ""; 
        txtConfirmDueDate.Text = "";
        txtExtensionDate.Text = "";
        txtExtensionMonth.Text = ""; 
        txtConfirmDate.Text = "";
        txtRemarks.Text = "";        
    }
    
    protected void grConfirmation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):

                ddlAction.SelectedValue = grConfirmation.DataKeys[_gridView.SelectedIndex].Values[1].ToString();
                hfId.Value = grConfirmation.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                txtProbationPeriod.Text = Common.CheckNullString(grConfirmation.SelectedRow.Cells[1].Text.Trim());
                txtConfirmDueDate.Text = Common.CheckNullString(grConfirmation.SelectedRow.Cells[2].Text.Trim());
                txtExtensionDate.Text = Common.CheckNullString(grConfirmation.SelectedRow.Cells[3].Text.Trim());
                txtConfirmDate.Text = Common.CheckNullString(grConfirmation.SelectedRow.Cells[4].Text.Trim());
                txtExtensionMonth.Text = Common.CheckNullString(grConfirmation.SelectedRow.Cells[5].Text.Trim());
                txtRemarks.Text = Common.CheckNullString(grConfirmation.SelectedRow.Cells[6].Text.Trim());
                this.EntryMode(true);
                lblMsg.Text = ""; 
                break;

        }
    }
    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        DateTime dtJoinDate = new DateTime();
        DateTime dtConfirmDueDate = new DateTime();
        if ((string.IsNullOrEmpty(txtProbationPeriod.Text) == false) && (string.IsNullOrEmpty(txtStartDate.Text) == false))
        {
            dtJoinDate = Convert.ToDateTime(Common.ReturnDate(txtStartDate.Text.Trim()));
            dtConfirmDueDate = dtJoinDate.AddMonths(Convert.ToInt32(txtProbationPeriod.Text.Trim()));
            txtConfirmDueDate.Text = Common.DisplayDate(dtConfirmDueDate.ToString());
            txtConfirmDate.Text = txtConfirmDueDate.Text;            
        }
    }
    protected void ddlEmpType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAction.SelectedValue.ToString() == "10")
        {
            ddlEmpType.SelectedIndex = 1;
            txtProbationPeriod.Enabled = false;
            btnCalculate.Enabled = false;
            txtStartDate.Enabled = false;
            txtConfirmDueDate.Enabled = false;
            txtExtensionDate.Enabled = false;
            txtExtensionMonth.Enabled = false;
            txtConfirmDate.Enabled = true;
        }
        else
        {
            ddlEmpType.SelectedIndex = 2;
            txtProbationPeriod.Enabled = true;
            btnCalculate.Enabled = true;
            txtStartDate.Enabled = true;
            txtConfirmDueDate.Enabled = true;
            txtExtensionDate.Enabled = true;
            txtExtensionMonth.Enabled = true;
            txtConfirmDate.Enabled = false ;
        }
    }
}
