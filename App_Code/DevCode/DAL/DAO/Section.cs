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
/// Summary description for Section
/// </summary>
public class Section
{
    private string _SectionID;

    public string SectionID
    {
      get { return _SectionID; }
      set { _SectionID = value; }
    }


    private string _SecName;

    public string SecName
    {
      get { return _SecName; }
      set { _SecName = value; }
    }

    private string _SecDesc;

    public string SecDesc
    {
        get { return _SecDesc; }
        set { _SecDesc = value; }
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
    private string _StrDeptID;

    public string StrDeptID
    {
        get { return _StrDeptID; }
        set { _StrDeptID = value; }
    }

    public Section(string SectionID, string SecName, string SecDesc, string isDeleted, string InsertedBy, string InsertedDate, string UpdatedBy, string UpdatedDate, string LastUpdatedFrom, string IsUpdate, string IsDelete, string StrDeptID, string IsActive)
    {
        this.SectionID = SectionID;
        this.SecName = SecName;
        this.SecDesc = SecDesc;
        this.IsDeleted = isDeleted;
        this.InsertedBy = InsertedBy;
        this.InsertedDate = InsertedDate;
        this.UpdatedBy = UpdatedBy;
        this.UpdatedDate = UpdatedDate;
        this.LastUpdatedFrom = LastUpdatedFrom;
        this.IsUpdate = IsUpdate;
        this.IsDelete = IsDelete;
        //this.IsActive = IsActive;
        this.StrDeptID = StrDeptID; 
    }
}
