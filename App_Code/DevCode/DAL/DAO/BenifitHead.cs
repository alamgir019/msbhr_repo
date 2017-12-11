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
/// Summary description for BenifitHead
/// </summary>
public class BenifitHead
{
    private string headId ;
    private string headNameEng;//40
    private string headNamebng;//40
    private string isActive;
    private string isDeleted;
    private string insertedBy;
    private string insertedDate;
    private string updatedBy;
    private string updatedDate;
    private string lastUpdatedFrom;
    private string isUpdate;
    

    public BenifitHead(string headId, string headNameEng, string headNamebng, string isActive, string insertedBy, string isUpdate)
    {
        this.HeadId = headId;
        this.HeadNameEng = headNameEng;
        this.HeadNamebng = headNamebng;
        this.IsActive = isActive;
        this.InsertedBy = insertedBy;
        this.IsUpdate = isUpdate;
    }
    //Properties
    /// <summary>
    /// Property for headId
    /// </summary>
    /// isUpdate

    /// 
    public string IsUpdate
    {
        get { return isUpdate; }
        set { isUpdate = value; }
    }
    public string HeadId
    {
        get { return headId; }
        set { headId = value; }
    }
    public string HeadNameEng
    {
        get { return headNameEng; }
        set
        {
            if (value.Length >= 40)
            {
                headNameEng = value.Remove(39);
            }
            else
            {
                headNameEng = value;
            }

        }
    }
    public string HeadNamebng
    {
        get { return headNamebng; }
        set
        {
            if (value.Length >= 40)
            {
                headNamebng = value.Remove(39);
            }
            else
            {
                headNamebng = value;
            }

        }
    }
    public string IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }
    public string IsDeleted
    {
        get { return isDeleted; }
        set { isDeleted = value; }
    }
    public string InsertedBy
    {
        get { return insertedBy; }
        set { insertedBy = value; }
    }
    public string InsertedDate
    {
        get { return insertedDate; }
        set { insertedDate = value; }
    }
    public string UpdatedBy
    {
        get { return updatedBy; }
        set { updatedBy = value; }
    }
    public string UpdatedDate
    {
        get { return updatedDate; }
        set { updatedDate = value; }
    }
    public string LastupdatedFrom
    {
        get { return lastUpdatedFrom; }
        set { lastUpdatedFrom = value; }
    }

}
