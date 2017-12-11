using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class EIS_MSBPageSetup : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    MasterTablesManager objMasMgr = new MasterTablesManager();
    DataTable dtDesigation = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpadate.Value = "N";
            gvUploadedFiles.DataSource = null;
            gvUploadedFiles.DataBind();
            lblMsg.Text = "";
            this.EntryMode(false);
            this.OpenRecord();
        }
    }

    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
        {
            btnSave.Text = "Update";
            hfIsUpadate.Value = "Y";
        }
        else
        {
            btnSave.Text = "Save";
            hfIsUpadate.Value = "N";
        }
    }

    private void OpenRecord()
    {
        dtDesigation = objMasMgr.SelectMSBFileList(0);
        gvUploadedFiles.DataSource = dtDesigation;
        gvUploadedFiles.DataBind();
    }

    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        try
        {
            MasterTablesManager MasMgr = new MasterTablesManager();
            if (hfIsUpadate.Value == "N")
                lngID = objDB.GerMaxIDNumber("FileDetails", "FileId");
            else
                lngID = Convert.ToInt32(hfID.Value);

             if (fuFileUploader.PostedFile != null && fuFileUploader.PostedFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(fuFileUploader.PostedFile.FileName);
                    string fileExtension = Path.GetExtension(fuFileUploader.PostedFile.FileName);

                    string fileSavePath = Server.MapPath("~//MSBPolicyFile");
                    if (!Directory.Exists(fileSavePath))
                        Directory.CreateDirectory(fileSavePath);
                    fileSavePath = fileSavePath + "//" + fileName;
                    fuFileUploader.PostedFile.SaveAs(fileSavePath);
                    FileInfo fileInfo = new FileInfo(fileSavePath);
               
                    MasMgr.InsertMSBFILEList(lngID.ToString(), hfIsUpadate.Value, "N", fileName, fileInfo.Length.ToString(), fileExtension, fileSavePath);
                    lblMsg.Text = "File Uploaded Successfully!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMsg.Text = "Error: Please select a file to upload!";
            }
            this.EntryMode(false);
            this.OpenRecord();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected void btnUploadMe_Click(object sender, EventArgs e)
    {
        long lngID = 0;
        try
        {
            MasterTablesManager MasMgr = new MasterTablesManager();
            if (hfIsUpadate.Value == "N")
                lngID = objDB.GerMaxIDNumber("FileDetails", "FileId");
            else
                lngID = Convert.ToInt32(hfID.Value);

            if (fuFileUploader.PostedFile != null && fuFileUploader.PostedFile.ContentLength > 0)
            {
                string fileName = Path.GetFileName(fuFileUploader.PostedFile.FileName);
                string fileExtension = Path.GetExtension(fuFileUploader.PostedFile.FileName);

                string fileSavePath = Server.MapPath("~//MSBPolicyFile");
                if (!Directory.Exists(fileSavePath))
                    Directory.CreateDirectory(fileSavePath);
                fileSavePath = fileSavePath + "//" + fileName;
                fuFileUploader.PostedFile.SaveAs(fileSavePath);
                FileInfo fileInfo = new FileInfo(fileSavePath);

                MasMgr.InsertMSBFILEList(lngID.ToString(), hfIsUpadate.Value, "N", fileName, fileInfo.Length.ToString(), fileExtension, fileSavePath);
                lblMsg.Text = "File Uploaded Successfully!";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMsg.Text = "Error: Please select a file to upload!";
            }

            this.EntryMode(false);
            this.OpenRecord();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    protected void lnkDownloadMe_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            int fileId = Convert.ToInt32(gvUploadedFiles.DataKeys[gvrow.RowIndex].Value.ToString());
            //using (SqlConnection sqlConn = new SqlConnection(strConn))
            //{
            //    using (SqlCommand sqlCmd = new SqlCommand())
            //    {
            //        sqlCmd.CommandText = "SELECT * FROM FileDetails WHERE FileId=@FileId";
            //        sqlCmd.Parameters.AddWithValue("@FileId", fileId);
            //        sqlCmd.Connection = sqlConn;
            //        sqlConn.Open();
            //        SqlDataReader dr = sqlCmd.ExecuteReader();

            dtDesigation = objMasMgr.SelectMSBFileList(fileId);

                   if (dtDesigation.Rows.Count>0)
                    {
                        string fileName = dtDesigation.Rows[0]["FileName"].ToString();
                        string fileLength = dtDesigation.Rows[0]["FileSize"].ToString();
                        string filePath = dtDesigation.Rows[0]["FilePath"].ToString();
                        if (File.Exists(filePath))
                        {
                            Response.Clear();
                            Response.BufferOutput = false;
                            Response.ContentType = "application/octet-stream";
                            Response.AddHeader("Content-Length", fileLength);
                            Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                            Response.TransmitFile(filePath);
                            Response.Flush();
                        }
                        else
                        {
                            lblMsg.Text = "Error: File not found!";
                        }
                    }

            //    }
            //}
        }
        catch
        {
            lblMsg.Text = "Error: Error while downloading file!";
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        //this.SaveData("N");
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
    }
   
   
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfID.Value) == false)
        {
            this.SaveData("Y");
            lblMsg.Text = "Record Deleted Successfully";
        }
        else
        {
            lblMsg.Text = "Select a record first then try to delete.";
        }

        this.EntryMode(false);
    }
    protected void grDesigation_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
