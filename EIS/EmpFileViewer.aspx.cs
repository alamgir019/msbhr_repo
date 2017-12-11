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

public partial class EIS_EmpFileViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // string strParams = Request.QueryString["params"];
        WebClient client = new WebClient();
        Byte[] buffer = client.DownloadData(Session["FILEPATH"].ToString());

        if (buffer != null)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", buffer.Length.ToString());
            Response.BinaryWrite(buffer);
        }
    }
}
