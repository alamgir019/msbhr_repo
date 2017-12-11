<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="BankSetup.aspx.cs" Inherits="Payroll_Banks_BankSetup" Title="Bank Setup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js"></script>

    <div id="PayrollConfigForm2" style="width: 80%">
        <div id="formhead1">
            Bank Setup</div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="PayrollConfigInner2">
            <div>
                <fieldset>
                    <table>
                        <tr>
                            <td class="textlevel" style="width: 150px">
                                Select Bank :</td>
                            <td>
                                <asp:DropDownList ID="ddlBank" runat="server" Width="357px" CssClass="textlevelleft">
                                </asp:DropDownList></td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel" style="width: 150px; height: 16px">
                                New Bank Code(Not in List) :</td>
                            <td style="height: 16px">
                                <asp:TextBox ID="txtBankCode" runat="server" Width="67px"></asp:TextBox></td>
                            <td style="height: 16px">
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel" style="width: 150px">
                                New Bank Name (Not In List) :</td>
                            <td>
                                <asp:TextBox ID="txtBankName" runat="server" Width="350px"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel" style="width: 150px">
                                Routing No :</td>
                            <td>
                                <asp:TextBox ID="txtBranchCode" runat="server" Width="96px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="ReqValHeadTitle" runat="server" ControlToValidate="txtBranchCode"
                                    ErrorMessage="*">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel" style="width: 150px">
                                Branch Name :</td>
                            <td>
                                <asp:TextBox ID="txtBranchName" runat="server" Width="350px"></asp:TextBox></td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBranchName"
                                    ErrorMessage="*">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel" style="width: 150px">
                                District :</td>
                            <td>
                                <asp:TextBox ID="txtDistrict" runat="server" Width="350px"></asp:TextBox></td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel" style="width: 150px">
                                DOS :</td>
                            <td>
                                <asp:DropDownList ID="ddlDOS" runat="server" Width="162px" CssClass="textlevelleft">
                                    <asp:ListItem Value="-1">Select</asp:ListItem>
                                    <asp:ListItem>DOS1-BCO</asp:ListItem>
                                    <asp:ListItem>DOS2-OSU</asp:ListItem>
                                </asp:DropDownList></td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="hfID" runat="server" />
                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                </fieldset>
            </div>
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
            <%--OnRowCommand="grSalaryHead_RowCommand"--%>
            <fieldset>
                <legend>Bank List</legend>
                <fieldset>
                    <table>
                        <tr>
                            <td class="textlevel" style="width: 150px">
                                Select Bank :</td>
                            <td>
                                <asp:DropDownList ID="ddlBankSearh" runat="server" Width="250px" CssClass="textlevelleft">
                                </asp:DropDownList></td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CausesValidation="False"
                                    OnClick="btnSearch_Click" /></td>
                        </tr>
                    </table>
                </fieldset>
                <asp:GridView ID="grBank" runat="server" DataKeyNames="SLID" AutoGenerateColumns="False"
                    EmptyDataText="No Record Found" Font-Size="9px" Width="99%" OnRowCommand="grBank_RowCommand">
                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                    </SelectedRowStyle>
                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                    <Columns>
                        <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                            <ItemStyle Width="5%" CssClass="ItemStylecss" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="BANKCODE" HeaderText="Bank Code">
                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="BANKNAME" HeaderText="Bank Name">
                            <ItemStyle CssClass="ItemStylecss" Width="25%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="BRANCHNAME" HeaderText="Branch Name">
                            <ItemStyle CssClass="ItemStylecss" Width="25%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ROUTINGNO" HeaderText="Routing No">
                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="DISTRICT" HeaderText="District">
                            <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="DOS" HeaderText="DOS">
                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </fieldset>
        </div>
    </div>
</asp:Content>
