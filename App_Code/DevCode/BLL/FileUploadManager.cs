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
/// Summary description for FileUploadManager
/// </summary>
public class FileUploadManager
{
    DBConnector objDC = new DBConnector();
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    #region Insert,Update,Delete
    public void InsertData(GridView grEmp, string strVID, string strSHeadID, string strMonth, string strYear, 
        string strIsActive, string strIsUpdate, string strInsBy, string strInsDate, string strRemarks)
    {
        int i = 0;
        int j = 0;
        string strSalPakID = "";
        string strEmpSalPakId = "";
        int empCount = grEmp.Rows.Count == 0 ? 1 : grEmp.Rows.Count;

        SqlCommand[] command;
        command = new SqlCommand[empCount * 2 + 1 * empCount + empCount + empCount];

        if (strIsUpdate == "N")
            strVID = Common.getMaxId("VARIABLEALLOWANCEDEDUCT", "VID");

        for (i = 0; i < empCount; i++)
        {
            if (Common.CheckNullString(grEmp.Rows[i].Cells[0].Text.Trim()) != "" && grEmp.Rows[i].Cells[1].Text.Trim() != "0")
            {
                command[j] = new SqlCommand("proc_Payroll_Insert_VARIABLEALLOWANCEDEDUCT");
                command[j].CommandType = CommandType.StoredProcedure;

                SqlParameter p_VID = command[j].Parameters.Add("VID", SqlDbType.BigInt);
                p_VID.Direction = ParameterDirection.Input;
                p_VID.Value = strVID;

                SqlParameter p_EMPID = command[j].Parameters.Add("EMPID", SqlDbType.Char);
                p_EMPID.Direction = ParameterDirection.Input;
                p_EMPID.Value = grEmp.Rows[i].Cells[0].Text.Trim();

                SqlParameter p_SHEADID = command[j].Parameters.Add("SHEADID", SqlDbType.BigInt);
                p_SHEADID.Direction = ParameterDirection.Input;
                p_SHEADID.Value = strSHeadID;

                SqlParameter p_PAYAMNT = command[j].Parameters.Add("PAYAMNT", SqlDbType.Decimal);
                p_PAYAMNT.Direction = ParameterDirection.Input;
                p_PAYAMNT.Value = grEmp.Rows[i].Cells[1].Text.Trim();

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
                command[j] = this.DeleteDetailsData(strVID); //Delete details

                j++;

                command[j] = this.InsertDetailsData(strVID, strMonth, strYear, grEmp.Rows[i].Cells[1].Text.Trim()); //Insert details
                j++;
                
                strSalPakID = this.IsEmpSalaryHeadExist(strSHeadID, grEmp.Rows[i].Cells[0].Text.Trim());
                
                if (string.IsNullOrEmpty(strSalPakID) == true)
                {
                    strSalPakID = this.GetEmpSalPakID(grEmp.Rows[i].Cells[0].Text.Trim());
                    command[j] = this.InsertSalaryPakDet(grEmp.Rows[i].Cells[0].Text.Trim(), strSalPakID, strSHeadID, strInsBy, strInsDate);
                    j++;
                }
                strSalPakID = "";
                strEmpSalPakId = this.GetEmpSalPakID(grEmp.Rows[i].Cells[0].Text.Trim());
                strSalPakID = this.IsSalaryHeadExistInPackage(strSHeadID, strEmpSalPakId);
                if (string.IsNullOrEmpty(strSalPakID) == true)
                {
                    command[j] = this.InsertPackageSalaryPakDet(strEmpSalPakId, strSHeadID, strInsBy, strInsDate);
                    j++;
                }

                if (strIsUpdate == "N")
                    strVID = Convert.ToString(Convert.ToInt32(strVID) + 1);
            }
        }
        objDC.MakeTransaction(command);
    }

    protected SqlCommand InsertDetailsData(string strVID, string strMonth, string strYear, string strAmount)
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
        p_VDAYS.Value = Common.GetMonthDay(Convert.ToInt16(strMonth), strYear);

        SqlParameter p_PAYAMNT = cmd.Parameters.Add("PAYAMNT", SqlDbType.Decimal);
        p_PAYAMNT.Direction = ParameterDirection.Input;
        p_PAYAMNT.Value = strAmount;

        return cmd;
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
    public string SynchronizeSalaryHead(GridView gr, string strInsBy, string strInsDate)
    {
        string strSalPakID = "";
        string strEmpSalPakId = "";
        string strSHeadID = "";
        SqlCommand[] command = new SqlCommand[gr.Rows.Count];
        int i = 0;
        foreach (GridViewRow grow in gr.Rows)
        {
            if (grow.Enabled == true)
            {
                strSalPakID = "";
                strEmpSalPakId = "";
                strSHeadID = "";
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

    #endregion
    #region Select
    public DataTable SelectVariableList(string strVID, string strIsActive, string strEmpIDs)
    {
        string strSQL = "SELECT E.EMPID,E.FULLNAME,J.JOBTITLE,L.PostingPlaceName,S.HEADNAME,V.SHEADID,V.PAYAMNT,V.ValidFrom,V.ValidTo,V.ISACTIVE,V.VID,V.REMARKS  "
                + " FROM VARIABLEALLOWANCEDEDUCT V,SALARYHEAD S,EMPINFO E,JOBTITLE J,LOCATIONLIST L "
                + " WHERE V.SHEADID=S.SHEADID AND V.EMPID=E.EMPID AND E.DESGID=J.JBTLID AND E.LOCID=L.LOCATIONID AND E.STATUS='A' AND E.ISDELETED='N' ";

        if (strVID != "0")
        {
            strSQL = strSQL + " AND V.VID=@VID ";
        }
        if (strIsActive != "")
        {
            strSQL = strSQL + " AND V.ISACTIVE=@ISACTIVE ";
        }
        if (strEmpIDs != "")
            strSQL = strSQL + " AND V.EMPID IN (" + strEmpIDs + ") ";

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


    public bool IsDuplicateData(string strHeadId, string strDate, string strEmpID)
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

    public string IsEmpSalaryHeadExist(string strSHeadID, string strEmpID)
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

    public void InsertEmpInfoFromFile(GridView grEmp)
    {
        SqlCommand[] command = new SqlCommand[grEmp.Rows.Count];

        for (int j = 0; j < grEmp.Rows.Count; j++)
        {
            string FName = "";
            string MName = "";
            string LName = "";
            string[] name = (grEmp.Rows[j].Cells[2].Text.Trim()).Split(' ');

            if (name.Length == 2)
            {
                FName = name[0].ToString().Trim();
                LName = name[1].ToString().Trim();
            }
            else if (name.Length == 3)
            {
                FName = name[0].ToString().Trim();
                MName = name[1].ToString().Trim();
                LName = name[2].ToString().Trim();
            }
            else if (name.Length == 4)
            {
                FName = name[0].ToString().Trim();
                MName = name[1].ToString().Trim() + " " + name[2].ToString().Trim();
                LName = name[3].ToString().Trim();
            }
            else if (name.Length == 5)
            {
                FName = name[0].ToString().Trim();
                MName = name[1].ToString().Trim() + " " + name[2].ToString().Trim() + " " + name[3].ToString().Trim();
                LName = name[4].ToString().Trim();
            }

            command[j] = new SqlCommand("proc_Insert_EmpInfo_FromFile");
            command[j].CommandType = CommandType.StoredProcedure;

            SqlParameter p_EMPID = command[j].Parameters.Add("EMPID", SqlDbType.Char);
            p_EMPID.Direction = ParameterDirection.Input;
            p_EMPID.Value = grEmp.Rows[j].Cells[1].Text.Trim();

            SqlParameter p_FullName = command[j].Parameters.Add("FullName", SqlDbType.VarChar);
            p_FullName.Direction = ParameterDirection.Input;
            p_FullName.Value = grEmp.Rows[j].Cells[2].Text.Trim();

            SqlParameter p_EmpFname = command[j].Parameters.Add("EmpFname", SqlDbType.VarChar);
            p_EmpFname.Direction = ParameterDirection.Input;
            p_EmpFname.Value = FName;

            SqlParameter p_EmpMName = command[j].Parameters.Add("EmpMName", SqlDbType.VarChar);
            p_EmpMName.Direction = ParameterDirection.Input;
            p_EmpMName.Value = MName;

            SqlParameter p_EmpLName = command[j].Parameters.Add("EmpLName", SqlDbType.VarChar);
            p_EmpLName.Direction = ParameterDirection.Input;
            p_EmpLName.Value = LName;

            SqlParameter p_DesgId = command[j].Parameters.Add("DesgId", SqlDbType.BigInt);
            p_DesgId.Direction = ParameterDirection.Input;
            p_DesgId.Value = grEmp.Rows[j].Cells[3].Text.Trim();

            SqlParameter p_Sex = command[j].Parameters.Add("Sex", SqlDbType.VarChar);
            p_Sex.Direction = ParameterDirection.Input;
            p_Sex.Value = grEmp.Rows[j].Cells[4].Text.Trim();

            SqlParameter p_DivisionID = command[j].Parameters.Add("DivisionID", SqlDbType.BigInt);
            p_DivisionID.Direction = ParameterDirection.Input;
            p_DivisionID.Value = grEmp.Rows[j].Cells[5].Text.Trim();

            SqlParameter p_JoiningDate = command[j].Parameters.Add("JoiningDate", DBNull.Value);
            p_JoiningDate.Direction = ParameterDirection.Input;
            p_JoiningDate.IsNullable = true;
            if (Common.CheckNullString(grEmp.Rows[j].Cells[6].Text.Trim()) != "")
                p_JoiningDate.Value = Common.ReturnDate(grEmp.Rows[j].Cells[6].Text.Trim());
        }
        objDC.MakeTransaction(command);
    }


    public void UpdateSalaryPackage(GridView grEList, string strInsBy, string strInsDate, string strLastUpFrom)
    {
        long i = 0;
        int j = 0;
        SqlCommand[] cmd;
        cmd = new SqlCommand[grEList.Rows.Count * 5];

        string strEmpId = "";
        string strPayEmpId = "";
        string strIsConfirm = "";

        string strSalPakId = "";
        string strBasicSal = "";
        string strNewBasicSal = "";
        string strHR = "";
        string strPF = "";

        foreach (GridViewRow gRow in grEList.Rows)
        {
            strEmpId = gRow.Cells[0].Text.Trim();
                       
            strBasicSal = gRow.Cells[1].Text.Trim();
            strNewBasicSal = gRow.Cells[1].Text.Trim();
            strHR = gRow.Cells[2].Text.Trim();
            strPF = gRow.Cells[3].Text.Trim();

            //Update EmpINfo BasicSal
            cmd[i] = UpdateEmpInfoBaiscSal(strEmpId, strNewBasicSal, strInsBy, strInsDate, strLastUpFrom);
            i++;

            strSalPakId = this.GetEmpSalPakID(strEmpId);
           
            //Update BasicSal in Salary Package Details
            cmd[i] = UpdateSalaryHeadWsAmt(strSalPakId, "1", strNewBasicSal, strInsBy, strInsDate, strLastUpFrom);
            i++;
            //Update House Rent in Salary Package Details
            cmd[i] = UpdateSalaryHeadWsAmt(strSalPakId, "2", strHR, strInsBy, strInsDate, strLastUpFrom);
            i++;
            //Update PF in Salary Package Details
            cmd[i] = UpdateSalaryHeadWsAmt(strSalPakId, "13", strPF, strInsBy, strInsDate, strLastUpFrom);
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

    public SqlCommand UpdateEmpInfoBaiscSal(string strEmpId, string strPayAmt, string strInsBy, string strInsDate, string strLastUpdatedFrom)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("proc_Update_EmpBasicSal");
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.VarChar);
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

    public void InsertBonusAllowanceData(GridView gr, string strFinYear, string strReligion, string strMonth, string strYear, string strFestiveDate,
        string strSheadID, string strInsBy, string strInsDate, string FestivalID)
    {
        SqlCommand[] cmd = new SqlCommand[gr.Rows.Count];
        int i = 0;
        string strVID = Common.getMaxId("BonusAllowance", "VID");

        foreach (GridViewRow gRow in gr.Rows)
        {
            cmd[i] = new SqlCommand("proc_Payroll_Insert_BonusAllowance");
            cmd[i].CommandType = CommandType.StoredProcedure;

            SqlParameter p_VID = cmd[i].Parameters.Add("VID", SqlDbType.BigInt);
            p_VID.Direction = ParameterDirection.Input;
            p_VID.Value = Convert.ToInt64(strVID);

            SqlParameter p_EMPID = cmd[i].Parameters.Add("EMPID", SqlDbType.Char);
            p_EMPID.Direction = ParameterDirection.Input;
            p_EMPID.Value = gRow.Cells[1].Text.Trim();

            SqlParameter p_EMPTYPEID = cmd[i].Parameters.Add("EMPTYPEID", SqlDbType.BigInt);
            p_EMPTYPEID.Direction = ParameterDirection.Input;
            p_EMPTYPEID.Value = gRow.Cells[2].Text.Trim();

            SqlParameter p_VMONTH = cmd[i].Parameters.Add("VMONTH", SqlDbType.BigInt);
            p_VMONTH.Direction = ParameterDirection.Input;
            p_VMONTH.Value = strMonth;

            SqlParameter p_VYEAR = cmd[i].Parameters.Add("VYEAR", SqlDbType.BigInt);
            p_VYEAR.Direction = ParameterDirection.Input;
            p_VYEAR.Value = strYear;

            SqlParameter p_FISCALYRID = cmd[i].Parameters.Add("FISCALYRID", SqlDbType.BigInt);
            p_FISCALYRID.Direction = ParameterDirection.Input;
            p_FISCALYRID.Value = strFinYear;

            SqlParameter p_SHEADID = cmd[i].Parameters.Add("SHEADID", SqlDbType.BigInt);
            p_SHEADID.Direction = ParameterDirection.Input;
            p_SHEADID.Value = strSheadID;

            SqlParameter p_EMPBASIC = cmd[i].Parameters.Add("EMPBASIC", SqlDbType.Decimal);
            p_EMPBASIC.Direction = ParameterDirection.Input;
            p_EMPBASIC.Value = gRow.Cells[3].Text.Trim();

            SqlParameter p_PAYAMT = cmd[i].Parameters.Add("PAYAMT", SqlDbType.Decimal);
            p_PAYAMT.Direction = ParameterDirection.Input;
            p_PAYAMT.Value = gRow.Cells[4].Text.Trim();

            SqlParameter p_ISPRORATA = cmd[i].Parameters.Add("ISPRORATA", SqlDbType.Char);
            p_ISPRORATA.Direction = ParameterDirection.Input;
            p_ISPRORATA.Value = "";

            SqlParameter p_VSTATUS = cmd[i].Parameters.Add("VSTATUS", SqlDbType.Char);
            p_VSTATUS.Direction = ParameterDirection.Input;
            p_VSTATUS.Value = "P";

            SqlParameter p_INSERTTEDBY = cmd[i].Parameters.Add("INSERTTEDBY", SqlDbType.VarChar);
            p_INSERTTEDBY.Direction = ParameterDirection.Input;
            p_INSERTTEDBY.Value = strInsBy;

            SqlParameter p_INSERTTEDDATE = cmd[i].Parameters.Add("INSERTTEDDATE", SqlDbType.Char);
            p_INSERTTEDDATE.Direction = ParameterDirection.Input;
            p_INSERTTEDDATE.Value = strInsDate;

            SqlParameter p_RELIGION = cmd[i].Parameters.Add("RELIGIONID", SqlDbType.BigInt);
            p_RELIGION.Direction = ParameterDirection.Input;
            p_RELIGION.Value = gRow.Cells[5].Text.Trim();

            SqlParameter p_PRORATADAYS = cmd[i].Parameters.Add("PRORATADAYS", SqlDbType.Char);
            p_PRORATADAYS.Direction = ParameterDirection.Input;
            p_PRORATADAYS.Value = 0;

            SqlParameter p_FESTIVEDATE = cmd[i].Parameters.Add("FESTIVEDATE", SqlDbType.DateTime);
            p_FESTIVEDATE.Direction = ParameterDirection.Input;
            p_FESTIVEDATE.Value = strFestiveDate;

            SqlParameter p_FestivalID = cmd[i].Parameters.Add("FestivalID", SqlDbType.BigInt);
            p_FestivalID.Direction = ParameterDirection.Input;
            p_FestivalID.Value = gRow.Cells[6].Text.Trim();

            i++;
            strVID = (Convert.ToInt64(strVID) + 1).ToString();  
        }
        objDC.MakeTransaction(cmd);
    }

    public void DeleteBonusAllowanceData(string strMonth, string strYear, string strFinYear, string strSheadId, string strRelegion, string strFistavalID)
    {
        SqlCommand cmd = new SqlCommand("Proc_Payroll_Delete_BonusAllowance");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_FISCALYRID = cmd.Parameters.Add("FISCALYRID", SqlDbType.BigInt);
        p_FISCALYRID.Direction = ParameterDirection.Input;
        p_FISCALYRID.Value = strFinYear;

        SqlParameter p_SHEADID = cmd.Parameters.Add("SHEADID", SqlDbType.BigInt);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = strSheadId;

        SqlParameter p_Religion = cmd.Parameters.Add("Religion", SqlDbType.VarChar);
        p_Religion.Direction = ParameterDirection.Input;
        p_Religion.Value = strRelegion;

        SqlParameter p_FestivalID = cmd.Parameters.Add("FestivalID", SqlDbType.BigInt);
        p_FestivalID.Direction = ParameterDirection.Input;
        p_FestivalID.Value = strFistavalID;




        objDC.ExecuteQuery(cmd);
    }
    #endregion


    #region UploadIncrement


    public void InsertIncrementData(GridView grEmp, string strMonth, string strYear,
        string strIsActive, string strIsUpdate, string strInsBy, string strInsDate, string strRemarks)
    {
        int i = 0;
        int j = 0;
        string strVID = "";
        string strLogId = "";
        string strSalPakID = "";
        string strEmpSalPakId = "";
        int empCount = grEmp.Rows.Count == 0 ? 1 : grEmp.Rows.Count;

        SqlCommand[] command;
        command = new SqlCommand[empCount + (empCount * 8)];

        if (strIsUpdate == "N")
            strVID = Common.getMaxId("EmpSalaryIncrementLog", "LogId");

        strLogId = Common.getMaxId("EmpActionLog", "LogId");
        for (i = 0; i < empCount; i++)
        {
            command[j] = new SqlCommand("proc_Insert_EmpSalaryIncrement");
            command[j].CommandType = CommandType.StoredProcedure;

            SqlParameter p_ConfirmId = command[j].Parameters.Add("LogId", SqlDbType.BigInt);
            p_ConfirmId.Direction = ParameterDirection.Input;
            p_ConfirmId.Value = strVID;

            SqlParameter p_EmpId = command[j].Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = grEmp.Rows[i].Cells[1].Text.Trim();

            SqlParameter p_ActionId = command[j].Parameters.Add("ActionId", SqlDbType.BigInt);
            p_ActionId.Direction = ParameterDirection.Input;
            p_ActionId.Value = "31";

            SqlParameter p_ActionDate = command[j].Parameters.Add("ActionDate", DBNull.Value);
            p_ActionDate.Direction = ParameterDirection.Input;
            p_ActionDate.IsNullable = true;
            if (strInsDate != "")
                p_ActionDate.Value = Common.ReturnDate(grEmp.Rows[i].Cells[8].Text.Trim());

            SqlParameter p_COLA = command[j].Parameters.Add("COLA", SqlDbType.Decimal);
            p_COLA.Direction = ParameterDirection.Input;
            p_COLA.Value = "0";

            SqlParameter p_GroupPer = command[j].Parameters.Add("GroupPer", SqlDbType.Decimal);
            p_GroupPer.Direction = ParameterDirection.Input;
            p_GroupPer.Value = "0";

            SqlParameter p_InvPer = command[j].Parameters.Add("InvPer", SqlDbType.Decimal);
            p_InvPer.Direction = ParameterDirection.Input;
            p_InvPer.Value = "0";

            SqlParameter p_NewGrossSal = command[j].Parameters.Add("NewGrossSalary", DBNull.Value);
            p_NewGrossSal.Direction = ParameterDirection.Input;
            p_NewGrossSal.IsNullable = true;
            if (grEmp.Rows[i].Cells[7].Text.Trim() != "")
                p_NewGrossSal.Value = grEmp.Rows[i].Cells[7].Text.Trim();

            SqlParameter p_IncAmount = command[j].Parameters.Add("IncPercent", SqlDbType.Decimal);
            p_IncAmount.Direction = ParameterDirection.Input;
            p_IncAmount.Value = "0";

            SqlParameter p_IncAmnt = command[j].Parameters.Add("IncAmount", SqlDbType.Decimal);
            p_IncAmnt.Direction = ParameterDirection.Input;
            p_IncAmnt.Value = "0";

            SqlParameter p_Remarks = command[j].Parameters.Add("Remarks", SqlDbType.VarChar);
            p_Remarks.Direction = ParameterDirection.Input;
            p_Remarks.Value = "";

            SqlParameter p_InsertedBy = command[j].Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = strInsBy;

            SqlParameter p_InsertedDate = command[j].Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate.Direction = ParameterDirection.Input;
            p_InsertedDate.Value = strInsDate;

            SqlParameter p_IsUpdate = command[j].Parameters.Add("IsUpdate", SqlDbType.Char);
            p_IsUpdate.Direction = ParameterDirection.Input;
            p_IsUpdate.Value = strIsUpdate;

            j++;
            if (strIsUpdate == "N")
            {
                strVID = Convert.ToString(Convert.ToInt32(strVID) + 1);

                ////command[1] = InsertEmpInfoLog(strEmpId);
                command[j] = objEmpMgr.UpdateEmpHRBasicGross(grEmp.Rows[i].Cells[1].Text.Trim(), grEmp.Rows[i].Cells[4].Text.Trim(), grEmp.Rows[i].Cells[7].Text.Trim(), "31", p_ActionDate.Value.ToString(), strInsBy, strInsDate);
                j++;

                command[j] = InsertEmpActionLog(strLogId, grEmp.Rows[i].Cells[1].Text.Trim(), "31", strInsDate, strInsBy, strInsDate);
                strLogId = Convert.ToString(Convert.ToInt32(strLogId) + 1);
                j++;

                //Basic, Housing, Medical & PF Allowance Update   

                strSalPakID = this.GetEmpSalPakID(grEmp.Rows[i].Cells[1].Text.Trim());

                if (string.IsNullOrEmpty(objEmpMgr.GetEmpIdWsSalHisPakId(strSalPakID, grEmp.Rows[i].Cells[1].Text.Trim())) == true)
                {
                    command[j] = objEmpMgr.InsertSalaryPakHisDetls(strSalPakID, grEmp.Rows[i].Cells[1].Text.Trim(), p_ActionDate.Value.ToString(), strInsBy, strInsDate, "Salary Package");
                    j++;
                }

                command[j] = UpdateSalaryPakDetls(strSalPakID, "1", grEmp.Rows[i].Cells[4].Text.Trim(), strInsBy, strInsDate, "Increment");
                j++;

                command[j] = UpdateSalaryPakDetls(strSalPakID, "2", grEmp.Rows[i].Cells[5].Text.Trim(), strInsBy, strInsDate, "Increment");
                j++;

                command[j] = UpdateSalaryPakDetls(strSalPakID, "3", grEmp.Rows[i].Cells[6].Text.Trim(), strInsBy, strInsDate, "Increment");
                j++;

                command[j] = objEmpMgr.InsertSalaryPakHisDetls(strSalPakID, grEmp.Rows[i].Cells[1].Text.Trim(), p_ActionDate.Value.ToString(), strInsBy, strInsDate, "Increment");
                j++;
            }
        }
        objDC.MakeTransaction(command);
    }


    public void InsertIncrementValue(GridView grEmp, string strMonth, string strYear,
        string strIsActive, string strIsUpdate, string strInsBy, string strActionDate, string strRemarks, string strCola, string strGroupPer, string strInvPer)
    {
        int i = 0;
        int j = 0;
        string strVID = "";
        string strLogId = "";
        string strSalPakID = "";
        string strEmpSalPakId = "";
        int empCount = grEmp.Rows.Count == 0 ? 1 : grEmp.Rows.Count;
        string strInsDate = Common.SetDateTime(DateTime.Now.ToString());
        SqlCommand[] command;
        command = new SqlCommand[empCount + (empCount * 8)];

        if (strIsUpdate == "N")
            strVID = Common.getMaxId("EmpSalaryIncrementLog", "LogId");

        strLogId = Common.getMaxId("EmpActionLog", "LogId");
        for (i = 0; i < empCount; i++)
        {
            command[j] = new SqlCommand("proc_Insert_EmpSalaryIncrement");
            command[j].CommandType = CommandType.StoredProcedure;

            SqlParameter p_ConfirmId = command[j].Parameters.Add("LogId", SqlDbType.BigInt);
            p_ConfirmId.Direction = ParameterDirection.Input;
            p_ConfirmId.Value = strVID;

            SqlParameter p_EmpId = command[j].Parameters.Add("EmpId", SqlDbType.Char);
            p_EmpId.Direction = ParameterDirection.Input;
            p_EmpId.Value = grEmp.Rows[i].Cells[0].Text.Trim();

            SqlParameter p_ActionId = command[j].Parameters.Add("ActionId", SqlDbType.BigInt);
            p_ActionId.Direction = ParameterDirection.Input;
            p_ActionId.Value = "31";

            SqlParameter p_ActionDate = command[j].Parameters.Add("ActionDate", DBNull.Value);
            p_ActionDate.Direction = ParameterDirection.Input;
            p_ActionDate.IsNullable = true;
            if (strActionDate != "")
                p_ActionDate.Value = Common.ReturnDate(strActionDate);

            SqlParameter p_COLA = command[j].Parameters.Add("COLA", SqlDbType.Decimal);
            p_COLA.Direction = ParameterDirection.Input;
            p_COLA.Value = strCola;

            SqlParameter p_GroupPer = command[j].Parameters.Add("GroupPer", SqlDbType.Decimal);
            p_GroupPer.Direction = ParameterDirection.Input;
            p_GroupPer.Value = strGroupPer;

            SqlParameter p_InvPer = command[j].Parameters.Add("InvPer", SqlDbType.Decimal);
            p_InvPer.Direction = ParameterDirection.Input;
            p_InvPer.Value = strInvPer;

            SqlParameter p_NewGrossSal = command[j].Parameters.Add("NewGrossSalary", DBNull.Value);
            p_NewGrossSal.Direction = ParameterDirection.Input;
            p_NewGrossSal.IsNullable = true;
            if (grEmp.Rows[i].Cells[10].Text.Trim() != "")
                p_NewGrossSal.Value = grEmp.Rows[i].Cells[10].Text.Trim();

            decimal strIncPer =Convert.ToDecimal(strCola)+Convert.ToDecimal(strGroupPer)+Convert.ToDecimal(strInvPer);
            
            SqlParameter p_IncAmount = command[j].Parameters.Add("IncPercent", SqlDbType.Decimal);
            p_IncAmount.Direction = ParameterDirection.Input;
            p_IncAmount.Value = strIncPer.ToString();

            SqlParameter p_IncAmnt = command[j].Parameters.Add("IncAmount", SqlDbType.Decimal);
            p_IncAmnt.Direction = ParameterDirection.Input;
            p_IncAmnt.Value = (Convert.ToDecimal(p_NewGrossSal.Value) - Convert.ToDecimal(grEmp.Rows[i].Cells[5].Text.Trim())).ToString();

            SqlParameter p_Remarks = command[j].Parameters.Add("Remarks", SqlDbType.VarChar);
            p_Remarks.Direction = ParameterDirection.Input;
            p_Remarks.Value = strRemarks;

            SqlParameter p_InsertedBy = command[j].Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = strInsBy;

            SqlParameter p_InsertedDate = command[j].Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate.Direction = ParameterDirection.Input;
            p_InsertedDate.Value = strInsDate;

            SqlParameter p_IsUpdate = command[j].Parameters.Add("IsUpdate", SqlDbType.Char);
            p_IsUpdate.Direction = ParameterDirection.Input;
            p_IsUpdate.Value = strIsUpdate;

            j++;
            if (strIsUpdate == "N")
            {
                strVID = Convert.ToString(Convert.ToInt32(strVID) + 1);

                ////command[1] = InsertEmpInfoLog(strEmpId);
                command[j] = objEmpMgr.UpdateEmpHRBasicGross(grEmp.Rows[i].Cells[0].Text.Trim(), grEmp.Rows[i].Cells[9].Text.Trim(), grEmp.Rows[i].Cells[10].Text.Trim(), "31", p_ActionDate.Value.ToString(), strInsBy, strInsDate);
                j++;

                command[j] = InsertEmpActionLog(strLogId, grEmp.Rows[i].Cells[0].Text.Trim(), "31", strInsDate, strInsBy, strInsDate);
                strLogId = Convert.ToString(Convert.ToInt32(strLogId) + 1);
                j++;

                //Basic, Housing, Medical & PF Allowance Update   

                strSalPakID = this.GetEmpSalPakID(grEmp.Rows[i].Cells[0].Text.Trim());

                if (string.IsNullOrEmpty(objEmpMgr.GetEmpIdWsSalHisPakId(strSalPakID, grEmp.Rows[i].Cells[0].Text.Trim())) == true)
                {
                    command[j] = objEmpMgr.InsertSalaryPakHisDetls(strSalPakID, grEmp.Rows[i].Cells[0].Text.Trim(),strActionDate, strInsBy, strInsDate, "Salary Package");
                    j++;
                }

                command[j] = UpdateSalaryPakDetls(strSalPakID, "1", grEmp.Rows[i].Cells[9].Text.Trim(), strInsBy, strInsDate, "Increment");
                j++;

                command[j] = UpdateSalaryPakDetls(strSalPakID, "2", grEmp.Rows[i].Cells[11].Text.Trim(), strInsBy, strInsDate, "Increment");
                j++;

                command[j] = UpdateSalaryPakDetls(strSalPakID, "3", grEmp.Rows[i].Cells[12].Text.Trim(), strInsBy, strInsDate, "Increment");
                j++;

                command[j] = objEmpMgr.InsertSalaryPakHisDetls(strSalPakID, grEmp.Rows[i].Cells[0].Text.Trim(),strActionDate, strInsBy, strInsDate, "Increment");
                j++;
            }
        }
        objDC.MakeTransaction(command);
    }

    public SqlCommand UpdateSalaryPakDetls(string StrSalPakId, string strSHeadId, string strPayAmnt, string strInsBy, string strInsDate, string strLastUpdatedFrom)
    {
        SqlCommand cmd = new SqlCommand("proc_Payroll_Update_SalaryPackDetls");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_SalPakId = cmd.Parameters.Add("SalPakId", SqlDbType.BigInt);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = StrSalPakId;

        SqlParameter p_SHeadId = cmd.Parameters.Add("SHeadId", SqlDbType.BigInt);
        p_SHeadId.Direction = ParameterDirection.Input;
        p_SHeadId.Value = strSHeadId;

        SqlParameter p_PayAmt = cmd.Parameters.Add("PayAmt", SqlDbType.Decimal);
        p_PayAmt.Direction = ParameterDirection.Input;
        p_PayAmt.Value = strPayAmnt;

        SqlParameter p_TotAmnt = cmd.Parameters.Add("TotAmnt", SqlDbType.Decimal);
        p_TotAmnt.Direction = ParameterDirection.Input;
        p_TotAmnt.Value = strPayAmnt;

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
    //Insert Employee ActionLog
    private SqlCommand InsertEmpActionLog(string strLogId, string strEmpId, string strActionId, string strActionDate, string strInsertedBy, string strInsertedDate)
    {
        SqlCommand command = new SqlCommand("proc_Insert_EmpActionLog");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LogId = command.Parameters.Add("LogId", SqlDbType.Char);
        p_LogId.Direction = ParameterDirection.Input;
        p_LogId.Value = strLogId;

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpId;

        SqlParameter p_ActionId = command.Parameters.Add("ActionId", SqlDbType.BigInt);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.Value = strActionId;

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

    public bool IsDuplicateIncDate(string strEmpID,string strActionDate)
    {
        string strRetText = "";
        string strSQL = "select LogId from EmpSalaryIncrementLog WHERE EMPID=@EMPID AND ActionDate=@ActionDate";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_EMPID = cmd.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = strEmpID;

        SqlParameter p_vDATE = cmd.Parameters.Add("ActionDate", SqlDbType.DateTime);
        p_vDATE.Direction = ParameterDirection.Input;
        p_vDATE.Value = strActionDate;

        strRetText = objDC.GetScalarVal(cmd);
        if (string.IsNullOrEmpty(strRetText) == false)
            return true;
        else
            return false;
    }
    #endregion
}
