using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for clsEmpInfo
/// </summary>
public class clsEmpInfo
{
    private string _EmpId;
    public string EmpId
    {
      get { return _EmpId; }
      set { _EmpId = value; }
    }

    private string _Title;
    public string Title
    {
        get { return _Title; }
        set { _Title = value; }
    }

    private string _FullName;
    public string FullName
    {
        get { return _FullName; }
        set { _FullName = value; }
    }

    private string _EmpFname;
    public string EmpFname
    {
        get { return _EmpFname; }
        set { _EmpFname = value; }
    }

    private string _EmpMname;
    public string EmpMname
    {
        get { return _EmpMname; }
        set { _EmpMname = value; }
    }

    private string _EmpLname;
    public string EmpLname
    {
        get { return _EmpLname; }
        set { _EmpLname = value; }
    }

    private string _FathersNm;
    public string FathersNm
        {
      get { return _FathersNm; }
      set { _FathersNm = value; }
    }

    private string _MothersNm;
    public string MothersNm
    {
      get { return _MothersNm; }
      set { _MothersNm = value; }
    }

    private string _PreAddress;
    public string PreAddress
    {
        get { return _PreAddress; }
        set { _PreAddress = value; }
    }

    private string _PrePhone;
    public string PrePhone
    {
        get { return _PrePhone; }
        set { _PrePhone = value; }
    }

    private string _PreFax;
    public string PreFax
    {
        get { return _PreFax; }
        set { _PreFax = value; }
    }

    private string _PerAddress;
    public string PerAddress
    {
        get { return _PerAddress; }
        set { _PerAddress = value; }
    }

    private string _PerPhone;
    public string PerPhone
    {
        get { return _PerPhone; }
        set { _PerPhone = value; }
    }

    private string _PerFax;
    public string PerFax
    {
        get { return _PerFax; }
        set { _PerFax = value; }
    }

    private string _PerDistrictID;
    public string PerDistrictID
    {
        get { return _PerDistrictID; }
        set { _PerDistrictID = value; }
    }

    private string _PerCountryID;
    public string PerCountryID
    {
        get { return _PerCountryID; }
        set { _PerCountryID = value; }
    }

    private string _Gender;
    public string Gender
    {
        get { return _Gender; }
        set { _Gender = value; }
    }

    private string _DOB;
    public string DOB
    {
        get { return _DOB; }
        set { _DOB = value; }
    }

    private string _Religion;
    public string Religion
    {
        get { return _Religion; }
        set { _Religion = value; }
    }

    private string _BloodGroup;
    public string BloodGroup
    {
        get { return _BloodGroup; }
        set { _BloodGroup = value; }
    }

    private string _MaritalStatus;
    public string MaritalStatus
    {
        get { return _MaritalStatus; }
        set { _MaritalStatus = value; }
    }

    private string _MarriageDate;
    public string MarriageDate
    {
        get { return _MarriageDate; }
        set { _MarriageDate = value; }
    }

    private string _Nationality;
    public string Nationality
    {
        get { return _Nationality; }
        set { _Nationality = value; }
    }

    private string _NationalId;
    public string NationalId
    {
        get { return _NationalId; }
        set { _NationalId = value; }
    }

    private string _DOBId;
    public string DOBId
    {
        get { return _DOBId; }
        set { _DOBId = value; }
    }

    private string _TINNo;
    public string TINNo
    {
        get { return _TINNo; }
        set { _TINNo = value; }
    }

    private string _Circle;
    public string Circle
    {
        get { return _Circle; }
        set { _Circle = value; }
    }

    private string _Zone;
    public string Zone
    {
        get { return _Zone; }
        set { _Zone = value; }
    }

    private string _PassportNo;
    public string PassportNo
    {
        get { return _PassportNo; }
        set { _PassportNo = value; }
    }

    private string _PassExpDate;
    public string PassExpDate
    {
        get { return _PassExpDate; }
        set { _PassExpDate = value; }
    }

    private string _PassIssOffice;
    public string PassIssOffice
    {
        get { return _PassIssOffice; }
        set { _PassIssOffice = value; }
    }

    private string _SkypeId;
    public string SkypeId
    {
        get { return _SkypeId; }
        set { _SkypeId = value; }
    }

    private string _OfficeExt;
    public string OfficeExt
    {
        get { return _OfficeExt; }
        set { _OfficeExt = value; }
    }

    private string _OfficeEmail;
    public string OfficeEmail
    {
        get { return _OfficeEmail; }
        set { _OfficeEmail = value; }
    }

    private string _CellPhone;
    public string CellPhone
    {
        get { return _CellPhone; }
        set { _CellPhone = value; }
    }

    private string _LandPhone;
    public string LandPhone
    {
        get { return _LandPhone; }
        set { _LandPhone = value; }
    }

    private string _PersonalEmail;
    public string PersonalEmail
    {
        get { return _PersonalEmail; }
        set { _PersonalEmail = value; }
    }

    private string _HighestEdu;
    public string HighestEdu
    {
        get { return _HighestEdu; }
        set { _HighestEdu = value; }
    }

    private string _ProffDegree;
    public string ProffDegree
    {
        get { return _ProffDegree; }
        set { _ProffDegree = value; }
    }

    private string _SpecialSkill;
    public string SpecialSkill
    {
        get { return _SpecialSkill; }
        set { _SpecialSkill = value; }
    }

    private string _IsRelativeInSC;
    public string IsRelativeInSC
    {
        get { return _IsRelativeInSC; }
        set { _IsRelativeInSC = value; }
    }

    private string _Relation;
    public string Relation
    {
        get { return _Relation; }
        set { _Relation = value; }
    }

    private string _IsSpectacled;
    public string IsSpectacled
    {
        get { return _IsSpectacled; }
        set { _IsSpectacled = value; }
    }

    private string _LicenseNo;
    public string LicenseNo
    {
        get { return _LicenseNo; }
        set { _LicenseNo = value; }
    }

    private string _LicenseExpDate;
    public string LicenseExpDate
    {
        get { return _LicenseExpDate; }
        set { _LicenseExpDate = value; }
    }

    private string _EmpPicLoc;
    public string EmpPicLoc
    {
        get { return _EmpPicLoc; }
        set { _EmpPicLoc = value; }
    }

    private string _RelativeInfo;
    public string RelativeInfo
    {
        get { return _RelativeInfo; }
        set { _RelativeInfo = value; }
    }
   
    private string _IsMedical;
    public string IsMedical
    {
        get { return _IsMedical; }
        set { _IsMedical = value; }
    }
    private string _Nature;
    public string Nature
    {
        get { return _Nature; }
        set { _Nature = value; }
    }
    private string _BMDCRegNo;
    public string BMDCRegNo
    {
        get { return _BMDCRegNo; }
        set { _BMDCRegNo = value; }
    }
    private string _BMDCRegDate;
    public string BMDCRegDate
    {
        get { return _BMDCRegDate; }
        set { _BMDCRegDate = value; }
    }

    private string _PreDistrictID;
    public string PreDistrictID
    {
        get { return _PreDistrictID; }
        set { _PreDistrictID = value; }
    }
    private string _PreCountryID;
    public string PreCountryID
    {
        get { return _PreCountryID; }
        set { _PreCountryID = value; }
    }
    private string _PrePostCode;
    public string PrePostCode
    {
        get { return _PrePostCode; }
        set { _PrePostCode = value; }
    }
    private string _PerPostCode;
    public string PerPostCode
    {
        get { return _PerPostCode; }
        set { _PerPostCode = value; }
    }
    private string _TNTCode;
    public string TNTCode
    {
        get { return _TNTCode; }
        set { _TNTCode = value; }
    }

    private string _NoofLiveChild;
    public string NoofLiveChild
    {
        get { return _NoofLiveChild; }
        set { _NoofLiveChild = value; }
    }

    private string _OldEmpId;
    public string OldEmpId
    {
        get { return _OldEmpId; }
        set { _OldEmpId = value; }
    }

    private string _SpouseName;
    public string SpouseName
    {
        get { return _SpouseName; }
        set { _SpouseName = value; }
    }
    
    public clsEmpInfo(
        string EmpId,
        string Title,
        string FullName,
        string EmpFname,
        string EmpMname,
        string EmpLname,
        string FathersNm,
        string MothersNm,
        string PreAddress,
        string PrePhone,
        string PreFax,
        string PerAddress,
        string PerPhone,
        string PerFax,
        string PerDistrictID,
        string PerCountryID,
        string Gender,
        string DOB,
        string Religion,
        string BloodGroup,
        string MaritalStatus,
        string MarriageDate,
        string Nationality,
        string NationalId,
        string DOBId,
        string TINNo,
        string Circle,
        string Zone,
        string PassportNo,
        string PassExpDate,
        string PassIssOffice,
        string SkypeId,
        string OfficeExt,
        string OfficeEmail,
        string CellPhone,
        string LandPhone,
        string PersonalEmail,
        string HighestEdu,
        string ProffDegree,
        string SpecialSkill,
        string IsRelativeInSC,
        string Relation,
        string IsSpectacled,
        string LicenseNo,
        string LicenseExpDate,
        string EmpPicLoc,
        string RelativeInfo,
        string IsMedical,
        string Nature,
        string BMDCRegNo,
        string BMDCRegDate,
        string PreDistrictID,
        string PreCountryID,
        string PrePostCode,
        string PerPostCode,
        string TNTCode,
        string NoofLiveChild,
        string OldEmpId,
        string SpouseName
        )
    {
        this.EmpId = EmpId;
        this.Title = Title;
        this.FullName = FullName;
        this.EmpFname = EmpFname;
        this.EmpMname = EmpMname;
        this.EmpLname = EmpLname;
        this.FathersNm = FathersNm;
        this.MothersNm = MothersNm;
        this.PreAddress = PreAddress;
        this.PrePhone = PrePhone;
        this.PreFax = PreFax;
        this.PerAddress = PerAddress;
        this.PerPhone = PerPhone;
        this.PerFax = PerFax;
        this.PerDistrictID = PerDistrictID;
        this.PerCountryID = PerCountryID;
        this.Gender = Gender;
        this.DOB = DOB;
        this.Religion = Religion;
        this.BloodGroup = BloodGroup;
        this.MaritalStatus = MaritalStatus;
        this.MarriageDate = MarriageDate;
        this.Nationality = Nationality;
        this.NationalId = NationalId;
        this.DOBId = DOBId;
        this.TINNo = TINNo;
        this.Circle = Circle;
        this.Zone = Zone;
        this.PassportNo = PassportNo;
        this.PassExpDate = PassExpDate;
        this.PassIssOffice = PassIssOffice;
        this.SkypeId = SkypeId;
        this.OfficeExt = OfficeExt;
        this.OfficeEmail = OfficeEmail;
        this.CellPhone = CellPhone;
        this.LandPhone = LandPhone;
        this.PersonalEmail = PersonalEmail;
        this.HighestEdu = HighestEdu;
        this.ProffDegree = ProffDegree;
        this.SpecialSkill = SpecialSkill;
        this.IsRelativeInSC = IsRelativeInSC;
        this.Relation = Relation;
        this.IsSpectacled = IsSpectacled;
        this.LicenseNo = LicenseNo;
        this.LicenseExpDate = LicenseExpDate;
        this.EmpPicLoc = EmpPicLoc;
        this.RelativeInfo = RelativeInfo;      
        this.IsMedical = IsMedical;
        this.Nature = Nature;
        this.BMDCRegNo = BMDCRegNo;
        this.BMDCRegDate = BMDCRegDate;
        this.PreDistrictID = PreDistrictID;
        this.PreCountryID = PreCountryID;
        this.PrePostCode = PrePostCode;
        this.PerPostCode = PerPostCode;
        this.TNTCode = TNTCode;
        this.NoofLiveChild = NoofLiveChild;
        this.OldEmpId = OldEmpId;
        this.SpouseName = SpouseName;
    }
}
