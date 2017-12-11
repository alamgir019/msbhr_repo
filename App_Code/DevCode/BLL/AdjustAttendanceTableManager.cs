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
/// Summary description for AdjustAttendanceTableManager
/// </summary>
public class AdjustAttendanceTableManager
{

    DBConnector objDC = new DBConnector();

    public DataTable GetData(string strDateFrom, string strDateTo, string strSearchId, string strValue,
        string strStatus, string strLocId, string strEmpTypeStatus)
    {
        SqlCommand command = new SqlCommand("proc_Select_AttnAdjust");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_AttndDateFrom = command.Parameters.Add("AttndDateFrom", SqlDbType.DateTime);
        p_AttndDateFrom.Direction = ParameterDirection.Input;
        p_AttndDateFrom.Value = strDateFrom;

        SqlParameter p_AttndDateTo = command.Parameters.Add("AttndDateTo", SqlDbType.DateTime);
        p_AttndDateTo.Direction = ParameterDirection.Input;
        p_AttndDateTo.Value = strDateTo;

        SqlParameter p_SearchId = command.Parameters.Add("SearchId", SqlDbType.Char);
        p_SearchId.Direction = ParameterDirection.Input;
        p_SearchId.Value = strSearchId;

        SqlParameter p_ValueId = command.Parameters.Add("ValueId", SqlDbType.VarChar);
        p_ValueId.Direction = ParameterDirection.Input;
        p_ValueId.Value = strValue;

        SqlParameter p_Status = command.Parameters.Add("Status", SqlDbType.Char);
        p_Status.Direction = ParameterDirection.Input;
        p_Status.Value = strStatus;

        SqlParameter p_LocId = command.Parameters.Add("LocID", SqlDbType.BigInt);
        p_LocId.Direction = ParameterDirection.Input;
        p_LocId.Value = Convert.ToInt32(strLocId);

        objDC.CreateDSFromProc(command, "AttnAdjust");
        return objDC.ds.Tables["AttnAdjust"];
    }

    public DataTable GetOTData(string strDateFrom, string strDateTo, string strSearchId,string strValue,  string strDivId, string strStatus, string strUserId,
        string strOTAdmin, string strshiftid)
    {
        SqlCommand command;
        if(strOTAdmin=="Y")
            command = new SqlCommand("proc_Select_OTApprove1_Shiftwise");
        else
            command = new SqlCommand("proc_Select_OTRecommend_ShiftWise");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_AttndDateFrom = command.Parameters.Add("AttndDateFrom", SqlDbType.VarChar);
        p_AttndDateFrom.Direction = ParameterDirection.Input;
        p_AttndDateFrom.Value = strDateFrom;

        SqlParameter p_AttndDateTo = command.Parameters.Add("AttndDateTo", SqlDbType.VarChar);
        p_AttndDateTo.Direction = ParameterDirection.Input;
        if (strDateTo != "")
            p_AttndDateTo.Value = strDateTo;
        else
            p_AttndDateTo.Value = strDateFrom;

        SqlParameter p_SearchId = command.Parameters.Add("SearchId", SqlDbType.Char);
        p_SearchId.Direction = ParameterDirection.Input;
        p_SearchId.Value = strSearchId;

        SqlParameter p_ValueId = command.Parameters.Add("ValueId", SqlDbType.VarChar);
        p_ValueId.Direction = ParameterDirection.Input;
        p_ValueId.Value = strValue;

        //SqlParameter p_BranchId = command.Parameters.Add("BranchId", SqlDbType.BigInt);
        //p_BranchId.Direction = ParameterDirection.Input;
        //p_BranchId.Value = strBranchId;

        SqlParameter p_DivisionId = command.Parameters.Add("DivisionId", SqlDbType.BigInt);
        p_DivisionId.Direction = ParameterDirection.Input;
        p_DivisionId.Value = strDivId;

        SqlParameter p_Status = command.Parameters.Add("Status", SqlDbType.Char);
        p_Status.Direction = ParameterDirection.Input;
        p_Status.Value = strStatus;

        SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
        p_UserId.Direction = ParameterDirection.Input;
        if (strSearchId == "3")
            p_UserId.Value = "'" + strUserId + "'";
        else
            p_UserId.Value = strUserId;

       
        SqlParameter p_sbuid = command.Parameters.Add("shiftid", SqlDbType.BigInt);
        p_sbuid.Direction = ParameterDirection.Input;
        p_sbuid.Value = strshiftid;       

        objDC.CreateDSFromProc(command, "OTApproval");
        return objDC.ds.Tables["OTApproval"];
    }
    public DataTable GetOTDataShiftWise(string strDateFrom, string strDateTo, string strSearchId, string strValue, string strDivId, string strStatus, string strUserId,
         string strOTAdmin, string strshiftid)
    {
        SqlCommand command;
        if(strOTAdmin=="Y")
            command = new SqlCommand("proc_Select_OTApprove1_ShiftWise");
        else
            command = new SqlCommand("proc_Select_OTRecommend_ShiftWise");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_AttndDateFrom = command.Parameters.Add("AttndDateFrom", SqlDbType.VarChar);
        p_AttndDateFrom.Direction = ParameterDirection.Input;
        p_AttndDateFrom.Value = strDateFrom;

        SqlParameter p_AttndDateTo = command.Parameters.Add("AttndDateTo", SqlDbType.VarChar);
        p_AttndDateTo.Direction = ParameterDirection.Input;
        if (strDateTo != "")
            p_AttndDateTo.Value = strDateTo;
        else
            p_AttndDateTo.Value = strDateFrom;

        SqlParameter p_SearchId = command.Parameters.Add("SearchId", SqlDbType.Char);
        p_SearchId.Direction = ParameterDirection.Input;
        p_SearchId.Value = strSearchId;

        SqlParameter p_ValueId = command.Parameters.Add("ValueId", SqlDbType.VarChar);
        p_ValueId.Direction = ParameterDirection.Input;
        p_ValueId.Value = strValue;

        //SqlParameter p_BranchId = command.Parameters.Add("BranchId", SqlDbType.BigInt);
        //p_BranchId.Direction = ParameterDirection.Input;
        //p_BranchId.Value = strBranchId;

        SqlParameter p_DivisionId = command.Parameters.Add("DivisionId", SqlDbType.VarChar);
        p_DivisionId.Direction = ParameterDirection.Input;
        p_DivisionId.Value = strDivId;

        SqlParameter p_Status = command.Parameters.Add("Status", SqlDbType.Char);
        p_Status.Direction = ParameterDirection.Input;
        p_Status.Value = strStatus;

        SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
        p_UserId.Direction = ParameterDirection.Input;
        if (strSearchId == "3")
            p_UserId.Value = "'" + strUserId + "'";
        else
            p_UserId.Value = strUserId;

        SqlParameter p_shiftid = command.Parameters.Add("shiftid", SqlDbType.BigInt);
        p_shiftid.Direction = ParameterDirection.Input;
        p_shiftid.Value = strshiftid;

        objDC.CreateDSFromProc(command, "OTApproval");
        return objDC.ds.Tables["OTApproval"];
    }
    
    //added by anol 25.04.2010
    public DataTable GetOTDataEmpWise(string strDateFrom, string strDateTo, string strSearchId, string strValue, string strStatus, string strUserId, string strLocId, string strOTAdmin)
    {
        SqlCommand command;
        if (strOTAdmin == "Y")
            command = new SqlCommand("proc_Select_OTApprove1");
        else
            command = new SqlCommand("proc_Select_OTRecommend");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_AttndDateFrom = command.Parameters.Add("AttndDateFrom", SqlDbType.VarChar);
        p_AttndDateFrom.Direction = ParameterDirection.Input;
        p_AttndDateFrom.Value = strDateFrom;

        SqlParameter p_AttndDateTo = command.Parameters.Add("AttndDateTo", SqlDbType.VarChar);
        p_AttndDateTo.Direction = ParameterDirection.Input;
        p_AttndDateTo.Value = strDateTo;

        SqlParameter p_SearchId = command.Parameters.Add("SearchId", SqlDbType.Char);
        p_SearchId.Direction = ParameterDirection.Input;
        p_SearchId.Value = strSearchId;

        SqlParameter p_ValueId = command.Parameters.Add("ValueId", SqlDbType.VarChar);
        p_ValueId.Direction = ParameterDirection.Input;
        p_ValueId.Value = strValue;

        SqlParameter p_Status = command.Parameters.Add("Status", SqlDbType.Char);
        p_Status.Direction = ParameterDirection.Input;
        p_Status.Value = strStatus;

        SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
        p_UserId.Direction = ParameterDirection.Input;
        p_UserId.Value = strUserId;

        SqlParameter p_LocId = command.Parameters.Add("LocID", SqlDbType.BigInt);
        p_LocId.Direction = ParameterDirection.Input;
        p_LocId.Value = strLocId;


        objDC.CreateDSFromProc(command, "OTApproval");
        return objDC.ds.Tables["OTApproval"];
    }

    public void InsertAdjustAttendance(GridView GrAttn, int CheckedDataCount, string strInsBy, string strInsDate)
    {
        int i = 0;
        int j = 0;
        SqlCommand[] cmd;
        string strSignInTime = "";
        string strSignOutTime = "";
        cmd = new SqlCommand[CheckedDataCount];
        foreach (GridViewRow gRow in GrAttn.Rows)
        {
            CheckBox chBox = new CheckBox();
            chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            if (chBox.Checked == true)
            {
                cmd[i] = new SqlCommand("proc_Adjust_Attendance");
                cmd[i].CommandType = CommandType.StoredProcedure;

                SqlParameter p_EmpId = cmd[i].Parameters.Add("EmpId", SqlDbType.Char);
                p_EmpId.Direction = ParameterDirection.Input;
                p_EmpId.Value = gRow.Cells[1].Text.Trim();

                SqlParameter p_AttndDate = cmd[i].Parameters.Add("AttndDate", SqlDbType.DateTime);
                p_AttndDate.Direction = ParameterDirection.Input;
                p_AttndDate.Value = Common.ReturnDate(gRow.Cells[5].Text.Trim());

                if (Common.CheckNullString(gRow.Cells[6].Text) != "")
                    strSignInTime = Common.ReturnDateTime(gRow.Cells[6].Text.Trim());
                else
                    strSignInTime = "";

                SqlParameter p_SignInTime = cmd[i].Parameters.Add("SignInTime", SqlDbType.VarChar);
                p_SignInTime.Direction = ParameterDirection.Input;
                p_SignInTime.Value = strSignInTime;
                
                if (Common.CheckNullString(gRow.Cells[8].Text) != "")
                    strSignOutTime = Common.ReturnDateTime(gRow.Cells[8].Text.Trim());
                else
                    strSignOutTime = "";

                SqlParameter p_SignOutTime = cmd[i].Parameters.Add("SignOutTime", SqlDbType.VarChar);
                p_SignOutTime.Direction = ParameterDirection.Input;
                p_SignOutTime.Value = strSignOutTime;

                SqlParameter p_Status = cmd[i].Parameters.Add("Status", SqlDbType.Char);
                p_Status.Direction = ParameterDirection.Input;
                p_Status.Value = gRow.Cells[10].Text.Trim();

                SqlParameter p_Remarks = cmd[i].Parameters.Add("Remarks", SqlDbType.VarChar);
                p_Remarks.Direction = ParameterDirection.Input;
                p_Remarks.Value = Common.CheckNullString(gRow.Cells[12].Text.Trim());

                SqlParameter p_isUpdatedManually = cmd[i].Parameters.Add("isUpdatedManually", SqlDbType.Char);
                p_isUpdatedManually.Direction = ParameterDirection.Input;
                p_isUpdatedManually.Value = gRow.Cells[16].Text.Trim();

                SqlParameter p_ExtraTimeWorked = cmd[i].Parameters.Add("ExtraTimeWorked", SqlDbType.Char);
                p_ExtraTimeWorked.Direction = ParameterDirection.Input;
                p_ExtraTimeWorked.Value = gRow.Cells[17].Text.Trim();

                SqlParameter p_AttnPolicyId = cmd[i].Parameters.Add("AttnPolicyId", SqlDbType.BigInt);
                p_AttnPolicyId.Direction = ParameterDirection.Input;
                p_AttnPolicyId.Value = string.IsNullOrEmpty(GrAttn.DataKeys[j].Values[4].ToString().Trim())==false?GrAttn.DataKeys[j].Values[4].ToString().Trim():"99999";                

                Label lblDelay = new Label();
                lblDelay=(Label)gRow.Cells[11].FindControl("lblDelays");

                SqlParameter p_LateTimeAmt = cmd[i].Parameters.Add("LateTimeAmt", SqlDbType.BigInt);
                p_LateTimeAmt.Direction = ParameterDirection.Input;
                p_LateTimeAmt.Value = lblDelay.Text.Trim();


                SqlParameter p_InsertedDate = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                p_InsertedDate.Direction = ParameterDirection.Input;
                p_InsertedDate.Value = strInsDate;

                SqlParameter p_InsertedBy = cmd[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                p_InsertedBy.Direction = ParameterDirection.Input;
                p_InsertedBy.Value = strInsBy;

                i++;
            }
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

    public void InsertCrossOverAdjust(GridView GrAttn, int CheckedDataCount, string AssignPolicyId,string strInsBy, string strInsDate)
    {
        int i = 0;
        int j = 0;
        SqlCommand[] cmd;
        string strSignInTime = "";
        string strSignOutTime = "";
        cmd = new SqlCommand[CheckedDataCount];
        foreach (GridViewRow gRow in GrAttn.Rows)
        {
            CheckBox chBox = new CheckBox();
            chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            if (chBox.Checked == true)
            {
                cmd[i] = new SqlCommand("proc_Adjust_Attendance");
                cmd[i].CommandType = CommandType.StoredProcedure;

                SqlParameter p_EmpId = cmd[i].Parameters.Add("EmpId", SqlDbType.Char);
                p_EmpId.Direction = ParameterDirection.Input;
                p_EmpId.Value = gRow.Cells[2].Text.Trim();

                SqlParameter p_AttndDate = cmd[i].Parameters.Add("AttndDate", SqlDbType.DateTime);
                p_AttndDate.Direction = ParameterDirection.Input;
                p_AttndDate.Value = Common.ReturnDate(gRow.Cells[6].Text.Trim());

                if (Common.CheckNullString(gRow.Cells[7].Text) != "")
                    strSignInTime = Common.ReturnDateTime(gRow.Cells[7].Text.Trim());
                else
                    strSignInTime = "";

                SqlParameter p_SignInTime = cmd[i].Parameters.Add("SignInTime", SqlDbType.VarChar);
                p_SignInTime.Direction = ParameterDirection.Input;
                p_SignInTime.Value = strSignInTime;

                if (Common.CheckNullString(gRow.Cells[9].Text) != "")
                    strSignOutTime = Common.ReturnDateTime(gRow.Cells[9].Text.Trim());
                else
                    strSignOutTime = "";


                SqlParameter p_SignOutTime = cmd[i].Parameters.Add("SignOutTime", SqlDbType.VarChar);
                p_SignOutTime.Direction = ParameterDirection.Input;
                p_SignOutTime.Value = strSignOutTime;

                SqlParameter p_Status = cmd[i].Parameters.Add("Status", SqlDbType.Char);
                p_Status.Direction = ParameterDirection.Input;
                p_Status.Value = gRow.Cells[11].Text.Trim();

                SqlParameter p_Remarks = cmd[i].Parameters.Add("Remarks", SqlDbType.VarChar);
                p_Remarks.Direction = ParameterDirection.Input;
                p_Remarks.Value = Common.CheckNullString(gRow.Cells[13].Text.Trim());

                SqlParameter p_isUpdatedManually = cmd[i].Parameters.Add("isUpdatedManually", SqlDbType.Char);
                p_isUpdatedManually.Direction = ParameterDirection.Input;
                p_isUpdatedManually.Value = gRow.Cells[17].Text.Trim();

                SqlParameter p_ExtraTimeWorked = cmd[i].Parameters.Add("ExtraTimeWorked", SqlDbType.Char);
                p_ExtraTimeWorked.Direction = ParameterDirection.Input;
                p_ExtraTimeWorked.Value = gRow.Cells[18].Text.Trim();

                SqlParameter p_AttnPolicyId = cmd[i].Parameters.Add("AttnPolicyId", SqlDbType.BigInt);
                p_AttnPolicyId.Direction = ParameterDirection.Input;
                if (AssignPolicyId == "")
                    p_AttnPolicyId.Value = string.IsNullOrEmpty(GrAttn.DataKeys[j].Values[4].ToString().Trim()) == false ? GrAttn.DataKeys[j].Values[4].ToString().Trim() : "99999";
                else
                    p_AttnPolicyId.Value = string.IsNullOrEmpty(AssignPolicyId) == false ? AssignPolicyId : "99999";

                Label lblDelay = new Label();
                lblDelay = (Label)gRow.Cells[11].FindControl("lblDelays");

                SqlParameter p_LateTimeAmt = cmd[i].Parameters.Add("LateTimeAmt", SqlDbType.BigInt);
                p_LateTimeAmt.Direction = ParameterDirection.Input;
                p_LateTimeAmt.Value = lblDelay.Text.Trim();
                
                SqlParameter p_InsertedDate = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                p_InsertedDate.Direction = ParameterDirection.Input;
                p_InsertedDate.Value = strInsDate;

                SqlParameter p_InsertedBy = cmd[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                p_InsertedBy.Direction = ParameterDirection.Input;
                p_InsertedBy.Value = strInsBy;

                i++;
            }
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


    public void InsertPreAdjustAttendance(GridView GrAttn, int CheckedDataCount, string strInsBy, string strInsDate)
    {
        int i = 0;
        int j = 0;
        SqlCommand[] cmd;
        string strSignInTime = "";
        string strSignOutTime = "";
        cmd = new SqlCommand[CheckedDataCount];
        foreach (GridViewRow gRow in GrAttn.Rows)
        {
            CheckBox chBox = new CheckBox();
            chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            if (chBox.Checked == true)
            {
                cmd[i] = new SqlCommand("proc_Pre_Adjust_Attendance");
                cmd[i].CommandType = CommandType.StoredProcedure;

                SqlParameter p_EmpId = cmd[i].Parameters.Add("EmpId", SqlDbType.Char);
                p_EmpId.Direction = ParameterDirection.Input;
                p_EmpId.Value = gRow.Cells[1].Text;

                SqlParameter p_AttndDate = cmd[i].Parameters.Add("AttndDate", SqlDbType.DateTime);
                p_AttndDate.Direction = ParameterDirection.Input;
                p_AttndDate.Value = Common.ReturnDate(gRow.Cells[5].Text);

                if (Common.CheckNullString(gRow.Cells[6].Text) != "")
                    strSignInTime = Common.ReturnDateTime(gRow.Cells[6].Text);
                else
                    strSignInTime = "";

                SqlParameter p_SignInTime = cmd[i].Parameters.Add("SignInTime", SqlDbType.VarChar);
                p_SignInTime.Direction = ParameterDirection.Input;
                p_SignInTime.Value = strSignInTime;

                if (Common.CheckNullString(gRow.Cells[8].Text) != "")
                    strSignOutTime = Common.ReturnDateTime(gRow.Cells[8].Text);
                else
                    strSignOutTime = "";


                SqlParameter p_SignOutTime = cmd[i].Parameters.Add("SignOutTime", SqlDbType.VarChar);
                p_SignOutTime.Direction = ParameterDirection.Input;
                p_SignOutTime.Value = strSignOutTime;

                SqlParameter p_Status = cmd[i].Parameters.Add("Status", SqlDbType.Char);
                p_Status.Direction = ParameterDirection.Input;
                p_Status.Value = gRow.Cells[10].Text;

                SqlParameter p_Remarks = cmd[i].Parameters.Add("Remarks", SqlDbType.VarChar);
                p_Remarks.Direction = ParameterDirection.Input;
                p_Remarks.Value = Common.CheckNullString(gRow.Cells[12].Text);

                SqlParameter p_isUpdatedManually = cmd[i].Parameters.Add("isUpdatedManually", SqlDbType.Char);
                p_isUpdatedManually.Direction = ParameterDirection.Input;
                p_isUpdatedManually.Value = gRow.Cells[16].Text;

                SqlParameter p_ExtraTimeWorked = cmd[i].Parameters.Add("ExtraTimeWorked", SqlDbType.Char);
                p_ExtraTimeWorked.Direction = ParameterDirection.Input;
                p_ExtraTimeWorked.Value = gRow.Cells[17].Text;


                SqlParameter p_AttnPolicyId = cmd[i].Parameters.Add("AttnPolicyId", SqlDbType.VarChar);
                p_AttnPolicyId.Direction = ParameterDirection.Input;
                p_AttnPolicyId.Value = GrAttn.DataKeys[j].Values[4].ToString();

                Label lblDelay = new Label();
                lblDelay = (Label)gRow.Cells[11].FindControl("lblDelays");

                SqlParameter p_LateTimeAmt = cmd[i].Parameters.Add("LateTimeAmt", SqlDbType.BigInt);
                p_LateTimeAmt.Direction = ParameterDirection.Input;
                p_LateTimeAmt.Value = lblDelay.Text;


                SqlParameter p_InsertedDate = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                p_InsertedDate.Direction = ParameterDirection.Input;
                p_InsertedDate.Value = strInsDate;

                SqlParameter p_InsertedBy = cmd[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                p_InsertedBy.Direction = ParameterDirection.Input;
                p_InsertedBy.Value = strInsBy;

                i++;
            }
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

    public DataTable GetPreAdjustData(string strDateFrom, string strSearchId, string strValue,
        string strUserId, string strLocId, string strEmpTypeStatus, string strEmpSubTypeStatus, String intSBUID)
    {
        SqlCommand command = new SqlCommand("proc_Select_PreAttnAdjust");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_AttndDateFrom = command.Parameters.Add("AttndDateFrom", SqlDbType.DateTime);
        p_AttndDateFrom.Direction = ParameterDirection.Input;
        p_AttndDateFrom.Value = strDateFrom;

        SqlParameter p_SearchId = command.Parameters.Add("SearchId", SqlDbType.Char);
        p_SearchId.Direction = ParameterDirection.Input;
        p_SearchId.Value = strSearchId;

        SqlParameter p_ValueId = command.Parameters.Add("ValueId", SqlDbType.VarChar);
        p_ValueId.Direction = ParameterDirection.Input;
        p_ValueId.Value = strValue;

        SqlParameter p_SBUID = command.Parameters.Add("sbuid", SqlDbType.BigInt);
        p_SBUID.Direction = ParameterDirection.Input;
        if (intSBUID == "")
            intSBUID = "0";
        p_SBUID.Value = intSBUID;

        SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
        p_UserId.Direction = ParameterDirection.Input;

        if (strSearchId == "3")
            p_UserId.Value = "'" + strUserId + "'";
        else
            p_UserId.Value = strUserId;

        SqlParameter p_LocId = command.Parameters.Add("LocID", SqlDbType.BigInt);
        p_LocId.Direction = ParameterDirection.Input;
        p_LocId.Value = Convert.ToInt32(strLocId);

        SqlParameter p_EmpType = command.Parameters.Add("EmpTypeStatus", SqlDbType.Char);
        p_EmpType.Direction = ParameterDirection.Input;
        p_EmpType.Value = strEmpTypeStatus;

        SqlParameter p_EmpStatus = command.Parameters.Add("EmpSubTypeStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = strEmpSubTypeStatus;

        objDC.CreateDSFromProc(command, "PreAttnAdjust");
        return objDC.ds.Tables["PreAttnAdjust"];
    }

    public DataTable GetCrossOverAdjustData(string strDateFrom, string strDateTo, string strSearchId, string strValue,
        string strUserId, string strLocId, string strEmpTypeStatus, string strEmpSubTypeStatus, String intSBUID, string strAttnPolicyId)
    {
        SqlCommand command = new SqlCommand("proc_Select_CrossOverAdjustment");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_AttndDateFrom = command.Parameters.Add("AttndDateFrom", SqlDbType.DateTime);
        p_AttndDateFrom.Direction = ParameterDirection.Input;
        p_AttndDateFrom.Value = strDateFrom;

        SqlParameter p_AttndDateTo = command.Parameters.Add("AttndDateTo", SqlDbType.DateTime);
        p_AttndDateTo.Direction = ParameterDirection.Input;
        p_AttndDateTo.Value = strDateTo;

        SqlParameter p_SearchId = command.Parameters.Add("SearchId", SqlDbType.Char);
        p_SearchId.Direction = ParameterDirection.Input;
        p_SearchId.Value = strSearchId;

        SqlParameter p_ValueId = command.Parameters.Add("ValueId", SqlDbType.VarChar);
        p_ValueId.Direction = ParameterDirection.Input;
        p_ValueId.Value = strValue;

        SqlParameter p_SBUID = command.Parameters.Add("sbuid", SqlDbType.BigInt);
        p_SBUID.Direction = ParameterDirection.Input;
        if (intSBUID == "")
            intSBUID = "0";
        p_SBUID.Value = intSBUID;

        SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
        p_UserId.Direction = ParameterDirection.Input;

        if (strSearchId == "3")
            p_UserId.Value = "'" + strUserId + "'";
        else
            p_UserId.Value = strUserId;

        SqlParameter p_LocId = command.Parameters.Add("LocID", SqlDbType.BigInt);
        p_LocId.Direction = ParameterDirection.Input;
        p_LocId.Value = Convert.ToInt32(strLocId);

        SqlParameter p_EmpType = command.Parameters.Add("EmpTypeStatus", SqlDbType.Char);
        p_EmpType.Direction = ParameterDirection.Input;
        p_EmpType.Value = strEmpTypeStatus;

        SqlParameter p_EmpStatus = command.Parameters.Add("EmpSubTypeStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = strEmpSubTypeStatus;

        SqlParameter p_AttnPolicyId = command.Parameters.Add("AttnPolicyId", SqlDbType.BigInt);
        p_AttnPolicyId.Direction = ParameterDirection.Input;
        p_AttnPolicyId.Value = Convert.ToInt32(strAttnPolicyId);

        objDC.CreateDSFromProc(command, "CrossOverAdjust");
        return objDC.ds.Tables["CrossOverAdjust"];
    }


    public DataTable GetCurrDayData(string strDateFrom, string strDateTo, string strSearchId, string strValue,
       string strUserId, string strLocId, string strEmpTypeStatus, string strEmpSubTypeStatus, String intSBUID)
    {
        SqlCommand command = new SqlCommand("proc_Select_CrossOverAdjustment_Today");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_AttndDateFrom = command.Parameters.Add("AttndDateFrom", SqlDbType.DateTime);
        p_AttndDateFrom.Direction = ParameterDirection.Input;
        p_AttndDateFrom.Value = strDateFrom;

        SqlParameter p_AttndDateTo = command.Parameters.Add("AttndDateTo", SqlDbType.DateTime);
        p_AttndDateTo.Direction = ParameterDirection.Input;
        p_AttndDateTo.Value = strDateTo;

        SqlParameter p_SearchId = command.Parameters.Add("SearchId", SqlDbType.Char);
        p_SearchId.Direction = ParameterDirection.Input;
        p_SearchId.Value = strSearchId;

        SqlParameter p_ValueId = command.Parameters.Add("ValueId", SqlDbType.VarChar);
        p_ValueId.Direction = ParameterDirection.Input;
        p_ValueId.Value = strValue;

        SqlParameter p_SBUID = command.Parameters.Add("sbuid", SqlDbType.BigInt);
        p_SBUID.Direction = ParameterDirection.Input;
        if (intSBUID == "")
            intSBUID = "0";
        p_SBUID.Value = intSBUID;

        SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
        p_UserId.Direction = ParameterDirection.Input;

        if (strSearchId == "3")
            p_UserId.Value = "'" + strUserId + "'";
        else
            p_UserId.Value = strUserId;

        SqlParameter p_LocId = command.Parameters.Add("LocID", SqlDbType.BigInt);
        p_LocId.Direction = ParameterDirection.Input;
        p_LocId.Value = Convert.ToInt32(strLocId);

        SqlParameter p_EmpType = command.Parameters.Add("EmpTypeStatus", SqlDbType.Char);
        p_EmpType.Direction = ParameterDirection.Input;
        p_EmpType.Value = strEmpTypeStatus;

        SqlParameter p_EmpStatus = command.Parameters.Add("EmpSubTypeStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = strEmpSubTypeStatus;

        objDC.CreateDSFromProc(command, "CurrentDay");
        return objDC.ds.Tables["CurrentDay"];
    }

	public AdjustAttendanceTableManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
