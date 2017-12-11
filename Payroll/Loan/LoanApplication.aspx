<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="LoanApplication.aspx.cs" Inherits="Payroll_Loan_LoanApplication" Title="Loan Information" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js"></script>
 <script language="javascript" type="text/javascript">
     function SetTotalLoan()
     {
         var txtNL= document.getElementById('<%=txtNewLoan.ClientID%>');
         var txtTL= document.getElementById('<%=txtTotalLoan.ClientID%>');
         var hfTL= document.getElementById('<%=hfPrevLoanAmt.ClientID%>');
         var txtIA= document.getElementById('<%=txtInterestAmt.ClientID%>');
         var txtNP= document.getElementById('<%=txtNetPayAmt.ClientID%>');
         
         var vValue=(hfTL.value*1)+(txtNL.value*1);
         txtTL.value=vValue;
        
         txtNP.value=(txtIA.value*1)+ vValue;
     }
     
     function SetInterestAndNetPayable()
     {
      
         var txtTL= document.getElementById('<%=txtTotalLoan.ClientID%>');
         var txtIR= document.getElementById('<%=txtInterestRate.ClientID%>');
         var txtIA= document.getElementById('<%=txtInterestAmt.ClientID%>');
         var txtNP= document.getElementById('<%=txtNetPayAmt.ClientID%>');
         
         var vIA=(txtIR.value*1)*(txtTL.value*1)/100;
         txtIA.value=vIA;
         txtNP.value=vIA+(txtTL.value*1);
     }
     
     function SetInstallment()
     {
         var txtIA= document.getElementById('<%=txtInstAmt.ClientID%>');
         var txtIN= document.getElementById('<%=txtInstNo.ClientID%>');
         var txtNP= document.getElementById('<%=txtNetPayAmt.ClientID%>');
         
         var vValue=(txtNP.value*1)/(txtIN.value*1);
         txtIA.value=vValue;
    }
 </script>
 
     <script language="javascript" type="text/javascript">
    function ToUpper(ctrl)
    {   
    var t = ctrl.value;
    ctrl.value = t.toUpperCase(); 
    }
    </script>
<div id="PayrollConfigForm">  
<div id="formhead1"> Loan Information </div>
    <div class="MsgBox">
      <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
    </div>
  <div id="PayrollConfigInner">
    <div style="background-color:#EFF3FB;">
        <fieldset>
            <table>
                <tr>
                    <td class="textlevel" style="width:85px;">
                        Employee Code:
                    </td>
                   <td>
                       <asp:TextBox ID="txtEmpCode" runat="server" Width="80px" onkeyup="ToUpper(this)"></asp:TextBox>
                   </td>
                   <td>
                    
                   </td>
                   <td>
                       <asp:ImageButton ID="imgbtnSearch" runat="server" ImageUrl="~/Images/Search_Icon.jpg" Width="20px" OnClick="imgbtnSearch_Click" />
                   </td>
                </tr>
             </table>
              <table width="100%">
                <tr>
                   <td class="textlevel" style="width:85px;">
                    Employee Name:
                    </td>
                    <td style="font-size:11px;">
                        <asp:Label ID="lblEmpName" runat="server" Text="" Width="140px"></asp:Label>
                    </td>
                    <td>
                    
                    </td>
                    <td class="textlevel" style="width:85px;">
                        Office:
                    </td>
                    <td style="font-size:11px;">
                        <asp:Label ID="lblOffice" runat="server" Text="" Width="140px"></asp:Label>
                    </td>
                    <td class="textlevel" style="width:85px;">
                        Designation:
                    </td>
                    <td style="font-size:11px;">
                        <asp:Label ID="lblDesignation" runat="server" Text="" Width="140px"></asp:Label>
                    </td>
                    
                </tr>
                 <tr>
                   <td class="textlevel" style="width:85px;">
                        Joining Date:
                    </td>
                    <td style="font-size:11px;">
                        <asp:Label ID="lblJoinDate" runat="server" Text="" ></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td class="textlevel" style="width:85px;">
                        Gross Salary:
                    </td>
                    <td style="font-size:11px;">
                        <asp:Label ID="lblGrossSalary" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="textlevel" style="width:85px;">
                        Loan Due (Total):
                    </td>
                    <td style="font-size:11px;">
                        <asp:Label ID="lblLoanDue" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div style="margin-top:10px;">
        <fieldset>
        <legend>Loan Information</legend>
            <div style="width:100%;">
                <div style="width:49%;float:left;">
               
                
                
                    <table>
                        <tr>
                            <td class="textlevel">
                                Application Date:
                            </td>
                            <td >
                                 <asp:TextBox ID="txtAppDate" runat="server" Width="120px"></asp:TextBox>
                            </td>
                            <td>
                                <A href="javascript:NewCal('<%= txtAppDate.ClientID %>','ddmmyyyy')"><IMG style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px" height=16 alt="Pick a date" src="../../images/cal.gif" width=16 /></A>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                             <td class="textlevel">
                                Loan Type:
                            </td>
                            <td >
                                <asp:DropDownList ID="ddlLoanType" runat="server" Width="125px" AutoPostBack="True" OnSelectedIndexChanged="ddlLoanType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                          <tr>
                             <td class="textlevel">
                                Required Date:
                            </td>
                            <td >
                                 <asp:TextBox ID="txtReqDate" runat="server" Width="120px"></asp:TextBox>
                            </td>
                            <td>
                                <A href="javascript:NewCal('<%= txtReqDate.ClientID %>','ddmmyyyy')"><IMG style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px" height=16 alt="Pick a date" src="../../images/cal.gif" width=16 /></A>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                             
                            <td style="font-size:11px;color:#3366CC;font-family:Tahoma;" colspan="4">
                                <asp:CheckBox ID="chkExistingSchedule" runat="server" Text="Existing Loan Schedule" />
                                <asp:CheckBox ID="chkCurrentMonth" runat="server" Text="Include Current Month" />
                            </td>
                          
                        </tr>
                         <tr>
                             <td class="textlevel">
                                New Loan Amt:
                            </td>
                            <td >
                                 <asp:TextBox ID="txtNewLoan" runat="server" Width="120px" onchange="SetTotalLoan();"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                             <td class="textlevel">
                                Total Loan Amt:
                            </td>
                            <td >
                                 <asp:TextBox ID="txtTotalLoan" runat="server" Width="120px" Enabled="false"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                         <tr>
                             <td class="textlevel">
                                Interest Rate:
                            </td>
                            <td >
                                 <asp:TextBox ID="txtInterestRate" runat="server" Width="120px" onchange="SetInterestAndNetPayable();" ></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                         <tr>
                             <td class="textlevel">
                                Interest Amount:
                            </td>
                            <td >
                                 <asp:TextBox ID="txtInterestAmt" runat="server" Width="120px" ></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                 
                </div>
                <div style="width:49%;float:left;">
                    
                        <table>
                            <tr>
                                 <td class="textlevel">
                                    Per Installment Amt:
                                </td>
                                <td >
                                     <asp:TextBox ID="txtInstAmt" runat="server" Width="120px" ></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                             <tr>
                                <td class="textlevel">
                                    No. of Installment:
                                </td>
                                <td >
                                     <asp:TextBox ID="txtInstNo" runat="server" Width="120px" onchange="SetInstallment();" ></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                             <tr>
                                <td class="textlevel">
                                    Reason:
                                </td>
                                <td >
                                     <asp:TextBox ID="txtReason" runat="server" Width="200px" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                             <tr>
                                <td class="textlevel">
                                    Net Payable Amount:
                                </td>
                                <td >
                                     <asp:TextBox ID="txtNetPayAmt" runat="server" Width="120px"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                  
                </div>
            </div>    
        </fieldset>
        <fieldset style="margin-top:10px;">
            <legend>Refund Information</legend>
            <table style="border-collapse:collapse;border:solid 1px DimGray;width:100%;">
                <tr>
                    <td style="background-color:#3366CC;color:White;font-weight:bold;font-family:Tahoma;font-size:11px;">
                        Refund Date
                    </td>
                    <td style="background-color:#3366CC;color:White;font-weight:bold;font-family:Tahoma;font-size:11px;">
                        Amount
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtRefDate" runat="server" Width="100px"></asp:TextBox>
                        <A href="javascript:NewCal('<%= txtRefDate.ClientID %>','ddmmyyyy')"><IMG style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px" height=16 alt="Pick a date" src="../../images/cal.gif" width=16 /></A>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRefAmount" runat="server" ></asp:TextBox></td>
                    <td>
                        <asp:Button ID="btnAdd" runat="server" Text="Add" Width="100px" OnClick="btnAdd_Click"/>
                        <asp:Button ID="btnGenerate" runat="server" Text="Generate" Width="100px" OnClick="btnGenerate_Click"/>
                       
                    <td>
                        <asp:Button ID="btnReset" runat="server" Text="Reset Refund List" Width="120px" OnClick="btnReset_Click"/></td>
                    </td>
                </tr>
                <tr>
                    <td>
                         
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <div style="overflow:scroll;">
                             <asp:GridView id="grLoanRefund" runat="server" DataKeyNames="LOANAPPID"  AutoGenerateColumns="False" 
                                    EmptyDataText="No Record Found" Font-Size="9px" Width="98%">
                            <HeaderStyle BackColor="#3366CC" ForeColor="White" Font-Bold="True"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True"></SelectedRowStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                            <Columns>
                            <asp:BoundField DataField="PAYDate" HeaderText="Refund Date">
                                <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PAYAMNT" HeaderText="Amount">
                                <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                            </asp:BoundField>    
                            <asp:ButtonField CommandName="DoubleClick" HeaderText="Remove" Text="Remove">
                              <ItemStyle Width="5%" CssClass="ItemStylecss" />
                            </asp:ButtonField>
                            </Columns>
                            </asp:GridView>
                        </div>
                        <asp:TextBox ID="txtTotRefundAmt" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <div style="overflow:scroll;">
                            <asp:GridView id="grAdvList" runat="server" DataKeyNames="LOANTYPEID,SHEADID"  AutoGenerateColumns="False" 
                                    EmptyDataText="No Record Found" Font-Size="9px" Width="98%">
                            <HeaderStyle BackColor="#3366CC" ForeColor="White" Font-Bold="True"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True"></SelectedRowStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                            <Columns>
                            <asp:ButtonField CommandName="DoubleClick" HeaderText="Select" Text="Select">
                                  <ItemStyle Width="10%" CssClass="ItemStylecss" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="LOANAPPID" HeaderText="Loan ID">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="APPDATE" HeaderText="App Date">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                </asp:BoundField>    
                                 <asp:BoundField DataField="REQDATE" HeaderText="Req. Date">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="LOANAMNT" HeaderText="Loan Amount">
                                    <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                                </asp:BoundField>    
                                 <asp:BoundField DataField="NOOFINSTALL" HeaderText="No of Installment">
                                    <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="LoanTypeID" HeaderText="Loan Type">
                                    <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                                </asp:BoundField>    
                                
                            </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                
                
            </table>
            
            <div>
                <asp:HiddenField ID="hfLoanAppID" runat="server" />
                <asp:HiddenField ID="hfPrevLoanAmt" runat="server" />
                
            </div>
        </fieldset>
    </div>
        <div id="DivCommand1" style="padding-top:3px;">
            <div style="text-align:left;float:left">
              <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px"  CausesValidation="False"  />
            </div>
            <div style="text-align:right;">
              <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px"  UseSubmitBehavior="False"   />
              <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"     />
             </div>
        </div>
  </div>
</div>
</asp:Content>

