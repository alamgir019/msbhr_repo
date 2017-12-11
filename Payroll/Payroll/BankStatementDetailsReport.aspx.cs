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
public partial class Payroll_Payroll_BankStatementDetailsReport : System.Web.UI.Page
{
    PayrollReportManager objPayRptMgr = new PayrollReportManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strParams = Request.QueryString["params"];
            string[] strVal = strParams.Split(',');

            grBankStatement.DataSource = objPayRptMgr.GetBankStatmentDetails(strVal[0], strVal[1], strVal[2], strVal[3], strVal[4]);
            grBankStatement.DataBind();

            int i = 1;
            foreach (GridViewRow gRow in grBankStatement.Rows)
            {
                gRow.Cells[0].Text = i.ToString();
                gRow.Cells[4].Text = gRow.Cells[4].Text.Trim() + " " + Common.ReturnFullMonthName(strVal[0].Trim()) + " " + strVal[1].Trim();
                i++;

                gRow.Cells[6].Text = Common.DisplayDate(gRow.Cells[6].Text);
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
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        grBankStatement.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the
        // specified ASP.NET server control at run time.
    }

    protected void btnExportToWord_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=BankStatement.doc";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-word";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        grBankStatement.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }
}
