<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="LeaveStatementHistory.aspx.cs" Inherits="Leave_LeaveStatementHistory"
    Title="Leave Statement History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="formStyle" style="height: 200px; width: 50%">
        <div id="formhead4">
            Leave Statement History</div>
        <div class="iesEmp" style="margin-top: 40px;">
            <table>
                <tr>
                    <td class="textlevel">
                        Office :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOffice" runat="server" Width="300px" CssClass="textlevelleft" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged"
                            AutoPostBack="true">
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
                        <asp:DropDownList ID="ddlEmployee" runat="server" Width="300px" CssClass="textlevelleft">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlEmployee"
                            ErrorMessage="*" Operator="NotEqual" ValueToCompare="-1"></asp:CompareValidator></td>
                </tr>
                <tr>
                    <td class="textlevel">
                        Leave Period :</td>
                    <td>
                        <asp:DropDownList ID="ddlLeavePeriod" runat="server" Width="300px" CssClass="textlevelleft">
                        </asp:DropDownList></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnPrint" runat="server" Text="Print Priview" OnClick="btnPrint_Click" />
                    </td>
                    <td style="width: 3px">
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
