<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="SalaryLocationSetup.aspx.cs" Inherits="EIS_SalaryLocationSetup" Title="Salary Location Setup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="MaiContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="setup">
        <div id='formhead1'>
            <div style="width: 96%; float: left;">
                Location Setup</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:HiddenField ID="hfIsUpadate" runat="server" />
        <asp:HiddenField ID="hfID" runat="server" />
        <div class="setupInner">
            <fieldset>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" CssClass="textlevel" Text="Salary Location Name :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDesigation" runat="server" Width="309px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDesigation"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            &nbsp;</td>
                        <td>
                            <asp:DropDownList ID="ddlCostCenterCode" runat="server" CssClass="textlevelleft"
                                ToolTip="Select an Action from this list. You have to select an Action for storing records. "
                                Width="205px" AutoPostBack="True" Visible="False">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="ChkIsActive" Text="Make Inactive" CssClass="textlevelleft" runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <div style="margin-left: 10px; margin-right: 10px; margin-top: 10px;">
                    <div style="width: 100%; height: 240px; overflow: scroll;">
                        <asp:GridView ID="grLocation" runat="server" DataKeyNames="SalLocId,CostCenterId"
                            AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" OnRowCommand="grLocation_RowCommand"
                            Width="99%" OnSelectedIndexChanged="grLocation_SelectedIndexChanged">
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                            <Columns>
                                <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="SalLocName" HeaderText="Location Name">
                                    <ItemStyle CssClass="ItemStylecss" Width="60%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IsActive" HeaderText="Is Active">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                            </SelectedRowStyle>
                        </asp:GridView>
                    </div>
            </fieldset>
        </div>
        <div class="DivCommand1">
            <div class="DivCommandL">
                <asp:Button ID="btnClear" runat="server" Text="Refresh" Width="70px" OnClick="btnClear_Click"
                    UseSubmitBehavior="False" />
            </div>
            <div class="DivCommandR">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                    OnClick="btnDelete_Click" />
            </div>
        </div>
    </div>
    </div>
</asp:Content>
