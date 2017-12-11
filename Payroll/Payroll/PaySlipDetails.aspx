<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaySlipDetails.aspx.cs" Inherits="Payroll_Payroll_PaySlipDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payslip Details View</title>
    <style type="text/css">
        .TextBoxAmt100 {
	    width:100px;
	    border: #666666;
	    border-style: solid;
	    border-top-width: 2px;
	    border-right-width: 2px;
	    border-bottom-width: 1px;
	    border-left-width: 2px;
	    text-align:right;
        }
        .textlevel {
	    float:left;
	    width:120px;
	    color : #09086E;
	    text-align:right;
	    font-size : 11px;
	    font-family :tahoma;
	    font-variant:normal;
	    font-style:normal;
	    font-variant:normal;
	    font-weight:normal;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div style="text-align: center;">
            <div style="width: 65%;">
                <fieldset style="background-color: #EFF3FB; text-align: left;">
                    <table>
                        <tr>
                            <td style="width: 10%">
                                <asp:Label ID="Label2" runat="server" Text="Employee : " CssClass="textlevel"></asp:Label></td>
                            <td style="width: 40%">
                                <asp:Label ID="lblName" runat="server" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                            </td>
                            <td style="width: 10%">
                                <asp:Label ID="Label1" runat="server" Text="Salary Packgae : " CssClass="textlevel"></asp:Label></td>
                            <td style="width: 40%">
                                <asp:Label ID="lblSalPack" runat="server" Font-Names="Tahoma" Font-Size="12px"> </asp:Label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <div style="width: 65%;">
                <asp:GridView ID="grPaySlipDetls" runat="server" ShowFooter="true" DataKeyNames="PSBookID,EmployeeID,SalHeadID,IsOtherPayment,
                IsAdvanceDeducttion,IsLateDeduction,PFAmount,IsAttndBonus,IsProductionBonus,IsOT,OTHour,IsPFLoanDeduction,
                IsFestivalBonus,IsArea" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                    Font-Size="9px" Width="99.98%">
                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" HorizontalAlign="Left" Font-Size="12px"
                        Font-Names="Tahoma"></HeaderStyle>
                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                    </SelectedRowStyle>
                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                    <RowStyle Font-Size="12px" HorizontalAlign="left" Font-Names="Tahoma" />
                    <FooterStyle BackColor="#B3CDE4" Font-Bold="True" HorizontalAlign="right" Font-Size="12px"
                        Font-Names="Tahoma"></FooterStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="SL">
                            <ItemStyle Width="4%" />
                            <ItemTemplate>
                                <asp:Label ID="lblSL" Text="" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SalHeadTitle" HeaderText="Salary Head">
                            <ItemStyle CssClass="ItemStylecss" Width="40%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="IsBasicSal" HeaderText="Is Basic">
                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Pay Amount">
                            <ItemStyle Width="11%" HorizontalAlign="left" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtPayAmnt" Text='<%# Convert.ToString(Eval("PayAmnt")) %>' runat="server"
                                    Width="95%" Style="text-align: right;"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="fltbPayAmnt" runat="server" TargetControlID="txtPayAmnt"
                                    FilterType="Custom,Numbers" ValidChars=".,-">
                                </cc1:FilteredTextBoxExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="IsDeducted" HeaderText="Is Deduction">
                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="IsProvidentFund" HeaderText="Is Provident Fund">
                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            <br />
            <asp:Button ID="btnAccept" runat="server" OnClick="btnAccept_Click" Text="Accept Changes" />&nbsp;</div>
    </form>
</body>
</html>
