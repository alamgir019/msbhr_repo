<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="SpecialSkillSetup.aspx.cs" Inherits="EIS_SpecialSkillSetup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>

    <div class="departmentSetup">
        <div id='formhead1'>
            <div style="width: 96%; float: left;">
                Special Skill Setup</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <!--Div for group-->
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="departmentSetupInner">
            <fieldset>
                <!--Div for Controls-->
                <asp:HiddenField ID="hfID" runat="server" />
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblDept" runat="server" CssClass="textlevel" Text="Special Skill Name :"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtDept" runat="server" Width="309px"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDept"
                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsActive" runat="server" CssClass="textlevelleft" Text="Mark Inactive">
                            </asp:CheckBox></td>
                        <td>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfIsUpadate" runat="server" />
            </fieldset>
        </div>
        <div class="GridFormat1">
            <!--Grid view Code starts for Second TAB -->
            <asp:GridView ID="grDepartment" runat="server" AutoGenerateColumns="False" DataKeyNames="SpecSkillId,IsActive"
                EmptyDataText="No Record Found" Font-Size="9px" Width="99%" OnRowCommand="grDepartment_RowCommand">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                <AlternatingRowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="SpecSkillName" HeaderText="Special Skill Name">
                        <ItemStyle CssClass="ItemStylecss" Width="80%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="IsActive" HeaderText="Active">
                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <!--Grid view Code Ends-->
        </div>
        <div class="DivCommand1">
            <div class="DivCommandL">
                <asp:Button ID="btnClear" runat="server" Text="Refresh" Width="70px" OnClick="btnClear_Click"
                    UseSubmitBehavior="False" CausesValidation="false" />
            </div>
            <div class="DivCommandR">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                    OnClick="btnDelete_Click" />
            </div>
        </div>
    </div>
</asp:Content>
