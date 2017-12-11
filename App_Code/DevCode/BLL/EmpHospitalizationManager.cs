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
/// Summary description for EmpHospitalizationManager
/// </summary>
public class EmpHospitalizationManager
{
    DBConnector objDC = new DBConnector();

    //Select Employee Family Members Including himself
    public DataTable SelectEmpAndFamily(string EmpID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpAndFamilymMembers");
        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;
        objDC.CreateDSFromProc(command, "EmpAndFamily");
        return objDC.ds.Tables["EmpAndFamily"];
    }
	public EmpHospitalizationManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
