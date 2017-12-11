<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="PayrollReports.aspx.cs" Inherits="CrystalReports_Payroll_PayrollReports"
    Title="Reports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../JScripts/datetimepicker.js" type="text/javascript"></script>
    <div class="formStyle">
        <div id='formhead6'>
            <div style="width: 98%; float: left;">
                Report List</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" alt="Close" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Fuchsia"></asp:Label>
        </div>
        <div class="repotForm2">
            <fieldset>
                <div id="reportList">
                    <asp:TreeView ID="tvReports" runat="server" ImageSet="Arrows" OnSelectedNodeChanged="tvReports_SelectedNodeChanged">
                        <Nodes>
                            <asp:TreeNode Text="Payroll Report List" Value="RL">
                                <asp:TreeNode Text="Salary" Value="SR">
                                    <asp:TreeNode Text="Salary Payslip" Value="ESPS"></asp:TreeNode>
                                    <asp:TreeNode Text="Bank Advice " Value="BSFF"></asp:TreeNode>
                                    <asp:TreeNode Text="Salary Certificate" Value="SC"></asp:TreeNode>
                                    <asp:TreeNode Text="Salary Sheet" Value="SSS"></asp:TreeNode>
                                    <asp:TreeNode Text="Salary Sheet Summary" Value="SSSum"></asp:TreeNode>                                   
                                </asp:TreeNode>
                                <asp:TreeNode Text="Provident Fund" Value="PF">
                                    <asp:TreeNode Text="Monthly PF Contribution" Value="MPFC"></asp:TreeNode>
                                    <asp:TreeNode Text="Yearly PF Contribution" Value="YPFC"></asp:TreeNode>
                                    <asp:TreeNode Text="Yearly PF Balance" Value="YPFB"></asp:TreeNode>
                                    <asp:TreeNode Text="PF Loan Deduction" Value="YPFLD"></asp:TreeNode>
                                </asp:TreeNode>
                                <asp:TreeNode Text="Final Payment" Value="FP">
                                    <asp:TreeNode Text="Final Payment" Value="FP"></asp:TreeNode>
                                   <%-- <asp:TreeNode Text="Final Payment List" Value="FPL"></asp:TreeNode>--%>
                                   <%-- <asp:TreeNode Text="Final Payment Due List" Value="FPDL"></asp:TreeNode>--%>
                                </asp:TreeNode>
                                 <asp:TreeNode Text="Employee Information" Value="E">
                                 <asp:TreeNode Text="Employee Salary Information" Value="ESI"></asp:TreeNode>
                                 <asp:TreeNode Text="Salary Exceptionnal Case" Value="SEC"></asp:TreeNode>
                                 <asp:TreeNode Text="Salary Change History" Value="SCH"></asp:TreeNode>
                                 </asp:TreeNode>  
                                  <asp:TreeNode Text="Bonus" Value="B">
                                    <asp:TreeNode Text="Bonus Statement for the festival" Value="BST"></asp:TreeNode>
                                    </asp:TreeNode> 
                                <asp:TreeNode Text="Salary SC" Value="SR">
                                    <asp:TreeNode Text="Salary Statement" Value="SS"></asp:TreeNode>
                                    <asp:TreeNode Text="Salary Summary" Value="SalSum"></asp:TreeNode>
                                    <%--<asp:TreeNode Text="Salary Sheet" Value="SSSEW"></asp:TreeNode>--%>
                                    <asp:TreeNode Text="Effort Report" Value="ER"></asp:TreeNode>
                                    <asp:TreeNode Text="Payroll Report(Location Wise)" Value="PRLW"></asp:TreeNode>
                                    <asp:TreeNode Text="Payroll Grant Wise" Value="SSWSD"></asp:TreeNode>
                                    <asp:TreeNode Text="Payroll Basic With Charging" Value="PBWC"></asp:TreeNode>
                                    <asp:TreeNode Text="Net Salary and Grant Wise Salary Distribution" Value="NSWSD">
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="Add/Deduction Report" Value="ADR"></asp:TreeNode>
                                    <asp:TreeNode Text="Salary Reconciliation Report " Value="SR"></asp:TreeNode>
                                    <asp:TreeNode Text="Salary Reconciliation Statement" Value="SRDTL"></asp:TreeNode>
                                    <asp:TreeNode Text="Salary Reconciliation Report 2" Value="SRR"></asp:TreeNode>
                                </asp:TreeNode>
                                <asp:TreeNode Text="Voucher" Value="V">
                                    <asp:TreeNode Text="All Voucher" Value="AV"></asp:TreeNode>
                                    <asp:TreeNode Text="Bonus Voucher" Value="BV"></asp:TreeNode>
                                </asp:TreeNode>
                                <%--<asp:TreeNode Text="Additional Responsibility Allowance" Value="ARA"></asp:TreeNode>--%>
                                <%-- <asp:TreeNode Text="Payroll Report(Employee Current Charging)" Value="PRECC"></asp:TreeNode>--%>
                                <%--  <asp:TreeNode Text="Medical" Value="M">
                                    <asp:TreeNode Text="Medical Report" Value="MR"></asp:TreeNode>
                                    <asp:TreeNode Text="Medical Benefits Balance" Value="MBB"></asp:TreeNode>
                                    <asp:TreeNode Text="Medical Benefits Received" Value="MBR"></asp:TreeNode>
                                    <asp:TreeNode Text="Monthly Medicine Received Report" Value="MMRR"></asp:TreeNode>
                                    <asp:TreeNode Text="Medical Hospital Received Report" Value="MHRR"></asp:TreeNode>
                                </asp:TreeNode>--%>
                                <asp:TreeNode Text="Bonus" Value="B">                                    
                                    <asp:TreeNode Text="Festival Bonus Summery" Value="FBS"></asp:TreeNode>
                                    <asp:TreeNode Text="Festival Bonus Source Wise" Value="FBSW"></asp:TreeNode>
                                    <asp:TreeNode Text="Bonus Statement Report" Value="BSR"></asp:TreeNode>
                                    <asp:TreeNode Text="Bonus Payslip" Value="EBPS"></asp:TreeNode>
                                </asp:TreeNode>
                                <asp:TreeNode Text="OT Calculation" Value="OTC"></asp:TreeNode>
                                <asp:TreeNode Text="Provident Fund" Value="PF2">
                                    <asp:TreeNode Text="Individual PF Contribution of Employee" Value="IPFC"></asp:TreeNode>
                                    <%--<asp:TreeNode Text="Yearly Report" Value="Y"></asp:TreeNode>        --%>
                                    
                                </asp:TreeNode>
                                <asp:TreeNode Text="Annual Income" Value="AI"></asp:TreeNode>
                                <asp:TreeNode Text="Tax" Value="T">
                                    <asp:TreeNode Text="Yearly Income Tax" Value="AITD"></asp:TreeNode>
                                    <asp:TreeNode Text="Tax Deduction Rpt." Value="TDR"></asp:TreeNode>
                                    <asp:TreeNode Text="Income Tax Assessment" Value="ITA"></asp:TreeNode>
                                    <asp:TreeNode Text="Computation of Income Tax" Value="ITC"></asp:TreeNode>
                                    <asp:TreeNode Text="Tax Certificate" Value="TC"></asp:TreeNode>
                                </asp:TreeNode>
                                <asp:TreeNode Text="Gratuity" Value="GR">
                                    <asp:TreeNode Text="Severance Benefits Report" Value="SBR"></asp:TreeNode>
                                    <asp:TreeNode Text="Severance Benefits Summery Report" Value="SBSR"></asp:TreeNode>
                                </asp:TreeNode>
                                <asp:TreeNode Text="Increment" Value="Inc">
                                    <asp:TreeNode Text="Increment Report" Value="IR"></asp:TreeNode>
                                    <asp:TreeNode Text="COLA/Performance Increment Letter" Value="CPIL"></asp:TreeNode>
                                </asp:TreeNode>
                                <%--<asp:TreeNode Text="Budget" Value="MB">
                                    <asp:TreeNode Text="Monthly Budget Projection" Value="MBP"></asp:TreeNode>
                                </asp:TreeNode>--%>
                                <%--<asp:TreeNode Text="Accured Vacation" Value="AVL"></asp:TreeNode>--%>
                                <%--<asp:TreeNode Text="NGO Bureau Salary Rpt" Value="NGOBSR"></asp:TreeNode>--%>
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
                            <table style="width: 340px;">
                                <tbody>
                                    <tr>
                                        <td style="width: 431px">
                                            <asp:Panel ID="PSearchBy" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                BorderWidth="1px" Width="420px">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label7" runat="server" Text="Report By :" Width="100px" CssClass="textlevel"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlReportBy" runat="server" Width="127px" CssClass="textlevelleft"
                                                                    OnSelectedIndexChanged="ddlReportBy_SelectedIndexChanged" AutoPostBack="True">
                                                                    <asp:ListItem Value="D">Cost Center</asp:ListItem>
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
                                        <td style="width: 431px">
                                            <asp:Panel ID="P_RptType" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                BorderWidth="1px" Width="420px">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label22" runat="server" Text="Select Report :" Width="100px" CssClass="textlevel"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlRptType" runat="server" Width="127px" CssClass="textlevelleft">
                                                                    <asp:ListItem Value="YPFC">Yearly PF Contribution </asp:ListItem>
                                                                    <asp:ListItem Value="YPFLD">PF Loan Deduction</asp:ListItem>
                                                                    <asp:ListItem Value="AI">Annual Income</asp:ListItem>
                                                                    <asp:ListItem Value="AITD">Yearly Income Tax</asp:ListItem>
                                                                    <asp:ListItem Value="TDR">Tax Deduction Rpt.</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 431px">
                                            <asp:Panel ID="PBranch" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                BorderWidth="1px" Width="420px">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text="Cost Center :" Width="100px" CssClass="textlevel"></asp:Label>
                                                            </td>
                                                            <td colspan="2">
                                                                <asp:DropDownList ID="ddlDivision" runat="server" Width="250px" AutoPostBack="True"
                                                                    CssClass="textlevelleft">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 431px">
                                            <asp:Panel ID="PDiv" runat="server" BorderStyle="Solid" BorderColor="DarkGray" BorderWidth="1px"
                                                Width="420px">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDiv" runat="server" Text="Team :" Width="100px" CssClass="textlevel"></asp:Label>
                                                            </td>
                                                            <td colspan="2">
                                                                <asp:DropDownList ID="ddlSUB" runat="server" Width="250px" AutoPostBack="True" CssClass="textlevelleft">
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
                                            <asp:Panel ID="P_Sector" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                BorderWidth="1px" Width="420px" Visible="False">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label18" runat="server" CssClass="textlevel" Text="Sector :" Width="100px"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlSector" runat="server" Width="140px" CssClass="textlevelleft"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlSector_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 431px">
                                            <asp:Panel ID="PDept" runat="server" BorderStyle="Solid" BorderColor="DarkGray" BorderWidth="1px"
                                                Width="420px" Visible="False">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblLoc" runat="server" Text="Department :" Width="100px" CssClass="textlevel"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlDept" runat="server" Width="250px" AutoPostBack="True" CssClass="textlevelleft">
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
                                            <asp:Panel ID="PPostingDist" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                BorderWidth="1px" Width="420px">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPostingDist" runat="server" Text="Posting District:" Width="100px"
                                                                    CssClass="textlevel"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPostingDist" runat="server" Width="127px" CssClass="textlevelleft">
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
                                            <asp:Panel ID="PClosed" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                BorderWidth="1px" Width="420px">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label9" runat="server" Text="Job Status :" Width="100px" CssClass="textlevel"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlIsClosed" runat="server" Width="127px" CssClass="textlevelleft">
                                                                    <asp:ListItem Value="-1">All</asp:ListItem>
                                                                    <asp:ListItem Selected="True" Value="Regular">Regular</asp:ListItem>
                                                                    <asp:ListItem Value="Contractual">Contractual</asp:ListItem>
                                                                    <asp:ListItem Value="Probation">Probation</asp:ListItem>
                                                                    <asp:ListItem Value="Released">Released</asp:ListItem>
                                                                    <asp:ListItem Value="Removed">Removed</asp:ListItem>
                                                                    <asp:ListItem Value="Resigned">Resigned</asp:ListItem>
                                                                    <asp:ListItem Value="Retired">Retired</asp:ListItem>
                                                                    <asp:ListItem Value="Suspended">Suspended</asp:ListItem>
                                                                    <asp:ListItem Value="Unauthorized Absent">Unauthorized Absent</asp:ListItem>
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
                                            <asp:Panel ID="P_SalaryLocation" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                BorderWidth="1px" Width="420px" Visible="False">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label8" runat="server" Text="Salary Location :" Width="100px" CssClass="textlevel"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlLocation" runat="server" Width="127px" CssClass="textlevelleft"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
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
                                            <asp:Panel ID="P_SalarySubLocation" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                BorderWidth="1px" Width="420px" Visible="False">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label11" runat="server" Text="Salary Division :" Width="100px" CssClass="textlevel"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlSubLoc" runat="server" Width="127px" CssClass="textlevelleft">
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
                                            <asp:Panel ID="P_PayType" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                BorderWidth="1px" Width="420px" Visible="False">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td class="textlevel" style="width: 80px">
                                                                <asp:Label ID="Label12" runat="server" Text="Payment Type:" Width="100px" CssClass="textlevel"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdbSalaryType" runat="server" Font-Names="Tahoma" Font-Size="11px"
                                                                    ForeColor="Blue" RepeatDirection="Horizontal" AutoPostBack="True">
                                                                    <asp:ListItem Selected="True" Value="S">Salary</asp:ListItem>
                                                                    <asp:ListItem Value="B">Only Bonus</asp:ListItem>                                                                    
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PFisY" runat="server" BorderStyle="Solid" BorderColor="DarkGray" BorderWidth="1px"
                                                Width="420px" Visible="False">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 80px">
                                                                <asp:Label ID="Label2" runat="server" Text="Fiscal Year :" Width="100px" CssClass="textlevel"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlFisYear" runat="server" Width="127px" CssClass="textlevelleft">
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
                                            <asp:Panel ID="PMonthFrom" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                BorderWidth="1px" Width="420px" Visible="False">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text="Month :" Width="100px" CssClass="textlevel"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlMonthFrm" runat="server" Width="127px" CssClass="textlevelleft">
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
                                            <asp:Panel ID="PYear" runat="server" BorderStyle="Solid" BorderColor="DarkGray" BorderWidth="1px"
                                                Width="420px" Visible="False">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label10" runat="server" Text="Year :" Width="100px" CssClass="textlevel"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlYear" runat="server" Width="127px" CssClass="textlevelleft">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <!-- Grade -->
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PGrade" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                BorderWidth="1px" Width="420px" Visible="False">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label15" runat="server" Text="Grade :" Width="100px" CssClass="textlevel"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlGrade" runat="server" Width="127px" CssClass="textlevelleft">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <!-- Designation -->
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PDesig" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                BorderWidth="1px" Width="420px" Visible="False">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label16" runat="server" Text="Designation :" Width="100px" CssClass="textlevel"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlDesig" runat="server" Width="127px" CssClass="textlevelleft">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <!-- Employee Type -->
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlEmpType" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                BorderWidth="1px" Width="420px" Visible="False">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label23" runat="server" Text="Employee Type :" Width="100px" CssClass="textlevel"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlEmpType" runat="server" Width="127px" CssClass="textlevelleft">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <!-- from to date -->
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pDate" runat="server" BorderStyle="Solid" BorderColor="DarkGray" BorderWidth="1px"
                                                Width="420px" Visible="False">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 20px">
                                                                <asp:Label ID="Label4" runat="server" Text="From :" Width="100px" CssClass="textlevel"></asp:Label>
                                                            </td>
                                                            <td style="width: 66px">
                                                                <asp:TextBox ID="txtFromDate" runat="server" Width="80px" MaxLength="10"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 70px">
                                                                <a href="javascript:NewCal('<%= txtFromDate.ClientID %>','ddmmyyyy')">
                                                                    <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                                                        height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>
                                                            </td>
                                                            <td style="width: 5px">
                                                                <asp:Label ID="Label5" runat="server" Text="To :" CssClass="textlevel" Width="80px"></asp:Label>
                                                            </td>
                                                            <td style="width: 62px">
                                                                <asp:TextBox ID="txtToDate" runat="server" Width="80px" MaxLength="10"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 99px">
                                                                <a href="javascript:NewCal('<%= txtToDate.ClientID %>','ddmmyyyy')">
                                                                    <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                                                        height="16" alt="Pick a date" src="../../Images/cal.gif" width="16" /></a>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtFromDate"
                                                    ValidChars="/" FilterType="Custom,Numbers" Enabled="true">
                                                </cc1:FilteredTextBoxExtender>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtToDate"
                                                    ValidChars="/" FilterType="Custom,Numbers" Enabled="true">
                                                </cc1:FilteredTextBoxExtender>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PEmpId" runat="server" BorderColor="DarkGray" BorderStyle="Solid"
                                                BorderWidth="1px" Visible="False" Width="420px">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" CssClass="textlevel" Text="Emp Id :" Width="100px"></asp:Label>
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
                                            <asp:Panel ID="P_AV" runat="server" BorderColor="DarkGray" BorderStyle="Solid" BorderWidth="1px"
                                                Visible="False" Width="420px">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label14" runat="server" CssClass="textlevel" Text="Select Voucher:"
                                                                    Width="100px"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlVType" runat="server" Width="250px" CssClass="textlevelleft">
                                                                    <asp:ListItem Selected="True" Value="01">Gross Salary Voucher</asp:ListItem>
                                                                    <asp:ListItem Value="02">Medical Hospital Voucher</asp:ListItem>
                                                                    <asp:ListItem Value="03">Arrear Plus Voucher</asp:ListItem>
                                                                    <asp:ListItem Value="04">Arrear Minus Voucher</asp:ListItem>
                                                                    <asp:ListItem Value="05">Remote Allowance Voucher</asp:ListItem>
                                                                    <asp:ListItem Value="06">Add.Resp. Allowance Voucher</asp:ListItem>
                                                                    <asp:ListItem Value="07">OT Voucher</asp:ListItem>
                                                                    <asp:ListItem Value="08">Child Education Voucher</asp:ListItem>
                                                                    <asp:ListItem Value="09">PF Voucher</asp:ListItem>
                                                                    <asp:ListItem Value="10">PF Loan Vlucher</asp:ListItem>
                                                                    <asp:ListItem Value="11">Tax Voucher</asp:ListItem>
                                                                    <asp:ListItem Value="12">Other Deduction voucher</asp:ListItem>
                                                                    <asp:ListItem Value="13">Gratuity Process & Distributions Voucher</asp:ListItem>
                                                                    <asp:ListItem Value="14">Other Allowance Voucher</asp:ListItem>
                                                                    <asp:ListItem Value="15">L.W.P. Voucher</asp:ListItem>
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
                                            <asp:Panel ID="P_ComText" runat="server" BorderColor="DarkGray" BorderStyle="Solid"
                                                BorderWidth="1px" Visible="False" Width="420px">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblCommon" runat="server" CssClass="textlevel" Text="" Width="100px"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCommon" runat="server" Width="250px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="PGridSalSubLoc" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                            BorderWidth="1px" Width="290px" Visible="False">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:CheckBox runat="server" ID="chkSelectAll" Text="Clear All" AutoPostBack="True"
                                                                                OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <div style="border-right: black 1px solid; border-top: black 1px solid; margin: 15px 0px 0px;
                                                                                overflow: scroll; border-left: black 1px solid; width: 100%; border-bottom: black 1px solid;
                                                                                height: 250px; width: 280px">
                                                                                <asp:GridView ID="grSalDivision" runat="server" Font-Size="9px" EmptyDataText="No Record Found"
                                                                                    AutoGenerateColumns="False" DataKeyNames="ClinicId" Width="260px">
                                                                                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                                                                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                                                                    </SelectedRowStyle>
                                                                                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Select">
                                                                                            <ItemStyle CssClass="ItemStylecss" Width="10%" />
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkBox" runat="Server"></asp:CheckBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="ClinicId" HeaderText="Cost Center ID">
                                                                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="ClinicName" HeaderText="Cost Center Name">
                                                                                            <ItemStyle CssClass="ItemStylecss" Width="80%"></ItemStyle>
                                                                                        </asp:BoundField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="PGridPostDist" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                            BorderWidth="1px" Width="300px" Visible="False">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:CheckBox runat="server" ID="chkPostDistAll" Text="Clear All" AutoPostBack="True"
                                                                                OnCheckedChanged="chkPostDistAll_CheckedChanged" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <div style="border-right: black 1px solid; border-top: black 1px solid; margin: 15px 0px 0px 10px;
                                                                                overflow: scroll; border-left: black 1px solid; width: 100%; border-bottom: black 1px solid;
                                                                                height: 250px; width: 280px">
                                                                                <asp:GridView ID="grPostDivision" runat="server" Font-Size="9px" EmptyDataText="No Record Found"
                                                                                    AutoGenerateColumns="False" DataKeyNames="PostingDistId" Width="260px" Visible="true">
                                                                                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                                                                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                                                                    </SelectedRowStyle>
                                                                                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Select">
                                                                                            <ItemStyle CssClass="ItemStylecss" Width="10%" />
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkBoxPost" runat="Server"></asp:CheckBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="PostingDistId" HeaderText="Posting Division ID">
                                                                                            <ItemStyle CssClass="ItemStylecss" Width="40%"></ItemStyle>
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="PostingDistName" HeaderText="Posting Division Name">
                                                                                            <ItemStyle CssClass="ItemStylecss" Width="50%"></ItemStyle>
                                                                                        </asp:BoundField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PGridEmpList" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                BorderWidth="1px" Width="590px" Visible="False">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox runat="server" ID="chkSelectEmp" Text="Clear All" AutoPostBack="True"
                                                                    OnCheckedChanged="chkSelectAllEmp_CheckedChanged" />
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="radBtnListEmp" runat="server" Font-Names="Tahoma" Font-Size="11px"
                                                                    ForeColor="Blue" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="radBtnListEmp_SelectedIndexChanged">
                                                                    <asp:ListItem Value="M">All</asp:ListItem>
                                                                    <asp:ListItem Selected="True" Value="A">Active</asp:ListItem>
                                                                    <asp:ListItem Value="I">Inactive</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <div style="border-right: black 1px solid; border-top: black 1px solid; margin: 15px 0px 0px;
                                                                    overflow: scroll; border-left: black 1px solid; width: 100%; border-bottom: black 1px solid;
                                                                    height: 250px; width: 380px">
                                                                    <asp:GridView ID="gvEmp" runat="server" Font-Size="9px" EmptyDataText="No Record Found"
                                                                        AutoGenerateColumns="False" DataKeyNames="EmpId" Width="360px">
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
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PanelTax" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                BorderWidth="1px" Width="420px" Visible="False">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rbtTax" runat="server" Font-Names="Tahoma" Font-Size="11px"
                                                                    ForeColor="Blue" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Selected="True" Value="1">With Tax</asp:ListItem>
                                                                    <asp:ListItem Value="0">Without Tax</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PSCEl" runat="server" BorderStyle="Solid" BorderColor="DarkGray" BorderWidth="1px"
                                                Width="420px" Visible="False">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rbtnEmptype" runat="server" AutoPostBack="True" Font-Names="Tahoma"
                                                                    Font-Size="11px" ForeColor="Blue" OnSelectedIndexChanged="rbtnEmptype_SelectedIndexChanged"
                                                                    RepeatDirection="Horizontal">
                                                                    <asp:ListItem Selected="True" Value="A">Active</asp:ListItem>
                                                                    <asp:ListItem Value="I">Inactive</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label13" runat="server" Text="Employee :" Width="80px" CssClass="textlevel"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlEmp" runat="server" CssClass="textlevelleft" Width="300px">
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
                                            <asp:Panel ID="P_SalHead" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                BorderWidth="1px" Width="420px" Visible="False">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label17" runat="server" CssClass="textlevel" Text="Salary Item :"
                                                                    Width="100px"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlSalHead" runat="server" Width="140px" CssClass="textlevelleft">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="-1" runat="server"
                                                                    ControlToValidate="ddlSalHead" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="P_Religion" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                BorderWidth="1px" Width="420px" Visible="False">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label19" runat="server" CssClass="textlevel" Text="Religion :" Width="100px"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlReligion" runat="server" Width="140px" CssClass="textlevelleft"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlReligion_SelectedIndexChanged">
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
                                            <asp:Panel ID="P_Festival" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                BorderWidth="1px" Width="420px" Visible="False">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label20" runat="server" CssClass="textlevel" Text="Festival :" Width="100px"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlFestival" runat="server" Width="140px" CssClass="textlevelleft">
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
                                            <asp:Panel ID="P_Quarter" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                BorderWidth="1px" Width="420px" Visible="False">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label21" runat="server" CssClass="textlevel" Text="Quarter :" Width="100px"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlQuarter" runat="server" CssClass="textlevelleft" Width="100px">
                                                                    <asp:ListItem Value="1">First Quarter</asp:ListItem>
                                                                    <asp:ListItem Value="2">Second Quarter</asp:ListItem>
                                                                    <asp:ListItem Value="3">Third Quarter</asp:ListItem>
                                                                    <asp:ListItem Value="4">Forth Quarter</asp:ListItem>
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
                                            <asp:Panel ID="P_SalSource" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                BorderWidth="1px" Width="420px" Visible="False">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:CheckBox runat="server" ID="chkSalarySource" Text="Clear All" AutoPostBack="True"
                                                                    OnCheckedChanged="chkSalarySourceId_CheckedChanged" />
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <div style="border-right: black 1px solid; border-top: black 1px solid; margin: 15px 0px 0px;
                                                                    overflow: scroll; border-left: black 1px solid; width: 100%; border-bottom: black 1px solid;
                                                                    height: 250px; width: 400px">
                                                                    <asp:GridView ID="gvSalSource" runat="server" Font-Size="9px" EmptyDataText="No Record Found"
                                                                        AutoGenerateColumns="False" DataKeyNames="SalarySourceId" Width="380px">
                                                                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                                                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                                                        </SelectedRowStyle>
                                                                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Select">
                                                                                <ItemStyle CssClass="ItemStylecss" Width="10%" />
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkSalarySourceId" runat="Server"></asp:CheckBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="SalSourceName" HeaderText="Salary Source">
                                                                                <ItemStyle CssClass="ItemStylecss" Width="80%"></ItemStyle>
                                                                            </asp:BoundField>
                                                                        </Columns>
                                                                    </asp:GridView>
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
                                    <tr>
                                        <td>
                                            <asp:Panel ID="P_LT" runat="server" BorderStyle="Solid" BorderColor="DarkGray" BorderWidth="1px"
                                                Width="420px" Visible="False">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td class="textlevel" style="width: 100px">
                                                                <asp:Label ID="Label24" runat="server" Text="Type of Letter:" Width="80px" CssClass="textlevel"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlTypeOfletter" runat="server" Width="127px" CssClass="textlevelleft">
                                                                    <asp:ListItem Selected="True" Value="I">Increment</asp:ListItem>
                                                                    <asp:ListItem Value="C">COLA</asp:ListItem>
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
                                            <asp:Panel ID="P_Date" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                BorderWidth="1px" Width="420px" Visible="False">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 20px">
                                                                <asp:Label ID="Label25" runat="server" Text="Print Date :" Width="80px" CssClass="textlevel"></asp:Label>
                                                            </td>
                                                            <td style="width: 66px">
                                                                <asp:TextBox ID="txtPrintDate" runat="server" Width="80px" MaxLength="10"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 70px">
                                                                <a href="javascript:NewCal('<%= txtPrintDate.ClientID %>','ddmmyyyy')">
                                                                    <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                                                        height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>
                                                            </td>
                                                            <td style="width: 5px">
                                                            </td>
                                                            <td style="width: 62px">
                                                            </td>
                                                            <td style="width: 99px">
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtPrintDate"
                                                    ValidChars="/" FilterType="Custom,Numbers" Enabled="true">
                                                </cc1:FilteredTextBoxExtender>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div style="padding-right: 15px; float: left; width: 428px">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="width: 220px">
                                            </td>
                                            <td style="width: 3px">
                                                <asp:Panel ID="PShow" runat="server" Visible="False">
                                                    <asp:Button ID="btnShow" OnClick="btnShow_Click" runat="server" Text="Show Report"
                                                        Font-Underline="False"></asp:Button>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                objMasMgr.SelectGrade(0), ddlGrade</fieldset>
        </div>
    </div>
</asp:Content>
