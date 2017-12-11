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

public partial class Payroll_Loan_PFLedger : System.Web.UI.Page
{
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    Payroll_PFManager objPFMgr = new Payroll_PFManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0, "P"), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);
        }
    }


    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        string strEmpName = objPFMgr.GetEmpName(txtEmpID.Text);
        if (string.IsNullOrEmpty(strEmpName) == false)
        {
            lblEmpName.Text = strEmpName;
            DataTable dtPFLedger = objPFMgr.GetPFLedgerData(ddlFiscalYear.SelectedValue.Trim(), ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim(), txtEmpID.Text.Trim());
            if (dtPFLedger.Rows.Count > 0)
            {
                //Opening Balance
                txtOPOWN.Text = dtPFLedger.Rows[0]["OPPFOWN"].ToString().Trim();
                txtOPCARE.Text = dtPFLedger.Rows[0]["OPPFCARE"].ToString().Trim();
                txtOPInterest.Text = dtPFLedger.Rows[0]["OPPFINTREST"].ToString().Trim();
                txtOPTotal.Text = dtPFLedger.Rows[0]["OPTOTAL"].ToString().Trim();

                // Current Month Credit
                txtCMOWN.Text = dtPFLedger.Rows[0]["CMPFOWN"].ToString().Trim();
                txtCMCARE.Text = dtPFLedger.Rows[0]["CMPFCARE"].ToString().Trim();
                txtCMInterest.Text = dtPFLedger.Rows[0]["CMPFINTREST"].ToString().Trim();
                txtCMTotal.Text = dtPFLedger.Rows[0]["CMTOTAL"].ToString().Trim();

                // Current Payment
                txtCPDate.Text = dtPFLedger.Rows[0]["CPDATE"].ToString().Trim();
                txtCPAmount.Text = dtPFLedger.Rows[0]["CPAMOUNT"].ToString().Trim();

                // Cummulative Balance
                txtCUOWN.Text = dtPFLedger.Rows[0]["CUPFOWN"].ToString().Trim();
                txtCUCARE.Text = dtPFLedger.Rows[0]["CUPFCARE"].ToString().Trim();
                txtCUInterest.Text = dtPFLedger.Rows[0]["CUPFINTREST"].ToString().Trim();
                txtCUTotal.Text = dtPFLedger.Rows[0]["CUTOTAL"].ToString().Trim();

                // Total Payment
                txtTotalPay.Text = dtPFLedger.Rows[0]["TOTALPAY"].ToString().Trim();

                // Net Balance
                txtNetBalance.Text = dtPFLedger.Rows[0]["NETBALANCE"].ToString().Trim();

                hfIsUpdate.Value = "Y";
                hfLedgerID.Value = dtPFLedger.Rows[0]["LEDGERID"].ToString().Trim();
                btnSave.Text = "Update";
            }
            else
            {
                //Opening Balance
                txtOPOWN.Text = "0";
                txtOPCARE.Text = "0";
                txtOPInterest.Text = "0";
                txtOPTotal.Text = "0";

                // Current Month Credit
                txtCMOWN.Text = "0";
                txtCMCARE.Text = "0";
                txtCMInterest.Text = "0";
                txtCMTotal.Text = "0";

                // Current Payment
                txtCPDate.Text = "0";
                txtCPAmount.Text = "0";

                // Cummulative Balance
                txtCUOWN.Text = "0";
                txtCUCARE.Text = "0";
                txtCUInterest.Text = "0";
                txtCUTotal.Text = "0";

                // Total Payment
                txtTotalPay.Text = "0";

                // Net Balance
                txtNetBalance.Text = "0";

                btnSave.Text = "Save";
                hfIsUpdate.Value = "N";
                hfLedgerID.Value = "";
            }
        }
        else
        {
            lblEmpName.Text = "";
            lblMsg.Text = "Invalid Employee ID";
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strLedgerID="";
        if (lblEmpName.Text == "")
        {
            lblMsg.Text = "No Record to save.";
            return;
        }
        if (hfIsUpdate.Value == "Y")
            strLedgerID = hfLedgerID.Value.Trim();
        else
            strLedgerID = Common.getMaxId("PFLEDGER", "LEDGERID");

        objPFMgr.InsertPFLedger(strLedgerID,
                                txtEmpID.Text.Trim(),
                                ddlMonth.SelectedValue.Trim(),
                                ddlYear.SelectedValue.Trim(),
                                ddlFiscalYear.SelectedValue.Trim(),
                                txtOPOWN.Text.Trim(),
                                txtOPCARE.Text.Trim(),
                                txtOPInterest.Text.Trim(),
                                txtOPTotal.Text.Trim(),
                                txtCMOWN.Text.Trim(),
                                txtCMCARE.Text.Trim(),
                                txtCMInterest.Text.Trim(),
                                txtCMTotal.Text.Trim(),
                                txtCPDate.Text.Trim(),
                                txtCPAmount.Text.Trim(),
                                txtCUOWN.Text.Trim(),
                                txtCUCARE.Text.Trim(),
                                txtCUInterest.Text.Trim(),
                                txtCUTotal.Text.Trim(),
                                txtTotalPay.Text.Trim(),
                                txtNetBalance.Text.Trim(),
                                Session["USERID"].ToString().Trim(),
                                Common.SetDateTime(DateTime.Now.ToString()),
                                hfIsUpdate.Value);
        lblMsg.Text = "Record Saved Successfully";
        hfIsUpdate.Value = "N";
        hfLedgerID.Value = "";
        btnSave.Text = "Save";

    }
}
