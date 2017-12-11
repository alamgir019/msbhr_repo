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
using System.Xml;
using System.Text;
using System.IO;

public partial class Payroll_Payroll_PayrollReportWithLeave : System.Web.UI.Page
{
    MasterTablesManager objMastMg = new MasterTablesManager();
    Payroll_PreparationManager objPreMgr = new Payroll_PreparationManager();
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();
    PayrollReportManager objPayRptMgr = new PayrollReportManager();


    DataTable dtGrossSalHead = new DataTable();
    dsPayroll_Payslip objPayslip = new dsPayroll_Payslip();
    DataTable dtPayrollSummary;
    DataTable dtEmpPayroll = new DataTable();

    decimal dclEmpBenefits = 0;
    decimal dclEmpDeduct = 0;
    decimal dclTotalSalary = 0;
   



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            Common.FillDropDownList(objMastMg.SelectLocation(0), ddlGenerateValue, "LocationName", "LocationID", false);
            Common.FillDropDownList(objPayrollMgr.SelectBankAndBranchList(), ddlBank, "BANKBRANCH", "RoutingNo", true, "Nil");
            Common.FillDropDownList(objMastMg.SelectEmpGroup(0), ddlGroup, "GrpName", "EmpGrpID", false);
        }
    }

    protected void OpenRecord()
    {
        //dtSalHead = new DataTable();
        //dtSalHead = objPreMgr.GetSalaryHead();

    }

    protected void InitializeSummaryTable(int inCol)
    {
        int i = 0;
        dtPayrollSummary = new DataTable();
        for (i = 0; i < inCol; i++)
        {
            dtPayrollSummary.Columns.Add(i.ToString());
        }
    }
    //protected void FillGenerateDropDownList()
    //{
    //    switch(ddlGeneratefor.SelectedValue)
    //    {
    //        case "O":
    //           // Common.FillDropDownList_Nil(objMasMgr.SelectLocation(0), ddlDivision);
    //            Common.FillDropDownList(objMastMg.SelectLocation(0), ddlGenerateValue, "LocationName", "LocationID", false);
    //            break;
    //        case "A":
    //            ddlGenerateValue.DataSource = null;
    //            ddlGenerateValue.DataBind();
    //            break;
    //        case "B":
    //            Common.FillDropDownList(objEmpInfoMgr.SelectBankList(), ddlGenerateValue, "BankName", "BankCode", true, "Nil");
    //            break;
    //    }
      
    //}

   

  

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
       // this.OpenRecord();
        this.GeneratePayrollReport();

        //grPayslipMst.DataSource = objPayslip.Tables["dtPaySlipMst"];
       // grPayslipMst.DataBind();

       // this.WritePaySlipDetailsToXmlFile();

       
    }


    protected void GeneratePayrollReport()
    {
        DataRow[] foundEmpSalHeadRow;
        string strEmpID="";
        string strGenerateValue="";
        
        int inGrossHeadCount = 0;
        bool EmpGrossColAdded = false;
        int inBenefitHeadCount = 0;
        int inDeductCount = 0;
        decimal dclSalHeadAmt=0;

        switch (ddlGeneratefor.SelectedValue.ToString())
        {
            case "O":
                strGenerateValue = ddlGenerateValue.SelectedValue.ToString();
                lblGenerateFor.Text = ddlGenerateValue.SelectedItem.Text.Trim();
                break;
            case "B":
                strGenerateValue = ddlBank.SelectedValue.ToString();
                lblGenerateFor.Text = ddlBank.SelectedItem.Text.Trim();
                break;
            case "E":
                strGenerateValue = txtTextValue.Text.Trim();                
                break;
        }
        DataTable dtSalaryHead = objPayrollMgr.SelectTotalSalHeadWithSeq(0);
        DataTable dtHeadCount = objPayRptMgr.GetHeadCount();
        DataRow[] founHCRows = dtHeadCount.Select("DISPLAYTYPE='B'");
        inBenefitHeadCount = Convert.ToInt32(founHCRows[0]["HEADCOUNT"]);
        founHCRows = null;
        founHCRows = dtHeadCount.Select("DISPLAYTYPE='D'");
        inDeductCount = Convert.ToInt32(founHCRows[0]["HEADCOUNT"]); 

        dtGrossSalHead = objPayrollMgr.SelectGrossSalHead(0);
        dtEmpPayroll = objPayRptMgr.GetPayrollDatAForExcel(ddlGeneratefor.SelectedValue.ToString(), strGenerateValue,
            ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), ddlBank.SelectedValue.Trim(),
            chkBonus.Checked == true ? "B" : "S",ddlGroup.SelectedValue.Trim());
        this.InitializeSummaryTable(dtSalaryHead.Rows.Count + 14);
       

        int i=4;
        int j = 1;
        foreach (DataRow dEmpRow in dtEmpPayroll.Rows)
        {
            dclEmpBenefits = 0;
            dclEmpDeduct = 0;
            dclTotalSalary = 0;
            this.GetEmpBenefitsAmount(dtSalaryHead, dEmpRow["EMPID"].ToString().Trim(), dEmpRow["GROSSAMNT"].ToString());
            i = 9;
            if(strEmpID ==dEmpRow["EMPID"].ToString().Trim())
            {
                continue;
            }
            DataRow nRow = dtPayrollSummary.NewRow();
            nRow[0]= Convert.ToString(j);
            nRow[1] = dEmpRow["EMPID"].ToString().Trim();
            nRow[2]=dEmpRow["FULLNAME"].ToString().Trim();
            nRow[3]="";
            nRow[4] = dEmpRow["JoiningDate"].ToString().Trim();
            nRow[5] = dEmpRow["DOB"].ToString().Trim();
            nRow[6] = dEmpRow["DEPTNAME"].ToString().Trim();
            nRow[7] = dEmpRow["JOBTITLE"].ToString().Trim();
            nRow[8] = dEmpRow["GradeName"].ToString().Trim();
            foreach (DataRow dSalRow in dtSalaryHead.Rows)
            {
                if (i - 9 == dtGrossSalHead.Rows.Count)
                {
                   
                    nRow[i] = Common.RoundDecimal(dEmpRow["GROSSAMNT"].ToString(), 0);
                    i++;

                }
                if ((i - 9) - dtGrossSalHead.Rows.Count  == inBenefitHeadCount + 1)
                {
                  
                    nRow[i] = dclEmpBenefits.ToString();
                    i++;
                }
                if ((i - 9) - dtGrossSalHead.Rows.Count == inBenefitHeadCount + 2)
                {
                   
                    nRow[i] = dclTotalSalary.ToString();
                    i++;

                    dclSalHeadAmt = 0;
                    dclSalHeadAmt = this.GetSalHeadAmt(dEmpRow["EMPID"].ToString().Trim(), dSalRow["SHEADID"].ToString().Trim());
                    if (dSalRow["DISPLAYTYPE"].ToString().Trim() == "D")
                    {
                        if (dclSalHeadAmt > 0)
                            dclSalHeadAmt = dclSalHeadAmt * -1;
                    }

                    nRow[i] = dclSalHeadAmt.ToString();
                    i++;
                }
                else
                {
                    dclSalHeadAmt=0;
                    dclSalHeadAmt= this.GetSalHeadAmt(dEmpRow["EMPID"].ToString().Trim(), dSalRow["SHEADID"].ToString().Trim());
                    if(dSalRow["DISPLAYTYPE"].ToString().Trim()=="D")
                    {
                        if (dclSalHeadAmt > 0)
                            dclSalHeadAmt = dclSalHeadAmt * -1;
                    }
                    
                    nRow[i] =dclSalHeadAmt.ToString();
                    i++;
                }
            }
           
            
            nRow[i] = dclEmpDeduct.ToString();
            i++;
            //}
           
            nRow[i] = Common.RoundDecimal(dEmpRow["NETPAY"].ToString(), 0);

            dtPayrollSummary.Rows.Add(nRow);
            dtPayrollSummary.AcceptChanges();
            strEmpID=dEmpRow["EMPID"].ToString().Trim();
            j++;
        }

        grPayroll.DataSource = dtPayrollSummary;
        grPayroll.DataBind();
        if (dtPayrollSummary.Rows.Count > 0)
        {
            this.FormatGridView(dtSalaryHead, inBenefitHeadCount);
            this.GetSummaryTotal();
            if (ddlGeneratefor.SelectedValue.Trim() == "E")
                lblGenerateFor.Text = grPayroll.Rows[0].Cells[2].Text.Trim() + " [" + grPayroll.Rows[0].Cells[1].Text.Trim() + "] ";
            lblPayrollMonth.Text = "Salary for the month of " + ddlMonth.SelectedItem.Text + " " + ddlYear.SelectedItem.Text;

            lblPreparedBy.Text = dtEmpPayroll.Rows[0]["PREPAREDBY"].ToString().Trim();
            lblPreparedDate.Text = string.IsNullOrEmpty(dtEmpPayroll.Rows[0]["PREPARINGDATE"].ToString().Trim()) == false ? Common.DisplayDateTime(dtEmpPayroll.Rows[0]["PREPARINGDATE"].ToString().Trim()) : "";
            lblReviewedBy.Text = dtEmpPayroll.Rows[0]["REVIEWEDBY"].ToString().Trim();
            lblReviewDate.Text = string.IsNullOrEmpty(dtEmpPayroll.Rows[0]["REVIEWDATE"].ToString().Trim()) == false ? Common.DisplayDateTime(dtEmpPayroll.Rows[0]["REVIEWDATE"].ToString().Trim()) : "";
            lblCheckedBy.Text = dtEmpPayroll.Rows[0]["CHECKEDBY"].ToString().Trim();
            lblCheckDate.Text = string.IsNullOrEmpty(dtEmpPayroll.Rows[0]["CHECKDATE"].ToString().Trim()) == false ? Common.DisplayDateTime(dtEmpPayroll.Rows[0]["CHECKDATE"].ToString().Trim()) : "";
            lblApprovedBy.Text = dtEmpPayroll.Rows[0]["APPROVEDBY"].ToString().Trim();
            lblApproveDate.Text = string.IsNullOrEmpty(dtEmpPayroll.Rows[0]["APPROVINGDATE"].ToString().Trim()) == false ? Common.DisplayDateTime(dtEmpPayroll.Rows[0]["APPROVINGDATE"].ToString().Trim()) : "";
            lblDisburseBy.Text = dtEmpPayroll.Rows[0]["DISBURSEBY"].ToString().Trim();
            lblDisburseDate.Text = string.IsNullOrEmpty(dtEmpPayroll.Rows[0]["DISBURSINGDATE"].ToString().Trim()) == false ? Common.DisplayDateTime(dtEmpPayroll.Rows[0]["DISBURSINGDATE"].ToString().Trim()) : "";
        }
        else
        {
            lblGenerateFor.Text = "";
            lblPayrollMonth.Text = "";
            lblPreparedBy.Text = "";
            lblPreparedDate.Text = "";
            lblReviewedBy.Text = "";
            lblReviewDate.Text = "";
            lblCheckedBy.Text = "";
            lblCheckDate.Text = "";
            lblApprovedBy.Text = "";
            lblApproveDate.Text = "";
            lblDisburseBy.Text = "";
            lblDisburseDate.Text = "";
        }
       
    }

    protected void FormatGridView(DataTable dtSalaryHead, int inBenefitHeadCount)
    {
        int j = 9;
        string strRowIndx = "";
        grPayroll.HeaderRow.Cells[0].Text = "Sl";
        grPayroll.HeaderRow.Cells[1].Text = "Emp. ID";
        grPayroll.HeaderRow.Cells[2].Text = "Employee Name";
        grPayroll.HeaderRow.Cells[3].Text = "Account Line";
        grPayroll.HeaderRow.Cells[4].Text = "Joining Date";
        grPayroll.HeaderRow.Cells[5].Text = "Date of Birth";
        grPayroll.HeaderRow.Cells[6].Text = "Designation";
        grPayroll.HeaderRow.Cells[7].Text = "JoiningDate";
        grPayroll.HeaderRow.Cells[8].Text = "Grade";
        for (int i=0;i< dtPayrollSummary.Columns.Count;i++)
        {
            if (j - 9 == dtGrossSalHead.Rows.Count)
            {
                grPayroll.HeaderRow.Cells[j].Text = "Gross Salary";
                strRowIndx = j.ToString();
                j++;
            }
            if ((j - 9) - dtGrossSalHead.Rows.Count == inBenefitHeadCount + 1)
            {
                grPayroll.HeaderRow.Cells[j].Text = "Total Benefits";
                strRowIndx = strRowIndx + "," + j.ToString();
                j++;
            }
            if ((j - 9) - dtGrossSalHead.Rows.Count  == inBenefitHeadCount + 2)
            {
                grPayroll.HeaderRow.Cells[j].Text = "Total Salary";
                strRowIndx = strRowIndx + "," + j.ToString();
                j++;

                if (i < dtSalaryHead.Rows.Count)
                {
                    grPayroll.HeaderRow.Cells[j].Text = dtSalaryHead.Rows[i]["SHORTNAME"].ToString();
                    //strRowIndx = strRowIndx + "," + j.ToString();
                    j++;
                }
            }
            else
            {
                if (i < dtSalaryHead.Rows.Count)
                {
                    grPayroll.HeaderRow.Cells[j].Text = dtSalaryHead.Rows[i]["SHORTNAME"].ToString();
                    //strRowIndx = strRowIndx + "," + j.ToString();
                    j++;
                }
            }
        }
        grPayroll.HeaderRow.Cells[j].Text = "Total";
        strRowIndx = strRowIndx + "," + j.ToString();
        j++;
        grPayroll.HeaderRow.Cells[j].Text = "Net Salary";
        strRowIndx = strRowIndx + "," + j.ToString();
        this.HighlightGridViewCells(strRowIndx);

    }

    protected void GetEmpBenefitsAmount(DataTable dtSalHead,string strEmpID,string strGrossSal)
    {
        dclTotalSalary = Convert.ToDecimal(strGrossSal);
        decimal dclSalHeadAmt=0;
        foreach (DataRow dRow in dtSalHead.Rows)
        {
            switch (dRow["DISPLAYTYPE"].ToString())
            {
                case "B":
                    dclEmpBenefits = dclEmpBenefits + this.GetSalHeadAmt(strEmpID,dRow["SHEADID"].ToString());
                    break;
                case "D":
                    dclSalHeadAmt=this.GetSalHeadAmt(strEmpID, dRow["SHEADID"].ToString());
                    
                    if(dclSalHeadAmt > 0)
                        dclSalHeadAmt=dclSalHeadAmt * -1;

                    dclEmpDeduct = dclEmpDeduct + dclSalHeadAmt;
                    break;
                    
            }
        }
        dclTotalSalary = dclTotalSalary + dclEmpBenefits;
        dclTotalSalary = Common.RoundDecimal(dclTotalSalary.ToString(), 0);
        dclEmpBenefits = Common.RoundDecimal(dclEmpBenefits.ToString(), 0);
        dclEmpDeduct = Common.RoundDecimal(dclEmpDeduct.ToString(), 0);

    }

    protected void HighlightGridViewCells(string strIndx)
    {
        string[] strArr = strIndx.Split(',');
        int indx=0;
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            foreach (string str in strArr)
            {
                indx=Convert.ToInt32(str);
                gRow.Cells[indx].Font.Bold = true;
            }

            if (Common.CheckNullString(gRow.Cells[5].Text.Trim()) != "")
                gRow.Cells[5].Text = Common.DisplayDate(gRow.Cells[5].Text.Trim());
            if (Common.CheckNullString(gRow.Cells[4].Text.Trim()) != "")
                gRow.Cells[4].Text = Common.DisplayDate(gRow.Cells[4].Text.Trim());
        }
    }

    protected void GetSummaryTotal()
    {
        int i=0;
        decimal dclSumValue=0;
        grPayroll.FooterRow.Cells[6].Text = "Total ";
        for (int j=9;j< dtPayrollSummary.Columns.Count;j++)
        {
            dclSumValue=0;
            i = 0;
            foreach (DataRow dRow in dtPayrollSummary.Rows)
            {
                dclSumValue=dclSumValue+ Convert.ToDecimal(dRow[j].ToString());
                if (Convert.ToInt64(dRow[j].ToString()) == 0)
                {
                    grPayroll.Rows[i].Cells[j].Text = "";
                }
                grPayroll.Rows[i].Cells[j].HorizontalAlign = HorizontalAlign.Right;
                i++;
            }
            if (dclSumValue == 0)
                grPayroll.FooterRow.Cells[j].Text = "";
            else
                grPayroll.FooterRow.Cells[j].Text = dclSumValue.ToString();
            grPayroll.FooterRow.Cells[j].HorizontalAlign = HorizontalAlign.Right;
        }
    }

    protected bool IsGrossHead(string strSHeadID)
    {
        DataRow[] foundRows = dtGrossSalHead.Select("SHEADID=" + strSHeadID );
        if (foundRows.Length > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected decimal GetSalHeadAmt(string strEmpID, string strSHeadID)
    {
        decimal dclSalHeadAmt = 0;
        DataRow[] foundRows = dtEmpPayroll.Select("EMPID='" + strEmpID + "' AND SHEADID=" + strSHeadID);
        if (foundRows.Length > 0)
        {
            dclSalHeadAmt = Convert.ToDecimal(foundRows[0]["PAYAMT"].ToString());
        }
        dclSalHeadAmt = Common.RoundDecimal(dclSalHeadAmt.ToString(), 0);
        return dclSalHeadAmt;
    }

    protected void grPayslipMst_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        //string strPreYrLv = "";
        switch (_commandName)
        {
            case ("DoubleClick"):
                //Open New Window
                StringBuilder sb = new StringBuilder();
                string strURL = "PaySlipDetails.aspx?params=" + grPayroll.SelectedRow.Cells[1].Text.Trim() + ","
                    + grPayroll.SelectedRow.Cells[2].Text.Trim() + ","
                    // + grPayslipMst.SelectedRow.Cells[3].Text.Trim() + ","
                    + grPayroll.SelectedRow.Cells[21].Text.Trim() + ","
                    + grPayroll.SelectedRow.Cells[9].Text.Trim() + ","
                    + grPayroll.SelectedRow.Cells[13].Text.Trim();
                sb.Append("<script>");
                //sb.Append("window.open('" + strURL + "', '', 'fullscreen=true,scrollbars=yes,resizable=yes');");//
                sb.Append("window.open('" + strURL + "', '', '');");
                sb.Append("</script>");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                         sb.ToString(), false);
                ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());
                break;
        }
    }

  

    protected void btnSave_Click(object sender, EventArgs e)
    {
      // this.ValidateAndSave();
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=Payroll.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        sw = this.GetHeaderText();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        grPayroll.RenderControl(htw);
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
        sw.WriteLine("iDE ");
        sw.WriteLine("</td>");
        sw.WriteLine("</tr>");
        sw.WriteLine("<tr>");
        sw.WriteLine("<td style=" + "\"text-align:right;border:solid 1px white;font-size:14px\">");
        sw.WriteLine(lblGenerateFor.Text.ToString());
        sw.WriteLine("</td>");
        sw.WriteLine("</tr>");
        sw.WriteLine("<tr>");
        sw.WriteLine("<td style=" + "\"text-align:right;border:solid 1px white;font-size:14px\">");
        sw.WriteLine(lblPayrollMonth.Text.ToString());
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
