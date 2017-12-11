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

public class AppraisalManager
{
    DBConnector objDC = new DBConnector();

    #region Configuration Info
    // Insert or Update  or Delete Data of Appraisal Rating table  
    public void InsertAppraisalRating(clsCommonSetup clsCommon, string strRatingMin, string strRatingMax, string strBasicPercent, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_AppraisalRatingList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("RatingId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = clsCommon.ID;

        SqlParameter p_Name = command.Parameters.Add("RatingName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = clsCommon.Name;

        SqlParameter p_RatingMin = command.Parameters.Add("RatingMin", SqlDbType.BigInt);
        p_RatingMin.Direction = ParameterDirection.Input;
        p_RatingMin.Value = strRatingMin;

        SqlParameter p_RatingMax = command.Parameters.Add("RatingMax", SqlDbType.BigInt);
        p_RatingMax.Direction = ParameterDirection.Input;
        p_RatingMax.Value = strRatingMax;

        SqlParameter p_BasicPercent = command.Parameters.Add("BasicPercent", SqlDbType.Decimal);
        p_BasicPercent.Direction = ParameterDirection.Input;
        p_BasicPercent.Value = strBasicPercent;

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


    // Insert or Update  or Delete Data of Learning Activity table  
    public void InsertLearningActivity(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_LearningActivityList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LActivityId = command.Parameters.Add("LActivityId", SqlDbType.BigInt);
        p_LActivityId.Direction = ParameterDirection.Input;
        p_LActivityId.Value = clsCommon.ID;

        SqlParameter p_LActivityName = command.Parameters.Add("LActivityName", SqlDbType.VarChar);
        p_LActivityName.Direction = ParameterDirection.Input;
        p_LActivityName.Value = clsCommon.Name;

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

    // Insert or Update  or Delete Data of Learning Method table  
    public void InsertLearningMethod(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_LearningMethodList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LMethodId = command.Parameters.Add("LMethodId", SqlDbType.BigInt);
        p_LMethodId.Direction = ParameterDirection.Input;
        p_LMethodId.Value = clsCommon.ID;

        SqlParameter p_LMethodName = command.Parameters.Add("LMethodName", SqlDbType.VarChar);
        p_LMethodName.Direction = ParameterDirection.Input;
        p_LMethodName.Value = clsCommon.Name;

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

    // Insert or Update  or Delete Data of Learning Area table  
    public void InsertLearningArea(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_LearningAreaList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LMethodId = command.Parameters.Add("LAreaId", SqlDbType.BigInt);
        p_LMethodId.Direction = ParameterDirection.Input;
        p_LMethodId.Value = clsCommon.ID;

        SqlParameter p_LMethodName = command.Parameters.Add("LAreaName", SqlDbType.VarChar);
        p_LMethodName.Direction = ParameterDirection.Input;
        p_LMethodName.Value = clsCommon.Name;

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

    public DataTable SelectAppraisalRating(int Id)
    {
        SqlCommand command = new SqlCommand("proc_Select_AppraisalRatingList");

        SqlParameter p_Id = command.Parameters.Add("RatingId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = Id;

        objDC.CreateDSFromProc(command, "tblAppraisalRatingList");
        return objDC.ds.Tables["tblAppraisalRatingList"];
    }

    public DataTable SelectLearningActivity(int Id)
    {
        SqlCommand command = new SqlCommand("proc_Select_LearningActivityList");

        SqlParameter p_Id = command.Parameters.Add("LActivityId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = Id;

        objDC.CreateDSFromProc(command, "tblLearningActivityList");
        return objDC.ds.Tables["tblLearningActivityList"];
    }

    public DataTable SelectLearningMethod(int Id)
    {
        SqlCommand command = new SqlCommand("proc_Select_LearningMethodList");

        SqlParameter p_Id = command.Parameters.Add("LMethodId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = Id;

        objDC.CreateDSFromProc(command, "tblLearningMethodList");
        return objDC.ds.Tables["tblLearningMethodList"];
    }

    public DataTable SelectLearningArea(int Id)
    {
        SqlCommand command = new SqlCommand("proc_Select_LearningAreaList");

        SqlParameter p_Id = command.Parameters.Add("LAreaId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = Id;

        objDC.CreateDSFromProc(command, "tblLearningAreaList");
        return objDC.ds.Tables["tblLearningAreaList"];
    }

    public DataTable SelectResourcePerson(int Id,string strActive)
    {
        SqlCommand command = new SqlCommand("proc_Select_ResourcePersonList");

        SqlParameter p_Id = command.Parameters.Add("ResourcePersonId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = Id;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = strActive;

        objDC.CreateDSFromProc(command, "tblResourcePersonList");
        return objDC.ds.Tables["tblResourcePersonList"];
    }
    #endregion

    #region Transaction Info


    //public void InsertAppraisal(string strAppId, string strEmpId, string strEntryDate, string strFiscalYrId, string strIsMidTerm, string strTotalRating,
    //    string strOverallRating, string strRemarks, string strInsBy, string strInsDate, GridView grActivity, string strIsUpdate)
    //{
    //    SqlCommand[] command = new SqlCommand[grActivity.Rows.Count + 2];

    //    command[0] = new SqlCommand("proc_Insert_AppraisalMst");
    //    command[0].CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_AppId = command[0].Parameters.Add("AppId", SqlDbType.BigInt);
    //    p_AppId.Direction = ParameterDirection.Input;
    //    p_AppId.Value = strAppId;

    //    SqlParameter p_EmpID = command[0].Parameters.Add("EmpId", SqlDbType.VarChar);
    //    p_EmpID.Direction = ParameterDirection.Input;
    //    p_EmpID.Value = strEmpId;

    //    SqlParameter p_EntryDate = command[0].Parameters.Add("EntryDate", DBNull.Value);
    //    p_EntryDate.Direction = ParameterDirection.Input;
    //    p_EntryDate.IsNullable = true;
    //    if (strEntryDate != "")
    //        p_EntryDate.Value = strEntryDate;

    //    SqlParameter p_FiscalYrId = command[0].Parameters.Add("FiscalYrId", SqlDbType.BigInt);
    //    p_FiscalYrId.Direction = ParameterDirection.Input;
    //    p_FiscalYrId.Value = strFiscalYrId;

    //    SqlParameter p_IsMidTerm = command[0].Parameters.Add("IsMidTerm", SqlDbType.Char);
    //    p_IsMidTerm.Direction = ParameterDirection.Input;
    //    p_IsMidTerm.Value = strIsMidTerm;

    //    SqlParameter p_TotalRating = command[0].Parameters.Add("TotalRating", DBNull.Value);
    //    p_TotalRating.Direction = ParameterDirection.Input;
    //    p_TotalRating.IsNullable = true;
    //    if (strTotalRating != "")
    //        p_TotalRating.Value = strTotalRating;

    //    SqlParameter p_OverallRating = command[0].Parameters.Add("OverallRating", DBNull.Value);
    //    p_OverallRating.Direction = ParameterDirection.Input;
    //    p_OverallRating.IsNullable = true;
    //    if (strOverallRating != "")
    //        p_OverallRating.Value = strOverallRating;

    //    SqlParameter p_Remarks = command[0].Parameters.Add("Remarks", SqlDbType.VarChar);
    //    p_Remarks.Direction = ParameterDirection.Input;
    //    p_Remarks.Value = strRemarks;

    //    SqlParameter p_InsertedBy = command[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy.Direction = ParameterDirection.Input;
    //    p_InsertedBy.Value = strInsBy;

    //    SqlParameter p_InsertedDate = command[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.Value = strInsDate;

    //    SqlParameter p_IsUpdate = command[0].Parameters.Add("IsUpdate", SqlDbType.Char);
    //    p_IsUpdate.Direction = ParameterDirection.Input;
    //    p_IsUpdate.Value = strIsUpdate;

    //    if (strIsMidTerm == "N")
    //    {
    //        command[1] = new SqlCommand("proc_Delete_AppraisalDet");
    //        command[1].CommandType = CommandType.StoredProcedure;

    //        p_AppId = command[1].Parameters.Add("AppId", SqlDbType.BigInt);
    //        p_AppId.Direction = ParameterDirection.Input;
    //        p_AppId.Value = strAppId;

    //        int i = 2;
    //        foreach (GridViewRow gRow in grActivity.Rows)
    //        {
    //            command[i] = new SqlCommand("proc_Insert_AppraisalDet");
    //            command[i].CommandType = CommandType.StoredProcedure;

    //            p_AppId = command[i].Parameters.Add("AppId", SqlDbType.BigInt);
    //            p_AppId.Direction = ParameterDirection.Input;
    //            p_AppId.Value = strAppId;

    //            SqlParameter p_ActivityName = command[i].Parameters.Add("ActivityName", SqlDbType.VarChar);
    //            p_ActivityName.Direction = ParameterDirection.Input;
    //            p_ActivityName.Value = gRow.Cells[2].Text.Trim();

    //            SqlParameter p_ActivityDesc = command[i].Parameters.Add("ActivityDesc", SqlDbType.VarChar);
    //            p_ActivityDesc.Direction = ParameterDirection.Input;
    //            p_ActivityDesc.Value = gRow.Cells[3].Text.Trim();

    //            SqlParameter p_Rating = command[i].Parameters.Add("Rating", SqlDbType.Decimal);
    //            p_Rating.Direction = ParameterDirection.Input;
    //            p_Rating.Value = gRow.Cells[4].Text.Trim();

    //            p_InsertedBy = command[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //            p_InsertedBy.Direction = ParameterDirection.Input;
    //            p_InsertedBy.Value = strInsBy;

    //            p_InsertedDate = command[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //            p_InsertedDate.Direction = ParameterDirection.Input;
    //            p_InsertedDate.Value = strInsDate;

    //            i++;
    //        }
    //    }
    //    try
    //    {
    //        objDC.MakeTransaction(command);
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


    public void InsertAppraisal(clsPerformanceAppraisal objAPPppraisal, GridView grActivity, string strIsUpdate)
    {
        SqlCommand[] command = new SqlCommand[grActivity.Rows.Count + 2];

        command[0] = new SqlCommand("proc_Insert_AppraisalMst");
        command[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_AppId = command[0].Parameters.Add("AppId", SqlDbType.BigInt);
        p_AppId.Direction = ParameterDirection.Input;
        p_AppId.Value = Convert.ToInt32(objAPPppraisal.AppId);

        SqlParameter p_EmpID = command[0].Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = objAPPppraisal.EmpId;

        SqlParameter p_EntryDate = command[0].Parameters.Add("EntryDate", DBNull.Value);
        p_EntryDate.Direction = ParameterDirection.Input;
        p_EntryDate.IsNullable = true;
        if (objAPPppraisal.EntryDate != "")
            p_EntryDate.Value = objAPPppraisal.EntryDate;

        SqlParameter p_FiscalYrId = command[0].Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = objAPPppraisal.FiscalYrId;

        SqlParameter p_IsMidTerm = command[0].Parameters.Add("IsMidTerm", SqlDbType.Char);
        p_IsMidTerm.Direction = ParameterDirection.Input;
        p_IsMidTerm.Value = objAPPppraisal.IsMidTerm;

        SqlParameter p_TotalRating = command[0].Parameters.Add("TotalRating", DBNull.Value);
        p_TotalRating.Direction = ParameterDirection.Input;
        p_TotalRating.IsNullable = true;
        if (objAPPppraisal.TotlalRating != "")
        p_TotalRating.Value = objAPPppraisal.TotlalRating;

        SqlParameter p_OverallRating = command[0].Parameters.Add("OverallRating", DBNull.Value);
        p_OverallRating.Direction = ParameterDirection.Input;
        p_OverallRating.IsNullable = true;
        if (objAPPppraisal.Overallrating != "")
            p_OverallRating.Value = objAPPppraisal.Overallrating;

        SqlParameter p_Remarks = command[0].Parameters.Add("Remarks", SqlDbType.VarChar);
        p_Remarks.Direction = ParameterDirection.Input;
        p_Remarks.Value = objAPPppraisal.Remarks;

        SqlParameter p_InsertedBy = command[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = objAPPppraisal.InsertedBy;

        SqlParameter p_InsertedDate = command[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = objAPPppraisal.InsertedDate;

        SqlParameter p_IsUpdate = command[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = strIsUpdate;

        if (objAPPppraisal.IsMidTerm == "N")
        {
            command[1] = new SqlCommand("proc_Delete_AppraisalDet");
            command[1].CommandType = CommandType.StoredProcedure;

            p_AppId = command[1].Parameters.Add("AppId", SqlDbType.BigInt);
            p_AppId.Direction = ParameterDirection.Input;
            p_AppId.Value = Convert.ToInt32(objAPPppraisal.AppId);

            int i = 2;
            foreach (GridViewRow gRow in grActivity.Rows)
            {
                command[i] = new SqlCommand("proc_Insert_AppraisalDet");
                command[i].CommandType = CommandType.StoredProcedure;

                p_AppId = command[i].Parameters.Add("AppId", SqlDbType.BigInt);
                p_AppId.Direction = ParameterDirection.Input;
                p_AppId.Value = Convert.ToInt32(objAPPppraisal.AppId);

                SqlParameter p_ActivityName = command[i].Parameters.Add("ActivityName", SqlDbType.VarChar);
                p_ActivityName.Direction = ParameterDirection.Input;
                p_ActivityName.Value = gRow.Cells[1].Text.Trim();

                SqlParameter p_ActivityDesc = command[i].Parameters.Add("ActivityDesc", SqlDbType.VarChar);
                p_ActivityDesc.Direction = ParameterDirection.Input;
                p_ActivityDesc.Value = gRow.Cells[2].Text.Trim();

                SqlParameter p_Rating = command[i].Parameters.Add("Rating", SqlDbType.Decimal);
                p_Rating.Direction = ParameterDirection.Input;
                p_Rating.Value = Convert.ToDecimal(gRow.Cells[3].Text.Trim());

                p_InsertedBy = command[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                p_InsertedBy.Direction = ParameterDirection.Input;
                p_InsertedBy.Value = objAPPppraisal.InsertedBy;

                p_InsertedDate = command[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                p_InsertedDate.Direction = ParameterDirection.Input;
                p_InsertedDate.Value = objAPPppraisal.InsertedDate;

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

    //public void InsertDevelopmentNeed(string strDevId, string strEmpId, string strFiscalYrId, GridView grActivity,  string strInsBy, string strInsDate,string strIsUpdate)
    //{
    //    SqlCommand[] command = new SqlCommand[grActivity.Rows.Count];
    //    int i = 0;
    //    foreach (GridViewRow gRow in grActivity.Rows)
    //    {
    //        command[i] = new SqlCommand("proc_Insert_DevelopmentNeeds");
    //        command[i].CommandType = CommandType.StoredProcedure;

    //        SqlParameter p_DevId = command[i].Parameters.Add("DevId", SqlDbType.BigInt);
    //        p_DevId.Direction = ParameterDirection.Input;
    //        p_DevId.Value = strDevId;

    //        SqlParameter p_EmpID = command[i].Parameters.Add("EmpId", SqlDbType.VarChar);
    //        p_EmpID.Direction = ParameterDirection.Input;
    //        p_EmpID.Value = strEmpId;

    //        SqlParameter p_FiscalYrId = command[i].Parameters.Add("FiscalYrId", SqlDbType.BigInt);
    //        p_FiscalYrId.Direction = ParameterDirection.Input;
    //        p_FiscalYrId.Value = strFiscalYrId;

    //        SqlParameter p_LActivityId = command[i].Parameters.Add("LActivityId", SqlDbType.BigInt);
    //        p_LActivityId.Direction = ParameterDirection.Input;
    //        p_LActivityId.Value = grActivity.DataKeys[i].Values[0].ToString();

    //        SqlParameter p_LMethodId = command[i].Parameters.Add("LMethodId", DBNull.Value);
    //        p_LMethodId.Direction = ParameterDirection.Input;
    //        p_LMethodId.IsNullable = true;
    //        if (grActivity.DataKeys[i].Values[1].ToString() != "99999")
    //            p_LMethodId.Value = grActivity.DataKeys[i].Values[1].ToString();

    //        SqlParameter p_LAreaId = command[i].Parameters.Add("LAreaId", DBNull.Value);
    //        p_LAreaId.Direction = ParameterDirection.Input;
    //        p_LAreaId.IsNullable = true;
    //        if (grActivity.DataKeys[i].Values[2].ToString() != "99999")
    //            p_LAreaId.Value = grActivity.DataKeys[i].Values[2].ToString();



    //        //SqlParameter p_StartDate = command[i].Parameters.Add("StartDate", DBNull.Value);
    //        //p_StartDate.Direction = ParameterDirection.Input;
    //        //p_StartDate.IsNullable = true;
    //        //if (Common.CheckNullString(gRow.Cells[5].Text.Trim()) != "")
    //        //    p_StartDate.Value = gRow.Cells[5].Text.Trim();

    //        //SqlParameter p_EndDate = command[i].Parameters.Add("EndDate", DBNull.Value);
    //        //p_EndDate.Direction = ParameterDirection.Input;
    //        //p_EndDate.IsNullable = true;
    //        //if (Common.CheckNullString(gRow.Cells[6].Text.Trim()) != "")
    //        //    p_EndDate.Value = gRow.Cells[6].Text.Trim();


    //        SqlParameter p_TMonth = command[i].Parameters.Add("TMonth", DBNull.Value);
    //        p_TMonth.Direction = ParameterDirection.Input;
    //        p_TMonth.IsNullable = true;
    //        if (Common.CheckNullString(gRow.Cells[5].Text.Trim()) != "")
    //            p_TMonth.Value = gRow.Cells[5].Text.Trim();

    //        SqlParameter p_TYaer = command[i].Parameters.Add("TYear", DBNull.Value);
    //        p_TYaer.Direction = ParameterDirection.Input;
    //        p_TYaer.IsNullable = true;
    //        if (Common.CheckNullString(gRow.Cells[6].Text.Trim()) != "")
    //            p_TYaer.Value = gRow.Cells[6].Text.Trim();

    //        SqlParameter p_AMonth = command[i].Parameters.Add("AMonth", DBNull.Value);
    //        p_AMonth.Direction = ParameterDirection.Input;
    //        p_AMonth.IsNullable = true;
    //        if (Common.CheckNullString(gRow.Cells[7].Text.Trim()) != "")
    //            p_AMonth.Value = gRow.Cells[7].Text.Trim();

    //        SqlParameter p_AYaer = command[i].Parameters.Add("AYear", DBNull.Value);
    //        p_AYaer.Direction = ParameterDirection.Input;
    //        p_AYaer.IsNullable = true;
    //        if (Common.CheckNullString(gRow.Cells[8].Text.Trim()) != "")
    //            p_AYaer.Value = gRow.Cells[8].Text.Trim();


    //        SqlParameter p_Cost = command[i].Parameters.Add("Cost", DBNull.Value);
    //        p_Cost.Direction = ParameterDirection.Input;
    //        p_Cost.IsNullable = true;
    //        if (gRow.Cells[9].Text.Trim() != "")
    //            p_Cost.Value = gRow.Cells[9].Text.Trim();

    //        SqlParameter p_ResourcePersonId = command[i].Parameters.Add("ResourcePersonId", DBNull.Value);
    //        p_ResourcePersonId.Direction = ParameterDirection.Input;
    //        p_ResourcePersonId.IsNullable = true;
    //        if (grActivity.DataKeys[i].Values[3].ToString() != "99999")
    //            p_ResourcePersonId.Value = grActivity.DataKeys[i].Values[3].ToString();// gRow.Cells[10].Text.Trim(); 


    //        SqlParameter p_InsertedBy = command[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //        p_InsertedBy.Direction = ParameterDirection.Input;
    //        p_InsertedBy.Value = strInsBy;

    //        SqlParameter p_InsertedDate = command[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //        p_InsertedDate.Direction = ParameterDirection.Input;
    //        p_InsertedDate.Value = strInsDate;

    //        SqlParameter p_IsUpdate = command[i].Parameters.Add("IsUpdate", SqlDbType.Char);
    //        p_IsUpdate.Direction = ParameterDirection.Input;
    //        p_IsUpdate.Value = strIsUpdate;

    //        i++;
    //    }

    //    try
    //    {
    //        objDC.MakeTransaction(command);
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


    public DataTable SelectAppraisalMst(int AppId, string EmpID)
    {
        SqlCommand command = new SqlCommand("proc_Select_AppraisalMst");

        SqlParameter p_AppId = command.Parameters.Add("AppId", SqlDbType.BigInt);
        p_AppId.Direction = ParameterDirection.Input;
        p_AppId.Value = AppId;

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;
        objDC.CreateDSFromProc(command, "tblAppraisalMst");
        return objDC.ds.Tables["tblAppraisalMst"];
    }
    //proc_Select_AppraisalDet MAMUN
    public DataTable SelectAppraisalDet(int AppId)
    {
        SqlCommand command = new SqlCommand("proc_Select_AppraisalDet");

        SqlParameter p_AppId = command.Parameters.Add("AppId", SqlDbType.BigInt);
        p_AppId.Direction = ParameterDirection.Input;
        p_AppId.Value = AppId;

        objDC.CreateDSFromProc(command, "tblAppraisalDet");
        return objDC.ds.Tables["tblAppraisalDet"];
    }

    

         public DataTable SelectAppraisalMstGrd(string EmpID)
    {
        SqlCommand command = new SqlCommand("proc_Select_AppraisalMstGrd");

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;
        objDC.CreateDSFromProc(command, "tblAppraisalMst");
        return objDC.ds.Tables["tblAppraisalMst"];
    }



    //------------------------ New added bu shazzad -----------------------

    public void InsertJobPlanSubmission(string strEmpJobPlanId, string strEmpId, string IsSubmitted, string SubmissionDate, string strInsBy, string strIsUpdate, string strIsDelete)
    {
            
        SqlCommand[] command = new SqlCommand[1];
        int i = 0;

            command[i] = new SqlCommand("proc_InUp_EmpJobPlanSubmission");
            command[i].CommandType = CommandType.StoredProcedure;

            SqlParameter p_EmpJobPlanId = command[i].Parameters.Add("EmpJobPlanId", SqlDbType.BigInt);
            p_EmpJobPlanId.Direction = ParameterDirection.Input;
            p_EmpJobPlanId.Value = strEmpJobPlanId;

            SqlParameter p_EmpID = command[i].Parameters.Add("EmpId", SqlDbType.VarChar);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = strEmpId;

            SqlParameter p_IsSubmitted = command[i].Parameters.Add("IsSubmitted", SqlDbType.Char);
            p_IsSubmitted.Direction = ParameterDirection.Input;
            p_IsSubmitted.Value = IsSubmitted;

            SqlParameter p_SubmissionDate = command[i].Parameters.Add("SubmissionDate", SqlDbType.DateTime);
            p_SubmissionDate.Direction = ParameterDirection.Input;
            p_SubmissionDate.Value = Common.ReturnDate(SubmissionDate);

            SqlParameter p_InsertedBy = command[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = strInsBy;

            SqlParameter p_IsUpdate = command[i].Parameters.Add("IsUpdate", SqlDbType.Char);
            p_IsUpdate.Direction = ParameterDirection.Input;
            p_IsUpdate.Value = strIsUpdate;

            SqlParameter p_IsDelete = command[i].Parameters.Add("IsDelete", SqlDbType.Char);
            p_IsDelete.Direction = ParameterDirection.Input;
            p_IsDelete.Value = strIsDelete;

            i++;
        

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


    public DataTable SelectJobPlanSubmission(string empID)
    {

        SqlCommand command = new SqlCommand("proc_Select_JobPlanSubmission");

     
        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = empID;

        objDC.CreateDSFromProc(command, "tblJobPlanSubmission");
        return objDC.ds.Tables["tblJobPlanSubmission"];
    }
    #endregion

    //public void InsertDevelopmentNeed(string strDevId, string strEmpId, string strFiscalYrId, GridView grActivity, string strInsBy, string strInsDate, string strIsUpdate, string strIsDelete)
    //{
    //    SqlCommand[] command = new SqlCommand[grActivity.Rows.Count];
    //    int i = 0;
    //    int iDevId = 0;
    //    iDevId = Convert.ToInt32(strDevId);

    //    foreach (GridViewRow gRow in grActivity.Rows)
    //    {
    //        command[i] = new SqlCommand("proc_Insert_DevelopmentNeeds");
    //        command[i].CommandType = CommandType.StoredProcedure;
           
    //        SqlParameter p_DevId = command[i].Parameters.Add("DevId", SqlDbType.BigInt);
    //        p_DevId.Direction = ParameterDirection.Input;
    //        p_DevId.Value = iDevId;
            
    //        SqlParameter p_EmpID = command[i].Parameters.Add("EmpId", SqlDbType.VarChar);
    //        p_EmpID.Direction = ParameterDirection.Input;
    //        p_EmpID.Value = strEmpId;

    //        SqlParameter p_FiscalYrId = command[i].Parameters.Add("FiscalYrId", SqlDbType.BigInt);
    //        p_FiscalYrId.Direction = ParameterDirection.Input;
    //        p_FiscalYrId.Value = strFiscalYrId;

    //        SqlParameter p_LActivityId = command[i].Parameters.Add("LActivityId", SqlDbType.BigInt);
    //        p_LActivityId.Direction = ParameterDirection.Input;
    //        p_LActivityId.Value = grActivity.DataKeys[i].Values[0].ToString();

    //        SqlParameter p_LMethodId = command[i].Parameters.Add("LMethodId", DBNull.Value);
    //        p_LMethodId.Direction = ParameterDirection.Input;
    //        p_LMethodId.IsNullable = true;
    //        if (grActivity.DataKeys[i].Values[1].ToString() != "99999")
    //            p_LMethodId.Value = grActivity.DataKeys[i].Values[1].ToString();

    //        SqlParameter p_LAreaId = command[i].Parameters.Add("LAreaId", DBNull.Value);
    //        p_LAreaId.Direction = ParameterDirection.Input;
    //        p_LAreaId.IsNullable = true;
    //        if (grActivity.DataKeys[i].Values[2].ToString() != "99999")
    //            p_LAreaId.Value = grActivity.DataKeys[i].Values[2].ToString();


    //        SqlParameter p_TMonth = command[i].Parameters.Add("TMonth", DBNull.Value);
    //        p_TMonth.Direction = ParameterDirection.Input;
    //        p_TMonth.IsNullable = true;
    //        if (Common.CheckNullString(gRow.Cells[5].Text.Trim()) != "")
    //            p_TMonth.Value = gRow.Cells[5].Text.Trim();

    //        SqlParameter p_TYaer = command[i].Parameters.Add("TYear", DBNull.Value);
    //        p_TYaer.Direction = ParameterDirection.Input;
    //        p_TYaer.IsNullable = true;
    //        if (Common.CheckNullString(gRow.Cells[6].Text.Trim()) != "")
    //            p_TYaer.Value = gRow.Cells[6].Text.Trim();

    //        SqlParameter p_AMonth = command[i].Parameters.Add("AMonth", DBNull.Value);
    //        p_AMonth.Direction = ParameterDirection.Input;
    //        p_AMonth.IsNullable = true;
    //        if (Common.CheckNullString(gRow.Cells[7].Text.Trim()) != "")
    //            p_AMonth.Value = gRow.Cells[7].Text.Trim();

    //        SqlParameter p_AYaer = command[i].Parameters.Add("AYear", DBNull.Value);
    //        p_AYaer.Direction = ParameterDirection.Input;
    //        p_AYaer.IsNullable = true;
    //        if (Common.CheckNullString(gRow.Cells[8].Text.Trim()) != "")
    //            p_AYaer.Value = gRow.Cells[8].Text.Trim();


    //        SqlParameter p_Cost = command[i].Parameters.Add("Cost", DBNull.Value);
    //        p_Cost.Direction = ParameterDirection.Input;
    //        p_Cost.IsNullable = true;
    //        if (gRow.Cells[9].Text.Trim() != "")
    //            p_Cost.Value = gRow.Cells[9].Text.Trim();

    //        SqlParameter p_ResourcePersonId = command[i].Parameters.Add("ResourcePersonId", DBNull.Value);
    //        p_ResourcePersonId.Direction = ParameterDirection.Input;
    //        p_ResourcePersonId.IsNullable = true;
    //        if (grActivity.DataKeys[i].Values[3].ToString() != "99999")
    //            p_ResourcePersonId.Value = grActivity.DataKeys[i].Values[3].ToString();// gRow.Cells[10].Text.Trim(); 


    //        SqlParameter p_InsertedBy = command[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //        p_InsertedBy.Direction = ParameterDirection.Input;
    //        p_InsertedBy.Value = strInsBy;

    //        SqlParameter p_InsertedDate = command[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //        p_InsertedDate.Direction = ParameterDirection.Input;
    //        p_InsertedDate.Value = strInsDate;

    //        SqlParameter p_IsUpdate = command[i].Parameters.Add("IsUpdate", SqlDbType.Char);
    //        p_IsUpdate.Direction = ParameterDirection.Input;
    //        p_IsUpdate.Value = strIsUpdate;

    //        SqlParameter p_IsDelete = command[i].Parameters.Add("IsDelete", SqlDbType.Char);
    //        p_IsDelete.Direction = ParameterDirection.Input;
    //        p_IsDelete.Value = strIsDelete;

    //        SqlParameter p_chkIsBudgetReq = command[i].Parameters.Add("IsBudgetReq", SqlDbType.Char);
    //        p_chkIsBudgetReq.Direction = ParameterDirection.Input;
    //        p_chkIsBudgetReq.Value = (gRow.Cells[11].Text.Trim() == "Yes" ? "Y" : "N");

    //        i++;
    //        iDevId++;
    //    }

    //    try
    //    {
    //        objDC.MakeTransaction(command);
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

    //public void InsertDevelopmentNeed( clsDevelopmentNeeds DevNeeds,string strIsUpdate, string strIsDelete)
    //{
    //    SqlCommand[] command = new SqlCommand[1];
    //    int i = 0;
    //    int iDevId = 0;
    //    iDevId = Convert.ToInt32(DevNeeds.DevId);

       
    //        command[i] = new SqlCommand("proc_Insert_DevelopmentNeeds");
    //        command[i].CommandType = CommandType.StoredProcedure;

    //        SqlParameter p_DevId = command[i].Parameters.Add("DevId", SqlDbType.BigInt);
    //        p_DevId.Direction = ParameterDirection.Input;
    //        p_DevId.Value = iDevId;

    //        SqlParameter p_EmpID = command[i].Parameters.Add("EmpId", SqlDbType.VarChar);
    //        p_EmpID.Direction = ParameterDirection.Input;
    //        p_EmpID.Value = DevNeeds.EmpId;

    //        SqlParameter p_FiscalYrId = command[i].Parameters.Add("FiscalYrId", SqlDbType.BigInt);
    //        p_FiscalYrId.Direction = ParameterDirection.Input;
    //        p_FiscalYrId.Value = DevNeeds.FiscalYrId;

    //        SqlParameter p_LActivityId = command[i].Parameters.Add("LActivityId", SqlDbType.BigInt);
    //        p_LActivityId.Direction = ParameterDirection.Input;
    //        p_LActivityId.Value = DevNeeds.LActivityId;

    //        SqlParameter p_LMethodId = command[i].Parameters.Add("LMethodId", DBNull.Value);
    //        p_LMethodId.Direction = ParameterDirection.Input;
    //        p_LMethodId.IsNullable = true;
    //        if (DevNeeds.LMethodId != "99999")
    //        p_LMethodId.Value = DevNeeds.LMethodId;

    //        SqlParameter p_LAreaId = command[i].Parameters.Add("LAreaId", DBNull.Value);
    //        p_LAreaId.Direction = ParameterDirection.Input;
    //        p_LAreaId.IsNullable = true;
    //         if (DevNeeds.LAreaId != "99999")
    //        p_LAreaId.Value = DevNeeds.LAreaId;


    //        SqlParameter p_TMonth = command[i].Parameters.Add("TMonth", DBNull.Value);
    //        p_TMonth.Direction = ParameterDirection.Input;
    //        p_TMonth.IsNullable = true;
    //         if (DevNeeds.LAreaId != "99999")
    //        p_TMonth.Value = DevNeeds.LAreaId;

    //        SqlParameter p_TYaer = command[i].Parameters.Add("TYear", DBNull.Value);
    //        p_TYaer.Direction = ParameterDirection.Input;
    //        p_TYaer.IsNullable = true;
    //         if (DevNeeds.TYear != "99999")
    //        p_TYaer.Value = DevNeeds.TYear;

    //        SqlParameter p_AMonth = command[i].Parameters.Add("AMonth", DBNull.Value);
    //        p_AMonth.Direction = ParameterDirection.Input;
    //        p_AMonth.IsNullable = true;
    //        if (DevNeeds.AMonth != "99999")
    //        p_AMonth.Value = DevNeeds.AMonth;

    //        SqlParameter p_AYaer = command[i].Parameters.Add("AYear", DBNull.Value);
    //        p_AYaer.Direction = ParameterDirection.Input;
    //        p_AYaer.IsNullable = true;
    //        if (DevNeeds.AYear != "99999")
    //        p_AYaer.Value = DevNeeds.AYear;


    //        SqlParameter p_Cost = command[i].Parameters.Add("Cost", DBNull.Value);
    //        p_Cost.Direction = ParameterDirection.Input;
    //        p_Cost.IsNullable = true;
    //        if (DevNeeds.Cost != "")
    //        p_AYaer.Value = DevNeeds.Cost;

    //        SqlParameter p_ResourcePersonId = command[i].Parameters.Add("ResourcePersonId", DBNull.Value);
    //        p_ResourcePersonId.Direction = ParameterDirection.Input;
    //        p_ResourcePersonId.IsNullable = true;
    //        if (DevNeeds.ResourcePersonId != "99999")
    //        p_ResourcePersonId.Value = DevNeeds.ResourcePersonId;


    //        SqlParameter p_InsertedBy = command[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //        p_InsertedBy.Direction = ParameterDirection.Input;
    //        p_InsertedBy.Value = DevNeeds.InsertedBy;

    //        SqlParameter p_InsertedDate = command[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //        p_InsertedDate.Direction = ParameterDirection.Input;
    //        p_InsertedDate.Value = DevNeeds.InsertedDate;

    //        SqlParameter p_IsUpdate = command[i].Parameters.Add("IsUpdate", SqlDbType.Char);
    //        p_IsUpdate.Direction = ParameterDirection.Input;
    //        p_IsUpdate.Value = strIsUpdate;

    //        SqlParameter p_IsDelete = command[i].Parameters.Add("IsDelete", SqlDbType.Char);
    //        p_IsDelete.Direction = ParameterDirection.Input;
    //        p_IsDelete.Value = strIsDelete;

    //        SqlParameter p_chkIsBudgetReq = command[i].Parameters.Add("IsBudgetReq", SqlDbType.Char);
    //        p_chkIsBudgetReq.Direction = ParameterDirection.Input;
    //        p_chkIsBudgetReq.Value =DevNeeds.IsBudgetReq;

        

    //        i++;
    //        iDevId++;
        

    //    try
    //    {
    //        objDC.MakeTransaction(command);
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

    public void InsertDevelopmentNeed(clsDevelopmentNeeds DevNeeds, string strIsUpdate, string strIsDelete)
    {
        


       SqlCommand command = new SqlCommand("proc_Insert_DevelopmentNeeds");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_DevId = command.Parameters.Add("DevId", SqlDbType.BigInt);
        p_DevId.Direction = ParameterDirection.Input;
        p_DevId.Value = DevNeeds.DevId;

        SqlParameter p_EmpID = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = DevNeeds.EmpId;

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = DevNeeds.FiscalYrId;

        SqlParameter p_LActivityId = command.Parameters.Add("LActivityId", SqlDbType.BigInt);
        p_LActivityId.Direction = ParameterDirection.Input;
        p_LActivityId.Value = DevNeeds.LActivityId;

        SqlParameter p_LMethodId = command.Parameters.Add("LMethodId", DBNull.Value);
        p_LMethodId.Direction = ParameterDirection.Input;
        p_LMethodId.IsNullable = true;
        if (DevNeeds.LMethodId != "99999")
            p_LMethodId.Value = DevNeeds.LMethodId;

        SqlParameter p_LAreaId = command.Parameters.Add("LAreaId", DBNull.Value);
        p_LAreaId.Direction = ParameterDirection.Input;
        p_LAreaId.IsNullable = true;
        if (DevNeeds.LAreaId != "99999")
            p_LAreaId.Value = DevNeeds.LAreaId;


        SqlParameter p_TMonth = command.Parameters.Add("TMonth", DBNull.Value);
        p_TMonth.Direction = ParameterDirection.Input;
        p_TMonth.IsNullable = true;
        if (DevNeeds.LAreaId != "99999")
            p_TMonth.Value = DevNeeds.LAreaId;

        SqlParameter p_TYaer = command.Parameters.Add("TYear", DBNull.Value);
        p_TYaer.Direction = ParameterDirection.Input;
        p_TYaer.IsNullable = true;
        if (DevNeeds.TYear != "99999")
            p_TYaer.Value = DevNeeds.TYear;

        SqlParameter p_AMonth = command.Parameters.Add("AMonth", DBNull.Value);
        p_AMonth.Direction = ParameterDirection.Input;
        p_AMonth.IsNullable = true;
        if (DevNeeds.AMonth != "99999")
            p_AMonth.Value = DevNeeds.AMonth;

        SqlParameter p_AYaer = command.Parameters.Add("AYear", DBNull.Value);
        p_AYaer.Direction = ParameterDirection.Input;
        p_AYaer.IsNullable = true;
        if (DevNeeds.AYear != "99999")
            p_AYaer.Value = DevNeeds.AYear;


        SqlParameter p_Cost = command.Parameters.Add("Cost", DBNull.Value);
        p_Cost.Direction = ParameterDirection.Input;
        p_Cost.IsNullable = true;
        if (DevNeeds.Cost != "")
        p_Cost.Value = DevNeeds.Cost;

        SqlParameter p_ResourcePersonId = command.Parameters.Add("ResourcePersonId", DBNull.Value);
        p_ResourcePersonId.Direction = ParameterDirection.Input;
        p_ResourcePersonId.IsNullable = true;
        if (DevNeeds.ResourcePersonId != "99999")
            p_ResourcePersonId.Value = DevNeeds.ResourcePersonId;


        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = DevNeeds.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = DevNeeds.InsertedDate;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = strIsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = strIsDelete;

        SqlParameter p_chkIsBudgetReq = command.Parameters.Add("IsBudgetReq", SqlDbType.Char);
        p_chkIsBudgetReq.Direction = ParameterDirection.Input;
        p_chkIsBudgetReq.Value = DevNeeds.IsBudgetReq;
    


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
    //Mamun
    public DataTable SelectDevelopmentNeeds(string empID)
    {

        SqlCommand command = new SqlCommand("proc_Select_DevelopmentNeeds");

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = empID;

        objDC.CreateDSFromProc(command, "tblDevelopmentNeeds");
        return objDC.ds.Tables["tblDevelopmentNeeds"];
    }

}
