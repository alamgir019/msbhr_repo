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

public class SingleExt : DirectoryInfoEx
{
	/// <summary>
	/// Summary description for SingleExt.
	/// </summary>
    //public class SingleExt : DirectoryInfoEx
    //{
		public SingleExt(string path) : base (path)
		{
			//
			// TODO: Add constructor logic here
			//
			
		}

		public override FileInfo[] getFiles(string ext)
		{
			/* Pointer to our directory */
			if (dirPath == null)
				return null;

			DirectoryInfo dinfo = new DirectoryInfo(dirPath);
			/* get files from the directory as a files collection */
			FileInfo[] finfo = dinfo.GetFiles(ext);

			return finfo;
		}
    //}
}
