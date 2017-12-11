<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="SalaryHeadBlock.aspx.cs" Inherits="Payroll_SalaryHeadBlock" Title="Salary Head Block" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../JScripts/jquery-1.2.3.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JScripts/ui.datepicker.js"></script>
    <script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js">
        //Date Time Picker script
    </script>
    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
        //Delete Confirmation Message
    </script>
    <script language="javascript" type="text/javascript">
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }
    </script>
    <div class="officeSetup">
        <div id="formhead1">
            <div style="width: 96%; float: left;">
                Salary Head Block</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <!--Div for group-->
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="officeSetupInner">
            <fieldset>
                <!--Div for Controls-->
                <asp:HiddenField ID="hfID" runat="server" />
                <asp:HiddenField ID="hfIsUpadate" runat="server" />
                <table>
                    <tr>
                        <td class="textlevel">
                            Employee :
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlSupervisor" runat="server" Width="335px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Salary Head :
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlSalHead" runat="server" Width="335px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            From Date :
                        </td>
                        <td>
                            <asp:TextBox ID="txtFromDate" runat="server" Width="89px">
                            </asp:TextBox>
                            <a href="javascript:NewCal('<%= txtFromDate.ClientID %>','ddmmyyyy')">
                                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                    height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a>
                        </td>
                        <td class="textlevel">
                            To Date :
                        </td>
                        <td>
                            <asp:TextBox ID="txtToDate" runat="server" Width="89px">
                            </asp:TextBox>
                            <a href="javascript:NewCal('<%= txtToDate.ClientID %>','ddmmyyyy')">
                                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                    height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Block Amount :
                        </td>
                        <td>
                            <asp:TextBox ID="txtBlockAmt" runat="server" Width="89px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBlockAmt"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                FilterType="Custom,Numbers" TargetControlID="txtBlockAmt" ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div class="GridFormat1">
            <!--Grid view Code starts-->
            <asp:GridView ID="grSHeadBlock" runat="server" DataKeyNames="TransId,SHeadId" AutoGenerateColumns="False"
                EmptyDataText="No Record Found" Font-Size="9px" OnRowCommand="grDivision_RowCommand"
                Width="99%">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                </SelectedRowStyle>
                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                <Columns>
                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                        <ItemStyle CssClass="ItemStylecss" Width="5%" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="EmpId" HeaderText="Emp Id">
                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="FullName" HeaderText="Name">
                        <ItemStyle CssClass="ItemStylecss" Width="35%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="HEADNAME" HeaderText="Head Name">
                        <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="FromDate" HeaderText="From">
                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="ToDate" HeaderText="To">
                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="BlockAmt" HeaderText="Amount">
                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
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
