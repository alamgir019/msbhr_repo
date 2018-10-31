using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

public partial class EIS_EmpDocUpload : System.Web.UI.Page
{
    UploadToolManager utm = new UploadToolManager();
    DBConnector objDB = new DBConnector();
    string filePath = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // BindGridviewData();
            txtEmpID.Text = Session["EMPID"].ToString().ToUpper().Trim();
            this.EmpUploadedDocument();
        }
    }

    private void EmpUploadedDocument()
    {
        DataSet ds = new DataSet();
        DataTable dtEmpInfo = new DataTable();
        if (Session["ISADMIN"].ToString() == "N")
        {
            txtEmpID.ReadOnly = true;
            ds = utm.SelectEmpDoc(txtEmpID.Text.Trim());
            gvDetails.DataSource = ds.Tables[0];
            gvDetails.DataBind();
            dtEmpInfo = ds.Tables[1];
        }
        else
        {
            ds = utm.SelectEmpDoc(txtEmpID.Text.Trim());
            gvDetails.DataSource = ds.Tables[0];
            gvDetails.DataBind();
            dtEmpInfo = ds.Tables[1];
        }
        if (dtEmpInfo.Rows.Count == 0)
        {
            lblMsg.Text = "Invalid Employee No.";
            return;
        }
        else
        {
            lblMsg.Text = "";
            foreach (DataRow dRow in dtEmpInfo.Rows)
            {
                lblName.Text = dRow["FullName"].ToString();
                lblDesignation.Text = dRow["DesigName"].ToString().Trim();
                lblSector.Text = dRow["ProjectName"].ToString().Trim();
                lblDept.Text = dRow["DeptName"].ToString().Trim();
            }
        }
    }
    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (txtEmpID.Text.Trim() == "")
            return;

        this.EmpUploadedDocument();       
    }

    // Bind Gridview Data
    private void BindGridviewData( )
    {
        DataSet ds = utm.SelectEmpDoc(txtEmpID.Text.Trim());
        gvDetails.DataSource = ds.Tables[0];
        gvDetails.DataBind();

    }
    // Save files to Folder and files path in database
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["EmpDocPath"];
       
        //DirectoryInfo info = new DirectoryInfo(Server.MapPath(string.Format("~/EmpDocument/") + txtEmpID.Text.Trim())); //Creating SubFolder inside the Folder with Name(which is provide by User).

        DirectoryInfo info = new DirectoryInfo(Server.MapPath(filePath + txtEmpID.Text.Trim()));
        string directoryPath = info+"/".ToString();
        if (!info.Exists)  //Checking If Not Exist.
        {
            info.Create();
        }
            HttpFileCollection hfc = Request.Files;
            for (int i = 0; i < hfc.Count; i++) //Checking how many files in File Upload control.
            {
                HttpPostedFile hpf = hfc[i];
                if (hpf.ContentLength > 0)
                {
                    hpf.SaveAs(directoryPath + Path.GetFileName(hpf.FileName)); //Uploading Multiple Files into newly created Folder (One by One).
                    this.SaveData(txtEmpID.Text.Trim(), Path.GetFileName(hpf.FileName).ToString(), filePath +txtEmpID.Text.Trim() + "/".ToString() + Path.GetFileName(hpf.FileName).ToString());
                }
            }
            imgBtnSearch_Click(null,null);
        //}
        //else
        //{
        //   // ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('This Folder already Created.');", true);
        //}

        //string filename = Path.GetFileName(fileUpload1.PostedFile.FileName);
        //fileUpload1.SaveAs(Server.MapPath("Files/" + filename));


        //con.Open();
        //SqlCommand cmd = new SqlCommand("insert into FilesTable(FileName,FilePath) values(@Name,@Path)", con);
        //cmd.Parameters.AddWithValue("@Name", filename);
        //cmd.Parameters.AddWithValue("@Path", "Files/" + filename);
        //cmd.ExecuteNonQuery();
        //con.Close();
        //BindGridviewData();
    }

    private void SaveData(string sEmpID,string sfileName,string sFilePath)
    {
        long lngID = 0;
        try
        {
            lngID = objDB.GerMaxIDNumber("EmpDocInfo", "ID");
            
            clsCommonSetup objCommonSetup = new clsCommonSetup(lngID.ToString(), "",  "Y", "N",
                                            Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "N","N");

            utm.InsertEmpDocument(objCommonSetup, sEmpID, sfileName, sFilePath, "N");

        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    // This button click event is used to download files from gridview
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = sender as LinkButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        string filePath = gvDetails.DataKeys[gvrow.RowIndex].Value.ToString();
        Response.ContentType = "image/jpg";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
        Response.TransmitFile(Server.MapPath(filePath));
        Response.End();
    }


    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = sender as LinkButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        string filePath = gvDetails.DataKeys[gvrow.RowIndex].Value.ToString();
        string strPhysicalFolder = Server.MapPath(filePath);
        string strFileFullPath = strPhysicalFolder;

        if (System.IO.File.Exists(strFileFullPath))
        {
            System.IO.File.Delete(strFileFullPath);
            utm.DeleteDocument(filePath);
        }

        this.EmpUploadedDocument();
        //Response.ContentType = "image/jpg";
        //Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
        //Response.TransmitFile(Server.MapPath(filePath));
        //Response.End();
    }

    protected void gvDetails_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}