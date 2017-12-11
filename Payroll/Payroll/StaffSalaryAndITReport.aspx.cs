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

public partial class Payroll_Payroll_StaffSalaryAndITReport : System.Web.UI.Page
{
    PayrollReportManager objPayRptMgr = new PayrollReportManager();
    DataTable dtReport;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strParams = Request.QueryString["params"];
            string[] strVal = strParams.Split(',');
            strVal[1] = strVal[1] == "-1" ? "0" : strVal[1];
            DataTable dtEmployee = new DataTable();
            DataTable dtSalaryItems = new DataTable();
            if (strVal[3].Trim() == "Nil")
            {
                dtEmployee = objPayRptMgr.GetFiscalYearWiseDistinctEmployee(strVal[0], strVal[1]);
                dtSalaryItems = objPayRptMgr.GetFiscalYearWiseSalaryItemData(strVal[0], strVal[1]);
            }
            else
            {
                dtEmployee = objPayRptMgr.GetMonthWiseDistinctEmployeeForStaffSalaryIT(strVal[0], strVal[1],strVal[3]);
                dtSalaryItems = objPayRptMgr.GetMonthWiseSalaryItemDataForStaffSalaryIT(strVal[0], strVal[1], strVal[3]);
            }
            DataTable dtReportSalHead = objPayRptMgr.SelectSalHeadWithSeqForReport(1);
            int inGrossSalIndx = this.GetGrossSalHeadIndex(dtReportSalHead);
            if (strVal[3].Trim() != "Nil")
                lblSubHead.Text = "Detail of Staff Salary and Income Tax for " + Common.ReturnFullMonthName(strVal[3]) + "/" + strVal[2];
            else
                lblSubHead.Text = "Detail of Staff Salary and Income Tax for " + strVal[2];

            if (dtReportSalHead.Rows.Count > 0)
            {
                this.IniReportDataTable(dtReportSalHead.Rows.Count + 2);
                this.GetReportData(dtEmployee, dtSalaryItems, inGrossSalIndx, dtReportSalHead);
                if (grReport.Rows.Count > 0)
                {
                    this.FormatGridView(dtReportSalHead, inGrossSalIndx);
                }
            }
        }
    }

    protected void IniReportDataTable(int inCol)
    {
        dtReport = new DataTable();
        int i = 0;
        for (i = 0; i < inCol; i++)
        {
            dtReport.Columns.Add(i.ToString());
        }
        dtReport.AcceptChanges();
    }

    protected int GetGrossSalHeadIndex(DataTable dtReportSalHead)
    {
        int inGrossSalIndx = 0;
        for (int i = 0; i < dtReportSalHead.Rows.Count; i++)
        {
            if (dtReportSalHead.Rows[i]["SHEADID"].ToString().Trim() == "99999")
            {
                inGrossSalIndx = i + 2;
            }
        }
        return inGrossSalIndx;
    }

    protected void GetReportData(DataTable dtEmployee, DataTable dtSalaryItems, int inGrossSalIndx, DataTable dtReportSalHead)
    {
        DataRow[] foundSalRows;
        int i = 0;
        int j = 2;
        foreach (DataRow dEmpRow in dtEmployee.Rows)
        {
            DataRow nRow = dtReport.NewRow();
            nRow[0] = dEmpRow["FULLNAME"].ToString().Trim();
            nRow[1] = dEmpRow["JobTitleName"].ToString().Trim();
            nRow[inGrossSalIndx] = dEmpRow["GROSSAMNT"].ToString().Trim();
            if (dEmpRow["EMPID"].ToString().Trim() == "PIB00237")
                i = 0;
            j = 2;
            foreach (DataRow dHRow in dtReportSalHead.Rows)
            {
                if (inGrossSalIndx != j)
                {
                    foundSalRows = dtSalaryItems.Select("EMPID='" + dEmpRow["EMPID"].ToString().Trim() + "' AND SHEADID=" + dHRow["SHEADID"].ToString().Trim());
                    if (foundSalRows.Length > 0)
                        nRow[j] = foundSalRows[0]["PAYAMT"].ToString().Trim();
                    else
                        nRow[j] = "0";
                }
                j++;
                foundSalRows = null;
            }
            dtReport.Rows.Add(nRow);
            dtReport.AcceptChanges();
        }
       

        if (dtReport.Rows.Count > 0)
        {
            grReport.DataSource = dtReport;
            grReport.DataBind();
        }
    }

    protected void FormatGridView(DataTable dtReportSalHead, int inGrossSalIndx)
    {
        decimal decSummTotal = 0;
        grReport.HeaderRow.Cells[0].Text = "Employee Name";
        grReport.HeaderRow.Cells[0].HorizontalAlign = HorizontalAlign.Left;
        grReport.HeaderRow.Cells[1].Text = "Designation";
        grReport.HeaderRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
        grReport.FooterRow.Cells[1].Text = "Total";
        grReport.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;

        for (int i = 2; i < dtReport.Columns.Count; i++)
        {
            if (i != inGrossSalIndx)
            {
                grReport.HeaderRow.Cells[i].Text = dtReportSalHead.Rows[i - 2]["HEADNAME"].ToString();
            }
            else
            {
                grReport.HeaderRow.Cells[i].Text = "Gross Salary";
            }
            grReport.HeaderRow.Cells[i].HorizontalAlign = HorizontalAlign.Right;

            // Align to Right and Calculate Summary of Column
            decSummTotal = 0;
            foreach (GridViewRow gRow in grReport.Rows)
            {
                gRow.Cells[i].HorizontalAlign = HorizontalAlign.Right;

                decSummTotal = decSummTotal + Common.RoundDecimal(gRow.Cells[i].Text.Trim(), 0);

                if (gRow.Cells[i].Text.Trim() == "0")
                    gRow.Cells[i].Text = "-";
                else
                    gRow.Cells[i].Text =  Common.RoundDecimal(gRow.Cells[i].Text.Trim(), 0).ToString();
            }

            // Footer Summary
            grReport.FooterRow.Cells[i].Text =  decSummTotal.ToString();
            grReport.FooterRow.Cells[i].HorizontalAlign = HorizontalAlign.Right;
        }
    }
}
