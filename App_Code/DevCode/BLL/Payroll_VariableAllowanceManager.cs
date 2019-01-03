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
/// Summary description for Payroll_VariableAllowanceManager
/// </summary>
public class Payroll_VariableAllowanceManager
{
    DBConnector objDC = new DBConnector();
    #region Insert,Update,Delete
    public void InsertData(GridView grEmp, string strVID, string strEmpID, string strSHeadID, string strPayAmt, string strFrom,
        string strTo, string strIsActive, string strIsUpdate, string strInsBy, string strInsDate, string strRemarks, GridView grSch)
    {
        int i = 0;
        int j = 0;
        string strSalPakID = "";
        string strEmpSalPakId = "";
        int empCount = grEmp.Rows.Count == 0 ? 1 : grEmp.Rows.Count;

        SqlCommand[] command;
        command = new SqlCommand[empCount * 2 + grSch.Rows.Count * empCount + empCount + empCount];
        if (strIsUpdate == "N")
            strVID = Common.getMaxId("VARIABLEALLOWANCEDEDUCT", "VID");
        for (i = 0; i < empCount; i++)
        {
            command[j] = new SqlCommand("proc_Payroll_Insert_VARIABLEALLOWANCEDEDUCT");
            command[j].CommandType = CommandType.StoredProcedure;

            SqlParameter p_VID = command[j].Parameters.Add("VID", SqlDbType.BigInt);
            p_VID.Direction = ParameterDirection.Input;
            p_VID.Value = strVID;

            SqlParameter p_EMPID = command[j].Parameters.Add("EMPID", SqlDbType.Char);
            p_EMPID.Direction = ParameterDirection.Input;

            if (strIsUpdate == "Y")
            {
                p_EMPID.Value = strEmpID;
            }
            else if (grEmp.Rows.Count > 0)
            {
                p_EMPID.Value = grEmp.DataKeys[i].Values[0].ToString().Trim();
            }

            SqlParameter p_SHEADID = command[j].Parameters.Add("SHEADID", SqlDbType.BigInt);
            p_SHEADID.Direction = ParameterDirection.Input;
            p_SHEADID.Value = strSHeadID;


            SqlParameter p_PAYAMNT = command[j].Parameters.Add("PAYAMNT", SqlDbType.Decimal);
            p_PAYAMNT.Direction = ParameterDirection.Input;
            p_PAYAMNT.Value = strPayAmt;

            SqlParameter p_VALIDFROM = command[j].Parameters.Add("VALIDFROM", SqlDbType.DateTime);
            p_VALIDFROM.Direction = ParameterDirection.Input;
            p_VALIDFROM.Value = Common.ReturnDate(strFrom);

            SqlParameter p_VALIDTO = command[j].Parameters.Add("VALIDTO", SqlDbType.DateTime);
            p_VALIDTO.Direction = ParameterDirection.Input;
            p_VALIDTO.Value = Common.ReturnDate(strTo);

            SqlParameter p_ISACTIVE = command[j].Parameters.Add("ISACTIVE", SqlDbType.Char);
            p_ISACTIVE.Direction = ParameterDirection.Input;
            p_ISACTIVE.Value = strIsActive;

            SqlParameter p_INSERTEDBY = command[j].Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
            p_INSERTEDBY.Direction = ParameterDirection.Input;
            p_INSERTEDBY.Value = strInsBy;

            SqlParameter p_INSERTEDDATE = command[j].Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
            p_INSERTEDDATE.Direction = ParameterDirection.Input;
            p_INSERTEDDATE.Value = strInsDate;

            SqlParameter p_ISUPDATE = command[j].Parameters.Add("ISUPDATE", SqlDbType.Char);
            p_ISUPDATE.Direction = ParameterDirection.Input;
            p_ISUPDATE.Value = strIsUpdate;

            SqlParameter p_REMARKS = command[j].Parameters.Add("REMARKS", SqlDbType.VarChar);
            p_REMARKS.Direction = ParameterDirection.Input;
            p_REMARKS.Value = strRemarks;

            j++;
            command[j] = this.DeleteDetailsData(strVID);

            j++;
            foreach (GridViewRow gRow in grSch.Rows)
            {
                command[j] = this.InsertDetailsData(strVID, grSch.DataKeys[gRow.DataItemIndex].Values[3].ToString(), gRow);
                j++;
            }

            strSalPakID = this.IsEmpSalaryHeadExist(strSHeadID, strEmpID);
            if (string.IsNullOrEmpty(strSalPakID) == true)
            {
                strSalPakID = this.GetEmpSalPakID(strEmpID);
                command[j] = this.InsertSalaryPakDet(strEmpID, strSalPakID, strSHeadID, strInsBy, strInsDate);
                j++;
            }

            strSalPakID = "";
            strEmpSalPakId = this.GetEmpSalPakID(strEmpID);
            strSalPakID = this.IsSalaryHeadExistInPackage(strSHeadID, strEmpSalPakId);
            if (string.IsNullOrEmpty(strSalPakID) == true)
            {
                command[j] = this.InsertPackageSalaryPakDet(strEmpSalPakId, strSHeadID, strInsBy, strInsDate);
                j++;
            }

            if (strIsUpdate == "N")
                strVID = Convert.ToString(Convert.ToInt32(strVID) + 1);
        }
        objDC.MakeTransaction(command);
    }

    protected SqlCommand InsertDetailsData(string strVID,string strMonth, GridViewRow gRow)
    {
        TextBox txtAmt = (TextBox)gRow.Cells[4].FindControl("txtPayAmnt"); 

        SqlCommand cmd = new SqlCommand("proc_Payroll_Insert_VARIABLEALLOWANCEDEDUCTDETLS");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VID = cmd.Parameters.Add("VID", SqlDbType.BigInt);
        p_VID.Direction = ParameterDirection.Input;
        p_VID.Value = strVID;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = gRow.Cells[2].Text.Trim();

        SqlParameter p_VDAYS = cmd.Parameters.Add("VDAYS", SqlDbType.BigInt);
        p_VDAYS.Direction = ParameterDirection.Input;
        p_VDAYS.Value = gRow.Cells[3].Text.Trim();

        SqlParameter p_PAYAMNT = cmd.Parameters.Add("PAYAMNT", SqlDbType.Decimal);
        p_PAYAMNT.Direction = ParameterDirection.Input;
        p_PAYAMNT.Value = txtAmt.Text.Trim();

        return cmd;
    }

    
     public string IsSalaryHeadExistInPackage(string strSHeadID, string strSalPackID)
    {
        string strText = "";
        string strSQL = "SELECT SALPAKID FROM SALARYPAKDETLS WHERE SHEADID=@SHEADID AND SALPAKID=@SALPAKID";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_SHEADID = cmd.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = strSHeadID;

        SqlParameter p_SALPAKID = cmd.Parameters.Add("SALPAKID", SqlDbType.BigInt);
        p_SALPAKID.Direction = ParameterDirection.Input;
        p_SALPAKID.Value = strSalPackID;

        strText = objDC.GetScalarVal(cmd);
        return strText;
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

    public SqlCommand InsertSalaryPakDet(string strEmpID, string StrSalPakId, string strSHeadID, string strInsBy, string strInsDate)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Insert_EmpSalaryPakDetls");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpID;

        SqlParameter p_SalPakId = cmd.Parameters.Add("SalPakId", SqlDbType.BigInt);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = StrSalPakId;

        SqlParameter p_SHeadId = cmd.Parameters.Add("SHeadId", SqlDbType.BigInt);
        p_SHeadId.Direction = ParameterDirection.Input;
        p_SHeadId.Value = strSHeadID;

        SqlParameter p_PayAmt = cmd.Parameters.Add("PayAmt", SqlDbType.Decimal);
        p_PayAmt.Direction = ParameterDirection.Input;
        p_PayAmt.Value = "0";

        SqlParameter p_isInPercent = cmd.Parameters.Add("isInPercent", SqlDbType.Char);
        p_isInPercent.Direction = ParameterDirection.Input;
        p_isInPercent.Value = "N";


        SqlParameter p_PercntField = cmd.Parameters.Add("PercntField", DBNull.Value);
        p_PercntField.Direction = ParameterDirection.Input;
        p_PercntField.IsNullable = true;
       

        SqlParameter p_isBasicSal = cmd.Parameters.Add("isBasicSal", SqlDbType.Char);
        p_isBasicSal.Direction = ParameterDirection.Input;
        p_isBasicSal.Value = "N";

        SqlParameter p_ISPFUND = cmd.Parameters.Add("ISPFUND", SqlDbType.Char);
        p_ISPFUND.Direction = ParameterDirection.Input;
        p_ISPFUND.Value = "N";

        SqlParameter p_AMTCOMPAY = cmd.Parameters.Add("AMTCOMPAY", SqlDbType.Decimal);
        p_AMTCOMPAY.Direction = ParameterDirection.Input;
        p_AMTCOMPAY.Value = "0";

        SqlParameter p_TotAmnt = cmd.Parameters.Add("TotAmnt", SqlDbType.Decimal);
        p_TotAmnt.Direction = ParameterDirection.Input;
        p_TotAmnt.Value = "0";


        SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_UpdatedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_UpdatedDate.Direction = ParameterDirection.Input;
        p_UpdatedDate.Value = strInsDate;

        return cmd;
    }

    public SqlCommand InsertPackageSalaryPakDet(string StrSalPakId, string strSHeadID, string strInsBy, string strInsDate)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Insert_SalaryPakDetls");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_SalPakId = cmd.Parameters.Add("SalPakId", SqlDbType.BigInt);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = StrSalPakId;

        SqlParameter p_SHeadId = cmd.Parameters.Add("SHeadId", SqlDbType.BigInt);
        p_SHeadId.Direction = ParameterDirection.Input;
        p_SHeadId.Value = strSHeadID;

        SqlParameter p_PayAmt = cmd.Parameters.Add("PayAmt", SqlDbType.Decimal);
        p_PayAmt.Direction = ParameterDirection.Input;
        p_PayAmt.Value = "0";

        SqlParameter p_isInPercent = cmd.Parameters.Add("isInPercent", SqlDbType.Char);
        p_isInPercent.Direction = ParameterDirection.Input;
        p_isInPercent.Value = "N";

        SqlParameter p_PercntField = cmd.Parameters.Add("PercntField", DBNull.Value);
        p_PercntField.Direction = ParameterDirection.Input;
        p_PercntField.IsNullable = true;

        SqlParameter p_isBasicSal = cmd.Parameters.Add("isBasicSal", SqlDbType.Char);
        p_isBasicSal.Direction = ParameterDirection.Input;
        p_isBasicSal.Value = "N";

        SqlParameter p_ISPFUND = cmd.Parameters.Add("ISPFUND", SqlDbType.Char);
        p_ISPFUND.Direction = ParameterDirection.Input;
        p_ISPFUND.Value = "N";

        SqlParameter p_AMTCOMPAY = cmd.Parameters.Add("AMTCOMPAY", SqlDbType.Decimal);
        p_AMTCOMPAY.Direction = ParameterDirection.Input;
        p_AMTCOMPAY.Value = "0";

        SqlParameter p_TotAmnt = cmd.Parameters.Add("TotAmnt", SqlDbType.Decimal);
        p_TotAmnt.Direction = ParameterDirection.Input;
        p_TotAmnt.Value = "0";

        SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_UpdatedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_UpdatedDate.Direction = ParameterDirection.Input;
        p_UpdatedDate.Value = strInsDate;

        SqlParameter p_IsActive = cmd.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = "Y";

        return cmd;
    }

    protected SqlCommand DeleteDetailsData(string strVID)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Delete_VARIABLEALLOWANCEDEDUCTDETLS");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VID = cmd.Parameters.Add("VID", SqlDbType.BigInt);
        p_VID.Direction = ParameterDirection.Input;
        p_VID.Value = strVID;

        return cmd;
    }

    public void DeleteData(string strVID)
    {
        string strSQL = "DELETE FROM VARIABLEALLOWANCEDEDUCT WHERE VID=@VID";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        SqlParameter p_VID = cmd.Parameters.Add("VID", SqlDbType.BigInt);
        p_VID.Direction = ParameterDirection.Input;
        p_VID.Value = strVID;
        objDC.ExecuteQuery(cmd);
    }


    public void InsertVariableAllowanceData(string strVID, string strEmpId, string strSHeadID, string strDay, string strAmt, string strMonth, string strYear,
       string strIsActive, string strIsUpdate, string strInsBy, string strInsDate, string strRemarks)
    {
        int i = 0;
        int j = 0;
        string strSalPakID = "";
        string strEmpSalPakId = "";

        SqlCommand[] command;
        command = new SqlCommand[5];

        command[j] = new SqlCommand("proc_Payroll_Insert_VARIABLEALLOWANCEDEDUCT");        
        command[j].CommandType = CommandType.StoredProcedure;

        SqlParameter p_VID = command[j].Parameters.Add("VID", SqlDbType.BigInt);
        p_VID.Direction = ParameterDirection.Input;
        p_VID.Value = strVID;

        SqlParameter p_EMPID = command[j].Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpId;

        SqlParameter p_SHEADID = command[j].Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = strSHeadID;

        SqlParameter p_PAYAMNT = command[j].Parameters.Add("PAYAMNT", SqlDbType.Decimal);
        p_PAYAMNT.Direction = ParameterDirection.Input;
        p_PAYAMNT.Value = strAmt;

        SqlParameter p_VALIDFROM = command[j].Parameters.Add("VALIDFROM", SqlDbType.DateTime);
        p_VALIDFROM.Direction = ParameterDirection.Input;
        p_VALIDFROM.Value = Common.ReturnDate("01/" + strMonth + "/" + strYear);

        SqlParameter p_VALIDTO = command[j].Parameters.Add("VALIDTO", SqlDbType.DateTime);
        p_VALIDTO.Direction = ParameterDirection.Input;
        p_VALIDTO.Value = Common.ReturnDate(Common.GetMonthDay(Convert.ToInt16(strMonth), strYear).ToString() + "/" + strMonth + "/" + strYear);

        SqlParameter p_ISACTIVE = command[j].Parameters.Add("ISACTIVE", SqlDbType.Char);
        p_ISACTIVE.Direction = ParameterDirection.Input;
        p_ISACTIVE.Value = strIsActive;

        SqlParameter p_INSERTEDBY = command[j].Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = strInsBy;

        SqlParameter p_INSERTEDDATE = command[j].Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDDATE.Direction = ParameterDirection.Input;
        p_INSERTEDDATE.Value = strInsDate;

        SqlParameter p_ISUPDATE = command[j].Parameters.Add("ISUPDATE", SqlDbType.Char);
        p_ISUPDATE.Direction = ParameterDirection.Input;
        p_ISUPDATE.Value = strIsUpdate;

        SqlParameter p_REMARKS = command[j].Parameters.Add("REMARKS", SqlDbType.VarChar);
        p_REMARKS.Direction = ParameterDirection.Input;
        p_REMARKS.Value = strRemarks;

        j++;
        command[j] = this.DeleteDetailsData(strVID);

        j++;
        command[j] = this.InsertVariableDetailsData(strVID, strMonth, strYear, strDay, strAmt);

        j++;

        strSalPakID = this.IsEmpSalaryHeadExist(strSHeadID, strEmpId);
        if (string.IsNullOrEmpty(strSalPakID) == true)
        {
            strSalPakID = this.GetEmpSalPakID(strEmpId);
            command[j] = this.InsertSalaryPakDet(strEmpId, strSalPakID, strSHeadID, strInsBy, strInsDate);
            j++;
        }

        strSalPakID = "";
        strEmpSalPakId = this.GetEmpSalPakID(strEmpId);
        strSalPakID = this.IsSalaryHeadExistInPackage(strSHeadID, strEmpSalPakId);
        if (string.IsNullOrEmpty(strSalPakID) == true)
        {
            command[j] = this.InsertPackageSalaryPakDet(strEmpSalPakId, strSHeadID, strInsBy, strInsDate);
            j++;
        }       
        objDC.MakeTransaction(command);
    }


protected SqlCommand InsertVariableDetailsData(string strVID,string strMonth, string strYear,string strDays,string strAmt)
    {

        SqlCommand cmd = new SqlCommand("proc_Payroll_Insert_VARIABLEALLOWANCEDEDUCTDETLS");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VID = cmd.Parameters.Add("VID", SqlDbType.BigInt);
        p_VID.Direction = ParameterDirection.Input;
        p_VID.Value = strVID;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_VDAYS = cmd.Parameters.Add("VDAYS", SqlDbType.BigInt);
        p_VDAYS.Direction = ParameterDirection.Input;
        p_VDAYS.Value = strDays;

        SqlParameter p_PAYAMNT = cmd.Parameters.Add("PAYAMNT", SqlDbType.Decimal);
        p_PAYAMNT.Direction = ParameterDirection.Input;
        p_PAYAMNT.Value = strAmt;

        return cmd;
    }


    public string SynchronizeSalaryHead(GridView gr,string strInsBy,string strInsDate)
    {
        string strSalPakID = "";
        string strEmpSalPakId = "";
        string strSHeadID="";
        SqlCommand[] command = new SqlCommand[gr.Rows.Count];
        int i = 0;
        foreach (GridViewRow grow in gr.Rows)
        {
            if (grow.Enabled == true)
            {
                strSalPakID = "";
                strEmpSalPakId = "";
                strSHeadID="";
                strSHeadID = gr.DataKeys[grow.DataItemIndex].Values[1].ToString().Trim();
                strEmpSalPakId = this.GetEmpSalPakID(grow.Cells[2].Text.Trim());
                strSalPakID = this.IsSalaryHeadExistInPackage(strSHeadID, strEmpSalPakId);
                if (string.IsNullOrEmpty(strSalPakID) == true)
                {
                    command[i] = this.InsertPackageSalaryPakDet(strEmpSalPakId, strSHeadID, strInsBy, strInsDate);
                    i++;
                }
            }
        }
        if (i > 0)
        {
            objDC.MakeTransaction(command);
        }
        return i.ToString();
    }
    public void InsertRemoteAllowance(Payroll_RemoteAllowannce objReAllow, string IsUpdate, string IsDelete)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Insert_RemoteAllowance");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_AllowanceID = cmd.Parameters.Add("AllowanceID", SqlDbType.BigInt);
        p_AllowanceID.Direction = ParameterDirection.Input;
        p_AllowanceID.Value = objReAllow.AllowanceID;

        SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = objReAllow.EmpId;

        SqlParameter p_PostingDivID = cmd.Parameters.Add("PostingDivID", SqlDbType.BigInt);
        p_PostingDivID.Direction = ParameterDirection.Input;
        p_PostingDivID.Value = objReAllow.PostingDivID;

        SqlParameter p_SalLocId = cmd.Parameters.Add("SalLocId", SqlDbType.BigInt);
        p_SalLocId.Direction = ParameterDirection.Input;
        p_SalLocId.Value = objReAllow.SalLocId;

        SqlParameter p_PostingPlaceId = cmd.Parameters.Add("PostingPlaceId", SqlDbType.BigInt);
        p_PostingPlaceId.Direction = ParameterDirection.Input;
        p_PostingPlaceId.Value = objReAllow.PostingPlaceId;

        SqlParameter p_DateFrom = cmd.Parameters.Add("DateFrom", DBNull.Value);
        p_DateFrom.Direction = ParameterDirection.Input;
        p_DateFrom.IsNullable = true;
        if (objReAllow.DateFrom != "")
            p_DateFrom.Value = objReAllow.DateFrom;

        SqlParameter p_DateTo = cmd.Parameters.Add("DateTo", DBNull.Value);
        p_DateTo.Direction = ParameterDirection.Input;
        p_DateTo.IsNullable = true;
        if (objReAllow.DateTo != "")
            p_DateTo.Value = objReAllow.DateTo;

        SqlParameter p_Basic = cmd.Parameters.Add("BasicSalary", SqlDbType.Decimal);
        p_Basic.Direction = ParameterDirection.Input;
        p_Basic.IsNullable = true;
        if (objReAllow.Basic != "")
            p_Basic.Value = objReAllow.Basic;

        SqlParameter p_Percentage = cmd.Parameters.Add("Percentage", DBNull.Value);
        p_Percentage.Direction = ParameterDirection.Input;
        p_Percentage.IsNullable = true;
        if (objReAllow.Percentage != "")
            p_Percentage.Value = objReAllow.Percentage;

        SqlParameter p_Amount = cmd.Parameters.Add("Amount", DBNull.Value);
        p_Amount.Direction = ParameterDirection.Input;
        p_Amount.IsNullable = true;
        if (objReAllow.Amount != "")
            p_Amount.Value = objReAllow.Amount;

        SqlParameter p_Remarks = cmd.Parameters.Add("Remarks", SqlDbType.VarChar);
        p_Remarks.Direction = ParameterDirection.Input; 
            p_Remarks.Value = objReAllow.Remarks;

        SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = objReAllow.InsertedBy;

        SqlParameter p_UpdatedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_UpdatedDate.Direction = ParameterDirection.Input;
        p_UpdatedDate.Value = objReAllow.InsertedDate;

        SqlParameter p_IsUpdate = cmd.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = cmd.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        try
        {
            objDC.ExecuteQuery(cmd);
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

    public void InsertChildEduAllowance(Payroll_ChildEduAllowannce objEduAllow, string IsUpdate, string IsDelete)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Insert_ChildEduAllowance");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_AllowanceID = cmd.Parameters.Add("AllowanceID", SqlDbType.BigInt);
        p_AllowanceID.Direction = ParameterDirection.Input;
        p_AllowanceID.Value = objEduAllow.AllowanceID;

        SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = objEduAllow.EmpId;

        SqlParameter p_ChildName = cmd.Parameters.Add("ChildName", SqlDbType.VarChar);
        p_ChildName.Direction = ParameterDirection.Input;
        p_ChildName.Value = objEduAllow.ChildName;

        SqlParameter p_ChildDOB = cmd.Parameters.Add("ChildDOB", DBNull.Value);
        p_ChildDOB.Direction = ParameterDirection.Input;
        p_ChildDOB.IsNullable = true;
        if (objEduAllow.ChildDOB != "")
            p_ChildDOB.Value = objEduAllow.ChildDOB;

        SqlParameter p_Gender = cmd.Parameters.Add("Gender", SqlDbType.Char);
        p_Gender.Direction = ParameterDirection.Input;
        p_Gender.Value = objEduAllow.Gender;

        SqlParameter p_Age = cmd.Parameters.Add("Age", DBNull.Value);
        p_Age.Direction = ParameterDirection.Input;
        p_Age.IsNullable = true;
        if (objEduAllow.Age != "")
            p_Age.Value = objEduAllow.Age;

        SqlParameter p_Amount = cmd.Parameters.Add("Amount", DBNull.Value);
        p_Amount.Direction = ParameterDirection.Input;
        p_Amount.IsNullable = true;
        if (objEduAllow.Amount != "")
            p_Amount.Value = objEduAllow.Amount;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = objEduAllow.VMONTH;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = objEduAllow.VYEAR;

        SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = objEduAllow.InsertedBy;

        SqlParameter p_UpdatedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_UpdatedDate.Direction = ParameterDirection.Input;
        p_UpdatedDate.Value = objEduAllow.InsertedDate;

        SqlParameter p_IsUpdate = cmd.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = cmd.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        try
        {
            objDC.ExecuteQuery(cmd);
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
    #endregion
    #region Select
    public DataTable SelectVariableList(string strVID, string strIsActive, string strEmpIDs,string strMonth,string strYear)
    {
        string strSQL = "SELECT E.EMPID,E.FULLNAME,J.DesigName AS JobTitleName,L.LocCatName AS SalLocName,S.HEADNAME,V.SHEADID, " +
                        "V.PAYAMNT,V.ValidFrom,V.ValidTo,V.ISACTIVE,V.VID,V.REMARKS  " +
                        "FROM VARIABLEALLOWANCEDEDUCT V,SALARYHEAD S,EMPINFO E,Designation J,LocationCategory L " +
                        "WHERE V.SHEADID=S.SHEADID AND V.EMPID=E.EMPID AND E.DesigId=J.DesigId AND E.LocCatId=L.LocCatId" +
                        " AND E.EmpStatus='A' AND E.ISDELETED='N' AND MONTH(VALIDFROM)=" + strMonth + " AND YEAR(VALIDFROM)=" + strYear;

        if (strVID != "0")
        {
            strSQL = strSQL + " AND V.VID=@VID ";
        }
        if (strIsActive != "")
        {
            strSQL = strSQL + " AND V.ISACTIVE=@ISACTIVE ";
        }
        if ((strEmpIDs != "") && (strEmpIDs != "-1"))
            strSQL = strSQL + " AND V.EMPID IN ('" + strEmpIDs + "') ";


        strSQL = strSQL + " ORDER BY V.EMPID,V.SHEADID";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        if (strVID != "0")
        {
            SqlParameter p_VID = cmd.Parameters.Add("VID", SqlDbType.BigInt);
            p_VID.Direction = ParameterDirection.Input;
            p_VID.Value = strVID;
        }
        if (strIsActive != "")
        {
            SqlParameter p_ISACTIVE = cmd.Parameters.Add("ISACTIVE", SqlDbType.Char);
            p_ISACTIVE.Direction = ParameterDirection.Input;
            p_ISACTIVE.Value = strIsActive;
        }
        objDC.CreateDT(cmd, "VariableList");
        return objDC.ds.Tables["VariableList"];
    }


    public bool IsDuplicateData(string strHeadId, string strDate,string strEmpID)
    {
        string strRetText = "";
        string strSQL = "select VID from VARIABLEALLOWANCEDEDUCT WHERE SHEADID=@SHEADID AND EMPID=@EMPID AND @vDATE BETWEEN VALIDFROM AND VALIDTO ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        SqlParameter p_SHEADID = cmd.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = strHeadId;
        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;
        SqlParameter p_vDATE = cmd.Parameters.Add("vDATE", SqlDbType.DateTime);
        p_vDATE.Direction = ParameterDirection.Input;
        p_vDATE.Value = strDate;
        strRetText = objDC.GetScalarVal(cmd);
        if (string.IsNullOrEmpty(strRetText) == false)
            return true;
        else
            return false;
    }

    public DataTable SelectDetailsData(string strVID)
    {
        string strSQL = "SELECT * FROM VARIABLEALLOWANCEDEDUCTDETLS WHERE VID=@VID";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_VID = cmd.Parameters.Add("VID", SqlDbType.BigInt);
        p_VID.Direction = ParameterDirection.Input;
        p_VID.Value = strVID;

        objDC.CreateDT(cmd, "SelectDetailsData");
        return objDC.ds.Tables["SelectDetailsData"];
    }

    public string IsEmpSalaryHeadExist(string strSHeadID,string strEmpID)
    {
        string strText = "";
        string strSQL = "SELECT SALPAKID FROM EmpSalaryPakDetls WHERE SHEADID=@SHEADID AND EMPID=@EMPID";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_SHEADID = cmd.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = strSHeadID;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        strText = objDC.GetScalarVal(cmd);
        return strText;
    }
    public DataTable SelectLWPDeduction(string strVID, string strEmpId)
    {
        string strSQL = "";

        if (strVID != "0")
            strSQL = "SELECT VM.*,VD.VMonth,VD.VYear,VD.VDays FROM VARIABLEALLOWANCEDEDUCT VM,VARIABLEALLOWANCEDEDUCTDETLS VD WHERE VM.VID=VD.VID AND VM.SHeadId=9 AND VM.VID=@VID AND VM.EmpId=@EmpId";
        else
            strSQL = "SELECT VM.*,VD.VMonth,VD.VYear,VD.VDays FROM VARIABLEALLOWANCEDEDUCT VM,VARIABLEALLOWANCEDEDUCTDETLS VD WHERE VM.VID=VD.VID AND VM.SHeadId=9 AND VM.EmpId=@EmpId";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_VID = cmd.Parameters.Add("VID", SqlDbType.BigInt);
        p_VID.Direction = ParameterDirection.Input;
        p_VID.Value = strVID;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpId;

        objDC.CreateDT(cmd, "LWPDeduction");
        return objDC.ds.Tables["LWPDeduction"];
    }
  
    public DataTable SelectRemoteAllowance(string strAllowanceID,string strEmpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_Payroll_RemoteAllowance");
        SqlParameter p_AllowanceID = command.Parameters.Add("AllowanceID", SqlDbType.BigInt);
        p_AllowanceID.Direction = ParameterDirection.Input;
        p_AllowanceID.Value = strAllowanceID;

        SqlParameter p_EMPID = command.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpId;

        objDC.CreateDSFromProc(command, "RemoteAllowance");
        return objDC.ds.Tables["RemoteAllowance"];
    }

    public DataTable SelectChildEduAllowance(string strAllowanceID, string strEmpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_Payroll_ChildEduAllowance");
        SqlParameter p_AllowanceID = command.Parameters.Add("AllowanceID", SqlDbType.BigInt);
        p_AllowanceID.Direction = ParameterDirection.Input;
        p_AllowanceID.Value = strAllowanceID;

        SqlParameter p_EMPID = command.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpId;

        objDC.CreateDSFromProc(command, "ChildEduAllowance");
        return objDC.ds.Tables["ChildEduAllowance"];
    }

#endregion

    #region Payroll Arrear Calculation

    public void InsertPayrollArrear(GridView gr, string strArrearCase, string strArrearMonth, string strFinYear, string strSalProcessMonth,
        string strInsBy, string strInsDate, string strLastUpFrom)
    {
        SqlCommand[] cmd = new SqlCommand[gr.Rows.Count];
        int i = 0;
        long lnTransID = Convert.ToInt64(Common.getMaxId("PayrollArrear", "TransID"));
        string lvPayAmt;
        string strSQL = "";
        foreach (GridViewRow gRow in gr.Rows)
        {
            strSQL = "INSERT INTO PayrollArrear(TransID,EmpID,VMonth,FiscalYrID,SHeadID,PayAmt,VDays,ValidFrom,ValidTo,"
                + " ArrearCase,ArrearMonth,InsertedBy,InsertedDate,LastUpdatedFrom,Remarks) "
                + " Values(@TransID, @EmpID, @VMonth, @FiscalYrID, @SHeadID, @PayAmt, @VDays, @ValidFrom, @ValidTo,"
                + " @ArrearCase, @ArrearMonth, @InsertedBy, @InsertedDate, @LastUpdatedFrom, @Remarks) ";
            cmd[i] = new SqlCommand(strSQL);
            cmd[i].CommandType = CommandType.Text;

            SqlParameter p_TransID = cmd[i].Parameters.Add("TransID", SqlDbType.BigInt);
            p_TransID.Direction = ParameterDirection.Input;
            p_TransID.Value = lnTransID;

            SqlParameter p_PAYEMPID = cmd[i].Parameters.Add("EMPID", SqlDbType.Char);
            p_PAYEMPID.Direction = ParameterDirection.Input;
            p_PAYEMPID.Value = gRow.Cells[1].Text.Trim();

            SqlParameter p_VMONTH = cmd[i].Parameters.Add("VMONTH", SqlDbType.BigInt);
            p_VMONTH.Direction = ParameterDirection.Input;
            p_VMONTH.Value = strSalProcessMonth;

            SqlParameter p_FiscalYrID = cmd[i].Parameters.Add("FiscalYrID", SqlDbType.BigInt);
            p_FiscalYrID.Direction = ParameterDirection.Input;
            p_FiscalYrID.Value = strFinYear;

            SqlParameter p_SHeadID = cmd[i].Parameters.Add("SHeadID", SqlDbType.BigInt);
            p_SHeadID.Direction = ParameterDirection.Input;
            p_SHeadID.Value = gRow.Cells[4].Text.Trim();

            TextBox tPayAmt = (TextBox)gRow.Cells[7].FindControl("txtPayAmnt");
            lvPayAmt = tPayAmt.Text;

            SqlParameter p_PAYAMT = cmd[i].Parameters.Add("PAYAMT", SqlDbType.Decimal);
            p_PAYAMT.Direction = ParameterDirection.Input;
            p_PAYAMT.Value = lvPayAmt;

            SqlParameter p_VDays = cmd[i].Parameters.Add("VDays", SqlDbType.Decimal);
            p_VDays.Direction = ParameterDirection.Input;
            p_VDays.Value = gRow.Cells[6].Text.Trim();

            SqlParameter p_ValidFrom = cmd[i].Parameters.Add("ValidFrom", SqlDbType.DateTime);
            p_ValidFrom.Direction = ParameterDirection.Input;
            p_ValidFrom.Value = Common.ReturnDate(gRow.Cells[8].Text.Trim());

            SqlParameter p_ValidTo = cmd[i].Parameters.Add("ValidTo", SqlDbType.DateTime);
            p_ValidTo.Direction = ParameterDirection.Input;
            p_ValidTo.Value = Common.ReturnDate(gRow.Cells[9].Text.Trim());

            SqlParameter p_ArrearCase = cmd[i].Parameters.Add("ArrearCase", SqlDbType.VarChar);
            p_ArrearCase.Direction = ParameterDirection.Input;
            p_ArrearCase.Value = strArrearCase;

            SqlParameter p_ArrearMonth = cmd[i].Parameters.Add("ArrearMonth", SqlDbType.BigInt);
            p_ArrearMonth.Direction = ParameterDirection.Input;
            p_ArrearMonth.Value = strArrearMonth;

            SqlParameter p_InsertedBy = cmd[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = strInsBy;

            SqlParameter p_InsertedDate = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate.Direction = ParameterDirection.Input;
            p_InsertedDate.Value = strInsDate;

            SqlParameter p_LastUpFrom = cmd[i].Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
            p_LastUpFrom.Direction = ParameterDirection.Input;
            p_LastUpFrom.Value = strLastUpFrom;

            SqlParameter p_Remarks = cmd[i].Parameters.Add("Remarks", SqlDbType.VarChar);
            p_Remarks.Direction = ParameterDirection.Input;
            p_Remarks.Value = strLastUpFrom;

            i++;
            lnTransID++;
        }
        objDC.MakeTransaction(cmd);
    }

    public void DeletePayrollArrearData(string strVMonth, string strFiscalYrId, string strArrearCase)
    {
        string strSQL = "DELETE FROM PayrollArrear WHERE VMonth=@VMonth AND FiscalYrId=@FiscalYrId AND ArrearCase=@ArrearCase";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_VMonth = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = strVMonth;

        SqlParameter p_FiscalYrId = cmd.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = strFiscalYrId;

        SqlParameter p_ArrearCase = cmd.Parameters.Add("ArrearCase", SqlDbType.Char);
        p_ArrearCase.Direction = ParameterDirection.Input;
        p_ArrearCase.Value = strArrearCase;

        objDC.ExecuteQuery(cmd);
    }
    public DataTable GetPayrollArrearData(string strMonth, string strYear, string strFinYear,
        string strArrearCase, string strFromDate, string strToDate)
    {
        if (objDC.ds.Tables["GetPayrollArrearData"] != null)
        {
            objDC.ds.Tables["GetPayrollArrearData"].Rows.Clear();
            objDC.ds.Tables["GetPayrollArrearData"].Dispose();
        }
        string strSQL = "";

        switch (strArrearCase)
        {
            case "1"://Confirmation            
                strSQL = "SELECT DISTINCT E.EmpId,E.FullName,E.JoiningDate,E.ContractEndDate,E.BasicSalary,E.SalPakId  AS SalaryPakId,J.DesigName AS JobTitle,G.GradeName,SPH.EffDate "
                    + " FROM EmpInfo E LEFT OUTER JOIN Designation J ON E.DesigId=J.DesigId"
                    + " LEFT OUTER  JOIN GradeList G ON E.GradeId=G.GradeId"
                    + " INNER JOIN SalaryPakHisDetls SPH ON E.EmpId=SPH.EmpId "                 
                    + " WHERE E.EmpStatus='A' AND E.IsPayrollStaff='Y'"
                    + " AND SPH.LASTUPDATEDFROM='Confirmation' AND SPH.EffDate BETWEEN '" + strFromDate + "' AND '" + strToDate + "'";
                break;
            //case "2"://Previous Months Joining              
            //    strSQL = "SELECT DISTINCT E.EmpId,E.FullName,E.JoiningDate,E.ContExpDate,E.BasicSal,E.SalPakId,J.JobTitle,G.GradeName"
            //        + " FROM EmpInfo E INNER JOIN JobTitle J ON E.DesgId=J.JbTlId"
            //        + " INNER JOIN GradeList G ON E.GradeId=G.GradeId"
            //        + " WHERE E.Status='A' AND IsPayrollStaff='Y'  AND E.JoiningDate BETWEEN '" + strFromDate + "' AND '" + strToDate + "'"
            //        + " AND E.PayEmpId NOT IN (SELECT PM.EmpId FROM PayslipMst PM WHERE E.PayEmpId=PM.EmpId"
            //        + " AND PM.VMonth>=" + strMonth + " AND PM.VYear=" + strYear + ")";
            //    break;
            //case "3"://LWOP
            //    strSQL = "SELECT E.EmpId,E.FullName,E.JoiningDate,E.ContExpDate,E.BasicSal,E.SalPakId,J.JobTitle,G.GradeName,SUM(LD.Duration) AS Duration"
            //        + " FROM EmpInfo E INNER JOIN JobTitle J ON E.DesgId=J.JbTlId"
            //        + " INNER JOIN GradeList G ON E.GradeId=G.GradeId"
            //        + " INNER JOIN LeavAppMst LM ON E.EmpId=LM.EmpId "
            //        + " INNER JOIN LeavAppDets LD ON LM.LvAppId=LD.LvAppId WHERE E.Status='A' AND E.IsPayrollStaff='Y' "
            //        + " AND LM.AppStatus='A' AND LD.LType=5 AND LD.LeaveStart BETWEEN '" + strFromDate + "' AND '" + strToDate + "'"
            //        + " AND LD.LeaveEnd BETWEEN '" + strFromDate + "' AND '" + strToDate + "'"
            //        + " GROUP BY E.EmpId,E.FullName,E.JoiningDate,E.ContExpDate,E.BasicSal,E.SalaryPakId,J.JobTitle,G.GradeName";
            //    break;
            case "4"://Promotion
                strSQL = "SELECT DISTINCT E.EmpId,E.FullName,E.JoiningDate,E.ContractEndDate,E.BasicSalary,E.SalPakId  AS SalaryPakId,J.DesigName AS JobTitle,G.GradeName,SPH.EffDate "
                    + " FROM EmpInfo E LEFT OUTER JOIN Designation J ON E.DesigId=J.DesigId"
                    + " LEFT OUTER  JOIN GradeList G ON E.GradeId=G.GradeId"
                    + " INNER JOIN SalaryPakHisDetls SPH ON E.EmpId=SPH.EmpId "
                    + " INNER JOIN EmpTransitionLog ET ON E.EmpId=ET.EmpId "
                    + " WHERE E.EmpStatus='A' AND E.IsPayrollStaff='Y'"
                    + " AND SPH.LASTUPDATEDFROM='Promotion' AND ET.EffDate BETWEEN '" + strFromDate + "' AND '" + strToDate + "'";
                break;
            case "5"://Salary Increment
                strSQL = "SELECT DISTINCT E.EmpId,E.FullName,E.JoiningDate,E.ContractEndDate,E.BasicSalary,"
                    + " E.SalPakId AS SalaryPakId,J.DesigName AS JobTitle,G.GradeName,SPH.EffDate "
                    + " FROM EmpInfo E LEFT OUTER JOIN Designation J ON E.DesigId=J.DesigId"
                    + " LEFT OUTER JOIN GradeList G ON E.GradeId=G.GradeId"
                    + " INNER JOIN SalaryPakHisDetls SPH ON E.EmpId=SPH.EmpId "
                    + " INNER JOIN EmpSalaryIncrementLog ET ON E.EmpId=ET.EmpId "
                    + " WHERE E.EmpStatus='A' AND E.IsPayrollStaff='Y' " // AND E.EmpId='E006043'
                    + " AND SPH.LASTUPDATEDFROM='Increment' AND SPH.EffDate BETWEEN '" + strFromDate + "' AND '" + strToDate + "' ORDER BY E.EmpId DESC";
                break;
            case "6"://APA
                strSQL = "SELECT DISTINCT E.EmpId,E.FullName,E.JoiningDate,E.ContractEndDate,E.BasicSalary,E.SalPakId,J.DesigName AS JobTitle,G.GradeName,SPH.EffDate "
                    + " FROM EmpInfo E INNER JOIN Designation J ON E.DesigId=J.DesigId"
                    + " INNER JOIN GradeList G ON E.GradeId=G.GradeId"
                    + " INNER JOIN SalaryPakHisDetls SPH ON E.EmpId=SPH.EmpId "
                    + " INNER JOIN APAMst ET ON E.EmpId=ET.EmpId"
                    + " WHERE E.EmpStatus='A' AND E.IsPayrollStaff='Y'"
                    + " AND SPH.LASTUPDATEDFROM='Appriasal' AND ET.EffectiveDate BETWEEN '" + strFromDate + "' AND '" + strToDate + "'";
                break;
        }
        SqlCommand cmd = new SqlCommand(strSQL);

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        objDC.CreateDT(cmd, "GetPayrollArrearData");
        return objDC.ds.Tables["GetPayrollArrearData"];
    }

    public bool IsDuplicateArrearData(string strEmpId, string strHeadId, string strSalProcessMonth, string strArrearMonth, string strFiscalYrId, string strArrearCase)
    {
        string strRetText = "";
        string strSQL = "select TransId from PayrollArrear WHERE EmpId='" + strEmpId + "'  AND SHeadId=" + strHeadId +
            " AND VMonth=" + strSalProcessMonth + " AND ArrearMonth=" + strArrearMonth + " AND FiscalYrId=" + strFiscalYrId + " AND ArrearCase='" + strArrearCase + "'";

        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_SHeadId = cmd.Parameters.Add("SHeadId", SqlDbType.BigInt);
        p_SHeadId.Direction = ParameterDirection.Input;
        p_SHeadId.Value = strHeadId;

        SqlParameter p_VMonth = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = strSalProcessMonth;

        SqlParameter p_ArrearMonth = cmd.Parameters.Add("ArrearMonth", SqlDbType.BigInt);
        p_ArrearMonth.Direction = ParameterDirection.Input;
        p_ArrearMonth.Value = strArrearMonth;

        SqlParameter p_FiscalYrId = cmd.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = strFiscalYrId;

        strRetText = objDC.GetScalarVal(cmd);
        if (string.IsNullOrEmpty(strRetText) == false)
            return true;
        else
            return false;
    }

    public string GetSalaryHeadName(string strSHeadId)
    {
        string strText = "";
        string strSQL = "SELECT HeadName From SalaryHead WHERE SHeadId=" + strSHeadId;
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_SHEADID = cmd.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = strSHeadId;

        strText = objDC.GetScalarVal(cmd);
        return strText;
    }

    public DataTable GetPayrollArrearList(string VMonth, string FiscalYrId)
    {
        string strSQL = " SELECT PA.*,e.EmpId,e.FullName,e.JoiningDate,j.JobTitle,g.GradeName,dp.DeptName,dv.DivisionName,SH.HeadName "
            + " FROM PayrollArrear PA INNER JOIN EmpInfo e ON PA.EmpId=E.EmpId "
            + " LEFT OUTER JOIN JobTitle j ON e.DesgId = j.JbTlId "
            + " LEFT OUTER JOIN GradeList g ON e.GradeID = g.GradeID "
            + " LEFT OUTER JOIN DivisionList dv ON e.DivisionId = dv.DivisionId "
            + " LEFT OUTER JOIN DeptList dp ON e.DeptId = dp.DeptId "
            + " LEFT OUTER JOIN SBUList sl ON e.SbuId = sl.SBUID "
            + " LEFT OUTER JOIN SalaryHead SH ON PA.SHeadId = SH.SHeadId "
            + " WHERE PA.VMonth=@VMonth AND PA.FiscalYrId=@FiscalYrId ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_VMonth = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = VMonth;

        SqlParameter p_FiscalYrId = cmd.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = FiscalYrId;

        objDC.CreateDT(cmd, "dtPayrollArrearList");
        return objDC.ds.Tables["dtPayrollArrearList"];
    }

    public DataTable GetSalaryData(string strEmpId, string VMonth, string VYear, string FiscalYrId, string strIsPayrollExist)
    {
        if (objDC.ds.Tables["dtGetPayslipDets"] != null)
        {
            objDC.ds.Tables["dtGetPayslipDets"].Rows.Clear();
            objDC.ds.Tables["dtGetPayslipDets"].Dispose();
        }
        string strSQL = "";
        if (strIsPayrollExist == "Y")
            strSQL = "SELECT PM.EmpId,PM.EmpId AS PayEmpId,SH.HEADNAME,PD.SHEADID,PD.PAYAMT FROM PAYSLIPDETS PD,SALARYHEAD SH,GROSSSALHEAD GH,PAYSLIPMST PM,SALARYHEADWITHSEQ SHQ"
              + " WHERE PD.SHEADID=SH.SHEADID AND PD.SHEADID=GH.SHEADID AND PD.PSBID=PM.PSBID AND PD.EMPID=PM.EMPID and PD.SHEADID=SHQ.SHEADID "
              + " AND PD.EMPID=@EMPID AND  PM.VMONTH=@VMONTH AND PM.FISCALYRID=@FISCALYRID AND PM.VYEAR=@VYEAR "
              + " ORDER BY SHQ.SEQNO ";
        else
            strSQL = "SELECT PM.EmpId,E.EmpId AS PayEmpId,SH.HEADNAME,PD.SHEADID,PD.PAYAMT FROM SalaryPakDetls PD,SALARYHEAD SH,GROSSSALHEAD GH,SalaryPakMst PM,SALARYHEADWITHSEQ SHQ,EmpInfo E"
             + " WHERE PD.SHEADID=SH.SHEADID AND PD.SHEADID=GH.SHEADID AND PD.SalPakId=PM.SalPakId AND PD.SHEADID=SHQ.SHEADID AND PM.EmpId=E.EmpId"
             + " AND PM.EMPID=@EMPID"
             + " ORDER BY SHQ.SEQNO ";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.VarChar);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpId;

        SqlParameter p_VMonth = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = VMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = VYear;

        SqlParameter p_FiscalYrId = cmd.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = FiscalYrId;

        objDC.CreateDT(cmd, "dtGetPayslipDets");
        return objDC.ds.Tables["dtGetPayslipDets"];
    }
    #endregion

    public Payroll_VariableAllowanceManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
