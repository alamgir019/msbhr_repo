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

public partial class Payroll_Payroll_EmpWisePayrollMovement : System.Web.UI.Page
{
    MasterTablesManager objMastMg = new MasterTablesManager();
    Payroll_PreparationManager objPreMgr = new Payroll_PreparationManager();
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();
    PayrollReportManager objPayRptMgr = new PayrollReportManager();
    EmpInfoManager objEmp = new EmpInfoManager();

    DataTable dtGrossSalHead = new DataTable();
    dsPayroll_Payslip objPayslip = new dsPayroll_Payslip();
    DataTable dtPayrollSummary;
    DataTable dtEmpPayroll = new DataTable();

    decimal dclEmpBenefits = 0;
    decimal dclEmpDeduct = 0;
    decimal dclTotalSalary = 0;
    decimal dclEmpPF = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);
            Common.FillDropDownList(objEmp.SelectEmpNameWithID("A"), ddlEmployee, "EmpName", "EmpID", false);
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

    //protected void FillGenerateDropDownList()
    //{
    //    switch(ddlGeneratefor.SelectedValue)
    //    {
    //        case "O":
    //           // Common.FillDropDownList_Nil(objMasMgr.SelectLocation(0), ddlDivision);
    //            Common.FillDropDownList(objMastMg.SelectLocation(0), ddlGenerateValue, "LocationName", "LocationID", false);
    //            break;
    //        case "A":
    //            ddlGenerateValue.DataSource = null;
    //            ddlGenerateValue.DataBind();
    //            break;
    //        case "B":
    //            Common.FillDropDownList(objEmpInfoMgr.SelectBankList(), ddlGenerateValue, "BankName", "BankCode", true, "Nil");
    //            break;
    //    }

    //}  

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        this.GeneratePayrollReport();    
    }

    protected void GeneratePayrollReport()
    {
        string strVmonth = "";
        int inBenefitHeadCount = 0;
        int inDeductCount = 0;
        decimal dclSalHeadAmt = 0;

        //lblGenerateFor.Text = ddlGenerateValue.SelectedItem.Text.Trim();
        DataTable dtSalaryHead = objPayrollMgr.SelectTotalSalHeadWithSeq(0);
        DataTable dtHeadCount = objPayRptMgr.GetHeadCount();
        DataRow[] founHCRows = dtHeadCount.Select("DISPLAYTYPE='B'");
        inBenefitHeadCount = Convert.ToInt32(founHCRows[0]["HEADCOUNT"]);
        founHCRows = null;
        founHCRows = dtHeadCount.Select("DISPLAYTYPE='D'");
        inDeductCount = Convert.ToInt32(founHCRows[0]["HEADCOUNT"]);

        dtGrossSalHead = objPayrollMgr.SelectGrossSalHead(0);
        dtEmpPayroll = objPayRptMgr.GetEmpWiseSalaryDataForFiscalYear(ddlFiscalYear.SelectedValue.Trim(), ddlEmployee.SelectedValue.Trim());

        DataRow[] fEmpPayrollRow;
        this.InitializeSummaryTable(dtSalaryHead.Rows.Count + 8);

        int i = 2;
        int j = 1;
        //foreach (DataRow dEmpRow in dtEmpPayroll.Rows)
        for (int k = 1; k <= 12; k++)
        {
            dclEmpBenefits = 0;
            dclEmpDeduct = 0;
            dclTotalSalary = 0;
            fEmpPayrollRow = null;
            switch (k)
            {
                case 1:
                    fEmpPayrollRow = dtEmpPayroll.Select("VMONTH = " + 7);
                    break;
                case 2:
                    fEmpPayrollRow = dtEmpPayroll.Select("VMONTH = " + 8);
                    break;
                case 3:
                    fEmpPayrollRow = dtEmpPayroll.Select("VMONTH = " + 9);
                    break;
                case 4:
                    fEmpPayrollRow = dtEmpPayroll.Select("VMONTH = " + 10);
                    break;
                case 5:
                    fEmpPayrollRow = dtEmpPayroll.Select("VMONTH = " + 11);
                    break;
                case 6:
                    fEmpPayrollRow = dtEmpPayroll.Select("VMONTH = " + 12);
                    break;
                case 7:
                    fEmpPayrollRow = dtEmpPayroll.Select("VMONTH = " + 1);
                    break;
                case 8:
                    fEmpPayrollRow = dtEmpPayroll.Select("VMONTH = " + 2);
                    break;
                case 9:
                    fEmpPayrollRow = dtEmpPayroll.Select("VMONTH = " + 3);
                    break;
                case 10:
                    fEmpPayrollRow = dtEmpPayroll.Select("VMONTH = " + 4);
                    break;
                case 11:
                    fEmpPayrollRow = dtEmpPayroll.Select("VMONTH = " + 5);
                    break;
                case 12:
                    fEmpPayrollRow = dtEmpPayroll.Select("VMONTH = " + 6);
                    break;
            }
            if (fEmpPayrollRow.Length > 0)
            {
                foreach (DataRow dEmpRow in fEmpPayrollRow)
                {
                    this.GetEmpBenefitsAmount(dtSalaryHead, dEmpRow["VMONTH"].ToString().Trim(), dEmpRow["GROSSAMNT"].ToString());
                    i = 2;
                    if (strVmonth == dEmpRow["VMONTH"].ToString().Trim())
                    {
                        continue;
                    }
                    DataRow nRow = dtPayrollSummary.NewRow();
                    nRow[0] = Convert.ToString(j);
                    nRow[1] = Common.ReturnFullMonthName(dEmpRow["VMONTH"].ToString().Trim()) + "-" + dEmpRow["VYEAR"].ToString().Trim();

                    foreach (DataRow dSalRow in dtSalaryHead.Rows)
                    {
                        if (i - 2 == dtGrossSalHead.Rows.Count)
                        {
                            nRow[i] = Common.RoundDecimal(dEmpRow["GROSSAMNT"].ToString(), 0);
                            i++;
                        }
                        //if ((i - 2) - dtGrossSalHead.Rows.Count == inBenefitHeadCount + 1)
                        //{
                        //    nRow[i] = dclEmpBenefits.ToString();
                        //    i++;
                        //}
                        if ((i - 2) - dtGrossSalHead.Rows.Count == inBenefitHeadCount + 1)
                        {
                            nRow[i] = dclTotalSalary.ToString();
                            i++;

                            dclSalHeadAmt = 0;
                            dclSalHeadAmt = this.GetSalHeadAmt(dEmpRow["VMONTH"].ToString().Trim(), dSalRow["SHEADID"].ToString().Trim());
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
                            dclSalHeadAmt = this.GetSalHeadAmt(dEmpRow["VMONTH"].ToString().Trim(), dSalRow["SHEADID"].ToString().Trim());
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
                    strVmonth = dEmpRow["VMONTH"].ToString().Trim();
                    j++;
                }
            }
            //else
            //{
            //    DataRow nRow = dtPayrollSummary.NewRow();
            //    nRow[0] = Convert.ToString(j);
            //    nRow[1] = dEmpRow["EMPID"].ToString().Trim();
            //    nRow[2] = dEmpRow["FULLNAME"].ToString().Trim();
            //    nRow[3] = dEmpRow["ACCLINE"].ToString().Trim();
            //    dtPayrollSummary.Rows.Add(nRow);
            //    dtPayrollSummary.AcceptChanges();
            //}
        }

        grPayroll.DataSource = dtPayrollSummary;
        grPayroll.DataBind();
        if (dtPayrollSummary.Rows.Count > 0)
        {
            this.FormatGridView(dtSalaryHead, inBenefitHeadCount);
            this.GenerateMovement();
            //this.GetSummaryTotal();

            //if (ddlGeneratefor.SelectedValue.Trim() == "E")
            lblGenerateFor.Text = "Payroll Movement For: " + ddlEmployee.SelectedItem.Text.Trim();
            lblPayrollMonth.Text = "Fiscal Year: " + ddlFiscalYear.SelectedItem.Text;
        }
        else
        {
            lblGenerateFor.Text = "";
            lblPayrollMonth.Text = "";
        }
    }

    protected void FormatGridView(DataTable dtSalaryHead, int inBenefitHeadCount)
    {
        int j = 2;
        string strRowIndx = "";
        grPayroll.HeaderRow.Cells[0].Text = "Sl";
        grPayroll.HeaderRow.Cells[1].Text = "Month";

        for (int i = 0; i < dtPayrollSummary.Columns.Count; i++)
        {
            if (j - 2 == dtGrossSalHead.Rows.Count)
            {
                grPayroll.HeaderRow.Cells[j].Text = "Gross Salary";
                strRowIndx = j.ToString();
                j++;
            }
            //if ((j - 2) - dtGrossSalHead.Rows.Count == inBenefitHeadCount + 1)
            //{
            //    grPayroll.HeaderRow.Cells[j].Text = "Total Benefits";
            //    strRowIndx = strRowIndx + "," + j.ToString();
            //    j++;
            //}
            if ((j - 2) - dtGrossSalHead.Rows.Count == inBenefitHeadCount + 1)
            {
                grPayroll.HeaderRow.Cells[j].Text = "Total Salary";
                strRowIndx = strRowIndx + "," + j.ToString();
                j++;

                if (i < dtSalaryHead.Rows.Count)
                {
                    grPayroll.HeaderRow.Cells[j].Text = dtSalaryHead.Rows[i]["SHORTNAME"].ToString();
                    //strRowIndx = strRowIndx + "," + j.ToString();
                    j++;
                }
            }
            else
            {
                if (i < dtSalaryHead.Rows.Count)
                {
                    grPayroll.HeaderRow.Cells[j].Text = dtSalaryHead.Rows[i]["SHORTNAME"].ToString();
                    //strRowIndx = strRowIndx + "," + j.ToString();
                    j++;
                }
            }
        }

        grPayroll.HeaderRow.Cells[j].Text = "Total Deduct";
        strRowIndx = strRowIndx + "," + j.ToString();
        j++;

        grPayroll.HeaderRow.Cells[j].Text = "Net Taka";
        strRowIndx = strRowIndx + "," + j.ToString();
        j++;

        grPayroll.HeaderRow.Cells[j].Text = "Net US$";
        strRowIndx = strRowIndx + "," + j.ToString();
        j++;

        grPayroll.HeaderRow.Cells[j].Text = "Comp. PF";
        strRowIndx = strRowIndx + "," + j.ToString();

        this.HighlightGridViewCells(strRowIndx);
    }

    protected void GenerateMovement()
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            if (gRow.DataItemIndex == 0)
            {
                gRow.BackColor = System.Drawing.Color.Orange;
            }
            else
            {
                for (int j = 2; j < dtPayrollSummary.Columns.Count; j++)
                {
                    gRow.Cells[j].Text = Convert.ToString(Common.RoundDecimal(gRow.Cells[j].Text, 0) - Common.RoundDecimal(grPayroll.Rows[0].Cells[j].Text.Trim(), 0));
                    if (gRow.Cells[j].Text == "0")
                        gRow.Cells[j].Text = "";
                }
            }
        }
    }

    protected void GetEmpBenefitsAmount(DataTable dtSalHead, string strVmonth, string strGrossSal)
    {
        dclTotalSalary = Convert.ToDecimal(strGrossSal);
        decimal dclSalHeadAmt = 0;
        foreach (DataRow dRow in dtSalHead.Rows)
        {
            switch (dRow["DISPLAYTYPE"].ToString())
            {
                case "B":
                    dclEmpBenefits = dclEmpBenefits + this.GetSalHeadAmt(strVmonth, dRow["SHEADID"].ToString());
                    break;
                case "D":
                    dclSalHeadAmt = this.GetSalHeadAmt(strVmonth, dRow["SHEADID"].ToString());

                    if (dclSalHeadAmt > 0)
                        dclSalHeadAmt = dclSalHeadAmt * -1;

                    dclEmpDeduct = dclEmpDeduct + dclSalHeadAmt;
                    break;
            }

            //PF
            if (dRow["ISPF"].ToString() == "Y")
            {
                dclEmpPF = this.GetSalHeadAmt(strVmonth, dRow["SHEADID"].ToString()) * -1;
            }
        }
        dclTotalSalary = dclTotalSalary + dclEmpBenefits;
        dclTotalSalary = Common.RoundDecimal(dclTotalSalary.ToString(), 0);
        dclEmpBenefits = Common.RoundDecimal(dclEmpBenefits.ToString(), 0);
        dclEmpDeduct = Common.RoundDecimal(dclEmpDeduct.ToString(), 0);
        dclEmpPF = Common.RoundDecimal(dclEmpPF.ToString(), 0);
    }

    protected void HighlightGridViewCells(string strIndx)
    {
        string[] strArr = strIndx.Split(',');
        int indx = 0;
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            foreach (string str in strArr)
            {
                indx = Convert.ToInt32(str);
                gRow.Cells[indx].Font.Bold = true;
            }
        }
    }

    protected void GetSummaryTotal()
    {
        int i = 0;
        decimal dclSumValue = 0;
        grPayroll.FooterRow.Cells[1].Text = "Total In Fiscal Year:  ";
        for (int j = 2; j < dtPayrollSummary.Columns.Count; j++)
        {
            dclSumValue = 0;
            i = 0;
            foreach (DataRow dRow in dtPayrollSummary.Rows)
            {
                dclSumValue = dclSumValue + Convert.ToDecimal(dRow[j].ToString());
                if (Convert.ToInt64(dRow[j].ToString()) == 0)
                {
                    grPayroll.Rows[i].Cells[j].Text = "";
                }
                grPayroll.Rows[i].Cells[j].HorizontalAlign = HorizontalAlign.Right;
                i++;
            }
            if (dclSumValue == 0)
                grPayroll.FooterRow.Cells[j].Text = "";
            else
                grPayroll.FooterRow.Cells[j].Text = dclSumValue.ToString();
            grPayroll.FooterRow.Cells[j].HorizontalAlign = HorizontalAlign.Right;
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

    protected decimal GetSalHeadAmt(string strVMonth, string strSHeadID)
    {
        decimal dclSalHeadAmt = 0;
        DataRow[] foundRows = dtEmpPayroll.Select("VMONTH=" + strVMonth + " AND SHEADID=" + strSHeadID);
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        // this.ValidateAndSave();
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=Payroll.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        sw = this.GetHeaderText();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        grPayroll.RenderControl(htw);
        //sw = this.GetFooterText(sw);
        Response.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    protected StringWriter GetHeaderText()
    {
        StringWriter sw = new StringWriter();
        sw.WriteLine("<table style=" + "\"width:100%;margin-top:10px;text-align:left;border-collapse:collapse;border:solid 1px white;\">");
        sw.WriteLine("<tr>");
        sw.WriteLine("<td style=" + "\"text-align:left;border:solid 1px white;font-size:20px;font-weight:bold;\">");
        sw.WriteLine("iDE ");
        sw.WriteLine("</td>");
        sw.WriteLine("</tr>");
        sw.WriteLine("<tr>");
        sw.WriteLine("<td style=" + "\"text-align:right;border:solid 1px white;font-size:14px\">");
        sw.WriteLine(lblGenerateFor.Text.ToString());
        sw.WriteLine("</td>");
        sw.WriteLine("</tr>");
        sw.WriteLine("<tr>");
        sw.WriteLine("<td style=" + "\"text-align:right;border:solid 1px white;font-size:14px\">");
        sw.WriteLine(lblPayrollMonth.Text.ToString());
        sw.WriteLine("</td>");
        sw.WriteLine("</tr>");
        sw.WriteLine("</table>");
        return sw;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the
        // specified ASP.NET server control at run time.
    }

    protected void lnkBtnPayrollReportWithOffice_Click(object sender, EventArgs e)
    {
        Response.Redirect("PayrollReportForExcel.aspx");
    }
}
