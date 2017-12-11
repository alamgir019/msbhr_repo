<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="StaffSalaryAndIT.aspx.cs" Inherits="Payroll_Payroll_StaffSalaryAndIT"
    Title="Salary and Income Tax" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="formStyle" style="width: 70%">
        <div id="formhead1">
        <div style="width: 96%; float: left;">
                Staff Salary and Income Tax</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
            </div>
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div style="margin-top: 10px; width: 99%; float: left; text-align: center;">
            <div style="width: 48%; float: left; text-align: left; margin-left: 5px;">
                <fieldset>
                    <table>
                        <tr>
                            <td class="textlevel">
                                Fiscal Year :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFiscalYear" CssClass="textlevelleft" runat="server" Width="150px">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlMonth" CssClass="textlevelleft" runat="server" Width="100px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Location
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlLocation" CssClass="textlevelleft" runat="server" Width="250px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="right">
                                <asp:Button ID="btnPreview" runat="server" Text="Print Preview" Width="120px" OnClick="btnPreview_Click" /></td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <div style="width: 48%; float: left; text-align: left; margin-left: 5px;">
                <fieldset>
                    <legend>Select Salary Item</legend>
                    <br />
                    <asp:GridView ID="grSalItem" runat="server" DataKeyNames="SHEADID,HeadNature" AutoGenerateColumns="False"
                        EmptyDataText="No Record Found" Font-Size="9px" Width="92%">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemStyle CssClass="ItemStylecss" Width="5%" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkBox" runat="Server"></asp:CheckBox>
                                    <asp:HiddenField ID="hfSHEADID" Value='<%# Convert.ToString(Eval("SHEADID"))%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="HEADNAME" HeaderText="Item Title">
                                <ItemStyle CssClass="ItemStylecss" Width="50%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="" HeaderText="Type">
                                <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Seq. No">
                                <ItemStyle CssClass="ItemStylecss" Width="20%" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSeqNo" runat="server" Text="" Width="60px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fltbSeqNo" runat="server" TargetControlID="txtSeqNo"
                                        FilterType="Custom,Numbers" ValidChars="">
                                    </cc1:FilteredTextBoxExtender>
                                    <asp:HiddenField ID="hfDispType" Value="" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </fieldset>
            </div>
        </div>
    </div>
</asp:Content>
