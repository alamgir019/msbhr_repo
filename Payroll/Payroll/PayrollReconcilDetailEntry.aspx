<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="PayrollReconcilDetailEntry.aspx.cs" Inherits="Payroll_Payroll_PayrollReconcilDetailEntry"
    Title="PayrollReconcilDetailEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
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
    </script>
    <div class="empTrainForm">
        <div id="formhead2">
            <div style="width: 97%; float: left;">
                Payroll Reconciliation Detail Entry</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="PayrollConfigInner">
            <div style="background-color: #EFF3FB; margin-bottom: 10px">
                <fieldset>
                    <table>
                        <tr>
                            <td class="textlevel">
                                Emp Id :
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmpID" runat="server" MaxLength="20" onkeyup="ToUpper(this)"
                                    Width="80px" ToolTip="Enter the Emp. Code and click on Find Button. Details of the find employee will be shown in employee general information screen if information exists. In order to enter employee."></asp:TextBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgBtnSearch" runat="server" Height="19px" ImageUrl="~/Images/Search_Icon.jpg"
                                    OnClick="imgBtnSearch_Click" ToolTip="Enter the Emp. Code and click on Find Button. Details of the find employee will be shown in employee general information screen if information exists. In order to enter employee." />
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
                            <td>
                                <asp:Label ID="lblName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Designation :
                            </td>
                            <td>
                                <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Sector :
                            </td>
                            <td>
                                <asp:Label ID="lblSector" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel" style="height: 16px">
                                Department :
                            </td>
                            <td style="height: 16px">
                                <asp:Label ID="lblDept" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Entry Date :
                            </td>
                            <td>
                                <asp:TextBox ID="txtEntryDate" runat="server" ToolTip="Input the confirmation date."
                                    Width="80px"></asp:TextBox>
                                <a href="javascript:NewCal('<%= txtEntryDate.ClientID %>','ddmmyyyy')">
                                    <img style="border: 0px;" height="16" alt="Pick a date" src="../../images/cal.gif"
                                        width="16" /></a>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                    ControlToValidate="txtEntryDate" CssClass="validator" ErrorMessage="Invalid Date"
                                    ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Date of Joining :
                            </td>
                            <td>
                                <asp:Label ID="lblJoinDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Basic Salary :
                            </td>
                            <td>
                                <asp:Label ID="lblBasicSalary" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <fieldset style="margin-bottom: 10px;">
                <legend>Reconciliation Detail</legend>
                <table>
                    <tr>
                        <td class="textlevel">
                            Month :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" runat="server" Width="85px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
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
                            Basic Salary
                        </td>
                        <td>
                            <asp:TextBox ID="txtBasicSal" runat="server" CssClass="TextBoxAmt60" MaxLength="20"
                                Width="80px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtBasicSal_FilteredTextBoxExtender" runat="server"
                                FilterType="Custom,Numbers" TargetControlID="txtBasicSal" ValidChars="0123456789.">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Allowance :
                        </td>
                        <td>
                            <asp:TextBox ID="txtAllowance" runat="server" CssClass="TextBoxAmt60" MaxLength="20"
                                Width="80px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtAllowance_FilteredTextBoxExtender" runat="server"
                                FilterType="Custom,Numbers" TargetControlID="txtAllowance" ValidChars="0123456789.">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Reason :
                        </td>
                        <td>
                            <asp:TextBox ID="txtReason" runat="server" MaxLength="200" Width="563px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Separation Date :
                        </td>
                        <td>
                            <asp:TextBox ID="txtSperationDate" runat="server" ToolTip="Input the confirmation date."
                                Width="80px"></asp:TextBox>
                            <a href="javascript:NewCal('<%= txtSperationDate.ClientID %>','ddmmyyyy')">
                                <img style="border: 0px;" height="16" alt="Pick a date" src="../../images/cal.gif"
                                    width="16" /></a>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                ControlToValidate="txtSperationDate" CssClass="validator" ErrorMessage="Invalid Date"
                                ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                <asp:HiddenField ID="hfId" runat="server" />                
            </fieldset>
            <fieldset>
                <legend>Reconciliation Detail List</legend>
                <div style="overflow: scroll; width: 100%; height: 250px">
                    <asp:GridView ID="grList" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                        AutoGenerateColumns="False" DataKeyNames="ReconcilId">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                             <asp:BoundField DataField="VMonth" HeaderText="Month">
                                <ItemStyle CssClass="ItemStylecssRight" Width="8%"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="VYear" HeaderText="Year">
                                <ItemStyle CssClass="ItemStylecssRight" Width="8%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="BasicSalary" HeaderText="Basic Salary">
                                <ItemStyle CssClass="ItemStylecssRight" Width="8%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Allowance" HeaderText="Allowance">
                                <ItemStyle CssClass="ItemStylecssCenter" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Reason" HeaderText="Reason">
                                <ItemStyle CssClass="ItemStylecssCenter" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SeparationDate" HeaderText="Separation Date">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
        <div class="DivCommand1" style="width: 98%;">
            <div class="DivCommandL">
                <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" Text="Refresh"
                    Width="70px" OnClick="btnRefresh_Click" ToolTip="Click on Save Button to store the employee data." />
            </div>
            <div class="DivCommandR">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click"
                    ToolTip="Click on Save Button to store the employee data." />
            </div>
        </div>
    </div>
</asp:Content>
