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

public partial class Payroll_Loan_MonthlyPFUpdate : System.Web.UI.Page
{
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    Payroll_LoanAppManager objLoanMgr = new Payroll_LoanAppManager();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
    dsPayroll_Loan objDs = new dsPayroll_Loan();
    Payroll_PayslipApprovalManager objPayAppMgr = new Payroll_PayslipApprovalManager();
    MasterTablesManager objMastMg = new MasterTablesManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //this.EntryMode(false);
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);
            Common.FillDropDownList(objMastMg.SelectEmpGroup(0), ddlGroup, "GrpName", "EmpGrpID", false);
        }
    }

   protected decimal GetSalHeadAmt(DataTable dt, string strEmpID, string strSHeadID)
    {
        decimal dclSalHeadAmt = 0;
        DataRow[] foundRows = dt.Select("EMPID='" + strEmpID + "' AND SHEADID=" + strSHeadID);
        if (foundRows.Length > 0)
        {
            dclSalHeadAmt = Convert.ToDecimal(foundRows[0]["PAYAMT"].ToString());
        }
        dclSalHeadAmt = Common.RoundDecimal(dclSalHeadAmt.ToString(), 0);
        return dclSalHeadAmt;
    }

    protected decimal GetEmpBenefitsAndDeductionAmount(DataTable dt, string strEmpID)
    {
        //dclTotalSalary = Convert.ToDecimal(strGrossSal);
        decimal dclSalHeadAmt = 0;
        decimal dclEmpDeduct = 0;
        DataRow[] foundRows = dt.Select("EMPID='" + strEmpID + "'");
        foreach (DataRow dRow in foundRows)
        {
            dclSalHeadAmt = 0;
            switch (dRow["ISDEDUCTED"].ToString())
            {
                //case "N":
                //    //dclEmpBenefits = dclEmpBenefits + this.GetSalHeadAmt(strEmpID, dRow["SHEADID"].ToString());
                //    break;
                case "Y":
                    dclSalHeadAmt = Common.RoundDecimal(dRow["PAYAMT"].ToString().Trim(), 0);
                    dclEmpDeduct = dclEmpDeduct + dclSalHeadAmt;
                    break;
            }
        }
        return dclEmpDeduct;
    }

    protected void btnUpdateGratuityLedger_Click(object sender, EventArgs e)
    {
        if (objLoanMgr.IsCurrentMonthGratuityLedgerExist(ddlMonth.SelectedValue.Trim(), ddlFiscalYear.SelectedValue.Trim()) == false)
        {
            DataTable dtEmpPayroll = objLoanMgr.GetDistinctEmployeeForLedger(ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString());
            DataTable dtEmpGratuityData = objPayAppMgr.GetGratuityFromSalary(ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), ddlGroup.SelectedValue.Trim());
            objLoanMgr.PrepareGratuityLedgerData(dtEmpPayroll, dtEmpGratuityData, ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.Trim(), "D", Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()), "S", Session["FISCALYRID"].ToString().Trim());
            if (dtEmpGratuityData.Rows.Count > 0)
            {
                lblMsg.Text = ddlMonth.SelectedItem.Text.Trim() + " Gratuity Ledger Prepared Successfully.";
            }
            else
            {
                lblMsg.Text = "Ledger not prepared. " + ddlMonth.SelectedItem.Text.Trim() + " month salary has not been disbursed yet.";
            }
        }
        else
        {
            lblMsg.Text = ddlMonth.SelectedItem.Text.Trim() + " PF Ledger Aleady Prepared.";
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objLoanMgr.DeleteGratuityLedgerData(ddlMonth.SelectedValue.Trim(), ddlFiscalYear.SelectedValue.Trim());
        lblMsg.Text = ddlMonth.SelectedItem.Text.Trim() + " PF Ledger Deleted Successfully.";
    }
}
