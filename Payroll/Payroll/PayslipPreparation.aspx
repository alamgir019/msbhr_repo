<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="PayslipPreparation.aspx.cs" Inherits="Payroll_Payroll_PayslipPreparation"
    Title="Payslip Preparation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js"></script>
    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js"></script>
    <script language="javascript" type="text/javascript">
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }

    </script>
    <div class="formStyle">
        <div id="formhead1">
            <div style="width: 98%; float: left;">
                Payroll Process</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Fuchsia"></asp:Label>
        </div>
        <div class="Div950">
            <div style="background-color: #EFF3FB; border: solid 1px #3E506C;">
                <table>
                    <tr>
                        <td class="textlevel">
                            Fiscal Year :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFiscalYear" runat="server" Width="130px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                            Fiscal Year (Tax) :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFiscalYearTax" runat="server" Width="130px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                            Fiscal Year (PF) :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFiscalYearPF" runat="server" Width="130px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                            Cost Center :</td>
                        <td>
                            <asp:DropDownList ID="ddlCostCenter" runat="server" Width="150px" 
                                CssClass="textlevelleft">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Month :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" runat="server" Width="130px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                            Year :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlYear" runat="server" Width="130px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                            Employee Status :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEmpStatus" runat="server" Width="130px" CssClass="textlevelleft">
                                <asp:ListItem Value="A">Active</asp:ListItem>
                                <asp:ListItem Value="I">In Active</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                            </td>
                        <td>
                            <asp:Button ID="btnGenerate" runat="server" Text="Generate" Width="130px" 
                                OnClick="btnGenerate_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Process Date :
                        </td>
                        <td>
                            <asp:TextBox ID="txtIssueDate" runat="server" Width="80px"></asp:TextBox>
                            <a href="javascript:NewCal('<%= txtIssueDate.ClientID %>','ddmmyyyy')">
                                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                    height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>
                        </td>
                        <td class="textlevel">
                            Emp Id :</td>
                        <td>
                            <asp:TextBox ID="txtEmpId" runat="server" Width="80px"></asp:TextBox>
                        </td>
                        <td class="textlevel">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:TextBox ID="txtPercentage" MaxLength="3" runat="server" Width="80px" 
                                Style="text-align: right;" Visible="False">100</asp:TextBox>
        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
            TargetControlID="txtPercentage">
        </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                </table>
            </div>
            <hr style="border: solid 1px #3399FF;" />
            <div style="border: solid 1px #3E506C;">
                <div style="width: 99%; overflow: scroll; height: 420px;">
                    <asp:GridView ID="grPayslipMst" runat="server" EmptyDataText="No Record Found" Font-Size="9px"
                        Width="100%" AutoGenerateColumns="False" DataKeyNames="PSBookID,PayID,LateDayCount,LateSalDeductCount,LateSalHeadID,
                        IsAllowedForAttBonus,OTAmnt,OTIsInPercent,OTSalHead,BonusPackID,IsConvertCurrency,CurrencyID,CurrencyConvAmnt,
                        LateDedASOTDEDHouR,LateDedASOTDEDAMt,SalPakID,PostingPlaceId,BANKCODE,RoutingNo,BankAccNo,DeptId,DesigId,EMPTYPEID,PLANACCLINE,ClinicId,DivisionId"
                        OnRowCommand="grPayslipMst_RowCommand" 
                        onselectedindexchanged="grPayslipMst_SelectedIndexChanged">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:ButtonField CommandName="DoubleClick" HeaderText="Details" Text="Details">
                                <ItemStyle Width="3%" CssClass="ItemStylecss" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="EmployeeID" HeaderText="Emp ID">
                                <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Empname" HeaderText="Name">
                                <ItemStyle CssClass="ItemStylecss" Width="11%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JobTitleName" HeaderText="Designation">
                                <ItemStyle CssClass="ItemStylecss" Width="11%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="DeptName" HeaderText="Department">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="StartDate" HeaderText="From">
                                <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="EndDate" HeaderText="To">
                                <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalDays" HeaderText="Month Days" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle CssClass="ItemStylecssCenter" Width="4%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TWorkingDayHour" HeaderText="Salary Days" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle CssClass="ItemStylecssCenter" Width="4%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PackageAmount" HeaderText="Net Pay" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle CssClass="ItemStylecssCenter" Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="GrossAmount" HeaderText="Gross Amount" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle CssClass="ItemStylecssCenter" Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="IsIrregular" HeaderText="Is Irregular" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle CssClass="ItemStylecssCenter" Width="4%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PaySlipSatus" HeaderText="Status" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle CssClass="ItemStylecssCenter" Width="4%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SalPayType" HeaderText="Pay Type" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle CssClass="ItemStylecssCenter" Width="4%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SPTitle" HeaderText="Salary Package">
                                <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="IsWithBonus" HeaderText="Is With Bonus" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle CssClass="ItemStylecssCenter" Width="4%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="IsOnlyBonus" HeaderText="Is Only Bonus" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle CssClass="ItemStylecssCenter" Width="4%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
                <span style="color: #3366CC; font-size: 12px; font-family: Tahoma; font-weight: bold;">
                    Total Records:
                    <asp:Label ID="lblRecordCount" runat="server" Text=""></asp:Label>
                </span>
            </div>
            <div id="DivCommand1" style="padding-top: 3px;">
                <div style="text-align: left; float: left">
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px" CausesValidation="False"
                        Visible="False" />
                </div>
                <div style="text-align: right;">
                    <asp:CheckBox ID="CheckBox1" CssClass="textlevelleft" runat="server" 
                        Text="Want to send Email to Verify" Visible="False" />
                    <asp:Button ID="btnSave" runat="server" Text="Prepare" Width="70px" UseSubmitBehavior="False"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
