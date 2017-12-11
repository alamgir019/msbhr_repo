<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="LeaveBalance.aspx.cs" Inherits="Leave_LeaveBalance" Title="Leave Balance" %>

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

    <div class="formStyle" style="height: 170px; width: 50%">
        <div id="formhead4">
            Leave Balance</div>
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
                            <asp:DropDownList ID="ddlDivision" CssClass="textlevelleft" runat="server" Width="350px">
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
                            <asp:DropDownList ID="ddlDept" CssClass="textlevelleft" runat="server" Width="350px">
                            </asp:DropDownList></td>
                        <td style="width: 3px">
                        </td>
                        <td>
                            <asp:Button ID="btnPriview" runat="server" Text="Print Priview" OnClick="btnPriview_Click" /></td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>
</asp:Content>
