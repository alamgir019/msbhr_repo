<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpLeaveProfileRpt.aspx.cs"
    Inherits="EmpLeaveProfileRpt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Leave History</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-family: Arial; font-size: 14px; font-weight: bold;">
            Employee Leave History</div>
        <!--Div for group-->
        <fieldset>
            <table>
                <tbody>
                    <tr>
                        <td style="font-family: Arial; font-size: 12px; font-weight: bold;">
                            <asp:Label ID="Label1" runat="server" Text="Employee No" CssClass="textlevel" Width="120px"></asp:Label>
                        </td>
                        <td style="font-family: Arial; font-size: 12px;">
                            :
                            <asp:Label ID="lblEmpNo" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="font-family: Arial; font-size: 12px; font-weight: bold;">
                            <asp:Label ID="Label2" runat="server" Text="Name" CssClass="textlevel" Width="120px"></asp:Label>
                        </td>
                        <td style="font-family: Arial; font-size: 12px;">
                            :
                            <asp:Label ID="lblName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-family: Arial; font-size: 12px; font-weight: bold;">
                            <asp:Label ID="Label3" runat="server" Text="Designation" CssClass="textlevel" Width="120px"></asp:Label>
                        </td>
                        <td style="font-family: Arial; font-size: 12px;">
                            :
                            <asp:Label ID="lblDesig" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-family: Arial; font-size: 12px; font-weight: bold;">
                            <asp:Label ID="Label4" runat="server" Text="Leave Package" CssClass="textlevel" Width="120px"></asp:Label>
                        </td>
                        <td style="font-family: Arial; font-size: 12px;">
                            :
                            <asp:Label ID="lblLvPackage" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <fieldset>
                <legend>Current Year Leave Status</legend>
                <asp:GridView ID="grLeaveStatus" runat="server" Width="98%" AutoGenerateColumns="False"
                    DataKeyNames="EmpId,LTypeID,LTypeTitle,lvPrevYearCarry,LCarryOverd,LEntitled,LCashed,LeaveEnjoyed,LeaveElapsed,lvOpening"
                    EmptyDataText="No Record Found" PageSize="7" OnSelectedIndexChanged="grLeaveStatus_SelectedIndexChanged">
                    <RowStyle Font-Names="Arial" Font-Size="12px" />
                    <Columns>
                        <asp:BoundField DataField="LTypeTitle" HeaderText="Leave Type" HeaderStyle-HorizontalAlign="left">
                            <ItemStyle Width="250px" HorizontalAlign="left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="lvPrevYearCarry" HeaderText="Opening Carry Over" Visible="False">
                            <ItemStyle Width="80px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="LCarryOverd" HeaderText="Carry Over">
                            <ItemStyle Width="80px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="LEntitled" HeaderText="This Year Entry">
                            <ItemStyle Width="120px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Total Leave Entitlement">
                            <ItemStyle Width="180px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="LeaveEnjoyed" HeaderText="Availed">
                            <ItemStyle Width="80px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Balance">
                            <ItemStyle Width="80px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle BackColor="LightGray" HorizontalAlign="Center" Font-Size="12px" Font-Bold="True"
                        Font-Names="Arial"></HeaderStyle>
                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                </asp:GridView>
            </fieldset>
            <br />
            <fieldset>
                <legend>Current Year Leave History</legend>Approved/Declined Leave Applications
                <asp:GridView ID="grLeaveDet" runat="server" AutoGenerateColumns="False" DataKeyNames="LvAppID,LTypeID,LTypeTitle,AppDate,LeaveStart,LeaveEnd,LDurInDays,AppStatus,LTReason,AddrAtLeave,PhoneNo,InsertedBy,InsertedDate"
                    EmptyDataText="No Record Found" OnSelectedIndexChanged="grLeaveDet_SelectedIndexChanged"
                    Width="98%" OnRowCommand="grLeaveDet_RowCommand">
                    <RowStyle Font-Names="Arial" Font-Size="12px" />
                    <Columns>
                        <asp:BoundField DataField="LTypeTitle" HeaderText="Leave Type" HeaderStyle-HorizontalAlign="left">
                            <ItemStyle Width="250px" HorizontalAlign="left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="AppDate" HeaderText="Submit Date">
                            <ItemStyle Width="80px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="LeaveStart" HeaderText="From">
                            <ItemStyle Width="120px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="LeaveEnd" HeaderText="To">
                            <ItemStyle Width="120px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="LDurInDays" HeaderText="Total Days">
                            <ItemStyle Width="100px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="AppStatus" HeaderText="Status">
                            <ItemStyle Width="100px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ApprovedBy" HeaderText="Approved By">
                            <ItemStyle Width="100px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ApproveDate" HeaderText="Approved Date">
                            <ItemStyle Width="100px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle BackColor="LightGray" HorizontalAlign="Center" Font-Size="12px" Font-Bold="True"
                        Font-Names="Arial"></HeaderStyle>
                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                </asp:GridView>
            </fieldset>
            <br />
            <fieldset>
                <legend>Previous Year Leave Status</legend>
                <asp:GridView ID="grPreYrLeaveStatus" runat="server" Width="98%" AutoGenerateColumns="False"
                    DataKeyNames="EmpId,LTypeID" EmptyDataText="No Record Found">
                    <RowStyle Font-Names="Arial" Font-Size="12px" />
                    <Columns>
                        <asp:BoundField DataField="LTypeTitle" HeaderText="Leave Type" HeaderStyle-HorizontalAlign="left">
                            <ItemStyle Width="250px" HorizontalAlign="left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="LCarryOverd" HeaderText="Carry Over">
                            <ItemStyle Width="80px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="LEntitled" HeaderText="Previous Year Entry">
                            <ItemStyle Width="120px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Total Leave Entitlement">
                            <ItemStyle Width="180px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="LeaveEnjoyed" HeaderText="Availed">
                            <ItemStyle Width="80px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Balance">
                            <ItemStyle Width="80px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle BackColor="LightGray" HorizontalAlign="Center" Font-Size="12px" Font-Bold="True"
                        Font-Names="Arial"></HeaderStyle>
                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                </asp:GridView>
            </fieldset>
            <br />
            <fieldset>
                <legend>Previous Year Leave History</legend>
                <asp:GridView ID="grPreYrLeaveDet" runat="server" AutoGenerateColumns="False" DataKeyNames="LvAppID,LTypeID,LTypeTitle,AppDate,LeaveStart,LeaveEnd,LDurInDays,AppStatus,LTReason,AddrAtLeave,PhoneNo,AppType,
                InsertedBy,InsertedDate" EmptyDataText="No Record Found" OnSelectedIndexChanged="grLeaveDet_SelectedIndexChanged"
                    Width="98%" OnRowCommand="grLeaveDet_RowCommand">
                    <RowStyle Font-Names="Arial" Font-Size="12px" />
                    <Columns>
                        <asp:BoundField DataField="LTypeTitle" HeaderText="Leave Type" HeaderStyle-HorizontalAlign="left">
                            <ItemStyle Width="250px" HorizontalAlign="left"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="AppDate" HeaderText="Submit Date">
                            <ItemStyle Width="80px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="LeaveStart" HeaderText="From">
                            <ItemStyle Width="120px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="LeaveEnd" HeaderText="To">
                            <ItemStyle Width="120px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="LDurInDays" HeaderText="Total Days">
                            <ItemStyle Width="100px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="AppStatus" HeaderText="Status">
                            <ItemStyle Width="100px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="InsertedBy" HeaderText="Approved By">
                            <ItemStyle Width="100px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="InsertedDate" HeaderText="Approved Date">
                            <ItemStyle Width="100px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle BackColor="LightGray" HorizontalAlign="Center" Font-Size="12px" Font-Bold="True"
                        Font-Names="Arial"></HeaderStyle>
                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                </asp:GridView>
            </fieldset>
        </fieldset>
    </form>
</body>
</html>
