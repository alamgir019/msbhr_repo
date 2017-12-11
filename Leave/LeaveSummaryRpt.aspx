<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeaveSummaryRpt.aspx.cs"
    Inherits="Leave_LeaveSummaryRpt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Summary</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-family: Arial; font-size: 20px;">
            Marie Stopes Bangladesh
        </div>
        <div style="font-family: Arial; font-size: 16px;">
            Leave Summary
        </div>
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
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        <div>
            <table width="95.5%" style="table-layout: fixed; border-collapse: collapse; border: solid 1px Gray;">
                <tr style="background-color: #3366CC; color: White; font-size: 11px; font-weight: bold;
                    font-family: Tahoma; height: 24px;">
                    <td style="width: 4%; font-family: Tahoma; padding-left: 5px;">
                        Sl. No
                    </td>
                    <td style="width: 8%; font-family: Tahoma; padding-left: 2px;">
                        Employee ID
                    </td>
                    <td style="width: 20%; font-family: Tahoma; padding-left: 2px;">
                        Employee Name
                    </td>
                    <td style="width: 18%; font-family: Tahoma; padding-left: 2px;">
                        Office Name
                    </td>
                    <td style="width: 10%; text-align: center;">
                        Leave Title
                    </td>
                    <td style="width: 10%; text-align: center;">
                        Carry Over
                    </td>
                    <td style="width: 10%; text-align: center;">
                        Entitled
                    </td>
                    <td style="width: 10%; text-align: center;">
                        Availed
                    </td>
                    <td style="width: 10%; text-align: center;">
                        Balance
                    </td>
                </tr>
            </table>
            <asp:GridView ID="grLeaveMaster" runat="server" AutoGenerateColumns="False" ShowHeader="false"
                DataKeyNames="" EmptyDataText="No Record Found" Font-Size="14px" Width="95.4%">
                <RowStyle Font-Size="14px" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray" />
                <Columns>
                    <asp:BoundField HeaderText="SL No." ItemStyle-VerticalAlign="Top">
                        <ItemStyle Width="4%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="EmpId" HeaderText="Emp ID" ItemStyle-VerticalAlign="Top">
                        <ItemStyle Width="8%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="FullName" HeaderText="Employee Name" ItemStyle-VerticalAlign="Top">
                        <ItemStyle Width="20%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="PostingDivName" HeaderText="Office" ItemStyle-VerticalAlign="Top">
                        <ItemStyle Width="18%"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemStyle Width="60%" BorderStyle="solid" BorderColor="DimGray" BorderWidth="1px" />
                        <ItemTemplate>
                            <asp:GridView ID="grLeaveDetails" runat="server" AutoGenerateColumns="False" ShowHeader="false"
                                DataKeyNames="LTypeId" EmptyDataText="No Record Found" Font-Size="12px" BorderStyle="none"
                                BorderColor="White" Width="100%">
                                <RowStyle Font-Size="12px" />
                                <Columns>
                                    <asp:BoundField DataField="LTypeTitle" HeaderText="" ItemStyle-HorizontalAlign="center">
                                        <ItemStyle Width="20%" BorderWidth="1px" BorderStyle="solid" BorderColor="LightGray" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LCarryOverd" HeaderText="" ItemStyle-HorizontalAlign="center">
                                        <ItemStyle Width="20%" BorderWidth="1px" BorderStyle="solid" BorderColor="LightGray" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LEntitled" HeaderText="" ItemStyle-HorizontalAlign="center">
                                        <ItemStyle Width="20%" BorderWidth="1px" BorderStyle="solid" BorderColor="LightGray" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LeaveEnjoyed" HeaderText="" ItemStyle-HorizontalAlign="center">
                                        <ItemStyle Width="20%" BorderWidth="1px" BorderStyle="solid" BorderColor="LightGray" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="" HeaderText="Balance" ItemStyle-HorizontalAlign="center">
                                        <ItemStyle Width="20%" BorderWidth="1px" BorderStyle="solid" BorderColor="LightGray" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
