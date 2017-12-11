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
using System.Net;
using System.IO;
public partial class Payroll_Loan_PFInterest : System.Web.UI.Page
{
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    Payroll_PFManager objPFMgr = new Payroll_PFManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.Items.Add("Nil");
            ddlYear.Items.Add("Nil");
            ddlMonth.SelectedValue = "Nil";
            ddlYear.SelectedValue = "Nil";
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0, "P"), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);
        }
    }


    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        string strMonth = "";
        string strYear = "";
        string strFiscalYear = "";
        string strEmpID = "";
        strMonth = ddlMonth.SelectedItem.Text == "Nil" ? "0" : ddlMonth.SelectedValue.Trim();
        strYear = ddlYear.SelectedItem.Text == "Nil" ? "0" : ddlYear.SelectedValue.Trim();
        strFiscalYear = ddlFiscalYear.SelectedItem.Text == "Nil" ? "0" : ddlFiscalYear.SelectedValue.Trim();
        strEmpID = txtEmpID.Text.Trim() == "" ? "" : txtEmpID.Text.Trim();
        if (strMonth == "0")
            lblPeriod.Text = ddlFiscalYear.SelectedItem.Text;
        else
            lblPeriod.Text = ddlMonth.SelectedItem.Text + " " + ddlFiscalYear.SelectedItem.Text;

        lblPrintDate.Text = Common.DisplayDateTime(DateTime.Now.ToString());

        //rptPFLedger.DataSource = objPFMgr.GetPFLedgerData(strFiscalYear, strMonth, strYear, strEmpID);
        //rptPFLedger.DataBind();
        //rptPFLedgerSummary.DataSource = objPFMgr.GetPFLedgerSummaryData(strFiscalYear, strMonth, strYear, strEmpID);
        //rptPFLedgerSummary.DataBind();
        DataTable dtLedger = objPFMgr.GetPFLedgerDataForInterest(ddlFiscalYear.SelectedValue.Trim(), ddlMonth.SelectedValue.Trim(),txtEmpID.Text.Trim());
        grExport.Visible = true;
        //dtLedger.Columns.Add("MonthofInterest");
        //dtLedger.AcceptChanges();
        grExport.DataSource = dtLedger;
        grExport.DataBind();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=PFLedger.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        grExport.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.Flush();
        Response.End();
        grExport.DataSource = null;
        grExport.DataBind();
        grExport.Visible = true;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

        // Confirms that an HtmlForm control is rendered for the
        // specified ASP.NET server control at run time.

    }

    protected void CalCulateInterest(bool IsYearly)
    {
        DateTime dtStartDate = new DateTime();
        DateTime dtEndDate = new DateTime();
        DateTime dtFiscalStartDate = new DateTime();
        int inConfMonth = 0;
        decimal decCurrInterest = 0;
        decimal decBalance = 0;
        dtEndDate = Convert.ToDateTime(ddlYear.SelectedValue.Trim() + "-" + ddlMonth.SelectedValue.Trim() + "-" + Common.GetMonthDay(ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim()));
        int inYear = 0;
        if (Convert.ToInt32(ddlMonth.SelectedValue.Trim()) >= 1 && Convert.ToInt32(ddlMonth.SelectedValue.Trim()) <= 6)
            inYear = Convert.ToInt32(ddlYear.SelectedValue.Trim()) - 1;
        else
            inYear = Convert.ToInt32(ddlYear.SelectedValue.Trim());

        dtFiscalStartDate = Convert.ToDateTime(inYear.ToString() + "-07-01");
        foreach (GridViewRow gRow in grExport.Rows)
        {
            if (gRow.Cells[2].Text.Trim() == "4249")
                decBalance = 0;

            decBalance = 0;
            decCurrInterest = 0;
            //if (Common.RoundDecimal(gRow.Cells[23].Text.Trim(), 0) == 0)
            //{
            //    continue;
            //}
            if (Common.CheckNullString(gRow.Cells[6].Text.Trim()) != "")
            {
                dtStartDate = Convert.ToDateTime(gRow.Cells[6].Text.Trim());
                if (dtStartDate <= dtFiscalStartDate)
                {
                    inConfMonth = 12 * (dtEndDate.Year - dtFiscalStartDate.Year) + (dtEndDate.Month - dtFiscalStartDate.Month);
                    inConfMonth = inConfMonth + 1;
                }
                else
                {
                    inConfMonth = 12 * (dtEndDate.Year - dtStartDate.Year) + (dtEndDate.Month - dtStartDate.Month);
                    if (dtStartDate.Day == 1)
                    {
                        inConfMonth = inConfMonth + 1;
                    }
                }
                if (inConfMonth >= 12)
                {
                    //decBalance = Common.RoundDecimal(gRow.Cells[23].Text.Trim(), 0);
                    //decCurrInterest = decBalance * Common.RoundDecimal(txtPFRate.Text.Trim(), 0) / 100;
                    decBalance = Common.RoundDecimal(gRow.Cells[23].Text.Trim(), 0) * Convert.ToDecimal(txtPFRate.Text.Trim());
                    decBalance = decBalance / 100;
                    decCurrInterest = decBalance;
                    decCurrInterest = Math.Round(decCurrInterest, 0);
                    gRow.Cells[14].Text = decCurrInterest.ToString();
                    gRow.Cells[27].Text = "12";
                    if (IsYearly == true)
                    {
                        gRow.Cells[20].Text = Convert.ToString(Common.RoundDecimal(gRow.Cells[20].Text.Trim(), 0) + decCurrInterest);
                        gRow.Cells[21].Text = Convert.ToString(Common.RoundDecimal(gRow.Cells[21].Text.Trim(), 0) + decCurrInterest);
                        gRow.Cells[23].Text = Convert.ToString(Common.RoundDecimal(gRow.Cells[23].Text.Trim(), 0) + decCurrInterest);
                    }

                }
                else if (inConfMonth >= 6 && inConfMonth < 12)
                {
                    //decBalance = Common.RoundDecimal(gRow.Cells[23].Text.Trim(), 0) / 12 * inConfMonth;
                    //decCurrInterest = decBalance * Common.RoundDecimal(txtPFRate.Text.Trim(), 0) / 100;
                    //decCurrInterest = Math.Round(decCurrInterest, 0);
                    //gRow.Cells[14].Text = decCurrInterest.ToString();
                    //gRow.Cells[27].Text = inConfMonth.ToString();

                    decBalance = Common.RoundDecimal(gRow.Cells[23].Text.Trim(), 0) * Convert.ToDecimal(txtPFRate.Text.Trim());
                    decBalance = decBalance / 100;
                    decCurrInterest = (decBalance / 12) * inConfMonth;
                    decCurrInterest = Math.Round(decCurrInterest, 0);
                    gRow.Cells[14].Text = decCurrInterest.ToString();
                    gRow.Cells[27].Text = inConfMonth.ToString();
                    if (IsYearly == true)
                    {
                        gRow.Cells[20].Text = Convert.ToString(Common.RoundDecimal(gRow.Cells[20].Text.Trim(), 0) + decCurrInterest);
                        gRow.Cells[21].Text = Convert.ToString(Common.RoundDecimal(gRow.Cells[21].Text.Trim(), 0) + decCurrInterest);
                        gRow.Cells[23].Text = Convert.ToString(Common.RoundDecimal(gRow.Cells[23].Text.Trim(), 0) + decCurrInterest);
                    }
                }
                else
                {
                    gRow.Cells[27].Text = "0";
                    continue;
                }
            }

            else
            {
                gRow.Cells[27].Text = "0";
                continue;
            }
        }
    }
    protected void btnYearlyInterest_Click1(object sender, EventArgs e)
    {
        this.CalCulateInterest(true);
        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (grExport.Rows.Count > 0)
        {
            objPFMgr.UpdatePFInterest(grExport, Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()),txtPFRate.Text.Trim());
            lblMsg.Text = "Interest Updated Successfully";
        }
    }
    protected void btnMidtermInterest_Click(object sender, EventArgs e)
    {
        this.CalCulateInterest(false);
    }
}
