<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeOnLeaveRpt.aspx.cs"
    Inherits="Leave_EmployeeOnLeaveRpt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employees on Leave</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-family: Arial; font-size: 14px;">
            Marie Stopes
        </div>
        <div style="font-family: Arial; font-size: 14px;">
            Employees on Leave
        </div>
        <div style="width: 95%; float: left; margin-bottom: 5px; border-bottom: solid 1px #FF9933">
            <table>
                <tr>
                    <td>
                        <span style="font-family: Tahoma; font-size: 12px;">From Date:</span>
                    </td>
                    <td>
                        <asp:Label ID="lblFrom" runat="server" Text="" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                    </td>
                    <td>
                        <span style="font-family: Tahoma; font-size: 12px;">To Date:</span>
                    </td>
                    <td>
                        <asp:Label ID="lblTo" runat="server" Text="" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="float: left; width: 95%;">
            <asp:GridView ID="grLeaveApp" runat="server" AutoGenerateColumns="False" DataKeyNames=""
                EmptyDataText="No Record Found" Font-Size="12px" Width="100%">
                <HeaderStyle BackColor="#3366CC" Font-Bold="True" HorizontalAlign="Left" ForeColor="whitesmoke"
                    Font-Names="Tahoma" Font-Size="12px" />
                <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                <AlternatingRowStyle BackColor="#EFF3FB" />
                <RowStyle Font-Size="12px" />
                <Columns>
                    <asp:BoundField HeaderText="SL No.">
                        <ItemStyle CssClass="ItemStylecss" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="EmpId" HeaderText="Emp ID">
                        <ItemStyle Width="5%" CssClass="ItemStylecss"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="FullName" HeaderText="Employee Name">
                        <ItemStyle Width="13%" CssClass="ItemStylecss"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="PostingDivName" HeaderText="Posting Division">
                        <ItemStyle Width="12%" CssClass="ItemStylecss"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="DeptName" HeaderText="Department">
                        <ItemStyle Width="10%" CssClass="ItemStylecss"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="LTypeTitle" HeaderText="Leave Type">
                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AppDate" HeaderText="Application Date">
                        <ItemStyle CssClass="ItemStylecss" Width="7%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LeaveStart" HeaderText="From">
                        <ItemStyle CssClass="ItemStylecss" Width="7%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LeaveEnd" HeaderText="To">
                        <ItemStyle CssClass="ItemStylecss" Width="7%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ResumeDate" HeaderText="Resume Office On">
                        <ItemStyle CssClass="ItemStylecss" Width="7%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LDurInDays" HeaderText="Total Days">
                        <ItemStyle CssClass="ItemStylecssRight" Width="5%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LTReason" HeaderText="Reason">
                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LvPhoneNo" HeaderText="Contact">
                        <ItemStyle CssClass="ItemStylecss" Width="5%" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
