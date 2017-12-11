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
/// Summary description for clsPaySlipOptions
/// </summary>
public class clsPaySlipOptions
{

    private string _OptID ="";
    private string _OptName = "";
    private string _OptValue = "";
    private string _ValidFrom = "";
    private string _ValidTo = "";

    public string PAYSLIP_PF_LOAN_DEDUCT_SALARY_HEAD_ID = "OC01";
    public string PAYSLIP_TAXDEDEDUCTION_SALARYHEAD = "OC02";
    public string PAYSLIP_VALIDITY = "OC03";

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

    public string OptID
    {
        get { return _OptID; }
        set { _OptID = value; }
    }

    public string OptName
    {
        get { return _OptName; }
        set { _OptName = value; }
    }

    public string OptValue
    {
        get { return _OptValue; }
        set { _OptValue = value; }
    }
    private string _isUpdate;

    public string IsUpdate
    {
        get { return _isUpdate; }
        set { _isUpdate = value; }
    }

    public string ValidFrom
    {
        get { return _ValidFrom; }
        set { _ValidFrom = value; }
    }

    public string ValidTo
    {
        get { return _ValidTo; }
        set { _ValidTo = value; }
    }
    //public clsPaySlipOptions()
    //{
    //    //
    //    // TODO: Add constructor logic here
    //    //
    //}
}
