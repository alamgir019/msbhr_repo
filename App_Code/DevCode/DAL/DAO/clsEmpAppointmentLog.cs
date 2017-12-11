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
/// Summary description for clsEmpAppointmentLog
/// </summary>
public class clsEmpAppointmentLog
{
    public string _AppointmentId;
    public string AppointmentId
    {
        get { return _AppointmentId; }
        set { _AppointmentId = value; }
    }

    public string _EmpId;
    public string EmpId
    {
        get { return _EmpId; }
        set { _EmpId = value; }
    }

    public string _ActionId;
    public string ActionId
    {
        get { return _ActionId; }
        set { _ActionId = value; }
    }

    public string _ActionName;
    public string ActionName
    {
        get { return _ActionName; }
        set { _ActionName = value; }
    }

    public string _EmpTypeID;
    public string EmpTypeID
    {
        get { return _EmpTypeID; }
        set { _EmpTypeID = value; }
    }

    public string _GradeId;
    public string GradeId
    {
        get { return _GradeId; }
        set { _GradeId = value; }
    }

    public string _GradeLevelId;
    public string GradeLevelId
    {
        get { return _GradeLevelId; }
        set { _GradeLevelId = value; }
    }

    public string _BasicSal;
    public string BasicSal
    {
        get { return _BasicSal; }
        set { _BasicSal = value; }
    }

    public string _DesgId;
    public string DesgId
    {
        get { return _DesgId; }
        set { _DesgId = value; }
    }

    public string _SubDesigID;
    public string SubDesigID
    {
        get { return _SubDesigID; }
        set { _SubDesigID = value; }
    }

    public string _DeptId;
    public string DeptId
    {
        get { return _DeptId; }
        set { _DeptId = value; }
    }

    public string _DivisionID;
    public string DivisionID
    {
        get { return _DivisionID; }
        set { _DivisionID = value; }
    }

    public string _LocID;
    public string LocID
    {
        get { return _LocID; }
        set { _LocID = value; }
    }

    public string _JoiningDate;
    public string JoiningDate
    {
        get { return _JoiningDate; }
        set { _JoiningDate = value; }
    }

    public string _ContExpDate;
    public string ContExpDate
    {
        get { return _ContExpDate; }
        set { _ContExpDate = value; }
    }

    public string _InsertedBy;
    public string InsertedBy
    {
        get { return _InsertedBy; }
        set { _InsertedBy = value; }
    }

    public string _InsertedDate;
    public string InsertedDate
    {
        get { return _InsertedDate; }
        set { _InsertedDate = value; }
    }

    public clsEmpAppointmentLog(string AppointmentId, string EmpId, string ActionId, string ActionName, string EmpTypeID, string GradeId, string GradeLevelId,
        string BasicSal, string DesgId, string SubDesigID, string DeptId, string DivisionID, string LocID, string JoiningDate, string ContExpDate,
        string InsertedBy, string InsertedDate)
	{
        this.AppointmentId = AppointmentId;
        this.EmpId = EmpId;
        this.ActionId = ActionId;
        this.ActionName = ActionName;
        this.EmpTypeID = EmpTypeID;
        this.GradeId = GradeId;
        this.GradeLevelId = GradeLevelId;
        this.BasicSal = BasicSal;
        this.DesgId = DesgId;
        this.SubDesigID = SubDesigID;
        this.DeptId = DeptId;
        this.DivisionID = DivisionID;
        this.LocID = LocID;
        this.JoiningDate = JoiningDate;
        this.ContExpDate = ContExpDate;
        this.InsertedBy = InsertedBy;
        this.InsertedDate = InsertedDate;
	}
}
