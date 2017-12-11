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
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

public partial class EIS_EmpFileUpload : System.Web.UI.Page
{
    string filePath = "";
    EmpInfoManager objMgr = new EmpInfoManager();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.GetDirectory();
            this.LoadDirectoryList(MyTree);
            TabContainer1.ActiveTabIndex = 0;
            btnCreateParentTab2.Enabled = true;
            btnCreateChildTab2.Enabled = false;
        } 
    }
    protected void GetDirectory()
    {
        DataTable dtDirectory = objMgr.DirectoryList();
        chkListDirectory.DataSource = dtDirectory;
        chkListDirectory.DataTextField = "DirNameLevel";
        chkListDirectory.DataValueField = "DirName";
        chkListDirectory.DataBind();

        grDirectory.DataSource = dtDirectory;
        grDirectory.DataBind();
    }
    protected void LoadDirectoryList(TreeView tv)
    {
        tv.Nodes.Clear();
        tv.Dispose();
        filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["EmpFilePath"];
        System.IO.DirectoryInfo RootDir = new System.IO.DirectoryInfo(filePath);
        TreeNode RootNode = OutputDirectory(RootDir, null);
        tv.Nodes.Add(RootNode);
        tv.ExpandDepth = 1;
    }

    TreeNode OutputDirectory(System.IO.DirectoryInfo directory, TreeNode parentNode)
    {
        if (directory == null) return null;

        TreeNode DirNode = new TreeNode(directory.Name);

        System.IO.DirectoryInfo[] SubDirectories = directory.GetDirectories();

        for (int DirectoryCount = 0; DirectoryCount < SubDirectories.Length; DirectoryCount++)
        {
            OutputDirectory(SubDirectories[DirectoryCount], DirNode);
        }

        //System.IO.FileInfo[] Files = directory.GetFiles();

        //for (int FileCount = 0; FileCount < Files.Length; FileCount++)
        //{
        //    DirNode.ChildNodes.Add(new TreeNode(Files[FileCount].Name));
        //}

        if (parentNode == null)
        {
            return DirNode;
        }

        else
        {

            parentNode.ChildNodes.Add(DirNode);

            return parentNode;
        }
    }



    protected void MyTree_SelectedNodeChanged(object sender, EventArgs e)
    {
        //this.lblPath.Text = MyTree.SelectedNode.Value.ToString();


        TreeNode node = this.MyTree.SelectedNode;
        String pathStr = node.Text;
        string separator = "\\";


        MyTree.PathSeparator = Convert.ToChar(separator);


        while (node.Parent != null)
        {
            lblNodeText.Text = pathStr;
            pathStr = node.Parent.Text + this.MyTree.PathSeparator + pathStr;
            node = node.Parent;
            
        }


        lblPath.Text = pathStr;
        txtSelectedDirectory.Text = lblNodeText.Text;
        string modpath = "";

        if (pathStr.IndexOf('\\') > 0)
        {
            modpath = pathStr.Split(new char[] { '\\' }, 2)[1];
        }


        string chk = Path.GetExtension(pathStr);

        if (chk.Equals(".pdf") || chk.Equals(".PDF"))
        {
            ReadPdfFile(modpath);

            //pdfFrame.Attributes["src"] = @"E:\work\PLAN BANGLADESH\" + pathStr;
        }
        TabContainer1.ActiveTabIndex = 0;
    }




    private void ReadPdfFile(string pdfFile)
    {
        filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["EmpFilePath"];

        string path = filePath +"\\"+ pdfFile; //@"E:\work\PLAN BANGLADESH\" + pdfFile;       
        WebClient client = new WebClient();
        Byte[] buffer = client.DownloadData(path);

        if (buffer != null)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", buffer.Length.ToString());
            Response.BinaryWrite(buffer);
        }


        //path = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.LastIndexOf("/") + 1) + @"Published/" + path;
        //string popupScript = "<script language='javascript'>" +
        //                       " window.open('" + path + " ','_blank')" +
        //                        "</script>";
        //Page.RegisterStartupScript("PopupScript", popupScript);

        //window.open('<%=strPDFfilePath%>',"Pdf_for m","height=450px,width=700px")>

        //}

    }

  
    protected void btnFind_Click1(object sender, EventArgs e)
    {
       this.FindByText(MyTree);
       TabContainer1.ActiveTabIndex = 0;
    }



    protected void UploadButton_Click(object sender, EventArgs e)
    {
        string filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["EmpFilePath"];
        System.IO.DirectoryInfo RootDir = new System.IO.DirectoryInfo(filePath);
        string extn = "";
        string fullPath = "";        

        if (FileUploadControl.HasFile)
        {            
            try
            {
                fullPath = filePath + @"\" + lblNodeText.Text + @"\";


                string filename = Path.GetFileName(FileUploadControl.FileName);
                extn = Path.GetExtension(filename);
                filename = Path.GetFileNameWithoutExtension(filename);

                Regex rgx = new Regex(".pdf");
                filename = rgx.Replace(filename, "");
                

                //FileUploadControl.SaveAs(fullPath + filename + "_" + txtFileName.Text + extn);
                //if (txtRename.Text == "")
                //{
                //    lblMsg.Text = "Rename file please";
                //    return;
                //}
                if (string.IsNullOrEmpty(txtRename.Text) == false)
                {
                    if(ddlRenameType.SelectedValue.Trim()=="0")
                        FileUploadControl.SaveAs(fullPath + txtRename.Text + "_" + filename + extn);
                    else
                        FileUploadControl.SaveAs(fullPath + filename + "_" + txtRename.Text + extn);
                }
                else
                    FileUploadControl.SaveAs(fullPath + filename + extn);

                lblMsg.Text = "File uploaded successfully.";
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
            }
        }
        TabContainer1.ActiveTabIndex = 0;
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {        
            //filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["EmpFilePath"];
            //System.IO.DirectoryInfo RootDir = new System.IO.DirectoryInfo(filePath);
            //TreeNode RootNode = OutputDirectory(RootDir, null);

            //MyTree.Nodes.Add(RootNode);        
    }

    protected void btnLoadDirectoryTab2_Click(object sender, EventArgs e)
    {
        this.LoadDirectoryList(MyTreeTab2);
        MyTreeTab2.ExpandDepth = 1;
        TabContainer1.ActiveTabIndex = 1;
    }

    protected void CreateDirectory(bool isParent)
    {
        string pathToCreate="";
        string childPathToCreate = "";
        string strDirectories = "";
        lblMsg.Text = "";
        if (string.IsNullOrEmpty(txtDirNameTab2.Text.Trim()) == true)
        {
            lblMsg.Text = "No dirctory is created.Directory mame cannot be empty.";
            return;
        }

        filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["EmpFilePath"];
        pathToCreate = filePath + @"\" + txtDirNameTab2.Text.Trim();

        if(isParent==true)
        {
            if (Directory.Exists(pathToCreate) == false)
            {
                Directory.CreateDirectory(pathToCreate);
                lblMsg.Text = "Parent directory created. ";
            }
            else
            {
                lblMsg.Text = "Cannot create parent directory. Directory already present.";
            }
        }
        for (int i = 0; i < chkListDirectory.Items.Count; i++)
        {
            if (chkListDirectory.Items[i].Selected == true)
            {
                childPathToCreate = pathToCreate + @"\" + chkListDirectory.Items[i].Value.Trim();
                if (Directory.Exists(childPathToCreate) == false)
                {
                    Directory.CreateDirectory(childPathToCreate);

                }
                else
                {
                    strDirectories = strDirectories + "," + chkListDirectory.Items[i].Value.Trim();
                }
            }
        }
           
        
        if (strDirectories != "")
        {
            lblMsg.Text = lblMsg.Text + "Cannot create " + strDirectories + " child directories. These are aleady exist.";
        }
        else
        {
            lblMsg.Text = lblMsg.Text + "Child directory created.";
        }
        this.LoadDirectoryList(MyTreeTab2);
        this.LoadDirectoryList(MyTree);
        TabContainer1.ActiveTabIndex = 1;
    }

    protected void btnCreateParentTab2_Click(object sender, EventArgs e)
    {
        this.CreateDirectory(true);
    }
    protected void MyTreeTab2_SelectedNodeChanged(object sender, EventArgs e)
    {
        TreeNode node = this.MyTreeTab2.SelectedNode;
        String pathStr = node.Text;
        string separator = "\\";
        string strNodeText = "";

        MyTreeTab2.PathSeparator = Convert.ToChar(separator);
        while (node.Parent != null)
        {
            strNodeText = pathStr;
            pathStr = node.Parent.Text + this.MyTreeTab2.PathSeparator + pathStr;
            node = node.Parent;
        }
        txtDirNameTab2.Text = strNodeText;
        TabContainer1.ActiveTabIndex = 1;
        if (string.IsNullOrEmpty(txtDirNameTab2.Text) == true)
        {
            btnCreateParentTab2.Enabled = true;
            btnCreateChildTab2.Enabled = false;
        }
        else
        {
            btnCreateParentTab2.Enabled = false;
            btnCreateChildTab2.Enabled = true;
        }
    }
    protected void btnCreateChildTab2_Click(object sender, EventArgs e)
    {
        this.CreateDirectory(false);
    }
    protected void btnRefreshTab2_Click(object sender, EventArgs e)
    {
        txtDirNameTab2.Text = "";

        for (int i = 0; i < chkListDirectory.Items.Count; i++)
        {
            chkListDirectory.Items[i].Selected = false;
        }
        btnCreateParentTab2.Enabled = true;
        btnCreateChildTab2.Enabled = false;
        TabContainer1.ActiveTabIndex = 1;
    }

    protected void grDirectory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        // Get the selected index and the command name
        int _selectedIndex = int.Parse(e.CommandArgument.ToString());
        string _commandName = e.CommandName;
        _gridView.SelectedIndex = _selectedIndex;
        switch (_commandName)
        {
            case ("DoubleClick"):
                objMgr.DeleteDirectoryList(grDirectory.DataKeys[_gridView.SelectedIndex].Values[0].ToString());
                lblMsg.Text = "Directory deleted successfully";
                this.GetDirectory();
                TabContainer1.ActiveTabIndex = 2;
                break;
        }
    }
    protected void btnAddTab2_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtNewDirNameTab2.Text) == true)
        {
            lblMsg.Text = "Please enter a directory name";
            return;
        }
        if (string.IsNullOrEmpty(txtNewDirLevelTab2.Text) == true)
        {
            lblMsg.Text = "Please enter a directory level";
            return;
        }
        objMgr.InsertDirectoryList(txtNewDirNameTab2.Text.Trim(),
            txtNewDirLevelTab2.Text.Trim(),
            Session["USERID"].ToString().Trim(),
            Common.SetDateTime(DateTime.Now.ToString()));
        txtNewDirNameTab2.Text = "";
        txtNewDirLevelTab2.Text = "";
        lblMsg.Text = "Directory created successfully";
        this.GetDirectory();
        TabContainer1.ActiveTabIndex = 2;
    }

    #region Find By Text

    /// <summary>
    /// Searching for nodes by text requires a special function
    /// this function recursively scans the treeview and
    /// marks matching items.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //private void btnNodeTextSearch_Click(object sender, EventArgs e)
    //{
    //    ClearBackColor();
    //    FindByText();
    //}

    private void FindByText(TreeView tv)
    {
        TreeNodeCollection nodes = tv.Nodes;
        foreach (TreeNode n in nodes)
        {
            FindRecursive(n);
        }
    }

    private void FindRecursive(TreeNode treeNode)
    {
        foreach (TreeNode tn in treeNode.ChildNodes)
        {
            // if the text properties match, color the item
            if (tn.Text == this.txtNode.Text.Trim())
            {
                tn.Expand();
                tn.Select();
                break;
            }
            //tn.BackColor = Color.Yellow;

            FindRecursive(tn);
        }
    }

    #endregion
    protected void btnFindTab2_Click(object sender, EventArgs e)
    {
        this.FindByText(MyTreeTab2);
        TabContainer1.ActiveTabIndex = 1;
    }
}
