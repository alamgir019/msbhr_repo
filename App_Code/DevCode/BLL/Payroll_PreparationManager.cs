using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Payroll_PreparationManager
/// </summary>
public class Payroll_PreparationManager
{
    DBConnector objDC = new DBConnector();
    Payroll_MasterMgr objPayMstMgr = new Payroll_MasterMgr();

    #region Insert/Update/Delete
    public void InsertPSBData(GridView gr, DataTable dtPaySlipDetls, string strInsBy, string strInsDate, string strIsUpdate,
        string strStartDate, string strEndDate, string strPrepDate, string strMonth, string strYear, string strFiscalYear,
        string strSalFor, string strEmpGrpID, string strTaxPercent, string strMedFiscalYrId, string strPFFiscalYrId, string strTaxFiscalYrId)
    {
        DataTable dtGrossHead = objPayMstMgr.SelectGrossSalHead(0);

        SqlCommand[] command = new SqlCommand[gr.Rows.Count + dtPaySlipDetls.Rows.Count + 3 + gr.Rows.Count];
        command[0] = new SqlCommand("proc_Payroll_Insert_PaySlipBook");
        command[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_PSBID = command[0].Parameters.Add("PSBID", SqlDbType.BigInt);
        p_PSBID.Direction = ParameterDirection.Input;
        p_PSBID.Value = gr.DataKeys[0].Values[0].ToString();

        SqlParameter p_PSBDATECREATED = command[0].Parameters.Add("PSBDATECREATED", SqlDbType.DateTime);
        p_PSBDATECREATED.Direction = ParameterDirection.Input;
        p_PSBDATECREATED.Value = strPrepDate;

        SqlParameter p_DATESTART = command[0].Parameters.Add("DATESTART", SqlDbType.DateTime);
        p_DATESTART.Direction = ParameterDirection.Input;
        p_DATESTART.Value = strStartDate;

        SqlParameter p_DATEEND = command[0].Parameters.Add("DATEEND", SqlDbType.DateTime);
        p_DATEEND.Direction = ParameterDirection.Input;
        p_DATEEND.Value = strEndDate;

        SqlParameter p_TOTALSLIP = command[0].Parameters.Add("TOTALSLIP", SqlDbType.BigInt);
        p_TOTALSLIP.Direction = ParameterDirection.Input;
        p_TOTALSLIP.Value = gr.Rows.Count;

        SqlParameter p_PREPAREDBY = command[0].Parameters.Add("PREPAREDBY", SqlDbType.VarChar);
        p_PREPAREDBY.Direction = ParameterDirection.Input;
        p_PREPAREDBY.Value = strInsBy;

        SqlParameter p_INSERTEDBY = command[0].Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = strInsBy;

        SqlParameter p_INSERTEDDATE = command[0].Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDDATE.Direction = ParameterDirection.Input;
        p_INSERTEDDATE.Value = strInsDate;

        SqlParameter p_ISROUND5 = command[0].Parameters.Add("ISROUND5", SqlDbType.Char);
        p_ISROUND5.Direction = ParameterDirection.Input;
        p_ISROUND5.Value = 'N';

        SqlParameter p_ISUPDATE = command[0].Parameters.Add("ISUPDATE", SqlDbType.Char);
        p_ISUPDATE.Direction = ParameterDirection.Input;
        p_ISUPDATE.Value = strIsUpdate;

        int i = 1;

        //Delete PaySlipMst
        string strSQLPSM = "DELETE FROM PAYSLIPMST WHERE PSBID=@PSBID";
        command[i] = new SqlCommand(strSQLPSM);
        command[i].CommandType = CommandType.Text;
        SqlParameter p_PSMBID = command[i].Parameters.Add("PSBID", SqlDbType.BigInt);
        p_PSMBID.Direction = ParameterDirection.Input;
        p_PSMBID.Value = gr.DataKeys[0].Values[0].ToString();
        i++;

        //Delete PaySlipDet
        string strSQLPSD = "DELETE FROM PAYSLIPDETS WHERE PSBID=@PSBID";
        command[i] = new SqlCommand(strSQLPSM);
        command[i].CommandType = CommandType.Text;
        SqlParameter p_PSDBID = command[i].Parameters.Add("PSBID", SqlDbType.BigInt);
        p_PSDBID.Direction = ParameterDirection.Input;
        p_PSDBID.Value = gr.DataKeys[0].Values[0].ToString();
        i++;

        // Insert PaySlipMst Data
        decimal dclNetPay = 0;
        decimal dclGrossPay = 0;
        foreach (GridViewRow gRow in gr.Rows)
        {
            DataRow[] foundRows = dtPaySlipDetls.Select("EmployeeID='" + gRow.Cells[1].Text.Trim() + "'");
            dclNetPay = 0;
            dclGrossPay = 0;
            foreach (DataRow dRow in foundRows)
            {
                dclNetPay = dclNetPay + Convert.ToDecimal(dRow["PayAmnt"].ToString());
                //if(gRow.Cells[15].Text.Trim()=="Y")
                if (Common.FindInDataTable(dtGrossHead, "SHEADID", dRow["SalHeadID"].ToString().Trim(), "SHEADID") == dRow["SalHeadID"].ToString().Trim())
                    dclGrossPay = dclGrossPay + Convert.ToDecimal(dRow["PayAmnt"].ToString());
            }
            string str;
            if (i == 2264)
                str = "";
            command[i] = this.InsertPaySlipMasterData(gr.DataKeys[gRow.DataItemIndex].Values[0].ToString(),
                                                      gr.DataKeys[gRow.DataItemIndex].Values[1].ToString(),
                                                      gRow.Cells[1].Text.Trim(),
                                                      gRow.Cells[5].Text.Trim(), gRow.Cells[6].Text.Trim(),
                                                      dclNetPay.ToString(),// need to get the Net pay
                                                      gRow.Cells[12].Text.Trim(),
                                                      gr.DataKeys[gRow.DataItemIndex].Values[15].ToString(),
                                                      strSalFor,// Is Only Bonus
                                                      gRow.Cells[15].Text.Trim(), // Is With Bonus
                                                      gRow.Cells[13].Text.Trim(),
                                                      gRow.Cells[8].Text.Trim(),
                                                      dclGrossPay.ToString(),
                                                      gRow.Cells[11].Text.Trim(),
                                                      gr.DataKeys[gRow.DataItemIndex].Values[10].ToString(),
                                                      gr.DataKeys[gRow.DataItemIndex].Values[11].ToString(),
                                                      gr.DataKeys[gRow.DataItemIndex].Values[12].ToString(),
                                                      strInsBy, strInsDate, strPrepDate,                                                      
                                                      gr.DataKeys[gRow.DataItemIndex].Values[17].ToString(),// BankCode
                                                      gr.DataKeys[gRow.DataItemIndex].Values[18].ToString(),// BranchCode
                                                      gr.DataKeys[gRow.DataItemIndex].Values[19].ToString(),// Bank Acc No
                                                      gr.DataKeys[gRow.DataItemIndex].Values[20].ToString(),// DeptID
                                                      gr.DataKeys[gRow.DataItemIndex].Values[21].ToString(),// DesgId
                                                      gr.DataKeys[gRow.DataItemIndex].Values[22].ToString(),
                                                      gr.DataKeys[gRow.DataItemIndex].Values[23].ToString(),// Plan Accline
                                                      gr.DataKeys[gRow.DataItemIndex].Values[25].ToString(),// ClinicId  
                                                      gr.DataKeys[gRow.DataItemIndex].Values[24].ToString(),// ClinicId                                                      
                                                      strMonth, strYear, strFiscalYear,// Month, year, fiscal year
                                                      strEmpGrpID, strTaxPercent, strPFFiscalYrId, strTaxFiscalYrId);
            i++;

            if (strSalFor == "B")
            {
                command[i] = this.UpdateBonusAllowance(gRow.Cells[1].Text.Trim(), strMonth, strYear, strInsBy, strInsDate);
                i++;
            }
            else if (gRow.Cells[15].Text.Trim() == "Y")
            {
                command[i] = this.UpdateBonusAllowance(gRow.Cells[1].Text.Trim(), strMonth, strYear, strInsBy, strInsDate);
                i++;
            }
            // Insert PaySlip Details Data
            foreach (DataRow dRow in foundRows)
            {
                command[i] = this.InsertPaySlipDetails(gr.DataKeys[gRow.DataItemIndex].Values[0].ToString(),
                                                     gRow.Cells[1].Text.Trim(),
                                                     dRow["SalHeadID"].ToString(),
                                                     dRow["PayAmnt"].ToString(),
                                                     dRow["IsDeducted"].ToString(),
                                                     this.getHeadType(dRow),
                                                     dRow["PFAmount"].ToString(),
                                                     strInsBy, strInsDate);
                i++;
            }
        }

        objDC.MakeTransaction(command);
    }

    protected SqlCommand InsertPaySlipMasterData(string strPSBId,string strPayID,string strEmpId,string strPayDurStart,string strPayDurEnd, 
        string strNetPay,string strPaySlipStatus,string strSalPackId,string strSalaryType,string strIsWithBonus,string strPayType,
        string strTWorkDayHour,string strGrossAmt,string strIsIrregular,string strIsCurrConv,string strCurrId, string strCurrConvAmt,
        string strInsBy, string strInsDate, string strPrepDate, string strBankCode, string strBranchCode, string strAccNo,
        string strDeptId, string strDesgId, string strEmpTypeId,string strAccline, string strDivId,string strClinicId,string strMonth,
        string strYear, string strFiscalYear, string strEmpGrpID, string strTaxPercent, string strPFFiscalYrId,string strTaxFiscalYrId)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Insert_PaySlipMst");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_PSBID = cmd.Parameters.Add("PSBID", SqlDbType.BigInt);
        p_PSBID.Direction = ParameterDirection.Input;
        p_PSBID.Value = strPSBId;

        SqlParameter p_PAYID = cmd.Parameters.Add("PAYID", SqlDbType.BigInt);
        p_PAYID.Direction = ParameterDirection.Input;
        p_PAYID.Value = strPayID;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpId;
        
        SqlParameter p_PAYDURSTART = cmd.Parameters.Add("PAYDURSTART", SqlDbType.DateTime);
        p_PAYDURSTART.Direction = ParameterDirection.Input;
        p_PAYDURSTART.Value = strPayDurStart;

        SqlParameter p_PAYDUREND= cmd.Parameters.Add("PAYDUREND", SqlDbType.DateTime);
        p_PAYDUREND.Direction = ParameterDirection.Input;
        p_PAYDUREND.Value = strPayDurEnd;

        SqlParameter p_NETPAY = cmd.Parameters.Add("NETPAY", SqlDbType.Decimal);
        p_NETPAY.Direction = ParameterDirection.Input;
        p_NETPAY.Value = strNetPay;

        SqlParameter p_PAYSLIPSTATUS = cmd.Parameters.Add("PAYSLIPSTATUS", SqlDbType.Char);
        p_PAYSLIPSTATUS.Direction = ParameterDirection.Input;
        p_PAYSLIPSTATUS.Value = strPaySlipStatus;

        SqlParameter p_SALPAKID = cmd.Parameters.Add("SALPAKID", SqlDbType.BigInt);
        p_SALPAKID.Direction = ParameterDirection.Input;
        p_SALPAKID.Value = strSalPackId;

        SqlParameter p_SALARYTYPE = cmd.Parameters.Add("SALARYTYPE", SqlDbType.Char);
        p_SALARYTYPE.Direction = ParameterDirection.Input;
        p_SALARYTYPE.Value = strSalaryType;

        SqlParameter p_ISWITHBONUS = cmd.Parameters.Add("ISWITHBONUS", SqlDbType.Char);
        p_ISWITHBONUS.Direction = ParameterDirection.Input;
        p_ISWITHBONUS.Value = strIsWithBonus;

        SqlParameter p_PAYTYPE = cmd.Parameters.Add("PAYTYPE", SqlDbType.BigInt);
        p_PAYTYPE.Direction = ParameterDirection.Input;
        p_PAYTYPE.Value = strPayType;

        SqlParameter p_TWORKINGDAYHOUR = cmd.Parameters.Add("TWORKINGDAYHOUR", SqlDbType.Decimal);
        p_TWORKINGDAYHOUR.Direction = ParameterDirection.Input;
        p_TWORKINGDAYHOUR.Value = strTWorkDayHour;

        SqlParameter p_GROSSAMNT = cmd.Parameters.Add("GROSSAMNT", SqlDbType.Decimal);
        p_GROSSAMNT.Direction = ParameterDirection.Input;
        p_GROSSAMNT.Value = strGrossAmt;

        SqlParameter p_ISIRREGULAR = cmd.Parameters.Add("ISIRREGULAR", SqlDbType.Char);
        p_ISIRREGULAR.Direction = ParameterDirection.Input;
        p_ISIRREGULAR.Value = strIsIrregular;

        SqlParameter p_ISCURRENCYCONV = cmd.Parameters.Add("ISCURRENCYCONV", SqlDbType.Char);
        p_ISCURRENCYCONV.Direction = ParameterDirection.Input;
        p_ISCURRENCYCONV.Value = strIsCurrConv;

        SqlParameter p_CURNCID = cmd.Parameters.Add("CURNCID", SqlDbType.BigInt);
        p_CURNCID.Direction = ParameterDirection.Input;
        p_CURNCID.Value = strCurrId;

        SqlParameter p_CURRCONVAMNT = cmd.Parameters.Add("CURRCONVAMNT", SqlDbType.Decimal);
        p_CURRCONVAMNT.Direction = ParameterDirection.Input;
        p_CURRCONVAMNT.Value = strCurrConvAmt;

        SqlParameter p_PREPAREDBY = cmd.Parameters.Add("PREPAREDBY", SqlDbType.VarChar);
        p_PREPAREDBY.Direction = ParameterDirection.Input;
        p_PREPAREDBY.Value = strInsBy;

        SqlParameter p_PREPARINGDATE = cmd.Parameters.Add("PREPARINGDATE", SqlDbType.DateTime);
        p_PREPARINGDATE.Direction = ParameterDirection.Input;
        p_PREPARINGDATE.Value = strInsDate;

        SqlParameter p_INSERTEDBY = cmd.Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = strInsBy;

        SqlParameter p_INSERTEDDATE = cmd.Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDDATE.Direction = ParameterDirection.Input;
        p_INSERTEDDATE.Value = strInsDate;

        SqlParameter p_BANKCODE = cmd.Parameters.Add("BANKCODE", SqlDbType.Char);
        p_BANKCODE.Direction = ParameterDirection.Input;
        p_BANKCODE.Value = strBankCode;

        SqlParameter p_BRANCHCODE = cmd.Parameters.Add("RoutingNo", SqlDbType.Char);
        p_BRANCHCODE.Direction = ParameterDirection.Input;
        p_BRANCHCODE.Value = strBranchCode;

        SqlParameter p_BankAccNo = cmd.Parameters.Add("BankAccNo", SqlDbType.VarChar);
        p_BankAccNo.Direction = ParameterDirection.Input;
        p_BankAccNo.Value = strAccNo;

        SqlParameter p_DEPTID = cmd.Parameters.Add("DEPTID", DBNull.Value);
        p_DEPTID.Direction = ParameterDirection.Input;
        p_DEPTID.IsNullable = true;
        if (strDeptId != "")
            p_DEPTID.Value = strDeptId;

        SqlParameter p_DESGID = cmd.Parameters.Add("DesigId", DBNull.Value );
        p_DESGID.Direction = ParameterDirection.Input;
        p_DESGID.IsNullable =true ;
        if (strDesgId != "")
            p_DESGID.Value = strDesgId;

        SqlParameter p_EMPTYPEID = cmd.Parameters.Add("EMPTYPEID", SqlDbType.BigInt);
        p_EMPTYPEID.Direction = ParameterDirection.Input;
        p_EMPTYPEID.Value = strEmpTypeId;

        SqlParameter p_PLANACCLINE = cmd.Parameters.Add("PLANACCLINE", SqlDbType.BigInt);
        p_PLANACCLINE.Direction = ParameterDirection.Input;
        p_PLANACCLINE.Value = 0;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYear;

        SqlParameter p_EMPGRPID = cmd.Parameters.Add("EMPGRPID", SqlDbType.BigInt);
        p_EMPGRPID.Direction = ParameterDirection.Input;
        p_EMPGRPID.Value = strEmpGrpID;

        SqlParameter p_TaxPercent = cmd.Parameters.Add("TaxPercent", SqlDbType.BigInt);
        p_TaxPercent.Direction = ParameterDirection.Input;
        p_TaxPercent.Value = strTaxPercent;

        SqlParameter p_DivId = cmd.Parameters.Add("DivisionId", SqlDbType.BigInt);
        p_DivId.Direction = ParameterDirection.Input;
        p_DivId.Value = strDivId;

        SqlParameter p_ClinicId = cmd.Parameters.Add("ClinicId", SqlDbType.BigInt);
        p_ClinicId.Direction = ParameterDirection.Input;
        p_ClinicId.Value = strClinicId;

        SqlParameter p_PFFISCALYRID = cmd.Parameters.Add("PFFISCALYRID", SqlDbType.BigInt);
        p_PFFISCALYRID.Direction = ParameterDirection.Input;
        p_PFFISCALYRID.Value = strPFFiscalYrId;

        SqlParameter p_TaxFISCALYRID = cmd.Parameters.Add("TaxFISCALYRID", SqlDbType.BigInt);
        p_TaxFISCALYRID.Direction = ParameterDirection.Input;
        p_TaxFISCALYRID.Value = strTaxFiscalYrId;

        return cmd;
    }

    protected SqlCommand InsertPaySlipDetails(string strPSBId,string strEmpId,string strSalHeadId,string strPayAmt,string strIsDeduct,
        string strHeadType,string strAmtCompPay, string strInsBy,string strInsDate)
    {
        SqlCommand cmd=new SqlCommand("proc_Payroll_Insert_PaySlipDets");
        cmd.CommandType=CommandType.StoredProcedure;

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
        p_PAYAMT.Value = Common.CheckNullString(strPayAmt) == "" ? "0" : strPayAmt;

        SqlParameter p_ISDEDUCTED = cmd.Parameters.Add("ISDEDUCTED", SqlDbType.Char);
        p_ISDEDUCTED.Direction = ParameterDirection.Input;
        p_ISDEDUCTED.Value = strIsDeduct;

        SqlParameter p_HEADTYPE = cmd.Parameters.Add("HEADTYPE", SqlDbType.Char);
        p_HEADTYPE.Direction = ParameterDirection.Input;
        p_HEADTYPE.Value = strHeadType;

        SqlParameter p_AMTCOMPAY = cmd.Parameters.Add("AMTCOMPAY", SqlDbType.Decimal);
        p_AMTCOMPAY.Direction = ParameterDirection.Input;
        p_AMTCOMPAY.Value = Common.CheckNullString(strAmtCompPay) == "" ? "0" : strAmtCompPay;

        SqlParameter p_INSERTEDBY = cmd.Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = strInsBy;

        SqlParameter p_INSERTEDDATE = cmd.Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDDATE.Direction = ParameterDirection.Input;
        p_INSERTEDDATE.Value = strInsDate;

        return cmd;
    }

    protected SqlCommand UpdateBonusAllowance(string strEmpId, string strMonth, string strYear,string strInsBy, string strInsDate)
    {
        string strSQL = "UPDATE BONUSALLOWANCE SET VSTATUS=@VSTATUS, UPDATEDBY=@UPDATEDBY, UPDATEDDATE=@UPDATEDDATE WHERE VMONTH=@VMONTH AND VYEAR=@VYEAR AND EMPID=@EMPID";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;


        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpId;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_VSTATUS = cmd.Parameters.Add("VSTATUS", SqlDbType.Char);
        p_VSTATUS.Direction = ParameterDirection.Input;
        p_VSTATUS.Value = "A";

        SqlParameter p_INSERTEDBY = cmd.Parameters.Add("UPDATEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = strInsBy;


        SqlParameter p_INSERTEDDATE = cmd.Parameters.Add("UPDATEDDATE", SqlDbType.DateTime);
        p_INSERTEDDATE.Direction = ParameterDirection.Input;
        p_INSERTEDDATE.Value = strInsDate;

        return cmd;
    }


    protected string getHeadType(DataRow dRow)
    {
        string strHeadType = "";
        if (dRow["IsBasicSal"].ToString().Trim() == "Y")
            strHeadType = "B";
        else if (dRow["IsProvidentFund"].ToString().Trim() == "Y")
            strHeadType = "P";
        else if (dRow["IsPFLoanDeduction"].ToString().Trim() == "Y")
            strHeadType = "E";
        else if (dRow["IsAdvanceDeducttion"].ToString().Trim() == "Y")
            strHeadType = "V";
        else if (dRow["IsLateDeduction"].ToString().Trim() == "Y")
            strHeadType = "L";
        else if (dRow["IsAttndBonus"].ToString().Trim() == "Y")
            strHeadType = "S";
        else if (dRow["IsProductionBonus"].ToString().Trim() == "Y")
            strHeadType = "U";
        else if (dRow["IsOT"].ToString().Trim() == "Y")
            strHeadType = "T";
        else if (dRow["IsFestivalBonus"].ToString().Trim() == "Y")
            strHeadType = "F";
        else if (dRow["IsArea"].ToString().Trim() == "Y")
            strHeadType = "R";
        else if (dRow["IsOtherPayment"].ToString().Trim() == "Y")
            strHeadType = "O";
        return strHeadType;
    }
    #endregion

    #region Select Querys
    public DataTable GetEmployeeData(string strMonth, string strYear, string strEmpStatus, string strEmpId,string strClinicId)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Select_EmpInfo_PayslipPreparation");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_EmpStatus = cmd.Parameters.Add("EmpStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = strEmpStatus;

        SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_ClinicId = cmd.Parameters.Add("ClinicId", SqlDbType.BigInt);
        p_ClinicId.Direction = ParameterDirection.Input;
        p_ClinicId.Value = strClinicId;

        objDC.CreateDSFromProc(cmd, "EmpInfoPayslipPreparation");
        return objDC.ds.Tables["EmpInfoPayslipPreparation"];
    }

    public DataTable GetEmployeeDataForAttndClearance(string strEndDate, string strMPCID, string strEmpID, string strEmpType,string strCostCenter)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Select_AttendanceClearance");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_JoiningDate = cmd.Parameters.Add("JoiningDate", SqlDbType.DateTime);
        p_JoiningDate.Direction = ParameterDirection.Input;
        p_JoiningDate.Value = strEndDate;

        SqlParameter p_MPCID = cmd.Parameters.Add("MPCID", SqlDbType.BigInt);
        p_MPCID.Direction = ParameterDirection.Input;
        p_MPCID.Value = strMPCID;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        SqlParameter p_EmpTypeID = cmd.Parameters.Add("EmpTypeID", SqlDbType.Char);
        p_EmpTypeID.Direction = ParameterDirection.Input;
        p_EmpTypeID.Value = strEmpType;

        SqlParameter p_ClinicId = cmd.Parameters.Add("ClinicId", SqlDbType.Char);
        p_ClinicId.Direction = ParameterDirection.Input;
        p_ClinicId.Value = strCostCenter;

        objDC.CreateDSFromProc(cmd, "EmployeeDataForAttndClearance");
        return objDC.ds.Tables["EmployeeDataForAttndClearance"];
    }    

    public string GetEmployeeLastSalaryDisbursementDate(string strEmpID)
    {
        string strRetValue = "";
        string strSQL="SELECT MAX(PAYDUREND) AS PAYDUREND FROM PaySlipMst WHERE EMPID=@EMPID AND SALARYTYPE='S' AND  PaySlipStatus<>'H'";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        strRetValue = objDC.GetScalarVal(cmd);
        return strRetValue;
    }

    public DataTable GetEmpSalaryPackDetails(string strEmpId)
    {
        if (objDC.ds.Tables["EmpSalaryPackDetails"] != null)
        {
            objDC.ds.Tables["EmpSalaryPackDetails"].Rows.Clear();
            objDC.ds.Tables["EmpSalaryPackDetails"].Dispose();
        }

        SqlCommand cmd = new SqlCommand("proc_Payroll_Select_EmpWiseSalarypackDetails");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpId;
        
        objDC.CreateDSFromProc(cmd, "EmpSalaryPackDetails");
        return objDC.ds.Tables["EmpSalaryPackDetails"];
    }


    public DataTable getEmpWiseAttendanceRecord(string strEmpID, string strStartDate, string strEndDate)
    {
        if (objDC.ds.Tables["EmpWiseAttndRecord"] != null)
        {
            objDC.ds.Tables["EmpWiseAttndRecord"].Rows.Clear();
            objDC.ds.Tables["EmpWiseAttndRecord"].Dispose();
        }

        SqlCommand cmd = new SqlCommand("proc_Payroll_Select_AttendanceRecord");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;
        
        SqlParameter p_STARTDATE = cmd.Parameters.Add("STARTDATE", SqlDbType.DateTime);
        p_STARTDATE.Direction = ParameterDirection.Input;
        p_STARTDATE.Value = strStartDate;

        SqlParameter p_ENDDATE = cmd.Parameters.Add("ENDDATE", SqlDbType.DateTime);
        p_ENDDATE.Direction = ParameterDirection.Input;
        p_ENDDATE.Value = strEndDate;

        objDC.CreateDSFromProc(cmd, "EmpWiseAttndRecord");
        return objDC.ds.Tables["EmpWiseAttndRecord"];
    }

    public DataTable GetLoanType()
    {
        string strSQL="SELECT SHeadID,LoanTypeID FROM LoanType where ISACTIVE='Y'";
        objDC.CreateDT(strSQL,"LoanType");
        return objDC.ds.Tables["LoanType"];
    }

    public DataTable GetLoanSalaryHead(string strHeadID)
    {
        string strSQL = "SELECT HEADNAME,HEADNATURE,ASSACCNUM FROM SalaryHead WHERE SHEADID=@SHEADID";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_SHEADID = cmd.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = strHeadID;


        objDC.CreateDT(cmd, "LoanSalHead");
        return objDC.ds.Tables["LoanSalHead"];
    }

    public Decimal GetEmployeLoanDetails(string strEmpID, string strEndDate, string strLoanTypeID)
    {

        string  strAmt = "";
        string strSQL = "SELECT SUM(b.PAYAMNT) as Amnt FROM EmpSalLoanMst a, EmpSalLoanDet b "
                + " WHERE a.LOANAPPID=b.LOANAPPID AND a.EMPID=@EMPID"
                + " AND b.PAYDATE <=@PAYDATE "
                + " AND  b.PaidStatus='N' AND a.LOANSTATUS='D' AND a.LoanTypeID=@LoanTypeID ";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EmpID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;


        SqlParameter p_PAYDATE = cmd.Parameters.Add("PAYDATE", SqlDbType.DateTime);
        p_PAYDATE.Direction = ParameterDirection.Input;
        p_PAYDATE.Value = strEndDate;

        SqlParameter p_LoanTypeID = cmd.Parameters.Add("LoanTypeID", SqlDbType.BigInt);
        p_LoanTypeID.Direction = ParameterDirection.Input;
        p_LoanTypeID.Value = strLoanTypeID;


        strAmt = objDC.GetScalarVal(cmd);
        return string.IsNullOrEmpty(strAmt) == false ? Convert.ToDecimal(strAmt) : 0;
    }

    public DataTable GetLoanPaymentCheck(string strEmpID, string strEndDate, string strLoanTypeID)
    {
        string strSQL = "SELECT a.LOANAPPID  FROM EmpSalLoanMst a, EmpSalLoanDet b "
                + " WHERE a.LOANAPPID=b.LOANAPPID AND a.EMPID=@EMPID"
                + " AND b.PAYDATE <=@PAYDATE"
                + " AND  b.PaidStatus='N' AND a.LOANSTATUS='D' AND a.LoanTypeID=@LoanTypeID ";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;


        SqlParameter p_EmpID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;


        SqlParameter p_PAYDATE = cmd.Parameters.Add("PAYDATE", SqlDbType.DateTime);
        p_PAYDATE.Direction = ParameterDirection.Input;
        p_PAYDATE.Value = strEndDate;

        SqlParameter p_LoanTypeID = cmd.Parameters.Add("LoanTypeID", SqlDbType.BigInt);
        p_LoanTypeID.Direction = ParameterDirection.Input;
        p_LoanTypeID.Value = strLoanTypeID;

        objDC.CreateDT(cmd, "LoanPaymentCheck");
        return objDC.ds.Tables["LoanPaymentCheck"];
    }

    public DataTable GetLoanAmountAndPFInterest(string strAppID, string strEmpId, string strLoanTypeID)
    {
        string strSQL="SELECT LoanAmnt,PFInterest FROM EmpSalLoanMst "
                + " WHERE LoanAppID=@LoanAppID AND EMPID=@EMPID "
                + " AND LOANSTATUS='D' AND LoanTypeID=@LoanTypeID";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType=CommandType.Text;
        
        SqlParameter p_LoanAppID = cmd.Parameters.Add("LoanAppID", SqlDbType.BigInt);
        p_LoanAppID.Direction = ParameterDirection.Input;
        p_LoanAppID.Value = strAppID;

        SqlParameter p_EmpID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpId;

        SqlParameter p_LoanTypeID = cmd.Parameters.Add("LoanTypeID", SqlDbType.BigInt);
        p_LoanTypeID.Direction = ParameterDirection.Input;
        p_LoanTypeID.Value = strLoanTypeID;

        objDC.CreateDT(cmd, "LoanAmountAndPFInterest");
        return objDC.ds.Tables["LoanAmountAndPFInterest"];

    }

    public DataTable GetAppWiseLoanPayAmt(string strAppID, string strEmpId, string strLoanTypeID)
    {
        string strSQL = "SELECT SUM(b.PAYAMNT) as Amnt FROM EmpSalLoanMst a, EmpSalLoanDet b "
                + " WHERE a.LOANAPPID=b.LOANAPPID AND LoanAppID=@LoanAppID AND EMPID=@EMPID "
                + " AND b.PaidStatus='Y' AND a.LOANSTATUS='D' AND  LoanTypeID=@LoanTypeID";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_LoanAppID = cmd.Parameters.Add("LoanAppID", SqlDbType.BigInt);
        p_LoanAppID.Direction = ParameterDirection.Input;
        p_LoanAppID.Value = strAppID;

        SqlParameter p_EmpID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpId;

        SqlParameter p_LoanTypeID = cmd.Parameters.Add("LoanTypeID", SqlDbType.BigInt);
        p_LoanTypeID.Direction = ParameterDirection.Input;
        p_LoanTypeID.Value = strLoanTypeID;

        objDC.CreateDT(cmd, "AppWiseLoanPayAmt");
        return objDC.ds.Tables["AppWiseLoanPayAmt"];
    }

    public DataTable GetAppWiseRefundAmt(string strAppID, string strEmpId, string strLoanTypeID)
    {
        string strSQL = "SELECT SUM(RefundAmount) AS ReFndAmt "
                + " FROM RefundInformation "
                + " WHERE EmpID=@EmpID AND LoanTypeID=@LoanTypeID AND Status='R' AND LoanAppID=@LoanAppID ";
               

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EmpID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpId;

        SqlParameter p_LoanTypeID = cmd.Parameters.Add("LoanTypeID", SqlDbType.BigInt);
        p_LoanTypeID.Direction = ParameterDirection.Input;
        p_LoanTypeID.Value = strLoanTypeID;

        SqlParameter p_LoanAppID = cmd.Parameters.Add("LoanAppID", SqlDbType.BigInt);
        p_LoanAppID.Direction = ParameterDirection.Input;
        p_LoanAppID.Value = strAppID;

        objDC.CreateDT(cmd, "AppWiseRefundAmt");
        return objDC.ds.Tables["AppWiseRefundAmt"];
    }


    public DataTable GetHolidays(string strStartDate, string strEndDate)
    {
        string strSQL = "SELECT COUNT(HOLIDATE) AS TDays FROM HolidaysDetls "
                    + " WHERE HOLIDATE BETWEEN @STARTDATE AND @ENDDATE ";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_STARTDATE = cmd.Parameters.Add("STARTDATE", SqlDbType.DateTime);
        p_STARTDATE.Direction = ParameterDirection.Input;
        p_STARTDATE.Value = strStartDate;

        SqlParameter p_ENDDATE = cmd.Parameters.Add("ENDDATE", SqlDbType.DateTime);
        p_ENDDATE.Direction = ParameterDirection.Input;
        p_ENDDATE.Value = strEndDate;

        objDC.CreateDT(cmd, "PayslipHolidays");
        return objDC.ds.Tables["PayslipHolidays"];
    }

    public DataTable GetEmpWiseLeaveDetails(string strEmpId, string strStartDate, string strEndDate)
    {
        string strSQL="SELECT LTYPE,LEVDEDUCTAMNT,IsHalfDayLeave from LevAppDetDate WHERE (AppStatus='U' OR AppStatus='A') AND " 
                    + " LEVDATE BETWEEN @STARTDATE AND @ENDDATE AND EMPID=@EMPID ";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EmpID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpId;

        SqlParameter p_STARTDATE = cmd.Parameters.Add("STARTDATE", SqlDbType.DateTime);
        p_STARTDATE.Direction = ParameterDirection.Input;
        p_STARTDATE.Value = strStartDate;

        SqlParameter p_ENDDATE = cmd.Parameters.Add("ENDDATE", SqlDbType.DateTime);
        p_ENDDATE.Direction = ParameterDirection.Input;
        p_ENDDATE.Value = strEndDate;

        objDC.CreateDT(cmd, "PaySlipEmpWiseLeaveDetails");
        return objDC.ds.Tables["PaySlipEmpWiseLeaveDetails"];
    }

    public DataTable GetSalaryHead()
    {
        string strSQL = "SELECT * FROM SalaryHead";
        objDC.CreateDT(strSQL, "PaySlipSalHead");
        return objDC.ds.Tables["PaySlipSalHead"];
    }

    public DataTable GetPackageIDAndOTAmt(string strSalPakID)
    {
        string strSQL = "Select PackageID,OTAMT from SalaryPakMst where SalPakId=@SalPakId AND IsCompFacility='Y' ";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_SalPakId = cmd.Parameters.Add("SalPakId", SqlDbType.BigInt);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = strSalPakID;

        objDC.CreateDT(cmd, "PackageIDAndOTAmt");
        return objDC.ds.Tables["PackageIDAndOTAmt"];
    }


    public DataTable GetCompanyFacilityDetls(string strPackID)
    {
        string strSQL = "Select * from SalaryPakFacilityDetls where PackageID=@PackageID";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_PackageID = cmd.Parameters.Add("PackageID", SqlDbType.BigInt);
        p_PackageID.Direction = ParameterDirection.Input;
        p_PackageID.Value = strPackID;

        objDC.CreateDT(cmd, "CompanyFacilityDetls");
        return objDC.ds.Tables["CompanyFacilityDetls"];
    }


    public Decimal GetBasicSalary(string strSalPackID)
    {
        Decimal dclAmt = 0;
        string strSQL = "Select TOTAMNT from EMPSALARYPAKDETLS where SALPAKID=@SALPAKID AND ISBASICSAL='Y'";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_SalPakId = cmd.Parameters.Add("SALPAKID", SqlDbType.BigInt);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = strSalPackID;

        dclAmt = Convert.ToDecimal(objDC.GetScalarVal(cmd));

        return dclAmt;

    }

    public Decimal GetGrossSalary(string strSalPackID)
    {
        Decimal dclAmt = 0;
        string strSQL = "Select TOTALGROSSSAL from EMPSALARYPAKMST where SALPAKID=@SALPAKID";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_SalPakId = cmd.Parameters.Add("SALPAKID", SqlDbType.BigInt);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = strSalPackID;

        dclAmt = Convert.ToDecimal(objDC.GetScalarVal(cmd));

        return dclAmt;

    }

    public DataTable GetSalAdjustMst(string strStartDate, string strEndDate)
    {
        string strSQL = "Select SHeadID,AdjustID from SalAdjustMst where ISDeleted='N' AND PayslipSDate=@PayslipSDate AND PayslipEDate=@PayslipEDate";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        //SqlParameter p_EmpID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        //p_EmpID.Direction = ParameterDirection.Input;
        //p_EmpID.Value = strEmpId;

        SqlParameter p_STARTDATE = cmd.Parameters.Add("PayslipSDate", SqlDbType.DateTime);
        p_STARTDATE.Direction = ParameterDirection.Input;
        p_STARTDATE.Value = strStartDate;

        SqlParameter p_ENDDATE = cmd.Parameters.Add("PayslipEDate", SqlDbType.DateTime);
        p_ENDDATE.Direction = ParameterDirection.Input;
        p_ENDDATE.Value = strEndDate;

        objDC.CreateDT(cmd, "SalAdjustMst");
        return objDC.ds.Tables["SalAdjustMst"];
    }

    public DataTable GetSalAdjustDets(string strEmpId, string strAdjustID, string strSHeadId)
    {
        string strSQL = "Select a.*,b.SHeadId,b.HeadName,b.HeadNature,AssAccNum From SalAdjustDetls a,SalaryHead b where a.EmpID=@EmpID "
                    + " AND a.AdjustID=@AdjustID AND a.STATUS='P' AND b.SHeadId=@SHeadId ";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EmpID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpId;

        SqlParameter p_AdjustID = cmd.Parameters.Add("AdjustID", SqlDbType.BigInt);
        p_AdjustID.Direction = ParameterDirection.Input;
        p_AdjustID.Value = strAdjustID;

        SqlParameter p_SHeadId = cmd.Parameters.Add("SHeadId", SqlDbType.BigInt);
        p_SHeadId.Direction = ParameterDirection.Input;
        p_SHeadId.Value = strSHeadId;

        objDC.CreateDT(cmd, "SalAdjustDets");
        return objDC.ds.Tables["SalAdjustDets"];
    }

    public void UpdateAdjustDet(string strBookID, string strEmpID, string strAdjustID)
    {
        string strSQL = " Update SalAdjustDetls set PSBID=@PSBID where EmpID=@EmpID AND AdjustID=@AdjustID AND STATUS='P'";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_PSBID = cmd.Parameters.Add("PSBID", SqlDbType.BigInt);
        p_PSBID.Direction = ParameterDirection.Input;
        p_PSBID.Value = strBookID;

        SqlParameter p_EmpID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        SqlParameter p_AdjustID = cmd.Parameters.Add("AdjustID", SqlDbType.BigInt);
        p_AdjustID.Direction = ParameterDirection.Input;
        p_AdjustID.Value = strAdjustID;

        objDC.ExecuteQuery(cmd);                         
    }

    public DataTable GetEmployeeData(string strEmpID)
    {
        if (objDC.ds.Tables["EmployeeData"] != null)
        {
            objDC.ds.Tables["EmployeeData"].Rows.Clear();
            objDC.ds.Tables["EmployeeData"].Dispose();
        }
        string strSQL = "Select EmpId,JoiningDate,leavingDate from EmpInfo where Empid=@Empid";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EmpID = cmd.Parameters.Add("Empid", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        objDC.CreateDT(cmd, "EmployeeData");
        return objDC.ds.Tables["EmployeeData"];
    }

    public bool IsPayrollPeriodValid(string strDate)
    {
        string strRetText = "";
        string strSQL = "SELECT OPTID FROM PaySlipOption WHERE @vDATE BETWEEN PAYROLLVALIDFROM AND PAYROLLVALIDTO";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_vDATE = cmd.Parameters.Add("vDATE", SqlDbType.DateTime);
        p_vDATE.Direction = ParameterDirection.Input;
        p_vDATE.Value = strDate;

        strRetText= objDC.GetScalarVal(cmd);
        if (string.IsNullOrEmpty(strRetText) == true)
            return false;
        return true;
    }

    public DataTable SelectVaribaleAllowanceData(string strMonth, string strYear)
    {
        string strSQL = "SELECT VD.*,VM.EMPID,VM.SHEADID,(VD.PAYAMNT * SH.HEADNATURE) AS PAYAMT "
                       + " FROM VARIABLEALLOWANCEDEDUCTDETLS VD,VARIABLEALLOWANCEDEDUCT VM, SALARYHEAD SH "
                       + " WHERE VD.VID=VM.VID AND VM.SHEADID=SH.SHEADID AND VMONTH=@VMONTH AND VYEAR=@VYEAR";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        objDC.CreateDT(cmd, "SelectVaribaleAllowanceData");
        return objDC.ds.Tables["SelectVaribaleAllowanceData"];
    }

    public DataTable SelectVaribaleAllowanceDataEmpWise(string strMonth, string strYear,string strEmpId,string strHeadId)
    {
        string strSQL = "SELECT VD.*,VM.EMPID,VM.SHEADID,(VD.PAYAMNT * SH.HEADNATURE) AS PAYAMT "
                       + " FROM VARIABLEALLOWANCEDEDUCTDETLS VD,VARIABLEALLOWANCEDEDUCT VM, SALARYHEAD SH "
                       + " WHERE VD.VID=VM.VID AND VM.SHEADID=SH.SHEADID AND VMONTH=@VMONTH AND VYEAR=@VYEAR AND VM.EmpId=@EmpId AND VM.SHeadId=@SHeadId ";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_SHeadId = cmd.Parameters.Add("SHeadId", SqlDbType.BigInt);
        p_SHeadId.Direction = ParameterDirection.Input;
        p_SHeadId.Value = strHeadId;

        objDC.CreateDT(cmd, "SelectVaribaleAllowanceData");
        return objDC.ds.Tables["SelectVaribaleAllowanceData"];
    }

    public DataTable SelectOverTimeData(string strMonth, string strYear)
    {
        string strSQL = "SELECT * FROM OTAdjustment WHERE VMONTH=@VMONTH AND VYEAR=@VYEAR ORDER BY EmpId";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        objDC.CreateDT(cmd, "SelectOverTimeData");
        return objDC.ds.Tables["SelectOverTimeData"];
    }

    public DataTable SelectMedicalBenefitData(string strFiscalYrId,string strFromDate,string strToDate)
    {
        SqlCommand cmd = new SqlCommand();

        if (objDC.ds.Tables["SelectMedicalBenefit"]  != null)
        {
            objDC.ds.Tables["SelectMedicalBenefit"].Rows.Clear();
            objDC.ds.Tables["SelectMedicalBenefit"].Dispose(); 
        }
        string strSQL = "SELECT EmpId,MedFiscalYrId,SUM(ApproveAmount) AS ApproveAmount FROM MedicalBenefit WHERE MedFiscalYrId=@MedFiscalYrId AND BenefitType='M'"
            + " AND MedicalDate BETWEEN @FromDate AND @ToDate GROUP BY EmpId,MedFiscalYrId ORDER BY EmpId";

        cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_FiscalYrId = cmd.Parameters.Add("MedFiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = strFiscalYrId;

        SqlParameter p_FromDate = cmd.Parameters.Add("FromDate", SqlDbType.DateTime);
        p_FromDate.Direction = ParameterDirection.Input;
        p_FromDate.Value = strFromDate;

        SqlParameter p_ToDate = cmd.Parameters.Add("ToDate", SqlDbType.DateTime);
        p_ToDate.Direction = ParameterDirection.Input;
        p_ToDate.Value = strToDate;

        objDC.CreateDT(cmd, "SelectMedicalBenefit");
        return objDC.ds.Tables["SelectMedicalBenefit"];
    }

    public DataTable SelectHospitalBenefitData(string strFiscalYrId, string strFromDate, string strToDate)
    {
        SqlCommand cmd = new SqlCommand();

        if (objDC.ds.Tables["SelectHospitalBenefit"] != null)
        {
            objDC.ds.Tables["SelectHospitalBenefit"].Rows.Clear();
            objDC.ds.Tables["SelectHospitalBenefit"].Dispose();
        }
        string strSQL = "SELECT EmpId,MedFiscalYrId,SUM(ApproveAmount) AS ApproveAmount FROM MedicalBenefit WHERE MedFiscalYrId=@MedFiscalYrId AND BenefitType='H'"
            + " AND MedicalDate BETWEEN @FromDate AND @ToDate GROUP BY EmpId,MedFiscalYrId ORDER BY EmpId";

        cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_FiscalYrId = cmd.Parameters.Add("MedFiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = strFiscalYrId;

        SqlParameter p_FromDate = cmd.Parameters.Add("FromDate", SqlDbType.DateTime);
        p_FromDate.Direction = ParameterDirection.Input;
        p_FromDate.Value = strFromDate;

        SqlParameter p_ToDate = cmd.Parameters.Add("ToDate", SqlDbType.DateTime);
        p_ToDate.Direction = ParameterDirection.Input;
        p_ToDate.Value = strToDate;

        objDC.CreateDT(cmd, "SelectHospitalBenefit");
        return objDC.ds.Tables["SelectHospitalBenefit"];
    }

    public DataTable SelectAdditionalResponseData(string strFromDate, string strToDate)
    {
        SqlCommand cmd = new SqlCommand();

        if (objDC.ds.Tables["AdditionalResponse"] != null)
        {
            objDC.ds.Tables["AdditionalResponse"].Rows.Clear();
            objDC.ds.Tables["AdditionalResponse"].Dispose();
        }
        string strSQL = "SELECT * FROM EmpAddResponsibilityLog WHERE StartDate<=@FromDate AND EndDate>=@ToDate ORDER BY EmpId";

        cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_FromDate = cmd.Parameters.Add("FromDate", SqlDbType.DateTime);
        p_FromDate.Direction = ParameterDirection.Input;
        p_FromDate.Value = strFromDate;

        SqlParameter p_ToDate = cmd.Parameters.Add("ToDate", SqlDbType.DateTime);
        p_ToDate.Direction = ParameterDirection.Input;
        p_ToDate.Value = strToDate;

        objDC.CreateDT(cmd, "AdditionalResponse");
        return objDC.ds.Tables["AdditionalResponse"];
    }


    public DataTable GetEmployeeBonusData(string strMonth, string strYear)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Select_BonusAllowanceForPreparation");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        //SqlParameter p_EmpGrpID = cmd.Parameters.Add("EmpGrpID", SqlDbType.BigInt);
        //p_EmpGrpID.Direction = ParameterDirection.Input;
        //p_EmpGrpID.Value = strEmpGrpId;


        objDC.CreateDSFromProc(cmd, "GetEmployeeBonusData");
        return objDC.ds.Tables["GetEmployeeBonusData"];
    }

    public DataTable GetEmployeeRevStampData(string strSHeadID)
    {
        SqlCommand cmd = new SqlCommand("select  A.SALPAKID,A.SHEADID,B.HEADNAME,B.HEADNATURE,A.TOTAMNT From SALARYPAKDETLS A,SALARYHEAD B "
                                        + " where A.SHEADID=B.SHEADID AND A.SHEADID=@SHEADID ");
        cmd.CommandType = CommandType.Text;

        SqlParameter p_SHEADID = cmd.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = strSHeadID;

        return objDC.CreateDT(cmd, "GetEmployeeRevStampData");
    }

    //Remote Allowance
    public DataTable SelectRemoteAllowanceData(string strDateFrom, string strDateTo)
    {
        string strSQL = "SELECT * FROM RemoteAllowanceAdd WHERE (DateFrom BETWEEN @DateFrom AND @DateTo)"
            + " OR (DateTo BETWEEN @DateFrom AND @DateTo) ORDER BY EmpId";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_DateFrom = cmd.Parameters.Add("DateFrom", SqlDbType.DateTime);
        p_DateFrom.Direction = ParameterDirection.Input;
        p_DateFrom.Value = strDateFrom;

        SqlParameter p_DateTo = cmd.Parameters.Add("DateTo", SqlDbType.DateTime);
        p_DateTo.Direction = ParameterDirection.Input;
        p_DateTo.Value = strDateTo;

        objDC.CreateDT(cmd, "SelectRemoteAllowanceAddData");
        return objDC.ds.Tables["SelectRemoteAllowanceAddData"];
    }

    //Child Edu Allowance
    public DataTable SelectChildEduAllowanceData(string strMonth, string strYear)
    {
        string strSQL = "SELECT EmpId,SUM(Amount) AS Amount FROM ChildEduAllowance Where VMONTH=@VMONTH AND VYEAR=@VYEAR GROUP BY EmpId ORDER BY EmpId";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        objDC.CreateDT(cmd, "SelectChildEduAllowanceData");
        return objDC.ds.Tables["SelectChildEduAllowanceData"];
    }
    
#endregion
    #region PayID
    public long GerMaxPayID(string strMonth, string strYear, string strSalType)
    {
        try
        {
            long maxIDField = 0;
            string strSQL = "select max(PAYID) from PAYSLIPMST where VMONTH=@VMONTH AND VYEAR=@VYEAR AND SALARYTYPE=@SALARYTYPE";
            SqlCommand cmd = new SqlCommand(strSQL);

            SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
            p_VMONTH.Direction = ParameterDirection.Input;
            p_VMONTH.Value = strMonth;

            SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
            p_VYEAR.Direction = ParameterDirection.Input;
            p_VYEAR.Value = strYear;

            SqlParameter p_SALARYTYPE = cmd.Parameters.Add("SALARYTYPE", SqlDbType.Char);
            p_SALARYTYPE.Direction = ParameterDirection.Input;
            p_SALARYTYPE.Value = strSalType;

            //maxIDField =Convert.ToInt64(cmd.ExecuteScalar());
            string strMaxID = objDC.GetScalarVal(cmd);

            if (string.IsNullOrEmpty(strMaxID))
            {
                maxIDField = 0;
            }
            else
            {
                maxIDField = Convert.ToInt64(strMaxID);

            }
            return maxIDField + 1;
        }


        catch (SqlException ex)
        {
            throw new HttpException(ex.ToString());
        }
        catch (Exception ex)
        {
            throw new HttpException("SQL: Error found" + ex.ToString());
        }

    }

    #endregion
    public void UpdatePaySlipFlag(string strEmpID,string strMonth,string strYear, string strFlag,string strSalType)
    {
        string strSQL = "UPDATE PAYSLIPMST SET PAYSLIPSTATUS='D' WHERE  EMPID=@EMPID AND VYEAR=@VYEAR AND VMONTH=@VMONTH AND SALARYTYPE=@SALARYTYPE";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_SALARYTYPE = cmd.Parameters.Add("SALARYTYPE", SqlDbType.Char);
        p_SALARYTYPE.Direction = ParameterDirection.Input;
        p_SALARYTYPE.Value = strSalType;

        objDC.ExecuteQuery(cmd);

    }

    #region PF
    public DataTable GetPFLoanLedgerForPayrollPreparation(string strMonth, string strFY)
    {
        //SqlCommand cmd = new SqlCommand("SELECT EMPID,CMREPAY,CLLOAN,OPLOAN,CMINTEREST,CMLOANAMT,CMCASH FROM PFLoanLedger WHERE VMONTH=@VMONTH AND FISCALYRID=@FISCALYRID");
        SqlCommand cmd = new SqlCommand("SELECT EMPID,CMREPAY,CLLOAN,OPLOAN,CMINTEREST,CMLOANAMT,CMCASH FROM PFLoanLedger WHERE VMONTH=@VMONTH AND FISCALYRID=@FISCALYRID AND CMINTS>0");
        cmd.CommandType = CommandType.Text;

        SqlParameter p_LOANMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_LOANMONTH.Direction = ParameterDirection.Input;
        p_LOANMONTH.Value = strMonth;
        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFY;
        objDC.CreateDT(cmd, "GetPFLoanLedgerForPayrollPreparation");
        return objDC.ds.Tables["GetPFLoanLedgerForPayrollPreparation"];
    }
    public DataTable GetPFLoanDataForPayrollPreparation(string strMonth, string strFY)
    {
        SqlCommand cmd = new SqlCommand("SELECT EmpID,MonthlyRepay,LoanRate,LoanAmt,MonthlyInterest from EMPPFLOANMST where LoanMonth=@VMONTH and FiscalYrID=@FISCALYRID");
        cmd.CommandType = CommandType.Text;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFY;

        objDC.CreateDT(cmd, "GetPFLoanDataForPayrollPreparation");
        return objDC.ds.Tables["GetPFLoanDataForPayrollPreparation"];
    }
    public DataTable GetPFLoanAdjustmentForPayrollPreparation(string strMonth, string strFY)
    {
        SqlCommand cmd = new SqlCommand("SELECT EMPID,ADJAMOUNT,PRINCIPALDUE,INTDUE,ADJTYPE FROM PFLoanAdjustment WHERE ADJMONTH=@VMONTH AND FISCALYRID=@FISCALYRID");
        cmd.CommandType = CommandType.Text;

        SqlParameter p_LOANMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_LOANMONTH.Direction = ParameterDirection.Input;
        p_LOANMONTH.Value = strMonth;
        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFY;
        objDC.CreateDT(cmd, "GetPFLoanAdjustmentForPayrollPreparation");
        return objDC.ds.Tables["GetPFLoanAdjustmentForPayrollPreparation"];
    }

    public DataTable GetPayrollArrearForPreparation(string strMonth, string strFY)
    {
        string strSQL = " SELECT * FROM  PayrollArrear WHERE VMonth=@VMonth AND FiscalYrID=@FiscalYrID";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFY;
        
        return objDC.CreateDT(cmd,"GetPayrollArrearForPreparation");
    }
    #endregion
    public Payroll_PreparationManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
