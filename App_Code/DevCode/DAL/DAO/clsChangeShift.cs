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
/// Summary description for clsChangeShift
/// </summary>
public class clsChangeShift
{
	public clsChangeShift()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string _EmpId;
    public string EmpId
    {
        get { return _EmpId; }
        set { _EmpId = value; }
    }
    private string _EmpName;
    public string EmpName
    {
        get { return _EmpName; }
        set { _EmpName = value; }
    }
    private string _Designation;
    public string Designation
    {
        get { return _Designation; }
        set { _Designation = value; }
    }
    private string _IsRoaster;
    public string IsRoaster
    {
        get { return _IsRoaster; }
        set { _IsRoaster = value; }
    }
    private string _ShiftId;
    public string ShiftId
    {
        get { return _ShiftId; }
        set { _ShiftId = value; }
    }
    private string _WeeekendId;
    public string WeeekendId
    {
        get { return _WeeekendId; }
        set { _WeeekendId = value; }
    }
}
