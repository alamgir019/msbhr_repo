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
/// Summary description for clsEmpTraninigNeed
/// </summary>
public class clsEmpTraninigNeed
{


    private string _EmpID;

    public string EmpID
    {
        get { return _EmpID; }
        set { _EmpID = value; }
    }

    private string _Emptraid;

    public string Emptraid
    {
        get { return _Emptraid; }
        set { _Emptraid = value; }
    }


    private string _fisicalYear;

    public string fisicalYear
    {
        get { return _fisicalYear; }
        set { _fisicalYear = value; }
    }

    private string _TrainingNeed;

    public string TrainingNeed
    {
        get { return _TrainingNeed; }
        set { _TrainingNeed = value; }
    }

    private string _Disciplineid;

    public string Disciplineid
    {
        get { return _Disciplineid; }
        set { _Disciplineid = value; }
    }
    private string _Quarter;

    public string Quarter
    {
        get { return _Quarter; }
        set { _Quarter = value; }
    }

    private string _Week;

    public string Week
    {
        get { return _Week; }
        set { _Week = value; }
    }

    private string _Priority;

    public string Priority
    {
        get { return _Priority; }
        set { _Priority = value; }
    }
    private string _Done;

    public string Done
    {
        get { return _Done; }
        set { _Done = value; }
    }

    private string _StartFrom;
    public string StartFrom
    {
        get { return _StartFrom; }
        set { _StartFrom = value; }
    }
    private string _EndFrom;
    public string EndFrom
    {
        get { return _EndFrom; }
        set { _EndFrom = value; }
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
   

    public clsEmpTraninigNeed(
        string Emptraid,
        string fisicalYear,
        string TrainingNeed,
        string Disciplineid,
        string Quarter,
        string Priority,
        string Week,
        string Done,
        string StartFrom,
        string EndFrom,
       
        string InsertedBy, 
        string InsertedDate
        
        )
	{
        this.Emptraid = Emptraid;
        this.fisicalYear = fisicalYear;
        this.TrainingNeed = TrainingNeed;
        this.Disciplineid=Disciplineid;
        this.Quarter=Quarter;
        this.Priority=Priority;
        this.Week = Week;
        this.Done=Done;
        this.StartFrom = StartFrom;
        this.EndFrom = EndFrom;
        this.InsertedBy=InsertedBy; 
        this.InsertedDate=InsertedDate;
       
	}



    
}
