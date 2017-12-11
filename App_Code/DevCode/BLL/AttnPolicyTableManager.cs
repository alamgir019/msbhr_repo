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
/// Summary description for AttnPolicyTableManager
/// </summary>
public class AttnPolicyTableManager
{
    DBConnector objDC = new DBConnector();

    public void InsertAttnPolicy(string IsUpdate, string IsDelete,AttendancePolicy objAttn)
    {
        SqlCommand command = new SqlCommand("proc_Insert_AttnPolicy");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_AttnPolicyId= command.Parameters.Add("AttnPolicyId", SqlDbType.BigInt);
        p_AttnPolicyId.Direction = ParameterDirection.Input;
        p_AttnPolicyId.Value = objAttn.AttnPolicyId;

        SqlParameter p_PolicyName = command.Parameters.Add("PolicyName", SqlDbType.VarChar);
        p_PolicyName.Direction = ParameterDirection.Input;
        p_PolicyName.Value = objAttn.PolicyName;

        SqlParameter p_PoliDesc = command.Parameters.Add("PoliDesc", SqlDbType.VarChar);
        p_PoliDesc.Direction = ParameterDirection.Input;
        p_PoliDesc.Value = objAttn.PoliDesc;

        SqlParameter p_OTStartGrace = command.Parameters.Add("OTStartGrace", SqlDbType.BigInt);
        p_OTStartGrace.Direction = ParameterDirection.Input;
        if (string.IsNullOrEmpty(objAttn.OTStartGrace) == false)
            p_OTStartGrace.Value = objAttn.OTStartGrace;
        else
            p_OTStartGrace.Value = "0";

        SqlParameter p_ArvlGrace = command.Parameters.Add("ArvlGrace", SqlDbType.BigInt);
        p_ArvlGrace.Direction = ParameterDirection.Input;
        if (string.IsNullOrEmpty(objAttn.ArvlGrace) == false)
            p_ArvlGrace.Value = objAttn.ArvlGrace;
        else
            p_ArvlGrace.Value = "0";

        SqlParameter p_LunchBreak = command.Parameters.Add("LunchBreak", SqlDbType.BigInt);
        p_LunchBreak.Direction = ParameterDirection.Input;
        if (string.IsNullOrEmpty(objAttn.LunchBreak) == false)
            p_LunchBreak.Value = objAttn.LunchBreak;
        else
            p_LunchBreak.Value = "0";

        SqlParameter p_InTime = command.Parameters.Add("InTime", SqlDbType.VarChar);
        p_InTime.Direction = ParameterDirection.Input;
        p_InTime.Value = objAttn.InTime;

        SqlParameter p_OutTime= command.Parameters.Add("OutTime", SqlDbType.VarChar);
        p_OutTime.Direction = ParameterDirection.Input;
        p_OutTime.Value = objAttn.OutTime;

        SqlParameter p_IsNextDay = command.Parameters.Add("IsNextDay", SqlDbType.Char);
        p_IsNextDay.Direction = ParameterDirection.Input;
        p_IsNextDay.Value = objAttn.IsNextDay;

        SqlParameter p_IsActive = command.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = objAttn.IsActive;

        SqlParameter p_ISDefault = command.Parameters.Add("ISDefault", SqlDbType.Char);
        p_ISDefault.Direction = ParameterDirection.Input;
        p_ISDefault.Value = objAttn.ISDefault;

        SqlParameter p_LunchTime = command.Parameters.Add("LunchTime", SqlDbType.VarChar);
        p_LunchTime.Direction = ParameterDirection.Input;
        p_LunchTime.Value = objAttn.LunchTime;

         SqlParameter p_WorkingHr = command.Parameters.Add("WorkingHr", SqlDbType.Decimal);
        p_WorkingHr.Direction = ParameterDirection.Input;
        if (string.IsNullOrEmpty(objAttn.WorkingHr) == false)
            p_WorkingHr.Value = objAttn.WorkingHr;
        else
            p_WorkingHr.Value = "0";

        SqlParameter p_DivisionId = command.Parameters.Add("DivisionId", SqlDbType.VarChar);
        p_DivisionId.Direction = ParameterDirection.Input;
        if (objAttn.DivisionId != "")
            p_DivisionId.Value = Convert.ToInt32(objAttn.DivisionId);
        else
            p_DivisionId.Value = "";
        SqlParameter p_SBUId = command.Parameters.Add("SBUId", SqlDbType.VarChar);
        p_SBUId.Direction = ParameterDirection.Input;
        if (objAttn.SBUId != "")
            p_SBUId.Value = Convert.ToInt32(objAttn.SBUId);
        else
            p_SBUId.Value = "";

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = objAttn.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = objAttn.InsertedDate;

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

    public DataTable GetData(string strAttnPolicyId)
    {
        SqlCommand command = new SqlCommand("proc_Select_AttnPolicy");
        command.CommandType = CommandType.StoredProcedure;
        SqlParameter p_AttnPolicyId = command.Parameters.Add("AttnPolicyId", SqlDbType.BigInt);
        p_AttnPolicyId.Direction = ParameterDirection.Input;
        p_AttnPolicyId.Value = strAttnPolicyId;
        objDC.CreateDSFromProc(command, "AttnPolicy");
        return objDC.ds.Tables["AttnPolicy"];
    }
    public DataTable GetDataSBUwise(string SBUId)
    {
        SqlCommand command = new SqlCommand("proc_Select_AttnPolicy_SBUWise");
        command.CommandType = CommandType.StoredProcedure;
        SqlParameter p_SBUId = command.Parameters.Add("SBUId", SqlDbType.BigInt);
        p_SBUId.Direction = ParameterDirection.Input;
        p_SBUId.Value = int.Parse(SBUId);
        objDC.CreateDSFromProc(command, "AttnPolicySBU1");
        return objDC.ds.Tables["AttnPolicySBU1"];
    }
    public DataTable GetDataSBUwiseNextDay(string SBUId)
    {
        SqlCommand command = new SqlCommand("proc_Select_AttnPolicy_SBUWise_NextDay");
        command.CommandType = CommandType.StoredProcedure;
        SqlParameter p_SBUId = command.Parameters.Add("SBUId", SqlDbType.BigInt);
        p_SBUId.Direction = ParameterDirection.Input;
        p_SBUId.Value = int.Parse(SBUId);
        objDC.CreateDSFromProc(command, "AttnPolicySBUNextDay");
        return objDC.ds.Tables["AttnPolicySBUNextDay"];
    }
    public DataTable SelectSBUwiseData(string SBUId) // for Validation
    {
        SqlCommand command = new SqlCommand("proc_Select_AttnPolicy_SBUWise");
        command.CommandType = CommandType.StoredProcedure;
        SqlParameter p_SBUId = command.Parameters.Add("SBUId", SqlDbType.BigInt);
        p_SBUId.Direction = ParameterDirection.Input;
        p_SBUId.Value = int.Parse(SBUId);
        objDC.CreateDSFromProc(command, "AttnPolicySBU");
        return objDC.ds.Tables["AttnPolicySBU"];
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
	public AttnPolicyTableManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
