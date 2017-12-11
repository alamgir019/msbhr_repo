<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="PlanAccLine.aspx.cs" Inherits="Payroll_Banks_PlanAccLine" Title="Plan Account Line" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <script language ="javascript" type="text/javascript" src ="../../JScripts/Confirmation.js"></script>
   <div id="PayrollConfigForm">
    <div id="formhead1"> Plan Account Line Setup </div>
    <div class="MsgBox">
      <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
    </div>
    <div id="PayrollConfigInner">
        <div>
         <fieldset>
             <table>
                <tr>
                    <td class="textlevel">
                        Plan Account Line
                    </td>
                    <td>
                        <asp:TextBox ID="txtAccLine" runat="server" Width="350px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="ReqValHeadTitle" runat="server" ControlToValidate="txtAccLine"
                                        ErrorMessage="*">*</asp:RequiredFieldValidator>
                    </td>
                 </tr>
                 <tr>
                    <td class="textlevel">
                        Description
                    </td>
                    <td>
                        <asp:TextBox ID="txtDesc" runat="server" Width="350px"></asp:TextBox>
                    </td>
                    <td>
                    </td>  
                </tr>
                 <tr>
                    <td class="textlevel">
                       
                    </td>
                    <td>
                        <asp:CheckBox ID="chkMakeInactive" runat="server" Text="Make Inactive" />
                    </td>
                    <td>
                    </td>  
                </tr>
                
             </table>
             <asp:HiddenField ID="hfID" runat="server" />
             <asp:HiddenField ID="hfIsUpdate" runat="server" />
         </fieldset>
         </div>
          <div id="DivCommand1" style="padding-top:3px;">
            <div style="text-align:left;float:left">
              <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px"  CausesValidation="False" OnClick="btnRefresh_Click"  />
            </div>
            <div style="text-align:right;">
              <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px"  UseSubmitBehavior="False" OnClick="btnSave_Click"   />
              <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();" OnClick="btnDelete_Click"    />
             </div>
        </div>
        <%--OnRowCommand="grSalaryHead_RowCommand"--%>
         <fieldset>
            <legend>Plan Account Line List</legend>
            <asp:GridView id="grAccLine" runat="server"  DataKeyNames="AccLineID"  AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" Width="98%" OnRowCommand="grAccLine_RowCommand" >
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True"></SelectedRowStyle>
                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                <Columns>
                <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                  <ItemStyle Width="5%" CssClass="ItemStylecss" />
                </asp:ButtonField>
                <asp:BoundField DataField="ACCLINE" HeaderText="Acc. Line">
                  <ItemStyle CssClass="ItemStylecss" Width="50%"></ItemStyle>
                </asp:BoundField>
                 <asp:BoundField DataField="DESCRIP" HeaderText="Description">
                  <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="IsActive" HeaderText="Is Active">
                  <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                </asp:BoundField>
                </Columns>
              </asp:GridView>
         </fieldset>   
    </div>
   </div>
</asp:Content>

