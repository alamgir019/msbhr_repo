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
/// Summary description for Payroll_ITDepositRecords
/// </summary>
public class Payroll_ITDepositRecords
{
    DBConnector objDC = new DBConnector();

    public void InsertData(GridView grv, string strGrp, string strMonth, string strYear, string strFinYear, string strChallanNo,
    string strBank, string strChallanDate, string strInsBy, string strInsDate)
    {
        int i = 0;
        SqlCommand[] command = new SqlCommand[grv.Rows.Count + 1];
        long lnTransID = Convert.ToInt64(Common.getMaxId("ITDEPOSITRECORDS", "TRANSID"));
        // Delete existing Records
        command[i] = new SqlCommand("Proc_Payroll_Delete_ITDepositRecords");
        command[i].CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = command[i].Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = command[i].Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_FISCALYRID = command[i].Parameters.Add("TaxFiscalYrId", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;


        // Insert Data
        i++;
        foreach (GridViewRow gRow in grv.Rows)
        {
            command[i] = new SqlCommand("Proc_Payroll_Insert_ITDepositRecords");
            command[i].CommandType = CommandType.StoredProcedure;

            SqlParameter p_TRANSID = command[i].Parameters.Add("TRANSID", SqlDbType.BigInt);
            p_TRANSID.Direction = ParameterDirection.Input;
            p_TRANSID.Value = lnTransID;

            SqlParameter p_EMPID = command[i].Parameters.Add("EMPID", SqlDbType.Char);
            p_EMPID.Direction = ParameterDirection.Input;
            p_EMPID.Value = gRow.Cells[1].Text.Trim();

            SqlParameter p_DIVISIONID = command[i].Parameters.Add("SalLocId", SqlDbType.BigInt);
            p_DIVISIONID.Direction = ParameterDirection.Input;
            p_DIVISIONID.Value = grv.DataKeys[gRow.DataItemIndex].Values[1].ToString().Trim();

            SqlParameter p_VMONTH1 = command[i].Parameters.Add("VMONTH", SqlDbType.BigInt);
            p_VMONTH1.Direction = ParameterDirection.Input;
            p_VMONTH1.Value = strMonth;

            SqlParameter p_VYEAR1 = command[i].Parameters.Add("VYEAR", SqlDbType.BigInt);
            p_VYEAR1.Direction = ParameterDirection.Input;
            p_VYEAR1.Value = strYear;

            SqlParameter p_FISCALYRID1 = command[i].Parameters.Add("TaxFiscalYrId", SqlDbType.BigInt);
            p_FISCALYRID1.Direction = ParameterDirection.Input;
            p_FISCALYRID1.Value = strFinYear;

            SqlParameter p_SHEADID = command[i].Parameters.Add("SHEADID", SqlDbType.BigInt);
            p_SHEADID.Direction = ParameterDirection.Input;
            p_SHEADID.Value = 15;

            SqlParameter p_PAYAMT = command[i].Parameters.Add("PAYAMT", SqlDbType.Decimal);
            p_PAYAMT.Direction = ParameterDirection.Input;
            p_PAYAMT.Value = Common.RoundDecimal(gRow.Cells[6].Text.Trim(), 0);

            SqlParameter p_CHALLANNO = command[i].Parameters.Add("CHALLANNO", SqlDbType.VarChar);
            p_CHALLANNO.Direction = ParameterDirection.Input;
            p_CHALLANNO.Value = strChallanNo;

            SqlParameter p_BANKNAME = command[i].Parameters.Add("BANKNAME", SqlDbType.VarChar);
            p_BANKNAME.Direction = ParameterDirection.Input;
            p_BANKNAME.Value = strBank;

            SqlParameter p_CHALLANDATE = command[i].Parameters.Add("CHALLANDATE", SqlDbType.DateTime);
            p_CHALLANDATE.Direction = ParameterDirection.Input;
            p_CHALLANDATE.Value = strChallanDate;

            SqlParameter p_INSERTEDBY = command[i].Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
            p_INSERTEDBY.Direction = ParameterDirection.Input;
            p_INSERTEDBY.Value = strInsBy;

            SqlParameter p_INSERTEDDATE = command[i].Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
            p_INSERTEDDATE.Direction = ParameterDirection.Input;
            p_INSERTEDDATE.Value = strInsDate;

            lnTransID++;
            i++;
        }
        objDC.MakeTransaction(command);
    }

    public DataTable GetITRefundData(string strEmpID, string strFromYear)
    {
        SqlCommand cmd = new SqlCommand("PROC_PAYROLL_SELECT_ITREFUND");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        SqlParameter p_FROMYEAR = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FROMYEAR.Direction = ParameterDirection.Input;
        p_FROMYEAR.Value = strFromYear;

        objDC.CreateDSFromProc(cmd, "GetITRefundData");
        return objDC.ds.Tables["GetITRefundData"];
    }

    public void UpdateITInSalaryPackage(GridView grv, string strSheadID, string strInsBy, string strInsDate, string strLastUpdatedFrom)
    {
        SqlCommand[] cmd = new SqlCommand[grv.Rows.Count];
        int i = 0;

        foreach (GridViewRow gRow in grv.Rows)
        {
            cmd[i] = new SqlCommand("proc_Payroll_Update_SalaryPackDetls");
            cmd[i].CommandType = CommandType.StoredProcedure;

            SqlParameter p_SalPakId = cmd[i].Parameters.Add("SalPakId", SqlDbType.BigInt);
            p_SalPakId.Direction = ParameterDirection.Input;
            p_SalPakId.Value = grv.DataKeys[gRow.DataItemIndex].Values[1].ToString().Trim();

            SqlParameter p_SHeadId = cmd[i].Parameters.Add("SHeadId", SqlDbType.BigInt);
            p_SHeadId.Direction = ParameterDirection.Input;
            p_SHeadId.Value = strSheadID;

            SqlParameter p_PayAmt = cmd[i].Parameters.Add("PayAmt", SqlDbType.Decimal);
            p_PayAmt.Direction = ParameterDirection.Input;
            p_PayAmt.Value = Common.RoundDecimal(gRow.Cells[29].Text.Trim(), 0) * -1;
                //Common.RoundDecimal(gRow.Cells[28].Text.Trim(), 0) * -1;

            SqlParameter p_TotAmnt = cmd[i].Parameters.Add("TotAmnt", SqlDbType.Decimal);
            p_TotAmnt.Direction = ParameterDirection.Input;
            p_TotAmnt.Value = Common.RoundDecimal(gRow.Cells[29].Text.Trim(), 0) * -1;

            SqlParameter p_InsertedBy = cmd[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = strInsBy;

            SqlParameter p_UpdatedDate = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_UpdatedDate.Direction = ParameterDirection.Input;
            p_UpdatedDate.Value = strInsDate;

            SqlParameter p_LastUpdatedFrom = cmd[i].Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
            p_LastUpdatedFrom.Direction = ParameterDirection.Input;
            p_LastUpdatedFrom.Value = strLastUpdatedFrom;

            i++;
        }
        objDC.MakeTransaction(cmd);
    }

    public void InsertITCalculationEmailHistory(GridView grv, string strMonth, string strFinYear, string strInsBy, string strInsDate)
    {
        int i = 0;
        decimal dclTRI = 0;
        SqlCommand[] command = new SqlCommand[grv.Rows.Count + 2];
        long lnTransID = Convert.ToInt64(Common.getMaxId("EMPITCALCULATIONEMAILHISTORY", "TRANSID"));

        foreach (GridViewRow gRow in grv.Rows)
        {
            // Delete existing Records
            if (grv.Rows.Count == 1)
            {
                command[i] = new SqlCommand("PROC_PAYROLL_DELETE_EMPITCALCULATIONEMAILHISTORY");
                command[i].CommandType = CommandType.StoredProcedure;

                SqlParameter p_EMPID = command[i].Parameters.Add("EMPID", SqlDbType.Char);
                p_EMPID.Direction = ParameterDirection.Input;
                p_EMPID.Value = gRow.Cells[2].Text.Trim();

                SqlParameter p_VMONTH = command[i].Parameters.Add("VMONTH", SqlDbType.BigInt);
                p_VMONTH.Direction = ParameterDirection.Input;
                p_VMONTH.Value = strMonth;

                SqlParameter p_FISCALYRID = command[i].Parameters.Add("FISCALYRID", SqlDbType.BigInt);
                p_FISCALYRID.Direction = ParameterDirection.Input;
                p_FISCALYRID.Value = strFinYear;

                SqlParameter p_EMPWISE = command[i].Parameters.Add("EMPWISE", SqlDbType.Char);
                p_EMPWISE.Direction = ParameterDirection.Input;
                p_EMPWISE.Value = "Y";

                i++;
            }

            // Insert Data           

            command[i] = new SqlCommand("PROC_PAYROLL_INSERT_EMPITCALCULATIONEMAILHISTORY");
            command[i].CommandType = CommandType.StoredProcedure;

            SqlParameter p_TRANSID = command[i].Parameters.Add("TRANSID", SqlDbType.BigInt);
            p_TRANSID.Direction = ParameterDirection.Input;
            p_TRANSID.Value = lnTransID;

            SqlParameter p_EMPID1 = command[i].Parameters.Add("EMPID", SqlDbType.Char);
            p_EMPID1.Direction = ParameterDirection.Input;
            p_EMPID1.Value = gRow.Cells[2].Text.Trim();

            SqlParameter p_DIVID = command[i].Parameters.Add("PostingDivId", SqlDbType.BigInt);
            p_DIVID.Direction = ParameterDirection.Input;
            p_DIVID.Value = grv.DataKeys[gRow.DataItemIndex].Values[2].ToString().Trim();

            SqlParameter p_DEPTID = command[i].Parameters.Add("DEPTID", SqlDbType.BigInt);
            p_DEPTID.Direction = ParameterDirection.Input;
            p_DEPTID.Value = grv.DataKeys[gRow.DataItemIndex].Values[3].ToString().Trim();

            SqlParameter p_VMONTH1 = command[i].Parameters.Add("VMONTH", SqlDbType.BigInt);
            p_VMONTH1.Direction = ParameterDirection.Input;
            p_VMONTH1.Value = strMonth;

            SqlParameter p_FISCALYRID1 = command[i].Parameters.Add("FISCALYRID", SqlDbType.BigInt);
            p_FISCALYRID1.Direction = ParameterDirection.Input;
            p_FISCALYRID1.Value = strFinYear;

            SqlParameter p_TRI = command[i].Parameters.Add("TRI", SqlDbType.Decimal);
            p_TRI.Direction = ParameterDirection.Input;
            dclTRI = (Convert.ToDecimal(gRow.Cells[18].Text.Trim()) * 20) / 100;
            p_TRI.Value = dclTRI.ToString();

            SqlParameter p_PFC = command[i].Parameters.Add("PFC", SqlDbType.Decimal);
            p_PFC.Direction = ParameterDirection.Input;
            p_PFC.Value = Common.RoundDecimal(gRow.Cells[20].Text.Trim(), 0) * 2;

            SqlParameter p_OIR = command[i].Parameters.Add("OIR", SqlDbType.Decimal);
            p_OIR.Direction = ParameterDirection.Input;
            p_OIR.Value = Convert.ToString(Convert.ToDecimal(p_TRI.Value) - Convert.ToDecimal(p_PFC.Value));

            SqlParameter p_REMINDATE = command[i].Parameters.Add("REMINDATE", DBNull.Value);
            p_REMINDATE.Direction = ParameterDirection.Input;
            p_REMINDATE.IsNullable = true;
            p_REMINDATE.Value = strInsDate;

            SqlParameter p_SENDINGDATE = command[i].Parameters.Add("SENDINGDATE", DBNull.Value);
            p_SENDINGDATE.Direction = ParameterDirection.Input;
            p_SENDINGDATE.IsNullable = true;
            p_SENDINGDATE.Value = strInsDate;

            SqlParameter p_INSERTEDBY = command[i].Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
            p_INSERTEDBY.Direction = ParameterDirection.Input;
            p_INSERTEDBY.Value = strInsBy;

            SqlParameter p_INSERTEDDATE = command[i].Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
            p_INSERTEDDATE.Direction = ParameterDirection.Input;
            p_INSERTEDDATE.Value = strInsDate;

            SqlParameter p_STATUS = command[i].Parameters.Add("STATUS", SqlDbType.BigInt);
            p_STATUS.Direction = ParameterDirection.Input;
            p_STATUS.Value = 0;

            SqlParameter p_ISUPDATE = command[i].Parameters.Add("ISUPDATE", SqlDbType.Char);
            p_ISUPDATE.Direction = ParameterDirection.Input;
            p_ISUPDATE.Value = "N";

            lnTransID++;
            i++;
        }
        objDC.MakeTransaction(command);
    }

    public void InsertITCalculationData(GridView grv, string strMonth, string strFinYear, string strInsBy, string strInsDate, string strAssYear)
    {
        int i = 0;
        SqlCommand[] command = new SqlCommand[grv.Rows.Count * 2];
        long lnTransID = Convert.ToInt64(Common.getMaxId("EMPITCALCULATION", "TRANSID"));

        foreach (GridViewRow gRow in grv.Rows)
        {
            // Delete existing Records
            command[i] = new SqlCommand("PROC_PAYROLL_DELETE_EMPITCALCULATION");
            command[i].CommandType = CommandType.StoredProcedure;

            SqlParameter p_EMPID = command[i].Parameters.Add("EMPID", SqlDbType.Char);
            p_EMPID.Direction = ParameterDirection.Input;
            p_EMPID.Value = gRow.Cells[2].Text.Trim();

            SqlParameter p_VMONTH = command[i].Parameters.Add("VMONTH", SqlDbType.BigInt);
            p_VMONTH.Direction = ParameterDirection.Input;
            p_VMONTH.Value = strMonth;

            SqlParameter p_FISCALYRID = command[i].Parameters.Add("FISCALYRID", SqlDbType.BigInt);
            p_FISCALYRID.Direction = ParameterDirection.Input;
            p_FISCALYRID.Value = strFinYear;


            // Insert Data
            i++;

            command[i] = new SqlCommand("PROC_PAYROLL_INSERT_EMPITCALCULATION");
            command[i].CommandType = CommandType.StoredProcedure;

            SqlParameter p_TRANSID = command[i].Parameters.Add("TRANSID", SqlDbType.BigInt);
            p_TRANSID.Direction = ParameterDirection.Input;
            p_TRANSID.Value = lnTransID;

            SqlParameter p_EMPID1 = command[i].Parameters.Add("EMPID", SqlDbType.Char);
            p_EMPID1.Direction = ParameterDirection.Input;
            p_EMPID1.Value = gRow.Cells[2].Text.Trim();

            SqlParameter p_FULLNAME = command[i].Parameters.Add("FULLNAME", SqlDbType.VarChar);
            p_FULLNAME.Direction = ParameterDirection.Input;
            p_FULLNAME.Value = Common.CheckNullString(grv.DataKeys[gRow.RowIndex].Values[4].ToString().Trim());
                //Common.CheckNullString(gRow.Cells[3].Text.Trim());

            SqlParameter p_JOBTITLE = command[i].Parameters.Add("JOBTITLE", SqlDbType.VarChar);
            p_JOBTITLE.Direction = ParameterDirection.Input;
            p_JOBTITLE.Value = Common.CheckNullString(grv.DataKeys[gRow.RowIndex].Values[5].ToString().Trim());

            SqlParameter p_TINNO = command[i].Parameters.Add("TINNO", SqlDbType.VarChar);
            p_TINNO.Direction = ParameterDirection.Input;
            p_TINNO.Value = Common.CheckNullString(grv.DataKeys[gRow.RowIndex].Values[6].ToString().Trim());

            SqlParameter p_SEX = command[i].Parameters.Add("Gender", SqlDbType.VarChar);
            p_SEX.Direction = ParameterDirection.Input;
            p_SEX.Value = gRow.Cells[6].Text.Trim();

            SqlParameter p_JOININGDATE = command[i].Parameters.Add("JOININGDATE", SqlDbType.DateTime);
            p_JOININGDATE.Direction = ParameterDirection.Input;
            p_JOININGDATE.Value = Common.ReturnDate(gRow.Cells[8].Text.Trim());

            SqlParameter p_VMONTH1 = command[i].Parameters.Add("VMONTH", SqlDbType.BigInt);
            p_VMONTH1.Direction = ParameterDirection.Input;
            p_VMONTH1.Value = strMonth;

            SqlParameter p_FISCALYRID1 = command[i].Parameters.Add("FISCALYRID", SqlDbType.BigInt);
            p_FISCALYRID1.Direction = ParameterDirection.Input;
            p_FISCALYRID1.Value = strFinYear;

            SqlParameter p_YBasicSalary = command[i].Parameters.Add("YBasicSalary", SqlDbType.Decimal);
            p_YBasicSalary.Direction = ParameterDirection.Input;
            p_YBasicSalary.Value = gRow.Cells[9].Text.Trim();

            SqlParameter p_YHouseRent = command[i].Parameters.Add("YHouseRent", SqlDbType.Decimal);
            p_YHouseRent.Direction = ParameterDirection.Input;
            p_YHouseRent.Value = gRow.Cells[10].Text.Trim();

            SqlParameter p_T_HA = command[i].Parameters.Add("T_HA", SqlDbType.Decimal);
            p_T_HA.Direction = ParameterDirection.Input;
            p_T_HA.Value = gRow.Cells[11].Text.Trim();

            SqlParameter p_YMedicalAllowance = command[i].Parameters.Add("YMedicalAllowance", SqlDbType.Decimal);
            p_YMedicalAllowance.Direction = ParameterDirection.Input;
            p_YMedicalAllowance.Value = gRow.Cells[33].Text.Trim();

            SqlParameter p_T_MA = command[i].Parameters.Add("T_MA", SqlDbType.Decimal);
            p_T_MA.Direction = ParameterDirection.Input;
            p_T_MA.Value = gRow.Cells[34].Text.Trim();

            SqlParameter p_YTransportAllowance = command[i].Parameters.Add("YTransportAllowance", SqlDbType.Decimal);
            p_YTransportAllowance.Direction = ParameterDirection.Input;
            p_YTransportAllowance.Value = gRow.Cells[12].Text.Trim();

            SqlParameter p_T_TA = command[i].Parameters.Add("T_TA", SqlDbType.Decimal);
            p_T_TA.Direction = ParameterDirection.Input;
            p_T_TA.Value = gRow.Cells[13].Text.Trim();

            SqlParameter p_YFieldAllowance = command[i].Parameters.Add("YFieldAllowance", SqlDbType.Decimal);
            p_YFieldAllowance.Direction = ParameterDirection.Input;
            p_YFieldAllowance.Value = 0;
                //gRow.Cells[15].Text.Trim();

            SqlParameter p_YFestivalBonus = command[i].Parameters.Add("YFestivalBonus", SqlDbType.Decimal);
            p_YFestivalBonus.Direction = ParameterDirection.Input;
            p_YFestivalBonus.Value = gRow.Cells[14].Text.Trim();

            SqlParameter p_YOtherallowance = command[i].Parameters.Add("YOtherallowance", SqlDbType.Decimal);
            p_YOtherallowance.Direction = ParameterDirection.Input;
            p_YOtherallowance.Value = gRow.Cells[15].Text.Trim();

            SqlParameter p_TTI_1 = command[i].Parameters.Add("TTI_1", SqlDbType.Decimal);
            p_TTI_1.Direction = ParameterDirection.Input;
            p_TTI_1.Value = gRow.Cells[16].Text.Trim();

            SqlParameter p_Rebate = command[i].Parameters.Add("Rebate", SqlDbType.Decimal);
            p_Rebate.Direction = ParameterDirection.Input;
            p_Rebate.Value = gRow.Cells[17].Text.Trim();

            SqlParameter p_YPFDeduction = command[i].Parameters.Add("YPFDeduction", SqlDbType.Decimal);
            p_YPFDeduction.Direction = ParameterDirection.Input;
            p_YPFDeduction.Value = gRow.Cells[18].Text.Trim();

            SqlParameter p_TTI_2 = command[i].Parameters.Add("TTI_2", SqlDbType.Decimal);
            p_TTI_2.Direction = ParameterDirection.Input;
            p_TTI_2.Value = gRow.Cells[19].Text.Trim();

            SqlParameter p_Z_M_F = command[i].Parameters.Add("Z_M_F", SqlDbType.Decimal);
            p_Z_M_F.Direction = ParameterDirection.Input;
            p_Z_M_F.Value = gRow.Cells[20].Text.Trim();

            SqlParameter p_P10 = command[i].Parameters.Add("P10", SqlDbType.Decimal);
            p_P10.Direction = ParameterDirection.Input;
            p_P10.Value = gRow.Cells[21].Text.Trim();

            SqlParameter p_P15 = command[i].Parameters.Add("P15", SqlDbType.Decimal);
            p_P15.Direction = ParameterDirection.Input;
            p_P15.Value = gRow.Cells[22].Text.Trim();

            SqlParameter p_P20 = command[i].Parameters.Add("P20", SqlDbType.Decimal);
            p_P20.Direction = ParameterDirection.Input;
            p_P20.Value = gRow.Cells[23].Text.Trim();

            SqlParameter p_P25 = command[i].Parameters.Add("P25", SqlDbType.Decimal);
            p_P25.Direction = ParameterDirection.Input;
            p_P25.Value = gRow.Cells[24].Text.Trim();

            SqlParameter p_P30 = command[i].Parameters.Add("P30", SqlDbType.Decimal);
            p_P30.Direction = ParameterDirection.Input;
            p_P30.Value = gRow.Cells[25].Text.Trim();

            SqlParameter p_G_Tax = command[i].Parameters.Add("G_Tax", SqlDbType.Decimal);
            p_G_Tax.Direction = ParameterDirection.Input;
            p_G_Tax.Value = gRow.Cells[26].Text.Trim();

            SqlParameter p_NetTax = command[i].Parameters.Add("NetTax", SqlDbType.Decimal);
            p_NetTax.Direction = ParameterDirection.Input;
            p_NetTax.Value = gRow.Cells[27].Text.Trim();

            SqlParameter p_ITDeposited = command[i].Parameters.Add("ITDeposited", SqlDbType.Decimal);
            p_ITDeposited.Direction = ParameterDirection.Input;
            p_ITDeposited.Value = gRow.Cells[30].Text.Trim();

            SqlParameter p_Demand = command[i].Parameters.Add("Demand", SqlDbType.Decimal);
            p_Demand.Direction = ParameterDirection.Input;
            p_Demand.Value = gRow.Cells[31].Text.Trim();


            SqlParameter p_Refund = command[i].Parameters.Add("Refund", SqlDbType.Decimal);
            p_Refund.Direction = ParameterDirection.Input;
            p_Refund.Value = gRow.Cells[32].Text.Trim();

            SqlParameter p_INSERTEDBY = command[i].Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
            p_INSERTEDBY.Direction = ParameterDirection.Input;
            p_INSERTEDBY.Value = strInsBy;

            SqlParameter p_INSERTEDDATE = command[i].Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
            p_INSERTEDDATE.Direction = ParameterDirection.Input;
            p_INSERTEDDATE.Value = strInsDate;

            SqlParameter p_ISUPDATE = command[i].Parameters.Add("ISUPDATE", SqlDbType.Char);
            p_ISUPDATE.Direction = ParameterDirection.Input;
            p_ISUPDATE.Value = "N";

            SqlParameter p_LastYrRefund = command[i].Parameters.Add("LastYrRefund", SqlDbType.Decimal);
            p_LastYrRefund.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(gRow.Cells[28].Text.Trim()) == false)
                p_LastYrRefund.Value = gRow.Cells[28].Text.Trim();
            else
                p_LastYrRefund.Value = 0;

            SqlParameter p_MonthlyTax = command[i].Parameters.Add("MonthlyTax", SqlDbType.Decimal);
            p_MonthlyTax.Direction = ParameterDirection.Input;
            p_MonthlyTax.Value = gRow.Cells[29].Text.Trim();

            SqlParameter p_AssYear = command[i].Parameters.Add("AssYear", SqlDbType.VarChar);
            p_AssYear.Direction = ParameterDirection.Input;
            p_AssYear.Value = strAssYear;

            lnTransID++;
            i++;
        }
        objDC.MakeTransaction(command);
    }

    public DataTable GetEmployeeITData(string strGenerateFor, string strGeneratValue, string strMonth, string strYear,string strEmpTypeId)
    {
        SqlCommand cmd = new SqlCommand("proc_payroll_select_ITDepositRecords");
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

        SqlParameter p_EmpTypeId = cmd.Parameters.Add("EmpTypeId", SqlDbType.BigInt);
        p_EmpTypeId.Direction = ParameterDirection.Input;
        p_EmpTypeId.Value = strEmpTypeId;

        objDC.CreateDSFromProc(cmd, "GetEmployeeITData");
        return objDC.ds.Tables["GetEmployeeITData"];
    }

    public DataTable GetITDepositedData( string strEmpID,string strFromYear)
    {
        SqlCommand cmd = new SqlCommand("PROC_PAYROLL_SELECT_FISCALYEARWISEITDEPOSITEDDATA");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FROMYEAR = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FROMYEAR.Direction = ParameterDirection.Input;
        p_FROMYEAR.Value = strFromYear;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        objDC.CreateDSFromProc(cmd, "GetITDepositedDataFY");
        return objDC.ds.Tables["GetITDepositedDataFY"];
    }

    public DataTable GetEmployeeForITCalculation(string strEmpID, string strMonth, string strFinYear)
    {
        SqlCommand cmd = new SqlCommand("PROC_PAYROLL_SELECT_EMPFORITCALCULATION");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.VarChar);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        objDC.CreateDSFromProc(cmd, "GetEMPForITCalculation");
        return objDC.ds.Tables["GetEMPForITCalculation"];
    }

    public DataTable GetSalaryDataForITCalculation(string strFisYrId,string strEmpID )
    {
        SqlCommand cmd = new SqlCommand("PROC_PAYROLL_SELECT_FISCALYEARWISESALARYDATA");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFisYrId;
        
        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;
       
        objDC.CreateDSFromProc(cmd, "GetSalaryDataForITCalculation");
        return objDC.ds.Tables["GetSalaryDataForITCalculation"];
    }

    public DataTable GetBonusAllowanceYearly(string strFisYr)
    {
        SqlCommand cmd = new SqlCommand("PROC_PAYROLL_SELECT_BONUSALLOWFORYTD_YEARLY");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FiscalYear", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFisYr;

        objDC.CreateDSFromProc(cmd, "GetBonusAllowanceYearly");
        return objDC.ds.Tables["GetBonusAllowanceYearly"];
    }

    public DataTable GetITCalculationReportData(string strEmpID, string strMonth, string strFinYear)
    {
        SqlCommand cmd = new SqlCommand("PROC_PAYROLL_SELECT_EMPITCALCULATIONDATA");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        objDC.CreateDSFromProc(cmd, "GetITCalculationReportData");
        return objDC.ds.Tables["GetITCalculationReportData"];
    }

    public DataTable GetExistingData(string strDiv, string strMonth, string strYear, string strFinYear,string strEmpType)
    {
        if (objDC.ds.Tables["GetExistingData"] != null)
        {
            objDC.ds.Tables["GetExistingData"].Rows.Clear();
            objDC.ds.Tables["GetExistingData"].Dispose();
        }
        string strSQL = "";
        //string strSQL = "SELECT DISTINCT CHALLANNO,BANKNAME,CHALLANDATE FROM ITDEPOSITRECORDS WHERE EmpGrpID=@EmpGrpID AND VMONTH=@VMONTH AND VYEAR=@VYEAR AND FISCALYRID=@FISCALYRID";
        if (strSQL != "")
            strSQL = "SELECT DISTINCT ITD.CHALLANNO,ITD.BANKNAME,ITD.CHALLANDATE FROM ITDEPOSITRECORDS ITD, EmpInfo E WHERE ITD.EmpId=E.EmpId"
                + " AND ITD.PostingDivId=@PostingDivId AND ITD.VMONTH=@VMONTH AND ITD.VYEAR=@VYEAR AND ITD.TAXFISCALYRID=@TAXFISCALYRID AND E.EmpTypeId=@EmpTypeId";
        else
            strSQL = "SELECT DISTINCT ITD.CHALLANNO,ITD.BANKNAME,ITD.CHALLANDATE FROM ITDEPOSITRECORDS ITD, EmpInfo E WHERE ITD.EmpId=E.EmpId"
                + " AND ITD.VMONTH=@VMONTH AND ITD.VYEAR=@VYEAR AND ITD.TAXFISCALYRID=@TAXFISCALYRID AND E.EmpTypeId=@EmpTypeId";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_DIVISIONID = cmd.Parameters.Add("PostingDivId", SqlDbType.BigInt);
        p_DIVISIONID.Direction = ParameterDirection.Input;
        p_DIVISIONID.Value = strDiv;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("TAXFISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        SqlParameter p_EmpTypeId = cmd.Parameters.Add("EmpTypeId", SqlDbType.BigInt);
        p_EmpTypeId.Direction = ParameterDirection.Input;
        p_EmpTypeId.Value = strEmpType;

        objDC.CreateDT(cmd, "GetExistingData");
        return objDC.ds.Tables["GetExistingData"];
    }

    public DataTable GetDistinctEmpoyeeData(string strLocation, string strFinYear, string strGenFor)
    {
        if (objDC.ds.Tables["GetReporteEmpoyeeData"] != null)
        {
            objDC.ds.Tables["GetReporteEmpoyeeData"].Rows.Clear();
            objDC.ds.Tables["GetReporteEmpoyeeData"].Dispose();
        }

        string strSQL = "";

        if (strLocation != "-1")
            strSQL = "SELECT DISTINCT ITD.EMPID,E.FULLNAME,J.JobTitleName,PD.ClinicName AS PostingDivName,E.JOININGDATE"
                    + " FROM EMPINFO E"
                    + " JOIN ITDEPOSITRECORDS ITD ON ITD.EMPID = E.EMPID"
                    + " LEFT OUTER JOIN JOBTITLE J ON E.JobTitleId = J.JobTitleId"
                    + " LEFT OUTER JOIN ClinicList PD ON E.ClinicId = PD.ClinicId"
                    + " WHERE ITD.PostingDivId=@PostingDivId AND ITD.TAXFISCALYRID=@TAXFISCALYRID ORDER BY ITD.EMPID";
        else
            strSQL = "SELECT DISTINCT ITD.EMPID,E.FULLNAME,J.JobTitleName,PD.ClinicName AS PostingDivName,E.JOININGDATE"
                + " FROM EMPINFO E"
                + " JOIN ITDEPOSITRECORDS ITD ON ITD.EMPID = E.EMPID"
                + " LEFT OUTER JOIN JOBTITLE J ON E.JobTitleId = J.JobTitleId"
                + " LEFT OUTER JOIN ClinicList PD ON E.ClinicId = PD.ClinicId"
                + " WHERE ITD.TAXFISCALYRID=@TAXFISCALYRID ORDER BY ITD.EMPID";
        
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_DIVISIONID = cmd.Parameters.Add("PostingDivId", SqlDbType.BigInt);
        p_DIVISIONID.Direction = ParameterDirection.Input;
        p_DIVISIONID.Value = strLocation;


        SqlParameter p_FISCALYRID = cmd.Parameters.Add("TAXFISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        objDC.CreateDT(cmd, "GetReporteEmpoyeeData");
        return objDC.ds.Tables["GetReporteEmpoyeeData"];
    }

    public DataTable GetDistinctEmpoyeeDataAll( string strFinYear)
    {
        if (objDC.ds.Tables["GetDistinctEmpoyeeDataAll"] != null)
        {
            objDC.ds.Tables["GetDistinctEmpoyeeDataAll"].Rows.Clear();
            objDC.ds.Tables["GetDistinctEmpoyeeDataAll"].Dispose();
        }

        string strSQL = "";
      
            strSQL = "SELECT DISTINCT ITD.EMPID,E.FULLNAME,J.JOBTITLE,D.DIVISIONNAME,E.JOININGDATE "
                    + " FROM ITDEPOSITRECORDS ITD,EMPINFO E , JOBTITLE J, DIVISIONLIST D "
                    + " WHERE ITD.EMPID=E.EMPID AND E.DESGID=J.JBTLID AND E.DIVISIONID=D.DIVISIONID "
                    + " AND ITD.FISCALYRID=@FISCALYRID ORDER BY ITD.EMPID ";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        objDC.CreateDT(cmd, "GetDistinctEmpoyeeDataAll");
        return objDC.ds.Tables["GetDistinctEmpoyeeDataAll"];

    }

    public DataTable GetDistinctDate(string strLocation, string strFinYear, string strGenFor)
    {
        if (objDC.ds.Tables["GetDistinctDate"] != null)
        {
            objDC.ds.Tables["GetDistinctDate"].Rows.Clear();
            objDC.ds.Tables["GetDistinctDate"].Dispose();
        }

        string strSQL = "";

        if (strLocation != "-1")
            strSQL = "SELECT DISTINCT CHALLANDATE,CHALLANNO FROM ITDEPOSITRECORDS WHERE PostingDivId=@PostingDivId AND TAXFISCALYRID=@TAXFISCALYRID ORDER BY CHALLANDATE DESC";
        else
            strSQL = "SELECT DISTINCT CHALLANDATE,CHALLANNO FROM ITDEPOSITRECORDS WHERE TAXFISCALYRID=@TAXFISCALYRID ORDER BY CHALLANDATE DESC";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_DIVISIONID = cmd.Parameters.Add("PostingDivId", SqlDbType.BigInt);
        p_DIVISIONID.Direction = ParameterDirection.Input;
        p_DIVISIONID.Value = strLocation;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("TAXFISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        objDC.CreateDT(cmd, "GetDistinctDate");
        return objDC.ds.Tables["GetDistinctDate"];

    }

    public DataTable GetDistinctDateAll(string strFinYear)
    {
        if (objDC.ds.Tables["GetDistinctDateAll"] != null)
        {
            objDC.ds.Tables["GetDistinctDateAll"].Rows.Clear();
            objDC.ds.Tables["GetDistinctDateAll"].Dispose();
        }

        string strSQL = "";

        
            strSQL = "SELECT DISTINCT CHALLANDATE,CHALLANNO FROM ITDEPOSITRECORDS WHERE FISCALYRID=@FISCALYRID ORDER BY CHALLANDATE DESC";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        objDC.CreateDT(cmd, "GetDistinctDateAll");
        return objDC.ds.Tables["GetDistinctDateAll"];

    }

    public DataTable GetDetailsData(string strLocation, string strFinYear, string strGenFor)
    {
        if (objDC.ds.Tables["GetDetailsData"] != null)
        {
            objDC.ds.Tables["GetDetailsData"].Rows.Clear();
            objDC.ds.Tables["GetDetailsData"].Dispose();
        }

        string strSQL="";

        if (strLocation != "-1")
            strSQL = "SELECT EMPID,CHALLANDATE,PAYAMT,CHALLANNO,TAXFISCALYRID "
                + " FROM ITDEPOSITRECORDS WHERE PostingDivId=@PostingDivId AND TAXFISCALYRID=@TAXFISCALYRID ORDER BY EMPID,CHALLANDATE";
        else
            strSQL = "SELECT EMPID,CHALLANDATE,PAYAMT,CHALLANNO,TAXFISCALYRID "
                + " FROM ITDEPOSITRECORDS WHERE TAXFISCALYRID=@TAXFISCALYRID ORDER BY EMPID,CHALLANDATE";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_DIVISIONID = cmd.Parameters.Add("PostingDivId", SqlDbType.BigInt);
        p_DIVISIONID.Direction = ParameterDirection.Input;
        p_DIVISIONID.Value = strLocation;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("TAXFISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        objDC.CreateDT(cmd, "GetDetailsData");
        return objDC.ds.Tables["GetDetailsData"];

    }

    public DataTable GetDetailsDataAll(string strFinYear)
    {
        if (objDC.ds.Tables["GetDetailsDataAll"] != null)
        {
            objDC.ds.Tables["GetDetailsDataAll"].Rows.Clear();
            objDC.ds.Tables["GetDetailsDataAll"].Dispose();
        }

        string strSQL = "";
        
            strSQL = "SELECT EMPID,CHALLANDATE,PAYAMT,CHALLANNO,FISCALYRID "
                + " FROM ITDEPOSITRECORDS WHERE FISCALYRID=@FISCALYRID ORDER BY EMPID,CHALLANDATE";
        

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        objDC.CreateDT(cmd, "GetDetailsDataAll");
        return objDC.ds.Tables["GetDetailsDataAll"];

    }

    public DataTable GetDistinctRefundSlot()
    {
        string strSQL = "SELECT DISTINCT SLOT FROM ITIRPolicy ORDER BY Slot";
        SqlCommand cmd = new SqlCommand(strSQL);

        return objDC.CreateDT(cmd, "GetDistinctRefundSlot");
    }
    public DataTable GetSlotWiseRefundPlcData(decimal iSlot)
    {
        if (objDC.ds.Tables["GetSlotWiseRefundPlcData"] != null)
        {
            objDC.ds.Tables["GetSlotWiseRefundPlcData"].Rows.Clear();
            objDC.ds.Tables["GetSlotWiseRefundPlcData"].Dispose();
        }

        string strSQL = "SELECT * FROM ITIRPolicy WHERE Slot=@Slot ORDER BY SLNo";
        SqlCommand cmd = new SqlCommand(strSQL);

        SqlParameter p_Slot = cmd.Parameters.Add("Slot", SqlDbType.BigInt);
        p_Slot.Direction = ParameterDirection.Input;
        p_Slot.Value = iSlot;

        return objDC.CreateDT(cmd, "GetSlotWiseRefundPlcData");
    }
	public Payroll_ITDepositRecords()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
