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
using System.Xml;
using System.Text;
using System.IO;

public partial class Payroll_Payroll_PayslipPreparation : System.Web.UI.Page
{
    MasterTablesManager objMastMg = new MasterTablesManager();
    Payroll_PreparationManager objPreMgr = new Payroll_PreparationManager();
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();
    DataTable dtEmpInfo;
    DataTable dtEmpSalPackDetls;
    DataTable dtEmpSalPackMst;
    DataTable dtCompFacility;
    DataTable dtOptions = new DataTable();
    DataTable dtBenefits = new DataTable();
    DataTable dtSalHead;
    DataTable dtGrossSalHead = new DataTable();
    dsPayroll_Payslip objPayslip = new dsPayroll_Payslip();

    DataTable dtOverTime = new DataTable();
    DataTable dtMedBenefit = new DataTable();
    DataTable dtHospitalBenefit = new DataTable();
    DataTable dtRemoteAllow = new DataTable();
    DataTable dtChildEduAllow = new DataTable();
    DataTable dtAddResAllow = new DataTable();

    long Leaveweekend;
    long LeaveweekendForAttndBonus;
    long LeaveHoliday;
    string strAttnStartDate = "";
    string strAttnEndDate = "";
    string strPayDurStartDate = "";
    string strPayDurEndDate = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            txtIssueDate.Text = Common.DisplayDate(Common.SetDate(DateTime.Today.ToShortDateString()));
            this.FillGenerateDropDownList();
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0,"F"), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0, "T"), ddlFiscalYearTax, "FISCALYRTITLE", "FISCALYRID", false);
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0, "P"), ddlFiscalYearPF, "FISCALYRTITLE", "FISCALYRID", false);
                        
            Common.FillDropDownList_All(objMastMg.SelectClinic("Y"), ddlCostCenter);
        }
    }

    protected void OpenRecord()
    {
        dtSalHead = new DataTable();
        dtSalHead = objPreMgr.GetSalaryHead();
    }

    protected void FillGenerateDropDownList()
    {      
    }

    #region EarlierCode
    //protected void GeneratePaySlip()
    //{
    //    dtEmpInfo = new DataTable();
    //    string strDateTo = "";
    //    string strIsWithBonus = "N";

    //    string strSalFind = "";
    //    long lngPayID = 0;
    //    long lngPayBookID = 0;
    //    bool boolIgnoreEmployee = false;
    //    bool IsIrregular = false;
    //    decimal dclEmpNetPayAmt = 0;
    //    decimal dclSalHeadAmount = 0;
    //    decimal dclBasicAmount = 0;

    //    string strFiscalYear = "";

    //    if (ddlMonth.SelectedValue.Trim() == "4")
    //        strFiscalYear = Convert.ToString(Convert.ToInt32(ddlFiscalYear.SelectedValue.Trim()) - 1);
    //    else
    //        strFiscalYear = ddlFiscalYear.SelectedValue.Trim();

    //    strDateTo = ddlYear.SelectedValue.ToString() + "/" + ddlMonth.SelectedValue.ToString() + "/" + Common.GetMonthDay(Convert.ToInt32(ddlMonth.SelectedValue.ToString()), ddlYear.SelectedValue.ToString());

    //    // Payroll General Policy for validty
    //    if (objPreMgr.IsPayrollPeriodValid(strDateTo) == false)
    //    {
    //        lblMsg.Text = "Payroll validity period is over. Please renew it.";
    //        return;
    //    }
    //    lngPayBookID = Convert.ToInt64(Common.getMaxId("PaySlipBook", "PSBID"));
    //    lngPayID = objPreMgr.GerMaxPayID(ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim(), "S");
    //    dtEmpInfo = objPreMgr.GetEmployeeData(ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim(),
    //        ddlEmpStatus.SelectedValue.Trim(), ddlEmpType.SelectedValue.Trim(), txtEmpId.Text.Trim(), ddlCostCenter.SelectedValue.ToString().Trim());
    //    dtGrossSalHead = objPayrollMgr.SelectGrossSalHead(0);

    //    dtBenefits = objPreMgr.SelectVaribaleAllowanceData(ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString());

    //    //Overtime
    //    //dtOverTime = objPreMgr.SelectOverTimeData(ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString());

    //    //Medical Benefit
    //    //dtMedBenefit = objPreMgr.SelectMedicalBenefitData(ddlFiscalYearMed.SelectedValue.ToString(), Common.ReturnDate("01/" + ddlMonth.SelectedValue.ToString() + "/" + ddlYear.SelectedValue.ToString()),
    //    //    Common.ReturnDate(Common.GetMonthDay(Convert.ToInt32(ddlMonth.SelectedValue.ToString()), ddlYear.SelectedValue.ToString())
    //    //    + "/" + ddlMonth.SelectedValue.ToString() + "/" + ddlYear.SelectedValue.ToString()));

    //    //Hospital Benefit
    //    //dtHospitalBenefit = objPreMgr.SelectHospitalBenefitData(ddlFiscalYearMed.SelectedValue.ToString(), Common.ReturnDate("01/" + ddlMonth.SelectedValue.ToString() + "/" + ddlYear.SelectedValue.ToString()),
    //    //   Common.ReturnDate(Common.GetMonthDay(Convert.ToInt32(ddlMonth.SelectedValue.ToString()), ddlYear.SelectedValue.ToString())
    //    //   + "/" + ddlMonth.SelectedValue.ToString() + "/" + ddlYear.SelectedValue.ToString()));

    //    //Additional Responsibility
    //    //dtAddResAllow = objPreMgr.SelectAdditionalResponseData(Common.ReturnDate("01/" + ddlMonth.SelectedValue.ToString() + "/" + ddlYear.SelectedValue.ToString()),
    //    //   Common.ReturnDate(Common.GetMonthDay(Convert.ToInt32(ddlMonth.SelectedValue.ToString()), ddlYear.SelectedValue.ToString())
    //    //   + "/" + ddlMonth.SelectedValue.ToString() + "/" + ddlYear.SelectedValue.ToString()));
    //    DataTable dtFestivalBonus = objPreMgr.GetEmployeeBonusData(ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim());
    //    DataRow[] foundBfRow;
    //    //DataRow[] foundOTRow;
    //    //DataRow[] foundMedicalRow;
    //    //DataRow[] foundHospitalRow;
    //    DataRow[] foundFBRow;
    //    //DataRow[] foundAddResponeRow;
    //     //PF
    //    DataTable dtPFLoanLedger = objPreMgr.GetPFLoanLedgerForPayrollPreparation(Common.GetPreviousMonth(ddlMonth.SelectedValue.ToString().Trim()), ddlFiscalYearPF.SelectedValue.ToString()   );
    //    DataTable dtPFLoan = objPreMgr.GetPFLoanDataForPayrollPreparation(ddlMonth.SelectedValue.ToString().Trim(), ddlFiscalYearPF.SelectedValue.ToString());
    //    DataTable dtPFLoanRepay = objPreMgr.GetPFLoanAdjustmentForPayrollPreparation(ddlMonth.SelectedValue.ToString().Trim(), ddlFiscalYearPF.SelectedValue.Trim());


    //    DataRow[] foundPFLLRow;
    //    DataRow[] foundPFLRRow;
    //    DataRow[] foundPFLoanRow;

    //    decimal dclCLLAmount = 0;
    //    decimal dclCashPay = 0;
    //    decimal dclRepay = 0;

    //    ////Remote Allowance
    //    //dtRemoteAllow = objPreMgr.SelectRemoteAllowanceData(Common.ReturnDate("01/" + ddlMonth.SelectedValue.ToString() + "/" + ddlYear.SelectedValue.ToString()),
    //    //    Common.ReturnDate(Common.GetMonthDay(Convert.ToInt32(ddlMonth.SelectedValue.ToString()), ddlYear.SelectedValue.ToString())
    //    //    + "/" + ddlMonth.SelectedValue.ToString() + "/" + ddlYear.SelectedValue.ToString()));
    //    //DataRow[] foundReAllowRow;

    //    //Child Education Allowance
    //    //dtChildEduAllow = objPreMgr.SelectChildEduAllowanceData(ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString());
    //    //DataRow[] foundChildEduAllowRow;

    //    if (dtEmpInfo.Rows.Count == 0)
    //    {
    //        lblMsg.Text = "No Record Found...";
    //        return;
    //    }        

    //    int inMonthDays = 0;
    //    TimeSpan tsMD;
    //    DateTime PayStartDate = new DateTime();
    //    DateTime PayEndDate = new DateTime();

    //    foreach (DataRow dEmpRow in dtEmpInfo.Rows)
    //    {
    //        boolIgnoreEmployee = false;
    //        bool IsPfLoanEmpty = true;
    //        inMonthDays = 0;

    //        PayStartDate = Convert.ToDateTime(dEmpRow["PAYSTARTDATE"].ToString().Trim());
    //        PayEndDate = Convert.ToDateTime(dEmpRow["PAYENDDATE"].ToString().Trim());
    //        if (dEmpRow["EmpTypeID"].ToString().Trim() != "2")
    //        {                
    //            tsMD = PayEndDate - PayStartDate;
    //            inMonthDays = tsMD.Days +1;
    //        }
    //        else
    //        {
    //            inMonthDays = Convert.ToInt16(dEmpRow["DAYSDUR"].ToString().Trim());
    //        }

    //        // Get the Last Payroll Disbursement date of the employee
    //        strSalFind = objPreMgr.GetEmployeeLastSalaryDisbursementDate(dEmpRow["EMPID"].ToString().Trim());
    //        //STEP 1
    //        if (string.IsNullOrEmpty(strSalFind) == false)
    //        {
    //            DateTime DatePAYDUREND = new DateTime();
    //            DatePAYDUREND = Convert.ToDateTime(strSalFind);
    //            if (DatePAYDUREND.AddDays(1) != PayStartDate)
    //            {
    //                if (DatePAYDUREND.AddDays(1) <= PayEndDate)
    //                    lblMsg.Text = "The last date of payment for the Employee is " + Common.DisplayDate(strSalFind);
    //                else
    //                    boolIgnoreEmployee = true;
    //            }
    //        }
    //        else
    //        {
    //            boolIgnoreEmployee = false;
    //        }
    //        //End Step 1

    //        if (boolIgnoreEmployee == false)
    //        {
    //            if (string.IsNullOrEmpty(dEmpRow["SalPakId"].ToString()) == false)
    //            {
    //                dtEmpSalPackDetls = new DataTable();
    //                dtEmpSalPackDetls = objPreMgr.GetEmpSalaryPackDetails(dEmpRow["EMPID"].ToString().Trim());
    //                //PF Loan & Interest
    //                foundPFLLRow = dtPFLoanLedger.Select("EMPID='" + dEmpRow["EMPID"].ToString().Trim() + "'");
    //                foundPFLRRow = dtPFLoanRepay.Select("EMPID='" + dEmpRow["EMPID"].ToString().Trim() + "'");
    //                foundPFLoanRow = dtPFLoan.Select("EMPID='" + dEmpRow["EMPID"].ToString().Trim() + "'");

    //                dclEmpNetPayAmt = 0;

    //                foreach (DataRow dSalPackRow in dtEmpSalPackDetls.Rows)
    //                {
    //                    IsIrregular = false;

    //                    if ((dSalPackRow["PAYTYPE"].ToString() == "3") && inMonthDays != Convert.ToInt32(dEmpRow["SALDUR"].ToString().Trim()))
    //                    {
    //                        dclSalHeadAmount = Convert.ToDecimal(dSalPackRow["TOTAMNT"].ToString());
    //                        IsIrregular = true;
    //                    }
    //                    else
    //                    {
    //                        dclSalHeadAmount = Convert.ToDecimal(dSalPackRow["TOTAMNT"].ToString());
    //                    }

    //                    // Get Benefits data
    //                    foundBfRow = dtBenefits.Select("EMPID='" + dEmpRow["EMPID"].ToString().Trim() + "' AND SHEADID='" + dSalPackRow["SHEADID"].ToString().Trim() + "'");
    //                    if (foundBfRow.Length > 0)
    //                    {
    //                        dclSalHeadAmount = Convert.ToDecimal(foundBfRow[0]["PAYAMT"].ToString());
    //                    }
                       

    //                    // Get OT data
    //                    //if (dSalPackRow["SHEADID"].ToString().Trim() == "8")
    //                    //{
    //                    //    foundOTRow = dtOverTime.Select("EMPID='" + dEmpRow["EMPID"].ToString().Trim() + "'");
    //                    //    if (foundOTRow.Length > 0)
    //                    //    {
    //                    //        dclSalHeadAmount = Convert.ToDecimal(foundOTRow[0]["OTAmount"].ToString());
    //                    //    }
    //                    //}
    //                    //// Get Additional Responsibility data
    //                    //if (dSalPackRow["SHEADID"].ToString().Trim() == "10")
    //                    //{
    //                    //    foundAddResponeRow = dtAddResAllow.Select("EMPID='" + dEmpRow["EMPID"].ToString().Trim() + "'");
    //                    //    if (foundAddResponeRow.Length > 0)
    //                    //    {
    //                    //        dclSalHeadAmount = Convert.ToDecimal(foundAddResponeRow[0]["Amount"].ToString());
    //                    //    }
    //                    //}
    //                    // Get Remote Allowance data
    //                    //if (dSalPackRow["SHEADID"].ToString().Trim() == "11")
    //                    //{
    //                    //    foundReAllowRow = dtRemoteAllow.Select("EMPID='" + dEmpRow["EMPID"].ToString().Trim() + "'");
    //                    //    if (foundReAllowRow.Length > 0)
    //                    //    {
    //                    //        dclSalHeadAmount = Convert.ToDecimal(foundReAllowRow[0]["Amount"].ToString());
    //                    //    }
    //                    //}                       
    //                    // Get Festival Bonus Data
    //                    foundFBRow = dtFestivalBonus.Select("EMPID='" + dEmpRow["EMPID"].ToString().Trim() + "' AND SHEADID='" + dSalPackRow["SHEADID"].ToString().Trim() + "'");
    //                    if (foundFBRow.Length > 0)
    //                    {
    //                        dclSalHeadAmount = Convert.ToDecimal(foundFBRow[0]["TOTALSALARY"].ToString());
    //                        strIsWithBonus = "Y";
    //                    }                  
    //                
    //                    // Check for Provident Fund
    //                    if (dSalPackRow["ISPFUND"].ToString() == "Y")
    //                    {
    //                        this.AddSalPackDets(lngPayBookID.ToString(), dEmpRow["EMPID"].ToString().Trim(), dSalPackRow["SHEADID"].ToString(),
    //                                            dSalPackRow["HEADNAME"].ToString(), dSalPackRow["ISBASICSAL"].ToString(), dclSalHeadAmount.ToString(),
    //                                            "Y", "N", "N", "N", "Y", dSalPackRow["AMTCOMPAY"].ToString(),"N", "N", "N", "0", "N", "N", "N");
    //                    }
    //                    else
    //                    {
    //                        this.AddSalPackDets(lngPayBookID.ToString(), dEmpRow["EMPID"].ToString().Trim(), dSalPackRow["SHEADID"].ToString(),
    //                                            dSalPackRow["HEADNAME"].ToString(), dSalPackRow["ISBASICSAL"].ToString(), dclSalHeadAmount.ToString(),
    //                                            dSalPackRow["HEADNATURE"].ToString() == "1" ? "N" : "Y", "N", "N", "N", "N", "0.00","N", "N", "N", "0", "N", "N", "N");
    //                    }

    //                    if (dSalPackRow["ISBASICSAL"].ToString() == "Y")
    //                        dclBasicAmount = dclSalHeadAmount;

    //                    // Calculate Net Pay Amt
    //                    dclEmpNetPayAmt = dclEmpNetPayAmt + dclSalHeadAmount;

    //                }// Loop End Here  

    //                // Insert Data into Salary Pack master Table of Dataset
    //                this.AddSalPackMst(lngPayBookID.ToString(), lngPayID.ToString(), dEmpRow["EMPID"].ToString().Trim(), dEmpRow["FULLNAME"].ToString().Trim(),
    //                    dEmpRow["JobTitleName"].ToString().Trim(), Common.SetDate(PayStartDate.ToShortDateString()), Common.SetDate(PayEndDate.ToShortDateString()), "P", dEmpRow["SalPakId"].ToString().Trim(), dEmpRow["PAYTYPE"].ToString(),
    //                    objPayslip.dtPaySlipDets, "N", dclEmpNetPayAmt.ToString(), dEmpRow["SALDUR"].ToString().Trim(), dEmpRow["DEPTNAME"].ToString(), dEmpRow["LATECOUNT"].ToString(),
    //                    dEmpRow["LATESALCOUNT"].ToString(), dEmpRow["LATESALHEAD"].ToString(), strIsWithBonus, "N", dEmpRow["TOTALGROSSSAL"].ToString(),
    //                    IsIrregular == false ? "N" : "Y", dEmpRow["OTAmt"].ToString(), dEmpRow["IsInPercent"].ToString(), dEmpRow["SalHeadID"].ToString(),
    //                    "N", dEmpRow["BONUSPAKID"].ToString(), dEmpRow["WillConvert"].ToString(), dEmpRow["CurrId"].ToString(),
    //                    dEmpRow["ConvrsAmt"].ToString(), dEmpRow["ArearAmnt"].ToString(), dEmpRow["IsArearPaid"].ToString(), dclBasicAmount.ToString(),
    //                    "0", "0.00", dEmpRow["SPTitle"].ToString().Trim(), PayStartDate.ToShortDateString(), PayEndDate.ToShortDateString(),
    //                    inMonthDays.ToString(),dEmpRow["BANKCODE"].ToString().Trim(), dEmpRow["RoutingNo"].ToString().Trim(),
    //                    dEmpRow["BankAccNo"].ToString().Trim(), dEmpRow["DEPTID"].ToString().Trim(), dEmpRow["DesigId"].ToString().Trim(),
    //                    dEmpRow["EMPTYPEID"].ToString().Trim(), dEmpRow["PLANACCLINE"].ToString().Trim(), dEmpRow["JoiningDate"].ToString().Trim(), dEmpRow["SeparateDate"].ToString().Trim(),
    //                    ddlEmpType.SelectedValue.Trim(), dEmpRow["ClinicId"].ToString().Trim(), dEmpRow["ProbationPeriod"].ToString().Trim(), dEmpRow["ConfirmationDate"].ToString().Trim());

    //                lngPayID = lngPayID + 1;
                   
    //                foundPFLLRow = null;
    //                foundPFLRRow = null;
    //                foundPFLoanRow = null;
    //                foundBfRow = null;
    //                //foundMedicalRow = null;
    //                //foundHospitalRow = null;
    //                //foundChildEduAllowRow = null;
    //                //foundOTRow = null;
    //                //foundAddResponeRow = null;
    //                //foundReAllowRow = null;
    //                foundFBRow = null;
    //            }
    //        }
    //    }

    //    foreach (DataRow dRowSalPackMst in objPayslip.dtPaySlipMst.Rows)
    //    {
    //        DataTable dtBlockHead = objMastMg.SelectSHeadBlockData(Common.ReturnDate(txtIssueDate.Text.Trim()));
    //        DataTable dtBlockPF = objMastMg.SelectProbationalEmployee(Common.ReturnDate(txtIssueDate.Text.Trim()));

    //        if (dRowSalPackMst["SalPayType"].ToString().Trim() == "3")
    //        {
    //            if (dRowSalPackMst["EMPTYPEID"].ToString().Trim() != "2")
    //            {
    //                if (dRowSalPackMst["IsIrregular"].ToString().Trim() == "N")
    //                {
    //                    this.FillMonthlyRegularPayment(dRowSalPackMst);
    //                    this.ValidateBlockSalaryHead(dRowSalPackMst, dtBlockHead, dtBlockPF);
    //                }
    //                else
    //                {
    //                    this.FillMonthlyNotRegularPayment(dRowSalPackMst);
    //                    this.ValidateBlockSalaryHead(dRowSalPackMst, dtBlockHead, dtBlockPF);
    //                }
    //            }
    //            else
    //            {
    //                this.FillMonthlyContractualPayment(dRowSalPackMst);
    //                this.ValidateBlockSalaryHead(dRowSalPackMst, dtBlockHead, dtBlockPF);
    //            }
    //        }

    //        if (string.IsNullOrEmpty(dRowSalPackMst["ConfirmationDate"].ToString()) == false)
    //        {
    //            DateTime dtConfirmDate = Convert.ToDateTime(dRowSalPackMst["ConfirmationDate"].ToString().Trim());
    //            if ((dtConfirmDate > PayStartDate) && (dtConfirmDate <= PayEndDate))
    //            {
    //                this.FillMonthlyNotRegularPaymentForPF(dRowSalPackMst);
    //            }
    //        }
    //    }
    //}
    #endregion
    protected void GeneratePaySlip()
    {
        dtEmpInfo = new DataTable();
        string strDateTo = "";
        string strIsWithBonus = "N";

        string strSalFind = "";
        long lngPayID = 0;
        long lngPayBookID = 0;
        bool boolIgnoreEmployee = false;
        bool IsIrregular = false;
        decimal dclEmpNetPayAmt = 0;
        decimal dclSalHeadAmount = 0;
        decimal dclBasicAmount = 0;

        string strFiscalYear = "";

        if (ddlMonth.SelectedValue.Trim() == "4")
            strFiscalYear = Convert.ToString(Convert.ToInt32(ddlFiscalYear.SelectedValue.Trim()) - 1);
        else
            strFiscalYear = ddlFiscalYear.SelectedValue.Trim();

        strDateTo = ddlYear.SelectedValue.ToString() + "/" + ddlMonth.SelectedValue.ToString() + "/" + Common.GetMonthDay(Convert.ToInt32(ddlMonth.SelectedValue.ToString()), ddlYear.SelectedValue.ToString());

        // Payroll General Policy for validty
        if (objPreMgr.IsPayrollPeriodValid(strDateTo) == false)
        {
            lblMsg.Text = "Payroll validity period is over. Please renew it.";
            return;
        }
        lngPayBookID = Convert.ToInt64(Common.getMaxId("PaySlipBook", "PSBID"));
        lngPayID = objPreMgr.GerMaxPayID(ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim(), "S");
        dtEmpInfo = objPreMgr.GetEmployeeData(ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim(),
            ddlEmpStatus.SelectedValue.Trim(), txtEmpId.Text.Trim(), ddlCostCenter.SelectedValue.ToString().Trim());
        dtGrossSalHead = objPayrollMgr.SelectGrossSalHead(0);

        dtBenefits = objPreMgr.SelectVaribaleAllowanceData(ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString());

      
        //Additional Responsibility
        dtAddResAllow = objPreMgr.SelectAdditionalResponseData(Common.ReturnDate("01/" + ddlMonth.SelectedValue.ToString() + "/" + ddlYear.SelectedValue.ToString()),
           Common.ReturnDate(Common.GetMonthDay(Convert.ToInt32(ddlMonth.SelectedValue.ToString()), ddlYear.SelectedValue.ToString())
           + "/" + ddlMonth.SelectedValue.ToString() + "/" + ddlYear.SelectedValue.ToString()));
        DataTable dtFestivalBonus = objPreMgr.GetEmployeeBonusData(ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim());

        DataRow[] foundBfRow;
        //DataRow[] foundOTRow;
        DataRow[] foundFBRow;
        DataRow[] foundAddResponeRow;
        //PF
        //DataTable dtPFLoanLedger = objPreMgr.GetPFLoanLedgerForPayrollPreparation(Common.GetPreviousMonth(ddlMonth.SelectedValue.ToString().Trim()), ddlFiscalYearPF.SelectedValue.ToString());
        DataTable dtPFLoanLedger = new DataTable();
        if (ddlMonth.SelectedValue != "1")
            dtPFLoanLedger = objPreMgr.GetPFLoanLedgerForPayrollPreparation(Common.GetPreviousMonth(ddlMonth.SelectedValue.ToString().Trim()), ddlFiscalYearPF.SelectedValue.ToString());
        else
            dtPFLoanLedger = objPreMgr.GetPFLoanLedgerForPayrollPreparation(Common.GetPreviousMonth(ddlMonth.SelectedValue.ToString().Trim()), objPayrollMgr.GetPrevPFFisYr(ddlFiscalYearPF.SelectedValue.ToString() ));
        DataTable dtPFLoan = objPreMgr.GetPFLoanDataForPayrollPreparation(ddlMonth.SelectedValue.ToString().Trim(), ddlFiscalYearPF.SelectedValue.ToString());
        DataTable dtPFLoanRepay = objPreMgr.GetPFLoanAdjustmentForPayrollPreparation(ddlMonth.SelectedValue.ToString().Trim(), ddlFiscalYearPF.SelectedValue.Trim());

        // Arrear
        DataTable dtArrear = objPreMgr.GetPayrollArrearForPreparation(ddlMonth.SelectedValue.Trim(), ddlFiscalYear.SelectedValue.Trim());

        DataRow[] foundPFLLRow;
        DataRow[] foundPFLRRow;
        DataRow[] foundPFLoanRow;
        DataRow[] foundArrRows;

        decimal dclCLLAmount = 0;
        decimal dclCashPay = 0;
        decimal dclRepay = 0;

        if (dtEmpInfo.Rows.Count == 0)
        {
            lblMsg.Text = "No Record Found...";
            return;
        }

        int inMonthDays = 0;
        TimeSpan tsMD;
        DateTime PayStartDate = new DateTime();
        DateTime PayEndDate = new DateTime();

        foreach (DataRow dEmpRow in dtEmpInfo.Rows)
        {
            boolIgnoreEmployee = false;
            bool IsPfLoanEmpty = true;
            inMonthDays = 0;

            PayStartDate = Convert.ToDateTime(dEmpRow["PAYSTARTDATE"].ToString().Trim());
            PayEndDate = Convert.ToDateTime(dEmpRow["PAYENDDATE"].ToString().Trim());
            ////if (dEmpRow["EmpTypeID"].ToString().Trim() != "2")
            ////{
                tsMD = PayEndDate - PayStartDate;
                inMonthDays = tsMD.Days + 1;
            ////}
            ////else
            ////{
            ////    inMonthDays = Convert.ToInt16(dEmpRow["DAYSDUR"].ToString().Trim());
            ////}

            // Get the Last Payroll Disbursement date of the employee
            strSalFind = objPreMgr.GetEmployeeLastSalaryDisbursementDate(dEmpRow["EMPID"].ToString().Trim());
            //STEP 1
            if (string.IsNullOrEmpty(strSalFind) == false)
            {
                DateTime DatePAYDUREND = new DateTime();
                DatePAYDUREND = Convert.ToDateTime(strSalFind);
                if (DatePAYDUREND.AddDays(1) != PayStartDate)
                {
                    if (DatePAYDUREND.AddDays(1) <= PayEndDate)
                        lblMsg.Text = "The last date of payment for the Employee is " + Common.DisplayDate(strSalFind);
                    else
                        boolIgnoreEmployee = true;
                }
            }
            else
            {
                boolIgnoreEmployee = false;
            }
            //End Step 1

            if (boolIgnoreEmployee == false)
            {
                if (string.IsNullOrEmpty(dEmpRow["SalPakId"].ToString()) == false)
                {
                    dtEmpSalPackDetls = new DataTable();
                    dtEmpSalPackDetls = objPreMgr.GetEmpSalaryPackDetails(dEmpRow["EMPID"].ToString().Trim());
                    //PF Loan & Interest
                    foundPFLLRow = dtPFLoanLedger.Select("EMPID='" + dEmpRow["EMPID"].ToString().Trim() + "'");
                    foundPFLRRow = dtPFLoanRepay.Select("EMPID='" + dEmpRow["EMPID"].ToString().Trim() + "'");
                    foundPFLoanRow = dtPFLoan.Select("EMPID='" + dEmpRow["EMPID"].ToString().Trim() + "'");

                    foundPFLLRow = dtPFLoanLedger.Select("EMPID='" + dEmpRow["EMPID"].ToString().Trim() + "'");

                    foundArrRows = dtArrear.Select("EMPID='" + dEmpRow["EMPID"].ToString().Trim() + "'");

                    dclEmpNetPayAmt = 0;

                    foreach (DataRow dSalPackRow in dtEmpSalPackDetls.Rows)
                    {
                        IsIrregular = false;

                        if ((dSalPackRow["PAYTYPE"].ToString() == "3") && inMonthDays != Convert.ToInt32(dEmpRow["SALDUR"].ToString().Trim()))
                            IsIrregular = true;
                        
                         dclSalHeadAmount = 0;
                        foundBfRow = dtBenefits.Select("EMPID='" + dEmpRow["EMPID"].ToString().Trim() + "' AND SHEADID='" + dSalPackRow["SHEADID"].ToString().Trim() + "'");
                        switch (dSalPackRow["SHEADID"].ToString())
                         {
                             case "1":// Basic
                                dclSalHeadAmount = Common.RoundDecimal(dSalPackRow["TOTAMNT"].ToString(), 0);
                                if (foundArrRows.Length > 0)
                                    dclSalHeadAmount = this.GetArrearHeadAmount(dtArrear, 16, dEmpRow["EMPID"].ToString().Trim());                               
                                break;
                            case "2":// House Rent
                                dclSalHeadAmount = Common.RoundDecimal(dSalPackRow["TOTAMNT"].ToString(), 0);
                                if (foundArrRows.Length > 0)
                                    dclSalHeadAmount = this.GetArrearHeadAmount(dtArrear, 17, dEmpRow["EMPID"].ToString().Trim());
                                break;
                            case "3":// Medical
                                dclSalHeadAmount = Common.RoundDecimal(dSalPackRow["TOTAMNT"].ToString(), 0);
                                if (foundArrRows.Length > 0)
                                    dclSalHeadAmount = this.GetArrearHeadAmount(dtArrear, 18, dEmpRow["EMPID"].ToString().Trim());
                                break;
                            case "4":// Convenyance
                             case "5":// Others Allowance

                                dclSalHeadAmount = Common.RoundDecimal(dSalPackRow["TOTAMNT"].ToString(), 0);
                                 break;
                             case "6":// Arrear +
                             case "7":// Arrear -
                                 //Get Benefits data
                                 
                                 if (foundBfRow.Length > 0)
                                 {
                                     dclSalHeadAmount = Convert.ToDecimal(foundBfRow[0]["PAYAMT"].ToString());
                                 }
                                 break;
                             case "8":// PF                                 
                                     dclSalHeadAmount = Common.RoundDecimal(dSalPackRow["TOTAMNT"].ToString(), 0);
                                if (foundArrRows.Length > 0)
                                    dclSalHeadAmount = this.GetArrearHeadAmount(dtArrear, 11, dEmpRow["EMPID"].ToString().Trim());
                                break;
                             case "9"://LWOP
                                      //dclSalHeadAmount = Common.RoundDecimal(dSalPackRow["TOTAMNT"].ToString(), 0);
                                if (foundBfRow.Length > 0)
                                {
                                    dclSalHeadAmount = Convert.ToDecimal(foundBfRow[0]["PAYAMT"].ToString());
                                }
                                break;
                             case "10"://Additional Responsibility                                 
                                 foundAddResponeRow = dtAddResAllow.Select("EMPID='" + dEmpRow["EMPID"].ToString().Trim() + "'");
                                 if (foundAddResponeRow.Length > 0)
                                 {
                                     dclSalHeadAmount = Convert.ToDecimal(foundAddResponeRow[0]["Amount"].ToString());
                                 }

                                 break;
                             case "11"://PF Arr
                                 foundBfRow = dtBenefits.Select("EMPID='" + dEmpRow["EMPID"].ToString().Trim() + "' AND SHEADID='" + dSalPackRow["SHEADID"].ToString().Trim() + "'");
                                 if (foundBfRow.Length > 0)
                                 {
                                     dclSalHeadAmount = Convert.ToDecimal(foundBfRow[0]["PAYAMT"].ToString());
                                 }
                                 break;
                             #region PF Loan
                             case "12":// PF Loan
                                 dclCLLAmount = 0;
                                 dclCashPay = 0;
                                 if (foundPFLLRow.Length > 0)                                 
                                     dclCLLAmount = Common.RoundDecimal(foundPFLLRow[0]["CLLOAN"].ToString().Trim(), 0);

                                 if (foundPFLRRow.Length > 0)
                                 {
                                     foreach (DataRow dCRow in foundPFLRRow)
                                     {
                                         if (dCRow["ADJTYPE"].ToString().Trim() == "Deduction")
                                         {
                                             dclRepay = Common.RoundDecimal(dCRow["ADJAMOUNT"].ToString().Trim(), 0);
                                         }
                                         else if (dCRow["ADJTYPE"].ToString().Trim() == "Cash Pay")
                                         {
                                             dclCashPay = Common.RoundDecimal(dCRow["ADJAMOUNT"].ToString().Trim(), 0);
                                         }
                                     }
                                     if (dclRepay == 0)
                                     {
                                         if (foundPFLoanRow.Length > 0)
                                         {
                                             dclSalHeadAmount = Common.RoundDecimal(foundPFLoanRow[0]["MonthlyRepay"].ToString().Trim(), 0);
                                         }
                                         else if (foundPFLLRow.Length > 0)
                                         {
                                             dclSalHeadAmount = Common.RoundDecimal(foundPFLLRow[0]["CMREPAY"].ToString().Trim(), 0);
                                         }
                                         else
                                         {
                                             dclSalHeadAmount = 0;
                                         }
                                     }
                                     else
                                         dclSalHeadAmount = dclRepay;
                                 }
                                 else
                                 {
                                     if (foundPFLoanRow.Length > 0)                                     
                                         dclSalHeadAmount = Common.RoundDecimal(foundPFLoanRow[0]["MonthlyRepay"].ToString().Trim(), 0);
                                     
                                     else if (foundPFLLRow.Length > 0)                                     
                                         dclSalHeadAmount = Common.RoundDecimal(foundPFLLRow[0]["CMREPAY"].ToString().Trim(), 0);
                                     
                                     else                                     
                                         dclSalHeadAmount = 0;                                     
                                 }
                                // Validate with Closing Amount;
                                if (dclCLLAmount == 0 && foundPFLLRow.Length > 0)
                                {
                                    dclSalHeadAmount = Common.RoundDecimal(foundPFLLRow[0]["CLLOAN"].ToString().Trim(), 0);
                                    ////dclSalHeadAmount = 0;
                                }
                                if (dclCLLAmount > 0)
                                 {
                                     if (dclCLLAmount < dclSalHeadAmount)
                                         dclSalHeadAmount = dclCLLAmount;
                                 }
                                 if (dclCashPay > 0)
                                 {
                                     if (dclCLLAmount<= dclCashPay)
                                         dclSalHeadAmount = 0;
                                 }
                                 dclSalHeadAmount = dclSalHeadAmount * -1;
                                 break;
                             case "13":// PF Loan Interest
                                 dclCLLAmount = 0;
                                 dclCashPay = 0;
                                 if (foundPFLLRow.Length > 0)
                                 {
                                     ////dclCLLAmount = Common.RoundDecimal(foundPFLLRow[0]["CLLOAN"].ToString().Trim(), 0);
                                     if (Common.RoundDecimal(foundPFLLRow[0]["CLLOAN"].ToString().Trim(), 0) > 0)
                                         dclCLLAmount = Common.RoundDecimal(foundPFLLRow[0]["CMINTEREST"].ToString().Trim(), 0);

                                    dclSalHeadAmount = dclSalHeadAmount * -1;
                                    dclSalHeadAmount = decimal.Round(dclSalHeadAmount, 0);
                                }
                                if (foundPFLRRow.Length > 0)
                                {
                                    foreach (DataRow dCRow in foundPFLRRow)
                                    {
                                        if (dCRow["ADJTYPE"].ToString().Trim() == "Deduction")
                                        {
                                            dclRepay = Common.RoundDecimal(dCRow["INTDUE"].ToString().Trim(), 0);
                                        }
                                        if (dCRow["ADJTYPE"].ToString().Trim() == "Cash Pay")
                                        {
                                            //dclCashPay = Common.RoundDecimal(dCRow["INTDUE"].ToString().Trim(), 0);
                                            dclRepay = Common.RoundDecimal(dCRow["INTDUE"].ToString().Trim(), 0);
                                        }
                                    }
                                    if (dclCLLAmount <= dclRepay)
                                        dclSalHeadAmount = 0;
                                    else
                                        dclSalHeadAmount = dclRepay * -1;
                                }
                                else
                                {
                                    if (foundPFLoanRow.Length > 0)
                                        dclSalHeadAmount = Common.RoundDecimal(foundPFLoanRow[0]["MonthlyInterest"].ToString().Trim(), 0);
                                    else if ((foundPFLLRow.Length > 0) && (Convert.ToDecimal(foundPFLLRow[0]["CLLOAN"].ToString())>0))
                                        dclSalHeadAmount = Common.RoundDecimal(foundPFLLRow[0]["CMINTEREST"].ToString().Trim(), 0);
                                    else
                                        dclSalHeadAmount = 0;

                                    dclSalHeadAmount = dclSalHeadAmount * -1;
                                    dclSalHeadAmount = decimal.Round(dclSalHeadAmount, 0);
                                }
                                 ////   else
                                 ////{
                                 ////    dclSalHeadAmount = dclCLLAmount;
                                 ////    //dclSalHeadAmount = Common.RoundDecimal(foundPFLLRow[0]["CMINTEREST"].ToString().Trim(), 0);
                                 ////}

                                 // Get Percent Amount;
                                 //if (dclSalHeadAmount > 0)
                                 //{
                                 //    dclSalHeadAmount = dclSalHeadAmount * 13 / 100;
                                 //}
                                                         
                                 break;
                             #endregion
                             case "14":// Festival Bonus 
                                 
                                 foundFBRow = dtFestivalBonus.Select("EMPID='" + dEmpRow["EMPID"].ToString().Trim() + "' AND SHEADID='" + dSalPackRow["SHEADID"].ToString().Trim() + "'");
                                 if (foundFBRow.Length > 0)
                                 {
                                     dclSalHeadAmount = Convert.ToDecimal(foundFBRow[0]["TOTALSALARY"].ToString());
                                     strIsWithBonus = "Y";
                                 }
                                 break;
                             case "15"://Income Tax

                                 dclSalHeadAmount = Common.RoundDecimal(dSalPackRow["TOTAMNT"].ToString(), 0);
                                 break;
                         }
                      
                        // Get OT data
                        //if (dSalPackRow["SHEADID"].ToString().Trim() == "8")
                        //{
                        //    foundOTRow = dtOverTime.Select("EMPID='" + dEmpRow["EMPID"].ToString().Trim() + "'");
                        //    if (foundOTRow.Length > 0)
                        //    {
                        //        dclSalHeadAmount = Convert.ToDecimal(foundOTRow[0]["OTAmount"].ToString());
                        //    }
                        //}

                        // Check for Provident Fund
                        if (dSalPackRow["ISPFUND"].ToString() == "Y")
                        {
                            this.AddSalPackDets(lngPayBookID.ToString(), dEmpRow["EMPID"].ToString().Trim(), dSalPackRow["SHEADID"].ToString(),
                                                dSalPackRow["HEADNAME"].ToString(), dSalPackRow["ISBASICSAL"].ToString(), dclSalHeadAmount.ToString(),
                                                "Y", "N", "N", "N", "Y", dSalPackRow["AMTCOMPAY"].ToString(), "N", "N", "N", "0", "N", "N", "N");
                        }
                        else
                        {
                            this.AddSalPackDets(lngPayBookID.ToString(), dEmpRow["EMPID"].ToString().Trim(), dSalPackRow["SHEADID"].ToString(),
                                                dSalPackRow["HEADNAME"].ToString(), dSalPackRow["ISBASICSAL"].ToString(), dclSalHeadAmount.ToString(),
                                                dSalPackRow["HEADNATURE"].ToString() == "1" ? "N" : "Y", "N", "N", "N", "N", "0.00", "N", "N", "N", "0", "N", "N", "N");
                        }

                        if (dSalPackRow["ISBASICSAL"].ToString() == "Y")
                            dclBasicAmount = dclSalHeadAmount;

                        // Calculate Net Pay Amt
                        dclEmpNetPayAmt = dclEmpNetPayAmt + dclSalHeadAmount;

                    }// Loop End Here  

                    // Insert Data into Salary Pack master Table of Dataset
                    this.AddSalPackMst(lngPayBookID.ToString(), lngPayID.ToString(), dEmpRow["EMPID"].ToString().Trim(), dEmpRow["FULLNAME"].ToString().Trim(),
                        dEmpRow["JobTitleName"].ToString().Trim(), Common.SetDate(PayStartDate.ToShortDateString()), Common.SetDate(PayEndDate.ToShortDateString()), "P", dEmpRow["SalPakId"].ToString().Trim(), dEmpRow["PAYTYPE"].ToString(),
                        objPayslip.dtPaySlipDets, "N", dclEmpNetPayAmt.ToString(), dEmpRow["SALDUR"].ToString().Trim(), dEmpRow["DEPTNAME"].ToString(), dEmpRow["LATECOUNT"].ToString(),
                        dEmpRow["LATESALCOUNT"].ToString(), dEmpRow["LATESALHEAD"].ToString(), strIsWithBonus, "N", dEmpRow["TOTALGROSSSAL"].ToString(),
                        IsIrregular == false ? "N" : "Y", dEmpRow["OTAmt"].ToString(), dEmpRow["IsInPercent"].ToString(), dEmpRow["SalHeadID"].ToString(),
                        "N", dEmpRow["BONUSPAKID"].ToString(), dEmpRow["WillConvert"].ToString(), dEmpRow["CurrId"].ToString(),
                        dEmpRow["ConvrsAmt"].ToString(), dEmpRow["ArearAmnt"].ToString(), dEmpRow["IsArearPaid"].ToString(), dclBasicAmount.ToString(),
                        "0", "0.00", dEmpRow["SPTitle"].ToString().Trim(), PayStartDate.ToShortDateString(), PayEndDate.ToShortDateString(),
                        inMonthDays.ToString(), dEmpRow["BANKCODE"].ToString().Trim(), dEmpRow["RoutingNo"].ToString().Trim(),
                        dEmpRow["BankAccNo"].ToString().Trim(), dEmpRow["DEPTID"].ToString().Trim(), dEmpRow["DesigId"].ToString().Trim(),
                        dEmpRow["EMPTYPEID"].ToString().Trim(), dEmpRow["PLANACCLINE"].ToString().Trim(), dEmpRow["JoiningDate"].ToString().Trim(), dEmpRow["SeparateDate"].ToString().Trim(),
                        "1", dEmpRow["DivisionId"].ToString().Trim(), dEmpRow["ClinicId"].ToString().Trim(), dEmpRow["ProbationPeriod"].ToString().Trim(), dEmpRow["ConfirmationDate"].ToString().Trim());

                    lngPayID = lngPayID + 1;

                    foundPFLLRow = null;
                    foundPFLRRow = null;
                    foundPFLoanRow = null;
                    foundBfRow = null;
                    //foundMedicalRow = null;
                    //foundHospitalRow = null;
                    //foundChildEduAllowRow = null;
                    //foundOTRow = null;
                    //foundAddResponeRow = null;
                    //foundReAllowRow = null;
                    foundFBRow = null;
                }
            }
        }

        foreach (DataRow dRowSalPackMst in objPayslip.dtPaySlipMst.Rows)
        {
            DataTable dtBlockHead = objMastMg.SelectSHeadBlockData(Common.ReturnDate(txtIssueDate.Text.Trim()));
            DataTable dtBlockPF = objMastMg.SelectProbationalEmployee(Common.ReturnDate(txtIssueDate.Text.Trim()));

            if (dRowSalPackMst["SalPayType"].ToString().Trim() == "3")
            {
                if (dRowSalPackMst["EMPTYPEID"].ToString().Trim() != "2")
                {
                    if (dRowSalPackMst["IsIrregular"].ToString().Trim() == "N")
                    {
                        this.FillMonthlyRegularPayment(dRowSalPackMst);
                        this.ValidateBlockSalaryHead(dRowSalPackMst, dtBlockHead, dtBlockPF);
                    }
                    else
                    {
                        this.FillMonthlyNotRegularPayment(dRowSalPackMst);
                        this.ValidateBlockSalaryHead(dRowSalPackMst, dtBlockHead, dtBlockPF);
                    }
                }
                else
                {
                    this.FillMonthlyContractualPayment(dRowSalPackMst);
                    this.ValidateBlockSalaryHead(dRowSalPackMst, dtBlockHead, dtBlockPF);
                }
            }

            ////if (string.IsNullOrEmpty(dRowSalPackMst["ConfirmationDate"].ToString()) == false)
            ////{
            ////    DateTime dtConfirmDate = Convert.ToDateTime(dRowSalPackMst["ConfirmationDate"].ToString().Trim());
            ////    if ((dtConfirmDate > PayStartDate) && (dtConfirmDate <= PayEndDate))
            ////    {
            ////        this.FillMonthlyNotRegularPaymentForPF(dRowSalPackMst);
            ////    }
            ////}
        }
    }

    //protected Decimal GetArrearHeadAmount(DataTable dt, Int32 inSheadID, string strEmpID)
    //{
    //    if (dt.Rows.Count == 0)
    //    {
    //        return 0;
    //    }
    //    DataRow[] foundArrRows = dt.Select("EMPID='" + strEmpID + "' AND SHEADID=" + inSheadID);
    //    if (foundArrRows.Length > 0)
    //    {
    //        return Common.RoundDecimal(foundArrRows[0]["PAYAMT"].ToString(), 0);
    //    }
    //    else
    //        return 0;
    //}

    protected void ValidateBlockSalaryHead(DataRow dRowSalPackMst, DataTable dtBlockHead, DataTable dtBlockPF)
    {
        DataRow[] fRowSalBlock = dtBlockHead.Select("EmpId='" + dRowSalPackMst["EmployeeID"].ToString().Trim() + "'");
        DataRow[] fRowPFBlock = dtBlockPF.Select("EmpId='" + dRowSalPackMst["EmployeeID"].ToString().Trim() + "'");//PF Blocked Employee
        DataRow[] fPayslipRows = objPayslip.Tables["dtPaySlipDets"].Select("EmployeeID='" + dRowSalPackMst["EmployeeID"].ToString().Trim() + "'");
        decimal decAmt = 0;
        decimal decHN = 0;
        foreach (DataRow fRowDet in fPayslipRows)
        {
            foreach (DataRow fRowSHB in fRowSalBlock)
            {
                if (fRowDet["SalHeadID"].ToString().Trim() == fRowSHB["SHEADID"].ToString().Trim())
                {
                    decAmt = Common.RoundDecimal(fRowSHB["BLOCKAMT"].ToString(), 0);
                    decHN = Common.RoundDecimal(fRowSHB["HEADNATURE"].ToString(), 0);
                    fRowDet["PAYAMNT"] = Convert.ToString(decAmt * decHN);
                    break;
                }
            }

            //Block PF if Employee in probation Period
            //foreach (DataRow fRowPF in fRowPFBlock)
            //{
            //    ////if (fRowDet["SalHeadID"].ToString().Trim() == "5" || fRowDet["SalHeadID"].ToString().Trim() == "6")
            //    if (fRowDet["SalHeadID"].ToString().Trim() == "13" || fRowDet["SalHeadID"].ToString().Trim() == "16")
            //    {
            //        fRowDet["PAYAMNT"] = 0;
            //        break;
            //    }
            //}
        }

        fRowSalBlock = null;
        fRowPFBlock = null;
        fPayslipRows = null;
        objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
    }

    protected void AddSalPackDets(string strPaySlipBookID, string strEmpID, string strSalPkHeadId,
                                  string strSalHeadName, string strIsBasicSal, string strSalHeadAmt,
                                  string strIsDeduct, string strIsOthPayment, string strIsAdvancDeduct,
                                  string strIsLateDeduct, string strIsPF, string strPFAmt,
                                  string strIsAttndBonus, string strIsProdBonus, string strIsOT,
                                  string strOTHour, string strPfLoanDeduct, string strIsFestivalBonus, string strIsArea)
    {
        DataRow nRow = objPayslip.dtPaySlipDets.NewRow();
        nRow["PSBookID"] = strPaySlipBookID;
        nRow["EmployeeID"] = strEmpID;
        nRow["SalHeadID"] = strSalPkHeadId;
        nRow["SalHeadTitle"] = strSalHeadName;
        nRow["IsBasicSal"] = strIsBasicSal;

        ////Partial IT Canculation Instead of Full.
        if (string.IsNullOrEmpty(txtPercentage.Text.Trim()) == false && strSalPkHeadId == "15")
            nRow["PayAmnt"] = Math.Round((Convert.ToDecimal(strSalHeadAmt) * Convert.ToDecimal(txtPercentage.Text.Trim()) / 100), 0);
        else
            nRow["PayAmnt"] = strSalHeadAmt;

        //Partial IT Canculation Instead of Full.
        //if (string.IsNullOrEmpty(txtPercentage.Text.Trim()) == false && strSalPkHeadId == "15")
        //    nRow["PayAmnt"] = Math.Round((Convert.ToDecimal(strSalHeadAmt) * Convert.ToDecimal(txtPercentage.Text.Trim()) / 100), 0);
        //else
        //{
        //    if (strIsPF == "N")
        //        nRow["PayAmnt"] = strSalHeadAmt;
        //    //else
        //        //Prio rata PF on confirmation date 
        //        // Call FillMonthlyNotRegularPayment
        //        //this.FillMonthlyNotRegularPaymentForPF(objPayslip.dtPaySlipMst.Rows);
        //        //nRow["PayAmnt"] = 
        //}

        nRow["IsDeducted"] = strIsDeduct;
        nRow["IsOtherPayment"] = strIsOthPayment;
        nRow["IsAdvanceDeducttion"] = strIsAdvancDeduct;
        nRow["IsLateDeduction"] = strIsLateDeduct;
        nRow["IsProvidentFund"] = strIsPF;
        nRow["PFAmount"] = Common.RoundDecimal(strPFAmt, 0).ToString();
        nRow["IsAttndBonus"] = strIsAttndBonus;
        nRow["IsProductionBonus"] = strIsProdBonus;
        nRow["IsOT"] = strIsOT;
        nRow["OTHour"] = strOTHour;
        nRow["IsPFLoanDeduction"] = strPfLoanDeduct;
        nRow["IsFestivalBonus"] = strIsFestivalBonus;
        nRow["IsArea"] = strIsArea;
        objPayslip.dtPaySlipDets.Rows.Add(nRow);
        objPayslip.dtPaySlipDets.AcceptChanges();
    }

    protected void AddSalPackMst(string strPaySlipBookID, string strPayID, string strEmpID, string strEmpName, string strJobTitle,
        string strStartDate, string strEndDate, string strPayslipStatus, string strSalPakId, string strSalPayType, DataTable dtSalPackDetailsTmp,
        string strIsAdjusting, string strPackageAmt, string TWorkingDayHour, string strDeptName, string strLateDayCount, string strLateSalDeductCount,
        string strLateSalHeadID, string strIsWithBonus, string strIsAllowedForAttBonus, string strGrossAmount, string strIsIrregular, string strOTAmt,
        string strOTIsInPercent, string strOTSalHead, string strIsOnlyBonus, string strBonusPackID, string strIsConvertCurrency, string strCurrencyID,
        string strCurrencyConvAmnt, string strAreaAmount, string strIsAreaPaid, string strBasicAmount, string strLateDedASOTDEDHouR,
        string strLateDedASOTDEDAMt, string strSPTitle, string strSalStartDate, string strSalEndDate, string strSalMonthDays,
        string strBankCode, string strBranchCode, string strAccNo, string strDeptId, string strDesgId,string strEmpTypeId, string strPlanAccline,
        string strJoinDate, string strLeavingDate, string strEmpGrpID, string strDivID,string strClinicID, string strProbPeriod, string strConfirmationDate)
    {
        DataRow nRow = objPayslip.dtPaySlipMst.NewRow();
        nRow["PSBookID"] = strPaySlipBookID;
        nRow["PayID"] = strPayID;
        nRow["EmployeeID"] = strEmpID;
        nRow["Empname"] = strEmpName;
        nRow["JobTitleName"] = strJobTitle;
        nRow["StartDate"] = strSalStartDate;
        nRow["EndDate"] = strSalEndDate;
        nRow["PaySlipSatus"] = strPayslipStatus;
        nRow["SalPakId"] = strSalPakId;
        nRow["SalPayType"] = strSalPayType;
        nRow["isAdjusting"] = strIsAdjusting;
        nRow["PackageAmount"] = Common.RoundDecimal(strPackageAmt, 0);
        nRow["TWorkingDayHour"] = TWorkingDayHour;
        nRow["DeptName"] = strDeptName;
        nRow["LateDayCount"] = strLateDayCount;
        nRow["LateSalDeductCount"] = strLateSalDeductCount;
        nRow["LateSalHeadID"] = strLateSalHeadID;
        nRow["IsWithBonus"] = strIsWithBonus;
        nRow["IsAllowedForAttBonus"] = strIsAllowedForAttBonus;

        nRow["GrossAmount"] = Common.RoundDecimal(strGrossAmount, 0);
        nRow["IsIrregular"] = strIsIrregular;
        nRow["OTAmnt"] = strOTAmt;
        nRow["OTIsInPercent"] = strOTIsInPercent;
        nRow["OTSalHead"] = strOTSalHead;
        nRow["IsOnlyBonus"] = strIsOnlyBonus;
        nRow["BonusPackID"] = strBonusPackID;
        nRow["IsConvertCurrency"] = strIsConvertCurrency;
        nRow["CurrencyID"] = strCurrencyID;
        nRow["CurrencyConvAmnt"] = strCurrencyConvAmnt;
        nRow["AreaAmount"] = strAreaAmount;
        nRow["IsAreaPaid"] = strIsAreaPaid;
        nRow["LateDedASOTDEDHouR"] = strLateDedASOTDEDHouR;
        nRow["LateDedASOTDEDAMt"] = strLateDedASOTDEDAMt;
        nRow["SPTitle"] = strSPTitle;

        nRow["SalStartDate"] = strSalStartDate;
        nRow["SalEndDate"] = strSalEndDate;
        nRow["MonthDays"] = strSalMonthDays;

        nRow["BANKCODE"] = strBankCode;
        nRow["RoutingNo"] = strBranchCode;
        nRow["BankAccNo"] = strAccNo;
        nRow["DEPTID"] = strDeptId;
        nRow["DesigId"] = strDesgId;
        nRow["EMPTYPEID"] = strEmpTypeId;
        nRow["DivisionId"] = strDivID;
        nRow["ClinicId"] = strClinicID;
        nRow["PLANACCLINE"] = strPlanAccline;
        nRow["JoiningDate"] = strJoinDate;
        nRow["SeparateDate"] = strLeavingDate;
        nRow["EmpGrpID"] = strEmpGrpID;        
        nRow["ProbationPeriod"] = strProbPeriod == "" ? "0" : strProbPeriod;
        nRow["ConfirmationDate"] = strConfirmationDate;
        objPayslip.dtPaySlipMst.Rows.Add(nRow);
        objPayslip.dtPaySlipMst.AcceptChanges();
    }

    protected void FillMonthlyRegularPayment(DataRow dRowMst)
    {
        DataTable dtAttnRecord = new DataTable();
        DataRow nRow = objPayslip.dtPayslipPreparation.NewRow();

        nRow["PSBID"] = dRowMst["PSBookID"];
        nRow["EMPID"] = dRowMst["EmployeeID"];
        nRow["FullName"] = dRowMst["Empname"];
        nRow["JobTitleName"] = dRowMst["JobTitleName"];
        nRow["JobTitleId"] = "";
        nRow["DeptName"] = dRowMst["DeptName"];
        nRow["DeptId"] = "";

        // Check for Bonus
        // AddBonusAmount(dRowMst)


        //  'Update code for all kinds of Loan/Deposit
        //If PAYSLIP_ADVANCE_LOAN_DEDUCT_SALARY_HEAD_ID > 0 Then
        DataTable dtLoanType = new DataTable();
        dtLoanType = objPreMgr.GetLoanType();
        foreach (DataRow dLoanRow in dtLoanType.Rows)
        {
            this.AddLoanDeduction(dRowMst, dLoanRow["SHeadID"].ToString(), dLoanRow["LoanTypeID"].ToString());
        }
        dtLoanType.Rows.Clear();
        dtLoanType.Dispose();
        
        // Salary Adjust
        this.AddSalaryAdjust(dRowMst, dRowMst["EmployeeID"].ToString().Trim());
     
        dRowMst["TotalDays"] = dRowMst["MonthDays"].ToString().Trim();
        dRowMst["SPTitle"] = dRowMst["SPTitle"].ToString().Trim();
        // dRowMst["TotalDays"] = dclTotalWorkedDay.ToString();
        dRowMst.AcceptChanges();
    }

    protected void FillMonthlyNotRegularPayment(DataRow dRowMst)
    {
        DataTable dtAttnRecord = new DataTable();
        DataTable dtEmpInfo = new DataTable();
        //int inStartMonth = Convert.ToDateTime().Month;
        DateTime dtStartDate = Convert.ToDateTime(dRowMst["StartDate"].ToString().Trim());
        DateTime dtEndDate = Convert.ToDateTime(dRowMst["EndDate"].ToString().Trim());
        DateTime dtJobStartDate = Convert.ToDateTime(dRowMst["JoiningDate"].ToString().Trim());

        DateTime dtJobEndDate = Convert.ToDateTime(dRowMst["SalEndDate"].ToString().Trim());

        Int16 iProbationPeriod = Convert.ToInt16(dRowMst["ProbationPeriod"].ToString().Trim());

        DateTime dtSepDate = new DateTime();
        if (string.IsNullOrEmpty(dRowMst["SeparateDate"].ToString().Trim()) == false)
        {
            dtSepDate = Convert.ToDateTime(dRowMst["SeparateDate"].ToString().Trim());
            dtJobEndDate = Convert.ToDateTime(dRowMst["SeparateDate"].ToString().Trim());
        }

        DateTime dtMonthStartDate = Convert.ToDateTime(ddlYear.SelectedValue.Trim() + "/" + ddlMonth.SelectedValue.Trim() + "/01");
        int inMonthDays = Convert.ToInt32(dRowMst["MonthDays"].ToString().Trim());
        //,int inSMonth,int inEMonth

        // Dim rsEmpInfo As New ADODB.Recordset
        int EmpWeekEndID;

        DataRow nRow = objPayslip.dtPayslipPreparation.NewRow();
        nRow["PSBID"] = dRowMst["PSBookID"];
        nRow["EMPID"] = dRowMst["EmployeeID"];
        nRow["FullName"] = dRowMst["Empname"];
        nRow["JobTitleName"] = dRowMst["JobTitleName"];
        nRow["JobTitleId"] = "";
        nRow["DeptName"] = dRowMst["DeptName"];
        nRow["DeptId"] = "";

        DataTable dtLoanType = new DataTable();
        dtLoanType = objPreMgr.GetLoanType();
        foreach (DataRow dLoanRow in dtLoanType.Rows)
        {
            this.AddLoanDeduction(dRowMst, dLoanRow["SHeadID"].ToString(), dLoanRow["LoanTypeID"].ToString());
        }
        dtLoanType.Rows.Clear();
        dtLoanType.Dispose();

        // Salary Adjust
        this.AddSalaryAdjust(dRowMst, dRowMst["EmployeeID"].ToString().Trim());
        //Call AddSalaryAdjust(i, Trim(objPaySlipMst(i).EmployeeID))
        // Tax Deuction  
        //If PAYSLIP_TAXDEDEDUCTION_SALARYHEAD > 0 Then
        //Call TaxDeduction(i)
        //End If

        dRowMst["TotalDays"] = dRowMst["MonthDays"].ToString().Trim();
        dRowMst["SPTitle"] = dRowMst["SPTitle"].ToString().Trim();
        dRowMst.AcceptChanges();

        // Calculate Unit Day Salary
        decimal decUnitDaySalary = 0;
        decimal decHeadArrayAmt = 0;
        decimal decNetPayAmt = 0;
        string strPrevMonth = "";
        string strPrevYear = "";
        long lngTotalMonthDays = 0;
        if (Convert.ToInt64(dRowMst["MonthDays"].ToString().Trim()) < Convert.ToInt64(dRowMst["TWorkingDayHour"].ToString().Trim()))
        {
            strPrevMonth = Common.GetPreviousMonth(Convert.ToDateTime(dRowMst["SalEndDate"].ToString().Trim()).Month.ToString());
            if (strPrevMonth == "12")
                strPrevYear = Convert.ToString(Convert.ToDateTime(dRowMst["SalEndDate"].ToString().Trim()).Year - 1);
            lngTotalMonthDays = Common.GetMonthDay(Convert.ToInt32(strPrevMonth), strPrevYear);
        }
        else
            lngTotalMonthDays = Convert.ToInt64(dRowMst["MonthDays"].ToString().Trim());
        
        // Pay Slip Details Update
        DataRow[] foundRows = objPayslip.Tables["dtPaySlipDets"].Select("EmployeeID='" + dRowMst["EmployeeID"].ToString().Trim() + "'");
        decimal decPfCont = 0;
        if (foundRows.Length > 0)
        {
            decNetPayAmt = 0;

            #region In Active
            //foreach (DataRow fRows in foundRows)
            //{
            //    if (fRows["SalHeadID"].ToString().Trim() == "15") //INCOME TAX
            //    {
            //        if (dRowMst["TWorkingDayHour"].ToString().Trim() != "0")
            //        {
            //            decHeadArrayAmt = Common.RoundDecimal(fRows["PayAmnt"].ToString(), 0);
            //            fRows["PayAmnt"] = decHeadArrayAmt.ToString();
            //            objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
            //            decNetPayAmt = decNetPayAmt + decHeadArrayAmt;
            //            decHeadArrayAmt = 0;
            //        }
            //        else
            //        {
            //            fRows["PayAmnt"] = "0";
            //            objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
            //        }

            //        continue;
            //    }

            //    if (fRows["SalHeadID"].ToString().Trim() == "6") // PF EMPLOYEE 
            //    {
            //        if (decPfCont != 0)
            //        {
            //            decPfCont = decPfCont * -2;
            //            fRows["PayAmnt"] = decPfCont.ToString();
            //            objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
            //            decNetPayAmt = decNetPayAmt + decPfCont;
            //            decPfCont = 0;
            //            continue;
            //        }
            //    }

            //    if (fRows["SalHeadID"].ToString().Trim() == "9") // REVENUE STAMP
            //    {
            //        if (dRowMst["TWorkingDayHour"].ToString().Trim() != "0")
            //        {
            //            decHeadArrayAmt = Common.RoundDecimal(fRows["PayAmnt"].ToString(), 0);
            //            fRows["PayAmnt"] = decHeadArrayAmt.ToString();
            //            objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
            //            decNetPayAmt = decNetPayAmt + decHeadArrayAmt;
            //            decHeadArrayAmt = 0;
            //        }
            //        else
            //        {
            //            fRows["PayAmnt"] = "0";
            //            objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
            //        }
            //        continue;
            //    }

            //    if (fRows["SalHeadID"].ToString().Trim() == "6") // PF Loan
            //    {
            //        if (dRowMst["TWorkingDayHour"].ToString().Trim() != "0")
            //        {
            //            decHeadArrayAmt = Common.RoundDecimal(fRows["PayAmnt"].ToString(), 0);
            //            fRows["PayAmnt"] = decHeadArrayAmt.ToString();
            //            objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
            //            decNetPayAmt = decNetPayAmt + decHeadArrayAmt;
            //            decHeadArrayAmt = 0;
            //        }
            //        else
            //        {
            //            fRows["PayAmnt"] = "0";
            //            objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
            //        }
            //        continue;
            //    }

            //    if (fRows["SalHeadID"].ToString().Trim() == "16") // PF Loan Interest
            //    {
            //        if (dRowMst["TWorkingDayHour"].ToString().Trim() != "0")
            //        {
            //            decHeadArrayAmt = Common.RoundDecimal(fRows["PayAmnt"].ToString(), 0);
            //            fRows["PayAmnt"] = decHeadArrayAmt.ToString();
            //            objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
            //            decNetPayAmt = decNetPayAmt + decHeadArrayAmt;
            //            decHeadArrayAmt = 0;
            //        }
            //        else
            //        {
            //            fRows["PayAmnt"] = "0";
            //            objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
            //        }
            //        continue;
            //    }

            #endregion

            //**********This Salary Head are not Applicable for Partial Calculation**********
            //int[] strV = { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 14, 16, 17, 18 };
            foreach (DataRow fRows in foundRows)
            {
                bool IsExist = false;
                if (iProbationPeriod != 0)
                {
                    int[] strV = new int[] {  4, 5, 6, 7, 8, 9, 10, 11, 12, 14, 16, 17, 18 };
                    for (int i = 0; i < strV.Length; i++)
                    {
                        if (fRows["SalHeadID"].ToString().Trim() == strV[i].ToString()) //Alico(+)
                        {
                            if (dRowMst["TWorkingDayHour"].ToString().Trim() != "0")
                            {
                                decHeadArrayAmt = Common.RoundDecimal(fRows["PayAmnt"].ToString(), 0);
                                fRows["PayAmnt"] = decHeadArrayAmt.ToString();
                                objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
                                decNetPayAmt = decNetPayAmt + decHeadArrayAmt;
                                decHeadArrayAmt = 0;
                            }
                            else
                            {
                                fRows["PayAmnt"] = "0";
                                objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
                            }

                            IsExist = true;
                            break;
                        }
                    }
                }
                else
                {
                    int[] strV = new int[] { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13,14, 16, 17, 18 };
                    for (int i = 0; i < strV.Length; i++)
                    {
                        if (fRows["SalHeadID"].ToString().Trim() == strV[i].ToString()) //Alico(+)
                        {
                            if (dRowMst["TWorkingDayHour"].ToString().Trim() != "0")
                            {
                                decHeadArrayAmt = Common.RoundDecimal(fRows["PayAmnt"].ToString(), 0);
                                fRows["PayAmnt"] = decHeadArrayAmt.ToString();
                                objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
                                decNetPayAmt = decNetPayAmt + decHeadArrayAmt;
                                decHeadArrayAmt = 0;
                            }
                            else
                            {
                                fRows["PayAmnt"] = "0";
                                objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
                            }

                            IsExist = true;
                            break;
                        }
                    }
                }

                if (IsExist == true)
                {
                    continue;
                }

                if (Common.FindInDataTable(dtGrossSalHead, fRows["SalHeadID"].ToString().Trim(), "SHeadID") == true)
                {
                    if (Convert.ToInt64(dRowMst["MonthDays"].ToString().Trim()) < Convert.ToInt64(dRowMst["TWorkingDayHour"].ToString().Trim()))
                    {
                        long lngDaysDiff = Convert.ToInt64(dRowMst["TWorkingDayHour"].ToString().Trim()) - Convert.ToInt64(dRowMst["MonthDays"].ToString().Trim());
                        decHeadArrayAmt = 0;
                        decHeadArrayAmt = Convert.ToDecimal(fRows["PayAmnt"].ToString());
                        decUnitDaySalary = decHeadArrayAmt / Convert.ToDecimal(lngTotalMonthDays);
                        decUnitDaySalary = Math.Round(decUnitDaySalary, 2);
                        decUnitDaySalary = decUnitDaySalary * Convert.ToDecimal(lngDaysDiff);
                        decHeadArrayAmt = decHeadArrayAmt + decUnitDaySalary;
                        decHeadArrayAmt = Math.Round(decHeadArrayAmt, 0);
                        fRows["PayAmnt"] = decHeadArrayAmt.ToString();
                        objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
                        decNetPayAmt = decNetPayAmt + decHeadArrayAmt;
                        continue;
                    }
                    else
                    {
                        bool IsDualMonth = false;

                        if (dtStartDate.Month < dtMonthStartDate.Month)
                        {
                            IsDualMonth = true;
                        }
                        else
                        {
                            IsDualMonth = false;
                        }
                        if (IsDualMonth == true)
                        {
                            int inDaysDiff = 0;
                            decimal decPUnitDaySalary = 0;
                            decimal decCUnitDaySalary = 0;
                            if (string.IsNullOrEmpty(dRowMst["LEAVINGDATE"].ToString().Trim()) == false)
                            {
                                if ((dtSepDate > dtStartDate) && (dtSepDate < dtEndDate))
                                {
                                    if (dtSepDate.Month == dtStartDate.Month)
                                    {
                                        decHeadArrayAmt = 0;
                                        decHeadArrayAmt = Convert.ToDecimal(fRows["PayAmnt"].ToString());
                                        // Current Month Days Diff
                                        inDaysDiff = 0;
                                        inDaysDiff = dtJobEndDate.Day - dtMonthStartDate.Day;
                                        decCUnitDaySalary = decHeadArrayAmt / Convert.ToDecimal(Common.GetMonthDay(dtMonthStartDate));
                                        decCUnitDaySalary = Math.Round(decCUnitDaySalary, 2);
                                        decCUnitDaySalary = decCUnitDaySalary * Convert.ToDecimal(inDaysDiff);
                                    }
                                    else if (dtSepDate.Month == dtEndDate.Month)
                                    {
                                        // Joind Date is Less Than Month StartDate
                                        int inPrevMonthDays = Common.GetMonthDay(dtStartDate);
                                        decHeadArrayAmt = 0;
                                        decHeadArrayAmt = Convert.ToDecimal(fRows["PayAmnt"].ToString());

                                        DateTime dtDualMonthEndDate = Convert.ToDateTime(dtStartDate.Year.ToString() + "-" + dtStartDate.Month.ToString() + "-" + inPrevMonthDays.ToString());
                                        // Previous Month Days Diff
                                        inDaysDiff = dtDualMonthEndDate.Day - dtStartDate.Day + 1;
                                        decPUnitDaySalary = decHeadArrayAmt / Convert.ToDecimal(inPrevMonthDays);
                                        decPUnitDaySalary = Math.Round(decPUnitDaySalary, 2);
                                        decPUnitDaySalary = decPUnitDaySalary * Convert.ToDecimal(inDaysDiff);

                                        // Current Month Days Diff
                                        inDaysDiff = 0;
                                        inDaysDiff = dtJobEndDate.Day - dtMonthStartDate.Day;
                                        decCUnitDaySalary = decHeadArrayAmt / Convert.ToDecimal(Common.GetMonthDay(dtMonthStartDate));
                                        decCUnitDaySalary = Math.Round(decCUnitDaySalary, 2);
                                        decCUnitDaySalary = decCUnitDaySalary * Convert.ToDecimal(inDaysDiff);
                                    }
                                }

                                else if (dtJobStartDate < dtMonthStartDate)
                                {
                                    // Joind Date is Less Than Month StartDate
                                    int inPrevMonthDays = Common.GetMonthDay(dtJobStartDate);
                                    decHeadArrayAmt = 0;
                                    decHeadArrayAmt = Convert.ToDecimal(fRows["PayAmnt"].ToString());

                                    DateTime dtDualMonthEndDate = Convert.ToDateTime(dtJobStartDate.Year.ToString() + "-" + dtJobStartDate.Month.ToString() + "-" + inPrevMonthDays.ToString());
                                    // Previous Month Days Diff
                                    inDaysDiff = dtDualMonthEndDate.Day - dtJobStartDate.Day + 1;
                                    decPUnitDaySalary = decHeadArrayAmt / Convert.ToDecimal(inPrevMonthDays);
                                    decPUnitDaySalary = Math.Round(decPUnitDaySalary, 2);
                                    decPUnitDaySalary = decPUnitDaySalary * Convert.ToDecimal(inDaysDiff);

                                    // Current Month Days Diff
                                    inDaysDiff = 0;
                                    inDaysDiff = dtEndDate.Day - dtMonthStartDate.Day + 1;
                                    decCUnitDaySalary = decHeadArrayAmt / Convert.ToDecimal(Common.GetMonthDay(dtMonthStartDate));
                                    decCUnitDaySalary = Math.Round(decCUnitDaySalary, 2);
                                    decCUnitDaySalary = decCUnitDaySalary * Convert.ToDecimal(inDaysDiff);
                                }
                                else
                                {
                                    // Joind Date is Equal or Greater Than Month StartDate
                                    decHeadArrayAmt = 0;
                                    decHeadArrayAmt = Convert.ToDecimal(fRows["PayAmnt"].ToString());
                                    // Current Month Days Diff
                                    inDaysDiff = 0;
                                    inDaysDiff = dtEndDate.Day - dtJobStartDate.Day + 1;
                                    decCUnitDaySalary = decHeadArrayAmt / Convert.ToDecimal(lngTotalMonthDays);
                                    decCUnitDaySalary = Math.Round(decCUnitDaySalary, 2);
                                    decCUnitDaySalary = decCUnitDaySalary * Convert.ToDecimal(inDaysDiff);
                                }
                                decHeadArrayAmt = decPUnitDaySalary + decCUnitDaySalary;
                                decHeadArrayAmt = Math.Round(decHeadArrayAmt, 0);
                                fRows["PayAmnt"] = decHeadArrayAmt.ToString();
                                objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
                                decNetPayAmt = decNetPayAmt + decHeadArrayAmt;
                                continue;

                            }
                            else if (dtJobStartDate < dtMonthStartDate)
                            {
                                // Joind Date is Less Than Month StartDate
                                int inPrevMonthDays = Common.GetMonthDay(dtJobStartDate);
                                decHeadArrayAmt = 0;
                                decHeadArrayAmt = Convert.ToDecimal(fRows["PayAmnt"].ToString());

                                DateTime dtDualMonthEndDate = Convert.ToDateTime(dtJobStartDate.Year.ToString() + "-" + dtJobStartDate.Month.ToString() + "-" + inPrevMonthDays.ToString());
                                // Previous Month Days Diff
                                inDaysDiff = dtDualMonthEndDate.Day - dtJobStartDate.Day + 1;
                                decPUnitDaySalary = decHeadArrayAmt / Convert.ToDecimal(inPrevMonthDays);
                                decPUnitDaySalary = Math.Round(decPUnitDaySalary, 2);
                                decPUnitDaySalary = decPUnitDaySalary * Convert.ToDecimal(inDaysDiff);

                                // Current Month Days Diff
                                inDaysDiff = 0;
                                inDaysDiff = dtEndDate.Day - dtMonthStartDate.Day + 1;
                                decCUnitDaySalary = decHeadArrayAmt / Convert.ToDecimal(Common.GetMonthDay(dtMonthStartDate));
                                decCUnitDaySalary = Math.Round(decCUnitDaySalary, 2);
                                decCUnitDaySalary = decCUnitDaySalary * Convert.ToDecimal(inDaysDiff);
                            }
                            else
                            {
                                // Joind Date is Equal or Greater Than Month StartDate
                                decHeadArrayAmt = 0;
                                decHeadArrayAmt = Convert.ToDecimal(fRows["PayAmnt"].ToString());
                                // Current Month Days Diff
                                inDaysDiff = 0;
                                inDaysDiff = dtEndDate.Day - dtJobStartDate.Day + 1;
                                decCUnitDaySalary = decHeadArrayAmt / Convert.ToDecimal(lngTotalMonthDays);
                                decCUnitDaySalary = Math.Round(decCUnitDaySalary, 2);
                                decCUnitDaySalary = decCUnitDaySalary * Convert.ToDecimal(inDaysDiff);
                            }

                            decHeadArrayAmt = decPUnitDaySalary + decCUnitDaySalary;
                            decHeadArrayAmt = Math.Round(decHeadArrayAmt, 0);
                            fRows["PayAmnt"] = decHeadArrayAmt.ToString();
                            objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
                            decNetPayAmt = decNetPayAmt + decHeadArrayAmt;
                            continue;
                        }
                        if (IsDualMonth == false)
                        {
                            decHeadArrayAmt = 0;
                            decHeadArrayAmt = Convert.ToDecimal(fRows["PayAmnt"].ToString());
                            decHeadArrayAmt = decHeadArrayAmt / Convert.ToDecimal(lngTotalMonthDays);
                            decHeadArrayAmt = Math.Round(decHeadArrayAmt, 2);
                            decHeadArrayAmt = decHeadArrayAmt * Convert.ToDecimal(dRowMst["TWorkingDayHour"].ToString().Trim());
                            decHeadArrayAmt = Math.Round(decHeadArrayAmt, 0);
                            fRows["PayAmnt"] = decHeadArrayAmt.ToString();
                            objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
                            decNetPayAmt = decNetPayAmt + decHeadArrayAmt;
                        }
                        continue;
                    }
                }

                DataRow[] foundBfRow = dtBenefits.Select("EMPID='" + dRowMst["EmployeeID"].ToString().Trim() + "' AND SHEADID='" + fRows["SalHeadID"].ToString().Trim() + "'");
                if (foundBfRow.Length == 0)
                {
                    if (Convert.ToInt64(dRowMst["MonthDays"].ToString().Trim()) < Convert.ToInt64(dRowMst["TWorkingDayHour"].ToString().Trim()))
                    {
                        long lngDaysDiff = Convert.ToInt64(dRowMst["TWorkingDayHour"].ToString().Trim()) - Convert.ToInt64(dRowMst["MonthDays"].ToString().Trim());
                        decHeadArrayAmt = 0;
                        decHeadArrayAmt = Convert.ToDecimal(fRows["PayAmnt"].ToString());
                        decUnitDaySalary = decHeadArrayAmt / Convert.ToDecimal(lngTotalMonthDays);
                        decUnitDaySalary = Math.Round(decUnitDaySalary, 0);
                        decUnitDaySalary = decUnitDaySalary * Convert.ToDecimal(lngDaysDiff);
                        decHeadArrayAmt = decHeadArrayAmt + decUnitDaySalary;
                        decHeadArrayAmt = Math.Round(decHeadArrayAmt, 0);
                        fRows["PayAmnt"] = decHeadArrayAmt.ToString();
                        objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
                        decNetPayAmt = decNetPayAmt + decHeadArrayAmt;
                        continue;
                    }
                    else
                    {
                        bool IsDualMonth = false;

                        if (dtStartDate.Month < dtMonthStartDate.Month)
                        {
                            IsDualMonth = true;
                        }
                        else
                        {
                            IsDualMonth = false;
                        }
                        if (IsDualMonth == true)
                        {
                            int inDaysDiff = 0;
                            decimal decPUnitDaySalary = 0;
                            decimal decCUnitDaySalary = 0;

                            if (string.IsNullOrEmpty(dRowMst["LEAVINGDATE"].ToString().Trim()) == false)
                            {
                                if ((dtSepDate > dtStartDate) && (dtSepDate < dtEndDate))
                                {
                                    if (dtSepDate.Month == dtStartDate.Month)
                                    {
                                        decHeadArrayAmt = 0;
                                        decHeadArrayAmt = Convert.ToDecimal(fRows["PayAmnt"].ToString());
                                        // Current Month Days Diff
                                        inDaysDiff = 0;
                                        inDaysDiff = dtJobEndDate.Day - dtMonthStartDate.Day;
                                        decCUnitDaySalary = decHeadArrayAmt / Convert.ToDecimal(Common.GetMonthDay(dtMonthStartDate));
                                        decCUnitDaySalary = Math.Round(decCUnitDaySalary, 2);
                                        decCUnitDaySalary = decCUnitDaySalary * Convert.ToDecimal(inDaysDiff);
                                    }
                                    else if (dtSepDate.Month == dtEndDate.Month)
                                    {
                                        // Joind Date is Less Than Month StartDate
                                        int inPrevMonthDays = Common.GetMonthDay(dtStartDate);
                                        decHeadArrayAmt = 0;
                                        decHeadArrayAmt = Convert.ToDecimal(fRows["PayAmnt"].ToString());

                                        DateTime dtDualMonthEndDate = Convert.ToDateTime(dtStartDate.Year.ToString() + "-" + dtStartDate.Month.ToString() + "-" + inPrevMonthDays.ToString());
                                        // Previous Month Days Diff
                                        inDaysDiff = dtDualMonthEndDate.Day - dtStartDate.Day + 1;
                                        decPUnitDaySalary = decHeadArrayAmt / Convert.ToDecimal(inPrevMonthDays);
                                        decPUnitDaySalary = Math.Round(decPUnitDaySalary, 2);
                                        decPUnitDaySalary = decPUnitDaySalary * Convert.ToDecimal(inDaysDiff);

                                        // Current Month Days Diff
                                        inDaysDiff = 0;
                                        inDaysDiff = dtJobEndDate.Day - dtMonthStartDate.Day;
                                        decCUnitDaySalary = decHeadArrayAmt / Convert.ToDecimal(Common.GetMonthDay(dtMonthStartDate));
                                        decCUnitDaySalary = Math.Round(decCUnitDaySalary, 2);
                                        decCUnitDaySalary = decCUnitDaySalary * Convert.ToDecimal(inDaysDiff);
                                    }
                                }

                                else if (dtJobStartDate < dtMonthStartDate)
                                {
                                    // Joind Date is Less Than Month StartDate
                                    int inPrevMonthDays = Common.GetMonthDay(dtJobStartDate);
                                    decHeadArrayAmt = 0;
                                    decHeadArrayAmt = Convert.ToDecimal(fRows["PayAmnt"].ToString());

                                    DateTime dtDualMonthEndDate = Convert.ToDateTime(dtJobStartDate.Year.ToString() + "-" + dtJobStartDate.Month.ToString() + "-" + inPrevMonthDays.ToString());
                                    // Previous Month Days Diff
                                    inDaysDiff = dtDualMonthEndDate.Day - dtJobStartDate.Day + 1;
                                    decPUnitDaySalary = decHeadArrayAmt / Convert.ToDecimal(inPrevMonthDays);
                                    decPUnitDaySalary = Math.Round(decPUnitDaySalary, 2);
                                    decPUnitDaySalary = decPUnitDaySalary * Convert.ToDecimal(inDaysDiff);

                                    // Current Month Days Diff
                                    inDaysDiff = 0;
                                    inDaysDiff = dtEndDate.Day - dtMonthStartDate.Day + 1;
                                    decCUnitDaySalary = decHeadArrayAmt / Convert.ToDecimal(Common.GetMonthDay(dtMonthStartDate));
                                    decCUnitDaySalary = Math.Round(decCUnitDaySalary, 2);
                                    decCUnitDaySalary = decCUnitDaySalary * Convert.ToDecimal(inDaysDiff);
                                }
                                else
                                {
                                    // Joind Date is Equal or Greater Than Month StartDate
                                    decHeadArrayAmt = 0;
                                    decHeadArrayAmt = Convert.ToDecimal(fRows["PayAmnt"].ToString());
                                    // Current Month Days Diff
                                    inDaysDiff = 0;
                                    inDaysDiff = dtEndDate.Day - dtJobStartDate.Day + 1;
                                    decCUnitDaySalary = decHeadArrayAmt / Convert.ToDecimal(lngTotalMonthDays);
                                    decCUnitDaySalary = Math.Round(decCUnitDaySalary, 2);
                                    decCUnitDaySalary = decCUnitDaySalary * Convert.ToDecimal(inDaysDiff);
                                }
                                decHeadArrayAmt = decPUnitDaySalary + decCUnitDaySalary;
                                decHeadArrayAmt = Math.Round(decHeadArrayAmt, 0);
                                fRows["PayAmnt"] = decHeadArrayAmt.ToString();
                                objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
                                decNetPayAmt = decNetPayAmt + decHeadArrayAmt;
                                continue;
                            }

                            else if (dtJobStartDate < dtMonthStartDate)
                            {
                                // Joind Date is Less Than Month StartDate
                                int inPrevMonthDays = Common.GetMonthDay(dtJobStartDate);
                                decHeadArrayAmt = 0;
                                decHeadArrayAmt = Convert.ToDecimal(fRows["PayAmnt"].ToString());

                                DateTime dtDualMonthEndDate = Convert.ToDateTime(dtJobStartDate.Year.ToString() + "-" + dtJobStartDate.Month.ToString() + "-" + inPrevMonthDays.ToString());
                                // Previous Month Days Diff
                                inDaysDiff = dtDualMonthEndDate.Day - dtJobStartDate.Day + 1;
                                decPUnitDaySalary = decHeadArrayAmt / Convert.ToDecimal(inPrevMonthDays);
                                decPUnitDaySalary = Math.Round(decPUnitDaySalary, 2);
                                decPUnitDaySalary = decPUnitDaySalary * Convert.ToDecimal(inDaysDiff);

                                // Current Month Days Diff
                                inDaysDiff = 0;
                                inDaysDiff = dtEndDate.Day - dtMonthStartDate.Day + 1;
                                decCUnitDaySalary = decHeadArrayAmt / Convert.ToDecimal(Common.GetMonthDay(dtMonthStartDate));
                                decCUnitDaySalary = Math.Round(decCUnitDaySalary, 2);
                                decCUnitDaySalary = decCUnitDaySalary * Convert.ToDecimal(inDaysDiff);
                            }
                            else
                            {
                                // Joind Date is Equal or Greater Than Month StartDate
                                decHeadArrayAmt = 0;
                                decHeadArrayAmt = Convert.ToDecimal(fRows["PayAmnt"].ToString());
                                // Current Month Days Diff
                                inDaysDiff = 0;
                                inDaysDiff = dtEndDate.Day - dtJobStartDate.Day + 1;
                                decCUnitDaySalary = decHeadArrayAmt / Convert.ToDecimal(lngTotalMonthDays);
                                decCUnitDaySalary = Math.Round(decCUnitDaySalary, 2);
                                decCUnitDaySalary = decCUnitDaySalary * Convert.ToDecimal(inDaysDiff);
                            }

                            decHeadArrayAmt = decPUnitDaySalary + decCUnitDaySalary;
                            decHeadArrayAmt = Math.Round(decHeadArrayAmt, 0);
                            fRows["PayAmnt"] = decHeadArrayAmt.ToString();
                            objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
                            decNetPayAmt = decNetPayAmt + decHeadArrayAmt;
                            continue;
                        }

                        if (IsDualMonth == false)
                        {
                            //Edited By Sulata 10.03.15

                            decHeadArrayAmt = 0;
                            decHeadArrayAmt = Convert.ToDecimal(fRows["PayAmnt"].ToString());
                            decHeadArrayAmt = decHeadArrayAmt / Convert.ToDecimal(lngTotalMonthDays);
                            decHeadArrayAmt = Math.Round(decHeadArrayAmt, 4);
                            decHeadArrayAmt = decHeadArrayAmt * Convert.ToDecimal(dRowMst["TWorkingDayHour"].ToString().Trim());
                            decHeadArrayAmt = Math.Round(decHeadArrayAmt, 0);
                            fRows["PayAmnt"] = decHeadArrayAmt.ToString();
                            objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
                            decNetPayAmt = decNetPayAmt + decHeadArrayAmt;
                        }
                    }
                }
                if (fRows["SalHeadID"].ToString().Trim() == "5")
                    decPfCont = decHeadArrayAmt;
            }
            dRowMst["PackageAmount"] = decNetPayAmt;
            objPayslip.Tables["dtPaySlipMst"].AcceptChanges();
        }
    }

    protected void FillMonthlyContractualPayment(DataRow dRowMst)
    {
        DataTable dtAttnRecord = new DataTable();
        DataTable dtEmpInfo = new DataTable();
        //int inStartMonth = Convert.ToDateTime().Month;
        DateTime dtStartDate = Convert.ToDateTime(dRowMst["StartDate"].ToString().Trim());
        DateTime dtEndDate = Convert.ToDateTime(dRowMst["EndDate"].ToString().Trim());
        DateTime dtJobStartDate = Convert.ToDateTime(dRowMst["JoiningDate"].ToString().Trim());

        DateTime dtJobEndDate = Convert.ToDateTime(dRowMst["SalEndDate"].ToString().Trim());

        DateTime dtSepDate = new DateTime();
        if (string.IsNullOrEmpty(dRowMst["SeparateDate"].ToString().Trim()) == false)
        {
            dtSepDate = Convert.ToDateTime(dRowMst["SeparateDate"].ToString().Trim());
            dtJobEndDate = Convert.ToDateTime(dRowMst["SeparateDate"].ToString().Trim());
        }

        DateTime dtMonthStartDate = Convert.ToDateTime(ddlYear.SelectedValue.Trim() + "/" + ddlMonth.SelectedValue.Trim() + "/01");
        int inMonthDays = Convert.ToInt32(dRowMst["MonthDays"].ToString().Trim());
        //,int inSMonth,int inEMonth


        // Dim rsEmpInfo As New ADODB.Recordset
        int EmpWeekEndID;

        DataRow nRow = objPayslip.dtPayslipPreparation.NewRow();
        nRow["PSBID"] = dRowMst["PSBookID"];
        nRow["EMPID"] = dRowMst["EmployeeID"];
        nRow["FullName"] = dRowMst["Empname"];
        nRow["JobTitleName"] = dRowMst["JobTitleName"];
        nRow["JobTitleId"] = "";
        nRow["DeptName"] = dRowMst["DeptName"];
        nRow["DeptId"] = "";

        //  'Update code for all kinds of Loan/Deposit
        //If PAYSLIP_ADVANCE_LOAN_DEDUCT_SALARY_HEAD_ID > 0 Then
        DataTable dtLoanType = new DataTable();
        dtLoanType = objPreMgr.GetLoanType();
        foreach (DataRow dLoanRow in dtLoanType.Rows)
        {
            this.AddLoanDeduction(dRowMst, dLoanRow["SHeadID"].ToString(), dLoanRow["LoanTypeID"].ToString());
        }
        dtLoanType.Rows.Clear();
        dtLoanType.Dispose();
        //End If

        // Salary Adjust
        this.AddSalaryAdjust(dRowMst, dRowMst["EmployeeID"].ToString().Trim());

        dRowMst["TotalDays"] = dRowMst["MonthDays"].ToString().Trim();
        dRowMst["SPTitle"] = dRowMst["SPTitle"].ToString().Trim();
        dRowMst.AcceptChanges();

        // Calculate Unit Day Salary
        //decimal decUnitDaySalary = 0;
        decimal decHeadArrayAmt = 0;
        decimal decNetPayAmt = 0;

        // Pay Slip Details Update
        DataRow[] foundRows = objPayslip.Tables["dtPaySlipDets"].Select("EmployeeID='" + dRowMst["EmployeeID"].ToString().Trim() + "'");
        //decimal decPfCont = 0;
        if (foundRows.Length > 0)
        {
            decNetPayAmt = 0;

            //**********This Salary Head are not Applicable for Partial Calculation**********            
            int[] strV = { 4, 5, 6, 7, 8, 9, 10, 11, 12, 14, 16, 17, 18 }; 
            foreach (DataRow fRows in foundRows)
            {
                bool IsExist = false;
                for (int i = 0; i < strV.Length; i++)
                {
                    if (fRows["SalHeadID"].ToString().Trim() == strV[i].ToString()) //Alico(+)
                    {
                        if (dRowMst["TWorkingDayHour"].ToString().Trim() != "0")
                        {
                            decHeadArrayAmt = Common.RoundDecimal(fRows["PayAmnt"].ToString(), 0);
                            fRows["PayAmnt"] = decHeadArrayAmt.ToString();
                            objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
                            decNetPayAmt = decNetPayAmt + decHeadArrayAmt;
                            decHeadArrayAmt = 0;
                        }
                        else
                        {
                            fRows["PayAmnt"] = "0";
                            objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
                        }

                        IsExist = true;
                        break;
                    }
                }

                if (IsExist == true)
                {
                    continue;
                }

                if (Common.FindInDataTable(dtGrossSalHead, fRows["SalHeadID"].ToString().Trim(), "SHeadID") == true)
                {
                    decHeadArrayAmt = 0;
                    decHeadArrayAmt = Convert.ToDecimal(fRows["PayAmnt"].ToString());
                    decHeadArrayAmt = decHeadArrayAmt / Convert.ToDecimal(dRowMst["MonthDays"].ToString().Trim());
                    decHeadArrayAmt = Math.Round(decHeadArrayAmt, 2);
                    decHeadArrayAmt = decHeadArrayAmt * Convert.ToDecimal(dRowMst["TWorkingDayHour"].ToString().Trim());
                    decHeadArrayAmt = Math.Round(decHeadArrayAmt, 0);
                    fRows["PayAmnt"] = decHeadArrayAmt.ToString();
                    objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
                    decNetPayAmt = decNetPayAmt + decHeadArrayAmt;
                    continue;
                }

                DataRow[] foundBfRow = dtBenefits.Select("EMPID='" + dRowMst["EmployeeID"].ToString().Trim() + "' AND SHEADID='" + fRows["SalHeadID"].ToString().Trim() + "'");
                if (foundBfRow.Length == 0)
                {
                    decHeadArrayAmt = 0;
                    decHeadArrayAmt = Convert.ToDecimal(fRows["PayAmnt"].ToString());
                    decHeadArrayAmt = decHeadArrayAmt / Convert.ToDecimal(dRowMst["MonthDays"].ToString().Trim());
                    decHeadArrayAmt = Math.Round(decHeadArrayAmt, 4);
                    decHeadArrayAmt = decHeadArrayAmt * Convert.ToDecimal(dRowMst["TWorkingDayHour"].ToString().Trim());
                    decHeadArrayAmt = Math.Round(decHeadArrayAmt, 0);
                    fRows["PayAmnt"] = decHeadArrayAmt.ToString();
                    objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
                    decNetPayAmt = decNetPayAmt + decHeadArrayAmt;
                }
            }

            dRowMst["PackageAmount"] = decNetPayAmt;
            objPayslip.Tables["dtPaySlipMst"].AcceptChanges();
        }
    }

    protected void AddLoanDeduction(DataRow dRowMst, string StrHeadID, string StrLoanTypeID)
    {
        Decimal LoanAmt = 0;
        Decimal LoanPayAmt = 0;
        Decimal LoanRefdAmt = 0;
        Decimal ApplicationID = 0;
        Decimal dblTempAmount = 0;
        Decimal dblSalHeadAmount = 0;
        Decimal FindLoanAmt = 0;
        string strAccName = "";
        long lngHeadNature = 0;
        string strAssAccNo = "";
        DataTable dtFind = new DataTable();
        DataTable dtLoan = new DataTable();

        // Select the Head for Salary Head
        dtFind = objPreMgr.GetLoanSalaryHead(StrHeadID);
        if (dtFind.Rows.Count == 0)
        {
            dtFind.Dispose();
            return;
        }
        else
        {
            strAccName = dtFind.Rows[0]["HEADNAME"].ToString();
            lngHeadNature = Convert.ToInt64(dtFind.Rows[0]["HEADNATURE"].ToString());
            strAssAccNo = dtFind.Rows[0]["ASSACCNUM"].ToString();
        }
        dtFind.Rows.Clear();
        dtFind.Dispose();

        // Select Emp. Salary Loan Master
        //dtFind = new DataTable();
        FindLoanAmt = objPreMgr.GetEmployeLoanDetails(dRowMst["EmployeeID"].ToString().Trim(), dRowMst["EndDate"].ToString().Trim(), StrLoanTypeID);
        if (FindLoanAmt == 0)
        {
            dtFind.Dispose();
            return;
        }

        //New Update for Loan Checking whether Loan has been already payment or Not.
        dtLoan = objPreMgr.GetLoanPaymentCheck(dRowMst["EmployeeID"].ToString().Trim(), dRowMst["EndDate"].ToString().Trim(), StrLoanTypeID);
        if (dtLoan.Rows.Count > 0)
        {
            ApplicationID = Convert.ToDecimal(dtLoan.Rows[0]["LOANAPPID"].ToString());
        }
        dtLoan.Rows.Clear();
        dtLoan.Dispose();

        dtLoan = new DataTable();
        dtLoan = objPreMgr.GetLoanAmountAndPFInterest(ApplicationID.ToString(), dRowMst["EmployeeID"].ToString().Trim(), StrLoanTypeID);
        if (dtLoan.Rows.Count > 0)
        {
            //Total Loan Taken Amount
            LoanAmt = Convert.ToDecimal(dtLoan.Rows[0]["LoanAmnt"].ToString())
                + Convert.ToDecimal(dtLoan.Rows[0]["LoanAmnt"].ToString())
                * Convert.ToDecimal(dtLoan.Rows[0]["PFInterest"].ToString())
                / 100;
            //ChkNull(RsLoan("LoanAmnt"), True) + ChkNull(RsLoan("LoanAmnt"), True) * ChkNull(RsLoan("PFInterest"), True) / 100
        }
        dtLoan.Rows.Clear();
        dtLoan.Dispose();

        dtLoan = new DataTable();
        dtLoan = objPreMgr.GetAppWiseLoanPayAmt(ApplicationID.ToString(), dRowMst["EmployeeID"].ToString().Trim(), StrLoanTypeID);
        if (dtLoan.Rows.Count > 0)
        {
            //Total Loan Payment from Salary Amount
            LoanPayAmt = FindLoanAmt;
        }
        dtLoan.Rows.Clear();
        dtLoan.Dispose();

        dtLoan = new DataTable();
        dtLoan = objPreMgr.GetAppWiseRefundAmt(ApplicationID.ToString(), dRowMst["EmployeeID"].ToString().Trim(), StrLoanTypeID);
        if (dtLoan.Rows.Count > 0)
        {
            //Total Loan Refund Amount
            LoanRefdAmt = Convert.ToDecimal(dtLoan.Rows[0]["ReFndAmt"].ToString());
        }
        dtLoan.Rows.Clear();
        dtLoan.Dispose();

        dblSalHeadAmount = Convert.ToDecimal(dtFind.Rows[0]["Amnt"].ToString());
        //If Loan Deduct amount is greater than Current Balance, then no Transation
        if (dblSalHeadAmount > (LoanAmt - LoanPayAmt - LoanRefdAmt))
            return;
        if (dblSalHeadAmount > 0)
        {
            // Update the Payslip Details table of Dataset
            DataRow[] foundRows = objPayslip.dtPaySlipDets.Select("PSBookID='" + dRowMst["PSBookID"].ToString() + "' AND EMPID='" + dRowMst["EmployeeID"].ToString().Trim() + "' AND SalHeadID='" + StrHeadID + "'");
            if (foundRows.Length > 0)
            {
                foundRows[0]["SalHeadTitle"] = strAccName;
                foundRows[0]["IsBasicSal"] = "N";
                foundRows[0]["PayAmnt"] = Convert.ToString((-1) * dblSalHeadAmount);
                foundRows[0]["IsDeducted"] = "Y";
                foundRows[0]["IsOtherPayment"] = "Y";
                foundRows[0]["IsAdvanceDeducttion"] = "Y";
                objPayslip.dtPaySlipDets.AcceptChanges();
            }
        }

        dtFind.Rows.Clear();
        dtFind.Dispose();
    }

    protected void AddLeaveDeduction(DataRow dRowMst, decimal DeductAmount, string strSheadID)
    {
        if (dtSalHead.Rows.Count == 0)
            return;
        DataRow[] foundRows = dtSalHead.Select("SHEADID='" + strSheadID + "'");
        if (foundRows.Length == 0)
            return;

        decimal dblUnitDeductionAmnt = 0;
        decimal lngTDayAbsent = 0;
        decimal dblDeductAmnt;

        if (DeductAmount < 0)
            return;

        dblDeductAmnt = DeductAmount;

        if (dblDeductAmnt != 0)
        {
            DataRow[] foundPSRows = objPayslip.dtPaySlipDets.Select("PSBookID='" + dRowMst["PSBookID"].ToString() + "' AND EMPID='" + dRowMst["EmployeeID"].ToString().Trim() + "' AND SalHeadID='" + strSheadID + "'");

            if (dRowMst["IsConvertCurrency"].ToString() == "Y")
            {
                if (foundPSRows.Length > 0)
                {
                    foundRows[0]["SalHeadTitle"] = foundRows[0]["HEADNAME"];
                    foundRows[0]["IsBasicSal"] = "N";
                    foundRows[0]["PayAmnt"] = Convert.ToString((-1) * dblDeductAmnt * Convert.ToDecimal(dRowMst["CurrencyConvAmnt"]));
                    foundRows[0]["IsDeducted"] = "Y";
                    foundRows[0]["IsOtherPayment"] = "Y";
                    objPayslip.dtPaySlipDets.AcceptChanges();
                }
            }
            else
            {
                if (foundPSRows.Length > 0)
                {
                    foundRows[0]["SalHeadTitle"] = foundRows[0]["HEADNAME"];
                    foundRows[0]["IsBasicSal"] = "N";
                    foundRows[0]["PayAmnt"] = Convert.ToString((-1) * dblDeductAmnt);
                    foundRows[0]["IsDeducted"] = "Y";
                    foundRows[0]["IsOtherPayment"] = "Y";
                    objPayslip.dtPaySlipDets.AcceptChanges();
                }
            }
        }
    }

    protected void AddCompanyFacility(DataRow dRowMst, string StrSalPackID, string StrStartDate, string strEndDate, string strEmpID)
    {
        decimal dblAmnt;
        decimal dblBasicSal = 0;
        decimal dblGrossSalary = 0;
        decimal ExtraTime = 0;
        int SalHeadID = 0;

        dblAmnt = 0;
        dblGrossSalary = 0;
        dblBasicSal = 0;

        dtEmpSalPackMst = new DataTable();
        dtEmpSalPackMst = objPreMgr.GetPackageIDAndOTAmt(StrSalPackID);
        if (dtEmpSalPackMst.Rows.Count > 0)
        {
            dtCompFacility = new DataTable();
            dtCompFacility = objPreMgr.GetCompanyFacilityDetls(dtEmpSalPackMst.Rows[0]["PackageID"].ToString());
            foreach (DataRow dCFRow in dtCompFacility.Rows)
            {
                if (dtSalHead.Rows.Count == 0)
                    return;
                DataRow[] foundHeadRows = dtSalHead.Select("SHEADID='" + dCFRow["SheadId"].ToString() + "' ");
                if (foundHeadRows.Length == 0)
                    return;
                dblAmnt = 0;
                SalHeadID = Convert.ToInt32(dCFRow["SheadId"].ToString());
                // 1
                if (dCFRow["PaymentType"].ToString() == "0")
                {
                    //2
                    if (dCFRow["CalRules"].ToString() == "0")  //IF CALCULATION RULES IS EVERY MONTH
                    {
                        //3
                        if (dCFRow["IsInPercent"].ToString() == "Y")
                        {
                            //4
                            if (dCFRow["PercentSalHead"].ToString() == "B")
                            {
                                dblAmnt = objPreMgr.GetBasicSalary(StrSalPackID) *
                                    Convert.ToDecimal(dRowMst["CurrencyConvAmnt"]) *
                                    Convert.ToDecimal(dCFRow["PayAmt"]) / 100;
                            }
                            else if (dCFRow["PercentSalHead"].ToString() == "G")
                            {
                                dblAmnt = objPreMgr.GetGrossSalary(StrSalPackID) *
                                      Convert.ToDecimal(dRowMst["CurrencyConvAmnt"]) *
                                      Convert.ToDecimal(dCFRow["PayAmt"]) / 100;
                            }// end of 4

                        }
                        else
                        {
                            dblAmnt = Convert.ToDecimal(dCFRow["PayAmt"]);
                        }// end of 3
                    }
                    else if (dCFRow["CalRules"].ToString() == "1") //IF CALCULATION RULES IS EVERY DAY
                    {

                    }
                    else if (dCFRow["CalRules"].ToString() == "2") //IF CALCULATION RULES IS EVERY WORKING DAY
                    {

                    }
                    else if (dCFRow["CalRules"].ToString() == "3") //IF CALCULATION RULES IS EVERY HOLIDAY DAY
                    {

                    }
                    else if (dCFRow["CalRules"].ToString() == "4") //IF CALCULATION RULES IS EVERY WEEKEND DAY
                    {

                    }
                    else if (dCFRow["CalRules"].ToString() == "5") //IF CALCULATION RULES IS EVERY WEEKEND DAY AND HOLIDAY
                    {

                    }
                    else if (dCFRow["CalRules"].ToString() == "6") //IF CALCULATION RULES IS EVERY OT DAYS
                    {

                    }
                    else if (dCFRow["CalRules"].ToString() == "7") //IF CALCULATION RULES IS FOR TRANSPORTATION
                    {

                    }
                    else if (dCFRow["CalRules"].ToString() == "8") //IF CALCULATION RULES IS EVERY DAYS AND TIME LIMIT
                    {

                    }
                    else if (dCFRow["CalRules"].ToString() == "5") //IF CALCULATION RULES IS EVERY WEEKEND DAY AND HOLIDAY
                    {

                    }// end of 2
                }// end of 1
                if (dblAmnt != 0)
                {
                    DataRow[] foundPSRows = objPayslip.dtPaySlipDets.Select("PSBookID='" + dRowMst["PSBookID"].ToString() + "' AND EmployeeID='" + dRowMst["EmployeeID"].ToString().Trim() + "' AND SalHeadID='" + SalHeadID.ToString() + "'");

                    if (dRowMst["IsConvertCurrency"].ToString() == "Y")
                    {
                        if (foundPSRows.Length > 0)
                        {
                            foundPSRows[0]["SalHeadTitle"] = foundHeadRows[0]["HEADNAME"];
                            foundPSRows[0]["IsBasicSal"] = "N";
                            foundPSRows[0]["PayAmnt"] = Convert.ToString(dblAmnt * Convert.ToDecimal(dRowMst["CurrencyConvAmnt"]));
                            foundPSRows[0]["IsDeducted"] = "N";
                            foundPSRows[0]["IsOtherPayment"] = "Y";
                            objPayslip.dtPaySlipDets.AcceptChanges();
                        }
                        else
                        {
                            this.AddSalPackDets(dRowMst["PSBookID"].ToString(), dRowMst["EmployeeID"].ToString().Trim(), SalHeadID.ToString(),
                                                foundHeadRows[0]["HEADNAME"].ToString(), "N", Convert.ToString(dblAmnt * Convert.ToDecimal(dRowMst["CurrencyConvAmnt"])),
                                                "N", "Y", "N", "N", "N", "0.00", "N", "N", "N", "0", "N", "N", "N");
                        }
                    }
                    else
                    {

                        if (foundPSRows.Length > 0)
                        {
                            foundPSRows[0]["SalHeadTitle"] = foundHeadRows[0]["HEADNAME"];
                            foundPSRows[0]["IsBasicSal"] = "N";
                            foundPSRows[0]["PayAmnt"] = Convert.ToString(dblAmnt);
                            foundPSRows[0]["IsDeducted"] = "N";
                            foundPSRows[0]["IsOtherPayment"] = "Y";
                            objPayslip.dtPaySlipDets.AcceptChanges();
                        }
                        else
                        {
                            this.AddSalPackDets(dRowMst["PSBookID"].ToString(), dRowMst["EmployeeID"].ToString().Trim(), SalHeadID.ToString(),
                                                foundHeadRows[0]["HEADNAME"].ToString(), "N", Convert.ToString(dblAmnt),
                                                "N", "Y", "N", "N", "N", "0.00", "N", "N", "N", "0", "N", "N", "N");
                        }
                    }
                }
            }

            dtCompFacility.Rows.Clear();
            dtCompFacility.Dispose();
        }
        dtEmpSalPackMst.Rows.Clear();
        dtEmpSalPackMst.Dispose();
    }

    public void AddSalaryAdjust(DataRow dRowMst, String strEmpID)
    {
        Decimal dblAmnt = 0;
        int SalHeadID = 0;
        Decimal dblAdjustID = 0;
        int Fid = 0;

        DataTable dtSalAdjustMst = new DataTable();
        DataTable dtSalAdjustDets;
        dtSalAdjustMst = objPreMgr.GetSalAdjustMst(dRowMst["StartDate"].ToString().Trim(), dRowMst["EndDate"].ToString().Trim());
        if (dtSalAdjustMst.Rows.Count == 0)
            return;

        foreach (DataRow dSalAdjMstRow in dtSalAdjustMst.Rows)
        {
            dtSalAdjustDets = new DataTable();
            dtSalAdjustDets = objPreMgr.GetSalAdjustDets(strEmpID, dSalAdjMstRow["AdjustID"].ToString().Trim(), dSalAdjMstRow["SHeadID"].ToString().Trim());
            if (dtSalAdjustDets.Rows.Count > 0)
            {
                dblAdjustID = Convert.ToDecimal(dSalAdjMstRow["AdjustID"].ToString().Trim());
                DataRow[] foundPSRows = objPayslip.dtPaySlipDets.Select("PSBookID='" + dRowMst["PSBookID"].ToString() + "' AND EMPID='" + dRowMst["EmployeeID"].ToString().Trim() + "' AND SalHeadID='" + dSalAdjMstRow["SHeadID"].ToString().Trim() + "'");
                if (foundPSRows.Length > 0)
                {
                    foundPSRows[0]["SalHeadTitle"] = dtSalAdjustDets.Rows[0]["HEADNAME"];
                    foundPSRows[0]["IsBasicSal"] = "N";
                    foundPSRows[0]["PayAmnt"] = Convert.ToString(Convert.ToDecimal(dtSalAdjustDets.Rows[0]["Amount"]) * Convert.ToDecimal(dtSalAdjustDets.Rows[0]["HeadNature"]));
                    foundPSRows[0]["IsDeducted"] = "N";
                    foundPSRows[0]["IsOtherPayment"] = "Y";
                    objPayslip.dtPaySlipDets.AcceptChanges();
                }
                objPreMgr.UpdateAdjustDet(dRowMst["PSBookID"].ToString(), strEmpID, dSalAdjMstRow["AdjustID"].ToString().Trim());
            }
            dtSalAdjustDets.Rows.Clear();
            dtSalAdjustDets.Dispose();
        }
        dtSalAdjustMst.Rows.Clear();
        dtSalAdjustMst.Dispose();
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        this.OpenRecord();
        //if (chkBonus.Checked == false)
        this.GeneratePaySlip();
        //else
        //    this.GenerateBonus();

        grPayslipMst.DataSource = objPayslip.Tables["dtPaySlipMst"];
        grPayslipMst.DataBind();

        lblRecordCount.Text = grPayslipMst.Rows.Count.ToString();
        this.WritePaySlipDetailsToXmlFile();
    }

    protected void WritePaySlipDetailsToXmlFile()
    {
        string FolderPath = ConfigurationManager.AppSettings["XMLFilePath"];
        string FilePath = Server.MapPath(FolderPath + "/" + "XMLPaySlipDets.xml");
        FileInfo File = new FileInfo(FilePath);
        if (File.Exists == true)
        {
            File.Delete();
        }
        objPayslip.Tables["dtPaySlipDets"].WriteXml(FilePath);
    }

    protected void grPayslipMst_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        //string strPreYrLv = "";
        switch (_commandName)
        {
            case ("DoubleClick"):
                //Open New Window
                StringBuilder sb = new StringBuilder();
                string strURL = "PaySlipDetails.aspx?params=" + grPayslipMst.SelectedRow.Cells[1].Text.Trim() + ","
                    + grPayslipMst.SelectedRow.Cells[2].Text.Trim() + ","
                    // + grPayslipMst.SelectedRow.Cells[3].Text.Trim() + ","
                    + grPayslipMst.SelectedRow.Cells[14].Text.Trim();
                sb.Append("<script>");
                //sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
                sb.Append("window.open('" + strURL + "', '', '');");
                sb.Append("</script>");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                         sb.ToString(), false);
                ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
                break;
        }
    }

    // Validate and Save
    protected void ValidateAndSave()
    {
        if (grPayslipMst.Rows.Count == 0)
        {
            lblMsg.Text = "There is no data to post";
            return;
        }
        this.SaveData();
    }

    // Save Data
    protected void SaveData()
    {
        try
        {
            string FolderPath = ConfigurationManager.AppSettings["XMLFilePath"];
            string FilePath = Server.MapPath(FolderPath + "/" + "XMLPaySlipDets.xml");
            FileInfo File = new FileInfo(FilePath);
            if (File.Exists == true)
            {
                objPayslip.Tables["dtPaySlipDets"].ReadXml(FilePath);
            }
            strPayDurEndDate = ddlYear.SelectedValue.ToString() + "/" + ddlMonth.SelectedValue.ToString() + "/" + Common.GetMonthDay(Convert.ToInt32(ddlMonth.SelectedValue.ToString()), ddlYear.SelectedValue.ToString());
            strPayDurStartDate = ddlYear.SelectedValue.ToString() + "/" + ddlMonth.SelectedValue.ToString() + "/" + "1";

            objPreMgr.InsertPSBData(grPayslipMst, objPayslip.Tables["dtPaySlipDets"], Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()), "N",
                strPayDurStartDate, strPayDurEndDate, Common.ReturnDate(txtIssueDate.Text.Trim()),
                ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim(), ddlFiscalYear.SelectedValue.ToString(), "S", "1", txtPercentage.Text.Trim(),
                "1", ddlFiscalYearPF.SelectedValue.ToString(), ddlFiscalYearTax.SelectedValue.ToString());
            lblMsg.Text = "Salary Prepared Successfully";
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.ValidateAndSave();

        if (CheckBox1.Checked == true)
        {
            if (grPayslipMst.Rows.Count > 0)
            {
                //this.SendEmail();
            }
            else
            {
                lblMsg.Text = "No record found for Verify.";
            }
        }
    }

    private void SendEmail()
    {
        MasterTablesManager MasMgr = new MasterTablesManager();
        MailManagerSmtpClient objMail = new MailManagerSmtpClient();

        DataTable dt = MasMgr.GetEmailNotification();
        
        string strRetText = "";
        string strToAddr = dt.Rows[0]["Verify"].ToString().Trim();
        string strSubject = "Salary of " + ddlMonth.SelectedItem.ToString().Trim() + " has been waiting for your verification";
        string strBody = "Hi,<br />The salary has been prepared by HR. Please verify and forword to Dir finance & Admin.";
        string strFromAddr = Session["EMAILID"].ToString().Trim();

        strRetText = objMail.PayslipEmail(strFromAddr, strToAddr, strSubject, strBody, "");

        if (strRetText == "Y")
            lblMsg.Text = lblMsg.Text + "Email has been sent to Verify by Finance.";
        else
            lblMsg.Text = lblMsg.Text + "Email sending failed.";
    }

    protected void ddlGeneratefor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.FillGenerateDropDownList();
    }

    #region PF Loan and PF Loan Interst
    private decimal GetPFLoanAmt(DataRow[] foundPFLLRow, DataRow[] foundPFLRRow, DataRow[] foundPFLoanRow)
    {
        decimal dclCLLAmount = 0;
        decimal dclCashPay = 0;
        decimal dclRepay = 0;
        decimal dclSalHeadAmount = 0;
        decimal dclCMLoanAmt = 0;

        if (foundPFLLRow.Length > 0)
        {
            dclCLLAmount = Common.RoundDecimal(foundPFLLRow[0]["CLLOAN"].ToString().Trim(), 0);
            dclCMLoanAmt = Common.RoundDecimal(foundPFLLRow[0]["CMLOANAMT"].ToString().Trim(), 0);
        }
        if (foundPFLRRow.Length > 0)
        {
            foreach (DataRow dCRow in foundPFLRRow)
            {
                if (dCRow["ADJTYPE"].ToString().Trim() == "Deduction")
                {
                    dclRepay = Common.RoundDecimal(dCRow["ADJAMOUNT"].ToString().Trim(), 0);
                }
                else if (dCRow["ADJTYPE"].ToString().Trim() == "Cash Pay")
                {
                    dclCashPay = Common.RoundDecimal(dCRow["ADJAMOUNT"].ToString().Trim(), 0);
                }
            }
            if (dclRepay == 0)
            {
                if (foundPFLoanRow.Length > 0)
                {
                    dclSalHeadAmount = Common.RoundDecimal(foundPFLoanRow[0]["MonthlyRepay"].ToString().Trim(), 0);
                }
                else if (foundPFLLRow.Length > 0)
                {
                    dclSalHeadAmount = Common.RoundDecimal(foundPFLLRow[0]["CMREPAY"].ToString().Trim(), 0);
                }
                else
                {
                    dclSalHeadAmount = 0;
                }
            }
            else
            {
                dclSalHeadAmount = dclRepay;
            }
        }
        else
        {
            if (foundPFLoanRow.Length > 0)
            {
                dclSalHeadAmount = Common.RoundDecimal(foundPFLoanRow[0]["MonthlyRepay"].ToString().Trim(), 0);
                dclCMLoanAmt = Common.RoundDecimal(foundPFLoanRow[0]["LOANAMT"].ToString().Trim(), 0);
            }
            else if (foundPFLLRow.Length > 0)
            {
                dclSalHeadAmount = Common.RoundDecimal(foundPFLLRow[0]["CMREPAY"].ToString().Trim(), 0);
            }
            else
            {
                dclSalHeadAmount = 0;
            }
        }
        //  Validate with Closing Amount;
        if ((dclCLLAmount == 0) && (dclCMLoanAmt == 0))
        {
            dclSalHeadAmount = 0;
        }

        if (dclCLLAmount > 0)
        {
            if (dclCLLAmount < dclSalHeadAmount)
                dclSalHeadAmount = dclCLLAmount;
        }
        if (dclCashPay > 0)
        {
            if (dclCLLAmount == dclCashPay)
            {
                if (foundPFLoanRow.Length == 0)
                    dclSalHeadAmount = 0;
            }
            else if (dclCLLAmount < dclCashPay)
            {
                decimal dclInterest = this.GetPFLoanInterestAmt(foundPFLLRow, foundPFLRRow, foundPFLoanRow);
                if (dclCLLAmount + dclInterest == dclCashPay)
                    dclSalHeadAmount = 0;
            }

        }
        dclSalHeadAmount = dclSalHeadAmount * -1;
        return dclSalHeadAmount;
    }

    private decimal GetPFLoanInterestAmt(DataRow[] foundPFLLRow, DataRow[] foundPFLRRow, DataRow[] foundPFLoanRow)
    {
        decimal dclCLLAmount = 0;
        decimal dclCashPay = 0;
        decimal dclSalHeadAmount = 0;
        decimal dclPercent = 0;
        decimal dclOPLoan = 0;
        decimal dclCMLoanAmt = 0;
        if (foundPFLLRow.Length > 0)
        {
            dclCLLAmount = Common.RoundDecimal(foundPFLLRow[0]["CLLOAN"].ToString().Trim(), 0);
            dclOPLoan = Common.RoundDecimal(foundPFLLRow[0]["OPLOAN"].ToString().Trim(), 0);
            dclCMLoanAmt = Common.RoundDecimal(foundPFLLRow[0]["CMLOANAMT"].ToString().Trim(), 0);

            if ((dclOPLoan > 0) && (dclCMLoanAmt > 0))
            {
                dclOPLoan = (dclOPLoan - Common.RoundDecimal(foundPFLLRow[0]["CMCASH"].ToString().Trim(), 0)) + (dclCMLoanAmt - Common.RoundDecimal(foundPFLLRow[0]["CMCASH"].ToString().Trim(), 0));
            }
            if (string.IsNullOrEmpty(foundPFLLRow[0]["CMINTEREST"].ToString().Trim()) == false)
            {
                if (Common.RoundDecimal(foundPFLLRow[0]["CMINTEREST"].ToString().Trim(), 0) > 0)
                {
                    if (dclOPLoan > 0)
                    {
                        dclPercent = (Common.RoundDecimal(foundPFLLRow[0]["CMINTEREST"].ToString().Trim(), 0) * 100 * 12) / dclOPLoan;
                    }
                    else
                    {
                        if (dclCMLoanAmt > 0)
                            dclPercent = (Common.RoundDecimal(foundPFLLRow[0]["CMINTEREST"].ToString().Trim(), 0) * 100 * 12) / dclCMLoanAmt;
                        else
                            dclPercent = (Common.RoundDecimal(foundPFLLRow[0]["CMINTEREST"].ToString().Trim(), 0) * 100 * 12) / dclCLLAmount;
                    }
                }
            }
        }
        if (foundPFLRRow.Length > 0)
        {
            foreach (DataRow dCRow in foundPFLRRow)
            {
                if (dCRow["ADJTYPE"].ToString().Trim() == "Cash Pay")
                {
                    dclCashPay = Common.RoundDecimal(dCRow["ADJAMOUNT"].ToString().Trim(), 0);
                }
            }
            dclSalHeadAmount = dclCLLAmount - dclCashPay;
        }
        else
        {

            dclSalHeadAmount = dclCLLAmount;
        }
        if (foundPFLoanRow.Length > 0)
        {
            dclPercent = Common.RoundDecimal(foundPFLoanRow[0]["LoanRate"].ToString().Trim(), 2);
            dclSalHeadAmount = Common.RoundDecimal(foundPFLoanRow[0]["LoanAmt"].ToString().Trim(), 2);
        }

        dclPercent = Common.RoundDecimal(dclPercent.ToString(), 2);

        // Get Percent Amount;
        if (dclSalHeadAmount > 0)
        {
            dclSalHeadAmount = (dclSalHeadAmount * dclPercent / 100) / 12;
        }
        dclSalHeadAmount = dclSalHeadAmount * -1;
        dclSalHeadAmount = Common.RoundDecimal5T1(dclSalHeadAmount.ToString(), 0);
        return dclSalHeadAmount;
    }
    #endregion

    #region Only Bonus Preparation

    protected void GenerateBonus()
    {
        dtEmpInfo = new DataTable();
        string strDateTo = "";
        long lngPayID = 0;
        long lngPayBookID = 0;
        bool IsIrregular = false;
        decimal dclSalHeadAmount = 0;
        
        strDateTo = ddlYear.SelectedValue.ToString() + "/" + ddlMonth.SelectedValue.ToString() + "/" + Common.GetMonthDay(Convert.ToInt32(ddlMonth.SelectedValue.ToString()), ddlYear.SelectedValue.ToString());
        // Payroll General Policy for validty
        if (objPreMgr.IsPayrollPeriodValid(strDateTo) == false)
        {
            lblMsg.Text = "Payroll validity period is over. Please renew it.";
            return;
        }

        lngPayBookID = Convert.ToInt64(Common.getMaxId("PaySlipBook", "PSBID"));
        dtEmpInfo = objPreMgr.GetEmployeeBonusData(ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim());
        DataTable dtRevData = objPreMgr.GetEmployeeRevStampData("9");
        if (dtEmpInfo.Rows.Count == 0)
        {
            lblMsg.Text = "No Record Found...";
            return;
        }

        int inMonthDays = 0;
        TimeSpan tsMD;
        string strPayStartDate = "";
        string strPayEndDate = "";
        DataRow[] fRevRows = null;
        foreach (DataRow dEmpRow in dtEmpInfo.Rows)
        {
            inMonthDays = 0;
            IsIrregular = false;

            strPayStartDate = ddlYear.SelectedValue.ToString() + "/" + ddlMonth.SelectedValue.ToString() + "/" + Common.GetMonthDay(Convert.ToInt32(ddlMonth.SelectedValue.ToString()), ddlYear.SelectedValue.ToString());
            strPayEndDate = ddlYear.SelectedValue.ToString() + "/" + ddlMonth.SelectedValue.ToString() + "/" + "01";
            // tsMD = PayEndDate - PayStartDate;
            //inMonthDays = tsMD.Days + 1;

            lngPayID = 0;

            lngPayID = lngPayID + 1;
            if (string.IsNullOrEmpty(dEmpRow["SALARYPAKID"].ToString()) == false)
            {
                if (dEmpRow["SALDUR"].ToString().Trim() != "0")
                    IsIrregular = true;
                fRevRows = dtRevData.Select("SALPAKID =" + dEmpRow["SALARYPAKID"].ToString().Trim());
                if (fRevRows.Length > 0)
                {
                    dclSalHeadAmount = Convert.ToDecimal(fRevRows[0]["TOTAMNT"].ToString().Trim());
                    this.AddSalPackDets(lngPayBookID.ToString(), dEmpRow["EMPID"].ToString().Trim(), fRevRows[0]["SHEADID"].ToString(),
                                    fRevRows[0]["HEADNAME"].ToString(), "N", dclSalHeadAmount.ToString(),
                                    fRevRows[0]["HEADNATURE"].ToString() == "1" ? "N" : "Y", "N", "N", "N", "N", "0.00",
                                    "N", "N", "N", "0", "N", "N", "N");
                }
                else
                {
                    dclSalHeadAmount = 0;
                }

                dclSalHeadAmount = 0;
                fRevRows = null;

                dclSalHeadAmount = Convert.ToDecimal(dEmpRow["TOTALSALARY"].ToString());
                this.AddSalPackDets(lngPayBookID.ToString(), dEmpRow["EMPID"].ToString().Trim(), dEmpRow["SHEADID"].ToString(),
                                    dEmpRow["HEADNAME"].ToString(), "N", dclSalHeadAmount.ToString(),
                                    dEmpRow["HEADNATURE"].ToString() == "1" ? "N" : "Y", "N", "N", "N", "N", "0.00",
                                    "N", "N", "N", "0", "N", "N", "N");

                // Insert Data into Salary Pack master Table of Dataset
                this.AddSalPackMst(lngPayBookID.ToString(), lngPayID.ToString(), dEmpRow["EMPID"].ToString().Trim(), dEmpRow["FULLNAME"].ToString().Trim(),
                    dEmpRow["JobTitleName"].ToString().Trim(), strPayStartDate, strPayEndDate, "P", dEmpRow["SALARYPAKID"].ToString().Trim(), dEmpRow["PAYTYPE"].ToString(),
                    objPayslip.dtPaySlipDets, "N", dclSalHeadAmount.ToString(), dEmpRow["SALDUR"].ToString().Trim(), dEmpRow["DEPTNAME"].ToString(), dEmpRow["LATECOUNT"].ToString(),
                    dEmpRow["LATESALCOUNT"].ToString(), dEmpRow["LATESALHEAD"].ToString(), "N", "N", dEmpRow["TOTALGROSSSAL"].ToString(),
                    IsIrregular == false ? "N" : "Y", dEmpRow["OTAmt"].ToString(), dEmpRow["IsInPercent"].ToString(), dEmpRow["SHeadID"].ToString(),
                    "Y", dEmpRow["VID"].ToString(), dEmpRow["WillConvert"].ToString(), dEmpRow["CurrId"].ToString(),
                    dEmpRow["ConvrsAmt"].ToString(), dEmpRow["ArearAmnt"].ToString(), dEmpRow["IsArearPaid"].ToString(), "0",
                    "0", "0.00", dEmpRow["SPTitle"].ToString().Trim(), strPayStartDate, strPayStartDate,
                    dEmpRow["SALDUR"].ToString().Trim(), dEmpRow["BANKCODE"].ToString().Trim(), dEmpRow["BRANCHCODE"].ToString().Trim(),
                    dEmpRow["BankAccNo"].ToString().Trim(), dEmpRow["DEPTID"].ToString().Trim(), dEmpRow["DESGID"].ToString().Trim(),
                    dEmpRow["EMPTYPEID"].ToString().Trim(), dEmpRow["PLANACCLINE"].ToString().Trim(), dEmpRow["JoiningDate"].ToString().Trim(), dEmpRow["LeavingDate"].ToString().Trim(),
                    "1", dEmpRow["DivisionId"].ToString().Trim(), dEmpRow["SalLocId"].ToString().Trim(), dEmpRow["ProbationPeriod"].ToString().Trim(), dEmpRow["ConfirmationDate"].ToString().Trim());
            }
        }
    }

    protected void FillMonthlyNotRegularPaymentForPF(DataRow dRowMst)
    {
        DataTable dtAttnRecord = new DataTable();
        DataTable dtEmpInfo = new DataTable();
        //int inStartMonth = Convert.ToDateTime().Month;
        DateTime dtStartDate = Convert.ToDateTime(dRowMst["StartDate"].ToString().Trim());
        DateTime dtEndDate = Convert.ToDateTime(dRowMst["EndDate"].ToString().Trim());
        DateTime dtJobStartDate = Convert.ToDateTime(dRowMst["ConfirmationDate"].ToString().Trim());

        DateTime dtJobEndDate = Convert.ToDateTime(dRowMst["SalEndDate"].ToString().Trim());

        DateTime dtSepDate = new DateTime();
        if (string.IsNullOrEmpty(dRowMst["SeparateDate"].ToString().Trim()) == false)
        {
            dtSepDate = Convert.ToDateTime(dRowMst["SeparateDate"].ToString().Trim());
            dtJobEndDate = Convert.ToDateTime(dRowMst["SeparateDate"].ToString().Trim());
        }

        DateTime dtMonthStartDate = Convert.ToDateTime(ddlYear.SelectedValue.Trim() + "/" + ddlMonth.SelectedValue.Trim() + "/01");
        int inMonthDays = Convert.ToInt32(dRowMst["MonthDays"].ToString().Trim());

        // Calculate Unit Day Salary
        decimal decUnitDaySalary = 0;
        decimal decHeadArrayAmt = 0;
        decimal decNetPayAmt = 0;
        decimal decBasicSalAmt = 0;
        string strPrevMonth = "";
        //string strPrevYear = "";
        //long lngTotalMonthDays = 0;
        int inDaysDiff = 0;
        //if (Convert.ToInt64(dRowMst["MonthDays"].ToString().Trim()) < Convert.ToInt64(dRowMst["TWorkingDayHour"].ToString().Trim()))
        //{
        //    strPrevMonth = Common.GetPreviousMonth(Convert.ToDateTime(dRowMst["SalEndDate"].ToString().Trim()).Month.ToString());
        //    if (strPrevMonth == "12")
        //        strPrevYear = Convert.ToString(Convert.ToDateTime(dRowMst["SalEndDate"].ToString().Trim()).Year - 1);
        //    lngTotalMonthDays = Common.GetMonthDay(Convert.ToInt32(strPrevMonth), strPrevYear);
        //}
        //else
        //    lngTotalMonthDays = Convert.ToInt64(dRowMst["MonthDays"].ToString().Trim());

        // Pay Slip Details Update for PF
      DataRow[] foundRows = objPayslip.Tables["dtPaySlipDets"].Select("EmployeeID='" + dRowMst["EmployeeID"].ToString().Trim() + "'");        
        if (foundRows.Length > 0)
        {
            decNetPayAmt = 0;
            //**********This Salary Head are not Applicable for Partial Calculation of PF depend on confirmation date**********            
            foreach (DataRow fRows in foundRows)
            {                
                int[] strV = new int[] { 8 };
                for (int i = 0; i < strV.Length; i++)
                {
                    if (fRows["SalHeadID"].ToString().Trim() == "1")
                    {
                        //decBasicSalAmt = (Convert.ToDecimal(fRows["PayAmnt"].ToString()) * 30) / 100;
                        decBasicSalAmt = Convert.ToDecimal(fRows["PayAmnt"].ToString()) /30;     
                    }
                    if (fRows["SalHeadID"].ToString().Trim() == strV[i].ToString()) 
                    {                       
                        if (dRowMst["TWorkingDayHour"].ToString().Trim() != "0")
                        {                            
                            //long lngDaysDiff = Convert.ToInt64(dRowMst["TWorkingDayHour"].ToString().Trim()) - Convert.ToInt64(dRowMst["MonthDays"].ToString().Trim());
                            if (Convert.ToInt32(dtJobEndDate.Day) >= 31)
                                inDaysDiff = dtJobEndDate.Day - dtJobStartDate.Day;
                            else
                                inDaysDiff = dtJobEndDate.Day - dtJobStartDate.Day + 1;
                            decHeadArrayAmt = 0;
                            decBasicSalAmt = decBasicSalAmt * inDaysDiff;
                            decHeadArrayAmt = decBasicSalAmt / 10;//Convert.ToDecimal(fRows["PayAmnt"].ToString());                            
                            decHeadArrayAmt = Math.Round(decHeadArrayAmt, 0);
                            fRows["PayAmnt"] = "-" + decHeadArrayAmt.ToString();
                            objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
                            decNetPayAmt = decNetPayAmt + decHeadArrayAmt;
                            decHeadArrayAmt = 0;
                        }
                        else
                        {
                            fRows["PayAmnt"] = "0";
                            objPayslip.Tables["dtPaySlipDets"].AcceptChanges();
                        }                       
                    }
                }
                #region Comment                
                #endregion
            }            
            objPayslip.Tables["dtPaySlipMst"].AcceptChanges();
        }
    }
    #endregion

    protected Decimal GetArrearHeadAmount(DataTable dt, Int32 inSheadID, string strEmpID)
    {
        if (dt.Rows.Count == 0)
            return 0;

        DataRow[] foundArrRows = dt.Select("EMPID='" + strEmpID + "' AND SHEADID=" + inSheadID);
        decimal dclPayAmt = 0;
        if (foundArrRows.Length > 0)
        {
            foreach (DataRow fRow in foundArrRows)
            {
                dclPayAmt = dclPayAmt + Common.RoundDecimal(fRow["PAYAMT"].ToString(), 0);
            }
            return dclPayAmt;
        }
        else
            return 0;
    }
    protected void grPayslipMst_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
