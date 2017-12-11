<%@ Page Title="OT Adjustment" Language="C#" MasterPageFile="~/MasterBTMS.master"
    AutoEventWireup="true" CodeFile="OTAdjustment.aspx.cs" Inherits="Payroll_Payroll_OTAdjustment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js">
        //Date Time Picker script
    </script>
    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js">
    </script>
    <script language="javascript" type="text/javascript">
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }

        function ConfirmDelete() {
            var x = confirm("Are you sure you want to delete?");
            if (x)
                return true;
            else
                return false;
        }

        function GetOTAmount() {
            var OTApproveHr = document.getElementById('<%=txtOTApproveHr.ClientID %>');
            var BasicSalary = document.getElementById('<%=txtBasicSalary.ClientID %>');
            var OTAmount = document.getElementById('<%=txtOTAmount.ClientID %>');

            OTAmount.value = Math.round(((BasicSalary.value * 2 * OTApproveHr.value) / (22 * 8)), 0);
        }
    </script>
    <div class="empTrainForm">
        <div id="formhead1">
            <div style="width: 97%; float: left;">
                OT Adjustment</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="PayrollConfigInner">
            <div style="background-color: #EFF3FB; margin-bottom: 10px;">
                <fieldset>
                    <table>
                        <tr>
                            <td class="textlevel">
                                Emp Id :
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmpID" runat="server" MaxLength="20" onkeyup="ToUpper(this)"
                                    Width="80px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgBtnSearch" runat="server" Height="19px" ImageUrl="~/Images/Search_Icon.jpg"
                                    OnClick="imgBtnSearch_Click" ToolTip="Click on find button to retrieve any existing employee information." />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="ReqFieldVal" runat="server" ErrorMessage="*" ControlToValidate="txtEmpID"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td class="textlevel">
                                Name :
                            </td>
                            <td style="min-width: 100px">
                                <asp:Label ID="lblName" runat="server"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Designation :
                            </td>
                            <td>
                                <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                            </td>
                            <td class="textlevel">
                                Project/Dept :
                            </td>
                            <td>
                                <asp:Label ID="lblDept" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Location :
                            </td>
                            <td>
                                <asp:Label ID="lblLocation" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <fieldset style="margin-bottom: 10px;">
                <table>
                    <tr>
                        <td class="textlevel">
                            Month :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" runat="server" Width="85px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                            Year :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlYear" runat="server" Width="85px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            OT Hour :
                        </td>
                        <td>
                            <asp:TextBox ID="txtOTHour" runat="server" Width="80px"></asp:TextBox>
                        </td>
                        <td class="textlevel">
                            OT Approve Hour :
                        </td>
                        <td>
                            <asp:TextBox ID="txtOTApproveHr" onkeyup="GetOTAmount()" runat="server" AutoCompleteType="Disabled"
                                Width="80px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="fltbPayAmnt" runat="server" TargetControlID="txtOTApproveHr"
                                FilterType="Custom,Numbers" ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Basic :
                        </td>
                        <td>
                            <asp:TextBox ID="txtBasicSalary" ReadOnly="true" runat="server" Width="80px"></asp:TextBox>
                        </td>
                        <td class="textlevel">
                            OT Amount/Hour :
                        </td>
                        <td>
                            <asp:TextBox ID="txtOTAmtHr" runat="server" Width="80px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            OT Amount :
                        </td>
                        <td>
                            <asp:TextBox ID="txtOTAmount" runat="server" Width="80px"></asp:TextBox>
                        </td>
                        <td class="textlevel">
                            Entry Date :
                        </td>
                        <td>
                            <asp:TextBox ID="txtEntryDate" runat="server" Width="80px" ToolTip="Input the from date of temporary duty."></asp:TextBox>
                            <a href="javascript:NewCal('<%= txtEntryDate.ClientID %>','ddmmyyyy')">
                                <img style="border: 0px;" height="16" alt="Pick a date" src="../../images/cal.gif"
                                    width="16" /></a>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEntryDate"
                                CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
                <div>
                    <div style="width: 45%; float: left;">
                        <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" Text="Refresh"
                            OnClick="btnRefresh_Click" />
                    </div>
                    <div style="width: 45%; float: right; text-align: right;">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClientClick="return ConfirmDelete();"
                            OnClick="btnDelete_Click" />
                    </div>
                </div>
                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                <asp:HiddenField ID="hfID" runat="server" />
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom,Numbers"
                    ValidChars="0123456789." TargetControlID="txtOTHour">
                </cc1:FilteredTextBoxExtender>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom,Numbers"
                    ValidChars="0123456789." TargetControlID="txtOTAmtHr">
                </cc1:FilteredTextBoxExtender>
            </fieldset>
            <fieldset style="margin-bottom: 10px;">
                <legend>List</legend>
                <div style="overflow: scroll; width: 100%; height: 250px">
                    <asp:GridView ID="grList" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                        AutoGenerateColumns="False" DataKeyNames="TransId" OnRowCommand="grList_RowCommand">
                        <HeaderStyle BackColor="#B3CDE4" HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                <ItemStyle Width="5%" CssClass="ItemStylecss" />
                            </asp:ButtonField>
                            <asp:ButtonField CommandName="RowDeleting" HeaderText="Delete" Text="Delete">
                                <ItemStyle Width="5%" CssClass="ItemStylecss" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="VMonth" HeaderText="Month">
                                <ItemStyle CssClass="ItemStylecssCenter" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="VYear" HeaderText="Year">
                                <ItemStyle CssClass="ItemStylecssCenter" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="OTHour" HeaderText="OTHour">
                                <ItemStyle CssClass="ItemStylecssCenter" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="OTAppHour" HeaderText="OTAppHour">
                                <ItemStyle CssClass="ItemStylecssCenter" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="BasicSalary" HeaderText="BasicSalary">
                                <ItemStyle CssClass="ItemStylecssRight" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="OTAmount" HeaderText="OTAmount">
                                <ItemStyle CssClass="ItemStylecssRight" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="EntryDate" HeaderText="EntryDate">
                                <ItemStyle CssClass="ItemStylecssCenter" Width="10%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
    </div>
    <br />
    <br />
</asp:Content>
