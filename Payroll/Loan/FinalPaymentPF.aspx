<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="FinalPaymentPF.aspx.cs" Inherits="Payroll_Loan_FinalPaymentPF" Title="Final Payment From PF" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js"></script>
<script language="javascript" type="text/javascript">
     function GetPaymentAmount()
     {
         var txtDays= document.getElementById('<%=txtFrDays.ClientID%>');
         var txtBasic= document.getElementById('<%=txtEmpBasic.ClientID%>');
         var txtFrAmt= document.getElementById('<%=txtFrPFAmt.ClientID%>');
         var txtFrInt= document.getElementById('<%=txtFrPFInt.ClientID%>');
         var txtPFLn= document.getElementById('<%=txtPFLoan.ClientID%>');
         var txtPFBal= document.getElementById('<%=txtPFBal.ClientID%>');
         var txtPFArr= document.getElementById('<%=txtPFArrear.ClientID%>');
         var txtAmt= document.getElementById('<%=txtPayAmt.ClientID%>');
         var hfAmt= document.getElementById('<%=hfPayAmt.ClientID%>');
         
         var vValue=((txtBasic.value*1)*12/260)*(txtDays.value*1);
         vValue=(vValue*1)*10/100;
         vValue=Math.round(vValue*1);
         txtFrAmt.value= vValue;
         txtAmt.value=(txtPFBal.value*1)+(txtFrAmt.value*1)+(txtFrAmt.value*1)+(txtFrInt.value*1)+(txtPFArr.value*1)+(txtPFArr.value*1);
         hfAmt.value=txtAmt.value;
     }
</script>    
<div id="PayrollConfigForm">
     <div id="formhead1">
      <div style="width:92%;float:left;">Final Payment From Provident Fund</div>        
        <div style=" margin:2px; float:left;padding-left:3px;"><a href="../../File/Home.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
    </div>
    <div class="MsgBox">
      <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
    </div>
    <div id="PayrollConfigInner">
        <div style="background-color:#EFF3FB;">
        <fieldset>
          <table>
            <tr>
              <td class="textlevel" style="width:85px;"> Employee Code: </td>
              <td><asp:TextBox ID="txtEmpCode" runat="server" Width="80px"></asp:TextBox></td>
              <td></td>
              <td><asp:ImageButton ID="imgbtnSearch" runat="server" ImageUrl="~/Images/Search_Icon.jpg" Width="20px" OnClick="imgbtnSearch_Click" /></td>
            </tr>
          </table>
          <div style="width:99%;overflow:scroll;margin-top:10px;">
             <asp:GridView id="grEmp" runat="server" DataKeyNames="EMPID"  AutoGenerateColumns="False" 
                                    EmptyDataText="No Record Found" Font-Size="9px" Width="120%">
                    <HeaderStyle BackColor="#3366CC" ForeColor="White" Font-Bold="True"></HeaderStyle>
                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True"></SelectedRowStyle>
                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                    <Columns>
                    <asp:BoundField DataField="EMPID" HeaderText="Code">
                      <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="FULLNAME" HeaderText="Name">
                      <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="JOBTITLE" HeaderText="Designation">
                      <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="DIVISIONID" HeaderText="LOC">
                      <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="BASICSAL" HeaderText="Basic Sal">
                      <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="NETCU" HeaderText="NetCU">
                      <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="NETPF" HeaderText="NetPF">
                      <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="CULoan" HeaderText="CULoan">
                      <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="PFLoan" HeaderText="PFLoan">
                      <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="NETPFCU" HeaderText="Net PF CU">
                      <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="MAXCUWITHDRAW" HeaderText="Max CU Withdraw">
                      <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="ALLOWPFLOAN" HeaderText="Allowable Loan">
                      <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                    </asp:BoundField>
                    </Columns>
                  </asp:GridView>
          </div>
        </fieldset>
        
      </div>
      <br />
      <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%" Height="450px">
      <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Width="100%" Height="440px">
      <HeaderTemplate>PF Payment</HeaderTemplate>
      <ContentTemplate>
      <br />
      <br />
      <fieldset>
           
            <table>
                <tr>
                    <td style="background-color:#DF6F1D;text-align:center;">SL No</td>
                    <td style="background-color:#DF6F1D;text-align:center;">
                        Date
                    </td>
                    <td style="background-color:#DF6F1D;text-align:center;">
                    </td>
                    <td style="background-color:#DF6F1D;text-align:center;">
                        Emp Code
                    </td>
                     
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtTransID" runat="server" Width="80px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTransDate" runat="server" Width="80px"></asp:TextBox></td>
                    <td><A href="javascript:NewCal('<%= txtTransDate.ClientID %>','ddmmyyyy')"><IMG style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px" height=16 alt="Pick a date" src="../../images/cal.gif" width=16 /></A></td>
                    <td>
                        <asp:TextBox ID="txtCode" runat="server" Width="80px"></asp:TextBox></td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>Details</legend>
            <table>
                <tr>
                    <td class="textlevel">
                        Payment Month</td>
                    <td><asp:DropDownList ID="ddlMonth" runat="server" Width="105px"></asp:DropDownList></td>
                    <td class="textlevel">FY</td>
                    <td><asp:DropDownList ID="ddlFiscalYear" runat="server" Width="105px"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="textlevel">
                        PF Employee</td>
                    <td>
                        <asp:TextBox ID="txtPFEmp" runat="server" Width="100px"></asp:TextBox></td>
                    <td class="textlevel">
                        PF CARE</td>
                    <td>
                        <asp:TextBox ID="txtPFCare" runat="server" Width="100px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="textlevel">
                        PF Interest Employee&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtPFIntEmp" runat="server" Width="100px"></asp:TextBox></td>
                    <td class="textlevel">
                        PF CARE Interest</td>
                    <td>
                        <asp:TextBox ID="txtPFIntCare" runat="server" Width="100px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="textlevel">
                        PF Outstanding Loan</td>
                    <td>
                        <asp:TextBox ID="txtPFLoan" runat="server" Width="100px"></asp:TextBox></td>
                    <td class="textlevel">
                        PF Balance</td>
                    <td>
                        <asp:TextBox ID="txtPFBal" runat="server" Width="100px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="textlevel" style="height: 16px">
                        Fractional Days</td>
                    <td style="height: 16px">
                        <asp:TextBox ID="txtFrDays" runat="server" Width="100px" onchange="GetPaymentAmount();"></asp:TextBox></td>
                    <td class="textlevel" style="height: 16px">
                        Employee Basic</td>
                    <td style="height: 16px">
                        <asp:TextBox ID="txtEmpBasic" runat="server" Width="100px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="textlevel">
                        Fractional PF Amount</td>
                    <td>
                        <asp:TextBox ID="txtFrPFAmt" runat="server" Width="100px" onchange="GetPaymentAmount();"></asp:TextBox></td>
                    <td class="textlevel">
                        Fractional PF Interest</td>
                    <td>
                        <asp:TextBox ID="txtFrPFInt" runat="server" Width="100px"  onchange="GetPaymentAmount();"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="textlevel">
                        PF Arrear</td>
                    <td>
                        <asp:TextBox ID="txtPFArrear" runat="server" onchange="GetPaymentAmount();" Width="100px"></asp:TextBox></td>
                    <td class="textlevel">
                        Payment Amount</td>
                    <td>
                        <asp:TextBox ID="txtPayAmt" runat="server" Width="100px"></asp:TextBox></td>
                </tr>
            </table>
            <asp:HiddenField ID="hfPayAmt" runat="server" />
        </fieldset>
        <fieldset>
            <legend>Bank Detail</legend>
            <table>
                <tr>
                    <td class="textlevel">
                        Cheque Number</td>
                    <td>
                        <asp:TextBox ID="txtChequeNumber" runat="server" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="textlevel">
                        Cheque Date</td>
                    <td>
                        <asp:TextBox ID="txtChequeDate" runat="server" Width="80px"></asp:TextBox>
                        <a href="javascript:NewCal('<%= txtChequeDate.ClientID %>','ddmmyyyy')">
                            <img alt="Pick a date" height="16" src="../../images/cal.gif" style="border-right: 0px;
                                border-top: 0px; border-left: 0px; border-bottom: 0px" width="16" /></a>
                    </td>
                </tr>
                <tr>
                    <td class="textlevel">
                        Bank Detail</td>
                    <td>
                        <asp:TextBox ID="txtBankDetail" runat="server" Width="300px"></asp:TextBox>
                    </td>
                </tr>
            </table>
           <br />
           <span style="border:solid 1px Gray;"></span>
                    
            <asp:HiddenField ID="hfIsUpdate" runat="server" />
        </fieldset>
        </ContentTemplate>
      </cc1:TabPanel>
          <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
              <HeaderTemplate>
                  Browse
              </HeaderTemplate>
              <ContentTemplate>
                <fieldset>
                    <div style="overflow:scroll;height:400px;margin-top:10px;">
                     <asp:GridView id="grLoan" runat="server" DataKeyNames="EMPID,FISCALYRID,VMONTH,BANKDETAIL,LASTWD,LASTPFDEDUCT,LASTPFINT,LASTPFBALANCE,PFARREAR"  AutoGenerateColumns="False" 
                                    EmptyDataText="No Record Found" Font-Size="9px" Width="99%" OnRowCommand="grLoan_RowCommand">
                    <HeaderStyle BackColor="#3366CC" ForeColor="White" Font-Bold="True"></HeaderStyle>
                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True"></SelectedRowStyle>
                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                    <Columns>
                     <asp:ButtonField CommandName="DoubleClick" HeaderText="Select" Text="Select">
                      <ItemStyle CssClass="ItemStylecss" Width="3%" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="TRANSID" HeaderText="Loan ID">
                      <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="TRANSDATE" HeaderText="Entry Date">
                      <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="VMONTH" HeaderText="Month">
                      <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="FISCALYRTITLE" HeaderText="Year">
                      <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="PAYAMT" HeaderText="Amount">
                      <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="ChequeNo" HeaderText="Cheque No">
                      <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="ChequeDate" HeaderText="Cheque Date">
                      <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                    </Columns>
                  </asp:GridView>
                    </div>
                </fieldset>
              </ContentTemplate>
          </cc1:TabPanel>
      </cc1:TabContainer>
      
         <div id="DivCommand1" style="padding-top:3px;">
        <div style="text-align:left;float:left">
          <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px"  CausesValidation="False" OnClick="btnRefresh_Click"  />
        </div>
        <div style="text-align:right;">
          <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px"  UseSubmitBehavior="False" OnClick="btnSave_Click"   />
          <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"     />
        </div>
      </div>
     </div>
   </div>
</asp:Content>

