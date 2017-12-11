using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for UploadToolManager
/// </summary>
public class UploadToolManager
{
    DBConnector objDC = new DBConnector();
    EmpInfoManager objEmpMgr = new EmpInfoManager();


    //public void InsertCOLAAdjust(GridView grEList, string strFiscalYrId, string strGradeId, string strPercentage, string strInsBy, string strInsDate, string strLastUpFrom)
    //{
    //    long i = 0;
        
    //    SqlCommand[] cmd;
    //    cmd = new SqlCommand[grEList.Rows.Count * 5];

    //    string strLogId = "";
    //    string strEmpId = "";
    //    string strPayEmpId = "";
    //    string strIsConfirm = "";

    //    string strSalPakId = "";
    //    string strBasicSal = "";
    //    string strNewBasicSal = "";
    //    string strHR = "";
    //    string strPF = "";

    //    strLogId = Common.getMaxId("CLOAAdjustLog", "LogId");
    //    DataTable dTable = new DataTable();
    //    foreach (GridViewRow gRow in grEList.Rows)
    //    {
    //        strEmpId = gRow.Cells[1].Text.Trim();
    //        strPayEmpId = gRow.Cells[2].Text.Trim();
    //        strIsConfirm = gRow.Cells[4].Text.Trim();

    //        dTable = GET_SalPakId(gRow.Cells[1].Text.Trim());
    //        strSalPakId = dTable.Rows[0]["SalPakId"].ToString();

    //        strBasicSal = gRow.Cells[5].Text.Trim();
    //        strNewBasicSal = gRow.Cells[6].Text.Trim();
    //        strHR = gRow.Cells[7].Text.Trim();
    //        strPF = gRow.Cells[8].Text.Trim();

    //        cmd[i] = objEmpMgr.InsertCOLAAdjustEntry(strLogId, strFiscalYrId, strGradeId, strEmpId, strPayEmpId, strIsConfirm,
    //            strBasicSal, strNewBasicSal, strHR, strPF, strPercentage, strInsBy, strInsDate);
    //        i++;
    //        //Update EmpINfo BasicSal
    //        cmd[i] = objEmpMgr.UpdateEmpInfoBaiscSal(strEmpId, strNewBasicSal, strInsBy, strInsDate, strLastUpFrom);
    //        i++;
    //        //Update BasicSal in Salary Package Details
    //        cmd[i] = objEmpMgr.UpdateSalaryHeadWsAmt(strSalPakId, "1", strNewBasicSal, strInsBy, strInsDate, strLastUpFrom);
    //        i++;
    //        //Update House Rent in Salary Package Details
    //        cmd[i] = objEmpMgr.UpdateSalaryHeadWsAmt(strSalPakId, "2", strHR, strInsBy, strInsDate, strLastUpFrom);
    //        i++;
    //        //Update PF in Salary Package Details
    //        cmd[i] = objEmpMgr.UpdateSalaryHeadWsAmt(strSalPakId, "9", strPF, strInsBy, strInsDate, strLastUpFrom);
    //        i++;           
    //    }
    //    try
    //    {
    //        objDC.MakeTransaction(cmd);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw (ex);
    //    }
    //    finally
    //    {
    //        cmd = null;
    //    }
    //}   

    public DataTable GET_SalPakId(string EmpId)
    {
        SqlCommand command = new SqlCommand("PROC_GET_SalPakId");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDSFromProc(command, "tblSalPakId" + EmpId);
        return objDC.ds.Tables["tblSalPakId" + EmpId];
    }

    public void INSERT_EmpInfoChangeLog(GridView grEmpDesig, string strChangeType, string strInsBy, string strInsDate, string strLastUpFrom)
    {
        int i = 0;
        SqlCommand[] command = new SqlCommand[grEmpDesig.Rows.Count*2];

        string sSeqId = Common.getMaxId("EmpInfoChangeLog", "SeqId");

        if (grEmpDesig.Rows.Count > 0)
        {
            foreach (GridViewRow gRow in grEmpDesig.Rows)
            {
                command[i] = InsertEmpInfoChangeLog(sSeqId, gRow.Cells[0].Text.Trim(), gRow.Cells[1].Text.Trim(), gRow.Cells[2].Text.Trim(),
                    strChangeType, strInsBy, strInsDate, strLastUpFrom);
                sSeqId = Convert.ToString(Convert.ToInt32(sSeqId) + 1);
                i++;

                command[i] = objEmpMgr.InsertEmpInfoLog(gRow.Cells[0].Text.Trim());
                i++;
            }
        }
        try
        {
            objDC.MakeTransaction(command);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            command = null;
        }
    }
    private SqlCommand InsertEmpInfoChangeLog(string strSeqId, string strEmpId, string strPrevId, string strCurntId, string strChangeType,
        string strInsBy, string strInsDate, string strLastUpFrom)
    {
        SqlCommand command = new SqlCommand("PROC_INSERT_EmpInfoChangeLog");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_SeqId = command.Parameters.Add("SeqId", SqlDbType.BigInt);
        p_SeqId.Direction = ParameterDirection.Input;
        p_SeqId.Value = Convert.ToInt32(strSeqId);

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_PrevId = command.Parameters.Add("PrevId", SqlDbType.VarChar);
        p_PrevId.Direction = ParameterDirection.Input;
        p_PrevId.Value = strPrevId;

        SqlParameter p_CurntId = command.Parameters.Add("CurntId", SqlDbType.VarChar);
        p_CurntId.Direction = ParameterDirection.Input;
        p_CurntId.Value = strCurntId;

        SqlParameter p_ChangeType = command.Parameters.Add("ChangeType", SqlDbType.Char);
        p_ChangeType.Direction = ParameterDirection.Input;
        p_ChangeType.Value = strChangeType;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        SqlParameter p_LastUpdateFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdateFrom.Direction = ParameterDirection.Input;
        p_LastUpdateFrom.Value = strLastUpFrom;

        return command;
    }

    public void InsertBonusArrearImportData(GridView gr, string strFinYear,
        string strSheadID, string strInsBy, string strInsDate)
    {
        SqlCommand[] cmd = new SqlCommand[gr.Rows.Count];
        int i = 0;
        long lngVID = objDC.GerMaxIDNumber("BonusAllowance", "VID");

        foreach (GridViewRow gRow in gr.Rows)
        {
            
            cmd[i] = new SqlCommand("proc_Payroll_Insert_BonusAllowance");
            cmd[i].CommandType = CommandType.StoredProcedure;

            SqlParameter p_VID = cmd[i].Parameters.Add("VID", SqlDbType.BigInt);
            p_VID.Direction = ParameterDirection.Input;
            p_VID.Value = lngVID;

            SqlParameter p_EMPID = cmd[i].Parameters.Add("EMPID", SqlDbType.Char);
            p_EMPID.Direction = ParameterDirection.Input;
            p_EMPID.Value = gRow.Cells[1].Text.Trim();//Pay Emp Id

            SqlParameter p_EMPTYPEID = cmd[i].Parameters.Add("EMPTYPEID", DBNull.Value);
            p_EMPTYPEID.Direction = ParameterDirection.Input;
            p_EMPTYPEID.IsNullable = true;
            p_EMPTYPEID.Value = "1";//gr.DataKeys[gRow.DataItemIndex].Values[2].ToString().Trim();

            SqlParameter p_VMONTH = cmd[i].Parameters.Add("VMONTH", SqlDbType.BigInt);
            p_VMONTH.Direction = ParameterDirection.Input;
            p_VMONTH.Value = gRow.Cells[4].Text.Trim();

            SqlParameter p_VYEAR = cmd[i].Parameters.Add("VYEAR", SqlDbType.BigInt);
            p_VYEAR.Direction = ParameterDirection.Input;
            p_VYEAR.Value = gRow.Cells[5].Text.Trim();

            SqlParameter p_FISCALYRID = cmd[i].Parameters.Add("FISCALYRID", SqlDbType.BigInt);
            p_FISCALYRID.Direction = ParameterDirection.Input;
            p_FISCALYRID.Value = strFinYear;

            SqlParameter p_SHEADID = cmd[i].Parameters.Add("SHEADID", SqlDbType.BigInt);
            p_SHEADID.Direction = ParameterDirection.Input;
            p_SHEADID.Value = "19";

            SqlParameter p_EMPBASIC = cmd[i].Parameters.Add("EMPBASIC", SqlDbType.Decimal);
            p_EMPBASIC.Direction = ParameterDirection.Input;
            p_EMPBASIC.Value = gRow.Cells[6].Text.Trim();

            SqlParameter p_PAYAMT = cmd[i].Parameters.Add("PAYAMT", SqlDbType.Decimal);
            p_PAYAMT.Direction = ParameterDirection.Input;
            p_PAYAMT.Value = gRow.Cells[7].Text.Trim();

            SqlParameter p_ISPRORATA = cmd[i].Parameters.Add("ISPRORATA", SqlDbType.Char);
            p_ISPRORATA.Direction = ParameterDirection.Input;
            p_ISPRORATA.Value = "Y";

            SqlParameter p_VSTATUS = cmd[i].Parameters.Add("VSTATUS", SqlDbType.Char);
            p_VSTATUS.Direction = ParameterDirection.Input;
            p_VSTATUS.Value = "D";

            SqlParameter p_INSERTTEDBY = cmd[i].Parameters.Add("INSERTTEDBY", SqlDbType.VarChar);
            p_INSERTTEDBY.Direction = ParameterDirection.Input;
            p_INSERTTEDBY.Value = strInsBy;

            SqlParameter p_INSERTTEDDATE = cmd[i].Parameters.Add("INSERTTEDDATE", SqlDbType.Char);
            p_INSERTTEDDATE.Direction = ParameterDirection.Input;
            p_INSERTTEDDATE.Value = strInsDate;

            SqlParameter p_RELIGION = cmd[i].Parameters.Add("RELIGIONId", SqlDbType.VarChar);
            p_RELIGION.Direction = ParameterDirection.Input;
            p_RELIGION.Value = gRow.Cells[2].Text.Trim();

            SqlParameter p_FestivalId = cmd[i].Parameters.Add("FestivalId", SqlDbType.VarChar);
            p_FestivalId.Direction = ParameterDirection.Input;
            p_FestivalId.Value = gRow.Cells[3].Text.Trim();

            SqlParameter p_PRORATADAYS = cmd[i].Parameters.Add("PRORATADAYS", SqlDbType.Decimal);
            p_PRORATADAYS.Direction = ParameterDirection.Input;
            p_PRORATADAYS.Value = Convert.ToDecimal("0");

            SqlParameter p_FESTIVEDATE = cmd[i].Parameters.Add("FESTIVEDATE", SqlDbType.DateTime);
            p_FESTIVEDATE.Direction = ParameterDirection.Input;
            p_FESTIVEDATE.Value = Common.ReturnDate(gRow.Cells[8].Text.Trim());
            i++;
            lngVID++;
        }
        objDC.MakeTransaction(cmd);
    }

    #region Tax AssessMent Adjust/ All Arrear Adj.
    public void InsertPayrollArrear(GridView gr,string strMonth,string strFinYear, string strInsBy,string strInsDate)
    {
        SqlCommand[] cmd = new SqlCommand[gr.Rows.Count];
        int i = 0;
        long lnTransID = Convert.ToInt64(Common.getMaxId("PayrollArrear", "TransID"));

        string strSheadID = "";
        decimal decAmount = 0;
        foreach (GridViewRow gRow in gr.Rows)
        {
            decAmount = Common.RoundDecimal(gRow.Cells[5].Text.Trim(), 0);
            decAmount = decAmount * -1;

                cmd[i] = new SqlCommand("Proc_Payroll_Insert_PayrollArrear");
                cmd[i].CommandType = CommandType.StoredProcedure;

                SqlParameter p_TransID = cmd[i].Parameters.Add("TransID", SqlDbType.BigInt);
                p_TransID.Direction = ParameterDirection.Input;
                p_TransID.Value = lnTransID;

                SqlParameter p_PAYEMPID = cmd[i].Parameters.Add("EMPID", SqlDbType.Char);
                p_PAYEMPID.Direction = ParameterDirection.Input;
                p_PAYEMPID.Value = gRow.Cells[1].Text.Trim();

                SqlParameter p_VMONTH = cmd[i].Parameters.Add("VMONTH", SqlDbType.BigInt);
                p_VMONTH.Direction = ParameterDirection.Input;
                p_VMONTH.Value = strMonth;

                SqlParameter p_FiscalYrID = cmd[i].Parameters.Add("FiscalYrID", SqlDbType.BigInt);
                p_FiscalYrID.Direction = ParameterDirection.Input;
                p_FiscalYrID.Value = strFinYear;

                SqlParameter p_SHeadID = cmd[i].Parameters.Add("SHeadID", SqlDbType.BigInt);
                p_SHeadID.Direction = ParameterDirection.Input;
                p_SHeadID.Value = 16;

                SqlParameter p_PAYAMT = cmd[i].Parameters.Add("PAYAMT", SqlDbType.Decimal);
                p_PAYAMT.Direction = ParameterDirection.Input;
                p_PAYAMT.Value = decAmount;

                SqlParameter p_InsertedBy = cmd[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                p_InsertedBy.Direction = ParameterDirection.Input;
                p_InsertedBy.Value = strInsBy;

                SqlParameter p_InsertedDate = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                p_InsertedDate.Direction = ParameterDirection.Input;
                p_InsertedDate.Value = strInsDate;

                i++;
                lnTransID++;
            
        }
        objDC.MakeTransaction(cmd);
    }
    #endregion

    #region Basic Upload
    
    //public void UpdateBasic(GridView grEList, string strFiscalYrId, string strGradeId, string strPercentage, string strInsBy, string strInsDate, 
    //    string strLastUpFrom)
    //{
    //    long i = 0;               

    //    string strLogId = "";

    //    string strEmpId = "";
    //    string strPayEmpId = "";

    //    string strSalPakId = "";
    //    string strBasicSal = "";

    //    DataTable dTable = new DataTable();

    //    dsPayroll_SalaryPackage objDs = new dsPayroll_SalaryPackage();
    //    Payroll_PaySlipOptionMgr objOptMgr = new Payroll_PaySlipOptionMgr();

    //    SqlCommand[] command = new SqlCommand[(grEList.Rows.Count*3) + (grEList.Rows.Count*3)];

    //    long lngId = Convert.ToInt64(Common.getMaxId("EmpSalaryAmendment", "LogId"));

    //    foreach (GridViewRow gRow in grEList.Rows)
    //    {
    //        strLogId = lngId.ToString();
    //        strEmpId = gRow.Cells[1].Text.Trim();
    //        strPayEmpId = gRow.Cells[2].Text.Trim();

    //        dTable = GET_SalPakId(gRow.Cells[1].Text.Trim());
    //        strSalPakId = dTable.Rows[0]["SalPakId"].ToString();

    //        strBasicSal = Convert.ToString(Math.Round(Convert.ToDecimal(gRow.Cells[3].Text.Trim()), 0));

    //        DataTable dtBfPlc = new DataTable();

    //        if (dtBfPlc != null)
    //        {
    //            dtBfPlc.Rows.Clear();
    //            dtBfPlc.Dispose();
    //        }

    //        if (objDs.dtSalPackUpdate != null)
    //        {
    //            objDs.dtSalPackUpdate.Rows.Clear();
    //            objDs.dtSalPackUpdate.Dispose();
    //        }
    //        dtBfPlc=objOptMgr.SelectPayrollBenefitsPolicyData("0");
            
    //        DataRow[] foundPlcRow;
    //        foundPlcRow = null;

    //        //Basic
    //        DataRow nRow1 = objDs.dtSalPackUpdate.NewRow();
    //        nRow1["SHEADID"] = 1;
    //        nRow1["PAYAMT"] = Common.RoundDecimal(strBasicSal, 0);
    //        objDs.dtSalPackUpdate.Rows.Add(nRow1);

    //        //House Rent
    //        foundPlcRow = dtBfPlc.Select("SHEADID=2");
    //        if (foundPlcRow.Length > 0)
    //        {
    //            DataRow nRow2 = objDs.dtSalPackUpdate.NewRow();

    //            nRow2["SHEADID"] = 2;
    //            nRow2["PAYAMT"] = objEmpMgr.GetHeadAmount(Common.RoundDecimal(strBasicSal, 0), Common.RoundDecimal(foundPlcRow[0]["Value"].ToString(), 0));

    //            objDs.dtSalPackUpdate.Rows.Add(nRow2);
    //        }

    //        //PF Allowance 
    //        //dtBfPlc = objOptMgr.SelectPayrollBenefitsPolicyData("9");
    //        foundPlcRow = null;

    //        foundPlcRow = dtBfPlc.Select("SHEADID=9");
    //        if (foundPlcRow.Length > 0)
    //        {
    //            DataRow nRow3 = objDs.dtSalPackUpdate.NewRow();

    //            nRow3["SHEADID"] = 9;
    //            nRow3["PAYAMT"] = objEmpMgr.GetHeadAmount(Common.RoundDecimal(strBasicSal, 0), Common.RoundDecimal(foundPlcRow[0]["Value"].ToString(), 0));

    //            objDs.dtSalPackUpdate.Rows.Add(nRow3);
    //        }
    //        objDs.dtSalPackUpdate.AcceptChanges();

    //        //InsertEmpSalaryAmendment(strLogId, strEmpId, "30", "", strInsDate,
    //        //    strBasicSal, strInsBy, strInsDate, "N", "N", strSalPakId, objDs.dtSalPackUpdate,i);

    //        //Insert into Salary Amendment Log Table

    //        command[i] = new SqlCommand("proc_Insert_EmpSalaryAmendment");
    //        command[i].CommandType = CommandType.StoredProcedure;

    //        SqlParameter p_ConfirmId = command[i].Parameters.Add("LogId", SqlDbType.BigInt);
    //        p_ConfirmId.Direction = ParameterDirection.Input;
    //        p_ConfirmId.Value = strLogId;

    //        SqlParameter p_EmpId = command[i].Parameters.Add("EmpId", SqlDbType.Char);
    //        p_EmpId.Direction = ParameterDirection.Input;
    //        p_EmpId.Value = strEmpId;

    //        SqlParameter p_ActionId = command[i].Parameters.Add("ActionId", SqlDbType.BigInt);
    //        p_ActionId.Direction = ParameterDirection.Input;
    //        p_ActionId.Value = "30";

    //        SqlParameter p_ConfirmDate = command[i].Parameters.Add("ActionDate", SqlDbType.DateTime);
    //        p_ConfirmDate.Direction = ParameterDirection.Input;
    //            p_ConfirmDate.Value = strInsDate;

    //        SqlParameter p_BasicSal = command[i].Parameters.Add("BasicSal", DBNull.Value);
    //        p_BasicSal.Direction = ParameterDirection.Input;
    //        p_BasicSal.IsNullable = true;
    //        if (strBasicSal != "")
    //            p_BasicSal.Value = strBasicSal;

    //        SqlParameter p_InsertedBy = command[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //        p_InsertedBy.Direction = ParameterDirection.Input;
    //        p_InsertedBy.Value = strInsBy;

    //        SqlParameter p_InsertedDate = command[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //        p_InsertedDate.Direction = ParameterDirection.Input;
    //        p_InsertedDate.Value = strInsDate;

    //        SqlParameter p_IsUpdate = command[i].Parameters.Add("IsUpdate", SqlDbType.Char);
    //        p_IsUpdate.Direction = ParameterDirection.Input;
    //        p_IsUpdate.Value = "N";

    //        SqlParameter p_IsDelete = command[i].Parameters.Add("IsDelete", SqlDbType.Char);
    //        p_IsDelete.Direction = ParameterDirection.Input;
    //        p_IsDelete.Value = "N";

    //        i++;
    //        command[i] = objEmpMgr.InsertEmpInfoLog(strEmpId);
    //        i++;
    //        //command[i] = objEmpMgr.UpdateEmpSalaryAmendment(strEmpId, "30", strInsDate, strBasicSal, strInsBy, strInsDate);
    //        //i++;
    //        //command[i] = this.InsertEmpActionLog(strLogId,strEmpId, "30", "Amendment of Salary", strInsDate, strInsBy, strInsDate);
    //        //i++;
    //        //Housing & PF Allowance Update                 
    //        if (objDs.dtSalPackUpdate.Rows.Count > 0)
    //        {
    //            foreach (SqlCommand cmdSal in objEmpMgr.GetSalPackDetUpdateCommand(objDs.dtSalPackUpdate, strSalPakId, strInsBy, strInsDate, "Salary Amendment"))
    //            {
    //                command[i] = cmdSal;
    //                i++;
    //            }
    //        }

    //        lngId = lngId + 1;
    //    }
    //    try
    //    {
    //        objDC.MakeTransaction(command);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw (ex);
    //    }
    //    finally
    //    {
    //        command = null;
    //    }
    //}

    //Insert Employee ActionLog
    public SqlCommand InsertEmpActionLog(string strLogId,string strEmpId, string strActionId, string strActionName, string strActionDate, string strInsertedBy, string strInsertedDate)
    {
        SqlCommand command = new SqlCommand("proc_Insert_EmpAdvice_ActionLog");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LogId = command.Parameters.Add("LogId", SqlDbType.Char);
        p_LogId.Direction = ParameterDirection.Input;
        p_LogId.Value = strLogId;

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpId;

        SqlParameter p_ActionId = command.Parameters.Add("ActionId", SqlDbType.BigInt);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.Value = strActionId;

        SqlParameter p_ActionName = command.Parameters.Add("ActionName", SqlDbType.VarChar);
        p_ActionName.Direction = ParameterDirection.Input;
        p_ActionName.Value = strActionName;

        SqlParameter p_ActionDate = command.Parameters.Add("ActionDate", DBNull.Value);
        p_ActionDate.Direction = ParameterDirection.Input;
        p_ActionDate.IsNullable = true;
        if (strActionDate != "")
            p_ActionDate.Value = strActionDate;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsertedDate;

        return command;
    }
    
    //public void InsertEmpSalaryAmendment(string strLogId, string strEmpId, string strActionId, string strActionName, string strActionDate,
    //   string strBasicSal, string strInsertedBy, string strInsertedDate, string IsUpdate, string IsDelete, string strSalPackId, 
    //    DataTable dtSalPackUpdate,int i)
    //{
    //    SqlCommand[] command = new SqlCommand[4 + dtSalPackUpdate.Rows.Count];

    //    command[i] = new SqlCommand("proc_Insert_EmpSalaryAmendment");
    //    command[i].CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_ConfirmId = command[i].Parameters.Add("LogId", SqlDbType.BigInt);
    //    p_ConfirmId.Direction = ParameterDirection.Input;
    //    p_ConfirmId.Value = strLogId;

    //    SqlParameter p_EmpId = command[i].Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = strEmpId;

    //    SqlParameter p_ActionId = command[i].Parameters.Add("ActionId", SqlDbType.BigInt);
    //    p_ActionId.Direction = ParameterDirection.Input;
    //    p_ActionId.Value = strActionId;

    //    SqlParameter p_ConfirmDate = command[i].Parameters.Add("ActionDate", DBNull.Value);
    //    p_ConfirmDate.Direction = ParameterDirection.Input;
    //    p_ConfirmDate.IsNullable = true;
    //    if (strActionDate != "")
    //        p_ConfirmDate.Value = strActionDate;

    //    SqlParameter p_BasicSal = command[i].Parameters.Add("BasicSal", DBNull.Value);
    //    p_BasicSal.Direction = ParameterDirection.Input;
    //    p_BasicSal.IsNullable = true;
    //    if (strBasicSal != "")
    //        p_BasicSal.Value = strBasicSal;

    //    SqlParameter p_InsertedBy = command[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy.Direction = ParameterDirection.Input;
    //    p_InsertedBy.Value = strInsertedBy;

    //    SqlParameter p_InsertedDate = command[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.Value = strInsertedDate;

    //    SqlParameter p_IsUpdate = command[i].Parameters.Add("IsUpdate", SqlDbType.Char);
    //    p_IsUpdate.Direction = ParameterDirection.Input;
    //    p_IsUpdate.Value = IsUpdate;

    //    SqlParameter p_IsDelete = command[i].Parameters.Add("IsDelete", SqlDbType.Char);
    //    p_IsDelete.Direction = ParameterDirection.Input;
    //    p_IsDelete.Value = IsDelete;

    //    i++;
    //    if (IsUpdate == "N")
    //    {
    //        command[i] = objEmpMgr.InsertEmpInfoLog(strEmpId);
    //        i++;
    //        command[i] = objEmpMgr.UpdateEmpSalaryAmendment(strEmpId, strActionId, strActionDate, strBasicSal, strInsertedBy, strInsertedDate);
    //        i++;
    //        command[i] = objEmpMgr.InsertEmpActionLog(strEmpId, strActionId, "Amendment of Salary", strActionDate, strInsertedBy, strInsertedDate);
    //        i++;
    //        //Housing & PF Allowance Update                 
    //        if (dtSalPackUpdate.Rows.Count > 0)
    //        {
    //            foreach (SqlCommand cmdSal in objEmpMgr.GetSalPackDetUpdateCommand(dtSalPackUpdate, strSalPackId, strInsertedBy, strInsertedDate, "Salary Amendment"))
    //            {
    //                command[i] = cmdSal;
    //                i++;
    //            }
    //        }
    //    }
    //    try
    //    {
    //        objDC.MakeTransaction(command);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw (ex);
    //    }
    //    finally
    //    {
    //        command = null;
    //    }
    //}

    #endregion

    #region Employee Document


    public DataSet SelectEmpDoc(string empId)
        {
            DataSet myDs = new DataSet();
            SqlCommand command = new SqlCommand("Select_EmpDocument");

            SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = empId;
            //objDC.CreateDSFromProc(command, "tblKPIReview");
            myDs =(DataSet)objDC.Get_Rpt_SalReconDetails(command);
            return myDs;// objDC.ds.Tables["tblKPIReview"];
        }


    public void InsertEmpDocument(clsCommonSetup clsCommon, string  sEmpID,string sfileName,string sFilePath,  string IsDelete)
    {
        // sproc functionality
        SqlCommand command = new SqlCommand("proc_InUp_EmpDocument");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("ID", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = Convert.ToInt32(clsCommon.ID);

        SqlParameter p_Name = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = sEmpID;

        SqlParameter p_GroupId = command.Parameters.Add("FileName", SqlDbType.VarChar);
        p_GroupId.Direction = ParameterDirection.Input;
        p_GroupId.Value = sfileName;

        SqlParameter p_IndTypeId = command.Parameters.Add("FilePath", SqlDbType.VarChar);
        p_IndTypeId.Direction = ParameterDirection.Input;
        p_IndTypeId.Value = sFilePath;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = clsCommon.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = clsCommon.InsertedDate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        try
        {
            objDC.ExecuteQuery(command);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            command = null;
        }
    }



    public void DeleteDocument(string fpath)
    {
        string strSQL = "update EmpDocInfo set IsDeleted='Y' where FilePath='" + fpath+"'";
        SqlCommand  cmd = new SqlCommand(strSQL);
                    cmd.CommandType = CommandType.Text;
                    objDC.ExecuteQuery(cmd);

    }

    
    #endregion
    public UploadToolManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
