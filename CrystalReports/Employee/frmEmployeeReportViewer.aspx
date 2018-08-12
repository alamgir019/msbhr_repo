<%--<script src="/crystalreportviewers13/js/crviewer/crv.js"></script>--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmEmployeeReportViewer.aspx.cs"
    Inherits="frmEmployeeReportViewer" Title="Employee Report Page" %>

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
                <%-- <asp:Label ID="Label1" runat="server" Text="From :" Width="50px" CssClass="textlevel"></asp:Label>
                <asp:TextBox ID="txtFrom" runat="server" Width="50px" Text="1"></asp:TextBox>
                 <asp:Label ID="Label2" runat="server" Text="To :" Width="50px" CssClass="textlevel"></asp:Label>
                <asp:TextBox ID="txtTo" runat="server" Width="50px" Text="1"></asp:TextBox>
                <asp:Button ID="btnPrint" runat="server" Visible="true" onclick="btnPrint_Click" Text="Print" />--%>
                <CR:CrystalReportViewer ID="CRVT" runat="server"  
                EnableDatabaseLogonPrompt="False" OnBeforeRender="CRVT_BeforeRender" 
                OnUnload="CRVT_Unload" AutoDataBind="true" PrintMode="ActiveX" 
                HasDrillUpButton="False"/>
            </fieldset>
        </div>
    </form>
</body>
</html>
