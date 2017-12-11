<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="PFActivities.aspx.cs" Inherits="Payroll_Loan_PFActivities" Title="Monthly PF Activites" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
    function printDiv(divName) 
    {
         var printContents = document.getElementById(divName).innerHTML;
         var originalContents = document.body.innerHTML;

         document.body.innerHTML = printContents;

         window.print();

         document.body.innerHTML = originalContents;
    }
  </script>
<div class="formStyle">
    <div id="formhead1">
      <div style="width:92%;float:left;">Monthly PF Activites</div>
      <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../File/Home.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
    </div>
    <div class="MsgBox">
      <asp:Label ID="lblMsg" runat="server" Width="500px" Font-Bold="True" ForeColor="Fuchsia"></asp:Label>
    </div>
    <div class="Div950" >
     <div style="background-color:#EFF3FB;">
        <fieldset>
            <table>
                <tr>
                    <td class="textlevel">Month</td>
                    <td><asp:DropDownList ID="ddlMonth" runat="server" Width="105px"></asp:DropDownList></td>
                    <td class="textlevel">Fiscal Year</td>
                    <td><asp:DropDownList ID="ddlFiscalYear" runat="server" Width="105px"></asp:DropDownList></td>
                    <td><asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" /></td>
                </tr>
            </table>
        </fieldset>
     </div>
     <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="2" Width="100%" Height="400px">
      <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Width="100%" Height="400px">
      <HeaderTemplate>Loan</HeaderTemplate>
      <ContentTemplate>
        <fieldset>
            <div style="overflow:scroll;width:99%;height:390px;">
                <asp:Button ID="btnPrint1" runat="server" Text="Print" Width="200px" onclientclick="printDiv('PrintMe1')" />
                <asp:Button ID="btnExport1" runat="server" Text="Export to Excel "
                    Width="200px" OnClick="btnExport1_Click" />
                <hr />
                <div id="Div2" >
                 <asp:GridView id="grLoan" runat="server" 
                                    EmptyDataText="No Record Found" Font-Size="9px">
                    <HeaderStyle BackColor="#3366CC" ForeColor="White" Font-Bold="True"></HeaderStyle>
                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True"></SelectedRowStyle>
                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                 </asp:GridView>
                </div>
            </div>
        </fieldset>
      </ContentTemplate>
      </cc1:TabPanel>
         <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
             <HeaderTemplate>
                 Adjustment
             </HeaderTemplate>
             <ContentTemplate>
                 <fieldset>
                    <div style="overflow:scroll;width:99%;height:390px;">
                        <asp:Button ID="btnPrint2" runat="server" Text="Print" Width="200px" onclientclick="printDiv('PrintMe2')" />
                        <asp:Button ID="btnExport2" runat="server"  Text="Export to Excel "
                            Width="200px" OnClick="btnExport2_Click" />
                        <hr />
                        <div id="Div1" >
                         <asp:GridView id="grAdjust" runat="server" 
                                            EmptyDataText="No Record Found" Font-Size="9px" >
                            <HeaderStyle BackColor="#3366CC" ForeColor="White" Font-Bold="True"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True"></SelectedRowStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                         </asp:GridView>
                        </div>
                    </div>
                 </fieldset>
             </ContentTemplate>
         </cc1:TabPanel>
         <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
             <HeaderTemplate>
                 Final Payment
             </HeaderTemplate>
             <ContentTemplate>
                 <fieldset>
                    <div style="overflow:scroll;width:99%;height:390px;">
                        <asp:Button ID="btnPrint3" runat="server" Text="Print" Width="200px" onclientclick="printDiv('PrintMe3')" />
                        <asp:Button ID="btnExport3" runat="server"  Text="Export to Excel "
                            Width="200px" OnClick="btnExport3_Click" />
                        <hr />
                        <div id="PrintMe3" >
                         <asp:GridView id="grFinalPayment" runat="server" 
                                            EmptyDataText="No Record Found" Font-Size="9px" >
                            <HeaderStyle BackColor="#3366CC" ForeColor="White" Font-Bold="True"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True"></SelectedRowStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                         </asp:GridView>
                        </div>
                    </div>
                 </fieldset>
             </ContentTemplate>
         </cc1:TabPanel>
     </cc1:TabContainer>
    </div>
</div>
</asp:Content>

