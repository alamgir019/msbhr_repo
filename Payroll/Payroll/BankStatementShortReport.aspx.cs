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

public partial class Payroll_Payroll_BankStatementShortReport : System.Web.UI.Page
{
    PayrollReportManager objPayRptMgr = new PayrollReportManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strParams = Request.QueryString["params"];
            string[] strVal = strParams.Split(',');

            grBankStatement.DataSource = objPayRptMgr.GetBankStatmentDetails(strVal[0], strVal[1], strVal[2],strVal[3], strVal[4]);
            grBankStatement.DataBind();
            int i = 1;
            decimal decTotal = 0;
            lblBank.Text = strVal[3].Trim();
            lblPrintDate.Text = DateTime.Today.ToLongDateString();
            foreach (GridViewRow gRow in grBankStatement.Rows)
            {
                decTotal = decTotal + Common.RoundDecimal(gRow.Cells[5].Text.Trim(), 2);
                gRow.Cells[6].Text = gRow.Cells[6].Text.Trim() + " " + Common.ReturnFullMonthName(strVal[0].Trim()) + " " + strVal[1].Trim();
                gRow.Cells[7].Text = Common.DisplayDate(gRow.Cells[7].Text);
                //gRow.Cells[0].Text = i.ToString();
                //i++;
            }
            if (grBankStatement.Rows.Count > 0)
            {
                grBankStatement.FooterRow.Cells[1].Text = "Total";
                grBankStatement.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                grBankStatement.FooterRow.Cells[5].Text = decTotal.ToString();
                grBankStatement.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            }
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=BankStatement.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        sw = this.GetHeaderText();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        grBankStatement.RenderControl(htw);
        sw = this.GetFooterText(sw);
        Response.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    protected void btnExportToWord_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=BankStatement.doc";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-word";
        StringWriter sw = new StringWriter();
        sw = this.GetHeaderText();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        grBankStatement.RenderControl(htw);
        sw = this.GetFooterText(sw);
        Response.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    protected StringWriter GetHeaderText()
    {
        StringWriter sw = new StringWriter();
        sw.WriteLine("<table style=" + "\"width:100%;margin-top:10px;text-align:left;font-weight:bold;border-collapse:collapse;border:solid 1px white;\">");
        sw.WriteLine("<tr>");
        sw.WriteLine("<td style=" + "\"width:70%;text-align:left;border:solid 1px white;\">");
        sw.WriteLine(lblBank.Text.ToString());
        sw.WriteLine("</td>");
        sw.WriteLine("<td style=" + "\"width:30%;text-align:right;border:solid 1px white;\">");
        sw.WriteLine(lblPrintDate.Text.ToString());
        sw.WriteLine("</td>");
        sw.WriteLine("</tr>");
        sw.WriteLine("</table>");
        return sw;
    }

    protected StringWriter GetFooterText(StringWriter sw)
    {
        sw.WriteLine("<table style=" + "\"width:100%;margin-top:100px;text-align:left;font-weight:bold;border-collapse:collapse;border:solid 1px white;\">");
        sw.WriteLine("</table>");
        sw.WriteLine("<table style=" + "\"width:100%;text-align:left;font-weight:bold;border-collapse:collapse;\">");
        sw.WriteLine("<tr>");
        sw.WriteLine("<td style=" + "\"width:50%;text-align:left;border:solid 1px white;\">");
        sw.WriteLine("<table style=" + "\"width:80%;text-align:left;font-weight:bold;border-collapse:collapse;border:solid 1px white;\">");
        sw.WriteLine("<tr>");
        sw.WriteLine("<td style=" + "\"width:100%;text-align:left;border-bottom:solid 1px white;border-left:solid 1px white;border-right:solid 1px white;border-top:solid 2px black;\">");
        sw.WriteLine("Authorized Signature");
        sw.WriteLine("</td>");
        sw.WriteLine("</tr>");
        sw.WriteLine("</table>");
        sw.WriteLine("</td>");
        sw.WriteLine("<td style=" + "\"width:50%;text-align:right;border:solid 1px white;\">");
        sw.WriteLine("<table style=" + "\"width:80%;text-align:left;font-weight:bold;border-collapse:collapse;border:solid 1px white;\">");
        sw.WriteLine("<tr>");
        sw.WriteLine("<td style=" + "\"width:100%;text-align:right;border-bottom:solid 1px white;border-left:solid 1px white;border-right:solid 1px white;border-top:solid 2px black;\">");
        sw.WriteLine("Authorized Signature");
        sw.WriteLine("</td>");
        sw.WriteLine("</tr>");
        sw.WriteLine("</table>");
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
