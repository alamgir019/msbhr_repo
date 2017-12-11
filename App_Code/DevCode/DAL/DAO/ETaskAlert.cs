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
/// Summary description for ETaskAlert
/// </summary>
public class ETaskAlert
{
    private string _TRANSID;
    public string TRANSID
    {
        get { return _TRANSID; }
        set { _TRANSID = value; }
    }
    private string _FROMEMPID;
    public string FROMEMPID
    {
        get { return _FROMEMPID; }
        set { _FROMEMPID = value; }
    }
    private string _FROMADDR;
    public string FROMADDR
    {
        get { return _FROMADDR; }
        set { _FROMADDR = value; }
    }
    private string _TOEMPID;
    public string TOEMPID
    {
        get { return _TOEMPID; }
        set { _TOEMPID = value; }
    }

    private string _TOADDR;
    public string TOADDR
    {
        get { return _TOADDR; }
        set { _TOADDR = value; }
    }
    private string _CCADDR;
    public string CCADDR
    {
        get { return _CCADDR; }
        set { _CCADDR = value; }
    }
    private string _BCCADDR;
    public string BCCADDR
    {
        get { return _BCCADDR; }
        set { _BCCADDR = value; }
    }
    private string _SUBJECT;
    public string SUBJECT
    {
        get { return _SUBJECT; }
        set { _SUBJECT = value; }
    }
    private string _ATTACHMENT;
    public string ATTACHMENT
    {
        get { return _ATTACHMENT; }
        set { _ATTACHMENT = value; }
    }
    private string _BODY;
    public string BODY
    {
        get { return _BODY; }
        set { _BODY = value; }
    }
    private string _SCHDATETIME;
    public string SCHDATETIME
    {
        get { return _SCHDATETIME; }
        set { _SCHDATETIME = value; }
    }
    private string _STATUS;
    public string STATUS
    {
        get { return _STATUS; }
        set { _STATUS = value; }
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
    public ETaskAlert(string strTRANSID, string strFROMEMPID, string strFROMADDR,string strToEmpID, string strTOADDR,string strCCADDR,
        string strBCCADDR, string strSUBJECT, string strATTACHMENT, string strBODY, string strSCHDATETIME, string strSTATUS,
        string strInsertedBy,string strInsertedDate)
    {
        this.TRANSID = strTRANSID;
        this.FROMEMPID = strFROMEMPID;
        this.FROMADDR = strFROMADDR;
        this.TOEMPID = strToEmpID;
        this.TOADDR = strTOADDR;
        this.CCADDR = strCCADDR;
        this.BCCADDR = strBCCADDR;
        this.SUBJECT = strSUBJECT;
        this.ATTACHMENT = strATTACHMENT;
        this.BODY = strBODY;
        this.SCHDATETIME = strSCHDATETIME;
        this.STATUS = strSTATUS;
        this.InsertedBy = strInsertedBy;
        this.InsertedDate = strInsertedDate;
    }
}
