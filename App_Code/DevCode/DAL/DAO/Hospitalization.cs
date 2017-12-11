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
/// Summary description for Hospitalization
/// </summary>
public class Hospitalization
{
    private string _EmpID;

    public string EmpID
    {
        get { return _EmpID; }
        set { _EmpID = value; }
    }

    private string _FmId;

    public string FmId
    {
        get { return _FmId; }
        set { _FmId = value; }
    }

    private string _Diseas;

    public string Diseas
    {
        get { return _Diseas; }
        set { _Diseas = value; }
    }

    private string _AdmittedOn;

    public string AdmittedOn
    {
        get { return _AdmittedOn; }
        set { _AdmittedOn = value; }
    }

    private string _ReleasedOn;

    public string ReleasedOn
    {
        get { return _ReleasedOn; }
        set { _ReleasedOn = value; }
    }

    private string _Hospital;

    public string Hospital
    {
        get { return _Hospital; }
        set { _Hospital = value; }
    }

    private string _ClaimedAmt;

    public string ClaimedAmt
    {
        get { return _ClaimedAmt; }
        set { _ClaimedAmt = value; }
    }

    private string _ReimbursedAmt;

    public string ReimbursedAmt
    {
        get { return _ReimbursedAmt; }
        set { _ReimbursedAmt = value; }
    }

    private string _ClaimedOn;

    public string ClaimedOn
    {
        get { return _ClaimedOn; }
        set { _ClaimedOn = value; }
    }


    private string _ReimbursedOn;

    public string ReimbursedOn
    {
        get { return _ReimbursedOn; }
        set { _ReimbursedOn = value; }
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



	public Hospitalization(string strEmpId,string strFmId,string strDiseas,string strAdmitted,string strRelease,string strHospital,
        string strClaimedAmt,string strReimAmt, string strClaimDate,string strReimDate, string strInsBy,string strInsDate)
	{
        this.EmpID = strEmpId;
        this.FmId = strFmId;
        this.Diseas = strDiseas;
        this.AdmittedOn = strAdmitted;
        this.ReleasedOn = strRelease;
        this.Hospital = strHospital;
        this.ClaimedAmt = strClaimedAmt;
        this.ReimbursedAmt = strReimAmt;
        this.ClaimedOn = strClaimDate;
        this.ReimbursedOn = strReimAmt;
        this.InsertedBy = strInsBy;
        this.InsertedDate = strInsDate;
		//
		// TODO: Add constructor logic here
		//
	}
}
