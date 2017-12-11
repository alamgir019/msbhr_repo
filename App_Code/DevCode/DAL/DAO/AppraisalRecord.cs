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
/// Summary description for AppraisalRecord
/// </summary>
public class AppraisalRecord
{

    private string _ApprId;

    public string ApprId
    {
        get { return _ApprId; }
        set { _ApprId = value; }
    }

    private string _Empid;

    public string Empid
    {
        get { return _Empid; }
        set { _Empid = value; }
    }



    private string _ApprYear;

    public string ApprYear
    {
        get { return _ApprYear; }
        set { _ApprYear = value; }
    }

    private string _Score;

    public string Score
    {
        get { return _Score; }
        set { _Score = value; }
    }

    private string _Appraiser;

    public string Appraiser
    {
        get { return _Appraiser; }
        set { _Appraiser = value; }
    }
    private string _AwardId;

    public string AwardId
    {
        get { return _AwardId; }
        set { _AwardId = value; }
    }

    private string _SpotBonus;

    public string SpotBonus
    {
        get { return _SpotBonus; }
        set { _SpotBonus = value; }
    }

    private string _Status;

    public string Status
    {
        get { return _Status; }
        set { _Status = value; }
    }
    private string _context;

    public string context
    {
        get { return _context; }
        set { _context = value; }
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



    public AppraisalRecord(
        string ApprId,
        string Empid,
        string ApprYear,
        string Score,
        string Appraiser,
        string AwardId,
        string SpotBonus,
        string Status,
        string context,
        string InsertedBy, 
        string InsertedDate
        
        )
	{
        this.ApprId =ApprId;
        this.Empid = Empid;
        this.ApprYear = ApprYear;
        this.Score = Score;
        this.Appraiser = Appraiser;
        this.AwardId = AwardId;
        this.SpotBonus = SpotBonus;
        this.Status = Status;
        this.context = context;
        this.InsertedBy=InsertedBy; 
        this.InsertedDate=InsertedDate;
       
	}



    
}
