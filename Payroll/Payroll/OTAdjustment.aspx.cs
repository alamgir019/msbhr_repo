using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Payroll_Payroll_OTAdjustment : System.Web.UI.Page
{
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    SOFManager objSOFMgr = new SOFManager();
    Payroll_MasterMgr objPayroll = new Payroll_MasterMgr();

    DataTable dtEmpInfo = new DataTable();
    DataTable dtSalaryCharge = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.EntryMode(false);
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            txtEntryDate.Text = Common.DisplayDate(Common.SetDate(DateTime.Today.ToShortDateString()));
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

    private void ClearControls()
    {
        txtEmpID.Text = "";
        lblName.Text = "";
        lblDesignation.Text = "";
        lblDept.Text = "";
        lblLocation.Text = "";

        grList.DataSource = null;
        grList.DataBind();
    }

    private void ClearSubControls()
    {
        txtBasicSalary.Text = "";
        txtOTAmount.Text = "";
        txtOTAmtHr.Text = "";
        txtOTApproveHr.Text = "";
        txtOTHour.Text = "";
        ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
        ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
    }

    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (txtEmpID.Text.Trim() == "")
            return;

        dtEmpInfo = objEmpMgr.SelectEmpInfoHRAction(txtEmpID.Text.Trim());
        if (dtEmpInfo.Rows.Count == 0)
        {
            lblMsg.Text = txtEmpID.Text.Trim() + " is not valid Emp Id .";
            this.EntryMode(false);
            return;
        }
        else
        {
            lblMsg.Text = "";
            foreach (DataRow dRow in dtEmpInfo.Rows)
            {
                lblName.Text = dRow["FullName"].ToString();
                lblDesignation.Text = dRow["DesigName"].ToString().Trim();
                lblDept.Text = dRow["DeptName"].ToString().Trim();
                txtBasicSalary.Text = dRow["BasicSalary"].ToString().Trim();
                lblLocation.Text = dRow["PostingPlaceName"].ToString().Trim();

                if (string.IsNullOrEmpty(dRow["BasicSalary"].ToString().Trim()) == false)
                {
                    txtOTAmtHr.Text = Convert.ToString(Math.Round(((Convert.ToDecimal(dRow["BasicSalary"].ToString().Trim()) * 2) / (22 * 8)), 0));
                }
            }
            this.OpenRecord();
        }
    }

    private void OpenRecord()
    {
        grList.DataSource = objPayroll.SelectOTAdjustment(txtEmpID.Text.Trim());
        grList.DataBind();

        foreach (GridViewRow gRow in grList.Rows)
        {
            if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[8].Text)) == false)
                gRow.Cells[8].Text = Common.DisplayDate(gRow.Cells[8].Text);
        }        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
            this.SaveData("N");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
            this.SaveData("Y");
    }

    protected bool ValidateAndSave()
    {
        try
        {
            //if (ddlSalarySource.SelectedIndex == 0)
            //{
            //    lblMsg.Text = "Please Select The Salary Charging From The List.";
            //    ddlSalarySource.Focus();
            //    return false;
            //}

            //if (string.IsNullOrEmpty(txtPercentage.Text) == true)
            //{
            //    lblMsg.Text = "Please enter salary source percentage.";
            //    txtPercentage.Focus();
            //    return false;
            //}
            
            return true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        this.EntryMode(false);
        this.ClearControls();
        this.ClearSubControls();
        lblMsg.Text = "";
    }

    private void SaveData(string IsDelete)
    {
        try
        {
            if (hfIsUpdate.Value == "N")
                hfID.Value = Common.getMaxId("OTAdjustment", "TransId");
            else
                hfID.Value = hfID.Value.ToString();

            clsOTAdjustment obj = new clsOTAdjustment();
            obj.TransId = hfID.Value.ToString();
            obj.BasicSal = txtBasicSalary.Text.Trim();
            obj.EmpId = txtEmpID.Text.Trim();
            obj.EntryDate = Common.ReturnDate(txtEntryDate.Text.Trim());
            obj.Month = ddlMonth.SelectedValue.Trim();
            obj.OTAmount = txtOTAmount.Text.Trim();
            obj.OTAppHour = txtOTApproveHr.Text.Trim();
            obj.OTHour = txtOTHour.Text.Trim();
            obj.Year = ddlYear.SelectedValue.Trim();                        
            obj.InsertedBy = Session["USERID"].ToString();
            obj.InsertedDate = Common.SetDateTime(DateTime.Now.ToString());

            objPayroll.InsertOTAdjustment(obj, hfIsUpdate.Value, IsDelete);

            if (hfIsUpdate.Value == "N" && IsDelete == "N")
                lblMsg.Text = "Record Saved Successfully";
            else if (hfIsUpdate.Value == "Y" && IsDelete == "N")
                lblMsg.Text = "Record Updated Successfully";
            else if (hfIsUpdate.Value == "Y" && IsDelete == "Y")
                lblMsg.Text = "Record Deleted Successfully";

            this.OpenRecord();
            this.EntryMode(false);
            this.ClearSubControls();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Input is not valid.";
        }
    }

    protected void grList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                hfIsUpdate.Value = "N";
                hfID.Value = grList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                txtBasicSalary.Text = Common.CheckNullString(grList.SelectedRow.Cells[6].Text);
                txtEntryDate.Text = Common.CheckNullString(grList.SelectedRow.Cells[8].Text);
                txtOTAmount.Text = Common.CheckNullString(grList.SelectedRow.Cells[7].Text);
                txtOTAmtHr.Text = "";
                txtOTApproveHr.Text = Common.CheckNullString(grList.SelectedRow.Cells[5].Text);
                txtOTHour.Text = Common.CheckNullString(grList.SelectedRow.Cells[4].Text);
                ddlMonth.SelectedValue = Common.CheckNullString(grList.SelectedRow.Cells[2].Text);
                ddlYear.SelectedValue = Common.CheckNullString(grList.SelectedRow.Cells[3].Text);
                this.EntryMode(true);
                break;
        }
    }
}
