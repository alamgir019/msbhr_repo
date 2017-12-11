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
/// Summary description for PlanAccLineManager
/// </summary>
public class PlanAccLineManager
{
    DBConnector objDC = new DBConnector();
    #region Insert, Update , Delele
    public void InsertData(string strAccLineID, string strAccLine, string strDesc, string strIsActive,string strIsUpdate,string strInsBy,string strInsDate)
    {
        string strSQL = "";
        if (strIsUpdate == "N")
            strSQL = "INSERT INTO PLANACCLINE(ACCLINEID,ACCLINE,DESCRIP,ISACTIVE,INSERTEDBY,INSERTEDDATE) "
                + " VALUES(@ACCLINEID,@ACCLINE,@DESCRIP,@ISACTIVE,@INSERTEDBY,@INSERTEDDATE)";
        else
            strSQL = "UPDATE PLANACCLINE SET ACCLINE=@ACCLINE,DESCRIP=@DESCRIP,ISACTIVE=@ISACTIVE,INSERTEDBY=@INSERTEDBY,INSERTEDDATE=@INSERTEDDATE "
                    + " WHERE ACCLINEID=@ACCLINEID";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        SqlParameter p_ACCLINEID = cmd.Parameters.Add("ACCLINEID", SqlDbType.BigInt);
        p_ACCLINEID.Direction = ParameterDirection.Input;
        p_ACCLINEID.Value = strAccLineID;

        SqlParameter p_ACCLINE = cmd.Parameters.Add("ACCLINE", SqlDbType.Char);
        p_ACCLINE.Direction = ParameterDirection.Input;
        p_ACCLINE.Value = strAccLine;

        SqlParameter p_DESCRIP = cmd.Parameters.Add("DESCRIP", SqlDbType.VarChar);
        p_DESCRIP.Direction = ParameterDirection.Input;
        p_DESCRIP.Value = strDesc;

        SqlParameter p_ISACTIVE = cmd.Parameters.Add("ISACTIVE", SqlDbType.Char);
        p_ISACTIVE.Direction = ParameterDirection.Input;
        p_ISACTIVE.Value = strIsActive;

        SqlParameter p_INSERTEDBY = cmd.Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = strInsBy;

        SqlParameter p_INSERTEDDATE = cmd.Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDDATE.Direction = ParameterDirection.Input;
        p_INSERTEDDATE.Value = strInsDate;

        objDC.ExecuteQuery(cmd);
    }

    public void DeleteData(string strAccLineID)
    {
        string strSQL = "DELETE FROM PLANACCLINE WHERE ACCLINEID=@ACCLINEID";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        SqlParameter p_ACCLINEID = cmd.Parameters.Add("ACCLINEID", SqlDbType.BigInt);
            p_ACCLINEID.Direction = ParameterDirection.Input;
            p_ACCLINEID.Value = strAccLineID;
        objDC.ExecuteQuery(cmd);
    }
    #endregion
    #region Select
    public DataTable SelectAccLineData(string strAccLineId,string strIsActive)
    {
        string strSQL = "SELECT * FROM PLANACCLINE WHERE 1<>2 ";
        if (strAccLineId != "0")
            strSQL = strSQL + " AND ACCLINEID=@ACCLINEID ";
        if (strIsActive != "")
            strSQL = strSQL + " AND ISACTIVE=@ISACTIVE ";
        strSQL = strSQL + " ORDER BY ACCLINE ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        if (strAccLineId != "0")
        {
            SqlParameter p_ACCLINEID = cmd.Parameters.Add("ACCLINEID", SqlDbType.BigInt);
            p_ACCLINEID.Direction = ParameterDirection.Input;
            p_ACCLINEID.Value = strAccLineId;
        }
        if (strIsActive != "")
        {
            SqlParameter p_ISACTIVE = cmd.Parameters.Add("ISACTIVE", SqlDbType.Char);
            p_ISACTIVE.Direction = ParameterDirection.Input;
            p_ISACTIVE.Value = strIsActive;
        }

        objDC.CreateDT(cmd, "AccLineData");
        return objDC.ds.Tables["AccLineData"];
    }
#endregion
    public PlanAccLineManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
