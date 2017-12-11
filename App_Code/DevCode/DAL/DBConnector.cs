using System;
using System.Web.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Linq; 

/// <summary>
/// The class DBConnector is used for maintain the functionality of DB.
/// </summary>
public class DBConnector
{
    private string strConnection = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;// works fine
    private SqlConnection OraConnection;

    private SqlCommand com = new SqlCommand();
    private SqlDataAdapter da = new SqlDataAdapter();
    public SqlDataReader dr;
    public DataSet ds = new DataSet();
    
    /// <summary>
    /// This Constructor Initialize SqlConnection.
    /// </summary>
    public DBConnector()
    {
        OraConnection = new SqlConnection(strConnection); 
    }


    /// <summary>
    /// This method Open the Database Connection.
    /// </summary>
    public void CreateConnection()
    {
        OraConnection.Open();             
    }

    /// <summary>
    /// This method Close the database Connection.
    /// </summary>
    public void CloseConnection()
    {
        OraConnection.Close();
    }

    
    /// <summary>
    /// This method creates a Dataset with table
    /// </summary>
    /// <param name="SQLQueryDS">Query string</param>
    /// <param name="TableName">Table name</param>
    public void CreateDS(string SQLQueryDS, string TableName)//, SqlCommand com, SqlDataAdapter da)
    {
        OraConnection = new SqlConnection(strConnection);
        com.CommandText = SQLQueryDS;
        com.Connection = OraConnection;
        da.SelectCommand = com;
        da.Fill(ds, TableName);           
        CloseConnection();
    }


    /// <summary>
    /// This method creates a Dataset with table from procedure
    /// </summary>
    /// <param name="SQLQueryDS">Query string</param>
    /// <param name="TableName">Table name</param>
    public void CreateDSFromProc(SqlCommand cmd, string TableName)//, SqlCommand com, SqlDataAdapter da)
    {
        try
        {
            OraConnection = new SqlConnection(strConnection);
           
            OraConnection.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = OraConnection;
            cmd.CommandTimeout = 120;
            da.SelectCommand = cmd;

            da.Fill(ds, TableName);
            CloseConnection();
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            if (OraConnection.State == ConnectionState.Open)
            {
                CloseConnection();
            }
        }
    }
    
    /// <summary>
    /// This method creates a DATATABLE with table
    /// </summary>
    /// <param name="SQLQueryDS">Query string</param>
    /// <param name="TableName">Table name</param>
    public DataTable CreateDT(string SQLQueryDT, string TableName)//, SqlCommand com, SqlDataAdapter da)
    {
        try
        {
            //OraConnection.Open(); 
            OraConnection = new SqlConnection(strConnection);
            OraConnection.Open();
            com.CommandText = SQLQueryDT;
            com.Connection = OraConnection;
            da.SelectCommand = com;
            da.Fill(ds, TableName);
            OraConnection.Close();
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            if (OraConnection.State == ConnectionState.Open)
            {
                CloseConnection();
            }
        }        
        return ds.Tables[TableName];
    }

    public DataTable CreateDT(SqlCommand command, string TableName)//, SqlCommand com, SqlDataAdapter da)
    {
        try
        {
            //OraConnection.Open(); 
            OraConnection = new SqlConnection(strConnection);
            OraConnection.Open();
            command.CommandType = CommandType.Text;
            command.Connection = OraConnection;
            da.SelectCommand = command;
            da.Fill(ds, TableName);

            OraConnection.Close();
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            if (OraConnection.State == ConnectionState.Open)
            {
                CloseConnection();
            }
        }

        return ds.Tables[TableName];
    }

    /// <summary>
    /// This method create design time data table.
    /// </summary>
    /// <param name="SQLQueryDS"></param>
    /// <param name="TableName"></param>
    //public void CreateDSReport(string SQLQueryDS, string TableName)//, SqlCommand com, SqlDataAdapter da)
    //{
    //    CreateConnection();

    //    com.CommandText = SQLQueryDS;
    //    com.Connection = sqlConnection;
    //    da.SelectCommand = com;
    //    da.Fill(dsReport, TableName);

    //    CloseConnection();
    //}
    /// <summary>
    /// This method returns SqlDataReader
    /// </summary>
    /// <param name="SQLQueryDR"></param>
    /// <returns></returns>
    public SqlDataReader CreateDR(string SQLQueryDR)
    {

        CreateConnection(); 
        com.CommandText = SQLQueryDR;
        com.Connection = OraConnection;
        dr = com.ExecuteReader();
        //CloseConnection();

        return dr;
    }

    /// <summary>
    /// This method can execute an Insert, delete or update query
    /// </summary>
    /// <param name="sqlCmd">The query to execute</param>

    public void ExecuteQuery(SqlCommand Cmd)
    {
        DataTable datatable = new DataTable();
        SqlTransaction transaction;
        OraConnection = new SqlConnection(strConnection); 
        using (OraConnection)
        {
            OraConnection.Open();
            transaction = OraConnection.BeginTransaction();

            try
            {
                Cmd.Transaction = transaction;
                Cmd.Connection = OraConnection;
                Cmd.ExecuteNonQuery();
                transaction.Commit();
                OraConnection.Close();
            }
            catch (SqlException ex)
            {
                transaction.Rollback();
                throw new HttpException(ex.ToString());
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new HttpException("SQL:" + Cmd.CommandText + " Error found" + ex.ToString());
            }
            finally
            {
                if (OraConnection.State == ConnectionState.Open)
                {
                    CloseConnection();
                }
            }
        }
    }


    /// <summary>
    /// This method can execute an Insert, delete or update query
    /// </summary>
    /// <param name="sqlCmd">The query to execute</param>

    public string ExecuteQueryRetMsg(SqlCommand Cmd)
    {
        string strMsg = "";
        DataTable datatable = new DataTable();
        SqlTransaction transaction;

        using (OraConnection)
        {

            OraConnection.Open();
            transaction = OraConnection.BeginTransaction();

            try
            {
                Cmd.Transaction = transaction;
                Cmd.Connection = OraConnection;
                Cmd.ExecuteNonQuery();
                strMsg = Cmd.Parameters["p_Msg"].Value.ToString();
                transaction.Commit();
                OraConnection.Close();


            }
            catch (SqlException ex)
            {
                transaction.Rollback();
                throw new HttpException(ex.ToString());

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new HttpException("SQL:" + Cmd.CommandText + " Error found" + ex.ToString());
            }
            finally
            {
                if (OraConnection.State == ConnectionState.Open)
                {
                    CloseConnection();
                }
            }


        }
        return strMsg;
    }

    /// <summary>
    /// This method can execute an Insert, delete or update multiple query
    /// </summary>
    /// <param name="sqlCmd">The query to execute</param>
    public void MakeTransaction(SqlCommand[] commands)
    {
        int i = 0;
        //SqlTransaction transaction;
        OraConnection = new SqlConnection(strConnection);
        using (OraConnection)
        {
            SqlTransaction transaction;
            OraConnection.Open();
            transaction = OraConnection.BeginTransaction();

            try
            {
                foreach (SqlCommand command in commands)
                {
                    if (command != null)
                    {
                        command.Transaction = transaction;
                        command.Connection = OraConnection;
                        command.ExecuteNonQuery();
                        i++;
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
                int j = i;
                transaction.Rollback();
                throw new HttpException(err.Message);
            }
            finally
            {
                if (OraConnection.State == ConnectionState.Open)
                {
                    CloseConnection();
                }
            }
        }
    }

    public void MakeTransaction(List<SqlCommand> commands)
    {
        int i = 0;
        //SqlTransaction transaction;
        OraConnection = new SqlConnection(strConnection);
        using (OraConnection)
        {
            SqlTransaction transaction;
            OraConnection.Open();
            transaction = OraConnection.BeginTransaction();

            try
            {
                foreach (SqlCommand command in commands)
                {
                    if (command != null)
                    {
                        command.Transaction = transaction;
                        command.Connection = OraConnection;
                        command.ExecuteNonQuery();
                        i++;
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
                int j = i;
                transaction.Rollback();
                throw new HttpException(err.Message);
            }
            finally
            {
                if (OraConnection.State == ConnectionState.Open)
                {
                    CloseConnection();
                }
            }
        }
    }

    /// <summary>
    /// This methos is use for Retrieving the Maximum Id.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="splitter"></param>
    /// <returns></returns>
    public long GerMaxIDNumber(string tbName, string field)
    {
        try
        {
            using (OraConnection)
            {
                OraConnection.Open();
                long maxIDField = 0;
                string strSQL = "select max(" + field + ") from " + tbName + " where (" + field + ") <>99999";
                SqlCommand cmd = new SqlCommand(strSQL, OraConnection);
                //maxIDField =Convert.ToInt64(cmd.ExecuteScalar());

                if (Convert.IsDBNull(cmd.ExecuteScalar()))
                {
                    maxIDField = 0;
                }
                else
                {
                    maxIDField = Convert.ToInt64(cmd.ExecuteScalar());
                }
                cmd = null;
                OraConnection.Close();
                return maxIDField + 1;
            }
        }

        catch (SqlException ex)
        {
            throw new HttpException(ex.ToString());
        }
        catch (Exception ex)
        {
            throw new HttpException("SQL: Error found" + ex.ToString());
        }
        finally
        {
            if (OraConnection.State == ConnectionState.Open)
            {
                CloseConnection();
            }
        }
    }


    public string GetMaxIdVar(string tbName, string field)
    {
        try
        {
            using (OraConnection)
            {
                CreateConnection();
                String maxIDField = "";
                int num = 0;
                string substr = "";
                string y1 = "";
                string y2 = "";
                DateTime dt = DateTime.Now;
                string y = dt.Year.ToString();
                string m = dt.Month.ToString();
                string d = dt.Day.ToString();
                if (m.Length == 1)
                {
                    m = "0" + m;
                }
                if (d.Length == 1)
                {
                    d = "0" + d;
                }
                y1 = y.Substring(0, 1);
                y2 = y.Substring(2, 2);
                y = y1 + y2;
                string thisyeaar = y + "" + m + "" + d;
                string strSQL = "select max(" + field + ") from " + tbName + " where " + field + " Like'" + thisyeaar + "%'";
                SqlCommand cmd = new SqlCommand(strSQL, OraConnection);
                string strMaxID = this.GetScalarVal(cmd);
                if (string.IsNullOrEmpty(strMaxID)==true)
                {
                    thisyeaar = thisyeaar + "001";
                }
                else
                {
                    maxIDField = strMaxID;
                    int intCardLength = Convert.ToInt32(maxIDField.Length);
                    substr = maxIDField.Substring(7,3).ToString();
                    num = Convert.ToInt16(substr) + 1;
                    thisyeaar = thisyeaar + num.ToString().PadLeft(3, '0');
                }
                cmd = null;
                CloseConnection();
                return thisyeaar;
            }
        }

        catch (SqlException ex)
        {
            throw new Exception(ex.ToString());
        }
        catch (Exception ex)
        {
            throw new Exception("SQL: Error found" + ex.ToString());
        }
        finally
        {
            if (OraConnection.State == ConnectionState.Open)
            {
                CloseConnection();
            }
        }
    }

    public string GetMaxIdVar2(string tbName, string field)
    {
        try
        {
            using (OraConnection)
            {
                CreateConnection();
                String maxIDField = "";
                int num = 0;
                string substr = "";
                string y1 = "";
                string y2 = "";
                DateTime dt = DateTime.Now;
                string y = dt.Year.ToString();
                string m = dt.Month.ToString();
                string d = dt.Day.ToString();
                if (m.Length == 1)
                {
                    m = "0" + m;
                }
                if (d.Length == 1)
                {
                    d = "0" + d;
                }
                y1 = y.Substring(0, 1);
                y2 = y.Substring(2, 2);
                y = y1 + y2;
                string thisyeaar = y + "" + m + "" + d;
                string strSQL = "select max(" + field + ") from " + tbName + " where " + field + " Like'" + thisyeaar + "%'";
                SqlCommand cmd = new SqlCommand(strSQL, OraConnection);

                if (Convert.IsDBNull(cmd.ExecuteScalar()))
                {
                    thisyeaar = thisyeaar + "000001";
                }
                else
                {
                    maxIDField = Convert.ToString(cmd.ExecuteScalar());
                    int intCardLength = Convert.ToInt32(maxIDField.Length);
                    substr = maxIDField.Substring(7, 6).ToString();
                    num = Convert.ToInt16(substr) + 1;
                    thisyeaar = thisyeaar + num.ToString().PadLeft(6, '0');
                }
                cmd = null;
                CloseConnection();
                return thisyeaar;
            }
        }

        catch (SqlException ex)
        {
            throw new Exception(ex.ToString());
        }
        catch (Exception ex)
        {
            throw new Exception("SQL: Error found" + ex.ToString());
        }
        finally
        {
            if (OraConnection.State == ConnectionState.Open)
            {
                CloseConnection();
            }
        }
    }

    public string GetMaxIdVar(string tbName, string field, int inLen)
    {
        try
        {
            using (OraConnection)
            {
                CreateConnection();
                string maxIDField = "";

                int num = 0;
                string strSQL = "select max(" + field + ") from " + tbName;
                SqlCommand cmd = new SqlCommand(strSQL, OraConnection);

                if (Convert.IsDBNull(cmd.ExecuteScalar()))
                {
                    maxIDField = "1".PadLeft(inLen, '0');
                }
                else
                {
                    maxIDField = Convert.ToString(cmd.ExecuteScalar());
                    int intCardLength = Convert.ToInt32(maxIDField.Length);
                    num = Convert.ToInt16(maxIDField) + 1;
                    maxIDField = num.ToString().PadLeft(inLen, '0');
                }
                cmd = null;
                CloseConnection();
                return maxIDField;
            }
        }

        catch (SqlException ex)
        {
            throw new Exception(ex.ToString());
        }
        catch (Exception ex)
        {
            throw new Exception("SQL: Error found" + ex.ToString());
        }
        finally
        {
            if (OraConnection.State == ConnectionState.Open)
            {
                CloseConnection();
            }
        }
    }
    public string GetScalarVal(SqlCommand Cmd)
    {
        try
        {
            using (OraConnection = new SqlConnection(strConnection))
            {
                OraConnection.Open();
                Cmd.Connection = OraConnection;
                string result = Convert.ToString(Cmd.ExecuteScalar());
                OraConnection.Close();
                return result;
            }
        }
        catch (SqlException ex)
        {
            throw new HttpException(ex.ToString());
        }
        catch (Exception ex)
        {
            throw new HttpException("SQL:" + Cmd.CommandText + " Error found" + ex.ToString());
        }
    }

    public string GetReaderValue(SqlCommand Cmd)
    {
        string result = "";
        try
        {
            using (OraConnection = new SqlConnection(strConnection))
            {
                OraConnection.Open();
                Cmd.Connection = OraConnection;
                SqlDataReader Odr = Cmd.ExecuteReader();
                if (Odr.HasRows)
                {
                    Odr.Read();
                    result = Convert.ToString(Odr.GetString(0));
                    Odr.Close();
                    Odr.Dispose();
                }

                OraConnection.Close();

            }

        }
        catch (SqlException ex)
        {
            throw new HttpException(ex.ToString());
        }
        catch (Exception ex)
        {
            throw new HttpException("SQL:" + Cmd.CommandText + " Error found" + ex.ToString());
        }
        return result;
    }

    public bool IsDuplicate(string TableName, string ValueField, string TestValueField, string IdField, string TestIdField, bool isUpdate)
    {
        string strSQL = "";
        if (isUpdate == false)
            strSQL = "SELECT " + ValueField + " FROM " + TableName + " WHERE " + ValueField + " = '" + TestValueField + "'";
        else
            strSQL = "SELECT " + ValueField + " FROM " + TableName + " WHERE " + ValueField + " = '" + TestValueField + "' AND " + IdField + " <> '" + TestIdField + "'";

        SqlConnection objCon = new SqlConnection(strConnection);
        SqlCommand cmd = new SqlCommand(strSQL, objCon);
        try
        {
            using (objCon)
            {
                objCon.Open();
                if (cmd.ExecuteScalar() != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                //cmd = null; 
                //objCon.Close();
            }
        }

        catch (SqlException ex)
        {
            throw new Exception(ex.ToString());
        }
        catch (Exception ex)
        {
            throw new Exception("SQL: Error found" + ex.ToString());
        }
        finally
        {
            cmd = null;
            objCon.Close();
        }

    }

    //public bool GetUser(string userName, string userPassword)
    //{

    //    try
    //    {

    //        using (SqlConnection objCon = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
    //        {

    //            objCon.Open();
    //            string sql = "";
    //            sql = @" SELECT userid FROM userinfo WHERE USERID=@userid AND password=@password AND IsDeleted='N'";
    //            SqlCommand cmd = new SqlCommand(sql, objCon);
    //            cmd.CommandType = CommandType.Text;
    //            cmd.Parameters.Add("userid", SqlDbType.VarChar).Value = userName;
    //            cmd.Parameters.Add("password", SqlDbType.VarChar).Value = userPassword;

    //            string result = Convert.ToString(cmd.ExecuteScalar());
    //            return result.ToString().Trim().Equals(userName) ? true : false;
    //            //objCon.Close();
    //        }
    //    }

    //    catch (SqlException ex)
    //    {
    //        throw new HttpException(ex.ToString());
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new HttpException("SQL: Error found" + ex.ToString());
    //    }
    //}

    //Sulata 18.11
    public bool GetUser(string userName, string userPassword)
    {
        try
        {
            using (SqlConnection objCon = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
            {
                objCon.Open();
                string sql = "";
                sql = @"SELECT u.userid,u.DivisionId,u.SBUId FROM userinfo u
                        LEFT OUTER JOIN DivisionList d ON u.DivisionId=d.DivisionId
                        LEFT OUTER JOIN SBUList s ON u.SBUId=s.SBUId
                        WHERE u.USERID=@userid AND u.password=@password AND u.IsDeleted='N'";
                //DataTable dtUser = new DataTable();
                //UserManager objUserMgr = new UserManager();
                //dtUser = objUserMgr.SelectUserInfo(userName, "Y");

                SqlCommand cmd = new SqlCommand(sql, objCon);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("userid", SqlDbType.VarChar).Value = userName;
                cmd.Parameters.Add("password", SqlDbType.VarChar).Value = userPassword;                

                string result = Convert.ToString(cmd.ExecuteScalar());
                return result.ToString().Trim().Equals(userName) ? true : false;
                //return DivisionId.ToString() ;
                //objCon.Close();
            }
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

    public string GetPassword(string userName, string userPassword)
    {
        try
        {
            using (SqlConnection objCon = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
            {
                objCon.Open();
                string sql = "";
                sql = @" SELECT password FROM userinfo WHERE userid=@userid AND password=@password AND IsDeleted='N'";
                SqlCommand cmd = new SqlCommand(sql, objCon);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("userid", SqlDbType.VarChar).Value = userName;
                cmd.Parameters.Add("password", SqlDbType.VarChar).Value = userPassword;

                string result = Convert.ToString(cmd.ExecuteScalar());
                return result.ToString().Equals(userName) ? result : "";
                //objCon.Close();
            }
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
    public long GerMaxIDNumberWithCast(string tbName, string field)
    {
        try
        {
            using (OraConnection)
            {
                OraConnection.Open();
                long maxIDField = 0;
                string strSQL = "select max( cast(" + field + " as Numeric)) from " + tbName;
                SqlCommand cmd = new SqlCommand(strSQL, OraConnection);
                //maxIDField =Convert.ToInt64(cmd.ExecuteScalar());

                if (Convert.IsDBNull(cmd.ExecuteScalar()))
                {
                    maxIDField = 0;

                }
                else
                {
                    maxIDField = Convert.ToInt64(cmd.ExecuteScalar());

                }


                cmd = null;
                OraConnection.Close();
                return maxIDField + 1;
            }
        }

        catch (SqlException ex)
        {
            throw new HttpException(ex.ToString());
        }
        catch (Exception ex)
        {
            throw new HttpException("SQL: Error found" + ex.ToString());
        }
        finally
        {
            if (OraConnection.State == ConnectionState.Open)
            {
                CloseConnection();
            }
        }

    }



    // New method for dataSet Return 

    public DataSet Get_Rpt_SalReconDetails(SqlCommand cmd, DataSet dsRpt)//, SqlCommand com, SqlDataAdapter da)
    {
        try
        {
            OraConnection = new SqlConnection(strConnection);
            OraConnection.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = OraConnection;
            da.SelectCommand = cmd;
            da.Fill(dsRpt);
            CloseConnection();
        }

        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            if (OraConnection.State == ConnectionState.Open)
            {
                CloseConnection();

            }
        }
        return dsRpt;
    }

    public DataSet Get_Rpt_SalReconDetails(SqlCommand cmd)//, SqlCommand com, SqlDataAdapter da)
    {
        DataSet ds = new DataSet();
        try
        {
            OraConnection = new SqlConnection(strConnection);
            OraConnection.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = OraConnection;
            da.SelectCommand = cmd;
            da.Fill(ds);
            CloseConnection();

        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            if (OraConnection.State == ConnectionState.Open)
            {
                CloseConnection();

            }
        }
        return ds;
    }

    #region Query & Parameter Generator

    public void SaveDataTable(DataTable dtData, string CmdType)
    {
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd = GenerateDML(dtData, CmdType);
            ExecuteQuery(cmd);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }

    public SqlCommand GenerateDML(DataTable dtData, string CmdType)
    {
        string strSQL = "";
        string strColumns = "";
        string strValues = "";
        string strWhere = "";
        int unColCount = 0;

        switch (CmdType)
        {
            case "I":
                for (int i = 0; i < dtData.Columns.Count; i++)
                {
                    if (strColumns == "")
                    {
                        strColumns = dtData.Columns[i].ColumnName.ToString();
                        strValues = "@" + strColumns;
                    }
                    else
                    {
                        strColumns = strColumns + ", " + dtData.Columns[i].ColumnName.ToString();
                        strValues = strValues + ",@" + dtData.Columns[i].ColumnName.ToString();
                    }

                }
                strSQL = "INSERT INTO " + dtData.TableName.ToString() + "(" + strColumns + ") VALUES(" + strValues + ")";
                break;
            case "U":
                for (int i = 0; i < dtData.Columns.Count; i++)
                {
                    DataColumn[] primCols = dtData.PrimaryKey;
                    if (dtData.Columns[i].Unique == true || this.FindInColCollections(primCols, dtData.Columns[i].ColumnName) == true)
                    {
                        if (strWhere == "")
                        {
                            strWhere = dtData.Columns[i].ColumnName.ToString() + "=@" + dtData.Columns[i].ColumnName.ToString();
                        }
                        else
                        {
                            strWhere = " AND " + dtData.Columns[i].ColumnName.ToString() + "=@" + dtData.Columns[i].ColumnName.ToString();
                        }
                        unColCount++;
                    }
                    else
                    {
                        if (strColumns == "")
                        {
                            strColumns = dtData.Columns[i].ColumnName.ToString() + "=@" + dtData.Columns[i].ColumnName.ToString();
                        }
                        else
                        {
                            strColumns = strColumns + ", " + dtData.Columns[i].ColumnName.ToString() + "=@" + dtData.Columns[i].ColumnName.ToString();
                        }
                    }

                }
                strSQL = "UPDATE " + dtData.TableName.ToString() + " SET " + strColumns + " WHERE " + strWhere;
                // strSQL = " UPDATE GENERAL_INFO SET EMP_NM=:EMP_NM, F_NM=:F_NM WHERE GOVT_ID=:GOVT_ID ";
                break;
        }


        SqlParameter[] param = new SqlParameter[dtData.Columns.Count];
        //SqlParameter[] param = new SqlParameter[2];
        SqlCommand cmd = new SqlCommand(strSQL);
        int[] posUnCol = new int[unColCount];
        int j = 0;
        int p = 0;

        switch (CmdType)
        {
            case "I":
                for (int i = 0; i < dtData.Columns.Count; i++)
                {
                    param[i] = new SqlParameter();

                    if ((string.IsNullOrEmpty(dtData.Rows[0][i].ToString().Trim()) == true) || (dtData.Rows[0][i].ToString().Trim() == "01-01-0001 12:00:00 AM"))
                    {
                        param[i] = cmd.Parameters.Add(dtData.Columns[i].ColumnName.ToString(), DBNull.Value);
                    }
                    else
                    {
                        param[i] = cmd.Parameters.Add(dtData.Columns[i].ColumnName.ToString(), SetColDataType(dtData.Columns[i].DataType.Name));
                    }
                    param[i].Direction = ParameterDirection.Input;
                    if (dtData.Columns[i].AllowDBNull == true)
                    {
                        param[i].IsNullable = true;
                    }


                    if ((string.IsNullOrEmpty(dtData.Rows[0][i].ToString().Trim()) == false) && (dtData.Rows[0][i].ToString().Trim() != "01-01-0001 12:00:00 AM"))
                    {
                        if (dtData.Columns[i].DataType.Name == "DateTime")
                            //param[i].Value =Common.ReturnDateTime(dtData.Rows[0][i].ToString().Trim(),true,Constant.strDateFormat);
                            param[i].Value = dtData.Rows[0][i];
                        else
                            param[i].Value = dtData.Rows[0][i].ToString().Trim();

                    }

                }

                break;
            case "U":

                for (int i = 0; i < dtData.Columns.Count; i++)
                {
                    // Intializing the Columns Parameter First
                    DataColumn[] primCols = dtData.PrimaryKey;
                    if (dtData.Columns[i].Unique == false && this.FindInColCollections(primCols, dtData.Columns[i].ColumnName) == false)
                    {

                        param[j] = new SqlParameter();
                        if ((string.IsNullOrEmpty(dtData.Rows[0][i].ToString().Trim()) == true) || (dtData.Rows[0][i].ToString().Trim() == "01-01-0001 12:00:00 AM"))
                        {
                            param[j] = cmd.Parameters.Add(dtData.Columns[i].ColumnName.ToString(), DBNull.Value);
                        }
                        else
                        {
                            param[j] = cmd.Parameters.Add(dtData.Columns[i].ColumnName.ToString(), SetColDataType(dtData.Columns[i].DataType.Name));
                        }
                        param[j].Direction = ParameterDirection.Input;
                        if (dtData.Columns[i].AllowDBNull == true)
                        {
                            param[j].IsNullable = true;
                        }
                        if ((string.IsNullOrEmpty(dtData.Rows[0][i].ToString().Trim()) == false) && (dtData.Rows[0][i].ToString().Trim() != "01-01-0001 12:00:00 AM"))
                        {
                            if (dtData.Columns[i].DataType.Name == "DateTime")
                                //param[i].Value =Common.ReturnDateTime(dtData.Rows[0][i].ToString().Trim(),true,Constant.strDateFormat);
                                param[j].Value = dtData.Rows[0][i];
                            else
                                param[j].Value = dtData.Rows[0][i].ToString().Trim();

                        }

                        j++;
                    }
                    else
                    {
                        posUnCol[p] = i;
                        p++;
                    }

                }

                // Intializing the WHERE Condition Parameter at the end
                for (int k = 0; k < posUnCol.Length; k++)
                {
                    param[j] = new SqlParameter();
                    param[j] = cmd.Parameters.Add(dtData.Columns[posUnCol[k]].ColumnName.ToString(), SetColDataType(dtData.Columns[posUnCol[k]].DataType.Name));
                    param[j].Direction = ParameterDirection.Input;
                    if (dtData.Columns[p].AllowDBNull == true)
                    {
                        param[j].IsNullable = true;
                    }
                    if ((string.IsNullOrEmpty(dtData.Rows[0][posUnCol[k]].ToString().Trim()) == false) && (dtData.Rows[0][posUnCol[k]].ToString().Trim() != "01-01-0001 12:00:00 AM"))
                    {
                        if (dtData.Columns[posUnCol[k]].DataType.Name == "DateTime")
                            param[j].Value = dtData.Rows[0][posUnCol[k]];
                        else
                            param[j].Value = dtData.Rows[0][posUnCol[k]].ToString().Trim();

                    }
                }

                break;

        }
        return cmd;
    }

    public SqlCommand GenerateDML(DataRow dRow, string CmdType)
    {
        string strSQL = "";
        string strColumns = "";
        string strValues = "";
        string strWhere = "";
        int unColCount = 0;

        switch (CmdType)
        {
            case "I":
                for (int i = 0; i < dRow.Table.Columns.Count; i++)
                {
                    if (strColumns == "")
                    {
                        strColumns = dRow.Table.Columns[i].ColumnName.ToString();
                        strValues = "@" + strColumns;
                    }
                    else
                    {
                        strColumns = strColumns + ", " + dRow.Table.Columns[i].ColumnName.ToString();
                        strValues = strValues + ",@" + dRow.Table.Columns[i].ColumnName.ToString();
                    }

                }
                strSQL = "INSERT INTO " + dRow.Table.TableName.ToString() + "(" + strColumns + ") VALUES(" + strValues + ")";
                break;
            case "U":
                for (int i = 0; i < dRow.Table.Columns.Count; i++)
                {
                    DataColumn[] primCols = dRow.Table.PrimaryKey;
                    if (dRow.Table.Columns[i].Unique == true || this.FindInColCollections(primCols, dRow.Table.Columns[i].ColumnName) == true)
                    {
                        if (strWhere == "")
                        {
                            strWhere = dRow.Table.Columns[i].ColumnName.ToString() + "=@" + dRow.Table.Columns[i].ColumnName.ToString();
                        }
                        else
                        {
                            strWhere = strWhere + " AND " + dRow.Table.Columns[i].ColumnName.ToString() + "=@" + dRow.Table.Columns[i].ColumnName.ToString();
                        }
                        unColCount++;
                    }
                    else
                    {
                        if (strColumns == "")
                        {
                            strColumns = dRow.Table.Columns[i].ColumnName.ToString() + "=@" + dRow.Table.Columns[i].ColumnName.ToString();
                        }
                        else
                        {
                            strColumns = strColumns + ", " + dRow.Table.Columns[i].ColumnName.ToString() + "=@" + dRow.Table.Columns[i].ColumnName.ToString();
                        }
                    }

                }
                strSQL = "UPDATE " + dRow.Table.TableName.ToString() + " SET " + strColumns + " WHERE " + strWhere;
                // strSQL = " UPDATE GENERAL_INFO SET EMP_NM=:EMP_NM, F_NM=:F_NM WHERE GOVT_ID=:GOVT_ID ";
                break;

        }


        SqlParameter[] param = new SqlParameter[dRow.Table.Columns.Count];
        //SqlParameter[] param = new SqlParameter[2];
        SqlCommand cmd = new SqlCommand(strSQL);
        int[] posUnCol = new int[unColCount];
        int j = 0;
        int p = 0;

        switch (CmdType)
        {
            case "I":
                for (int i = 0; i < dRow.Table.Columns.Count; i++)
                {
                    param[i] = new SqlParameter();

                    if ((string.IsNullOrEmpty(dRow[i].ToString().Trim()) == true) || (dRow[i].ToString().Trim() == "01-01-0001 12:00:00 AM"))
                    {
                        param[i] = cmd.Parameters.Add(dRow.Table.Columns[i].ColumnName.ToString(), DBNull.Value);
                    }
                    else
                    {
                        param[i] = cmd.Parameters.Add(dRow.Table.Columns[i].ColumnName.ToString(), SetColDataType(dRow.Table.Columns[i].DataType.Name));
                    }
                    param[i].Direction = ParameterDirection.Input;
                    if (dRow.Table.Columns[i].AllowDBNull == true)
                    {
                        param[i].IsNullable = true;
                    }


                    if ((string.IsNullOrEmpty(dRow[i].ToString().Trim()) == false) && (dRow[i].ToString().Trim() != "01-01-0001 12:00:00 AM"))
                    {
                        if (dRow.Table.Columns[i].DataType.Name == "DateTime")
                            //param[i].Value =Common.ReturnDateTime(dtData.Rows[0][i].ToString().Trim(),true,Constant.strDateFormat);
                            param[i].Value = dRow[i];
                        else
                            param[i].Value = dRow[i].ToString().Trim();

                    }

                }

                break;
            case "U":

                for (int i = 0; i < dRow.Table.Columns.Count; i++)
                {
                    // Intializing the Columns Parameter First

                    DataColumn[] primCols = dRow.Table.PrimaryKey;
                    if (dRow.Table.Columns[i].Unique == false && this.FindInColCollections(primCols, dRow.Table.Columns[i].ColumnName) == false)
                    {

                        param[j] = new SqlParameter();
                        if ((string.IsNullOrEmpty(dRow[i].ToString().Trim()) == true) || (dRow[i].ToString().Trim() == "01-01-0001 12:00:00 AM"))
                        {
                            param[j] = cmd.Parameters.Add(dRow.Table.Columns[i].ColumnName.ToString(), DBNull.Value);
                        }
                        else
                        {
                            param[j] = cmd.Parameters.Add(dRow.Table.Columns[i].ColumnName.ToString(), SetColDataType(dRow.Table.Columns[i].DataType.Name));
                        }
                        param[j].Direction = ParameterDirection.Input;
                        if (dRow.Table.Columns[i].AllowDBNull == true)
                        {
                            param[j].IsNullable = true;
                        }
                        if ((string.IsNullOrEmpty(dRow[i].ToString().Trim()) == false) && (dRow[i].ToString().Trim() != "01-01-0001 12:00:00 AM"))
                        {
                            if (dRow.Table.Columns[i].DataType.Name == "DateTime")
                                //param[i].Value =Common.ReturnDateTime(dtData.Rows[0][i].ToString().Trim(),true,Constant.strDateFormat);
                                param[j].Value = dRow[i];
                            else
                                param[j].Value = dRow[i].ToString().Trim();

                        }

                        j++;
                    }
                    else
                    {
                        posUnCol[p] = i;
                        p++;
                    }

                }

                // Intializing the WHERE Condition Parameter at the end
                for (int k = 0; k < posUnCol.Length; k++)
                {
                    param[j] = new SqlParameter();
                    param[j] = cmd.Parameters.Add(dRow.Table.Columns[posUnCol[k]].ColumnName.ToString(), SetColDataType(dRow.Table.Columns[posUnCol[k]].DataType.Name));
                    param[j].Direction = ParameterDirection.Input;
                    if (dRow.Table.Columns[p].AllowDBNull == true)
                    {
                        param[j].IsNullable = true;
                    }
                    if ((string.IsNullOrEmpty(dRow[posUnCol[k]].ToString().Trim()) == false) && (dRow[posUnCol[k]].ToString().Trim() != "01-01-0001 12:00:00 AM"))
                    {
                        if (dRow.Table.Columns[posUnCol[k]].DataType.Name == "DateTime")
                            param[j].Value = dRow[posUnCol[k]];
                        else
                            param[j].Value = dRow[posUnCol[k]].ToString().Trim();

                    }
                }

                break;


        }
        return cmd;
    }


    public SqlCommand DeleteData(DataRow KeyRow, string DeleteTableName)
    {
        SqlCommand delCmd = new SqlCommand();
        string strWhere = "";
        string strSQL = "";
        DataColumn[] primCols = KeyRow.Table.PrimaryKey;
        SqlParameter[] param = new SqlParameter[KeyRow.Table.PrimaryKey.Count()];
        for (int i = 0; i < KeyRow.Table.Columns.Count; i++)
        {
            if (KeyRow.Table.Columns[i].Unique == true || this.FindInColCollections(primCols, KeyRow.Table.Columns[i].ColumnName) == true)
            {
                if (strWhere == "")
                {
                    strWhere = KeyRow.Table.Columns[i].ColumnName.ToString() + "=@" + KeyRow.Table.Columns[i].ColumnName.ToString();
                }
                else
                {
                    strWhere = strWhere + " AND " + KeyRow.Table.Columns[i].ColumnName.ToString() + "=@" + KeyRow.Table.Columns[i].ColumnName.ToString();
                }
            }

        }
        strSQL = "DELETE FROM " + DeleteTableName + " WHERE " + strWhere;
        delCmd = new SqlCommand(strSQL);

        for (int k = 0; k < primCols.Length; k++)
        {
            param[k] = new SqlParameter();
            param[k] = delCmd.Parameters.Add(primCols.ElementAt(k).ColumnName.ToString(), SetColDataType(primCols.ElementAt(k).DataType.Name));
            param[k].Direction = ParameterDirection.Input;
            if (primCols.ElementAt(k).AllowDBNull == true)
            {
                param[k].IsNullable = true;
            }
            if ((string.IsNullOrEmpty(KeyRow[primCols.ElementAt(k).ToString().Trim()].ToString().Trim()) == false) && (KeyRow[primCols.ElementAt(k).ToString().Trim()].ToString().Trim() != "01-01-0001 12:00:00 AM"))
            {
                if (primCols.ElementAt(k).DataType.Name == "DateTime")
                    param[k].Value = KeyRow[primCols.ElementAt(k).ToString().Trim()];
                else
                    param[k].Value = KeyRow[primCols.ElementAt(k).ToString().Trim()];

            }
        }

        return delCmd;

    }
    protected SqlDbType SetColDataType(string name)
    {
        SqlDbType dbType = new SqlDbType();
        switch (name)
        {
            case "Decimal":
                dbType = SqlDbType.Decimal;
                break;
            case "String":
                dbType = SqlDbType.VarChar;
                break;
            case "DateTime":
                dbType = SqlDbType.Date;
                break;
            case "Char":
                dbType = SqlDbType.Char;
                break;
        }
        return dbType;
    }


    protected bool FindInColCollections(DataColumn[] cols, string strColName)
    {
        bool retValue = false;
        foreach (DataColumn col in cols)
        {
            if (strColName == col.ColumnName)
            {
                retValue = true;
                break;
            }
        }
        return retValue;
    }
    #endregion

}

