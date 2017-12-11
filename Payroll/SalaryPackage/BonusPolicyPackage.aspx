<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="BonusPolicyPackage.aspx.cs" Inherits="Payroll_SalaryPackage_BonusPolicyPackage" Title="Bonus Policy" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script language ="javascript" type="text/javascript" src ="../../JScripts/Confirmation.js"></script>
<div id="PayrollConfigForm2">
    <div id="formhead1"> Bonus Policy </div>
    <div class="MsgBox">
      <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
    </div>
    <div id="PayrollConfigInner2">
         <fieldset>
             <table>
                 <tr>
                     <td class="textlevel">
                         Job Duration</td>
                     <td colspan="2">
                                <asp:DropDownList ID="ddlEmpType" runat="server" Width="156px">
                                </asp:DropDownList></td>
                     <td class="textlevel">
                         Percent</td>
                     <td>
                                <asp:TextBox ID="txtPercent" runat="server" Width="80px" CssClass="TextBoxAmt60"></asp:TextBox></td>
                     <td>
                         <asp:CheckBox ID="chkProrata" runat="server" Text="Is Prorata" /></td>
                 </tr>
                    </table>
             <asp:HiddenField ID="hfID" runat="server" />
             <asp:HiddenField ID="hfIsUpdate" runat="server" />
             </fieldset>
         <fieldset>
            <legend>Bonus Policy List</legend>
            <div style="width:98%;overflow:scroll;height:120px;">
                        <asp:GridView id="grBonus" runat="server"  DataKeyNames="BPID,EMPTYPEID,PRCENT,ISPRORATA"  AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" Width="95%" OnRowCommand="grBonus_RowCommand">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True"></SelectedRowStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                            <Columns>
                            <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                              <ItemStyle Width="5%" CssClass="ItemStylecss" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="BPID" HeaderText="SL">
                              <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="TYPENAME" HeaderText="Employee Type">
                              <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="PRCENT" HeaderText="Percent">
                              <ItemStyle CssClass="ItemStylecssRight" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ISPRORATA" HeaderText="Is Prorata">
                              <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
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

