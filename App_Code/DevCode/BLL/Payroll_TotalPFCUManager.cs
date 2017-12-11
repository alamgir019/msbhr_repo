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
/// Summary description for Payroll_TotalPFCUManager
/// </summary>
public class Payroll_TotalPFCUManager
{
    DBConnector objDB = new DBConnector();


    public DataTable GetDistinctDataForTPF(string strFiscalYear)
    {
        string strSQL = "SELECT DISTINCT EMPID,MAX(LEDGERID) AS LEDGERID  " 
                    + " FROM PFLEDGER   "
                    + " WHERE FISCALYRID=@FISCALYRID  "
                    + " GROUP BY EMPID"
                    + " ORDER BY EMPID";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYear;

        return objDB.CreateDT(cmd, "GetDistinctDataForTPF");
    }

    public DataTable GetDistinctDataForTCU(string strFiscalYear)
    {
        string strSQL = "SELECT DISTINCT CAST(EMPID as Numeric)  AS EMPID,MAX(LEDGERID) AS LEDGERID  "
                    + " FROM CULEDGER   "
                    + " WHERE FISCALYRID=@FISCALYRID  "
                    + " GROUP BY CAST(EMPID as Numeric) "
                    + " ORDER BY CAST(EMPID as Numeric) ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYear;

        return objDB.CreateDT(cmd, "GetDistinctDataForTCU");
    }
 

    public DataTable GetPFLedgerData(string strFiscalYear)
    {
        string strSQL="SELECT PFL.*,E.FULLNAME,E.JOININGDATE,E.CONFIRMATIONDATE     "    
                + " FROM PFLEDGER PFL LEFT OUTER JOIN PAYSLIPMST P ON PFL.EMPID=P.EMPID    "
                + " LEFT OUTER JOIN EMPINFO E ON P.EMPID=E.EMPID        "
                + " WHERE PFL.FISCALYRID=@FISCALYRID "
                + " AND P.PSBID=(SELECT MAX(PSBID) FROM PAYSLIPMST WHERE EMPID=PFL.EMPID)  "
                + " ORDER BY PFL.EMPID,PFL.VMONTH ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYear;


        return objDB.CreateDT(cmd, "GetPFLedgerData");
    }

    // Not in Use
    public DataTable GetPayrollPFData(string strFiscalYear)
    {
        string strSQL = " SELECT PM.VMONTH,PM.EMPID,PD.PAYAMT  FROM PAYSLIPMST PM,PAYSLIPDETS PD "
                        + " WHERE PM.PSBID=PD.PSBID AND PM.EMPID=PD.EMPID "
                        + " AND PD.SHEADID IN(9,24) AND PM.FISCALYRID=@FISCALYRID "
	                    + " AND PM.EMPSTATUS='A' AND PM.SALARYTYPE='S' "
                        + " ORDER BY CAST(PM.EMPID AS NUMERIC),PM.VMONTH ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYear;

        return objDB.CreateDT(cmd, "GetPayrollPFData");
    }

    public DataTable GetPFLoanLedgerDataForTPF(string strFiscalYear)
    {
        if (objDB.ds.Tables["GetPFLoanLedgerDataForTPF"] != null)
        {
            objDB.ds.Tables["GetPFLoanLedgerDataForTPF"].Rows.Clear();
            objDB.ds.Tables["GetPFLoanLedgerDataForTPF"].Dispose();
        }
        string strSQL = "SELECT PFL.* "
               + " FROM PFLOANLEDGER PFL "
               + " WHERE PFL.FISCALYRID=@FISCALYRID     "
             //  + " AND PFL.VMONTH= @VMONTH "
               + " AND PFL.TRANSID=(SELECT MAX(CAST(TRANSID AS NUMERIC)) FROM PFLOANLEDGER WHERE EMPID=PFL.EMPID) "
               + " ORDER BY EMPID ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYear;

        //SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        //p_VMONTH.Direction = ParameterDirection.Input;
        //p_VMONTH.Value = strMonth;

        return objDB.CreateDT(cmd, "GetPFLoanLedgerDataForTPF");
    }


    public DataTable GetCULedgerData(string strFiscalYear)
    {
        string strSQL = "SELECT PFL.*,E.FULLNAME,P.PARLOCID,E.CONFIRMATIONDATE     "
                + " FROM CULEDGER PFL LEFT OUTER JOIN PAYSLIPMST P ON PFL.EMPID=P.EMPID    "
                + " LEFT OUTER JOIN EMPINFO E ON P.HREMPID=E.EMPID        "
                + " WHERE PFL.FISCALYRID=@FISCALYRID "
                + " AND P.PSBID=(SELECT MAX(PSBID) FROM PAYSLIPMST WHERE EMPID=PFL.EMPID)  "
                + " ORDER BY CAST(PFL.EMPID as Numeric),PFL.VMONTH ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYear;

        return objDB.CreateDT(cmd, "GetCULedgerData");
    }
    public DataTable GetCULoanLedgerDataForTCU(string strFiscalYear)
    {
        if (objDB.ds.Tables["GetCULoanLedgerDataForTCU"] != null)
        {
            objDB.ds.Tables["GetCULoanLedgerDataForTCU"].Rows.Clear();
            objDB.ds.Tables["GetCULoanLedgerDataForTCU"].Dispose();
        }
        string strSQL = "SELECT PFL.* "
               + " FROM CULOANLEDGER PFL "
               + " WHERE PFL.FISCALYRID=@FISCALYRID     "
              // + " AND PFL.VMONTH= @VMONTH "
               + " AND PFL.TRANSID=(SELECT MAX(CAST(TRANSID AS NUMERIC)) FROM CULOANLEDGER WHERE EMPID=PFL.EMPID) "
               + " ORDER BY CAST(EMPID as Numeric) ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYear;

        //SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        //p_VMONTH.Direction = ParameterDirection.Input;
        //p_VMONTH.Value = strMonth;

        return objDB.CreateDT(cmd, "GetCULoanLedgerDataForTCU");
    }
    // Not in Use
    public DataTable GetPayrollCUData(string strFiscalYear)
    {
        string strSQL = " SELECT PM.VMONTH,PM.EMPID,ABS(PD.PAYAMT) AS PAYAMT  FROM PAYSLIPMST PM,PAYSLIPDETS PD "
                        + " WHERE PM.PSBID=PD.PSBID AND PM.EMPID=PD.EMPID "
                        + " AND PD.SHEADID IN(12) AND PM.FISCALYRID=@FISCALYRID "
                        + " AND PM.EMPSTATUS='A' AND PM.SALARYTYPE='S' "
                        + " ORDER BY CAST(PM.EMPID AS NUMERIC),PM.VMONTH ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFiscalYear;

        return objDB.CreateDT(cmd, "GetPayrollPFData");
    }

	public Payroll_TotalPFCUManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
