<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KPIReportsViewer.aspx.cs" 
    Inherits="CrystalReports_KPI_KPIReportsViewer" Title="KPI Report Page" %>

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
</body>
</html>


