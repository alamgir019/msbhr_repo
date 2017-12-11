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

public partial class Payroll_Payroll_PayrollAudit : System.Web.UI.Page
{
    MasterTablesManager objMastMg = new MasterTablesManager();
    Payroll_PreparationManager objPreMgr = new Payroll_PreparationManager();
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();
    PayrollReportManager objPayRptMgr = new PayrollReportManager();
    Payroll_PayslipApprovalManager objPayAppMgr = new Payroll_PayslipApprovalManager();
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
            //Common.FillDropDownList(objMastMg.SelectLocation(0), ddlGenerateValue, "LocationName", "LocationID", false);
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
       // this.GetMovementReport();
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
            case "B":
                strGenerateValue = ddlBank.SelectedValue.ToString();
                lblGenerateFor.Text = ddlBank.SelectedItem.Text.Trim();
                lblGenerateForTab2.Text = ddlBank.SelectedItem.Text.Trim();
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
        
        // Reviewed Data
        dtEmpPayroll = objPayAppMgr.GetReviewedDataForEndorcement(ddlGeneratefor.SelectedValue.ToString(), strGenerateValue,
            ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), ddlBank.SelectedValue.Trim(),
            chkBonus.Checked == true ? "B" : "S", ddlGroup.SelectedValue.Trim());

        this.GenerateReport(grReviewList, dtSalaryHead, inBenefitHeadCount,lblGenerateFor,lblPayrollMonth);
        if (grReviewList.Rows.Count > 0)
        {
            lblPreparedBy.Text = dtEmpPayroll.Rows[0]["PREPAREDBY"].ToString().Trim();
            lblPreparedDate.Text = string.IsNullOrEmpty(dtEmpPayroll.Rows[0]["PREPARINGDATE"].ToString().Trim()) == false ? Common.DisplayDateTime(dtEmpPayroll.Rows[0]["PREPARINGDATE"].ToString().Trim()) : "";
            lblReviewedBy.Text = dtEmpPayroll.Rows[0]["REVIEWEDBY"].ToString().Trim();
            lblReviewDate.Text = string.IsNullOrEmpty(dtEmpPayroll.Rows[0]["REVIEWDATE"].ToString().Trim()) == false ? Common.DisplayDateTime(dtEmpPayroll.Rows[0]["REVIEWDATE"].ToString().Trim()) : "";
        }
        else
        {
            lblPreparedBy.Text = "";
            lblPreparedDate.Text = "";
            lblReviewedBy.Text = "";
            lblReviewDate.Text = "";
        }
        // Audited Data
        dtEmpPayroll.Rows.Clear();
        dtEmpPayroll.Dispose();
        dtEmpPayroll = objPayAppMgr.GetPayrollAuditedData(ddlGeneratefor.SelectedValue.ToString(), strGenerateValue,
            ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), ddlBank.SelectedValue.Trim(),
            chkBonus.Checked == true ? "B" : "S", ddlGroup.SelectedValue.Trim());
        this.GenerateReport(grAuditList, dtSalaryHead, inBenefitHeadCount, lblGenerateForTab2, lblPayrollMonthTab2);
        if (grAuditList.Rows.Count > 0)
        {
            lblPreparedByTab2.Text = dtEmpPayroll.Rows[0]["PREPAREDBY"].ToString().Trim();
            lblPreparedDateTab2.Text = string.IsNullOrEmpty(dtEmpPayroll.Rows[0]["PREPARINGDATE"].ToString().Trim()) == false ? Common.DisplayDateTime(dtEmpPayroll.Rows[0]["PREPARINGDATE"].ToString().Trim()) : "";
            lblReviewedByTab2.Text = dtEmpPayroll.Rows[0]["REVIEWEDBY"].ToString().Trim();
            lblReviewDateTab2.Text = string.IsNullOrEmpty(dtEmpPayroll.Rows[0]["REVIEWDATE"].ToString().Trim()) == false ? Common.DisplayDateTime(dtEmpPayroll.Rows[0]["REVIEWDATE"].ToString().Trim()) : "";
            lblCheckedByTab2.Text = dtEmpPayroll.Rows[0]["CHECKEDBY"].ToString().Trim();
            lblCheckDateTab2.Text = string.IsNullOrEmpty(dtEmpPayroll.Rows[0]["CHECKDATE"].ToString().Trim()) == false ? Common.DisplayDateTime(dtEmpPayroll.Rows[0]["CHECKDATE"].ToString().Trim()) : "";
            lblApprovedByTab2.Text = dtEmpPayroll.Rows[0]["APPROVEDBY"].ToString().Trim();
            lblApproveDateTab2.Text = string.IsNullOrEmpty(dtEmpPayroll.Rows[0]["APPROVINGDATE"].ToString().Trim()) == false ? Common.DisplayDateTime(dtEmpPayroll.Rows[0]["APPROVINGDATE"].ToString().Trim()) : "";
            lblDisburseByTab2.Text = dtEmpPayroll.Rows[0]["DISBURSEBY"].ToString().Trim();
            lblDisburseDatetab2.Text = string.IsNullOrEmpty(dtEmpPayroll.Rows[0]["DISBURSINGDATE"].ToString().Trim()) == false ? Common.DisplayDateTime(dtEmpPayroll.Rows[0]["DISBURSINGDATE"].ToString().Trim()) : "";
        }
        else
        {
            lblPreparedByTab2.Text = "";
            lblPreparedDateTab2.Text = "";
            lblReviewedByTab2.Text = "";
            lblReviewDateTab2.Text = "";
            lblCheckedByTab2.Text = "";
            lblCheckDateTab2.Text = "";
            lblApprovedByTab2.Text = "";
            lblApproveDateTab2.Text = "";
            lblDisburseByTab2.Text = "";
            lblDisburseDatetab2.Text = "";
        }

        // Tab Container Height
        int inHeightRev = grReviewList.Rows.Count * 17 + 200;
        int inHeightAud = grAuditList.Rows.Count * 17 + 200;

        if (inHeightRev < 430)
            inHeightRev = 430;
        if (inHeightAud < 430)
            inHeightAud = 430;

        if (inHeightRev > inHeightAud)
        {
            TabContainer1.Height = Unit.Pixel(inHeightRev);
        }
        else
        {
            TabContainer1.Height = Unit.Pixel(inHeightAud);
        }
        if (grReviewList.Rows.Count > 0)
        {
            TabContainer1.ActiveTabIndex = 0;
        }
        else if (grAuditList.Rows.Count > 0)
        {
            TabContainer1.ActiveTabIndex = 1;
        }
        else
        {
            TabContainer1.ActiveTabIndex = 0;
        }            
    }

    protected void GenerateReport(GridView grv, DataTable dtSalaryHead, int inBenefitHeadCount,Label lblGenFor,Label lblMonth)
    {
        string strEmpID = "";
        decimal dclSalHeadAmt=0;
        this.InitializeSummaryTable(dtSalaryHead.Rows.Count + 9);
        int i = 4;
        int j = 1;
        foreach (DataRow dEmpRow in dtEmpPayroll.Rows)
        {
            dclEmpBenefits = 0;
            dclEmpDeduct = 0;
            dclTotalSalary = 0;
            this.GetEmpBenefitsAmount(dtSalaryHead, dEmpRow["EMPID"].ToString().Trim(), dEmpRow["GROSSAMNT"].ToString());
            i = 4;
            if (strEmpID == dEmpRow["EMPID"].ToString().Trim())
            {
                continue;
            }
            DataRow nRow = dtPayrollSummary.NewRow();
            nRow[0] = Convert.ToString(j);
            nRow[1] = dEmpRow["EMPID"].ToString().Trim();
            nRow[2] = dEmpRow["FULLNAME"].ToString().Trim();
            nRow[3] = dEmpRow["JOBTITLE"].ToString().Trim();
            foreach (DataRow dSalRow in dtSalaryHead.Rows)
            {
                if (i - 4 == dtGrossSalHead.Rows.Count)
                {

                    nRow[i] = Common.RoundDecimal(dEmpRow["GROSSAMNT"].ToString(), 0);
                    i++;

                }
                if ((i - 4) - dtGrossSalHead.Rows.Count == inBenefitHeadCount + 1)
                {

                    nRow[i] = dclEmpBenefits.ToString();
                    i++;
                }
                if ((i - 4) - dtGrossSalHead.Rows.Count == inBenefitHeadCount + 2)
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
            }


            nRow[i] = dclEmpDeduct.ToString();
            i++;


            nRow[i] = Common.RoundDecimal(dEmpRow["NETPAY"].ToString(), 0);

            dtPayrollSummary.Rows.Add(nRow);
            dtPayrollSummary.AcceptChanges();
            strEmpID = dEmpRow["EMPID"].ToString().Trim();
            j++;
        }


        grv.DataSource = dtPayrollSummary;
        grv.DataBind();


        if (dtPayrollSummary.Rows.Count > 0)
        {
            this.FormatGridView(grv, dtSalaryHead, inBenefitHeadCount);
            this.GetSummaryTotal(grv);
            if (ddlGeneratefor.SelectedValue.Trim() == "E")
                lblGenFor.Text = grv.Rows[0].Cells[2].Text.Trim() + " [" + grv.Rows[0].Cells[1].Text.Trim() + "] ";

            if (dtEmpPayroll.Rows[0]["SALARYTYPE"].ToString().Trim() == "S")
                lblMonth.Text = "Salary for the month of " + ddlMonth.SelectedItem.Text + " " + ddlYear.SelectedItem.Text;
            else if (dtEmpPayroll.Rows[0]["SALARYTYPE"].ToString().Trim() == "B")
                lblMonth.Text = "Bonus for the month of " + ddlMonth.SelectedItem.Text + " " + ddlYear.SelectedItem.Text;
        }
        else
        {
            lblGenFor.Text = "";
            lblMonth.Text = "";
        }
    }

    protected void FormatGridView(GridView grv, DataTable dtSalaryHead, int inBenefitHeadCount)
    {
        int j = 4;
        string strRowIndx = "";
        grv.HeaderRow.Cells[0].Text = "Sl";
        grv.HeaderRow.Cells[1].Text = "Emp. ID";
        grv.HeaderRow.Cells[2].Text = "Employee Name";
        grv.HeaderRow.Cells[3].Text = "Designation";
        for (int i=0;i< dtPayrollSummary.Columns.Count;i++)
        {
            if (j - 4 == dtGrossSalHead.Rows.Count)
            {
                grv.HeaderRow.Cells[j].Text = "Gross Salary";
                strRowIndx = j.ToString();
                j++;
            }
            if ((j - 4) - dtGrossSalHead.Rows.Count == inBenefitHeadCount + 1)
            {
                grv.HeaderRow.Cells[j].Text = "Total Benefits";
                strRowIndx = strRowIndx + "," + j.ToString();
                j++;
            }
            if ((j - 4) - dtGrossSalHead.Rows.Count  == inBenefitHeadCount + 2)
            {
                grv.HeaderRow.Cells[j].Text = "Total Salary";
                strRowIndx = strRowIndx + "," + j.ToString();
                j++;

                if (i < dtSalaryHead.Rows.Count)
                {
                    grv.HeaderRow.Cells[j].Text = dtSalaryHead.Rows[i]["SHORTNAME"].ToString();
                    //strRowIndx = strRowIndx + "," + j.ToString();
                    j++;
                }
            }
            else
            {
                if (i < dtSalaryHead.Rows.Count)
                {
                    grv.HeaderRow.Cells[j].Text = dtSalaryHead.Rows[i]["SHORTNAME"].ToString();
                    //strRowIndx = strRowIndx + "," + j.ToString();
                    j++;
                }
            }
        }
        grv.HeaderRow.Cells[j].Text = "Total";
        strRowIndx = strRowIndx + "," + j.ToString();
        j++;
        grv.HeaderRow.Cells[j].Text = "Net Salary";
        strRowIndx = strRowIndx + "," + j.ToString();
        this.HighlightGridViewCells(grv, strRowIndx);

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

    protected void HighlightGridViewCells(GridView grv, string strIndx)
    {
        string[] strArr = strIndx.Split(',');
        int indx=0;
        foreach (GridViewRow gRow in grv.Rows)
        {
            foreach (string str in strArr)
            {
                indx=Convert.ToInt32(str);
                gRow.Cells[indx].Font.Bold = true;
            }
        }
    }

    protected void GetSummaryTotal(GridView grv)
    {
        int i=0;
        decimal dclSumValue=0;
        grv.FooterRow.Cells[3].Text = "Total ";
        for (int j=4;j< dtPayrollSummary.Columns.Count;j++)
        {
            dclSumValue=0;
            i = 0;
            foreach (DataRow dRow in dtPayrollSummary.Rows)
            {
                dclSumValue=dclSumValue+ Convert.ToDecimal(dRow[j].ToString());
                if (Convert.ToInt64(dRow[j].ToString()) == 0)
                {
                    grv.Rows[i].Cells[j].Text = "";
                }
                grv.Rows[i].Cells[j].HorizontalAlign = HorizontalAlign.Right;
                i++;
            }
            if (dclSumValue == 0)
                grv.FooterRow.Cells[j].Text = "";
            else
                grv.FooterRow.Cells[j].Text = dclSumValue.ToString();
            grv.FooterRow.Cells[j].HorizontalAlign = HorizontalAlign.Right;
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
      // this.ValidateAndSave();
    }
 
    protected void btnAudited_Click(object sender, EventArgs e)
    {
        string strGenerateValue = "";
        if (grReviewList.Rows.Count <= 0)
        {
            lblMsg.Text = "No records to audit";
            return;
        }
        switch (ddlGeneratefor.SelectedValue.ToString())
        {
            case "B":
                strGenerateValue = ddlBank.SelectedValue.ToString();
                break;
           
        }

        dtEmpPayroll = objPayAppMgr.GetDistinctEmployeeForEndorcement(ddlGeneratefor.SelectedValue.ToString(), strGenerateValue,
            ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), ddlBank.SelectedValue.Trim(), "R");
        objPayAppMgr.UpdatePayslipMst(dtEmpPayroll, ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.Trim(), "C", Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()));
        lblMsg.Text = "Records has been Reviewed Successfully";
        this.GeneratePayrollReport();

        //Send Email
        //this.SendEmail();
    }

    private void SendEmail()
    {
        MasterTablesManager MasMgr = new MasterTablesManager();
        MailManagerSmtpClient objMail = new MailManagerSmtpClient();

        DataTable dt = MasMgr.GetEmailNotification();

        string strRetText = "";
        string strToAddr = dt.Rows[0]["Approval"].ToString().Trim();
        string strCC = dt.Rows[0]["Notify"].ToString().Trim() + ";" + dt.Rows[0]["Verify"].ToString().Trim();
        string strSubject = "Salary of " + ddlMonth.SelectedItem.ToString().Trim() + " has been waiting for your Approval";
        string strBody = "Hi,<br />The salary has been reviwed and found ok, you may now approve.";
        string strFromAddr = Session["EMAILID"].ToString().Trim();

        strRetText = objMail.PayslipEmail(strFromAddr, strToAddr, strSubject, strBody, strCC);

        if (strRetText == "Y")
            lblMsg.Text = lblMsg.Text + ", and Email has been sent to Approve by CR.";
        else
            lblMsg.Text = lblMsg.Text + ", Email sending failed.";
    }

    #region Movement Report
    //protected void GetMovementReport()
    //{
    //    lblBank.Text = ddlBank.SelectedItem.Text;
    //    objPayRptMgr.GetMovementReport(ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim(), ddlGeneratefor.SelectedValue.ToString(), ddlBank.SelectedValue.ToString(),
    //        grMovement, grMovementDetls, grValidation, lblMonthBetween);

    //}
    #endregion
}
