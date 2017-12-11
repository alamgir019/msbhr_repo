<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="ITStatement.aspx.cs" Inherits="Payroll_Payroll_ITStatement" Title="IT Statement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="formStyle" style="height: 200px; width: 50%">
        <div id="formhead1">
        <div style="width: 96%; float: left;">
                IT Statement</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
            </div>
        <div style="margin-top: 10px; margin-left: 10px; margin-right: 10px;">
            <br />
            <fieldset>
                <br />
                <table>
                    <tr>
                        <td class="textlevel">
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdbEmpStatus" runat="server" Font-Names="Tahoma" Font-Size="11px"
                                ForeColor="Blue" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbEmpStatus_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="A">Active Employee</asp:ListItem>
                                <asp:ListItem Value="I">Inactive Employee</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Fiscal Year :</td>
                        <td>
                            <asp:DropDownList ID="ddlFiscalYear" CssClass="textlevelleft" runat="server" Width="250px"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlFiscalYear_SelectedIndexChanged">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Employee :</td>
                        <td>
                            <asp:DropDownList ID="ddlEmployee" CssClass="textlevelleft" runat="server" Width="250px">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Assess. Year :</td>
                        <td style="text-align: right;">
                            <asp:TextBox ID="txtAssessYear" runat="server" CssClass="textlevel" 
                                Height="19px" Width="242px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right;">
                            <asp:Button ID="dtnPrint" runat="server" Text="Print Preview" Width="100px" OnClick="dtnPrint_Click" /></td>
                    </tr>
                </table>
                <br />
            </fieldset>
        </div>
    </div>
</asp:Content>
