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
/// Summary description for Payroll_MovementStatement
/// </summary>
public class Payroll_MovementStatement
{
    DBConnector objDC = new DBConnector();

    public void SaveAndApproveMoveStatement(GridView grSumm, GridView grDet, string strMonth, string strYear, 
        string strBank, string strOffice, string strMonthBetween,string strInsBy,string strInsDate)
    {
        SqlCommand[] command = new SqlCommand[grDet.Rows.Count + 2 + 1];
        command[0] = DeleteSalaryMovement(strMonth, strYear, strBank, strOffice);
        string strMovID = Common.getMaxId("SalaryMovement", "MOVEID");
        int i = 1;
        foreach (GridViewRow gRowSumm in grSumm.Rows)
        {
            command[i] = InsertSalaryMovement(strMonth, strYear, strBank, strOffice, strMonthBetween, strInsBy, strInsDate, strMovID, "Y", gRowSumm);
            i++;
        }
        foreach (GridViewRow gRowDet in grDet.Rows)
        {
            command[i] = InsertSalaryMovement(strMonth, strYear, strBank, strOffice, strMonthBetween, strInsBy, strInsDate, strMovID, "N", gRowDet);
            i++;
        }
        objDC.MakeTransaction(command);
    }

    public SqlCommand DeleteSalaryMovement(string strMonth, string strYear, string strBank, string strOffice)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Delete_SalaryMovement");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.Char);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_BRANCHCODE = cmd.Parameters.Add("BRANCHCODE", SqlDbType.Char);
        p_BRANCHCODE.Direction = ParameterDirection.Input;
        p_BRANCHCODE.Value = strBank;

        SqlParameter p_LOCID = cmd.Parameters.Add("LOCID", SqlDbType.BigInt);
        p_LOCID.Direction = ParameterDirection.Input;
        p_LOCID.Value = "0";

        return cmd;

    }

    

    public SqlCommand InsertSalaryMovement(string strMonth, string strYear, string strBank, string strOffice,
       string strMonthBetween, string strInsBy, string strInsDate, string strMovID,string strIsSummary, GridViewRow gRow)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Insert_SalaryMovement");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_MOVEID = cmd.Parameters.Add("MOVEID", SqlDbType.BigInt);
        p_MOVEID.Direction = ParameterDirection.Input;
        p_MOVEID.Value = strMovID;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.Char);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_BRANCHCODE = cmd.Parameters.Add("BRANCHCODE", DBNull.Value);
        p_BRANCHCODE.Direction = ParameterDirection.Input;
        p_BRANCHCODE.IsNullable = true;
        if (strBank != "")
            p_BRANCHCODE.Value = strBank;

        SqlParameter p_LOCID = cmd.Parameters.Add("LOCID", DBNull.Value);
        p_LOCID.Direction = ParameterDirection.Input;
        p_LOCID.IsNullable = true;
        if (strOffice != "")
            p_LOCID.Value = strOffice;

        SqlParameter p_MONTHBETWEEN = cmd.Parameters.Add("MONTHBETWEEN", SqlDbType.VarChar);
        p_MONTHBETWEEN.Direction = ParameterDirection.Input;
        p_MONTHBETWEEN.Value = strMonthBetween;


        SqlParameter p_PAYID = cmd.Parameters.Add("PAYID", DBNull.Value);
        p_PAYID.Direction = ParameterDirection.Input;
        p_PAYID.IsNullable = true;
        if (strIsSummary == "N")
            p_PAYID.Value = gRow.Cells[0].Text.Trim();


        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", DBNull.Value);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.IsNullable = true;
        if (strIsSummary == "N")
            p_EMPID.Value = gRow.Cells[1].ToolTip.Trim();

        SqlParameter p_EMPNAME = cmd.Parameters.Add("EMPNAME", DBNull.Value);
        p_EMPNAME.Direction = ParameterDirection.Input;
        p_EMPNAME.IsNullable = true;
        if (strIsSummary == "N")
            p_EMPNAME.Value = gRow.Cells[1].Text.Trim();


        SqlParameter p_DESCRIP = cmd.Parameters.Add("DESCRIP", SqlDbType.VarChar);
        p_DESCRIP.Direction = ParameterDirection.Input;
        if (strIsSummary == "N")
        {
            TextBox txtD = (TextBox)gRow.Cells[2].FindControl("txtDescrip");
            p_DESCRIP.Value = txtD.Text.Trim();
        }
        else
        {
            p_DESCRIP.Value = gRow.Cells[0].Text.Trim();
        }
        SqlParameter p_TOTALSAL = cmd.Parameters.Add("TOTALSAL", SqlDbType.Decimal);
        p_TOTALSAL.Direction = ParameterDirection.Input;
        if (strIsSummary == "N")
            p_TOTALSAL.Value = Common.RoundDecimal(gRow.Cells[3].Text.Trim(), 0);
        else
            p_TOTALSAL.Value = Common.RoundDecimal(gRow.Cells[1].Text.Trim(), 0);


        SqlParameter p_PFCONT = cmd.Parameters.Add("PFCONT", SqlDbType.Decimal);
        p_PFCONT.Direction = ParameterDirection.Input;
        if (strIsSummary == "N")
            p_PFCONT.Value = Common.RoundDecimal(gRow.Cells[4].Text.Trim(), 0);
        else
            p_PFCONT.Value = Common.RoundDecimal(gRow.Cells[2].Text.Trim(), 0);

        SqlParameter p_PFLOAN = cmd.Parameters.Add("PFLOAN", SqlDbType.Decimal);
        p_PFLOAN.Direction = ParameterDirection.Input;
        if (strIsSummary == "N")
            p_PFLOAN.Value = Common.RoundDecimal(gRow.Cells[5].Text.Trim(), 0);
        else
            p_PFLOAN.Value = Common.RoundDecimal(gRow.Cells[3].Text.Trim(), 0);

        SqlParameter p_IT = cmd.Parameters.Add("IT", SqlDbType.Decimal);
        p_IT.Direction = ParameterDirection.Input;
        if (strIsSummary == "N")
            p_IT.Value = Common.RoundDecimal(gRow.Cells[6].Text.Trim(), 0);
        else
            p_IT.Value = Common.RoundDecimal(gRow.Cells[4].Text.Trim(), 0);


        SqlParameter p_SALADV = cmd.Parameters.Add("SALADV", SqlDbType.Decimal);
        p_SALADV.Direction = ParameterDirection.Input;
        if (strIsSummary == "N")
            p_SALADV.Value = Common.RoundDecimal(gRow.Cells[7].Text.Trim(), 0);
        else
            p_SALADV.Value = Common.RoundDecimal(gRow.Cells[5].Text.Trim(), 0);

        SqlParameter p_LWP = cmd.Parameters.Add("LWP", SqlDbType.Decimal);
        p_LWP.Direction = ParameterDirection.Input;
        if (strIsSummary == "N")
            p_LWP.Value = Common.RoundDecimal(gRow.Cells[8].Text.Trim(), 0);
        else
            p_LWP.Value = Common.RoundDecimal(gRow.Cells[6].Text.Trim(), 0);

        SqlParameter p_TOTALDEDUCT = cmd.Parameters.Add("TOTALDEDUCT", SqlDbType.Decimal);
        p_TOTALDEDUCT.Direction = ParameterDirection.Input;
        if (strIsSummary == "N")
            p_TOTALDEDUCT.Value = Common.RoundDecimal(gRow.Cells[9].Text.Trim(), 0);
        else
            p_TOTALDEDUCT.Value = Common.RoundDecimal(gRow.Cells[7].Text.Trim(), 0);

        SqlParameter p_NETPAY = cmd.Parameters.Add("NETPAY", SqlDbType.Decimal);
        p_NETPAY.Direction = ParameterDirection.Input;
        if (strIsSummary == "N")
            p_NETPAY.Value = Common.RoundDecimal(gRow.Cells[10].Text.Trim(), 0);
        else
            p_NETPAY.Value = Common.RoundDecimal(gRow.Cells[8].Text.Trim(), 0);

        SqlParameter p_ISSUMMARY = cmd.Parameters.Add("ISSUMMARY", SqlDbType.Char);
        p_ISSUMMARY.Direction = ParameterDirection.Input;
        p_ISSUMMARY.Value = strIsSummary;
        
        SqlParameter p_INSERTEDBY = cmd.Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = strInsBy;

        SqlParameter p_INSERTEDDATE = cmd.Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDDATE.Direction = ParameterDirection.Input;
        p_INSERTEDDATE.Value = strInsDate;

        return cmd;
    }

    public void UpdateMoveStatement(GridView grDet,string strInsBy, string strInsDate)
    {
        SqlCommand[] command = new SqlCommand[grDet.Rows.Count];
        int i = 0;  
        foreach (GridViewRow gRowDet in grDet.Rows)
        {
            TextBox txtDesc=(TextBox)gRowDet.Cells[2].FindControl("txtDescrip");
            command[i] = UpdateSalaryMovement(grDet.DataKeys[gRowDet.DataItemIndex].Values[0].ToString().Trim(),
                                            txtDesc.Text.Trim(), strInsBy, strInsDate);
            i++;
        }
        objDC.MakeTransaction(command);
    }
    protected SqlCommand UpdateSalaryMovement(string strTransID,string strDescrip,string strInsBy, string strInsDate)
    {
        string strSQL = "UPDATE SALARYMOVEMENT SET DESCRIP=@DESCRIP,INSERTEDBY=@INSERTEDBY,INSERTEDDATE=@INSERTEDDATE WHERE TRANSID=@TRANSID";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_TRANSID = cmd.Parameters.Add("TRANSID", SqlDbType.BigInt);
        p_TRANSID.Direction = ParameterDirection.Input;
        p_TRANSID.Value = strTransID;

        SqlParameter p_DESCRIP = cmd.Parameters.Add("DESCRIP", SqlDbType.VarChar);
        p_DESCRIP.Direction = ParameterDirection.Input;
        p_DESCRIP.Value = strDescrip;

        SqlParameter p_INSERTEDBY = cmd.Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = strInsBy;

        SqlParameter p_INSERTEDDATE = cmd.Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDDATE.Direction = ParameterDirection.Input;
        p_INSERTEDDATE.Value = strInsDate;

        return cmd;

    }

	public Payroll_MovementStatement()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region PayrollReconcilDetail
    // Insert or Update or Delete Data of Payroll Reconcil Detail Log table
    public void InsertPayrollReconcilDetail(string strReconcilId, string strEmpId, string sVMonth,string sVYear,string strBasicSalary, 
        string strAllowance, string strReason, string strSeparationDate,string strInsBy,string strInsDate)
    {
        SqlCommand command = new SqlCommand("proc_Insert_PayrollReconcilDetailLog");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_TransId = command.Parameters.Add("ReconcilId", SqlDbType.BigInt);
        p_TransId.Direction = ParameterDirection.Input;
        p_TransId.Value = strReconcilId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_VMonth = command.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = sVMonth;

        SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.BigInt);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = sVYear;

        SqlParameter p_BasicSalary = command.Parameters.Add("BasicSalary", SqlDbType.Decimal);
        p_BasicSalary.Direction = ParameterDirection.Input;
        p_BasicSalary.Value = Convert.ToDecimal(strBasicSalary);

        SqlParameter p_Allowance = command.Parameters.Add("Allowance", SqlDbType.Decimal);
        p_Allowance.Direction = ParameterDirection.Input;
        p_Allowance.Value = Convert.ToDecimal(strAllowance);

        SqlParameter p_OTHour = command.Parameters.Add("Reason", SqlDbType.VarChar);
        p_OTHour.Direction = ParameterDirection.Input;
        p_OTHour.Value = strReason;

        SqlParameter p_OTAppHour = command.Parameters.Add("SeparationDate", SqlDbType.VarChar);
        p_OTAppHour.Direction = ParameterDirection.Input;
        p_OTAppHour.Value = strSeparationDate;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;
     
        try
        {
            objDC.ExecuteQuery(command);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            command = null;
        }
    }

    public DataTable SelectPayrollReconcilDetail(string strEmpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_PayrollReconcilDetailLog");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        //SqlParameter p_VMonth = command.Parameters.Add("VMonth", SqlDbType.BigInt);
        //p_VMonth.Direction = ParameterDirection.Input;
        //p_VMonth.Value = sVMonth;

        //SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.BigInt);
        //p_VYear.Direction = ParameterDirection.Input;
        //p_VYear.Value = sVYear;

        objDC.CreateDSFromProc(command, "SelectOTAdjustment");
        return objDC.ds.Tables["SelectOTAdjustment"];
    }
    #endregion
}
