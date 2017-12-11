<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BankStatementShortReport.aspx.cs"
    Inherits="Payroll_Payroll_BankStatementShortReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bank Statement</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="color: White; background-color: Gray; width: 98%; margin-bottom: 10px;">
            <table width="100%">
                <tr>
                    <td style="width: 50%; text-align: left; height: 21px; color: White;">
                        <asp:LinkButton ID="btnExportToWord" runat="server" ForeColor="white" OnClick="btnExportToWord_Click">Export to Word File</asp:LinkButton>
                    </td>
                    <td style="width: 50%; text-align: right; height: 21px; color: White;">
                        <asp:LinkButton ID="btnExport" runat="server" ForeColor="white" OnClick="btnExport_Click">Export to Excel File</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 98%; margin-bottom: 5px;">
            <table width="100%" style="border-collapse: collapse; border-style: hidden;">
                <tr>
                    <td style="width: 70%; border: none;">
                        <asp:Label ID="lblBank" runat="server" Text="" Font-Size="16px" Font-Names="Arial"></asp:Label>
                    </td>
                    <td style="width: 30%; text-align: right; border-style: hidden;">
                        <asp:Label ID="lblPrintDate" runat="server" Text="" Font-Size="12px" Font-Names="Arial"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 98%;">
            <asp:GridView ID="grBankStatement" runat="server" AutoGenerateColumns="False" ShowHeader="true"
                EmptyDataText="No Record Found" Font-Size="12px" DataKeyNames="EMPID" Font-Names="Arial"
                Width="100%" ShowFooter="true">
                <RowStyle Font-Size="12px" Font-Names="Arial" />
                <HeaderStyle Font-Size="12px" BackColor="lightgray" Font-Names="Arial" />
                <FooterStyle Font-Size="12px" BackColor="gray" Font-Names="Arial" Font-Bold="true" />
                <Columns>
                    <%--<asp:BoundField DataField="" HeaderText="SL#" HeaderStyle-HorizontalAlign="Left">
                        <ItemStyle Width="5%" />
                    </asp:BoundField>--%>
                    <asp:BoundField DataField="FULLNAME" HeaderText="Customer Reference(16)" HeaderStyle-HorizontalAlign="Left">
                        <ItemStyle Width="12%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FULLNAME" HeaderText="Payee Name(22)" HeaderStyle-HorizontalAlign="Left">
                        <ItemStyle Width="12%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="BANKACCNO2" HeaderText="PayeeBankAccNo" HeaderStyle-HorizontalAlign="Left">
                        <ItemStyle Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AccType" HeaderText="Payee Acc Type" HeaderStyle-HorizontalAlign="Left">
                        <ItemStyle Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="BranchCode" HeaderText="Payee Bank Routing" HeaderStyle-HorizontalAlign="Left">
                        <ItemStyle Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NETPAY" HeaderText="Amount" HeaderStyle-HorizontalAlign="right">
                        <ItemStyle Width="8%" HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PaymentDetails1" HeaderText="Payment Details (35)" HeaderStyle-HorizontalAlign="Left">
                        <ItemStyle Width="18%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CURDATE1" HeaderText="Payment Date (DD/MM/YYYY)" HeaderStyle-HorizontalAlign="Left">
                        <ItemStyle Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DebitACNo" HeaderText="Debit A/C No." HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        <div style="width: 98%; margin-top: 80px;">
            <table width="100%">
                <tr>
                    <td style="width: 50%; text-align: left; font-family: Arial; font-size: 13px;">
                        <hr style="width: 60%; border: solid 2px Black;" />
                        Authorized Signature
                    </td>
                    <td style="width: 50%; text-align: right; font-family: Arial; font-size: 13px;">
                        <hr style="width: 60%; border: solid 2px Black;" />
                        Authorized Signature
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
