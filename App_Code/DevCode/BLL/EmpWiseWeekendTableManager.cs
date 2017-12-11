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
/// Summary description for EmpWiseWeekendTableManager
/// </summary>
public class EmpWiseWeekendTableManager
{
    DBConnector objDC = new DBConnector();

    public DataTable GetEmployee(string EmpID, string strDivision, string strSBU, string strDept, string strGrade, string strDesgId,
        string strFullName, string strCardNo, string strEmpAbbrName, string strSearchBy, string strUserId,string strAttndDate)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpWiseWeekend");

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = Convert.ToInt32(strDivision);

        SqlParameter p_SbuId = command.Parameters.Add("SbuId", SqlDbType.BigInt);
        p_SbuId.Direction = ParameterDirection.Input;
        p_SbuId.Value = Convert.ToInt32(strSBU);

        SqlParameter p_DeptId = command.Parameters.Add("DeptId", SqlDbType.BigInt);
        p_DeptId.Direction = ParameterDirection.Input;
        p_DeptId.Value = Convert.ToInt32(strDept);

        SqlParameter p_GradeId = command.Parameters.Add("GradeId", SqlDbType.BigInt);
        p_GradeId.Direction = ParameterDirection.Input;
        p_GradeId.Value = Convert.ToInt32(strGrade);

        SqlParameter p_DesgId = command.Parameters.Add("DesgId", SqlDbType.BigInt);
        p_DesgId.Direction = ParameterDirection.Input;
        p_DesgId.Value = Convert.ToInt32(strDesgId);

        SqlParameter p_FullName = command.Parameters.Add("FullName", SqlDbType.VarChar);
        p_FullName.Direction = ParameterDirection.Input;
        p_FullName.Value = strFullName;
        //p_FullName.Value = "'" + strFullName + "'";

        SqlParameter p_CardNo = command.Parameters.Add("CardNo", SqlDbType.VarChar);
        p_CardNo.Direction = ParameterDirection.Input;
        p_CardNo.Value = strCardNo;

        SqlParameter p_EmpAbbrName = command.Parameters.Add("EmpAbbrName", SqlDbType.VarChar);
        p_EmpAbbrName.Direction = ParameterDirection.Input;
        p_EmpAbbrName.Value = strEmpAbbrName;

        SqlParameter p_SearchBy = command.Parameters.Add("SearchBy", SqlDbType.Char);
        p_SearchBy.Direction = ParameterDirection.Input;
        p_SearchBy.Value = strSearchBy;

        SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
        p_UserId.Direction = ParameterDirection.Input;

        if (string.IsNullOrEmpty(EmpID) == true)
            p_UserId.Value = "'" + strUserId + "'";
        else
            p_UserId.Value = strUserId;



        SqlParameter p_AttndDate = command.Parameters.Add("AttndDate", SqlDbType.DateTime);
        p_AttndDate.Direction = ParameterDirection.Input;
        p_AttndDate.Value = strAttndDate;

        objDC.CreateDSFromProc(command, "EmpWiseWeekend");
        return objDC.ds.Tables["EmpWiseWeekend"];
    }


    public void InsertEmpWiseWeekendData(GridView grv,string strAttnDate,string strInsBY,string strInsDate)
    {
        SqlCommand[] command;// = new SqlCommand("proc_Select_EmpWiseWeekend");
        command = new SqlCommand[grv.Rows.Count];

        int i=0;
        foreach (GridViewRow gRow in grv.Rows)
        {
            if (Common.CheckNullString(gRow.Cells[6].Text) != "")
            {
                command[i] = new SqlCommand("proc_Insert_EmpWiseWeekend");
                command[i].CommandType = CommandType.StoredProcedure;

                SqlParameter p_EmpID = command[i].Parameters.Add("EmpID", SqlDbType.Char);
                p_EmpID.Direction = ParameterDirection.Input;
                p_EmpID.Value = gRow.Cells[1].Text.Trim();

                SqlParameter p_AttndDate = command[i].Parameters.Add("AttndDate", SqlDbType.DateTime);
                p_AttndDate.Direction = ParameterDirection.Input;
                p_AttndDate.Value = strAttnDate;

                SqlParameter p_Status = command[i].Parameters.Add("Status", SqlDbType.Char);
                p_Status.Direction = ParameterDirection.Input;
                p_Status.Value = gRow.Cells[6].Text.Trim();

                SqlParameter p_ExtraTimeWorked = command[i].Parameters.Add("ExtraTimeWorked", SqlDbType.Decimal);
                p_ExtraTimeWorked.Direction = ParameterDirection.Input;
                p_ExtraTimeWorked.Value = gRow.Cells[7].Text.Trim();

                SqlParameter p_InsertedBy = command[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                p_InsertedBy.Direction = ParameterDirection.Input;
                p_InsertedBy.Value = strInsBY;

                SqlParameter p_InsertedDate = command[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                p_InsertedDate.Direction = ParameterDirection.Input;
                p_InsertedDate.Value = strInsDate;
                i++;

            }
        }
        try
        {
            objDC.MakeTransaction(command);
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


	public EmpWiseWeekendTableManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
