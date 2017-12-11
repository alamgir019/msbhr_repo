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
/// Summary description for Payroll_LoanType
/// </summary>
public class Payroll_LoanType
{
    private string _LOANTYPEID;

    public string LOANTYPEID
    {
        get { return _LOANTYPEID; }
        set { _LOANTYPEID = value; }
    }

    private string _LOANTYPENAME;
    public string LOANTYPENAME
    {
        get { return _LOANTYPENAME; }
        set { _LOANTYPENAME = value; }
    }

    private string _LOANDESCRIPTION;
    public string LOANDESCRIPTION
    {
        get { return _LOANDESCRIPTION; }
        set { _LOANDESCRIPTION = value; }
    }

    private string _SHEADID;
    public string SHEADID
    {
        get { return _SHEADID; }
        set { _SHEADID = value; }
    }

    private string _ISACTIVE;

    public string ISACTIVE
    {
        get { return _ISACTIVE; }
        set { _ISACTIVE = value; }
    }
    private string _ISPFLOAN;

    public string ISPFLOAN
    {
        get { return _ISPFLOAN; }
        set { _ISPFLOAN = value; }
    }
    private string _MINSERVICELIFE;

    public string MINSERVICELIFE
    {
        get { return _MINSERVICELIFE; }
        set { _MINSERVICELIFE = value; }
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
	public Payroll_LoanType(string strLOANTYPEID,string strLOANTYPENAME,string strLOANDESCRIPTION,string strSHEADID,
        string strISACTIVE, string strISPFLOAN, string strMINSERVICELIFE, string strInsertedBy, string strInsertedDate, 
        string strIsUpdate, string strIsDelete)
	{
        this.LOANTYPEID = strLOANTYPEID;
        this.LOANTYPENAME = strLOANTYPENAME;
        this.LOANDESCRIPTION = strLOANDESCRIPTION;
        this.SHEADID = strSHEADID;        
        this.ISACTIVE = strISACTIVE;
        this.ISPFLOAN = strISPFLOAN;
        this.MINSERVICELIFE = strMINSERVICELIFE;
        this.InsertedBy = strInsertedBy;
        this.InsertedDate = strInsertedDate;
        this.IsUpdate = strIsUpdate;
        this.IsDelete = strIsDelete;
	}
}
