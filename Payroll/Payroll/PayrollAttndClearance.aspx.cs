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

public partial class Payroll_Payroll_PayrollAttndClearance : System.Web.UI.Page
{
    Payroll_PreparationManager objPreMgr = new Payroll_PreparationManager();
    MasterTablesManager objMastMg = new MasterTablesManager();
    Payroll_AttendanceClearance objPayAttndMgr = new Payroll_AttendanceClearance();
    Payroll_PaySlipOptionMgr objPayOptMgr = new Payroll_PaySlipOptionMgr();
    string strAttnEndDate = "";
    string strAttnStartDate = "";
    DataTable dtMPC = new DataTable();

    DateTime AttnDateFrom = new DateTime();
    DateTime AttnDateTo = new DateTime();
    DateTime PayDateFrom = new DateTime();
    DateTime PayDateTo = new DateTime();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            txtIssueDate.Text = Common.DisplayDate(Common.SetDate(DateTime.Today.ToShortDateString()));
            //txtDateFrom.Text = "21" + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
            //txtDateTo.Text = Convert.ToString(Common.GetMonthDay(DateTime.Today)) + "/" + (DateTime.Today.Month+1). + "/" + DateTime.Today.Year.ToString();
            //this.FillGenerateDropDownList();
            Common.FillDropDownList(objPayOptMgr.GetMonthlyPayrollCycleData(), ddlMPC, "MPCTITLE", "MPCID", false);
            Common.FillDropDownList(objMastMg.SelectEmpType(0,"Y"), ddlEmpType, "TypeName", "EmpTypeID", false);
            Common.FillDropDownList_All(objMastMg.SelectClinic(), ddlCostCenter);
        }
    }

    protected void OpenExistingRecord()
    {
        DataTable dtExistingData = objPayAttndMgr.GetExistingClearanceRecord(ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim(),
            ddlMPC.SelectedValue.ToString(), ddlEmpType.SelectedValue.Trim());
        int days = DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue));

        if (dtExistingData.Rows.Count > 0)
        {
            TabContainer1.ActiveTabIndex = 1;
        }
        else
        {
            TabContainer1.ActiveTabIndex = 0;
        }

        grAttndClr.DataSource = dtExistingData;
        grAttndClr.DataBind();

        lblExistRecordCount.Text = grAttndClr.Rows.Count.ToString();

        foreach (GridViewRow gRow in grAttndClr.Rows)
        {
            if (Common.CheckNullString(gRow.Cells[4].Text.Trim()) != "")
            {
                gRow.Cells[4].Text = Common.DisplayDate(gRow.Cells[4].Text.Trim());
            }

            if (Common.CheckNullString(gRow.Cells[5].Text.Trim()) != "")
            {
                gRow.Cells[5].Text = Common.DisplayDate(gRow.Cells[5].Text.Trim());
            }

            if (Common.CheckNullString(gRow.Cells[7].Text.Trim()) != "")
            {
                gRow.Cells[7].Text = Common.DisplayDate(gRow.Cells[7].Text.Trim());
            }

            if (Common.CheckNullString(gRow.Cells[8].Text.Trim()) != "")
            {
                gRow.Cells[8].Text = Common.DisplayDate(gRow.Cells[8].Text.Trim());
            }

            if (gRow.Cells[20].Text.Trim() == "Y")
            {
                gRow.Cells[10].BackColor = System.Drawing.Color.Yellow;
            }

            if (Convert.ToInt32(gRow.Cells[10].Text.Trim()) < days)
            {
                gRow.Cells[10].BackColor = System.Drawing.Color.Yellow;
            }
        }
    }

    protected void OpenEmployeeRecord()
    {
        dtMPC = objPayOptMgr.GetMPCData(ddlMPC.SelectedValue.Trim());
        if (dtMPC.Rows.Count > 0)
        {
            this.GetEmpWiseValidationDate(dtMPC.Rows[0]["ASTARTDAY"].ToString().Trim(),
                dtMPC.Rows[0]["AENDDAY"].ToString().Trim(),
                dtMPC.Rows[0]["PSTARTDAY"].ToString().Trim(),
                dtMPC.Rows[0]["PENDDAY"].ToString().Trim(),
                ddlMonth.SelectedValue.Trim(),
                ddlYear.SelectedValue.Trim());

            strAttnStartDate = Common.SetDate(AttnDateFrom.ToShortDateString());
            //if (ddlEmpType.SelectedValue == "1")
            //    strAttnEndDate = Common.SetDate(AttnDateTo.ToShortDateString());
            //else
            //{
                //DateTime dtToDate = new DateTime();
                AttnDateTo = AttnDateTo.AddDays(10);  
                strAttnEndDate = Common.SetDate(AttnDateTo.ToShortDateString());
            //}
        }
        else
            return;


        DataTable dtEmpInfo = objPreMgr.GetEmployeeDataForAttndClearance(strAttnEndDate, ddlMPC.SelectedValue.ToString(),
            txtEmpID.Text.Trim(), ddlEmpType.SelectedValue.Trim(), ddlCostCenter.SelectedValue.ToString().Trim());
        if (dtEmpInfo.Rows.Count == 0)
        {
            lblMsg.Text = "No Employee information found... ";
            return;
        }
        else
        {
            lblMsg.Text = "";
            grAttendance.DataSource = dtEmpInfo;
            grAttendance.DataBind();
            lblRecordCount.Text = grAttendance.Rows.Count.ToString();
            this.GenerateAttendanceSummery();
        }
    }

    protected void GenerateAttendanceSummery()
    {
        long lngTotalAttend = 0;
        long lngTotalLWP = 0;
        long lngTotalTravel = 0;
        long lngTotalLeave = 0;
        long lngTotalDelay = 0;
        long lngTotalWeekend = 0;
        long lngTotalAbsent = 0;
        long lngTotalWkButPresent = 0;
        long lngTotalHolidayButPresent = 0;
        long lngTotalHoliday = 0;
        long lngTotalWorkDay = 0;
        long lngAttnMonthDay = 0;
        long lngSalMonthDay = 0;

        bool IsIrregular = false;
        bool IsJoiner = false;
        bool IsSeperator = false;

        DateTime JoinDate = new DateTime();


        string strStartDate = "";
        string strEndDate = "";
        DateTime LastWorkDay = new DateTime();

        // Attendance days duration 
        ////TimeSpan tsm = AttnDateTo - AttnDateFrom;
        TimeSpan tsm = PayDateTo - PayDateFrom;
        lngAttnMonthDay = tsm.Days + 1;

        TimeSpan tss = PayDateTo - PayDateFrom;
        lngSalMonthDay = tss.Days + 1;

        foreach (GridViewRow gEmpRow in grAttendance.Rows)
        {
            lngTotalAttend = 0;
            lngTotalLWP = 0;
            lngTotalTravel = 0;
            lngTotalLeave = 0;
            lngTotalDelay = 0;
            lngTotalWeekend = 0;
            lngTotalAbsent = 0;
            lngTotalWkButPresent = 0;
            lngTotalHolidayButPresent = 0;
            lngTotalHoliday = 0;
            lngTotalWorkDay = 0;

            IsIrregular = false;
            IsJoiner = false;
            IsSeperator = false;

            TextBox txtAttndDays = (TextBox)gEmpRow.FindControl("txtAttndDays");
            TextBox txtSDays = (TextBox)gEmpRow.FindControl("txtSalDays");
            //TextBox txtLWP = (TextBox)gEmpRow.FindControl("txtLWP");

            if (Common.CheckNullString(gEmpRow.Cells[4].Text.Trim()) != "")
            {
                gEmpRow.Cells[4].Text = Common.DisplayDate(gEmpRow.Cells[4].Text.Trim());
            }
            if (Common.CheckNullString(gEmpRow.Cells[5].Text.Trim()) != "")
                gEmpRow.Cells[5].Text = Common.DisplayDate(gEmpRow.Cells[5].Text.Trim());

            if (Convert.ToDateTime(grAttendance.DataKeys[gEmpRow.DataItemIndex].Values[0].ToString().Trim()) > AttnDateTo)
            {
                continue;
            }
            ////// Validate with Joing date
            ////if ((Convert.ToDateTime(grAttendance.DataKeys[gEmpRow.DataItemIndex].Values[0].ToString().Trim()) > AttnDateFrom) &&
            ////                (Convert.ToDateTime(grAttendance.DataKeys[gEmpRow.DataItemIndex].Values[0].ToString().Trim()) <= AttnDateTo))
            ////{
            ////    IsIrregular = true;
            ////    IsJoiner = true;
            ////}

            // Validate with Joing date
            if ((Convert.ToDateTime(grAttendance.DataKeys[gEmpRow.DataItemIndex].Values[0].ToString().Trim()) > PayDateFrom) &&
                            (Convert.ToDateTime(grAttendance.DataKeys[gEmpRow.DataItemIndex].Values[0].ToString().Trim()) <= PayDateTo))
            {
                IsIrregular = true;
                IsJoiner = true;
            }

            // validate with Leaving Date
            if (string.IsNullOrEmpty(grAttendance.DataKeys[gEmpRow.DataItemIndex].Values[1].ToString().Trim()) == false)
            {
                if ((Convert.ToDateTime(grAttendance.DataKeys[gEmpRow.DataItemIndex].Values[1].ToString().Trim()) > PayDateFrom) &&
                    (Convert.ToDateTime(grAttendance.DataKeys[gEmpRow.DataItemIndex].Values[1].ToString().Trim()) <= PayDateTo))
                {
                    IsIrregular = true;
                    IsSeperator = true;
                }
            }

            DataTable dtAttnRecord = objPreMgr.getEmpWiseAttendanceRecord(gEmpRow.Cells[1].Text.Trim(), strAttnStartDate, strAttnEndDate);
            foreach (DataRow dAttnRow in dtAttnRecord.Rows)
            {
                switch (dAttnRow["STATUS"].ToString().Trim())
                {
                    case "P":
                        lngTotalAttend = lngTotalAttend + 1;
                        break;
                    case "LV":
                        if (dAttnRow["LeaveFlag"].ToString().Trim() == "LWP")
                        {
                            lngTotalLWP = lngTotalLWP + 1;
                        }
                        lngTotalLeave = lngTotalLeave + 1;
                        break;
                    case "TV":
                        lngTotalTravel = lngTotalTravel + 1;
                        break;
                    case "HP":
                        lngTotalHolidayButPresent = lngTotalHolidayButPresent + 1;
                        break;
                    case "H":
                        lngTotalHoliday = lngTotalHoliday + 1;
                        break;
                    case "L":
                        lngTotalDelay = lngTotalDelay + 1;
                        lngTotalAttend = lngTotalAttend + 1;
                        break;
                    case "W":
                        lngTotalWeekend = lngTotalWeekend + 1;
                        break;
                    case "WP":
                        lngTotalWkButPresent = lngTotalWkButPresent + 1;
                        break;
                    case "A":
                        lngTotalAbsent = lngTotalAbsent + 1;
                        break;
                }
            }
            // Fill Attendance information to gridview
            // Attendance date
            ////gEmpRow.Cells[7].Text = Common.DisplayDate(AttnDateFrom.ToShortDateString());
            gEmpRow.Cells[7].Text = Common.DisplayDate(PayDateFrom.ToShortDateString());
            gEmpRow.Cells[7].ToolTip = Common.DisplayDate(PayDateFrom.ToShortDateString());

            ////gEmpRow.Cells[8].Text = Common.DisplayDate(AttnDateTo.ToShortDateString());
            gEmpRow.Cells[8].Text = Common.DisplayDate(PayDateTo.ToShortDateString());
            gEmpRow.Cells[8].ToolTip = Common.DisplayDate(PayDateTo.ToShortDateString());

            // Month days\

            TimeSpan ts;
            TimeSpan tsSal;
            if (IsIrregular == true)
            {
                if (IsJoiner == true)
                {
                    if (string.IsNullOrEmpty(gEmpRow.Cells[4].Text.Trim()) == false)
                    {
                        JoinDate = Convert.ToDateTime(Common.ReturnDate(gEmpRow.Cells[4].Text.Trim()));
                        ////if (JoinDate > AttnDateFrom)
                        if (JoinDate > PayDateFrom)
                        {
                            strStartDate = Common.SetDate(JoinDate.ToString());
                        }
                        else
                        {
                            strStartDate = strAttnStartDate;
                        }
                    }
                    ts = AttnDateTo - Convert.ToDateTime(strStartDate);
                    lngTotalWorkDay = ts.Days + 1;

                    tsSal = PayDateTo - Convert.ToDateTime(strStartDate);
                    lngSalMonthDay = tsSal.Days + 1;

                    if (lngSalMonthDay > (tss.Days + 1))
                        gEmpRow.BackColor = System.Drawing.Color.Green;
                    else if (lngSalMonthDay < (tss.Days + 1))
                        gEmpRow.BackColor = System.Drawing.Color.Yellow;
                }
                if (IsSeperator == true)
                {
                    strStartDate = strAttnStartDate;
                    LastWorkDay = Convert.ToDateTime(grAttendance.DataKeys[gEmpRow.DataItemIndex].Values[1].ToString().Trim());
                    LastWorkDay = LastWorkDay.AddDays(-1);
                    if ((LastWorkDay > PayDateFrom) &&
                        (LastWorkDay <= PayDateTo))
                    {
                        if (LastWorkDay < PayDateTo)
                        {
                            strEndDate = Common.SetDate(LastWorkDay.ToString());
                        }
                        else
                        {
                            strEndDate = Common.SetDate(PayDateTo.ToShortDateString());
                        }
                    }
                    ts = Convert.ToDateTime(strEndDate) - Convert.ToDateTime(strAttnStartDate);
                    lngTotalWorkDay = ts.Days + 1;

                    tsSal = Convert.ToDateTime(strEndDate) - PayDateFrom;
                    lngSalMonthDay = tsSal.Days + 1;

                    gEmpRow.BackColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                ////ts = AttnDateTo - AttnDateFrom;
                ts = PayDateTo - PayDateFrom;
                lngTotalWorkDay = ts.Days + 1;

                tsSal = PayDateTo - PayDateFrom;
                lngSalMonthDay = tsSal.Days + 1;
            }
            //gEmpRow.Cells[9].Text = lngTotalWorkDay.ToString();

            //Attendance Day
            //if (grAttendance.DataKeys[gEmpRow.RowIndex].Values[6].ToString().Trim() == "2")
            //{
            //    //txtAttndDays.Text = "0";
            //    txtSDays.Text = txtAttndDays.Text.Trim();
            //    //txtSDays.Text = gEmpRow.Cells[9].Text.Trim();
            //}
            //else
            //{
                txtAttndDays.Text = lngTotalWorkDay.ToString();
                txtSDays.Text = lngSalMonthDay.ToString();
                //txtLWP.Text = lngTotalLWP.ToString();
                //txtSDays.Text = (lngSalMonthDay - lngTotalLWP).ToString();
            //}

            // gEmpRow.Cells[10].Text = lngSalMonthDay.ToString();

            //Present
            gEmpRow.Cells[11].Text = lngTotalAttend.ToString();

            //Absent
            gEmpRow.Cells[12].Text = lngTotalAbsent.ToString();

            //Weekend
            gEmpRow.Cells[13].Text = lngTotalWeekend.ToString();

            //Weekend but present
            gEmpRow.Cells[14].Text = lngTotalWkButPresent.ToString();

            //Holiday
            gEmpRow.Cells[15].Text = lngTotalHoliday.ToString();

            //Holiday but present
            gEmpRow.Cells[16].Text = lngTotalHolidayButPresent.ToString();

            //Travel
            gEmpRow.Cells[17].Text = lngTotalTravel.ToString();

            //Leave
            gEmpRow.Cells[18].Text = lngTotalLeave.ToString();

            // LWP Leave
            gEmpRow.Cells[19].Text = lngTotalLWP.ToString();

            // Irregular
            gEmpRow.Cells[20].Text = IsIrregular == true ? "Y" : "N";
            if (IsJoiner == true)
            {
                gEmpRow.Cells[20].ToolTip = "J";
            }
            if (IsSeperator == true)
                gEmpRow.Cells[20].ToolTip = "S";
            if (IsJoiner == false && IsSeperator == false)
                gEmpRow.Cells[20].ToolTip = "R";
        }
    }

    protected void GetEmpWiseValidationDate(string strAStart, string strAEnd, string strPStartDate, string strPEndDate, string strMonth, string strYear)
    {
        string strASDate = "";
        string strAEDate = "";
        string strPSDate = "";
        string strPEDate = "";

        int intASDay = Convert.ToInt32(strAStart);
        int intAEDay = Convert.ToInt32(strAEnd);
        int intPSDay = Convert.ToInt32(strPStartDate);
        int intPEDay = Convert.ToInt32(strPEndDate);
        int intAMonth = Convert.ToInt32(strMonth);
        int intPMonth = Convert.ToInt32(strMonth);

        int intAYear = Convert.ToInt32(strYear);
        int intPYear = Convert.ToInt32(strYear);

        intAEDay = this.GetDate(intAEDay, intAMonth, intAYear);
        strAEDate = intAYear.ToString() + "/" + intAMonth.ToString() + "/" + intAEDay.ToString();
        if (intASDay > intAEDay)
        {
            intAMonth = Convert.ToInt32(Common.GetPreviousMonth(strMonth));
            if (intAMonth == 12)
                intAYear = intAYear - 1;

            intASDay = this.GetDate(intASDay, intAMonth, intAYear);
            strASDate = intAYear.ToString() + "/" + intAMonth.ToString() + "/" + intASDay.ToString();
        }
        else
        {
            intASDay = this.GetDate(intASDay, intAMonth, intAYear);
            strASDate = intAYear.ToString() + "/" + intAMonth.ToString() + "/" + intASDay.ToString();
        }

        intPEDay = this.GetDate(intPEDay, intPMonth, intPYear);
        strPEDate = intPYear.ToString() + "/" + intPMonth.ToString() + "/" + intPEDay.ToString();
        if (intPSDay > intPEDay)
        {
            intPMonth = Convert.ToInt32(Common.GetPreviousMonth(strMonth));
            if (intPMonth == 12)
                intPYear = intPYear - 1;
            intPSDay = this.GetDate(intPSDay, intPMonth, intPYear);
            strPSDate = intPYear.ToString() + "/" + intPMonth.ToString() + "/" + intPSDay.ToString();
        }
        else
        {
            intPSDay = this.GetDate(intPSDay, intPMonth, intPYear);
            strPSDate = intPYear.ToString() + "/" + intPMonth.ToString() + "/" + intPSDay.ToString();
        }
        AttnDateFrom = Convert.ToDateTime(strASDate);
        AttnDateTo = Convert.ToDateTime(strAEDate);
        PayDateFrom = Convert.ToDateTime(strPSDate);
        PayDateTo = Convert.ToDateTime(strPEDate);
    }

    protected int GetDate(int Days, int Month, int Year)
    {
        int RetValue = Days;
        if (Days == 31)
            RetValue = Common.GetMonthDay(Month, Year.ToString());
        return RetValue;
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        this.OpenEmployeeRecord();
        this.OpenExistingRecord();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (grAttendance.Rows.Count == 0)
        {
            lblMsg.Text = "No employee found for clearance";
            return;
        }

        objPayAttndMgr.InsertData(grAttendance, ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim(),
            Common.ReturnDate(txtIssueDate.Text.Trim()), Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()));
        lblMsg.Text = "Attendance has been cleared successfully.";
        this.OpenExistingRecord();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (grAttndClr.Rows.Count > 0)
        {
            objPayAttndMgr.DeleteAttendanceClearance(grAttndClr, ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString());
            this.OpenEmployeeRecord();
            this.OpenExistingRecord();
            lblMsg.Text = "Record has been deleted.";
        }
        else
        {
            lblMsg.Text = "No record found for delete.";
        }
    }
}
