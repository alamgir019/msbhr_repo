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
using System.Data.OleDb;
using System.Data.SqlClient;

public partial class File_ImportTool : System.Web.UI.Page
{
    MasterTablesManager objMasMgr = new MasterTablesManager();
    dsPayroll_Payslip objPayslip = new dsPayroll_Payslip();
    Payroll_PreparationManager objPreMgr = new Payroll_PreparationManager();
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    DBConnector objDC = new DBConnector();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            //Common.FillDropDownList(objMasMgr.SelectEmpInfoDesignation(), ddlDesig,"PAYEMPID","DESGID",false);
            //Common.FillDropDownList(objMasMgr.SelectDepartmentddl2(0), ddlProject, "DEPTCODE", "DEPTID", false);
            //Common.FillDropDownList(objMasMgr.SelectDivisionddl(0), ddlDivision, "DIVISIONNAME", "DIVISIONID", false);
            //Common.FillDropDownList(objMasMgr.SelectLocation(0), ddlLocation, "LOCATIONNAME", "LOCATIONID", false);
            //Common.FillDropDownList(objEmpMgr.SelectAllBranchList(), ddlBank, "RoutingNo", "BankCode", false);
            //Common.FillDropDownList(this.GetTeamOfficeData(), ddlLocationExcel, "Name", "OfficeCode", false);
        }
    }

    protected DataTable GetTeamOfficeData()
    {
        string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=E:\\tOffice.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        return ds.Tables[0];
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {

    }

    protected void btnSync_Click(object sender, EventArgs e)
    {
        DataTable dtEmpInfo = objEmpMgr.SelectEmployeeAllInfo("");
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            //for (int i = 0; i < dtEmpInfo.Rows.Count; i++)
            //{
            //    if(gRow.Cells[0].Text.Trim())
            //}
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        long lngPayID = 0;
        long lngPayBookID = 1;
        string PayStartDate = "";
        string PayEndDate = "";
        int inMonthDays = 0;
        decimal dclEmpNetPayAmt = 0;
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            PayStartDate = "2011/11/01";
            PayEndDate = "2011/11/31";
            inMonthDays = 31;
            //STEP 1
            lngPayID = lngPayID + 1;

            dclEmpNetPayAmt = 0;
            // Basic
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "1",
                                       "Basic Salary", "Y", gRow.Cells[6].Text.Trim(),
                                       "N", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "N", "N");

            // Basic Arrear
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "19",
                                       "Basic Arrear", "N", gRow.Cells[7].Text.Trim(),
                                       "N", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "N", "Y");


            // House Rent
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "2",
                                       "", "N", gRow.Cells[8].Text.Trim(),
                                       "N", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "N", "N");

            // House Rent Arrear
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "20",
                                       "", "N", gRow.Cells[9].Text.Trim(),
                                       "N", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "N", "Y");
            // Medical
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "3",
                                       "", "N", gRow.Cells[10].Text.Trim(),
                                       "N", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "N", "N");
            // Medical Arrear
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "21",
                                       "", "N", gRow.Cells[11].Text.Trim(),
                                       "N", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "N", "Y");

            // TransPort
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "4",
                                       "", "N", gRow.Cells[12].Text.Trim(),
                                       "N", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "N", "N");
            // TransPort Arrear
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "22",
                                       "", "N", gRow.Cells[13].Text.Trim(),
                                       "N", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "N", "Y");

            // Field
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "7",
                                       "", "N", gRow.Cells[16].Text.Trim(),
                                       "N", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "N", "N");
            // Field Arrear
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "23",
                                       "", "N", gRow.Cells[17].Text.Trim(),
                                       "N", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "N", "Y");
            // Other Allow
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "8",
                                       "", "N", gRow.Cells[18].Text.Trim(),
                                       "N", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "N", "N");

            // Advance Adjustment
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "26",
                                       "", "N", gRow.Cells[19].Text.Trim(),
                                       "Y", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "N", "N");

            // Revenue Stamp
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "17",
                                       "", "N", gRow.Cells[20].Text.Trim(),
                                       "Y", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "N", "N");
            // Other Deduction
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "18",
                                       "", "N", gRow.Cells[21].Text.Trim(),
                                       "Y", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "N", "N");

            // PF Dedcution
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "9",
                                       "", "N", gRow.Cells[22].Text.Trim(),
                                       "Y", "N", "N", "N", "Y", "0.00",
                                       "N", "N", "N", "0", "N", "N", "N");
            // PF Dedcution Arrear
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "24",
                                       "", "N", gRow.Cells[23].Text.Trim(),
                                       "Y", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "N", "Y");

            // PF Loan
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "10",
                                       "", "N", gRow.Cells[24].Text.Trim(),
                                       "Y", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "N", "N");
            // PF Interest
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "11",
                                       "", "N", gRow.Cells[25].Text.Trim(),
                                       "Y", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "N", "N");

            // PF Loan Charge
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "27",
                                       "", "N", gRow.Cells[26].Text.Trim(),
                                       "Y", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "N", "N");

            // CU Share
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "12",
                                       "", "N", gRow.Cells[27].Text.Trim(),
                                       "Y", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "N", "N");

            // CU Loan
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "13",
                                       "", "N", gRow.Cells[28].Text.Trim(),
                                       "Y", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "N", "N");

            // CU Interest
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "14",
                                       "", "N", gRow.Cells[29].Text.Trim(),
                                       "Y", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "N", "N");

            // CU Loan Charge
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "28",
                                       "", "N", gRow.Cells[30].Text.Trim(),
                                       "Y", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "N", "N");

            // Income Tax
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "15",
                                       "", "N", gRow.Cells[31].Text.Trim(),
                                       "Y", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "N", "N");

            // Income Tax Ass
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "16",
                                       "", "N", gRow.Cells[32].Text.Trim(),
                                       "Y", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "N", "N");
            // Festival Bonus
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "6",
                                       "", "N", gRow.Cells[33].Text.Trim(),
                                       "N", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "Y", "N");

            // SER Bonus
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "29",
                                       "", "N", gRow.Cells[34].Text.Trim(),
                                       "N", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "Y", "N");


            // LWOP
            this.AddSalPackDets(lngPayBookID.ToString(), gRow.Cells[0].Text.Trim(), "25",
                                       "", "N", gRow.Cells[35].Text.Trim(),
                                       "Y", "N", "N", "N", "N", "0.00",
                                       "N", "N", "N", "0", "N", "N", "N");

            // Calculate Net Pay Amt
            // dclEmpNetPayAmt = dclEmpNetPayAmt + dclSalHeadAmount;

            // Loop End Here
            // Insert Data into Salary Pack master Table of Dataset
            this.AddSalPackMst(lngPayBookID.ToString(), lngPayID.ToString(), gRow.Cells[0].Text.Trim(), gRow.Cells[1].Text.Trim(),
                gRow.Cells[2].Text.Trim(), PayStartDate, PayEndDate, "P", gRow.Cells[0].Text.Trim(), "3",
                objPayslip.dtPaySlipDets, "N", "0", inMonthDays.ToString(), gRow.Cells[3].Text.Trim(), "0",
                "0", "0", "N", "N", "0",
                "N", "0", "0", "0",
                "N", "0", "N", "0",
                "0", "0", "Y", gRow.Cells[6].Text.Trim(),
                "0", "0.00", "", PayStartDate, PayEndDate,
                inMonthDays.ToString(),
                gRow.Cells[36].Text.Trim(), gRow.Cells[41].Text.Trim(), gRow.Cells[37].Text.Trim(),
                gRow.Cells[38].Text.Trim(), gRow.Cells[3].Text.Trim(), gRow.Cells[2].Text.Trim(),
                "1", gRow.Cells[4].Text.Trim(), gRow.Cells[5].Text.Trim(), gRow.Cells[42].Text.Trim());
        }

        this.SaveData();

    }



    protected void AddSalPackDets(string strPaySlipBookID, string strEmpID, string strSalPkHeadId,
                                     string strSalHeadName, string strIsBasicSal, string strSalHeadAmt,
                                     string strIsDeduct, string strIsOthPayment, string strIsAdvancDeduct,
                                     string strIsLateDeduct, string strIsPF, string strPFAmt,
                                     string strIsAttndBonus, string strIsProdBonus, string strIsOT,
                                     string strOTHour, string strPfLoanDeduct, string strIsFestivalBonus, string strIsArea)
    {
        DataRow nRow = objPayslip.dtPaySlipDets.NewRow();
        nRow["PSBookID"] = strPaySlipBookID;
        nRow["EmployeeID"] = strEmpID;
        nRow["SalHeadID"] = strSalPkHeadId;
        nRow["SalHeadTitle"] = strSalHeadName;
        nRow["IsBasicSal"] = strIsBasicSal;
        if (strIsDeduct == "Y")
            strSalHeadAmt = Convert.ToString(Convert.ToDecimal(strSalHeadAmt) * -1);
        nRow["PayAmnt"] = strSalHeadAmt;
        nRow["IsDeducted"] = strIsDeduct;
        nRow["IsOtherPayment"] = strIsOthPayment;
        nRow["IsAdvanceDeducttion"] = strIsAdvancDeduct;
        nRow["IsLateDeduction"] = strIsLateDeduct;
        nRow["IsProvidentFund"] = strIsPF;
        nRow["PFAmount"] = Common.RoundDecimal(strPFAmt, 0).ToString();
        nRow["IsAttndBonus"] = strIsAttndBonus;
        nRow["IsProductionBonus"] = strIsProdBonus;
        nRow["IsOT"] = strIsOT;
        nRow["OTHour"] = strOTHour;
        nRow["IsPFLoanDeduction"] = strPfLoanDeduct;
        nRow["IsFestivalBonus"] = strIsFestivalBonus;
        nRow["IsArea"] = strIsArea;
        objPayslip.dtPaySlipDets.Rows.Add(nRow);
        objPayslip.dtPaySlipDets.AcceptChanges();
    }
    protected void AddSalPackMst(string strPaySlipBookID, string strPayID, string strEmpID, string strEmpName, string strJobTitle,
      string strStartDate, string strEndDate, string strPayslipStatus, string strSalPakId, string strSalPayType, DataTable dtSalPackDetailsTmp,
      string strIsAdjusting, string strPackageAmt, string TWorkingDayHour, string strDeptName, string strLateDayCount, string strLateSalDeductCount,
      string strLateSalHeadID, string strIsWithBonus, string strIsAllowedForAttBonus, string strGrossAmount, string strIsIrregular, string strOTAmt,
      string strOTIsInPercent, string strOTSalHead, string strIsOnlyBonus, string strBonusPackID, string strIsConvertCurrency, string strCurrencyID,
      string strCurrencyConvAmnt, string strAreaAmount, string strIsAreaPaid, string strBasicAmount, string strLateDedASOTDEDHouR,
      string strLateDedASOTDEDAMt, string strSPTitle, string strSalStartDate, string strSalEndDate, string strSalMonthDays,
      string strLocID, string strBankCode, string strBranchCode, string strAccNo, string strDeptId, string strDesgId, string strEmpTypeId, string strDivID,
      string strEmpStatus, string strHrEmpID)
    {
        DataRow nRow = objPayslip.dtPaySlipMst.NewRow();
        nRow["PSBookID"] = strPaySlipBookID;
        nRow["PayID"] = strPayID;
        nRow["EmployeeID"] = strEmpID;
        nRow["Empname"] = strEmpName;
        nRow["JobTitle"] = strJobTitle;
        nRow["StartDate"] = strSalStartDate;
        nRow["EndDate"] = strSalEndDate;
        nRow["PaySlipSatus"] = strPayslipStatus;
        nRow["SalPackID"] = strSalPakId;
        nRow["SalPayType"] = strSalPayType;
        nRow["isAdjusting"] = strIsAdjusting;
        nRow["PackageAmount"] = Common.RoundDecimal(strPackageAmt, 0);
        nRow["TWorkingDayHour"] = TWorkingDayHour;
        nRow["DeptName"] = strDeptName;
        nRow["LateDayCount"] = strLateDayCount;
        nRow["LateSalDeductCount"] = strLateSalDeductCount;
        nRow["LateSalHeadID"] = strLateSalHeadID;
        nRow["IsWithBonus"] = strIsWithBonus;
        nRow["IsAllowedForAttBonus"] = strIsAllowedForAttBonus;

        nRow["GrossAmount"] = Common.RoundDecimal(strGrossAmount, 0);
        nRow["IsIrregular"] = strIsIrregular;
        nRow["OTAmnt"] = strOTAmt;
        nRow["OTIsInPercent"] = strOTIsInPercent;
        nRow["OTSalHead"] = strOTSalHead;
        nRow["IsOnlyBonus"] = strIsOnlyBonus;
        nRow["BonusPackID"] = strBonusPackID;
        nRow["IsConvertCurrency"] = strIsConvertCurrency;
        nRow["CurrencyID"] = strCurrencyID;
        nRow["CurrencyConvAmnt"] = strCurrencyConvAmnt;
        nRow["AreaAmount"] = strAreaAmount;
        nRow["IsAreaPaid"] = strIsAreaPaid;
        nRow["LateDedASOTDEDHouR"] = strLateDedASOTDEDHouR;
        nRow["LateDedASOTDEDAMt"] = strLateDedASOTDEDAMt;
        nRow["SPTitle"] = strSPTitle;

        nRow["SalStartDate"] = strSalStartDate;
        nRow["SalEndDate"] = strSalEndDate;
        nRow["MonthDays"] = strSalMonthDays;

        nRow["LOCID"] = strLocID;
        nRow["BANKCODE"] = strBankCode;
        nRow["BRANCHCODE"] = strBranchCode;
        nRow["BankAccNo"] = strAccNo;
        nRow["DEPTID"] = strDeptId;
        nRow["DESGID"] = strDesgId;
        nRow["EMPTYPEID"] = strEmpTypeId;
        nRow["DIVISIONID"] = strDivID;
        nRow["EmpStatus"] = strEmpStatus;
        nRow["HREmpID"] = strHrEmpID;

        objPayslip.dtPaySlipMst.Rows.Add(nRow);
        objPayslip.dtPaySlipMst.AcceptChanges();
    }

    protected void SaveData()
    {
        try
        {

        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }
    }

    public void UpdateEmpInfo(string EmpId, string AccountNo, string Email)
    {
        string strSQL = "UPDATE EMPINFO SET BankAccNo = @BankAccNo,PersEmail1 = @PersEmail1 WHERE EmpId = @EmpId";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_InsertedDate = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = EmpId;

        SqlParameter p_DirName = command.Parameters.Add("BankAccNo", SqlDbType.VarChar);
        p_DirName.Direction = ParameterDirection.Input;
        p_DirName.Value = AccountNo;

        SqlParameter p_InsertedBy = command.Parameters.Add("PersEmail1", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = Email;

        objDC.ExecuteQuery(command);
    }
    protected void btnUploadSalaryPackageTitle_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=C:\\BASESOFT\\Increment 2017.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [SalaryPakHisDetls$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();
    }
    protected void btnUpdateFlag_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            this.UpdateSalaryPackage(gRow.Cells[0].Text.Trim(), Common.CheckNullString(gRow.Cells[1].Text.Trim()));
        }
        lblMsg.Text = "Record Upadated Successfully";
    }

    public void UpdateSalaryPackage(string EmpId, string SPTitle)
    {
        string strSQL = "UPDATE SalaryPakMst SET SPTitle = @SPTitle WHERE EmpId = @EmpId";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_InsertedDate = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = EmpId;

        SqlParameter p_SPTitle = command.Parameters.Add("SPTitle", SqlDbType.VarChar);
        p_SPTitle.Direction = ParameterDirection.Input;
        p_SPTitle.Value = SPTitle;

        objDC.ExecuteQuery(command);
    }
    protected void btnSalaryAmt_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\\SCBUploadTool\\MSB\\Salary Data July 2018.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();
    }
    protected void btnUpdateFlag1_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            this.UpdateSalaryPackageAmt(gRow.Cells[0].Text.Trim(), Common.CheckNullString(gRow.Cells[1].Text.Trim()), Common.CheckNullString(gRow.Cells[2].Text.Trim()));
        }
        lblMsg.Text = "Record Upadated Successfully";
    }
    public void UpdateSalaryPackageAmt(string SALPAKID, string SHEADID, string TOTAMNT)
    {
        string strSQL = "UPDATE SalaryPakDetls SET PAYAMT = @PAYAMT,TOTAMNT = @TOTAMNT WHERE SALPAKID = @SALPAKID AND SHEADID=@SHEADID";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_InsertedDate = command.Parameters.Add("SALPAKID", SqlDbType.BigInt);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = SALPAKID;

        SqlParameter p_SPTitle = command.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SPTitle.Direction = ParameterDirection.Input;
        p_SPTitle.Value = SHEADID;

        SqlParameter p_PAYAMT = command.Parameters.Add("PAYAMT", SqlDbType.Decimal);
        p_PAYAMT.Direction = ParameterDirection.Input;
        p_PAYAMT.Value = TOTAMNT;

        SqlParameter p_TOTAMNT = command.Parameters.Add("TOTAMNT", SqlDbType.Decimal);
        p_TOTAMNT.Direction = ParameterDirection.Input;
        p_TOTAMNT.Value = TOTAMNT;

        objDC.ExecuteQuery(command);
    }
    protected void btnUpdateFlag2_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            this.UpdateSalaryPackageAmt(gRow.Cells[0].Text.Trim(), Common.CheckNullString(gRow.Cells[1].Text.Trim()), Common.CheckNullString(gRow.Cells[2].Text.Trim()));
        }
        lblMsg.Text = "Record Upadated Successfully";
    }

    protected void btnUploadEmpSalPakId_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\\UploadFile\\MSB\\Salary Imput From Jan  to Aug  2018.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();
    }
    
    protected void btnUpdateEmpSalPakId_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            this.UpdateEmpGrossBasicSal(gRow.Cells[0].Text.Trim(), Common.CheckNullString(gRow.Cells[3].Text.Trim()));
        }
        lblMsg.Text = "Record Updated Successfully";
    }
   
    public void UpdateEmpWiseSalPackId(string EmpId, string SALPAKID)
    {
        string strSQL = "UPDATE EmpInfo SET SALPAKID = @SALPAKID WHERE EmpId = @EmpId";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_InsertedDate = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = EmpId;

        SqlParameter p_SPTitle = command.Parameters.Add("SALPAKID", SqlDbType.VarChar);
        p_SPTitle.Direction = ParameterDirection.Input;
        p_SPTitle.Value = SALPAKID;
        objDC.ExecuteQuery(command);
    }

    public void UpdateEmpWiseStatus(string EmpId, string Status, string SeparationDate)
    {
        string strSQL = "UPDATE EmpInfo SET EmpStatus = @EmpStatus,SeparateDate=@SeparateDate WHERE EmpId = @EmpId";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_InsertedDate = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = EmpId;

        SqlParameter p_SPTitle = command.Parameters.Add("EmpStatus", SqlDbType.Char);
        p_SPTitle.Direction = ParameterDirection.Input;
        p_SPTitle.Value = Status;

        SqlParameter p_SeparationDate = command.Parameters.Add("SeparateDate", SqlDbType.DateTime);
        p_SeparationDate.Direction = ParameterDirection.Input;
        p_SeparationDate.Value = SeparationDate;
        objDC.ExecuteQuery(command);
    }

    public void UpdateEmpGrossBasicSal(string EmpId, string sBasicSalary)//string sBasicSalary,
    {
        //string strSQL = "UPDATE EmpInfo SET BasicSalary = @BasicSalary,GrossSalary=@GrossSalary WHERE EmpId = @EmpId";
        string strSQL = "UPDATE EmpInfo SET BasicSalary=@BasicSalary WHERE EmpId = @EmpId";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_InsertedDate = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = EmpId;

        SqlParameter p_BasicSalary = command.Parameters.Add("BasicSalary", SqlDbType.Decimal);
        p_BasicSalary.Direction = ParameterDirection.Input;
        p_BasicSalary.Value = sBasicSalary;

        //SqlParameter p_GrossSalary = command.Parameters.Add("GrossSalary", SqlDbType.Decimal);
        //p_GrossSalary.Direction = ParameterDirection.Input;
        //p_GrossSalary.Value = sGrossSalary;
        objDC.ExecuteQuery(command);
    }
    protected void btnUploadBankCodeRouting_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\\SCBUploadTool\\BankCode_RoutingNo.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();
    }
    protected void btnUpdateBankCodeRouting_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            this.UpdateBankCodeRoutingNo(gRow.Cells[0].Text.Trim(), Common.CheckNullString(gRow.Cells[1].Text.Trim()), Common.CheckNullString(gRow.Cells[2].Text.Trim()));
            //this.UpdateBankCode(gRow.Cells[0].Text.Trim(), Common.CheckNullString(gRow.Cells[1].Text.Trim()));
        }
        lblMsg.Text = "Record Updated Successfully";
    }

    public void UpdateBankCodeRoutingNo(string SLId, string BankCode, string RoutingNo)
    {
        string strSQL = "UPDATE EmpInfo SET RoutingNo=@RoutingNo WHERE BankCode = @SLId";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_InsertedDate = command.Parameters.Add("SLId", SqlDbType.VarChar);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = SLId;

        //SqlParameter p_SPTitle = command.Parameters.Add("BankCode", SqlDbType.VarChar);
        //p_SPTitle.Direction = ParameterDirection.Input;
        //p_SPTitle.Value = BankCode;

        SqlParameter p_RoutingNo = command.Parameters.Add("RoutingNo", SqlDbType.VarChar);
        p_RoutingNo.Direction = ParameterDirection.Input;
        p_RoutingNo.Value = RoutingNo;

        objDC.ExecuteQuery(command);
    }

    protected void btnUpdateBankCode_Click(object sender, EventArgs e)
    {
        int i = 0;
        SqlCommand[] command = new SqlCommand[300];
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            command[i] = UpdateBankCode(gRow.Cells[0].Text.Trim(), Common.CheckNullString(gRow.Cells[1].Text.Trim()), Common.CheckNullString(gRow.Cells[2].Text.Trim()));
            i++;
        }
        objDC.MakeTransaction(command);

        lblMsg.Text = "Record Updated Successfully";
    }

    public SqlCommand UpdateBankCode(string SLId, string BankCode, string RoutingNo)
    {
        string strSQL = "UPDATE EmpInfo SET BankCode=@BankCode WHERE RoutingNo = @RoutingNo";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_InsertedDate = command.Parameters.Add("RoutingNo", SqlDbType.VarChar);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = RoutingNo;

        SqlParameter p_RoutingNo = command.Parameters.Add("BankCode", SqlDbType.VarChar);
        p_RoutingNo.Direction = ParameterDirection.Input;
        p_RoutingNo.Value = BankCode;

        return command;
    }
    protected void btnUploadBankId_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\BASESOFT\\StaffWsBankBranch.xls;Extended Properties=Excel 12.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();
    }
    protected void btnUpdateBankId_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            this.UpdateBankId(gRow.Cells[0].Text.Trim(), Common.CheckNullString(gRow.Cells[1].Text.Trim()));
        }
        lblMsg.Text = "Record Updated Successfully";
    }

    public void UpdateBankId(string EmpId, string BankCode)
    {
        string strSQL = "UPDATE EmpInfo SET BankCode = @BankCode WHERE EmpId = @EmpId";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_SPTitle = command.Parameters.Add("BankCode", SqlDbType.VarChar);
        p_SPTitle.Direction = ParameterDirection.Input;
        p_SPTitle.Value = BankCode;

        objDC.ExecuteQuery(command);
    }

    protected void btnUploadJobTitle_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\\SCBUploadTool\\JobTitle.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();

        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[3].Text)) == true)
                gRow.BackColor = System.Drawing.Color.Red;
        }
    }
    protected void btnUpdateJobTilte_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            this.UpdateJobTitle(gRow.Cells[0].Text.Trim(), Common.CheckNullString(gRow.Cells[1].Text.Trim()), Common.CheckNullString(gRow.Cells[2].Text.Trim()), Common.CheckNullString(gRow.Cells[4].Text.Trim()), Common.CheckNullString(gRow.Cells[3].Text.Trim()));
        }
        lblMsg.Text = "Record Updated Successfully";
    }

    public void UpdateJobTitle(string EmpId, string MarriageDate, string IsNotRehirable, string NotRehireReason, string JobTitle)
    {
        string strSQL = "UPDATE EmpInfo SET MarriageDate=@MarriageDate,IsNotRehirable=@IsNotRehirable,NotRehireReason=@NotRehireReason,JobTitleId=@JobTitleId WHERE EmpId = @EmpId";
        //string strSQL = "UPDATE EmpInfo SET MarriageDate=@MarriageDate,IsNotRehirable=@IsNotRehirable,NotRehireReason=@NotRehireReason WHERE EmpId = @EmpId";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_SPTitle = command.Parameters.Add("MarriageDate", DBNull.Value);
        p_SPTitle.Direction = ParameterDirection.Input;
        p_SPTitle.IsNullable = true;
        if (MarriageDate != "")
            p_SPTitle.Value = Common.ReturnDate(MarriageDate);

        SqlParameter p_IsNotRehirable = command.Parameters.Add("IsNotRehirable", SqlDbType.Char);
        p_IsNotRehirable.Direction = ParameterDirection.Input;
        p_IsNotRehirable.Value = IsNotRehirable;

        SqlParameter p_NotRehireReason = command.Parameters.Add("NotRehireReason", SqlDbType.VarChar);
        p_NotRehireReason.Direction = ParameterDirection.Input;
        p_NotRehireReason.Value = NotRehireReason;

        SqlParameter p_JobTitleId = command.Parameters.Add("JobTitleId", DBNull.Value);
        p_JobTitleId.Direction = ParameterDirection.Input;
        p_JobTitleId.IsNullable = true;
        string ConvertedJobTitle = "";
        ConvertedJobTitle = GetJobTitleId(JobTitle);
        if (ConvertedJobTitle != "")
            p_JobTitleId.Value = ConvertedJobTitle;
        else
            p_JobTitleId.Value = JobTitle;

        objDC.ExecuteQuery(command);
    }

    private string GetJobTitleId(string JobTitleName)
    {
        string strSql = "Select JobTitleId From JobTitle Where JobTitleName=@JobTitleName";

        SqlCommand command = new SqlCommand(strSql);
        command.CommandType = CommandType.Text;

        SqlParameter p_USERID = command.Parameters.Add("JobTitleName", SqlDbType.VarChar);
        p_USERID.Direction = ParameterDirection.Input;
        p_USERID.Value = JobTitleName;

        return objDC.GetScalarVal(command);
    }
    protected void btnUploadTax_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=C:\\BASESOFT\\TaxNewList.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();

        //foreach (GridViewRow gRow in grPayroll.Rows)
        //{
        //    if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[3].Text)) == true)
        //        gRow.BackColor = System.Drawing.Color.Red;
        //}
    }
    protected void btnUpdateTax_Click(object sender, EventArgs e)
    {
        //this.UpdateTax(gRow.Cells[0].Text.Trim(), "-" + Common.CheckNullString(gRow.Cells[1].Text.Trim()));
        this.InsertTax(grPayroll);

        lblMsg.Text = "Record Updated Successfully";
    }

    public void UpdateTax(string EmpId, string PayAmt)
    {
        string strSQL = "UPDATE PayslipDets SET PayAmt =@PayAmt WHERE EmpId=@EmpId AND SHeadId=15 AND PSBID=10";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_SalPakId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = EmpId;

        SqlParameter p_SPTitle = command.Parameters.Add("PayAmt", DBNull.Value);
        p_SPTitle.Direction = ParameterDirection.Input;
        if (PayAmt != "")
            p_SPTitle.Value = PayAmt;

        objDC.ExecuteQuery(command);
    }
    protected void btnUploadPFLoan_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\\SCBUploadTool\\PFLoan_Upload_Jan_2016.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();

        //foreach (GridViewRow gRow in grPayroll.Rows)
        //{
        //    if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[3].Text)) == true)
        //        gRow.BackColor = System.Drawing.Color.Red;
        //}
    }
    protected void btnUpdatePFLoan_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            this.UpdatePFLoan(gRow.Cells[0].Text.Trim(), "-" + Common.CheckNullString(gRow.Cells[1].Text.Trim()));
        }
        lblMsg.Text = "Record Updated Successfully";
        //Int32 iVId = 0;
        //foreach (GridViewRow gRow in grPayroll.Rows)
        //{
        //    iVId = Convert.ToInt32(Common.getMaxId("VARIABLEALLOWANCEDEDUCT", "VID"));
        //    if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[3].Text.Trim())) == false)
        //    {
        //        this.UpdatePFLoan(iVId, gRow.Cells[0].Text.Trim(), Common.CheckNullString(gRow.Cells[3].Text.Trim()));
        //        this.UpdatePFLoanDet(iVId, gRow.Cells[0].Text.Trim(), Common.CheckNullString(gRow.Cells[3].Text.Trim()));
        //        iVId = iVId + 1;
        //    }
        //}
        //lblMsg.Text = "Record Updated Successfully";
    }

    public void UpdatePFLoan(Int32 iVId, string EmpId, string PayAmt)
    {
        string strSQL = "INSERT INTO VARIABLEALLOWANCEDEDUCT (VID,EMPID,SHEADID,PAYAMNT,VALIDFROM,VALIDTO,ISACTIVE,INSERTEDBY,INSERTEDDATE)"
            + " VALUES(@VID,@EMPID,@SHEADID,@PAYAMNT,@VALIDFROM,@VALIDTO,@ISACTIVE,@INSERTEDBY,@INSERTEDDATE)";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_SalPakId = command.Parameters.Add("VID", SqlDbType.BigInt);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = iVId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_SHEADID = command.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = "16";

        SqlParameter p_TotAmnt = command.Parameters.Add("PAYAMNT", DBNull.Value);
        p_TotAmnt.Direction = ParameterDirection.Input;
        if (PayAmt != "")
            p_TotAmnt.Value = PayAmt;

        SqlParameter p_VALIDFROM = command.Parameters.Add("VALIDFROM", SqlDbType.DateTime);
        p_VALIDFROM.Direction = ParameterDirection.Input;
        p_VALIDFROM.Value = "2015/01/01";

        SqlParameter p_VALIDTO = command.Parameters.Add("VALIDTO", SqlDbType.DateTime);
        p_VALIDTO.Direction = ParameterDirection.Input;
        p_VALIDTO.Value = "2015/01/31";

        SqlParameter p_ISACTIVE = command.Parameters.Add("ISACTIVE", SqlDbType.Char);
        p_ISACTIVE.Direction = ParameterDirection.Input;
        p_ISACTIVE.Value = "A";

        SqlParameter p_INSERTEDBY = command.Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = "admin";

        SqlParameter p_INSERTEDDATE = command.Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDDATE.Direction = ParameterDirection.Input;
        p_INSERTEDDATE.Value = "2015/03/01";

        objDC.ExecuteQuery(command);
    }

    public void UpdatePFLoanDet(Int32 iVId, string EmpId, string PayAmt)
    {
        string strSQL = "INSERT INTO VARIABLEALLOWANCEDEDUCTDetls (VID,VMONTH,VYEAR,VDAYS,PAYAMNT)"
            + " VALUES(@VID,@VMONTH,@VYEAR,@VDAYS,@PAYAMNT)";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_SalPakId = command.Parameters.Add("VID", SqlDbType.BigInt);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = iVId;

        SqlParameter p_VMONTH = command.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = "1";

        SqlParameter p_VYEAR = command.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = "2015";

        SqlParameter p_VDAYS = command.Parameters.Add("VDAYS", SqlDbType.BigInt);
        p_VDAYS.Direction = ParameterDirection.Input;
        p_VDAYS.Value = "31";

        SqlParameter p_TotAmnt = command.Parameters.Add("PAYAMNT", DBNull.Value);
        p_TotAmnt.Direction = ParameterDirection.Input;
        if (PayAmt != "")
            p_TotAmnt.Value = PayAmt;

        objDC.ExecuteQuery(command);
    }
    protected void btnUploadJobTitleDes_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\\SCBUploadTool\\Designation_JobTitle_May2015.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();

        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            if (string.IsNullOrEmpty(GetDesigIdNameWs(gRow.Cells[0].Text.ToString().Trim())) == false)
                gRow.Cells[0].Text = GetDesigIdNameWs(gRow.Cells[0].Text.ToString().Trim());
            //else
            //{
            //    gRow.Cells[0].Text = gRow.Cells[0].Text.ToString().Trim();
            //    gRow.BackColor = System.Drawing.Color.Yellow;
            //}         

            if (string.IsNullOrEmpty(GetJobTitleIdNameWs(gRow.Cells[1].Text.ToString().Trim())) == false)
                gRow.Cells[1].Text = GetJobTitleIdNameWs(gRow.Cells[1].Text.ToString().Trim());
            else
            {
                gRow.Cells[1].Text = gRow.Cells[1].Text.ToString().Trim();
                gRow.BackColor = System.Drawing.Color.Red;
            }

        }
    }
    public string GetDesigIdNameWs(string strDesigName)
    {
        string strSalSourceId = "";

        string strSql = "SELECT DesigName FROM Designation Where DesigName='" + strDesigName + "'";

        SqlCommand cmd = new SqlCommand(strSql);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_SHEADID = cmd.Parameters.Add("DesigName", SqlDbType.VarChar);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = strDesigName;

        strSalSourceId = objDC.GetScalarVal(cmd);

        return strSalSourceId;
    }
    public string GetJobTitleIdNameWs(string strJobTlName)
    {
        string strSalSourceId = "";

        string strSql = "SELECT JobTitleName FROM JobTitle Where JobTitleName='" + strJobTlName + "'";

        SqlCommand cmd = new SqlCommand(strSql);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_SHEADID = cmd.Parameters.Add("JobTitleName", SqlDbType.VarChar);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = strJobTlName;

        strSalSourceId = objDC.GetScalarVal(cmd);

        return strSalSourceId;
    }

    protected void btnUpdateJobTitleDes_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            if ((string.IsNullOrEmpty(gRow.Cells[0].Text.Trim()) == false) && (string.IsNullOrEmpty(gRow.Cells[1].Text.Trim()) == false))
                this.UpdateJobTitleDes(gRow.Cells[0].Text.Trim(), Common.CheckNullString(gRow.Cells[1].Text.Trim()));
        }
        lblMsg.Text = "Record Updated Successfully";
    }
    public void UpdateJobTitleDes(string DesigId, string iJobTitleId)
    {
        string strSQL = "INSERT INTO DesigWiseJobTitle (DesigId,JobTitleId)"
            + " VALUES(@DesigId,@JobTitleId)";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_SalPakId = command.Parameters.Add("DesigId", SqlDbType.BigInt);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = Convert.ToInt32(DesigId);

        SqlParameter p_VMONTH = command.Parameters.Add("JobTitleId", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = Convert.ToInt32(iJobTitleId);

        objDC.ExecuteQuery(command);
    }

    protected void btnUploadSecWsDept_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\\SCBUploadTool\\Sector_ProgramDepartment.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();

        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            gRow.Cells[0].Text = GetSectorIdNameWs(gRow.Cells[0].Text.ToString().Trim());

            if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[0].Text)) == true)
                gRow.BackColor = System.Drawing.Color.Red;

            gRow.Cells[1].Text = GetDeptIdNameWs(gRow.Cells[1].Text.ToString().Trim());
        }
    }

    public string GetSectorIdNameWs(string strSectorName)
    {
        string strSalSourceId = "";

        string strSql = "SELECT SectorId FROM SectorList Where SectorName='" + strSectorName + "'";

        SqlCommand cmd = new SqlCommand(strSql);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_SHEADID = cmd.Parameters.Add("SectorName", SqlDbType.VarChar);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = strSectorName;

        strSalSourceId = objDC.GetScalarVal(cmd);

        return strSalSourceId;
    }
    public string GetDeptIdNameWs(string strDeptName)
    {
        string strSalSourceId = "";

        string strSql = "SELECT DeptId FROM DepartmentList Where DeptName='" + strDeptName + "'";

        SqlCommand cmd = new SqlCommand(strSql);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_SHEADID = cmd.Parameters.Add("DeptName", SqlDbType.VarChar);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = strDeptName;

        strSalSourceId = objDC.GetScalarVal(cmd);

        return strSalSourceId;
    }
    protected void btnUpdateSecWsDept_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            if ((string.IsNullOrEmpty(gRow.Cells[0].Text.Trim()) == false) && (string.IsNullOrEmpty(gRow.Cells[1].Text.Trim()) == false))
                this.UpdateSectotDept(gRow.Cells[0].Text.Trim(), Common.CheckNullString(gRow.Cells[1].Text.Trim()));
        }
        lblMsg.Text = "Record Updated Successfully";
    }
    public void UpdateSectotDept(string SectorId, string DeptId)
    {
        string strSQL = "INSERT INTO SectorWiseDepartment (SectorId,DeptId)"
            + " VALUES(@DesigId,@JobTitleId)";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_SalPakId = command.Parameters.Add("SectorId", SqlDbType.BigInt);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = Convert.ToInt32(SectorId);

        SqlParameter p_VMONTH = command.Parameters.Add("DeptId", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = Convert.ToInt32(DeptId);

        objDC.ExecuteQuery(command);
    }
    protected void btnInsertSalPakBonusHeadId_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[1].Text.Trim())) == false)
                this.InsertSalaryPackageBonsHeadId(gRow.Cells[0].Text.Trim(), gRow.Cells[1].Text.Trim());
        }
        lblMsg.Text = "Record Upadated Successfully";
    }

    public void InsertSalaryPackageBonsHeadId(string strEmpId, string strSalPakId)
    {
        string strSQL = "INSERT INTO SalaryPakDetls(SalPakId,SHEADID,PAYAMT,ISBASICSAL,ISPFUND,TOTAMNT,ISACTIVE,INSERTEDBY,INSERTEDDATE)"
            + " VALUES(@SalPakId,@SHEADID,@PAYAMT,@ISBASICSAL,@ISPFUND,@TOTAMNT,@ISACTIVE,@INSERTEDBY,@INSERTEDDATE)";

        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_SalPakId = command.Parameters.Add("SalPakId", SqlDbType.BigInt);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = Convert.ToInt32(strSalPakId);

        SqlParameter p_SHEADID = command.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = 19;

        SqlParameter p_PAYAMT = command.Parameters.Add("PAYAMT", SqlDbType.BigInt);
        p_PAYAMT.Direction = ParameterDirection.Input;
        p_PAYAMT.Value = 0;

        SqlParameter p_ISBASICSAL = command.Parameters.Add("ISBASICSAL", SqlDbType.Char);
        p_ISBASICSAL.Direction = ParameterDirection.Input;
        p_ISBASICSAL.Value = "N";

        SqlParameter p_ISPFUND = command.Parameters.Add("ISPFUND", SqlDbType.Char);
        p_ISPFUND.Direction = ParameterDirection.Input;
        p_ISPFUND.Value = "N";

        SqlParameter p_TOTAMNT = command.Parameters.Add("TOTAMNT", SqlDbType.Decimal);
        p_TOTAMNT.Direction = ParameterDirection.Input;
        p_TOTAMNT.Value = 0;

        SqlParameter p_ISACTIVE = command.Parameters.Add("ISACTIVE", SqlDbType.Char);
        p_ISACTIVE.Direction = ParameterDirection.Input;
        p_ISACTIVE.Value = "Y";

        SqlParameter p_INSERTEDBY = command.Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = Convert.ToInt32(strSalPakId);

        SqlParameter p_INSERTEDDATE = command.Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDDATE.Direction = ParameterDirection.Input;
        p_INSERTEDDATE.Value = DateTime.Now.ToString();

        objDC.ExecuteQuery(command);
    }
    protected void btnUploadDistrcit_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=C:\\BASESOFT\\ContractEndDate.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();
    }

    public string GetDistictIdNameWs(string strDesigName)
    {
        string strSalSourceId = "";

        string strSql = "SELECT DistId FROM HomeDistrictList Where DistName='" + strDesigName + "'";

        SqlCommand cmd = new SqlCommand(strSql);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_SHEADID = cmd.Parameters.Add("DistName", SqlDbType.VarChar);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = strDesigName;

        strSalSourceId = objDC.GetScalarVal(cmd);

        return strSalSourceId;
    }
    protected void btnUpdateDistrict_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[1].Text.Trim())) == false)
                this.UpdateContractEndDate(gRow.Cells[0].Text.Trim(), Common.CheckNullString(gRow.Cells[1].Text.Trim()));
        }
        lblMsg.Text = "Record Updated Successfully";
    }

    public void InsertUserInfo(string USERID, string EMPID, string Pass)
    {
        string strSQL = "INSERT INTO USERINFO (USERID,EMPID,AccountDisabled,PASSWORD,ChangePassword)" +
            " VALUES(@USERID,@EMPID,@AccountDisabled,@PASSWORD,@ChangePassword)";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_USERID = command.Parameters.Add("USERID", SqlDbType.VarChar);
        p_USERID.Direction = ParameterDirection.Input;
        p_USERID.Value = USERID;

        SqlParameter p_EmpId = command.Parameters.Add("EMPID", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EMPID;

        SqlParameter p_AccountDisabled = command.Parameters.Add("AccountDisabled", SqlDbType.Char);
        p_AccountDisabled.Direction = ParameterDirection.Input;
        p_AccountDisabled.Value = "Y";

        SqlParameter p_Pass = command.Parameters.Add("PASSWORD", SqlDbType.VarChar);
        p_Pass.Direction = ParameterDirection.Input;
        p_Pass.Value = Pass;

        SqlParameter p_ChPass = command.Parameters.Add("ChangePassword", SqlDbType.Char);
        p_ChPass.Direction = ParameterDirection.Input;
        p_ChPass.Value = "0";

        objDC.ExecuteQuery(command);
    }
    protected void btnUploadBankAccNo_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\BASESOFT\\Staff Wise Sal Pak Id.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();
    }
    public string GetDegreeIdNameWs(string strDegreeName)
    {
        string strSalSourceId = "";

        string strSql = "SELECT DegreeId FROM DegreeList Where DegreeName='" + strDegreeName + "'";

        SqlCommand cmd = new SqlCommand(strSql);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_SHEADID = cmd.Parameters.Add("DegreeName", SqlDbType.VarChar);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = strDegreeName;

        strSalSourceId = objDC.GetScalarVal(cmd);

        return strSalSourceId;
    }

    protected void btnUpdateBankAccNo_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[1].Text.Trim())) == false)
                this.UpdateBankAccNo(gRow.Cells[0].Text.Trim(), Common.CheckNullString(gRow.Cells[1].Text.Trim()));
        }
        lblMsg.Text = "Record Updated Successfully";
    }

    public void UpdateBankAccNo(string EmpId, string TaxRegionId)
    {
        string strSQL = "UPDATE EmpInfo SET TaxRegionId=@TaxRegionId WHERE EmpId = @EmpId";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_BasicSalary = command.Parameters.Add("TaxRegionId", SqlDbType.BigInt);
        p_BasicSalary.Direction = ParameterDirection.Input;
        p_BasicSalary.Value = TaxRegionId;

        objDC.ExecuteQuery(command);
    }

    protected void btnUploadSupervisorId_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\\SCBUploadTool\\EmpSuervisorInfo.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        DataTable dt = new DataTable();
        dt = ds.Tables[0].Copy();
        dt.Columns.Add("AccNo");
        foreach (DataRow dRow in dt.Rows)
        {
            dRow["AccNo"] = dRow[1].ToString().Trim();
        }
        dt.AcceptChanges();

        grPayroll.DataSource = dt;
        grPayroll.DataBind();
    }
    protected void btnUpdateSupervisorId_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            //if (string.IsNullOrEmpty(Common.CheckNullString(gRow.Cells[1].Text.Trim())) == false)
            this.UpdateSupervisorId(gRow.Cells[0].Text.Trim(), Common.CheckNullString(gRow.Cells[1].Text.Trim()), Common.CheckNullString(gRow.Cells[2].Text.Trim()));
        }
        lblMsg.Text = "Record Updated Successfully";
    }

    public void UpdateSupervisorId(string EmpId, string SupervisorId, string SeveranceId)
    {
        string strSQL = "UPDATE EmpInfo SET SupervisorId=@SupervisorId,SeveranceId=@SeveranceId WHERE EmpId = @EmpId";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_SupervisorId = command.Parameters.Add("SupervisorId", SqlDbType.VarChar);
        p_SupervisorId.Direction = ParameterDirection.Input;
        p_SupervisorId.Value = SupervisorId;

        SqlParameter p_SeveranceId = command.Parameters.Add("SeveranceId", SqlDbType.VarChar);
        p_SeveranceId.Direction = ParameterDirection.Input;
        p_SeveranceId.Value = SeveranceId;

        objDC.ExecuteQuery(command);
    }

    public void UpdateContractEndDate(string EmpId, string OfficeEmail)
    {
        string strSQL = "UPDATE EmpInfo SET ContractEndDate=@ContractEndDate WHERE EmpId = @EmpId";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_SupervisorId = command.Parameters.Add("ContractEndDate", SqlDbType.VarChar);
        p_SupervisorId.Direction = ParameterDirection.Input;
        p_SupervisorId.Value = OfficeEmail;

        objDC.ExecuteQuery(command);
    }
    protected void btnUpdateSalPak_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            this.InsertSalaryPackageMst(Common.CheckNullString(gRow.Cells[2].Text.Trim()),
                Common.CheckNullString(gRow.Cells[3].Text.Trim()), Common.CheckNullString(gRow.Cells[9].Text.Trim()),
                Common.CheckNullString(gRow.Cells[0].Text.Trim()));

            this.InsertSalaryPackageDet(Common.CheckNullString(gRow.Cells[2].Text.Trim()), "1", gRow.Cells[4].Text.Trim());

            this.InsertSalaryPackageDet(Common.CheckNullString(gRow.Cells[2].Text.Trim()), "2", gRow.Cells[5].Text.Trim());

            this.InsertSalaryPackageDet(Common.CheckNullString(gRow.Cells[2].Text.Trim()), "3", gRow.Cells[6].Text.Trim());

            this.InsertSalaryPackageDet(Common.CheckNullString(gRow.Cells[2].Text.Trim()), "4", "0");
            this.InsertSalaryPackageDet(Common.CheckNullString(gRow.Cells[2].Text.Trim()), "5", "0");
            this.InsertSalaryPackageDet(Common.CheckNullString(gRow.Cells[2].Text.Trim()), "6", "0");
            this.InsertSalaryPackageDet(Common.CheckNullString(gRow.Cells[2].Text.Trim()), "7", "0");

            this.InsertSalaryPackageDet(Common.CheckNullString(gRow.Cells[2].Text.Trim()), "8", gRow.Cells[8].Text.Trim());

            this.InsertSalaryPackageDet(Common.CheckNullString(gRow.Cells[2].Text.Trim()), "9", "0");
            this.InsertSalaryPackageDet(Common.CheckNullString(gRow.Cells[2].Text.Trim()), "10", "0");
            this.InsertSalaryPackageDet(Common.CheckNullString(gRow.Cells[2].Text.Trim()), "15", "0");
        }
        lblMsg.Text = "Record Upadated Successfully";
    }

    public void InsertSalaryPackageMst(string SALPAKID, string SPTitle, string TotalGrossSal, string EmpId)
    {
        string strSQL = "INSERT INTO SalaryPakMst(SalPakId,SPTitle,TotalGrossSal,TotalSalary,EmpId,CurrId,PayType)" +
                        "VALUES(@SalPakId,@SPTitle,@TotalGrossSal,@TotalSalary,@EmpId,@CurrId,@PayType)";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_SalPakId = command.Parameters.Add("SalPakId", SqlDbType.BigInt);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = SALPAKID;

        SqlParameter p_SPTitle = command.Parameters.Add("SPTitle", SqlDbType.VarChar);
        p_SPTitle.Direction = ParameterDirection.Input;
        p_SPTitle.Value = SPTitle;

        SqlParameter p_TotalGrossSal = command.Parameters.Add("TotalGrossSal", SqlDbType.Decimal);
        p_TotalGrossSal.Direction = ParameterDirection.Input;
        p_TotalGrossSal.Value = TotalGrossSal;

        SqlParameter p_TotalSalary = command.Parameters.Add("TotalSalary", SqlDbType.Decimal);
        p_TotalSalary.Direction = ParameterDirection.Input;
        p_TotalSalary.Value = TotalGrossSal;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_CurrId = command.Parameters.Add("CurrId", SqlDbType.BigInt);
        p_CurrId.Direction = ParameterDirection.Input;
        p_CurrId.Value = 1;

        SqlParameter p_PayType = command.Parameters.Add("PayType", SqlDbType.BigInt);
        p_PayType.Direction = ParameterDirection.Input;
        p_PayType.Value = 3;

        objDC.ExecuteQuery(command);
    }
    public void InsertSalaryPackageDet(string SALPAKID, string SHEADID, string TOTAMNT)
    {
        string strSQL = "INSERT INTO SalaryPakDetls (SalPakId,SHeadId,PayAmt) VALUES(@SalPakId,@SHeadId,@PayAmt)";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_SALPAKID = command.Parameters.Add("SALPAKID", SqlDbType.BigInt);
        p_SALPAKID.Direction = ParameterDirection.Input;
        p_SALPAKID.Value = SALPAKID;

        SqlParameter p_SHEADID = command.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = SHEADID;

        SqlParameter p_PayAmt = command.Parameters.Add("PayAmt", SqlDbType.Decimal);
        p_PayAmt.Direction = ParameterDirection.Input;
        p_PayAmt.Value = TOTAMNT;

        SqlParameter p_TOTAMNT = command.Parameters.Add("TOTAMNT", SqlDbType.Decimal);
        p_TOTAMNT.Direction = ParameterDirection.Input;
        p_TOTAMNT.Value = TOTAMNT;

        objDC.ExecuteQuery(command);
    }

    public void UpdateSalaryPackageDet(string SALPAKID, string SHEADID, string TOTAMNT)
    {
        string strSQL = "UPDATE SalaryPakDetls SET PayAmt = @PayAmt,TOTAMNT = @TOTAMNT WHERE SALPAKID = @SALPAKID AND SHEADID=@SHEADID";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_InsertedDate = command.Parameters.Add("SALPAKID", SqlDbType.BigInt);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = SALPAKID;

        SqlParameter p_SPTitle = command.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SPTitle.Direction = ParameterDirection.Input;
        p_SPTitle.Value = SHEADID;

        SqlParameter p_PayAmt = command.Parameters.Add("PayAmt", SqlDbType.Decimal);
        p_PayAmt.Direction = ParameterDirection.Input;
        p_PayAmt.Value = TOTAMNT;

        SqlParameter p_TOTAMNT = command.Parameters.Add("TOTAMNT", SqlDbType.Decimal);
        p_TOTAMNT.Direction = ParameterDirection.Input;
        p_TOTAMNT.Value = TOTAMNT;

        objDC.ExecuteQuery(command);
    }
    protected void btnSalPak_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\\SULATA BACK\\SPTitle.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();

        //foreach (GridViewRow gRow in grPayroll.Rows)
        //{
        //    if (string.IsNullOrEmpty(gRow.Cells[1].Text.ToString().Trim()) == false)
        //    {
        //        if (string.IsNullOrEmpty(GetEmpIdWsSalPakId(gRow.Cells[0].Text.ToString().Trim())) == false)
        //            gRow.Cells[5].Text = GetEmpIdWsSalPakId(gRow.Cells[0].Text.ToString().Trim());
        //        else
        //        {
        //            gRow.Cells[1].Text = gRow.Cells[1].Text.ToString().Trim();
        //            gRow.BackColor = System.Drawing.Color.Red;
        //        }
        //    }
        //}
    }

    public string GetEmpIdWsSalPakId(string strEmpId)
    {
        string strSalSourceId = "";

        string strSql = "SELECT SalPakId FROM SalaryPakMst Where EmpId='" + strEmpId + "'";

        SqlCommand cmd = new SqlCommand(strSql);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        strSalSourceId = objDC.GetScalarVal(cmd);

        return strSalSourceId;
    }


    protected void btnCOLA_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\\SCBUploadTool\\EmpImage.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();

        //foreach (GridViewRow gRow in grPayroll.Rows)
        //{
        //    if (string.IsNullOrEmpty(gRow.Cells[1].Text.ToString().Trim()) == false)
        //    {
        //        if (string.IsNullOrEmpty(GetEmpIdWsSalPakId(gRow.Cells[0].Text.ToString().Trim())) == false)
        //            gRow.Cells[5].Text = GetEmpIdWsSalPakId(gRow.Cells[0].Text.ToString().Trim());
        //        else
        //        {
        //            gRow.Cells[1].Text = gRow.Cells[1].Text.ToString().Trim();
        //            gRow.BackColor = System.Drawing.Color.Red;
        //        }
        //    }
        //}
    }
    protected void btnInsertCOLA_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            this.InsertCOLA(gRow.Cells[0].Text.Trim(), gRow.Cells[1].Text.Trim(), gRow.Cells[2].Text.Trim(), gRow.Cells[3].Text.Trim(), gRow.Cells[4].Text.Trim(),
                gRow.Cells[5].Text.Trim(), gRow.Cells[6].Text.Trim());
        }
        lblMsg.Text = "Record Upadated Successfully";
    }

    public void InsertCOLA(string EmpId, string GradeId, string BasicSal, string NewBasicSal, string Allowance, string PF, string Percentage)
    {
        string strSQL = "INSERT INTO CLOAAdjustLog(LogId,FiscalYrId,VMonth,VYear,GradeId,EmpId,BasicSal,NewBasicSal,Allowance,PF,Percentage)"
            + " VALUES(@LogId,@FiscalYrId,@VMonth,@VYear,@GradeId,@EmpId,@BasicSal,@NewBasicSal,@Allowance,@PF,@Percentage)";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_InsertedDate = command.Parameters.Add("LogId", SqlDbType.BigInt);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = "1";

        SqlParameter p_SPTitle = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_SPTitle.Direction = ParameterDirection.Input;
        p_SPTitle.Value = "39";

        SqlParameter p_VMonth = command.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = "7";

        SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.BigInt);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = "2015";

        SqlParameter p_TOTAMNT = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_TOTAMNT.Direction = ParameterDirection.Input;
        p_TOTAMNT.Value = EmpId;

        SqlParameter p_PayAmt = command.Parameters.Add("GradeId", SqlDbType.BigInt);
        p_PayAmt.Direction = ParameterDirection.Input;
        p_PayAmt.Value = GradeId;

        SqlParameter p_BasicSal = command.Parameters.Add("BasicSal", SqlDbType.Decimal);
        p_BasicSal.Direction = ParameterDirection.Input;
        p_BasicSal.Value = BasicSal;

        SqlParameter p_NewBasicSal = command.Parameters.Add("NewBasicSal", SqlDbType.Decimal);
        p_NewBasicSal.Direction = ParameterDirection.Input;
        p_NewBasicSal.Value = NewBasicSal;

        SqlParameter p_Allowance = command.Parameters.Add("Allowance", SqlDbType.Decimal);
        p_Allowance.Direction = ParameterDirection.Input;
        p_Allowance.Value = Allowance;

        SqlParameter p_PF = command.Parameters.Add("PF", SqlDbType.Decimal);
        p_PF.Direction = ParameterDirection.Input;
        p_PF.Value = PF;

        SqlParameter p_Percentage = command.Parameters.Add("Percentage", SqlDbType.Decimal);
        p_Percentage.Direction = ParameterDirection.Input;
        p_Percentage.Value = Percentage;

        objDC.ExecuteQuery(command);
    }
    protected void btnUploadPF_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\\SCBUploadTool\\New_Sal_Aug2015_PF.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();
    }

    protected void btnUpdatePF_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            this.UpdatePF(gRow.Cells[0].Text.Trim(), "-" + Common.CheckNullString(gRow.Cells[1].Text.Trim()));
        }
        lblMsg.Text = "Record Updated Successfully";
    }

    public void UpdatePF(string EmpId, string PayAmt)
    {
        string strSQL = "UPDATE PayslipDets SET PayAmt =@PayAmt WHERE EmpId=@EmpId AND SHeadId=13 AND PSBID=10";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_SalPakId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = EmpId;

        SqlParameter p_SPTitle = command.Parameters.Add("PayAmt", DBNull.Value);
        p_SPTitle.Direction = ParameterDirection.Input;
        if (PayAmt != "")
            p_SPTitle.Value = PayAmt;

        objDC.ExecuteQuery(command);
    }

    public void UpdatePFLoan(string EmpId, string PayAmt)
    {
        string strSQL = "UPDATE PayslipDets SET PayAmt =@PayAmt WHERE EmpId=@EmpId AND SHeadId=16 AND PSBID=15";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_SalPakId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = EmpId;

        SqlParameter p_SPTitle = command.Parameters.Add("PayAmt", DBNull.Value);
        p_SPTitle.Direction = ParameterDirection.Input;
        if (PayAmt != "")
            p_SPTitle.Value = PayAmt;

        objDC.ExecuteQuery(command);
    }
    protected void btnUploadChildEducation_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\\SCBUploadTool\\Chield Education Allowance Jan 2016.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();

    }
    protected void btnUpdateChildEducation_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            this.UpdateChildEducation(gRow.Cells[0].Text.Trim(), Common.CheckNullString(gRow.Cells[1].Text.Trim()));
        }
        lblMsg.Text = "Record Updated Successfully";
    }

    public void UpdateChildEducation(string EmpId, string PayAmt)
    {
        string strSQL = "UPDATE PayslipDets SET PayAmt =@PayAmt WHERE EmpId=@EmpId AND SHeadId=7 AND PSBID=15";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_SalPakId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = EmpId;

        SqlParameter p_SPTitle = command.Parameters.Add("PayAmt", DBNull.Value);
        p_SPTitle.Direction = ParameterDirection.Input;
        if (PayAmt != "")
            p_SPTitle.Value = PayAmt;

        objDC.ExecuteQuery(command);
    }

    protected void btnUpdateBasicSalary_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            this.UpdateBasicSalary(Common.CheckNullString(gRow.Cells[0].Text.Trim()), gRow.Cells[2].Text.Trim(),
                gRow.Cells[4].Text.Trim(), gRow.Cells[7].Text.Trim());
        }
    }

    public void UpdateBasicSalary(string EmpId, string salPakId, string BasicSalary, string GrossSalary)
    {
        string strSQL = "UPDATE EmpInfo SET SalPakId=@SalPakId,BasicSalary=@BasicSalary,GrossSalary=@GrossSalary WHERE EmpId = @EmpId";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_SalPakId = command.Parameters.Add("SalPakId", SqlDbType.BigInt);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = salPakId;

        SqlParameter p_BasicSalary = command.Parameters.Add("BasicSalary", SqlDbType.Decimal);
        p_BasicSalary.Direction = ParameterDirection.Input;
        p_BasicSalary.Value = BasicSalary;

        SqlParameter p_GrossSalary = command.Parameters.Add("GrossSalary", SqlDbType.Decimal);
        p_GrossSalary.Direction = ParameterDirection.Input;
        p_GrossSalary.Value = GrossSalary;

        objDC.ExecuteQuery(command);
    }

    protected void btnUpdateEmpImage_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            this.UpdateEmpImage(Common.CheckNullString(gRow.Cells[0].Text.Trim()), gRow.Cells[1].Text.Trim());
        }
    }

    public void UpdateEmpImage(string EmpId, string EmpImage)
    {
        string strSQL = "UPDATE EmpInfo SET EmpImage=@EmpImage WHERE EmpId ='00018'";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_BasicSalary = command.Parameters.Add("EmpImage", SqlDbType.Image);
        p_BasicSalary.Direction = ParameterDirection.Input;
        p_BasicSalary.Value = EmpImage;

        objDC.ExecuteQuery(command);
    }
    protected void btnLvBalance_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\\SCBUploadTool\\Leave Cross Check_July 19 2016 for Action Enjoyed.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();
    }
    protected void btnUpdateLvBalance_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            this.UpdateLeaveEnjoyed(Common.CheckNullString(gRow.Cells[0].Text.Trim()), gRow.Cells[1].Text.Trim(), gRow.Cells[2].Text.Trim());
        }
    }
    public void UpdateLeaveEnjoyed(string EmpId, string sLTypeId, string sLEnjoyed)
    {
        string strSQL = "UPDATE EmpLeaveProfile SET LeaveEnjoyed=@LeaveEnjoyed WHERE EmpId = @EmpId AND LTypeId=@LTypeId";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_LTypeId = command.Parameters.Add("LTypeId", SqlDbType.BigInt);
        p_LTypeId.Direction = ParameterDirection.Input;
        p_LTypeId.Value = sLTypeId;

        SqlParameter p_LEnjoyed = command.Parameters.Add("LeaveEnjoyed", SqlDbType.Decimal);
        p_LEnjoyed.Direction = ParameterDirection.Input;
        p_LEnjoyed.Value = sLEnjoyed;

        objDC.ExecuteQuery(command);
    }


    protected void btnLvEntitle_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=C:\\BASESOFT\\LeaveData_20171228.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();
    }
    protected void bt(object sender, EventArgs e)
    {

    }
    protected void btnUpdateLvEntitle_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            this.UpdateLeaveEntitle(Common.CheckNullString(gRow.Cells[0].Text.Trim()), gRow.Cells[1].Text.Trim(),
                gRow.Cells[2].Text.Trim());
            this.UpdateLeaveEntitle(Common.CheckNullString(gRow.Cells[0].Text.Trim()), gRow.Cells[3].Text.Trim(),
                gRow.Cells[4].Text.Trim());
            this.UpdateLeaveEntitle(Common.CheckNullString(gRow.Cells[0].Text.Trim()), gRow.Cells[5].Text.Trim(),
                gRow.Cells[6].Text.Trim());
            //this.InsertLeaveEntitle(Common.CheckNullString(gRow.Cells[0].Text.Trim()), "4", "0");
            //this.InsertLeaveEntitle(Common.CheckNullString(gRow.Cells[0].Text.Trim()), "5", "0");
            //this.InsertLeaveEntitle(Common.CheckNullString(gRow.Cells[0].Text.Trim()), "6", "0");
            //this.InsertLeaveEntitle(Common.CheckNullString(gRow.Cells[0].Text.Trim()), "7", "0");
            //this.InsertLeaveEntitle(Common.CheckNullString(gRow.Cells[0].Text.Trim()), "8", "0");
            lblMsg.Text = "Record Updated Successfully";
        }

        //foreach (GridViewRow gRow in grPayroll.Rows)
        //{
        //    this.InsertLeaveEntitle(Common.CheckNullString(gRow.Cells[0].Text.Trim()), gRow.Cells[4].Text.Trim(), gRow.Cells[5].Text.Trim());
        //}

        //foreach (GridViewRow gRow in grPayroll.Rows)
        //{
        //    this.InsertLeaveEntitle(Common.CheckNullString(gRow.Cells[0].Text.Trim()), gRow.Cells[7].Text.Trim(), gRow.Cells[8].Text.Trim());
        //}


    }

    public void InsertLeaveEntitle(string EmpId, string sLTypeId, string sLEntitled)
    {
        string strSQL = "INSERT INTO EmpLeaveProfile (EMPID,LTYPEID,LEntitled) VALUES(@EMPID,@LTYPEID,@LEntitled)";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_LTypeId = command.Parameters.Add("LTypeId", SqlDbType.BigInt);
        p_LTypeId.Direction = ParameterDirection.Input;
        p_LTypeId.Value = sLTypeId;

        SqlParameter p_LEnjoyed = command.Parameters.Add("LEntitled", SqlDbType.Decimal);
        p_LEnjoyed.Direction = ParameterDirection.Input;
        p_LEnjoyed.Value = sLEntitled;

        objDC.ExecuteQuery(command);
    }

    public void UpdateLeaveEntitle(string EmpId, string sLTypeId, string sLEntitled)
    {
        string strSQL = "UPDATE EmpLeaveProfile SET LEntitled=@LEntitled WHERE EMPID=@EMPID AND LTYPEID=@LTYPEID";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_LTypeId = command.Parameters.Add("LTypeId", SqlDbType.BigInt);
        p_LTypeId.Direction = ParameterDirection.Input;
        p_LTypeId.Value = sLTypeId;

        SqlParameter p_LEnjoyed = command.Parameters.Add("LEntitled", SqlDbType.Decimal);
        p_LEnjoyed.Direction = ParameterDirection.Input;
        p_LEnjoyed.Value = Math.Round(Convert.ToDecimal(sLEntitled), 1);

        objDC.ExecuteQuery(command);
    }
    protected void btnCostCenter_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\BASESOFT\\ClinicBank.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();
    }
    protected void btnUpdateCostCenter_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            this.UpdateCostCenter(gRow.Cells[0].Text.Trim(), Common.CheckNullString(gRow.Cells[1].Text.Trim()), Common.CheckNullString(gRow.Cells[2].Text.Trim()), Common.CheckNullString(gRow.Cells[3].Text.Trim()));
        }
        lblMsg.Text = "Record Updated Successfully";
    }

    public void UpdateCostCenter(string SunCode, string BankAccNo, string BankCode, string RoutingNo)
    {
        string strSQL = "UPDATE ClinicList SET BankAccNo=@BankAccNo,BankCode = @BankCode,RoutingNo=@RoutingNo WHERE SunCode = @SunCode";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_SunCode = command.Parameters.Add("SunCode", SqlDbType.VarChar);
        p_SunCode.Direction = ParameterDirection.Input;
        p_SunCode.Value = SunCode;

        SqlParameter p_BankAccNo = command.Parameters.Add("BankAccNo", SqlDbType.VarChar);
        p_BankAccNo.Direction = ParameterDirection.Input;
        p_BankAccNo.Value = BankAccNo;

        SqlParameter p_SPTitle = command.Parameters.Add("BankCode", SqlDbType.VarChar);
        p_SPTitle.Direction = ParameterDirection.Input;
        p_SPTitle.Value = BankCode;

        SqlParameter p_RoutingNo = command.Parameters.Add("RoutingNo", SqlDbType.VarChar);
        p_RoutingNo.Direction = ParameterDirection.Input;
        p_RoutingNo.Value = RoutingNo;

        objDC.ExecuteQuery(command);
    }
    protected void btnInsertSalaryHead_Click(object sender, EventArgs e)
    {
        this.InsertPFArrear(grPayroll);
        this.InsertPFLoan(grPayroll);
        this.InsertPFLoanInterest(grPayroll);
        this.InsertFestivalBonus(grPayroll);
        lblMsg.Text = "PF Arrear Salary head has inserted for all staff has inserted Successfully";
    }

    public void InsertPFArrear(GridView gr)
    {
        SqlCommand[] cmd = new SqlCommand[gr.Rows.Count];
        int i = 0;

        foreach (GridViewRow gRow in gr.Rows)
        {
            cmd[i] = new SqlCommand("INSERT INTO SALARYPAKDETLS (SalPakId,SHeadId,PayAmt,IsInPercent,IsBasicSal,IsPFund,TotAmnt,IsActive,InsertedBy,InsertedDate)"
                + " VALUES(@SalPakId,11,0,'N','N','N',0,'Y',@InsertedBy,@InsertedDate)");

            SqlParameter p_PAYEMPID = cmd[i].Parameters.Add("SalPakId", SqlDbType.BigInt);
            p_PAYEMPID.Direction = ParameterDirection.Input;
            p_PAYEMPID.Value = gRow.Cells[1].Text.Trim();

            SqlParameter p_InsertedBy = cmd[i].Parameters.Add("InsertedBy", SqlDbType.Char);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = "admin";

            SqlParameter p_InsertedDate = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate.Direction = ParameterDirection.Input;
            p_InsertedDate.Value = Common.SetDateTime(DateTime.Now.ToString());

            i++;
        }
        objDC.MakeTransaction(cmd);
    }

    public void InsertPFLoan(GridView gr)
    {
        SqlCommand[] cmd = new SqlCommand[gr.Rows.Count];
        int i = 0;

        foreach (GridViewRow gRow in gr.Rows)
        {
            cmd[i] = new SqlCommand("INSERT INTO SALARYPAKDETLS (SalPakId,SHeadId,PayAmt,IsInPercent,IsBasicSal,IsPFund,TotAmnt,IsActive,InsertedBy,InsertedDate)"
                + " VALUES(@SalPakId,12,0,'N','N','N',0,'Y',@InsertedBy,@InsertedDate)");

            SqlParameter p_PAYEMPID = cmd[i].Parameters.Add("SalPakId", SqlDbType.BigInt);
            p_PAYEMPID.Direction = ParameterDirection.Input;
            p_PAYEMPID.Value = gRow.Cells[1].Text.Trim();

            SqlParameter p_InsertedBy = cmd[i].Parameters.Add("InsertedBy", SqlDbType.Char);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = "admin";

            SqlParameter p_InsertedDate = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate.Direction = ParameterDirection.Input;
            p_InsertedDate.Value = Common.SetDateTime(DateTime.Now.ToString());

            i++;
        }
        objDC.MakeTransaction(cmd);
    }

    public void InsertPFLoanInterest(GridView gr)
    {
        SqlCommand[] cmd = new SqlCommand[gr.Rows.Count];
        int i = 0;

        foreach (GridViewRow gRow in gr.Rows)
        {
            cmd[i] = new SqlCommand("INSERT INTO SALARYPAKDETLS (SalPakId,SHeadId,PayAmt,IsInPercent,IsBasicSal,IsPFund,TotAmnt,IsActive,InsertedBy,InsertedDate)"
                + " VALUES(@SalPakId,13,0,'N','N','N',0,'Y',@InsertedBy,@InsertedDate)");

            SqlParameter p_PAYEMPID = cmd[i].Parameters.Add("SalPakId", SqlDbType.BigInt);
            p_PAYEMPID.Direction = ParameterDirection.Input;
            p_PAYEMPID.Value = gRow.Cells[1].Text.Trim();

            SqlParameter p_InsertedBy = cmd[i].Parameters.Add("InsertedBy", SqlDbType.Char);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = "admin";

            SqlParameter p_InsertedDate = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate.Direction = ParameterDirection.Input;
            p_InsertedDate.Value = Common.SetDateTime(DateTime.Now.ToString());

            i++;
        }
        objDC.MakeTransaction(cmd);
    }

    public void InsertFestivalBonus(GridView gr)
    {
        SqlCommand[] cmd = new SqlCommand[gr.Rows.Count];
        int i = 0;

        foreach (GridViewRow gRow in gr.Rows)
        {
            cmd[i] = new SqlCommand("INSERT INTO SALARYPAKDETLS (SalPakId,SHeadId,PayAmt,IsInPercent,IsBasicSal,IsPFund,TotAmnt,IsActive,InsertedBy,InsertedDate)"
                + " VALUES(@SalPakId,14,0,'N','N','N',0,'Y',@InsertedBy,@InsertedDate)");

            SqlParameter p_PAYEMPID = cmd[i].Parameters.Add("SalPakId", SqlDbType.BigInt);
            p_PAYEMPID.Direction = ParameterDirection.Input;
            p_PAYEMPID.Value = gRow.Cells[1].Text.Trim();

            SqlParameter p_InsertedBy = cmd[i].Parameters.Add("InsertedBy", SqlDbType.Char);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = "admin";

            SqlParameter p_InsertedDate = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate.Direction = ParameterDirection.Input;
            p_InsertedDate.Value = Common.SetDateTime(DateTime.Now.ToString());

            i++;
        }
        objDC.MakeTransaction(cmd);
    }

    public void InsertTax(GridView gr)
    {
        SqlCommand[] cmd = new SqlCommand[gr.Rows.Count];
        int i = 0;

        foreach (GridViewRow gRow in gr.Rows)
        {
            cmd[i] = new SqlCommand("INSERT INTO SALARYPAKDETLS (SalPakId,SHeadId,PayAmt,IsInPercent,IsBasicSal,IsPFund,TotAmnt,IsActive,InsertedBy,InsertedDate)"
                + " VALUES(@SalPakId,15,0,'N','N','N',0,'Y',@InsertedBy,@InsertedDate)");

            SqlParameter p_PAYEMPID = cmd[i].Parameters.Add("SalPakId", SqlDbType.BigInt);
            p_PAYEMPID.Direction = ParameterDirection.Input;
            p_PAYEMPID.Value = gRow.Cells[1].Text.Trim();

            SqlParameter p_InsertedBy = cmd[i].Parameters.Add("InsertedBy", SqlDbType.Char);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = "admin";

            SqlParameter p_InsertedDate = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate.Direction = ParameterDirection.Input;
            p_InsertedDate.Value = Common.SetDateTime(DateTime.Now.ToString());

            i++;
        }
        objDC.MakeTransaction(cmd);
    }

    protected void btnInsSalPakHisDetls_Click(object sender, EventArgs e)
    {
        string strSalPakID = "";
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            strSalPakID = this.GetEmpSalPakID(gRow.Cells[1].Text.Trim());
            //InsertSalaryPackageHisDetls(gRow.Cells[0].Text.Trim(), strSalPakID, gRow.Cells[1].Text.Trim(), gRow.Cells[3].Text.Trim(), gRow.Cells[4].Text.Trim());
            //InsertSalaryPackageHisDetls(gRow.Cells[0].Text.Trim(), strSalPakID, gRow.Cells[1].Text.Trim(), gRow.Cells[5].Text.Trim(), gRow.Cells[6].Text.Trim());
            //InsertSalaryPackageHisDetls(gRow.Cells[0].Text.Trim(), strSalPakID, gRow.Cells[1].Text.Trim(), gRow.Cells[7].Text.Trim(), gRow.Cells[8].Text.Trim());
        }
        lblMsg.Text = "Record Upadated Successfully";
    }

    public void InsertSalaryPackageHisDetls(string strLogId, string strSalPakId, string strEmpId, string strSHeadId, string strPayAmt,
        string strEffDate,string sLastupdatedFrom)
    {
        string strSQL = "INSERT INTO SalaryPakHisDetls(LogId,SalPakId,EmpId,SHEADID,PAYAMT,EffDate,InsertedBy,InsertedDate,LastUpdatedFrom)"
            + " VALUES(@LogId,@SalPakId,@EmpId,@SHEADID,@PAYAMT,@EffDate,@InsertedBy,@InsertedDate,@LastUpdatedFrom)";

        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_LogId = command.Parameters.Add("LogId", SqlDbType.BigInt);
        p_LogId.Direction = ParameterDirection.Input;
        p_LogId.Value = strLogId;

        SqlParameter p_SalPakId = command.Parameters.Add("SalPakId", SqlDbType.BigInt);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = Convert.ToInt32(strSalPakId);

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_SHEADID = command.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = strSHeadId;

        SqlParameter p_PAYAMT = command.Parameters.Add("PAYAMT", SqlDbType.Decimal);
        p_PAYAMT.Direction = ParameterDirection.Input;
        p_PAYAMT.Value = Convert.ToDecimal(strPayAmt);

        SqlParameter p_EffDate = command.Parameters.Add("EffDate", SqlDbType.DateTime);
        p_EffDate.Direction = ParameterDirection.Input;
        p_EffDate.Value = Common.ReturnDate(strEffDate);

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.Char);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = "admin";

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = Common.SetDateTime(DateTime.Now.ToString());

        SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = sLastupdatedFrom;

        objDC.ExecuteQuery(command);
    }

    public string GetEmpSalPakID(string strEmpID)
    {
        string strText = "";
        string strSQL = "SELECT SalPakId FROM EMPINFO WHERE EMPID=@EMPID";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        strText = objDC.GetScalarVal(cmd);
        return strText;
    }
    protected void btnInsertOtherINfo_Click(object sender, EventArgs e)
    {
        string strSalPakID = "";
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            strSalPakID = this.GetEmpSalPakID(gRow.Cells[1].Text.Trim());
            //InsertSalaryPackageHisDetls(gRow.Cells[25].Text.Trim(), strSalPakID, gRow.Cells[1].Text.Trim(), gRow.Cells[3].Text.Trim(), gRow.Cells[17].Text.Trim());
            //InsertSalaryPackageHisDetls(gRow.Cells[25].Text.Trim(), strSalPakID, gRow.Cells[1].Text.Trim(), gRow.Cells[5].Text.Trim(), gRow.Cells[19].Text.Trim());
            //InsertSalaryPackageHisDetls(gRow.Cells[25].Text.Trim(), strSalPakID, gRow.Cells[1].Text.Trim(), gRow.Cells[7].Text.Trim(), gRow.Cells[21].Text.Trim());
        }
        lblMsg.Text = "Record Upadated Successfully";
    }

    protected void btnGenerateSalPakId_Click(object sender, EventArgs e)
    {
        string strSalPakID = "";
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            strSalPakID = this.GetEmpSalPakID(gRow.Cells[0].Text.Trim());

            if (strSalPakID == "")
            {
                gRow.Cells[1].Text = "";
                gRow.Cells[1].BackColor = System.Drawing.Color.Yellow;
            }
            else
                gRow.Cells[1].Text = strSalPakID;
        }
    }

    protected void btnUpdatePack_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            if (string.IsNullOrEmpty(gRow.Cells[1].Text.Trim()) == false)
            {
                this.UpdateSalPackDetls(Common.CheckNullString(gRow.Cells[1].Text.Trim()), gRow.Cells[2].Text.Trim(),
                   gRow.Cells[3].Text.Trim(),"Increment");
                this.UpdateSalPackDetls(Common.CheckNullString(gRow.Cells[1].Text.Trim()), gRow.Cells[4].Text.Trim(),
                    gRow.Cells[5].Text.Trim(), "Increment");
                this.UpdateSalPackDetls(Common.CheckNullString(gRow.Cells[1].Text.Trim()), gRow.Cells[6].Text.Trim(),
                    gRow.Cells[7].Text.Trim(), "Increment");
                //this.UpdateSalPackDetls(Common.CheckNullString(gRow.Cells[1].Text.Trim()), gRow.Cells[8].Text.Trim(),
                //  "-" + gRow.Cells[9].Text.Trim());
                //this.UpdateSalPackDetls(Common.CheckNullString(gRow.Cells[1].Text.Trim()), gRow.Cells[11].Text.Trim(),
                //    "-" + gRow.Cells[12].Text.Trim());
            }
        }
        lblMsg.Text = "Record Upadated Successfully";
    }

    public void UpdateSalPackDetls(string sSalPakId, string sSHeadId,string sPayAmt,string sLastupdatedFrom)
    {
        string strSQL = "UPDATE SALARYPAKDETLS SET PayAmt=@PayAmt,TotAmnt=@TotAmnt,UpdatedBy=@UpdatedBy,UpdatedDate=@UpdatedDate,"
                + " LastUpdatedFrom=@LastUpdatedFrom WHERE SalPakId=@SalPakId AND SHeadId=@SHeadId";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_SalPakId = command.Parameters.Add("SalPakId", SqlDbType.Decimal);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = sSalPakId;

        SqlParameter p_SHeadId = command.Parameters.Add("SHeadId", SqlDbType.Decimal);
        p_SHeadId.Direction = ParameterDirection.Input;
        p_SHeadId.Value = sSHeadId;

        SqlParameter p_PayAmt = command.Parameters.Add("PayAmt", SqlDbType.Decimal);
        p_PayAmt.Direction = ParameterDirection.Input;
        p_PayAmt.Value = Math.Round(Convert.ToDecimal(sPayAmt), 2);

        SqlParameter p_TotAmnt = command.Parameters.Add("TotAmnt", SqlDbType.Decimal);
        p_TotAmnt.Direction = ParameterDirection.Input;
        p_TotAmnt.Value = Math.Round(Convert.ToDecimal(sPayAmt), 2);

        SqlParameter p_InsertedBy = command.Parameters.Add("UpdatedBy", SqlDbType.Char);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = "admin";

        SqlParameter p_InsertedDate = command.Parameters.Add("UpdatedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = Common.SetDateTime(DateTime.Now.ToString());

        SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = sLastupdatedFrom;

        objDC.ExecuteQuery(command);
    }

    protected void btnUploadSalaryPackageTitle0_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\\UploadFile\\MSB\\Salary Imput From Jan  to Aug  2018.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();
    }

    protected void btnUploadSalaryPackageTitle1_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\\UploadFile\\MSB\\Salary Data July 2018.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();
    }



    protected void btnUploadSalaryHisPackageTitle1_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\\UploadFile\\MSB\\Salary History Data July 2018.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();
    }

    protected void btnGenerateLogId_Click(object sender, EventArgs e)
    {
        int iCount = 1;
        Int32 iLogId = 250; 

        foreach (GridViewRow gRow in grPayroll.Rows)
        {          
            if (iCount <= 3)
            {
                gRow.Cells[4].Text = iLogId.ToString(); 
            }           
            iCount++;
            if (iCount==4)
            {
                iCount = 1;
                iLogId++;
            }
        }
       
    }

    protected void btnUpdateHisPack_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {           
            InsertSalaryPackageHisDetls(gRow.Cells[8].Text.Trim(),gRow.Cells[1].Text.Trim(), gRow.Cells[0].Text.Trim(), gRow.Cells[2].Text.Trim(), 
                gRow.Cells[3].Text.Trim(), gRow.Cells[9].Text.Trim(),"Increment");

            InsertSalaryPackageHisDetls(gRow.Cells[8].Text.Trim(), gRow.Cells[1].Text.Trim(), gRow.Cells[0].Text.Trim(), gRow.Cells[4].Text.Trim(),
                gRow.Cells[5].Text.Trim(), gRow.Cells[9].Text.Trim(), "Increment");

            InsertSalaryPackageHisDetls(gRow.Cells[8].Text.Trim(), gRow.Cells[1].Text.Trim(), gRow.Cells[0].Text.Trim(), gRow.Cells[6].Text.Trim(),
                gRow.Cells[7].Text.Trim(), gRow.Cells[9].Text.Trim(), "Increment");

        }
        lblMsg.Text = "Record Upadated Successfully";
    }

    protected void btnUpdateMasterPack_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            if (string.IsNullOrEmpty(gRow.Cells[1].Text.Trim()) == false)
            {
                this.InsertSalaryPackageHisDetls(gRow.Cells[8].Text.Trim(), gRow.Cells[1].Text.Trim(), gRow.Cells[0].Text.Trim(),
                    gRow.Cells[2].Text.Trim(), gRow.Cells[3].Text.Trim(), gRow.Cells[9].Text.Trim(),"Increment");
                this.InsertSalaryPackageHisDetls(gRow.Cells[8].Text.Trim(), gRow.Cells[1].Text.Trim(), gRow.Cells[0].Text.Trim(),
                    gRow.Cells[4].Text.Trim(), gRow.Cells[5].Text.Trim(), gRow.Cells[9].Text.Trim(), "Increment");
                this.InsertSalaryPackageHisDetls(gRow.Cells[8].Text.Trim(), gRow.Cells[1].Text.Trim(), gRow.Cells[0].Text.Trim(),
                    gRow.Cells[6].Text.Trim(), gRow.Cells[7].Text.Trim(), gRow.Cells[9].Text.Trim(), "Increment");
            }
        }
        lblMsg.Text = "Record Upadated Successfully";
    }

    protected void btnGenerateMaxLogId_Click(object sender, EventArgs e)
    {
        int iCount = 1;
        Int32 iLogId = 2229;

        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            if (iCount <= 3)
            {
                gRow.Cells[4].Text = iLogId.ToString();
            }
            iCount++;
            if (iCount == 4)
            {
                iCount = 1;
                iLogId++;
            }
        }
    }

    protected void btnUploadSalaryHisPackageTitleInc_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\\UploadFile\\MSB\\Salary Data July 2018 For History.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();
    }

    protected void btnUploadSalPackDate_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\\UploadFile\\MSB\\Salary Package Date.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();
    }

    public void UpdateSalPackDate(string sSalPakId, string sEmpId, string sEffDate)
    {
        string strSQL = "UPDATE SalaryPakHisDetls SET EffDate=@EffDate WHERE InsertedDate BETWEEN '2018-08-02 00:00' AND '2018-08-02 23:59' AND LastUpdatedFrom='Salary Package'"
            + " AND SalPakId=@SalPakId AND EmpId=@EmpId";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_SalPakId = command.Parameters.Add("SalPakId", SqlDbType.Decimal);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = sSalPakId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = sEmpId;

        SqlParameter p_EffDate = command.Parameters.Add("EffDate", SqlDbType.DateTime);
        p_EffDate.Direction = ParameterDirection.Input;
        p_EffDate.Value = Common.ReturnDate(sEffDate);

        objDC.ExecuteQuery(command);
    }

    protected void btnUpdateSalPackdate_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            this.UpdateSalPackDate(Common.CheckNullString(gRow.Cells[0].Text.Trim()), gRow.Cells[2].Text.Trim(),
               gRow.Cells[1].Text.Trim());
        }
    }

    protected void btnUploadInc_Click(object sender, EventArgs e)
    {
        string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=D:\\UploadFile\\MSB\\Salary Inc Date.xls;Extended Properties=Excel 8.0";
        OleDbConnection conn = new OleDbConnection(connstr);
        string strSQL = "SELECT * FROM [Sheet1$]";

        OleDbCommand cmd = new OleDbCommand(strSQL, conn);
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        da.Fill(ds);
        grPayroll.DataSource = ds;
        grPayroll.DataBind();
    }

    protected void btnUpdateIncDate_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gRow in grPayroll.Rows)
        {
            this.UpdateIncDate(Common.CheckNullString(gRow.Cells[0].Text.Trim()), gRow.Cells[1].Text.Trim());
        }
    }

    public void UpdateIncDate(string sEmpId, string sEffDate)
    {
        string strSQL = "UPDATE SalaryPakHisDetls SET EffDate=@EffDate WHERE InsertedDate BETWEEN '2018-08-02 00:00' AND '2018-08-02 23:59' AND LastUpdatedFrom='Increment'"
            + " AND EmpId=@EmpId";
        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = sEmpId;

        SqlParameter p_EffDate = command.Parameters.Add("EffDate", SqlDbType.DateTime);
        p_EffDate.Direction = ParameterDirection.Input;
        p_EffDate.Value = Common.ReturnDate(sEffDate);

        objDC.ExecuteQuery(command);
    }
}
