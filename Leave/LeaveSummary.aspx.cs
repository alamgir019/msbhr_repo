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

public partial class Leave_LeaveSummary : System.Web.UI.Page
{
    MasterTablesManager objMasMgr = new MasterTablesManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList_Nil(objMasMgr.SelectDivisionddl(0), ddlDivision);
            Common.FillDropDownList_Nil(objMasMgr.SelectDepartmentddl(0), ddlDept);
            Common.FillCheckBoxList(objMasMgr.SelectLeaveType(0), chkLeaveTypeList, "LTypeTitle", "LTypeId");
        }
    }

    protected string GetSelectedLeaveType()
    {
        string strSelValue="";
        foreach (ListItem lst in chkLeaveTypeList.Items)
        {
            if (lst.Selected == true)
            {
                if (strSelValue == "")
                    strSelValue = lst.Value.ToString().Trim();
                else
                    strSelValue = strSelValue + "/" + lst.Value.ToString().Trim();
            }
        }
        return strSelValue;
    }
    protected void btnPriview_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        string strURL = "LeaveSummaryRpt.aspx?params=" + "," + ddlDivision.SelectedValue.ToString() + "," + ddlDept.SelectedValue.ToString() + "," + txtEmpId.Text.Trim() + "," + this.GetSelectedLeaveType()+ "," + rdbEmpStatus.SelectedValue.ToString().Trim() ;
        sb.Append("<script>");
        //sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
        sb.Append("window.open('" + strURL + "', '', '');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                 sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
    }
}
