<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="Options.aspx.cs" Inherits="File_Options" Title="PaySlip Option" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="officeSetup">
        <div id="formhead1">
            PaySlip Options</div>
        <!--Div for group-->
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="officeSetupInner">
            <fieldset>
                <%--<asp:UpdatePanel id="UpdatePanel1" runat="server">
              <contenttemplate>--%>
                <table style="width: 400px">
                    <tbody>
                        <tr>
                            <td class="textlevel">
                                Retirement Age</td>
                            <td>
                                <asp:TextBox ID="txtRetAge" Width="80px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 236px" class="textlevel">
                            </td>
                            <td style="width: 103px; text-align: left" class="textlevel">
                            </td>
                        </tr>
                    </tbody>
                </table>
                &nbsp;&nbsp; </contenttemplate>
                <%--</asp:UpdatePanel>
              </fieldset>--%>
                <div id="DivCommand1" style="padding-top: 3px;">
                    <div style="text-align: left; float: left">
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px" CausesValidation="False" />
                    </div>
                    <div style="text-align: right;">
                        <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" UseSubmitBehavior="False"
                            OnClick="btnSave_Click" />
                    </div>
                </div>
        </div>
    </div>
</asp:Content>
