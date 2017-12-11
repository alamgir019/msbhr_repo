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
/// Summary description for Weekend
/// </summary>
public class Weekend
{
    private string _WeekEndID;
    private string _WEPackName;
    private string _WESun;
    private string _WEMon;
    private string _WETues;
    private string _WEWed;
    private string _WETue;
    private string _WEFri;
    private string _WESat;
    private string _TotalWeekEnd;
    private string _IsActive;
    private string _InsertedBy;
    private string _InsertedDate;

    public string WeekEndID
    {
        get { return _WeekEndID; }
        set { _WeekEndID = value; }
    }
    public string WEPackName
    {
        get { return _WEPackName; }
        set { _WEPackName = value; }
    }
    public string WESun
    {
        get { return _WESun; }
        set { _WESun = value; }
    }
    public string WEMon
    {
        get { return _WEMon; }
        set { _WEMon = value; }
    }
    public string WETues
    {
        get { return _WETues; }
        set { _WETues = value; }
    }
    public string WEWed
    {
        get { return _WEWed; }
        set { _WEWed = value; }
    }
    public string WETue
    {
        get { return _WETue; }
        set { _WETue = value; }
    }
    public string WEFri
    {
        get { return _WEFri; }
        set { _WEFri = value; }
    }
    public string WESat
    {
        get { return _WESat; }
        set { _WESat = value; }
    }
    public string TotalWeekEnd
    {
        get { return _TotalWeekEnd; }
        set { _TotalWeekEnd = value; }
    }
   
     public string IsActive
    {
        get { return _IsActive; }
        set { _IsActive = value; }
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

   
	public Weekend(string strWeekEndID,string  strWEPackName,string strWESun,string strWEMon,string strWETues,string strWEWed,string strWETue,string strWEFri,string strWESat,string strTotalWeekEnd,string strIsActive,string strInsertedBy,string strInsertedDate)
	{
        WeekEndID=strWeekEndID;
        WEPackName = strWEPackName;
        WESun=strWESun;
        WEMon=strWEMon;
        WETues=strWETues;
        WEWed=strWEWed;
        WETue=strWETue;
        WEFri=strWEFri;
        WESat=strWESat;
        TotalWeekEnd=strTotalWeekEnd;
        IsActive=strIsActive;
        InsertedBy=strInsertedBy;
        InsertedDate = strInsertedDate;
	}
}
