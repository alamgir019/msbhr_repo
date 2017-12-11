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
/// Summary description for clsTempDutyAssign
/// </summary>
public class clsTempDutyAssign
{
    private string _DutyAssignID;
    public string DutyAssignID
    {
        get { return _DutyAssignID; }
        set { _DutyAssignID = value; }
    }

    private string _EmpID;
    public string EmpID
    {
        get { return _EmpID; }
        set { _EmpID = value; }
    }

    private string _ActionId;
    public string ActionId
    {
        get { return _ActionId; }
        set { _ActionId = value; }
    }

    private string _DivisionId;
    public string DivisionId
    {
        get { return _DivisionId; }
        set { _DivisionId = value; }
    }

    private string _ClinicId;
    public string ClinicId
    {
        get { return _ClinicId; }
        set { _ClinicId = value; }
    }

    private string _ProjectId;
    public string ProjectId
    {
        get { return _ProjectId; }
        set { _ProjectId = value; }
    }

    
    private string _DeptId;
    public string DeptId
    {
        get { return _DeptId; }
        set { _DeptId = value; }
    }

    private string _SubDeptId;
    public string SubDeptId
    {
        get { return _SubDeptId; }
        set { _SubDeptId = value; }
    }


    private string _Assignment;
    public string Assignment
    {
        get { return _Assignment; }
        set { _Assignment = value; }
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

    private string _Percentage;
    public string Percentage
    {
        get { return _Percentage; }
        set { _Percentage = value; }
    }

    private string _Amount;
    public string Amount
    {
        get { return _Amount; }
        set { _Amount = value; }
    }
    private string _SupervisorId;
    public string SupervisorId
    {
        get { return _SupervisorId; }
        set { _SupervisorId = value; }
    }
    private string _SupervisorComment;
    public string SupervisorComment
    {
        get { return _SupervisorComment; }
        set { _SupervisorComment = value; }
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

    public clsTempDutyAssign(string DutyAssignID, string EmpID, string ActionId, string DivisionId, string ClinicId, string ProjectId, string DeptId,string SubDeptId,
        string Assignment, string StartDate, string EndDate, string Percentage, string Amount, string SupervisorId, string SupervisorComment,string InsertedBy,string InsertedDate        )
	{
        this.DutyAssignID = DutyAssignID;
        this.EmpID = EmpID;
        this.ActionId = ActionId;
        this.DivisionId = DivisionId;
        this.ClinicId = ClinicId;
        this.ProjectId = ProjectId;         
        this.DeptId = DeptId;
        this.SubDeptId = SubDeptId;
        this.Assignment = Assignment;
        this.StartDate = StartDate;
        this.EndDate = EndDate;
        this.Percentage = Percentage;
        this.Amount = Amount;
        this.SupervisorId = SupervisorId;
        this.SupervisorComment = SupervisorComment;
        this.InsertedBy = InsertedBy;
        this.InsertedDate = InsertedDate;
	}
}
