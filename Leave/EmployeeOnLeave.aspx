<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="EmployeeOnLeave.aspx.cs" Inherits="Leave_EmployeeOnLeave" Title="Employees On Leave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../Script/datetimepicker.js">
    </script>

    <script language="javascript" type="text/javascript">
    function ToUpper(ctrl)
    {   
    var t = ctrl.value;
    ctrl.value = t.toUpperCase(); 
    }
    </script>

    <div class="formStyle" style="height: 180px; width: 50%">
        <div id="formhead4">
            Employees on Leave</div>
        <div class="iesEmp" style="margin-top: 40px;">
            <fieldset>
                <table>
                    <tr>
                        <td class="textlevel">
                            Emp Id :</td>
                        <td colspan="5">
                            <asp:TextBox ID="txtEmpId" Width="80px" runat="server" onkeyup="ToUpper(this)"></asp:TextBox></td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Office :</td>
                        <td colspan="6">
                            <asp:DropDownList ID="ddlDivision" CssClass="textlevelleft" runat="server" Width="100%">
                            </asp:DropDownList></td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Team :</td>
                        <td colspan="6">
                            <asp:DropDownList ID="ddlDept" CssClass="textlevelleft" runat="server" Width="100%">
                            </asp:DropDownList></td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            From Date :
                        </td>
                        <td>
                            <asp:TextBox ID="txtFrom" runat="server" Width="80px"></asp:TextBox>
                        </td>
                        <td>
                            <a href="javascript:NewCal('<%= txtFrom.ClientID %>','ddmmyyyy')">
                                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                    height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFrom"
                                ErrorMessage="*" Height="16px"></asp:RequiredFieldValidator></td>
                        <td class="textlevel">
                            To Date :
                        </td>
                        <td>
                            <asp:TextBox ID="txtTo" runat="server" Width="80px"></asp:TextBox>
                        </td>
                        <td>
                            <a href="javascript:NewCal('<%= txtTo.ClientID %>','ddmmyyyy')">
                                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                    height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTo"
                                ErrorMessage="*" Height="16px"></asp:RequiredFieldValidator></td>
                        <td>
                            <asp:Button ID="btnPriview" runat="server" Text="Print Priview" OnClick="btnPriview_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>
</asp:Content>
