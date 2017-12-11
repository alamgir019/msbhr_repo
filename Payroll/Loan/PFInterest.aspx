<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="PFInterest.aspx.cs" Inherits="Payroll_Loan_PFInterest" Title="PF Interest Calculation" %>
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
  <div class="formStyle" style="width:220%;">
    <div id="formhead1">
      <div style="width:96%;float:left;">PF Interest Calculation</div>      
      <div style=" margin:2px; float:left;padding-left:3px;"><a href="../../File/Home.aspx">
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
              <td class="textlevel">Payroll Month</td>
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
          <table>
            <tr>
                <td class="textlevel">PF Interest Percent</td>
                <td><asp:TextBox ID="txtPFRate" runat="server" Width="80px"></asp:TextBox></td>
                <td style="width: 203px"><asp:Button ID="btnYearlyInterest" runat="server" Text="Yearly Interest"  Width="200px" Height="30px" Font-Bold="True" OnClick="btnYearlyInterest_Click1" CausesValidation=false/></td>
                <td style="width: 203px">
                    <asp:Button ID="btnMidtermInterest" runat="server" Text="Midterm Interest"  Width="200px" Height="30px" Font-Bold="True" OnClick="btnMidtermInterest_Click" CausesValidation=false/></td>
            </tr>
          </table>
        </fieldset>
      </div>
      <div style="margin-top:10px;">
        <fieldset>
          <div style="background-color:Gray;text-align:center;">
            <asp:Button ID="btnPrint" runat="server" Text="Print" onclientclick="printDiv('PrintMe')" Width="200px" Height="30px" CausesValidation=false/>
            <asp:Button ID="btnExport" runat="server" Font-Bold="true" Height="30px"
                  Text="Export Complete Ledger to Excel" CausesValidation="false" OnClick="btnExport_Click" />&nbsp;
          </div>
          <div id="PrintMe" style="width:100%;margin-top:5px;"> <span style="font-size:16px;font-weight:bold;"> Provident Fund Interest Calculation</span><br />
            <span style="font-size:13px;font-weight:bold;">Period: </span>
            <asp:Label ID="lblPeriod" runat="server" Text=""></asp:Label>
            <br />
            <span style="font-size:12px;font-weight:bold;">Date of Print: </span>
            <asp:Label ID="lblPrintDate" runat="server" Text=""></asp:Label>
            <br />
            <br />
              <asp:GridView ID="grExport" runat="server" AutoGenerateColumns="true" ShowHeader="true"
                  Visible="true">
              </asp:GridView>
          </div>
        </fieldset>
      </div>
      
      <div style="text-align:left;font-weight:bold;background-color:#EFF3FB;margin-top:5px;">
      <fieldset>
          <asp:Button ID="btnSave" runat="server" Text="Save Interest" Width="150px" Font-Bold="true" OnClick="btnSave_Click" CausesValidation=false />
      </fieldset>
      </div>
  
    </div>
  </div>
</asp:Content>
