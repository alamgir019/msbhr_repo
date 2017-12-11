<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="AttnLogEdit.aspx.cs" Inherits="Attendance_AttnLogEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Script/datetimepicker.js">
    </script>
    <script language="javascript" type="text/javascript">
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }
    </script>
    <div class="formStyle" style="width: 600px;">
        <div id="formhead4">
            <div style="width: 96%; float: left;">
                Time Sheet Report</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../home.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div class="iesEmp" style="margin-top: 0px;" width="500px">
            <fieldset>
                <table>
                    <tr>
                        <td class="textlevel">
                            Remarks Type:
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlRemarksType" runat="server" Width="350px" CssClass="textlevelleft">
                                <asp:ListItem Text="Set IN Time" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Set Out Time" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Field Trip/Workshop/Other - Workday" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Field Trip/Workshop/Other - Workday/Holiday" Value="4"></asp:ListItem>
                                <asp:ListItem Text="IN Time Missing for 1st Half Leave" Value="5"></asp:ListItem>
                                <asp:ListItem Text="OUT Time Missing for 2nd Half Leave" Value="6"></asp:ListItem>
                                <asp:ListItem Text="Work at Home" Value="7"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Remarks :
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtRemarks" runat="server" Width="350px" CssClass="textlevelleft"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Date From :
                        </td>
                        <td>
                            <asp:TextBox ID="txtFromDate" runat="server" Width="80px">
                            </asp:TextBox>
                            <a href="javascript:NewCal('<%= txtFromDate.ClientID %>','ddmmyyyy')">
                                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                    height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                ControlToValidate="txtFromDate"></asp:RequiredFieldValidator>
                        </td>
                        <td class="textlevel">
                            Date To :
                        </td>
                        <td>
                            <asp:TextBox ID="txtToDate" runat="server" Width="80px">
                            </asp:TextBox>
                            <a href="javascript:NewCal('<%= txtToDate.ClientID %>','ddmmyyyy')">
                                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                    height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                ControlToValidate="txtToDate"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="3">
                            <span style="color: Green; font-weight: bold; margin-bottom: 15px;">Note: Full Day leave
                                do not required any Remarks.</span>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <div class="DivCommand1" style="width: 98%;">
                <div class="DivCommandL">
                    <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" Text="Refresh"
                        Width="70px" OnClick="btnRefresh_Click" ToolTip="Click on Save Button to store the employee data." />
                </div>
                <div class="DivCommandR">
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click"
                        ToolTip="Click on Save Button to store the employee data." />
                    <asp:Button ID="btnDelete" runat="server" OnClientClick="javascript:return DeleteConfirmation();"
                        Text="Delete" Width="70px" />
                </div>
                <br />
            </div>
            <%-- <div style="margin-top: 10px;">
                <asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel File" ForeColor="blue"
                    OnClick="btnExportExcel_Click" />
            </div>--%>
            <%-- <div style="overflow: scroll; height: 350px; margin-top: 10px;">
                <asp:GridView ID="grReport" Width="99%" runat="server" EmptyDataText="No Record Found"
                    AutoGenerateColumns="true" DataKeyNames="" Font-Size="12px" Font-Names="Arial"
                    ShowFooter="true" OnRowDataBound="grReport_RowDataBound">
                    <HeaderStyle Font-Bold="True" HorizontalAlign="center" BackColor="LightGray" Font-Size="12px"
                        Font-Names="Arial"></HeaderStyle>
                    <RowStyle HorizontalAlign="center" />
                    <FooterStyle Font-Bold="True" HorizontalAlign="center" BackColor="LightGray" Font-Size="12px"
                        Font-Names="Arial"></FooterStyle>
                </asp:GridView>
            </div>--%>
        </div>
    </div>
</asp:Content>
