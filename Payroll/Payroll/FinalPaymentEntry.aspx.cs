using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Payroll_Payroll_FinalPaymentEntry : System.Web.UI.Page
{
    DataTable dtEmpInfo = new DataTable();
    //DataTable dtTempDuty = new DataTable();

    MasterTablesManager objMasMgr = new MasterTablesManager();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
    LeaveManager objLvMgr = new LeaveManager();
    Payroll_MasterMgr objPayMgr = new Payroll_MasterMgr();
    Payroll_PFManager objPFMgr = new Payroll_PFManager();
    Payroll_PayslipApprovalManager objPayAppMgr = new Payroll_PayslipApprovalManager();
    Payroll_FinalPaymentMgr objFinalPayMgr = new Payroll_FinalPaymentMgr();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
            this.EntryMode(false);
        }
    }

    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (string.IsNullOrEmpty(txtEmpID.Text.Trim()) == false)
        {
            this.ClearControls();
            this.FillEmpInfo(txtEmpID.Text.Trim());
            this.OpenRecord();
            // this.EntryMode(false);
        }
    }

    private void FillEmpInfo(string EmpId)
    {

        dtEmpInfo = objEmpInfoMgr.SelectEmpInfoWithAwardLength(txtEmpID.Text.Trim());

        if (dtEmpInfo.Rows.Count > 0)
        {
            foreach (DataRow row in dtEmpInfo.Rows)
            {
                if (string.IsNullOrEmpty(Common.DisplayDate(row["SeparateDate"].ToString().Trim())) == true)
                {
                    lblMsg.Text = "Please seperate this staff first. Then process final payment.";
                    return;
                }
                lblName.Text = row["FullName"].ToString().Trim();
                lblName.ToolTip = row["FullName"].ToString().Trim();
                lblOffice_Loc.Text = row["ClinicName"].ToString().Trim();
                lblDeg_Project.Text = row["DesigName"].ToString().Trim();

                lblJoiningDate.Text = Common.DisplayDate(row["JoiningDate"].ToString().Trim());
                lblSeprateDate.Text = Common.DisplayDate(row["SeparateDate"].ToString().Trim());

                lblSeparateType.Text = row["ActionName"].ToString().Trim();
                lblServiceYr.Text = Math.Round(Convert.ToDecimal(row["ServiceLength"].ToString().Trim()), 2).ToString();

                lblConfirmationDate.Text = Common.DisplayDate(row["ConfirmationDate"].ToString().Trim());
                if (string.IsNullOrEmpty(lblConfirmationDate.Text) == false)
                    lblGratuityYr.Text = this.GratuityYear(Convert.ToDecimal(row["GratuityYr"].ToString().Trim())).ToString();
                else
                    lblGratuityYr.Text = "0";

                txtBasicPay.Text = Math.Round(Convert.ToDecimal(row["BasicSalary"].ToString().Trim())).ToString();
                txtBasicPay.ToolTip = row["GrossSalary"].ToString().Trim();

                txtTotalPay.Text = row["GrossSalary"].ToString().Trim();

                this.CalculateBalance(Convert.ToInt32(row["SalPakId"].ToString()));

            }
        }
        else
        {
            lblMsg.Text = "Employee code is not valid.";
            txtEmpID.Text = "";
            txtEmpID.Text = "";
            lblName.Text = "";
            lblDeg_Project.Text = "";
            lblOffice_Loc.Text = "";

            return;
        }
        lblMsg.Text = "";
    }

    private decimal GratuityYear(decimal dclGratuityYr)
    {
        decimal dclGrYr = 0;
        string[] arInfo = new string[2];
        char[] splitter = { '.' };
        string strGrYr = "";
        arInfo = Common.str_split(Math.Round(dclGratuityYr, 2).ToString(), splitter);

        strGrYr = Math.Round(Convert.ToDecimal(arInfo[1]), 2).ToString();

        if ((Convert.ToDecimal(strGrYr) >= 25) && Convert.ToDecimal(strGrYr) < 50)
            arInfo[1] = "25";
        else if ((Convert.ToDecimal(strGrYr) > 50) && (Convert.ToDecimal(strGrYr) < 75))
            arInfo[1] = "50";
        else if ((Convert.ToDecimal(strGrYr) > 75) && (Convert.ToDecimal(strGrYr) < 100))
            arInfo[1] = "75";
        else
            arInfo[1] = "0";
        strGrYr = arInfo[0] + "." + arInfo[1];

        dclGrYr = Convert.ToDecimal(strGrYr);
        return dclGrYr;
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        this.ClearControls();
        this.EntryMode(false);
    }

    private void CalculateBalance(int iSalPakId)
    {
        //Total Salary
        decimal dclTotalSalary = 0;
        DataTable dtSalPakDtls = objPayMgr.SelectSalaryPakDetls(iSalPakId);

        DataRow[] foundRows = dtSalPakDtls.Select("HeadNature=1 AND ItemCategory='S'");

        if (foundRows.Length > 0)
        {
            foreach (DataRow fRow in foundRows)
            {
                dclTotalSalary = dclTotalSalary + Convert.ToDecimal(fRow["PayAmt"]);
            }
        }
        //txtTotalPay.Text = Math.Round(dclTotalSalary).ToString();

        dclTotalSalary = 0;
        dtSalPakDtls.Rows.Clear();
        dtSalPakDtls.Dispose();

        //EL Balance
        decimal dclELBalance = 0;
        DataTable dtLv = new DataTable();
        dtLv = objLvMgr.SelectEmpLeaveProfileHistory(txtEmpID.Text.Trim(), "2019-01-01", "2019-12-31");
        DataRow[] foundELRows;
        foundELRows = dtLv.Select("LTypeId=2");
        if (foundELRows.Length > 0)
        {
            dclELBalance = (Convert.ToDecimal(foundELRows[0]["LCarryOverd"].ToString()) + Convert.ToDecimal(foundELRows[0]["LEntitled"].ToString())) - Convert.ToDecimal(foundELRows[0]["LeaveEnjoyed"].ToString());
            txtELLeave.Text = dclELBalance.ToString();
            this.LeaveEncashment();
        }

        dtLv.Rows.Clear();
        dtLv.Dispose();

        //Salary of Last Month        
        DateTime dtSeperateDate = Convert.ToDateTime(Common.ReturnDate(lblSeprateDate.Text.Trim()));
        string strMonth = dtSeperateDate.Month.ToString();
        string strYear = dtSeperateDate.Year.ToString();
        string strPrevMonth = Common.GetPreviousMonth(strMonth);
        if (strMonth == "1")
            strYear = Convert.ToString(Convert.ToInt32(strYear) - 1);

        DataTable dtEmpPayroll = objPayAppMgr.GetPayrollApprovedDataForDisbursement("E", txtEmpID.Text.Trim(), strPrevMonth, strYear, "");

        if (dtEmpPayroll.Rows.Count > 0)
            txtLastMonthSalary.Text = "0";
        else
            txtLastMonthSalary.Text = txtBasicPay.ToolTip.ToString();

        //PF Balance      
        DataTable dtPF = objPFMgr.SelectEmpWisePFBF(txtEmpID.Text.Trim());
        if (dtPF.Rows.Count > 0)
            txtPF.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtPF.Rows[0]["TotalPF"]), 0));

        dtPF.Rows.Clear();
        dtPF.Dispose();

        //Gratuity
        txtGratuity.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txtBasicPay.Text) * Convert.ToDecimal(Common.ReturnZeroForNull(lblGratuityYr.Text))));


        lblSalaryMonth.Text = Common.ReturnFullMonthName(strMonth);

        int iSalaryDays = Convert.ToInt32(Common.CalculateTotalDays("01/" + strMonth + "/" + strYear, lblSeprateDate.Text.Trim()));
        lblSalaryDays.Text = Convert.ToString(iSalaryDays - 1);

        txtSeperateMonthSal.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txtTotalPay.Text) / Common.GetMonthDay(Convert.ToInt32(strMonth), strYear) * Convert.ToDecimal(lblSalaryDays.Text), 0));

        dtEmpPayroll.Rows.Clear();
        dtEmpPayroll.Dispose();
    }

    private void LeaveEncashment()
    {
        decimal dclLvEncash=0;
        if (string.IsNullOrEmpty(txtELLeave.Text) == false)
            dclLvEncash = Math.Round(((Convert.ToDecimal(txtBasicPay.ToolTip)) / 30) * Convert.ToDecimal(txtELLeave.Text), 0);
        txtLeaveEncash.Text = dclLvEncash.ToString();
    }
    protected void btnCalculateNet_Click(object sender, EventArgs e)
    {
        decimal dclAddtion = 0, dclDeduction = 0, dclNetPay = 0;
        dclAddtion = Convert.ToDecimal(Common.ReturnZeroForNull(txtLeaveEncash.Text)) + Convert.ToDecimal(Common.ReturnZeroForNull(txtPF.Text))
            + Convert.ToDecimal(Common.ReturnZeroForNull(txtGratuity.Text)) + Convert.ToDecimal(Common.ReturnZeroForNull(txtSeperateMonthSal.Text))
            + Convert.ToDecimal(Common.ReturnZeroForNull(txtLastMonthSalary.Text)) ;

        dclDeduction = Convert.ToDecimal(Common.ReturnZeroForNull(txtTripAdvPay.Text)) + Convert.ToDecimal(Common.ReturnZeroForNull(txtAlreadyPay.Text)) + Convert.ToDecimal(Common.ReturnZeroForNull(txtOtherPay.Text))
           + Convert.ToDecimal(Common.ReturnZeroForNull(txtPFLoan.Text));

        //   dclAddtion= Common.ReturnZeroForNull(txtLeaveEncash.Text) +
        //    Common.ReturnZeroForNull(txtPF.Text) + Common.ReturnZeroForNull(txtGratuity.Text) + Common.ReturnZeroForNull(txtOther.Text));

        //dclDeduction=Convert.ToDecimal(Common.ReturnZeroForNull(txtTripAdvPay.Text) + Common.ReturnZeroForNull(txtAlreadyPay.Text) + 
        //    Common.ReturnZeroForNull(txtOtherPay.Text) + Common.ReturnZeroForNull(txtPFLoan.Text));

        txtTotal.Text = dclAddtion.ToString();
        dclAddtion = dclAddtion + Convert.ToDecimal(Common.ReturnZeroForNull(txtOther.Text));

        dclNetPay = dclAddtion - dclDeduction;
        txtNetPay.Text = dclNetPay.ToString();
        this.LeaveEncashment();
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
            btnSave.Text = "Save";
            hfIsUpdate.Value = "N";
            this.ClearControls();
            lblName.Text = "";
            lblOffice_Loc.Text = "";
            lblDeg_Project.Text = "";
            lblJoiningDate.Text = "";
            lblSeprateDate.Text = "";
            lblSeparateType.Text = "";
            lblServiceYr.Text = "";
            txtELLeave.Text = "";
            lblConfirmationDate.Text = "";
            lblGratuityYr.Text = "";

            grList.DataSource = null;
            grList.DataBind();
        }
    }

    private void OpenRecord()
    {
        DataTable dt = new DataTable();
        // dt= objEmpInfoMgr.SelectEmpInfoHRAction(txtEmpID.Text.Trim());
         dt = objFinalPayMgr.SelectEmpFinalPayment(txtEmpID.Text.Trim());
        grList.DataSource = dt;
        grList.DataBind();
        if (grList.Rows.Count > 0)
        {
            btnSave.Enabled = false;
            foreach (GridViewRow gRow in grList.Rows)
            {
                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[15].Text)) == false)
                    gRow.Cells[15].Text = Common.DisplayDate(gRow.Cells[15].Text);

                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[16].Text)) == false)
                    gRow.Cells[16].Text = Common.DisplayDate(gRow.Cells[16].Text);

                string diff = Common.CalculateYearMonthDay(gRow.Cells[16].Text, 1);
                if (Convert.ToInt32(diff)<6)
                {
                    btnSave.Enabled = true;
                    EntryMode(true);
                    hfId.Value = grList.DataKeys[0].Values[0].ToString();
                }
            }
        }
        else
        {
            btnSave.Enabled = true;
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
    protected bool ValidateAndSave(string Flag)
    {
        try
        {
            if (string.IsNullOrEmpty(txtProcessDate.Text) == true)
            {
                lblMsg.Text = "Please enter process date.";
                txtProcessDate.Focus();
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

    private void ClearControls()
    {       
        txtBasicPay.Text = "";
        txtTotalPay.Text = "";
        txtLeaveEncash.Text = "";
        txtPF.Text = "";
        txtGratuity.Text = "";
        txtLastMonthSalary.Text = "";
        txtSeperateMonthSal.Text = "";
        txtTripAdvPay.Text = "";
        txtAlreadyPay.Text = "";
        txtOtherPay.Text = "";
        txtPFLoan.Text = "";
        txtOther.Text = "";
        txtNetPay.Text = "";
        txtProcessDate.Text = DateTime.Now.ToShortDateString();
        txtRemarks.Text = "";
    }

    private void SaveData(string cmdType)
    {
        dsPayroll objDS = new dsPayroll();
        if (cmdType == "I")
        {
            hfId.Value = Common.getMaxId("FinalPayment", "FinalPayId");
        }
      
        DataTable dtMst = objDS.Tables["FinalPayment"];
        DataRow nRow = dtMst.NewRow();

        nRow["FinalPayId"] = Common.RoundDecimal(hfId.Value, 0);
        nRow["EmpID"] = txtEmpID.Text.Trim();
        nRow["TotServiceYr"] = lblServiceYr.Text.Trim() == "" ? "0" : lblServiceYr.Text.Trim();
        nRow["BasicPay"] = txtBasicPay.Text.Trim() == "" ? "0" : txtBasicPay.Text.Trim();
        nRow["TotalPay"] = txtTotalPay.Text.Trim() == "" ? "0" : txtTotalPay.Text.Trim();
        nRow["ELBalance"] = txtELLeave.Text.Trim() == "" ? "0" : txtELLeave.Text.Trim();
        nRow["LeaveEncash"] = txtLeaveEncash.Text.Trim() == "" ? "0" : txtLeaveEncash.Text.Trim();
        nRow["PF"] = txtPF.Text.Trim() == "" ? "0" : txtPF.Text.Trim();
        nRow["Gratuity"] = txtGratuity.Text.Trim() == "" ? "0" : txtGratuity.Text.Trim();
        nRow["LastMonthSalary"] = txtLastMonthSalary.Text.Trim() == "" ? "0" : txtLastMonthSalary.Text.Trim();
        nRow["SeperateMonthSalary"] = txtSeperateMonthSal.Text.Trim() == "" ? "0" : txtSeperateMonthSal.Text.Trim();
        nRow["TripAdvPay"] = txtTripAdvPay.Text.Trim() == "" ? "0" : txtTripAdvPay.Text.Trim();
        nRow["AlreadyPay"] = txtAlreadyPay.Text.Trim() == "" ? "0" : txtAlreadyPay.Text.Trim();
        nRow["OtherPay"] = txtOtherPay.Text.Trim() == "" ? "0" : txtOtherPay.Text.Trim();
        nRow["PFLoan"] = txtPFLoan.Text.Trim() == "" ? "0" : txtPFLoan.Text.Trim();
        nRow["Other"] = txtOther.Text.Trim() == "" ? "0" : txtOther.Text.Trim();
        nRow["NetPay"] = txtNetPay.Text.Trim() == "" ? "0" : txtNetPay.Text.Trim();
        nRow["SeparateDate"] = Common.ReturnDate(lblSeprateDate.Text.Trim());
        nRow["SeparateDay"] = lblSalaryDays.Text.Trim() == "" ? "0" : lblSalaryDays.Text.Trim();
        nRow["ProcessDate"] = Common.ReturnDate(txtProcessDate.Text.Trim());      
        nRow["Remarks"] = txtRemarks.Text.ToString().Trim();
        nRow["PayStatus"] = "P";
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
        dtMst.Rows.Add(nRow);
        dtMst.AcceptChanges();

       try
        {
            objPayMgr.SaveData(dtMst, cmdType == "D" ? "U" : cmdType);
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (objFinalPayMgr.CheckFinalPaymentStatus(hfId.Value.ToString(), txtEmpID.Text.Trim()) == true)
            {
                lblMsg.Text = "Final payment has already reviewed or approved. Cannot delete this entry.";
                return;
            }
            objFinalPayMgr.DeleteFinalPayment(hfId.Value.ToString(), txtEmpID.Text.Trim(), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), hfIsUpdate.Value, "N");

            lblMsg.Text = "Record Deleted Successfully";
            this.EntryMode(false);
            this.FillEmpInfo(txtEmpID.Text.Trim());
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }
}