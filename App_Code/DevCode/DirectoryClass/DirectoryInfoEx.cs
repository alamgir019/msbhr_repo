using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public abstract class DirectoryInfoEx
{
	/// <summary>
	/// Summary description for DirectoryInfoEx.
	/// </summary>
    //public abstract class DirectoryInfoEx
    //{
		protected string dirPath = null;

		public DirectoryInfoEx(string path)
		{
			//
			// TODO: Add constructor logic here
			//
			dirPath = path;
		}

    public abstract FileInfo[] getFiles(string ext);
    //}
}
