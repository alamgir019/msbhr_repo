using System;
using System.Data;
using System.Data.SqlClient ;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Attendance_Holiday : System.Web.UI.Page
{
    HolidayTableManager objHolMgr = new HolidayTableManager();

    DataTable dtTemp = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillYearList(5, ddlYear);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);

            Common.FillYearList(5, ddlShowYear);
            ddlShowYear.SelectedValue = Convert.ToString(DateTime.Today.Year);

            Common.FillYearList(5, ddlToYear);
            ddlToYear.SelectedValue = Convert.ToString(DateTime.Today.Year);

            dtTemp = Common.SelectDistinct("DisYear", objHolMgr.GetDataList(), "HolidayYear", "HolidayYear");
            Common.FillDropDownList(dtTemp, ddlShowYear, "HolidayYear", "HolidayYear", false);
            Common.FillDropDownList(dtTemp, ddlFromYear, "HolidayYear", "HolidayYear", false);
           
            this.OpenRecord(DateTime.Today.Year.ToString());
            //}            
            this.EntryMode(false, 2);
            this.EntryMode(false, 0);
            if (grHoliday.Rows.Count > 0)
                ddlShowYear.SelectedValue = DateTime.Today.Year.ToString();           
        }
    }

    protected void EntryMode(bool IsUpdate, int TabIndex)
    {
        if (TabIndex == 0)
        {
            if (IsUpdate == true)
            {
                btnSave.Text = "Update";
                hfIsUpdate.Value = "Y";
            }
            else
            {
                btnMultiSave.Enabled = true;
                btnDelete.Enabled = true;

                ddlYear.SelectedValue = DateTime.Today.Year.ToString();
                txtHolidayTitle.Text = "";
                txtHoliDesc.Text = "";
                ddlMonthFrom.SelectedIndex = 0;
                ddlDayFrom.SelectedIndex = 0;
                ddlMonthTo.SelectedIndex = 0;
                ddlDayTo.SelectedIndex = 0;
                chkInActive.Checked = false;
                chkIsFestival.Checked = false;
                btnMultiSave.Text = "Save";
                hfIsUpdate.Value = "N";
                btnSave.Text = "Save";

                TabContainer1.ActiveTabIndex = 0;
            }
        }        
    }

    private void OpenRecord(string strYear)
    {
        //if (string.IsNullOrEmpty(Session["DIVISIONID"].ToString()) == false && string.IsNullOrEmpty(Session["BRANCHID"].ToString()) == false)
        //{
        grHoliday.DataSource = objHolMgr.GetData(strYear, "");
        grHoliday.DataBind();

        //grMultiHoliday.DataSource = objHolMgr.GetData(strYear, Session["DivisionId"].ToString(), Session["BRANCHID"].ToString());
        //grMultiHoliday.DataBind();


        this.FormatHolidayDayGridNumber1();
        this.FormatHolidayDayGridNumber2();
        //}
    }
   
    protected void FormatHolidayDayGridNumber1()
    {
        foreach (GridViewRow gRow in grHoliday.Rows)
        {
            gRow.Cells[3].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[3].Text)), 0));
        }
    }

    protected void FormatHolidayDayGridNumber2()
    {
        foreach (GridViewRow gRow in grMultiHoliday.Rows)
        {
            gRow.Cells[2].Text = Convert.ToString(Common.DisplayDate(gRow.Cells[2].Text));
            gRow.Cells[3].Text = Convert.ToString(Common.DisplayDate(gRow.Cells[3].Text));
            gRow.Cells[4].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[4].Text)), 0));
        }
    }

      protected bool ValidateAndSave(string strIsUpdate, int TabIndex)
    {
        if (TabIndex == 0)
        {
            string strMonthFrom = "";
            string strMonthTo = "";
            if (Convert.ToInt32(ddlMonthFrom.SelectedValue) <= 9)
                strMonthFrom = "0" + ddlMonthFrom.SelectedValue.ToString();
            else
                strMonthFrom = ddlMonthFrom.SelectedValue.ToString();
            if (Convert.ToInt32(ddlMonthTo.SelectedValue) <= 9)
                strMonthTo = "0" + ddlMonthTo.SelectedValue.ToString();
            else
                strMonthTo = ddlMonthTo.SelectedValue.ToString();
            string StartDate = ddlYear.SelectedValue.ToString() + "/" + strMonthFrom + "/" + ddlDayFrom.SelectedValue.ToString();
            string EndDate = ddlYear.SelectedValue.ToString() + "/" + strMonthTo + "/" + ddlDayTo.SelectedValue.ToString();
            DateTime dtStart = Convert.ToDateTime(StartDate);
            DateTime dtEnd = Convert.ToDateTime(EndDate);
            TimeSpan tsDateDiff = dtEnd - dtStart;
            int intDateDiff = tsDateDiff.Days;
            if (intDateDiff < 0)
            {
                lblMsg.Text = "Start Date Can not be Greater Than End Date. Please Select Valid End Date.";
                return false;
            }
            intDateDiff++;
            DataTable dt = new DataTable();
            //if (string.IsNullOrEmpty(Session["BRANCHID"].ToString()) == false && (string.IsNullOrEmpty(Session["DIVISIONId"].ToString()) == false))
            dt = objHolMgr.GetChildData(ddlYear.SelectedValue.ToString());

            foreach (DataRow dRow in dt.Rows)
            {                
                if (DateTime.Compare(dtStart, Convert.ToDateTime(dRow["HoliDate"].ToString())) == 0)
                {
                    lblMsg.Text = "Record on Selected Date Is Exist";
                    return false;

                }
                else if (DateTime.Compare(dtEnd, Convert.ToDateTime(dRow["HoliDate"].ToString())) == 0)
                {
                    lblMsg.Text = "Record on Selected Date Is Exist";
                    return false;
                }
                else
                {
                    for (int j = 1; j < intDateDiff; j++)
                    {
                        DateTime dtDay = dtStart.AddDays(j);
                        if (DateTime.Compare(dtDay, Convert.ToDateTime(dRow["HoliDate"].ToString())) == 0)
                        {
                            lblMsg.Text = "Record on Selected Date Is Exist";
                            return false;
                        }
                    }
                }
            }
            return true;
        }       
        return true;
    }

    private void SaveData(string IsDelete)
    {
        try
        {
            if (IsDelete == "N")
            {
                if (ValidateAndSave(hfIsUpdate.Value, TabContainer1.ActiveTabIndex) == false)
                {
                    lblMsg.CssClass = "msglabelerr";
                    return;
                }
            }
            string strHolidayId = "";
            int i = 0;
            string IsUpdate = hfIsUpdate.Value;
            string strHoliDays = "";
            string strMonthFrom = "";
            string strMonthTo = "";
            if (Convert.ToInt32(ddlMonthFrom.SelectedValue) <= 9)
                strMonthFrom = "0" + ddlMonthFrom.SelectedValue.ToString();
            else
                strMonthFrom = ddlMonthFrom.SelectedValue.ToString();
            if (Convert.ToInt32(ddlMonthTo.SelectedValue) <= 9)
                strMonthTo = "0" + ddlMonthTo.SelectedValue.ToString();
            else
                strMonthTo = ddlMonthTo.SelectedValue.ToString();
            string StartDate = ddlYear.SelectedValue.ToString() + "/" + strMonthFrom + "/" + ddlDayFrom.SelectedValue.ToString();
            string EndDate = ddlYear.SelectedValue.ToString() + "/" + strMonthTo + "/" + ddlDayTo.SelectedValue.ToString();
            DateTime dtStart = Convert.ToDateTime(StartDate);
            DateTime dtEnd = Convert.ToDateTime(EndDate);

            TimeSpan tsDateDiff = dtEnd - dtStart;
            int intDateDiff = tsDateDiff.Days;

            intDateDiff++;

            for (i = 0; i < intDateDiff; i++)
            {
                if (strHoliDays == "")
                    strHoliDays = Convert.ToString(dtStart.AddDays(i));
                else
                    strHoliDays = strHoliDays + "," + Convert.ToString(dtStart.AddDays(i));
            }

            if ((IsUpdate == "N") && (IsDelete == "N"))
            {
                strHolidayId = Common.getMaxId("HolidaysMst", "HoliDayId");
            }
            else
            {
                strHolidayId = hfHolidayId.Value;
            }
           
            Holiday objHoli = new Holiday(ddlYear.SelectedValue.ToString(), strHolidayId, txtHolidayTitle.Text.Trim(), StartDate, EndDate, intDateDiff.ToString(), txtHoliDesc.Text.Trim(), strHoliDays, chkInActive.Checked ==true ?"N":"Y",
                "1", "1", Session["USERID"].ToString(), Common.SetDate(DateTime.Today.ToString()));

            objHolMgr.InsertHoliday(IsUpdate, IsDelete, objHoli, chkIsFestival.Checked == true ? "Y" : "N");

            if ((IsUpdate == "N") && (IsDelete == "N"))
                lblMsg.Text = "Record Saved Successfully";
            else if ((IsUpdate == "Y") && (IsDelete == "N"))
                lblMsg.Text = "Record Updated Successfully";
            else if ((IsUpdate == "Y") && (IsDelete == "Y"))
                lblMsg.Text = "Record Deleted Successfully";

            lblMsg.CssClass = "msglabel";

            this.EntryMode(false, 0);
            this.OpenRecord(ddlShowYear.SelectedValue.ToString());

            dtTemp = Common.SelectDistinct("DisYear", objHolMgr.GetDataList(), "HolidayYear", "HolidayYear");
            Common.FillDropDownList(dtTemp, ddlShowYear, "HolidayYear", "HolidayYear", false);
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected string GetDate(string strDate, int Duration)
    {
        string strValue = "";
        DateTime date;
        string StrStartDate = "";
        string strEndDate = "";
        if (Duration == 1)
            strValue = Common.DisplayDate(strDate);
        else
        {
            date = Convert.ToDateTime(strDate);
            date = date.AddDays(Duration - 1);
            strEndDate = Common.DisplayDate(date.ToShortDateString());
            StrStartDate = Common.DisplayDate(strDate);
            strValue = StrStartDate + " To " + strEndDate;
        }
        return strValue;
    }

    protected void grHoliday_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        string[] arInfo = new string[3];
        switch (_commandName)
        {
            case ("DoubleClick"):

                hfHolidayId.Value = grHoliday.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
                txtHolidayTitle.Text = grHoliday.SelectedRow.Cells[1].Text;
                chkInActive.Checked = grHoliday.SelectedRow.Cells[4].Text == "Y" ? false : true;

                decimal decDuration = Convert.ToDecimal(grHoliday.SelectedRow.Cells[3].Text);
                int intDuration = Convert.ToInt32(decDuration);
                DateTime dtStart = Convert.ToDateTime(grHoliday.DataKeys[_gridView.SelectedIndex].Values[3].ToString().Trim());
                //dtStart= (Convert.ToString(  dtStart));
                DateTime dtEnd = dtStart.AddDays(intDuration - 1);
                string strStartDate = Common.DisplayDate(dtStart.ToShortDateString());
                arInfo = Common.SpllitedDate(strStartDate);
                ddlYear.SelectedValue = arInfo[2].ToString();
                ddlMonthFrom.SelectedValue = Convert.ToInt32(arInfo[1]).ToString();
                // lblMsg.Text = arInfo[1].ToString();
                Common.FillDayList(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonthFrom.SelectedValue), ddlDayFrom);
                if (arInfo[0].Length == 1)
                    arInfo[0] = "0" + arInfo[0];
                ddlDayFrom.SelectedValue = arInfo[0];

                arInfo = null;

                string strEndDate = Common.DisplayDate(dtEnd.ToShortDateString());
                arInfo = Common.SpllitedDate(strEndDate);
                ddlMonthTo.SelectedValue = Convert.ToInt32(arInfo[1]).ToString();
                Common.FillDayList(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonthTo.SelectedValue), ddlDayTo);

                if (arInfo[0].Length == 1)
                    arInfo[0] = "0" + arInfo[0];
                ddlDayTo.SelectedValue = arInfo[0];

                arInfo = null;

                txtHoliDesc.Text = grHoliday.DataKeys[_gridView.SelectedIndex].Values[2].ToString().Trim();
                chkIsFestival.Checked = grHoliday.DataKeys[_gridView.SelectedIndex].Values[4].ToString().Trim() == "Y" ? true : false;
                this.EntryMode(true, 0);
                TabContainer1.ActiveTabIndex = 0;
                break;
        }
    }

    private Int32 retMonthNumber(string str)
    {
        if (str == "")
        {
            return 0;
        }
        else if (str == "Jan")
        {
            return 1;
        }
        else if (str == "Feb")
        {
            return 2;
        }
        else if (str == "Mar")
        {
            return 3;
        }
        else if (str == "Apr")
        {
            return 4;
        }
        else if (str == "May")
        {
            return 5;
        }
        else if (str == "Jun")
        {
            return 6;
        }
        else if (str == "Jul")
        {
            return 7;
        }
        else if (str == "Aug")
        {
            return 8;
        }
        else if (str == "Sep")
        {
            return 9;
        }
        else if (str == "01")
        {
            return 1;
        }
        else if (str == "02")
        {
            return 2;
        }
        else if (str == "03")
        {
            return 3;
        }
        else if (str == "04")
        {
            return 4;
        }
        else if (str == "05")
        {
            return 5;
        }
        else if (str == "06")
        {
            return 6;
        }
        else if (str == "07")
        {
            return 7;
        }
        else if (str == "08")
        {
            return 8;
        }
        else if (str == "09")
        {
            return 9;
        }
        else if (str == "Oct")
        {
            return 10;
        }
        else if (str == "Nov")
        {
            return 11;
        }
        else if (str == "Dec")
        {
            return 12;
        }
        else
            return 12;
    }

    protected void ddlMonthFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMonthFrom.SelectedValue != "-1")
            Common.FillDayList(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonthFrom.SelectedValue), ddlDayFrom);
    }

    protected void ddlMonthTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMonthTo.SelectedValue != "-1")
            Common.FillDayList(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonthTo.SelectedValue), ddlDayTo);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.SaveData("N");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        this.SaveData("Y");
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        grHoliday.DataSource = objHolMgr.GetData(ddlShowYear.SelectedValue.ToString(), "N");
        grHoliday.DataBind();
        this.FormatHolidayDayGridNumber1();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        this.EntryMode(false, 0);
        lblMsg.Text = "";
    }

    protected void btnFromYear_Click(object sender, EventArgs e)
    {
        //if (string.IsNullOrEmpty(Session["DivisionId"].ToString()) == false && string.IsNullOrEmpty(Session["BRANCHID"].ToString()) == false)
        //{
        grMultiHoliday.DataSource = objHolMgr.GetMultipleData(ddlFromYear.SelectedValue.ToString());
        grMultiHoliday.DataBind();
        this.FormatHolidayDayGridNumber2();
        //}
    }

    protected void btnMultiSave_Click(object sender, EventArgs e)
    {
        this.MultiSave();
    }

    private void MultiSave()
    {
        int CkeckedMstDataCount = 0;
        try
        {
            foreach (GridViewRow gRow in grMultiHoliday.Rows)
            {
                CheckBox chBox = new CheckBox();
                chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
                if (chBox.Checked == true)
                {
                    CkeckedMstDataCount = CkeckedMstDataCount + 1;
                    if (Convert.ToInt32(gRow.Cells[4].Text) > 0)
                        CkeckedMstDataCount = CkeckedMstDataCount + Convert.ToInt32(gRow.Cells[4].Text);
                }
            }

            string strInsBy = Session["USERID"].ToString();
            string strInsDate = Common.SetDateTime(DateTime.Now.ToString());

            objHolMgr.InsertMultipleHoliday(grMultiHoliday, CkeckedMstDataCount, ddlToYear.SelectedValue.ToString(),
                strInsBy, strInsDate, "N", "N");
            lblMsg.Text = "Record Saved Successfully";
            //lblMsg.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (grMultiHoliday.Rows.Count > 0)
        {
            foreach (GridViewRow gRow in grMultiHoliday.Rows)
            {
                gRow.Cells[2].Text = Common.ReturnDay(gRow.Cells[2].Text) + "/" + ddlToYear.SelectedValue.ToString();
                gRow.Cells[3].Text = Common.ReturnDay(gRow.Cells[3].Text) + "/" + ddlToYear.SelectedValue.ToString();
            }
        }
    }   
}
