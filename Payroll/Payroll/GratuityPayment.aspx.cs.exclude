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
using System.IO;
using System.Text;
public partial class Payroll_Payroll_GratuityPayment : System.Web.UI.Page
{
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    Payroll_GratuityLedgerManager objGrMgr = new Payroll_GratuityLedgerManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           
            Common.FillMonthList(ddlMonth);           
            Common.FillYearList(5, ddlYear);

            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);                       

            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0, "F"), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);           
        }
    }

    protected void btnProcess_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
        {
            this.SaveData();
        }
    }

    protected bool ValidateAndSave()
    {

        if (objGrMgr.IsCurrentMonthPaymentExist("", ddlMonth.SelectedValue.Trim(), ddlFiscalYear.SelectedValue.Trim()) == true)
        {
            lblMsg.Text = "Current month payment already exist.Operation cannot be processed.";
            return false;
        }

        return true;
    }

    protected void SaveData()
    {
        string strProcessDate = "";
        if (string.IsNullOrEmpty(txtProcessDate.Text.Trim()) == false)
            strProcessDate = Common.ReturnDate(txtProcessDate.Text.Trim());

        objGrMgr.ExecuteGratuityProcess(ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), ddlQuarter.SelectedValue.ToString(), strProcessDate, ddlFiscalYear.SelectedValue.ToString(), "");
        lblMsg.Text = "Gratuity Processed Successfully";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.ValidateAndSave() == true)
        {
            this.SaveData();
        }
    }
}
