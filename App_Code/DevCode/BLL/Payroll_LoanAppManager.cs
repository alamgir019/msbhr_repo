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
/// Summary description for LoanAppManager
/// </summary>
public class Payroll_LoanAppManager
{
    DBConnector objDC = new DBConnector();
    public DataTable GetEmployeeInfoforLoan(string strEmpID)
    {
        string strSQL = " SELECT a.EmpID,a.FullName, b.JobTitle,c.DivisionName,a.SalaryPakId,a.joiningDate,d.TOTALGROSSSAL "
                    + " FROM EmpInfo a, JobTitle b,DivisionList c,EMPSALARYPAKMST d "
                    + " WHERE a.DESGID=b.JBTLID and a.DivisionId=c.DivisionID and a.SalaryPakId=d.SalPakID and a.EmpID=d.EmpId and a.Status='A' "
                    + " and a.IsDeleted='N' and a.EmpId=@EmpId ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpID;

        objDC.CreateDT(cmd, "EmployeeInfoforLoan");
        return objDC.ds.Tables["EmployeeInfoforLoan"];
    }
    public string GetLoanOpening(string strEmpID)
    {
        string strSQL = "Select sum(OPENAMOUNT)-SUM(REFUNDAMT) as OL from LoanOpen where EMPID=@EmpId and ISDELETED='N' ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpID;

        return objDC.GetScalarVal(cmd);
    }

    public string GetLoanDetail(string strEmpID)
    {
        string strSQL = "Select sum(LOANAMNT) as OL from EMPSALLOANMST where EMPSALLOANMST.LoanStatus='D' and EMPSALLOANMST.EMPID=@EmpId ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpID;

        return objDC.GetScalarVal(cmd);
    }

    public string GetLoanRecovered(string strEmpID)
    {
        string strSQL = "Select  sum(PAYAMT) as OL from PaySlipMst,PaySlipDets,SalaryHead "
                    + " where PaySlipMst.EMPID= PaySlipDets.EMPID AND PaySlipMst.PAYSLIPSTATUS='D' "
                    + " and PaySlipDets.SHEADID= SalaryHead.SHEADID and  PaySlipMst.PSBID=PaySlipDets.PSBID  "
                    + " and  PaySlipDets.EMPID=@EmpId and  (SalaryHead.ISLOAN ='Y' or SalaryHead.ISSALADV ='Y')";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpID;

        return objDC.GetScalarVal(cmd);
    }

    public DataTable GetSalaryAdvance(string strLoanTypeId)
    {
        string strSQL = "Select * from LOANTYPE,SALARYHEAD where SALARYHEAD.SHEADID=LOANTYPE.SHEADID And ISSALADV='Y' AND LOANTYPE.LOANTYPEID=@LOANTYPEID ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_LOANTYPEID = cmd.Parameters.Add("LOANTYPEID", SqlDbType.BigInt);
        p_LOANTYPEID.Direction = ParameterDirection.Input;
        p_LOANTYPEID.Value = strLoanTypeId;

        objDC.CreateDT(cmd, "SalaryAdvance");
        return objDC.ds.Tables["SalaryAdvance"];
    }

    public DataTable GetAdvanceList(string strEmpId)
    {
        string strSQL = "SELECT LoanTypeID,LOANAPPID,APPDATE , REQDATE, LOANAMNT, NOOFINSTALL "
                + " FROM EmpSalLoanMst  WHERE EmpID=@EmpId AND LoanStatus='P'";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        objDC.CreateDT(cmd, "AdvanceList");
        return objDC.ds.Tables["AdvanceList"];
    }

    public DataTable GetLoanMasterRecord(string strAppId)
    {
        string strSQL = " SELECT * FROM EmpSalLoanMst WHERE LOANAPPID=@LOANAPPID ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_LOANAPPID = cmd.Parameters.Add("LOANAPPID", SqlDbType.BigInt);
        p_LOANAPPID.Direction = ParameterDirection.Input;
        p_LOANAPPID.Value = strAppId;

        objDC.CreateDT(cmd, "LoanMasterRecord");
        return objDC.ds.Tables["LoanMasterRecord"];
    }

    public DataTable getLoanDetailsRecord(string strAppId)
    {
        string strSQL = " SELECT * FROM EmpSalLoanDet WHERE LOANAPPID=@LOANAPPID ORDER BY INSSEQNO ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_LOANAPPID = cmd.Parameters.Add("LOANAPPID", SqlDbType.BigInt);
        p_LOANAPPID.Direction = ParameterDirection.Input;
        p_LOANAPPID.Value = strAppId;

        objDC.CreateDT(cmd, "LoanDetailsRecord");
        return objDC.ds.Tables["LoanDetailsRecord"];
    }

    // New Loan Active Code. Earlier code form are not in use.
    #region Loan

    public void InsertPFLoanData(string strTransID, string strTransDate, string strEmpID, string strMonth, string strFinYear,
        string strAmount, string strRate, string strRepay, string strInst, string strInterest,
        string strChqNo, string strChqDate, string strBank, string strStatus, string strIsComplete, 
        string strIsUpdate,string strInsBy, string strInsDate)
    {
        SqlCommand[] cmd = new SqlCommand[2];
        cmd[0] = new SqlCommand("Proc_Payroll_Insert_EmpPFLoanMst");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_TRANSID = cmd[0].Parameters.Add("TRANSID", SqlDbType.Char);
        p_TRANSID.Direction = ParameterDirection.Input;
        p_TRANSID.Value = strTransID;

        SqlParameter p_TRANSDATE = cmd[0].Parameters.Add("TRANSDATE", SqlDbType.DateTime);
        p_TRANSDATE.Direction = ParameterDirection.Input;
        p_TRANSDATE.Value = strTransDate;

        SqlParameter p_EMPID = cmd[0].Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        SqlParameter p_LOANMONTH = cmd[0].Parameters.Add("LOANMONTH", SqlDbType.BigInt);
        p_LOANMONTH.Direction = ParameterDirection.Input;
        p_LOANMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID = cmd[0].Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        SqlParameter p_LOANAMT = cmd[0].Parameters.Add("LOANAMT", SqlDbType.Decimal);
        p_LOANAMT.Direction = ParameterDirection.Input;
        p_LOANAMT.Value = strAmount;

        SqlParameter p_LOANRATE = cmd[0].Parameters.Add("LOANRATE", SqlDbType.Decimal);
        p_LOANRATE.Direction = ParameterDirection.Input;
        p_LOANRATE.Value = strRate;

        SqlParameter p_MONTHLYREPAY = cmd[0].Parameters.Add("MONTHLYREPAY", SqlDbType.Decimal);
        p_MONTHLYREPAY.Direction = ParameterDirection.Input;
        p_MONTHLYREPAY.Value = strRepay;

        SqlParameter p_INSTALLMENT = cmd[0].Parameters.Add("INSTALLMENT", SqlDbType.BigInt);
        p_INSTALLMENT.Direction = ParameterDirection.Input;
        p_INSTALLMENT.Value = strInst;

        SqlParameter p_MONTHLYINTEREST = cmd[0].Parameters.Add("MONTHLYINTEREST", SqlDbType.Decimal);
        p_MONTHLYINTEREST.Direction = ParameterDirection.Input;
        p_MONTHLYINTEREST.Value = strInterest;

        SqlParameter p_CHEQUENUMER = cmd[0].Parameters.Add("CHEQUENUMER", SqlDbType.VarChar);
        p_CHEQUENUMER.Direction = ParameterDirection.Input;
        p_CHEQUENUMER.Value = strChqNo;

        SqlParameter p_CHEQUEDATE = cmd[0].Parameters.Add("CHEQUEDATE", DBNull.Value);
        p_CHEQUEDATE.Direction = ParameterDirection.Input;
        p_CHEQUEDATE.IsNullable = true;
        if(string.IsNullOrEmpty(strChqDate) ==false)
            p_CHEQUEDATE.Value = strChqDate;

        SqlParameter p_BANKDETAIL = cmd[0].Parameters.Add("BANKDETAIL", SqlDbType.VarChar);
        p_BANKDETAIL.Direction = ParameterDirection.Input;
        p_BANKDETAIL.Value = strBank;

        SqlParameter p_LOANSTATUS = cmd[0].Parameters.Add("LOANSTATUS", SqlDbType.Char);
        p_LOANSTATUS.Direction = ParameterDirection.Input;
        p_LOANSTATUS.Value = strStatus;

        SqlParameter p_ISDEDUCTCOMPLETE = cmd[0].Parameters.Add("ISDEDUCTCOMPLETE", SqlDbType.Char);
        p_ISDEDUCTCOMPLETE.Direction = ParameterDirection.Input;
        p_ISDEDUCTCOMPLETE.Value = strIsComplete;

        SqlParameter p_INSERTEDBY = cmd[0].Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = strInsBy;

        SqlParameter p_INSERTEDATE = cmd[0].Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDATE.Direction = ParameterDirection.Input;
        p_INSERTEDATE.Value = strInsDate;

        SqlParameter p_ISUPDATE = cmd[0].Parameters.Add("ISUPDATE", SqlDbType.Char);
        p_ISUPDATE.Direction = ParameterDirection.Input;
        p_ISUPDATE.Value = strIsUpdate;


        objDC.MakeTransaction(cmd);
    }

    public void InsertCULoanData(string strTransID, string strTransDate, string strEmpID, string strMonth, string strFinYear,
        string strAmount, string strRate, string strRepay, string strInst, string strInterest,
        string strChqNo, string strChqDate, string strBank, string strStatus, string strIsComplete,
        string strIsUpdate, string strInsBy, string strInsDate)
    {
        SqlCommand[] cmd = new SqlCommand[2];
        cmd[0] = new SqlCommand("Proc_Payroll_Insert_EmpCULoanMst");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_TRANSID = cmd[0].Parameters.Add("TRANSID", SqlDbType.Char);
        p_TRANSID.Direction = ParameterDirection.Input;
        p_TRANSID.Value = strTransID;

        SqlParameter p_TRANSDATE = cmd[0].Parameters.Add("TRANSDATE", SqlDbType.DateTime);
        p_TRANSDATE.Direction = ParameterDirection.Input;
        p_TRANSDATE.Value = strTransDate;

        SqlParameter p_EMPID = cmd[0].Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        SqlParameter p_LOANMONTH = cmd[0].Parameters.Add("LOANMONTH", SqlDbType.BigInt);
        p_LOANMONTH.Direction = ParameterDirection.Input;
        p_LOANMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID = cmd[0].Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        SqlParameter p_LOANAMT = cmd[0].Parameters.Add("LOANAMT", SqlDbType.Decimal);
        p_LOANAMT.Direction = ParameterDirection.Input;
        p_LOANAMT.Value = strAmount;

        SqlParameter p_LOANRATE = cmd[0].Parameters.Add("LOANRATE", SqlDbType.Decimal);
        p_LOANRATE.Direction = ParameterDirection.Input;
        p_LOANRATE.Value = strRate;

        SqlParameter p_MONTHLYREPAY = cmd[0].Parameters.Add("MONTHLYREPAY", SqlDbType.Decimal);
        p_MONTHLYREPAY.Direction = ParameterDirection.Input;
        p_MONTHLYREPAY.Value = strRepay;

        SqlParameter p_INSTALLMENT = cmd[0].Parameters.Add("INSTALLMENT", SqlDbType.BigInt);
        p_INSTALLMENT.Direction = ParameterDirection.Input;
        p_INSTALLMENT.Value = strInst;

        SqlParameter p_MONTHLYINTEREST = cmd[0].Parameters.Add("MONTHLYINTEREST", SqlDbType.Decimal);
        p_MONTHLYINTEREST.Direction = ParameterDirection.Input;
        p_MONTHLYINTEREST.Value = strInterest;

        SqlParameter p_CHEQUENUMER = cmd[0].Parameters.Add("CHEQUENUMER", SqlDbType.VarChar);
        p_CHEQUENUMER.Direction = ParameterDirection.Input;
        p_CHEQUENUMER.Value = strChqNo;

        SqlParameter p_CHEQUEDATE = cmd[0].Parameters.Add("CHEQUEDATE", DBNull.Value);
        p_CHEQUEDATE.Direction = ParameterDirection.Input;
        p_CHEQUEDATE.IsNullable = true;
        if (string.IsNullOrEmpty(strChqDate) == false)
            p_CHEQUEDATE.Value = strChqDate;

        SqlParameter p_BANKDETAIL = cmd[0].Parameters.Add("BANKDETAIL", SqlDbType.VarChar);
        p_BANKDETAIL.Direction = ParameterDirection.Input;
        p_BANKDETAIL.Value = strBank;

        SqlParameter p_LOANSTATUS = cmd[0].Parameters.Add("LOANSTATUS", SqlDbType.Char);
        p_LOANSTATUS.Direction = ParameterDirection.Input;
        p_LOANSTATUS.Value = strStatus;

        SqlParameter p_ISDEDUCTCOMPLETE = cmd[0].Parameters.Add("ISDEDUCTCOMPLETE", SqlDbType.Char);
        p_ISDEDUCTCOMPLETE.Direction = ParameterDirection.Input;
        p_ISDEDUCTCOMPLETE.Value = strIsComplete;

        SqlParameter p_INSERTEDBY = cmd[0].Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = strInsBy;

        SqlParameter p_INSERTEDATE = cmd[0].Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDATE.Direction = ParameterDirection.Input;
        p_INSERTEDATE.Value = strInsDate;

        SqlParameter p_ISUPDATE = cmd[0].Parameters.Add("ISUPDATE", SqlDbType.Char);
        p_ISUPDATE.Direction = ParameterDirection.Input;
        p_ISUPDATE.Value = strIsUpdate;

        //// Insert PF Balance Data
        //cmd[1] = new SqlCommand("PROC_PAYROLL_INSERT_EMPPFBalance");
        //cmd[1].CommandType = CommandType.StoredProcedure;

        //SqlParameter p_TRANSID1 = cmd[1].Parameters.Add("TRANSID", SqlDbType.Char);
        //p_TRANSID1.Direction = ParameterDirection.Input;
        //p_TRANSID1.Value = Common.getMaxIdVar("EMPPFLOANBALANCE", "TRANSID", 7);

        //SqlParameter p_EMPID1 = cmd[1].Parameters.Add("EMPID", SqlDbType.Char);
        //p_EMPID1.Direction = ParameterDirection.Input;
        //p_EMPID1.Value = strEmpID;

        //SqlParameter p_LOANBALANCE = cmd[1].Parameters.Add("LOANBALANCE", SqlDbType.Decimal);
        //p_LOANBALANCE.Direction = ParameterDirection.Input;
        //p_LOANBALANCE.Value = strAmount;

        //SqlParameter p_LOANREFUND = cmd[1].Parameters.Add("LOANREFUND", SqlDbType.Decimal);
        //p_LOANREFUND.Direction = ParameterDirection.Input;
        //p_LOANREFUND.Value = "0";

        //SqlParameter p_INSERTEDBY1 = cmd[1].Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        //p_INSERTEDBY1.Direction = ParameterDirection.Input;
        //p_INSERTEDBY1.Value = strInsBy;

        //SqlParameter p_INSERTEDATE1 = cmd[1].Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        //p_INSERTEDATE1.Direction = ParameterDirection.Input;
        //p_INSERTEDATE1.Value = strInsDate;

        //SqlParameter p_ISUPDATE1 = cmd[1].Parameters.Add("ISUPDATE", SqlDbType.Char);
        //p_ISUPDATE1.Direction = ParameterDirection.Input;
        //p_ISUPDATE1.Value = this.IsPFBalanceExist(strEmpID);


        objDC.MakeTransaction(cmd);
    }


    public void InsertPFLoanAdjustmentData(string strTransID, string strTransDate, string strEmpID, string strMonth, string strYear,
        string strFinYear, string strType,string strAmount, string strPrincDue, string strIntDue,string strRemark,
        string strIsUpdate, string strInsBy, string strInsDate)
    {
        SqlCommand[] cmd = new SqlCommand[2];
        cmd[0] = new SqlCommand("PROC_PAYROLL_INSERT_PFLOANADJUSTMENT");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_TRANSID = cmd[0].Parameters.Add("TRANSID", SqlDbType.Char);
        p_TRANSID.Direction = ParameterDirection.Input;
        p_TRANSID.Value = strTransID;

        SqlParameter p_TRANSDATE = cmd[0].Parameters.Add("TRANSDATE", SqlDbType.DateTime);
        p_TRANSDATE.Direction = ParameterDirection.Input;
        p_TRANSDATE.Value = strTransDate;

        SqlParameter p_EMPID = cmd[0].Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        SqlParameter p_LOANMONTH = cmd[0].Parameters.Add("ADJMONTH", SqlDbType.BigInt);
        p_LOANMONTH.Direction = ParameterDirection.Input;
        p_LOANMONTH.Value = strMonth;

        SqlParameter p_ADJYEAR = cmd[0].Parameters.Add("ADJYEAR", SqlDbType.BigInt);
        p_ADJYEAR.Direction = ParameterDirection.Input;
        p_ADJYEAR.Value = strYear;

        SqlParameter p_FISCALYRID = cmd[0].Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        SqlParameter p_ADJTYPE = cmd[0].Parameters.Add("ADJTYPE", SqlDbType.VarChar);
        p_ADJTYPE.Direction = ParameterDirection.Input;
        p_ADJTYPE.Value = strType;

        SqlParameter p_ADJAMOUNT = cmd[0].Parameters.Add("ADJAMOUNT", SqlDbType.Decimal);
        p_ADJAMOUNT.Direction = ParameterDirection.Input;
        p_ADJAMOUNT.Value = strAmount;

        SqlParameter p_PrincDue = cmd[0].Parameters.Add("PRINCIPALDUE", SqlDbType.Decimal);
        p_PrincDue.Direction = ParameterDirection.Input;
        p_PrincDue.Value = strPrincDue;

        SqlParameter p_IntDue = cmd[0].Parameters.Add("INTDUE", SqlDbType.Decimal);
        p_IntDue.Direction = ParameterDirection.Input;
        p_IntDue.Value = strIntDue;

        SqlParameter p_REMARK = cmd[0].Parameters.Add("REMARK", SqlDbType.VarChar);
        p_REMARK.Direction = ParameterDirection.Input;
        p_REMARK.Value = strRemark;

        SqlParameter p_INSERTEDBY = cmd[0].Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = strInsBy;

        SqlParameter p_INSERTEDATE = cmd[0].Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDATE.Direction = ParameterDirection.Input;
        p_INSERTEDATE.Value = strInsDate;

        SqlParameter p_ISUPDATE = cmd[0].Parameters.Add("ISUPDATE", SqlDbType.Char);
        p_ISUPDATE.Direction = ParameterDirection.Input;
        p_ISUPDATE.Value = strIsUpdate;

        //if (strType != "Deduction")
        //{
        //Insert PF Balance Data
        //cmd[1] = new SqlCommand("PROC_PAYROLL_UPDATE_EMPPFBalanceAndRefund");
        //cmd[1].CommandType = CommandType.StoredProcedure;

        //SqlParameter p_EMPID1 = cmd[1].Parameters.Add("EMPID", SqlDbType.Char);
        //p_EMPID1.Direction = ParameterDirection.Input;
        //p_EMPID1.Value = strEmpID;

        //SqlParameter p_LOANREFUND = cmd[1].Parameters.Add("LOANREFUND", SqlDbType.Decimal);
        //p_LOANREFUND.Direction = ParameterDirection.Input;
        //p_LOANREFUND.Value = strAmount;

        //SqlParameter p_INSERTEDBY1 = cmd[1].Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        //p_INSERTEDBY1.Direction = ParameterDirection.Input;
        //p_INSERTEDBY1.Value = strInsBy;

        //SqlParameter p_INSERTEDATE1 = cmd[1].Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        //p_INSERTEDATE1.Direction = ParameterDirection.Input;
        //p_INSERTEDATE1.Value = strInsDate;

        //}
        //else
        //{
        //Update Monthly Reapy
        //get Max Trans ID
        string strSQL = "SELECT MAX(TRANSID) FROM EMPPFLOANMST WHERE EMPID=@EMPID";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;
        SqlParameter p_EMPID2 = command.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID2.Direction = ParameterDirection.Input;
        p_EMPID2.Value = strEmpID;
        string strMaxTransID = objDC.GetScalarVal(command);
        // End

        cmd[1] = new SqlCommand("UPDATE EMPPFLOANMST SET MONTHLYREPAYADJ=@MONTHLYREPAYADJ,UPDATEDBY=@INSERTEDBY,UPDATEDDATE=@INSERTEDDATE WHERE TRANSID=@TRANSID");
        cmd[1].CommandType = CommandType.Text;

        SqlParameter p_TRANSID1 = cmd[1].Parameters.Add("TRANSID", SqlDbType.Char);
        p_TRANSID1.Direction = ParameterDirection.Input;
        p_TRANSID1.Value = strMaxTransID;

        SqlParameter p_MONTHLYREPAYADJ = cmd[1].Parameters.Add("MONTHLYREPAYADJ", SqlDbType.Decimal);
        p_MONTHLYREPAYADJ.Direction = ParameterDirection.Input;
        p_MONTHLYREPAYADJ.Value = strAmount;


        SqlParameter p_INSERTEDBY1 = cmd[1].Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY1.Direction = ParameterDirection.Input;
        p_INSERTEDBY1.Value = strInsBy;

        SqlParameter p_INSERTEDATE1 = cmd[1].Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDATE1.Direction = ParameterDirection.Input;
        p_INSERTEDATE1.Value = strInsDate;
        //}

        objDC.MakeTransaction(cmd);
    }

    public void InsertCULoanAdjustmentData(string strTransID, string strTransDate, string strEmpID, string strMonth, string strYear,
        string strFinYear, string strType,
        string strAmount, string strRemark,
        string strIsUpdate, string strInsBy, string strInsDate)
    {
        SqlCommand[] cmd = new SqlCommand[2];
        cmd[0] = new SqlCommand("PROC_PAYROLL_INSERT_CULOANADJUSTMENT");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_TRANSID = cmd[0].Parameters.Add("TRANSID", SqlDbType.Char);
        p_TRANSID.Direction = ParameterDirection.Input;
        p_TRANSID.Value = strTransID;

        SqlParameter p_TRANSDATE = cmd[0].Parameters.Add("TRANSDATE", SqlDbType.DateTime);
        p_TRANSDATE.Direction = ParameterDirection.Input;
        p_TRANSDATE.Value = strTransDate;

        SqlParameter p_EMPID = cmd[0].Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        SqlParameter p_LOANMONTH = cmd[0].Parameters.Add("ADJMONTH", SqlDbType.BigInt);
        p_LOANMONTH.Direction = ParameterDirection.Input;
        p_LOANMONTH.Value = strMonth;

        SqlParameter p_ADJYEAR = cmd[0].Parameters.Add("ADJYEAR", SqlDbType.BigInt);
        p_ADJYEAR.Direction = ParameterDirection.Input;
        p_ADJYEAR.Value = strYear;

        SqlParameter p_FISCALYRID = cmd[0].Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        SqlParameter p_ADJTYPE = cmd[0].Parameters.Add("ADJTYPE", SqlDbType.VarChar);
        p_ADJTYPE.Direction = ParameterDirection.Input;
        p_ADJTYPE.Value = strType;

        SqlParameter p_ADJAMOUNT = cmd[0].Parameters.Add("ADJAMOUNT", SqlDbType.Decimal);
        p_ADJAMOUNT.Direction = ParameterDirection.Input;
        p_ADJAMOUNT.Value = strAmount;


        SqlParameter p_REMARK = cmd[0].Parameters.Add("REMARK", SqlDbType.VarChar);
        p_REMARK.Direction = ParameterDirection.Input;
        p_REMARK.Value = strRemark;

        SqlParameter p_INSERTEDBY = cmd[0].Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = strInsBy;

        SqlParameter p_INSERTEDATE = cmd[0].Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDATE.Direction = ParameterDirection.Input;
        p_INSERTEDATE.Value = strInsDate;

        SqlParameter p_ISUPDATE = cmd[0].Parameters.Add("ISUPDATE", SqlDbType.Char);
        p_ISUPDATE.Direction = ParameterDirection.Input;
        p_ISUPDATE.Value = strIsUpdate;

        if (strType == "Deduction")
        {
            //Update Monthly Reapy
            //get Max Trans ID
            string strSQL = "SELECT MAX(TRANSID) FROM EMPCULOANMST WHERE EMPID=@EMPID";
            SqlCommand command = new SqlCommand(strSQL);
            command.CommandType = CommandType.Text;
            SqlParameter p_EMPID2 = command.Parameters.Add("EMPID", SqlDbType.Char);
            p_EMPID2.Direction = ParameterDirection.Input;
            p_EMPID2.Value = strEmpID;
            string strMaxTransID = objDC.GetScalarVal(command);
            // End

            cmd[1] = new SqlCommand("UPDATE EMPCULOANMST SET MONTHLYREPAYADJ=@MONTHLYREPAYADJ,UPDATEDBY=@INSERTEDBY,UPDATEDDATE=@INSERTEDDATE WHERE TRANSID=@TRANSID");
            cmd[1].CommandType = CommandType.Text;

            SqlParameter p_TRANSID1 = cmd[1].Parameters.Add("TRANSID", SqlDbType.Char);
            p_TRANSID1.Direction = ParameterDirection.Input;
            p_TRANSID1.Value = strMaxTransID;

            SqlParameter p_MONTHLYREPAYADJ = cmd[1].Parameters.Add("MONTHLYREPAYADJ", SqlDbType.Decimal);
            p_MONTHLYREPAYADJ.Direction = ParameterDirection.Input;
            p_MONTHLYREPAYADJ.Value = strAmount;


            SqlParameter p_INSERTEDBY1 = cmd[1].Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
            p_INSERTEDBY1.Direction = ParameterDirection.Input;
            p_INSERTEDBY1.Value = strInsBy;

            SqlParameter p_INSERTEDATE1 = cmd[1].Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
            p_INSERTEDATE1.Direction = ParameterDirection.Input;
            p_INSERTEDATE1.Value = strInsDate;
        }

        objDC.MakeTransaction(cmd);
    }

   

    public void InsertPFLoanLedgerData( DataTable dtEmpPayroll,string strMonth, string strYear,string strFinYear, 
        string strType,
        string strInsBy, string strInsDate)
    {

        DataTable dtDistinctLoanee = this.GetDintinctLoaneeRecord(strMonth,strFinYear);
        dtDistinctLoanee=Common.SelectDistinct("DistinctEmpForPFLoan", dtDistinctLoanee, "EMPID", "EMPID");       

        // Previous Month Loan Ledger Record
        string strFY = "";
        if (strMonth == "4")
            strFY = Convert.ToString(Convert.ToInt32(strFinYear) - 1);
        else
            strFY = strFinYear;
        DataTable dtPrevMonthLoanLedger = this.GetPreviousMonthLoanLedgerRecord(Common.GetPreviousMonth(strMonth), strFY);
        DataRow[] foundRowsPrevMonth;

        DataTable dtCurrentMonthLoan = this.GetCurrentMonthLoan(strMonth, strFinYear, "PF");
        DataRow[] foundRowsCurrentMonthLoan;

        DataTable dtLoanAdjustment = this.GetCurrentMonthLoanAdjustmentRecord(strMonth, strFinYear);
        DataRow[] foundRowsCurrentMonthLoanAdj;

        SqlCommand[] cmd = new SqlCommand[dtEmpPayroll.Rows.Count];
        string strTransID = Common.getMaxIdWithCast("PFLOANLEDGER", "TRANSID");
        string strOpLoan="0";
        string strLMReapay = "0";
        string strCMDate="";
        string strCMLoanAmt="0";
        string strCMInts="0";
        string strCashDate = "";
        decimal decCash = 0;
        decimal decMonRepay = 0;
        decimal decLMTotalLoan = 0;
        decimal decCMLoanAmt = 0;
        decimal decLMTotalRepay = 0;
        decimal decLMTotalInterest = 0;

        decimal decTotalInterest = 0;

        Int32 iLoanNo=0;
        int iInstNo;
        DataRow[] fPayRows;
        int i = 0;
        foreach (DataRow dRow in dtDistinctLoanee.Rows)
        {
            fPayRows = dtEmpPayroll.Select("EMPID='" + dRow["EMPID"].ToString().Trim() + "'");

            strOpLoan="0";
            strLMReapay = "0";
            strCMDate="";
            strCMLoanAmt="0";
            strCMInts="0";
            strCashDate = "";
            decCash = 0;
            decMonRepay = 0;
            decLMTotalLoan = 0;
            decCMLoanAmt = 0;
            decLMTotalRepay = 0;
            decLMTotalInterest = 0;

            cmd[i] = new SqlCommand("PROC_PAYROLL_INSERT_PFLOANLEDGER");
            cmd[i].CommandType = CommandType.StoredProcedure;

            SqlParameter p_TRANSID = cmd[i].Parameters.Add("TRANSID", SqlDbType.Char);
            p_TRANSID.Direction = ParameterDirection.Input;
            p_TRANSID.Value = strTransID;

            SqlParameter p_EMPID = cmd[i].Parameters.Add("EMPID", SqlDbType.Char);
            p_EMPID.Direction = ParameterDirection.Input;
            p_EMPID.Value = dRow["EMPID"].ToString().Trim();

            SqlParameter p_VMONTH = cmd[i].Parameters.Add("VMONTH", SqlDbType.BigInt);
            p_VMONTH.Direction = ParameterDirection.Input;
            p_VMONTH.Value = strMonth;

            SqlParameter p_VYEAR = cmd[i].Parameters.Add("VYEAR", SqlDbType.BigInt);
            p_VYEAR.Direction = ParameterDirection.Input;
            p_VYEAR.Value = strYear;

            SqlParameter p_FISCALYRID = cmd[i].Parameters.Add("FISCALYRID", SqlDbType.BigInt);
            p_FISCALYRID.Direction = ParameterDirection.Input;
            p_FISCALYRID.Value = strFinYear;

            // Opening Loan
            foundRowsPrevMonth = dtPrevMonthLoanLedger.Select("EMPID='" + dRow["EMPID"].ToString().Trim() + "'");
            if (foundRowsPrevMonth.Length > 0)
            {
                strOpLoan = foundRowsPrevMonth[0]["CLLOAN"].ToString().Trim();
                strLMReapay = foundRowsPrevMonth[0]["CMREPAY"].ToString().Trim();
                decLMTotalLoan = Common.RoundDecimal(foundRowsPrevMonth[0]["TOTALLOAN"].ToString().Trim(), 0);
                decLMTotalRepay = Common.RoundDecimal(foundRowsPrevMonth[0]["TOTALREPAID"].ToString().Trim(), 0);
                decLMTotalInterest = Common.RoundDecimal(foundRowsPrevMonth[0]["TOTALINTEREST"].ToString().Trim(), 0);

                iLoanNo = Convert.ToInt32(foundRowsPrevMonth[0]["LOANNO"]);
                strCMInts = Convert.ToString(Convert.ToInt16(foundRowsPrevMonth[0]["CMINTS"]) - 1);               
            }

            //SqlParameter p_OPLOAN = cmd[i].Parameters.Add("OPLOAN", SqlDbType.Decimal);
            //p_OPLOAN.Direction = ParameterDirection.Input;
            //p_OPLOAN.Value = strOpLoan;
            // Previous Month Monthly Repay
            SqlParameter p_LMREPAY = cmd[i].Parameters.Add("LMREPAY", SqlDbType.Decimal);
            p_LMREPAY.Direction = ParameterDirection.Input;
            p_LMREPAY.Value = strLMReapay;


            // Current Month Loan
            foundRowsCurrentMonthLoan = dtCurrentMonthLoan.Select("EMPID='" + dRow["EMPID"].ToString().Trim() + "'");
            if (foundRowsCurrentMonthLoan.Length > 0)
            {
                strOpLoan = foundRowsCurrentMonthLoan[0]["LOANAMT"].ToString().Trim();
                strCMDate = Common.SetDate(foundRowsCurrentMonthLoan[0]["CHEQUEDATE"].ToString().Trim());
                strCMLoanAmt = foundRowsCurrentMonthLoan[0]["LOANAMT"].ToString().Trim();
                strCMInts = foundRowsCurrentMonthLoan[0]["INSTALLMENT"].ToString().Trim();
                decCMLoanAmt = Common.RoundDecimal(foundRowsCurrentMonthLoan[0]["LOANAMT"].ToString().Trim(), 0);

                iLoanNo = Convert.ToInt32(foundRowsCurrentMonthLoan[0]["LOANNO"]);
                strCMInts = Convert.ToString(Convert.ToInt16(foundRowsCurrentMonthLoan[0]["INSTALLMENT"]) - 1);
            }

            SqlParameter p_OPLOAN = cmd[i].Parameters.Add("OPLOAN", SqlDbType.Decimal);
            p_OPLOAN.Direction = ParameterDirection.Input;
            p_OPLOAN.Value = strOpLoan;

            SqlParameter p_CMDATE = cmd[i].Parameters.Add("CMDATE", DBNull.Value);
            p_CMDATE.Direction = ParameterDirection.Input;
            p_CMDATE.IsNullable = true;
            if(string.IsNullOrEmpty(strCMDate)==false)
                p_CMDATE.Value = strCMDate;

            SqlParameter p_CMLOANAMT = cmd[i].Parameters.Add("CMLOANAMT", SqlDbType.Decimal);
            p_CMLOANAMT.Direction = ParameterDirection.Input;
            p_CMLOANAMT.Value = strCMLoanAmt;

          
                // Current Month Loan Adjustment
                foundRowsCurrentMonthLoanAdj = dtLoanAdjustment.Select("EMPID='" + dRow["EMPID"].ToString().Trim() + "'");
            if (foundRowsCurrentMonthLoanAdj.Length > 0)
            {
                decCash = Common.RoundDecimal(foundRowsCurrentMonthLoanAdj[0]["ADJAMOUNT"].ToString().Trim(), 0);
                strCashDate = foundRowsCurrentMonthLoanAdj[0]["TRANSDATE"].ToString().Trim();
            }
            SqlParameter p_CMCASH = cmd[i].Parameters.Add("CMCASH", SqlDbType.Decimal);
            p_CMCASH.Direction = ParameterDirection.Input;
            p_CMCASH.Value = decCash.ToString();

            SqlParameter p_CMCASHDATE = cmd[i].Parameters.Add("CMCASHDATE", DBNull.Value);
            p_CMCASHDATE.Direction = ParameterDirection.Input;
            p_CMCASHDATE.IsNullable = true;
            if (string.IsNullOrEmpty(strCashDate) == false)
                p_CMCASHDATE.Value = Common.SetDate(strCashDate);

            // Current Month Loan Repay
            if (fPayRows.Length > 0)
            {
                decMonRepay = Common.RoundDecimal(fPayRows[0]["PFLOAN"].ToString().Trim(), 0);
                decMonRepay = Math.Abs(decMonRepay);
            }
            else
            {
                decMonRepay = 0;
            }
            SqlParameter p_CMREPAY = cmd[i].Parameters.Add("CMREPAY", SqlDbType.Decimal);
            p_CMREPAY.Direction = ParameterDirection.Input;
            p_CMREPAY.Value = decMonRepay;
            
            // Current Month Total Repay 
            SqlParameter p_CMTOTALREPAY = cmd[i].Parameters.Add("CMTOTALREPAY", SqlDbType.Decimal);
            p_CMTOTALREPAY.Direction = ParameterDirection.Input;
            p_CMTOTALREPAY.Value = decCash + decMonRepay;

            // Accumulated Total Loan
            SqlParameter p_TOTALLOAN = cmd[i].Parameters.Add("TOTALLOAN", SqlDbType.Decimal);
            p_TOTALLOAN.Direction = ParameterDirection.Input;
            p_TOTALLOAN.Value = decLMTotalLoan + decCMLoanAmt;

            // Accumulated Total Loan Repaid
            SqlParameter p_TOTALREPAID = cmd[i].Parameters.Add("TOTALREPAID", SqlDbType.Decimal);
            p_TOTALREPAID.Direction = ParameterDirection.Input;
            p_TOTALREPAID.Value = decLMTotalRepay + decCash + decMonRepay;

            // Closing Balance
            SqlParameter p_CLLOAN = cmd[i].Parameters.Add("CLLOAN", SqlDbType.Decimal);
            p_CLLOAN.Direction = ParameterDirection.Input;
            p_CLLOAN.Value = Convert.ToDecimal(strOpLoan) + Convert.ToDecimal(strCMLoanAmt) - decCash - decMonRepay;

            SqlParameter p_CMINTS = cmd[i].Parameters.Add("CMINTS", SqlDbType.Decimal);
            p_CMINTS.Direction = ParameterDirection.Input;
            if (Convert.ToDecimal(p_CLLOAN.Value) != 0)
                p_CMINTS.Value = strCMInts;
            else
                p_CMINTS.Value = 0;

            // Current Month Interest
          
            SqlParameter p_CMINTEREST = cmd[i].Parameters.Add("CMINTEREST", SqlDbType.Decimal);
            p_CMINTEREST.Direction = ParameterDirection.Input;
            if (fPayRows.Length > 0)
            {
                p_CMINTEREST.Value = Math.Abs(Common.RoundDecimal(fPayRows[0]["PFINT"].ToString().Trim(), 0));
            }
            else
            {
                p_CMINTEREST.Value ="0";
            }

            // Total Interest
            SqlParameter p_TOTALINTEREST = cmd[i].Parameters.Add("TOTALINTEREST", SqlDbType.Decimal);
            p_TOTALINTEREST.Direction = ParameterDirection.Input;
            if (foundRowsCurrentMonthLoanAdj.Length > 0)
                decTotalInterest = Common.RoundDecimal(foundRowsCurrentMonthLoanAdj[0]["INTDUE"].ToString().Trim(), 0);
            else
                decTotalInterest =Convert.ToDecimal(cmd[i].Parameters.Add("TOTALINTEREST", SqlDbType.Decimal));

            p_TOTALINTEREST.Value = decTotalInterest.ToString();

          

            if (fPayRows.Length > 0)
            {
                p_TOTALINTEREST.Value = decLMTotalInterest + Math.Abs(Common.RoundDecimal(fPayRows[0]["PFINT"].ToString().Trim(), 0));
            }
            else
            {
                p_TOTALINTEREST.Value = decLMTotalInterest;
            }



            SqlParameter p_LOANNO = cmd[i].Parameters.Add("LOANNO", SqlDbType.BigInt);
            p_LOANNO.Direction = ParameterDirection.Input;
            p_LOANNO.Value = iLoanNo;
            
            SqlParameter p_INSERTEDBY = cmd[i].Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
            p_INSERTEDBY.Direction = ParameterDirection.Input;
            p_INSERTEDBY.Value = strInsBy;

            SqlParameter p_INSERTEDATE = cmd[i].Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
            p_INSERTEDATE.Direction = ParameterDirection.Input;
            p_INSERTEDATE.Value = strInsDate;

            // Next TransID
            int intCardLength = Convert.ToInt32(strTransID.Length);
            int num = Convert.ToInt16(strTransID) + 1;
            strTransID = num.ToString();
            i++;
           
        }

        objDC.MakeTransaction(cmd);
    }

    public void InsertCUWithdrawData(string strTransID, string strTransDate, string strEmpID, string strMonth,string strYear, string strFinYear,
        string strAmount,
        string strChqNo, string strChqDate, string strBank,
        string strIsUpdate, string strInsBy, string strInsDate)
    {
        SqlCommand[] cmd = new SqlCommand[1];
        cmd[0] = new SqlCommand("PROC_PAYROLL_INSERT_CUWITHDRAW");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_TRANSID = cmd[0].Parameters.Add("TRANSID", SqlDbType.Char);
        p_TRANSID.Direction = ParameterDirection.Input;
        p_TRANSID.Value = strTransID;

        SqlParameter p_TRANSDATE = cmd[0].Parameters.Add("TRANSDATE", SqlDbType.DateTime);
        p_TRANSDATE.Direction = ParameterDirection.Input;
        p_TRANSDATE.Value = strTransDate;

        SqlParameter p_EMPID = cmd[0].Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        SqlParameter p_LOANMONTH = cmd[0].Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_LOANMONTH.Direction = ParameterDirection.Input;
        p_LOANMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd[0].Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_FISCALYRID = cmd[0].Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        SqlParameter p_LOANAMT = cmd[0].Parameters.Add("WITHDRAWAMT", SqlDbType.Decimal);
        p_LOANAMT.Direction = ParameterDirection.Input;
        p_LOANAMT.Value = strAmount;

        SqlParameter p_CHEQUENUMER = cmd[0].Parameters.Add("ChequeNumber", SqlDbType.VarChar);
        p_CHEQUENUMER.Direction = ParameterDirection.Input;
        p_CHEQUENUMER.Value = strChqNo;

        SqlParameter p_CHEQUEDATE = cmd[0].Parameters.Add("CHEQUEDATE", DBNull.Value);
        p_CHEQUEDATE.Direction = ParameterDirection.Input;
        p_CHEQUEDATE.IsNullable = true;
        if (string.IsNullOrEmpty(strChqDate) == false)
            p_CHEQUEDATE.Value =  Common.ReturnDate(strChqDate);

        SqlParameter p_BANKDETAIL = cmd[0].Parameters.Add("BankDetail", SqlDbType.VarChar);
        p_BANKDETAIL.Direction = ParameterDirection.Input;
        p_BANKDETAIL.Value = strBank;

        SqlParameter p_INSERTEDBY = cmd[0].Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = strInsBy;

        SqlParameter p_INSERTEDATE = cmd[0].Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDATE.Direction = ParameterDirection.Input;
        p_INSERTEDATE.Value = strInsDate;

        SqlParameter p_ISUPDATE = cmd[0].Parameters.Add("ISUPDATE", SqlDbType.Char);
        p_ISUPDATE.Direction = ParameterDirection.Input;
        p_ISUPDATE.Value = strIsUpdate;


        objDC.MakeTransaction(cmd);
    }

    public void InsertFinalPaymentPFData(string strTransID, string strTransDate, string strEmpID, string strMonth, string strYear, string strFinYear,
         string strAmount, string strLastWD, string strLastPFDeduct, string strLastPFInt, string strLastPFBal,
         string strChqNo, string strChqDate, string strBank,
         string strIsUpdate, string strInsBy, string strInsDate, string strPFArrear)
    {
        SqlCommand[] cmd = new SqlCommand[1];
        cmd[0] = new SqlCommand("PROC_PAYROLL_INSERT_FINALPAYMENTPF");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_TRANSID = cmd[0].Parameters.Add("TRANSID", SqlDbType.Char);
        p_TRANSID.Direction = ParameterDirection.Input;
        p_TRANSID.Value = strTransID;

        SqlParameter p_TRANSDATE = cmd[0].Parameters.Add("TRANSDATE", SqlDbType.DateTime);
        p_TRANSDATE.Direction = ParameterDirection.Input;
        p_TRANSDATE.Value = strTransDate;

        SqlParameter p_EMPID = cmd[0].Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        SqlParameter p_LOANMONTH = cmd[0].Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_LOANMONTH.Direction = ParameterDirection.Input;
        p_LOANMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd[0].Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_FISCALYRID = cmd[0].Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        SqlParameter p_PAYAMT = cmd[0].Parameters.Add("PAYAMT", SqlDbType.Decimal);
        p_PAYAMT.Direction = ParameterDirection.Input;
        p_PAYAMT.Value = strAmount;

        SqlParameter p_CHEQUENO = cmd[0].Parameters.Add("CHEQUENO", SqlDbType.VarChar);
        p_CHEQUENO.Direction = ParameterDirection.Input;
        p_CHEQUENO.Value = strChqNo;

        SqlParameter p_CHEQUEDATE = cmd[0].Parameters.Add("CHEQUEDATE", DBNull.Value);
        p_CHEQUEDATE.Direction = ParameterDirection.Input;
        p_CHEQUEDATE.IsNullable = true;
        if (string.IsNullOrEmpty(strChqDate) == false)
            p_CHEQUEDATE.Value = Common.ReturnDate(strChqDate);

        SqlParameter p_BANKDETAIL = cmd[0].Parameters.Add("BankDetail", SqlDbType.VarChar);
        p_BANKDETAIL.Direction = ParameterDirection.Input;
        p_BANKDETAIL.Value = strBank;

        SqlParameter p_LASTWD = cmd[0].Parameters.Add("LASTWD", SqlDbType.Decimal);
        p_LASTWD.Direction = ParameterDirection.Input;
        p_LASTWD.Value = strLastWD;

        SqlParameter p_LASTPFDEDUCT = cmd[0].Parameters.Add("LASTPFDEDUCT", SqlDbType.Decimal);
        p_LASTPFDEDUCT.Direction = ParameterDirection.Input;
        p_LASTPFDEDUCT.Value = strLastPFDeduct;

        SqlParameter p_LASTPFINT = cmd[0].Parameters.Add("LASTPFINT", SqlDbType.Decimal);
        p_LASTPFINT.Direction = ParameterDirection.Input;
        p_LASTPFINT.Value = strLastPFInt;

        SqlParameter p_LASTPFBALANCE = cmd[0].Parameters.Add("LASTPFBALANCE", SqlDbType.Decimal);
        p_LASTPFBALANCE.Direction = ParameterDirection.Input;
        p_LASTPFBALANCE.Value = strLastPFBal;

        SqlParameter p_PFARREAR = cmd[0].Parameters.Add("PFARREAR", SqlDbType.Decimal);
        p_PFARREAR.Direction = ParameterDirection.Input;
        p_PFARREAR.Value = strPFArrear;

        SqlParameter p_INSERTEDBY = cmd[0].Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = strInsBy;

        SqlParameter p_INSERTEDATE = cmd[0].Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDATE.Direction = ParameterDirection.Input;
        p_INSERTEDATE.Value = strInsDate;

        SqlParameter p_ISUPDATE = cmd[0].Parameters.Add("ISUPDATE", SqlDbType.Char);
        p_ISUPDATE.Direction = ParameterDirection.Input;
        p_ISUPDATE.Value = strIsUpdate;


        objDC.MakeTransaction(cmd);
    }


    private string IsPFBalanceExist(string strEmpID)
    {
        SqlCommand cmd = new SqlCommand("SELECT EMPID FROM EMPPFLOANBALANCE WHERE EMPID=@EMPID");
        cmd.CommandType = CommandType.Text;
        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;
        string strIsExist = objDC.GetScalarVal(cmd);
        if (string.IsNullOrEmpty(strIsExist) == false)
            return "Y";
        else
            return "N";
    }

    public DataTable getEmpLoanSummary(string strEmpID)
    {
        SqlCommand cmd = new SqlCommand("PROC_PAYROLL_SELECT_LOANSUMMARY");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        objDC.CreateDSFromProc(cmd, "getEmpLoanSummary");
        return objDC.ds.Tables["getEmpLoanSummary"];
    }

    public DataTable getPFLoanLedger(string strEmpID)
    {
        string strSQL = "select PL.*,EP.TOTALREPAY  from PFLOANLEDGER PL,EmpPFLoanMst EP WHERE PL.EmpId=EP.EmpId AND PL.LoanNo=EP.LOANNO AND EP.empid='" + strEmpID + "'"
            + " and cast(PL.TRANSID AS NUMERIC)= (select max(cast(TRANSID as NUMERIC)) from PFLOANLEDGER where empid = '" + strEmpID + "')";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        return objDC.CreateDT(cmd, "getPFLoanLedger"); ;
    }

    public DataTable getEmpPFLedger(string strEmpID)
    {
        SqlCommand cmd = new SqlCommand("PROC_PAYROLL_SELECT_PFLEDGERFORFINALPAYMENT");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        objDC.CreateDSFromProc(cmd, "getEmpPFLedger");
        return objDC.ds.Tables["getEmpPFLedger"];
    }

    public DataTable GetLoanRecord(string strEmpID)
    {
        string strSQL = "SELECT  EPP.*,F.FISCALYRTITLE  FROM EMPPFLOANMST EPP,FISCALYEARLIST F "
                    + " WHERE EPP.FISCALYRID=F.FISCALYRID AND EMPID=@EMPID ORDER BY TRANSID DESC";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        return objDC.CreateDT(cmd, "GetLoanRecord");
    }

    public DataTable GetCULoanRecord(string strEmpID)
    {
        string strSQL = "SELECT ECP.*,F.FISCALYRTITLE FROM EMPCULOANMST ECP,FISCALYEARLIST F"
                    + " WHERE ECP.FISCALYRID=F.FISCALYRID AND EMPID=@EMPID ORDER BY TRANSID DESC";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        return objDC.CreateDT(cmd, "GetCULoanRecord");
    }

    public DataTable GetSumLoanRecord(string strMonth,string strFinYear)
    {
        string strSQL = "SELECT EMPID,SUM(LOANAMT) AS LOANAMT FROM EMPPFLOANMST GROUP BY EMPID";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        return objDC.CreateDT(cmd, "GetSumLoanRecord");
    }


    public DataTable GetLoanAdjustmentRecord(string strEmpID)
    {
        string strSQL = "SELECT PLA.*,F.FISCALYRTITLE FROM PFLOANADJUSTMENT PLA,FISCALYEARLIST F"
                    + " WHERE PLA.FISCALYRID=F.FISCALYRID AND EMPID=@EMPID ORDER BY TRANSID DESC";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        return objDC.CreateDT(cmd, "GetLoanAdjustmentRecord");
    }

    public DataTable GetLoanAdjustmentRecord(string strEmpID,string strMonth,string strFinYear)
    {
        string strSQL = "SELECT PLA.*,F.FISCALYRTITLE FROM PFLOANADJUSTMENT PLA,FISCALYEARLIST F"
                    + " WHERE PLA.FISCALYRID=F.FISCALYRID AND EMPID=@EMPID AND PLA.FISCALYRID=@FISCALYRID "
                    + " AND PLA.ADJMONTH=@ADJMONTH  ORDER BY TRANSID DESC";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        SqlParameter p_ADJMONTH = cmd.Parameters.Add("ADJMONTH", SqlDbType.BigInt);
        p_ADJMONTH.Direction = ParameterDirection.Input;
        p_ADJMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        return objDC.CreateDT(cmd, "GetLoanAdjustmentRecord");
    }

    public DataTable GetCULoanAdjustmentRecord(string strEmpID)
    {
        string strSQL = "SELECT CLA.*,F.FISCALYRTITLE FROM CULOANADJUSTMENT CLA,FISCALYEARLIST F"
                    + " WHERE CLA.FISCALYRID=F.FISCALYRID AND EMPID=@EMPID ORDER BY TRANSID DESC";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        return objDC.CreateDT(cmd, "GetCULoanAdjustmentRecord");
    }

    public DataTable GetCurrentMonthLoanAdjustmentRecord(string strMonth,string strFinYear)
    {
        string strSQL = "SELECT * FROM PFLOANADJUSTMENT WHERE ADJMONTH=@ADJMONTH AND FISCALYRID=@FISCALYRID AND ADJTYPE='Cash Pay' ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_ADJMONTH = cmd.Parameters.Add("ADJMONTH", SqlDbType.BigInt);
        p_ADJMONTH.Direction = ParameterDirection.Input;
        p_ADJMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.Char);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        return objDC.CreateDT(cmd, "GetCurrentMonthLoanAdjustmentRecord");
    }

    public DataTable GetCurrentMonthLoanAdjustmentRecordCU(string strMonth, string strFinYear)
    {
        string strSQL = "SELECT * FROM CULOANADJUSTMENT WHERE ADJMONTH=@ADJMONTH AND FISCALYRID=@FISCALYRID AND ADJTYPE='Cash Pay' ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_ADJMONTH = cmd.Parameters.Add("ADJMONTH", SqlDbType.BigInt);
        p_ADJMONTH.Direction = ParameterDirection.Input;
        p_ADJMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.Char);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        return objDC.CreateDT(cmd, "GetCurrentMonthLoanAdjustmentRecordCU");
    }
    public DataTable GetCUWithdrawRecord(string strEmpID)
    {
        string strSQL = "SELECT CW.*,F.FISCALYRTITLE FROM CUWITHDRAW CW,FISCALYEARLIST F"
                    + " WHERE CW.FISCALYRID=F.FISCALYRID AND EMPID=@EMPID ORDER BY TRANSID DESC";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        return objDC.CreateDT(cmd, "GetCUWithdrawRecord");
    }

    public DataTable GetCUWithdrawRecord(string strEmpID,string strMonth,string strFinYear)
    {
        if (objDC.ds.Tables["GetCUWithdrawRecord2"] != null)
        {
            objDC.ds.Tables["GetCUWithdrawRecord2"].Rows.Clear();
            objDC.ds.Tables["GetCUWithdrawRecord2"].Dispose();
        }
        string strSQL = "SELECT * FROM CUWITHDRAW "
                    + " WHERE EMPID=@EMPID AND VMONTH=@VMONTH AND FISCALYRID=@FISCALYRID";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        return objDC.CreateDT(cmd, "GetCUWithdrawRecord2");
    }

    public DataTable GetFinalPaymentPFRecord(string strEmpID)
    {
        string strSQL = "SELECT FP.*,F.FISCALYRTITLE FROM FinalPaymentPF FP,FISCALYEARLIST F"
                    + " WHERE FP.FISCALYRID=F.FISCALYRID AND EMPID=@EMPID ORDER BY TRANSID DESC";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        return objDC.CreateDT(cmd, "GetFinalPaymentPFRecord");
    }

    public DataTable GetFinalPaymentPFRecord(string strMonth, string strFisCalYear, string strEmpID)
    {
        if (objDC.ds.Tables["GetFinalPaymentPFRecord2"] != null)
        {
            objDC.ds.Tables["GetFinalPaymentPFRecord2"].Rows.Clear();
            objDC.ds.Tables["GetFinalPaymentPFRecord2"].Dispose();
        }
        string strSQL = "SELECT * FROM FinalPaymentPF "
                    + " WHERE VMONTH=@VMONTH AND FISCALYRID=@FISCALYRID AND EMPID=@EMPID";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFisCalYear;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        return objDC.CreateDT(cmd, "GetFinalPaymentPFRecord2");
    }

    public bool IsCurrentMonthLoanExist(string strEmpID,string strMonth,string strFinYear,string strLoanType)
    {
        string strSQL = "";
        if(strLoanType=="PF")
            strSQL = "SELECT TRANSID FROM EMPPFLOANMST WHERE EMPID=@EMPID AND LOANMONTH=@LOANMONTH AND FISCALYRID=@FISCALYRID";
        else
            strSQL = "SELECT TRANSID FROM EMPCULOANMST WHERE EMPID=@EMPID AND LOANMONTH=@LOANMONTH AND FISCALYRID=@FISCALYRID";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;
        SqlParameter p_LOANMONTH = cmd.Parameters.Add("LOANMONTH", SqlDbType.BigInt);
        p_LOANMONTH.Direction = ParameterDirection.Input;
        p_LOANMONTH.Value = strMonth;
        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        string strValue = objDC.GetScalarVal(cmd);
        if (string.IsNullOrEmpty(strValue) == true)
            return false;
        else
            return true;
    }

    public bool IsCurrentMonthWithdrawExist(string strEmpID, string strMonth, string strYear)
    {
        string strSQL = "";
        strSQL = "SELECT TRANSID FROM CUWITHDRAW WHERE EMPID=@EMPID AND VMONTH=@VMONTH AND VYEAR=@VYEAR";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;
        SqlParameter p_LOANMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_LOANMONTH.Direction = ParameterDirection.Input;
        p_LOANMONTH.Value = strMonth;
        SqlParameter p_LOANYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_LOANYEAR.Direction = ParameterDirection.Input;
        p_LOANYEAR.Value = strYear;

        string strValue = objDC.GetScalarVal(cmd);
        if (string.IsNullOrEmpty(strValue) == true)
            return false;
        else
            return true;
    }

    public bool IsMonthFinalPaymentExist(string strEmpID,string strFisCalYrID)
    {
        string strSQL = "";
        strSQL = "SELECT TRANSID FROM FinalPaymentPF WHERE EMPID=@EMPID AND FISCALYRID=@FISCALYRID";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFisCalYrID;

        string strValue = objDC.GetScalarVal(cmd);
        if (string.IsNullOrEmpty(strValue) == true)
            return false;
        else
            return true;
    }


    public DataTable GetCurrentMonthLoan(string strMonth, string strFinYear, string strLoanType)
    {
        string strSQL = "";
        if (strLoanType == "PF")
            strSQL = "SELECT * FROM EMPPFLOANMST WHERE LOANMONTH=@LOANMONTH AND FISCALYRID=@FISCALYRID";
        else
            strSQL = "SELECT * FROM EMPCULOANMST WHERE LOANMONTH=@LOANMONTH AND FISCALYRID=@FISCALYRID";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_LOANMONTH = cmd.Parameters.Add("LOANMONTH", SqlDbType.BigInt);
        p_LOANMONTH.Direction = ParameterDirection.Input;
        p_LOANMONTH.Value = strMonth;
        SqlParameter p_LOANYEAR = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_LOANYEAR.Direction = ParameterDirection.Input;
        p_LOANYEAR.Value = strFinYear;

        return objDC.CreateDT(cmd, "GetCurrentMonthLoan");
    }

    private DataTable GetPreviousMonthLoanLedgerRecord(string strMonth,string strFY)
    {
        string strSQL = "SELECT * FROM PFLOANLEDGER WHERE VMONTH=@VMONTH AND FISCALYRID=@FISCALYRID";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_LOANMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_LOANMONTH.Direction = ParameterDirection.Input;
        p_LOANMONTH.Value = strMonth;
        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFY;

        return objDC.CreateDT(cmd, "GetPreviousMonthLoanLedgerRecord");
    }

    private DataTable GetPreviousMonthLoanLedgerRecordCU(string strMonth, string strFY)
    {
        string strSQL = "SELECT * FROM CULOANLEDGER WHERE VMONTH=@VMONTH AND FISCALYRID=@FISCALYRID";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_LOANMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_LOANMONTH.Direction = ParameterDirection.Input;
        p_LOANMONTH.Value = strMonth;
        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFY;

        return objDC.CreateDT(cmd, "GetPreviousMonthLoanLedgerRecordCU");
    }

    private DataTable GetDintinctLoaneeRecord(string strMonth, string strFinYear)
    {
        string strFY = "";
        if (strMonth == "4")
            strFY = Convert.ToString(Convert.ToInt32(strFinYear) - 1);
        else
            strFY = strFinYear;

        string strSQL = "SELECT EMPID AS EMPID FROM PFLoanLedger WHERE VMONTH=@VMONTH AND FISCALYRID=@FISCALYRID1"// AND EMPID IN('E000333','E004348')"
                       + " UNION ALL "
                       + " SELECT EMPID AS EMPID FROM EmpPFLoanMst WHERE LOANMONTH=@LOANMONTH AND FISCALYRID=@FISCALYRID2"//  AND EMPID IN('E000333','E004348')"
                       + " ORDER BY EMPID";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = Common.GetPreviousMonth(strMonth);

        SqlParameter p_LOANMONTH = cmd.Parameters.Add("LOANMONTH", SqlDbType.BigInt);
        p_LOANMONTH.Direction = ParameterDirection.Input;
        p_LOANMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID1 = cmd.Parameters.Add("FISCALYRID1", SqlDbType.BigInt);
        p_FISCALYRID1.Direction = ParameterDirection.Input;
        p_FISCALYRID1.Value = strFY;

        SqlParameter p_FISCALYRID2 = cmd.Parameters.Add("FISCALYRID2", SqlDbType.BigInt);
        p_FISCALYRID2.Direction = ParameterDirection.Input;
        p_FISCALYRID2.Value = strFinYear;

        return objDC.CreateDT(cmd, "GetDintinctLoaneeRecord");
    }

    private DataTable GetDintinctLoaneeRecordCU(string strMonth, string strFinYear)
    {
        string strFY = "";
        if (strMonth == "7")
            strFY = Convert.ToString(Convert.ToInt32(strFinYear) - 1);
        else
            strFY = strFinYear;

        string strSQL = "SELECT CAST(EMPID AS NUMERIC) AS EMPID FROM CULoanLedger WHERE VMONTH=@VMONTH AND FISCALYRID=@FISCALYRID1 "
                       + " UNION ALL "
                       + " SELECT CAST(EMPID AS NUMERIC) AS EMPID FROM EmpCULoanMst WHERE LOANMONTH=@LOANMONTH AND FISCALYRID=@FISCALYRID2 "
                       + " ORDER BY CAST(EMPID AS NUMERIC)";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = Common.GetPreviousMonth(strMonth);

        SqlParameter p_LOANMONTH = cmd.Parameters.Add("LOANMONTH", SqlDbType.BigInt);
        p_LOANMONTH.Direction = ParameterDirection.Input;
        p_LOANMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID1 = cmd.Parameters.Add("FISCALYRID1", SqlDbType.BigInt);
        p_FISCALYRID1.Direction = ParameterDirection.Input;
        p_FISCALYRID1.Value = strFY;

        SqlParameter p_FISCALYRID2 = cmd.Parameters.Add("FISCALYRID2", SqlDbType.BigInt);
        p_FISCALYRID2.Direction = ParameterDirection.Input;
        p_FISCALYRID2.Value = strFinYear;


        return objDC.CreateDT(cmd, "GetDintinctLoaneeRecordCU");
    }

    public DataSet GetPFMonthlyActivities(string strMonth, string strFY)
    {
        SqlCommand cmd = new SqlCommand("proc_payroll_select_MonthlyPFActivities");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LOANMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_LOANMONTH.Direction = ParameterDirection.Input;
        p_LOANMONTH.Value = strMonth;
        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFY;
        objDC.CreateDSFromProc(cmd, "GetPFMonthlyActivities");
        return objDC.ds;
    }

    public DataSet GetCUMonthlyActivities(string strMonth,string strFY)
    {
        SqlCommand cmd = new SqlCommand("proc_payroll_select_MonthlyCBEMCSLActivities");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LOANMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_LOANMONTH.Direction = ParameterDirection.Input;
        p_LOANMONTH.Value = strMonth;
        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFY;
        objDC.CreateDSFromProc(cmd, "GetCUMonthlyActivities");
        return objDC.ds;
    }

   

    #endregion

    public DataTable GetDistinctEmployeeForLedger(string strMonth, string strYear)
    {
        if (objDC.ds.Tables["GetDistinctEmployeeForLedger"] != null)
        {
            objDC.ds.Tables["GetDistinctEmployeeForLedger"].Rows.Clear();
            objDC.ds.Tables["GetDistinctEmployeeForLedger"].Dispose();
        }
        SqlCommand cmd = new SqlCommand("proc_payroll_select_DistinctEmployeeForLedger");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        objDC.CreateDSFromProc(cmd, "GetDistinctEmployeeForLedger");
        return objDC.ds.Tables["GetDistinctEmployeeForLedger"];
    }


    #region PF Ledger

    public void PreparePFLedgerData(DataTable dt, string strMonth, string strYear, string strStatus, string strInsBy, string strInsDate,
      string strSalType, string strFinYear)
    {
        int i = 0;
        SqlCommand[] command = new SqlCommand[dt.Rows.Count*2];
        long lngLedgerID = Convert.ToInt64(Common.getMaxId("PFLEDGER", "LEDGERID"));
        foreach (DataRow dRow in dt.Rows)
        {
            // PF Ledger
            command[i] = this.InsertPFLedger(strMonth, strYear, dRow["PSBID"].ToString().Trim(), dRow["EMPID"].ToString().Trim(), strFinYear,
                strInsBy, strInsDate, dRow["STATUS"].ToString().Trim(), lngLedgerID.ToString());
            lngLedgerID++;
            i++;
        }

        ////foreach (DataRow dRow in dt.Rows)
        ////{
        ////    // Update Provident Fund Balance
        ////    command[i] = this.InsertProvidentFundBF(strMonth, strYear,  dRow["EMPID"].ToString().Trim(), strFinYear,
        ////        strInsBy, strInsDate, dRow["STATUS"].ToString().Trim());
        ////    lngLedgerID++;
        ////    i++;
        ////}
        objDC.MakeTransaction(command);
    }

    protected SqlCommand InsertPFLedger(string strMonth, string strYear, string strPSBID, string strEMPID, string strFinYear,
        string strInsBy, string strInsDate, string strEmpStatus, string strLedgerID)
    {
        Payroll_PFManager objPFMgr = new Payroll_PFManager();
        Payroll_PayslipApprovalManager objPayAppMgr = new Payroll_PayslipApprovalManager();
        SqlCommand cmd;
        string strPFAmount = "0";
        string strPFAmountArr = "0";
        string strPFTOtal = "0";
        string strPFInt = "0";       
        string strTotalPayment = "0";
        string strCPDate = "";
        string strCPAmount = "0";
        string strNetBalance = "0";
        string strPrevMonth = "";
        string strPrevYear = strYear;
        
        strPrevMonth = Common.GetPreviousMonth(strMonth);
        if (strMonth == "1")
            strPrevYear = Convert.ToString(Convert.ToInt32(strYear) - 1);
        DataTable dtPFLedger = objPFMgr.GetPFLedgerData("0", strPrevMonth, strPrevYear, strEMPID);
        DataTable dtFinalPayment = this.GetFinalPaymentPFRecord(strMonth, strFinYear, strEMPID);
        if (strEmpStatus == "A")
        {
            // Current Month PF AMount
            DataTable dtPayroll = objPayAppMgr.GetPayrollApprovedDataForDisbursement("E", strEMPID, strMonth, strYear, strEMPID);

            foreach (DataRow dRow in dtPayroll.Rows)
            {
                if (dRow["SHEADID"].ToString().Trim() == "8")
                {
                    strPFAmount = Common.RoundDecimal(dRow["PAYAMT"].ToString().Trim(), 0).ToString();
                }
                if (dRow["SHEADID"].ToString().Trim() == "11")
                {
                    strPFAmountArr = Common.RoundDecimal(dRow["PAYAMT"].ToString().Trim(), 0).ToString();

                    if (Common.RoundDecimal(strPFAmountArr, 0) > 0)
                    {
                        strPFAmount = Convert.ToString((Convert.ToDecimal(strPFAmountArr) * -1) - Convert.ToDecimal(strPFAmount));
                        strPFTOtal = Convert.ToString(Convert.ToInt64(strPFAmount) * 2);
                    }
                    else
                    {
                        strPFAmount = Convert.ToString(Convert.ToDecimal(strPFAmount) + Convert.ToDecimal(strPFAmountArr));
                        strPFTOtal = Convert.ToString(Convert.ToInt64(strPFAmount) * 2);
                        strPFAmount = Convert.ToString(Math.Abs(Convert.ToInt64(strPFAmount)));
                        strPFTOtal = Convert.ToString(Math.Abs(Convert.ToInt64(strPFTOtal)));
                    }


                    strPFInt = "0";
                    //if (Common.RoundDecimal(strPFAmountArr, 0) > 0)
                    //{
                    //    strPFAmount = Convert.ToString(Math.Abs(Convert.ToInt64(strPFAmount)));
                    //    strPFTOtal = Convert.ToString(Math.Abs(Convert.ToInt64(strPFTOtal)));
                    //}
                    break;
                }

            }

        }
        // From Final Payment
        if (strEmpStatus == "I")
        {
            if (dtFinalPayment.Rows.Count == 0)
            {
                if (dtPFLedger.Rows.Count > 0)
                {
                    strPFAmount = "0";
                    strPFTOtal = "0";
                    strPFInt = "0";
                    strPFAmount = "0";
                    strPFTOtal = "0";
                    strCPDate = dtPFLedger.Rows[0]["CPDATE"].ToString().Trim();
                    if (string.IsNullOrEmpty(strCPDate) == false)
                    {
                        strCPDate = Common.SetDate(strCPDate);
                    }
                    strCPAmount = Common.RoundDecimal(dtPFLedger.Rows[0]["CPAMOUNT"].ToString().Trim(), 0).ToString();
                    strTotalPayment = Common.RoundDecimal(dtPFLedger.Rows[0]["TOTALPAY"].ToString().Trim(), 0).ToString();
                }
            }
            else
            {
                foreach (DataRow dRow in dtFinalPayment.Rows)
                {
                    decimal dclLastMonthPF = 0;
                    dclLastMonthPF = Common.RoundDecimal(dtFinalPayment.Rows[0]["LASTPFDEDUCT"].ToString().Trim(), 0);
                    //dclLastMonthPF = Math.Abs(dclLastMonthPF);// Omiited for Negative amount of LastPFDeduct

                    strPFAmount = Convert.ToString(dclLastMonthPF + Common.RoundDecimal(dtFinalPayment.Rows[0]["PFARREAR"].ToString().Trim(), 0));

                    strPFTOtal = Convert.ToString(Convert.ToInt64(strPFAmount) * 2);
                    strPFInt = Common.RoundDecimal(dtFinalPayment.Rows[0]["LASTPFINT"].ToString().Trim(), 0).ToString();
                    //strPFAmount = Convert.ToString(Convert.ToInt64(strPFAmount));
                    strPFTOtal = Convert.ToString(Convert.ToDecimal(strPFTOtal) + Convert.ToDecimal(strPFInt));
                    strCPDate = Common.SetDate(dtFinalPayment.Rows[0]["TRANSDATE"].ToString().Trim());
                    strCPAmount = Common.RoundDecimal(dtFinalPayment.Rows[0]["PAYAMT"].ToString().Trim(), 0).ToString();
                    strTotalPayment = Common.RoundDecimal(dtFinalPayment.Rows[0]["PAYAMT"].ToString().Trim(), 0).ToString();
                }
            }
        }


        if (dtPFLedger.Rows.Count > 0)
        {
            if (Convert.ToInt64(dtPFLedger.Rows[0]["NETBALANCE"].ToString().Trim()) == 0)
                strNetBalance = Convert.ToString(Convert.ToInt64(dtPFLedger.Rows[0]["NETBALANCE"].ToString().Trim()) + Convert.ToInt64(strPFTOtal));
            else
                strNetBalance = Convert.ToString(Convert.ToInt64(dtPFLedger.Rows[0]["NETBALANCE"].ToString().Trim()) + Convert.ToInt64(strPFTOtal) - Convert.ToInt64(strTotalPayment));
            cmd = new SqlCommand();
            cmd = objPFMgr.GetCommandOfInsertPFLedger(strLedgerID,
                                                                strEMPID,
                                                                strMonth,
                                                                strYear,
                                                                strFinYear,
                // Opening Balance
                                                                dtPFLedger.Rows[0]["OPPFOWN"].ToString().Trim(),
                                                                dtPFLedger.Rows[0]["OPPFCARE"].ToString().Trim(),
                                                                dtPFLedger.Rows[0]["OPPFINTREST"].ToString().Trim(),
                                                                dtPFLedger.Rows[0]["OPTOTAL"].ToString().Trim(),
                // Current Month Credit
                                                                "0",
                                                                "0",
                                                                "0",
                                                                "0",
                // Current Payment
                                                                strCPDate, strCPAmount,
                // Total Payment
                                                                strTotalPayment,
                // Net balance
                                                                strNetBalance,
                                                                strInsBy, strInsDate, "N");
            return cmd;

        }
        // Preveious Month PF Ledger Not Exist
        else
        {
            strPFTOtal = Convert.ToString(Convert.ToInt64(strPFAmount) * 2);
            
            //strPFAmount = Convert.ToString(Convert.ToInt64(strPFAmount));
            strPFTOtal = Convert.ToString(Convert.ToDecimal(strPFTOtal) + Convert.ToDecimal(strPFInt));
            strNetBalance = Convert.ToString(Convert.ToInt64(strPFTOtal) - Convert.ToInt64(strTotalPayment));
            cmd = new SqlCommand();
            cmd = objPFMgr.GetCommandOfInsertPFLedger(strLedgerID,
                                                                strEMPID,
                                                                strMonth,
                                                                strYear,
                                                                strFinYear,
                // Opening Balance
                                                                strPFAmount,
                                                                strPFAmount,
                                                                strPFInt,
                                                                strPFTOtal, 
                // Current Month Credit
                                                                "0",
                                                                "0",
                                                                "0",
                                                                "0",
                // Current Payment
                                                                strCPDate, strCPAmount,
                // Total Payment
                                                                strTotalPayment,
                // Net balance
                                                                strNetBalance,
                                                                strInsBy, strInsDate, "N");
            return cmd;
        }
        return null;
    }

    protected SqlCommand InsertProvidentFundBF(string strMonth, string strYear,  string strEMPID, string strFinYear,
        string strInsBy, string strInsDate, string strEmpStatus)
    {
        Payroll_PFManager objPFMgr = new Payroll_PFManager();
        Payroll_PayslipApprovalManager objPayAppMgr = new Payroll_PayslipApprovalManager();
        SqlCommand cmd;
        string strPFAmount = "0";
        string strPFAmountArr = "0";     
        string strEmpContri = "0";
        string strCompContri = "0";
        string strTotalContri = "0";
        string strPrevMonth = "";
        string strPrevYear = strYear;
        string strPFBFId = "";

        strPrevMonth = Common.GetPreviousMonth(strMonth);
        if (strMonth == "1")
            strPrevYear = Convert.ToString(Convert.ToInt32(strYear) - 1);
        DataTable dtPFBF = objPFMgr.GetProvidentFundBF(strFinYear, strEMPID);
        strPFBFId = Common.getMaxId("ProvidentFundBF", "PFBFId"); 
        if (strEmpStatus == "A")
        {
            // Current Month PF AMount
            DataTable dtPayroll = objPayAppMgr.GetPayrollApprovedDataForDisbursement("E", strEMPID, strMonth, strYear, strEMPID);
            foreach (DataRow dRow in dtPayroll.Rows)
            {
                if (dRow["SHEADID"].ToString().Trim() == "8")
                {
                    strPFAmount = Common.RoundDecimal(dRow["PAYAMT"].ToString().Trim(), 0).ToString();
                }
                if (dRow["SHEADID"].ToString().Trim() == "11")
                {
                    strPFAmountArr = Common.RoundDecimal(dRow["PAYAMT"].ToString().Trim(), 0).ToString();
                    break;
                }
            }
        }

        if (Convert.ToDecimal(strPFAmount) != 0)
        {
            if (dtPFBF.Rows.Count > 0)
            {
                strEmpContri = Convert.ToString(Convert.ToDecimal(dtPFBF.Rows[0]["EmpContribution"].ToString().Trim()) + (-Convert.ToDecimal(strPFAmount)));
                strCompContri = Convert.ToString(Convert.ToDecimal(dtPFBF.Rows[0]["CompContribution"].ToString().Trim()) + (-Convert.ToDecimal(strPFAmount)));
                strTotalContri = Convert.ToString(Convert.ToDecimal(dtPFBF.Rows[0]["TotalContribution"].ToString().Trim()) + (-Convert.ToDecimal(strPFAmount) * 2));

                cmd = new SqlCommand();
                cmd = objPFMgr.GetCommandOfInsertProvidentFundBF(dtPFBF.Rows[0]["PFBFId"].ToString().Trim(), strEMPID, strFinYear, strEmpContri, strCompContri, strTotalContri, strInsBy, strInsDate, "Y");
                return cmd;
            }
            // Preveious Month PF Ledger Not Exist
            else
            {
                
                strEmpContri = Convert.ToString(-Convert.ToDecimal(strPFAmount));
                strCompContri = Convert.ToString(-Convert.ToDecimal(strPFAmount));
                strTotalContri = Convert.ToString(-Convert.ToDecimal(strPFAmount) * 2);

                cmd = new SqlCommand();
                cmd = objPFMgr.GetCommandOfInsertProvidentFundBF(strPFBFId, strEMPID, strFinYear, strEmpContri, strCompContri, strTotalContri, strInsBy, strInsDate, "N");
                strPFBFId = Convert.ToString(Convert.ToInt32(strPFBFId) + 1);
                return cmd;
            }
        }
        return null;
    }

    protected SqlCommand UpdateProvidentFundBF(string strMonth, string strYear, string strEMPID, string strPFAmount, string strFinYear,
        string strInsBy, string strInsDate)
    {
        Payroll_PFManager objPFMgr = new Payroll_PFManager();
       
        SqlCommand cmd;
       
        string strPFAmountArr = "0";
        string strEmpContri = "0";
        string strCompContri = "0";
        string strTotalContri = "0";
        string strPrevMonth = "";
        string strPrevYear = strYear;

        strPrevMonth = Common.GetPreviousMonth(strMonth);
        if (strMonth == "1")
            strPrevYear = Convert.ToString(Convert.ToInt32(strYear) - 1);
        DataTable dtPFBF = objPFMgr.GetProvidentFundBF(strFinYear, strEMPID);       

        if (dtPFBF.Rows.Count > 0)
        {
            strEmpContri = Convert.ToString(Convert.ToDecimal(dtPFBF.Rows[0]["EmpContribution"].ToString().Trim()) -Convert.ToDecimal(strPFAmount));
            strCompContri = Convert.ToString(Convert.ToDecimal(dtPFBF.Rows[0]["CompContribution"].ToString().Trim()) - Convert.ToDecimal(strPFAmount));
            strTotalContri = Convert.ToString(Convert.ToDecimal(dtPFBF.Rows[0]["TotalContribution"].ToString().Trim()) - Convert.ToDecimal(strPFAmount) * 2);

            cmd = new SqlCommand();
            cmd = objPFMgr.GetCommandOfInsertProvidentFundBF(dtPFBF.Rows[0]["PFBFId"].ToString().Trim(), strEMPID, strFinYear, strEmpContri, strCompContri, strTotalContri, strInsBy, strInsDate, "Y");
            return cmd;
        }       
        return null;
    }

    public void DeleteLedgerData(string strMonth, string strFinYear, string strLoanType, string strLedgerType)
    {
        string strSQL = "";
        if (strLoanType == "PF")
        {
            if (strLedgerType == "LOAN")
            {
                strSQL = "DELETE FROM PFLOANLEDGER WHERE VMONTH=@VMONTH AND FISCALYRID=@FISCALYRID";
            }
            else
            {
                strSQL = "DELETE FROM PFLEDGER WHERE VMONTH=@VMONTH AND FISCALYRID=@FISCALYRID";
            }
        }        
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        objDC.ExecuteQuery(cmd);
    }

    public void DeletePFLedgerData(DataTable dtPFLedger, string strMonth,string strYear, string strFinYear, string strLoanType, string strLedgerType, string strInsBy, string strInsDate)
    {
        SqlCommand[] cmd = new SqlCommand[dtPFLedger.Rows.Count + 1];
        int i = 0;
        string strSQL = "";

        strSQL = "DELETE FROM PFLEDGER WHERE VMONTH=@VMONTH AND FISCALYRID=@FISCALYRID";

        cmd[0] = new SqlCommand(strSQL);
        cmd[0].CommandType = CommandType.Text;

        SqlParameter p_VMONTH = cmd[0].Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID = cmd[0].Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        i++;
        ////foreach (DataRow dRow in dtPFLedger.Rows)
        ////{
        ////    // Update Provident Fund Balance
        ////    cmd[i] = this.UpdateProvidentFundBF(strMonth, strYear, dRow["EMPID"].ToString().Trim(), dRow["OPPFOWN"].ToString().Trim(), strFinYear, strInsBy, strInsDate);
            
        ////    i++;
        ////}
        objDC.MakeTransaction(cmd);
    }

    public bool IsCurrentMonthLedgerExist(string strMonth, string strFinYear, string strLoanType, string strLedgerType)
    {
        string strSQL = "";
        string strRetText = "";
        if (strLoanType == "PF")
        {
            if (strLedgerType == "LOAN")
            {
                strSQL = "SELECT VMONTH FROM PFLOANLEDGER WHERE VMONTH=@VMONTH AND FISCALYRID=@FISCALYRID";
            }
            else
            {
                strSQL = "SELECT VMONTH FROM PFLEDGER WHERE VMONTH=@VMONTH AND FISCALYRID=@FISCALYRID";
            }
        }
        else
        {
            if (strLedgerType == "LOAN")
            {
                strSQL = "SELECT VMONTH FROM CULOANLEDGER WHERE VMONTH=@VMONTH AND FISCALYRID=@FISCALYRID";
            }
            else
            {
                strSQL = "SELECT VMONTH FROM CULEDGER WHERE VMONTH=@VMONTH AND FISCALYRID=@FISCALYRID";
            }
        }

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        strRetText = objDC.GetScalarVal(cmd);

        if (string.IsNullOrEmpty(strRetText) == false)
            return true;
        else
            return false;
    }

    public DataTable GetPFLedger(string strMonth, string strYear)
    {
        string strSQL = "SELECT * FROM PFLedger WHERE VMONTH=@VMONTH AND VYEAR=@VYEAR";

        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;
          
        SqlParameter p_VMONTH = command.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = command.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        return objDC.CreateDT(command, "GetPFLedger");
    }
    public void DeletePFLoanData(string strTransID)
    {
        string strSQL = "DELETE FROM EmpPFLoanMst WHERE TransId=" + strTransID;
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_TRANSID = cmd.Parameters.Add("TRANSID", SqlDbType.BigInt);
        p_TRANSID.Direction = ParameterDirection.Input;
        p_TRANSID.Value = strTransID;

        objDC.ExecuteQuery(cmd);
    }


    public void DeletePFLoanAdjustedData(string strTransID)
    {
        string strSQL = "DELETE FROM PFLOANADJUSTMENT WHERE TransId=" + strTransID;
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_TRANSID = cmd.Parameters.Add("TRANSID", SqlDbType.BigInt);
        p_TRANSID.Direction = ParameterDirection.Input;
        p_TRANSID.Value = strTransID;

        objDC.ExecuteQuery(cmd);
    }

    #endregion

    #region Gratuity Ledger

    public bool IsCurrentMonthGratuityLedgerExist(string strMonth, string strFinYear)
    {
        string strSQL = "";
        string strRetText = "";
        strSQL = "SELECT VMONTH FROM GratuityLedger WHERE VMONTH=@VMONTH AND FISCALYRID=@FISCALYRID";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        strRetText = objDC.GetScalarVal(cmd);

        if (string.IsNullOrEmpty(strRetText) == false)
            return true;
        else
            return false;
    }

    public void PrepareGratuityLedgerData(DataTable dtEmpPayroll, DataTable dtEmpGratuityData, string strMonth, string strYear, string strStatus, string strInsBy, string strInsDate,
       string strSalType, string strFinYear)
    {
        int i = 0;
        SqlCommand[] command = new SqlCommand[dtEmpPayroll.Rows.Count];
        long lngLedgerID = Convert.ToInt64(Common.getMaxId("GratuityLedger", "LEDGERID"));
        string strPFAmt = "0";

        if (dtEmpGratuityData.Rows.Count > 0)
        {
            foreach (DataRow dRow in dtEmpPayroll.Rows)
            {
                DataRow[] fRows = dtEmpGratuityData.Select("EMPID= '" + dRow["EMPID"].ToString().Trim() + "'");
                if (fRows.Length > 0)
                    strPFAmt = Convert.ToString(Math.Abs(Common.RoundDecimal(fRows[0]["PAYAMT"].ToString().Trim(), 0)));
                else
                    strPFAmt = "0";

                //PF Ledger
                command[i] = this.InsertGratuityLedger(strMonth, strYear, dRow["PSBID"].ToString().Trim(), dRow["EMPID"].ToString().Trim(), strFinYear,
                    strInsBy, strInsDate, dRow["STATUS"].ToString().Trim(), lngLedgerID.ToString(), strPFAmt);

                i++;

                lngLedgerID++;
                //// CU Ledger
                //command[i] = this.InsertCULedger(strMonth, strYear, dRow["PSBID"].ToString().Trim(), dRow["EMPID"].ToString().Trim(), strFinYear,
                //    strInsBy, strInsDate);
                //i++;
            }
        }
        objDC.MakeTransaction(command);
    }

    protected SqlCommand InsertGratuityLedger(string strMonth, string strYear, string strPSBID, string strEMPID, string strFinYear,
        string strInsBy, string strInsDate, string strEmpStatus, string strLedgerID, string strPFAmt)
    {
        Payroll_PFManager objPFMgr = new Payroll_PFManager();
        Payroll_PayslipApprovalManager objPayAppMgr = new Payroll_PayslipApprovalManager();
        SqlCommand cmd;
        string strGRAmount = strPFAmt;
        string strPFAmountArr = "0";
        string strPFTOtal = "0";
        string strPFInt = "0";
        string strCUPFAmount = "0";
        string strCUPFTotal = "0";
        string strCUPFInt = "0";
        string strTotalPayment = "0";
        string strCPDate = "";
        string strCPAmount = "0";
        string strNetBalance = "0";
        string strPrevMonth = "";
        string strPrevYear = strYear;

        strPrevMonth = Common.GetPreviousMonth(strMonth);
        if (strMonth == "1")
            strPrevYear = Convert.ToString(Convert.ToInt32(strYear) - 1);

        DataTable dtGRLedger = objPFMgr.GetGratuityLedgerData("0", strPrevMonth, strPrevYear, strEMPID);
        DataTable dtFinalPayment = this.GetFinalPaymentPFRecord(strMonth, strFinYear, strEMPID);

        #region
        //if (strEmpStatus == "A")
        //{
        //     //Current Month PF AMount
        //   // DataTable dtPayroll = objPayAppMgr.GetPFRecordFromSalary("E", strEMPID, strMonth, strYear, "", "S", "A");

        //    foreach (DataRow dRow in dtPayroll.Rows)
        //    {
        //        if (dRow["SHEADID"].ToString().Trim() == "8")
        //        {
        //            strPFAmount = Common.RoundDecimal(dRow["PAYAMT"].ToString().Trim(), 0).ToString();
        //        }
        //        if (dRow["SHEADID"].ToString().Trim() == "24")
        //        {
        //            strPFAmountArr = Common.RoundDecimal(dRow["PAYAMT"].ToString().Trim(), 0).ToString();

        //            if (Common.RoundDecimal(strPFAmountArr, 0) > 0)
        //            {
        //                strPFAmount = Convert.ToString((Convert.ToDecimal(strPFAmountArr) * -1) - Convert.ToDecimal(strPFAmount));
        //                strPFTOtal = Convert.ToString(Convert.ToInt64(strPFAmount) * 2);
        //            }
        //            else
        //            {

        //            }


        //            strPFInt = "0";
        //            //if (Common.RoundDecimal(strPFAmountArr, 0) > 0)
        //            //{
        //            //    strPFAmount = Convert.ToString(Math.Abs(Convert.ToInt64(strPFAmount)));
        //            //    strPFTOtal = Convert.ToString(Math.Abs(Convert.ToInt64(strPFTOtal)));
        //            //}
        //            break;
        //        }

        //    }

        //}
        #endregion

        strGRAmount = Convert.ToString(Convert.ToDecimal(strPFAmt));
        strGRAmount = Convert.ToString(Common.RoundDecimal5T1(strGRAmount, 0));
        strPFTOtal = strPFAmt;
        strGRAmount = Convert.ToString(Math.Abs(Convert.ToInt64(strGRAmount)));
        strPFTOtal = Convert.ToString(Math.Abs(Convert.ToInt64(strPFTOtal)));

        // From Final Payment
        if (strEmpStatus == "A")
        {
            if (dtFinalPayment.Rows.Count == 0)
            {
                if (dtGRLedger.Rows.Count > 0)
                {
                    //strPFAmount = Convert.ToString(Convert.ToDecimal(strPFAmt) / 2);
                    //strPFTOtal = strPFAmt;
                    strPFInt = "0";

                    strCPDate = dtGRLedger.Rows[0]["CPDATE"].ToString().Trim();
                    if (string.IsNullOrEmpty(strCPDate) == false)
                    {
                        strCPDate = Common.SetDate(strCPDate);
                    }
                    strCPAmount = Common.RoundDecimal(dtGRLedger.Rows[0]["CPAMOUNT"].ToString().Trim(), 0).ToString();
                    strTotalPayment = Common.RoundDecimal(dtGRLedger.Rows[0]["TOTALPAY"].ToString().Trim(), 0).ToString();
                }
            }
            else
            {
                foreach (DataRow dRow in dtFinalPayment.Rows)
                {
                    decimal dclLastMonthPF = 0;
                    dclLastMonthPF = Common.RoundDecimal(dtFinalPayment.Rows[0]["LASTPFDEDUCT"].ToString().Trim(), 0);
                    //dclLastMonthPF = Math.Abs(dclLastMonthPF);// Omiited for Negative amount of LastPFDeduct

                    strGRAmount = Convert.ToString(dclLastMonthPF + Common.RoundDecimal(dtFinalPayment.Rows[0]["PFARREAR"].ToString().Trim(), 0));

                    strPFTOtal = Convert.ToString(Convert.ToInt64(strGRAmount) * 2);
                    strPFInt = Common.RoundDecimal(dtFinalPayment.Rows[0]["LASTPFINT"].ToString().Trim(), 0).ToString();
                    //strPFAmount = Convert.ToString(Convert.ToInt64(strPFAmount));
                    strPFTOtal = Convert.ToString(Convert.ToDecimal(strPFTOtal) + Convert.ToDecimal(strPFInt));
                    strCPDate = Common.SetDate(dtFinalPayment.Rows[0]["TRANSDATE"].ToString().Trim());
                    strCPAmount = Common.RoundDecimal(dtFinalPayment.Rows[0]["PAYAMT"].ToString().Trim(), 0).ToString();
                    strTotalPayment = Common.RoundDecimal(dtFinalPayment.Rows[0]["PAYAMT"].ToString().Trim(), 0).ToString();
                }
            }
        }

        if (dtGRLedger.Rows.Count > 0)
        {
            strCUPFAmount = Convert.ToString(Convert.ToInt64(dtGRLedger.Rows[0]["CUGR"].ToString().Trim()) + Convert.ToInt64(strGRAmount));
            strCUPFInt = Convert.ToString(Convert.ToInt64(dtGRLedger.Rows[0]["CUGRINTREST"].ToString().Trim()) + Convert.ToInt64(strPFInt));
            strCUPFTotal = Convert.ToString(Convert.ToInt64(dtGRLedger.Rows[0]["CUTOTAL"].ToString().Trim()) + Convert.ToInt64(strPFTOtal));
            if (Convert.ToInt64(dtGRLedger.Rows[0]["NETBALANCE"].ToString().Trim()) == 0)
                strNetBalance = Convert.ToString(Convert.ToInt64(dtGRLedger.Rows[0]["NETBALANCE"].ToString().Trim()) + Convert.ToInt64(strPFTOtal));
            else
                strNetBalance = Convert.ToString(Convert.ToInt64(dtGRLedger.Rows[0]["NETBALANCE"].ToString().Trim()) + Convert.ToInt64(strPFTOtal) - Convert.ToInt64(strTotalPayment));
            cmd = new SqlCommand();
            cmd = objPFMgr.GetCommandOfInsertGratuityLedger(strLedgerID,
                                                                strEMPID,
                                                                strMonth,
                                                                strYear,
                                                                strFinYear,             
                                                                // Opening Balance
                                                                dtGRLedger.Rows[0]["CUGR"].ToString().Trim(),
                                                                dtGRLedger.Rows[0]["CUGRINTREST"].ToString().Trim(),
                                                                dtGRLedger.Rows[0]["CUTOTAL"].ToString().Trim(),                
                // Current Month Credit
                                                                strGRAmount,
                                                                strPFInt,
                                                                strPFTOtal,
                // Current Payment
                                                                strCPDate, strCPAmount,
                // Cummulative Balance
                                                                strCUPFAmount,
                                                                strCUPFInt,
                                                                strCUPFTotal,
                // Total Payment
                                                                strTotalPayment,
                // Net balance
                                                                strNetBalance,
                                                                strInsBy, strInsDate, "N");
            return cmd;

        }
        // Preveious Month PF Ledger Not Exist
        else
        {
            strCUPFAmount = Convert.ToString(Convert.ToInt64(strGRAmount));
            strCUPFInt = Convert.ToString(Convert.ToInt64(strPFInt));
            strCUPFTotal = Convert.ToString(Convert.ToInt64(strPFTOtal));
            strNetBalance = Convert.ToString(Convert.ToInt64(strPFTOtal) - Convert.ToInt64(strTotalPayment));
            cmd = new SqlCommand();
            cmd = objPFMgr.GetCommandOfInsertGratuityLedger(strLedgerID,
                                                                strEMPID,
                                                                strMonth,
                                                                strYear,
                                                                strFinYear,
                // Opening Balance
                                                                "0",
                                                                "0",
                                                                "0",
                // Current Month Credit
                                                                strGRAmount,
                                                                strPFInt,
                                                                strPFTOtal,
                // Current Payment
                                                                strCPDate, strCPAmount,
                // Cummulative Balance
                                                                strCUPFAmount,
                                                                strCUPFInt,
                                                                strCUPFTotal,
                // Total Payment
                                                                strTotalPayment,
                // Net balance
                                                                strNetBalance,
                                                                strInsBy, strInsDate, "N");
            return cmd;
        }
        return null;
    }

    public void DeleteGratuityLedgerData(string strMonth, string strFinYear)
    {
        string strSQL = "";
        strSQL = "DELETE FROM GratuityLedger WHERE VMONTH=@VMONTH AND FISCALYRID=@FISCALYRID";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        objDC.ExecuteQuery(cmd);
    }

    #endregion
}
