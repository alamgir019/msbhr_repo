using System;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Mail;
using System.Net.Mail;

//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
//using System.Drawing.Printing;
//using System.IO;
//using System.Net;

public partial class Leave_LeavePostApproveAdjust : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    LeaveManager objLMgr = new LeaveManager();
    LeaveApplicationManager objLeaveMgr = new LeaveApplicationManager();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    HolidayTableManager objHoliMgr = new HolidayTableManager();

    DataTable dtLeaveApp = new DataTable();
    DataTable dtLeaveType = new DataTable();
    DataTable dtResponsePerson = new DataTable();
    DataTable dtAppType = new DataTable();
    DataTable dtEmpInfo = new DataTable();
    DataTable dtEmpLvProfile = new DataTable();

    OptionManager OptMgr = new OptionManager();


    static string strStartDate = "";
    static string strEndDate = "";
    static double dblTotWeekedDay = 0;
    static double dblTotHoliDay = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpadate.Value = "N";
            dtLeaveApp.Rows.Clear();
            dtLeaveApp.Dispose();
            grLeaveApp.DataSource = null;
            grLeaveApp.DataBind();
            hfLEnjoyed.Value = "0";
            lblMsg.Text = "";
            this.EntryMode(false);

            if (Session["ISADMIN"].ToString() == "N")
            {
                txtEmpId.Text = Session["EMPID"].ToString().ToUpper();
                this.SearchEmployee();
                if (Session["COUNTRYDIRECTOR"].ToString() == "N")
                {
                    //grEmpList.DataSource = objEmpInfoMgr.GetSuperVisiorWiseEmp(Session["EMPID"].ToString().ToUpper(), Session["OFFICEID"].ToString());
                    Common.FillIdNameDropDownList_Nil(objEmpInfoMgr.GetSuperVisiorWiseEmp(Session["EMPID"].ToString().ToUpper(), Session["OFFICEID"].ToString()), ddlEmpList, "FULLNAME", "EMPID", true);
                }
                else
                {
                    //grEmpList.DataSource = objEmpInfoMgr.GetSuperviseeEmp(Session["EMPID"].ToString().ToUpper());
                    Common.FillIdNameDropDownList_Nil(objEmpInfoMgr.GetSuperviseeEmp(Session["EMPID"].ToString().ToUpper()), ddlEmpList, "FULLNAME", "EMPID", true);
                }

                // grEmpList.DataBind();
                //btnSearch.Visible = false;
                imgBtnSearch.Visible = false;
                txtEmpId.Enabled = false;
                pnlEmpList.Visible = true;
            }
            else if (Session["ISADMIN"].ToString() == "Y")
            {
                if (Session["USERID"].ToString().ToUpper() != "ADMIN")
                {
                    txtEmpId.Text = Session["EMPID"].ToString().ToUpper();
                    this.SearchEmployee();
                    //grEmpList.DataSource = objEmpInfoMgr.SelectDivisionWiseEmp(Session["EMPID"].ToString().ToUpper());
                    // grEmpList.DataBind();
                    Common.FillIdNameDropDownList_Nil(objEmpInfoMgr.SelectDivisionWiseEmp(Session["EMPID"].ToString().ToUpper()), ddlEmpList, "FULLNAME", "EMPID", true);
                    //btnSearch.Visible = false;
                    imgBtnSearch.Visible = false;
                    txtEmpId.Enabled = false;
                    pnlEmpList.Visible = true;
                }
                else
                {
                    txtEmpId.Text = "";
                    //btnSearch.Visible = true;
                    imgBtnSearch.Visible = true;
                    txtEmpId.Enabled = true;
                    // grEmpList.DataSource = null;
                    //grEmpList.DataBind();
                    pnlEmpList.Visible = false;
                    //pnlEmpList.Visible = false;
                }
            }
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        dtLeaveApp.Dispose();
        dtLeaveType.Dispose();
        dtAppType.Dispose();
        dtResponsePerson.Dispose();
        dtEmpInfo.Dispose();
        grLeaveApp.DataSource = null;
        grLeaveApp.DataBind();
    }

    private void OpenRecord()
    {
        txtAppDate.Text = DateTime.Now.Day + " /" + DateTime.Now.Month + " /" + DateTime.Now.Year;
        this.EntryMode(false);

        string strStartYear = DateTime.Now.Year.ToString();
        string strStartMonth = DateTime.Now.Month.ToString();

        if (Convert.ToInt32(strStartMonth) >= 1)
        {
            strStartDate = Convert.ToString(Convert.ToInt32(strStartYear) - 1);
            strEndDate = Convert.ToString(Convert.ToInt32(strStartDate) + 1);
            strStartDate = strStartDate + "-" + "07" + "-" + "01";
            strEndDate = strEndDate + "-" + "12" + "-" + "31";
        }

        if (txtEmpId.Text.Trim() != "")
            dtLeaveApp = objLeaveMgr.SelectRequestDenyLeaveAppMst(0, txtEmpId.Text.Trim(), "A", strStartDate, strEndDate, "");
        else
            dtLeaveApp = objLeaveMgr.SelectRequestDenyLeaveAppMst(0, "N", "A", strStartDate, strEndDate, "");
        grLeaveApp.DataSource = dtLeaveApp;
        grLeaveApp.DataBind();
        this.FormatGridDate();
    }
    protected void FormatGridDate()
    {
        int SlNo = 0;
        if (grLeaveApp.Rows.Count > 0)
        {
            foreach (GridViewRow gRow in grLeaveApp.Rows)
            {
                SlNo = SlNo + 1;
                gRow.Cells[1].Text = SlNo.ToString();
                gRow.Cells[4].Text = Common.DisplayDate(gRow.Cells[4].Text);
                gRow.Cells[5].Text = Common.DisplayDate(gRow.Cells[5].Text);
                gRow.Cells[6].Text = Common.DisplayDate(gRow.Cells[6].Text);
                gRow.Cells[7].Text = Convert.ToString(Math.Round(Convert.ToDouble(gRow.Cells[7].Text), 1));
            }
        }
        SlNo = 0;
    }

    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
        {
            btnSave.Text = "Re-Apply";
            btnCancel.Enabled = true;
            hfIsUpadate.Value = "Y";
        }
        else
        {
            btnSave.Text = "Apply";
            btnCancel.Enabled = false;
            hfIsUpadate.Value = "N";
            //lblName.Text = "";
            //lblEmpType.Text = "";
            //lblDept.Text = "";
            //lblDesig.Text = "";
            //lblSupervisor.Text = ""; 
            txtLeaveReason.Text = "";
            txtLeaveAdd.Text = "";
            txtPhone.Text = "";
            ddlLeaveType.SelectedIndex = -1;
            LAv.Text = "";
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtLDurInDays.Text = "";
            txtResumeOn.Text = "";
            lblMsg.Text = "";
            lblMsg2.Text = "";
            ddlResPerson.SelectedIndex = -1;
            grLeaveStatus.DataSource = null;
            grLeaveStatus.DataBind();
            grLeaveApp.DataSource = null;
            grLeaveApp.DataBind();
            txtAppDate.Text = DateTime.Now.Day + " /" + DateTime.Now.Month + " /" + DateTime.Now.Year;
            this.TabContainer1.ActiveTabIndex = 0;
            this.UnCheckedCopyToConcern();
        }
        LAv.Text = "";
    }

    private void UnCheckedCopyToConcern()
    {
        foreach (GridViewRow gRow in grConcernPerson.Rows)
        {
            CheckBox chk = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            chk.Checked = false;
        }
    }

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        if (hfIsUpadate.Value == "Y" && hfPreLTypeId.Value == ddlLeaveType.SelectedValue.ToString())
        {
            this.AvailableLeave();
            if (LAv.Text != "" && txtLDurInDays.Text != "" && (hfLTypeNature.Value != "5") && (hfLTypeNature.Value != "6"))
                LAv.Text = Convert.ToString(Convert.ToDouble(LAv.Text));
        }
        if ((hfIsOffdayCounted.Value.ToString() == "Y") || (hfLAbbrName.Value.ToString() == "ML"))
            this.Get_LeaveDate_With_Weekend_Holiday();
        else
            this.Get_LeaveDate_WithOut_Weekend_Holiday("A");
    }
    protected void Get_LeaveDate_WithOut_Weekend_Holiday(string strGridView)
    {
        HiddenField hfLeaveDates = new HiddenField();
        HiddenField hfWeeekendDay = new HiddenField();
        HiddenField hfHoliDay = new HiddenField();

        double TotDay = 0;
        DateTime dtFrom = new DateTime();
        DateTime dtTo = new DateTime();
        hfLDates.Value = "";
        if (string.IsNullOrEmpty(txtFromDate.Text) == true)
        {
            lblMsg2.Text = "Please insert valid start date";
            return;
        }
        if (string.IsNullOrEmpty(txtToDate.Text) == true)
        {
            lblMsg2.Text = "Please insert valid end date";
            return;
        }

        if (string.IsNullOrEmpty(txtResumeOn.Text) == true)
        {
            lblMsg2.Text = "Please insert valid office resume date.";
            return;
        }
        if (string.IsNullOrEmpty(txtToDate.Text) == false && string.IsNullOrEmpty(txtFromDate.Text) == false)
        {
            char[] splitter ={ '/' };
            string[] arinfo = Common.str_split(txtFromDate.Text.Trim(), splitter);
            if (arinfo.Length == 3)
            {
                dtFrom = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                arinfo = null;
            }
            arinfo = Common.str_split(txtToDate.Text.Trim(), splitter);
            if (arinfo.Length == 3)
            {
                dtTo = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                arinfo = null;
            }

            TimeSpan Dur = dtTo.Subtract(dtFrom);

            TotDay = Math.Round(Convert.ToDouble(Dur.Days), 0) + 1;
            if (TotDay < 0)
            {
                lblMsg2.Text = "Start Date can not be greater than end date.";
                return;
            }
        }

        DataTable dtEmpWeekend = new DataTable();
        dtEmpWeekend = objLeaveMgr.SelectEmpWiseWeekend(txtEmpId.Text.Trim());

        DataTable dtHoliDay = new DataTable();

        DateTime LDate = dtFrom;
        int row;
        int LeaveDay = 0;
        dblTotWeekedDay = 0;
        dblTotHoliDay = 0;
        hfLeaveDates.Value = "";

        int i = 0;
        for (i = 0; i < Convert.ToInt32(TotDay); i++)
        {
            //Check for HoliDay
            dtHoliDay.Rows.Clear();
            dtHoliDay.Dispose();
            //dtHoliDay = objLeaveMgr.CheckLvDateBetweenHoliDate(Common.ReturnDateFormat_ddmmyyyy(LDate.ToString(), false),
            //    Common.ReturnDateFormat_ddmmyyyy(LDate.ToString(), true), DateTime.Now.Year.ToString());

            dtHoliDay = objLeaveMgr.CheckLvDateWithHoliDate(Common.SetDate(LDate.ToString()),
                Common.SetDate(LDate.ToString()), DateTime.Now.Year.ToString());

            if (dtHoliDay.Rows.Count > 0)
            {
                if (Common.ReturnDate(dtHoliDay.Rows[0]["HoliDate"].ToString()) != Common.ReturnDate((LDate.ToString())))
                    hfLeaveDates.Value = hfLeaveDates.Value + LDate.ToString();
                else
                {
                    if (hfHoliDay.Value != "")
                        hfHoliDay.Value = hfHoliDay.Value + ", " + Common.DisplayDate(dtHoliDay.Rows[0]["HoliDate"].ToString());
                    else
                        hfHoliDay.Value = Common.DisplayDate(dtHoliDay.Rows[0]["HoliDate"].ToString());
                    dblTotHoliDay++;
                }

            }
            //Check for weekend
            else if (dtEmpWeekend.Rows.Count > 0)
            {
                string DayName = LDate.DayOfWeek.ToString();
                switch (DayName)
                {
                    case "Sunday":
                        {
                            if (dtEmpWeekend.Rows[0]["WESun"].ToString() == "N")
                            {
                                LeaveDay = LeaveDay + 1;
                                if (hfLeaveDates.Value != "")
                                    hfLeaveDates.Value = hfLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                                else
                                    hfLeaveDates.Value = Common.SetDate(LDate.ToString());
                                break;
                            }
                            else
                            {
                                LDate = LDate.AddDays(1);
                                if (hfWeeekendDay.Value == "")
                                    hfWeeekendDay.Value = "Sunday";
                                else
                                    hfWeeekendDay.Value = hfWeeekendDay.Value + ", Sunday";
                                dblTotWeekedDay++;
                                continue;
                            }
                        }
                    case "Monday":
                        {
                            if (dtEmpWeekend.Rows[0]["WEMon"].ToString() == "N")
                            {
                                LeaveDay = LeaveDay + 1;
                                if (hfLeaveDates.Value != "")
                                    hfLeaveDates.Value = hfLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                                else
                                    hfLeaveDates.Value = Common.SetDate(LDate.ToString());
                                break;
                            }
                            else
                            {
                                LDate = LDate.AddDays(1);
                                if (hfWeeekendDay.Value == "")
                                    hfWeeekendDay.Value = "Monday";
                                else
                                    hfWeeekendDay.Value = hfWeeekendDay.Value + ", Monday";
                                dblTotWeekedDay++;
                                continue;
                            }
                        }
                    case "Tuesday":
                        {
                            if (dtEmpWeekend.Rows[0]["WETues"].ToString() == "N")
                            {
                                LeaveDay = LeaveDay + 1;
                                if (hfLeaveDates.Value != "")
                                    hfLeaveDates.Value = hfLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                                else
                                    hfLeaveDates.Value = Common.SetDate(LDate.ToString());
                                break;
                            }
                            else
                            {
                                LDate = LDate.AddDays(1);
                                if (hfWeeekendDay.Value == "")
                                    hfWeeekendDay.Value = "Tuesday";
                                else
                                    hfWeeekendDay.Value = hfWeeekendDay.Value + ", Tuesday";
                                dblTotWeekedDay++;
                                continue;
                            }
                        }
                    case "Wednesday":
                        {
                            if (dtEmpWeekend.Rows[0]["WEWed"].ToString() == "N")
                            {
                                LeaveDay = LeaveDay + 1;
                                if (hfLeaveDates.Value != "")
                                    hfLeaveDates.Value = hfLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                                else
                                    hfLeaveDates.Value = Common.SetDate(LDate.ToString());
                                break;
                            }
                            else
                            {
                                LDate = LDate.AddDays(1);
                                if (hfWeeekendDay.Value == "")
                                    hfWeeekendDay.Value = "Wednesday";
                                else
                                    hfWeeekendDay.Value = hfWeeekendDay.Value + ", Wednesday";
                                dblTotWeekedDay++;
                                continue;
                            }
                        }
                    case "Thursday":
                        {
                            if (dtEmpWeekend.Rows[0]["WETue"].ToString() == "N")
                            {
                                LeaveDay = LeaveDay + 1;
                                if (hfLeaveDates.Value != "")
                                    hfLeaveDates.Value = hfLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                                else
                                    hfLeaveDates.Value = Common.SetDate(LDate.ToString());
                                break;
                            }
                            else
                            {
                                LDate = LDate.AddDays(1);
                                if (hfWeeekendDay.Value == "")
                                    hfWeeekendDay.Value = "Thursday";
                                else
                                    hfWeeekendDay.Value = hfWeeekendDay.Value + ", Thursday";
                                dblTotWeekedDay++;
                                continue;
                            }
                        }
                    case "Friday":
                        {
                            if (dtEmpWeekend.Rows[0]["WEFri"].ToString() == "N")
                            {
                                LeaveDay = LeaveDay + 1;
                                if (hfLeaveDates.Value != "")
                                    hfLeaveDates.Value = hfLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                                else
                                    hfLeaveDates.Value = Common.SetDate(LDate.ToString());
                                break;
                            }
                            else
                            {
                                LDate = LDate.AddDays(1);
                                if (hfWeeekendDay.Value == "")
                                    hfWeeekendDay.Value = "Friday";
                                else
                                    hfWeeekendDay.Value = hfWeeekendDay.Value + ", Friday";
                                dblTotWeekedDay++;
                                continue;
                            }
                        }
                    case "Saturday":
                        {
                            if (dtEmpWeekend.Rows[0]["WESat"].ToString() == "N")
                            {
                                LeaveDay = LeaveDay + 1;
                                if (hfLeaveDates.Value != "")
                                    hfLeaveDates.Value = hfLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                                else
                                    hfLeaveDates.Value = Common.SetDate(LDate.ToString());
                                break;
                            }
                            else
                            {
                                LDate = LDate.AddDays(1);
                                if (hfWeeekendDay.Value == "")
                                    hfWeeekendDay.Value = "Saturday";
                                else
                                    hfWeeekendDay.Value = hfWeeekendDay.Value + ", Saturday";
                                dblTotWeekedDay++;
                                continue;
                            }
                        }
                }
            }
            LDate = LDate.AddDays(1);
        }

        hfLDates.Value = hfLeaveDates.Value;
        if (hfWeeekendDay.Value != "")
            lblMsg2.Text = hfWeeekendDay.Value + " Is Weekend";
        else
            lblMsg2.Text = "";

        if (hfHoliDay.Value != "")
            lblMsg3.Text = hfHoliDay.Value + " Is Holiday";
        else
            lblMsg3.Text = "";

        if (ddlHalfDay.SelectedValue != "0")
            TotDay = TotDay - 0.5;

        TotDay = TotDay - dblTotWeekedDay - dblTotHoliDay;
        txtLDurInDays.Text = TotDay.ToString();
        lblLDurInDays.Visible = true;
        hfWeeekendDay.Value = "";
        hfHoliDay.Value = "";
    }

    protected void Get_LeaveDate_With_Weekend_Holiday()
    {
        HiddenField hfLeaveDates = new HiddenField();
        HiddenField hfWeeekendDay = new HiddenField();

        double TotDay = 0;
        DateTime dtFrom = new DateTime();
        DateTime dtTo = new DateTime();

        if (string.IsNullOrEmpty(txtFromDate.Text) == true)
        {
            lblMsg2.Text = "Please insert valid start date";
            return;
        }
        if (string.IsNullOrEmpty(txtToDate.Text) == true)
        {
            lblMsg2.Text = "Please insert valid end date";
            return;
        }
        if (string.IsNullOrEmpty(txtResumeOn.Text) == true)
        {
            lblMsg2.Text = "Please insert valid office resume date.";
            return;
        }

        if (string.IsNullOrEmpty(txtToDate.Text) == false && string.IsNullOrEmpty(txtFromDate.Text) == false)
        {
            char[] splitter ={ '/' };
            string[] arinfo = Common.str_split(txtFromDate.Text.Trim(), splitter);
            if (arinfo.Length == 3)
            {
                dtFrom = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                arinfo = null;
            }
            arinfo = Common.str_split(txtToDate.Text.Trim(), splitter);
            if (arinfo.Length == 3)
            {
                dtTo = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                arinfo = null;
            }

            TimeSpan Dur = dtTo.Subtract(dtFrom);

            TotDay = Math.Round(Convert.ToDouble(Dur.Days), 0) + 1;
            if (TotDay < 0)
            {
                lblMsg2.Text = "Start Date can not be greater than end date.";
                return;
            }
        }

        DataTable dtEmpWeekend = new DataTable();
        dtEmpWeekend = objLeaveMgr.SelectEmpWiseWeekend(txtEmpId.Text.Trim());
        DateTime LDate = dtFrom;
        int row;
        int LeaveDay = 0;
        hfLeaveDates.Value = "";
        for (row = 0; row < Convert.ToInt32(TotDay); row++)
        {
            if (dtEmpWeekend.Rows.Count > 0)
            {
                string DayName = LDate.DayOfWeek.ToString();
                switch (DayName)
                {
                    case "Sunday":
                        {
                            LeaveDay = LeaveDay + 1;
                            if (hfLeaveDates.Value != "")
                                hfLeaveDates.Value = hfLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                            else
                                hfLeaveDates.Value = Common.SetDate(LDate.ToString());

                            if (dtEmpWeekend.Rows[0]["WESun"].ToString() == "Y")
                            {
                                if (hfWeeekendDay.Value == "")
                                    hfWeeekendDay.Value = "Sunday";
                                else
                                    hfWeeekendDay.Value = hfWeeekendDay.Value + ", Sunday";
                                dblTotWeekedDay++;
                            }
                            break;
                        }
                    case "Monday":
                        {
                            LeaveDay = LeaveDay + 1;
                            if (hfLeaveDates.Value != "")
                                hfLeaveDates.Value = hfLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                            else
                                hfLeaveDates.Value = Common.SetDate(LDate.ToString());

                            if (dtEmpWeekend.Rows[0]["WEMon"].ToString() == "Y")
                            {
                                if (hfWeeekendDay.Value == "")
                                    hfWeeekendDay.Value = "Monday";
                                else
                                    hfWeeekendDay.Value = hfWeeekendDay.Value + ", Monday";
                                dblTotWeekedDay++;
                            }
                            break;
                        }
                    case "Tuesday":
                        {
                            LeaveDay = LeaveDay + 1;
                            if (hfLeaveDates.Value != "")
                                hfLeaveDates.Value = hfLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                            else
                                hfLeaveDates.Value = Common.SetDate(LDate.ToString());
                            if (dtEmpWeekend.Rows[0]["WETues"].ToString() == "Y")
                            {
                                LDate = LDate.AddDays(1);
                                if (hfWeeekendDay.Value == "")
                                    hfWeeekendDay.Value = "Tuesday";
                                else
                                    hfWeeekendDay.Value = hfWeeekendDay.Value + ", Tuesday";
                                dblTotWeekedDay++;
                            }
                            break;
                        }
                    case "Wednesday":
                        {
                            LeaveDay = LeaveDay + 1;
                            if (hfLeaveDates.Value != "")
                                hfLeaveDates.Value = hfLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                            else
                                hfLeaveDates.Value = Common.SetDate(LDate.ToString());
                            if (dtEmpWeekend.Rows[0]["WEWed"].ToString() == "Y")
                            {
                                if (hfWeeekendDay.Value == "")
                                    hfWeeekendDay.Value = "Wednesday";
                                else
                                    hfWeeekendDay.Value = hfWeeekendDay.Value + ", Wednesday";
                                dblTotWeekedDay++;
                            }
                            break;
                        }
                    case "Thursday":
                        {
                            LeaveDay = LeaveDay + 1;
                            if (hfLeaveDates.Value != "")
                                hfLeaveDates.Value = hfLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                            else
                                hfLeaveDates.Value = Common.SetDate(LDate.ToString());
                            if (dtEmpWeekend.Rows[0]["WETue"].ToString() == "Y")
                            {
                                if (hfWeeekendDay.Value == "")
                                    hfWeeekendDay.Value = "Thursday";
                                else
                                    hfWeeekendDay.Value = hfWeeekendDay.Value + ", Thursday";
                                dblTotWeekedDay++;
                            }
                            break;
                        }
                    case "Friday":
                        {
                            LeaveDay = LeaveDay + 1;
                            if (hfLeaveDates.Value != "")
                                hfLeaveDates.Value = hfLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                            else
                                hfLeaveDates.Value = Common.SetDate(LDate.ToString());

                            if (dtEmpWeekend.Rows[0]["WEFri"].ToString() == "Y")
                            {
                                if (hfWeeekendDay.Value == "")
                                    hfWeeekendDay.Value = "Friday";
                                else
                                    hfWeeekendDay.Value = hfWeeekendDay.Value + ", Friday";
                                dblTotWeekedDay++;
                            }
                            break;
                        }
                    case "Saturday":
                        {

                            if (hfLeaveDates.Value != "")
                                hfLeaveDates.Value = hfLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                            else
                                hfLeaveDates.Value = Common.SetDate(LDate.ToString());

                            if (dtEmpWeekend.Rows[0]["WESat"].ToString() == "Y")
                            {
                                if (hfWeeekendDay.Value == "")
                                    hfWeeekendDay.Value = "Saturday";
                                else
                                    hfWeeekendDay.Value = hfWeeekendDay.Value + ", Saturday";
                                dblTotWeekedDay++;
                            }
                            break;
                        }
                    //LDate = LDate.AddDays(1);
                }
                LDate = LDate.AddDays(1);
            }
        }

        hfLDates.Value = hfLeaveDates.Value;
        if (hfWeeekendDay.Value != "")
            lblMsg2.Text = hfWeeekendDay.Value + " Is Weekend";
        else
            lblMsg2.Text = "";

        if (ddlHalfDay.SelectedValue != "0")
            TotDay = TotDay - 0.5;

        txtLDurInDays.Text = TotDay.ToString();
        lblLDurInDays.Visible = true;
    }

    protected void Get_Pre_LeaveDate_WithOut_Weekend_Holiday(string strGridView)
    {
        HiddenField hfPreLeaveDates = new HiddenField();
        HiddenField hfPreWeeekendDay = new HiddenField();

        double TotDay = 0;
        DateTime dtFrom = new DateTime();
        DateTime dtTo = new DateTime();

        if (string.IsNullOrEmpty(txtFromDate.Text) == true)
        {
            lblMsg2.Text = "Please insert valid start date";
            return;
        }
        if (string.IsNullOrEmpty(txtToDate.Text) == true)
        {
            lblMsg2.Text = "Please insert valid end date";
            return;
        }

        if (string.IsNullOrEmpty(txtToDate.Text) == false && string.IsNullOrEmpty(txtFromDate.Text) == false)
        {
            char[] splitter ={ '/' };
            string[] arinfo = Common.str_split(txtFromDate.Text.Trim(), splitter);
            if (arinfo.Length == 3)
            {
                dtFrom = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                arinfo = null;
            }
            arinfo = Common.str_split(txtToDate.Text.Trim(), splitter);
            if (arinfo.Length == 3)
            {
                dtTo = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                arinfo = null;
            }

            TimeSpan Dur = dtTo.Subtract(dtFrom);

            TotDay = Math.Round(Convert.ToDouble(Dur.Days), 0) + 1;
        }

        DataTable dtEmpWeekend = new DataTable();
        dtEmpWeekend = objLeaveMgr.SelectEmpWiseWeekend(txtEmpId.Text.Trim());

        DataTable dtHoliDay = new DataTable();
        //dtHoliDay = objLeaveMgr.CheckLvDateBetweenHoliDate(Common.ReturnDateFormat_ddmmyyyy(txtFromDate.Text.Trim(), false),
        //    Common.ReturnDateFormat_ddmmyyyy(txtToDate.Text.Trim(), true), DateTime.Now.Year.ToString());

        DateTime LDate = dtFrom;
        int row;
        int LeaveDay = 0;
        dblTotWeekedDay = 0;
        dblTotHoliDay = 0;
        hfPreLeaveDates.Value = "";

        int i = 0;
        for (i = 0; i < Convert.ToInt32(TotDay); i++)
        {
            //Check for HoliDay
            dtHoliDay.Rows.Clear();
            dtHoliDay.Dispose();
            dtHoliDay = objLeaveMgr.CheckLvDateWithHoliDate(Common.SetDate(LDate.ToString()),
                Common.SetDate(LDate.ToString()), DateTime.Now.Year.ToString());

            if (dtHoliDay.Rows.Count > 0)
            {
                if (Common.SetDate(dtHoliDay.Rows[0]["HoliDate"].ToString()) != Common.SetDate((LDate.ToString())))
                    hfPreLeaveDates.Value = hfPreLeaveDates.Value + LDate.ToString();
                else
                    dblTotHoliDay++;
            }
            //Check for weekend
            else if (dtEmpWeekend.Rows.Count > 0)
            {
                string DayName = LDate.DayOfWeek.ToString();
                switch (DayName)
                {
                    case "Sunday":
                        {
                            if (dtEmpWeekend.Rows[0]["WESun"].ToString() == "N")
                            {
                                LeaveDay = LeaveDay + 1;
                                if (hfPreLeaveDates.Value != "")
                                    hfPreLeaveDates.Value = hfPreLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                                else
                                    hfPreLeaveDates.Value = Common.SetDate(LDate.ToString());
                                break;
                            }
                            else
                            {
                                LDate = LDate.AddDays(1);
                                if (hfPreWeeekendDay.Value == "")
                                    hfPreWeeekendDay.Value = "Sunday";
                                else
                                    hfPreWeeekendDay.Value = hfPreWeeekendDay.Value + ", Sunday";
                                dblTotWeekedDay++;
                                continue;
                            }
                        }
                    case "Monday":
                        {
                            if (dtEmpWeekend.Rows[0]["WEMon"].ToString() == "N")
                            {
                                LeaveDay = LeaveDay + 1;
                                if (hfPreLeaveDates.Value != "")
                                    hfPreLeaveDates.Value = hfPreLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                                else
                                    hfPreLeaveDates.Value = Common.SetDate(LDate.ToString());
                                break;
                            }
                            else
                            {
                                LDate = LDate.AddDays(1);
                                if (hfPreWeeekendDay.Value == "")
                                    hfPreWeeekendDay.Value = "Monday";
                                else
                                    hfPreWeeekendDay.Value = hfPreWeeekendDay.Value + ", Monday";
                                dblTotWeekedDay++;
                                continue;
                            }
                        }
                    case "Tuesday":
                        {
                            if (dtEmpWeekend.Rows[0]["WETues"].ToString() == "N")
                            {
                                LeaveDay = LeaveDay + 1;
                                if (hfPreLeaveDates.Value != "")
                                    hfPreLeaveDates.Value = hfPreLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                                else
                                    hfPreLeaveDates.Value = Common.SetDate(LDate.ToString());
                                break;
                            }
                            else
                            {
                                LDate = LDate.AddDays(1);
                                if (hfPreWeeekendDay.Value == "")
                                    hfPreWeeekendDay.Value = "Tuesday";
                                else
                                    hfPreWeeekendDay.Value = hfPreWeeekendDay.Value + ", Tuesday";
                                dblTotWeekedDay++;
                                continue;
                            }
                        }
                    case "Wednesday":
                        {
                            if (dtEmpWeekend.Rows[0]["WEWed"].ToString() == "N")
                            {
                                LeaveDay = LeaveDay + 1;
                                if (hfPreLeaveDates.Value != "")
                                    hfPreLeaveDates.Value = hfPreLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                                else
                                    hfPreLeaveDates.Value = Common.SetDate(LDate.ToString());
                                break;
                            }
                            else
                            {
                                LDate = LDate.AddDays(1);
                                if (hfPreWeeekendDay.Value == "")
                                    hfPreWeeekendDay.Value = "Wednesday";
                                else
                                    hfPreWeeekendDay.Value = hfPreWeeekendDay.Value + ", Wednesday";
                                dblTotWeekedDay++;
                                continue;
                            }
                        }
                    case "Thursday":
                        {
                            if (dtEmpWeekend.Rows[0]["WETue"].ToString() == "N")
                            {
                                LeaveDay = LeaveDay + 1;
                                if (hfPreLeaveDates.Value != "")
                                    hfPreLeaveDates.Value = hfPreLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                                else
                                    hfPreLeaveDates.Value = Common.SetDate(LDate.ToString());
                                break;
                            }
                            else
                            {
                                LDate = LDate.AddDays(1);
                                if (hfPreWeeekendDay.Value == "")
                                    hfPreWeeekendDay.Value = "Thursday";
                                else
                                    hfPreWeeekendDay.Value = hfPreWeeekendDay.Value + ", Thursday";
                                dblTotWeekedDay++;
                                continue;
                            }
                        }
                    case "Friday":
                        {
                            if (dtEmpWeekend.Rows[0]["WEFri"].ToString() == "N")
                            {
                                LeaveDay = LeaveDay + 1;
                                if (hfPreLeaveDates.Value != "")
                                    hfPreLeaveDates.Value = hfPreLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                                else
                                    hfPreLeaveDates.Value = Common.SetDate(LDate.ToString());
                                break;
                            }
                            else
                            {
                                LDate = LDate.AddDays(1);
                                if (hfPreWeeekendDay.Value == "")
                                    hfPreWeeekendDay.Value = "Friday";
                                else
                                    hfPreWeeekendDay.Value = hfPreWeeekendDay.Value + ", Friday";
                                dblTotWeekedDay++;
                                continue;
                            }
                        }
                    case "Saturday":
                        {
                            if (dtEmpWeekend.Rows[0]["WESat"].ToString() == "N")
                            {
                                LeaveDay = LeaveDay + 1;
                                if (hfPreLeaveDates.Value != "")
                                    hfPreLeaveDates.Value = hfPreLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                                else
                                    hfPreLeaveDates.Value = Common.SetDate(LDate.ToString());
                                break;
                            }
                            else
                            {
                                LDate = LDate.AddDays(1);
                                if (hfPreWeeekendDay.Value == "")
                                    hfPreWeeekendDay.Value = "Saturday";
                                else
                                    hfPreWeeekendDay.Value = hfPreWeeekendDay.Value + ", Saturday";
                                dblTotWeekedDay++;
                                continue;
                            }
                        }
                }
                LDate = LDate.AddDays(1);
            }
            LDate = LDate.AddDays(1);
        }

        hfPreLDates.Value = hfPreLeaveDates.Value;
        if (hfPreWeeekendDay.Value != "")
            lblMsg2.Text = hfPreWeeekendDay.Value + " Is Weekend";
        else
            lblMsg2.Text = "";


        if (ddlHalfDay.SelectedValue != "0")
            TotDay = TotDay - 0.5;

        TotDay = TotDay - dblTotWeekedDay - dblTotHoliDay;
        txtLDurInDays.Text = TotDay.ToString();
        lblLDurInDays.Visible = true;
    }

    protected void Get_Pre_LeaveDate_With_Weekend_Holiday()
    {
        HiddenField hfPreLeaveDates = new HiddenField();
        HiddenField hfPreWeeekendDay = new HiddenField();

        double TotDay = 0;
        DateTime dtFrom = new DateTime();
        DateTime dtTo = new DateTime();

        if (string.IsNullOrEmpty(txtFromDate.Text) == true)
        {
            lblMsg2.Text = "Please insert valid start date";
            return;
        }
        if (string.IsNullOrEmpty(txtToDate.Text) == true)
        {
            lblMsg2.Text = "Please insert valid end date";
            return;
        }

        if (string.IsNullOrEmpty(txtToDate.Text) == false && string.IsNullOrEmpty(txtFromDate.Text) == false)
        {
            char[] splitter ={ '/' };
            string[] arinfo = Common.str_split(txtFromDate.Text.Trim(), splitter);
            if (arinfo.Length == 3)
            {
                dtFrom = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                arinfo = null;
            }
            arinfo = Common.str_split(txtToDate.Text.Trim(), splitter);
            if (arinfo.Length == 3)
            {
                dtTo = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                arinfo = null;
            }

            TimeSpan Dur = dtTo.Subtract(dtFrom);

            TotDay = Math.Round(Convert.ToDouble(Dur.Days), 0) + 1;
            if (TotDay < 0)
            {
                lblMsg2.Text = "Start Date can not be greater than end date.";
                return;
            }
        }

        DataTable dtEmpWeekend = new DataTable();
        dtEmpWeekend = objLeaveMgr.SelectEmpWiseWeekend(txtEmpId.Text.Trim());
        DateTime LDate = dtFrom;
        int row;
        int LeaveDay = 0;
        hfPreLeaveDates.Value = "";
        for (row = 0; row < Convert.ToInt32(TotDay); row++)
        {
            if (dtEmpWeekend.Rows.Count > 0)
            {
                string DayName = LDate.DayOfWeek.ToString();
                switch (DayName)
                {
                    case "Sunday":
                        {
                            LeaveDay = LeaveDay + 1;
                            if (hfPreLeaveDates.Value != "")
                                hfPreLeaveDates.Value = hfPreLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                            else
                                hfPreLeaveDates.Value = Common.SetDate(LDate.ToString());

                            if (dtEmpWeekend.Rows[0]["WESun"].ToString() == "Y")
                            {
                                if (hfPreWeeekendDay.Value == "")
                                    hfPreWeeekendDay.Value = "Sunday";
                                else
                                    hfPreWeeekendDay.Value = hfPreWeeekendDay.Value + ", Sunday";
                                dblTotWeekedDay++;
                            }
                            break;
                        }
                    case "Monday":
                        {
                            LeaveDay = LeaveDay + 1;
                            if (hfPreLeaveDates.Value != "")
                                hfPreLeaveDates.Value = hfPreLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                            else
                                hfPreLeaveDates.Value = Common.SetDate(LDate.ToString());

                            if (dtEmpWeekend.Rows[0]["WEMon"].ToString() == "Y")
                            {
                                if (hfPreWeeekendDay.Value == "")
                                    hfPreWeeekendDay.Value = "Monday";
                                else
                                    hfPreWeeekendDay.Value = hfPreWeeekendDay.Value + ", Monday";
                                dblTotWeekedDay++;
                            }
                            break;
                        }
                    case "Tuesday":
                        {
                            LeaveDay = LeaveDay + 1;
                            if (hfPreLeaveDates.Value != "")
                                hfPreLeaveDates.Value = hfPreLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                            else
                                hfPreLeaveDates.Value = Common.SetDate(LDate.ToString());
                            if (dtEmpWeekend.Rows[0]["WETues"].ToString() == "Y")
                            {
                                LDate = LDate.AddDays(1);
                                if (hfPreWeeekendDay.Value == "")
                                    hfPreWeeekendDay.Value = "Tuesday";
                                else
                                    hfPreWeeekendDay.Value = hfPreWeeekendDay.Value + ", Tuesday";
                                dblTotWeekedDay++;
                            }
                            break;
                        }
                    case "Wednesday":
                        {
                            LeaveDay = LeaveDay + 1;
                            if (hfPreLeaveDates.Value != "")
                                hfPreLeaveDates.Value = hfPreLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                            else
                                hfPreLeaveDates.Value = Common.SetDate(LDate.ToString());
                            if (dtEmpWeekend.Rows[0]["WEWed"].ToString() == "Y")
                            {
                                if (hfPreWeeekendDay.Value == "")
                                    hfPreWeeekendDay.Value = "Wednesday";
                                else
                                    hfPreWeeekendDay.Value = hfPreWeeekendDay.Value + ", Wednesday";
                                dblTotWeekedDay++;
                            }
                            break;
                        }
                    case "Thursday":
                        {
                            LeaveDay = LeaveDay + 1;
                            if (hfPreLeaveDates.Value != "")
                                hfPreLeaveDates.Value = hfPreLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                            else
                                hfPreLeaveDates.Value = Common.SetDate(LDate.ToString());
                            if (dtEmpWeekend.Rows[0]["WETue"].ToString() == "Y")
                            {
                                if (hfPreWeeekendDay.Value == "")
                                    hfPreWeeekendDay.Value = "Thursday";
                                else
                                    hfPreWeeekendDay.Value = hfPreWeeekendDay.Value + ", Thursday";
                                dblTotWeekedDay++;
                            }
                            break;
                        }
                    case "Friday":
                        {
                            LeaveDay = LeaveDay + 1;
                            if (hfPreLeaveDates.Value != "")
                                hfPreLeaveDates.Value = hfPreLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                            else
                                hfPreLeaveDates.Value = Common.SetDate(LDate.ToString());

                            if (dtEmpWeekend.Rows[0]["WEFri"].ToString() == "Y")
                            {
                                if (hfPreWeeekendDay.Value == "")
                                    hfPreWeeekendDay.Value = "Friday";
                                else
                                    hfPreWeeekendDay.Value = hfPreWeeekendDay.Value + ", Friday";
                                dblTotWeekedDay++;
                            }
                            break;
                        }
                    case "Saturday":
                        {
                            if (hfPreLeaveDates.Value != "")
                                hfPreLeaveDates.Value = hfPreLeaveDates.Value + "," + Common.SetDate(LDate.ToString());
                            else
                                hfPreLeaveDates.Value = Common.SetDate(LDate.ToString());

                            if (dtEmpWeekend.Rows[0]["WESat"].ToString() == "Y")
                            {
                                if (hfPreWeeekendDay.Value == "")
                                    hfPreWeeekendDay.Value = "Saturday";
                                else
                                    hfPreWeeekendDay.Value = hfPreWeeekendDay.Value + ", Saturday";
                                dblTotWeekedDay++;
                            }
                            break;
                        }
                    //LDate = LDate.AddDays(1);
                }
                LDate = LDate.AddDays(1);
            }
        }

        hfPreLDates.Value = hfPreLeaveDates.Value;
        if (hfPreWeeekendDay.Value != "")
            lblMsg2.Text = hfPreWeeekendDay.Value + " Is Weekend";
        else
            lblMsg2.Text = "";

        if (ddlHalfDay.SelectedValue != "0")
            TotDay = TotDay - 0.5;

        txtLDurInDays.Text = TotDay.ToString();
        lblLDurInDays.Visible = true;
    }
   

    //protected void CalculateLeaveDates()
    //{
    //    double TotDay = 0;
    //    DateTime dtFrom = new DateTime();
    //    DateTime dtTo = new DateTime();
    //    if (string.IsNullOrEmpty(txtFromDate.Text) == true)
    //    {
    //        lblMsg2.Text = "Please insert valid start date";
    //        return;
    //    }
    //    if (string.IsNullOrEmpty(txtToDate.Text) == true)
    //    {
    //        lblMsg2.Text = "Please insert valid end date";
    //        return;
    //    }
    //    if (string.IsNullOrEmpty(txtToDate.Text) == false && string.IsNullOrEmpty(txtFromDate.Text) == false)
    //    {
    //        char[] splitter ={ '/' };
    //        string[] arinfo = Common.str_split(txtFromDate.Text.Trim(), splitter);
    //        if (arinfo.Length == 3)
    //        {
    //            dtFrom = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
    //            arinfo = null;
    //        }
    //        arinfo = Common.str_split(txtToDate.Text.Trim(), splitter);
    //        if (arinfo.Length == 3)
    //        {
    //            dtTo = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
    //            arinfo = null;
    //        }

    //        TimeSpan Dur = dtTo.Subtract(dtFrom);

    //        TotDay = Math.Round(Convert.ToDouble(Dur.Days), 0) + 1;
    //        if (TotDay < 0)
    //        {
    //            lblMsg2.Text = "Start Date can not be greater than end date.";
    //            return;
    //        }
    //    }
    //    DateTime LDate = dtFrom;
    //    int row = 0;
    //    int LeaveDay = 0;
    //    hfLDates.Value = "";
    //    for (row = 0; row < Convert.ToInt32(TotDay); row++)
    //    {
    //        if (hfLDates.Value != "")
    //        {
    //            hfLDates.Value = hfLDates.Value + ",";
    //        }
    //        LeaveDay = LeaveDay + 1;
    //        hfLDates.Value = hfLDates.Value + Common.SetDate(LDate.ToString());
    //        LDate = LDate.AddDays(1);
    //    }
    //    txtLDurInDays.Text = TotDay.ToString();
    //    lblLDurInDays.Visible = true;
        
    //}

    //protected void CalculatePreLeaveDates(string FromDate, string ToDate)
    //{
    //    double TotDay = 0;
    //    DateTime dtFrom = new DateTime();
    //    DateTime dtTo = new DateTime();

    //    char[] splitter ={ '/' };
    //    string[] arinfo = Common.str_split(FromDate, splitter);
    //    if (arinfo.Length == 3)
    //    {
    //        dtFrom = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
    //        arinfo = null;
    //    }
    //    arinfo = Common.str_split(ToDate, splitter);
    //    if (arinfo.Length == 3)
    //    {
    //        dtTo = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
    //        arinfo = null;
    //    }

    //    DateTime LDate = Convert.ToDateTime(dtFrom);
    //    int row = 0;
    //    hfPreLDates.Value = "";
    //    TimeSpan Dur = dtTo.Subtract(dtFrom);
    //    TotDay = Math.Round(Convert.ToDouble(Dur.Days), 0) + 1;

    //    for (row = 0; row < Convert.ToInt32(TotDay); row++)
    //    {
    //        if (hfPreLDates.Value != "")
    //        {
    //            hfPreLDates.Value = hfPreLDates.Value + ",";
    //        }
    //        hfPreLDates.Value = hfPreLDates.Value + Common.SetDate(LDate.ToString());
    //        LDate = LDate.AddDays(1);
    //    }
    //}

    private void Bind_DdlLeaveType()
    {
        Common.FillDropDownList(dtLeaveType, ddlLeaveType,true );
    }
    private void Bind_DdlResponsiblePerson()
    {
        Common.FillIdNameDropDownList_Nil(dtResponsePerson, ddlResPerson, "FullName", "EmpId", true);     
    }

    private void FillEmpInfo(string EmpId)
    {
        if (Session["USERID"].ToString().Trim().ToUpper() == "ADMIN")
            dtEmpInfo = objEmpInfoMgr.SelectEmpInfoOfficeWiseForLeave(txtEmpId.Text.Trim(), "-1");
        else if (Session["USERID"].ToString().Trim().ToUpper() != "ADMIN")
        {
            dtEmpInfo = objEmpInfoMgr.SelectEmpInfoOfficeWiseForLeave(txtEmpId.Text.Trim(), "-1");
        }

        if (dtEmpInfo.Rows.Count > 0)
        {
            this.OpenRecord();

            chkIsAllEmp.Checked = true;
            dtResponsePerson = objLeaveMgr.SelectDivisionWiseEmp(txtEmpId.Text.Trim());
            this.Bind_DdlResponsiblePerson();

            foreach (DataRow row in dtEmpInfo.Rows)
            {
                lblName.Text = row["FullName"].ToString().Trim() + ", " + row["JobTitle"].ToString().Trim()
                             + ", " + row["DivisionName"].ToString().Trim();
                lblEmpType.Text = row["LPackName"].ToString().Trim();
                //lblDept.Text = row["DivisionName"].ToString().Trim();
                // lblDesig.Text = row["JobTitle"].ToString().Trim();
                hfSupervisor.Value = row["SupervisorId"].ToString().Trim();
                DataTable dtSuper = new DataTable();
                if (hfSupervisor.Value != "")
                {
                    dtSuper = objEmpInfoMgr.SelectEmpInfoOfficeWiseForLeaveSPV(hfSupervisor.Value.ToString(), "-1");
                    if (dtSuper.Rows.Count > 0)
                    {
                        lblSupervisor.Text = dtSuper.Rows[0]["FullName"].ToString().Trim() + ", " + dtSuper.Rows[0]["DivisionName"].ToString().Trim()
                             + ", " + dtSuper.Rows[0]["JobTitle"].ToString().Trim();
                        hfSupervisorEmail.Value = dtSuper.Rows[0]["PersEmail1"].ToString().Trim();
                    }
                }
                else
                {
                    lblSupervisor.Text = "";
                }
                hfSex.Value = row["Sex"].ToString().Trim();
                hfLPakId.Value = row["LeavePakId"].ToString().Trim();
                hfIsOffdayCounted.Value = row["IsOffdayCounted"].ToString().Trim();
            }

            this.FillEmpWithLeaveInfo(txtEmpId.Text.Trim());

            dtLeaveType = objLeaveMgr.SelectEmpWiseLeaveType(0, txtEmpId.Text.Trim(), hfSex.Value.ToString());
            this.Bind_DdlLeaveType();
            lblMsg.Text = "";

            grConcernPerson.DataSource = null;
            grConcernPerson.DataBind();
            DataTable dtConcernPerson = new DataTable();
            dtConcernPerson.Rows.Clear();
            dtConcernPerson.Dispose();
            dtConcernPerson = objLeaveMgr.SelectDivisionWiseEmp(txtEmpId.Text.Trim());
            grConcernPerson.DataSource = dtConcernPerson;
            grConcernPerson.DataBind();
        }
        else
        {
            lblMsg.Text = "Employee number is not valid Or not under your office.";
            txtEmpId.Text = "";
            lblName.Text = "";
            lblDept.Text = "";
            lblDesig.Text = "";
            dtLeaveApp.Rows.Clear();
            dtLeaveApp.Dispose();
            grLeaveApp.DataSource = null;
            grLeaveApp.DataBind();
            grLeaveStatus.DataSource = null;
            grLeaveStatus.DataBind();
            return;
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        lblMsg2.Text = "";
        txtEmpId.Text = "";
        lblName.Text = "";
        lblEmpType.Text = ""; 
        lblDept.Text = "";
        lblDesig.Text = "";        
        this.OpenRecord();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateAndSave() == true)
        {
            this.SaveData("N");
            //Don't Delete
            //Send Email Notification
            MailManager objMail = new MailManager();

            DateTime LvPackStDate = Convert.ToDateTime(hfLvPackStartDate.Value);
            DateTime LvPackEnDate = new DateTime();
            if (hfLvPackEndDate.Value != "")
                LvPackEnDate = Convert.ToDateTime(hfLvPackEndDate.Value);

            //lblMsg2.Text = objMail.RequestForApproval(txtEmpId.Text.Trim(), hfID.Value.ToString(),
            //    Common.SetDate(LvPackStDate.ToShortDateString()), hfLvPackEndDate.Value != "" ? Common.SetDate(LvPackEnDate.ToShortDateString()) : "",
            //    Session["EMPID"].ToString(), Session["USERNAME"].ToString(),
            //    Session["DESIGNATION"].ToString(), Session["LOCATION"].ToString(),
            //    Session["USERID"].ToString().Trim().ToUpper() == "ADMIN" ? "Y" : "N", hfSupervisor.Value.ToString(),
            //    hfSupervisorEmail.Value.ToString());

            lblMsg.Text = "Application is mailed to the supervisor";

            //this.SendEmailNotification(txtEmpId.Text.Trim());

            //Open New Window
            StringBuilder sb = new StringBuilder();
            string strURL = "LeaveApplicationRpt.aspx?params=" + txtEmpId.Text.Trim() + "," + hfID.Value.ToString() + ", R";
            sb.Append("<script>");
            //sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
            sb.Append("window.open('" + strURL + "', '', '');");
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                     sb.ToString(), false);
            ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
        }
    }

    protected bool ValidateAndSave()
    {
        try
        {
            long lngID = 0;
            if (hfIsUpadate.Value == "N")
            {
                lngID = objDB.GerMaxIDNumber("LeaveAppMst", "LvAppID");
                hfID.Value = lngID.ToString();
            }
            else
            {
                lngID = Convert.ToInt32(hfID.Value);
            }

            if (ddlLeaveType.SelectedIndex == 0)
            {
                lblMsg.Text = "Please select the leave type.";
                ddlLeaveType.Focus();
                return false;
            }            
            if (hfLTypeNature.Value != "5" && hfLTypeNature.Value != "6")
            {
                if (LAv.Text == "0")
                {
                    lblMsg.Text = "No leave is available for the leave type.";
                    return false;
                }
                if (string.IsNullOrEmpty(LAv.Text.Trim()) == false && string.IsNullOrEmpty(txtLDurInDays.Text.Trim()) == false)
                {
                    if (Convert.ToDouble(LAv.Text) < Convert.ToDouble(txtLDurInDays.Text))
                    {
                        lblMsg.Text = "Leave can not taken more than available leave.";
                        return false;
                    }
                }
            }
            if (string.IsNullOrEmpty(txtFromDate.Text) == true)
            {
                lblMsg.Text = "Please enter the leave start date duration.";
                return false;
            }
            if (string.IsNullOrEmpty(txtToDate.Text) == true)
            {
                lblMsg.Text = "Please enter the leave end date duration.";
                return false;
            }
            if (txtLDurInDays.Text == "")
            {
                lblMsg.Text = "Please press on calculate button.";
                return false;
            }

            //DataTable dtEmpAttnd = new DataTable();
            //int i = 0;
            //int j = 0;
            //string[] arinfo = new string[10];
            //char[] splitter = { ',' };
            //arinfo = Common.str_split(hfLDates.Value, splitter);
            //for (i = 0; i < arinfo.Length; i++)
            //{
            //    dtEmpAttnd = objLeaveMgr.SelectEmpWsAttnd(txtEmpId.Text.Trim(), arinfo[i].ToString());
            //    if (dtEmpAttnd.Rows.Count > 0)
            //    {
            //        for (j = 0; j < dtEmpAttnd.Rows.Count; j++)
            //        {
            //            if (dtEmpAttnd.Rows[j]["SignInTime"].ToString() != "" || dtEmpAttnd.Rows[j]["SignOutTime"].ToString() != "")
            //            {
            //                lblMsg.Text = "In time & out time is already assigned for this date. Leave is not applicable for this date.";
            //                return false;
            //            }
            //        }
            //    }
            //}
            //dtEmpAttnd.Rows.Clear();
            //dtEmpAttnd.Dispose();

            //Employees leave already existed in this leave dates
            DataTable dtLvExisted = new DataTable();
            string[] arinfo = new string[10];
            char[] splitter = { ',' };
            int i = 0;
            int j = 0;
            if(string.IsNullOrEmpty(hfLDates.Value.ToString())==false)
                arinfo = Common.str_split(hfLDates.Value, splitter);
            else
                arinfo = Common.str_split(hfPreLDates.Value, splitter);

            for (i = 0; i < arinfo.Length; i++)
            {
                if (string.IsNullOrEmpty(arinfo[i]) == false)
                {
                    dtLvExisted = objLeaveMgr.SelectEmpLeaveDateDetails(Convert.ToInt32(hfID.Value),
                        txtEmpId.Text.Trim(), arinfo[i].ToString());
                    if (dtLvExisted.Rows.Count > 0)
                    {
                        for (j = 0; j < dtLvExisted.Rows.Count; j++)
                        {
                            DateTime dtLevDate;
                            DateTime dtAllDate;
                            dtLevDate = Convert.ToDateTime(dtLvExisted.Rows[j]["LevDate"].ToString());
                            dtAllDate = Convert.ToDateTime(arinfo[i].ToString());
                            TimeSpan DateDiff = dtLevDate.Subtract(dtAllDate);
                            string strTotDay = Common.ReturnTotalDay(DateDiff.ToString());
                            if (strTotDay == "00:00:00")
                            {
                                lblMsg.Text = txtEmpId.Text.Trim() + " already applied for leave for the date " + Common.DisplayDate(arinfo[i].ToString()) + ".";
                                return false;
                            }
                        }
                    }
                }
            }
            dtLvExisted.Rows.Clear();
            dtLvExisted.Dispose();

            //Check Leave start date is in between leave period
            DataTable dtLvPeriod = objLeaveMgr.CheckLvDateBetweenLeavePeriod(hfLPakId.Value.ToString() , Common.ReturnDate(txtFromDate.Text.Trim()));
            if (dtLvPeriod.Rows.Count > 0)
            {
                lblMsg.Text = "Please renew leave period of " + txtEmpId.Text.Trim() + " Id's leave package.";
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    private void SaveData(string IsDelete)
    {
        try
        {
            double EDay = 0;
            if (hfIsUpadate.Value == "N")
            {
                EDay = Math.Round(Convert.ToDouble(hfLEnjoyed.Value)) + Convert.ToDouble(txtLDurInDays.Text.Trim());
                hfLEnjoyed.Value = Convert.ToString(EDay);
            }
            else
            {
                if (ddlLeaveType.SelectedValue == hfPreLTypeId.Value)
                {
                    EDay = Math.Round(Convert.ToDouble(hfLEnjoyed.Value) - Convert.ToDouble(hfPreLEnjoyed.Value)) + Convert.ToDouble(txtLDurInDays.Text.Trim());
                    hfLEnjoyed.Value = Convert.ToString(EDay);
                }
                else
                {
                    EDay = Math.Round(Convert.ToDouble(hfLEnjoyed.Value)) + Convert.ToDouble(txtLDurInDays.Text.Trim());
                    hfLEnjoyed.Value = Convert.ToString(EDay);

                    hfPreLEnjoyed.Value = hfPreLEnjoyed.Value;
                }
            }

            int CopyToCount = 0;
            foreach (GridViewRow gRow in grConcernPerson.Rows)
            {
                CheckBox chk = (CheckBox)gRow.Cells[0].FindControl("chkBox");
                if (chk.Checked == true)
                {
                    CopyToCount++;
                }
            }

            LeaveApp objLeave = new LeaveApp(hfID.Value.ToString(), txtEmpId.Text.Trim(), txtAppDate.Text, txtLeaveReason.Text.Trim(),
                txtLeaveAdd.Text.Trim(), txtPhone.Text.Trim(), txtLDurInDays.Text, "A", "R", Session["USERID"].ToString(),
                Common.SetDateTime(DateTime.Now.ToString()), hfIsUpadate.Value.ToString(), IsDelete, ddlLeaveType.SelectedValue.ToString(),
                txtFromDate.Text, txtToDate.Text, txtLDurInDays.Text, ddlResPerson.SelectedValue.ToString(), ddlHalfDay.SelectedValue.ToString(), txtResumeOn.Text.Trim(),"");

            if (hfIsUpadate.Value == "N")
                //objLeaveMgr.InsertLeaveAppMst(objLeave, hfIsUpadate.Value.ToString(), "N", "A", hfLEnjoyed.Value, hfLDates.Value.ToString(),
                //    hfLAbbrName.Value.ToString(), Convert.ToInt16(txtLDurInDays.Text), "", "", "", hfLTypeNature.Value.ToString(), "Y", grConcernPerson, CopyToCount);
                objLeaveMgr.InsertLeaveAppMst(objLeave, hfIsUpadate.Value.ToString(), "N", "A", hfLEnjoyed.Value, hfLDates.Value.ToString(),
                hfLAbbrName.Value.ToString(), Convert.ToInt16(txtLDurInDays.Text), "", "", "", hfLTypeNature.Value.ToString(), "Y");
            else
                //objLeaveMgr.InsertLeaveAppMst(objLeave, hfIsUpadate.Value.ToString(), "N", "A", hfLEnjoyed.Value, hfLDates.Value.ToString(),
                //hfLAbbrName.Value.ToString(), Convert.ToInt16(txtLDurInDays.Text), hfPreLTypeId.Value.ToString(), hfPreLEnjoyed.Value,
                //hfPreLDates.Value.ToString(), hfLTypeNature.Value.ToString(), "Y", grConcernPerson, CopyToCount);
                objLeaveMgr.InsertLeaveAppMst(objLeave, hfIsUpadate.Value.ToString(), "N", "A", hfLEnjoyed.Value, hfLDates.Value.ToString(),
                hfLAbbrName.Value.ToString(), Convert.ToInt16(txtLDurInDays.Text), hfPreLTypeId.Value.ToString(), hfPreLEnjoyed.Value,
                hfPreLDates.Value.ToString(), hfLTypeNature.Value.ToString(), "Y");

            if (hfIsUpadate.Value == "N")
                lblMsg.Text = "Record Saved Successfully";
            else
                lblMsg.Text = "Record Updated Successfully";

            this.EntryMode(false);
            this.OpenRecord();
            this.FillEmpWithLeaveInfo(txtEmpId.Text.Trim());
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfID.Value) == false)
        {
            this.SaveData("Y");
        }
        this.EntryMode(false);
    }

    protected void grLeaveApp_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void grLeaveApp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                hfID.Value = grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim();
                txtEmpId.Text = grLeaveApp.SelectedRow.Cells[2].Text.Trim();
                this.FillEmpInfo(txtEmpId.Text.Trim());
                txtAppDate.Text = grLeaveApp.SelectedRow.Cells[4].Text;
                ddlLeaveType.SelectedValue = grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[2].ToString().Trim();
                hfPreLTypeId.Value = ddlLeaveType.SelectedValue.ToString();
                txtFromDate.Text = grLeaveApp.SelectedRow.Cells[5].Text;
                txtToDate.Text = grLeaveApp.SelectedRow.Cells[6].Text;
                txtLDurInDays.Text = grLeaveApp.SelectedRow.Cells[7].Text;
                hfPreLEnjoyed.Value = txtLDurInDays.Text;
                txtLeaveReason.Text = grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[9].ToString().Trim();
                txtLeaveAdd.Text = grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[12].ToString().Trim();
                txtPhone.Text = grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[13].ToString().Trim();

                hfLTypeNature.Value = grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[3].ToString();
                if (string.IsNullOrEmpty(grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim()) == false)
                    ddlResPerson.SelectedValue = grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[11].ToString().Trim();
                ddlHalfDay.SelectedValue = grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[14].ToString().Trim();
                if (string.IsNullOrEmpty(grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[15].ToString().Trim()) == false)
                    txtResumeOn.Text = Common.DisplayDate(grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[15].ToString().Trim());
                this.PreAvailableLeave();

                //DataTable dtCopyTo = objLeaveMgr.SelectLeaveCopyTo(hfID.Value.ToString());
                //int i = 0;
                //if (dtCopyTo.Rows.Count > 0)
                //{
                //    foreach (GridViewRow gRow in grConcernPerson.Rows)
                //    {
                //        CheckBox chk = (CheckBox)gRow.Cells[0].FindControl("chkBox");
                //        if (Common.FindInDataTable(dtCopyTo, grConcernPerson.DataKeys[i].Values[0].ToString().Trim(), "CopyToId") == true)
                //        {
                //            chk.Checked = true;
                //        }
                //        i++;
                //    }
                //}

                this.EntryMode(true);
                this.TabContainer1.ActiveTabIndex = 0;
                this.GetLeaveTypeDependency();


                //this.Get_LeaveDate_WithOut_Weekend_Holiday("A"); 
                //this.CalculateLeaveDates();
                //this.CalculatePreLeaveDates(txtFromDate.Text, txtToDate.Text);


                if ((hfIsOffdayCounted.Value.ToString() == "Y") || (hfLAbbrName.Value.ToString() == "ML"))
                    this.Get_Pre_LeaveDate_With_Weekend_Holiday();
                else
                    this.Get_Pre_LeaveDate_WithOut_Weekend_Holiday("A");


                this.FillEmpWithLeaveInfo(txtEmpId.Text.Trim());


                break;
            case ("ViewClick"):
                //Open New Window
                StringBuilder sb = new StringBuilder();
                string strURL = "LeaveApplicationRpt.aspx?params=" + grLeaveApp.SelectedRow.Cells[2].Text.Trim() + "," + grLeaveApp.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim() + ", A";
                sb.Append("<script>");
                //sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
                sb.Append("window.open('" + strURL + "', '', '');");
                sb.Append("</script>");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                         sb.ToString(), false);
                ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());

                break;
        }
    }

    protected void SearchEmployee()
    {
        if (string.IsNullOrEmpty(txtEmpId.Text.Trim()) == false)
        {
            grLeaveStatus.DataSource = null;
            grLeaveStatus.DataBind();
            this.FillEmpInfo(txtEmpId.Text.Trim());            
        }
        else
        {
            this.EntryMode(false);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtEmpId.Text.Trim()) == false)
        {
            grLeaveStatus.DataSource = null;
            grLeaveStatus.DataBind();
            this.FillEmpInfo(txtEmpId.Text.Trim());
        }
        else
        {
            this.EntryMode(false);
        }

        //Don't Delete these lines.  
        //if (Session["ISADMIN"].ToString() == "Y")
        //{
        //if (string.IsNullOrEmpty(txtEmpId.Text.Trim()) == false)
        //{
        //    grLeaveStatus.DataSource = null;
        //    grLeaveStatus.DataBind();
        //    this.FillEmpInfo(txtEmpId.Text.Trim());
        //    //this.FillEmpWithLeaveInfo(txtEmpId.Text.Trim());
        //    //this.OpenRecord();
        //}
        //else
        //{
        //    this.EntryMode(false);
        //}
        //}
        //else
        //{
        //    if (Session["EMPLOYEEID"].ToString()  == txtEmpId.Text.Trim())
        //    {
        //        if (string.IsNullOrEmpty(txtEmpId.Text.Trim()) == false)
        //        {
        //            grLeaveStatus.DataSource = null;
        //            grLeaveStatus.DataBind();
        //            this.FillEmpInfo(txtEmpId.Text.Trim());
        //            //this.FillEmpWithLeaveInfo(txtEmpId.Text.Trim());
        //            //this.OpenRecord();
        //        }
        //    }
        //    else
        //    {
        //        lblMsg.Text = "Log in user is not admin user and Employee Emp No. is not matched with users Emp No.. ";
        //        this.EntryMode(false);
        //    }
        //}        
    }

    private void FillEmpWithLeaveInfo(string EmpId)
    {
        dtEmpLvProfile.Rows.Clear();
        dtEmpLvProfile.Dispose();
        dtEmpLvProfile = objLeaveMgr.SelectEmpLeaveProfileEXCPL(EmpId, "0", hfSex.Value.ToString());

        if (dtEmpLvProfile.Rows.Count > 0)
        {
            grLeaveStatus.DataSource = dtEmpLvProfile;
            grLeaveStatus.DataBind();
            this.FormatLeaveStatusGridNumber();
            this.GetLeaveYearDates(dtEmpLvProfile);
        }
    }

    // Employee Leave Year From and To Date
    protected void GetLeaveYearDates(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            if (string.IsNullOrEmpty(dt.Rows[0]["LeaveStartPeriod"].ToString()) == false)
            {
                hfLvPackStartDate.Value = dt.Rows[0]["LeaveStartPeriod"].ToString();
                hfLvPackEndDate.Value = dt.Rows[0]["LeaveEndPeriod"].ToString();
            }
            else
            {
                hfLvPackStartDate.Value = Common.ReturnDate(dt.Rows[0]["JoiningDate"].ToString());
                if (string.IsNullOrEmpty(dt.Rows[0]["LeavingDate"].ToString()) == false)
                    hfLvPackEndDate.Value = Common.ReturnDate(dt.Rows[0]["LeavingDate"].ToString());
            }
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
            gRow.Cells[5].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[5].Text)), 1) + Convert.ToDouble(grLeaveStatus.DataKeys[i].Values[8].ToString().Trim() == "" ? "0" : grLeaveStatus.DataKeys[i].Values[8].ToString().Trim()));
            gRow.Cells[6].Text = Convert.ToString(Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[4].Text)) + Math.Round(Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[1].Text))) - Convert.ToDouble(Common.ReturnZeroForNull(gRow.Cells[5].Text)), 1));

            if (Convert.ToDecimal(gRow.Cells[6].Text) < 0)
            {
                gRow.Cells[6].Text = "0";
            }
            i++;
        }
    }
    protected void ddlLeaveType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.GetLeaveTypeDependency();
    }

    private void GetLeaveTypeDependency()
    {
        LAv.Text = "";
        DataTable dtLType = new DataTable();
        dtLType = objLMgr.SelectLeaveType(Convert.ToInt32(ddlLeaveType.SelectedValue));
        if (dtLType.Rows.Count > 0)
        {
            hfLTypeNature.Value = dtLType.Rows[0]["LNature"].ToString();
        }
        if ((ddlLeaveType.SelectedValue != "-1"))
        {
            this.AvailableLeave();
        }
        DateTime dtCurrMonth = Convert.ToDateTime(Common.SetDateTime(DateTime.Now.ToString()));
        int iCurrMonth = Convert.ToInt16(dtCurrMonth.Month);

        iCurrMonth = 0;
    }

    private void AvailableLeave()
    {
        if (ddlLeaveType.SelectedValue != "-1")
        {
            if (txtEmpId.Text.Trim() != "")
            {
                DataTable dtLeaveProfile = new DataTable();
                dtLeaveProfile = objLeaveMgr.SelectEmpLeaveProfile(txtEmpId.Text.Trim(), ddlLeaveType.SelectedValue.ToString());
                decimal intAvail = 0;
                decimal LCarryOverd = 0;
                decimal LEntitled = 0;
                //decimal LCashed = 0;
                decimal LEnjoyed = 0;
                decimal LeaveElapsed = 0;
                LAv.Text = "";
                if (dtLeaveProfile.Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["LCarryOverd"].ToString()) == false)
                        LCarryOverd = LCarryOverd + Convert.ToDecimal(dtLeaveProfile.Rows[0]["LCarryOverd"].ToString());
                    else
                        LCarryOverd = 0;

                    if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["lvPrevYearCarry"].ToString()) == false)
                        LCarryOverd = LCarryOverd + Convert.ToDecimal(dtLeaveProfile.Rows[0]["lvPrevYearCarry"].ToString());

                    if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["LEntitled"].ToString()) == false)
                        LEntitled = LEntitled + Convert.ToDecimal(dtLeaveProfile.Rows[0]["LEntitled"].ToString());
                    else
                        LEntitled = 0;
                    //if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["LCashed"].ToString()) == false)
                    //    LCashed = LCashed + Convert.ToDecimal(dtLeaveProfile.Rows[0]["LCashed"].ToString());
                    //else
                    //    LCashed = 0;
                    if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["LeaveEnjoyed"].ToString()) == false)
                        hfLEnjoyed.Value = dtLeaveProfile.Rows[0]["LeaveEnjoyed"].ToString();
                    else
                        hfLEnjoyed.Value = "0";
                    if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["LeaveElapsed"].ToString()) == false)
                        LeaveElapsed = LeaveElapsed + Convert.ToDecimal(dtLeaveProfile.Rows[0]["LeaveElapsed"].ToString());
                    else
                        LeaveElapsed = 0;

                    if (string.IsNullOrEmpty(dtLeaveProfile.Rows[0]["lvOpening"].ToString()) == false)
                        LEnjoyed = LEnjoyed + Convert.ToDecimal(dtLeaveProfile.Rows[0]["lvOpening"].ToString());
                    else
                        LEnjoyed = 0;

                    hfLAbbrName.Value = dtLeaveProfile.Rows[0]["LAbbrName"].ToString();

                    if (hfLTypeNature.Value != "5" && hfLTypeNature.Value != "6")
                    {
                        intAvail = (LCarryOverd + LEntitled) - (Convert.ToDecimal(hfLEnjoyed.Value) + LeaveElapsed + LEnjoyed);

                        LAv.Text = Convert.ToString(Math.Round(intAvail, 1));
                        if (Convert.ToDecimal(LAv.Text) < 0)
                        {
                            LAv.Text = "0";
                        }
                    }
                    else
                        intAvail = Convert.ToDecimal(hfLEnjoyed.Value);

                }
            }
        }
    }

    private void PreAvailableLeave()
    {
        if (ddlLeaveType.SelectedValue != "-1")
        {
            if (txtEmpId.Text.Trim() != "")
            {
                DataTable dtLeaveProfile = new DataTable();
                Decimal PreLvEnjoyed = 0;
                dtLeaveProfile = objLeaveMgr.SelectEmpLeaveProfile(txtEmpId.Text.Trim(), ddlLeaveType.SelectedValue.ToString());

                LAv.Text = "";
                if (dtLeaveProfile.Rows.Count > 0)
                {
                    foreach (DataRow row in dtLeaveProfile.Rows)
                    {
                        if (string.IsNullOrEmpty(row["LeaveEnjoyed"].ToString()) == false)
                            PreLvEnjoyed = Convert.ToDecimal(row["LeaveEnjoyed"].ToString());
                        else
                            PreLvEnjoyed = 0;
                    }
                    //hfPreLEnjoyed.Value = Convert.ToString(PreLvEnjoyed - Convert.ToDecimal(hfPreLEnjoyed.Value));
                }
            }
        }
    }


    protected void txtEmpId_TextChanged(object sender, EventArgs e)
    {

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Enjoyed leave calculation
        DataTable dtLeaveProfile = new DataTable();
        dtLeaveProfile = objLeaveMgr.SelectEmpLeaveProfile(txtEmpId.Text.Trim(), ddlLeaveType.SelectedValue.ToString());
        HiddenField hfLvEnjoyed = new HiddenField();
        if (dtLeaveProfile.Rows.Count > 0)
        {
            foreach (DataRow row in dtLeaveProfile.Rows)
            {
                if (string.IsNullOrEmpty(row["LeaveEnjoyed"].ToString()) == false)
                {
                    hfLvEnjoyed.Value = Convert.ToString(Convert.ToDecimal(row["LeaveEnjoyed"].ToString()) - Convert.ToDecimal(txtLDurInDays.Text));
                    if (Convert.ToDecimal(hfLvEnjoyed.Value) < 0)
                        hfLvEnjoyed.Value = "0";
                }
                else
                    hfLvEnjoyed.Value = "0";
            }
        }
        else
            hfLvEnjoyed.Value = "0";

        LeaveApp objLeave = new LeaveApp(hfID.Value, txtEmpId.Text.Trim(), "", "", "", "", "",
            "C", "R", Session["USERID"].ToString(),
            Common.SetDateTime(DateTime.Now.ToString()), "Y", "N", ddlLeaveType.SelectedValue, "", "",
            hfLvEnjoyed.Value.ToString(), ddlResPerson.SelectedValue.ToString(), "", "","");

        objLeaveMgr.UpdateLeaveAppMstForCancel(objLeave, "Y", "C", hfLvEnjoyed.Value.ToString(), hfLDates.Value.ToString());

        lblMsg.Text = "Leave Cancelled Successfully";
        this.FillEmpWithLeaveInfo(txtEmpId.Text.Trim());
        //Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
        hfLvEnjoyed.Value = "";
        hfLDates.Value = "";
        dtLeaveProfile.Dispose();
    }

    protected void chkIsAllEmp_CheckedChanged(object sender, EventArgs e)
    {
        if (chkIsAllEmp.Checked == false)
        {
            dtResponsePerson = objLeaveMgr.SelectDivisionWiseEmp(txtEmpId.Text.Trim());
            this.Bind_DdlResponsiblePerson();
        }
        else
        {
            dtResponsePerson = objLeaveMgr.SelectDivisionWiseEmp("");
            this.Bind_DdlResponsiblePerson();
        }
    }
    protected void GetWeekend()
    {
        HiddenField hfLeaveDates = new HiddenField();
        HiddenField hfWeeekendDay = new HiddenField();

        double TotDay = 0;
        DateTime dtFrom = new DateTime();
        DateTime dtTo = new DateTime();

        if (string.IsNullOrEmpty(txtFromDate.Text) == true)
        {
            lblMsg2.Text = "Please insert valid start date";
            return;
        }
        if (string.IsNullOrEmpty(txtToDate.Text) == true)
        {
            lblMsg2.Text = "Please insert valid end date";
            return;
        }
        //if (string.IsNullOrEmpty(txtToDate.Text) == false && string.IsNullOrEmpty(txtFromDate.Text) == false)
        //{
        //double TotDay;
        //TimeSpan Dur = Convert.ToDateTime(txtToDate.Text) - Convert.ToDateTime(txtFromDate.Text);
        //TotDay = Math.Round(Convert.ToDouble(Dur.Days), 0) + 1;
        //if (TotDay < 0)
        //{
        //    lblMsg2.Text = "Start Date can not be greater than end date.";
        //    return;
        //}

        if (string.IsNullOrEmpty(txtToDate.Text) == false && string.IsNullOrEmpty(txtFromDate.Text) == false)
        {
            char[] splitter ={ '/' };
            string[] arinfo = Common.str_split(txtFromDate.Text.Trim(), splitter);
            if (arinfo.Length == 3)
            {
                dtFrom = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                arinfo = null;
            }
            arinfo = Common.str_split(txtToDate.Text.Trim(), splitter);
            if (arinfo.Length == 3)
            {
                dtTo = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                arinfo = null;
            }

            TimeSpan Dur = dtTo.Subtract(dtFrom);

            TotDay = Math.Round(Convert.ToDouble(Dur.Days), 0) + 1;
            if (TotDay < 0)
            {
                lblMsg2.Text = "Start Date can not be greater than end date.";
                return;
            }
        }

        DataTable dtEmpWeekend = new DataTable();
        dtEmpWeekend = objLeaveMgr.SelectEmpWiseWeekend(txtEmpId.Text.Trim());
        DateTime LDate = dtFrom;
        int row;
        int LeaveDay = 0;
        hfLeaveDates.Value = "";
        if (dtEmpWeekend.Rows.Count > 0)
        {
            for (row = 0; row < Convert.ToInt32(TotDay); row++)
            {
                string DayName = LDate.DayOfWeek.ToString();
                if (hfLeaveDates.Value != "")
                {
                    hfLeaveDates.Value = hfLeaveDates.Value + ",";
                }
                switch (DayName)
                {
                    case "Sunday":
                        {
                            if (dtEmpWeekend.Rows[0]["WESun"].ToString() == "N")
                            {
                                LeaveDay = LeaveDay + 1;
                                hfLeaveDates.Value = hfLeaveDates.Value + Common.DisplayDate(LDate.ToString());
                                break;
                            }
                            else
                            {
                                LDate = LDate.AddDays(1);
                                if (hfWeeekendDay.Value == "")
                                    hfWeeekendDay.Value = "Sunday";
                                else
                                    hfWeeekendDay.Value = hfWeeekendDay.Value + ", Sunday";
                                continue;
                            }
                        }
                    case "Monday":
                        {
                            if (dtEmpWeekend.Rows[0]["WEMon"].ToString() == "N")
                            {
                                LeaveDay = LeaveDay + 1;
                                hfLeaveDates.Value = hfLeaveDates.Value + Common.DisplayDate(LDate.ToString());
                                break;
                            }
                            else
                            {
                                LDate = LDate.AddDays(1);
                                if (hfWeeekendDay.Value == "")
                                    hfWeeekendDay.Value = "Monday";
                                else
                                    hfWeeekendDay.Value = hfWeeekendDay.Value + ", Monday";
                                continue;
                            }
                        }
                    case "Tuesday":
                        {
                            if (dtEmpWeekend.Rows[0]["WETues"].ToString() == "N")
                            {
                                LeaveDay = LeaveDay + 1;
                                hfLeaveDates.Value = hfLeaveDates.Value + Common.DisplayDate(LDate.ToString());
                                break;
                            }
                            else
                            {
                                LDate = LDate.AddDays(1);
                                if (hfWeeekendDay.Value == "")
                                    hfWeeekendDay.Value = "Tuesday";
                                else
                                    hfWeeekendDay.Value = hfWeeekendDay.Value + ", Tuesday";
                                continue;
                            }
                        }
                    case "Wednesday":
                        {
                            if (dtEmpWeekend.Rows[0]["WEWed"].ToString() == "N")
                            {
                                LeaveDay = LeaveDay + 1;
                                hfLeaveDates.Value = hfLeaveDates.Value + Common.DisplayDate(LDate.ToString());
                                break;
                            }
                            else
                            {
                                LDate = LDate.AddDays(1);
                                if (hfWeeekendDay.Value == "")
                                    hfWeeekendDay.Value = "Wednesday";
                                else
                                    hfWeeekendDay.Value = hfWeeekendDay.Value + ", Wednesday";
                                continue;
                            }
                        }
                    case "Thursday":
                        {
                            if (dtEmpWeekend.Rows[0]["WETue"].ToString() == "N")
                            {
                                LeaveDay = LeaveDay + 1;
                                hfLeaveDates.Value = hfLeaveDates.Value + Common.DisplayDate(LDate.ToString());
                                break;
                            }
                            else
                            {
                                LDate = LDate.AddDays(1);
                                if (hfWeeekendDay.Value == "")
                                    hfWeeekendDay.Value = "Thursday";
                                else
                                    hfWeeekendDay.Value = hfWeeekendDay.Value + ", Thursday";
                                continue;
                            }
                        }
                    case "Friday":
                        {
                            if (dtEmpWeekend.Rows[0]["WEFri"].ToString() == "N")
                            {
                                LeaveDay = LeaveDay + 1;
                                hfLeaveDates.Value = hfLeaveDates.Value + Common.DisplayDate(LDate.ToString());
                                break;
                            }
                            else
                            {
                                LDate = LDate.AddDays(1);
                                if (hfWeeekendDay.Value == "")
                                    hfWeeekendDay.Value = "Friday";
                                else
                                    hfWeeekendDay.Value = hfWeeekendDay.Value + ", Friday";
                                continue;
                            }
                        }
                    case "Saturday":
                        {
                            if (dtEmpWeekend.Rows[0]["WESat"].ToString() == "N")
                            {
                                LeaveDay = LeaveDay + 1;
                                hfLeaveDates.Value = hfLeaveDates.Value + Common.DisplayDate(LDate.ToString());
                                break;
                            }
                            else
                            {
                                LDate = LDate.AddDays(1);
                                if (hfWeeekendDay.Value == "")
                                    hfWeeekendDay.Value = "Saturday";
                                else
                                    hfWeeekendDay.Value = hfWeeekendDay.Value + ", Saturday";
                                continue;
                            }
                        }
                }
                LDate = LDate.AddDays(1);
            }
        }
        if (hfWeeekendDay.Value != "")
            lblMsg2.Text = hfWeeekendDay.Value + " Is Weekend";
        else
            lblMsg2.Text = "";
    }

    protected void SendEmailNotification(string strEmpId)
    {
        string strFromAdd = "";
        string strToEmpId = "";
        string strToAdd = "";
        string strSubject = "";
        string strBody = "";

        DataTable dtFromEmp = new DataTable();
        dtFromEmp = objEmpInfoMgr.SelectEmpInfo(strEmpId);

        if (dtFromEmp.Rows.Count > 0)
            strFromAdd = dtFromEmp.Rows[0]["OfficeEmail"].ToString().Trim();

        DataTable dtToEmpId = new DataTable();
        dtToEmpId = objEmpInfoMgr.SelectEmpInfoHR(strEmpId);

        if (dtToEmpId.Rows.Count > 0)
            strToEmpId = dtToEmpId.Rows[0]["SupervisorId"].ToString().Trim();

        dtFromEmp.Rows.Clear();
        dtFromEmp.Dispose();

        DataTable dtToAdd = new DataTable();
        dtToAdd = objEmpInfoMgr.SelectEmpInfo(strToEmpId);

        if (dtToAdd.Rows.Count > 0)
        {
            foreach (DataRow TRow in dtToAdd.Rows)
            {
                if (strToEmpId == TRow["EmpId"].ToString().Trim())
                    strToAdd = TRow["OfficeEmail"].ToString().Trim();
            }
        }

        DataTable dtLeaveApp = new DataTable();
        dtLeaveApp = objLeaveMgr.SelectRequestLeaveAppMst(Convert.ToInt16(hfID.Value), strEmpId, "R", strStartDate, strEndDate, "");

        if (dtLeaveApp.Rows.Count > 0)
        {
            strSubject = "Requesting for " + dtLeaveApp.Rows[0]["LTypeTitle"].ToString() + " leave";
            strBody = "Kindly grant me " + dtLeaveApp.Rows[0]["LTypeTitle"].ToString() + " for " +
                 Math.Round(Convert.ToDouble(dtLeaveApp.Rows[0]["LDurInDays"].ToString()), 0) + " days on " +
                 Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveStart"].ToString()) +
                 " to " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveEnd"].ToString()) + ".";
        }
        if (strFromAdd != "" && strToAdd != "")
           // SmtpMail.Send(strFromAdd, strToAdd, strSubject, strBody);

        dtToEmpId.Rows.Clear();
        dtToEmpId.Dispose();

        dtToAdd.Rows.Clear();
        dtToAdd.Dispose();
    }

    protected void txtEmpId_TextChanged1(object sender, EventArgs e)
    {

    }

    protected void btnEmpDetails_Click(object sender, EventArgs e)
    {
        txtEmpId.Text = ddlEmpList.SelectedValue.ToString().Trim();
        this.SearchEmployee();
    }
    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (string.IsNullOrEmpty(txtEmpId.Text.Trim()) == false)
        {
            grLeaveStatus.DataSource = null;
            grLeaveStatus.DataBind();
            this.FillEmpInfo(txtEmpId.Text.Trim());
            //this.FillEmpWithLeaveInfo(txtEmpId.Text.Trim());
            //this.OpenRecord();
        }
        else
        {
            this.EntryMode(false);
        }

        //Don't Delete these lines.  
        //if (Session["ISADMIN"].ToString() == "Y")
        //{
        //if (string.IsNullOrEmpty(txtEmpId.Text.Trim()) == false)
        //{
        //    grLeaveStatus.DataSource = null;
        //    grLeaveStatus.DataBind();
        //    this.FillEmpInfo(txtEmpId.Text.Trim());
        //    //this.FillEmpWithLeaveInfo(txtEmpId.Text.Trim());
        //    //this.OpenRecord();
        //}
        //else
        //{
        //    this.EntryMode(false);
        //}
        //}
        //else
        //{
        //    if (Session["EMPLOYEEID"].ToString()  == txtEmpId.Text.Trim())
        //    {
        //        if (string.IsNullOrEmpty(txtEmpId.Text.Trim()) == false)
        //        {
        //            grLeaveStatus.DataSource = null;
        //            grLeaveStatus.DataBind();
        //            this.FillEmpInfo(txtEmpId.Text.Trim());
        //            //this.FillEmpWithLeaveInfo(txtEmpId.Text.Trim());
        //            //this.OpenRecord();
        //        }
        //    }
        //    else
        //    {
        //        lblMsg.Text = "Log in user is not admin user and Employee Emp No. is not matched with users Emp No.. ";
        //        this.EntryMode(false);
        //    }
        //}    
    }
}
