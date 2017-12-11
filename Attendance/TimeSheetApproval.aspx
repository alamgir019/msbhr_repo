<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="TimeSheetApproval.aspx.cs" Inherits="Attendance_TimeSheetApproval"
    Title="Time Sheet Approval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
    function ToUpper(ctrl)
    {   
        var t = ctrl.value;
        ctrl.value = t.toUpperCase(); 
    }
    </script>

    <div class="formStyle">
        <div id='formhead4'>
            Time Sheet Approval</div>
        <div id='Div1'>
            &nbsp;</div>
        <div class="iesEmp">
            <table>
                
                <tr>
                    <td class="textlevel" style="width: 80px">
                        Pay Month:</td>
                    <td colspan="5">
                        <asp:DropDownList ID="ddlMonth" runat="server" Width="300px" ToolTip="Automatically current month will be selected.">
                        </asp:DropDownList></td>
                    <td colspan="1">
                    </td>

                    <td class="textlevel" style="width: 80px">
                        pay Year:</td>
                    <td colspan="5">
                        <asp:DropDownList ID="ddlYear" runat="server" Width="300px" ToolTip="Automatically current year will be selected.">
                        </asp:DropDownList></td>
                    <td colspan="1">
                    </td>
                </tr>
                <tr>
                    <td class="textlevel" style="width: 80px">
                        Fiscal Year:</td>
                    <td colspan="5">
                        <asp:DropDownList ID="ddlFiscalYear" runat="server" Width="300px">
                        </asp:DropDownList></td>
                    <td colspan="1">
                    </td>

                    <td class="textlevelshort">
                    </td>
                    <td colspan="5" align="right">
                        <asp:Button ID="btnPriview" runat="server" Text="Search" OnClick="btnPriview_Click" /></td>
                </tr>
            </table>
            <!--Div for group-->
            <fieldset>
                <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%"
                    Height="500px">
                    <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Width="860px"
                        Height="450px">
                        <HeaderTemplate>
                            Time Sheet List
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="Div900">
                                <fieldset>
                                    <legend>Requested Time Sheet List</legend>
                                    <div class="MsgBox">
                                        <!--Div for msg-->
                                        <asp:Label ID="lblMsg" runat="server" ForeColor="Navy" CssClass="msglabel" Font-Bold="True"></asp:Label>
                                    </div>
                                    <div style="border-right: gray 1px solid; border-top: gray 1px solid; margin: 10px 5px 5px 10px;
                                        overflow: scroll; border-left: gray 1px solid; width: 98%; border-bottom: gray 1px solid;
                                        height: 400px">
                                        <!--Grid view Code starts for First TAB-->
                                        <asp:GridView ID="grTimeSheetApp" runat="server" Font-Size="9px" Width="98%" AutoGenerateColumns="False"
                                            EmptyDataText="No Record Found" DataKeyNames="EMPID,VMonth,VYear" PageSize="7" OnRowCommand="grTimeSheetApp_RowCommand">
                                            <Columns>
                                                <asp:BoundField HeaderText="SL No.">
                                                    <ItemStyle Width="3%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                
                                                <asp:BoundField DataField="EMPID" HeaderText="Emp No">
                                                    <ItemStyle Width="18%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FullName" HeaderText="Full Name">
                                                    <ItemStyle Width="18%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="JobTitle" HeaderText="Designation">
                                                    <ItemStyle Width="18%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FROM" HeaderText="From">
                                                    <ItemStyle Width="18%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TO" HeaderText="To">
                                                    <ItemStyle Width="18%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="workTotal" HeaderText="Worked Total">
                                                    <ItemStyle Width="18%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                
                                                <asp:ButtonField HeaderText="View" Text="View" CommandName="ViewClick">
                                                    <ItemStyle Width="5%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:ButtonField>
                                                <asp:ButtonField HeaderText="Approve" Text="Approve" CommandName="ApproveClick">
                                                    <ItemStyle Width="5%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:ButtonField>
                                                
                                                <%--<asp:ButtonField HeaderText="Cancel" Text="Cancel" CommandName="CancelClick">
                                                    <ItemStyle Width="5%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:ButtonField>--%>
                                                <%--<asp:TemplateField HeaderText="Select">
                                                    <ItemStyle CssClass="ItemStylecss" Width="3%" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkBox" runat="server"></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                            </SelectedRowStyle>
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
                    <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel1" Width="860px"
                        Height="450px">
                        <HeaderTemplate>
                            Approved Time Sheet List
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="Div900" style="height: 500px">
                                <fieldset>
                                    <legend>Approved Time Sheet List </legend>
                                    <br />
                                    <div class="MsgBox">
                                        <!--Div for msg-->
                                        <asp:Label ID="lblMsgCancel" runat="server" CssClass="msglabel" Font-Bold="True"
                                            ForeColor="Navy" Width="700px"></asp:Label>
                                    </div>
                                    <div style="border: gray 1px solid; margin: 10px 5px 5px 10px; overflow: scroll;
                                        width: 98%; height: 400px">
                                        <!--Grid view Code starts for 2nd TAB-->
                                        <asp:GridView ID="grTimeSheetApproved" runat="server" Font-Size="9px" Width="100%"
                                            PageSize="7" EmptyDataText="No Record Found" DataKeyNames="EMPID,VMonth,VYear" AutoGenerateColumns="False" OnRowCommand="grTimeSheetApproved_RowCommand">
                                            <Columns>
                                                <asp:BoundField HeaderText="SL No.">
                                                    <ItemStyle Width="3%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                
                                                <asp:BoundField DataField="EMPID" HeaderText="Emp No">
                                                    <ItemStyle Width="18%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FullName" HeaderText="Full Name">
                                                    <ItemStyle Width="18%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="JobTitle" HeaderText="Designation">
                                                    <ItemStyle Width="18%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FROM" HeaderText="From">
                                                    <ItemStyle Width="18%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TO" HeaderText="To">
                                                    <ItemStyle Width="18%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="workTotal" HeaderText="Worked Total">
                                                    <ItemStyle Width="18%" CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                
                                                <asp:ButtonField HeaderText="View" Text="View" CommandName="ViewClick">
                                                    <ItemStyle Width="5%" CssClass="ItemStylecss"></ItemStyle>
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
        </div>
    </div>
</asp:Content>
