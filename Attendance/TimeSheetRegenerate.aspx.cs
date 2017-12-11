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

public partial class Attendance_TimeSheetRegenerate : System.Web.UI.Page
{
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();        
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    TimeSheetManager objTSMgr = new TimeSheetManager();
    SOFManager objSOFMgr = new SOFManager();

    DataTable dtSalaryCharge = new DataTable();

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

        lblMsg.Text = "Time Sheet has been regenerated for " + ddlMonth.SelectedItem.ToString() + ", " + ddlYear.SelectedItem.ToString();
    }
    protected void ddlEmpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Common.FillDropDownList(objMasMgr.SelectEmpTypeWsEmp(ddlEmpType.SelectedValue.ToString()), ddlEmployee, "FullName", "EmpID", true, "All");
    }
    protected void radEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radEmp.SelectedValue == "A")
            Common.FillDropDownList(objEmpMgr.SelectEmpNameWithID("A"), ddlEmployee, "EmpName", "EmpID", true, "All");
        else
            Common.FillDropDownList(objEmpMgr.SelectEmpNameWithID("I"), ddlEmployee, "EmpName", "EmpID", true, "All");
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
}
