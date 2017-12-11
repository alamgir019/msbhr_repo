<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="EmailNotificationSetup.aspx.cs" Inherits="Payroll_EmailNotificationSetup"
    Title="Email Notification Setup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>

    <div class="officeSetup">
        <div id="formhead1">
            Email Notification Setup</div>
        <!--Div for group-->
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="officeSetupInner">
            <fieldset>
                <!--Div for Controls-->
                <asp:HiddenField ID="hfID" runat="server" />
                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                <table>
                    <tr>
                        <td class="textlevel">
                            Recommended by HR :
                        </td>
                        <td>
                            <asp:TextBox ID="txtHR" runat="server" Width="80px"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtHR"
                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Verified by Finance :
                        </td>
                        <td>
                            <asp:TextBox ID="txtFinance" runat="server" Width="80px"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFinance"
                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Review by Dir Finance :
                        </td>
                        <td>
                            <asp:TextBox ID="txtDirFinance" runat="server" Width="80px"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDirFinance"
                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Payroll Approval :
                        </td>
                        <td>
                            <asp:TextBox ID="txtApprover" runat="server" Width="80px"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtApprover"
                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Payroll Disbursement :
                        </td>
                        <td>
                            <asp:TextBox ID="txtDisburse" runat="server" Width="80px"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDisburse"
                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div class="DivCommand1">
            <div class="DivCommandL">
                
            </div>
            <div class="DivCommandR">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click" />
            </div>
        </div>
    </div>
</asp:Content>
