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
/// Summary description for Holiday
/// </summary>
public class Holiday
{
    private string _HolidayYear;
    private string _HoliDayId;
    private string _HolidayName;
    private string _StartDate;
    private string _EndDate;
    private string _Duration;
    private string _HoliDesc;

    private string _HoliDays;
    private string _IsActive;
    private string _DivisionId;
    private string _SBUId;

    private string _InsertedBy;
    private string _InsertedDate;

    public string HolidayYear
    {
        get { return _HolidayYear; }
        set { _HolidayYear = value; }
    }

    public string HoliDayId
    {
        get { return _HoliDayId; }
        set { _HoliDayId = value; }
    }

    public string HolidayName
    {
        get { return _HolidayName; }
        set { _HolidayName = value; }
    }

    public string StartDate
    {
        get { return _StartDate; }
        set { _StartDate = value; }
    }
    public string EndDate
    {
        get { return _EndDate; }
        set { _EndDate = value; }
    }

    public string Duration
    {
        get { return _Duration; }
        set { _Duration = value; }
    }

    public string HoliDesc
    {
        get { return _HoliDesc; }
        set { _HoliDesc = value; }
    }

    public string HoliDays
    {
        get { return _HoliDays; }
        set { _HoliDays = value; }
    }

    public string IsActive
    {
        get { return _IsActive; }
        set { _IsActive = value; }
    }

    public string SBUId
    {
        get { return _SBUId; }
        set { _SBUId = value; }
    }

    public string DivisionId
    {
        get { return _DivisionId; }
        set { _DivisionId = value; }
    }

    public string InsertedBy
    {
        get { return _InsertedBy; }
        set { _InsertedBy = value; }
    }

    public string InsertedDate
    {
        get { return _InsertedDate; }
        set { _InsertedDate = value; }
    }
    public Holiday(string strHolidayYear, string strHoliDayId, string strHolidayName, string strStartDate, string strEndDate,
        string strDuration, string strHoliDesc, string strHoliDays, string strIsActive,
         string strDivisionId, string strSBUId, string strInsertedBy, string strInsertedDate)
    {
        HolidayYear = strHolidayYear;
        HoliDayId = strHoliDayId;
        HolidayName = strHolidayName;
        StartDate = strStartDate;
        EndDate = strEndDate;
        Duration = strDuration;
        HoliDesc = strHoliDesc;
        HoliDays = strHoliDays;
        IsActive = strIsActive;
        DivisionId = strDivisionId;  
        SBUId = strSBUId;
        InsertedBy = strInsertedBy;
        InsertedDate = strInsertedDate;
    }
}
