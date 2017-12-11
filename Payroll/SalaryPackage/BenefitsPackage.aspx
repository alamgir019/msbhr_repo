<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="BenefitsPackage.aspx.cs" Inherits="Payroll_SalaryPackage_BenefitsPackage"
    Title="Benfits Package" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js"></script>

    <div id="PayrollConfigForm2">
        <div id="formhead1">
            Benefits Package Setup
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="PayrollConfigInner2">
            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="430px">
                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Salary Package Setup" Height="410px">
                    <HeaderTemplate>
                        Benefits Package Setup
                    </HeaderTemplate>
                    <ContentTemplate>
                        <fieldset>
                            <table>
                                <tr>
                                    <td class="textlevel">
                                        Package Title
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHeadTitle" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="ReqValHeadTitle" runat="server" ControlToValidate="txtHeadTitle"
                                            ErrorMessage="*">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="textlevel">
                                        <asp:CheckBox ID="chkInActive" runat="server" Text="Make Inactive " CssClass="textlevelauto" /></td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="textlevel">
                                        Description
                                    </td>
                                    <td colspan="4">
                                        <asp:TextBox ID="txtDescription" runat="server" Width="400px"></asp:TextBox></td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td class="textlevelauto">
                                        Salary Head
                                    </td>
                                    <td class="textlevelauto">
                                        Payment Amount
                                    </td>
                                    <td class="textlevelauto">
                                    </td>
                                    <td class="textlevelauto">
                                        Percent of
                                    </td>
                                    <td class="textlevelauto">
                                        Payment Type
                                    </td>
                                    <td class="textlevelauto">
                                        Calculation Rules</td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlSalHead" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPayAmount" runat="server" Width="80px" CssClass="TextBoxAmt60"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="fltbPayAmount" runat="server" TargetControlID="txtPayAmount"
                                            FilterType="Custom, Numbers" ValidChars="." Enabled="True">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                    <td class="textlevelauto">
                                        <asp:CheckBox ID="chkPercent" runat="server" Text="Percent of " AutoPostBack="True"
                                            OnCheckedChanged="chkPercent_CheckedChanged" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPercentSalHead" runat="server" Width="100px" Enabled="False">
                                            <asp:ListItem Value="B">Basic</asp:ListItem>
                                            <asp:ListItem Value="G">Gross Salary</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPaymentType" runat="server" Width="100px">
                                            <asp:ListItem Value="0">Payslip</asp:ListItem>
                                            <asp:ListItem Value="1">Daily Payment</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCalRules" runat="server" Width="240px">
                                            <asp:ListItem Value="0">For Every Month</asp:ListItem>
                                            <asp:ListItem Value="1">For Each Day</asp:ListItem>
                                            <asp:ListItem Value="22">For Each Working Day</asp:ListItem>
                                            <asp:ListItem Value="3">For Each Holiday</asp:ListItem>
                                            <asp:ListItem Value="4">For Each Weekend Day</asp:ListItem>
                                            <asp:ListItem Value="5">For Every Weekend and Holiday</asp:ListItem>
                                            <asp:ListItem Value="6">For Each OT Hour</asp:ListItem>
                                            <asp:ListItem Value="7">For Transportation</asp:ListItem>
                                            <asp:ListItem Value="8">For Extra Time With Fixed Time Limit</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td>
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" Width="70px" UseSubmitBehavior="False"
                                            OnClick="btnAdd_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlSalHead"
                                            ErrorMessage="Select Salary Head" Operator="NotEqual" ValueToCompare="-1"></asp:CompareValidator></td>
                                    <td>
                                    </td>
                                    <td class="textlevelauto">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:HiddenField ID="hfID" runat="server" />
                            <asp:HiddenField ID="hfIsUpdate" runat="server" />
                        </fieldset>
                        <fieldset>
                            <div style="width: 98%; overflow: scroll; height: 280px;">
                                <asp:GridView ID="grBenefitHeads" runat="server" EmptyDataText="No Record Found"
                                    Font-Size="9px" Width="99.99%" AutoGenerateColumns="False" DataKeyNames="PACKAGEID,SHEADID,PERCENTSALHEAD,PAYMENTTYPE,CALRULES"
                                    OnRowCommand="grBenefitHeads_RowCommand">
                                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                    </SelectedRowStyle>
                                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:ButtonField CommandName="DoubleClick" HeaderText="Remove" Text="Remove">
                                            <ItemStyle Width="5%" CssClass="ItemStylecss" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="HEADNAME" HeaderText="Salary Head">
                                            <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PAYAMT" HeaderText="Payment Amount">
                                            <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ISINPERCENT" HeaderText="Is Pay(%)">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PERCENTSALHEAD" HeaderText="Percent Sal. Head">
                                            <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PAYMENTTYPE" HeaderText="Payment Type">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CALRULES" HeaderText="Calculation Rules">
                                            <ItemStyle CssClass="ItemStylecss" Width="25%"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </fieldset>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2" Height="410px">
                    <HeaderTemplate>
                        Benefits Package List
                    </HeaderTemplate>
                    <ContentTemplate>
                        <fieldset>
                            <div style="width: 98%; overflow: scroll; height: 390px;">
                                <asp:GridView ID="grPackageList" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                                    Font-Size="9px" Width="96%" OnRowCommand="grPackageList_RowCommand" DataKeyNames="PackageID">
                                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                    </SelectedRowStyle>
                                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:ButtonField CommandName="RowEdit" HeaderText="Edit" Text="Edit">
                                            <ItemStyle Width="5%" CssClass="ItemStylecss" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="PackageName" HeaderText="Package Title">
                                            <ItemStyle CssClass="ItemStylecss" Width="40%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PackageDescription" HeaderText="Description">
                                            <ItemStyle CssClass="ItemStylecss" Width="40%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ISActive" HeaderText="Is Active">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </fieldset>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <div id="DivCommand1" style="padding-top: 3px;">
                <div style="text-align: left; float: left">
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px" CausesValidation="False" />
                </div>
                <div style="text-align: right;">
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" UseSubmitBehavior="False"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
