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
/// Summary description for LeaveApp
/// </summary>
public class LeaveApp
{
    private string _LvAppID;
    public string LvAppID
    {
        get { return _LvAppID; }
        set { _LvAppID = value; }
    }

    private string _EmpID;
    public string EmpID
    {
        get { return _EmpID; }
        set { _EmpID = value; }
    }

    private string _AppDate;
    public string AppDate
    {
        get { return _AppDate; }
        set { _AppDate = value; }
    }

    private string _LTypeId;
    public string LTypeId
    {
        get { return _LTypeId; }
        set { _LTypeId = value; }
    }

    private string _LTReason;
    public string LTReason
    {
        get { return _LTReason; }
        set { _LTReason = value; }
    }

    private string _AddrAtLeave;
    public string AddrAtLeave
    {
        get { return _AddrAtLeave; }
        set { _AddrAtLeave = value; }
    }
    private string _PhoneNo;
    public string PhoneNo
    {
        get { return _PhoneNo; }
        set { _PhoneNo = value; }
    }
    private string _LDurInDays;
    public string LDurInDays
    {
        get { return _LDurInDays; }
        set { _LDurInDays = value; }
    }
    private string _AppStatus;
    public string AppStatus
    {
        get { return _AppStatus; }
        set { _AppStatus = value; }
    }

    private string _AppType;
    public string AppType
    {
        get { return _AppType; }
        set { _AppType = value; }
    }

    private string _LevDate;
    public string LevDate
    {
        get { return _LevDate; }
        set { _LevDate = value; }
    }

    private string _IsUpdate;
    public string IsUpdate
    {
        get { return _IsUpdate; }
        set { _IsUpdate = value; }
    }
    private string _IsDelete;
    public string IsDelete
    {
        get { return _IsDelete; }
        set { _IsDelete = value; }
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
 
    private string _LeaveStart;
    public string LeaveStart
    {
        get { return _LeaveStart; }
        set { _LeaveStart = value; }
    }

    private string _LeaveEnd;
    public string LeaveEnd
    {
        get { return _LeaveEnd; }
        set { _LeaveEnd = value; }
    }

    private string _Duration;
    public string Duration
    {
        get { return _Duration; }
        set { _Duration = value; }
    }

    private string _LeaveEnjoyed;
    public string LeaveEnjoyed
    {
        get { return _LeaveEnjoyed; }
        set { _LeaveEnjoyed = value; }
    }
    private string _ResponsiveEmpId;
    public string ResponsiveEmpId
    {
        get { return _ResponsiveEmpId; }
        set { _ResponsiveEmpId = value; }
    }
    private string _IsHalfDay;
    public string IsHalfDay
    {
        get { return _IsHalfDay; }
        set { _IsHalfDay = value; }
    }

    private string _ResumeDate;
    public string ResumeDate
    {
        get { return _ResumeDate; }
        set { _ResumeDate = value; }
    }

    private string _FiscalYrId;
    public string FiscalYrId
    {
        get { return _FiscalYrId; }
        set { _FiscalYrId = value; }
    }

    public LeaveApp(string LvAppID, string EmpID, string AppDate, 
        string LTReason,string AddrAtLeave, string PhoneNo, string LDurInDays, 
        string AppStatus, string AppType,string InsertedBy, string InsertedDate,
        string IsUpdate, string IsDelete, string LTypeId, string LeaveStart,
        string LeaveEnd, string Duration,string ResponsiveEmpId,string IsHalfDay,string strResumeDate,string sFiscalYrId)
	{
        this.LvAppID = LvAppID;
        this.EmpID = EmpID;
        this.AppDate = AppDate;        
        this.LTReason = LTReason;
        this.AddrAtLeave = AddrAtLeave;
        this.PhoneNo = PhoneNo;
        this.LDurInDays = LDurInDays;
        this.AppStatus = AppStatus;
        this.AppType = AppType;
        this.InsertedBy = InsertedBy;
        this.InsertedDate = InsertedDate;
        this.IsUpdate = IsUpdate;
        this.IsDelete = IsDelete;
        this.LTypeId = LTypeId;
        this.LeaveStart = LeaveStart;
        this.LeaveEnd = LeaveEnd;
        this.Duration = Duration;
        this.ResponsiveEmpId = ResponsiveEmpId;
        this.IsHalfDay = IsHalfDay;
        this.ResumeDate = strResumeDate;
        this.FiscalYrId = sFiscalYrId;
	}

    public LeaveApp(string LvAppID, string AppStatus,string EmpID,string LTypeID,  string LeaveEnjoyed,string InsertedBy, string InsertedDate)
    {
        this.LvAppID = LvAppID;        
        this.AppStatus = AppStatus;
        this.EmpID = EmpID;
        this.LTypeId = LTypeID;
        this.LeaveEnjoyed = LeaveEnjoyed;              
        this.InsertedBy = InsertedBy;
        this.InsertedDate = InsertedDate;        
    }
}
