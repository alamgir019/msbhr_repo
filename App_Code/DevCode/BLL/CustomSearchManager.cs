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
/// Summary description for CustomSearchManager
/// </summary>
public class CustomSearchManager
{
    private string strConnection = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;// works fine
    private SqlConnection OraConnection;
    private SqlDataAdapter da = new SqlDataAdapter();
    public DataSet dsDesc = new DataSet();
    SqlCommand com = new SqlCommand();

    public DataTable GetQueryResult(string strSQL)
    {
        CreateDS(strSQL, "GetQueryResult");
        return dsDesc.Tables["GetQueryResult"];
    }

    public DataTable GetSchemaDescription(string strSchemaName)
    {
        string strSQL = "sp_help " + strSchemaName;
        CreateDS(strSQL, "SchemaDescription");
        return dsDesc.Tables[1];
    }

    public void CreateDS(string SQLQueryDS, string TableName)//, SqlCommand com, SqlDataAdapter da)
    {

        OraConnection = new SqlConnection(strConnection);
        com.CommandText = SQLQueryDS;
        com.Connection = OraConnection;
        da.SelectCommand = com;
        da.Fill(dsDesc, TableName);
        if (OraConnection.State == ConnectionState.Open)
        {
            OraConnection.Close();
        }
    }

	public CustomSearchManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
