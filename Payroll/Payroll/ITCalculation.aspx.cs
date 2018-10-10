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
using System.Net;
using System.IO;

public partial class Payroll_Payroll_ITCalculation : System.Web.UI.Page
{
    Payroll_ITDepositRecords objITMgr = new Payroll_ITDepositRecords();
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    DataTable dtFisYr = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            dtFisYr = objPayrollMgr.SelectFiscalYear(0, "T");
            Common.FillDropDownList(dtFisYr, ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);
            Session["TAXFISCALSTARTDATE"] = dtFisYr.Rows[0]["STARTDATE"].ToString().Trim();   
            txtAssYear.Text = DateTime.Now.Year.ToString();           
        }
        ScriptManager _ScriptMan = ScriptManager.GetCurrent(this);
        _ScriptMan.AsyncPostBackTimeout = 1200;
        _ScriptMan.RegisterPostBackControl(this.btnExport);
    }

    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
        {
            // btnSave.Text = "Update";           
        }
        else
        {
            // btnSave.Text = "Save";
            txtEmpCode.Text = "";
            txtYBasicSalary.Text = "";
            txtYHouseRent.Text = "";
            //txtYMedicalAllowance.Text = "";
            txtYTransportAllowance.Text = "";
            //txtYFieldAllowance.Text = "";
            txtYFestivalBonus.Text = "";
            txtYOtherallowance.Text = "";
            txtT_HA.Text = "";
            txtT_TA.Text = "";
            txtTTI_1.Text = "";
            txtTTI_2.Text = "";
            txtYPFDeduction.Text = "";
            txtZ_M_F.Text = "";
            txtRebate.Text = "";
            txtP10.Text = "";
            txtP15.Text = "";
            txtP20.Text = "";
            txtP25.Text = "";
            txtG_Tax.Text = "";
            txtNetTax.Text = "";
            txtITDeposited.Text = "";
            txtDemand.Text = "";
            txtRefund.Text = "";
            txtGender.Text = "";
        }
    }

    protected void OpenRecord()
    {
        string strEmpID = "";
        if (txtEmpID.Text.Trim() != "")
            strEmpID = txtEmpID.Text.Trim();
        DataTable dtEmployee = objITMgr.GetEmployeeForITCalculation(strEmpID, ddlMonth.SelectedValue.Trim(), ddlFiscalYear.SelectedValue.Trim()  );
        grEmployee.DataSource = dtEmployee;
        grEmployee.DataBind();
        lblRecordCount.Text = grEmployee.Rows.Count.ToString();

        //Inv. Threshold Level
        DataTable dtRefSlot = objITMgr.GetDistinctRefundSlot();
        if (dtRefSlot.Rows.Count == 0)
        {
            lblMsg.Text = "Inv. Refund Policy Not Set Yet. Please Set it";
            return;
        }

        this.FillSalaryData(strEmpID, dtRefSlot);
    }   

    protected void FillSalaryData(string strEmpID, DataTable dtRefSlot)
    {
        PayrollReportManager objPayRptMgr = new PayrollReportManager();
        DataTable dtSalary = objITMgr.GetSalaryDataForITCalculation(ddlFiscalYear.SelectedValue.Trim(), strEmpID);
        // Bonus
        // DataTable dtEmpFA = objPayRptMgr.GetBonusAllowanceMonthly(strMonth, strFisYr);
        DataTable dtEmpFAYr = objPayRptMgr.GetBonusAllowanceYearly(ddlFiscalYear.SelectedValue.Trim());
        //  DataRow[] fEmpFARows;
        DataRow[] fEmpFAYrRows;

        DataTable dtITDeposit = objITMgr.GetITDepositedData(strEmpID, ddlFiscalYear.SelectedValue.Trim());
        //Comment Date:16-7-2013. Introduce Refund Faclity.  DataTable dtLastYrITCalData = objITMgr.GetITCalculationReportData("0","6", Convert.ToString(Convert.ToInt32(ddlFiscalYear.SelectedValue.Trim())-1));
        DataTable dtLastYrITCalData = objITMgr.GetITRefundData("", ddlFiscalYear.SelectedValue.Trim());

        // IT Policy Data
        DataTable dtITPolicy = objOptMgr.GetITPolicyData();

        DataRow[] foundLastYrRow;
        DataRow[] foundSalRows;
        DataRow[] foundDepoRows;

        // Policy Variable
        decimal dclYHAPlc = 0;
        decimal dclMHAPlc = 0;
        decimal dclYTAPlc = 0;
        decimal dclYMAPlc = 0;//Yearly Medical Exeption
        decimal dclSlot0Plc = 0;
        decimal dclSlot10Plc = 0;
        decimal dclSlot15Plc = 0;
        decimal dclSlot20Plc = 0;
        decimal dclSlot25Plc = 0;
        decimal dclMinTaxPlc = 0;
        decimal dclInvAllowPlc = 0;
        decimal dclInvRebatePlc = 0;
        // End of Policy Variable

        decimal dclYBasic = 0;
        decimal dclYHouse = 0;
        decimal dclYMedical = 0;
        decimal dclYConveyance = 0;
        decimal dclYField = 0;
        decimal dclYTransport = 0;
        decimal dclYFestival = 0;
        decimal dclYOther = 0;
        decimal dclYPF = 0;
        decimal dclMonthDur = 0;
        decimal dclMonthDurFromJoining = 0;
        decimal dclRebate = 0;
        decimal dclITDepo = 0;
        decimal dclDemand = 0;
        decimal dclRefund = 0;
        decimal dclTaxDiff = 0;
        decimal dclYChgAllow = 0;
        decimal dclYPerfAllow = 0;
        decimal dclYArrear = 0;
         decimal dclYOverTime = 0;
         decimal dclYLWP = 0;

        DateTime dtFisStartDate = new DateTime();
        DateTime dtJoinDate = new DateTime();

        int inJoinMonth = 0;
        decimal dclYHRebate = 0;
        int i=1;

        // Assign Fiscal Start Date
        if (string.IsNullOrEmpty(Session["TAXFISCALSTARTDATE"].ToString().Trim()) == false)
        {
            dtFisStartDate = Convert.ToDateTime(Common.SetDate(Session["TAXFISCALSTARTDATE"].ToString().Trim()));
            //Convert.ToDateTime(Common.SetDate(Session["FISCALSTARTDATE"].ToString().Trim()));
        }

        foreach (GridViewRow gRow in grEmployee.Rows)
        {
            dclYBasic = 0;
            dclYHouse = 0;
            dclYMedical = 0;
            dclYField = 0;
            dclYTransport = 0;
            dclYFestival = 0;
            dclYOther = 0;
            dclYOverTime = 0;
            dclYPF = 0;
            dclMonthDur = 0;
            dclRebate = 0;
            dclITDepo = 0;
            dclDemand = 0;
            dclRefund = 0;
            dclTaxDiff = 0;
            dclYChgAllow = 0;
            dclYPerfAllow = 0;
            dclYArrear = 0;

            // Policy Variable & Value Assign
            dclYHAPlc = 0;
            dclMHAPlc = 0;
            dclYTAPlc = 0;
            dclSlot0Plc = 0;
            dclSlot10Plc = 0;
            dclSlot15Plc = 0;
            dclSlot20Plc = 0;
            dclMinTaxPlc = 0;
            dclInvAllowPlc = 0;
            dclInvRebatePlc = 0;

            gRow.Cells[1].Text = i.ToString() ;
            foreach (DataRow dRow in dtITPolicy.Rows)
            {
                switch (dRow["POLICYID"].ToString().Trim())
                {
                    case "YHA":
                        if (gRow.Cells[6].Text.Trim() == "M")
                            dclYHAPlc = Common.RoundDecimal(dRow["MAMT"].ToString().Trim(), 0);
                        else if (gRow.Cells[6].Text.Trim() == "F")
                            dclYHAPlc = Common.RoundDecimal(dRow["FAMT"].ToString().Trim(), 0);
                        break;
                    case "MHA":
                        if (gRow.Cells[6].Text.Trim() == "M")
                            dclMHAPlc = Common.RoundDecimal(dRow["MAMT"].ToString().Trim(), 0);
                        else if (gRow.Cells[6].Text.Trim() == "F")
                            dclMHAPlc = Common.RoundDecimal(dRow["FAMT"].ToString().Trim(), 0);
                        break;
                    case "YTA":
                        if (gRow.Cells[6].Text.Trim() == "M")
                            dclYTAPlc = Common.RoundDecimal(dRow["MAMT"].ToString().Trim(), 0);
                        else if (gRow.Cells[6].Text.Trim() == "F")
                            dclYTAPlc = Common.RoundDecimal(dRow["FAMT"].ToString().Trim(), 0);
                        break;
                    case "YMA"://Early Medical Exemption
                        if (gRow.Cells[6].Text.Trim() == "M")
                            dclYMAPlc = Common.RoundDecimal(dRow["MAMT"].ToString().Trim(), 0);
                        else if (gRow.Cells[6].Text.Trim() == "F")
                            dclYMAPlc = Common.RoundDecimal(dRow["FAMT"].ToString().Trim(), 0);
                        break;
                    case "SL0":
                        if (gRow.Cells[6].Text.Trim() == "M")
                            dclSlot0Plc = Common.RoundDecimal(dRow["MAMT"].ToString().Trim(), 0);
                        else if (gRow.Cells[6].Text.Trim() == "F")
                            dclSlot0Plc = Common.RoundDecimal(dRow["FAMT"].ToString().Trim(), 0);
                        break;
                    case "SL10":
                        if (gRow.Cells[6].Text.Trim() == "M")
                            dclSlot10Plc = Common.RoundDecimal(dRow["MAMT"].ToString().Trim(), 0);
                        else if (gRow.Cells[6].Text.Trim() == "F")
                            dclSlot10Plc = Common.RoundDecimal(dRow["FAMT"].ToString().Trim(), 0);
                        break;
                    case "SL15":
                        if (gRow.Cells[6].Text.Trim() == "M")
                            dclSlot15Plc = Common.RoundDecimal(dRow["MAMT"].ToString().Trim(), 0);
                        else if (gRow.Cells[6].Text.Trim() == "F")
                            dclSlot15Plc = Common.RoundDecimal(dRow["FAMT"].ToString().Trim(), 0);
                        break;
                    case "SL20":
                        if (gRow.Cells[6].Text.Trim() == "M")
                            dclSlot20Plc = Common.RoundDecimal(dRow["MAMT"].ToString().Trim(), 0);
                        else if (gRow.Cells[6].Text.Trim() == "F")
                            dclSlot20Plc = Common.RoundDecimal(dRow["FAMT"].ToString().Trim(), 0);
                        break;
                    case "SL25":
                        if (gRow.Cells[6].Text.Trim() == "M")
                            dclSlot25Plc = Common.RoundDecimal(dRow["MAMT"].ToString().Trim(), 0);
                        else if (gRow.Cells[6].Text.Trim() == "F")
                            dclSlot25Plc = Common.RoundDecimal(dRow["FAMT"].ToString().Trim(), 0);
                        break;
                    case "MNT":
                        dclMinTaxPlc = Common.RoundDecimal(grEmployee.DataKeys[gRow.DataItemIndex].Values[7].ToString().Trim(), 0);
                            break;
                        ////if (gRow.Cells[6].Text.Trim() == "M")
                        ////    dclMinTaxPlc = Common.RoundDecimal(dRow["MAMT"].ToString().Trim(), 0);
                        ////else if (gRow.Cells[6].Text.Trim() == "F")
                        ////    dclMinTaxPlc = Common.RoundDecimal(dRow["FAMT"].ToString().Trim(), 0);
                        ////break;
                    case "INVA":
                        if (gRow.Cells[6].Text.Trim() == "M")
                            dclInvAllowPlc = Common.RoundDecimal(dRow["MAMT"].ToString().Trim(), 0);
                        else if (gRow.Cells[6].Text.Trim() == "F")
                            dclInvAllowPlc = Common.RoundDecimal(dRow["FAMT"].ToString().Trim(), 0);
                        break;
                    case "INVR":
                        if (gRow.Cells[6].Text.Trim() == "M")
                            dclInvRebatePlc = Common.RoundDecimal(dRow["MAMT"].ToString().Trim(), 0);
                        else if (gRow.Cells[6].Text.Trim() == "F")
                            dclInvRebatePlc = Common.RoundDecimal(dRow["FAMT"].ToString().Trim(), 0);
                        break;
                }
            }

            // End of Policy Variable

            dtJoinDate = Convert.ToDateTime(Common.SetDate(gRow.Cells[8].Text.Trim()));
            inJoinMonth = dtJoinDate.Month;

            foundSalRows = dtSalary.Select("EMPID='" + gRow.Cells[2].Text.Trim() + "'");
            foundDepoRows = dtITDeposit.Select("EMPID='" + gRow.Cells[2].Text.Trim() + "'");
            // Bonus
            fEmpFAYrRows = dtEmpFAYr.Select("EMPID ='" + gRow.Cells[2].Text.Trim() + "'");
            // Last Year IT 108 Data
            foundLastYrRow = dtLastYrITCalData.Select("EmpID ='" + gRow.Cells[2].Text.Trim() + "'");

            DataTable dtSalPak=new DataTable ();
            if (string.IsNullOrEmpty(grEmployee.DataKeys[gRow.DataItemIndex].Values[1].ToString().Trim()) == false)
                dtSalPak = objPayrollMgr.SelectSalaryPakDetls(Convert.ToInt32(grEmployee.DataKeys[gRow.DataItemIndex].Values[1].ToString().Trim()));
            //else
            //{
            //    lblMsg.Text = lblMsg.Text + ","gRow.Cells[2].Text.Trim() + " Staffs Salary Package has not set";
            //    return;
            //}

            foreach (DataRow dRow in foundSalRows)
            {
                switch (dRow["SHEADID"].ToString().Trim())
                {
                    case "1"://Basic
                        dclYBasic = dclYBasic + Common.RoundDecimal(dRow["PayAmt"].ToString().Trim(), 0);

                        //dclYHouse = dclYHouse + ((Common.RoundDecimal(dRow["PayAmt"].ToString().Trim(), 0) * 50) / 100);

                        //dclYMedical = dclYMedical + ((Common.RoundDecimal(dRow["PayAmt"].ToString().Trim(), 0) * 10) / 100);

                        //dclYConveyance = dclYConveyance + ((Common.RoundDecimal(dRow["PayAmt"].ToString().Trim(), 0) * 5) / 100);
                        break;

                    case "2"://House Rent
                        dclYHouse = dclYHouse + Common.RoundDecimal(dRow["PayAmt"].ToString().Trim(), 0);
                        break;

                    case "3"://Medical
                        dclYMedical = dclYMedical + Common.RoundDecimal(dRow["PayAmt"].ToString().Trim(), 0);
                        break;

                    case "14"://Festival Allow                        
                        Decimal dclYFA = 0;
                        dclYFA = Common.RoundDecimal(dRow["PayAmt"].ToString().Trim(), 0);
                        if (dclYFA == 0)
                        {
                            if (fEmpFAYrRows.Length > 0)
                            {
                                dclYFA = Common.RoundDecimal(fEmpFAYrRows[0]["PAYAMT"].ToString(), 0);
                            }
                            else
                            {
                                dclYFA = 0;
                            }
                        }
                        else
                        {
                            if (fEmpFAYrRows.Length > 0)
                            {
                                if (dclYFA != Common.RoundDecimal(fEmpFAYrRows[0]["PAYAMT"].ToString(), 0))
                                {
                                    dclYFA = Common.RoundDecimal(fEmpFAYrRows[0]["PAYAMT"].ToString(), 0);
                                }
                            }
                        }
                        dclYFestival = dclYFA;
                        //nRow["YFestivalBonus"] = dclYFA.ToString();
                        break;

                    //case "8":
                    //    dclYOverTime = 0;//dclYOverTime + Common.RoundDecimal(dRow["PayAmt"].ToString().Trim(), 0);
                    //    break;

                    ////case "13":
                    ////    dclYOther = dclYOther + Common.RoundDecimal(dRow["PayAmt"].ToString().Trim(), 0);
                    ////    break;

                    case "8"://PF
                    case "11":
                        dclYPF = dclYPF+Common.RoundDecimal(dRow["PayAmt"].ToString().Trim(), 0);

                        dclYPF = Math.Abs(dclYPF);
                        break;

                    //case "6":// Festival Bonus
                    //    dclYPF = dclYPF + Common.RoundDecimal(dRow["PayAmt"].ToString().Trim(), 0);
                    //    dclYPF = Math.Abs(dclYPF);
                    //    break;

                    //case "11":// Charge Allow
                    //    dclYChgAllow = dclYOther + Common.RoundDecimal(dRow["PayAmt"].ToString().Trim(), 0);
                    //    break;
                    case "9":// LWOP
                        dclYLWP = dclYLWP + Common.RoundDecimal(dRow["PayAmt"].ToString().Trim(), 0);
                        break;

                    //case "12": //Performance Allow
                    //    dclYPerfAllow = dclYOther + Common.RoundDecimal(dRow["PayAmt"].ToString().Trim(), 0);
                    //    break;

                    //case "17":// Arrear 
                    //    dclYArrear = dclYOther + Common.RoundDecimal(dRow["PayAmt"].ToString().Trim(), 0);
                    //    break;
                }
            }

            //Forcasting Salary from Salary Package
            if ((Convert.ToInt32(ddlMonth.SelectedValue.Trim()) >= 7) && (Convert.ToInt32(ddlMonth.SelectedValue.Trim()) <= 12))
            {
                if (dtSalary.Rows.Count > 0)
                    dclMonthDur = 6 - (Convert.ToDecimal(ddlMonth.SelectedValue.Trim())) + 13;//12
                else
                    dclMonthDur = 6 - (Convert.ToDecimal(ddlMonth.SelectedValue.Trim())) + 13;
            }
            else
            {
                dclMonthDur = 6 - (Convert.ToDecimal(ddlMonth.SelectedValue.Trim()));
            }

            //Calculate Joining to Fiscal Year end duration
            if (dtJoinDate >= dtFisStartDate)
            {
                if ((inJoinMonth >= 7) && (inJoinMonth <= 12))
                {
                    dclMonthDurFromJoining = 12 - inJoinMonth + 7;
                }
                else
                {
                    dclMonthDurFromJoining = 6 - inJoinMonth + 1;
                }
            }
            else
            {
                dclMonthDurFromJoining = 12;
            }

            if (dtSalPak.Rows.Count > 0)
            {
                dclYBasic = dclYBasic + this.GetSalHeadAmt("1", grEmployee.DataKeys[gRow.DataItemIndex].Values[1].ToString().Trim(), dtSalPak) * dclMonthDur;
                dclYHouse = dclYHouse + this.GetSalHeadAmt("2", grEmployee.DataKeys[gRow.DataItemIndex].Values[1].ToString().Trim(), dtSalPak) * dclMonthDur;//(dclYBasic* 50) / 100;
                dclYMedical = dclYMedical + this.GetSalHeadAmt("3", grEmployee.DataKeys[gRow.DataItemIndex].Values[1].ToString().Trim(), dtSalPak) * dclMonthDur; //(dclYBasic * 10) / 100;
                dclYTransport = 0;// (dclYBasic * 5) / 100;
                dclYOverTime = 0; //dclYOverTime + this.GetSalHeadAmt("8", grEmployee.DataKeys[gRow.DataItemIndex].Values[1].ToString().Trim(), dtSalPak) * dclMonthDur;
                dclYPF = dclYPF + this.GetSalHeadAmt("8", grEmployee.DataKeys[gRow.DataItemIndex].Values[1].ToString().Trim(), dtSalPak) * dclMonthDur;
                // Festival Not required in ForeCasting;
                // Edited Due to Bonus Disbursed Outside Payroll in July, 2015 while There is no payroll data available.
                // Edited Date: 12-07-215
                // Edited By: Amit & Sulata
                if (fEmpFAYrRows.Length > 0)
                {
                    dclYFestival = Common.RoundDecimal(fEmpFAYrRows[0]["PAYAMT"].ToString(), 0);
                }
            }

            //If Arrear Exist
            dclYArrear = SalaryPayslipDetTaxFiscalYrWs(gRow.Cells[2].Text.Trim());
            if (dclYArrear > 0)
            {
                dclYArrear = Math.Round((dclYArrear / Convert.ToDecimal(1.65)), 2);
                dclYBasic = Math.Round(dclYBasic + dclYArrear, 0);

                dclYHouse = Math.Round(dclYHouse + ((dclYArrear * 50) / 100), 0);

                dclYMedical = Math.Round(dclYMedical + ((dclYArrear * 10) / 100), 0);

                dclYTransport = 0;// Math.Round(dclYTransport + ((dclYArrear * 5) / 100), 0);

                dclYPF = Math.Round(dclYPF + ((dclYArrear * 10) / 100), 0);                
            }

            // Fill value
            gRow.Cells[9].Text = dclYBasic.ToString();
            gRow.Cells[10].Text = dclYHouse.ToString();
            gRow.Cells[12].Text = dclYTransport.ToString();
            gRow.Cells[14].Text = dclYFestival.ToString();
            gRow.Cells[15].Text = dclYOverTime.ToString() ;//Convert.ToString(dclYOther + dclYChgAllow + dclYPerfAllow + dclYArrear);
            gRow.Cells[18].Text = dclYPF.ToString();
            gRow.Cells[33].Text = dclYMedical.ToString();

            // T_HA (dclYHAPlc=180000)
            if (dclMonthDurFromJoining == 12)
            {
                if (dclYHouse > dclYHAPlc)
                    gRow.Cells[11].Text = Convert.ToString(dclYHouse - dclYHAPlc);
                else
                    gRow.Cells[11].Text = "0";
            }
            else
            {
                // If Join Date is From August Onward then Yearly Rebate = No of Month From Join * 15000
                // dclMHAPlc=15000
                dclYHRebate = dclMonthDurFromJoining * dclMHAPlc;
                if (dclYHouse > dclYHRebate)
                    gRow.Cells[11].Text = Convert.ToString(dclYHouse - dclYHRebate);
                else
                    gRow.Cells[11].Text = "0";
            }

            // T_TA (dclYTAPlc=24000)
            if (dclYTransport > dclYTAPlc)
                gRow.Cells[13].Text = Convert.ToString(dclYTransport - dclYTAPlc);
            else
                gRow.Cells[13].Text = "0";

            // T_MA (dclYMAPlc=120000) Medical Tax Exemption
            if (dclMonthDurFromJoining == 12)
            {
                if (dclYMedical > dclYMAPlc)
                    gRow.Cells[34].Text = Convert.ToString(dclYMedical - dclYMAPlc);
                else
                    gRow.Cells[34].Text = "0";
            }
            // TTI_1
            gRow.Cells[16].Text = Convert.ToString(Math.Round(dclYBasic + Common.RoundDecimal(gRow.Cells[11].Text, 0) + Common.RoundDecimal(gRow.Cells[13].Text, 0) + 
                Common.RoundDecimal(gRow.Cells[34].Text,0)  +dclYFestival + dclYField + dclYOther, 0));

            // Rebate dclInvAllowPlc=30%; dclInvRebatePlc=15%
            ////dclRebate = Common.RoundDecimal(gRow.Cells[16].Text, 0) * dclInvAllowPlc / 100;
            ////dclRebate = (Common.RoundDecimal(gRow.Cells[16].Text, 0) + dclYPF) * dclInvAllowPlc / 100;
            ////dclRebate = dclRebate * dclInvRebatePlc / 100;
            ////gRow.Cells[17].Text = Common.RoundDecimal(dclRebate.ToString(), 0).ToString();

            //dclRebate = Common.RoundDecimal(gRow.Cells[16].Text, 0) * dclInvAllowPlc / 100;
            //if (ddlMonth.SelectedValue =="7" )
            //    dclRebate = Common.RoundDecimal(gRow.Cells[16].Text, 0) * dclInvAllowPlc / 100;
            //else
            dclRebate = (Common.RoundDecimal(gRow.Cells[16].Text, 0) + dclYPF) * dclInvAllowPlc / 100;



            //Inv Slot 16.10.16
            decimal dclSlot = 0;
            dclInvRebatePlc = 0;
            decimal dclTotalRebate = 0;
            for (int r = 0; r < dtRefSlot.Rows.Count; r++)
            {
                //Commit at 2018.10.04
                if (Convert.ToDecimal(gRow.Cells[16].Text)+ dclYPF <= Convert.ToDecimal(dtRefSlot.Rows[r]["Slot"]))
                ////if (Convert.ToDecimal(gRow.Cells[16].Text) <= Convert.ToDecimal(dtRefSlot.Rows[r]["Slot"]))
                {
                    dclSlot = Convert.ToDecimal(dtRefSlot.Rows[r]["Slot"]);
                    break;
                }
                if (r == dtRefSlot.Rows.Count - 1)
                    dclSlot = Convert.ToDecimal(dtRefSlot.Rows[r]["Slot"]);
            }
            DataTable dtRefPlcData = objITMgr.GetSlotWiseRefundPlcData(dclSlot);
            if (dtRefPlcData.Rows.Count > 0)
            {
                decimal dclRefRem = dclRebate;
                decimal dclTmpRebate = 0;

                for (int x = 0; x < dtRefPlcData.Rows.Count; x++)
                {
                    if (dclRefRem >= 0)
                    {
                        if (dclRefRem <= Convert.ToDecimal(dtRefPlcData.Rows[x]["TopBand"]))
                        {
                            dclTmpRebate = dclRefRem * Convert.ToDecimal(dtRefPlcData.Rows[x]["Percentage"]) / 100;
                            dclTotalRebate = dclTotalRebate + dclTmpRebate;
                        }
                        else
                        {
                            if (Convert.ToDecimal(dtRefPlcData.Rows[x]["TopBand"]) == 0)//Unlimited Slot Amount
                            {
                                dclTmpRebate = dclRefRem * Convert.ToDecimal(dtRefPlcData.Rows[x]["Percentage"]) / 100;
                                dclTotalRebate = dclTotalRebate + dclTmpRebate;
                            }
                            else
                            {
                                dclTmpRebate = Convert.ToDecimal(dtRefPlcData.Rows[x]["TopBand"]) * Convert.ToDecimal(dtRefPlcData.Rows[x]["Percentage"]) / 100;
                                dclTotalRebate = dclTotalRebate + dclTmpRebate;
                            }
                            dclRefRem = dclRefRem - Convert.ToDecimal(dtRefPlcData.Rows[x]["TopBand"]);
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                dclRefRem = 0;
            }

            dclRebate = dclTotalRebate;

            //dclRebate = dclRebate * dclInvRebatePlc / 100;
            gRow.Cells[17].Text = Common.RoundDecimal(dclRebate.ToString(), 0).ToString();

            // TTI_2
            gRow.Cells[19].Text = Convert.ToString(Common.RoundDecimal(gRow.Cells[16].Text, 0) + dclYPF);

            ////// Rebate dclInvAllowPlc=30%; dclInvRebatePlc=15%
            ////dclRebate = (Common.RoundDecimal(gRow.Cells[19].Text, 0) - dclYPF) * dclInvAllowPlc / 100;
            ////dclRebate = dclRebate * dclInvRebatePlc / 100;
            ////gRow.Cells[17].Text = Common.RoundDecimal(dclRebate.ToString(), 0).ToString();

            // Z_M_F (dclSlot0Plc[M]=200000// dclSlot0Plc[F]=225000)
            if (gRow.Cells[6].Text.Trim() == "M")
            {
                if (Common.RoundDecimal(gRow.Cells[19].Text, 0) > dclSlot0Plc)
                    gRow.Cells[20].Text = Convert.ToString(Common.RoundDecimal(gRow.Cells[19].Text, 0) - dclSlot0Plc);
                else
                    gRow.Cells[20].Text = "0";
            }
            else if (gRow.Cells[6].Text.Trim() == "F")
            {
                if (Common.RoundDecimal(gRow.Cells[19].Text, 0) > dclSlot0Plc)
                    gRow.Cells[20].Text = Convert.ToString(Common.RoundDecimal(gRow.Cells[19].Text, 0) - dclSlot0Plc);
                else
                    gRow.Cells[20].Text = "0";
            }

            // Income tax Assessment and IT Deposoted Data, Demand, Refund
            decimal[] dclTax = this.GetITAssessAmount(Common.RoundDecimal(gRow.Cells[20].Text, 0),
                                    Common.RoundDecimal(gRow.Cells[17].Text, 0),
                                    dclSlot10Plc,
                                    dclSlot15Plc,
                                    dclSlot20Plc,
                                    dclSlot25Plc,
                                    dclMinTaxPlc);

            gRow.Cells[21].Text = Common.RoundDecimal(dclTax[0].ToString(), 0).ToString();
            gRow.Cells[22].Text = Common.RoundDecimal(dclTax[1].ToString(), 0).ToString();
            gRow.Cells[23].Text = Common.RoundDecimal(dclTax[2].ToString(), 0).ToString();
            gRow.Cells[24].Text = Common.RoundDecimal(dclTax[3].ToString(), 0).ToString();
            gRow.Cells[25].Text = Common.RoundDecimal(dclTax[4].ToString(), 0).ToString();

            //G-Tax
            gRow.Cells[26].Text = Common.RoundDecimal(dclTax[5].ToString(), 0).ToString();

            //Net Tax
            gRow.Cells[27].Text = Common.RoundDecimal(dclTax[6].ToString(), 0).ToString();

            // Last Year Refund and New Year Monthly Tax
            decimal dclMonthlyTax = 0;
            decimal dclActTax = 0;
            int inMon = Convert.ToInt32(ddlMonth.SelectedValue.Trim());
            inMon = Common.GetMonthDiffTillJuly(ddlMonth.SelectedValue.Trim()); //Common.GetMonthDiffTillJune(ddlMonth.SelectedValue.Trim());

            if (foundLastYrRow.Length > 0)
                gRow.Cells[28].Text = Common.ReturnZeroForNull(foundLastYrRow[0]["REFUNDAMT"].ToString().Trim());
            else
                gRow.Cells[28].Text = "0";

            dclActTax = Common.RoundDecimal(gRow.Cells[27].Text, 0) - Common.RoundDecimal(gRow.Cells[28].Text, 0);

            // IT Deposoted Data, Demand, Refund
            if (foundDepoRows.Length > 0)
            {
                dclITDepo = Common.RoundDecimal(foundDepoRows[0]["PAYAMT"].ToString().Trim(), 0);
                //dclTaxDiff = Common.RoundDecimal(gRow.Cells[28].Text, 0) - dclITDepo;
                dclTaxDiff = dclActTax - dclITDepo;
                if (dclTaxDiff > 0)
                    dclDemand = dclTaxDiff;
                else if (dclTaxDiff < 0)
                    dclRefund = dclTaxDiff;

                gRow.Cells[30].Text = dclITDepo.ToString();
                gRow.Cells[31].Text = dclDemand.ToString();
                gRow.Cells[32].Text = dclRefund.ToString();
            }

            if (ddlMonth.SelectedValue.Trim() != "6")
            {
                if (ddlMonth.SelectedValue.Trim() == "7")
                    inMon = inMon;
                ////inMon = inMon + 1;
                ////inMon = inMon; //Open below link from August Month                   
                  
                dclMonthlyTax = (dclActTax - dclITDepo) / inMon;
                dclMonthlyTax = Math.Round(dclMonthlyTax, 0);
                if (dclMonthlyTax > 0)
                    gRow.Cells[29].Text = dclMonthlyTax.ToString();
                else
                    gRow.Cells[29].Text = "0";
            }
            else
            {
                dclActTax = Common.RoundDecimal(gRow.Cells[26].Text, 0);
                gRow.Cells[29].Text = "0";
            }

            // Clear
            foundSalRows = null;
            foundDepoRows = null;

            // Date Format
            if (Common.CheckNullString(gRow.Cells[7].Text) != "")
                gRow.Cells[7].Text = Common.DisplayDate(gRow.Cells[7].Text.Trim());

            if (Common.CheckNullString(gRow.Cells[8].Text) != "")
                gRow.Cells[8].Text = Common.DisplayDate(gRow.Cells[8].Text.Trim());

            i++;
        }
    }

    protected Decimal GetSalHeadAmt(string strSheadID, string strSalPakID, DataTable dt)
    {
        DataRow[] fRows = dt.Select("SALPAKID=" + strSalPakID + " AND SHEADID=" + strSheadID);
        if (fRows.Length > 0)
            return Math.Abs(Common.RoundDecimal(fRows[0]["PAYAMT"].ToString(), 0));
        else
            return 0;
    }

    protected Decimal[] GetITAssessAmount(decimal dclIncome, decimal dclRebate, decimal dclSlot10Plc, decimal dclSlot15Plc, decimal dclSlot20Plc, decimal dclSlot25Plc, decimal dclMinTaxPlc)
    {
        decimal decRemAmount = 0;
        bool goNextSlot = true;
        decimal[] decTax = new decimal[7];
        decTax[0] = 0;
        decTax[1] = 0;
        decTax[2] = 0;
        decTax[3] = 0;
        decTax[4] = 0;
        decTax[5] = 0;
        decTax[6] = 0;

        if (dclIncome == 0)
            return decTax;

        //Note dclSlot10Plc=300000
        decRemAmount = dclIncome - dclSlot10Plc;
        // 10% slot
        if (dclIncome > dclSlot10Plc)
            decTax[0] = dclSlot10Plc * 10 / 100;
        else
            decTax[0] = dclIncome * 10 / 100;
        // 15% slot
        if (decRemAmount > 0)
        {
            //Note dclSlot15Plc=400000
            if (decRemAmount > dclSlot15Plc)
                decTax[1] = dclSlot15Plc * 15 / 100;
            else
            {
                decTax[1] = decRemAmount * 15 / 100;
                goNextSlot = false;
            }
            if (decRemAmount - dclSlot15Plc > 0)
                decRemAmount = decRemAmount - dclSlot15Plc;

        }
        // 20% slot
        if (goNextSlot == true)
        {
            if (decRemAmount > 0)
            {
                //Note dclSlot20Plc=300000
                if (decRemAmount > dclSlot20Plc)
                    decTax[2] = dclSlot20Plc * 20 / 100;
                else
                {
                    decTax[2] = decRemAmount * 20 / 100;
                    goNextSlot = false;
                }
                if (decRemAmount - dclSlot20Plc > 0)
                    decRemAmount = decRemAmount - dclSlot20Plc;
            }
        }
        // 25% slot
        if (goNextSlot == true)
        {
            if (decRemAmount > 0)
            {
                //Note dclSlot25Plc=300000
                if (decRemAmount > dclSlot25Plc)
                    decTax[3] = dclSlot25Plc * 25 / 100;
                else
                {
                    decTax[3] = decRemAmount * 25 / 100;
                    goNextSlot = false;
                }
                if (decRemAmount - dclSlot25Plc > 0)
                    decRemAmount = decRemAmount - dclSlot25Plc;
            }
        }
        // 30% slot
        if (goNextSlot == true)
        {
            if (decRemAmount > 0)
            {
                decTax[4] = decRemAmount * 30 / 100;
            }
        }
        decTax[5] = decTax[0] + decTax[1] + decTax[2] + decTax[3] + decTax[4];
        decimal decGtax = decTax[5];
        if (decTax[5] > dclRebate)
        {
            decTax[6] = decTax[5] - dclRebate;
            if (decTax[6] < dclMinTaxPlc)
            {
                decTax[6] = dclMinTaxPlc;
            }
        }
        else
        {
            decTax[6] = dclMinTaxPlc;
        }
        
        return decTax;
    }

    protected void grEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                txtEmpCode.Text = grEmployee.SelectedRow.Cells[2].Text.Trim();
                txtYBasicSalary.Text = grEmployee.SelectedRow.Cells[9].Text.Trim();
                txtYHouseRent.Text = grEmployee.SelectedRow.Cells[10].Text.Trim();
                txtT_HA.Text = grEmployee.SelectedRow.Cells[11].Text.Trim(); ;
                //txtYMedicalAllowance.Text = grEmployee.SelectedRow.Cells[12].Text.Trim();
                txtYTransportAllowance.Text = grEmployee.SelectedRow.Cells[12].Text.Trim();
                txtT_TA.Text = grEmployee.SelectedRow.Cells[13].Text.Trim();
                //txtYFieldAllowance.Text = grEmployee.SelectedRow.Cells[15].Text.Trim();
                txtYFestivalBonus.Text = grEmployee.SelectedRow.Cells[14].Text.Trim();
                txtYOtherallowance.Text = grEmployee.SelectedRow.Cells[15].Text.Trim();


                txtTTI_1.Text = grEmployee.SelectedRow.Cells[16].Text.Trim();
                txtRebate.Text = grEmployee.SelectedRow.Cells[17].Text.Trim();
                txtYPFDeduction.Text = grEmployee.SelectedRow.Cells[18].Text.Trim();
                txtTTI_2.Text = grEmployee.SelectedRow.Cells[19].Text.Trim();

                txtZ_M_F.Text = grEmployee.SelectedRow.Cells[20].Text.Trim();

                txtP10.Text = grEmployee.SelectedRow.Cells[21].Text.Trim();
                txtP15.Text = grEmployee.SelectedRow.Cells[22].Text.Trim();
                txtP20.Text = grEmployee.SelectedRow.Cells[23].Text.Trim();
                txtP25.Text = grEmployee.SelectedRow.Cells[24].Text.Trim();
                txtP30.Text = grEmployee.SelectedRow.Cells[27].Text.Trim();
                txtG_Tax.Text = grEmployee.SelectedRow.Cells[28].Text.Trim();
                txtNetTax.Text = grEmployee.SelectedRow.Cells[29].Text.Trim();

                txtITDeposited.Text = grEmployee.SelectedRow.Cells[30].Text.Trim();
                txtDemand.Text = grEmployee.SelectedRow.Cells[31].Text.Trim();
                txtRefund.Text = grEmployee.SelectedRow.Cells[32].Text.Trim();

                txtGender.Text = grEmployee.SelectedRow.Cells[6].Text.Trim();
                //this.EntryMode(true);
                break;
        }
    }

    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        this.OpenRecord();
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        grEmployee.SelectedRow.Cells[9].Text = txtYBasicSalary.Text;
        grEmployee.SelectedRow.Cells[10].Text = txtYHouseRent.Text;
        grEmployee.SelectedRow.Cells[11].Text = txtT_HA.Text;

        grEmployee.SelectedRow.Cells[12].Text = txtYTransportAllowance.Text;
        grEmployee.SelectedRow.Cells[13].Text = txtT_TA.Text;
        grEmployee.SelectedRow.Cells[14].Text = txtYFestivalBonus.Text;
        grEmployee.SelectedRow.Cells[15].Text = txtYOtherallowance.Text;

        grEmployee.SelectedRow.Cells[16].Text = txtTTI_1.Text;
        grEmployee.SelectedRow.Cells[17].Text = txtRebate.Text;
        grEmployee.SelectedRow.Cells[18].Text = txtYPFDeduction.Text;
        grEmployee.SelectedRow.Cells[19].Text = txtTTI_2.Text;

        grEmployee.SelectedRow.Cells[20].Text = txtZ_M_F.Text;

        grEmployee.SelectedRow.Cells[21].Text = txtP10.Text;
        grEmployee.SelectedRow.Cells[22].Text = txtP15.Text;
        grEmployee.SelectedRow.Cells[23].Text = txtP20.Text;

        grEmployee.SelectedRow.Cells[24].Text = txtP25.Text;
        grEmployee.SelectedRow.Cells[25].Text = txtG_Tax.Text;
        grEmployee.SelectedRow.Cells[26].Text = txtNetTax.Text;

        grEmployee.SelectedRow.Cells[29].Text = txtITDeposited.Text;
        grEmployee.SelectedRow.Cells[30].Text = txtDemand.Text;
        grEmployee.SelectedRow.Cells[31].Text = txtRefund.Text;
        this.EntryMode(false);
    }

    protected void DataChanged()
    {
        // Policy Variable & Value Assign
        DataTable dtITPolicy = objOptMgr.GetITPolicyData();

        decimal dclYHAPlc = 0;
        decimal dclMHAPlc = 0;
        decimal dclYTAPlc = 0;
        decimal dclYMAPlc = 0;
        decimal dclSlot0Plc = 0;
        decimal dclSlot10Plc = 0;
        decimal dclSlot15Plc = 0;
        decimal dclSlot20Plc = 0;
        decimal dclSlot25Plc = 0;
        decimal dclMinTaxPlc = 0;
        decimal dclInvAllowPlc = 0;
        decimal dclInvRebatePlc = 0;
        foreach (DataRow dRow in dtITPolicy.Rows)
        {
            switch (dRow["POLICYID"].ToString().Trim())
            {
                case "YHA":
                    if (txtGender.Text == "M")
                        dclYHAPlc = Common.RoundDecimal(dRow["MAMT"].ToString().Trim(), 0);
                    else if (txtGender.Text == "F")
                        dclYHAPlc = Common.RoundDecimal(dRow["FAMT"].ToString().Trim(), 0);
                    break;
                case "MHA":
                    if (txtGender.Text == "M")
                        dclMHAPlc = Common.RoundDecimal(dRow["MAMT"].ToString().Trim(), 0);
                    else if (txtGender.Text == "F")
                        dclMHAPlc = Common.RoundDecimal(dRow["FAMT"].ToString().Trim(), 0);
                    break;

                case "YTA":
                    if (txtGender.Text == "M")
                        dclYTAPlc = Common.RoundDecimal(dRow["MAMT"].ToString().Trim(), 0);
                    else if (txtGender.Text == "F")
                        dclYTAPlc = Common.RoundDecimal(dRow["FAMT"].ToString().Trim(), 0);
                    break;
                case "YMA"://Yearly Medical Exemption
                    if (txtGender.Text == "M")
                        dclYMAPlc = Common.RoundDecimal(dRow["MAMT"].ToString().Trim(), 0);
                    else if (txtGender.Text == "F")
                        dclYMAPlc = Common.RoundDecimal(dRow["FAMT"].ToString().Trim(), 0);
                    break;
                case "SL0":
                    if (txtGender.Text == "M")
                        dclSlot0Plc = Common.RoundDecimal(dRow["MAMT"].ToString().Trim(), 0);
                    else if (txtGender.Text == "F")
                        dclSlot0Plc = Common.RoundDecimal(dRow["FAMT"].ToString().Trim(), 0);
                    break;
                case "SL10":
                    if (txtGender.Text == "M")
                        dclSlot10Plc = Common.RoundDecimal(dRow["MAMT"].ToString().Trim(), 0);
                    else if (txtGender.Text == "F")
                        dclSlot10Plc = Common.RoundDecimal(dRow["FAMT"].ToString().Trim(), 0);
                    break;
                case "SL15":
                    if (txtGender.Text == "M")
                        dclSlot15Plc = Common.RoundDecimal(dRow["MAMT"].ToString().Trim(), 0);
                    else if (txtGender.Text == "F")
                        dclSlot15Plc = Common.RoundDecimal(dRow["FAMT"].ToString().Trim(), 0);
                    break;
                case "SL20":
                    if (txtGender.Text == "M")
                        dclSlot20Plc = Common.RoundDecimal(dRow["MAMT"].ToString().Trim(), 0);
                    else if (txtGender.Text == "F")
                        dclSlot20Plc = Common.RoundDecimal(dRow["FAMT"].ToString().Trim(), 0);
                    break;
                case "MNT":
                    if (txtGender.Text == "M")
                        dclMinTaxPlc = Common.RoundDecimal(dRow["MAMT"].ToString().Trim(), 0);
                    else if (txtGender.Text == "F")
                        dclMinTaxPlc = Common.RoundDecimal(dRow["FAMT"].ToString().Trim(), 0);
                    break;
                case "INVA":
                    if (txtGender.Text == "M")
                        dclInvAllowPlc = Common.RoundDecimal(dRow["MAMT"].ToString().Trim(), 0);
                    else if (txtGender.Text == "F")
                        dclInvAllowPlc = Common.RoundDecimal(dRow["FAMT"].ToString().Trim(), 0);
                    break;
                case "INVR":
                    if (txtGender.Text == "M")
                        dclInvRebatePlc = Common.RoundDecimal(dRow["MAMT"].ToString().Trim(), 0);
                    else if (txtGender.Text == "F")
                        dclInvRebatePlc = Common.RoundDecimal(dRow["FAMT"].ToString().Trim(), 0);
                    break;
            }
        }

        decimal dclYHouse = Common.RoundDecimal(txtYHouseRent.Text, 0);
        decimal dclYTransport = Common.RoundDecimal(txtYTransportAllowance.Text, 0);
        // T_HA
        if (Common.RoundDecimal(txtYHouseRent.Text, 0) > dclYHAPlc)
            txtT_HA.Text = Convert.ToString(dclYHouse - dclYHAPlc);
        else
            txtT_HA.Text = "0";

        // T_TA
        if (Common.RoundDecimal(txtYTransportAllowance.Text, 0) > dclYTAPlc)
            txtT_TA.Text = Convert.ToString(dclYTransport - dclYTAPlc);
        else
            txtT_TA.Text = "0";
        // TTI_1
        txtTTI_1.Text = Convert.ToString(Common.RoundDecimal(txtYBasicSalary.Text, 0) +
                                            Common.RoundDecimal(txtT_HA.Text, 0) +
                                            Common.RoundDecimal(txtT_TA.Text, 0) +
                                            Common.RoundDecimal(txtYFestivalBonus.Text, 0) +                                           
                                            Common.RoundDecimal(txtYOtherallowance.Text, 0));
        // Rebate
        decimal dclRebate = Common.RoundDecimal(txtTTI_1.Text, 0) * dclInvAllowPlc / 100;
        dclRebate = dclRebate * dclInvRebatePlc / 100;
        txtRebate.Text = Common.RoundDecimal(dclRebate.ToString(), 0).ToString();

        // TTI_2
        txtTTI_2.Text = Convert.ToString(Common.RoundDecimal(txtTTI_1.Text, 0));// + Common.RoundDecimal(txtYPFDeduction.Text, 0));
        // Z_M_F
        if (txtGender.Text == "M")
        {
            if (Common.RoundDecimal(txtTTI_2.Text, 0) > dclSlot0Plc)
                txtZ_M_F.Text = Convert.ToString(Common.RoundDecimal(txtTTI_2.Text, 0) - dclSlot0Plc);
            else
                txtZ_M_F.Text = "0";
        }
        else if (txtGender.Text == "F")
        {
            if (Common.RoundDecimal(txtTTI_2.Text, 0) > dclSlot0Plc)
                txtZ_M_F.Text = Convert.ToString(Common.RoundDecimal(txtTTI_2.Text, 0) - dclSlot0Plc);
            else
                txtZ_M_F.Text = "0";
        }
        // Income tax Assessment and IT Deposoted Data, Demand, Refund
        decimal[] dclTax = this.GetITAssessAmount(Common.RoundDecimal(txtZ_M_F.Text, 0),
                                Common.RoundDecimal(txtRebate.Text, 0),
                                dclSlot10Plc,
                                dclSlot15Plc,
                                dclSlot20Plc,
                                dclSlot25Plc,
                                dclMinTaxPlc);

        txtP10.Text = Common.RoundDecimal(dclTax[0].ToString(), 0).ToString();
        txtP15.Text = Common.RoundDecimal(dclTax[1].ToString(), 0).ToString();
        txtP20.Text = Common.RoundDecimal(dclTax[2].ToString(), 0).ToString();
        txtP25.Text = Common.RoundDecimal(dclTax[3].ToString(), 0).ToString();
        txtP30.Text = Common.RoundDecimal(dclTax[4].ToString(), 0).ToString();
        txtG_Tax.Text = Common.RoundDecimal(dclTax[5].ToString(), 0).ToString();
        txtNetTax.Text = Common.RoundDecimal(dclTax[6].ToString(), 0).ToString();
        // IT Deposoted Data, Demand, Refund

        decimal dclITDepo = Common.RoundDecimal(txtITDeposited.Text, 0);
        decimal dclTaxDiff = Common.RoundDecimal(txtNetTax.Text, 0) - dclITDepo;
        if (dclTaxDiff > 0)
            txtDemand.Text = dclTaxDiff.ToString();
        else if (dclTaxDiff < 0)
            txtRefund.Text = dclTaxDiff.ToString();
    }

    protected void txtYHouseRent_TextChanged(object sender, EventArgs e)
    {
        this.DataChanged();
    }

    protected void txtYBasicSalary_TextChanged(object sender, EventArgs e)
    {
        this.DataChanged();
    }

    protected void txtYMedicalAllowance_TextChanged(object sender, EventArgs e)
    {
        this.DataChanged();
    }

    protected void txtYTransportAllowance_TextChanged(object sender, EventArgs e)
    {
        this.DataChanged();
    }

    protected void txtYFieldAllowance_TextChanged(object sender, EventArgs e)
    {
        this.DataChanged();
    }

    protected void txtYFestivalBonus_TextChanged(object sender, EventArgs e)
    {
        this.DataChanged();
    }

    protected void txtYOtherallowance_TextChanged(object sender, EventArgs e)
    {
        this.DataChanged();
    }

    protected void txtYPFDeduction_TextChanged(object sender, EventArgs e)
    {
        this.DataChanged();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (grEmployee.Rows.Count > 0)
        {
            objITMgr.InsertITCalculationData(grEmployee, ddlMonth.SelectedValue.Trim(), ddlFiscalYear.SelectedValue.Trim(),
                Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), txtAssYear.Text.Trim());
            lblMsg.Text = "Record Saved Successfully";
        }
        else
        {
            lblMsg.Text = "";
        }
    }

    protected void txtNetTax_TextChanged(object sender, EventArgs e)
    {
        txtDemand.Text = Convert.ToString(Common.RoundDecimal(txtNetTax.Text.Trim(), 0) - Common.RoundDecimal(txtITDeposited.Text.Trim(), 0));
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=IT-Calculation.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grEmployee.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
            Response.Write(ex.Message);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the
        // specified ASP.NET server control at run time.
    }

    protected void btnUpdatePackage_Click(object sender, EventArgs e)
    {
        if (grEmployee.Rows.Count > 0)
        {
            objITMgr.UpdateITInSalaryPackage(grEmployee, "15", Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "IT Calculation");
            lblMsg.Text = "Income Tax Salary Package Item Updated Successfully";
        }
        else
        {
            lblMsg.Text = "";
        }
    }

    protected void btnSaveInvestmentEmail_Click(object sender, EventArgs e)
    {
        if (grEmployee.Rows.Count > 0)
        {
            objITMgr.InsertITCalculationEmailHistory(grEmployee, ddlMonth.SelectedValue.Trim(), ddlFiscalYear.SelectedValue.Trim(),
                Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));
            lblMsg.Text = "Income Tax salary package email information has been saved Successfully.";
        }
        else
        {
            lblMsg.Text = "";
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        this.ClearFields();
    }

    private void ClearFields()
    {
        //txtAssYear.Text = "";
        txtDemand.Text = "";
        txtEmpCode.Text = "";
        //txtEmpID.Text = "";
        txtG_Tax.Text = "";
        txtGender.Text = "";
        txtITDeposited.Text = "";
        txtNetTax.Text = "";
        txtP10.Text = "";
        txtP15.Text = "";
        txtP20.Text = "";
        txtP25.Text = "";
        txtP30.Text = "";
        txtRebate.Text = "";
        txtRefund.Text = "";
        txtT_HA.Text = "";
        txtT_TA.Text = "";
        txtTTI_1.Text = "";
        txtTTI_2.Text = "";
        txtYBasicSalary.Text = "";
        txtYFestivalBonus.Text = "";
        txtYHouseRent.Text = "";
        txtYOtherallowance.Text = "";
        txtYPFDeduction.Text = "";
        txtYTransportAllowance.Text = "";
        txtZ_M_F.Text = "";
    }

    private decimal SalaryPayslipDetTaxFiscalYrWs(string strEmpId)
    {
        decimal dclArrearAmt = 0;
        DataTable dtArrAmt = objPayrollMgr.SelectPayslipDetArrearTaxFiscalYrWs(ddlFiscalYear.SelectedValue.ToString(), strEmpId);
        if (dtArrAmt.Rows.Count > 0)
        {
            dclArrearAmt = Convert.ToDecimal(Common.ReturnZeroForNull(dtArrAmt.Rows[0]["PayAmt"].ToString()));
        }
        return dclArrearAmt;
    }
}
