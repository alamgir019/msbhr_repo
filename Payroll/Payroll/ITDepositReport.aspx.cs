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
using System.IO;

public partial class Payroll_Payroll_ITDepositReport : System.Web.UI.Page
{
    MasterTablesManager objMastMg = new MasterTablesManager();
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    PayrollReportManager objPayRptMgr = new PayrollReportManager();
    MailManagerSmtpClient objMail = new MailManagerSmtpClient();
    Payroll_ITDepositRecords objITMgr = new Payroll_ITDepositRecords();

    DataTable dtReport;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Common.FillMonthList(ddlMonth);
            // Common.FillYearList(5, ddlYear);
            // ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            // ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            Common.FillDropDownList(objMastMg.SelectDivision(0), ddlCompany,"DivisionName","DivisionId",true ,"All");
            Common.FillDropDownList(objMastMg.SelectClinic("Y"), ddlLocation, "ClinicName", "ClinicId", true, "All");            
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0, "T"), ddlFinYear, "FISCALYRTITLE", "FISCALYRID", false);
        }
    }

    protected void IniReportDataTable(int inCol)
    {
        dtReport = new DataTable();
        for (int i = 0; i < inCol; i++)
        {
            dtReport.Columns.Add(i.ToString());
        }
        dtReport.AcceptChanges();
    }

    protected void OpenRecord()
    {
        string strGenFor = "";        

        DataTable dtEmp = objITMgr.GetDistinctEmpoyeeData(ddlCompany.SelectedValue.ToString(), ddlLocation.SelectedValue.Trim(), ddlFinYear.SelectedValue.Trim(), strGenFor);
        DataTable dtDate = objITMgr.GetDistinctDate(ddlCompany.SelectedValue.ToString(), ddlLocation.SelectedValue.Trim(), ddlFinYear.SelectedValue.Trim(), strGenFor);
        DataTable dtRecords = objITMgr.GetDetailsData(ddlCompany.SelectedValue.ToString(),    ddlLocation.SelectedValue.Trim(), ddlFinYear.SelectedValue.Trim(), strGenFor);

        lblFiscalYear.Text = ddlFinYear.SelectedItem.Text.Trim();
        // Data Filling for Report
        if (dtDate.Rows.Count == 0)
        {
            lblMsg.Text = "No deposited reocrds found. Please use IT deposit Records screen to input the data.";
            grEmployee.DataSource = null;
            grEmployee.DataBind();
            return;
        }
        this.IniReportDataTable(dtDate.Rows.Count + 3);
        int inSL = 1;
        int i = 2;
        decimal decTotal = 0;
        foreach (DataRow dEmpRow in dtEmp.Rows)
        {
            DataRow nRow = dtReport.NewRow();
            nRow[0] = inSL.ToString();
            nRow[1] = dEmpRow["FULLNAME"].ToString().Trim();
            i = 2;
            decTotal = 0;
            foreach (DataRow dDateRow in dtDate.Rows)
            {
                DataRow[] foundRows = dtRecords.Select("EMPID='" + dEmpRow["EMPID"].ToString().Trim() + "' AND CHALLANDATE='" + dDateRow["CHALLANDATE"].ToString().Trim() + "'");
                if (foundRows.Length > 0)
                {
                    nRow[i] = foundRows[0]["PAYAMT"].ToString().Trim();
                    decTotal = decTotal + Convert.ToDecimal(foundRows[0]["PAYAMT"].ToString().Trim());
                }
                else
                    nRow[i] = "0";
                i++;
            }
            inSL++;
            nRow[i] = decTotal.ToString();

            dtReport.Rows.Add(nRow);
            dtReport.AcceptChanges();
        }

        grEmployee.DataSource = dtReport;
        grEmployee.DataBind();

        if (grEmployee.Rows.Count > 0)
        {
            grEmployee.HeaderRow.Cells[0].Text = "SL#";
            grEmployee.HeaderRow.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            grEmployee.HeaderRow.Cells[0].VerticalAlign = VerticalAlign.Top;
            grEmployee.HeaderRow.Cells[1].Text = "IT Challan Date and No" + "<br><br>" + "Employee Name";
            grEmployee.HeaderRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            for (i = 2; i < dtReport.Columns.Count - 1; i++)
            {
                grEmployee.HeaderRow.Cells[i].Text = Common.DisplayDate(dtDate.Rows[i - 2]["CHALLANDATE"].ToString().Trim()) + "<br><br>" + dtDate.Rows[i - 2]["CHALLANNO"].ToString().Trim();
                grEmployee.HeaderRow.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            }
            grEmployee.HeaderRow.Cells[dtReport.Columns.Count - 1].Text = "Total";
            grEmployee.HeaderRow.Cells[dtReport.Columns.Count - 1].HorizontalAlign = HorizontalAlign.Right;
            grEmployee.HeaderRow.Cells[dtReport.Columns.Count - 1].VerticalAlign = VerticalAlign.Top;
        }

        // Summary Total
        decimal dclFooterTotal = 0;
        int j = 0;
        grEmployee.FooterRow.Cells[1].Text = "Total :";
        grEmployee.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
        for (i = 2; i < dtReport.Columns.Count; i++)
        {
            dclFooterTotal = 0;
            for (j = 0; j < grEmployee.Rows.Count; j++)
            {
                dclFooterTotal = dclFooterTotal + Convert.ToDecimal(grEmployee.Rows[j].Cells[i].Text.Trim());
                grEmployee.Rows[j].Cells[i].HorizontalAlign = HorizontalAlign.Right;
                if (Convert.ToDecimal(grEmployee.Rows[j].Cells[i].Text.Trim()) == 0)
                    grEmployee.Rows[j].Cells[i].Text = "-";
            }
            grEmployee.FooterRow.Cells[i].Text = dclFooterTotal.ToString();
            grEmployee.FooterRow.Cells[i].HorizontalAlign = HorizontalAlign.Right;
        }
    }

    protected void btnGetEmployee_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        this.OpenRecord();
    }
}
