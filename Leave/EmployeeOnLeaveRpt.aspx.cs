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

public partial class Leave_EmployeeOnLeaveRpt : System.Web.UI.Page
{

    LeaveApplicationManager objLeaveMgr = new LeaveApplicationManager();

    protected void Page_Load(object sender, EventArgs e)
    {
         string strDivId = "";
         string strDeptId = "";
         string strParams = Request.QueryString["params"];
         string[] strVal = new string[5];
         if (string.IsNullOrEmpty(strParams) == false)
         {
             char[] splitter ={ ',' };
             strVal = Common.str_split(strParams, splitter);
         }

         lblFrom.Text = Common.DisplayDate(strVal[0].Trim());
         lblTo.Text = Common.DisplayDate(strVal[1].Trim());
         strDivId = strVal[2] == "99999" ? "0" : strVal[2];
         strDeptId = strVal[3] == "99999" ? "0" : strVal[3];
        
         if (strVal.Length == 5)
         {
             DataTable dtLeaveApp = objLeaveMgr.SelectEmployeeOnLeaveReport(0, strVal[4], "A", strVal[0], strVal[1], strDivId, strDeptId);
             grLeaveApp.DataSource = dtLeaveApp;
             grLeaveApp.DataBind();

             int i = 1;
             // FORMAT THE DATE FIELD
             foreach (GridViewRow gRow in grLeaveApp.Rows)
             {
                 gRow.Cells[0].Text = i.ToString();
                 if (Common.CheckNullString(gRow.Cells[6].Text) != "")
                 {
                     gRow.Cells[6].Text = Common.DisplayDate(gRow.Cells[6].Text).ToString();
                 }
                 if (Common.CheckNullString(gRow.Cells[7].Text) != "")
                 {
                     gRow.Cells[7].Text = Common.DisplayDate(gRow.Cells[7].Text).ToString();
                 }
                 if (Common.CheckNullString(gRow.Cells[8].Text) != "")
                 {
                     gRow.Cells[8].Text = Common.DisplayDate(gRow.Cells[8].Text).ToString();
                 }
                 if (Common.CheckNullString(gRow.Cells[9].Text) != "")
                 {
                     gRow.Cells[9].Text = Common.DisplayDate(gRow.Cells[9].Text).ToString();
                 }
                 i++;
             }
         }
    }
}
