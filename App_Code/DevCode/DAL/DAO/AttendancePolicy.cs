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
/// Summary description for AttendancePolicy
/// </summary>
public class AttendancePolicy
{
    private string _AttnPolicyId;
    private string _PolicyName;
    private string _PoliDesc;
    private string _OTStartGrace;
    private string _ArvlGrace;
    private string _LunchBreak;
    private string _IfLateNoOT;
    private string _OTLateMin;
    private string _InTime;
    private string _OutTime;
    private string _IsNextDay;
    private string _IsActive;
    private string _InsertedBy;
    private string _InsertedDate;
    private string _LunchTime;
    private string _WorkingHr;
    private string _ISLeaveAff;
    private string _LeaveAffTime;
    private string _ISDefault;
    private string _WeekEndID;
    private string _DivisionId;
    private string _SBUId;

    public string AttnPolicyId
    {
        get { return _AttnPolicyId; }
        set { _AttnPolicyId = value; }
    }

    public string PolicyName
    {
        get { return _PolicyName; }
        set { _PolicyName = value; }
    }

    public string PoliDesc
    {
        get { return _PoliDesc; }
        set { _PoliDesc = value; }
    }

    public string OTStartGrace
    {
        get { return _OTStartGrace; }
        set { _OTStartGrace = value; }
    }

    public string ArvlGrace
    {
        get { return _ArvlGrace; }
        set { _ArvlGrace = value; }
    }

    public string LunchBreak
    {
        get { return _LunchBreak; }
        set { _LunchBreak = value; }
    }

    public string IfLateNoOT
    {
        get { return _IfLateNoOT; }
        set { _IfLateNoOT = value; }
    }

    public string OTLateMin
    {
        get { return _OTLateMin; }
        set { _OTLateMin = value; }
    }

    public string InTime
    {
        get { return _InTime; }
        set { _InTime = value; }
    }

    public string OutTime
    {
        get { return _OutTime; }
        set { _OutTime = value; }
    }

    public string IsNextDay
    {
        get { return _IsNextDay; }
        set { _IsNextDay = value; }
    }

    public string IsActive
    {
        get { return _IsActive; }
        set { _IsActive = value; }
    }

    public string InsertedBy
    {
        get { return _InsertedBy; }
        set { _InsertedBy = value; }
    }

    public string InsertedDate
    {
        get { return _InsertedDate; }
        set { _InsertedDate = value; }
    }

    public string LunchTime
    {
        get { return _LunchTime; }
        set { _LunchTime = value; }
    }

    public string WorkingHr
    {
        get { return _WorkingHr; }
        set { _WorkingHr = value; }
    }

    public string ISLeaveAff
    {
        get { return _ISLeaveAff; }
        set { _ISLeaveAff = value; }
    }

    public string LeaveAffTime
    {
        get { return _LeaveAffTime; }
        set { _LeaveAffTime = value; }
    }

    public string ISDefault
    {
        get { return _ISDefault; }
        set { _ISDefault = value; }
    }

    public string WeekEndID
    {
        get { return _WeekEndID; }
        set { _WeekEndID = value; }
    }

    public string DivisionId
    {
        get { return _DivisionId; }
        set { _DivisionId = value; }
    }

    public string SBUId
    {
        get { return _SBUId; }
        set { _SBUId = value; }
    }

    public AttendancePolicy(string strAttnPolicyId, string strPolicyName, string strPoliDesc, string strOTStartGrace, string strArvlGrace,
        string strLunchBreak, string strInTime, string strOutTime, string strIsNextDay, string strIsActive, string strISDefault,
        string strInsertedBy, string strInsertedDate, string strLunchTime, string strWorkingHr, string strDivisionId, string strSBUId)
	{
        AttnPolicyId = strAttnPolicyId;
        PolicyName = strPolicyName;
        PoliDesc = strPoliDesc;
        OTStartGrace = strOTStartGrace;
        ArvlGrace = strArvlGrace;
        LunchBreak = strLunchBreak;
        InTime = strInTime;
        OutTime = strOutTime;
        IsNextDay = strIsNextDay;
        IsActive = strIsActive;
        ISDefault = strISDefault;
        InsertedBy = strInsertedBy;
        InsertedDate = strInsertedDate;
        LunchTime = strLunchTime;
        WorkingHr = strWorkingHr;
        DivisionId = strDivisionId;
        SBUId = strSBUId;
	}
}
