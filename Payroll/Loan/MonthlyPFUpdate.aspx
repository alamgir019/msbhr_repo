<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="MonthlyPFUpdate.aspx.cs" Inherits="Payroll_Loan_MonthlyPFUpdate" Title="Monthly PF Update" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <script language ="javascript" type="text/javascript" src ="../../JScripts/Confirmation.js">
</script>
<div id="PayrollConfigForm2">
     <div id="formhead1">
      <div style="width:92%;float:left;">Monthly PF Update </div>
        <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../File/Home.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
    </div>
    <div class="MsgBox">
      <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
    </div>
    <div id="PayrollConfigInner">
        <div style="background-color:#EFF3FB;margin-top:20px;">
            <fieldset>
                This process will update Loan, Deduction, Interest, PF Adjustment, Cashpay of salary file. The process will alos check
                the last PF payment and calculate its interest.
            </fieldset>
        </div>
        <hr />
        <fieldset style="margin-top:10px;">
            <legend>Monthly Provident Fund Activities</legend>
            <table>
                <tr>
                    <td class="textlevel">PF Month</td>
                    <td style="width: 108px"><asp:DropDownList ID="ddlMonth" runat="server" Width="105px"></asp:DropDownList></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="textlevel">PF Year</td>
                    <td style="width: 108px"><asp:DropDownList ID="ddlYear" runat="server" Width="105px"></asp:DropDownList></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="textlevel">Fiscal Year</td>
                    <td style="width: 108px"><asp:DropDownList ID="ddlFiscalYear" runat="server" Width="105px"></asp:DropDownList></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="textlevel">
                        <asp:Button ID="btnUpdatePFLedger" runat="server" Text="Update PF Ledger" OnClick="btnUpdatePFLedger_Click"  /></td>
                    <td style="text-align:right; width: 108px;"></td>
                    <td style="text-align: right">
                        <asp:Button ID="btnUpdatePFLoan" runat="server" Text="Update PF Loan Ledger" OnClick="btnUpdatePFLoan_Click"  /></td>
                </tr>
                <tr>
                    <td style="background-color:Gray;height:10px;">
                    </td>
                    <td style="background-color:Gray;height:10px;">
                    </td>
                    <td style="background-color:Gray;height:10px;">
                    </td>
                </tr>
                <tr>
                    <td class="textlevel">
                        <asp:Button ID="btnDelete" runat="server" Text="Delete PF Ledger" ForeColor="red"  OnClientClick="javascript:return DeleteConfirmation();" OnClick="btnDelete_Click" /></td>
                    <td style="width: 108px; text-align: right">
                    </td>
                    <td style="text-align: right">
                        <asp:Button ID="btnDeleteLoan" runat="server" Text="Delete PF Loan Ledger" ForeColor="red"  OnClientClick="javascript:return DeleteConfirmation();" OnClick="btnDeleteLoan_Click"/></td>
                </tr>
            </table>
        </fieldset>
    </div>
</div>
</asp:Content>

