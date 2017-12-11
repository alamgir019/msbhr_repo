using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Attendance_TimeSheetGenerate : System.Web.UI.Page
{
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    ReportManager objReport = new ReportManager();
    SOFManager objSOFMgr = new SOFManager();
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    TimeSheetManager objTSMgr = new TimeSheetManager();

    DataTable dtSalaryCharge = new DataTable();
    DataTable dtSalarySourceHeadWs = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0, "F"), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            radEmp.SelectedValue = "A";            
            Common.FillDropDownList(objMasMgr.SelectEmpType(0,"Y"), ddlEmpType, "TypeName", "EmpTypeId", false);
            Common.FillDropDownList(objMasMgr.SelectEmpTypeWsEmp(ddlEmpType.SelectedValue.ToString()), ddlEmployee, "FullName", "EmpID", true, "All");
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        objTSMgr.GenerateTimeSheet(ddlEmployee.SelectedValue.Trim(), ddlMonth.SelectedValue.Trim(),
            ddlYear.SelectedValue.Trim(), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));

        lblMsg.Text = "Time Sheet has been Prepared for " + ddlMonth.SelectedItem.ToString() + ", " + ddlYear.SelectedItem.ToString();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
        {
            this.SaveData();
            btnGenerate.Enabled = true;
        }
    }

    protected bool ValidateAndSave()
    {
        try
        {
            if (grList.Rows.Count == 0)
            {
                lblMsg.Text = "Please select employee for SOF Settlement.";
                btnView.Focus();
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    private void SaveData()
    {
        try
        {
            string strYear = ddlYear.SelectedItem.Value.ToString().Trim();
            string strMonth = ddlMonth.SelectedItem.Value.ToString().Trim();
            string strPreMonth = "";

            DateTime dtCurr = DateTime.Now;
            string strCurrMonth = dtCurr.Year.ToString();

            if (strCurrMonth != strMonth)
                strPreMonth = "Y";
            else
                strPreMonth = "N";

            objSOFMgr.InsertEmpSalarySource(grList, Convert.ToInt32(ddlFiscalYear.SelectedValue.ToString()),
                Convert.ToInt32(strMonth), Convert.ToInt32(strYear), ddlEmployee.SelectedValue.ToString().Trim(),
                Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), strPreMonth, ddlEmpType.SelectedValue.ToString());

            lblMsg.Text = "SOF has been Settled Successfully for " + ddlMonth.SelectedItem.ToString() + ", " + ddlYear.SelectedItem.ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
            throw (ex);
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {     
        this.ClearControls();
        lblMsg.Text = "";
        btnGenerate.Enabled = false;
    }

    private void ClearControls()
    {
        grList.DataSource = null;
        grList.DataBind();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        string strYear = ddlYear.SelectedItem.Value.ToString().Trim();
        string strMonth = ddlMonth.SelectedItem.Value.ToString().Trim();

        dtSalaryCharge = objSOFMgr.SelectSOFSettlement(strYear, strMonth, ddlEmpType.SelectedValue.ToString().Trim(), ddlEmployee.SelectedValue.ToString().Trim());
        grList.DataSource = dtSalaryCharge;
        grList.DataBind();
    }
    protected void radEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radEmp.SelectedValue == "A")
            Common.FillDropDownList(objEmpMgr.SelectEmpNameWithID("A"), ddlEmployee, "EmpName", "EmpID", true, "All");
        else
            Common.FillDropDownList(objEmpMgr.SelectEmpNameWithID("I"), ddlEmployee, "EmpName", "EmpID", true, "All");
    }
    protected void ddlEmpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Common.FillDropDownList(objMasMgr.SelectEmpTypeWsEmp(ddlEmpType.SelectedValue.ToString()), ddlEmployee, "FullName", "EmpID", true, "All");
    }
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
