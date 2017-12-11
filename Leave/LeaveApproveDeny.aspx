<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="LeaveApproveDeny.aspx.cs" Inherits="LeaveApproveDeny" Title="LeaveApproveDeny" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function ToUpper(ctrl)
        {   
            var t = ctrl.value;
            ctrl.value = t.toUpperCase(); 
        }
    </script>

    <div class="formStyle">
        <div id='formhead4'>
            <div style="width: 98%; float: left;">
                Leave Approval</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <div class="iesEmp">
            <!--Div for group-->
            <%--<asp:UpdateProgress Id="UpProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                DisplayAfter="0">
                <progresstemplate>
                    <div style="position: absolute; visibility: visible; border: none; z-index: 100;
                        width: 100%; height: 100%; background: #999; filter: alpha(opacity=80); -moz-opacity: .8;
                        opacity: .8; text-align: center;">
                    
                    <img style="position: relative; top: 45%;" src="../Images/photo-loader.gif" alt="" />
                    </div>
                </progresstemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <contenttemplate>--%>
            <fieldset>
                <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%"
                    Height="500px">
                    <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Width="860px"
                        Height="450px">
                        <HeaderTemplate>
                            Leave Application List
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="Div900">
                                <fieldset>
                                    <legend>Requested Leave List</legend>
                                    <div class="MsgBox">
                                        <!--Div for msg-->
                                        <asp:Label ID="lblMsg" runat="server" ForeColor="Navy" CssClass="msglabel" Font-Bold="True"></asp:Label>
                                    </div>
                                    <div style="text-align: center;">
                                        <asp:Button ID="btnSendReminder" runat="server" Text="Send Reminder to Supervisor"
                                            OnClick="btnSendReminder_Click" Visible="False" />
                                    </div>
                                    <div style="border-right: gray 1px solid; border-top: gray 1px solid; margin: 10px 5px 5px 10px;
                                        overflow: scroll; border-left: gray 1px solid; width: 98%; border-bottom: gray 1px solid;
                                        height: 400px">
                                        <!--Grid view Code starts for First TAB-->
                                        <asp:GridView ID="grLeaveApp" runat="server" Font-Size="9px" Width="100%" AutoGenerateColumns="False"
                                            
                                            DataKeyNames="LvAppID,LTypeTitle,LTypeId,LNature,AppDate,LeaveStart,LeaveEnd,LDurInDays,InsertedBy,LTReason,LAbbrName,EmpId,Fullname,SupervisorId" EmptyDataText="No Record Found"
                                            OnRowCommand="grLeaveApp_RowCommand" 
                                            onselectedindexchanged="grLeaveApp_SelectedIndexChanged">
                                            <Columns>
                                                <asp:BoundField HeaderText="SL No.">
                                                    <ItemStyle Width="3%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EmpId" HeaderText="Emp ID and Name">
                                                    <ItemStyle Width="19%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AppDate" HeaderText="Application Date">
                                                    <ItemStyle Width="7%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LTypeTitle" HeaderText="Leave Name">
                                                    <ItemStyle Width="10%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LeaveStart" HeaderText="Leave From">
                                                    <ItemStyle Width="7%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LeaveEnd" HeaderText="Leave To">
                                                    <ItemStyle Width="7%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LDurInDays" HeaderText="Total Days">
                                                    <ItemStyle Width="4%" CssClass="ItemStylecssCenter"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="InsertedBy" HeaderText="Posted By">
                                                    <ItemStyle Width="7%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:ButtonField HeaderText="View" Text="View" CommandName="ViewClick">
                                                    <ItemStyle Width="4%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:ButtonField>
                                                <asp:ButtonField HeaderText="Approve" Text="Approve" CommandName="ApproveClick">
                                                    <ItemStyle Width="5%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:ButtonField>
                                                <asp:ButtonField HeaderText="Regret" Text="Regret" CommandName="DenyClick">
                                                    <ItemStyle Width="5%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:ButtonField>
                                                <asp:ButtonField HeaderText="Cancel" Text="Cancel" CommandName="CancelClick">
                                                    <ItemStyle Width="4%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:ButtonField>
                                            </Columns>
                                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True"></SelectedRowStyle>
                                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                        </asp:GridView>
                                        <!--Grid view Code Ends-->
                                    </div>
                                    <asp:HiddenField ID="hfLDates" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="hfLEnjoyed" runat="server"></asp:HiddenField>
                                </fieldset>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2" Width="860px"
                        Height="450px">
                        <HeaderTemplate>
                            Regretted Leave List
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="Div900">
                                <fieldset>
                                    <legend>Regretted Leave List </legend>
                                    <div class="MsgBox">
                                        <!--Div for msg-->
                                        <asp:Label ID="lblMsgDeny" runat="server" ForeColor="Navy" Width="700px" Font-Bold="True"
                                            CssClass="msglabel"></asp:Label>
                                    </div>
                                    <div style="border-right: gray 1px solid; border-top: gray 1px solid; margin: 10px 5px 5px 10px;
                                        overflow: scroll; border-left: gray 1px solid; width: 98%; border-bottom: gray 1px solid;
                                        height: 400px;">
                                        <!--Grid view Code starts for 2nd TAB-->
                                        <asp:GridView ID="grLeaveDeny" runat="server" Font-Size="9px" Width="100%" OnRowCommand="grLeaveDeny_RowCommand"
                                            DataKeyNames="LvAppID,LTypeTitle,LTypeId,LNature,AppDate,
                                            LeaveStart,LeaveEnd,LDurInDays,UpdatedBy,LTReason,LAbbrName,EmpId,FullName" AutoGenerateColumns="False"
                                            EmptyDataText="No Record Found" PageSize="7">
                                            <Columns>
                                                <asp:BoundField HeaderText="SL No.">
                                                    <ItemStyle Width="3%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EmpId" HeaderText="Emp ID and Name">
                                                    <ItemStyle Width="22%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AppDate" HeaderText="Application Date">
                                                    <ItemStyle Width="7%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LTypeTitle" HeaderText="Leave Name">
                                                    <ItemStyle Width="10%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LeaveStart" HeaderText="Leave From">
                                                    <ItemStyle Width="7%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LeaveEnd" HeaderText="Leave To">
                                                    <ItemStyle Width="7%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LDurInDays" HeaderText="Total Days" HeaderStyle-HorizontalAlign="center">
                                                    <ItemStyle Width="4%" CssClass="ItemStylecssCenter"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="UpdatedBy" HeaderText="Regretted By">
                                                    <ItemStyle Width="7%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:ButtonField HeaderText="View" Text="View" CommandName="ViewClick">
                                                    <ItemStyle Width="4%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:ButtonField>
                                                <asp:ButtonField HeaderText="Approve" Text="Approve" CommandName="ApproveClick">
                                                    <ItemStyle Width="5%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:ButtonField>
                                                <asp:ButtonField HeaderText="Cancel" Text="Cancel" CommandName="CancelClick">
                                                    <ItemStyle Width="5%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:ButtonField>
                                            </Columns>
                                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True"></SelectedRowStyle>
                                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                        </asp:GridView>
                                        <!--Grid view Code Ends-->
                                    </div>
                                </fieldset>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel1" Width="860px"
                        Height="450px">
                        <HeaderTemplate>
                            Approved Leave List
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="Div900" style="height: 500px">
                                <fieldset>
                                    <legend>Approved Leave List </legend>
                                    <br />
                                    <div class="MsgBox">
                                        <!--Div for msg-->
                                        <asp:Label ID="lblMsgCancel" runat="server" CssClass="msglabel" Font-Bold="True"
                                            ForeColor="Navy" Width="700px"></asp:Label>
                                    </div>
                                    <div style="border: gray 1px solid; margin: 10px 5px 5px 10px; overflow: scroll;
                                        width: 98%; height: 400px">
                                        <!--Grid view Code starts for 2nd TAB-->
                                        <asp:GridView ID="grLeaveApprove" runat="server" Font-Size="9px" Width="100%" PageSize="7"
                                            EmptyDataText="No Record Found" DataKeyNames="LvAppID,LTypeTitle,LTypeId,LNature,AppDate,
                                            LeaveStart,LeaveEnd,LDurInDays,ApprovedBy,LTReason,LAbbrName,EmpId,FullName" AutoGenerateColumns="False"
                                            OnRowCommand="grLeaveApprove_RowCommand">
                                            <Columns>
                                                <asp:BoundField HeaderText="SL No.">
                                                    <ItemStyle Width="3%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EmpId" HeaderText="Emp ID and Name">
                                                    <ItemStyle Width="30%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AppDate" HeaderText="Application Date">
                                                    <ItemStyle Width="7%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LTypeTitle" HeaderText="Leave Name">
                                                    <ItemStyle Width="10%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LeaveStart" HeaderText="Leave From">
                                                    <ItemStyle Width="7%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LeaveEnd" HeaderText="Leave To">
                                                    <ItemStyle Width="7%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LDurInDays" HeaderText="Total Days" HeaderStyle-HorizontalAlign="center">
                                                    <ItemStyle Width="4%" CssClass="ItemStylecssCenter"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ApprovedBy" HeaderText="Approve By">
                                                    <ItemStyle Width="15%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:ButtonField HeaderText="View" Text="View" CommandName="ViewClick">
                                                    <ItemStyle Width="4%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:ButtonField>
                                                <asp:ButtonField HeaderText="Cancel" Text="Cancel" CommandName="CancelClick">
                                                    <ItemStyle Width="4%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:ButtonField>
                                            </Columns>
                                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                            </SelectedRowStyle>
                                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                        </asp:GridView>
                                        <!--Grid view Code Ends-->
                                    </div>
                                    <asp:HiddenField ID="hfLDatesForCancel" runat="server" />
                                </fieldset>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </fieldset>
            <%--</contenttemplate>
            </asp:UpdatePanel>--%>
        </div>
    </div>
</asp:Content>
