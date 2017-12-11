<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BankStatementDetailsReport.aspx.cs"
    Inherits="Payroll_Payroll_BankStatementDetailsReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bank Statement</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 98%; margin-bottom: 10px;">
            <table width="100%">
                <tr>
                    <td style="width: 50%; text-align: left;">
                        <asp:LinkButton ID="btnExportToWord" runat="server" OnClick="btnExportToWord_Click">Export to Word File</asp:LinkButton>
                    </td>
                    <td style="width: 50%; text-align: right;">
                        <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click">Export to Excel File</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 98%;">
            <asp:GridView ID="grBankStatement" runat="server" AutoGenerateColumns="False" ShowHeader="true"
                EmptyDataText="No Record Found" Font-Size="12px" DataKeyNames="EMPID" Width="100%"
                BorderColor="#DEDFDE" BorderStyle="solid" BorderWidth="1px" ForeColor="Black">
                <RowStyle Font-Size="12px" BorderColor="#DEDFDE" BorderStyle="solid" BorderWidth="1px" />
                <Columns>
                    <asp:BoundField DataField="" HeaderText="SL" HeaderStyle-BorderWidth="1px" HeaderStyle-BorderStyle="solid"
                        HeaderStyle-BorderColor="dimgray" HeaderStyle-HorizontalAlign="Left">
                        <ItemStyle Width="5%" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray" />
                    </asp:BoundField>
                    <asp:BoundField DataField="BANKACCNO1" HeaderText="Account No" HeaderStyle-BorderWidth="1px"
                        HeaderStyle-BorderStyle="solid" HeaderStyle-BorderColor="dimgray" HeaderStyle-HorizontalAlign="Left">
                        <ItemStyle Width="10%" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FULLNAME" HeaderText="Account Name" HeaderStyle-BorderWidth="1px"
                        HeaderStyle-BorderStyle="solid" HeaderStyle-BorderColor="dimgray" HeaderStyle-HorizontalAlign="Left">
                        <ItemStyle Width="20%" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NETPAY" HeaderText="Amount(Taka)" HeaderStyle-HorizontalAlign="right"
                        HeaderStyle-BorderWidth="1px" HeaderStyle-BorderStyle="solid" HeaderStyle-BorderColor="dimgray">
                        <ItemStyle Width="10%" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray"
                            HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PaymentDetails1" HeaderText="Payment Details" HeaderStyle-BorderWidth="1px"
                        HeaderStyle-BorderStyle="solid" HeaderStyle-BorderColor="dimgray" HeaderStyle-HorizontalAlign="Left">
                        <ItemStyle Width="20%" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray" />
                    </asp:BoundField>
                    <%--<asp:BoundField DataField="BANKACCNO" HeaderText="Payment Details-2" HeaderStyle-BorderWidth="1px"
                        HeaderStyle-BorderStyle="solid" HeaderStyle-BorderColor="dimgray" HeaderStyle-HorizontalAlign="Left">
                        <ItemStyle Width="10%" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray" />
                    </asp:BoundField>--%>
                    <asp:BoundField DataField="PersonalEmail" HeaderText="Email Address" HeaderStyle-BorderWidth="1px"
                        HeaderStyle-BorderStyle="solid" HeaderStyle-BorderColor="dimgray" HeaderStyle-HorizontalAlign="Left">
                        <ItemStyle Width="25%" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CURDATE1" HeaderText="PAYMENT DATE (DD/MM/YYYY)" HeaderStyle-BorderWidth="1px"
                        HeaderStyle-BorderStyle="solid" HeaderStyle-BorderColor="dimgray" HeaderStyle-HorizontalAlign="Left">
                        <ItemStyle Width="10%" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
