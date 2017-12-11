<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="LeaveReports.aspx.cs" Inherits="CrystalReports_Leave_LeaveReports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js">
        //Date Time Picker script
    </script>
    <script language="javascript" type="text/javascript">
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }
    </script>
    <div class="formStyle">
        <div id='formhead6'>
            <div style="width: 98%; float: left;">
                Report List</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" alt="Close" /></a></div>
        </div>
        <div class="repotForm2">
            <fieldset>
                <div id="reportList">
                    <asp:TreeView ID="tvReports" runat="server" ImageSet="Arrows" OnSelectedNodeChanged="tvReports_SelectedNodeChanged">
                        <Nodes>
                            <asp:TreeNode Text="Report List" Value="RL">
                                <asp:TreeNode Text="Employee Leave Balance" Value="ELBR"></asp:TreeNode>
                                <asp:TreeNode Text="Employee Leave Details" Value="EWLIR"></asp:TreeNode>
                                <asp:TreeNode Text="Employee Month Wise Leave" Value="EMWLR"></asp:TreeNode>
                                <asp:TreeNode Text="Employee Individual Leave" Value="EILR"></asp:TreeNode>
                            </asp:TreeNode>
                        </Nodes>
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                        <SelectedNodeStyle BackColor="#E0E0E0" Font-Underline="True" ForeColor="#5555DD"
                            HorizontalPadding="0px" VerticalPadding="0px" />
                        <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="5px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                    </asp:TreeView>
                </div>
                <div id="reportListFild">
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div>
                                <div>
                                    <div style="float: left">
                                        <table style="width: 300px;">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 290px">
                                                        <asp:Panel ID="PSearchBy" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                            BorderWidth="1px" Width="289px">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label7" runat="server" Text="Report By :" Width="80px" CssClass="textlevel"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlReportBy" runat="server" Width="127px" CssClass="textlevelleft"
                                                                                OnSelectedIndexChanged="ddlReportBy_SelectedIndexChanged" AutoPostBack="True">
                                                                                <asp:ListItem Value="D">Division</asp:ListItem>
                                                                                <asp:ListItem Value="E">Employee</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td>
                                                        <asp:Panel ID="PEmployeeType" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                            BorderWidth="1px" Width="289px">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label5" runat="server" Text="Employee Type:" Width="80px"
                                                                                CssClass="textlevel"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlEmployeeType" runat="server" Width="127px" CssClass="textlevelleft">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 290px">
                                                        <asp:Panel ID="PDiv" runat="server" BorderStyle="Solid" BorderColor="DarkGray" BorderWidth="1px"
                                                            Width="289px">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label1" runat="server" Text="Organization :" Width="80px" CssClass="textlevel"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlPostDivision" runat="server" Width="127px" CssClass="textlevelleft"
                                                                                OnSelectedIndexChanged="ddlPostDivision_SelectedIndexChanged" AutoPostBack="true">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <%--<td colspan="2">
                                                                <asp:DropDownList ID="ddlDivision" runat="server" Width="250px" AutoPostBack="True"
                                                                    CssClass="textlevelleft">
                                                                </asp:DropDownList>
                                                            </td>--%>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="PEmp" runat="server" BorderStyle="Solid" BorderColor="DarkGray" BorderWidth="1px"
                                                            Width="289px" Visible="False">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label2" runat="server" Text="Employee Id :" Width="80px" CssClass="textlevel"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtEmpCode" runat="server" Width="80px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="PFiscalYr" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                            BorderWidth="1px" Width="289px">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label9" runat="server" Text="Fiscal Year :" Width="80px" CssClass="textlevel"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlFiscalYr" runat="server" Width="127px" CssClass="textlevelleft"
                                                                                OnSelectedIndexChanged="ddlFiscalYr_SelectedIndexChanged">
                                                                                <asp:ListItem Value="-1">All</asp:ListItem>
                                                                                <asp:ListItem Selected="True" Value="FY">FY-2014</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="PLeavetype" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                            BorderWidth="1px" Width="289px">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label3" runat="server" Text="Leave Type :" Width="80px" CssClass="textlevel"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlLeaveType" runat="server" Width="127px" CssClass="textlevelleft"
                                                                                OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged">
                                                                                <asp:ListItem Value="-1">All</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="PSector" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                            BorderWidth="1px" Width="289px">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label8" runat="server" Text="Suncode:" Width="80px" CssClass="textlevel"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlSector" runat="server" Width="127px" CssClass="textlevelleft">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="PProgDept" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                            BorderWidth="1px" Width="289px">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label10" runat="server" Text="Department :" Width="80px"
                                                                                CssClass="textlevel"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlDepartment" runat="server" Width="127px" CssClass="textlevelleft">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="pDate" runat="server" BorderStyle="Solid" BorderColor="DarkGray" BorderWidth="1px"
                                                            Width="289px">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 20px">
                                                                            <asp:Label ID="Label4" runat="server" Text="Date :" Width="80px" CssClass="textlevel"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 66px">
                                                                            <asp:TextBox ID="txtDate" runat="server" Width="80px" MaxLength="10"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 70px">
                                                                            <a href="javascript:NewCal('<%= txtDate.ClientID %>','ddmmyyyy')">
                                                                                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                                                                    height="16" alt="Pick a date" src="../../Images/cal.gif" width="16" /></a>
                                                                        </td>
                                                                        <td colspan="1">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                                                ControlToValidate="txtDate">
                                                                            </asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td colspan="1">
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Width="60px"
                                                                                ErrorMessage="Invalid" ControlToValidate="txtDate" CssClass="validator" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtDate"
                                                            ValidChars="/" FilterType="Custom,Numbers" Enabled="true">
                                                        </cc1:FilteredTextBoxExtender>--%>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="PDateRange" runat="server" BorderColor="DarkGray" BorderStyle="Solid"
                                                            BorderWidth="1px" Width="450px">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 20px">
                                                                            <asp:Label ID="LabelFromDate" runat="server" CssClass="textlevel" Text="From Date :"
                                                                                Width="80px"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 66px">
                                                                            <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="70px"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 70px">
                                                                            <a href="javascript:NewCal('<%= txtFromDate.ClientID %>','ddmmyyyy')">
                                                                                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                                                                    height="16" alt="Pick a date" src="../../images/cal.gif" width="16" />
                                                                            </a>
                                                                        </td>
                                                                        <td colspan="1">
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtFromDate"
                                                                                CssClass="validator" ErrorMessage="Invalid" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"
                                                                                Width="40px"></asp:RegularExpressionValidator>
                                                                        </td>
                                                                        <td style="width: 5px">
                                                                            <asp:Label ID="LabelToDate" runat="server" CssClass="textlevel" Text="To Date :"
                                                                                Width="50px"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 62px">
                                                                            <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="70px"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 99px">
                                                                            <a href="javascript:NewCal('<%= txtToDate.ClientID %>','ddmmyyyy')">
                                                                                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                                                                    height="16" alt="Pick a date" src="../../images/cal.gif" width="16" />
                                                                            </a>
                                                                        </td>
                                                                        <td colspan="1">
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtToDate"
                                                                                CssClass="validator" ErrorMessage="Invalid" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"
                                                                                Width="40px"></asp:RegularExpressionValidator>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="PEmpStatus" runat="server" BorderColor="DarkGray" BorderStyle="Solid"
                                                            BorderWidth="1px" Width="289px">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <%--OnSelectedIndexChanged="radBtnListEmp_SelectedIndexChanged" AutoPostBack="True"--%>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="radBtnListEmp" runat="server" Font-Names="Tahoma" Font-Size="11px"
                                                                                ForeColor="Blue" RepeatDirection="Horizontal">
                                                                                <asp:ListItem Value="M">All</asp:ListItem>
                                                                                <asp:ListItem Selected="True" Value="A">Active</asp:ListItem>
                                                                                <asp:ListItem Value="I">Inactive</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div style="float: left">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="P_Emp" runat="server" BorderStyle="Solid" BorderColor="DarkGray" BorderWidth="1px"
                                                            Width="590px" Visible="False">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <div style="border-right: black 1px solid; border-top: black 1px solid; margin: 15px 0px 0px;
                                                                                overflow: scroll; border-left: black 1px solid; width: 100%; border-bottom: black 1px solid;
                                                                                height: 250px; width: 580px">
                                                                                <asp:GridView ID="gvEmp" runat="server" Font-Size="9px" EmptyDataText="No Record Found"
                                                                                    AutoGenerateColumns="False" DataKeyNames="EmpId" Width="560px">
                                                                                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                                                                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                                                                    </SelectedRowStyle>
                                                                                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Select">
                                                                                            <ItemStyle CssClass="ItemStylecss" Width="5%" />
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkBoxEmp" runat="Server"></asp:CheckBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="EmpId" HeaderText="Employee ID">
                                                                                            <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="FullName" HeaderText="Employee Name">
                                                                                            <ItemStyle CssClass="ItemStylecss" Width="40%"></ItemStyle>
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="DesigName" HeaderText="Designation">
                                                                                            <ItemStyle CssClass="ItemStylecss" Width="35%"></ItemStyle>
                                                                                        </asp:BoundField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                                <!--Grid view Code Ends-->
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div>
                                    <div style="float: left; width: 50%;">
                                        <div style="padding-right: 15px; float: right;">
                                            <asp:Panel ID="PShow" runat="server" Visible="False" Width="289px">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 30px">
                                                                <asp:Button ID="btnShow" OnClick="btnShow_Click" runat="server" Text="Show Report"
                                                                    Font-Underline="False"></asp:Button>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <div style="float: right; width: 30%;">
                                        <div style="padding-right: 15px; float: right;">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 30px">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
