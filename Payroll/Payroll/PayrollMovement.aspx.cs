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
using System.Xml;
using System.Text;
using System.IO;

public partial class Payroll_Payroll_PayrollMovement : System.Web.UI.Page
{
    MasterTablesManager objMastMg = new MasterTablesManager();
    Payroll_PreparationManager objPreMgr = new Payroll_PreparationManager();
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();
    PayrollReportManager objPayRptMgr = new PayrollReportManager();

    DataTable dtGrossSalHead = new DataTable();
    dsPayroll_Payslip objPayslip = new dsPayroll_Payslip();
    DataTable dtPayrollSummary;
    DataTable dtEmpPayroll = new DataTable();
    DataTable dtMonthlySummary;
    DataTable dtResult;

    DataTable dtSalaryHead = new DataTable();
    DataTable dtHeadCount = new DataTable();

    decimal dclEmpBenefits = 0;
    decimal dclEmpDeduct = 0;
    decimal dclTotalSalary = 0;
    decimal dclEmpPF = 0;

    string strAbsent = "";
    string strAddition = "";
    string strPrevMonthHeadCount = "";
    string strCurrMonthHeadCount = "";
    int inSalMoveStartId = 0; // Start Index for Salary Movement Statement
    string strAbsentEmpID = "";
    string strAdditionEmpID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            Common.FillDropDownList(objMastMg.SelectLocationCategory(0), ddlGenerateValue, "LocCatName", "LocCatId", false);
            Common.FillDropDownList(objPayrollMgr.SelectBankAndBranchList(), ddlBank, "BANKBRANCH", "RoutingNo", true, "Nil");
            //Common.FillDropDownList(objMastMg.SelectEmpGroup(0), ddlGroup, "GrpName", "EmpGrpID", false);
        }
    }

    protected void OpenRecord()
    {
        //dtSalHead = new DataTable();
        //dtSalHead = objPreMgr.GetSalaryHead();
    }

    protected void InitializeSummaryTable(int inCol)
    {
        int i = 0;
        dtPayrollSummary = new DataTable();
        for (i = 0; i < inCol; i++)
        {
            dtPayrollSummary.Columns.Add(i.ToString());
        }
    }

    protected void InitializeMonthlySummaryTable(int inCol)
    {
        int i = 0;
        dtMonthlySummary = new DataTable();
        for (i = 0; i < inCol; i++)
        {
            dtMonthlySummary.Columns.Add(i.ToString());
        }
    }

    protected void InitializeResultTable(int inCol)
    {
        int i = 0;
        dtResult = new DataTable();
        for (i = 0; i < inCol; i++)
        {
            dtResult.Columns.Add(i.ToString());
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        strAbsent = "";
        strAddition = "";
        strPrevMonthHeadCount = "";
        strCurrMonthHeadCount = "";
        inSalMoveStartId = 0; // Start Index for Salary Movement Statement
        strAbsentEmpID = "";
        strAdditionEmpID = "";

        this.GeneratePayrollReport();
        this.GeneratePrevMonthPayrollReport();

        this.GetMovemetResult();

        // Filing Emp Wise Movement Text saved in PayslipMst Table
        this.FillEmpWiseMovementText(grDetails);

        grPayroll.Visible = false;
        grPayrollPrevMonth.Visible = false;
        lblLog.Visible = false;
    }

    protected void GetMovemetResult()
    {
        grResult.HeaderRow.Cells[0].ForeColor = System.Drawing.Color.White;
        grResult.HeaderRow.Cells[0].Text = "";
        grResult.HeaderRow.Cells[1].ForeColor = System.Drawing.Color.White;
        grResult.HeaderRow.Cells[1].Text = "";
        grResult.HeaderRow.Cells[2].ForeColor = System.Drawing.Color.White;
        grResult.HeaderRow.Cells[2].Text = "";
        grResult.HeaderRow.Cells[3].ForeColor = System.Drawing.Color.White;
        grResult.HeaderRow.Cells[3].Text = "";
        grResult.HeaderRow.Cells[4].ForeColor = System.Drawing.Color.White;
        grResult.HeaderRow.Cells[4].Text = "";

        for (int i = 5; i < dtResult.Columns.Count; i++)
        {
            if (grDetails.Rows.Count > 0)
            {
                if (grPayrollSummary.FooterRow.Cells[i].Text.Trim() == grDetails.FooterRow.Cells[i].Text.Trim())
                {
                    grResult.Rows[0].Cells[i].Text = "TRUE";
                    grResult.Rows[0].Cells[i].ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    grResult.Rows[0].Cells[i].Text = "FALSE";
                    grResult.Rows[0].Cells[i].ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                grResult.Rows[0].Cells[i].Text = "TRUE";
                grResult.Rows[0].Cells[i].ForeColor = System.Drawing.Color.Green;
            }
            grResult.Rows[0].Cells[i].HorizontalAlign = HorizontalAlign.Right;
            grResult.Rows[0].Cells[i].Font.Bold = true;
        }
    }

    protected void CalculateMonthlySummaryDifference(GridView grv, string strMonth)
    {
        int i = 5;
        decimal decCurrValue = 0;
        decimal decPrevVal = 0;
        decimal decDiffValue = 0;

        grPayrollSummary.FooterRow.Cells[2].Text = "";
        grPayrollSummary.FooterRow.Cells[3].Text = strMonth;
        grPayrollSummary.FooterRow.Cells[4].Text = "Differ";
        grPayrollSummary.HeaderRow.Cells[4].Text = "";
        grPayrollSummary.HeaderRow.Cells[3].Text = "Payroll Month";
        grPayrollSummary.HeaderRow.Cells[2].Text = "";
        grPayrollSummary.HeaderRow.Cells[1].Text = "";
        grPayrollSummary.HeaderRow.Cells[0].Text = "";

        //grPayrollSummary.HeaderRow.Cells[3].ForeColor = System.Drawing.Color.White;
        //grPayrollSummary.HeaderRow.Cells[2].ForeColor = System.Drawing.Color.White;
        //grPayrollSummary.HeaderRow.Cells[1].ForeColor = System.Drawing.Color.White;
        //grPayrollSummary.HeaderRow.Cells[0].ForeColor = System.Drawing.Color.White;     

        for (i = 5; i < dtPayrollSummary.Columns.Count; i++)
        {
            decCurrValue = Common.RoundDecimal(grv.Rows[1].Cells[i].Text.Trim(), 2);
            decPrevVal = Common.RoundDecimal(grv.Rows[0].Cells[i].Text.Trim(), 2);
            decCurrValue = Math.Abs(decCurrValue);
            decPrevVal = Math.Abs(decPrevVal);
            decDiffValue = decCurrValue - decPrevVal;
            if (Math.Abs(decDiffValue) <= 0)
                grPayrollSummary.FooterRow.Cells[i].Text = "";
            else
                grPayrollSummary.FooterRow.Cells[i].Text = decDiffValue.ToString();

            grPayrollSummary.FooterRow.Cells[i].HorizontalAlign = HorizontalAlign.Right;
        }
    }

    protected void FillMonthlySummary(GridView grv)
    {
        dtMonthlySummary.Rows.Clear();
        dtMonthlySummary.Dispose();

        DataRow nRow1 = dtMonthlySummary.NewRow();
        dtMonthlySummary.Rows.Add(nRow1);
        DataRow nRow2 = dtMonthlySummary.NewRow();
        dtMonthlySummary.Rows.Add(nRow2);


        dtMonthlySummary.AcceptChanges();
        grv.DataSource = dtMonthlySummary;
        grv.DataBind();
    }

    protected void FillSingeRowResult(GridView grv)
    {
        dtResult.Rows.Clear();
        dtResult.Dispose();

        DataRow nRow1 = dtResult.NewRow();
        dtResult.Rows.Add(nRow1);

        dtResult.AcceptChanges();
        grv.DataSource = dtResult;
        grv.DataBind();
    }

    protected void GeneratePayrollReport()
    {
        string strEmpID = "";
        string strGenerateValue = "";

        int inBenefitHeadCount = 0;
        int inDeductCount = 0;
        decimal dclSalHeadAmt = 0;

        switch (ddlGeneratefor.SelectedValue.ToString())
        {
            case "O":
                strGenerateValue = ddlGenerateValue.SelectedValue.ToString();
                lblGenerateFor.Text = ddlGenerateValue.SelectedItem.Text.Trim();
                break;
            case "B":
                strGenerateValue = ddlBank.SelectedValue.ToString();
                lblGenerateFor.Text = ddlBank.SelectedItem.Text.Trim();
                break;
            case "E":
                strGenerateValue = txtTextValue.Text.Trim();
                break;
        }
        dtSalaryHead = objPayrollMgr.SelectTotalSalHeadWithSeq(0);
        dtHeadCount = objPayRptMgr.GetHeadCount();
        DataRow[] founHCRows = dtHeadCount.Select("DISPLAYTYPE='B'");
        inBenefitHeadCount = Convert.ToInt32(founHCRows[0]["HEADCOUNT"]);
        founHCRows = null;
        founHCRows = dtHeadCount.Select("DISPLAYTYPE='D'");
        inDeductCount = Convert.ToInt32(founHCRows[0]["HEADCOUNT"]);

        dtGrossSalHead = objPayrollMgr.SelectGrossSalHead(0);
        dtEmpPayroll = objPayRptMgr.GetPayrollData(ddlGeneratefor.SelectedValue.ToString(), strGenerateValue,
            ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), ddlBank.SelectedValue.Trim());
        
        this.InitializeSummaryTable(dtSalaryHead.Rows.Count + 11);

        int i = 5;
        int j = 1;
        foreach (DataRow dEmpRow in dtEmpPayroll.Rows)
        {
            dclEmpBenefits = 0;
            dclEmpDeduct = 0;
            dclTotalSalary = 0;
            this.GetEmpBenefitsAmount(dtSalaryHead, dEmpRow["EMPID"].ToString().Trim(), dEmpRow["GROSSAMNT"].ToString());
            i = 5;
            if (strEmpID == dEmpRow["EMPID"].ToString().Trim())
            {
                continue;
            }
            DataRow nRow = dtPayrollSummary.NewRow();
            nRow[0] = dEmpRow["PAYID"].ToString().Trim();
            nRow[1] = dEmpRow["EMPID"].ToString().Trim();
            nRow[2] = dEmpRow["FULLNAME"].ToString().Trim();
            nRow[3] = dEmpRow["JobTitleName"].ToString().Trim();
            nRow[4] = dEmpRow["GradeName"].ToString().Trim();

            foreach (DataRow dSalRow in dtSalaryHead.Rows)
            {
                //if (dSalRow["SHEADID"].ToString() == "9")
                //{
                //    i = i;
                //}

                if (i - 5 == dtGrossSalHead.Rows.Count)
                {
                    nRow[i] = Common.RoundDecimal(dEmpRow["GROSSAMNT"].ToString(), 0);
                    i++;
                }

                //if ((i - 5) - dtGrossSalHead.Rows.Count == inBenefitHeadCount + 1)
                //{

                //    nRow[i] = dclEmpBenefits.ToString();
                //    i++;
                //}

                if ((i - 5) - dtGrossSalHead.Rows.Count == inBenefitHeadCount + 1)
                {
                    nRow[i] = dclTotalSalary.ToString();
                    i++;

                    dclSalHeadAmt = 0;
                    dclSalHeadAmt = this.GetSalHeadAmt(dEmpRow["EMPID"].ToString().Trim(), dSalRow["SHEADID"].ToString().Trim());
                    if (dSalRow["DISPLAYTYPE"].ToString().Trim() == "D")
                    {
                        if (dclSalHeadAmt > 0)
                            dclSalHeadAmt = dclSalHeadAmt * -1;
                    }

                    nRow[i] = dclSalHeadAmt.ToString();
                    i++;
                }
                else
                {
                    dclSalHeadAmt = 0;
                    dclSalHeadAmt = this.GetSalHeadAmt(dEmpRow["EMPID"].ToString().Trim(), dSalRow["SHEADID"].ToString().Trim());
                    if (dSalRow["DISPLAYTYPE"].ToString().Trim() == "D")
                    {
                        if (dclSalHeadAmt > 0)
                            dclSalHeadAmt = dclSalHeadAmt * -1;
                    }

                    nRow[i] = dclSalHeadAmt.ToString();
                    i++;
                }
            }

            nRow[i] = dclEmpDeduct.ToString();
            i++;

            nRow[i] = Common.RoundDecimal(dEmpRow["NETPAY"].ToString(), 0);
            i++;

            nRow[i] = "0";
            i++;

            nRow[i] = dclEmpPF.ToString();

            dtPayrollSummary.Rows.Add(nRow);
            dtPayrollSummary.AcceptChanges();
            strEmpID = dEmpRow["EMPID"].ToString().Trim();
            j++;
        }

        grPayroll.DataSource = dtPayrollSummary;
        grPayroll.DataBind();

        if (dtPayrollSummary.Rows.Count > 0)
        {
            this.FormatGridView(grPayroll, dtSalaryHead, inBenefitHeadCount);
            this.GetSummaryTotal(grPayroll, ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), true);
            if (ddlGeneratefor.SelectedValue.Trim() == "E")
                lblGenerateFor.Text = grPayroll.Rows[0].Cells[2].Text.Trim() + " [" + grPayroll.Rows[0].Cells[1].Text.Trim() + "] ";
            //lblPayrollMonth.Text = "Salary for the month of " + Common.retMonthName(ddlMonth.SelectedValue.Trim()) + " " + ddlYear.SelectedItem.Text;


            strCurrMonthHeadCount = grPayroll.Rows.Count.ToString();
            lblCurrHeadCount.Text = "HR Count in " + Common.ReturnFullMonthName(ddlMonth.SelectedValue.Trim()) + " Payroll :  " + strCurrMonthHeadCount;

            this.InitializeMonthlySummaryTable(dtPayrollSummary.Columns.Count);
            this.FillMonthlySummary(grPayrollSummary);
            this.FormatGridView(grPayrollSummary, dtSalaryHead, inBenefitHeadCount);
            this.FillMonthlySummary(grMovement);


            this.GetMonthlySummaryData(grPayrollSummary, grPayroll, 1, Common.ReturnFullMonthName(ddlMonth.SelectedValue.Trim()));


            this.InitializeResultTable(dtPayrollSummary.Columns.Count);
            this.FillSingeRowResult(grResult);
            this.FormatGridView(grResult, dtSalaryHead, inBenefitHeadCount);

        }
        else
        {
            lblCurrHeadCount.Text = "HR Count in " + Common.ReturnFullMonthName(ddlMonth.SelectedValue.Trim()) + " Payroll :  " + "0";

            this.InitializeMonthlySummaryTable(dtPayrollSummary.Columns.Count);
            this.FillMonthlySummary(grPayrollSummary);
            this.FormatGridView(grPayrollSummary, dtSalaryHead, inBenefitHeadCount);

            this.InitializeResultTable(dtPayrollSummary.Columns.Count);
            this.FillMonthlySummary(grMovement);
            this.FillSingeRowResult(grResult);
            this.FormatGridView(grResult, dtSalaryHead, inBenefitHeadCount);
        }
    }

    protected void FormatGridView(GridView grv, DataTable dtSalaryHead, int inBenefitHeadCount)
    {
        int j = 5;
        double NumericCellWidth = dtPayrollSummary.Columns.Count - 4;
        NumericCellWidth = 79 / NumericCellWidth;

        string strRowIndx = "";
        grv.HeaderRow.Cells[0].Text = "Sl";
        grv.HeaderRow.Cells[0].Width = Unit.Percentage(1);
        grv.HeaderRow.Cells[1].Text = "Emp. ID";
        grv.HeaderRow.Cells[1].Width = Unit.Percentage(3);
        grv.HeaderRow.Cells[2].Text = "Employee Name";
        grv.HeaderRow.Cells[2].Width = Unit.Percentage(10);
        grv.HeaderRow.Cells[3].Text = "Designation";
        grv.HeaderRow.Cells[3].Width = Unit.Percentage(6);
        grv.HeaderRow.Cells[4].Text = "Grade";
        grv.HeaderRow.Cells[4].Width = Unit.Percentage(5);

        for (int i = 0; i < dtPayrollSummary.Columns.Count; i++)
        {
            if (j - 5 == dtGrossSalHead.Rows.Count)
            {
                grv.HeaderRow.Cells[j].Text = "Gross Salary";
                grv.HeaderRow.Cells[j].Width = Unit.Percentage(NumericCellWidth);
                grv.HeaderRow.Cells[j].HorizontalAlign = HorizontalAlign.Right;
                strRowIndx = j.ToString();
                j++;
            }
            //if ((j - 5) - dtGrossSalHead.Rows.Count == inBenefitHeadCount + 1)
            //{
            //    grv.HeaderRow.Cells[j].Text = "Total Benefits";
            //    grv.HeaderRow.Cells[j].Width = Unit.Percentage(NumericCellWidth);
            //    grv.HeaderRow.Cells[j].HorizontalAlign = HorizontalAlign.Right;
            //    strRowIndx = strRowIndx + "," + j.ToString();
            //    j++;
            //}
            if ((j - 5) - dtGrossSalHead.Rows.Count == inBenefitHeadCount + 1)
            {
                grv.HeaderRow.Cells[j].Text = "Total Salary";
                grv.HeaderRow.Cells[j].Width = Unit.Percentage(NumericCellWidth);
                grv.HeaderRow.Cells[j].HorizontalAlign = HorizontalAlign.Right;
                strRowIndx = strRowIndx + "," + j.ToString();
                inSalMoveStartId = j;
                j++;

                if (i < dtSalaryHead.Rows.Count)
                {
                    grv.HeaderRow.Cells[j].Text = dtSalaryHead.Rows[i]["SHORTNAME"].ToString();
                    grv.HeaderRow.Cells[j].Width = Unit.Percentage(NumericCellWidth);
                    grv.HeaderRow.Cells[j].HorizontalAlign = HorizontalAlign.Right;
                    j++;
                }
            }
            else
            {
                if (i < dtSalaryHead.Rows.Count)
                {
                    grv.HeaderRow.Cells[j].Text = dtSalaryHead.Rows[i]["SHORTNAME"].ToString();
                    grv.HeaderRow.Cells[j].Width = Unit.Percentage(NumericCellWidth);
                    grv.HeaderRow.Cells[j].HorizontalAlign = HorizontalAlign.Right;
                    j++;
                }
            }
        }

        grv.HeaderRow.Cells[j].Text = "Total Deduct";
        grv.HeaderRow.Cells[j].Width = Unit.Percentage(NumericCellWidth);
        grv.HeaderRow.Cells[j].HorizontalAlign = HorizontalAlign.Right;
        strRowIndx = strRowIndx + "," + j.ToString();
        j++;

        grv.HeaderRow.Cells[j].Text = "Net Taka";
        grv.HeaderRow.Cells[j].Width = Unit.Percentage(NumericCellWidth);
        grv.HeaderRow.Cells[j].HorizontalAlign = HorizontalAlign.Right;
        strRowIndx = strRowIndx + "," + j.ToString();
        j++;

        grv.HeaderRow.Cells[j].Text = "Net US$";
        grv.HeaderRow.Cells[j].Width = Unit.Percentage(NumericCellWidth);
        grv.HeaderRow.Cells[j].HorizontalAlign = HorizontalAlign.Right;
        strRowIndx = strRowIndx + "," + j.ToString();
        j++;

        grv.HeaderRow.Cells[j].Text = "Comp. PF";
        grv.HeaderRow.Cells[j].Width = Unit.Percentage(NumericCellWidth);
        grv.HeaderRow.Cells[j].HorizontalAlign = HorizontalAlign.Right;
        strRowIndx = strRowIndx + "," + j.ToString();

        this.HighlightGridViewCells(strRowIndx, grv);
    }

    protected void GetEmpBenefitsAmount(DataTable dtSalHead, string strEmpID, string strGrossSal)
    {
        dclTotalSalary = Convert.ToDecimal(strGrossSal);
        decimal dclSalHeadAmt = 0;
        foreach (DataRow dRow in dtSalHead.Rows)
        {
            switch (dRow["DISPLAYTYPE"].ToString())
            {
                case "B":
                    dclEmpBenefits = dclEmpBenefits + this.GetSalHeadAmt(strEmpID, dRow["SHEADID"].ToString());
                    break;
                case "D":
                    dclSalHeadAmt = this.GetSalHeadAmt(strEmpID, dRow["SHEADID"].ToString());

                    if (dclSalHeadAmt > 0)
                        dclSalHeadAmt = dclSalHeadAmt * -1;

                    dclEmpDeduct = dclEmpDeduct + dclSalHeadAmt;
                    break;
            }

            //PF
            if (dRow["ISPF"].ToString() == "Y")
            {
                dclEmpPF = this.GetSalHeadAmt(strEmpID, dRow["SHEADID"].ToString()) * -1;
            }
        }

        dclTotalSalary = dclTotalSalary + dclEmpBenefits;
        dclTotalSalary = Common.RoundDecimal(dclTotalSalary.ToString(), 0);
        dclEmpBenefits = Common.RoundDecimal(dclEmpBenefits.ToString(), 0);
        dclEmpDeduct = Common.RoundDecimal(dclEmpDeduct.ToString(), 0);
        dclEmpPF = Common.RoundDecimal(dclEmpPF.ToString(), 0);
    }

    protected void HighlightGridViewCells(string strIndx, GridView grv)
    {
        string[] strArr = strIndx.Split(',');
        int indx = 0;
        foreach (GridViewRow gRow in grv.Rows)
        {
            foreach (string str in strArr)
            {
                indx = Convert.ToInt32(str);
                gRow.Cells[indx].Font.Bold = true;
            }
        }
    }

    protected void GetSummaryTotal(GridView grv, string strMonth, string strYear, bool IsABS)
    {
        int i = 0;
        decimal dclSumValue = 0;
        grv.FooterRow.Cells[3].Text = Common.ReturnFullMonthName(strMonth) + " " + strYear;
        grv.FooterRow.Cells[4].Text = "Total ";
        for (int j = 5; j < dtPayrollSummary.Columns.Count; j++)
        {
            dclSumValue = 0;
            i = 0;
            foreach (DataRow dRow in dtPayrollSummary.Rows)
            {
                dclSumValue = dclSumValue + Common.RoundDecimal(dRow[j].ToString(), 2);
                if (Common.RoundDecimal(dRow[j].ToString(), 0) == 0)
                {
                    grv.Rows[i].Cells[j].Text = "";
                }
                grv.Rows[i].Cells[j].HorizontalAlign = HorizontalAlign.Right;
                i++;
            }
            if (dclSumValue == 0)
                grv.FooterRow.Cells[j].Text = "";
            else
            {
                if (IsABS == true)
                {
                    grv.FooterRow.Cells[j].Text = Convert.ToString(Math.Abs(dclSumValue));
                }
                else
                {
                    grv.FooterRow.Cells[j].Text = Convert.ToString(dclSumValue);
                }
            }
            grv.FooterRow.Cells[j].HorizontalAlign = HorizontalAlign.Right;
        }
    }

    protected bool IsGrossHead(string strSHeadID)
    {
        DataRow[] foundRows = dtGrossSalHead.Select("SHEADID=" + strSHeadID);
        if (foundRows.Length > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected decimal GetSalHeadAmt(string strEmpID, string strSHeadID)
    {
        decimal dclSalHeadAmt = 0;
        DataRow[] foundRows = dtEmpPayroll.Select("EMPID='" + strEmpID + "' AND SHEADID=" + strSHeadID);
        if (foundRows.Length > 0)
        {
            dclSalHeadAmt = Convert.ToDecimal(foundRows[0]["PAYAMT"].ToString());
        }
        dclSalHeadAmt = Common.RoundDecimal(dclSalHeadAmt.ToString(), 0);
        return dclSalHeadAmt;
    }

    protected void grPayslipMst_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        //string strPreYrLv = "";
        switch (_commandName)
        {
            case ("DoubleClick"):
                //Open New Window
                StringBuilder sb = new StringBuilder();
                string strURL = "PaySlipDetails.aspx?params=" + grPayroll.SelectedRow.Cells[1].Text.Trim() + ","
                    + grPayroll.SelectedRow.Cells[2].Text.Trim() + ","
                    // + grPayslipMst.SelectedRow.Cells[3].Text.Trim() + ","
                    + grPayroll.SelectedRow.Cells[21].Text.Trim() + ","
                    + grPayroll.SelectedRow.Cells[9].Text.Trim() + ","
                    + grPayroll.SelectedRow.Cells[13].Text.Trim();
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

    protected void GetMonthlySummaryData(GridView grvSum, GridView grvData, int rowIndx, string strSalMonth)
    {
        //grvSum.Rows[rowIndx].Cells[2].Text = strSalMonth;
        for (int i = 0; i < dtPayrollSummary.Columns.Count; i++)
        {
            grvSum.Rows[rowIndx].Cells[i].Text = Common.CheckNullString(grvData.FooterRow.Cells[i].Text.Trim());
            if (i <= 3)
                grvSum.Rows[rowIndx].Cells[i].HorizontalAlign = HorizontalAlign.Left;
            else
                grvSum.Rows[rowIndx].Cells[i].HorizontalAlign = HorizontalAlign.Right;
        }
        grvSum.Rows[rowIndx].Cells[0].Text = "";
        grvSum.Rows[rowIndx].Cells[0].ForeColor = System.Drawing.Color.White;
        grvSum.Rows[rowIndx].Cells[1].Text = "";
        grvSum.Rows[rowIndx].Cells[1].ForeColor = System.Drawing.Color.White;
    }

    #region Previous Month Summery Data
    protected void GeneratePrevMonthPayrollReport()
    {
        string strEmpID = "";
        string strGenerateValue = "";
        int inBenefitHeadCount = 0;
        int inDeductCount = 0;
        decimal dclSalHeadAmt = 0;

        switch (ddlGeneratefor.SelectedValue.ToString())
        {
            case "O":
                strGenerateValue = ddlGenerateValue.SelectedValue.ToString();
                lblGenerateFor.Text = ddlGenerateValue.SelectedItem.Text.Trim();
                break;
            case "B":
                strGenerateValue = ddlBank.SelectedValue.ToString();
                lblGenerateFor.Text = ddlBank.SelectedItem.Text.Trim();
                break;
            case "E":
                strGenerateValue = txtTextValue.Text.Trim();
                break;
        }

        DataRow[] founHCRows = dtHeadCount.Select("DISPLAYTYPE='B'");
        inBenefitHeadCount = Convert.ToInt32(founHCRows[0]["HEADCOUNT"]);
        founHCRows = null;
        founHCRows = dtHeadCount.Select("DISPLAYTYPE='D'");
        inDeductCount = Convert.ToInt32(founHCRows[0]["HEADCOUNT"]);

        string strPrevMonth = Common.GetPreviousMonth(ddlMonth.SelectedValue.ToString());
        string strPrevYear = ddlYear.SelectedValue.ToString();
        if (strPrevMonth == "12")
            strPrevYear = Convert.ToString(Convert.ToInt32(strPrevYear) - 1);

        dtPayrollSummary.Rows.Clear();
        dtPayrollSummary.Dispose();

        dtEmpPayroll = objPayRptMgr.GetPayrollData(ddlGeneratefor.SelectedValue.ToString(), strGenerateValue,
            strPrevMonth, strPrevYear, ddlBank.SelectedValue.Trim());

        int i = 5;
        int j = 1;
        if (dtEmpPayroll.Rows.Count == 0)
        {
            DataRow nRow = dtPayrollSummary.NewRow();
            dtPayrollSummary.Rows.Add(nRow);
            dtPayrollSummary.AcceptChanges();
        }

        foreach (DataRow dEmpRow in dtEmpPayroll.Rows)
        {
            dclEmpBenefits = 0;
            dclEmpDeduct = 0;
            dclTotalSalary = 0;
            this.GetEmpBenefitsAmount(dtSalaryHead, dEmpRow["EMPID"].ToString().Trim(), dEmpRow["GROSSAMNT"].ToString());
            i = 5;
            if (strEmpID == dEmpRow["EMPID"].ToString().Trim())
            {
                continue;
            }
            DataRow nRow = dtPayrollSummary.NewRow();
            nRow[0] = dEmpRow["PAYID"].ToString().Trim();
            nRow[1] = dEmpRow["EMPID"].ToString().Trim();
            nRow[2] = dEmpRow["FULLNAME"].ToString().Trim();
            nRow[3] = dEmpRow["JobTitleName"].ToString().Trim();
            nRow[4] = dEmpRow["GradeName"].ToString().Trim();

            foreach (DataRow dSalRow in dtSalaryHead.Rows)
            {
                if (i - 5 == dtGrossSalHead.Rows.Count)
                {
                    nRow[i] = Common.RoundDecimal(dEmpRow["GROSSAMNT"].ToString(), 0);
                    i++;
                }
                //if ((i - 5) - dtGrossSalHead.Rows.Count == inBenefitHeadCount + 1)
                //{
                //    nRow[i] = dclEmpBenefits.ToString();
                //    i++;
                //}
                if ((i - 5) - dtGrossSalHead.Rows.Count == inBenefitHeadCount + 1)
                {
                    nRow[i] = dclTotalSalary.ToString();
                    i++;

                    dclSalHeadAmt = 0;
                    dclSalHeadAmt = this.GetSalHeadAmt(dEmpRow["EMPID"].ToString().Trim(), dSalRow["SHEADID"].ToString().Trim());
                    if (dSalRow["DISPLAYTYPE"].ToString().Trim() == "D")
                    {
                        if (dclSalHeadAmt > 0)
                            dclSalHeadAmt = dclSalHeadAmt * -1;
                    }

                    nRow[i] = dclSalHeadAmt.ToString();
                    i++;
                }
                else
                {
                    dclSalHeadAmt = 0;
                    dclSalHeadAmt = this.GetSalHeadAmt(dEmpRow["EMPID"].ToString().Trim(), dSalRow["SHEADID"].ToString().Trim());
                    if (dSalRow["DISPLAYTYPE"].ToString().Trim() == "D")
                    {
                        if (dclSalHeadAmt > 0)
                            dclSalHeadAmt = dclSalHeadAmt * -1;
                    }

                    nRow[i] = dclSalHeadAmt.ToString();
                    i++;
                }
            }
            nRow[i] = dclEmpDeduct.ToString();
            i++;

            nRow[i] = Common.RoundDecimal(dEmpRow["NETPAY"].ToString(), 0);
            i++;

            nRow[i] = "0";
            i++;

            nRow[i] = dclEmpPF.ToString();

            dtPayrollSummary.Rows.Add(nRow);
            dtPayrollSummary.AcceptChanges();
            strEmpID = dEmpRow["EMPID"].ToString().Trim();
            j++;
        }

        grPayrollPrevMonth.DataSource = dtPayrollSummary;
        grPayrollPrevMonth.DataBind();
        if (dtPayrollSummary.Rows.Count > 0)
        {
            this.FormatGridView(grPayrollPrevMonth, dtSalaryHead, inBenefitHeadCount);
            this.GetSummaryTotal(grPayrollPrevMonth, strPrevMonth, strPrevYear, true);
            lblPayrollMonth.Text = "Payroll cost for the month of " + Common.ReturnFullMonthName(strPrevMonth) + " " + strPrevYear
                + " and " + Common.ReturnFullMonthName(ddlMonth.SelectedValue.Trim()) + " " + ddlYear.SelectedValue.Trim();


            if (dtEmpPayroll.Rows.Count == 0)
                strPrevMonthHeadCount = "";
            else
                strPrevMonthHeadCount = grPayrollPrevMonth.Rows.Count.ToString();
            lblPrevHeadCount.Text = "HR Count in " + Common.ReturnFullMonthName(strPrevMonth) + " Payroll :  " + strPrevMonthHeadCount;


            this.GetMonthlySummaryData(grPayrollSummary, grPayrollPrevMonth, 0, Common.ReturnFullMonthName(strPrevMonth));
            this.CalculateMonthlySummaryDifference(grPayrollSummary, Common.retMonthName(ddlMonth.SelectedValue.Trim()) + " - " + Common.retMonthName(strPrevMonth));

            dtPayrollSummary.Rows.Clear();
            dtPayrollSummary.Dispose();

            if (dtEmpPayroll.Rows.Count == 0)
            {
                this.CompareMonthlyPayroll(grPayroll, grPayrollPrevMonth, 0);
            }
            else
            {
                this.CompareMonthlyPayroll(grPayroll, grPayrollPrevMonth, 0);
                this.CompareMonthlyPayroll(grPayrollPrevMonth, grPayroll, 1);
            }
            if (grDetails.Rows.Count > 0)
            {
                this.FormatGridView(grDetails, dtSalaryHead, inBenefitHeadCount);
                this.GetSummaryTotal(grDetails, "", "", false);
                this.FormatPayollSerial(grDetails, grPayroll);
            }
            else
            {

            }
            // if (strAddition != "")
            lblAddition.Text = " Addition : " + strAddition;
            // if (strAbsent != "")
            lblAbsent.Text = " Absent : " + strAbsent;
            this.FillSalaryMovementStatement(inSalMoveStartId);
        }
        else
        {
            lblPrevHeadCount.Text = "HR Count in " + Common.ReturnFullMonthName(strPrevMonth) + " Payroll :  " + "0";
            lblAddition.Text = "";
            lblAbsent.Text = "";
            //lblGenerateFor.Text = "";
            //lblPayrollMonth.Text = "";
        }
    }

    protected void FormatPayollSerial(GridView grDet, GridView grPay)
    {
        bool IsAbsentEmp = true;
        foreach (GridViewRow gDRow in grDet.Rows)
        {
            IsAbsentEmp = true;
            foreach (GridViewRow gPRow in grPay.Rows)
            {
                if (gDRow.Cells[1].Text.Trim() == gPRow.Cells[1].Text.Trim())
                {
                    IsAbsentEmp = false;
                    gDRow.Cells[0].Text = gPRow.Cells[0].Text.Trim();
                }
            }
            if (IsAbsentEmp == true)
            {
                gDRow.Cells[0].Text = "0";
            }
        }
    }
    #endregion

    #region Compare Payroll
    public void CompareMonthlyPayroll(GridView grvCurrMonth, GridView grvPrevMonth, int Indx)
    {
        int i = 5;
        int j = 0;
        bool IsEmpExist = false;
        bool IsAmountDiffer = false;
        //dtPayrollSummary.Rows.Clear();
        decimal dclCurrValue = 0;
        decimal dclPrevValue = 0;
        decimal dclDiffValue = 0;

        foreach (GridViewRow gCurrRow in grvCurrMonth.Rows)
        {
            dclCurrValue = 0;
            dclPrevValue = 0;
            dclDiffValue = 0;
            IsEmpExist = false;
            DataRow nRow = dtPayrollSummary.NewRow();
            nRow[0] = Convert.ToString(j++);
            nRow[1] = gCurrRow.Cells[1].Text.Trim();
            nRow[2] = gCurrRow.Cells[2].Text.Trim();
            nRow[3] = gCurrRow.Cells[3].Text.Trim();
            nRow[4] = gCurrRow.Cells[4].Text.Trim();

            foreach (GridViewRow gPrevRow in grvPrevMonth.Rows)
            {
                if (gCurrRow.Cells[1].Text.Trim() == gPrevRow.Cells[1].Text.Trim())
                {
                    IsEmpExist = true;
                    IsAmountDiffer = false;
                    for (i = 5; i < dtPayrollSummary.Columns.Count; i++)
                    {
                        if (Common.RoundDecimal(gCurrRow.Cells[i].Text.Trim(), 2) != Common.RoundDecimal(gPrevRow.Cells[i].Text.Trim(), 2))
                        {
                            IsAmountDiffer = true;
                            dclCurrValue = Math.Abs(Common.RoundDecimal(gCurrRow.Cells[i].Text.Trim(), 2));
                            dclPrevValue = Math.Abs(Common.RoundDecimal(gPrevRow.Cells[i].Text.Trim(), 2));
                            if (Indx == 0)
                                dclDiffValue = dclCurrValue - dclPrevValue;
                            else
                                dclDiffValue = dclPrevValue - dclCurrValue;
                            nRow[i] = dclDiffValue.ToString();

                            gCurrRow.Cells[i].ForeColor = System.Drawing.Color.Red;
                            gPrevRow.Cells[i].ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            gCurrRow.Cells[i].Text = "";
                            gPrevRow.Cells[i].Text = "";
                        }
                    }
                    if (IsAmountDiffer == false)
                    {
                        gCurrRow.Visible = false;
                        gPrevRow.Visible = false;
                    }
                    if (IsAmountDiffer == true)
                    {
                        if (dtPayrollSummary.Rows.Count > 0)
                        {
                            if (Common.FindInDataTable(dtPayrollSummary, gCurrRow.Cells[1].Text.Trim(), "1") == false)
                            {
                                dtPayrollSummary.Rows.Add(nRow);
                                dtPayrollSummary.AcceptChanges();
                            }
                        }
                        else
                        {
                            dtPayrollSummary.Rows.Add(nRow);
                            dtPayrollSummary.AcceptChanges();
                        }
                    }
                }
            }
            // If New Employee
            if (IsEmpExist == false)
            {
                for (i = 5; i < dtPayrollSummary.Columns.Count; i++)
                {
                    dclCurrValue = 0;
                    gCurrRow.Cells[i].ForeColor = System.Drawing.Color.Red;
                    dclCurrValue = Math.Abs(Common.RoundDecimal(gCurrRow.Cells[i].Text.Trim(), 2));
                    if (Indx == 0)
                    {
                        nRow[i] = dclCurrValue.ToString();
                    }
                    else
                        nRow[i] = Convert.ToString(dclCurrValue * -1);

                }
                if (Indx == 0)
                {
                    if (strAddition == "")
                    {
                        strAddition = gCurrRow.Cells[1].Text.Trim() + "-" + gCurrRow.Cells[2].Text.Trim();
                        strAdditionEmpID = gCurrRow.Cells[1].Text.Trim();
                    }
                    else
                    {
                        strAddition = strAddition + ", " + gCurrRow.Cells[1].Text.Trim() + "-" + gCurrRow.Cells[2].Text.Trim();
                        strAdditionEmpID = strAdditionEmpID + "," + gCurrRow.Cells[1].Text.Trim();
                    }
                }
                else
                {
                    if (strAbsent == "")
                    {
                        strAbsent = gCurrRow.Cells[1].Text.Trim() + "-" + gCurrRow.Cells[2].Text.Trim();
                        strAbsentEmpID = strAbsentEmpID + gCurrRow.Cells[1].Text.Trim();
                    }
                    else
                    {
                        strAbsent = strAbsent + ", " + gCurrRow.Cells[1].Text.Trim() + "-" + gCurrRow.Cells[2].Text.Trim();
                        strAbsentEmpID = strAbsentEmpID + "," + gCurrRow.Cells[1].Text.Trim();
                    }
                }
                dtPayrollSummary.Rows.Add(nRow);
                dtPayrollSummary.AcceptChanges();
            }
        }

        grDetails.DataSource = dtPayrollSummary;
        grDetails.DataBind();

        grMovementDetls.DataSource = dtPayrollSummary;
        grMovementDetls.DataBind();
    }

    private void FillEmpWiseMovementText(GridView gr)
    {
        DataTable dtMovText = new DataTable();
        dtMovText.Columns.Add("EMPID");
        dtMovText.Columns.Add("PAYID");
        dtMovText.Columns.Add("EMPLOYEE");
        dtMovText.Columns.Add("REMARKS");
        dtMovText.Columns.Add("ISPRINTTOPAYSLIP");
        dtMovText.AcceptChanges();

        foreach (GridViewRow gRow in gr.Rows)
        {
            DataRow nRow = dtMovText.NewRow();
            nRow["EMPID"] = gRow.Cells[1].Text.Trim();
            nRow["PAYID"] = gRow.Cells[0].Text.Trim();
            nRow["EMPLOYEE"] = gRow.Cells[1].Text.Trim() + " " + gRow.Cells[2].Text.Trim();
            nRow["REMARKS"] = objPayRptMgr.GetPayrollRemarks(gRow.Cells[1].Text.Trim(), ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim());
            if (Common.CheckNullString(nRow["REMARKS"].ToString().Trim()) == "")
            {
                if (gRow.Cells[0].Text.Trim() == "0")
                    nRow["REMARKS"] = "Seperated effective from " + Common.DisplayDate(objPayRptMgr.GetEmpJoinOrSeperateDate(gRow.Cells[1].Text.Trim(), "SeparateDate"));
            }

            nRow["ISPRINTTOPAYSLIP"] = objPayRptMgr.GetPayrollRemarksP2P(gRow.Cells[1].Text.Trim(), ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim());
            dtMovText.Rows.Add(nRow);
        }
        dtMovText.AcceptChanges();

        grMoveText.DataSource = dtMovText;
        grMoveText.DataBind();
    }
    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        // this.ValidateAndSave();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (lblLog.Visible == false)
        {
            grPayroll.Visible = true;
            grPayrollPrevMonth.Visible = true;
            lblLog.Visible = true;
            btnShow.Text = "Hide Movement Log";
        }
        else
        {
            grPayroll.Visible = false;
            grPayrollPrevMonth.Visible = false;
            lblLog.Visible = false;
            btnShow.Text = "Show Movement Log";
        }
    }

    protected void FillSalaryMovementStatement(int Indx)
    {
        Indx = Indx - 1;
        int i = Indx;
        int j = 0;
        int k = 0;
        string[] strAbsEmpArr = strAbsentEmpID.Split(','); ;
        string[] strAddEmpArr = strAdditionEmpID.Split(',');

        foreach (GridViewRow gRowSumm in grPayrollSummary.Rows)
        {
            k = 0;

            for (i = Indx; i < dtMonthlySummary.Columns.Count; i++)
            {
                if (i == Indx)
                {
                    grMovement.Rows[j].Cells[0].Text = "Gross Salary for " + gRowSumm.Cells[2].Text;
                    grMovement.FooterRow.Cells[0].Text = "Salary Increased/(Decreased) in " + grPayrollSummary.Rows[1].Cells[2].Text;
                    grMovement.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                }
                else
                {
                    if (k <= grMovement.Columns.Count - 1)
                    {
                        if (grPayrollSummary.HeaderRow.Cells[i].Text.Trim() == "Others(-)")
                            continue;
                        grMovement.Rows[j].Cells[k].Text = gRowSumm.Cells[i].Text.Trim();
                        grMovement.Rows[j].Cells[k].HorizontalAlign = HorizontalAlign.Right;
                        if (j == 0)
                        {
                            grMovement.FooterRow.Cells[k].Text = grPayrollSummary.FooterRow.Cells[i].Text.Trim();
                        }
                    }
                }
                k++;
            }
            j++;
        }

        j = 0;
        k = 0;
        string strDate = "";
        bool IsJoinOrSeperate = false;
        foreach (GridViewRow gRow in grDetails.Rows)
        {
            TextBox txtD = (TextBox)grMovementDetls.Rows[j].Cells[2].FindControl("txtDescrip");
            k = 0;
            IsJoinOrSeperate = false;
            for (i = Indx; i < dtMonthlySummary.Columns.Count; i++)
            {
                if (i == Indx)
                {
                    grMovementDetls.Rows[j].Cells[0].Text = gRow.Cells[0].Text;
                    grMovementDetls.Rows[j].Cells[1].Text = gRow.Cells[2].Text;
                    grMovementDetls.Rows[j].Cells[1].ToolTip = gRow.Cells[1].Text;
                    grMovementDetls.FooterRow.Cells[2].Text = "Salary Increased/(Decreased) in " + grPayrollSummary.Rows[1].Cells[2].Text;
                    grMovementDetls.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;

                    // Auto description

                    if (strAbsentEmpID != "")
                    {
                        if (Common.FindInString(gRow.Cells[1].Text.Trim(), strAbsEmpArr) == true)
                        {
                            IsJoinOrSeperate = true;
                            strDate = objPayRptMgr.GetEmpJoinOrSeperateDate(gRow.Cells[1].Text.Trim(), "SeparateDate");
                            if (string.IsNullOrEmpty(strDate) == false)
                            {
                                strDate = Common.DisplayDate(strDate);
                                txtD.Text = "Seperated on " + strDate;
                            }
                            else
                            {
                                txtD.Text = "Payroll may not prepared";
                            }
                        }
                    }

                    if (strAdditionEmpID != "")
                    {
                        if (Common.FindInString(gRow.Cells[1].Text.Trim(), strAddEmpArr) == true)
                        {
                            IsJoinOrSeperate = true;
                            strDate = objPayRptMgr.GetEmpJoinOrSeperateDate(gRow.Cells[1].Text.Trim(), "JoiningDate");
                            if (string.IsNullOrEmpty(strDate) == false)
                            {
                                strDate = Common.DisplayDate(strDate);
                                txtD.Text = "New employee joined on " + strDate;
                            }
                            else
                            {
                                txtD.Text = "New employee joined on " + "N/A";
                            }
                        }
                    }
                }
                else
                {
                    if (k <= grMovement.Columns.Count - 1)
                    {
                        if (grDetails.HeaderRow.Cells[i].Text.Trim() == "Others(-)")
                            continue;
                        grMovementDetls.Rows[j].Cells[k + 2].Text = gRow.Cells[i].Text;
                        if (Common.CheckNullString(gRow.Cells[i].Text) != "")
                        {
                            if (IsJoinOrSeperate == false)
                            {
                                if (txtD.Text == "")
                                {
                                    txtD.Text = grDetails.HeaderRow.Cells[i].Text.Trim() + " changed";
                                }
                                else
                                {
                                    txtD.Text = txtD.Text + ", " + grDetails.HeaderRow.Cells[i].Text.Trim() + " changed";
                                }
                            }
                        }

                        grMovementDetls.Rows[j].Cells[k + 2].HorizontalAlign = HorizontalAlign.Right;
                        if (j == 0)
                        {
                            grMovementDetls.FooterRow.Cells[k + 2].Text = grDetails.FooterRow.Cells[i].Text;
                        }
                    }
                }
                k++;
            }
            j++;
        }
    }

    protected void btnSaveAndApprove_Click(object sender, EventArgs e)
    {
        string strBank = "";
        string strOffice = "";
        string strMonthBetween = "";
        if (grDetails.Rows.Count > 0)
        {
            if (Common.CheckNullString(grDetails.Rows[0].Cells[1].Text) == "")
            {
                lblMsgMoveStatement.Text = "No Data to Save";
                ModalPopupTree.Show();
            }
        }
        switch (ddlGeneratefor.SelectedValue.ToString())
        {
            case "O":
                strOffice = ddlGenerateValue.SelectedValue.ToString();
                break;
            case "B":
                strBank = ddlBank.SelectedValue.ToString();
                break;
            case "E":
                strOffice = "";
                strBank = "";
                break;
            case "A":
                strOffice = "";
                strBank = "";
                break;
        }
        strMonthBetween = grPayrollSummary.Rows[0].Cells[2].Text.Trim() + " to " + grPayrollSummary.Rows[1].Cells[2].Text.Trim();
        Payroll_MovementStatement objMovMgr = new Payroll_MovementStatement();
        if (ddlGeneratefor.SelectedValue.ToString().Trim() != "E")
            objMovMgr.SaveAndApproveMoveStatement(grMovement, grMovementDetls, ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim(),
                strBank, strOffice, strMonthBetween, Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));
        else
            lblMsgMoveStatement.Text = "Individual employee movement statement cannot be prepared";
    }

    protected void btnEditMovement_Click(object sender, EventArgs e)
    {
        string strGenerateValue = "";
        string strGenerateText = "";
        switch (ddlGeneratefor.SelectedValue.ToString())
        {
            case "B":
                strGenerateValue = ddlBank.SelectedValue.ToString();
                strGenerateText = ddlBank.SelectedItem.Text.Trim();
                break;
            case "O":
                strGenerateValue = ddlGenerateValue.SelectedValue.ToString();
                strGenerateText = ddlGenerateValue.SelectedItem.Text.Trim();
                break;
            case "A":
                strGenerateValue = "";
                break;
        }

        StringBuilder sb = new StringBuilder();
        string strURL = "MovementStatementEdit.aspx?params=" + ddlMonth.SelectedValue.ToString().Trim() + ","
                                                                     + ddlYear.SelectedValue.Trim() + ","
                                                                     + ddlGeneratefor.SelectedValue.Trim() + ","
                                                                     + strGenerateValue + ","
                                                                     + strGenerateText;

        sb.Append("<script>");
        //sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
        sb.Append("window.open('" + strURL + "', '', '');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                 sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
    }

    protected void btnPrintMovement_Click(object sender, EventArgs e)
    {
        string strGenerateValue = "";
        string strGenerateText = "";
        switch (ddlGeneratefor.SelectedValue.ToString())
        {
            case "B":
                strGenerateValue = ddlBank.SelectedValue.ToString();
                strGenerateText = ddlBank.SelectedItem.Text.Trim();
                break;
            case "A":
                strGenerateValue = "";
                break;
        }

        StringBuilder sb = new StringBuilder();
        string strURL = "SalaryMovementStatementReport.aspx?params=" + ddlMonth.SelectedValue.ToString().Trim() + ","
                                                                     + ddlYear.SelectedValue.Trim() + ","
                                                                     + ddlGeneratefor.SelectedValue.Trim() + ","
                                                                     + strGenerateValue + ","
                                                                     + strGenerateText;

        sb.Append("<script>");
        //sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
        sb.Append("window.open('" + strURL + "', '', '');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                 sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
    }

    protected void grMoveText_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("ViewClick"):
                //Open New Window
                hfMoveTextEmpID.Value = grMoveText.DataKeys[_gridView.SelectedIndex].Values[0].ToString().Trim();
                hfMoveTextMonth.Value = ddlMonth.SelectedValue.Trim();
                hfMoveTextYear.Value = ddlYear.SelectedValue.Trim();
                txtMoveText.Text = Common.CheckNullString(grMoveText.SelectedRow.Cells[3].Text.Trim());
                if (Common.CheckNullString(grMoveText.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim()) != "")
                    chkP2P.Checked = grMoveText.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim() == "Y" ? true : false;
                else
                    chkP2P.Checked = false;
                ModalpopupextenderMoveText.Show();
                break;
        }
    }

    protected void btnSaveMoveText_Click(object sender, EventArgs e)
    {
        objPayRptMgr.UpdatePayrollRemarks(txtMoveText.Text.Trim(), chkP2P.Checked == true ? "Y" : "N", hfMoveTextEmpID.Value.Trim(), hfMoveTextMonth.Value.Trim(), hfMoveTextYear.Value.Trim());
        grMoveText.SelectedRow.Cells[2].Text = Common.CheckNullString(txtMoveText.Text.Trim());
    }

    protected void imgbtnSaveMoveText_Click(object sender, ImageClickEventArgs e)
    {
        objPayRptMgr.UpdatePayrollRemarks(txtMoveText.Text.Trim(), chkP2P.Checked == true ? "Y" : "N",
            hfMoveTextEmpID.Value.Trim(), hfMoveTextMonth.Value.Trim(), hfMoveTextYear.Value.Trim());
        grMoveText.SelectedRow.Cells[3].Text = Common.CheckNullString(txtMoveText.Text.Trim());
    }
}
