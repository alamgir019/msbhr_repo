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

public partial class Payroll_UploadTool : System.Web.UI.Page
{
    string FolderPath = ConfigurationManager.AppSettings["AllowanceFilePath"];
    UploadToolManager objUploadMgr = new UploadToolManager();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    EmpInfoManager objEmpMgr = new EmpInfoManager();
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnlEmpChangeType.Visible = false ;
        }
    }  
    
    protected void tvUpload_SelectedNodeChanged(object sender, EventArgs e)
    {
        pnlEmpChangeType.Visible = false;
        this.GridVisibility("0");                
        switch (tvUpload.SelectedValue)
        {             
            //case "CU":
            //    {
            //        this.GridVisibility("1", "0", "0", "0", "0", "0", "0", "0");                    
            //        break;
            //    }
            //case "IU":
            //    {
            //        this.GridVisibility("0", "1", "0", "0", "0", "0", "0", "0");
            //        break;
            //    }
            case "AU":
                {
                    this.GridVisibility("1");
                    break;
                }
            //case "ECU":
            //    {
            //        pnlEmpChangeType.Visible = true;
            //        ddlChangeType.SelectedIndex = 0; 
            //        this.EmpChangeVisibility();
            //        break;
            //    }
            //case "TU":
            //    {
            //        this.GridVisibility("0", "1", "0", "0", "0", "0", "0", "0");
            //        break;
            //    }
            //case "BU":
            //    {
            //        this.GridVisibility("1", "0", "0", "0", "0", "0", "0", "1");
            //        break;
            //    }
        }
    }
    private void EmpChangeVisibility()
    {
        if (ddlChangeType.SelectedValue == "0")
            this.GridVisibility("1");      
    }

    private void GridVisibility(string strBonusAllowance)
    {
        if (strBonusAllowance == "1")
            grAllowanceUpload.Visible = true;
        else
            grAllowanceUpload.Visible = false;
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            //string FilePath = "";
            //string Extension = "";
            //lblMsg.Text = "";

            //if (fileuploadExcel.HasFile)
            //{
            //    string FileName = Path.GetFileName(fileuploadExcel.PostedFile.FileName);
            //    Extension = Path.GetExtension(fileuploadExcel.PostedFile.FileName);
            //    string FolderPath = ConfigurationManager.AppSettings["UploadFilePath"];

            //    //FilePath = Server.MapPath("../" + FolderPath + "/" + FileName);
            //    FilePath = Server.MapPath(FolderPath + "/" + FileName);
            //    fileuploadExcel.SaveAs(FilePath);
            //}

            string Extension = Path.GetExtension(fileuploadExcel.PostedFile.FileName);
            string FileName = "BonusAllowanceUpload" + Extension;
            string FilePath = Server.MapPath(FolderPath + "/" + FileName);
            fileuploadExcel.SaveAs(FilePath);

            string connstr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=" + FilePath + ";Extended Properties=Excel 8.0";
            OleDbConnection conn = new OleDbConnection(connstr);
            string strSQL = "SELECT * FROM [Sheet1$]";

            OleDbCommand cmd = new OleDbCommand(strSQL, conn);
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(ds);
            grAllowanceUpload.DataSource = ds;
            grAllowanceUpload.DataBind();

            lblRecord.Text = "Total Records : " + grAllowanceUpload.Rows.Count.ToString();

            Import_To_Grid(FilePath, Extension);
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString ();
        }
    }

    private void Import_To_Grid(string FilePath, string Extension)
    {
        try
        {
            string conStr = "";
            this.EmpChangeVisibility();
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                             .ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                              .ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, 1);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();

            lblUpload.Visible = true;
            switch (tvUpload.SelectedValue)
            {
                case "AU":
                    {
                        lblUpload.Text = "All KInds of Allowances Upload";
                        grAllowanceUpload.DataSource = dt;
                        grAllowanceUpload.DataBind();

                        if (grAllowanceUpload.Rows.Count > 0)
                        {
                            foreach (GridViewRow gRow in grAllowanceUpload.Rows)
                            {
                                if (string.IsNullOrEmpty(gRow.Cells[8].Text.Trim()) == false)
                                    gRow.Cells[8].Text = Common.DisplayDate(gRow.Cells[8].Text);
                            }
                        }
                        break;
                    }             
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString(); 
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        switch (tvUpload.SelectedValue)
        {
            //case "CU":
            //    if (grCOLAUpload.Rows.Count > 0)
            //    {
            //        objUploadMgr.InsertCOLAAdjust(grCOLAUpload, Session["FISCALYRID"].ToString(), "", "0",
            //            Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "COLAUpload");

            //        lblMsg.Text = "Records updated successfully";
            //    }
            //    break;
            //case "IU":
            //    if (grIncUpload.Rows.Count > 0)
            //    {
            //        objUploadMgr.InsertCOLAAdjust(grIncUpload, Session["FISCALYRID"].ToString(), "", "0",
            //            Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "IncrementUpload");

            //        lblMsg.Text = "Records updated successfully";
            //    }
            //    break;
            case "AU":
                if (grAllowanceUpload.Rows.Count > 0)
                {
                    objUploadMgr.InsertBonusArrearImportData(grAllowanceUpload, Session["FISCALYRID"].ToString(),
                    "", Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()));
                    lblMsg.Text = "Records updated successfully";
                }
                break;
            //case "ECU":
            //    {
            //        if (ddlChangeType.SelectedValue.ToString() == "1")
            //        {
            //            if (grDesigUpload.Rows.Count > 0)
            //            {
            //                objUploadMgr.INSERT_EmpInfoChangeLog(grDesigUpload, "D", Session["USERID"].ToString(),
            //                    Common.SetDateTime(DateTime.Now.ToString()), "EmpChangeUpload");
            //                lblMsg.Text = "Designation has been updated successfully.";
            //            }
            //        }

            //        if (ddlChangeType.SelectedValue.ToString() == "2")
            //        {
            //            if (grFCUpload.Rows.Count > 0)
            //            {
            //                objUploadMgr.INSERT_EmpInfoChangeLog(grFCUpload, "F", Session["USERID"].ToString(),
            //                Common.SetDateTime(DateTime.Now.ToString()), "EmpChangeUpload");
            //                lblMsg.Text = "Fund Code has been updated successfully.";
            //            }
            //        }

            //        if (ddlChangeType.SelectedValue.ToString() == "3")
            //        {
            //            if (grPARLOCUpload.Rows.Count > 0)
            //            {
            //                objUploadMgr.INSERT_EmpInfoChangeLog(grPARLOCUpload, "L", Session["USERID"].ToString(),
            //                    Common.SetDateTime(DateTime.Now.ToString()), "EmpChangeUpload");
            //                lblMsg.Text = "PAR Location has been updated successfully.";

            //            }
            //        }

            //        if (ddlChangeType.SelectedValue.ToString() == "4")
            //        {
            //            if (grPARDeptUpload.Rows.Count > 0)
            //            {
            //                objUploadMgr.INSERT_EmpInfoChangeLog(grPARDeptUpload, "P", Session["USERID"].ToString(),
            //                    Common.SetDateTime(DateTime.Now.ToString()), "EmpChangeUpload");

            //                lblMsg.Text = "Payroll project has been updated successfully.";
            //            }
            //        }
            //    }
            //    break;
            //case "TU":
            //    if (grPayrollArr.Rows.Count > 0)
            //    {
            //        objUploadMgr.InsertPayrollArrear(grPayrollArr,DateTime.Today.Month.ToString(), Session["FISCALYRID"].ToString(), 
            //            Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()));

            //        lblMsg.Text = "Records updated successfully";
            //    }
            //    break;
            //case "BU":
            //    if (grBasicUpload.Rows.Count > 0)
            //    {                    
            //        objUploadMgr.UpdateBasic(grBasicUpload, Session["FISCALYRID"].ToString(), "", "0",
            //            Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "COLAUpload");

            //        lblMsg.Text = "Records updated successfully";
            //    }
            //    break;
        }
    }
    
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        DataTable dtReligion = new DataTable();      
        dtReligion = objMasMgr.SelectReligionList(0);
        
        DataTable dtFestival = new DataTable();
        dtFestival = objMasMgr.SelectFestivalList(0);
        
        string strReligionId = "";
        string strFestivalId = "";

        if (grAllowanceUpload.Rows.Count > 0)
        {
            foreach (GridViewRow gRow in grAllowanceUpload.Rows)
            {
                strReligionId = "ReligionName='" + gRow.Cells[2].Text.Trim() + "'";
                DataRow[] foundRowsRe;
                foundRowsRe = dtReligion.Select(strReligionId);               

                if (foundRowsRe.Length > 0)
                    gRow.Cells[2].Text = foundRowsRe[0]["ReligionId"].ToString();
                else
                {
                    gRow.Cells[2].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
                    lblMsg.Text = "'" + gRow.Cells[2].Text.Trim() + "' name does not exist in the database. Please correct the name in excel sheet.";
                    return;
                }
                strReligionId = "";
                foundRowsRe = null;


                strFestivalId = "Festival='" + gRow.Cells[3].Text.Trim() + "'";
                DataRow[] foundRowsFe;
                foundRowsFe = dtFestival.Select(strFestivalId);

                if (foundRowsFe.Length > 0)
                    gRow.Cells[3].Text = foundRowsFe[0]["FestivalId"].ToString();
                else
                {
                    gRow.Cells[3].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
                    lblMsg.Text = "'" + gRow.Cells[3].Text.Trim() + "' name does not exist in the database. Please correct the name in excel sheet.";
                    return;
                }
                strFestivalId = "";
                foundRowsFe = null;   
            }
        }
    }
}
