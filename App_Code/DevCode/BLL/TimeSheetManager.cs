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
/// Summary description for TimeSheetManager
/// </summary>
public class TimeSheetManager
{
    DBConnector objDC = new DBConnector();

    public SqlCommand Insert_TimeSheet(string strTransID, string EMPID, string VMonth, string VYear, string FiscalYrId,
         string gadCode, string accLine, string strVDate, string VHour, string VTask, string INSERTEDBY, string INSERTEDDATE, string aStatus, string SupEmpId,
        string strIsUpdate, string strIsDelete)
    {        
        SqlCommand command = new SqlCommand("PROC_INSERT_TIMESHEET");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_TRANSID = command.Parameters.Add("TRANSID", SqlDbType.BigInt);
        p_TRANSID.Direction = ParameterDirection.Input;
        p_TRANSID.Value = string.IsNullOrEmpty(strTransID) == true ? "0" : strTransID;
        //p_TRANSID.Value = string.IsNullOrEmpty(strTransID) == true ? 0 : Convert.ToInt64(strTransID);
        
        SqlParameter p_EMPID = command.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = EMPID;

        SqlParameter p_VMonth = command.Parameters.Add("VMonth", SqlDbType.VarChar);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = VMonth;

        SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.VarChar);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = VYear;

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input; ;
        p_FiscalYrId.Value = FiscalYrId;

        SqlParameter p_GADCODE = command.Parameters.Add("GADCODE", SqlDbType.Char);
        p_GADCODE.Direction = ParameterDirection.Input;
        p_GADCODE.Value = gadCode;

        SqlParameter p_AccLine = command.Parameters.Add("AccLine", SqlDbType.VarChar);
        p_AccLine.Direction = ParameterDirection.Input;
        p_AccLine.Value = accLine;

        SqlParameter p_VDate = command.Parameters.Add("VDate", SqlDbType.DateTime);
        p_VDate.Direction = ParameterDirection.Input;
        p_VDate.Value = strVDate;

        SqlParameter p_VHour = command.Parameters.Add("VHour", DBNull.Value);
        p_VHour.Direction = ParameterDirection.Input;
        p_VHour.IsNullable = true;
        if (VHour != "")
            p_VHour.Value = VHour;

        SqlParameter p_VTask = command.Parameters.Add("VTask", SqlDbType.VarChar);
        p_VTask.Direction = ParameterDirection.Input;
        p_VTask.Value = VTask;

        SqlParameter p_INSERTEDBY = command.Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = INSERTEDBY;

        SqlParameter p_INSERTEDDATE = command.Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDDATE.Direction = ParameterDirection.Input;
        p_INSERTEDDATE.Value = INSERTEDDATE;

        SqlParameter p_aStatus = command.Parameters.Add("aStatus", SqlDbType.Char);
        p_aStatus.Direction = ParameterDirection.Input;
        p_aStatus.Value = aStatus;

        SqlParameter p_SupEmpId = command.Parameters.Add("SupEmpId", SqlDbType.Char);
        p_SupEmpId.Direction = ParameterDirection.Input;
        p_SupEmpId.Value = SupEmpId;

        SqlParameter p_isUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_isUpdate.Direction = ParameterDirection.Input;
        p_isUpdate.Value = strIsUpdate;

        SqlParameter p_isDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_isDelete.Direction = ParameterDirection.Input;
        p_isDelete.Value = strIsDelete;

        return command;


    }

    //string GADCODE, string VDate, string VHour, string VTask,
    //string gadCode,string VHour, string VTask, string strVDate,

    public void Insert_TimeSheetNew(string TRANSID, string EMPID, string VMonth, string VYear, string FiscalYrId, 
        GridView grItem, string INSERTEDBY, string INSERTEDDATE, string aStatus, string SupEmpId,
        string strIsUpdate, string strIsDelete, int aval)
    {
        SqlCommand[] command = new SqlCommand[(grItem.Columns.Count-(aval-1))*grItem.Rows.Count];

        string gadCode = "";
        string accLine = "";
        string VHour = "";
        string VTask = "";
        string strVDate = "";


        int i = 0;
        foreach (GridViewRow gRow in grItem.Rows)
        {
            gadCode = grItem.DataKeys[gRow.DataItemIndex].Values[0].ToString().Trim();

            //TextBox txtAcc = (TextBox)gRow.Cells[2].FindControl("txtAccLine");
            accLine =grItem.DataKeys[gRow.DataItemIndex].Values[4].ToString().Trim();//txtAcc.Text; //grItem.DataKeys[gRow.DataItemIndex].Values[4].ToString().Trim();

            for (int j = 0; j < grItem.Columns.Count - aval; j++)
            {
                if (grItem.HeaderRow.Cells[j + 3].Text != "")
                {
                    TextBox txt = (TextBox)gRow.Cells[j + 3].FindControl("txt" + Convert.ToString(j + 1));
                    VHour = txt.Text.Trim();
                    TRANSID = txt.ToolTip;

                    LinkButton lb = (LinkButton)gRow.Cells[j + 3].FindControl("lb" + Convert.ToString(j + 1));
                    VTask = lb.ToolTip.ToString();

                    strVDate = grItem.HeaderRow.Cells[j + 3].Text.Trim();// +"/" + VYear;

                    command[i] = Insert_TimeSheet(TRANSID, EMPID, VMonth, VYear, FiscalYrId, gadCode, accLine, Common.ReturnDate(strVDate),
                        VHour, VTask, INSERTEDBY, INSERTEDDATE, aStatus, SupEmpId, strIsUpdate, strIsDelete);
                    i++;
                }
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
            command[0] = null;
        }
    }



    //TimeSheetLeave
    public SqlCommand Insert_TimeSheetLeave(string strTransID, string EMPID, string VMonth, string VYear, string FiscalYrId,
         string Ltype, string accLine, string strVDate, string VHour, string VTask, string INSERTEDBY, string INSERTEDDATE, string aStatus, string SupEmpId,
        string strIsUpdate, string strIsDelete)
    {
        SqlCommand command = new SqlCommand("PROC_INSERT_TimeSheetLeave");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_TRANSID = command.Parameters.Add("TRANSID", SqlDbType.BigInt);
        p_TRANSID.Direction = ParameterDirection.Input;
        p_TRANSID.Value = string.IsNullOrEmpty(strTransID) == true ? "0" : strTransID;
        //p_TRANSID.Value = string.IsNullOrEmpty(strTransID) == true ? 0 : Convert.ToInt64(strTransID);

        SqlParameter p_EMPID = command.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = EMPID;

        SqlParameter p_VMonth = command.Parameters.Add("VMonth", SqlDbType.VarChar);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = VMonth;

        SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.VarChar);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = VYear;

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = FiscalYrId;

        SqlParameter p_Ltype = command.Parameters.Add("Ltype", SqlDbType.BigInt);
        p_Ltype.Direction = ParameterDirection.Input;
        p_Ltype.Value = Ltype;

        SqlParameter p_AccLine = command.Parameters.Add("AccLine", SqlDbType.VarChar);
        p_AccLine.Direction = ParameterDirection.Input;
        p_AccLine.Value = accLine;

        SqlParameter p_VDate = command.Parameters.Add("VDate", SqlDbType.DateTime);
        p_VDate.Direction = ParameterDirection.Input;
        p_VDate.Value = strVDate;

        SqlParameter p_VHour = command.Parameters.Add("VHour", DBNull.Value);
        p_VHour.Direction = ParameterDirection.Input;
        p_VHour.IsNullable = true;
        if (VHour != "")
            p_VHour.Value = VHour;

        SqlParameter p_VTask = command.Parameters.Add("VTask", SqlDbType.VarChar);
        p_VTask.Direction = ParameterDirection.Input;
        p_VTask.Value = VTask;

        SqlParameter p_INSERTEDBY = command.Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = INSERTEDBY;

        SqlParameter p_INSERTEDDATE = command.Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDDATE.Direction = ParameterDirection.Input;
        p_INSERTEDDATE.Value = INSERTEDDATE;

        SqlParameter p_aStatus = command.Parameters.Add("aStatus", SqlDbType.Char);
        p_aStatus.Direction = ParameterDirection.Input;
        p_aStatus.Value = aStatus;

        SqlParameter p_SupEmpId = command.Parameters.Add("SupEmpId", SqlDbType.Char);
        p_SupEmpId.Direction = ParameterDirection.Input;
        p_SupEmpId.Value = SupEmpId;

        SqlParameter p_isUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_isUpdate.Direction = ParameterDirection.Input;
        p_isUpdate.Value = strIsUpdate;

        SqlParameter p_isDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_isDelete.Direction = ParameterDirection.Input;
        p_isDelete.Value = strIsDelete;

        return command;


    }

    //string GADCODE, string VDate, string VHour, string VTask,
    //string gadCode,string VHour, string VTask, string strVDate,

    public void Insert_TimeSheetLeaveNew(string TRANSID, string EMPID, string VMonth, string VYear, string FiscalYrId,
        GridView grItem, string INSERTEDBY, string INSERTEDDATE, string aStatus, string SupEmpId,
        string strIsUpdate, string strIsDelete, int aVal)
    {
        SqlCommand[] command = new SqlCommand[(grItem.Columns.Count - 3) * grItem.Rows.Count + 1];

        string Ltype = "";
        string accLine = "";
        string VHour = "";
        string VTask = "";
        string strVDate = "";


        int i = 0;

        command[i] = new SqlCommand("PROC_DELETE_TimeSheetLeave");
        command[i].CommandType = CommandType.StoredProcedure;

        SqlParameter p_EMPID = command[i].Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = EMPID;

        SqlParameter p_VMonth = command[i].Parameters.Add("VMonth", SqlDbType.VarChar);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = VMonth;

        SqlParameter p_VYear = command[i].Parameters.Add("VYear", SqlDbType.VarChar);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = VYear;

        SqlParameter p_FiscalYrId = command[i].Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = FiscalYrId;

        i++;

        foreach (GridViewRow gRow in grItem.Rows)
        {
            Ltype = grItem.DataKeys[gRow.DataItemIndex].Values[0].ToString().Trim();

            TextBox txtAcc = (TextBox)gRow.Cells[2].FindControl("txtAccLine2");
            accLine = txtAcc.Text; //grItem.DataKeys[gRow.DataItemIndex].Values[1].ToString().Trim();

            for (int j = 0; j < grItem.Columns.Count - aVal; j++)
            {
                if (grItem.HeaderRow.Cells[j + 3].Text != "")
                {
                    TextBox txt = (TextBox)gRow.Cells[j + 3].FindControl("txt" + Convert.ToString(j + 1));
                    VHour = txt.Text.Trim();
                    TRANSID = txt.ToolTip;

                    LinkButton lb = (LinkButton)gRow.Cells[j + 3].FindControl("lb" + Convert.ToString(j + 1));
                    VTask = lb.ToolTip.ToString();

                    strVDate = grItem.HeaderRow.Cells[j + 3].Text.Trim();// +"/" + VYear;

                    command[i] = Insert_TimeSheetLeave(TRANSID, EMPID, VMonth, VYear, FiscalYrId, Ltype, accLine, Common.ReturnDate(strVDate),
                        VHour, VTask, INSERTEDBY, INSERTEDDATE, aStatus, SupEmpId, "N", "N");
                    i++;
                }
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
            command[0] = null;
        }
    }



    public DataTable GET_TimeSheet(string EMPID, string VMonth, string VYear, string FiscalYrId, string VDateFrom, string VDateTo)
    {
        SqlCommand command = new SqlCommand("PROC_GET_TimeSheet");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EMPID = command.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = EMPID;

        SqlParameter p_VMonth = command.Parameters.Add("VMonth", SqlDbType.VarChar);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = VMonth;

        SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.VarChar);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = VYear;

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = FiscalYrId;

        SqlParameter p_VDateFrom = command.Parameters.Add("VDateFrom", SqlDbType.DateTime);
        p_VDateFrom.Direction = ParameterDirection.Input;
        p_VDateFrom.Value = VDateFrom;

        SqlParameter p_VDateTo = command.Parameters.Add("VDateTo", SqlDbType.DateTime);
        p_VDateTo.Direction = ParameterDirection.Input;
        p_VDateTo.Value = VDateTo;

        objDC.CreateDSFromProc(command, "dtTimeSheet");
        return objDC.ds.Tables["dtTimeSheet"];
    }

    //Time Sheet Parameters for Report Viewer (30.08.2012)
    public DataTable GET_TimeSheetVwr(string EMPID, string VMonth, string VYear, string FiscalYrId)
    {
        SqlCommand command = new SqlCommand("PROC_GET_TimeSheet_Viewer");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EMPID = command.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = EMPID;

        SqlParameter p_VMonth = command.Parameters.Add("VMonth", SqlDbType.VarChar);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = VMonth;

        SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.VarChar);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = VYear;

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = FiscalYrId;        

        objDC.CreateDSFromProc(command, "dtTimeSheetVwr");
        return objDC.ds.Tables["dtTimeSheetVwr"];
    }

    public DataTable GET_TimeSheet_Report(string EMPID, string VMonth, string VYear)
    {
        SqlCommand command = new SqlCommand("PROC_GET_TimeSheet_Report");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EMPID = command.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = EMPID;

        SqlParameter p_VMonth = command.Parameters.Add("VMonth", SqlDbType.VarChar);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = VMonth;

        SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.VarChar);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = VYear;        

        objDC.CreateDSFromProc(command, "dtTimeSheetRpt");
        return objDC.ds.Tables["dtTimeSheetRpt"];
    }


    public DataTable GET_EMP_WeekEnd(string EMPID)
    {
        SqlCommand command = new SqlCommand("PROC_SELECT_EMP_WeekEnd");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EMPID = command.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = EMPID;

        objDC.CreateDSFromProc(command, "dtEMP_WeekEnd");
        return objDC.ds.Tables["dtEMP_WeekEnd"];
    }


    public DataTable GET_Holidays_YrDay(string VYear, string VDateFrom, string VDateTo)
    {
        SqlCommand command = new SqlCommand("GET_Holidays_YrDay");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VYear = command.Parameters.Add("HolidayYear", SqlDbType.VarChar);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = VYear;

        SqlParameter p_VDateFrom = command.Parameters.Add("HoliDateFrom", SqlDbType.DateTime);
        p_VDateFrom.Direction = ParameterDirection.Input;
        p_VDateFrom.Value = VDateFrom;

        SqlParameter p_VDateTo = command.Parameters.Add("HoliDateTo", SqlDbType.DateTime);
        p_VDateTo.Direction = ParameterDirection.Input;
        p_VDateTo.Value = VDateTo;

        objDC.CreateDSFromProc(command, "dtHolidays_YrDay");
        return objDC.ds.Tables["dtHolidays_YrDay"];
    }


    public DataTable GET_LeaveRecord_TimeSheet(string EMPID, string VDateFrom, string VDateTo)
    {
        SqlCommand command = new SqlCommand("PROC_LeaveRecord_TimeSheet");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EMPID = command.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = EMPID;

        SqlParameter p_VDateFrom = command.Parameters.Add("LevDateFrom", SqlDbType.DateTime);
        p_VDateFrom.Direction = ParameterDirection.Input;
        p_VDateFrom.Value = VDateFrom;

        SqlParameter p_VDateTo = command.Parameters.Add("LevDateTo", SqlDbType.DateTime);
        p_VDateTo.Direction = ParameterDirection.Input;
        p_VDateTo.Value = VDateTo;

        objDC.CreateDSFromProc(command, "dtLeaveRecord");
        return objDC.ds.Tables["dtLeaveRecord"];
    }


    public DataTable GET_LeaveDate_TimeSheet(string EMPID, string VDateFrom, string VDateTo)
    {
        SqlCommand command = new SqlCommand("PROC_LeaveDate_TimeSheet");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EMPID = command.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = EMPID;

        SqlParameter p_VDateFrom = command.Parameters.Add("LevDateFrom", SqlDbType.DateTime);
        p_VDateFrom.Direction = ParameterDirection.Input;
        p_VDateFrom.Value = VDateFrom;

        SqlParameter p_VDateTo = command.Parameters.Add("LevDateTo", SqlDbType.DateTime);
        p_VDateTo.Direction = ParameterDirection.Input;
        p_VDateTo.Value = VDateTo;

        objDC.CreateDSFromProc(command, "dtLeaveDate");
        return objDC.ds.Tables["dtLeaveDate"];
    }



    //GET TimeSheetLeave
    public DataTable GET_TimeSheetLeave(string EMPID, string VMonth, string VYear, string FiscalYrId, string VDateFrom, string VDateTo)
    {
        SqlCommand command = new SqlCommand("PROC_GET_TimeSheetLeave");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EMPID = command.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = EMPID;

        SqlParameter p_VMonth = command.Parameters.Add("VMonth", SqlDbType.VarChar);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = VMonth;

        SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.VarChar);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = VYear;

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.BigInt);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = FiscalYrId;

        SqlParameter p_VDateFrom = command.Parameters.Add("VDateFrom", SqlDbType.DateTime);
        p_VDateFrom.Direction = ParameterDirection.Input;
        p_VDateFrom.Value = VDateFrom;

        SqlParameter p_VDateTo = command.Parameters.Add("VDateTo", SqlDbType.DateTime);
        p_VDateTo.Direction = ParameterDirection.Input;
        p_VDateTo.Value = VDateTo;

        objDC.CreateDSFromProc(command, "dtTimeSheetLeave");
        return objDC.ds.Tables["dtTimeSheetLeave"];
    }




    //Insert record fro Time Sheet Policy
    public void Insert_TimeSheetPolicy(string PID, string PYear, string PMonth, string PHour, string INSERTEDBY, string INSERTEDDATE, string strIsUpdate, string strIsDelete)
    {
        SqlCommand command = new SqlCommand("PROC_GET_TimeSheetPolicy");
        command.CommandType = CommandType.StoredProcedure;


        SqlParameter p_PID = command.Parameters.Add("PID", SqlDbType.BigInt);
        p_PID.Direction = ParameterDirection.Input;
        p_PID.Value = string.IsNullOrEmpty(PID) == true ? "0" : PID;


        SqlParameter p_PYear = command.Parameters.Add("PYear", SqlDbType.Char);
        p_PYear.Direction = ParameterDirection.Input;
        p_PYear.Value = PYear;

        SqlParameter p_PMonth = command.Parameters.Add("PMonth", SqlDbType.VarChar);
        p_PMonth.Direction = ParameterDirection.Input;
        p_PMonth.Value = PMonth;

        SqlParameter p_PHour = command.Parameters.Add("PHour", SqlDbType.Decimal);
        p_PHour.Direction = ParameterDirection.Input;
        p_PHour.Value = PHour;

        SqlParameter p_INSERTEDBY = command.Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
        p_INSERTEDBY.Direction = ParameterDirection.Input;
        p_INSERTEDBY.Value = INSERTEDBY;

        SqlParameter p_INSERTEDDATE = command.Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
        p_INSERTEDDATE.Direction = ParameterDirection.Input;
        p_INSERTEDDATE.Value = INSERTEDDATE;

        SqlParameter p_isUpdate = command.Parameters.Add("IsUpdate", SqlDbType.Char);
        p_isUpdate.Direction = ParameterDirection.Input;
        p_isUpdate.Value = strIsUpdate;

        SqlParameter p_isDelete = command.Parameters.Add("IsDelete", SqlDbType.Char);
        p_isDelete.Direction = ParameterDirection.Input;
        p_isDelete.Value = strIsDelete;

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



    public DataTable GET_TimeSheetPolicy(string PYear, string PMonth)
    {
        SqlCommand command = new SqlCommand("PROC_SELECT_TimeSheetPolicy");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_PYear = command.Parameters.Add("PYear", SqlDbType.Char);
        p_PYear.Direction = ParameterDirection.Input;
        p_PYear.Value = PYear;

        SqlParameter p_PMonth = command.Parameters.Add("PMonth", SqlDbType.VarChar);
        p_PMonth.Direction = ParameterDirection.Input;
        p_PMonth.Value = PMonth;

        objDC.CreateDSFromProc(command, "dtTimeSheetPolicy");
        return objDC.ds.Tables["dtTimeSheetPolicy"];
    }



    //14.10.2012    
    public DataTable GET_TimeSheet_For_App(string VMonth, string VYear, string FiscalYrId, string astatus, string SupEmpId)
    {
        //if (objDC.ds.Tables["dtTimeSheet_For_App"] != null)
        //{
        //    objDC.ds.Tables["dtTimeSheet_For_App"].Rows.Clear();
        //    objDC.ds.Tables["dtTimeSheet_For_App"].Dispose();
        //}

        SqlCommand command = new SqlCommand("PROC_GET_TimeSheet_For_App");
        command.CommandType = CommandType.StoredProcedure;

        //SqlParameter p_EMPID = command.Parameters.Add("EMPID", SqlDbType.Char);
        //p_EMPID.Direction = ParameterDirection.Input;
        //p_EMPID.Value = EMPID;

        SqlParameter p_VMonth = command.Parameters.Add("VMonth", SqlDbType.VarChar);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = VMonth;

        SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.VarChar);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = VYear;

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.Int);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = FiscalYrId;

        SqlParameter p_astatus = command.Parameters.Add("astatus", SqlDbType.Char);
        p_astatus.Direction = ParameterDirection.Input;
        p_astatus.Value = astatus;

        SqlParameter p_SupEmpId = command.Parameters.Add("SupEmpId", SqlDbType.Char);
        p_SupEmpId.Direction = ParameterDirection.Input;
        p_SupEmpId.Value = SupEmpId;

        objDC.CreateDSFromProc(command, "dtTimeSheet_For_App" + astatus);
        return objDC.ds.Tables["dtTimeSheet_For_App" + astatus];
    }




    public void UPDATE_TIMESHEET_STATUS(string EMPID, string VMonth, string VYear, string FiscalYrId,
        string astatus, string UPDATEDBY, string UPDATEDDATE)
    {
        SqlCommand command = new SqlCommand("PROC_UPDATE_TIMESHEET_STATUS");
        command.CommandType = CommandType.StoredProcedure;


        SqlParameter p_EMPID = command.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = EMPID;

        SqlParameter p_VMonth = command.Parameters.Add("VMonth", SqlDbType.VarChar);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = VMonth;

        SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.VarChar);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = VYear;

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.Int);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = FiscalYrId;

        SqlParameter p_astatus = command.Parameters.Add("astatus", SqlDbType.Char);
        p_astatus.Direction = ParameterDirection.Input;
        p_astatus.Value = astatus;

        SqlParameter p_UPDATEDBY = command.Parameters.Add("UPDATEDBY", SqlDbType.VarChar);
        p_UPDATEDBY.Direction = ParameterDirection.Input;
        p_UPDATEDBY.Value = UPDATEDBY;

        SqlParameter p_UPDATEDDATE = command.Parameters.Add("UPDATEDDATE", SqlDbType.DateTime);
        p_UPDATEDDATE.Direction = ParameterDirection.Input;
        p_UPDATEDDATE.Value = UPDATEDDATE;

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



    public void UPDATE_TIMESHEETLEAVE_STATUS(string EMPID, string VMonth, string VYear, string FiscalYrId,
        string astatus, string UPDATEDBY, string UPDATEDDATE)
    {
        SqlCommand command = new SqlCommand("PROC_UPDATE_TIMESHEETLEAVE_STATUS");
        command.CommandType = CommandType.StoredProcedure;


        SqlParameter p_EMPID = command.Parameters.Add("EMPID", SqlDbType.Char);
        p_EMPID.Direction = ParameterDirection.Input;
        p_EMPID.Value = EMPID;

        SqlParameter p_VMonth = command.Parameters.Add("VMonth", SqlDbType.VarChar);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = VMonth;

        SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.VarChar);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = VYear;

        SqlParameter p_FiscalYrId = command.Parameters.Add("FiscalYrId", SqlDbType.Int);
        p_FiscalYrId.Direction = ParameterDirection.Input;
        p_FiscalYrId.Value = FiscalYrId;

        SqlParameter p_astatus = command.Parameters.Add("astatus", SqlDbType.Char);
        p_astatus.Direction = ParameterDirection.Input;
        p_astatus.Value = astatus;

        SqlParameter p_UPDATEDBY = command.Parameters.Add("UPDATEDBY", SqlDbType.VarChar);
        p_UPDATEDBY.Direction = ParameterDirection.Input;
        p_UPDATEDBY.Value = UPDATEDBY;

        SqlParameter p_UPDATEDDATE = command.Parameters.Add("UPDATEDDATE", SqlDbType.DateTime);
        p_UPDATEDDATE.Direction = ParameterDirection.Input;
        p_UPDATEDDATE.Value = UPDATEDDATE;

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

    public DataTable Get_SOF_SummaryData(string strEmpId, string strMonthFrom, string strYearFrom, string strMonthTo, string strYearTo)
    {
        SqlCommand command = new SqlCommand("proc_Get_SOF_SummaryData");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_MonthFrom = command.Parameters.Add("MonthFrom", SqlDbType.BigInt);
        p_MonthFrom.Direction = ParameterDirection.Input;
        p_MonthFrom.Value = strMonthFrom;

        SqlParameter p_YearFrom = command.Parameters.Add("YearFrom", SqlDbType.BigInt);
        p_YearFrom.Direction = ParameterDirection.Input;
        p_YearFrom.Value = strYearFrom;

        SqlParameter p_MonthTo = command.Parameters.Add("MonthTo", SqlDbType.BigInt);
        p_MonthTo.Direction = ParameterDirection.Input;
        p_MonthTo.Value = strMonthTo;

        SqlParameter p_YearTo = command.Parameters.Add("YearTo", SqlDbType.BigInt);
        p_YearTo.Direction = ParameterDirection.Input;
        p_YearTo.Value = strYearTo;

        objDC.CreateDSFromProc(command, "Get_SOF_SummaryData");
        return objDC.ds.Tables["Get_SOF_SummaryData"];
    }

    public DataTable Get_MonthWiseTimeSheetHour(string strEmpId, string strYear)
    {
        SqlCommand command = new SqlCommand("proc_Get_MonthWiseTimeSheetHour");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.BigInt);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = strYear;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        objDC.CreateDSFromProc(command, "Get_MonthWiseTimeSheetHour");
        return objDC.ds.Tables["Get_MonthWiseTimeSheetHour"];
    }

    public DataTable Get_SOF_Wise_HR(string strEmpId, string strYear)
    {
        SqlCommand command = new SqlCommand("proc_Get_SOF_Wise_HR");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.BigInt);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = strYear;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        objDC.CreateDSFromProc(command, "Get_SOF_Wise_HR");
        return objDC.ds.Tables["Get_SOF_Wise_HR"];
    }

    #region TimeSheet Generate
    public void GenerateTimeSheet(string strEmpId, string strMonth, string strYear, string strInsertedBy, string strInsertedDate)
    {        
        string[] Status = { "H", "SL", "LWP", "WH", "V" };
        DataTable dtTimeSheet = Get_AttendanceDataToGenerateTimeSheet(strEmpId, strMonth, strYear);
        DataTable dtTimeSheetAbsent = Get_AttendanceDataToGenerateTimeSheetAbsent(strEmpId, strMonth, strYear);

        SqlCommand[] command = new SqlCommand[dtTimeSheet.Rows.Count + dtTimeSheet.Rows.Count * 5 + dtTimeSheet.Rows.Count + 1];
        if (dtTimeSheet.Rows.Count > 0)
            command = new SqlCommand[dtTimeSheet.Rows.Count + dtTimeSheet.Rows.Count * 5 + dtTimeSheet.Rows.Count + 1];
        else if (dtTimeSheetAbsent.Rows.Count > 0)
            command = new SqlCommand[dtTimeSheetAbsent.Rows.Count + dtTimeSheetAbsent.Rows.Count * 5 + dtTimeSheetAbsent.Rows.Count + 1];
       
        int i = 0;
        long TimeSheetId = objDC.GerMaxIDNumber("TimeSheet", "TimeSheetId");
        string strEmp = "";
        DataRow[] foundAbsent = null;

        command[i] = this.DeleteTimeSheet(strEmpId, strMonth, strYear);
        i++;

        if (dtTimeSheet.Rows.Count > 0)
        {
            foreach (DataRow dRow in dtTimeSheet.Rows)
            {
                if (strEmp != dRow["EmpId"].ToString().Trim())
                {
                    for (int j = 0; j < Status.Length; j++)
                    {
                        foundAbsent = dtTimeSheetAbsent.Select("EmpId='" + dRow["EmpId"].ToString().Trim() + "' AND Status='" + Status[j].Trim() + "'");
                        if (foundAbsent.Length > 0)
                        {
                            command[i] = this.InsertTimeSheet(foundAbsent[0], TimeSheetId, strMonth, strYear, strInsertedBy, strInsertedDate, Status[j].Trim());
                        }
                        else
                        {
                            command[i] = this.InsertTimeSheetForNull(dRow, TimeSheetId, strMonth, strYear, strInsertedBy, strInsertedDate, Status[j].Trim());
                        }

                        foundAbsent = null;
                        i++;
                        TimeSheetId++;
                    }
                    strEmp = dRow["EmpId"].ToString().Trim();
                }

                command[i] = this.InsertTimeSheet(dRow, TimeSheetId, strMonth, strYear, strInsertedBy, strInsertedDate, "P");
                i++;
                TimeSheetId++;
            }
        }
        else if (dtTimeSheetAbsent.Rows.Count > 0)
        {
            foreach (DataRow dRow in dtTimeSheetAbsent.Rows)
            {
                if (strEmp != dRow["EmpId"].ToString().Trim())
                {
                    for (int j = 0; j < Status.Length; j++)
                    {
                        foundAbsent = dtTimeSheetAbsent.Select("EmpId='" + dRow["EmpId"].ToString().Trim() + "' AND Status='" + Status[j].Trim() + "'");
                        if (foundAbsent.Length > 0)
                        {
                            command[i] = this.InsertTimeSheet(foundAbsent[0], TimeSheetId, strMonth, strYear, strInsertedBy, strInsertedDate, Status[j].Trim());
                        }
                        else
                        {
                            command[i] = this.InsertTimeSheetForNull(dRow, TimeSheetId, strMonth, strYear, strInsertedBy, strInsertedDate, Status[j].Trim());
                        }

                        foundAbsent = null;
                        i++;
                        TimeSheetId++;
                    }
                    strEmp = dRow["EmpId"].ToString().Trim();
                }

                //command[i] = this.InsertTimeSheet(dRow, TimeSheetId, strMonth, strYear, strInsertedBy, strInsertedDate, "P");
                i++;
                TimeSheetId++;
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

    public SqlCommand InsertTimeSheet(DataRow dRow, long TimeSheetId, string strMonth, string strYear,
       string strInsertedBy, string strInsertedDate, string strStatus)
    {
        SqlCommand command = new SqlCommand("proc_Insert_TimeSheet");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_TimeSheetId = command.Parameters.Add("TimeSheetId", SqlDbType.BigInt);
        p_TimeSheetId.Direction = ParameterDirection.Input;
        p_TimeSheetId.Value = TimeSheetId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = dRow["EmpId"].ToString().Trim();

        SqlParameter p_VMonth = command.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = strMonth;

        SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.BigInt);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = strYear;

        SqlParameter p_SOFId = command.Parameters.Add("SalarySourceId", DBNull.Value);
        p_SOFId.Direction = ParameterDirection.Input;
        p_SOFId.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["SalarySourceId"].ToString()) == false)
            p_SOFId.Value = dRow["SalarySourceId"].ToString();

        SqlParameter p_1 = command.Parameters.Add("1", DBNull.Value);
        p_1.Direction = ParameterDirection.Input;
        p_1.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["1"].ToString()) == false)
            p_1.Value = dRow["1"].ToString();

        SqlParameter p_2 = command.Parameters.Add("2", DBNull.Value);
        p_2.Direction = ParameterDirection.Input;
        p_2.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["2"].ToString()) == false)
            p_2.Value = dRow["2"].ToString();

        SqlParameter p_3 = command.Parameters.Add("3", DBNull.Value);
        p_3.Direction = ParameterDirection.Input;
        p_3.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["3"].ToString()) == false)
            p_3.Value = dRow["3"].ToString();

        SqlParameter p_4 = command.Parameters.Add("4", DBNull.Value);
        p_4.Direction = ParameterDirection.Input;
        p_4.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["4"].ToString()) == false)
            p_4.Value = dRow["4"].ToString();

        SqlParameter p_5 = command.Parameters.Add("5", DBNull.Value);
        p_5.Direction = ParameterDirection.Input;
        p_5.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["5"].ToString()) == false)
            p_5.Value = dRow["5"].ToString();

        SqlParameter p_6 = command.Parameters.Add("6", DBNull.Value);
        p_6.Direction = ParameterDirection.Input;
        p_6.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["6"].ToString()) == false)
            p_6.Value = dRow["6"].ToString();

        SqlParameter p_7 = command.Parameters.Add("7", DBNull.Value);
        p_7.Direction = ParameterDirection.Input;
        p_7.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["7"].ToString()) == false)
            p_7.Value = dRow["7"].ToString();

        SqlParameter p_8 = command.Parameters.Add("8", DBNull.Value);
        p_8.Direction = ParameterDirection.Input;
        p_8.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["8"].ToString()) == false)
            p_8.Value = dRow["8"].ToString();

        SqlParameter p_9 = command.Parameters.Add("9", DBNull.Value);
        p_9.Direction = ParameterDirection.Input;
        p_9.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["9"].ToString()) == false)
            p_9.Value = dRow["9"].ToString();

        SqlParameter p_10 = command.Parameters.Add("10", DBNull.Value);
        p_10.Direction = ParameterDirection.Input;
        p_10.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["10"].ToString()) == false)
            p_10.Value = dRow["10"].ToString();

        SqlParameter p_11 = command.Parameters.Add("11", DBNull.Value);
        p_11.Direction = ParameterDirection.Input;
        p_11.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["11"].ToString()) == false)
            p_11.Value = dRow["11"].ToString();

        SqlParameter p_12 = command.Parameters.Add("12", DBNull.Value);
        p_12.Direction = ParameterDirection.Input;
        p_12.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["12"].ToString()) == false)
            p_12.Value = dRow["12"].ToString();

        SqlParameter p_13 = command.Parameters.Add("13", DBNull.Value);
        p_13.Direction = ParameterDirection.Input;
        p_13.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["13"].ToString()) == false)
            p_13.Value = dRow["13"].ToString();

        SqlParameter p_14 = command.Parameters.Add("14", DBNull.Value);
        p_14.Direction = ParameterDirection.Input;
        p_14.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["14"].ToString()) == false)
            p_14.Value = dRow["14"].ToString();

        SqlParameter p_15 = command.Parameters.Add("15", DBNull.Value);
        p_15.Direction = ParameterDirection.Input;
        p_15.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["15"].ToString()) == false)
            p_15.Value = dRow["15"].ToString();

        SqlParameter p_16 = command.Parameters.Add("16", DBNull.Value);
        p_16.Direction = ParameterDirection.Input;
        p_16.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["16"].ToString()) == false)
            p_16.Value = dRow["16"].ToString();

        SqlParameter p_17 = command.Parameters.Add("17", DBNull.Value);
        p_17.Direction = ParameterDirection.Input;
        p_17.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["17"].ToString()) == false)
            p_17.Value = dRow["17"].ToString();

        SqlParameter p_18 = command.Parameters.Add("18", DBNull.Value);
        p_18.Direction = ParameterDirection.Input;
        p_18.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["18"].ToString()) == false)
            p_18.Value = dRow["18"].ToString();

        SqlParameter p_19 = command.Parameters.Add("19", DBNull.Value);
        p_19.Direction = ParameterDirection.Input;
        p_19.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["19"].ToString()) == false)
            p_19.Value = dRow["19"].ToString();

        SqlParameter p_20 = command.Parameters.Add("20", DBNull.Value);
        p_20.Direction = ParameterDirection.Input;
        p_20.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["20"].ToString()) == false)
            p_20.Value = dRow["20"].ToString();

        SqlParameter p_21 = command.Parameters.Add("21", DBNull.Value);
        p_21.Direction = ParameterDirection.Input;
        p_21.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["21"].ToString()) == false)
            p_21.Value = dRow["21"].ToString();

        SqlParameter p_22 = command.Parameters.Add("22", DBNull.Value);
        p_22.Direction = ParameterDirection.Input;
        p_22.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["22"].ToString()) == false)
            p_22.Value = dRow["22"].ToString();

        SqlParameter p_23 = command.Parameters.Add("23", DBNull.Value);
        p_23.Direction = ParameterDirection.Input;
        p_23.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["23"].ToString()) == false)
            p_23.Value = dRow["23"].ToString();

        SqlParameter p_24 = command.Parameters.Add("24", DBNull.Value);
        p_24.Direction = ParameterDirection.Input;
        p_24.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["24"].ToString()) == false)
            p_24.Value = dRow["24"].ToString();

        SqlParameter p_25 = command.Parameters.Add("25", DBNull.Value);
        p_25.Direction = ParameterDirection.Input;
        p_25.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["25"].ToString()) == false)
            p_25.Value = dRow["25"].ToString();

        SqlParameter p_26 = command.Parameters.Add("26", DBNull.Value);
        p_26.Direction = ParameterDirection.Input;
        p_26.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["26"].ToString()) == false)
            p_26.Value = dRow["26"].ToString();

        SqlParameter p_27 = command.Parameters.Add("27", DBNull.Value);
        p_27.Direction = ParameterDirection.Input;
        p_27.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["27"].ToString()) == false)
            p_27.Value = dRow["27"].ToString();

        SqlParameter p_28 = command.Parameters.Add("28", DBNull.Value);
        p_28.Direction = ParameterDirection.Input;
        p_28.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["28"].ToString()) == false)
            p_28.Value = dRow["28"].ToString();

        SqlParameter p_29 = command.Parameters.Add("29", DBNull.Value);
        p_29.Direction = ParameterDirection.Input;
        p_29.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["29"].ToString()) == false)
            p_29.Value = dRow["29"].ToString();

        SqlParameter p_30 = command.Parameters.Add("30", DBNull.Value);
        p_30.Direction = ParameterDirection.Input;
        p_30.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["30"].ToString()) == false)
            p_30.Value = dRow["30"].ToString();

        SqlParameter p_31 = command.Parameters.Add("31", DBNull.Value);
        p_31.Direction = ParameterDirection.Input;
        p_31.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["31"].ToString()) == false)
            p_31.Value = dRow["31"].ToString();

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsertedDate;

        SqlParameter p_Status = command.Parameters.Add("Status", SqlDbType.Char);
        p_Status.Direction = ParameterDirection.Input;
        p_Status.Value = strStatus;

        return command;
    }

    public SqlCommand InsertTimeSheetForNull(DataRow dRow, long TimeSheetId, string strMonth, string strYear,
        string strInsertedBy, string strInsertedDate, string strStatus)
    {
        SqlCommand command = new SqlCommand("proc_Insert_TimeSheet");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_TimeSheetId = command.Parameters.Add("TimeSheetId", SqlDbType.BigInt);
        p_TimeSheetId.Direction = ParameterDirection.Input;
        p_TimeSheetId.Value = TimeSheetId;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = dRow["EmpId"].ToString().Trim();

        SqlParameter p_VMonth = command.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_VMonth.Direction = ParameterDirection.Input;
        p_VMonth.Value = strMonth;

        SqlParameter p_VYear = command.Parameters.Add("VYear", SqlDbType.BigInt);
        p_VYear.Direction = ParameterDirection.Input;
        p_VYear.Value = strYear;

        SqlParameter p_SOFId = command.Parameters.Add("SalarySourceId", DBNull.Value);
        p_SOFId.Direction = ParameterDirection.Input;
        p_SOFId.IsNullable = true;
        if (string.IsNullOrEmpty(dRow["SalarySourceId"].ToString()) == false)
            p_SOFId.Value = dRow["SalarySourceId"].ToString();

        SqlParameter p_1 = command.Parameters.Add("1", DBNull.Value);
        p_1.Direction = ParameterDirection.Input;
        p_1.IsNullable = true;


        SqlParameter p_2 = command.Parameters.Add("2", DBNull.Value);
        p_2.Direction = ParameterDirection.Input;
        p_2.IsNullable = true;

        SqlParameter p_3 = command.Parameters.Add("3", DBNull.Value);
        p_3.Direction = ParameterDirection.Input;
        p_3.IsNullable = true;

        SqlParameter p_4 = command.Parameters.Add("4", DBNull.Value);
        p_4.Direction = ParameterDirection.Input;
        p_4.IsNullable = true;

        SqlParameter p_5 = command.Parameters.Add("5", DBNull.Value);
        p_5.Direction = ParameterDirection.Input;
        p_5.IsNullable = true;

        SqlParameter p_6 = command.Parameters.Add("6", DBNull.Value);
        p_6.Direction = ParameterDirection.Input;
        p_6.IsNullable = true;

        SqlParameter p_7 = command.Parameters.Add("7", DBNull.Value);
        p_7.Direction = ParameterDirection.Input;
        p_7.IsNullable = true;

        SqlParameter p_8 = command.Parameters.Add("8", DBNull.Value);
        p_8.Direction = ParameterDirection.Input;
        p_8.IsNullable = true;

        SqlParameter p_9 = command.Parameters.Add("9", DBNull.Value);
        p_9.Direction = ParameterDirection.Input;
        p_9.IsNullable = true;

        SqlParameter p_10 = command.Parameters.Add("10", DBNull.Value);
        p_10.Direction = ParameterDirection.Input;
        p_10.IsNullable = true;

        SqlParameter p_11 = command.Parameters.Add("11", DBNull.Value);
        p_11.Direction = ParameterDirection.Input;
        p_11.IsNullable = true;

        SqlParameter p_12 = command.Parameters.Add("12", DBNull.Value);
        p_12.Direction = ParameterDirection.Input;
        p_12.IsNullable = true;

        SqlParameter p_13 = command.Parameters.Add("13", DBNull.Value);
        p_13.Direction = ParameterDirection.Input;
        p_13.IsNullable = true;

        SqlParameter p_14 = command.Parameters.Add("14", DBNull.Value);
        p_14.Direction = ParameterDirection.Input;
        p_14.IsNullable = true;

        SqlParameter p_15 = command.Parameters.Add("15", DBNull.Value);
        p_15.Direction = ParameterDirection.Input;
        p_15.IsNullable = true;

        SqlParameter p_16 = command.Parameters.Add("16", DBNull.Value);
        p_16.Direction = ParameterDirection.Input;
        p_16.IsNullable = true;

        SqlParameter p_17 = command.Parameters.Add("17", DBNull.Value);
        p_17.Direction = ParameterDirection.Input;
        p_17.IsNullable = true;

        SqlParameter p_18 = command.Parameters.Add("18", DBNull.Value);
        p_18.Direction = ParameterDirection.Input;
        p_18.IsNullable = true;

        SqlParameter p_19 = command.Parameters.Add("19", DBNull.Value);
        p_19.Direction = ParameterDirection.Input;
        p_19.IsNullable = true;

        SqlParameter p_20 = command.Parameters.Add("20", DBNull.Value);
        p_20.Direction = ParameterDirection.Input;
        p_20.IsNullable = true;

        SqlParameter p_21 = command.Parameters.Add("21", DBNull.Value);
        p_21.Direction = ParameterDirection.Input;
        p_21.IsNullable = true;

        SqlParameter p_22 = command.Parameters.Add("22", DBNull.Value);
        p_22.Direction = ParameterDirection.Input;
        p_22.IsNullable = true;

        SqlParameter p_23 = command.Parameters.Add("23", DBNull.Value);
        p_23.Direction = ParameterDirection.Input;
        p_23.IsNullable = true;

        SqlParameter p_24 = command.Parameters.Add("24", DBNull.Value);
        p_24.Direction = ParameterDirection.Input;
        p_24.IsNullable = true;

        SqlParameter p_25 = command.Parameters.Add("25", DBNull.Value);
        p_25.Direction = ParameterDirection.Input;
        p_25.IsNullable = true;

        SqlParameter p_26 = command.Parameters.Add("26", DBNull.Value);
        p_26.Direction = ParameterDirection.Input;
        p_26.IsNullable = true;

        SqlParameter p_27 = command.Parameters.Add("27", DBNull.Value);
        p_27.Direction = ParameterDirection.Input;
        p_27.IsNullable = true;

        SqlParameter p_28 = command.Parameters.Add("28", DBNull.Value);
        p_28.Direction = ParameterDirection.Input;
        p_28.IsNullable = true;

        SqlParameter p_29 = command.Parameters.Add("29", DBNull.Value);
        p_29.Direction = ParameterDirection.Input;
        p_29.IsNullable = true;

        SqlParameter p_30 = command.Parameters.Add("30", DBNull.Value);
        p_30.Direction = ParameterDirection.Input;
        p_30.IsNullable = true;

        SqlParameter p_31 = command.Parameters.Add("31", DBNull.Value);
        p_31.Direction = ParameterDirection.Input;
        p_31.IsNullable = true;

        SqlParameter p_InsertedBy = command.Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsertedBy;

        SqlParameter p_InsertedDate = command.Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsertedDate;

        SqlParameter p_Status = command.Parameters.Add("Status", SqlDbType.Char);
        p_Status.Direction = ParameterDirection.Input;
        p_Status.Value = strStatus;

        return command;
    }

    private DataTable Get_AttendanceDataToGenerateTimeSheet(string strEmpId, string strMonth, string strYear)
    {

        if (objDC.ds.Tables["tblTimeSheet"] != null)
        {
            objDC.ds.Tables["tblTimeSheet"].Rows.Clear();
            objDC.ds.Tables["tblTimeSheet"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_TimeSheet_Generate");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_Month = command.Parameters.Add("Month", SqlDbType.BigInt);
        p_Month.Direction = ParameterDirection.Input;
        p_Month.Value = strMonth;

        SqlParameter p_Year = command.Parameters.Add("Year", SqlDbType.BigInt);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = strYear;

        objDC.CreateDSFromProc(command, "tblTimeSheet");
        return objDC.ds.Tables["tblTimeSheet"];
    }

    private DataTable Get_AttendanceDataToGenerateTimeSheetAbsent(string strEmpId, string strMonth, string strYear)
    {
        if (objDC.ds.Tables["tblTimeSheetAbsent"] != null)
        {
            objDC.ds.Tables["tblTimeSheetAbsent"].Rows.Clear();
            objDC.ds.Tables["tblTimeSheetAbsent"].Dispose();
        }

        SqlCommand command = new SqlCommand("proc_TimeSheetForAbsent");

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_Month = command.Parameters.Add("Month", SqlDbType.BigInt);
        p_Month.Direction = ParameterDirection.Input;
        p_Month.Value = strMonth;

        SqlParameter p_Year = command.Parameters.Add("Year", SqlDbType.BigInt);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = strYear;

        objDC.CreateDSFromProc(command, "tblTimeSheetAbsent");
        return objDC.ds.Tables["tblTimeSheetAbsent"];
    }  

    public SqlCommand DeleteTimeSheet(string strEmpId, string strMonth, string strYear)
    {
        SqlCommand command = new SqlCommand("proc_Delete_TimeSheet");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EmpId = command.Parameters.Add("EmpId", SqlDbType.VarChar);
        p_EmpId.Direction = ParameterDirection.Input;
        p_EmpId.Value = strEmpId;

        SqlParameter p_Month = command.Parameters.Add("VMonth", SqlDbType.BigInt);
        p_Month.Direction = ParameterDirection.Input;
        p_Month.Value = strMonth;

        SqlParameter p_Year = command.Parameters.Add("VYear", SqlDbType.BigInt);
        p_Year.Direction = ParameterDirection.Input;
        p_Year.Value = strYear;

        return command;
    }

   

    
    #endregion
}
