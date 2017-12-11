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
/// Summary description for ImportToolMgr
/// </summary>
public class ImportToolMgr
{
    DBConnector objDC = new DBConnector();
    EmpLeaveProfileManager objELPMgr = new EmpLeaveProfileManager();
	public ImportToolMgr()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public bool InsertEmpData(GridView gr,string strUser)
    {
        int inGrRowChkCount = 0;
        foreach (GridViewRow gRow in gr.Rows)
        {
            CheckBox chkEmp = (CheckBox)gRow.FindControl("chkEmp");
            if (chkEmp.Checked == true)
            {
                inGrRowChkCount++;
            }
        }

        SqlCommand[] cmd;
        cmd = new SqlCommand[inGrRowChkCount*5];
        int i = 0;
        try
        {
            foreach (GridViewRow gRow in gr.Rows)
            {
                CheckBox chkEmp = (CheckBox)gRow.FindControl("chkEmp");
                if (chkEmp.Checked == true)
                {
                    HiddenField hfExist = (HiddenField)gRow.FindControl("hfIsExist");
                    cmd[i] = new SqlCommand(GetCommandString(hfExist.Value));

                    SqlParameter p_EmpID = cmd[i].Parameters.Add("EmpID", SqlDbType.Char);
                    p_EmpID.Direction = ParameterDirection.Input;
                    p_EmpID.Value = gRow.Cells[1].Text.Trim();

                    SqlParameter p_FullName = cmd[i].Parameters.Add("FullName", SqlDbType.VarChar);
                    p_FullName.Direction = ParameterDirection.Input;
                    p_FullName.Value = gRow.Cells[2].Text.Trim();

                    SqlParameter p_BranchId = cmd[i].Parameters.Add("BranchId", SqlDbType.VarChar);
                    p_BranchId.Direction = ParameterDirection.Input;
                    p_BranchId.Value = gr.DataKeys[i].Values[1].ToString().Trim();

                    SqlParameter p_DivisionId = cmd[i].Parameters.Add("DivisionId", DBNull.Value);
                    p_DivisionId.Direction = ParameterDirection.Input;
                    p_DivisionId.IsNullable = true;
                    if (gr.DataKeys[i].Values[2].ToString().Trim() != "")
                        p_DivisionId.Value = gr.DataKeys[i].Values[2].ToString().Trim();
                  
                    SqlParameter p_DeptId = cmd[i].Parameters.Add("DeptId", DBNull.Value);
                    p_DeptId.Direction = ParameterDirection.Input;
                    p_DeptId.Value = "99999";
                    //if (gr.DataKeys[i].Values[4].ToString().Trim() != "")
                        //p_DeptId.Value = gr.DataKeys[i].Values[4].ToString().Trim();                   

                    SqlParameter p_DesgId = cmd[i].Parameters.Add("DesgId", DBNull.Value);
                    p_DesgId.Direction = ParameterDirection.Input;
                    p_DesgId.IsNullable = true;
                    if (gr.DataKeys[i].Values[3].ToString().Trim() != "")
                        p_DesgId.Value = gr.DataKeys[i].Values[3].ToString().Trim();
                    
                    TextBox txtCard = (TextBox)gRow.FindControl("txtCardNo");
                    SqlParameter p_CardNo = cmd[i].Parameters.Add("CardNo", DBNull.Value);
                    p_CardNo.Direction = ParameterDirection.Input;
                    p_CardNo.IsNullable = true;
                    if (txtCard.Text != "")
                        p_CardNo.Value = txtCard.Text;
                   
                    HiddenField hfWeek = (HiddenField)gRow.FindControl("hfWeekend");
                    SqlParameter p_WeekEndID = cmd[i].Parameters.Add("WeekEndID", DBNull.Value);
                    p_WeekEndID.Direction = ParameterDirection.Input;
                    p_WeekEndID.IsNullable = true;
                    if (hfWeek.Value != "")
                        p_WeekEndID.Value = hfWeek.Value;
                    
                    HiddenField hfShf = (HiddenField)gRow.FindControl("hfShift");
                    SqlParameter p_AttnPolicyID = cmd[i].Parameters.Add("AttnPolicyID", DBNull.Value);
                    p_AttnPolicyID.Direction = ParameterDirection.Input;
                    p_AttnPolicyID.IsNullable = true;
                    if (hfShf.Value != "")
                        p_AttnPolicyID.Value = hfShf.Value;

                    HiddenField hfLPack = (HiddenField)gRow.FindControl("hfLeavePkg");
                    SqlParameter p_LPakID = cmd[i].Parameters.Add("LPakID", DBNull.Value);
                    p_LPakID.Direction = ParameterDirection.Input;
                    p_LPakID.IsNullable = true;
                    if (hfLPack.Value != "")
                        p_LPakID.Value = hfLPack.Value;                   

                    CheckBox chkRoaster = (CheckBox)gRow.FindControl("chkRoaster");
                    SqlParameter p_IsRoaster = cmd[i].Parameters.Add("IsRoaster", SqlDbType.Char);
                    p_IsRoaster.Direction = ParameterDirection.Input;
                    p_IsRoaster.Value = chkRoaster.Checked == true ? 'Y' : 'N';

                    CheckBox chkOverT = (CheckBox)gRow.FindControl("chkOT");
                    SqlParameter p_OTNOTALLOWED = cmd[i].Parameters.Add("OTNOTALLOWED", SqlDbType.Char);
                    p_OTNOTALLOWED.Direction = ParameterDirection.Input;
                    p_OTNOTALLOWED.Value = chkOverT.Checked == true ? 'N' : 'Y';

                    SqlParameter p_InsertedBy = cmd[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                    p_InsertedBy.Direction = ParameterDirection.Input;
                    p_InsertedBy.Value = strUser;

                    SqlParameter p_InsertedDate = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                    p_InsertedDate.Direction = ParameterDirection.Input;
                    p_InsertedDate.Value = Common.SetDateTime(DateTime.Now.ToString());

                    SqlParameter p_IsDeleted = cmd[i].Parameters.Add("IsDeleted", SqlDbType.Char);
                    p_IsDeleted.Direction = ParameterDirection.Input;
                    p_IsDeleted.Value = 'N';

                    SqlParameter p_ISSEPERATED = cmd[i].Parameters.Add("ISSEPERATED", SqlDbType.Char);
                    p_ISSEPERATED.Direction = ParameterDirection.Input;
                    p_ISSEPERATED.Value = 'N';

                    SqlParameter p_JoiningDate = cmd[i].Parameters.Add("JoiningDate", SqlDbType.DateTime);
                    p_JoiningDate.Direction = ParameterDirection.Input;
                    p_JoiningDate.Value = gRow.Cells[7].Text.Trim();

                    SqlParameter p_Status = cmd[i].Parameters.Add("Status", SqlDbType.VarChar);
                    p_Status.Direction = ParameterDirection.Input;
                    p_Status.Value = gRow.Cells[8].Text.Trim();

                    // Leave Update
                    //if (hfLPack.Value != "")
                    //{
                    //    SqlCommand[] cmdL;
                    //    cmdL = new SqlCommand[5];
                    //    cmdL = objELPMgr.ImportEmpLevProfile(p_EmpID.Value.ToString(), hfLPack.Value.ToString(), gRow.Cells[7].Text.Trim(), strUser);
                    //    foreach (SqlCommand cmdTmp in cmdL)
                    //    {
                    //        if (cmdTmp != null)
                    //        {
                    //            i++;
                    //            cmd[i] = cmdTmp;
                    //        }
                    //    }
                    //}
                    i++;
                }
            }

            objDC.MakeTransaction(cmd);
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
            return false;
        }
        
    }
    protected String GetCommandString(string strIsUpdate)
    {
        string strSQL = "";
        if (strIsUpdate == "Y")
        {
            strSQL = " UPDATE EmpInfo set "
                 + " FullName=@FullName, "
                 + " BranchId=@BranchId,  "
                 + " DivisionId=@DivisionId,  "
                 + " DeptId=@DeptId,  "
                 + " DesgId=@DesgId,  "
                 + " CardNo=@CardNo,      "
                 + " WeekEndID=@WeekEndID,      "
                 + " AttnPolicyID=@AttnPolicyID, "
                 + " LPakID=@LPakID, "
                 + " IsRoaster=@IsRoaster, "
                 + " OTNOTALLOWED=@OTNOTALLOWED, "
                 + " UpdatedBy=@InsertedBy, "
                 + " UpdatedDate=@InsertedDate, "
                 + " IsDeleted=@IsDeleted, "
                 + " ISSEPERATED=@ISSEPERATED, "
                 + " JoiningDate=@JoiningDate, "
                 + " Status=@Status "
                 + " WHERE EmpID=@EmpID ";
        }
        else
        {
            strSQL = " INSERT INTO EmpInfo(EmpID,FullName, BranchId,DivisionId,DeptId,DesgId, CardNo, WeekEndID, AttnPolicyID, "
                            + " LPakID,IsRoaster, OTNOTALLOWED, InsertedBy, InsertedDate,IsDeleted,ISSEPERATED,JoiningDate,Status)"
                + " VALUES (@EmpID,@FullName,@BranchId,@DivisionId,@DeptId,@DesgId, @CardNo, @WeekEndID, @AttnPolicyID, "
                        + " @LPakID,@IsRoaster, @OTNOTALLOWED,@InsertedBy,@InsertedDate,@IsDeleted,@ISSEPERATED,@JoiningDate,@Status)";
         
        }
        return strSQL;
    }

    public void InsertList(DataTable dtBranch,DataTable dtDivision,DataTable dtBrWsDiv,
        DataTable dtJobTitle, DataTable dtDept, string strInsBy, string strInsDate)
    {
        SqlCommand[] cmd;
        cmd = new SqlCommand[dtBranch.Rows.Count + dtDivision.Rows.Count + dtBrWsDiv.Rows.Count +
            dtDept.Rows.Count + dtJobTitle.Rows.Count + 2];        
        int i = 0;
        int j = 0;
        foreach (DataRow BrRow in dtBranch.Rows)
        {
            cmd[i] = InsertBranch(BrRow["BranchId"].ToString() , BrRow["BranchName"].ToString(), strInsBy, strInsDate);
            i++;
        }
        foreach (DataRow DivRow in dtDivision.Rows)
        {
            cmd[i] = InsertDivision(DivRow["DivisionId"].ToString(), DivRow["DivisionName"].ToString(), strInsBy, strInsDate);
            i++;
        }

        cmd[i] = DeleteBranchWsDivision();
        i++;
        foreach (DataRow BrWsDivRow in dtBrWsDiv.Rows)
        {
            cmd[i] = InsertBranchWsDivision(BrWsDivRow["BranchId"].ToString(), BrWsDivRow["DivisionId"].ToString());
            i++;
        }
        foreach (DataRow JobTileRow in dtJobTitle.Rows)
        {
            cmd[i] = InsertJobTitle(JobTileRow["JobTiltleId"].ToString(), JobTileRow["JobTitleName"].ToString(), strInsBy, strInsDate);
            i++;
        }

        foreach (DataRow DeptRow in dtDept.Rows)
        {
            cmd[i] = InsertDeptList(DeptRow["DepartmentId"].ToString(), DeptRow["DepartmentName"].ToString(), strInsBy, strInsDate);
            i++;
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

    public SqlCommand InsertBranch(string BranchId,string BranchName,  string strInsBy, string strInsDate)
    {        
        SqlCommand cmd = new SqlCommand("proc_INSERT_Branch");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_BranchId = cmd.Parameters.Add("BranchId", SqlDbType.VarChar);
        p_BranchId.Direction = ParameterDirection.Input;
        p_BranchId.Value = BranchId;

        SqlParameter p_BranchName = cmd.Parameters.Add("BranchName", SqlDbType.VarChar);
        p_BranchName.Direction = ParameterDirection.Input;
        p_BranchName.Value = BranchName;

        SqlParameter p_IsActive = cmd.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = "Y";

        SqlParameter p_IsDeleted = cmd.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_IsDeleted.Direction = ParameterDirection.Input;
        p_IsDeleted.Value = "N";

        SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        return cmd;
    }

    public SqlCommand InsertDivision(string DivisionID, string DivisionName, string strInsBy, string strInsDate)
    {
        //INSERT INTO DETAILS TABLE
        SqlCommand cmd = new SqlCommand("proc_INSERT_Division");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_DivisionID = cmd.Parameters.Add("DivisionID", SqlDbType.VarChar);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = DivisionID;

        SqlParameter p_DivisionName = cmd.Parameters.Add("DivisionName", SqlDbType.VarChar);
        p_DivisionName.Direction = ParameterDirection.Input;
        p_DivisionName.Value = DivisionName;

        SqlParameter p_IsActive = cmd.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = "Y";

        SqlParameter p_IsDeleted = cmd.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_IsDeleted.Direction = ParameterDirection.Input;
        p_IsDeleted.Value = "N";

        SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        return cmd;
    }

    public SqlCommand InsertEmpInfo(string EmpId, string FullName, string BranchId, string DivisionId, string Status, string strInsBy, string strInsDate)
    {
        SqlCommand cmd = new SqlCommand("proc_Insert_EmpInfo_CSV");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpId = cmd.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = EmpId;

        SqlParameter p_FullName = cmd.Parameters.Add("FullName", SqlDbType.VarChar);
        p_FullName.Direction = ParameterDirection.Input;
        p_FullName.Value = FullName;

        SqlParameter p_BranchId = cmd.Parameters.Add("BranchId", SqlDbType.VarChar);
        p_BranchId.Direction = ParameterDirection.Input;
        p_BranchId.Value = BranchId;

        SqlParameter p_DivisionId = cmd.Parameters.Add("DivisionId", SqlDbType.VarChar);
        p_DivisionId.Direction = ParameterDirection.Input;
        p_DivisionId.Value = DivisionId;

        SqlParameter p_Status = cmd.Parameters.Add("Status", SqlDbType.VarChar);
        p_Status.Direction = ParameterDirection.Input;
        p_Status.Value = Status;

        SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        return cmd;
    }

    public SqlCommand InsertBranchWsDivision(string BranchId, string DivisionID)
    {
        //INSERT INTO DETAILS TABLE
        SqlCommand cmd = new SqlCommand("proc_Insert_BranchWiseDivision");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_BranchId = cmd.Parameters.Add("BranchId", SqlDbType.VarChar);
        p_BranchId.Direction = ParameterDirection.Input;
        p_BranchId.Value = BranchId;

        SqlParameter p_DivisionID = cmd.Parameters.Add("DivisionID", SqlDbType.VarChar);
        p_DivisionID.Direction = ParameterDirection.Input;
        p_DivisionID.Value = DivisionID;     

        return cmd;
    }

    public SqlCommand DeleteBranchWsDivision()
    {
        //INSERT INTO DETAILS TABLE
        SqlCommand cmd = new SqlCommand("proc_Delete_BranchWiseDivision");
        cmd.CommandType = CommandType.StoredProcedure;

        return cmd;
    }

    public SqlCommand InsertJobTitle(string JbTlId, string JobTitle, string strInsBy, string strInsDate)
    {
        //INSERT INTO DETAILS TABLE
        SqlCommand cmd = new SqlCommand("proc_Insert_JobTilte");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_JbTlId = cmd.Parameters.Add("JbTlId", SqlDbType.VarChar);
        p_JbTlId.Direction = ParameterDirection.Input;
        p_JbTlId.Value = JbTlId;

        SqlParameter p_JobTitle = cmd.Parameters.Add("JobTitle", SqlDbType.VarChar);
        p_JobTitle.Direction = ParameterDirection.Input;
        p_JobTitle.Value = JobTitle;

        SqlParameter p_IsActive = cmd.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = "Y";

        SqlParameter p_IsDeleted = cmd.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_IsDeleted.Direction = ParameterDirection.Input;
        p_IsDeleted.Value = "N";

        SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        return cmd;
    }

    public SqlCommand InsertDeptList(string DeptId, string DeptName, string strInsBy, string strInsDate)
    {
        //INSERT INTO DETAILS TABLE
        SqlCommand cmd = new SqlCommand("proc_Insert_Department");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_DeptId = cmd.Parameters.Add("DeptId", SqlDbType.VarChar);
        p_DeptId.Direction = ParameterDirection.Input;
        p_DeptId.Value = DeptId;

        SqlParameter p_DeptName = cmd.Parameters.Add("DeptName", SqlDbType.VarChar);
        p_DeptName.Direction = ParameterDirection.Input;
        p_DeptName.Value = DeptName;

        SqlParameter p_IsActive = cmd.Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = "Y";

        SqlParameter p_IsDeleted = cmd.Parameters.Add("IsDeleted", SqlDbType.Char);
        p_IsDeleted.Direction = ParameterDirection.Input;
        p_IsDeleted.Value = "N";

        SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        return cmd;
    }

    
}

