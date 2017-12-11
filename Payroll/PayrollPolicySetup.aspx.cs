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


public partial class PayrollPolicySetup : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    Payroll_MasterMgr objSalaryHeadMgr = new Payroll_MasterMgr();
    Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    

    DataTable dtpaySlip =new DataTable();
    dsPayroll_Policy objDS = new dsPayroll_Policy();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();
            objOptMgr.GetPaySlipOptionsValue();

            Common.FillDropDownList(objSalaryHeadMgr.SelectSalaryHead(0, "N"), ddlBenefitHead, "HEADNAME", "SHEADID", true, "Select");
            Common.FillDropDownList(objMasMgr.SelectEmpType(0,"Y"), ddlEmpType, "TYPENAME", "EMPTYPEID", true, "Select");

            Common.FillDropDownListWithGross(objSalaryHeadMgr.SelectGrossSalHeadWithName(0), ddlPercentHead, "HEADNAME", "SHEADID", true);            

            Common.FillDropDownList(objSalaryHeadMgr.SelectFiscalYear(0), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);
            this.OpenRecord();
            hfMPCIsUpdate.Value = "N";
            this.FillMPCDayDropDownList(31);
            this.OpenMonthlyPayrollCycleData();
            this.OpenBenefitsPolicyRecord();
            hfBPID.Value = "";
        }      
    }   

    private void OpenRecord()
    {    
        dtpaySlip = objOptMgr.SelectpaySlipOption("");

        foreach (DataRow dtOp in dtpaySlip.Rows)
        {
            //if (dtOp["OptId"].ToString() == "OC01".ToString())
            //{
            //    ddlPFLoanDeduct.SelectedValue = dtOp["OptValue"].ToString();
            //}

            //if (dtOp["OptId"].ToString() == "OC02".ToString())
            //{
            //    ddltaxdeducation.SelectedValue = dtOp["OptValue"].ToString();
            //}

            if (dtOp["OptId"].ToString() == "OC03".ToString())
            {
                txtValidFrom.Text = String.IsNullOrEmpty(dtOp["PayrollValidFrom"].ToString()) == false ? Common.DisplayDate(dtOp["PayrollValidFrom"].ToString()) : "";
                txtValidTo.Text = String.IsNullOrEmpty(dtOp["PayrollValidTo"].ToString()) == false ? Common.DisplayDate(dtOp["PayrollValidTo"].ToString()) : "";
                if (String.IsNullOrEmpty(dtOp["OptValue"].ToString()) == false)
                    ddlFiscalYear.SelectedValue = dtOp["OptValue"].ToString().Trim();
            }

            //if (dtOp["OptId"].ToString() == "OC04".ToString())
            //{
            //    txtFYStartDate.Text = String.IsNullOrEmpty(dtOp["PayrollValidFrom"].ToString()) == false ? Common.DisplayDate(dtOp["PayrollValidFrom"].ToString()) : "";
            //    txtFYEndDate.Text = String.IsNullOrEmpty(dtOp["PayrollValidTo"].ToString()) == false ? Common.DisplayDate(dtOp["PayrollValidTo"].ToString()) : "";
               
            //}
        }

        dtpaySlip.Clear();
        dtpaySlip.Dispose();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            clsPaySlipOptions[] objOpt = new clsPaySlipOptions[1];
            objOpt[0] = new clsPaySlipOptions();
            objOpt[0].OptID = objOpt[0].PAYSLIP_VALIDITY;
            objOpt[0].OptName = "PAYSLIP_VALIDITY";
            objOpt[0].OptValue = ddlFiscalYear.SelectedValue.Trim();
            objOpt[0].ValidFrom = Common.ReturnDate(txtValidFrom.Text.Trim());
            objOpt[0].ValidTo = Common.ReturnDate(txtValidTo.Text.Trim());

            //objOpt[1] = new clsPaySlipOptions();
            //objOpt[1].OptID = objOpt[1].FISCAL_VALIDITY;
            //objOpt[1].OptName = "FISCAL_VALIDITY";
            //objOpt[1].OptValue = "0";
            //objOpt[1].ValidFrom = Common.ReturnDate(txtFYStartDate.Text.Trim());
            //objOpt[1].ValidTo = Common.ReturnDate(txtFYEndDate.Text.Trim());

            //objOpt._OptID[1] = objOpt.PAYSLIP_TAXDEDEDUCTION_SALARYHEAD;
            //objOpt._OptName[1] = "taxDeducation";
            //objOpt._OptValue[1] = ddltaxdeducation.SelectedValue.ToString();

            Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();

            objOptMgr.InsertpaySlipOption(objOpt, Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));

            lblMsg.Text = "Record Saved Successfully";
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    #region Benefits Policy
    protected void OpenBenefitsPolicyRecord()
    {
        Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();
        grBPList.DataSource = objOptMgr.SelectPayrollBenefitsPolicyData("0","0");
        grBPList.DataBind();

        foreach (GridViewRow gRow in grBPList.Rows)
        {
            if (grBPList.DataKeys[gRow.RowIndex][5].ToString() == "99")
            {
                gRow.Cells[6].Text = "Gross Salary";
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string strPID = "";
            Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();
            // objOptMgr.InsertpaySlipOption(objOpt, Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));
            if (hfBPID.Value == "")
                strPID = Common.getMaxId("PayrollBenefitsPolicy", "PID");
            else
                strPID = hfBPID.Value;
            objOptMgr.InsertPayrollBenefitsPolicyData(strPID, ddlBenefitHead.SelectedValue.Trim(), ddlEmpType.SelectedValue.Trim(),
                chkIsPercent.Checked == true ? "Y" : "N", ddlPercentHead.SelectedValue.Trim(), txtValue.Text.Trim(), hfBPID.Value == "" ? "N" : "Y",
               Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));
            this.OpenBenefitsPolicyRecord();
            hfBPID.Value = "";
            btnAdd.Text = "Save";
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    protected void grBPList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                hfBPID.Value = grBPList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim();
                ddlBenefitHead.SelectedValue = grBPList.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim();
                ddlEmpType.SelectedValue = grBPList.DataKeys[_gridView.SelectedIndex].Values[2].ToString().Trim();
                txtValue.Text = grBPList.DataKeys[_gridView.SelectedIndex].Values[3].ToString().Trim();
                chkIsPercent.Checked = grBPList.DataKeys[_gridView.SelectedIndex].Values[4].ToString().Trim() == "Y" ? true : false;
                ddlPercentHead.SelectedValue = grBPList.DataKeys[_gridView.SelectedIndex].Values[5].ToString().Trim();
                btnAdd.Text = "Update";
                break;
            case ("RowDeleting"):
                try
                {
                    objOptMgr.DeletePayrollBenefitsPolicyData(grBPList.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim());
                    hfBPID.Value = "";
                    btnAdd.Text = "Add";
                    this.OpenBenefitsPolicyRecord();
                    lblMsg.Text = "Recrod deleted successfully";
                }
                catch (Exception ex)
                {
                    lblMsg.Text = "Cannot delete the data. Reference data found";
                }

                break;
        }
    }
    #endregion

    # region Monthly Payroll Cycle
    protected void FillMPCDayDropDownList(int inDay)
    {
        int i=1;
        for (i = 1; i <= inDay; i++)
        {
            ddlPCSDay.Items.Add(i.ToString());
            ddlPCEDay.Items.Add(i.ToString());
            ddlACSDay.Items.Add(i.ToString());
            ddlACEDay.Items.Add(i.ToString());
        }
    }

    protected void OpenMonthlyPayrollCycleData()
    {
        DataTable dtMPC = new DataTable();
        dtMPC = objOptMgr.GetMonthlyPayrollCycleData();
        grMPCList.DataSource = dtMPC;
        grMPCList.DataBind();
        dtMPC.Rows.Clear();
        dtMPC.Dispose();
        btnMPCAdd.Text = "Save";
    }
    
    protected void btnMPCAdd_Click(object sender, EventArgs e)
    {
        string strMPCID = "";
        if (hfMPCIsUpdate.Value == "N")
            strMPCID = Common.getMaxId("MONTHLYPAYROLLCYCLE", "MPCID");
        else
            strMPCID = hfMPCID.Value.Trim();

        objOptMgr.InsertMonthlyPayrollCycleData(strMPCID, txtMPCTitle.Text.Trim(), ddlPCSDay.SelectedValue.Trim(),
            ddlPCEDay.SelectedValue.Trim(), ddlACSDay.SelectedValue.Trim(), ddlACEDay.SelectedValue.Trim(), hfMPCIsUpdate.Value,
            Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()));

        this.OpenMonthlyPayrollCycleData();
        hfMPCIsUpdate.Value = "N";
        hfMPCID.Value = "";
    }

    protected void grMPCList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                hfMPCID.Value = grMPCList.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                txtMPCTitle.Text = grMPCList.SelectedRow.Cells[2].Text;
                ddlPCSDay.SelectedValue = grMPCList.SelectedRow.Cells[3].Text;
                ddlPCEDay.SelectedValue = grMPCList.SelectedRow.Cells[4].Text;
                ddlACSDay.SelectedValue = grMPCList.SelectedRow.Cells[5].Text;
                ddlACEDay.SelectedValue = grMPCList.SelectedRow.Cells[6].Text;
                hfMPCIsUpdate.Value = "Y";
                btnMPCAdd.Text = "Update";
                break;
            case ("RowDeleting"):
                try
                {
                    objOptMgr.DeleteMonthlyPayrollCycleData(grMPCList.DataKeys[_gridView.SelectedIndex].Values[0].ToString());
                    hfMPCIsUpdate.Value = "N";
                    hfMPCID.Value = "";
                    this.OpenMonthlyPayrollCycleData();
                }
                catch (Exception ex)
                {
                    lblMsg.Text = "Cannot delete the data. Reference employee data found";
                }
                
                break;
        }
    }

    #endregion

    protected void ddlFiscalYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtFinYear = objSalaryHeadMgr.SelectFiscalYear(Convert.ToInt32(ddlFiscalYear.SelectedValue.Trim()));
        if (dtFinYear.Rows.Count > 0)
        {
            txtValidFrom.Text = Common.DisplayDate(dtFinYear.Rows[0]["StartDate"].ToString().Trim());
            txtValidTo.Text = Common.DisplayDate(dtFinYear.Rows[0]["EndDate"].ToString().Trim());
        }
    }
    protected void chkIsPercent_CheckedChanged(object sender, EventArgs e)
    {

    }
}
