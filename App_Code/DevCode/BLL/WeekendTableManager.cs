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
/// Summary description for WeekendTableManager
/// </summary>
public class WeekendTableManager
{
    DBConnector objDC = new DBConnector();

    public void InsertWeekend(string IsUpdate, string IsDelete, Weekend objWeek)
    {
        SqlCommand command = new SqlCommand("proc_Insert_Weekend");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_WeekEndID = command.Parameters.Add("WeekEndID", SqlDbType.BigInt);
        p_WeekEndID.Direction = ParameterDirection.Input;
        p_WeekEndID.Value = objWeek.WeekEndID;

        SqlParameter p_WEPackName = command.Parameters.Add("WEPackName", SqlDbType.VarChar);
        p_WEPackName.Direction = ParameterDirection.Input;
        p_WEPackName.Value = objWeek.WEPackName;

        SqlParameter p_PoliDesc = command.Parameters.Add("WESun", SqlDbType.Char);
        p_PoliDesc.Direction = ParameterDirection.Input;
        p_PoliDesc.Value = objWeek.WESun;

        SqlParameter p_WEMon = command.Parameters.Add("WEMon", SqlDbType.Char);
        p_WEMon.Direction = ParameterDirection.Input;
        p_WEMon.Value = objWeek.WEMon;

        SqlParameter p_WETues = command.Parameters.Add("WETues", SqlDbType.Char);
        p_WETues.Direction = ParameterDirection.Input;
        p_WETues.Value = objWeek.WETues;

        SqlParameter p_WEWed = command.Parameters.Add("WEWed", SqlDbType.Char);
        p_WEWed.Direction = ParameterDirection.Input;
        p_WEWed.Value = objWeek.WEWed;

        SqlParameter p_WETue = command.Parameters.Add("WETue", SqlDbType.Char);
        p_WETue.Direction = ParameterDirection.Input;
        p_WETue.Value = objWeek.WETue;

        SqlParameter p_WEFri = command.Parameters.Add("WEFri", SqlDbType.Char);
        p_WEFri.Direction = ParameterDirection.Input;
        p_WEFri.Value = objWeek.WEFri;

        SqlParameter p_IWESat = command.Parameters.Add("WESat", SqlDbType.Char);
        p_IWESat.Direction = ParameterDirection.Input;
        p_IWESat.Value = objWeek.WESat;

        SqlParameter p_TotalWeekEnd = command.Parameters.Add("TotalWeekEnd", SqlDbType.BigInt);
        p_TotalWeekEnd.Direction = ParameterDirection.Input;
        p_TotalWeekEnd.Value = objWeek.TotalWeekEnd;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = objWeek.IsActive;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = objWeek.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = objWeek.InsertedDate;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

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
    public DataTable GetData()
    {
        SqlCommand command = new SqlCommand("proc_Select_Weekend");
        command.CommandType = CommandType.StoredProcedure;
        SqlParameter p_WeekEndID = command.Parameters.Add("WeekEndID", SqlDbType.BigInt);
        p_WeekEndID.Direction = ParameterDirection.Input;
        p_WeekEndID.Value = "0";
        objDC.CreateDSFromProc(command, "Weekend");
        return objDC.ds.Tables["Weekend"];
    }

	public WeekendTableManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
