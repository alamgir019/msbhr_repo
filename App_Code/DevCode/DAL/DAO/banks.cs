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
/// Summary description for banks
/// </summary>
public class banks
{
	private string BANKCODE;
    private string BANKNAMEENG;
    private string BANKNAMEBNG;
    private string ISACTIVE;
    private string ISDELETED;
 
    private string INSERTEDBY;
    private string INSERTEDDATE;
    private string UPDATEDDATE;
    private string UPDATEDBY;
    private string LASTUPDATEDFROM;


    public banks(string BankCode, string BankNameEng, string BanknameBng, string IsActive, string InsertedBy, string InsertedDate)
    {
        this.BANKCODE = BankCode;
        this.BANKNAMEENG = BankNameEng;
        this.BANKNAMEBNG = BanknameBng;       
        this.ISACTIVE = IsActive;
        this.ISDELETED = IsDeleted;
        this.INSERTEDBY = InsertedBy;
        this.INSERTEDDATE = InsertedDate;
    }

    // Properties

    public string BankCode
    {
        get { return BANKCODE; }
        set { BANKCODE = value; }
    }
    public string BankNameEng
    {
        get { return BANKNAMEENG; }
        set { BANKNAMEENG = value; }
    }
    public string BankNameBng
    {
        get { return BANKNAMEBNG; }
        set { BANKNAMEBNG = value; }
    }
    public string isActive
    {
        get { return ISACTIVE; }
        set { ISACTIVE = value; }
    }
    public string IsDeleted
    {
        get { return ISDELETED; }
        set { ISDELETED = value; }
    }
    public string InsertedBy
    {
        get { return INSERTEDBY; }
        set { INSERTEDBY = value; }
    }
    public string InsertedDate
    {
        get { return INSERTEDDATE; }
        set { INSERTEDDATE = value; }
    }

}
