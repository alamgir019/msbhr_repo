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
/// Summary description for RoasterManager
/// </summary>
public class RoasterManager
{
    DBConnector objDC = new DBConnector();
    public DataTable GetEmpData(string strEmpID,string strDeptId,string strUserId)
    {
        SqlCommand command = new SqlCommand("proc_Select_Roaster_Emp");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpID;

        SqlParameter p_DeptId = command.Parameters.Add("DeptId", SqlDbType.VarChar);
        p_DeptId.Direction = ParameterDirection.Input;
        p_DeptId.Value =strDeptId;

        SqlParameter p_UserId = command.Parameters.Add("UserId", SqlDbType.Char);
        p_UserId.Direction = ParameterDirection.Input;
        p_UserId.Value = strUserId;

        objDC.CreateDSFromProc(command, "RoasterEmp");
        return objDC.ds.Tables["RoasterEmp"];
    }


    public DataTable GetEmpAttnData(string strEmpID,string strFromDate,string strTodate)
    {
        //SqlCommand command = new SqlCommand("proc_Select_Roaster_Attn_Emp");
       // SqlCommand command = new SqlCommand("Select EMPID,AttndDate FROM ATTANDANCE WHERE EMPID IN(" + strEmpID + ") AND ATTNDDATE BETWEEN '" + strFromDate + "' AND '" + strTodate + "'");
        //command.CommandType = CommandType.StoredProcedure;
        //command.CommandType = CommandType.Text;
        string sql = "Select EMPID,AttndDate FROM Attendance WHERE EMPID IN(" + strEmpID + ") AND ATTNDDATE BETWEEN '" + strFromDate + "' AND '" + strTodate + "'";
        //SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.VarChar);
        //p_EmpID.Direction = ParameterDirection.Input;
        //p_EmpID.Value = strEmpID;

        //SqlParameter p_FromDate = command.Parameters.Add("FromDate", SqlDbType.VarChar);
        //p_FromDate.Direction = ParameterDirection.Input;
        //p_FromDate.Value = strFromDate;

        //SqlParameter p_ToDate = command.Parameters.Add("ToDate", SqlDbType.VarChar);
        //p_ToDate.Direction = ParameterDirection.Input;
        //p_ToDate.Value = strTodate;

       // objDC.CreateDSFromProc(command, "RoasterAttnEmp");
        objDC.CreateDS(sql, "RoasterAttnEmp");
        return objDC.ds.Tables["RoasterAttnEmp"];
    }
    public void InsertRoasterShift(DataTable dtEmAttn, DataTable dtAttn,GridView grRs,string FromDate,string ToDate)
    {
        int i = 0;
        int col=0;
        int cmdCount=0;
        string strExpr="";
        DateTime dtFromDate=Convert.ToDateTime(FromDate);
        DateTime dtToDate=Convert.ToDateTime(ToDate);
        int MinDay=dtFromDate.Day;
        int MaxDay=dtToDate.Day;
        string strAttndDate="";
        string strOutTime = "";
        string strAttnPolicyId = "";
        SqlCommand[] command;
        command = new SqlCommand[dtEmAttn.Rows.Count * (MaxDay - MinDay + 1)];
        foreach (GridViewRow gRow in grRs.Rows)
        {
            col = MinDay;
            for (col = MinDay; col <= MaxDay; col++)
            {
                command[cmdCount] = new SqlCommand("Insert_Roaster_Shift");
                command[cmdCount].CommandType = CommandType.StoredProcedure;

                strAttndDate = dtFromDate.Year.ToString() + "-" + dtFromDate.Month.ToString() + "-" + col.ToString();
                if (MinDay <= MaxDay)
                {
                    SqlParameter p_EmpID = command[cmdCount].Parameters.Add("EmpID", SqlDbType.Char);
                    p_EmpID.Direction = ParameterDirection.Input;
                    p_EmpID.Value = gRow.Cells[0].Text.Trim();



                    SqlParameter p_AttndDate = command[cmdCount].Parameters.Add("AttndDate", SqlDbType.DateTime);
                    p_AttndDate.Direction = ParameterDirection.Input;
                    p_AttndDate.Value = strAttndDate;


                    if (gRow.Cells[col + 1].Text != "W" && gRow.Cells[col + 1].Text != "H" && gRow.Cells[col + 1].Text != "LV")
                    {

                        strAttnPolicyId = grRs.DataKeys[i].Values[col].ToString().Trim();
                        strOutTime = FindInDataTableReturnValue(dtAttn, "AttnPolicyId='" + strAttnPolicyId + "'");

                        SqlParameter p_Status = command[cmdCount].Parameters.Add("Status", SqlDbType.Char);
                        p_Status.Direction = ParameterDirection.Input;
                        p_Status.Value = "A";

                        SqlParameter p_AttnPolicyId = command[cmdCount].Parameters.Add("AttnPolicyId", SqlDbType.BigInt);
                        p_AttnPolicyId.Direction = ParameterDirection.Input;
                        p_AttnPolicyId.Value = strAttnPolicyId;

                        //SqlParameter p_SingOutTimeF = command[cmdCount].Parameters.Add("SingOutTimeF", SqlDbType.VarChar);
                        //p_SingOutTimeF.Direction = ParameterDirection.Input;
                        //p_SingOutTimeF.Value = strOutTime;
                    }
                    else
                    {
                        SqlParameter p_Status = command[cmdCount].Parameters.Add("Status", SqlDbType.Char);
                        p_Status.Direction = ParameterDirection.Input;
                        p_Status.Value = gRow.Cells[col + 1].Text.Trim();

                        SqlParameter p_AttnPolicyId = command[cmdCount].Parameters.Add("AttnPolicyId", SqlDbType.BigInt);
                        p_AttnPolicyId.Direction = ParameterDirection.Input;
                        p_AttnPolicyId.Value = "0";

                        //SqlParameter p_SingOutTimeF = command[cmdCount].Parameters.Add("SingOutTimeF", SqlDbType.VarChar);
                        //p_SingOutTimeF.Direction = ParameterDirection.Input;
                        //p_SingOutTimeF.Value = "";

                    }


                    strExpr = "EmpID='" + gRow.Cells[0].Text.Trim() + "' AND AttndDate='" + strAttndDate + "'";

                    if (FindInDataTable(dtEmAttn, strExpr) == true)
                    {

                        SqlParameter p_IsUpdate = command[cmdCount].Parameters.Add("IsUpdate", SqlDbType.Char);
                        p_IsUpdate.Direction = ParameterDirection.Input;
                        p_IsUpdate.Value = "Y";

                    }
                    else
                    {

                        SqlParameter p_IsUpdate = command[cmdCount].Parameters.Add("IsUpdate", SqlDbType.Char);
                        p_IsUpdate.Direction = ParameterDirection.Input;
                        p_IsUpdate.Value = "N";

                    }

                }
                cmdCount++;
            }
            
            i++;

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
    protected bool FindInDataTable(DataTable dtEmAttn, string strExpr)
    {
        DataRow[] foundRows;
        foundRows = dtEmAttn.Select(strExpr);
        if(foundRows.Length>0)
            return true;
        else
            return false;
    }
    protected string FindInDataTableReturnValue(DataTable dt, string strExpr)
    {
        DataRow[] foundRows;
        foundRows = dt.Select(strExpr);
        if (foundRows.Length > 0)
            return foundRows[0]["OutTime"].ToString();
        else
            return "";
    }


	public RoasterManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
