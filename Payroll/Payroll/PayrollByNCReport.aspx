<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayrollByNCReport.aspx.cs"
    Inherits="Payroll_Payroll_PayrollByNCReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payroll By NC Report</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-size: 20px; font-family: Arial; font-weight: bold; width: 80%;">
            Marie Stopes
        </div>
        <br />
        <div style="font-size: 16px; font-family: Arial; font-weight: bold; width: 80%;">
            <asp:Label ID="lblBank" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblSubHead" runat="server" Text=""></asp:Label>
        </div>
        <br />
        <asp:GridView ID="grReport" runat="server" EmptyDataText="No Record Found" Width="80%"
            AutoGenerateColumns="false" DataKeyNames="" Font-Size="12px" Font-Names="Arial"
            ShowFooter="true">
            <HeaderStyle Font-Bold="True" BackColor="LightGray" Font-Size="12px" Font-Names="Arial">
            </HeaderStyle>
            <FooterStyle Font-Bold="True" BackColor="LightGray" Font-Size="12px" Font-Names="Arial">
            </FooterStyle>
            <RowStyle Font-Size="11px" Font-Names="Arial" />
            <Columns>
                <asp:BoundField DataField="" HeaderText="SL#" HeaderStyle-HorizontalAlign="center">
                    <ItemStyle Width="5%" HorizontalAlign="center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="ACCLINE" HeaderText="Account Line" HeaderStyle-HorizontalAlign="Left">
                    <ItemStyle Width="35%" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="7100" HeaderText="NC 7100" HeaderStyle-HorizontalAlign="Right">
                    <ItemStyle Width="20%" HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="7200" HeaderText="NC 7200" HeaderStyle-HorizontalAlign="Right">
                    <ItemStyle Width="20%" HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="" HeaderText="Total" HeaderStyle-HorizontalAlign="Right">
                    <ItemStyle Width="20%" HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
