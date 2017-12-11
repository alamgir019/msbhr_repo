<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PFLoanLedgerViewer.aspx.cs"
    Inherits="CrystalReports_Payroll_PFLoanLedgerViewer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PF Loan Ledger</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="background-color: Gray; text-align: center; font-weight: bold;">
            <asp:Button ID="btnExport" runat="server" Text="Export to Excel"
                Height="30px" Font-Bold="true" OnClick="btnExport_Click" />
            <asp:GridView ID="grExport" runat="server" AutoGenerateColumns="true" ShowHeader="true"
                Visible="false">
            </asp:GridView>
        </div>
        <div>
            <CR:CrystalReportViewer ID="CRV" runat="server" EnableDatabaseLogonPrompt="False"
                OnBeforeRender="CRV_BeforeRender" OnUnload="CRV_Unload" />
        </div>
    </form>
</body>
</html>
