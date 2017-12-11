<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="PFLoanRepay.aspx.cs" Inherits="Payroll_Loan_PFLoanRepay" Title="PF Loan Adjustment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js"></script>
    
<div id="PayrollConfigForm2">
     <div id="formhead1">
      <div style="width:92%;float:left;">Loan, Repay & Deduction Adjustment in PF</div>        
        <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../File/Home.aspx">
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
                    <asp:BoundField DataField="DIVISIONName" HeaderText="Intervention">
                      <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="BASICSAL" HeaderText="Basic Sal">
                      <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                    </asp:BoundField>                    
                     <asp:BoundField DataField="NETPF" HeaderText="NetPF">
                      <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                    </asp:BoundField>                    
                    <asp:BoundField DataField="PFLoan" HeaderText="PFLoan">
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
      <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%" Height="400px">
      <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Width="100%" Height="400px">
      <HeaderTemplate>Adjustment</HeaderTemplate>
      <ContentTemplate>
      <br />
      <br />
      <fieldset>
            <legend>Adjustment Details</legend>
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
                    <td><A href="javascript:NewCal('<%= txtTransDate.ClientID %>','ddmmyyyy')">
                        <IMG style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px" height=16 alt="Pick a date" src="../../images/cal.gif" width=16 /></A></td>
                    <td>
                        <asp:TextBox ID="txtCode" runat="server" Width="80px"></asp:TextBox></td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>Loan Detail</legend>
            <table>
                <tr>
                    <td class="textlevel">
                        Adjustment Month</td>
                    <td><asp:DropDownList ID="ddlMonth" runat="server" Width="105px"></asp:DropDownList></td>
                    <td class="textlevel">FY</td>
                    <td><asp:DropDownList ID="ddlFiscalYear" runat="server" Width="105px"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="textlevel">
                        Adjustment Type</td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server" Width="105px">
                            <asp:ListItem>Deduction</asp:ListItem>
                            <asp:ListItem>Cash Pay</asp:ListItem>
                        </asp:DropDownList></td>
                    <td class="textlevel">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="textlevel">
                        Adjustment Amount</td>
                    <td><asp:TextBox ID="txtLoanAmount" runat="server" Width="100px"></asp:TextBox></td>
                    <td class="textlevel"></td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <table>
                <tr>
                    <td class="textlevel"> Remark</td>
                    <td><asp:TextBox ID="txtRemark" runat="server" Width="364px"></asp:TextBox></td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
           <span style="font-weight:bold;">Note:</span> 
           <br />
           <span style="border:solid 1px Gray;">Deduction=Monthly Loan Deduction</span>
           <br />
           <span style="border:solid 1px Gray;">Cash Pay=Loan Refund</span>
                    
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
                    <div style="overflow:scroll;height:300px;margin-top:10px;">
                     <asp:GridView id="grLoan" runat="server" DataKeyNames="EMPID,FISCALYRID,ADJMONTH"  AutoGenerateColumns="False" 
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
                     <asp:BoundField DataField="ADJMONTH" HeaderText="Month">
                      <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="FISCALYRTITLE" HeaderText="Year">
                      <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="ADJTYPE" HeaderText="Adj. Type">
                      <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="ADJAMOUNT" HeaderText="Amount">
                      <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="REMARK" HeaderText="Remark">
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

