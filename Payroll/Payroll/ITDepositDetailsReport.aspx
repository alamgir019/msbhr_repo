<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="ITDepositDetailsReport.aspx.cs" Inherits="Payroll_Payroll_ITDepositDetailsReport"
    Title="IT Deposit Details Reports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js"></script>

    <script language="javascript" type="text/javascript">
function printDiv(divName) 
{
     var printContents = document.getElementById(divName).innerHTML;
     var originalContents = document.body.innerHTML;

     document.body.innerHTML = printContents;

     window.print();

     document.body.innerHTML = originalContents;
}

    </script>

    <div class="formStyle" style="width: 200%;">
        <div id="formhead1">
            <div style="width: 98%; float: left;">
                IT Deposit Reports with Yearly Benefits Amount</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Fuchsia"></asp:Label>
        </div>
        <div class="Div950">
            <fieldset>
                <table>
                    <tr>
                        <td class="textlevel">
                            Select Group :</td>
                        <td>
                            <asp:DropDownList ID="ddlGroup" runat="server" Width="270px" CssClass="textlevel">
                            </asp:DropDownList></td>
                        <td class="textlevel">
                            Office :</td>
                        <td>
                            <asp:DropDownList ID="ddlLocation" runat="server" Width="270px" CssClass="textlevel">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Fiscal Year :</td>
                        <td style="">
                            <asp:DropDownList ID="ddlFinYear" runat="server" Width="270px" CssClass="textlevel">
                            </asp:DropDownList></td>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnGetEmployee" runat="server" Text="Get IT Records" Width="184px"
                                OnClick="btnGetEmployee_Click" /></td>
                    </tr>
                    <tr>
                        <td class="textlevelshort">
                        </td>
                        <td>
                            <asp:Button ID="btnPrintTop" runat="server" Text="Print" OnClientClick="printDiv('PrintMe')"
                                Width="120px" />
                            <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Width="120px" OnClick="btnExport_Click" /></td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <div style="text-align: right; width: 99.99%;">
                </div>
                <div id="PrintMe" style="width: 99.99%;">
                    <div style="font-family: Arial; font-size: 20px; margin-left: 5px;">
                        Marie Stopes
                    </div>
                    <div style="font-family: Arial; font-size: 14px; margin-left: 5px;">
                        Detail Income tax Deduction at Source from Staff Salary and Deposited to Govt. Treasury
                        for
                        <asp:Label ID="lblFiscalYear" runat="server" Text=""></asp:Label>
                    </div>
                    <br />
                    <asp:GridView ID="grEmployee" runat="server" EmptyDataText="No Record Found" Width="99.99%"
                        AutoGenerateColumns="true" DataKeyNames="" Font-Size="12px" Font-Names="Arial"
                        ShowFooter="true">
                        <HeaderStyle Font-Bold="True" HorizontalAlign="center" BackColor="LightGray" Font-Size="12px"
                            Font-Names="Arial"></HeaderStyle>
                        <FooterStyle Font-Bold="True" HorizontalAlign="center" BackColor="LightGray" Font-Size="12px"
                            Font-Names="Arial"></FooterStyle>
                    </asp:GridView>
                </div>
                <div style="text-align: right; width: 99.99%;">
                    <asp:Button ID="btnPrintBottom" runat="server" Text="Print" OnClientClick="printDiv('PrintMe')"
                        Width="100px" />
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
