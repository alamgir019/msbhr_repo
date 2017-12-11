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
/// Summary description for Payroll_BonusPak
/// </summary>
public class Payroll_BonusPak
{
    private string _BPID;

    public string BPID
    {
        get { return _BPID; }
        set { _BPID = value; }
    }

    private string _BPTITLE;
    public string BPTITLE
    {
        get { return _BPTITLE; }
        set { _BPTITLE = value; }
    }

    private string _BPDESC;
    public string BPDESC
    {
        get { return _BPDESC; }
        set { _BPDESC = value; }
    }

    private string _BAMT;
    public string BAMT
    {
        get { return _BAMT; }
        set { _BAMT = value; }
    }

    private string _ISINPERCENT;

    public string ISINPERCENT
    {
        get { return _ISINPERCENT; }
        set { _ISINPERCENT = value; }
    }
    private string _SHEADID;

    public string SHEADID
    {
        get { return _SHEADID; }
        set { _SHEADID = value; }
    }
    private string _NUMOFPAY;

    public string NUMOFPAY
    {
        get { return _NUMOFPAY; }
        set { _NUMOFPAY = value; }
    }
    private string _CurrId;

    public string CurrId
    {
        get { return _CurrId; }
        set { _CurrId = value; }
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
    public Payroll_BonusPak(string strBPID, string strBPTITLE,string strBPDESC, string strBAMT, string strISINPERCENT, string strSHEADID,
        string strNUMOFPAY,string strCurrId, string strISACTIVE, string strInsertedBy, string strInsertedDate, string strIsUpdate, string strIsDelete)
	{
        this.BPID = strBPID;
        this.BPTITLE = strBPTITLE;
        this.BPDESC = strBPDESC;
        this.BAMT = strBAMT;
        this.ISINPERCENT = strISINPERCENT;
        this.SHEADID = strSHEADID;
        this.NUMOFPAY = strNUMOFPAY;
        this.CurrId = strCurrId;
        this.ISACTIVE = strISACTIVE;
        this.InsertedBy = strInsertedBy;
        this.InsertedDate = strInsertedDate;
        this.IsUpdate = strIsUpdate;
        this.IsDelete = strIsDelete;
	}
}
