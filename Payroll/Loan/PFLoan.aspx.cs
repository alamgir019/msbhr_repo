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

public partial class Payroll_Loan_PFLoan : System.Web.UI.Page
{
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    Payroll_LoanAppManager objLoanMgr = new Payroll_LoanAppManager();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();

    dsPayroll_Loan objDSLoan = new dsPayroll_Loan();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.EntryMode(false);
            Common.FillMonthList(ddlMonth);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
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
            this.ClearControls();            
        }
    }

    private void ClearControls()
    {
        txtTransID.ReadOnly = true;
        txtTransDate.Text = Common.DisplayDate(Common.SetDate(DateTime.Today.ToShortDateString()));
        txtRecomdBy.Text = "";
        txtPurpose.Text = ""; 
        ddlMonth.Enabled = true;
        txtCode.Text = txtEmpCode.Text.Trim();
        txtTransID.Text = Common.getMaxIdVar("EMPPFLOANMST", "TRANSID", 7);
        txtReqAmount.Text = "0";
        txtLoanAmount.Text = "0";
        txtApproveDate.Text = "";
        ddlInsMonth.SelectedIndex = 0;
        txtInsStartDate.Text = "";
        txtRepay.Text = "0";
        txtRecTk.Text = "0";
        txtRecDate.Text = ""; 
        txtChequeNumber.Text = "";
        txtBankDetail.Text = "Agrani Bank Limited, New Market Branch,Dhaka";
        txtChequeDate.Text = "";
        ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
        txtInterest.Text = "0";
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
                nRow["DIVISIONName"] = row["DivisionName"].ToString().Trim();
                nRow["BASICSAL"] = string.IsNullOrEmpty(row["CurrBasicSal"].ToString().Trim()) == true ? "0" : Common.RoundDecimal(row["CurrBasicSal"].ToString().Trim(), 0).ToString();
                //nRow["SEVERANCEID"] = row["SEVERANCEID"].ToString().Trim();
                //txtPFCode.Text = row["SEVERANCEID"].ToString().Trim();
                if (dtLoanSumm.Rows.Count > 0)
                {                    
                    nRow["NETPF"] = string.IsNullOrEmpty(dtLoanSumm.Rows[1]["ColValue"].ToString().Trim()) == true ? "0" : dtLoanSumm.Rows[1]["ColValue"].ToString().Trim();                    
                    if (dtLoanSumm.Rows.Count > 3)
                        nRow["PFLoan"] = string.IsNullOrEmpty(dtLoanSumm.Rows[3]["ColValue"].ToString().Trim()) == true ? "0" : dtLoanSumm.Rows[3]["ColValue"].ToString().Trim();
                    else
                        nRow["PFLoan"] = "0";                    
                    dclMaxCuWithdraw = dclNetCULoan * 10 / 100;
                    dclMaxCuWithdraw = dclNetCU - dclMaxCuWithdraw;                 
                    nRow["ALLOWPFLOAN"] = Convert.ToString(Common.RoundDecimal(nRow["PFLoan"].ToString(), 0));
                }
                else
                {                    
                    nRow["NETPF"] = "0";                   
                    nRow["PFLoan"] = "0";
                }
                objDSLoan.dtEmpPFLoanMst.Rows.Add(nRow);
                objDSLoan.dtEmpPFLoanMst.AcceptChanges();
            }
            grEmp.DataSource = objDSLoan.Tables["dtEmpPFLoanMst"];
            grEmp.DataBind();

            // Open Loan Records
            grLoan.DataSource = objLoanMgr.GetLoanRecord(EmpId);
            grLoan.DataBind();
            foreach (GridViewRow gRow in grLoan.Rows)
            {
                if (Common.CheckNullString(gRow.Cells[2].Text) != "")
                    gRow.Cells[2].Text = Common.DisplayDate(gRow.Cells[2].Text.Trim());
                if (Common.CheckNullString(gRow.Cells[3].Text) != "")
                    gRow.Cells[3].Text = Common.ReturnFullMonthName(gRow.Cells[3].Text.Trim());                
                if (Common.CheckNullString(gRow.Cells[7].Text) != "")
                    gRow.Cells[7].Text = Common.DisplayDate(gRow.Cells[7].Text.Trim());
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
                txtPFCode.Text = grLoan.DataKeys[_gridView.SelectedIndex].Values[8].ToString().Trim();
                txtTransID.Text = grLoan.SelectedRow.Cells[1].Text.Trim();
                txtTransDate.Text = grLoan.SelectedRow.Cells[2].Text.Trim();
                txtRecomdBy.Text = grLoan.SelectedRow.Cells[11].Text.Trim();
                txtPurpose.Text = grLoan.SelectedRow.Cells[12].Text.Trim();
                ddlMonth.SelectedValue = grLoan.DataKeys[_gridView.SelectedIndex].Values[2].ToString().Trim();
                txtReqAmount.Text = grLoan.DataKeys[_gridView.SelectedIndex].Values[9].ToString().Trim();
                txtLoanAmount.Text = grLoan.SelectedRow.Cells[5].Text.Trim();
                if (string.IsNullOrEmpty(grLoan.DataKeys[_gridView.SelectedIndex].Values[10].ToString().Trim()) == false)
                    txtApproveDate.Text = Common.DisplayDate(grLoan.DataKeys[_gridView.SelectedIndex].Values[10].ToString().Trim());
                ddlInsMonth.Text = grLoan.SelectedRow.Cells[6].Text.Trim();
                if (string.IsNullOrEmpty(grLoan.SelectedRow.Cells[7].Text.Trim()) == false)
                    txtInsStartDate.Text = grLoan.SelectedRow.Cells[7].Text.Trim();
                txtLoanRate.Text = grLoan.SelectedRow.Cells[8].Text.Trim();
                txtRepay.Text = grLoan.SelectedRow.Cells[9].Text.Trim();
                txtInterest.Text = grLoan.SelectedRow.Cells[10].Text.Trim();
                txtRecTk.Text = grLoan.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim();
                if (string.IsNullOrEmpty(grLoan.DataKeys[_gridView.SelectedIndex].Values[12].ToString().Trim()) == false)
                    txtRecDate.Text = Common.DisplayDate(grLoan.DataKeys[_gridView.SelectedIndex].Values[12].ToString().Trim());
                txtChequeNumber.Text = grLoan.DataKeys[_gridView.SelectedIndex].Values[3].ToString().Trim();
                if (string.IsNullOrEmpty(grLoan.DataKeys[_gridView.SelectedIndex].Values[4].ToString().Trim()) == false)
                    txtChequeDate.Text = Common.DisplayDate(grLoan.DataKeys[_gridView.SelectedIndex].Values[4].ToString().Trim());
                txtBankDetail.Text = grLoan.DataKeys[_gridView.SelectedIndex].Values[5].ToString().Trim();
                ddlFiscalYear.SelectedValue = grLoan.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim();
                this.EntryMode(true);
                break;
        }
    }

    protected bool ValidateAndSave(string IsUpdate)
    {
        if (IsUpdate == "N")
        {
            if (objLoanMgr.IsCurrentMonthLoanExist(txtEmpCode.Text.Trim(), ddlMonth.SelectedValue.Trim(), ddlFiscalYear.SelectedValue.Trim(), "PF") == true)
            {
                lblMsg.Text = "Current month PF loan already exist.Loan cannot be processed.";
                return false;
            }
            if (grEmp.Rows.Count > 0)
            {
                ////if (Convert.ToInt32(grEmp.Rows[0].Cells[7].Text) < Convert.ToInt32(txtLoanAmount.Text.Trim()))
                ////{
                ////    lblMsg.Text = "Allowable PF loan exceed.Loan cannot be processed.";
                ////    return false;
                ////}
            }
        }
        return true;
    }

    private void SaveData()
    {
        string strID = "";
        try
        {
            //Filling Class Properties with values
            strID = txtTransID.Text.Trim();
            DataTable dtPFloan = objDSLoan.Tables["EmpPFLoanMst"];
            DataRow dRow = dtPFloan.NewRow();
            dRow["TRANSID"] = strID;
            dRow["TRANSDATE"] = Common.ReturnDate(txtTransDate.Text);
            dRow["EMPID"] = txtEmpCode.Text.Trim();
            //dRow["PFCODE"] = txtPFCode.Text.Trim();
            dRow["RECOMMENDBY"] = txtRecomdBy.Text.Trim();
            dRow["PURPOSE"] = txtPurpose.Text.Trim();
            dRow["LOANMONTH"] = ddlMonth.SelectedValue;
            dRow["FISCALYRID"] = ddlFiscalYear.SelectedValue;
            dRow["REQLOANAMT"] = Convert.ToDecimal(txtReqAmount.Text);
            dRow["LOANAMT"] = Convert.ToDecimal(txtLoanAmount.Text);
            dRow["APPLOANDATE"] = Common.ReturnDate(txtApproveDate.Text);
            dRow["LOANRATE"] = txtLoanRate.Text;
            dRow["INSTALLMENT"] = ddlInsMonth.SelectedValue;
            dRow["INSDATE"] = Common.ReturnDate(txtInsStartDate.Text);
            dRow["LOANRATE"] = txtLoanRate.Text;
            dRow["MONTHLYINTEREST"] = txtInterest.Text;
            dRow["MONTHLYREPAY"] = txtRepay.Text;
            dRow["RECEIVEAMT"] = txtRecTk.Text;
            dRow["RECEIVEDATE"] = Common.ReturnDate(txtRecDate.Text);
            dRow["CHEQUENUMER"] = txtChequeNumber.Text;
            dRow["CHEQUEDATE"] = Common.ReturnDate(txtChequeDate.Text);
            dRow["BANKDETAIL"] = txtChequeDate.Text;

            if (hfIsUpdate.Value == "N")
            {
                dRow["INSERTEDBY"] = Session["USERID"].ToString();
                dRow["INSERTEDDATE"] = DateTime.Now;
            }
            else
            {
                dRow["UPDATEDBY"] = Session["USERID"].ToString();
                dRow["UPDATEDDATE"] = DateTime.Now;
            }

            dtPFloan.Rows.Add(dRow);
            dtPFloan.AcceptChanges();

            objMasMgr.SaveData(dtPFloan, hfIsUpdate.Value == "N" ? "I" : "U");

            lblMsg.Text = Common.GetMessage(hfIsUpdate.Value == "N" ? "I" : "U");
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
        if (ValidateAndSave(hfIsUpdate.Value.Trim()) == true)
            this.SaveData();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        this.EntryMode(false);
    }
}
