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
/// Summary description for SalaryPakMst
/// </summary>
public class Payroll_SalaryPakMst
{
    private string _SalPakId;
    public string SalPakId
    {
        get { return _SalPakId; }
        set { _SalPakId = value; }
    }

    private string _SPTitle;
    public string SPTitle
    {
        get { return _SPTitle; }
        set { _SPTitle = value; }
    }


    private string _SPDesc;
    public string SPDesc
    {
        get { return _SPDesc; }
        set { _SPDesc = value; }
    }

    private string _CurrId;  
   public string CurrId
    {
        get { return _CurrId; }
        set { _CurrId = value; }
    }


    private string _WillConvert;  
    public string WillConvert
    {
        get { return _WillConvert; }
        set { _WillConvert = value; }
    }

    private string _PayType;  
    public string PayType
    {
        get { return _PayType; }
        set { _PayType= value; }
    }

    private string _OTAmt; 
     public string OTAmt
    {
        get { return _OTAmt; }
        set { _OTAmt= value; }
    }

    private string _IsInPercent; 
    public string IsInPercent
    {
        get { return _IsInPercent; }
        set { _IsInPercent= value; }
    }

    private string _SalHeadID; 
    public string SalHeadID
    {
        get { return _SalHeadID; }
        set { _SalHeadID= value; }
    }

    private string _AttBonusAmt; 
    public string  AttBonusAmt
    {
        get { return _AttBonusAmt; }
        set { _AttBonusAmt= value; }
    }


    private string _IsBonusInPer;
    public string IsBonusInPer
    {
        get { return _IsBonusInPer; }
        set { _IsBonusInPer = value; }
    }
    private string _SalHeadIDBonus; 
    public string  SalHeadIDBonus
    {
        get { return _SalHeadIDBonus; }
        set { _SalHeadIDBonus= value; }
    }

    private string _LateCount;
     public string  LateCount
        {
            get { return _LateCount; }
            set { _LateCount= value; }
        }

    private string _LateSalCount;
    public string  LateSalCount
        {
            get { return _LateSalCount; }
            set { _LateSalCount= value; }
        }



    private string _LateSalHead;
    public string LateSalHead
        {
            get { return _LateSalHead; }
            set { _LateSalHead = value; }
        }



     private string _TotalGrossSal;
     public string  TotalGrossSal
        {
            get { return _TotalGrossSal; }
            set { _TotalGrossSal= value; }
        }

     private string _IsAutoGrossCalc;
     public string  IsAutoGrossCalc
        {
            get { return _IsAutoGrossCalc; }
            set { _IsAutoGrossCalc= value; }
        }

     private string _totalSalary;
     public string  totalSalary
        {
            get { return _totalSalary; }
            set { _totalSalary= value; }
        }


     private string _IsActive;
     public string  IsActive
        {
            get { return _IsActive; }
            set { _IsActive= value; }
        }

    private string _IsCompFacility;
    public string IsCompFacility
    {
        get { return _IsCompFacility; }
        set { _IsCompFacility= value; }
    }


    private string _PackageID;
    public string PackageID
    {
        get { return _PackageID; }
        set { _PackageID= value; }
    }

    //private string _IsDeleted;

    //public string IsDeleted
    //{
    //    get { return _IsDeleted; }
    //    set { _IsDeleted = value; }
    //}


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


    public Payroll_SalaryPakMst(string SalPakId, string SPTitle, string SPDesc, string CurrId, string WillConvert, string PayType, string OTAmt,
    string IsInPercent, string SalHeadID, string AttBonusAmt, string IsBonusInPer, string SalHeadIDBonus, string LateCount, string LateSalCount, string LateSalHead,
    string TotalGrossSal, string IsAutoGrossCalc, string totalSalary, string IsActive, string IsCompFacility,
    string PackageID, string InsertedBy, string InsertedDate)
    {
        this.SalPakId = SalPakId;
        this.SPTitle = SPTitle;
        this.SPDesc = SPDesc;
        this.CurrId = CurrId;

        this.WillConvert = WillConvert;
        this.PayType = PayType;
        this.OTAmt = OTAmt;

        this.IsInPercent = IsInPercent;
        this.SalHeadID = SalHeadID;
        this.AttBonusAmt = AttBonusAmt;

        this.IsBonusInPer = IsBonusInPer;

        this.SalHeadIDBonus = SalHeadIDBonus;
        this.LateCount = LateCount;
        this.LateSalCount = LateSalCount;
        this.LateSalHead = LateSalHead;

        this.TotalGrossSal = TotalGrossSal;
        this.IsAutoGrossCalc = IsAutoGrossCalc;
        this.totalSalary = totalSalary;
        this.IsActive = IsActive;
        this.IsCompFacility = IsCompFacility;
        this.PackageID = PackageID;
        this.InsertedBy = InsertedBy;
        this.InsertedDate = InsertedDate;
    }

  
}
