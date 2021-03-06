<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="VariableAllowanceUpload.aspx.cs" Inherits="Payroll_Payroll_VariableAllowanceUpload" Title="File Upload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js"></script>
    <div id="PayrollConfigForm2" style="width: 90%;">
        <div id="formhead1">
            <div style="width: 98%; float: left;">
                Variable Allowance Deduction Upload</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div style="margin-left: 10px; margin-right: 10px;">
            <fieldset>
                <table>
                    <tr>
                        <td class="textlevel">
                            Month :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" runat="server" Width="100px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                            Year :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlYear" runat="server" Width="100px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                            Salary Item :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSalHead" runat="server" Width="140px" CssClass="textlevelleft">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="-1" runat="server"
                                ControlToValidate="ddlSalHead" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2">
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
            <fieldset style="margin-top: 10px;">
                <legend>Variable Allowances/ Deductions List</legend>
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
        </div>
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
</asp:Content>
