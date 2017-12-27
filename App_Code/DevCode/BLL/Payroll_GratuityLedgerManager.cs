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
/// Summary description for Payroll_GratuityLedgerManager
/// </summary>
public class Payroll_GratuityLedgerManager
{
    DBConnector objDC = new DBConnector();


    public void InsertGratuityLedger(string strLedgerID, string strEmpID, string strMonth, string strYear, string strFiscalYear,
       string strDesgId, string strGratuityFrom, string strBasicSal, string strPMonth,string strPYear, string strPMonthAmt,
       string strCMonth,string strCYear, string strCMonthAmt,string strChargingAmt,
       string strInsBy, string strInsDate, string strIsUpdate)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Insert_GratuityLedger");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LEDGERID = cmd.Parameters.Add("LEDGERID", SqlDbType.BigInt);
        p_LEDGERID.Direction = ParameterDirection.Input;
        p_LEDGERID.Value = strLedgerID;

        SqlParameter p_EmpID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        SqlParameter p_DESGID = cmd.Parameters.Add("DESGID", SqlDbType.BigInt);
        p_DESGID.Direction = ParameterDirection.Input;
        p_DESGID.Value = strDesgId;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYear;

        SqlParameter p_GRATUITYFROM = cmd.Parameters.Add("GRATUITYFROM", SqlDbType.DateTime);
        p_GRATUITYFROM.Direction = ParameterDirection.Input;
        p_GRATUITYFROM.Value = strGratuityFrom;

        
        SqlParameter p_BASIC = cmd.Parameters.Add("BASIC", SqlDbType.BigInt);
        p_BASIC.Direction = ParameterDirection.Input;
        p_BASIC.Value = strBasicSal;

        SqlParameter p_PMONTH = cmd.Parameters.Add("PMONTH", SqlDbType.BigInt);
        p_PMONTH.Direction = ParameterDirection.Input;
        p_PMONTH.Value = strPMonth;

        SqlParameter p_PYEAR = cmd.Parameters.Add("PYEAR", SqlDbType.BigInt);
        p_PYEAR.Direction = ParameterDirection.Input;
        p_PYEAR.Value = strPYear;

        SqlParameter p_PMONTHAMT = cmd.Parameters.Add("PMONTHAMT", SqlDbType.BigInt);
        p_PMONTHAMT.Direction = ParameterDirection.Input;
        p_PMONTHAMT.Value = strPMonthAmt;

        SqlParameter p_CMONTH = cmd.Parameters.Add("CMONTH", SqlDbType.BigInt);
        p_CMONTH.Direction = ParameterDirection.Input;
        p_CMONTH.Value = strCMonth;

        SqlParameter p_CYEAR = cmd.Parameters.Add("CYEAR", SqlDbType.BigInt);
        p_CYEAR.Direction = ParameterDirection.Input;
        p_CYEAR.Value = strCYear;

        SqlParameter p_CMONTHAMT = cmd.Parameters.Add("CMONTHAMT", SqlDbType.BigInt);
        p_CMONTHAMT.Direction = ParameterDirection.Input;
        p_CMONTHAMT.Value = strCMonthAmt;

        SqlParameter p_CHARGINGAMT = cmd.Parameters.Add("CHARGINGAMT", SqlDbType.BigInt);
        p_CHARGINGAMT.Direction = ParameterDirection.Input;
        p_CHARGINGAMT.Value = strChargingAmt;

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

    public bool IsCurrentMonthLeaveEncashmentPaymentExist(string strEmpID, string strMonth, string strFinYear)
    {
        string strSQL = "";
        strSQL = "SELECT TRANSID FROM LeaveEncashment WHERE EMPID=@EMPID AND GRATUITYMONTH=@GRATUITYMONTH AND FISCALYRID=@FISCALYRID";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_GRATUITYMONTH = cmd.Parameters.Add("GRATUITYMONTH", SqlDbType.BigInt);
        p_GRATUITYMONTH.Direction = ParameterDirection.Input;
        p_GRATUITYMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        SqlParameter p_EmpID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        string strValue = objDC.GetScalarVal(cmd);

        if (string.IsNullOrEmpty(strValue) == true)
            return false;
        else
            return true;
    }

    public void InsertLeaveEncashment(string strTransID, string strEmpID, string strHrEmpID, string strDesgID, string strJoinDate,
        string strGrFrom, string strGrTo, string strNextGrFrom, string strGrMonth, string strGrYear, string strFinYear,
        string strBasic, string strGrAccrued, string strCurrGr, string strGrFraction, string strGrPayDate, string strPayAmt, string strRemarks,
        string strInsBy, string strInsDate, string strGrDuration, string strUnitDaySal, string strBonusAllow)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Insert_LeaveEncashment");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LEDGERID = cmd.Parameters.Add("TRANSID", SqlDbType.BigInt);
        p_LEDGERID.Direction = ParameterDirection.Input;
        p_LEDGERID.Value = strTransID;

        SqlParameter p_EmpID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        SqlParameter p_HREMPID = cmd.Parameters.Add("HREMPID", SqlDbType.Char);
        p_HREMPID.Direction = ParameterDirection.Input;
        p_HREMPID.Value = strHrEmpID;

        SqlParameter p_JBTLID = cmd.Parameters.Add("JBTLID", SqlDbType.BigInt);
        p_JBTLID.Direction = ParameterDirection.Input;
        p_JBTLID.Value = strDesgID;

        SqlParameter p_JOININGDATE = cmd.Parameters.Add("JOININGDATE", SqlDbType.DateTime);
        p_JOININGDATE.Direction = ParameterDirection.Input;
        p_JOININGDATE.Value = strJoinDate;

        SqlParameter p_GRATUITYFROM = cmd.Parameters.Add("GRATUITYFROM", DBNull.Value);
        p_GRATUITYFROM.Direction = ParameterDirection.Input;
        p_GRATUITYFROM.IsNullable = true;
        if (string.IsNullOrEmpty(strGrFrom) == false)
            p_GRATUITYFROM.Value = strGrFrom;

        SqlParameter p_GRATUITYTO = cmd.Parameters.Add("GRATUITYTO", DBNull.Value);
        p_GRATUITYTO.Direction = ParameterDirection.Input;
        p_GRATUITYTO.IsNullable = true;
        if (string.IsNullOrEmpty(strGrTo) == false)
            p_GRATUITYTO.Value = strGrTo;

        SqlParameter p_NEXTGRATUITYFROM = cmd.Parameters.Add("NEXTGRATUITYFROM", SqlDbType.DateTime);
        p_NEXTGRATUITYFROM.Direction = ParameterDirection.Input;
        p_NEXTGRATUITYFROM.Value = strNextGrFrom;

        SqlParameter p_GRATUITYMONTH = cmd.Parameters.Add("GRATUITYMONTH", SqlDbType.BigInt);
        p_GRATUITYMONTH.Direction = ParameterDirection.Input;
        p_GRATUITYMONTH.Value = strGrMonth;

        SqlParameter p_GRATUITYYEAR = cmd.Parameters.Add("GRATUITYYEAR", SqlDbType.BigInt);
        p_GRATUITYYEAR.Direction = ParameterDirection.Input;
        p_GRATUITYYEAR.Value = strGrYear;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        SqlParameter p_BASIC = cmd.Parameters.Add("BASIC", SqlDbType.Decimal);
        p_BASIC.Direction = ParameterDirection.Input;
        p_BASIC.Value = strBasic;

        SqlParameter p_LASTGRATUITY = cmd.Parameters.Add("LASTGRATUITY", SqlDbType.Decimal);
        p_LASTGRATUITY.Direction = ParameterDirection.Input;
        p_LASTGRATUITY.Value = strGrAccrued;

        SqlParameter p_CURRGRATUITY = cmd.Parameters.Add("CURRGRATUITY", SqlDbType.Decimal);
        p_CURRGRATUITY.Direction = ParameterDirection.Input;
        p_CURRGRATUITY.Value = strCurrGr;

        SqlParameter p_GRATUITYFRACTION = cmd.Parameters.Add("GRATUITYFRACTION", SqlDbType.Decimal);
        p_GRATUITYFRACTION.Direction = ParameterDirection.Input;
        p_GRATUITYFRACTION.Value = strGrFraction;

        SqlParameter p_PAYDATE = cmd.Parameters.Add("PAYDATE", SqlDbType.DateTime);
        p_PAYDATE.Direction = ParameterDirection.Input;
        p_PAYDATE.Value = strGrPayDate;

        SqlParameter p_PAYAMT = cmd.Parameters.Add("PAYAMT", SqlDbType.Decimal);
        p_PAYAMT.Direction = ParameterDirection.Input;
        p_PAYAMT.Value = strPayAmt;

        SqlParameter p_REMARKS = cmd.Parameters.Add("REMARKS", SqlDbType.VarChar);
        p_REMARKS.Direction = ParameterDirection.Input;
        p_REMARKS.Value = strRemarks;

        SqlParameter p_INSERTEDBY = cmd.Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = strInsBy;

        SqlParameter p_INSERTEDDATE = cmd.Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDDATE.Direction = ParameterDirection.Input;
        p_INSERTEDDATE.Value = strInsDate;

        SqlParameter p_GrDuration = cmd.Parameters.Add("GrDuration", SqlDbType.Decimal);
        p_GrDuration.Direction = ParameterDirection.Input;
        p_GrDuration.Value = strGrDuration;

        SqlParameter p_UnitDaySal = cmd.Parameters.Add("UnitDaySal", SqlDbType.Decimal);
        p_UnitDaySal.Direction = ParameterDirection.Input;
        p_UnitDaySal.Value = strUnitDaySal;

        SqlParameter p_BONUSALLOW = cmd.Parameters.Add("BONUSALLOW", SqlDbType.Decimal);
        p_BONUSALLOW.Direction = ParameterDirection.Input;
        p_BONUSALLOW.Value = strBonusAllow;

        objDC.ExecuteQuery(cmd);
    }

    public void DeleteLeaveEncashmentData(string strTransID)
    {
        SqlCommand cmd = new SqlCommand("Proc_Delete_LeaveEncashment");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LEDGERID = cmd.Parameters.Add("TRANSID", SqlDbType.BigInt);
        p_LEDGERID.Direction = ParameterDirection.Input;
        p_LEDGERID.Value = strTransID;

        objDC.ExecuteQuery(cmd);
    }

    public DataTable GetLastFestivalDate(string strEmpID)
    {
        if (objDC.ds.Tables["GetLastFestivalDate"] != null)
        {
            objDC.ds.Tables["GetLastFestivalDate"].Rows.Clear();
            objDC.ds.Tables["GetLastFestivalDate"].Dispose();
        }

        SqlCommand command = new SqlCommand();
        command = new SqlCommand("proc_Get_Last_Festival");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpID = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        objDC.CreateDSFromProc(command, "GetLastFestivalDate");
        return objDC.ds.Tables["GetLastFestivalDate"];
    }

    public void GratuityAccrued(GridView grLedger, string strInsBy, string strInsDate, string strFisCalYear,string  strGrStatus,string strMonth)
    {
        int i=0;
        decimal dclPrevGratuityAmt = 0;
        decimal dclCurrGratuityAmt = 0;
        decimal dclDiff = 0;

        SqlCommand[] command = new SqlCommand[grLedger.Rows.Count + 1];
        
        command[i] = new SqlCommand("Proc_Payroll_Delete_GratuityLedger");
        command[i].CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH1 = command[i].Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH1.Direction = ParameterDirection.Input;
        p_VMONTH1.Value = strMonth;

        SqlParameter p_FISCALYRID1 = command[i].Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID1.Direction = ParameterDirection.Input;
        p_FISCALYRID1.Value = strFisCalYear;

        SqlParameter p_GratuityStatus1 = command[i].Parameters.Add("GratuityStatus", SqlDbType.Char);
        p_GratuityStatus1.Direction = ParameterDirection.Input;
        p_GratuityStatus1.Value = "Y";
        
        i++;
        foreach (GridViewRow gRow in grLedger.Rows)
        {
            dclPrevGratuityAmt = 0;
            dclCurrGratuityAmt = 0;
            dclDiff = 0;
            dclCurrGratuityAmt = Common.RoundDecimal(gRow.Cells[10].Text.Trim(), 0);
            dclPrevGratuityAmt = Common.RoundDecimal(grLedger.DataKeys[gRow.DataItemIndex].Values[9].ToString().Trim(), 0);
            dclDiff = dclCurrGratuityAmt - dclPrevGratuityAmt;

            command[i] = this.GetGratuityLedgerCommand(
                grLedger.DataKeys[gRow.DataItemIndex].Values[7].ToString().Trim(),
                grLedger.DataKeys[gRow.DataItemIndex].Values[0].ToString().Trim(),
                grLedger.DataKeys[gRow.DataItemIndex].Values[4].ToString().Trim(),
                grLedger.DataKeys[gRow.DataItemIndex].Values[5].ToString().Trim(),
                strFisCalYear,
                grLedger.DataKeys[gRow.DataItemIndex].Values[1].ToString().Trim(),
                Common.SetDate(gRow.Cells[3].Text.Trim()), // Join Date
                Common.SetDate(gRow.Cells[4].Text.Trim()), // Gr From
                Common.SetDate(gRow.Cells[5].Text.Trim()), // Gr Upto
                gRow.Cells[6].Text.Trim(), // Basic
                gRow.Cells[7].Text.Trim(), // Gr Basic
                gRow.Cells[8].Text.Trim(), // Length Days
                gRow.Cells[9].Text.Trim(), // Length Years
                grLedger.DataKeys[gRow.DataItemIndex].Values[2].ToString().Trim(), // Pmonth
                grLedger.DataKeys[gRow.DataItemIndex].Values[3].ToString().Trim(), // PYear
                grLedger.DataKeys[gRow.DataItemIndex].Values[9].ToString().Trim(), // PMonth Amount
                grLedger.DataKeys[gRow.DataItemIndex].Values[4].ToString().Trim(), // Cmonth
                grLedger.DataKeys[gRow.DataItemIndex].Values[5].ToString().Trim(), // CYear
                gRow.Cells[10].Text.Trim(), // CMonth Amount
                dclDiff.ToString(), // Charging Amount
                strInsBy, strInsDate, grLedger.DataKeys[gRow.DataItemIndex].Values[6].ToString().Trim(), strGrStatus);
            i++;                                    
        }
        objDC.MakeTransaction(command);
    }

    public SqlCommand GetGratuityLedgerCommand(string strLedgerID, string strEmpID, string strMonth, string strYear, string strFiscalYear,
        string strDesgId,string strJoinDate,
        string strGratuityFrom, string strGratuityUpto,
        string strBasicSal, string strGratuityBasic,string strGratuityLength,string strGratuityYrs,
        string strPMonth, string strPYear, string strPMonthAmt,
      string strCMonth, string strCYear, string strCMonthAmt, string strChargingAmt,
      string strInsBy, string strInsDate, string strIsUpdate, string strEmpStatus)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Insert_GratuityLedger");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LEDGERID = cmd.Parameters.Add("LEDGERID", SqlDbType.BigInt);
        p_LEDGERID.Direction = ParameterDirection.Input;
        p_LEDGERID.Value = strLedgerID;

        SqlParameter p_EmpID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        SqlParameter p_DESGID = cmd.Parameters.Add("DESGID", SqlDbType.BigInt);
        p_DESGID.Direction = ParameterDirection.Input;
        p_DESGID.Value = strDesgId;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYear;

        SqlParameter p_JoiningDate = cmd.Parameters.Add("JoiningDate", SqlDbType.DateTime);
        p_JoiningDate.Direction = ParameterDirection.Input;
        p_JoiningDate.Value = strJoinDate;

        SqlParameter p_GRATUITYFROM = cmd.Parameters.Add("GRATUITYFROM", SqlDbType.DateTime);
        p_GRATUITYFROM.Direction = ParameterDirection.Input;
        p_GRATUITYFROM.Value = strGratuityFrom;

        SqlParameter p_GratuityUpto = cmd.Parameters.Add("GratuityUpto", SqlDbType.DateTime);
        p_GratuityUpto.Direction = ParameterDirection.Input;
        p_GratuityUpto.Value = strGratuityUpto;

        SqlParameter p_BASIC = cmd.Parameters.Add("BASIC", SqlDbType.Decimal);
        p_BASIC.Direction = ParameterDirection.Input;
        p_BASIC.Value = strBasicSal;

        SqlParameter p_GratuityBasic = cmd.Parameters.Add("GratuityBasic", SqlDbType.Decimal);
        p_GratuityBasic.Direction = ParameterDirection.Input;
        p_GratuityBasic.Value = strGratuityBasic;

        SqlParameter p_GratuityLength = cmd.Parameters.Add("GratuityLength", SqlDbType.Decimal);
        p_GratuityLength.Direction = ParameterDirection.Input;
        p_GratuityLength.Value = strGratuityLength;

        SqlParameter p_GratuityYrs = cmd.Parameters.Add("GratuityYrs", SqlDbType.Decimal);
        p_GratuityYrs.Direction = ParameterDirection.Input;
        p_GratuityYrs.Value = strGratuityYrs;

        SqlParameter p_PMONTH = cmd.Parameters.Add("PMONTH", SqlDbType.BigInt);
        p_PMONTH.Direction = ParameterDirection.Input;
        p_PMONTH.Value = strPMonth;

        SqlParameter p_PYEAR = cmd.Parameters.Add("PYEAR", SqlDbType.BigInt);
        p_PYEAR.Direction = ParameterDirection.Input;
        p_PYEAR.Value = strPYear;

        SqlParameter p_PMONTHAMT = cmd.Parameters.Add("PMONTHAMT", SqlDbType.BigInt);
        p_PMONTHAMT.Direction = ParameterDirection.Input;
        p_PMONTHAMT.Value = strPMonthAmt;

        SqlParameter p_CMONTH = cmd.Parameters.Add("CMONTH", SqlDbType.BigInt);
        p_CMONTH.Direction = ParameterDirection.Input;
        p_CMONTH.Value = strCMonth;

        SqlParameter p_CYEAR = cmd.Parameters.Add("CYEAR", SqlDbType.BigInt);
        p_CYEAR.Direction = ParameterDirection.Input;
        p_CYEAR.Value = strCYear;

        SqlParameter p_CMONTHAMT = cmd.Parameters.Add("CMONTHAMT", SqlDbType.BigInt);
        p_CMONTHAMT.Direction = ParameterDirection.Input;
        p_CMONTHAMT.Value = strCMonthAmt;

        SqlParameter p_CHARGINGAMT = cmd.Parameters.Add("CHARGINGAMT", SqlDbType.BigInt);
        p_CHARGINGAMT.Direction = ParameterDirection.Input;
        p_CHARGINGAMT.Value = strChargingAmt;

        SqlParameter p_INSERTEDBY = cmd.Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = strInsBy;

        SqlParameter p_INSERTEDDATE = cmd.Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDDATE.Direction = ParameterDirection.Input;
        p_INSERTEDDATE.Value = strInsDate;

        SqlParameter p_ISUPDATE = cmd.Parameters.Add("ISUPDATE", SqlDbType.Char);
        p_ISUPDATE.Direction = ParameterDirection.Input;
        p_ISUPDATE.Value = strIsUpdate;

        SqlParameter p_GratuityStatus = cmd.Parameters.Add("GratuityStatus", SqlDbType.Char);
        p_GratuityStatus.Direction = ParameterDirection.Input;
        p_GratuityStatus.Value = "Y";
        

        return cmd;
    }

    public DataTable GetEmpInfo(string strEmpID,string strMonth,string strFinYear)
    {
         string strSQL = "";
         if (strEmpID != "")
         {
             strSQL = "SELECT P.EMPID,E.FULLNAME,E.JOININGDATE,E.GRATUITYFROM,PD.PAYAMT AS BASICSAL,J.DESIGNAME,E.DESIGID,E.SEPARATEDATE"
                 + " FROM PAYSLIPMST P,PAYSLIPDETS PD, EMPINFO E,DESIGNATION J "
                 + " WHERE P.EMPID=E.EMPID AND P.PSBID=PD.PSBID AND P.EMPID=PD.EMPID "
                 + " AND PD.SHEADID=1 "
                 + " AND E.DESIGID=J.DESIGID AND P.EMPID=@EMPID "
                 + " AND P.VMONTH=@VMONTH and P.FISCALYRID=@FISCALYRID ";
         }
         else
         {
             strSQL = "SELECT P.EMPID,E.FULLNAME,E.JOININGDATE,E.GRATUITYFROM,PD.PAYAMT AS BASICSAL,J.DESIGNAME,E.DESIGID,E.SEPARATEDATE "
                 + " ROM PAYSLIPMST P,PAYSLIPDETS PD, EMPINFO E,DESIGNATION J "
                 + " WHERE P.EMPID=E.EMPID AND P.PSBID=PD.PSBID AND P.EMPID=PD.EMPID "
                 + " AND PD.SHEADID=1 "
                 + " AND E.DESIGID=J.DESIGID AND "
                 + " AND P.VMONTH=@VMONTH and P.FISCALYRID=@FISCALYRID ";
         }
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        if (strEmpID != "")
        {
            SqlParameter p_EmpID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = strEmpID;
        }
        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        return objDC.CreateDT(cmd, "GetEmpName");
    }

    public DataTable GetFinalPaymentData(string strMonth, string strYear, string strEmpID, string IsFromPayroll)
    {
        if (objDC.ds.Tables["GetFinalPaymentData"] != null)
        {
            objDC.ds.Tables["GetFinalPaymentData"].Rows.Clear();
            objDC.ds.Tables["GetFinalPaymentData"].Dispose();
        }

        SqlCommand command = new SqlCommand();
        command = new SqlCommand("proc_FinalPayment");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = command.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = command.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_EmpID = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        SqlParameter p_IsFromPayroll = command.Parameters.Add("IsFromPayroll", SqlDbType.Char);
        p_IsFromPayroll.Direction = ParameterDirection.Input;
        p_IsFromPayroll.Value = IsFromPayroll;


        objDC.CreateDSFromProc(command, "GetFinalPaymentData");
        return objDC.ds.Tables["GetFinalPaymentData"];
    }

    public DataTable GetLeaveEncashmentList(string strMonth, string strFinYear, string strEmpID)
    {
        if (objDC.ds.Tables["GetGrPaymentList"] != null)
        {
            objDC.ds.Tables["GetGrPaymentList"].Rows.Clear();
            objDC.ds.Tables["GetGrPaymentList"].Dispose();
        }

        SqlCommand command = new SqlCommand();
        command = new SqlCommand("Proc_Select_LeaveEncashment");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_GRATUITYMONTH = command.Parameters.Add("GRATUITYMONTH", SqlDbType.BigInt);
        p_GRATUITYMONTH.Direction = ParameterDirection.Input;
        p_GRATUITYMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID = command.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        SqlParameter p_EmpID = command.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        objDC.CreateDSFromProc(command, "GetGrPaymentList");
        return objDC.ds.Tables["GetGrPaymentList"];
    }

    public DataTable GetEmployeeForGratuity(string strMonth, string strFinYear,string strDate)
    {
        string strSQL = "";
        strSQL = "SELECT P.EMPID,P.EMPID,E.FULLNAME,E.JOININGDATE,E.GRATUITYFROM,E.BASICSALARY,J.DESIGNAME,E.DESIGID "
            + " FROM PAYSLIPMST P, EMPINFO E,DESIGNATION J "
            + " WHERE P.EMPID=E.EMPID AND E.DESIGID=J.DESIGID AND E.JOININGDATE<@JOININGDATE AND E.EMPSTATUS='A' AND E.EmpTypeId=1"
            + " AND P.VMONTH=@VMONTH and P.FISCALYRID=@FISCALYRID "
            + " ORDER BY P.EMPID";
        
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        
        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        SqlParameter p_JOININGDATE = cmd.Parameters.Add("JOININGDATE", SqlDbType.DateTime);
        p_JOININGDATE.Direction = ParameterDirection.Input;
        p_JOININGDATE.Value = strDate;

        return objDC.CreateDT(cmd, "GetEmployeeForGratuity");
    }

    public DataTable GetNewEmployeeForGratuity(string strMonth, string strFinYear, string strDateFrom,string strDateTo)
    {
        string strSQL = "";
        strSQL = "SELECT P.EMPID,P.EMPID,E.FULLNAME,E.JOININGDATE,E.JOININGDATE AS GRATUITYFROM,E.BASICSALARY,J.DESIGNAME,E.DESIGID "
            + " FROM PAYSLIPMST P, EMPINFO E,DESIGNATION J "
            + " WHERE P.EMPID=E.EMPID AND E.DESIGID=J.DESIGID "
            + " AND JOININGDATE BETWEEN @STARTDATE AND @ENDDATE "
            + " AND P.VMONTH=@VMONTH AND P.FISCALYRID=@FISCALYRID "
            + " ORDER BY E.EMPID ";
        
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        SqlParameter p_STARTDATE = cmd.Parameters.Add("STARTDATE", SqlDbType.DateTime);
        p_STARTDATE.Direction = ParameterDirection.Input;
        p_STARTDATE.Value = strDateFrom;

        SqlParameter p_ENDDATE = cmd.Parameters.Add("ENDDATE", SqlDbType.DateTime);
        p_ENDDATE.Direction = ParameterDirection.Input;
        p_ENDDATE.Value = strDateTo;

        return objDC.CreateDT(cmd, "GetNewEmployeeForGratuity");
    }

    public DataTable GetEmployeeWithNullGratuityDateExceptNewJoiner(string strMonth, string strFinYear, string strJoinDate, string strDateFrom, string strDateTo)
    {
        string strSQL = "";
        strSQL = " SELECT P.EMPID,P.EMPID,E.FULLNAME,E.JOININGDATE,E.GRATUITYFROM,E.BASICSALARY,J.DESIGNAME,E.DESIGID "
                + " FROM PAYSLIPMST P, EMPINFO E,DESIGNATION J "
                + " WHERE P.EMPID=E.EMPID AND E.DESIGID=J.DESIGID AND E.JOININGDATE<@JOININGDATE AND E.EMPSTATUS='A' "
                + " AND P.VMONTH=@VMONTH and P.FISCALYRID=@FISCALYRID AND E.GratuityFrom is Null "
                + " and P.EMPID not in( "
                + " SELECT P.EMPID "
                + " FROM PAYSLIPMST P, EMPINFO E,DESIGNATION J "
                + " WHERE P.EMPID=E.EMPID AND E.DESIGID=J.DESIGID "
                + " AND JOININGDATE BETWEEN @STARTDATE AND @ENDDATE "
                + " AND P.VMONTH=@VMONTH AND P.FISCALYRID=@FISCALYRID )";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        SqlParameter p_STARTDATE = cmd.Parameters.Add("STARTDATE", SqlDbType.DateTime);
        p_STARTDATE.Direction = ParameterDirection.Input;
        p_STARTDATE.Value = strDateFrom;

        SqlParameter p_ENDDATE = cmd.Parameters.Add("ENDDATE", SqlDbType.DateTime);
        p_ENDDATE.Direction = ParameterDirection.Input;
        p_ENDDATE.Value = strDateTo;

        SqlParameter p_JOININGDATE = cmd.Parameters.Add("JOININGDATE", SqlDbType.DateTime);
        p_JOININGDATE.Direction = ParameterDirection.Input;
        p_JOININGDATE.Value = strJoinDate;

        return objDC.CreateDT(cmd, "GetEmployeeWithNullGratuityDateExceptNewJoiner");
    }

    public DataTable GetLedgerData(string strFiscalYr, string strMonth, string strEmpID)
    {
        if (objDC.ds.Tables["GetLedgerData"] != null)
        {
            objDC.ds.Tables["GetLedgerData"].Rows.Clear();
            objDC.ds.Tables["GetLedgerData"].Dispose();
        }

        SqlCommand command = new SqlCommand();
        command = new SqlCommand("Proc_Payroll_Select_GratuityLedger");
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

        objDC.CreateDSFromProc(command, "GetLedgerData");
        return objDC.ds.Tables["GetLedgerData"];
    }

    public DataTable GetEmpWiseLedgerData(string strEmpID)
    {
        if (objDC.ds.Tables["GetEmpWiseLedgerData"] != null)
        {
            objDC.ds.Tables["GetEmpWiseLedgerData"].Rows.Clear();
            objDC.ds.Tables["GetEmpWiseLedgerData"].Dispose();
        }
        SqlCommand command = new SqlCommand();
        command = new SqlCommand("Proc_Payroll_Select_EmpWiseGratuityLedger");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpID = command.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        objDC.CreateDSFromProc(command, "GetEmpWiseLedgerData");
        return objDC.ds.Tables["GetEmpWiseLedgerData"];
    }

    public DataTable GetGratuityLedgerData(string strFiscalYr, string strMonth, string strEmpID)
    {
        if (objDC.ds.Tables["GetGratuityLedgerData"] != null)
        {
            objDC.ds.Tables["GetGratuityLedgerData"].Rows.Clear();
            objDC.ds.Tables["GetGratuityLedgerData"].Dispose();
        }
        SqlCommand command = new SqlCommand();
        command = new SqlCommand("SELECT GL.*,E.FULLNAME,J.DESIGNAME AS JOBTITLE FROM GRATUITYLEDGER GL,PAYSLIPMST PM,EMPINFO E,DESIGNATION J   "
                                   + " WHERE GL.EMPID=PM.EMPID AND PM.EMPID=E.EMPID AND PM.DESIGID=J.DESIGID "
                                   + " AND PM.VMONTH=@VMONTH AND PM.FISCALYRID=@FISCALYRID "
                                   + " AND GL.VMONTH=@VMONTH AND GL.FISCALYRID=@FISCALYRID ORDER BY GL.EMPID");
        command.CommandType = CommandType.Text;

        SqlParameter p_FISCALYRID = command.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYr;

        SqlParameter p_VMONTH = command.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

       return objDC.CreateDT(command, "GetGratuityLedgerData");
    }

    public string GetLastGLMonth(string strFinYear)
    {
        string strSQL = "SELECT Max(VMONTH) from GratuityLedger where FiscalYrID=@FiscalYrID";
        SqlCommand cmd = new SqlCommand(strSQL);

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        return objDC.GetScalarVal(cmd);
    }

    
    public void InsertGratuityPayment(string strTransID, string strEmpID, string strHrEmpID, string strDesgID, string strJoinDate,
        string strGrFrom, string strGrTo, string strNextGrFrom, string strGrMonth, string strGrYear, string strFinYear,
        string strBasic, string strGrAccrued, string strCurrGr, string strGrFraction, string strGrPayDate, string strPayAmt, string strRemarks,
        string strInsBy, string strInsDate)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Insert_GratuityPayment");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LEDGERID = cmd.Parameters.Add("TRANSID", SqlDbType.BigInt);
        p_LEDGERID.Direction = ParameterDirection.Input;
        p_LEDGERID.Value = strTransID;

        SqlParameter p_EmpID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        //SqlParameter p_HREMPID = cmd.Parameters.Add("HREMPID", SqlDbType.Char);
        //p_HREMPID.Direction = ParameterDirection.Input;
        //p_HREMPID.Value = strHrEmpID;

        SqlParameter p_JBTLID = cmd.Parameters.Add("DesigId", SqlDbType.BigInt);
        p_JBTLID.Direction = ParameterDirection.Input;
        p_JBTLID.Value = strDesgID;

        SqlParameter p_JOININGDATE = cmd.Parameters.Add("JOININGDATE", SqlDbType.DateTime);
        p_JOININGDATE.Direction = ParameterDirection.Input;
        p_JOININGDATE.Value = strJoinDate;

        SqlParameter p_GRATUITYFROM = cmd.Parameters.Add("GRATUITYFROM", SqlDbType.DateTime);
        p_GRATUITYFROM.Direction = ParameterDirection.Input;
        p_GRATUITYFROM.Value = strGrFrom;

        SqlParameter p_GRATUITYTO = cmd.Parameters.Add("GRATUITYTO", SqlDbType.DateTime);
        p_GRATUITYTO.Direction = ParameterDirection.Input;
        p_GRATUITYTO.Value = strGrTo;

        SqlParameter p_NEXTGRATUITYFROM = cmd.Parameters.Add("NEXTGRATUITYFROM", SqlDbType.DateTime);
        p_NEXTGRATUITYFROM.Direction = ParameterDirection.Input;
        p_NEXTGRATUITYFROM.Value = strNextGrFrom;

        SqlParameter p_GRATUITYMONTH = cmd.Parameters.Add("GRATUITYMONTH", SqlDbType.BigInt);
        p_GRATUITYMONTH.Direction = ParameterDirection.Input;
        p_GRATUITYMONTH.Value = strGrMonth;

        SqlParameter p_GRATUITYYEAR = cmd.Parameters.Add("GRATUITYYEAR", SqlDbType.BigInt);
        p_GRATUITYYEAR.Direction = ParameterDirection.Input;
        p_GRATUITYYEAR.Value = strGrYear;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        SqlParameter p_BASIC = cmd.Parameters.Add("BASICSALARY", SqlDbType.Decimal);
        p_BASIC.Direction = ParameterDirection.Input;
        p_BASIC.Value = strBasic;

        SqlParameter p_LASTGRATUITY = cmd.Parameters.Add("LASTGRATUITY", SqlDbType.Decimal);
        p_LASTGRATUITY.Direction = ParameterDirection.Input;
        p_LASTGRATUITY.Value = strGrAccrued;

        SqlParameter p_CURRGRATUITY = cmd.Parameters.Add("CURRGRATUITY", SqlDbType.Decimal);
        p_CURRGRATUITY.Direction = ParameterDirection.Input;
        p_CURRGRATUITY.Value = strCurrGr;

        SqlParameter p_GRATUITYFRACTION = cmd.Parameters.Add("GRATUITYFRACTION", SqlDbType.Decimal);
        p_GRATUITYFRACTION.Direction = ParameterDirection.Input;
        p_GRATUITYFRACTION.Value = strGrFraction;

        SqlParameter p_PAYDATE = cmd.Parameters.Add("PAYDATE", SqlDbType.DateTime);
        p_PAYDATE.Direction = ParameterDirection.Input;
        p_PAYDATE.Value = strGrPayDate;

        SqlParameter p_PAYAMT = cmd.Parameters.Add("PAYAMT", SqlDbType.Decimal);
        p_PAYAMT.Direction = ParameterDirection.Input;
        p_PAYAMT.Value = strPayAmt;

        SqlParameter p_REMARKS = cmd.Parameters.Add("REMARKS", SqlDbType.VarChar);
        p_REMARKS.Direction = ParameterDirection.Input;
        p_REMARKS.Value = strRemarks;

        SqlParameter p_INSERTEDBY = cmd.Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = strInsBy;

        SqlParameter p_INSERTEDDATE = cmd.Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDDATE.Direction = ParameterDirection.Input;
        p_INSERTEDDATE.Value = strInsDate;

        objDC.ExecuteQuery(cmd);
    }

     #region Gratuity Payment
    public void ExecuteGratuityProcess(string strMonth, string strYear, string strQuarter, string ProcessDate, string strFiscalYrId, string strEmpId)
    {
        SqlCommand cmd = new SqlCommand("proc_GratuityProcess");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Month = cmd.Parameters.Add("Month", SqlDbType.BigInt);
        p_Month.Direction = ParameterDirection.Input;
        p_Month.Value = strMonth;

        SqlParameter p_Year = cmd.Parameters.Add("Year", SqlDbType.BigInt);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = strYear;

        SqlParameter p_Quarter = cmd.Parameters.Add("Quarter", SqlDbType.BigInt);
        p_Quarter.Direction = ParameterDirection.Input;
        p_Quarter.Value = strQuarter;

        SqlParameter p_ProcessingDate = cmd.Parameters.Add("ProcessingDate", SqlDbType.DateTime);
        p_ProcessingDate.Direction = ParameterDirection.Input;
        p_ProcessingDate.Value = ProcessDate;

        SqlParameter p_FiscalYear = cmd.Parameters.Add("FiscalYear", SqlDbType.BigInt);
        p_FiscalYear.Direction = ParameterDirection.Input;
        p_FiscalYear.Value = strFiscalYrId;

        SqlParameter p_EmpId = cmd.Parameters.Add("EmployeeIdXML", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        objDC.ExecuteQuery(cmd);
    }
    public void DeleteGrPaymentData(string strTransID)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Delete_GratuityPayment");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LEDGERID = cmd.Parameters.Add("TRANSID", SqlDbType.BigInt);
        p_LEDGERID.Direction = ParameterDirection.Input;
        p_LEDGERID.Value = strTransID;
        
        objDC.ExecuteQuery(cmd);

    }

    public DataTable GetGrPaymentList(string strMonth, string strFinYear, string strEmpID)
    {
        if (objDC.ds.Tables["GetGrPaymentList"] != null)
        {
            objDC.ds.Tables["GetGrPaymentList"].Rows.Clear();
            objDC.ds.Tables["GetGrPaymentList"].Dispose();
        }
        SqlCommand command = new SqlCommand();
        command = new SqlCommand("Proc_Payroll_Select_GratuityPayment");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_GRATUITYMONTH = command.Parameters.Add("GRATUITYMONTH", SqlDbType.BigInt);
        p_GRATUITYMONTH.Direction = ParameterDirection.Input;
        p_GRATUITYMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID = command.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        SqlParameter p_EmpID = command.Parameters.Add("EMPID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        objDC.CreateDSFromProc(command, "GetGrPaymentList");
        return objDC.ds.Tables["GetGrPaymentList"];
    }

    public bool IsCurrentMonthPaymentExist(string strEmpID, string strMonth, string strFinYear)
    {
        string strSQL = "";
        strSQL = "SELECT GrtProcessID FROM GratuityProcess WHERE EMPID=@EMPID AND VMonth=@VMonth AND FiscalYrID=@FiscalYrID";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_GRATUITYMONTH = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_GRATUITYMONTH.Direction = ParameterDirection.Input;
        p_GRATUITYMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FiscalYrID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        SqlParameter p_EmpID = cmd.Parameters.Add("EMPID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;


        string strValue = objDC.GetScalarVal(cmd);
        if (string.IsNullOrEmpty(strValue) == true)
            return false;
        else
            return true;
    }
    #endregion

    public Payroll_GratuityLedgerManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
