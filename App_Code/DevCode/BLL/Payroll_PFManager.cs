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
/// Summary description for Payroll_PFManager
/// </summary>
public class Payroll_PFManager
{
    DBConnector objDC=new DBConnector();

    public void InsertPFLedger(string strLedgerID,string strEmpID, string strMonth, string strYear, string strFiscalYear,
        string strOPOwn, string strOPCare, string strOPInt, string strOPTotal,
        string strCMOwn, string strCMCare, string strCMInt, string strCMTotal,
        string strCPDate, string strCPAmount,
        string strCUOwn, string strCUCare, string strCUInt, string strCUTotal,
        string strTotalPay, string strNetBalance, string strInsBy, string strInsDate, string strIsUpdate)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Insert_PFLedger");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LEDGERID = cmd.Parameters.Add("LEDGERID", SqlDbType.BigInt);
        p_LEDGERID.Direction = ParameterDirection.Input;
        p_LEDGERID.Value = strLedgerID;

        SqlParameter p_EmpID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYear;

        SqlParameter p_OPPFOWN = cmd.Parameters.Add("OPPFOWN", SqlDbType.BigInt);
        p_OPPFOWN.Direction = ParameterDirection.Input;
        p_OPPFOWN.Value = strOPOwn;

        SqlParameter p_OPPFCARE = cmd.Parameters.Add("OPPFCARE", SqlDbType.BigInt);
        p_OPPFCARE.Direction = ParameterDirection.Input;
        p_OPPFCARE.Value = strOPCare;

        SqlParameter p_OPPFINTREST = cmd.Parameters.Add("OPPFINTREST", SqlDbType.BigInt);
        p_OPPFINTREST.Direction = ParameterDirection.Input;
        p_OPPFINTREST.Value = strOPInt;

        SqlParameter p_OPTOTAL = cmd.Parameters.Add("OPTOTAL", SqlDbType.BigInt);
        p_OPTOTAL.Direction = ParameterDirection.Input;
        p_OPTOTAL.Value = strOPTotal;

        SqlParameter p_CMPFOWN = cmd.Parameters.Add("CMPFOWN", SqlDbType.BigInt);
        p_CMPFOWN.Direction = ParameterDirection.Input;
        p_CMPFOWN.Value = strCMOwn;

        SqlParameter p_CMPFCARE = cmd.Parameters.Add("CMPFCARE", SqlDbType.BigInt);
        p_CMPFCARE.Direction = ParameterDirection.Input;
        p_CMPFCARE.Value = strCMCare;

        SqlParameter p_CMPFINTREST = cmd.Parameters.Add("CMPFINTREST", SqlDbType.BigInt);
        p_CMPFINTREST.Direction = ParameterDirection.Input;
        p_CMPFINTREST.Value = strCMInt;

        SqlParameter p_CMTOTAL = cmd.Parameters.Add("CMTOTAL", SqlDbType.BigInt);
        p_CMTOTAL.Direction = ParameterDirection.Input;
        p_CMTOTAL.Value = strCMTotal;

        SqlParameter p_CPDATE = cmd.Parameters.Add("CPDATE", DBNull.Value);
        p_CPDATE.Direction = ParameterDirection.Input;
        p_CPDATE.IsNullable = true;
        if (strCPDate != "")
            p_CPDATE.Value = strCPDate;


        SqlParameter p_CPAMOUNT = cmd.Parameters.Add("CPAMOUNT", SqlDbType.BigInt);
        p_CPAMOUNT.Direction = ParameterDirection.Input;
        p_CPAMOUNT.Value = strCPAmount;

        SqlParameter p_CUPFOWN = cmd.Parameters.Add("CUPFOWN", SqlDbType.BigInt);
        p_CUPFOWN.Direction = ParameterDirection.Input;
        p_CUPFOWN.Value = strCUOwn;

        SqlParameter p_CUPFCARE = cmd.Parameters.Add("CUPFCARE", SqlDbType.BigInt);
        p_CUPFCARE.Direction = ParameterDirection.Input;
        p_CUPFCARE.Value = strCUCare;

        SqlParameter p_CUPFINTREST = cmd.Parameters.Add("CUPFINTREST", SqlDbType.BigInt);
        p_CUPFINTREST.Direction = ParameterDirection.Input;
        p_CUPFINTREST.Value = strCUInt;

        SqlParameter p_CUTOTAL = cmd.Parameters.Add("CUTOTAL", SqlDbType.BigInt);
        p_CUTOTAL.Direction = ParameterDirection.Input;
        p_CUTOTAL.Value = strCUTotal;

        SqlParameter p_TOTALPAY = cmd.Parameters.Add("TOTALPAY", SqlDbType.BigInt);
        p_TOTALPAY.Direction = ParameterDirection.Input;
        p_TOTALPAY.Value = strTotalPay;

        SqlParameter p_NETBALANCE = cmd.Parameters.Add("NETBALANCE", SqlDbType.BigInt);
        p_NETBALANCE.Direction = ParameterDirection.Input;
        p_NETBALANCE.Value = strNetBalance;

        SqlParameter p_INSERTEDBY = cmd.Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = strInsBy;

        SqlParameter p_INSERTEDDATE = cmd.Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDDATE.Direction = ParameterDirection.Input;
        p_INSERTEDDATE.Value = strInsDate;

        SqlParameter p_ISUPDATE = cmd.Parameters.Add("ISUPDATE", SqlDbType.Char);
        p_ISUPDATE.Direction = ParameterDirection.Input;
        p_ISUPDATE.Value = strIsUpdate;

        objDC.ExecuteQuery(cmd);
    }

    public SqlCommand GetCommandOfInsertPFLedger(string strLedgerID, string strEmpID, string strMonth, string strYear, string strFiscalYear,
        string strOPOwn, string strOPCare, string strOPInt, string strOPTotal,
        string strCMOwn, string strCMCare, string strCMInt, string strCMTotal,
        string strCPDate, string strCPAmount,
        string strTotalPay, string strNetBalance, string strInsBy, string strInsDate, string strIsUpdate)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Insert_PFLedger");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LEDGERID = cmd.Parameters.Add("LEDGERID", SqlDbType.BigInt);
        p_LEDGERID.Direction = ParameterDirection.Input;
        p_LEDGERID.Value = strLedgerID;

        SqlParameter p_EmpID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYear;

        SqlParameter p_OPPFOWN = cmd.Parameters.Add("OPPFOWN", SqlDbType.BigInt);
        p_OPPFOWN.Direction = ParameterDirection.Input;
        p_OPPFOWN.Value = strOPOwn;

        SqlParameter p_OPPFCARE = cmd.Parameters.Add("OPPFCARE", SqlDbType.BigInt);
        p_OPPFCARE.Direction = ParameterDirection.Input;
        p_OPPFCARE.Value = strOPCare;

        SqlParameter p_OPPFINTREST = cmd.Parameters.Add("OPPFINTREST", SqlDbType.BigInt);
        p_OPPFINTREST.Direction = ParameterDirection.Input;
        p_OPPFINTREST.Value = strOPInt;

        SqlParameter p_OPTOTAL = cmd.Parameters.Add("OPTOTAL", SqlDbType.BigInt);
        p_OPTOTAL.Direction = ParameterDirection.Input;
        p_OPTOTAL.Value = strOPTotal;

        SqlParameter p_CMPFOWN = cmd.Parameters.Add("CMPFOWN", SqlDbType.BigInt);
        p_CMPFOWN.Direction = ParameterDirection.Input;
        p_CMPFOWN.Value = strCMOwn;

        SqlParameter p_CMPFCARE = cmd.Parameters.Add("CMPFCARE", SqlDbType.BigInt);
        p_CMPFCARE.Direction = ParameterDirection.Input;
        p_CMPFCARE.Value = strCMCare;

        SqlParameter p_CMPFINTREST = cmd.Parameters.Add("CMPFINTREST", SqlDbType.BigInt);
        p_CMPFINTREST.Direction = ParameterDirection.Input;
        p_CMPFINTREST.Value = strCMInt;

        SqlParameter p_CMTOTAL = cmd.Parameters.Add("CMTOTAL", SqlDbType.BigInt);
        p_CMTOTAL.Direction = ParameterDirection.Input;
        p_CMTOTAL.Value = strCMTotal;

        SqlParameter p_CPDATE = cmd.Parameters.Add("CPDATE", DBNull.Value);
        p_CPDATE.Direction = ParameterDirection.Input;
        p_CPDATE.IsNullable = true;
        if (strCPDate != "")
            p_CPDATE.Value = strCPDate;

        SqlParameter p_CPAMOUNT = cmd.Parameters.Add("CPAMOUNT", SqlDbType.BigInt);
        p_CPAMOUNT.Direction = ParameterDirection.Input;
        p_CPAMOUNT.Value = strCPAmount;

        SqlParameter p_TOTALPAY = cmd.Parameters.Add("TOTALPAY", SqlDbType.BigInt);
        p_TOTALPAY.Direction = ParameterDirection.Input;
        p_TOTALPAY.Value = strTotalPay;

        SqlParameter p_NETBALANCE = cmd.Parameters.Add("NETBALANCE", SqlDbType.BigInt);
        p_NETBALANCE.Direction = ParameterDirection.Input;
        p_NETBALANCE.Value = strNetBalance;

        SqlParameter p_INSERTEDBY = cmd.Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = strInsBy;

        SqlParameter p_INSERTEDDATE = cmd.Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDDATE.Direction = ParameterDirection.Input;
        p_INSERTEDDATE.Value = strInsDate;

        SqlParameter p_ISUPDATE = cmd.Parameters.Add("ISUPDATE", SqlDbType.Char);
        p_ISUPDATE.Direction = ParameterDirection.Input;
        p_ISUPDATE.Value = strIsUpdate;

        return cmd;
    }

    public SqlCommand GetCommandOfInsertGratuityLedger(string strLedgerID, string strEmpID, string strMonth, string strYear, string strFiscalYear,
        string strOPGR, string strOPInt, string strOPTotal, string strCMGR, string strCMInt, string strCMTotal,
        string strCPDate, string strCPAmount, string strCUGR, string strCUInt, string strCUTotal,
        string strTotalPay, string strNetBalance, string strInsBy, string strInsDate, string strIsUpdate)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Insert_GratuityLedger");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LEDGERID = cmd.Parameters.Add("LEDGERID", SqlDbType.BigInt);
        p_LEDGERID.Direction = ParameterDirection.Input;
        p_LEDGERID.Value = strLedgerID;

        SqlParameter p_EmpID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYear;

        SqlParameter p_OPGR = cmd.Parameters.Add("OPGR", SqlDbType.BigInt);
        p_OPGR.Direction = ParameterDirection.Input;
        p_OPGR.Value = strOPGR;

        SqlParameter p_OPGRINTREST = cmd.Parameters.Add("OPGRINTREST", SqlDbType.BigInt);
        p_OPGRINTREST.Direction = ParameterDirection.Input;
        p_OPGRINTREST.Value = strOPInt;

        SqlParameter p_OPTOTAL = cmd.Parameters.Add("OPTOTAL", SqlDbType.BigInt);
        p_OPTOTAL.Direction = ParameterDirection.Input;
        p_OPTOTAL.Value = strOPTotal;

        SqlParameter p_CMGR = cmd.Parameters.Add("CMGR", SqlDbType.BigInt);
        p_CMGR.Direction = ParameterDirection.Input;
        p_CMGR.Value = strCMGR;

        SqlParameter p_CMGRINTREST = cmd.Parameters.Add("CMGRINTREST", SqlDbType.BigInt);
        p_CMGRINTREST.Direction = ParameterDirection.Input;
        p_CMGRINTREST.Value = strCMInt;

        SqlParameter p_CMTOTAL = cmd.Parameters.Add("CMTOTAL", SqlDbType.BigInt);
        p_CMTOTAL.Direction = ParameterDirection.Input;
        p_CMTOTAL.Value = strCMTotal;

        SqlParameter p_CPDATE = cmd.Parameters.Add("CPDATE", DBNull.Value);
        p_CPDATE.Direction = ParameterDirection.Input;
        p_CPDATE.IsNullable = true;
        if (strCPDate != "")
            p_CPDATE.Value = strCPDate;

        SqlParameter p_CPAMOUNT = cmd.Parameters.Add("CPAMOUNT", SqlDbType.BigInt);
        p_CPAMOUNT.Direction = ParameterDirection.Input;
        p_CPAMOUNT.Value = strCPAmount;

        SqlParameter p_CUGR = cmd.Parameters.Add("CUGR", SqlDbType.BigInt);
        p_CUGR.Direction = ParameterDirection.Input;
        p_CUGR.Value = strCUGR;

        SqlParameter p_CUGRINTREST = cmd.Parameters.Add("CUGRINTREST", SqlDbType.BigInt);
        p_CUGRINTREST.Direction = ParameterDirection.Input;
        p_CUGRINTREST.Value = strCUInt;

        SqlParameter p_CUTOTAL = cmd.Parameters.Add("CUTOTAL", SqlDbType.BigInt);
        p_CUTOTAL.Direction = ParameterDirection.Input;
        p_CUTOTAL.Value = strCUTotal;

        SqlParameter p_TOTALPAY = cmd.Parameters.Add("TOTALPAY", SqlDbType.BigInt);
        p_TOTALPAY.Direction = ParameterDirection.Input;
        p_TOTALPAY.Value = strTotalPay;

        SqlParameter p_NETBALANCE = cmd.Parameters.Add("NETBALANCE", SqlDbType.BigInt);
        p_NETBALANCE.Direction = ParameterDirection.Input;
        p_NETBALANCE.Value = strNetBalance;

        SqlParameter p_INSERTEDBY = cmd.Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = strInsBy;

        SqlParameter p_INSERTEDDATE = cmd.Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDDATE.Direction = ParameterDirection.Input;
        p_INSERTEDDATE.Value = strInsDate;

        SqlParameter p_ISUPDATE = cmd.Parameters.Add("ISUPDATE", SqlDbType.Char);
        p_ISUPDATE.Direction = ParameterDirection.Input;
        p_ISUPDATE.Value = strIsUpdate;

        return cmd;
    }

    public void UpdatePFInterest(GridView grv, string strInsBy, string strInsDate, string strIntRate)
    {
        SqlCommand[] cmd = new SqlCommand[grv.Rows.Count];
        int i = 0;
        foreach (GridViewRow gRow in grv.Rows)
        {
            cmd[i] = new SqlCommand("Proc_Payroll_Update_PFLedgerForInterest");
            cmd[i].CommandType = CommandType.StoredProcedure;

            SqlParameter p_LEDGERID = cmd[i].Parameters.Add("LEDGERID", SqlDbType.BigInt);
            p_LEDGERID.Direction = ParameterDirection.Input;
            p_LEDGERID.Value = gRow.Cells[26].Text.Trim();

            SqlParameter p_CMPFINTREST = cmd[i].Parameters.Add("CMPFINTREST", SqlDbType.BigInt);
            p_CMPFINTREST.Direction = ParameterDirection.Input;
            p_CMPFINTREST.Value = gRow.Cells[14].Text.Trim();

            SqlParameter p_MonthofInterest = cmd[i].Parameters.Add("MonthofInterest", SqlDbType.BigInt);
            p_MonthofInterest.Direction = ParameterDirection.Input;
            p_MonthofInterest.Value = gRow.Cells[27].Text.Trim();

            SqlParameter p_InterestRate = cmd[i].Parameters.Add("InterestRate", SqlDbType.Decimal);
            p_InterestRate.Direction = ParameterDirection.Input;
            p_InterestRate.Value = string.IsNullOrEmpty(strIntRate) == false ? strIntRate : "0";

            SqlParameter p_UPDATEDBY = cmd[i].Parameters.Add("UPDATEDBY", SqlDbType.VarChar);
            p_UPDATEDBY.Direction = ParameterDirection.Input;
            p_UPDATEDBY.Value = strInsBy;

            SqlParameter p_UPDATEDDATE = cmd[i].Parameters.Add("UPDATEDDATE", SqlDbType.DateTime);
            p_UPDATEDDATE.Direction = ParameterDirection.Input;
            p_UPDATEDDATE.Value = strInsDate;

            i = i + 1;

        }

        objDC.MakeTransaction(cmd);
        
    }

    public string GetEmpName(string strEmpID)
    {
        string strSQL="SELECT FULLNAME FROM EMPINFO WHERE EMPID=@EMPID";
        SqlCommand cmd=new SqlCommand(strSQL);
        cmd.CommandType=CommandType.Text;

        SqlParameter p_EmpID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        return objDC.GetScalarVal(cmd);
    }

    public DataTable GetPFLedgerData(string strFiscalYr, string strMonth, string strYear, string strEmpID)
    {
        SqlCommand command = new SqlCommand();
        command = new SqlCommand("Proc_Payroll_Select_PFLedger");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FISCALYRID = command.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYr;

         SqlParameter p_VMONTH = command.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

         SqlParameter p_VYEAR = command.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

         SqlParameter p_EmpID = command.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        objDC.CreateDSFromProc(command, "GetPFLedgerData");
        return objDC.ds.Tables["GetPFLedgerData"];
    }

    public DataTable GetGratuityLedgerData(string strFiscalYr, string strMonth, string strYear, string strEmpID)
    {
        SqlCommand command = new SqlCommand();
        command = new SqlCommand("Proc_Payroll_Select_GratuityLedger");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FISCALYRID = command.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYr;

        SqlParameter p_VMONTH = command.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = command.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_EmpID = command.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        objDC.CreateDSFromProc(command, "GetPFLedgerData");
        return objDC.ds.Tables["GetPFLedgerData"];
    }

    public DataTable GetPFLedgerSummaryData(string strFiscalYr, string strMonth, string strYear, string strEmpID)
    {
        SqlCommand command = new SqlCommand();
        command = new SqlCommand("Proc_Payroll_Select_PFLedgerSummary");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FISCALYRID = command.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYr;

        SqlParameter p_VMONTH = command.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = command.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_EmpID = command.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        objDC.CreateDSFromProc(command, "GetPFLedgerSummaryData");
        return objDC.ds.Tables["GetPFLedgerSummaryData"];
    }

    public DataTable GetGratuityLedgerSummaryData(string strFiscalYr, string strMonth, string strYear, string strEmpID)
    {
        SqlCommand command = new SqlCommand();
        command = new SqlCommand("Proc_Payroll_Select_GratuityLedgerSummary");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FISCALYRID = command.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYr;

        SqlParameter p_VMONTH = command.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = command.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_EmpID = command.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        objDC.CreateDSFromProc(command, "GetPFLedgerSummaryData");
        return objDC.ds.Tables["GetPFLedgerSummaryData"];
    }

    public DataTable GetPFLedgerDataForExcel(string strFiscalYr,string strMonth)
    {
        SqlCommand command = new SqlCommand();
        command = new SqlCommand("Proc_Payroll_Select_PFLedgerForExcel");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FISCALYRID = command.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYr;

         SqlParameter p_VMONTH = command.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        objDC.CreateDSFromProc(command, "GetPFLedgerDataForExcel");
        return objDC.ds.Tables["GetPFLedgerDataForExcel"];
    }

    public DataTable GetGratuityLedgerDataForExcel(string strFiscalYr, string strMonth)
    {
        SqlCommand command = new SqlCommand();
        command = new SqlCommand("Proc_Payroll_Select_GratuityLedgerForExcel");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FISCALYRID = command.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYr;

        SqlParameter p_VMONTH = command.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        objDC.CreateDSFromProc(command, "GetPFLedgerDataForExcel");
        return objDC.ds.Tables["GetPFLedgerDataForExcel"];
    }

    public DataTable GetPFLedgerDataForInterest(string strFiscalYr, string strMonth, string strEmpID)
    {
        SqlCommand command = new SqlCommand();
        command = new SqlCommand("Proc_Payroll_Select_PFLedgerForInterest");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FISCALYRID = command.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYr;

        SqlParameter p_VMONTH = command.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_EmpID = command.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        objDC.CreateDSFromProc(command, "GetPFLedgerDataForInterest");
        return objDC.ds.Tables["GetPFLedgerDataForInterest"];
    }


    #region PFBalanceUpload

    public void InsertPFBalanceUploadData(GridView grPay, string strFisYrId, string strInsBy, string strInsDate)
    {
        int i = 0;
        int j = 0;
        int empCount = grPay.Rows.Count == 0 ? 1 : grPay.Rows.Count;

        SqlCommand[] command;
        command = new SqlCommand[empCount];

        string strVID = Common.getMaxId("ProvidentFundBF", "PFBFID");
        int strVIDi = Convert.ToInt32(strVID);

        for (i = 0; i < empCount; i++)
        {
            if (Common.CheckNullString(grPay.Rows[i].Cells[0].Text.Trim()) != "" && grPay.Rows[i].Cells[1].Text.Trim() != "0")
            {
                command[j] = new SqlCommand("proc_Payroll_Insert_ProvidentFundBF");
                command[j].CommandType = CommandType.StoredProcedure;

                SqlParameter p_VID = command[j].Parameters.Add("PFBFID", SqlDbType.BigInt);
                p_VID.Direction = ParameterDirection.Input;
                p_VID.Value = strVIDi;

                SqlParameter p_EMPID = command[j].Parameters.Add("EMPID", SqlDbType.Char);
                p_EMPID.Direction = ParameterDirection.Input;
                p_EMPID.Value = grPay.Rows[i].Cells[1].Text.Trim();

                SqlParameter p_SHEADID = command[j].Parameters.Add("PFFiscalYrID", SqlDbType.BigInt);
                p_SHEADID.Direction = ParameterDirection.Input;
                p_SHEADID.Value = strFisYrId;

                //SqlParameter p_CarryForward = command[j].Parameters.Add("CarryForward", SqlDbType.Decimal);
                //p_CarryForward.Direction = ParameterDirection.Input;
                //p_CarryForward.Value = grPay.Rows[i].Cells[2].Text.Trim();

                SqlParameter p_EmpContribution = command[j].Parameters.Add("EmpContribution", SqlDbType.Decimal);
                p_EmpContribution.Direction = ParameterDirection.Input;
                p_EmpContribution.Value = grPay.Rows[i].Cells[2].Text.Trim();

                SqlParameter p_CompContribution = command[j].Parameters.Add("CompContribution", SqlDbType.Decimal);
                p_CompContribution.Direction = ParameterDirection.Input;
                p_CompContribution.Value = grPay.Rows[i].Cells[3].Text.Trim();

                SqlParameter p_TotalInterest = command[j].Parameters.Add("TotalInterest", SqlDbType.Decimal);
                p_TotalInterest.Direction = ParameterDirection.Input;
                p_TotalInterest.Value = grPay.Rows[i].Cells[4].Text.Trim();

                SqlParameter p_TotalContribution = command[j].Parameters.Add("TotalContribution", SqlDbType.Decimal);
                p_TotalContribution.Direction = ParameterDirection.Input;
                p_TotalContribution.Value = grPay.Rows[i].Cells[5].Text.Trim();

                //SqlParameter p_BroadForward = command[j].Parameters.Add("BroadForward", SqlDbType.Decimal);
                //p_BroadForward.Direction = ParameterDirection.Input;
                //p_BroadForward.Value = grPay.Rows[i].Cells[5].Text.Trim();

                SqlParameter p_INSERTEDBY = command[j].Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
                p_INSERTEDBY.Direction = ParameterDirection.Input;
                p_INSERTEDBY.Value = strInsBy;

                SqlParameter p_INSERTEDDATE = command[j].Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
                p_INSERTEDDATE.Direction = ParameterDirection.Input;
                p_INSERTEDDATE.Value = strInsDate;

                SqlParameter p_IsUpdate = command[j].Parameters.Add("IsUpdate", SqlDbType.Char);
                p_IsUpdate.Direction = ParameterDirection.Input;
                p_IsUpdate.Value = "N";
                strVIDi = strVIDi + 1;
            }
            objDC.MakeTransaction(command);
        }
    }

    // new 

    public void InsertUploadPFBalanceData(GridView grPay, string strFisYrId, string strInsBy, string strInsDate)
    {
        int i = 0;
        int j = 0;
        int empCount = grPay.Rows.Count == 0 ? 1 : grPay.Rows.Count;

        SqlCommand[] command;
        command = new SqlCommand[empCount];

        string strVID = Common.getMaxId("ProvidentFundBF", "PFBFID");

        int strVIDi =Convert.ToInt32(strVID);

        for (i = 0; i < empCount; i++)
        {
            if (Common.CheckNullString(grPay.Rows[i].Cells[0].Text.Trim()) != "" && grPay.Rows[i].Cells[1].Text.Trim() != "0")
            {
                command[j] = new SqlCommand("proc_Payroll_Insert_ProvidentFundBF");
                command[j].CommandType = CommandType.StoredProcedure;

                SqlParameter p_VID = command[j].Parameters.Add("PFBFID", SqlDbType.BigInt);
                p_VID.Direction = ParameterDirection.Input;
                p_VID.Value = strVIDi;

                SqlParameter p_EMPID = command[j].Parameters.Add("EMPID", SqlDbType.Char);
                p_EMPID.Direction = ParameterDirection.Input;
                p_EMPID.Value = grPay.Rows[i].Cells[1].Text.Trim();

                SqlParameter p_SHEADID = command[j].Parameters.Add("PFFiscalYrID", SqlDbType.BigInt);
                p_SHEADID.Direction = ParameterDirection.Input;
                p_SHEADID.Value = strFisYrId;

                //SqlParameter p_CarryForward = command[j].Parameters.Add("CarryForward", SqlDbType.Decimal);
                //p_CarryForward.Direction = ParameterDirection.Input;
                //p_CarryForward.Value = grPay.Rows[i].Cells[2].Text.Trim();

                SqlParameter p_EmpContribution = command[j].Parameters.Add("EmpContribution", SqlDbType.Decimal);
                p_EmpContribution.Direction = ParameterDirection.Input;
                p_EmpContribution.Value = grPay.Rows[i].Cells[2].Text.Trim();

                SqlParameter p_CompContribution = command[j].Parameters.Add("CompContribution", SqlDbType.Decimal);
                p_CompContribution.Direction = ParameterDirection.Input;
                p_CompContribution.Value = grPay.Rows[i].Cells[3].Text.Trim();

                SqlParameter p_TotalInterest = command[j].Parameters.Add("TotalInterest", SqlDbType.Decimal);
                p_TotalInterest.Direction = ParameterDirection.Input;
                p_TotalInterest.Value = grPay.Rows[i].Cells[4].Text.Trim();

                SqlParameter p_TotalContribution = command[j].Parameters.Add("TotalContribution", SqlDbType.Decimal);
                p_TotalContribution.Direction = ParameterDirection.Input;
                p_TotalContribution.Value = grPay.Rows[i].Cells[5].Text.Trim();

                //SqlParameter p_BroadForward = command[j].Parameters.Add("BroadForward", SqlDbType.Decimal);
                //p_BroadForward.Direction = ParameterDirection.Input;
                //p_BroadForward.Value = grPay.Rows[i].Cells[5].Text.Trim();

                SqlParameter p_INSERTEDBY = command[j].Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
                p_INSERTEDBY.Direction = ParameterDirection.Input;
                p_INSERTEDBY.Value = strInsBy;

                SqlParameter p_INSERTEDDATE = command[j].Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
                p_INSERTEDDATE.Direction = ParameterDirection.Input;
                p_INSERTEDDATE.Value = strInsDate;

                SqlParameter p_IsUpdate = command[j].Parameters.Add("IsUpdate", SqlDbType.Char);
                p_IsUpdate.Direction = ParameterDirection.Input;
                p_IsUpdate.Value = grPay.Rows[i].Cells[6].Text.Trim();

                strVIDi=strVIDi+1;
            }
            objDC.MakeTransaction(command);
        }
    }

    public DataTable SelectPaySlipDetlsPFAmount(string strFiscalYrId)
    {
        string strSQL = "SELECT PD.EmpId,SUM(PD.PayAmt) AS TotPayAmt FROM PayslipMst PM, PayslipDets PD WHERE PM.PSBId=PD.PSBId"
            + " AND PM.EmpId=PD.EmpId AND PM.FiscalYrId=@FiscalYrId AND PD.SHEADID=13 GROUP BY PD.EmpId";
        SqlCommand cmd=new SqlCommand(strSQL);
        cmd.CommandType=CommandType.Text;

        SqlParameter p_FiscalYrId = cmd.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = strFiscalYrId;

        return objDC.CreateDT(cmd, "SalaryPakPFAmount");        
    }    

    #endregion
    public Payroll_PFManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}

 
    public DataTable GeneratePFBF(string strPFID)
    {
        SqlCommand command = new SqlCommand("proc_Select_GeneratePFBF");

        SqlParameter p_EmpId = command.Parameters.Add("PFFiscalYrID", SqlDbType.BigInt);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = Convert.ToInt32(strPFID);

        objDC.CreateDSFromProc(command, "SelectPFBF");
        return objDC.ds.Tables["SelectPFBF"];
    }

    public DataTable SelectEmpWisePFBF(string strEmpId)
    {
        string strSQL = "SELECT TotalContribution AS TotalPF FROM ProvidentFundBF WHERE EmpId=@EmpId"
            + " ORDER BY PFFiscalYrId DESC";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EmpID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpId;

        return objDC.CreateDT(cmd, "SelectEmpWisePFBF");       
    }
}
