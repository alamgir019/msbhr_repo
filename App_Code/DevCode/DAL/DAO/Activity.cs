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
/// Summary description for Activity
/// </summary>
public class Activity
{
    private string _Test;

    public string Test
    {
        get { return _Test; }
        set { _Test = value; }
    }

    private string ACTIVITYCODE;
    private string ACTIVITYNAMEENG;
    private string ACTIVITYNAMEBNG;
    private string ACTIVITYTYPE;
 
    private string INSERTEDBY;
    private string INSERTEDDATE;
    private string UPDATEDDATE;
    private string UPDATEDBY;
    private string LASTUPDATEDFROM;

	public Activity(string ActivityCode,string ActivityNameEng,string ActivityNameBng,string ActivityType)
	{
        this.ACTIVITYCODE = ActivityCode;
        this.ACTIVITYNAMEENG = ActivityNameEng;
        this.ActivityNameBng = ActivityNameBng;
        this.ACTIVITYTYPE = ActivityType;
    }

    public Activity(string ActivityCode, string ActivityNameEng, string ActivityNameBng, string ActivityType, string InsertedBy, string InsertedDate)
    {
        this.ACTIVITYCODE = ActivityCode;
        this.ACTIVITYNAMEENG = ActivityNameEng;
        this.ACTIVITYNAMEBNG = ActivityNameBng;
        this.ACTIVITYTYPE = ActivityType;
        this.INSERTEDBY = InsertedBy;
        this.INSERTEDDATE = InsertedDate;
    }

    // Properties

    public string ActivityCode
    {
        get { return ACTIVITYCODE; }
        set { ACTIVITYCODE = value; }
    }
    public string ActivityNameEng
    {
        get { return ACTIVITYNAMEENG; }
        set { ACTIVITYNAMEENG = value; }
    }
    public string ActivityNameBng
    {
        get { return ACTIVITYNAMEBNG; }
        set { ACTIVITYNAMEBNG = value; }
    }
    public string ActivityType
    {
        get { return ACTIVITYTYPE; }
        set { ACTIVITYTYPE = value; }
    }
    public string InsertedBy
    {
        get { return INSERTEDBY; }
        set { INSERTEDBY = value; }
    }
    public string InsertedDate
    {
        get { return INSERTEDDATE; }
        set { INSERTEDDATE = value; }
    }

}
