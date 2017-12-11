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
public class ChangeShiftManager
{

    DBConnector objDC = new DBConnector();

    public DataTable GetData(string strFlag, string UserId, string IsAdmin, string DivisionID, string SbuID,
        string DeptID,string SectionID,string LocId,string EmpTypeStatus,string strStatus,
        string strUserId, string strLocId,  string EmpId, string isClosed)
    {        
        SqlCommand command = new SqlCommand("proc_Select_ChangeShift");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_Flag = command.Parameters.Add("Flag", SqlDbType.Char);
        p_Flag.Direction = ParameterDirection.Input;
        p_Flag.Value = strFlag;

        SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
        p_UserId.Direction = ParameterDirection.Input;
        p_UserId.Value = UserId;

        SqlParameter p_IsAdmin = command.Parameters.Add("IsAdmin", SqlDbType.Char);
        p_IsAdmin.Direction = ParameterDirection.Input;
        p_IsAdmin.Value = IsAdmin;

        SqlParameter p_DivisionID = command.Parameters.Add("DivisionID", SqlDbType.BigInt);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = -1;

        SqlParameter p_SbuID = command.Parameters.Add("SbuID", SqlDbType.BigInt);
        p_SbuID.Direction = ParameterDirection.Input;
        if (SbuID== "")
            p_SbuID.Value = -1;
        else
            p_SbuID.Value = SbuID;

        SqlParameter p_DeptID = command.Parameters.Add("DeptID", SqlDbType.BigInt);
        p_DeptID.Direction = ParameterDirection.Input;
        p_DeptID.Value = DeptID;


        SqlParameter p_SectionID = command.Parameters.Add("SectionID", SqlDbType.BigInt);
        p_SectionID.Direction = ParameterDirection.Input;
        p_SectionID.Value = SectionID;

        SqlParameter p_LocId = command.Parameters.Add("LocId", SqlDbType.BigInt);
        p_LocId.Direction = ParameterDirection.Input;
        p_LocId.Value = LocId; 
        
        SqlParameter p_EmpType = command.Parameters.Add("EmpTypeStatus", SqlDbType.Char);
        p_EmpType.Direction = ParameterDirection.Input;
        p_EmpType.Value = EmpTypeStatus;

        SqlParameter p_EmpStatus = command.Parameters.Add("EmpSubTypeStatus", SqlDbType.Char);
        p_EmpStatus.Direction = ParameterDirection.Input;
        p_EmpStatus.Value = strStatus;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_isClosed = command.Parameters.Add("isClosed", SqlDbType.Char);
        p_isClosed.Direction = ParameterDirection.Input;
        p_isClosed.Value = isClosed;

        objDC.CreateDSFromProc(command, "ShiftChange");
        return objDC.ds.Tables["ShiftChange"];
    }

}
