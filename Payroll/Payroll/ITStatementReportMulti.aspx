<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ITStatementReportMulti.aspx.cs"
    Inherits="Payroll_Payroll_ITStatementReportMulti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IT Statement</title>
    <style type="text/css">
        P.breakhere
        {
            page-break-before: always;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 95%; text-align: left; font-size: 12px;">
        <asp:Label ID="lblDate" runat="server" Text=""></asp:Label></div>
    <div style="width: 95%; font-family: Arial; font-size: 14px; text-align: center;
        font-weight: bold; margin-bottom: 20px; margin-top: 140px;">
        Annual salary certificate and advance tax deduction for the financial year &nbsp;
        <asp:Label ID="lblFisYear" runat="server" Text=""></asp:Label>
        <br />
    </div>
    <div style="width: 80%; font-family: Arial; font-size: 12px; text-align: justify;
        margin-left: 80px;">
        This is to certify that &nbsp;<asp:Label ID="lblEmp" runat="server" Text=""></asp:Label>
        <%--TIN No.&nbsp;<asp:Label ID="lblTIN" runat="server" Text=""></asp:Label> &nbsp; is an employee --%>
        of SCI Bd has been paid a sum Tk.
        <asp:Label ID="TotalAmt" runat="server" Text=""></asp:Label>
        as salary and allowances during the fiscal year
        <asp:Label ID="lblFisYear1" runat="server" Text=""></asp:Label>&nbsp; (Corresponding
        Assessment year &nbsp;<asp:Label ID="lblFisYear2" runat="server" Text=""></asp:Label>&nbsp;
        ) which comprises:
        <br />
    </div>
    <div style="width: 99%; font-family: Arial; font-size: 12px;">
        <div style="margin-left: 80px;">
            <asp:GridView ID="grPayroll" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                Font-Size="12px" DataKeyNames="" Font-Names="Arial" Width="80%" ShowHeader="true"
                ShowFooter="true" GridLines="None">
                <RowStyle Font-Size="12px" Font-Names="Arial" />
                <FooterStyle Font-Bold="true" Font-Size="12px" Font-Names="Arial" Height="30px" />
                <Columns>
                    <asp:BoundField DataField="HEADNAME" HeaderText="" HeaderStyle-HorizontalAlign="Left">
                        <ItemStyle Width="70%" HorizontalAlign="Left" Height="15px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="" HeaderText="" HeaderStyle-HorizontalAlign="Left">
                        <ItemStyle Width="0%" HorizontalAlign="Right" Height="15px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PAYAMT" HeaderText="Amount in Tk." HeaderStyle-HorizontalAlign="right">
                        <ItemStyle Width="30%" HorizontalAlign="right" Height="15px" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <table width="80%" font-size="12px">
                <tr>
                    <td width="70%" horizontalalign="Left" height="20px">
                        PF (Both Employer and Employee Contribution)
                    </td>
                    <td width="0%" horizontalalign="Right" height="20px">
                    </td>
                    <td width="30%" align="right" height="20px">
                        <asp:Label ID="lblPFBoth" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        &nbsp;<asp:Label ID="lblPayrollInWord" runat="server" Width="793px"></asp:Label>
    </div>
    <div style="width: 80%; font-family: Arial; font-size: 12px; text-align: justify;
        margin-top: 5px; margin-left: 80px;">
        As per Income-Tax Ordinance 1984,we have deducted advance tax of Tk.
        <asp:Label ID="lblTTaxPaid" runat="server" Text=""></asp:Label>&nbsp; which has
        been deposited in her/his name to the national exchequer (Government of Bangladesh)
        through the following Bangladesh Bank challan/receipt numbers during the said fiscal
        year:<br />
        <%--   Also certified that as per the Income Tax Ordinance 1984, that we have deducted advance Tax at source and deposited into 
     Bangladesh Bank in &nbsp;<asp:Label ID="lblGender" runat="server" Text=""></asp:Label>&nbsp;favor in accordance with the following Challan # during the said fiscal year:-
        --%>
        <%-- During the income year amount of Taka &nbsp;<asp:Label ID="lblTotalChallanAmt" runat="server" Text=""></asp:Label>&nbsp;(
     <asp:Label ID="lblChallanInWord" runat="server" Text=""></asp:Label>)&nbsp; was deducted from 
    </asp:Label>&nbsp;salary as tax and that amount was duly deposited into
     <asp:Label ID="lblChallanBankName" runat="server" Text=""></asp:Label>.
     Details of the deposit are given below:--%>
    </div>
    <div style="width: 99%; font-family: Arial; font-size: 12px; margin-top: 2px;">
        <div style="margin-left: 80px;">
            <asp:GridView ID="grChallan" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                Font-Size="12px" DataKeyNames="VYEAR" Font-Names="Arial" Width="80%" ShowHeader="true"
                ShowFooter="true" GridLines="None">
                <RowStyle Font-Size="12px" Font-Names="Arial" />
                <FooterStyle Font-Bold="true" Font-Size="12px" Font-Names="Arial" Height="30px" />
                <Columns>
                    <asp:BoundField DataField="CHALLANNO" HeaderText="Challan No." HeaderStyle-HorizontalAlign="Left"
                        HeaderStyle-Font-Underline="true">
                        <ItemStyle Width="25%" HorizontalAlign="Left" Height="15px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CHALLANDATE" HeaderText="Date" HeaderStyle-HorizontalAlign="Left"
                        HeaderStyle-Font-Underline="true">
                        <ItemStyle Width="25%" HorizontalAlign="Left" Height="15px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="VMONTH" HeaderText="Month" HeaderStyle-HorizontalAlign="Left"
                        HeaderStyle-Font-Underline="true">
                        <ItemStyle Width="25%" HorizontalAlign="Left" Height="15px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="" HeaderText="" HeaderStyle-HorizontalAlign="right" HeaderStyle-Font-Underline="true"
                        Visible="false">
                        <ItemStyle Width="0%" HorizontalAlign="right" Height="15px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PAYAMT" HeaderText="Amount in Tk." HeaderStyle-HorizontalAlign="right"
                        HeaderStyle-Font-Underline="true">
                        <ItemStyle Width="25%" HorizontalAlign="right" Height="15px" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div style="width: 80%; font-family: Arial; font-size: 12px; text-align: justify;
        margin-top: 5px; margin-left: 80px;">
        We are advising the concern staff to enclose this income certificate along with
        the prescribed income tax return while submitting to the commissioner of taxes before
        September 30, 2016
    </div>
    <div style="width: 30%; font-family: Arial; font-size: 12px; margin-top: 5px; float: right;">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/EmpImage/SCB_TaxSign.jpg" />
        <br />
        Rosalind Hawlader
        <br />
        Director HR and Administration
        <br />
        Bangladesh Country Office
    </div>
    </form>
</body>
</html>
