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
using System.Text;

public partial class Attendance_SummaryAttendance : System.Web.UI.Page
{
    MasterTablesManager objMasMgr = new MasterTablesManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Division is Changed to Location
            Common.FillDropDownList_Nil(objMasMgr.SelectLocation(0), ddlDivision);
            // Office Filled up
            Common.FillDropDownList_Nil(objMasMgr.SelectDivisionddl(0), ddlOffice);
            Common.FillDropDownList_Nil(objMasMgr.SelectDepartmentddl(0), ddlDept);
            //Common.FillMonthList(ddlMonth);
            //Common.FillYearListBackward(3, ddlYear);
            //ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            //ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);

        }
    }
    protected void btnPriview_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        string strFromDate = "";
        string strToDate = "";
        lblMsg.Text = "";
        //if ((ddlMonth.SelectedIndex != -1) && (ddlYear.SelectedIndex != -1))
        //{
        //    strFromDate = ddlYear.SelectedValue.ToString()+ "/" + ddlMonth.SelectedValue.ToString() + "/" + "01" ;
        //    strToDate = ddlYear.SelectedValue.ToString() + "/" + ddlMonth.SelectedValue.ToString() + "/" + Convert.ToString(Common.GetMonthDay(Convert.ToDateTime(strFromDate)));
        //}
        strFromDate = Common.ReturnDate(txtFrom.Text.Trim());
        strToDate = Common.ReturnDate(txtTo.Text.Trim());

        TimeSpan ts = Convert.ToDateTime(strToDate.Trim()) - Convert.ToDateTime(strFromDate.Trim());
        if (ts.Days > 30)
        {
            lblMsg.Text = "Maximum date range must not exceed 31 days..";
            return;
        }

        string strURL = "SummaryAttendanceRpt.aspx?params=" + strFromDate + ", " + strToDate + "," + ddlDivision.SelectedValue.ToString() + "," + ddlDept.SelectedValue.ToString() + "," + txtEmpId.Text.Trim()+ "," + rdbtnEmpStatus.SelectedValue.ToString()+","+ddlOffice.SelectedValue.ToString().Trim();
        sb.Append("<script>");        
        sb.Append("window.open('" + strURL + "', '', '');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                 sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
    }

}