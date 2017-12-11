<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="LWPDeduction.aspx.cs" Inherits="Payroll_Payroll_LWPDeduction"
    Title="LWP Deduction" %>

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
                LWP Deduction</div>
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
                                Name :</td>
                            <td>
                                <asp:Label ID="lblName" runat="server"></asp:Label></td>
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
                                <asp:Label ID="lblSector" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="textlevel" style="height: 16px">
                                Department :</td>
                            <td style="height: 16px">
                                <asp:Label ID="lblDept" runat="server"></asp:Label>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="textlevel">
                                Entry Date :</td>
                            <td>
                                <asp:TextBox ID="txtEntryDate" runat="server" ToolTip="Input the confirmation date."
                                    Width="80px"></asp:TextBox>
                                <a href="javascript:NewCal('<%= txtEntryDate.ClientID %>','ddmmyyyy')">
                                    <img style="border: 0px;" height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtEntryDate"
                                    CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                                </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Date of Joining :</td>
                            <td>
                                <asp:Label ID="lblJoinDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Gross Salary :</td>
                            <td>
                                <asp:Label ID="lblGrossSalary" runat="server"></asp:Label>
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
                        Month :
                    </td>
                    <td>
                        &nbsp;<asp:DropDownList ID="ddlMonth" runat="server" Width="80px" 
                                        CssClass="textlevelleft">
                                    </asp:DropDownList>
                    </td>
                    <td class="textlevel">
                        Year :</td>
                    <td>
                                    <asp:DropDownList ID="ddlYear" runat="server" Width="130px" 
                                        CssClass="textlevelleft">
                                    </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="textlevel">
                        Days :
                    </td>
                    <td>
                        <asp:TextBox ID="txtDays" runat="server" MaxLength="20" Width="80px" 
                            ontextchanged="txtDays_TextChanged"></asp:TextBox>
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
                    DataKeyNames="VId" 
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
                        <asp:BoundField DataField="VMonth" HeaderText="Month">
                            <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="VYear" HeaderText="Year">
                            <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                        </asp:BoundField>
                         <asp:BoundField DataField="VDays" HeaderText="LWP Days">
                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PayAmnt" HeaderText="Pay Amount">
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
