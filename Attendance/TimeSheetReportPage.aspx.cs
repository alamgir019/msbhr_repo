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
using System.Collections.Generic;

public partial class Attendance_TimeSheetReportPage : System.Web.UI.Page
{
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    TimeSheetManager timeSheetMgr = new TimeSheetManager();
    Payroll_MasterMgr objPayMstMgr = new Payroll_MasterMgr();
    GADRecoveryManager objGADMgr = new GADRecoveryManager();

    DataTable dtEmpInfo = new DataTable();
    DataTable dtTimeSheet = new DataTable();
    DataTable dtWeekEnd = new DataTable();
    DataTable dtHolidays = new DataTable();
    DataTable dtLeave = new DataTable();
    DataTable dtLeaveDate = new DataTable();
    DataTable dtTimeSheetLeave = new DataTable();
    DataTable dtMonHour = new DataTable();

    ArrayList arl = new ArrayList();
    decimal monlyHour = 0.0M;
    string[] strVal = new string[4];
    string fisCalYrId = "";
    string mon = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        lblDate.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
        lblDateSup.Text = System.DateTime.Today.ToString("dd/MM/yyyy");

        string strParams = Request.QueryString["params"];

        //string[] strVal = new string[5];
        DataTable dtSummAttnd = new DataTable();
        if (string.IsNullOrEmpty(strParams) == false)
        {
            char[] splitter ={ ',' };
            strVal = Common.str_split(strParams, splitter);
        }

        //lblFrom.Text = Common.DisplayDate(strVal[0].Trim());
        //lblTo.Text = Common.DisplayDate(strVal[1].Trim());

        
        lblEmpId.Text = strVal[0].Trim();

        //Month put in strVal[1]
        mon = strVal[1].Trim();

        lblYear.Text = strVal[2].Trim();
        fisCalYrId = strVal[3].Trim();



        //lblEmpName.Text = strVal[3].Trim() + "," + strVal[4].Trim();
        //lblPos.Text = strVal[4].Trim();
        //lblProject.Text = strVal[5].Trim();// +"," + strVal[6].Trim() + "," + strVal[7].Trim();


        GetStartEndDate();        
        GetEmpPersonalInfo();
        if (lblMsg.Text == "No data found")
        {
            return;
        }
        else
        {
            lblMsg.Text = "";
        }
        GenerateDate();

        //DataRow dr = dt.NewRow();
        for (int i = 0; i < arl.Count; i++)
        {
            this.grTimeSheet.Columns[0].HeaderText = "Name of Project";
            this.grTimeSheet.Columns[i + 3].HeaderText = arl[i].ToString();
        }



        int aVal = 0;
        if (arl.Count == 30)
        {
            aVal = 6;
        }
        else if (arl.Count == 29)
        {
            aVal = 7;
        }
        else if (arl.Count == 28)
        {
            aVal = 8;
        }
        else
            aVal = 5;




        BindGrid();
        //Cal(grTimeSheet);
        //GetSummaryTotal(grTimeSheet, aVal);
        


        for (int i = 0; i < arl.Count; i++)
        {
            this.grLeave.Columns[0].HeaderText = "Type  of  Leave";
            this.grLeave.Columns[i + 3].HeaderText = arl[i].ToString();
        }


        int aVal1 = 0;
        if (arl.Count == 30)
        {
            aVal1 = 5;
        }
        else if (arl.Count == 29)
        {
            aVal1 = 6;
        }
        else if (arl.Count == 28)
        {
            aVal1 = 7;
        }
        else
            aVal1 = 4;


        BindLeave();
        DailyHourDistribution();


        Cal(grTimeSheet);
        GetSummaryTotal(grTimeSheet, aVal);
        GetGrandTtal(grTimeSheet);


        
        Cal(grLeave);
        GetSummaryTotal(grLeave, aVal1);
        GetGrandTtal(grLeave);

        GetMonthlyHour();
        RatCal();
        GetFullSummary();
    }


    private void DailyHourDistribution()
    {
        int aVal = 0;
        if (arl.Count == 30)
        {
            aVal = 5;
        }
        else if (arl.Count == 29)
        {
            aVal = 6;
        }
        else if (arl.Count == 28)
        {
            aVal = 7;
        }
        else
            aVal = 4;



        if (grLeave.Rows.Count > 0)
        {
            foreach (GridViewRow gr in grLeave.Rows)
            {
                for (int i = 0; i < grLeave.Columns.Count - aVal; i++)
                {
                    //TextBox txt = (TextBox)gr.Cells[i + 3].FindControl("txt" + Convert.ToString(i + 1));
                    if (gr.Cells[i + 3].Text != "")
                    {

                        foreach (GridViewRow grr in grTimeSheet.Rows)
                        {

                            grTimeSheet.HeaderRow.Cells[i + 3].BackColor = System.Drawing.ColorTranslator.FromHtml("#C0C0C0");
                            grr.Cells[i + 3].BackColor = System.Drawing.ColorTranslator.FromHtml("#C0C0C0");
                            //TextBox txtt = (TextBox)grr.Cells[i + 3].FindControl("txt" + Convert.ToString(i + 1));
                            grr.Cells[i + 3].BackColor = System.Drawing.ColorTranslator.FromHtml("#C0C0C0");                           

                        }


                    }
                }
            }
        }

    }

    public void GetEmpPersonalInfo()
    {
        DataTable dtSuper = new DataTable();
        dtEmpInfo = objEmpMgr.SelectEmpInfo(lblEmpId.Text.Trim());
        if (dtEmpInfo.Rows.Count > 0)
        {
            foreach (DataRow row in dtEmpInfo.Rows)
            {
                //hfEmpId.Value = row["EmpId"].ToString().Trim();
                lblEmpName.Text = row["FullName"].ToString().Trim() + "," + row["JobTitle"].ToString().Trim();
                lblProject.Text = row["DivisionName"].ToString().Trim() + "," + row["PostingPlaceName"].ToString().Trim();
                lblEmpSig.Text = row["FullName"].ToString().Trim();



                string superVisorID = row["ReportingTo"].ToString().Trim();

                if (superVisorID != "")
                {
                    dtSuper = objEmpMgr.SelectEmpInfoOfficeWiseForLeaveSPV(superVisorID, "-1");
                    if (dtSuper.Rows.Count > 0)
                    {
                        lblSup.Text = dtSuper.Rows[0]["FullName"].ToString().Trim();
                    }
                }
                else
                {
                    lblSup.Text = "";
                }
            }


        }
        else
        {            
            lblEmpName.Text = "";
            return;
        }
    }


    private static List<DateTime> GetDateRange(DateTime StartingDate, DateTime EndingDate)
    {
        if (StartingDate > EndingDate)
        {
            return null;
        }
        List<DateTime> rv = new List<DateTime>();
        DateTime tmpDate = StartingDate;
        do
        {
            rv.Add(tmpDate);
            tmpDate = tmpDate.AddDays(1);
        } while (tmpDate <= EndingDate);
        return rv;
    }

    public void GetStartEndDate()
    {
        DataTable dtVwr = new DataTable();
        dtVwr = timeSheetMgr.GET_TimeSheetVwr(lblEmpId.Text.Trim(),mon.Trim(), lblYear.Text.Trim(), fisCalYrId);

        if (dtVwr.Rows.Count > 0)
        {
            foreach (DataRow row in dtVwr.Rows)
            {
                lblFrom.Text = Common.DisplayDate(row["Startdate"].ToString().Trim());
                lblTo.Text = Common.DisplayDate(row["EndDate"].ToString().Trim());

                if (lblFrom.Text == "")
                {
                    lblMsg.Text = "No data found";
                    return;
                }
            }
            lblMsg.Text = "";
        }        
    }

    
    public void GenerateDate()
    {
        if (lblFrom.Text == "")
        {
            lblMsg.Text = "No data found";
            return;
        }

        string strFromDate = "";
        if (string.IsNullOrEmpty(lblFrom.Text.Trim()) == false)
            strFromDate = Common.ReturnDate(lblFrom.Text.Trim());


        string strEndDate = "";
        if (string.IsNullOrEmpty(lblTo.Text.Trim()) == false)
            strEndDate = Common.ReturnDate(lblTo.Text.Trim());

        //ArrayList arl = new ArrayList();

        DateTime StartingDate = DateTime.Parse(strFromDate);
        DateTime EndingDate = DateTime.Parse(strEndDate);



        //mon.Trim(),lblYear.Text.Trim()

        if (string.IsNullOrEmpty(Session["USERID"].ToString()) == false)
        {
            if (Session["USERID"].ToString().ToUpper() != "ADMIN")
            {
                string t1 = lblFrom.Text;
                string t2 = lblTo.Text;

                DateTime datee = Convert.ToDateTime(Common.ReturnDate(lblFrom.Text));
                DateTime dateend = Convert.ToDateTime(Common.ReturnDate(lblTo.Text));
                int start = Convert.ToInt32(datee.Day);
                int end = Convert.ToInt32(dateend.Day);

                //int start = Convert.ToInt32(txtStartDt.Text);
                //int end = Convert.ToInt32(txtEndDate.Text);
                if (start > end)
                {
                    StartingDate = DateTime.Parse(lblYear.Text.Trim() + @"/" + mon.Trim() + @"/" + start.ToString()); //DateTime.Parse(strFromDate);
                    EndingDate = DateTime.Parse(lblYear.Text.Trim() + @"/" + mon.Trim() + @"/" + end.ToString()); // DateTime.Parse(strEndDate);                
                    StartingDate = StartingDate.AddMonths(-1);
                    //StartingDate = StartingDate.AddDays(1);
                }
                else
                {
                    StartingDate = DateTime.Parse(lblYear.Text.Trim() + @"/" + mon.Trim() + @"/" + start.ToString()); //DateTime.Parse(strFromDate);
                    EndingDate = DateTime.Parse(lblYear.Text.Trim() + @"/" + mon.Trim() + @"/" + end.ToString()); // DateTime.Parse(strEndDate);                
                }
            }
            else
            {
                StartingDate = DateTime.Parse(strFromDate);
                EndingDate = DateTime.Parse(strEndDate);
            }

        }




        foreach (DateTime date in GetDateRange(StartingDate, EndingDate))
        {
            //arl.Add(Common.SetDate(date.ToString()));
            arl.Add(date.ToString("dd/MM/yyyy"));
        }
        string cnt = arl.Count.ToString();

        if (arl.Count <= 31)
        {
           
            grTimeSheet.DataSource = null;
            grTimeSheet.DataBind();
            
            return;
        }
        else
        {
            
        }
    }




    private void BindGrid()
    {
        string dtFrmDate = "";
        if (string.IsNullOrEmpty(lblFrom.Text.Trim()) == false)
            dtFrmDate = Common.ReturnDate(lblFrom.Text.Trim());

        string dtTillDate = "";
        if (string.IsNullOrEmpty(lblTo.Text.Trim()) == false)
            dtTillDate = Common.ReturnDate(lblTo.Text.Trim());


        //DataTable dtTimeSheet = new DataTable();
        dtTimeSheet = timeSheetMgr.GET_TimeSheet(lblEmpId.Text.Trim(), mon.Trim(),lblYear.Text.Trim(), fisCalYrId, dtFrmDate, dtTillDate);

        dtWeekEnd = timeSheetMgr.GET_EMP_WeekEnd(lblEmpId.Text.Trim());

        dtHolidays = timeSheetMgr.GET_Holidays_YrDay(lblYear.Text.Trim(), dtFrmDate, dtTillDate);



        



        string GADCODE = "";

        DataTable dtCostRecData = objGADMgr.SelectCostRecoveryPlanData(fisCalYrId, lblEmpId.Text.Trim());
        grTimeSheet.DataSource = dtCostRecData;
        grTimeSheet.DataBind();

        int month = Convert.ToInt32(mon.Trim());// ddlMonth.SelectedValue.ToString());
        DataRow[] fTRow;
        string strVDate = "";
        foreach (GridViewRow gRow in grTimeSheet.Rows)
        {
            GADCODE = grTimeSheet.DataKeys[gRow.DataItemIndex].Values[0].ToString().Trim();
            fTRow = null;

            //Retriving GAD Percentage value from Table CostRecovery.
            foreach (DataRow row in dtCostRecData.Rows)
            {
                if (grTimeSheet.DataKeys[gRow.DataItemIndex].Values[0].ToString().Trim() == row["GADCODE"].ToString().Trim())
                {
                    switch (month)
                    {
                        case 1:
                            gRow.Cells[1].Text = row["JAN"].ToString().Trim();
                            break;

                        case 2:
                            gRow.Cells[1].Text = row["FEB"].ToString().Trim();
                            break;

                        case 3:
                            gRow.Cells[1].Text = row["MAR"].ToString().Trim();
                            break;

                        case 4:
                            gRow.Cells[1].Text = row["APR"].ToString().Trim();
                            break;

                        case 5:
                            gRow.Cells[1].Text = row["MAY"].ToString().Trim();
                            break;

                        case 6:
                            gRow.Cells[1].Text = row["JUN"].ToString().Trim();
                            break;

                        case 7:
                            gRow.Cells[1].Text = row["JUL"].ToString().Trim();
                            break;

                        case 8:
                            gRow.Cells[1].Text = row["AUG"].ToString().Trim();
                            break;

                        case 9:
                            gRow.Cells[1].Text = row["SEP"].ToString().Trim();
                            break;

                        case 10:
                            gRow.Cells[1].Text = row["OCT"].ToString().Trim();
                            break;

                        case 11:
                            gRow.Cells[1].Text = row["NOV"].ToString().Trim();
                            break;

                        case 12:
                            gRow.Cells[1].Text = row["DEC"].ToString().Trim();
                            break;
                    }
                }

            }


            DateTime dtVDate;
            string vDay = "";
            string weekEndDay = "";
            DateTime holiDay;


            int aVal = 0;
            if (arl.Count == 30)
            {
                aVal = 6;
            }
            else if (arl.Count == 29)
            {
                aVal = 7;
            }
            else if (arl.Count == 28)
            {
                aVal = 8;
            }
            else
                aVal = 5;


            //Cross checking and filling the cell value from Table TimeSheet.
            for (int i = 0; i < grTimeSheet.Columns.Count - aVal; i++)
            {
                strVDate = "";
                fTRow = null;
                strVDate = grTimeSheet.HeaderRow.Cells[i + 3].Text.Trim() + "/" + lblYear.Text.Trim(); // ddlYear.SelectedValue.Trim();
                strVDate = Common.ReturnDate(strVDate);

                dtVDate = Convert.ToDateTime(strVDate);
                vDay = dtVDate.DayOfWeek.ToString();
                foreach (DataRow dtRw in dtWeekEnd.Rows)
                {
                    switch (vDay)
                    {
                        case "Sunday":
                            weekEndDay = dtRw["WESun"].ToString().Trim();
                            break;

                        case "Monday":
                            weekEndDay = dtRw["WEMon"].ToString().Trim();
                            break;

                        case "Tuesday":
                            weekEndDay = dtRw["WETues"].ToString().Trim();
                            break;

                        case "Wednesday":
                            weekEndDay = dtRw["WEWed"].ToString().Trim();
                            break;

                        case "Thursday":
                            weekEndDay = dtRw["WETue"].ToString().Trim();
                            break;

                        case "Friday":
                            weekEndDay = dtRw["WEFri"].ToString().Trim();
                            break;

                        case "Saturday":
                            weekEndDay = dtRw["WESat"].ToString().Trim();
                            break;
                    }
                }



                fTRow = dtTimeSheet.Select("GADCODE='" + GADCODE + "' AND VDate='" + strVDate + "'");
                gRow.Cells[2].Text = fTRow[0]["AccLine"].ToString().Trim();
                
                decimal allocatedPercentage = Convert.ToDecimal(gRow.Cells[1].Text);
                decimal allocatedDailyHour = Convert.ToDecimal(System.Web.Configuration.WebConfigurationManager.AppSettings["LeaveHour"]);
                decimal distributedHour = (allocatedDailyHour * allocatedPercentage) / 100;
                distributedHour = Math.Truncate(distributedHour * 1000) / 1000;

                decimal dtVal = 0;

                if (fTRow.Length > 0)
                {
                    dtVal = fTRow[0]["VHour"].ToString() == "" ? 0 : Convert.ToDecimal(fTRow[0]["VHour"].ToString().Trim());
                    dtVal = Math.Truncate(dtVal * 1000) / 1000;                    

                    gRow.Cells[i + 3].Text = dtVal.ToString().Trim();
                }
                else
                {
                    
                }

                if (gRow.Cells[i + 3].Text != distributedHour.ToString())
                {
                    gRow.Cells[i + 3].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFA500");
                }


                foreach (DataRow dRow in dtHolidays.Rows)
                {
                    holiDay = Convert.ToDateTime(dRow["HoliDate"].ToString().Trim());
                    if (holiDay == dtVDate)
                    {
                        {
                            gRow.Cells[i + 3].BackColor = System.Drawing.ColorTranslator.FromHtml("#F2DBDB");                            
                            grTimeSheet.HeaderRow.Cells[i + 3].BackColor = System.Drawing.ColorTranslator.FromHtml("#F2DBDB");                            
                        }
                    }
                }


                if (weekEndDay == "Y")
                {
                    gRow.Cells[i + 3].BackColor = System.Drawing.ColorTranslator.FromHtml("#B8CCE4");                    
                    grTimeSheet.HeaderRow.Cells[i + 3].BackColor = System.Drawing.ColorTranslator.FromHtml("#B8CCE4");                    
                }
            }

        }

    }


    private void BindLeave()
    {
        
        LeaveProto();

       

        DateTime dtVDate;
        string vDay = "";
        string weekEndDay = "";
        DateTime leaveDay;
        string strVDate = "";
        DateTime holiDay;
        DataRow[] fTRow;
        string Ltype = "";

        foreach (GridViewRow gr in grLeave.Rows)
        {
            Ltype = grLeave.DataKeys[gr.DataItemIndex].Values[0].ToString().Trim();

            foreach (DataRow row in dtLeave.Rows)
            {
                if (grLeave.DataKeys[gr.DataItemIndex].Values[0].ToString().Trim() == row["Ltype"].ToString().Trim())
                {
                    gr.Cells[1].Text = row["RecCount"].ToString().Trim();
                }
            }

            int aVal = 0;
            if (arl.Count == 30)
            {
                aVal = 5;
            }
            else if (arl.Count == 29)
            {
                aVal = 6;
            }
            else if (arl.Count == 28)
            {
                aVal = 7;
            }
            else
                aVal = 4;


            for (int i = 0; i < grLeave.Columns.Count - aVal; i++)
            {
                strVDate = "";
                strVDate = grLeave.HeaderRow.Cells[i + 3].Text.Trim() + "/" + lblYear.Text.Trim();
                strVDate = Common.ReturnDate(strVDate);

                dtVDate = Convert.ToDateTime(strVDate);
                vDay = dtVDate.DayOfWeek.ToString();



                fTRow = dtTimeSheetLeave.Select("Ltype='" + Ltype + "' AND VDate='" + strVDate + "'");
                gr.Cells[2].Text = fTRow[0]["AccLine"].ToString().Trim();                   
                //TextBox txt = (TextBox)gr.Cells[i + 2].FindControl("txt" + Convert.ToString(i + 1));
                LinkButton lb = (LinkButton)gr.Cells[i + 3].FindControl("lb" + Convert.ToString(i + 1));
                if (fTRow.Length > 0)
                {
                    gr.Cells[i + 3].Text = fTRow[0]["VHour"].ToString().Trim();                   
                }
                else
                {                    
                    lb.ToolTip = "";
                }




                //TextBox txt = (TextBox)gr.Cells[i + 2].FindControl("txt" + Convert.ToString(i + 1));

                foreach (DataRow dRow in dtLeaveDate.Rows)
                {
                    leaveDay = Convert.ToDateTime(dRow["LevDate"].ToString().Trim());
                    if (leaveDay == dtVDate)
                    {
                        {                            
                            if (dtTimeSheetLeave.Rows.Count == 0)
                            {
                                gr.Cells[i + 3].Text = System.Web.Configuration.WebConfigurationManager.AppSettings["LeaveHour"];
                            }
                        }
                    }
                }



                foreach (DataRow dRow in dtHolidays.Rows)
                {
                    holiDay = Convert.ToDateTime(dRow["HoliDate"].ToString().Trim());
                    if (holiDay == dtVDate)
                    {
                        {
                            gr.Cells[i + 3].BackColor = System.Drawing.ColorTranslator.FromHtml("#C0C0C0");                            
                            grLeave.HeaderRow.Cells[i + 3].BackColor = System.Drawing.ColorTranslator.FromHtml("#C0C0C0");                            
                        }
                    }
                }

                dtVDate = Convert.ToDateTime(strVDate);
                vDay = dtVDate.DayOfWeek.ToString();
                foreach (DataRow dtRw in dtWeekEnd.Rows)
                {
                    switch (vDay)
                    {
                        case "Sunday":
                            weekEndDay = dtRw["WESun"].ToString().Trim();
                            break;

                        case "Monday":
                            weekEndDay = dtRw["WEMon"].ToString().Trim();
                            break;

                        case "Tuesday":
                            weekEndDay = dtRw["WETues"].ToString().Trim();
                            break;

                        case "Wednesday":
                            weekEndDay = dtRw["WEWed"].ToString().Trim();
                            break;

                        case "Thursday":
                            weekEndDay = dtRw["WETue"].ToString().Trim();
                            break;

                        case "Friday":
                            weekEndDay = dtRw["WEFri"].ToString().Trim();
                            break;

                        case "Saturday":
                            weekEndDay = dtRw["WESat"].ToString().Trim();
                            break;
                    }
                }


                if (weekEndDay == "Y")
                {
                    gr.Cells[i + 3].BackColor = System.Drawing.ColorTranslator.FromHtml("#C0C0C0");                    
                    grLeave.HeaderRow.Cells[i + 3].BackColor = System.Drawing.ColorTranslator.FromHtml("#C0C0C0");                    
                }



            }
        }
    }


    public void LeaveProto()
    {
        string dtFrmDate = "";
        if (string.IsNullOrEmpty(lblFrom.Text.Trim()) == false)
            dtFrmDate = Common.ReturnDate(lblFrom.Text.Trim());

        string dtTillDate = "";
        if (string.IsNullOrEmpty(lblTo.Text.Trim()) == false)
            dtTillDate = Common.ReturnDate(lblTo.Text.Trim());

        dtLeave = timeSheetMgr.GET_LeaveRecord_TimeSheet(lblEmpId.Text.Trim(), dtFrmDate, dtTillDate);
        grLeave.DataSource = dtLeave;
        grLeave.DataBind();

        dtLeaveDate = timeSheetMgr.GET_LeaveDate_TimeSheet(lblEmpId.Text.Trim(), dtFrmDate, dtTillDate);

        dtTimeSheetLeave = timeSheetMgr.GET_TimeSheetLeave(lblEmpId.Text.Trim(), mon.Trim(), lblYear.Text.Trim(), fisCalYrId,
            dtFrmDate, dtTillDate);
    }


    public void GetMonthlyHour()
    {
        //decimal monlyHour = 0;
        string strDayhour = System.Web.Configuration.WebConfigurationManager.AppSettings["LeaveHour"];
        dtMonHour = timeSheetMgr.GET_TimeSheetPolicy(lblYear.Text.Trim(), mon.Trim());
        if (dtMonHour.Rows.Count > 0)
        {
            foreach (DataRow dr in dtMonHour.Rows)
            {
                monlyHour = Convert.ToDecimal(dr["PHour"].ToString());
                lblAssignedHour.Text = monlyHour.ToString();
            }
            //lblMsg.Text = "";
        }
        else
        {
            monlyHour = 0.0M;
            //lblMsg.Text = "No hour is assigned for this month-year";
        }
    }




    public void Cal(GridView grdvw)
    {
        decimal d1 = 0;
        decimal d2 = 0;
        decimal d3 = 0;
        decimal d4 = 0;
        decimal d5 = 0;
        decimal d6 = 0;

        decimal d7 = 0;
        decimal d8 = 0;
        decimal d9 = 0;
        decimal d10 = 0;
        decimal d11 = 0;
        decimal d12 = 0;

        decimal d13 = 0;
        decimal d14 = 0;
        decimal d15 = 0;
        decimal d16 = 0;
        decimal d17 = 0;
        decimal d18 = 0;

        decimal d19 = 0;
        decimal d20 = 0;
        decimal d21 = 0;
        decimal d22 = 0;
        decimal d23 = 0;
        decimal d24 = 0;
        decimal d25 = 0;
        decimal d26 = 0;
        decimal d27 = 0;
        decimal d28 = 0;
        decimal d29 = 0;
        decimal d30 = 0;
        decimal d31 = 0;


        foreach (GridViewRow gRow in grdvw.Rows)
        {
            if (gRow.Cells[3].Text == "")
                d1 = 0;
            else
                d1 = Convert.ToDecimal(gRow.Cells[3].Text);


            if (gRow.Cells[4].Text == "")
                d2 = 0;
            else
                d2 = Convert.ToDecimal(gRow.Cells[4].Text);



            if (gRow.Cells[5].Text == "")
                d3 = 0;
            else
                d3 = Convert.ToDecimal(gRow.Cells[5].Text);


            if (gRow.Cells[6].Text == "")
                d4 = 0;
            else
                d4 = Convert.ToDecimal(gRow.Cells[6].Text);


            if (gRow.Cells[7].Text == "")
                d5 = 0;
            else
                d5 = Convert.ToDecimal(gRow.Cells[7].Text);


            if (gRow.Cells[8].Text == "")
                d6 = 0;
            else
                d6 = Convert.ToDecimal(gRow.Cells[8].Text);


            if (gRow.Cells[9].Text == "")
                d7 = 0;
            else
                d7 = Convert.ToDecimal(gRow.Cells[9].Text);


            if (gRow.Cells[10].Text == "")
                d8 = 0;
            else
                d8 = Convert.ToDecimal(gRow.Cells[10].Text);


            if (gRow.Cells[11].Text == "")
                d9 = 0;
            else
                d9 = Convert.ToDecimal(gRow.Cells[11].Text);


            if (gRow.Cells[12].Text == "")
                d10 = 0;
            else
                d10 = Convert.ToDecimal(gRow.Cells[12].Text);


            if (gRow.Cells[13].Text == "")
                d11 = 0;
            else
                d11 = Convert.ToDecimal(gRow.Cells[13].Text);


            if (gRow.Cells[14].Text == "")
                d12 = 0;
            else
                d12 = Convert.ToDecimal(gRow.Cells[14].Text);


            if (gRow.Cells[15].Text == "")
                d13 = 0;
            else
                d13 = Convert.ToDecimal(gRow.Cells[15].Text);


            if (gRow.Cells[16].Text == "")
                d14 = 0;
            else
                d14 = Convert.ToDecimal(gRow.Cells[16].Text);


            if (gRow.Cells[17].Text == "")
                d15 = 0;
            else
                d15 = Convert.ToDecimal(gRow.Cells[17].Text);


            if (gRow.Cells[18].Text == "")
                d16 = 0;
            else
                d16 = Convert.ToDecimal(gRow.Cells[18].Text);



            if (gRow.Cells[19].Text == "")
                d17 = 0;
            else
                d17 = Convert.ToDecimal(gRow.Cells[19].Text);


            if (gRow.Cells[20].Text == "")
                d18 = 0;
            else
                d18 = Convert.ToDecimal(gRow.Cells[20].Text);



            if (gRow.Cells[21].Text == "")
                d19 = 0;
            else
                d19 = Convert.ToDecimal(gRow.Cells[21].Text);


            if (gRow.Cells[22].Text == "")
                d20 = 0;
            else
                d20 = Convert.ToDecimal(gRow.Cells[22].Text);


            if (gRow.Cells[23].Text == "")
                d21 = 0;
            else
                d21 = Convert.ToDecimal(gRow.Cells[23].Text);


            if (gRow.Cells[24].Text == "")
                d22 = 0;
            else
                d22 = Convert.ToDecimal(gRow.Cells[24].Text);


            if (gRow.Cells[25].Text == "")
                d23 = 0;
            else
                d23 = Convert.ToDecimal(gRow.Cells[25].Text);


            if (gRow.Cells[26].Text == "")
                d24 = 0;
            else
                d24 = Convert.ToDecimal(gRow.Cells[26].Text);


            if (gRow.Cells[27].Text == "")
                d25 = 0;
            else
                d25 = Convert.ToDecimal(gRow.Cells[27].Text);


            if (gRow.Cells[28].Text == "")
                d26 = 0;
            else
                d26 = Convert.ToDecimal(gRow.Cells[28].Text);


            if (gRow.Cells[29].Text == "")
                d27 = 0;
            else
                d27 = Convert.ToDecimal(gRow.Cells[29].Text);


            if (gRow.Cells[30].Text == "")
                d28 = 0;
            else
                d28 = Convert.ToDecimal(gRow.Cells[30].Text);


            if (gRow.Cells[31].Text == "")
                d29 = 0;
            else
                d29 = Convert.ToDecimal(gRow.Cells[31].Text);


            if (gRow.Cells[32].Text == "")
                d30 = 0;
            else
                d30 = Convert.ToDecimal(gRow.Cells[32].Text);


            if (gRow.Cells[33].Text == "")
                d31 = 0;
            else
                d31 = Convert.ToDecimal(gRow.Cells[33].Text);






            decimal res = d1 + d2 + d3 + d4 + d5 + d6 + d7 + d8 + d9 + d10 + d11 + d12 + d13 + d14 + d15 + d16 + d17 + d18 + d19 + d20 + d21 + d22 + d23 + d24 + d25 + d26 + d27 + d28 + d29 + d30 + d31; //Convert.ToDecimal(txtBox1.Text) + Convert.ToDecimal(txtBox2.Text) + Convert.ToDecimal(txtBox3.Text);
            gRow.Cells[34].Text = res.ToString();


        }

    }




    public void RatCal()
    {
        decimal totalWorkHour = 0;
        decimal ratio = 0;

        foreach (GridViewRow gRow in grTimeSheet.Rows)
        {
            if (gRow.Cells[1].Text != "")
            {
                ratio = Convert.ToDecimal(gRow.Cells[1].Text);
            }
            else
                ratio = 0;

            
            if (gRow.Cells[34].Text == "")
                totalWorkHour = 0;
            else
                totalWorkHour = Convert.ToDecimal(gRow.Cells[34].Text);
            


            decimal gTotal = Common.CheckNullString(grTimeSheet.FooterRow.Cells[34].Text) == "" ? Convert.ToDecimal("0") : Convert.ToDecimal(grTimeSheet.FooterRow.Cells[34].Text);
            if (gTotal > 0)
            {

                decimal res = ((totalWorkHour / gTotal) * 100);
                gRow.Cells[35].Text = res.ToString("f2") + "%";
            }

        }

    }



    protected void GetGrandTtal(GridView grv)
    {
        int i = 0;
        decimal gTotal = 0;

        if (grv.Rows.Count > 0)
        {
            foreach (GridViewRow gRow in grv.Rows)
            {
                
                string txtGrandTotal = gRow.Cells[34].Text;                


                gTotal = gTotal + Common.RoundDecimal(txtGrandTotal, 2);
                grv.Rows[i].Cells[34].HorizontalAlign = HorizontalAlign.Right;
                i++;
            }
            if (gTotal == 0)
                grv.FooterRow.Cells[34].Text = "";
            else
                grv.FooterRow.Cells[34].Text = gTotal.ToString();
            grv.FooterRow.Cells[34].HorizontalAlign = HorizontalAlign.Right;
        }
    }


    protected void GetSummaryTotal(GridView grv, int colCnt)
    {
        int i = 0;
        decimal dclSumValue = 0;
        if (grv.Rows.Count > 0)
        {
            grv.FooterRow.Cells[0].Text = "Total ";
            //grv.FooterRow.Cells[0].Font.Bold;
        
            for (int j = 0; j < grv.Columns.Count - colCnt; j++)
            {
                dclSumValue = 0;
                i = 0;
                foreach (GridViewRow gRow in grv.Rows)
                {
                    dclSumValue = dclSumValue + Common.RoundDecimal(gRow.Cells[j+3].Text, 2);
                    grv.Rows[i].Cells[j+3].HorizontalAlign = HorizontalAlign.Right;
                    i++;
                }
                if (dclSumValue == 0)
                    grv.FooterRow.Cells[j+3].Text = "";
                else
                    grv.FooterRow.Cells[j + 3].Text = dclSumValue.ToString("F1");
                grv.FooterRow.Cells[j+3].HorizontalAlign = HorizontalAlign.Right;
            }
        }
    }



    protected void GetFullSummary()
    {        
        lblWorkedHour.Text = grTimeSheet.FooterRow.Cells[34].Text;

        if (grLeave.Rows.Count > 0)
        {
            lblLeaveHour.Text = grLeave.FooterRow.Cells[34].Text;
        }
        else
            lblLeaveHour.Text = "0";


        decimal assignedHr = lblAssignedHour.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(lblAssignedHour.Text);
        decimal workedHr = lblWorkedHour.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(lblWorkedHour.Text);
        decimal leaveHr = lblLeaveHour.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(lblLeaveHour.Text);
        decimal wlHr = workedHr + leaveHr;
        lblWLHr.Text = wlHr.ToString();
        lblDiffHr.Text = Convert.ToString(assignedHr - wlHr);
    }


    //no require(test purpose)
    protected void grTimeSheet_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        int total = 0;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtTotal = (TextBox)e.Row.FindControl("txt1");
            total += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Amount"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblamount = (Label)e.Row.FindControl("lblTotal");
            lblamount.Text = total.ToString();
        }



        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            TextBox txt1 = (TextBox)e.Row.FindControl("txt1");
        }


    }
}
