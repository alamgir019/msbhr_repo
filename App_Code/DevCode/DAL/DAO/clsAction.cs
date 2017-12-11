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
/// Summary description for clsAction
/// </summary>
public class clsAction
{
    private string _ActionId;

    public string ActionId
    {
        get { return _ActionId; }
        set { _ActionId = value; }
    }
    private string _ActionName;

    public string ActionName
    {
        get { return _ActionName; }
        set { _ActionName = value; }
    }

    private string _ActionDesc;

    public string ActionDesc
    {
        get { return _ActionDesc; }
        set { _ActionDesc = value; }
    }
    private string _ActionType;
    public string ActionType
    {
        get { return _ActionType; }
        set { _ActionType = value; }
    }
    private string _ActionNature;
    public string ActionNature
    {
        get { return _ActionNature; }
        set { _ActionNature = value; }
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

    public clsAction(string strActionId, string strActionName, string strActionDesc,string strActionType,
        string strActionNature,string strIsActive,string strIsDeleted,string strInsertedBy, string strInsertedDate)
	{
        this.ActionId = strActionId;
        this.ActionName = strActionName;
        this.ActionDesc = strActionDesc;
        this.ActionType = strActionType;
        this.ActionNature = strActionNature;
        this.IsActive = strIsActive;
        this.IsDeleted = strIsDeleted;
        this.InsertedBy = strInsertedBy;
        this.InsertedDate = strInsertedDate;  
	}
}
