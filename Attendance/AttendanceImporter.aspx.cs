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

public partial class Attendance_AttendanceImporter : System.Web.UI.Page
{
    AttnManager objAttnMgr = new AttnManager();
    DataTable dtReader = new DataTable();
    DataTable dtEmp = new DataTable();
    DataTable dtWeekend = new DataTable();
    DataTable dtHoli = new DataTable();
    DataTable dtAttnPlcy = new DataTable();
    DataTable dtAttendance = new DataTable();
    DataTable dtLeaveDateDet = new DataTable();
    dsAttnImporter objDS = new dsAttnImporter();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Office Changed to Location

            Common.FillDropDownList_Nil(objAttnMgr.SelectLocation(0), ddlDivision);
           // this.GetImporterLog();
        }
    }

    protected void GetImporterLog()
    {
        DataTable dtLog = objAttnMgr.GetImporterLog();
        if (dtLog.Rows.Count > 0)
        {
            string strDivision="";
            if (dtLog.Rows[0]["LocationID"].ToString().Trim() == "99999")
                strDivision = "All";
            else
                strDivision = Common.FindInDdlTextData(ddlDivision, dtLog.Rows[0]["LocationID"].ToString());

            lblLog.Text = "Last Import History:&nbsp;&nbsp; " + " Office: " + strDivision
                        + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Upto Date: " + Common.DisplayDate(dtLog.Rows[0]["TODATE"].ToString());
        }
        else
        {
            lblLog.Text = "";
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        objDS.Tables["Attandance"].Rows.Clear();
        objDS.Tables["Attandance"].Dispose();

        string strDateFrom = Common.ReturnDate(txtFrom.Text.Trim());
        string strDateTo = Common.ReturnDate(txtTo.Text.Trim());
        
        string strDateTimeFrom = strDateFrom + " 00:00";
        string strDateTimeTo = strDateTo + " 23:59";


        DateTime dtFrom = Convert.ToDateTime(strDateFrom);
        DateTime dtTo = Convert.ToDateTime(strDateTo);

        dtReader = objAttnMgr.GetReaderData(strDateTimeFrom, strDateTimeTo);
        dtEmp = objAttnMgr.GetDistinctEmpData(ddlDivision.SelectedValue.ToString().Trim(), rdbtnEmpStatus.SelectedValue.ToString());
        dtAttendance = objAttnMgr.GetEmpData(strDateFrom,strDateTo,ddlDivision.SelectedValue.ToString().Trim());
        dtWeekend = objAttnMgr.GetWeekEndData();
        dtHoli = objAttnMgr.GetHolidayData(dtFrom.Year.ToString(), strDateFrom, strDateTo);
        dtLeaveDateDet = objAttnMgr.GetLeaveDateData(strDateFrom, strDateTo);

        dtAttnPlcy = objAttnMgr.GetAttnPlicy();
        GenerateAttnRecord(dtFrom, dtTo);

        
        if (objDS.Tables["Attandance"].Rows.Count > 0)
        {
          lblMsg.Text=  objAttnMgr.InsertAttendance(objDS.Tables["Attandance"], ddlDivision.SelectedValue.ToString(), strDateFrom, strDateTo);
            //lblMsg.Text = "Attendance Record Imported Successfully.";
        //  this.GetImporterLog();
        }
        else
        {
            lblMsg.Text = "No Attendance Record Found";
        }

    }


    protected void GenerateAttnRecord(DateTime Fromdate, DateTime ToDate)
    {
        int i = 0;
        string strStatus = "A";
        string strIsWeekEnd = "N";
        int intInDelay = 0;
        string strEmpID = "";
        string strSignOutFDateTime = "";
        DateTime date = new DateTime();
        DataRow[] foundAttnPlcy;
        while (Fromdate <= ToDate)
        {
            foreach (DataRow dRow in dtEmp.Rows)
            {
                strSignOutFDateTime="";
                strStatus = "A";
                if (dRow["EMPID"].ToString().Trim().ToUpper() == "PIB00075")
                    strEmpID = "PIB00075";
                //if ((strEmpID != "") && (strEmpID == dRow["EMPID"].ToString().Trim().ToUpper()) && (date == Fromdate))
                //    continue;

                //strEmpID = dRow["EMPID"].ToString().Trim().ToUpper();
                //date = Fromdate;

                DataRow[] foundReader = dtReader.Select("EMPID='" + dRow["EMPID"].ToString().Trim().ToUpper() + "' AND ATTNDDATE='" + Common.SetDate(Fromdate.ToShortDateString()) + "' ");
                foundAttnPlcy = dtAttnPlcy.Select("AttnPolicyID= '" + dRow["AttnPolicyID"].ToString() + "'");
                DataRow[] foundWk = dtWeekend.Select(" WEEKENDID= " + dRow["WEEKENDID"].ToString());
                DataRow[] foundHoliday = dtHoli.Select(" HOLIDATE= '" + Common.SetDate(Fromdate.ToShortDateString()) + "'");
                DataRow[] foundEmp = dtAttendance.Select("EMPID='" + dRow["EMPID"].ToString().Trim().ToUpper() + "' AND ATTNDDATE='" + Common.SetDate(Fromdate.ToShortDateString()) + "' ");
                DataRow[] foundLeave = dtLeaveDateDet.Select("EMPID='" + dRow["EMPID"].ToString().Trim().ToUpper() + "' AND LEVDATE='" + Common.SetDate(Fromdate.ToShortDateString()) + "' ");
                // Retrive the Weekend Day in Alphabet and found the day is weekend or not
                string[] strWeekDays = GetWeekendStatus(foundWk[0]);
                foreach (string str in strWeekDays)
                {
                    if (str != null)
                    {
                        if (str == Fromdate.DayOfWeek.ToString().ToUpper())
                        {
                            strStatus = "W";
                        }
                    }
                }

                if (foundReader.Length > 0)
                {
                    // Attendance Record Not Exist but present in reader
                    
                    if (foundEmp.Length==0)
                    {
                        // Emp Attendance Policy Exist but Attendance Record Not Exist
                        if (foundAttnPlcy.Length > 0)
                        {

                            intInDelay = Convert.ToInt32(CalculateDelay(Convert.ToString(Convert.ToDateTime(foundReader[0]["SIGNINTIME"].ToString())),
                                foundAttnPlcy[0]["SunIn"].ToString(), foundAttnPlcy[0]["arvlgrace"].ToString()));
                            if (intInDelay <= 0)
                            {
                                if (strStatus == "W")
                                    strStatus = "WP";
                                else
                                    strStatus = "P";
                            }
                            else
                            {
                                if (strStatus == "W")
                                    strStatus = "WP";
                                else
                                    strStatus = "L";
                            }
                            if (foundHoliday.Length > 0)
                            {
                                strStatus = "HP";
                            }
                            if (foundLeave.Length > 0)
                            {
                                strStatus = "LV";
                            }
                           // Policy Out Time
                            strSignOutFDateTime = Common.DisplayTime(foundAttnPlcy[0]["SunOut"].ToString());
                            strSignOutFDateTime = Fromdate.ToShortDateString() + " " +strSignOutFDateTime;

                            this.AddToAttendanceTable(dRow["EMPID"].ToString(), Fromdate.ToShortDateString(), foundReader[0]["SIGNINTIME"].ToString(), foundReader[0]["SIGNOUTTIME"].ToString(),
                                strSignOutFDateTime, strStatus, intInDelay.ToString(), dRow["AttnPolicyID"].ToString(), dRow["WEEKENDID"].ToString(),
                                "", "", foundAttnPlcy[0]["lunchbreak"].ToString(), "N");
                        }
                        // No Attendance Policy Found but Attendance Recrod in Reader Exist
                        else
                        {
                            if (foundLeave.Length > 0)
                            {
                                strStatus = "LV";
                            }
                            else
                            {
                                strStatus = "X";
                            }

                            this.AddToAttendanceTable(dRow["EMPID"].ToString(), Fromdate.ToShortDateString(), foundReader[0]["SIGNINTIME"].ToString(), foundReader[0]["SIGNOUTTIME"].ToString(),
                               "", strStatus, intInDelay.ToString(), "", dRow["WEEKENDID"].ToString(),
                               "", "", "", "N");
                        }
                    }
                    // Attendance record Exist in Attendance table
                    else
                    {
                        if (foundEmp[0]["Status"].ToString() == "LV")
                        {
                            string strSignIn = "";
                            string strSignOut = "";
                            strSignIn = foundReader[0]["SIGNINTIME"].ToString();
                            strSignOut = foundReader[0]["SIGNOUTTIME"].ToString();
                            if (string.IsNullOrEmpty(strSignIn) == false)
                            {
                                this.AddToAttendanceTable(dRow["EMPID"].ToString(), Fromdate.ToShortDateString(), strSignIn, strSignOut,
                               strSignOutFDateTime, "LV", "0", dRow["AttnPolicyID"].ToString(), dRow["WEEKENDID"].ToString(),
                               "", "", foundAttnPlcy[0]["lunchbreak"].ToString(), "Y");
                                continue;
                            }
                            else
                            {
                                continue;
                            }

                        }
                        else
                        {
                            if (foundLeave.Length > 0)
                            {
                                this.AddToAttendanceTable(dRow["EMPID"].ToString(), Fromdate.ToShortDateString(), foundReader[0]["SIGNINTIME"].ToString(), foundReader[0]["SIGNOUTTIME"].ToString(),
                               "", "LV", intInDelay.ToString(), "", dRow["WEEKENDID"].ToString(),
                               "", "", "", "Y");
                                continue;
                            }
                        }

                        if (foundEmp[0]["Status"].ToString() == "TV")
                            continue;

                        if (foundAttnPlcy.Length > 0)
                        {

                            intInDelay = Convert.ToInt32(CalculateDelay(Convert.ToString(Convert.ToDateTime(foundReader[0]["SIGNINTIME"].ToString())),
                                foundAttnPlcy[0]["SunIn"].ToString(), foundAttnPlcy[0]["arvlgrace"].ToString()));
                            
                            if (intInDelay <= 0)
                            {
                                if (strStatus == "W")
                                    strStatus = "WP";
                                else
                                    strStatus = "P";
                            }
                            else
                            {
                                if (strStatus == "W")
                                    strStatus = "WP";
                                else
                                    strStatus = "L";
                            }
                            if (foundHoliday.Length > 0)
                            {
                                strStatus = "HP";
                            }
                            // Policy Out Time
                            strSignOutFDateTime = Common.DisplayTime(foundAttnPlcy[0]["SunOut"].ToString());
                            strSignOutFDateTime = Fromdate.ToShortDateString() + " " +strSignOutFDateTime;

                            this.AddToAttendanceTable(dRow["EMPID"].ToString(), Fromdate.ToShortDateString(), foundReader[0]["SIGNINTIME"].ToString(), foundReader[0]["SIGNOUTTIME"].ToString(),
                                strSignOutFDateTime, strStatus, intInDelay.ToString(), dRow["AttnPolicyID"].ToString(), dRow["WEEKENDID"].ToString(),
                                "", "", foundAttnPlcy[0]["lunchbreak"].ToString(), "Y");
                        }
                    }
                }
                // No Record Found in Reader
                else
                {
                    // Employee is Inactive
                    if (rdbtnEmpStatus.SelectedValue.ToString() == "A")
                    {
                        if (dRow["EmpStatus"].ToString() == "I")
                            continue;
                    }

                    // Employee on Travel
                    if (foundEmp.Length > 0)
                    {
                        if (foundEmp[0]["Status"].ToString().Trim() == "TV")
                            continue;
                    }

                    // Leave record Found
                    if (foundLeave.Length > 0)
                    {
                        if (foundEmp.Length > 0)
                            this.AddToAttendanceTable(dRow["EMPID"].ToString(), Fromdate.ToShortDateString(), "", "",
                                   "", "LV", "", "", dRow["WEEKENDID"].ToString(),
                                   "", "", foundAttnPlcy[0]["lunchbreak"].ToString(), "Y");
                        else
                            this.AddToAttendanceTable(dRow["EMPID"].ToString(), Fromdate.ToShortDateString(), "", "",
                                   "", "LV", "", "", dRow["WEEKENDID"].ToString(),
                                   "", "", foundAttnPlcy[0]["lunchbreak"].ToString(), "N");
                        continue;
                    }

                    //Holiday Record Found
                    if (foundHoliday.Length > 0)
                    {
                        if(foundEmp.Length>0)
                            this.AddToAttendanceTable(dRow["EMPID"].ToString(), Fromdate.ToShortDateString(), "", "",
                                   "", "H", "", dRow["AttnPolicyID"].ToString(), dRow["WEEKENDID"].ToString(),
                                   "", "", foundAttnPlcy[0]["lunchbreak"].ToString(), "Y");
                        else
                            this.AddToAttendanceTable(dRow["EMPID"].ToString(), Fromdate.ToShortDateString(), "", "",
                                   "", "H", "", dRow["AttnPolicyID"].ToString(), dRow["WEEKENDID"].ToString(),
                                   "", "", foundAttnPlcy[0]["lunchbreak"].ToString(), "N");
                    }

                    // IF Weekend    
                    else if (strStatus == "W")
                    {
                        if(foundEmp.Length>0)
                            this.AddToAttendanceTable(dRow["EMPID"].ToString(), Fromdate.ToShortDateString(), "", "",
                                   "", "W", "", dRow["AttnPolicyID"].ToString(), dRow["WEEKENDID"].ToString(),
                                   "", "", foundAttnPlcy[0]["lunchbreak"].ToString(), "Y");
                        else
                            this.AddToAttendanceTable(dRow["EMPID"].ToString(), Fromdate.ToShortDateString(), "", "",
                                  "", "W", "", dRow["AttnPolicyID"].ToString(), dRow["WEEKENDID"].ToString(),
                                  "", "", foundAttnPlcy[0]["lunchbreak"].ToString(), "N");
                    }

                    else
                    {
                        if (foundEmp.Length > 0)
                        {
                            if (foundAttnPlcy.Length > 0)
                            {
                                this.AddToAttendanceTable(dRow["EMPID"].ToString(), Fromdate.ToShortDateString(), "", "",
                                            "", "A", "", dRow["AttnPolicyID"].ToString(), dRow["WEEKENDID"].ToString(),
                                            "", "", foundAttnPlcy[0]["lunchbreak"].ToString(), "Y");
                            }
                            else
                            {
                                this.AddToAttendanceTable(dRow["EMPID"].ToString(), Fromdate.ToShortDateString(), "", "",
                                           "", "A", "", "", dRow["WEEKENDID"].ToString(),
                                           "", "", "", "Y");
                            }

                        }
                        else
                        {
                            if (foundAttnPlcy.Length > 0)
                            {
                                this.AddToAttendanceTable(dRow["EMPID"].ToString(), Fromdate.ToShortDateString(), "", "",
                                            "", "A", "", dRow["AttnPolicyID"].ToString(), dRow["WEEKENDID"].ToString(),
                                            "", "", foundAttnPlcy[0]["lunchbreak"].ToString(), "N");
                            }
                            else
                            {
                                this.AddToAttendanceTable(dRow["EMPID"].ToString(), Fromdate.ToShortDateString(), "", "",
                                           "", "A", "", "", dRow["WEEKENDID"].ToString(),
                                           "", "", "", "N");
                            }
                        }
                    }
                }
                foundLeave = null;
                foundReader = null;
                foundHoliday = null;
                foundEmp=null;
                foundWk = null;

            }
            Fromdate = Fromdate.AddDays(1);
            foundAttnPlcy = null;
            
        }
    }

    protected void AddToAttendanceTable(string strEmpId, string strAttdate, string strSignIn, string strSignOut, string strSignOutF, string strStatus,
        string strLateTimeAmt, string strAttnPlcyId, string strWkId, string strInLoc, string strOutLoc, string strLunchMin, string strIsExist)
    {
        DataRow nRow = objDS.Tables["Attandance"].NewRow();
        nRow["EMPID"] = strEmpId.Trim();
        nRow["AttndDate"] = strAttdate;
        nRow["SignInTime"] = strSignIn;
        nRow["SignOutTime"] = strSignOut;
        nRow["SingOutTimeF"] = strSignOutF;
        nRow["Status"] = strStatus;
        nRow["LateTimeAmt"] = strLateTimeAmt;
        nRow["AttnPolicyId"] = strAttnPlcyId;
        nRow["WeekEndID"] = strWkId;
        nRow["InLocation"] = strInLoc;
        nRow["OutLocation"] = strOutLoc;
        nRow["LaunchMinutes"] = strLunchMin;
        nRow["IsAttnExist"] = strIsExist;
        objDS.Tables["Attandance"].Rows.Add(nRow);
        objDS.Tables["Attandance"].AcceptChanges();

    }

    public string CalculateDelay(string strInTime, string strPolicyInTime, string strArrGraceTime)
    {
        string strRetValue = "";
        if (string.IsNullOrEmpty(strInTime) == true)
            return "0";
        if (string.IsNullOrEmpty(strPolicyInTime) == true)
            return "0";
        int GraceTime = Convert.ToInt32(strArrGraceTime);
        int timeDiff = 0;
        int delay = 0;
        string InTime = Common.DisplayTime(strInTime);
        string PcyInTime = Common.DisplayTime(strPolicyInTime);
        timeDiff = GetTimeDiff(InTime, PcyInTime, "N");
        delay = timeDiff + GraceTime;
        if (timeDiff >= 0)
            strRetValue = "0";
        else
        {
            if (delay >= 0)
                strRetValue = "0";
            else
                strRetValue = Convert.ToString(Math.Abs(timeDiff));
        }
        return strRetValue;
    }

    protected int GetTimeDiff(string InTime, string PcyInTime, string IsNextDay)
    {
        int timeDiff = 0;
        DateTime dFrom;
        DateTime dTo;

        if (DateTime.TryParse(InTime, out dFrom) && DateTime.TryParse(PcyInTime, out dTo))
        {
            TimeSpan TS = dTo - dFrom;
            int hour = TS.Hours;
            int mins = TS.Minutes;
            int secs = TS.Seconds;
            timeDiff = hour * 60 + mins;
            if (IsNextDay == "Y")
                timeDiff = 1440 + timeDiff;
        }
        return timeDiff;
    }

    protected string[] GetWeekendStatus(DataRow row)
    {
        string[] strWeekDays = new string[7];
        if (row["WESUN"].ToString() == "Y")
            strWeekDays[0] = "SUNDAY";
        if (row["WEMON"].ToString() == "Y")
            strWeekDays[1] = "MONDAY";
        if (row["WETUES"].ToString() == "Y")
            strWeekDays[2] = "TUESDAY";
        if (row["WEWED"].ToString() == "Y")
            strWeekDays[3] = "WEDNESDAY";
        if (row["WETUE"].ToString() == "Y")
            strWeekDays[4] = "THURSDAY";
        if (row["WEFRI"].ToString() == "Y")
            strWeekDays[5] = "FRIDAY";
        if (row["WESAT"].ToString() == "Y")
            strWeekDays[6] = "SATURDAY";

        return strWeekDays;
    }
}
