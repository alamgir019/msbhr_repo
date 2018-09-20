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
/// Summary description for BonusAllowanceManager
/// </summary>
public class BonusAllowanceManager
{
    DBConnector objDC = new DBConnector();

    public void InsertBonusAllowanceData(GridView gr, string strFinYear, string strReligion, string strMonth, string strYear, string strFestiveDate,
        string strSheadID, string strInsBy, string strInsDate, string FestivalID, string strVStatus,string strTaxFinYr)
    {
        SqlCommand[] cmd = new SqlCommand[gr.Rows.Count + 1];
        int i = 1;
        long lngVID = objDC.GerMaxIDNumber("BonusAllowance", "VID");

        cmd[0] = new SqlCommand("Proc_Payroll_Delete_BonusAllowance");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = cmd[0].Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd[0].Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_FISCALYRID = cmd[0].Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        SqlParameter p_SHEADID = cmd[0].Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = strSheadID;

        SqlParameter p_Religion = cmd[0].Parameters.Add("Religion", SqlDbType.VarChar);
        p_Religion.Direction = ParameterDirection.Input;
        p_Religion.Value = strReligion;

        SqlParameter p_FestivalID = cmd[0].Parameters.Add("FestivalID", SqlDbType.BigInt);
        p_FestivalID.Direction = ParameterDirection.Input;
        p_FestivalID.Value = FestivalID;

        //SqlParameter p_EmpTypeId = cmd[0].Parameters.Add("EmpTypeId", SqlDbType.BigInt);
        //p_EmpTypeId.Direction = ParameterDirection.Input;
        //p_EmpTypeId.Value = strEmpTypeId;

        foreach (GridViewRow gRow in gr.Rows)
        {
            CheckBox chkB = (CheckBox)gRow.Cells[0].FindControl("ChkBox");
            if (chkB.Checked == true)
            {
                cmd[i] = new SqlCommand("proc_Payroll_Insert_BonusAllowance");
                cmd[i].CommandType = CommandType.StoredProcedure;

                SqlParameter p_VID = cmd[i].Parameters.Add("VID", SqlDbType.BigInt);
                p_VID.Direction = ParameterDirection.Input;
                p_VID.Value = lngVID;

                SqlParameter p_EMPID = cmd[i].Parameters.Add("EMPID", SqlDbType.Char);
                p_EMPID.Direction = ParameterDirection.Input;
                p_EMPID.Value = gRow.Cells[1].Text.Trim();

                SqlParameter p_EMPTYPEID = cmd[i].Parameters.Add("EMPTYPEID", SqlDbType.BigInt);
                p_EMPTYPEID.Direction = ParameterDirection.Input;
                p_EMPTYPEID.Value = gr.DataKeys[gRow.DataItemIndex].Values[2].ToString().Trim();

                p_VMONTH = cmd[i].Parameters.Add("VMONTH", SqlDbType.BigInt);
                p_VMONTH.Direction = ParameterDirection.Input;
                p_VMONTH.Value = strMonth;

                p_VYEAR = cmd[i].Parameters.Add("VYEAR", SqlDbType.BigInt);
                p_VYEAR.Direction = ParameterDirection.Input;
                p_VYEAR.Value = strYear;

                p_FISCALYRID = cmd[i].Parameters.Add("FISCALYRID", SqlDbType.BigInt);
                p_FISCALYRID.Direction = ParameterDirection.Input;
                p_FISCALYRID.Value = strFinYear;

                p_SHEADID = cmd[i].Parameters.Add("SHEADID", SqlDbType.BigInt);
                p_SHEADID.Direction = ParameterDirection.Input;
                p_SHEADID.Value = strSheadID;

                SqlParameter p_EMPBASIC = cmd[i].Parameters.Add("EMPBASIC", SqlDbType.Decimal);
                p_EMPBASIC.Direction = ParameterDirection.Input;
                p_EMPBASIC.Value = gRow.Cells[8].Text.Trim();

                TextBox txtB = (TextBox)gRow.Cells[9].FindControl("txtBonus");
                SqlParameter p_PAYAMT = cmd[i].Parameters.Add("PAYAMT", SqlDbType.Decimal);
                p_PAYAMT.Direction = ParameterDirection.Input;
                p_PAYAMT.Value = txtB.Text;

                SqlParameter p_ISPRORATA = cmd[i].Parameters.Add("ISPRORATA", SqlDbType.Char);
                p_ISPRORATA.Direction = ParameterDirection.Input;
                p_ISPRORATA.Value = gRow.Cells[7].Text.Trim();

                SqlParameter p_VSTATUS = cmd[i].Parameters.Add("VSTATUS", SqlDbType.Char);
                p_VSTATUS.Direction = ParameterDirection.Input;
                p_VSTATUS.Value = strVStatus;

                SqlParameter p_INSERTTEDBY = cmd[i].Parameters.Add("INSERTTEDBY", SqlDbType.VarChar);
                p_INSERTTEDBY.Direction = ParameterDirection.Input;
                p_INSERTTEDBY.Value = strInsBy;

                SqlParameter p_INSERTTEDDATE = cmd[i].Parameters.Add("INSERTTEDDATE", SqlDbType.Char);
                p_INSERTTEDDATE.Direction = ParameterDirection.Input;
                p_INSERTTEDDATE.Value = strInsDate;

                SqlParameter p_RELIGION = cmd[i].Parameters.Add("RELIGIONId", DBNull.Value  );
                p_RELIGION.Direction = ParameterDirection.Input;
                if (strReligion != "-1")
                    p_RELIGION.Value = strReligion;
               
                SqlParameter p_PRORATADAYS = cmd[i].Parameters.Add("PRORATADAYS", SqlDbType.Char);
                p_PRORATADAYS.Direction = ParameterDirection.Input;
                p_PRORATADAYS.Value = gRow.Cells[10].Text.Trim();

                SqlParameter p_FESTIVEDATE = cmd[i].Parameters.Add("FESTIVEDATE", SqlDbType.DateTime);
                p_FESTIVEDATE.Direction = ParameterDirection.Input;
                p_FESTIVEDATE.Value = strFestiveDate;

                p_FestivalID = cmd[i].Parameters.Add("FestivalID", SqlDbType.BigInt);
                p_FestivalID.Direction = ParameterDirection.Input;
                p_FestivalID.Value = FestivalID;

                SqlParameter p_TaxFiscalYrId = cmd[i].Parameters.Add("TaxFiscalYrId", SqlDbType.BigInt);
                p_TaxFiscalYrId.Direction = ParameterDirection.Input;
                p_TaxFiscalYrId.Value = strTaxFinYr;
                
                i++;
                lngVID++;
            }
        }
        objDC.MakeTransaction(cmd);
    }

    public void DeleteBonusAllowanceData(string strMonth, string strYear, string strFinYear, string strSheadId, string strRelegion, string strFistavalID)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Delete_BonusAllowance");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        SqlParameter p_SHEADID = cmd.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = strSheadId;

        SqlParameter p_Religion = cmd.Parameters.Add("Religion", SqlDbType.VarChar);
        p_Religion.Direction = ParameterDirection.Input;
        p_Religion.Value = strRelegion;

        SqlParameter p_FestivalID = cmd.Parameters.Add("FestivalID", SqlDbType.BigInt);
        p_FestivalID.Direction = ParameterDirection.Input;
        p_FestivalID.Value = strFistavalID;

        //SqlParameter p_EmpTypeId = cmd.Parameters.Add("EmpTypeId", SqlDbType.BigInt);
        //p_EmpTypeId.Direction = ParameterDirection.Input;
        //p_EmpTypeId.Value = strEmpTypeId;

        objDC.ExecuteQuery(cmd);
    }

    public DataTable GetBonusAllowanceData(string strMonth,string strYear,string strFinYear,string strSheadId, string strRelegion,string strFistavalID)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Select_BonusAllowance");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        SqlParameter p_SHEADID = cmd.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = strSheadId;

        SqlParameter p_Religion = cmd.Parameters.Add("ReligionId", SqlDbType.BigInt);
        p_Religion.Direction = ParameterDirection.Input;
        p_Religion.Value = strRelegion;

        SqlParameter p_FestivalID = cmd.Parameters.Add("FestivalID", SqlDbType.BigInt);
        p_FestivalID.Direction = ParameterDirection.Input;
        p_FestivalID.Value = strFistavalID;

        //SqlParameter p_EmpTypeId = cmd.Parameters.Add("EmpTypeId", SqlDbType.BigInt);
        //p_EmpTypeId.Direction = ParameterDirection.Input;
        //p_EmpTypeId.Value = strEmpTypeId;

        objDC.CreateDSFromProc(cmd, "GetBonusAllowanceData");
        return objDC.ds.Tables["GetBonusAllowanceData"];
    }

    public DataTable GetEmployeeForBonusAllowance(string strRelegion, string strDate)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Select_EmployeeForBonusAllowance");
        cmd.CommandType = CommandType.StoredProcedure;
        
        SqlParameter p_Religion = cmd.Parameters.Add("ReligionId", SqlDbType.BigInt);
        p_Religion.Direction = ParameterDirection.Input;
        p_Religion.Value = strRelegion;

         SqlParameter p_JoiningDate = cmd.Parameters.Add("JOININGDATE", SqlDbType.DateTime);
        p_JoiningDate.Direction = ParameterDirection.Input;
        p_JoiningDate.Value = strDate;

        //SqlParameter p_EmpTypeId = cmd.Parameters.Add("EmpTypeId", SqlDbType.BigInt);
        //p_EmpTypeId.Direction = ParameterDirection.Input;
        //p_EmpTypeId.Value = strEmpTypeId;

        // SqlParameter p_JoiningDate = cmd.Parameters.Add("JoiningDate", SqlDbType.DateTime);
        //p_JoiningDate.Direction = ParameterDirection.Input;
        //p_JoiningDate.Value = strDate;

        //SqlParameter p_Religion = cmd.Parameters.Add("ReligionId", SqlDbType.BigInt);
        //p_Religion.Direction = ParameterDirection.Input;
        //p_Religion.Value = strRelegion;

        //@Religion varchar(60),        
        //@JOININGDATE datetime         
 
        objDC.CreateDSFromProc(cmd, "GetEmployeeForBonusAllowance");
        return objDC.ds.Tables["GetEmployeeForBonusAllowance"];
    }

    public DataTable GetNoOfBasicRelagionWise(string strRelagionId)
    {
        string strSQL = "SELECT isnull(NumberOfbasic,1) as NumberOfbasic FROM ReligionList WHERE ReligionId =" + Convert.ToInt32(strRelagionId);
          
        SqlCommand cmd = new SqlCommand(strSQL);
        //cmd2.CommandType = CommandType.StoredProcedure;

        objDC.CreateDT(cmd, "NumberOfbasic");
        return objDC.ds.Tables["NumberOfbasic"];
    }

    public void UpdateBonusStatus(GridView grEmp, string strMonth, string strYear, string strStatus, string strInsBy, string strInsDate)
    {
        int i = 0;
        SqlCommand[] command = new SqlCommand[grEmp.Rows.Count];
        string strSQL = "";
        foreach (GridViewRow gRow in grEmp.Rows)
        {
            if (strStatus=="R")
            strSQL = "UPDATE BonusAllowance SET VStatus=@VStatus,ReviewedBy=@INSERTEDBY,ReviewedDate=@INSERTEDDATE WHERE VMONTH=@VMONTH AND VYEAR=@VYEAR AND EMPID=@EMPID";
            else
                strSQL = "UPDATE BonusAllowance SET VStatus=@VStatus,ApprovedBy=@INSERTEDBY,ApprovedDate=@INSERTEDDATE WHERE VMONTH=@VMONTH AND VYEAR=@VYEAR AND EMPID=@EMPID";
            command[i] = new SqlCommand(strSQL);
            command[i].CommandType = CommandType.Text;

            SqlParameter p_VMONTH = command[i].Parameters.Add("VMONTH", SqlDbType.BigInt);
            p_VMONTH.Direction = ParameterDirection.Input;
            p_VMONTH.Value = strMonth;

            SqlParameter p_VYEAR = command[i].Parameters.Add("VYEAR", SqlDbType.BigInt);
            p_VYEAR.Direction = ParameterDirection.Input;
            p_VYEAR.Value = strYear;

            //SqlParameter p_PSBID = command[i].Parameters.Add("PSBID", SqlDbType.BigInt);
            //p_PSBID.Direction = ParameterDirection.Input;
            //p_PSBID.Value = dRow["PSBID"].ToString().Trim();

            SqlParameter p_EMPID = command[i].Parameters.Add("EMPID", SqlDbType.Char);
            p_EMPID.Direction = ParameterDirection.Input;
            p_EMPID.Value = gRow.Cells[1].Text.Trim();

            SqlParameter p_PAYSLIPSTATUS = command[i].Parameters.Add("VSTATUS", SqlDbType.Char);
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
    public BonusAllowanceManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
