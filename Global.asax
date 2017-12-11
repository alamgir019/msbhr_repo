<%@ Application Language="C#" %>
<%@ Import Namespace="System.Diagnostics" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup LogoPath

    }

    void Application_End(object sender, EventArgs e)
    {
        // Code that runs on application shutdown       

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
        //get reference to the source of the exception chain
        Exception ex = Server.GetLastError().GetBaseException();

        //log the details of the exception and page state to the
        //Windows 2000/XP Event Log
        //EventLog.WriteEntry("PLAN HR Web",
        //  "MESSAGE: " + ex.Message +
        //  "\nSOURCE: " + ex.Source +
        //  //"\nFORM: " + Request.Form.ToString() +
        //  //"\nQUERYSTRING: " + Request.QueryString.ToString() +
        //  "\nTARGETSITE: " + ex.TargetSite +
        //  "\nSTACKTRACE: " + ex.StackTrace,
        //  EventLogEntryType.Error);

        //Insert optional email notification here...
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
        Session["HREMPID"] = "";
        Session["CHANGEPASSWORD"] = "";
        Session["USERID"] = "";
        Session["EMAILID"] = "";
        Session["COUNTRYDIRECTOR"] = "";
        Session["USERNAME"] = "";
        Session["OFFICE"] = "";
        Session["PROGRAM"] = "";
        Session["OFFICEID"] = "";
        Session["PROGRAMID"] = "";
        Session["TEAM"] = "";
        Session["TEAMID"] = "";
        Session["EMPLOYEEID"] = "";
        Session["ISADMIN"] = "";
        Session["ISPAYADMIN"] = "";
        Session["ISSHIFTINCHR"] = "";
        Session["DESIGNATION"] = "";
        Session["LOCATION"] = "";
        Session["EMPID"] = "";
        Session["FILEPATH"] = "";
        Response.Redirect("~/index.aspx?inval=SO");
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        //Response.AppendHeader("Refresh", "100,!~login.aspx");
    }
       
</script>

