using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using Microsoft.Office.Interop.Word;

public partial class EIS_MSBPageView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            DirectoryInfo rootInfo = new DirectoryInfo(Server.MapPath("~/MSBPolicyFile/"));
            //TreeView1.Nodes[0].Value = Server.MapPath("~/MSBPolicyFile");
            this.PopulateTreeView(rootInfo, null);
        }

    }
    private void PopulateTreeView(DirectoryInfo dirInfo, TreeNode treeNode)
    {
        foreach (DirectoryInfo directory in dirInfo.GetDirectories())
        {
            TreeNode directoryNode = new TreeNode
            {
                Text = directory.Name,
                Value = directory.FullName
            };

            if (treeNode == null)
            {
                //If Root Node, add to TreeView.
                TreeView1.Nodes.Add(directoryNode);
            }
            else
            {
                //If Child Node, add to Parent Node.
                treeNode.ChildNodes.Add(directoryNode);
            }

            //Get all files in the Directory.
            foreach (FileInfo file in directory.GetFiles())
            {
                //Add each file as Child Node.
                TreeNode fileNode = new TreeNode
                {
                    SelectAction = TreeNodeSelectAction.Select,
                    Text = file.Name,
                    Value = file.FullName,
                    PopulateOnDemand = true,
                    Target = "_blank",
                    NavigateUrl = (new Uri(Server.MapPath("~/"))).MakeRelativeUri(new Uri(file.FullName)).ToString()
                };
                directoryNode.ChildNodes.Add(fileNode);
            }
            PopulateTreeView(directory, directoryNode);
        }
    }
  

    protected void TreeView1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        if (IsCallback)
        {
            if (e.Node.ChildNodes.Count == 0)
            {
                DirectoryInfo directory = null;
                directory = new DirectoryInfo(e.Node.Value);

                foreach (DirectoryInfo subtree in directory.GetDirectories())
                {
                    TreeNode subNode = new TreeNode(subtree.Name);
                    subNode.Value = subtree.FullName;
                    try
                    {
                        if (subtree.GetDirectories().Length > 0 | subtree.GetFiles().Length > 0)
                        {
                            subNode.SelectAction = TreeNodeSelectAction.SelectExpand;
                            subNode.PopulateOnDemand = true;
                            subNode.NavigateUrl = "#";
                        }
                    }
                    catch
                    {

                    }
                    e.Node.ChildNodes.Add(subNode);
                }
                foreach (FileInfo fi in directory.GetFiles())
                {
                    TreeNode subNode = new TreeNode(fi.Name);
                    e.Node.ChildNodes.Add(subNode);
                    subNode.NavigateUrl = "~/MSBPolicyFile/" + fi.Name.ToString();
                }
            }
        }
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {

    }
}