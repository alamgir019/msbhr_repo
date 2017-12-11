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
/// Summary description for clsEmpTransfer
/// </summary>
public class clsEmpTransfer
{
	public clsEmpTransfer()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string _TransId;
    public string TransId
    {
        get { return _TransId; }
        set { _TransId = value; }
    }

    private string _EmpId;
    public string EmpId
    {
        get { return _EmpId; }
        set { _EmpId = value; }
    }
    private string _EffectiveDate;
    public string EffectiveDate
    {
        get { return _EffectiveDate; }
        set { _EffectiveDate = value; }
    }
    private string _OEmpTypeStatus;
    public string OEmpTypeStatus
    {
        get { return _OEmpTypeStatus; }
        set { _OEmpTypeStatus = value; }
    }
    private string _OEmpSubTypeStatus;
    public string OEmpSubTypeStatus
    {
        get { return _OEmpSubTypeStatus; }
        set { _OEmpSubTypeStatus = value; }
    }
    private string _NEmpTypeStatus;
    public string NEmpTypeStatus
    {
        get { return _NEmpTypeStatus; }
        set { _NEmpTypeStatus = value; }
    }
    private string _NEmpSubTypeStatus;
    public string NEmpSubTypeStatus
    {
        get { return _NEmpSubTypeStatus; }
        set { _NEmpSubTypeStatus = value; }
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

    private string _Remarks;
    public string Remarks
    {
        get { return _Remarks; }
        set { _Remarks = value; }
    }

    public clsEmpTransfer(
        string TransId,
        string EmpId,
        string EffectiveDate,
        string OEmpTypeStatus,
        string OEmpSubTypeStatus,
        string NEmpTypeStatus,
        string NEmpSubTypeStatus,
        string InsertedBy,
        string InsertedDate,
        string Remarks
        )
	{
        this.TransId = TransId;
        this.EmpId = EmpId;
        this.EffectiveDate = EffectiveDate;
        this.OEmpTypeStatus = OEmpTypeStatus;
        this.OEmpSubTypeStatus = OEmpSubTypeStatus;
        this.NEmpTypeStatus = NEmpTypeStatus;
        this.NEmpSubTypeStatus = NEmpSubTypeStatus;
        this.InsertedBy = InsertedBy;
        this.InsertedDate = InsertedDate;
        this.Remarks = Remarks;
	}
}
