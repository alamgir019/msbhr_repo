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

public partial class Appraisal_AppraisalIncUpdate : System.Web.UI.Page
{
    Payroll_MasterMgr objPayMgr = new Payroll_MasterMgr();
    Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();

    DataTable dtApprIncUpdate = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            Common.FillDropDownList(objPayMgr.SelectFiscalYear(0, "FA"), ddlFiscalYr, "FISCALYRTITLE", "FISCALYRID", false);

        }
    }

    

    private void OpenRecord()
    {
        string strFromScrore;
        string strToScrore;


        if (string.IsNullOrEmpty(txtScoreFrom.Text.Trim()) == false)
            strFromScrore = txtScoreFrom.Text.Trim();
        else
            strFromScrore = Convert.ToString(0);

        if (string.IsNullOrEmpty(txtScoreTo.Text.Trim()) == false)
            strToScrore = txtScoreTo.Text.Trim();
        else
            strToScrore = Convert.ToString(0);


         dtApprIncUpdate = objPayMgr.SelectAppraisalIncSalaryUpdate(ddlFiscalYr.SelectedValue.ToString(), strFromScrore, strToScrore);

        if (dtApprIncUpdate.Rows.Count > 0)
        {
            grAppraisalIncUpdate.DataSource = dtApprIncUpdate;
            grAppraisalIncUpdate.DataBind();
            NewBasic();
        }
        else
        {
            grAppraisalIncUpdate.DataSource = null;
            grAppraisalIncUpdate.DataBind();

        }
    }

    private void NewBasic()
    {
        DataTable dtBenefitsPolicy = objOptMgr.SelectPayrollBenefitsPolicyData("0","0");
        decimal dclBasicAmt = Convert.ToDecimal(txtPercntOfBasic.Text);
        decimal dclAllowanceAmt = 0;
        decimal dclPFAmt = 0;
        foreach (GridViewRow gRow in grAppraisalIncUpdate.Rows)
        {
            if (Common.CheckNullString(gRow.Cells[2].Text) != "")
            {
                dclBasicAmt = Convert.ToDecimal(gRow.Cells[2].Text) + (Convert.ToDecimal(gRow.Cells[2].Text) * Convert.ToDecimal(txtPercntOfBasic.Text) / 100);
                gRow.Cells[4].Text = dclBasicAmt.ToString();
            }
            //New Allowance         
            dclAllowanceAmt = this.CalculateHeadAmount(dclBasicAmt, dtBenefitsPolicy, "1", "2");
            gRow.Cells[5].Text = dclAllowanceAmt.ToString();

            //PF       
            dclPFAmt = this.CalculateHeadAmount(dclBasicAmt, dtBenefitsPolicy, "1", "13");
            gRow.Cells[6].Text = dclPFAmt.ToString();
        }
    }


    protected Decimal CalculateHeadAmount(Decimal dclBasic, DataTable dtBfPolicy, string strEmpType, string strSHeadID)
    {
        if (dtBfPolicy.Rows.Count > 0)
        {
            DataRow[] foundRows = dtBfPolicy.Select("EMPTYPEID =" + strEmpType + " AND SHEADID=" + strSHeadID);
            Decimal dclPercent = 0;
            Decimal dclAmount = 0;
            if (foundRows.Length > 0)
            {
                if (foundRows[0]["ISPERCENT"].ToString().Trim() == "Y")
                {
                    dclPercent = Common.RoundDecimal(foundRows[0]["VALUE"].ToString().Trim(), 2);
                    dclAmount = dclBasic * dclPercent / 100;
                }
                else
                {
                    dclAmount = Common.RoundDecimal(foundRows[0]["VALUE"].ToString().Trim(), 0);
                }
            }
            return Math.Round(dclAmount, 0);
        }
        else
            return 0;
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        OpenRecord();
    }

    protected void btnUpdateSalaryPak_Click(object sender, EventArgs e)
    {      
        objPayMgr.InsertAppraisalSalaryIncrement(grAppraisalIncUpdate, ddlFiscalYr.SelectedValue.ToString(), txtPercntOfBasic.Text, Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "N");
        if (hfIsUpdate.Value == "N")
            lblMsg.Text = "Record Saved Successfully";
    }
}

