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

public partial class Payroll_Loan_MonthlyPFUpdate : System.Web.UI.Page
{
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    Payroll_LoanAppManager objLoanMgr = new Payroll_LoanAppManager();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
    dsPayroll_SalaryReport objDsSal = new dsPayroll_SalaryReport();
    Payroll_PayslipApprovalManager objPayAppMgr = new Payroll_PayslipApprovalManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //this.EntryMode(false);
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0, "P"), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);
        }
    }


    protected DataTable GeneratePayrollReport(string strGenFor, string strGenValue, string strMonth, string strYear, string strBank, string strSalType)
    {
        string strEmpID = "";


        //DataTable dtEmpPayroll = objPayRptMgr.GetSalarySheetDataForCrystalReport(strGenFor, strGenValue,
        //    strMonth, strYear, strBank,
        //    strSalType);
        DataTable dtEmpPayroll = objPayAppMgr.GetPayrollApprovedDataForDisbursement(strGenFor, strGenValue,
            strMonth, strYear, strBank, "1");
        //
        foreach (DataRow dEmpRow in dtEmpPayroll.Rows)
        {
            if (strEmpID == dEmpRow["EMPID"].ToString().Trim())
            {
                continue;
            }
            DataRow nRow = objDsSal.dtSalarySheet.NewRow();
            // Employee Basic
            nRow["EMPID"] = dEmpRow["EMPID"].ToString().Trim();

            // Gross, Net Pay, Total Deduction, Bank Acc
            nRow["GROSS"] = dEmpRow["GROSSAMNT"].ToString().Trim();
            nRow["NETSAL"] = dEmpRow["NETPAY"].ToString().Trim();
            nRow["BANKACC"] = dEmpRow["BANKACCNO"].ToString().Trim();
            nRow["TOTALDED"] = GetEmpBenefitsAndDeductionAmount(dtEmpPayroll, dEmpRow["EMPID"].ToString().Trim());

            // Employee Salary Items
            // Basic    1
            nRow["BASIC"] = this.GetSalHeadAmt(dtEmpPayroll, dEmpRow["EMPID"].ToString().Trim(), "1").ToString();
            // House Rent   2
            nRow["HRENT"] = this.GetSalHeadAmt(dtEmpPayroll, dEmpRow["EMPID"].ToString().Trim(), "2").ToString();

            // Medical  3
            nRow["MEDC"] = this.GetSalHeadAmt(dtEmpPayroll, dEmpRow["EMPID"].ToString().Trim(), "3").ToString();


            // TransPort 4
            nRow["Conveyance"] = this.GetSalHeadAmt(dtEmpPayroll, dEmpRow["EMPID"].ToString().Trim(), "4").ToString();


            // Field 5
            nRow["Others"] = this.GetSalHeadAmt(dtEmpPayroll, dEmpRow["EMPID"].ToString().Trim(), "5").ToString();
            // Field Arrear 6
            nRow["ArrearP"] = this.GetSalHeadAmt(dtEmpPayroll, dEmpRow["EMPID"].ToString().Trim(), "6").ToString();
            // Other Allow 11
            nRow["ArrearN"] = this.GetSalHeadAmt(dtEmpPayroll, dEmpRow["EMPID"].ToString().Trim(), "7").ToString();

            // PF Dedcution 15
            nRow["PF"] = this.GetSalHeadAmt(dtEmpPayroll, dEmpRow["EMPID"].ToString().Trim(), "8").ToString();
            // LWOP 9
            nRow["LWOP"] = this.GetSalHeadAmt(dtEmpPayroll, dEmpRow["EMPID"].ToString().Trim(), "9").ToString();
            //Add Res Allow 10
            nRow["AddResAllow"] = this.GetSalHeadAmt(dtEmpPayroll, dEmpRow["EMPID"].ToString().Trim(), "10").ToString();
            // PF Dedcution Arrear 16
            nRow["PFARR"] = this.GetSalHeadAmt(dtEmpPayroll, dEmpRow["EMPID"].ToString().Trim(), "11").ToString();

            // PF Loan 17
            nRow["PFLOAN"] = this.GetSalHeadAmt(dtEmpPayroll, dEmpRow["EMPID"].ToString().Trim(), "12").ToString();
            // PF Interest 18
            nRow["PFINT"] = this.GetSalHeadAmt(dtEmpPayroll, dEmpRow["EMPID"].ToString().Trim(), "13").ToString();
            // Festival Bonus 14
            nRow["FESTIVAL"] = this.GetSalHeadAmt(dtEmpPayroll, dEmpRow["EMPID"].ToString().Trim(), "16").ToString();
            // Income Tax 24
            nRow["IT"] = this.GetSalHeadAmt(dtEmpPayroll, dEmpRow["EMPID"].ToString().Trim(), "15").ToString();

            // Income Tax Ass 25
            nRow["ITASS"] = this.GetSalHeadAmt(dtEmpPayroll, dEmpRow["EMPID"].ToString().Trim(), "16").ToString();

            objDsSal.dtSalarySheet.Rows.Add(nRow);
            objDsSal.dtSalarySheet.AcceptChanges();
            strEmpID = dEmpRow["EMPID"].ToString().Trim();
        }
        return objDsSal.Tables["dtSalarySheet"];
    }

    protected decimal GetSalHeadAmt(DataTable dt, string strEmpID, string strSHeadID)
    {
        decimal dclSalHeadAmt = 0;
        DataRow[] foundRows = dt.Select("EMPID='" + strEmpID + "' AND SHEADID=" + strSHeadID);
        if (foundRows.Length > 0)
        {
            dclSalHeadAmt = Convert.ToDecimal(foundRows[0]["PAYAMT"].ToString());
        }
        dclSalHeadAmt = Common.RoundDecimal(dclSalHeadAmt.ToString(), 0);
        return dclSalHeadAmt;
    }

    protected decimal GetEmpBenefitsAndDeductionAmount(DataTable dt, string strEmpID)
    {
        //dclTotalSalary = Convert.ToDecimal(strGrossSal);
        decimal dclSalHeadAmt = 0;
        decimal dclEmpDeduct = 0;
        DataRow[] foundRows = dt.Select("EMPID='" + strEmpID + "'");
        foreach (DataRow dRow in foundRows)
        {
            dclSalHeadAmt = 0;
            switch (dRow["ISDEDUCTED"].ToString())
            {
                //case "N":
                //    //dclEmpBenefits = dclEmpBenefits + this.GetSalHeadAmt(strEmpID, dRow["SHEADID"].ToString());
                //    break;
                case "Y":
                    dclSalHeadAmt = Common.RoundDecimal(dRow["PAYAMT"].ToString().Trim(), 0);
                    dclEmpDeduct = dclEmpDeduct + dclSalHeadAmt;
                    break;

            }
        }
        return dclEmpDeduct;
    }
    
    protected void btnUpdatePFLedger_Click(object sender, EventArgs e)
    {
        if (objLoanMgr.IsCurrentMonthLedgerExist(ddlMonth.SelectedValue.Trim(), ddlFiscalYear.SelectedValue.Trim(), "PF", "PF") == false)
        {
            DataTable dtEmpPayroll2 = objLoanMgr.GetDistinctEmployeeForLedger(ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString());
            objLoanMgr.PreparePFLedgerData(dtEmpPayroll2, ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.Trim(), "D",
                Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()), "S", ddlFiscalYear.SelectedValue.ToString());
            lblMsg.Text = ddlMonth.SelectedItem.Text.Trim() + " PF Ledger Prepared Successfully.";
        }
        else
            lblMsg.Text = ddlMonth.SelectedItem.Text.Trim() + " PF Ledger Aleady Prepared.";

    }
    protected void btnUpdatePFLoan_Click(object sender, EventArgs e)
    {
        if (objLoanMgr.IsCurrentMonthLedgerExist(ddlMonth.SelectedValue.Trim(), ddlFiscalYear.SelectedValue.Trim(), "PF", "LOAN") == false)
        {
            DataTable dtEmpPayroll = this.GeneratePayrollReport("A", "", ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim(), "", "S");
            objLoanMgr.InsertPFLoanLedgerData(dtEmpPayroll, ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim(), ddlFiscalYear.SelectedValue.Trim(),
                "PF", Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()));
            lblMsg.Text = ddlMonth.SelectedItem.Text.Trim() +  " PF Loan Ledger Prepared Successfully.";
        }
        else
            lblMsg.Text = ddlMonth.SelectedItem.Text.Trim() +  " PF Loan Ledger Aleady Prepared.";
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objLoanMgr.DeleteLedgerData(ddlMonth.SelectedValue.Trim(), ddlFiscalYear.SelectedValue.Trim(), "PF", "PF");
        lblMsg.Text = ddlMonth.SelectedItem.Text.Trim() + " PF Ledger Deleted Successfully.";
    }
    protected void btnDeleteLoan_Click(object sender, EventArgs e)
    {
        objLoanMgr.DeleteLedgerData(ddlMonth.SelectedValue.Trim(), ddlFiscalYear.SelectedValue.Trim(), "PF", "LOAN");
        lblMsg.Text = ddlMonth.SelectedItem.Text.Trim() + " PF Loan Ledger Deleted Successfully.";
    }
}
