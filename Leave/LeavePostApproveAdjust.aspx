<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="LeavePostApproveAdjust.aspx.cs" Inherits="Leave_LeavePostApproveAdjust"
    Title="Leave Post Approval Adjustment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js">
    //Date Time Picker script
    </script>

    <script language="javascript">
    function ToUpper(ctrl)
    {   
    var t = ctrl.value;
    ctrl.value = t.toUpperCase(); 
    }
    </script>

    <div class="leaveApplicaionStyle">
        <div id='formhead4'>
            Leave Post Approval Adjustment Form</div>
        <!--Div for group-->
        <div id="leaveApplicaionFormInner">
            <div class="MsgBox">
                <!--Div for msg-->
                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Navy" CssClass="msglabelleft"></asp:Label>
            </div>
            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1" Height="710px">
                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                    <HeaderTemplate>
                        Leave Application With Approval Form</HeaderTemplate>
                    <ContentTemplate>
                        <div class="Div900">
                            <fieldset style="background-color: #EFF3FB;">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="Applicant : " CssClass="textlevel"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblName" runat="server" Width="600px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label15" runat="server" CssClass="textlevel" Text="Supervisor :"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSupervisor" runat="server" Width="600px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" CssClass="textlevel" Text="Leave Package : "></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEmpType" runat="server" Width="211px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hfSex" runat="server" />
                            </fieldset>
                            <fieldset>
                                <div style="width: 22%; float: left;">
                                    <fieldset style="margin-top: 7px; height: 480px;">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text="Employee ID" CssClass="textlevelcenterfine"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmpId"
                                                        ErrorMessage="*" Height="16px"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtEmpId" runat="server" onkeyup="ToUpper(this)" Height="16px" Width="125px"></asp:TextBox>
                                                    <asp:ImageButton ID="imgBtnSearch" runat="server" Height="19px" ImageUrl="~/Images/Search_Icon.jpg"
                                                        Width="21px" OnClick="imgBtnSearch_Click" /></td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text="Application Date [dd/mm/yyyy]" CssClass="textlevelcenterfine"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAppDate"
                                                        ErrorMessage="*" Height="16px"></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtAppDate" runat="server" Height="16px" Width="125px"></asp:TextBox>
                                                    &nbsp;<a href="javascript:NewCal('','ddmmyyyy')"><img style="border-right: 0px; border-top: 0px;
                                                        border-left: 0px; border-bottom: 0px" height="16" alt="Pick a date" src="../images/cal.gif"
                                                        width="16" /></a>
                                                </td>
                                                <td>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Width="60px"
                                                        CssClass="validator" ErrorMessage="Invalid Date" ControlToValidate="txtAppDate"
                                                        ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator></td>
                                            </tr>
                                        </table>
                                        <asp:UpdatePanel id="UpdatePanelLeaveType" runat="server">
                                            <contenttemplate>
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td style="width: 174px">
                                                        <asp:Label ID="Label11" runat="server" Text="Leave Type" CssClass="textlevelcenterfine"
                                                            __designer:wfdid="w97"></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 174px">
                                                        <asp:DropDownList ID="ddlLeaveType" runat="server" CausesValidation="True" Width="160px"
                                                            AutoPostBack="True" __designer:wfdid="w98" OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged">
                                                            <asp:ListItem Value="99999">Nil</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 174px">
                                                        <asp:Label ID="Label12" runat="server" Text="From Date [dd/mm/yyyy]" CssClass="textlevelcenterfine"
                                                            __designer:wfdid="w99"></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 174px">
                                                        <asp:TextBox ID="txtFromDate" runat="server" Width="125px" MaxLength="10" __designer:wfdid="w100"></asp:TextBox>&nbsp;&nbsp;<a
                                                            href="javascript:NewCal('<%= txtFromDate.ClientID %>','ddmmyyyy')"><img style="border-right: 0px;
                                                                border-top: 0px; border-left: 0px; border-bottom: 0px" height="16" alt="Pick a date"
                                                                src="../images/cal.gif" width="16" /></a>
                                                    </td>
                                                    <td>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" CssClass="validator"
                                                            Width="60px" ControlToValidate="txtFromDate" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"
                                                            __designer:wfdid="w101"></asp:RegularExpressionValidator></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 174px">
                                                        <asp:Label ID="Label13" runat="server" Text="To Date [dd/mm/yyyy]" CssClass="textlevelcenterfine"
                                                            __designer:wfdid="w102"></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 174px">
                                                        <asp:TextBox ID="txtToDate" runat="server" Width="125px" MaxLength="10" __designer:wfdid="w103"></asp:TextBox>&nbsp;&nbsp;<a
                                                            href="javascript:NewCal('<%= txtToDate.ClientID %>','ddmmyyyy')"><img style="border-right: 0px;
                                                                border-top: 0px; border-left: 0px; border-bottom: 0px" height="16" alt="Pick a date"
                                                                src="../images/cal.gif" width="16" /></a>
                                                    </td>
                                                    <td>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" CssClass="validator"
                                                            Width="60px" ControlToValidate="txtToDate" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"
                                                            __designer:wfdid="w104"></asp:RegularExpressionValidator></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 174px">
                                                        <asp:Label ID="Label16" runat="server" Text="Half Day" CssClass="textlevelcenterfine"
                                                            __designer:wfdid="w105"></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 174px">
                                                        <asp:DropDownList ID="ddlHalfDay" runat="server" CausesValidation="True" Width="160px"
                                                            AutoPostBack="True" __designer:wfdid="w106" OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Nil</asp:ListItem>
                                                            <asp:ListItem Value="1">1st Half</asp:ListItem>
                                                            <asp:ListItem Value="2">2nd Half</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 174px">
                                                        <asp:Label ID="Label17" runat="server" Text="Off. Resume Date [dd/mm/yyyy]" CssClass="textlevelcenterfine"
                                                            __designer:wfdid="w107"></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 174px">
                                                        <asp:TextBox ID="txtResumeOn" runat="server" Width="125px" MaxLength="10" __designer:wfdid="w108"></asp:TextBox>&nbsp;&nbsp;<a
                                                            href="javascript:NewCal('<%= txtResumeOn.ClientID %>','ddmmyyyy')"><img style="border-right: 0px;
                                                                border-top: 0px; border-left: 0px; border-bottom: 0px" height="16" alt="Pick a date"
                                                                src="../images/cal.gif" width="16" /></a>
                                                    </td>
                                                    <td>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" CssClass="validator"
                                                            Width="60px" ControlToValidate="txtResumeOn" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"
                                                            __designer:wfdid="w109"></asp:RegularExpressionValidator></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 174px">
                                                        <asp:Button ID="btnCalculate" OnClick="btnCalculate_Click" runat="server" Text="Calculate Days "
                                                            Width="160px" __designer:wfdid="w110"></asp:Button></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 174px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label14" runat="server" Text="Availability" CssClass="textlevelcenterfine"
                                                                            __designer:wfdid="w111" BackColor="#FF9933" Width="74px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label114" runat="server" Text="Applied For" CssClass="textlevelcenterfine"
                                                                            __designer:wfdid="w112" BackColor="#FF9933" Width="74px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: center">
                                                                        <asp:TextBox ID="LAv" runat="server" Width="69px" ForeColor="#3366CC" __designer:wfdid="w113"
                                                                            BorderStyle="None" BorderColor="Transparent"></asp:TextBox>
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        <asp:TextBox ID="txtLDurInDays" runat="server" Width="69px" ForeColor="#3366CC" __designer:wfdid="w114"
                                                                            BorderStyle="None" BorderColor="Transparent"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblMsg2" runat="server" CssClass="msglabel" Width="240px" __designer:wfdid="w115"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblMsg3" runat="server" CssClass="msglabel" Width="240px" __designer:wfdid="w116"></asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <asp:HiddenField ID="hfLEnjoyed" runat="server" __designer:wfdid="w117"></asp:HiddenField>
                                        <asp:HiddenField ID="hfLAbbrName" runat="server" __designer:wfdid="w118"></asp:HiddenField>
                                        <asp:HiddenField ID="hfLDates" runat="server" __designer:wfdid="w119"></asp:HiddenField>
                                        <asp:HiddenField ID="hfLTypeNature" runat="server" __designer:wfdid="w120"></asp:HiddenField>
                                        <asp:HiddenField ID="hfPreLTypeId" runat="server" __designer:wfdid="w121"></asp:HiddenField>
                                        <asp:HiddenField ID="hfPreLEnjoyed" runat="server" __designer:wfdid="w122"></asp:HiddenField>
                                        <asp:HiddenField ID="hfPreLDates" runat="server" __designer:wfdid="w123"></asp:HiddenField>
                                        <asp:HiddenField ID="hfIsOffdayCounted" runat="server" __designer:wfdid="w124"></asp:HiddenField>
                                        <asp:Label ID="lblLDurInDays" runat="server" __designer:wfdid="w125"></asp:Label>
                                        </contenttemplate>
                                        </asp:UpdatePanel>
                                    </fieldset>
                                </div>
                                <div style="width: 26%; float: left;">
                                    <fieldset style="margin-top: 7px; margin-left: 5px; margin-right: 5px; height: 480px;">
                                        <table>
                                            <tr>
                                                <td colspan="2" style="width: 300px; color: #ff9933; height: 10px; font-weight: bold;
                                                    font-size: 11px;" class="textlevelleft">
                                                    &nbsp; Please do not use any " &amp; " in your text.
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label8" runat="server" CssClass="textlevelcenterfine" Text="Reason for Leave (Optional)"
                                                        Width="250px"></asp:Label></td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtLeaveReason" runat="server" Height="113px" MaxLength="1000" TextMode="MultiLine"
                                                        Width="245px"></asp:TextBox></td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label9" runat="server" CssClass="textlevelcenterfine" Text="Leave Address"
                                                        Width="250px"></asp:Label></td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtLeaveAdd" runat="server" Height="114px" MaxLength="150" TextMode="MultiLine"
                                                        Width="245px"></asp:TextBox></td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label10" runat="server" CssClass="textlevelcenterfine" Text="Contact Number"
                                                        Width="250px"></asp:Label></td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtPhone" runat="server" Height="17px" MaxLength="100" Width="245px"></asp:TextBox></td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label18" runat="server" CssClass="textlevelcenterfine" Text="Person Responsible During Leave"
                                                        Width="250px"></asp:Label></td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlResPerson" runat="server" AutoPostBack="True" CausesValidation="True"
                                                        Width="250px">
                                                        <asp:ListItem Value="99999">Nil</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkIsAllEmp" runat="server" AutoPostBack="True" CssClass="textlevel"
                                                        OnCheckedChanged="chkIsAllEmp_CheckedChanged" Text="Show All Employee" Width="109px" /></td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                                <div style="width: 43%; float: left;">
                                    <fieldset>
                                        <legend>Leave Balance</legend>
                                        <div style="float: left; margin: 5px 0px 0px 5px; width: 98%; height: 180px; overflow: scroll;">
                                            <asp:GridView ID="grLeaveStatus" runat="server" AutoGenerateColumns="False" DataKeyNames="EmpId,LTypeID,LTypeTitle,LCarryOverd,LEntitled,LCashed,LeaveEnjoyed,LeaveElapsed,lvOpening"
                                                EmptyDataText="No Record Found" Font-Size="9px" PageSize="7" Width="98%">
                                                <HeaderStyle BackColor="#FF9933" Font-Bold="True" Font-Size="10px" HorizontalAlign="Center"
                                                    ForeColor="WhiteSmoke" />
                                                <AlternatingRowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:BoundField DataField="LTypeTitle" HeaderText="Leave Title">
                                                        <ItemStyle CssClass="ItemStylecssCenter" Width="25%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="lvPrevYearCarry" Visible="False">
                                                        <ItemStyle CssClass="ItemStylecssCenter" HorizontalAlign="Right" Width="0%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LCarryOverd" HeaderText="Carry Over">
                                                        <ItemStyle CssClass="ItemStylecssCenter" HorizontalAlign="Right" Width="15%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LEntitled" HeaderText="Entitled">
                                                        <ItemStyle CssClass="ItemStylecssCenter" HorizontalAlign="Right" Width="15%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Total Leave Entitlement">
                                                        <ItemStyle CssClass="ItemStylecssCenter" HorizontalAlign="Right" Width="15%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LeaveEnjoyed" HeaderText="Availed">
                                                        <ItemStyle CssClass="ItemStylecssCenter" HorizontalAlign="Right" Width="15%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Current Balance">
                                                        <ItemStyle CssClass="ItemStylecssCenter" HorizontalAlign="Right" Width="15%" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </fieldset>
                                    <fieldset>
                                        <legend>Copy To</legend>
                                        <div style="float: left; margin: 5px 0px 0px 5px; width: 98%; height: 259px; overflow: scroll;">
                                            <asp:GridView ID="grConcernPerson" runat="server" AutoGenerateColumns="False" DataKeyNames="EmpId,FullName,PersEmail1"
                                                EmptyDataText="No Record Found" Font-Size="9px" PageSize="7" Width="98%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Select">
                                                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkBox" runat="Server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="FullName" HeaderText="Employee Name">
                                                        <ItemStyle CssClass="ItemStylecss" Width="89%" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <HeaderStyle BackColor="#3366CC" Font-Bold="True" Font-Size="10px" HorizontalAlign="Left"
                                                    ForeColor="WhiteSmoke" />
                                                <AlternatingRowStyle BackColor="#EFF3FB" />
                                            </asp:GridView>
                                        </div>
                                    </fieldset>
                                </div>
                            </fieldset>
                            <fieldset style="margin-top: 5px;">
                                <div style="width: 28%; float: left;">
                                    <div class="DivCommand1">
                                        <div class="DivCommandR">
                                            <div class="DivCommandL" style="margin-left: -5px;">
                                                <asp:Button ID="btnSave" runat="server" Text="Apply" Width="245px" OnClick="btnSave_Click" />&nbsp;
                                            </div>
                                            <asp:Button ID="btnClear" runat="server" Text="Refresh" Width="70px" OnClick="btnClear_Click"
                                                UseSubmitBehavior="False" CausesValidation="False" Visible="False" />
                                            <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                                                OnClick="btnDelete_Click" Visible="False" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Leave Cancel" OnClick="btnCancel_Click"
                                                Visible="False" />
                                        </div>
                                    </div>
                                </div>
                                <div style="width: 65%; float: left;">
                                    <%--<div class="MsgBox">
                                        <!--Div for msg-->
                                        <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Navy" CssClass="msglabelleft"></asp:Label>
                                    </div>--%>
                                </div>
                            </fieldset>
                            <asp:Panel ID="pnlEmpList" runat="server">
                                <fieldset>
                                    <legend>Forward Leave Application for your Collegues</legend>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label19" runat="server" Text="Select the employee for whom you want to forward leave application and press Load Employee button "
                                                    Width="500px" CssClass="textlevelcenterfine"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 3px">
                                                <asp:DropDownList ID="ddlEmpList" runat="server" Width="500px">
                                                </asp:DropDownList></td>
                                            <td>
                                                <asp:Button ID="btnEmpDetails" runat="server" Text="Load Employee" OnClick="btnEmpDetails_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </asp:Panel>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="Office : " CssClass="textlevel" Visible="False"></asp:Label></td>
                                    <td colspan="3">
                                        <asp:Label ID="lblDept" runat="server" Width="211px" Visible="False"></asp:Label></td>
                                    <td style="width: 2px">
                                        <asp:Label ID="Label6" runat="server" Text="Designation :" CssClass="textlevel" Visible="False"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblDesig" runat="server" Width="211px" Visible="False"></asp:Label></td>
                                    <td style="width: 3px">
                                    </td>
                                    <td style="width: 3px">
                                    </td>
                                </tr>
                            </table>
                            <asp:HiddenField ID="hfIsUpadate" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hfID" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hfLvPackStartDate" runat="server" />
                            <asp:HiddenField ID="hfLvPackEndDate" runat="server" />
                            <asp:HiddenField ID="hfLPakId" runat="server" />
                            <cc1:FilteredTextBoxExtender ID="ftbADate" runat="server" Enabled="True" ValidChars="/"
                                FilterType="Custom, Numbers" TargetControlID="txtAppDate">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:FilteredTextBoxExtender ID="ftbEmpId" runat="server" Enabled="True" ValidChars="-"
                                FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" TargetControlID="txtEmpId">
                            </cc1:FilteredTextBoxExtender>
                            <asp:HiddenField ID="hfSupervisor" runat="server" />
                            <asp:HiddenField ID="hfSupervisorEmail" runat="server" />
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                    <ContentTemplate>
                        <div style="margin: 10px 5px 5px 10px; height: 700px; width: 98%; _width: 97%; overflow: scroll;
                            border: solid 1px gray;">
                            <asp:GridView ID="grLeaveApp" runat="server" AutoGenerateColumns="False" DataKeyNames="LvAppID,LTypeTitle,Ltype,LNature,AppDate,LeaveStart,LeaveEnd,Duration,InsertedBy,LTReason,EmpId,ResponsiveEmpId,AddrAtLeave,PhoneNo,IsHalfDay,ResumeDate"
                                EmptyDataText="No Record Found" Font-Size="9px" OnRowCommand="grLeaveApp_RowCommand"
                                OnSelectedIndexChanged="grLeaveApp_SelectedIndexChanged" PageSize="7" Width="100%">
                                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                                <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                        <ItemStyle CssClass="ItemStylecss" Width="50px" />
                                    </asp:ButtonField>
                                    <asp:BoundField HeaderText="SL No.">
                                        <ItemStyle CssClass="ItemStylecss" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EmpId" HeaderText="Emp No.">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LTypeTitle" HeaderText="Leave Name">
                                        <ItemStyle CssClass="ItemStylecss" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AppDate" HeaderText="Posting Date">
                                        <ItemStyle CssClass="ItemStylecss" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LeaveStart" HeaderText="Leave From">
                                        <ItemStyle CssClass="ItemStylecss" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LeaveEnd" HeaderText="Leave To">
                                        <ItemStyle CssClass="ItemStylecss" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Duration" HeaderText="Total Days">
                                        <ItemStyle CssClass="ItemStylecssRight" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="InsertedBy" HeaderText="Posted By">
                                        <ItemStyle CssClass="ItemStylecss" Width="100px" />
                                    </asp:BoundField>
                                    <asp:ButtonField CommandName="ViewClick" HeaderText="View" Text="View">
                                        <ItemStyle CssClass="ItemStylecss" Width="50px" />
                                    </asp:ButtonField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                    <HeaderTemplate>
                        Approved Leave Application List&nbsp;
                    </HeaderTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <%--</cc1:TabContainer>--%>
        </div>
    </div>
</asp:Content>
