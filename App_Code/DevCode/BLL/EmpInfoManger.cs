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
/// Summary description for EmpInfoManager
/// </summary>
public class EmpInfoManager
{
    DBConnector objDC = new DBConnector();
    dsEmpList objDS = new dsEmpList();

    #region Insert Update Delete From Tables By Store procedure

    public void InsertEmpInfo(clsEmpInfo clEmp, string IsUpdate, string IsDelete, byte[] imgByte, byte[] imgSignByte)
    {
        SqlCommand command = new SqlCommand();
        command = new SqlCommand("proc_Insert_EmpInfo");

        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = clEmp.EmpId;

        SqlParameter p_Title = command.Parameters.Add("Title", SqlDbType.VarChar);
        p_Title.Direction = ParameterDirection.Input;
        p_Title.Value = clEmp.Title;

        SqlParameter p_EmpFname = command.Parameters.Add("FirstName", SqlDbType.VarChar);
        p_EmpFname.Direction = ParameterDirection.Input;
        p_EmpFname.Value = clEmp.EmpFname;

        SqlParameter p_EmpMname = command.Parameters.Add("MiddleName", SqlDbType.VarChar);
        p_EmpMname.Direction = ParameterDirection.Input;
        p_EmpMname.Value = clEmp.EmpMname;

        SqlParameter p_EmpLname = command.Parameters.Add("LastName", SqlDbType.VarChar);
        p_EmpLname.Direction = ParameterDirection.Input;
        p_EmpLname.Value = clEmp.EmpLname;

        SqlParameter p_FullName = command.Parameters.Add("FullName", SqlDbType.VarChar);
        p_FullName.Direction = ParameterDirection.Input;
        p_FullName.Value = clEmp.FullName;

        SqlParameter p_Gender = command.Parameters.Add("Gender", SqlDbType.VarChar);
        p_Gender.Direction = ParameterDirection.Input;
        p_Gender.Value = clEmp.Gender;

        SqlParameter p_ReligionId = command.Parameters.Add("ReligionId", DBNull.Value);
        p_ReligionId.Direction = ParameterDirection.Input;
        p_ReligionId.IsNullable = true;
        if (clEmp.Religion != "99999")
            p_ReligionId.Value = clEmp.Religion;

        SqlParameter p_BloodGroupId = command.Parameters.Add("BloodGroupId", DBNull.Value);
        p_BloodGroupId.Direction = ParameterDirection.Input;
        p_BloodGroupId.IsNullable = true;
        if (clEmp.BloodGroup != "99999")
            p_BloodGroupId.Value = clEmp.BloodGroup;

        SqlParameter p_DOB = command.Parameters.Add("DOB", DBNull.Value);
        p_DOB.Direction = ParameterDirection.Input;
        p_DOB.IsNullable = true;
        if (string.IsNullOrEmpty(clEmp.DOB) == false)
            p_DOB.Value = Common.ReturnDate(clEmp.DOB);

        SqlParameter p_FathersNm = command.Parameters.Add("FatherName", SqlDbType.VarChar);
        p_FathersNm.Direction = ParameterDirection.Input;
        p_FathersNm.Value = clEmp.FathersNm;

        SqlParameter p_MothersNm = command.Parameters.Add("MotherName", SqlDbType.VarChar);
        p_MothersNm.Direction = ParameterDirection.Input;
        p_MothersNm.Value = clEmp.MothersNm;

        SqlParameter p_PreAddress = command.Parameters.Add("PreAddress", SqlDbType.VarChar);
        p_PreAddress.Direction = ParameterDirection.Input;
        p_PreAddress.Value = clEmp.PreAddress;

        SqlParameter p_PrePhone = command.Parameters.Add("PrePhone", SqlDbType.VarChar);
        p_PrePhone.Direction = ParameterDirection.Input;
        p_PrePhone.Value = clEmp.PrePhone;

        SqlParameter p_PreFax = command.Parameters.Add("PreFax", SqlDbType.VarChar);
        p_PreFax.Direction = ParameterDirection.Input;
        p_PreFax.Value = clEmp.PreFax;

        SqlParameter p_PerAddress = command.Parameters.Add("PerAddress", SqlDbType.VarChar);
        p_PerAddress.Direction = ParameterDirection.Input;
        p_PerAddress.Value = clEmp.PerAddress;

        SqlParameter p_PerPhone = command.Parameters.Add("PerPhone", SqlDbType.VarChar);
        p_PerPhone.Direction = ParameterDirection.Input;
        p_PerPhone.Value = clEmp.PerPhone;

        SqlParameter p_PerFax = command.Parameters.Add("PerFax", SqlDbType.VarChar);
        p_PerFax.Direction = ParameterDirection.Input;
        p_PerFax.Value = clEmp.PerFax;

        SqlParameter p_DistrictID = command.Parameters.Add("PerDistrictID", DBNull.Value);
        p_DistrictID.Direction = ParameterDirection.Input;
        p_DistrictID.IsNullable = true;
        if (clEmp.PerDistrictID != "99999")
            p_DistrictID.Value = clEmp.PerDistrictID;

        SqlParameter p_PerCountryID = command.Parameters.Add("PerCountryID", DBNull.Value);
        p_PerCountryID.Direction = ParameterDirection.Input;
        p_PerCountryID.IsNullable = true;
        if (clEmp.PerCountryID != "99999")
            p_PerCountryID.Value = clEmp.PerCountryID;

        SqlParameter p_OfficeExt = command.Parameters.Add("OfficeExt", SqlDbType.VarChar);
        p_OfficeExt.Direction = ParameterDirection.Input;
        p_OfficeExt.Value = clEmp.OfficeExt;

        SqlParameter p_OfficeEmail = command.Parameters.Add("OfficeEmail", SqlDbType.VarChar);
        p_OfficeEmail.Direction = ParameterDirection.Input;
        p_OfficeEmail.Value = clEmp.OfficeEmail;

        SqlParameter p_PersonalEmail = command.Parameters.Add("PersonalEmail", SqlDbType.VarChar);
        p_PersonalEmail.Direction = ParameterDirection.Input;
        p_PersonalEmail.Value = clEmp.PersonalEmail;

        SqlParameter p_MailingAddrs = command.Parameters.Add("MailingAddrs", SqlDbType.VarChar);
        p_MailingAddrs.Direction = ParameterDirection.Input;
        p_MailingAddrs.Value = "";

        SqlParameter p_MaritalStatus = command.Parameters.Add("MaritalStatus", SqlDbType.VarChar);
        p_MaritalStatus.Direction = ParameterDirection.Input;
        p_MaritalStatus.Value = clEmp.MaritalStatus;

        SqlParameter p_MarriageDate = command.Parameters.Add("MarriageDate", DBNull.Value);
        p_MarriageDate.Direction = ParameterDirection.Input;
        p_MarriageDate.IsNullable = true;
        if (string.IsNullOrEmpty(clEmp.MarriageDate) == false)
            p_MarriageDate.Value = Common.ReturnDate(clEmp.MarriageDate);

        SqlParameter p_HighestEduId = command.Parameters.Add("EduId", DBNull.Value);
        p_HighestEduId.Direction = ParameterDirection.Input;
        p_HighestEduId.IsNullable = true;
        if (clEmp.HighestEdu != "99999")
            p_HighestEduId.Value = clEmp.HighestEdu;

        SqlParameter p_ProffDegreeId = command.Parameters.Add("ProffDegreeId", DBNull.Value);
        p_ProffDegreeId.Direction = ParameterDirection.Input;
        p_ProffDegreeId.IsNullable = true;
        if (clEmp.ProffDegree != "99999")
            p_ProffDegreeId.Value = clEmp.ProffDegree;

        SqlParameter p_SpecialSkillId = command.Parameters.Add("SpecialSkillId", DBNull.Value);
        p_SpecialSkillId.Direction = ParameterDirection.Input;
        p_SpecialSkillId.IsNullable = true;
        if (clEmp.SpecialSkill != "99999")
            p_SpecialSkillId.Value = clEmp.SpecialSkill;

        SqlParameter p_IsSpectacled = command.Parameters.Add("IsSpectacled", SqlDbType.Char);
        p_IsSpectacled.Direction = ParameterDirection.Input;
        p_IsSpectacled.Value = clEmp.IsSpectacled;

        SqlParameter p_DrivingLicense = command.Parameters.Add("DrivingLicense", SqlDbType.VarChar);
        p_DrivingLicense.Direction = ParameterDirection.Input;
        p_DrivingLicense.Value = clEmp.LicenseNo;

        SqlParameter p_LicenseRenewDate = command.Parameters.Add("LicenseRenewDate", DBNull.Value);
        p_LicenseRenewDate.Direction = ParameterDirection.Input;
        p_LicenseRenewDate.IsNullable = true;
        if (string.IsNullOrEmpty(clEmp.LicenseExpDate) == false)
            p_LicenseRenewDate.Value = Common.ReturnDate(clEmp.LicenseExpDate);

        SqlParameter p_IsRelativeInSC = command.Parameters.Add("IsRelativeInSC", SqlDbType.Char);
        p_IsRelativeInSC.Direction = ParameterDirection.Input;
        p_IsRelativeInSC.Value = clEmp.IsRelativeInSC;

        SqlParameter p_RelationId = command.Parameters.Add("RelationId", DBNull.Value);
        p_RelationId.Direction = ParameterDirection.Input;
        p_RelationId.IsNullable = true;
        if (clEmp.Relation != "99999")
            p_RelationId.Value = clEmp.Relation;

        SqlParameter p_TINNo = command.Parameters.Add("TINNo", SqlDbType.VarChar);
        p_TINNo.Direction = ParameterDirection.Input;
        p_TINNo.Value = clEmp.TINNo;

        SqlParameter p_PassportNo = command.Parameters.Add("PassportNo", SqlDbType.VarChar);
        p_PassportNo.Direction = ParameterDirection.Input;
        p_PassportNo.Value = clEmp.PassportNo;

        SqlParameter p_PassExpDate = command.Parameters.Add("PassExpDate", DBNull.Value);
        p_PassExpDate.Direction = ParameterDirection.Input;
        p_PassExpDate.IsNullable = true;
        if (string.IsNullOrEmpty(clEmp.PassExpDate) == false)
            p_PassExpDate.Value = Common.ReturnDate(clEmp.PassExpDate);

        SqlParameter p_Circle = command.Parameters.Add("Circle", SqlDbType.VarChar);
        p_Circle.Direction = ParameterDirection.Input;
        p_Circle.Value = clEmp.Circle;

        SqlParameter p_Zone = command.Parameters.Add("Zone", SqlDbType.VarChar);
        p_Zone.Direction = ParameterDirection.Input;
        p_Zone.Value = clEmp.Zone;

        SqlParameter p_PassportIssueOffice = command.Parameters.Add("PassportIssueOffice", SqlDbType.VarChar);
        p_PassportIssueOffice.Direction = ParameterDirection.Input;
        p_PassportIssueOffice.Value = clEmp.PassIssOffice;

        SqlParameter p_NationalId = command.Parameters.Add("NationalId", SqlDbType.VarChar);
        p_NationalId.Direction = ParameterDirection.Input;
        p_NationalId.Value = clEmp.NationalId;

        SqlParameter p_Nationality = command.Parameters.Add("Nationality", SqlDbType.VarChar);
        p_Nationality.Direction = ParameterDirection.Input;
        p_Nationality.Value = clEmp.Nationality;

        SqlParameter p_DOBId = command.Parameters.Add("DOBId", SqlDbType.VarChar);
        p_DOBId.Direction = ParameterDirection.Input;
        p_DOBId.Value = clEmp.DOBId;

        SqlParameter p_SkypeId = command.Parameters.Add("SkypeId", SqlDbType.VarChar);
        p_SkypeId.Direction = ParameterDirection.Input;
        p_SkypeId.Value = clEmp.SkypeId;

        SqlParameter p_CellPhone = command.Parameters.Add("CellPhone", SqlDbType.VarChar);
        p_CellPhone.Direction = ParameterDirection.Input;
        p_CellPhone.Value = clEmp.CellPhone;

        SqlParameter p_LandPhone = command.Parameters.Add("LandPhone", SqlDbType.VarChar);
        p_LandPhone.Direction = ParameterDirection.Input;
        p_LandPhone.Value = clEmp.LandPhone;

        SqlParameter p_EmpPicLoc = command.Parameters.Add("EmpPicLoc", SqlDbType.VarChar);
        p_EmpPicLoc.Direction = ParameterDirection.Input;
        p_EmpPicLoc.Value = clEmp.EmpPicLoc;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_IsDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_IsDeleted.Direction = ParameterDirection.Input;
        p_IsDeleted.Value = IsDelete;

        SqlParameter p_EmpImage = command.Parameters.Add("EmpImage", SqlDbType.Image);
        p_EmpImage.Direction = ParameterDirection.Input;
        p_EmpImage.Value = imgByte;

        SqlParameter p_EmpSignImage = command.Parameters.Add("EmpSignImage", SqlDbType.Image);
        p_EmpSignImage.Direction = ParameterDirection.Input;
        p_EmpSignImage.Value = imgSignByte;

        SqlParameter p_RelativeInfo = command.Parameters.Add("RelativeInfo", SqlDbType.VarChar);
        p_RelativeInfo.Direction = ParameterDirection.Input;
        p_RelativeInfo.Value = clEmp.RelativeInfo;

        SqlParameter p_IsMedical = command.Parameters.Add("TNTPosition", SqlDbType.Char);
        p_IsMedical.Direction = ParameterDirection.Input;
        p_IsMedical.Value = clEmp.IsMedical ;

        SqlParameter p_Nature = command.Parameters.Add("Nature", SqlDbType.Char);
        p_Nature.Direction = ParameterDirection.Input;
        p_Nature.Value = clEmp.Nature;

        SqlParameter p_BMDCRegNo = command.Parameters.Add("BMDCRegNo", SqlDbType.VarChar);
        p_BMDCRegNo.Direction = ParameterDirection.Input;
        p_BMDCRegNo.Value = clEmp.BMDCRegNo;

        SqlParameter p_BMDCRegDate = command.Parameters.Add("BMDCRegDate", DBNull.Value);
        p_BMDCRegDate.Direction = ParameterDirection.Input;
        p_BMDCRegDate.IsNullable = true;
        if (string.IsNullOrEmpty(clEmp.BMDCRegDate) == false)
            p_BMDCRegDate.Value = Common.ReturnDate(clEmp.BMDCRegDate);

        SqlParameter p_PreDistrictID = command.Parameters.Add("PreDistrictID", DBNull.Value);
        p_PreDistrictID.Direction = ParameterDirection.Input;
        p_PreDistrictID.IsNullable = true;
        if (clEmp.PreDistrictID != "99999")
            p_PreDistrictID.Value = clEmp.PreDistrictID;

        SqlParameter p_PreCountryID = command.Parameters.Add("PreCountryID", DBNull.Value);
        p_PreCountryID.Direction = ParameterDirection.Input;
        p_PreCountryID.IsNullable = true;
        if (clEmp.PreCountryID != "99999")            
        p_PreCountryID.Value = clEmp.PreCountryID;

        SqlParameter p_PrePostCode = command.Parameters.Add("PrePostCode", SqlDbType.VarChar);
        p_PrePostCode.Direction = ParameterDirection.Input;
        p_PrePostCode.Value = clEmp.PrePostCode;

        SqlParameter p_PerPostCode = command.Parameters.Add("PerPostCode", SqlDbType.VarChar);
        p_PerPostCode.Direction = ParameterDirection.Input;
        p_PerPostCode.Value = clEmp.PerPostCode;

        SqlParameter p_TNTCode = command.Parameters.Add("TNTCode", SqlDbType.VarChar);
        p_TNTCode.Direction = ParameterDirection.Input;
        p_TNTCode.Value = clEmp.TNTCode;

        SqlParameter p_NoofLiveChild = command.Parameters.Add("NoofLiveChild", DBNull.Value);
        p_NoofLiveChild.Direction = ParameterDirection.Input;
        p_NoofLiveChild.IsNullable = true;
        if (string.IsNullOrEmpty(clEmp.NoofLiveChild) == false)
        p_NoofLiveChild.Value = clEmp.NoofLiveChild;

        SqlParameter p_OldEmpId = command.Parameters.Add("OldEmpId", SqlDbType.VarChar);
        p_OldEmpId.Direction = ParameterDirection.Input;
        p_OldEmpId.Value = clEmp.OldEmpId;

        SqlParameter p_SpouseName = command.Parameters.Add("SpouseName", SqlDbType.VarChar);
        p_SpouseName.Direction = ParameterDirection.Input;
        p_SpouseName.Value = clEmp.SpouseName;
       
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

    public void InsertEmpInfoTabHR(clsEmpInfoHr clEmp, string strIsLeaveUpdate, string strIsNew, string strFiscalYrId)
    {
        int i = 0;
        SqlCommand[] cmd1 = new SqlCommand[1];

        //Save Leave Info
        if (strIsLeaveUpdate == "Y")
        {
            EmpLeaveProfileManager objLevProMgr = new EmpLeaveProfileManager();
            cmd1 = objLevProMgr.InsertIntoEmpLevProfile(clEmp.EmpId, clEmp.LeavePakId, clEmp.JoiningDate, clEmp.InsertedBy, clEmp.EmpTypeID);
        }
        
        #region Save HR Info
        SqlCommand[] cmd = new SqlCommand[cmd1.Length + 4];
        cmd[0] = new SqlCommand("proc_Insert_EmpInfo_tabHR");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpId = cmd[0].Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = clEmp.EmpId;

        SqlParameter p_EmpTypeId = cmd[0].Parameters.Add("EmpTypeID", DBNull.Value);
        p_EmpTypeId.Direction = ParameterDirection.Input;
        p_EmpTypeId.IsNullable = true;
        if (clEmp.EmpTypeID != "99999")
            p_EmpTypeId.Value = clEmp.EmpTypeID;

        SqlParameter p_CompanyId = cmd[0].Parameters.Add("DivisionId", DBNull.Value);
        p_CompanyId.Direction = ParameterDirection.Input;
        p_CompanyId.IsNullable = true;
        if (clEmp.CompanyId != "99999")
            p_CompanyId.Value = clEmp.CompanyId;

        SqlParameter p_GradeId = cmd[0].Parameters.Add("GradeId", DBNull.Value);
        p_GradeId.Direction = ParameterDirection.Input;
        p_GradeId.IsNullable = true;
        if (clEmp.GradeId != "99999")
            p_GradeId.Value = clEmp.GradeId;

        SqlParameter p_DesigId = cmd[0].Parameters.Add("DesigId", DBNull.Value);
        p_DesigId.Direction = ParameterDirection.Input;
        p_DesigId.IsNullable = true;
        if (clEmp.DesigId != "99999")
            p_DesigId.Value = clEmp.DesigId;

        SqlParameter p_DeptId = cmd[0].Parameters.Add("DeptId", DBNull.Value);
        p_DeptId.Direction = ParameterDirection.Input;
        p_DeptId.IsNullable = true;
        if (clEmp.DeptId != "99999")
            p_DeptId.Value = clEmp.DeptId;

        SqlParameter p_SubDeptId = cmd[0].Parameters.Add("SubDeptId", DBNull.Value);
        p_SubDeptId.Direction = ParameterDirection.Input;
        p_SubDeptId.IsNullable = true;
        if (clEmp.SubDeptId != "99999")
            p_SubDeptId.Value = clEmp.SubDeptId;

        SqlParameter p_ContractPurpose = cmd[0].Parameters.Add("ContractPurpose", SqlDbType.VarChar);
        p_ContractPurpose.Direction = ParameterDirection.Input;
        p_ContractPurpose.Value = clEmp.ContractPurpose;

        SqlParameter p_WorkArea = cmd[0].Parameters.Add("WorkArea", SqlDbType.VarChar);
        p_WorkArea.Direction = ParameterDirection.Input;
        p_WorkArea.Value = clEmp.WorkArea;

        SqlParameter p_SalLocId = cmd[0].Parameters.Add("SalLocId", DBNull.Value);
        p_SalLocId.Direction = ParameterDirection.Input;
        p_SalLocId.IsNullable = true;
        if (clEmp.SalLocId != "99999")
            p_SalLocId.Value = clEmp.SalLocId;

        SqlParameter p_LocCatId = cmd[0].Parameters.Add("LocCatId", DBNull.Value);
        p_LocCatId.Direction = ParameterDirection.Input;
        p_LocCatId.IsNullable = true;
        if (clEmp.LocCatId != "99999")
            p_LocCatId.Value = clEmp.LocCatId;

        SqlParameter p_BasicSalary = cmd[0].Parameters.Add("BasicSalary", SqlDbType.Decimal);
        p_BasicSalary.Direction = ParameterDirection.Input;
        p_BasicSalary.Value = clEmp.BasicSalary;

        SqlParameter p_GrossSalary = cmd[0].Parameters.Add("GrossSalary", SqlDbType.Decimal);
        p_GrossSalary.Direction = ParameterDirection.Input;
        p_GrossSalary.Value = clEmp.GrossSalary;

        SqlParameter p_JoiningDate = cmd[0].Parameters.Add("JoiningDate", DBNull.Value);
        p_JoiningDate.Direction = ParameterDirection.Input;
        p_JoiningDate.IsNullable = true;
        if (clEmp.JoiningDate != "")
            p_JoiningDate.Value = clEmp.JoiningDate;

        SqlParameter p_ProbationPeriod = cmd[0].Parameters.Add("ProbationPeriod", DBNull.Value);
        p_ProbationPeriod.Direction = ParameterDirection.Input;
        p_ProbationPeriod.IsNullable = true;
        if (clEmp.ProbationPeriod != "")
            p_ProbationPeriod.Value = clEmp.ProbationPeriod;
        else
            p_ProbationPeriod.Value = 0;

        SqlParameter p_ContractInterval = cmd[0].Parameters.Add("ContractInterval", DBNull.Value);
        p_ContractInterval.Direction = ParameterDirection.Input;
        p_ContractInterval.IsNullable = true;
        if (clEmp.ContractInterval != "")
            p_ContractInterval.Value = clEmp.ContractInterval;

        SqlParameter p_ContractEndDate = cmd[0].Parameters.Add("ContractEndDate", DBNull.Value);
        p_ContractEndDate.Direction = ParameterDirection.Input;
        p_ContractEndDate.IsNullable = true;
        if (clEmp.ContractEndDate != "")
            p_ContractEndDate.Value = clEmp.ContractEndDate;

        SqlParameter p_ConfirmationDate = cmd[0].Parameters.Add("ConfirmationDate", DBNull.Value);
        p_ConfirmationDate.Direction = ParameterDirection.Input;
        p_ConfirmationDate.IsNullable = true;
        if (clEmp.ConfirmationDate != "")
            p_ConfirmationDate.Value = clEmp.ConfirmationDate;

        SqlParameter p_IsServiceAgrmnt = cmd[0].Parameters.Add("IsServiceAgrmnt", SqlDbType.Char);
        p_IsServiceAgrmnt.Direction = ParameterDirection.Input;
        p_IsServiceAgrmnt.Value = clEmp.IsServiceAgrmnt;

        SqlParameter p_ServiceStartDate = cmd[0].Parameters.Add("ServiceStartDate", DBNull.Value);
        p_ServiceStartDate.Direction = ParameterDirection.Input;
        p_ServiceStartDate.IsNullable = true;
        if (clEmp.ServiceStartDate != "")
            p_ServiceStartDate.Value = clEmp.ServiceStartDate;

        SqlParameter p_ServiceEndDate = cmd[0].Parameters.Add("ServiceEndDate", DBNull.Value);
        p_ServiceEndDate.Direction = ParameterDirection.Input;
        p_ServiceEndDate.IsNullable = true;
        if (clEmp.ServiceEndDate != "")
            p_ServiceEndDate.Value = clEmp.ServiceEndDate;

        SqlParameter p_WorkAreaType = cmd[0].Parameters.Add("WorkAreaType", SqlDbType.Char);
        p_WorkAreaType.Direction = ParameterDirection.Input;
        p_WorkAreaType.Value = clEmp.WorkAreaType;

        SqlParameter p_IsSeveranceBenefit = cmd[0].Parameters.Add("IsSeveranceBenefit", SqlDbType.Char);
        p_IsSeveranceBenefit.Direction = ParameterDirection.Input;
        p_IsSeveranceBenefit.Value = clEmp.IsSeveranceBenefit;

        SqlParameter p_SeveranceId = cmd[0].Parameters.Add("SeveranceId", SqlDbType.VarChar);
        p_SeveranceId.Direction = ParameterDirection.Input;
        p_SeveranceId.Value = clEmp.SeveranceId;

        SqlParameter p_SeveranceReason = cmd[0].Parameters.Add("SeveranceReason", SqlDbType.VarChar);
        p_SeveranceReason.Direction = ParameterDirection.Input;
        p_SeveranceReason.Value = clEmp.SeveranceReason;

        SqlParameter p_RetirementDate = cmd[0].Parameters.Add("RetirementDate", DBNull.Value);
        p_RetirementDate.Direction = ParameterDirection.Input;
        p_RetirementDate.IsNullable = true;
        if (clEmp.RetirementDate != "")
            p_RetirementDate.Value = clEmp.RetirementDate;

        SqlParameter p_EmpStatus = cmd[0].Parameters.Add("EmpStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = clEmp.EmpStatus;

        SqlParameter p_SeparateTypeId = cmd[0].Parameters.Add("SeparateTypeId", DBNull.Value);
        p_SeparateTypeId.Direction = ParameterDirection.Input;
        p_SeparateTypeId.IsNullable = true;
        if (clEmp.SeparateTypeId != "99999")
            p_SeparateTypeId.Value = clEmp.SeparateTypeId;

        SqlParameter p_SeparateDate = cmd[0].Parameters.Add("SeparateDate", DBNull.Value);
        p_SeparateDate.Direction = ParameterDirection.Input;
        p_SeparateDate.IsNullable = true;
        if (clEmp.SeparateDate != "")
            p_SeparateDate.Value = clEmp.SeparateDate;

        SqlParameter p_SeparateReason = cmd[0].Parameters.Add("SeparateReason", SqlDbType.VarChar);
        p_SeparateReason.Direction = ParameterDirection.Input;
        p_SeparateReason.Value = clEmp.SeparateReason;

        SqlParameter p_BankCode = cmd[0].Parameters.Add("BankCode", SqlDbType.VarChar);
        p_BankCode.Direction = ParameterDirection.Input;
        p_BankCode.Value = clEmp.BankCode;

        SqlParameter p_RoutingNo = cmd[0].Parameters.Add("RoutingNo", SqlDbType.VarChar);
        p_RoutingNo.Direction = ParameterDirection.Input;
        p_RoutingNo.Value = clEmp.RoutingNo;

        SqlParameter p_BankAccNo = cmd[0].Parameters.Add("BankAccNo", SqlDbType.VarChar);
        p_BankAccNo.Direction = ParameterDirection.Input;
        p_BankAccNo.Value = clEmp.BankAccNo;

        SqlParameter p_SupervisorId = cmd[0].Parameters.Add("SupervisorId", SqlDbType.VarChar);
        p_SupervisorId.Direction = ParameterDirection.Input;
        p_SupervisorId.Value = clEmp.SupervisorId;

        SqlParameter p_OtherBenefit = cmd[0].Parameters.Add("OtherBenefit", SqlDbType.VarChar);
        p_OtherBenefit.Direction = ParameterDirection.Input;
        p_OtherBenefit.Value = clEmp.OtherBenefit;

        SqlParameter p_Remarks = cmd[0].Parameters.Add("Remarks", SqlDbType.VarChar);
        p_Remarks.Direction = ParameterDirection.Input;
        p_Remarks.Value = clEmp.Remarks;

        SqlParameter p_IsMedicalEntmnt = cmd[0].Parameters.Add("IsMedicalEntmnt", SqlDbType.Char);
        p_IsMedicalEntmnt.Direction = ParameterDirection.Input;
        p_IsMedicalEntmnt.Value = clEmp.IsMedicalEntmnt;

        SqlParameter p_IsOTEntmnt = cmd[0].Parameters.Add("IsOTEntmnt", SqlDbType.Char);
        p_IsOTEntmnt.Direction = ParameterDirection.Input;
        p_IsOTEntmnt.Value = clEmp.IsOTEntmnt;

        SqlParameter p_IsChildEduAllow = cmd[0].Parameters.Add("IsChildEduAllow", SqlDbType.Char);
        p_IsChildEduAllow.Direction = ParameterDirection.Input;
        p_IsChildEduAllow.Value = clEmp.IsChildEduAllow;

        SqlParameter p_EmpSignature = cmd[0].Parameters.Add("EmpSignature", SqlDbType.VarChar);
        p_EmpSignature.Direction = ParameterDirection.Input;
        p_EmpSignature.Value = clEmp.EmpSignature;

        SqlParameter p_UploadCV = cmd[0].Parameters.Add("UploadCV", SqlDbType.VarChar);
        p_UploadCV.Direction = ParameterDirection.Input;
        p_UploadCV.Value = clEmp.EmpCV;

        SqlParameter p_UploadDocument = cmd[0].Parameters.Add("UploadDocument", SqlDbType.VarChar);
        p_UploadDocument.Direction = ParameterDirection.Input;
        p_UploadDocument.Value = clEmp.EmpDocument;

        SqlParameter p_PostingDate = cmd[0].Parameters.Add("PostingDate", DBNull.Value);
        p_PostingDate.Direction = ParameterDirection.Input;
        p_PostingDate.IsNullable = true;
        if (clEmp.PostingDate != "")
            p_PostingDate.Value = clEmp.PostingDate;

        SqlParameter p_DateInPosition = cmd[0].Parameters.Add("DateInPosition", DBNull.Value);
        p_DateInPosition.Direction = ParameterDirection.Input;
        p_DateInPosition.IsNullable = true;
        if (clEmp.DateInPosition != "")
            p_DateInPosition.Value = clEmp.DateInPosition;

        SqlParameter p_DateInGrade = cmd[0].Parameters.Add("DateInGrade", DBNull.Value);
        p_DateInGrade.Direction = ParameterDirection.Input;
        p_DateInGrade.IsNullable = true;
        if (clEmp.DateInGrade != "")
            p_DateInGrade.Value = clEmp.DateInGrade;

        SqlParameter p_ActionDate = cmd[0].Parameters.Add("ActionDate", DBNull.Value);
        p_ActionDate.Direction = ParameterDirection.Input;
        p_ActionDate.IsNullable = true;
        if (clEmp.ActionDate != "")
            p_ActionDate.Value = Common.ReturnDate(clEmp.ActionDate);

        SqlParameter p_SalPakId = cmd[0].Parameters.Add("SalPakId", DBNull.Value);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.IsNullable = true;
        if (clEmp.SalPakId != "99999")
            p_SalPakId.Value = clEmp.SalPakId;

        SqlParameter p_BonusPakId = cmd[0].Parameters.Add("BonusPakId", DBNull.Value);
        p_BonusPakId.Direction = ParameterDirection.Input;
        p_BonusPakId.IsNullable = true;
        if ((clEmp.BonusPakId != "99999") && (clEmp.BonusPakId != "-1"))
            p_BonusPakId.Value = clEmp.BonusPakId;

        SqlParameter p_LeavePakId = cmd[0].Parameters.Add("LeavePakId", DBNull.Value);
        p_LeavePakId.Direction = ParameterDirection.Input;
        p_LeavePakId.IsNullable = true;
        if (clEmp.LeavePakId != "99999")
            p_LeavePakId.Value = clEmp.LeavePakId;

        SqlParameter p_WeekendId = cmd[0].Parameters.Add("WeekendId", DBNull.Value);
        p_WeekendId.Direction = ParameterDirection.Input;
        p_WeekendId.IsNullable = true;
        if (clEmp.WeekendId != "99999")
            p_WeekendId.Value = clEmp.WeekendId;

        SqlParameter p_AttnPolicyID = cmd[0].Parameters.Add("AttnPolicyID", DBNull.Value);
        p_AttnPolicyID.Direction = ParameterDirection.Input;
        p_AttnPolicyID.IsNullable = true;
        if (clEmp.AttnPolicyID != "99999")
            p_AttnPolicyID.Value = clEmp.AttnPolicyID;

        SqlParameter p_CardNo = cmd[0].Parameters.Add("CardNo", SqlDbType.Char);
        p_CardNo.Direction = ParameterDirection.Input;
        p_CardNo.Value = clEmp.CardNo;

        SqlParameter p_MPCID = cmd[0].Parameters.Add("MPCId", DBNull.Value);
        p_MPCID.Direction = ParameterDirection.Input;
        p_MPCID.IsNullable = true;
        if (clEmp.MPCId != "99999")
            p_MPCID.Value = clEmp.MPCId;

        SqlParameter p_IsPayrollStaff = cmd[0].Parameters.Add("IsPayrollStaff", SqlDbType.Char);
        p_IsPayrollStaff.Direction = ParameterDirection.Input;
        p_IsPayrollStaff.Value = clEmp.IsPayrollStaff;

        SqlParameter p_WorkingDays = cmd[0].Parameters.Add("WorkingDays", DBNull.Value);
        p_WorkingDays.Direction = ParameterDirection.Input;
        p_WorkingDays.IsNullable = true;
        if (clEmp.WorkingDays != "")
            p_WorkingDays.Value = clEmp.WorkingDays;

        SqlParameter p_IsNotRehirable = cmd[0].Parameters.Add("IsNotRehirable", SqlDbType.Char);
        p_IsNotRehirable.Direction = ParameterDirection.Input;
        p_IsNotRehirable.Value = clEmp.IsNotRehirable;

        SqlParameter p_NotRehireReason = cmd[0].Parameters.Add("NotRehireReason", SqlDbType.VarChar);
        p_NotRehireReason.Direction = ParameterDirection.Input;
        p_NotRehireReason.Value = clEmp.NotRehireReason;
        
        SqlParameter p_ProjectId = cmd[0].Parameters.Add("ProjectId", DBNull.Value);
        p_ProjectId.Direction = ParameterDirection.Input;
        p_ProjectId.IsNullable = true;
        if (clEmp.ProjectId != "99999")
            p_ProjectId.Value = clEmp.ProjectId;

        SqlParameter p_ProjOfficeId = cmd[0].Parameters.Add("ProjOfficeId", DBNull.Value);
        p_ProjOfficeId.Direction = ParameterDirection.Input;
        p_ProjOfficeId.IsNullable = true;
        if (clEmp.ProjectOfficeId != "99999")
            p_ProjOfficeId.Value = clEmp.ProjectOfficeId;

        SqlParameter p_ClinicId = cmd[0].Parameters.Add("ClinicId", DBNull.Value);
        p_ClinicId.Direction = ParameterDirection.Input;
        p_ClinicId.IsNullable = true;
        if (clEmp.ClinicId != "99999")
            p_ClinicId.Value = clEmp.ClinicId;

        SqlParameter p_Asset = cmd[0].Parameters.Add("Asset", SqlDbType.VarChar);
        p_Asset.Direction = ParameterDirection.Input;
        p_Asset.Value = clEmp.Asset;

        SqlParameter p_TaxRegionId = cmd[0].Parameters.Add("TaxRegionId", DBNull.Value);
        p_TaxRegionId.Direction = ParameterDirection.Input;
        p_TaxRegionId.IsNullable = true;
        if (clEmp.TaxRegionId != "99999")
            p_TaxRegionId.Value = clEmp.TaxRegionId;

        SqlParameter p_IncrementDate = cmd[0].Parameters.Add("IncrementDate", DBNull.Value);
        p_IncrementDate.Direction = ParameterDirection.Input;
        p_IncrementDate.IsNullable = true;
        if (clEmp.IncrementDate != "")
            p_IncrementDate.Value =clEmp.IncrementDate;

        SqlParameter p_IsConfirmed = cmd[0].Parameters.Add("IsConfirmed", SqlDbType.Char);
        p_IsConfirmed.Direction = ParameterDirection.Input;
        p_IsConfirmed.Value = clEmp.IsConfirmed;
        #endregion

        //if (clEmp.IsMedicalEntmnt == "Y")
        //{
        //    DataTable dtMedical = SelectEmpMedicalBalance(clEmp.EmpId);
        //    if (dtMedical.Rows.Count == 0)
        //        cmd[1] = InsertEmpMedicalBalance(clEmp.EmpId, clEmp.JoiningDate, clEmp.InsertedBy, clEmp.InsertedDate);
        //}

        if (strIsNew == "Y")
        {
            cmd[2] = InsertEmpActionLog(clEmp.EmpId, "21", clEmp.JoiningDate, clEmp.InsertedBy, clEmp.InsertedDate);
            //cmd[3] = InsertEmpInfoJoining(clEmp.EmpId, clEmp.JobTitleId, clEmp.JoiningDate, clEmp.CompanyId, clEmp.DeptId, clEmp.SalLocId, clEmp.GrossSalary, clEmp.InsertedBy, clEmp.InsertedDate);
        }
        if (clEmp.SeparateTypeId != "" && clEmp.SeparateTypeId != "99999")
        {
            DataTable dtActionLog = SelectEmpActionLog(clEmp.EmpId);
            DataRow[] foundRow;
            foundRow = dtActionLog.Select("ActionDate='" + clEmp.SeparateDate + "'");

            if (foundRow.Length == 0)
                cmd[4] = InsertEmpActionLog(clEmp.EmpId, clEmp.SeparateTypeId, clEmp.SeparateDate, clEmp.InsertedBy, clEmp.InsertedDate);
        }
        i = 5;

        foreach (SqlCommand cmdTemp in cmd1)
        {
            if (cmdTemp != null)
            {
                cmd[i] = cmdTemp;
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
   
    #endregion

    #region Select Queries From Tables By store procedure

    public DataTable SelectEmpInfoForLeave(string EmpID)
    {
        if (objDC.ds.Tables["EmpInfo"] != null)
        {
            objDC.ds.Tables["EmpInfo"].Rows.Clear();
            objDC.ds.Tables["EmpInfo"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_Select_EmpInfo");
        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        objDC.CreateDSFromProc(command, "EmpInfo");
        return objDC.ds.Tables["EmpInfo"];
    }


    public DataTable SelectEmpInfoOfficeWiseForLeave(string EmpID, string strDivID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpInfoOfficeWise");

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        SqlParameter p_DivID = command.Parameters.Add("DivisionId", SqlDbType.BigInt);
        p_DivID.Direction = ParameterDirection.Input;
        p_DivID.Value = strDivID;

        objDC.CreateDSFromProc(command, "EmpInfoOffWise");
        return objDC.ds.Tables["EmpInfoOffWise"];
    }

    // For Supervisor
    public DataTable SelectEmpInfoOfficeWiseForLeaveSPV(string EmpID, string strDivID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpInfoOfficeWise");
        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;
        SqlParameter p_DivID = command.Parameters.Add("DivisionId", SqlDbType.BigInt);
        p_DivID.Direction = ParameterDirection.Input;
        p_DivID.Value = strDivID;
        objDC.CreateDSFromProc(command, "EmpInfoOffWiseSPV");
        return objDC.ds.Tables["EmpInfoOffWiseSPV"];
    }

    public DataTable SelectEmpInfo(string EmpID)
    {
        if (objDC.ds.Tables["EmpInfo"] != null)
        {
            objDC.ds.Tables["EmpInfo"].Rows.Clear();
            objDC.ds.Tables["EmpInfo"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_select_EmpInfo");

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        objDC.CreateDSFromProc(command, "EmpInfo");
        return objDC.ds.Tables["EmpInfo"];
    }

    public DataTable SelectEmpSupervisorInfo(string EmpID)
    {
        if (objDC.ds.Tables["EmpSupervisorInfo"] != null)
        {
            objDC.ds.Tables["EmpSupervisorInfo"].Rows.Clear();
            objDC.ds.Tables["EmpSupervisorInfo"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_select_EmpInfo");

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        objDC.CreateDSFromProc(command, "EmpSupervisorInfo");
        return objDC.ds.Tables["EmpSupervisorInfo"];
    }

    public DataTable GET_EMP_MPCID(string EmpID)
    {
        SqlCommand command = new SqlCommand("PROC_GET_EMP_MPCID");
        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        objDC.CreateDSFromProc(command, "EmpInfoMPC");
        return objDC.ds.Tables["EmpInfoMPC"];
    }

    public DataTable SelectEmployeeAllInfo(string EmpID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpInfo");
        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpID;
        objDC.CreateDSFromProc(command, "EmpInfo");
        return objDC.ds.Tables["EmpInfo"];
    }

    public DataTable SelectEmpInfoHR(string EmpID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpInfo_tabHR");
        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;
        objDC.CreateDSFromProc(command, "EmpInfoHr");
        return objDC.ds.Tables["EmpInfoHr"];
    }

    public DataTable Select_GradeLevel_MinMaxSal(Int32 GradeId)
    {
        SqlCommand command = new SqlCommand("Select_GradeLevel_MinMaxSal");
        SqlParameter p_GradeID = command.Parameters.Add("gradeId", SqlDbType.BigInt);
        p_GradeID.Direction = ParameterDirection.Input;
        p_GradeID.Value = GradeId;
        objDC.CreateDSFromProc(command, "Grdlvl");
        return objDC.ds.Tables["Grdlvl"];
    }

    public Int32 SelectLeavePakHRTAB(string EmpTypeStatus)
    {
        SqlCommand command = new SqlCommand("proc_Select_LeavePak_HRTAB");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpTypeStatus = command.Parameters.Add("EmpTypeStatus", SqlDbType.Char);
        p_EmpTypeStatus.Direction = ParameterDirection.Input;
        p_EmpTypeStatus.Value = EmpTypeStatus;

        objDC.CreateDSFromProc(command, "LeavePak_HRTAB");
        Int32 LeavpakID = 99999;
        foreach (DataRow dRow in objDC.ds.Tables["LeavePak_HRTAB"].Rows)
        {
            LeavpakID = Convert.ToInt32(dRow["LPakID"].ToString());
        }
        return LeavpakID;
    }

    public string SelectEmpWiseContractType(string sEmpId)
    {
        string strSQL = "SELECT EmpTypeId From EmpInfo Where EmpId='" + sEmpId + "' AND EmpTypeId IN(2,3)";
        SqlCommand command = new SqlCommand(strSQL);

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = sEmpId;

        return objDC.GetScalarVal(command);
    }

    public string SelectOldEmpInfo(string sOldEmpId)
    {
        string strSQL = "SELECT EmpId,OldEmpId From EmpInfo Where OldEmpId='" + sOldEmpId + "'";
        SqlCommand command = new SqlCommand(strSQL);

        SqlParameter p_EmpId = command.Parameters.Add("OldEmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = sOldEmpId;

        return objDC.GetScalarVal(command);
    }
    public string GetMaxEmpID( string strPrefix)
    {
        string strSQL = "";
        string strMaxID = "";
        string strTmp = "";
        long lnMaxID = 0;
        strSQL = "SELECT MAX(CAST(EMPID AS VARCHAR)) FROM EMPINFO WHERE EMPID NOT LIKE 'I%'";
        SqlCommand command = new SqlCommand(strSQL);
        strTmp = objDC.GetScalarVal(command);
        strTmp = strTmp.Trim();
        if (string.IsNullOrEmpty(strTmp))
        {
            strMaxID = strPrefix + "000001";
            return strMaxID;
        }
        strTmp = strTmp.Substring(1);
        lnMaxID = Convert.ToInt32(strTmp);
        lnMaxID = lnMaxID + 1;
        strTmp = lnMaxID.ToString();

        switch (strTmp.Length)
        {
            case 1:
                strMaxID = strPrefix + "00000" + strTmp;
                break;
            case 2:
                strMaxID = strPrefix + "0000" + strTmp;
                break;
            case 3:
                strMaxID = strPrefix + "000" + strTmp;
                break;
            case 4:
                strMaxID = strPrefix + "00" + strTmp;
                break;
            case 5:
                strMaxID = strPrefix + "0" + strTmp;
                break;
            case 6:
                strMaxID = strPrefix + strTmp;
                break;
        }
        return strMaxID;
    }

    #endregion

    #region Select Method for Employee Search Screen

    public DataTable GetSearchEmployee(string EmpID, string strProjectId, string strCompanyId,string strDept, string strGrade, string strDesgId,
        string strFullName, string strPersMobile, string strEmpAbbrName, string strSearchBy, string strUserId, string emptype, string EmpStatus)
    {
        string strCond = "";

        if (EmpID != "")
            strCond = " AND EmpId=@EmpId";
        else
            strCond = "";

        if (strProjectId != "-1")
            strCond = strCond + " AND ProjectId=@ProjectId";
        
        if (strCompanyId != "-1")
            strCond = strCond + " AND DivisionId=@DivisionId";
       
        if (strDept != "-1")
            strCond = strCond + " AND DeptId=@DeptId";
        
        if (strGrade != "-1")
            strCond = strCond + " AND GradeId=@GradeId";

        if (strDesgId != "-1")
            strCond = strCond + " AND DesigId=@DesigId";

        if (strFullName != "")
            strCond = strCond + " AND FullName LIKE '%" + strFullName + "%'";



        if (emptype != "-1")
            strCond = strCond + " AND EmpTypeId=@EmpTypeId";

        if (EmpStatus != "-1")
            strCond = strCond + " AND EmpStatus=@EmpStatus";

        string strSQL = "SELECT * FROM VW_EmpInfo WHERE 1<>2" + strCond + " ORDER BY EmpId";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;   

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        SqlParameter p_EmpTypeStatus = command.Parameters.Add("EmpTypeId", SqlDbType.BigInt);
        p_EmpTypeStatus.Direction = ParameterDirection.Input;
        p_EmpTypeStatus.Value = emptype;

        SqlParameter p_SectorId = command.Parameters.Add("DivisionId", SqlDbType.BigInt);
        p_SectorId.Direction = ParameterDirection.Input;
        p_SectorId.Value = strCompanyId;

        SqlParameter p_DeptId = command.Parameters.Add("DeptId", SqlDbType.BigInt);
        p_DeptId.Direction = ParameterDirection.Input;
        p_DeptId.Value = strDept;

        SqlParameter p_DesgId = command.Parameters.Add("DesigId", SqlDbType.BigInt);
        p_DesgId.Direction = ParameterDirection.Input;
        p_DesgId.Value = strDesgId;

        SqlParameter p_GradeId = command.Parameters.Add("GradeId", SqlDbType.BigInt);
        p_GradeId.Direction = ParameterDirection.Input;
        p_GradeId.Value = strGrade;

        SqlParameter p_UnitId = command.Parameters.Add("ProjectId", SqlDbType.BigInt);
        p_UnitId.Direction = ParameterDirection.Input;
        p_UnitId.Value = strProjectId;

        //SqlParameter p_FullName = command.Parameters.Add("FullName", SqlDbType.VarChar);
        //p_FullName.Direction = ParameterDirection.Input;
        //p_FullName.Value = strFullName;

        SqlParameter p_PersMobile = command.Parameters.Add("CellPhone", SqlDbType.VarChar);
        p_PersMobile.Direction = ParameterDirection.Input;
        p_PersMobile.Value = strPersMobile;

        SqlParameter p_SearchBy = command.Parameters.Add("SearchBy", SqlDbType.Char);
        p_SearchBy.Direction = ParameterDirection.Input;
        p_SearchBy.Value = strSearchBy;

        SqlParameter p_EmpStatus = command.Parameters.Add("EmpStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = EmpStatus;
       
        return objDC.CreateDT(command, "EmpSearch");
    }

    public DataTable GetEmployeeForTaskAlert()
    {
        if (objDC.ds.Tables["EmpSearch"] != null)
        {
            objDC.ds.Tables["EmpSearch"].Rows.Clear();
            objDC.ds.Tables["EmpSearch"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_Select_EmpForTaskAlert");
        objDC.CreateDSFromProc(command, "EmpSearch");
        return objDC.ds.Tables["EmpSearch"];
    }


    #endregion

    # region To get the Supervisor Wise Emp
    // Edited By Amit For Dynamic Menu Load
    public DataTable GetSuperVisiorWiseEmp(string strEmpID, string strDivID)
    {
        int i = 0;
        string sql = "";
        DataRow[] mRows;        
        DBConnector objDB1 = new DBConnector();

        sql = "SELECT EMPID,FULLNAME,SupervisorId,PersonalEmail FROM EMPINFO WHERE EmpStatus='A' ORDER BY EMPID";//,ISCOUNTRYDIRECTOR
        DataTable dtEmp = objDB1.CreateDT(sql, "SPVWiseEmp");

        DataRow[] spvRow = FindInDataTable(dtEmp, "EMPID='" + strEmpID + "'");
        objDS.dtEmpList.ImportRow(spvRow[0]);

        mRows = FindInDataTable(dtEmp, "SupervisorId='" + strEmpID + "'");
        foreach (DataRow row in mRows)
        {
            //if (row["ISCOUNTRYDIRECTOR"].ToString().Trim() == "Y")
            //{
            //    objDS.dtEmpList.ImportRow(row);
            //}
            //else
            //{
                objDS.dtEmpList.ImportRow(row);
                GetChildEmployee(dtEmp, row);
            //}
            i++;
        }

        objDS.dtEmpList.AcceptChanges();
        dtEmp = null;
        return objDS.dtEmpList;
    }

    public DataTable GetSuperviseeEmp(string strEmpID)
    {
        string sql = "";
        DBConnector objDB1 = new DBConnector();
        sql = "SELECT EMPID,FULLNAME,SupervisorId,PersonalEmail FROM EMPINFO WHERE EmpStatus='A' AND SupervisorId='" + strEmpID + "' ORDER BY EMPID";
        return objDB1.CreateDT(sql, "SuperviseeEmp");
    }

    public DataRow[] FindInDataTable(DataTable dtEMp, string strExpr)
    {
        DataRow[] foundRows;
        foundRows = dtEMp.Select(strExpr);
        return foundRows;
    }

    public void GetChildEmployee(DataTable dtEMp, DataRow row)
    {
        DataRow[] cRows;
        cRows = null;
        cRows = FindInDataTable(dtEMp, "REPORTINGTO='" + row["EMPID"].ToString().Trim() + "'");
        foreach (DataRow rowc in cRows)
        {
            if (row["ISCOUNTRYDIRECTOR"].ToString().Trim() != "Y")
            {
                objDS.dtEmpList.ImportRow(rowc);
                GetChildEmployee(dtEMp, rowc);
            }
        }
        return;
    }

    public DataTable SelectDivisionWiseEmp(string EmpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_DivisionWiseEmpForUserPermission");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDSFromProc(command, "DivisionWiseEmp");
        return objDC.ds.Tables["DivisionWiseEmp"];
    }

    public DataTable SelectDivisionalEmp(string strDivID)
    {
        string strSQL = "SELECT FULLNAME + ' [' + EMPID + ']' AS EMPNAME,EMPID FROM EMPINFO WHERE LocCatId=@DIVISIONID ORDER BY EMPID";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_DivId = command.Parameters.Add("DIVISIONID", SqlDbType.BigInt);
        p_DivId.Direction = ParameterDirection.Input;
        p_DivId.Value = strDivID;

        objDC.CreateDT(command, "DivisionalEmp");
        return objDC.ds.Tables["DivisionalEmp"];
    }

    public DataTable SelectSupervisor()
    {
        if (objDC.ds.Tables["SupervisorEmp"] != null)
        {
            objDC.ds.Tables["SupervisorEmp"].Rows.Clear();
            objDC.ds.Tables["SupervisorEmp"].Dispose();
        }
        string strSQL = "SELECT FULLNAME + ' [' + EMPID + ']' AS EMPNAME,EMPID FROM EMPINFO ORDER BY EMPID";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        objDC.CreateDT(command, "SupervisorEmp");
        return objDC.ds.Tables["SupervisorEmp"];
    }

    public DataTable SelectEmpNameWithID(string strStatus)
    {
        string strSQL = "SELECT  FULLNAME + ' [' + EMPID + ']' AS EMPNAME,EMPID,dg.DesigName,dl.DeptName" +
            " from EmpInfo ei left join Designation dg on ei.DesigId=dg.DesigId "+
            "left join DepartmentList dl on ei.DeptId = dl.DeptId WHERE ei.ISDELETED ='N'";
        if (!string.IsNullOrEmpty(strStatus))
        {
            strSQL += " AND ei.EmpStatus='" + strStatus+"'";
        }
        strSQL+= " ORDER BY ei.EMPID";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        //SqlParameter p_STATUS = command.Parameters.Add("STATUS", SqlDbType.Char);
        //p_STATUS.Direction = ParameterDirection.Input;
        //p_STATUS.Value = strStatus;

        objDC.CreateDT(command, "SelectEmpNameWithID");
        return objDC.ds.Tables["SelectEmpNameWithID"];
    }
    
    public DataTable SelectIntEmpWithID()
    {
        string strSQL = "SELECT FULLNAME + ' [' + EMPID + ']' AS EMPNAME,EMPID FROM EMPINFO WHERE EmpId like 'I%' AND ISDELETED='N' ORDER BY EMPID";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        objDC.CreateDT(command, "SelectEmpNameWithID");
        return objDC.ds.Tables["SelectEmpNameWithID"];
    }

    public DataTable SelectScheWiseEmp(string strScheduleID)
    {
        if (objDC.ds.Tables["SelectScheWiseEmp"] != null)
        {
            objDC.ds.Tables["SelectScheWiseEmp"].Rows.Clear();
            objDC.ds.Tables["SelectScheWiseEmp"].Dispose();
        }

        string strSQL = "SELECT E.FULLNAME + ' [' + E.EMPID + ']' AS EMPNAME,EMPID FROM EMPINFO E, TrTrainingListSetup TLM,TrTrainingListSetupDtl TLD"
            + " WHERE TLM.TrainListId=TLD.TrainListId AND E.EmpId=TLD.TraineeId AND TLM.ScheduleID=" + strScheduleID + " ORDER BY E.EMPID";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_ScheduleID = command.Parameters.Add("ScheduleID", SqlDbType.BigInt);
        p_ScheduleID.Direction = ParameterDirection.Input;
        p_ScheduleID.Value = strScheduleID;

        objDC.CreateDT(command, "SelectScheWiseEmp");
        return objDC.ds.Tables["SelectScheWiseEmp"];
    }

    public DataTable SelectEmpNameWithIDForIT(string strStatus, string strFinYear)
    {
        string strSQL = "SELECT Distinct P.EMPID,E.FULLNAME + ' [' + RTRIM(P.EMPID) + ']' AS EMPNAME "
                      + " FROM PAYSLIPMST P,EMPINFO E "
                      + " WHERE P.EMPID=E.EmpId AND E.EmpStatus=@STATUS AND E.ISDELETED='N' AND P.TAXFISCALYRID=@TAXFISCALYRID "
                      + " ORDER BY EMPID ";

        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_STATUS = command.Parameters.Add("STATUS", SqlDbType.Char);
        p_STATUS.Direction = ParameterDirection.Input;
        p_STATUS.Value = strStatus;

        SqlParameter p_FISCALYRID = command.Parameters.Add("TAXFISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        objDC.CreateDT(command, "SelectEmpNameWithIDForIT");
        return objDC.ds.Tables["SelectEmpNameWithIDForIT"];
    }

    //public DataTable SelectEmpPayslipPersonalInfo(string strEmpID)
    //{
    //    string strSQL = "SELECT E.EmpId,E.FullName AS FULLNAME,J.JobTitleName,LL.PostingPlaceName " +
    //                    "FROM EmpInfo E,JobTitle J,PostingPlaceList LL " +
    //                    "WHERE E.JobTitleId=J.JobTitleId AND E.PostingPlaceId=LL.PostingPlaceId AND EMPID=@EMPID";

    //    SqlCommand command = new SqlCommand(strSQL);
    //    command.CommandType = CommandType.Text;

    //    SqlParameter p_EMPID = command.Parameters.Add("EMPID", SqlDbType.Char);
    //    p_EMPID.Direction = ParameterDirection.Input;
    //    p_EMPID.Value = strEmpID;

    //    objDC.CreateDT(command, "SelectEmpPayslipPersonalInfo");
    //    return objDC.ds.Tables["SelectEmpPayslipPersonalInfo"];
    //}

    public DataTable SelectEmpPayslipPersonalInfo(string strEmpID)
    {
        string strSQL = "SELECT E.EmpId,E.FullName AS FULLNAME,J.LocCatName,e.BankAccNo,Desi.DesigName,pd.DivisionName," +
                    "PDis.ProjectName,DeptL.DeptName,sl.ClinicName,g.GradeName" +
                    " FROM EmpInfo E left join LocationCategory J on E.LocCatId=J.LocCatId" +
                    " left join Designation Desi on E.DesigId=Desi.DesigId" +
                    " left join DivisionList pd on E.DivisionId=pd.DivisionId" +
                    " left join ProjectList PDis on E.ProjectID=PDis.ProjectID" +
                    " left join DepartmentList DeptL on E.DeptId=DeptL.DeptId " +
                    " left join ClinicList  sl on E.ClinicId=sl.ClinicId" +
                    " left join GradeList g on E.GradeId=g.GradeId" +
                    " WHERE E.DesigId=Desi.DesigId AND EMPID=@EMPID";

        //string strSQL = "SELECT E.EmpId,E.FullName AS FULLNAME,J.JobTitleName,LL.PostingPlaceName " +
        //                "FROM EmpInfo E,JobTitle J,PostingPlaceList LL " +
        //                "WHERE E.JobTitleId=J.JobTitleId AND E.PostingPlaceId=LL.PostingPlaceId AND EMPID=@EMPID";

        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_EMPID = command.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        objDC.CreateDT(command, "SelectEmpPayslipPersonalInfo");
        return objDC.ds.Tables["SelectEmpPayslipPersonalInfo"];
    }

    public string GetEmpName(string strUserID)
    {
        string strSQL = "SELECT E.FULLNAME + ' <br> ' + J.DesigName FROM EMPINFO E,USERINFO U,Designation J "
                   + " WHERE E.EMPID=U.EMPID AND E.DESIGID=J.DESIGID AND U.USERID=@USERID";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_USERID = command.Parameters.Add("USERID", SqlDbType.Char);
        p_USERID.Direction = ParameterDirection.Input;
        p_USERID.Value = strUserID;

        return objDC.GetScalarVal(command);
    }
    #endregion

    #region Bank and Branch
    public DataTable SelectBranchList(string BankCode)
    {
        SqlCommand command = new SqlCommand("proc_Select_BranchList");

        SqlParameter p_BankCode = command.Parameters.Add("BankCode", SqlDbType.Char);
        p_BankCode.Direction = ParameterDirection.Input;
        p_BankCode.Value = BankCode;

        objDC.CreateDSFromProc(command, "BranchList");
        return objDC.ds.Tables["BranchList"];
    }
    public DataTable SelectBankList()
    {
        string strSQL = "SELECT DISTINCT BankName,BankCode FROM BankList "
            + " WHERE BankName IS NOT NULL ORDER BY BankName";
        return objDC.CreateDT(strSQL, "BankList");
    }
    #endregion

    #region Payroll
    public void InsertEmpPayrollInfo(string strEmpID, string strSalPakID, string strGradeId, string strGrdEffDate, string strGMSEffDate, string strGMSClsDate,
       string strBonusPakID, string strBankAccNo, string strBankCode, string strBranchCode,
       string strInsBy, string strInsDate, DataTable dtSalPackMst, DataTable dtSalPackDetls, string strAccLIne, string strMPCID)
    {//Hr information of Employee
        int i = 0;
        SqlCommand[] cmd = new SqlCommand[3 + dtSalPackMst.Rows.Count + dtSalPackDetls.Rows.Count];

        // Update Empinfo Table
        cmd[0] = new SqlCommand("proc_Payroll_Insert_EmpInfo");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpId = cmd[0].Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpID;

        SqlParameter p_SalPakId = cmd[0].Parameters.Add("SalaryPakId", SqlDbType.BigInt);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = strSalPakID;

        SqlParameter p_GradeId = cmd[0].Parameters.Add("GradeId", SqlDbType.BigInt);
        p_GradeId.Direction = ParameterDirection.Input;
        p_GradeId.Value = strGradeId;

        SqlParameter p_GradeEffDate = cmd[0].Parameters.Add("GradeEffDate", DBNull.Value);
        p_GradeEffDate.Direction = ParameterDirection.Input;
        p_GradeEffDate.IsNullable = true;
        if (strGrdEffDate != "")
            p_GradeEffDate.Value = strGrdEffDate;

        SqlParameter p_SalPakEffDate = cmd[0].Parameters.Add("SalPakEffDate", DBNull.Value);
        p_SalPakEffDate.Direction = ParameterDirection.Input;
        p_SalPakEffDate.IsNullable = true;
        if (strGMSEffDate != "")
            p_SalPakEffDate.Value = strGMSEffDate;

        SqlParameter p_SalPakClsDate = cmd[0].Parameters.Add("SalPakClsDate", DBNull.Value);
        p_SalPakClsDate.Direction = ParameterDirection.Input;
        p_SalPakClsDate.IsNullable = true;
        if (strGMSClsDate != "")
            p_SalPakClsDate.Value = strGMSClsDate;


        SqlParameter p_BonusPakId = cmd[0].Parameters.Add("BonusPakId", DBNull.Value);
        p_BonusPakId.Direction = ParameterDirection.Input;
        p_BonusPakId.IsNullable = true;
        if (strBonusPakID != "")
            p_BonusPakId.Value = strBonusPakID;

        SqlParameter p_BankAccNo = cmd[0].Parameters.Add("BankAccNo", SqlDbType.Char);
        p_BankAccNo.Direction = ParameterDirection.Input;
        p_BankAccNo.Value = strBankAccNo;

        SqlParameter p_BankCode = cmd[0].Parameters.Add("BankCode", SqlDbType.Char);
        p_BankCode.Direction = ParameterDirection.Input;
        p_BankCode.Value = strBankCode;

        SqlParameter p_BranchCode = cmd[0].Parameters.Add("BranchCode", SqlDbType.Char);
        p_BranchCode.Direction = ParameterDirection.Input;
        p_BranchCode.Value = strBranchCode;



        SqlParameter p_InsertedBy = cmd[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = cmd[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        SqlParameter p_PlanAccLIne = cmd[0].Parameters.Add("PlanAccLIne", DBNull.Value);
        p_PlanAccLIne.Direction = ParameterDirection.Input;
        p_PlanAccLIne.IsNullable = true;
        if (strAccLIne != "")
            p_PlanAccLIne.Value = strAccLIne;

        SqlParameter p_MPCID = cmd[0].Parameters.Add("MPCID", SqlDbType.BigInt);
        p_MPCID.Direction = ParameterDirection.Input;
        p_MPCID.Value = strMPCID;

        i++;
        // Delete from EmpSalaryPakMst
        cmd[i] = new SqlCommand("proc_Payroll_Delete_EmpSalPackDetls");
        cmd[i].CommandType = CommandType.StoredProcedure;
        SqlParameter p_EmpId1 = cmd[i].Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId1.Direction = ParameterDirection.Input;
        p_EmpId1.Value = strEmpID;

        i++;
        // Delete from EmpSalaryPakDetls
        cmd[i] = new SqlCommand("proc_Payroll_Delete_EmpSalPackMst");
        cmd[i].CommandType = CommandType.StoredProcedure;
        SqlParameter p_EmpId2 = cmd[i].Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId2.Direction = ParameterDirection.Input;
        p_EmpId2.Value = strEmpID;
        i++;

        // Insert Into EmpSalPakMst
        foreach (DataRow dRow in dtSalPackMst.Rows)
        {
            cmd[i] = new SqlCommand();
            cmd[i] = InsertEmpSalPackMst(strEmpID, "1", strSalPakID, strInsDate, strInsBy, dRow);
            i++;
        }

        // Insert Into EmpSalPackDetls

        foreach (DataRow dRow1 in dtSalPackDetls.Rows)
        {
            cmd[i] = new SqlCommand();
            cmd[i] = InsertSalaryPakDet(strEmpID, strSalPakID, strInsDate, strInsBy, dRow1);
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


    private SqlCommand InsertEmpSalPackMst(string strEmpID, string strIncrID, string strSalPakID, string strInsDate, string strInsBy, DataRow dRow)
    {

        // Insert  EmpSalPackMst Table
        SqlCommand cmd = new SqlCommand("proc_Payroll_Insert_EmpSalPackMst");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpID;

        SqlParameter p_SalPakId = cmd.Parameters.Add("SalPakId", SqlDbType.Decimal);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = strSalPakID;

        SqlParameter p_IncrementID = cmd.Parameters.Add("IncrementID", DBNull.Value);
        p_IncrementID.Direction = ParameterDirection.Input;
        p_IncrementID.IsNullable = true;
        if (string.IsNullOrEmpty(strIncrID) == false)
            p_IncrementID.Value = strIncrID;

        SqlParameter p_CurrId = cmd.Parameters.Add("CurrId", SqlDbType.BigInt);
        p_CurrId.Direction = ParameterDirection.Input;
        p_CurrId.Value = dRow["CurrId"];

        SqlParameter p_WillConvert = cmd.Parameters.Add("WillConvert", SqlDbType.Char);
        p_WillConvert.Direction = ParameterDirection.Input;
        p_WillConvert.Value = dRow["WillConvert"];

        SqlParameter p_PayType = cmd.Parameters.Add("PayType", SqlDbType.BigInt);
        p_PayType.Direction = ParameterDirection.Input;
        p_PayType.Value = dRow["PayType"];

        SqlParameter p_OTAmt = cmd.Parameters.Add("OTAmt", SqlDbType.Decimal);
        p_OTAmt.Direction = ParameterDirection.Input;
        p_OTAmt.Value = dRow["OTAmt"];

        SqlParameter p_IsInPercent = cmd.Parameters.Add("IsInPercent", SqlDbType.Char);
        p_IsInPercent.Direction = ParameterDirection.Input;
        p_IsInPercent.Value = dRow["IsInPercent"];

        SqlParameter p_SalHeadID = cmd.Parameters.Add("SalHeadID", DBNull.Value);
        p_SalHeadID.Direction = ParameterDirection.Input;
        p_SalHeadID.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["SalHeadID"].ToString()) == false)
            p_SalHeadID.Value = dRow["SalHeadID"];

        SqlParameter p_AttBonusAmt = cmd.Parameters.Add("AttBonusAmt", SqlDbType.Decimal);
        p_AttBonusAmt.Direction = ParameterDirection.Input;
        p_AttBonusAmt.Value = dRow["AttBonusAmt"];

        SqlParameter p_IsBonusInPer = cmd.Parameters.Add("IsBonusInPer", SqlDbType.Char);
        p_IsBonusInPer.Direction = ParameterDirection.Input;
        p_IsBonusInPer.Value = dRow["IsBonusInPer"];

        SqlParameter p_SalHeadIDBonus = cmd.Parameters.Add("SalHeadIDBonus", DBNull.Value);
        p_SalHeadIDBonus.Direction = ParameterDirection.Input;
        p_SalHeadIDBonus.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["SalHeadIDBonus"].ToString()) == false)
            p_SalHeadIDBonus.Value = dRow["SalHeadIDBonus"];

        SqlParameter p_LateCount = cmd.Parameters.Add("LateCount", SqlDbType.BigInt);
        p_LateCount.Direction = ParameterDirection.Input;
        p_LateCount.Value = dRow["LateCount"];

        SqlParameter p_LateSalCount = cmd.Parameters.Add("LateSalCount", SqlDbType.BigInt);
        p_LateSalCount.Direction = ParameterDirection.Input;
        p_LateSalCount.Value = dRow["LateSalCount"];

        SqlParameter p_LateSalHead = cmd.Parameters.Add("LateSalHead", DBNull.Value);
        p_LateSalHead.Direction = ParameterDirection.Input;
        p_LateSalHead.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["LateSalHead"].ToString()) == false)
            p_LateSalHead.Value = dRow["LateSalHead"];

        SqlParameter p_TotalGrossSal = cmd.Parameters.Add("TotalGrossSal", SqlDbType.Decimal);
        p_TotalGrossSal.Direction = ParameterDirection.Input;
        p_TotalGrossSal.Value = dRow["TotalGrossSal"];

        SqlParameter p_IsAutoGrossCalc = cmd.Parameters.Add("IsAutoGrossCalc", SqlDbType.Char);
        p_IsAutoGrossCalc.Direction = ParameterDirection.Input;
        p_IsAutoGrossCalc.Value = dRow["IsAutoGrossCalc"];

        SqlParameter p_totalSalary = cmd.Parameters.Add("totalSalary", SqlDbType.Decimal);
        p_totalSalary.Direction = ParameterDirection.Input;
        p_totalSalary.Value = dRow["totalSalary"];


        SqlParameter p_InsertedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        return cmd;
    }


    public SqlCommand InsertSalaryPakDet(string strEmpID, string StrSalPakId, string strInsDate, string strInsBy, DataRow dRow)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Insert_EmpSalaryPakDetls");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpID;

        SqlParameter p_SalPakId = cmd.Parameters.Add("SalPakId", SqlDbType.BigInt);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = StrSalPakId;

        SqlParameter p_SHeadId = cmd.Parameters.Add("SHeadId", SqlDbType.BigInt);
        p_SHeadId.Direction = ParameterDirection.Input;
        p_SHeadId.Value = dRow["SHeadId"];

        SqlParameter p_PayAmt = cmd.Parameters.Add("PayAmt", SqlDbType.Decimal);
        p_PayAmt.Direction = ParameterDirection.Input;
        p_PayAmt.Value = dRow["PayAmt"];

        SqlParameter p_isInPercent = cmd.Parameters.Add("isInPercent", SqlDbType.Char);
        p_isInPercent.Direction = ParameterDirection.Input;
        p_isInPercent.Value = dRow["isInPercent"];


        SqlParameter p_PercntField = cmd.Parameters.Add("PercntField", DBNull.Value);
        p_PercntField.Direction = ParameterDirection.Input;
        p_PercntField.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["PercntField"].ToString()) == false)
            p_PercntField.Value = dRow["PercntField"];

        SqlParameter p_isBasicSal = cmd.Parameters.Add("isBasicSal", SqlDbType.Char);
        p_isBasicSal.Direction = ParameterDirection.Input;
        p_isBasicSal.Value = dRow["isBasicSal"];

        SqlParameter p_ISPFUND = cmd.Parameters.Add("ISPFUND", SqlDbType.Char);
        p_ISPFUND.Direction = ParameterDirection.Input;
        p_ISPFUND.Value = dRow["ISPFUND"];

        SqlParameter p_AMTCOMPAY = cmd.Parameters.Add("AMTCOMPAY", SqlDbType.Decimal);
        p_AMTCOMPAY.Direction = ParameterDirection.Input;
        p_AMTCOMPAY.Value = dRow["AMTCOMPAY"];

        SqlParameter p_TotAmnt = cmd.Parameters.Add("TotAmnt", SqlDbType.Decimal);
        p_TotAmnt.Direction = ParameterDirection.Input;
        p_TotAmnt.Value = dRow["TotAmnt"];


        SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_UpdatedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_UpdatedDate.Direction = ParameterDirection.Input;
        p_UpdatedDate.Value = strInsDate;

        return cmd;
    }
    #endregion

    #region DirctoryList
    public void InsertDirectoryList(string strDirName, string strDirLevel, string strInsBy, string strInsDate)
    {
        string strSQL = "Insert Into EmpDirectoryList(DirName,DirLevel,InsertedBy,InsertedDate ) "
                    + " Values(@DirName,@DirLevel,@InsertedBy,@InsertedDate )";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_DirName = command.Parameters.Add("DirName", SqlDbType.VarChar);
        p_DirName.Direction = ParameterDirection.Input;
        p_DirName.Value = strDirName;

        SqlParameter p_DirLevel = command.Parameters.Add("DirLevel", SqlDbType.Int);
        p_DirLevel.Direction = ParameterDirection.Input;
        p_DirLevel.Value = (string.IsNullOrEmpty(strDirLevel) == false ? Convert.ToInt32(strDirLevel) : 0);

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        objDC.ExecuteQuery(command);
    }
    public void DeleteDirectoryList(string strDirName)
    {
        string strSQL = "Delete From EmpDirectoryList Where DirName=@DirName";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;
        SqlParameter p_DirName = command.Parameters.Add("DirName", SqlDbType.VarChar);
        p_DirName.Direction = ParameterDirection.Input;
        p_DirName.Value = strDirName;

        objDC.ExecuteQuery(command);
    }

    public DataTable DirectoryList()
    {
        string strSQL = "SELECT (DirName + '-' + cast(DirLevel as Varchar)) as DirNameLevel,DirName,DirLevel "
                    + " FROM EmpDirectoryList ORDER BY  DirLevel,DirName ";
        return objDC.CreateDT(strSQL, "DirectoryList");
    }

    #endregion

    #region PF Loan
    public DataTable SelectEmpInfoForLedger(string EmpID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpInfo_ForLedger");
        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        objDC.CreateDSFromProc(command, "SelectEmpInfoForLedger");
        return objDC.ds.Tables["SelectEmpInfoForLedger"];
    }
    #endregion

    //Insert or update Employee Salary Amendment
    public void InsertEmpSalaryAmendment(string strLogId, string strEmpId, string strActionId, string strActionName, string strActionDate,
       string strBasicSal, string strInsertedBy, string strInsertedDate, string IsUpdate, string IsDelete, string strSalPackId, DataTable dtSalPackUpdate)
    {
        SqlCommand[] command = new SqlCommand[4 + dtSalPackUpdate.Rows.Count];
        command[0] = new SqlCommand("proc_Insert_EmpSalaryAmendment");
        command[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_ConfirmId = command[0].Parameters.Add("LogId", SqlDbType.BigInt);
        p_ConfirmId.Direction = ParameterDirection.Input;
        p_ConfirmId.Value = strLogId;

        SqlParameter p_EmpId = command[0].Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_ActionId = command[0].Parameters.Add("ActionId", SqlDbType.BigInt);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.Value = strActionId;

        SqlParameter p_ConfirmDate = command[0].Parameters.Add("ActionDate", DBNull.Value);
        p_ConfirmDate.Direction = ParameterDirection.Input;
        p_ConfirmDate.IsNullable = true;
        if (strActionDate != "")
            p_ConfirmDate.Value = strActionDate;

        SqlParameter p_BasicSal = command[0].Parameters.Add("BasicSal", DBNull.Value);
        p_BasicSal.Direction = ParameterDirection.Input;
        p_BasicSal.IsNullable = true;
        if (strBasicSal != "")
            p_BasicSal.Value = strBasicSal;

        SqlParameter p_InsertedBy = command[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsertedBy;

        SqlParameter p_InsertedDate = command[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsertedDate;

        SqlParameter p_IsUpdate = command[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command[0].Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        if (IsUpdate == "N")
        {
            //command[1] = InsertEmpInfoLog(strEmpId);

            //command[2] = UpdateEmpSalaryAmendment(strEmpId, strActionId, strActionDate, strBasicSal, strInsertedBy, strInsertedDate);

            command[3] = InsertEmpActionLog(strEmpId, strActionId,  strActionDate, strInsertedBy, strInsertedDate);

            //Housing & PF Allowance Update     
            int i = 4;
            if (dtSalPackUpdate.Rows.Count > 0)
            {
                foreach (SqlCommand cmdSal in this.GetSalPackDetUpdateCommand(dtSalPackUpdate, strSalPackId, strInsertedBy, strInsertedDate, "Salary Amendment"))
                {
                    command[i] = cmdSal;
                    i++;
                }
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

    public SqlCommand[] GetSalPackDetUpdateCommand(DataTable dtSalPackUpdate, string StrSalPakId, string strInsBy, string strInsDate, string strLastUpdatedFrom)
    {
        SqlCommand[] cmd = new SqlCommand[dtSalPackUpdate.Rows.Count];
        int i = 0;

        foreach (DataRow dRow in dtSalPackUpdate.Rows)
        {
            cmd[i] = new SqlCommand("proc_Payroll_Update_SalaryPackDetls");
            cmd[i].CommandType = CommandType.StoredProcedure;

            SqlParameter p_SalPakId = cmd[i].Parameters.Add("SalPakId", SqlDbType.BigInt);
            p_SalPakId.Direction = ParameterDirection.Input;
            p_SalPakId.Value = StrSalPakId;

            SqlParameter p_SHeadId = cmd[i].Parameters.Add("SHeadId", SqlDbType.BigInt);
            p_SHeadId.Direction = ParameterDirection.Input;
            p_SHeadId.Value = dRow["SHeadId"];

            SqlParameter p_PayAmt = cmd[i].Parameters.Add("PayAmt", SqlDbType.Decimal);
            p_PayAmt.Direction = ParameterDirection.Input;
            p_PayAmt.Value = dRow["PayAmt"];

            SqlParameter p_TotAmnt = cmd[i].Parameters.Add("TotAmnt", SqlDbType.Decimal);
            p_TotAmnt.Direction = ParameterDirection.Input;
            p_TotAmnt.Value = dRow["PayAmt"];

            SqlParameter p_InsertedBy = cmd[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = strInsBy;

            SqlParameter p_UpdatedDate = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_UpdatedDate.Direction = ParameterDirection.Input;
            p_UpdatedDate.Value = strInsDate;

            SqlParameter p_LastUpdatedFrom = cmd[i].Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
            p_LastUpdatedFrom.Direction = ParameterDirection.Input;
            p_LastUpdatedFrom.Value = strLastUpdatedFrom;

            i++;
        }
        return cmd;
    }

    //Insert or update employee advice warning information
    public void InsertAdvice_Warning(string strAdviceId, string strEmpId, string strActionId, string strActionName, string strEffectiveDate,
        string strAdviceCause, string strWarningNo, string strInsertedBy, string strInsertedDate, string IsUpdate, string IsDelete)
    {
        SqlCommand[] command = new SqlCommand[4];
        command[0] = new SqlCommand("proc_Insert_EmpAdvice_WarningLog");
        command[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_AdviceId = command[0].Parameters.Add("AdviceId", SqlDbType.BigInt);
        p_AdviceId.Direction = ParameterDirection.Input;
        p_AdviceId.Value = strAdviceId;

        SqlParameter p_EmpId = command[0].Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_ActionId = command[0].Parameters.Add("ActionId", SqlDbType.BigInt);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.Value = strActionId;

        SqlParameter p_EffectiveDate = command[0].Parameters.Add("EffectiveDate", DBNull.Value);
        p_EffectiveDate.Direction = ParameterDirection.Input;
        p_EffectiveDate.IsNullable = true;
        if (strEffectiveDate != "")
            p_EffectiveDate.Value = strEffectiveDate;

        SqlParameter p_AdviceCause = command[0].Parameters.Add("AdviceCause", SqlDbType.VarChar);
        p_AdviceCause.Direction = ParameterDirection.Input;
        p_AdviceCause.Value = strAdviceCause;

        SqlParameter p_WarningNo = command[0].Parameters.Add("WarningNo", DBNull.Value);
        p_WarningNo.Direction = ParameterDirection.Input;
        p_WarningNo.IsNullable = true;
        if (strWarningNo != "")
            p_WarningNo.Value = strWarningNo;

        SqlParameter p_InsertedBy = command[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsertedBy;

        SqlParameter p_InsertedDate = command[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsertedDate;

        SqlParameter p_IsUpdate = command[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command[0].Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        if (IsUpdate == "N")
        {
            //command[1] = InsertEmpInfoLog(strEmpId);

            //command[2] = UpdateEmpHRAction(strEmpId, strActionId, strEffectiveDate, strInsertedBy, strInsertedDate);

            command[3] = InsertEmpActionLog(strEmpId, strActionId,  strEffectiveDate, strInsertedBy, strInsertedDate);
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

    //public void InsertEmpTransitionLogUpdateEmpInfo(clsEmpTransition objEmpTransition, string strActionName, string strPreDivId, string strPreSBUId, string strPreLocID, string strPreDeptId,
    //            string strPreDesgId, string strPreGradeId, string strPreGradeLevelId, string strBasicSal, string strTransAmt, string strIsUpdate, string strSalPackId, DataTable dtSalPackUpdate)
    //{
    //    DBConnector objDC = new DBConnector();
    //    SqlCommand[] command = new SqlCommand[4 + dtSalPackUpdate.Rows.Count];
    //    command[0] = new SqlCommand("proc_Insert_EmpTransitionLog");
    //    command[0].CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_EmpID = command[0].Parameters.Add("EmpID", SqlDbType.Char);
    //    p_EmpID.Direction = ParameterDirection.Input;
    //    p_EmpID.Value = objEmpTransition.EmpID;

    //    SqlParameter p_TransitionId = command[0].Parameters.Add("TransitionId", SqlDbType.BigInt);
    //    p_TransitionId.Direction = ParameterDirection.Input;
    //    p_TransitionId.Value = objEmpTransition.TransitionId;

    //    SqlParameter p_TransitionType = command[0].Parameters.Add("TransitionType", SqlDbType.Char);
    //    p_TransitionType.Direction = ParameterDirection.Input;
    //    p_TransitionType.Value = objEmpTransition.TransitionType;

    //    SqlParameter p_DivisionID = command[0].Parameters.Add("DivisionID", SqlDbType.BigInt);
    //    p_DivisionID.Direction = ParameterDirection.Input;
    //    p_DivisionID.Value = strPreDivId;

    //    SqlParameter p_SbuId = command[0].Parameters.Add("SbuId", SqlDbType.BigInt);
    //    p_SbuId.Direction = ParameterDirection.Input;
    //    p_SbuId.Value = strPreSBUId;

    //    SqlParameter p_LocID = command[0].Parameters.Add("LocID", SqlDbType.BigInt);
    //    p_LocID.Direction = ParameterDirection.Input;
    //    p_LocID.Value = strPreLocID;

    //    SqlParameter p_DeptId = command[0].Parameters.Add("DeptId", SqlDbType.BigInt);
    //    p_DeptId.Direction = ParameterDirection.Input;
    //    p_DeptId.Value = strPreDeptId;

    //    SqlParameter p_DesgId = command[0].Parameters.Add("DesgId", SqlDbType.BigInt);
    //    p_DesgId.Direction = ParameterDirection.Input;
    //    p_DesgId.Value = strPreDesgId;

    //    SqlParameter p_GradeId = command[0].Parameters.Add("GradeId", SqlDbType.BigInt);
    //    p_GradeId.Direction = ParameterDirection.Input;
    //    p_GradeId.Value = strPreGradeId;

    //    SqlParameter p_GradeLevelId = command[0].Parameters.Add("GradeLevelId", SqlDbType.BigInt);
    //    p_GradeLevelId.Direction = ParameterDirection.Input;
    //    p_GradeLevelId.Value = strPreGradeLevelId;

    //    SqlParameter p_ActionId = command[0].Parameters.Add("ActionId", SqlDbType.BigInt);
    //    p_ActionId.Direction = ParameterDirection.Input;
    //    p_ActionId.Value = objEmpTransition.ActionId;

    //    SqlParameter p_EffDate = command[0].Parameters.Add("EffDate", DBNull.Value);
    //    p_EffDate.Direction = ParameterDirection.Input;
    //    p_EffDate.IsNullable = true;
    //    if (objEmpTransition.EffDate != "")
    //        p_EffDate.Value = objEmpTransition.EffDate;

    //    SqlParameter p_BasicSal = command[0].Parameters.Add("BasicSal", DBNull.Value);
    //    p_BasicSal.Direction = ParameterDirection.Input;
    //    p_BasicSal.IsNullable = true;
    //    if (strBasicSal != "")
    //        p_BasicSal.Value = strBasicSal;

    //    SqlParameter p_Transportation = command[0].Parameters.Add("Transportation", DBNull.Value);
    //    p_Transportation.Direction = ParameterDirection.Input;
    //    p_Transportation.IsNullable = true;
    //    if (strTransAmt != "")
    //        p_Transportation.Value = strTransAmt;

    //    SqlParameter p_SubDesigID = command[0].Parameters.Add("SubDesigID", DBNull.Value);
    //    p_SubDesigID.Direction = ParameterDirection.Input;
    //    p_SubDesigID.IsNullable = true;
    //    if (objEmpTransition.SubDesigId != "")
    //        p_SubDesigID.Value = objEmpTransition.SubDesigId;

    //    SqlParameter p_ContExpDate = command[0].Parameters.Add("ContExpDate", DBNull.Value);
    //    p_ContExpDate.Direction = ParameterDirection.Input;
    //    p_ContExpDate.IsNullable = true;
    //    if (objEmpTransition.ContactExpDate != "")
    //        p_ContExpDate.Value = objEmpTransition.ContactExpDate;

    //    SqlParameter p_PostindDate = command[0].Parameters.Add("PostindDate", DBNull.Value);
    //    p_PostindDate.Direction = ParameterDirection.Input;
    //    p_PostindDate.IsNullable = true;
    //    if (objEmpTransition.PostingDate != "")
    //        p_PostindDate.Value = objEmpTransition.PostingDate;

    //    SqlParameter p_DateInPosition = command[0].Parameters.Add("DateInPosition", DBNull.Value);
    //    p_DateInPosition.Direction = ParameterDirection.Input;
    //    p_DateInPosition.IsNullable = true;
    //    if (objEmpTransition.DateInPosition != "")
    //        p_DateInPosition.Value = objEmpTransition.DateInPosition;

    //    SqlParameter p_GradeEffDate = command[0].Parameters.Add("GradeEffDate", DBNull.Value);
    //    p_GradeEffDate.Direction = ParameterDirection.Input;
    //    p_GradeEffDate.IsNullable = true;
    //    if (objEmpTransition.GradeEffDate != "")
    //        p_GradeEffDate.Value = objEmpTransition.GradeEffDate;

    //    SqlParameter p_GradelevelDate = command[0].Parameters.Add("GradelevelDate", DBNull.Value);
    //    p_GradelevelDate.Direction = ParameterDirection.Input;
    //    p_GradelevelDate.IsNullable = true;
    //    if (objEmpTransition.GradeLevelDate != "")
    //        p_GradelevelDate.Value = objEmpTransition.GradeLevelDate;

    //    SqlParameter p_GRATUITYFROM = command[0].Parameters.Add("GRATUITYFROM", DBNull.Value);
    //    p_GRATUITYFROM.Direction = ParameterDirection.Input;
    //    p_GRATUITYFROM.IsNullable = true;
    //    if (objEmpTransition.GRATUITYFROM != "")
    //        p_GRATUITYFROM.Value = objEmpTransition.GRATUITYFROM;

    //    SqlParameter p_InsertedBy = command[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy.Direction = ParameterDirection.Input;
    //    p_InsertedBy.Value = objEmpTransition.InsertedBy;

    //    SqlParameter p_InsertedDate = command[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.Value = objEmpTransition.InsertedDate;

    //    SqlParameter p_IsUpdate = command[0].Parameters.Add("IsUpdate", SqlDbType.Char);
    //    p_IsUpdate.Direction = ParameterDirection.Input;
    //    p_IsUpdate.Value = strIsUpdate;

    //    if (strIsUpdate == "N")
    //    {
    //        command[1] = InsertEmpInfoLog(objEmpTransition.EmpID);

    //        command[2] = new SqlCommand("proc_Update_EmpInfoTransitionLog");
    //        command[2].CommandType = CommandType.StoredProcedure;

    //        SqlParameter p_EmpID1 = command[2].Parameters.Add("EmpID", SqlDbType.Char);
    //        p_EmpID1.Direction = ParameterDirection.Input;
    //        p_EmpID1.Value = objEmpTransition.EmpID;

    //        SqlParameter p_DivisionID1 = command[2].Parameters.Add("DivisionID", SqlDbType.BigInt);
    //        p_DivisionID1.Direction = ParameterDirection.Input;
    //        p_DivisionID1.Value = objEmpTransition.DivisionID;

    //        SqlParameter p_SbuId1 = command[2].Parameters.Add("SbuId", SqlDbType.BigInt);
    //        p_SbuId1.Direction = ParameterDirection.Input;
    //        p_SbuId1.Value = objEmpTransition.DeptId;

    //        SqlParameter p_LocID1 = command[2].Parameters.Add("LocID", SqlDbType.BigInt);
    //        p_LocID1.Direction = ParameterDirection.Input;
    //        p_LocID1.Value = objEmpTransition.LocID;

    //        SqlParameter p_DeptId1 = command[2].Parameters.Add("DeptId", SqlDbType.BigInt);
    //        p_DeptId1.Direction = ParameterDirection.Input;
    //        p_DeptId1.Value = objEmpTransition.SbuId;

    //        SqlParameter p_DesgId1 = command[2].Parameters.Add("DesgId", SqlDbType.BigInt);
    //        p_DesgId1.Direction = ParameterDirection.Input;
    //        p_DesgId1.Value = objEmpTransition.DesgId;

    //        SqlParameter p_GradeId1 = command[2].Parameters.Add("GradeId", SqlDbType.BigInt);
    //        p_GradeId1.Direction = ParameterDirection.Input;
    //        p_GradeId1.Value = objEmpTransition.GradeId;

    //        SqlParameter p_GradeLevelId1 = command[2].Parameters.Add("GradeLevelId", SqlDbType.BigInt);
    //        p_GradeLevelId1.Direction = ParameterDirection.Input;
    //        p_GradeLevelId1.Value = objEmpTransition.GradeLevelId;

    //        SqlParameter p_BasicSal1 = command[2].Parameters.Add("BasicSal", DBNull.Value);
    //        p_BasicSal1.Direction = ParameterDirection.Input;
    //        p_BasicSal1.IsNullable = true;
    //        if (objEmpTransition.BasicSal != "")
    //            p_BasicSal1.Value = objEmpTransition.BasicSal;

    //        SqlParameter p_Transportation11 = command[2].Parameters.Add("Transportation", DBNull.Value);
    //        p_Transportation11.Direction = ParameterDirection.Input;
    //        p_Transportation11.IsNullable = true;
    //        if (objEmpTransition.Transportation != "")
    //            p_Transportation11.Value = objEmpTransition.Transportation;

    //        SqlParameter p_SubDesigID2 = command[2].Parameters.Add("SubDesigID", DBNull.Value);
    //        p_SubDesigID2.Direction = ParameterDirection.Input;
    //        p_SubDesigID2.IsNullable = true;
    //        if (objEmpTransition.SubDesigId != "")
    //            p_SubDesigID2.Value = objEmpTransition.SubDesigId;

    //        SqlParameter p_ContExpDate2 = command[2].Parameters.Add("ContExpDate", DBNull.Value);
    //        p_ContExpDate2.Direction = ParameterDirection.Input;
    //        p_ContExpDate2.IsNullable = true;
    //        if (objEmpTransition.ContactExpDate != "")
    //            p_ContExpDate2.Value = objEmpTransition.ContactExpDate;

    //        SqlParameter p_PostindDate2 = command[2].Parameters.Add("PostindDate", DBNull.Value);
    //        p_PostindDate2.Direction = ParameterDirection.Input;
    //        p_PostindDate2.IsNullable = true;
    //        if (objEmpTransition.PostingDate != "")
    //            p_PostindDate2.Value = objEmpTransition.PostingDate;

    //        SqlParameter p_DateInPosition2 = command[2].Parameters.Add("DateInPosition", DBNull.Value);
    //        p_DateInPosition2.Direction = ParameterDirection.Input;
    //        p_DateInPosition2.IsNullable = true;
    //        if (objEmpTransition.DateInPosition != "")
    //            p_DateInPosition2.Value = objEmpTransition.DateInPosition;

    //        SqlParameter p_GradeEffDate2 = command[2].Parameters.Add("GradeEffDate", DBNull.Value);
    //        p_GradeEffDate2.Direction = ParameterDirection.Input;
    //        p_GradeEffDate2.IsNullable = true;
    //        if (objEmpTransition.GradeEffDate != "")
    //            p_GradeEffDate2.Value = objEmpTransition.GradeEffDate;

    //        SqlParameter p_GradelevelDate2 = command[2].Parameters.Add("GradelevelDate", DBNull.Value);
    //        p_GradelevelDate2.Direction = ParameterDirection.Input;
    //        p_GradelevelDate2.IsNullable = true;
    //        if (objEmpTransition.GradeLevelDate != "")
    //            p_GradelevelDate2.Value = objEmpTransition.GradeLevelDate;

    //        SqlParameter p_GratuityPaymentDate2 = command[2].Parameters.Add("GRATUITYFROM", DBNull.Value);
    //        p_GratuityPaymentDate2.Direction = ParameterDirection.Input;
    //        p_GratuityPaymentDate2.IsNullable = true;
    //        if (objEmpTransition.GRATUITYFROM != "")
    //            p_GratuityPaymentDate2.Value = objEmpTransition.GRATUITYFROM;

    //        SqlParameter p_ActionId2 = command[2].Parameters.Add("ActionId", SqlDbType.BigInt);
    //        p_ActionId2.Direction = ParameterDirection.Input;
    //        p_ActionId2.Value = objEmpTransition.ActionId;

    //        SqlParameter p_ActionDate = command[2].Parameters.Add("ActionDate", DBNull.Value);
    //        p_ActionDate.Direction = ParameterDirection.Input;
    //        p_ActionDate.IsNullable = true;
    //        if (objEmpTransition.EffDate != "")
    //            p_ActionDate.Value = objEmpTransition.EffDate;

    //        SqlParameter p_InsertedBy2 = command[2].Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //        p_InsertedBy2.Direction = ParameterDirection.Input;
    //        p_InsertedBy2.Value = objEmpTransition.InsertedBy;

    //        SqlParameter p_InsertedDate2 = command[2].Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //        p_InsertedDate2.Direction = ParameterDirection.Input;
    //        p_InsertedDate2.Value = objEmpTransition.InsertedDate;

    //        command[3] = InsertEmpActionLog(objEmpTransition.EmpID, objEmpTransition.ActionId, strActionName, objEmpTransition.EffDate, objEmpTransition.InsertedBy, objEmpTransition.InsertedDate);

    //        //Housing & PF & Transportation Allowance Update     
    //        int i = 4;
    //        if (dtSalPackUpdate.Rows.Count > 0)
    //        {
    //            foreach (SqlCommand cmdSal in this.GetSalPackDetUpdateCommand(dtSalPackUpdate, strSalPackId, objEmpTransition.InsertedBy, objEmpTransition.InsertedDate, "Transition"))
    //            {
    //                command[i] = cmdSal;
    //                i++;
    //            }
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

    //public DataTable SelectEmpTransitionLog(Int32 TransitionId, string EmpID)
    //{
    //    SqlCommand command = new SqlCommand("proc_Select_EmpTransitionLog");

    //    SqlParameter p_TransitionId = command.Parameters.Add("TransitionId", SqlDbType.BigInt);
    //    p_TransitionId.Direction = ParameterDirection.Input;
    //    p_TransitionId.Value = TransitionId;

    //    SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
    //    p_EmpID.Direction = ParameterDirection.Input;
    //    p_EmpID.Value = EmpID;


    //    objDC.CreateDSFromProc(command, "EmpTransitionLog");
    //    return objDC.ds.Tables["EmpTransitionLog"];
    //}

    public void InsertEmpSeparation(ClsEmpSeparation objEmp, string StrActionName, string strIsUpdate, string strIsDelete,
        string strSLYear, string strSLMonth, string strSLDay, string strTotdays, string strLPakId)
    {
        SqlCommand[] command = new SqlCommand[6];
        command[0] = new SqlCommand("proc_Insert_EmpSeparation");
        command[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpID = command[0].Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = objEmp.EmpID;

        SqlParameter p_SepId = command[0].Parameters.Add("SeparationId", SqlDbType.BigInt);
        p_SepId.Direction = ParameterDirection.Input;
        p_SepId.Value = objEmp.SeparationID;

        SqlParameter p_SepMode = command[0].Parameters.Add("ActionId", SqlDbType.BigInt);
        p_SepMode.Direction = ParameterDirection.Input;
        p_SepMode.Value = objEmp.SeparationMode;

        SqlParameter p_SepDate = command[0].Parameters.Add("SeparationDate", DBNull.Value);
        p_SepDate.Direction = ParameterDirection.Input;
        p_SepDate.IsNullable = true;
        if (objEmp.SeparationDate != "")
            p_SepDate.Value = objEmp.SeparationDate;

        SqlParameter p_PrevWorkDuration = command[0].Parameters.Add("PrevWorkDuration", SqlDbType.VarChar);
        p_PrevWorkDuration.Direction = ParameterDirection.Input;
        p_PrevWorkDuration.Value = objEmp.PrevWorkDuration;

        SqlParameter p_ReHiredStatus = command[0].Parameters.Add("ReHiredStatus", SqlDbType.VarChar);
        p_ReHiredStatus.Direction = ParameterDirection.Input;
        p_ReHiredStatus.Value = objEmp.ReHiredStatus;

        SqlParameter p_ReHiredCause = command[0].Parameters.Add("ReHiredCause", SqlDbType.VarChar);
        p_ReHiredCause.Direction = ParameterDirection.Input;
        p_ReHiredCause.Value = objEmp.ReHiredStatusCause;

        SqlParameter p_InsertedBy = command[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = objEmp.InsertedBy;

        SqlParameter p_InsertedDate = command[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = objEmp.InsertedDate;

        SqlParameter p_isUpdate = command[0].Parameters.Add("isUpdate", SqlDbType.Char);
        p_isUpdate.Direction = ParameterDirection.Input;
        p_isUpdate.Value = strIsUpdate;

        SqlParameter p_isDelete = command[0].Parameters.Add("isDelete", SqlDbType.Char);
        p_isDelete.Direction = ParameterDirection.Input;
        p_isDelete.Value = strIsDelete;

        if (strIsUpdate == "N")
        {
            //Insert on Table EmpInfoLog
           // command[1] = InsertEmpInfoLog(objEmp.EmpID);

            //Insert on Table EmpLeaveHisProfile
            if (strLPakId != "")
                command[2] = InsertEmpLeaveHisProfile(objEmp.EmpID, strLPakId, objEmp.InsertedBy, objEmp.InsertedDate);

            //Update Emp Separation Date
            command[3] = UpdateEmpHRActionAnStatus(objEmp.EmpID,"I",objEmp.SeparationMode, objEmp.SeparationDate, objEmp.InsertedBy, objEmp.InsertedDate);

            //Insert on Table EmpActionLog
            command[4] = InsertEmpActionLog(objEmp.EmpID, objEmp.SeparationMode,  objEmp.SeparationDate, objEmp.InsertedBy, objEmp.InsertedDate);

            //Update on Table EmpServiceAwardLength
            //command[5] = UpdateEmpServiceAwardLength(objEmp.EmpID, strSLYear, strSLMonth, strSLDay, strTotdays, objEmp.InsertedBy, objEmp.InsertedDate);
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

    //Update Employee Service Award Length
    private SqlCommand UpdateEmpServiceAwardLength(string strEmpId, string strSLYear, string strSLMonth, string strSLDay, string strTotDay,
        string strInsertedBy, string strInsertedDate)
    {
        string strSQL = "UPDATE EmpServiceAwardLength SET SLYear=" + strSLYear + ", SLMonth=" + strSLMonth + " ,SLDay=" + strSLDay + ",TotDays=" + strTotDay + ",UpdatedBy='" + strInsertedBy
            + "',UpdatedDate='" + strInsertedDate + "' WHERE EmpId='" + strEmpId + "'";

        SqlCommand command = new SqlCommand(strSQL);

        SqlParameter p_CourseId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_CourseId.Direction = ParameterDirection.Input;
        p_CourseId.Value = strEmpId;

        SqlParameter p_SLYear = command.Parameters.Add("SLYear", DBNull.Value);
        p_SLYear.Direction = ParameterDirection.Input;
        p_SLYear.IsNullable = true;
        if (strSLYear != "")
            p_SLYear.Value = strSLYear;

        SqlParameter p_SLMonth = command.Parameters.Add("SLMonth", DBNull.Value);
        p_SLMonth.Direction = ParameterDirection.Input;
        p_SLMonth.IsNullable = true;
        if (strSLMonth != "")
            p_SLMonth.Value = strSLMonth;

        SqlParameter p_SLDay = command.Parameters.Add("SLDay", DBNull.Value);
        p_SLDay.Direction = ParameterDirection.Input;
        p_SLDay.IsNullable = true;
        if (strSLDay != "")
            p_SLDay.Value = strSLDay;

        SqlParameter p_TotDays = command.Parameters.Add("TotDays", DBNull.Value);
        p_TotDays.Direction = ParameterDirection.Input;
        p_TotDays.IsNullable = true;
        if (strTotDay != "")
            p_TotDays.Value = strTotDay;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsertedDate;

        return command;
    }

    //Insert or update employee confirmation information
    //public void InsertConfirmation(string strConfirmId, string strEmpId, string strActionId, string strActionName,
    //    string strLPakId, string strConfirmDate, string strInsertedBy, string strInsertedDate, string IsUpdate,
    //    string IsDelete, string strSalPackId, DataTable dtSalPackUpdate)
    //{
    //    int i = 0;
    //    SqlCommand[] cmd1 = new SqlCommand[1];

    //    if (IsUpdate == "N")
    //    {
    //        //EmpLeaveProfileManager objLevProMgr = new EmpLeaveProfileManager();
    //        //cmd1 = objLevProMgr.UpdateConfirmLevProfile(strEmpId, strLPakId, strConfirmDate, strInsertedBy);
    //    }
    //    SqlCommand[] command = new SqlCommand[cmd1.Length + 4 + dtSalPackUpdate.Rows.Count];

    //    command[0] = new SqlCommand("proc_Insert_EmpConfirmationLog");
    //    command[0].CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_ConfirmId = command[0].Parameters.Add("ConfirmId", SqlDbType.BigInt);
    //    p_ConfirmId.Direction = ParameterDirection.Input;
    //    p_ConfirmId.Value = strConfirmId;

    //    SqlParameter p_EmpId = command[0].Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = strEmpId;

    //    SqlParameter p_ActionId = command[0].Parameters.Add("ActionId", SqlDbType.BigInt);
    //    p_ActionId.Direction = ParameterDirection.Input;
    //    p_ActionId.Value = strActionId;

    //    SqlParameter p_ConfirmDate = command[0].Parameters.Add("ConfirmDate", DBNull.Value);
    //    p_ConfirmDate.Direction = ParameterDirection.Input;
    //    p_ConfirmDate.IsNullable = true;
    //    if (strConfirmDate != "")
    //        p_ConfirmDate.Value = strConfirmDate;

    //    SqlParameter p_InsertedBy = command[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy.Direction = ParameterDirection.Input;
    //    p_InsertedBy.Value = strInsertedBy;

    //    SqlParameter p_InsertedDate = command[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.Value = strInsertedDate;

    //    SqlParameter p_IsUpdate = command[0].Parameters.Add("IsUpdate", SqlDbType.Char);
    //    p_IsUpdate.Direction = ParameterDirection.Input;
    //    p_IsUpdate.Value = IsUpdate;

    //    SqlParameter p_IsDelete = command[0].Parameters.Add("IsDelete", SqlDbType.Char);
    //    p_IsDelete.Direction = ParameterDirection.Input;
    //    p_IsDelete.Value = IsDelete;

    //    if (IsUpdate == "N")
    //    {
    //        //command[1] = InsertEmpInfoLog(strEmpId);

    //        command[2] = UpdateEmpHRActionConf(strEmpId, strConfirmDate, strInsertedBy, strInsertedDate);

    //        command[3] = InsertEmpActionLog(strEmpId, strActionId,  strConfirmDate, strInsertedBy, strInsertedDate);

    //        i = 4;
    //        foreach (SqlCommand cmdTemp in cmd1)
    //        {
    //            if (cmdTemp != null)
    //            {
    //                command[i] = cmdTemp;
    //                i++;
    //            }
    //        }

    //        //PF Allowance Update            
    //        if (dtSalPackUpdate.Rows.Count > 0)
    //        {
    //            foreach (SqlCommand cmdSal in this.GetSalPackDetUpdateCommand(dtSalPackUpdate, strSalPackId, strInsertedBy, strInsertedDate, "Confirmation"))
    //            {
    //                command[i] = cmdSal;
    //                i++;
    //            }
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

    private SqlCommand UpdateEmpHRActionConf(string strEmpId, string strActionId, string strActionDate, string strEmpTypeId, string strNewGrossSalary, string strNewBasicSalary,
      string strConfirmationDate, string strInsertedBy, string strInsertedDate)
    {
        string strSQL = "UPDATE EmpInfo SET EmpTypeId=" + strEmpTypeId + ",GrossSalary=" + strNewGrossSalary + ",BasicSalary=" + strNewBasicSalary +
            ",ConfirmationDate='" + strConfirmationDate + "',ActionId=" + strActionId + ",ActionDate='" + strActionDate +
            "',UpdatedBy='" + strInsertedBy + "',UpdatedDate='" + strInsertedDate + "' WHERE EmpId='" + strEmpId + "'";

        SqlCommand command = new SqlCommand(strSQL);

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_ActionId = command.Parameters.Add("ActionId", SqlDbType.BigInt);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.Value = strActionId;

        SqlParameter p_ActionDate = command.Parameters.Add("ActionDate", DBNull.Value);
        p_ActionDate.Direction = ParameterDirection.Input;
        p_ActionDate.IsNullable = true;
        if (strActionDate != "")
            p_ActionDate.Value = strActionDate;

        SqlParameter p_EmpTypeId = command.Parameters.Add("EmpTypeId", SqlDbType.BigInt);
        p_EmpTypeId.Direction = ParameterDirection.Input;
        p_EmpTypeId.Value = strEmpTypeId;

        SqlParameter p_GrossSalary = command.Parameters.Add("GrossSalary", SqlDbType.Decimal);
        p_GrossSalary.Direction = ParameterDirection.Input;
        p_GrossSalary.Value = strNewGrossSalary;

        SqlParameter p_BasicSalary = command.Parameters.Add("BasicSalary", SqlDbType.Decimal);
        p_BasicSalary.Direction = ParameterDirection.Input;
        p_BasicSalary.Value = strNewBasicSalary;

        SqlParameter p_ConfirmationDate = command.Parameters.Add("ConfirmationDate", DBNull.Value);
        p_ConfirmationDate.Direction = ParameterDirection.Input;
        p_ConfirmationDate.IsNullable = true;
        if (strConfirmationDate != "")
            p_ConfirmationDate.Value = strConfirmationDate;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsertedDate;

        return command;
    }

    private SqlCommand UpdateEmpHRActionAnStatus(string strEmpId, string strStatus,string strActionId,  string strActionDate, string strInsertedBy, string strInsertedDate)
    {
        string strSQL = "UPDATE EmpInfo SET EmpStatus='" + strStatus + "',SeparateTypeId=" + strActionId +
            ", SeparateDate='" + strActionDate + "',LeavePakId=null,ActionId=" + strActionId + 
            ",ActionDate='" + strActionDate +"',UpdatedBy='" +
            strInsertedBy + "',UpdatedDate='" + strInsertedDate + "' WHERE EmpId='" + strEmpId + "'";

        SqlCommand command = new SqlCommand(strSQL);

        SqlParameter p_CourseId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_CourseId.Direction = ParameterDirection.Input;
        p_CourseId.Value = strEmpId;

        SqlParameter p_SeparateTypeId = command.Parameters.Add("SeparateTypeId", SqlDbType.BigInt);
        p_SeparateTypeId.Direction = ParameterDirection.Input;
        p_SeparateTypeId.Value = strActionId;

        SqlParameter p_leavingDate = command.Parameters.Add("SeparateDate", DBNull.Value);
        p_leavingDate.Direction = ParameterDirection.Input;
        p_leavingDate.IsNullable = true;
        if (strActionDate != "")
            p_leavingDate.Value = strActionDate;

        SqlParameter p_ActionId = command.Parameters.Add("ActionId", SqlDbType.BigInt);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.Value = strActionId;

        SqlParameter p_ActionDate = command.Parameters.Add("ActionDate", DBNull.Value);
        p_ActionDate.Direction = ParameterDirection.Input;
        p_ActionDate.IsNullable = true;
        if (strActionDate != "")
            p_ActionDate.Value = strActionDate;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsertedDate;

        return command;
    }

    private SqlCommand InsertEmpLeaveHisProfile(string strEmpId, string strLPakId, string strInsertedBy, string strInsertedDate)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Proc_INSERT_EmpLeaveHisProfile");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter p_EmpID = cmd.Parameters.Add("ProfileEmpId", SqlDbType.Char);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = strEmpId;

            SqlParameter p_LPakId = cmd.Parameters.Add("LeavePakId", SqlDbType.BigInt);
            p_LPakId.Direction = ParameterDirection.Input;
            p_LPakId.Value = strLPakId;

            return cmd;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    //public DataTable SelectEmpSeparation(Int32 SeparationId, string EmpID)
    //{
    //    SqlCommand command = new SqlCommand("proc_Select_EmpSeparationLog");

    //    SqlParameter p_SeparationId = command.Parameters.Add("SeparationId", SqlDbType.BigInt);
    //    p_SeparationId.Direction = ParameterDirection.Input;
    //    p_SeparationId.Value = SeparationId;

    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = EmpID;

    //    objDC.CreateDSFromProc(command, "EmpSeparationLog");
    //    return objDC.ds.Tables["EmpSeparationLog"];
    //}

    //private SqlCommand UpdateEmpSalaryAmendment(string strEmpId, string strActionId, string strActionDate, string strBasicSal,
    // string strInsertedBy, string strInsertedDate)
    //{
    //    string strSQL = "UPDATE EmpInfo SET ActionId=" + strActionId + ",ActionDate='" + strActionDate + "',BasicSal=" + strBasicSal + ", UpdatedBy='" + strInsertedBy
    //        + "',UpdatedDate='" + strInsertedDate + "' WHERE EmpId='" + strEmpId + "'";

    //    SqlCommand command = new SqlCommand(strSQL);

    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = strEmpId;

    //    SqlParameter p_ActionId = command.Parameters.Add("ActionId", SqlDbType.BigInt);
    //    p_ActionId.Direction = ParameterDirection.Input;
    //    p_ActionId.Value = strActionId;

    //    SqlParameter p_ActionDate = command.Parameters.Add("ActionDate", DBNull.Value);
    //    p_ActionDate.Direction = ParameterDirection.Input;
    //    p_ActionDate.IsNullable = true;
    //    if (strActionDate != "")
    //        p_ActionDate.Value = strActionDate;

    //    SqlParameter p_BasicSal = command.Parameters.Add("BasicSal", DBNull.Value);
    //    p_BasicSal.Direction = ParameterDirection.Input;
    //    p_BasicSal.IsNullable = true;
    //    if (strBasicSal != "")
    //        p_BasicSal.Value = strBasicSal;

    //    SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy.Direction = ParameterDirection.Input;
    //    p_InsertedBy.Value = strInsertedBy;

    //    SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.Value = strInsertedDate;

    //    return command;
    //}

    //public DataTable SelectEmpReHiredlogLog(Int32 ReHiredId, string EmpID)
    //{
    //    SqlCommand command = new SqlCommand("proc_Select_EmpReHiredLog");

    //    SqlParameter p_ReHiredId = command.Parameters.Add("ReHiredId", SqlDbType.BigInt);
    //    p_ReHiredId.Direction = ParameterDirection.Input;
    //    p_ReHiredId.Value = ReHiredId;

    //    SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
    //    p_EmpID.Direction = ParameterDirection.Input;
    //    p_EmpID.Value = EmpID;

    //    objDC.CreateDSFromProc(command, "EmpReHiredLog");
    //    return objDC.ds.Tables["EmpReHiredLog"];
    //}

    public DataTable SelectEmpTempDuty(long TempDutyID, string EmpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpTempDutyAssignLog");
        SqlParameter p_DutyAssignID = command.Parameters.Add("DutyAssignID", SqlDbType.BigInt);
        p_DutyAssignID.Direction = ParameterDirection.Input;
        p_DutyAssignID.Value = TempDutyID;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDSFromProc(command, "EmpTempDutyAssignLog");
        return objDC.ds.Tables["EmpTempDutyAssignLog"];
    }

    public decimal GetHeadAmount(decimal dblAmount, decimal dblPercent)
    {
        decimal dblRetValue = 0;

        dblRetValue = (dblAmount * dblPercent) / 100;

        dblRetValue = Math.Round(dblRetValue, 0);

        return dblRetValue;
    }
   
    public DataTable SelectTransition(string EmpID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpInfo_Transition");
        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;
        objDC.CreateDSFromProc(command, "tran");
        return objDC.ds.Tables["tran"];
    }
      
    //Update EmpInfo Action Info
    private SqlCommand UpdateEmpHRAction(string strEmpId, string strActionId, string strActionDate, string strInsertedBy, string strInsertedDate)
    {
        string strSQL = "UPDATE EmpInfo SET ActionId=" + strActionId + ",ActionDate='" + strActionDate + "',UpdatedBy='" + strInsertedBy
            + "',UpdatedDate='" + strInsertedDate + "' WHERE EmpId='" + strEmpId + "'";

        SqlCommand command = new SqlCommand(strSQL);

        SqlParameter p_CourseId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_CourseId.Direction = ParameterDirection.Input;
        p_CourseId.Value = strEmpId;

        SqlParameter p_ActionId = command.Parameters.Add("ActionId", SqlDbType.BigInt);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.Value = strActionId;

        SqlParameter p_ActionDate = command.Parameters.Add("ActionDate", DBNull.Value);
        p_ActionDate.Direction = ParameterDirection.Input;
        p_ActionDate.IsNullable = true;
        if (strActionDate != "")
            p_ActionDate.Value = strActionDate;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsertedDate;

        return command;
    }

    private SqlCommand UpdateEmpProbationPeriodExtDate(string strEmpId, string strProExtensionDate, string strProbationPeriod,
    string strInsBy, string strInsDate)
    {
        string strSQL = "UPDATE EmpInfo SET ConfirmDueDate='" + strProExtensionDate + "',ValuationInterval=" + strProbationPeriod + ",UpdatedBy='" + strInsBy
            + "',UpdatedDate='" + strInsDate + "' WHERE EmpId='" + strEmpId + "'";

        SqlCommand command = new SqlCommand(strSQL);

        SqlParameter p_CourseId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_CourseId.Direction = ParameterDirection.Input;
        p_CourseId.Value = strEmpId;

        SqlParameter p_ProExtensionDate = command.Parameters.Add("ProExtensionDate", SqlDbType.DateTime);
        p_ProExtensionDate.Direction = ParameterDirection.Input;
        p_ProExtensionDate.Value = strProExtensionDate;

        SqlParameter p_ProbationPeriod = command.Parameters.Add("ProbationPeriod", DBNull.Value);
        p_ProbationPeriod.Direction = ParameterDirection.Input;
        p_ProbationPeriod.IsNullable = true;
        if (strProbationPeriod != "")
            p_ProbationPeriod.Value = strProbationPeriod;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        return command;
    }

    //Select Advice Warning information
    public DataTable SelectAdvice_Warning(Int32 AdviceId, string EmpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpAdvice_WarningLog");
        SqlParameter p_AdviceId = command.Parameters.Add("AdviceId", SqlDbType.BigInt);
        p_AdviceId.Direction = ParameterDirection.Input;
        p_AdviceId.Value = AdviceId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDSFromProc(command, "Advice_Warning");
        return objDC.ds.Tables["Advice_Warning"];
    }

    public void InsertReHiredLog(clsContractExt objReHired, string strPayEmpId, string strActionname, string strIsUpdate)
    {
        SqlCommand[] command = new SqlCommand[4];

        //EmpLeaveProfileManager objLevProMgr = new EmpLeaveProfileManager();
        //command = objLevProMgr.InsertIntoFirstJoinLevProfile(objReHired.EmpID, strLPakId, "", objReHired.InsertedBy);

        command[0] = new SqlCommand("proc_Insert_EmpReHiredLog");
        command[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpID = command[0].Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = objReHired.EmpID;

        SqlParameter p_ReHiredId = command[0].Parameters.Add("ReHiredId", SqlDbType.BigInt);
        p_ReHiredId.Direction = ParameterDirection.Input;
        p_ReHiredId.Value = objReHired.ReHiredId;
         

        SqlParameter p_ActionId = command[0].Parameters.Add("ActionId", DBNull.Value);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.IsNullable = true;
        if (objReHired.ActionId != "")
            p_ActionId.Value = objReHired.ActionId;        

        SqlParameter p_EffectiveDate = command[0].Parameters.Add("EffectiveDate", DBNull.Value);
        p_EffectiveDate.Direction = ParameterDirection.Input;
        p_EffectiveDate.IsNullable = true;
        if (objReHired.EffectiveDate != "")
            p_EffectiveDate.Value = objReHired.EffectiveDate;

        SqlParameter p_InsertedBy = command[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = objReHired.InsertedBy;

        SqlParameter p_InsertedDate = command[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = objReHired.InsertedDate;

        SqlParameter p_isUpdate = command[0].Parameters.Add("isUpdate", SqlDbType.Char);
        p_isUpdate.Direction = ParameterDirection.Input;
        p_isUpdate.Value = strIsUpdate;

        if (strIsUpdate == "N")
        {
            //command[1] = InsertEmpInfoLog(objReHired.EmpID);

            command[2] = UpdateEmpHRActionReHiredStatus(objReHired.EmpID, strPayEmpId, objReHired.ActionId, objReHired.EffectiveDate, objReHired.InsertedBy, objReHired.InsertedDate);

            EmpLeaveProfileManager objLevProMgr = new EmpLeaveProfileManager();
            //command[4] = objLevProMgr.InsertIntoEmpLevProfile(objReHired.EmpID, clEmp.LeavePakId, clEmp.JoiningDate, objReHired.InsertedBy, clEmp.EmpTypeID);
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

    public DataTable SelectEmpReHiredlog(Int32 LogId, string EmpId)
    {
        String strSql = "SELECT * FROM EmpRehiredLog WHere EmpId='" + EmpId + "'";
        SqlCommand cmd = new SqlCommand(strSql);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_LogId = cmd.Parameters.Add("LogId", SqlDbType.BigInt);
        p_LogId.Direction = ParameterDirection.Input;
        p_LogId.Value = LogId;

        SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        return objDC.CreateDT(strSql, "EmpReHiredlog");
    }
    ////Insert into Employee Log File
    //public SqlCommand InsertEmpInfoLog(string strEmpId)
    //{
    //    string strLogId = Common.getMaxId("EmpInfoLog", "LogId");
    //    string strSQL = "INSERT INTO EmpInfoLog SELECT " + strLogId + ",EmpId, EmpFname, EmpMName, EmpLName," +
    //        " FullName, BanglaName, FathersNm, MothersNm, PreCO, PreVill, PreUnion, PrePO, PrePostCode, PrePS," +
    //        " PreUpazillaID, PreDistrictID, PreCountryID, PrePhone, PreMobile, PerCO, PerVill, PerUnion, PerPO," +
    //        " PerPostCode, PerPS, PerUpazillaID, PerDistrictID, PerCountryID, PerPhone, PerMobile, DOB, Age, Sex," +
    //        " BloodGroup, MaritalStatus, MarriageDate, SpuseName, SpouseOccup, Religion, Nationality, PassportNo," +
    //        " PersMobile, PersEmail1, PersEmail2, Hobbies, MotherToung, AcomdType, LivingWith, FamiliAddInfo, SpecialNote," +
    //        " PResponsibility, SResponsibility, ExtComm, IntComm, DeptId, SectionID, DesgId, CardNo, RouteId, WeekEndID," +
    //        " AttnPolicyID, SalaryPakId, SalPakEffDate, BonusPakId, RecvAttBonus, LPakID, reportingTo, JoiningDate," +
    //        " ConfirmDueDate, EmpTypeID, EmpTypeStatus, EmpSubTypeStatus, NextValuationDate, ValuationInterval, Status," +
    //        " leavingDate, BankAccNo, ArearAmnt, IsRoaster, IsVisiRestricted, USERID, ISDELETED, InsertedBy, InsertedDate," +
    //        " UpdatedBy, UpdatedDate, LastUpdatedFrom, LocID, SbuId, DivisionID, GroupId, OTNOTALLOWED, OTNOTALLOWEDDATE," +
    //        " ISSEPERATED, SEPERATEDATE, SubGradeId, GradeId, EmpPhoto, EmpPicLoc, EmpAbbrName, TINNo, NationalCardId, PreAddress," +
    //        " IsDeptHead, IsShiftInchr, PerUpazillaName, srcsbuid, GradeEffDate, SalPakClsDate, RetirementDate, IsPayRollStaff," +
    //        " RenewalDate, PerLandPhone, ValidUnit, ValidDate, Cricle, Zone, EmperConPerson, EmperConNumber, SeparationType," +
    //        " EmpPrefix, EmpSuffix, IsCountryDirector, OFFICEPHONE, IsArearPaid, IsUnderCostRecovery, SubDesigID, GradeLevelId," +
    //        " IsRelativeExist, AppntLetterDate, PostindDate, DateInPosition, ContExpDate, ProbExtDate, PrevWorkDuration, Dependants," +
    //        " NoOfChildren, ConfirmationDate, PerAddress, ProbationPeriod, JoinAs, BasicSal, Transportation, GradelevelDate, Remarks," +
    //        " ActionId, ActionDate, BankCode, BranchCode, PlanAccLIne, MPCID, GRATUITYFROM, PayEmpID, ReverseName, AlterReportingTo," +
    //        " PARLOCID, FundCode, IsConfirmed, IsStopIncrement, RecFieldAllow, RecTransportationAllow, IsIntStaff,PayDeptId,FiscalYRID,IsCarAssigned" +
    //        " FROM EmpInfo WHERE EmpId='" + strEmpId + "'";

    //    SqlCommand command = new SqlCommand(strSQL);

    //    return command;
    //}

    private SqlCommand UpdateEmpHRActionReHiredStatus(string strEmpId, string strPayEmpId, string strActionId, string strActionDate, string strInsertedBy, string strInsertedDate)
    {
        string strSQL = "UPDATE EmpInfo SET SeparateTypeId=NULL,SeparateDate=NULL,ActionId=NULL,ActionDate=NULL,UpdatedBy='" + strInsertedBy
            + "',UpdatedDate='" + strInsertedDate + "',EmpStatus='A' WHERE EmpId='" + strEmpId + "'";

        SqlCommand command = new SqlCommand(strSQL);

        SqlParameter p_CourseId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_CourseId.Direction = ParameterDirection.Input;
        p_CourseId.Value = strEmpId;

        //SqlParameter p_ActionId = command.Parameters.Add("ActionId", SqlDbType.BigInt);
        //p_ActionId.Direction = ParameterDirection.Input;
        //p_ActionId.Value = strActionId;

        //SqlParameter p_ActionDate = command.Parameters.Add("ActionDate", DBNull.Value);
        //p_ActionDate.Direction = ParameterDirection.Input;
        //p_ActionDate.IsNullable = true;
        //if (strActionDate != "")
        //    p_ActionDate.Value = strActionDate;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsertedDate;

        return command;
    }

    ////Insert Employee ActionLog
    //public SqlCommand InsertEmpActionLog(string strEmpId, string strActionId, string strActionName, string strActionDate, string strInsertedBy, string strInsertedDate)
    //{
    //    SqlCommand command = new SqlCommand("proc_Insert_EmpActionLog");
    //    command.CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_LogId = command.Parameters.Add("LogId", SqlDbType.Char);
    //    p_LogId.Direction = ParameterDirection.Input;
    //    p_LogId.Value = Common.getMaxId("EmpActionLog", "LogId");

    //    SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
    //    p_EmpID.Direction = ParameterDirection.Input;
    //    p_EmpID.Value = strEmpId;

    //    SqlParameter p_ActionId = command.Parameters.Add("ActionId", SqlDbType.BigInt);
    //    p_ActionId.Direction = ParameterDirection.Input;
    //    p_ActionId.Value = strActionId;

    //    SqlParameter p_ActionDate = command.Parameters.Add("ActionDate", DBNull.Value);
    //    p_ActionDate.Direction = ParameterDirection.Input;
    //    p_ActionDate.IsNullable = true;
    //    if (strActionDate != "")
    //        p_ActionDate.Value = strActionDate;

    //    SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy.Direction = ParameterDirection.Input;
    //    p_InsertedBy.Value = strInsertedBy;

    //    SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.Value = strInsertedDate;

    //    return command;
    //}

    //Select Employee Gratuity Payment
    public DataTable SelectEmpGratuityPayment(Int32 LogId, string EmpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpGratuityPayment");
        SqlParameter p_LogId = command.Parameters.Add("LogId", SqlDbType.BigInt);
        p_LogId.Direction = ParameterDirection.Input;
        p_LogId.Value = LogId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDSFromProc(command, "tblEmpConfirmationLog");
        return objDC.ds.Tables["tblEmpConfirmationLog"];
    }

    //Insert or update Employee Gratuity Payment
    public void InsertEmpGratuityPayment(string strLogId, string strEmpId, string strActionId, string strActionName, string strActionDate,
       string strInsertedBy, string strInsertedDate, string IsUpdate, string IsDelete)
    {
        SqlCommand[] command = new SqlCommand[4];
        command[0] = new SqlCommand("proc_Insert_EmpGratuityPayment");
        command[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_ConfirmId = command[0].Parameters.Add("LogId", SqlDbType.BigInt);
        p_ConfirmId.Direction = ParameterDirection.Input;
        p_ConfirmId.Value = strLogId;

        SqlParameter p_EmpId = command[0].Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_ActionId = command[0].Parameters.Add("ActionId", SqlDbType.BigInt);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.Value = strActionId;

        SqlParameter p_ConfirmDate = command[0].Parameters.Add("ActionDate", DBNull.Value);
        p_ConfirmDate.Direction = ParameterDirection.Input;
        p_ConfirmDate.IsNullable = true;
        if (strActionDate != "")
            p_ConfirmDate.Value = strActionDate;

        SqlParameter p_InsertedBy = command[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsertedBy;

        SqlParameter p_InsertedDate = command[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsertedDate;

        SqlParameter p_IsUpdate = command[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command[0].Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        if (IsUpdate == "N")
        {
            //command[1] = InsertEmpInfoLog(strEmpId);

            //command[2] = UpdateEmpGratuityHRAction(strEmpId, strActionId, strActionDate, strInsertedBy, strInsertedDate);

            command[3] = InsertEmpActionLog(strEmpId, strActionId,  strActionDate, strInsertedBy, strInsertedDate);
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

    //Update EmpInfo Action Info
    private SqlCommand UpdateEmpGratuityHRAction(string strEmpId, string strActionId, string strActionDate, string strInsertedBy, string strInsertedDate)
    {
        string strSQL = "UPDATE EmpInfo SET ActionId='" + strActionId + "',ActionDate='" + strActionDate + "',GratuityFrom='" + strActionDate + "',UpdatedBy='" + strInsertedBy
            + "',UpdatedDate='" + strInsertedDate + "' WHERE EmpId='" + strEmpId + "'";

        SqlCommand command = new SqlCommand(strSQL);

        SqlParameter p_CourseId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_CourseId.Direction = ParameterDirection.Input;
        p_CourseId.Value = strEmpId;

        SqlParameter p_ActionId = command.Parameters.Add("ActionId", SqlDbType.BigInt);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.Value = strActionId;

        SqlParameter p_ActionDate = command.Parameters.Add("ActionDate", DBNull.Value);
        p_ActionDate.Direction = ParameterDirection.Input;
        p_ActionDate.IsNullable = true;
        if (strActionDate != "")
            p_ActionDate.Value = strActionDate;

        SqlParameter p_GratuityFrom = command.Parameters.Add("GratuityFrom", DBNull.Value);
        p_GratuityFrom.Direction = ParameterDirection.Input;
        p_GratuityFrom.IsNullable = true;
        if (strActionDate != "")
            p_GratuityFrom.Value = strActionDate;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsertedDate;

        return command;
    }

    public DataTable SelectEmpInfoSbuWiseForAction(string EmpID, string sbuID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpInfo_tab1sbuwise_ForAction");
        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        SqlParameter p_SbuId = command.Parameters.Add("SbuId", SqlDbType.BigInt);
        p_SbuId.Direction = ParameterDirection.Input;
        p_SbuId.Value = sbuID;

        objDC.CreateDSFromProc(command, "dtEmpInfoForAction");
        return objDC.ds.Tables["dtEmpInfoForAction"];
    }

    //Select Employee Salary Amendment
    public DataTable SelectEmpSalaryAmendment(Int32 LogId, string EmpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpSalaryAmendment");
        SqlParameter p_LogId = command.Parameters.Add("LogId", SqlDbType.BigInt);
        p_LogId.Direction = ParameterDirection.Input;
        p_LogId.Value = LogId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDSFromProc(command, "tblEmpSalaryAmendment");
        return objDC.ds.Tables["tblEmpSalaryAmendment"];
    }

    //Select Employee Salary Amendment
    public DataTable SelectEmpSalaryIncrement(Int32 LogId, string EmpId)
    {
        string strSQL = @"SELECT ESL.*,A.ActionName FROM EmpSalaryIncrementLog ESL, ActionList A WHERE ESL.ActionId=A.ActionID AND ESL.EmpId='" + EmpId + "'";

        return objDC.CreateDT(strSQL, "tblEmpSalaryIncrement");
    }

    public DataTable SelectEmpInfoWithAwardLength(string EmpID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpInfo_AwardLength");
        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        objDC.CreateDSFromProc(command, "EmpInfo");
        return objDC.ds.Tables["EmpInfo"];
    }

    public void InsertEmpAppointmentLogUpdateEmpInfo(clsEmpAppointmentLog objEmpAppointment, string strIsUpdate)
    {
        DBConnector objDC = new DBConnector();
        SqlCommand[] command = new SqlCommand[4];
        command[0] = new SqlCommand("proc_Insert_EmpAppointmentLog");
        command[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_AppointmentId = command[0].Parameters.Add("AppointmentId", SqlDbType.BigInt);
        p_AppointmentId.Direction = ParameterDirection.Input;
        p_AppointmentId.Value = objEmpAppointment.AppointmentId;

        SqlParameter p_EmpID = command[0].Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = objEmpAppointment.EmpId;

        SqlParameter p_ActionId = command[0].Parameters.Add("ActionId", DBNull.Value);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.IsNullable = true;
        if (objEmpAppointment.ActionId != "")
            p_ActionId.Value = objEmpAppointment.ActionId;

        SqlParameter p_EmpTypeID = command[0].Parameters.Add("EmpTypeID", DBNull.Value);
        p_EmpTypeID.Direction = ParameterDirection.Input;
        p_EmpTypeID.IsNullable = true;
        if (objEmpAppointment.EmpTypeID != "")
            p_EmpTypeID.Value = objEmpAppointment.EmpTypeID;

        SqlParameter p_GradeId = command[0].Parameters.Add("GradeId", DBNull.Value);
        p_GradeId.Direction = ParameterDirection.Input;
        p_GradeId.IsNullable = true;
        if (objEmpAppointment.GradeId != "")
            p_GradeId.Value = objEmpAppointment.GradeId;

        SqlParameter p_GradeLevelId = command[0].Parameters.Add("GradeLevelId", DBNull.Value);
        p_GradeLevelId.Direction = ParameterDirection.Input;
        p_GradeLevelId.IsNullable = true;
        if (objEmpAppointment.GradeLevelId != "")
            p_GradeLevelId.Value = objEmpAppointment.GradeLevelId;

        SqlParameter p_BasicSal = command[0].Parameters.Add("BasicSal", DBNull.Value);
        p_BasicSal.Direction = ParameterDirection.Input;
        p_BasicSal.IsNullable = true;
        if (objEmpAppointment.BasicSal != "")
            p_BasicSal.Value = objEmpAppointment.BasicSal;

        SqlParameter p_DesgId = command[0].Parameters.Add("DesgId", DBNull.Value);
        p_DesgId.Direction = ParameterDirection.Input;
        p_DesgId.IsNullable = true;
        if (objEmpAppointment.DesgId != "")
            p_DesgId.Value = objEmpAppointment.DesgId;

        SqlParameter p_SubDesigID = command[0].Parameters.Add("SubDesigID", DBNull.Value);
        p_SubDesigID.Direction = ParameterDirection.Input;
        p_SubDesigID.IsNullable = true;
        if (objEmpAppointment.SubDesigID != "")
            p_SubDesigID.Value = objEmpAppointment.SubDesigID;

        SqlParameter p_DeptId = command[0].Parameters.Add("DeptId", DBNull.Value);
        p_DeptId.Direction = ParameterDirection.Input;
        p_DeptId.IsNullable = true;
        if (objEmpAppointment.DeptId != "")
            p_DeptId.Value = objEmpAppointment.DeptId;

        SqlParameter p_DivisionID = command[0].Parameters.Add("DivisionID", DBNull.Value);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = objEmpAppointment.DivisionID;

        SqlParameter p_LocID = command[0].Parameters.Add("LocID", DBNull.Value);
        p_LocID.Direction = ParameterDirection.Input;
        p_LocID.IsNullable = true;
        if (objEmpAppointment.LocID != "")
            p_LocID.Value = objEmpAppointment.LocID;

        SqlParameter p_JoiningDate = command[0].Parameters.Add("JoiningDate", DBNull.Value);
        p_JoiningDate.Direction = ParameterDirection.Input;
        p_JoiningDate.IsNullable = true;
        if (objEmpAppointment.JoiningDate != "")
            p_JoiningDate.Value = objEmpAppointment.JoiningDate;

        SqlParameter p_ContExpDate = command[0].Parameters.Add("ContExpDate", DBNull.Value);
        p_ContExpDate.Direction = ParameterDirection.Input;
        p_ContExpDate.IsNullable = true;
        if (objEmpAppointment.ContExpDate != "")
            p_ContExpDate.Value = objEmpAppointment.ContExpDate;

        SqlParameter p_InsertedBy = command[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = objEmpAppointment.InsertedBy;

        SqlParameter p_InsertedDate = command[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = objEmpAppointment.InsertedDate;

        SqlParameter p_IsUpdate = command[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = strIsUpdate;

        if (strIsUpdate == "N")
        {
            command[1] = InsertEmpInfoLog(objEmpAppointment.EmpId);

            command[2] = UpdateEmpHRAppointment(objEmpAppointment);

            command[3] = InsertEmpActionLog(objEmpAppointment.EmpId, objEmpAppointment.ActionId, 
                objEmpAppointment.JoiningDate, objEmpAppointment.InsertedBy, objEmpAppointment.InsertedDate);
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

    private SqlCommand UpdateEmpHRAppointment(clsEmpAppointmentLog objEmpAppointment)
    {
        SqlCommand command = new SqlCommand("proc_Update_EmpInfo");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = objEmpAppointment.EmpId;

        SqlParameter p_ActionId = command.Parameters.Add("ActionId", DBNull.Value);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.IsNullable = true;
        if (objEmpAppointment.ActionId != "")
            p_ActionId.Value = objEmpAppointment.ActionId;

        SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", DBNull.Value);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.IsNullable = true;
        if (objEmpAppointment.DivisionID != "")
            p_DivisionID.Value = objEmpAppointment.DivisionID;

        SqlParameter p_DeptId = command.Parameters.Add("DeptId", DBNull.Value);
        p_DeptId.Direction = ParameterDirection.Input;
        p_DeptId.IsNullable = true;
        if (objEmpAppointment.DeptId != "")
            p_DeptId.Value = objEmpAppointment.DeptId;

        SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", DBNull.Value);
        p_EmpTypeID.Direction = ParameterDirection.Input;
        p_EmpTypeID.IsNullable = true;
        if (objEmpAppointment.EmpTypeID != "")
            p_EmpTypeID.Value = objEmpAppointment.EmpTypeID;

        SqlParameter p_LocID = command.Parameters.Add("LocID", DBNull.Value);
        p_LocID.Direction = ParameterDirection.Input;
        p_LocID.IsNullable = true;
        if (objEmpAppointment.LocID != "")
            p_LocID.Value = objEmpAppointment.LocID;

        SqlParameter p_GradeId = command.Parameters.Add("GradeId", DBNull.Value);
        p_GradeId.Direction = ParameterDirection.Input;
        p_GradeId.IsNullable = true;
        if (objEmpAppointment.GradeId != "")
            p_GradeId.Value = objEmpAppointment.GradeId;

        SqlParameter p_GradeLevelId = command.Parameters.Add("GradeLevelId", DBNull.Value);
        p_GradeLevelId.Direction = ParameterDirection.Input;
        p_GradeLevelId.IsNullable = true;
        if (objEmpAppointment.GradeLevelId != "")
            p_GradeLevelId.Value = objEmpAppointment.GradeLevelId;

        SqlParameter p_DesgId = command.Parameters.Add("DesgId", DBNull.Value);
        p_DesgId.Direction = ParameterDirection.Input;
        p_DesgId.IsNullable = true;
        if (objEmpAppointment.DesgId != "")
            p_DesgId.Value = objEmpAppointment.DesgId;

        SqlParameter p_SubDesigId = command.Parameters.Add("SubDesigId", DBNull.Value);
        p_SubDesigId.Direction = ParameterDirection.Input;
        p_SubDesigId.IsNullable = true;
        if (objEmpAppointment.SubDesigID != "")
            p_SubDesigId.Value = objEmpAppointment.SubDesigID;

        SqlParameter p_BasicSal = command.Parameters.Add("BasicSal", DBNull.Value);
        p_BasicSal.Direction = ParameterDirection.Input;
        p_BasicSal.IsNullable = true;
        if (objEmpAppointment.BasicSal != "")
            p_BasicSal.Value = objEmpAppointment.BasicSal;

        SqlParameter p_JoiningDate = command.Parameters.Add("JoiningDate", DBNull.Value);
        p_JoiningDate.Direction = ParameterDirection.Input;
        p_JoiningDate.IsNullable = true;
        if (objEmpAppointment.JoiningDate != "")
            p_JoiningDate.Value = objEmpAppointment.JoiningDate;

        SqlParameter p_ContExpDate = command.Parameters.Add("ContExpDate", DBNull.Value);
        p_ContExpDate.Direction = ParameterDirection.Input;
        p_ContExpDate.IsNullable = true;
        if (objEmpAppointment.ContExpDate != "")
            p_ContExpDate.Value = objEmpAppointment.ContExpDate;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = objEmpAppointment.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = objEmpAppointment.InsertedDate;

        return command;
    }

    #region Insert HR Action
    //Insert Additional Responsibility
    public void InsertAddResponsibility(string strId, string strEmpId, string strActionId, string strEntryDt, string strStartDt, string strEndDt,
        string strAmount, string strPercent, string strResponsibility, string strIsResponse, string strIsRepeat, string strInsBy, string strInsDate, string strIsUpdate)
    {
        SqlCommand[] command = new SqlCommand[3];

        command[0] = new SqlCommand("proc_Insert_EmpAddResponsibilityLog");
        command[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_AddResponseId = command[0].Parameters.Add("AddResponseId", SqlDbType.BigInt);
        p_AddResponseId.Direction = ParameterDirection.Input;
        p_AddResponseId.Value = strId;

        SqlParameter p_EmpId = command[0].Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_ActionId = command[0].Parameters.Add("ActionId", SqlDbType.BigInt);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.Value = strActionId;

        SqlParameter p_EntryDate = command[0].Parameters.Add("EntryDate", DBNull.Value);
        p_EntryDate.Direction = ParameterDirection.Input;
        p_EntryDate.IsNullable = true;
        if (strEntryDt != "")
            p_EntryDate.Value = strEntryDt;

        SqlParameter p_StartDate = command[0].Parameters.Add("StartDate", DBNull.Value);
        p_StartDate.Direction = ParameterDirection.Input;
        p_StartDate.IsNullable = true;
        if (strStartDt != "")
            p_StartDate.Value = strStartDt;

        SqlParameter p_EndDate = command[0].Parameters.Add("EndDate", DBNull.Value);
        p_EndDate.Direction = ParameterDirection.Input;
        p_EndDate.IsNullable = true;
        if (strEndDt != "")
            p_EndDate.Value = strEndDt;

        SqlParameter p_Amount = command[0].Parameters.Add("Amount", DBNull.Value);
        p_Amount.Direction = ParameterDirection.Input;
        p_Amount.IsNullable = true;
        if (strAmount != "")
            p_Amount.Value = strAmount;

        SqlParameter p_Percent = command[0].Parameters.Add("PercentAmt", DBNull.Value);
        p_Percent.Direction = ParameterDirection.Input;
        p_Percent.IsNullable = true;
        if (strPercent != "")
            p_Percent.Value = strPercent;

        SqlParameter p_Responsibility = command[0].Parameters.Add("Responsibility", SqlDbType.VarChar);
        p_Responsibility.Direction = ParameterDirection.Input;
        p_Responsibility.Value = strResponsibility;

        SqlParameter p_IsResponse = command[0].Parameters.Add("IsResponsibleAllowance", SqlDbType.Char);
        p_IsResponse.Direction = ParameterDirection.Input;
        p_IsResponse.Value = strIsResponse;

        SqlParameter p_IsRepeat = command[0].Parameters.Add("IsRepeat", SqlDbType.Char);
        p_IsRepeat.Direction = ParameterDirection.Input;
        p_IsRepeat.Value = strIsRepeat;

        SqlParameter p_InsertedBy = command[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = command[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        SqlParameter p_IsUpdate = command[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = strIsUpdate;

        command[1] = UpdateEmpHRAction(strEmpId, strActionId, strEntryDt, strInsBy, strInsDate);

        command[2] = InsertEmpActionLog(strEmpId, strActionId, strEntryDt, strInsBy, strInsDate);

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

    //Insert or update employee confirmation information
    public void InsertConfirmation(string strConfirmId, string strEmpId, string strActionId, string strEntryDate, string strProbationPeriod,
        string strConfirmDueDate, string strExtensionDate, string strConfirmDate, string strExtensionMonth, string strRemarks, string strInsBy, string strInsDate, string IsUpdate,
        string strSalPackId, DataTable dtSalPackUpdate, string strIsLeaveUpdate,string strJoinDate,string strLeavePakId,string strEmpTypeId,string strNewGrossSal,string strNewBasicSal)
    {

        int i = 0;
        SqlCommand[] cmd1 = new SqlCommand[1];       

        ////if (strIsLeaveUpdate == "Y")
        ////{
        ////    EmpLeaveProfileManager objLevProMgr = new EmpLeaveProfileManager();
        ////    cmd1 = objLevProMgr.UpdateConfirmLevProfile(strEmpId, strLeavePakId, strConfirmDate,strJoinDate, strInsBy);
        ////}

        SqlCommand[] command = new SqlCommand[cmd1.Length + 6 + dtSalPackUpdate.Rows.Count];
        command[0] = new SqlCommand("proc_Insert_EmpConfirmationLog");
        command[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_ConfirmId = command[0].Parameters.Add("ConfirmId", SqlDbType.BigInt);
        p_ConfirmId.Direction = ParameterDirection.Input;
        p_ConfirmId.Value = strConfirmId;

        SqlParameter p_EmpId = command[0].Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_ActionId = command[0].Parameters.Add("ActionId", SqlDbType.BigInt);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.Value = strActionId;

        SqlParameter p_EntryDate = command[0].Parameters.Add("EntryDate", DBNull.Value);
        p_EntryDate.Direction = ParameterDirection.Input;
        p_EntryDate.IsNullable = true;
        if (strEntryDate != "")
            p_EntryDate.Value = strEntryDate;

        SqlParameter p_EmpTypeId = command[0].Parameters.Add("EmpTypeId", SqlDbType.BigInt);
        p_EmpTypeId.Direction = ParameterDirection.Input;
        p_EmpTypeId.Value = strEmpTypeId;

        SqlParameter p_NewGrossSal = command[0].Parameters.Add("NewGrossSalary", SqlDbType.Decimal);
        p_NewGrossSal.Direction = ParameterDirection.Input;
        p_NewGrossSal.Value = strNewGrossSal;

        SqlParameter p_ProbationPeriod = command[0].Parameters.Add("ProbationPeriod", DBNull.Value);
        p_ProbationPeriod.Direction = ParameterDirection.Input;
        p_ProbationPeriod.IsNullable = true;
        if (strProbationPeriod != "")
            p_ProbationPeriod.Value = strProbationPeriod;

        SqlParameter p_ConfirmDueDate = command[0].Parameters.Add("ConfirmDueDate", DBNull.Value);
        p_ConfirmDueDate.Direction = ParameterDirection.Input;
        p_ConfirmDueDate.IsNullable = true;
        if (strConfirmDueDate != "")
            p_ConfirmDueDate.Value = strConfirmDueDate;

        SqlParameter p_ExtensionDate = command[0].Parameters.Add("ExtensionDate", DBNull.Value);
        p_ExtensionDate.Direction = ParameterDirection.Input;
        p_ExtensionDate.IsNullable = true;
        if (strExtensionDate != "")
            p_ExtensionDate.Value = strExtensionDate;

        SqlParameter p_ConfirmDate = command[0].Parameters.Add("ConfirmDate", DBNull.Value);
        p_ConfirmDate.Direction = ParameterDirection.Input;
        p_ConfirmDate.IsNullable = true;
        if (strConfirmDate != "")
            p_ConfirmDate.Value = strConfirmDate;

        SqlParameter p_ExtensionMonth = command[0].Parameters.Add("ExtensionMonth", DBNull.Value);
        p_ExtensionMonth.Direction = ParameterDirection.Input;
        p_ExtensionMonth.IsNullable = true;
        if (strExtensionMonth != "")
            p_ExtensionMonth.Value = strExtensionMonth;

        SqlParameter p_Remarks = command[0].Parameters.Add("Remarks", SqlDbType.VarChar);
        p_Remarks.Direction = ParameterDirection.Input;
        p_Remarks.Value = strRemarks;

        SqlParameter p_InsertedBy = command[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = command[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        SqlParameter p_IsUpdate = command[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        if (IsUpdate == "N")
        {
            //command[1] = InsertEmpInfoLog(strEmpId);
            if (strEmpTypeId == "1")
                command[2] = UpdateEmpHRActionConf(strEmpId, strActionId, strEntryDate, strEmpTypeId, strNewGrossSal, strNewBasicSal, strConfirmDate, strInsBy, strInsDate);
            else
                command[2] = UpdateEmpHRProbationExt(strEmpId, strActionId, strEntryDate, strNewGrossSal, strNewBasicSal, strProbationPeriod, strInsBy, strInsDate);

            command[3] = InsertEmpActionLog(strEmpId, strActionId, strConfirmDate, strInsBy, strInsDate);

            
            if (string.IsNullOrEmpty(GetEmpIdWsSalHisPakId(strSalPackId, strEmpId)) == true)
                command[4] = InsertSalaryPakHisDetls(strSalPackId, strEmpId, strEntryDate,strInsBy, strInsDate, "Salary Package");

            i = 5;
            if (dtSalPackUpdate.Rows.Count > 0)
            {
                foreach (SqlCommand cmdSal in this.GetSalPackDetUpdateCommand(dtSalPackUpdate, strSalPackId, strInsBy, strInsDate, "Confirmation"))
                {
                    command[i] = cmdSal;
                    i++;
                }
            }
            i++;

            command[i] = InsertSalaryPakHisDetls(strSalPackId, strEmpId, strConfirmDate, strInsBy, strInsDate, "Confirmation");
        }

        foreach (SqlCommand cmdTemp in cmd1)
        {
            if (cmdTemp != null)
            {
                command[i] = cmdTemp;
            }
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

    public string GetEmpIdWsSalHisPakId(string strSalPakId,string strEmpId)
    {
        string strSPakId = "";

        string strSql = "SELECT SalPakId FROM SalaryPakHisDetls Where SalPakId=" + strSalPakId + " AND EmpId='" + strEmpId + "'" ;

        SqlCommand cmd = new SqlCommand(strSql);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_SalPakId = cmd.Parameters.Add("SalPakId", SqlDbType.VarChar);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = strSalPakId;

        SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        strSPakId = objDC.GetScalarVal(cmd);

        return strSPakId;
    }

    public void InsertContractExtension(clsContractExt objContExt, string strActionname, string strIsUpdate)
    {
        SqlCommand[] command = new SqlCommand[4];
        command[0] = new SqlCommand("proc_Insert_EmpContractExtLog");
        command[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpID = command[0].Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = objContExt.EmpID;

        SqlParameter p_ContractExtId = command[0].Parameters.Add("ContractExtId", SqlDbType.BigInt);
        p_ContractExtId.Direction = ParameterDirection.Input;
        p_ContractExtId.Value = objContExt.ContractExtId;

        SqlParameter p_ActionId = command[0].Parameters.Add("ActionId", SqlDbType.BigInt);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.Value = objContExt.ActionId;

        SqlParameter p_EffectiveDate = command[0].Parameters.Add("EffectiveDate", DBNull.Value);
        p_EffectiveDate.Direction = ParameterDirection.Input;
        p_EffectiveDate.IsNullable = true;
        if (objContExt.EffectiveDate != "")
            p_EffectiveDate.Value = objContExt.EffectiveDate;

        SqlParameter p_ContractExtDate = command[0].Parameters.Add("ContractExtDate", DBNull.Value);
        p_ContractExtDate.Direction = ParameterDirection.Input;
        p_ContractExtDate.IsNullable = true;
        if (objContExt.ContractExtDate != "")
            p_ContractExtDate.Value = objContExt.ContractExtDate;

        SqlParameter p_InsertedBy = command[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = objContExt.InsertedBy;

        SqlParameter p_InsertedDate = command[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = objContExt.InsertedDate;

        SqlParameter p_isUpdate = command[0].Parameters.Add("isUpdate", SqlDbType.Char);
        p_isUpdate.Direction = ParameterDirection.Input;
        p_isUpdate.Value = strIsUpdate;

        if (strIsUpdate == "N")
        {
            //command[1] = InsertEmpInfoLog(objContExt.EmpID);

            command[2] = UpdateEmpHRContactExt(objContExt.EmpID, objContExt.ActionId, objContExt.EffectiveDate, objContExt.ContractExtDate, objContExt.InsertedBy, objContExt.InsertedDate);

            command[3] = InsertEmpActionLog(objContExt.EmpID, objContExt.ActionId, objContExt.EffectiveDate, objContExt.InsertedBy, objContExt.InsertedDate);
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

    private SqlCommand InsertEmpMedicalBalance(string strEmpId, string strJoiningDate, string strInsertedBy, string strInsertedDate)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();

            DateTime JoinYear;
            //DateTime CurrYear =Convert.ToDateTime("2015-01-01");

            DateTime CurrYear = DateTime.Now;
            CurrYear = Convert.ToDateTime(DateTime.Now.Year + "-01-01");

            decimal tempMedicalValue = 0;
            decimal tempHospitalValue = 0;
            decimal dclMedicalBal = 0;
            decimal dclHospitalBal = 0;
            decimal dclMedicalCost = 0;
            decimal dclHospitalCost = 0;
            if (strJoiningDate == "")
            {
                JoinYear = CurrYear;
            }
            else
            {
                JoinYear = Convert.ToDateTime(strJoiningDate);
            }


            if (JoinYear > CurrYear)
            {
                TimeSpan DateDiff = JoinYear - CurrYear;
                string strTotDay = Common.ReturnTotalDay(DateDiff.ToString());
                tempMedicalValue = (Convert.ToDecimal("35000") / 366);
                tempMedicalValue = (tempMedicalValue * (366 - Convert.ToInt32(strTotDay)));
                dclMedicalBal = Convert.ToInt32(tempMedicalValue);
                dclMedicalCost = dclMedicalBal;

                tempHospitalValue = (Convert.ToDecimal("40000") / 366);
                tempHospitalValue = (tempHospitalValue * (366 - Convert.ToInt32(strTotDay)));
                dclHospitalBal = Convert.ToInt32(tempHospitalValue);
                dclHospitalCost = dclHospitalBal;
            }
            else
            {
                dclMedicalCost = Convert.ToDecimal("35000");
                dclHospitalCost = Convert.ToDecimal("40000");
            }            
            
            cmd = InsertEmpMedicalBalanceSetup(strEmpId, dclMedicalCost,dclHospitalCost, strInsertedBy, strInsertedDate);

            return cmd;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    public SqlCommand InsertEmpMedicalBalanceSetup(string strEmpId, decimal dclMedicalCost,decimal dclHospitalCost, string strInsertedBy, string strInsertedDate)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_Insert_MedicalSetup");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.VarChar);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = strEmpId;

            SqlParameter p_FiscalYrId = cmd.Parameters.Add("MedFiscalYrId", SqlDbType.BigInt);
            p_FiscalYrId.Direction = ParameterDirection.Input;
            p_FiscalYrId.Value = "40";

            SqlParameter p_MedicalCost= cmd.Parameters.Add("MedicalCost", SqlDbType.Decimal);
            p_MedicalCost.Direction = ParameterDirection.Input;
            p_MedicalCost.Value = dclMedicalCost;

            SqlParameter p_HospitalCost = cmd.Parameters.Add("HospitalCost", SqlDbType.Decimal);
            p_HospitalCost.Direction = ParameterDirection.Input;
            p_HospitalCost.Value = dclHospitalCost;

            SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = strInsertedBy;

            SqlParameter p_InsertedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate.Direction = ParameterDirection.Input;
            p_InsertedDate.Value = strInsertedDate;

            return cmd;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    ////Insert into Employee Log File
    //public SqlCommand InsertEmpInfoLog(string strEmpId)
    //{
    //    string strLogId = Common.getMaxId("EmpInfoLog", "LogId");
    //    string strSQL = "INSERT INTO EmpInfoLog SELECT " + strLogId + ",EmpId, EmpFname, EmpMName, EmpLName," +
    //        " FullName, BanglaName, FathersNm, MothersNm, PreCO, PreVill, PreUnion, PrePO, PrePostCode, PrePS," +
    //        " PreUpazillaID, PreDistrictID, PreCountryID, PrePhone, PreMobile, PerCO, PerVill, PerUnion, PerPO," +
    //        " PerPostCode, PerPS, PerUpazillaID, PerDistrictID, PerCountryID, PerPhone, PerMobile, DOB, Age, Sex," +
    //        " BloodGroup, MaritalStatus, MarriageDate, SpuseName, SpouseOccup, Religion, Nationality, PassportNo," +
    //        " PersMobile, PersEmail1, PersEmail2, Hobbies, MotherToung, AcomdType, LivingWith, FamiliAddInfo, SpecialNote," +
    //        " PResponsibility, SResponsibility, ExtComm, IntComm, DeptId, SectionID, DesgId, CardNo, RouteId, WeekEndID," +
    //        " AttnPolicyID, SalaryPakId, SalPakEffDate, BonusPakId, RecvAttBonus, LPakID, reportingTo, JoiningDate," +
    //        " ConfirmDueDate, EmpTypeID, EmpTypeStatus, EmpSubTypeStatus, NextValuationDate, ValuationInterval, Status," +
    //        " leavingDate, BankAccNo, ArearAmnt, IsRoaster, IsVisiRestricted, USERID, ISDELETED, InsertedBy, InsertedDate," +
    //        " UpdatedBy, UpdatedDate, LastUpdatedFrom, LocID, SbuId, DivisionID, GroupId, OTNOTALLOWED, OTNOTALLOWEDDATE," +
    //        " ISSEPERATED, SEPERATEDATE, SubGradeId, GradeId, EmpPhoto, EmpPicLoc, EmpAbbrName, TINNo, NationalCardId, PreAddress," +
    //        " IsDeptHead, IsShiftInchr, PerUpazillaName, srcsbuid, GradeEffDate, SalPakClsDate, RetirementDate, IsPayRollStaff," +
    //        " RenewalDate, PerLandPhone, ValidUnit, ValidDate, Cricle, Zone, EmperConPerson, EmperConNumber, SeparationType," +
    //        " EmpPrefix, EmpSuffix, IsCountryDirector, OFFICEPHONE, IsArearPaid, IsUnderCostRecovery, SubDesigID, GradeLevelId," +
    //        " IsRelativeExist, AppntLetterDate, PostindDate, DateInPosition, ContExpDate, ProbExtDate, PrevWorkDuration, Dependants," +
    //        " NoOfChildren, ConfirmationDate, PerAddress, ProbationPeriod, JoinAs, BasicSal, Transportation, GradelevelDate, Remarks," +
    //        " ActionId, ActionDate, BankCode, BranchCode, PlanAccLIne, MPCID, GRATUITYFROM, PayEmpID, ReverseName, AlterReportingTo," +
    //        " PARLOCID, FundCode, IsConfirmed, IsStopIncrement, RecFieldAllow, RecTransportationAllow, IsIntStaff,PayDeptId,FiscalYRID,IsCarAssigned" +
    //        " FROM EmpInfo WHERE EmpId='" + strEmpId + "'";

    //    SqlCommand command = new SqlCommand(strSQL);

    //    return command;
    //}

    //private SqlCommand UpdateEmpHRActionConf(string strEmpId, string strConfirmationDate, string strInsertedBy, string strInsertedDate)
    //{
    //    string strSQL = "UPDATE EmpInfo SET ConfirmDueDate='" + strConfirmationDate +
    //        "', UpdatedBy='" + strInsertedBy + "',UpdatedDate='" + strInsertedDate + "' WHERE EmpId='" + strEmpId + "'";

    //    SqlCommand command = new SqlCommand(strSQL);

    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = strEmpId;

    //    SqlParameter p_ConfirmationDate = command.Parameters.Add("ConfirmationDate", DBNull.Value);
    //    p_ConfirmationDate.Direction = ParameterDirection.Input;
    //    p_ConfirmationDate.IsNullable = true;
    //    if (strConfirmationDate != "")
    //        p_ConfirmationDate.Value = strConfirmationDate;

    //    SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy.Direction = ParameterDirection.Input;
    //    p_InsertedBy.Value = strInsertedBy;

    //    SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.Value = strInsertedDate;

    //    return command;
    //}

    ////Insert Employee ActionLog
    //public SqlCommand InsertEmpActionLog(string strEmpId, string strActionId, string strActionName, string strActionDate, string strInsertedBy, string strInsertedDate)
    //{
    //    SqlCommand command = new SqlCommand("proc_Insert_EmpAdvice_ActionLog");
    //    command.CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_LogId = command.Parameters.Add("LogId", SqlDbType.Char);
    //    p_LogId.Direction = ParameterDirection.Input;
    //    p_LogId.Value = Common.getMaxId("EmpActionLog", "LogId");

    //    SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
    //    p_EmpID.Direction = ParameterDirection.Input;
    //    p_EmpID.Value = strEmpId;

    //    SqlParameter p_ActionId = command.Parameters.Add("ActionId", SqlDbType.BigInt);
    //    p_ActionId.Direction = ParameterDirection.Input;
    //    p_ActionId.Value = strActionId;

    //    SqlParameter p_ActionDate = command.Parameters.Add("ActionDate", DBNull.Value);
    //    p_ActionDate.Direction = ParameterDirection.Input;
    //    p_ActionDate.IsNullable = true;
    //    if (strActionDate != "")
    //        p_ActionDate.Value = strActionDate;

    //    SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy.Direction = ParameterDirection.Input;
    //    p_InsertedBy.Value = strInsertedBy;

    //    SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.Value = strInsertedDate;

    //    return command;
    //}

    //public SqlCommand[] GetSalPackDetUpdateCommand(DataTable dtSalPackUpdate, string StrSalPakId, string strInsBy, string strInsDate, string strLastUpdatedFrom)
    //{
    //    SqlCommand[] cmd = new SqlCommand[dtSalPackUpdate.Rows.Count];
    //    int i = 0;

    //    foreach (DataRow dRow in dtSalPackUpdate.Rows)
    //    {
    //        cmd[i] = new SqlCommand("proc_Payroll_Update_SalaryPackDetls");
    //        cmd[i].CommandType = CommandType.StoredProcedure;

    //        SqlParameter p_SalPakId = cmd[i].Parameters.Add("SalPakId", SqlDbType.BigInt);
    //        p_SalPakId.Direction = ParameterDirection.Input;
    //        p_SalPakId.Value = StrSalPakId;

    //        SqlParameter p_SHeadId = cmd[i].Parameters.Add("SHeadId", SqlDbType.BigInt);
    //        p_SHeadId.Direction = ParameterDirection.Input;
    //        p_SHeadId.Value = dRow["SHeadId"];

    //        SqlParameter p_PayAmt = cmd[i].Parameters.Add("PayAmt", SqlDbType.Decimal);
    //        p_PayAmt.Direction = ParameterDirection.Input;
    //        p_PayAmt.Value = dRow["PayAmt"];

    //        SqlParameter p_TotAmnt = cmd[i].Parameters.Add("TotAmnt", SqlDbType.Decimal);
    //        p_TotAmnt.Direction = ParameterDirection.Input;
    //        p_TotAmnt.Value = dRow["PayAmt"];

    //        SqlParameter p_InsertedBy = cmd[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //        p_InsertedBy.Direction = ParameterDirection.Input;
    //        p_InsertedBy.Value = strInsBy;

    //        SqlParameter p_UpdatedDate = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //        p_UpdatedDate.Direction = ParameterDirection.Input;
    //        p_UpdatedDate.Value = strInsDate;

    //        SqlParameter p_LastUpdatedFrom = cmd[i].Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
    //        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
    //        p_LastUpdatedFrom.Value = strLastUpdatedFrom;

    //        i++;
    //    }
    //    return cmd;
    //}

    //Insert Disciplinary Responsibility
    public void InsertDisciplinary(string strId, string strEmpId, string strEntryDt, string strActionId, string strReason, string strActionDate,
        string strReviewDate, string strIsReview, string strIsSuspendInc, string strRemarks, string strInsBy, string strInsDate, string strIsUpdate)
    {
        SqlCommand[] command = new SqlCommand[3];
        command[0] = new SqlCommand("proc_Insert_EmpDisciplinaryLog");
        command[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_DisciplinaryId = command[0].Parameters.Add("DisciplinaryId", SqlDbType.BigInt);
        p_DisciplinaryId.Direction = ParameterDirection.Input;
        p_DisciplinaryId.Value = strId;

        SqlParameter p_EmpId = command[0].Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_EntryDate = command[0].Parameters.Add("EntryDate", DBNull.Value);
        p_EntryDate.Direction = ParameterDirection.Input;
        p_EntryDate.IsNullable = true;
        if (strEntryDt != "")
            p_EntryDate.Value = strEntryDt;

        SqlParameter p_ActionId = command[0].Parameters.Add("ActionId", SqlDbType.BigInt);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.Value = strActionId;

        SqlParameter p_Reason = command[0].Parameters.Add("ReasonId", SqlDbType.BigInt);
        p_Reason.Direction = ParameterDirection.Input;
        p_Reason.Value = strReason;

        SqlParameter p_ActionDate = command[0].Parameters.Add("ActionDate", DBNull.Value);
        p_ActionDate.Direction = ParameterDirection.Input;
        p_ActionDate.IsNullable = true;
        if (strActionDate != "")
            p_ActionDate.Value = strActionDate;

        SqlParameter p_ReviewDate = command[0].Parameters.Add("ReviewDate", DBNull.Value);
        p_ReviewDate.Direction = ParameterDirection.Input;
        p_ReviewDate.IsNullable = true;
        if (strReviewDate != "")
            p_ReviewDate.Value = strReviewDate;

        SqlParameter p_IsReview = command[0].Parameters.Add("IsReview", SqlDbType.Char);
        p_IsReview.Direction = ParameterDirection.Input;
        p_IsReview.Value = strIsReview;

        SqlParameter p_IsSuspendInc = command[0].Parameters.Add("IsSuspendInc", SqlDbType.Char);
        p_IsSuspendInc.Direction = ParameterDirection.Input;
        p_IsSuspendInc.Value = strIsSuspendInc;

        SqlParameter p_Remarks = command[0].Parameters.Add("Remarks", SqlDbType.VarChar);
        p_Remarks.Direction = ParameterDirection.Input;
        p_Remarks.Value = strRemarks;

        SqlParameter p_InsertedBy = command[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = command[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        SqlParameter p_IsUpdate = command[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = strIsUpdate;

        command[1] = UpdateEmpHRAction(strEmpId, strActionId, strEntryDt, strInsBy, strInsDate);

        command[2] = InsertEmpActionLog(strEmpId, strActionId, strEntryDt, strInsBy, strInsDate);

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

    public void InsertEmpTransitionLog(clsEmpTransition objEmpTrans, string strPreDivId, string strPreClinicId, string strPreProjectId, 
        string strPreDeptId, string strPreSubDeptId, string strPreDesigId, string strPreGradeId, string strPreSalLocId, string strPreEmpTypeId,
        string strPreBasicSal, string strPreEffDate, string strPreNextIncDate, string strGradeChDate, string strRetirementDate,
        string strPreRemarks, string strIsUpdate, string strSalPackId, string strPreGrossSalary, DataTable dtSalPackUpdate)
    {
        DBConnector objDC = new DBConnector();
        SqlCommand[] command = new SqlCommand[7 + dtSalPackUpdate.Rows.Count];
        command[0] = new SqlCommand("proc_Insert_EmpTransitionLog");
        command[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_TransId = command[0].Parameters.Add("TransId", SqlDbType.BigInt);
        p_TransId.Direction = ParameterDirection.Input;
        p_TransId.Value = objEmpTrans.TransId;

        SqlParameter p_EmpID = command[0].Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = objEmpTrans.EmpID;

        SqlParameter p_EntryDate = command[0].Parameters.Add("EntryDate", DBNull.Value);
        p_EntryDate.Direction = ParameterDirection.Input;
        p_EntryDate.IsNullable = true;
        if (objEmpTrans.EntryDate != "")
            p_EntryDate.Value = objEmpTrans.EntryDate;

        SqlParameter p_TransType = command[0].Parameters.Add("TransType", SqlDbType.Char);
        p_TransType.Direction = ParameterDirection.Input;
        p_TransType.Value = objEmpTrans.TransType;

        SqlParameter p_ActionId = command[0].Parameters.Add("ActionId", SqlDbType.BigInt);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.Value = objEmpTrans.ActionId;

        SqlParameter p_PreDivId = command[0].Parameters.Add("DivisionId", DBNull.Value);
        p_PreDivId.Direction = ParameterDirection.Input;
        p_PreDivId.IsNullable = true;
        if ((strPreDivId != "-1") && (strPreDivId != "") && (strPreDivId != "99999"))
            p_PreDivId.Value = strPreDivId;

        SqlParameter p_PreClinicId = command[0].Parameters.Add("ClinicId", DBNull.Value);
        p_PreClinicId.Direction = ParameterDirection.Input;
        p_PreClinicId.IsNullable = true;
        if ((strPreClinicId != "-1") && (strPreClinicId != "") && (strPreClinicId != "99999"))
            p_PreClinicId.Value = strPreClinicId;

        SqlParameter p_PreProjectId = command[0].Parameters.Add("ProjectId", DBNull.Value);
        p_PreProjectId.Direction = ParameterDirection.Input;
        p_PreProjectId.IsNullable = true;
        if ((strPreProjectId != "-1") && (strPreProjectId != "") && (strPreProjectId != "99999"))
            p_PreProjectId.Value = strPreProjectId;

        SqlParameter p_PreDeptId = command[0].Parameters.Add("DeptId", DBNull.Value);
        p_PreDeptId.Direction = ParameterDirection.Input;
        p_PreDeptId.IsNullable = true;
        if ((strPreDeptId != "-1") && (strPreDeptId != "") && (strPreDeptId != "99999"))
            p_PreDeptId.Value = strPreDeptId;

        SqlParameter p_PreSubDeptId = command[0].Parameters.Add("SubDeptId", DBNull.Value);
        p_PreSubDeptId.Direction = ParameterDirection.Input;
        p_PreSubDeptId.IsNullable = true;
        if ((strPreSubDeptId != "-1") && (strPreSubDeptId != "") && (strPreSubDeptId != "99999"))
            p_PreSubDeptId.Value = strPreSubDeptId;

        SqlParameter p_PreDesigId = command[0].Parameters.Add("DesigId", DBNull.Value);
        p_PreDesigId.Direction = ParameterDirection.Input;
        p_PreDesigId.IsNullable = true;
        if ((strPreDesigId != "-1") && (strPreDesigId != "") && (strPreDesigId != "99999"))
            p_PreDesigId.Value = strPreDesigId;

        SqlParameter p_PreGradeId = command[0].Parameters.Add("GradeId", DBNull.Value);
        p_PreGradeId.Direction = ParameterDirection.Input;
        p_PreGradeId.IsNullable = true;
        if ((strPreGradeId != "-1") && (strPreGradeId != "") && (strPreGradeId != "99999"))
            p_PreGradeId.Value = strPreGradeId;

        SqlParameter p_PreSalLocId = command[0].Parameters.Add("SalLocId", DBNull.Value);
        p_PreSalLocId.Direction = ParameterDirection.Input;
        p_PreSalLocId.IsNullable = true;
        if ((strPreSalLocId != "-1") && (strPreSalLocId != "") && (strPreSalLocId != "99999"))
            p_PreSalLocId.Value = strPreSalLocId;

        SqlParameter p_PreEmpTypeId = command[0].Parameters.Add("EmpTypeId", DBNull.Value);
        p_PreEmpTypeId.Direction = ParameterDirection.Input;
        p_PreEmpTypeId.IsNullable = true;
        if ((strPreEmpTypeId != "-1") && (strPreEmpTypeId != "") && (strPreEmpTypeId != "99999"))
            p_PreEmpTypeId.Value = strPreEmpTypeId;

        SqlParameter p_BasicSal = command[0].Parameters.Add("GrossSalary", DBNull.Value);
        p_BasicSal.Direction = ParameterDirection.Input;
        p_BasicSal.IsNullable = true;
        if (strPreBasicSal != "")
            p_BasicSal.Value = strPreBasicSal;

        SqlParameter p_EffDate = command[0].Parameters.Add("EffDate", DBNull.Value);
        p_EffDate.Direction = ParameterDirection.Input;
        p_EffDate.IsNullable = true;
        if (strPreEffDate != "")
            p_EffDate.Value = strPreEffDate;

        SqlParameter p_NextIncDate = command[0].Parameters.Add("NextIncDate", DBNull.Value);
        p_NextIncDate.Direction = ParameterDirection.Input;
        p_NextIncDate.IsNullable = true;
        if (strPreNextIncDate != "")
            p_NextIncDate.Value = strPreNextIncDate;

        SqlParameter p_GradeChangeDate = command[0].Parameters.Add("GradeChangeDate", DBNull.Value);
        p_GradeChangeDate.Direction = ParameterDirection.Input;
        p_GradeChangeDate.IsNullable = true;
        if (strGradeChDate != "")
            p_GradeChangeDate.Value = strGradeChDate;

        SqlParameter p_Remarks = command[0].Parameters.Add("Remarks", SqlDbType.VarChar);
        p_Remarks.Direction = ParameterDirection.Input;
        p_Remarks.Value = objEmpTrans.Remarks;

        SqlParameter p_IsTAApplicable = command[0].Parameters.Add("IsTAApplicable", SqlDbType.VarChar);
        p_IsTAApplicable.Direction = ParameterDirection.Input;
        p_IsTAApplicable.Value = objEmpTrans.IsTAApplicable;

        SqlParameter p_InsertedBy = command[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = objEmpTrans.InsertedBy;

        SqlParameter p_InsertedDate = command[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = objEmpTrans.InsertedDate;

        SqlParameter p_IsUpdate = command[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = strIsUpdate;

        if (strIsUpdate == "N")
        {
            //command[1] = InsertEmpInfoLog(objEmpTrans.EmpID);

            command[2] = new SqlCommand("proc_Update_EmpInfoTransitionLog");
            command[2].CommandType = CommandType.StoredProcedure;

            SqlParameter p_EmpID1 = command[2].Parameters.Add("EmpID", SqlDbType.VarChar);
            p_EmpID1.Direction = ParameterDirection.Input;
            p_EmpID1.Value = objEmpTrans.EmpID;

            SqlParameter p_DivisionId = command[2].Parameters.Add("DivisionId", DBNull.Value);
            p_DivisionId.Direction = ParameterDirection.Input;
            p_DivisionId.IsNullable = true;
            if ((objEmpTrans.DivisionId != "-1") && (objEmpTrans.DivisionId != "") && (objEmpTrans.DivisionId != "99999"))
                p_DivisionId.Value = objEmpTrans.DivisionId;

            SqlParameter p_ClinicId = command[2].Parameters.Add("ClinicId", DBNull.Value);
            p_ClinicId.Direction = ParameterDirection.Input;
            p_ClinicId.IsNullable = true;
            if ((objEmpTrans.ClinicId != "-1") && (objEmpTrans.ClinicId != "") && (objEmpTrans.ClinicId != "99999"))
                p_ClinicId.Value = objEmpTrans.ClinicId;

            SqlParameter p_ProjectId = command[2].Parameters.Add("ProjectId", DBNull.Value);
            p_ProjectId.Direction = ParameterDirection.Input;
            p_ProjectId.IsNullable = true;
            if ((objEmpTrans.ProjectId != "-1") && (objEmpTrans.ProjectId != "") && (objEmpTrans.ProjectId != "99999"))
                p_ProjectId.Value = objEmpTrans.ProjectId;

            SqlParameter p_DeptId = command[2].Parameters.Add("DeptId", DBNull.Value);
            p_DeptId.Direction = ParameterDirection.Input;
            p_DeptId.IsNullable = true;
            if ((objEmpTrans.DeptId != "-1") && (objEmpTrans.DeptId != "") && (objEmpTrans.DeptId != "99999"))
                p_DeptId.Value = objEmpTrans.DeptId;

            SqlParameter p_SubDeptId = command[2].Parameters.Add("SubDeptId", DBNull.Value);
            p_SubDeptId.Direction = ParameterDirection.Input;
            p_SubDeptId.IsNullable = true;
            if ((objEmpTrans.SubDeptId != "-1") && (objEmpTrans.SubDeptId != "") && (objEmpTrans.SubDeptId != "99999"))
                p_SubDeptId.Value = objEmpTrans.SubDeptId;

            SqlParameter p_DesigId = command[2].Parameters.Add("DesigId", DBNull.Value);
            p_DesigId.Direction = ParameterDirection.Input;
            p_DesigId.IsNullable = true;
            if ((objEmpTrans.DesigId != "-1") && (objEmpTrans.DesigId != "") && (objEmpTrans.DesigId != "99999"))
                p_DesigId.Value = objEmpTrans.DesigId;

            SqlParameter p_GradeId1 = command[2].Parameters.Add("GradeId", DBNull.Value);
            p_GradeId1.Direction = ParameterDirection.Input;
            p_GradeId1.IsNullable = true;
            if ((objEmpTrans.GradeId != "-1") && (objEmpTrans.GradeId != "") && (objEmpTrans.GradeId != "99999"))
                p_GradeId1.Value = objEmpTrans.GradeId;

            SqlParameter p_SalLocId1 = command[2].Parameters.Add("SalLocId", DBNull.Value);
            p_SalLocId1.Direction = ParameterDirection.Input;
            p_SalLocId1.IsNullable = true;
            if ((objEmpTrans.SalLocId != "-1") && (objEmpTrans.SalLocId != "") && (objEmpTrans.SalLocId != "99999"))
                p_SalLocId1.Value = objEmpTrans.SalLocId;

            SqlParameter p_EmpTypeId = command[2].Parameters.Add("EmpTypeId", DBNull.Value);
            p_EmpTypeId.Direction = ParameterDirection.Input;
            p_EmpTypeId.IsNullable = true;
            if ((objEmpTrans.EmpTypeId != "-1") && (objEmpTrans.EmpTypeId != "") && (objEmpTrans.EmpTypeId != "99999"))
                p_EmpTypeId.Value = objEmpTrans.EmpTypeId;

            SqlParameter p_BasicSal1 = command[2].Parameters.Add("GrossSalary", DBNull.Value);
            p_BasicSal1.Direction = ParameterDirection.Input;
            p_BasicSal1.IsNullable = true;
            if (objEmpTrans.BasicSal != "-1")
                p_BasicSal1.Value = objEmpTrans.BasicSal;

            SqlParameter p_ActionId2 = command[2].Parameters.Add("ActionId", SqlDbType.BigInt);
            p_ActionId2.Direction = ParameterDirection.Input;
            p_ActionId2.Value = objEmpTrans.ActionId;

            SqlParameter p_ActionDate = command[2].Parameters.Add("ActionDate", DBNull.Value);
            p_ActionDate.Direction = ParameterDirection.Input;
            p_ActionDate.IsNullable = true;
            if (objEmpTrans.EffDate != "-1")
                p_ActionDate.Value = objEmpTrans.EffDate;

            SqlParameter p_DateInGrade = command[2].Parameters.Add("DateInGrade", DBNull.Value);
            p_DateInGrade.Direction = ParameterDirection.Input;
            p_DateInGrade.IsNullable = true;
            if (objEmpTrans.GradeChangeDate != "-1")
                p_DateInGrade.Value = objEmpTrans.GradeChangeDate;

            SqlParameter p_RetirementDate = command[2].Parameters.Add("RetirementDate", DBNull.Value);
            p_RetirementDate.Direction = ParameterDirection.Input;
            p_RetirementDate.IsNullable = true;
            if (strRetirementDate != "")
                p_RetirementDate.Value = strRetirementDate;

            SqlParameter p_DateInPosition = command[2].Parameters.Add("EffDate", DBNull.Value);
            p_DateInPosition.Direction = ParameterDirection.Input;
            p_DateInPosition.IsNullable = true;
            if (objEmpTrans.EffDate != "-1")
                p_DateInPosition.Value = objEmpTrans.EffDate;

            SqlParameter p_InsertedBy2 = command[2].Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy2.Direction = ParameterDirection.Input;
            p_InsertedBy2.Value = objEmpTrans.InsertedBy;

            SqlParameter p_InsertedDate2 = command[2].Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate2.Direction = ParameterDirection.Input;
            p_InsertedDate2.Value = objEmpTrans.InsertedDate;

            p_TransType = command[2].Parameters.Add("TransType", SqlDbType.Char);
            p_TransType.Direction = ParameterDirection.Input;
            p_TransType.Value = objEmpTrans.TransType;

            command[3] = InsertEmpActionLog(objEmpTrans.EmpID, objEmpTrans.ActionId, objEmpTrans.EffDate, objEmpTrans.InsertedBy, objEmpTrans.InsertedDate);

            int i = 4;
            if (dtSalPackUpdate.Rows.Count > 0)
            {
                if (objEmpTrans.TransType == "P")
                {
                    if (string.IsNullOrEmpty(GetEmpIdWsSalHisPakId(strSalPackId, objEmpTrans.EmpID)) == true)
                        command[4] = InsertSalaryPakHisDetls(strSalPackId, objEmpTrans.EmpID, objEmpTrans.EffDate, objEmpTrans.InsertedBy, objEmpTrans.InsertedDate, "Salary Package");
                    i++;
                }

                foreach (SqlCommand cmdSal in this.GetSalPackDetUpdateCommand(dtSalPackUpdate, strSalPackId, objEmpTrans.InsertedBy, objEmpTrans.InsertedDate, "Transition"))
                {
                    command[i] = cmdSal;
                    i++;
                }

                if (objEmpTrans.TransType == "P")
                {
                    i++;
                    command[i] = InsertSalaryPakHisDetls(strSalPackId, objEmpTrans.EmpID, objEmpTrans.EffDate, objEmpTrans.InsertedBy, objEmpTrans.InsertedDate, "Promotion");

                }
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

    //Insert Employee ActionLog
    public SqlCommand InsertEmpActionLog(string strEmpId, string strActionId, string strActionDate, string strInsertedBy, string strInsertedDate)
    {
        SqlCommand command = new SqlCommand("proc_Insert_EmpActionLog");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LogId = command.Parameters.Add("LogId", SqlDbType.Char);
        p_LogId.Direction = ParameterDirection.Input;
        p_LogId.Value = Common.getMaxId("EmpActionLog", "LogId");

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpId;

        SqlParameter p_ActionId = command.Parameters.Add("ActionId", SqlDbType.BigInt);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.Value = strActionId;

        SqlParameter p_ActionDate = command.Parameters.Add("ActionDate", DBNull.Value);
        p_ActionDate.Direction = ParameterDirection.Input;
        p_ActionDate.IsNullable = true;
        if (strActionDate != "")
            p_ActionDate.Value = strActionDate;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsertedDate;

        return command;
    }
 
    public SqlCommand InsertEmpInfoJoining(string strEmpId, string strJoinJobTitleId, string strJoiningDate, string strJoinSectorId, string strJoinDeptId, string strJoinSalLocId, string strJoinBasicSalary, string strInsertedBy, string strInsertedDate)
    {
        SqlCommand command = new SqlCommand("proc_Insert_EmpInfoJoining");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LogId = command.Parameters.Add("LogId", SqlDbType.BigInt);
        p_LogId.Direction = ParameterDirection.Input;
        p_LogId.Value = Convert.ToInt32(Common.getMaxId("EmpInfoJoining", "LogId"));

        SqlParameter p_EmpID = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpId;

        SqlParameter p_JoinJobTitleId = command.Parameters.Add("JoinJobTitleId", DBNull.Value);
        p_JoinJobTitleId.Direction = ParameterDirection.Input;
        p_JoinJobTitleId.IsNullable = true;
        if (strJoinJobTitleId != "99999")
        p_JoinJobTitleId.Value = strJoinJobTitleId;

       
        SqlParameter p_JoiningDate = command.Parameters.Add("JoiningDate", DBNull.Value);
        p_JoiningDate.Direction = ParameterDirection.Input;
        p_JoiningDate.IsNullable = true;
        if (strJoiningDate != "")
        p_JoiningDate.Value = strJoiningDate;

        SqlParameter p_JoinSectorId = command.Parameters.Add("JoinSectorId", DBNull.Value);
        p_JoinSectorId.Direction = ParameterDirection.Input;
        p_JoinSectorId.IsNullable = true;
        if (strJoinSectorId != "99999")
        p_JoinSectorId.Value = strJoinSectorId;
     
        SqlParameter p_JoinDeptId = command.Parameters.Add("JoinDeptId", DBNull.Value);
        p_JoinDeptId.Direction = ParameterDirection.Input;
        p_JoinDeptId.IsNullable = true;
        if (strJoinDeptId != "99999")
        p_JoinDeptId.Value = strJoinDeptId;
       
        SqlParameter p_JoinSalLocId = command.Parameters.Add("JoinSalLocId", DBNull.Value);
        p_JoinSalLocId.Direction = ParameterDirection.Input;
        p_JoinSalLocId.IsNullable = true;
        if (strJoinSalLocId != "99999")
        p_JoinSalLocId.Value = strJoinSalLocId;

        SqlParameter p_JoinBasicSalary = command.Parameters.Add("JoinBasicSalary", SqlDbType.Decimal);
        p_JoinBasicSalary.Direction = ParameterDirection.Input;
        p_JoinBasicSalary.Value = Convert.ToDecimal(strJoinBasicSalary);


        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsertedDate;
        return command;
    }

    public void InsertEmpTempDuty(clsTempDutyAssign objTempDutyAssign, string IsUpdate)
    {
        SqlCommand[] command = new SqlCommand[4];
        command[0] = new SqlCommand("proc_Insert_EmpTempDutyAssignLog");
        command[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_DutyAssignID = command[0].Parameters.Add("DutyAssignID", SqlDbType.BigInt);
        p_DutyAssignID.Direction = ParameterDirection.Input;
        p_DutyAssignID.Value = objTempDutyAssign.DutyAssignID;

        SqlParameter p_EmpID = command[0].Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = objTempDutyAssign.EmpID;

        SqlParameter p_ActionId = command[0].Parameters.Add("ActionId", SqlDbType.BigInt);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.Value = objTempDutyAssign.ActionId;

        SqlParameter p_DivisionId = command[0].Parameters.Add("DivisionId", DBNull.Value);
        p_DivisionId.Direction = ParameterDirection.Input;
        p_DivisionId.IsNullable = true;
        if (objTempDutyAssign.DivisionId != "")
            p_DivisionId.Value = objTempDutyAssign.DivisionId;

        SqlParameter p_ClinicId = command[0].Parameters.Add("ClinicId", DBNull.Value);
        p_ClinicId.Direction = ParameterDirection.Input;
        p_ClinicId.IsNullable = true;
        if (objTempDutyAssign.ClinicId != "")
            p_ClinicId.Value = objTempDutyAssign.ClinicId;

        SqlParameter p_ProjectId = command[0].Parameters.Add("ProjectId", DBNull.Value);
        p_ProjectId.Direction = ParameterDirection.Input;
        p_ProjectId.IsNullable = true;
        if (objTempDutyAssign.ProjectId != "")
            p_ProjectId.Value = objTempDutyAssign.ProjectId;

        SqlParameter p_DeptId = command[0].Parameters.Add("DeptId", DBNull.Value);
        p_DeptId.Direction = ParameterDirection.Input;
        p_DeptId.IsNullable = true;
        if (objTempDutyAssign.DeptId != "")
            p_DeptId.Value = objTempDutyAssign.DeptId;

        SqlParameter p_Assignment = command[0].Parameters.Add("Assignment", SqlDbType.VarChar);
        p_Assignment.Direction = ParameterDirection.Input;
        p_Assignment.Value = objTempDutyAssign.Assignment;

        SqlParameter p_StartDate = command[0].Parameters.Add("StartDate", DBNull.Value);
        p_StartDate.Direction = ParameterDirection.Input;
        p_StartDate.IsNullable = true;
        if (objTempDutyAssign.StartDate != "")
            p_StartDate.Value = objTempDutyAssign.StartDate;

        SqlParameter p_EndDate = command[0].Parameters.Add("EndDate", DBNull.Value);
        p_EndDate.Direction = ParameterDirection.Input;
        p_EndDate.IsNullable = true;
        if (objTempDutyAssign.EndDate != "")
            p_EndDate.Value = objTempDutyAssign.EndDate;

        SqlParameter p_Percentage = command[0].Parameters.Add("Percentage", DBNull.Value);
        p_Percentage.Direction = ParameterDirection.Input;
        p_Percentage.IsNullable = true;
        if (objTempDutyAssign.Percentage != "")
            p_Percentage.Value = objTempDutyAssign.Percentage;

        SqlParameter p_Amount = command[0].Parameters.Add("Amount", DBNull.Value);
        p_Amount.Direction = ParameterDirection.Input;
        p_Amount.IsNullable = true;
        if (objTempDutyAssign.Amount != "")
            p_Amount.Value = objTempDutyAssign.Amount;

        SqlParameter p_SupervisorId = command[0].Parameters.Add("SupervisorId", SqlDbType.Char);
        p_SupervisorId.Direction = ParameterDirection.Input;
        p_SupervisorId.Value = objTempDutyAssign.SupervisorId;

        SqlParameter p_SupervisorComment = command[0].Parameters.Add("SupervisorComment", SqlDbType.VarChar);
        p_SupervisorComment.Direction = ParameterDirection.Input;
        p_SupervisorComment.Value = objTempDutyAssign.SupervisorComment;

        SqlParameter p_InsertedBy = command[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = objTempDutyAssign.InsertedBy;

        SqlParameter p_InsertedDate = command[0].Parameters.Add("InsertedDate", DBNull.Value);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.IsNullable = true;
        if (objTempDutyAssign.InsertedDate != "")
            p_InsertedDate.Value = objTempDutyAssign.InsertedDate;

        SqlParameter p_IsUpdate = command[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        if (IsUpdate == "N")
        {
            //command[1] = InsertEmpInfoLog(objTempDutyAssign.EmpID);

            command[2] = UpdateEmpHRAction(objTempDutyAssign.EmpID, objTempDutyAssign.ActionId, objTempDutyAssign.InsertedDate, objTempDutyAssign.InsertedBy, objTempDutyAssign.InsertedDate);

            command[3] = InsertEmpActionLog(objTempDutyAssign.EmpID, objTempDutyAssign.ActionId, objTempDutyAssign.InsertedDate, objTempDutyAssign.InsertedBy, objTempDutyAssign.InsertedDate);
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

    private SqlCommand UpdateEmpHRProbationExt(string strEmpId, string strActionId, string strActionDate, string strNewGrossSalary, string strNewBasicSalary,
      string strProbationExt, string strInsertedBy, string strInsertedDate)
    {
        string strSQL = "UPDATE EmpInfo SET ActionId=" + strActionId + ",ActionDate='" + strActionDate + ",GrossSalary=" + strNewGrossSalary + ",BasicSalary=" + strNewBasicSalary +
            "ProbationPeriod=ProbationPeriod+" + strProbationExt +"',UpdatedBy='" + strInsertedBy + "',UpdatedDate='" + strInsertedDate + "' WHERE EmpId='" + strEmpId + "'";

        SqlCommand command = new SqlCommand(strSQL);

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_ActionId = command.Parameters.Add("ActionId", SqlDbType.BigInt);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.Value = strActionId;

        SqlParameter p_ActionDate = command.Parameters.Add("ActionDate", DBNull.Value);
        p_ActionDate.Direction = ParameterDirection.Input;
        p_ActionDate.IsNullable = true;
        if (strActionDate != "")
            p_ActionDate.Value = strActionDate;

        SqlParameter p_NewGrossSal = command.Parameters.Add("GrossSalary", SqlDbType.Decimal);
        p_NewGrossSal.Direction = ParameterDirection.Input;
        p_NewGrossSal.Value = strNewGrossSalary;

        SqlParameter p_BasicSalary = command.Parameters.Add("BasicSalary", SqlDbType.Decimal);
        p_BasicSalary.Direction = ParameterDirection.Input;
        p_BasicSalary.Value = strNewBasicSalary;


        SqlParameter p_ProbationPeriod = command.Parameters.Add("ProbationPeriod", SqlDbType.BigInt);
        p_ProbationPeriod.Direction = ParameterDirection.Input;
        p_ProbationPeriod.Value = strProbationExt;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsertedDate;

        return command;
    }

    private SqlCommand UpdateEmpHRContactExt(string strEmpId, string strActionId, string strActionDate, string strContExpDate, string strInsertedBy, string strInsertedDate)
    {
        string strSQL = "UPDATE EmpInfo SET ActionId='" + strActionId + "',ActionDate='" + strActionDate + "',ContractEndDate='" + strContExpDate + "',UpdatedBy='" + strInsertedBy
            + "',UpdatedDate='" + strInsertedDate + "' WHERE EmpId='" + strEmpId + "'";

        SqlCommand command = new SqlCommand(strSQL);

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_ActionId = command.Parameters.Add("ActionId", SqlDbType.BigInt);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.Value = strActionId;

        SqlParameter p_ActionDate = command.Parameters.Add("ActionDate", DBNull.Value);
        p_ActionDate.Direction = ParameterDirection.Input;
        p_ActionDate.IsNullable = true;
        if (strActionDate != "")
            p_ActionDate.Value = strActionDate;

        SqlParameter p_ContExpDate = command.Parameters.Add("ContractEndDate", DBNull.Value);
        p_ContExpDate.Direction = ParameterDirection.Input;
        p_ContExpDate.IsNullable = true;
        if (strContExpDate != "")
            p_ContExpDate.Value = strContExpDate;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsertedDate;

        return command;
    }

    //Insert into Employee Log File
    public SqlCommand InsertEmpInfoLog(string strEmpId)
    {
        string strLogId = Common.getMaxId("EmpInfoLog", "LogId");
        string strSQL = "INSERT INTO EmpInfoLog SELECT " + strLogId + ",EmpId,Title, FirstName, MiddleName, LastName," +
            " FullName, Gender,ReligionId,BloodGroupId,DOB,FatherName,MotherName,PreAddress,PrePhone,PreFax,PerAddress," +
            " PerPhone,PerFax,PerDistrictID,PerCountryID,OfficeExt,OfficeEmail,PersonalEmail,MailingAddrs,MaritalStatus," +
            " MarriageDate,ProffDegreeId,SpecialSkillId,IsSpectacled,DrivingLicense,LicenseRenewDate,IsRelativeInSC,RelationId," +
            " TINNo,PassportNo,PassExpDate,Circle,Zone,PassportIssueOffice,NationalId,Nationality,DOBId,SkypeId,CellPhone,LandPhone," +
            " EmpTypeID,GradeId,GradeLevelId,JobTitleId,DesigId,SectorId,DeptId,UnitId,PosFuncId,PostingDivId,PostingDistId,PostingPlaceId," +
            " ContractPurpose,WorkArea,SalLocId,SalSubLocId,GrossSalary,BonusPakId,AttnPolicyID,LeavePakId,JoiningDate,ProbationPeriod," +
            " ContractInterval,ContractEndDate,ConfirmationDate,IsServiceAgrmnt,ServiceStartDate,ServiceEndDate,WorkAreaType,IsSeveranceBenefit," +
            " SeveranceId,SeveranceReason,RetirementDate,EmpStatus,SeparateTypeId,SeparateDate,SeparateReason,BankCode,RoutingNo,BankAccNo," +
            " SupervisorId,OtherBenefit,Remarks,IsMedicalEntmnt,IsOTEntmnt,IsChildEduAllow,EmpSignature,UploadCV,UploadDocument,IsDeleted,PostingDate," +
            " DateInPosition,DateInGrade,ActionDate,SalPakId,MPCId,IsPayrollStaff,EmpPicLoc,ActionId,InsertedBy,InsertedDate,UpdatedBy,UpdatedDate," +
            " LastUpdatedFrom,AddResponseEndDate,WeekendId,CardNo,IsRoaster,WorkingDays" +
            " FROM EmpInfo WHERE EmpId='" + strEmpId + "'";

        SqlCommand command = new SqlCommand(strSQL);

        return command;
    }
    #endregion

    #region Employment
    //Insert Experience
    public void InsertExperience(string strId, string strEmpId, string strJobTitle, string strCompany, string strStartDate, string strEndDate, string strDuration,
        string strResponsibility, string strIsSC, string strIsEmergency, string strInsBy, string strInsDate, string strIsUpdate, string strIsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_EmpExperience");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_ExperId = command.Parameters.Add("ExperId", SqlDbType.BigInt);
        p_ExperId.Direction = ParameterDirection.Input;
        p_ExperId.Value = strId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_JobTitle = command.Parameters.Add("JobTitle", SqlDbType.VarChar);
        p_JobTitle.Direction = ParameterDirection.Input;
        p_JobTitle.Value = strJobTitle;

        SqlParameter p_Company = command.Parameters.Add("Company", SqlDbType.VarChar);
        p_Company.Direction = ParameterDirection.Input;
        p_Company.Value = strCompany;

        SqlParameter p_StartDate = command.Parameters.Add("StartDate", DBNull.Value);
        p_StartDate.Direction = ParameterDirection.Input;
        p_StartDate.IsNullable = true;
        if (strStartDate != "")
            p_StartDate.Value = strStartDate;

        SqlParameter p_EndDate = command.Parameters.Add("EndDate", DBNull.Value);
        p_EndDate.Direction = ParameterDirection.Input;
        p_EndDate.IsNullable = true;
        if (strEndDate != "")
            p_EndDate.Value = strEndDate;

        SqlParameter p_Duration = command.Parameters.Add("Duration", DBNull.Value);
        p_Duration.Direction = ParameterDirection.Input;
        p_Duration.IsNullable = true;
        if (strDuration != "")
            p_Duration.Value = strDuration;

        SqlParameter p_Responsibility = command.Parameters.Add("Responsibility", SqlDbType.VarChar);
        p_Responsibility.Direction = ParameterDirection.Input;
        p_Responsibility.Value = strResponsibility;

        SqlParameter p_IsSC = command.Parameters.Add("IsSC", SqlDbType.Char);
        p_IsSC.Direction = ParameterDirection.Input;
        p_IsSC.Value = strIsSC;

        SqlParameter p_IsEmergency = command.Parameters.Add("IsEmergency", SqlDbType.Char);
        p_IsEmergency.Direction = ParameterDirection.Input;
        p_IsEmergency.Value = strIsEmergency;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

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

    //Insert Emergency Contact
    public void InsertEmergencyContact(string strId, string strEmpId, string strName, string strAdd, string strPhone, string strReation,
        string strInsBy, string strInsDate, string strIsUpdate, string strIsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_EmpEmergencyContact");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_ContactId = command.Parameters.Add("ContactId", SqlDbType.BigInt);
        p_ContactId.Direction = ParameterDirection.Input;
        p_ContactId.Value = strId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_Name = command.Parameters.Add("Name", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = strName;

        SqlParameter p_Address = command.Parameters.Add("Address", SqlDbType.VarChar);
        p_Address.Direction = ParameterDirection.Input;
        p_Address.Value = strAdd;

        SqlParameter p_Phone = command.Parameters.Add("Phone", SqlDbType.VarChar);
        p_Phone.Direction = ParameterDirection.Input;
        p_Phone.Value = strPhone;

        SqlParameter p_ReationId = command.Parameters.Add("RelationId", DBNull.Value);
        p_ReationId.Direction = ParameterDirection.Input;
        p_ReationId.IsNullable = true;
        if (strReation != "")
            p_ReationId.Value = strReation;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

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

    //Insert Experience
    public void InsertNominee(string strId, string strEmpId, string strName, string strRelation, string strDOB, string strBenefit, string strGender,
        string strRemarks, string strNomineeType, string strInsBy, string strInsDate, string strIsUpdate, string strIsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_Nominee");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_NomineeId = command.Parameters.Add("NomineeId", SqlDbType.BigInt);
        p_NomineeId.Direction = ParameterDirection.Input;
        p_NomineeId.Value = strId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_NomineeName = command.Parameters.Add("NomineeName", SqlDbType.VarChar);
        p_NomineeName.Direction = ParameterDirection.Input;
        p_NomineeName.Value = strName;

        SqlParameter p_Relation = command.Parameters.Add("RelationId", DBNull.Value);
        p_Relation.Direction = ParameterDirection.Input;
        p_Relation.IsNullable = true;
        if (strRelation != "")
            p_Relation.Value = strRelation;

        SqlParameter p_DOB = command.Parameters.Add("DOB", DBNull.Value);
        p_DOB.Direction = ParameterDirection.Input;
        p_DOB.IsNullable = true;
        if (strDOB != "")
            p_DOB.Value = strDOB;

        SqlParameter p_Benefit = command.Parameters.Add("Benefit", DBNull.Value);
        p_Benefit.Direction = ParameterDirection.Input;
        p_Benefit.IsNullable = true;
        if (strBenefit != "")
            p_Benefit.Value = strBenefit;

        SqlParameter p_Gender = command.Parameters.Add("Gender", SqlDbType.Char);
        p_Gender.Direction = ParameterDirection.Input;
        p_Gender.Value = strGender;

        SqlParameter p_Remarks = command.Parameters.Add("Remarks", SqlDbType.VarChar);
        p_Remarks.Direction = ParameterDirection.Input;
        p_Remarks.Value = strRemarks;

        SqlParameter p_NomineeType = command.Parameters.Add("NomineeType", SqlDbType.VarChar);
        p_NomineeType.Direction = ParameterDirection.Input;
        p_NomineeType.Value = strNomineeType;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

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

    //Insert Medical Benefit
    public void InsertMedicalBenefit(string strId, string strEmpId, string strFisYrId, string strBenefitType, string strIsSpHospital, string strMedicalDate,string strLimit,
        string strReqAmount, string strApproveAmount, string strRemarks, string strNomineeId, string strInsBy, string strInsDate, string strIsUpdate, string strIsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_MedicalBenefit");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_BenefitId = command.Parameters.Add("BenefitId", SqlDbType.BigInt);
        p_BenefitId.Direction = ParameterDirection.Input;
        p_BenefitId.Value = strId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_FiscalYrId = command.Parameters.Add("MedFiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = strFisYrId;

        SqlParameter p_NomineeName = command.Parameters.Add("BenefitType", SqlDbType.Char);
        p_NomineeName.Direction = ParameterDirection.Input;
        p_NomineeName.Value = strBenefitType;

        SqlParameter p_IsSpHospitalization = command.Parameters.Add("IsSpHospital", SqlDbType.Char);
        p_IsSpHospitalization.Direction = ParameterDirection.Input;
        p_IsSpHospitalization.Value = strIsSpHospital;

        SqlParameter p_MedicalDate = command.Parameters.Add("MedicalDate", DBNull.Value);
        p_MedicalDate.Direction = ParameterDirection.Input;
        p_MedicalDate.IsNullable = true;
        if (strMedicalDate != "")
            p_MedicalDate.Value = strMedicalDate;

        SqlParameter p_Limit = command.Parameters.Add("Limit", DBNull.Value);
        p_Limit.Direction = ParameterDirection.Input;
        p_Limit.IsNullable = true;
        if (strLimit != "")
            p_Limit.Value = strLimit;

        SqlParameter p_ReqAmount = command.Parameters.Add("ReqAmount", DBNull.Value);
        p_ReqAmount.Direction = ParameterDirection.Input;
        p_ReqAmount.IsNullable = true;
        if (strReqAmount != "")
            p_ReqAmount.Value = strReqAmount;

        SqlParameter p_ApproveAmount = command.Parameters.Add("ApproveAmount", DBNull.Value);
        p_ApproveAmount.Direction = ParameterDirection.Input;
        p_ApproveAmount.IsNullable = true;
        if (strApproveAmount != "")
            p_ApproveAmount.Value = strApproveAmount;

        SqlParameter p_Remarks = command.Parameters.Add("Remarks", SqlDbType.VarChar);
        p_Remarks.Direction = ParameterDirection.Input;
        p_Remarks.Value = strRemarks;


        SqlParameter p_NomineeId = command.Parameters.Add("NomineeId", DBNull.Value);
        p_NomineeId.Direction = ParameterDirection.Input;
        p_NomineeId.IsNullable = true;
        if (strNomineeId !="99999")
        p_NomineeId.Value = strNomineeId;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

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

    //Insert Witness
    public void InsertWitness(string strId, string strEmpId, string strName, string strAddress, string strSignDate, string strInsBy,
        string strInsDate, string strIsUpdate, string strIsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_WitnessList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_NomineeId = command.Parameters.Add("WitnessId", SqlDbType.BigInt);
        p_NomineeId.Direction = ParameterDirection.Input;
        p_NomineeId.Value = strId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_NomineeName = command.Parameters.Add("WitnessName", SqlDbType.VarChar);
        p_NomineeName.Direction = ParameterDirection.Input;
        p_NomineeName.Value = strName;

        SqlParameter p_Gender = command.Parameters.Add("WitnessAdd", SqlDbType.VarChar);
        p_Gender.Direction = ParameterDirection.Input;
        p_Gender.Value = strAddress;

        SqlParameter p_SignDOB = command.Parameters.Add("SignDate", DBNull.Value);
        p_SignDOB.Direction = ParameterDirection.Input;
        p_SignDOB.IsNullable = true;
        if (strSignDate != "")
            p_SignDOB.Value = strSignDate;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

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
    #endregion

    #region Select Employment

    public string GetSupervisorName(string strSuppId)
    {
        string strSQL = "SELECT FullName FROM EmpInfo WHERE EmpId='" + strSuppId + "'";

        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        return objDC.GetScalarVal(command);
    }
    #endregion

    #region Education
    // Insert or Update  or Delete Data of Degree table  
    public void InsertDegree(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_DegreeList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("DegreeId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = clsCommon.ID;

        SqlParameter p_Name = command.Parameters.Add("DegreeName", SqlDbType.VarChar);
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

    // Insert or Update  or Delete Data of Institute table  
    public void InsertInstitute(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_InstituteList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("InstituteId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = clsCommon.ID;

        SqlParameter p_Name = command.Parameters.Add("InstituteName", SqlDbType.VarChar);
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


    // Insert or Update  or Delete Data of Subject table  
    public void InsertSubject(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_SubjectList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("SubjectId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = clsCommon.ID;

        SqlParameter p_Name = command.Parameters.Add("SubjectName", SqlDbType.VarChar);
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

    // Insert or Update  or Delete Data of Degree table  
    public void InsertResult(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_ResultList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("ResultId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = clsCommon.ID;

        SqlParameter p_Name = command.Parameters.Add("ResultName", SqlDbType.VarChar);
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

    //Insert Emergency Contact
    public void InsertEmpEducation(clsEmpEducation objEdu, string strIsUpdate, string strIsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_EmpEducation");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_ContactId = command.Parameters.Add("EduId", SqlDbType.BigInt);
        p_ContactId.Direction = ParameterDirection.Input;
        p_ContactId.Value = objEdu.EduId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = objEdu.EmpID;

        SqlParameter p_DegreeId = command.Parameters.Add("DegreeId", DBNull.Value);
        p_DegreeId.Direction = ParameterDirection.Input;
        p_DegreeId.IsNullable = true;
        if (objEdu.DegreeId != "")
            p_DegreeId.Value = objEdu.DegreeId;

        SqlParameter p_InstituteId = command.Parameters.Add("InstituteId", DBNull.Value);
        p_InstituteId.Direction = ParameterDirection.Input;
        p_InstituteId.IsNullable = true;
        if (objEdu.InstituteId != "")
            p_InstituteId.Value = objEdu.InstituteId;

        SqlParameter p_SubjectId = command.Parameters.Add("SubjectId", DBNull.Value);
        p_SubjectId.Direction = ParameterDirection.Input;
        p_SubjectId.IsNullable = true;
        if (objEdu.SubjectId != "")
            p_SubjectId.Value = objEdu.SubjectId;

        SqlParameter p_ResultId = command.Parameters.Add("ResultId", DBNull.Value);
        p_ResultId.Direction = ParameterDirection.Input;
        p_ResultId.IsNullable = true;
        if (objEdu.ResultId != "")
            p_ResultId.Value = objEdu.ResultId;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = objEdu.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = objEdu.InsertedDate;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = strIsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = strIsDelete;



        SqlParameter p_PassedYear = command.Parameters.Add("PassedYear", DBNull.Value);
        p_PassedYear.Direction = ParameterDirection.Input;
        p_PassedYear.IsNullable = true;
        if (objEdu.PassedYear != "")
            p_PassedYear.Value = objEdu.PassedYear;

        SqlParameter p_Marks = command.Parameters.Add("Marks", DBNull.Value);
        p_Marks.Direction = ParameterDirection.Input;
        p_Marks.IsNullable = true;
        if (objEdu.Marks != "")
            p_Marks.Value = objEdu.Marks;

        SqlParameter p_DegreeTitle = command.Parameters.Add("DegreeTitle", DBNull.Value);
        p_DegreeTitle.Direction = ParameterDirection.Input;
        p_DegreeTitle.IsNullable = true;
        if (objEdu.DegreeTitle != "")
            p_DegreeTitle.Value = objEdu.DegreeTitle;

        SqlParameter p_IsMaxDegree = command.Parameters.Add("IsMaxDegree", DBNull.Value);
        p_IsMaxDegree.Direction = ParameterDirection.Input;
        p_IsMaxDegree.IsNullable = true;
        if (objEdu.IsMaxDegree != "")
            p_IsMaxDegree.Value = objEdu.IsMaxDegree;

        //@PassedYear char(4),
        //@Marks varchar(20),
        //@DegreeTitle varchar(100),
        //@IsMaxDegree char(1),

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

    //Select degree
    public DataTable SelectDegree(int intId, string strIsActive,string strIsProfessional)
    {
        string strSQL = "";
        string strCond = "";

        if (objDC.ds.Tables["tblDegreeList"] != null)
        {
            objDC.ds.Tables["tblDegreeList"].Rows.Clear();
            objDC.ds.Tables["tblDegreeList"].Dispose();
        }

        if (intId != 0)
            strCond = " AND DegreeId= " + intId;

        if (strIsProfessional != "")
            strCond = strCond + " AND IsProfessional='Y'";

        if (strIsActive == "Y")
            strCond = strCond + " AND IsActive='" + strIsActive + "'";

        strSQL = "SELECT * FROM DegreeList WHERE IsDeleted='N'" + strCond + " ORDER BY DegreeName";

        return objDC.CreateDT(strSQL, "tblDegreeList");
    }

    //Select Institute
    public DataTable SelectInstitute(int intId, string strIsActive)
    {
        string strSQL = "";
        string strCond = "";

        if (intId != 0)
            strCond = " AND InstituteId= " + intId;

        if (strIsActive == "Y")
            strCond = strCond + " AND IsActive='" + strIsActive + "'";

        strSQL = "SELECT * FROM InstituteList WHERE IsDeleted='N'" + strCond + " ORDER BY InstituteName";

        return objDC.CreateDT(strSQL, "tblInstituteList");
    }

    //Select subject
    public DataTable SelectSubject(int intId, string strIsActive)
    {
        string strSQL = "";
        string strCond = "";

        if (intId != 0)
            strCond = " AND SubjectId= " + intId;

        if (strIsActive == "Y")
            strCond = strCond + " AND IsActive='" + strIsActive + "'";

        strSQL = "SELECT * FROM SubjectList WHERE IsDeleted='N'" + strCond + " ORDER BY SubjectName";

        return objDC.CreateDT(strSQL, "tblSubjectList");
    }

    //Select degree
    public DataTable SelectResult(int intId, string strIsActive)
    {
        string strSQL = "";
        string strCond = "";

        if (intId != 0)
            strCond = " AND ResultId= " + intId;

        if (strIsActive == "Y")
            strCond = strCond + " AND IsActive='" + strIsActive + "'";

        strSQL = "SELECT * FROM ResultList WHERE IsDeleted='N'" + strCond + " ORDER BY ResultName";

        return objDC.CreateDT(strSQL, "tblResultList");
    }

    public DataTable SelectEmpEducation(long EduId, string EmpId,string IsMaxDegree)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpEducation");

        SqlParameter p_EduId = command.Parameters.Add("EduId", SqlDbType.BigInt);
        p_EduId.Direction = ParameterDirection.Input;
        p_EduId.Value = EduId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_IsMaxDegree = command.Parameters.Add("IsMaxDegree", SqlDbType.Char);
        p_IsMaxDegree.Direction = ParameterDirection.Input;
        p_IsMaxDegree.Value = IsMaxDegree;

        objDC.CreateDSFromProc(command, "tblEmpEducation");
        return objDC.ds.Tables["tblEmpEducation"];
    }
    #endregion

    #region Select HR Action

    //Select type wise action
    public DataTable SelectNatureWiseAction(string strActionNature)
    {
        string strSQL = "SELECT ActionId,ActionName FROM ActionList WHERE ISACTIVE='Y' "
        + " AND ActionNature='" + strActionNature + "'";

        return objDC.CreateDT(strSQL, "ActionList");
    }

    public DataTable SelectAction(Int32 ActionId)
    {
        if (objDC.ds.Tables["ActionList"] != null)
        {
            objDC.ds.Tables["ActionList"].Rows.Clear();
            objDC.ds.Tables["ActionList"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_Select_Action");

        SqlParameter p_ActionId = command.Parameters.Add("ActionId", SqlDbType.BigInt);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.Value = ActionId;

        objDC.CreateDSFromProc(command, "ActionList");
        return objDC.ds.Tables["ActionList"];
    }


    //Select Employee Information for HR Action
    public DataTable SelectEmpInfoHRAction(string EmpID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpInfo_HRAction");
        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;
        objDC.CreateDSFromProc(command, "tblEmpInfoHrAction");
        return objDC.ds.Tables["tblEmpInfoHrAction"];
    }

    //Select type wise action
    




    //Select Additional Responsibility information
    public DataTable SelectAddResponsibility(Int32 AddResponseId, string EmpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpAddResponsibilityLog");
        SqlParameter p_AddResponseId = command.Parameters.Add("AddResponseId", SqlDbType.BigInt);
        p_AddResponseId.Direction = ParameterDirection.Input;
        p_AddResponseId.Value = AddResponseId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDSFromProc(command, "tblEmpAddResponsibilityLog");
        return objDC.ds.Tables["tblEmpAddResponsibilityLog"];
    }

    //Select Confrimation information
    public DataTable SelectConfirmation(Int32 ConfirmId, string EmpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpConfirmationLog");
        SqlParameter p_ConfirmId = command.Parameters.Add("ConfirmId", SqlDbType.BigInt);
        p_ConfirmId.Direction = ParameterDirection.Input;
        p_ConfirmId.Value = ConfirmId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDSFromProc(command, "tblConfirmationLog");
        return objDC.ds.Tables["tblConfirmationLog"];
    }

    //Select Confrimation information
    public DataTable SelectDisciplinary( string EmpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpDisciplinaryLog");
     
        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDSFromProc(command, "tblDisciplinaryLog");
        return objDC.ds.Tables["tblDisciplinaryLog"];
    }

    //Select Transition Log information
    public DataTable SelectEmpTransitionLog( string EmpID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpTransitionLog");

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        objDC.CreateDSFromProc(command, "EmpTransitionLog");
        return objDC.ds.Tables["EmpTransitionLog"];
    }
    //Select Transition Log information
    public DataTable SelectEmpTempDutyAssignLog( string EmpID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpTempDutyAssignLog");

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        objDC.CreateDSFromProc(command, "EmpTempDutyAssignLog");
        return objDC.ds.Tables["EmpTempDutyAssignLog"];
    }
    //Select Contract Extension information
    public DataTable SelectEmpContractExtLog(Int32 ConExtId, string EmpID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpContractExtLog");

        SqlParameter p_ContractExtId = command.Parameters.Add("ContractExtId", SqlDbType.BigInt);
        p_ContractExtId.Direction = ParameterDirection.Input;
        p_ContractExtId.Value = ConExtId;

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;


        objDC.CreateDSFromProc(command, "EmpContractExtLog");
        return objDC.ds.Tables["EmpContractExtLog"];
    }
    public DataTable SelectEmpExperience( string EmpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpExperience");
      

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDSFromProc(command, "tblEmpExperience");
        return objDC.ds.Tables["tblEmpExperience"];
    }

    public DataTable SelectEmergencyContact(string EmpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpEmergencyContact");
        
        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDSFromProc(command, "tblEmergencyContact");
        return objDC.ds.Tables["tblEmergencyContact"];
    }

    public DataTable SelectNominee( string EmpId, string NomineeType)
    {
        SqlCommand command = new SqlCommand("proc_Select_Nominee");
      
        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_NomineeType = command.Parameters.Add("NomineeType", SqlDbType.Char);
        p_NomineeType.Direction = ParameterDirection.Input;
        p_NomineeType.Value = NomineeType;

        objDC.CreateDSFromProc(command, "tblNominee");
        return objDC.ds.Tables["tblNominee"];
    }

    public DataTable SelectMedicalBenefit(long BenefitId, string EmpId, string sMedFiscalYrId)
    {
        SqlCommand command = new SqlCommand("proc_Select_MedicalBenefit");
        SqlParameter p_BenefitId = command.Parameters.Add("BenefitId", SqlDbType.BigInt);
        p_BenefitId.Direction = ParameterDirection.Input;
        p_BenefitId.Value = BenefitId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_MedFiscalYrId = command.Parameters.Add("MedFiscalYrId", SqlDbType.BigInt);
        p_MedFiscalYrId.Direction = ParameterDirection.Input;
        p_MedFiscalYrId.Value = sMedFiscalYrId;

        objDC.CreateDSFromProc(command, "tblMedicalBenefit");
        return objDC.ds.Tables["tblMedicalBenefit"];
    }

    public string  SelectMedicalHospitalBalance(string sEmpId, string sFiscalYrId, string sBenefitType)
    {
        string strSQL = "SELECT Limit-SUM(ApproveAmount) AS Balance FROM MedicalBenefit WHERE EmpId='" + sEmpId + "' AND MedFiscalYrId=" + sFiscalYrId
            + " AND BenefitType='" + sBenefitType + "' GROUP BY Limit";
        SqlCommand command = new SqlCommand(strSQL);

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = sEmpId;

        SqlParameter p_FiscalYrId = command.Parameters.Add("MedFiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = sFiscalYrId;

        SqlParameter p_BenefitType = command.Parameters.Add("BenefitType", SqlDbType.Char);
        p_BenefitType.Direction = ParameterDirection.Input;
        p_BenefitType.Value = sBenefitType;

        return objDC.GetScalarVal(command); 
    }

    public string SelectMedicalCost(string sEmpId, string sFiscalYrId,string sType)
    {
        string strSQL="";
        if (sType == "M")
            strSQL = "SELECT MedicalCost FROM MedicalSetup WHERE EmpId='" + sEmpId + "' AND MedFiscalYrId=" + sFiscalYrId;
        else
            strSQL = "SELECT HospitalCost FROM MedicalSetup WHERE EmpId='" + sEmpId + "' AND MedFiscalYrId=" + sFiscalYrId;

        SqlCommand command = new SqlCommand(strSQL);

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = sEmpId;

        SqlParameter p_FiscalYrId = command.Parameters.Add("MedFiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = sFiscalYrId;

        return objDC.GetScalarVal(command);
    }

    public DataTable SelectWitness( string EmpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_WitnessList");
      
        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDSFromProc(command, "tblWitnessList");
        return objDC.ds.Tables["tblWitnessList"];
    }

    #endregion

    public DataTable SelectProfDegree(string strIsActive)
    {
        string strSQL = "";
        string strCond = "";

        if (strIsActive == "Y")
            strCond = strCond + " AND IsActive='" + strIsActive + "'";

        strSQL = "SELECT * FROM DegreeList WHERE IsProfessional='Y' and IsDeleted='N'" + strCond;

        return objDC.CreateDT(strSQL, "tblDegreeList");
    }

    public DataTable SelectEmpMedicalBalance(string EmpID)
    {
        if (objDC.ds.Tables["MedicalSetup"] != null)
        {
            objDC.ds.Tables["MedicalSetup"].Rows.Clear();
            objDC.ds.Tables["MedicalSetup"].Dispose();
        }

        string strSQL = "";

        strSQL = "SELECT * FROM MedicalSetup WHERE EmpId='" + EmpID + "'";

        return objDC.CreateDT(strSQL, "tblMedicalSetup");
    }   

    public DataTable SelectEmpActionLog(string EmpID)
    {
        if (objDC.ds.Tables["EmpActionLog"] != null)
        {
            objDC.ds.Tables["EmpActionLog"].Rows.Clear();
            objDC.ds.Tables["EmpActionLog"].Dispose();
        }

        string strSQL = "";

        strSQL = "SELECT A.ActionName,EA.ActionDate FROM EmpActionLog EA,ActionList A WHERE EA.ActionId=A.ActionId AND EA.EmpId='" + EmpID + "'";

        return objDC.CreateDT(strSQL, "tblEmpActionLog");
    }

    public DataTable SelectEmpInfoEmpTypeWs(string strGradeId, string strEmpType)
    {
        string strSQL = "";
        if (strGradeId == "")
        {
            strSQL = "SELECT E.EmpId,E.FullName,E.GrossSalary,E.BasicSalary,E.ConfirmationDate,SPM.SalPakId FROM EmpInfo E,SalaryPakMst SPM WHERE "
                + "  SPM.SalPakId=E.SalPakId AND E.EmpStatus='A' AND IsDeleted='N' AND E.EmpTypeId=" + strEmpType + " ORDER BY E.EmpId";
        }
        else
        {
            strSQL = "SELECT E.EmpId,E.FullName,E.GrossSalary,E.BasicSalary,E.ConfirmationDate,SPM.SalPakId FROM EmpInfo E,SalaryPakMst SPM WHERE "
                + "  SPM.SalPakId=E.SalPakId AND E.EmpStatus='A' AND IsDeleted='N' AND E.EmpTypeId=" + strEmpType + "AND E.GradeId=" + strGradeId + " ORDER BY E.EmpId";
        }
        return objDC.CreateDT(strSQL, "EmpInfoGrWs");
    }
     
    public DataTable SelectTrainService(string EmpID, string strFiscalYrId)
    {
        if (objDC.ds.Tables["tblTrainingService"] != null)
        {
            objDC.ds.Tables["tblTrainingService"].Rows.Clear();
            objDC.ds.Tables["tblTrainingService"].Dispose();
        }

        string strSQL = "";

        strSQL = "SELECT ServAgreement,AgrStartDate,AgrEndDate FROM TrainingService WHERE EmpId='" + EmpID + "' AND FiscalYrID=" +strFiscalYrId;

        return objDC.CreateDT(strSQL, "tblTrainingService");
    }
    
    public DataTable SelectEmpEmailAddress(string empID)
    
    {
        if (objDC.ds.Tables["tblEmpEmailAddress"] != null)
        {
            objDC.ds.Tables["tblEmpEmailAddress"].Clear();
            objDC.ds.Tables["tblEmpEmailAddress"].Dispose();
        }
      

        SqlCommand comand = new SqlCommand();
        comand = new SqlCommand("proc_Select_EmpEmailAddress");

        SqlParameter p_EmpID = comand.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = empID;

        objDC.CreateDSFromProc(comand, "tblEmpEmailAddress");
        return objDC.ds.Tables["tblEmpEmailAddress"];
 
    }

    public DataTable SelectEmpSeparation(Int32 SeparationId, string EmpID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpSeparationLog");

        SqlParameter p_SeparationId = command.Parameters.Add("SeparationId", SqlDbType.BigInt);
        p_SeparationId.Direction = ParameterDirection.Input;
        p_SeparationId.Value = SeparationId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpID;

        objDC.CreateDSFromProc(command, "EmpSeparationLog");
        return objDC.ds.Tables["EmpSeparationLog"];
    }

    //Insert or update Employee Salary Amendment
    public void InsertEmpSalaryAmendment(string strLogId, string strEmpId, string strActionId, string strActionName, string strActionDate,
       string strBasicSal, string sRemarks, string sIncPercentage, string strInsertedBy, string strInsertedDate, string IsUpdate, string IsDelete, string strSalPackId, DataTable dtSalPackUpdate)
    {
        SqlCommand[] command = new SqlCommand[4 + dtSalPackUpdate.Rows.Count];
        command[0] = new SqlCommand("proc_Insert_EmpSalaryAmendment");
        command[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_ConfirmId = command[0].Parameters.Add("LogId", SqlDbType.BigInt);
        p_ConfirmId.Direction = ParameterDirection.Input;
        p_ConfirmId.Value = strLogId;

        SqlParameter p_EmpId = command[0].Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_ActionId = command[0].Parameters.Add("ActionId", SqlDbType.BigInt);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.Value = strActionId;

        SqlParameter p_ConfirmDate = command[0].Parameters.Add("ActionDate", DBNull.Value);
        p_ConfirmDate.Direction = ParameterDirection.Input;
        p_ConfirmDate.IsNullable = true;
        if (strActionDate != "")
            p_ConfirmDate.Value = strActionDate;

        SqlParameter p_BasicSal = command[0].Parameters.Add("IncAmount", DBNull.Value);
        p_BasicSal.Direction = ParameterDirection.Input;
        p_BasicSal.IsNullable = true;
        if (strBasicSal != "")
            p_BasicSal.Value = strBasicSal;

        SqlParameter p_IncPercent = command[0].Parameters.Add("IncPercent", DBNull.Value);
        p_IncPercent.Direction = ParameterDirection.Input;
        p_IncPercent.IsNullable = true;
        if (sIncPercentage != "")
            p_IncPercent.Value = sIncPercentage;

        SqlParameter p_Remarks = command[0].Parameters.Add("Remarks", SqlDbType.VarChar);
        p_Remarks.Direction = ParameterDirection.Input;
        p_Remarks.Value = sRemarks;

        SqlParameter p_InsertedBy = command[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsertedBy;

        SqlParameter p_InsertedDate = command[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsertedDate;

        SqlParameter p_IsUpdate = command[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command[0].Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        if (IsUpdate == "N")
        {
            ////command[1] = InsertEmpInfoLog(strEmpId);

            ////command[2] = UpdateEmpSalaryAmendment(strEmpId, strActionId, strActionDate, strBasicSal, strInsertedBy, strInsertedDate);

            command[3] = InsertEmpActionLog(strEmpId, strActionId,  strActionDate, strInsertedBy, strInsertedDate);
            
            //Housing & PF Allowance Update     
            ////int i = 4;
            ////if (dtSalPackUpdate.Rows.Count > 0)
            ////{
            ////    foreach (SqlCommand cmdSal in this.GetSalPackDetUpdateCommand(dtSalPackUpdate, strSalPackId, strInsertedBy, strInsertedDate, "Salary Amendment"))
            ////    {
            ////        command[i] = cmdSal;
            ////        i++;
            ////    }
            ////}
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

    //Insert or update Employee Salary Amendment
    public void InsertEmpSalaryIncrement(string strLogId, string strEmpId, string strActionId, string strActionName, string strActionDate,
       string strCOLA, string strGroupPer, string strInvPer, string strNewGrossSal, string strBasicSal, string sRemarks, string sIncPer, string sIncAmnt,
        string strInsertedBy, string strInsertedDate, string IsUpdate, string IsDelete, 
        string strSalPackId, DataTable dtSalPackUpdate)
    {
        SqlCommand[] command = new SqlCommand[7 + (dtSalPackUpdate.Rows.Count*2)];
        command[0] = new SqlCommand("proc_Insert_EmpSalaryIncrement");
        command[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_ConfirmId = command[0].Parameters.Add("LogId", SqlDbType.BigInt);
        p_ConfirmId.Direction = ParameterDirection.Input;
        p_ConfirmId.Value = strLogId;

        SqlParameter p_EmpId = command[0].Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_ActionId = command[0].Parameters.Add("ActionId", SqlDbType.BigInt);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.Value = strActionId;

        SqlParameter p_ConfirmDate = command[0].Parameters.Add("ActionDate", DBNull.Value);
        p_ConfirmDate.Direction = ParameterDirection.Input;
        p_ConfirmDate.IsNullable = true;
        if (strActionDate != "")
            p_ConfirmDate.Value = strActionDate;

        SqlParameter p_COLA = command[0].Parameters.Add("COLA", SqlDbType.Decimal);
        p_COLA.Direction = ParameterDirection.Input;
        p_COLA.Value = strCOLA;

        SqlParameter p_GroupPer = command[0].Parameters.Add("GroupPer", SqlDbType.Decimal);
        p_GroupPer.Direction = ParameterDirection.Input;
        p_GroupPer.Value = strGroupPer;

        SqlParameter p_InvPer = command[0].Parameters.Add("InvPer", SqlDbType.Decimal);
        p_InvPer.Direction = ParameterDirection.Input;
        p_InvPer.Value = strInvPer;

        SqlParameter p_NewGrossSal = command[0].Parameters.Add("NewGrossSalary", DBNull.Value);
        p_NewGrossSal.Direction = ParameterDirection.Input;
        p_NewGrossSal.IsNullable = true;
        if (strNewGrossSal != "")
            p_NewGrossSal.Value = strNewGrossSal;

        SqlParameter p_IncAmount = command[0].Parameters.Add("IncPercent", DBNull.Value);
        p_IncAmount.Direction = ParameterDirection.Input;
        p_IncAmount.IsNullable = true;
        if (sIncPer != "")
            p_IncAmount.Value = sIncPer;

        SqlParameter p_IncAmnt = command[0].Parameters.Add("IncAmount", DBNull.Value);
        p_IncAmnt.Direction = ParameterDirection.Input;
        p_IncAmnt.IsNullable = true;
        if (sIncAmnt != "")
            p_IncAmnt.Value = sIncAmnt;

        SqlParameter p_Remarks = command[0].Parameters.Add("Remarks", SqlDbType.VarChar);
        p_Remarks.Direction = ParameterDirection.Input;
        p_Remarks.Value = sRemarks;

        SqlParameter p_InsertedBy = command[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsertedBy;

        SqlParameter p_InsertedDate = command[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsertedDate;

        SqlParameter p_IsUpdate = command[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        if (IsUpdate == "N")
        {
            ////command[1] = InsertEmpInfoLog(strEmpId);
            command[2] = UpdateEmpHRBasicGross(strEmpId, strBasicSal, strNewGrossSal, strActionId, strActionDate, strInsertedBy, strInsertedDate);

            command[3] = InsertEmpActionLog(strEmpId, strActionId, strActionDate, strInsertedBy, strInsertedDate);

            if (string.IsNullOrEmpty(GetEmpIdWsSalHisPakId(strSalPackId, strEmpId)) == true)
                command[4] = InsertSalaryPakHisDetls(strSalPackId, strEmpId, strActionDate,strInsertedBy, strInsertedDate, "Salary Package");

            //Basic, Housing, Medical & PF Allowance Update   
            int i = 5;
            if (dtSalPackUpdate.Rows.Count > 0)
            {
                foreach (SqlCommand cmdSal in this.GetSalPackDetUpdateCommand(dtSalPackUpdate, strSalPackId, strInsertedBy, strInsertedDate, "Salary Increment"))
                {
                    command[i] = cmdSal;
                    i++;
                }
            }
            i++;
            command[i] = InsertSalaryPakHisDetls(strSalPackId, strEmpId,strActionDate, strInsertedBy, strInsertedDate, "Increment");
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

    public SqlCommand UpdateEmpHRBasicGross(string strEmpId, string strBasicSalary,string strGrossSalary,string strActionId,string strActionDate, string strInsertedBy, string strInsertedDate)
    {
        string strSQL = "UPDATE EmpInfo SET BasicSalary=" + strBasicSalary + ",GrossSalary=" + strGrossSalary + ",ActionId=" + strActionId + 
            ",ActionDate='" + strActionDate + "',IncrementDate='" + strActionDate + "',UpdatedBy='" + strInsertedBy + "',UpdatedDate='" + strInsertedDate + "' WHERE EmpId='" + strEmpId + "'";

        SqlCommand command = new SqlCommand(strSQL);

        SqlParameter p_CourseId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_CourseId.Direction = ParameterDirection.Input;
        p_CourseId.Value = strEmpId;

        SqlParameter p_BasicSalary = command.Parameters.Add("BasicSalary", SqlDbType.Decimal);
        p_BasicSalary.Direction = ParameterDirection.Input;
        p_BasicSalary.Value = strBasicSalary;

        SqlParameter p_GrossSalary = command.Parameters.Add("GrossSalary", SqlDbType.Decimal);
        p_GrossSalary.Direction = ParameterDirection.Input;
        p_GrossSalary.Value = strGrossSalary;

        SqlParameter p_ActionId = command.Parameters.Add("ActionId", SqlDbType.BigInt);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.Value = strActionId;

        SqlParameter p_leavingDate = command.Parameters.Add("ActionDate", DBNull.Value);
        p_leavingDate.Direction = ParameterDirection.Input;
        p_leavingDate.IsNullable = true;
        if (strActionDate != "")
            p_leavingDate.Value = strActionDate;

        SqlParameter p_IncrementDate = command.Parameters.Add("IncrementDate", DBNull.Value);
        p_IncrementDate.Direction = ParameterDirection.Input;
        p_IncrementDate.IsNullable = true;
        if (strActionDate != "")
            p_IncrementDate.Value = strActionDate;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsertedDate;

        return command;
    }

    public SqlCommand InsertSalaryPakHisDetls(string strSalPakId, string strEmpId, string strEffDate, string strInsertedBy, string strInsertedDate, string strLastUpdatedFrom)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Proc_INSERT_SalaryPakHisDetls");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter p_SalPakId = cmd.Parameters.Add("SalPakId", SqlDbType.BigInt);
            p_SalPakId.Direction = ParameterDirection.Input;
            p_SalPakId.Value = strSalPakId;

            SqlParameter p_EmpID = cmd.Parameters.Add("EmpId", SqlDbType.VarChar);
            p_EmpID.Direction = ParameterDirection.Input;
            p_EmpID.Value = strEmpId;

            SqlParameter p_EffDate = cmd.Parameters.Add("EffDate", SqlDbType.DateTime);
            p_EffDate.Direction = ParameterDirection.Input;
            p_EffDate.Value = strEffDate;

            SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = strInsertedBy;

            SqlParameter p_InsertedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate.Direction = ParameterDirection.Input;
            p_InsertedDate.Value = strInsertedDate;

            SqlParameter p_LastUpdatedFrom = cmd.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
            p_LastUpdatedFrom.Direction = ParameterDirection.Input;
            p_LastUpdatedFrom.Value = strLastUpdatedFrom;

            return cmd;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    public DataTable SelectEmpInfoForLedger(string EmpID, string sbuID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpInfo_ForLedger");
        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        SqlParameter p_SbuId = command.Parameters.Add("SbuId", SqlDbType.BigInt);
        p_SbuId.Direction = ParameterDirection.Input;
        p_SbuId.Value = sbuID;

        objDC.CreateDSFromProc(command, "SelectEmpInfoForLedger");
        return objDC.ds.Tables["SelectEmpInfoForLedger"];
    }

    public DataTable GetEmpDesignation(string strId)
    {
        string strSQL = "select DesigId,DesigName from Designation where IsDeleted='N'";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        objDC.CreateDT(command, "Designation");
        return objDC.ds.Tables["Designation"];
    }

    public DataTable SelectProjectList(int Id)
    {
        SqlCommand command = new SqlCommand("proc_Select_ProjectList");

        SqlParameter p_Id = command.Parameters.Add("ProjectId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = Id;

        objDC.CreateDSFromProc(command, "tblProjectList");
        return objDC.ds.Tables["tblProjectList"];
    }
    
    public DataTable SelectEmpForIncrement(string strClinicID)
    {
        string strSQL = "SELECT E.EMPID,E.FULLNAME,E.EmpTypeId,E.BasicSalary,E.GrossSalary,E.JoiningDate,E.IncrementDate,D.DesigName,C.ClinicName,ET.TypeName" +
            " FROM EMPINFO E,Designation D,ClinicList C,EmpTypeList ET" +
            " WHERE E.EmpStatus='A' AND E.DesigId=D.DesigId AND E.ClinicId=C.ClinicId AND E.EmpTypeId=ET.EmpTypeId AND E.ClinicId=@ClinicId" // AND E.EmpId IN('E006007','E006136')" 
            + " ORDER BY EMPID";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_DivId = command.Parameters.Add("ClinicId", SqlDbType.BigInt);
        p_DivId.Direction = ParameterDirection.Input;
        p_DivId.Value = strClinicID;

        objDC.CreateDT(command, "dtEmpForIncrement");
        return objDC.ds.Tables["dtEmpForIncrement"];
    }
}