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
/// Summary description for clsEmpType
/// </summary>
public class clsEmpType
{
    private string _EmpTypeID;

    public string EmpTypeID
    {
        get { return _EmpTypeID; }
        set { _EmpTypeID = value; }
    }
    private string _TypeName;

    public string TypeName
    {
        get { return _TypeName; }
        set { _TypeName = value; }
    }

    private string _TypeDesc;

    public string TypeDesc
    {
        get { return _TypeDesc; }
        set { _TypeDesc = value; }
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

    public clsEmpType(string sEmpTypeID, string sTypeName,string sTypeDesc,  string sIsActive,
        string sIsDeleted, string sInsertedBy, string sInsertedDate, string sIsUpdate, string sIsDelete)
    {
        this.EmpTypeID = sEmpTypeID;
        this.TypeName = sTypeName;
        this.TypeDesc = sTypeDesc;
        this.IsActive = sIsActive;
        this.IsDeleted  = sIsDeleted;
        this.InsertedBy = sInsertedBy;
        this.InsertedDate = sInsertedDate;       
        this.IsUpdate = sIsUpdate;
        this.IsDelete = sIsDelete;
    }
}

