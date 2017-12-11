<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="ITCalculationReport.aspx.cs" Inherits="Payroll_Payroll_ITCalculationReport"
    Title="IT Calculation Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
         function ToUpper(ctrl)
         {   
            var t = ctrl.value;
            ctrl.value = t.toUpperCase(); 
         }
        function printDiv(divName) 
        {
             var printContents = document.getElementById(divName).innerHTML;
             var originalContents = document.body.innerHTML;

             document.body.innerHTML = printContents;

             window.print();

             document.body.innerHTML = originalContents;
        }
    </script>

    <div class="formStyle" style="width: 150%;">
        <div id="formhead1">
            <div style="width: 98%; float: left;">
                IT Calculation Report</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" Width="500px" Font-Bold="True" ForeColor="Fuchsia"></asp:Label>
        </div>
        <div class="Div950">
            <div style="background-color: #EFF3FB;">
                <fieldset>
                    <table>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td style="text-align: right;">
                             <%--   <asp:RequiredFieldValidator ID="rfEmpID" runat="server" ControlToValidate="txtEmpID"
                                    ErrorMessage="*"></asp:RequiredFieldValidator></td>--%>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Month :</td>
                            <td>
                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="textlevel" Width="80px">
                                </asp:DropDownList></td>
                            <td class="textlevel">
                                Fiscal Year :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFiscalYear" runat="server" CssClass="textlevel" Width="100px">
                                </asp:DropDownList></td>
                            <td class="textlevel">
                                Employee :</td>
                            <td>
                                <asp:TextBox ID="txtEmpID" runat="server" Width="80px"></asp:TextBox></td>
                            <td>
                                <asp:ImageButton ID="imgBtnSearch" runat="server" Height="19px" ImageUrl="~/Images/Search_Icon.jpg"
                                    Width="21px" CausesValidation="False" OnClick="imgBtnSearch_Click" /></td>
                            <td>
                                <asp:Button ID="btnIndReport" runat="server" Text="IT Calculation Report" OnClick="btnIndReport_Click" /></td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="3">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <div>
                <fieldset>
                    <legend>Employee List</legend>
                    <div style="background-color: Gray; text-align: left;">
                        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="printDiv('PrintMe')"
                            Width="200px" Height="30px" />
                        <asp:Button ID="btnExport" runat="server" Font-Bold="true" Height="30px" Text="Export to Excel"
                            CausesValidation="false" OnClick="btnExport_Click" />
                    </div>
                    <div id="PrintMe" style="margin-top: 10px; width: 100%;">
                        <asp:GridView ID="grEmployee" runat="server" EmptyDataText="No Record Found" Font-Size="9px"
                            AutoGenerateColumns="False" DataKeyNames="" Width="100%">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                            </SelectedRowStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                            <Columns>
                                <asp:BoundField DataField="" HeaderText="SL">
                                    <ItemStyle CssClass="ItemStylecss" Width="2%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMPCODE" HeaderText="EmpCode">
                                    <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Staffname" HeaderText="Staffname">
                                    <ItemStyle CssClass="ItemStylecss" Width="8%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Designation" HeaderText="Designation">
                                    <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TIN" HeaderText="TIN">
                                    <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Sex" HeaderText="Sex">
                                    <ItemStyle CssClass="ItemStylecss" Width="2%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="LOC" HeaderText="LOC">
                                    <ItemStyle CssClass="ItemStylecss" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="JoiningDate" HeaderText="JoiningDate">
                                    <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="YBasicSalary" HeaderText="YBasicSalary" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="YHouseRent" HeaderText="YHouseRent" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="T_HA" HeaderText="T_HA" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="YMedicalAllowance" HeaderText="YMedicalAllowance" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="YTransportAllowance" HeaderText="YTransportAllowance"
                                    HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="T_TA" HeaderText="T_TA" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="YFieldAllowance" HeaderText="YFieldAllowance" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="YFestivalBonus" HeaderText="YFestivalBonus" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="YOtherallowance" HeaderText="YOtherallowance" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TTI_1" HeaderText="TTI_1" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Rebate" HeaderText="Rebate" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="YPFDeduction" HeaderText="YPFDeduction" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TTI_2" HeaderText="TTI_2" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Z_M_F" HeaderText="Z_M_F" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="P10" HeaderText="P10" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="P15" HeaderText="P15" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="P20" HeaderText="P20" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="P25" HeaderText="P25" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="G_Tax" HeaderText="G_Tax" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="NetTax" HeaderText="NetTax" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="LastYrRefund" HeaderText="Last Yr Refund" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="MonthlyTax" HeaderText="Monthly Tax" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ITDeposited" HeaderText="ITDeposited" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Demand" HeaderText="Demand" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Refund" HeaderText="Refund" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="3%"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </fieldset>
                <span style="color: #3366CC; font-size: 12px; font-family: Tahoma; font-weight: bold;">
                    Total Records:
                    <asp:Label ID="lblRecordCount" runat="server" Text=""></asp:Label>
                </span>
            </div>
        </div>
    </div>
</asp:Content>
