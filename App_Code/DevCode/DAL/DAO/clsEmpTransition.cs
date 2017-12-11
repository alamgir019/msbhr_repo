using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


//Created by Murad
//17.10.2011
/// <summary>
/// Summary description for clsEmpTransition
/// </summary>
public class clsEmpTransition
{

    private string _TransId;
    public string TransId
    {
        get { return _TransId; }
        set { _TransId = value; }
    }
    private string _EmpID;
    public string EmpID
    {
        get { return _EmpID; }
        set { _EmpID = value; }
    }

    private string _EntryDate;
    public string EntryDate
    {
        get { return _EntryDate; }
        set { _EntryDate = value; }
    }

    private string _TransType;
    public string TransType
    {
        get { return _TransType; }
        set { _TransType = value; }
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
    private string _DesigId;
    public string DesigId
    {
        get { return _DesigId; }
        set { _DesigId = value; }
    }

    
    private string _GradeId;
    public string GradeId
    {
        get { return _GradeId; }
        set { _GradeId = value; }
    }

    private string _SalLocId;
    public string SalLocId
    {
        get { return _SalLocId; }
        set { _SalLocId = value; }
    }

    private string _EmpTypeId;
    public string EmpTypeId
    {
        get { return _EmpTypeId; }
        set { _EmpTypeId = value; }
    }


    private string _BasicSal;
    public string BasicSal
    {
        get { return _BasicSal; }
        set { _BasicSal = value; }
    }
  
    private string _EffDate;
    public string EffDate
    {
        get { return _EffDate; }
        set { _EffDate = value; }
    }  

    private string _NextIncDate;
    public string NextIncDate
    {
        get { return _NextIncDate; }
        set { _NextIncDate = value; }
    }

    private string _SalaryChangeDate;
    public string SalaryChangeDate
    {
        get { return _SalaryChangeDate; }
        set { _SalaryChangeDate = value; }
    }

    private string _GradeChangeDate;
    public string GradeChangeDate
    {
        get { return _GradeChangeDate; }
        set { _GradeChangeDate = value; }
    }

    private string _Remarks;
    public string Remarks
    {
        get { return _Remarks; }
        set { _Remarks = value; }
    }

    private string _IsTAApplicable;
    public string IsTAApplicable
    {
        get { return _IsTAApplicable; }
        set { _IsTAApplicable = value; }
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

    public clsEmpTransition(string TransId, string EmpId, string EntryDate, string TransType, string ActionId, string DivisionId, string ClinicId,
        string ProjectId, string DeptId,string strsubDeptId, string DesigId, string GradeId, string SalLocId, string strEmpTypeId, string BasicSal, string EffDate, string NextIncDate,string GradeChangeDate,  string Remarks, string IsTAApplicable,string InsertedBy, string InsertedDate)
    {
        this.TransId = TransId;
        this.EmpID = EmpId;
        this.EntryDate = EntryDate;
        this.TransType = TransType;
        this.ActionId = ActionId;
        this.DivisionId = DivisionId;
        this.ClinicId = ClinicId;
        this.ProjectId = ProjectId;
        this.DeptId = DeptId;
        this.SubDeptId = strsubDeptId;
        this.DesigId = DesigId;
        this.GradeId = GradeId;
        this.SalLocId = SalLocId;
        this.EmpTypeId = strEmpTypeId;
        this.BasicSal = BasicSal;
        this.EffDate = EffDate;
        this.NextIncDate = NextIncDate;
        this.SalaryChangeDate = SalaryChangeDate;
        this.GradeChangeDate = GradeChangeDate;
        this.Remarks = Remarks;
        this.InsertedBy = InsertedBy;
        this.InsertedDate = InsertedDate;
        this.IsTAApplicable = IsTAApplicable; 
    }
}
