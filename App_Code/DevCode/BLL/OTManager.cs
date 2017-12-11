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
/// Summary description for OTManager
/// </summary>
public class OTManager
{
    DBConnector objDC = new DBConnector();

    public void InsertOTApproval(GridView grv,int CheckedDataCount,string strInsBy,string strInsDate,string strIsApprove,string strAppStatus)
    {
        string strHM = "";
        int i = 0;
        SqlCommand[] cmd;

        cmd = new SqlCommand[CheckedDataCount];
        foreach (GridViewRow gRow in grv.Rows)
        {
            CheckBox chBox = new CheckBox();
            chBox = (CheckBox)gRow.Cells[1].FindControl("chkBox");
            if (chBox.Checked == true)
            {
                if(strIsApprove=="Y")
                    cmd[i] = new SqlCommand("proc_Approve_OT");
                else
                    cmd[i] = new SqlCommand("proc_Recommend_OT");
                cmd[i].CommandType = CommandType.StoredProcedure;

                SqlParameter p_EmpId = cmd[i].Parameters.Add("EmpId", SqlDbType.Char);
                p_EmpId.Direction = ParameterDirection.Input;
                p_EmpId.Value = gRow.Cells[2].Text;

                SqlParameter p_AttndDate = cmd[i].Parameters.Add("AttndDate", SqlDbType.DateTime);
                p_AttndDate.Direction = ParameterDirection.Input;
                p_AttndDate.Value = Common.ReturnDate(gRow.Cells[6].Text);

                TextBox txtExtTimeH = new TextBox();
                txtExtTimeH = (TextBox)gRow.Cells[14].FindControl("txtOTH");

                TextBox txtExtTimeM = new TextBox();
                txtExtTimeM = (TextBox)gRow.Cells[15].FindControl("txtOTM");

                strHM = Convert.ToString(Convert.ToInt64(txtExtTimeH.Text.Trim()) * 60 + Convert.ToInt64(txtExtTimeM.Text.Trim()));

                SqlParameter p_ExtraTimeWorkedF = cmd[i].Parameters.Add("ExtraTimeWorkedF", SqlDbType.BigInt);
                p_ExtraTimeWorkedF.Direction = ParameterDirection.Input;
                p_ExtraTimeWorkedF.Value = strHM;

                SqlParameter p_IsForcedTimeSet = cmd[i].Parameters.Add("IsForcedTimeSet", SqlDbType.Char);
                p_IsForcedTimeSet.Direction = ParameterDirection.Input;
                p_IsForcedTimeSet.Value = "Y";

                SqlParameter p_InsertedDate = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                p_InsertedDate.Direction = ParameterDirection.Input;
                p_InsertedDate.Value = strInsDate;

                SqlParameter p_InsertedBy = cmd[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                p_InsertedBy.Direction = ParameterDirection.Input;
                p_InsertedBy.Value = strInsBy;

                SqlParameter p_AppStatus = cmd[i].Parameters.Add("AppStatus", SqlDbType.Char);
                p_AppStatus.Direction = ParameterDirection.Input;
                p_AppStatus.Value = strAppStatus;

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

	public OTManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
