<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="AccruedVacationUpload.aspx.cs" Inherits="Leave_AccruedVacationUpload" Title="Accrued Vacation Upload" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
  <div class="EmpLeaveProfStyle" style="height:550px;">
    <div id='formhead4'> 
        <div style="width:95%;float:left;">Accrued Vacation Upload</div>
         <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>   
    </div>
    <div class="MsgBox">
      <!--Div for msg-->
      <asp:Label ID="lblMsg" runat="server" CssClass="lblMsg"></asp:Label>
    </div>
    <div class="Div950">
      <!--Div for group-->
      <fieldset>
        <legend>Accrued Vacation </legend>
        <table>
            <tr>
                <td class="textlevel">
                    Month</td>
                <td>
                    <asp:DropDownList ID="ddlMonth" runat="server" Width="100px" CssClass="ItemStylecssCenter">
                    </asp:DropDownList></td>
                <td class="textlevel">
                    Year</td>
                <td class="textlevelleft">
                    <asp:DropDownList ID="ddlYear" runat="server" Width="80px" CssClass="ItemStylecssCenter">
                    </asp:DropDownList></td>
                <td class="textlevel">
                    Fiscal Year</td>
                <td>
                    <asp:DropDownList ID="ddlFiscalYear" runat="server" CssClass="ItemStylecssCenter"
                        Width="120px">
                    </asp:DropDownList></td>
                <td>
                    <asp:Button id="btnGenerate" runat="server" Text="Generate" 
                        OnClick="btnGenerate_Click" ></asp:Button>
                </td>
            </tr>   
          </table>
          <fieldset><legend>Accrued Vacation List</legend> 
          <div id="empSearchResult">
                <asp:GridView id="grAccruedVacation" runat="server" Width="97%" Font-Size="9px" PageSize="7" EmptyDataText="No Record Found" 
                AutoGenerateColumns="False">
                  <Columns>
                  <asp:BoundField HeaderText="SL No">
                    <ItemStyle Width="5%" CssClass="ItemStylecssCenter"></ItemStyle>
                  </asp:BoundField>
                  <asp:BoundField DataField="EmpId" HeaderText="Emp Id">
                    <ItemStyle Width="15%" CssClass="ItemStylecss"></ItemStyle>
                  </asp:BoundField>
                  <asp:BoundField DataField="FullName" HeaderText="Employee Name">
                    <ItemStyle Width="25%" CssClass="ItemStylecss"></ItemStyle>
                  </asp:BoundField>
                  <asp:BoundField DataField="NoOfAccuredVacation" HeaderText="No of Accrued Vacation">
                    <ItemStyle Width="10%" CssClass="ItemStylecssRight"></ItemStyle>
                  </asp:BoundField>                   
                  <asp:BoundField DataField="Gross" HeaderText="Monthly Salary">
                    <ItemStyle Width="10%" CssClass="ItemStylecssRight"></ItemStyle>
                  </asp:BoundField> 
                   <asp:BoundField  HeaderText="Daily Rate">
                    <ItemStyle Width="10%" CssClass="ItemStylecssRight"></ItemStyle>
                  </asp:BoundField>    
                  <asp:BoundField  HeaderText="Total Accrued">
                    <ItemStyle Width="10%" CssClass="ItemStylecssRight"></ItemStyle>
                  </asp:BoundField>     
                  <asp:BoundField HeaderText="Total Accrued in USD ">
                    <ItemStyle Width="10%" CssClass="ItemStylecssRight"></ItemStyle>
                  </asp:BoundField>                    
                  </Columns>
                  <HeaderStyle BackColor="#B3CDE4" HorizontalAlign="Center" Font-Size="10px" Font-Bold="True"></HeaderStyle>
                  <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                </asp:GridView> </div></fieldset> 
      </fieldset>
    </div>
    <div class="DivCommand1" style="padding-right:15px; float:right; padding-top:3px; width: 79px;">
      <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" UseSubmitBehavior="False" OnClick="btnSave_Click" />
    </div>
  </div>
</asp:Content>
