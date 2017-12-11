<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="MonthlyBankStatement.aspx.cs" Inherits="Payroll_Payroll_MonthlyBankStatement"
    Title="Bank Instruction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="formStyle" style="height: 250px; width: 60%">
        <div id="formhead1">
            <div style="width: 96%; float: left;">
                Bank Instruction</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div style="margin-top: 10px; margin-left: 10px; margin-right: 10px;">
            <fieldset>
                <table>
                    <tr>
                        <td class="textlevel">
                            Select Report :</td>
                        <td>
                            <asp:RadioButtonList ID="rdbReportType" runat="server" CssClass="textlevelleft" Width="400px"
                                RepeatDirection="Horizontal" AutoPostBack="false">
                                <asp:ListItem Selected="True" Value="D">Bank Instruction (S2B Format)</asp:ListItem>
                                <asp:ListItem Value="S">Bank Instruction (BEFTN Format)</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Select Payment Type :</td>
                        <td>
                            <asp:RadioButtonList ID="rdbSalaryType" runat="server" CssClass="textlevelleft" Width="300px"
                                RepeatDirection="Horizontal" AutoPostBack="false">
                                <asp:ListItem Selected="True" Value="S">Salary</asp:ListItem>
                                <asp:ListItem Value="B">Only Bonus</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Payroll Month :</td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" runat="server" Width="120px" CssClass="textlevelleft">
                            </asp:DropDownList></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Payroll Year :</td>
                        <td>
                            <asp:DropDownList ID="ddlYear" runat="server" Width="120px" CssClass="textlevelleft">
                            </asp:DropDownList></td>
                        <td>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="textlevel">
                            Cost Center :</td>
                        <td>
                                    <asp:DropDownList ID="ddlClinic" runat="server" Width="300px"
                                        CssClass="textlevelleft">
                                    </asp:DropDownList>
                                </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    
                    <tr>
                        <td class="textlevel">
                            Bank :</td>
                        <td>
                            <asp:DropDownList ID="ddlBank" runat="server" Width="350px" CssClass="textlevelleft">
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
            </fieldset>
        </div>
    </div>
</asp:Content>
