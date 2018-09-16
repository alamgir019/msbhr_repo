using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Payroll_Payroll_FinalPaymentReview : System.Web.UI.Page
{
    DataTable dtEmpInfo = new DataTable();
    DataTable dtFinalPay = new DataTable();

    MasterTablesManager objMasMgr = new MasterTablesManager();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
    LeaveManager objLvMgr = new LeaveManager();
    Payroll_MasterMgr objPayMgr = new Payroll_MasterMgr();
    Payroll_PFManager objPFMgr = new Payroll_PFManager();
    Payroll_FinalPaymentMgr objFinalPayMgr = new Payroll_FinalPaymentMgr();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            Common.FillDropDownList_All(objMasMgr.SelectClinic("Y"), ddlBank);
            //Common.FillDropDownList(objMastMg.SelectLocation(0), ddlGenerateValue, "LocationName", "LocationID", false);
            //Common.FillDropDownList(objPayrollMgr.SelectBankAndBranchList(), ddlBank, "BANKBRANCH", "RoutingNo", true, "Nil");
            //Common.FillDropDownList(objMastMg.SelectEmpGroup(0), ddlGroup, "GrpName", "EmpGrpID", false);            
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        string strGenerateValue = "";
     
        switch (ddlGeneratefor.SelectedValue.ToString())
        {
            case "O":
                strGenerateValue = ddlBank.SelectedValue.ToString();                
                break;
            case "B":
                strGenerateValue = ddlBank.SelectedValue.ToString();              
                break;
        }

        //Prepared Data
        dtFinalPay = objFinalPayMgr.SelectEmpFinalPayment(ddlGeneratefor.SelectedValue.ToString(), ddlBank.SelectedValue.Trim(),
            ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(),"P");
        
        grList.DataSource = dtFinalPay;
        grList.DataBind();
        if (grList.Rows.Count > 0)
        {
            btnSave.Enabled = false;
            foreach (GridViewRow gRow in grList.Rows)
            {
                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[14].Text)) == false)
                    gRow.Cells[14].Text = Common.DisplayDate(gRow.Cells[14].Text);

                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[15].Text)) == false)
                    gRow.Cells[15].Text = Common.DisplayDate(gRow.Cells[15].Text);

                string diff = Common.CalculateYearMonthDay(gRow.Cells[15].Text, 1);
                if (Convert.ToInt32(diff) < 6)
                {
                    btnSave.Enabled = true;                   
                }
            }
        }
        else
        {
            btnSave.Enabled = true;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strGenerateValue = "";
        if (grList.Rows.Count <= 0)
        {
            lblMsg.Text = "No records to review";
            return;
        }
        switch (ddlGeneratefor.SelectedValue.ToString())
        {
            case "O":
                strGenerateValue = ddlBank.SelectedValue.ToString();
                break;
            case "B":
                strGenerateValue = ddlBank.SelectedValue.ToString();
                break;
        }

        //objFinalPayMgr.UpdatePayslipMst(dtEmpPayroll, ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.Trim(), "R", Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()));
        //lblMsg.Text = "Records has been Reviewed Successfully";
        //this.GeneratePayrollReport();
    }
}