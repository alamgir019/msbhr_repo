<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="EmpLeaveProfile.aspx.cs" Inherits="EmpLeaveProfile" Title="EmpLeaveProfile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
    function ToUpper(ctrl)
    {   
    var t = ctrl.value;
    ctrl.value = t.toUpperCase(); 
    }
    </script>

    <div class="EmpLeaveProfStyle" style="text-align: left;">
        <div id='formhead4'>
            Leave Records</div>
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server" ForeColor="Navy" Font-Bold="True" CssClass="lblMsg"></asp:Label>
            &nbsp;
        </div>
        <table style="text-align: left; width: 100%">
            <tr style="border: solid 1px red;">
                <td style="width: 99%;">
                    <div class="Div950">
                        <!--Div for group-->
                        <fieldset>
                            <legend>Status</legend>
                            <%--<asp:UpdatePanel id="UpdatePanel1" runat="server">
                                <contenttemplate>--%>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="Employee No : " CssClass="textlevel"
                                                Width="80px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEmpId" onkeyup="ToUpper(this)" Width="80px" runat="server" Height="16px"> </asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Search"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="Name : " CssClass="textlevel" Width="80px"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <asp:Label ID="lblName" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="Designation :" CssClass="textlevel" Width="80px"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <asp:Label ID="lblDesig" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text="Leave Package :" CssClass="textlevel"
                                                Width="80px"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <asp:Label ID="lblLvPackage" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="color: #f00" colspan="3">
                                            <asp:Label ID="lblMsg2" runat="server" CssClass="lblMsg" Width="100%"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <fieldset>
                                <legend>Current Year Leave Status</legend>
                                <div id="empLeaveProfiFrame">
                                    <asp:GridView ID="grLeaveStatus" runat="server" Width="100%" Font-Size="9px" AutoGenerateColumns="False"
                                        DataKeyNames="EmpId,LTypeID,LTypeTitle,lvPrevYearCarry,LCarryOverd,LEntitled,LCashed,LeaveEnjoyed,LeaveElapsed,lvOpening"
                                        EmptyDataText="No Record Found" PageSize="7" OnSelectedIndexChanged="grLeaveStatus_SelectedIndexChanged">
                                        <Columns>
                                            <asp:BoundField DataField="LTypeTitle" HeaderText="Leave Title">
                                                <ItemStyle Width="250px" CssClass="ItemStylecssCenter"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="lvPrevYearCarry" HeaderText="Opening Carry Over" Visible="False">
                                                <ItemStyle Width="80px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LCarryOverd" HeaderText="Carry Over">
                                                <ItemStyle Width="80px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LEntitled" HeaderText="This Year Entry">
                                                <ItemStyle Width="120px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Total Leave Entitlement">
                                                <ItemStyle Width="180px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LeaveEnjoyed" HeaderText="Availed">
                                                <ItemStyle Width="80px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Balance">
                                                <ItemStyle Width="80px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle BackColor="#3366CC" HorizontalAlign="Center" Font-Size="10px" Font-Bold="True"
                                            ForeColor="whitesmoke"></HeaderStyle>
                                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                    </asp:GridView>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Current Year Leave History</legend>Approved/Declined Leave Applications
                                <div id="empLeaveProfiFrame">
                                    <asp:GridView ID="grLeaveDet" runat="server" AutoGenerateColumns="False" DataKeyNames="LvAppID,LTypeID,LTypeTitle,AppDate,LeaveStart,LeaveEnd,LDurInDays,AppStatus,LTReason,AddrAtLeave,PhoneNo,InsertedBy,InsertedDate"
                                        EmptyDataText="No Record Found" OnSelectedIndexChanged="grLeaveDet_SelectedIndexChanged"
                                        Width="100%" OnRowCommand="grLeaveDet_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="LTypeTitle" HeaderText="Leave Name">
                                                <ItemStyle Width="250px" CssClass="ItemStylecssCenter"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AppDate" HeaderText="Submit Date">
                                                <ItemStyle Width="80px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LeaveStart" HeaderText="From">
                                                <ItemStyle Width="120px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LeaveEnd" HeaderText="To">
                                                <ItemStyle Width="120px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LDurInDays" HeaderText="Total Days">
                                                <ItemStyle Width="100px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AppStatus" HeaderText="Status">
                                                <ItemStyle Width="100px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ApprovedBy" HeaderText="Approved By">
                                                <ItemStyle Width="100px" CssClass="ItemStylecss"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ApproveDate" HeaderText="Approved Date">
                                                <ItemStyle Width="100px" CssClass="ItemStylecss"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle BackColor="#3366CC" HorizontalAlign="Center" Font-Size="10px" Font-Bold="True"
                                            ForeColor="whitesmoke"></HeaderStyle>
                                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                    </asp:GridView>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Previous Year Leave Status</legend>
                                <div id="empLeaveProfiFrame">
                                    <asp:GridView ID="grPreYrLeaveStatus" runat="server" Width="100%" Font-Size="9px"
                                        AutoGenerateColumns="False" DataKeyNames="EmpId,LTypeID" EmptyDataText="No Record Found"
                                        PageSize="7">
                                        <Columns>
                                            <asp:BoundField DataField="LTypeTitle" HeaderText="Leave Title">
                                                <ItemStyle Width="250px" CssClass="ItemStylecssCenter"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LCarryOverd" HeaderText="Carry Over">
                                                <ItemStyle Width="80px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LEntitled" HeaderText="Previous Year Entry">
                                                <ItemStyle Width="120px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Total Leave Entitlement">
                                                <ItemStyle Width="180px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LeaveEnjoyed" HeaderText="Availed">
                                                <ItemStyle Width="80px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Balance">
                                                <ItemStyle Width="80px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle BackColor="#B3CDE4" HorizontalAlign="Center" Font-Size="10px" Font-Bold="True"
                                            ForeColor="whitesmoke"></HeaderStyle>
                                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                    </asp:GridView>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Previous Year Leave History</legend>
                                <div id="empLeaveProfiFrame">
                                    <asp:GridView ID="grPreYrLeaveDet" runat="server" AutoGenerateColumns="False" DataKeyNames="LvAppID,LTypeID,LTypeTitle,AppDate,LeaveStart,LeaveEnd,LDurInDays,AppStatus,LTReason,AddrAtLeave,PhoneNo,InsertedBy,InsertedDate"
                                        EmptyDataText="No Record Found" OnSelectedIndexChanged="grLeaveDet_SelectedIndexChanged"
                                        Font="9px" Width="100%" OnRowCommand="grLeaveDet_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="LTypeTitle" HeaderText="Leave Name">
                                                <ItemStyle Width="250px" CssClass="ItemStylecssCenter"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AppDate" HeaderText="Submit Date">
                                                <ItemStyle Width="80px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LeaveStart" HeaderText="From">
                                                <ItemStyle Width="120px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LeaveEnd" HeaderText="To">
                                                <ItemStyle Width="120px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LDurInDays" HeaderText="Total Days">
                                                <ItemStyle Width="100px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AppStatus" HeaderText="Status">
                                                <ItemStyle Width="100px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="InsertedBy" HeaderText="Approved By">
                                                <ItemStyle Width="100px" CssClass="ItemStylecss"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="InsertedDate" HeaderText="Approved Date">
                                                <ItemStyle Width="100px" CssClass="ItemStylecss"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle BackColor="#3366CC" HorizontalAlign="Center" Font-Size="10px" Font-Bold="True"
                                            ForeColor="whitesmoke"></HeaderStyle>
                                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                    </asp:GridView>
                                </div>
                            </fieldset>
                            <%--</contenttemplate>
                            </asp:UpdatePanel>--%>
                        </fieldset>
                        <div class="DivCommand1" style="padding-left: 15px; padding-top: 3px; float: left;">
                            <asp:Button ID="btnClear" runat="server" Text="Refresh" Width="70px" OnClick="btnClear_Click"
                                UseSubmitBehavior="False" />
                        </div>
                    </div>
                </td>
                <td>
                    <asp:Panel ID="pnlEmpList" runat="server">
                        <fieldset>
                            <legend>Employee List</legend>
                            <div style="float: left; margin: 5px 0px 0px 5px; width: 99%; height: 822px; overflow: scroll;">
                                <asp:GridView ID="grEmpList" runat="server" AutoGenerateColumns="False" DataKeyNames="EmpId,FullName,SupervisorId"
                                    EmptyDataText="" Font-Size="9px" PageSize="7" Width="100%" OnRowCommand="grEmpList_RowCommand">
                                    <HeaderStyle BackColor="#3366CC" Font-Bold="True" Font-Size="10px" HorizontalAlign="Left"
                                        ForeColor="whitesmoke" />
                                    <AlternatingRowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#FF9933" />
                                    <Columns>
                                        <asp:ButtonField HeaderText="Select" Text="Select" CommandName="ViewClick" CausesValidation="true">
                                            <ItemStyle Width="10%" CssClass="ItemStylecss"></ItemStyle>
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="EmpId" HeaderText="Emp. ID">
                                            <ItemStyle CssClass="ItemStylecss" Width="20%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FullName" HeaderText="Employee Name">
                                            <ItemStyle CssClass="ItemStylecss" Width="60%" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </fieldset>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
