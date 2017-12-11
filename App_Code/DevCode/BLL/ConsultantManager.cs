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
/// Summary description for ConsultantManager
/// </summary>
public class ConsultantManager
{
    DBConnector objDC = new DBConnector();
	public ConsultantManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Insert Consultant Configuration Info
    // Insert or Update  or Delete Data of Work Area table  
    public void InsertWorkArea(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_ConsWorkAreaList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_SectorId = command.Parameters.Add("WorkAreaId", SqlDbType.BigInt);
        p_SectorId.Direction = ParameterDirection.Input;
        p_SectorId.Value = clsCommon.ID;

        SqlParameter p_SectorName = command.Parameters.Add("WorkAreaName", SqlDbType.VarChar);
        p_SectorName.Direction = ParameterDirection.Input;
        p_SectorName.Value = clsCommon.Name;

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

    // Insert or Update  or Delete Data of Payment Method table  
    public void InsertPaymentMethod(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_ConsPayMethodList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_SectorId = command.Parameters.Add("PayMethodId", SqlDbType.BigInt);
        p_SectorId.Direction = ParameterDirection.Input;
        p_SectorId.Value = clsCommon.ID;

        SqlParameter p_SectorName = command.Parameters.Add("PayMethodName", SqlDbType.VarChar);
        p_SectorName.Direction = ParameterDirection.Input;
        p_SectorName.Value = clsCommon.Name;

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

    // Insert or Update  or Delete Data of Payment Method table  
    public void InsertConsultantType(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_ConsTypeList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_AssessmentRateId = command.Parameters.Add("TypeId", SqlDbType.BigInt);
        p_AssessmentRateId.Direction = ParameterDirection.Input;
        p_AssessmentRateId.Value = clsCommon.ID;

        SqlParameter p_AssessmentRateName = command.Parameters.Add("TypeName", SqlDbType.VarChar);
        p_AssessmentRateName.Direction = ParameterDirection.Input;
        p_AssessmentRateName.Value = clsCommon.Name;

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

    // Insert or Update  or Delete Data of Consultant Hiring Method
    public void InsertConsultantHiringMethod(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_ConsHiringMethodList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_AssessmentRateId = command.Parameters.Add("MethodId", SqlDbType.BigInt);
        p_AssessmentRateId.Direction = ParameterDirection.Input;
        p_AssessmentRateId.Value = clsCommon.ID;

        SqlParameter p_AssessmentRateName = command.Parameters.Add("MethodName", SqlDbType.VarChar);
        p_AssessmentRateName.Direction = ParameterDirection.Input;
        p_AssessmentRateName.Value = clsCommon.Name;

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
    // Insert or Update  or Delete Data of Payment Method table  
    public void InsertAssesmentRate(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_ConsAssessRateList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_AssessmentRateId = command.Parameters.Add("AssessmentRateId", SqlDbType.BigInt);
        p_AssessmentRateId.Direction = ParameterDirection.Input;
        p_AssessmentRateId.Value = clsCommon.ID;

        SqlParameter p_AssessmentRateName = command.Parameters.Add("AssessmentRateName", SqlDbType.VarChar);
        p_AssessmentRateName.Direction = ParameterDirection.Input;
        p_AssessmentRateName.Value = clsCommon.Name;

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

    // Insert or Update  or Delete Data of Payment Method table  
    public void InsertAssesmentNature(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_ConsAssessNatureList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_AssessmentRateId = command.Parameters.Add("AssessNatureId", SqlDbType.BigInt);
        p_AssessmentRateId.Direction = ParameterDirection.Input;
        p_AssessmentRateId.Value = clsCommon.ID;

        SqlParameter p_AssessmentRateName = command.Parameters.Add("AssessNature", SqlDbType.VarChar);
        p_AssessmentRateName.Direction = ParameterDirection.Input;
        p_AssessmentRateName.Value = clsCommon.Name;

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

    // Insert or Update  or Delete Data of Payment Method table  
    public void InsertSupplier(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_SupplierList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_AssessmentRateId = command.Parameters.Add("SupplierId", SqlDbType.BigInt);
        p_AssessmentRateId.Direction = ParameterDirection.Input;
        p_AssessmentRateId.Value = clsCommon.ID;

        SqlParameter p_AssessmentRateName = command.Parameters.Add("SupplierName", SqlDbType.VarChar);
        p_AssessmentRateName.Direction = ParameterDirection.Input;
        p_AssessmentRateName.Value = clsCommon.Name;

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
    #endregion
    #region Insert Consultant Transaction Info
    // Insert or Update or Delete Data of Consultant
    public void InsertConsultantInfo(clsConsultantInfo objClsCons, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_ConsultantInfo");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("ConsId", SqlDbType.VarChar);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = objClsCons.ID;

        SqlParameter p_SectorName = command.Parameters.Add("Name", SqlDbType.VarChar);
        p_SectorName.Direction = ParameterDirection.Input;
        p_SectorName.Value = objClsCons.Name;

        SqlParameter p_Origin = command.Parameters.Add("CountryId", DBNull.Value);
        p_Origin.Direction = ParameterDirection.Input;
        p_Origin.IsNullable = true;
        if (objClsCons.Origin != "")
            p_Origin.Value = objClsCons.Origin;

        SqlParameter p_Type = command.Parameters.Add("ConsTypeId", DBNull.Value);
        p_Type.Direction = ParameterDirection.Input;
        p_Type.IsNullable = true;
        if (objClsCons.Type != "")
        p_Type.Value = objClsCons.Type;

        SqlParameter p_HiringMethod = command.Parameters.Add("MethodId", DBNull.Value);
        p_HiringMethod.Direction = ParameterDirection.Input;
        p_HiringMethod.IsNullable = true;
        if (objClsCons.HiringMethod != "")
        p_HiringMethod.Value = objClsCons.HiringMethod;

        SqlParameter p_Sex = command.Parameters.Add("Sex", SqlDbType.VarChar);
        p_Sex.Direction = ParameterDirection.Input;
        p_Sex.Value = objClsCons.Sex;

        SqlParameter p_WorkAreaID = command.Parameters.Add("WorkAreaID", DBNull.Value);
        p_WorkAreaID.Direction = ParameterDirection.Input;
        p_WorkAreaID.IsNullable = true;
        if (objClsCons.WorkAreaID != "")
            p_WorkAreaID.Value = objClsCons.WorkAreaID;

        SqlParameter p_DetailAssignment = command.Parameters.Add("DetailAssignment", SqlDbType.VarChar);
        p_DetailAssignment.Direction = ParameterDirection.Input;
        p_DetailAssignment.Value = objClsCons.DetailAssignment;

        SqlParameter p_SectorID = command.Parameters.Add("SectorID", DBNull.Value);
        p_SectorID.Direction = ParameterDirection.Input;
        p_SectorID.IsNullable = true;
        if (objClsCons.SectorID != "")
            p_SectorID.Value = objClsCons.SectorID;

        SqlParameter p_DeptId = command.Parameters.Add("DeptId", DBNull.Value);
        p_DeptId.Direction = ParameterDirection.Input;
        p_DeptId.IsNullable = true;
        if (objClsCons.DeptId != "")
            p_DeptId.Value = objClsCons.DeptId;

        SqlParameter p_LocationID = command.Parameters.Add("LocationID", DBNull.Value);
        p_LocationID.Direction = ParameterDirection.Input;
        p_LocationID.IsNullable = true;
        if (objClsCons.LocationID != "")
            p_LocationID.Value = objClsCons.LocationID;

        SqlParameter p_SuperviserID = command.Parameters.Add("SuperviserID", DBNull.Value);
        p_SuperviserID.Direction = ParameterDirection.Input;
        p_SuperviserID.IsNullable = true;
        if (objClsCons.SuperviserID != "")
            p_SuperviserID.Value = "1";// objClsCons.SuperviserID;

        SqlParameter p_Religion = command.Parameters.Add("Religion", DBNull.Value);
        p_Religion.Direction = ParameterDirection.Input;
        p_Religion.IsNullable = true;
        if (objClsCons.Religion != "")
            p_Religion.Value = objClsCons.Religion;

        SqlParameter p_IsUKClearance = command.Parameters.Add("IsUKClearance", SqlDbType.Char);
        p_IsUKClearance.Direction = ParameterDirection.Input;
        p_IsUKClearance.Value = objClsCons.IsUKClearance;

        SqlParameter p_Vetting = command.Parameters.Add("VettingRemarks", SqlDbType.VarChar);
        p_Vetting.Direction = ParameterDirection.Input;
        p_Vetting.Value = objClsCons.VettingRemarks;

        SqlParameter p_StartDate = command.Parameters.Add("StartDate", DBNull.Value);
        p_StartDate.Direction = ParameterDirection.Input;
        p_StartDate.IsNullable = true;
        if (objClsCons.StartDate != "")
            p_StartDate.Value = objClsCons.StartDate;

        SqlParameter p_EndDate = command.Parameters.Add("EndDate", DBNull.Value);
        p_EndDate.Direction = ParameterDirection.Input;
        p_EndDate.IsNullable = true;
        if (objClsCons.EndDate != "")
            p_EndDate.Value = objClsCons.EndDate;

        SqlParameter p_TINNo = command.Parameters.Add("TINNo", SqlDbType.VarChar);
        p_TINNo.Direction = ParameterDirection.Input;
        p_TINNo.Value = objClsCons.TINNo;

        SqlParameter p_Bank = command.Parameters.Add("BankCode", DBNull.Value);
        p_Bank.Direction = ParameterDirection.Input;
        p_Bank.IsNullable = true;
        if (objClsCons.BankCode != "")
            p_Bank.Value = "1"; //objClsCons.BankCode;

        SqlParameter p_BankAccNo = command.Parameters.Add("BankAccNo", SqlDbType.VarChar);
        p_BankAccNo.Direction = ParameterDirection.Input;
        p_BankAccNo.Value = objClsCons.BankAccNo;

        SqlParameter p_SupplierID = command.Parameters.Add("SupplierID", DBNull.Value);
        p_SupplierID.Direction = ParameterDirection.Input;
        p_SupplierID.IsNullable = true;
        //if (objClsCons.SupplierID != "")
        p_SupplierID.Value = "1";// objClsCons.SupplierID;

        SqlParameter p_PassportNo = command.Parameters.Add("PassportNo", SqlDbType.VarChar);
        p_PassportNo.Direction = ParameterDirection.Input;
        p_PassportNo.Value = objClsCons.PassportNo;

        SqlParameter p_BloodGroup = command.Parameters.Add("BloodGroup", SqlDbType.VarChar);
        p_BloodGroup.Direction = ParameterDirection.Input;
        p_BloodGroup.Value = objClsCons.BloodGroup;

        SqlParameter p_DateOfBirth = command.Parameters.Add("DateOfBirth", DBNull.Value);
        p_DateOfBirth.Direction = ParameterDirection.Input;
        p_DateOfBirth.IsNullable = true;
        if (objClsCons.DateOfBirth != "")
            p_DateOfBirth.Value = objClsCons.DateOfBirth;

        SqlParameter p_FatherName = command.Parameters.Add("FatherName", SqlDbType.VarChar);
        p_FatherName.Direction = ParameterDirection.Input;
        p_FatherName.Value = objClsCons.FatherName;

        SqlParameter p_MotherName = command.Parameters.Add("MotherName", SqlDbType.VarChar);
        p_MotherName.Direction = ParameterDirection.Input;
        p_MotherName.Value = objClsCons.MotherName;

        SqlParameter p_PresentAddress = command.Parameters.Add("PresentAddress", SqlDbType.VarChar);
        p_PresentAddress.Direction = ParameterDirection.Input;
        p_PresentAddress.Value = objClsCons.PresentAddress;

        SqlParameter p_PermanentAddress = command.Parameters.Add("PermanentAddress", SqlDbType.VarChar);
        p_PermanentAddress.Direction = ParameterDirection.Input;
        p_PermanentAddress.Value = objClsCons.PermanentAddress;

        SqlParameter p_ContactNo = command.Parameters.Add("ContactNo", SqlDbType.VarChar);
        p_ContactNo.Direction = ParameterDirection.Input;
        p_ContactNo.Value = objClsCons.ContactNo;

        SqlParameter p_Email = command.Parameters.Add("Email", SqlDbType.VarChar);
        p_Email.Direction = ParameterDirection.Input;
        p_Email.Value = objClsCons.Email;

        SqlParameter p_DegreeId = command.Parameters.Add("DegreeId", DBNull.Value );
        p_DegreeId.Direction = ParameterDirection.Input;
        p_DegreeId.IsNullable = true;
        if (objClsCons.DegreeId != "")
            p_DegreeId.Value = objClsCons.DegreeId;

        SqlParameter p_ProfDegreeId = command.Parameters.Add("ProfDegreeId", DBNull.Value);
        p_ProfDegreeId.Direction = ParameterDirection.Input;
        p_ProfDegreeId.IsNullable = true;
        if (objClsCons.ProfDegreeId != "")
        p_ProfDegreeId.Value = objClsCons.ProfDegreeId;

        SqlParameter p_Remarks = command.Parameters.Add("Remarks", SqlDbType.VarChar);
        p_Remarks.Direction = ParameterDirection.Input;
        p_Remarks.Value = objClsCons.Remarks;

        SqlParameter p_ExpertiseArea = command.Parameters.Add("ExpertiseArea", SqlDbType.VarChar);
        p_ExpertiseArea.Direction = ParameterDirection.Input;
        p_ExpertiseArea.Value = objClsCons.ExpertiseArea;

        SqlParameter p_PayMethodID = command.Parameters.Add("PayMethodID", DBNull.Value);
        p_PayMethodID.Direction = ParameterDirection.Input;
        p_PayMethodID.IsNullable = true;
        if (objClsCons.PayMethodID != "")
        p_PayMethodID.Value = objClsCons.PayMethodID;

        SqlParameter p_NoOfDays = command.Parameters.Add("NoOfDays", DBNull.Value);//
        p_NoOfDays.Direction = ParameterDirection.Input;
        p_NoOfDays.IsNullable = true;
        if (objClsCons.NoOfDays != "")
            p_NoOfDays.Value = objClsCons.NoOfDays;

        SqlParameter p_TotalDuration = command.Parameters.Add("TotalDuration", DBNull.Value);//
        p_TotalDuration.Direction = ParameterDirection.Input;
        p_TotalDuration.IsNullable = true;
        if (objClsCons.TotalDuration != "")
        p_TotalDuration.Value = objClsCons.TotalDuration;

        SqlParameter p_Rate = command.Parameters.Add("Rate", DBNull.Value);//DBNull.Value
        p_Rate.Direction = ParameterDirection.Input;
        p_Rate.IsNullable = true;
        if (objClsCons.Rate != "")
            p_Rate.Value = objClsCons.Rate;

        SqlParameter p_AssignmentValue = command.Parameters.Add("AssignmentValue", DBNull.Value);//
        p_AssignmentValue.Direction = ParameterDirection.Input;
        p_AssignmentValue.IsNullable = true;
        if (objClsCons.AssignmentValue != "")
            p_AssignmentValue.Value = objClsCons.AssignmentValue;

        SqlParameter p_OtherCost = command.Parameters.Add("OtherCost", DBNull.Value);//
        p_OtherCost.Direction = ParameterDirection.Input;
        p_OtherCost.IsNullable = true;
        if (objClsCons.OtherCost != "")
            p_OtherCost.Value = objClsCons.OtherCost;

        SqlParameter p_ReimbursableCostNote = command.Parameters.Add("ReimbursableCostNote", SqlDbType.VarChar);
        p_ReimbursableCostNote.Direction = ParameterDirection.Input;
        p_ReimbursableCostNote.Value = objClsCons.ReimbursableCostNote;

        SqlParameter p_VATTaxRate = command.Parameters.Add("VATTaxRate", DBNull.Value);//
        p_VATTaxRate.Direction = ParameterDirection.Input;
        p_VATTaxRate.IsNullable = true;
        if (objClsCons.VATTaxRate != "")
            p_VATTaxRate.Value = objClsCons.VATTaxRate;

        SqlParameter p_NoOfInstallment = command.Parameters.Add("NoOfInstallment", DBNull.Value);
        p_NoOfInstallment.Direction = ParameterDirection.Input;
        p_NoOfInstallment.IsNullable = true;
        if (objClsCons.NoOfInstallment != "")
            p_NoOfInstallment.Value = objClsCons.NoOfInstallment;

        SqlParameter p_Currency = command.Parameters.Add("CurncId", DBNull.Value);
        p_Currency.Direction = ParameterDirection.Input;
        p_Currency.IsNullable = true;
        if (objClsCons.Currency != "99999")
            p_Currency.Value = objClsCons.Currency;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = objClsCons.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = objClsCons.InsertedDate;

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

    // Insert or Update Consultant Payemnt Info 
    public void InsertConsultantPayment(string strPayId, string strConsId, string strPayDate, string strPayAmount, string strOtherCost,
        string strInstallmentNo,string strRatio,string strIsDeleted, string strIns,string strInsDate,string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_ConsPayment");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_PaymentID = command.Parameters.Add("PaymentID", SqlDbType.BigInt);
        p_PaymentID.Direction = ParameterDirection.Input;
        p_PaymentID.Value = strPayId;

        SqlParameter p_ConsId = command.Parameters.Add("ConsId", SqlDbType.VarChar);
        p_ConsId.Direction = ParameterDirection.Input;
        p_ConsId.Value = strConsId;

        SqlParameter p_PayDate = command.Parameters.Add("PayDate", SqlDbType.DateTime);
        p_PayDate.Direction = ParameterDirection.Input;
        p_PayDate.Value = strPayDate;

        SqlParameter p_PayAmount = command.Parameters.Add("PayAmount", SqlDbType.Decimal);
        p_PayAmount.Direction = ParameterDirection.Input;
        p_PayAmount.Value = strPayAmount;

        SqlParameter p_OtherCost = command.Parameters.Add("OtherCost", DBNull.Value);
        p_OtherCost.Direction = ParameterDirection.Input;
        p_OtherCost.IsNullable = true;
        if (strOtherCost != "")
        p_OtherCost.Value = strOtherCost;

        SqlParameter p_InstallmentNo = command.Parameters.Add("InstallmentNo", DBNull.Value);
        p_InstallmentNo.Direction = ParameterDirection.Input;
        p_InstallmentNo.IsNullable = true;
        if (strInstallmentNo != "")
        p_InstallmentNo.Value = strInstallmentNo;

        SqlParameter p_Ratio = command.Parameters.Add("Ratio", DBNull.Value);
        p_Ratio.Direction = ParameterDirection.Input;
        p_Ratio.IsNullable = true;
        if (strRatio != "")
        p_Ratio.Value = strRatio;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = strIsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strIns;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

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

    // Insert or Update Consultant Payemnt Info 
    public void InsertConsultantPaymentCharge(string strPayId, string strConsId, string strSalarySourceId, string strProject,
        string strRatio, string strIsDeleted, string strIns, string strInsDate, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_ConsPaymentCharge");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_PaymentID = command.Parameters.Add("PaymentID", SqlDbType.BigInt);
        p_PaymentID.Direction = ParameterDirection.Input;
        p_PaymentID.Value = strPayId;

        SqlParameter p_ConsId = command.Parameters.Add("ConsId", SqlDbType.VarChar);
        p_ConsId.Direction = ParameterDirection.Input;
        p_ConsId.Value = strConsId;

        SqlParameter p_SalarySourceId = command.Parameters.Add("SalarySourceId", DBNull.Value);
        p_SalarySourceId.Direction = ParameterDirection.Input;
        p_SalarySourceId.IsNullable = true;
        if (strSalarySourceId != "99999")
        p_SalarySourceId.Value = Convert.ToInt32(strSalarySourceId);

        SqlParameter p_ProjectCode = command.Parameters.Add("ProjectCode", DBNull.Value);
        p_ProjectCode.Direction = ParameterDirection.Input;
        p_ProjectCode.IsNullable = true;
        if (strProject != "")
        p_ProjectCode.Value = strProject;

        SqlParameter p_Ratio = command.Parameters.Add("Ratio", SqlDbType.Decimal);
        p_Ratio.Direction = ParameterDirection.Input;
        p_Ratio.Value = strRatio;

        //SqlParameter p_Ratio = command.Parameters.Add("Ratio", DBNull.Value);
        //p_Ratio.Direction = ParameterDirection.Input;
        //p_Ratio.IsNullable = true;
        //if (strRatio != "-1")
        //p_Ratio.Value = strRatio;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = strIsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strIns;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

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

    // Insert or Update Consultant Payemnt Info 
    public void InsertConsultantAssessment(string strAssesId, string strConsId, string strIsHire, string strNotHireRemarks, string strContribution,
        string strIsAddAssignment, string strComments, string strIsProFinalPay, GridView grNature,GridView grRating,
        string strIns, string strInsDate, string IsUpdate, string IsDelete)
    {

        SqlCommand[] command = new SqlCommand[grNature.Rows.Count + grRating.Rows.Count + 3];

            command[0] =new SqlCommand("proc_Insert_ConsAssessment");
            command[0].CommandType = CommandType.StoredProcedure;

            SqlParameter p_AssesId = command[0].Parameters.Add("AssessId", SqlDbType.BigInt);
        p_AssesId.Direction = ParameterDirection.Input;
        p_AssesId.Value = strAssesId;

        SqlParameter p_ConsId = command[0].Parameters.Add("ConsId", SqlDbType.VarChar);
        p_ConsId.Direction = ParameterDirection.Input;
        p_ConsId.Value = strConsId;

        SqlParameter p_IsHire = command[0].Parameters.Add("IsHire", SqlDbType.Char);
        p_IsHire.Direction = ParameterDirection.Input;
        p_IsHire.Value = strIsHire;

        SqlParameter p_NotHireRemarks = command[0].Parameters.Add("NotHireRemarks", SqlDbType.VarChar);
        p_NotHireRemarks.Direction = ParameterDirection.Input;
        p_NotHireRemarks.Value = strNotHireRemarks;

        SqlParameter p_Contribution = command[0].Parameters.Add("Contribution", SqlDbType.VarChar);
        p_Contribution.Direction = ParameterDirection.Input;
        p_Contribution.Value = strContribution;

        SqlParameter p_IsAddAssignment = command[0].Parameters.Add("IsAddAssignment", SqlDbType.Char);
        p_IsAddAssignment.Direction = ParameterDirection.Input;
        p_IsAddAssignment.Value = strIsAddAssignment;

        SqlParameter p_Comments = command[0].Parameters.Add("Comments", SqlDbType.VarChar);
        p_Comments.Direction = ParameterDirection.Input;
        p_Comments.Value = strComments;

        SqlParameter p_IsProFinalPay = command[0].Parameters.Add("IsProFinalPay", SqlDbType.Char);
        p_IsProFinalPay.Direction = ParameterDirection.Input;
        p_IsProFinalPay.Value = strIsProFinalPay;

        SqlParameter p_InsertedBy = command[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strIns;

        SqlParameter p_InsertedDate = command[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        SqlParameter p_IsUpdate = command[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command[0].Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        command[1] = new SqlCommand("proc_Delete_ConsAssessNatureDet");
        command[1].CommandType = CommandType.StoredProcedure;

        p_AssesId = command[1].Parameters.Add("AssessId", SqlDbType.BigInt);
        p_AssesId.Direction = ParameterDirection.Input;
        p_AssesId.Value = strAssesId;

        int i = 2;
        foreach (GridViewRow gNatureRow in grNature.Rows)
        {
            command[i] = new SqlCommand("proc_Insert_ConsAssessNatureDet");
            command[i].CommandType = CommandType.StoredProcedure;

            p_AssesId = command[i].Parameters.Add("AssessId", SqlDbType.BigInt);
            p_AssesId.Direction = ParameterDirection.Input;
            p_AssesId.Value = strAssesId;

            SqlParameter p_AssessNatureId = command[i].Parameters.Add("AssessNatureId", SqlDbType.BigInt);
            p_AssessNatureId.Direction = ParameterDirection.Input;
            p_AssessNatureId.Value = grNature.DataKeys[gNatureRow.RowIndex].Values[0].ToString();

            p_InsertedBy = command[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = strIns;

            p_InsertedDate = command[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate.Direction = ParameterDirection.Input;
            p_InsertedDate.Value = strInsDate;

            i++;
        }

        command[i] = new SqlCommand("proc_Delete_ConsAssessRatingDet");
        command[i].CommandType = CommandType.StoredProcedure;

        p_AssesId = command[i].Parameters.Add("AssessId", SqlDbType.BigInt);
        p_AssesId.Direction = ParameterDirection.Input;
        p_AssesId.Value = strAssesId;

        i++;
        foreach (GridViewRow gRatingRow in grRating.Rows)
        {
            command[i] = new SqlCommand("proc_Insert_ConsAssessRatingDet");
            command[i].CommandType = CommandType.StoredProcedure;

            p_AssesId = command[i].Parameters.Add("AssessId", SqlDbType.BigInt);
            p_AssesId.Direction = ParameterDirection.Input;
            p_AssesId.Value = strAssesId;

            SqlParameter p_AssessRatingId = command[i].Parameters.Add("RatingId", SqlDbType.BigInt);
            p_AssessRatingId.Direction = ParameterDirection.Input;
            p_AssessRatingId.Value = grRating.DataKeys[gRatingRow.RowIndex].Values[0].ToString();

            SqlParameter p_Rating = command[i].Parameters.Add("Rating", SqlDbType.BigInt);
            p_Rating.Direction = ParameterDirection.Input;
            p_Rating.Value = gRatingRow.Cells[2].Text;   

            p_InsertedBy = command[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = strIns;

            p_InsertedDate = command[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate.Direction = ParameterDirection.Input;
            p_InsertedDate.Value = strInsDate;

            i++;
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

    #endregion

    #region Select Consultant Configuration Info
    public DataTable SelectWorkArea(Int32 WorkAreaID)
    {
        SqlCommand command = new SqlCommand("proc_Select_ConsWorkAreaList");

        SqlParameter p_WorkAreaID = command.Parameters.Add("WorkAreaID", SqlDbType.BigInt);
        p_WorkAreaID.Direction = ParameterDirection.Input;
        p_WorkAreaID.Value = WorkAreaID;

        objDC.CreateDSFromProc(command, "tblWorkArea");
        return objDC.ds.Tables["tblWorkArea"];
    }

    public DataTable SelectPayMethod(Int32 PayMethodID)
    {
        SqlCommand command = new SqlCommand("proc_Select_ConsPayMethodList");

        SqlParameter p_PayMethodID = command.Parameters.Add("PayMethodID", SqlDbType.BigInt);
        p_PayMethodID.Direction = ParameterDirection.Input;
        p_PayMethodID.Value = PayMethodID;

        objDC.CreateDSFromProc(command, "tblPayMethod");
        return objDC.ds.Tables["tblPayMethod"];
    }

    public DataTable SelectConsultantType(Int32 TypeId)
    {
        SqlCommand command = new SqlCommand("proc_Select_ConsTypeList");

        SqlParameter p_TypeId = command.Parameters.Add("TypeId", SqlDbType.BigInt);
        p_TypeId.Direction = ParameterDirection.Input;
        p_TypeId.Value = TypeId;

        objDC.CreateDSFromProc(command, "tblConsultantTypeList");
        return objDC.ds.Tables["tblConsultantTypeList"];
    }

    public DataTable SelectConsHiringMethod(Int32 TypeId)
    {
        SqlCommand command = new SqlCommand("proc_Select_ConsHiringMethodList");

        SqlParameter p_TypeId = command.Parameters.Add("MethodId", SqlDbType.BigInt);
        p_TypeId.Direction = ParameterDirection.Input;
        p_TypeId.Value = TypeId;

        objDC.CreateDSFromProc(command, "tblConsHiringMethodList");
        return objDC.ds.Tables["tblConsHiringMethodList"];
    }

    public DataTable SelectConsAssesmentRateList(Int32 AssessmentRateId)
    {
        SqlCommand command = new SqlCommand("proc_Select_ConsAssessRateList");

        SqlParameter p_AssesmentRateID = command.Parameters.Add("AssessmentRateId", SqlDbType.BigInt);
        p_AssesmentRateID.Direction = ParameterDirection.Input;
        p_AssesmentRateID.Value = AssessmentRateId;

        objDC.CreateDSFromProc(command, "tblPayMethod");
        return objDC.ds.Tables["tblPayMethod"];
    }

    public DataTable SelectConsAssessNatureList(Int32 AssessNatureId)
    {
        SqlCommand command = new SqlCommand("proc_Select_ConsAssessNatureList");

        SqlParameter p_AssesmentNatureID = command.Parameters.Add("AssessNatureId", SqlDbType.BigInt);
        p_AssesmentNatureID.Direction = ParameterDirection.Input;
        p_AssesmentNatureID.Value = AssessNatureId;

        objDC.CreateDSFromProc(command, "tblConsAssesmentNature");
        return objDC.ds.Tables["tblConsAssesmentNature"];
    }

    public DataTable SelectSupplier(Int32 SupplierId)
    {
        SqlCommand command = new SqlCommand("proc_Select_SupplierList");
        SqlParameter p_SupplierId = command.Parameters.Add("SupplierId", SqlDbType.BigInt);
        p_SupplierId.Direction = ParameterDirection.Input;
        p_SupplierId.Value = SupplierId;
        objDC.CreateDSFromProc(command, "tblSupplier");
        return objDC.ds.Tables["tblSupplier"];
    }

    #endregion

     #region Consultant Transaction Info
     public DataTable SelectConsultantInfo(string  strId)
    {
        if (objDC.ds.Tables["tblConsultantInfo"] != null)
        {
            objDC.ds.Tables["tblConsultantInfo"].Clear();
            objDC.ds.Tables["tblConsultantInfo"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_Select_ConsultantInfo");

        SqlParameter p_Id = command.Parameters.Add("ConsId", SqlDbType.VarChar);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = strId;

        objDC.CreateDSFromProc(command, "tblConsultantInfo");
        return objDC.ds.Tables["tblConsultantInfo"];
    }

     public DataTable SelectConsultantPaymentCharge(string strConsId)
    {
        
        SqlCommand command = new SqlCommand("proc_Select_ConsPaymentCharge");

        SqlParameter p_Id = command.Parameters.Add("ConsId", SqlDbType.VarChar);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = strConsId;

        objDC.CreateDSFromProc(command, "tblConsultantPaymentCharge");
        return objDC.ds.Tables["tblConsultantPaymentCharge"];
    }

    public DataTable SelectConsultantPayment(string strConsId)
    {
        SqlCommand command = new SqlCommand("proc_Select_ConsPayment");

        SqlParameter p_Id = command.Parameters.Add("ConsId", SqlDbType.VarChar);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = strConsId;

        objDC.CreateDSFromProc(command, "tblConsultantPayment");
        return objDC.ds.Tables["tblConsultantPayment"];
    }

    public DataTable SelectConsultantAssessment(string strConsId)
    {
        SqlCommand command = new SqlCommand("proc_Select_ConsultantAssessment");

        SqlParameter p_Id = command.Parameters.Add("ConsId", SqlDbType.VarChar);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = strConsId;

        objDC.CreateDSFromProc(command, "tblConsultantAssessment");
        return objDC.ds.Tables["tblConsultantAssessment"];
    }

    public DataTable SelectConsAssessNatureDet(string AssessId)
    {
        SqlCommand command = new SqlCommand("proc_Select_ConsAssessNatureDet");

        SqlParameter p_AssessId = command.Parameters.Add("AssessId", SqlDbType.BigInt);
        p_AssessId.Direction = ParameterDirection.Input;
        p_AssessId.Value = AssessId;

        objDC.CreateDSFromProc(command, "tblConsAssessNatureDet");
        return objDC.ds.Tables["tblConsAssessNatureDet"];
    }
    public DataTable SelectConsAssessRatingDet(string AssessId)
    {
        SqlCommand command = new SqlCommand("proc_Select_ConsAssessRatingDet");

        SqlParameter p_AssessId = command.Parameters.Add("AssessId", SqlDbType.BigInt);
        p_AssessId.Direction = ParameterDirection.Input;
        p_AssessId.Value = AssessId;

        objDC.CreateDSFromProc(command, "tblConsAssessRatingDet");
        return objDC.ds.Tables["tblConsAssessRatingDet"];
    }
    #endregion

    #region Consultant DEA Info
    public DataTable SelectDEA(Int32 DeptID)
    {
        SqlCommand command = new SqlCommand("proc_Select_DEA");
        SqlParameter p_DeptID = command.Parameters.Add("DEAId", SqlDbType.BigInt);
        p_DeptID.Direction = ParameterDirection.Input;
        p_DeptID.Value = DeptID;

        objDC.CreateDSFromProc(command, "DEAList");
        return objDC.ds.Tables["DEAList"];
    }
    #endregion

    //Insert Emergency Contact
    public void InsertConsEducation(clsConsEducation objCons, string strIsUpdate, string strIsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_ConsEducation");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_ContactId = command.Parameters.Add("EduId", SqlDbType.BigInt);
        p_ContactId.Direction = ParameterDirection.Input;
        p_ContactId.Value = objCons.EduId;

        SqlParameter p_EmpId = command.Parameters.Add("ConsId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = objCons.ConsId;

        SqlParameter p_DegreeId = command.Parameters.Add("DegreeId", DBNull.Value);
        p_DegreeId.Direction = ParameterDirection.Input;
        p_DegreeId.IsNullable = true;
        if (objCons.DegreeId != "")
            p_DegreeId.Value = objCons.DegreeId;

        SqlParameter p_InstituteId = command.Parameters.Add("InstituteId", DBNull.Value);
        p_InstituteId.Direction = ParameterDirection.Input;
        p_InstituteId.IsNullable = true;
        if (objCons.InstituteId != "")
            p_InstituteId.Value = objCons.InstituteId;

        SqlParameter p_SubjectId = command.Parameters.Add("SubjectId", DBNull.Value);
        p_SubjectId.Direction = ParameterDirection.Input;
        p_SubjectId.IsNullable = true;
        if (objCons.SubjectId != "")
            p_SubjectId.Value = objCons.SubjectId;

        SqlParameter p_ResultId = command.Parameters.Add("ResultId", DBNull.Value);
        p_ResultId.Direction = ParameterDirection.Input;
        p_ResultId.IsNullable = true;
        if (objCons.ResultId != "")
            p_ResultId.Value = objCons.ResultId;

        SqlParameter p_PassedYear = command.Parameters.Add("PassedYear", DBNull.Value);
        p_PassedYear.Direction = ParameterDirection.Input;
        p_PassedYear.IsNullable = true;
        if (objCons.PassedYear != "")
            p_PassedYear.Value = objCons.PassedYear;

        SqlParameter p_DegreeTitle = command.Parameters.Add("DegreeTitle", DBNull.Value);
        p_DegreeTitle.Direction = ParameterDirection.Input;
        p_DegreeTitle.IsNullable = true;
        if (objCons.DegreeTitle != "")
            p_DegreeTitle.Value = objCons.DegreeTitle;

        SqlParameter p_IsMaxDegree = command.Parameters.Add("IsMaxDegree", SqlDbType.Char);
        p_IsMaxDegree.Direction = ParameterDirection.Input;
        p_IsMaxDegree.Value = objCons.IsMaxDegree;


        SqlParameter p_Marks = command.Parameters.Add("Marks", DBNull.Value);
        p_Marks.Direction = ParameterDirection.Input;
        p_Marks.IsNullable = true;
        if (objCons.Marks != "")
            p_Marks.Value = objCons.Marks;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = objCons.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = objCons.InsertedDate;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = strIsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = strIsDelete;

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

    public DataTable SelectConsEducation(long EduId, string ConsId)
    {
        SqlCommand command = new SqlCommand("proc_Select_ConsEducation");
        SqlParameter p_EduId = command.Parameters.Add("EduId", SqlDbType.BigInt);
        p_EduId.Direction = ParameterDirection.Input;
        p_EduId.Value = EduId;

        SqlParameter p_ConsId = command.Parameters.Add("ConsId", SqlDbType.VarChar);
        p_ConsId.Direction = ParameterDirection.Input;
        p_ConsId.Value = ConsId;

        objDC.CreateDSFromProc(command, "tblConsEducation");
        return objDC.ds.Tables["tblConsEducation"];
    }
    public DataTable SelectConsAgrEndList(string ConsId)
    {
        string strSQL = "Select LogID,ConsId,EndDate from ConsAgrEndLog where ConsId='" + ConsId + "'";
        return objDC.CreateDT(strSQL, "tblConsAgrEndDateLog");

    }

    public void InsertConsAgrEndLog(string strLogID, string strConsID, string strEndDate, string strUser, string strIsUpdate, string strIsDelete)
    {

        SqlCommand command = new SqlCommand("proc_Insert_ConsEndDateLog");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LogID = command.Parameters.Add("LogID", SqlDbType.Decimal);
        p_LogID.Direction = ParameterDirection.Input;
        p_LogID.Value = Convert.ToDecimal(strLogID);

        SqlParameter p_EmpId = command.Parameters.Add("ConsId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strConsID;

        SqlParameter p_EndDate = command.Parameters.Add("EndDate", SqlDbType.DateTime);
        p_EndDate.Direction = ParameterDirection.Input;
        p_EndDate.Value = Common.ReturnDate(strEndDate);

        SqlParameter p_UpdatedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_UpdatedBy.Direction = ParameterDirection.Input;
        p_UpdatedBy.Value = strUser;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = strIsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = strIsDelete;

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

}