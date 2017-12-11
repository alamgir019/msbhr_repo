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
/// Summary description for UserCreation
    /// </summary>
public class UserCreation
{
    private string _USERID;
    public string USERID
    {
        get { return _USERID; }
        set { _USERID = value; }
    }

    private string _Name;
    public string Name
    {
        get { return _Name; }
        set { _Name = value; }
    }
    private string _PASSWORD;
    public string PASSWORD
    {
        get { return _PASSWORD; }
        set { _PASSWORD = value; }
    }

    private string _AccountDisabled;

    public string AccountDisabled
    {
        get { return _AccountDisabled; }
        set { _AccountDisabled = value; }
    }

    private string _ChangePassword;
    public string ChangePassword
    {
        get { return _ChangePassword; }
        set { _ChangePassword = value; }
    }
    private string _EmpId;

    public string EmpId
    {
        get { return _EmpId; }
        set { _EmpId = value; }
    }

    private string _IsAdmin;
    public string IsAdmin
    {
        get { return _IsAdmin; }
        set { _IsAdmin = value; }
    }

    private string _DivisionId;
    public string DivisionId
    {
        get { return _DivisionId; }
        set { _DivisionId = value; }
    }

    private string _SBUId;
    public string SBUId
    {
        get { return _SBUId; }
        set { _SBUId = value; }
    }

    private char _IsActive;
    public char IsActive
    {
        get { return _IsActive; }
        set { _IsActive = value; }
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
    private string _IsUpdate;

    public string IsUpdate
    {
        get { return _IsUpdate; }
        set { _IsUpdate = value; }
    }

    private string _IsDelete;

    public string IsDelete
    {
        get { return _IsDelete; }
        set { _IsDelete = value; }
    }

    private string _DeptId;
    public string DeptId
    {
        get { return _DeptId; }
        set { _DeptId = value; }
    }

    public UserCreation(string USERID, string PASSWORD, string Name, string AccountDisabled, string ChangePassword,string EmpId,string IsAdmin,string DivisionId,string SBUId, string InsertedBy, string InsertedDate, string IsUpdate, string IsDelete,string strDeptId)
    {
        this.USERID = USERID;
        this.PASSWORD = PASSWORD;
        this.Name = Name;
        this.AccountDisabled = AccountDisabled;
        this.ChangePassword = ChangePassword;
        this.EmpId = EmpId;
        this.IsAdmin = IsAdmin;
        this.DivisionId = DivisionId;
        this.SBUId = SBUId;  
        this.InsertedBy = InsertedBy;
        this.InsertedDate = InsertedDate;        
        //this.LastUpdatedFrom = LastUpdatedFrom;
        this.IsUpdate = IsUpdate;
        this.IsDelete = IsDelete;
        this.DeptId = strDeptId;
    }

    public UserCreation(string USERID, string PASSWORD)
    {
        this.USERID = USERID;
        this.PASSWORD = PASSWORD;    
    }
}
