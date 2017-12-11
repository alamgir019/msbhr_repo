using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Globalization;

public partial class Attendance_AttnLogEdit : System.Web.UI.Page
{
    EmpInfoManager objEmpMgr = new EmpInfoManager();
    AttnManager objPayRptMgr = new AttnManager();
    TimeSheetManager objTSM = new TimeSheetManager();
    DataTable dtReport;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }


    private string GetColumnName(string col)
    {
        string strCol = "";
        return strCol;
    }



    protected StringWriter GetHeaderText()
    {
        StringWriter sw = new StringWriter();
        sw.WriteLine("<table style=" + "\"width:100%;margin-top:10px;text-align:left;border-collapse:collapse;border:solid 1px white;\">");
        sw.WriteLine("<tr>");
        sw.WriteLine("<td style=" + "\"text-align:left;border:solid 1px white;font-size:20px;font-weight:bold;\">");
        sw.WriteLine("Marie Stopes ");
        sw.WriteLine("</td>");
        sw.WriteLine("</tr>");
        sw.WriteLine("<tr>");
        sw.WriteLine("<td style=" + "\"text-align:right;border:solid 1px white;font-size:14px\">");
        //sw.WriteLine(lblGenerateFor.Text.ToString());
        sw.WriteLine("</td>");
        sw.WriteLine("</tr>");
        sw.WriteLine("<tr>");
        sw.WriteLine("<td style=" + "\"text-align:right;border:solid 1px white;font-size:14px\">");
        //sw.WriteLine(lblPayrollMonth.Text.ToString());
        sw.WriteLine("</td>");
        sw.WriteLine("</tr>");
        sw.WriteLine("</table>");
        return sw;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the
        // specified ASP.NET server control at run time.
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.SaveData("N");
    }


    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
    }

    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        try
        {
            string EmpID = Session["EMPLOYEEID"].ToString();

            string strMonth = Convert.ToString(DateTime.Today.Month);
            string strYear = Convert.ToString(DateTime.Today.Year);

            clsCommonSetup objCommonSetup = new clsCommonSetup(lngID.ToString(), "", "Y", "N", Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "N", "N");

            objPayRptMgr.UpdateAttandance(objCommonSetup, this.ddlRemarksType.SelectedItem.Value.ToString(), this.txtRemarks.Text.ToString(),
                this.txtFromDate.Text.ToString(), this.txtToDate.Text.ToString(), EmpID);

            objTSM.GenerateTimeSheet(EmpID, strMonth, strYear, Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));

            this.UpdateFoxpro();

            lblMsg.Text = Common.GetMessage("Y", IsDelete);

            Common.EmptyTextBoxValues(this);
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    private void UpdateFoxpro()
    {
        string dbfFileName = @"C:\Program Files\FingerTec Worldwide\TCMSv2\attend.dbf";
        string dbfFileName2 = @"C:\Program Files\FingerTec Worldwide\TCMSv2\badge.dbf";

        string constr = "Provider=VFPOLEDB.1;Data Source=" + Directory.GetParent(dbfFileName).FullName;

        string ExcelFileName = AppDomain.CurrentDomain.BaseDirectory + "converted_file.xls";

        string fDate = Convert.ToDateTime(Common.ReturnDate(txtFromDate.Text.Trim())).ToString("yyyy-MM-dd");// DateTime.Parse(txtFromDate.Text.Trim()).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
        string TDate = Convert.ToDateTime(Common.ReturnDate(txtToDate.Text.Trim())).ToString("yyyy-MM-dd");//.ToShortDateString();//.ToString("MM/dd/yyyy");// DateTime.Parse(txtToDate.Text.Trim()).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

        using (OleDbConnection con = new OleDbConnection(constr))
        {
            string EmpID = Session["EMPLOYEEID"].ToString();
            OleDbCommand cmd;
            if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() != "")
            {
                var sql = "select badge.EMPNO as sl,badge.EMPNO,attend.name,attend.badgeno,attend.date,attend.daytype,attend.in,attend.out,REMARK from " +
                Path.GetFileName(dbfFileName) +
                " Inner join " + Path.GetFileName(dbfFileName2) + " on attend.BADGENO=badge.BADGENO where badge.EMPNO = '" + EmpID +
                "' AND attend.date Between " + "{^" + fDate + "} " +
                " AND " + "{^" + TDate + "}" +
                " ORDER BY attend.date";
                cmd = new OleDbCommand(sql, con);
            }
            else
            {
                var sql = "";
                cmd = new OleDbCommand(sql, con);
            }

            DataTable dt = new DataTable();

            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting database: " + ex.Message);
                return;
            }

            if (con.State == ConnectionState.Open)
            {
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                //Console.Write("Reading database...  ");
                da.Fill(dt);
                //Console.WriteLine("Completed.");
            }

            //if (con.State == ConnectionState.Open)
            //{
            //    try
            //    {
            //        con.Close();
            //    }
            //    catch { }
            //}

            if (dt != null && dt.Rows.Count > 0)
            {

                int i = 1;
                var sql1 = "";

                string strRemType = this.ddlRemarksType.SelectedItem.Value.ToString();
                if (strRemType == "1")
                {
                    foreach (DataRow Row in dt.Rows)
                    {
                        string strEmpID = Row["badgeno"].ToString();
                        string strDate = Convert.ToDateTime(Row["date"].ToString()).ToShortDateString();
                        sql1 = " update " + Path.GetFileName(dbfFileName) + " set attend.in='08:30', attend.DAYTYPE='WORKDAY'  where attend.BADGENO=" + strEmpID + " AND attend.DATE=CTOD('" + strDate + "') ";
                        Path.GetFileName(dbfFileName);
                        try
                        {
                            cmd = new OleDbCommand(sql1, con);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            lblMsg.Text = "";
                            throw (ex);
                        }
                    }
                }
                if (strRemType == "2")
                {
                    foreach (DataRow Row in dt.Rows)
                    {
                        string strEmpID = Row["badgeno"].ToString();
                        string strDate = Convert.ToDateTime(Row["date"].ToString()).ToShortDateString();
                        sql1 = "update " + Path.GetFileName(dbfFileName) + " set attend.out='16:30', attend.DAYTYPE='WORKDAY'  where attend.BADGENO=" + strEmpID + " AND attend.DATE=CTOD('" + strDate + "') ";
                        Path.GetFileName(dbfFileName);
                        try
                        {
                            cmd = new OleDbCommand(sql1, con);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            lblMsg.Text = "";
                            throw (ex);
                        }
                    }
                }
                if (strRemType == "3")
                {
                    foreach (DataRow Row in dt.Rows)
                    {
                        string strEmpID = Row["badgeno"].ToString();
                        string strDate = Convert.ToDateTime(Row["date"].ToString()).ToShortDateString();
                        sql1 = "update " + Path.GetFileName(dbfFileName) + " set attend.in='08:30', attend.out='16:30', attend.DAYTYPE='WORKDAY'  where attend.BADGENO=" + strEmpID + " AND attend.DATE=CTOD('" + strDate + "') ";
                        Path.GetFileName(dbfFileName);
                        try
                        {
                            cmd = new OleDbCommand(sql1, con);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            lblMsg.Text = "";
                            throw (ex);
                        }
                    }
                }
                if (strRemType == "4")
                {
                    foreach (DataRow Row in dt.Rows)
                    {
                        string strEmpID = Row["badgeno"].ToString();
                        string strDate = Convert.ToDateTime(Row["date"].ToString()).ToShortDateString();
                        sql1 = "update " + Path.GetFileName(dbfFileName) + " set attend.in='08:30', attend.out='16:30', attend.DAYTYPE='HOLIDAY'  where attend.BADGENO=" + strEmpID + " AND attend.DATE=CTOD('" + strDate + "') ";
                        Path.GetFileName(dbfFileName);
                        try
                        {
                            cmd = new OleDbCommand(sql1, con);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            lblMsg.Text = "";
                            throw (ex);
                        }
                    }
                }
                if (strRemType == "5")
                {
                    foreach (DataRow Row in dt.Rows)
                    {
                        string strEmpID = Row["badgeno"].ToString();
                        string strDate = Convert.ToDateTime(Row["date"].ToString()).ToShortDateString();
                        sql1 = "update " + Path.GetFileName(dbfFileName) + " set attend.in='12:30', attend.DAYTYPE='LEAVE', attend.remark='" + this.txtRemarks.Text + "'  where attend.BADGENO=" + strEmpID + " AND attend.DATE=CTOD('" + strDate + "') ";
                        Path.GetFileName(dbfFileName);
                        try
                        {
                            cmd = new OleDbCommand(sql1, con);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            lblMsg.Text = "";
                            throw (ex);
                        }
                    }
                }
                if (strRemType == "6")
                {
                    foreach (DataRow Row in dt.Rows)
                    {
                        string strEmpID = Row["badgeno"].ToString();
                        string strDate = Convert.ToDateTime(Row["date"].ToString()).ToShortDateString();
                        sql1 = "update " + Path.GetFileName(dbfFileName) + " set attend.out='12:30', attend.DAYTYPE='LEAVE',attend.remark='" + this.txtRemarks.Text + "'  where attend.BADGENO=" + strEmpID + " AND attend.DATE=CTOD('" + strDate + "') ";
                        Path.GetFileName(dbfFileName);
                        try
                        {
                            cmd = new OleDbCommand(sql1, con);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            lblMsg.Text = "";
                            throw (ex);
                        }
                    }
                }
                if (strRemType == "7")
                {
                    foreach (DataRow Row in dt.Rows)
                    {
                        string strEmpID = Row["badgeno"].ToString();
                        string strDate = Convert.ToDateTime(Row["date"].ToString()).ToShortDateString();
                        sql1 = "update " + Path.GetFileName(dbfFileName) + " set attend.in='08:30', attend.out='16:30', attend.DAYTYPE='WORKDAY'  where attend.BADGENO=" + strEmpID + " AND attend.DATE=CTOD('" + strDate + "') ";
                        Path.GetFileName(dbfFileName);
                        try
                        {
                            cmd = new OleDbCommand(sql1, con);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            lblMsg.Text = "";
                            throw (ex);
                        }
                    }
                }
                if (con.State == ConnectionState.Open)
                {
                    try
                    {
                        con.Close();
                    }
                    catch { }
                }

            }
        }
    }
}


