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
using cashword.BLL;

public partial class Payroll_Payroll_ITStatementReport : System.Web.UI.Page
{
    DataTable dtEmployee = new DataTable();
    DataTable dtPayrollDet = new DataTable();
    DataTable dtChallan = new DataTable();
    DataTable dtFestival = new DataTable();
    PayrollReportManager objPayRptMgr = new PayrollReportManager();
    clscashword objCashWord = new clscashword();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strParams = Request.QueryString["params"];
            string[] strVal = strParams.Split(',');

            dtEmployee = objPayRptMgr.GetEmployeeDataForITStatement(strVal[0], strVal[2], strVal[1]);
            dtFestival = objPayRptMgr.GetBonusAllowanceYearlyEmpWise(strVal[1], strVal[0]);
            if (dtEmployee.Rows.Count > 0)
            {
                lblEmp.Text = dtEmployee.Rows[0]["FULLNAME"].ToString().Trim() + " # " + dtEmployee.Rows[0]["EMPID"].ToString().Trim();
                //lblJoin.Text = string.IsNullOrEmpty(dtEmployee.Rows[0]["JOININGDATE"].ToString().Trim()) == false ? Common.DisplayDate(dtEmployee.Rows[0]["JOININGDATE"].ToString().Trim()) : "N/A";
                //lblFiscalYear.Text = strVal[3];
                lblYear.Text = "30-06-" + strVal[4];
                lblTIN.Text = dtEmployee.Rows[0]["TINNO"].ToString().Trim();
                lblDate.Text = Common.DisplayDate(DateTime.Today.ToShortDateString());

                if (string.IsNullOrEmpty(dtEmployee.Rows[0]["SEX"].ToString().Trim()) == false)
                {
                    lblGender.Text = dtEmployee.Rows[0]["SEX"].ToString().Trim() == "M" ? "his" : "her";
                }
                else
                {
                    lblGender.Text = "his/her";
                }

                // Payroll data

                dtPayrollDet = objPayRptMgr.GetPayrollDataForITStatement(strVal[0], strVal[1]);
                DataTable dtPayrollSum = dtPayrollDet.Clone();
                decimal dclPayAmt = 0;
                foreach (DataRow dRow in dtPayrollDet.Rows)
                {
                    dclPayAmt = 0;
                    if (Convert.ToInt32(dRow["SHEADID"].ToString().Trim()) <= 9)
                    {
                        switch (dRow["SHEADID"].ToString().Trim())
                        {
                            case "1":
                            case "19":
                                dclPayAmt = this.GetSalHeadAmt(dtPayrollDet, strVal[0], "1");
                                dclPayAmt = dclPayAmt + this.GetSalHeadAmt(dtPayrollDet, strVal[0], "19");
                                this.AddNewRow(dtPayrollSum, dclPayAmt, dRow);
                                break;
                            case "2":
                            case "20":
                                dclPayAmt = this.GetSalHeadAmt(dtPayrollDet, strVal[0], "2");
                                dclPayAmt = dclPayAmt + this.GetSalHeadAmt(dtPayrollDet, strVal[0], "20");
                                this.AddNewRow(dtPayrollSum, dclPayAmt, dRow);
                                break;
                            case "3":
                            case "21":
                                dclPayAmt = this.GetSalHeadAmt(dtPayrollDet, strVal[0], "3");
                                dclPayAmt = dclPayAmt + this.GetSalHeadAmt(dtPayrollDet, strVal[0], "21");
                                this.AddNewRow(dtPayrollSum, dclPayAmt, dRow);
                                break;
                            case "4":
                            case "22":
                                dclPayAmt = this.GetSalHeadAmt(dtPayrollDet, strVal[0], "4");
                                dclPayAmt = dclPayAmt + this.GetSalHeadAmt(dtPayrollDet, strVal[0], "22");
                                this.AddNewRow(dtPayrollSum, dclPayAmt, dRow);
                                break;
                            case "6":
                                //dclPayAmt = this.GetSalHeadAmt(dtPayrollDet, strVal[0], "6");
                                if (dtFestival.Rows.Count > 0)
                                {
                                    dclPayAmt = Common.RoundDecimal(dtFestival.Rows[0]["PAYAMT"].ToString(), 0);
                                }
                                else
                                    dclPayAmt = 0;
                                this.AddNewRow(dtPayrollSum, dclPayAmt, dRow);
                                break;
                            case "7":
                            case "23":
                                dclPayAmt = this.GetSalHeadAmt(dtPayrollDet, strVal[0], "7");
                                dclPayAmt = dclPayAmt + this.GetSalHeadAmt(dtPayrollDet, strVal[0], "23");
                                this.AddNewRow(dtPayrollSum, dclPayAmt, dRow);
                                break;

                            case "8":
                                dclPayAmt = this.GetSalHeadAmt(dtPayrollDet, strVal[0], "8");
                                this.AddNewRow(dtPayrollSum, dclPayAmt, dRow);
                                break;
                            case "9":
                            case "24":
                                dclPayAmt = this.GetSalHeadAmt(dtPayrollDet, strVal[0], "9");
                                dclPayAmt = dclPayAmt + this.GetSalHeadAmt(dtPayrollDet, strVal[0], "24");
                                this.AddNewRow(dtPayrollSum, dclPayAmt, dRow);
                                break;
                        }
                    }

                }
                dtPayrollSum.AcceptChanges();
                grPayroll.DataSource = dtPayrollSum;
                grPayroll.DataBind();
                foreach (GridViewRow gRow in grPayroll.Rows)
                {
                    gRow.Cells[1].Text = "Tk.";
                }

                if (grPayroll.Rows.Count > 0)
                {

                    //lblPayrollInWord.Text =objCashWord.getCashWord(this.GetSummaryTotal(grPayroll, 1,0,"Total Amount",0));
                    this.GetSummaryTotal(grPayroll, 2, 0, "Total Amount", 0);
                }
                // Challan Data
                dtChallan = objPayRptMgr.GetChallanDataForITStatement(strVal[0], strVal[1]);
                grChallan.DataSource = dtChallan;
                grChallan.DataBind();
                foreach (GridViewRow gRow in grChallan.Rows)
                {
                    if (Common.CheckNullString(gRow.Cells[4].Text.Trim()) != "")
                    {
                        gRow.Cells[4].Text = Convert.ToString(Math.Round(Convert.ToDecimal(gRow.Cells[4].Text.Trim()), 0));
                    }
                    gRow.Cells[3].Text = "Tk.";
                }
                if (grChallan.Rows.Count > 0)
                {
                    this.GetSummaryTotal(grChallan, 4, 1, "Total Tax deposited from July-" + Convert.ToString(Convert.ToInt32(strVal[4]) - 1) + " thru " + "Jun-" + strVal[4], 2);
                    grChallan.FooterRow.Cells.RemoveAt(1);
                    grChallan.FooterRow.Cells[0].ColumnSpan = 2;

                    //lblTotalChallanAmt.Text = this.GetSummaryTotal(grChallan, 2,1);
                    //lblChallanInWord.Text = objCashWord.getCashWord(lblTotalChallanAmt.Text.Trim());
                    // lblChallanBankName.Text = dtChallan.Rows[0]["BANKNAME"].ToString().Trim();
                }
            }

        }
    }



    protected string GetSummaryTotal(GridView grv, int inCellIndx, int inDateCol, string strSummaryTitle, int inMonthCol)
    {
        decimal decTotal = 0;
        foreach (GridViewRow gRow in grv.Rows)
        {
            decTotal = decTotal + Common.RoundDecimal(gRow.Cells[inCellIndx].Text.Trim(), 2);
            if (inDateCol != 0)
            {
                if (Common.CheckNullString(gRow.Cells[inDateCol].Text.Trim()) != "")
                    gRow.Cells[inDateCol].Text = Common.DisplayDate(gRow.Cells[inDateCol].Text.Trim());
            }
            if (inMonthCol != 0)
            {
                gRow.Cells[inMonthCol].Text = Common.retMonthName(gRow.Cells[inMonthCol].Text) + "," + grv.DataKeys[gRow.DataItemIndex].Values[0].ToString();
            }
        }
        grv.FooterRow.Cells[inCellIndx].Text = decTotal.ToString();

        grv.FooterRow.Cells[inCellIndx].Font.Overline = true;
        grv.FooterRow.Cells[inCellIndx].Font.Underline = true;
        grv.FooterRow.Cells[inCellIndx].HorizontalAlign = HorizontalAlign.Right;

        grv.FooterRow.Cells[inCellIndx - 1].Text = "Tk.";
        grv.FooterRow.Cells[inCellIndx - 1].HorizontalAlign = HorizontalAlign.Right;

        grv.FooterRow.Cells[0].Text = strSummaryTitle;
        grv.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Left;

        //grv.FooterRow.Cells[inCellIndx].BorderStyle = BorderStyle.Solid;
        //grv.FooterRow.Cells[inCellIndx].BorderColor = System.Drawing.Color.Black;
        //grv.FooterRow.Cells[inCellIndx].BorderWidth = Unit.Pixel(2);

        return decTotal.ToString();
    }

    protected decimal GetSalHeadAmt(DataTable dt, string strEmpID, string strSHeadID)
    {
        decimal dclSalHeadAmt = 0;
        DataRow[] foundRows = dt.Select("EMPID='" + strEmpID + "' AND SHEADID=" + strSHeadID);
        if (foundRows.Length > 0)
        {
            dclSalHeadAmt = Convert.ToDecimal(foundRows[0]["PAYAMT"].ToString());
        }
        dclSalHeadAmt = Common.RoundDecimal(dclSalHeadAmt.ToString(), 0);
        return dclSalHeadAmt;
    }

    protected void AddNewRow(DataTable dt, decimal decPayAmt, DataRow dRow)
    {
        DataRow nRow = dt.NewRow();
        nRow["EMPID"] = dRow["EMPID"];
        nRow["SHEADID"] = dRow["SHEADID"];
        nRow["HEADNAME"] = dRow["HEADNAME"];
        nRow["PAYAMT"] = decPayAmt;
        dt.Rows.Add(nRow);
    }
}

