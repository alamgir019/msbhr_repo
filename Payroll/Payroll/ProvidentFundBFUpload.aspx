<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="ProvidentFundBFUpload.aspx.cs" Inherits="Payroll_Payroll_ProvidentFundBFUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="PayrollConfigForm2" style="width: 90%;">
        <div id="formhead1">
            <div style="width: 98%; float: left;">
                Provident Fund Balance Forward Upload</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div style="margin-left: 10px; margin-right: 10px;">
            <cc1:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="0" Height="460px">
                <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel1">
                    <HeaderTemplate>
                        PF File Upload
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td class="textlevel">
                                    Fiscal Year:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFiscalYear" runat="server" Width="130px" CssClass="textlevelleft">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="textlevel">
                                    File:
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                                </td>
                            </tr>
                        </table>
                        <fieldset style="margin-top: 10px;">
                            <legend>Provident Fund Balance Forward List</legend>
                            <div>
                                <asp:GridView ID="grPayroll" runat="server" AutoGenerateColumns="true" ShowHeader="true"
                                    Width="97%" EmptyDataText="No record found">
                                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                                    <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingRowStyle BackColor="#EFF3FB" />
                                </asp:GridView>
                            </div>
                            <div style="width: 100%; text-align: center;">
                                <asp:Label ID="lblRecord" runat="server" Font-Bold="True" ForeColor="#006666"></asp:Label>
                            </div>
                        </fieldset>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="TabPanel1">
                    <HeaderTemplate>
                        PF Forward</HeaderTemplate>
                    <ContentTemplate>
                        <fieldset>
                            <table>
                                <tr>
                                    <td class="textlevel">
                                        Fiscal Year:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPFFiscalYearID" runat="server" Width="250px" Font-Bold="True" ForeColor="Black"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="textlevel">
                                    </td>
                                    <td>
                                        <asp:Button ID="BtnGenerate" runat="server" Text="Generate" OnClick="BtnGenerate_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:HiddenField ID="hfIsUpdate" runat="server" />
                            <asp:HiddenField ID="hfId" runat="server" />
                        </fieldset>
                        <legend>PF Forward</legend>
                        <div style="overflow: scroll; width: 100%; height: 400px; overflow: scroll">
                            <asp:GridView ID="grPFBF" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                                AutoGenerateColumns="False" DataKeyNames="EmpId">
                                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                </SelectedRowStyle>
                                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                <Columns>
                                    <asp:BoundField DataField="EmpId" HeaderText="Emp. ID">
                                        <ItemStyle CssClass="ItemStylecssRight" Width="10%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PFFiscalYrId" HeaderText="PF Fisca lYear">
                                        <ItemStyle CssClass="ItemStylecssRight" Width="10%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CarryForward" HeaderText="Carry Forward">
                                        <ItemStyle CssClass="ItemStylecssRight" Width="15%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalContribution" HeaderText="Total Contribution">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="15%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalInterest" HeaderText="Total Interest">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="15%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BroadForward" HeaderText="Broad Forward">
                                        <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="IsUpdate" HeaderText="" Visible="true">
                                        <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        </fieldset>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <div id="DivCommand1" style="margin-left: 10px; margin-right: 10px;">
                <div style="text-align: left; float: left">
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px" CausesValidation="False"
                        OnClick="btnRefresh_Click" />
                </div>
                <div style="text-align: right; margin-right: 20px;">
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" UseSubmitBehavior="False"
                        OnClick="btnSave_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
