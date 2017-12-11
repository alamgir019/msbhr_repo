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

public partial class Payroll_Loan_PFLoanRepay : System.Web.UI.Page
{
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    Payroll_LoanAppManager objLoanMgr = new Payroll_LoanAppManager();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();

    dsPayroll_Loan objDSLoan = new dsPayroll_Loan();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.EntryMode(false);
            Common.FillMonthList(ddlMonth);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            txtTransDate.Text = Common.DisplayDate(Common.SetDate(DateTime.Today.ToShortDateString()));
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0, "P"), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);
            TabContainer1.ActiveTabIndex = 0;
        }
    }

    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
        {
            btnSave.Text = "Update";
            hfIsUpdate.Value = "Y";
            ddlMonth.Enabled = false;
            txtTransID.ReadOnly = true;
        }
        else
        {
            btnSave.Text = "Save";
            hfIsUpdate.Value = "N";
            txtTransID.ReadOnly = true;
            txtTransDate.Text = Common.DisplayDate(Common.SetDate(DateTime.Today.ToShortDateString()));
            ddlMonth.Enabled = true;
            txtCode.Text = txtEmpCode.Text.Trim();
            txtTransID.Text = Common.getMaxIdVar("PFLOANADJUSTMENT", "TRANSID", 7);
            txtLoanAmount.Text = "0";
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            txtRemark.Text = "";
        }
    }

    private void FillEmpInfo(string EmpId)
    {

        DataTable dtEmpInfo = objEmpInfoMgr.SelectEmpInfoForLedger(EmpId, "-1");
        DataTable dtLoanSumm = objLoanMgr.getEmpLoanSummary(EmpId);
        Decimal dclNetCU = 0;
        Decimal dclMaxCuWithdraw = 0;
        Decimal dclNetCULoan = 0;
        if (dtEmpInfo.Rows.Count > 0)
        {
            foreach (DataRow row in dtEmpInfo.Rows)
            {
                DataRow nRow = objDSLoan.dtEmpPFLoanMst.NewRow();
                nRow["EMPID"] = EmpId;
                nRow["FULLNAME"] = row["FullName"].ToString().Trim();
                nRow["JOBTITLE"] = row["JobTitle"].ToString().Trim();
                nRow["DIVISIONName"] = row["DIVISIONName"].ToString().Trim();
                nRow["BASICSAL"] = string.IsNullOrEmpty(row["CurrBasicSal"].ToString().Trim()) == true ? "0" : Common.RoundDecimal(row["CurrBasicSal"].ToString().Trim(), 0).ToString();
                if (dtLoanSumm.Rows.Count > 0)
                {
                    //nRow["NETCU"] = string.IsNullOrEmpty(dtLoanSumm.Rows[0]["ColValue"].ToString().Trim()) == true ? "0" : dtLoanSumm.Rows[0]["ColValue"].ToString().Trim();
                    nRow["NETPF"] = string.IsNullOrEmpty(dtLoanSumm.Rows[1]["ColValue"].ToString().Trim()) == true ? "0" : dtLoanSumm.Rows[1]["ColValue"].ToString().Trim();
                    //if (dtLoanSumm.Rows.Count > 2)
                    //    nRow["CULoan"] = string.IsNullOrEmpty(dtLoanSumm.Rows[2]["ColValue"].ToString().Trim()) == true ? "0" : dtLoanSumm.Rows[2]["ColValue"].ToString().Trim();
                    //else
                    //    nRow["CULoan"]="0";
                    if (dtLoanSumm.Rows.Count > 3)
                        nRow["PFLoan"] = string.IsNullOrEmpty(dtLoanSumm.Rows[3]["ColValue"].ToString().Trim()) == true ? "0" : dtLoanSumm.Rows[3]["ColValue"].ToString().Trim();
                    else
                        nRow["PFLoan"]="0";
                    //dclNetCULoan = Common.RoundDecimal(nRow["CULoan"].ToString(), 0);

                    // Net PFCU
                    //nRow["NETPFCU"] = Convert.ToString(Common.RoundDecimal(nRow["NETCU"].ToString().Trim(), 0) + Common.RoundDecimal(nRow["NETPF"].ToString().Trim(), 0));
                    //dclNetCU = Common.RoundDecimal(nRow["NETCU"].ToString().Trim(),0);
                    dclMaxCuWithdraw = dclNetCULoan * 10 / 100;
                    dclMaxCuWithdraw = dclNetCU - dclMaxCuWithdraw;
                    //nRow["MAXCUWITHDRAW"] = dclMaxCuWithdraw.ToString();
                    //nRow["ALLOWPFLOAN"] = Convert.ToString(Common.RoundDecimal(nRow["NETPF"].ToString().Trim(), 0) + dclMaxCuWithdraw);
                    nRow["ALLOWPFLOAN"] = Convert.ToString( Common.RoundDecimal(nRow["PFLoan"].ToString(), 0));
                }
                else
                {
                    //nRow["NETCU"] = "0";
                    nRow["NETPF"] = "0";
                    //nRow["CULoan"] = "0";
                    nRow["PFLoan"] = "0";
                }
                objDSLoan.dtEmpPFLoanMst.Rows.Add(nRow);
                objDSLoan.dtEmpPFLoanMst.AcceptChanges();
            }
            grEmp.DataSource = objDSLoan.Tables["dtEmpPFLoanMst"];
            grEmp.DataBind();

            // Open Loan Records
            grLoan.DataSource = objLoanMgr.GetLoanAdjustmentRecord(EmpId);
            grLoan.DataBind();
            foreach (GridViewRow gRow in grLoan.Rows)
            {
                // Date
                if (Common.CheckNullString(gRow.Cells[2].Text) != "")
                    gRow.Cells[2].Text = Common.DisplayDate(gRow.Cells[2].Text.Trim());
                // Month Name
                if (Common.CheckNullString(gRow.Cells[3].Text) != "")
                    gRow.Cells[3].Text = Common.ReturnFullMonthName(gRow.Cells[3].Text.Trim());
            }

        }
        else
        {
            lblMsg.Text = "Employee code is not valid or not under your office.";
            grEmp.DataSource = null;
            grEmp.DataBind();
            return;
        }
        txtCode.Text = txtEmpCode.Text.Trim();
    }

    
    protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        this.FillEmpInfo(txtEmpCode.Text.Trim());
    }
    protected void grLoan_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                hfIsUpdate.Value = "Y";
                txtTransID.Text = grLoan.SelectedRow.Cells[1].Text.Trim();
                txtTransDate.Text = grLoan.SelectedRow.Cells[2].Text.Trim();
                ddlMonth.SelectedValue = grLoan.DataKeys[_gridView.SelectedIndex].Values[2].ToString().Trim();
                txtLoanAmount.Text = grLoan.SelectedRow.Cells[6].Text.Trim();
                ddlType.SelectedValue = grLoan.SelectedRow.Cells[5].Text.Trim();
                txtRemark.Text = grLoan.SelectedRow.Cells[7].Text.Trim();
                ddlFiscalYear.SelectedValue = grLoan.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim();
                this.EntryMode(true);
                break;
        }
    }

    protected bool ValidateAndSave()
    {
        if (objLoanMgr.IsCurrentMonthLoanExist(txtEmpCode.Text.Trim(), ddlMonth.SelectedValue.Trim(), DateTime.Today.Year.ToString(), "PF") == true)
        {
            lblMsg.Text = "Current month PF loan already exist.Loan cannot be processed.";
            return false;
        }
        if (objLoanMgr.IsCurrentMonthLoanExist(txtEmpCode.Text.Trim(), ddlMonth.SelectedValue.Trim(), DateTime.Today.Year.ToString(), "CU") == true)
        {
            lblMsg.Text = "Current month CU loan already exist.Loan cannot be processed.";
            return false;
        }
        if (grEmp.Rows.Count>0)
        {
            if (Convert.ToInt32(grEmp.Rows[0].Cells[11].Text) < Convert.ToInt32(txtLoanAmount.Text.Trim()))
            {
                lblMsg.Text = "Allowable PF loan exceed.Loan cannot be processed.";
                return false;
            }
        }
        return true;
    }
    private void SaveData()
    {
        string lngID = "";
        try
        {
            //Filling Class Properties with values
                    
            lngID = txtTransID.Text.Trim();

            objLoanMgr.InsertPFLoanAdjustmentData(txtTransID.Text.Trim(), Common.ReturnDate(txtTransDate.Text.Trim()), txtCode.Text.Trim(),
                ddlMonth.SelectedValue.Trim(),DateTime.Today.Year.ToString(),ddlFiscalYear.SelectedValue.Trim(),ddlType.SelectedValue.Trim(),txtLoanAmount.Text.Trim(),
                txtRemark.Text.Trim(),hfIsUpdate.Value,Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));
            if (hfIsUpdate.Value == "N")
                lblMsg.Text = "Record Saved Successfully";
            else
                lblMsg.Text = "Record Updated Successfully";
            this.EntryMode(false);
            this.FillEmpInfo(txtEmpCode.Text.Trim());
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        
            this.SaveData();

    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        this.EntryMode(false);
    }
}
