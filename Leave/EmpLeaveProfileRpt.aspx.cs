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
using System.Data.SqlClient;

public partial class EmpLeaveProfileRpt : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    LeaveApplicationManager objLeaveMgr = new LeaveApplicationManager();
    LeaveManager objLvMgr = new LeaveManager();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();

    DataTable dtLeaveApp = new DataTable();
    DataTable dtEmp = new DataTable();
    DataTable dtPreYrLeave = new DataTable();  
    DataTable dtLeaveDet = new DataTable();
    DataTable dtPreYrLeaveDet = new DataTable();
    HiddenField hfLvOpening = new HiddenField();

    static DataTable dtEmpList = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strEmpID = Request.QueryString["params"];
            lblEmpNo.Text = strEmpID;
            this.SearchEmployee(strEmpID.Trim());
        }

    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        dtLeaveApp.Dispose();
        dtEmp.Dispose();
        dtPreYrLeave.Dispose(); 
        dtLeaveDet.Dispose();
        grLeaveDet.DataSource = null;
        grLeaveDet.DataBind();
        grLeaveStatus.DataSource = null;
        grLeaveStatus.DataBind();
        grPreYrLeaveStatus.DataSource = null;
        grPreYrLeaveStatus.DataBind();
        grPreYrLeaveDet.DataSource = null;
        grPreYrLeaveDet.DataBind();
             
    }

    protected void SearchEmployee(string strEmpID)
    {
        if (string.IsNullOrEmpty(strEmpID) == false)
        {
            //if (Session["DIVISIONID"].ToString() != "")
            //{
            //    dtEmp = objEmpInfoMgr.SelectEmpInfoDivisionWise(txtEmpId.Text.Trim(), Session["DIVISIONID"].ToString());
            //}
            //else
            dtEmp = objEmpInfoMgr.SelectEmpInfoForLeave(strEmpID);

            if (dtEmp.Rows.Count == 0)
            {
               
                
                lblName.Text = "";
                lblDesig.Text = "";
                
                grLeaveStatus.DataSource = null;
                grLeaveStatus.DataBind();

                grLeaveDet.DataSource = null;
                grLeaveDet.DataBind();

                grPreYrLeaveStatus.DataSource = null;
                grPreYrLeaveStatus.DataBind();

                grPreYrLeaveDet.DataSource = null;
                grPreYrLeaveDet.DataBind();
                return;
            }
            else
            {

                this.FillEmpWithLeaveInfo(strEmpID, dtEmp.Rows[0]["Gender"].ToString(),
                    dtEmp.Rows[0]["LeaveStartPeriod"].ToString(), dtEmp.Rows[0]["LeaveEndPeriod"].ToString());
            }
        }
    }
  

    private void FillEmpWithLeaveInfo(string EmpId, string Sex,string strLeaveStartPeriod,string strLeaveEndPeriod)
    {
        dtEmp.Rows.Clear();
        dtEmp.Dispose();

        dtEmp = objLeaveMgr.SelectEmpLeaveProfileEXCPL(EmpId, "0", Sex);

        DateTime dtStart = Convert.ToDateTime(strLeaveStartPeriod);
        DateTime dtEnd = Convert.ToDateTime(strLeaveEndPeriod);
        string strPreYrStartDate = Convert.ToString(dtStart.Year - 1) + "-" + dtStart.Month.ToString() + "-" + dtStart.Day.ToString();
        string strPreYrEndDate = Convert.ToString(dtEnd.Year - 1) + "-" + dtEnd.Month.ToString() + "-" + dtEnd.Day.ToString();


        dtPreYrLeave = objLvMgr.SelectEmpLeaveProfileHistory(EmpId, strPreYrStartDate, strPreYrEndDate);

        if (dtEmp.Rows.Count > 0)
        {
            lblName.Text = dtEmp.Rows[0]["FullName"].ToString();
            lblDesig.Text = dtEmp.Rows[0]["DesigName"].ToString();
            lblLvPackage.Text = dtEmp.Rows[0]["LPackName"].ToString();

            grLeaveStatus.DataSource = null;
            grLeaveStatus.DataBind();

            grLeaveStatus.DataSource = dtEmp;
            grLeaveStatus.DataBind();

            grPreYrLeaveStatus.DataSource = null;
            grPreYrLeaveStatus.DataBind();

            grPreYrLeaveStatus.DataSource = dtPreYrLeave;
            grPreYrLeaveStatus.DataBind();

            this.FormatLeaveStatusGridNumber();
            this.FormatHistoryLeaveStatusGridNumber();
            this.FillEmpLeaveDet(EmpId, Common.SetDate(strLeaveStartPeriod), Common.SetDate(strLeaveEndPeriod));

            this.FillEmpPreYrLeaveDet(EmpId, strPreYrStartDate, strPreYrEndDate);
        }
        else
        {
            lblName.Text = "";
            lblDesig.Text = "";
            lblLvPackage.Text = "";

            grLeaveStatus.DataSource = null;
            grLeaveStatus.DataBind();

            grLeaveDet.DataSource = null;
            grLeaveDet.DataBind();

            grPreYrLeaveStatus.DataSource = null;
            grPreYrLeaveStatus.DataBind();

            grPreYrLeaveDet.DataSource = null;
            grPreYrLeaveDet.DataBind();
            
        }
    }

    protected void FormatLeaveStatusGridNumber()
    {
        int i = 0;
        foreach (GridViewRow gRow in grLeaveStatus.Rows)
        {
            gRow.Cells[1].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[1].Text)), 1));
            gRow.Cells[2].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[2].Text)), 1));
            gRow.Cells[3].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[3].Text)), 1));
            gRow.Cells[4].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[2].Text)) + Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[3].Text)), 1));
            gRow.Cells[5].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[5].Text)), 1) + Convert.ToDouble(grLeaveStatus.DataKeys[i].Values[9].ToString().Trim() == "" ? "0" : grLeaveStatus.DataKeys[i].Values[9].ToString().Trim()));
            gRow.Cells[6].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[4].Text)), 1) + (Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[1].Text)), 1)) - Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[5].Text)));

            if (Convert.ToDecimal(gRow.Cells[6].Text) < 0)
            {
                gRow.Cells[6].Text = "0";
            }
            i++;
        }
    }

    protected void FormatHistoryLeaveStatusGridNumber()
    {
        int i = 0;
        foreach (GridViewRow gRow in grPreYrLeaveStatus.Rows)
        {
            gRow.Cells[1].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[1].Text)), 1));
            gRow.Cells[2].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[2].Text)), 1));
            gRow.Cells[3].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[1].Text)) + Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[2].Text)), 1));
            //gRow.Cells[4].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[4].Text)), 0) + Convert.ToDouble(grLeaveStatus.DataKeys[i].Values[8].ToString().Trim() == "" ? "0" : grLeaveStatus.DataKeys[i].Values[8].ToString().Trim()));
            gRow.Cells[4].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[4].Text)), 1));
            gRow.Cells[5].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[3].Text)) - Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[4].Text)), 1));

            if (Convert.ToDecimal(gRow.Cells[5].Text) < 0)
            {
                gRow.Cells[5].Text = "0";
            }
            i++;
        }
    }

    private void FillEmpLeaveDet(string EmpId, string strStartDate, string strEndDate)
    {
        dtLeaveDet = objLeaveMgr.SelectEmpLeaveDetails(EmpId, strStartDate,strEndDate);
        grLeaveDet.DataSource = dtLeaveDet;
        grLeaveDet.DataBind();
        this.FormatLeaveDetailGrid();
    }

    private void FillEmpPreYrLeaveDet(string EmpId, string strPreYrStartDate, string strPreYrEndDate)
    {
        grPreYrLeaveDet.DataSource = null;
        grPreYrLeaveDet.DataBind();

        dtPreYrLeaveDet = objLeaveMgr.SelectEmpPreLeaveDetails(EmpId, strPreYrStartDate, strPreYrEndDate);
        grPreYrLeaveDet.DataSource = dtPreYrLeaveDet;
        grPreYrLeaveDet.DataBind();
        this.FormatPreYrLeaveDetailGrid();
    }

    protected void FormatLeaveDetailGrid()
    {
        foreach (GridViewRow gRow in grLeaveDet.Rows)
        {
            gRow.Cells[1].Text = Common.DisplayDate(gRow.Cells[1].Text);
            gRow.Cells[2].Text = Common.DisplayDate(gRow.Cells[2].Text);
            gRow.Cells[3].Text = Common.DisplayDate(gRow.Cells[3].Text);            
            gRow.Cells[4].Text = Convert.ToString(Math.Round(Convert.ToDouble(gRow.Cells[4].Text), 1));
            if (gRow.Cells[5].Text.Trim()  == "A")
                gRow.Cells[5].Text = "Availed";
            else if (gRow.Cells[5].Text.Trim()  == "R")
                gRow.Cells[5].Text = "Requested";
            if(Common.CheckNullString(gRow.Cells[7].Text.Trim())!="")
                gRow.Cells[7].Text = Common.DisplayDate(gRow.Cells[7].Text);
        }
    }

    protected void FormatPreYrLeaveDetailGrid()
    {
        foreach (GridViewRow gRow in grPreYrLeaveDet.Rows)
        {
            gRow.Cells[1].Text = Common.DisplayDate(gRow.Cells[1].Text);
            gRow.Cells[2].Text = Common.DisplayDate(gRow.Cells[2].Text);
            gRow.Cells[3].Text = Common.DisplayDate(gRow.Cells[3].Text);
            gRow.Cells[4].Text = Convert.ToString(Math.Round(Convert.ToDouble(gRow.Cells[4].Text), 1));
            if (gRow.Cells[5].Text.Trim() == "A")
                gRow.Cells[5].Text = "Availed";
            else if (gRow.Cells[5].Text.Trim() == "R")
                gRow.Cells[5].Text = "Requested";
            gRow.Cells[7].Text = Common.DisplayDate(gRow.Cells[7].Text);
        }
    }

   
    protected void grLeaveStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grLeaveDet_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grLeaveDet_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //GridView _gridView = (GridView)sender;
        //// Get the selected index and the command name
        //int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        //string _commandName = e.CommandName;
        //_gridView.SelectedIndex = _selectedIndex;
        //switch (_commandName)
        //{      
        //    case ("RowDeleting"):
        //        DataTable dtUserPriv = new DataTable();
        //        UserManager objUser = new UserManager();

        //        dtUserPriv = objUser.SelectUserPriv(Session["USERID"].ToString());

        //        if (dtUserPriv.Rows.Count > 0)
        //        {
        //            DataRow[] foundRows;
        //            string strExpr = "";

        //            strExpr ="SCREEN_ID='501' AND A='Y'";
        //            foundRows = dtUserPriv.Select(strExpr);
        //            if (foundRows.Length ==0  )
        //            {
        //                lblMsg.Text = "You dot not have the permission to cancel leave.";
        //                return; 
        //            }
        //        }

        //        HiddenField hfLvAppId = new HiddenField();
        //        hfLvAppId.Value = grLeaveDet.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim();

        //        HiddenField hfLvTitleId = new HiddenField();
        //        hfLvTitleId.Value = grLeaveDet.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim();

        //        //Enjoyed leave calculation
        //        DataTable dtLeaveProfile = new DataTable();
        //        dtLeaveProfile = objLeaveMgr.SelectEmpLeaveProfile2(txtEmpId.Text.Trim(), hfLvTitleId.Value.ToString());
        //        HiddenField hfLvEnjoyed = new HiddenField();
        //        if (dtLeaveProfile.Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dtLeaveProfile.Rows)
        //            {
        //                if (string.IsNullOrEmpty(row["LeaveEnjoyed"].ToString()) == false)
        //                {        
        //                    hfLvEnjoyed.Value = Convert.ToString(Convert.ToDecimal(row["LeaveEnjoyed"].ToString()) - Convert.ToDecimal(grLeaveDet.DataKeys[_gridView.SelectedIndex].Values[6].ToString()));  
        //                }
        //                else
        //                    hfLvEnjoyed.Value = "0";                        
        //            }
        //        }
        //        else
        //            hfLvEnjoyed.Value = "0"; 
                
        //        //All date between Date duration
        //        HiddenField hfLDates = new HiddenField();
        //        DateTime LDate = Convert.ToDateTime(grLeaveDet.DataKeys[_gridView.SelectedIndex].Values[4].ToString().Trim());
        //        int i = 0;
        //        int LeaveDay = 0;
        //        hfLDates.Value = "";
        //        for (i = 0; i < Math.Round(Convert.ToDecimal(grLeaveDet.DataKeys[_gridView.SelectedIndex].Values[6].ToString().Trim()), 0); i++)
        //        {
        //            if (hfLDates.Value != "")
        //            {
        //                hfLDates.Value = hfLDates.Value + ",";
        //            }
        //            LeaveDay = LeaveDay + 1;
        //            hfLDates.Value = hfLDates.Value + Common.DisplayDate(LDate.ToString());
        //            LDate = LDate.AddDays(1);
        //        }

        //        LeaveApp objLeave = new LeaveApp(hfLvAppId.Value, txtEmpId.Text.Trim(), "", "", "", "", "",
        //            "C", "R", Session["USERID"].ToString(),
        //            Common.SetDateTime(DateTime.Now.ToString()), "Y", "N", hfLvTitleId.Value, "", "", hfLvEnjoyed.Value.ToString(),);

        //        objLeaveMgr.UpdateLeaveAppMstForCancel(objLeave, "Y", "C", hfLvEnjoyed.Value.ToString(), hfLDates.Value.ToString());

        //        lblMsg.Text = "Leave Cancelled Successfully";

        //        if (txtEmpId.Text.Trim() != "")
        //        {
        //            grLeaveStatus.DataSource = null;
        //            grLeaveStatus.DataBind(); 

        //            this.FillEmpWithLeaveInfo(txtEmpId.Text.Trim());
        //        }
        //        hfLvAppId.Value = "";
        //        hfLvTitleId.Value = "";
        //        hfLvEnjoyed.Value = "";
        //        hfLDates.Value = "";               
        //        dtLeaveProfile.Dispose(); 
        //        break;
        //}  
    }

    protected void grEmpList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
       
        switch (_commandName)
        {
            case ("ViewClick"):
              
                break;

        }
    }
}
