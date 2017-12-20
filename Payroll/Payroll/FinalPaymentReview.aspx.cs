using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Payroll_Payroll_FinalPaymentReview : System.Web.UI.Page
{
    DataTable dtEmpInfo = new DataTable();
    DataTable dtTempDuty = new DataTable();

    MasterTablesManager objMasMgr = new MasterTablesManager();
    EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
    LeaveManager objLvMgr = new LeaveManager();
    Payroll_MasterMgr objPayMgr = new Payroll_MasterMgr();
    Payroll_PFManager objPFMgr = new Payroll_PFManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.SelectedValue = Convert.ToString(DateTime.Today.Month);
            ddlYear.SelectedValue = Convert.ToString(DateTime.Today.Year);
            Common.FillDropDownList_All(objMasMgr.SelectClinic(), ddlBank);
            //Common.FillDropDownList(objMastMg.SelectLocation(0), ddlGenerateValue, "LocationName", "LocationID", false);
            //Common.FillDropDownList(objPayrollMgr.SelectBankAndBranchList(), ddlBank, "BANKBRANCH", "RoutingNo", true, "Nil");
            //Common.FillDropDownList(objMastMg.SelectEmpGroup(0), ddlGroup, "GrpName", "EmpGrpID", false);
            
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        string strGenerateValue = "";
        int inBenefitHeadCount = 0;
        int inDeductCount = 0;

        switch (ddlGeneratefor.SelectedValue.ToString())
        {
            case "O":
                strGenerateValue = ddlBank.SelectedValue.ToString();                
                break;
            case "B":
                strGenerateValue = ddlBank.SelectedValue.ToString();              
                break;
        }
              
        

      
        // Prepared Data
        //dtEmpPayroll = objEmpInfoMgr.SelectEmpFinalPayment(ddlGeneratefor.SelectedValue.ToString(), strGenerateValue,
        //    ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), ddlBank.SelectedValue.Trim());


        //dt = objEmpInfoMgr.SelectEmpFinalPayment(txtEmpID.Text.Trim());
        //grList.DataSource = dt;
        //grList.DataBind();
        //if (grList.Rows.Count > 0)
        //{
        //    btnSave.Enabled = false;
        //    foreach (GridViewRow gRow in grList.Rows)
        //    {
        //        if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[9].Text)) == false)
        //            gRow.Cells[9].Text = Common.DisplayDate(gRow.Cells[9].Text);

        //        string diff = Common.CalculateYearMonthDay(gRow.Cells[9].Text, 1);
        //        if (Convert.ToInt32(diff) < 6)
        //        {
        //            btnSave.Enabled = true;
        //            EntryMode(true);
        //            hfId.Value = grList.DataKeys[0].Values[0].ToString();
        //        }
        //    }
        //}
        //else
        //{
        //    btnSave.Enabled = true;
        //}
    }

    private void OpenRecord()
    {
        DataTable dt = new DataTable();
        // dt= objEmpInfoMgr.SelectEmpInfoHRAction(txtEmpID.Text.Trim());
        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //if (ValidateAndSave("Save") == false)
        //{
        //    return;
        //}

        //if (hfIsUpdate.Value == "N")
        //{
        //    this.SaveData("I");
        //}
        //else
        //{
        //    this.SaveData("U");
        //}
    }
    //protected bool ValidateAndSave(string Flag)
    //{
    //    //try
    //    //{
    //    //    if (string.IsNullOrEmpty(txtProcessDate.Text) == true)
    //    //    {
    //    //        lblMsg.Text = "Please enter process date.";
    //    //        txtProcessDate.Focus();
    //    //        return false;
    //    //    }
            
    //    //    return true;
    //    //}
    //    //catch (Exception ex)
    //    //{
    //    //    lblMsg.Text = "";
    //    //    throw (ex);
    //    //}
    //}

    private void ClearControls()
    {
        //txtEmpID.Text = "";
        //txtBasicPay.Text = "";
        //txtTotalPay.Text = "";
        //txtLeaveEncash.Text = "";
        //txtPF.Text = "";
        //txtGratuity.Text = "";
        //txtLastMonthSalary.Text = ""; 
        //txtTripAdvPay.Text = "";
        //txtAlreadyPay.Text = "";
        //txtOtherPay.Text = "";
        //txtPFLoan.Text = "";
        //txtOther.Text = "";
        //txtNetPay.Text = "";
        //txtProcessDate.Text = DateTime.Now.ToShortDateString();
        //txtRemarks.Text = "";
    }

    //private void SaveData(string cmdType)
    //{
    //    dsPayroll objDS = new dsPayroll();
    //    if (cmdType == "I")
    //    {
    //        hfId.Value = Common.getMaxId("FinalPayment", "FinalPayId");
    //    }
      
    //    DataTable dtMst = objDS.Tables["FinalPayment"];
    //    DataRow nRow = dtMst.NewRow();

    //    nRow["FinalPayId"] = Common.RoundDecimal(hfId.Value, 0);
    //    nRow["EmpID"] = txtEmpID.Text.Trim();
    //    nRow["TotServiceYr"] = lblServiceYr.Text.Trim() == "" ? "0" : lblServiceYr.Text.Trim();
    //    nRow["TotalPay"] = txtTotalPay.Text.Trim() == "" ? "0" : txtTotalPay.Text.Trim();
    //    nRow["ELBalance"] = lblLeave.Text.Trim() == "" ? "0" : lblLeave.Text.Trim();
    //    nRow["LeaveEncash"] = txtLeaveEncash.Text.Trim() == "" ? "0" : txtLeaveEncash.Text.Trim();
    //    nRow["PF"] = txtPF.Text.Trim() == "" ? "0" : txtPF.Text.Trim();
    //    nRow["Gratuity"] = txtGratuity.Text.Trim() == "" ? "0" : txtGratuity.Text.Trim();
    //    nRow["TripAdvPay"] = txtTripAdvPay.Text.Trim() == "" ? "0" : txtTripAdvPay.Text.Trim();
    //    nRow["AlreadyPay"] = txtAlreadyPay.Text.Trim() == "" ? "0" : txtAlreadyPay.Text.Trim();
    //    nRow["OtherPay"] = txtOtherPay.Text.Trim() == "" ? "0" : txtOtherPay.Text.Trim();
    //    nRow["PFLoan"] = txtPFLoan.Text.Trim() == "" ? "0" : txtPFLoan.Text.Trim();
    //    nRow["Other"] = txtOther.Text.Trim() == "" ? "0" : txtOther.Text.Trim();
    //    nRow["NetPay"] = txtNetPay.Text.Trim() == "" ? "0" : txtNetPay.Text.Trim();
    //    nRow["ProcessDate"] = Common.ReturnDate(txtProcessDate.Text.Trim());      
    //    nRow["Remarks"] = txtRemarks.Text.ToString().Trim();

    //    if (cmdType == "I")
    //    {
    //        nRow["InsertedBy"] = Session["USERID"].ToString().Trim();
    //        nRow["InsertedDate"] = DateTime.Now;
    //    }
    //    else if (cmdType == "U")
    //    {
    //        nRow["UpdatedBy"] = Session["USERID"].ToString().Trim();
    //        nRow["UpdatedDate"] = DateTime.Now;
    //    }
    //    dtMst.Rows.Add(nRow);
    //    dtMst.AcceptChanges();

    //   try
    //    {
    //        objPayMgr.SaveData(dtMst, cmdType == "D" ? "U" : cmdType);
    //        lblMsg.Text = Common.GetMessage(cmdType);
    //        Common.EmptyTextBoxValues(this);
    //        this.EntryMode(false);
    //        this.OpenRecord();
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = ex.ToString();
    //    }
    //}    
}