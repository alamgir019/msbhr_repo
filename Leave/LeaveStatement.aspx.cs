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

public partial class Leave_LeaveStatement : System.Web.UI.Page
{
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList_Nil(objMasMgr.SelectLocationCategory(0), ddlOffice);
        }
    }
    protected void GetDivisionWiseEmp(string strDivID)
    {
        Common.FillDropDownList(objEmpMgr.SelectDivisionalEmp(strDivID), ddlEmployee, "EMPNAME", "EMPID", true);

    }
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.GetDivisionWiseEmp(ddlOffice.SelectedValue.ToString());
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        string strURL = "LeaveStatementRpt.aspx?params=" +  ddlEmployee.SelectedValue.ToString();
        sb.Append("<script>");
        //sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
        sb.Append("window.open('" + strURL + "', '', '');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                 sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
    }
}
