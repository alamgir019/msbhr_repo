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


public partial class CrystalReports_Payroll_TotalPFViewer : System.Web.UI.Page
{
    Payroll_TotalPFCUManager objTotMgr = new Payroll_TotalPFCUManager();
    Payroll_PFManager objPFMgr = new Payroll_PFManager();
    dsPayroll_TPFCU objDs = new dsPayroll_TPFCU();
    Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();
    //private ReportDocument ReportDoc;
    //private PrintDocument printDoc = new PrintDocument();
    //private string ReportPath = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string strParams = Request.QueryString["params"];
        string[] strVal = strParams.Split(',');
        this.GenerateReport(strVal[0], strVal[1]);
    }





    protected void GenerateReport(string strFinYear, string strFinYrTitle)
    {
        // ReportDoc = new ReportDocument();
        // ReportPath = Server.MapPath("~/CrystalReports/Payroll/rptPFLoanLedger.rpt");
        //this.PassParameter(Common.ReturnFullMonthName(strMonth));
        //ReportDoc.Load(ReportPath);
        // ReportDoc.SetDataSource(objPayRptMgr.GetPFLoanLedgerData(strMonth, strFinYear, "M"));


        // CRV.ReportSource = ReportDoc;

        //string strYear = "";
        //DataTable dtPayOpt = objOptMgr.SelectpaySlipOption("OC03");
        //if (dtPayOpt.Rows.Count > 0)
        //{
        //    strYear = Convert.ToDateTime(dtPayOpt.Rows[0]["PAYROLLVALIDFROM"].ToString().Trim()).Year.ToString();
        //}

        DataTable dtPFLedger = objTotMgr.GetPFLedgerData(strFinYear);
        DataTable dtPFLoanLedger = objTotMgr.GetPFLoanLedgerDataForTPF(strFinYear);
        DataTable dtEmp = objTotMgr.GetDistinctDataForTPF(strFinYear);
        DataTable dtPayrollPF = objTotMgr.GetPayrollPFData(strFinYear);
       // DataTable dtPFLedgerJuly = objPFMgr.GetPFLedgerData(strFinYear, "7", strYear, "");

        DataRow[] fPFRows;
        DataRow[] fLoanRows;
        decimal decOwn = 0;
        decimal decCare = 0;
        decimal decTotal = 0;
        decimal decCurrPF = 0;
        decimal decCurrPFInt = 0;

        foreach (DataRow dEmpRow in dtEmp.Rows)
        {
            fPFRows = null;
            fLoanRows = null;
            fPFRows = dtPFLedger.Select("EMPID='" + dEmpRow["EMPID"].ToString().Trim() + "'");
            fLoanRows = dtPFLoanLedger.Select("EMPID='" + dEmpRow["EMPID"].ToString().Trim() + "'");

            DataRow nRow = objDs.dtTotalPF.NewRow();
            nRow["EMPID"] = dEmpRow["EMPID"].ToString().Trim();

            if (fPFRows.Length > 0)
            {
                nRow["NAME"] = fPFRows[0]["FULLNAME"].ToString().Trim();
                nRow["JOININGDATE"] = fPFRows[0]["JOININGDATE"].ToString().Trim();
                if (string.IsNullOrEmpty(fPFRows[0]["CONFIRMATIONDATE"].ToString().Trim()) == false)
                    nRow["CONFIRMATIONDATE"] = fPFRows[0]["CONFIRMATIONDATE"].ToString().Trim();

            }
            decCurrPF = 0;
            decOwn = 0;
            decCare = 0;
            decTotal = 0;
            foreach (DataRow dPFRow in fPFRows)
            {
                // PF Ledger
                if (dPFRow["LEDGERID"].ToString().Trim() == dEmpRow["LEDGERID"].ToString().Trim())
                {
                    //decOwn = Common.RoundDecimal(dPFRow["CUPFOWN"].ToString().Trim(), 0);
                    //decCare = Common.RoundDecimal(dPFRow["CUPFCARE"].ToString().Trim(), 0);
                    //decTotal = decOwn + decCare;
                  //  nRow["OPENINGPF"] = decTotal.ToString();
                    //nRow["OPENINGINTEREST"] = dPFRow["CUPFINTREST"].ToString().Trim();
                    nRow["CURRENTPFINTEREST"] = "0";
                    nRow["MONTHOFINTEREST"] = Common.RoundDecimal(dPFRow["MONTHOFINTEREST"].ToString().Trim(), 0);
                    nRow["PFMONTH"] = Common.ReturnFullMonthName("6");
                    nRow["PFYEAR"] = strFinYrTitle;
                }

               

                switch (dPFRow["VMONTH"].ToString().Trim())
                {
                    case "1":
                        nRow["JAN"] = dPFRow["CMPFOWN"].ToString().Trim();
                        break;
                    case "2":
                        nRow["FEB"] = dPFRow["CMPFOWN"].ToString().Trim();
                        break;
                    case "3":
                        nRow["MAR"] = dPFRow["CMPFOWN"].ToString().Trim();
                        break;
                    case "4":
                        nRow["APR"] = dPFRow["CMPFOWN"].ToString().Trim();
                        break;
                    case "5":
                        nRow["MAY"] = dPFRow["CMPFOWN"].ToString().Trim();
                        break;
                    case "6":
                        nRow["JUN"] = dPFRow["CMPFOWN"].ToString().Trim();
                        break;
                    case "7":
                       // decOwn = Common.RoundDecimal(dPFRow["CUPFOWN"].ToString().Trim(), 0);
                       // decCare = Common.RoundDecimal(dPFRow["CUPFCARE"].ToString().Trim(), 0);
                      //  decTotal = decOwn + decCare;
                        nRow["OPENINGPF"] = Common.RoundDecimal(dPFRow["OPTOTAL"].ToString().Trim(), 0);
                        nRow["OPENINGINTEREST"] = dPFRow["OPPFINTREST"].ToString().Trim();
                        nRow["JUL"] = dPFRow["CMPFOWN"].ToString().Trim();
                        break;
                    case "8":
                        nRow["AUG"] = dPFRow["CMPFOWN"].ToString().Trim();
                        break;
                    case "9":
                        nRow["SEP"] = dPFRow["CMPFOWN"].ToString().Trim();
                        break;
                    case "10":
                        nRow["OCT"] = dPFRow["CMPFOWN"].ToString().Trim();
                        break;
                    case "11":
                        nRow["NOV"] = dPFRow["CMPFOWN"].ToString().Trim();
                        break;
                    case "12":
                        nRow["DEC"] = dPFRow["CMPFOWN"].ToString().Trim();
                        break;
                }

                // Current Fiscal PF
                decCurrPF = decCurrPF + Common.RoundDecimal(dPFRow["CMPFOWN"].ToString().Trim(), 0);

                // PF Loan Ledger
                if (fLoanRows.Length > 0)
                {
                    nRow["PFLOAN"] = fLoanRows[0]["TOTALLOAN"].ToString().Trim();
                    nRow["PFLOANDEDUCTION"] = fLoanRows[0]["TOTALREPAID"].ToString().Trim();
                    nRow["PFINTERESTDEDUCTION"] = fLoanRows[0]["TOTALINTEREST"].ToString().Trim();
                    nRow["CASHPAY"] = fLoanRows[0]["CMCASH"].ToString().Trim();
                    nRow["MONTHLYREPAY"] = fLoanRows[0]["CMREPAY"].ToString().Trim();
                    nRow["LASTOPENINGLOAN"] = fLoanRows[0]["CLLOAN"].ToString().Trim();
                    nRow["PFMONTHLYINTEREST"] = fLoanRows[0]["CMINTEREST"].ToString().Trim();
                    //nRow["PAYMENTDATE"] ="";
                    nRow["PAYMENTAMOUNT"] = "0";
                    nRow["LASTMONTHREPAY"] = fLoanRows[0]["LMREPAY"].ToString().Trim();
                }
            }
            // Current Fiscal PF
            nRow["CURRENTPF"] = decCurrPF.ToString();

            objDs.dtTotalPF.Rows.Add(nRow);
        }
        objDs.dtTotalPF.AcceptChanges();
        grExport.DataSource = objDs.dtTotalPF;
        grExport.DataBind();

        // Format by Zero
        foreach (GridViewRow gRow in grExport.Rows)
        {
            if (Common.CheckNullString(gRow.Cells[2].Text.Trim()) != "")
                gRow.Cells[2].Text = Common.DisplayDateIIS(gRow.Cells[2].Text.Trim());
            if (Common.CheckNullString(gRow.Cells[3].Text.Trim()) != "")
                gRow.Cells[3].Text = Common.DisplayDateIIS(gRow.Cells[3].Text.Trim());

            for (int i = 4; i <= 32; i++)
            {
                if (i != 30)
                {
                    if (Common.CheckNullString(gRow.Cells[i].Text.Trim()) == "")
                        gRow.Cells[i].Text = "0";
                }
            }
        }
    }

    //public void PassParameter(string strMonthName)
    //{
    //    ParameterFields pFields = new ParameterFields();        
    //    ParameterField pMonthName = new ParameterField();
       
    //    //Generate ParameterDiscreteValue        
    //    ParameterDiscreteValue MonthName = new ParameterDiscreteValue();
    //    ParameterDiscreteValue dvHeader = new ParameterDiscreteValue();

    //    //Adding ParameterDiscreteValue to ParameterField        
    //    pMonthName.Name = "pMonthName";
    //    MonthName.Value = strMonthName;
    //    pMonthName.CurrentValues.Add(MonthName);

    //    //Adding Parameters to ParameterFields 
    //    pFields.Add(pMonthName);
        
    //    //Passing ParameterFields to CrystalReportViewer
    //    CRV.ParameterFieldInfo = pFields;
    //}

    //protected void CRV_Unload(object sender, EventArgs e)
    //{
    //    //ReportDoc.Close();
    //    //ReportDoc.Dispose();
    //    //ReportDoc = null;
    //    //GC.Collect();
    //    //GC.WaitForPendingFinalizers();
    //}
    //protected void CRV_BeforeRender(object source, CrystalDecisions.Web.HtmlReportRender.BeforeRenderEvent e)
    //{
    //    Page.ClientScript.RegisterForEventValidation(CRV.UniqueID);
    //}
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=tPF.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        grExport.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

        // Confirms that an HtmlForm control is rendered for the
        // specified ASP.NET server control at run time.

    }
    
}
