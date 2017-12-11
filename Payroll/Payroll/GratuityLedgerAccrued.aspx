<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="GratuityLedgerAccrued.aspx.cs" Inherits="Payroll_Payroll_GratuityLedgerAccrued" Title="Gratuity Ledger Accrued" %>
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
  <div class="formStyle">
    <div id="formhead4">
      <div style="width:92%;float:left;">Gratuity Ledger Accrued</div>
      <div style=" margin:2px;float:right; padding-right:3px; "><a href="../../Help/Appraisal/APAComment.htm" target="_blank"><img src="../../Images/help.png" /></a></div>
      <div style=" margin:2px; float:right; padding-right:3px;"><a href="../../home.aspx"><img src="../../Images/close_icon.gif" /></a></div>
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
              <asp:Button ID="btnExport" runat="server" Font-Bold="True" OnClick="btnExport_Click"
                  Text="Export to Excel" Width="200px" />
            <asp:Button ID="btnPrint" runat="server" Text="Print" onclientclick="printDiv('PrintMe')" Width="200px"/>
          </div>
          <div id="PrintMe" style="width:100%;margin-top:5px;"> <span style="font-size:16px;font-weight:bold;">Individual Gratuity Ledger</span> <br />
            <span style="font-size:13px;font-weight:bold;">Period: </span>
            <asp:Label ID="lblPeriod" runat="server" Text=""></asp:Label>
            <br />
            <span style="font-size:12px;font-weight:bold;">Date of Accrued: </span>
            <asp:Label ID="lblPrintDate" runat="server" Text=""></asp:Label>
            <br />
            <br />
            <asp:GridView id="grLedger" runat="server"  EmptyDataText="No Record Found" Font-Size="10px" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="EMPID,DESGID,PMONTH,PYEAR,CMONTH,CYEAR,ISEXIST,LEDGERID">
              <HeaderStyle Font-Bold="True"></HeaderStyle>
              <Columns>
              <asp:BoundField DataField="EMPID" HeaderText="Code">
                <ItemStyle Width="5%"></ItemStyle>
              </asp:BoundField>
              <asp:BoundField DataField="FULLNAME" HeaderText="Name">
                <ItemStyle Width="15%"></ItemStyle>
              </asp:BoundField>
              <asp:BoundField DataField="JOBTITLE" HeaderText="Designtaion">
                <ItemStyle Width="10%"></ItemStyle>
              </asp:BoundField>
              <asp:BoundField DataField="JoiningDate" HeaderText="Joining Date">
                <ItemStyle Width="8%"></ItemStyle>
              </asp:BoundField>
              <asp:BoundField DataField="GratuityFrom" HeaderText="Gratuity From">
                <ItemStyle Width="8%"></ItemStyle>
              </asp:BoundField>
              <asp:BoundField DataField="BASIC" HeaderText="Basic Salary" HeaderStyle-HorizontalAlign="right">
                <ItemStyle Width="10%" HorizontalAlign="right"></ItemStyle>
              </asp:BoundField>
              <asp:BoundField DataField="PMONTHAMT" HeaderText="" HeaderStyle-HorizontalAlign="right">
                <ItemStyle Width="12%" HorizontalAlign="right"></ItemStyle>
              </asp:BoundField>
              <asp:BoundField DataField="CMONTHAMT" HeaderText="" HeaderStyle-HorizontalAlign="right">
                <ItemStyle Width="12%" HorizontalAlign="right"></ItemStyle>
              </asp:BoundField>
              <asp:BoundField DataField="CHARGINGAMT" HeaderText="Additional Gratuity" HeaderStyle-HorizontalAlign="right">
                <ItemStyle Width="10%" HorizontalAlign="right"></ItemStyle>
              </asp:BoundField>
              <asp:BoundField DataField="VMONTH" HeaderText="Month" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
              </asp:BoundField>
              <asp:BoundField DataField="VYEAR" HeaderText="Year" HeaderStyle-HorizontalAlign="Center">
                <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
              </asp:BoundField>
              </Columns>
            </asp:GridView>
          </div>
        </fieldset>
      </div>
      <div id="DivCommand1" style="padding-top:3px;">
        <div style="text-align:left;float:left"> </div>
        <div style="text-align:right;">
          <asp:Button ID="btnSave" runat="server" Text="Accrued" Width="70px"  UseSubmitBehavior="False" OnClick="btnSave_Click"  />
        </div>
      </div>
    </div>
  </div>
</asp:Content>
