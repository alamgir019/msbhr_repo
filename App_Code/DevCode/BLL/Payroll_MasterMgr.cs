using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

/// <summary>
/// Summary description for PayrollTableMgr
/// </summary>
public class Payroll_MasterMgr
{
    DBConnector objDC = new DBConnector();

    #region Common Data Save
    public void SaveData(DataTable dtData, string CmdType)
    {
        try
        {
            objDC.SaveDataTable(dtData, CmdType);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }

    public void SaveMultiTableData(List<DataTable> dtList, string CmdType)
    {
        try
        {
            List<SqlCommand> lstCommand = new List<SqlCommand>();
            for (int i = 0; i < dtList.Count; i++)
            {
                if (i == 0)
                {
                    foreach (DataRow dRow in dtList[i].Rows)
                    {
                        lstCommand.Add(objDC.GenerateDML(dRow, CmdType));
                    }
                }
                else
                {

                    if (CmdType == "U")
                    {
                        lstCommand.Add(objDC.DeleteData(dtList[0].Rows[0], dtList[i].TableName.ToString().Trim()));
                    }
                    foreach (DataRow dRow in dtList[i].Rows)
                    {
                        lstCommand.Add(objDC.GenerateDML(dRow, CmdType == "U" ? "I" : CmdType));
                    }
                }
            }

            objDC.MakeTransaction(lstCommand);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }

    #endregion
   
    FileUploadManager objFileUpMgr = new FileUploadManager();
    public double USDRate = 0;
    public Payroll_MasterMgr()
	{
		//
		// TODO: Add constructor logic here
		//
    }

    #region Insert Update Delete From Payroll Master Tables By Store procedure
    // Insert or Update  or Delete Data of Department table
    public void InsertSalaryHead(Payroll_SalaryHead PaySalHead, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Payroll_Insert_SalaryHead");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_SHEADID = command.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = PaySalHead.SHEADID;

        SqlParameter p_HEADNAME = command.Parameters.Add("HEADNAME", SqlDbType.VarChar);
        p_HEADNAME.Direction = ParameterDirection.Input;
        p_HEADNAME.Value = PaySalHead.HEADNAME;

        SqlParameter p_HEADNATURE = command.Parameters.Add("HEADNATURE", SqlDbType.Char);
        p_HEADNATURE.Direction = ParameterDirection.Input;
        p_HEADNATURE.Value = PaySalHead.HEADNATURE;

        SqlParameter p_SHDESC = command.Parameters.Add("SHDESC", SqlDbType.VarChar);
        p_SHDESC.Direction = ParameterDirection.Input;
        p_SHDESC.Value = PaySalHead.SHDESC;

        SqlParameter p_ISOTHERPAYMENT = command.Parameters.Add("ISOTHERPAYMENT", SqlDbType.Char);
        p_ISOTHERPAYMENT.Direction = ParameterDirection.Input;
        p_ISOTHERPAYMENT.Value = PaySalHead.ISOTHERPAYMENT;

        SqlParameter p_DEFALTAMNT = command.Parameters.Add("DEFALTAMNT", SqlDbType.Decimal);
        p_DEFALTAMNT.Direction = ParameterDirection.Input;
        p_DEFALTAMNT.Value = PaySalHead.DEFALTAMNT;

        SqlParameter p_ISACTIVE = command.Parameters.Add("ISACTIVE", SqlDbType.Char);
        p_ISACTIVE.Direction = ParameterDirection.Input;
        p_ISACTIVE.Value = PaySalHead.ISACTIVE;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = PaySalHead.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = PaySalHead.InsertedDate;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_ISBASIC = command.Parameters.Add("ISBASIC", SqlDbType.Char);
        p_ISBASIC.Direction = ParameterDirection.Input;
        p_ISBASIC.Value = PaySalHead.IsBasic;

        SqlParameter p_ISPF = command.Parameters.Add("ISPF", SqlDbType.Char);
        p_ISPF.Direction = ParameterDirection.Input;
        p_ISPF.Value = PaySalHead.IsPF;

        SqlParameter p_IsHouseRent = command.Parameters.Add("IsHouseRent", SqlDbType.Char);
        p_IsHouseRent.Direction = ParameterDirection.Input;
        p_IsHouseRent.Value = PaySalHead.IsHouseRent;

        SqlParameter p_IsMedical = command.Parameters.Add("IsMedical", SqlDbType.Char);
        p_IsMedical.Direction = ParameterDirection.Input;
        p_IsMedical.Value = PaySalHead.IsMedical;

        SqlParameter p_SHORTNAME = command.Parameters.Add("SHORTNAME", SqlDbType.Char);
        p_SHORTNAME.Direction = ParameterDirection.Input;
        p_SHORTNAME.Value = PaySalHead.ShortName;

        SqlParameter p_NATURALCODE = command.Parameters.Add("NATURALCODE", SqlDbType.Char);
        p_NATURALCODE.Direction = ParameterDirection.Input;
        p_NATURALCODE.Value = PaySalHead.NaturalCode;

        SqlParameter p_ITEMCATEGORY = command.Parameters.Add("ITEMCATEGORY", SqlDbType.Char);
        p_ITEMCATEGORY.Direction = ParameterDirection.Input;
        p_ITEMCATEGORY.Value = PaySalHead.ItemCategory;

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

    public void InsertGrossSalHead(GridView grGrossSalary,int TotCount)
    {
        SqlCommand[] command = new SqlCommand[TotCount + 1];
        int i = 0;
        command[i] = new SqlCommand("proc_Payroll_Delete_GROSSSALHEAD");
        command[i].CommandType = CommandType.StoredProcedure;

        i++;

        foreach (GridViewRow gRow in grGrossSalary.Rows)
        {
            CheckBox chBox = new CheckBox();
            chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            if (chBox.Checked == true)
            {

                command[i] = new SqlCommand("proc_Payroll_Insert_GROSSSALHEAD");
                command[i].CommandType = CommandType.StoredProcedure;

                HiddenField hfSHEADID = (HiddenField)gRow.Cells[1].FindControl("hfSHEADID");                

                SqlParameter p_SHEADID = command[i].Parameters.Add("SHEADID", SqlDbType.BigInt);
                p_SHEADID.Direction = ParameterDirection.Input;
                p_SHEADID.Value = hfSHEADID.Value.ToString();

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

    public void InsertTotalSalHead(GridView grTotalSalary, int TotCount)
    {
        SqlCommand[] command = new SqlCommand[TotCount + 1];
        int i = 0;
        command[i] = new SqlCommand("proc_Payroll_Delete_SALARYHEADWITHSEQ");
        command[i].CommandType = CommandType.StoredProcedure;

        i++;

        foreach (GridViewRow gRow in grTotalSalary.Rows)
        {
            CheckBox chBox = new CheckBox();
            chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            if (chBox.Checked == true)
            {

                command[i] = new SqlCommand("proc_Payroll_Insert_SALARYHEADWITHSEQ");
                command[i].CommandType = CommandType.StoredProcedure;

                HiddenField hfSHEADID = (HiddenField)gRow.Cells[1].FindControl("hfSHEADID");
                TextBox txtSN1 = (TextBox)gRow.Cells[3].FindControl("txtSeqNo");
                HiddenField hfDT = (HiddenField)gRow.Cells[3].FindControl("hfDispType");

                SqlParameter p_SHEADID = command[i].Parameters.Add("SHEADID", SqlDbType.BigInt);
                p_SHEADID.Direction = ParameterDirection.Input;
                p_SHEADID.Value = hfSHEADID.Value.ToString();

                SqlParameter p_SEQNO = command[i].Parameters.Add("SEQNO", SqlDbType.BigInt);
                p_SEQNO.Direction = ParameterDirection.Input;
                p_SEQNO.Value = txtSN1.Text.Trim();

                SqlParameter p_DISPLAYTYPE = command[i].Parameters.Add("DISPLAYTYPE", SqlDbType.Char);
                p_DISPLAYTYPE.Direction = ParameterDirection.Input;
                p_DISPLAYTYPE.Value = hfDT.Value.ToString();

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

    public void InsertPaySlipSalHeadSeq(string strSeqNo, string strSHEADID, string IsUpdate, string IsDelete, string strDisplayType)
    {
        SqlCommand command = new SqlCommand("proc_Payroll_Insert_PaySlipSalHeadSeq");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_SEQNO = command.Parameters.Add("SEQNO", SqlDbType.Decimal);
        p_SEQNO.Direction = ParameterDirection.Input;
        p_SEQNO.Value = strSeqNo;

        SqlParameter p_SHEADID = command.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = strSHEADID;

        SqlParameter p_DISPLAYTYPE = command.Parameters.Add("DISPLAYTYPE", SqlDbType.Char);
        p_DISPLAYTYPE.Direction = ParameterDirection.Input;
        p_DISPLAYTYPE.Value = strDisplayType;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

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

    public void InsertBonusPackage(Payroll_BonusPak PayBonus, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Payroll_Insert_BonusPak");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_BPID = command.Parameters.Add("BPID", SqlDbType.BigInt);
        p_BPID.Direction = ParameterDirection.Input;
        p_BPID.Value = PayBonus.BPID;

        SqlParameter p_BPTITLE = command.Parameters.Add("BPTITLE", SqlDbType.VarChar);
        p_BPTITLE.Direction = ParameterDirection.Input;
        p_BPTITLE.Value = PayBonus.BPTITLE;

        SqlParameter p_BPDESC = command.Parameters.Add("BPDESC", SqlDbType.VarChar);
        p_BPDESC.Direction = ParameterDirection.Input;
        p_BPDESC.Value = PayBonus.BPDESC;

        SqlParameter p_BAMT = command.Parameters.Add("BAMT", SqlDbType.Decimal);
        p_BAMT.Direction = ParameterDirection.Input;
        p_BAMT.Value = PayBonus.BAMT;

        SqlParameter p_ISINPERCENT = command.Parameters.Add("ISINPERCENT", SqlDbType.Char);
        p_ISINPERCENT.Direction = ParameterDirection.Input;
        p_ISINPERCENT.Value = PayBonus.ISINPERCENT;

        SqlParameter p_SHEADID = command.Parameters.Add("SHEADID", DBNull.Value);
        p_SHEADID.Direction = ParameterDirection.Input;

        p_SHEADID.IsNullable = true;
        if (PayBonus.SHEADID != null)
            p_SHEADID.Value = PayBonus.SHEADID;


        //p_SHEADID.IsNullable = true;
        //if (PayBonus.SHEADID != "")
        //    p_SHEADID.Value = PayBonus.SHEADID;

        SqlParameter p_NUMOFPAY = command.Parameters.Add("NUMOFPAY", DBNull.Value);
        p_NUMOFPAY.Direction = ParameterDirection.Input;
        p_NUMOFPAY.IsNullable = true;
        if (PayBonus.NUMOFPAY != "")
            p_NUMOFPAY.Value = PayBonus.NUMOFPAY;

        SqlParameter p_CurrId = command.Parameters.Add("CurrId", DBNull.Value);
        p_CurrId.Direction = ParameterDirection.Input;
        p_CurrId.IsNullable = true;
        if (PayBonus.CurrId != "-1")
            p_CurrId.Value = PayBonus.CurrId;

        SqlParameter p_ISACTIVE = command.Parameters.Add("ISACTIVE", SqlDbType.Char);
        p_ISACTIVE.Direction = ParameterDirection.Input;
        p_ISACTIVE.Value = PayBonus.ISACTIVE;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = PayBonus.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = PayBonus.InsertedDate;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

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



    public void InsertSalaryPakMst(Payroll_SalaryPakMst SLmp, string IsUpdate, string IsDelete, GridView grSalHead,string strEmpID)
    {
        SqlCommand[] cmd;
        cmd = new SqlCommand[grSalHead.Rows.Count + 3];

        // Delete all from Salary Packgae Details
        cmd[0] = new SqlCommand("proc_Payroll_Delete_SalaryPakDet");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_SalPakId = cmd[0].Parameters.Add("SalPakId", SqlDbType.Decimal);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = SLmp.SalPakId;

        //Insert into Salary Package Master
        cmd[1] = new SqlCommand("proc_Payroll_Insert_SalaryPakMst");
        cmd[1].CommandType = CommandType.StoredProcedure;

        p_SalPakId = cmd[1].Parameters.Add("SalPakId", SqlDbType.BigInt);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = SLmp.SalPakId;

        SqlParameter p_SPTitle = cmd[1].Parameters.Add("SPTitle", SqlDbType.VarChar);
        p_SPTitle.Direction = ParameterDirection.Input;
        p_SPTitle.Value = SLmp.SPTitle;

        SqlParameter p_SPDesc = cmd[1].Parameters.Add("SPDesc", SqlDbType.VarChar);
        p_SPDesc.Direction = ParameterDirection.Input;
        p_SPDesc.Value = SLmp.SPDesc;

        SqlParameter p_CurrId = cmd[1].Parameters.Add("CurrId", SqlDbType.BigInt);
        p_CurrId.Direction = ParameterDirection.Input;
        p_CurrId.Value = SLmp.CurrId;

        SqlParameter p_WillConvert = cmd[1].Parameters.Add("WillConvert", SqlDbType.Char);
        p_WillConvert.Direction = ParameterDirection.Input;
        p_WillConvert.Value = SLmp.WillConvert;

        SqlParameter p_PayType = cmd[1].Parameters.Add("PayType", SqlDbType.BigInt);
        p_PayType.Direction = ParameterDirection.Input;
        p_PayType.Value = SLmp.PayType;

        SqlParameter p_OTAmt = cmd[1].Parameters.Add("OTAmt", SqlDbType.Decimal);
        p_OTAmt.Direction = ParameterDirection.Input;
        p_OTAmt.Value = SLmp.OTAmt;

        SqlParameter p_IsInPercent = cmd[1].Parameters.Add("IsInPercent", SqlDbType.Char);
        p_IsInPercent.Direction = ParameterDirection.Input;
        p_IsInPercent.Value = SLmp.IsInPercent;

        SqlParameter p_SalHeadID = cmd[1].Parameters.Add("SalHeadID", DBNull.Value);
        p_SalHeadID.Direction = ParameterDirection.Input;
        p_SalHeadID.IsNullable = true;
        if (string.IsNullOrEmpty(SLmp.SalHeadID) == false)
            p_SalHeadID.Value = SLmp.SalHeadID;

        SqlParameter p_AttBonusAmt = cmd[1].Parameters.Add("AttBonusAmt", SqlDbType.Decimal);
        p_AttBonusAmt.Direction = ParameterDirection.Input;
        p_AttBonusAmt.Value = SLmp.AttBonusAmt;

        SqlParameter p_IsBonusInPer = cmd[1].Parameters.Add("IsBonusInPer", SqlDbType.Char);
        p_IsBonusInPer.Direction = ParameterDirection.Input;
        p_IsBonusInPer.Value = SLmp.IsBonusInPer;

        SqlParameter p_SalHeadIDBonus = cmd[1].Parameters.Add("SalHeadIDBonus", DBNull.Value);
        p_SalHeadIDBonus.Direction = ParameterDirection.Input;
        p_SalHeadIDBonus.IsNullable = true;
        if (string.IsNullOrEmpty(SLmp.SalHeadIDBonus)==false)
            p_SalHeadIDBonus.Value = SLmp.SalHeadIDBonus;

        SqlParameter p_LateCount = cmd[1].Parameters.Add("LateCount", SqlDbType.BigInt);
        p_LateCount.Direction = ParameterDirection.Input;
        p_LateCount.Value = SLmp.LateCount;

        SqlParameter p_LateSalCount = cmd[1].Parameters.Add("LateSalCount", SqlDbType.BigInt);
        p_LateSalCount.Direction = ParameterDirection.Input;
        p_LateSalCount.Value = SLmp.LateSalCount;

        SqlParameter p_LateSalHead = cmd[1].Parameters.Add("LateSalHead", DBNull.Value);
        p_LateSalHead.Direction = ParameterDirection.Input;
        p_LateSalHead.IsNullable = true;
        if (string.IsNullOrEmpty(SLmp.LateSalHead)==false)
            p_LateSalHead.Value = SLmp.LateSalHead;

        SqlParameter p_TotalGrossSal = cmd[1].Parameters.Add("TotalGrossSal", SqlDbType.Decimal);
        p_TotalGrossSal.Direction = ParameterDirection.Input;
        p_TotalGrossSal.Value = SLmp.TotalGrossSal;

        SqlParameter p_IsAutoGrossCalc = cmd[1].Parameters.Add("IsAutoGrossCalc", SqlDbType.Char);
        p_IsAutoGrossCalc.Direction = ParameterDirection.Input;
        p_IsAutoGrossCalc.Value = SLmp.IsAutoGrossCalc;

        SqlParameter p_totalSalary = cmd[1].Parameters.Add("totalSalary", SqlDbType.Decimal);
        p_totalSalary.Direction = ParameterDirection.Input;
        p_totalSalary.Value = SLmp.totalSalary;

        SqlParameter p_IsActive = cmd[1].Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = SLmp.IsActive;

        SqlParameter p_IsCompFacility = cmd[1].Parameters.Add("IsCompFacility", SqlDbType.Char);
        p_IsCompFacility.Direction = ParameterDirection.Input;
        p_IsCompFacility.Value = SLmp.IsCompFacility;

        SqlParameter p_PackageID = cmd[1].Parameters.Add("PackageID", DBNull.Value);
        p_PackageID.Direction = ParameterDirection.Input;
        p_PackageID.IsNullable = true;
        if (string.IsNullOrEmpty(SLmp.PackageID) == false)
            p_PackageID.Value = SLmp.PackageID;

        SqlParameter p_InsertedDate = cmd[1].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = SLmp.InsertedDate;

        SqlParameter p_InsertedBy = cmd[1].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = SLmp.InsertedBy;

        SqlParameter p_IsUpdate = cmd[1].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = cmd[1].Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_EmpID = cmd[1].Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        // Insert into Salary Package Details Table
        long i = 2;
        int gRowIndx = 0;
        if (IsDelete == "N")
        {
            foreach (GridViewRow gRow in grSalHead.Rows)
            {

                cmd[i] = InsertSalaryPakDet(SLmp.SalPakId, gRow,
                                            grSalHead.DataKeys[gRowIndx].Values[0].ToString().Trim(),
                                            grSalHead.DataKeys[gRowIndx].Values[1].ToString().Trim(),
                                            grSalHead.DataKeys[gRowIndx].Values[2].ToString().Trim(),
                                            grSalHead.DataKeys[gRowIndx].Values[3].ToString().Trim(),
                                            SLmp.InsertedBy, SLmp.InsertedDate);
                
                i++;
                gRowIndx++;               
            }
        }
        try
        {
            objDC.MakeTransaction(cmd);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            cmd = null;
        }
    }

    public SqlCommand InsertSalaryPakDet(string StrSalPakId, GridViewRow gRow, string strSalHeadID, string strIsPFund, string strAmtCompPay,string strPercentField, string strInsBy, string strInsDate)
    {
        TextBox txtAmt = (TextBox)gRow.Cells[4].FindControl("txtPayAmnt");

        SqlCommand cmd = new SqlCommand("proc_Payroll_Insert_SalaryPakDetls");
        cmd.CommandType = CommandType.StoredProcedure;


        SqlParameter p_SalPakId = cmd.Parameters.Add("SalPakId", SqlDbType.BigInt);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = StrSalPakId;

        SqlParameter p_SHeadId = cmd.Parameters.Add("SHeadId", SqlDbType.BigInt);
        p_SHeadId.Direction = ParameterDirection.Input;
        p_SHeadId.Value = strSalHeadID;

        SqlParameter p_PayAmt = cmd.Parameters.Add("PayAmt", DBNull.Value);
        p_PayAmt.Direction = ParameterDirection.Input;
        p_PayAmt.IsNullable = true;
        if (string.IsNullOrEmpty(txtAmt.Text.Trim()) == false)
            p_PayAmt.Value = txtAmt.Text.Trim();

        SqlParameter p_isInPercent = cmd.Parameters.Add("isInPercent", SqlDbType.Char);
        p_isInPercent.Direction = ParameterDirection.Input;
        p_isInPercent.Value = "N";


        SqlParameter p_PercntField = cmd.Parameters.Add("PercntField", DBNull.Value);
        p_PercntField.Direction = ParameterDirection.Input;
        p_PercntField.IsNullable = true;
        if (string.IsNullOrEmpty(strPercentField) == false)
            p_PercntField.Value = strPercentField == "Gross Payment" ? "-1" : strPercentField;

        SqlParameter p_isBasicSal = cmd.Parameters.Add("isBasicSal", SqlDbType.Char);
        p_isBasicSal.Direction = ParameterDirection.Input;
        p_isBasicSal.Value = gRow.Cells[3].Text.Trim();

        SqlParameter p_ISPFUND = cmd.Parameters.Add("ISPFUND", SqlDbType.Char);
        p_ISPFUND.Direction = ParameterDirection.Input;
        p_ISPFUND.Value = strIsPFund;

        SqlParameter p_AMTCOMPAY = cmd.Parameters.Add("AMTCOMPAY", DBNull.Value);
        p_AMTCOMPAY.Direction = ParameterDirection.Input;
        p_AMTCOMPAY.IsNullable = true;
        if (strAmtCompPay != "")
            p_AMTCOMPAY.Value = strAmtCompPay;

        SqlParameter p_TotAmnt = cmd.Parameters.Add("TotAmnt", SqlDbType.Decimal);
        p_TotAmnt.Direction = ParameterDirection.Input;
        p_TotAmnt.Value = txtAmt.Text.Trim();

        SqlParameter p_IsActive = cmd.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = "Y";

        SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_UpdatedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_UpdatedDate.Direction = ParameterDirection.Input;
        p_UpdatedDate.Value = strInsDate;

        return cmd;
    }


    public void InsertBenefitPakMst(Payroll_BenefitPakMst Beft, string IsUpdate, string IsDelete, string IsActive, GridView grSalHead)
    {
        SqlCommand[] cmd;
        cmd = new SqlCommand[grSalHead.Rows.Count + 3];

        cmd[0] = new SqlCommand("proc_Payroll_Delete_BenefitFacilityDet");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_PackageID = cmd[0].Parameters.Add("PackageID", SqlDbType.Decimal);
        p_PackageID.Direction = ParameterDirection.Input;
        p_PackageID.Value = Beft.PackageID;

        //Insert into Salary Package Master
        cmd[1] = new SqlCommand("proc_Payroll_Insert_BenefitFacilityMST");
        cmd[1].CommandType = CommandType.StoredProcedure;

        p_PackageID = cmd[1].Parameters.Add("PackageID", SqlDbType.BigInt);
        p_PackageID.Direction = ParameterDirection.Input;
        p_PackageID.Value = Beft.PackageID;

        SqlParameter p_PackageName = cmd[1].Parameters.Add("PackageName", SqlDbType.VarChar);
        p_PackageName.Direction = ParameterDirection.Input;
        p_PackageName.Value = Beft.PackageName;

        SqlParameter p_PackageDescription = cmd[1].Parameters.Add("PackageDescription", SqlDbType.VarChar);
        p_PackageDescription.Direction = ParameterDirection.Input;
        p_PackageDescription.Value = Beft.PackageDescription;
        
        SqlParameter p_IsActive = cmd[1].Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = Beft.IsActive;
        
        SqlParameter p_InsertedDate = cmd[1].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = Beft.InsertedDate;

        SqlParameter p_InsertedBy = cmd[1].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = Beft.InsertedBy;

        SqlParameter p_IsUpdate = cmd[1].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = cmd[1].Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        // Insert Benefits Details 
        long i = 2;
        int gRowIndx = 0;
        foreach (GridViewRow gRow in grSalHead.Rows)
        {

            cmd[i] = InsertBenefitPakDet(Beft.PackageID, 
                                        grSalHead.DataKeys[gRowIndx].Values[1].ToString().Trim(),
                                        gRow.Cells[2].Text.Trim(),
                                        gRow.Cells[3].Text.Trim(),
                                        grSalHead.DataKeys[gRowIndx].Values[2].ToString().Trim(),
                                        grSalHead.DataKeys[gRowIndx].Values[3].ToString().Trim(),
                                        grSalHead.DataKeys[gRowIndx].Values[4].ToString().Trim(),
                                        Beft.InsertedBy, Beft.InsertedDate);
            i++;
            gRowIndx++;
        }
        try
        {
            objDC.MakeTransaction(cmd);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            cmd = null;
        }
    }

    public SqlCommand InsertBenefitPakDet(string StrPackageID, string strSHeadId, string strPayAmt, string StrisInPercent, string StrPercentSalHead, string StrPaymentType, string StrCalrules, string strInsBy, string strInsDate)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Insert_BenefitFacilityDetls");
        cmd.CommandType = CommandType.StoredProcedure;


        SqlParameter p_PackageID = cmd.Parameters.Add("PackageID", SqlDbType.BigInt);
        p_PackageID.Direction = ParameterDirection.Input;
        p_PackageID.Value = StrPackageID;

        SqlParameter p_SHeadId = cmd.Parameters.Add("SHeadId", SqlDbType.BigInt);
        p_SHeadId.Direction = ParameterDirection.Input;
        p_SHeadId.Value = strSHeadId;

        SqlParameter p_PayAm = cmd.Parameters.Add("PayAmt", SqlDbType.BigInt);
        p_PayAm.Direction = ParameterDirection.Input;
        p_PayAm.Value = strPayAmt;

        SqlParameter p_isInPercent = cmd.Parameters.Add("isInPercent", SqlDbType.Char);
        p_isInPercent.Direction = ParameterDirection.Input;
        p_isInPercent.Value = StrisInPercent;


        SqlParameter p_PercentSalHead = cmd.Parameters.Add("PercentSalHead", DBNull.Value);
        p_PercentSalHead.Direction = ParameterDirection.Input;
        p_PercentSalHead.IsNullable = true;
        if (string.IsNullOrEmpty(StrPercentSalHead)==false)
            p_PercentSalHead.Value = StrPercentSalHead;

        SqlParameter p_PaymentType = cmd.Parameters.Add("PaymentType", SqlDbType.BigInt);
        p_PaymentType.Direction = ParameterDirection.Input;
        p_PaymentType.Value = StrPaymentType;


        SqlParameter p_Calrules = cmd.Parameters.Add("Calrules", SqlDbType.BigInt);
        p_Calrules.Direction = ParameterDirection.Input;
        p_Calrules.Value = StrCalrules;


        SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_UpdatedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_UpdatedDate.Direction = ParameterDirection.Input;
        p_UpdatedDate.Value = strInsDate;

        return cmd;
    }

    public void InsertCurrency(Payroll_Currency PayCurr, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Payroll_Insert_Currency");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_CURNCID = command.Parameters.Add("CURNCID", SqlDbType.BigInt);
        p_CURNCID.Direction = ParameterDirection.Input;
        p_CURNCID.Value = PayCurr.CURNCID;

        SqlParameter p_CURNCNAME = command.Parameters.Add("CURNCNAME", SqlDbType.VarChar);
        p_CURNCNAME.Direction = ParameterDirection.Input;
        p_CURNCNAME.Value = PayCurr.CURNCNAME;

        SqlParameter p_CURNCSYMBOL = command.Parameters.Add("CURNCSYMBOL", SqlDbType.VarChar);
        p_CURNCSYMBOL.Direction = ParameterDirection.Input;
        p_CURNCSYMBOL.Value = PayCurr.CURNCSYMBOL;

        SqlParameter p_LOWESTUNITNAME = command.Parameters.Add("LOWESTUNITNAME", SqlDbType.VarChar);
        p_LOWESTUNITNAME.Direction = ParameterDirection.Input;
        p_LOWESTUNITNAME.Value = PayCurr.LOWESTUNITNAME;

        SqlParameter p_ISDEFAULT = command.Parameters.Add("ISDEFAULT", SqlDbType.Char);
        p_ISDEFAULT.Direction = ParameterDirection.Input;
        p_ISDEFAULT.Value = PayCurr.ISDEFAULT;

        SqlParameter p_CONVRSAMT = command.Parameters.Add("CONVRSAMT", SqlDbType.Decimal);
        p_CONVRSAMT.Direction = ParameterDirection.Input;
        p_CONVRSAMT.Value = PayCurr.CONVRSAMT;

        SqlParameter p_ISACTIVE = command.Parameters.Add("ISACTIVE", SqlDbType.Char);
        p_ISACTIVE.Direction = ParameterDirection.Input;
        p_ISACTIVE.Value = PayCurr.ISACTIVE;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = PayCurr.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = PayCurr.InsertedDate;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

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

    public void InsertFiscalYear(Payroll_FiscalYear PayFisYr, string IsUpdate, string IsDelete, string strIsFYTax,
        string strIsFYPF, string strIsFYMed)
    {
        SqlCommand command = new SqlCommand("proc_Payroll_Insert_FiscalYearList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = PayFisYr.FiscalYrId;

        //SqlParameter p_FiscalYrCode = command.Parameters.Add("FiscalYrCode", SqlDbType.VarChar);
        //p_FiscalYrCode.Direction = ParameterDirection.Input;
        //p_FiscalYrCode.Value = PayFisYr.FiscalYrCode;

        SqlParameter p_FiscalYrTitle = command.Parameters.Add("FiscalYrTitle", SqlDbType.VarChar);
        p_FiscalYrTitle.Direction = ParameterDirection.Input;
        p_FiscalYrTitle.Value = PayFisYr.FiscalYrTitle;
        
        SqlParameter p_FiscalDesc = command.Parameters.Add("FiscalDesc", SqlDbType.VarChar);
        p_FiscalDesc.Direction = ParameterDirection.Input;
        p_FiscalDesc.Value = PayFisYr.FiscalDesc;

        SqlParameter p_StartDate = command.Parameters.Add("StartDate", DBNull.Value);
        p_StartDate.Direction = ParameterDirection.Input;
        p_StartDate.IsNullable = true;
        if (PayFisYr.StartDate != "")
            p_StartDate.Value = Common.ReturnDate(PayFisYr.StartDate);

        SqlParameter p_EndDate = command.Parameters.Add("EndDate", DBNull.Value);
        p_EndDate.Direction = ParameterDirection.Input;
        p_EndDate.IsNullable = true;
        if (PayFisYr.EndDate != "")
            p_EndDate.Value = Common.ReturnDate(PayFisYr.EndDate);

        SqlParameter p_IsClosed = command.Parameters.Add("IsClosed", SqlDbType.Char);
        p_IsClosed.Direction = ParameterDirection.Input;
        p_IsClosed.Value = PayFisYr.IsClosed;

        SqlParameter p_IsCurrFiscalYr = command.Parameters.Add("IsCurrFiscalYr", SqlDbType.Char);
        p_IsCurrFiscalYr.Direction = ParameterDirection.Input;
        p_IsCurrFiscalYr.Value = PayFisYr.IsCurrFiscalYr;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = PayFisYr.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = PayFisYr.InsertedDate;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_FYTitleTax = command.Parameters.Add("IsFYTax", SqlDbType.Char);
        p_FYTitleTax.Direction = ParameterDirection.Input;
        p_FYTitleTax.Value = strIsFYTax;

        SqlParameter p_FYTitlePF = command.Parameters.Add("IsFYPF", SqlDbType.Char);
        p_FYTitlePF.Direction = ParameterDirection.Input;
        p_FYTitlePF.Value = strIsFYPF;

        SqlParameter p_FYTitleMed = command.Parameters.Add("IsFYMed", SqlDbType.Char);
        p_FYTitleMed.Direction = ParameterDirection.Input;
        p_FYTitleMed.Value = strIsFYMed;

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

    public void InsertLoanType(Payroll_LoanType PayLoanType, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Payroll_Insert_LoanType");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LOANTYPEID = command.Parameters.Add("LOANTYPEID", SqlDbType.BigInt);
        p_LOANTYPEID.Direction = ParameterDirection.Input;
        p_LOANTYPEID.Value = PayLoanType.LOANTYPEID;

        SqlParameter p_LOANTYPENAME = command.Parameters.Add("LOANTYPENAME", SqlDbType.VarChar);
        p_LOANTYPENAME.Direction = ParameterDirection.Input;
        p_LOANTYPENAME.Value = PayLoanType.LOANTYPENAME;

        SqlParameter p_LOANDESCRIPTION = command.Parameters.Add("LOANDESCRIPTION", SqlDbType.VarChar);
        p_LOANDESCRIPTION.Direction = ParameterDirection.Input;
        p_LOANDESCRIPTION.Value = PayLoanType.LOANDESCRIPTION;

        SqlParameter p_SHEADID = command.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = PayLoanType.SHEADID;

        SqlParameter p_ISACTIVE = command.Parameters.Add("ISACTIVE", SqlDbType.Char);
        p_ISACTIVE.Direction = ParameterDirection.Input;
        p_ISACTIVE.Value = PayLoanType.ISACTIVE;

        SqlParameter p_ISPFLOAN = command.Parameters.Add("ISPFLOAN", SqlDbType.Char);
        p_ISPFLOAN.Direction = ParameterDirection.Input;
        p_ISPFLOAN.Value = PayLoanType.ISPFLOAN;

        SqlParameter p_MINSERVICELIFE = command.Parameters.Add("MINSERVICELIFE", SqlDbType.BigInt);
        p_MINSERVICELIFE.Direction = ParameterDirection.Input;
        p_MINSERVICELIFE.Value = PayLoanType.MINSERVICELIFE;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = PayLoanType.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = PayLoanType.InsertedDate;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

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
   #endregion

    #region Select Queries From Payroll Master Tables By store procedure
    public int GetHeadNature(string strHeadID)
    {
        string strRetValue="";
        string strSQL = "SELECT HEADNATURE FROM SALARYHEAD WHERE SHEADID=@SHEADID";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_SHEADID = cmd.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = strHeadID;

        strRetValue = objDC.GetScalarVal(cmd);
        return Convert.ToInt32(strRetValue);
    }

    public DataTable SelectSalaryHead(Int32 SHEADID, string strISOTHERPAYMENT)
    {
        SqlCommand command = new SqlCommand("proc_Payroll_Select_SalaryHead");

        SqlParameter p_SHEADID = command.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = SHEADID;

        SqlParameter p_ISOTHERPAYMENT = command.Parameters.Add("ISOTHERPAYMENT", SqlDbType.Char);
        p_ISOTHERPAYMENT.Direction = ParameterDirection.Input;
        p_ISOTHERPAYMENT.Value = strISOTHERPAYMENT;


        objDC.CreateDSFromProc(command, "SalaryHead");
        return objDC.ds.Tables["SalaryHead"];
    }

    public DataTable SelectSalaryHeadCategoryWise(string strCategory)
    {
        SqlCommand command =new SqlCommand ();
        string strSQL = "";
        if (strCategory != "")
            strSQL = "SELECT * FROM SALARYHEAD WHERE ITEMCATEGORY=@ITEMCATEGORY ORDER BY SHEADID";
        else
            strSQL = "SELECT * FROM SALARYHEAD ORDER BY SHEADID";
        command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;        

        SqlParameter p_ITEMCATEGORY = command.Parameters.Add("ITEMCATEGORY", SqlDbType.Char);
        p_ITEMCATEGORY.Direction = ParameterDirection.Input;
        p_ITEMCATEGORY.Value = strCategory;

        objDC.CreateDT(command, "SalaryHeadCategoryWise");
        return objDC.ds.Tables["SalaryHeadCategoryWise"];
    }


    public DataTable SelectSalaryHeadwithoutGross(Int32 SHEADID)
    {
        SqlCommand command = new SqlCommand("proc_Payroll_Select_SalaryHeadWithoutGross");

        SqlParameter p_SHEADID = command.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = SHEADID;

        objDC.CreateDSFromProc(command, "SalaryHeadWithoutGross");
        return objDC.ds.Tables["SalaryHeadWithoutGross"];
    }
    public DataTable SelectSalaryHeadDeduction(Int32 SHEADID, string strISOTHERPAYMENT)
    {
        SqlCommand command = new SqlCommand("proc_Payroll_Select_SalaryHeadDeduction");

        SqlParameter p_SHEADID = command.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = SHEADID;

        SqlParameter p_ISOTHERPAYMENT = command.Parameters.Add("ISOTHERPAYMENT", SqlDbType.Char);
        p_ISOTHERPAYMENT.Direction = ParameterDirection.Input;
        p_ISOTHERPAYMENT.Value = strISOTHERPAYMENT;


        objDC.CreateDSFromProc(command, "SalaryHeadDeduction");
        return objDC.ds.Tables["SalaryHeadDeduction"];
    }

    public DataTable SelectSalaryPakDetls(Int32 SalPakId)
    {
        SqlCommand command = new SqlCommand("proc_Payroll_Select_SalaryPakDetls");

        SqlParameter p_SalPakId = command.Parameters.Add("SalPakId", SqlDbType.BigInt);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = SalPakId;

        objDC.CreateDSFromProc(command, "SalaryPakDetls");
        return objDC.ds.Tables["SalaryPakDetls"];
    }

    public DataTable SelectSalaryPakHisDetls(Int32 SalPakId, string strEmpId, string strLogId)
    {
        if (objDC.ds.Tables["SalaryPakHisDetls"] != null)
        {
            objDC.ds.Tables["SalaryPakHisDetls"].Rows.Clear();
            objDC.ds.Tables["SalaryPakHisDetls"].Dispose();
        }
        string strSql = "";
        if (strLogId == "")
            strSql = " SELECT SPH.*,SH.HeadName FROM SalaryPakHisDetls SPH,SalaryHead SH WHERE SPH.SHeadId=SH.SHeadId"
                + " AND SPH.SalPakId=" + SalPakId + " AND SPH.EmpId='" + strEmpId + "'";
        else
            strSql = " SELECT SPH.*,SH.HeadName FROM SalaryPakHisDetls SPH,SalaryHead SH WHERE SPH.SHeadId=SH.SHeadId"
            + " AND SPH.LogId=" + strLogId;
        SqlCommand cmd = new SqlCommand(strSql);

        SqlParameter p_SalPakId = cmd.Parameters.Add("SalPakId", SqlDbType.BigInt);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = SalPakId;

        SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_LogId = cmd.Parameters.Add("LogId", SqlDbType.BigInt);
        p_LogId.Direction = ParameterDirection.Input;
        p_LogId.Value = strLogId;

        objDC.CreateDT(cmd, "SalaryPakHisDetls");
        return objDC.ds.Tables["SalaryPakHisDetls"];
    }

    public DataTable SelectSalaryPackage(Int32 SalPakId)
    {
        SqlCommand command = new SqlCommand("proc_Payroll_Select_SalaryPackage");

        SqlParameter p_SalPakId = command.Parameters.Add("SalPakId", SqlDbType.BigInt);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = SalPakId;

        objDC.CreateDSFromProc(command, "SalaryPackage");
        return objDC.ds.Tables["SalaryPackage"];
    }

    public DataTable SelectBenefitPackage(Int32 BenftPakId)
    {
        SqlCommand command = new SqlCommand("proc_Payroll_Select_BenefitPackage");

        SqlParameter p_PackageID = command.Parameters.Add("PackageID", SqlDbType.BigInt);
        p_PackageID.Direction = ParameterDirection.Input;
        p_PackageID.Value = BenftPakId;

        objDC.CreateDSFromProc(command, "BenefitPackage");
        return objDC.ds.Tables["BenefitPackage"];
    }

    public DataTable SelectBenefitPakDetls(Int32 BenftPakId)
    {
        SqlCommand command = new SqlCommand("proc_Payroll_Select_BenefitPakDetls");

        SqlParameter p_PackageID = command.Parameters.Add("PackageID", SqlDbType.BigInt);
        p_PackageID.Direction = ParameterDirection.Input;
        p_PackageID.Value = BenftPakId;

        objDC.CreateDSFromProc(command, "BenefitPakDetls");
        return objDC.ds.Tables["BenefitPakDetls"];
    }

    public DataTable SelectGrossSalHead(Int32 SHEADID)
    {
        SqlCommand command = new SqlCommand("proc_Payroll_Select_GrossSalHead");

        SqlParameter p_SHEADID = command.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = SHEADID;

        objDC.CreateDSFromProc(command, "GrossSalHead");
        return objDC.ds.Tables["GrossSalHead"];
    }

    public DataTable SelectGrossSalHeadWithNature(Int32 SHEADID)
    {
        SqlCommand command = new SqlCommand("proc_Payroll_Select_GROSSSALHEADWITHNATURE");

        SqlParameter p_SHEADID = command.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = SHEADID;

        objDC.CreateDSFromProc(command, "GrossSalHeadWithNature");
        return objDC.ds.Tables["GrossSalHeadWithNature"];
    }

    public DataTable SelectGrossSalHeadWithName(Int32 SHEADID)
    {
        SqlCommand command = new SqlCommand("proc_Payroll_Select_GROSSSALHEADWITHNAME");

        SqlParameter p_SHEADID = command.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = SHEADID;

        objDC.CreateDSFromProc(command, "GrossSalHeadWithName");
        return objDC.ds.Tables["GrossSalHeadWithName"];
    }   

    public DataTable SelectTotalSalHeadWithSeq(Int32 SHEADID)
    {
        SqlCommand command = new SqlCommand("proc_Payroll_Select_SALARYHEADWITHSEQ");

        SqlParameter p_SHEADID = command.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = SHEADID;

        objDC.CreateDSFromProc(command, "TotalSalHead");
        return objDC.ds.Tables["TotalSalHead"];
    }    

    public DataTable SelectPaySlipSalHeadSeq(decimal SEQNO,Int32 SHEADID)
    {
        SqlCommand command = new SqlCommand("proc_Payroll_Select_PaySlipSalHeadSeq");

        SqlParameter p_SEQNO = command.Parameters.Add("SEQNO", SqlDbType.Decimal);
        p_SEQNO.Direction = ParameterDirection.Input;
        p_SEQNO.Value =SEQNO;

        SqlParameter p_SHEADID = command.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = SHEADID;

        objDC.CreateDSFromProc(command, "PaySlipSalHeadSeq");
        return objDC.ds.Tables["PaySlipSalHeadSeq"];
    }

    public DataTable SelectPaySlipSeqAndHead(decimal SEQNO, Int32 SHEADID, string Type, string IsUpdate)
    {
        string strSQL = "";

        if (IsUpdate == "N")
        {
            if (Type == "S")
                strSQL = "SELECT PS.* FROM PaySlipSalHeadSeq PS"
                + " WHERE PS.SEQNO=@SEQNO";
            else
                strSQL = "SELECT PS.* FROM PaySlipSalHeadSeq PS"
               + " WHERE SHEADID=@SHEADID";
        }
        else
        {
            strSQL = "SELECT PS.* FROM PaySlipSalHeadSeq PS"
            + " WHERE PS.SEQNO=@SEQNO AND PS.SHEADID<>@SHEADID";
        }
        SqlCommand cmd = new SqlCommand(strSQL);

        SqlParameter p_SEQNO = cmd.Parameters.Add("SEQNO", SqlDbType.Decimal);
        p_SEQNO.Direction = ParameterDirection.Input;
        p_SEQNO.Value = SEQNO;

        SqlParameter p_SHEADID = cmd.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = SHEADID;

        objDC.CreateDT(cmd, "PaySlipSalHeadSeq");
        return objDC.ds.Tables["PaySlipSalHeadSeq"];
    }

    public DataTable SelectBonusPackage(Int32 BPID)
    {
        SqlCommand command = new SqlCommand("proc_Payroll_Select_BonusPackage");

        SqlParameter p_BPID = command.Parameters.Add("BPID", SqlDbType.BigInt);
        p_BPID.Direction = ParameterDirection.Input;
        p_BPID.Value = BPID;

        objDC.CreateDSFromProc(command, "BonusPackage");
        return objDC.ds.Tables["BonusPackage"];
    }

    public DataTable SelectCurrencyList(Int32 CURNCID)
    {
        SqlCommand command = new SqlCommand("proc_Payroll_Select_CurrencyList");

        SqlParameter p_CURNCID = command.Parameters.Add("CURNCID", SqlDbType.BigInt);
        p_CURNCID.Direction = ParameterDirection.Input;
        p_CURNCID.Value = CURNCID;

        objDC.CreateDSFromProc(command, "CurrencyList");
        return objDC.ds.Tables["CurrencyList"];
    }

    public bool IsDefaultExists(string strCurrId)
    {
        string strSQL = "";
        strSQL = "SELECT CURNCNAME FROM CurrencyList WHERE ISDEFAULT='Y' AND CURNCID!=" + strCurrId;


        DataTable dtDefaultCurr = new DataTable();
        dtDefaultCurr = objDC.CreateDT(strSQL, "DefaultCurr");

        if (dtDefaultCurr.Rows.Count > 0)
            return true;
        else
            return false;
    }

    public DataTable SelectFiscalYear(Int32 FiscalYrId)
    {
        if (objDC.ds.Tables["FiscalYearList"] != null)
        {
            objDC.ds.Tables["FiscalYearList"].Rows.Clear();
            objDC.ds.Tables["FiscalYearList"].Dispose();
        }


        SqlCommand command = new SqlCommand("proc_Payroll_Select_FiscalYearList");

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = FiscalYrId;

        objDC.CreateDSFromProc(command, "FiscalYearList");
        return objDC.ds.Tables["FiscalYearList"];
    }

    public DataTable SelectFiscalYear(Int32 FiscalYrId, string strType)
    {
        if (objDC.ds.Tables["dtFiscalYearList"] != null)
        {
            objDC.ds.Tables["dtFiscalYearList"].Rows.Clear();
            objDC.ds.Tables["dtFiscalYearList"].Dispose();
        }

        string strSQL = "";
        if (strType == "F")
            strSQL = "SELECT * FROM FiscalYearList WHERE IsCurrFiscalYr='Y' AND IsFYTax='N' AND  IsFYPF='N' AND IsFYMed='N' ";
        else if (strType == "T")
            strSQL = "SELECT * FROM FiscalYearList WHERE IsFYTax='Y' ORDER BY FiscalYrId DESC";
        else if (strType == "P")
            strSQL = "SELECT * FROM FiscalYearList WHERE IsFYPF='Y' ORDER BY FiscalYrId DESC";
        else if (strType == "M")
            strSQL = "SELECT * FROM FiscalYearList WHERE IsFYMed='Y' ORDER BY FiscalYrId DESC";
        else if (strType == "FA")
            strSQL = "SELECT * FROM FiscalYearList WHERE IsFYTax='N' AND  IsFYPF='N' AND IsFYMed='N' ORDER BY IsCurrFiscalYr DESC,FiscalYrId DESC";
        else if (strType == "LPF")
            strSQL = "SELECT top 1 FiscalYrId,FiscalYrTitle FROM FiscalYearList WHERE IsFYPF='Y' ORDER BY FiscalYrId DESC";

        //else if (strType == "PF")
        //    strSQL = "SELECT * FROM FiscalYearList WHERE IsFYMed='Y' and FiscalYrTitle LIKE 'PF%' ORDER BY FiscalYrId DESC";
        SqlCommand cmd = new SqlCommand(strSQL);

        objDC.CreateDT(cmd, "dtFiscalYearList");
        return objDC.ds.Tables["dtFiscalYearList"];
    }

    public DataTable SelectLoanType(Int32 LOANTYPEID)
    {
        SqlCommand command = new SqlCommand("proc_Payroll_Select_LoanType");

        SqlParameter p_LOANTYPEID = command.Parameters.Add("LOANTYPEID", SqlDbType.BigInt);
        p_LOANTYPEID.Direction = ParameterDirection.Input;
        p_LOANTYPEID.Value = LOANTYPEID;

        objDC.CreateDSFromProc(command, "LoanType");
        return objDC.ds.Tables["LoanType"];
    }


    public DataTable GenerateSalaryPackTitle()
    {
        string strSQL = "SELECT RTRIM(EMPID) + '-' + FullName + '-SalPack' as SalPackTitle,EmpID from EmpInfo where EmpStatus='A' AND Isdeleted='N' ORDER BY EmpID";
        return objDC.CreateDT(strSQL, "GenerateSalaryPackTitle");
    }

    public string GetDuplicatePackage(string strEmpID)
    {
        string strSQL = "SELECT EMPID FROM SalaryPakMst WHERE EMPID=@EMPID";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        return objDC.GetScalarVal(cmd);
    }

    public DataTable GetIsBasicOrPF(string strHeadId)
    {
        string strSQL = "SELECT ISBASIC,ISPF FROM SALARYHEAD WHERE SHEADID = @SHEADID";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_SHEADID = cmd.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = strHeadId;

        objDC.CreateDT(cmd, "IsBasicOrPF");
        return objDC.ds.Tables["IsBasicOrPF"];
    }

    public DataTable GetPackageListBySearch(string strText)
    {
        string strSQL = " SELECT a.*, e.FullName,d.desigName,l.ClinicName FROM SalaryPakMst a, EmpInfo e,Designation d,ClinicList l   "
                     + " WHERE a.EmpID=e.EmpId and e.DesigId=d.DesigId and e.ClinicId=l.ClinicId "
                     + " and SPTITLE LIKE('%" + strText + "%')  AND a.SALPAKID<>99999 ORDER BY a.SPTITLE ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        objDC.CreateDT(cmd, "PackageListBySearch");
        return objDC.ds.Tables["PackageListBySearch"];
    }

    public DataTable SelectBankAndBranchList()
    {
        string strSQL = "SELECT DISTINCT B.BankName + '-' + B.BranchName as BANKBRANCH,B.RoutingNo FROM BankList B,EMPINFO E  "
            + " WHERE B.RoutingNo=E.RoutingNo AND B.BankName IS NOT NULL";
        return objDC.CreateDT(strSQL, "BankBranchList");
    }

    public DataTable SelectDivisionWiseBankAndBranchList(string strDivID)
    {
        string strSQL = "SELECT DISTINCT B.BankName + '-' + B.BranchName as BANKBRANCH,B.RoutingNo FROM BankList B,EMPINFO E  "
            + " WHERE B.RoutingNo=E.BranchCode AND E.DivisionID=@DivisionID AND B.BankName IS NOT NULL";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        SqlParameter p_DivisionID = cmd.Parameters.Add("DivisionID", SqlDbType.BigInt);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = strDivID;
        return objDC.CreateDT(strSQL, "SelectDivisionWiseBankAndBranchList");
    }

    public string GetEmpType(string strEmpID)
    {
        string strType = "";
        SqlCommand cmd = new SqlCommand("SELECT EMPTYPEID FROM EMPINFO WHERE EMPID=@EMPID");
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        strType = objDC.GetScalarVal(cmd);
        return strType;
    }

    public string GetGradeWiseMobileAllow(string strEmpID)
    {
        string MobileAllow = "0";
        SqlCommand cmd = new SqlCommand(" SELECT MobileAllow FROM GradeList GL" +
                                        " JOIN EmpInfo EI ON GL.GradeID = EI.GradeId" +
                                        " WHERE EI.EmpId = @EMPID");
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        MobileAllow = objDC.GetScalarVal(cmd);
        return MobileAllow;
    }

    public DataTable GetSalaryItem()
    {
        string strSQL = "SELECT * FROM SALARYHEAD WHERE ITEMCATEGORY='S' ORDER BY SHEADID";

        return objDC.CreateDT(strSQL, "GetSalaryItem");
    }

    //public string GetEmpBasic(string strEmpID)
    //{
    //    string strBasic = "";
    //    SqlCommand cmd = new SqlCommand("SELECT BASICSALARY FROM EMPINFO WHERE EMPID = @EMPID");
    //    cmd.CommandType = CommandType.Text;

    //    SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
    //    p_EMPID.Direction = ParameterDirection.Input;
    //    p_EMPID.Value = strEmpID;

    //    strBasic = objDC.GetScalarVal(cmd);
    //    return strBasic;
    //}

    public string GetTotalGrossSalary(string strEmpID)
    {
        string strSQL = "SELECT TOTALGROSSSAL FROM SalaryPakMst WHERE EMPID=@EMPID";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        return objDC.GetScalarVal(cmd);
    }

    public DataTable GetSalaryBenefitsHead()
    {
        string strSQL = "SELECT A.*,B.SHORTNAME,B.HeadName FROM SALARYHEADWITHSEQ A,SALARYHEAD B   "
                    + " WHERE A.SHEADID=B.SHEADID  AND A.DisplayType<>'D' "
                    + " ORDER BY A.SEQNO ";
        return objDC.CreateDT(strSQL, "GetSalaryBenefitsHead");
    }
    #endregion

    #region Bonus Policy
    public void InsertBonusPolicyData(string strBPID, string strEmpType,string strPercent, string IsUpdate, string strInsBy, string strInsDate,string strIsProrata)
    {
        SqlCommand command = new SqlCommand("Proc_Payroll_Insert_BonusPolicy");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_BPID = command.Parameters.Add("BPID", SqlDbType.BigInt);
        p_BPID.Direction = ParameterDirection.Input;
        p_BPID.Value = strBPID;

        SqlParameter p_EMPTYPEID = command.Parameters.Add("EMPTYPEID", SqlDbType.BigInt);
        p_EMPTYPEID.Direction = ParameterDirection.Input;
        p_EMPTYPEID.Value = strEmpType;

        SqlParameter p_PRCENT = command.Parameters.Add("PRCENT", SqlDbType.Decimal);
        p_PRCENT.Direction = ParameterDirection.Input;
        p_PRCENT.Value = strPercent;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_INSERTEDBY = command.Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = strInsBy;

        SqlParameter p_INSERTEDDATE = command.Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDDATE.Direction = ParameterDirection.Input;
        p_INSERTEDDATE.Value = strInsDate;

        SqlParameter p_IsProrata = command.Parameters.Add("IsProrata", SqlDbType.Char);
        p_IsProrata.Direction = ParameterDirection.Input;
        p_IsProrata.Value = strIsProrata;


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

    public void DeleteBonusPolicyData(string strBPID)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Delete_BonusPolicy");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_BPID = cmd.Parameters.Add("BPID", SqlDbType.BigInt);
        p_BPID.Direction = ParameterDirection.Input;
        p_BPID.Value = strBPID;

        objDC.ExecuteQuery(cmd);
    }

    public DataTable SelectBonusPolicyData()
    {
        string strSQL = "SELECT B.*,E.TYPENAME FROM BonusPolicy B,EMPTYPELIST E WHERE B.EMPTYPEID=E.EMPTYPEID ";
        return objDC.CreateDT(strSQL, "BonusPolicy");
    }
    
    #endregion

    // Insert or Update or Delete Data of OTAdjustment table
    public void InsertOTAdjustment(clsOTAdjustment obj, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_OTAdjustment");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_TransId = command.Parameters.Add("TransId", SqlDbType.BigInt);
        p_TransId.Direction = ParameterDirection.Input;
        p_TransId.Value = obj.TransId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = obj.EmpId;

        SqlParameter p_VMonth = command.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = obj.Month;

        SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.BigInt);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = obj.Year;

        SqlParameter p_OTHour = command.Parameters.Add("OTHour", SqlDbType.Decimal);
        p_OTHour.Direction = ParameterDirection.Input;
        p_OTHour.Value = obj.OTHour;

        SqlParameter p_OTAppHour = command.Parameters.Add("OTAppHour", SqlDbType.Decimal);
        p_OTAppHour.Direction = ParameterDirection.Input;
        p_OTAppHour.Value = obj.OTAppHour;

        SqlParameter p_BasicSalary = command.Parameters.Add("BasicSalary", SqlDbType.Decimal);
        p_BasicSalary.Direction = ParameterDirection.Input;
        p_BasicSalary.Value = obj.BasicSal;

        SqlParameter p_OTAmount = command.Parameters.Add("OTAmount", SqlDbType.Decimal);
        p_OTAmount.Direction = ParameterDirection.Input;
        p_OTAmount.Value = obj.OTAmount;

        SqlParameter p_EntryDate = command.Parameters.Add("EntryDate", SqlDbType.DateTime);
        p_EntryDate.Direction = ParameterDirection.Input;
        p_EntryDate.Value = obj.EntryDate;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = obj.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = obj.InsertedDate;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

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


    public DataTable SelectAppraisalIncSalaryUpdate(string FiscalyrId,string FromRating,string ToRating)
    {
        SqlCommand command = new SqlCommand("proc_Select_AppraisalIncUpdate");

        SqlParameter p_DivID = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_DivID.Direction = ParameterDirection.Input;
        p_DivID.Value = FiscalyrId;


        SqlParameter p_RatingFrom = command.Parameters.Add("RatingFrom", SqlDbType.BigInt);
        p_RatingFrom.Direction = ParameterDirection.Input;
        p_RatingFrom.Value = FromRating;

        SqlParameter p_RatingTo = command.Parameters.Add("RatingTo", SqlDbType.BigInt);
        p_RatingTo.Direction = ParameterDirection.Input;
        p_RatingTo.Value = ToRating;

        objDC.CreateDSFromProc(command, "tblAppraisalIncUpdate");
        return objDC.ds.Tables["tblAppraisalIncUpdate"];
    }
    //Mamun
    //Insert or update employee confirmation information 
    public void InsertAppraisalSalaryIncrement( GridView grAppraisalIncUpdate,string strFisYrId, string strIncPercent, string strInsBy, string strInsDate, string IsUpdate)
    {
        int i = 0;
        int j = 0;

        int iAppId = 0;
        iAppId=Convert.ToInt32(Common.getMaxId("AppraisalSalaryIncrement", "AppId"));

        SqlCommand[] command = new SqlCommand[grAppraisalIncUpdate.Rows.Count  + grAppraisalIncUpdate.Rows.Count * 3];

        foreach (GridViewRow gRow in grAppraisalIncUpdate.Rows)
        {
            command[i] = new SqlCommand("proc_Insert_AppraisalSalaryIncrement");
            command[i].CommandType = CommandType.StoredProcedure;



            SqlParameter p_AppId = command[i].Parameters.Add("AppId", SqlDbType.Char);
            p_AppId.Direction = ParameterDirection.Input;
            p_AppId.Value = iAppId;

            SqlParameter p_EmpId = command[i].Parameters.Add("EmpId", SqlDbType.VarChar);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = gRow.Cells[0].Text.Trim();

            SqlParameter p_FiscalYrId = command[i].Parameters.Add("FiscalYrId", SqlDbType.BigInt);
            p_FiscalYrId.Direction = ParameterDirection.Input;
            p_FiscalYrId.Value = strFisYrId;

            SqlParameter p_Rating = command[i].Parameters.Add("Rating", SqlDbType.Decimal);
            p_Rating.Direction = ParameterDirection.Input;
            p_Rating.Value = Convert.ToDecimal(gRow.Cells[3].Text);

            SqlParameter p_IncPercent = command[i].Parameters.Add("IncPercent", SqlDbType.BigInt);
            p_IncPercent.Direction = ParameterDirection.Input;
            p_IncPercent.Value = strIncPercent;

            SqlParameter p_BasicSalary = command[i].Parameters.Add("BasicSalary", SqlDbType.Decimal);
            p_BasicSalary.Direction = ParameterDirection.Input;
            p_BasicSalary.Value = gRow.Cells[4].Text.Trim();

            SqlParameter p_Allowance = command[i].Parameters.Add("Allowance", SqlDbType.Decimal);
            p_Allowance.Direction = ParameterDirection.Input;
            p_Allowance.Value = gRow.Cells[5].Text.Trim();

            SqlParameter p_PF = command[i].Parameters.Add("PF", SqlDbType.Decimal);
            p_PF.Direction = ParameterDirection.Input;
            p_PF.Value = gRow.Cells[6].Text.Trim();

            SqlParameter p_InsertedBy = command[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = strInsBy;

            SqlParameter p_InsertedDate = command[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate.Direction = ParameterDirection.Input;
            p_InsertedDate.Value = strInsDate;

            SqlParameter p_IsUpdate = command[i].Parameters.Add("IsUpdate", SqlDbType.Char);
            p_IsUpdate.Direction = ParameterDirection.Input;
            p_IsUpdate.Value = IsUpdate;

            i++;
            command[i] = objFileUpMgr.UpdateSalaryHeadWsAmt(grAppraisalIncUpdate.DataKeys[j].Values[0].ToString(), "1", gRow.Cells[4].Text, strInsBy, strInsDate, "Appraisal Increment");

            i++;
            command[i] = objFileUpMgr.UpdateSalaryHeadWsAmt(grAppraisalIncUpdate.DataKeys[j].Values[0].ToString(), "2", gRow.Cells[5].Text, strInsBy, strInsDate, "Appraisal Increment");

            i++;
            command[i] = objFileUpMgr.UpdateSalaryHeadWsAmt(grAppraisalIncUpdate.DataKeys[j].Values[0].ToString(), "13", gRow.Cells[6].Text, strInsBy, strInsDate, "Appraisal Increment");

            j++;
            i++;
            iAppId++;
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

    


    public DataTable SelectOTAdjustment(string strEmpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_OTAdjustment");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        objDC.CreateDSFromProc(command, "SelectOTAdjustment");
        return objDC.ds.Tables["SelectOTAdjustment"];
    }


    #region USD Rate

    public DataTable SelectLastUSDRate(string StrOptId)
    {
        SqlCommand command = new SqlCommand("proc_Payroll_Get_LastUSDRate");

        objDC.CreateDSFromProc(command, "LastUSDRate");
        return objDC.ds.Tables["LastUSDRate"];
    }

    public DataTable SelectUSDRate(string StrOptId)
    {
        SqlCommand command = new SqlCommand("proc_Payroll_Select_USDRate");

        objDC.CreateDSFromProc(command, "USDRate");
        return objDC.ds.Tables["USDRate"];
    }

    public void InsertUDSRate(string strUSDId,string strUSDRate, string strRateDate, string strInsBy, string strInsDate,string IsUpdate,string IsDelete)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Insert_USDRate");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_USDId = cmd.Parameters.Add("USDId", SqlDbType.BigInt);
        p_USDId.Direction = ParameterDirection.Input;
        p_USDId.Value = Convert.ToInt32(strUSDId);

        SqlParameter p_USDRate = cmd.Parameters.Add("USDRate", SqlDbType.Decimal);
        p_USDRate.Direction = ParameterDirection.Input;
        p_USDRate.Value = Convert.ToDecimal(strUSDRate);

        SqlParameter p_RateDate = cmd.Parameters.Add("USDDate", SqlDbType.DateTime);
        p_RateDate.Direction = ParameterDirection.Input;
        p_RateDate.Value = strRateDate;

        SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        SqlParameter p_IsUpdate = cmd.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        objDC.ExecuteQuery(cmd);
    }

    public string SelectUSDRate()
    {
        string strSQL = "SELECT TOP(1) USDRate from USDRate ORDER BY USDDate DESC";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        return objDC.GetScalarVal(cmd);
    }

    public string SelectUSDRate(string sMonth, string sYear)
    {
        string strSQL = "SELECT top(1)  USDRate from USDRate where MONTH(USDDate)=" + sMonth + " and YEAR(USDDate)=" + sYear + " ORDER BY USDDate DESC";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        return objDC.GetScalarVal(cmd);
    }

    #endregion

    public DataTable GetEmpBasic(string strEmpID)
    {
        string strSQL = " SELECT BasicSalary,GrossSalary,EMPTYPEID FROM EMPINFO WHERE EMPID=@EMPID ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        return objDC.CreateDT(cmd, "GetEmpBasic");
    }

    public DataTable GetEmpGross(string strEmpID)
    {
        string strSQL = " SELECT GrossSalary,EMPTYPEID FROM EMPINFO WHERE EMPID=@EMPID ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        return objDC.CreateDT(cmd, "GetGrossSalary");
    }

    public DataTable GetSalaryHeadForPackageSetup()
    {
        string strSQL = " SELECT SH.SHEADID,SH.HEADNAME,'Y' AS ISPERCENT,0 AS VALUE,SQ.DISPLAYTYPE, "
                      + " SH.ISBASIC,SH.ISPF,SH.ISHOUSERENT,SH.ISMEDICAL,SH.ISCONVEYANCE "
                      + " FROM SALARYHEAD SH,SALARYHEADWITHSEQ SQ "
                      + " WHERE SH.SHEADID=SQ.SHEADID "
                      + " ORDER BY SQ.SEQNO ";
        return objDC.CreateDT(strSQL, "GetSalaryHeadForPackageSetup");
    }



    public DataTable GetLocationData()
    {
        SqlCommand command = new SqlCommand("proc_Select_SalLocation");

        objDC.CreateDSFromProc(command, "SalLoc");
        return objDC.ds.Tables["SalLoc"];
    }

    // SelectSalDivision

    public DataTable SelectSalDivision(int lick)
    {
        SqlCommand command = new SqlCommand("proc_Select_SalDivition");

        SqlParameter p_Lock = command.Parameters.Add("Lock", SqlDbType.BigInt);
        p_Lock.Direction = ParameterDirection.Input;
        p_Lock.Value = lick;

        objDC.CreateDSFromProc(command, "SalSubLoc");
        return objDC.ds.Tables["SalSubLoc"];
    }

    public DataTable SelectClinic()
    {
        string strSQL = "SELECT * FROM ClinicList WHERE ISACTIVE='Y' and IsDeleted='N' ORDER BY ClinicName";
        return objDC.CreateDT(strSQL, "ClinicList");
    }
    //SelectSalSource
    public DataTable SelectSalSource(int lick)
    {
        SqlCommand command = new SqlCommand("proc_Select_SalSource");

        SqlParameter p_Lock = command.Parameters.Add("SalarySourceId", SqlDbType.BigInt);
        p_Lock.Direction = ParameterDirection.Input;
        p_Lock.Value = lick;

        objDC.CreateDSFromProc(command, "SalSource");
        return objDC.ds.Tables["SalSource"];
    }

    //SelectEmployeeList
    public DataTable SelectEmployeeList(string salDiv, string salPostDiv, string type)
    {
        SqlCommand command = new SqlCommand("proc_Select_SalRptEmpList");

        SqlParameter p_SalSubLocId = command.Parameters.Add("SalSubLocId", SqlDbType.VarChar);
        p_SalSubLocId.Direction = ParameterDirection.Input;
        p_SalSubLocId.Value = salDiv;

        SqlParameter p_PostingDistId = command.Parameters.Add("PostingDistId", SqlDbType.VarChar);
        p_PostingDistId.Direction = ParameterDirection.Input;
        p_PostingDistId.Value = salPostDiv;

        SqlParameter p_EType = command.Parameters.Add("EType", SqlDbType.Char);
        p_EType.Direction = ParameterDirection.Input;
        p_EType.Value = type;


        objDC.CreateDSFromProc(command, "SalSubLoc");
        return objDC.ds.Tables["SalSubLoc"];
    }

    public DataTable SelectEmployeeListPF( string type)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpListByType");


        SqlParameter p_EType = command.Parameters.Add("EType", SqlDbType.Char);
        p_EType.Direction = ParameterDirection.Input;
        p_EType.Value = type;


        objDC.CreateDSFromProc(command, "tblEmpList");
        return objDC.ds.Tables["tblEmpList"];
    }




    public DataTable SelectPostDist()
    {
        SqlCommand command = new SqlCommand("proc_Select_PostDivition");

        objDC.CreateDSFromProc(command, "PostDist");
        return objDC.ds.Tables["PostDist"];
    }

    public DataTable GetEmpListID(string etype)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpListWithID");

        SqlParameter E_Type = command.Parameters.Add("E_Type", SqlDbType.Char);
        E_Type.Direction = ParameterDirection.Input;
        E_Type.Value = etype;

        objDC.CreateDSFromProc(command, "EmpListWithID");
        return objDC.ds.Tables["EmpListWithID"];
    }

    public DataTable GetProvidentFundBF(string FisYrID)
    {
        SqlCommand command = new SqlCommand("proc_ProvidentFundBF");

        SqlParameter P_FisYrId = command.Parameters.Add("FisYrId", SqlDbType.BigInt);
        P_FisYrId.Direction = ParameterDirection.Input;
        P_FisYrId.Value =Convert.ToInt32(FisYrID);

        objDC.CreateDSFromProc(command, "ProvidentFundBF");
        return objDC.ds.Tables["ProvidentFundBF"];
    }

    //InsertPFBFData
    public void InsertPFBFData( GridView grPFBF,string dtMaxID, string InsertedBy, string InDate)
    {
        SqlCommand[] cmd;
        cmd = new SqlCommand[grPFBF.Rows.Count]; 
        long i = 0;
        int MaxID = Convert.ToInt32(dtMaxID);
        foreach (GridViewRow gRow in grPFBF.Rows)
        {
            //EMPID,FullName,FisYear,CF,TotalCon,TotalInter,BroadForword
            MaxID++;
            cmd[i] = InsertPFBFPakDet(  gRow.Cells[0].Text.Trim(),
                                        gRow.Cells[2].Text.Trim(),
                                        gRow.Cells[3].Text.Trim(),
                                        gRow.Cells[4].Text.Trim(),
                                        gRow.Cells[5].Text.Trim(),
                                        gRow.Cells[6].Text.Trim(),
                                        InsertedBy, InDate, MaxID.ToString());
            i++;
           // gRowIndx++;
        }
        try
        {
            objDC.MakeTransaction(cmd);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            cmd = null;
        }
    }

    public SqlCommand InsertPFBFPakDet(string EMPID, string FisYear, string CF, string TotalCon, string TotalInter, string BroadForword, 
        string strInsBy, string strInsDate, string MaxID)
    {
        //EMPID,FullName,FisYear,CF,TotalCon,TotalInter,BroadForword
        SqlCommand cmd = new SqlCommand("proc_Payroll_Insert_PFBFProvidentFundBF");
        cmd.CommandType = CommandType.StoredProcedure;


        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = EMPID;

        SqlParameter p_FisYear = cmd.Parameters.Add("FisYear", SqlDbType.BigInt);
        p_FisYear.Direction = ParameterDirection.Input;
        p_FisYear.Value = Convert.ToDecimal(FisYear);

        SqlParameter p_CF = cmd.Parameters.Add("CF", SqlDbType.BigInt);
        p_CF.Direction = ParameterDirection.Input;
        p_CF.Value = Convert.ToDecimal(CF);

        SqlParameter p_TotalCon = cmd.Parameters.Add("TotalCon", SqlDbType.BigInt);
        p_TotalCon.Direction = ParameterDirection.Input;
        p_TotalCon.Value = Convert.ToDecimal(TotalCon);

        SqlParameter p_TotalInter = cmd.Parameters.Add("TotalInter", SqlDbType.BigInt);
        p_TotalInter.Direction = ParameterDirection.Input;
        p_TotalInter.Value = Convert.ToDecimal(TotalInter);


        SqlParameter p_BroadForword = cmd.Parameters.Add("BroadForword", SqlDbType.BigInt);
        p_BroadForword.Direction = ParameterDirection.Input;
        p_BroadForword.Value = Convert.ToDecimal(BroadForword);


        SqlParameter p_InsertedBy = cmd.Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_UpdatedDate = cmd.Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_UpdatedDate.Direction = ParameterDirection.Input;
        p_UpdatedDate.Value = strInsDate;

        SqlParameter p_MaxID = cmd.Parameters.Add("PFBFID", SqlDbType.BigInt);
        p_MaxID.Direction = ParameterDirection.Input;
        p_MaxID.Value = Convert.ToInt32(MaxID);



        return cmd;
    }

    public string GetMaxPFBFID()
    {
        string strSQL = "SELECT isnull(MAX(PFBFID),0) FROM ProvidentFundBF";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        return objDC.GetScalarVal(cmd);
    }

    
    #region COLAAdjustment
    public bool CheckForMultipleCLOAEntry(string strEffDate)
    {
        string strSQL = "";
        string strRetValue = "";
        strSQL = "SELECT * FROM COLAAdjustLog WHERE VMonth=MONTH(" + strEffDate + ")AND VYear=YEAR(" + strEffDate + ")";

        SqlCommand command = new SqlCommand(strSQL);
        command.CommandType = CommandType.Text;

        strRetValue = objDC.GetScalarVal(command);
        if (string.IsNullOrEmpty(strRetValue) == true)
            return false;
        else
            return true;
    }


    public void InsertCOLAAdjust(GridView grEList, string strFiscalYrId,  string strPercentage,
        string strEffDate, string strInsBy, string strInsDate, string strLastUpFrom)
    {
        long i = 0;
        int j = 0;
        SqlCommand[] cmd;
        cmd = new SqlCommand[grEList.Rows.Count * 5];

        string strLogId = "";
        string strEmpId = "";
        //string strPayEmpId = "";
        string strIsConfirm = "";

        string strSalPakId = "";
        string strBasicSal = "";
        string strNewBasicSal = "";
        string strAllowance = "";
        string strPF = "";

        strLogId = Common.getMaxId("COLAAdjustLog", "LogId");
        DataTable dTable = new DataTable();
        foreach (GridViewRow gRow in grEList.Rows)
        {
            strEmpId = gRow.Cells[1].Text.Trim();

            strIsConfirm = gRow.Cells[4].Text.Trim();

            strSalPakId = grEList.DataKeys[j].Values[0].ToString();

            strBasicSal = gRow.Cells[3].Text.Trim();
            strNewBasicSal = gRow.Cells[4].Text.Trim();
            strAllowance = gRow.Cells[5].Text.Trim();
            strPF = gRow.Cells[6].Text.Trim();

            cmd[i] = this.InsertCOLAAdjustEntry(strLogId, strFiscalYrId,  strEmpId,
                strBasicSal, strNewBasicSal, strAllowance, strPF, strPercentage, strEffDate, strInsBy, strInsDate);
            i++;
            //Update EmpINfo BasicSal
            cmd[i] = UpdateEmpInfoBaiscSal(strEmpId, strNewBasicSal, strInsBy, strInsDate, strLastUpFrom);
            i++;
            //Update BasicSal in Salary Package Details
            cmd[i] = UpdateSalaryHeadWsAmt(strSalPakId, "1", strNewBasicSal, strInsBy, strInsDate, strLastUpFrom);
            i++;
            //Update House Rent in Salary Package Details
            cmd[i] = UpdateSalaryHeadWsAmt(strSalPakId, "2", strAllowance, strInsBy, strInsDate, strLastUpFrom);
            i++;
            //Update PF in Salary Package Details
            cmd[i] = UpdateSalaryHeadWsAmt(strSalPakId, "8", strPF, strInsBy, strInsDate, strLastUpFrom);
            i++;

            j++;
        }
        try
        {
            objDC.MakeTransaction(cmd);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            cmd = null;
        }
    }

    public SqlCommand InsertCOLAAdjustEntry(string strLogId, string strFiscalYrId, string strEmpId,
        string strBasicSal, string strNewBasicSal, string strAllowance, string strPF, string strPercentage, string strEffDate,
        string strInsBy, string strInsDate)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_Insert_COLAAdjustLog");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter p_LogId = cmd.Parameters.Add("LogId", SqlDbType.BigInt);
            p_LogId.Direction = ParameterDirection.Input;
            p_LogId.Value = strLogId;

            SqlParameter p_FiscalYrId = cmd.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
            p_FiscalYrId.Direction = ParameterDirection.Input;
            p_FiscalYrId.Value = strFiscalYrId;

            SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = strEmpId;

            SqlParameter p_BasicSal = cmd.Parameters.Add("BasicSal", DBNull.Value);
            p_BasicSal.Direction = ParameterDirection.Input;
            p_BasicSal.IsNullable = true;
            if (strBasicSal != "")
                p_BasicSal.Value = strBasicSal;

            SqlParameter p_NewBasicSal = cmd.Parameters.Add("NewBasicSal", DBNull.Value);
            p_NewBasicSal.Direction = ParameterDirection.Input;
            p_NewBasicSal.IsNullable = true;
            if (strNewBasicSal != "")
                p_NewBasicSal.Value = strNewBasicSal;

            SqlParameter p_HouseRent = cmd.Parameters.Add("Allowance", DBNull.Value);
            p_HouseRent.Direction = ParameterDirection.Input;
            p_HouseRent.IsNullable = true;
            if (strAllowance != "")
                p_HouseRent.Value = strAllowance;

            SqlParameter p_PF = cmd.Parameters.Add("PF", DBNull.Value);
            p_PF.Direction = ParameterDirection.Input;
            p_PF.IsNullable = true;
            if (strPF != "")
                p_PF.Value = strPF;

            SqlParameter p_Percentage = cmd.Parameters.Add("Percentage", SqlDbType.Decimal);
            p_Percentage.Direction = ParameterDirection.Input;
            p_Percentage.Value = strPercentage;

            SqlParameter p_EffDate = cmd.Parameters.Add("EffDate", SqlDbType.DateTime);
            p_EffDate.Direction = ParameterDirection.Input;
            p_EffDate.Value = strEffDate;

            SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = strInsBy;

            SqlParameter p_InsertedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate.Direction = ParameterDirection.Input;
            p_InsertedDate.Value = strInsDate;

            return cmd;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    public SqlCommand UpdateEmpInfoBaiscSal(string strEmpId, string strPayAmt, string strInsBy, string strInsDate, string strLastUpdatedFrom)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_Update_EmpBasicSal");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = strEmpId;

            SqlParameter p_PayAmt = cmd.Parameters.Add("BasicSalary", SqlDbType.Decimal);
            p_PayAmt.Direction = ParameterDirection.Input;
            p_PayAmt.Value = strPayAmt;

            SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = strInsBy;

            SqlParameter p_UpdatedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_UpdatedDate.Direction = ParameterDirection.Input;
            p_UpdatedDate.Value = strInsDate;

            SqlParameter p_LastUpdatedFrom = cmd.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
            p_LastUpdatedFrom.Direction = ParameterDirection.Input;
            p_LastUpdatedFrom.Value = strLastUpdatedFrom;

            return cmd;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    public SqlCommand UpdateSalaryHeadWsAmt(string strSalPakId, string strSHeadId, string strPayAmt, string strInsBy, string strInsDate, string strLastUpdatedFrom)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_Payroll_Update_SalaryPackDetls");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter p_SalPakId = cmd.Parameters.Add("SalPakId", SqlDbType.BigInt);
            p_SalPakId.Direction = ParameterDirection.Input;
            p_SalPakId.Value = strSalPakId;

            SqlParameter p_SHeadId = cmd.Parameters.Add("SHeadId", SqlDbType.BigInt);
            p_SHeadId.Direction = ParameterDirection.Input;
            p_SHeadId.Value = strSHeadId;

            SqlParameter p_PayAmt = cmd.Parameters.Add("PayAmt", SqlDbType.Decimal);
            p_PayAmt.Direction = ParameterDirection.Input;
            p_PayAmt.Value = strPayAmt;

            SqlParameter p_TotAmnt = cmd.Parameters.Add("TotAmnt", SqlDbType.Decimal);
            p_TotAmnt.Direction = ParameterDirection.Input;
            p_TotAmnt.Value = strPayAmt;

            SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = strInsBy;

            SqlParameter p_UpdatedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_UpdatedDate.Direction = ParameterDirection.Input;
            p_UpdatedDate.Value = strInsDate;

            SqlParameter p_LastUpdatedFrom = cmd.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
            p_LastUpdatedFrom.Direction = ParameterDirection.Input;
            p_LastUpdatedFrom.Value = strLastUpdatedFrom;

            return cmd;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    #endregion

    public DataTable SelectPayslipDetArrearTaxFiscalYrWs(string strTaxFiscalYr, string strEmpId)
    {
        SqlCommand command = new SqlCommand("proc_Payroll_Select_PayslipDetArrearTaxFiscalYrWs");

        SqlParameter p_TaxFiscalYrId = command.Parameters.Add("TaxFiscalYrId", SqlDbType.BigInt);
        p_TaxFiscalYrId.Direction = ParameterDirection.Input;
        p_TaxFiscalYrId.Value = strTaxFiscalYr;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        objDC.CreateDSFromProc(command, "PayslipDetArrearTaxFiscalYrWs");
        return objDC.ds.Tables["PayslipDetArrearTaxFiscalYrWs"];
    }


    public DataTable GeneratePFBF(string strPFID)
    {
        SqlCommand command = new SqlCommand("proc_Select_GeneratePFBF");

        SqlParameter p_EmpId = command.Parameters.Add("PFFiscalYrID", SqlDbType.BigInt);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = Convert.ToInt32(strPFID);

        objDC.CreateDSFromProc(command, "SelectPFBF");
        return objDC.ds.Tables["SelectPFBF"];
    }

    public string Select2ndMaxLogId(string strEmpId)
    {
        if (objDC.ds.Tables["Select2ndMaxLogId"] != null)
        {
            objDC.ds.Tables["Select2ndMaxLogId"].Rows.Clear();
            objDC.ds.Tables["Select2ndMaxLogId"].Dispose();
        }
        string strRetValue;
        string strSql = " SELECT MAX( LogId ) FROM SalaryPakHisDetls WHERE EmpId='" + strEmpId + "'"
            + " AND LogId < ( SELECT MAX( LogId ) FROM SalaryPakHisDetls WHERE EmpId='" + strEmpId + "')";
        SqlCommand cmd = new SqlCommand(strSql);
        cmd.CommandType = CommandType.Text;

        //SqlParameter p_SalPakId = cmd.Parameters.Add("SalPakId", SqlDbType.BigInt);
        //p_SalPakId.Direction = ParameterDirection.Input;
        //p_SalPakId.Value = SalPakId;

        SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        strRetValue = objDC.GetScalarVal(cmd);
        return strRetValue;
    }

}
