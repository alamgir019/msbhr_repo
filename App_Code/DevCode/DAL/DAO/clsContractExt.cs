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
/// Summary description for clsContractExt
/// Created by Murad
/// 13.10.2011
/// </summary>
public class clsContractExt
{
    private string _EmpID;
    public string EmpID
    {
        get { return _EmpID; }
        set { _EmpID = value; }
    }
    private string _ContractExtId;
    public string ContractExtId
    {
        get { return _ContractExtId; }
        set { _ContractExtId = value; }
    }
    private string _ActionId;
    public string ActionId
    {
        get { return _ActionId; }
        set { _ActionId = value; }
    }
    private string _EffectiveDate;
    public string EffectiveDate
    {
        get { return _EffectiveDate; }
        set { _EffectiveDate = value; }
    }
    private string _ContractExtDate;
    public string ContractExtDate
    {
        get { return _ContractExtDate; }
        set { _ContractExtDate = value; }
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




    public clsContractExt(string emId, string ContractExtId, string actionId, string effDate, 
        string contExtDate, string insertedBy, string insertedDate)
	{
        this.EmpID = emId;
        this.ContractExtId = ContractExtId;
        this.ActionId = actionId;
        this.EffectiveDate = effDate;
        this.ContractExtDate = contExtDate;
        this.InsertedBy = insertedBy;
        this.InsertedDate = insertedDate;
	}

    private string _ReHiredId;
    public string ReHiredId
    {
        get { return _ReHiredId; }
        set { _ReHiredId = value; }
    }

    public clsContractExt(string emId, string reHiredId, string actionId, string effDate,
         string insertedBy, string insertedDate)
    {
        this.EmpID = emId;
        this.ReHiredId = reHiredId;
        this.ActionId = actionId;
        this.EffectiveDate = effDate;        
        this.InsertedBy = insertedBy;
        this.InsertedDate = insertedDate;
    }

}
