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
/// Summary description for EmpEdu
/// </summary>
public class EmpEduManager
{
    DBConnector objDC = new DBConnector();
	
    
    //Select EmpEdu
    public DataTable SelectEmpEdu(string EmpID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpEducation");
        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;
        objDC.CreateDSFromProc(command, "EmpEdu");
        return objDC.ds.Tables["EmpEdu"];
    }
    //public void  InsertEmpEdu(clsEmpEdu objEmp, string strIsUpdate,string strIsDelete)
    //{

    //        SqlCommand command = new SqlCommand("proc_Insert_EmpEdu");
    //        command.CommandType = CommandType.StoredProcedure;

    //        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
    //        p_EmpID.Direction = ParameterDirection.Input;
    //        p_EmpID.Value = objEmp.EmpID;


    //        SqlParameter p_Eduid = command.Parameters.Add("Eduid", SqlDbType.Char);
    //        p_Eduid.Direction = ParameterDirection.Input;
    //        p_Eduid.Value = objEmp.Eduid;


    //        SqlParameter p_Certiname = command.Parameters.Add("Certiname ", SqlDbType.VarChar);
    //        p_Certiname.Direction = ParameterDirection.Input;
    //        p_Certiname.Value = objEmp.Certiname ;

    //        SqlParameter p_Rsltname = command.Parameters.Add("Rsltname", SqlDbType.VarChar);
    //        p_Rsltname.Direction = ParameterDirection.Input;
    //        p_Rsltname.Value = objEmp.Rsltname;

    //        SqlParameter p_LevelId = command.Parameters.Add("LevelId", DBNull.Value);
    //        p_LevelId.Direction = ParameterDirection.Input;
    //        p_LevelId.IsNullable = true;
    //        if (objEmp.LevelId != "")
    //            p_LevelId.Value = objEmp.LevelId;

    //        SqlParameter p_InstName = command.Parameters.Add("InstName", SqlDbType.VarChar);
    //        p_InstName.Direction = ParameterDirection.Input;
    //        p_InstName.Value = objEmp.InstName;

    //        SqlParameter p_PassedYear = command.Parameters.Add("PassedYear", SqlDbType.VarChar);
    //        p_PassedYear.Direction = ParameterDirection.Input;
    //        p_PassedYear.Value = objEmp.PassedYear;



    //        SqlParameter p_MazorArea = command.Parameters.Add("MazorArea", SqlDbType.VarChar);
    //        p_MazorArea.Direction = ParameterDirection.Input;
    //        p_MazorArea.Value = objEmp.MazorArea;
        

    //        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //        p_InsertedBy.Direction = ParameterDirection.Input;
    //        p_InsertedBy.Value = objEmp.InsertedBy;

    //        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //        p_InsertedDate.Direction = ParameterDirection.Input;
    //        p_InsertedDate.Value = objEmp.InsertedDate;

    //        SqlParameter p_isUpdate = command.Parameters.Add("isUpdate", SqlDbType.Char);
    //        p_isUpdate.Direction = ParameterDirection.Input;
    //        p_isUpdate.Value = strIsUpdate ;

    //        SqlParameter p_isDelete = command.Parameters.Add("isDelete", SqlDbType.Char);
    //        p_isDelete.Direction = ParameterDirection.Input;
    //        p_isDelete.Value = strIsDelete;          



    //    try
    //    {
    //        objDC.ExecuteQuery(command);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw (ex);
    //    }
    //    finally
    //    {
    //        command = null;
    //    }      
               
                
              
    //}

}
