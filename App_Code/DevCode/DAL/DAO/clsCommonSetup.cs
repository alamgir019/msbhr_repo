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
/// Summary description for Designation
/// </summary>
public class clsCommonSetup
{
    private string _ID;

    public string ID
    {
        get { return _ID; }
        set { _ID = value; }
    }
    private string _Code;
    public string Code
    {
        get { return _Code; }
        set { _Code = value; }
    }
    private string _Name;

    public string Name
    {
        get { return _Name; }
        set { _Name = value; }
    }
    private string _isDeleted;

    public string IsDeleted
    {
        get { return _isDeleted; }
        set { _isDeleted = value; }
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
    private string _IsActive;
    public string IsActive
    {
        get { return _IsActive; }
        set { _IsActive = value; }
    }

    public clsCommonSetup(string DesgID, string DesgName,  string IsActive,string isDeleted, string InsertedBy, string InsertedDate, 
        string IsUpdate, string IsDelete)
	{
        this.ID = DesgID;
        this.Name = DesgName;
        this.IsActive = IsActive;
        this.IsDeleted = isDeleted;
        this.InsertedBy = InsertedBy;
        this.InsertedDate = InsertedDate;
        this.IsUpdate = IsUpdate;
        this.IsDelete = IsDelete;
	}
    public clsCommonSetup(string DesgID, string Code, string DesgName, string IsActive, string isDeleted, string InsertedBy, string InsertedDate,
       string IsUpdate, string IsDelete)
    {
        this.ID = DesgID;
        this.Code = Code;
        this.Name = DesgName;
        this.IsActive = IsActive;
        this.IsDeleted = isDeleted;
        this.InsertedBy = InsertedBy;
        this.InsertedDate = InsertedDate;
        this.IsUpdate = IsUpdate;
        this.IsDelete = IsDelete;
    }
    public clsCommonSetup()
    {
    }
}
