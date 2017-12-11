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
using System.Data.SqlClient;

public partial class Leave_AccruedVacationUpload : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();        
    //EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
    //MasterTablesManager objMasMgr = new MasterTablesManager();
    //ReportManager objRptMgr = new ReportManager();
    //FinancialLeaveMgr objFinLvMgr = new FinancialLeaveMgr();
    Payroll_MasterMgr objPayrollMgr = new Payroll_MasterMgr();
    LeaveManager objLvMgr = new LeaveManager(); 
    DataTable dtFinancialLv = new DataTable();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Common.FillMonthList(ddlMonth);
            Common.FillYearList(5, ddlYear);
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            //ddlMonth.SelectedValue = Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue) - 1);
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            Common.FillDropDownList(objPayrollMgr.SelectFiscalYear(0,"F"), ddlFiscalYear, "FISCALYRTITLE", "FISCALYRID", false);
            this.EntryMode();
        }
    }

    protected void EntryMode()
    {
                
    }

 protected void btnGenerate_Click(object sender, EventArgs e)
    {
        dtFinancialLv = objLvMgr.Get_AccruedVacation();
        grAccruedVacation.DataSource = dtFinancialLv;
        grAccruedVacation.DataBind();
        this.FormatAccruedVacation();
    }

 protected void FormatAccruedVacation()
 {
     int i = 1;
     decimal dclDailyRate = 0;
     decimal dclTotalAccruedV = 0;
     decimal dclTotalAccruedVUSD = 0;

     foreach (GridViewRow gRow in grAccruedVacation.Rows)
     {
         gRow.Cells[0].Text = i.ToString(); 
         if (gRow.Cells[3].Text.Trim() != "0.00")
             dclDailyRate = Math.Round(Convert.ToDecimal(gRow.Cells[4].Text.Trim())/ 22,0) ;//Convert.ToDecimal(gRow.Cells[3].Text.Trim()),2);
         else
             dclDailyRate = 0;

         gRow.Cells[5].Text=dclDailyRate.ToString() ;

         dclTotalAccruedV = Math.Round(Convert.ToDecimal(gRow.Cells[3].Text.Trim()) * dclDailyRate,2);
         gRow.Cells[6].Text = dclTotalAccruedV.ToString();

         dclTotalAccruedVUSD = Math.Round(dclTotalAccruedV / Convert.ToDecimal(Session["USDRATE"].ToString()),2);
         gRow.Cells[7].Text = dclTotalAccruedVUSD.ToString();

         i++;
     }
 }

    //private string GetLeaveEnjoyed(string strEmpId, Int32 VMonth, Int32 VYear)
    //{
    //    string strSQL = "SELECT SUM(L.LDurInDays) FROM LeavAppMst L JOIN LeavAppDets LD ON L.LvAppId=LD.LvAppId"
    //        + " WHERE L.AppStatus='A' AND LD.LType=1 AND EmpId ='" + strEmpId + "' AND MONTH(LD.LeaveStart) =" + VMonth
    //        + " AND MONTH(LD.LeaveEnd) =" + VMonth
    //        + " AND YEAR(LD.LeaveStart) =" + VYear + " AND YEAR(LD.LeaveEnd) =" + VYear;

    //    SqlCommand command = new SqlCommand(strSQL);
    //    command.CommandType = CommandType.Text;

    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = strEmpId;

    //    SqlParameter p_VMonth = command.Parameters.Add("VMonth", SqlDbType.BigInt);
    //    p_VMonth.Direction = ParameterDirection.Input;
    //    p_VMonth.Value = VMonth;

    //    SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.BigInt);
    //    p_VYear.Direction = ParameterDirection.Input;
    //    p_VYear.Value = VYear;

    //    return objDB.GetScalarVal(command);
    //}

    //private string GetPreviousMonthPayDeduct(string strEmpId, Int32 iPrevMonth, Int32 iYear)
    //{
    //    string strSQL = "SELECT PayDeduct FROM FinancialLeaveList"
    //            + " WHERE EmpId ='" + strEmpId + "' AND VMonth=" + iPrevMonth + " AND VYear=" + iYear;

    //    SqlCommand command = new SqlCommand(strSQL);
    //    command.CommandType = CommandType.Text;

    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = strEmpId;

    //    SqlParameter p_VMonth = command.Parameters.Add("VMonth", SqlDbType.BigInt);
    //    p_VMonth.Direction = ParameterDirection.Input;
    //    p_VMonth.Value = iPrevMonth;

    //    SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.BigInt);
    //    p_VYear.Direction = ParameterDirection.Input;
    //    p_VYear.Value = iYear;

    //    return objDB.GetScalarVal(command);
    //}

 protected void btnSave_Click(object sender, EventArgs e)
 {
     if (ValidateAndSave() == true)
     {
         this.SaveData();
     }
 }

 private bool ValidateAndSave()
 {
     try
     {
         //if (objLvMgr.CheckForMultipleEntry(ddlLType.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString()) == true)
         //{
         //    lblMsg.Text = "Leave already accrued for '" + ddlLType.SelectedItem.Text.Trim() + "' of the month " + ddlMonth.SelectedItem.Text.Trim() + ", " + ddlYear.SelectedItem.Text.Trim();
         //    return false; 
         //}
         return true;
     }
     catch (Exception ex)
     {
         lblMsg.Text = "";
         throw (ex);
     }
 }

 private void SaveData()
 {
     objLvMgr.InsertAccruedVacationList(grAccruedVacation, ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(),
         ddlFiscalYear.SelectedValue.ToString(), Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));

     lblMsg.Text = "Monthly Accrued Vacation has been uploaded successfully.";
     this.EntryMode();
 } 
}
