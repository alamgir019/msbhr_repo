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
/// Summary description for Award
/// </summary>
public class Award
{

	private string _AwardId;

    public string AwardId
    {
        get { return _AwardId; }
        set { _AwardId = value; }
    }


    private string _Awards;

    public string Awards
    {
        get { return _Awards; }
        set { _Awards = value; }
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

    public Award(string AwardId, string Awards, string isDeleted, string InsertedBy, string InsertedDate, string UpdatedBy, string UpdatedDate, string LastUpdatedFrom, string IsUpdate, string IsDelete, string IsActive)
    {
        this.AwardId = AwardId;
        this.Awards = Awards;
        this.IsDeleted = isDeleted;
        this.InsertedBy = InsertedBy;
        this.InsertedDate = InsertedDate;
        this.UpdatedBy = UpdatedBy;
        this.UpdatedDate = UpdatedDate;
        this.LastUpdatedFrom = LastUpdatedFrom;
        this.IsUpdate = IsUpdate;
        this.IsDelete = IsDelete;
        this.IsActive = IsActive;
    }
	
}