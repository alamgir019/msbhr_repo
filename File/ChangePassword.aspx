<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" Title="ChangePassword" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="Form500">
        <div id='formhead5'>
            <div style="width: 95%; float: left;">
                Change Password</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="Div450">
            <fieldset>
                <legend>Change Password</legend>
                <table>
                    <tr>
                        <td class="textlevel">
                            User Id :
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserId" runat="server" Width="205px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Old Password :
                        </td>
                        <td>
                            <asp:TextBox ID="txtOldPass" runat="server" Width="205px" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtOldPass"
                                runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            New Password :
                        </td>
                        <td>
                            <asp:TextBox ID="txtNewPass" runat="server" Width="205px" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtNewPass"
                                runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Confirm New Password :
                        </td>
                        <td>
                            <asp:TextBox ID="txtConfNewPass" runat="server" Width="205px" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtConfNewPass"
                                runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div class="DivCommand1">
            <div class="DivCommandR" style="padding-right: 17px;">
                <asp:Button ID="btnSave" Width="70px" runat="server" Text="Save" OnClick="btnSave_Click" />
            </div>
        </div>
    </div>
</asp:Content>
