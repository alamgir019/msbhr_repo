<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="BonusPackage.aspx.cs" Inherits="Payroll_SalaryPackage_BonusPackage" Title="Bonus Package" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script language ="javascript" type="text/javascript" src ="../../JScripts/Confirmation.js"></script>
<div id="PayrollConfigForm2">
    <div id="formhead1"> Bonus Package Setup </div>
    <div class="MsgBox">
      <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
    </div>
    <div id="PayrollConfigInner2">
         <fieldset>
             <table>
                        <tr>
                            <td class="textlevel">
                                Package Title
                            </td>
                            <td>
                                <asp:TextBox ID="txtHeadTitle" runat="server" Width="200px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="ReqValHeadTitle" runat="server" ControlToValidate="txtHeadTitle"
                                        ErrorMessage="*">*</asp:RequiredFieldValidator>
                            </td>
                            <td class="textlevel">
                              
                            </td>
                             <td>
                               
                            </td>
                             
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Description
                            </td>
                            <td colspan="4">
                               <asp:TextBox ID="txtDescription" runat="server" Width="503px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Bonus Amount
                            </td>
                            <td>
                               <asp:TextBox ID="txtBonusAmount" runat="server" Width="80px" CssClass="TextBoxAmt60"></asp:TextBox>
                            </td>
                            <td>
                                
                            </td>
                            <td class="textlevel">
                               <asp:CheckBox ID="chkPercent" runat="server" Text="In (%) of " AutoPostBack="True" OnCheckedChanged="chkPercent_CheckedChanged" /></td>
                             <td>
                                 <asp:DropDownList ID="ddlPercentSalHead" runat="server" Width="156px">
                                </asp:DropDownList>
                            </td>
                        </tr> 
                        <tr>
                            <td class="textlevel">
                                No of Payment
                            </td>
                           <td colspan="2">
                                <asp:TextBox ID="txtNoOfPayment" runat="server" Width="80px" CssClass="TextBoxAmt60"></asp:TextBox>
                               per annum</td>
                            
                            <td class="textlevel">
                                Currency
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCurrency" runat="server" Width="156px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="4">
                                <asp:CheckBox ID="chkInActive" runat="server" Text="Make Inactive " />
                            </td>
                        </tr>    
                    </table>
             <asp:HiddenField ID="hfID" runat="server" />
             <asp:HiddenField ID="hfIsUpdate" runat="server" />
             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtBonusAmount" FilterType ="Custom,Numbers" ValidChars=".">
             </cc1:FilteredTextBoxExtender>
             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtNoOfPayment" FilterType ="Custom,Numbers" ValidChars=".">
             </cc1:FilteredTextBoxExtender>
             <br />
             &nbsp;</fieldset>
         <fieldset>
            <legend>Bonus Package List</legend>
            <div style="width:98%;overflow:scroll;height:120px;">
                        <asp:GridView id="grBonus" runat="server"  DataKeyNames="BPID,SHEADID,CURRID"  AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" Width="95%" OnRowCommand="grBonus_RowCommand">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True"></SelectedRowStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                            <Columns>
                            <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                              <ItemStyle Width="5%" CssClass="ItemStylecss" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="BPTitle" HeaderText="Package Title">
                              <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="BPDesc" HeaderText="Description">
                              <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="BAmt" HeaderText="Bonus Amount">
                              <ItemStyle CssClass="ItemStylecssRight" Width="10%"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="isInPercent" HeaderText="Is(%)">
                              <ItemStyle CssClass="ItemStylecssCenter" Width="5%"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="HEADNAME" HeaderText="Percent of">
                              <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="NumOfPay" HeaderText="No. of pay per annum">
                              <ItemStyle CssClass="ItemStylecssRight" Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CurncName" HeaderText="Currency">
                              <ItemStyle CssClass="ItemStylecssCenter" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="IsActive" HeaderText="Is Active">
                              <ItemStyle CssClass="ItemStylecssCenter" Width="5%"></ItemStyle>
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

