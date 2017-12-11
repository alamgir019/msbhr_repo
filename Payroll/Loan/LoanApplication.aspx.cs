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

public partial class Payroll_Loan_LoanApplication : System.Web.UI.Page
{
    Payroll_MasterMgr objMasMgr = new Payroll_MasterMgr();
    Payroll_LoanAppManager objLoanAppMgr = new Payroll_LoanAppManager();
    dsPayroll_Loan dsLoan=new dsPayroll_Loan();
    bool IsSalaryAdvanceSelected=false;
    DataTable dtSalaryAdvance=new DataTable();
    DataTable dtLoanMst=new DataTable();
    DataTable dtLoanDet=new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillDropDownList(objMasMgr.SelectLoanType(0), ddlLoanType, "LOANTYPENAME", "LOANTYPEID", true, "Select");
        }
    }

    protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        this.SearchEmployee();
    }

    protected void SearchEmployee()
    {
        if (string.IsNullOrEmpty(txtEmpCode.Text.Trim()) == false)
        {
            //grLeaveStatus.DataSource = null;
            //grLeaveStatus.DataBind();
            this.FillEmpInfoForLoan(txtEmpCode.Text.Trim());
            this.FillAdvanceList(txtEmpCode.Text.Trim());
        }
        else
        {
            //this.EntryMode(false);
        }
    }

    protected void FillEmpInfoForLoan(string strEmpId)
    {
        DataTable dtEmp = objLoanAppMgr.GetEmployeeInfoforLoan(strEmpId);
        if (dtEmp.Rows.Count > 0)
        {
            lblEmpName.Text = dtEmp.Rows[0]["FullName"].ToString().Trim();
            lblJoinDate.Text = string.IsNullOrEmpty(dtEmp.Rows[0]["JoiningDate"].ToString().Trim()) == false ? Common.DisplayDate(dtEmp.Rows[0]["JoiningDate"].ToString().Trim()) : "";
            lblOffice.Text = dtEmp.Rows[0]["DivisionName"].ToString().Trim();
            lblDesignation.Text = dtEmp.Rows[0]["JobTitle"].ToString().Trim();
            lblGrossSalary.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtEmp.Rows[0]["TOTALGROSSSAL"].ToString().Trim()), 2));
            lblLoanDue.Text = this.GetCurrentMonthDue(strEmpId);
        }
        else
        {
            lblEmpName.Text = "";
            lblJoinDate.Text = "";
            lblOffice.Text = "";
            lblDesignation.Text = "";
        }
    }

    protected string GetCurrentMonthDue(string strEmpId)
    {
        string strLoanOpenMinusRefund="";
        string strLoanDetail="";
        string strLoanRecovered="";
        decimal decLoanOpenMinusRefund=0;
        decimal decLoanDetail=0;
        decimal decLoanRecovered=0;
        decimal decCurrentMonthDue=0;
        strLoanOpenMinusRefund=objLoanAppMgr.GetLoanOpening(strEmpId);
        decLoanOpenMinusRefund=string.IsNullOrEmpty(strLoanOpenMinusRefund)==false?Convert.ToDecimal(strLoanOpenMinusRefund):0;

        strLoanDetail=objLoanAppMgr.GetLoanDetail(strEmpId);
        decLoanDetail=decLoanOpenMinusRefund=string.IsNullOrEmpty(strLoanDetail)==false?Convert.ToDecimal(strLoanDetail):0;

         strLoanRecovered =objLoanAppMgr.GetLoanRecovered(strEmpId);
         decLoanRecovered=decLoanOpenMinusRefund=string.IsNullOrEmpty(strLoanRecovered)==false?Convert.ToDecimal(strLoanRecovered):0;

        decCurrentMonthDue=decLoanOpenMinusRefund+decLoanDetail+decLoanRecovered;
        decCurrentMonthDue=Math.Round(decCurrentMonthDue,2);
        
        return Convert.ToString(decCurrentMonthDue);
    }

    protected bool SalaryAdvanceSelected()
    {
       dtSalaryAdvance=objLoanAppMgr.GetSalaryAdvance(ddlLoanType.SelectedValue.ToString());
        if(dtSalaryAdvance.Rows.Count>0)
        {
           return true;
        }
        else
        {
            return false;
        }
    }
  

protected void  ddlLoanType_SelectedIndexChanged(object sender, EventArgs e)
{
    IsSalaryAdvanceSelected = this.SalaryAdvanceSelected();
    if (IsSalaryAdvanceSelected == false)
    {
        txtTotalLoan.Text = lblLoanDue.Text;
        hfPrevLoanAmt.Value = lblLoanDue.Text; 
    }
    else
    {
        txtTotalLoan.Text = "0";
        hfPrevLoanAmt.Value = "0";
    }
    
}
    protected void FillAdvanceList(string strEmpID)
    {
        DataTable dtAdvanceList=objLoanAppMgr.GetAdvanceList(strEmpID);
        if(dtAdvanceList.Rows.Count>0)
        {
            grAdvList.DataSource=dtAdvanceList;
            grAdvList.DataBind();
        }
        else
        {
            grAdvList.DataSource=null;
            grAdvList.DataBind();
        }
        dtAdvanceList.Rows.Clear();
        dtAdvanceList.Dispose();
    }

    
    protected void OpenRecord(string strAppId)
    {
        dtLoanMst=objLoanAppMgr.GetLoanMasterRecord(strAppId);
        dtLoanDet=objLoanAppMgr.getLoanDetailsRecord(strAppId);
    }

    protected void RetriveRecord(string strAppNo)
    {
        this.OpenRecord(strAppNo);
        decimal dclLoanAmt=0;
        decimal dclInstRate=0;
        decimal dclInstAmt=0;
        decimal dclNetPayAmt=0;
        decimal dclPayAmt=0;
        if(dtLoanMst.Rows.Count>0)
        {
            hfLoanAppID.Value=dtLoanMst.Rows[0]["LOANAPPID"].ToString().Trim();
            txtReqDate.Text=Common.DisplayDate(dtLoanMst.Rows[0]["REQDATE"].ToString().Trim());
            txtAppDate.Text=Common.DisplayDate(dtLoanMst.Rows[0]["APPDATE"].ToString().Trim());
            ddlLoanType.SelectedValue=dtLoanMst.Rows[0]["LOANTYPEID"].ToString().Trim();

            dclLoanAmt=Convert.ToDecimal(dtLoanMst.Rows[0]["LOANAMNT"].ToString().Trim());
            dclLoanAmt=Math.Round(dclLoanAmt,2);
            txtTotalLoan.Text=dclLoanAmt.ToString();
            
            dclInstRate=Convert.ToDecimal(dtLoanMst.Rows[0]["PFInterest"].ToString().Trim());
            dclInstRate=Math.Round(dclInstRate,2);
            txtInterestRate.Text=dclInstRate.ToString();

            dclInstAmt=dclInstRate*dclLoanAmt/100;
            dclInstAmt=Math.Round(dclInstAmt,2);
            txtInterestAmt.Text=dclInstAmt.ToString();

            dclNetPayAmt=dclLoanAmt+dclInstAmt;
            dclNetPayAmt=Math.Round(dclNetPayAmt,2);
            txtNetPayAmt.Text=dclNetPayAmt.ToString();
            foreach(DataRow dRow in dtLoanDet.Rows)
            {
                DataRow nRow=dsLoan.dtLoanRefund.NewRow();
                nRow["LOANAPPID"]=dtLoanMst.Rows[0]["LOANAPPID"];
                nRow["PAYDATE"]=Common.DisplayDate(dRow["PAYDATE"].ToString());
                dclPayAmt=dclPayAmt+Convert.ToDecimal(dRow["PAYAMT"].ToString());
                nRow["PAYAMNT"] = Convert.ToString(Math.Round(Convert.ToDecimal(dRow["PAYAMNT"].ToString()), 2));
                dsLoan.dtLoanRefund.Rows.Add(nRow);
            }
            dsLoan.dtLoanRefund.AcceptChanges();
            txtTotRefundAmt.Text=Convert.ToString(Math.Round(dclPayAmt,2));
            grLoanRefund.DataSource = dsLoan.Tables["dtLoanRefund"];
            grLoanRefund.DataBind();
        }
       
    }



    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        DateTime dtAppDate = Convert.ToDateTime(Common.ReturnDate(txtReqDate.Text.Trim()));
        decimal dclInstAmt = Convert.ToDecimal(txtInstAmt.Text.Trim());
        int inNoofInst = Convert.ToInt32(txtInstNo.Text.Trim());
      

        for (int i = 0; i < inNoofInst; i++)
        {
            DataRow nRow = dsLoan.dtLoanRefund.NewRow();
            nRow["LOANAPPID"] = Convert.ToString(i + 1);
            nRow["PAYDATE"] = Common.DisplayDate(dtAppDate.AddMonths(i).ToShortDateString());
            nRow["PAYAMNT"] = Convert.ToString(Math.Round(dclInstAmt,2));
            dsLoan.dtLoanRefund.Rows.Add(nRow);
        }
        dsLoan.dtLoanRefund.AcceptChanges();
        txtTotRefundAmt.Text = txtNetPayAmt.Text;
        grLoanRefund.DataSource = dsLoan.Tables["dtLoanRefund"];
        grLoanRefund.DataBind();
    }

       
    protected void AddRefundData()
    {
        if (txtTotRefundAmt.Text == "")
            txtTotRefundAmt.Text = "0";
        int i = 0;
        decimal dclPayAmt = 0;
        if (Convert.ToDecimal(txtNetPayAmt.Text.Trim()) >= Convert.ToDecimal(txtTotRefundAmt.Text.Trim()) + Convert.ToDecimal(txtRefAmount.Text.Trim()))
        {
            foreach (GridViewRow gRow in grLoanRefund.Rows)
            {
                DataRow nRow = dsLoan.dtLoanRefund.NewRow();
                nRow["LOANAPPID"] = Convert.ToString(i + 1);
                nRow["PAYDATE"] = gRow.Cells[0].Text;
                nRow["PAYAMNT"] = Convert.ToString(Math.Round(Convert.ToDecimal(gRow.Cells[1].Text), 2));
                dsLoan.dtLoanRefund.Rows.Add(nRow);
                
                dsLoan.dtLoanRefund.AcceptChanges();
                dclPayAmt = dclPayAmt + Convert.ToDecimal(gRow.Cells[1].Text);
                i++;
            }

            DataRow nRow2 = dsLoan.dtLoanRefund.NewRow();
            nRow2["LOANAPPID"] = Convert.ToString(grLoanRefund.Rows.Count + 1);
            nRow2["PAYDATE"] = txtRefDate.Text;
            nRow2["PAYAMNT"] = Convert.ToString(Math.Round(Convert.ToDecimal(txtRefAmount.Text.Trim()), 2));
            dsLoan.dtLoanRefund.Rows.Add(nRow2);

            dsLoan.dtLoanRefund.AcceptChanges();
            dclPayAmt = dclPayAmt + Convert.ToDecimal(txtRefAmount.Text.Trim());

            txtTotRefundAmt.Text = dclPayAmt.ToString();

            grLoanRefund.DataSource = dsLoan.Tables["dtLoanRefund"];
            grLoanRefund.DataBind();
        }
    }

   
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.AddRefundData();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        grLoanRefund.DataSource = null;
        grLoanRefund.DataBind();
        txtTotRefundAmt.Text = "0";
    }
}
