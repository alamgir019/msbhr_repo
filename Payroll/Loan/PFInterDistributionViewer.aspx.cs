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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing.Printing;
using System.IO;
using System.Net;


public partial class Payroll_Loan_PFInterDistributionViewer : System.Web.UI.Page
{
    ReportManager rptManager = new ReportManager();
    //private ReportDocument ReportDoc;
    //private PrintDocument printDoc = new PrintDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strParams = Request.QueryString["params"];
            string[] strVal = strParams.Split(',');
            this.GenerateReport( strVal[0], strVal[1]);
            lblFisYr.Text = strVal[1].ToString(); 
        }
    }

    protected void GenerateReport(string strFinYear, string strFinYearTitle)
    {
        DataTable MyDataTable = rptManager.GetPFInterDistribution(strFinYear);
        if (MyDataTable.Rows.Count > 0)
        {
            grPFInterDis.DataSource = MyDataTable;
            grPFInterDis.DataBind();
            this.CalculateData();
        }
    }

    protected void CalculateData()
    {
        decimal dclNewRate = 0;
        int i = 1;

        foreach (GridViewRow gRow in grPFInterDis.Rows)
        {
            gRow.Cells[0].Text = i.ToString(); 
            // New Rate
            //dclNewRate = 0;            
            //dclNewRate = Common.RoundDecimal(gRow.Cells[10].Text.Trim(), 10) * Common.RoundDecimal(gRow.Cells[9].Text.Trim(), 10);
            //if (dclNewRate > 0)
            //    dclNewRate = dclNewRate / 100;
            //gRow.Cells[11].Text = dclNewRate.ToString();
            i++;
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=PFInterDistribution.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        grPFInterDis.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the
        // specified ASP.NET server control at run time.
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
}
