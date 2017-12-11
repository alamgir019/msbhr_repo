<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="LeaveStatement.aspx.cs" Inherits="Leave_LeaveStatement" Title="Leave Statement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="formStyle" style="height: 170px; width: 50%">
        <div id="formhead4">
            Leave Statement</div>
        <div class="iesEmp" style="margin-top: 40px;">
            <fieldset>
                <table>
                    <tr>
                        <td class="textlevel">
                            Organization :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlOffice" runat="server" CssClass="textlevelleft" Width="300px"
                                OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="ddlOffice"
                                ErrorMessage="*" Operator="NotEqual" ValueToCompare="99999"></asp:CompareValidator></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Employee :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="textlevelleft" Width="300px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlEmployee"
                                ErrorMessage="*" Operator="NotEqual" ValueToCompare="-1"></asp:CompareValidator></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            <asp:Button ID="btnPrint" runat="server" Text="Print Priview" OnClick="btnPrint_Click" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>
</asp:Content>
