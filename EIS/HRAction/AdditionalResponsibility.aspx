<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="AdditionalResponsibility.aspx.cs" Inherits="EIS_HRAction_AdditionalResponsibility"
    Title="Employee Additional Responsibility" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js">
    //Date Time Picker script
    </script>

    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js">
    </script>

    <script language="javascript">
    function ToUpper(ctrl)
    {   
    var t = ctrl.value;
    ctrl.value = t.toUpperCase(); 
    }
    </script>

    <div class="empTrainForm">
        <div id="formhead2">
            <div style="width: 97%; float: left;">
                Employee Additional Responsibility</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div style="background-color: #EFF3FB; margin-bottom: 10px;">
            <fieldset>
                <table>
                    <tr>
                        <td class="textlevel">
                            Emp Id :</td>
                        <td>
                            <asp:TextBox ID="txtEmpID" Width="80px" runat="server" onkeyup="ToUpper(this)"></asp:TextBox>&nbsp;<asp:ImageButton
                                ID="imgBtnSearch" runat="server" Height="19px" ImageUrl="~/Images/Search_Icon.jpg"
                                OnClick="imgBtnSearch_Click" ToolTip="Enter the Emp. Code and click on Find Button. Details of the find employee will be shown in employee general information screen if information exists. In order to enter employee." />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmpID"
                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Name :</td>
                        <td>
                                <asp:Label ID="lblName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Designation :</td>
                        <td>
                                <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Project :</td>
                        <td>
                                <asp:Label ID="lblSector" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Department :
                        </td>
                        <td>
                                <asp:Label ID="lblDept" runat="server"></asp:Label>
                        </td>
                    </tr>                    
                    <tr>
                        <td class="textlevel">
                            Gross Salary :</td>
                        <td>
                                <asp:Label ID="lblBasicSalary" runat="server"></asp:Label>
                        </td>
                    </tr>                    
                    <tr>
                        <td class="textlevel">
                            Entry Date :</td>
                        <td>
                            <asp:TextBox ID="txtEntryDate" runat="server" Width="80px"></asp:TextBox>
                            <a href="javascript:NewCal('<%= txtEntryDate.ClientID %>','ddmmyyyy')">
                                <img style="border: 0px;" height="16" alt="Pick a date" src="../../images/cal.gif"
                                    width="16" /></a>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                ControlToValidate="txtEntryDate" CssClass="validator" ErrorMessage="Invalid Date"
                                ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                        </td>
                </tr>
                </table>
            </fieldset>
        </div>
        <div style="margin: 5px 10px 10px 10px;">
            <div class="Div920">
                <fieldset>
                    <legend>Responsibility Details</legend>
                    <table>
                        <tr>
                            <td class="textlevelleft" style="height: 15px">
                                Name of Action :</td>
                            <td class="textlevelleft" style="height: 15px">
                            <asp:DropDownList ID="ddlAction" runat="server" CssClass="textlevelleft" Width="300px"
                                ToolTip="Select the name of action for confirmation from the list.">
                            </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="textlevelleft" style="height: 15px">
                                Starting Date :</td>
                            <td class="textlevelleft" style="height: 15px">
                                <asp:TextBox ID="txtStartDate" runat="server" Width="80px"></asp:TextBox>
                                <a href="javascript:NewCal('<%= txtStartDate.ClientID %>','ddmmyyyy')">
                                    <img alt="Pick a date" height="16" src="../../images/cal.gif" style="border-right: 0px;
                                        border-top: 0px; border-left: 0px; border-bottom: 0px" width="16" /></a>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtStartDate"
                                    CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="textlevelleft" style="height: 15px">
                                Ending Date :</td>
                            <td class="textlevelleft" style="height: 15px">
                                <asp:TextBox ID="txtEndDate" runat="server" Width="80px"></asp:TextBox>
                                <a href="javascript:NewCal('<%= txtEndDate.ClientID %>','ddmmyyyy')">
                                    <img alt="Pick a date" height="16" src="../../images/cal.gif" style="border-right: 0px;
                                        border-top: 0px; border-left: 0px; border-bottom: 0px" width="16" /></a>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEndDate"
                                    CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="textlevelleft">
                                Percent :</td>
                            <td>
                                <asp:TextBox ID="txtPercent" runat="server" MaxLength="5" 
                                    CssClass="TextBoxAmt60"></asp:TextBox>&nbsp;<asp:Button 
                                    ID="btnCalculate" runat="server" Text="Calculate" Width="70px" 
                        CausesValidation="False" onclick="btnCalculate_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevelleft">
                                Amount :</td>
                            <td>
                                <asp:TextBox ID="txtAmount" runat="server" MaxLength="12" 
                                    CssClass="TextBoxAmt60"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="textlevelleft">
                                Responsibility :</td>
                            <td>
                                <asp:TextBox ID="txtResponsibility" runat="server" Width="290px" 
                                    MaxLength="200" TextMode="MultiLine"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="textlevelleft">
                            </td>
                            <td>
                                <asp:CheckBox ID="chkIsResponseAllowance" runat="server" 
                                    CssClass="textlevelleft" Text="Is Responsibility Allowance"
                                    Width="150px" />
                                <asp:CheckBox ID="chkIsRepeat" runat="server" CssClass="textlevelleft" 
                                    Text="Is Repeat" Checked="True" /></td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                    <cc1:FilteredTextBoxExtender ID="FilteredTxt1" runat="server" FilterType="Custom,Numbers"
                        TargetControlID="txtPercent" ValidChars="0123456789.">
                    </cc1:FilteredTextBoxExtender>
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom,Numbers"
                        TargetControlID="txtAmount" ValidChars="0123456789.">
                    </cc1:FilteredTextBoxExtender>
                </fieldset>
            <div class="DivCommand1">
                <div class="DivCommandL">
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px" 
                        CausesValidation="False" onclick="btnRefresh_Click" />
                </div>
                <div class="DivCommandR">
                                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" 
                        UseSubmitBehavior="False" onclick="btnSave_Click" /></div>
            </div>
            </div>
            <div id="empEduDiv02" style="height: 200px;">
                <asp:GridView ID="grAddResponsibility" runat="server" 
                    AutoGenerateColumns="False" DataKeyNames="AddResponseId,EmpId"
                    EmptyDataText="No Record Found" Font-Size="9px" Width="99%" 
                    onrowcommand="grAddResponsibility_RowCommand">
                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                    <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                    <AlternatingRowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                            <ItemStyle CssClass="ItemStylecss" Width="5%" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="EntryDate" HeaderText="Entry Date">
                            <ItemStyle CssClass="ItemStylecss" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="StartDate" HeaderText="Start Date">
                            <ItemStyle CssClass="ItemStylecss" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EndDate" HeaderText="End Date">
                            <ItemStyle CssClass="ItemStylecss" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PercentAmt" HeaderText="Percent">
                            <ItemStyle CssClass="ItemStylecssRight" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Amount" HeaderText="Amount">
                            <ItemStyle CssClass="ItemStylecssRight" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Responsibility" HeaderText="Responsibility">
                            <ItemStyle CssClass="ItemStylecss" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IsResponsibleAllowance" HeaderText="Is Responsible Allowance">
                            <ItemStyle CssClass="ItemStylecssCenter" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IsRepeat" HeaderText="Is Repeat">
                            <ItemStyle CssClass="ItemStylecssCenter" Width="10%" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            
        </div>
    </div>
</asp:Content>
