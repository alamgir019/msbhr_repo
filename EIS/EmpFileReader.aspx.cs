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
using System.Text;

public partial class EIS_EmpFileReader : System.Web.UI.Page
{
    MasterTablesManager tblObj = new MasterTablesManager();

    string filePath = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            this.LoadDirectory();
        } 
    }

    protected void LoadDirectory()
    {
        MyTree.Nodes.Clear();
        MyTree.Dispose();
        filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["EmpFilePath"];
        System.IO.DirectoryInfo RootDir = new System.IO.DirectoryInfo(filePath);
        TreeNode RootNode = OutputDirectory(RootDir, null);
        MyTree.Nodes.Add(RootNode);
        MyTree.ExpandDepth = 1;
    }

    TreeNode OutputDirectory(System.IO.DirectoryInfo directory, TreeNode parentNode)
    {
        if (directory == null) return null;

        TreeNode DirNode = new TreeNode(directory.Name, directory.Name);

        System.IO.DirectoryInfo[] SubDirectories = directory.GetDirectories();

        for (int DirectoryCount = 0; DirectoryCount < SubDirectories.Length; DirectoryCount++)
        {
            OutputDirectory(SubDirectories[DirectoryCount], DirNode);
        }

        System.IO.FileInfo[] Files = directory.GetFiles();

        for (int FileCount = 0; FileCount < Files.Length; FileCount++)
        {
            DirNode.ChildNodes.Add(new TreeNode(Files[FileCount].Name, Files[FileCount].Name));
        }

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

        lblMsg.Text = "";
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
        string modpath = "";

        if (pathStr.IndexOf('\\') > 0)
        {
            modpath = pathStr.Split(new char[] { '\\' }, 2)[1];
        }


        string chk = Path.GetExtension(pathStr);

        if (chk.Equals(".pdf") || chk.Equals(".PDF") || chk.Equals(".docx"))
        {
            ReadPdfFile(modpath);
            string[] result = modpath.Split(new string[] { "\\" }, StringSplitOptions.None);
            
            string FileName = result[result.Length-1].ToString();
            InsertViewLogInfo(FileName);

            //pdfFrame.Attributes["src"] = @"E:\work\PLAN BANGLADESH\" + pathStr;
        }

    }

    private void InsertViewLogInfo(string FileName)
    {
        string ViewID = Common.getMaxId("MSBPageViewDtls", "ViewID");
        tblObj.InsertViewLogInfo(ViewID,Session["USERID"].ToString(),FileName, Common.SetDateTime(DateTime.Now.ToString()));
    }





    private void ReadPdfFile(string pdfFile)
    {
        Session["FILEPATH"] = "";
        filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["EmpFilePath"];

        string path = filePath +"\\"+ pdfFile; //@"E:\work\PLAN BANGLADESH\" + pdfFile;    

        Session["FILEPATH"] = path;
        ///File View to a new page
        StringBuilder sb = new StringBuilder();
        string strURL = "EmpFileViewer.aspx";
        sb.Append("<script>");
        sb.Append("window.open('" + strURL + "', '', 'directories=0,titlebar=0,toolbar=0,location=0,status=0,menubar=0,fullscreen=no,scrollbars=no,resizable=yes');");//

        //"directories=0,titlebar=0,toolbar=0,location=0,status=0,menubar=0,scrollbars=no,resizable=no,width=400,height=350"
        //sb.Append("window.open('" + strURL + "', '', '');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                 sb.ToString(), false);
        ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());

    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        
        

    }
    protected void btnFind_Click1(object sender, EventArgs e)
    {
        //lblMsg.Text = "";
        //string nodeToSelect = txtNode.Text.Trim();        
        //TreeNode node = MyTree.FindNode(Server.HtmlEncode(txtNode.Text.Trim()));

        //if (node != null)
        //{
        //    node.Expand();
        //    node.Select();
        //    txtNode.Text = "";
        //}
        //else
        //{
        //    txtNode.Text = "Node not found.";
        //}

        //ClearBackColor();

        //try
        //{
        //    TreeNode[] tn =
        //    MyTree.FindNode(txtNode.Text, true);
        //    for (int i = 0; i < tn.Length; i++)
        //    {
        //        MyTree.SelectedNode = tn[i];
        //        MyTree.SelectedNode.BackColor = System.Drawing.Color.Yellow;
        //    }
        //}
        //catch { }
       
        FindByText();
    }



    protected void btnRefresh_Click(object sender, EventArgs e)
    {        
            //filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["EmpFilePath"];
            //System.IO.DirectoryInfo RootDir = new System.IO.DirectoryInfo(filePath);
            //TreeNode RootNode = OutputDirectory(RootDir, null);

            //MyTree.Nodes.Add(RootNode);        
    }
    protected void btnFileDelete_Click(object sender, EventArgs e)
    {
        string fullFilePath = "";
        if (lblNodeText.Text != "" && txtFileName.Text != "")
        {
            fullFilePath = System.Web.Configuration.WebConfigurationManager.AppSettings["EmpFilePath"] + @"\" + lblNodeText.Text + @"\" + txtFileName.Text;

            FileInfo File = new FileInfo(fullFilePath);
            if (File.Exists == true)
            {
                File.Delete();
                lblMsg.Text = "File deleted successfully";
            }
            else
            {
                lblMsg.Text = "File not found";
            }
            
            this.LoadDirectory();
            MyTree.ExpandDepth = 2;
            txtFileName.Text = "";
            lblNodeText.Text = "";
        }
        else
            lblMsg.Text = "Please select node and file to be deleted";
        
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

    private void FindByText()
    {
        TreeNodeCollection nodes = MyTree.Nodes;
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
}
