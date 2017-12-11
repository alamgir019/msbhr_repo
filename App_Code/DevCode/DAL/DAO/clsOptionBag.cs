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
/// Summary description for clsOptionBag
/// </summary>
public class clsOptionBag
{
    private string _SbuId;

    public string SbuId
    {
        get { return _SbuId; }
        set { _SbuId = value; }
    }
    private string _OptID;

    public string OptID
    {
        get { return _OptID; }
        set { _OptID = value; }
    }
    private string _OptOThour3Rate;

    public string OptOThour3Rate
    {
        get { return _OptOThour3Rate; }
        set { _OptOThour3Rate = value; }
    }

    private string _OptOThour4Rate;

    public string OptOThour4Rate
    {
        get { return _OptOThour4Rate; }
        set { _OptOThour4Rate = value; }
    }

    private string _OptOThour6Rate;

    public string OptOThour6Rate
    {
        get { return _OptOThour6Rate; }
        set { _OptOThour6Rate = value; }
    }

    private string _OptOThour3RateHoli;

    public string OptOThour3RateHoli
    {
        get { return _OptOThour3RateHoli; }
        set { _OptOThour3RateHoli = value; }
    }

    private string _OptOThour4RateHoli;

    public string OptOThour4RateHoli
    {
        get { return _OptOThour4RateHoli; }
        set { _OptOThour4RateHoli = value; }
    }

    private string _OptOThour6RateHoli;

    public string OptOThour6RateHoli
    {
        get { return _OptOThour6RateHoli; }
        set { _OptOThour6RateHoli = value; }
    }

    private string _BreakFastTK;

    public string BreakFastTK
    {
        get { return _BreakFastTK; }
        set { _BreakFastTK = value; }
    }
    private string _LunchTK;

    public string LunchTK
    {
        get { return _LunchTK; }
        set { _LunchTK = value; }
    }
    public clsOptionBag(string SbuId, string OptID, string OptOThour3Rate, string OptOThour4Rate, string OptOThour6Rate,
        string OptOThour3RateHoli, string OptOThour4RateHoli, string OptOThour6RateHoli, 
        string BreakFastTK, string LunchTK)
	{
        this.SbuId = SbuId;
        this.OptID = OptID;
        this.OptOThour3Rate = OptOThour3Rate;
        this.OptOThour4Rate = OptOThour4Rate;
        this.OptOThour6Rate = OptOThour6Rate;
        this.OptOThour3RateHoli = OptOThour3RateHoli;
        this.OptOThour4RateHoli = OptOThour4RateHoli;
        this.OptOThour6RateHoli = OptOThour6RateHoli;
        this.BreakFastTK = BreakFastTK;
        this.LunchTK = LunchTK; 
	}
}
