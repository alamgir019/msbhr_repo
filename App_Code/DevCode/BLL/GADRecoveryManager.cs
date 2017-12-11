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
/// Summary description for GADRecoveryManager
/// </summary>
public class GADRecoveryManager
{
    DBConnector objDC = new DBConnector();

    public void AddGADRecoverPlanning(string strFinYear, string strEmpID, string strGAD,
        string strJul, string strIsJulAmt, string strAug, string strIsAugAmt, string strSep, string strIsSepAmt,
        string strOct, string strIsOctAmt, string strNov, string strIsNovAmt, string strDec, string strIsDecAmt,
        string strJan, string strIsJanAmt, string strFeb, string strIsFebAmt, string strMar, string strIsMarAmt,
        string strApr, string strIsAprAmt, string strMay, string strIsMayAmt, string strJun, string strIsJunAmt,
        string strInsBy, string strInsDate, string strIsUpdate,string strAccLine)
    {
        SqlCommand cmd = this.InsertGADRecoverPlanning(strFinYear, strEmpID, strGAD,
        strJul, strIsJulAmt, strAug, strIsAugAmt, strSep, strIsSepAmt,
        strOct, strIsOctAmt, strNov, strIsNovAmt, strDec, strIsDecAmt,
        strJan, strIsJanAmt, strFeb, strIsFebAmt, strMar, strIsMarAmt,
        strApr, strIsAprAmt, strMay, strIsMayAmt, strJun, strIsJunAmt,
        strInsBy, strInsDate, strIsUpdate, "", strAccLine);

        objDC.ExecuteQuery(cmd);
    }

    public void UpdateCostRecoveryPlanning(GridView gr, string strFinYear, string strEmpID, string strInsBy, string strInsDate)
    {
        int i=0;
        SqlCommand[] command = new SqlCommand[gr.Rows.Count];
        foreach (GridViewRow gRow in gr.Rows)
        {
            command[i] = this.InsertGADRecoverPlanning(strFinYear, strEmpID, gRow.Cells[1].Text.Trim(),
                Common.GetGridControlValue(gRow, 3, "txtJul"),
                Common.GetGridControlValue(gRow, 3, "hfJul"),
                Common.GetGridControlValue(gRow, 4, "txtAug"),
                Common.GetGridControlValue(gRow, 4, "hfAug"),
                Common.GetGridControlValue(gRow, 5, "txtSep"),
                Common.GetGridControlValue(gRow, 5, "hfSep"),
                Common.GetGridControlValue(gRow, 6, "txtOct"),
                Common.GetGridControlValue(gRow, 6, "hfOct"),
                Common.GetGridControlValue(gRow, 7, "txtNov"),
                Common.GetGridControlValue(gRow, 7, "hfNov"),
                Common.GetGridControlValue(gRow, 8, "txtDec"),
                Common.GetGridControlValue(gRow, 8, "hfDec"),
                Common.GetGridControlValue(gRow, 9, "txtJan"),
                Common.GetGridControlValue(gRow, 9, "hfJan"),
                Common.GetGridControlValue(gRow, 10, "txtFeb"),
                Common.GetGridControlValue(gRow, 10, "hfFeb"),
                Common.GetGridControlValue(gRow, 11, "txtMar"),
                Common.GetGridControlValue(gRow, 11, "hfMar"),
                Common.GetGridControlValue(gRow, 12, "txtApr"),
                Common.GetGridControlValue(gRow, 12, "hfApr"),
                Common.GetGridControlValue(gRow, 13, "txtMay"),
                Common.GetGridControlValue(gRow, 13, "hfMay"),
                Common.GetGridControlValue(gRow, 14, "txtJun"),
                Common.GetGridControlValue(gRow, 14, "hfJun"),
                strInsBy, strInsDate, "Y", gr.DataKeys[gRow.DataItemIndex].Values[0].ToString().Trim(),
                gr.DataKeys[gRow.DataItemIndex].Values[3].ToString().Trim());
            i++;
        }
        objDC.MakeTransaction(command);
        //ommand.CommandType = CommandType.StoredProcedure;
    }


    protected SqlCommand InsertGADRecoverPlanning(string strFinYear, string strEmpID, string strGAD,
        string strJul, string strIsJulAmt, string strAug, string strIsAugAmt, string strSep, string strIsSepAmt,
        string strOct, string strIsOctAmt, string strNov, string strIsNovAmt, string strDec, string strIsDecAmt,
        string strJan, string strIsJanAmt, string strFeb, string strIsFebAmt, string strMar, string strIsMarAmt,
        string strApr, string strIsAprAmt, string strMay, string strIsMayAmt, string strJun, string strIsJunAmt,
        string strInsBy, string strInsDate, string strIsUpdate,string strTransID,string strAccLine)
    {
        string strID="";
        SqlCommand command = new SqlCommand("Proc_Payroll_Insert_PayrollCostRecoveryPlan");
        command.CommandType = CommandType.StoredProcedure;
        if (strIsUpdate == "Y")
            strID = strTransID;
        else
            strID=objDC.GerMaxIDNumber("PayrollCostRecoveryPlan", "TRANSID").ToString();

        SqlParameter p_TRANSID = command.Parameters.Add("TRANSID", SqlDbType.BigInt);
        p_TRANSID.Direction = ParameterDirection.Input;
        p_TRANSID.Value = strID;

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = strFinYear;

        SqlParameter p_EMPID = command.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        SqlParameter p_GADCODE = command.Parameters.Add("GADCODE", SqlDbType.Char);
        p_GADCODE.Direction = ParameterDirection.Input;
        p_GADCODE.Value = strGAD;

        SqlParameter p_JUL = command.Parameters.Add("JUL", SqlDbType.Decimal);
        p_JUL.Direction = ParameterDirection.Input;
        p_JUL.Value = strJul;

        SqlParameter p_ISJULASAMT = command.Parameters.Add("ISJULASAMT", SqlDbType.Char);
        p_ISJULASAMT.Direction = ParameterDirection.Input;
        p_ISJULASAMT.Value = strIsJulAmt;

        SqlParameter p_AUG = command.Parameters.Add("AUG", SqlDbType.Decimal);
        p_AUG.Direction = ParameterDirection.Input;
        p_AUG.Value = strAug;

        SqlParameter p_ISAUGASAMT = command.Parameters.Add("ISAUGASAMT", SqlDbType.Char);
        p_ISAUGASAMT.Direction = ParameterDirection.Input;
        p_ISAUGASAMT.Value = strIsAugAmt;

        SqlParameter p_SEP = command.Parameters.Add("SEP", SqlDbType.Decimal);
        p_SEP.Direction = ParameterDirection.Input;
        p_SEP.Value = strSep;

        SqlParameter p_ISSEPASAMT = command.Parameters.Add("ISSEPASAMT", SqlDbType.Char);
        p_ISSEPASAMT.Direction = ParameterDirection.Input;
        p_ISSEPASAMT.Value = strIsSepAmt;

        SqlParameter p_OCT = command.Parameters.Add("OCT", SqlDbType.Decimal);
        p_OCT.Direction = ParameterDirection.Input;
        p_OCT.Value = strOct;

        SqlParameter p_ISOCTASAMT = command.Parameters.Add("ISOCTASAMT", SqlDbType.Char);
        p_ISOCTASAMT.Direction = ParameterDirection.Input;
        p_ISOCTASAMT.Value = strIsOctAmt;

        SqlParameter p_NOV = command.Parameters.Add("NOV", SqlDbType.Decimal);
        p_NOV.Direction = ParameterDirection.Input;
        p_NOV.Value = strNov;

        SqlParameter p_ISNOVASAMT = command.Parameters.Add("ISNOVASAMT", SqlDbType.Char);
        p_ISNOVASAMT.Direction = ParameterDirection.Input;
        p_ISNOVASAMT.Value = strIsNovAmt;

        SqlParameter p_DEC = command.Parameters.Add("DEC", SqlDbType.Decimal);
        p_DEC.Direction = ParameterDirection.Input;
        p_DEC.Value = strDec;

        SqlParameter p_ISDECASAMT = command.Parameters.Add("ISDECASAMT", SqlDbType.Char);
        p_ISDECASAMT.Direction = ParameterDirection.Input;
        p_ISDECASAMT.Value = strIsDecAmt;

        SqlParameter p_JAN = command.Parameters.Add("JAN", SqlDbType.Decimal);
        p_JAN.Direction = ParameterDirection.Input;
        p_JAN.Value = strJan;

        SqlParameter p_ISJANASAMT = command.Parameters.Add("ISJANASAMT", SqlDbType.Char);
        p_ISJANASAMT.Direction = ParameterDirection.Input;
        p_ISJANASAMT.Value = strIsJanAmt;

        SqlParameter p_FEB = command.Parameters.Add("FEB", SqlDbType.Decimal);
        p_FEB.Direction = ParameterDirection.Input;
        p_FEB.Value = strFeb;

        SqlParameter p_ISFEBASAMT = command.Parameters.Add("ISFEBASAMT", SqlDbType.Char);
        p_ISFEBASAMT.Direction = ParameterDirection.Input;
        p_ISFEBASAMT.Value = strIsFebAmt;

        SqlParameter p_MAR = command.Parameters.Add("MAR", SqlDbType.Decimal);
        p_MAR.Direction = ParameterDirection.Input;
        p_MAR.Value = strMar;

        SqlParameter p_ISMARASAMT = command.Parameters.Add("ISMARASAMT", SqlDbType.Char);
        p_ISMARASAMT.Direction = ParameterDirection.Input;
        p_ISMARASAMT.Value = strIsMarAmt;

        SqlParameter p_APR = command.Parameters.Add("APR", SqlDbType.Decimal);
        p_APR.Direction = ParameterDirection.Input;
        p_APR.Value = strApr;

        SqlParameter p_ISAPRASAMT = command.Parameters.Add("ISAPRASAMT", SqlDbType.Char);
        p_ISAPRASAMT.Direction = ParameterDirection.Input;
        p_ISAPRASAMT.Value = strIsAprAmt;

        SqlParameter p_MAY = command.Parameters.Add("MAY", SqlDbType.Decimal);
        p_MAY.Direction = ParameterDirection.Input;
        p_MAY.Value = strMay;

        SqlParameter p_ISMAYASAMT = command.Parameters.Add("ISMAYASAMT", SqlDbType.Char);
        p_ISMAYASAMT.Direction = ParameterDirection.Input;
        p_ISMAYASAMT.Value = strIsMayAmt;

        SqlParameter p_JUN = command.Parameters.Add("JUN", SqlDbType.Decimal);
        p_JUN.Direction = ParameterDirection.Input;
        p_JUN.Value = strJun;

        SqlParameter p_ISJUNASAMT = command.Parameters.Add("ISJUNASAMT", SqlDbType.Char);
        p_ISJUNASAMT.Direction = ParameterDirection.Input;
        p_ISJUNASAMT.Value = strIsJunAmt;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        SqlParameter p_isUpdate = command.Parameters.Add("isUpdate", SqlDbType.Char);
        p_isUpdate.Direction = ParameterDirection.Input;
        p_isUpdate.Value = strIsUpdate;

        SqlParameter p_PLANACCLINE = command.Parameters.Add("PLANACCLINE", SqlDbType.Char);
        p_PLANACCLINE.Direction = ParameterDirection.Input;
        p_PLANACCLINE.Value = strAccLine;

        return command;
    }

    public void DeleteCostRecoveryPlanData(string strTransId)
    {
        SqlCommand command = new SqlCommand("DELETE FROM PayrollCostRecoveryPlan WHERE TRANSID=@TRANSID");
        command.CommandType = CommandType.Text;

        SqlParameter p_TRANSID = command.Parameters.Add("TRANSID", SqlDbType.BigInt);
        p_TRANSID.Direction = ParameterDirection.Input;
        p_TRANSID.Value = strTransId;

        objDC.ExecuteQuery(command);
    }


    public DataTable SelectCostRecoveryPlanData(string strFinYear, string strEmpID)
    {
        SqlCommand command = new SqlCommand("Proc_Payroll_Select_PayrollCostRecoveryPlan");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = strFinYear;

        SqlParameter p_EMPID = command.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        objDC.CreateDSFromProc(command, "SelectCostRecoveryPlanData");
        return objDC.ds.Tables["SelectCostRecoveryPlanData"];
    }
	public GADRecoveryManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
