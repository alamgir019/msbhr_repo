<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="LoanTypeSetup.aspx.cs" Inherits="Payroll_LoanTypeSetup" Title="Loan Type Setup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language ="javascript" type="text/javascript" src ="../../JScripts/Confirmation.js"></script>
<div id="PayrollConfigForm">    
    <div id="formhead1"> Loan Type Setup </div>
    <div class="MsgBox">
      <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
    </div>
    <div id="PayrollConfigInner">
        <fieldset>
            <legend>Loan Type Setup</legend>
            <table>
                <tr>
                    <td class="textlevel">
                        Loan Type Name</td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                            ErrorMessage="*">*</asp:RequiredFieldValidator></td>
                    <td class="textlevel" style="width: 120px">
                        <asp:CheckBox ID="chkInActive" runat="server" Text="Make Inactive" /></td>
                </tr>
            </table>
            <table>
                <tr>
                    <td class="textlevel" style="vertical-align:top;"> 
                        Description
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="304px" Height="67px"></asp:TextBox>
                    </td>                    
                </tr>
            </table><table>
                <tr>
                    <td class="textlevel">
                        Salary Head&nbsp;</td>
                    <td class="textlevel">
                        <asp:DropDownList ID="ddlSalaryHead" runat="server" AutoPostBack="True" Width="150px">
                        </asp:DropDownList></td>
                    <td class="textlevel">
                        <asp:CheckBox ID="chkIsPFLoan" runat="server" Text="Is PF Loan"/></td>                    
                </tr>
              </table> 
              <table>
                <tr>
                    <td class="textlevelleft" style="width:45%">
                        One will get this loan if anybody complete</td>
                    <td class="textlevel" style="width:10%">
                        <asp:TextBox ID="txtMinServiceLife" runat="server" CssClass="TextBoxAmt60" MaxLength="5"></asp:TextBox></td>
                        <td class="textlevelleft" style="width:55%">
                            months of service life</td>
                </tr>
            </table>
            <asp:HiddenField ID="hfID" runat="server" />
            <asp:HiddenField ID="hfIsUpdate" runat="server" />
            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMinServiceLife" FilterType ="Custom,Numbers">
            </cc1:FilteredTextBoxExtender>
        </fieldset>
        <fieldset>
            <legend>Loan Type List</legend>
            <div style="width:98%;overflow:scroll;height:250px;">
            <asp:GridView id="grLoanType" runat="server" DataKeyNames="LOANTYPEID,SHEADID"  AutoGenerateColumns="False" 
            EmptyDataText="No Record Found" Font-Size="9px" Width="98%" OnRowCommand="grLoanType_RowCommand">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True"></SelectedRowStyle>
                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                <Columns>
                <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                  <ItemStyle Width="5%" CssClass="ItemStylecss" />
                </asp:ButtonField>
                <asp:BoundField DataField="LOANTYPENAME" HeaderText="Loan Type Name">
                  <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                </asp:BoundField>  
                <asp:BoundField DataField="LOANDESCRIPTION" HeaderText="Description">
                  <ItemStyle CssClass="ItemStylecss" Width="25%"></ItemStyle>
                </asp:BoundField>               
                 <asp:BoundField DataField="HEADNAME" HeaderText="Head Name">
                  <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                </asp:BoundField>                
                <asp:BoundField DataField="ISACTIVE" HeaderText="Is Active">
                  <ItemStyle CssClass="ItemStylecssCenter" Width="10%"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="ISPFLOAN" HeaderText="Is PF Loan">
                  <ItemStyle CssClass="ItemStylecssCenter" Width="10%"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="MINSERVICELIFE" HeaderText="Min Service Life">
                  <ItemStyle CssClass="ItemStylecssCenter" Width="10%"></ItemStyle>
                </asp:BoundField>
                </Columns>
              </asp:GridView>
           </div>
        </fieldset>
        
        <div id="DivCommand1" style="padding-top:3px;">
        <div style="text-align:left;float:left">
          <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px"  CausesValidation="False" OnClick="btnRefresh_Click"  />
        </div>
        <div style="text-align:right;">
          <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px"  UseSubmitBehavior="False" OnClick="btnSave_Click"   />
          <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();" OnClick="btnDelete_Click"    />
         </div>
      </div>
    </div>
 </div>
</asp:Content>

