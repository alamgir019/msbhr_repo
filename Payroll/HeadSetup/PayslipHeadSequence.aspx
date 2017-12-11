<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="PayslipHeadSequence.aspx.cs" Inherits="Payroll_HeadSetup_PayslipHeadSequence"
    Title="Payslip Head Sequence" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js"></script>
    <div id="PayrollConfigForm">
        <div id="formhead1">
            Payslip Head Sequence
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="PayrollConfigInner">
            <fieldset>
                <legend>Payslip Head Sequence</legend><span style="color: Blue;"></span>
                <table>
                    <tr>
                        <td class="textlevel">
                            Seq No. :
                        </td>
                        <td>
                            <asp:TextBox ID="txtSeqNo" runat="server" Width="80px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="ReqValSeqNo" runat="server" ControlToValidate="txtSeqNo"
                                ErrorMessage="*">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <%--<asp:UpdatePanel id="UpdatePanel1" runat="server">
                <contenttemplate>--%>
                <table>
                    <tr>
                        <td class="textlevel">
                            Salary Head :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSalaryHead" runat="server" Width="150px" CssClass="textlevelleft"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlSalaryHead_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                            Display Type :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDisplayType" runat="server" CssClass="textlevelleft" Width="150px"
                                Enabled="False">
                                <asp:ListItem Value="S">Salary</asp:ListItem>
                                <asp:ListItem Value="B">Benefit</asp:ListItem>
                                <asp:ListItem Value="D">Deduction</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <%--</contenttemplate>                
            </asp:UpdatePanel>--%>
                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                <cc1:FilteredTextBoxExtender ID="FilTextBoxSeqNo" runat="server" FilterType="Numbers"
                    TargetControlID="txtSeqNo">
                </cc1:FilteredTextBoxExtender>
                <fieldset>
                    <legend>Sequence List</legend>
                    <div style="width: 100%; overflow: scroll; height: 250px;">
                        <asp:GridView ID="grHeadSeq" runat="server" DataKeyNames="SHEADID,DisplayType" AutoGenerateColumns="False"
                            EmptyDataText="No Record Found" Font-Size="9px" Width="99%" OnRowCommand="grHeadSeq_RowCommand">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                            </SelectedRowStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                            <Columns>
                                <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                    <ItemStyle Width="5%" CssClass="ItemStylecss" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="SeqNo" HeaderText="Sequence No">
                                    <ItemStyle CssClass="ItemStylecss" Width="25%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="HEADNAME" HeaderText="Salary Head">
                                    <ItemStyle CssClass="ItemStylecss" Width="40%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DisplayType" HeaderText="Display Type">
                                    <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </fieldset>
            </fieldset>
            <div id="DivCommand1" style="padding-top: 3px;">
                <div style="text-align: left; float: left">
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px" CausesValidation="False"
                        OnClick="btnRefresh_Click" />
                </div>
                <div style="text-align: right;">
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" UseSubmitBehavior="False"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                        OnClick="btnDelete_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
