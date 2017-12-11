using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for Company
/// </summary>
public class Company
{
	 private string _DivisionId;

    public string DivisionId
    {
        get { return _DivisionId; }
        set { _DivisionId = value; }
    }
    private string _DivisionName;

    public string DivisionName
    {
        get { return _DivisionName; }
        set { _DivisionName = value; }
    }

    private string _DivisionShortName;

    public string DivisionShortName
    {
        get { return _DivisionShortName; }
        set { _DivisionShortName = value; }
    }
    
    private string _IsActive;
    public string IsActive
    {
        get { return _IsActive; }
        set { _IsActive = value; }
    }

    private string _IsDeleted;
    public string IsDeleted
    {
        get { return _IsDeleted; }
        set { _IsDeleted = value; }
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

     private string _LastUpdatedFrom;

    public string LastUpdatedFrom
    {
        get { return _LastUpdatedFrom; }
        set { _LastUpdatedFrom = value; }
    }

     private string _UpdatedBy;

    public string UpdatedBy
    {
        get { return _UpdatedBy; }
        set { _UpdatedBy = value; }
    }
    private string _UpdatedDate;

    public string UpdatedDate
    {
        get { return _UpdatedDate; }
        set { _UpdatedDate = value; }
    }

    public Company(string strDivisionId, string strDivisionName, string strDivisionShortName, string strIsActive, string strIsDeleted,
        string strInsertedBy, string strInsertedDate,string strUpdatedBy, string strUpdatedDate,string strLastUpdatedFrom)
	{
        this.DivisionId = strDivisionId;
        this.DivisionName = strDivisionName;
        this.DivisionShortName = strDivisionShortName;
        this.IsActive = strIsActive;
        this.IsDeleted = strIsDeleted;
        this.InsertedBy = strInsertedBy;
        this.InsertedDate = strInsertedDate;
        this.UpdatedBy = strUpdatedBy;
        this.UpdatedDate = strUpdatedDate;
        this.LastUpdatedFrom = strLastUpdatedFrom;

	}
}
