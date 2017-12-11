<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TimeSheetReportViewer.aspx.cs"
    Inherits="TimeSheetReportViewer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="cr" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Time Sheet Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cr:CrystalReportViewer ID="CRVA" runat="server" EnableDatabaseLogonPrompt="False"
            OnBeforeRender="CRVA_BeforeRender" OnUnload="CRVA_Unload" 
            HasToggleGroupTreeButton="False" ToolPanelView="None" />
    </div>
    </form>
</body>
</html>
