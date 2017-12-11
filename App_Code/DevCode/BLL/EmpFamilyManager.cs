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
///for EmpFamilyManager
/// </summary>
public class EmpFamilyManager
{
    DBConnector objDC = new DBConnector();
	
    
    //Select EmpEdu
    public DataTable SelectEmpFamily(string EmpID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpFamilyInfo");
        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;
        objDC.CreateDSFromProc(command, "EmpFamily");
        return objDC.ds.Tables["EmpFamily"];
    }
    public void DeleteEmpFamily(string EmpID,string StrFamilyName)
    {
        SqlCommand command = new SqlCommand("proc_Delete_EmpFamily");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        SqlParameter p_FamilyName = command.Parameters.Add("FamilyName", SqlDbType.Char);
        p_FamilyName.Direction = ParameterDirection.Input;
        p_FamilyName.Value = StrFamilyName;


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
    public void InsertEmpFamily(clsEmpFamilyInfo  objEmpFam, string strIsUpdate, string strIsDelete)
    {
        // sproc functionality
        SqlCommand command = new SqlCommand("proc_Insert_EmpFamilyInfo");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = objEmpFam.EmpID ;

        SqlParameter p_FmId = command.Parameters.Add("FmId", SqlDbType.BigInt);
        p_FmId.Direction = ParameterDirection.Input;
        p_FmId.Value = objEmpFam.FmId;

        SqlParameter p_fmName = command.Parameters.Add("fmName", SqlDbType.VarChar);
        p_fmName.Direction = ParameterDirection.Input;
        p_fmName.Value = objEmpFam.fmName;

        SqlParameter p_Fmocc = command.Parameters.Add("Fmocc", SqlDbType.VarChar);
        p_Fmocc.Direction = ParameterDirection.Input;
        p_Fmocc.Value = objEmpFam.Fmocc;

        SqlParameter p_frRelation = command.Parameters.Add("frRelation", SqlDbType.VarChar);
        p_frRelation.Direction = ParameterDirection.Input;
        p_frRelation.Value = objEmpFam.frRelation;

        SqlParameter p_FmDoB = command.Parameters.Add("FmDoB", DBNull.Value  );
        p_FmDoB.Direction = ParameterDirection.Input;
        p_FmDoB.IsNullable = true;
        if (objEmpFam.FmDoB != "")
            p_FmDoB.Value = Common.ReturnDate(objEmpFam.FmDoB);

        SqlParameter p_FmBloodGrp = command.Parameters.Add("FmBloodGrp", SqlDbType.VarChar);
        p_FmBloodGrp.Direction = ParameterDirection.Input;
        p_FmBloodGrp.Value = objEmpFam.FmBloodGrp;


        SqlParameter p_Isdependent = command.Parameters.Add("Isdependent", SqlDbType.Char);
        p_Isdependent.Direction = ParameterDirection.Input;
        p_Isdependent.Value = objEmpFam.Isdependent;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = objEmpFam.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = objEmpFam.InsertedDate;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = strIsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = strIsDelete;

        SqlParameter p_InsuranceID = command.Parameters.Add("InsuranceID", SqlDbType.VarChar);
        p_InsuranceID.Direction = ParameterDirection.Input;
        p_InsuranceID.Value = objEmpFam.InsuranceID;

        SqlParameter p_InclusionDate = command.Parameters.Add("InclusionDate", DBNull.Value);
        p_InclusionDate.Direction = ParameterDirection.Input;
        p_InclusionDate.IsNullable = true;
        if (string.IsNullOrEmpty(objEmpFam.InclusionDate)==false)
            p_InclusionDate.Value = Common.ReturnDate(objEmpFam.InclusionDate);

        SqlParameter p_RenewalDate = command.Parameters.Add("RenewalDate", DBNull.Value);
        p_RenewalDate.Direction = ParameterDirection.Input;
        p_RenewalDate.IsNullable = true;
        if (string.IsNullOrEmpty(objEmpFam.RenewalDate) == false)
            p_RenewalDate.Value = Common.ReturnDate(objEmpFam.RenewalDate);

        SqlParameter p_EmpPicLoc = command.Parameters.Add("EmpPicLoc", SqlDbType.VarChar);
        p_EmpPicLoc.Direction = ParameterDirection.Input;
        p_EmpPicLoc.Value = objEmpFam.EmpPicLoc;


        try
        {
            objDC.ExecuteQuery(command ) ;
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

}
