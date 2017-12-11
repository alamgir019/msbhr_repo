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

public partial class Attendance_TimeSheetViewer : System.Web.UI.Page
{
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    PayrollReportManager objPayRptMgr = new PayrollReportManager();
    TimeSheetManager objTSM = new TimeSheetManager();
    DataTable dtReport;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList(objEmpMgr.SelectEmpNameWithID("A"), ddlEmployee, "EmpName", "EmpID", true, "All");
            Common.FillMonthList(ddlMonthFrom);
            Common.FillYearList(5, ddlYearFrom);

            Common.FillMonthList(ddlMonthTo);            
            Common.FillYearList(5, ddlYearTo);

            ddlMonthFrom.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYearFrom.SelectedValue = Convert.ToString(DateTime.Today.Year);

            ddlMonthTo.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYearTo.SelectedValue = Convert.ToString(DateTime.Today.Year);
        }
    }

    protected void btnPriview_Click(object sender, EventArgs e)
    {
        DataTable dtTS = objTSM.Get_SOF_SummaryData(ddlEmployee.SelectedValue.Trim(), ddlMonthFrom.SelectedValue.Trim(),
            ddlYearFrom.SelectedValue.Trim(), ddlMonthTo.SelectedValue.Trim(), ddlYearTo.SelectedValue.Trim());
        grReport.DataSource = dtTS;
        grReport.DataBind();

        foreach (GridViewRow gRow in grReport.Rows)
        {
            decimal total = 0;
            for (int i = 2; i < dtTS.Columns.Count; i++)
            {
                if (Common.CheckNullString(gRow.Cells[i].Text) != "")
                    total = total + Convert.ToDecimal(gRow.Cells[i].Text);
            }

            for (int i = 2; i < dtTS.Columns.Count; i++)
            {
                if (Common.CheckNullString(gRow.Cells[i].Text) != "")
                    gRow.Cells[i].Text = Convert.ToString(Math.Round((Convert.ToDecimal(gRow.Cells[i].Text) * 100 / total), 0)) + "%";
            }
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=TimeSheetSummary.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        sw = this.GetHeaderText();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        grReport.RenderControl(htw);
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
        sw.WriteLine("Marie Stopes ");
        sw.WriteLine("</td>");
        sw.WriteLine("</tr>");
        sw.WriteLine("<tr>");
        sw.WriteLine("<td style=" + "\"text-align:right;border:solid 1px white;font-size:14px\">");
        //sw.WriteLine(lblGenerateFor.Text.ToString());
        sw.WriteLine("</td>");
        sw.WriteLine("</tr>");
        sw.WriteLine("<tr>");
        sw.WriteLine("<td style=" + "\"text-align:right;border:solid 1px white;font-size:14px\">");
        //sw.WriteLine(lblPayrollMonth.Text.ToString());
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