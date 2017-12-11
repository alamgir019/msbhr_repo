<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeaveStatementRpt.aspx.cs" Inherits="Leave_LeaveStatementRpt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Leave Statement</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family:Arial;font-size:24px;margin-left:5px;">
        Marie Stopes Bangladesh
    </div>
    <div style="font-family:Arial;font-size:16px;margin-left:5px;">
       Leave Statement
    </div>
    <div style="width:70%;margin-top:20px;">
        <table>
            <tr>
                <td valign="top" style="width:60%;">
                    <table>
                        <tr>
                            <td>
                                <span style="font-family:Tahoma;font-size:12px;font-weight:bold;">Employee ID:</span> 
                            </td>
                            <td>
                                <asp:Label ID="lblEmpID" runat="server" Text="" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span style="font-family:Tahoma;font-size:12px;font-weight:bold;">Employee Name:</span> 
                            </td>
                            <td>
                                <asp:Label ID="lblEmpName" runat="server" Text="" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span style="font-family:Tahoma;font-size:12px;font-weight:bold;">Position:</span> 
                            </td>
                            <td>
                                <asp:Label ID="lblPosition" runat="server" Text="" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span style="font-family:Tahoma;font-size:12px;font-weight:bold;">Office:</span> 
                            </td>
                            <td>
                                <asp:Label ID="lblOffice" runat="server" Text="" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span style="font-family:Tahoma;font-size:12px;font-weight:bold;">Joining Date:</span> 
                            </td>
                            <td>
                                <asp:Label ID="lblJoinDate" runat="server" Text="" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
               <td valign="top" style="width:40%;">
                    <span style="font-family:Tahoma;font-size:12px;font-weight:bold;">Leave balance as of <asp:Label ID="lblPrintDate" runat="server" Text="" Font-Names="Tahoma" Font-Size="12px"></asp:Label></span>
                    <asp:GridView id="grLeaveBalance" runat="server" Width="98%" AutoGenerateColumns="False" 
                    DataKeyNames="EmpId,LTypeID,LTypeTitle,lvPrevYearCarry,LCarryOverd,LEntitled,LCashed,LeaveEnjoyed,LeaveElapsed,lvOpening" 
                    EmptyDataText="No Record Found" BorderColor="dimgray" BorderWidth="1px">
                    <RowStyle BorderColor="dimgray" BorderWidth="1px" Font-Size="10px" Font-Names="Tahoma"/>
                      <Columns>
                            <asp:BoundField DataField="LTypeTitle" HeaderText="Leave Type" HeaderStyle-HorizontalAlign="Left">
                            <ItemStyle Width="250px" CssClass="ItemStylecssLeft" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField> 
                            <asp:BoundField DataField="LCarryOverd" HeaderText="Carry Over" HeaderStyle-HorizontalAlign="Right">
                            <ItemStyle Width="120px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="LEntitled" HeaderText="Credit" HeaderStyle-HorizontalAlign="Right">
                            <ItemStyle Width="120px" CssClass="ItemStylecssRight" HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="LeaveEnjoyed" HeaderText="Availed" HeaderStyle-HorizontalAlign="Right">
                            <ItemStyle Width="80px" CssClass="ItemStylecssRight" HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Balance" HeaderStyle-HorizontalAlign="Right">
                            <ItemStyle Width="80px" CssClass="ItemStylecssRight" HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                      </Columns>
                  <HeaderStyle HorizontalAlign="Center" Font-Size="10px" Font-Bold="True" Font-Names="Tahoma"></HeaderStyle>
                </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <div style="width:70%;margin-left:5px;"> 
        <span style="font-family:Tahoma;font-size:12px;font-weight:bold;">Leave Records from <asp:Label ID="lblLvStartPeriod" runat="server" Text="" Font-Names="Tahoma" Font-Size="12px"></asp:Label> to <asp:Label ID="lblLvEndPeriod" runat="server" Text="" Font-Names="Tahoma" Font-Size="12px"></asp:Label> </span>
        <asp:GridView id="grLeaveHistory" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="" EmptyDataText="No Record Found" Font-Size="12px" Width="98.30%" BorderColor="dimgray" BorderWidth="1px" BorderStyle="solid">
        <RowStyle BorderColor="dimgray" BorderWidth="1px" Font-Size="10px" BorderStyle="solid" Font-Names="Tahoma"/>
                  <Columns>
                     <asp:BoundField DataField="" HeaderText="SL" HeaderStyle-HorizontalAlign="Left">
                    <ItemStyle Width="20px" CssClass="ItemStylecssLeft" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField> 
                     <asp:BoundField DataField="LTypeTitle" HeaderText="Leave Type" HeaderStyle-HorizontalAlign="Left">
                    <ItemStyle Width="210px" CssClass="ItemStylecssLeft" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField> 
                    <asp:BoundField DataField="AppDate" HeaderText="Applied On" HeaderStyle-HorizontalAlign="Left">
                    <ItemStyle Width="80px" CssClass="ItemStylecssCenter" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="LeaveStart" HeaderText="Leave From" HeaderStyle-HorizontalAlign="Left">
                    <ItemStyle Width="80px" CssClass="ItemStylecssCenter" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="LeaveEnd" HeaderText="Leave To" HeaderStyle-HorizontalAlign="Left">
                    <ItemStyle Width="80px" CssClass="ItemStylecssCenter" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="LDurInDays" HeaderText="Days" HeaderStyle-HorizontalAlign="Right">
                    <ItemStyle Width="50px" CssClass="ItemStylecssRight" HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="LTReason" HeaderText="Description" HeaderStyle-HorizontalAlign="Left">
                    <ItemStyle Width="30%" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="ApprovedBy" HeaderText="Approved By" HeaderStyle-HorizontalAlign="Left">
                    <ItemStyle Width="70px" CssClass="ItemStylecssCenter" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="approveDate" HeaderText="Approved On" HeaderStyle-HorizontalAlign="Left">
                    <ItemStyle Width="80px" CssClass="ItemStylecssCenter" HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                  </Columns>
                  <HeaderStyle Font-Size="10px" Font-Bold="True" Font-Names="Tahoma"></HeaderStyle>
                </asp:GridView>
    </div>
    </form>
</body>
</html>
