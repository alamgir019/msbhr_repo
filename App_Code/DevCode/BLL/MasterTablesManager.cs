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
/// Summary description for MasterTablesManager
/// </summary>
public class MasterTablesManager
{
    DBConnector objDC = new DBConnector();

   
    #region Notification Dashboard
    public DataTable GetConfirmationEmp(string StartDate, string EndDate)
    {
        SqlCommand command = new SqlCommand("proc_Get_ConfirmationEmp");

        SqlParameter p_InsertedDate = command.Parameters.Add("StartDate", DBNull.Value);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.IsNullable = true;
        if (string.IsNullOrEmpty(StartDate) == false)
            p_InsertedDate.Value = StartDate;
        else
            p_InsertedDate.Value = "";

        SqlParameter p_EndDate = command.Parameters.Add("EndDate", DBNull.Value);
        p_EndDate.Direction = ParameterDirection.Input;
        p_EndDate.IsNullable = true;
        if (string.IsNullOrEmpty(EndDate) == false)
            p_EndDate.Value = EndDate;
        else
            p_EndDate.Value = "";

        objDC.CreateDSFromProc(command, "GetConfirmationEmp");
        return objDC.ds.Tables["GetConfirmationEmp"];
    }

    public DataTable GetEmpBirthday()
    {
        SqlCommand command = new SqlCommand("proc_Get_EmpBirthday");

        objDC.CreateDSFromProc(command, "GetEmpBirthday");
        return objDC.ds.Tables["GetEmpBirthday"];
    }

    public DataTable GetLicenseExpireDate(string StartDate, string EndDate)
    {
        SqlCommand command = new SqlCommand("proc_Get_DrivingExpDate");

        SqlParameter p_InsertedDate = command.Parameters.Add("StartDate", DBNull.Value);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.IsNullable = true;
        if (string.IsNullOrEmpty(StartDate) == false)
            p_InsertedDate.Value = StartDate;
        else
            p_InsertedDate.Value = "";

        SqlParameter p_EndDate = command.Parameters.Add("EndDate", DBNull.Value);
        p_EndDate.Direction = ParameterDirection.Input;
        p_EndDate.IsNullable = true;
        if (string.IsNullOrEmpty(EndDate) == false)
            p_EndDate.Value = EndDate;
        else
            p_EndDate.Value = "";


        objDC.CreateDSFromProc(command, "GetLicenseExpireDate");
        return objDC.ds.Tables["GetLicenseExpireDate"];
    }

    public DataTable GetContractExpireDate(string StartDate, string EndDate)
    {
        if (objDC.ds.Tables["GetContractExpireDate"] != null)
        {
            objDC.ds.Tables["GetContractExpireDate"].Rows.Clear();
            objDC.ds.Tables["GetContractExpireDate"].Dispose();
        }
        SqlCommand command = new SqlCommand("proc_Get_ContractExpDate");

        SqlParameter p_InsertedDate = command.Parameters.Add("StartDate", DBNull.Value);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.IsNullable = true;
        if (string.IsNullOrEmpty(StartDate) == false)
            p_InsertedDate.Value = StartDate;
        else
            p_InsertedDate.Value = "";

        SqlParameter p_EndDate = command.Parameters.Add("EndDate", DBNull.Value);
        p_EndDate.Direction = ParameterDirection.Input;
        p_EndDate.IsNullable = true;
        if (string.IsNullOrEmpty(EndDate) == false)
            p_EndDate.Value = EndDate;
        else
            p_EndDate.Value = "";

        objDC.CreateDSFromProc(command, "GetContractExpireDate");
        return objDC.ds.Tables["GetContractExpireDate"];
    }

    public DataTable GetRetirementDate(string StartDate, string EndDate)
    {
        if (objDC.ds.Tables["GetRetirementDate"] != null)
        {
            objDC.ds.Tables["GetRetirementDate"].Rows.Clear();
            objDC.ds.Tables["GetRetirementDate"].Dispose();
        }
        SqlCommand command = new SqlCommand("proc_Get_RetirementDate");

        SqlParameter p_InsertedDate = command.Parameters.Add("StartDate", DBNull.Value);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.IsNullable = true;
        if (string.IsNullOrEmpty(StartDate) == false)
            p_InsertedDate.Value = StartDate;
        else
            p_InsertedDate.Value = "";

        SqlParameter p_EndDate = command.Parameters.Add("EndDate", DBNull.Value);
        p_EndDate.Direction = ParameterDirection.Input;
        p_EndDate.IsNullable = true;
        if (string.IsNullOrEmpty(EndDate) == false)
            p_EndDate.Value = EndDate;
        else
            p_EndDate.Value = "";

        objDC.CreateDSFromProc(command, "GetRetirementDate");
        return objDC.ds.Tables["GetRetirementDate"];
    }

    public DataTable GetBMDCRegDate(string StartDate, string EndDate)
    {
        if (objDC.ds.Tables["GetBMDCRegDate"] != null)
        {
            objDC.ds.Tables["GetBMDCRegDate"].Rows.Clear();
            objDC.ds.Tables["GetBMDCRegDate"].Dispose();
        }
        SqlCommand command = new SqlCommand("proc_Get_BMDCRegDate");

        SqlParameter p_InsertedDate = command.Parameters.Add("StartDate", DBNull.Value);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.IsNullable = true;
        if (string.IsNullOrEmpty(StartDate) == false)
            p_InsertedDate.Value = StartDate;
        else
            p_InsertedDate.Value = "";

        SqlParameter p_EndDate = command.Parameters.Add("EndDate", DBNull.Value);
        p_EndDate.Direction = ParameterDirection.Input;
        p_EndDate.IsNullable = true;
        if (string.IsNullOrEmpty(EndDate) == false)
            p_EndDate.Value = EndDate;
        else
            p_EndDate.Value = "";

        objDC.CreateDSFromProc(command, "GetBMDCRegDate");
        return objDC.ds.Tables["GetBMDCRegDate"];
    }

    public DataTable GetDrLicRenewDate(string StartDate, string EndDate)
    {
        if (objDC.ds.Tables["GetDrLicRenewDate"] != null)
        {
            objDC.ds.Tables["GetDrLicRenewDate"].Rows.Clear();
            objDC.ds.Tables["GetDrLicRenewDate"].Dispose();
        }
        SqlCommand command = new SqlCommand("proc_Get_LicenseRenewDate");

        SqlParameter p_InsertedDate = command.Parameters.Add("StartDate", DBNull.Value);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.IsNullable = true;
        if (string.IsNullOrEmpty(StartDate) == false)
            p_InsertedDate.Value = StartDate;
        else
            p_InsertedDate.Value = "";

        SqlParameter p_EndDate = command.Parameters.Add("EndDate", DBNull.Value);
        p_EndDate.Direction = ParameterDirection.Input;
        p_EndDate.IsNullable = true;
        if (string.IsNullOrEmpty(EndDate) == false)
            p_EndDate.Value = EndDate;
        else
            p_EndDate.Value = "";

        objDC.CreateDSFromProc(command, "GetDrLicRenewDate");
        return objDC.ds.Tables["GetDrLicRenewDate"];
    }

    public DataTable GetAddResponsibility(string StartDate, string EndDate)
    {
        SqlCommand command = new SqlCommand("proc_Get_AddResponsibility");

        SqlParameter p_InsertedDate = command.Parameters.Add("StartDate", DBNull.Value);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.IsNullable = true;
        if (string.IsNullOrEmpty(StartDate) == false)
            p_InsertedDate.Value = StartDate;
        else
            p_InsertedDate.Value = "";

        SqlParameter p_EndDate = command.Parameters.Add("EndDate", DBNull.Value);
        p_EndDate.Direction = ParameterDirection.Input;
        p_EndDate.IsNullable = true;
        if (string.IsNullOrEmpty(EndDate) == false)
            p_EndDate.Value = EndDate;
        else
            p_EndDate.Value = "";

        objDC.CreateDSFromProc(command, "GetAddResponsibility");
        return objDC.ds.Tables["GetAddResponsibility"];
    }


    #endregion

    #region Insert Update Delete From Tables By Store procedure
    // Insert or Update  or Delete Data of Location table
    public void InsertLocation(Location lo, string IsUpdate, string IsDelete, string strIsActive)
    {
        SqlCommand command = new SqlCommand("proc_Insert_Location");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LocationID = command.Parameters.Add("LocationID", SqlDbType.BigInt);
        p_LocationID.Direction = ParameterDirection.Input;
        p_LocationID.Value = lo.LocationID;

        SqlParameter p_LocationName = command.Parameters.Add("LocationName", SqlDbType.VarChar);
        p_LocationName.Direction = ParameterDirection.Input;
        p_LocationName.Value = lo.LocationName;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = strIsActive;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = lo.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = lo.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = lo.InsertedDate;

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

    public void InsertLocationCategory(clsCommonSetup objCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_LocationCategory");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("LocCatId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = objCommon.ID;

        SqlParameter p_Name = command.Parameters.Add("LocCatName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = objCommon.Name;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = objCommon.IsActive;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = objCommon.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = objCommon.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = objCommon.InsertedDate;

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
    public void InsertDisicipline(Disicipline dc, string IsUpdate, string IsDelete, string IsActive)
    {
        // sproc functionality
        SqlCommand command = new SqlCommand("proc_Insert_Disicipline");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_DisiciplineId = command.Parameters.Add("DisiciplineId", SqlDbType.BigInt);
        p_DisiciplineId.Direction = ParameterDirection.Input;
        p_DisiciplineId.Value = dc.DisiciplineId;

        SqlParameter p_DisiciplineTitel = command.Parameters.Add("DisiciplineTitel", SqlDbType.VarChar);
        p_DisiciplineTitel.Direction = ParameterDirection.Input;
        p_DisiciplineTitel.Value = dc.DisiciplineTitel;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = dc.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = dc.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = dc.InsertedDate;

        SqlParameter p_UpdatedBy = command.Parameters.Add("UpdatedBy", SqlDbType.VarChar);
        p_UpdatedBy.Direction = ParameterDirection.Input;
        p_UpdatedBy.Value = dc.UpdatedBy;

        SqlParameter p_UpdatedDate = command.Parameters.Add("UpdatedDate", SqlDbType.DateTime);
        p_UpdatedDate.Direction = ParameterDirection.Input;
        p_UpdatedDate.Value = dc.UpdatedDate; //If flag=0 then it will insert data,or if flag=1 then it will update data.

        SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = dc.LastUpdatedFrom;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;



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

    // Insert or Update  or Delete Data of Division table  
    public void InsertDivision(Division dv, string IsUpdate, string IsDelete, string IsActive)
    {
        // sproc functionality
        SqlCommand command = new SqlCommand("proc_Insert_Division");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = dv.DivisionID;

        SqlParameter p_DivisionName = command.Parameters.Add("DivisionName", SqlDbType.VarChar);
        p_DivisionName.Direction = ParameterDirection.Input;
        p_DivisionName.Value = dv.DivisionName;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = dv.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = dv.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = dv.InsertedDate;

        SqlParameter p_UpdatedBy = command.Parameters.Add("UpdatedBy", SqlDbType.VarChar);
        p_UpdatedBy.Direction = ParameterDirection.Input;
        p_UpdatedBy.Value = dv.UpdatedBy;

        SqlParameter p_UpdatedDate = command.Parameters.Add("UpdatedDate", SqlDbType.DateTime);
        p_UpdatedDate.Direction = ParameterDirection.Input;
        p_UpdatedDate.Value = dv.UpdatedDate; //If flag=0 then it will insert data,or if flag=1 then it will update data.

        SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = dv.LastUpdatedFrom;


        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;
        


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

    // Insert or Update  or Delete Data of Division table  
    public void InsertSector(Division dv, string IsUpdate, string IsDelete, string IsActive)
    {
        SqlCommand command = new SqlCommand("proc_Insert_SectorList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_SectorId = command.Parameters.Add("SectorId", SqlDbType.BigInt);
        p_SectorId.Direction = ParameterDirection.Input;
        p_SectorId.Value = dv.DivisionID;

        SqlParameter p_SectorName = command.Parameters.Add("SectorName", SqlDbType.VarChar);
        p_SectorName.Direction = ParameterDirection.Input;
        p_SectorName.Value = dv.DivisionName;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = dv.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = dv.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = dv.InsertedDate;

        SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = dv.LastUpdatedFrom;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;

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

    // Insert or Update  or Delete Data of Division table  
    public void InsertUnit(GridView grLocation, Desigation dsg, string IsUpdate, string IsDelete, string IsActive)
    {
        SqlCommand[] cmd = new SqlCommand[grLocation.Rows.Count + 2];

        cmd[0] = new SqlCommand("proc_Insert_UnitList");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_DesignationID = cmd[0].Parameters.Add("UnitId", SqlDbType.BigInt);
        p_DesignationID.Direction = ParameterDirection.Input;
        p_DesignationID.Value = dsg.DesgID;

        SqlParameter p_DesignationName = cmd[0].Parameters.Add("UnitName", SqlDbType.VarChar);
        p_DesignationName.Direction = ParameterDirection.Input;
        p_DesignationName.Value = dsg.DesgName;

        SqlParameter p_isDeleted = cmd[0].Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = dsg.IsDeleted;

        SqlParameter p_InsertedBy = cmd[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = dsg.InsertedBy;

        SqlParameter p_InsertedDate = cmd[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = dsg.InsertedDate;

        SqlParameter p_LastUpdatedFrom = cmd[0].Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = dsg.LastUpdatedFrom;

        SqlParameter p_IsUpdate = cmd[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = cmd[0].Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_IsActive = cmd[0].Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;

        cmd[1] = new SqlCommand("DELETE FROM UnitWiseDepartment WHERE UnitId = @UnitId");
        cmd[1].CommandType = CommandType.Text;

        SqlParameter p_DesignationID2 = cmd[1].Parameters.Add("UnitId", SqlDbType.BigInt);
        p_DesignationID2.Direction = ParameterDirection.Input;
        p_DesignationID2.Value = dsg.DesgID;

        int i = 2;
        foreach (GridViewRow gRow in grLocation.Rows)
        {
            CheckBox chkBox = (CheckBox)gRow.FindControl("chkBox");
            if (chkBox.Checked == true)
            {
                string query = "INSERT INTO UnitWiseDepartment(UnitId,DeptId,InsertedBy,InsertedDate) " +
                               "VALUES(@UnitId,@DeptId,@InsertedBy,@InsertedDate)";

                cmd[i] = new SqlCommand(query);
                cmd[i].CommandType = CommandType.Text;

                SqlParameter p_DesignationID3 = cmd[i].Parameters.Add("UnitId", SqlDbType.BigInt);
                p_DesignationID3.Direction = ParameterDirection.Input;
                p_DesignationID3.Value = dsg.DesgID;

                SqlParameter p_LocationId = cmd[i].Parameters.Add("DeptId", SqlDbType.BigInt);
                p_LocationId.Direction = ParameterDirection.Input;
                p_LocationId.Value = grLocation.DataKeys[gRow.RowIndex].Values[0].ToString();

                SqlParameter p_InsertedBy2 = cmd[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                p_InsertedBy2.Direction = ParameterDirection.Input;
                p_InsertedBy2.Value = dsg.InsertedBy;

                SqlParameter p_InsertedDate2 = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                p_InsertedDate2.Direction = ParameterDirection.Input;
                p_InsertedDate2.Value = dsg.InsertedDate;
                i++;
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

    public void InsertSalaryHeadBlock(string TransId, string EmpId, string SHeadId, string FromDate, string ToDate, string BlockAmt, string InsertedBy, string InsertedDate, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_SalaryHeadBlock");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_TransId = command.Parameters.Add("TransId", SqlDbType.BigInt);
        p_TransId.Direction = ParameterDirection.Input;
        p_TransId.Value = TransId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_SHeadId = command.Parameters.Add("SHeadId", SqlDbType.BigInt);
        p_SHeadId.Direction = ParameterDirection.Input;
        p_SHeadId.Value = SHeadId;

        SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.DateTime);
        p_FromDate.Direction = ParameterDirection.Input;
        p_FromDate.Value = Common.ReturnDate(FromDate);

        SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.DateTime);
        p_ToDate.Direction = ParameterDirection.Input;
        p_ToDate.Value = Common.ReturnDate(ToDate);

        SqlParameter p_BlockAmt = command.Parameters.Add("BlockAmt", SqlDbType.Decimal);
        p_BlockAmt.Direction = ParameterDirection.Input;
        p_BlockAmt.Value = BlockAmt;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = InsertedDate;

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

    public void InsertEmailNotification(string Notify, string Verify, string Review, string Approval, string Disburse, string InsertedBy, string InsertedDate)
    {
        SqlCommand command = new SqlCommand("proc_Insert_EmailNotification");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Notify = command.Parameters.Add("Notify", SqlDbType.Char);
        p_Notify.Direction = ParameterDirection.Input;
        p_Notify.Value = Notify;

        SqlParameter p_Verify = command.Parameters.Add("Verify", SqlDbType.Char);
        p_Verify.Direction = ParameterDirection.Input;
        p_Verify.Value = Verify;

        SqlParameter p_Review = command.Parameters.Add("Review", SqlDbType.Char);
        p_Review.Direction = ParameterDirection.Input;
        p_Review.Value = Review;

        SqlParameter p_Approval = command.Parameters.Add("Approval", SqlDbType.Char);
        p_Approval.Direction = ParameterDirection.Input;
        p_Approval.Value = Approval;

        SqlParameter p_Disburse = command.Parameters.Add("Disburse", SqlDbType.Char);
        p_Disburse.Direction = ParameterDirection.Input;
        p_Disburse.Value = Disburse;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedOn", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = InsertedDate;

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

    public DataTable GetEmailNotification()
    {
        if (objDC.ds.Tables["tblEmailNotification"] != null)
        {
            objDC.ds.Tables["tblEmailNotification"].Rows.Clear();
            objDC.ds.Tables["tblEmailNotification"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_Select_EmailNotification");
        command.CommandType = CommandType.StoredProcedure;

        objDC.CreateDSFromProc(command, "tblEmailNotification");
        return objDC.ds.Tables["tblEmailNotification"];
    }

    public void InsertJobTitle(Desigation dsg, string IsUpdate, string IsDelete, string IsActive)
    {
        SqlCommand command = new SqlCommand("proc_Insert_JobTitle");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_DesignationID = command.Parameters.Add("JobTitleId", SqlDbType.BigInt);
        p_DesignationID.Direction = ParameterDirection.Input;
        p_DesignationID.Value = dsg.DesgID;

        SqlParameter p_DesignationName = command.Parameters.Add("JobTitleName", SqlDbType.VarChar);
        p_DesignationName.Direction = ParameterDirection.Input;
        p_DesignationName.Value = dsg.DesgName;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = dsg.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = dsg.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = dsg.InsertedDate;

        SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = dsg.LastUpdatedFrom;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;

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

    public void InsertDesignation(Desigation  dsg, string IsUpdate, string IsDelete, string IsActive)
    {
        SqlCommand command = new SqlCommand("proc_Insert_Designation");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_DesignationID = command.Parameters.Add("DesigId", SqlDbType.BigInt);
        p_DesignationID.Direction = ParameterDirection.Input;
        p_DesignationID.Value = dsg.DesgID  ;

        SqlParameter p_DesignationName = command.Parameters.Add("DesigName", SqlDbType.VarChar);
        p_DesignationName.Direction = ParameterDirection.Input;
        p_DesignationName.Value = dsg.DesgName ;

        SqlParameter p_DesigShortName = command.Parameters.Add("DesigShortName", SqlDbType.VarChar);
        p_DesigShortName.Direction = ParameterDirection.Input;
        p_DesigShortName.Value = dsg.ShortName;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = dsg.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = dsg.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = dsg.InsertedDate;

        SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = dsg.LastUpdatedFrom;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;

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

    public void InsertDivisionList(GridView grLocation, Desigation dsg, string IsUpdate, string IsDelete, string IsActive)
    {
        SqlCommand[] cmd = new SqlCommand[grLocation.Rows.Count + 2];
        cmd[0] = new SqlCommand("proc_Insert_DivisionList");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_DesignationID = cmd[0].Parameters.Add("DivisionID", SqlDbType.BigInt);
        p_DesignationID.Direction = ParameterDirection.Input;
        p_DesignationID.Value = dsg.DesgID;

        SqlParameter p_DesignationName = cmd[0].Parameters.Add("DivisionName", SqlDbType.VarChar);
        p_DesignationName.Direction = ParameterDirection.Input;
        p_DesignationName.Value = dsg.DesgName;

        SqlParameter p_DivisionShortName = cmd[0].Parameters.Add("DivisionShortName", SqlDbType.VarChar);
        p_DivisionShortName.Direction = ParameterDirection.Input;
        p_DivisionShortName.Value = dsg.ShortName;

        SqlParameter p_isDeleted = cmd[0].Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = dsg.IsDeleted;

        SqlParameter p_InsertedBy = cmd[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = dsg.InsertedBy;

        SqlParameter p_InsertedDate = cmd[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = dsg.InsertedDate;

        SqlParameter p_LastUpdatedFrom = cmd[0].Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = dsg.LastUpdatedFrom;

        SqlParameter p_IsUpdate = cmd[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = cmd[0].Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_IsActive = cmd[0].Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;

        cmd[1] = new SqlCommand("DELETE FROM DivisionWiseDistrict WHERE PostingDivId = @DivisionId");
        cmd[1].CommandType = CommandType.Text;

        SqlParameter p_DesignationID2 = cmd[1].Parameters.Add("DivisionID", SqlDbType.BigInt);
        p_DesignationID2.Direction = ParameterDirection.Input;
        p_DesignationID2.Value = dsg.DesgID;

        int i = 2;
        foreach (GridViewRow gRow in grLocation.Rows)
        {
            CheckBox chkBox = (CheckBox)gRow.FindControl("chkBox");
            if (chkBox.Checked == true)
            {
                string query = "INSERT INTO DivisionWiseDistrict(PostingDivId,PostingDistId,InsertedBy,InsertedDate) " +
                               "VALUES(@DivisionId,@PostingDistId,@InsertedBy,@InsertedDate)";
                cmd[i] = new SqlCommand(query);
                cmd[i].CommandType = CommandType.Text;

                SqlParameter p_DesignationID3 = cmd[i].Parameters.Add("DivisionID", SqlDbType.BigInt);
                p_DesignationID3.Direction = ParameterDirection.Input;
                p_DesignationID3.Value = dsg.DesgID;

                SqlParameter p_LocationId = cmd[i].Parameters.Add("PostingDistId", SqlDbType.BigInt);
                p_LocationId.Direction = ParameterDirection.Input;
                p_LocationId.Value = grLocation.DataKeys[gRow.RowIndex].Values[0].ToString();

                SqlParameter p_InsertedBy2 = cmd[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                p_InsertedBy2.Direction = ParameterDirection.Input;
                p_InsertedBy2.Value = dsg.InsertedBy;

                SqlParameter p_InsertedDate2 = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                p_InsertedDate2.Direction = ParameterDirection.Input;
                p_InsertedDate2.Value = dsg.InsertedDate;
                i++;
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

    public void InsertDesignation(GridView grLocation, Desigation dsg, string IsUpdate, string IsDelete, string IsActive)
    {
        SqlCommand[] cmd = new SqlCommand[grLocation.Rows.Count + 2];
        cmd[0] = new SqlCommand("proc_Insert_Designation");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_DesignationID = cmd[0].Parameters.Add("DesigId", SqlDbType.BigInt);
        p_DesignationID.Direction = ParameterDirection.Input;
        p_DesignationID.Value = dsg.DesgID;

        SqlParameter p_DesignationName = cmd[0].Parameters.Add("DesigName", SqlDbType.VarChar);
        p_DesignationName.Direction = ParameterDirection.Input;
        p_DesignationName.Value = dsg.DesgName;

        SqlParameter p_DesigShortName = cmd[0].Parameters.Add("DesigShortName", SqlDbType.VarChar);
        p_DesigShortName.Direction = ParameterDirection.Input;
        p_DesigShortName.Value = dsg.ShortName;

        SqlParameter p_isDeleted = cmd[0].Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = dsg.IsDeleted;

        SqlParameter p_InsertedBy = cmd[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = dsg.InsertedBy;

        SqlParameter p_InsertedDate = cmd[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = dsg.InsertedDate;

        SqlParameter p_LastUpdatedFrom = cmd[0].Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = dsg.LastUpdatedFrom;

        SqlParameter p_IsUpdate = cmd[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = cmd[0].Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_IsActive = cmd[0].Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;

        cmd[1] = new SqlCommand("DELETE FROM DesigWiseJobTitle WHERE DesigId = @DesigId");
        cmd[1].CommandType = CommandType.Text;

        SqlParameter p_DesignationID2 = cmd[1].Parameters.Add("DesigId", SqlDbType.BigInt);
        p_DesignationID2.Direction = ParameterDirection.Input;
        p_DesignationID2.Value = dsg.DesgID;

        int i = 2;
        foreach (GridViewRow gRow in grLocation.Rows)
        {
            CheckBox chkBox = (CheckBox)gRow.FindControl("chkBox");
            if (chkBox.Checked == true)
            {
                string query = "INSERT INTO DesigWiseJobTitle(DesigId,JobTitleId,InsertedBy,InsertedDate) " +
                               "VALUES(@DesigId,@JobTitleId,@InsertedBy,@InsertedDate)";
                cmd[i] = new SqlCommand(query);
                cmd[i].CommandType = CommandType.Text;

                SqlParameter p_DesignationID3 = cmd[i].Parameters.Add("DesigId", SqlDbType.BigInt);
                p_DesignationID3.Direction = ParameterDirection.Input;
                p_DesignationID3.Value = dsg.DesgID;

                SqlParameter p_LocationId = cmd[i].Parameters.Add("JobTitleId", SqlDbType.BigInt);
                p_LocationId.Direction = ParameterDirection.Input;
                p_LocationId.Value = grLocation.DataKeys[gRow.RowIndex].Values[0].ToString();

                SqlParameter p_InsertedBy2 = cmd[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                p_InsertedBy2.Direction = ParameterDirection.Input;
                p_InsertedBy2.Value = dsg.InsertedBy;

                SqlParameter p_InsertedDate2 = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                p_InsertedDate2.Direction = ParameterDirection.Input;
                p_InsertedDate2.Value = dsg.InsertedDate;
                i++;
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

    public void InsertSector(GridView grLocation, Desigation dsg, string IsUpdate, string IsDelete, string IsActive)
    {
        SqlCommand[] cmd = new SqlCommand[grLocation.Rows.Count + 2];

        cmd[0] = new SqlCommand("proc_Insert_SectorList");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_DesignationID = cmd[0].Parameters.Add("SectorId", SqlDbType.BigInt);
        p_DesignationID.Direction = ParameterDirection.Input;
        p_DesignationID.Value = dsg.DesgID;

        SqlParameter p_DesignationName = cmd[0].Parameters.Add("SectorName", SqlDbType.VarChar);
        p_DesignationName.Direction = ParameterDirection.Input;
        p_DesignationName.Value = dsg.DesgName;

        SqlParameter p_ShortName = cmd[0].Parameters.Add("ShortName", SqlDbType.VarChar);
        p_ShortName.Direction = ParameterDirection.Input;
        p_ShortName.Value = dsg.ShortName;

        SqlParameter p_isDeleted = cmd[0].Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = dsg.IsDeleted;

        SqlParameter p_InsertedBy = cmd[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = dsg.InsertedBy;

        SqlParameter p_InsertedDate = cmd[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = dsg.InsertedDate;

        SqlParameter p_LastUpdatedFrom = cmd[0].Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = dsg.LastUpdatedFrom;

        SqlParameter p_IsUpdate = cmd[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = cmd[0].Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_IsActive = cmd[0].Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;

        cmd[1] = new SqlCommand("DELETE FROM SectorWiseDepartment WHERE SectorId = @SectorId");
        cmd[1].CommandType = CommandType.Text;

        SqlParameter p_DesignationID2 = cmd[1].Parameters.Add("SectorId", SqlDbType.BigInt);
        p_DesignationID2.Direction = ParameterDirection.Input;
        p_DesignationID2.Value = dsg.DesgID;

        int i = 2;
        foreach (GridViewRow gRow in grLocation.Rows)
        {
            CheckBox chkBox = (CheckBox)gRow.FindControl("chkBox");
            if (chkBox.Checked == true)
            {
                string query = "INSERT INTO SectorWiseDepartment(SectorId,DeptId,InsertedBy,InsertedDate) " +
                               "VALUES(@SectorId,@DeptId,@InsertedBy,@InsertedDate)";

                cmd[i] = new SqlCommand(query);
                cmd[i].CommandType = CommandType.Text;

                SqlParameter p_DesignationID3 = cmd[i].Parameters.Add("SectorId", SqlDbType.BigInt);
                p_DesignationID3.Direction = ParameterDirection.Input;
                p_DesignationID3.Value = dsg.DesgID;

                SqlParameter p_LocationId = cmd[i].Parameters.Add("DeptId", SqlDbType.BigInt);
                p_LocationId.Direction = ParameterDirection.Input;
                p_LocationId.Value = grLocation.DataKeys[gRow.RowIndex].Values[0].ToString();

                SqlParameter p_InsertedBy2 = cmd[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                p_InsertedBy2.Direction = ParameterDirection.Input;
                p_InsertedBy2.Value = dsg.InsertedBy;

                SqlParameter p_InsertedDate2 = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                p_InsertedDate2.Direction = ParameterDirection.Input;
                p_InsertedDate2.Value = dsg.InsertedDate;
                i++;
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

    public void InsertSubDesignation(Desigation dsg, string IsUpdate, string IsDelete, string IsActive)
    {
        SqlCommand command = new SqlCommand("proc_Insert_SubDesignation");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_DesignationID = command.Parameters.Add("SubDesigId", SqlDbType.BigInt);
        p_DesignationID.Direction = ParameterDirection.Input;
        p_DesignationID.Value = dsg.DesgID;

        SqlParameter p_DesignationName = command.Parameters.Add("SubDesigName", SqlDbType.VarChar);
        p_DesignationName.Direction = ParameterDirection.Input;
        p_DesignationName.Value = dsg.DesgName;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = dsg.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = dsg.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = dsg.InsertedDate;

        SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = dsg.LastUpdatedFrom;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;

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

    // Insert or Update  or Delete Data of Gradelist table
    public void InsertGrade(GridView grDesig, GradeEquiv gd, string IsUpdate, string IsDelete, string IsActive, string strMobileAllow,string strMinSal,string strMaxSal)
    {
        SqlCommand[] cmd = new SqlCommand[grDesig.Rows.Count + 2];
        cmd[0] = new SqlCommand("proc_Insert_Grade");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_GradeID = cmd[0].Parameters.Add("GradeID", SqlDbType.BigInt);
        p_GradeID.Direction = ParameterDirection.Input;
        p_GradeID.Value = gd.GradeID;

        SqlParameter p_GradeName = cmd[0].Parameters.Add("GradeName", SqlDbType.VarChar);
        p_GradeName.Direction = ParameterDirection.Input;
        p_GradeName.Value = gd.GradeName;

        SqlParameter p_isDeleted = cmd[0].Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = gd.IsDeleted;

        SqlParameter p_InsertedBy = cmd[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = gd.InsertedBy;

        SqlParameter p_InsertedDate = cmd[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = gd.InsertedDate;

        SqlParameter p_IsUpdate = cmd[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = cmd[0].Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;
        //IsActive
        SqlParameter p_IsActive = cmd[0].Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;

        SqlParameter p_BasicMin = cmd[0].Parameters.Add("BasicMin", DBNull.Value);
        p_BasicMin.Direction = ParameterDirection.Input;
        p_BasicMin.IsNullable = true;
        if (strMinSal != "")
            p_BasicMin.Value = strMinSal;

        SqlParameter p_BasicMax = cmd[0].Parameters.Add("BasicMax", DBNull.Value);
        p_BasicMax.Direction = ParameterDirection.Input;
        p_BasicMax.IsNullable = true;
        if (strMaxSal != "")
            p_BasicMax.Value = strMaxSal;

        SqlParameter p_IsOTEntitle = cmd[0].Parameters.Add("IsOTEntitle", SqlDbType.Char);
        p_IsOTEntitle.Direction = ParameterDirection.Input;
        p_IsOTEntitle.Value = gd.IsOTEntitle;

        cmd[1] = new SqlCommand("DELETE FROM GradeWsDesig WHERE GradeId = @GradeId");
        cmd[1].CommandType = CommandType.Text;

        SqlParameter p_DesignationID2 = cmd[1].Parameters.Add("GradeId", SqlDbType.BigInt);
        p_DesignationID2.Direction = ParameterDirection.Input;
        p_DesignationID2.Value = gd.GradeID;

        int i = 2;
        foreach (GridViewRow gRow in grDesig.Rows)
        {
            CheckBox chkBox = (CheckBox)gRow.FindControl("chkBox");
            if (chkBox.Checked == true)
            {
                string query = "INSERT INTO GradeWsDesig(GradeId,DesigId,InsertedBy,InsertedDate) " +
                               "VALUES(@GradeId,@DesigId,@InsertedBy,@InsertedDate)";
                cmd[i] = new SqlCommand(query);
                cmd[i].CommandType = CommandType.Text;

                SqlParameter p_DesignationID3 = cmd[i].Parameters.Add("GradeId", SqlDbType.BigInt);
                p_DesignationID3.Direction = ParameterDirection.Input;
                p_DesignationID3.Value = gd.GradeID;

                SqlParameter p_LocationId = cmd[i].Parameters.Add("DesigId", SqlDbType.BigInt);
                p_LocationId.Direction = ParameterDirection.Input;
                p_LocationId.Value = grDesig.DataKeys[gRow.RowIndex].Values[0].ToString();

                SqlParameter p_InsertedBy2 = cmd[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                p_InsertedBy2.Direction = ParameterDirection.Input;
                p_InsertedBy2.Value = gd.InsertedBy;

                SqlParameter p_InsertedDate2 = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                p_InsertedDate2.Direction = ParameterDirection.Input;
                p_InsertedDate2.Value = gd.InsertedDate;
                i++;
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

    // Insert or Update  or Delete Data of GradeLevelList table
    public void InsertGradeLevel(GradeEquiv gd, string IsUpdate, string IsDelete, string IsActive)
    {
        SqlCommand command = new SqlCommand("proc_Insert_GradeLevel");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_GradeID = command.Parameters.Add("GradeID", SqlDbType.BigInt);
        p_GradeID.Direction = ParameterDirection.Input;
        p_GradeID.Value = gd.GradeID;

        SqlParameter p_GradeName = command.Parameters.Add("GradeName", SqlDbType.VarChar);
        p_GradeName.Direction = ParameterDirection.Input;
        p_GradeName.Value = gd.GradeName;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = gd.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = gd.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = gd.InsertedDate;

        SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = gd.LastUpdatedFrom;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;

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

    public void InsertSalaryLocation(Desigation dsg, string IsUpdate, string IsDelete, string IsActive)
    {
        SqlCommand[] cmd = new SqlCommand[1];
        cmd[0] = new SqlCommand("proc_Insert_SalaryLocation");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_DesignationID = cmd[0].Parameters.Add("SalLocId", SqlDbType.BigInt);
        p_DesignationID.Direction = ParameterDirection.Input;
        p_DesignationID.Value = dsg.DesgID;

        SqlParameter p_DesignationName = cmd[0].Parameters.Add("SalLocName", SqlDbType.VarChar);
        p_DesignationName.Direction = ParameterDirection.Input;
        p_DesignationName.Value = dsg.DesgName;

        SqlParameter p_CostCenterCode = cmd[0].Parameters.Add("CostCenterCode", SqlDbType.VarChar);
        p_CostCenterCode.Direction = ParameterDirection.Input;
        p_CostCenterCode.Value = dsg.ShortName;

        SqlParameter p_isDeleted = cmd[0].Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = dsg.IsDeleted;

        SqlParameter p_InsertedBy = cmd[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = dsg.InsertedBy;

        SqlParameter p_InsertedDate = cmd[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = dsg.InsertedDate;

        SqlParameter p_LastUpdatedFrom = cmd[0].Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = dsg.LastUpdatedFrom;

        SqlParameter p_IsUpdate = cmd[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = cmd[0].Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_IsActive = cmd[0].Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;
      
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

    public void InsertSalarySubLocation(Desigation dsg, string IsUpdate, string IsDelete, string IsActive)
    {
        SqlCommand command = new SqlCommand("proc_Insert_SalarySubLocation");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_DesignationID = command.Parameters.Add("SalSubLocId", SqlDbType.BigInt);
        p_DesignationID.Direction = ParameterDirection.Input;
        p_DesignationID.Value = dsg.DesgID;

        SqlParameter p_DesignationName = command.Parameters.Add("SalSubLocName", SqlDbType.VarChar);
        p_DesignationName.Direction = ParameterDirection.Input;
        p_DesignationName.Value = dsg.DesgName;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = dsg.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = dsg.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = dsg.InsertedDate;

        SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = dsg.LastUpdatedFrom;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;

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

    public void InsertPositionByFunction(Desigation dsg, string IsUpdate, string IsDelete, string IsActive)
    {
        SqlCommand command = new SqlCommand("proc_Insert_PositionByFunction");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_DesignationID = command.Parameters.Add("PosFuncId", SqlDbType.BigInt);
        p_DesignationID.Direction = ParameterDirection.Input;
        p_DesignationID.Value = dsg.DesgID;

        SqlParameter p_DesignationName = command.Parameters.Add("PosFuncName", SqlDbType.VarChar);
        p_DesignationName.Direction = ParameterDirection.Input;
        p_DesignationName.Value = dsg.DesgName;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = dsg.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = dsg.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = dsg.InsertedDate;

        SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = dsg.LastUpdatedFrom;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;

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

    public void InsertReligionList(Desigation dsg, string IsUpdate, string IsDelete, string IsActive,string NoOfBasic, string percentage)
    {
        SqlCommand command = new SqlCommand("proc_Insert_ReligionList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_DesignationID = command.Parameters.Add("ReligionId", SqlDbType.BigInt);
        p_DesignationID.Direction = ParameterDirection.Input;
        p_DesignationID.Value = dsg.DesgID;

        SqlParameter p_DesignationName = command.Parameters.Add("ReligionName", SqlDbType.VarChar);
        p_DesignationName.Direction = ParameterDirection.Input;
        p_DesignationName.Value = dsg.DesgName;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = dsg.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = dsg.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = dsg.InsertedDate;

        SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = dsg.LastUpdatedFrom;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;

        SqlParameter p_NoOfBasic = command.Parameters.Add("NumberOfbasic", SqlDbType.BigInt);
        p_NoOfBasic.Direction = ParameterDirection.Input;
        p_NoOfBasic.Value = Convert.ToInt32(NoOfBasic);

        SqlParameter p_percentage = command.Parameters.Add("percentage", SqlDbType.BigInt);
        p_percentage.Direction = ParameterDirection.Input;
        p_percentage.Value =Convert.ToInt32(percentage);

        //string NoOfBasic, string percentage

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

    //InsertFastivalList

    public void InsertFastivalList(Desigation dsg, string IsUpdate, string IsDelete, string IsActive, string ReligionID)
    {
        SqlCommand command = new SqlCommand("proc_Insert_Festival");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_DesignationID = command.Parameters.Add("FestivalID", SqlDbType.BigInt);
        p_DesignationID.Direction = ParameterDirection.Input;
        p_DesignationID.Value = dsg.DesgID;

        SqlParameter p_DesignationName = command.Parameters.Add("Festival", SqlDbType.VarChar);
        p_DesignationName.Direction = ParameterDirection.Input;
        p_DesignationName.Value = dsg.DesgName;

        SqlParameter p_ReligionID = command.Parameters.Add("ReligionID", DBNull.Value  );
        p_ReligionID.Direction = ParameterDirection.Input;
        if (ReligionID != "-1")
            p_ReligionID.Value = Convert.ToInt32(ReligionID);

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = dsg.InsertedBy;
       
        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = dsg.InsertedDate;

        SqlParameter p_UpdatedBy = command.Parameters.Add("UpdatedBy", SqlDbType.VarChar);
        p_UpdatedBy.Direction = ParameterDirection.Input;
        p_UpdatedBy.Value = dsg.UpdatedBy;

        SqlParameter p_UpdatedDate = command.Parameters.Add("UpdatedDate", SqlDbType.DateTime);
        p_UpdatedDate.Direction = ParameterDirection.Input;
        p_UpdatedDate.Value = dsg.UpdatedDate;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;
        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;

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

    public void InsertBloodGroupList(Desigation dsg, string IsUpdate, string IsDelete, string IsActive)
    {
        SqlCommand command = new SqlCommand("proc_Insert_BloodGroupList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_DesignationID = command.Parameters.Add("BloodGroupId", SqlDbType.BigInt);
        p_DesignationID.Direction = ParameterDirection.Input;
        p_DesignationID.Value = dsg.DesgID;

        SqlParameter p_DesignationName = command.Parameters.Add("BloodGroupName", SqlDbType.VarChar);
        p_DesignationName.Direction = ParameterDirection.Input;
        p_DesignationName.Value = dsg.DesgName;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = dsg.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = dsg.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = dsg.InsertedDate;

        SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = dsg.LastUpdatedFrom;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;

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

    public void InsertRelationList(Desigation dsg, string IsUpdate, string IsDelete, string IsActive)
    {
        SqlCommand command = new SqlCommand("proc_Insert_RelationList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_DesignationID = command.Parameters.Add("RelationId", SqlDbType.BigInt);
        p_DesignationID.Direction = ParameterDirection.Input;
        p_DesignationID.Value = dsg.DesgID;

        SqlParameter p_DesignationName = command.Parameters.Add("RelationName", SqlDbType.VarChar);
        p_DesignationName.Direction = ParameterDirection.Input;
        p_DesignationName.Value = dsg.DesgName;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = dsg.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = dsg.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = dsg.InsertedDate;

        SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = dsg.LastUpdatedFrom;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;

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

    ////Insert Sub Grade
    //public void InsertSubGrade(SubGrade Subgd, string IsUpdate, string IsDelete, string strGradeId, string IsActive)
    //{
    //    // sproc functionality
    //    SqlCommand command = new SqlCommand("proc_Insert_SubGrade");
    //    command.CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_SubGradeID = command.Parameters.Add("SubGradeID", SqlDbType.BigInt);
    //    p_SubGradeID.Direction = ParameterDirection.Input;
    //    p_SubGradeID.Value = Subgd.SubGradeID;

    //    SqlParameter p_GradeName = command.Parameters.Add("SubGradeName", SqlDbType.VarChar);
    //    p_GradeName.Direction = ParameterDirection.Input;
    //    p_GradeName.Value = Subgd.SubGradeName;

    //    SqlParameter p_SubGradeDesc = command.Parameters.Add("SubGradeDesc", SqlDbType.VarChar);
    //    p_SubGradeDesc.Direction = ParameterDirection.Input;
    //    p_SubGradeDesc.Value = Subgd.SubGradeDesc;

    //    SqlParameter p_strGradeId = command.Parameters.Add("strGradeId", SqlDbType.VarChar);
    //    p_strGradeId.Direction = ParameterDirection.Input;
    //    p_strGradeId.Value = strGradeId;

    //    SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
    //    p_isDeleted.Direction = ParameterDirection.Input;
    //    p_isDeleted.Value = Subgd.IsDeleted;

    //    SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy.Direction = ParameterDirection.Input;
    //    p_InsertedBy.Value = Subgd.InsertedBy;

    //    SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.Value = Subgd.InsertedDate;

    //    SqlParameter p_UpdatedBy = command.Parameters.Add("UpdatedBy", SqlDbType.VarChar);
    //    p_UpdatedBy.Direction = ParameterDirection.Input;
    //    p_UpdatedBy.Value = Subgd.UpdatedBy;

    //    SqlParameter p_UpdatedDate = command.Parameters.Add("UpdatedDate", SqlDbType.DateTime);
    //    p_UpdatedDate.Direction = ParameterDirection.Input;
    //    p_UpdatedDate.Value = Subgd.UpdatedDate;

    //    SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
    //    p_LastUpdatedFrom.Direction = ParameterDirection.Input;
    //    p_LastUpdatedFrom.Value = Subgd.LastUpdatedFrom;


    //    SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
    //    p_IsUpdate.Direction = ParameterDirection.Input;
    //    p_IsUpdate.Value = IsUpdate;

    //    SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
    //    p_IsDelete.Direction = ParameterDirection.Input;
    //    p_IsDelete.Value = IsDelete;
    //    //IsActive
    //    SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
    //    p_IsActive.Direction = ParameterDirection.Input;
    //    p_IsActive.Value = IsActive;

    //    try
    //    {
    //        objDC.ExecuteQuery(command);
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

    //Insert Sub Grade
    public void InsertGradeWiseSubGrade(GridView grSubGrade, string GradeId, string InsertedBy, string InsertedDate)
    {
        SqlCommand[] command = new SqlCommand[grSubGrade.Rows.Count];

        for (int i = 0; i < grSubGrade.Rows.Count; i++)
        {
            command[i] = new SqlCommand("proc_Insert_GradeWiseSubGrade");
            command[i].CommandType = CommandType.StoredProcedure;

            SqlParameter p_SubGradeID = command[i].Parameters.Add("SubGradeID", SqlDbType.BigInt);
            p_SubGradeID.Direction = ParameterDirection.Input;
            p_SubGradeID.Value = grSubGrade.DataKeys[i].Values[0].ToString();

            SqlParameter p_GradeID = command[i].Parameters.Add("GradeID", SqlDbType.BigInt);
            p_GradeID.Direction = ParameterDirection.Input;
            p_GradeID.Value = GradeId;

            TextBox txtAmount = (TextBox)grSubGrade.Rows[i].FindControl("txtAmount");

            SqlParameter p_BasicSal = command[i].Parameters.Add("BasicSal", SqlDbType.BigInt);
            p_BasicSal.Direction = ParameterDirection.Input;
            p_BasicSal.Value = txtAmount.Text.Trim();

            SqlParameter p_InsertedBy = command[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
            p_InsertedBy.Direction = ParameterDirection.Input;
            p_InsertedBy.Value = InsertedBy;

            SqlParameter p_InsertedDate = command[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
            p_InsertedDate.Direction = ParameterDirection.Input;
            p_InsertedDate.Value = InsertedDate;
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

    ////Insert Staff Type
    //public void InsertStaffType(string StfTypeID, string StfTypeName, string StfTypeDesc, string IsActive, string InsertedBy, string @InsertedDate, string IsUpdate, string IsDelete)
    //{
    //    SqlCommand command = new SqlCommand("proc_Insert_StaffTypeList");
    //    command.CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_StfTypeID = command.Parameters.Add("StfTypeID", SqlDbType.BigInt);
    //    p_StfTypeID.Direction = ParameterDirection.Input;
    //    p_StfTypeID.Value = StfTypeID;

    //    SqlParameter p_StfTypeName = command.Parameters.Add("StfTypeName", SqlDbType.VarChar);
    //    p_StfTypeName.Direction = ParameterDirection.Input;
    //    p_StfTypeName.Value = StfTypeName;

    //    SqlParameter p_StfTypeDesc = command.Parameters.Add("StfTypeDesc", SqlDbType.VarChar);
    //    p_StfTypeDesc.Direction = ParameterDirection.Input;
    //    p_StfTypeDesc.Value = StfTypeDesc;

    //    SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
    //    p_IsActive.Direction = ParameterDirection.Input;
    //    p_IsActive.Value = IsActive;

    //    SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy.Direction = ParameterDirection.Input;
    //    p_InsertedBy.Value = InsertedBy;

    //    SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.Value = InsertedDate;

    //    SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
    //    p_IsUpdate.Direction = ParameterDirection.Input;
    //    p_IsUpdate.Value = IsUpdate;

    //    SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
    //    p_IsDelete.Direction = ParameterDirection.Input;
    //    p_IsDelete.Value = IsDelete;

    //    try
    //    {
    //        objDC.ExecuteQuery(command);
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

    ////Insert Staff Type
    //public void InsertStaffMode(string StfTypeID, string StfTypeName, string StfTypeDesc, string IsActive, string InsertedBy, string @InsertedDate, string IsUpdate, string IsDelete)
    //{
    //    SqlCommand command = new SqlCommand("proc_Insert_StaffModeList");
    //    command.CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_StfTypeID = command.Parameters.Add("StfModeID", SqlDbType.BigInt);
    //    p_StfTypeID.Direction = ParameterDirection.Input;
    //    p_StfTypeID.Value = StfTypeID;

    //    SqlParameter p_StfTypeName = command.Parameters.Add("StfModeName", SqlDbType.VarChar);
    //    p_StfTypeName.Direction = ParameterDirection.Input;
    //    p_StfTypeName.Value = StfTypeName;

    //    SqlParameter p_StfTypeDesc = command.Parameters.Add("StfModeDesc", SqlDbType.VarChar);
    //    p_StfTypeDesc.Direction = ParameterDirection.Input;
    //    p_StfTypeDesc.Value = StfTypeDesc;

    //    SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
    //    p_IsActive.Direction = ParameterDirection.Input;
    //    p_IsActive.Value = IsActive;

    //    SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy.Direction = ParameterDirection.Input;
    //    p_InsertedBy.Value = InsertedBy;

    //    SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.Value = InsertedDate;

    //    SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
    //    p_IsUpdate.Direction = ParameterDirection.Input;
    //    p_IsUpdate.Value = IsUpdate;

    //    SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
    //    p_IsDelete.Direction = ParameterDirection.Input;
    //    p_IsDelete.Value = IsDelete;

    //    try
    //    {
    //        objDC.ExecuteQuery(command);
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

    // Insert or Update  or Delete Data of Countrylist table
    public void InsertCountry(Country Cnt, string IsUpdate, string IsDelete)
    {
        // sproc functionality
        SqlCommand command = new SqlCommand("proc_Insert_Country");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_CountryID = command.Parameters.Add("CountryID", SqlDbType.BigInt);
        p_CountryID.Direction = ParameterDirection.Input;
        p_CountryID.Value = Cnt.CountryID;

        SqlParameter p_CountryName = command.Parameters.Add("CountryName", SqlDbType.VarChar);
        p_CountryName.Direction = ParameterDirection.Input;
        p_CountryName.Value = Cnt.CountryName;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = Cnt.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = Cnt.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = Cnt.InsertedDate;

        SqlParameter p_UpdatedBy = command.Parameters.Add("UpdatedBy", SqlDbType.VarChar);
        p_UpdatedBy.Direction = ParameterDirection.Input;
        p_UpdatedBy.Value = Cnt.UpdatedBy;

        SqlParameter p_UpdatedDate = command.Parameters.Add("UpdatedDate", SqlDbType.DateTime);
        p_UpdatedDate.Direction = ParameterDirection.Input;
        p_UpdatedDate.Value = Cnt.UpdatedDate;

        SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = Cnt.LastUpdatedFrom;


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
    // Insert or Update  or Delete Data of Districtlist table
    public void InsertDistrict(Desigation Dst, string IsUpdate, string IsDelete)
    {
        // sproc functionality
        SqlCommand command = new SqlCommand("proc_Insert_District");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_DistrictID = command.Parameters.Add("DistrictID", SqlDbType.BigInt);
        p_DistrictID.Direction = ParameterDirection.Input;
        p_DistrictID.Value = Dst.DesgID;

        SqlParameter p_DistrictName = command.Parameters.Add("DistrictName", SqlDbType.VarChar);
        p_DistrictName.Direction = ParameterDirection.Input;
        p_DistrictName.Value = Dst.DesgName;

        SqlParameter p_ShortName = command.Parameters.Add("ShortName", SqlDbType.VarChar);
        p_ShortName.Direction = ParameterDirection.Input;
        p_ShortName.Value = Dst.ShortName;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = Dst.IsActive;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = Dst.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = Dst.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = Dst.InsertedDate;

        SqlParameter p_UpdatedBy = command.Parameters.Add("UpdatedBy", SqlDbType.VarChar);
        p_UpdatedBy.Direction = ParameterDirection.Input;
        p_UpdatedBy.Value = Dst.UpdatedBy;

        SqlParameter p_UpdatedDate = command.Parameters.Add("UpdatedDate", SqlDbType.DateTime);
        p_UpdatedDate.Direction = ParameterDirection.Input;
        p_UpdatedDate.Value = Dst.UpdatedDate;

        SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = Dst.LastUpdatedFrom;

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

    //
    // Insert or Update  or Delete Data of ReasonList table
    public void InsertReason(Reason Rsn, string IsUpdate, string IsDelete)
    {
        // sproc functionality
        SqlCommand command = new SqlCommand("proc_Insert_Reason");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_ReasonId = command.Parameters.Add("ReasonId", SqlDbType.BigInt);
        p_ReasonId.Direction = ParameterDirection.Input;
        p_ReasonId.Value = Rsn.ReasonId;

        SqlParameter p_ReasonName = command.Parameters.Add("ReasonName", SqlDbType.VarChar);
        p_ReasonName.Direction = ParameterDirection.Input;
        p_ReasonName.Value = Rsn.ReasonName;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = Rsn.IsActive;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = Rsn.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = Rsn.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = Rsn.InsertedDate;

        SqlParameter p_UpdatedBy = command.Parameters.Add("UpdatedBy", SqlDbType.VarChar);
        p_UpdatedBy.Direction = ParameterDirection.Input;
        p_UpdatedBy.Value = Rsn.UpdatedBy;

        SqlParameter p_UpdatedOn = command.Parameters.Add("UpdatedOn", SqlDbType.DateTime);
        p_UpdatedOn.Direction = ParameterDirection.Input;
        p_UpdatedOn.Value = Rsn.UpdatedOn;

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

    // Insert or Update  or Delete Data of Districtlist table
    public void InsertHomeDistrict(Desigation Dst, string IsUpdate, string IsDelete)
    {
        // sproc functionality
        SqlCommand command = new SqlCommand("proc_Insert_HomeDistrict");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_DistrictID = command.Parameters.Add("DistrictID", SqlDbType.BigInt);
        p_DistrictID.Direction = ParameterDirection.Input;
        p_DistrictID.Value = Dst.DesgID;

        SqlParameter p_DistrictName = command.Parameters.Add("DistrictName", SqlDbType.VarChar);
        p_DistrictName.Direction = ParameterDirection.Input;
        p_DistrictName.Value = Dst.DesgName;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = Dst.IsActive;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = Dst.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = Dst.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = Dst.InsertedDate;

        SqlParameter p_UpdatedBy = command.Parameters.Add("UpdatedBy", SqlDbType.VarChar);
        p_UpdatedBy.Direction = ParameterDirection.Input;
        p_UpdatedBy.Value = Dst.UpdatedBy;

        SqlParameter p_UpdatedDate = command.Parameters.Add("UpdatedDate", SqlDbType.DateTime);
        p_UpdatedDate.Direction = ParameterDirection.Input;
        p_UpdatedDate.Value = Dst.UpdatedDate;

        SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = Dst.LastUpdatedFrom;

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

    public void InsertProjectOffice(Desigation Dst, string IsUpdate, string IsDelete)
    {
        // sproc functionality
        SqlCommand command = new SqlCommand("proc_Insert_ProjectOffice");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_DistrictID = command.Parameters.Add("ProjOfficeId", SqlDbType.BigInt);
        p_DistrictID.Direction = ParameterDirection.Input;
        p_DistrictID.Value = Dst.DesgID;

        SqlParameter p_DistrictName = command.Parameters.Add("ProjOfficeName", SqlDbType.VarChar);
        p_DistrictName.Direction = ParameterDirection.Input;
        p_DistrictName.Value = Dst.DesgName;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = Dst.IsActive;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = Dst.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = Dst.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = Dst.InsertedDate;

        SqlParameter p_UpdatedBy = command.Parameters.Add("UpdatedBy", SqlDbType.VarChar);
        p_UpdatedBy.Direction = ParameterDirection.Input;
        p_UpdatedBy.Value = Dst.UpdatedBy;

        SqlParameter p_UpdatedDate = command.Parameters.Add("UpdatedDate", SqlDbType.DateTime);
        p_UpdatedDate.Direction = ParameterDirection.Input;
        p_UpdatedDate.Value = Dst.UpdatedDate;

        SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = Dst.LastUpdatedFrom;

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

    // Insert or Update  or Delete Data of Department table
    public void InsertDepartment(Department Dpt, string IsUpdate, string IsDelete, string StrLocationID, string IsActive,
        string strDeptCode, string strValidFrom, string strValidTo)
    {
        SqlCommand command = new SqlCommand("proc_Insert_Department");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_DeptId = command.Parameters.Add("DeptId", SqlDbType.BigInt);
        p_DeptId.Direction = ParameterDirection.Input;
        p_DeptId.Value = Dpt.DeptId;

        SqlParameter p_DeptName = command.Parameters.Add("DeptName", SqlDbType.VarChar);
        p_DeptName.Direction = ParameterDirection.Input;
        p_DeptName.Value = Dpt.DeptName;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = Dpt.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = Dpt.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = Dpt.InsertedDate;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_DeptCode = command.Parameters.Add("DeptCode", SqlDbType.VarChar);
        p_DeptCode.Direction = ParameterDirection.Input;
        p_DeptCode.Value = strDeptCode;

        SqlParameter p_ValidFrom = command.Parameters.Add("ValidFrom", DBNull.Value);
        p_ValidFrom.Direction = ParameterDirection.Input;
        p_ValidFrom.IsNullable =true ;
        if (strValidFrom!="")
        p_ValidFrom.Value = strValidFrom;

        SqlParameter p_ValidTo = command.Parameters.Add("ValidTo", DBNull.Value);
        p_ValidTo.Direction = ParameterDirection.Input;
        p_ValidTo.IsNullable = true;
        if (strValidTo != "")
        p_ValidTo.Value = strValidTo;

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

    // Insert or Update  or Delete Data of Department table
    public void InsertSpecialSkill(Department Dpt, string IsUpdate, string IsDelete, string StrLocationID, string IsActive)
    {
        SqlCommand command = new SqlCommand("proc_Insert_SpecialSkillList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_DeptId = command.Parameters.Add("SpecSkillId", SqlDbType.BigInt);
        p_DeptId.Direction = ParameterDirection.Input;
        p_DeptId.Value = Dpt.DeptId;

        SqlParameter p_DeptName = command.Parameters.Add("SpecSkillName", SqlDbType.VarChar);
        p_DeptName.Direction = ParameterDirection.Input;
        p_DeptName.Value = Dpt.DeptName;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = Dpt.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = Dpt.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = Dpt.InsertedDate;

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

    public void InsertSection(Section sec, string IsUpdate, string IsDelete, string StrLocationID, string IsActive)
    {
        SqlCommand command = new SqlCommand("proc_Insert_Section");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_SectionId = command.Parameters.Add("SectionId", SqlDbType.BigInt);
        p_SectionId.Direction = ParameterDirection.Input;
        p_SectionId.Value = sec.SectionID;

        SqlParameter p_SecName = command.Parameters.Add("SecName", SqlDbType.VarChar);
        p_SecName.Direction = ParameterDirection.Input;
        p_SecName.Value = sec.SecName;

        SqlParameter p_SecDesc = command.Parameters.Add("SecDesc", SqlDbType.VarChar);
        p_SecDesc.Direction = ParameterDirection.Input;
        p_SecDesc.Value = sec.SecDesc;


        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = sec.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = sec.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = sec.InsertedDate;

        SqlParameter p_UpdatedBy = command.Parameters.Add("UpdatedBy", SqlDbType.VarChar);
        p_UpdatedBy.Direction = ParameterDirection.Input;
        p_UpdatedBy.Value = sec.UpdatedBy;

        SqlParameter p_UpdatedDate = command.Parameters.Add("UpdatedDate", SqlDbType.DateTime);
        p_UpdatedDate.Direction = ParameterDirection.Input;
        p_UpdatedDate.Value = sec.UpdatedDate;

        SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = sec.LastUpdatedFrom;


        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_StrDeptID = command.Parameters.Add("StrDeptID", SqlDbType.VarChar);
        p_StrDeptID.Direction = ParameterDirection.Input;
        p_StrDeptID.Value = sec.StrDeptID;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;


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

    public void InsertLeaveType(LeaveType Lvt, string IsUpdate, string IsDelete, string IsActive)
    {
        SqlCommand command = new SqlCommand("proc_Insert_LeaveType");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_LTypeID = command.Parameters.Add("LTypeID", SqlDbType.Decimal);
        p_LTypeID.Direction = ParameterDirection.Input;
        p_LTypeID.Value = Lvt.LTypeID;

        SqlParameter p_LTypeTitle = command.Parameters.Add("LTypeTitle", SqlDbType.VarChar);
        p_LTypeTitle.Direction = ParameterDirection.Input;
        p_LTypeTitle.Value = Lvt.LTypeTitle;

        SqlParameter p_LeaveDesc = command.Parameters.Add("LeaveDesc", SqlDbType.VarChar);
        p_LeaveDesc.Direction = ParameterDirection.Input;
        p_LeaveDesc.Value = Lvt.LeaveDesc;
        
         SqlParameter p_LMunit = command.Parameters.Add("LMunit", SqlDbType.Char);
        p_LMunit.Direction = ParameterDirection.Input;
        p_LMunit.Value = Lvt.LMunit;
        
         SqlParameter p_LCalcType = command.Parameters.Add("LCalcType", SqlDbType.Char);
        p_LCalcType.Direction = ParameterDirection.Input;
        p_LCalcType.Value = Lvt.LCalcType;

        SqlParameter p_CalcInterval = command.Parameters.Add("CalcInterval", SqlDbType.Decimal);
        p_CalcInterval.Direction = ParameterDirection.Input;
        p_CalcInterval.Value = Lvt.CalcInterval;

        SqlParameter p_CalcBase = command.Parameters.Add("CalcBase", SqlDbType.Decimal);
        p_CalcBase.Direction = ParameterDirection.Input;
        p_CalcBase.Value = Lvt.CalcBase;

        SqlParameter p_LNature = command.Parameters.Add("LNature", SqlDbType.Char);
        p_LNature.Direction = ParameterDirection.Input;
        p_LNature.Value = Lvt.LNature;

        SqlParameter p_LeaveTTL = command.Parameters.Add("LeaveTTL", SqlDbType.Char);
        p_LeaveTTL.Direction = ParameterDirection.Input;
        p_LeaveTTL.Value = Lvt.LNature;




        SqlParameter p_MaxCarryCashDay = command.Parameters.Add("MaxCarryCashDay", SqlDbType.Decimal);
        p_MaxCarryCashDay.Direction = ParameterDirection.Input;
        p_MaxCarryCashDay.Value = Lvt.MaxCarryCashDay;

        SqlParameter p_Eligibility = command.Parameters.Add("Eligibility", SqlDbType.Decimal);
        p_Eligibility.Direction = ParameterDirection.Input;
        p_Eligibility.Value = Lvt.Eligibility;

        SqlParameter p_NextLevInterval = command.Parameters.Add("NextLevInterval", SqlDbType.Decimal);
        p_NextLevInterval.Direction = ParameterDirection.Input;
        p_NextLevInterval.Value = Lvt.NextLevInterval;

        SqlParameter p_TotalMatLev = command.Parameters.Add("TotalMatLev", SqlDbType.Decimal);
        p_TotalMatLev.Direction = ParameterDirection.Input;
        p_TotalMatLev.Value = Lvt.TotalMatLev;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = Lvt.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = Lvt.InsertedDate;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_IsDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_IsDeleted.Direction = ParameterDirection.Input;
        p_IsDeleted.Value = Lvt.IsDeleted;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = IsActive;


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


    //public void InsertEvent(EventType dc, string IsUpdate, string IsDelete, string IsActive)
    //{
    //    // sproc functionality
    //    SqlCommand command = new SqlCommand("proc_Insert_EventType");
    //    command.CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_EventId = command.Parameters.Add("EventId", SqlDbType.BigInt);
    //    p_EventId.Direction = ParameterDirection.Input;
    //    p_EventId.Value = dc.EventId;

    //    SqlParameter p_EventName = command.Parameters.Add("EventName", SqlDbType.VarChar);
    //    p_EventName.Direction = ParameterDirection.Input;
    //    p_EventName.Value = dc.EventName;

    //    SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
    //    p_isDeleted.Direction = ParameterDirection.Input;
    //    p_isDeleted.Value = dc.IsDeleted;

    //    SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy.Direction = ParameterDirection.Input;
    //    p_InsertedBy.Value = dc.InsertedBy;

    //    SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.Value = dc.InsertedDate;

    //    SqlParameter p_UpdatedBy = command.Parameters.Add("UpdatedBy", SqlDbType.VarChar);
    //    p_UpdatedBy.Direction = ParameterDirection.Input;
    //    p_UpdatedBy.Value = dc.UpdatedBy;

    //    SqlParameter p_UpdatedDate = command.Parameters.Add("UpdatedDate", SqlDbType.DateTime);
    //    p_UpdatedDate.Direction = ParameterDirection.Input;
    //    p_UpdatedDate.Value = dc.UpdatedDate; //If flag=0 then it will insert data,or if flag=1 then it will update data.

    //    SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
    //    p_LastUpdatedFrom.Direction = ParameterDirection.Input;
    //    p_LastUpdatedFrom.Value = dc.LastUpdatedFrom;


    //    SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
    //    p_IsUpdate.Direction = ParameterDirection.Input;
    //    p_IsUpdate.Value = IsUpdate;

    //    SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
    //    p_IsDelete.Direction = ParameterDirection.Input;
    //    p_IsDelete.Value = IsDelete;

    //    SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
    //    p_IsActive.Direction = ParameterDirection.Input;
    //    p_IsActive.Value = IsActive;



    //    try
    //    {
    //        objDC.ExecuteQuery(command);
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


    //public void InsertAward(Award dc, string IsUpdate, string IsDelete, string IsActive)
    //{
    //    // sproc functionality
    //    SqlCommand command = new SqlCommand("proc_Insert_Award");
    //    command.CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_AwardId = command.Parameters.Add("AwardId", SqlDbType.BigInt);
    //    p_AwardId.Direction = ParameterDirection.Input;
    //    p_AwardId.Value = dc.AwardId;

    //    SqlParameter p_Award = command.Parameters.Add("Award", SqlDbType.VarChar);
    //    p_Award.Direction = ParameterDirection.Input;
    //    p_Award.Value = dc.Awards;

    //    SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
    //    p_isDeleted.Direction = ParameterDirection.Input;
    //    p_isDeleted.Value = dc.IsDeleted;

    //    SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy.Direction = ParameterDirection.Input;
    //    p_InsertedBy.Value = dc.InsertedBy;

    //    SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.Value = dc.InsertedDate;

    //    SqlParameter p_UpdatedBy = command.Parameters.Add("UpdatedBy", SqlDbType.VarChar);
    //    p_UpdatedBy.Direction = ParameterDirection.Input;
    //    p_UpdatedBy.Value = dc.UpdatedBy;

    //    SqlParameter p_UpdatedDate = command.Parameters.Add("UpdatedDate", SqlDbType.DateTime);
    //    p_UpdatedDate.Direction = ParameterDirection.Input;
    //    p_UpdatedDate.Value = dc.UpdatedDate; //If flag=0 then it will insert data,or if flag=1 then it will update data.

    //    SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
    //    p_LastUpdatedFrom.Direction = ParameterDirection.Input;
    //    p_LastUpdatedFrom.Value = dc.LastUpdatedFrom;


    //    SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
    //    p_IsUpdate.Direction = ParameterDirection.Input;
    //    p_IsUpdate.Value = IsUpdate;

    //    SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
    //    p_IsDelete.Direction = ParameterDirection.Input;
    //    p_IsDelete.Value = IsDelete;

    //    SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
    //    p_IsActive.Direction = ParameterDirection.Input;
    //    p_IsActive.Value = IsActive;



    //    try
    //    {
    //        objDC.ExecuteQuery(command);
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
    //// INSERT UPDATE GAD
    //public void InsertGAD(string strCode,string strTitle, string IsActive, string strIsUpdate, string strInsBy,string strInsDate,string strFrom,string strTo)
    //{
    //    // sproc functionality
    //    SqlCommand command = new SqlCommand("proc_Insert_GADLIST");
    //    command.CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_GADCODE = command.Parameters.Add("GADCODE", SqlDbType.Char);
    //    p_GADCODE.Direction = ParameterDirection.Input;
    //    p_GADCODE.Value = strCode;

    //    SqlParameter p_GADTITLE = command.Parameters.Add("GADTITLE", SqlDbType.VarChar);
    //    p_GADTITLE.Direction = ParameterDirection.Input;
    //    p_GADTITLE.Value = strTitle;

    //    SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy.Direction = ParameterDirection.Input;
    //    p_InsertedBy.Value = strInsBy;

    //    SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.Value = strInsDate;

    //    SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
    //    p_IsUpdate.Direction = ParameterDirection.Input;
    //    p_IsUpdate.Value = strIsUpdate;

    //    SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
    //    p_IsActive.Direction = ParameterDirection.Input;
    //    p_IsActive.Value = IsActive;

    //    SqlParameter p_VALIDFROM = command.Parameters.Add("VALIDFROM", DBNull.Value);
    //    p_VALIDFROM.Direction = ParameterDirection.Input;
    //    p_VALIDFROM.IsNullable = true;
    //    if (strFrom != "")
    //        p_VALIDFROM.Value = Common.ReturnDate(strFrom);

    //    SqlParameter p_VALIDTO = command.Parameters.Add("VALIDTO", DBNull.Value);
    //    p_VALIDTO.Direction = ParameterDirection.Input;
    //    p_VALIDTO.IsNullable = true;
    //    if (strTo != "")
    //        p_VALIDTO.Value = Common.ReturnDate(strTo);



    //    try
    //    {
    //        objDC.ExecuteQuery(command);
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

    //public void DeleteGAD(string strCode)
    //{
    //    // sproc functionality
    //    SqlCommand command = new SqlCommand("proc_Delete_GADLIST");
    //    command.CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_GADCODE = command.Parameters.Add("GADCODE", SqlDbType.Char);
    //    p_GADCODE.Direction = ParameterDirection.Input;
    //    p_GADCODE.Value = strCode;

    //    try
    //    {
    //        objDC.ExecuteQuery(command);
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

    //// INSERT UPDATE SPOT BONUS AWARD
    //public void InsertSBAward(SBAward dc, string IsUpdate, string IsDelete, string IsActive)
    //{
    //    // sproc functionality
    //    SqlCommand command = new SqlCommand("proc_Insert_SBAward");
    //    command.CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_AwardId = command.Parameters.Add("SBAwardId", SqlDbType.BigInt);
    //    p_AwardId.Direction = ParameterDirection.Input;
    //    p_AwardId.Value = dc.SBAwardId;

    //    SqlParameter p_Award = command.Parameters.Add("SBAward", SqlDbType.VarChar);
    //    p_Award.Direction = ParameterDirection.Input;
    //    p_Award.Value = dc.SBAwards;

    //    SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
    //    p_isDeleted.Direction = ParameterDirection.Input;
    //    p_isDeleted.Value = dc.IsDeleted;

    //    SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy.Direction = ParameterDirection.Input;
    //    p_InsertedBy.Value = dc.InsertedBy;

    //    SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.Value = dc.InsertedDate;

    //    SqlParameter p_UpdatedBy = command.Parameters.Add("UpdatedBy", SqlDbType.VarChar);
    //    p_UpdatedBy.Direction = ParameterDirection.Input;
    //    p_UpdatedBy.Value = dc.UpdatedBy;

    //    SqlParameter p_UpdatedDate = command.Parameters.Add("UpdatedDate", SqlDbType.DateTime);
    //    p_UpdatedDate.Direction = ParameterDirection.Input;
    //    p_UpdatedDate.Value = dc.UpdatedDate; //If flag=0 then it will insert data,or if flag=1 then it will update data.

    //    SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
    //    p_LastUpdatedFrom.Direction = ParameterDirection.Input;
    //    p_LastUpdatedFrom.Value = dc.LastUpdatedFrom;


    //    SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
    //    p_IsUpdate.Direction = ParameterDirection.Input;
    //    p_IsUpdate.Value = IsUpdate;

    //    SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
    //    p_IsDelete.Direction = ParameterDirection.Input;
    //    p_IsDelete.Value = IsDelete;

    //    SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
    //    p_IsActive.Direction = ParameterDirection.Input;
    //    p_IsActive.Value = IsActive;



    //    try
    //    {
    //        objDC.ExecuteQuery(command);
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


    
    public void InsertEmpType(clsEmpType ClEmpT, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_EmpTypeList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EmpTypeID.Direction = ParameterDirection.Input;
        p_EmpTypeID.Value = ClEmpT.EmpTypeID;

        SqlParameter p_TypeName = command.Parameters.Add("TypeName", SqlDbType.VarChar);
        p_TypeName.Direction = ParameterDirection.Input;
        p_TypeName.Value = ClEmpT.TypeName;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = ClEmpT.IsActive;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = ClEmpT.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = ClEmpT.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = ClEmpT.InsertedDate;

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

    public void InsertEmpNature(clsCommonSetup ClsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_EmpNatureList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpTypeID = command.Parameters.Add("EmpNatureID", SqlDbType.BigInt);
        p_EmpTypeID.Direction = ParameterDirection.Input;
        p_EmpTypeID.Value = ClsCommon.ID;

        SqlParameter p_TypeName = command.Parameters.Add("NatureName", SqlDbType.VarChar);
        p_TypeName.Direction = ParameterDirection.Input;
        p_TypeName.Value = ClsCommon.Name;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = ClsCommon.IsActive;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = ClsCommon.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = ClsCommon.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = ClsCommon.InsertedDate;

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
    public void InsertEmpGroup(clsEmpType ClEmpT, GridView grView, string IsUpdate, string IsDelete)
    {
        SqlCommand[] cmd = new SqlCommand[grView.Rows.Count + 2];
        cmd[0] = new SqlCommand("proc_Insert_EmpGroupList");
        cmd[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpTypeID = cmd[0].Parameters.Add("EmpGrpId", SqlDbType.BigInt);
        p_EmpTypeID.Direction = ParameterDirection.Input;
        p_EmpTypeID.Value = ClEmpT.EmpTypeID;

        SqlParameter p_TypeName = cmd[0].Parameters.Add("GrpName", SqlDbType.VarChar);
        p_TypeName.Direction = ParameterDirection.Input;
        p_TypeName.Value = ClEmpT.TypeName;

        SqlParameter p_TypeDesc = cmd[0].Parameters.Add("GrpDesc", SqlDbType.VarChar);
        p_TypeDesc.Direction = ParameterDirection.Input;
        p_TypeDesc.Value = ClEmpT.TypeDesc;

        SqlParameter p_IsActive = cmd[0].Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = ClEmpT.IsActive;

        SqlParameter p_isDeleted = cmd[0].Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = ClEmpT.IsDeleted;

        SqlParameter p_InsertedBy = cmd[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = ClEmpT.InsertedBy;

        SqlParameter p_InsertedDate = cmd[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = ClEmpT.InsertedDate;

        SqlParameter p_IsUpdate = cmd[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = cmd[0].Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        //Reset Emp Group Id
        cmd[1] = new SqlCommand("proc_Update_EmpTypeList");
        cmd[1].CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpTypeID2 = cmd[1].Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EmpTypeID2.Direction = ParameterDirection.Input;
        p_EmpTypeID2.Value = "0";

        SqlParameter p_EmpGrpId2 = cmd[1].Parameters.Add("EmpGrpId", SqlDbType.BigInt);
        p_EmpGrpId2.Direction = ParameterDirection.Input;
        p_EmpGrpId2.Value = ClEmpT.EmpTypeID;

        int i = 2;
        foreach (GridViewRow gRow in grView.Rows)
        {
            CheckBox chkSelect = (CheckBox)gRow.FindControl("chkSelect");
            if (chkSelect.Checked == true)
            {
                cmd[i] = new SqlCommand("proc_Update_EmpTypeList");
                cmd[i].CommandType = CommandType.StoredProcedure;

                SqlParameter p_EmpTypeID3 = cmd[i].Parameters.Add("EmpTypeID", SqlDbType.BigInt);
                p_EmpTypeID3.Direction = ParameterDirection.Input;
                p_EmpTypeID3.Value = grView.DataKeys[gRow.RowIndex].Values[0].ToString();

                SqlParameter p_EmpGrpId3 = cmd[i].Parameters.Add("EmpGrpId", SqlDbType.BigInt);
                p_EmpGrpId3.Direction = ParameterDirection.Input;
                p_EmpGrpId3.Value = ClEmpT.EmpTypeID;
                i++;
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

    public void InsertProject(Project ClPro, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_ProjectList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_ProjectId = command.Parameters.Add("ProjectId", SqlDbType.BigInt);
        p_ProjectId.Direction = ParameterDirection.Input;
        p_ProjectId.Value = ClPro.ProjectId;

        SqlParameter p_ProjectName = command.Parameters.Add("ProjectName", SqlDbType.VarChar);
        p_ProjectName.Direction = ParameterDirection.Input;
        p_ProjectName.Value = ClPro.ProjectName;

        SqlParameter p_TypeName = command.Parameters.Add("PDesc", SqlDbType.VarChar);
        p_TypeName.Direction = ParameterDirection.Input;
        p_TypeName.Value = ClPro.PDesc;

        SqlParameter p_StartDate = command.Parameters.Add("StartDate", DBNull.Value);
        p_StartDate.Direction = ParameterDirection.Input;
        p_StartDate.IsNullable = true;
        if (ClPro.StartDate != "")
            p_StartDate.Value = ClPro.StartDate;

        SqlParameter p_EndDate = command.Parameters.Add("EndDate", DBNull.Value);
        p_EndDate.Direction = ParameterDirection.Input;
        p_EndDate.IsNullable = true;
        if (ClPro.EndDate != "")
            p_EndDate.Value = ClPro.EndDate;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = ClPro.IsActive;

        SqlParameter p_IsDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_IsDeleted.Direction = ParameterDirection.Input;
        p_IsDeleted.Value = ClPro.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = ClPro.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = ClPro.InsertedDate;

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

    //// Insert or Update  or Delete Data of Level of Study table  
    //public void InsertLevelofStudy(clsLevelofStudy objLevel, string IsUpdate, string IsDelete, string IsActive)
    //{
    //    SqlCommand command = new SqlCommand("proc_Insert_LevelofStudy");
    //    command.CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_LevelId = command.Parameters.Add("LevelId", SqlDbType.BigInt);
    //    p_LevelId.Direction = ParameterDirection.Input;
    //    p_LevelId.Value = objLevel.LevelId;

    //    SqlParameter p_LevelName = command.Parameters.Add("LevelName", SqlDbType.VarChar);
    //    p_LevelName.Direction = ParameterDirection.Input;
    //    p_LevelName.Value = objLevel.LevelName;

    //    SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
    //    p_isDeleted.Direction = ParameterDirection.Input;
    //    p_isDeleted.Value = objLevel.IsDeleted;

    //    SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy.Direction = ParameterDirection.Input;
    //    p_InsertedBy.Value = objLevel.InsertedBy;

    //    SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.Value = objLevel.InsertedDate;

    //    SqlParameter p_UpdatedBy = command.Parameters.Add("UpdatedBy", SqlDbType.VarChar);
    //    p_UpdatedBy.Direction = ParameterDirection.Input;
    //    p_UpdatedBy.Value = objLevel.UpdatedBy;

    //    SqlParameter p_UpdatedDate = command.Parameters.Add("UpdatedDate", SqlDbType.DateTime);
    //    p_UpdatedDate.Direction = ParameterDirection.Input;
    //    p_UpdatedDate.Value = objLevel.UpdatedDate; 

    //    SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
    //    p_IsUpdate.Direction = ParameterDirection.Input;
    //    p_IsUpdate.Value = IsUpdate;

    //    SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
    //    p_IsDelete.Direction = ParameterDirection.Input;
    //    p_IsDelete.Value = IsDelete;

    //    SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
    //    p_IsActive.Direction = ParameterDirection.Input;
    //    p_IsActive.Value = IsActive;

    //    try
    //    {
    //        objDC.ExecuteQuery(command);
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

    public void InsertHRBudgetInfo(string strBudgetId, string strFiscalYrId, string strDesigId, string strEmpCount,
        string strInsertedBy, string strInsertedDate, string IsUpdate, string IsDelete,string strGADCode,string strBudgetAmt,string strStatus)
    {
        SqlCommand command = new SqlCommand("proc_Insert_HRBudgetInfo");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_BudgetId = command.Parameters.Add("BudgetId", SqlDbType.BigInt);
        p_BudgetId.Direction = ParameterDirection.Input;
        p_BudgetId.Value = strBudgetId;

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = strFiscalYrId;

        SqlParameter p_DesigId = command.Parameters.Add("JobTitleId", SqlDbType.BigInt);
        p_DesigId.Direction = ParameterDirection.Input;
        p_DesigId.Value = strDesigId;

        SqlParameter p_EmpCount = command.Parameters.Add("EmpCount", SqlDbType.BigInt);
        p_EmpCount.Direction = ParameterDirection.Input;
        p_EmpCount.Value = Convert.ToInt32(strEmpCount);

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsertedDate;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_GADCode = command.Parameters.Add("GADCode", SqlDbType.Char);
        p_GADCode.Direction = ParameterDirection.Input;
        p_GADCode.Value = strGADCode;

        SqlParameter p_BudgetAmt = command.Parameters.Add("BudgetAmt", SqlDbType.Decimal);
        p_BudgetAmt.Direction = ParameterDirection.Input;
        p_BudgetAmt.Value = strBudgetAmt;

        SqlParameter p_VStatus = command.Parameters.Add("VStatus", SqlDbType.Char);
        p_VStatus.Direction = ParameterDirection.Input;
        p_VStatus.Value = strStatus;

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
    //public void InsertEmpHospitalization(EmpHospital EmpHos, string IsUpdate, string IsDelete)
    //{
    //    // sproc functionality
    //    SqlCommand command = new SqlCommand("proc_Insert_EmpHospitalization");
    //    command.CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_HSRecId = command.Parameters.Add("HSRecId", SqlDbType.BigInt);
    //    p_HSRecId.Direction = ParameterDirection.Input;
    //    p_HSRecId.Value = EmpHos.HSRecId;

    //    SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
    //    p_EmpId.Direction = ParameterDirection.Input;
    //    p_EmpId.Value = EmpHos.EmpId;

    //    SqlParameter p_FmId = command.Parameters.Add("FmId", DBNull.Value);
    //    p_FmId.Direction = ParameterDirection.Input;
    //    p_FmId.IsNullable = true;
    //    if (EmpHos.FmId != "")
    //        p_FmId.Value = EmpHos.FmId;

    //    SqlParameter p_Diseas = command.Parameters.Add("Diseas", SqlDbType.VarChar);
    //    p_Diseas.Direction = ParameterDirection.Input;
    //    p_Diseas.Value = EmpHos.Diseas;

    //    SqlParameter p_AdmittedOn = command.Parameters.Add("AdmittedOn", DBNull.Value);
    //    p_AdmittedOn.Direction = ParameterDirection.Input;
    //    p_AdmittedOn.IsNullable = true;
    //    if (EmpHos.AdmittedOn != "")
    //        p_AdmittedOn.Value = EmpHos.AdmittedOn;

    //    SqlParameter p_ReleasedOn = command.Parameters.Add("ReleasedOn", DBNull.Value);
    //    p_ReleasedOn.Direction = ParameterDirection.Input;
    //    p_ReleasedOn.IsNullable = true;
    //    if (EmpHos.ReleasedOn != "")
    //        p_ReleasedOn.Value = EmpHos.ReleasedOn;

    //    SqlParameter p_Hospital = command.Parameters.Add("Hospital", SqlDbType.VarChar);
    //    p_Hospital.Direction = ParameterDirection.Input;
    //    p_Hospital.Value = EmpHos.Hospital;

    //    SqlParameter p_ClaimedAmt = command.Parameters.Add("ClaimedAmt", SqlDbType.Decimal);
    //    p_ClaimedAmt.Direction = ParameterDirection.Input;
    //    p_ClaimedAmt.Value = EmpHos.ClaimedAmt;

    //    SqlParameter p_ReimbursedAmt = command.Parameters.Add("ReimbursedAmt", SqlDbType.Decimal);
    //    p_ReimbursedAmt.Direction = ParameterDirection.Input;
    //    p_ReimbursedAmt.Value = EmpHos.ReimbursedAmt;

    //    SqlParameter p_ClaimedOn = command.Parameters.Add("ClaimedOn", DBNull.Value);
    //    p_ClaimedOn.Direction = ParameterDirection.Input;
    //    p_ClaimedOn.IsNullable = true;
    //    if (EmpHos.ClaimedOn != "")
    //        p_ClaimedOn.Value = EmpHos.ClaimedOn;

    //    SqlParameter p_ReimbursedOn = command.Parameters.Add("ReimbursedOn", DBNull.Value);
    //    p_ReimbursedOn.Direction = ParameterDirection.Input;
    //    p_ReimbursedOn.IsNullable = true;
    //    if (EmpHos.ReimbursedOn != "")
    //        p_ReimbursedOn.Value = EmpHos.ReimbursedOn;

    //    SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy.Direction = ParameterDirection.Input;
    //    p_InsertedBy.Value = EmpHos.InsertedBy;

    //    SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.Value = EmpHos.InsertedDate;

    //    SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
    //    p_IsUpdate.Direction = ParameterDirection.Input;
    //    p_IsUpdate.Value = IsUpdate;

    //    SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
    //    p_IsDelete.Direction = ParameterDirection.Input;
    //    p_IsDelete.Value = IsDelete;

    //    try
    //    {
    //        objDC.ExecuteQuery(command);
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
    
    #region Select Queries From Tables By store procedure
    //Select division
    public DataTable  SelectDivision(Int32 DivisionID)
    {
        if (objDC.ds.Tables["Division"] != null)
        {
            objDC.ds.Tables["Division"].Rows.Clear();
            objDC.ds.Tables["Division"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_Select_Division");

        SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = DivisionID;

        objDC.CreateDSFromProc(command, "Division");
        return objDC.ds.Tables["Division"];
    }
    //Mamun Bank

    //public DataTable SelectBank(Int32 DivisionID)
    //{
    //    if (objDC.ds.Tables["Division"] != null)
    //    {
    //        objDC.ds.Tables["Division"].Rows.Clear();
    //        objDC.ds.Tables["Division"].Dispose();
    //    }

    //    SqlCommand command = new SqlCommand("proc_Select_Bank");

    //    SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
    //    p_DivisionID.Direction = ParameterDirection.Input;
    //    p_DivisionID.Value = DivisionID;

    //    objDC.CreateDSFromProc(command, "Division");
    //    return objDC.ds.Tables["Division"];
    //}



    public DataTable SelectSector(Int32 SectorID)
    {
        SqlCommand command = new SqlCommand("proc_Select_SectorList");

        SqlParameter p_SectorId = command.Parameters.Add("SectorId", SqlDbType.BigInt);
        p_SectorId.Direction = ParameterDirection.Input;
        p_SectorId.Value = SectorID;

        objDC.CreateDSFromProc(command, "SectorList");
        return objDC.ds.Tables["SectorList"];
    }


    //proc_Select_EmpType

    public DataTable SelectEmpTypeList(Int32 EmpTypeID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpType");

        SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EmpTypeID.Direction = ParameterDirection.Input;
        p_EmpTypeID.Value = EmpTypeID;

        objDC.CreateDSFromProc(command, "EmpTypeList");
        return objDC.ds.Tables["EmpTypeList"];
    }

    public DataTable SelectSHeadBlockData(string strDate)
    {
        SqlCommand command = new SqlCommand("proc_Select_SalaryHeadBlock");

        SqlParameter p_VDate = command.Parameters.Add("VDate", DBNull.Value);
        p_VDate.Direction = ParameterDirection.Input;
        p_VDate.IsNullable = true;
        if (string.IsNullOrEmpty(strDate) == false)
            p_VDate.Value = strDate;

        objDC.CreateDSFromProc(command, "tblSalaryHeadBlock");
        return objDC.ds.Tables["tblSalaryHeadBlock"];
    }

    public DataTable GetEmpSalaryHeadBlockedData()
    {
        SqlCommand cmd = new SqlCommand("SELECT SHB.*,SH.HEADNAME,EI.FullName FROM SalaryHeadBlock SHB "+    
                                        "JOIN SALARYHEAD SH ON SHB.SHeadId = SH.SHEADID "+
                                        "JOIN EmpInfo EI  ON EI.EmpId = SHB.EmpId "+
                                        "ORDER BY EmpId, SHB.SHeadId, SHB.FromDate");
        cmd.CommandType = CommandType.Text;

        return objDC.CreateDT(cmd, "GetEmpSalaryHeadBlockedData");
    }

    public DataTable SelectProbationalEmployee(string vDate)
    {
        if (objDC.ds.Tables["tblProbationalEmp"] != null)
        {
            objDC.ds.Tables["tblProbationalEmp"].Rows.Clear();
            objDC.ds.Tables["tblProbationalEmp"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_PF_Head_Block");

        SqlParameter p_vDate = command.Parameters.Add("vDate", SqlDbType.DateTime);
        p_vDate.Direction = ParameterDirection.Input;
        p_vDate.Value = vDate;

        objDC.CreateDSFromProc(command, "tblProbationalEmp");
        return objDC.ds.Tables["tblProbationalEmp"];
    }

    public DataTable SelectDivisionddl(Int32 DivisionID)
    {
        SqlCommand command = new SqlCommand("proc_Select_Divisionddl");
        SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = DivisionID;
        objDC.CreateDSFromProc(command, "Division");
        return objDC.ds.Tables["Division"];
    }

    public DataTable SelectUnit(Int32 Unit)
    {
        SqlCommand command = new SqlCommand("proc_Select_UnitList");

        SqlParameter p_UnitId = command.Parameters.Add("UnitId", SqlDbType.BigInt);
        p_UnitId.Direction = ParameterDirection.Input;
        p_UnitId.Value = Unit;

        objDC.CreateDSFromProc(command, "SelectUnit");
        return objDC.ds.Tables["SelectUnit"];
    }

    public DataTable SelectLeavePeriodddl()
    {
        SqlCommand command = new SqlCommand("proc_Select_LeavePeriodddl");
        
        objDC.CreateDSFromProc(command, "LeavePeriodddl");
        return objDC.ds.Tables["LeavePeriodddl"];
    }
    public DataTable SelectDisicipline(Int32 DisiciplineId)
    {
        SqlCommand command = new SqlCommand("proc_Select_DisiciplineList");
        SqlParameter p_DisiciplineId = command.Parameters.Add("DisiciplineId", SqlDbType.BigInt);
        p_DisiciplineId.Direction = ParameterDirection.Input;
        p_DisiciplineId.Value = DisiciplineId;
        objDC.CreateDSFromProc(command, "Disiciplineinfo");
        return objDC.ds.Tables["Disiciplineinfo"];
    }

    public DataTable SelectSBU(Int32 SBUID)
    {
        SqlCommand command = new SqlCommand("proc_Select_SBU");
        SqlParameter p_SBUID = command.Parameters.Add("SBUID", SqlDbType.BigInt);
        p_SBUID.Direction = ParameterDirection.Input;
        p_SBUID.Value = SBUID;
        objDC.CreateDSFromProc(command, "SBU");
        return objDC.ds.Tables["SBU"];
    }
    public DataTable SelectSBUddl(Int32 SBUID)
    {
        SqlCommand command = new SqlCommand("proc_Select_SBUddl");
        SqlParameter p_SBUID = command.Parameters.Add("SBUID", SqlDbType.BigInt);
        p_SBUID.Direction = ParameterDirection.Input;
        p_SBUID.Value = SBUID;
        objDC.CreateDSFromProc(command, "SBUddl");
        return objDC.ds.Tables["SBUddl"];
    }
    public DataTable SelectDepartment(Int32 DeptID)
    {
        SqlCommand command = new SqlCommand("proc_Select_Dept");
        SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
        p_DeptID.Direction = ParameterDirection.Input;
        p_DeptID.Value = DeptID;
        objDC.CreateDSFromProc(command, "Dept");
        return objDC.ds.Tables["Dept"];
    }

    public DataTable SelectDepartmentCode(Int32 DeptID)
    {
        string strSQL = "SELECT DeptId,DeptCode FROM DepartmentList WHERE ISACTIVE='Y'";

        return objDC.CreateDT(strSQL, "DepartmentCode");
    }

    public DataTable SelectSubDepartment(Int32 SubDeptID)
    {
        SqlCommand command = new SqlCommand("proc_Select_SubDept");
        SqlParameter p_DeptID = command.Parameters.Add("SubDeptID", SqlDbType.BigInt);
        p_DeptID.Direction = ParameterDirection.Input;
        p_DeptID.Value = SubDeptID;
        objDC.CreateDSFromProc(command, "SubDept");
        return objDC.ds.Tables["SubDept"];
    }

    public DataTable SelectDeptWsSubDept(Int32 iDeptId)
    {
        string strSQL = "Select SubDeptId,SubDeptName From SubDepartmentList Where DeptId=" + iDeptId;
        SqlCommand cmd = new SqlCommand(strSQL);

        SqlParameter p_DeptId = cmd.Parameters.Add("DeptId", SqlDbType.BigInt);
        p_DeptId.Direction = ParameterDirection.Input;
        p_DeptId.Value = iDeptId;

        return objDC.CreateDT(cmd, "DeptWsSubDept");
    }

    public DataTable SelectSpecialSkill(Int32 SpecSkillId)
    {
        SqlCommand command = new SqlCommand("proc_Select_SpecialSkillList");

        SqlParameter p_DeptID = command.Parameters.Add("SpecSkillId", SqlDbType.BigInt);
        p_DeptID.Direction = ParameterDirection.Input;
        p_DeptID.Value = SpecSkillId;

        objDC.CreateDSFromProc(command, "Dept");
        return objDC.ds.Tables["Dept"];
    }

    public DataTable SelectDepartmentddl(Int32 DeptID)
    {
        SqlCommand command = new SqlCommand("proc_Select_Deptddl");
        SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
        p_DeptID.Direction = ParameterDirection.Input;
        p_DeptID.Value = DeptID;
        objDC.CreateDSFromProc(command, "Deptddl");
        return objDC.ds.Tables["Deptddl"];
    }
    public DataTable SelectDepartmentddl2(Int32 DeptID)
    {
        SqlCommand command = new SqlCommand("proc_Select_Deptddl");
        SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
        p_DeptID.Direction = ParameterDirection.Input;
        p_DeptID.Value = DeptID;
        objDC.CreateDSFromProc(command, "Deptddl2");
        return objDC.ds.Tables["Deptddl2"];
    }
    public DataTable SelectLocation(Int32 LocationID)
    {
        SqlCommand command = new SqlCommand("proc_Select_Location");
        SqlParameter p_LocationID = command.Parameters.Add("LocationID", SqlDbType.BigInt);
        p_LocationID.Direction = ParameterDirection.Input;
        p_LocationID.Value = LocationID;
        objDC.CreateDSFromProc(command, "Location");
        return objDC.ds.Tables["Location"];
    }

    public DataTable SelectDivisionWiseDistrict(Int32 DivisionId)
    {
        SqlCommand command = new SqlCommand("proc_select_DivisionWiseDistrict");

        SqlParameter p_LocationID = command.Parameters.Add("DivisionId", SqlDbType.BigInt);
        p_LocationID.Direction = ParameterDirection.Input;
        p_LocationID.Value = DivisionId;

        objDC.CreateDSFromProc(command, "tblDivisionWiseDistrict");
        return objDC.ds.Tables["tblDivisionWiseDistrict"];
    }

    public DataTable SelectDivisionWiseDistrict2(Int32 DivisionId)
    {
        SqlCommand command = new SqlCommand("proc_select_DivisionWiseDistrict2");

        SqlParameter p_LocationID = command.Parameters.Add("DivisionId", SqlDbType.BigInt);
        p_LocationID.Direction = ParameterDirection.Input;
        p_LocationID.Value = DivisionId;

        objDC.CreateDSFromProc(command, "tblDivisionWiseDistrict2");
        return objDC.ds.Tables["tblDivisionWiseDistrict2"];
    }

    public DataTable SelectSectorWiseDepartment2(Int32 SectorId)
    {
        SqlCommand command = new SqlCommand("proc_Select_SectorWiseDepartment2");

        SqlParameter p_LocationID = command.Parameters.Add("SectorId", SqlDbType.BigInt);
        p_LocationID.Direction = ParameterDirection.Input;
        p_LocationID.Value = SectorId;

        objDC.CreateDSFromProc(command, "tblSectorWiseDepartment2");
        return objDC.ds.Tables["tblSectorWiseDepartment2"];
    }

    public DataTable SelectDesigWiseJobTitle(Int32 DesigId)
    {
        SqlCommand command = new SqlCommand("proc_select_DesigWiseJobTitle");

        SqlParameter p_LocationID = command.Parameters.Add("DesigId", SqlDbType.BigInt);
        p_LocationID.Direction = ParameterDirection.Input;
        p_LocationID.Value = DesigId;

        objDC.CreateDSFromProc(command, "tblDesigWiseJobTitle");
        return objDC.ds.Tables["tblDesigWiseJobTitle"];
    }

    public DataTable SelectSectorWiseDepartment(Int32 SectorId)
    {
        SqlCommand command = new SqlCommand("proc_select_SectorWiseDepartment");

        SqlParameter p_SectorId = command.Parameters.Add("SectorId", SqlDbType.BigInt);
        p_SectorId.Direction = ParameterDirection.Input;
        p_SectorId.Value = SectorId;

        objDC.CreateDSFromProc(command, "tblSectorWiseDepartment");
        return objDC.ds.Tables["tblSectorWiseDepartment"];
    }

    public DataTable SelectSalaryLocWiseSubLoc(Int32 SalLocId)
    {
        SqlCommand command = new SqlCommand("proc_select_SalaryLocWiseSubLoc");

        SqlParameter p_SalLocId = command.Parameters.Add("SalLocId", SqlDbType.BigInt);
        p_SalLocId.Direction = ParameterDirection.Input;
        p_SalLocId.Value = SalLocId;

        objDC.CreateDSFromProc(command, "tblSalaryLocWiseSubLoc");
        return objDC.ds.Tables["tblSalaryLocWiseSubLoc"];
    }

    public DataTable SelectDesigWiseJobTitle2(Int32 DesigId)
    {
        SqlCommand command = new SqlCommand("proc_select_DesigWiseJobTitle2");

        SqlParameter p_LocationID = command.Parameters.Add("DesigId", SqlDbType.BigInt);
        p_LocationID.Direction = ParameterDirection.Input;
        p_LocationID.Value = DesigId;

        objDC.CreateDSFromProc(command, "tblDesigWiseJobTitle2");
        return objDC.ds.Tables["tblDesigWiseJobTitle2"];
    }

    public DataTable SelectCountry(Int32 CountryID)
    {
        SqlCommand command = new SqlCommand("proc_Select_Country");
        SqlParameter p_CountryID = command.Parameters.Add("CountryID", SqlDbType.BigInt);
        p_CountryID.Direction = ParameterDirection.Input;
        p_CountryID.Value = CountryID;
        objDC.CreateDSFromProc(command, "Country");
        return objDC.ds.Tables["Country"];
    }
    
    public DataTable SelectSection(Int32 SectionID)
    {
        SqlCommand command = new SqlCommand("proc_Select_Section");
        SqlParameter p_SectionID = command.Parameters.Add("SectionID", SqlDbType.BigInt);
        p_SectionID.Direction = ParameterDirection.Input;
        p_SectionID.Value = SectionID;
        objDC.CreateDSFromProc(command, "Section");
        return objDC.ds.Tables["Section"];
    }
    public DataTable SelectSectionddl(Int32 SectionID)
    {
        SqlCommand command = new SqlCommand("proc_Select_Sectionddl");
        SqlParameter p_SectionID = command.Parameters.Add("SectionID", SqlDbType.BigInt);
        p_SectionID.Direction = ParameterDirection.Input;
        p_SectionID.Value = SectionID;
        objDC.CreateDSFromProc(command, "Sectionddl");
        return objDC.ds.Tables["Sectionddl"];
    }
    public DataTable SelectUpazilla(Int32 UpazillaID)
    {
        SqlCommand command = new SqlCommand("proc_Select_Upazilla");
        SqlParameter p_UpazillaID = command.Parameters.Add("UpazillaID", SqlDbType.BigInt);
        p_UpazillaID.Direction = ParameterDirection.Input;
        p_UpazillaID.Value = UpazillaID;
        objDC.CreateDSFromProc(command, "Upazilla");
        return objDC.ds.Tables["Upazilla"];
    }
    public DataTable SelectDistrict(Int32 DistrictID)
    {
        if (objDC.ds.Tables["District"] != null)
        {
            objDC.ds.Tables["District"].Rows.Clear();
            objDC.ds.Tables["District"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_Select_District");

        SqlParameter p_DistrictID = command.Parameters.Add("DistrictID", SqlDbType.BigInt);
        p_DistrictID.Direction = ParameterDirection.Input;
        p_DistrictID.Value = DistrictID;

        objDC.CreateDSFromProc(command, "District");
        return objDC.ds.Tables["District"];
    }

    public DataTable SelectMedNominee(Int32 NomineeId, string EmpId)
    {
        if (objDC.ds.Tables["Nominee"] != null)
        {
            objDC.ds.Tables["Nominee"].Rows.Clear();
            objDC.ds.Tables["Nominee"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_Select_MedNominee");

        SqlParameter p_NomineeId = command.Parameters.Add("NomineeId", SqlDbType.BigInt);
        p_NomineeId.Direction = ParameterDirection.Input;
        p_NomineeId.Value = NomineeId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDSFromProc(command, "Nominee");
        return objDC.ds.Tables["Nominee"];
    }

    public DataTable SelectReason(Int32 ReasonId)
    {
        if (objDC.ds.Tables["ReasonList"] != null)
        {
            objDC.ds.Tables["ReasonList"].Rows.Clear();
            objDC.ds.Tables["ReasonList"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_Select_Reason");

        SqlParameter p_DistrictID = command.Parameters.Add("ReasonId", SqlDbType.BigInt);
        p_DistrictID.Direction = ParameterDirection.Input;
        p_DistrictID.Value = ReasonId;

        objDC.CreateDSFromProc(command, "ReasonList");
        return objDC.ds.Tables["ReasonList"];
    }

    public DataTable SelectHomeDistrict(Int32 DistrictID)
    {
        if (objDC.ds.Tables["HomeDistrict"] != null)
        {
            objDC.ds.Tables["HomeDistrict"].Rows.Clear();
            objDC.ds.Tables["HomeDistrict"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_Select_HomeDistrict");

        SqlParameter p_DistrictID = command.Parameters.Add("DistrictID", SqlDbType.BigInt);
        p_DistrictID.Direction = ParameterDirection.Input;
        p_DistrictID.Value = DistrictID;

        objDC.CreateDSFromProc(command, "District");
        return objDC.ds.Tables["District"];
    }

    public DataTable SelectGrade(Int32 GradeID)
    {
        SqlCommand command = new SqlCommand("proc_Select_Grade");

        SqlParameter p_GradeID = command.Parameters.Add("GradeID", SqlDbType.BigInt);
        p_GradeID.Direction = ParameterDirection.Input;
        p_GradeID.Value = GradeID;

        objDC.CreateDSFromProc(command, "Grade");
        return objDC.ds.Tables["Grade"];
    }

    public DataTable SelectGradeWsDesignation(Int32 iGradeId)
    {
        SqlCommand command = new SqlCommand("proc_select_GradeWsDesig");

        SqlParameter p_GradeIdD = command.Parameters.Add("GradeId", SqlDbType.BigInt);
        p_GradeIdD.Direction = ParameterDirection.Input;
        p_GradeIdD.Value = iGradeId;

        objDC.CreateDSFromProc(command, "tblGradeWsDesig");
        return objDC.ds.Tables["tblGradeWsDesig"];
    }

    public DataTable SelectGradeLevel(Int32 GradeLevelID)
    {
        SqlCommand command = new SqlCommand("proc_Select_GradeLevel");

        SqlParameter p_GradeID = command.Parameters.Add("GradeLevelId", SqlDbType.BigInt);
        p_GradeID.Direction = ParameterDirection.Input;
        p_GradeID.Value = GradeLevelID;

        objDC.CreateDSFromProc(command, "tblGradeLevel");
        return objDC.ds.Tables["tblGradeLevel"];
    }

    public DataTable SelectGradeddl(Int32 GradeID)
    {
        SqlCommand command = new SqlCommand("proc_Select_Gradeddl");
        SqlParameter p_GradeID = command.Parameters.Add("GradeID", SqlDbType.BigInt);
        p_GradeID.Direction = ParameterDirection.Input;
        p_GradeID.Value = GradeID;
        objDC.CreateDSFromProc(command, "Gradeddl");
        return objDC.ds.Tables["Gradeddl"];
    }

    public DataTable SelectJobTitle(Int32 JobTitleId)
    {
        SqlCommand command = new SqlCommand("proc_Select_JobTitle");

        SqlParameter p_JobTitleId = command.Parameters.Add("JobTitleId", SqlDbType.BigInt);
        p_JobTitleId.Direction = ParameterDirection.Input;
        p_JobTitleId.Value = JobTitleId;

        objDC.CreateDSFromProc(command, "JobTitle");
        return objDC.ds.Tables["JobTitle"];
    }
   
    public DataTable SelectDesignation(Int32 DesgID)
    {
        SqlCommand command = new SqlCommand("proc_Select_Designation");

        SqlParameter p_DesgID = command.Parameters.Add("DesgId", SqlDbType.BigInt);
        p_DesgID.Direction = ParameterDirection.Input;
        p_DesgID.Value = DesgID;

        objDC.CreateDSFromProc(command, "Designation");
        return objDC.ds.Tables["Designation"];
    }

    public DataTable SelectDivisionList(Int32 DivisionID)
    {
        SqlCommand command = new SqlCommand("proc_Select_DivisionList");

        SqlParameter p_DesgID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
        p_DesgID.Direction = ParameterDirection.Input;
        p_DesgID.Value = DivisionID;

        objDC.CreateDSFromProc(command, "DivisionList");
        return objDC.ds.Tables["DivisionList"];
    }

    public DataTable SelectSubDesignation(Int32 DesgID)
    {
        SqlCommand command = new SqlCommand("proc_Select_SubDesignation");

        SqlParameter p_DesgID = command.Parameters.Add("SubDesigId", SqlDbType.BigInt);
        p_DesgID.Direction = ParameterDirection.Input;
        p_DesgID.Value = DesgID;

        objDC.CreateDSFromProc(command, "SubDesignation");
        return objDC.ds.Tables["SubDesignation"];
    }

    public DataTable SelectSalaryLocation(Int32 SalaryLocId)
    {
        SqlCommand command = new SqlCommand("proc_Select_SalaryLocation");

        SqlParameter p_SalLocId = command.Parameters.Add("SalLocId", SqlDbType.BigInt);
        p_SalLocId.Direction = ParameterDirection.Input;
        p_SalLocId.Value = SalaryLocId;

        objDC.CreateDSFromProc(command, "SalaryLocation");
        return objDC.ds.Tables["SalaryLocation"];
    }

    public DataTable SelectSalarySubLocation(Int32 SalSubLocId)
    {
        SqlCommand command = new SqlCommand("proc_Select_SalarySubLocation");

        SqlParameter p_SalLocId = command.Parameters.Add("SalSubLocId", SqlDbType.BigInt);
        p_SalLocId.Direction = ParameterDirection.Input;
        p_SalLocId.Value = SalSubLocId;

        objDC.CreateDSFromProc(command, "SalarySubLocation");
        return objDC.ds.Tables["SalarySubLocation"];
    }

    public DataTable SelectProjectOffice(Int32 DistrictID)
    {
        if (objDC.ds.Tables["ProjectOffice"] != null)
        {
            objDC.ds.Tables["ProjectOffice"].Rows.Clear();
            objDC.ds.Tables["ProjectOffice"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_Select_ProjectOffice");

        SqlParameter p_DistrictID = command.Parameters.Add("ProjOfficeId", SqlDbType.BigInt);
        p_DistrictID.Direction = ParameterDirection.Input;
        p_DistrictID.Value = DistrictID;

        objDC.CreateDSFromProc(command, "ProjectOffice");
        return objDC.ds.Tables["ProjectOffice"];
    }

    public DataTable SelectPositionByFunction(Int32 PosFuncId)
    {
        SqlCommand command = new SqlCommand("proc_Select_PositionByFunction");

        SqlParameter p_DesgID = command.Parameters.Add("PosFuncId", SqlDbType.BigInt);
        p_DesgID.Direction = ParameterDirection.Input;
        p_DesgID.Value = PosFuncId;

        objDC.CreateDSFromProc(command, "PositionByFunction");
        return objDC.ds.Tables["PositionByFunction"];
    }

    public DataTable SelectReligionList(Int32 ReligionId)
    {
        SqlCommand command = new SqlCommand("proc_Select_ReligionList");

        SqlParameter p_DesgID = command.Parameters.Add("ReligionId", SqlDbType.BigInt);
        p_DesgID.Direction = ParameterDirection.Input;
        p_DesgID.Value = ReligionId;

        objDC.CreateDSFromProc(command, "ReligionList");
        return objDC.ds.Tables["ReligionList"];
    }

    public string SelectReligionId(string strReligionName)
    {
        string strSQL = "Select ReligionId From ReligionList Where ReligionName='" + strReligionName + "'";
        SqlCommand cmd = new SqlCommand(strSQL);

        SqlParameter p_ReligionName = cmd.Parameters.Add("ReligionName", SqlDbType.VarChar);
        p_ReligionName.Direction = ParameterDirection.Input;
        p_ReligionName.Value = strReligionName;

        return objDC.GetScalarVal(cmd);
    }

    public DataTable SelectFestivalList(Int32 FestivalID)
    {
        SqlCommand command = new SqlCommand("proc_Select_FestivalList");

        SqlParameter p_FestivalID = command.Parameters.Add("FestivalID", SqlDbType.BigInt);
        p_FestivalID.Direction = ParameterDirection.Input;
        p_FestivalID.Value = FestivalID;

        objDC.CreateDSFromProc(command, "FestivalList");
        return objDC.ds.Tables["FestivalList"];
    }

    public DataTable SelectRelagionFestivalList(Int32 ReligionID)
    {
        SqlCommand command = new SqlCommand("proc_Select_ReligionWiseFestivalList");

        SqlParameter p_FestivalID = command.Parameters.Add("@ReligionID", SqlDbType.BigInt);
        p_FestivalID.Direction = ParameterDirection.Input;
        p_FestivalID.Value = ReligionID;

        objDC.CreateDSFromProc(command, "RFestivalList");
        return objDC.ds.Tables["RFestivalList"];
    }


    public DataTable SelectBloodGroupList(Int32 BloodGroupId)
    {
        SqlCommand command = new SqlCommand("proc_Select_BloodGroupList");

        SqlParameter p_DesgID = command.Parameters.Add("BloodGroupId", SqlDbType.BigInt);
        p_DesgID.Direction = ParameterDirection.Input;
        p_DesgID.Value = BloodGroupId;

        objDC.CreateDSFromProc(command, "BloodGroupList");
        return objDC.ds.Tables["BloodGroupList"];
    }

    public DataTable SelectRelationList(Int32 RelationId)
    {
        SqlCommand command = new SqlCommand("proc_Select_RelationList");

        SqlParameter p_DesgID = command.Parameters.Add("RelationId", SqlDbType.BigInt);
        p_DesgID.Direction = ParameterDirection.Input;
        p_DesgID.Value = RelationId;

        objDC.CreateDSFromProc(command, "RelationList");
        return objDC.ds.Tables["RelationList"];
    }

    public DataTable SelectLeaveType(Int32 LTypeID)
    {
        SqlCommand command = new SqlCommand("proc_Select_LeaveType");
        SqlParameter p_LTypeID = command.Parameters.Add("LTypeID", SqlDbType.BigInt);
        p_LTypeID.Direction = ParameterDirection.Input;
        p_LTypeID.Value = LTypeID;
        objDC.CreateDSFromProc(command, "LeaveType");
        return objDC.ds.Tables["LeaveType"];
    }

    public DataTable SelectLeavePakMst(Int32 LPakID)
    {
        SqlCommand command = new SqlCommand("proc_Select_LeavePakMst");
        SqlParameter p_LTypeID = command.Parameters.Add("LeavePakID", SqlDbType.BigInt);
        p_LTypeID.Direction = ParameterDirection.Input;
        p_LTypeID.Value = LPakID;
        objDC.CreateDSFromProc(command, "LeavePakMst");
        return objDC.ds.Tables["LeavePakMst"];
    }

    public DataTable SelectAttendancePolicy(Int32 AttnPolicyId)
    {
        SqlCommand command = new SqlCommand("proc_Select_AttnPolicy");
        SqlParameter p_AttnPolicyId = command.Parameters.Add("AttnPolicyId", SqlDbType.BigInt);
        p_AttnPolicyId.Direction = ParameterDirection.Input;
        p_AttnPolicyId.Value = AttnPolicyId;
        objDC.CreateDSFromProc(command, "AttnPolicy");
        return objDC.ds.Tables["AttnPolicy"];
    }
    public DataTable SelectAttendancePolicySBUWise(string    SBUId)
    {
        SqlCommand command = new SqlCommand("proc_Select_AttnPolicy_SBUWise");
       
        SqlParameter p_SBUId = command.Parameters.Add("SBUId", SqlDbType.BigInt);
        p_SBUId.Direction = ParameterDirection.Input;
        p_SBUId.Value = int.Parse(SBUId);

        objDC.CreateDSFromProc(command, "AttnPolicySBU");
        return objDC.ds.Tables["AttnPolicySBU"];
    }
    


    public DataTable SelectWeekendPolicy(Int32 WeekendID)
    {
        SqlCommand command = new SqlCommand("proc_Select_Weekend");
        SqlParameter p_WeekendID = command.Parameters.Add("WeekendID", SqlDbType.BigInt);
        p_WeekendID.Direction = ParameterDirection.Input;
        p_WeekendID.Value = WeekendID;
        objDC.CreateDSFromProc(command, "Weekend");
        return objDC.ds.Tables["Weekend"];
    }
    public DataTable SelectLeavePakDet(Int32 LPakID)
    {
        SqlCommand command = new SqlCommand("proc_Select_LPakDetls");
        SqlParameter p_LTypeID = command.Parameters.Add("LPakID", SqlDbType.BigInt);
        p_LTypeID.Direction = ParameterDirection.Input;
        p_LTypeID.Value = LPakID;
        objDC.CreateDSFromProc(command, "LeavePakDet");
        return objDC.ds.Tables["LeavePakDet"];
    }

    public DataTable SelectSubGrade(Int32 SubGradeID)
    {
        if (objDC.ds.Tables["SubGrade"] != null)
        {
            objDC.ds.Tables["SubGrade"].Rows.Clear();
            objDC.ds.Tables["SubGrade"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_Select_SubGrade");

        SqlParameter p_SubGradeID = command.Parameters.Add("SubGradeID", SqlDbType.BigInt);
        p_SubGradeID.Direction = ParameterDirection.Input;
        p_SubGradeID.Value = SubGradeID;

        objDC.CreateDSFromProc(command, "SubGrade");
        return objDC.ds.Tables["SubGrade"];
    }

    public DataTable SelectStaffType(Int32 StaffTypeId)
    {
        SqlCommand command = new SqlCommand("proc_Select_StaffTypeList");

        SqlParameter p_StaffTypeId = command.Parameters.Add("StfTypeID", SqlDbType.BigInt);
        p_StaffTypeId.Direction = ParameterDirection.Input;
        p_StaffTypeId.Value = StaffTypeId;

        objDC.CreateDSFromProc(command, "StaffTypeList");
        return objDC.ds.Tables["StaffTypeList"];
    }

    public DataTable SelectStaffMode(Int32 StfModeID)
    {
        SqlCommand command = new SqlCommand("proc_Select_StaffModeList");

        SqlParameter p_StfModeID = command.Parameters.Add("StfModeID", SqlDbType.BigInt);
        p_StfModeID.Direction = ParameterDirection.Input;
        p_StfModeID.Value = StfModeID;

        objDC.CreateDSFromProc(command, "StaffModeList");
        return objDC.ds.Tables["StaffModeList"];
    }

    public DataTable SelectDivisionWiseSBU(Int32 SBUID)
    {
        SqlCommand command = new SqlCommand("proc_Select_DivisionWiseSBU");
        SqlParameter p_SBUID = command.Parameters.Add("SBUID", SqlDbType.BigInt);
        p_SBUID.Direction = ParameterDirection.Input;
        p_SBUID.Value = SBUID;
        objDC.CreateDSFromProc(command, "DivisionWiseSBU");
        return objDC.ds.Tables["DivisionWiseSBU"];
    }
    //*Sulata
    public DataTable SelectSBUWiseDivision(Int32 DivisionID)
    {
        SqlCommand command = new SqlCommand("proc_Select_SBUWiseDivision");
        SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = DivisionID;
        objDC.CreateDSFromProc(command, "DivisionWiseSBU");
        return objDC.ds.Tables["DivisionWiseSBU"];
    }
    public DataTable SelectSBUWiseDivisionsrc(Int32 DivisionID)
    {
        SqlCommand command = new SqlCommand("proc_Select_SBUWiseDivisionddl");
        SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = DivisionID;
        objDC.CreateDSFromProc(command, "DivisionWiseSBUsrc");
        return objDC.ds.Tables["DivisionWiseSBUsrc"];
    }
    public DataTable SelectSBUWiseDivisionddl(Int32 DivisionID)
    {
        SqlCommand command = new SqlCommand("proc_Select_SBUWiseDivisionddl");
        SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = DivisionID;
        objDC.CreateDSFromProc(command, "DivisionWiseSBUddl");
        return objDC.ds.Tables["DivisionWiseSBUddl"];
    }
    public DataTable SelectSBUWiseDivisionddlSrc(Int32 DivisionID)
    {
        SqlCommand command = new SqlCommand("proc_Select_SBUWiseDivisionddl");
        SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = DivisionID;
        objDC.CreateDSFromProc(command, "DivisionWiseSBUddlsrc");
        return objDC.ds.Tables["DivisionWiseSBUddlsrc"];
    }
    public DataTable SelectDeptWiseSBU(Int32 DeptID)
    {
        SqlCommand command = new SqlCommand("proc_Select_SBUWiseDept");
        SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
        p_DeptID.Direction = ParameterDirection.Input;
        p_DeptID.Value = DeptID;
        objDC.CreateDSFromProc(command, "SBUWiseDept");
        return objDC.ds.Tables["SBUWiseDept"];
    }
    public DataTable SelectSBUWiseDept(Int32 SBUID)
    {
        SqlCommand command = new SqlCommand("proc_Select_DeptWiseSBU");
        SqlParameter p_SBUID = command.Parameters.Add("SBUID", SqlDbType.BigInt);
        p_SBUID.Direction = ParameterDirection.Input;
        p_SBUID.Value = SBUID;
        objDC.CreateDSFromProc(command, "DeptWiseSBU");
        return objDC.ds.Tables["DeptWiseSBU"];
    }
     public DataTable SelectSBUWiseDeptddl(Int32 SBUID)
    {
        SqlCommand command = new SqlCommand("proc_Select_DeptWiseSBUddl");
        SqlParameter p_SBUID = command.Parameters.Add("SBUID", SqlDbType.BigInt);
        p_SBUID.Direction = ParameterDirection.Input;
        p_SBUID.Value = SBUID;
        objDC.CreateDSFromProc(command, "DeptWiseSBUddl");
        return objDC.ds.Tables["DeptWiseSBUddl"];
    }
    
    public DataTable SelectSectionWiseDept(Int32 SectionID)
    {
        SqlCommand command = new SqlCommand("proc_Select_DeptWiseSection");
        SqlParameter p_SectionID = command.Parameters.Add("SectionID", SqlDbType.BigInt);
        p_SectionID.Direction = ParameterDirection.Input;
        p_SectionID.Value = SectionID;
        objDC.CreateDSFromProc(command, "DeptWiseSection");
        return objDC.ds.Tables["DeptWiseSection"];
    }
    public DataTable SelectDeptWiseSection(Int32 DeptID)
    {
        SqlCommand command = new SqlCommand("proc_Select_SectionWiseDept");
        SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
        p_DeptID.Direction = ParameterDirection.Input;
        p_DeptID.Value = DeptID;
        objDC.CreateDSFromProc(command, "DeptWiseSection");
        return objDC.ds.Tables["DeptWiseSection"];
    }
    public DataTable SelectDeptWiseSectionddl(Int32 DeptID)
    {
        SqlCommand command = new SqlCommand("proc_Select_SectionWiseDeptddl");
        SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
        p_DeptID.Direction = ParameterDirection.Input;
        p_DeptID.Value = DeptID;
        objDC.CreateDSFromProc(command, "DeptWiseSectionddl");
        return objDC.ds.Tables["DeptWiseSectionddl"];
    }

    public DataTable SelectSubGradeWiseGrade(Int32 SubGradeID)
    {
        SqlCommand command = new SqlCommand("proc_Select_SubGradeWiseGrade");
        SqlParameter p_SubGradeID = command.Parameters.Add("SubGradeID", SqlDbType.BigInt);
        p_SubGradeID.Direction = ParameterDirection.Input;
        p_SubGradeID.Value = SubGradeID;
        objDC.CreateDSFromProc(command, "SubGradeWiseGrade");
        return objDC.ds.Tables["SubGradeWiseGrade"];
    }
    public DataTable SelectGradeWiseSubGrade(Int32 GradeID)
    {
        SqlCommand command = new SqlCommand("proc_Select_GradeWiseSubGrade");
        SqlParameter p_GradeID = command.Parameters.Add("GradeID", SqlDbType.BigInt);
        p_GradeID.Direction = ParameterDirection.Input;
        p_GradeID.Value = GradeID;
        objDC.CreateDSFromProc(command, "SubGradeWiseGrade");
        return objDC.ds.Tables["SubGradeWiseGrade"];
    }
    public DataTable SelectGradeWiseSubGradeddl(Int32 GradeID)
    {
        SqlCommand command = new SqlCommand("proc_Select_GradeWiseSubGradeddl");
        SqlParameter p_GradeID = command.Parameters.Add("GradeID", SqlDbType.BigInt);
        p_GradeID.Direction = ParameterDirection.Input;
        p_GradeID.Value = GradeID;
        objDC.CreateDSFromProc(command, "SubGradeWiseGradeddl");
        return objDC.ds.Tables["SubGradeWiseGradeddl"];
    }
    public DataTable SelectSalaryddl(Int32 SalPakId)
    {
        SqlCommand command = new SqlCommand("proc_Select_SalaryPakMst");
        SqlParameter p_SalPakId = command.Parameters.Add("SalPakId", SqlDbType.BigInt);
        p_SalPakId.Direction = ParameterDirection.Input;
        p_SalPakId.Value = SalPakId;

        objDC.CreateDSFromProc(command, "SalaryPakMstddl");
        return objDC.ds.Tables["SalaryPakMstddl"];
    }

    public DataTable SelectSponsor(Int32 SponsorId)
    {
        SqlCommand command = new SqlCommand("proc_Select_SponsorIdList");
        SqlParameter p_SponsorId = command.Parameters.Add("SponsorId", SqlDbType.BigInt);
        p_SponsorId.Direction = ParameterDirection.Input;
        p_SponsorId.Value = SponsorId;
        objDC.CreateDSFromProc(command, "SponsorInfo");
        return objDC.ds.Tables["SponsorInfo"];
    }
    public DataTable SelectEvent(Int32 EventId)
    {
        SqlCommand command = new SqlCommand("proc_Select_EventType");
        SqlParameter p_EventId = command.Parameters.Add("EventId", SqlDbType.BigInt);
        p_EventId.Direction = ParameterDirection.Input;
        p_EventId.Value = EventId;
        objDC.CreateDSFromProc(command, "EventInfo");
        return objDC.ds.Tables["EventInfo"];
    }

    public DataTable SelectEmpType(Int32 EmpTypeId)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpTypeList");
        SqlParameter p_EmpTypeID = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EmpTypeID.Direction = ParameterDirection.Input;
        p_EmpTypeID.Value = EmpTypeId;
        objDC.CreateDSFromProc(command, "EmpType");
        return objDC.ds.Tables["EmpType"];
    }

    public DataTable SelectEmpType(Int32 EmpTypeId,string strIsActive)
    {
        string strSQL = "SELECT EmpTypeId,TypeName FROM EmpTypeList WHERE ISACTIVE='Y' AND IsDeleted='N' ORDER BY TypeName DESC";

        return objDC.CreateDT(strSQL, "EmpTypeAc");
    }

    public DataTable SelectEmpNature(Int32 EmpNatureId)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpNatureList");
        SqlParameter p_EmpTypeID = command.Parameters.Add("EmpNatureID", SqlDbType.BigInt);
        p_EmpTypeID.Direction = ParameterDirection.Input;
        p_EmpTypeID.Value = EmpNatureId;
        objDC.CreateDSFromProc(command, "EmpNature");
        return objDC.ds.Tables["EmpNature"];
    }


    public DataTable SelectEmpTypeContract(Int32 EmpTypeId)
    {
        string strSQL = "SELECT EmpTypeId,TypeName FROM EmpTypeList WHERE EmpTypeId IN(2,3) AND ISACTIVE='Y'";

        return objDC.CreateDT(strSQL, "EmpTypeContract");
    }

    public DataTable SelectEmpTypeForEmpGroup(Int32 EmpGrpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpTypeList_For_EmpGroup");

        SqlParameter p_EmpTypeID = command.Parameters.Add("EmpGrpId", SqlDbType.BigInt);
        p_EmpTypeID.Direction = ParameterDirection.Input;
        p_EmpTypeID.Value = EmpGrpId;

        objDC.CreateDSFromProc(command, "EmpType");
        return objDC.ds.Tables["EmpType"];
    }

    public DataTable SelectEmpGroup(Int32 EmpGrpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpGroupList");

        SqlParameter p_EmpGrpId = command.Parameters.Add("EmpGrpId", SqlDbType.BigInt);
        p_EmpGrpId.Direction = ParameterDirection.Input;
        p_EmpGrpId.Value = EmpGrpId;

        objDC.CreateDSFromProc(command, "EmpGroupList");
        return objDC.ds.Tables["EmpGroupList"];
    }

    public DataTable SelectProject(Int32 ProjectId)
    {
        SqlCommand command = new SqlCommand("proc_Select_ProjectList");
        SqlParameter p_ProjectId = command.Parameters.Add("ProjectId", SqlDbType.BigInt);
        p_ProjectId.Direction = ParameterDirection.Input;
        p_ProjectId.Value = ProjectId;

        objDC.CreateDSFromProc(command, "ProjectList");
        return objDC.ds.Tables["ProjectList"];
    }

    public DataTable SelectProjectCode(Int32 ProjectId)
    {
        string strSQL = "SELECT ProjectId,ProjectCode FROM ProjectList WHERE ISACTIVE='Y'";

        return objDC.CreateDT(strSQL, "ProjectCode");
    }

    public DataTable SelectProjectWsBenefit(Int32 ProjectId)
    {
        SqlCommand command = new SqlCommand("proc_Select_ProjectWsBenefit");
        SqlParameter p_ProjectId = command.Parameters.Add("ProjectId", SqlDbType.BigInt);
        p_ProjectId.Direction = ParameterDirection.Input;
        p_ProjectId.Value = ProjectId;
        objDC.CreateDSFromProc(command, "ProjectWsBenefit");
        return objDC.ds.Tables["ProjectWsBenefit"];
    }
    public DataTable SelectCostCenterCode(Int32 ProjectId)
    {
        string strSQL = "SELECT CostCenterId,CostCenterCode FROM CostCenterList WHERE ISACTIVE='Y'";

        return objDC.CreateDT(strSQL, "CostCenterCode");
    }

    public DataTable SelectLevelofStudy(Int32 LevelId)
    {
        SqlCommand command = new SqlCommand("proc_Select_LevelofStudy");
        SqlParameter p_LevelId = command.Parameters.Add("LevelId", SqlDbType.BigInt);
        p_LevelId.Direction = ParameterDirection.Input;
        p_LevelId.Value = LevelId;
        objDC.CreateDSFromProc(command, "LevelofStudy");
        return objDC.ds.Tables["LevelofStudy"];
    }

    public DataTable SelectLevelofStudydll(Int32 LevelId)
    {
        SqlCommand command = new SqlCommand("proc_Select_LevelofStudydll");
        SqlParameter p_LevelId = command.Parameters.Add("LevelId", SqlDbType.BigInt);
        p_LevelId.Direction = ParameterDirection.Input;
        p_LevelId.Value = LevelId;
        objDC.CreateDSFromProc(command, "LevelofStudydll");
        return objDC.ds.Tables["LevelofStudydll"];
    }

    public DataTable SelectHRBudgetInfo(string strFisCalYrId)
    {
        if (objDC.ds.Tables["HRBudgetInfo"] != null)
        {
            objDC.ds.Tables["HRBudgetInfo"].Rows.Clear();
            objDC.ds.Tables["HRBudgetInfo"].Dispose();
        }
        SqlCommand command = new SqlCommand("proc_Select_HRBudgetInfo");
        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = strFisCalYrId;
        objDC.CreateDSFromProc(command, "HRBudgetInfo");
        return objDC.ds.Tables["HRBudgetInfo"];
    }

    public DataTable SelectHRBudgetGADInfo(Int32 BudgetId,string strFisCalYrId, string strGAD)
    {
        SqlCommand command = new SqlCommand("proc_Select_HRBudgetGADInfo");

        SqlParameter p_BudgetId = command.Parameters.Add("BudgetId", SqlDbType.BigInt);
        p_BudgetId.Direction = ParameterDirection.Input;
        p_BudgetId.Value = BudgetId;

        SqlParameter p_GADCODE = command.Parameters.Add("GADCODE", SqlDbType.Char);
        p_GADCODE.Direction = ParameterDirection.Input;
        p_GADCODE.Value = strGAD;

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = strFisCalYrId;

        objDC.CreateDSFromProc(command, "SelectHRBudgetGADInfo");
        return objDC.ds.Tables["SelectHRBudgetGADInfo"];
    }

    public DataTable GetHRBudgetGADData(string strFisCalYrId, string strGAD, string strJbTlId)
    {
        SqlCommand command = new SqlCommand("proc_Select_HRBudgetGADReport");

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = strFisCalYrId;

        SqlParameter p_JBTLID = command.Parameters.Add("JBTLID", SqlDbType.BigInt);
        p_JBTLID.Direction = ParameterDirection.Input;
        p_JBTLID.Value = strJbTlId;

        SqlParameter p_GADCODE = command.Parameters.Add("GADCODE", SqlDbType.Char);
        p_GADCODE.Direction = ParameterDirection.Input;
        p_GADCODE.Value = strGAD;


        objDC.CreateDSFromProc(command, "GetHRBudgetGADData");
        return objDC.ds.Tables["GetHRBudgetGADData"];
    }

    
    public DataTable SelectEmpMovementLog(string EmpID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpMovementLog");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpID;

        objDC.CreateDSFromProc(command, "EmpMovementLog");
        return objDC.ds.Tables["EmpMovementLog"];
    }

    public DataTable SelectEmpMovementLogId(string EmpID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpMovementLogId");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpID;

        objDC.CreateDSFromProc(command, "EmpMovementLogId");
        return objDC.ds.Tables["EmpMovementLogId"];
    }
    public DataTable SelectEmpHospitalization(string HSRecId, string EmpID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpHospitalization");

        SqlParameter p_HSRecId = command.Parameters.Add("HSRecId", SqlDbType.BigInt);
        p_HSRecId.Direction = ParameterDirection.Input;
        p_HSRecId.Value = Convert.ToInt32(HSRecId);

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpID;

        objDC.CreateDSFromProc(command, "EmpHospitalization");
        return objDC.ds.Tables["EmpHospitalization"];
    }

    public DataTable SelectEmpAndFamily(string EmpID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpAndFamilymMembers");
        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;
        objDC.CreateDSFromProc(command, "EmpAndFamily");
        return objDC.ds.Tables["EmpAndFamily"];
    }
#endregion

    #region Edited By Sulata
    //*Sulata
    public void InsertUser(UserCreation User, string IsUpdate, string IsDelete)
    {
        // sproc functionality
        SqlCommand command = new SqlCommand("proc_Insert_User");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_USERID = command.Parameters.Add("USERID", SqlDbType.Char);
        p_USERID.Direction = ParameterDirection.Input;
        p_USERID.Value = User.USERID;

        SqlParameter p_PASSWORD = command.Parameters.Add("PASSWORD", SqlDbType.Char);
        p_PASSWORD.Direction = ParameterDirection.Input;
        p_PASSWORD.Value = User.PASSWORD;

        SqlParameter p_Name = command.Parameters.Add("Name", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = User.Name;

        SqlParameter p_AccountDisabled = command.Parameters.Add("AccountDisabled", SqlDbType.Char);
        p_AccountDisabled.Direction = ParameterDirection.Input;
        p_AccountDisabled.Value = User.AccountDisabled;

        SqlParameter p_ChangePassword = command.Parameters.Add("ChangePassword", SqlDbType.Char);
        p_ChangePassword.Direction = ParameterDirection.Input;
        p_ChangePassword.Value = User.ChangePassword;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = User.EmpId;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = User.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = User.InsertedDate;

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

    //public void InsertUserPrevilege(UserPrevilege User, string UserId, GridView grPriv)
    //{
    //    SqlCommand[] cmd;
    //    cmd = new SqlCommand[grPriv.Rows.Count + 1];

    //    //DELETE FROM DETAILS TABLE        
    //    cmd[0] = new SqlCommand("proc_Delete_UserPrivs");
    //    cmd[0].CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_USERID = cmd[0].Parameters.Add("USERID", SqlDbType.Char);
    //    p_USERID.Direction = ParameterDirection.Input;
    //    p_USERID.Value = UserId;

    //    int i = 1;

    //    foreach (GridViewRow tt in grPriv.Rows)
    //    {
    //        //string a = tt.Cells[0].Text;
    //        Boolean chkAll = Convert.ToBoolean(((CheckBox)tt.Cells[1].FindControl("chkAll")).Checked);
    //        string All = "";
    //        if (chkAll == true)
    //            All = "Y";
    //        else
    //            All = "N";

    //        string pagviewid = Convert.ToString(grPriv.DataKeys[tt.RowIndex].Value);
    //        cmd[i] = InsertUserPrevDetails(User, tt, UserId, pagviewid, All);
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
    //public SqlCommand InsertUserPrevDetails(UserPrevilege User, GridViewRow grow, string UserId, string ScreenId, string A)
    //{
    //    //INSERT INTO DETAILS TABLE
    //    SqlCommand cmd = new SqlCommand("proc_INSERT_UserPrivs");
    //    cmd.CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_USERID = cmd.Parameters.Add("USERID", SqlDbType.Char);
    //    p_USERID.Direction = ParameterDirection.Input;
    //    p_USERID.Value = UserId;

    //    SqlParameter p_ScreenId = cmd.Parameters.Add("Screen_Id", SqlDbType.Char);
    //    p_ScreenId.Direction = ParameterDirection.Input;
    //    p_ScreenId.Value = ScreenId;

    //    SqlParameter p_A = cmd.Parameters.Add("A", SqlDbType.Char);
    //    p_A.Direction = ParameterDirection.Input;
    //    p_A.Value = A;

    //    SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy.Direction = ParameterDirection.Input;
    //    p_InsertedBy.Value = "Admin";

    //    SqlParameter p_InsertedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.Value = "01/01/2009";

    //    return cmd;
    //}

    public void UpdatePassword(UserCreation User)
    {
        // sproc functionality
        SqlCommand command = new SqlCommand("proc_UPDATE_Password");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_USERID = command.Parameters.Add("USERID", SqlDbType.Char);
        p_USERID.Direction = ParameterDirection.Input;
        p_USERID.Value = User.USERID;

        SqlParameter p_PASSWORD = command.Parameters.Add("PASSWORD", SqlDbType.Char);
        p_PASSWORD.Direction = ParameterDirection.Input;
        p_PASSWORD.Value = User.PASSWORD;

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

    //public void InsertUserAssignment(UserAssignment User, string UserId, string DivisionId, string SBUId, GridView grDept)
    //{
    //    SqlCommand[] cmd;
    //    //cmd = new SqlCommand[Cnt + 1];
    //    cmd = new SqlCommand[grDept.Rows.Count + 1];
    //    //DELETE FROM DETAILS TABLE        
    //    cmd[0] = new SqlCommand("proc_Delete_LeavePakDet");
    //    cmd[0].CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_UserId = cmd[0].Parameters.Add("UserId", SqlDbType.Char);
    //    p_UserId.Direction = ParameterDirection.Input;
    //    p_UserId.Value = UserId;

    //    SqlParameter p_DivisionId = cmd[0].Parameters.Add("DivisionId", SqlDbType.BigInt);
    //    p_DivisionId.Direction = ParameterDirection.Input;
    //    p_DivisionId.Value = DivisionId;

    //    SqlParameter p_SBUId = cmd[0].Parameters.Add("SBUId", SqlDbType.BigInt);
    //    p_SBUId.Direction = ParameterDirection.Input;
    //    p_SBUId.Value = SBUId;

    //    int i = 1;

    //    foreach (GridViewRow grView in grDept.Rows)
    //    {
    //        string a = grView.Cells[0].Text;
    //        string Deptid = Convert.ToString(grDept.DataKeys[grView.RowIndex].Value);
    //        Boolean chkAll = Convert.ToBoolean(((CheckBox)grView.Cells[1].FindControl("chkAll")).Checked);
    //        string All = "";
    //        if (chkAll == true)
    //        {
    //            All = "Y";
    //            string CombineId = DivisionId + "|" + SBUId + "|" + Deptid;
    //            cmd[i] = InsertUserAssignDetails(User, grView, UserId, DivisionId, SBUId, CombineId, Deptid);
    //        }
    //        else
    //            All = "N";
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

    //public SqlCommand InsertUserAssignDetails(UserAssignment User, GridViewRow grow, string UserId, string DivisionId, string SBUId, string CombineId, string DeptId)
    //{
    //    //INSERT INTO DETAILS TABLE
    //    SqlCommand cmd = new SqlCommand("proc_Insert_UserAssignment");
    //    cmd.CommandType = CommandType.StoredProcedure;

    //    SqlParameter p_USERID = cmd.Parameters.Add("USERID", SqlDbType.Char);
    //    p_USERID.Direction = ParameterDirection.Input;
    //    p_USERID.Value = UserId;

    //    SqlParameter p_DivisionId = cmd.Parameters.Add("DivisionId", SqlDbType.BigInt);
    //    p_DivisionId.Direction = ParameterDirection.Input;
    //    p_DivisionId.Value = DivisionId;

    //    SqlParameter p_SBUId = cmd.Parameters.Add("SBUId", SqlDbType.BigInt);
    //    p_SBUId.Direction = ParameterDirection.Input;
    //    p_SBUId.Value = SBUId;

    //    SqlParameter p_DeptId = cmd.Parameters.Add("DeptId", SqlDbType.BigInt);
    //    p_DeptId.Direction = ParameterDirection.Input;
    //    p_DeptId.Value = DeptId;

    //    SqlParameter p_CombineId = cmd.Parameters.Add("CombineId", SqlDbType.VarChar);
    //    p_CombineId.Direction = ParameterDirection.Input;
    //    p_CombineId.Value = CombineId;

    //    SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
    //    p_InsertedBy.Direction = ParameterDirection.Input;
    //    p_InsertedBy.Value = "Admin";

    //    SqlParameter p_InsertedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.Value = "01/01/2009";

    //    return cmd;
    //}

    public DataTable SelectEmployee(string EmpID)
    {
        if (objDC.ds.Tables["EmpInfo"] != null)
        {
            objDC.ds.Tables["EmpInfo"].Clear();
            objDC.ds.Tables["EmpInfo"].Dispose();
        }
        SqlCommand command = new SqlCommand("proc_Select_EmpInfo");
        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpID;
        objDC.CreateDSFromProc(command, "EmpInfo");
        return objDC.ds.Tables["EmpInfo"];
    }


    public DataTable SelectEmployee(string EmpID, string EmpStatus)
    {
        if (objDC.ds.Tables["EmpInfoWithEmpStatus"] != null)
        {
            objDC.ds.Tables["EmpInfoWithEmpStatus"].Clear();
            objDC.ds.Tables["EmpInfoWithEmpStatus"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_select_EmpInfoWithEmpStatus");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpID;

        SqlParameter p_EmpStatus = command.Parameters.Add("EmpStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = EmpStatus;

        objDC.CreateDSFromProc(command, "EmpInfoWithEmpStatus");
        return objDC.ds.Tables["EmpInfoWithEmpStatus"];
    }


    public DataTable SelectEmpList( string EmpStatus)
    {

        string strSql = " select e.EmpId,e.FullName,d.DesigName  from EmpInfo e left join Designation d on e.DesigId=d.DesigId"
		                +" WHERE  e.EmpStatus='" +EmpStatus+"'  order by e.EmpId";
        return objDC.CreateDT(strSql, "EmpList");

        //SqlCommand command = new SqlCommand("proc_select_EmpInfoList");
        //SqlParameter p_EmpStatus = command.Parameters.Add("EmpStatus", SqlDbType.Char);
        //p_EmpStatus.Direction = ParameterDirection.Input;
        //p_EmpStatus.Value = EmpStatus;

        //objDC.CreateDSFromProc(command, "EmpInfoWithEmpStatus");
        //return objDC.ds.Tables["EmpInfoWithEmpStatus"];
    }



    //Employee Type permament or Contractual
    public DataTable SelectEmpTypeWsEmp(string EmpTypeID)
    {
        if (objDC.ds.Tables["EmpInfoWithEmpType"] != null)
        {
            objDC.ds.Tables["EmpInfoWithEmpType"].Clear();
            objDC.ds.Tables["EmpInfoWithEmpType"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_select_EmpTypeWsEmp");

        SqlParameter p_EmpStatus = command.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = Convert.ToInt32(EmpTypeID);

        objDC.CreateDSFromProc(command, "EmpTypeWsEmp");
        return objDC.ds.Tables["EmpTypeWsEmp"];
    }
    //proc_select_EmpInfoWithEmpStatus

    public DataTable SelectDeptWiseLocation(Int32 DeptID)
    {
        SqlCommand command = new SqlCommand("proc_Select_LocationWiseDept");
        SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
        p_DeptID.Direction = ParameterDirection.Input;
        p_DeptID.Value = DeptID;
        objDC.CreateDSFromProc(command, "LocationWiseDept");
        return objDC.ds.Tables["LocationWiseDept"];
    }

    #endregion 

    #region Edited by Ashique
    public DataTable GetDivision()
    {

        SqlCommand command = new SqlCommand("proc_Get_Division");
        //command.Parameters.Add("p_recordset1", OracleType.Cursor).Direction = ParameterDirection.Output;
        objDC.CreateDSFromProc(command, "Get_Division");
        return objDC.ds.Tables["Get_Division"];
    }


    //public DataTable SelectSBUWiseDivision(Int32 DivisionID)
    //{
    //    SqlCommand command = new SqlCommand("proc_Select_SBUWiseDivision");
    //    SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
    //    p_DivisionID.Direction = ParameterDirection.Input;
    //    p_DivisionID.Value = DivisionID;
    //    objDC.CreateDSFromProc(command, "DivisionWiseSBU");
    //    return objDC.ds.Tables["DivisionWiseSBU"];
    //}


    //public DataTable SelectDeptWiseSBU(Int32 SBUID)
    //{
    //    SqlCommand command = new SqlCommand("proc_Select_DeptWiseSBU");
    //    SqlParameter p_SBUId = command.Parameters.Add("SBUId", SqlDbType.BigInt);
    //    p_SBUId.Direction = ParameterDirection.Input;
    //    p_SBUId.Value = SBUID;
    //    objDC.CreateDSFromProc(command, "DeptWiseSBU");
    //    return objDC.ds.Tables["DeptWiseSBU"];
    //}
    #endregion

    #region Edited By ANol for Institute Certificate,Result Master Table Setup
   
   
    // SELECT AWARD
    public DataTable SelectAward(Int32 EventId)
    {
        SqlCommand command = new SqlCommand("proc_Select_Award");
        SqlParameter p_EventId = command.Parameters.Add("AwardId", SqlDbType.BigInt);
        p_EventId.Direction = ParameterDirection.Input;
        p_EventId.Value = EventId;
        objDC.CreateDSFromProc(command, "AwardInfo");
        return objDC.ds.Tables["AwardInfo"];
    }
    // SELECT SPOT BONUS AWARD
    public DataTable SelectSBAward(Int32 SBEventId)
    {
        SqlCommand command = new SqlCommand("proc_Select_SBAward");
        SqlParameter p_SBEventId = command.Parameters.Add("SBAwardId", SqlDbType.BigInt);
        p_SBEventId.Direction = ParameterDirection.Input;
        p_SBEventId.Value = SBEventId;
        objDC.CreateDSFromProc(command, "SBAwardInfo");
        return objDC.ds.Tables["SBAwardInfo"];
    }
  
    #endregion

    #region Company

    public DataTable SelectDivision()
    {
        string strSQL = "SELECT * FROM DivisionList WHERE ISACTIVE='Y' and IsDeleted='N'";
        return objDC.CreateDT(strSQL, "DivisionList");
    }

    //public DataTable GetCompanyList(Int32 sDivisionId)
    //{
    //    string strSQL = "SELECT * FROM DivisionList WHERE ISACTIVE='Y' and DivisionId=@DivisionId ";
    //    SqlCommand cmd = new SqlCommand(strSQL);

    //    SqlParameter p_ReligionName = cmd.Parameters.Add("DivisionId", SqlDbType.BigInt);
    //    p_ReligionName.Direction = ParameterDirection.Input;
    //    p_ReligionName.Value = sDivisionId;

    //    objDC.CreateDSFromProc(cmd, "DivisionList");
    //    return objDC.ds.Tables["DivisionList"];

    //   // return objDC.GetScalarVal(cmd);


    //    //string strSQL = "SELECT * FROM DivisionList WHERE ISACTIVE='Y' and DivisionId=@DivisionId";
    //    //return objDC.CreateDT(strSQL, "DEAList");
    //}

    public void InsertUpCompanyList(Company clsCompany, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_Company");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("DivisionId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = clsCompany.DivisionId;

        SqlParameter p_Name = command.Parameters.Add("DivisionName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = clsCompany.DivisionName;

         SqlParameter p_SName = command.Parameters.Add("DivisionShortName", SqlDbType.VarChar);
        p_SName.Direction = ParameterDirection.Input;
        p_SName.Value = clsCompany.DivisionShortName;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = clsCompany.IsActive;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = clsCompany.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = clsCompany.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = clsCompany.InsertedDate;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = clsCompany.LastUpdatedFrom;


      

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

    public DataTable SelectRegion()
    {
        string strSQL = "SELECT * FROM RegionList WHERE ISACTIVE='Y' and IsDeleted='N'";
        return objDC.CreateDT(strSQL, "RegionList");
    }

    public void InserRegionList(clsCommonSetup objCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_Region");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("RegionId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = objCommon.ID;

        SqlParameter p_Name = command.Parameters.Add("RegionName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = objCommon.Name;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = objCommon.IsActive;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = objCommon.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = objCommon.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = objCommon.InsertedDate;

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

    #region ClenicType/Office

    public DataTable GetClinicTypeList()
    {
        string strSQL = "SELECT * FROM ClinicTypeList WHERE ISACTIVE='Y' and IsDeleted='N'";
        return objDC.CreateDT(strSQL, "ClinicTypeList");
    }


    public void InsertUpClinicTypeList(clsClinicType clsClinic, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_ClinicType");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("ClinicTypeId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = clsClinic.ClinicTypeId;

        SqlParameter p_Name = command.Parameters.Add("ClinicTypeName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = clsClinic.ClinicTypeName;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = clsClinic.IsActive;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = clsClinic.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = clsClinic.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = clsClinic.InsertedDate;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = clsClinic.LastUpdatedFrom;

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

    #region Clinic

    public DataTable SelectClinic(string strIsActive)
    {
        string strSQL = "";
        if (strIsActive=="A")
         strSQL = "SELECT CL.*,B.BankName,B.BranchName FROM ClinicList CL LEFT OUTER JOIN BankList B ON CL.BankCode=B.BankCode "
            + "  AND CL.RoutingNo=B.RoutingNo WHERE CL.IsDeleted='N' ORDER BY ClinicName";
        else
        strSQL= "SELECT CL.*,B.BankName,B.BranchName FROM ClinicList CL LEFT OUTER JOIN BankList B ON CL.BankCode = B.BankCode "
               + "  AND CL.RoutingNo=B.RoutingNo WHERE CL.ISACTIVE='Y' and CL.IsDeleted='N' ORDER BY ClinicName"; 

        return objDC.CreateDT(strSQL, "ClinicList");
    }

    public void InsertUpClinicList(Clinic clsClinic, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_Clinic");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("ClinicId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = clsClinic.ClinicId;

        SqlParameter p_Name = command.Parameters.Add("ClinicName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = clsClinic.ClinicName;

        SqlParameter p_SName = command.Parameters.Add("ClinicShortName", SqlDbType.VarChar);
        p_SName.Direction = ParameterDirection.Input;
        p_SName.Value = clsClinic.ClinicShortName;
        
        SqlParameter p_CTypeId = command.Parameters.Add("ClinicTypeId", DBNull.Value);
        p_CTypeId.Direction = ParameterDirection.Input;
        p_CTypeId.IsNullable = true;
        if (clsClinic.ClinicTypeId != "")
            p_CTypeId.Value = clsClinic.ClinicTypeId;

        SqlParameter p_SunCode = command.Parameters.Add("SunCode", SqlDbType.VarChar);
        p_SunCode.Direction = ParameterDirection.Input;
        p_SunCode.Value = clsClinic.SunCode;

        SqlParameter p_BankAccNo = command.Parameters.Add("BankAccNo", SqlDbType.VarChar);
        p_BankAccNo.Direction = ParameterDirection.Input;
        p_BankAccNo.Value = clsClinic.BankAccNo;

        SqlParameter p_BankCode = command.Parameters.Add("BankCode", SqlDbType.Char);
        p_BankCode.Direction = ParameterDirection.Input;
        p_BankCode.Value = clsClinic.BankCode;

        SqlParameter p_RoutingNo = command.Parameters.Add("RoutingNo", SqlDbType.Char);
        p_RoutingNo.Direction = ParameterDirection.Input;
        p_RoutingNo.Value = clsClinic.RoutingNo;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = clsClinic.IsActive;

        SqlParameter p_isDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = clsClinic.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = clsClinic.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = clsClinic.InsertedDate;

        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_LastUpdatedFrom = command.Parameters.Add("LastUpdatedFrom", SqlDbType.VarChar);
        p_LastUpdatedFrom.Direction = ParameterDirection.Input;
        p_LastUpdatedFrom.Value = clsClinic.LastUpdatedFrom;

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

    #region Project
    public DataTable SelectProject()
    {
        string strSQL = "SELECT * FROM ProjectList WHERE ISACTIVE='Y' and IsDeleted='N'";
        return objDC.CreateDT(strSQL, "ProjectList");
    }

    public void InsertUpProject(GridView grBenefits, clsProject clsPrj, string IsUpdate, string IsDelete)
    {

        SqlCommand[] command = new SqlCommand[grBenefits.Rows.Count + 1];
        command[0] = new SqlCommand("proc_Insert_Project");
        command[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command[0].Parameters.Add("ProjectId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = clsPrj.ProjectId;

        SqlParameter p_Name = command[0].Parameters.Add("ProjectName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = clsPrj.ProjectName;

        SqlParameter p_SName = command[0].Parameters.Add("ProjectCode", SqlDbType.VarChar);
        p_SName.Direction = ParameterDirection.Input;
        p_SName.Value = clsPrj.ProjectCode;

        SqlParameter p_StartDate = command[0].Parameters.Add("StartDate", DBNull.Value);
        p_StartDate.Direction = ParameterDirection.Input;
        p_StartDate.IsNullable = true;
        if (clsPrj.StartDate != "")
            p_StartDate.Value = clsPrj.StartDate;

        SqlParameter p_EndDate = command[0].Parameters.Add("EndDate", DBNull.Value);
        p_EndDate.Direction = ParameterDirection.Input;
        p_EndDate.IsNullable = true;
        if (clsPrj.EndDate != "")
            p_EndDate.Value = clsPrj.EndDate;

        SqlParameter p_WeekEndID = command[0].Parameters.Add("WeekEndID", SqlDbType.BigInt);
        p_WeekEndID.Direction = ParameterDirection.Input;
        p_WeekEndID.Value = clsPrj.WeekEndID;

        SqlParameter p_IncrementType = command[0].Parameters.Add("IncrementType", SqlDbType.VarChar);
        p_IncrementType.Direction = ParameterDirection.Input;
        p_IncrementType.Value = clsPrj.IncrementType;

        SqlParameter p_IncrementMonth = command[0].Parameters.Add("IncrementMonth", SqlDbType.BigInt);
        p_IncrementMonth.Direction = ParameterDirection.Input;
        p_IncrementMonth.Value = clsPrj.IncrementMonth;

        SqlParameter p_IncrementAfter = command[0].Parameters.Add("IncrementAfter", DBNull.Value);
        p_IncrementAfter.Direction = ParameterDirection.Input;
        if (clsPrj.IncrementAfter != null)
            p_IncrementAfter.Value = clsPrj.IncrementAfter;
        else
            p_IncrementAfter.Value = 0;

        SqlParameter p_IsActive = command[0].Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = clsPrj.IsActive;

        SqlParameter p_isDeleted = command[0].Parameters.Add("IsDeleted", SqlDbType.Char);
        p_isDeleted.Direction = ParameterDirection.Input;
        p_isDeleted.Value = clsPrj.IsDeleted;

        SqlParameter p_InsertedBy = command[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = clsPrj.InsertedBy;

        SqlParameter p_InsertedDate = command[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = clsPrj.InsertedDate;

        SqlParameter p_IsUpdate = command[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command[0].Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        // Insert Into Project Benefit
        string strEmpTypeId;
        CheckBox chkIsPF;
        int i = 1;
        foreach (GridViewRow tt in grBenefits.Rows)
        {
            CheckBox chBox = new CheckBox();
            chBox = (CheckBox)tt.Cells[0].FindControl("chkBox");
            if (chBox.Checked == true)
            {
                HiddenField hfEmpTypeID = (HiddenField)tt.Cells[3].FindControl("hfEmpTypeId");
                strEmpTypeId = hfEmpTypeID.Value.ToString();

                CheckBox chIsPFControl = new CheckBox();
                chkIsPF = (CheckBox)tt.Cells[2].FindControl("chIsPFControl");




                command[i] = this.InsertIntoProjectBenefit(clsPrj.ProjectId, strEmpTypeId, chkIsPF.Checked == true ? "Y" : "N");
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
    public SqlCommand InsertIntoProjectBenefit(string strProjId, string strEmpTypeId, string strIsPF)
    {
        SqlCommand command = new SqlCommand("proc_Insert_ProjectWsBenefit");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("ProjectId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = strProjId;

        SqlParameter p_EmpTypeId = command.Parameters.Add("EmpTypeId", SqlDbType.BigInt);
        p_EmpTypeId.Direction = ParameterDirection.Input;
        p_EmpTypeId.Value = strEmpTypeId;

        SqlParameter p_IsPF = command.Parameters.Add("IsPF", SqlDbType.Char);
        p_IsPF.Direction = ParameterDirection.Input;
        p_IsPF.Value = strIsPF;

        //SqlParameter p_IsGratuity = command.Parameters.Add("IsGratuity", SqlDbType.Char);
        //p_IsGratuity.Direction = ParameterDirection.Input;
        //p_IsGratuity.Value = clsPrj.IsGratuity;

        //SqlParameter p_IsEOC = command.Parameters.Add("IsEOC", SqlDbType.Char);
        //p_IsEOC.Direction = ParameterDirection.Input;
        //p_IsEOC.Value = clsPrj.IsEOC;

        //SqlParameter p_IsLE = command.Parameters.Add("IsLE", SqlDbType.Char);
        //p_IsLE.Direction = ParameterDirection.Input;
        //p_IsLE.Value = clsPrj.IsLE;

        //SqlParameter p_IsInsurance = command.Parameters.Add("IsInsurance", SqlDbType.Char);
        //p_IsInsurance.Direction = ParameterDirection.Input;
        //p_IsInsurance.Value = clsPrj.IsInsurance;
        
        return command;

    }


    #endregion


    #region Select Queries From Tables By store procedure
    //Select Option Bag

    public DataTable SelectOptionBag(string OptId)
    {
        SqlCommand command = new SqlCommand("proc_Select_OptionBag");
       
        SqlParameter p_OptId = command.Parameters.Add("OptID", SqlDbType.Char);
        p_OptId.Direction = ParameterDirection.Input;
        p_OptId.Value = OptId; 

        objDC.CreateDSFromProc(command, "OptionBag");
        return objDC.ds.Tables["OptionBag"];
    }

    #endregion

    public DataTable Select_GradeLevel_MinMaxSal(Int32 GradeId)
    {
        SqlCommand command = new SqlCommand("Select_Grade_MinMaxSal");
        SqlParameter p_GradeID = command.Parameters.Add("GradeId", SqlDbType.BigInt);
        p_GradeID.Direction = ParameterDirection.Input;
        p_GradeID.Value = GradeId;
        objDC.CreateDSFromProc(command, "Grdlvl");
        return objDC.ds.Tables["Grdlvl"];
    }
    
    public void InsertGradeLevel(GradeLevel objGradeLevel, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_GradeLevelList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_GradeLevelId = command.Parameters.Add("GradeLevelId", SqlDbType.BigInt);
        p_GradeLevelId.Direction = ParameterDirection.Input;
        p_GradeLevelId.Value = objGradeLevel.GradeLevelId;

        SqlParameter p_GradeID = command.Parameters.Add("GradeID", DBNull.Value);
        p_GradeID.Direction = ParameterDirection.Input;
        p_GradeID.IsNullable = true;
        if (objGradeLevel.GradeID != "")
            p_GradeID.Value = objGradeLevel.GradeID;

        SqlParameter p_GradeLevelName = command.Parameters.Add("GradeLevelName", SqlDbType.VarChar);
        p_GradeLevelName.Direction = ParameterDirection.Input;
        p_GradeLevelName.Value = objGradeLevel.GradeLevelName;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = objGradeLevel.IsActive;

        SqlParameter p_BasicMin = command.Parameters.Add("BasicMin", DBNull.Value);
        p_BasicMin.Direction = ParameterDirection.Input;
        p_BasicMin.IsNullable = true;
        if (objGradeLevel.BasicMin != "")
            p_BasicMin.Value = objGradeLevel.BasicMin;

        SqlParameter p_BasicMax = command.Parameters.Add("BasicMax", DBNull.Value);
        p_BasicMax.Direction = ParameterDirection.Input;
        p_BasicMax.IsNullable = true;
        if (objGradeLevel.BasicMax != "")
            p_BasicMax.Value = objGradeLevel.BasicMax;

        SqlParameter p_TransportAmnt = command.Parameters.Add("TransportAmnt", DBNull.Value);
        p_TransportAmnt.Direction = ParameterDirection.Input;
        p_TransportAmnt.IsNullable = true;
        if (objGradeLevel.TransportAmnt != "")
            p_TransportAmnt.Value = objGradeLevel.TransportAmnt;

        SqlParameter p_IsDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_IsDeleted.Direction = ParameterDirection.Input;
        p_IsDeleted.Value = objGradeLevel.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = objGradeLevel.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = objGradeLevel.InsertedDate;

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

    // Insert or Update  or Delete Data of Action table
    public void InsertAction(clsAction Act, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_ActionList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_ActionId = command.Parameters.Add("ActionId", SqlDbType.BigInt);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.Value = Act.ActionId;

        SqlParameter p_ActionName = command.Parameters.Add("ActionName", SqlDbType.VarChar);
        p_ActionName.Direction = ParameterDirection.Input;
        p_ActionName.Value = Act.ActionName;

        SqlParameter p_ActionDesc = command.Parameters.Add("ActionDesc", SqlDbType.VarChar);
        p_ActionDesc.Direction = ParameterDirection.Input;
        p_ActionDesc.Value = Act.ActionDesc;

        SqlParameter p_ActionType = command.Parameters.Add("ActionType", SqlDbType.Char);
        p_ActionType.Direction = ParameterDirection.Input;
        p_ActionType.Value = Act.ActionType;

        SqlParameter p_ActionNature = command.Parameters.Add("ActionNature", SqlDbType.Char);
        p_ActionNature.Direction = ParameterDirection.Input;
        p_ActionNature.Value = Act.ActionNature;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = Act.IsActive;

        SqlParameter p_IsDeleted = command.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_IsDeleted.Direction = ParameterDirection.Input;
        p_IsDeleted.Value = Act.IsDeleted;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = Act.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = Act.InsertedDate;

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

    //Select from ActionList
    public DataTable SelectAction(Int32 ActionId, string ActionNature)
    {
        SqlCommand command = new SqlCommand("proc_Select_ActionList");

        SqlParameter p_ActionId = command.Parameters.Add("ActionId", SqlDbType.BigInt);
        p_ActionId.Direction = ParameterDirection.Input;
        p_ActionId.Value = ActionId;

        SqlParameter p_ActionNature = command.Parameters.Add("ActionNature", SqlDbType.Char);
        p_ActionNature.Direction = ParameterDirection.Input;
        p_ActionNature.Value = ActionNature;

        objDC.CreateDSFromProc(command, "ActionList");
        return objDC.ds.Tables["ActionList"];
    }

    public DataTable SelectGradeLevelddl(Int32 GradeLevelId, Int32 GradeId)
    {
        SqlCommand command = new SqlCommand("proc_Select_GradeLevelList");

        SqlParameter p_DivisionID = command.Parameters.Add("GradeLevelId", SqlDbType.BigInt);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = GradeLevelId;

        SqlParameter p_GradeID = command.Parameters.Add("GradeID", SqlDbType.BigInt);
        p_GradeID.Direction = ParameterDirection.Input;
        p_GradeID.Value = GradeId;

        objDC.CreateDSFromProc(command, "GradeLevelddl");
        return objDC.ds.Tables["GradeLevelddl"];
    }

    public DataTable SelectTypeWiseAction(string strActionType)
    {
        string strSQL = "SELECT ActionId,ActionName FROM ActionList WHERE ISACTIVE='Y' "
        + " AND ActionType='" + strActionType + "'";

        return objDC.CreateDT(strSQL, "ActionList");
    }

    //Select Confrimation information
    public DataTable SelectConfirmation(Int32 ConfirmId, string EmpId)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpConfirmationLog");
        SqlParameter p_ConfirmId = command.Parameters.Add("ConfirmId", SqlDbType.BigInt);
        p_ConfirmId.Direction = ParameterDirection.Input;
        p_ConfirmId.Value = ConfirmId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        objDC.CreateDSFromProc(command, "ConfirmationLog");
        return objDC.ds.Tables["ConfirmationLog"];
    }

    public void UpdateProfessionalDegree(GridView grLocation, string UserID)
    {
        SqlCommand command = new SqlCommand();
        foreach (GridViewRow gRow in grLocation.Rows)
        {
            CheckBox chkBox = (CheckBox)gRow.FindControl("chkBox");
            decimal degreeID = Convert.ToDecimal(grLocation.DataKeys[gRow.RowIndex].Values[0].ToString());  //Convert.ToDecimal(gRow.FindControl("DegreeId"));


            string query = " Update DegreeList set IsProfessional=@IsProfessional where DegreeId=@DegreeId";
            command = new SqlCommand(query);
            command.CommandType = CommandType.Text;

            SqlParameter p_DegreeId = command.Parameters.Add("DegreeId", SqlDbType.Decimal);
            p_DegreeId.Direction = ParameterDirection.Input;
            p_DegreeId.Value = degreeID;

            SqlParameter p_IsProfessional = command.Parameters.Add("IsProfessional", SqlDbType.Char);
            p_IsProfessional.Direction = ParameterDirection.Input;
            p_IsProfessional.Value = (chkBox.Checked == true ? "Y" : "N");

            try
            {
                //objDC.MakeTransaction(command);ExecuteQueryRetMsg
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
    }

    public DataTable GetFestivalDate(string StartDate, string EndDate)
    {
        SqlCommand command = new SqlCommand("proc_Get_FestivalDate");

        SqlParameter p_InsertedDate = command.Parameters.Add("StartDate", DBNull.Value);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.IsNullable = true;
        if (string.IsNullOrEmpty(StartDate) == false)
            p_InsertedDate.Value = StartDate;
        else
            p_InsertedDate.Value = "";

        SqlParameter p_EndDate = command.Parameters.Add("EndDate", DBNull.Value);
        p_EndDate.Direction = ParameterDirection.Input;
        p_EndDate.IsNullable = true;
        if (string.IsNullOrEmpty(EndDate) == false)
            p_EndDate.Value = EndDate;
        else
            p_EndDate.Value = "";
        objDC.CreateDSFromProc(command, "GetFestivalDate");
        return objDC.ds.Tables["GetFestivalDate"];
    }

    public DataTable SelectDEAList(Int32 DeptID)
    {
        string strSQL = "SELECT * FROM DEAList WHERE ISACTIVE='Y'";
        return objDC.CreateDT(strSQL, "DEAList");
    }

    public void InsertDEAList(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        SqlCommand command = new SqlCommand("proc_Insert_DEAList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("DEAId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = clsCommon.ID;

        SqlParameter p_Name = command.Parameters.Add("DEAName", SqlDbType.VarChar);
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

    public DataTable SelectTrainingName(Int32 TrainingID,string strIsActive)
    {
        SqlCommand command = new SqlCommand("proc_Select_Traininglist");
        SqlParameter p_TrainingID = command.Parameters.Add("TrainingID", SqlDbType.BigInt);
        p_TrainingID.Direction = ParameterDirection.Input;
        p_TrainingID.Value = TrainingID;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = strIsActive;

        objDC.CreateDSFromProc(command, "Traininglist");
        return objDC.ds.Tables["Traininglist"];
    }

    public DataTable SelectLearningArea(Int32 LAreaId)
    {
        SqlCommand command = new SqlCommand("proc_Select_LearningAreaList");
        SqlParameter p_LAreaId = command.Parameters.Add("LAreaId", SqlDbType.BigInt);
        p_LAreaId.Direction = ParameterDirection.Input;
        p_LAreaId.Value = LAreaId;
        objDC.CreateDSFromProc(command, "LAreaIdlist");
        return objDC.ds.Tables["LAreaIdlist"];
    }
    //SelectResourcePersonList

    public DataTable SelectResourcePersonList(Int32 ResourcePersonId,string strActive)
    {
        SqlCommand command = new SqlCommand("proc_Select_ResourcePersonList");
        SqlParameter p_ResourcePersonId = command.Parameters.Add("ResourcePersonId", SqlDbType.BigInt);
        p_ResourcePersonId.Direction = ParameterDirection.Input;
        p_ResourcePersonId.Value = ResourcePersonId;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = strActive;

        objDC.CreateDSFromProc(command, "ResourcePersonIdlist");
        return objDC.ds.Tables["ResourcePersonIdlist"];
    }

    public DataTable SelectTrainingServiceList(Int32 TraServiceID, string EmpID)
    {
        SqlCommand command = new SqlCommand("proc_Select_TrainingServiceList");
        SqlParameter p_TraServiceID = command.Parameters.Add("TraServiceID", SqlDbType.Decimal);
        p_TraServiceID.Direction = ParameterDirection.Input;
        p_TraServiceID.Value = TraServiceID;

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        objDC.CreateDSFromProc(command, "TraServicelist");
        return objDC.ds.Tables["TraServicelist"];
    }

    public DataTable SelectEmpInfoHRAction(string EmpID)
    {
        if (objDC.ds.Tables["tblEmpInfoHrAction"] != null)
        {
            objDC.ds.Tables["tblEmpInfoHrAction"].Rows.Clear();
            objDC.ds.Tables["tblEmpInfoHrAction"].Dispose();
        }
        SqlCommand command = new SqlCommand("proc_Select_EmpInfo_HRAction");
        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;
        objDC.CreateDSFromProc(command, "tblEmpInfoHrAction");
        return objDC.ds.Tables["tblEmpInfoHrAction"];
    }

    public DataTable SelectUnitWiseDepartment(Int32 UnitId)
    {
        SqlCommand command = new SqlCommand("proc_select_UnitWiseDepartment");

        SqlParameter p_UnitId = command.Parameters.Add("UnitId", SqlDbType.BigInt);
        p_UnitId.Direction = ParameterDirection.Input;
        p_UnitId.Value = UnitId;

        objDC.CreateDSFromProc(command, "tblUnitWiseDepartment");
        return objDC.ds.Tables["tblUnitWiseDepartment"];
    }

    public DataTable SelectMSBFileList(Int32 FileId)
    {
        string strSQL = @"SELECT * FROM FileDetails";
        if (FileId.ToString() != "0")
            strSQL = @"SELECT * FROM FileDetails where FileId=" + FileId;
        return objDC.CreateDT(strSQL, "DEAList");
    }

    public void InsertMSBFILEList(string FileId, string IsUpdate, string IsDelete, string fileName, string FileLength, string fileExtension, string fileSavePath)
    {

        SqlCommand command = new SqlCommand("proc_Insert_FileDetails");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("FileId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = FileId;

        SqlParameter p_Name = command.Parameters.Add("fileName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = fileName;

        SqlParameter p_FileLength = command.Parameters.Add("FileSize", SqlDbType.VarChar);
        p_FileLength.Direction = ParameterDirection.Input;
        p_FileLength.Value = FileLength;

        SqlParameter p_fileExtension = command.Parameters.Add("FileExtension", SqlDbType.VarChar);
        p_fileExtension.Direction = ParameterDirection.Input;
        p_fileExtension.Value = fileExtension;

        SqlParameter p_fileSavePath = command.Parameters.Add("FilePath", SqlDbType.VarChar);
        p_fileSavePath.Direction = ParameterDirection.Input;
        p_fileSavePath.Value = fileSavePath;



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
    //InsertViewLogInfo(ViewID,Session["USERID"].ToString(),FileName, Common.SetDateTime(DateTime.Now.ToString()));
    public void InsertViewLogInfo(string sViewID, string sEmpId, string fileName, string InsertedDate)
    {

        SqlCommand command = new SqlCommand("proc_Insert_InsertViewLogInfo");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("ViewID", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = sViewID;
        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = sEmpId;

        SqlParameter p_Name = command.Parameters.Add("DocName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = fileName;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = InsertedDate;

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

    public DataTable SelectLocationCategory(Int16 LocCatId )
    {
        string strSQL = "SELECT * FROM LocationCategory WHERE ISACTIVE='Y' and IsDeleted='N'";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_UnitId = cmd.Parameters.Add("LocCatId", SqlDbType.BigInt);
        p_UnitId.Direction = ParameterDirection.Input;
        p_UnitId.Value = LocCatId;

        return objDC.CreateDT(cmd, "LocationCategory");
    }

    public DataTable SelectTaxRegion(Int16 RegionId)
    {
        string strSQL = "SELECT * FROM TaxRegionList WHERE ISACTIVE='Y' and IsDeleted='N' Order By RegionId Desc";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;

        SqlParameter p_UnitId = cmd.Parameters.Add("RegionId", SqlDbType.BigInt);
        p_UnitId.Direction = ParameterDirection.Input;
        p_UnitId.Value = RegionId;

        return objDC.CreateDT(cmd, "TaxRegionList");
    }

    public string GetGradeWsOTEntitlement(Int16 iGradeId)
    {
        string strSQL = "SELECT IsOTEntitle FROM GradeList WHERE GradeId=" + iGradeId;
        SqlCommand cmd = new SqlCommand(strSQL);

        SqlParameter p_GradeId = cmd.Parameters.Add("GradeId", SqlDbType.VarChar);
        p_GradeId.Direction = ParameterDirection.Input;
        p_GradeId.Value = iGradeId;

        return objDC.GetScalarVal(cmd);
    }

    #region Query & Parameter Generator
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
    #endregion
}