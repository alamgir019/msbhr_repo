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
/// Summary description for Payroll_SalaryHead
/// </summary>
public class Payroll_SalaryHead
{
    private string _SHEADID;

    public string SHEADID
    {
        get { return _SHEADID; }
        set { _SHEADID = value; }
    }

    private string _HEADNAME;
    public string HEADNAME
    {
        get { return _HEADNAME; }
        set { _HEADNAME = value; }
    }

    private string _HEADNATURE;
    public string HEADNATURE
    {
        get { return _HEADNATURE; }
        set { _HEADNATURE = value; }
    }

    private string _SHDESC;
    public string SHDESC
    {
        get { return _SHDESC; }
        set { _SHDESC = value; }
    }

    private string _ISOTHERPAYMENT;

    public string ISOTHERPAYMENT
    {
        get { return _ISOTHERPAYMENT; }
        set { _ISOTHERPAYMENT = value; }
    }
    private string _DEFALTAMNT;

    public string DEFALTAMNT
    {
        get { return _DEFALTAMNT; }
        set { _DEFALTAMNT = value; }
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

    private string _IsBasic;

    public string IsBasic
    {
        get { return _IsBasic; }
        set { _IsBasic = value; }
    }

    private string _IsPF;

    public string IsPF
    {
        get { return _IsPF; }
        set { _IsPF = value; }
    }

    private string _IsHouseRent;

    public string IsHouseRent
    {
        get { return _IsHouseRent; }
        set { _IsHouseRent = value; }
    }

    private string _IsMedical;

    public string IsMedical
    {
        get { return _IsMedical; }
        set { _IsMedical = value; }
    }

    private string _ShortName;

    public string ShortName
    {
        get { return _ShortName; }
        set { _ShortName = value; }
    }

    private string _NaturalCode;

    public string NaturalCode
    {
        get { return _NaturalCode; }
        set { _NaturalCode = value; }
    }

    private string _ItemCategory;
    public string ItemCategory
    {
        get { return _ItemCategory; }
        set { _ItemCategory = value; }
    }

    public Payroll_SalaryHead(string strSHEADID, string strHEADNAME, string strHEADNATURE, string strSHDESC,
        string strISOTHERPAYMENT,string strDEFALTAMNT,string strISACTIVE,string InsertedBy, string InsertedDate, string IsUpdate, string IsDelete,
        string strIsBasic, string strIsPf, string strIsHouseRent, string strIsMedical, string strShortName, string strNaturalCode, string strItemCat)
    {
        this.SHEADID = strSHEADID;
        this.HEADNAME = strHEADNAME;
        this.HEADNATURE = strHEADNATURE;
        this.SHDESC = strSHDESC;
        this.ISOTHERPAYMENT = strISOTHERPAYMENT;
        this.DEFALTAMNT = strDEFALTAMNT;
        this.ISACTIVE = strISACTIVE;

        this.InsertedBy = InsertedBy;
        this.InsertedDate = InsertedDate;        
        this.IsUpdate = IsUpdate;
        this.IsDelete = IsDelete;
        this.IsBasic = strIsBasic;
        this.IsPF = strIsPf;
        this.IsHouseRent = strIsHouseRent;
        this.IsMedical = strIsMedical;
        this.ShortName = strShortName;
        this.NaturalCode = strNaturalCode;
        this.ItemCategory = strItemCat;
    }
	public Payroll_SalaryHead()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
