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

public partial class EIS_HRAction_SalaryIncrement : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();

    DataTable dtEmpInfo = new DataTable();
    DataTable dtConfrim = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList_Nil(objMasMgr.SelectAction(0, "I"), ddlAction);
        }
    }

    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (string.IsNullOrEmpty(txtEmpID.Text.Trim()) == false)
        {
            this.FillEmpInfo(txtEmpID.Text.Trim());
            this.OpenRecord();
            this.EntryMode(false);
        }
        else
        {
            this.EntryMode(false);
        }
    }

    private void FillEmpInfo(string EmpId)
    {
        dtEmpInfo = objEmpInfoMgr.SelectEmpInfoHRAction(txtEmpID.Text.Trim());
       
        if (dtEmpInfo.Rows.Count > 0)
        {
            if (Common.CheckNullString(dtEmpInfo.Rows[0]["EmpStatus"].ToString()) == "I")
            {
                lblMsg.Text = "This Staff Has Been Separated.";
                return;
            }
            else
            {
                foreach (DataRow row in dtEmpInfo.Rows)
                {
                    lblName.Text = row["FullName"].ToString().Trim();
                    lblName.ToolTip = row["EmpTypeId"].ToString();
                    lblEmpType.Text = row["TypeName"].ToString();
                    lblOffice_Loc.Text = row["CompanyName"].ToString().Trim();
                    lblDeg_Project.Text = row["DesigName"].ToString().Trim();
                    lblDept.Text = row["DeptName"].ToString().Trim();
                    lblSubDept.Text = row["SubDeptName"].ToString().Trim();
                    lblSuncode.Text = row["ClinicName"].ToString().Trim();
                    lblGrossSal.Text = row["GrossSalary"].ToString().Trim();
                    lblSalPac.Text = row["SPTitle"].ToString().Trim();
                    lblSalPac.ToolTip = row["SalPakId"].ToString().Trim();

                }
            }
        }
        else
        {
            lblMsg.Text = "Employee code is not valid or not under your office.";
            txtEmpID.Text = "";
            lblName.Text = "";
            lblDeg_Project.Text = "";
            lblOffice_Loc.Text = "";
            lblDept.Text = "";
            lblSubDept.Text = "";
            lblSuncode.Text = "";
            lblSalPac.Text = "";
            lblSalPac.ToolTip = "";
            lblGrossSal.Text = "";
            return;
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
            ddlAction.SelectedIndex = -1;
            txtCOLA.Text = "";
            txtGpPer.Text = "";
            txtInvPer.Text = ""; 
            txtNewGrossSalary.Text = ""; 
            txtEffDate.Text = "";
            chkIsPercent.Checked = false;
            txtIncrement.Text = "";
            txtRemarks.Text = "";
        }
    }

    private void ClearControls()
    {
        txtEmpID.Text = "";
        lblName.Text = "";
        lblEmpType.Text = ""; 
        lblDeg_Project.Text = "";
        lblOffice_Loc.Text = "";
        lblDept.Text = "";
        lblSubDept.Text = "";
        lblSuncode.Text = "";
        lblSalPac.Text = "";
        lblSalPac.ToolTip = "";
        lblGrossSal.Text = "";

        grConfirmation.DataSource = null;
        grConfirmation.DataBind();
    }

    private void OpenRecord()
    {
        dtConfrim = objEmpInfoMgr.SelectEmpSalaryIncrement(0, txtEmpID.Text.Trim());
        grConfirmation.DataSource = dtConfrim;
        grConfirmation.DataBind();

        if (dtConfrim.Rows.Count > 0)
        {
            foreach (GridViewRow gRow in grConfirmation.Rows)
            {
                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[1].Text)) == false)
                    gRow.Cells[1].Text = Common.DisplayDate(gRow.Cells[1].Text);
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
        if (txtEmpID.Text == "")
        {
            lblMsg.Text = "Please select Emp code.";
            txtEmpID.Focus();
            return false;
        }

        if (ddlAction.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select an action from the list.";
            ddlAction.Focus();
            return false;
        }

        if (txtNewGrossSalary.Text == "")
        {
            lblMsg.Text = "Please calculate new gross salary.";
            txtNewGrossSalary.Focus();
            return false;
        }
        if (txtEffDate.Text == "")
        {
            lblMsg.Text = "Please enter effective date.";
            txtEffDate.Focus();
            return false;
        }
        return true;
    }

    private void SaveData()
    {
        dsPayroll_SalaryPackage objDs = new dsPayroll_SalaryPackage();
        Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();

        string IsDelete = "N";
        long lngID = 0;
        try
        {
            string strConfrimDate = "";
            decimal dclNewBasicSal = 0;

            if (string.IsNullOrEmpty(txtEffDate.Text.Trim()) == false)
                strConfrimDate = Common.ReturnDate(txtEffDate.Text.Trim());

            if (hfIsUpdate.Value == "N")
                lngID = objDB.GerMaxIDNumber("EmpSalaryIncrementLog", "LogId");
            else
                lngID = Convert.ToInt32(hfID.Value);

            DataTable dtBfPlc = objOptMgr.SelectPayrollBenefitsPolicyData("0", lblName.ToolTip.ToString());
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

            //PF Allowance 
            foundPlcRow = dtBfPlc.Select("SHEADID=8");
            if (foundPlcRow.Length > 0)
            {
                DataRow nRow4 = objDs.dtSalPackUpdate.NewRow();
                nRow4["SHEADID"] = 8;
                nRow4["PAYAMT"] = objEmpInfoMgr.GetHeadAmount(Common.RoundDecimal(dclNewBasicSal.ToString(), 0), Common.RoundDecimal(foundPlcRow[0]["Value"].ToString(), 2));
                objDs.dtSalPackUpdate.Rows.Add(nRow4);
            }

            objDs.dtSalPackUpdate.AcceptChanges();

            objEmpInfoMgr.InsertEmpSalaryIncrement(lngID.ToString(), txtEmpID.Text.Trim(), ddlAction.SelectedValue.ToString(), ddlAction.SelectedItem.ToString(),
                strConfrimDate, Common.ReturnZeroForNull(txtCOLA.Text.Trim()), Common.ReturnZeroForNull(txtGpPer.Text.Trim()), Common.ReturnZeroForNull(txtInvPer.Text.Trim()),
                txtNewGrossSalary.Text.Trim(), dclNewBasicSal.ToString(), txtRemarks.Text.Trim(), txtIncrement.Text, txtNewGrossSalary.ToolTip.ToString(), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), hfIsUpdate.Value, IsDelete,
                lblSalPac.ToolTip.ToString(), objDs.dtSalPackUpdate);

            if (hfIsUpdate.Value == "N")
                lblMsg.Text = "Record Saved Successfully";
            else
                lblMsg.Text = "Record Updated Successfully";
            this.EntryMode(false);
            this.ClearControls();
            //this.OpenRecord();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
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
                hfID.Value = grConfirmation.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                txtEffDate.Text = Common.CheckNullString(grConfirmation.SelectedRow.Cells[1].Text.Trim());
                txtCOLA.Text = Common.CheckNullString(grConfirmation.SelectedRow.Cells[2].Text.Trim());
                txtGpPer.Text = Common.CheckNullString(grConfirmation.SelectedRow.Cells[3].Text.Trim());
                txtInvPer.Text = Common.CheckNullString(grConfirmation.SelectedRow.Cells[4].Text.Trim());
                txtNewGrossSalary.Text = Common.CheckNullString(grConfirmation.SelectedRow.Cells[5].Text.Trim());
                txtIncrement.Text = Common.CheckNullString(grConfirmation.SelectedRow.Cells[6].Text.Trim());
                //txtIncPercentage.Text = Common.CheckNullString(grConfirmation.SelectedRow.Cells[7].Text.Trim());
                txtRemarks.Text = Common.CheckNullString(grConfirmation.SelectedRow.Cells[8].Text.Trim());
                this.EntryMode(true);
                break;
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        this.EntryMode(false);
        this.ClearControls();
    }

    /*protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfID.Value) == false)
        {
            this.SaveData("Y");
        }
        else
        {
            lblMsg.Text = "Select an confirmation info. first from the list then try to delete.";
        }

        this.EntryMode(false);
    }*/



    private void CalculateGrossSalary()
    {
        decimal dclGrossSal = 0;
        decimal dclTotalPercent = 0;
        
        if (ddlAction.SelectedValue == "30")
        {
            dclTotalPercent = Convert.ToDecimal(Common.ReturnZeroForNull(txtCOLA.Text)) + Convert.ToDecimal(Common.ReturnZeroForNull(txtGpPer.Text)) + Convert.ToDecimal(Common.ReturnZeroForNull(txtInvPer.Text));
            dclGrossSal = (Convert.ToDecimal(lblGrossSal.Text)) + ((Convert.ToDecimal(lblGrossSal.Text)) * dclTotalPercent) / 100;
            txtNewGrossSalary.Text = dclGrossSal.ToString();

            dclTotalPercent = Convert.ToDecimal(Common.ReturnZeroForNull(txtCOLA.Text)) + Convert.ToDecimal(Common.ReturnZeroForNull(txtInvPer.Text)) + Convert.ToDecimal(Common.ReturnZeroForNull(txtGpPer.Text));

            txtIncrement.Text = dclTotalPercent.ToString();
            txtNewGrossSalary.ToolTip = Convert.ToString(Convert.ToDecimal(txtNewGrossSalary.Text) - Convert.ToDecimal(lblGrossSal.Text));//Increment Amount
        }
        else if (ddlAction.SelectedValue == "31")
        {
            if (chkIsPercent.Checked == true)
            {
                dclTotalPercent = Convert.ToDecimal(txtIncrement.Text);
                dclGrossSal = Convert.ToDecimal(lblGrossSal.Text);
                dclGrossSal = dclGrossSal + ((dclGrossSal * Convert.ToDecimal(Common.ReturnZeroForNull(txtIncrement.Text))) / 100);
                txtNewGrossSalary.Text = dclGrossSal.ToString();

                txtIncrement.Text = dclTotalPercent.ToString();
                txtNewGrossSalary.ToolTip = Convert.ToString(Convert.ToDecimal(txtNewGrossSalary.Text) - Convert.ToDecimal(lblGrossSal.Text));//Increment Amount
            }
            else
            {
                txtNewGrossSalary.Text = Convert.ToString(Convert.ToDecimal(txtIncrement.Text) + Convert.ToDecimal(lblGrossSal.Text));
                txtIncrement.Text = "0";
                txtNewGrossSalary.ToolTip = Convert.ToString(Convert.ToDecimal(txtNewGrossSalary.Text) - Convert.ToDecimal(lblGrossSal.Text));//Increment Amount
            }
        }
    }

    protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAction.SelectedValue.ToString() == "30")
        {
            txtCOLA.Enabled = true;
            txtGpPer.Enabled = true;
            txtInvPer.Enabled = true;
            txtIncrement.Enabled = false;
            chkIsPercent.Checked = false;
            chkIsPercent.Enabled = false;
        }
        else
        {
            txtCOLA.Enabled = false;
            txtGpPer.Enabled = false;
            txtInvPer.Enabled = false;
            txtCOLA.Text = "";
            txtGpPer.Text = "";
            txtInvPer.Text = ""; 
            txtIncrement.Enabled = true;
            chkIsPercent.Enabled = true;
        }
    }
    protected void cmdCalGross_Click(object sender, EventArgs e)
    {
        this.CalculateGrossSalary();
    }
}
