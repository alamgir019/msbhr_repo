<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="TimeSheetViewer.aspx.cs" Inherits="Attendance_TimeSheetViewer" Title="Time Sheet Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Script/datetimepicker.js">
    </script>
    <script language="javascript" type="text/javascript">
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }
    </script>
    <div class="formStyle">
        <div id="formhead4">
            <div style="width: 98%; float: left;">
                Time Sheet Summary</div>
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
                            Employee ID:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEmployee" runat="server" Width="200px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                            Month From :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMonthFrom" runat="server" Width="100px" CssClass="textlevelleft">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlYearFrom" runat="server" Width="100px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                            Month To :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMonthTo" runat="server" Width="100px" CssClass="textlevelleft">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlYearTo" runat="server" Width="100px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnPriview" runat="server" Text="Show Time Sheet Summary" OnClick="btnPriview_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <div style="margin-top: 10px;">
                <asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel File" ForeColor="blue"
                    OnClick="btnExportExcel_Click" />
            </div>
            <div style="overflow: scroll; height: 350px; margin-top: 10px;">
                <asp:GridView ID="grReport" runat="server" EmptyDataText="No Record Found" AutoGenerateColumns="true"
                    DataKeyNames="" Font-Size="12px" Font-Names="Arial" ShowFooter="true">
                    <HeaderStyle Font-Bold="True" HorizontalAlign="center" BackColor="LightGray" Font-Size="12px"
                        Font-Names="Arial"></HeaderStyle>
                    <FooterStyle Font-Bold="True" HorizontalAlign="center" BackColor="LightGray" Font-Size="12px"
                        Font-Names="Arial"></FooterStyle>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
