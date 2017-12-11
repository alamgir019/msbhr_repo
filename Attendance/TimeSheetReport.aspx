<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="TimeSheetReport.aspx.cs" Inherits="Attendance_TimeSheetReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Script/datetimepicker.js">
    </script>
    <script language="javascript" type="text/javascript">
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }
    </script>
    <div class="formStyle" style="width: 80%;">
        <div id="formhead4">
            <div style="width: 97%; float: left;">
                Time Sheet Report</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div class="iesEmp" style="margin-top: 0px;">
            <fieldset>
                <table>
                    <tr>
                        <td class="textlevel">
                            Emp ID:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEmployee" runat="server" Width="150px" 
                                CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                            <asp:RadioButtonList ID="radEmp" runat="server" Font-Names="Tahoma" Font-Size="11px"
                                ForeColor="Blue" RepeatDirection="Horizontal" AutoPostBack="True" 
                                onselectedindexchanged="radEmp_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="A">Active</asp:ListItem>
                                <asp:ListItem Value="I">Seperated</asp:ListItem>                                
                            </asp:RadioButtonList>
                        </td>
                        <td class="textlevel">
                            Month :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" runat="server" Width="80px" 
                                CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                            Year :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlYear" runat="server" Width="60px" 
                                CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnPriview" runat="server" Text="Print Preview" 
                                OnClick="btnPriview_Click"/>                            
                        </td>
                        <td><asp:CheckBox ID="chkIsRound" runat="server" CssClass="textlevel" 
                                Text="With Round" Width="100px"/></td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>
</asp:Content>
