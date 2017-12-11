<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StaffSalaryAndITReport.aspx.cs"
    Inherits="Payroll_Payroll_StaffSalaryAndITReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Salary Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-size: 20px; font-family: Arial; font-weight: bold; width: 100%;">
        Marie Stopes
    </div>
    <br />
    <div style="font-size: 16px; font-family: Arial; font-weight: bold; width: 100%;">
        <asp:Label ID="lblSubHead" runat="server" Text=""></asp:Label>
    </div>
    <asp:GridView ID="grReport" runat="server" EmptyDataText="No Record Found" Width="99.99%"
        AutoGenerateColumns="true" DataKeyNames="" Font-Size="12px" Font-Names="Arial"
        ShowFooter="true">
        <HeaderStyle Font-Bold="True" HorizontalAlign="center" BackColor="LightGray" Font-Size="12px"
            Font-Names="Arial"></HeaderStyle>
        <FooterStyle Font-Bold="True" HorizontalAlign="center" BackColor="LightGray" Font-Size="12px"
            Font-Names="Arial"></FooterStyle>
    </asp:GridView>
    </form>
</body>
</html>
