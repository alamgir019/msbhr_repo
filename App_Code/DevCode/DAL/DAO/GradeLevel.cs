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
/// Summary description for GradeLevel
/// </summary>
public class GradeLevel
{
    private string _GradeLevelId;
    public string GradeLevelId
    {
        get { return _GradeLevelId; }
        set { _GradeLevelId = value; }
    }

    private string _GradeID;
    public string GradeID
    {
        get { return _GradeID; }
        set { _GradeID = value; }
    }


    private string _GradeLevelName;
    public string GradeLevelName
    {
        get { return _GradeLevelName; }
        set { _GradeLevelName = value; }
    }

    private string _IsActive;
    public string IsActive
    {
        get { return _IsActive; }
        set { _IsActive = value; }
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

    private string _BasicMin;
    public string BasicMin
    {
        get { return _BasicMin; }
        set { _BasicMin = value; }
    }

    private string _BasicMax;
    public string BasicMax
    {
        get { return _BasicMax; }
        set { _BasicMax = value; }
    }

    private string _TransportAmnt;
    public string TransportAmnt
    {
        get { return _TransportAmnt; }
        set { _TransportAmnt = value; }
    }

    private string _IsDeleted;
    public string IsDeleted
    {
        get { return _IsDeleted; }
        set { _IsDeleted = value; }
    }

    public GradeLevel(string GradeLevelId, string GradeID,string GradeLevelName, string IsActive, string BasicMin, string BasicMax,
        string TransportAmnt,string IsDeleted,string InsertedBy, string InsertedDate)
	{
        this.GradeLevelId = GradeLevelId;
        this.GradeID = GradeID;
        this.GradeLevelName = GradeLevelName;
        this.IsActive = IsActive;
        this.BasicMin = BasicMin;
        this.BasicMax = BasicMax;
        this.TransportAmnt = TransportAmnt;
        this.IsDeleted = IsDeleted;
        this.InsertedBy = InsertedBy;
        this.InsertedDate = InsertedDate;        
	}
}
