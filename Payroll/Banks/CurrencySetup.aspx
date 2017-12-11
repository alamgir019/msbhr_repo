<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="CurrencySetup.aspx.cs" Inherits="Payroll_CurrencySetup" Title="Currency Setup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="officeSetup">
    <div id="formhead1">Currency Setup</div>
    <!--Div for group-->
    <div class="MsgBox">
      <!--Div for msg-->
      <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>
    <div class="officeSetupInner">
        <fieldset>
            <table>
                        <tr>
                            <td class="textlevel">
                                Currency Name
                            </td>
                            <td>
                                <asp:TextBox ID="txtCurrName" runat="server" Width="200px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="ReqValHeadTitle" runat="server" ControlToValidate="txtCurrName"
                                        ErrorMessage="*">*</asp:RequiredFieldValidator>
                            </td>
                            <td class="textlevel">
                              
                            </td>
                             <td>
                               
                            </td>
                             
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Currency Symbol
                            </td>
                            <td colspan="2">
                               <asp:TextBox ID="txtCurrSymbol" runat="server" Width="60px"></asp:TextBox>
                            </td>
                            <td class="textlevel">
                                Smallest Unit Name
                            </td>
                            <td>
                                 <asp:TextBox ID="txtSmallUnitName" runat="server" Width="60px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                
                            </td>
                            <td colspan="4">
                               <asp:CheckBox ID="chkDefault" runat="server" Text="Set as operational currency " />
                            </td>
                             
                        </tr> 
                        <tr>
                            <td colspan="5">
                                Conversion amount for each unit
                                <asp:TextBox ID="txtConvAmt" runat="server" Width="80px" CssClass="TextBoxAmt60"></asp:TextBox>
                                Operation currency amount
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
            <cc1:filteredtextboxextender id="FilteredTextBoxExtender1" runat="server" targetcontrolid="txtConvAmt" FilterType ="Custom,Numbers" ValidChars="."></cc1:filteredtextboxextender>
        </fieldset>
         <fieldset>
            <legend>Currency List</legend>
            <div style="width:98%;overflow:scroll;height:120px;">
                        <asp:GridView id="grCurrency" runat="server"  DataKeyNames="CURNCID"  AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" Width="95%" OnRowCommand="grCurrency_RowCommand">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True"></SelectedRowStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                            <Columns>
                            <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                              <ItemStyle Width="5%" CssClass="ItemStylecss" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="CURNCNAME" HeaderText="Currency Name">
                              <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="CURNCSYMBOL" HeaderText="Symbol">
                              <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="LOWESTUNITNAME" HeaderText="Smallest Unit">
                              <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="ISDEFAULT" HeaderText="Is Operational">
                              <ItemStyle CssClass="ItemStylecssCenter" Width="10%"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="CONVRSAMT" HeaderText="Conversion Amount">
                              <ItemStyle CssClass="ItemStylecssRight" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ISACTIVE" HeaderText="IsActive">
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

