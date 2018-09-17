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
using cashword.BLL;

public partial class Payroll_Payroll_ITDepositDetailsReport : System.Web.UI.Page
{
    MasterTablesManager objMastMg = new MasterTablesManager();
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    PayrollReportManager objPayRptMgr = new PayrollReportManager();
    MailManagerSmtpClient objMail = new MailManagerSmtpClient();
    Payroll_ITDepositRecords objITMgr = new Payroll_ITDepositRecords();
    clscashword objCashWord = new clscashword();

    DataTable dtReport;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Common.FillMonthList(ddlMonth);
            // Common.FillYearList(5, ddlYear);
            // ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            // ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            Common.FillDropDownList(objMastMg.SelectSalaryLocation(0), ddlLocation, "SalLocName", "SalLocID", true, "All");
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0, "F"), ddlFinYear, "FISCALYRTITLE", "FISCALYRID", false);
            Common.FillDropDownList(objMastMg.SelectEmpGroup(0), ddlGroup, "GrpName", "EmpGrpID", false);
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
        if (ddlGroup.SelectedValue.Trim() == "A")
            if (ddlLocation.SelectedValue.Trim() != "-1")
                strGenFor = "O";
            else
            {
                //lblMsg.Text = "No value is selected. Please select either DOS or Office to generate the report.";
                //return;
                strGenFor = "A";
            }
        else
            strGenFor = "D";


        DataTable dtEmp = new DataTable();
        DataTable dtDate = new DataTable();
        DataTable dtRecords = new DataTable();

        if (strGenFor != "A")
        {
            dtEmp = objITMgr.GetDistinctEmpoyeeData("-1", ddlLocation.SelectedValue.Trim(), ddlFinYear.SelectedValue.Trim(), strGenFor);
            dtDate = objITMgr.GetDistinctDate("-1", ddlLocation.SelectedValue.Trim(), ddlFinYear.SelectedValue.Trim(), strGenFor);
            dtRecords = objITMgr.GetDetailsData("-1", ddlLocation.SelectedValue.Trim(), ddlFinYear.SelectedValue.Trim(), strGenFor);
        }
        else
        {
            dtEmp = objITMgr.GetDistinctEmpoyeeDataAll(ddlFinYear.SelectedValue.Trim());
            dtDate = objITMgr.GetDistinctDateAll(ddlFinYear.SelectedValue.Trim());
            dtRecords = objITMgr.GetDetailsDataAll(ddlFinYear.SelectedValue.Trim());
        }

        // Payroll data
        DataTable dtPayrollDet = objPayRptMgr.GetPayrollDataForITStatement(ddlFinYear.SelectedValue.Trim());
        DataTable dtBenefitsHead = objPayrollMgr.GetSalaryBenefitsHead();
        DataRow[] fPayrollDet;

        lblFiscalYear.Text = ddlFinYear.SelectedItem.Text.Trim();
        // Data Filling for Report
        if (dtDate.Rows.Count == 0)
        {
            lblMsg.Text = "No deposited reocrds found. Please use IT deposit Records screen to input the data.";
            grEmployee.DataSource = null;
            grEmployee.DataBind();
            return;
        }
        this.IniReportDataTable(dtDate.Rows.Count + 3 + dtBenefitsHead.Rows.Count + 3 + 4);
        int inSL = 1;
        int i = 2;
        decimal decTotal = 0;
        decimal decBfTotal = 0;
        foreach (DataRow dEmpRow in dtEmp.Rows)
        {
            DataRow nRow = dtReport.NewRow();
            nRow[0] = inSL.ToString();
            nRow[1] = dEmpRow["EMPID"].ToString().Trim();
            nRow[2] = dEmpRow["FULLNAME"].ToString().Trim();
            nRow[3] = dEmpRow["JobTitleName"].ToString().Trim();
            nRow[4] = dEmpRow["PostingDivName"].ToString().Trim();
            nRow[5] = Common.SetDate(dEmpRow["JOININGDATE"].ToString().Trim());
            i = 6;
            decBfTotal = 0;
            foreach (DataRow dBfRows in dtBenefitsHead.Rows)
            {
                fPayrollDet = dtPayrollDet.Select("EMPID='" + dEmpRow["EMPID"].ToString().Trim() + "' AND SHEADID=" + dBfRows["SHEADID"].ToString().Trim());
                if (fPayrollDet.Length > 0)
                {
                    nRow[i] = Common.RoundDecimal(fPayrollDet[0]["PAYAMT"].ToString().Trim(), 0).ToString();
                    decBfTotal = decBfTotal + Common.RoundDecimal(fPayrollDet[0]["PAYAMT"].ToString().Trim(), 0);
                }
                else
                {
                    nRow[i] = "0";
                }

                i++;

                fPayrollDet = null;
            }
            nRow[i] = decBfTotal.ToString();
            i++;
            nRow[i] = objCashWord.getCashWord(decBfTotal.ToString() + ".00");
            i++;

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
            i++;
            nRow[i] = objCashWord.getCashWord(decTotal.ToString() + ".00");

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
            grEmployee.HeaderRow.Cells[1].Text = "Employee ID";
            grEmployee.HeaderRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            grEmployee.HeaderRow.Cells[2].Text = "Employee Name";
            grEmployee.HeaderRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;
            grEmployee.HeaderRow.Cells[3].Text = "Designation";
            grEmployee.HeaderRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
            grEmployee.HeaderRow.Cells[4].Text = "Office";
            grEmployee.HeaderRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
            grEmployee.HeaderRow.Cells[5].Text = "Joining Date";
            grEmployee.HeaderRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;

            int j = 6;
            foreach (DataRow dBfRows in dtBenefitsHead.Rows)
            {
                grEmployee.HeaderRow.Cells[j].Text = dBfRows["HeadName"].ToString();
                grEmployee.HeaderRow.Cells[j].HorizontalAlign = HorizontalAlign.Right;
                j++;
            }

            grEmployee.HeaderRow.Cells[j].Text = "Total Remuneration ";
            grEmployee.HeaderRow.Cells[j].HorizontalAlign = HorizontalAlign.Right;
            j++;
            grEmployee.HeaderRow.Cells[j].Text = "In Word";
            grEmployee.HeaderRow.Cells[j].HorizontalAlign = HorizontalAlign.Left;
            j++;

            for (i = j; i < dtReport.Columns.Count - 2; i++)
            {
                grEmployee.HeaderRow.Cells[i].Text = Common.DisplayDate(dtDate.Rows[i - j]["CHALLANDATE"].ToString().Trim()) + "<br><br>" + dtDate.Rows[i - j]["CHALLANNO"].ToString().Trim();
                grEmployee.HeaderRow.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            }
            grEmployee.HeaderRow.Cells[dtReport.Columns.Count - 2].Text = "Total Tax";
            grEmployee.HeaderRow.Cells[dtReport.Columns.Count - 2].HorizontalAlign = HorizontalAlign.Right;
            grEmployee.HeaderRow.Cells[dtReport.Columns.Count - 2].VerticalAlign = VerticalAlign.Top;
            grEmployee.HeaderRow.Cells[dtReport.Columns.Count - 1].Text = "In Word";
            grEmployee.HeaderRow.Cells[dtReport.Columns.Count - 1].HorizontalAlign = HorizontalAlign.Left;
            grEmployee.HeaderRow.Cells[dtReport.Columns.Count - 1].VerticalAlign = VerticalAlign.Top;
        }

        //// Summary Total
        //decimal dclFooterTotal = 0;
        //int j = 0;
        //grEmployee.FooterRow.Cells[1].Text = "Total :";
        //grEmployee.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
        //for (i = 2; i < dtReport.Columns.Count; i++)
        //{
        //    dclFooterTotal = 0;
        //    for (j = 0; j < grEmployee.Rows.Count;j++ )
        //    {
        //        dclFooterTotal = dclFooterTotal + Convert.ToDecimal(grEmployee.Rows[j].Cells[i].Text.Trim());
        //        grEmployee.Rows[j].Cells[i].HorizontalAlign = HorizontalAlign.Right;
        //        if (Convert.ToDecimal(grEmployee.Rows[j].Cells[i].Text.Trim()) == 0)
        //            grEmployee.Rows[j].Cells[i].Text = "-";
        //    }
        //    grEmployee.FooterRow.Cells[i].Text = dclFooterTotal.ToString();
        //    grEmployee.FooterRow.Cells[i].HorizontalAlign = HorizontalAlign.Right;
        //}
    }

    protected void btnGetEmployee_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        this.OpenRecord();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=ITReport.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        //sw = this.GetHeaderText();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        grEmployee.RenderControl(htw);
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
        sw.WriteLine("Detail Income tax Deduction at Source from Staff Salary and Deposited to Govt. Treasury for " + lblFiscalYear.Text);
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
}
