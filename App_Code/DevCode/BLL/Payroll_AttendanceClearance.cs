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
/// Summary description for Payroll_AttendanceClearance
/// </summary>
public class Payroll_AttendanceClearance
{
    DBConnector objDC = new DBConnector();

    public void InsertData(GridView grv, string strMonth, string strYear,string strClrDate, string strInsBy, string strInsDate)
    {
        int i = 0;
        SqlCommand[] command = new SqlCommand[grv.Rows.Count];
        long lngTransID = Convert.ToInt64(Common.getMaxId("AttendanceClearance", "TransId"));
        foreach (GridViewRow gRow in grv.Rows)
        {
            CheckBox chkB = (CheckBox)gRow.Cells[0].FindControl("chkBox");
            if (chkB.Checked == true)
            {
                command[i] = new SqlCommand("Proc_Payroll_Insert_AttandClearance");
                command[i].CommandType = CommandType.StoredProcedure;

                SqlParameter p_TRANSID= command[i].Parameters.Add("TRANSID", SqlDbType.BigInt);
                p_TRANSID.Direction = ParameterDirection.Input;
                p_TRANSID.Value = lngTransID.ToString();

                SqlParameter p_EMPID= command[i].Parameters.Add("EMPID", SqlDbType.Char);
                p_EMPID.Direction = ParameterDirection.Input;
                p_EMPID.Value = gRow.Cells[1].Text.Trim();

                SqlParameter p_VMONTH= command[i].Parameters.Add("VMONTH", SqlDbType.BigInt);
                p_VMONTH.Direction = ParameterDirection.Input;
                p_VMONTH.Value = strMonth;

                SqlParameter p_VYEAR= command[i].Parameters.Add("VYEAR", SqlDbType.BigInt);
                p_VYEAR.Direction = ParameterDirection.Input;
                p_VYEAR.Value = strYear;

                SqlParameter p_FROMDATE= command[i].Parameters.Add("FROMDATE", SqlDbType.DateTime);
                p_FROMDATE.Direction = ParameterDirection.Input;
                p_FROMDATE.Value = Common.ReturnDate(gRow.Cells[7].Text.Trim());

                SqlParameter p_TODATE= command[i].Parameters.Add("TODATE", SqlDbType.DateTime);
                p_TODATE.Direction = ParameterDirection.Input;
                p_TODATE.Value = Common.ReturnDate(gRow.Cells[8].Text.Trim());

                SqlParameter p_PAYSTARTDATE = command[i].Parameters.Add("PAYSTARTDATE", SqlDbType.DateTime);
                p_PAYSTARTDATE.Direction = ParameterDirection.Input;
                p_PAYSTARTDATE.Value = Common.ReturnDate(gRow.Cells[7].ToolTip.Trim());

                SqlParameter p_PAYENDDATE = command[i].Parameters.Add("PAYENDDATE", SqlDbType.DateTime);
                p_PAYENDDATE.Direction = ParameterDirection.Input;
                p_PAYENDDATE.Value = Common.ReturnDate(gRow.Cells[8].ToolTip.Trim());

                SqlParameter p_CLEARDATE= command[i].Parameters.Add("CLEARDATE", SqlDbType.DateTime);
                p_CLEARDATE.Direction = ParameterDirection.Input;
                p_CLEARDATE.Value = strClrDate;

                TextBox txtAttndDays = (TextBox)gRow.FindControl("txtAttndDays");
                SqlParameter p_DAYSDUR= command[i].Parameters.Add("DAYSDUR", SqlDbType.BigInt);
                p_DAYSDUR.Direction = ParameterDirection.Input;
                p_DAYSDUR.Value = txtAttndDays.Text.Trim();

                // Salary Duaration
                TextBox txtSD = (TextBox)gRow.Cells[10].FindControl("txtSalDays");
                if (string.IsNullOrEmpty(txtSD.Text.Trim()) == true)
                    txtSD.Text = "0";
                SqlParameter p_SALDUR = command[i].Parameters.Add("SALDUR", SqlDbType.BigInt);
                p_SALDUR.Direction = ParameterDirection.Input;
                p_SALDUR.Value = txtSD.Text.Trim();

                SqlParameter p_P= command[i].Parameters.Add("P", SqlDbType.BigInt);
                p_P.Direction = ParameterDirection.Input;
                p_P.Value = gRow.Cells[11].Text.Trim();

                SqlParameter p_A= command[i].Parameters.Add("A", SqlDbType.BigInt);
                p_A.Direction = ParameterDirection.Input;
                p_A.Value = gRow.Cells[12].Text.Trim();

                SqlParameter p_W= command[i].Parameters.Add("W", SqlDbType.BigInt);
                p_W.Direction = ParameterDirection.Input;
                p_W.Value = gRow.Cells[13].Text.Trim();

                SqlParameter p_WP= command[i].Parameters.Add("WP", SqlDbType.BigInt);
                p_WP.Direction = ParameterDirection.Input;
                p_WP.Value = gRow.Cells[14].Text.Trim();

                SqlParameter p_H= command[i].Parameters.Add("H", SqlDbType.BigInt);
                p_H.Direction = ParameterDirection.Input;
                p_H.Value = gRow.Cells[15].Text.Trim();

                SqlParameter p_HP= command[i].Parameters.Add("HP", SqlDbType.BigInt);
                p_HP.Direction = ParameterDirection.Input;
                p_HP.Value = gRow.Cells[16].Text.Trim();

                SqlParameter p_TV= command[i].Parameters.Add("TV", SqlDbType.BigInt);
                p_TV.Direction = ParameterDirection.Input;
                p_TV.Value = gRow.Cells[17].Text.Trim();

                SqlParameter p_LV= command[i].Parameters.Add("LV", SqlDbType.BigInt);
                p_LV.Direction = ParameterDirection.Input;
                p_LV.Value = gRow.Cells[18].Text.Trim();

                SqlParameter p_LWP = command[i].Parameters.Add("LWP", SqlDbType.BigInt);
                p_LWP.Direction = ParameterDirection.Input;
                p_LWP.Value = gRow.Cells[19].Text.Trim();

                //TextBox txtLWP = (TextBox)gRow.FindControl("txtLWP");
                //SqlParameter p_LWP = command[i].Parameters.Add("LWP", SqlDbType.BigInt);
                //p_LWP.Direction = ParameterDirection.Input;
                //p_LWP.Value = txtLWP.Text.Trim();

                SqlParameter p_ISIRREGULAR= command[i].Parameters.Add("ISIRREGULAR", SqlDbType.Char);
                p_ISIRREGULAR.Direction = ParameterDirection.Input;
                p_ISIRREGULAR.Value = gRow.Cells[20].Text.Trim();

                SqlParameter p_EMPTYPE= command[i].Parameters.Add("EMPTYPE", SqlDbType.Char);
                p_EMPTYPE.Direction = ParameterDirection.Input;
                p_EMPTYPE.Value = gRow.Cells[20].ToolTip.ToString().Trim();

                SqlParameter p_INSERTEDBY= command[i].Parameters.Add("INSERTEDBY", SqlDbType.VarChar);
                p_INSERTEDBY.Direction = ParameterDirection.Input;
                p_INSERTEDBY.Value = strInsBy;

                SqlParameter p_INSERTEDDATE= command[i].Parameters.Add("INSERTEDDATE", SqlDbType.DateTime);
                p_INSERTEDDATE.Direction = ParameterDirection.Input;
                p_INSERTEDDATE.Value = strInsDate;

                i++;
                lngTransID++;
            }
        }
        objDC.MakeTransaction(command);
    }

    public DataTable GetExistingClearanceRecord(string strMonth, string strYear, string strMPCID, string strEmpTypeId)
    {
        if (objDC.ds.Tables["GetExistingClearanceRecord"] != null)
        {
            objDC.ds.Tables["GetExistingClearanceRecord"].Rows.Clear();
            objDC.ds.Tables["GetExistingClearanceRecord"].Dispose();
        }

        SqlCommand cmd = new SqlCommand("proc_Payroll_Select_ExistingAttendanceClearance");
        cmd.CommandType = CommandType.StoredProcedure;

        SqlParameter p_VMONTH = cmd.Parameters.Add("VMONTH", SqlDbType.BigInt);
        p_VMONTH.Direction = ParameterDirection.Input;
        p_VMONTH.Value = strMonth;

        SqlParameter p_VYEAR = cmd.Parameters.Add("VYEAR", SqlDbType.BigInt);
        p_VYEAR.Direction = ParameterDirection.Input;
        p_VYEAR.Value = strYear;

        SqlParameter p_MPCID = cmd.Parameters.Add("MPCID", SqlDbType.BigInt);
        p_MPCID.Direction = ParameterDirection.Input;
        p_MPCID.Value = strMPCID;

        SqlParameter p_EmpTypeID = cmd.Parameters.Add("EmpTypeID", SqlDbType.BigInt);
        p_EmpTypeID.Direction = ParameterDirection.Input;
        p_EmpTypeID.Value = strEmpTypeId;

        objDC.CreateDSFromProc(cmd, "GetExistingClearanceRecord");
        return objDC.ds.Tables["GetExistingClearanceRecord"];
    }

    public void DeleteAttendanceClearance(GridView grAttendance, string month, string year)
    {
        SqlCommand[] command = new SqlCommand[grAttendance.Rows.Count];

        int i = 0;
        foreach (GridViewRow gRow in grAttendance.Rows)
        {
            CheckBox ChkBox = (CheckBox)gRow.FindControl("ChkBox");
            if (ChkBox.Checked == true)
            {
                command[i] = new SqlCommand("proc_Delete_AttandClearance");
                command[i].CommandType = CommandType.StoredProcedure;

                SqlParameter p_EmpId = command[i].Parameters.Add("EmpId", SqlDbType.VarChar);
                p_EmpId.Direction = ParameterDirection.Input;
                p_EmpId.Value = gRow.Cells[1].Text.Trim();

                SqlParameter p_VMONTH = command[i].Parameters.Add("VMONTH", SqlDbType.BigInt);
                p_VMONTH.Direction = ParameterDirection.Input;
                p_VMONTH.Value = month;

                SqlParameter p_VYEAR = command[i].Parameters.Add("VYEAR", SqlDbType.BigInt);
                p_VYEAR.Direction = ParameterDirection.Input;
                p_VYEAR.Value = year;
                i++;
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
		
	public Payroll_AttendanceClearance()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
