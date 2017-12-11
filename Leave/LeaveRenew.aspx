<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="LeaveRenew.aspx.cs" Inherits="Leave_LeaveRenew" Title="Leave Renew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>

    <div class="empSearchForm">
        <div id='formhead4'>
            Leave Renewal</div>
        <div class="Div950">
            <fieldset>
                <legend>Leave Renew </legend>
                <div class="MsgBox">                    <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
                </div>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" CssClass="textlevel" Text="Leave Package :"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlLeavePak" runat="server" CssClass="textlevelleft" Width="250px">
                            </asp:DropDownList></td>
                        <td>
                            <asp:Button ID="btnShow" OnClick="btnShow_Click" runat="server" Text="Show" Width="80px">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
                <div id="empSearchResult">
                    <strong>Employee Search Result </strong>
                    <asp:GridView ID="grEmployee" runat="server" AutoGenerateColumns="False" DataKeyNames="EmpId,DeptId,SbuId,LocID,DivisionID,DesgId"
                        EmptyDataText="No Record Found" Font-Size="9px">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                        <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingRowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemStyle CssClass="ItemStylecss" Width="40px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkBox" runat="Server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="SL.No">
                                <ItemStyle CssClass="ItemStylecss" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EmpId" HeaderText="Emp.No">
                                <ItemStyle CssClass="ItemStylecss" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FullName" HeaderText="Employee Name">
                                <ItemStyle CssClass="ItemStylecss" Width="25%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="JobTitle" HeaderText="Designation">
                                <ItemStyle CssClass="ItemStylecss" Width="12%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DeptName" HeaderText="Team">
                                <ItemStyle CssClass="ItemStylecss" Width="12%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SBUName" HeaderText="Program">
                                <ItemStyle CssClass="ItemStylecss" Width="12%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LocationName" HeaderText="Location">
                                <ItemStyle CssClass="ItemStylecss" Width="12%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DivisionName" HeaderText="Office">
                                <ItemStyle CssClass="ItemStylecss" Width="12%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CardNo" HeaderText="Card No">
                                <ItemStyle CssClass="ItemStylecss" Width="5%" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div style="margin-left: 10px">
                    <table>
                        <tr>
                            <td style="width: 3px">
                                <asp:Label ID="Label3" runat="server" CssClass="textlevel" Text="Total Record Count:"></asp:Label></td>
                            <td style="width: 3px">
                                <asp:Label ID="lblRecordCount" runat="server" Font-Bold="True" Font-Size="Smaller"
                                    ForeColor="Blue"></asp:Label></td>
                        </tr>
                    </table>
                </div>
                <%--         <table style="width: 656px">
                <tr>
                    <td class="" colspan="2" rowspan="2" style="width: 384px">
            <asp:GridView ID="GridView1" runat="server">
                
            </asp:GridView>
        </td>
                    <td class="textlevel" style="width: 216px; height: 20px">
                        Click Strart Button for Leave renew</td>
                </tr>
                <tr>
                    <td style="width: 216px; height: 26px">
                        <asp:Button ID="txtStart" runat="server" Text="Start" Width="95px" /></td>
                </tr>
            </table>--%>
                <table style="width: 100%; border-top-style: solid; border-right-style: solid; border-left-style: solid;
                    border-bottom-style: solid;" border="1">
                    <tr>
                        <td style="width: 250px; height: 20px" align="left" class="textlevelleft">
                            Old Leave period</td>
                        <td class="textlevelleft" style="height: 20px; width: 250px;" align="left">
                            New leave period</td>
                        <td class="textlevel" style="width: 216px; height: 20px">
                            Click Start Button for Leave renew</td>
                    </tr>
                    <tr>
                        <td style="width: 250px; height: 26px">
                            <asp:GridView ID="GridView1" runat="server" Width="360px" AutoGenerateColumns="False">
                                <HeaderStyle BackColor="#B3CDE4" HorizontalAlign="Center" Font-Size="Small" Font-Bold="True">
                                </HeaderStyle>
                                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                <Columns>
                                    <asp:BoundField DataField="LPackName" HeaderText="Leave Package">
                                        <ItemStyle CssClass="ItemStylecss" Width="170px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LeaveStartPeriod" HeaderText="Leave Start Period">
                                        <ItemStyle CssClass="ItemStylecss" Width="100px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LeaveEndPeriod" HeaderText="Leave End Period">
                                        <ItemStyle CssClass="ItemStylecss" Width="100px"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                        <td style="height: 26px; width: 250px;">
                            <asp:GridView ID="GridView2" runat="server" Width="360px" AutoGenerateColumns="False">
                                <HeaderStyle BackColor="#B3CDE4" HorizontalAlign="Center" Font-Size="Small" Font-Bold="True">
                                </HeaderStyle>
                                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                <Columns>
                                    <asp:BoundField DataField="LPackName" HeaderText="Leave Package">
                                        <ItemStyle CssClass="ItemStylecss" Width="170px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LeaveStartPeriod" HeaderText="Leave Start Period">
                                        <ItemStyle CssClass="ItemStylecss" Width="100px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LeaveEndPeriod" HeaderText="Leave End Period">
                                        <ItemStyle CssClass="ItemStylecss" Width="100px"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                        <td style="width: 216px; height: 26px">
                            <asp:Button ID="btnStart" runat="server" Text="Start" Width="95px" OnClientClick="javascript:return LeaveRenewConfirmation();"
                                OnClick="cmdStart_Click" />&nbsp;
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>
    <br />
    <br />
</asp:Content>
