<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="SalaryPackageUpTool.aspx.cs" Inherits="Payroll_Payroll_SalaryPackageUpTool" Title="Salary Package Upload Tool" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">

</script>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js"></script>
    <div id="PayrollConfigForm2" style="width: 90%;">
        <div id="formhead1">
            <div style="width: 98%; float: left;">
                Salary Package Upload Tool</div>
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
                        <td>
                            <asp:Button ID="btnGenerate" runat="server" Text="Generate" 
                                onclick="btnGenerate_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset style="margin-top: 10px;">
                <legend>Employees Basic Salary List</legend>
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
