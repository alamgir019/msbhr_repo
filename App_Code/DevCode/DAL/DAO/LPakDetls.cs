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
/// Summary LeaveDesc for LPakDetls
/// </summary>
/// 


public class LPakDetls
{
    private string _LPakID;

    public string LPakID
    {
        get { return _LPakID; }
        set { _LPakID = value; }
    }

    private string _LTypeID;

    public string LTypeID
    {
        get { return _LTypeID; }
        set { _LTypeID = value; }
    }


    private string _MaxLAmt;

    public string MaxLAmt
    {
        get { return _MaxLAmt; }
        set { _MaxLAmt = value; }
    }

    private string _IsLeavePay;

    public string IsLeavePay
    {
        get { return _IsLeavePay; }
        set { _IsLeavePay = value; }
    }

    private string _LeavePayAmnt;

    public string LeavePayAmnt
    {
        get { return _LeavePayAmnt; }
        set { _LeavePayAmnt = value; }
    }


    private string _MaxAllowed;

    public string MaxAllowed
    {
        get { return _MaxAllowed; }
        set { _MaxAllowed = value; }
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


    public LPakDetls(string LPakID, string LTypeID, string MaxLAmt,string MaxAllowed ,
        string InsertedBy,
        string InsertedDate)//16
    {
        this.LPakID = LPakID;//1
        this.LTypeID = LTypeID;//2
        this.MaxLAmt = MaxLAmt;//3       
        this.MaxAllowed = MaxAllowed;
        this.InsertedBy = InsertedBy;//13
        this.InsertedDate = InsertedDate;//14                
       
    }
}
