<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="GratuityPayment.aspx.cs" Inherits="Payroll_Payroll_GratuityPayment" Title="Gratuity Payment" %>
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
  <div class="formStyle" style="width:80%;">
    <div id="formhead1">
      <div style="width:92%;float:left;">Gratuity Payment</div>
      <div style=" margin:2px;float:left; padding-right:3px; "><a href="../../Help/Appraisal/APAComment.htm" target="_blank"><img src="../../Images/help.png" /></a></div>
      <div style=" margin:2px; float:left; padding-right:3px;"><a href="../../home.aspx"><img src="../../Images/close_icon.gif" /></a></div>
    </div>
    <div class="MsgBox">
      <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Fuchsia"></asp:Label>
    </div>
    <div class="Div950" >
      <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%" Height="360px">
      <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Width="100%" Height="350px">
      <HeaderTemplate> Payment </HeaderTemplate>
      <ContentTemplate>  
      <div style="background-color:#EFF3FB;">
        <fieldset >
          <table>
            <tr>
              <td>Gratuity Month</td>
                <td style="background-color:Gray;">
                    Last Payroll Month & Fiscal Year</td>
              <td>Gratuity Year</td>
              <td>Fiscal Year</td>
              <td style="text-align:right;">
                  Employee<asp:RequiredFieldValidator ID="rfEmpID" runat="server" ControlToValidate="txtEmpID"
                            ErrorMessage="*"></asp:RequiredFieldValidator></td>
              <td>Gratuity From</td>
              <td></td>
              <td>Gratuity UpTo</td>
              <td></td>
            </tr>
            <tr>
              <td><asp:DropDownList ID="ddlMonth" runat="server" Width="95px"></asp:DropDownList></td>
                <td>
                    <asp:DropDownList ID="ddlPayrollMonth" runat="server" Width="95px">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlPayrollFinYear" runat="server" Width="120px">
                    </asp:DropDownList></td>
              <td><asp:DropDownList ID="ddlYear" runat="server" Width="85px"></asp:DropDownList></td>
              <td><asp:DropDownList ID="ddlFiscalYear" runat="server" Width="100px"></asp:DropDownList></td>
              <td><asp:TextBox ID="txtEmpID" runat="server" Width="80px"></asp:TextBox></td>
              <td><asp:TextBox ID="txtGrFrom" runat="server" Width="80px"></asp:TextBox></td>
              <td> <A href="javascript:NewCal('<%= txtGrFrom.ClientID %>','ddmmyyyy')">
                  <IMG style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px" height=16 alt="Pick a date" src="../../images/cal.gif" width=16 /></A></td>
              <td><asp:TextBox ID="txtGrUpto" runat="server" Width="80px"></asp:TextBox></td>
              <td><A href="javascript:NewCal('<%= txtGrUpto.ClientID %>','ddmmyyyy')">
                  <IMG style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px" height=16 alt="Pick a date" src="../../images/cal.gif" width=16 /></A></td>
              <td><asp:ImageButton ID="imgBtnSearch" runat="server" Height="19px" ImageUrl="~/Images/Search_Icon.jpg"
                                    Width="21px" CausesValidation="False" OnClick="imgBtnSearch_Click" /></td>
              
            </tr>
            
            
          </table>
        </fieldset>
      </div>
      <div style="margin-top:10px;">
        <table>
            <tr>
              <td class="textlevel" style="height: 18px">Employee Name</td>
              <td colspan="3" style="height: 18px"><asp:Label ID="lblEmpName" runat="server"></asp:Label></td>
              <td class="textlevel" style="height: 18px">
                  Gratuity Upto</td>
              <td colspan="3" style="height: 18px">
                  <asp:Label ID="lblGrauityUpto" runat="server"></asp:Label></td>
            </tr>
            <tr>
              <td class="textlevel">Designation</td>
              <td colspan="3"><asp:Label ID="lblDesig" runat="server"></asp:Label></td>
              <td class="textlevel">Gratuity From</td>
              <td colspan="3"><asp:Label ID="lblGratuityFrom" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="textlevel">
                    Date of Join</td>
                <td colspan="3">
                    <asp:Label ID="lblJoiningDate" runat="server"></asp:Label></td>
                <td class="textlevel">
                    </td>
                <td colspan="3">
                    </td>
            </tr>
            <tr>
                <td class="textlevel">
                </td>
                <td colspan="3">
                </td>
                <td class="textlevel">
                    </td>
                <td colspan="3">
                    </td>
            </tr>
            <tr>
                <td class="textlevel" colspan="8" style="border-top:solid 1px Gray;">
               
                </td>
            </tr>
            <tr>
                <td class="textlevel">
                    Basic Salary</td>
                <td colspan="3">
                    <asp:TextBox ID="txtBasicSal" runat="server" CssClass="TextBoxAmt100" ReadOnly="True"></asp:TextBox></td>
                <td class="textlevel">
                    Current Gratuity</td>
                <td colspan="3">
                    <asp:TextBox ID="txtCurrMonthGratuity" runat="server" CssClass="TextBoxAmt100"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="textlevel">
                    Gratuity Accrued</td>
                <td colspan="3">
                    <asp:TextBox ID="txtPrevMonthGratuity" runat="server" CssClass="TextBoxAmt100"></asp:TextBox></td>
                <td class="textlevel">
                    Gratuity Fraction</td>
                <td colspan="3">
                    <asp:TextBox ID="txtCharging" runat="server" CssClass="TextBoxAmt100"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="textlevel" colspan="8" style="border-top:solid 1px Gray;">
               
                </td>
            </tr>
            <tr>
                <td colspan="5" style="padding-left:10px;font-weight:bold; text-align: right;">
                    Total</td>
                <td colspan="3">
                    <asp:TextBox ID="txtBalance" runat="server" CssClass="TextBoxAmt100"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="textlevel" colspan="8" style="border-top:solid 1px Gray;">
               
                </td>
            </tr>
             <tr>
                <td class="textlevel">
                    Pay Date</td>
                <td colspan="3">
                    <asp:TextBox ID="txtPayDate" runat="server" Width="100px"></asp:TextBox>
                    <A href="javascript:NewCal('<%= txtPayDate.ClientID %>','ddmmyyyy')">
                    <IMG style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px" height=16 alt="Pick a date" src="../../images/cal.gif" width=16 /></A>
                    </td>
                <td colspan="4">
                </td>
            </tr>
             <tr>
                <td class="textlevel">
                    Remarks</td>
                <td colspan="7">
                    <asp:TextBox ID="txtRemarks" runat="server" Width="382px"></asp:TextBox></td>
            </tr>
            
            
        </table>
        
      </div>
     
          <cc1:filteredtextboxextender id="fltbOPCARE" runat="server" targetcontrolid="txtPrevMonthGratuity" FilterType ="Custom, Numbers" Enabled="True"></cc1:filteredtextboxextender><cc1:filteredtextboxextender id="fltbOPInt" runat="server" targetcontrolid="txtCurrMonthGratuity" FilterType ="Custom, Numbers" Enabled="True"></cc1:filteredtextboxextender><cc1:filteredtextboxextender id="fltbOPTotal" runat="server" targetcontrolid="txtCharging" FilterType ="Custom, Numbers" Enabled="True"></cc1:filteredtextboxextender>          
          <asp:HiddenField ID="hfLedgerID" runat="server" />
          <asp:HiddenField ID="hfIsUpdate" runat="server" />
      
      </ContentTemplate>
      </cc1:TabPanel>
          <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2" Height="360px">
           <HeaderTemplate>Payment List</HeaderTemplate>
           <ContentTemplate>
            <div style="background-color:#EFF3FB;">
                <table>
                    <tr>
                        <td class="textlevel">Gratuity Month</td>
                        <td><asp:DropDownList ID="ddlMonth2" runat="server" Width="95px"></asp:DropDownList></td>
                        <td class="textlevel">
                            Fiscal Year</td>
                        <td><asp:DropDownList ID="ddlFiscalYear2" runat="server" Width="95px"></asp:DropDownList></td>
                        <td><asp:ImageButton ID="imgSearch2" runat="server" Height="19px" ImageUrl="~/Images/Search_Icon.jpg"
                                    Width="21px" CausesValidation="False" OnClick="imgSearch2_Click"  /></td>
                        <td style="width: 3px">
                            <asp:Button ID="btnPrint2" runat="server" Text="Print All Gratuity Slip For Selected Month" CausesValidation="False" OnClick="btnPrint2_Click"/>
                         </td>
                    </tr>
                </table>
            </div>
            <asp:Button ID="btnExport1" runat="server" Text="Export to Excel "
                    Width="200px" CausesValidation="False" OnClick="btnExport1_Click"  />
            <hr />
            <div style="width:99%;overflow:scroll;margin-top:10px;height:300px;">
            <asp:GridView id="grPayment" runat="server" DataKeyNames="TRANSID,CURRGRATUITY"  AutoGenerateColumns="False" 
                                    EmptyDataText="No Record Found" Font-Size="9px" Width="120%" OnRowCommand="grPayment_RowCommand">
                    <HeaderStyle BackColor="#3366CC" ForeColor="White" Font-Bold="True"></HeaderStyle>
                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True"></SelectedRowStyle>
                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                    <Columns>
                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Select" Text="Select">
                      <ItemStyle CssClass="ItemStylecss" Width="3%" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="TRANSID" HeaderText="Tr.No">
                      <ItemStyle CssClass="ItemStylecss" Width="3%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="EMPID" HeaderText="Code">
                      <ItemStyle CssClass="ItemStylecss" Width="3%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="FULLNAME" HeaderText="Name">
                      <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="DesigName" HeaderText="Designation">
                      <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="JOININGDATE" HeaderText="Date of Join">
                      <ItemStyle CssClass="ItemStylecss" Width="8%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="GRATUITYFROM" HeaderText="Gratuity From">
                      <ItemStyle CssClass="ItemStylecss" Width="8%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="GRATUITYTO" HeaderText="Gratuity Upto">
                      <ItemStyle CssClass="ItemStylecss" Width="8%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="BASICSALARY" HeaderText="Basic">
                      <ItemStyle CssClass="ItemStylecss" Width="8%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="LASTGRATUITY" HeaderText="Gratuity Accrued">
                      <ItemStyle CssClass="ItemStylecss" Width="8%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="GRATUITYFRACTION" HeaderText="Gratuity Fraction">
                      <ItemStyle CssClass="ItemStylecss" Width="8%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="PAYAMT" HeaderText="Balance">
                      <ItemStyle CssClass="ItemStylecss" Width="8%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="PAYDATE" HeaderText="Date">
                      <ItemStyle CssClass="ItemStylecss" Width="8%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="REMARKS" HeaderText="Remarks">
                      <ItemStyle CssClass="ItemStylecss" Width="8%"></ItemStyle>
                    </asp:BoundField>
                    <asp:ButtonField CommandName="RowDeleting" HeaderText="Gratuity Slip" Text="Print">
                      <ItemStyle CssClass="ItemStylecss" Width="5%" />
                    </asp:ButtonField>
                    </Columns>
                  </asp:GridView>     
          </div>
           </ContentTemplate>
          </cc1:TabPanel>
      </cc1:TabContainer>
      <div id="DivCommand1" style="padding-top:3px;">
        <div style="text-align:left;float:left">
          <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px"  CausesValidation="False" OnClick="btnRefresh_Click"  />
        </div>
        <div style="text-align:right;">
          <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px"  UseSubmitBehavior="False" OnClick="btnSave_Click"  />
          <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();" OnClick="btnDelete_Click" />
        </div>
      </div>
    </div>
  </div>
</asp:Content>
