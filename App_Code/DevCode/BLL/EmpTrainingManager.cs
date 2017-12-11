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
///for EmpTrainingManager
/// </summary>
public class EmpTrainingManager
{
    DBConnector objDC = new DBConnector();
	
    
    //Select EmpEdu
    public DataTable SelectEmpTraining(string EmpID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpTraining");
        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;
        objDC.CreateDSFromProc(command, "EmpTraining");
        return objDC.ds.Tables["EmpTraining"];
    }


    public DataTable SelectEmpTrainingNeed(string EmpID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpTrainingNeed");
        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;
        objDC.CreateDSFromProc(command, "TrainingNeed");
        return objDC.ds.Tables["TrainingNeed"];
    }

    public DataTable SelectEmpTrainingCompleted(string EmpID)
    {
        SqlCommand command = new SqlCommand("proc_Select_EmpTrainingCompleted");
        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;
        objDC.CreateDSFromProc(command, "SelectEmpTrainingCompleted");
        return objDC.ds.Tables["SelectEmpTrainingCompleted"];
    }


    public void DeleteEmpTraining(string EmpID,string StrTrainingid)
    {
        SqlCommand command = new SqlCommand("proc_Delete_EmpTraining");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        SqlParameter p_TrainId = command.Parameters.Add("TrainId", SqlDbType.Char);
        p_TrainId.Direction = ParameterDirection.Input;
        p_TrainId.Value = StrTrainingid;


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
    public void InsertEmpTraining(clsEmpTraining  objEmpTrain, string strIsUpdate, string strIsDelete)
    {
        // sproc functionality
        SqlCommand command = new SqlCommand("proc_Insert_EmpTraining");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = objEmpTrain.EmpID ;



        SqlParameter p_TrainingName = command.Parameters.Add("TrainingName", SqlDbType.VarChar);
        p_TrainingName.Direction = ParameterDirection.Input;
        p_TrainingName.Value = objEmpTrain.TrainingName;


       
        SqlParameter p_Vanue = command.Parameters.Add("Vanue", SqlDbType.VarChar);
        p_Vanue.Direction = ParameterDirection.Input;
        p_Vanue.Value = objEmpTrain.Vanue;

        SqlParameter p_Duration = command.Parameters.Add("Duration", SqlDbType.BigInt);
        p_Duration.Direction = ParameterDirection.Input;
        p_Duration.Value = objEmpTrain.Duration;

        SqlParameter p_CountryID = command.Parameters.Add("CountryID", SqlDbType.VarChar);
        p_CountryID.Direction = ParameterDirection.Input;
        p_CountryID.Value = objEmpTrain.countryID;

        SqlParameter p_StartFrom = command.Parameters.Add("StartFrom", SqlDbType.VarChar);
        p_StartFrom.Direction = ParameterDirection.Input;
        p_StartFrom.Value = Common.ReturnDate (objEmpTrain.StartFrom);

        SqlParameter p_EndFrom = command.Parameters.Add("EndFrom", SqlDbType.VarChar);
        p_EndFrom.Direction = ParameterDirection.Input;
        p_EndFrom.Value = Common.ReturnDate (objEmpTrain.EndFrom);

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = objEmpTrain.InsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = objEmpTrain.InsertedDate;



        SqlParameter p_SponsorId = command.Parameters.Add("SponsorId", SqlDbType.BigInt);
        p_SponsorId.Direction = ParameterDirection.Input;
        p_SponsorId.Value = objEmpTrain.SponsorId;

        SqlParameter p_Organized = command.Parameters.Add("Organized", SqlDbType.VarChar);
        p_Organized.Direction = ParameterDirection.Input;
        p_Organized.Value = objEmpTrain.Organized;

        SqlParameter p_EventId = command.Parameters.Add("EventId", SqlDbType.BigInt);
        p_EventId.Direction = ParameterDirection.Input;
        p_EventId.Value = objEmpTrain.EventId;

        SqlParameter p_DisiciplineId = command.Parameters.Add("DisiciplineId", SqlDbType.BigInt);
        p_DisiciplineId.Direction = ParameterDirection.Input;
        p_DisiciplineId.Value = objEmpTrain.DisiciplineId;


        SqlParameter p_TrainId = command.Parameters.Add("TrainId", SqlDbType.Char);
        p_TrainId.Direction = ParameterDirection.Input;
        p_TrainId.Value = objEmpTrain.TrainId;


        SqlParameter p_IsUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = strIsUpdate;

        SqlParameter p_IsDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = strIsDelete;

        try
        {
            objDC.ExecuteQuery(command ) ;
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

    public void DeleteEmpTrainingNeed(string EmpID, string StrTrainingId)
    {
        SqlCommand command = new SqlCommand("proc_Delete_EmpTrainingNeed");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        SqlParameter p_Emptraid = command.Parameters.Add("Emptraid", SqlDbType.Char);
        p_Emptraid.Direction = ParameterDirection.Input;
        p_Emptraid.Value = StrTrainingId;


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

    public void DeleteEmpTrainingCompleted(string EmpID, string StrTrainingId)
    {
        SqlCommand command = new SqlCommand("proc_Delete_EmpTrainingCompleted");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpID = command.Parameters.Add("EmpID", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = EmpID;

        SqlParameter p_Emptraid = command.Parameters.Add("Emptraid", SqlDbType.Char);
        p_Emptraid.Direction = ParameterDirection.Input;
        p_Emptraid.Value = StrTrainingId;


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

    public void EmpTrainingAttended(GridView GrEmpTrain, int CheckedDataCount, string strAttendedChecked,
        string strEmpId, string IsAttended, string TrainDates, string strInsBy, string strInsDate)
    {
        int i = 0;
        int j = 0;
        char[] splitter = { ',' };

        string[] arinfo = new string[10];
        arinfo = Common.str_split(TrainDates, splitter);


        SqlCommand[] cmd;
        cmd = new SqlCommand[CheckedDataCount + arinfo.Length];
        foreach (GridViewRow gRow in GrEmpTrain.Rows)
        {
            CheckBox chBox = new CheckBox();
            if (strAttendedChecked == "Y")
                chBox = (CheckBox)gRow.Cells[8].FindControl("chkIsAttended");
            if (chBox.Checked == true)
            {
                cmd[i] = new SqlCommand("proc_EmpTraining_Attended");
                cmd[i].CommandType = CommandType.StoredProcedure;

                SqlParameter p_EmpId = cmd[i].Parameters.Add("EmpId", SqlDbType.Char);
                p_EmpId.Direction = ParameterDirection.Input;
                p_EmpId.Value = strEmpId;

                SqlParameter p_TrainId = cmd[i].Parameters.Add("TrainId", SqlDbType.BigInt);
                p_TrainId.Direction = ParameterDirection.Input;
                //p_TrainId.Value = GrEmpTrain.DataKeys[i].Values[1].ToString();
                p_TrainId.Value = GrEmpTrain.DataKeys[j].Values[1].ToString();

                SqlParameter p_Attended = cmd[i].Parameters.Add("IsAttended", SqlDbType.Char);
                p_Attended.Direction = ParameterDirection.Input;
                p_Attended.Value = IsAttended;

                SqlParameter p_InsertedBy = cmd[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                p_InsertedBy.Direction = ParameterDirection.Input;
                p_InsertedBy.Value = strInsBy;

                SqlParameter p_InsertedDate = cmd[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                p_InsertedDate.Direction = ParameterDirection.Input;
                p_InsertedDate.Value = strInsDate;
                i++;
            }
            j++;
        }

        //Insert Or Update Traning Status in Attandance Table 
        foreach (GridViewRow gRow in GrEmpTrain.Rows)
        {
            CheckBox chBox = new CheckBox();
            if (strAttendedChecked == "Y")
                chBox = (CheckBox)gRow.Cells[8].FindControl("chkIsAttended");
            if (chBox.Checked == true)
            {
                int row = i;
                int k = row;
                for (k = row; k < arinfo.Length + row; k++)
                {
                    cmd[k] = UpdateAttendanceForTraning(strEmpId, arinfo[k - row], strInsBy, strInsDate);
                }
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


    public SqlCommand UpdateAttendanceForTraning(string strEmpId, string strAttndDate, string strInsBy, string strInsDate)
    {
        SqlCommand cmd = new SqlCommand("proc_In_Or_Up_Attandance_TrainStatus");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpID = cmd.Parameters.Add("EmpId", SqlDbType.Char);
        p_EmpID.Direction = ParameterDirection.Input;
        p_EmpID.Value = strEmpId;

        SqlParameter p_AttndDate = cmd.Parameters.Add("AttndDate", SqlDbType.DateTime);
        p_AttndDate.Direction = ParameterDirection.Input;
        p_AttndDate.Value = strAttndDate;

        SqlParameter p_Status = cmd.Parameters.Add("Status", SqlDbType.Char);
        p_Status.Direction = ParameterDirection.Input;
        p_Status.Value = "T";

        //SqlParameter p_LeaveFlag = cmd.Parameters.Add("LeaveFlag", SqlDbType.Char);
        //p_LeaveFlag.Direction = ParameterDirection.Input;
        //p_LeaveFlag.Value = strLeaveAbbrName;

        SqlParameter p_InsertedBy = cmd.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = cmd.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = Convert.ToDateTime(strInsDate);

        return cmd;
    }


    public void InsertEmpTrainingNeed(clsEmpTraninigNeed objEmpTrain, string strIsUpdate, string strIsDelete, GridView grEmp)
    {
        int i = 0;
        long EmpTrainID = Convert.ToInt64(objEmpTrain.Emptraid);

        SqlCommand[] command = new SqlCommand[grEmp.Rows.Count];

        foreach (GridViewRow gRow in grEmp.Rows)
        {
            CheckBox chkB = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            if (chkB.Checked == true)
            {
                // sproc functionality
                command[i] = new SqlCommand("proc_Insert_EmpTrainingNeed");
                command[i].CommandType = CommandType.StoredProcedure;


                SqlParameter p_EmpID = command[i].Parameters.Add("EmpID", SqlDbType.Char);
                p_EmpID.Direction = ParameterDirection.Input;
                p_EmpID.Value = gRow.Cells[1].Text.Trim();

                SqlParameter p_Emptraid = command[i].Parameters.Add("Emptraid", SqlDbType.Char);
                p_Emptraid.Direction = ParameterDirection.Input;
                p_Emptraid.Value = EmpTrainID;

                SqlParameter p_fisicalYear = command[i].Parameters.Add("fisicalYear", SqlDbType.VarChar);
                p_fisicalYear.Direction = ParameterDirection.Input;
                p_fisicalYear.Value = objEmpTrain.fisicalYear;


                SqlParameter p_TrainingNeed = command[i].Parameters.Add("TrainingNeed", SqlDbType.VarChar);
                p_TrainingNeed.Direction = ParameterDirection.Input;
                p_TrainingNeed.Value = objEmpTrain.TrainingNeed;


                SqlParameter p_Disciplineid = command[i].Parameters.Add("Disciplineid", SqlDbType.BigInt);
                p_Disciplineid.Direction = ParameterDirection.Input;
                p_Disciplineid.Value = objEmpTrain.Disciplineid;

                SqlParameter p_Quarter = command[i].Parameters.Add("Quarter", SqlDbType.VarChar);
                p_Quarter.Direction = ParameterDirection.Input;
                p_Quarter.Value = objEmpTrain.Quarter;


                SqlParameter p_Week = command[i].Parameters.Add("Week", SqlDbType.VarChar);
                p_Week.Direction = ParameterDirection.Input;
                p_Week.Value = objEmpTrain.Week;


                SqlParameter p_Priority = command[i].Parameters.Add("Priority", SqlDbType.VarChar);
                p_Priority.Direction = ParameterDirection.Input;
                p_Priority.Value = objEmpTrain.Priority;

                SqlParameter p_Done = command[i].Parameters.Add("Done", SqlDbType.VarChar);
                p_Done.Direction = ParameterDirection.Input;
                p_Done.Value = objEmpTrain.Done;


                SqlParameter p_StartFrom = command[i].Parameters.Add("StartFrom", SqlDbType.VarChar);
                p_StartFrom.Direction = ParameterDirection.Input;
                p_StartFrom.Value = Common.ReturnDate(objEmpTrain.StartFrom);

                SqlParameter p_EndFrom = command[i].Parameters.Add("EndFrom", SqlDbType.VarChar);
                p_EndFrom.Direction = ParameterDirection.Input;
                p_EndFrom.Value = Common.ReturnDate(objEmpTrain.EndFrom);

                SqlParameter p_InsertedBy = command[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                p_InsertedBy.Direction = ParameterDirection.Input;
                p_InsertedBy.Value = objEmpTrain.InsertedBy;

                SqlParameter p_InsertedDate = command[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                p_InsertedDate.Direction = ParameterDirection.Input;
                p_InsertedDate.Value = objEmpTrain.InsertedDate;

                SqlParameter p_IsUpdate = command[i].Parameters.Add("IsUpdate", SqlDbType.Char);
                p_IsUpdate.Direction = ParameterDirection.Input;
                p_IsUpdate.Value = strIsUpdate;

                SqlParameter p_IsDelete = command[i].Parameters.Add("IsDelete", SqlDbType.Char);
                p_IsDelete.Direction = ParameterDirection.Input;
                p_IsDelete.Value = strIsDelete;
                i++;
                EmpTrainID++;
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

    public void InsertEmpTrainingCompleted(clsEmpTraninigCompleted objEmpTrain, string strIsUpdate, string strIsDelete, GridView grEmp)
    {
        int i = 0;
        long EmpTrainID = Convert.ToInt64(objEmpTrain.Emptraid);

        SqlCommand[] command = new SqlCommand[grEmp.Rows.Count];

        foreach (GridViewRow gRow in grEmp.Rows)
        {
            CheckBox chkB = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            if (chkB.Checked == true)
            {
                // sproc functionality
                command[i] = new SqlCommand("proc_Insert_EmpTrainingCompleted");
                command[i].CommandType = CommandType.StoredProcedure;


                SqlParameter p_EmpID = command[i].Parameters.Add("EmpID", SqlDbType.Char);
                p_EmpID.Direction = ParameterDirection.Input;
                p_EmpID.Value = gRow.Cells[1].Text.Trim();

                SqlParameter p_Emptraid = command[i].Parameters.Add("Emptraid", SqlDbType.Char);
                p_Emptraid.Direction = ParameterDirection.Input;
                p_Emptraid.Value = EmpTrainID;

                SqlParameter p_fisicalYear = command[i].Parameters.Add("fisicalYear", SqlDbType.VarChar);
                p_fisicalYear.Direction = ParameterDirection.Input;
                p_fisicalYear.Value = objEmpTrain.fisicalYear;


                SqlParameter p_TrainingNeed = command[i].Parameters.Add("TrainingNeed", SqlDbType.VarChar);
                p_TrainingNeed.Direction = ParameterDirection.Input;
                p_TrainingNeed.Value = objEmpTrain.TrainingNeed;


                SqlParameter p_Disciplineid = command[i].Parameters.Add("Disciplineid", SqlDbType.BigInt);
                p_Disciplineid.Direction = ParameterDirection.Input;
                p_Disciplineid.Value = objEmpTrain.Disciplineid;

                SqlParameter p_Quarter = command[i].Parameters.Add("Quarter", SqlDbType.VarChar);
                p_Quarter.Direction = ParameterDirection.Input;
                p_Quarter.Value = objEmpTrain.Quarter;


                SqlParameter p_Week = command[i].Parameters.Add("Week", SqlDbType.VarChar);
                p_Week.Direction = ParameterDirection.Input;
                p_Week.Value = objEmpTrain.Week;


                SqlParameter p_Venue = command[i].Parameters.Add("Venue", SqlDbType.VarChar);
                p_Venue.Direction = ParameterDirection.Input;
                p_Venue.Value = objEmpTrain.Venue;

                SqlParameter p_CountryID = command[i].Parameters.Add("CountryID", SqlDbType.BigInt);
                p_CountryID.Direction = ParameterDirection.Input;
                p_CountryID.Value = objEmpTrain.Country;

                SqlParameter p_Done = command[i].Parameters.Add("Done", SqlDbType.VarChar);
                p_Done.Direction = ParameterDirection.Input;
                p_Done.Value = objEmpTrain.Done;


                SqlParameter p_StartFrom = command[i].Parameters.Add("StartFrom", SqlDbType.VarChar);
                p_StartFrom.Direction = ParameterDirection.Input;
                p_StartFrom.Value = Common.ReturnDate(objEmpTrain.StartFrom);

                SqlParameter p_EndFrom = command[i].Parameters.Add("EndFrom", SqlDbType.VarChar);
                p_EndFrom.Direction = ParameterDirection.Input;
                p_EndFrom.Value = Common.ReturnDate(objEmpTrain.EndFrom);

                SqlParameter p_InsertedBy = command[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                p_InsertedBy.Direction = ParameterDirection.Input;
                p_InsertedBy.Value = objEmpTrain.InsertedBy;

                SqlParameter p_InsertedDate = command[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                p_InsertedDate.Direction = ParameterDirection.Input;
                p_InsertedDate.Value = objEmpTrain.InsertedDate;

                SqlParameter p_IsUpdate = command[i].Parameters.Add("IsUpdate", SqlDbType.Char);
                p_IsUpdate.Direction = ParameterDirection.Input;
                p_IsUpdate.Value = strIsUpdate;

                SqlParameter p_IsDelete = command[i].Parameters.Add("IsDelete", SqlDbType.Char);
                p_IsDelete.Direction = ParameterDirection.Input;
                p_IsDelete.Value = strIsDelete;
                i++;
                EmpTrainID++;
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

    public DataTable GetEmpTrainingReportData(string strFinYear, string strQuarter, string strDisc)
    {
        string strCond="";
        string strSQL = "SELECT ETN.*,DI.DisiciplineTitel,FYL.FiscalYrTitle,EI.FullName,JT.JobTitle,DV.DIVISIONNAME,DP.DeptName "
                    + " FROM EmpTrainingNeed ETN,Disiciplineinfo DI,FiscalYearList FYL, "
                    + " EmpInfo EI,JobTitle JT,DivisionList DV,DeptList DP "
                    + " WHERE ETN.Disciplineid=DI.DisiciplineId "
                    + " AND ETN.fisicalYear=FYL.FiscalYrId "
                    + " AND ETN.Empid=EI.EmpId "
                    + " AND EI.DesgId=JT.JbTlId "
                    + " AND EI.DivisionID=DV.DivisionID "
                    + " AND EI.DeptId=DP.DeptId ";
        if (strFinYear != "-1")
            strCond = " AND ETN.fisicalYear=@fisicalYear";
        if (strQuarter != "Nil")
            strCond = strCond + " AND ETN.QUARTER=@QUARTER";
        if (strDisc != "99999")
            strCond = strCond + " AND ETN.Disciplineid=@Disciplineid";
        strSQL = strSQL + strCond + " ORDER BY  ETN.fisicalYear,ETN.QUARTER,ETN.Disciplineid,ETN.EMPID";
        SqlCommand cmd = new SqlCommand(strSQL);
        cmd.CommandType = CommandType.Text;
        if (strFinYear != "-1")
        {
            SqlParameter p_fisicalYear = cmd.Parameters.Add("fisicalYear", SqlDbType.BigInt);
            p_fisicalYear.Direction = ParameterDirection.Input;
            p_fisicalYear.Value = strFinYear;
        }
        if (strQuarter != "Nil")
        {
            SqlParameter p_QUARTER = cmd.Parameters.Add("QUARTER", SqlDbType.VarChar);
            p_QUARTER.Direction = ParameterDirection.Input;
            p_QUARTER.Value = strQuarter;
        }
        if (strDisc != "99999")
        {
            SqlParameter p_Disciplineid = cmd.Parameters.Add("Disciplineid", SqlDbType.BigInt);
            p_Disciplineid.Direction = ParameterDirection.Input;
            p_Disciplineid.Value = strDisc;
        }
        return objDC.CreateDT(cmd, "GetEmpTrainingReportData");

    }

}
