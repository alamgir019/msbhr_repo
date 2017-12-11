<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="DonerSetup.aspx.cs" Inherits="SOF_DonerSetup" Title="Doner Setup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../../../JScripts/datetimepicker.js">
        //Date Time Picker script
    </script>
    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js">
    </script>
    <script language="javascript" type="text/javascript">
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }
    </script>
    <div class="empTrainForm" style="height: 750px">
        <div id="formhead2">
            <div style="width: 97%; float: left;">
                Doner Setup</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%"
            Height="660px">
            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel1" Width="100%">
                <HeaderTemplate>
                    Doner Entry
                </HeaderTemplate>
                <ContentTemplate>
                    <div id="PayrollConfigInner" style="height: 350px">
                        <fieldset>
                            <table>
                                <tr>
                                    <td class="textlevel">
                                        Doner Name :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSalarySourceName" runat="server" MaxLength="20" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="textlevel">
                                        Doner Code :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSalarySourceCode" runat="server" MaxLength="20" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td class="textlevelright">
                                        <asp:CheckBox ID="chkInActive" runat="server" CssClass="textlevelleft" Text="Mark Inactive" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <asp:HiddenField ID="hfID" runat="server" />
                        <asp:HiddenField ID="hfIsUpdate" runat="server" />
                        <div class="DivCommand1" style="width: 99%;">
                            <fieldset>
                                <div class="DivCommandL">
                                    <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" Text="Refresh"
                                        Width="70px" OnClick="btnRefresh_Click" />
                                </div>
                                <div class="DivCommandR">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click"
                                        ToolTip="Click on Save Button to store the employee data." />
                                </div>
                            </fieldset>
                        </div>
                        <div style="float: left; margin-top: 10px; width: 99%;">
                            <fieldset>
                                <legend>Doner List</legend>
                                <div style="overflow: scroll; width: 100%; height: 250px">
                                    <asp:GridView ID="grList" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                                        AutoGenerateColumns="False" DataKeyNames="SalarySourceId" OnRowCommand="grList_RowCommand">
                                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                        </SelectedRowStyle>
                                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                        <Columns>
                                            <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                                <ItemStyle Width="5%" CssClass="ItemStylecss" />
                                            </asp:ButtonField>                                           
                                            <asp:BoundField DataField="DonerName" HeaderText="Doner Name">
                                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DonerCode" HeaderText="Doner Code">
                                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                            </asp:BoundField>                                           
                                            <asp:BoundField DataField="IsActive" HeaderText="IsActive">
                                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Width="100%">
                <HeaderTemplate>
                    Doner Upload
                </HeaderTemplate>
                <ContentTemplate>
                    <fieldset>
                        <table>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <div style="width: 100%; height: 580px; overflow: scroll;">
                        <asp:GridView ID="grUpload" runat="server" Width="97%" EmptyDataText="No record found">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                            <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                            <AlternatingRowStyle BackColor="#EFF3FB" />
                        </asp:GridView>
                    </div>
                    <div class="DivCommandR">
                        <asp:Button ID="btnSaveBatch" runat="server" Text="Save Batch" OnClick="btnSaveBatch_Click" />
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </div>
</asp:Content>
