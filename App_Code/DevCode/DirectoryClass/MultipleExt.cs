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

public class MultipleExt : DirectoryInfoEx
{
	/// <summary>
	/// Summary description for MultipleExt.
	/// </summary>
    //public class MultipleExt : DirectoryInfoEx
    //{
		public MultipleExt(string path) : base (path)
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
			string[] exts = ext.Split(',');

			FileInfo[][] finfo = new FileInfo[exts.Length][];
			int i = 0;

			foreach(string e in exts)
			{
				/* get files from the directory as a files collection */
				finfo[i++] = dinfo.GetFiles(e);
			}

			int tlength = 0;

			for (i = 0 ; i < finfo.Length ; i++)
			{
				tlength += finfo[i].Length;
			}

			FileInfo[] res = new FileInfo[tlength];
			int j = 0;
			int rindex = 0;

			for (i = 0 ; i < finfo.Length ; i++)
			{
				for (j = 0 ; j < finfo[i].Length ; j++)
				{
					res[rindex++] = finfo[i][j];
				}
			}

			return res;
		}
    //}
}
