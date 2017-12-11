using System;
using System.Data;
using System.Text; 
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for HolidayTableManager
/// </summary>
public class HolidayTableManager
{
    DBConnector objDC = new DBConnector();

    public void InsertHoliday(string IsUpdate, string IsDelete, Holiday objHol, string strIsFestival)
    {
        string[] arinfo = new string[4];
        char[] splitter = { ',' };
        arinfo = Common.str_split(objHol.HoliDays, splitter);
        int i = 0;
        //SqlCommand[] cmd;
        //cmd = new SqlCommand[4];

        SqlCommand[] command;
        command = new SqlCommand[arinfo.Length + 1];
  
        command[0] = new SqlCommand("proc_Insert_Holiday");
        command[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_HolidayYear = command[0].Parameters.Add("HolidayYear", SqlDbType.Char);
        p_HolidayYear.Direction = ParameterDirection.Input;
        p_HolidayYear.Value = objHol.HolidayYear;

        SqlParameter p_HoliDayId = command[0].Parameters.Add("HoliDayId", SqlDbType.BigInt);
        p_HoliDayId.Direction = ParameterDirection.Input;
        p_HoliDayId.Value = objHol.HoliDayId;

        SqlParameter p_HolidayName = command[0].Parameters.Add("HolidayName", SqlDbType.VarChar);
        p_HolidayName.Direction = ParameterDirection.Input;
        p_HolidayName.Value = objHol.HolidayName;

        SqlParameter p_StartDate = command[0].Parameters.Add("StartDate", SqlDbType.DateTime);
        p_StartDate.Direction = ParameterDirection.Input;
        p_StartDate.Value = objHol.StartDate;

        SqlParameter p_EndDate = command[0].Parameters.Add("EndDate", SqlDbType.DateTime);
        p_EndDate.Direction = ParameterDirection.Input;
        p_EndDate.Value = objHol.EndDate;

        SqlParameter p_Duration = command[0].Parameters.Add("Duration", SqlDbType.Decimal);
        p_Duration.Direction = ParameterDirection.Input;
        p_Duration.Value = objHol.Duration;

        SqlParameter p_HoliDesc = command[0].Parameters.Add("HoliDesc", SqlDbType.VarChar);
        p_HoliDesc.Direction = ParameterDirection.Input;
        p_HoliDesc.Value = objHol.HoliDesc;

        SqlParameter p_IsActive = command[0].Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = objHol.IsActive;

        SqlParameter p_InsertedBy = command[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = objHol.InsertedBy;

        SqlParameter p_InsertedDate = command[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = objHol.InsertedDate;

        SqlParameter p_IsUpdate = command[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command[0].Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        SqlParameter p_IsFestival = command[0].Parameters.Add("IsFestival", SqlDbType.Char);
        p_IsFestival.Direction = ParameterDirection.Input;
        p_IsFestival.Value = strIsFestival;

        // For Inserting Holiday Detail Data
        if (IsDelete != "Y")
        {
            if (arinfo.Length > 0)
            {
                for (i = 0; i <= arinfo.Length - 1; i++)
                {
                    command[i + 1] = new SqlCommand("proc_Insert_HolidaysDetls");
                    command[i + 1].CommandType = CommandType.StoredProcedure;

                    p_HolidayYear = command[i + 1].Parameters.Add("HolidayYear", SqlDbType.Char);
                    p_HolidayYear.Direction = ParameterDirection.Input;
                    p_HolidayYear.Value = objHol.HolidayYear;

                    p_HoliDayId = command[i + 1].Parameters.Add("HoliDayId", SqlDbType.BigInt);
                    p_HoliDayId.Direction = ParameterDirection.Input;
                    p_HoliDayId.Value = objHol.HoliDayId;

                    SqlParameter p_HoliDate = command[i + 1].Parameters.Add("HoliDate", SqlDbType.DateTime);
                    p_HoliDate.Direction = ParameterDirection.Input;
                    p_HoliDate.Value = Common.SetDate(arinfo[i]);

                    p_InsertedBy = command[i + 1].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                    p_InsertedBy.Direction = ParameterDirection.Input;
                    p_InsertedBy.Value = objHol.InsertedBy;

                    p_InsertedDate = command[i + 1].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                    p_InsertedDate.Direction = ParameterDirection.Input;
                    p_InsertedDate.Value = objHol.InsertedDate;
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
            command = null;
        }
    }

    public void InsertMultipleHoliday(GridView grMulti, int CheckedDataCount,string ToYear,string strInsBy, string strInsDate,string IsUpdate,string IsDelete)
    {
        int i = 0;
        SqlCommand[] command;
        command = new SqlCommand[50];

        string HoliDayId = "";

        foreach (GridViewRow gRow in grMulti.Rows)
        {
            CheckBox chBox = new CheckBox();
            chBox = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            DataTable dtMultiHoliday = new DataTable();
            if (chBox.Checked == true)
            {
                //Select from HolidayMst

                dtMultiHoliday = GetMultiHoliday(ToYear, gRow.Cells[2].Text.Trim(), gRow.Cells[3].Text.Trim());
                if (dtMultiHoliday.Rows.Count > 0)
                {
                    //Delete Selected Holiday Info from 
                    command[i] = new SqlCommand("proc_Delete_MultiHoliday");

                    SqlParameter p_HoliDayId_D = command[i].Parameters.Add("HoliDayId", SqlDbType.BigInt);
                    p_HoliDayId_D.Direction = ParameterDirection.Input;
                    p_HoliDayId_D.Value = dtMultiHoliday.Rows[0]["HoliDayId"];

                    SqlParameter p_HolidayYear_D = command[i].Parameters.Add("HoliDayId", SqlDbType.BigInt);
                    p_HolidayYear_D.Direction = ParameterDirection.Input;
                    p_HolidayYear_D.Value = dtMultiHoliday.Rows[0]["HolidayYear"];
                }

                //Insert Into HolidayMst

                command[i] = new SqlCommand("proc_Insert_MultiHolidayMst");
                command[i].CommandType = CommandType.StoredProcedure;

                SqlParameter p_HolidayYear = command[i].Parameters.Add("HolidayYear", SqlDbType.Char);
                p_HolidayYear.Direction = ParameterDirection.Input;
                p_HolidayYear.Value = ToYear;

                SqlParameter p_HoliDayId = command[i].Parameters.Add("HoliDayId", SqlDbType.BigInt);
                p_HoliDayId.Direction = ParameterDirection.Input;
                p_HoliDayId.Value = Common.getMaxId("HolidaysMst", "HoliDayId");
                HoliDayId = p_HoliDayId.Value.ToString();

                SqlParameter p_HolidayName = command[i].Parameters.Add("HolidayName", SqlDbType.VarChar);
                p_HolidayName.Direction = ParameterDirection.Input;
                p_HolidayName.Value = gRow.Cells[1].Text.Trim();

                SqlParameter p_StartDate = command[i].Parameters.Add("StartDate", SqlDbType.DateTime);
                p_StartDate.Direction = ParameterDirection.Input;
                p_StartDate.Value = gRow.Cells[2].Text.Trim();

                SqlParameter p_EndDate = command[i].Parameters.Add("EndDate", SqlDbType.DateTime);
                p_EndDate.Direction = ParameterDirection.Input;
                p_EndDate.Value = gRow.Cells[3].Text.Trim();

                SqlParameter p_Duration = command[i].Parameters.Add("Duration", SqlDbType.Decimal);
                p_Duration.Direction = ParameterDirection.Input;
                p_Duration.Value = gRow.Cells[4].Text.Trim();

                SqlParameter p_HoliDesc = command[i].Parameters.Add("HoliDesc", SqlDbType.VarChar);
                p_HoliDesc.Direction = ParameterDirection.Input;
                p_HoliDesc.Value = "";

                SqlParameter p_IsActive = command[i].Parameters.Add("IsActive", SqlDbType.Char);
                p_IsActive.Direction = ParameterDirection.Input;
                p_IsActive.Value = gRow.Cells[5].Text.Trim();

                SqlParameter p_InsertedBy = command[i].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                p_InsertedBy.Direction = ParameterDirection.Input;
                p_InsertedBy.Value = strInsBy;

                SqlParameter p_InsertedDate = command[i].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                p_InsertedDate.Direction = ParameterDirection.Input;
                p_InsertedDate.Value = strInsDate;
                i++;

                //Insert Into HolidayDtls
                int j = 0;
                double TotDay = 0;
                DateTime dtFrom = new DateTime();
                DateTime dtTo = new DateTime();

                char[] splitter = { '/' };
                string[] arinfo = Common.str_split(gRow.Cells[2].Text.Trim(), splitter);
                if (arinfo.Length == 3)
                {
                    dtFrom = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                    arinfo = null;
                }
                arinfo = Common.str_split(gRow.Cells[3].Text.Trim(), splitter);
                if (arinfo.Length == 3)
                {
                    dtTo = Convert.ToDateTime(arinfo[2] + "/" + arinfo[1] + "/" + arinfo[0]);
                    arinfo = null;
                }

                TimeSpan Dur = dtTo.Subtract(dtFrom);

                TotDay = Math.Round(Convert.ToDouble(Dur.Days), 0) + 1;

                DateTime LDate = dtFrom;
                int row = 0;
                int LeaveDay = 0;
                HiddenField hfLDates = new HiddenField();
                hfLDates.Value = "";
                for (row = 0; row < Convert.ToInt32(TotDay); row++)
                {
                    if (hfLDates.Value != "")
                    {
                        hfLDates.Value = hfLDates.Value + ",";
                    }
                    LeaveDay = LeaveDay + 1;
                    hfLDates.Value = hfLDates.Value + Common.SetDate(LDate.ToString());
                    LDate = LDate.AddDays(1);
                }

                string[] arinfo3 = new string[10];
                char[] splitter3 = { ',' };
                arinfo3 = Common.str_split(hfLDates.Value.ToString(), splitter3);

                j = i;
                TotDay = TotDay + i;
                for (j = i; j < TotDay; j++)
                {
                    command[j] = new SqlCommand("proc_Insert_MultiHolidaysDetls");
                    command[j].CommandType = CommandType.StoredProcedure;

                    p_HolidayYear = command[j].Parameters.Add("HolidayYear", SqlDbType.Char);
                    p_HolidayYear.Direction = ParameterDirection.Input;
                    p_HolidayYear.Value = ToYear;

                    p_HoliDayId = command[j].Parameters.Add("HoliDayId", SqlDbType.BigInt);
                    p_HoliDayId.Direction = ParameterDirection.Input;
                    p_HoliDayId.Value = HoliDayId;

                    SqlParameter p_HoliDate = command[j].Parameters.Add("HoliDate", SqlDbType.DateTime);
                    p_HoliDate.Direction = ParameterDirection.Input;
                    p_HoliDate.Value = arinfo3[j - i];

                    p_InsertedBy = command[j].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                    p_InsertedBy.Direction = ParameterDirection.Input;
                    p_InsertedBy.Value = strInsBy;

                    p_InsertedDate = command[j].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                    p_InsertedDate.Direction = ParameterDirection.Input;
                    p_InsertedDate.Value = strInsDate;
                }
                i = i + j;
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


    public void InsertEventDay(string strEventYear, string strEventId, string strTitle, string strFromDate, string strToDate,string strEventDays, string strFlag,
        string strIsActive, string strInsBy, string strInsDate, string IsUpdate, string IsDelete)
    {
        string[] arinfo = new string[4];
        char[] splitter = { ',' };
        arinfo = Common.str_split(strEventDays, splitter);
        int i = 0;
        //SqlCommand[] cmd;
        //cmd = new SqlCommand[4];

        SqlCommand[] command;
        command = new SqlCommand[arinfo.Length + 1];

        command[0] = new SqlCommand("proc_Insert_EventDaysMst");
        command[0].CommandType = CommandType.StoredProcedure;

        SqlParameter p_EventDayYear = command[0].Parameters.Add("EventDayYear", SqlDbType.Char);
        p_EventDayYear.Direction = ParameterDirection.Input;
        p_EventDayYear.Value = strEventYear;

        SqlParameter p_EventDayId = command[0].Parameters.Add("EventDayId", SqlDbType.BigInt);
        p_EventDayId.Direction = ParameterDirection.Input;
        p_EventDayId.Value = strEventId;

        SqlParameter p_Title = command[0].Parameters.Add("Title", SqlDbType.VarChar);
        p_Title.Direction = ParameterDirection.Input;
        p_Title.Value = strTitle;

        SqlParameter p_FromDate = command[0].Parameters.Add("FromDate", DBNull.Value);
        p_FromDate.Direction = ParameterDirection.Input;
        p_FromDate.IsNullable = true;
        if (strFromDate != "")
            p_FromDate.Value = Common.ReturnDate(strFromDate);

        SqlParameter p_ToDate = command[0].Parameters.Add("ToDate", DBNull.Value);
        p_ToDate.Direction = ParameterDirection.Input;
        p_ToDate.IsNullable = true;
        if (strToDate != "")
            p_ToDate.Value =  Common.ReturnDate(strToDate);

        SqlParameter p_Flag = command[0].Parameters.Add("Flag", SqlDbType.VarChar);
        p_Flag.Direction = ParameterDirection.Input;
        p_Flag.Value = strFlag;

        SqlParameter p_IsActive = command[0].Parameters.Add("IsActive", SqlDbType.Char);
        p_IsActive.Direction = ParameterDirection.Input;
        p_IsActive.Value = strIsActive;

        SqlParameter p_InsertedBy = command[0].Parameters.Add("InsertedBy", SqlDbType.VarChar);
        p_InsertedBy.Direction = ParameterDirection.Input;
        p_InsertedBy.Value = strInsBy;

        SqlParameter p_InsertedDate = command[0].Parameters.Add("InsertedDate", SqlDbType.DateTime);
        p_InsertedDate.Direction = ParameterDirection.Input;
        p_InsertedDate.Value = strInsDate;

        SqlParameter p_IsUpdate = command[0].Parameters.Add("IsUpdate", SqlDbType.Char);
        p_IsUpdate.Direction = ParameterDirection.Input;
        p_IsUpdate.Value = IsUpdate;

        SqlParameter p_IsDelete = command[0].Parameters.Add("IsDelete", SqlDbType.Char);
        p_IsDelete.Direction = ParameterDirection.Input;
        p_IsDelete.Value = IsDelete;

        // For Inserting Holiday Detail Data
        if (IsDelete != "Y")
        {
            if (arinfo.Length > 0)
            {
                for (i = 0; i <= arinfo.Length - 1; i++)
                {
                    command[i + 1] = new SqlCommand("proc_Insert_EventDaysDetls");
                    command[i + 1].CommandType = CommandType.StoredProcedure;

                    p_EventDayYear = command[i + 1].Parameters.Add("EventDayYear", SqlDbType.Char);
                    p_EventDayYear.Direction = ParameterDirection.Input;
                    p_EventDayYear.Value = strEventYear;

                    p_EventDayId = command[i + 1].Parameters.Add("EventDayId", SqlDbType.BigInt);
                    p_EventDayId.Direction = ParameterDirection.Input;
                    p_EventDayId.Value = strEventId;

                    SqlParameter p_EventDate = command[i + 1].Parameters.Add("EventDate", SqlDbType.DateTime);
                    p_EventDate.Direction = ParameterDirection.Input;
                    p_EventDate.Value = Common.SetDate(arinfo[i]);

                    p_InsertedBy = command[i + 1].Parameters.Add("InsertedBy", SqlDbType.VarChar);
                    p_InsertedBy.Direction = ParameterDirection.Input;
                    p_InsertedBy.Value = strInsBy;

                    p_InsertedDate = command[i + 1].Parameters.Add("InsertedDate", SqlDbType.DateTime);
                    p_InsertedDate.Direction = ParameterDirection.Input;
                    p_InsertedDate.Value = strInsDate;
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
            command = null;
        }
    }

    public DataTable GetMultiHoliday(string sToYear,string sStartDate,string sEndDate)
    {
        if (objDC.ds.Tables["MultiHoliday"] != null)
        {
            objDC.ds.Tables["MultiHoliday"].Clear();
            objDC.ds.Tables["MultiHoliday"].Dispose(); 
        }
        SqlCommand cmd = new SqlCommand("proc_Select_MultiHoliday");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_HolidayYear = cmd.Parameters.Add("HolidayYear", SqlDbType.Char);
        p_HolidayYear.Direction = ParameterDirection.Input;
        p_HolidayYear.Value = sToYear;

        SqlParameter p_StartDate = cmd.Parameters.Add("StartDate", SqlDbType.DateTime);
        p_StartDate.Direction = ParameterDirection.Input;
        p_StartDate.Value = Common.ReturnDate(sStartDate);

        SqlParameter p_EndDate = cmd.Parameters.Add("EndDate", SqlDbType.DateTime);
        p_EndDate.Direction = ParameterDirection.Input;
        p_EndDate.Value = Common.ReturnDate(sEndDate);

        objDC.CreateDSFromProc(cmd, "MultiHoliday");
        return objDC.ds.Tables["MultiHoliday"];
    }

    public DataTable GetData(string strHolidayYear, string strGetListData)
    {
        SqlCommand command = new SqlCommand("proc_Select_Holiday");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_HolidayYear = command.Parameters.Add("HolidayYear", SqlDbType.Char);
        p_HolidayYear.Direction = ParameterDirection.Input;
        p_HolidayYear.Value = strHolidayYear;

        SqlParameter p_GetListData = command.Parameters.Add("GetListData", SqlDbType.Char);
        p_GetListData.Direction = ParameterDirection.Input;
        p_GetListData.Value = strGetListData;

        objDC.CreateDSFromProc(command, "Holiday");
        return objDC.ds.Tables["Holiday"];
    }

    public DataTable GetMultipleData(string strHolidayYear)
    {
        SqlCommand command = new SqlCommand("proc_Select_Holiday");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_HolidayYear = command.Parameters.Add("HolidayYear", SqlDbType.Char);
        p_HolidayYear.Direction = ParameterDirection.Input;
        p_HolidayYear.Value = strHolidayYear;

        SqlParameter p_GetAllData = command.Parameters.Add("GetListData", SqlDbType.Char);
        p_GetAllData.Direction = ParameterDirection.Input;
        p_GetAllData.Value = "N";        
       
        objDC.CreateDSFromProc(command, "HolidayMulti");
        return objDC.ds.Tables["HolidayMulti"];
    }


    public DataTable GetChildData(string strHolidayYear)
    {
        SqlCommand command = new SqlCommand("proc_Select_HolidayDetls");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_HolidayYear = command.Parameters.Add("HolidayYear", SqlDbType.Char);
        p_HolidayYear.Direction = ParameterDirection.Input;
        p_HolidayYear.Value = strHolidayYear;
     
        objDC.CreateDSFromProc(command, "HolidayDetls");
        return objDC.ds.Tables["HolidayDetls"];
    }

    public DataTable GetDataList()
    {
        SqlCommand command = new SqlCommand("proc_Select_Holiday");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_HolidayYear = command.Parameters.Add("HolidayYear", SqlDbType.Char);
        p_HolidayYear.Direction = ParameterDirection.Input;
        p_HolidayYear.Value = "";

        SqlParameter p_GetAllData = command.Parameters.Add("GetListData", SqlDbType.Char);
        p_GetAllData.Direction = ParameterDirection.Input;
        p_GetAllData.Value = "Y";

        objDC.CreateDSFromProc(command, "HolidayList");
        return objDC.ds.Tables["HolidayList"];
    }

    public DataTable GetDataEventDay(string strEventDayYear, string strEventDayId)
    {
        SqlCommand command = new SqlCommand("proc_Select_EventDay");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_EventDayYear = command.Parameters.Add("EventDayYear", SqlDbType.Char);
        p_EventDayYear.Direction = ParameterDirection.Input;
        p_EventDayYear.Value = strEventDayYear;

        SqlParameter p_EventDayId = command.Parameters.Add("EventDayId", SqlDbType.BigInt);
        p_EventDayId.Direction = ParameterDirection.Input;
        p_EventDayId.Value = strEventDayId;

        objDC.CreateDSFromProc(command, "EventDay");
        return objDC.ds.Tables["EventDay"];
    }

    public DataTable GetEventChildData(string strHolidayYear)
    {
        SqlCommand command = new SqlCommand("proc_Select_HolidayDetls");
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter p_HolidayYear = command.Parameters.Add("HolidayYear", SqlDbType.Char);
        p_HolidayYear.Direction = ParameterDirection.Input;
        p_HolidayYear.Value = strHolidayYear;

        objDC.CreateDSFromProc(command, "HolidayDetls");
        return objDC.ds.Tables["HolidayDetls"];
    }
	public HolidayTableManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
