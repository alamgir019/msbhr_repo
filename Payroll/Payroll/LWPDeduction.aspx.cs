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

public partial class Payroll_Payroll_LWPDeduction : System.Web.UI.Page
{
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();

    Payroll_VariableAllowanceManager objVarMgr = new Payroll_VariableAllowanceManager();

    DataTable dtEmpInfo = new DataTable();
    DataTable dtReAllow = new DataTable();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            Common.FillYearList(5,ddlYear);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            this.EntryMode(false);                       
        }
    }   

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        this.EntryMode(false);
        this.ClearControls();
    }

    private void ClearControls()
    {
        lblName.Text = "";
        lblDesignation.Text = "";
        lblDept.Text = "";
        lblSector.Text = "";
        txtEntryDate.Text = "";
        lblJoinDate.Text = "";
        lblGrossSalary.Text = "";
        txtEntryDate.Text = "";

        txtRemarks.Text = "";    
        grList.DataSource = null;
        grList.DataBind();
    }

    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (string.IsNullOrEmpty(txtEmpID.Text.Trim()) == false)
        {
            this.FillEmpInfo(txtEmpID.Text.Trim());
            this.OpenRecord();
        }
    }

    private void FillEmpInfo(string EmpId)
    {
        if (txtEmpID.Text.Trim() == "")
            return;

        dtEmpInfo = objEmpMgr.SelectEmpInfoHRAction(txtEmpID.Text.Trim());
        if (dtEmpInfo.Rows.Count == 0)
        {
            lblMsg.Text = "Invalid Employee No .";
            return;
        }
        else
        {
            lblMsg.Text = "";
            foreach (DataRow dRow in dtEmpInfo.Rows)
            {
                lblName.Text = dRow["FullName"].ToString();
                lblName.ToolTip = dRow["EmpTypeId"].ToString();
                lblDesignation.Text = dRow["DesigName"].ToString().Trim();
                lblSector.Text = dRow["ProjectName"].ToString().Trim();
                lblDept.Text = dRow["DeptName"].ToString().Trim();
                lblJoinDate.Text = Common.DisplayDate(dRow["JoiningDate"].ToString());
                lblJoinDate.ToolTip = dRow["LeavePakId"].ToString();
                           
                lblGrossSalary.Text = dRow["GrossSalary"].ToString().Trim();
                lblGrossSalary.ToolTip = dRow["SalPakId"].ToString().Trim();
            }            
        }
    }

    private void OpenRecord()
    {
        dtReAllow = objVarMgr.SelectLWPDeduction("0", txtEmpID.Text.Trim());
        grList.DataSource = dtReAllow;
        grList.DataBind();
        foreach (GridViewRow gRow in grList.Rows)
        {            
         
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave("N") == true)
            SaveData("N");
    }
    protected bool ValidateAndSave(string IsDelete)
    {
        try
        {
            if (hfIsUpdate.Value == "N")
            {
                // validate with From date
                ////if (objVarMgr.IsDuplicateData("9", Common.ReturnDate(ddlMonth ), txtEmpID.Text.Trim()) == true)
                ////{
                ////    lblMsg.Text = "Record cannot save. Duplicate record exist.";
                ////    return false ;
                ////}
                ////// validate with To date
                ////if (objVarMgr.IsDuplicateData("9", Common.ReturnDate(txtEndDate.Text.Trim()), txtEmpID.Text.Trim()) == true)
                ////{
                ////    lblMsg.Text = "Record cannot save. Duplicate record exist.";
                ////    return false ;
                ////}
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
        string strID = "";
        try
        {
            //Filling Class Properties with values
            if (hfIsUpdate.Value == "Y")
                strID = hfID.Value;
            else
                strID = Common.getMaxId("VARIABLEALLOWANCEDEDUCT", "VID");

            if (IsDelete == "N")
            {
                objVarMgr.InsertVariableAllowanceData(strID, txtEmpID.Text.Trim(), "9", txtDays.Text.Trim(), txtAmount.Text.Trim(),
                   ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), "Y", hfIsUpdate.Value.ToString(), Session["USERID"].ToString(),
                   Common.SetDateTime(DateTime.Now.ToString()), txtRemarks.Text.Trim());

                if (hfIsUpdate.Value == "N")
                    lblMsg.Text = "Record Saved Successfully";
                else
                    lblMsg.Text = "Record Updated Successfully";
            }
            else
            {
                objVarMgr.DeleteData(strID);
                lblMsg.Text = "Record Deleted Successfully";
            }
            //Common.EmptyTextBoxValues(this);
            this.EntryMode(false);
            this.OpenRecord();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
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
                hfIsUpdate.Value = "N";
                hfID.Value = grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();               
                ddlMonth.Text = Common.CheckNullString(grList.SelectedRow.Cells[2].Text);
                ddlYear.Text = Common.CheckNullString(grList.SelectedRow.Cells[3].Text);                
                txtDays.Text = Common.CheckNullString(grList.SelectedRow.Cells[4].Text.Trim());
                txtAmount.Text = Common.CheckNullString(grList.SelectedRow.Cells[5].Text.Trim());
                txtRemarks.Text = Common.CheckNullString(grList.SelectedRow.Cells[6].Text.Trim());
                this.EntryMode(true);
                break;
            case ("RowDeleting"):
                hfIsUpdate.Value = "N";
                SaveData("Y");
                break;
        }
    }
    protected void txtDays_TextChanged(object sender, EventArgs e)
    {
        decimal dclLWPAmt = 0;

        if ((string.IsNullOrEmpty(txtDays.Text) == false) && (string.IsNullOrEmpty(lblGrossSalary.Text) == false))
        {
            dclLWPAmt = (Convert.ToDecimal(lblGrossSalary.Text) / 30) * Convert.ToDecimal(txtDays.Text);
        }

        txtAmount.Text =dclLWPAmt.ToString (); 
    }
}
