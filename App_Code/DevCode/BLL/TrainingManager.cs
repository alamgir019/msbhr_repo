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
using System.Collections.Generic;
 

/// <summary>
/// Summary description for TrainingManager
/// </summary>

public class TrainingManager
{

    
    DBConnector objDC = new DBConnector();
    #region Common Data Save
    public void SaveData(DataTable dtData, string CmdType)
    {
        try
        {
            objDC.SaveDataTable(dtData, CmdType);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }

    public void SaveMultiTableData(List<DataTable> dtList, string CmdType)
    {
        try
        {
            List<SqlCommand> lstCommand=new List<SqlCommand>();
            for(int i=0;i<dtList.Count;i++)
            {
                if(i==0)
                {
                    foreach(DataRow dRow in  dtList[i].Rows)
                    {
                        lstCommand.Add(objDC.GenerateDML(dRow,CmdType));
                    }
                }
                else
                {

                    if(CmdType=="U")
                    {
                        lstCommand.Add(objDC.DeleteData(dtList[0].Rows[0],dtList[i].TableName.ToString().Trim()));
                    }
                    foreach(DataRow dRow in  dtList[i].Rows)
                    {
                        lstCommand.Add(objDC.GenerateDML(dRow, CmdType == "U" ? "I" : CmdType));
                    }
                }
            }

            objDC.MakeTransaction(lstCommand);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }
    
    #endregion
   
    #region Training Category Setup
    public DataTable SelectTrainingCategory(string TrCategoryId)
    {
        SqlCommand command = new SqlCommand("proc_Select_TrCategory");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpID = command.Parameters.Add("TrCategoryId", SqlDbType.BigInt);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value =Convert.ToInt32(TrCategoryId);

        objDC.CreateDSFromProc(command, "tblTrCategory");
        return objDC.ds.Tables["tblTrCategory"];

    }


    public void InsertTrainingCategory(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_InUp_TrCategory");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("TrCategoryId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = clsCommon.ID;

        SqlParameter p_Name = command.Parameters.Add("TrCategoryName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = clsCommon.Name;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = clsCommon.IsActive;


        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = clsCommon.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = clsCommon.InsertedDate;


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


   #endregion


    public DataTable GetEmpData(string strEmpID, string strDeptId, string strUserId)
    {
        SqlCommand command = new SqlCommand("proc_Select_Roaster_Emp");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        SqlParameter p_DeptId = command.Parameters.Add("DeptId", SqlDbType.VarChar);
        p_DeptId.Direction = ParameterDirection.Input;
        p_DeptId.Value = strDeptId;

        SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
        p_UserId.Direction = ParameterDirection.Input;
        p_UserId.Value = strUserId;

        objDC.CreateDSFromProc(command, "RoasterEmp");
        return objDC.ds.Tables["RoasterEmp"];
    }

    public DataTable SelectTraining()
    {
        string strSQL = "SELECT * FROM TrainingList where IsDeleted='N' ORDER BY TrainingName";
        return objDC.CreateDT(strSQL, "TrainingList");

    }

    #region TrainingService Info
    // Insert or Update  or Delete Data of Appraisal Rating table  
    public void InsertTraining(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_InUp_TrainingList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("TrainingId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = clsCommon.ID;

        SqlParameter p_Name = command.Parameters.Add("TrainingName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = clsCommon.Name;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = clsCommon.IsActive;


        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = clsCommon.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = clsCommon.InsertedDate;


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


    public void InsertTrainingService(clsTrainingService clObjTS,clsCommonSetup objTrain,clsCommonSetup objResource,  string UserID, string strIsUpdate, string IsDelete)
    {       
        SqlCommand[] cmd = new SqlCommand[3];

        cmd[0] = new SqlCommand("proc_InUp_TrainingService");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_TraServiceID = cmd[0].Parameters.Add("TraServiceID", SqlDbType.BigInt);
        p_TraServiceID.Direction = ParameterDirection.Input;
        p_TraServiceID.Value = clObjTS.TraServiceID;

        SqlParameter p_EmpId = cmd[0].Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = clObjTS.EmpId;

        SqlParameter p_TranType = cmd[0].Parameters.Add("TrainType", SqlDbType.Char);
        p_TranType.Direction = ParameterDirection.Input;
        p_TranType.Value = clObjTS.TrainType;

        SqlParameter p_FiscalYrId = cmd[0].Parameters.Add("FiscalYrID", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = clObjTS.FiscalYrID;

        SqlParameter p_TrainingID = cmd[0].Parameters.Add("TrainingID", SqlDbType.BigInt);
        p_TrainingID.Direction = ParameterDirection.Input;
        p_TrainingID.Value = clObjTS.TrainingID;

        SqlParameter p_LAreaId = cmd[0].Parameters.Add("LAreaId", DBNull.Value);
        p_LAreaId.Direction = ParameterDirection.Input;
        p_LAreaId.IsNullable = true;
        if (clObjTS.LAreaId != "99999")
            p_LAreaId.Value = clObjTS.LAreaId;

        SqlParameter p_ResourcePersonId = cmd[0].Parameters.Add("ResourcePersonId", DBNull.Value);
        p_ResourcePersonId.Direction = ParameterDirection.Input;
        p_ResourcePersonId.IsNullable = true;
        if (clObjTS.ResourcePersonId != "99999")
            p_ResourcePersonId.Value = clObjTS.ResourcePersonId;

        SqlParameter p_CountryID = cmd[0].Parameters.Add("CountryID", DBNull.Value);
        p_CountryID.Direction = ParameterDirection.Input;
        p_CountryID.IsNullable = true;
        if (clObjTS.CountryID != "99999")
            p_CountryID.Value = clObjTS.CountryID;

        SqlParameter p_ContactDtl = cmd[0].Parameters.Add("ContactDtl", SqlDbType.VarChar);
        p_ContactDtl.Direction = ParameterDirection.Input;
        p_ContactDtl.Value = clObjTS.ContactDtl;

        SqlParameter p_TrnStartDate = cmd[0].Parameters.Add("TrnStartDate", DBNull.Value);
        p_TrnStartDate.Direction = ParameterDirection.Input;
        p_TrnStartDate.IsNullable = true;
        if (string.IsNullOrEmpty(clObjTS.TrnStartDate) == false)
            p_TrnStartDate.Value = Common.ReturnDate(clObjTS.TrnStartDate);

        SqlParameter p_TrnEndDate = cmd[0].Parameters.Add("TrnEndDate", DBNull.Value);
        p_TrnEndDate.Direction = ParameterDirection.Input;
        p_TrnEndDate.IsNullable = true;
        if (string.IsNullOrEmpty(clObjTS.TrnEndDate) == false)
            p_TrnEndDate.Value = Common.ReturnDate(clObjTS.TrnEndDate);

        SqlParameter p_Remarks = cmd[0].Parameters.Add("Remarks", SqlDbType.VarChar);
        p_Remarks.Direction = ParameterDirection.Input;
        p_Remarks.Value = clObjTS.Remarks;

        SqlParameter p_NeedType = cmd[0].Parameters.Add("NeedType", SqlDbType.Char);
        p_NeedType.Direction = ParameterDirection.Input;
        p_NeedType.Value = clObjTS.NeedType;

        SqlParameter p_RunningRate = cmd[0].Parameters.Add("RunningRate", DBNull.Value);
        p_RunningRate.Direction = ParameterDirection.Input;
        p_RunningRate.IsNullable = true;
        if (string.IsNullOrEmpty(clObjTS.RunningRate) == false)
            p_RunningRate.Value = Convert.ToDecimal(clObjTS.RunningRate);

        SqlParameter p_ReteToUse = cmd[0].Parameters.Add("RateToUse", DBNull.Value);
        p_ReteToUse.Direction = ParameterDirection.Input;
        p_ReteToUse.IsNullable = true;
        if (string.IsNullOrEmpty(clObjTS.RateToUse) == false)
            p_ReteToUse.Value = Convert.ToDecimal(clObjTS.RateToUse);

        SqlParameter p_ServAgreement = cmd[0].Parameters.Add("ServAgreement", SqlDbType.VarChar);
        p_ServAgreement.Direction = ParameterDirection.Input;
        p_ServAgreement.Value = clObjTS.ServAgreement;

        SqlParameter p_AgrStartDate = cmd[0].Parameters.Add("AgrStartDate", DBNull.Value);
        p_AgrStartDate.Direction = ParameterDirection.Input;
        p_AgrStartDate.IsNullable = true;
        if (string.IsNullOrEmpty(clObjTS.AgrStartDate) == false)
            p_AgrStartDate.Value = Common.ReturnDate(clObjTS.AgrStartDate);

        SqlParameter p_AgrEndDate = cmd[0].Parameters.Add("AgrEndDate", DBNull.Value);
        p_AgrEndDate.Direction = ParameterDirection.Input;
        p_AgrEndDate.IsNullable = true;
        if (string.IsNullOrEmpty(clObjTS.AgrEndDate) == false)
            p_AgrEndDate.Value = Common.ReturnDate(clObjTS.AgrEndDate);

        SqlParameter p_AgrPeriod = cmd[0].Parameters.Add("AgrPeriod", DBNull.Value);
        p_AgrPeriod.Direction = ParameterDirection.Input;
        p_AgrPeriod.IsNullable = true;
        if (string.IsNullOrEmpty(clObjTS.AgrPeriod) == false)
            p_AgrPeriod.Value = Convert.ToInt32(clObjTS.AgrPeriod);

        SqlParameter p_EstAgrAmtBDT = cmd[0].Parameters.Add("EstAgrAmtBDT", DBNull.Value);
        p_EstAgrAmtBDT.Direction = ParameterDirection.Input;
        p_EstAgrAmtBDT.IsNullable = true;
        if (string.IsNullOrEmpty(clObjTS.EstAgrAmtBDT) == false)
            p_EstAgrAmtBDT.Value = Convert.ToDecimal(clObjTS.EstAgrAmtBDT);

        SqlParameter p_EstAgrAmtUSD = cmd[0].Parameters.Add("EstAgrAmtUSD", DBNull.Value);
        p_EstAgrAmtUSD.Direction = ParameterDirection.Input;
        p_EstAgrAmtUSD.IsNullable = true;
        if (string.IsNullOrEmpty(clObjTS.EstAgrAmtUSD) == false)
            p_ReteToUse.Value = Convert.ToDecimal(clObjTS.EstAgrAmtUSD);

        SqlParameter p_ActAgrAmtBDT = cmd[0].Parameters.Add("ActAgrAmtBDT", DBNull.Value);
        p_ActAgrAmtBDT.Direction = ParameterDirection.Input;
        p_ActAgrAmtBDT.IsNullable = true;
        if (string.IsNullOrEmpty(clObjTS.ActAgrAmtBDT) == false)
            p_ActAgrAmtBDT.Value = Convert.ToDecimal(clObjTS.ActAgrAmtBDT);

        SqlParameter p_ActAgrAmtUSD = cmd[0].Parameters.Add("ActAgrAmtUSD", DBNull.Value);
        p_ActAgrAmtUSD.Direction = ParameterDirection.Input;
        p_ActAgrAmtUSD.IsNullable = true;
        if (string.IsNullOrEmpty(clObjTS.ActAgrAmtUSD) == false)
            p_ActAgrAmtUSD.Value = Convert.ToDecimal(clObjTS.ActAgrAmtUSD);

        SqlParameter p_AgrRemarks = cmd[0].Parameters.Add("AgrRemarks", SqlDbType.VarChar);
        p_AgrRemarks.Direction = ParameterDirection.Input;
        p_AgrRemarks.Value = clObjTS.AgrRemarks;

        SqlParameter p_TrainingCostBDT = cmd[0].Parameters.Add("TrainingCostBDT", DBNull.Value);
        p_TrainingCostBDT.Direction = ParameterDirection.Input;
        p_TrainingCostBDT.IsNullable = true;
        if (string.IsNullOrEmpty(clObjTS.TrainingCostBDT) == false)
            p_TrainingCostBDT.Value = Convert.ToDecimal(clObjTS.TrainingCostBDT);

        SqlParameter p_TrainingCostUSD = cmd[0].Parameters.Add("TrainingCostUSD", DBNull.Value);
        p_TrainingCostUSD.Direction = ParameterDirection.Input;
        p_TrainingCostUSD.IsNullable = true;
        if (string.IsNullOrEmpty(clObjTS.TrainingCostUSD) == false)
            p_TrainingCostUSD.Value = Convert.ToDecimal(clObjTS.TrainingCostUSD);

        SqlParameter p_SponsoredBy = cmd[0].Parameters.Add("SponsoredBy", SqlDbType.VarChar);
        p_SponsoredBy.Direction = ParameterDirection.Input;
        p_SponsoredBy.Value = clObjTS.SponsoredBy;

        SqlParameter p_SCCostPercent = cmd[0].Parameters.Add("SCCostPercent", DBNull.Value);
        p_SCCostPercent.Direction = ParameterDirection.Input;
        p_SCCostPercent.IsNullable = true;
        if (string.IsNullOrEmpty(clObjTS.SCCostPercent) == false)
            p_SCCostPercent.Value = Convert.ToDecimal(clObjTS.SCCostPercent);

        SqlParameter p_SCCostBDT = cmd[0].Parameters.Add("SCCostBDT", DBNull.Value);
        p_SCCostBDT.Direction = ParameterDirection.Input;
        p_SCCostBDT.IsNullable = true;
        if (string.IsNullOrEmpty(clObjTS.SCCostBDT) == false)
            p_SCCostBDT.Value = Convert.ToDecimal(clObjTS.SCCostBDT);

        SqlParameter p_SCCostUSD = cmd[0].Parameters.Add("SCCostUSD", DBNull.Value);
        p_SCCostUSD.Direction = ParameterDirection.Input;
        p_SCCostUSD.IsNullable = true;
        if (string.IsNullOrEmpty(clObjTS.SCCostUSD) == false)
            p_SCCostUSD.Value = Convert.ToDecimal(clObjTS.SCCostUSD);

        SqlParameter p_OtherCostPercent = cmd[0].Parameters.Add("OtherCostPercent", DBNull.Value);
        p_OtherCostPercent.Direction = ParameterDirection.Input;
        p_OtherCostPercent.IsNullable = true;
        if (string.IsNullOrEmpty(clObjTS.OtherCostPercent) == false)
            p_OtherCostPercent.Value = Convert.ToDecimal(clObjTS.OtherCostPercent);

        SqlParameter p_OtherCostPerBDT = cmd[0].Parameters.Add("OtherCostPerBDT", DBNull.Value);
        p_OtherCostPerBDT.Direction = ParameterDirection.Input;
        p_OtherCostPerBDT.IsNullable = true;
        if (string.IsNullOrEmpty(clObjTS.OtherCostPerBDT) == false)
            p_OtherCostPerBDT.Value = Convert.ToDecimal(clObjTS.OtherCostPerBDT);

        SqlParameter p_OtherCostPerUSD = cmd[0].Parameters.Add("OtherCostPerUSD", DBNull.Value);
        p_OtherCostPerUSD.Direction = ParameterDirection.Input;
        p_OtherCostPerUSD.IsNullable = true;
        if (string.IsNullOrEmpty(clObjTS.OtherCostPerUSD) == false)
            p_OtherCostPerUSD.Value = Convert.ToDecimal(clObjTS.OtherCostPerUSD);

        SqlParameter p_InsertedBy = cmd[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = UserID;

        SqlParameter p_InsertedDate = cmd[0].Parameters.Add("InsertedDate", DBNull.Value);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.IsNullable = true;
        p_InsertedDate.Value = Common.ReturnDate((System.DateTime.Now.ToString("dd/MM/yyyy")).ToString());

        SqlParameter p_IsUpdate = cmd[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = strIsUpdate;

        SqlParameter p_IsDelete = cmd[0].Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        if (objTrain.ID != null)
            cmd[1] = InsertTraining(objTrain);

        if (objResource.ID != null)
            cmd[2] = InsertResourcePerson(objResource);
        
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

    public bool IsDuplicateTrainingEntry(string strEmpId, string strTrainId, string strTrainStDate, string strTraServiceID)
    {
        string strTrainingID = "";
        string strSql="";

        if (strTraServiceID=="")
         strSql = "SELECT TrainingID From TrainingService Where EmpId='" + strEmpId + "' AND TrainingID=" + strTrainId 
            + " AND TrnStartDate='" + strTrainStDate + "'";
        else
             strSql = "SELECT TrainingID From TrainingService Where EmpId='" + strEmpId + "' AND TrainingID='" + strTrainId
            + "' AND TrnStartDate='" + strTrainStDate + "' AND TraServiceID<>" + strTraServiceID;
        SqlCommand cmd = new SqlCommand(strSql);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_ProjectCode = cmd.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_ProjectCode.Direction = ParameterDirection.Input;
        p_ProjectCode.Value = strEmpId;

        SqlParameter p_SHEADID = cmd.Parameters.Add("TrainingID", SqlDbType.VarChar);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = strTrainId;

        SqlParameter p_Salary = cmd.Parameters.Add("TrnStartDate", SqlDbType.VarChar);
        p_Salary.Direction = ParameterDirection.Input;
        p_Salary.Value = strTrainStDate;

        if (strTraServiceID != "")
        {
            SqlParameter p_TraServiceID = cmd.Parameters.Add("TraServiceID", SqlDbType.BigInt);
            p_TraServiceID.Direction = ParameterDirection.Input;
            p_TraServiceID.Value = strTraServiceID;
        }
        strTrainingID = objDC.GetScalarVal(cmd);

        if (strTrainingID == "")
            return false;
        else
            return true;       
    }

    private SqlCommand InsertTraining(clsCommonSetup clsCommon)
    {
        SqlCommand command = new SqlCommand("proc_InUp_TrainingList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("TrainingId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = clsCommon.ID;

        SqlParameter p_Name = command.Parameters.Add("TrainingName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = clsCommon.Name;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = clsCommon.IsActive;
        
        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = clsCommon.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = clsCommon.InsertedDate;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = "N";

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = "N";

        return command;
    }

    private SqlCommand InsertResourcePerson(clsCommonSetup clsCommon)
    {
        SqlCommand command = new SqlCommand("proc_Insert_ResourcePersonList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("ResourcePersonId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = clsCommon.ID;

        SqlParameter p_Name = command.Parameters.Add("ResourcePersonName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = clsCommon.Name;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = clsCommon.IsActive;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = clsCommon.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = clsCommon.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = clsCommon.InsertedDate;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = "N"; 

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = "N";

        return command;
    }


    public void InsertResourcePerson(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_ResourcePersonList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("ResourcePersonId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = clsCommon.ID;

        SqlParameter p_Name = command.Parameters.Add("ResourcePersonName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = clsCommon.Name;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = clsCommon.IsActive;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = clsCommon.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = clsCommon.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = clsCommon.InsertedDate;

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
    //public void DeleteServiceList(string TraServiceID)
    //{
    //    string strSQL = " Update TrainingService set IsDeleted='Y' where TraServiceID='" + TraServiceID+"'";
    //    objDC.ExecuteQuery(strSQL);

    //}

    #endregion

    #region TrainingNeedTier Info

    public void InsertTrainingNeedTier(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_InUp_TrainingNeedTier");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("TrainingNeedTierID", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = clsCommon.ID;

        SqlParameter p_Name = command.Parameters.Add("TrainingNeedTierName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = clsCommon.Name;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = clsCommon.IsActive;


        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = clsCommon.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = clsCommon.InsertedDate;


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

    public DataTable SelectTrainNeedTierList()
    {
        string strSQL = "SELECT * FROM TrainingNeedTier where IsDeleted='N' ORDER BY TrainingNeedTierName";
        return objDC.CreateDT(strSQL, "TrainingNeedTierList");

    }

    #endregion

    #region TrainingNeedSubType Info

    public void InsertTrainingSubType(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_InUp_TrainingSubType");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("TrainingSubTypeID", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = clsCommon.ID;

        SqlParameter p_Name = command.Parameters.Add("TrainingSubTypeName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = clsCommon.Name;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = clsCommon.IsActive;


        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = clsCommon.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = clsCommon.InsertedDate;


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

    public DataTable TrainingSubTypeList()
    {
        string strSQL = "SELECT * FROM TrainingSubType where IsDeleted='N' ORDER BY TrainingSubTypeName";
        return objDC.CreateDT(strSQL, "TrainingSubTypeList");
    }

    public DataTable TrnSubTypeListByType(Int32 TrmType)
    {
        string strSQL = @"SELECT TWST.*,ST.TrainingSubTypeName FROM  TainingTypeWiseSubtype TWST left join TrainingSubType ST
                        on TWST.TrainingSubTypeId=ST.TrainingSubTypeID where TrainingNeedTypeId=" + TrmType + " order by ST.TrainingSubTypeName";
        return objDC.CreateDT(strSQL, "TrainingSubTypeList");
    }

    public void InsertTrnNeedSubType(string TrainingNeedID, string EmpId, string TrainingNeedTierID, string TrainingNeedTypeID, string TrainingSubTypeID,
        string TrainingModeID, string TrainingYear, string isDeleted, string InsertedBy, string isUpdate)
    {

        SqlCommand command = new SqlCommand("proc_InUp_TrainingNeed");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_TrainingNeedID = command.Parameters.Add("TrainingNeedID", SqlDbType.BigInt);
        p_TrainingNeedID.Direction = ParameterDirection.Input;
        p_TrainingNeedID.Value = Convert.ToInt32(TrainingNeedID);

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_TrainingNeedTierID = command.Parameters.Add("TrainingNeedTierID", SqlDbType.BigInt);
        p_TrainingNeedTierID.Direction = ParameterDirection.Input;
        p_TrainingNeedTierID.Value = Convert.ToInt32(TrainingNeedTierID);


        SqlParameter p_TrainingNeedTypeID = command.Parameters.Add("TrainingNeedTypeID", SqlDbType.BigInt);
        p_TrainingNeedTypeID.Direction = ParameterDirection.Input;
        p_TrainingNeedTypeID.Value = Convert.ToInt32(TrainingNeedTypeID);

        SqlParameter p_TrainingSubTypeID = command.Parameters.Add("TrainingSubTypeID", DBNull.Value);
        p_TrainingSubTypeID.Direction = ParameterDirection.Input;

        p_TrainingSubTypeID.IsNullable = true;
        if (TrainingSubTypeID != "")
            p_TrainingSubTypeID.Value = Convert.ToInt32(TrainingSubTypeID);

        SqlParameter p_TrainingModeID = command.Parameters.Add("TrainingModeID", SqlDbType.BigInt);
        p_TrainingModeID.Direction = ParameterDirection.Input;
        p_TrainingModeID.Value = Convert.ToInt32(TrainingModeID);
        SqlParameter p_TrainingYear = command.Parameters.Add("TrainingYear", SqlDbType.BigInt);
        p_TrainingYear.Direction = ParameterDirection.Input;
        p_TrainingYear.Value = Convert.ToInt32(TrainingYear);
        SqlParameter p_isDeleted = command.Parameters.Add("isDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = isDeleted;
        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = InsertedBy;
        SqlParameter p_isUpdate = command.Parameters.Add("isUpdate", SqlDbType.Char);
        p_isUpdate.Direction = ParameterDirection.Input;
        p_isUpdate.Value = isUpdate;



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

    public DataTable SelectTrainingNeedList(string EmpId)
    {
        string strSQL = @"	Select TN.*,TNT.TrainingNeedTierName,TnTy.TrainingNeedTypeName,TST.TrainingSubTypeName,TM.TrainingModeName 	from TrainingNeed TN 
							left join TrainingNeedTier TNT on TN.TrainingNeedTierID=TNT.TrainingNeedTierID
							left join TrainingNeedType TnTy on TN.TrainingNeedTypeID=TnTy.TrainingNeedTypeID
							left join TrainingSubType TST on TN.TrainingSubTypeID=TST.TrainingSubTypeID
							left join TrainingMode TM on TN.TrainingModeID=TM.TrainingModeID  where TN.IsDeleted <>'Y' and  TN.EmpId='" + EmpId + "'";
        return objDC.CreateDT(strSQL, "TrainingNeedList");
    }

    #endregion

    // 20/10/2014
    #region TrainingNeedType Info

    public DataTable SelectTypeWiseSubType(Int32 TypeId)
    {
        SqlCommand command = new SqlCommand("proc_select_TrainingSubWiseType");

        SqlParameter p_LocationID = command.Parameters.Add("TrainingNeedTypeID", SqlDbType.BigInt);
        p_LocationID.Direction = ParameterDirection.Input;
        p_LocationID.Value = TypeId;

        objDC.CreateDSFromProc(command, "tblTrainingNeedType");
        return objDC.ds.Tables["tblTrainingNeedType"];
    }

    public DataTable SelectTrnNeedTypeList(Int32 TrainingNeedTypeId)
    {
        SqlCommand command = new SqlCommand("proc_Select_TrainingneedTypeList");

        SqlParameter p_DesgID = command.Parameters.Add("TrainingNeedTypeId", SqlDbType.BigInt);
        p_DesgID.Direction = ParameterDirection.Input;
        p_DesgID.Value = TrainingNeedTypeId;

        objDC.CreateDSFromProc(command, "tblTrainingNeedTypeList");
        return objDC.ds.Tables["tblTrainingNeedTypeList"];
    }

    public void InsTainingTypeWiseSubtype(GridView grLocation, Desigation dsg, string IsUpdate, string IsDelete, string IsActive)
    {
        SqlCommand[] cmd = new SqlCommand[grLocation.Rows.Count + 2];
        cmd[0] = new SqlCommand("proc_InUp_TrainingNeedType");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_DesignationID = cmd[0].Parameters.Add("TrainingNeedTypeID", SqlDbType.BigInt);
        p_DesignationID.Direction = ParameterDirection.Input;
        p_DesignationID.Value = dsg.DesgID;

        SqlParameter p_DesignationName = cmd[0].Parameters.Add("TrainingNeedTypeName", SqlDbType.VarChar);
        p_DesignationName.Direction = ParameterDirection.Input;
        p_DesignationName.Value = dsg.DesgName;

        //SqlParameter p_isDeleted = cmd[0].Parameters.Add("IsDeleted", SqlDbType.Char);
        //p_isDeleted.Direction = ParameterDirection.Input;
        //p_isDeleted.Value = dsg.IsDeleted;

        SqlParameter p_InsertedBy = cmd[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = dsg.InsertedBy;

        SqlParameter p_InsertedDate = cmd[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = dsg.InsertedDate;

        //SqlParameter p_LastUpdatedFrom = cmd[0].Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        //p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        //p_LastUpdatedFrom.Value = dsg.LastUpdatedFrom;

        SqlParameter p_IsUpdate = cmd[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = cmd[0].Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_IsActive = cmd[0].Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;

        cmd[1] = new SqlCommand("DELETE FROM TainingTypeWiseSubtype WHERE TrainingNeedTypeId = @TrainingNeedTypeId");
        cmd[1].CommandType = CommandType.Text;

        SqlParameter p_DesignationID2 = cmd[1].Parameters.Add("TrainingNeedTypeId", SqlDbType.BigInt);
        p_DesignationID2.Direction = ParameterDirection.Input;
        p_DesignationID2.Value = dsg.DesgID;

        int i = 2;
        foreach (GridViewRow gRow in grLocation.Rows)
        {
            CheckBox chkBox = (CheckBox)gRow.FindControl("chkBox");
            if (chkBox.Checked == true)
            {
                string query = "INSERT INTO TainingTypeWiseSubtype(TrainingNeedTypeId,TrainingSubTypeId,InsertedBy,InsertedDate) " +
                               "VALUES(@TrainingNeedTypeId,@TrainingSubTypeId,@InsertedBy,@InsertedDate)";
                cmd[i] = new SqlCommand(query);
                cmd[i].CommandType = CommandType.Text;

                SqlParameter p_DesignationID3 = cmd[i].Parameters.Add("TrainingNeedTypeId", SqlDbType.BigInt);
                p_DesignationID3.Direction = ParameterDirection.Input;
                p_DesignationID3.Value = dsg.DesgID;

                SqlParameter p_LocationId = cmd[i].Parameters.Add("TrainingSubTypeId", SqlDbType.BigInt);
                p_LocationId.Direction = ParameterDirection.Input;
                p_LocationId.Value = grLocation.DataKeys[gRow.RowIndex].Values[0].ToString();

                SqlParameter p_InsertedBy2 = cmd[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                p_InsertedBy2.Direction = ParameterDirection.Input;
                p_InsertedBy2.Value = dsg.InsertedBy;

                SqlParameter p_InsertedDate2 = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                p_InsertedDate2.Direction = ParameterDirection.Input;
                p_InsertedDate2.Value = dsg.InsertedDate;
                i++;
            }
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

    public void InsertTrainingType(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_InUp_TrainingSubType");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("TrainingSubTypeID", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = clsCommon.ID;

        SqlParameter p_Name = command.Parameters.Add("TrainingSubTypeName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = clsCommon.Name;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = clsCommon.IsActive;


        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = clsCommon.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = clsCommon.InsertedDate;


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

    public DataTable TrainingTypeList()
    {
        string strSQL = "SELECT * FROM TrainingSubType where IsDeleted='N' ORDER BY TrainingSubTypeName";
        return objDC.CreateDT(strSQL, "TrainingSubTypeList");

    }


    #endregion

    //public DataTable SelectDivisionWiseDistrict(Int32 DivisionId)
    //{
    //    SqlCommand command = new SqlCommand("proc_select_DivisionWiseDistrict");

    //    SqlParameter p_LocationID = command.Parameters.Add("DivisionId", SqlDbType.BigInt);
    //    p_LocationID.Direction = ParameterDirection.Input;
    //    p_LocationID.Value = DivisionId;

    //    objDC.CreateDSFromProc(command, "tblDivisionWiseDistrict");
    //    return objDC.ds.Tables["tblDivisionWiseDistrict"];
    //}

    #region TrainingMode Info

    public void InsertTrainingMode(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_InUp_TrainingMode");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("TrainingModeID", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = clsCommon.ID;

        SqlParameter p_Name = command.Parameters.Add("TrainingModeName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = clsCommon.Name;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = clsCommon.IsActive;


        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = clsCommon.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = clsCommon.InsertedDate;


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

    public DataTable SelectTrainMode()
    {
        string strSQL = "SELECT * FROM TrainingMode where IsDeleted='N' ORDER BY TrainingModeName";
        return objDC.CreateDT(strSQL, "TrainingMode");

    }


    #endregion

    #region OrientationTraining Info

    public void InsertOrientaTraining(string OriTrainingID, string EmpId, string strFirstDayOri, string strFirstDayOriDate, string strAngChiSeftyPol,
                string strOT, string strOTDate, string strRemark, string IsDelete, string InsertedBy, string IsUpdate)
    {

        SqlCommand command = new SqlCommand("proc_InUp_OrientationTraining");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_OriTrainingID = command.Parameters.Add("OriTrainingID", SqlDbType.BigInt);
        p_OriTrainingID.Direction = ParameterDirection.Input;
        p_OriTrainingID.Value = Convert.ToInt32(OriTrainingID);

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_FirstOrient = command.Parameters.Add("FirstOrient", DBNull.Value);
        p_FirstOrient.Direction = ParameterDirection.Input;
        p_FirstOrient.IsNullable = true;
        if (strFirstDayOri != "")
            p_FirstOrient.Value = strFirstDayOri;


        SqlParameter p_FirstOrientDate = command.Parameters.Add("FirstOrientDate", DBNull.Value);
        p_FirstOrientDate.Direction = ParameterDirection.Input;
        p_FirstOrientDate.IsNullable = true;
        if (strFirstDayOriDate != "")
            p_FirstOrientDate.Value = Common.ReturnDate(strFirstDayOriDate);

        SqlParameter p_AgeChiPoliOrient = command.Parameters.Add("AgeChiPoliOrient", DBNull.Value);
        p_AgeChiPoliOrient.Direction = ParameterDirection.Input;
        p_AgeChiPoliOrient.IsNullable = true;
        if (strAngChiSeftyPol != "")
            p_AgeChiPoliOrient.Value = strAngChiSeftyPol;

        SqlParameter p_OrienTraining = command.Parameters.Add("OrienTraining", DBNull.Value);
        p_OrienTraining.Direction = ParameterDirection.Input;
        p_OrienTraining.IsNullable = true;
        if (strOT != "")
            p_OrienTraining.Value = strOT;

        SqlParameter p_OrienTrainingDate = command.Parameters.Add("OrienTrainingDate", DBNull.Value);
        p_OrienTrainingDate.Direction = ParameterDirection.Input;
        p_OrienTrainingDate.IsNullable = true;
        if (strOTDate != "")
            p_OrienTrainingDate.Value = Common.ReturnDate(strOTDate);

        SqlParameter p_Remarks = command.Parameters.Add("Remarks", SqlDbType.VarChar);
        p_Remarks.Direction = ParameterDirection.Input;
        p_Remarks.Value = strRemark;

        SqlParameter p_isDeleted = command.Parameters.Add("isDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = IsDelete;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = InsertedBy;

        SqlParameter p_isUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_isUpdate.Direction = ParameterDirection.Input;
        p_isUpdate.Value = IsUpdate;

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

    public DataTable SelectOrientationTraining(string empid)
    {
        string strSQL = "SELECT * FROM OrientationTraining where IsDeleted='N' and EmpId='" + empid + "'";
        return objDC.CreateDT(strSQL, "OrienTrainingList");
    }

    #endregion



    #region TrTeainingList

    public void InsertTrainingList(GridView grLocation, clsTrTrainingList dsg, string IsActive, string IsUpdate, string IsDelete)
    {
        SqlCommand[] cmd = new SqlCommand[grLocation.Rows.Count + 2];
        cmd[0] = new SqlCommand("proc_InUp_TrTrainingList");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_DesignationID = cmd[0].Parameters.Add("TrainId", SqlDbType.BigInt);
        p_DesignationID.Direction = ParameterDirection.Input;
        p_DesignationID.Value = dsg.TrainId;

        SqlParameter p_TrCategoryId = cmd[0].Parameters.Add("TrCategoryId", SqlDbType.BigInt);
        p_TrCategoryId.Direction = ParameterDirection.Input;
        p_TrCategoryId.Value = dsg.TrCategoryId;

        SqlParameter p_TrainName = cmd[0].Parameters.Add("TrainName", SqlDbType.VarChar);
        p_TrainName.Direction = ParameterDirection.Input;
        p_TrainName.Value = dsg.TrainName;

        SqlParameter p_TentativeDay = cmd[0].Parameters.Add("TentativeDay", SqlDbType.BigInt);
        p_TentativeDay.Direction = ParameterDirection.Input;
        p_TentativeDay.Value = Convert.ToInt32(dsg.TentativeDay);

        SqlParameter p_IsInHouse = cmd[0].Parameters.Add("IsInHouse", SqlDbType.Char);
        p_IsInHouse.Direction = ParameterDirection.Input;
        p_IsInHouse.Value = dsg.IsInHouse;

        SqlParameter p_IsMedicos = cmd[0].Parameters.Add("IsMedicos", SqlDbType.Char);
        p_IsMedicos.Direction = ParameterDirection.Input;
        p_IsMedicos.Value = dsg.IsMedicos;

         SqlParameter p_IndvCost = cmd[0].Parameters.Add("IndvCost", SqlDbType.BigInt);
        p_IndvCost.Direction = ParameterDirection.Input;
        p_IndvCost.Value = Convert.ToInt32(dsg.IndvCost);

         SqlParameter p_IndvIncome = cmd[0].Parameters.Add("IndvIncome", SqlDbType.BigInt);
        p_IndvIncome.Direction = ParameterDirection.Input;
        p_IndvIncome.Value = Convert.ToInt32(dsg.IndvIncome);

        SqlParameter p_InsertedBy = cmd[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = dsg.InsertedBy;

        SqlParameter p_InsertedDate = cmd[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = dsg.InsertedDate;

        SqlParameter p_IsUpdate = cmd[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = cmd[0].Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_IsActive = cmd[0].Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;

        cmd[1] = new SqlCommand("DELETE FROM TrTrainingDtls WHERE TrainId = @TrainId");
        cmd[1].CommandType = CommandType.Text;

        SqlParameter p_DesignationID2 = cmd[1].Parameters.Add("TrainId", SqlDbType.BigInt);
        p_DesignationID2.Direction = ParameterDirection.Input;
        p_DesignationID2.Value = Convert.ToInt32(dsg.TrainId);

        int i = 2;
        foreach (GridViewRow gRow in grLocation.Rows)
        {
            //CheckBox chkBox = (CheckBox)gRow.FindControl("chkBox");
            //if (chkBox.Checked == true)
            //{

                string query = " Insert into TrTrainingDtls(TrDetlID,TrainId,DesigId,Period,IsActive,InsertedBy,InsertedDate) " +
                "VALUES( (select isnull(MAX(TrDetlID),0)+1 from TrTrainingDtls),@TrainId,@DesigId,@Period,@IsActive,@InsertedBy,@InsertedDate)";


                cmd[i] = new SqlCommand(query);
                cmd[i].CommandType = CommandType.Text;


                SqlParameter p_LocationId = cmd[i].Parameters.Add("TrainId", SqlDbType.BigInt);
                p_LocationId.Direction = ParameterDirection.Input;
                p_LocationId.Value = Convert.ToInt32(dsg.TrainId);//grLocation.DataKeys[gRow.RowIndex].Values[0].ToString();

                SqlParameter p_DesigId2 = cmd[i].Parameters.Add("DesigId", SqlDbType.BigInt);
                p_DesigId2.Direction = ParameterDirection.Input;
                p_DesigId2.Value = Convert.ToInt32(grLocation.DataKeys[gRow.RowIndex].Values[0].ToString());

                SqlParameter p_Period2 = cmd[i].Parameters.Add("Period", SqlDbType.BigInt);
                p_Period2.Direction = ParameterDirection.Input;
                p_Period2.Value = grLocation.DataKeys[gRow.RowIndex].Values[2].ToString() == "" ? 0 : Convert.ToInt32(grLocation.DataKeys[gRow.RowIndex].Values[2].ToString());

                SqlParameter p_IsActive2 = cmd[i].Parameters.Add("IsActive", SqlDbType.Char);
                p_IsActive2.Direction = ParameterDirection.Input;
                p_IsActive2.Value = IsActive;

                SqlParameter p_InsertedBy2 = cmd[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                p_InsertedBy2.Direction = ParameterDirection.Input;
                p_InsertedBy2.Value = dsg.InsertedBy;

                SqlParameter p_InsertedDate2 = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                p_InsertedDate2.Direction = ParameterDirection.Input;
                p_InsertedDate2.Value = dsg.InsertedDate;
                i++;
           // }
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
    public DataTable SelectTrainingListMst(string traininNane)
    {
        string strSQL = @"select top 1 * from TrTrainingList where TrainName='" + traininNane + "' and IsDeleted='N' ";
        return objDC.CreateDT(strSQL, "TrTrainingMst");
    }

    public DataTable SelectTrainingListDetail(string traininNane)
    {

        SqlCommand command = new SqlCommand("proc_select_TrTrainingDtls");

        SqlParameter p_traininNane = command.Parameters.Add("TrainName", SqlDbType.VarChar);
        p_traininNane.Direction = ParameterDirection.Input;
        p_traininNane.Value = traininNane;

        objDC.CreateDSFromProc(command, "TrTrainingDtls");
        return objDC.ds.Tables["TrTrainingDtls"];
    }

    public DataTable SelectTrainingList(string Trid)
    {
        string strSQL = @"select a.TrainId,a.TrainName,b.TrCategoryName,a.TentativeDay,a.IsActive,a.IsInHouse,a.IsMedicos,CONVERT(numeric(12,0),a.IndvCost) as IndvCost,CONVERT(numeric(12,0),a.IndvIncome) as IndvIncome from TrTrainingList a left join TrCategory b on a.TrCategoryId=b.TrCategoryId where a.IsDeleted='N'";
        return objDC.CreateDT(strSQL, "TrTrainingList");
    }
    public DataTable SelectTrainingEditList(string Trid)
    {
        string strSQL = @"select TrainId,TrCategoryId,TrainName,TentativeDay,IsInHouse,IsMedicos,CONVERT(numeric(12,0),IndvCost) as IndvCost,CONVERT(numeric(12,0),IndvIncome) as IndvIncome,IsActive  from TrTrainingList where TrainId=@TrainId ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        SqlParameter p_TriID = cmd.Parameters.Add("TrainId", SqlDbType.BigInt);
        p_TriID.Direction = ParameterDirection.Input;
        p_TriID.Value = Convert.ToInt32(Trid);
        return objDC.CreateDT(cmd, "TrTrainingList");
    }
    public DataTable SelectTrainingDtlList(string Trid)
    {
        string strSQL = @"select a.DesigId,b.DesigName,a.Period as PeriodMM  from TrTrainingDtls a left join Designation b on a.DesigId=b.DesigId where a.TrainId=@TrainId ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        SqlParameter p_TriID = cmd.Parameters.Add("TrainId", SqlDbType.BigInt);
        p_TriID.Direction = ParameterDirection.Input;
        p_TriID.Value = Convert.ToInt32(Trid);
        return objDC.CreateDT(cmd, "TrTrainingDtlList");
    }
    public DataTable SelectTrainingDtlWithDesig(string Trid)
    {
        string strSQL = @"select tld.DesigId,di.DesigName,tl.TentativeDay from TrTrainingList tl inner join TrTrainingDtls tld on tld.TrainId=tl.TrainId left join Designation di on di.DesigId=tld.DesigId where tl.TrainId=@TrainId ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        SqlParameter p_TriID = cmd.Parameters.Add("TrainId", SqlDbType.BigInt);
        p_TriID.Direction = ParameterDirection.Input;
        p_TriID.Value = Convert.ToInt32(Trid);
        return objDC.CreateDT(cmd, "TrTrainingDtlListWithDesig");
    }
    public DataTable SelectEmployeeDetail(string EmpId)
    {
        string strSQL = @"select ei.EmpId,dg.DesigName,C.ClinicName,dl.DeptName from EmpInfo ei left join Designation dg on ei.DesigId=dg.DesigId 
            left join DepartmentList dl on ei.DeptId=dl.DeptId
            left join ClinicList C on ei.ClinicId=C.ClinicId where ei.EmpId=@EmpId";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        SqlParameter p_TriID = cmd.Parameters.Add("EmpId", SqlDbType.Char);
        p_TriID.Direction = ParameterDirection.Input;
        p_TriID.Value = EmpId;
        return objDC.CreateDT(cmd, "TrTrainingEmployeeDetail");
    }
    public DataTable SelectLocation(string Trid)
    {
        string strSQL = @"select  * from SalaryLocation where  IsDeleted='N' ";
        //string strSQL = @"select * from ClinicList where  IsDeleted='N' ";
        return objDC.CreateDT(strSQL, "TrSalLication");
    }
    public DataTable SelectTrainingVenue(string strVenueid)
    {
        if (strVenueid.Equals("A"))
        {
            string strSQL = @"select VenueId,VenueName, VenueAddress,IsActive from TrVenue where IsDeleted='N'";
            return objDC.CreateDT(strSQL, "TrVenue");   
        }
        else
        {
            string strSQL = @"select VenueId,VenueName, VenueAddress,IsActive from TrVenue where IsDeleted='N' and VenueId=@VenueId";

            SqlCommand cmd = new SqlCommand(strSQL);
            cmd.CommandType = CommandType.Text;
            SqlParameter p_TriID = cmd.Parameters.Add("VenueId", SqlDbType.Int);
            p_TriID.Direction = ParameterDirection.Input;
            p_TriID.Value = int.Parse(strVenueid);

            return objDC.CreateDT(cmd, "TrVenue");
        }
    }
    public DataTable SelectTrainingLocation(string strLocationId)
    {
        if (strLocationId.Equals("A"))
        {
            string strSQL = @"select LocationID,LocationName, LocAddress,IsActive from LocationList where IsDeleted='N'";
            return objDC.CreateDT(strSQL, "LocationList");
        }
        else
        {
            string strSQL = @"select LocationID,LocationName, LocAddress,IsActive from LocationList where IsDeleted='N' and LocationID=@locId";

            SqlCommand cmd = new SqlCommand(strSQL);
            cmd.CommandType = CommandType.Text;
            SqlParameter p_TriID = cmd.Parameters.Add("locId", SqlDbType.Int);
            p_TriID.Direction = ParameterDirection.Input;
            p_TriID.Value = int.Parse(strLocationId);

            return objDC.CreateDT(cmd, "LocationList");
        }
    }
    public DataTable SelectTrainingSetupListDtl(string strTrainListId)
    {
        string strSQL = @"select tl.TraineeId,tl.TraineeId+' ['+ei.FullName+']' as TraineeName,dg.DesigName as Designation,
                            C.ClinicName AS Dept,tl.FundedBy,pl.ProjectName,tl.IsResidential,C.ClinicId from TrTrainingListSetupDtl tl
                            left join EmpInfo ei on tl.TraineeId=ei.EMPID left join Designation dg on ei.DesigId=dg.DesigId left join DepartmentList dl 
                            on ei.DeptId=dl.DeptId left join ProjectList pl on pl.ProjectId=tl.FundedBy
                            left join ClinicList C on ei.ClinicId=C.ClinicId where tl.TrainListId=@TrainListId";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        SqlParameter p_TrainListId = cmd.Parameters.Add("TrainListId", SqlDbType.BigInt);
        p_TrainListId.Direction = ParameterDirection.Input;
        p_TrainListId.Value = Int32.Parse(strTrainListId);
        return objDC.CreateDT(cmd, "TrTrainingListSetupDtl");
    }
    //public DataTable SelectTrainingVenueList(string strVenueId)
    //{
    //    //if (strTrainListId.Equals("A"))
    //    //{
    //        string strSQL = @"select VenueId,VenueName, VenueAddress,IsActive from TrVenue where IsDeleted='N'";
    //        return objDC.CreateDT(strSQL, "TrVenue");
    //    //}
    //}
    public DataTable SelectTrainingPlanList(string strTrPlanId)
    {
        string strSQL = @"select tp.TrainingPlanId,tp.TrainId,tr.TrainName+' ['+CONVERT(varchar(5),tp.TrainId)+']' as TrainName,convert(varchar(10),tp.TentativeDate,103) as TentativeDate,tp.ParticipantLevel,d.DesigName+' ['+CONVERT(varchar(5),tp.ParticipantLevel)+']' as DesigName,
                            tp.VenueId,tv.VenueName+' ['+CONVERT(varchar(5),tp.VenueId)+']' as VenueName,tp.NoofParticipant,tp.ParticipantMatrix,tp.Remarks,tp.CourseCoordinator,ei.fullname+' ['+tp.CourseCoordinator+']' as EmpName,
                                tp.IsActive from TrTrainingPlan tp left join TrTrainingList tr on tr.TrainId=tp.TrainId left join Designation d on d.DesigId=tp.ParticipantLevel
                                    left join TrVenue tv on tv.VenueId=tp.VenueId left join EmpInfo ei on ei.EmpId=tp.CourseCoordinator  where tp.IsDeleted='N'";
       
        return objDC.CreateDT(strSQL, "TrTrainingPlan");
    }
    public DataTable SelectTrainingPlanDtlsList(string strTrainingPlanId)
    {
        string strSQL = @"select tp.RespectiveResource as RespectiveResourceId,ei.FullName+' ['+tp.RespectiveResource+']' as RespectiveResourceName from 
                             TrTrainingPlanDtls tp left join EmpInfo ei on tp.RespectiveResource=ei.EmpId where tp.TrainingPlanId=@TrainingPlanId;";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        SqlParameter p_TrainingPlanId = cmd.Parameters.Add("TrainingPlanId", SqlDbType.Int);
        p_TrainingPlanId.Direction = ParameterDirection.Input;
        p_TrainingPlanId.Value = Int32.Parse(strTrainingPlanId);

        return objDC.CreateDT(cmd, "TrTrainingPlanDtls");
    }
    public DataTable SelectTrainingPlanLevelDtlsList(string strTrainingPlanId)
    {
        string strSQL = @"select tp.FundedBy,P.ProjectName,tp.NoOfParticipants from TrTrainingPlanLevelDtls tp 
left join ProjectList P ON tp.FundedBy=P.ProjectId where tp.TrainingPlanId=@TrainingPlanId";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        SqlParameter p_TrainingPlanId = cmd.Parameters.Add("TrainingPlanId", SqlDbType.Int);
        p_TrainingPlanId.Direction = ParameterDirection.Input;
        p_TrainingPlanId.Value = Int32.Parse(strTrainingPlanId);

        return objDC.CreateDT(cmd, "TrTrainingPlanLevelDtls");
    }
    public DataTable SelectTrainingSetupList(string strTrainListId)
    {
        if (strTrainListId.Equals("A"))
        {
            string strSQL = @"select ts.TrainListId,tss.TrainId,tl.TrainName+' ['+CONVERT(varchar(5),tss.TrainId)+']' as TrainName,ts.ScheduleID,
                                convert(varchar(5),ts.ScheduleID)+' ['+convert(varchar(10),tss.StrDate,103)+'-'+convert(varchar(10),tss.EndDate,103)+']' as ScheduleName,
                                ts.VenueId,ve.VenueName,ve.VenueAddress,convert(varchar(10),ts.onDate,103) as onDate,ts.onTime,
                                ts.SignBy1,(select fullname from EmpInfo where EmpId=ts.SignBy1)+' ['+ts.SignBy1+']' as SignBy1Name,
                                ts.SignBy2,(select fullname from EmpInfo where EmpId=ts.SignBy2)+' ['+ts.SignBy2+']' as SignBy2Name,
                                ts.SignBy3,(select fullname from EmpInfo where EmpId=ts.SignBy3)+' ['+ts.SignBy3+']' as SignBy3Name,
                                ts.CC,ts.AdminGuideline,ts.OrganizedBy,ts.Remarks,(select fullname from EmpInfo where EmpId=ts.OrganizedBy)+' ['+ts.OrganizedBy+']' as OrganizedByName,
                                ts.IsActive from TrTrainingListSetup ts left join TrVenue ve on ts.VenueId=ve.VenueId left join ProjectList pl on pl.ProjectId=ts.OrganizedBy 
                                inner join TrSchedule tss inner join  TrTrainingList tl on tss.TrainId=tl.TrainId on ts.ScheduleID=tss.ScheduleID where ts.IsDeleted='N'";
                                  
            return objDC.CreateDT(strSQL, "TrTrainingListSetup");
        }
        else
        {
            string strSQL = @"select ts.TrainListId,tss.TrainId,tl.TrainName+' ['+CONVERT(varchar(5),tss.TrainId)+']' as TrainName,ts.ScheduleID,
                                convert(varchar(5),ts.ScheduleID)+' ['+convert(varchar(10),tss.StrDate,103)+'-'+convert(varchar(10),tss.EndDate,103)+']' as ScheduleName,
                                ts.VenueId,ve.VenueName,ve.VenueAddress,convert(varchar(10),ts.onDate,103) as onDate,ts.onTime,
                                ts.SignBy1,(select fullname from EmpInfo where EmpId=ts.SignBy1)+' ['+ts.SignBy1+']' as SignBy1Name,
                                ts.SignBy2,(select fullname from EmpInfo where EmpId=ts.SignBy2)+' ['+ts.SignBy2+']' as SignBy2Name,
                                ts.SignBy3,(select fullname from EmpInfo where EmpId=ts.SignBy3)+' ['+ts.SignBy3+']' as SignBy3Name,
                                ts.CC,ts.AdminGuideline,ts.OrganizedBy,(select fullname from EmpInfo where EmpId=ts.OrganizedBy)+' ['+ts.OrganizedBy+']' as OrganizedByName,
                                ts.IsActive from TrTrainingListSetup ts left join TrVenue ve on ts.VenueId=ve.VenueId left join ProjectList pl on pl.ProjectId=ts.OrganizedBy 
                                inner join TrSchedule tss inner join  TrTrainingList tl on tss.TrainId=tl.TrainId on ts.ScheduleID=tss.ScheduleID where ts.IsDeleted='N' and TrainListId=@TrainListId";
            
            SqlCommand cmd = new SqlCommand(strSQL);
            cmd.CommandType = CommandType.Text;
            SqlParameter p_TrainListId = cmd.Parameters.Add("TrainListId", SqlDbType.BigInt);
            p_TrainListId.Direction = ParameterDirection.Input;
            p_TrainListId.Value = Int32.Parse(strTrainListId);
            return objDC.CreateDT(cmd, "TrTrainingListSetup");
        }
    }
    public DataTable SelectTrainingResultList(string strTrainResultId) 
    {
        if (strTrainResultId.Equals("A"))
        {
            string strSQL = @"select tr.ResultId,tr.TrainId,tt.TrainName+' ['+convert(varchar(5),tr.TrainId)+']' as TrainName,convert(varchar(10),tr.EvalDate,103) as EvalDate,tr.EvalMethod,tr.EvalBy,tr.SignID1,tr.SignID1+' ['+(select fullname from EmpInfo where EmpId=tr.SignID1)+']' as SignIDName1,
                                tr.SignID2,tr.SignID2+' ['+(select fullname from EmpInfo where EmpId=tr.SignID2)+']' as SignIDName2,tr.SignID3,tr.SignID3+' ['+(select fullname from EmpInfo where EmpId=tr.SignID3)+']' as SignIDName3,tr.SignID4,tr.SignID4+' ['+(select fullname from EmpInfo where EmpId=tr.SignID4)+']' as SignIDName4,tr.IsActive 
                                    ,tr.ScheduleID,sl.SalLocName+' ['+convert(varchar(12),sl.SalLocId)+']' as TrainLocation,ven.VenueName+' ['+convert(varchar(12),ven.VenueId) as VenueName
FROM TrTrainResult tr left join TrTrainingList tt on  tr.TrainId=tt.TrainId 
                                        left join TrSchedule ts on ts.ScheduleID=tr.ScheduleID left join SalaryLocation sl on ts.SalLocId=sl.SalLocId
left join TrVenue ven on ts.VenueId=ven.VenueId where tr.IsDeleted='N'";

            return objDC.CreateDT(strSQL, "TrTrainingResultList");
        }
        else 
        {
            string strSQL = @"select tr.ResultId,tr.TrainId,tt.TrainName+' ['+convert(varchar(5),tr.TrainId)+']' as TrainName,tr.EvalDate,tr.EvalMethod,tr.EvalBy,tr.SignID1,tr.SignID1+' ['+(select fullname from EmpInfo where EmpId=tr.SignID1)+']' as SignIDName1,
                                 tr.SignID2,tr.SignID2+' ['+(select fullname from EmpInfo where EmpId=tr.SignID2)+']' as SignIDName2,tr.SignID3,tr.SignID3+' ['+(select fullname from EmpInfo where EmpId=tr.SignID3)+']' as SignIDName3,tr.SignID4,tr.SignID4+' ['+(select fullname from EmpInfo where EmpId=tr.SignID4)+']' as SignIDName4,tr.IsActive FROM TrTrainResult tr left join TrTrainingList tt on  tr.TrainId=tt.TrainId where tr.IsDeleted='N' and tr.TrainId=@TrainId";
            SqlCommand cmd = new SqlCommand(strSQL);
            cmd.CommandType = CommandType.Text;
            SqlParameter p_TrainId = cmd.Parameters.Add("TrainId", SqlDbType.BigInt);
            p_TrainId.Direction = ParameterDirection.Input;
            p_TrainId.Value = Int32.Parse(strTrainResultId);
            return objDC.CreateDT(cmd, "TrTrainingResultList");
        }

    }
    public DataTable SelectTrainingResultDtlList(string strTrainResultId)
    {
        string strSQL = @"select TraineeId,tr.TraineeId+' ['+ei.FullName+']' as TraineeName,tr.FundedBy,tr.PreTest,tr.PostTest,tr.PracticalTest,tr.Viva,tr.Overall,tr.CompetencyLevel,tr.Remark,PL.ProjectName
                               from TrTrainResultDtls tr left join EmpInfo ei on tr.TraineeId=ei.EmpId
                                left join ProjectList PL on tr.FundedBy=PL.ProjectId where tr.ResultId=@ResultId";
       
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        SqlParameter p_ResultId = cmd.Parameters.Add("ResultId", SqlDbType.BigInt);
        p_ResultId.Direction = ParameterDirection.Input;
        p_ResultId.Value = Int32.Parse(strTrainResultId);
        return objDC.CreateDT(cmd, "TrTrainingResultDtlList");
    }
    public DataTable SelectScheduleList(string TrainId) 
    {
        if (TrainId.Equals("A"))
        {
            string strSQL = @" select ts.ScheduleID,ts.TrainId,tt.TrainName+' ['+CONVERT(varchar(5),ts.TrainId)+']' as TrainName,ts.SalLocId,sl.SalLocName,
                              convert(varchar(10),ts.StrDate,103) as StrDate,convert(varchar(10),ts.EndDate,103) as EndDate,ts.Duration,ts.NoofPerson,ts.CoordinatorName as CoordinatorId,ei.FullName+' ['+ts.CoordinatorName+']' as CoordinatorName,  
                              ts.Residential as ResidentialId,ts.VenueId,ts.Remarks,
                              case when ts.Residential='R' then 'Residential' when ts.Residential='N' then 'Non-Residential' when ts.Residential='RN' then 'Residential/Non-Residential' when ts.Residential='O' then 'Outside' end as Residential,
                              ts.IsActive, ts.IsDeleted,convert(varchar(5),ts.ScheduleID)+' ['+convert(varchar(10),ts.StrDate,103)+'  to  '+convert(varchar(10),ts.EndDate,103)+']' as ScheDate from TrSchedule ts 
                              left join TrTrainingList tt on ts.TrainId=tt.TrainId left join SalaryLocation sl on  ts.SalLocId=sl.SalLocId left join EmpInfo ei on ei.EmpId=ts.CoordinatorName
                              where ts.IsDeleted='N'";
            return objDC.CreateDT(strSQL, "TrSchedule");
        }
        else
        {
            string strSQL = @"select ts.ScheduleID,ts.TrainId,tt.TrainName+' ['+CONVERT(varchar(5),ts.TrainId)+']' as TrainName,ts.SalLocId,sl.SalLocName,
                                ts.StrDate,ts.EndDate,ts.Duration,ts.NoofPerson,ts.CoordinatorName as CoordinatorId,ei.FullName+' ['+ts.CoordinatorName+']' as CoordinatorName,  
                                   ts.Residential as ResidentialId,
                                    case when ts.Residential='R' then 'Residential' when ts.Residential='N' then 'Non-Residential' when ts.Residential='RN' then 'Residential/Non-Residential' when ts.Residential='O' then 'Outside' end as Residential,
                                    ts.IsActive, ts.IsDeleted,convert(varchar(5),ts.ScheduleID)+' ['+convert(varchar(10),ts.StrDate,103)+'  to  '+convert(varchar(10),ts.EndDate,103)+']' as ScheDate from TrSchedule ts 
                                    left join TrTrainingList tt on ts.TrainId=tt.TrainId left join SalaryLocation sl on  ts.SalLocId=sl.SalLocId left join EmpInfo ei on ei.EmpId=ts.CoordinatorName
                                        where ts.TrainId=@TrainId and ts.IsDeleted='N'";

            SqlCommand cmd = new SqlCommand(strSQL);
            cmd.CommandType = CommandType.Text;
            SqlParameter p_TrainId = cmd.Parameters.Add("TrainId", SqlDbType.BigInt);
            p_TrainId.Direction = ParameterDirection.Input;
            p_TrainId.Value = Int32.Parse(TrainId);
            return objDC.CreateDT(cmd, "TrSchedule");
        }
    }

    public DataTable SelectScheduleDtls(string ScheduleID)
    {
        string strSQL = @"select tl.FundedBy,pl.ProjectName,tl.ParticipantNo from TrScheduleDtls tl
                            left join ProjectList pl on pl.ProjectId=tl.FundedBy
                            WHERE tl.ScheduleID=@ScheduleID";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        SqlParameter p_TrainListId = cmd.Parameters.Add("ScheduleID", SqlDbType.BigInt);
        p_TrainListId.Direction = ParameterDirection.Input;
        p_TrainListId.Value = Int32.Parse(ScheduleID);
        return objDC.CreateDT(cmd, "TrScheduleDtls");
    }
    public DataTable SelectOtherTrainingList(string strOtherTrainId)
    {
        if (strOtherTrainId.Equals("A")) //fullname from EmpInfo where EmpId
        {
            string strSQL = @"select ot.OtherTrainId,ot.TrainId,tl.TrainName+' ['+convert(varchar(5),ot.TrainId)+']' 
            as TrainName,convert(varchar(10),ot.StartDate,103) as StartDate,convert(varchar(10),ot.EndDate,103) as EndDate,
            ot.Duration,ot.OrganizedBy,ei.fullname+' ['+ot.OrganizedBy+']' as OrganizedByName,ot.Location,ot.Venue,
            ot.Remarks,ot.IsCertificate,ot.IsActive from TrOtherTrain ot left join TrTrainingList tl on 
            tl.TrainId=ot.TrainId left join EmpInfo ei on ei.EmpId=ot.OrganizedBy where ot.IsDeleted='N'";
            return objDC.CreateDT(strSQL, "TrOtherTrain");
        }
        else
        {
            string strSQL = @"select ot.OtherTrainId,ot.TrainId,tl.TrainName+' ['+convert(varchar(5),ot.TrainId)+']' 
            as TrainName,convert(varchar(10),ot.StartDate,103) as StartDate,convert(varchar(10),ot.EndDate,103) as EndDate,
            ot.Duration,ot.OrganizedBy,ei.fullname+' ['+ot.OrganizedBy+']' as OrganizedByName,ot.Location,ot.Venue,
            ot.Remarks,ot.IsCertificate,ot.IsActive from TrOtherTrain ot left join TrTrainingList tl on 
            tl.TrainId=ot.TrainId left join EmpInfo ei on ei.EmpId=ot.OrganizedBy where ot.OtherTrainId=@OtherTrainId 
            and ot.IsDeleted='N' ";
            SqlCommand cmd = new SqlCommand(strSQL);
            cmd.CommandType = CommandType.Text;
            SqlParameter p_OtherTrainId = cmd.Parameters.Add("OtherTrainId", SqlDbType.BigInt);
            p_OtherTrainId.Direction = ParameterDirection.Input;
            p_OtherTrainId.Value = Int32.Parse(strOtherTrainId);
            return objDC.CreateDT(cmd, "TrOtherTrain");
        }
    }
    public DataTable SelectOtherTrainingDtlList(string strOtherTrainId)
    {
        string strSQL = @"select ot.ParticipantName,ot.Designation from TrOtherTrainDtls ot where ot.OtherTrainId=@OtherTrainId";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        SqlParameter p_OtherTrainId = cmd.Parameters.Add("OtherTrainId", SqlDbType.BigInt);
        p_OtherTrainId.Direction = ParameterDirection.Input;
        p_OtherTrainId.Value = Int32.Parse(strOtherTrainId);
        return objDC.CreateDT(cmd, "TrOtherTrainDtls");
    }

    public DataTable SelectTrainingScheduleInfo(string strScheduleId)
    {
        string strSQL = @"select tr.TrainId,convert(varchar(10),tr.StrDate,103) AS StrDate,convert(varchar(10),tr.EndDate,103) AS EndDate,tr.Duration,tr.NoofPerson,sl.SalLocId,sl.SalLocName+' ['+convert(varchar(5),sl.SalLocId)+']' as SalLocName,
                            tr.CoordinatorName,ei.FullName+' ['+tr.CoordinatorName+']' as CoordinatorFName,tr.FundedBy,
                            tr.Residential as ResidentialId,  case when tr.Residential='R' then 'Residential' when tr.Residential='N' then 'Non-Residential'
 when tr.Residential='RN' then 'Residential/Non-Residential' when tr.Residential='O' then 'Outside' end as Residential,
tr.IsActive
,(select pl.ProjectName+',' as 'data()' from ProjectList pl
inner join trScheduleDtls scd on scd.FundedBy=pl.ProjectId
where scd.ScheduleID= tr.ScheduleID
FOR XML PATH('')) ProjectName
from  TrSchedule tr left join SalaryLocation sl on tr.SalLocId=sl.SalLocId left join EmpInfo ei on ei.EmpId=  tr.CoordinatorName where tr.ScheduleID=@ScheduleID and tr.IsDeleted='N'";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        SqlParameter p_TriID = cmd.Parameters.Add("ScheduleID", SqlDbType.BigInt);
        p_TriID.Direction = ParameterDirection.Input;
        p_TriID.Value = Convert.ToInt32(strScheduleId);
        return objDC.CreateDT(cmd, "TrTrainingScheduleInfo");
    }
    public DataTable SelectTrainingNameLocUsingSchedule(string strScheduleId) 
    {
        string strSQL = @"select tt.TrainId,tt.TrainName+' ['+convert(varchar(12),tt.TrainId)+']' as TrainName,sl.SalLocName+' ['+convert(varchar(12),sl.SalLocId)+']' as TrainLocation,
tv.VenueName+' ['+convert(varchar(12),ts.VenueId)+']' as VenueName
                        from TrSchedule ts left join TrTrainingList tt on ts.TrainId=tt.TrainId left join SalaryLocation sl on ts.SalLocId=sl.SalLocId
                        left join TrVenue tv on ts.VenueId=tv.VenueId
                        where ts.ScheduleID=@ScheduleID and ts.IsDeleted='N'";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        SqlParameter p_TriID = cmd.Parameters.Add("ScheduleID", SqlDbType.BigInt);
        p_TriID.Direction = ParameterDirection.Input;
        p_TriID.Value = Convert.ToInt32(strScheduleId);
        return objDC.CreateDT(cmd, "TrTrainingNameLoc");
    }
    public DataTable SelectTrainingRequisitionList() 
    {
        string strSQL = @"select tr.ReqID,tr.ScheduleID,tt.TrainName as ScheduleName,tr.TrainId,tt.TrainName+' ['+convert(varchar(12),tr.TrainId)+']' as TrainName,
                            sl.SalLocName+' ['+convert(varchar(12),sl.SalLocId)+']' as TrainLocation,tr.SignDesig1,tr.SignDesig2,tr.SeenBy,tr.ReviewBy,tr.RecommendBy,
                            tr.ApprovBy,tr.IsActive,tr.Remarks from TrRequisition tr left join TrTrainingList tt  on tr.TrainId=tt.TrainId left join TrSchedule ts on tr.ScheduleID=ts.ScheduleID
                            left join SalaryLocation sl on ts.SalLocId=sl.SalLocId where tr.IsDeleted='N' and ts.IsDeleted='N'";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        return objDC.CreateDT(cmd, "TrTrainingReqList");

    }
    public DataTable SelectTrainingReqDtlsList(string strReqId)
    {
        string strSQL = @"SELECT tr.TraineeId as EmpID,ei.FULLNAME + ' [' + ei.EMPID + ']' AS EmpName,tr.ProjectId,pl.ProjectName FROM TrRequisitionDtls tr  
                            left join EMPINFO ei on tr.TraineeId =ei.EMPID left join ProjectList pl on pl.ProjectId=tr.ProjectId
                                WHERE tr.IsDeleted='N' and tr.ReqId=@ReqId and ei.ISDELETED='N' ORDER BY EMPID ";
       
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        SqlParameter p_ReqId = cmd.Parameters.Add("ReqId", SqlDbType.BigInt);
        p_ReqId.Direction = ParameterDirection.Input;
        p_ReqId.Value = Convert.ToInt32(strReqId);
        return objDC.CreateDT(cmd, "TrRequisitionDtls");
    }
//    public DataTable SelectTrainingBudgetList(string strBudgetId)
//    {
//        string strSQL = @"select tb.BudgetId,tb.TrainId,tl.TrainName+' ['+CONVERT(varchar(5),tb.TrainId)+']' as TrainName,tb.ScheduleID,CONVERT(varchar(10),ts.StrDate,103) as StrDate,CONVERT(varchar(10),ts.EndDate,103) as EndDate,ts.Duration,ts.NoofPerson,
//                            ei.FullName+' ['+ts.CoordinatorName+']' as CoordinatorName,pl.ProjectName+' ['+CONVERT(varchar(5),ts.FundedBy)+']' as FundedBy,case when ts.Residential='R' then 'Residential' when ts.Residential='N' then 'Non-Residential' 
//                            when ts.Residential='O' then 'Outside' end as Residential,tb.FeePerPerson,tb.IncomePerPerson,tb.ResidencialCost,tb.OtherIncome,tb.IsActive from TrTrainingBudget tb left join TrSchedule ts on ts.ScheduleID=tb.ScheduleID 
//                            left join TrTrainingList tl on tl.TrainId=tb.TrainId left join ProjectList pl on pl.ProjectId=ts.FundedBy left join EmpInfo ei on ei.EmpId=  ts.CoordinatorName where tb.IsDeleted='N';";

     
//        return objDC.CreateDT(strSQL, "TrTrainingBudget");
//    }

    public DataTable SelectTrainingBudgetList(string strBudgetId)
    {
        string strSQL = @"select tb.BudgetId,tb.TrainId,tl.TrainName+' ['+CONVERT(varchar(5),tb.TrainId)+']' as TrainName,tb.ScheduleID,CONVERT(varchar(10),ts.StrDate,103) as StrDate,CONVERT(varchar(10),ts.EndDate,103) as EndDate,ts.Duration,ts.NoofPerson,
                            ei.FullName+' ['+tb.PreparedBy+']' as PreparedByName,tb.PreparedBy,tb.ReviewedBy,tb.RecomBy1,tb.RecomBy2,tb.RecomBy3,
                            tb.ApprovedBy,
(select pl.ProjectName+',' as 'data()' from ProjectList pl
inner join trScheduleDtls scd on scd.FundedBy=pl.ProjectId
where scd.ScheduleID= tb.ScheduleID
FOR XML PATH('')) as FundedBy,
case when ts.Residential='R' then 'Residential' when ts.Residential='N' then 'Non-Residential' 
 when ts.Residential='RN' then 'Residential/Non-Residential'
                            when ts.Residential='O' then 'Outside' end as Residential,tb.CreateDate,tb.ParticipantLevel,tb.VenueId,tb.IsActive 
                            from TrTrainingBudget tb 
                            left join TrSchedule ts on ts.ScheduleID=tb.ScheduleID
                            left join TrTrainingList tl on tl.TrainId=tb.TrainId
                            left join EmpInfo ei on ei.EmpId=  tb.PreparedBy where tb.IsDeleted='N';";
        return objDC.CreateDT(strSQL, "TrTrainingBudget");
    }
    public DataTable SelectTrainingBudgetDtlList(string strTrainBudgetId)
    {

        string strSQL = @"select tr.BudgetId,bt.BTitleType,type.BTitleTypeName as BudgetType,tr.BTitleId,
bt.BTitle as BTitleName,tr.UnitCost,tr.ParticipantNo,tr.DaysNo,ISNULL(tr.UnitCost, 0)*ISNULL(tr.ParticipantNo, 0)*ISNULL(tr.DaysNo, 0) as TotalTaka
                               from TrTrainingBudgetDtls tr left join TrBudgetTitle bt on tr.BTitleId=bt.BTitleId
inner join TrBudgetTitleType type on type.BTitleTypeId=bt.BTitleType
where tr.BudgetId=@BudgetId";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        SqlParameter p_ResultId = cmd.Parameters.Add("BudgetId", SqlDbType.BigInt);
        p_ResultId.Direction = ParameterDirection.Input;
        p_ResultId.Value = Int32.Parse(strTrainBudgetId);
        return objDC.CreateDT(cmd, "TrTrainingResultDtlList");
    }
    public DataTable SelectTrainingYrPlanList(string strMatrixId)
    {
        string strSQL = @"SELECT TM.YrPlanId,TM.YrPlanName,TM.TrainType,TM.Year,CONVERT(varchar(10),TM.StrDate,103) as StrDate,CONVERT(varchar(10),TM.EndDate,103) as EndDate,
TM.Duration,TM.Remarks,TM.IsActive,TM.TrainId,TL.TrainName+' ['+CONVERT(VARCHAR(5),LTRIM(RTRIM(TM.TrainId)))+']' AS TrainName,
TM.Remarks,TM.CourseFee,TM.Perdiem,Tm.FAOthers,TM.Transport,TM.TrainType,TM.LocalTransport,TM.TotalParticipant,TM.PracticalCost,TM.Miscellaneous,
TM.SubtitledBy,TM.ReviewedBy,TM.RecommBy1,TM.Recommby2,TM.ApprovedBy
from TrTrainingYrPlan TM LEFT JOIN TrTrainingList TL ON TM.TrainId=TL.TrainId
        WHERE TM.IsDeleted='N'";
        return objDC.CreateDT(strSQL, "TrTrainingYrPlan");
    }

    public DataTable SelectTrainingYrPlanDtlList(string strYrPlanId)
    {
        string strSQL = @"select TMD.DesigId,DE.DesigName,TMD.FundedBy,pl.ProjectName as FundedByName,TMD.NoOfPerson,TMD.VenueId,ven.VenueName,TMD.LocationId,lc.SalLocName as LocationName
from dbo.TrTrainingYrPlanDtls TMD LEFT JOIN Designation DE ON DE.DesigId=TMD.DesigId 
left join ProjectList pl on TMD.FundedBy=pl.ProjectId
left join TrVenue ven on TMD.VenueId=ven.VenueId
left join SalaryLocation lc on TMD.LocationId=lc.SalLocId
                                WHERE TMD.YrPlanId=@PlanId ";

        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_PlanId = command.Parameters.Add("PlanId", SqlDbType.Int);
        p_PlanId.Direction = ParameterDirection.Input;
        p_PlanId.Value = Convert.ToInt32(strYrPlanId);

        return objDC.CreateDT(command, "TrTrainingYrPlanDtls");
    }

    public DataTable SelectPassedEmployee(int trainId, string status)
    {
        string strSQL = "SELECT EMPID,FULLNAME + ' [' + EMPID + ']' AS EMPNAME FROM TrTrainResult tr" +
        " inner join TrTrainResultDtls trd on tr.ResultId=trd.ResultId"+
        " inner join EMPINFO emp on trd.TraineeId=emp.empId WHERE tr.TrainId=@TrainId and emp.EmpStatus=@STATUS AND emp.ISDELETED='N' and trd.CompetencyLevel=1 ORDER BY EMPID";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_TrainId = command.Parameters.Add("TrainId", SqlDbType.Int);
        p_TrainId.Direction = ParameterDirection.Input;
        p_TrainId.Value = trainId;

        SqlParameter p_STATUS = command.Parameters.Add("STATUS", SqlDbType.Char);
        p_STATUS.Direction = ParameterDirection.Input;
        p_STATUS.Value = status;

        objDC.CreateDT(command, "SelectPassedEmployee");
        return objDC.ds.Tables["SelectPassedEmployee"];
    }
    public void TrainingSchedule(clsTrSchedule objTS, string IsActive,string userID,string InsDate,string IsUpdate, string IsDelete)
    { 

        SqlCommand command = new SqlCommand("proc_InUp_TrainingSchedule");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_ScheduleID = command.Parameters.Add("ScheduleID", SqlDbType.BigInt);
        p_ScheduleID.Direction = ParameterDirection.Input;
        p_ScheduleID.Value =Convert.ToInt64( objTS.ScheduleID);

        SqlParameter p_DesignationID = command.Parameters.Add("TrainId", SqlDbType.BigInt);
        p_DesignationID.Direction = ParameterDirection.Input;
        p_DesignationID.Value = Convert.ToInt64(objTS.TrainId);

        SqlParameter p_SalLocId = command.Parameters.Add("SalLocId", SqlDbType.BigInt);
        p_SalLocId.Direction = ParameterDirection.Input;
        p_SalLocId.Value = Convert.ToInt64(objTS.SalLocId);

        SqlParameter p_VenueId = command.Parameters.Add("VenueId", SqlDbType.BigInt);
        p_VenueId.Direction = ParameterDirection.Input;
        p_VenueId.Value = Convert.ToInt64(objTS.VenueId);

        SqlParameter p_StrDate = command.Parameters.Add("StrDate", DBNull.Value);
        p_StrDate.Direction = ParameterDirection.Input;
        p_StrDate.IsNullable = true;
        if (objTS.StrDate != "")
            p_StrDate.Value = Common.ReturnDate(objTS.StrDate);
        
        SqlParameter p_EndDate = command.Parameters.Add("EndDate", DBNull.Value);
        p_EndDate.Direction = ParameterDirection.Input;
        p_EndDate.IsNullable = true;
        if (objTS.EndDate != "")
            p_EndDate.Value = Common.ReturnDate(objTS.EndDate);

        SqlParameter p_Duration = command.Parameters.Add("Duration", SqlDbType.BigInt);
        p_Duration.Direction = ParameterDirection.Input;
        p_Duration.Value =Convert.ToInt32(objTS.Duration);

        SqlParameter p_NoofPerson = command.Parameters.Add("NoofPerson", SqlDbType.BigInt);
        p_NoofPerson.Direction = ParameterDirection.Input;
        p_NoofPerson.Value = Convert.ToInt32(objTS.NoofPerson);

        SqlParameter p_CoordinatorName = command.Parameters.Add("CoordinatorName", SqlDbType.VarChar);
        p_CoordinatorName.Direction = ParameterDirection.Input;
        p_CoordinatorName.Value = objTS.CoordinatorName;


        SqlParameter p_FundedBy = command.Parameters.Add("FundedBy", SqlDbType.VarChar);
        p_FundedBy.Direction = ParameterDirection.Input;
        p_FundedBy.Value = objTS.FundedBy;

        SqlParameter p_Residential = command.Parameters.Add("Residential", SqlDbType.Char);
        p_Residential.Direction = ParameterDirection.Input;
        p_Residential.Value = objTS.Residential;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = userID;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = Common.ReturnDate((System.DateTime.Now.ToString("dd/MM/yyyy")).ToString());

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        try
        {
            //objDC.MakeTransaction(command);
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


    public void InsertTrRequisition(GridView grLocation, clsTrRequisition dsg, string InsertedBy, string InsertedDate, string IsUpdate, string IsDelete)
    {
        SqlCommand[] cmd = new SqlCommand[grLocation.Rows.Count + 2];
        //SqlCommand[] cmd = new SqlCommand[1];
        cmd[0] = new SqlCommand("proc_InUp_TrRequisition");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_DesignationID = cmd[0].Parameters.Add("ReqID", SqlDbType.BigInt);
        p_DesignationID.Direction = ParameterDirection.Input;
        p_DesignationID.Value = Convert.ToInt32(dsg.ReqID);

        SqlParameter p_TrCategoryId = cmd[0].Parameters.Add("ScheduleID", SqlDbType.BigInt);
        p_TrCategoryId.Direction = ParameterDirection.Input;
        p_TrCategoryId.Value = Convert.ToInt32(dsg.ScheduleID);

        SqlParameter p_TrainName = cmd[0].Parameters.Add("TrainId", SqlDbType.BigInt);
        p_TrainName.Direction = ParameterDirection.Input;
        p_TrainName.Value = Convert.ToInt32(dsg.TrainId);

        SqlParameter p_TentativeDay = cmd[0].Parameters.Add("SignDesig1", SqlDbType.Char);
        p_TentativeDay.Direction = ParameterDirection.Input;
        p_TentativeDay.Value =dsg.SignDesig1;

        SqlParameter p_IsInHouse = cmd[0].Parameters.Add("SignDesig2", SqlDbType.Char);
        p_IsInHouse.Direction = ParameterDirection.Input;
        p_IsInHouse.Value = dsg.SignDesig2;

        SqlParameter p_IsMedicos = cmd[0].Parameters.Add("SeenBy", SqlDbType.Char);
        p_IsMedicos.Direction = ParameterDirection.Input;
        p_IsMedicos.Value = dsg.SeenBy;

        SqlParameter p_IndvCost = cmd[0].Parameters.Add("ReviewBy", SqlDbType.Char);
        p_IndvCost.Direction = ParameterDirection.Input;
        p_IndvCost.Value = dsg.ReviewBy;

        SqlParameter p_IndvIncome = cmd[0].Parameters.Add("RecommendBy", SqlDbType.Char);
        p_IndvIncome.Direction = ParameterDirection.Input;
        p_IndvIncome.Value = dsg.RecommendBy;

        SqlParameter p_ApprovBy = cmd[0].Parameters.Add("ApprovBy", SqlDbType.Char);
        p_ApprovBy.Direction = ParameterDirection.Input;
        p_ApprovBy.Value = dsg.ApprovBy;


        SqlParameter p_InsertedBy = cmd[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = InsertedBy;

        SqlParameter p_InsertedDate = cmd[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = Common.ReturnDate((System.DateTime.Now.ToString("dd/MM/yyyy")).ToString());

        SqlParameter p_IsUpdate = cmd[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = cmd[0].Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_IsActive = cmd[0].Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = dsg.IsActive;

        SqlParameter p_Remarks = cmd[0].Parameters.Add("Remarks", SqlDbType.VarChar);
        p_Remarks.Direction = ParameterDirection.Input;
        p_Remarks.Value = dsg.Remarks;


        cmd[1] = new SqlCommand("DELETE FROM TrRequisitionDtls WHERE ReqID=@ReqID ");
        cmd[1].CommandType = CommandType.Text;

        SqlParameter p_DesignationID2 = cmd[1].Parameters.Add("ReqID", SqlDbType.BigInt);
        p_DesignationID2.Direction = ParameterDirection.Input;
        p_DesignationID2.Value = Convert.ToInt32(dsg.ReqID);

        int i = 2;
        foreach (GridViewRow gRow in grLocation.Rows)
        {
            //CheckBox chkBox = (CheckBox)gRow.FindControl("chkBox");
            //if (chkBox.Checked == true)
            //{

            string query = " Insert into TrRequisitionDtls(ReqDetID,ReqID,ScheduleID,TraineeId,ProjectId,IsActive,IsDeleted,InsertedBy,InsertedDate) " +
            "VALUES((select isnull(MAX(ReqDetID),0)+1 from TrRequisitionDtls), @ReqID,@ScheduleID, @TraineeId,@ProjectId,@IsActive,'N', @InsertedBy, @InsertedDate)";


            cmd[i] = new SqlCommand(query);
            cmd[i].CommandType = CommandType.Text;


            SqlParameter p_ReqID = cmd[i].Parameters.Add("ReqID", SqlDbType.BigInt);
            p_ReqID.Direction = ParameterDirection.Input;
            p_ReqID.Value = Convert.ToInt32(dsg.ReqID); //grLocation.DataKeys[gRow.RowIndex].Values[0].ToString();

            SqlParameter p_ScheduleID = cmd[i].Parameters.Add("ScheduleID", SqlDbType.BigInt);
            p_ScheduleID.Direction = ParameterDirection.Input;
           // p_ScheduleID.Value = Convert.ToInt32(grLocation.DataKeys[gRow.RowIndex].Values[0].ToString());
            p_ScheduleID.Value = Convert.ToInt64(dsg.ScheduleID);

            SqlParameter p_TraineeId = cmd[i].Parameters.Add("TraineeId", SqlDbType.Char);
            p_TraineeId.Direction = ParameterDirection.Input;
            p_TraineeId.Value = grLocation.DataKeys[gRow.RowIndex].Values[0].ToString();
            //p_TraineeId.Value = Convert.ToInt64(dsg.TrainId);
            SqlParameter p_ProjectId = cmd[i].Parameters.Add("ProjectId", SqlDbType.Char);
            p_ProjectId.Direction = ParameterDirection.Input;
            p_ProjectId.Value = grLocation.DataKeys[gRow.RowIndex].Values[2].ToString();

            SqlParameter p_IsActive2 = cmd[i].Parameters.Add("IsActive", SqlDbType.Char);
            p_IsActive2.Direction = ParameterDirection.Input;
            p_IsActive2.Value = dsg.IsActive;

            SqlParameter p_InsertedBy2 = cmd[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy2.Direction = ParameterDirection.Input;
            p_InsertedBy2.Value = InsertedBy;

            SqlParameter p_InsertedDate2 = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate2.Direction = ParameterDirection.Input;
            p_InsertedDate2.Value = Common.ReturnDate((System.DateTime.Now.ToString("dd/MM/yyyy")).ToString()); //Common.ReturnDate(InsertedDate);
            i++;
            // }
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

    public DataTable SelectBudgetTitleList(int bTitleId,int bTitleTypeId)
    {
        if (bTitleId == 0 && bTitleTypeId == 0)
        {
            string strSQL = @"select tit.BTitleId,tit.BTitleType,tit.BTitle,tit.IsActive,BTitleTypeName from TrBudgetTitle tit
                               inner join TrBudgetTitleType type on type.BTitleTypeId=tit.BTitleType where tit.IsDeleted='N'";
            return objDC.CreateDT(strSQL, "TrBudgetTitle");
        }
        else if (bTitleTypeId > 0)
        {
            string strSQL = @"select tit.BTitleId,tit.BTitle,tit.BTitleType,tit.IsActive,BTitleTypeName from TrBudgetTitle tit
                               inner join TrBudgetTitleType type on type.BTitleTypeId=tit.BTitleType where tit.IsDeleted='N' and tit.BTitleType=@BTitleType";

            SqlCommand cmd = new SqlCommand(strSQL);
            cmd.CommandType = CommandType.Text;
            SqlParameter p_TriID = cmd.Parameters.Add("BTitleType", SqlDbType.Int);
            p_TriID.Direction = ParameterDirection.Input;
            p_TriID.Value = bTitleTypeId;
            return objDC.CreateDT(cmd, "TrBudgetTitle");
        }
        else
        {
            string strSQL = @"select tit.BTitleId,tit.BTitleType,tit.BTitle,tit.IsActive,BTitleTypeName from TrBudgetTitle tit
                               inner join TrBudgetTitleType type on type.BTitleTypeId=tit.BTitleType where tit.IsDeleted='N' and tit.BTitleId=@bTitleId";

            SqlCommand cmd = new SqlCommand(strSQL);
            cmd.CommandType = CommandType.Text;
            SqlParameter p_TriID = cmd.Parameters.Add("bTitleId", SqlDbType.Int);
            p_TriID.Direction = ParameterDirection.Input;
            p_TriID.Value = bTitleId;
            return objDC.CreateDT(cmd, "TrBudgetTitle");
        }
    }

    public DataTable SelectBudgetTitleType(string strBTitleTypeId)
    {
        if (strBTitleTypeId.Equals("A"))
        {
            string strSQL = @"select BTitleTypeId,BTitleTypeName,InActive from TrBudgetTitleType where IsDeleted='N'";
            return objDC.CreateDT(strSQL, "TrBudgetTitleType");
        }
        else
        {
            string strSQL = @"select BTitleTypeId,BTitleTypeName,InActive from TrBudgetTitleType where IsDeleted='N' and BTitleTypeId=@bTitleTypeId";

            SqlCommand cmd = new SqlCommand(strSQL);
            cmd.CommandType = CommandType.Text;
            SqlParameter p_TriID = cmd.Parameters.Add("bTitleTypeId", SqlDbType.BigInt);
            p_TriID.Direction = ParameterDirection.Input;
            p_TriID.Value = Int64.Parse(strBTitleTypeId);
            return objDC.CreateDT(cmd, "TrBudgetTitleType");
        }
    }
    #endregion


    public TrainingManager()
    {
        //
        // TODO: Add constructor logic here
        //
    }

}