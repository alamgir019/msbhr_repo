<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="ChildEducationAllowance.aspx.cs" Inherits="Payroll_Payroll_ChildEducationAllowance"
    Title="Child Education Allowance" %>

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
                Child Education Allowance</div>
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
                                Sector:
                            </td>
                            <td>
                                <asp:Label ID="lblSector" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Department :</td>
                            <td>
                                <asp:Label ID="lblDept" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <fieldset style="margin-bottom: 10px;">
                <legend>Child Education Allowance Details</legend>
                <table>
                    <tr>
                        <td class="textlevel">
                            Child Name :
                        </td>
                        <td>
                            <asp:TextBox ID="txtChildName" runat="server" Width="238px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Date of Birth :
                        </td>
                        <td>
                            <asp:TextBox ID="txtDOB" runat="server" Width="80px"></asp:TextBox>
                            <a href="javascript:NewCal('<%= txtDOB.ClientID %>','ddmmyyyy')">
                                <img style="border: 0px;" height="16" alt="Pick a date" src="../../images/cal.gif"
                                    width="16" /></a>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDOB"
                                CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Gender :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlGender" runat="server" Width="89px" 
                                CssClass="textlevelleft">
                                <asp:ListItem Value="N">Nil</asp:ListItem>
                                <asp:ListItem Value="M">Male</asp:ListItem>
                                <asp:ListItem Value="F">Female</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Age :
                        </td>
                        <td>
                            <asp:TextBox ID="txtAge" runat="server" MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Amount :
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="txtAmount" runat="server" MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Month :
                        </td>
                        <td>
                           <asp:DropDownList ID="ddlMonth" runat="server" Width="100px" CssClass="textlevelleft">
                                </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Year :
                        </td>
                        <td colspan="1">
                           <asp:DropDownList ID="ddlYear" runat="server" Width="100px" CssClass="textlevelleft">
                                </asp:DropDownList>
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
                <legend>Child Education Allowance List</legend>
                <div style="overflow: scroll; width: 100%; height: 250px">
                    <asp:GridView ID="grList" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                        AutoGenerateColumns="False" DataKeyNames="AllowanceId,Gender,VMONTH,VYEAR" 
                        onrowcommand="grList_RowCommand" 
                        onselectedindexchanged="grList_SelectedIndexChanged">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                <ItemStyle Width="5%" CssClass="ItemStylecss" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="ChildName" HeaderText="Child Name">
                                <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ChildDOB" HeaderText="Child DOB">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Gender" HeaderText="Gender">
                                <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Age" HeaderText="Age">
                                <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Amount" HeaderText="Amount">
                                <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                            </asp:BoundField> 
                            <asp:BoundField DataField="VMONTH" HeaderText="Month">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="VYEAR" HeaderText="Year">
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
