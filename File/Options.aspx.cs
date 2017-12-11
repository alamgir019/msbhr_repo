using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public partial class File_Options : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
  //  MasterTablesManager objMasMgr = new MasterTablesManager();
   // Payroll_MasterMgr objSalaryHeadMgr = new Payroll_MasterMgr();
    Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();
    DataTable dtpaySlip =new DataTable();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Common.FillDropDownList(objSalaryHeadMgr.SelectSalaryHeadDeduction(0, ""), ddlPFLoanDeduct);
            //Common.FillDropDownList(objSalaryHeadMgr.SelectSalaryHeadDeduction(0, ""), ddltaxdeducation);
            this.OpenRecord();
           // Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();
          //  objOptMgr.GetPaySlipOptionsValue(); 
            //objOptMgr.PAYSLIP_DEPOSITDEDUCTION_SALARYHEAD 
        }       
    }

    private void OpenRecord()
    {    
        dtpaySlip = objOptMgr.SelectpaySlipOption("");
        foreach (DataRow dtOp in dtpaySlip.Rows)
        {
            if (dtOp["OptId"].ToString() == "OC01".ToString())
            {
                txtRetAge.Text = dtOp["OptValue"].ToString();
            }         

        }
        dtpaySlip.Clear();
        dtpaySlip.Dispose();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        clsPaySlipOptions[] objOpt = new clsPaySlipOptions[1];
        try
        {
            objOpt[0] = new clsPaySlipOptions();
            //objOpt[0].OptID = objOpt[0].PAYSLIP_RET_AGE;
            objOpt[0].OptName = "RETIREMENT_AGE";
            objOpt[0].OptValue = txtRetAge.Text;
           
            ////objOpt[1].OptID = objOpt[0].PAYSLIP_BAISC_PERCENT;
            //objOpt[1].OptName = "PAYSLIP_BAISC_PERCENT";
            //objOpt[1].OptValue = txtBasicPercent.Text;

            Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();

            //objOptMgr.InsertOptionBag(objOpt, Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));

            lblMsg.Text = "Record Saved Successfully";
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }
}
