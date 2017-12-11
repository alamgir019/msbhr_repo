<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="Department.aspx.cs" Inherits="DepartmentSetup" Title="Department Setup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../JScripts/jquery-1.2.3.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JScripts/ui.datepicker.js"></script>
    <script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js">
        //Date Time Picker script
    </script>
    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>
    <div class="departmentSetup">
        <div id='formhead1'>
            <div style="width: 96%; float: left;">
                Department Setup</div>
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
                        <td class="textlevel">
                            Dept Code :
                        </td>
                        <td>
                            <asp:TextBox ID="txtDeptCode" runat="server" Width="100px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDeptCode"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Dept Name :
                        </td>
                        <td>
                            <asp:TextBox ID="txtDept" runat="server" Width="315px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDept"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td class="textlevel">
                            Valid From :
                        </td>
                        <td>
                            <asp:TextBox ID="txtValidFrom" runat="server" Width="80px">
                            </asp:TextBox>
                            <a href="javascript:NewCal('<%= txtValidFrom.ClientID %>','ddmmyyyy')">
                                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                    height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a>
                        </td>
                        <td class="textlevel">
                            Valid To :
                        </td>
                        <td>
                            <asp:TextBox ID="txtValidTo" runat="server" Width="80px">
                            </asp:TextBox>
                            <a href="javascript:NewCal('<%= txtValidTo.ClientID %>','ddmmyyyy')">
                                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                    height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsActive" runat="server" CssClass="textlevelleft" Text="Make Inactive">
                            </asp:CheckBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfIsUpadate" runat="server" />
            </fieldset>
        </div>
        <div class="GridFormat1">
            <!--Grid view Code starts for Second TAB -->
            <asp:GridView ID="grDepartment" runat="server" AutoGenerateColumns="False" DataKeyNames="DeptID,IsActive"
                EmptyDataText="No Record Found" Font-Size="9px" Width="99%" OnRowCommand="grDepartment_RowCommand">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                <AlternatingRowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                        <ItemStyle CssClass="ItemStylecss" Width="5%" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="DeptCode" HeaderText="Dept Code">
                        <ItemStyle CssClass="ItemStylecss" Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DeptName" HeaderText="Dept Name">
                        <ItemStyle CssClass="ItemStylecss" Width="40%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ValidFrom" HeaderText="Valid From">
                        <ItemStyle CssClass="ItemStylecss" Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ValidTo" HeaderText="Valid To">
                        <ItemStyle CssClass="ItemStylecss" Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="IsActive" HeaderText="Active">
                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="DivCommand1">
            <div class="DivCommandL">
                <asp:Button ID="btnClear" runat="server" Text="Refresh" Width="70px" OnClick="btnClear_Click"
                    UseSubmitBehavior="False" CausesValidation="false" />
            </div>
            <div class="DivCommandR">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                    OnClick="btnDelete_Click" CausesValidation="false" />
            </div>
        </div>
    </div>
</asp:Content>
