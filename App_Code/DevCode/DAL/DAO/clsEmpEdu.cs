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
/// Summary description for clsEmpEdu
/// </summary>
public class clsEmpEducation
{
    private string _EmpID;
    public string EmpID
    {
        get { return _EmpID; }
        set { _EmpID = value; }
    }
    private string _EduId;
    public string EduId
    {
        get { return _EduId; }
        set { _EduId = value; }
    }
    private string _DegreeId;
    public string DegreeId
    {
        get { return _DegreeId; }
        set { _DegreeId = value; }
    }

   private string _InstituteId;
    public string InstituteId
    {
        get { return _InstituteId; }
        set { _InstituteId = value; }
    }
    private string _SubjectId;
    public string SubjectId
    {
        get { return _SubjectId; }
        set { _SubjectId = value; }
    }
    private string _ResultId;
    public string ResultId
    {
        get { return _ResultId; }
        set { _ResultId = value; }
    }
    private string _PassedYear;
    public string PassedYear
    {
        get { return _PassedYear; }
        set { _PassedYear = value; }
    }
    private string _Marks;
    public string Marks
    {
        get { return _Marks; }
        set { _Marks = value; }
    }
    private string _DegreeTitle;
    public string DegreeTitle
    {
        get { return _DegreeTitle; }
        set { _DegreeTitle = value; }
    }
    private string _IsMaxDegree;
    public string IsMaxDegree
    {
        get { return _IsMaxDegree; }
        set { _IsMaxDegree = value; }
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

    public clsEmpEducation(string EmpID,
    string EduId,
    string DegreeId,
    string InstituteId,
    string SubjectId,
    string ResultId,
    string PassedYear,
    string Marks,
    string DegreeTitle,
    string IsMaxDegree,
    string InsertedBy,
    string InsertedDate
    )
    {
    this.EmpID=EmpID;
    this.EduId = EduId;
    this.DegreeId=DegreeId;
    this.InstituteId=InstituteId;
    this.SubjectId = SubjectId;
    this.ResultId=ResultId;
    this.PassedYear=PassedYear;
    this.Marks=Marks;
    this.DegreeTitle = DegreeTitle;
    this.IsMaxDegree = IsMaxDegree;
    this.PassedYear = PassedYear;
    this.InsertedBy = InsertedBy;
    this.InsertedDate = InsertedDate;		
    }
}
