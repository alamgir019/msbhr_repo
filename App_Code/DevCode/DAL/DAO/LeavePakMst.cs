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
/// Summary LeaveDesc for LeavePakMst
/// </summary>
/// 


public class LeavePakMst
{
    private string _LPakID;

    public string LPakID
    {
        get { return _LPakID; }
        set { _LPakID = value; }
    }

    private string _LPackName;

    public string LPackName
    {
        get { return _LPackName; }
        set { _LPackName = value; }
    }


    private string _LPdesc;

    public string LPdesc
    {
        get { return _LPdesc; }
        set { _LPdesc = value; }
    }

    private string _IsOffdayCounted;

    public string IsOffdayCounted
    {
        get { return _IsOffdayCounted; }
        set { _IsOffdayCounted = value; }
    }

    private string _IsLCalOnJoinDate;

    public string IsLCalOnJoinDate
    {
        get { return _IsLCalOnJoinDate; }
        set { _IsLCalOnJoinDate = value; }
    }


    
    private string _ISDefault;

    public string ISDefault
    {
        get { return _ISDefault; }
        set { _ISDefault = value; }
    }

    private string _EmpTypeStatus;

    public string EmpTypeStatus
    {
        get { return _EmpTypeStatus; }
        set { _EmpTypeStatus = value; }
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

    private string _FromMonth;
    public string FromMonth
    {
        get { return _FromMonth; }
        set { _FromMonth = value; }
    }

    private string _ToMonth;
    public string ToMonth
    {
        get { return _ToMonth; }
        set { _ToMonth = value; }
    }
    private string _IsNextYear;
    public string IsNextYear
    {
        get { return _IsNextYear; }
        set { _IsNextYear = value; }
    }
    private string _LeaveStartPeriod;
    public string LeaveStartPeriod
    {
        get { return _LeaveStartPeriod; }
        set { _LeaveStartPeriod = value; }
    }

    private string _LeaveEndPeriod;
    public string LeaveEndPeriod
    {
        get { return _LeaveEndPeriod; }
        set { _LeaveEndPeriod = value; }
    }

    public LeavePakMst(string LPakID, string LPackName, string LPdesc, string IsOffdayCounted,
        string IsLCalOnJoinDate, string IsActive, string EmpTypeStatus,string ISDefault, string InsertedBy,
        string InsertedDate, string IsDeleted, string FromMonth, string ToMonth,string IsNextYear,
        string LeaveStartPeriod, string LeaveEndPeriod)
    {
        this.LPakID = LPakID;
        this.LPackName = LPackName;
        this.LPdesc = LPdesc;
        this.IsOffdayCounted = IsOffdayCounted;
        this.IsLCalOnJoinDate = IsLCalOnJoinDate;   
        this.ISDefault = ISDefault;
        this.EmpTypeStatus = EmpTypeStatus;
        this.InsertedBy = InsertedBy;
        this.InsertedDate = InsertedDate;                
        this.IsActive = IsActive;
        this.IsDeleted = IsDeleted;  
        this.FromMonth = FromMonth;      
        this.ToMonth = ToMonth;
        this.IsNextYear = IsNextYear;
        this.LeaveStartPeriod = LeaveStartPeriod;
        this.LeaveEndPeriod = LeaveEndPeriod;      
    }
}

