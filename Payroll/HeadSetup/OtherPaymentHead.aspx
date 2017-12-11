<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="OtherPaymentHead.aspx.cs" Inherits="Payroll_HeadSetup_OtherPaymentHead" Title="Other Payment Head" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language ="javascript" type="text/javascript" src ="../../JScripts/Confirmation.js"></script>
<div id="PayrollConfigForm">
    <div id="formhead1"> Other Payment Head Setup </div>
    <div class="MsgBox">
      <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
    </div>
    <div id="PayrollConfigInner">
        <fieldset>
            <legend>Other Payment Head Setup</legend>
            <table>
                <tr>
                    <td class="textlevel">
                        Head Title
                    </td>
                    <td>
                        <asp:TextBox ID="txtHeadTitle" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="ReqValHeadTitle" runat="server" ControlToValidate="txtHeadTitle"
                                        ErrorMessage="*">*</asp:RequiredFieldValidator>
                    </td>
                     <td>
                    </td>
                </tr>
                <tr>
                    <td class="textlevel">
                        Head Type
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlHeadType" runat="server" Width="156px">
                            <asp:ListItem Value="1">Additive</asp:ListItem>
                            <asp:ListItem Value="-1">Deductive</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkInActive" runat="server" Text="Make Inactive" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td class="textlevel" style="vertical-align:top;" > 
                        Default Amount
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtDefAmt" runat="server" CssClass="TextBoxAmt60" ></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hfID" runat="server" />
            <asp:HiddenField ID="hfIsUpdate" runat="server" />
            <cc1:filteredtextboxextender id="FilTextBoxDefAmt" runat="server" targetcontrolid="txtDefAmt" FilterType ="Custom,Numbers" ValidChars="."></cc1:filteredtextboxextender>
            
        </fieldset>
        <fieldset>
            <legend>Other Payment Head List</legend>
            <div style="width:98%;overflow:scroll;height:250px;">
            <asp:GridView id="grOtherHead" runat="server"  DataKeyNames="SHEADID,HEADNATURE"  AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" Width="98%" OnRowCommand="grOtherHead_RowCommand" OnSelectedIndexChanged="grOtherHead_SelectedIndexChanged">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True"></SelectedRowStyle>
                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                <Columns>
                <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                  <ItemStyle Width="5%" CssClass="ItemStylecss" />
                </asp:ButtonField>
                <asp:BoundField DataField="HEADNAME" HeaderText="Head Title">
                  <ItemStyle CssClass="ItemStylecss" Width="35%"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="HEADNATURE" HeaderText="Head Type">
                  <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                </asp:BoundField>
                 <asp:BoundField DataField="DEFALTAMNT" HeaderText="Default Amount">
                  <ItemStyle CssClass="ItemStylecssRight" Width="20%"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="IsActive" HeaderText="Is Active">
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

