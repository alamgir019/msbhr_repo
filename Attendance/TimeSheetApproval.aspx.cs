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

public partial class Attendance_TimeSheetApproval : System.Web.UI.Page
{
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    TimeSheetManager timeSheetMgr = new TimeSheetManager();
    Payroll_MasterMgr objPayMstMgr = new Payroll_MasterMgr();
    GADRecoveryManager objGADMgr = new GADRecoveryManager();

    DataTable dtTimeSheetP = new DataTable();
    DataTable dtTimeSheetA = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.SelectedValue = DateTime.Today.Month.ToString();
            ddlYear.SelectedValue = DateTime.Today.Year.ToString();
            Common.FillDropDownList(objPayMstMgr.SelectFiscalYear(0), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);
            ddlFiscalYear.SelectedValue = Session["FISCALYRID"].ToString().Trim();

            
            this.TabContainer1.ActiveTabIndex = 0;
        }
    }

    private void OpenRecord()
    {
        grTimeSheetApp.DataSource = null;
        grTimeSheetApp.DataBind();
        dtTimeSheetP.Rows.Clear();
        grTimeSheetApp.Dispose();

        //grTimeSheetApproved.DataSource = null;
        //grTimeSheetApproved.DataBind();
        //dtTimeSheetA.Rows.Clear();
        //grTimeSheetApproved.Dispose();

       if (Session["USERID"].ToString().Trim().ToUpper() != "ADMIN")
        {
            dtTimeSheetP = timeSheetMgr.GET_TimeSheet_For_App(ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), 
                ddlFiscalYear.SelectedValue.ToString(), "P", Session["EMPID"].ToString().Trim());


            dtTimeSheetA = timeSheetMgr.GET_TimeSheet_For_App(ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(),
                ddlFiscalYear.SelectedValue.ToString(), "A", Session["EMPID"].ToString().Trim());
        }
        else
        {
            dtTimeSheetP = timeSheetMgr.GET_TimeSheet_For_App(ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), 
                ddlFiscalYear.SelectedValue.ToString(), "P", "");


            dtTimeSheetA = timeSheetMgr.GET_TimeSheet_For_App(ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(),
                 ddlFiscalYear.SelectedValue.ToString(), "A", "");
        }

        grTimeSheetApp.DataSource = dtTimeSheetP;
        grTimeSheetApp.DataBind();


        grTimeSheetApproved.DataSource = dtTimeSheetA;
        grTimeSheetApproved.DataBind();

        int slP = 0;
        if (grTimeSheetApp.Rows.Count > 0)
        {
            foreach (GridViewRow gRow in grTimeSheetApp.Rows)
            {
                slP = slP + 1;
                gRow.Cells[0].Text = slP.ToString();

                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[4].Text)) == false)
                    gRow.Cells[4].Text = Common.DisplayDate(gRow.Cells[4].Text);

                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[5].Text)) == false)
                    gRow.Cells[5].Text = Common.DisplayDate(gRow.Cells[5].Text);

                gRow.Cells[6].Text = Common.RoundDecimal5T1(gRow.Cells[6].Text, 1).ToString();
            }
            slP = 0;
        }
        

        int slA = 0;
        if (grTimeSheetApproved.Rows.Count > 0)
        {
            foreach (GridViewRow gRow in grTimeSheetApproved.Rows)
            {
                slA = slA + 1;
                gRow.Cells[0].Text = slA.ToString();

                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[4].Text)) == false)
                    gRow.Cells[4].Text = Common.DisplayDate(gRow.Cells[4].Text);

                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[5].Text)) == false)
                    gRow.Cells[5].Text = Common.DisplayDate(gRow.Cells[5].Text);
            }
        }

        if (grTimeSheetApp != null)
            grTimeSheetApp.Dispose();

        if (grTimeSheetApproved != null)
            grTimeSheetApproved.Dispose();
    }


    protected void grTimeSheetApp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        //string strPreYrLv = "";
        switch (_commandName)
        {
            case ("ApproveClick"):
                try
                {
                    timeSheetMgr.UPDATE_TIMESHEET_STATUS(grTimeSheetApp.DataKeys[_gridView.SelectedIndex].Values[0].ToString(),
                        ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), ddlFiscalYear.SelectedValue.ToString(), "A",
                        Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));


                    timeSheetMgr.UPDATE_TIMESHEETLEAVE_STATUS(grTimeSheetApp.DataKeys[_gridView.SelectedIndex].Values[0].ToString(),
                        ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), ddlFiscalYear.SelectedValue.ToString(), "A",
                        Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));

                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                this.OpenRecord();

                break;

            case ("ViewClick"):
                StringBuilder sb = new StringBuilder();
                string strFromDate = "";
                string strToDate = "";
                lblMsg.Text = "";

                string strURL = "TimeSheetReportPage.aspx?params=" + grTimeSheetApp.DataKeys[_gridView.SelectedIndex].Values[0].ToString() + "," + ddlMonth.SelectedValue.ToString() + "," + ddlYear.SelectedValue.ToString() + "," + ddlFiscalYear.SelectedValue.ToString(); //lblEmpName.Text.Trim() + "," + lblDesig.Text.Trim();// +"," + lblDept.Text.Trim() + "," + lblLoc.Text.Trim() + "," + lblOffice.Text.Trim();
                sb.Append("<script>");
                sb.Append("window.open('" + strURL + "', '', '');");
                sb.Append("</script>");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                         sb.ToString(), false);
                ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
                break;

            case ("CancelClick"):
                break;
        }
    }
    protected void grTimeSheetApproved_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        //string strPreYrLv = "";
        switch (_commandName)
        {
            
            case ("ViewClick"):
                StringBuilder sb = new StringBuilder();
                string strFromDate = "";
                string strToDate = "";
                lblMsg.Text = "";

                string strURL = "TimeSheetReportPage.aspx?params=" + grTimeSheetApproved.DataKeys[_gridView.SelectedIndex].Values[0].ToString() + "," + ddlMonth.SelectedValue.ToString() + "," + ddlYear.SelectedValue.ToString() + "," + ddlFiscalYear.SelectedValue.ToString(); //lblEmpName.Text.Trim() + "," + lblDesig.Text.Trim();// +"," + lblDept.Text.Trim() + "," + lblLoc.Text.Trim() + "," + lblOffice.Text.Trim();
                sb.Append("<script>");
                sb.Append("window.open('" + strURL + "', '', '');");
                sb.Append("</script>");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                         sb.ToString(), false);
                ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
                break;
            
        }
    }
    protected void btnPriview_Click(object sender, EventArgs e)
    {
        this.OpenRecord();
    }
}
