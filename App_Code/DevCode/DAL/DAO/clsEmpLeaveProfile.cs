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
/// Summary description for clsEmpLeaveProfile
/// </summary>
public class clsEmpLeaveProfile
{
	public clsEmpLeaveProfile()
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
    private string _LTypeID;
    public string LTypeID
    {
        get { return _LTypeID; }
        set { _LTypeID = value; }
    }    
    private string _LEntitled;
    public string LEntitled
    {
        get { return _LEntitled; }
        set { _LEntitled = value; }
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
    public clsEmpLeaveProfile(string EmpId,
        string LTypeID,       
        string LEntitled,        
        string InsertedBy,
        string InsertedDate
        )
	{
        this.EmpId = EmpId;
        this.LTypeID = LTypeID;        
        this.LEntitled = LEntitled;       
        this.InsertedBy = InsertedBy;
        this.InsertedDate = InsertedDate;
	}
}
