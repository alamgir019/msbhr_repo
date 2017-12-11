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
/// Summary description for ClsEmpSeparation
/// Created by Murad
/// 11.10.2011
/// </summary>
public class ClsEmpSeparation
{
    private string _EmpID;
    public string EmpID
    {
        get { return _EmpID; }
        set { _EmpID = value; }
    }
    private string _SeparationID;
    public string SeparationID
    {
        get { return _SeparationID; }
        set { _SeparationID = value; }
    }
    private string _SeparationMode;
    public string SeparationMode
    {
        get { return _SeparationMode; }
        set { _SeparationMode = value; }
    }
    private string _SeparationDate;
    public string SeparationDate
    {
        get { return _SeparationDate; }
        set { _SeparationDate = value; }
    }
    private string _PrevWorkDuration;
    public string PrevWorkDuration
    {
        get { return _PrevWorkDuration; }
        set { _PrevWorkDuration = value; }
    }
    private string _ReHiredStatus;
    public string ReHiredStatus
    {
        get { return _ReHiredStatus; }
        set { _ReHiredStatus = value; }
    }
    private string _ReHiredStatusCause;
    public string ReHiredStatusCause
    {
        get { return _ReHiredStatusCause; }
        set { _ReHiredStatusCause = value; }
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




	public ClsEmpSeparation(string emId, string sepId, string sepMode, string sepDate, string strPrevWorkDuration,
        string reHiredSts, string reHiredCause, string insertedBy, string insertedDate)
	{
        this.EmpID = emId;
        this.SeparationID = sepId;
        this.SeparationMode = sepMode;
        this.SeparationDate = sepDate;
        this.PrevWorkDuration = strPrevWorkDuration;
        this.ReHiredStatus = reHiredSts;
        this.ReHiredStatusCause = reHiredCause;
        this.InsertedBy = insertedBy;
        this.InsertedDate = insertedDate;
	}
}
