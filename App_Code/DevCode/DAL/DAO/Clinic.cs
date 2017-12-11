using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Clinic
/// </summary>
public class Clinic
{
    private string _ClinicId;
    private string _ClinicName;
    private string _ClinicShortName;
    private string _ClinicTypeId;
    private string _SunCode;
    private string _BankAccNo;
    private string _BankCode;
    private string _RoutingNo;
    private string _IsActive;
    private string _IsDeleted;
    private string _InsertedBy;
    private string _InsertedDate;
    private string _LastUpdatedFrom;
    private string _UpdatedBy;
    private string _UpdatedDate;

    public string ClinicId { get { return _ClinicId; } set { _ClinicId = value; } }
    public string ClinicName { get { return _ClinicName; } set { _ClinicName = value; } }
    public string ClinicShortName { get { return _ClinicShortName; } set { _ClinicShortName = value; } }
    public string ClinicTypeId { get { return _ClinicTypeId; } set { _ClinicTypeId = value; } }
    public string SunCode { get { return _SunCode; } set { _SunCode = value; } }
    public string BankAccNo { get { return _BankAccNo; } set { _BankAccNo = value; } }
    public string BankCode { get { return _BankCode; } set { _BankCode = value; } }
    public string RoutingNo { get { return _RoutingNo; } set { _RoutingNo = value; } }
    public string IsActive { get { return _IsActive; } set { _IsActive = value; } }
    public string IsDeleted { get { return _IsDeleted; } set { _IsDeleted = value; } }
    public string InsertedBy { get { return _InsertedBy; } set { _InsertedBy = value; } }
    public string InsertedDate { get { return _InsertedDate; } set { _InsertedDate = value; } }
    public string LastUpdatedFrom { get { return _LastUpdatedFrom; } set { _LastUpdatedFrom = value; } }
    public string UpdatedBy { get { return _UpdatedBy; } set { _UpdatedBy = value; } }
    public string UpdatedDate { get { return _UpdatedDate; } set { _UpdatedDate = value; } }

    public Clinic(string strClinicId, string strClinicName, string strClinicShortName, string strClinicTypeId, string strSunCode, string strBankAccNo, string strBankCode, string strRoutingNo, 
        string strIsActive,               string strIsDeleted, string strInsertedBy, string strInsertedDate, string strUpdatedBy, string strUpdatedDate, string strLastUpdatedFrom)
    {
        this.ClinicId = strClinicId;
        this.ClinicName = strClinicName;
        this.ClinicShortName = strClinicShortName;
        this.ClinicTypeId = strClinicTypeId;
        this.SunCode = strSunCode;
        this.BankAccNo = strBankAccNo;
        this.BankCode = strBankCode;
        this.RoutingNo = strRoutingNo;
        this.IsActive = strIsActive;
        this.IsDeleted = strIsDeleted;
        this.InsertedBy = strInsertedBy;
        this.InsertedDate = strInsertedDate;
        this.UpdatedBy = strUpdatedBy;
        this.UpdatedDate = strUpdatedDate;
        this.LastUpdatedFrom = strLastUpdatedFrom;
    }
}
