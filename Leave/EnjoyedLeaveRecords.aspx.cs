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

public partial class Leave_EnjoyedLeaveRecords : System.Web.UI.Page
{
    MasterTablesManager objMasMgr = new MasterTablesManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList_Nil(objMasMgr.SelectDivisionddl(0), ddlDivision);
            Common.FillDropDownList_Nil(objMasMgr.SelectDepartmentddl(0), ddlDept);
            Common.FillDropDownList(objMasMgr.SelectLeaveType(0), ddlLeaveType, "LTypeTitle", "LTypeId", false);
            Common.FillDropDownList_Nil(objMasMgr.SelectEmpType(0,"Y"), ddlEmpType);
            if (Session["ISADMIN"].ToString() == "N")
            {
                txtEmpId.Text   = Session["EMPID"].ToString().ToUpper().Trim();
                txtEmpId.Enabled = false;
                ddlDivision.Enabled = false;
                ddlDept.Enabled = false;
                ddlEmpType.Enabled = false;
            }
            else if (Session["ISADMIN"].ToString() == "Y")
            {
                if (Session["USERID"].ToString().ToUpper() != "ADMIN")
                    txtEmpId.Text = Session["EMPID"].ToString().ToUpper();
            }
        }
    }

    protected void btnPriview_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        string strURL = "EnjoyedLeaveRecordsRpt.aspx?params=" + "," + ddlDivision.SelectedValue.ToString() + "," 
            + ddlDept.SelectedValue.ToString() + "," 
            + txtEmpId.Text.Trim() + ","
            + rdbEmpStatus.SelectedValue.ToString().Trim()+ "," 
            + ddlLeaveType.SelectedValue.ToString().Trim() + "," 
            + ddlLeaveType.SelectedItem.Text.Trim()+ "," 
            + Common.ReturnDate(txtFrom.Text.Trim()) + "," 
            + Common.ReturnDate(txtTo.Text.Trim()) + ","
            + ddlEmpType.SelectedValue.Trim() + ","
            + ddlEmpType.SelectedItem.Text.Trim() + ","
            + ddlDivision.SelectedItem.Text.Trim() + ","
            + ddlDept.SelectedItem.Text.Trim();
        sb.Append("<script>");
        //sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
        sb.Append("window.open('" + strURL + "', '', '');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                 sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
    }
}
