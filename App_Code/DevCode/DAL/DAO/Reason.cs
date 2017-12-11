using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for Reason
/// </summary>
public class Reason
{
    private string _ReasonId;

    public string ReasonId
    {
        get { return _ReasonId;}
        set { _ReasonId = value; }
    }

    private string _ReasonName;

    public string ReasonName{

        get { return _ReasonName; }
        set { _ReasonName = value; }
    
    }

    private string _IsActive;

    public string IsActive {

        get { return _IsActive; }
        set { _IsActive = value; }
    
    }

    private string _IsDeleted;

    public string IsDeleted {
        get { return _IsDeleted; }
        set { _IsDeleted = value; }
    }

    private string _InsertedBy;
    public string InsertedBy {

        get { return _InsertedBy; }
        set { _InsertedBy = value; }
    }

    private string _InsertedDate;

    public string InsertedDate {

        get { return _InsertedDate; }
        set { _InsertedDate = value; }
    }

    private string _UpdatedBy;

    public string UpdatedBy {

        get { return _UpdatedBy; }
        set { _UpdatedBy = value; }
    }

    private string _UpdatedOn;

    public string UpdatedOn{

        get { return _UpdatedOn; }
        set { _UpdatedOn = value; }
    }

    public Reason(string ReasonId, string ReasonName, string IsActive, string IsDeleted, string InsertedBy, string InsertedDate, string UpdatedBy, string UpdatedOn)
	{
        this.ReasonId = ReasonId;
        this.ReasonName = ReasonName;
        this.IsActive = IsActive;
        this.IsDeleted = IsDeleted;
        this.InsertedBy = InsertedBy;
        this.InsertedDate = InsertedDate;
        this.UpdatedBy = UpdatedBy;
        this.UpdatedOn = UpdatedOn;
		
	}
}