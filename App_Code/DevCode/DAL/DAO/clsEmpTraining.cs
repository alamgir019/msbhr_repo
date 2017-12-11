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
/// Summary description for clsEmpTraining
/// </summary>
public class clsEmpTraining
{
    private string _EmpID;

    public string EmpID
    {
        get { return _EmpID; }
        set { _EmpID = value; }
    }

    private string _TrainId;
    public string TrainId
    {
        get { return _TrainId; }
        set { _TrainId = value; }
    }

    private string _TrainingName;
    public string TrainingName
    {
        get { return _TrainingName; }
        set { _TrainingName = value; }
    }
    private string _Vanue;
    public string Vanue
    {
        get { return _Vanue; }
        set { _Vanue = value; }
    }
    private string _countryID;

    public string countryID
    {
        get { return _countryID; }
        set { _countryID = value; }
    }
    private string _Duration;
    public string Duration
    {
        get { return _Duration; }
        set { _Duration = value; }
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

    private string _SponsorId;
    public string SponsorId
    {
        get { return _SponsorId; }
        set { _SponsorId = value; }
    }
    private string _Organized;
    public string Organized
    {
        get { return _Organized; }
        set { _Organized = value; }
    }
    private string _EventId;
    public string EventId
    {
        get { return _EventId; }
        set { _EventId = value; }
    }
    private string _DisiciplineId;
    public string DisiciplineId
    {
        get { return _DisiciplineId; }
        set { _DisiciplineId = value; }
    }


    public clsEmpTraining(string EmpID,
        string TrainId,
        string TrainingName,
        string Vanue,
        string countryID,
        string Duration,
        string StartFrom,
        string EndFrom,
        string InsertedBy,
        string InsertedDate,
        string SponsorId,
        string Organized,
        string EventId,
        string DisiciplineId
        )
	{
        this.EmpID = EmpID;
        this.TrainId = TrainId;
        this.TrainingName = TrainingName;
        this.Vanue = Vanue;
        this.countryID = countryID;
        this.Duration = Duration;
        this.StartFrom = StartFrom;
        this.EndFrom = EndFrom;
        this.InsertedBy = InsertedBy;
        this.InsertedDate = InsertedDate;
        this.SponsorId = SponsorId;
        this.Organized = Organized;
        this.EventId = EventId;
        this.DisiciplineId = DisiciplineId;
	}
}
