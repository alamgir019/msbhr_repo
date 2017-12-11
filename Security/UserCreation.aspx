<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="UserCreation.aspx.cs" Inherits="UserCreationSetup" Title="User Creation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>
    <script language="javascript">
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }
    </script>
    <div class="EmpLeaveProfStyle">
        <div id="formhead5">
            <div style="width: 97%; float: left;">
                User Creation</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <!--Div for group-->
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="iesEmp">
            <fieldset>
                <!--Div for Controls-->
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblDept" runat="server" CssClass="textlevel" Text="User Id :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserId" runat="server" Width="205px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserId"
                                ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Employee Id :" CssClass="textlevel"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmpId" runat="server" Width="205px" OnTextChanged="txtEmpId_TextChanged"
                                    AutoPostBack="True"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtEmpId">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Employee Name :" CssClass="textlevel"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUserName" runat="server" Width="205px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator"
                                    ControlToValidate="txtUserName">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <asp:HiddenField ID="hfDivision" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hfSBU" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hfDept" runat="server"></asp:HiddenField>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" CssClass="textlevel" Text="Password :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPass" runat="server" Width="205px" TextMode="Password"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtUserId"
                                ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" CssClass="textlevel" Text="Confirm Password :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtConfirmPass" runat="server" Width="205px" TextMode="Password"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtUserId"
                                ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" CssClass="textlevel" Text="Permission Package :"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPrivs" runat="server" Width="210px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlPrivs"
                                ErrorMessage="*" Operator="NotEqual" ValueToCompare="-1"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsActive" runat="server" Width="145px" CssClass="textlevelleft"
                                Text="Make The Account Disable"></asp:CheckBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsAdmin" runat="server" Width="74px" CssClass="textlevelleft"
                                Text="Admin User"></asp:CheckBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfIsUpadate" runat="server" />
                <asp:HiddenField ID="hfID" runat="server" />
            </fieldset>
        </div>
        <div class="Grid650">
            <!--Grid view Code starts for First TAB-->
            <asp:GridView ID="grUser" runat="server" AutoGenerateColumns="False" DataKeyNames="UserId,FullName,AccountDisabled,EmpId,IsAdmin,DeptId,PrivPackID"
                EmptyDataText="No Record Found" Font-Size="9px" Width="100%" OnRowCommand="grUser_RowCommand">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                <AlternatingRowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:ButtonField CommandName="OnClick" HeaderText="Edit" Text="Edit">
                        <ItemStyle CssClass="ItemStylecss" Width="5%" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="UserId" HeaderText="User Id">
                        <ItemStyle CssClass="ItemStylecss" Width="5%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FullName" HeaderText="Emp Name">
                        <ItemStyle CssClass="ItemStylecss" Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="EmpId" HeaderText="Emp Id">
                        <ItemStyle CssClass="ItemStylecss" Width="5%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AccountDisabled" HeaderText="Is Active">
                        <ItemStyle CssClass="ItemStylecssCenter" Width="5%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="IsAdmin" HeaderText="Sys. Admin">
                        <ItemStyle CssClass="ItemStylecssCenter" Width="5%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DivisionName" HeaderText="Division Name">
                        <ItemStyle CssClass="ItemStylecss" Width="12%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ProjectName" HeaderText="Project Name">
                        <ItemStyle CssClass="ItemStylecss" Width="12%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SalLocName" HeaderText="Location">
                        <ItemStyle CssClass="ItemStylecss" Width="12%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DeptName" HeaderText="Dept Name">
                        <ItemStyle CssClass="ItemStylecss" Width="12%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="JobTitleName" HeaderText="Job Title">
                        <ItemStyle CssClass="ItemStylecss" Width="12%" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <!--Grid view Code Ends-->
        </div>
        <div id="DivCommand1">
            <table style="width: 100%; padding-top: 2px; padding-right: 15px;">
                <tr>
                    <td align="left">
                        <asp:Button ID="btnClear" runat="server" CausesValidation="false" Text="Refresh"
                            Width="70px" OnClick="btnClear_Click" UseSubmitBehavior="False" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                            OnClick="btnDelete_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
        FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters" ValidChars="-"
        TargetControlID="txtUserId">
    </cc1:FilteredTextBoxExtender>
</asp:Content>
