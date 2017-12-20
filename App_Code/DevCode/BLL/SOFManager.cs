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
/// <summary>
/// Summary description for SOFManager
/// </summary>
public class SOFManager
{
    DBConnector objDC = new DBConnector();

    public SOFManager()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Insert Configuration Info
    // Insert or Update  or Delete Data of Source table  
    public void InsertSourceList(clsCommonSetup clsCommon, string strSourceCode, string strDesc, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_SourceList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("SourceId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = clsCommon.ID;

        SqlParameter p_Name = command.Parameters.Add("SourceName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = clsCommon.Name;

        SqlParameter p_SourceCode = command.Parameters.Add("SourceCode", SqlDbType.VarChar);
        p_SourceCode.Direction = ParameterDirection.Input;
        p_SourceCode.Value = strSourceCode;

        SqlParameter p_Desc = command.Parameters.Add("SourceDesc", SqlDbType.VarChar);
        p_Desc.Direction = ParameterDirection.Input;
        p_Desc.Value = strDesc;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = clsCommon.IsActive;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = clsCommon.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = clsCommon.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = clsCommon.InsertedDate;

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

    // Insert or Update  or Delete Data of SOF table
    public void InsertSOFList(clsSOFSetup clsSOF, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_SalarySourceList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("SalarySourceId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = clsSOF.SalarySourceId;

        SqlParameter p_Name = command.Parameters.Add("SourceType", SqlDbType.Char);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = clsSOF.SourceType;

        SqlParameter p_SalSourceName = command.Parameters.Add("SalSourceName", SqlDbType.VarChar);
        p_SalSourceName.Direction = ParameterDirection.Input;
        p_SalSourceName.Value = clsSOF.SalSourceName;

        SqlParameter p_SalSourceCode = command.Parameters.Add("SalSourceCode", SqlDbType.VarChar);
        p_SalSourceCode.Direction = ParameterDirection.Input;
        p_SalSourceCode.Value = clsSOF.SalSourceCode;

        SqlParameter p_ProjectCode = command.Parameters.Add("ProjectCode", DBNull.Value);
        p_ProjectCode.Direction = ParameterDirection.Input;
        p_ProjectCode.IsNullable = true;
        if (clsSOF.ProjectCode != "")
            p_ProjectCode.Value = clsSOF.ProjectCode;

        SqlParameter p_SOF = command.Parameters.Add("SalarySource", DBNull.Value);
        p_SOF.Direction = ParameterDirection.Input;
        p_SOF.IsNullable = true;
        if (clsSOF.SalarySource != "")
            p_SOF.Value = clsSOF.SalarySource;

        SqlParameter p_Salary = command.Parameters.Add("Salary", DBNull.Value);
        p_Salary.Direction = ParameterDirection.Input;
        p_Salary.IsNullable = true;
        if (clsSOF.Salary != "")
            p_Salary.Value = clsSOF.Salary;

        SqlParameter p_Bonus = command.Parameters.Add("Bonus", DBNull.Value);
        p_Bonus.Direction = ParameterDirection.Input;
        p_Bonus.IsNullable = true;
        if (clsSOF.Bonus != "")
            p_Bonus.Value = clsSOF.Bonus;

        SqlParameter p_PF = command.Parameters.Add("PF", DBNull.Value);
        p_PF.Direction = ParameterDirection.Input;
        p_PF.IsNullable = true;
        if (clsSOF.PF != "")
            p_PF.Value = clsSOF.PF;

        SqlParameter p_IT = command.Parameters.Add("IT", DBNull.Value);
        p_IT.Direction = ParameterDirection.Input;
        p_IT.IsNullable = true;
        if (clsSOF.IT != "")
            p_IT.Value = clsSOF.IT;

        SqlParameter p_PFLoan = command.Parameters.Add("PFLoan", DBNull.Value);
        p_PFLoan.Direction = ParameterDirection.Input;
        p_PFLoan.IsNullable = true;
        if (clsSOF.PFLoan != "")
            p_PFLoan.Value = clsSOF.PFLoan;

        SqlParameter p_FringePF = command.Parameters.Add("FringePF", DBNull.Value);
        p_FringePF.Direction = ParameterDirection.Input;
        p_FringePF.IsNullable = true;
        if (clsSOF.FringePF != "")
            p_FringePF.Value = clsSOF.FringePF;

        SqlParameter p_Medical = command.Parameters.Add("Medical", DBNull.Value);
        p_Medical.Direction = ParameterDirection.Input;
        p_Medical.IsNullable = true;
        if (clsSOF.Medical != "")
            p_Medical.Value = clsSOF.Medical;

        SqlParameter p_Gratuity = command.Parameters.Add("Gratuity", DBNull.Value);
        p_Gratuity.Direction = ParameterDirection.Input;
        p_Gratuity.IsNullable = true;
        if (clsSOF.Gratuity != "")
            p_Gratuity.Value = clsSOF.Gratuity;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = clsSOF.IsActive;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = clsSOF.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = clsSOF.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = clsSOF.InsertedDate;

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

    public void InsertSOFBatch(GridView grUpload,string strInsBy,string strInsDate)
    {        
         SqlCommand[] command =new SqlCommand[grUpload.Rows.Count ];
         int i = 0;
         Int32 iSalarySourceId =Convert.ToInt32(Common.getMaxId("SalarySourceList", "SalarySourceId"));
        string strSalarySourceId="";
         foreach (GridViewRow gRow in grUpload.Rows)
         {
             strSalarySourceId= GetSalarySourceId(gRow.Cells[0].Text.ToString().Trim(), gRow.Cells[1].Text.ToString().Trim(), gRow.Cells[2].Text.ToString().Trim(), gRow.Cells[3].Text.ToString().Trim());

             if (strSalarySourceId == "")
             {
                 command[i] = new SqlCommand("proc_Insert_SalarySourceList");
                 command[i].CommandType = CommandType.StoredProcedure;

                 SqlParameter p_Id = command[i].Parameters.Add("SalarySourceId", SqlDbType.BigInt);
                 p_Id.Direction = ParameterDirection.Input;
                 p_Id.Value = iSalarySourceId;

                 SqlParameter p_Name = command[i].Parameters.Add("SourceType", SqlDbType.Char);
                 p_Name.Direction = ParameterDirection.Input;
                 p_Name.Value = "";

                 SqlParameter p_SalSourceName = command[i].Parameters.Add("SalSourceName", SqlDbType.VarChar);
                 p_SalSourceName.Direction = ParameterDirection.Input;
                 p_SalSourceName.Value = "";

                 SqlParameter p_SalSourceCode = command[i].Parameters.Add("SalSourceCode", SqlDbType.VarChar);
                 p_SalSourceCode.Direction = ParameterDirection.Input;
                 p_SalSourceCode.Value = gRow.Cells[0].Text.Trim();

                 SqlParameter p_ProjectCode = command[i].Parameters.Add("ProjectCode", SqlDbType.VarChar);
                 p_ProjectCode.Direction = ParameterDirection.Input;
                 p_ProjectCode.Value = gRow.Cells[1].Text.Trim();

                 SqlParameter p_SOF = command[i].Parameters.Add("SalarySource", SqlDbType.VarChar);
                 p_SOF.Direction = ParameterDirection.Input;
                 p_SOF.Value = "";

                 SqlParameter p_Salary = command[i].Parameters.Add("Salary", SqlDbType.VarChar);
                 p_Salary.Direction = ParameterDirection.Input;
                 p_Salary.Value = gRow.Cells[2].Text.Trim();

                 SqlParameter p_Bonus = command[i].Parameters.Add("Bonus", SqlDbType.VarChar);
                 p_Bonus.Direction = ParameterDirection.Input;
                 p_Bonus.Value = gRow.Cells[2].Text.Trim();

                 SqlParameter p_PF = command[i].Parameters.Add("PF", SqlDbType.VarChar);
                 p_PF.Direction = ParameterDirection.Input;
                 p_PF.Value = gRow.Cells[2].Text.Trim();

                 SqlParameter p_IT = command[i].Parameters.Add("IT", SqlDbType.VarChar);
                 p_IT.Direction = ParameterDirection.Input;
                 p_IT.Value = gRow.Cells[2].Text.Trim();

                 SqlParameter p_PFLoan = command[i].Parameters.Add("PFLoan", SqlDbType.VarChar);
                 p_PFLoan.Direction = ParameterDirection.Input;
                 p_PFLoan.Value = gRow.Cells[2].Text.Trim();

                 SqlParameter p_FringePF = command[i].Parameters.Add("FringePF", SqlDbType.VarChar);
                 p_FringePF.Direction = ParameterDirection.Input;
                 p_FringePF.Value = gRow.Cells[3].Text.Trim();

                 SqlParameter p_Medical = command[i].Parameters.Add("Medical", SqlDbType.VarChar);
                 p_Medical.Direction = ParameterDirection.Input;
                 p_Medical.Value = gRow.Cells[3].Text.Trim();

                 SqlParameter p_Gratuity = command[i].Parameters.Add("Gratuity", SqlDbType.VarChar);
                 p_Gratuity.Direction = ParameterDirection.Input;
                 p_Gratuity.Value = gRow.Cells[3].Text.Trim();

                 SqlParameter p_IsActive = command[i].Parameters.Add("IsActive", SqlDbType.Char);
                 p_IsActive.Direction = ParameterDirection.Input;
                 p_IsActive.Value = "Y";

                 SqlParameter p_isDeleted = command[i].Parameters.Add("IsDeleted", SqlDbType.Char);
                 p_isDeleted.Direction = ParameterDirection.Input;
                 p_isDeleted.Value = "N";

                 SqlParameter p_InsertedBy = command[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                 p_InsertedBy.Direction = ParameterDirection.Input;
                 p_InsertedBy.Value = strInsBy;

                 SqlParameter p_InsertedDate = command[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                 p_InsertedDate.Direction = ParameterDirection.Input;
                 p_InsertedDate.Value = strInsDate;

                 SqlParameter p_IsUpdate = command[i].Parameters.Add("IsUpdate", SqlDbType.Char);
                 p_IsUpdate.Direction = ParameterDirection.Input;
                 p_IsUpdate.Value = "N";

                 SqlParameter p_IsDelete = command[i].Parameters.Add("IsDelete", SqlDbType.Char);
                 p_IsDelete.Direction = ParameterDirection.Input;
                 p_IsDelete.Value = "N";

                 
                 try
                 {
                     objDC.ExecuteQuery(command[i]);
                 }
                 catch (Exception ex)
                 {
                     throw (ex);
                 }
                 finally
                 {
                     //command = null;
                 }
                 iSalarySourceId++;
                 i++;

             }
         }       
    }


    // Insert or Update  or Delete Data of Appraisal Rating table  
    public void InsertCostCenterList(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_CostCenterList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("CostCenterId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = clsCommon.ID;

        SqlParameter p_Code = command.Parameters.Add("CostCenterCode", SqlDbType.VarChar);
        p_Code.Direction = ParameterDirection.Input;
        p_Code.Value = clsCommon.Name;

        SqlParameter p_Name = command.Parameters.Add("CostCenterName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = clsCommon.Name;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = clsCommon.IsActive;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = clsCommon.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = clsCommon.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = clsCommon.InsertedDate;

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
    // Insert or Update  or Delete Data of Appraisal Rating table  
    public void InsertSponsorList(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_SponsorList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("SponsorId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = clsCommon.ID;

        SqlParameter p_Name = command.Parameters.Add("SponsorName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = clsCommon.Name;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = clsCommon.IsActive;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = clsCommon.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = clsCommon.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = clsCommon.InsertedDate;

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

    // Insert or Update  or Delete Data of Appraisal Rating table  
    public void InsertProjectList(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_ProjectList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("ProjectId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = clsCommon.ID;

        SqlParameter p_Code = command.Parameters.Add("ProjectCode", SqlDbType.VarChar);
        p_Code.Direction = ParameterDirection.Input;
        p_Code.Value = clsCommon.Code;

        SqlParameter p_Name = command.Parameters.Add("ProjectName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = clsCommon.Name;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = clsCommon.IsActive;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = clsCommon.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = clsCommon.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = clsCommon.InsertedDate;

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

    // Insert or Update Data of Salary Charge table
    public void InsertEmpSalaryCharge(clsEmpSalaryCharging objclsSalCharge, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_EmpSalaryCharge");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("SalChargeId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = objclsSalCharge.SalChargeId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = objclsSalCharge.EmpId;

        SqlParameter p_EntryDate = command.Parameters.Add("EntryDate", DBNull.Value);
        p_EntryDate.Direction = ParameterDirection.Input;
        p_EntryDate.IsNullable = true;
        if (objclsSalCharge.EntryDate != "")
            p_EntryDate.Value = objclsSalCharge.EntryDate;

        SqlParameter p_SalarySourceId = command.Parameters.Add("SalarySourceId", DBNull.Value);
        p_SalarySourceId.Direction = ParameterDirection.Input;
        p_SalarySourceId.IsNullable = true;
        if (objclsSalCharge.SalarySourceId != "")
            p_SalarySourceId.Value = objclsSalCharge.SalarySourceId;

        SqlParameter p_Percentage = command.Parameters.Add("Percentage", DBNull.Value);
        p_Percentage.Direction = ParameterDirection.Input;
        p_Percentage.IsNullable = true;
        if (objclsSalCharge.Percentage != "")
            p_Percentage.Value = objclsSalCharge.Percentage;   

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = objclsSalCharge.IsActive;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = objclsSalCharge.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = objclsSalCharge.InsertedDate;

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

    public void InsertEmpSalarySourceWithBatch(GridView gvData,Int32 FiscalYrId,
        Int32 vmonth, Int32 vyear, string strInsBy, string strInsDate)
    {
        SqlCommand[] command = new SqlCommand[100];//(2 * gvData.Rows.Count) + (gvData.Rows.Count * 20)
        int i = 0;

        string strEmpId = "";
        foreach (GridViewRow gRow in gvData.Rows)
        {
            if (gRow.Cells[1].Text.Trim() != strEmpId)
            {             
                command[i] = new SqlCommand("proc_Delete_EmpSalarySource");
                command[i].CommandType = CommandType.StoredProcedure;

                SqlParameter p_VMonth = command[i].Parameters.Add("VMonth", SqlDbType.BigInt);
                p_VMonth.Direction = ParameterDirection.Input;
                p_VMonth.Value = vmonth;

                SqlParameter p_VYear = command[i].Parameters.Add("VYear", SqlDbType.BigInt);
                p_VYear.Direction = ParameterDirection.Input;
                p_VYear.Value = vyear;

                SqlParameter p_EmpTypeId = command[i].Parameters.Add("EmpTypeId", SqlDbType.BigInt);
                p_EmpTypeId.Direction = ParameterDirection.Input;
                p_EmpTypeId.Value = "1";

                SqlParameter p_EmpId = command[i].Parameters.Add("EmpId", SqlDbType.VarChar);
                p_EmpId.Direction = ParameterDirection.Input;
                p_EmpId.Value = gRow.Cells[1].Text.Trim();

                i = i + 1;

                command[i] = new SqlCommand("proc_Delete_EmpSalarySourcePayroll");
                command[i].CommandType = CommandType.StoredProcedure;

                p_VMonth = command[i].Parameters.Add("VMonth", SqlDbType.BigInt);
                p_VMonth.Direction = ParameterDirection.Input;
                p_VMonth.Value = vmonth;

                p_VYear = command[i].Parameters.Add("VYear", SqlDbType.BigInt);
                p_VYear.Direction = ParameterDirection.Input;
                p_VYear.Value = vyear;

                p_EmpTypeId = command[i].Parameters.Add("EmpTypeId", SqlDbType.BigInt);
                p_EmpTypeId.Direction = ParameterDirection.Input;
                p_EmpTypeId.Value = "1";

                p_EmpId = command[i].Parameters.Add("EmpId", SqlDbType.VarChar);
                p_EmpId.Direction = ParameterDirection.Input;
                p_EmpId.Value = gRow.Cells[0].Text.Trim();

                strEmpId = gRow.Cells[1].Text.ToString().Trim();
            }
        }        

        int intTransId = 0;
        intTransId = Convert.ToInt32(Common.getMaxId("EmpSalarySource", "TransId"));

        i = i + 1;
        strEmpId = gvData.Rows[0].Cells[1].Text.ToString().Trim();        

        foreach (GridViewRow gRow in gvData.Rows)
        {
            if (gRow.Cells[1].Text.Trim() != strEmpId)
            {
                intTransId++;
                strEmpId = gRow.Cells[1].Text.Trim();
            }

            command[i] = this.InsertEmpSalarySource1(intTransId.ToString(), gRow.Cells[1].Text.Trim(), gvData.DataKeys[gRow.DataItemIndex].Values[0].ToString(), gRow.Cells[2].Text.Trim(), gRow.Cells[3].Text.Trim(),
                    gRow.Cells[4].Text.Trim(), gRow.Cells[5].Text.Trim(), gRow.Cells[6].Text.Trim(), gRow.Cells[7].Text.Trim(), gRow.Cells[8].Text.Trim(), gRow.Cells[9].Text.Trim(),
                    gRow.Cells[10].Text.Trim(), gRow.Cells[11].Text.Trim(), gRow.Cells[12].Text.Trim(), vmonth, vyear,"", strInsBy, strInsDate);
            i++;
        }

        DataTable dtSalarySourceHeadWs = new DataTable();
        decimal dclPercentTotPayAmt = 0;
        decimal dclPercentPayAmt = 0;
        int intPayTransId = 0;
        int intRowCount = 0;

        intPayTransId = Convert.ToInt32(Common.getMaxId("EmpSalarySourcePayroll", "TransId"));

        strEmpId = gvData.Rows[0].Cells[1].Text.ToString().Trim();
        intRowCount = 1;
        string strSHeadId = "";
        foreach (GridViewRow gPayRow in gvData.Rows)
        {
            dtSalarySourceHeadWs = this.SelectSalarySourceSHeadWs(vyear.ToString(), vmonth.ToString(), gPayRow.Cells[1].Text.Trim());

            //DataRow[] foundEmpRow = dtSalarySourceHeadWs.Select("EmpId='" + gPayRow.Cells[1].Text.Trim() + "' AND SHeadId=" + dtSalarySourceHeadWs.Rows[0]["SHeadId"].ToString().Trim());
            DataRow[] foundEmpRow = dtSalarySourceHeadWs.Select("EmpId='" + gPayRow.Cells[1].Text.Trim()+ "'");
            if (foundEmpRow.Length > 0)
            {
                foreach (DataRow fRows in foundEmpRow)
                {
                    if (strSHeadId != fRows["SHeadId"].ToString().Trim())
                    {
                        strSHeadId = fRows["SHeadId"].ToString().Trim();
                        dclPercentPayAmt = 0;
                        dclPercentTotPayAmt = 0;
                        intRowCount = 1;
                    }
                    if (intRowCount < foundEmpRow.Length)
                    {
                        dclPercentPayAmt = Math.Round((Convert.ToDecimal(fRows["PayAmt"].ToString()) * Convert.ToDecimal(gPayRow.Cells[12].Text.Trim())) / 100, 0);
                        dclPercentTotPayAmt = dclPercentTotPayAmt + dclPercentPayAmt;
                    }
                    else
                        dclPercentPayAmt = Convert.ToDecimal(fRows["PayAmt"].ToString()) - dclPercentTotPayAmt;
                    intRowCount++;

                    if (fRows["EmpId"].ToString().Trim() != strEmpId)
                    {
                        intPayTransId++;
                        strEmpId = fRows["EmpId"].ToString().Trim();
                    }
                    command[i] = this.InsertEmpSalarySourcePayroll(intPayTransId.ToString(), fRows["EmpId"].ToString().Trim(), gvData.DataKeys[gPayRow.RowIndex].Values[0].ToString(),
                        fRows["SHeadId"].ToString(), gPayRow.Cells[12].Text.Trim(), dclPercentPayAmt.ToString(), FiscalYrId, vmonth, vyear, "1",strInsBy, strInsDate);
                    i++;
                }
            }
        }
        objDC.MakeTransaction(command);
    }
    #endregion

    #region Select Configuration Info

    public DataTable SelectSourceList(int Id)
    {
        SqlCommand command = new SqlCommand("proc_Select_SourceList");

        SqlParameter p_Id = command.Parameters.Add("SourceId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = Id;

        objDC.CreateDSFromProc(command, "tblSourceList");
        return objDC.ds.Tables["tblSourceList"];
    }

    public DataTable SelectCostCenterList(int Id)
    {
        SqlCommand command = new SqlCommand("proc_Select_CostCenterList");

        SqlParameter p_Id = command.Parameters.Add("CostCenterId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = Id;

        objDC.CreateDSFromProc(command, "tblCostCenterList");
        return objDC.ds.Tables["tblCostCenterList"];
    }

    public DataTable SelectSponsorList(int Id)
    {
        SqlCommand command = new SqlCommand("proc_Select_SponsorList");

        SqlParameter p_Id = command.Parameters.Add("SponsorId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = Id;

        objDC.CreateDSFromProc(command, "tblSponsorList");
        return objDC.ds.Tables["tblSponsorList"];
    }

    public DataTable SelectProjectList(int Id)
    {
        SqlCommand command = new SqlCommand("proc_Select_ProjectList");

        SqlParameter p_Id = command.Parameters.Add("ProjectId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = Id;

        objDC.CreateDSFromProc(command, "tblProjectList");
        return objDC.ds.Tables["tblProjectList"];
    }

    public DataTable SelectSOFList(int Id)
    {
        SqlCommand command = new SqlCommand("proc_Select_SOFList");

        SqlParameter p_Id = command.Parameters.Add("SalarySourceId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = Id;

        objDC.CreateDSFromProc(command, "tblSOFList");
        return objDC.ds.Tables["tblSOFList"];
    }

    //public DataTable SelectSalarySource(Int32 SOFId)
    //{
    //    string strSQL = "SELECT SalarySourceId,SalSourceCode FROM SalarySourceList WHERE ISACTIVE='Y'";

    //    return objDC.CreateDT(strSQL, "SalarySource");
    //}

    public DataTable SelectEmpSalaryCharge(int Id, string strEmpId)
    {
        if (objDC.ds.Tables["tblEmpSalaryCharge"] != null)
        {
            objDC.ds.Tables["tblEmpSalaryCharge"].Rows.Clear();
            objDC.ds.Tables["tblEmpSalaryCharge"].Dispose(); 
        }
        SqlCommand command = new SqlCommand("proc_Select_EmpSalaryCharge");

        SqlParameter p_Id = command.Parameters.Add("SalChargeId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = Id;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        objDC.CreateDSFromProc(command, "tblEmpSalaryCharge");
        return objDC.ds.Tables["tblEmpSalaryCharge"];
    }

    #endregion

    #region SOF Settlement 

    public DataTable SelectSOFSettlement(string strYear, string month,string strEmpTypeId,string strEmpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpSalarySource");

        SqlParameter p_Year = command.Parameters.Add("VYear", SqlDbType.VarChar);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = strYear;

        SqlParameter p_Month = command.Parameters.Add("VMonth", SqlDbType.VarChar);
        p_Month.Direction = ParameterDirection.Input;
        p_Month.Value = month;

        SqlParameter p_EmpTypeId = command.Parameters.Add("EmpTypeId", SqlDbType.VarChar);
        p_EmpTypeId.Direction = ParameterDirection.Input;
        p_EmpTypeId.Value = strEmpTypeId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        objDC.CreateDSFromProc(command, "tblEmpSOFSettelment");
        return objDC.ds.Tables["tblEmpSOFSettelment"];
    }

    public DataTable SelectSalaryChargeSHeadWs(string strYear, string month, string EmpId)
    {
        if (objDC.ds.Tables["tblEmpSalaryChargeSHeadWs"] != null)
        {
            objDC.ds.Tables["tblEmpSalaryChargeSHeadWs"].Rows.Clear();
            objDC.ds.Tables["tblEmpSalaryChargeSHeadWs"].Dispose();
        }
        SqlCommand command = new SqlCommand("proc_Select_EmpSalaryChargeSHeadWs");

        SqlParameter p_Year = command.Parameters.Add("VYear", SqlDbType.VarChar);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = strYear;

        SqlParameter p_Month = command.Parameters.Add("VMonth", SqlDbType.VarChar);
        p_Month.Direction = ParameterDirection.Input;
        p_Month.Value = month;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDSFromProc(command, "tblEmpSalaryChargeSHeadWs");
        return objDC.ds.Tables["tblEmpSalaryChargeSHeadWs"];
    }

    public DataTable SelectSalarySourceSHeadWs(string strYear, string month, string EmpId)
    {
        if (objDC.ds.Tables["tblEmpSalarySourceSHeadWs"] != null)
        {
            objDC.ds.Tables["tblEmpSalarySourceSHeadWs"].Rows.Clear();
            objDC.ds.Tables["tblEmpSalarySourceSHeadWs"].Dispose();
        }
        SqlCommand command = new SqlCommand("proc_Select_EmpSalarySourceSHeadWs");

        SqlParameter p_Year = command.Parameters.Add("VYear", SqlDbType.VarChar);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = strYear;

        SqlParameter p_Month = command.Parameters.Add("VMonth", SqlDbType.VarChar);
        p_Month.Direction = ParameterDirection.Input;
        p_Month.Value = month;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDSFromProc(command, "tblEmpSalarySourceSHeadWs");
        return objDC.ds.Tables["tblEmpSalarySourceSHeadWs"];
    }

    public DataTable SelectEmpSalaryChargeForSOFSettle(string strYear, string month, string EmpId)
    {
        if (objDC.ds.Tables["tblEmpSalaryChargeForSOFSettle"] != null)
        {
            objDC.ds.Tables["tblEmpSalaryChargeForSOFSettle"].Rows.Clear();
            objDC.ds.Tables["tblEmpSalaryChargeForSOFSettle"].Dispose();
        }
        SqlCommand command = new SqlCommand("proc_Select_EmpSalaryChargeForSOFSettle");

        SqlParameter p_Year = command.Parameters.Add("VYear", SqlDbType.VarChar);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = strYear;

        SqlParameter p_Month = command.Parameters.Add("VMonth", SqlDbType.VarChar);
        p_Month.Direction = ParameterDirection.Input;
        p_Month.Value = month;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDSFromProc(command, "tblEmpSalaryChargeForSOFSettle");
        return objDC.ds.Tables["tblEmpSalaryChargeForSOFSettle"];
    }

    public void InsertEmpSalarySource(GridView gvData, Int32 FiscalYrId, Int32 vmonth, Int32 vyear, string strEmpId, 
        string strInsBy, string strInsDate, string strPreMonth,string strEmpTypeId)
    {
        SqlCommand[] command = new SqlCommand[2 + gvData.Rows.Count + (gvData.Rows.Count * 25)];
        int i = 0;
        //int iRowIndex = 0;

        command[i] = new SqlCommand("proc_Delete_EmpSalarySource");
        command[i].CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMonth = command[i].Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = vmonth;

        SqlParameter p_VYear = command[i].Parameters.Add("VYear", SqlDbType.BigInt);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = vyear;

        SqlParameter p_EmpTypeId = command[i].Parameters.Add("EmpTypeId", SqlDbType.BigInt);
        p_EmpTypeId.Direction = ParameterDirection.Input;
        p_EmpTypeId.Value = strEmpTypeId;

        SqlParameter p_EmpId = command[i].Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        i = i + 1;  

        command[i] = new SqlCommand("proc_Delete_EmpSalarySourcePayroll");
        command[i].CommandType = CommandType.StoredProcedure;

        p_VMonth = command[i].Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = vmonth;

        p_VYear = command[i].Parameters.Add("VYear", SqlDbType.BigInt);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = vyear;

        p_EmpTypeId = command[i].Parameters.Add("EmpTypeId", SqlDbType.BigInt);
        p_EmpTypeId.Direction = ParameterDirection.Input;
        p_EmpTypeId.Value = strEmpTypeId;
        
        p_EmpId = command[i].Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        int intTransId = 0;
        intTransId = Convert.ToInt32(Common.getMaxId("EmpSalarySource", "TransId"));

        i = i + 1;  
        strEmpId =  gvData.Rows[0].Cells[0].Text.ToString().Trim();

        foreach (GridViewRow gRow in gvData.Rows)
        {           
            if (gRow.Cells[0].Text.Trim() != strEmpId)
            {
                intTransId++;
                strEmpId = gRow.Cells[0].Text.Trim();
            }
            command[i] = this.InsertEmpSalarySource1(intTransId.ToString(), gRow.Cells[0].Text.Trim(), gvData.DataKeys[gRow.DataItemIndex].Values[0].ToString(),
                gRow.Cells[2].Text.Trim(), "", "", "", "", "", "", "", "", "",
                gRow.Cells[3].Text.Trim(), vmonth, vyear, strEmpTypeId, strInsBy, strInsDate);
            i++;           
        }

        Payroll_PayslipApprovalManager objPayApproveMgr = new Payroll_PayslipApprovalManager();
        DataTable dtSalarySourceHeadWs = new DataTable();
        decimal dclPercentTotPayAmt = 0;
        decimal dclPercentPayAmt = 0;
        int intPayTransId = 0;
        int intRowCount = 0;

        intPayTransId = Convert.ToInt32(Common.getMaxId("EmpSalarySourcePayroll", "TransId"));

        //strEmpId = gvData.Rows[0].Cells[0].Text.ToString().Trim();  
        strEmpId = "";
        intRowCount = 1;
        string strSHeadId="";

        foreach (GridViewRow gPayRow in gvData.Rows)
        {
            if (strEmpId != gPayRow.Cells[0].Text.Trim())
            {
                strEmpId = gPayRow.Cells[0].Text.Trim();

                if (strPreMonth == "Y")
                    dtSalarySourceHeadWs = this.SelectEmpSalaryChargeForSOFSettle(vyear.ToString(), vmonth.ToString(), gPayRow.Cells[0].Text.Trim());
                else
                    dtSalarySourceHeadWs = this.SelectEmpSalaryChargeForSOFSettle(vyear.ToString(), vmonth.ToString(), gPayRow.Cells[0].Text.Trim());

                //DataRow[] foundEmpRow = dtSalarySourceHeadWs.Select("EmpId='" + gPayRow.Cells[1].Text.Trim() + "' AND SHeadId=" + dtSalarySourceHeadWs.Rows[0]["SHeadId"].ToString().Trim());
                DataRow[] foundEmpRow = dtSalarySourceHeadWs.Select("EmpId='" + gPayRow.Cells[0].Text.Trim() + "'");
                if (foundEmpRow.Length > 0)
                {
                    //iRowIndex = 0;
                    foreach (DataRow fRows in foundEmpRow)
                    {
                        if (strSHeadId != fRows["SHeadId"].ToString().Trim())
                        {
                            strSHeadId = fRows["SHeadId"].ToString().Trim();
                            dclPercentPayAmt = 0;
                            dclPercentTotPayAmt = 0;
                            intRowCount = 1;
                        }
                        DataRow[] foundSHeadRow = dtSalarySourceHeadWs.Select("EmpId='" + gPayRow.Cells[0].Text.Trim() + "' AND SHeadId=" + fRows["SHeadId"].ToString().Trim());

                        if (foundSHeadRow.Length == 1)
                            dclPercentPayAmt = Math.Round((Convert.ToDecimal(fRows["PayAmt"].ToString()) * Convert.ToDecimal(fRows["Percentage"].ToString().Trim())) / 100, 0);

                        else
                        {
                            if (intRowCount < foundSHeadRow.Length)
                            {
                                dclPercentPayAmt = Math.Round((Convert.ToDecimal(fRows["PayAmt"].ToString()) * Convert.ToDecimal(fRows["Percentage"].ToString().Trim())) / 100, 0);
                                dclPercentTotPayAmt = dclPercentTotPayAmt + dclPercentPayAmt;
                            }
                            else
                                dclPercentPayAmt = Convert.ToDecimal(fRows["PayAmt"].ToString()) - dclPercentTotPayAmt;
                            intRowCount++;
                        }

                        //if (fRows["EmpId"].ToString().Trim() != strEmpId)
                        //{
                        //    intPayTransId++;
                        //    strEmpId = fRows["EmpId"].ToString().Trim();
                        //}
                        command[i] = this.InsertEmpSalarySourcePayroll(intPayTransId.ToString(), fRows["EmpId"].ToString().Trim(), fRows["SalarySourceId"].ToString(),
                            fRows["SHeadId"].ToString(), fRows["Percentage"].ToString(), dclPercentPayAmt.ToString(), FiscalYrId, vmonth, vyear, strEmpTypeId,strInsBy, strInsDate);
                        i++;
                    }
                }
            }
        }
        objDC.MakeTransaction(command);
        //foreach (GridViewRow gPayRow in gvData.Rows)
        //{
        //    if (strPreMonth == "Y")
        //        dtSalarySourceHeadWs = this.SelectSalarySourceSHeadWs(vyear.ToString(), vmonth.ToString(), gPayRow.Cells[0].Text.Trim());
        //    else
        //        dtSalarySourceHeadWs = this.SelectSalaryChargeSHeadWs(vyear.ToString(), vmonth.ToString(), gPayRow.Cells[0].Text.Trim());

        //    //DataRow[] foundEmpRow = dtSalarySourceHeadWs.Select("EmpId='" + gPayRow.Cells[1].Text.Trim() + "' AND SHeadId=" + dtSalarySourceHeadWs.Rows[0]["SHeadId"].ToString().Trim());
        //    DataRow[] foundEmpRow = dtSalarySourceHeadWs.Select("EmpId='" + gPayRow.Cells[0].Text.Trim() + "'");
        //    if (foundEmpRow.Length > 0)
        //    {
        //        //iRowIndex = 0;
        //        foreach (DataRow fRows in foundEmpRow)
        //        {                    
        //            if (strSHeadId != fRows["SHeadId"].ToString().Trim())
        //            {
        //                strSHeadId = fRows["SHeadId"].ToString().Trim();
        //                dclPercentPayAmt = 0;
        //                dclPercentTotPayAmt = 0;
        //                intRowCount = 1;
        //            }
        //            if (intRowCount < foundEmpRow.Length)
        //            {
        //                dclPercentPayAmt = Math.Round((Convert.ToDecimal(fRows["PayAmt"].ToString()) * Convert.ToDecimal(gPayRow.Cells[4].Text.Trim())) / 100, 0);
        //                dclPercentTotPayAmt = dclPercentTotPayAmt + dclPercentPayAmt;
        //            }
        //            else
        //                dclPercentPayAmt = Convert.ToDecimal(fRows["PayAmt"].ToString()) - dclPercentTotPayAmt;
        //            intRowCount++;

        //            if (fRows["EmpId"].ToString().Trim() != strEmpId)
        //            {
        //                intPayTransId++;
        //                strEmpId = fRows["EmpId"].ToString().Trim();
        //            }
        //            command[i] = this.InsertEmpSalarySourcePayroll(intPayTransId.ToString(), fRows["EmpId"].ToString().Trim(), gvData.DataKeys[gPayRow.DataItemIndex].Values[0].ToString(),
        //                fRows["SHeadId"].ToString(), gPayRow.Cells[4].Text.Trim(), dclPercentPayAmt.ToString(), FiscalYrId, vmonth, vyear, strInsBy, strInsDate);
        //            i++;

        //            //if (i == 24)
        //            //    goto stop;
        //            //iRowIndex++;
        //        }
        //    }
        //}
        
        //foreach (DataRow dRow in dtSalarySourceHeadWs.Rows)
        //{            
        //    DataRow[] foundEmpRow = dtSalarySourceHeadWs.Select("EmpId='" + dRow["EmpId"].ToString().Trim() + "' AND SHeadId=" + dRow["SHeadId"].ToString().Trim());
        //    if (strSHeadId != dRow["SHeadId"].ToString().Trim())
        //    {
        //        strSHeadId = dRow["SHeadId"].ToString().Trim();
        //        dclPercentPayAmt = 0;
        //        dclPercentTotPayAmt = 0;
        //        intRowCount = 1;
        //    }
        //    if (intRowCount < foundEmpRow.Length)
        //    {
        //        dclPercentPayAmt = Math.Round((Convert.ToDecimal(dRow["PayAmt"].ToString()) * Convert.ToDecimal(dRow["Percentage"].ToString().Trim())) / 100, 0);
        //        dclPercentTotPayAmt = dclPercentTotPayAmt + dclPercentPayAmt;
        //    }
        //    else
        //        dclPercentPayAmt = Convert.ToDecimal(dRow["PayAmt"].ToString()) - dclPercentTotPayAmt;
        //    intRowCount++;

        //    if (dRow["EmpId"].ToString().Trim() != strEmpId)
        //    {
        //        intPayTransId++;
        //        strEmpId = dRow["EmpId"].ToString().Trim();
        //    }
        //    command[i] = this.InsertEmpSalarySourcePayroll(intPayTransId.ToString(), dRow["EmpId"].ToString().Trim(), dRow["SalarySourceId"].ToString().Trim(), dRow["SHeadId"].ToString(),
        //        dRow["Percentage"].ToString().Trim(), dclPercentPayAmt.ToString(), FiscalYrId,vmonth, vyear, strInsBy, strInsDate);
        //    i++;
        //}  
    //stop:
       
    }

    protected SqlCommand InsertEmpSalarySource1(string strTransId,string EmpId, string SalarySourceId, string SalSourceCode, string ProjectCode,
                                                string Salary, string Bonus, string PF,string IT,string PFLoan,string FringePF,string Medical,
                                                string Gratuity, string Percentage, Int32 VMonth, Int32 VYear, string strEmpTypeId,string strInsBy, string strInsDate)
    {
        SqlCommand cmd = new SqlCommand("proc_Insert_EmpSalarySource");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_TransId = cmd.Parameters.Add("TransId", SqlDbType.BigInt);
        p_TransId.Direction = ParameterDirection.Input;
        p_TransId.Value = strTransId;

        SqlParameter p_EMPID = cmd.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = EmpId;

        SqlParameter p_SOFId = cmd.Parameters.Add("SalarySourceId", DBNull.Value);
        p_SOFId.Direction = ParameterDirection.Input;
        p_SOFId.IsNullable = true;
        if (SalarySourceId != "")
            p_SOFId.Value = SalarySourceId;

        SqlParameter p_SalSourceCode = cmd.Parameters.Add("SalSourceCode", DBNull.Value);
        p_SalSourceCode.Direction = ParameterDirection.Input;
        p_SalSourceCode.IsNullable = true;
        if (SalSourceCode != "")
            p_SalSourceCode.Value = SalSourceCode;

        SqlParameter p_ProjectCode = cmd.Parameters.Add("ProjectCode", DBNull.Value);
        p_ProjectCode.Direction = ParameterDirection.Input;
        p_ProjectCode.IsNullable = true;
        if (ProjectCode != "")
            p_ProjectCode.Value = ProjectCode;

        SqlParameter p_Salary = cmd.Parameters.Add("Salary", DBNull.Value);
        p_Salary.Direction = ParameterDirection.Input;
        p_Salary.IsNullable = true;
        if (Salary != "")
            p_Salary.Value = Salary;

        SqlParameter p_Bonus = cmd.Parameters.Add("Bonus", DBNull.Value);
        p_Bonus.Direction = ParameterDirection.Input;
        p_Bonus.IsNullable = true;
        if (Bonus != "")
            p_Bonus.Value = Bonus;

        SqlParameter p_PF = cmd.Parameters.Add("PF", DBNull.Value);
        p_PF.Direction = ParameterDirection.Input;
        p_PF.IsNullable = true;
        if (PF != "")
            p_PF.Value = PF;

        SqlParameter p_IT = cmd.Parameters.Add("IT", DBNull.Value);
        p_IT.Direction = ParameterDirection.Input;
        p_IT.IsNullable = true;
        if (IT != "")
            p_IT.Value = IT;

        SqlParameter p_PFLoan = cmd.Parameters.Add("PFLoan", DBNull.Value);
        p_PFLoan.Direction = ParameterDirection.Input;
        p_PFLoan.IsNullable = true;
        if (PFLoan != "")
            p_PFLoan.Value = PFLoan;

        SqlParameter p_FringePF = cmd.Parameters.Add("FringePF", DBNull.Value);
        p_FringePF.Direction = ParameterDirection.Input;
        p_FringePF.IsNullable = true;
        if (FringePF != "")
            p_FringePF.Value = FringePF;

        SqlParameter p_Medical = cmd.Parameters.Add("Medical", DBNull.Value);
        p_Medical.Direction = ParameterDirection.Input;
        p_Medical.IsNullable = true;
        if (Medical != "")
            p_Medical.Value = Medical;

        SqlParameter p_Gratuity = cmd.Parameters.Add("Gratuity", DBNull.Value);
        p_Gratuity.Direction = ParameterDirection.Input;
        p_Gratuity.IsNullable = true;
        if (Gratuity != "")
            p_Gratuity.Value = Gratuity;

        SqlParameter p_Percentage = cmd.Parameters.Add("Percentage", DBNull.Value);
        p_Percentage.Direction = ParameterDirection.Input;
        p_Percentage.IsNullable = true;
        if (Percentage != "")
            p_Percentage.Value = Percentage;

        SqlParameter p_VMonth = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = VMonth;

        SqlParameter p_VYear = cmd.Parameters.Add("VYear", SqlDbType.BigInt);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = VYear;

        SqlParameter p_EmpTypeId = cmd.Parameters.Add("EmpTypeId", SqlDbType.BigInt);
        p_EmpTypeId.Direction = ParameterDirection.Input;
        p_EmpTypeId.Value = strEmpTypeId;

        SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        return cmd;
    }

    protected SqlCommand InsertEmpSalarySourcePayroll(string strTransId, string EmpId, string SalarySourceId, string SHeadId, string Percentage,
         string PercentAmount, Int32 FiscalYrId, Int32 VMonth, Int32 VYear, string strEmpTypeId,string strInsBy, string strInsDate)
    {
        SqlCommand cmd = new SqlCommand("proc_Insert_EmpSalarySourcePayroll");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_TransId = cmd.Parameters.Add("TransId", SqlDbType.BigInt);
        p_TransId.Direction = ParameterDirection.Input;
        p_TransId.Value = strTransId;

        SqlParameter p_EMPID = cmd.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = EmpId;

        SqlParameter p_SOFId = cmd.Parameters.Add("SalarySourceId", DBNull.Value);
        p_SOFId.Direction = ParameterDirection.Input;
        p_SOFId.IsNullable = true;
        if (SalarySourceId != "")
            p_SOFId.Value = SalarySourceId;

        SqlParameter p_SHeadId = cmd.Parameters.Add("SHeadId", SqlDbType.BigInt);
        p_SHeadId.Direction = ParameterDirection.Input;
        p_SHeadId.Value = SHeadId;

        SqlParameter p_Percentage = cmd.Parameters.Add("Percentage", DBNull.Value);
        p_Percentage.Direction = ParameterDirection.Input;
        p_Percentage.IsNullable = true;
        if (Percentage != "")
            p_Percentage.Value = Percentage;

        SqlParameter p_PercentAmount = cmd.Parameters.Add("PercentAmount", SqlDbType.Decimal);
        p_PercentAmount.Direction = ParameterDirection.Input;
        p_PercentAmount.Value = PercentAmount;

        SqlParameter p_FiscalYrId = cmd.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = FiscalYrId;

        SqlParameter p_VMonth = cmd.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = VMonth;

        SqlParameter p_VYear = cmd.Parameters.Add("VYear", SqlDbType.BigInt);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = VYear;

        SqlParameter p_EmpTypeId = cmd.Parameters.Add("EmpTypeId", SqlDbType.BigInt);
        p_EmpTypeId.Direction = ParameterDirection.Input;
        p_EmpTypeId.Value = strEmpTypeId;

        SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        return cmd;
    }

    public void InsertSalaryChargingUpData(GridView grList, string strInsBy, string strInsDate, string strRemarks)
    {
        int i = 0;

        SqlCommand[] command;
        command = new SqlCommand[grList.Rows.Count * 2];

        string strEmpId = "";

        foreach (GridViewRow gRow in grList.Rows)
        {
            if (strEmpId == "")
            {
                strEmpId = gRow.Cells[0].Text.ToString().Trim();
                //strEmpId = gRow.Cells[0].Text.ToString().Trim();

                command[i] = new SqlCommand("proc_Delete_EmpSalaryCharge");
                command[i].CommandType = CommandType.StoredProcedure;

                SqlParameter p_EmpId = command[i].Parameters.Add("EmpId", SqlDbType.VarChar);
                p_EmpId.Direction = ParameterDirection.Input;
                p_EmpId.Value = strEmpId;
                i++;
            }

            if (strEmpId != gRow.Cells[0].Text.ToString().Trim())
            {
                strEmpId = gRow.Cells[0].Text.ToString().Trim();

                command[i] = new SqlCommand("proc_Delete_EmpSalaryCharge");
                command[i].CommandType = CommandType.StoredProcedure;

                SqlParameter p_EmpId = command[i].Parameters.Add("EmpId", SqlDbType.VarChar);
                p_EmpId.Direction = ParameterDirection.Input;
                p_EmpId.Value = strEmpId;
                i++;
            }
        }

        string strSalChargeId = Common.getMaxId("EmpSalaryCharge", "SalChargeId");
        foreach (GridViewRow gRow in grList.Rows)
        {
            if (string.IsNullOrEmpty(gRow.Cells[2].Text.ToString().Trim()) == false)
            {
                command[i] = new SqlCommand("proc_Insert_EmpSalaryCharge");
                command[i].CommandType = CommandType.StoredProcedure;

                SqlParameter p_Id = command[i].Parameters.Add("SalChargeId", SqlDbType.BigInt);
                p_Id.Direction = ParameterDirection.Input;
                p_Id.Value = strSalChargeId;

                SqlParameter p_EmpId2 = command[i].Parameters.Add("EmpId", SqlDbType.VarChar);
                p_EmpId2.Direction = ParameterDirection.Input;
                p_EmpId2.Value = gRow.Cells[0].Text.ToString().Trim();

                SqlParameter p_EntryDate = command[i].Parameters.Add("EntryDate", DBNull.Value);
                p_EntryDate.Direction = ParameterDirection.Input;
                p_EntryDate.IsNullable = true;
                if (gRow.Cells[1].Text.ToString().Trim() != "")
                    p_EntryDate.Value = Common.ReturnDate(gRow.Cells[1].Text.ToString().Trim());

                SqlParameter p_SalarySourceId = command[i].Parameters.Add("SalarySourceId", DBNull.Value);
                p_SalarySourceId.Direction = ParameterDirection.Input;
                p_SalarySourceId.IsNullable = true;
                if (gRow.Cells[2].Text.ToString().Trim() != "")
                    p_SalarySourceId.Value = gRow.Cells[2].Text.ToString().Trim();

                SqlParameter p_Percentage = command[i].Parameters.Add("Percentage", DBNull.Value);
                p_Percentage.Direction = ParameterDirection.Input;
                p_Percentage.IsNullable = true;
                if (gRow.Cells[3].Text.ToString().Trim() != "")
                    p_Percentage.Value = gRow.Cells[3].Text.ToString().Trim();

                SqlParameter p_IsActive = command[i].Parameters.Add("IsActive", SqlDbType.Char);
                p_IsActive.Direction = ParameterDirection.Input;
                p_IsActive.Value = "Y";

                SqlParameter p_InsertedBy = command[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                p_InsertedBy.Direction = ParameterDirection.Input;
                p_InsertedBy.Value = strInsBy;

                SqlParameter p_InsertedDate = command[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                p_InsertedDate.Direction = ParameterDirection.Input;
                p_InsertedDate.Value = strInsDate;

                SqlParameter p_IsUpdate = command[i].Parameters.Add("IsUpdate", SqlDbType.Char);
                p_IsUpdate.Direction = ParameterDirection.Input;
                p_IsUpdate.Value = "N";

                SqlParameter p_IsDelete = command[i].Parameters.Add("IsDelete", SqlDbType.Char);
                p_IsDelete.Direction = ParameterDirection.Input;
                p_IsDelete.Value = "N";

                strSalChargeId = Convert.ToString(Convert.ToInt32(strSalChargeId) + 1);

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

    public string GetSalarySourceId(string strSalSourceCode,string strProjectCode,string strSalary,string strBenefit)
    {
        string strSalSourceId = "";

        string strSql = "SELECT SalarySourceId From SalarySourceList Where SalSourceCode='" + strSalSourceCode + "' AND ProjectCode='" + strProjectCode
            + "' AND Salary='" + strSalary + "' AND Gratuity='" + strBenefit + "'";

        SqlCommand cmd = new SqlCommand(strSql);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_SHEADID = cmd.Parameters.Add("SalSourceCode", SqlDbType.VarChar);
        p_SHEADID.Direction = ParameterDirection.Input;
        p_SHEADID.Value = strSalSourceCode;

        SqlParameter p_ProjectCode = cmd.Parameters.Add("ProjectCode", SqlDbType.VarChar);
        p_ProjectCode.Direction = ParameterDirection.Input;
        p_ProjectCode.Value = strProjectCode;

        SqlParameter p_Salary = cmd.Parameters.Add("Salary", SqlDbType.VarChar);
        p_Salary.Direction = ParameterDirection.Input;
        p_Salary.Value = strSalary;

        SqlParameter p_FringePF = cmd.Parameters.Add("Gratuity", SqlDbType.VarChar);
        p_FringePF.Direction = ParameterDirection.Input;
        p_FringePF.Value = strBenefit;

        strSalSourceId = objDC.GetScalarVal(cmd);

        return strSalSourceId;
    }

    public string GetProjectId(string strProjectName)
    {
        string strSalSourceId = "";

        string strSql = "SELECT ProjectId From ProjectList Where ProjectName='" + strProjectName+"'";

        SqlCommand cmd = new SqlCommand(strSql);
        cmd.CommandType = CommandType.Text;


        SqlParameter p_ProjectName = cmd.Parameters.Add("ProjectName", SqlDbType.VarChar);
        p_ProjectName.Direction = ParameterDirection.Input;
        p_ProjectName.Value = strProjectName;
        
        strSalSourceId = objDC.GetScalarVal(cmd);

        return strSalSourceId;
    }
    
    public DataTable SelectEmpSalarySourceMonthWs(string strEmpId,string strMonth,string strYear)
    {
        if (objDC.ds.Tables["tblEmpSalarySourceMonthWs"] != null)
        {
            objDC.ds.Tables["tblEmpSalarySourceMonthWs"].Rows.Clear();
            objDC.ds.Tables["tblEmpSalarySourceMonthWs"].Dispose();
        }
        SqlCommand command = new SqlCommand("proc_Select_EmpSalarySourceMonthWs");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_VMonth = command.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = strMonth;

        SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.BigInt);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value =strYear;

        objDC.CreateDSFromProc(command, "tblEmpSalarySourceMonthWs");
        return objDC.ds.Tables["tblEmpSalarySourceMonthWs"];
    }

    #endregion
}