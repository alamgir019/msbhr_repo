using System;
using System.Data;
using System.Data.SqlClient; 
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
/// <summary>
/// Summary description for UserPrevilege
/// </summary>
public class UserPrevilege
{
    private string _USERID;

    public string USERID
    {
        get { return _USERID; }
        set { _USERID = value; }
    }

    private string _ScreenId;

    public string ScreenId
    {
        get { return _ScreenId; }
        set { _ScreenId = value; }
    }

    private string _A;

    public string A
    {
        get { return _A; }
        set { _A = value; }
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
}
