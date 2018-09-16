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
/// Summary description for Payroll_PayslipApprovalManager
/// </summary>
public class Payroll_PayslipApprovalManager
{
    DBConnector objDC = new DBConnector();
    #region Approve Data
    Payroll_MasterMgr objPayMstMgr = new Payroll_MasterMgr();

    public string SaveAndApproveData(GridView gr, string strPSBID, string strEmdID,string strPayID, string strInsBy,string strInsDate)
    {
        DataTable dtGrossHead = objPayMstMgr.SelectGrossSalHead(0);

        SqlCommand[] command = new SqlCommand[gr.Rows.Count + 1];
        // Update Payslip Details Data
        int i = 0;
        decimal decNetPay = 0;
        decimal dclGrossPay = 0;
        foreach (GridViewRow gRow in gr.Rows)
        {
            TextBox txtPAmt = (TextBox)gRow.Cells[2].FindControl("txtPayAmnt");
            decNetPay = decNetPay + Convert.ToDecimal(txtPAmt.Text.Trim());
            if (Common.FindInDataTable(dtGrossHead, "SHEADID", gr.DataKeys[gRow.DataItemIndex].Values[2].ToString().Trim(), "SHEADID") == gr.DataKeys[gRow.DataItemIndex].Values[2].ToString().Trim())
                dclGrossPay = dclGrossPay + Common.RoundDecimal(txtPAmt.Text.Trim(), 2);
            command[i] = this.UpdatePaySlipDetails(gr.DataKeys[gRow.DataItemIndex].Values[0].ToString(),
                                                 gr.DataKeys[gRow.DataItemIndex].Values[1].ToString(),
                                                 gr.DataKeys[gRow.DataItemIndex].Values[2].ToString(),
                                                 txtPAmt.Text,
                                                 strInsBy, strInsDate);
            i++;
        }
        // Update Payslip Master Data
        command[i] = new SqlCommand("proc_Payroll_Update_PaySlipMst");
        command[i].CommandType = CommandType.StoredProcedure;

        SqlParameter p_PSBID = command[i].Parameters.Add("PSBID", SqlDbType.BigInt);
        p_PSBID.Direction = ParameterDirection.Input;
        p_PSBID.Value = strPSBID;

        SqlParameter p_PAYID = command[i].Parameters.Add("PAYID", SqlDbType.BigInt);
        p_PAYID.Direction = ParameterDirection.Input;
        p_PAYID.Value = strPayID;

        SqlParameter p_EMPID = command[i].Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmdID;

        SqlParameter p_NETPAY = command[i].Parameters.Add("NETPAY", SqlDbType.Decimal);
        p_NETPAY.Direction = ParameterDirection.Input;
        p_NETPAY.Value = decNetPay;

        SqlParameter p_GROSSAMNT = command[i].Parameters.Add("GROSSAMNT", SqlDbType.Decimal);
        p_GROSSAMNT.Direction = ParameterDirection.Input;
        p_GROSSAMNT.Value = dclGrossPay;

        SqlParameter p_PAYSLIPSTATUS = command[i].Parameters.Add("PAYSLIPSTATUS", SqlDbType.Char);
        p_PAYSLIPSTATUS.Direction = ParameterDirection.Input;
        p_PAYSLIPSTATUS.Value = "P";

        SqlParameter p_INSERTEDBY = command[i].Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = strInsBy;

        SqlParameter p_INSERTEDDATE = command[i].Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDDATE.Direction = ParameterDirection.Input;
        p_INSERTEDDATE.Value = strInsDate;

        objDC.MakeTransaction(command);
        return decNetPay.ToString() + "," + dclGrossPay.ToString();
    }

    protected SqlCommand UpdatePaySlipDetails(string strPSBId, string strEmpId, string strSalHeadId, string strPayAmt, string strInsBy, string strInsDate)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Update_PaySlipDets");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_PSBID = cmd.Parameters.Add("PSBID", SqlDbType.BigInt);
        p_PSBID.Direction = ParameterDirection.Input;
        p_PSBID.Value = strPSBId;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpId;

        SqlParameter p_SHEADID = cmd.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = strSalHeadId;

        SqlParameter p_PAYAMT = cmd.Parameters.Add("PAYAMT", SqlDbType.Decimal);
        p_PAYAMT.Direction = ParameterDirection.Input;
        p_PAYAMT.Value = strPayAmt;

        SqlParameter p_INSERTEDBY = cmd.Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = strInsBy;


        SqlParameter p_INSERTEDDATE = cmd.Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDDATE.Direction = ParameterDirection.Input;
        p_INSERTEDDATE.Value = strInsDate;

        return cmd;
    }

    public bool DeleteData(GridView grPayslipMst)
    {
        int i = 0;
        SqlCommand[] command = new SqlCommand[grPayslipMst.Rows.Count];
        foreach (GridViewRow gRow in grPayslipMst.Rows)
        {
            CheckBox chkB = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            if (chkB.Checked == true)
            {
                command[i] = new SqlCommand("proc_Payroll_Delete_PayrollPreparedData");
                command[i].CommandType = CommandType.StoredProcedure;

                SqlParameter p_PSBID = command[i].Parameters.Add("PSBID", SqlDbType.BigInt);
                p_PSBID.Direction = ParameterDirection.Input;
                p_PSBID.Value = grPayslipMst.DataKeys[gRow.DataItemIndex].Values[0].ToString().Trim(); ;

                SqlParameter p_PAYID = command[i].Parameters.Add("PAYID", SqlDbType.BigInt);
                p_PAYID.Direction = ParameterDirection.Input;
                p_PAYID.Value = grPayslipMst.DataKeys[gRow.DataItemIndex].Values[1].ToString().Trim();

                SqlParameter p_EMPID = command[i].Parameters.Add("EMPID", SqlDbType.Char);
                p_EMPID.Direction = ParameterDirection.Input;
                p_EMPID.Value = gRow.Cells[2].Text.Trim();
                i++;
            }
        }
        if (command[0] != null)
        {
            objDC.MakeTransaction(command);
            return true;
        }
        return false;
    }

    public void UpdatePayslipMst(DataTable dt, string strMonth, string strYear,string strStatus,string strInsBy,string strInsDate)
    {
        int i = 0;
        SqlCommand[] command = new SqlCommand[dt.Rows.Count];
        foreach (DataRow dRow in dt.Rows)
        {
            command[i] = new SqlCommand("Proc_Payroll_Update_PayslipMstStatus");
            command[i].CommandType = CommandType.StoredProcedure;

            SqlParameter p_VMONTH = command[i].Parameters.Add("VMONTH", SqlDbType.BigInt);
            p_VMONTH.Direction = ParameterDirection.Input;
            p_VMONTH.Value = strMonth;

            SqlParameter p_VYEAR = command[i].Parameters.Add("VYEAR", SqlDbType.BigInt);
            p_VYEAR.Direction = ParameterDirection.Input;
            p_VYEAR.Value = strYear;

            SqlParameter p_PSBID = command[i].Parameters.Add("PSBID", SqlDbType.BigInt);
            p_PSBID.Direction = ParameterDirection.Input;
            p_PSBID.Value = dRow["PSBID"].ToString().Trim();

            SqlParameter p_EMPID = command[i].Parameters.Add("EMPID", SqlDbType.Char);
            p_EMPID.Direction = ParameterDirection.Input;
            p_EMPID.Value = dRow["EMPID"].ToString().Trim();

            SqlParameter p_PAYSLIPSTATUS = command[i].Parameters.Add("PAYSLIPSTATUS", SqlDbType.Char);
            p_PAYSLIPSTATUS.Direction = ParameterDirection.Input;
            p_PAYSLIPSTATUS.Value = strStatus;

            SqlParameter p_INSERTEDBY = command[i].Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
            p_INSERTEDBY.Direction = ParameterDirection.Input;
            p_INSERTEDBY.Value = strInsBy;


            SqlParameter p_INSERTEDDATE = command[i].Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
            p_INSERTEDDATE.Direction = ParameterDirection.Input;
            p_INSERTEDDATE.Value = strInsDate;

            i++;
        }
        objDC.MakeTransaction(command);       
    }

    #endregion
    #region Select Data
    public DataTable GetPayslipPreparedData(string strGenerateFor, string strGeneratValue, string strMonth, string strYear, string strBranchCode,string strEmpTypeId)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Select_PayslipPreparedData");
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

        SqlParameter p_EmpTypeId = cmd.Parameters.Add("EmpTypeId", SqlDbType.BigInt);
        p_EmpTypeId.Direction = ParameterDirection.Input;
        p_EmpTypeId.Value = strEmpTypeId;

        objDC.CreateDSFromProc(cmd, "PayslipPreparedData");
        return objDC.ds.Tables["PayslipPreparedData"];
    }

    public DataTable GetPayslipDetailsData(string strPSBID,string strEmpID)
    {
        if (objDC.ds.Tables["PayslipDetailsData"] != null)
        {
            objDC.ds.Tables["PayslipDetailsData"].Rows.Clear();
            objDC.ds.Tables["PayslipDetailsData"].Dispose();
        }

        SqlCommand cmd = new SqlCommand("proc_Payroll_Select_PayslipDetailsData");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_PSBID = cmd.Parameters.Add("PSBID", SqlDbType.BigInt);
        p_PSBID.Direction = ParameterDirection.Input;
        p_PSBID.Value = strPSBID;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        objDC.CreateDSFromProc(cmd, "PayslipDetailsData");
        return objDC.ds.Tables["PayslipDetailsData"];
    }

    public DataTable GetPayrollDataForEndorcement(string strGenerateFor, string strGeneratValue, string strMonth, string strYear,
        string strBranchCode)
    {
        if (objDC.ds.Tables["GetPayrollDataForEndorcement"] != null)
        {
            objDC.ds.Tables["GetPayrollDataForEndorcement"].Rows.Clear();
            objDC.ds.Tables["GetPayrollDataForEndorcement"].Dispose();
        }
        SqlCommand cmd = new SqlCommand("proc_payroll_select_PayrollPreparedDataForEndorcement");
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

        objDC.CreateDSFromProc(cmd, "GetPayrollDataForEndorcement");
        return objDC.ds.Tables["GetPayrollDataForEndorcement"];
    }

    public DataTable GetDistinctEmployeeForEndorcement(string strGenerateFor, string strGeneratValue, string strMonth, string strYear,
        string strBranchCode, string strStatus)
    {
        if (objDC.ds.Tables["GetDistinctEmployeeForEndorcement"] != null)
        {
            objDC.ds.Tables["GetDistinctEmployeeForEndorcement"].Rows.Clear();
            objDC.ds.Tables["GetDistinctEmployeeForEndorcement"].Dispose();
        }
        SqlCommand cmd = new SqlCommand("proc_payroll_select_DistinctEmployeeForEndorcement");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_GenerateFor = cmd.Parameters.Add("GenerateFor", SqlDbType.Char);
        p_GenerateFor.Direction = ParameterDirection.Input;
        p_GenerateFor.Value = strGenerateFor;

        SqlParameter p_GenerateValue = cmd.Parameters.Add("GenerateValue", SqlDbType.Char);
        p_GenerateValue.Direction = ParameterDirection.Input;
        p_GenerateValue.Value = strGeneratValue;

        SqlParameter p_BranchCode = cmd.Parameters.Add("RoutingNo", SqlDbType.Char);
        p_BranchCode.Direction = ParameterDirection.Input;
        p_BranchCode.Value = strBranchCode;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_PAYSLIPSTATUS = cmd.Parameters.Add("PAYSLIPSTATUS", SqlDbType.Char);
        p_PAYSLIPSTATUS.Direction = ParameterDirection.Input;
        p_PAYSLIPSTATUS.Value = strStatus;

        objDC.CreateDSFromProc(cmd, "GetDistinctEmployeeForEndorcement");
        return objDC.ds.Tables["GetDistinctEmployeeForEndorcement"];
    }

    public DataTable GetPayrollReviewedData(string strGenerateFor, string strGeneratValue, string strMonth, string strYear,
        string strBranchCode)
    {
        if (objDC.ds.Tables["GetPayrollReviewedData"] != null)
        {
            objDC.ds.Tables["GetPayrollReviewedData"].Rows.Clear();
            objDC.ds.Tables["GetPayrollReviewedData"].Dispose();
        }
        SqlCommand cmd = new SqlCommand("proc_payroll_select_PayrollReviewedData");
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

        objDC.CreateDSFromProc(cmd, "GetPayrollReviewedData");
        return objDC.ds.Tables["GetPayrollReviewedData"];
    }

    public DataTable GetReviewedDataForEndorcement(string strGenerateFor, string strGeneratValue, string strMonth, string strYear,
        string strBranchCode, string strSalType, string strEmpGrpID)
    {
        if (objDC.ds.Tables["GetReviewedDataForEndorcement"] != null)
        {
            objDC.ds.Tables["GetReviewedDataForEndorcement"].Rows.Clear();
            objDC.ds.Tables["GetReviewedDataForEndorcement"].Dispose();
        }
        SqlCommand cmd = new SqlCommand("proc_payroll_select_PayrollReviewedDataForEndorcement");
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

        objDC.CreateDSFromProc(cmd, "GetReviewedDataForEndorcement");
        return objDC.ds.Tables["GetReviewedDataForEndorcement"];
    }

    public DataTable GetPayrollAuditedData(string strGenerateFor, string strGeneratValue, string strMonth, string strYear,
        string strBranchCode, string strSalType, string strEmpGrpID)
    {
        if (objDC.ds.Tables["GetPayrollAuditedData"] != null)
        {
            objDC.ds.Tables["GetPayrollAuditedData"].Rows.Clear();
            objDC.ds.Tables["GetPayrollAuditedData"].Dispose();
        }
        SqlCommand cmd = new SqlCommand("proc_payroll_select_PayrollAuditedData");
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

        objDC.CreateDSFromProc(cmd, "GetPayrollAuditedData");
        return objDC.ds.Tables["GetPayrollAuditedData"];

    }
    
    public DataTable GetAuditedDataForEndorcement(string strGenerateFor, string strGeneratValue, string strMonth, string strYear,
        string strBranchCode)
    {
        if (objDC.ds.Tables["GetAuditedDataForEndorcement"] != null)
        {
            objDC.ds.Tables["GetAuditedDataForEndorcement"].Rows.Clear();
            objDC.ds.Tables["GetAuditedDataForEndorcement"].Dispose();
        }
        SqlCommand cmd = new SqlCommand("proc_payroll_select_PayrollAuditedDataForEndorcement");
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

        objDC.CreateDSFromProc(cmd, "GetAuditedDataForEndorcement");
        return objDC.ds.Tables["GetAuditedDataForEndorcement"];
    }

    public DataTable GetPayrollApprovedData(string strGenerateFor, string strGeneratValue, string strMonth, string strYear,
        string strBranchCode)
    {
        if (objDC.ds.Tables["GetPayrollApprovedData"] != null)
        {
            objDC.ds.Tables["GetPayrollApprovedData"].Rows.Clear();
            objDC.ds.Tables["GetPayrollApprovedData"].Dispose();
        }
        SqlCommand cmd = new SqlCommand("proc_payroll_select_PayrollApprovedData");
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

        objDC.CreateDSFromProc(cmd, "GetPayrollApprovedData");
        return objDC.ds.Tables["GetPayrollApprovedData"];
    }

    public DataTable GetPayrollApprovedDataForDisbursement(string strGenerateFor, string strGeneratValue,
        string strMonth, string strYear, string strBranchCode)
    {
        if (objDC.ds.Tables["GetPayrollApprovedDataForDisbursement"] != null)
        {
            objDC.ds.Tables["GetPayrollApprovedDataForDisbursement"].Rows.Clear();
            objDC.ds.Tables["GetPayrollApprovedDataForDisbursement"].Dispose();
        }

        SqlCommand cmd = new SqlCommand("proc_payroll_select_PayrollApprovedDataForDisbursement");
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
        
        objDC.CreateDSFromProc(cmd, "GetPayrollApprovedDataForDisbursement");
        return objDC.ds.Tables["GetPayrollApprovedDataForDisbursement"];
    }

    public DataTable GetPFRecordFromSalary(string strMonth, string strYear,string strEmpGrpID)
    {
        if (objDC.ds.Tables["GetPFRecordFromSalary"] != null)
        {
            objDC.ds.Tables["GetPFRecordFromSalary"].Rows.Clear();
            objDC.ds.Tables["GetPFRecordFromSalary"].Dispose();
        }

        SqlCommand cmd = new SqlCommand("proc_payroll_select_PayrollPFLoanAndIntDataForPFLedger");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_EmpGrpID = cmd.Parameters.Add("EmpGrpID", SqlDbType.BigInt);
        p_EmpGrpID.Direction = ParameterDirection.Input;
        p_EmpGrpID.Value = strEmpGrpID;

        objDC.CreateDSFromProc(cmd, "GetPFRecordFromSalary");
        return objDC.ds.Tables["GetPFRecordFromSalary"];
    }

    public DataTable GetGratuityFromSalary(string strMonth, string strYear, string strEmpGrpID)
    {
        if (objDC.ds.Tables["GetGratuityFromSalary"] != null)
        {
            objDC.ds.Tables["GetGratuityFromSalary"].Rows.Clear();
            objDC.ds.Tables["GetGratuityFromSalary"].Dispose();
        }

        SqlCommand cmd = new SqlCommand("proc_select_GratuityDataForGratuityLedger");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_EmpGrpID = cmd.Parameters.Add("EmpGrpID", SqlDbType.BigInt);
        p_EmpGrpID.Direction = ParameterDirection.Input;
        p_EmpGrpID.Value = strEmpGrpID;

        objDC.CreateDSFromProc(cmd, "GetGratuityFromSalary");
        return objDC.ds.Tables["GetGratuityFromSalary"];
    }

    #endregion
    public Payroll_PayslipApprovalManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
