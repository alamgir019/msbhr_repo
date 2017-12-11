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
/// Summary description for KPIManager
/// </summary>
public class KPIManager
{

    DBConnector objDC = new DBConnector();

    public void InsertQuarter(clsCommonSetup clsCommon, string strFrom,string strTo, string IsUpdate, string IsDelete)
    {
        // sproc functionality
        SqlCommand command = new SqlCommand("proc_InUp_KPIQuarterList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("QuarterId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = Convert.ToInt32(clsCommon.ID);

        SqlParameter p_Name = command.Parameters.Add("QuarterName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = clsCommon.Name;


        SqlParameter p_FromMonth = command.Parameters.Add("FromMonth", SqlDbType.BigInt);
        p_FromMonth.Direction = ParameterDirection.Input;
        p_FromMonth.Value = Convert.ToInt32(strFrom);

        SqlParameter p_strTo = command.Parameters.Add("ToMonth", SqlDbType.BigInt);
        p_strTo.Direction = ParameterDirection.Input;
        p_strTo.Value = Convert.ToInt32(strTo);

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = clsCommon.IsActive;

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

    public DataTable SelectQuarter(Int32 QuarterId)
    {
        SqlCommand command = new SqlCommand("proc_Select_Quarter");
        SqlParameter p_QuarterId = command.Parameters.Add("QuarterId", SqlDbType.BigInt);
        p_QuarterId.Direction = ParameterDirection.Input;
        p_QuarterId.Value = QuarterId;
        objDC.CreateDSFromProc(command, "Quarter");
        return objDC.ds.Tables["Quarter"];
    }


    public void InsertGroup(clsCommonSetup clsCommon,  string IsUpdate, string IsDelete)
    {
        // sproc functionality
        SqlCommand command = new SqlCommand("proc_InUp_KPIGroupList");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("GroupId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = Convert.ToInt32(clsCommon.ID);

        SqlParameter p_Name = command.Parameters.Add("GroupName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = clsCommon.Name;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = clsCommon.IsActive;

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


    public DataTable SelectGroup(Int32 GroupId)
    {
        SqlCommand command = new SqlCommand("proc_Select_KPIGroupList");
        SqlParameter p_QuarterId = command.Parameters.Add("GroupId", SqlDbType.BigInt);
        p_QuarterId.Direction = ParameterDirection.Input;
        p_QuarterId.Value = GroupId;
        objDC.CreateDSFromProc(command, "GroupList");
        return objDC.ds.Tables["GroupList"];
    }



    public void InsertIndicatirType(clsCommonSetup clsCommon, string IsUpdate, string IsDelete)
    {
        // sproc functionality
        SqlCommand command = new SqlCommand("proc_InUp_KPIIndicatirType");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("IndTypeId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = Convert.ToInt32(clsCommon.ID);

        SqlParameter p_Name = command.Parameters.Add("IndicatirTypeName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = clsCommon.Name;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = clsCommon.IsActive;

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


    public DataTable SelectIndicatirType(Int32 IndTypeId)
    {
        SqlCommand command = new SqlCommand("proc_Select_KPIIndicatirType");
        SqlParameter p_QuarterId = command.Parameters.Add("IndTypeId", SqlDbType.BigInt);
        p_QuarterId.Direction = ParameterDirection.Input;
        p_QuarterId.Value = IndTypeId;
        objDC.CreateDSFromProc(command, "KPIIndicatirType");
        return objDC.ds.Tables["KPIIndicatirType"];
    }



    public void InsertKPI(clsCommonSetup clsCommon, string strGroupID,string strIndicatorID, string IsUpdate, string IsDelete)
    {
        // sproc functionality
        SqlCommand command = new SqlCommand("proc_InUp_KPISetup");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("KpiId", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = Convert.ToInt32(clsCommon.ID);

        SqlParameter p_Name = command.Parameters.Add("KpiName", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = clsCommon.Name;

        SqlParameter p_GroupId = command.Parameters.Add("GroupId", SqlDbType.BigInt);
        p_GroupId.Direction = ParameterDirection.Input;
        p_GroupId.Value = Convert.ToInt32(strGroupID);

        SqlParameter p_IndTypeId = command.Parameters.Add("IndTypeId", SqlDbType.BigInt);
        p_IndTypeId.Direction = ParameterDirection.Input;
        p_IndTypeId.Value = Convert.ToInt32(strIndicatorID);

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = clsCommon.IsActive;

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

    public DataTable SelectKPI(Int32 KpiId)
    {
        SqlCommand command = new SqlCommand("proc_Select_KPISetup");
        SqlParameter p_QuarterId = command.Parameters.Add("KpiId", SqlDbType.BigInt);
        p_QuarterId.Direction = ParameterDirection.Input;
        p_QuarterId.Value = KpiId;
        objDC.CreateDSFromProc(command, "KPIList");
        return objDC.ds.Tables["KPIList"];
    }


    public void InsertScoring(clsCommonSetup clsCommon, string strGroupID, string strQuarterID,string txtYear,string txtMFrom,string txtMTo, string strScore, string IsUpdate, string IsDelete)
    {
        // sproc functionality
        SqlCommand command = new SqlCommand("proc_InUp_KPIScoring");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Id = command.Parameters.Add("ScoringID", SqlDbType.BigInt);
        p_Id.Direction = ParameterDirection.Input;
        p_Id.Value = Convert.ToInt32(clsCommon.ID);

        SqlParameter p_Name = command.Parameters.Add("Rating", SqlDbType.VarChar);
        p_Name.Direction = ParameterDirection.Input;
        p_Name.Value = clsCommon.Name;

        SqlParameter p_GroupId = command.Parameters.Add("GroupId", SqlDbType.BigInt);
        p_GroupId.Direction = ParameterDirection.Input;
        p_GroupId.Value = Convert.ToInt32(strGroupID);

        SqlParameter p_IndTypeId = command.Parameters.Add("QuarterId", SqlDbType.BigInt);
        p_IndTypeId.Direction = ParameterDirection.Input;
        p_IndTypeId.Value = Convert.ToInt32(strQuarterID);

        SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.BigInt);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = Convert.ToInt32(txtYear);

        SqlParameter p_MarksF = command.Parameters.Add("MarksF", SqlDbType.BigInt);
        p_MarksF.Direction = ParameterDirection.Input;
        p_MarksF.Value = Convert.ToInt32(txtMFrom);

        SqlParameter p_MarksTo = command.Parameters.Add("MarksTo", SqlDbType.BigInt);
        p_MarksTo.Direction = ParameterDirection.Input;
        p_MarksTo.Value = Convert.ToInt32(txtMTo);

        SqlParameter p_txtScore = command.Parameters.Add("Score", SqlDbType.BigInt);
        p_txtScore.Direction = ParameterDirection.Input;
        p_txtScore.Value = Convert.ToInt32(strScore);

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = clsCommon.IsActive;

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


    public DataTable SelectScoring(Int32 KpiId)
    {
        SqlCommand command = new SqlCommand("proc_Select_KPIScoring");
        SqlParameter p_QuarterId = command.Parameters.Add("ScoringID", SqlDbType.BigInt);
        p_QuarterId.Direction = ParameterDirection.Input;
        p_QuarterId.Value = KpiId;
        objDC.CreateDSFromProc(command, "KPIList");
        return objDC.ds.Tables["KPIList"];
    }


    public DataTable SelectKPIList(string strGroup,string strQuarter, string strYear,string strEmpId)
    {

        SqlCommand command = new SqlCommand("proc_Select_KPIList");
        SqlParameter p_GroupId = command.Parameters.Add("GroupId", SqlDbType.BigInt);
        p_GroupId.Direction = ParameterDirection.Input;
        p_GroupId.Value =Convert.ToInt32(strGroup);

         SqlParameter p_QuarterId = command.Parameters.Add("QuarterId", SqlDbType.BigInt);
        p_QuarterId.Direction = ParameterDirection.Input;
        p_QuarterId.Value =Convert.ToInt32(strQuarter);

         SqlParameter p_Year = command.Parameters.Add("VYear", SqlDbType.BigInt);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = Convert.ToInt32(strYear);

         SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        objDC.CreateDSFromProc(command, "EKPIList");
        return objDC.ds.Tables["EKPIList"];
    }

    // Emp KPI Entry
    public void InsertEmpKpiData(clsCommonSetup clsCommon, GridView grv, string strGroup,string strQuertar, string strYear, string strEmpId, string IsUpdate, string IsDelete)
    {
        int i = 0;
        SqlCommand[] command;
        command = new SqlCommand[grv.Rows.Count + 1];
        //DELETE FROM DETAILS TABLE        
        command[0] = new SqlCommand("proc_InUp_KPIEmpkpiMst");
        command[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_UserId = command[0].Parameters.Add("EKPIID", SqlDbType.BigInt);
        p_UserId.Direction = ParameterDirection.Input;
        p_UserId.Value =Convert.ToInt32(clsCommon.ID);

        SqlParameter p_DivisionId = command[0].Parameters.Add("EmpId", SqlDbType.Char);
        p_DivisionId.Direction = ParameterDirection.Input;
        p_DivisionId.Value = strEmpId;

        SqlParameter p_SBUId = command[0].Parameters.Add("GroupId", SqlDbType.BigInt);
        p_SBUId.Direction = ParameterDirection.Input;
        p_SBUId.Value = Convert.ToInt32(strGroup);

        SqlParameter p_QuarterId = command[0].Parameters.Add("QuarterId", SqlDbType.BigInt);
        p_QuarterId.Direction = ParameterDirection.Input;
        p_QuarterId.Value = Convert.ToInt32(strQuertar);

        SqlParameter p_VYear = command[0].Parameters.Add("VYear", SqlDbType.BigInt);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = Convert.ToInt32(strYear);

         SqlParameter p_IsActive = command[0].Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = clsCommon.IsActive;

        SqlParameter p_InsertedBy = command[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = clsCommon.InsertedBy;

        SqlParameter p_InsertedDate = command[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = clsCommon.InsertedDate;

        SqlParameter p_IsUpdate = command[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete =command[0].Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;


        i = 1;
       // long lngTransID = Convert.ToInt64(Common.getMaxId("KPIEmpkpiDetail", "KPIDtlId"));

        foreach (GridViewRow gRow in grv.Rows)
        {
            CheckBox chkB = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            if (chkB.Checked == true)
            {
                command[i] = new SqlCommand("Proc_InUp_KPIEmpkpiDetail");
                command[i].CommandType = CommandType.StoredProcedure;

                SqlParameter p_EKPIID = command[i].Parameters.Add("EKPIID", SqlDbType.BigInt);
                p_EKPIID.Direction = ParameterDirection.Input;
                p_EKPIID.Value = Convert.ToInt32(clsCommon.ID); 

                SqlParameter p_EMPID = command[i].Parameters.Add("EmpId", SqlDbType.Char);
                p_EMPID.Direction = ParameterDirection.Input;
                p_EMPID.Value = strEmpId;

                SqlParameter p_KpiId = command[i].Parameters.Add("KpiId", SqlDbType.BigInt);
                p_KpiId.Direction = ParameterDirection.Input;
                p_KpiId.Value = Convert.ToInt32(grv.DataKeys[gRow.DataItemIndex].Values[1].ToString().Trim()); 
                // Convert.ToInt32(gRow.Cells[2].Text.Trim()); //gRow.Cells[2].Text.Trim()
                //grScoring.DataKeys[_gridView.SelectedIndex].Values[0].ToString();

                TextBox txtB = (TextBox)gRow.Cells[4].FindControl("txtMeasur");
                 SqlParameter p_Measur = command[i].Parameters.Add("Measur", SqlDbType.VarChar);
                p_Measur.Direction = ParameterDirection.Input;
                p_Measur.Value = txtB.Text; //gRow.Cells[4].Text.Trim();
                // grv.SelectedRow.Cells[3].Text

                TextBox txtM = (TextBox)gRow.Cells[5].FindControl("txtTarget");
                 SqlParameter p_Target = command[i].Parameters.Add("Target", SqlDbType.VarChar);
                p_Target.Direction = ParameterDirection.Input;
                p_Target.Value = txtM.Text; //gRow.Cells[5].Text.Trim();

                SqlParameter p_IsActive1 = command[i].Parameters.Add("IsActive", SqlDbType.Char);
                p_IsActive1.Direction = ParameterDirection.Input;
                p_IsActive1.Value = clsCommon.IsActive;

                SqlParameter p_InsertedBy1 = command[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                p_InsertedBy1.Direction = ParameterDirection.Input;
                p_InsertedBy1.Value = clsCommon.InsertedBy;

                SqlParameter p_InsertedDate1 = command[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                p_InsertedDate1.Direction = ParameterDirection.Input;
                p_InsertedDate1.Value = clsCommon.InsertedDate;

                SqlParameter p_IsUpdate1 = command[i].Parameters.Add("IsUpdate", SqlDbType.Char);
                p_IsUpdate1.Direction = ParameterDirection.Input;
                p_IsUpdate1.Value = IsUpdate;

                SqlParameter p_IsDelete1 =command[i].Parameters.Add("IsDelete", SqlDbType.Char);
                p_IsDelete1.Direction = ParameterDirection.Input;
                p_IsDelete1.Value = IsDelete;

                i++;
            }
        }
        objDC.MakeTransaction(command);
    }

    // Emp KPI Entry
    public void UpdateEmpKpi(clsCommonSetup clsCommon, GridView grv, string strGroup, string strQuertar, string strYear, 
        string strEmpId, string IsUpdate, string IsDelete)
    {
        int i = 0;
        SqlCommand[] command;
        command = new SqlCommand[grv.Rows.Count];
       
        foreach (GridViewRow gRow in grv.Rows)
        {
            CheckBox chkB = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            if (chkB.Checked == true)
            {
                command[i] = new SqlCommand("Proc_Up_KPIEmpkpi");
                command[i].CommandType = CommandType.StoredProcedure;

                SqlParameter p_EKPIID = command[i].Parameters.Add("EKPIID", SqlDbType.BigInt);
                p_EKPIID.Direction = ParameterDirection.Input;
                p_EKPIID.Value = Convert.ToInt32(grv.DataKeys[gRow.DataItemIndex].Values[2].ToString().Trim());

                SqlParameter p_EMPID = command[i].Parameters.Add("EmpId", SqlDbType.Char);
                p_EMPID.Direction = ParameterDirection.Input;
                p_EMPID.Value = strEmpId;

                SqlParameter p_SBUId = command[i].Parameters.Add("GroupId", SqlDbType.BigInt);
                p_SBUId.Direction = ParameterDirection.Input;
                p_SBUId.Value = Convert.ToInt32(strGroup);

                SqlParameter p_QuarterId = command[i].Parameters.Add("QuarterId", SqlDbType.BigInt);
                p_QuarterId.Direction = ParameterDirection.Input;
                p_QuarterId.Value = Convert.ToInt32(strQuertar);

                SqlParameter p_VYear = command[i].Parameters.Add("VYear", SqlDbType.BigInt);
                p_VYear.Direction = ParameterDirection.Input;
                p_VYear.Value = Convert.ToInt32(strYear);


                SqlParameter p_KpiId = command[i].Parameters.Add("KpiId", SqlDbType.BigInt);
                p_KpiId.Direction = ParameterDirection.Input;
                p_KpiId.Value = Convert.ToInt32(grv.DataKeys[gRow.DataItemIndex].Values[1].ToString().Trim());

                TextBox txtM = (TextBox)gRow.Cells[6].FindControl("txtAchive");
                SqlParameter p_Target = command[i].Parameters.Add("Achive", SqlDbType.BigInt);
                p_Target.Direction = ParameterDirection.Input;
                p_Target.Value = Convert.ToInt32("0" + txtM.Text);

                TextBox txtB = (TextBox)gRow.Cells[7].FindControl("txtAchivePercent");
                SqlParameter p_Measur = command[i].Parameters.Add("AchivePercent", SqlDbType.BigInt);
                p_Measur.Direction = ParameterDirection.Input;
                p_Measur.Value = Convert.ToInt32("0" + txtB.Text);

                SqlParameter p_IsActive1 = command[i].Parameters.Add("IsActive", SqlDbType.Char);
                p_IsActive1.Direction = ParameterDirection.Input;
                p_IsActive1.Value = clsCommon.IsActive;

                SqlParameter p_InsertedBy1 = command[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                p_InsertedBy1.Direction = ParameterDirection.Input;
                p_InsertedBy1.Value = clsCommon.InsertedBy;

                SqlParameter p_InsertedDate1 = command[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                p_InsertedDate1.Direction = ParameterDirection.Input;
                p_InsertedDate1.Value = clsCommon.InsertedDate;

                SqlParameter p_IsUpdate1 = command[i].Parameters.Add("IsUpdate", SqlDbType.Char);
                p_IsUpdate1.Direction = ParameterDirection.Input;
                p_IsUpdate1.Value = IsUpdate;

                SqlParameter p_IsDelete1 = command[i].Parameters.Add("IsDelete", SqlDbType.Char);
                p_IsDelete1.Direction = ParameterDirection.Input;
                p_IsDelete1.Value = IsDelete;

                i++;
            }
        }
        objDC.MakeTransaction(command);
    }

    public DataTable SelectEmpKPI(string strGroup, string strQuarter, string strYear, string strEmpId)
    {

        SqlCommand command = new SqlCommand("proc_Select_EMPKPIList");
        SqlParameter p_GroupId = command.Parameters.Add("GroupId", SqlDbType.BigInt);
        p_GroupId.Direction = ParameterDirection.Input;
        p_GroupId.Value = Convert.ToInt32(strGroup);

        SqlParameter p_QuarterId = command.Parameters.Add("QuarterId", SqlDbType.BigInt);
        p_QuarterId.Direction = ParameterDirection.Input;
        p_QuarterId.Value = Convert.ToInt32(strQuarter);

        SqlParameter p_Year = command.Parameters.Add("VYear", SqlDbType.BigInt);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = Convert.ToInt32(strYear);

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        objDC.CreateDSFromProc(command, "EKPIList1");
        return objDC.ds.Tables["EKPIList1"];
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


    //#region Notification
    //public DataTable GetConfirmationEmp(string StartDate, string EndDate)
    //{
    //    SqlCommand command = new SqlCommand("proc_Get_ConfirmationEmp");

    //    SqlParameter p_InsertedDate = command.Parameters.Add("StartDate", DBNull.Value);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.IsNullable = true;
    //    if (string.IsNullOrEmpty(StartDate) == false)
    //        p_InsertedDate.Value = StartDate;
    //    else
    //        p_InsertedDate.Value = "";

    //    SqlParameter p_EndDate = command.Parameters.Add("EndDate", DBNull.Value);
    //    p_EndDate.Direction = ParameterDirection.Input;
    //    p_EndDate.IsNullable = true;
    //    if (string.IsNullOrEmpty(EndDate) == false)
    //        p_EndDate.Value = EndDate;
    //    else
    //        p_EndDate.Value = "";

    //    objDC.CreateDSFromProc(command, "GetConfirmationEmp");
    //    return objDC.ds.Tables["GetConfirmationEmp"];
    //}

    //public DataTable GetEmpBirthday()
    //{
    //    SqlCommand command = new SqlCommand("proc_Get_EmpBirthday");

    //    objDC.CreateDSFromProc(command, "GetEmpBirthday");
    //    return objDC.ds.Tables["GetEmpBirthday"];
    //}

    //public DataTable GetLicenseExpireDate(string StartDate, string EndDate)
    //{
    //    SqlCommand command = new SqlCommand("proc_Get_DrivingExpDate");

    //    SqlParameter p_InsertedDate = command.Parameters.Add("StartDate", DBNull.Value);
    //    p_InsertedDate.Direction = ParameterDirection.Input;
    //    p_InsertedDate.IsNullable = true;
    //    if (string.IsNullOrEmpty(StartDate) == false)
    //        p_InsertedDate.Value = StartDate;
    //    else
    //        p_InsertedDate.Value = "";

    //    SqlParameter p_EndDate = command.Parameters.Add("EndDate", DBNull.Value);
    //    p_EndDate.Direction = ParameterDirection.Input;
    //    p_EndDate.IsNullable = true;
    //    if (string.IsNullOrEmpty(EndDate) == false)
    //        p_EndDate.Value = EndDate;
    //    else
    //        p_EndDate.Value = "";


    //    objDC.CreateDSFromProc(command, "GetLicenseExpireDate");
    //    return objDC.ds.Tables["GetLicenseExpireDate"];
    //}

    //#endregion



    public KPIManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}