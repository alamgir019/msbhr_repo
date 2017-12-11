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
/// Summary description for MedicalManager
/// </summary>
public class MedicalManager
{
    DBConnector objDC = new DBConnector();

    //clsEmpLeaveProfile objEmpLevPro = new clsEmpLeaveProfile();

    public DataTable SelectMedicalPeriod()
    {
        SqlCommand cmd = new SqlCommand("proc_Select_MedicalPeriod");

        objDC.CreateDSFromProc(cmd, "MedicalPeriod");
        return objDC.ds.Tables["MedicalPeriod"];
    }

    public void UpdateMedicalProfile(GridView grEmployee)
    {
        SqlCommand[] cmd = new SqlCommand[grEmployee.Rows.Count];
        int i = 0;
        foreach (GridViewRow gRow in grEmployee.Rows)
        {
            CheckBox chBox = new CheckBox();
            chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            if (chBox.Checked == true)
            {
                cmd[i] = new SqlCommand("proc_Medical_Renew");
                cmd[i].CommandType = CommandType.StoredProcedure;

                SqlParameter p_EmpId = cmd[i].Parameters.Add("ProfileEmpId", SqlDbType.Char);
                p_EmpId.Direction = ParameterDirection.Input;
                p_EmpId.Value = gRow.Cells[2].Text.Trim();
            }
            i++;
        }
        try
        {
            objDC.MakeTransaction(cmd);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            cmd = null;
        }
    }
	public MedicalManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}