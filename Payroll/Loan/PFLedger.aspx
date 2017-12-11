<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="PFLedger.aspx.cs" Inherits="Payroll_Loan_PFLedger" Title="PF Ledger" %>
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

  </script>
  <div class="formStyle">
    <div id="formhead4">
      <div style="width:92%;float:left;">PF Ledger</div>
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
              <td class="textlevelshort">Employee Name</td>
              <td colspan="3"><asp:Label ID="lblEmpName" runat="server" Text=""></asp:Label></td>
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
          <table style="width:100%;border-collapse:collapse;border:solid 1px Black;">
            <tr>
              <td style="text-align:center;border:solid 1px Black;" colspan="4">Opening Balance</td>
              <td style="text-align:center;border:solid 1px Black;" colspan="4">Current Months Credit</td>
              <td style="text-align:center;border:solid 1px Black;" colspan="2">Current Payment</td>
              <td style="text-align:center;border:solid 1px Black;" colspan="4">Cummulative Balance</td>
              <td style="text-align:center;border:solid 1px Black;" rowspan="2">Total Payment</td>
              <td style="text-align:center;border:solid 1px Black;" rowspan="2">Net Balance</td>
            </tr>
            <tr>
              <td style="text-align:center;border:solid 1px Black;" >OWN</td>
              <td style="text-align:center;border:solid 1px Black;" >CARE</td>
              <td style="text-align:center;border:solid 1px Black;" >Interest</td>
              <td style="text-align:center;border:solid 1px Black;" >Total</td>
              <td style="text-align:center;border:solid 1px Black;" >OWN</td>
              <td style="text-align:center;border:solid 1px Black;" >CARE</td>
              <td style="text-align:center;border:solid 1px Black;" >Interest</td>
              <td style="text-align:center;border:solid 1px Black;" >Total</td>
              <td style="text-align:center;border:solid 1px Black;" >Date</td>
              <td style="text-align:center;border:solid 1px Black;" >Amount</td>
              <td style="text-align:center;border:solid 1px Black;" >OWN</td>
              <td style="text-align:center;border:solid 1px Black;" >CARE</td>
              <td style="text-align:center;border:solid 1px Black;" >Interest</td>
              <td style="text-align:center;border:solid 1px Black;" >Total</td>
            </tr>
            <tr>
              <td><asp:TextBox ID="txtOPOWN" runat="server" CssClass="TextBoxAmt60"></asp:TextBox></td>
              <td><asp:TextBox ID="txtOPCARE" runat="server" CssClass="TextBoxAmt60"></asp:TextBox></td>
              <td><asp:TextBox ID="txtOPInterest" runat="server" CssClass="TextBoxAmt60"></asp:TextBox></td>
              <td><asp:TextBox ID="txtOPTotal" runat="server" CssClass="TextBoxAmt60"></asp:TextBox></td>
              <td><asp:TextBox ID="txtCMOWN" runat="server" CssClass="TextBoxAmt60"></asp:TextBox></td>
              <td><asp:TextBox ID="txtCMCARE" runat="server" CssClass="TextBoxAmt60"></asp:TextBox></td>
              <td><asp:TextBox ID="txtCMInterest" runat="server" CssClass="TextBoxAmt60"></asp:TextBox></td>
              <td><asp:TextBox ID="txtCMTotal" runat="server" CssClass="TextBoxAmt60"></asp:TextBox></td>
              <td><asp:TextBox ID="txtCPDate" runat="server" CssClass="TextBoxAmt60"></asp:TextBox></td>
              <td><asp:TextBox ID="txtCPAmount" runat="server" CssClass="TextBoxAmt60"></asp:TextBox></td>
              <td><asp:TextBox ID="txtCUOWN" runat="server" CssClass="TextBoxAmt60"></asp:TextBox></td>
              <td><asp:TextBox ID="txtCUCARE" runat="server" CssClass="TextBoxAmt60"></asp:TextBox></td>
              <td><asp:TextBox ID="txtCUInterest" runat="server" CssClass="TextBoxAmt60"></asp:TextBox></td>
              <td><asp:TextBox ID="txtCUTotal" runat="server" CssClass="TextBoxAmt60"></asp:TextBox></td>
              <td><asp:TextBox ID="txtTotalPay" runat="server" CssClass="TextBoxAmt60"></asp:TextBox></td>
              <td><asp:TextBox ID="txtNetBalance" runat="server" CssClass="TextBoxAmt60"></asp:TextBox></td>
            </tr>
            <tr>
              <td style="text-align:center;" ><cc1:filteredtextboxextender id="fltbOPOWN" runat="server" targetcontrolid="txtOPOWN" FilterType ="Custom,Numbers" ValidChars=""></cc1:filteredtextboxextender></td>
              <td style="text-align:center;" ><cc1:filteredtextboxextender id="fltbOPCARE" runat="server" targetcontrolid="txtOPCARE" FilterType ="Custom,Numbers" ValidChars=""></cc1:filteredtextboxextender></td>
              <td style="text-align:center;" ><cc1:filteredtextboxextender id="fltbOPInt" runat="server" targetcontrolid="txtOPInterest" FilterType ="Custom,Numbers" ValidChars=""></cc1:filteredtextboxextender></td>
              <td style="text-align:center;" ><cc1:filteredtextboxextender id="fltbOPTotal" runat="server" targetcontrolid="txtOPTotal" FilterType ="Custom,Numbers" ValidChars=""></cc1:filteredtextboxextender></td>
              <td style="text-align:center;" ><cc1:filteredtextboxextender id="fltbCMOWN" runat="server" targetcontrolid="txtCMOWN" FilterType ="Custom,Numbers" ValidChars=""></cc1:filteredtextboxextender></td>
              <td style="text-align:center;" ><cc1:filteredtextboxextender id="fltbCMCARE" runat="server" targetcontrolid="txtCMCARE" FilterType ="Custom,Numbers" ValidChars=""></cc1:filteredtextboxextender></td>
              <td style="text-align:center;" ><cc1:filteredtextboxextender id="fltbCMInt" runat="server" targetcontrolid="txtCMInterest" FilterType ="Custom,Numbers" ValidChars=""></cc1:filteredtextboxextender></td>
              <td style="text-align:center;" ><cc1:filteredtextboxextender id="fltbCMTotal" runat="server" targetcontrolid="txtCMTotal" FilterType ="Custom,Numbers" ValidChars=""></cc1:filteredtextboxextender></td>
              <td style="text-align:center;" ><cc1:filteredtextboxextender id="fltbCPDate" runat="server" targetcontrolid="txtCPDate" FilterType ="Custom,Numbers" ValidChars=""></cc1:filteredtextboxextender></td>
              <td style="text-align:center;" ><cc1:filteredtextboxextender id="fltbCPAmt" runat="server" targetcontrolid="txtCPAmount" FilterType ="Custom,Numbers" ValidChars=""></cc1:filteredtextboxextender></td>
              <td style="text-align:center;" ><cc1:filteredtextboxextender id="fltbCUOWN" runat="server" targetcontrolid="txtCUOWN" FilterType ="Custom,Numbers" ValidChars=""></cc1:filteredtextboxextender></td>
              <td style="text-align:center;" ><cc1:filteredtextboxextender id="fltbCUCARE" runat="server" targetcontrolid="txtCUCARE" FilterType ="Custom,Numbers" ValidChars=""></cc1:filteredtextboxextender></td>
              <td style="text-align:center;" ><cc1:filteredtextboxextender id="fltbCUInt" runat="server" targetcontrolid="txtCUInterest" FilterType ="Custom,Numbers" ValidChars=""></cc1:filteredtextboxextender></td>
              <td style="text-align:center;" ><cc1:filteredtextboxextender id="fltbCUTotal" runat="server" targetcontrolid="txtCUTotal" FilterType ="Custom,Numbers" ValidChars=""></cc1:filteredtextboxextender></td>
              <td style="text-align:center;" ><cc1:filteredtextboxextender id="fltbTotalPay" runat="server" targetcontrolid="txtTotalPay" FilterType ="Custom,Numbers" ValidChars=""></cc1:filteredtextboxextender></td>
              <td style="text-align:center;" ><cc1:filteredtextboxextender id="fltbNetBal" runat="server" targetcontrolid="txtNetBalance" FilterType ="Custom,Numbers" ValidChars=""></cc1:filteredtextboxextender></td>
            </tr>
          </table>
          <asp:HiddenField ID="hfIsUpdate" runat="server" />
          <asp:HiddenField ID="hfLedgerID" runat="server" />
        </fieldset>
      </div>
      <div id="DivCommand1" style="padding-top:3px;">
        <div style="text-align:left;float:left">
          <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px"  CausesValidation="False"  />
        </div>
        <div style="text-align:right;">
          <asp:Button ID="btnSave" runat="server" Text="Prepare" Width="70px"  UseSubmitBehavior="False" OnClick="btnSave_Click"  />
          <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();" />
        </div>
      </div>
    </div>
  </div>
</asp:Content>
