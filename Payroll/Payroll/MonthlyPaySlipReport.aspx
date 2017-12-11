<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MonthlyPaySlipReport.aspx.cs"
    Inherits="Payroll_Payroll_MonthlyPaySlipReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Monthly Payslip</title>
    </head>
<body>
    <form id="form1" runat="server">
    <div style="width: 60%;">
        <table style="width: 30%;">            
            <tr>
                <td>
                    <asp:Image ID="Image1" ImageUrl="~/Images/MSB-Logo.jpg" Width="168px" Height="60px"
                        runat="server" />
                    </td>
            </tr>
            <tr>
                <td style="text-align:center;width: 30%; font-weight: 700;">Pay Slip</td>
            </tr>
        </table>
    </div>
    <div style="margin-top: 20px; font-size: 14px; font-family: Tahoma; font-weight: bold;">
        <asp:Label ID="lblMonth" runat="server" Text=""></asp:Label>
    </div>
    <div style="margin-top: 20px; font-size: 14px; font-family: Tahoma;" >
     <table style="width: 70%;">
        <tr>
            <td style="width: 15%;">Employee ID</td>
            <td  style="width: 3%;">:</td>
            <td style="width: 25%;">
                <asp:Label ID="lblID" runat="server" Text="" /></td>
            <td style="width: 15%;">Name</td>
            <td style="width: 3%;">:</td>
            <td style="width: 40%;"> <asp:Label ID="lblName" runat="server" Text=""/></td>
        </tr>
        <tr>
            <td>Grade</td>
            <td>:</td>
            <td> <asp:Label ID="lblGrade" runat="server" Text="" /></td>
            <td>Location Category</td>
            <td>:</td>
            <td> <asp:Label ID="lblJobTitle" runat="server" Text="" /></td>
        </tr>
        <tr>
            <td>Bank A/C No</td>
            <td>:</td>
            <td> <asp:Label ID="lblBankAc" runat="server" Text="" /></td>
            <td>Designation</td>
            <td>:</td>
            <td> <asp:Label ID="lblDesig" runat="server" Text="" /></td>
        </tr>
        <tr>
            <td>Organization</td>
            <td>:</td>
            <td> <asp:Label ID="lblPostDiv" runat="server" Text="" /></td>
            <td>Department</td>
            <td>:</td>
            <td> <asp:Label ID="lblDept" runat="server" Text="" /></td>
        </tr>
        <tr>
            <td>Project</td>
            <td>:</td>
            <td> <asp:Label ID="lblPostDist" runat="server" Text="" /></td>
            <td>Clinic</td>
            <td>:</td>
            <td> <asp:Label ID="lblSalLoc" runat="server" Text="" /></td>
        </tr>
        </table>
    </div>
    <div style="width: 90%;">
        <table style="width: 80%;">
            <tr>
                <td style="width: 70%;">
                    <asp:GridView ID="grGrossandBenefits" runat="server" AutoGenerateColumns="False"
                        ShowHeader="true" EmptyDataText="No Record Found" Font-Size="12px" DataKeyNames="HTYPE"
                        Width="100%">
                        <HeaderStyle BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray" />
                        <RowStyle Font-Size="12px" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray" />
                        <Columns>
                            <asp:BoundField DataField="HeadName" HeaderText="Salary Items" HeaderStyle-BorderWidth="1px"
                                HeaderStyle-BorderStyle="solid" HeaderStyle-BorderColor="dimgray" HeaderStyle-HorizontalAlign="Left">
                                <ItemStyle Width="80%" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PAYAMT" HeaderText="Taka" HeaderStyle-HorizontalAlign="right"
                                HeaderStyle-BorderWidth="1px" HeaderStyle-BorderStyle="solid" HeaderStyle-BorderColor="dimgray">
                                <ItemStyle Width="20%" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray"
                                    HorizontalAlign="right" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
                <td style="width: 0%; vertical-align: top;">
                    <asp:GridView ID="grDeduct" runat="server" AutoGenerateColumns="False" ShowHeader="true"
                        Visible="false" EmptyDataText="No Record Found" Font-Size="12px" DataKeyNames="HTYPE"
                        Width="0%">
                        <HeaderStyle BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray" />
                        <RowStyle Font-Size="12px" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray" />
                        <Columns>
                            <asp:BoundField DataField="HeadName" HeaderText="Deduction(s)" HeaderStyle-BorderWidth="1px"
                                HeaderStyle-BorderStyle="solid" HeaderStyle-BorderColor="dimgray" HeaderStyle-HorizontalAlign="Left">
                                <ItemStyle Width="70%" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PayAmt" HeaderText="Taka" HeaderStyle-HorizontalAlign="right"
                                HeaderStyle-BorderWidth="1px" HeaderStyle-BorderStyle="solid" HeaderStyle-BorderColor="dimgray">
                                <ItemStyle Width="30%" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray"
                                    HorizontalAlign="right" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <br />
                    <asp:GridView ID="grNetPay" runat="server" AutoGenerateColumns="False" ShowHeader="false"
                        Visible="false" EmptyDataText="No Record Found" Font-Size="12px" Width="0%">
                        <HeaderStyle BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray" />
                        <RowStyle Font-Size="12px" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray" />
                        <Columns>
                            <asp:BoundField DataField="HeadName" HeaderText="Deduction(s)" HeaderStyle-BorderWidth="1px"
                                HeaderStyle-BorderStyle="solid" HeaderStyle-BorderColor="dimgray" HeaderStyle-HorizontalAlign="Left">
                                <ItemStyle Width="70%" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray"
                                    Font-Bold="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PayAmt" HeaderText="Taka" HeaderStyle-HorizontalAlign="right"
                                HeaderStyle-BorderWidth="1px" HeaderStyle-BorderStyle="solid" HeaderStyle-BorderColor="dimgray">
                                <ItemStyle Width="30%" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray"
                                    HorizontalAlign="right" Font-Bold="true" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                   
                    
                    <br />
                    <asp:Label ID="lblRemarks" runat="server" Font-Size="12px" Font-Names="Tahoma"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="font-size: 11px; font-family: Verdana; font-style:italic;">
                 <asp:Label ID="lblTakaInWord" runat="server" Font-Size="12px" Font-Names="Tahoma"></asp:Label>
                    <br />
                    <br />
                    Note: This is a computer generated payslip and does not require any signature. If
                    any discrepancy is found please inform HR Team within 7 days of the issuance.
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
