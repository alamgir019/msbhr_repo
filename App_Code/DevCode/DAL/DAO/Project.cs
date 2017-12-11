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
/// Summary description for Project
/// </summary>
public class Project
{
    private string _ProjectId;
    public string ProjectId
    {
        get { return _ProjectId; }
        set { _ProjectId = value; }
    }

    private string _ProjectName;
    public string ProjectName
    {
        get { return _ProjectName; }
        set { _ProjectName = value; }
    }

    private string _PDesc;
    public string PDesc
    {
        get { return _PDesc; }
        set { _PDesc = value; }
    }

    private string _StartDate;
    public string StartDate
    {
        get { return _StartDate; }
        set { _StartDate = value; }
    }

    private string _EndDate;
    public string EndDate
    {
        get { return _EndDate; }
        set { _EndDate = value; }
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
    private string _LastUpdatedFrom;
    public string LastUpdatedFrom
    {
        get { return _LastUpdatedFrom; }
        set { _LastUpdatedFrom = value; }
    }
    private string _isUpdate;
    public string IsUpdate
    {
        get { return _isUpdate; }
        set { _isUpdate = value; }
    }

    private string _IsDelete;
    public string IsDelete
    {
        get { return _IsDelete; }
        set { _IsDelete = value; }
    }
    public Project(string strProjectId, string strProjectName, string strPDesc,  string strStartDate, string strEndDate,
        string strISACTIVE, string strIsDeleted,string strInsertedBy, string strInsertedDate, string strIsUpdate, string strIsDelete)
    {
        this.ProjectId = strProjectId;
        this.ProjectName = strProjectName;
        this.PDesc = strPDesc;        
        this.StartDate = strStartDate;
        this.EndDate = strEndDate;
        this.IsActive = strISACTIVE;
        this.IsDeleted = strIsDeleted;
        this.InsertedBy = strInsertedBy;
        this.InsertedDate = strInsertedDate;
        this.IsUpdate = strIsUpdate;
        this.IsDelete = strIsDelete;
    }
	public Project()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
