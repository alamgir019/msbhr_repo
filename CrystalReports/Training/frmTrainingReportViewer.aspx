<script src="/crystalreportviewers13/js/crviewer/crv.js"></script>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmTrainingReportViewer.aspx.cs"
    Inherits="frmTrainingReportViewer" Title="Training Report Page" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Report Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <fieldset style="text-align: left; background-color: White">
                <CR:CrystalReportViewer ID="CRVT" runat="server" EnableDatabaseLogonPrompt="False"
                    OnBeforeRender="CRVT_BeforeRender" OnUnload="CRVT_Unload" 
                    BorderColor="White" BorderWidth="0px" GroupTreeStyle-BackColor="White" 
                    GroupTreeStyle-BorderColor="White" GroupTreeStyle-BorderStyle="None" 
                    Height="50px" ToolbarStyle-BackColor="White" 
                    ToolbarStyle-BorderColor="White" Width="350px" 
                    HasToggleGroupTreeButton="False" ToolPanelView="None" />
            </fieldset>
        </div>
    </form>

     <%-- Add Reference to jQuery at Google CDN --%>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
 
    <%-- Add Reference to spin.js (an animated spinner) --%>
    <script src="http://fgnass.github.io/spin.js/spin.min.js"></script>
 
    <script type="text/javascript">
     
        var wcppPingDelay_ms = 10000; 
 
        function wcppDetectOnSuccess(){
            <%-- WCPP utility is installed at the client side
                 redirect to WebClientPrint sample page --%>
             
            <%-- get WCPP version --%>
            var wcppVer = arguments[0];
            if(wcppVer.substring(0, 1) == "2")
                window.location.href = "PrintCrystalReports.aspx";
            else //force to install WCPP v2.0
                wcppDetectOnFailure();
        }
 
        function wcppDetectOnFailure() {
            <%-- It seems WCPP is not installed at the client side
                 ask the user to install it --%>
            $('#msgInProgress').hide();
            $('#msgInstallWCPP').show();                
        }
 
        $(document).ready(function () {
            <%-- Create the Spinner with options (http://fgnass.github.io/spin.js/) --%>
            var spinner = new Spinner({
          lines: 12, 
          length: 7, 
          width: 3, 
          radius: 10, 
          color: '#336699', 
          speed: 1, 
          trail: 60               
            }).spin($('#mySpinner')[0]); 
        });
 
    </script>
</body>
</html>
