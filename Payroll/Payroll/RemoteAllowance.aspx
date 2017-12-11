<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="RemoteAllowance.aspx.cs" Inherits="Payroll_Payroll_RemoteAllowance"
    Title="Remote Allowance" %>

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
    </script>
    <div class="empTrainForm">
        <div id="formhead1">
            <div style="width: 97%; float: left;">
                Remote Allowance</div>
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
                            Emp Code :
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmpID" runat="server" MaxLength="20" onkeyup="ToUpper(this)"
                                Width="80px" ToolTip="Enter the Emp. Code"></asp:TextBox>
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
                            Sector :</td>
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
                            Basic :
                        </td>
                        <td>
                                <asp:Label ID="lblBasic" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <fieldset style="margin-bottom: 10px;">
            <legend>Remote Allowance Details</legend>
            <table>
                <tr>
                    <td class="textlevel">
                        Division :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPostingDiv" runat="server" Width="200px" CssClass="textlevelleft"
                            ToolTip="Select the name of project/department of the employee.">
                        </asp:DropDownList>
                    </td>
                    <td class="textlevel">
                        District :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSalaryLoc" runat="server" Width="200px" CssClass="textlevelleft"
                            ToolTip="Select the name of project/department of the employee.">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="textlevel">
                        Place of Posting :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPlaceofPosting" runat="server" Width="200px" CssClass="textlevelleft"
                            ToolTip="Select the location of office of the employee.">
                        </asp:DropDownList>
                    </td>
                    <td class="textlevel">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="textlevel">
                        Strating Date :
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartDate" runat="server" Width="80px" 
                            ToolTip="Input the from date of temporary duty."></asp:TextBox>
                        <a href="javascript:NewCal('<%= txtStartDate.ClientID %>','ddmmyyyy')">
                            <img style="border: 0px;" height="16" alt="Pick a date" src="../../images/cal.gif"
                                width="16" /></a>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtStartDate"
                            CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="textlevel">
                        Ending Date :
                    </td>
                    <td>
                        <asp:TextBox ID="txtEndDate" runat="server" Width="80px" 
                            ToolTip="Input the to date of temporary duty."></asp:TextBox>
                        <a href="javascript:NewCal('<%= txtEndDate.ClientID %>','ddmmyyyy')">
                            <img style="border: 0px;" height="16" alt="Pick a date" src="../../images/cal.gif"
                                width="16" /></a>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtEndDate"
                            CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="textlevel">
                        Percentage :
                    </td>
                    <td>
                        <asp:TextBox ID="txtPercentage" runat="server" MaxLength="20" Width="80px"></asp:TextBox>
                    </td>
                    <td class="textlevel">
                        Amount :
                    </td>
                    <td>
                        <asp:TextBox ID="txtAmount" runat="server" MaxLength="20" Width="80px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="textlevel">
                        Remarks :
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="503px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                </tr>
            </table>
            <asp:HiddenField ID="hfIsUpdate" runat="server" />
            <asp:HiddenField ID="hfID" runat="server" />
        </fieldset>
        <fieldset style="margin-bottom: 10px;">
            <legend>Remote Allowance List</legend>
            <div style="overflow: scroll; width: 100%; height: 250px">
                <asp:GridView ID="grList" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                    AutoGenerateColumns="False" 
                    DataKeyNames="AllowanceId,PostingDivID,SalLocId,PostingPlaceId" 
                    onrowcommand="grList_RowCommand">
                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                    </SelectedRowStyle>
                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                    <Columns>
                        <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                            <ItemStyle Width="5%" CssClass="ItemStylecss" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="EmpId" HeaderText="Emp Id">
                            <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PostingDivName" HeaderText="Posting Division">
                            <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="SalLocName" HeaderText="Salary Location">
                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PostingPlaceName" HeaderText="Posting Place">
                            <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="DateFrom" HeaderText="Date From">
                            <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="DateTo" HeaderText="Date To">
                            <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="BasicSalary" HeaderText="Basic Salary">
                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Percentage" HeaderText="Percentage">
                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Amount" HeaderText="Amount">
                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Remarks" HeaderText="Remarks">
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
                Width="70px" OnClick="btnRefresh_Click" ToolTip="Click this button to clear all fields." />
        </div>
        <div class="DivCommandR">
            <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click"
                ToolTip="Click this button to store the information after providing all necessary fields." />
        </div>
    </div>
    </div>
    <br />
    <br />
</asp:Content>
