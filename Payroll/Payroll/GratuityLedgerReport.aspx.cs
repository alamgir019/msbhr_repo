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
using System.IO;
using System.Text;

public partial class Payroll_Payroll_GratuityLedgerReport : System.Web.UI.Page
{
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    Payroll_GratuityLedgerManager objGrMgr = new Payroll_GratuityLedgerManager();
    dsPayroll_Loan objDs = new dsPayroll_Loan();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.Items.Add("Nil");
            ddlYear.Items.Add("Nil");
            ddlMonth.SelectedValue = DateTime.Today.Month.ToString();
            ddlYear.SelectedValue = DateTime.Today.Year.ToString();
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0,"F"), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);
        }
    }


    protected void GetLedgerData(bool isHTMLFormat)
    {
        string strMonth = "";
        string strYear = "";
        string strFiscalYear = "";
        string strEmpID = "";

        strMonth = ddlMonth.SelectedItem.Text == "Nil" ? "0" : ddlMonth.SelectedValue.Trim();
        strYear = ddlYear.SelectedItem.Text == "Nil" ? "0" : ddlYear.SelectedValue.Trim();
        strFiscalYear = ddlFiscalYear.SelectedItem.Text == "Nil" ? "0" : ddlFiscalYear.SelectedValue.Trim();
        strEmpID = "";
        if (strMonth == "0")
            lblPeriod.Text = ddlFiscalYear.SelectedItem.Text;
        else
            lblPeriod.Text = ddlMonth.SelectedItem.Text + " " + ddlFiscalYear.SelectedItem.Text;

        lblPrintDate.Text = Common.DisplayDateTime(DateTime.Now.ToString());

        //Get Employee Info
        string strBasicSal = "";
        string strLedgerID = "";
        string strPrevMonth = objGrMgr.GetLastGLMonth(strFiscalYear);
        string strPrevYear = Common.GetPreviousYear(strMonth, strPrevMonth, strYear);
        bool IsCurrLedgerExist = false;
        long lngLedgerID = 0;

        DataTable dtNewLedger = new DataTable();

        // DataTable dtEmpInfo = objGrMgr.GetEmployeeForGratuity(strMonth, strFiscalYear,"2011-04-02");
        //  DataTable dtEmpInfo = objGrMgr.GetEmployeeForGratuity(strMonth, strFiscalYear, Common.SetDate(dtAccureDate.ToShortDateString()));
        //DataTable dtNewEmp = objGrMgr.GetNewEmployeeForGratuity(strMonth, strFiscalYear, "2011-01-02","2011-04-01");
        // DataTable dtNewEmp = objGrMgr.GetNewEmployeeForGratuity(strMonth, strFiscalYear, Common.SetDate(dtJoinFrom.ToShortDateString()), Common.SetDate(dtJoinTo.ToShortDateString()));
        // DataTable dtEmpWithNullGrDate = objGrMgr.GetEmployeeWithNullGratuityDateExceptNewJoiner(strMonth, strFiscalYear, "2011-04-02", "2011-01-02", "2011-04-01");
        // DataTable dtEmpWithNullGrDate = objGrMgr.GetEmployeeWithNullGratuityDateExceptNewJoiner(strMonth, strFiscalYear, Common.SetDate(dtAccureDate.ToShortDateString()), Common.SetDate(dtJoinFrom.ToShortDateString()), Common.SetDate(dtJoinTo.ToShortDateString()));
        // DataTable dtGrPayment = objGrMgr.GetGrPaymentList(ddlMonth.SelectedValue.Trim(), ddlFiscalYear.SelectedValue.Trim(),"");

        DataTable dtLedger = objGrMgr.GetGratuityLedgerData(strFiscalYear, strMonth, "");
        if (isHTMLFormat == true)
        {
            grLedger.DataSource = dtLedger;
            grLedger.DataBind();
            this.FormatGridView(strYear + "-" + strMonth + "-" + Common.GetMonthDay(strMonth, strYear));
        }
        else
        {
            
        }
    }

    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        this.GetLedgerData(true);
    }

    protected void FormatGridView(string strGraUpto)
    {
        int i = 1;
        foreach (GridViewRow gRow in grLedger.Rows)
        {
            gRow.Cells[0].Text = i.ToString(); 

            if (Common.CheckNullString(gRow.Cells[4].Text.Trim()) != "")
            {
                gRow.Cells[4].Text = Common.DisplayDateIIS(gRow.Cells[4].Text.Trim());
            }
            if (Common.CheckNullString(gRow.Cells[5].Text.Trim()) != "")
            {
                gRow.Cells[5].Text = Common.DisplayDateIIS(gRow.Cells[5].Text.Trim());
            }
            if (Common.CheckNullString(gRow.Cells[6].Text.Trim()) != "")
            {
                gRow.Cells[6].Text = Common.DisplayDateIIS(gRow.Cells[6].Text.Trim());
            }
            gRow.Cells[10].Text = Common.RoundDecimal(gRow.Cells[10].Text.Trim(), 4).ToString(); 
            
            //Month Name
            gRow.Cells[14].Text = Common.ReturnFullMonthName(gRow.Cells[14].Text.Trim());

           // gRow.Cells[6].Text = Convert.ToString(Common.RoundDecimal(gRow.Cells[6].Text.Trim(),0));
            // Amount
          //  gRow.Cells[7].Text = Common.RoundDecimal(gRow.Cells[7].Text.Trim(), 0).ToString();
            // Length

          //  gRow.Cells[8].Text = Common.RoundDecimal(gRow.Cells[8].Text.Trim(), 0).ToString();
            // Yrs

          //  gRow.Cells[9].Text = Common.RoundDecimal(gRow.Cells[9].Text.Trim(), 4).ToString(); 
          //  gRow.Cells[10].Text = Common.RoundDecimal(gRow.Cells[10].Text.Trim(), 0).ToString(); 
            i++;
        }
        if (grLedger.Rows.Count > 0)
        {
            //grLedger.HeaderRow.Cells[10].Text = "As of " + Common.ReturnFullMonthName(grLedger.DataKeys[0].Values[2].ToString().Trim()) + " " + grLedger.DataKeys[0].Values[3].ToString().Trim();
            grLedger.HeaderRow.Cells[11].Text = "As of " + Common.ReturnFullMonthName(grLedger.DataKeys[0].Values[4].ToString().Trim()) + " " + grLedger.DataKeys[0].Values[5].ToString().Trim();
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (grLedger.Rows.Count > 0)
        {
            objGrMgr.GratuityAccrued(grLedger, Session["USERID"].ToString().Trim(), 
                Common.SetDateTime(DateTime.Now.ToString()), ddlFiscalYear.SelectedValue.Trim(),"Y",ddlMonth.SelectedValue.Trim());
            lblMsg.Text = "Gratuity Ledger Saved Successfully.";
        }
        else
        {
            lblMsg.Text = "No record to save.";
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=Gratuity.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        grLedger.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

        // Confirms that an HtmlForm control is rendered for the
        // specified ASP.NET server control at run time.

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        //S = Common.ReturnFullMonthName(ddlMonth.SelectedValue.ToString());

        string strURL = "../../CrystalReports/Payroll/GratuityLedgerViewer.aspx?params=" + ddlMonth.SelectedValue.ToString() + "," + ddlYear.SelectedValue.ToString() + "," + ddlFiscalYear.SelectedValue.ToString()+" ," + ddlFiscalYear.SelectedItem.Text;
        sb.Append("<script>");
       // sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");
        //sb.Append("window.open('" + strURL + "', '', 'directories=0,titlebar=0,toolbar=0,location=0,status=0,menubar=0,scrollbars=yes,resizable=yes');");
        sb.Append("window.open('" + strURL + "', '', '');");
        sb.Append("</script>");

        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit", sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
    }
}
