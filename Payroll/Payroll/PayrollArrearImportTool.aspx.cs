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
using System.IO;
using System.Data.OleDb;

public partial class Payroll_Payroll_PayrollArrearImportTool : System.Web.UI.Page
{
    MasterTablesManager MasMgr = new MasterTablesManager();
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    BonusAllowanceManager objBonMgr = new BonusAllowanceManager();
    Payroll_MasterMgr objPayMstMgr = new Payroll_MasterMgr();
    Payroll_VariableAllowanceManager objVarMgr = new Payroll_VariableAllowanceManager();
    LeaveApplicationManager objLeaveMgr = new LeaveApplicationManager();
    DataTable dtSch = new DataTable();
    //PayrollReportManager objPayRptMgr = new PayrollReportManager();

    double dclTotWorkingDays = 0;
    double dclPreWorkingDays = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlArrearMonth);
            Common.FillMonthList(ddlMonth);
            Common.FillMonthList(ddlJoiningMonth);
            Common.FillMonthList(ddlMonthSearch);
            Common.FillYearList(10,ddlYear);
            ddlArrearMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlMonthSearch.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);

            ddlJoiningMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);

            Common.FillDropDownList(objPayMstMgr.SelectFiscalYear(0, "F"), ddlFiscalYr, "FISCALYRTITLE", "FISCALYRID", false);
            Common.FillDropDownList(objPayMstMgr.SelectFiscalYear(0, "F"), ddlFiscalYrSearch, "FISCALYRTITLE", "FISCALYRID", false);
            
            this.EntryMode();


            ddlArrearCase.SelectedValue = "1";
            ddlJoiningMonth.Visible = true;
            ddlJoiningMonth.SelectedValue = "11";
            ddlArrearMonth.SelectedValue = "11";
            ddlMonth.SelectedValue = "11";
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        this.EntryMode();
        this.InitializeSchDataTable();
        DataTable dtSalPakDetls = new DataTable();
        DateTime dtFrom, dtTo, dtFromMonthEndDate, dtToStartDate;
        int inMonthDays = 0;
        int iWeekendDays = 0;
        int inFromMonth = 0;
        int inFromYear = 0;
        TimeSpan ts;
        string strFromDate, strToDate;
        double dclDurInDays = 0;
        double dclPayAmt = 0;
        if (ddlArrearCase.SelectedValue == "-1")
        {
            lblMsg.Text = "Please select arrear case";
            ddlArrearCase.Focus();
            return;
        }
        else
            lblMsg.Text = "";        

        grArrrearDetails.Visible = false;

        switch (ddlArrearCase.SelectedValue)
        {

            #region Fractionnal Joining

            //case "1"://Fractional Joining     
            //    strFromDate = ddlYear.SelectedValue.ToString() + "/" + ddlArrearMonth.SelectedValue.ToString() + "/02";
            //    dtFrom = Convert.ToDateTime(strFromDate);
            //    strToDate = ddlYear.SelectedValue.ToString() + "/" + ddlArrearMonth.SelectedValue.ToString() + "/" + Common.GetMonthDay(dtFrom);
            //    dtTo = Convert.ToDateTime(strToDate);

            //    DataTable dtNewJoiner = objVarMgr.GetPayrollArrearData(ddlArrearMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(),
            //        ddlFiscalYr.SelectedValue.ToString(), ddlArrearCase.SelectedValue.ToString(), strFromDate, strToDate);
            //    if (dtNewJoiner.Rows.Count > 0)
            //    {
            //        foreach (DataRow dRow in dtNewJoiner.Rows)
            //        {
            //            iWeekendDays = Get_Days_Without_Weekend(dRow["EmpId"].ToString().Trim(), Common.ReturnDate(dRow["JoiningDate"].ToString().Trim()), Common.ReturnDate(strToDate));
            //            if (string.IsNullOrEmpty(dRow["SalaryPakId"].ToString()) == false)
            //            {
            //                dtSalPakDetls = objPayMstMgr.SelectSalaryPakDetls(Convert.ToInt32(dRow["SalaryPakId"].ToString()));
            //                foreach (DataRow dPakRow in dtSalPakDetls.Rows)
            //                {
            //                    if ((Convert.ToDecimal(dPakRow["PayAmt"].ToString()) != 0) && (dPakRow["SHeadId"].ToString() != "17"))
            //                    {
            //                        dtFrom = Convert.ToDateTime(dRow["JoiningDate"].ToString());
            //                        dtFromMonthEndDate = Convert.ToDateTime(dtFrom.Year.ToString() + "/" + dtFrom.Month.ToString() + "/" + Common.GetMonthDay(dtFrom));

            //                        inFromMonth = dtFrom.Month;
            //                        if (inFromMonth == 12)
            //                        {
            //                            inFromMonth = 1;
            //                            //inFromYear = dtFrom.Year + 1;
            //                        }
            //                        else
            //                        {
            //                            inFromMonth = dtFrom.Month + 1;
            //                            //inFromYear = dtFrom.Year;
            //                        }
            //                        dtToStartDate = Convert.ToDateTime(dtFrom.Year.ToString() + "/" + inFromMonth.ToString() + "/" + "1");

            //                        if ((dtFrom.Month == dtTo.Month) && (dtFrom.Year == dtTo.Year))
            //                        {
            //                            ts = dtTo - dtFrom;
            //                            //inMonthDays = Common.GetMonthDay(dtFrom);
            //                            //if ((dtFrom.Day == 1) && dtTo.Day == inMonthDays)
            //                            //{
            //                            //    this.AddScheduleData(dRow["EmpId"].ToString(), dRow["FullName"].ToString(), dRow["JoiningDate"].ToString(), dPakRow["SHeadId"].ToString(), dPakRow["HeadName"].ToString(),
            //                            //        dtFrom.Month.ToString(),Convert.ToDouble(dPakRow["PayAmt"]), dtFrom.Year.ToString(), ts.Days + 1,   dtFrom.ToString(), dtFromMonthEndDate.ToString());
            //                            //}
            //                            //else
            //                            //{
            //                            dclDurInDays = GetDaysDur(ts.Days + 1, dPakRow["SHeadId"].ToString(), iWeekendDays);
            //                            if (ddlArrearMonth.SelectedValue.ToString() == ddlMonth.SelectedValue.ToString())
            //                                dclPayAmt = Convert.ToDouble("-" + dPakRow["PayAmt"].ToString());
            //                            else
            //                                dclPayAmt = Convert.ToDouble(dPakRow["PayAmt"].ToString());

            //                            this.AddScheduleData(dRow["EmpId"].ToString(), dRow["FullName"].ToString(), dRow["JoiningDate"].ToString(), dPakRow["SHeadId"].ToString(), dPakRow["HeadName"].ToString(),
            //                                dtFrom.Month.ToString(), this.GetPayAmnt(dclDurInDays, dclPayAmt, dPakRow["SHeadId"].ToString(), ddlArrearCase.SelectedValue.ToString()),
            //                                dtFrom.Year.ToString(), dclDurInDays, dtFrom.ToString(), dtFromMonthEndDate.ToString());
            //                            //}
            //                        }
            //                        grPayrollArrear.DataSource = dtSch;
            //                        grPayrollArrear.DataBind();
            //                        this.SetGrScheduleSerial();
            //                    }
            //                }
            //            }
            //            dclTotWorkingDays = 0;
            //            dclPreWorkingDays = 0;
            //        }
            //    }
            //    break;
            #endregion
            #region Previous Month Joining
            case "2":
                //Get new joiner staffs those salary has not processed at earlier month
                strFromDate = ddlYear.SelectedValue.ToString() + "/" + ddlArrearMonth.SelectedValue.ToString() + "/02";
                dtFrom = Convert.ToDateTime(strFromDate);
                strToDate = ddlYear.SelectedValue.ToString() + "/" + ddlArrearMonth.SelectedValue.ToString() + "/" + Common.GetMonthDay(dtFrom);
                dtTo = Convert.ToDateTime(strToDate);

                DataTable dtPrevJoiner = objVarMgr.GetPayrollArrearData(ddlArrearMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(),
                    ddlFiscalYr.SelectedValue.ToString(), ddlArrearCase.SelectedValue.ToString(), strFromDate, strToDate);
                if (dtPrevJoiner.Rows.Count > 0)
                {
                    foreach (DataRow dRow in dtPrevJoiner.Rows)
                    {
                        iWeekendDays = Get_Days_Without_Weekend(dRow["EmpId"].ToString().Trim(), Common.ReturnDate(dRow["JoiningDate"].ToString().Trim()), Common.ReturnDate(strToDate));
                        if (string.IsNullOrEmpty(dRow["SalaryPakId"].ToString()) == false)
                        {
                            dtSalPakDetls = objPayMstMgr.SelectSalaryPakDetls(Convert.ToInt32(dRow["SalaryPakId"].ToString()));
                            foreach (DataRow dPakRow in dtSalPakDetls.Rows)
                            {
                                if ((Convert.ToDecimal(dPakRow["PayAmt"].ToString()) != 0) && (dPakRow["SHeadId"].ToString() != "17"))
                                {
                                    dtFrom = Convert.ToDateTime(dtFrom.Year.ToString() + "/" + dtFrom.Month.ToString() + "/01");
                                    //dtTo = Convert.ToDateTime(Common.ReturnDate("2014/08/31"));
                                    dtFromMonthEndDate = Convert.ToDateTime(dtFrom.Year.ToString() + "/" + dtFrom.Month.ToString() + "/" + Common.GetMonthDay(dtFrom));

                                    inFromMonth = dtFrom.Month;
                                    if (inFromMonth == 12)
                                    {
                                        inFromMonth = 1;
                                        //inFromYear = dtFrom.Year + 1;
                                    }
                                    else
                                    {
                                        inFromMonth = dtFrom.Month + 1;
                                        //inFromYear = dtFrom.Year;
                                    }
                                    dtToStartDate = Convert.ToDateTime(dtFrom.Year.ToString() + "/" + inFromMonth.ToString() + "/" + "1");

                                    if ((dtFrom.Month == dtTo.Month) && (dtFrom.Year == dtTo.Year))
                                    {
                                        ts = dtTo - dtFrom;
                                        inMonthDays = Common.GetMonthDay(dtFrom);
                                        if ((dtFrom.Day == 1) && dtTo.Day == inMonthDays)
                                        {
                                            this.AddScheduleData(dRow["EmpId"].ToString(), dRow["FullName"].ToString(), dRow["JoiningDate"].ToString(), dPakRow["SHeadId"].ToString(), dPakRow["HeadName"].ToString(),
                                                dtFrom.Month.ToString(), Convert.ToDouble(dPakRow["PayAmt"]), dtFrom.Year.ToString(), ts.Days + 1, dtFrom.ToString(), dtTo.ToString());
                                        }
                                        else
                                        {
                                            dclDurInDays = GetDaysDur(ts.Days + 1, dPakRow["SHeadId"].ToString(), iWeekendDays);
                                            this.AddScheduleData(dRow["EmpId"].ToString(), dRow["FullName"].ToString(), dRow["JoiningDate"].ToString(), dPakRow["SHeadId"].ToString(), dPakRow["HeadName"].ToString(),
                                                dtFrom.Month.ToString(), this.GetPayAmnt(dclDurInDays, Convert.ToDouble(dPakRow["PayAmt"].ToString()), dPakRow["SHeadId"].ToString(), ""),
                                                dtFrom.Year.ToString(), dclDurInDays,  dtFrom.ToString(), dtTo.ToString());
                                        }
                                    }
                                    grPayrollArrear.DataSource = dtSch;
                                    grPayrollArrear.DataBind();
                                    this.SetGrScheduleSerial();
                                }
                            }
                        }
                        dclTotWorkingDays = 0;
                        dclPreWorkingDays = 0;
                    }
                }
                break;
            #endregion
            #region LWOP
            case "3"://LWOP

                strFromDate = ddlYear.SelectedValue.ToString() + "/" + ddlArrearMonth.SelectedValue.ToString() + "/01";
                dtFrom = Convert.ToDateTime(strFromDate);
                strToDate = ddlYear.SelectedValue.ToString() + "/" + ddlArrearMonth.SelectedValue.ToString() + "/" + Common.GetMonthDay(dtFrom);
                dtTo = Convert.ToDateTime(strToDate);

                DataTable dtLWOP = objVarMgr.GetPayrollArrearData(ddlArrearMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(),
                    ddlFiscalYr.SelectedValue.ToString(), ddlArrearCase.SelectedValue.ToString(), strFromDate, strToDate);
                if (dtLWOP.Rows.Count > 0)
                {
                    foreach (DataRow dLWOPRow in dtLWOP.Rows)
                    {
                        if (string.IsNullOrEmpty(dLWOPRow["SalaryPakId"].ToString()) == false)
                        {
                            dtSalPakDetls = objPayMstMgr.SelectSalaryPakDetls(Convert.ToInt32(dLWOPRow["SalaryPakId"].ToString()));
                            DataRow[] foundLWOPRows = dtSalPakDetls.Select("SHeadId=1");


                            dtFrom = Convert.ToDateTime(dtFrom.Year.ToString() + "/" + dtFrom.Month.ToString() + "/01");
                            //inMonthDays = Common.GetMonthDay(dtFrom);
                            this.AddScheduleData(dLWOPRow["EmpId"].ToString().Trim(), dLWOPRow["FullName"].ToString().Trim(), dLWOPRow["JoiningDate"].ToString().Trim(),
                                foundLWOPRows[0]["SHeadId"].ToString(), foundLWOPRows[0]["HeadName"].ToString().Trim(), dtFrom.Month.ToString(),
                                this.GetPayAmnt(Convert.ToInt32(dLWOPRow["Duration"]), Convert.ToDouble("-" + dLWOPRow["BasicSal"].ToString()), foundLWOPRows[0]["SHeadId"].ToString(), ""),
                                dtFrom.Year.ToString(), Convert.ToDouble(dLWOPRow["Duration"]),  dtFrom.ToString(), dtTo.ToString());

                            dtSalPakDetls = objPayMstMgr.SelectSalaryPakDetls(Convert.ToInt32(dLWOPRow["SalaryPakId"].ToString()));
                            DataRow[] foundRows;
                            foundRows = dtSalPakDetls.Select("SHeadId=9 AND PayAmt<>0");

                            if (foundRows.Length > 0)
                                this.AddScheduleData(dLWOPRow["EmpId"].ToString().Trim(), dLWOPRow["FullName"].ToString().Trim(), dLWOPRow["JoiningDate"].ToString().Trim(),
                                    foundRows[0]["SHeadId"].ToString(), foundRows[0]["HeadName"].ToString().Trim(), dtFrom.Month.ToString(),
                                    Math.Round(Convert.ToDouble(this.GetPayAmnt(Convert.ToInt32(dLWOPRow["Duration"]), Convert.ToDouble(dLWOPRow["BasicSal"].ToString()), "1", ddlArrearCase.SelectedValue.ToString())) / 10),
                                    dtFrom.Year.ToString(), Convert.ToDouble(dLWOPRow["Duration"]),   dtFrom.ToString(), dtTo.ToString());
                        }

                        grPayrollArrear.DataSource = dtSch;
                        grPayrollArrear.DataBind();
                        this.SetGrScheduleSerial();
                    }
                }

                break;
            #endregion
            #region Confirmation
            case "1"://Confirmation    
                grPayrollArrear.Visible = true;
                grArrrearDetails.Visible = true;
                this.GetArrearDetails();
                break;
            #endregion
            #region Promotion
            case "4"://Promotion    
                grPayrollArrear.Visible = true;
                grArrrearDetails.Visible = true;
                this.GetArrearDetails();     
                break;
            #endregion
            #region Salary Amendment
            case "5"://Salary Amendment
                grPayrollArrear.Visible = true;
                grArrrearDetails.Visible = true;
                this.GetArrearDetails();
                break;
            #endregion
            #region APA
            case "6"://APA
                //grPayrollArrear.Visible = true;
                //grArrrearDetails.Visible = true;
                this.GetArrearDetails();
                break;
             #endregion

        }

        if (grPayrollArrear.Rows.Count == 0)
        {
            lblMsg.Text = "No Record Found...";
            return;
        }
    }
    protected double GetDaysDur(double dclDaysDur, string strSheadId, int iWeekendDays)
    {
        double dclDur = 0;
        //if (strSheadId == "1")
        //{
        //    //if (dclTotWorkingDays > 0)
        //    //{
        //    //    dclTotWorkingDays = dclDaysDur +dclTotWorkingDays;
        //    //    if (dclTotWorkingDays > 21.67)
        //    //        dclDur = 21.67 - dclPreWorkingDays;
        //    //}
        //    //else
        //        dclDur = Convert.ToDouble(dclDaysDur - iWeekendDays);

        //    dclTotWorkingDays = dclTotWorkingDays + dclDaysDur;
        //    dclPreWorkingDays = dclDur;
        //}
        //else
            dclDur = dclDaysDur;
        return Math.Round(dclDur, 2);
    }
    protected double GetPayAmnt(double dclDaysDur, double dclPayAmt, string strSheadId, string strArrearCase)
    {
        double decUnitDayAmnt = 0;
        double decPayAmnt = 0;
        double decMonthlyAmount = dclPayAmt;
   
        //if (strSheadId == "1" || strSheadId == "25")
        //    decPayAmnt = (dclPayAmt * 12) / 260 * dclDaysDur;
        //else
        //{
            decMonthlyAmount = dclPayAmt;
        //decUnitDayAmnt = decMonthlyAmount / 30;
        decUnitDayAmnt = decMonthlyAmount / Convert.ToDouble(Common.GetMonthDay(ddlJoiningMonth.SelectedValue, ddlYear.SelectedValue));
        decPayAmnt = decUnitDayAmnt * dclDaysDur;
        //}
        if (strArrearCase == "1")
        {
            if (strSheadId != "29")
                decPayAmnt = dclPayAmt - decPayAmnt;           
            else
                decPayAmnt = -decPayAmnt;
        }      

        return Math.Round(decPayAmnt);
    }

    protected void AddScheduleData(string strEmpId, string strFullName, string strJoiningDate, string strSHeadId, string strHeadName,
        string strMonth, double dclPayAmnt, string strYear, double dclDaysDur, string strValidFrom, string ValidTo)
    {
        DataRow nRow = dtSch.NewRow();

        nRow["SlNo"] = 1;
        nRow["EmpId"] = strEmpId;
        nRow["FullName"] = strFullName;
        nRow["JoiningDate"] = Common.DisplayDate(strJoiningDate);
        #region SHeadId
        switch (strSHeadId)
        {
            case "1":
            case "16":
                nRow["SHeadId"] = "16";
                break;
            case "2":
            case "17":
                nRow["SHeadId"] = "17";
                break;
            case "3":
            case "18":
                nRow["SHeadId"] = "18";
                break;          
            case "8":
            case "11":
                nRow["SHeadId"] = "11";
                break;                    
        }
        #endregion
        nRow["HeadName"] = objVarMgr.GetSalaryHeadName(nRow["SHeadId"].ToString());
        nRow["VMONTH"] = strMonth;
        nRow["VYEAR"] = strYear;
        nRow["VDAYS"] = dclDaysDur.ToString();
        nRow["PAYAMT"] = dclPayAmnt;
        nRow["ValidFrom"] =  Common.DisplayDate(strValidFrom);
        nRow["ValidTo"] = Common.DisplayDate(ValidTo);
        dtSch.Rows.Add(nRow);
        dtSch.AcceptChanges();
    }
    protected void SetGrScheduleSerial()
    {
        int i = 1;
        foreach (GridViewRow gRow in grPayrollArrear.Rows)
        {
            gRow.Cells[0].Text = i.ToString();           
            TextBox txtAmt = (TextBox)gRow.Cells[6].FindControl("txtPayAmnt");
            txtAmt.Text = Common.RoundDecimal(txtAmt.Text.Trim(), 2).ToString();
            i++;
        }
    }
    
    protected void InitializeSchDataTable()
    {
        dtSch = new DataTable();
        dtSch.Columns.Add("SlNo");
        dtSch.Columns.Add("EmpId");
        dtSch.Columns.Add("FullName");
        dtSch.Columns.Add("JoiningDate");
        dtSch.Columns.Add("SHeadId");
        dtSch.Columns.Add("HeadName");
        dtSch.Columns.Add("VMONTH");
        dtSch.Columns.Add("VYEAR");
        dtSch.Columns.Add("VDAYS");
        dtSch.Columns.Add("PAYAMT");
        dtSch.Columns.Add("ValidFrom");
        dtSch.Columns.Add("ValidTo");
    }
    private void GetArrearDetails()
    {
        DataTable dtSalPakDetls = new DataTable();
        DateTime dtFrom, dtTo, dtFromMonthEndDate, dtToStartDate;
        int inMonthDays = 0;
        //int iWeekendDays = 0;
        int inFromMonth = 0;
        int inFromYear = 0;
        TimeSpan ts;
        string strFromDate, strToDate;
        string strLogId = "";
        int iWeekendDaysPart1 = 0, iWeekendDaysPart2 = 0;
        double dclDurInDays = 0;

        dclTotWorkingDays = 0;
        strFromDate = ddlYear.SelectedValue.ToString() + "/" + ddlJoiningMonth.SelectedValue.ToString() + "/02";
        dtFrom = Convert.ToDateTime(strFromDate);
        strToDate = ddlYear.SelectedValue.ToString() + "/" + ddlJoiningMonth.SelectedValue.ToString() + "/" + Common.GetMonthDay(dtFrom);
        dtTo = Convert.ToDateTime(strToDate);

        DataTable dtAction = objVarMgr.GetPayrollArrearData(ddlArrearMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(),
            ddlFiscalYr.SelectedValue.ToString(), ddlArrearCase.SelectedValue.ToString(), strFromDate, strToDate);
        #region Promotion Frational Calculation
        if (dtAction.Rows.Count > 0)
        {
            #region Joining & Effective month is same
            if (ddlJoiningMonth.SelectedValue.ToString() == ddlArrearMonth.SelectedValue.ToString())
            {
                foreach (DataRow dRow in dtAction.Rows)
                {
                    //Second Part of Action
                    iWeekendDaysPart2 = Get_Days_Without_Weekend(dRow["EmpId"].ToString().Trim(), Common.ReturnDate(dRow["EffDate"].ToString().Trim()), Common.ReturnDate(strToDate));
                    if (string.IsNullOrEmpty(dRow["SalaryPakId"].ToString()) == false)
                    {
                        dtSalPakDetls = objPayMstMgr.SelectSalaryPakDetls(Convert.ToInt32(dRow["SalaryPakId"].ToString()));
                        foreach (DataRow dPakRow in dtSalPakDetls.Rows)
                        {
                            if ((Convert.ToDecimal(dPakRow["PayAmt"].ToString()) != 0) && ((dPakRow["SHeadId"].ToString() == "1") || (dPakRow["SHeadId"].ToString() == "2")
                                || (dPakRow["SHeadId"].ToString() == "3") || (dPakRow["SHeadId"].ToString() == "8")))
                            {
                                dtFrom = Convert.ToDateTime(dRow["EffDate"].ToString().Trim());
                                dtTo = Convert.ToDateTime(dtFrom.Year.ToString() + "/" + dtFrom.Month.ToString() + "/" + Common.GetMonthDay(dtFrom));
                                dtFromMonthEndDate = Convert.ToDateTime(dtFrom.Year.ToString() + "/" + dtFrom.Month.ToString() + "/" + Common.GetMonthDay(dtFrom));

                                //dtEffDate = Convert.ToDateTime(dRow["EffDate"].ToString());
                                inFromMonth = dtFrom.Month;
                                if (inFromMonth == 12)
                                {
                                    inFromMonth = 1;
                                    //inFromYear = dtFrom.Year + 1;
                                }
                                else
                                {
                                    inFromMonth = dtFrom.Month + 1;
                                    //inFromYear = dtFrom.Year;
                                }
                                dtToStartDate = Convert.ToDateTime(dRow["EffDate"].ToString()); //Convert.ToDateTime(inFromYear.ToString() + "/" + inFromMonth.ToString() + "/" + "1");

                                if ((dtFrom.Month == dtTo.Month) && (dtFrom.Year == dtTo.Year))
                                {
                                    ts = dtTo - dtFrom;
                                    inMonthDays = Common.GetMonthDay(dtFrom);
                                    if ((dtFrom.Day == 1) && dtTo.Day == inMonthDays)
                                    {
                                        //this.AddScheduleData(dRow["EmpId"].ToString(), dRow["FullName"].ToString(), dRow["JoiningDate"].ToString(), dPakRow["SHeadId"].ToString(), dPakRow["HeadName"].ToString(),
                                        //    dtFrom.Month.ToString(), dPakRow["PayAmt"].ToString(), dtFrom.Year.ToString(), Convert.ToString(ts.Days + 1), "0", dtFrom.Month.ToString(), dtFrom.ToString(), dtTo.ToString());
                                        ////this.AddScheduleData(dRow["EmpId"].ToString(), dRow["FullName"].ToString(), dRow["JoiningDate"].ToString(), dPakRow["SHeadId"].ToString(), dPakRow["HeadName"].ToString(),
                                        ////    dtFrom.Month.ToString(), dPakRow["PayAmt"].ToString(), dtFrom.Year.ToString(), Convert.ToString(ts.Days + 1), "0", dtFrom.Month.ToString(), dtFrom.ToString(), dtTo.ToString());
                                    }
                                    else
                                    {
                                      
                                        dclDurInDays = GetDaysDur(ts.Days + 1, dPakRow["SHeadId"].ToString(), iWeekendDaysPart2);
                                        this.AddScheduleData(dRow["EmpId"].ToString(), dRow["FullName"].ToString(), dRow["JoiningDate"].ToString(), dPakRow["SHeadId"].ToString(), dPakRow["HeadName"].ToString(),
                                            dtFrom.Month.ToString(), this.GetPayAmnt(dclDurInDays, Convert.ToDouble(dPakRow["PayAmt"].ToString()), dPakRow["SHeadId"].ToString(),  ""),
                                            dtFrom.Year.ToString(), dclDurInDays,  dtFrom.ToString(), dtTo.ToString());
                                    }
                                }
                                grArrrearDetails.DataSource = dtSch;
                                grArrrearDetails.DataBind();
                                this.SetGrScheduleSerial();
                            }
                        }
                    }

                    //First Part of Action
                    int iDaysDur = 0;
                    strLogId = objPayMstMgr.Select2ndMaxLogId(dRow["EmpId"].ToString().Trim());
                    iWeekendDaysPart1 = Get_Days_Without_Weekend(dRow["EmpId"].ToString().Trim(), Common.ReturnDate(strFromDate), Common.ReturnDate(dRow["EffDate"].ToString().Trim()));
                    if (string.IsNullOrEmpty(dRow["SalaryPakId"].ToString()) == false)
                    {
                        dtSalPakDetls = objPayMstMgr.SelectSalaryPakHisDetls(Convert.ToInt32(dRow["SalaryPakId"].ToString()), dRow["EmpId"].ToString().Trim(), strLogId);
                        foreach (DataRow dPakRow in dtSalPakDetls.Rows)
                        {
                            if ((Convert.ToDecimal(dPakRow["PayAmt"].ToString()) != 0) && ((dPakRow["SHeadId"].ToString() == "1") || (dPakRow["SHeadId"].ToString() == "2")
                                || (dPakRow["SHeadId"].ToString() == "3")|| (dPakRow["SHeadId"].ToString() == "8")))
                            {
                                dtFrom = Convert.ToDateTime(dtFrom.Year.ToString() + "/" + dtFrom.Month.ToString() + "/01");
                                dtTo = Convert.ToDateTime(dRow["EffDate"].ToString().Trim());
                                dtTo = dtTo.AddDays(-1);
                                dtFromMonthEndDate = Convert.ToDateTime(dtFrom.Year.ToString() + "/" + dtFrom.Month.ToString() + "/" + Common.GetMonthDay(dtFrom));
                                inFromMonth = dtFrom.Month;
                                if (inFromMonth == 12)
                                {
                                    inFromMonth = 1;
                                    //inFromYear = dtFrom.Year + 1;
                                }
                                else
                                {
                                    inFromMonth = dtFrom.Month + 1;
                                    //inFromYear = dtFrom.Year;
                                }
                                dtToStartDate = Convert.ToDateTime(dRow["EffDate"].ToString()); //Convert.ToDateTime(inFromYear.ToString() + "/" + inFromMonth.ToString() + "/" + "1");

                                if ((dtFrom.Month == dtTo.Month) && (dtFrom.Year == dtTo.Year))
                                {
                                    ts = dtTo - dtFrom;
                                    inMonthDays = Common.GetMonthDay(dtFrom);
                                    //if (inMonthDays == 31)
                                    //    iDaysDur = ts.Days;
                                    //else
                                        iDaysDur = ts.Days + 1;
                                    if ((dtFrom.Day == 1) && dtTo.Day == inMonthDays)
                                    {
                                        this.AddScheduleData(dRow["EmpId"].ToString(), dRow["FullName"].ToString(), dRow["JoiningDate"].ToString(), dPakRow["SHeadId"].ToString(), dPakRow["HeadName"].ToString(),
                                            dtFrom.Month.ToString(),Convert.ToDouble(dPakRow["PayAmt"]), dtFrom.Year.ToString(), ts.Days + 1,  dtFrom.ToString(), dtTo.ToString());
                                    }
                                    else
                                    {
                                       
                                        dclDurInDays = GetDaysDur(iDaysDur, dPakRow["SHeadId"].ToString(), iWeekendDaysPart1);
                                        this.AddScheduleData(dRow["EmpId"].ToString(), dRow["FullName"].ToString(), dRow["JoiningDate"].ToString(), dPakRow["SHeadId"].ToString(), dPakRow["HeadName"].ToString(),
                                            dtFrom.Month.ToString(), this.GetPayAmnt(dclDurInDays, Convert.ToDouble(dPakRow["PayAmt"].ToString()), dPakRow["SHeadId"].ToString(),  ""),
                                            dtFrom.Year.ToString(), dclDurInDays,   dtFrom.ToString(), dtTo.ToString());// Convert.ToString(iDaysDur)

                                    }
                                }
                                grArrrearDetails.DataSource = dtSch;
                                grArrrearDetails.DataBind();
                                this.SetGrScheduleSerial();
                            }
                        }
                    }
                    dclTotWorkingDays = 0;
                    dclPreWorkingDays = 0;
                }
            }
            #endregion
            //Full Promotion amount Calculation for next arrear month
            else
            {
                foreach (DataRow dRow in dtAction.Rows)
                {
                    if (string.IsNullOrEmpty(dRow["SalaryPakId"].ToString()) == false)
                    {
                        dtSalPakDetls = objPayMstMgr.SelectSalaryPakDetls(Convert.ToInt32(dRow["SalaryPakId"].ToString()));
                        foreach (DataRow dPakRow in dtSalPakDetls.Rows)
                        {
                            if ((Convert.ToDecimal(dPakRow["PayAmt"].ToString()) != 0) && ((dPakRow["SHeadId"].ToString() == "1") || (dPakRow["SHeadId"].ToString() == "2")
                                || (dPakRow["SHeadId"].ToString() == "3") || (dPakRow["SHeadId"].ToString() == "8")))
                            {
                                dtFrom = Convert.ToDateTime(dtFrom.Year.ToString() + "/" + dtFrom.Month.ToString() + "/01");
                                dtFromMonthEndDate = Convert.ToDateTime(dtFrom.Year.ToString() + "/" + dtFrom.Month.ToString() + "/" + Common.GetMonthDay(dtFrom));

                                inFromMonth = dtFrom.Month;
                                if (inFromMonth == 12)
                                {
                                    inFromMonth = 1;
                                    inFromYear = dtFrom.Year + 1;
                                }
                                else
                                {
                                    inFromMonth = dtFrom.Month + 1;
                                    inFromYear = dtFrom.Year;
                                }
                                dtToStartDate = Convert.ToDateTime(inFromYear.ToString() + "/" + inFromMonth.ToString() + "/" + "1");

                                if ((dtFrom.Month == dtTo.Month) && (dtFrom.Year == dtTo.Year))
                                {
                                    ts = dtTo - dtFrom;
                                    inMonthDays = Common.GetMonthDay(dtFrom);
                                    if ((dtFrom.Day == 1) && dtTo.Day == inMonthDays)
                                    {
                                        this.AddScheduleData(dRow["EmpId"].ToString(), dRow["FullName"].ToString(), dRow["JoiningDate"].ToString(), dPakRow["SHeadId"].ToString(), dPakRow["HeadName"].ToString(),
                                            dtFrom.Month.ToString(), Convert.ToDouble(dPakRow["PayAmt"]), dtFrom.Year.ToString(), ts.Days + 1, dtFrom.ToString(), dtTo.ToString());
                                    }
                                }
                                grArrrearDetails.DataSource = dtSch;
                                grArrrearDetails.DataBind();
                                this.SetGrScheduleSerial();
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Promotion Arrear
        
        DataTable dtArrearDtls=new DataTable ();
        dtArrearDtls = dtSch.Clone();  
        foreach (DataRow dRowArr in dtSch.Rows)
        {
            dtArrearDtls.ImportRow(dRowArr);
        }

        dtSch.Rows.Clear();
        dtSch.Dispose();
        foreach (DataRow dPRow in dtAction.Rows)
        {
            DataTable dtPayslip = objVarMgr.GetSalaryData(dPRow["EmpId"].ToString().Trim()  , ddlArrearMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), ddlFiscalYr.SelectedValue.ToString(), "Y");
            if (dtPayslip.Rows.Count > 0)
                GetArrearMst(dtPayslip, dtArrearDtls);
            else
            {
                dtSalPakDetls = objVarMgr.GetSalaryData(dPRow["EmpId"].ToString().Trim(), ddlArrearMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), ddlFiscalYr.SelectedValue.ToString(), "N");
                GetArrearMst(dtSalPakDetls, dtArrearDtls);
            }
        }
        #endregion
    }

    private void GetArrearMst(DataTable dtSalary, DataTable dtArrearDtls)
    {

        double dblTotDurs = 0;
        double dblTotAmt = 0;
        double dblBasicAmt = 0, dblHRAmt=0, dblMedAmt=0, dblPFAmt = 0;
        string lvPayAmt;
        string strSHeadId = "";
        string strFullName = "";
        string strJoinDate = "";

        int i = 0;

        string strFromDate, strToDate;
        DateTime dtFrom, dtTo;

        strFromDate = ddlYear.SelectedValue.ToString() + "/" + ddlArrearMonth.SelectedValue.ToString() + "/01";
        dtFrom = Convert.ToDateTime(strFromDate);
        strToDate = ddlYear.SelectedValue.ToString() + "/" + ddlArrearMonth.SelectedValue.ToString() + "/" + Common.GetMonthDay(dtFrom);
        dtTo = Convert.ToDateTime(strToDate);
        //foreach (GridViewRow gRow in grArrrearDetails.Rows)
        //{
        DataRow[] foundRows = dtArrearDtls.Select("EmpId='" + dtSalary.Rows[0]["EmpId"].ToString().Trim() + "'");
        dblTotDurs = Common.GetMonthDay(ddlJoiningMonth.SelectedValue, ddlYear.SelectedValue);

        foreach (DataRow dDtlsRow in foundRows)
        {
            strFullName = dDtlsRow["FullName"].ToString().Trim();
            strJoinDate = dDtlsRow["JoiningDate"].ToString().Trim();
            if ((dDtlsRow["SHeadId"].ToString() == "1") || (dDtlsRow["SHeadId"].ToString() == "16"))
                dblBasicAmt = dblBasicAmt + Convert.ToDouble(dDtlsRow["PayAmt"]);
            if ((dDtlsRow["SHeadId"].ToString() == "2") || (dDtlsRow["SHeadId"].ToString() == "17"))
                dblHRAmt = dblHRAmt + Convert.ToDouble(dDtlsRow["PayAmt"]);
            if ((dDtlsRow["SHeadId"].ToString() == "3") || (dDtlsRow["SHeadId"].ToString() == "18"))
                dblMedAmt = dblMedAmt + Convert.ToDouble(dDtlsRow["PayAmt"]);
            if ((dDtlsRow["SHeadId"].ToString() == "8") || (dDtlsRow["SHeadId"].ToString() == "11"))
                dblPFAmt = dblPFAmt + Convert.ToDouble(dDtlsRow["PayAmt"]);
        }
        if (dblBasicAmt > 0)
        {
            this.AddScheduleData(dtSalary.Rows[0]["EmpId"].ToString().Trim(), strFullName, strJoinDate,
                     "1", "Basic", dtFrom.Month.ToString(), dblBasicAmt, ddlYear.SelectedValue.ToString(), dblTotDurs, dtFrom.ToString(), dtTo.ToString());
            dblBasicAmt = 0;
        }
        if (dblHRAmt > 0)
        {
            this.AddScheduleData(dtSalary.Rows[0]["EmpId"].ToString().Trim(), strFullName, strJoinDate,
                     "2", "House Rent", dtFrom.Month.ToString(), dblHRAmt, ddlYear.SelectedValue.ToString(), dblTotDurs, dtFrom.ToString(), dtTo.ToString());
            dblHRAmt = 0;
        }
        if (dblMedAmt > 0)
        {
            this.AddScheduleData(dtSalary.Rows[0]["EmpId"].ToString().Trim(), strFullName, strJoinDate,
                     "3", "Medical", dtFrom.Month.ToString(), dblMedAmt, ddlYear.SelectedValue.ToString(), dblTotDurs, dtFrom.ToString(), dtTo.ToString());
            dblMedAmt = 0;
        }
        if (dblPFAmt < 0)
        {
            this.AddScheduleData(dtSalary.Rows[0]["EmpId"].ToString().Trim(), strFullName, strJoinDate,
                     "8", "PF", dtFrom.Month.ToString(), dblPFAmt, ddlYear.SelectedValue.ToString(), dblTotDurs, dtFrom.ToString(), dtTo.ToString());
            dblPFAmt = 0;
        }

        grPayrollArrear.DataSource = dtSch;
        grPayrollArrear.DataBind();
        this.SetGrScheduleSerial();

        //}
    }
    //private void GetArrearMst(DataTable dtSalary, DataTable dtArrearDtls)
    //{
    //    double dblTotDurs = 0;
    //    double dblTotAmt = 0;
    //    double dblSalaryAmt = 0;
    //    string lvPayAmt;       
    //    string strSHeadId = "";
    //    int i = 0;

    //    string strFromDate, strToDate;
    //    DateTime dtFrom, dtTo;

    //    strFromDate = ddlYear.SelectedValue.ToString() + "/" + ddlArrearMonth.SelectedValue.ToString() + "/01";
    //    dtFrom = Convert.ToDateTime(strFromDate);
    //    strToDate = ddlYear.SelectedValue.ToString() + "/" + ddlArrearMonth.SelectedValue.ToString() + "/" + Common.GetMonthDay(dtFrom);
    //    dtTo = Convert.ToDateTime(strToDate);
        
    //    DataRow[] foundRows = dtArrearDtls.Select("EmpId='" + dtSalary.Rows[0]["EmpId"].ToString().Trim() + "'");

    //    foreach (DataRow dRow in dtSalary.Rows)
    //    {
    //        strSHeadId = dRow["SHeadId"].ToString();
    //        dblTotDurs = 0;
    //        dblTotAmt = 0;
    //        i = 0;

    //        foreach (DataRow dDtlsRow in foundRows)
    //        {
    //            if ((dRow["SHeadId"].ToString() == "1") && (dDtlsRow["SHeadId"].ToString() == "16"))
    //                dblSalaryAmt = Convert.ToDouble(dRow["PayAmt"]);
    //            if ((dRow["SHeadId"].ToString() == "2") && (dDtlsRow["SHeadId"].ToString() == "17"))
    //                dblSalaryAmt = Convert.ToDouble(dRow["PayAmt"]);
    //            if ((dRow["SHeadId"].ToString() == "3") && (dDtlsRow["SHeadId"].ToString() == "18"))
    //                dblSalaryAmt = Convert.ToDouble(dRow["PayAmt"]);
    //            //if ((dRow["SHeadId"].ToString() == "4") && (dDtlsRow["SHeadId"].ToString() == "22"))
    //            //    dblSalaryAmt = Convert.ToDouble(dRow["PayAmt"]);
    //            //if ((dRow["SHeadId"].ToString() == "7") && (dDtlsRow["SHeadId"].ToString() == "23"))
    //            //    dblSalaryAmt = Convert.ToDouble(dRow["PayAmt"]);

    //            if (strSHeadId == dDtlsRow["SHeadId"].ToString())
    //            {
    //                i++;
    //                lvPayAmt = dDtlsRow["PayAmt"].ToString(); 

    //                dblTotDurs = dblTotDurs + Convert.ToDouble(dDtlsRow["VDAYS"].ToString());
    //                dblTotAmt = dblTotAmt + Convert.ToDouble(lvPayAmt);

    //                if ((i == 1) || (i == 2))
    //                {
    //                    dblTotAmt = dblTotAmt - dblSalaryAmt;
    //                    this.AddScheduleData(dRow["EmpId"].ToString().Trim(), dDtlsRow["FullName"].ToString().Trim(), Common.ReturnDate(dDtlsRow["JoiningDate"].ToString().Trim()),
    //                             dRow["SHeadId"].ToString(), dDtlsRow["HeadName"].ToString().Trim(), dtFrom.Month.ToString(),
    //                              dblTotAmt, "", dblTotDurs,dtFrom.ToString(),dtTo.ToString());

    //                    if (dDtlsRow["SHeadId"].ToString() == "16")
    //                        this.AddScheduleData(dRow["EmpId"].ToString().Trim(), dDtlsRow["FullName"].ToString().Trim(), Common.ReturnDate(dDtlsRow["JoiningDate"].ToString().Trim()),
    //                            "8", "PF Emp Arrear", dtFrom.Month.ToString(),
    //                            Math.Round(Convert.ToDouble(this.GetPayAmnt(dblTotDurs, -dblTotAmt, "1", ddlArrearCase.SelectedValue.ToString())) / 10),
    //                            dtFrom.Year.ToString(), dblTotDurs, dtFrom.ToString(), dtTo.ToString());

    //                    grPayrollArrear.DataSource = dtSch;
    //                    grPayrollArrear.DataBind();
    //                    this.SetGrScheduleSerial();
    //                }
    //            }
    //        }
    //    }
    //}

    protected void btnArrearSave_Click(object sender, EventArgs e)
    {
        this.ValidateandSave("N");
    }

    protected void ValidateandSave(string IsDelete)
    {
        if (grPayrollArrear.Rows.Count == 0)
        {
            lblMsg.Text = "No Record to Save";
            return;
        }

        // validate with From date
        foreach (GridViewRow gRow in grPayrollArrear.Rows)
        {
            if (objVarMgr.IsDuplicateArrearData(gRow.Cells[1].Text.Trim(), gRow.Cells[4].Text.Trim(), ddlMonth.SelectedValue.ToString(),
                ddlArrearMonth.SelectedValue.ToString(), ddlFiscalYr.SelectedValue.ToString(), ddlArrearCase.SelectedValue.ToString()) == true)
            {
                lblMsg.Text = "Duplicate record cannot save." + gRow.Cells[1].Text.Trim() + " staffs " + gRow.Cells[5].Text.Trim() + " salary heads data already exist for this month.";
                return;
            }
        }
        this.SaveData(IsDelete);
    }

    protected void SaveData(string IsDelete)
    {
        try
        {
            objVarMgr.InsertPayrollArrear(grPayrollArrear, ddlArrearCase.SelectedValue.ToString(), ddlArrearMonth.SelectedValue.ToString(), ddlFiscalYr.SelectedValue.Trim(), ddlMonth.SelectedValue.ToString(),
                 Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()), "Payroll Arrear Import");

            lblMsg.Text = "Payroll arrear data has been saved successfully.";
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected int Get_Days_Without_Weekend(string strEmpId, string strFromDate, string strToDate)
    {
        double TotDay = 0;
        DateTime dtFrom = new DateTime();
        DateTime dtTo = new DateTime();
        int dblTotWeekedDay = 0;
        if (string.IsNullOrEmpty(strToDate) == false && string.IsNullOrEmpty(strFromDate) == false)
        {
            char[] splitter ={ '/' };
            string[] arinfo = Common.str_split(strFromDate, splitter);
            if (arinfo.Length == 3)
            {
                dtFrom = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                arinfo = null;
            }
            arinfo = Common.str_split(strToDate, splitter);
            if (arinfo.Length == 3)
            {
                dtTo = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                arinfo = null;
            }

            TimeSpan Dur = dtTo.Subtract(dtFrom);

            TotDay = Math.Round(Convert.ToDouble(Dur.Days), 0) + 1;
        }
        DataTable dtEmpWeekend = new DataTable();
        dtEmpWeekend = objLeaveMgr.SelectEmpWiseWeekend(strEmpId);

        int row;
        DateTime LDate = dtFrom;
        for (row = 0; row < Convert.ToInt32(TotDay); row++)
        {
            if (dtEmpWeekend.Rows.Count > 0)
            {
                string DayName = LDate.DayOfWeek.ToString();
                switch (DayName)
                {
                    case "Friday":
                        {
                            if (dtEmpWeekend.Rows[0]["WEFri"].ToString() == "Y")
                            {
                                dblTotWeekedDay++;
                            }
                            break;
                        }
                    case "Saturday":
                        {
                            if (dtEmpWeekend.Rows[0]["WESat"].ToString() == "Y")
                            {
                                dblTotWeekedDay++;
                            }
                            break;
                        }
                }
                LDate = LDate.AddDays(1);
            }
        }
        TotDay = 0;
        return dblTotWeekedDay;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.EntryMode();
    }

    private void EntryMode()
    {
        grPayrollArrear.DataSource = null;
        grPayrollArrear.DataBind();

        grArrrearDetails.DataSource = null;
        grArrrearDetails.DataBind();
        TabContainer1.ActiveTabIndex = 0; 
    }
    protected void ddlArrearCase_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblArrearDtls.Visible = false;
        grArrrearDetails.Visible = false; 
        switch (ddlArrearCase.SelectedValue)
        {
            case "1":
                lblJoiningMonth.Text = "Confirmation Month";
                lblArrearMonth.Text = "Arrear Month";
                lblJoiningMonth.Visible = true;
                ddlJoiningMonth.Visible = true;
                lblArrearDtls.Visible = true;
                grArrrearDetails.Visible = true;
                break;
            //case "2":
            //    lblJoiningMonth.Text = "Joining Month";
            //    lblArrearMonth.Text = "Salary Held Up Month";
            //    lblJoiningMonth.Visible = true;
            //    ddlJoiningMonth.Visible = true;
            //    break;
            //case "3":           
            //    lblJoiningMonth.Visible = false;
            //    ddlJoiningMonth.Visible = false;
            //    lblArrearMonth.Text = "Arrear Month";
            //    break;
            case "4":
                lblJoiningMonth.Text = "Promotion Month";
                lblArrearMonth.Text = "Arrear Month";
                lblJoiningMonth.Visible = true;
                ddlJoiningMonth.Visible = true;
                lblArrearDtls.Visible = true;
                grArrrearDetails.Visible = true;
                break;
            case "5":
                lblJoiningMonth.Text = "Increment Month";
                lblArrearMonth.Text = "Arrear Month";
                lblJoiningMonth.Visible = true;
                ddlJoiningMonth.Visible = true;
                lblArrearDtls.Visible = true;
                grArrrearDetails.Visible = true;
                break;
            case "6":
                lblJoiningMonth.Text = "Appraisal Month";
                lblArrearMonth.Text = "Arrear Month";
                lblJoiningMonth.Visible = true;
                ddlJoiningMonth.Visible = true;
                lblArrearDtls.Visible = true;
                grArrrearDetails.Visible = true;
                break;
        }
        this.EntryMode();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dtArrearList = new DataTable();
        grList.DataSource = null;
        grList.DataBind();
        int i=1;
        dtArrearList = objVarMgr.GetPayrollArrearList(ddlMonthSearch.SelectedValue.ToString(), ddlFiscalYrSearch.SelectedValue.ToString());
        if (dtArrearList.Rows.Count > 0)
        {
            grList.DataSource = dtArrearList;
            grList.DataBind();

            foreach (GridViewRow gRow in grList.Rows)
            {
                gRow.Cells[0].Text = i.ToString();

                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[3].Text)) == false)
                    gRow.Cells[3].Text = Common.DisplayDate(gRow.Cells[3].Text);

                if (gRow.Cells[5].Text == "1")
                    gRow.Cells[5].Text = "Fractional Joining";
                else if (gRow.Cells[5].Text == "2")
                    gRow.Cells[5].Text = "Previous Month Joining";
                else if (gRow.Cells[5].Text == "3")
                    gRow.Cells[5].Text = "LWOP";
                
                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[9].Text)) == false)
                    gRow.Cells[9].Text = Common.DisplayDate(gRow.Cells[9].Text);

                if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[10].Text)) == false)
                    gRow.Cells[10].Text = Common.DisplayDate(gRow.Cells[10].Text);

                i++;
            }
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtPayrollProcess = new DataTable();
            Payroll_PayslipApprovalManager objPreMgr = new Payroll_PayslipApprovalManager();
            string strEmpId = "";
            if (grPayrollArrear.Rows.Count == 0)
            {
                lblMsg.Text = "No Record Found...";
                return;
            }
            //dtPayrollProcess = objPreMgr.GetPayslipPreparedData("A", "", ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), "");
            dtPayrollProcess = objPreMgr.GetPayslipPreparedData("A", "", ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), "", "1");
            if (dtPayrollProcess.Rows.Count > 0)
            {
                ////DataTable dtArrearDtls = new DataTable();
                ////dtArrearDtls = dtSch.Clone();
                ////foreach (DataRow dRowArr in dtSch.Rows)
                ////{
                ////    dtArrearDtls.ImportRow(dRowArr);
                ////}

                ////dtSch.Rows.Clear();
                ////dtSch.Dispose();

                foreach (GridViewRow gRow in grPayrollArrear.Rows)
                {
                    DataRow[] foundRows = dtPayrollProcess.Select("EMPID=" + gRow.Cells[1].Text.Trim());

                    if (foundRows.Length > 0)
                    {
                        if (strEmpId == "")
                            strEmpId = gRow.Cells[1].Text.Trim();
                        else
                            strEmpId = strEmpId + "," + gRow.Cells[1].Text.Trim();
                    }
                }
            }
            if ((dtPayrollProcess.Rows.Count == 0) || (strEmpId == ""))
            {
                objVarMgr.DeletePayrollArrearData(ddlMonth.SelectedValue.ToString(), ddlFiscalYr.SelectedValue.Trim(), ddlArrearCase.SelectedValue.ToString());
                lblMsg.Text = "Payroll arrear data has been deleted successfully.";
                this.EntryMode();
            }
            else
            {
                lblMsg.Text = "Payroll arrear data can not delete for " + ddlMonth.SelectedItem.Text.Trim() + " month of " + ddlArrearCase.SelectedItem.Text.Trim() + " case. Salary has already prepare for this " + strEmpId + " staffs.";
            }
            //DataTable dtPayrollProcess = new DataTable();
            //Payroll_PayslipApprovalManager objPreMgr = new Payroll_PayslipApprovalManager();
            //string strEmpId = "";
            //if (grPayrollArrear.Rows.Count == 0)
            //{
            //    lblMsg.Text = "No Record Found...";
            //    return;
            //}
            //dtPayrollProcess = objPreMgr.GetPayslipPreparedData("A", "", ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), "", "");
            //if (dtPayrollProcess.Rows.Count > 0)
            //{
            //    ////DataTable dtArrearDtls = new DataTable();
            //    ////dtArrearDtls = dtSch.Clone();
            //    ////foreach (DataRow dRowArr in dtSch.Rows)
            //    ////{
            //    ////    dtArrearDtls.ImportRow(dRowArr);
            //    ////}

            //    ////dtSch.Rows.Clear();
            //    ////dtSch.Dispose();

            //    foreach (GridViewRow gRow in grPayrollArrear.Rows)
            //    {
            //        DataRow[] foundRows = dtPayrollProcess.Select("EMPID=" + gRow.Cells[1].Text.Trim());

            //        if (foundRows.Length > 0)
            //        {
            //            if (strEmpId == "")
            //                strEmpId = gRow.Cells[1].Text.Trim();
            //            else
            //                strEmpId = strEmpId + "," + gRow.Cells[1].Text.Trim();
            //        }
            //    }
            //}
            //if ((dtPayrollProcess.Rows.Count == 0) || (strEmpId == ""))
            //{
            //    objVarMgr.DeletePayrollArrearData(ddlMonth.SelectedValue.ToString(), ddlFiscalYr.SelectedValue.Trim(), ddlArrearCase.SelectedValue.ToString());
            //    lblMsg.Text = "Payroll arrear data has been deleted successfully.";
            //    this.EntryMode();
            //}
            //else
            //{
            //    lblMsg.Text = "Payroll arrear data can not delete for " + ddlMonth.SelectedItem.Text.Trim() + " month of " + ddlArrearCase.SelectedItem.Text.Trim() + " case. Salary has already prepare for this " + strEmpId + " staffs.";
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }
}

