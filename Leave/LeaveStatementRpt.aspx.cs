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

public partial class Leave_LeaveStatementRpt : System.Web.UI.Page
{
    LeaveApplicationManager objLvMgr = new LeaveApplicationManager();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
    string strEmpSex = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string strParams = Request.QueryString["params"];
        this.GetData(strParams);
        lblPrintDate.Text = Common.DisplayDate(DateTime.Today.ToShortDateString());
    }

    protected void GetData(string strEmpID)
    {
        DataTable dtEmp = objEmpInfoMgr.SelectEmpInfoForLeave(strEmpID);
        if (dtEmp.Rows.Count > 0)
        {
            lblEmpID.Text = strEmpID.Trim();
            lblEmpName.Text = dtEmp.Rows[0]["FullName"].ToString();
            lblPosition.Text = dtEmp.Rows[0]["DesigName"].ToString();
            lblOffice.Text = dtEmp.Rows[0]["DivisionName"].ToString();
            lblJoinDate.Text =Common.DisplayDate(dtEmp.Rows[0]["JoiningDate"].ToString());
            strEmpSex = dtEmp.Rows[0]["Gender"].ToString();
            this.GetLeaveBalance(strEmpID, strEmpSex);
        }
    }

    protected void GetLeaveBalance(string strEmpID, string strSex)
    {
        DataTable dtLeaveBalance = objLvMgr.SelectEmpLeaveProfileEXCPL(strEmpID, "0", strSex);
        if (dtLeaveBalance.Rows.Count > 0)
        {
            grLeaveBalance.DataSource = dtLeaveBalance;
            grLeaveBalance.DataBind();
            this.FormatLeaveStatusGridNumber();

            this.GetLeaveHistory(strEmpID,
                dtLeaveBalance.Rows[0]["LeaveStartPeriod"].ToString().Trim(),
                dtLeaveBalance.Rows[0]["LeaveEndPeriod"].ToString().Trim());

        }        
    }
    protected void GetLeaveHistory(string strEmpID, string strLvStartPeriod, string strLvEndPeriod)
    {
        lblLvStartPeriod.Text = Common.DisplayDate(strLvStartPeriod);
        lblLvEndPeriod.Text = Common.DisplayDate(strLvEndPeriod);

        strLvStartPeriod = Common.SetDate(strLvStartPeriod);
        strLvEndPeriod = Common.SetDate(strLvEndPeriod);
       

        DataTable dtLeaveHistory = objLvMgr.SelectEmpLeaveDetails(strEmpID, strLvStartPeriod, strLvEndPeriod);

        if (dtLeaveHistory.Rows.Count > 0)
        {
            grLeaveHistory.DataSource = null;
            grLeaveHistory.DataBind();

            grLeaveHistory.DataSource = dtLeaveHistory;
            grLeaveHistory.DataBind();
            this.FormatLeaveDetailsGridNumber();
        }
    }

    protected void FormatLeaveStatusGridNumber()
    {
        int i = 0;
        foreach (GridViewRow gRow in grLeaveBalance.Rows)
        {
            gRow.Cells[1].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[1].Text)), 1));
            gRow.Cells[2].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[2].Text)), 1));
            gRow.Cells[3].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[3].Text)), 1));
            gRow.Cells[4].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[1].Text)), 1) + (Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[2].Text)), 1)) - Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[3].Text)));

            if (Convert.ToDecimal(gRow.Cells[4].Text) < 0)
            {
                gRow.Cells[4].Text = "0";
            }
            i++;
        }
    }

    protected void FormatLeaveDetailsGridNumber()
    {
        int i = 1;
        foreach (GridViewRow gRow in grLeaveHistory.Rows)
        {
            gRow.Cells[0].Text = i.ToString();
            gRow.Cells[2].Text = Common.DisplayDate(gRow.Cells[2].Text);
            gRow.Cells[3].Text = Common.DisplayDate(gRow.Cells[3].Text);
            gRow.Cells[4].Text = Common.DisplayDate(gRow.Cells[4].Text);
            if (gRow.Cells[6].Text.Trim() == "A")
                gRow.Cells[6].Text = "Availed";
            else if (gRow.Cells[6].Text.Trim() == "R")
                gRow.Cells[6].Text = "Requsted";
            if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[8].Text)) == false)
                gRow.Cells[8].Text = Common.DisplayDate(gRow.Cells[8].Text);
            if (Common.CheckNullString(gRow.Cells[5].Text)!="")
                gRow.Cells[5].Text = Convert.ToString(Math.Round(Convert.ToDecimal(gRow.Cells[5].Text),1));
            i++;
        }
    }
}
