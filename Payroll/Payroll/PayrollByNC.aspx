<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="PayrollByNC.aspx.cs" Inherits="Payroll_Payroll_PayrollByNC" Title="Payroll By NC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="formStyle" style="height: 240px; width: 50%">
        <div id="formhead4">
            Payroll By NC</div>
        <div style="margin-top: 10px;">
            <table>
                <tr>
                    <td class="textlevel">
                        Select Report :</td>
                    <td>
                        <asp:RadioButtonList ID="rdbReportType" runat="server" Font-Names="Tahoma" Font-Size="11px"
                            ForeColor="Blue" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbReportType_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="0">Month Wise</asp:ListItem>
                            <asp:ListItem Value="1">Fiscal Year Wise</asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="textlevel">
                        Select Payment Type :</td>
                    <td>
                        <asp:RadioButtonList ID="rdbSalaryType" runat="server" Font-Names="Tahoma" Font-Size="11px"
                            ForeColor="Blue" RepeatDirection="Horizontal" AutoPostBack="false">
                            <asp:ListItem Selected="True" Value="S">Salary</asp:ListItem>
                            <asp:ListItem Value="B">Only Bonus</asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="textlevel">
                        Fiscal Year :</td>
                    <td>
                        <asp:DropDownList ID="ddlFisYear" runat="server" Width="250px" CssClass="textlevel">
                        </asp:DropDownList></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="textlevel">
                        Payroll Month :</td>
                    <td>
                        <asp:DropDownList ID="ddlMonth" runat="server" Width="120px" CssClass="textlevel">
                        </asp:DropDownList></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="textlevel">
                        Payroll Year :</td>
                    <td>
                        <asp:DropDownList ID="ddlYear" runat="server" Width="120px" CssClass="textlevel">
                        </asp:DropDownList></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="textlevel">
                        Bank :</td>
                    <td>
                        <asp:DropDownList ID="ddlBank" runat="server" Width="250px" CssClass="textlevel">
                        </asp:DropDownList></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="dtnPrint" runat="server" Text="Print Preview" Width="100px" OnClick="dtnPrint_Click" /></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
