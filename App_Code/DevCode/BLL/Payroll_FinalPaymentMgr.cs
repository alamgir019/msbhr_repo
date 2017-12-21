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
/// Summary description for Payroll_FinalPaymentMgr
/// </summary>
public class Payroll_FinalPaymentMgr
{
    DBConnector objDC = new DBConnector();
    public Payroll_FinalPaymentMgr()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable SelectEmpFinalPayment(string strEmpid)
    {
        string strSQL = "SELECT * from FinalPayment WHERE EmpID='" + strEmpid + "'";

        return objDC.CreateDT(strSQL, "EmpFinalPayment");
    }

    public DataTable SelectEmpFinalPayment(string strGenerateFor, string strGeneratValue, string strMonth, string strYear,string strPayStatus)
    {
        string strCond = "";
        string strSQL = "";
       

        strSQL = "SELECT * from FinalPayment FP,EmpInfo E WHERE FP.EmpId=E.EmpId AND datepart(mm,ProcessDate) = @VMonth AND datepart(yyyy,ProcessDate) = @VYear"
            + " AND FP.PayStatus= @PayStatus";
        if (strGeneratValue != "-1")
            strCond = " AND E.ClinicId =" + strGeneratValue;
        else
            strCond = "";

        if (strPayStatus != "")
            strCond = strCond+  " AND FP.PayStatus ='" + strPayStatus + "'";
        else
            strCond = strCond+"";
        strSQL = strSQL + strCond;

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        
        SqlParameter p_GenerateValue = cmd.Parameters.Add("ClinicId", SqlDbType.BigInt);
        p_GenerateValue.Direction = ParameterDirection.Input;
        p_GenerateValue.Value = strGeneratValue;
        
        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_PayStatus = cmd.Parameters.Add("PayStatus", SqlDbType.Char );
        p_PayStatus.Direction = ParameterDirection.Input;
        p_PayStatus.Value = strPayStatus;

        objDC.CreateDT(cmd, "EmpFinalPayment");
        return objDC.ds.Tables["EmpFinalPayment"];        
    }

    public void UpdateFinalPayment(DataTable dt, string strMonth, string strYear, string strStatus, string strInsBy, string strInsDate)
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

            SqlParameter p_EMPID = command[i].Parameters.Add("EMPID", SqlDbType.Char);
            p_EMPID.Direction = ParameterDirection.Input;
            p_EMPID.Value = dRow["EMPID"].ToString().Trim();

            SqlParameter p_PAYSLIPSTATUS = command[i].Parameters.Add("PAYSTATUS", SqlDbType.Char);
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
}