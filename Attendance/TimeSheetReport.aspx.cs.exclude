using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Attendance_TimeSheetReport : System.Web.UI.Page
{
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            Common.FillDropDownList(objEmpMgr.SelectEmpNameWithID("A"), ddlEmployee, "EmpName", "EmpID", true, "All");
        }
    }

    protected void btnPriview_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        string strURL = "TimeSheetReportViewer.aspx?params=" + ddlEmployee.SelectedValue.ToString().Trim() + ","
                                                                     + ddlMonth.SelectedValue.Trim() + ","
                                                                     + ddlYear.SelectedItem.Text.Trim() + "," + chkIsRound.Checked.ToString() ;
        sb.Append("<script>");
        sb.Append("window.open('" + strURL + "', '', '');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit", sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
    }
    protected void radEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radEmp.SelectedValue == "A")
            Common.FillDropDownList(objEmpMgr.SelectEmpNameWithID("A"), ddlEmployee, "EmpName", "EmpID", true, "All");
        else
            Common.FillDropDownList(objEmpMgr.SelectEmpNameWithID("I"), ddlEmployee, "EmpName", "EmpID", true, "All");
    }
}