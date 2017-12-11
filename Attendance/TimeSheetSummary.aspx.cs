using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Attendance_TimeSheetSummary : System.Web.UI.Page
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
            Common.FillYearList(5, ddlYear);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
        }
    }

    protected void btnPriview_Click(object sender, EventArgs e)
    {
        DataTable dtHR = objTSM.Get_SOF_Wise_HR(ddlEmployee.SelectedValue.Trim(), ddlYear.SelectedValue.Trim());
        DataTable dtTS = objTSM.Get_MonthWiseTimeSheetHour(ddlEmployee.SelectedValue.Trim(), ddlYear.SelectedValue.Trim());
        grReport.DataSource = dtTS;
        grReport.DataBind();

        //Fill SOF Code wise Hour
        this.FillHour(dtHR);
    }

    private void FillHour(DataTable dtHR)
    {
        DataRow[] foundHR;
        foreach (GridViewRow gRow in grReport.Rows)
        {
            foundHR = dtHR.Select("EmpId='" + gRow.Cells[0].Text.Trim() + "' AND SalSourceCode='" + gRow.Cells[2].Text.Trim() + "'");
            if (foundHR.Length > 0)
            {
               // int j = 3;
                int j = 2;
                for (int i = 0; i < 24; i += 2)
                {
                    gRow.Cells[i + 4].Text = foundHR[0][j].ToString();
                    j++;
                }
            }
        }
    }

    protected void grReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int i = 0; i < 24; i++)
            {
                e.Row.Cells[i + 3].Text = GetColumnName(e.Row.Cells[i + 3].Text.Trim());
            }
        }
    }

    private string GetColumnName(string col)
    {
        string strYear = ddlYear.SelectedItem.ToString().Substring(2, 2);
        string strCol = "";
        switch (col)
        {
            case "1":
            case "2":
            case "3":
            case "4":
            case "5":
            case "6":
            case "7":
            case "8":
            case "9":
            case "10":
            case "11":
            case "12":
                strCol = "%";
                break;
            case "13":
                strCol = "Jan'" + strYear;
                break;
            case "14":
                strCol = "Feb'" + strYear;
                break;
            case "15":
                strCol = "Mar'" + strYear;
                break;
            case "16":
                strCol = "Apr'" + strYear;
                break;
            case "17":
                strCol = "May'" + strYear;
                break;
            case "18":
                strCol = "Jun'" + strYear;
                break;
            case "19":
                strCol = "Jul'" + strYear;
                break;
            case "20":
                strCol = "Aug'" + strYear;
                break;
            case "21":
                strCol = "Sep'" + strYear;
                break;
            case "22":
                strCol = "Oct'" + strYear;
                break;
            case "23":
                strCol = "Nov'" + strYear;
                break;
            case "24":
                strCol = "Dec'" + strYear;
                break;
        }
        return strCol;
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