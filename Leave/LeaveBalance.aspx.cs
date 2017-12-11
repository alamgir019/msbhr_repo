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

public partial class Leave_LeaveBalance : System.Web.UI.Page
{
    MasterTablesManager objMasMgr = new MasterTablesManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList_Nil(objMasMgr.SelectDivisionddl(0), ddlDivision);
            Common.FillDropDownList_Nil(objMasMgr.SelectDepartmentddl(0), ddlDept);
        }
    }
    protected void btnPriview_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        string strURL = "LeaveBalanceRpt.aspx?params=" + "," + ddlDivision.SelectedValue.ToString() + "," + ddlDept.SelectedValue.ToString() + "," + txtEmpId.Text.Trim();
        sb.Append("<script>");
        //sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
        sb.Append("window.open('" + strURL + "', '', '');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                 sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
    }
    //MasterTablesManager objMasMgr = new MasterTablesManager();
    //LeaveManager objLeaveMgr = new LeaveManager();

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    if (!IsPostBack)
    //    {
    //        Common.FillDropDownList(objLeaveMgr.SelectLeaveType(0), ddlPLeaveType, "LTypeTitle", "LTypeID", false);
    //        Common.FillDropDownList(objLeaveMgr.SelectLeaveType(0), ddlNLeaveType, "LTypeTitle", "LTypeID", false);
    //    }
    //}
    //protected void btnPriview_Click(object sender, EventArgs e)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    string strURL = "LeaveBalanceRpt.aspx?params=" + "," + ddlDivision.SelectedValue.ToString() + "," + ddlDept.SelectedValue.ToString() + "," + txtEmpId.Text.Trim();
    //    sb.Append("<script>");
    //    //sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
    //    sb.Append("window.open('" + strURL + "', '', '');");
    //    sb.Append("</script>");
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
    //                             sb.ToString(), false);
    //    ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
    //}
}
