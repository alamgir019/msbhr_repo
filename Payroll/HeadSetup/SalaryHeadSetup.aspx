<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="SalaryHeadSetup.aspx.cs" Inherits="Payroll_HeadSetup_SalaryHeadSetup"
    Title="Salary Items Setup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js"></script>
    <div id="PayrollConfigForm">
        <div id="formhead1">
            Salary Items Setup
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="PayrollConfigInner">
            <fieldset>
                <legend>Salary Items</legend>
                <table>
                    <tr>
                        <td class="textlevel">
                            Item Title :
                        </td>
                        <td>
                            <asp:TextBox ID="txtHeadTitle" runat="server" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="ReqValHeadTitle" runat="server" ControlToValidate="txtHeadTitle"
                                ErrorMessage="*">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsBasic" CssClass="textlevelleft" runat="server" Text="Is Basic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Short Title :
                        </td>
                        <td>
                            <asp:TextBox ID="txtShortTitle" runat="server" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsHouseRent" runat="server" CssClass="textlevelleft" 
                                Text="Is House Rent" />
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Item Type :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlHeadType" runat="server" Width="200px" CssClass="textlevelleft">
                                <asp:ListItem Value="1">Additive</asp:ListItem>
                                <asp:ListItem Value="-1">Deductive</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsMedical" runat="server" CssClass="textlevelleft" 
                                Text="Is Medical" />
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Item Category :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlItemCategory" runat="server" Width="200px" CssClass="textlevelleft">
                                <asp:ListItem Value="S">Salary</asp:ListItem>
                                <asp:ListItem Value="V">Variable</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>                        
                        </td>                            
                        <td>
                            <asp:CheckBox ID="chkPF" runat="server" CssClass="textlevelleft" Text="Is PF Deduction" />
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Natural Code :
                        </td>
                        <td>
                            <asp:TextBox ID="txtNaturalCode" runat="server" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        <asp:CheckBox ID="chkInActive" CssClass="textlevelleft" runat="server" Text="Make Inactive" />
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td class="textlevel" style="vertical-align: top;">
                            Description :
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfID" runat="server" />
                <asp:HiddenField ID="hfIsUpdate" runat="server" />
            </fieldset>
            <fieldset>
                <legend>Salary Items List</legend>
                <div class="GridFormat1">
                    <asp:GridView ID="grSalaryHead" runat="server" DataKeyNames="SHEADID,HEADNATURE,ISBASIC,ISPF,SHORTNAME,NATURALCODE,ITEMCATEGORY,ISHOUSERENT,ISMEDICAL"
                        AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" Width="99%"
                        OnRowCommand="grSalaryHead_RowCommand">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                <ItemStyle Width="5%" CssClass="ItemStylecss" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="HEADNAME" HeaderText="Item Title">
                                <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SHORTNAME" HeaderText="Short Name">
                                <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="HEADNATURE" HeaderText="Item Type">
                                <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SHDESC" HeaderText="Description">
                                <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="IsActive" HeaderText="Is Active">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ITEMCATEGORY" HeaderText="Category">
                                <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </fieldset>
            <div id="DivCommand1" style="padding-top: 3px;">
                <div style="text-align: left; float: left">
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px" CausesValidation="False"
                        OnClick="btnRefresh_Click" />
                </div>
                <div style="text-align: right;">
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" UseSubmitBehavior="False"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                        OnClick="btnDelete_Click" />
                </div>
            </div>
        </div>
    </div>
    <br />
</asp:Content>
