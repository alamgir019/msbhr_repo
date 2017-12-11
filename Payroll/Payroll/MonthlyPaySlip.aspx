<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="MonthlyPaySlip.aspx.cs" Inherits="Payroll_Payroll_MonthlyPaySlip" Title="Monthly Payslip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="formStyle" style="height: 180px; width: 50%">
        <div id="formhead1">
            <div style="width: 96%; float: left;">
                Monthly Payslip</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div style="margin-top: 10px; margin-left: 10px; margin-right: 10px;">
            <fieldset>
                <table>
                    <tr>
                        <td class="textlevel">
                            Payroll Month :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" runat="server" Width="120px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Payroll Year :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlYear" runat="server" Width="120px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdbEmpStatus" runat="server" Font-Names="Tahoma" Font-Size="11px"
                                ForeColor="Blue" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbEmpStatus_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="A">Active Employee</asp:ListItem>
                                <asp:ListItem Value="I">Inactive Employee</asp:ListItem>
                            </asp:RadioButtonList>
                            &nbsp;
                        </td>
                        <td>
                        </td>
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
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdbSalaryType" runat="server" Font-Names="Tahoma" Font-Size="11px"
                                ForeColor="Blue" RepeatDirection="Horizontal" AutoPostBack="false">
                                <asp:ListItem Selected="True" Value="S">Salary</asp:ListItem>
                                <asp:ListItem Value="B">Only Bonus</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <asp:Button ID="dtnPrint" runat="server" Text="Print Preview" Width="100px" OnClick="dtnPrint_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <br />
        </div>
    </div>
</asp:Content>
