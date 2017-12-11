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
/// Summary description for TextFileImpManager
/// </summary>
public class TextFileImpManager
{
    private string strConnection = ConfigurationManager.ConnectionStrings["dbattnconn"].ConnectionString;// works fine
    private SqlConnection sqlConn;
    // SqlCommand command;
    SqlDataAdapter adapter = new SqlDataAdapter();
    DataSet dSet = new DataSet();

    DBConnector objDC = new DBConnector();

    public DataTable CreateDT(SqlCommand command, string TableName)//, SqlCommand com, SqlDataAdapter da)
    {
        try
        {
            //OraConnection.Open(); 
            sqlConn = new SqlConnection(strConnection);
            sqlConn.Open();
            command.CommandType = CommandType.Text;
            command.Connection = sqlConn;
            adapter.SelectCommand = command;
            adapter.Fill(dSet, TableName);

            sqlConn.Close();
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            if (sqlConn.State == ConnectionState.Open)
            {
                sqlConn.Close();
            }
        }

        return dSet.Tables[TableName];
    }

    public DataTable GetAttendanceRecordTextFile(string strStatus)
    {
        SqlCommand cmd = new SqlCommand("proc_select_AttendanceRecordTextFile");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_STATUS = cmd.Parameters.Add("STATUS", SqlDbType.Char);
        p_STATUS.Direction = ParameterDirection.Input;
        p_STATUS.Value = strStatus;

        objDC.CreateDSFromProc(cmd, "AttendanceRecordTextFile");
        return objDC.ds.Tables["AttendanceRecordTextFile"];
    }

    public DataTable GetCardNoWiseEmployee(string cardNo)
    {
        if (objDC.ds.Tables["tblCardNoWiseEmp"] != null)
        {
            objDC.ds.Tables["tblCardNoWiseEmp"].Clear();
            objDC.ds.Tables["tblCardNoWiseEmp"].Dispose();
        }

        SqlCommand cmd = new SqlCommand("proc_Select_CardNo_Wise_Emp");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_CardNo = cmd.Parameters.Add("CardNo", SqlDbType.VarChar);
        p_CardNo.Direction = ParameterDirection.Input;
        p_CardNo.Value = cardNo;

        objDC.CreateDSFromProc(cmd, "tblCardNoWiseEmp");
        return objDC.ds.Tables["tblCardNoWiseEmp"];
    }

    public DataTable GetDistinctEmpID()
    {
        string strSQL= "SELECT DISTINCT EMPID FROM AttendanceRecordTextFile WHERE STATUS='0'";
        objDC.CreateDT(strSQL, "DistinctEmpIDTextFile");
        return objDC.ds.Tables["DistinctEmpIDTextFile"];

    }

    public DataTable GetMinMaxAttndDate()
    {
        string strSQL = "select MIN(ATTDATE) as MinDate,MAX(ATTDATE) as MAXDATE from AttendanceRecordTextFile WHERE STATUS='0'";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        objDC.CreateDT(strSQL, "MinMaxAttndDate");
        return objDC.ds.Tables["MinMaxAttndDate"];

    }

    public DataTable GetLoginLogoutData(string strMinDate, string strMaxDate)
    {
        string strSQL = "select *,CONVERT(VARCHAR(10),LOGINTIME,111) as ATTNDDATE from LoginLogout WHERE LOGINTIME BETWEEN @STARTDATE and @ENDDATE ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_STARTDATE = cmd.Parameters.Add("STARTDATE", SqlDbType.DateTime);
        p_STARTDATE.Direction = ParameterDirection.Input;
        p_STARTDATE.Value = strMinDate + " 00:00";

        SqlParameter p_ENDDATE = cmd.Parameters.Add("ENDDATE", SqlDbType.DateTime);
        p_ENDDATE.Direction = ParameterDirection.Input;
        p_ENDDATE.Value = strMaxDate + " 23:59";

        this.CreateDT(cmd, "LoginLogoutData");
        return dSet.Tables["LoginLogoutData"];

    }

    public void ImportData(GridView gr)
    {
        int i = 0;
        SqlCommand[] cmd;
        cmd = new SqlCommand[gr.Rows.Count];
        
        long lngMaxID = Convert.ToInt64(objDC.GetMaxIdVar2("AttendanceRecordTextFile", "SRNO"));

        foreach (GridViewRow gRow in gr.Rows)
        {
            if (gRow.Visible == true)
            {
                lngMaxID++;
                
                cmd[i] = new SqlCommand("proc_Insert_AttendanceRecordTextFile");
                cmd[i].CommandType = CommandType.StoredProcedure;

                SqlParameter p_SRNO = cmd[i].Parameters.Add("SRNO", SqlDbType.Char);
                p_SRNO.Direction = ParameterDirection.Input;
                p_SRNO.Value = (lngMaxID-1).ToString();

                SqlParameter p_CARDNO = cmd[i].Parameters.Add("CARDNO", SqlDbType.Char);
                p_CARDNO.Direction = ParameterDirection.Input;
                p_CARDNO.Value = gRow.Cells[1].Text.Trim();

                SqlParameter p_EMPID = cmd[i].Parameters.Add("EMPID", SqlDbType.Char);
                p_EMPID.Direction = ParameterDirection.Input;
                p_EMPID.Value = gRow.Cells[2].Text.Trim();

                SqlParameter p_ATTDATE = cmd[i].Parameters.Add("ATTDATE", SqlDbType.DateTime);
                p_ATTDATE.Direction = ParameterDirection.Input;
                p_ATTDATE.Value = Common.ReturnDate(gRow.Cells[4].Text.Trim());

                SqlParameter p_ATTTIME = cmd[i].Parameters.Add("ATTTIME", SqlDbType.DateTime);
                p_ATTTIME.Direction = ParameterDirection.Input;
                p_ATTTIME.Value = Common.ReturnDateTime( gRow.Cells[5].Text.Trim());

                SqlParameter p_CONTROLLER = cmd[i].Parameters.Add("CONTROLLER", SqlDbType.VarChar);
                p_CONTROLLER.Direction = ParameterDirection.Input;
                p_CONTROLLER.Value = gRow.Cells[6].Text.Trim();

                SqlParameter p_INOUT = cmd[i].Parameters.Add("INOUT", SqlDbType.Char);
                p_INOUT.Direction = ParameterDirection.Input;
                p_INOUT.Value = gRow.Cells[7].Text.Trim();

                SqlParameter p_DOOR = cmd[i].Parameters.Add("DOOR", SqlDbType.VarChar);
                p_DOOR.Direction = ParameterDirection.Input;
                p_DOOR.Value = gRow.Cells[8].Text.Trim();

                SqlParameter p_STATUS = cmd[i].Parameters.Add("STATUS", SqlDbType.Char);
                p_STATUS.Direction = ParameterDirection.Input;
                p_STATUS.Value = "0";
                i++;
               
            }
        }
        objDC.MakeTransaction(cmd);
    }

    public void MergeData(GridView gr)
    {
        int i = 0;
        SqlCommand[] cmd;
        cmd = new SqlCommand[gr.Rows.Count];
        foreach (GridViewRow gRow in gr.Rows)
        {
            string strAttndDate = gRow.Cells[1].Text.Trim();
            strAttndDate = strAttndDate.Split(' ')[0];

            cmd[i] = new SqlCommand(GetMergeSQLString(gRow.Cells[3].Text.Trim()));
            cmd[i].CommandType = CommandType.Text;

            SqlParameter p_USERNAME = cmd[i].Parameters.Add("USERNAME", SqlDbType.Char);
            p_USERNAME.Direction = ParameterDirection.Input;
            p_USERNAME.Value = gRow.Cells[0].Text.Trim();

            SqlParameter p_LOGINTIME = cmd[i].Parameters.Add("LOGINTIME", SqlDbType.DateTime);
            p_LOGINTIME.Direction = ParameterDirection.Input;
            p_LOGINTIME.Value = gRow.Cells[1].Text.Trim();

            SqlParameter p_LogoutTime = cmd[i].Parameters.Add("LogoutTime", DBNull.Value);
            p_LogoutTime.Direction = ParameterDirection.Input;
            p_LogoutTime.IsNullable = true;
            if(Common.CheckNullString(gRow.Cells[2].Text)!="")
                p_LogoutTime.Value = gRow.Cells[2].Text.Trim();

            if (gRow.Cells[3].Text.Trim() == "Y")
            {
                SqlParameter p_ATTNDDATE = cmd[i].Parameters.Add("ATTNDDATE", SqlDbType.DateTime);
                p_ATTNDDATE.Direction = ParameterDirection.Input;
                p_ATTNDDATE.Value = strAttndDate;
            }
            i++;
        }

        this.MakeTransaction(cmd);
        SqlCommand cmd1 = new SqlCommand("UPDATE AttendanceRecordTextFile  SET STATUS='1' WHERE STATUS='0'");
        cmd1.CommandType = CommandType.Text;
        objDC.ExecuteQuery(cmd1);


    }

    public string GetMergeSQLString(string IsExist)
    {
        string strSQL = "";
        if (IsExist == "Y")
        {
            strSQL = "UPDATE LoginLogout SET LOGINTIME=@LOGINTIME, LogoutTime=@LogoutTime WHERE CONVERT(Varchar(10),LOGINTIME,111)=@ATTNDDATE AND USERNAME=@USERNAME";
        }
        else
        {
            strSQL = "INSERT INTO LoginLogout(USERNAME,LOGINTIME,LogoutTime) VALUES(@USERNAME,@LOGINTIME,@LogoutTime)";
        }

        return strSQL;
    }

    public void MakeTransaction(SqlCommand[] commands)
    {
        //SqlTransaction transaction;
        sqlConn = new SqlConnection(strConnection);
        using (sqlConn)
        {
            SqlTransaction transaction;
            sqlConn.Open();
            transaction = sqlConn.BeginTransaction();

            try
            {
                foreach (SqlCommand command in commands)
                {
                    if (command != null)
                    {
                        command.Transaction = transaction;
                        command.Connection = sqlConn;
                        command.ExecuteNonQuery();
                    }
                }
                transaction.Commit();
            }
            catch (SqlException ex)
            {
                transaction.Rollback();
                throw new HttpException(ex.ToString());

            }

            catch (Exception err)
            {
                transaction.Rollback();
                throw new HttpException(err.Message);
            }
            finally
            {
                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlConn.Close();
                }
            }
        }
    }
	public TextFileImpManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
