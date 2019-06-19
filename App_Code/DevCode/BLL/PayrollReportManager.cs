using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for PayrollReportManager
/// </summary>
public class PayrollReportManager
{
    DBConnector objDC = new DBConnector();

    public DataTable GetPayrollData(string strGenerateFor, string strGeneratValue, string strMonth, string strYear, string strBranchCode)
    {
        if (objDC.ds.Tables["GetPayrollData"] != null)
        {
            objDC.ds.Tables["GetPayrollData"].Rows.Clear();
            objDC.ds.Tables["GetPayrollData"].Dispose();
        }
        SqlCommand cmd = new SqlCommand("proc_payroll_select_PayrollSummaryReport");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_GenerateFor = cmd.Parameters.Add("GenerateFor", SqlDbType.Char);
        p_GenerateFor.Direction = ParameterDirection.Input;
        p_GenerateFor.Value = strGenerateFor;

        SqlParameter p_GenerateValue = cmd.Parameters.Add("GenerateValue", SqlDbType.Char);
        p_GenerateValue.Direction = ParameterDirection.Input;
        p_GenerateValue.Value = strGeneratValue;

        SqlParameter p_BranchCode = cmd.Parameters.Add("BranchCode", SqlDbType.Char);
        p_BranchCode.Direction = ParameterDirection.Input;
        p_BranchCode.Value = strBranchCode;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        objDC.CreateDSFromProc(cmd, "GetPayrollData");
        return objDC.ds.Tables["GetPayrollData"];
    }


    public DataTable GetBonusAllowanceYearly(string strTaxFiscalYr)
    {
        SqlCommand cmd = new SqlCommand("PROC_PAYROLL_SELECT_BONUSALLOWFORYTD_YEARLY");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_TaxFiscalYr = cmd.Parameters.Add("TaxFiscalYrId", SqlDbType.BigInt);
        p_TaxFiscalYr.Direction = ParameterDirection.Input;
        p_TaxFiscalYr.Value = strTaxFiscalYr;

        objDC.CreateDSFromProc(cmd, "GetBonusAllowanceYearly");
        return objDC.ds.Tables["GetBonusAllowanceYearly"];
    }

    public string GetPayrollRemarks(string strEmpId, string strMonth, string strYear)
    {
        string strSQL = "SELECT REMARKS FROM PAYSLIPMST WHERE EMPID=@EMPID AND VMONTH=@VMONTH AND VYEAR=@VYEAR AND SALARYTYPE='S'";
        SqlCommand cmd = new SqlCommand(strSQL);

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpId;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        return objDC.GetScalarVal(cmd);

    }

    public string GetPayrollRemarksForPayslip(string strEmpId, string strMonth, string strYear, string strSalType)
    {
        
        string strSQL = "SELECT REMARKS FROM PAYSLIPMST WHERE EMPID=@EMPID AND VMONTH=@VMONTH AND VYEAR=@VYEAR "
                        + " AND SALARYTYPE=@SALARYTYPE AND ISPRINTTOPAYSLIP='Y'";
        SqlCommand cmd = new SqlCommand(strSQL);

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpId;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_SALARYTYPE = cmd.Parameters.Add("SALARYTYPE", SqlDbType.Char);
        p_SALARYTYPE.Direction = ParameterDirection.Input;
        p_SALARYTYPE.Value = strSalType;

        return objDC.GetScalarVal(cmd);

    }

    public string GetPayrollRemarksP2P(string strEmpId, string strMonth, string strYear)
    {
        string strSQL = "SELECT ISPRINTTOPAYSLIP FROM PAYSLIPMST WHERE EMPID=@EMPID AND VMONTH=@VMONTH AND VYEAR=@VYEAR AND SALARYTYPE='S'";
        SqlCommand cmd = new SqlCommand(strSQL);

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpId;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        return objDC.GetScalarVal(cmd);

    }

    public void UpdatePayrollRemarks(string strRemarks, string strIsP2P, string strEmpId, string strMonth, string strYear)
    {
        string strSQL = "UPDATE PAYSLIPMST SET REMARKS=@REMARKS,ISPRINTTOPAYSLIP=@ISPRINTTOPAYSLIP WHERE EMPID=@EMPID AND VMONTH=@VMONTH AND VYEAR=@VYEAR AND SALARYTYPE='S'";
        SqlCommand cmd = new SqlCommand(strSQL);

        SqlParameter p_REMARKS = cmd.Parameters.Add("REMARKS", SqlDbType.VarChar);
        p_REMARKS.Direction = ParameterDirection.Input;
        p_REMARKS.Value = strRemarks;

        SqlParameter p_ISPRINTTOPAYSLIP = cmd.Parameters.Add("ISPRINTTOPAYSLIP", SqlDbType.Char);
        p_ISPRINTTOPAYSLIP.Direction = ParameterDirection.Input;
        p_ISPRINTTOPAYSLIP.Value = strIsP2P;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpId;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        objDC.ExecuteQuery(cmd);

    }

    public DataTable GetPayrollDatAForExcel(string strGenerateFor, string strGeneratValue,
    string strMonth, string strYear, string strBranchCode, string strSalType,string strEmpGrpID)
    {
        if (objDC.ds.Tables["GetPayrollDatAForExcel"] != null)
        {
            objDC.ds.Tables["GetPayrollDatAForExcel"].Rows.Clear();
            objDC.ds.Tables["GetPayrollDatAForExcel"].Dispose();
        }
        SqlCommand cmd = new SqlCommand("proc_payroll_select_PayrollReportForExcel");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_GenerateFor = cmd.Parameters.Add("GenerateFor", SqlDbType.Char);
        p_GenerateFor.Direction = ParameterDirection.Input;
        p_GenerateFor.Value = strGenerateFor;

        SqlParameter p_GenerateValue = cmd.Parameters.Add("GenerateValue", SqlDbType.Char);
        p_GenerateValue.Direction = ParameterDirection.Input;
        p_GenerateValue.Value = strGeneratValue;

        SqlParameter p_BranchCode = cmd.Parameters.Add("BranchCode", SqlDbType.Char);
        p_BranchCode.Direction = ParameterDirection.Input;
        p_BranchCode.Value = strBranchCode;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_SALARYTYPE = cmd.Parameters.Add("SALARYTYPE", SqlDbType.Char);
        p_SALARYTYPE.Direction = ParameterDirection.Input;
        p_SALARYTYPE.Value = strSalType;

        SqlParameter p_EmpGrpID = cmd.Parameters.Add("EmpGrpID", SqlDbType.BigInt);
        p_EmpGrpID.Direction = ParameterDirection.Input;
        p_EmpGrpID.Value = strEmpGrpID;

        objDC.CreateDSFromProc(cmd, "GetPayrollDatAForExcel");
        return objDC.ds.Tables["GetPayrollDatAForExcel"];

    }
    public DataTable GetPayrollDataSalaryTypeWise(string strGenerateFor, string strGeneratValue,
        string strMonth, string strYear, string strBranchCode)
    {
        if (objDC.ds.Tables["GetPayrollDataSalaryTypeWise"] != null)
        {
            objDC.ds.Tables["GetPayrollDataSalaryTypeWise"].Rows.Clear();
            objDC.ds.Tables["GetPayrollDataSalaryTypeWise"].Dispose();
        }
        SqlCommand cmd = new SqlCommand("proc_payroll_select_PayrollSummaryReportSalaryTypeWise");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_GenerateFor = cmd.Parameters.Add("GenerateFor", SqlDbType.Char);
        p_GenerateFor.Direction = ParameterDirection.Input;
        p_GenerateFor.Value = strGenerateFor;

        SqlParameter p_GenerateValue = cmd.Parameters.Add("GenerateValue", SqlDbType.Char);
        p_GenerateValue.Direction = ParameterDirection.Input;
        p_GenerateValue.Value = strGeneratValue;

        SqlParameter p_BranchCode = cmd.Parameters.Add("BranchCode", SqlDbType.Char);
        p_BranchCode.Direction = ParameterDirection.Input;
        p_BranchCode.Value = strBranchCode;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        objDC.CreateDSFromProc(cmd, "GetPayrollDataSalaryTypeWise");
        return objDC.ds.Tables["GetPayrollDataSalaryTypeWise"];
    }

    public DataTable GetPayrollDataBankWise(string strGenerateFor, string strGeneratValue,
        string strMonth, string strYear, string strBranchCode, string strSalType, string strEmpGrpID)
    {
        if (objDC.ds.Tables["GetPayrollDataBankWise"] != null)
        {
            objDC.ds.Tables["GetPayrollDataBankWise"].Rows.Clear();
            objDC.ds.Tables["GetPayrollDataBankWise"].Dispose();
        }
        SqlCommand cmd = new SqlCommand("proc_payroll_select_PayrollSummaryReportBankWise");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_GenerateFor = cmd.Parameters.Add("GenerateFor", SqlDbType.Char);
        p_GenerateFor.Direction = ParameterDirection.Input;
        p_GenerateFor.Value = strGenerateFor;

        SqlParameter p_GenerateValue = cmd.Parameters.Add("GenerateValue", SqlDbType.Char);
        p_GenerateValue.Direction = ParameterDirection.Input;
        p_GenerateValue.Value = strGeneratValue;

        SqlParameter p_BranchCode = cmd.Parameters.Add("BranchCode", SqlDbType.Char);
        p_BranchCode.Direction = ParameterDirection.Input;
        p_BranchCode.Value = strBranchCode;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_SALARYTYPE = cmd.Parameters.Add("SALARYTYPE", SqlDbType.Char);
        p_SALARYTYPE.Direction = ParameterDirection.Input;
        p_SALARYTYPE.Value = strSalType;

        SqlParameter p_EmpGrpID = cmd.Parameters.Add("EmpGrpID", SqlDbType.BigInt);
        p_EmpGrpID.Direction = ParameterDirection.Input;
        p_EmpGrpID.Value = strEmpGrpID;

        objDC.CreateDSFromProc(cmd, "GetPayrollDataBankWise");
        return objDC.ds.Tables["GetPayrollDataBankWise"];
    }

    public DataTable GetEmpWiseSalaryDataForFiscalYear(string strFinYear, string strEmpID)
    {
        if (objDC.ds.Tables["GetEmpWiseSalaryDataForFiscalYear"] != null)
        {
            objDC.ds.Tables["GetEmpWiseSalaryDataForFiscalYear"].Rows.Clear();
            objDC.ds.Tables["GetEmpWiseSalaryDataForFiscalYear"].Dispose();
        }
        SqlCommand cmd = new SqlCommand("proc_payroll_select_EmpWisePayrollmovement");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_GenerateFor = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_GenerateFor.Direction = ParameterDirection.Input;
        p_GenerateFor.Value = strFinYear;

        SqlParameter p_GenerateValue = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_GenerateValue.Direction = ParameterDirection.Input;
        p_GenerateValue.Value = strEmpID;

        objDC.CreateDSFromProc(cmd, "GetEmpWiseSalaryDataForFiscalYear");
        return objDC.ds.Tables["GetEmpWiseSalaryDataForFiscalYear"];
    }

    public DataTable GetHeadCount()
    {
        string strSQL = "SELECT count(*) AS HEADCOUNT,DISPLAYTYPE from PAYSLIPSALHEADSEQ GROUP BY DISPLAYTYPE";
        return objDC.CreateDT(strSQL, "GetHeadCount");
    }

    public DataTable GetPayslipMonthlyGrossAndBenefits(string strEmpId, string strMonth, string strYear, string strSalType)
    {
        if (objDC.ds.Tables["GetPayslipMonthlyGrossAndBenefits"] != null)
        {
            objDC.ds.Tables["GetPayslipMonthlyGrossAndBenefits"].Rows.Clear();
            objDC.ds.Tables["GetPayslipMonthlyGrossAndBenefits"].Dispose();
        }

        SqlCommand cmd = new SqlCommand("Proc_Payroll_Select_PayslipMonthlyGrossAndBenefits");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpId;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_SALARYTYPE = cmd.Parameters.Add("SALARYTYPE", SqlDbType.Char);
        p_SALARYTYPE.Direction = ParameterDirection.Input;
        p_SALARYTYPE.Value = strSalType;

        objDC.CreateDSFromProc(cmd, "GetPayslipMonthlyGrossAndBenefits");
        return objDC.ds.Tables["GetPayslipMonthlyGrossAndBenefits"];
    }

    public DataTable GetPayslipMonthlyDeductions(string strEmpId, string strMonth, string strYear, string strSalType)
    {
        if (objDC.ds.Tables["GetPayslipMonthlyDeductions"] != null)
        {
            objDC.ds.Tables["GetPayslipMonthlyDeductions"].Rows.Clear();
            objDC.ds.Tables["GetPayslipMonthlyDeductions"].Dispose();
        }

        SqlCommand cmd = new SqlCommand("Proc_Payroll_Select_PayslipMonthlyDeductions");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpId;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_SALARYTYPE = cmd.Parameters.Add("SALARYTYPE", SqlDbType.Char);
        p_SALARYTYPE.Direction = ParameterDirection.Input;
        p_SALARYTYPE.Value = strSalType;

        objDC.CreateDSFromProc(cmd, "GetPayslipMonthlyDeductions");
        return objDC.ds.Tables["GetPayslipMonthlyDeductions"];
    }

    public DataTable GetPayslipMonthlyNetPay(string strEmpId, string strMonth, string strYear, string strSalType)
    {
        if (objDC.ds.Tables["GetPayslipMonthlyNetPay"] != null)
        {
            objDC.ds.Tables["GetPayslipMonthlyNetPay"].Rows.Clear();
            objDC.ds.Tables["GetPayslipMonthlyNetPay"].Dispose();
        }

        SqlCommand cmd = new SqlCommand("Proc_Payroll_Select_PayslipMonthlyNetPay");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpId;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_SALARYTYPE = cmd.Parameters.Add("SALARYTYPE", SqlDbType.Char);
        p_SALARYTYPE.Direction = ParameterDirection.Input;
        p_SALARYTYPE.Value = strSalType;

        objDC.CreateDSFromProc(cmd, "GetPayslipMonthlyNetPay");
        return objDC.ds.Tables["GetPayslipMonthlyNetPay"];
    }

    public DataTable GetPayslipEmployeeData(string strGenerateFor, string strGeneratValue, string strMonth, string strYear, string strBranchCode, string strSalType)
    {
        SqlCommand cmd = new SqlCommand("proc_payroll_select_PayslipEmployee");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_GenerateFor = cmd.Parameters.Add("GenerateFor", SqlDbType.Char);
        p_GenerateFor.Direction = ParameterDirection.Input;
        p_GenerateFor.Value = strGenerateFor;

        SqlParameter p_GenerateValue = cmd.Parameters.Add("GenerateValue", SqlDbType.Char);
        p_GenerateValue.Direction = ParameterDirection.Input;
        p_GenerateValue.Value = strGeneratValue;

        SqlParameter p_BranchCode = cmd.Parameters.Add("BranchCode", SqlDbType.Char);
        p_BranchCode.Direction = ParameterDirection.Input;
        p_BranchCode.Value = strBranchCode;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_SALARYTYPE = cmd.Parameters.Add("SALARYTYPE", SqlDbType.Char);
        p_SALARYTYPE.Direction = ParameterDirection.Input;
        p_SALARYTYPE.Value = strSalType;

        objDC.CreateDSFromProc(cmd, "GetPayslipEmployeeData");
        return objDC.ds.Tables["GetPayslipEmployeeData"];
    }

    public string GetEmpJoinOrSeperateDate(string strEmpID, string strCol)
    {
        string strSQL = "SELECT " + strCol + " FROM EMPINFO WHERE EMPID=@EMPID";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        return objDC.GetScalarVal(cmd);
    }

    public DataTable GetSalaryMovementData(string strMonth, string strYear, string strGenerateFor, string strGeneratValue, string strIsSumm)
    {
        if (objDC.ds.Tables["GetSalaryMovementData"] != null)
        {
            objDC.ds.Tables["GetSalaryMovementData"].Rows.Clear();
            objDC.ds.Tables["GetSalaryMovementData"].Dispose();
        }

        SqlCommand cmd = new SqlCommand("proc_payroll_select_SalaryMovement");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_GenerateFor = cmd.Parameters.Add("GenerateFor", SqlDbType.Char);
        p_GenerateFor.Direction = ParameterDirection.Input;
        p_GenerateFor.Value = strGenerateFor;

        SqlParameter p_GenerateValue = cmd.Parameters.Add("GenerateValue", SqlDbType.Char);
        p_GenerateValue.Direction = ParameterDirection.Input;
        p_GenerateValue.Value = strGeneratValue;


        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_ISSUMMARY = cmd.Parameters.Add("ISSUMMARY", SqlDbType.Char);
        p_ISSUMMARY.Direction = ParameterDirection.Input;
        p_ISSUMMARY.Value = strIsSumm;

        objDC.CreateDSFromProc(cmd, "GetSalaryMovementData");
        return objDC.ds.Tables["GetSalaryMovementData"];
    }

    #region Bank Statment
    public DataTable GetBankStatmentDetails(string strMonth, string strYear, string strBank, string strClinic,string strSalType)
    {
        if (objDC.ds.Tables["GetBankStatmentDetails"] != null)
        {
            objDC.ds.Tables["GetBankStatmentDetails"].Rows.Clear();
            objDC.ds.Tables["GetBankStatmentDetails"].Dispose();
        }
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Select_BankStatementDetails");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_BRANCHCODE = cmd.Parameters.Add("BRANCHCODE", SqlDbType.Char);
        p_BRANCHCODE.Direction = ParameterDirection.Input;
        p_BRANCHCODE.Value = strBank;

        SqlParameter p_CLINICID = cmd.Parameters.Add("CLINICID", SqlDbType.BigInt);
        p_CLINICID.Direction = ParameterDirection.Input;
        p_CLINICID.Value = strClinic;

        SqlParameter p_SALARYTYPE = cmd.Parameters.Add("SALARYTYPE", SqlDbType.Char);
        p_SALARYTYPE.Direction = ParameterDirection.Input;
        p_SALARYTYPE.Value = strSalType;

        objDC.CreateDSFromProc(cmd, "GetBankStatmentDetails");
        return objDC.ds.Tables["GetBankStatmentDetails"];
    }

    #endregion
    #region Payroll by NC
    public DataTable GetPayrollDataByNC(string strMonth, string strYear, string strBank, string strSalType, string strFiscalYrID)
    {
        if (objDC.ds.Tables["GetBankStatmentDetails"] != null)
        {
            objDC.ds.Tables["GetBankStatmentDetails"].Rows.Clear();
            objDC.ds.Tables["GetBankStatmentDetails"].Dispose();
        }
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Select_PayrollByNCData");
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_BRANCHCODE = cmd.Parameters.Add("BRANCHCODE", SqlDbType.Char);
        p_BRANCHCODE.Direction = ParameterDirection.Input;
        p_BRANCHCODE.Value = strBank;

        SqlParameter p_SALARYTYPE = cmd.Parameters.Add("SALARYTYPE", SqlDbType.Char);
        p_SALARYTYPE.Direction = ParameterDirection.Input;
        p_SALARYTYPE.Value = strSalType;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYrID;

        objDC.CreateDSFromProc(cmd, "GetPayrollDataByNC");
        return objDC.ds.Tables["GetPayrollDataByNC"];
    }
#endregion
    #region IT Statement
    //public DataTable GetEmployeeDataForITStatement(string strEmpId, string strStatus, string strFinYear)
    //{
    //    SqlCommand cmd = new SqlCommand("proc_payroll_select_EmployeeForITStatement");
    //    cmd.CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_VMONTH = cmd.Parameters.Add("EMPID", SqlDbType.Char);
    //    p_VMONTH.Direction = ParameterDirection.Input;
    //    p_VMONTH.Value = strEmpId;

    //    SqlParameter p_VYEAR = cmd.Parameters.Add("STATUS", SqlDbType.Char);
    //    p_VYEAR.Direction = ParameterDirection.Input;
    //    p_VYEAR.Value = strStatus;

    //    SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
    //    p_FISCALYRID.Direction = ParameterDirection.Input;
    //    p_FISCALYRID.Value = strFinYear;

    //    objDC.CreateDSFromProc(cmd, "GetEmployeeDataForITStatement");
    //    return objDC.ds.Tables["GetEmployeeDataForITStatement"];
    //}

    public DataTable GetEmployeeDataForITStatement(string strEmpId, string strStatus, string strFiscalYRID)
    {
        SqlCommand cmd = new SqlCommand("proc_payroll_select_EmployeeForITStatement");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strEmpId;

        SqlParameter p_VYEAR = cmd.Parameters.Add("STATUS", SqlDbType.Char);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strStatus;

        SqlParameter p_FiscalYRID = cmd.Parameters.Add("TaxFiscalYRID", SqlDbType.Char);
        p_FiscalYRID.Direction = ParameterDirection.Input;
        p_FiscalYRID.Value = strFiscalYRID;

        objDC.CreateDSFromProc(cmd, "GetEmployeeDataForITStatement");
        return objDC.ds.Tables["GetEmployeeDataForITStatement"];
    }
    // New added 
    public DataTable GetBonusAllowanceYearlyEmpWise(string strFisYr, string strEmpID)
    {
        SqlCommand cmd = new SqlCommand("PROC_PAYROLL_SELECT_BONUSALLOW_YEARLY_EMPWISE");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("TaxFiscalYrId", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFisYr;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        objDC.CreateDSFromProc(cmd, "GetBonusAllowanceYearlyEmpWise");
        return objDC.ds.Tables["GetBonusAllowanceYearlyEmpWise"];

    }

    //public DataTable GetPayrollDataForITStatement(string strEmpId, string strFiscalYear)
    //{

    //    if (objDC.ds.Tables["GetPayrollDataForITStatement"] != null)
    //    {
    //        objDC.ds.Tables["GetPayrollDataForITStatement"].Rows.Clear();
    //        objDC.ds.Tables["GetPayrollDataForITStatement"].Dispose();
    //    }

    //    SqlCommand cmd = new SqlCommand("Proc_Payroll_Select_FiscalYearWisePayrollDetails");
    //    cmd.CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_VMONTH = cmd.Parameters.Add("EMPID", SqlDbType.Char);
    //    p_VMONTH.Direction = ParameterDirection.Input;
    //    p_VMONTH.Value = strEmpId;

    //    SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
    //    p_FISCALYRID.Direction = ParameterDirection.Input;
    //    p_FISCALYRID.Value = strFiscalYear;

    //    objDC.CreateDSFromProc(cmd, "GetPayrollDataForITStatement");
    //    return objDC.ds.Tables["GetPayrollDataForITStatement"];
    //}


    public DataTable GetPayrollDataForITStatement(string strEmpId, string strFiscalYear)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Select_FiscalYearWisePayrollDetails");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strEmpId;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("TaxFiscalYrId", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYear;

        objDC.CreateDSFromProc(cmd, "GetPayrollDataForITStatement");
        return objDC.ds.Tables["GetPayrollDataForITStatement"];
    }

    public DataTable GetPayrollDataForITStatement(string strFiscalYear)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Select_FiscalYearWisePayrollDetailsForITReports");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYear;

        objDC.CreateDSFromProc(cmd, "GetPayrollDataForITStatement2");
        return objDC.ds.Tables["GetPayrollDataForITStatement2"];
    }

    //public DataTable GetChallanDataForITStatement(string strEmpId, string strFinYear)
    //{
    //    if (objDC.ds.Tables["GetChallanDataForITStatement"] != null)
    //    {
    //        objDC.ds.Tables["GetChallanDataForITStatement"].Rows.Clear();
    //        objDC.ds.Tables["GetChallanDataForITStatement"].Dispose();
    //    }

    //    string strSQL = "SELECT EMPID,CHALLANDATE,PAYAMT,CHALLANNO,FISCALYRID,BANKNAME "
    //        + " FROM ITDEPOSITRECORDS WHERE EMPID=@EMPID AND FISCALYRID=@FISCALYRID ORDER BY EMPID,CHALLANDATE";
    //    SqlCommand cmd = new SqlCommand(strSQL);
    //    cmd.CommandType = CommandType.Text;

    //    SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
    //    p_EMPID.Direction = ParameterDirection.Input;
    //    p_EMPID.Value = strEmpId;

    //    SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
    //    p_FISCALYRID.Direction = ParameterDirection.Input;
    //    p_FISCALYRID.Value = strFinYear;

    //    objDC.CreateDT(cmd, "GetChallanDataForITStatement");
    //    return objDC.ds.Tables["GetChallanDataForITStatement"];

    //}
    public DataTable GetChallanDataForITStatement(string strEmpId, string strFinYear)
    {
        if (objDC.ds.Tables["GetChallanDataForITStatement"] != null)
        {
            objDC.ds.Tables["GetChallanDataForITStatement"].Rows.Clear();
            objDC.ds.Tables["GetChallanDataForITStatement"].Dispose();
        }

        string strSQL = "SELECT EMPID,CHALLANDATE,PAYAMT,CHALLANNO,TaxFiscalYrId,BANKNAME,Vmonth,VYear "
            + " FROM ITDEPOSITRECORDS WHERE EMPID=@EMPID AND TaxFiscalYrId=@TaxFiscalYrId ORDER BY EMPID,VYear,VMONTH";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpId;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("TaxFiscalYrId", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        objDC.CreateDT(cmd, "GetChallanDataForITStatement");
        return objDC.ds.Tables["GetChallanDataForITStatement"];

    }


    #endregion
    #region MovementReport
    DataTable dtValidation;
    public void GetMovementReport(string strMonth, string strYear, string strGenerateFor, string strGeneratValue,
        GridView grMovement, GridView grMovementDetls, GridView grValidation, Label lblMonthBetween)
    {
        EmpInfoManager objEmpMgr = new EmpInfoManager();
        DataTable dtMovement = this.GetSalaryMovementData(strMonth, strYear, strGenerateFor, strGeneratValue, "Y");
        grMovement.DataSource = dtMovement;
        grMovement.DataBind();
        if (grMovement.Rows.Count > 0)
        {
            grMovement.FooterRow.Cells[0].Text = "Salary Increased/(Decreased) in " + Common.ReturnFullMonthName(strMonth) + strYear;
            grMovement.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            this.GetFooterDifference(grMovement, 1);
        }

        if (dtMovement.Rows.Count > 0)
        {
            lblMonthBetween.Text = dtMovement.Rows[0]["MONTHBETWEEN"].ToString().Trim();

        }
        DataTable dtMovementDetls = this.GetSalaryMovementData(strMonth, strYear, strGenerateFor, strGeneratValue, "N");
        grMovementDetls.DataSource = dtMovementDetls;
        grMovementDetls.DataBind();
        if (grMovementDetls.Rows.Count > 0)
        {
            this.GetFooterSummary(grMovementDetls, 3);
            grMovementDetls.FooterRow.Cells[2].Text = "Salary Increased/(Decreased) in " + Common.ReturnFullMonthName(strMonth) + strYear;
            grMovementDetls.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;
        }


        this.IniValidationDataTable(9, grValidation);
        this.GetValidationResult(grValidation, grMovement, grMovementDetls);

    }

    protected void GetFooterDifference(GridView grv, int inIndx)
    {
        decimal decSummary = 0;
        for (int i = inIndx; i < grv.Columns.Count; i++)
        {
            decSummary = 0;
            decSummary = Common.RoundDecimal(grv.Rows[1].Cells[i].Text.Trim(), 0) - Common.RoundDecimal(grv.Rows[0].Cells[i].Text.Trim(), 0);
            if (decSummary == 0)
                grv.FooterRow.Cells[i].Text = "-";
            else if (decSummary < 0)
                grv.FooterRow.Cells[i].Text = "(" + Convert.ToString(Math.Abs(decSummary)) + ")";
            else
                grv.FooterRow.Cells[i].Text = decSummary.ToString();

            if (Common.RoundDecimal(grv.Rows[1].Cells[i].Text.Trim(), 0) == 0)
                grv.Rows[1].Cells[i].Text = "-";
            else if (Common.RoundDecimal(grv.Rows[1].Cells[i].Text.Trim(), 0) < 0)
                grv.Rows[1].Cells[i].Text = "(" + Convert.ToString(Math.Abs(Convert.ToDecimal(grv.Rows[1].Cells[i].Text.Trim()))) + ")";

            if (Common.RoundDecimal(grv.Rows[0].Cells[i].Text.Trim(), 0) == 0)
                grv.Rows[0].Cells[i].Text = "-";
            else if (Common.RoundDecimal(grv.Rows[0].Cells[i].Text.Trim(), 0) < 0)
                grv.Rows[1].Cells[i].Text = "(" + Convert.ToString(Math.Abs(Convert.ToDecimal(grv.Rows[0].Cells[i].Text.Trim()))) + ")";
        }
    }

    protected void GetFooterSummary(GridView grv, int inIndx)
    {
        decimal decSummary = 0;
        for (int i = inIndx; i < grv.Columns.Count; i++)
        {
            decSummary = 0;
            foreach (GridViewRow gRow in grv.Rows)
            {
                decSummary = decSummary + Common.RoundDecimal(gRow.Cells[i].Text.Trim(), 0);
                if (Common.RoundDecimal(gRow.Cells[i].Text.Trim(), 0) == 0)
                    gRow.Cells[i].Text = "-";
                else if (Common.RoundDecimal(gRow.Cells[i].Text.Trim(), 0) < 0)
                    gRow.Cells[i].Text = "(" + Convert.ToString(Math.Abs(Convert.ToDecimal(gRow.Cells[i].Text.Trim()))) + ")";
            }
            if (decSummary == 0)
                grv.FooterRow.Cells[i].Text = "-";
            else if (decSummary < 0)
                grv.FooterRow.Cells[i].Text = "(" + Convert.ToString(Math.Abs(decSummary)) + ")";
            else
                grv.FooterRow.Cells[i].Text = decSummary.ToString();

        }
    }



    protected void IniValidationDataTable(int inCol, GridView grValidation)
    {
        dtValidation = new DataTable();
        for (int i = 0; i < inCol; i++)
        {
            dtValidation.Columns.Add(i.ToString());
        }
        dtValidation.AcceptChanges();

        DataRow nRow = dtValidation.NewRow();
        dtValidation.Rows.Add(nRow);
        dtValidation.AcceptChanges();

        grValidation.DataSource = dtValidation;
        grValidation.DataBind();
    }

    protected void GetValidationResult(GridView grValidation, GridView grMovement, GridView grMovementDetls)
    {
        grValidation.Rows[0].Cells[0].Text = "Validation Check  ";
        for (int i = 1; i < grValidation.Columns.Count; i++)
        {
            if (grMovementDetls.Rows.Count > 0)
            {
                if (grMovement.FooterRow.Cells[i].Text.Trim() == grMovementDetls.FooterRow.Cells[i + 2].Text.Trim())
                {
                    grValidation.Rows[0].Cells[i].Text = "TRUE";
                }
                else
                {
                    grValidation.Rows[0].Cells[i].Text = "FALSE";
                    //grValidation.Rows[0].Cells[i].ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                grValidation.Rows[0].Cells[i].Text = "TRUE";
            }
            grValidation.Rows[0].Cells[i].HorizontalAlign = HorizontalAlign.Center;
            grValidation.Rows[0].Cells[i].Font.Bold = true;
        }
    }
    #endregion

    #region Salary Report


    // Report ID 1
    public void InsertSalHeadWithSeqForReport(GridView grv, int inReportID)
    {
        int i = 0;
        SqlCommand[] command = new SqlCommand[grv.Rows.Count + 1];
        command[i] = new SqlCommand("Proc_Payroll_Delete_SALARYHEADWITHSEQFORREPORT");
        command[i].CommandType = CommandType.StoredProcedure;

        SqlParameter p_REPORTID1 = command[i].Parameters.Add("REPORTID", SqlDbType.BigInt);
        p_REPORTID1.Direction = ParameterDirection.Input;
        p_REPORTID1.Value = inReportID;
        i++;

        foreach (GridViewRow gRow in grv.Rows)
        {
            CheckBox chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            TextBox txtSN = (TextBox)gRow.Cells[3].FindControl("txtSeqNo");
            if (chBox.Checked == true)
            {
                command[i] = new SqlCommand("Proc_Payroll_Insert_SALARYHEADWITHSEQFORREPORT");
                command[i].CommandType = CommandType.StoredProcedure;

                SqlParameter p_SEQNO = command[i].Parameters.Add("SEQNO", SqlDbType.BigInt);
                p_SEQNO.Direction = ParameterDirection.Input;
                p_SEQNO.Value = txtSN.Text.Trim();

                SqlParameter p_SHEADID = command[i].Parameters.Add("SHEADID", SqlDbType.BigInt);
                p_SHEADID.Direction = ParameterDirection.Input;
                p_SHEADID.Value = grv.DataKeys[gRow.DataItemIndex].Values[0].ToString().Trim();

                SqlParameter p_REPORTID = command[i].Parameters.Add("REPORTID", SqlDbType.BigInt);
                p_REPORTID.Direction = ParameterDirection.Input;
                p_REPORTID.Value = inReportID;
                i++;
            }
        }
        objDC.MakeTransaction(command);
    }

    public DataTable SelectSalHeadWithSeqForReport(int inReportID)
    {
        SqlCommand command = new SqlCommand("Proc_Payroll_Select_SALARYHEADWITHSEQFORREPORT");

        SqlParameter p_REPORTID = command.Parameters.Add("REPORTID", SqlDbType.BigInt);
        p_REPORTID.Direction = ParameterDirection.Input;
        p_REPORTID.Value = inReportID;

        objDC.CreateDSFromProc(command, "SalHeadWithSeqForReport");
        return objDC.ds.Tables["SalHeadWithSeqForReport"];
    }

    public DataTable GetFiscalYearWiseDistinctEmployee(string strFiscalYrID, string strLocId)
    {
        SqlCommand command = new SqlCommand("Proc_Payroll_Select_FiscalYearWiseDistinctEmployee");

        SqlParameter p_FISCALYRID = command.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYrID;

        SqlParameter p_LOCID = command.Parameters.Add("LOCID", SqlDbType.BigInt);
        p_LOCID.Direction = ParameterDirection.Input;
        p_LOCID.Value = strLocId;

        objDC.CreateDSFromProc(command, "GetFiscalYearWiseDistinctEmployee");
        return objDC.ds.Tables["GetFiscalYearWiseDistinctEmployee"];
    }

    public DataTable GetMonthWiseDistinctEmployeeForStaffSalaryIT(string strFiscalYrID, string strLocId, string strMonth)
    {
        SqlCommand command = new SqlCommand("Proc_Payroll_Select_MonthWiseDistinctEmployeeForStaffSlaaryIT");

        SqlParameter p_FISCALYRID = command.Parameters.Add("TAXFISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYrID;

        SqlParameter p_LOCID = command.Parameters.Add("LOCID", SqlDbType.BigInt);
        p_LOCID.Direction = ParameterDirection.Input;
        p_LOCID.Value = strLocId;

        SqlParameter p_VMONTH = command.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        objDC.CreateDSFromProc(command, "GetMonthWiseDistinctEmployeeForStaffSalaryIT");
        return objDC.ds.Tables["GetMonthWiseDistinctEmployeeForStaffSalaryIT"];
    }

    public DataTable GetFiscalYearWiseSalaryItemData(string strFiscalYrID, string strLocId)
    {
        SqlCommand command = new SqlCommand("Proc_Payroll_Select_FiscalYearWiseSalarayItemData");

        SqlParameter p_FISCALYRID = command.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYrID;

        SqlParameter p_LOCID = command.Parameters.Add("LOCID", SqlDbType.BigInt);
        p_LOCID.Direction = ParameterDirection.Input;
        p_LOCID.Value = strLocId;

        objDC.CreateDSFromProc(command, "GetFiscalYearWiseSalaryItemData");
        return objDC.ds.Tables["GetFiscalYearWiseSalaryItemData"];
    }

    public DataTable GetMonthWiseSalaryItemDataForStaffSalaryIT(string strFiscalYrID, string strLocId, string strMonth)
    {
        SqlCommand command = new SqlCommand("Proc_Payroll_Select_MonthWiseSalarayItemDataForStaffSalaryIT");

        SqlParameter p_FISCALYRID = command.Parameters.Add("TAXFISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYrID;

        SqlParameter p_LOCID = command.Parameters.Add("LOCID", SqlDbType.BigInt);
        p_LOCID.Direction = ParameterDirection.Input;
        p_LOCID.Value = strLocId;

        SqlParameter p_VMONTH = command.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        objDC.CreateDSFromProc(command, "GetMonthWiseSalaryItemDataForStaffSalaryIT");
        return objDC.ds.Tables["GetMonthWiseSalaryItemDataForStaffSalaryIT"];
    }


    #endregion

    #region Provident Fund
    public DataTable GetPFLoanLedgerData(string strMonth, string strFinYear,string strEmpId)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Select_PFLoanLedgerMonthlyCrystalReportData");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;   

        SqlParameter p_EMPID = cmd.Parameters.Add("EmpId", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpId;

        objDC.CreateDSFromProc(cmd, "GetPFLoanLedgerData");
        return objDC.ds.Tables["GetPFLoanLedgerData"];
    }
    #endregion

    public DataTable GetSalaryStatement(string strFlag, string strEmpId, string strMonth, string strYear,
        string strDivId, string strDistId, string strPlaceId, string strEmpTypeId)
    {
        if (objDC.ds.Tables["GetSalaryStatement"] != null)
        {
            objDC.ds.Tables["GetSalaryStatement"].Rows.Clear();
            objDC.ds.Tables["GetSalaryStatement"].Dispose();
        }

        SqlCommand cmd = new SqlCommand("proc_GetSalaryStatement");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Flag = cmd.Parameters.Add("Flag", SqlDbType.Char);
        p_Flag.Direction = ParameterDirection.Input;
        p_Flag.Value = strFlag;

        SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_PostingDivId = cmd.Parameters.Add("PostingDivId", SqlDbType.BigInt);
        p_PostingDivId.Direction = ParameterDirection.Input;
        p_PostingDivId.Value = strDivId;

        SqlParameter p_PostingDistId = cmd.Parameters.Add("PostingDistId", SqlDbType.BigInt);
        p_PostingDistId.Direction = ParameterDirection.Input;
        p_PostingDistId.Value = strDistId;

        SqlParameter p_PostingPlaceId = cmd.Parameters.Add("PostingPlaceId", SqlDbType.BigInt);
        p_PostingPlaceId.Direction = ParameterDirection.Input;
        p_PostingPlaceId.Value = strPlaceId;

        SqlParameter p_EmpTypeId = cmd.Parameters.Add("EmpTypeId", SqlDbType.BigInt);
        p_EmpTypeId.Direction = ParameterDirection.Input;
        p_EmpTypeId.Value = strEmpTypeId;

        objDC.CreateDSFromProc(cmd, "GetSalaryStatement");
        return objDC.ds.Tables["GetSalaryStatement"];
    }

    #region Payroll Report New

    public DataTable Get_Salary_Reconcilation(string fDate, string usdrate, string strEmpTypeId)
    {
        SqlCommand cmd = new SqlCommand("proc_Rpt_SalaryReconcilation");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMonth", SqlDbType.DateTime);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = fDate;

        SqlParameter p_USD = cmd.Parameters.Add("USDRate", SqlDbType.Decimal);
        p_USD.Direction = ParameterDirection.Input;
        p_USD.Value = usdrate;

        SqlParameter p_EmpTypeId = cmd.Parameters.Add("EmpTypeId", SqlDbType.BigInt);
        p_EmpTypeId.Direction = ParameterDirection.Input;
        p_EmpTypeId.Value = strEmpTypeId;

        //Common.ReturnDate(SubmissionDate);
        objDC.CreateDSFromProc(cmd, "GetPRSalRecon");
        return objDC.ds.Tables["GetPRSalRecon"];

    }
    //Get_Salary_Reconcilation_Param
    public DataTable Get_Salary_Reconcilation_Param(string fDate, string strEmpTypeId)
    {
        SqlCommand cmd = new SqlCommand("proc_Select_SalaryReconcilation");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMonth", SqlDbType.DateTime);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = fDate;// Common.ReturnDate(fDate);

        SqlParameter p_EType = cmd.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EType.Direction = ParameterDirection.Input;
        p_EType.Value = Convert.ToInt32(strEmpTypeId);

        //Common.ReturnDate(SubmissionDate);
        objDC.CreateDSFromProc(cmd, "GetPRSalReconParam");
        return objDC.ds.Tables["GetPRSalReconParam"];
    }
    public DataTable Get_Salary_SheetEmpWise(string fdate, string FisYear, string SalDivision, string strCompany)
    {
        SqlCommand cmd = new SqlCommand("proc_Rpt_SalarySheetEmpWs");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = fdate;

        SqlParameter p_FisYear = cmd.Parameters.Add("FisYear", SqlDbType.BigInt);
        p_FisYear.Direction = ParameterDirection.Input;
        p_FisYear.Value = FisYear;

        SqlParameter p_DivisionId = cmd.Parameters.Add("DivisionId", SqlDbType.BigInt);
        p_DivisionId.Direction = ParameterDirection.Input;
        p_DivisionId.Value = strCompany;

        SqlParameter p_SalSubLocId = cmd.Parameters.Add("ClinicId", SqlDbType.BigInt);
        p_SalSubLocId.Direction = ParameterDirection.Input;
        p_SalSubLocId.Value = SalDivision;     

        objDC.CreateDSFromProc(cmd, "GetSalarySheetEmpWs");
        return objDC.ds.Tables["GetSalarySheetEmpWs"];
    }
    public DataTable Get_Salary_SheetSummary(string fdate, string FisYear,string SalDivision)
    {
        SqlCommand cmd = new SqlCommand("proc_Rpt_SalarySheetSummery");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = fdate;

        SqlParameter p_FisYear = cmd.Parameters.Add("FisYear", SqlDbType.BigInt);
        p_FisYear.Direction = ParameterDirection.Input;
        p_FisYear.Value = FisYear;

        SqlParameter p_SalSubLocId = cmd.Parameters.Add("p_SalSubLocId", SqlDbType.Char);
        p_SalSubLocId.Direction = ParameterDirection.Input;
        p_SalSubLocId.Value = SalDivision;

        objDC.CreateDSFromProc(cmd, "GetPRSalReconParam");
        return objDC.ds.Tables["GetPRSalReconParam"];
    }

    public DataTable Get_BonusSummary(string fdate, string FisYear, string SalDivision)
    {
        SqlCommand cmd = new SqlCommand("proc_Rpt_BonusSummery");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = fdate;

        SqlParameter p_FisYear = cmd.Parameters.Add("FisYear", SqlDbType.BigInt);
        p_FisYear.Direction = ParameterDirection.Input;
        p_FisYear.Value = FisYear;

        SqlParameter p_SalSubLocId = cmd.Parameters.Add("p_SalSubLocId", SqlDbType.Char);
        p_SalSubLocId.Direction = ParameterDirection.Input;
        p_SalSubLocId.Value = SalDivision;

        objDC.CreateDSFromProc(cmd, "GetPRSalReconParam");
        return objDC.ds.Tables["GetPRSalReconParam"];
    }

    public DataTable Get_Salary_SheetSummary01(string fdate, string FisYear, string SalDivision)
    {
        SqlCommand cmd = new SqlCommand("proc_Rpt_SalarySheetSummery01");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMonth", SqlDbType.DateTime);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = fdate;

        SqlParameter p_FisYear = cmd.Parameters.Add("FisYear", SqlDbType.BigInt);
        p_FisYear.Direction = ParameterDirection.Input;
        p_FisYear.Value = FisYear;

        SqlParameter p_SalSubLocId = cmd.Parameters.Add("p_SalSubLocId", SqlDbType.Char);
        p_SalSubLocId.Direction = ParameterDirection.Input;
        p_SalSubLocId.Value = SalDivision;

        //p_VMONTH.Value = Common.ReturnDate(fdate);

        objDC.CreateDSFromProc(cmd, "GetPRSalReconParam");
        return objDC.ds.Tables["GetPRSalReconParam"];
    }



    #endregion
    
    #region Payroll Report New

    public DataTable Get_Salary_SheetSummaryEmpWise(string VMonth, string empid, string FisYear, string strEmpTypeId)
    {
        SqlCommand cmd = new SqlCommand("proc_Rpt_SalarySheetEmpWise");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = VMonth;

        SqlParameter p_EmoId = cmd.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmoId.Direction = ParameterDirection.Input;
        p_EmoId.Value = empid;

        SqlParameter p_FisYear = cmd.Parameters.Add("FisYear", SqlDbType.BigInt);
        p_FisYear.Direction = ParameterDirection.Input;
        p_FisYear.Value = FisYear;

        SqlParameter p_EmpTypeId = cmd.Parameters.Add("EmpTypeId", SqlDbType.BigInt);
        p_EmpTypeId.Direction = ParameterDirection.Input;
        p_EmpTypeId.Value = strEmpTypeId;

        objDC.CreateDSFromProc(cmd, "GetSalSheetSummEmpWise");
        return objDC.ds.Tables["GetSalSheetSummEmpWise"];
    }

    public DataTable Get_SalaryStatement(string FisYear, string VMonth, string VYear, string EmpID, string SalDiv, string SalType,string stEmpTypeID)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Select_BankStatement");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FisYear = cmd.Parameters.Add("FisYear", SqlDbType.Char);
        p_FisYear.Direction = ParameterDirection.Input;
        p_FisYear.Value = FisYear;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMonth", SqlDbType.Char);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = VMonth;

        SqlParameter p_VYear = cmd.Parameters.Add("VYear", SqlDbType.Char);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = VYear;

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        SqlParameter p_SalDiv = cmd.Parameters.Add("ClinicId", SqlDbType.VarChar);
        p_SalDiv.Direction = ParameterDirection.Input;
        p_SalDiv.Value = SalDiv;

        SqlParameter p_SalType = cmd.Parameters.Add("SalType", SqlDbType.Char);
        p_SalType.Direction = ParameterDirection.Input;
        p_SalType.Value = SalType;

        SqlParameter p_EType = cmd.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EType.Direction = ParameterDirection.Input;
        p_EType.Value = Convert.ToInt32(stEmpTypeID);

        //Common.ReturnDate(SubmissionDate);
        objDC.CreateDSFromProc(cmd, "GetSalaryStatement");
        return objDC.ds.Tables["GetSalaryStatement"];

    }

    public DataTable Get_SalaryStatementSummery(string FisYear, string VMonth, string VYear, string SalLoc,  string SalType, string rbtEType)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_SalaryStatementSummery");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FisYear = cmd.Parameters.Add("FisYear", SqlDbType.Char);
        p_FisYear.Direction = ParameterDirection.Input;
        p_FisYear.Value = FisYear;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMonth", SqlDbType.Char);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = VMonth;

        SqlParameter p_VYear = cmd.Parameters.Add("VYear", SqlDbType.Char);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = VYear;

        SqlParameter p_SalLoc = cmd.Parameters.Add("SalLoc", SqlDbType.VarChar);
        p_SalLoc.Direction = ParameterDirection.Input;
        p_SalLoc.Value = SalLoc;

        SqlParameter p_SalType = cmd.Parameters.Add("SalType", SqlDbType.Char);
        p_SalType.Direction = ParameterDirection.Input;
        p_SalType.Value = SalType;

        SqlParameter p_EType = cmd.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EType.Direction = ParameterDirection.Input;
        p_EType.Value = Convert.ToInt32(rbtEType);

        objDC.CreateDSFromProc(cmd, "GetSalaryStatement");
        return objDC.ds.Tables["GetSalaryStatement"];
    }

    //Get_BonusStatementFastival
    //
    public DataTable Get_BonusStatementFastival(string FisYear, string VMonth, string Division, string Religion, string Festival, string rbtEType)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_BonusStatementFastival");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FisYear = cmd.Parameters.Add("FisYear", SqlDbType.BigInt);
        p_FisYear.Direction = ParameterDirection.Input;
        p_FisYear.Value = Convert.ToInt32(FisYear);

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = Convert.ToInt32(VMonth);

        SqlParameter p_SalLoc = cmd.Parameters.Add("DivisionId", SqlDbType.BigInt);
        p_SalLoc.Direction = ParameterDirection.Input;
        p_SalLoc.Value = Convert.ToInt32(Division);

        //SqlParameter p_SalSubLoc = cmd.Parameters.Add("SalSubLoc", SqlDbType.BigInt);
        //p_SalSubLoc.Direction = ParameterDirection.Input;
        //p_SalSubLoc.Value = Convert.ToInt32(SalSubLoc);

        SqlParameter p_Religion = cmd.Parameters.Add("ReligionID", SqlDbType.BigInt);
        p_Religion.Direction = ParameterDirection.Input;
        p_Religion.Value = Convert.ToInt32(Religion);

        SqlParameter p_Festival = cmd.Parameters.Add("FestivalID", SqlDbType.BigInt);
        p_Festival.Direction = ParameterDirection.Input;
        p_Festival.Value = Convert.ToInt32(Festival);

        SqlParameter p_EType = cmd.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EType.Direction = ParameterDirection.Input;
        p_EType.Value = Convert.ToInt32(rbtEType);

        //rbtEType


        //Common.ReturnDate(SubmissionDate);
        objDC.CreateDSFromProc(cmd, "GetBonusStatementFastival");
        return objDC.ds.Tables["GetBonusStatementFastival"];

    }
    //Get_FastivalBonusSummery
    public DataTable Get_FastivalBonusSummery(string FisYear, string VMonth,  string Festival)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_FastivalBonusSummery");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FisYear = cmd.Parameters.Add("FisYear", SqlDbType.BigInt);
        p_FisYear.Direction = ParameterDirection.Input;
        p_FisYear.Value = Convert.ToInt32(FisYear);

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = Convert.ToInt32(VMonth);

        SqlParameter p_Festival = cmd.Parameters.Add("FestivalID", SqlDbType.BigInt);
        p_Festival.Direction = ParameterDirection.Input;
        p_Festival.Value = Convert.ToInt32(Festival);

        objDC.CreateDSFromProc(cmd, "GetSalaryStatement");
        return objDC.ds.Tables["GetSalaryStatement"];
    }

    public DataTable Get_Salary_SheetSOFWise(string FisYear, string VMonth, string empid, string SalSubLoc)
    {
        SqlCommand cmd = new SqlCommand("proc_Rpt_SalarySheetSOFWise");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FisYear = cmd.Parameters.Add("FisYear", SqlDbType.Char);
        p_FisYear.Direction = ParameterDirection.Input;
        p_FisYear.Value = FisYear;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMonth", SqlDbType.DateTime);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = VMonth;

        SqlParameter p_EmoId = cmd.Parameters.Add("EmplId ", SqlDbType.VarChar);
        p_EmoId.Direction = ParameterDirection.Input;
        p_EmoId.Value = empid;

        SqlParameter p_SalSubLoc = cmd.Parameters.Add("SalSubLocId", DBNull.Value );
        p_SalSubLoc.Direction = ParameterDirection.Input;
        p_SalSubLoc.IsNullable = true;
        if (SalSubLoc != "")
            p_SalSubLoc.Value = SalSubLoc;

        //Common.ReturnDate(SubmissionDate);
        objDC.CreateDSFromProc(cmd, "GetSalSheetSOFWise");
        return objDC.ds.Tables["GetSalSheetSOFWise"];
    }

    #endregion

    //Get_Salary_ReconDetail

    public DataTable Get_Salary_ReconDetail(string fdate,string sEmpTypeID)
    {
        SqlCommand cmd = new SqlCommand("proc_Rpt_SalaryReconDtls");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMonth", SqlDbType.DateTime);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = fdate;

        SqlParameter p_EType = cmd.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EType.Direction = ParameterDirection.Input;
        p_EType.Value = Convert.ToInt32(sEmpTypeID);

        objDC.CreateDSFromProc(cmd, "GetSalReconDetail");
        return objDC.ds.Tables["GetSalReconDetail"];
    }

    public DataTable Get_PaySleepWithTax(string P_Month, string P_Year, string P_EmpID)//, string strSector, string strPostDist
    {
        SqlCommand cmd = new SqlCommand("proc_RPT_PaySleepWithTax");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_P_VMonth = cmd.Parameters.Add("P_VMonth", SqlDbType.Int);
        p_P_VMonth.Direction = ParameterDirection.Input;
        p_P_VMonth.Value = 0; 

        SqlParameter SP_VYear = cmd.Parameters.Add("P_VYear", SqlDbType.Int);
        SP_VYear.Direction = ParameterDirection.Input;
        SP_VYear.Value = 0;  

        SqlParameter p_P_EmpID = cmd.Parameters.Add("EmpId", SqlDbType.Char);
        p_P_EmpID.Direction = ParameterDirection.Input;
        p_P_EmpID.Value = P_EmpID;

        //SqlParameter p_SectorId = cmd.Parameters.Add("SectorId", SqlDbType.BigInt);
        //p_SectorId.Direction = ParameterDirection.Input;
        //p_SectorId.Value = strSector;

        //SqlParameter p_PostingDistId = cmd.Parameters.Add("PostingDistId", SqlDbType.BigInt);
        //p_PostingDistId.Direction = ParameterDirection.Input;
        //p_PostingDistId.Value = strPostDist;

        objDC.CreateDSFromProc(cmd, "GetSalReconDetail");
        return objDC.ds.Tables["GetSalReconDetail"];
    }

    public DataTable Get_CD_Voucher(string FisYear, string P_Month, string P_Year, string AccNo, string SalHeadID,
        string salLoc, string deaCall, string accNo, string vouType, string SEmpTypeID)
    {
        SqlCommand cmd = new SqlCommand("proc_RPT_CD_Voucher");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FisYear = cmd.Parameters.Add("FisYear", SqlDbType.Char);
        p_FisYear.Direction = ParameterDirection.Input;
        p_FisYear.Value = FisYear;

        SqlParameter p_P_VMonth = cmd.Parameters.Add("P_VMonth", SqlDbType.Int);
        p_P_VMonth.Direction = ParameterDirection.Input;
        p_P_VMonth.Value = P_Month;

        SqlParameter SP_VYear = cmd.Parameters.Add("P_VYear", SqlDbType.Int);
        SP_VYear.Direction = ParameterDirection.Input;
        SP_VYear.Value = P_Year;

        SqlParameter p_VouType = cmd.Parameters.Add("SalHeadID", SqlDbType.Char);
        p_VouType.Direction = ParameterDirection.Input;
        p_VouType.Value = SalHeadID;

        SqlParameter p_salLoc = cmd.Parameters.Add("SalLoc", SqlDbType.Char);
        p_salLoc.Direction = ParameterDirection.Input;
        p_salLoc.Value = salLoc;

        SqlParameter p_DeaColl = cmd.Parameters.Add("DeaColl", SqlDbType.Char);
        p_DeaColl.Direction = ParameterDirection.Input;
        p_DeaColl.Value = deaCall;

        SqlParameter p_AccNo = cmd.Parameters.Add("AccNo", SqlDbType.Char);
        p_AccNo.Direction = ParameterDirection.Input;
        p_AccNo.Value = accNo;

        SqlParameter p_vouType = cmd.Parameters.Add("vouType", SqlDbType.Char);
        p_vouType.Direction = ParameterDirection.Input;
        p_vouType.Value = vouType;

        SqlParameter p_EType = cmd.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EType.Direction = ParameterDirection.Input;
        p_EType.Value = Convert.ToInt32(SEmpTypeID);

        objDC.CreateDSFromProc(cmd, "GetSalCDV");
        return objDC.ds.Tables["GetSalCDV"];
    }

    public DataTable Get_Bonus_Voucher(string FisYear, string P_Month, string Festival,string salLoc, string AccNo,string SEmpTypeID)
    {
        SqlCommand cmd = new SqlCommand("proc_RPT_Bonus_Voucher");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FisYear = cmd.Parameters.Add("FisYear", SqlDbType.Char);
        p_FisYear.Direction = ParameterDirection.Input;
        p_FisYear.Value = FisYear;

        SqlParameter p_P_VMonth = cmd.Parameters.Add("P_VMonth", SqlDbType.Int);
        p_P_VMonth.Direction = ParameterDirection.Input;
        p_P_VMonth.Value = P_Month;

        SqlParameter p_salLoc = cmd.Parameters.Add("SalLoc", SqlDbType.Char);
        p_salLoc.Direction = ParameterDirection.Input;
        p_salLoc.Value = salLoc;

        SqlParameter p_AccNo = cmd.Parameters.Add("AccNo", SqlDbType.Char);
        p_AccNo.Direction = ParameterDirection.Input;
        p_AccNo.Value = AccNo;

        SqlParameter p_EType = cmd.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EType.Direction = ParameterDirection.Input;
        p_EType.Value = Convert.ToInt32(SEmpTypeID);

        objDC.CreateDSFromProc(cmd, "GetBonusV");
        return objDC.ds.Tables["GetBonusV"];
    }

    public DataTable Get_Rpt_BankStatement(string FisYear, string P_Month, string P_Year, string DivisionId, string SEmpTypeID, string strSalType)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Rpt_BankStatement");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FisYear = cmd.Parameters.Add("FisYear", SqlDbType.Char);
        p_FisYear.Direction = ParameterDirection.Input;
        p_FisYear.Value = FisYear;

        SqlParameter p_P_VMonth = cmd.Parameters.Add("VMonth", SqlDbType.Char);
        p_P_VMonth.Direction = ParameterDirection.Input;
        p_P_VMonth.Value = Convert.ToInt16(P_Month);

        SqlParameter SP_VYear = cmd.Parameters.Add("VYear", SqlDbType.Char);
        SP_VYear.Direction = ParameterDirection.Input;
        SP_VYear.Value = P_Year;

        SqlParameter p_DivisionId = cmd.Parameters.Add("DivisionId", SqlDbType.Char);
        p_DivisionId.Direction = ParameterDirection.Input;
        p_DivisionId.Value = DivisionId;

        SqlParameter p_EType = cmd.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EType.Direction = ParameterDirection.Input;
        p_EType.Value = Convert.ToInt32(SEmpTypeID);

        SqlParameter p_SalType = cmd.Parameters.Add("SalaryType", SqlDbType.Char);
        p_SalType.Direction = ParameterDirection.Input;
        p_SalType.Value = strSalType;

        objDC.CreateDSFromProc(cmd, "GetBankStatemENT");
        return objDC.ds.Tables["GetBankStatemENT"];
    }
   
    public DataTable Get_Rpt_PayrollBasicWChargiong(string P_Month, string P_Year, string SalSubLoc,string PostDist,string EmpID)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Rpt_BasicWiseCharging");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_P_VMonth = cmd.Parameters.Add("P_VMonth", SqlDbType.Char);
        p_P_VMonth.Direction = ParameterDirection.Input;
        p_P_VMonth.Value = Convert.ToInt16(P_Month);

        SqlParameter SP_VYear = cmd.Parameters.Add("P_VYear", SqlDbType.Char);
        SP_VYear.Direction = ParameterDirection.Input;
        SP_VYear.Value = P_Year;

        SqlParameter p_SalSubLoc = cmd.Parameters.Add("SalSubLocId", SqlDbType.Char);
        p_SalSubLoc.Direction = ParameterDirection.Input;
        p_SalSubLoc.Value = SalSubLoc;

        SqlParameter p_PostDist = cmd.Parameters.Add("PostDistId", SqlDbType.Char);
        p_PostDist.Direction = ParameterDirection.Input;
        p_PostDist.Value = PostDist;

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        objDC.CreateDSFromProc(cmd, "GetPayBasiWCharg");
        return objDC.ds.Tables["GetPayBasiWCharg"];
    }

    public DataTable Get_Rpt_EffortReport(string P_Month, string P_Year, string SalSubLoc, string PostDist, string EmpID)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Rpt_EffortReport");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_P_VMonth = cmd.Parameters.Add("P_VMonth", SqlDbType.Char);
        p_P_VMonth.Direction = ParameterDirection.Input;
        p_P_VMonth.Value = Convert.ToInt16(P_Month);

        SqlParameter SP_VYear = cmd.Parameters.Add("P_VYear", SqlDbType.Char);
        SP_VYear.Direction = ParameterDirection.Input;
        SP_VYear.Value = P_Year;

        SqlParameter p_SalSubLoc = cmd.Parameters.Add("SalSubLocId", SqlDbType.Char);
        p_SalSubLoc.Direction = ParameterDirection.Input;
        p_SalSubLoc.Value = SalSubLoc;

        SqlParameter p_PostDist = cmd.Parameters.Add("PostDistId", SqlDbType.Char);
        p_PostDist.Direction = ParameterDirection.Input;
        p_PostDist.Value = PostDist;

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        objDC.CreateDSFromProc(cmd, "GetEffortRpt");
        return objDC.ds.Tables["GetEffortRpt"];
    }

    public DataTable Get_Rpt_SalarySourceWiseSalDisReport(string P_Month, string P_Year, string SalSubLoc, string PostDist, string EmpID, string SalSourceID)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Rpt_SalarySourceWSDistibution");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_P_VMonth = cmd.Parameters.Add("P_VMonth", SqlDbType.Char);
        p_P_VMonth.Direction = ParameterDirection.Input;
        p_P_VMonth.Value = Convert.ToInt16(P_Month);

        SqlParameter SP_VYear = cmd.Parameters.Add("P_VYear", SqlDbType.Char);
        SP_VYear.Direction = ParameterDirection.Input;
        SP_VYear.Value = P_Year;

        SqlParameter p_SalSubLoc = cmd.Parameters.Add("SalSubLocId", SqlDbType.Char);
        p_SalSubLoc.Direction = ParameterDirection.Input;
        p_SalSubLoc.Value = SalSubLoc;

        SqlParameter p_PostDist = cmd.Parameters.Add("PostDistId", SqlDbType.Char);
        p_PostDist.Direction = ParameterDirection.Input;
        p_PostDist.Value = PostDist;

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        SqlParameter p_SalSourceID = cmd.Parameters.Add("SalarySourceId", SqlDbType.VarChar);
        p_SalSourceID.Direction = ParameterDirection.Input;
        p_SalSourceID.Value = SalSourceID;

        objDC.CreateDSFromProc(cmd, "GetEffortRpt");
        return objDC.ds.Tables["GetEffortRpt"];
    }

    public DataTable Get_Rpt_PayrollReportLocWise(string P_Month, string P_Year, string SalSubLoc, string PostDist, string EmpID)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Rpt_PayrollRptLocWise");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_P_VMonth = cmd.Parameters.Add("P_VMonth", SqlDbType.Char);
        p_P_VMonth.Direction = ParameterDirection.Input;
        p_P_VMonth.Value = Convert.ToInt16(P_Month);

        SqlParameter SP_VYear = cmd.Parameters.Add("P_VYear", SqlDbType.Char);
        SP_VYear.Direction = ParameterDirection.Input;
        SP_VYear.Value = P_Year;

        SqlParameter p_SalSubLoc = cmd.Parameters.Add("SalSubLocId", SqlDbType.Char);
        p_SalSubLoc.Direction = ParameterDirection.Input;
        p_SalSubLoc.Value = SalSubLoc;

        SqlParameter p_PostDist = cmd.Parameters.Add("PostDistId", SqlDbType.Char);
        p_PostDist.Direction = ParameterDirection.Input;
        p_PostDist.Value = PostDist;

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        objDC.CreateDSFromProc(cmd, "dtPayrollReportLocWise");
        return objDC.ds.Tables["dtPayrollReportLocWise"];
    }

    public DataTable Get_Rpt_CompWsSalaryCharging(string P_Month, string P_Year, string SalSubLoc, string PostDist, string EmpID)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Rpt_CompWsSalaryCharging");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_P_VMonth = cmd.Parameters.Add("P_VMonth", SqlDbType.Char);
        p_P_VMonth.Direction = ParameterDirection.Input;
        p_P_VMonth.Value = Convert.ToInt16(P_Month);

        SqlParameter SP_VYear = cmd.Parameters.Add("P_VYear", SqlDbType.Char);
        SP_VYear.Direction = ParameterDirection.Input;
        SP_VYear.Value = P_Year;

        SqlParameter p_SalSubLoc = cmd.Parameters.Add("SalSubLocId", SqlDbType.Char);
        p_SalSubLoc.Direction = ParameterDirection.Input;
        p_SalSubLoc.Value = SalSubLoc;

        SqlParameter p_PostDist = cmd.Parameters.Add("PostDistId", SqlDbType.Char);
        p_PostDist.Direction = ParameterDirection.Input;
        p_PostDist.Value = PostDist;

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        objDC.CreateDSFromProc(cmd, "dtCompWsSalaryCharging");
        return objDC.ds.Tables["dtCompWsSalaryCharging"];
    }

    public DataTable Get_Rpt_PayrollReportEmpCurrCharging(string P_Month, string P_Year, string SalSubLoc, string PostDist, string EmpID)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Rpt_EmpCurrCharging");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_P_VMonth = cmd.Parameters.Add("P_VMonth", SqlDbType.Char);
        p_P_VMonth.Direction = ParameterDirection.Input;
        p_P_VMonth.Value = Convert.ToInt16(P_Month);

        SqlParameter SP_VYear = cmd.Parameters.Add("P_VYear", SqlDbType.Char);
        SP_VYear.Direction = ParameterDirection.Input;
        SP_VYear.Value = P_Year;

        SqlParameter p_SalSubLoc = cmd.Parameters.Add("SalSubLocId", SqlDbType.Char);
        p_SalSubLoc.Direction = ParameterDirection.Input;
        p_SalSubLoc.Value = SalSubLoc;

        SqlParameter p_PostDist = cmd.Parameters.Add("PostDistId", SqlDbType.Char);
        p_PostDist.Direction = ParameterDirection.Input;
        p_PostDist.Value = PostDist;

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        objDC.CreateDSFromProc(cmd, "dtEmpCurrCharging");
        return objDC.ds.Tables["dtEmpCurrCharging"];
    }

    public DataTable Get_Rpt_NetSalarySourceWiseSalDisReport(string P_Month, string P_Year, string SalSubLoc)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Rpt_NetSalarySourceWSDistibution");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_P_VMonth = cmd.Parameters.Add("P_VMonth", SqlDbType.Char);
        p_P_VMonth.Direction = ParameterDirection.Input;
        p_P_VMonth.Value = Convert.ToInt16(P_Month);

        SqlParameter SP_VYear = cmd.Parameters.Add("P_VYear", SqlDbType.Char);
        SP_VYear.Direction = ParameterDirection.Input;
        SP_VYear.Value = P_Year;

        SqlParameter p_SalSubLoc = cmd.Parameters.Add("SalSubLocId", SqlDbType.Char);
        p_SalSubLoc.Direction = ParameterDirection.Input;
        p_SalSubLoc.Value = SalSubLoc;
        
        objDC.CreateDSFromProc(cmd, "dtNetSalarySourceWiseSalDis");
        return objDC.ds.Tables["dtNetSalarySourceWiseSalDis"];
    }

    public DataTable Get_Rpt_FestivalBonusCharging(string P_Month, string FisYear, string SalLoc, string SalSubLoc, string EmpID,
        string Festival, string SalSourceID, string strUSDRate, string sEmpTypeID)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Rpt_EmpBonusCurrCharging");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_P_VMonth = cmd.Parameters.Add("P_VMonth", SqlDbType.Int);
        p_P_VMonth.Direction = ParameterDirection.Input;
        p_P_VMonth.Value = Convert.ToInt16(P_Month);

        SqlParameter SP_VYear = cmd.Parameters.Add("FisYear", SqlDbType.Int);
        SP_VYear.Direction = ParameterDirection.Input;
        SP_VYear.Value = FisYear;
        SqlParameter p_PostDist = cmd.Parameters.Add("SalLoc", SqlDbType.Char);
        p_PostDist.Direction = ParameterDirection.Input;
        p_PostDist.Value = SalLoc;

        SqlParameter p_SalSubLoc = cmd.Parameters.Add("SalSubLocId", SqlDbType.Char);
        p_SalSubLoc.Direction = ParameterDirection.Input;
        p_SalSubLoc.Value = SalSubLoc;

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        SqlParameter p_Festival = cmd.Parameters.Add("Festival", SqlDbType.Int);
        p_Festival.Direction = ParameterDirection.Input;
        p_Festival.Value = Festival;

        SqlParameter p_SalSourceID = cmd.Parameters.Add("SalSourceID", SqlDbType.Char);
        p_SalSourceID.Direction = ParameterDirection.Input;
        p_SalSourceID.Value = SalSourceID;

        SqlParameter p_USDRate = cmd.Parameters.Add("USDRate", SqlDbType.Decimal);
        p_USDRate.Direction = ParameterDirection.Input;
        p_USDRate.Value = Convert.ToDecimal(strUSDRate);

        SqlParameter p_EType = cmd.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EType.Direction = ParameterDirection.Input;
        p_EType.Value = Convert.ToInt32(sEmpTypeID);
      
        objDC.CreateDSFromProc(cmd, "dtEmpBonusCurrCharging");
        return objDC.ds.Tables["dtEmpBonusCurrCharging"];
    }

    public DataTable Get_Rpt_MedicalReport(string FisYear, string sEmpTypeID)
    {
        SqlCommand cmd = new SqlCommand("proc_Rpt_MedicalHospitality");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FisYear = cmd.Parameters.Add("FYear", SqlDbType.BigInt);
        p_FisYear.Direction = ParameterDirection.Input;
        p_FisYear.Value = Convert.ToInt32(FisYear);

         SqlParameter p_EType = cmd.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
         p_EType.Direction = ParameterDirection.Input;
         p_EType.Value = Convert.ToInt32(sEmpTypeID);

        objDC.CreateDSFromProc(cmd, "dtMedicalHospitlity");
        return objDC.ds.Tables["dtMedicalHospitlity"];
    }

    public DataTable Get_Rpt_AddRequirementAllow(string P_Month, string P_Year)
    {
        SqlCommand cmd = new SqlCommand("proc_Rpt_AddRequirementAllow");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_P_VMonth = cmd.Parameters.Add("P_VMonth", SqlDbType.Char);
        p_P_VMonth.Direction = ParameterDirection.Input;
        p_P_VMonth.Value = Convert.ToInt16(P_Month);

        SqlParameter SP_VYear = cmd.Parameters.Add("P_VYear", SqlDbType.Char);
        SP_VYear.Direction = ParameterDirection.Input;
        SP_VYear.Value = P_Year;

        objDC.CreateDSFromProc(cmd, "dtAddRequirementAllow");
        return objDC.ds.Tables["dtAddRequirementAllow"];
    }

    public DataTable Get_Rpt_EmpPromotionHistory(string SalDiv, string VMonth, string VYear, string Grade, string Desig,
                                        string FDate, string TDate,string EmpID)
    {
        SqlCommand cmd = new SqlCommand("Proc_GET_EmpTranaition");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_SalLoc = cmd.Parameters.Add("SalLoc", SqlDbType.BigInt);
        p_SalLoc.Direction = ParameterDirection.Input;
        if (SalDiv=="")
            p_SalLoc.Value = -1;
        else
        p_SalLoc.Value = SalDiv;

        SqlParameter p_P_VMonth = cmd.Parameters.Add("P_VMonth", SqlDbType.BigInt);
        p_P_VMonth.Direction = ParameterDirection.Input;
        p_P_VMonth.Value = Convert.ToInt16(VMonth);

        SqlParameter SP_VYear = cmd.Parameters.Add("P_VYear", SqlDbType.BigInt);
        SP_VYear.Direction = ParameterDirection.Input;
        SP_VYear.Value = VYear;

        SqlParameter SP_GradeType = cmd.Parameters.Add("GradeType", SqlDbType.BigInt);
        SP_GradeType.Direction = ParameterDirection.Input;
        if (Grade=="")
            SP_GradeType.Value = -1;
        else
        SP_GradeType.Value = Grade;

        SqlParameter SP_DesigID = cmd.Parameters.Add("DesigID", SqlDbType.BigInt);
        SP_DesigID.Direction = ParameterDirection.Input;
        if (Desig=="")
        SP_DesigID.Value = -1;
        else
        SP_DesigID.Value = Desig;

        SqlParameter p_FDate = cmd.Parameters.Add("FDate", DBNull.Value);
        p_FDate.Direction = ParameterDirection.Input;
        p_FDate.IsNullable = true;
        if (FDate != "")
            p_FDate.Value = Common.ReturnDate(FDate);

        SqlParameter SP_TDate = cmd.Parameters.Add("TDate", DBNull.Value);
        SP_TDate.Direction = ParameterDirection.Input;
        SP_TDate.IsNullable = true;
        if (TDate != "")
            SP_TDate.Value = Common.ReturnDate(TDate);
      

        SqlParameter SP_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        SP_EmpID.Direction = ParameterDirection.Input;
        SP_EmpID.Value = EmpID;

        objDC.CreateDSFromProc(cmd, "dtEmpTranaition");
        return objDC.ds.Tables["dtEmpTranaition"];
    }

    public DataTable Get_Rpt_EmpTransferReport(string SalDiv, string VMonth, string VYear, string Grade, string Desig,
                                    string FDate, string TDate, string EmpID)
    {
        SqlCommand cmd = new SqlCommand("Proc_GET_EmpTransfer");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_SalLoc = cmd.Parameters.Add("SalLoc", SqlDbType.BigInt);
        p_SalLoc.Direction = ParameterDirection.Input;
        if (SalDiv == "")
            p_SalLoc.Value = -1;
        else
            p_SalLoc.Value = SalDiv;

        SqlParameter p_P_VMonth = cmd.Parameters.Add("P_VMonth", SqlDbType.BigInt);
        p_P_VMonth.Direction = ParameterDirection.Input;
        p_P_VMonth.Value = Convert.ToInt16(VMonth);

        SqlParameter SP_VYear = cmd.Parameters.Add("P_VYear", SqlDbType.BigInt);
        SP_VYear.Direction = ParameterDirection.Input;
        SP_VYear.Value = VYear;

        SqlParameter SP_GradeType = cmd.Parameters.Add("GradeType", SqlDbType.BigInt);
        SP_GradeType.Direction = ParameterDirection.Input;
        if (Grade == "")
            SP_GradeType.Value = -1;
        else
            SP_GradeType.Value = Grade;

        SqlParameter SP_DesigID = cmd.Parameters.Add("DesigID", SqlDbType.BigInt);
        SP_DesigID.Direction = ParameterDirection.Input;
        if (Desig == "")
            SP_DesigID.Value = -1;
        else
            SP_DesigID.Value = Desig;

        SqlParameter p_FDate = cmd.Parameters.Add("FDate", DBNull.Value);
        p_FDate.Direction = ParameterDirection.Input;
        p_FDate.IsNullable = true;
        if (FDate != "")
            p_FDate.Value = Common.ReturnDate(FDate);

        SqlParameter SP_TDate = cmd.Parameters.Add("TDate", DBNull.Value);
        SP_TDate.Direction = ParameterDirection.Input;
        SP_TDate.IsNullable = true;
        if (TDate != "")
            SP_TDate.Value = Common.ReturnDate(TDate);
       
        SqlParameter SP_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        SP_EmpID.Direction = ParameterDirection.Input;
        SP_EmpID.Value = EmpID;

        objDC.CreateDSFromProc(cmd, "dtEmpTransfer");
        return objDC.ds.Tables["dtEmpTransfer"];
    }

    public DataTable Get_Rpt_EmpChangeStatusReport(string SalDiv, string VMonth, string VYear, string Grade, string Desig,
                                string FDate, string TDate, string EmpID)
    {
        SqlCommand cmd = new SqlCommand("Proc_GET_EmpChangeStatus");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_SalLoc = cmd.Parameters.Add("SalLoc", SqlDbType.BigInt);
        p_SalLoc.Direction = ParameterDirection.Input;
        if (SalDiv == "")
            p_SalLoc.Value = -1;
        else
            p_SalLoc.Value = SalDiv;

        SqlParameter p_P_VMonth = cmd.Parameters.Add("P_VMonth", SqlDbType.BigInt);
        p_P_VMonth.Direction = ParameterDirection.Input;
        p_P_VMonth.Value = Convert.ToInt16(VMonth);

        SqlParameter SP_VYear = cmd.Parameters.Add("P_VYear", SqlDbType.BigInt);
        SP_VYear.Direction = ParameterDirection.Input;
        SP_VYear.Value = VYear;

        SqlParameter SP_GradeType = cmd.Parameters.Add("GradeType", SqlDbType.BigInt);
        SP_GradeType.Direction = ParameterDirection.Input;
        if (Grade == "")
            SP_GradeType.Value = -1;
        else
            SP_GradeType.Value = Grade;

        SqlParameter SP_DesigID = cmd.Parameters.Add("DesigID", SqlDbType.BigInt);
        SP_DesigID.Direction = ParameterDirection.Input;
        if (Desig == "")
            SP_DesigID.Value = -1;
        else
            SP_DesigID.Value = Desig;

        SqlParameter p_FDate = cmd.Parameters.Add("FDate", DBNull.Value);
        p_FDate.Direction = ParameterDirection.Input;
        p_FDate.IsNullable = true;
        if (FDate != "")
            p_FDate.Value = Common.ReturnDate(FDate);

        SqlParameter SP_TDate = cmd.Parameters.Add("TDate", DBNull.Value);
        SP_TDate.Direction = ParameterDirection.Input;
        SP_TDate.IsNullable = true;
        if (TDate != "")
            SP_TDate.Value = Common.ReturnDate(TDate);

        SqlParameter SP_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        SP_EmpID.Direction = ParameterDirection.Input;
        SP_EmpID.Value = EmpID;

        objDC.CreateDSFromProc(cmd, "dtEmpChangeStatus");
        return objDC.ds.Tables["dtEmpChangeStatus"];
    }

    public DataTable Get_Rpt_AddDeductMonthRpt(string VMonth, string VYear, string salHead, string sEmpTypeID)
    {
        SqlCommand cmd = new SqlCommand("Proc_Get_AddDuduction");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_P_VMonth = cmd.Parameters.Add("P_VMonth", SqlDbType.BigInt);
        p_P_VMonth.Direction = ParameterDirection.Input;
        p_P_VMonth.Value = Convert.ToInt16(VMonth);

        SqlParameter SP_VYear = cmd.Parameters.Add("P_VYear", SqlDbType.BigInt);
        SP_VYear.Direction = ParameterDirection.Input;
        SP_VYear.Value = VYear;

        SqlParameter S_HeadID = cmd.Parameters.Add("SHeadID", SqlDbType.BigInt);
        S_HeadID.Direction = ParameterDirection.Input;
        S_HeadID.Value = salHead;

        SqlParameter p_EType = cmd.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EType.Direction = ParameterDirection.Input;
        p_EType.Value = Convert.ToInt32(sEmpTypeID);



        objDC.CreateDSFromProc(cmd, "dtAddDeduct");
        return objDC.ds.Tables["dtAddDeduct"];
    }

    public DataTable Get_Rpt_EmpSalChanHistoryRpt(string FDate,string TDate,string EmpID, string Sector,string Dept)
    {
        SqlCommand cmd = new SqlCommand("Proc_Get_EmpSalChanHistoryRpt");
        cmd.CommandType = CommandType.StoredProcedure;
      
        SqlParameter p_FDate = cmd.Parameters.Add("FDate", SqlDbType.DateTime);
        p_FDate.Direction = ParameterDirection.Input;
        p_FDate.Value = Common.ReturnDate(FDate);

        SqlParameter p_TDate = cmd.Parameters.Add("TDate", SqlDbType.DateTime);
        p_TDate.Direction = ParameterDirection.Input;
        p_TDate.Value = Common.ReturnDate(TDate);

        SqlParameter P_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        P_EmpID.Direction = ParameterDirection.Input;
        P_EmpID.Value = EmpID;

        SqlParameter P_Sector = cmd.Parameters.Add("SectorID", SqlDbType.BigInt);
        P_Sector.Direction = ParameterDirection.Input;
        P_Sector.Value =Convert.ToInt32(Sector);

        SqlParameter P_Dept = cmd.Parameters.Add("DeptID", SqlDbType.BigInt);
        P_Dept.Direction = ParameterDirection.Input;
        P_Dept.Value = Convert.ToInt32(Dept);

        objDC.CreateDSFromProc(cmd, "dtEmpSalChanHistoryRpt");
        return objDC.ds.Tables["dtEmpSalChanHistoryRpt"];
    }

    public DataTable Get_Rpt_MedicalBenefitsBalance(string FisYear, string SalSubLoc, string EmpID, string sEmpTypeID)
    {
        SqlCommand cmd = new SqlCommand("proc_Rpt_MedicalBenefitsBalance");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FisYear = cmd.Parameters.Add("FYear", SqlDbType.BigInt);
        p_FisYear.Direction = ParameterDirection.Input;
        p_FisYear.Value = Convert.ToInt32(FisYear);

        SqlParameter p_SalSubLoc = cmd.Parameters.Add("SalSubLocId", SqlDbType.VarChar);
        p_SalSubLoc.Direction = ParameterDirection.Input;
        p_SalSubLoc.Value = Convert.ToInt32(SalSubLoc);

        SqlParameter P_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        P_EmpID.Direction = ParameterDirection.Input;
        P_EmpID.Value = EmpID;

        SqlParameter p_EType = cmd.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EType.Direction = ParameterDirection.Input;
        p_EType.Value = Convert.ToInt32(sEmpTypeID);

        objDC.CreateDSFromProc(cmd, "dtMedicalBenefitsBalance");
        return objDC.ds.Tables["dtMedicalBenefitsBalance"];
    }

    public DataTable Get_Rpt_MonthlyMHReceivedBalance(string VMonth, string FisYear, string SalSubLoc, string EmpId, string BenefitType, string sEmpTypeID)
    {
        SqlCommand cmd = new SqlCommand("proc_Rpt_MonthlyMHReceivedBalance");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMonth = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = Convert.ToInt32(VMonth);

        SqlParameter p_FisYear = cmd.Parameters.Add("FYear", SqlDbType.BigInt);
        p_FisYear.Direction = ParameterDirection.Input;
        p_FisYear.Value = Convert.ToInt32(FisYear);

        SqlParameter p_SalSubLoc = cmd.Parameters.Add("SalSubLocId", SqlDbType.VarChar);
        p_SalSubLoc.Direction = ParameterDirection.Input;
        p_SalSubLoc.Value = Convert.ToInt32(SalSubLoc);

        SqlParameter P_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        P_EmpID.Direction = ParameterDirection.Input;
        P_EmpID.Value = EmpId;

        SqlParameter p_BenefitType = cmd.Parameters.Add("BenefitType", SqlDbType.Char);
        p_BenefitType.Direction = ParameterDirection.Input;
        p_BenefitType.Value = BenefitType;

        SqlParameter p_EType = cmd.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EType.Direction = ParameterDirection.Input;
        p_EType.Value = Convert.ToInt32(sEmpTypeID);

        objDC.CreateDSFromProc(cmd, "dtMonthlyMHReceivedBalance");
        return objDC.ds.Tables["dtMonthlyMHReceivedBalance"];
    }

    //public DataTable Get_Rpt_FaynalPaymentList(string VMonth, string FisYear, string EmpID)
    //{
    //    SqlCommand cmd = new SqlCommand("proc_Rpt_FaynalPaymentList");
    //    cmd.CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_VMonth = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
    //    p_VMonth.Direction = ParameterDirection.Input;
    //    p_VMonth.Value = Convert.ToInt32(VMonth);

    //    SqlParameter p_FisYear = cmd.Parameters.Add("VYear", SqlDbType.BigInt);
    //    p_FisYear.Direction = ParameterDirection.Input;
    //    p_FisYear.Value = Convert.ToInt32(FisYear);

    //    SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
    //    p_EmpID.Direction = ParameterDirection.Input;
    //    p_EmpID.Value = EmpID;

    //    objDC.CreateDSFromProc(cmd, "dtFaynalPaymentList");
    //    return objDC.ds.Tables["dtFaynalPaymentList"];
    //}

    public DataSet Get_Rpt_FaynalPaymentList(string VMonth, string VYear, string FisYear, string EmpID, DataSet dsRpt)
    //public DataTable Get_Rpt_FaynalPaymentList(string VMonth, string FisYear, string EmpID)
    {
        SqlCommand cmd = new SqlCommand("proc_Rpt_FaynalPaymentList");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMonth = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = Convert.ToInt32(VMonth);

        SqlParameter p_VYear = cmd.Parameters.Add("VYear", SqlDbType.BigInt);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = Convert.ToInt32(VYear);

        SqlParameter p_FisYear = cmd.Parameters.Add("FiscalYrID", SqlDbType.BigInt);
        p_FisYear.Direction = ParameterDirection.Input;
        p_FisYear.Value = Convert.ToInt32(FisYear);

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        return objDC.Get_Rpt_SalReconDetails(cmd, dsRpt);

        //objDC.CreateDSFromProc(cmd, "dtFaynalPaymentList");
        //return objDC.ds.Tables["dtFaynalPaymentList"];
    }

    public DataTable Get_Rpt_FaynalPaymentDueList(string VMonth, string FisYear, string EmpID)
    {
        SqlCommand cmd = new SqlCommand("proc_Rpt_FaynalPaymentDueList");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMonth = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = Convert.ToInt32(VMonth);

        SqlParameter p_FisYear = cmd.Parameters.Add("VYear", SqlDbType.BigInt);
        p_FisYear.Direction = ParameterDirection.Input;
        p_FisYear.Value = Convert.ToInt32(FisYear);

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        objDC.CreateDSFromProc(cmd, "dtFaynalPaymentDueList");
        return objDC.ds.Tables["dtFaynalPaymentDueList"];
    }

    public DataTable Get_GratuityBenefitsSummery(string VMonth, string Year, string Quarter)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Rpt_GratuityBenefitsSummery");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMonth = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = Convert.ToInt32(VMonth);

        SqlParameter p_Year = cmd.Parameters.Add("VYear", SqlDbType.BigInt);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = Convert.ToInt32(Year);

        SqlParameter p_Quarter = cmd.Parameters.Add("Quater", SqlDbType.BigInt);
        p_Quarter.Direction = ParameterDirection.Input;
        p_Quarter.Value = Quarter;

        objDC.CreateDSFromProc(cmd, "dtrptGratuityBenefitsSummery");
        return objDC.ds.Tables["dtrptGratuityBenefitsSummery"];
    }

    public DataTable Get_GratuityBenefits(string VMonth, string Year, string Quarter)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Rpt_GratuityBenefits");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMonth = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = Convert.ToInt32(VMonth);

        SqlParameter p_Year = cmd.Parameters.Add("VYear", SqlDbType.BigInt);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = Convert.ToInt32(Year);

        SqlParameter p_Quarter = cmd.Parameters.Add("Quater", SqlDbType.BigInt);
        p_Quarter.Direction = ParameterDirection.Input;
        p_Quarter.Value = Quarter;

        objDC.CreateDSFromProc(cmd, "dtrptGratuityBenefits");
        return objDC.ds.Tables["dtrptGratuityBenefits"];
    }

    public DataTable Get_MonthlyPFContribution(string FisYear, string VMonth, string FisYearP, string VMonthP,string SalDiv , string EmpID)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Rpt_MonthlyPFContribution");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Year = cmd.Parameters.Add("FisYear", SqlDbType.BigInt);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = Convert.ToInt32(FisYear);

        SqlParameter p_VMonth = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = Convert.ToInt32(VMonth);

        SqlParameter p_YearP = cmd.Parameters.Add("FisYearP", SqlDbType.BigInt);
        p_YearP.Direction = ParameterDirection.Input;
        p_YearP.Value = Convert.ToInt32(FisYearP);

        SqlParameter p_VMonthP = cmd.Parameters.Add("VMonthP", SqlDbType.BigInt);
        p_VMonthP.Direction = ParameterDirection.Input;
        p_VMonthP.Value = Convert.ToInt32(VMonthP);

        SqlParameter p_SalDiv = cmd.Parameters.Add("SalDiv", SqlDbType.Char);
        p_SalDiv.Direction = ParameterDirection.Input;
        p_SalDiv.Value = SalDiv;

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        objDC.CreateDSFromProc(cmd, "dtMonthlyPFContribution");
        return objDC.ds.Tables["dtMonthlyPFContribution"];
    }
   
    //SalarySSourandEmpWise
    public DataTable Get_SalarySSourandEmpWise(string FisYear, string VMonth, string VYear, string SalDiv, string EmpID, 
        string SalSourceID, string strEmpTypeId,string usdRate)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Rpt_SSheeSAndEmpwise");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Year = cmd.Parameters.Add("FisYear", SqlDbType.BigInt);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = Convert.ToInt32(FisYear);

        SqlParameter p_VMonth = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = Convert.ToInt32(VMonth);

        SqlParameter p_VYear = cmd.Parameters.Add("VYear", SqlDbType.BigInt);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = Convert.ToInt32(VYear);

        SqlParameter p_SalDiv = cmd.Parameters.Add("SalDiv", SqlDbType.Char);
        p_SalDiv.Direction = ParameterDirection.Input;
        p_SalDiv.Value = SalDiv;

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        SqlParameter p_SalSourceID = cmd.Parameters.Add("SalarySourceId", SqlDbType.VarChar);
        p_SalSourceID.Direction = ParameterDirection.Input;
        p_SalSourceID.Value = SalSourceID;

        SqlParameter p_EmpTypeId = cmd.Parameters.Add("EmpTypeId", SqlDbType.VarChar);
        p_EmpTypeId.Direction = ParameterDirection.Input;
        p_EmpTypeId.Value = strEmpTypeId;

        SqlParameter p_USDRate = cmd.Parameters.Add("USDRate", SqlDbType.Decimal);
        p_USDRate.Direction = ParameterDirection.Input;
        p_USDRate.Value = Convert.ToDecimal(usdRate);
        
        objDC.CreateDSFromProc(cmd, "dtSalarySSourandEmpWise");
        return objDC.ds.Tables["dtSalarySSourandEmpWise"];
    }

    // Get_AnnualReport
    public DataTable Get_AnnualReport(string FisYear, string SalDiv, string EmpID, string sHeadtype, string sEmpTypeID)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Rpt_AnnualReport");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Year = cmd.Parameters.Add("FisYear", SqlDbType.BigInt);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = Convert.ToInt32(FisYear);

        SqlParameter p_SalDiv = cmd.Parameters.Add("SalDiv", SqlDbType.Char);
        p_SalDiv.Direction = ParameterDirection.Input;
        p_SalDiv.Value = SalDiv;

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        SqlParameter p_Headtype = cmd.Parameters.Add("Headtype", SqlDbType.Char);
        p_Headtype.Direction = ParameterDirection.Input;
        p_Headtype.Value = sHeadtype;

         SqlParameter p_EType = cmd.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EType.Direction = ParameterDirection.Input;
        p_EType.Value = Convert.ToInt32(sEmpTypeID);

        objDC.CreateDSFromProc(cmd, "dtAnnualReport");
        return objDC.ds.Tables["dtAnnualReport"];
    }
    public DataTable Get_YearlyPFBalance(string FisYear, string SalDiv, string EmpID, string sHeadtype, string sEmpTypeID)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Rpt_YearlyPFBalance");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Year = cmd.Parameters.Add("FisYear", SqlDbType.BigInt);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = Convert.ToInt32(FisYear);

        SqlParameter p_SalDiv = cmd.Parameters.Add("SalDiv", SqlDbType.Char);
        p_SalDiv.Direction = ParameterDirection.Input;
        p_SalDiv.Value = SalDiv;

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        SqlParameter p_Headtype = cmd.Parameters.Add("Headtype", SqlDbType.Char);
        p_Headtype.Direction = ParameterDirection.Input;
        p_Headtype.Value = sHeadtype;

        SqlParameter p_EType = cmd.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EType.Direction = ParameterDirection.Input;
        p_EType.Value = Convert.ToInt32(sEmpTypeID);

        objDC.CreateDSFromProc(cmd, "dtYearlyPFBalance");
        return objDC.ds.Tables["dtYearlyPFBalance"];
    }
    // Get_AnnualReport
    public DataTable Get_TaxDedMonthWise(string VMonth, string FisYear, string SalDiv, string EmpID,  string sEmpTypeID)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Rpt_TaxDedMonthWise");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMonth = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = Convert.ToInt32(VMonth);

        SqlParameter p_Year = cmd.Parameters.Add("FisYear", SqlDbType.BigInt);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = Convert.ToInt32(FisYear);

        SqlParameter p_SalDiv = cmd.Parameters.Add("SalDiv", SqlDbType.Char);
        p_SalDiv.Direction = ParameterDirection.Input;
        p_SalDiv.Value = SalDiv;

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;
              
        SqlParameter p_EType = cmd.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EType.Direction = ParameterDirection.Input;
        p_EType.Value = Convert.ToInt32(sEmpTypeID);

        objDC.CreateDSFromProc(cmd, "dtTaxDedMonthWise");
        return objDC.ds.Tables["dtTaxDedMonthWise"];
    }

    // Computation of Income Tax
    public DataTable Get_ITComputation(string FisYear, string VMonth, string EmpID)
    {
        SqlCommand cmd = new SqlCommand("PROC_PAYROLL_SELECT_ComputationofIT");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Year = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = Convert.ToInt32(FisYear);

        SqlParameter p_VMonth = cmd.Parameters.Add("VMonth", SqlDbType.Char);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = VMonth;

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        objDC.CreateDSFromProc(cmd, "dtITComputation");
        DataTable dtITComputation = new DataTable();
        dtITComputation = objDC.ds.Tables["dtITComputation"];

        //objDC.CreateDSFromProc(cmd, "dtITComputation");
        //return objDC.ds.Tables["dtITComputation"];


        dsPayroll_CrystalReport objdsPay = new dsPayroll_CrystalReport();
        string sEmpId = "";
        Int32 iMonth = 7;
        string sBasicSalary = "";
        string sGrossSalary = "";
        string sLastTaxPayMonth = "";
        DataRow FinalRow;
        if (dtITComputation.Rows.Count > 0)
        {
            //sEmpId = dtITComputation.Rows[0]["EmpId"].ToString().Trim();

            foreach (DataRow dRow in dtITComputation.Rows)
            {
                DataRow[] foundRow;

                if (sEmpId != dRow["EmpId"].ToString().Trim())
                {
                    foundRow = dtITComputation.Select("EmpId='" + dRow["EmpId"].ToString().Trim() + "'");
                    if (foundRow.Length > 0)
                    {
                        foreach (DataRow fRow in foundRow)
                        {
                            FinalRow = objdsPay.dtITComputation.NewRow();
                            FinalRow["EmpId"] = dtITComputation.Rows[0]["EmpId"].ToString();
                            //sEmpId = dtITComputation.Rows[0]["EmpId"].ToString().Trim();
                            FinalRow["FullName"] = dtITComputation.Rows[0]["FullName"].ToString();
                            FinalRow["SalLocName"] = dtITComputation.Rows[0]["SalLocName"].ToString();
                            FinalRow["YPFDeduction"] = dtITComputation.Rows[0]["YPFDeduction"].ToString();
                            FinalRow["YFestivalBonus"] = dtITComputation.Rows[0]["YFestivalBonus"].ToString();
                            FinalRow["YOverTime"] = Common.ReturnZeroForNull(dtITComputation.Rows[0]["YOverTime"].ToString());
                            //FinalRow["YHouseRent"] = Convert.ToString((Convert.ToDecimal(dtITComputation.Rows[0]["YBasicSalary"]) * 50) / 100);
                            //FinalRow["YMedicalAllowance"] = Convert.ToString((Convert.ToDecimal(dtITComputation.Rows[0]["YBasicSalary"]) * 10) / 100);
                            //FinalRow["YTransportAllowance"] = Convert.ToString((Convert.ToDecimal(dtITComputation.Rows[0]["YBasicSalary"]) * 5) / 100);
                            FinalRow["YHouseRent"] = Common.ReturnZeroForNull(dRow["YHouseRent"].ToString());
                            FinalRow["YMedicalAllowance"] = Convert.ToString((Convert.ToDecimal(dtITComputation.Rows[0]["YBasicSalary"]) * 10) / 100);
                            FinalRow["YTransportAllowance"] = Common.ReturnZeroForNull(dRow["YTransportAllowance"].ToString());
                            FinalRow["TTI_2"] = Common.ReturnZeroForNull(dtITComputation.Rows[0]["TTI_2"].ToString());
                            FinalRow["AssYear"] = dtITComputation.Rows[0]["AssYear"].ToString();

                            #region Tax Liability
                            decimal dclRemainTaxLiable = 0;
                            if (Convert.ToDecimal(dtITComputation.Rows[0]["TTI_2"].ToString()) > 250000)
                            {
                                FinalRow["TaxLiableP0"] = "250000";
                                dclRemainTaxLiable = Convert.ToDecimal(dtITComputation.Rows[0]["TTI_2"].ToString()) - 250000;
                            }
                            else
                            {
                                FinalRow["TaxLiableP0"] = dclRemainTaxLiable;
                                dclRemainTaxLiable = 0;
                            }

                            if (dclRemainTaxLiable > 400000)
                            {
                                FinalRow["TaxLiableP10"] = "400000";
                                dclRemainTaxLiable = dclRemainTaxLiable - 400000;
                            }
                            else
                            {
                                FinalRow["TaxLiableP10"] = dclRemainTaxLiable;
                                dclRemainTaxLiable = 0;
                            }

                            if (dclRemainTaxLiable > 500000)
                            {
                                FinalRow["TaxLiableP15"] = "500000";
                                dclRemainTaxLiable = dclRemainTaxLiable - 500000;
                            }
                            else
                            {
                                FinalRow["TaxLiableP15"] = dclRemainTaxLiable;
                                dclRemainTaxLiable = 0;
                            }

                            if (dclRemainTaxLiable > 600000)
                            {
                                FinalRow["TaxLiableP20"] = "600000";
                                dclRemainTaxLiable = dclRemainTaxLiable - 600000;
                            }
                            else
                            {
                                FinalRow["TaxLiableP20"] = dclRemainTaxLiable;
                                dclRemainTaxLiable = 0;
                            }

                            if (dclRemainTaxLiable > 3000000)
                            {
                                FinalRow["TaxLiableP25"] = "3000000";
                                dclRemainTaxLiable = dclRemainTaxLiable - 3000000;
                            }
                            else
                            {
                                FinalRow["TaxLiableP25"] = dclRemainTaxLiable;
                                dclRemainTaxLiable = 0;
                            }

                            if (dclRemainTaxLiable > 3000000)
                            {
                                FinalRow["TaxLiableP30"] = "3000000";
                                dclRemainTaxLiable = dclRemainTaxLiable - 3000000;
                            }
                            else
                            {
                                FinalRow["TaxLiableP30"] = dclRemainTaxLiable;
                                dclRemainTaxLiable = 0;
                            }
                            #endregion

                            FinalRow["VMonth"] = Common.ReturnFullMonthName(fRow["VMonth"].ToString());
                            sLastTaxPayMonth = fRow["VMonth"].ToString();
                            FinalRow["TaxAmt"] = fRow["TaxAmt"].ToString();
                            FinalRow["BasicSalary"] = fRow["BasicSalary"].ToString();
                            sBasicSalary = fRow["BasicSalary"].ToString();
                            FinalRow["GrossSalary"] = fRow["GrossSalary"].ToString();
                            sGrossSalary = fRow["GrossSalary"].ToString();

                            FinalRow["P10"] = dtITComputation.Rows[0]["P10"].ToString();
                            FinalRow["P15"] = dtITComputation.Rows[0]["P15"].ToString();
                            FinalRow["P20"] = dtITComputation.Rows[0]["P20"].ToString();
                            FinalRow["P25"] = dtITComputation.Rows[0]["P25"].ToString();
                            FinalRow["P30"] = dtITComputation.Rows[0]["P30"].ToString();                           
                            FinalRow["G_Tax"] = Common.ReturnZeroForNull(dRow["G_Tax"].ToString());
                            FinalRow["Rebate"] = Common.ReturnZeroForNull(dRow["Rebate"].ToString()); //((((Convert.ToDecimal(FinalRow["TTI_2"]) * 30) / 100) * 15) / 100);
                            FinalRow["MonthlyTax"] = Common.ReturnZeroForNull(dRow["MonthlyTax"].ToString());
                            FinalRow["ITDeposited"] = Common.ReturnZeroForNull(dRow["ITDeposited"].ToString());
                            FinalRow["VMonthNo"] = VMonth;

                            objdsPay.dtITComputation.Rows.Add(FinalRow);
                            iMonth++;

                        }
                    }
                    sEmpId = dRow["EmpId"].ToString().Trim();
                }
            }
        }
        objdsPay.dtITComputation.AcceptChanges();
        return objdsPay.dtITComputation;
    }

    //Income Tax Assessment
    public DataTable Get_ITAssessment(string FisYear, string VMonth, string EmpID)
    {
        SqlCommand cmd = new SqlCommand("PROC_PAYROLL_SELECT_ITAssessment");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Year = cmd.Parameters.Add("TaxFiscalYrId", SqlDbType.BigInt);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = Convert.ToInt32(FisYear);

        SqlParameter p_VMonth = cmd.Parameters.Add("VMonth", SqlDbType.Char);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = VMonth;

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        objDC.CreateDSFromProc(cmd, "dtITAssessment");
        DataTable dtITAssessment = new DataTable();
        dtITAssessment = objDC.ds.Tables["dtITAssessment"];

        dsPayroll_CrystalReport objdsPay = new dsPayroll_CrystalReport();
        string sEmpId = "";
        DataRow FinalRow;
        if (dtITAssessment.Rows.Count > 0)
        {
            foreach (DataRow dRow in dtITAssessment.Rows)
            {
                FinalRow = objdsPay.dtITComputation.NewRow();

                if (dRow["EmpId"].ToString().Trim() != sEmpId)
                {
                    FinalRow["AssYear"] = dRow["AssYear"].ToString();
                    FinalRow["EmpId"] = dRow["EmpId"].ToString();
                    sEmpId = dRow["EmpId"].ToString().Trim();
                    FinalRow["FullName"] = dRow["FullName"].ToString();
                    FinalRow["SalLocName"] = dRow["SalLocName"].ToString();
                    FinalRow["BasicSalary"] = dRow["BasicSalary"].ToString();
                    FinalRow["YBasicSalary"] = Common.ReturnZeroForNull(dRow["YBasicSalary"].ToString());
                    FinalRow["YHouseRent"] = Common.ReturnZeroForNull(dRow["YHouseRent"].ToString());
                    FinalRow["YMedicalAllowance"] = Common.ReturnZeroForNull(dRow["YMedicalAllowance"].ToString());
                    FinalRow["YTransportAllowance"] = Common.ReturnZeroForNull(dRow["YTransportAllowance"].ToString());
                    FinalRow["YFestivalBonus"] = Common.ReturnZeroForNull(dRow["YFestivalBonus"].ToString());
                    FinalRow["YPFDeduction"] = Common.ReturnZeroForNull(dRow["YPFDeduction"].ToString());
                    FinalRow["YOverTime"] = Common.ReturnZeroForNull(dRow["YOverTime"].ToString());
                    FinalRow["NetTax"] = Common.ReturnZeroForNull(dRow["NetTax"].ToString());
                    FinalRow["TTI_2"] = Common.ReturnZeroForNull(dRow["TTI_2"].ToString());
                    #region Tax Liability
                    decimal dclRemainTaxLiable = 0;
                    if (Convert.ToDecimal(Common.ReturnZeroForNull(dRow["TTI_2"].ToString())) > 250000)
                    {
                        FinalRow["TaxLiableP0"] = "250000";
                        dclRemainTaxLiable = Convert.ToDecimal(Common.ReturnZeroForNull(dRow["TTI_2"].ToString())) - 250000;
                    }
                    else
                    {
                        FinalRow["TaxLiableP0"] = dclRemainTaxLiable;
                        dclRemainTaxLiable = 0;
                    }

                    if (dclRemainTaxLiable > 400000)
                    {
                        FinalRow["TaxLiableP10"] = "400000";
                        dclRemainTaxLiable = dclRemainTaxLiable - 400000;
                    }
                    else
                    {
                        FinalRow["TaxLiableP10"] = dclRemainTaxLiable;
                        dclRemainTaxLiable = 0;
                    }

                    if (dclRemainTaxLiable > 500000)
                    {
                        FinalRow["TaxLiableP15"] = "500000";
                        dclRemainTaxLiable = dclRemainTaxLiable - 500000;
                    }
                    else
                    {
                        FinalRow["TaxLiableP15"] = dclRemainTaxLiable;
                        dclRemainTaxLiable = 0;
                    }

                    if (dclRemainTaxLiable > 600000)
                    {
                        FinalRow["TaxLiableP20"] = "600000";
                        dclRemainTaxLiable = dclRemainTaxLiable - 600000;
                    }
                    else
                    {
                        FinalRow["TaxLiableP20"] = dclRemainTaxLiable;
                        dclRemainTaxLiable = 0;
                    }

                    if (dclRemainTaxLiable > 3000000)
                    {
                        FinalRow["TaxLiableP25"] = "3000000";
                        dclRemainTaxLiable = dclRemainTaxLiable - 3000000;
                    }
                    else
                    {
                        FinalRow["TaxLiableP25"] = dclRemainTaxLiable;
                        dclRemainTaxLiable = 0;
                    }

                    if (dclRemainTaxLiable > 3000000)
                    {
                        FinalRow["TaxLiableP30"] = "3000000";
                        dclRemainTaxLiable = dclRemainTaxLiable - 3000000;
                    }
                    else
                    {
                        FinalRow["TaxLiableP30"] = dclRemainTaxLiable;
                        dclRemainTaxLiable = 0;
                    }
                    #endregion
                    FinalRow["P10"] = Common.ReturnZeroForNull(dRow["P10"].ToString());
                    FinalRow["P15"] = Common.ReturnZeroForNull(dRow["P15"].ToString());
                    FinalRow["P20"] = Common.ReturnZeroForNull(dRow["P20"].ToString());
                    FinalRow["P25"] = Common.ReturnZeroForNull(dRow["P25"].ToString());
                    FinalRow["P30"] = Common.ReturnZeroForNull(dRow["P30"].ToString());                   
                    FinalRow["G_Tax"] = Common.ReturnZeroForNull(dRow["G_Tax"].ToString());
                    FinalRow["Rebate"] = Common.ReturnZeroForNull(dRow["Rebate"].ToString());// ((((Convert.ToDecimal(FinalRow["TTI_2"]) * 30) / 100) * 15) / 100);
                    FinalRow["MonthlyTax"] = Common.ReturnZeroForNull(dRow["MonthlyTax"].ToString());
                    FinalRow["ITDeposited"] = Common.ReturnZeroForNull(dRow["ITDeposited"].ToString());
                    FinalRow["VMonthNo"] = Common.ReturnZeroForNull(dRow["VMonthNo"].ToString());
                    objdsPay.dtITComputation.Rows.Add(FinalRow);
                    objdsPay.dtITComputation.AcceptChanges();
                }
            }
        }
        return objdsPay.dtITComputation;
    }

    // Get_MonthlyPFContribution
    public DataTable Get_YearlyPFContribution(string FisYear, string SalDiv, string EmpID)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Rpt_YearlyPFContribution");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Year = cmd.Parameters.Add("FisYear", SqlDbType.BigInt);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = Convert.ToInt32(FisYear);

        SqlParameter p_SalDiv = cmd.Parameters.Add("SalDiv", SqlDbType.Char);
        p_SalDiv.Direction = ParameterDirection.Input;
        p_SalDiv.Value = SalDiv;

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        objDC.CreateDSFromProc(cmd, "dtYearlyPFContribution");
        return objDC.ds.Tables["dtYearlyPFContribution"];
    }

    // Get_MonthlyPFContribution
    public DataTable Get_YearlyPFLoanDeduct(string FisYear, string SalDiv, string EmpID)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Rpt_YearlyPFLoanDeduct");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Year = cmd.Parameters.Add("FisYear", SqlDbType.BigInt);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = Convert.ToInt32(FisYear);

        SqlParameter p_SalDiv = cmd.Parameters.Add("SalDiv", SqlDbType.Char);
        p_SalDiv.Direction = ParameterDirection.Input;
        p_SalDiv.Value = SalDiv;

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        objDC.CreateDSFromProc(cmd, "dtYearlyPFLoanDeduct");
        return objDC.ds.Tables["dtYearlyPFLoanDeduct"];
    }
    //Get_AnnualIncome
    public DataTable Get_AnnualIncome(string FisYear, string SalDiv, string EmpID)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Rpt_AnnualIncome");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Year = cmd.Parameters.Add("FisYear", SqlDbType.BigInt);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = Convert.ToInt32(FisYear);

        SqlParameter p_SalDiv = cmd.Parameters.Add("SalDiv", SqlDbType.Char);
        p_SalDiv.Direction = ParameterDirection.Input;
        p_SalDiv.Value = SalDiv;

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        objDC.CreateDSFromProc(cmd, "dtYearlyPFLoanDeduct");
        return objDC.ds.Tables["dtYearlyPFLoanDeduct"];
    }

    public DataSet Get_Salary_ReconAll(string fdate, string sEmpTypeId, DataSet dsRpt)
    {
        SqlCommand cmd = new SqlCommand("proc_Rpt_SalReconDetail");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMonth", SqlDbType.DateTime);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = fdate;

        SqlParameter p_EmpTypeId = cmd.Parameters.Add("EmpTypeId", SqlDbType.BigInt);
        p_EmpTypeId.Direction = ParameterDirection.Input;
        p_EmpTypeId.Value = Convert.ToInt32(sEmpTypeId);

        return objDC.Get_Rpt_SalReconDetails(cmd, dsRpt);
    }

    public DataTable Get_PayslipMonthlyAll(string FisYear, string VMonth, string VYear, string EmpID, string sDesig, string SalSubLocId)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Select_PayslipMonthlyAll");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Year = cmd.Parameters.Add("FisYear", SqlDbType.BigInt);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = Convert.ToInt32(FisYear);

        SqlParameter p_VMonth = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = Convert.ToInt32(VMonth);

        SqlParameter p_VYear = cmd.Parameters.Add("VYear", SqlDbType.BigInt);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = Convert.ToInt32(VYear);

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        SqlParameter p_SalSourceID = cmd.Parameters.Add("DesigId", SqlDbType.BigInt);
        p_SalSourceID.Direction = ParameterDirection.Input;
        p_SalSourceID.Value = Convert.ToInt32(sDesig);


        SqlParameter p_SalDiv = cmd.Parameters.Add("ClinicId", SqlDbType.Char);
        p_SalDiv.Direction = ParameterDirection.Input;
        p_SalDiv.Value = SalSubLocId;


        objDC.CreateDSFromProc(cmd, "dtSalarySSourandEmpWise");
        return objDC.ds.Tables["dtSalarySSourandEmpWise"];
    }
    //MyDataTable = objPayRptMgr.Get_BonusPayslipMonthlyAll(Session["FisYear"].ToString(), Session["VMonth"].ToString(),
    //                 Session["EmpID"].ToString(), Session["Religion"].ToString(), Session["Festival"].ToString(),
    //                 Session["SalLoc"].ToString(), Session["SalSubLoc"].ToString());
    public DataTable Get_BonusPayslipMonthlyAll(string FisYear, string EmpID, string sSalLoc, string sSalSubLoc, string sEmpTypeID)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Select_BonusPayslipMonthly");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Year = cmd.Parameters.Add("FisYear", SqlDbType.BigInt);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = Convert.ToInt32(FisYear);

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        SqlParameter p_SalLoc = cmd.Parameters.Add("SalLocId", SqlDbType.BigInt);
        p_SalLoc.Direction = ParameterDirection.Input;
        p_SalLoc.Value = Convert.ToInt32(sSalLoc);

        SqlParameter p_SalDiv = cmd.Parameters.Add("SalSubLocId", SqlDbType.BigInt);
        p_SalDiv.Direction = ParameterDirection.Input;
        p_SalDiv.Value = Convert.ToInt32(sSalSubLoc);

        SqlParameter p_EType = cmd.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EType.Direction = ParameterDirection.Input;
        p_EType.Value = Convert.ToInt32(sEmpTypeID);

        objDC.CreateDSFromProc(cmd, "dtSalaryBounsPaySlip");
        return objDC.ds.Tables["dtSalaryBounsPaySlip"];
    }
    public DataTable Get_COLA_PerformanceIncrementLetter(string sLetterType, string VMonth, string VYear, string sPostDist)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Select_PerformanceIncrementLetter");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Year = cmd.Parameters.Add("LetterType", SqlDbType.Char);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = sLetterType;

        SqlParameter p_VMonth = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = Convert.ToInt32(VMonth);

        SqlParameter p_VYear = cmd.Parameters.Add("VYear", SqlDbType.BigInt);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = Convert.ToInt32(VYear);

        SqlParameter p_EmpID = cmd.Parameters.Add("PostingPlaceId", SqlDbType.BigInt);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = Convert.ToInt32(sPostDist);

        objDC.CreateDSFromProc(cmd, "dtPerformanceIncrementLetter");
        return objDC.ds.Tables["dtPerformanceIncrementLetter"];
    }

    public DataTable GetIncrementReport(string empId, string salLocId, string SalSubLocId, string sIncType, string VMonth, string VYear)
    {

        SqlCommand command = new SqlCommand("proc_Get_IncrementReport");

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = empId;

        SqlParameter p_SalLocId = command.Parameters.Add("SalLocId", SqlDbType.BigInt);
        p_SalLocId.Direction = ParameterDirection.Input;
        p_SalLocId.Value = Convert.ToInt32(salLocId);

        SqlParameter p_PostingDivId = command.Parameters.Add("SalSubLocId", SqlDbType.BigInt);
        p_PostingDivId.Direction = ParameterDirection.Input;
        p_PostingDivId.Value = Convert.ToInt32(SalSubLocId);

        SqlParameter p_LetterType = command.Parameters.Add("IncType", SqlDbType.Char);
        p_LetterType.Direction = ParameterDirection.Input;
        p_LetterType.Value = sIncType;

        SqlParameter p_VMonth = command.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = Convert.ToInt32(VMonth);

        SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.BigInt);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = Convert.ToInt32(VYear);

        objDC.CreateDSFromProc(command, "tblIncrementReport");
        return objDC.ds.Tables["tblIncrementReport"];
    }

    public DataTable Get_MonthlyBudgetProjection(string VMonth, string VYear)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Rpt_MonthlyBudgetProjection");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMonth = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = Convert.ToInt32(VMonth);

        SqlParameter p_VYear = cmd.Parameters.Add("VYear", SqlDbType.BigInt);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = Convert.ToInt32(VYear);

        objDC.CreateDSFromProc(cmd, "dtMonthlyBudProjection");
        return objDC.ds.Tables["dtMonthlyBudProjection"];
    }

    public DataTable GetOTCalculation(string empId, string salLocId, string SalSubLocId, Int32 vMonth, Int32 vYear, string sEmpTypeID)
    {

        SqlCommand command = new SqlCommand("proc_Get_OTCalculation");

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = empId;

        SqlParameter p_SalLocId = command.Parameters.Add("SalLocId", SqlDbType.BigInt);
        p_SalLocId.Direction = ParameterDirection.Input;
        p_SalLocId.Value = Convert.ToInt32(salLocId);

        SqlParameter p_PostingDivId = command.Parameters.Add("SalSubLocId", SqlDbType.BigInt);
        p_PostingDivId.Direction = ParameterDirection.Input;
        p_PostingDivId.Value = Convert.ToInt32(SalSubLocId);

        if (vMonth == 1)
        {
            vMonth = 12;
            vYear = vYear - 1;
        }
        else
        {
            vMonth = vMonth -1;
            vYear = vYear;
        }

        SqlParameter p_VMonth = command.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = Convert.ToInt32(vMonth);

        SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.BigInt);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = Convert.ToInt32(vYear);

        SqlParameter p_EType = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EType.Direction = ParameterDirection.Input;
        p_EType.Value = Convert.ToInt32(sEmpTypeID);

        objDC.CreateDSFromProc(command, "tblOTCalculation");
        return objDC.ds.Tables["tblOTCalculation"];
    }
    public DataTable GetAccuredVacationSchedule(string vMonth, string vYear,string sFiscalYrId,string sEmpTypeId)
    {
        SqlCommand command = new SqlCommand("proc_Get_AccuredVacation");

        SqlParameter p_VMonth = command.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = Convert.ToInt32(vMonth);

        SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.BigInt);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = Convert.ToInt32(vYear);

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = Convert.ToInt32(sFiscalYrId);

        SqlParameter p_EType = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EType.Direction = ParameterDirection.Input;
        p_EType.Value = Convert.ToInt32(sEmpTypeId);

        objDC.CreateDSFromProc(command, "tblAccuredVacation");
        return objDC.ds.Tables["tblAccuredVacation"];
    }

    public DataTable Get_Rpt_NGOBureauSalaryRpt(string P_Month, string P_Year, string SalSourceID)
    {
        SqlCommand cmd = new SqlCommand("Proc_NGO_Bureau_Salary_Rpt");
        cmd.CommandType = CommandType.StoredProcedure;


        SqlParameter p_P_VMonth = cmd.Parameters.Add("P_VMonth", SqlDbType.Char);
        p_P_VMonth.Direction = ParameterDirection.Input;
        p_P_VMonth.Value = Convert.ToInt16(P_Month);

        SqlParameter SP_VYear = cmd.Parameters.Add("P_VYear", SqlDbType.Char);
        SP_VYear.Direction = ParameterDirection.Input;
        SP_VYear.Value = P_Year;

        SqlParameter p_SalSourceID = cmd.Parameters.Add("SalarySourceID", SqlDbType.Char);
        p_SalSourceID.Direction = ParameterDirection.Input;
        p_SalSourceID.Value = SalSourceID;

        objDC.CreateDSFromProc(cmd, "GetNGOBureauSalary");
        return objDC.ds.Tables["GetNGOBureauSalary"];
    }

    public DataSet Get_Rpt_ITStatement(string sTaxFYrId, string sEmpTypeId, string sEmpID, string sSalLoc, string SalSubLocId)
    {

        SqlCommand cmd = new SqlCommand("Proc_Payroll_Select_FiscalYearWisePayrollDetails_C");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_sTaxFYrId = cmd.Parameters.Add("TaxFiscalYrId", SqlDbType.BigInt);
        p_sTaxFYrId.Direction = ParameterDirection.Input;
        p_sTaxFYrId.Value = Convert.ToInt32(sTaxFYrId);

        SqlParameter p_EmpTypeId = cmd.Parameters.Add("EmpTypeId", SqlDbType.BigInt);
        p_EmpTypeId.Direction = ParameterDirection.Input;
        p_EmpTypeId.Value = Convert.ToInt32(sEmpTypeId);

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = sEmpID;

        SqlParameter p_SalLoc = cmd.Parameters.Add("SalLoc", SqlDbType.VarChar);
        p_SalLoc.Direction = ParameterDirection.Input;
        p_SalLoc.Value = sSalLoc;

        SqlParameter p_SalSubLocId = cmd.Parameters.Add("SalSubLocId", SqlDbType.VarChar);
        p_SalSubLocId.Direction = ParameterDirection.Input;
        p_SalSubLocId.Value = SalSubLocId;

        return objDC.Get_Rpt_SalReconDetails(cmd);
    }

    public DataTable Get_ITDedStatement(string fdate, string FisYear, string SalDivision,string strCompany)
    {
        SqlCommand cmd = new SqlCommand("proc_Rpt_ITDedStatement");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = fdate;

        SqlParameter p_FisYear = cmd.Parameters.Add("FisYear", SqlDbType.BigInt);
        p_FisYear.Direction = ParameterDirection.Input;
        p_FisYear.Value = FisYear;

        SqlParameter p_SalSubLocId = cmd.Parameters.Add("ClinicId", SqlDbType.BigInt);
        p_SalSubLocId.Direction = ParameterDirection.Input;
        p_SalSubLocId.Value = SalDivision;

         SqlParameter p_DivisionId = cmd.Parameters.Add("DivisionId", SqlDbType.BigInt);
        p_DivisionId.Direction = ParameterDirection.Input;
        p_DivisionId.Value = strCompany;

        objDC.CreateDSFromProc(cmd, "GetITDedStatement");
        return objDC.ds.Tables["GetITDedStatement"];
    }
}
