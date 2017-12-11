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
/// Summary description for Payroll_Currency
/// </summary>
public class Payroll_Currency
{
    private string _CURNCID;
    public string CURNCID
    {
        get { return _CURNCID; }
        set { _CURNCID = value; }
    }

    private string _CURNCNAME;
    public string CURNCNAME
    {
        get { return _CURNCNAME; }
        set { _CURNCNAME = value; }
    }

    private string _CURNCSYMBOL;
    public string CURNCSYMBOL
    {
        get { return _CURNCSYMBOL; }
        set { _CURNCSYMBOL = value; }
    }

    private string _LOWESTUNITNAME;
    public string LOWESTUNITNAME
    {
        get { return _LOWESTUNITNAME; }
        set { _LOWESTUNITNAME = value; }
    }

    private string _ISDEFAULT;

    public string ISDEFAULT
    {
        get { return _ISDEFAULT; }
        set { _ISDEFAULT = value; }
    }
    private string _CONVRSAMT;

    public string CONVRSAMT
    {
        get { return _CONVRSAMT; }
        set { _CONVRSAMT = value; }
    }
    private string _ISACTIVE;

    public string ISACTIVE
    {
        get { return _ISACTIVE; }
        set { _ISACTIVE = value; }
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
    public Payroll_Currency(string strCURNCID, string strCURNCNAME, string strCURNCSYMBOL, string strLOWESTUNITNAME, string strISDEFAULT,
        string strCONVRSAMT,string strISACTIVE, string strInsertedBy, string strInsertedDate, string strIsUpdate, string strIsDelete)
	{
        this.CURNCID = strCURNCID;
        this.CURNCNAME = strCURNCNAME;
        this.CURNCSYMBOL = strCURNCSYMBOL;
        this.LOWESTUNITNAME = strLOWESTUNITNAME;
        this.ISDEFAULT = strISDEFAULT;
        this.CONVRSAMT = strCONVRSAMT;
        this.ISACTIVE = strISACTIVE;
        this.InsertedBy = strInsertedBy;
        this.InsertedDate = strInsertedDate;
        this.IsUpdate = strIsUpdate;
        this.IsDelete = strIsDelete;
	}
}
