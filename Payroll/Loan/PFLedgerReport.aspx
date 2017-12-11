<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="PFLedgerReport.aspx.cs" Inherits="Payroll_Loan_PFLedgerReport" Title="PF Ledger Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js"></script>
  <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js"></script>
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
  <div class="formStyle" style="width:120%;">
    <div id="formhead4">
      <div style="width:92%;float:left;">PF Ledger Report</div>
      <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../File/Home.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
    </div>
    <div class="MsgBox">
      <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Fuchsia"></asp:Label>
    </div>
    <div class="Div950" >
      <div style="background-color:#EFF3FB;">
        <fieldset >
          <table>
            <tr>
              <td></td>
              <td></td>
              <td></td>
              <td></td>
              <td></td>
              <td></td>
              <td></td>
              <td style="text-align:right;"><asp:RequiredFieldValidator ID="rfEmpID" runat="server" ControlToValidate="txtEmpID"
                            ErrorMessage="*"></asp:RequiredFieldValidator></td>
              <td></td>
            </tr>
            <tr>
              <td class="textlevelshort">Payroll Month</td>
              <td><asp:DropDownList ID="ddlMonth" runat="server" Width="80px"></asp:DropDownList></td>
              <td class="textlevelshort">Payroll Year </td>
              <td><asp:DropDownList ID="ddlYear" runat="server" Width="80px"></asp:DropDownList></td>
              <td class="textlevelshort">Fiscal Year </td>
              <td><asp:DropDownList ID="ddlFiscalYear" runat="server" Width="100px"></asp:DropDownList></td>
              <td class="textlevelshort">Employee</td>
              <td><asp:TextBox ID="txtEmpID" runat="server" Width="80px"></asp:TextBox></td>
              <td><asp:ImageButton ID="imgBtnSearch" runat="server" Height="19px" ImageUrl="~/Images/Search_Icon.jpg"
                                    Width="21px" CausesValidation="False" OnClick="imgBtnSearch_Click" /></td>
            </tr>
            <tr>
              <td class="textlevelshort"></td>
              <td colspan="3"></td>
              <td></td>
              <td></td>
              <td></td>
              <td></td>
              <td></td>
            </tr>
          </table>
        </fieldset>
      </div>
      <div style="margin-top:10px;">
        <fieldset>
          <div style="background-color:Gray;text-align:center;">
            <asp:Button ID="btnPrint" runat="server" Text="Print" onclientclick="printDiv('PrintMe')" Width="200px" Height="30px" OnClick="btnPrint_Click"/>
            <asp:Button ID="btnExport" runat="server" Font-Bold="true" Height="30px"
                  Text="Export Complete Ledger to Excel" CausesValidation="false" OnClick="btnExport_Click" />
              <asp:Button ID="btnReport" runat="server" Text="Show Crystal Report" 
                  Width="200px" Height="30px" OnClick="btnReport_Click" CausesValidation="False" 
                  Visible="False"/>
              <asp:GridView ID="grExport" runat="server" AutoGenerateColumns="true" ShowHeader="true"
                  Visible="false">
              </asp:GridView>
          </div>
          <div id="PrintMe" style="width:100%;margin-top:5px;"> <span style="font-size:16px;font-weight:bold;">Monthly Provident Fund Ledger</span> <br />
            <span style="font-size:13px;font-weight:bold;">Period: </span>
            <asp:Label ID="lblPeriod" runat="server" Text=""></asp:Label>
            <br />
            <span style="font-size:12px;font-weight:bold;">Date of Print: </span>
            <asp:Label ID="lblPrintDate" runat="server" Text=""></asp:Label>
            <br />
            <br />
            <table style="width:100%;border-collapse:collapse;border:solid 1px Black;">
              <tr>
                <td style="text-align:center;border:solid 1px Black;width:5%;" rowspan="2">Emp#</td>
                <td style="text-align:center;border:solid 1px Black;width:15%;" rowspan="2">Name of Employee</td>
                <td style="text-align:center;border:solid 1px Black;width:20%;" colspan="4">Opening Balance</td>                
                <td style="text-align:center;border:solid 1px Black;width:10%;" colspan="2">Current Payment</td>
                <td style="text-align:center;border:solid 1px Black;width:20%;" colspan="4">Cummulative Balance</td>
                <td style="text-align:center;border:solid 1px Black;width:5%;" rowspan="2">Total Payment</td>
                <td style="text-align:center;border:solid 1px Black;width:5%;" rowspan="2">Net Balance</td>
              </tr>
              <tr>
                <td style="text-align:center;border:solid 1px Black; height: 17px;" >OWN</td>
                <td style="text-align:center;border:solid 1px Black; height: 17px;" >MSB</td>
                <td style="text-align:center;border:solid 1px Black; height: 17px;" >Interest</td>
                <td style="text-align:center;border:solid 1px Black; height: 17px;" >Total</td>
                <td style="text-align:center;border:solid 1px Black; height: 17px;" >OWN</td>
                <td style="text-align:center;border:solid 1px Black; height: 17px;" >MSB</td>
                <td style="text-align:center;border:solid 1px Black; height: 17px;" >OWN</td>
                <td style="text-align:center;border:solid 1px Black; height: 17px;" >MSB</td>
                <td style="text-align:center;border:solid 1px Black; height: 17px;" >Interest</td>
                <td style="text-align:center;border:solid 1px Black; height: 17px;" >Total</td>
              </tr>
              <asp:Repeater ID="rptPFLedger" runat="server">
                <ItemTemplate>
                  <tr >
                    <td style="border-right:solid 1px Black;text-align:left;"><%#Convert.ToString(Eval("EMPID"))%></td>
                    <td style="border-right:solid 1px Black;text-align:left;"><%#Convert.ToString(Eval("FULLNAME"))%></td>
                    <td style="text-align:right;"><%#Convert.ToString(Eval("OPPFOWN"))%></td>
                    <td style="text-align:right;"><%#Convert.ToString(Eval("OPPFCARE"))%></td>
                    <td style="text-align:right;"><%#Convert.ToString(Eval("OPPFINTREST"))%></td>
                    <td style="border-right:solid 1px Black;text-align:right;"><%#Convert.ToString(Eval("OPTOTAL"))%></td>                   
                    <td style="text-align:left;"><%# string.IsNullOrEmpty(Convert.ToString(Eval("CPDATE")))==false?Common.DisplayDate(Convert.ToString(Eval("CPDATE"))):""%></td>
                    <td style="border-right:solid 1px Black;text-align:right;"><%#Convert.ToString(Eval("CPAMOUNT"))%></td>                   
                    <td style="border-right:solid 1px Black;text-align:right;"><%#Convert.ToString(Eval("TOTALPAY"))%></td>
                    <td style="text-align:right;"><%#Convert.ToString(Eval("NETBALANCE"))%></td>
                  </tr>
                </ItemTemplate>
              </asp:Repeater>
              <asp:Repeater ID="rptPFLedgerSummary" runat="server">
                <ItemTemplate>
                  <tr style="border:solid 1px black;">
                    <td style="border-right:solid 1px Black;text-align:left;border:solid 1px black;"></td>
                    <td style="border-right:solid 1px Black;text-align:left;border:solid 1px black;">Summary Total:</td>
                    <td style="text-align:right;border:solid 1px black;"><%#Convert.ToString(Eval("OPPFOWN"))%></td>
                    <td style="text-align:right;border:solid 1px black;"><%#Convert.ToString(Eval("OPPFCARE"))%></td>
                    <td style="text-align:right;border:solid 1px black;"><%#Convert.ToString(Eval("OPPFINTREST"))%></td>
                    <td style="border-right:solid 1px Black;text-align:right;border:solid 1px black;"><%#Convert.ToString(Eval("OPTOTAL"))%></td>                    
                    <td style="text-align:left;border:solid 1px black;"></td>
                    <td style="border-right:solid 1px Black;text-align:right;border:solid 1px black;"><%#Convert.ToString(Eval("CPAMOUNT"))%></td>                    
                    <td style="border-right:solid 1px Black;text-align:right;border:solid 1px black;"><%#Convert.ToString(Eval("TOTALPAY"))%></td>
                    <td style="text-align:right;border:solid 1px black;"><%#Convert.ToString(Eval("NETBALANCE"))%></td>
                  </tr>
                </ItemTemplate>
              </asp:Repeater>
            </table>
          </div>
        </fieldset>
      </div>
    </div>
  </div>
</asp:Content>
