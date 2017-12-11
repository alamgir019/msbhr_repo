using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class CrystalReports_Training_trainingMatrixRpt : System.Web.UI.Page
{
    ReportManager objReportMgr = new ReportManager();
    DataTable dtTrainingMatrix = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.GeneratePayrollReport();
    }

    protected void InitializeSummaryTable(DataTable dt)
    {
        int i = 0;
        dtTrainingMatrix = new DataTable();
        int inCol = dt.Columns.Count;
        foreach (DataColumn column in dt.Columns)
        {
            dtTrainingMatrix.Columns.Add(column.ColumnName);
        }
    }

    protected void GeneratePayrollReport()
    {
        DataTable dtMatrixHead = objReportMgr.GetTrainingMatrix();
        //int head = dtSalaryHead.Columns.Count;
        this.InitializeSummaryTable(dtMatrixHead);
        foreach (DataRow dRow in dtMatrixHead.Rows)
        {
            DataRow nRow = dtTrainingMatrix.NewRow();
            int i = 0;
            foreach (DataColumn column in dtMatrixHead.Columns)
            {
                nRow[i] = dRow[column.ColumnName].ToString();
                i++;
            }
            dtTrainingMatrix.Rows.Add(nRow);
            dtTrainingMatrix.AcceptChanges();
        }
        grPayroll.DataSource = dtTrainingMatrix;
        grPayroll.DataBind();
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=Matirx.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        sw = this.GetHeaderText();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        grPayroll.RenderControl(htw);
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
        sw.WriteLine("Training Matrix ");
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