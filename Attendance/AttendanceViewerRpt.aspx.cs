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


public partial class Attendance_AttendanceViewerRpt : System.Web.UI.Page
{
    


    AdjustAttendanceTableManager objAdjMgr = new AdjustAttendanceTableManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string[] strVal=new string[3];
            string strParams = Request.QueryString["params"];
            if (string.IsNullOrEmpty(strParams) == false)
            {
                char[] splitter ={ ',' };
                strVal = Common.str_split(strParams, splitter);
            }

            lblFrom.Text = Common.DisplayDate(strVal[1].Trim());
            lblTo.Text = Common.DisplayDate(strVal[2].Trim());

            if (strVal.Length == 3)
            {
                DataTable dtAttnAdj = objAdjMgr.GetData(strVal[1], strVal[2], "4", strVal[0],
                   "0", "0", "0");
                grAttnAdj.DataSource = dtAttnAdj;
                grAttnAdj.DataBind();
                foreach (GridViewRow gRow in grAttnAdj.Rows)
                {
                    gRow.Cells[4].Text = Common.DisplayDate(gRow.Cells[4].Text);
                    if (Common.CheckNullString(gRow.Cells[5].Text) != "")
                    {
                        gRow.Cells[5].Text = Common.DisplayTime(gRow.Cells[5].Text).ToString();
                    }
                    if (Common.CheckNullString(gRow.Cells[6].Text) != "")
                    {
                        gRow.Cells[6].Text = Common.DisplayTime(gRow.Cells[6].Text).ToString();
                    }
                }
            }
        }
    }
}
