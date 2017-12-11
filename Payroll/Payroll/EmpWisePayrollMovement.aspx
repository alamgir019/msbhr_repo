<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="EmpWisePayrollMovement.aspx.cs" Inherits="Payroll_Payroll_EmpWisePayrollMovement"
    Title="Payroll Movement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js"></script>
    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js"></script>
    <script language="javascript" type="text/javascript">


        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }


        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }


    </script>
    <div class="formStyle">
        <div id="formhead1">
            <div style="width: 98%; float: left;">
                Employee Wise Payroll Movement Report</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" Width="500px" Font-Bold="True" ForeColor="Fuchsia"></asp:Label>
        </div>
        <div class="Div950">
            <fieldset>
                <table>
                    <tr>
                        <td class="textlevel">
                            Fiscal Year :
                        </td>
                        <td style="height: 24px">
                            <asp:DropDownList ID="ddlFiscalYear" runat="server" Width="150px" AutoPostBack="True"
                                CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                            Select Employee :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEmployee" runat="server" Width="250px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="font-size: 11px" colspan="2">
                            <asp:CheckBox ID="chkBonus" runat="server" Text="Only Bonus" Visible="False" />
                        </td>
                        <td colspan="2">
                            <asp:Button ID="btnGenerate" runat="server" Text="Generate Report" Width="184px"
                                OnClick="btnGenerate_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <hr style="border: solid 1px #3399FF;" />
            <div style="text-align: left; width: 100%">
                <asp:Button ID="btnPrintTop" runat="server" Text="Print" Width="100px" OnClientClick="printDiv('PrintMe')" />
                <asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel File" Width="200px"
                    ForeColor="blue" OnClick="btnExportExcel_Click" />
            </div>
            <fieldset>
                <div id="PrintMe" style="width: 99.99%; page-break-before: always; height: 100%;">
                    <table width="100%" style="border-collapse: collapse; border: solid 1px white;">
                        <tr>
                            <td style="width: 40%; text-align: left; font-family: Arial; font-size: 20px; padding-left: 5px;">
                                Marie Stopes
                            </td>
                            <td rowspan="3" style="width: 60%;" valign="top">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 40%; font-family: Arial; font-size: 14px; padding-left: 5px;">
                                <asp:Label ID="lblGenerateFor" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-family: Arial; font-size: 14px; padding-left: 5px;">
                                <asp:Label ID="lblPayrollMonth" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:GridView ID="grPayroll" runat="server" EmptyDataText="No Record Found" Font-Size="10px"
                        Width="99%" Font-Names="Arial" AutoGenerateColumns="true" ShowFooter="true" OnRowCommand="grPayslipMst_RowCommand">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <FooterStyle BackColor="#B3CDE4" Font-Bold="True"></FooterStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                    </asp:GridView>
                </div>
            </fieldset>
            <div style="text-align: left;">
                <asp:Button ID="btnPrintBottom" runat="server" Width="100px" Text="Print" OnClientClick="printDiv('PrintMe')" /></div>
        </div>
    </div>
</asp:Content>
