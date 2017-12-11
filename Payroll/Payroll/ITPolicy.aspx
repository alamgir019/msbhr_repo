<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="ITPolicy.aspx.cs" Inherits="Payroll_Payroll_ITPolicy" Title="IT Policy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="formStyle" style="height:750px;width:60%">
     <div id="formhead1">
       <div style="width:92%;float:left;">Income Tax Policy</div>
      
      <div style=" margin:2px; float:right; padding-right:3px;"><a href="../home.aspx"><img src="../Images/close_icon.gif" /></a></div></div>
     
    <div class="MsgBox">
      <asp:Label ID="lblMsg" runat="server" Width="98%" Font-Bold="True" ForeColor="Fuchsia"></asp:Label>
    </div>
    <div style="margin-top:10px;margin-left:20px;">
     
     <table>
         <tr>
             <td class="textlevel">
             </td>
             <td style="text-align:center;background-color:#C04400;color:White;font-weight:bold;">
                 Male</td>
             <td style="text-align:center;background-color:#C04400;color:White;font-weight:bold;">
                 Female</td>
             <td style="text-align:center;background-color:#C04400;color:White;font-weight:bold;">
                Autistic
             </td>
         </tr>
         <tr>
             <td class="textlevel">
                 Yearly House Rent Max Exemption</td>
             <td>
                 <asp:TextBox ID="txtYHAM" runat="server" Width="100px" CssClass="TextBoxAmt100"></asp:TextBox></td>
             <td>
                 <asp:TextBox ID="txtYHAF" runat="server" Width="100px" CssClass="TextBoxAmt100"></asp:TextBox></td>
             <td>
                 <asp:TextBox ID="txtYHAA" runat="server" Width="100px" CssClass="TextBoxAmt100"></asp:TextBox></td>
         </tr>
        <tr>
            <td class="textlevel" style="height: 24px">
                Monthly House Rent Exemption</td>
            <td style="height: 24px" >
                <asp:TextBox ID="txtMHAM" runat="server" Width="100px" CssClass="TextBoxAmt100"></asp:TextBox></td>
            <td style="height: 24px">
                <asp:TextBox ID="txtMHAF" runat="server" Width="100px" CssClass="TextBoxAmt100"></asp:TextBox></td>
            <td style="height: 24px">
                <asp:TextBox ID="txtMHAA" runat="server" Width="100px" CssClass="TextBoxAmt100"></asp:TextBox></td>
           
        </tr>
        <tr>
            <td class="textlevel"> Yearly Transport Allowance Exemption</td>
            <td>
                <asp:TextBox ID="txtYTAM" runat="server" Width="100px" CssClass="TextBoxAmt100"></asp:TextBox></td>
            <td>
                <asp:TextBox ID="txtYTAF" runat="server" Width="100px" CssClass="TextBoxAmt100"></asp:TextBox></td>
            <td>
                <asp:TextBox ID="txtYTAA" runat="server" Width="100px" CssClass="TextBoxAmt100"></asp:TextBox></td>
        </tr>
        <tr>
             <td class="textlevel" style="">
             </td>
             <td style="height: 16px; ">
             </td>
             <td style="height: 16px; ">
             </td>
             <td style="height: 16px;">
             </td>
         </tr>
         <tr>
             <td class="textlevel" style="height: 16px;background-color:#186D0B">
                 0 Income Tax Slot</td>
             <td style="height: 16px;background-color:#186D0B">
                 <asp:TextBox ID="txtSlot0M" runat="server" Width="100px" CssClass="TextBoxAmt100"></asp:TextBox></td>
             <td style="height: 16px;background-color:#186D0B">
                 <asp:TextBox ID="txtSlot0F" runat="server" Width="100px" CssClass="TextBoxAmt100"></asp:TextBox></td>
             <td style="height: 16px;background-color:#186D0B">
                 <asp:TextBox ID="txtSlot0A" runat="server" Width="100px" CssClass="TextBoxAmt100"></asp:TextBox></td>
         </tr>
         <tr>
             <td class="textlevel" style="background-color:#00A3F6;">
                 10% Income Tax Slot</td>
              <td style="height: 16px;background-color:#00A3F6">
                 <asp:TextBox ID="txtSlot10M" runat="server" Width="100px" CssClass="TextBoxAmt100"></asp:TextBox></td>
              <td style="height: 16px;background-color:#00A3F6">
                 <asp:TextBox ID="txtSlot10F" runat="server" Width="100px" CssClass="TextBoxAmt100"></asp:TextBox></td>
              <td style="height: 16px;background-color:#00A3F6">
                 <asp:TextBox ID="txtSlot10A" runat="server" Width="100px" CssClass="TextBoxAmt100"></asp:TextBox></td>
         </tr>
         <tr>
             <td class="textlevel" style="background-color:#EACF54;">
                 15% Income Tax Slot</td>
              <td style="height: 16px;background-color:#EACF54">
                 <asp:TextBox ID="txtSlot15M" runat="server" Width="100px" CssClass="TextBoxAmt100"></asp:TextBox></td>
              <td style="height: 16px;background-color:#EACF54">
                 <asp:TextBox ID="txtSlot15F" runat="server" Width="100px" CssClass="TextBoxAmt100"></asp:TextBox></td>
              <td style="height: 16px;background-color:#EACF54">
                 <asp:TextBox ID="txtSlot15A" runat="server" Width="100px" CssClass="TextBoxAmt100"></asp:TextBox></td>
         </tr>
         <tr>
             <td class="textlevel" style="background-color:#FF4B3C;">
                 20% Income Tax Slot</td>
               <td style="height: 16px;background-color:#FF4B3C">
                 <asp:TextBox ID="txtSlot20M" runat="server" Width="100px" CssClass="TextBoxAmt100"></asp:TextBox></td>
               <td style="height: 16px;background-color:#FF4B3C">
                 <asp:TextBox ID="txtSlot20F" runat="server" Width="100px" CssClass="TextBoxAmt100"></asp:TextBox></td>
               <td style="height: 16px;background-color:#FF4B3C">
                 <asp:TextBox ID="txtSlot20A" runat="server" Width="100px" CssClass="TextBoxAmt100"></asp:TextBox></td>
         </tr>
         <tr>
             <td class="textlevel" style="background-color: #c04400">
                 25% Income Tax Slot
             </td>
             <td style="height: 16px; background-color: #c04400">
                 <asp:TextBox ID="txtSlot25M" runat="server" CssClass="TextBoxAmt100" Width="100px"></asp:TextBox></td>
             <td style="height: 16px; background-color: #c04400">
                 <asp:TextBox ID="txtSlot25F" runat="server" CssClass="TextBoxAmt100" Width="100px"></asp:TextBox></td>
             <td style="height: 16px; background-color: #c04400">
                 <asp:TextBox ID="txtSlot25A" runat="server" CssClass="TextBoxAmt100" Width="100px"></asp:TextBox></td>
         </tr>
         <tr>
             <td class="textlevel" style="background-color: #9966ff;">
                 30% Income Tax Slot
             </td>
             <td style="height: 16px; background-color: #9966ff">
                 <asp:TextBox ID="txtSlot30M" runat="server" CssClass="TextBoxAmt100" Width="100px"></asp:TextBox></td>
             <td style="height: 16px; background-color: #9966ff">
                 <asp:TextBox ID="txtSlot30F" runat="server" CssClass="TextBoxAmt100" Width="100px"></asp:TextBox></td>
             <td style="height: 16px; background-color: #9966ff">
                 <asp:TextBox ID="txtSlot30A" runat="server" CssClass="TextBoxAmt100" Width="100px"></asp:TextBox></td>
         </tr>
         <tr>
             <td class="textlevel" style="">
             </td>
             <td style="height: 16px; ">
             </td>
             <td style="height: 16px; ">
             </td>
             <td style="height: 16px;">
             </td>
         </tr>
         <tr>
             <td class="textlevel" style="height: 16px;background-color:#8EC439;">
                 Investment Allowance</td>
             <td style="height: 16px;background-color:#8EC439;">
                 <asp:TextBox ID="txtInvAllowM" runat="server" CssClass="TextBoxAmt60"></asp:TextBox>%</td>
             <td style="height: 16px;background-color:#8EC439;">
                 <asp:TextBox ID="txtInvAllowF" runat="server" CssClass="TextBoxAmt60"></asp:TextBox>%</td>
             <td style="height: 16px;background-color:#8EC439;">
                 <asp:TextBox ID="txtInvAllowA" runat="server" CssClass="TextBoxAmt60"></asp:TextBox>%</td>
         </tr>
         <tr style="display:none;">
             <td class="textlevel" style="height: 16px;background-color:#8EC439;">
                 15% Rebate</td>
             <td style="height: 16px;background-color:#8EC439;">
                 <asp:TextBox ID="txtIR15M" runat="server" CssClass="TextBoxAmt60" Width="100px"></asp:TextBox></td>
             <td style="height: 16px;background-color:#8EC439;">
                 <asp:TextBox ID="txtIR15F" runat="server" CssClass="TextBoxAmt60" Width="100px"></asp:TextBox></td>
             <td style="height: 16px;background-color:#8EC439;">
                 <asp:TextBox ID="txtIR15A" runat="server" CssClass="TextBoxAmt60" Width="100px"></asp:TextBox></td>
         </tr>
        
         <tr style="display:none;">
             <td class="textlevel" style="background-color: #aaacae" visible="false">
                 Minimum Tax</td>
             <td style="height: 16px; background-color: #aaacae">
                 <asp:TextBox ID="txtMinTaxM" runat="server" CssClass="TextBoxAmt100" Width="100px"></asp:TextBox></td>
             <td style="height: 16px; background-color: #aaacae">
                 <asp:TextBox ID="txtMinTaxF" runat="server" CssClass="TextBoxAmt100" Width="100px"></asp:TextBox></td>
             <td style="height: 16px; background-color: #aaacae">
                 <asp:TextBox ID="txtMinTaxA" runat="server" CssClass="TextBoxAmt100" Width="100px"></asp:TextBox></td>
         </tr>
        <tr>
            <td></td>
            <td style="text-align:right;"></td>
            <td style="text-align: right">
            </td>
            <td style="text-align: right">
            <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" OnClick="btnSave_Click" /></td>
        </tr>
     </table>
     <fieldset><legend><strong>Tax Rebate Structure Threshold Level</strong></legend> 
     <table>
        <tr>
            <td class="textlevelleft" style="background-color: #00ffcc">  SL No</td>
            <td class="textlevelleft" align="center" style="background-color: #00ffcc">Start</td>
            <td class="textlevelleft" style="background-color: #00ffcc">End</td>
            <td class="textlevelleft" style="background-color: #00ffcc">Top Band</td>
            <td class="textlevelleft" style="background-color: #00ffcc">Slot</td>
            <td class="textlevelleft" style="background-color: #00ffcc">%</td>
            <td></td>
        </tr>
         <tr>
            <td style="background-color: #00ff99"><asp:TextBox ID="txtSLNo" runat="server" CssClass="TextBoxAmt60"  Width="40px"></asp:TextBox></td>
            <td style="background-color: #00ff99"><asp:TextBox ID="txtStartRange" runat="server" CssClass="TextBoxAmt60" Width="90px"></asp:TextBox></td>
            <td style="background-color: #00ff99"><asp:TextBox ID="txtEndRange" runat="server" CssClass="TextBoxAmt60"  Width="90px"></asp:TextBox></td>
            <td style="background-color: #00ff99"><asp:TextBox ID="txtTopBand" runat="server" CssClass="TextBoxAmt60"  Width="90px"></asp:TextBox></td>
            <td style="background-color: #00ff99"><asp:TextBox ID="txtSlot" runat="server" CssClass="TextBoxAmt60"  Width="90px"></asp:TextBox></td>
            <td style="background-color: #00ff99"><asp:TextBox ID="txtPercentage" runat="server" CssClass="TextBoxAmt60"  Width="50px"></asp:TextBox></td>
            <td><asp:Button ID="btnIRAdd" runat="server" Text="Add" Width="100px" OnClick="btnIRAdd_Click"/></td>
        </tr>
     </table> 
     
     <div class="departmentSetupGrid450" style="height: 145px">        
        <asp:GridView ID="grInvRebateSlab" runat="server"  AutoGenerateColumns="False"
          DataKeyNames="ITIRPolicyId" EmptyDataText="No Record Found" Font-Size="9px" 
          Width="98%" OnRowCommand="grInvRebateSlab_RowCommand">
          <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
          <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
          <AlternatingRowStyle BackColor="#EFF3FB" />
          <Columns>
          <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
            <ItemStyle CssClass="ItemStylecss" Width="5%" />
          </asp:ButtonField>        
          <asp:BoundField DataField="SLNo" HeaderText="SL No">
            <ItemStyle CssClass="ItemStylecssRight" Width="5%" />
          </asp:BoundField>
          <asp:BoundField DataField="StartRange" HeaderText="Start Range">
            <ItemStyle CssClass="ItemStylecssRight" Width="15%" />
          </asp:BoundField>
          <asp:BoundField DataField="EndRange" HeaderText="End Range">
            <ItemStyle CssClass="ItemStylecssRight" Width="15%" />
          </asp:BoundField>
          <asp:BoundField DataField="TopBand" HeaderText="Top Band">
            <ItemStyle CssClass="ItemStylecssRight" Width="15%" />
          </asp:BoundField>
           <asp:BoundField DataField="Slot" HeaderText="Slot">
            <ItemStyle CssClass="ItemStylecssRight" Width="15%" />
          </asp:BoundField>
          <asp:BoundField DataField="Percentage" HeaderText="%">
            <ItemStyle CssClass="ItemStylecssRight" Width="5%" />
          </asp:BoundField>         
          <asp:ButtonField CommandName="DeleteClick" HeaderText="Delete" Text="Delete">
            <ItemStyle CssClass="ItemStylecss" Width="5%" />
          </asp:ButtonField>
          </Columns>
        </asp:GridView>        
      </div>
        <asp:HiddenField ID="hfIsUpdate" runat="server" />
        <asp:HiddenField ID="hfIRId" runat="server" />
        </fieldset> 
     </div>     
</div>
</asp:Content>

