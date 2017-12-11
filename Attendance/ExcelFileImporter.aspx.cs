using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data.OleDb;
using System.Text.RegularExpressions;

public partial class Attendance_ExcelFileImporter : System.Web.UI.Page
{
    DataTable dtTextFile = new DataTable();
    dsTextFileImporter objDS = new dsTextFileImporter();
    TextFileImpManager objImpTxtMgr = new TextFileImpManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.OpenRecord("0");
            grTextData.DataSource = dtTextFile;
            grTextData.DataBind();
            foreach (GridViewRow gRow in grTextData.Rows)
            {
                gRow.Cells[4].Text = Common.SetDate(gRow.Cells[4].Text.Trim());
                gRow.Cells[5].Text = Common.SetDateTime(gRow.Cells[5].Text.Trim());
            }
            if (grTextData.Rows.Count > 0)
            {
                lblMsg.Text = "There are some already uploaded data. <br /> Please click on the Prepare for merging and Save them.";
                btnApply.Enabled = true;
            }
            else
                btnApply.Enabled = false;
        }
    }

    protected void OpenRecord(string strStatus)
    {
        dtTextFile = objImpTxtMgr.GetAttendanceRecordTextFile(strStatus);
    }

    protected void ReadDataFromExcelFile()
    {

        // string baseLocation = "C:\\temp\\";
        string FolderPath = ConfigurationManager.AppSettings["AttnTextFilePath"];
        string status = "";
        if (FileUpload1.HasFile == false)
        {
            lblMsg.Text = "Error - a file name must be specified.";
            return;
        }
        if (FileUpload1.HasFile == true)
        {
            string fn = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            string FilePath = Server.MapPath(FolderPath + "/" + fn);
            FileInfo File = new FileInfo(FilePath);
            if (File.Exists == true)
            {
                lblMsg.Text = "This file has already marged and now replaced with the new one.";
                File.Delete();
            }
            FileUpload1.PostedFile.SaveAs(FilePath);
            this.OpenRecord("2");
            //this.AllExcelToDataTable(FilePath, true);

            FileUpload1.SaveAs(FilePath);
            string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=" + FilePath + ";Extended Properties=Excel 8.0";
            

            OleDbConnection conn = new OleDbConnection(connstr);
            string strSQL = "SELECT * FROM [Sheet1$]";
            OleDbCommand cmd = new OleDbCommand(strSQL, conn);
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(ds);
            grTextData.DataSource = ds;

            //grTextData.DataSource = null;
            //grTextData.DataSource = objDS.Tables["dtAttRecordTextFile"];
            grTextData.DataBind();
            FilterGridView();
            lblMsg.Text = "File uploaded successfully.";
        }
    }

    public void FilterGridView()
    {
        foreach (GridViewRow gRow in grTextData.Rows)
        {
            if (string.Compare(gRow.Cells[2].Text.Substring(0, 1), "-") == 0)
            {
                gRow.Visible = false;
            }
            //string[] CellData = gRow.Cells[4].Text.Split('/');
            //gRow.Cells[4].Text = CellData[2] + "/" + CellData[0] + "/" + CellData[1];
            char[] strSpliter = { ' ' };
            string[] arinfo = new string[2];

            arinfo = gRow.Cells[5].Text.ToString().Split(strSpliter);

            gRow.Cells[5].Text = gRow.Cells[4].Text + " " + arinfo[1] + " " + arinfo[2];

            DataTable dt = objImpTxtMgr.GetCardNoWiseEmployee(gRow.Cells[1].Text.Trim());
            if (dt.Rows.Count > 0)
            {
                gRow.Cells[2].Text = dt.Rows[0]["EmpId"].ToString();
                gRow.Cells[3].Text = dt.Rows[0]["FullName"].ToString();
            }
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        this.ReadDataFromExcelFile();
    }

    protected void btnMerge_Click(object sender, EventArgs e)
    {
        if (grTextData.Rows.Count == 0)
        {
            lblMsg.Text = "No uploaded data found for merging.";
            return;
        }
        dtTextFile.Rows.Clear();
        this.OpenRecord("0");
        if (dtTextFile.Rows.Count == 0)
        {
            objImpTxtMgr.ImportData(grTextData);
            this.OpenRecord("0");
        }
        DataTable dtEmpList = objImpTxtMgr.GetDistinctEmpID();
        DataTable dtMinMax = objImpTxtMgr.GetMinMaxAttndDate();


        if (dtEmpList.Rows.Count == 0)
        {
            lblLog.Text = "There is record to merge";
            return;
        }
        if (dtMinMax.Rows.Count == 0)
        {
            lblLog.Text = "There is record to merge";
            return;
        }
        DateTime MinDate = Convert.ToDateTime(dtMinMax.Rows[0]["MinDate"]);
        DateTime MaxDate = Convert.ToDateTime(dtMinMax.Rows[0]["MaxDate"]);
        DataRow[] foundAttnRows;
        DataTable dtAttnd = objImpTxtMgr.GetLoginLogoutData(Common.SetDate(MinDate.ToShortDateString()), Common.SetDate(MaxDate.ToShortDateString()));

        while (MinDate <= MaxDate)
        {
            foundAttnRows = null;

            string strMinDate = Common.SetDate(MinDate.ToShortDateString());
            foreach (DataRow dEmpRow in dtEmpList.Rows)
            {
                DataRow[] foundRows = dtTextFile.Select("EMPID='" + dEmpRow["EMPID"].ToString().Trim() + "' AND ATTDATE='" + strMinDate + "'");
                if (dtAttnd.Rows.Count > 0)
                    foundAttnRows = dtAttnd.Select("USERNAME='" + dEmpRow["EMPID"].ToString().Trim() + "' AND ATTNDDATE='" + strMinDate + "'");

                if (foundRows.Length > 0)
                {
                    DataRow nRow = objDS.dtLoginLogout.NewRow();
                    nRow["UserName"] = dEmpRow["EMPID"].ToString().Trim();
                    if (foundAttnRows != null)
                    {
                        if (foundAttnRows.Length > 0)
                        {
                            if ((string.IsNullOrEmpty(foundAttnRows[0]["LoginTime"].ToString()) == false) && (string.IsNullOrEmpty(foundRows[foundRows.Length - 1]["ATTTIME"].ToString()) == false))
                            {
                                if (Convert.ToDateTime(foundRows[0]["ATTTIME"]) < Convert.ToDateTime(foundAttnRows[0]["LoginTime"]))
                                    nRow["LoginTime"] = Common.SetDateTime(foundRows[0]["ATTTIME"].ToString());
                                else
                                    nRow["LoginTime"] = Common.SetDateTime(foundAttnRows[0]["LoginTime"].ToString());
                            }

                            if (foundRows.Length > 1)
                            {
                                if ((string.IsNullOrEmpty(foundAttnRows[0]["LogoutTime"].ToString()) == false) && (string.IsNullOrEmpty(foundRows[foundRows.Length - 1]["ATTTIME"].ToString()) == false))
                                {
                                    if (Convert.ToDateTime(foundRows[foundRows.Length - 1]["ATTTIME"]) > Convert.ToDateTime(foundAttnRows[0]["LogoutTime"]))
                                        nRow["LogoutTime"] = Common.SetDateTime(foundRows[foundRows.Length - 1]["ATTTIME"].ToString());
                                    else
                                        nRow["LogoutTime"] = Common.SetDateTime(foundAttnRows[0]["LogoutTime"].ToString());
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(foundRows[foundRows.Length - 1]["ATTTIME"].ToString()) == false)
                                        nRow["LogoutTime"] = Common.SetDateTime(foundRows[foundRows.Length - 1]["ATTTIME"].ToString());
                                }
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(foundAttnRows[0]["LogoutTime"].ToString()) == false)
                                    nRow["LogoutTime"] = Common.SetDateTime(foundAttnRows[0]["LogoutTime"].ToString());
                                else
                                    nRow["LogoutTime"] = "";
                            }
                            nRow["IsExist"] = "Y";
                        }
                        else
                        {
                            nRow["LoginTime"] = Common.SetDateTime(foundRows[0]["ATTTIME"].ToString());
                            if (foundRows.Length > 1)
                                nRow["LogoutTime"] = Common.SetDateTime(foundRows[foundRows.Length - 1]["ATTTIME"].ToString());
                            else
                                nRow["LogoutTime"] = "";

                            nRow["IsExist"] = "N";
                        }

                    }
                    else
                    {
                        nRow["LoginTime"] = Common.SetDateTime(foundRows[0]["ATTTIME"].ToString());
                        if (foundRows.Length > 1)
                            nRow["LogoutTime"] = Common.SetDateTime(foundRows[foundRows.Length - 1]["ATTTIME"].ToString());
                        else
                            nRow["LogoutTime"] = "";

                        nRow["IsExist"] = "N";
                    }
                    objDS.dtLoginLogout.Rows.Add(nRow);
                }

            }
            MinDate = MinDate.AddDays(1);
        }
        objDS.dtLoginLogout.AcceptChanges();
        grLoginLogout.DataSource = objDS.Tables["dtLoginLogout"];
        grLoginLogout.DataBind();
        this.FormatGridLoginLogOut();
        lblMsg.Text = "Preparation of data merging is completed. <br /> Please click on the Merge Data Button.";
        btnApply.Enabled = true;
    }

    private void FormatGridLoginLogOut()
    {
        foreach (GridViewRow gRow in grLoginLogout.Rows)
        {
            if (gRow.Cells[3].Text == "Y")
                gRow.BackColor = System.Drawing.Color.FromArgb(255, 153, 051);
        }
    }

    public void MergeData()
    {

    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        if (grLoginLogout.Rows.Count == 0)
        {
            lblMsg.Text = "Please prepare the data for merging first then proceed.";
            return;
        }
        objImpTxtMgr.MergeData(grLoginLogout);
        lblMsg.Text = "Data Merging is completed successfully.";
        btnApply.Enabled = false;
        btnMerge.Enabled = false;
    }
}
