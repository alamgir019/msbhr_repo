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
/// Summary LeaveDesc for LeaveType
/// </summary>
public class LeaveType
{
    private string _LTypeID;

    public string LTypeID
    {
        get { return _LTypeID; }
        set { _LTypeID = value; }
    }
    private string _LTypeTitle;

    public string LTypeTitle
    {
        get { return _LTypeTitle; }
        set { _LTypeTitle = value; }
    }

    private string _LAbbrName;

    public string LAbbrName
    {
        get { return _LAbbrName; }
        set { _LAbbrName = value; }
    }


    private string _maxCarryLimit;

    public string maxCarryLimit
    {
        get { return _maxCarryLimit; }
        set { _maxCarryLimit = value; }
    }


    private string _LeaveDesc;

    public string LeaveDesc
    {
        get { return _LeaveDesc; }
        set { _LeaveDesc = value; }
    }

    private string _LMunit;

    public string LMunit
    {
        get { return _LMunit; }
        set { _LMunit = value; }
    }    

private string _LCalcType;

public string LCalcType
{
          get { return _LCalcType; }
          set { _LCalcType = value; }
}

private string _CalcInterval;    

public string CalcInterval
{
          get { return _CalcInterval; }
          set { _CalcInterval = value; }
}
    private string _CalcBase;

public string CalcBase
{
          get { return _CalcBase; }
          set { _CalcBase = value; }
}
    private string _LNature;

public string LNature
{
          get { return _LNature; }
          set { _LNature = value; }
}

     private string _LeaveTTL;

public string LeaveTTL
{
          get { return _LeaveTTL; }
          set { _LeaveTTL = value; }
}

     private string _MaxCarryCashDay;

public string MaxCarryCashDay
{
          get { return _MaxCarryCashDay; }
          set { _MaxCarryCashDay = value; }
}

     private string _Eligibility;

public string Eligibility
{
          get { return _Eligibility; }
          set { _Eligibility = value; }
}

    
 private string _NextLevInterval;

public string NextLevInterval
{
          get { return _NextLevInterval; }
          set { _NextLevInterval = value; }
}

     private string _TotalMatLev;

public string TotalMatLev
{
          get { return _TotalMatLev; }
          set { _TotalMatLev = value; }
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
   
    private string _IsActive;

    public string IsActive
    {
        get { return _IsActive; }
        set { _IsActive = value; }
    }
    private string _IsDeleted;

    public string IsDeleted
    {
        get { return _IsDeleted; }
        set { _IsDeleted = value; }
    }
    private string _FiscalYrId;

    public string FiscalYrId
    {
        get { return _FiscalYrId; }
        set { _FiscalYrId = value; }
    }

    private string _IsOffdayCounted;

    public string IsOffdayCounted
    {
        get { return _IsOffdayCounted; }
        set { _IsOffdayCounted = value; }
    }
    
        public LeaveType(string LTypeID,string maxCarryLimit, string LTypeTitle,string LAbbrName,string LeaveDesc, string LMunit,
        string LCalcType, string CalcInterval, string CalcBase, string LNature, string LeaveTTL, string MaxCarryCashDay, 
        string Eligibility, string NextLevInterval, string TotalMatLev,  string InsertedBy,
        string InsertedDate, string IsActive, string IsDeleted, string FiscalYrId, string IsOffdayCounted)//16
    {
        this.LTypeID = LTypeID;//1
        this.maxCarryLimit = maxCarryLimit;
        this.LTypeTitle = LTypeTitle;//2
        this.LAbbrName = LAbbrName;//3
        this.LeaveDesc = LeaveDesc;//4
        this.LMunit = LMunit;//5
        this.LCalcType = LCalcType;//6
        this.CalcInterval = CalcInterval;//7
        this.CalcBase = CalcBase;//8
        this.LNature = LNature;//9
        this.LeaveTTL = LeaveTTL;
        this.MaxCarryCashDay = MaxCarryCashDay;//10
        this.Eligibility = Eligibility;    //11   
        this.NextLevInterval = NextLevInterval;//12
        this.TotalMatLev = TotalMatLev;     //13  
        this.InsertedBy = InsertedBy;//14
        this.InsertedDate = InsertedDate;//15                
        this.IsActive = IsActive;//16
        this.IsDeleted = IsDeleted;  //17     
        this.FiscalYrId = FiscalYrId;  //17     
        this.IsOffdayCounted = IsOffdayCounted;  //17     
    }
}
