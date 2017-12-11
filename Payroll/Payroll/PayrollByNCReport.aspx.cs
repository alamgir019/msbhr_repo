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

public partial class Payroll_Payroll_PayrollByNCReport : System.Web.UI.Page
{
    PayrollReportManager objPayRptMgr = new PayrollReportManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strParams = Request.QueryString["params"];
            string[] strVal = strParams.Split(',');

            grReport.DataSource = objPayRptMgr.GetPayrollDataByNC(strVal[0], strVal[1], strVal[2], strVal[4], strVal[5]);
            grReport.DataBind();
            int i = 1;
            decimal decTotal1 = 0;
            decimal decTotal2 = 0;
            decimal decTotal3 = 0;
            lblBank.Text = strVal[3].Trim();
            if (strVal[5] != "0")
                lblSubHead.Text = "Salary For the Fiscal Year " + strVal[6];
            else if (strVal[4] == "S")
                lblSubHead.Text = "Salary For the month of " + Common.ReturnFullMonthName(strVal[0]) + "," + strVal[1];
            else if (strVal[4] == "B")
                lblSubHead.Text = "Festival Bonus " + Common.ReturnFullMonthName(strVal[0]) + "," + strVal[1];
            
            if (grReport.Rows.Count > 0)
            {
                this.GetRowTotal();
            }

            foreach (GridViewRow gRow in grReport.Rows)
            {
                decTotal1 = decTotal1 + Common.RoundDecimal(gRow.Cells[2].Text.Trim(), 0);
                decTotal2 = decTotal2 + Common.RoundDecimal(gRow.Cells[3].Text.Trim(), 0);
                decTotal3 = decTotal3 + Common.RoundDecimal(gRow.Cells[4].Text.Trim(), 0);

                gRow.Cells[0].Text = i.ToString();
                gRow.Cells[1].Text = gRow.Cells[1].Text + " Total";
                i++;
            }
            if (grReport.Rows.Count > 0)
            {
                grReport.FooterRow.Cells[1].Text = "Grand Total";
                grReport.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                grReport.FooterRow.Cells[2].Text = decTotal1.ToString();
                grReport.FooterRow.Cells[3].Text = decTotal2.ToString();
                grReport.FooterRow.Cells[4].Text = decTotal3.ToString();
                grReport.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                grReport.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                grReport.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            }
        }
    }

    protected void GetRowTotal()
    {
        decimal decTotal = 0;
        foreach (GridViewRow gRow in grReport.Rows)
        {
            decTotal = 0;
            for (int i = 0; i < grReport.Columns.Count-1; i++)
            {
                if (i > 1)
                {
                    decTotal = decTotal+Common.RoundDecimal(gRow.Cells[i].Text.Trim(), 0);
                }
            }
            gRow.Cells[grReport.Columns.Count - 1].Text = decTotal.ToString();
        }
    }

}
