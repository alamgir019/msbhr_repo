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
/// Summary description for Payroll_FiscalYear
/// </summary>
public class Payroll_FiscalYear
{
    private string _FiscalYrId;
    public string FiscalYrId
    {
        get { return _FiscalYrId; }
        set { _FiscalYrId = value; }
    }
    private string _FiscalYrCode;
    public string FiscalYrCode
    {
        get { return _FiscalYrCode; }
        set { _FiscalYrCode = value; }
    }
    private string _FiscalYrTitle;
    public string FiscalYrTitle
    {
        get { return _FiscalYrTitle; }
        set { _FiscalYrTitle = value; }
    }
    private string _FiscalDesc;
    public string FiscalDesc
    {
        get { return _FiscalDesc; }
        set { _FiscalDesc = value; }
    }
    private string _StartDate;
    public string StartDate
    {
        get { return _StartDate; }
        set { _StartDate = value; }
    }
    private string _EndDate;
    public string EndDate
    {
        get { return _EndDate; }
        set { _EndDate = value; }
    }
    private string _IsClosed;
    public string IsClosed
    {
        get { return _IsClosed; }
        set { _IsClosed = value; }
    }
    private string _IsCurrFiscalYr;
    public string IsCurrFiscalYr
    {
        get { return _IsCurrFiscalYr; }
        set { _IsCurrFiscalYr = value; }
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
    public Payroll_FiscalYear(string strFiscalYrId, string strFiscalYrCode, string strFiscalYrTitle, string strFiscalDesc,
        string strStartDate, string strEndDate, string strIsClosed,string strIsCurrFiscalYr, string strIsDeleted, string strInsertedBy, string strInsertedDate, string strIsUpdate, string strIsDelete)
	{
        this.FiscalYrId = strFiscalYrId;
        this.FiscalYrCode = strFiscalYrCode;
        this.FiscalYrTitle = strFiscalYrTitle;
        this.FiscalDesc = strFiscalDesc;
        this.StartDate = strStartDate;
        this.EndDate = strEndDate;
        this.IsClosed = strIsClosed;
        this.IsCurrFiscalYr = strIsCurrFiscalYr;
        this.IsDeleted = strIsDeleted;
        this.InsertedBy = strInsertedBy;
        this.InsertedDate = strInsertedDate;
        this.IsUpdate = strIsUpdate;
        this.IsDelete = strIsDelete;
		//
		// TODO: Add constructor logic here
		//
	}
}
