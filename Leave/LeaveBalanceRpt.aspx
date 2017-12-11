<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeaveBalanceRpt.aspx.cs"
    Inherits="Leave_LeaveBalanceRpt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employees Leave Balance</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-family: Arial; font-size: 20px;">
            Marie Stopes
        </div>
        <div style="font-family: Arial; font-size: 16px;">
            Employees Leave Balance
        </div>
        <div>
            <div style="width: 95%; float: left; margin-bottom: 5px; border-bottom: solid 1px #FF9933">
                <table>
                    <tr>
                        <td>
                            <span style="font-family: Tahoma; font-size: 14px;">Date Till:</span>
                        </td>
                        <td>
                            <asp:Label ID="lblFrom" runat="server" Text="" Font-Names="Tahoma" Font-Size="14px"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top: 10px; width: 100%; float: left;">
                <asp:GridView ID="grLeaveProfile" runat="server" AutoGenerateColumns="False" DataKeyNames=""
                    EmptyDataText="No Record Found" Font-Size="12px" Width="95%">
                    <HeaderStyle BackColor="#3366CC" Font-Bold="True" ForeColor="whitesmoke" Font-Names="Tahoma"
                        Font-Size="12px" />
                    <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                    <AlternatingRowStyle BackColor="#EFF3FB" />
                    <RowStyle Font-Size="12px" />
                    <Columns>
                        <asp:BoundField HeaderText="SL No.">
                            <ItemStyle CssClass="ItemStylecss" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EmpId" HeaderText="Emp ID">
                            <ItemStyle Width="5%" CssClass="ItemStylecss"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FullName" HeaderText="Employee Name">
                            <ItemStyle Width="20%" CssClass="ItemStylecss"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PostingDivName" HeaderText="Posting Division Name">
                            <ItemStyle Width="20%" CssClass="ItemStylecss"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="DeptName" HeaderText="Department">
                            <ItemStyle Width="20%" CssClass="ItemStylecss"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Earned Leave">
                            <ItemStyle Width="8%" CssClass="ItemStylecssRight" HorizontalAlign="center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Compensatory Leave">
                            <ItemStyle Width="8%" CssClass="ItemStylecssRight" HorizontalAlign="center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Sick Leave">
                            <ItemStyle Width="8%" CssClass="ItemStylecssRight" HorizontalAlign="center"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
