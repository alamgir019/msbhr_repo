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
using cashword.BLL;

public partial class Payroll_Payroll_MonthlyPaySlipReport : System.Web.UI.Page
{   
    EmpInfoManager objEmp = new EmpInfoManager();
    PayrollReportManager objPayRptMgr = new PayrollReportManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strParams = Request.QueryString["params"];
            string[] strVal = strParams.Split(',');

            if(strVal[4].Trim()=="S")
                lblMonth.Text = "Payslip for the Month of " + strVal[1] + ", " + strVal[2];
            else
                lblMonth.Text = "Festival Allowance - " + strVal[1] + " " + strVal[2];

            // Get Employee Personal data             
            DataTable dtPersonal = objEmp.SelectEmpPayslipPersonalInfo(strVal[3].Trim());
            if (dtPersonal.Rows.Count > 0)
            {
                 lblID.Text = dtPersonal.Rows[0]["EmpId"].ToString().Trim();
                 lblName.Text = dtPersonal.Rows[0]["FULLNAME"].ToString().Trim();
                 lblGrade.Text = dtPersonal.Rows[0]["GradeName"].ToString().Trim();
                 lblJobTitle.Text = dtPersonal.Rows[0]["LocCatName"].ToString().Trim();
                 lblBankAc.Text = dtPersonal.Rows[0]["BankAccNo"].ToString().Trim();
                 lblDesig.Text = dtPersonal.Rows[0]["DesigName"].ToString().Trim();
                 lblPostDiv.Text = dtPersonal.Rows[0]["DivisionName"].ToString().Trim();
                 lblDept.Text = dtPersonal.Rows[0]["DeptName"].ToString().Trim();
                 lblPostDist.Text = dtPersonal.Rows[0]["ProjectName"].ToString().Trim();
                 lblSalLoc.Text = dtPersonal.Rows[0]["ClinicName"].ToString().Trim();
            }           
            DataTable dt = new DataTable();
            dt = (DataTable)objPayRptMgr.GetPayslipMonthlyGrossAndBenefits(strVal[3].Trim(), strVal[0].Trim(), strVal[2].Trim(), strVal[4].Trim());

            grGrossandBenefits.DataSource = dt;
            grGrossandBenefits.DataBind();
            clscashword cal = new clscashword();
            int nouOfRow = dt.Rows.Count;
            if (string.IsNullOrEmpty(dt.Rows[nouOfRow - 1]["PAYAMT"].ToString().Trim()) == false)
                lblTakaInWord.Text = "<b>In Words : </b>" + cal.getCashWord(Common.ReturnZeroForNull(dt.Rows[nouOfRow - 1]["PAYAMT"].ToString().Trim())); //nouOfRow.ToString();

            dt.Rows.Clear();            
            lblRemarks.Text = objPayRptMgr.GetPayrollRemarksForPayslip(strVal[3].Trim(), strVal[0].Trim(), strVal[2].Trim(), strVal[4].Trim());
            if (string.IsNullOrEmpty(lblRemarks.Text) == false)
                lblRemarks.Text = "* " + lblRemarks.Text;

            this.FormatGrossAndBenefitsGrid();
            this.FormatDeductionGrid();
        }
    }

    protected void FormatGrossAndBenefitsGrid()
    {
        foreach (GridViewRow gRow in grGrossandBenefits.Rows)
        {
            if (grGrossandBenefits.DataKeys[gRow.DataItemIndex].Values[0].ToString() == "A")
            {
                gRow.Font.Bold = true;
            }
            if (grGrossandBenefits.DataKeys[gRow.DataItemIndex].Values[0].ToString() == "B")
            {
                gRow.Font.Bold = true;
            }
            if (grGrossandBenefits.DataKeys[gRow.DataItemIndex].Values[0].ToString() == "Y")
            {
                gRow.Cells[1].Text="";
                gRow.Font.Bold = true;
            }
        }
    }

    protected void FormatDeductionGrid()
    {
        foreach (GridViewRow gRow in grDeduct.Rows)
        {
            if (grDeduct.DataKeys[gRow.DataItemIndex].Values[0].ToString() == "C")
            {
                gRow.Font.Bold = true;
            }
        }
    }
}
