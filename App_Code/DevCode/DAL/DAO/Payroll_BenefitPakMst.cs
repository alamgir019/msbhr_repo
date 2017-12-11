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
/// Summary description for Payroll_BenefitPakMst
/// </summary>
public class Payroll_BenefitPakMst
{

    private string _PackageID;
    public string PackageID
    {
        get { return _PackageID; }
        set { _PackageID = value; }
    }

    private string _PackageName;
    public string PackageName
    {
        get { return _PackageName; }
        set { _PackageName = value; }
    }


    private string _PackageDescription;
    public string PackageDescription
    {
        get { return _PackageDescription; }
        set { _PackageDescription = value; }
    }

    private string _IsActive;
    public string IsActive
    {
        get { return _IsActive; }
        set { _IsActive = value; }
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


    public Payroll_BenefitPakMst(
        string PackageID, string PackageName, string PackageDescription, string IsActive, string InsertedBy, string InsertedDate)
    {
        this.PackageID = PackageID;
        this.PackageName = PackageName;
        this.PackageDescription = PackageDescription;
        this.IsActive = IsActive;
        this.InsertedBy = InsertedBy;
        this.InsertedDate = InsertedDate;

    }

	
}
