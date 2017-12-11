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
/// Summary description for clsEmpFamilyInfo
/// </summary>
public class clsEmpFamilyInfo
{
    private string _EmpID;

    public string EmpID
    {
        get { return _EmpID; }
        set { _EmpID = value; }
    }

    private string _FmId;

    public string FmId
    {
        get { return _FmId; }
        set { _FmId = value; }
    }


    private string _fmName;
    public string fmName
    {   get{return _fmName;}
        set { _fmName = value; }
    }


    private string _Fmocc;
    public string Fmocc
    {
        get { return _Fmocc; }
        set { _Fmocc = value; }
    }

    private string _frRelation;
    public string frRelation
    {
        get { return _frRelation; }
        set { _frRelation = value; }
    }
    private string _FmDoB;
    public string FmDoB
    {
        get { return _FmDoB; }
        set { _FmDoB = value; }
    }
    private string _FmBloodGrp;
    public string FmBloodGrp
    {
        get { return _FmBloodGrp; }
        set { _FmBloodGrp = value; }
    }

    private string _Isdependent;
    public string Isdependent
    {
        get { return _Isdependent; }
        set { _Isdependent = value; }
    }
    private string _InsertedBy; 
    public string InsertedBy
    {
        get { return _InsertedBy; }
        set { _InsertedBy = value; }
    }

    private string _InsertedDate;
    public string InsertedDate
    {
        get { return _InsertedDate; }
        set { _InsertedDate = value; }
    }

    private string _UpdatedBy;
    public string UpdatedBy
    {
        get { return _UpdatedBy; }
        set { _UpdatedBy = value; }
    }

    private string _UpdatedDate;
    public string UpdatedDate
    {
        get { return _UpdatedDate; }
        set { _UpdatedDate = value; }
    }

    private string _InsuranceID;
    public string InsuranceID
    {
        get { return _InsuranceID; }
        set { _InsuranceID = value; }
    }

    private string _InclusionDate;
    public string InclusionDate
    {
        get { return _InclusionDate; }
        set { _InclusionDate = value; }
    }

    private string _RenewalDate;
    public string RenewalDate
    {
        get { return _RenewalDate; }
        set { _RenewalDate = value; }
    }

    private string _EmpPicLoc;
    public string EmpPicLoc
    {
        get { return _EmpPicLoc; }
        set { _EmpPicLoc = value; }
    }



    public clsEmpFamilyInfo(string EmpID,
        string FmId,
        string fmName,
        string Fmocc,
        string frRelation,
        string FmDoB,
        string FmBloodGrp,
        string Isdependent,
        string InsertedBy,
        string InsertedDate,
        string InsID,
        string InclDate,
        string RenDate,
        string PicLoc
        )
    {
        this.EmpID = EmpID;
        this.FmId = FmId;
        this.fmName = fmName;
        this.Fmocc = Fmocc;
        this.frRelation = frRelation;
        this.FmDoB = FmDoB;
        this.FmBloodGrp = FmBloodGrp;
        this.Isdependent = Isdependent;
        this.InsertedBy = InsertedBy;
        this.InsertedDate = InsertedDate;
        this.InsuranceID = InsID;
        this.InclusionDate = InclDate;
        this.RenewalDate = RenDate;
        this.EmpPicLoc = PicLoc;
    }
}
