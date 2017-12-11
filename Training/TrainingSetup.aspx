<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="TrainingSetup.aspx.cs" Inherits="Training_TrainingSetup" Title="Training Setup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>
  
    <div class="officeSetup">
        <div id="formhead1">
            <div style="width: 96%; float: left;">
                Training Setup</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
   <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%"
                    Height="500px">
   <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Width="860px"
                        Height="450px">                                                                                                                                                                                                                            
    <HeaderTemplate>
            Setup
    </HeaderTemplate>
    <ContentTemplate>
        <!--Div for group-->
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="officeSetupInner">
            <fieldset>
                <!--Div for Controls-->
                <asp:HiddenField ID="hfId" runat="server" /> 
               <asp:HiddenField ID="hfIsUpdate" runat="server" />
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" CssClass="textlevel" Text="Training Name :"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtName" runat="server" Width="305px"></asp:TextBox>
                        </td>
                        <td><asp:RequiredFieldValidator ID="ReqFieldVal" runat="server" ErrorMessage="*" ControlToValidate="txtName"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                    <td> <asp:Label ID="Label1" runat="server" CssClass="textlevel" Text="Training Category :"></asp:Label></td>
                    <td colspan="3">
                     <asp:DropDownList ID="ddlCategoty" runat="server" CssClass="textlevelleft" Width="309px"
                                ToolTip="Select Training Category from the list.">
                            </asp:DropDownList>
                    </td>
                     
                    <td></td>
                    </tr>
                    <tr><td><asp:Label ID="Label5" runat="server" CssClass="textlevel" Text="Tentative Days :"></asp:Label></td>
                    <td><asp:TextBox ID="txtTentativeDays" runat="server" Width="100px" 
                            onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" MaxLength="3"></asp:TextBox></td>
                     <td><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtTentativeDays"></asp:RequiredFieldValidator></td>
                    <td>&nbsp;</td>
                     <td style="height: 22px">
                            <asp:CheckBox ID="chkInActive" runat="server" CssClass="textlevelleft" Text="Make Inactive" />
                        </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td></td>
                    </tr>
                    <tr>
                    <td><asp:Label ID="Label6" runat="server" CssClass="textlevel" Text="In House/Out Door :"></asp:Label></td>
                    <td><asp:DropDownList ID="ddlInOut" runat="server" CssClass="textlevelleft" Width="104px">
                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                    </asp:DropDownList></td>
                    <td><asp:Label ID="Label7" runat="server" CssClass="textlevel" Text="Medicos Y/N :"></asp:Label></td>
                    <td><asp:DropDownList ID="ddlMedicos" runat="server" CssClass="textlevelleft" Width="104px">
                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                    </asp:DropDownList></td>
                    <td></td>
                    </tr>
                    <tr>
                    <td><asp:Label ID="Label8" runat="server" CssClass="textlevel" Text="Cost Per Person :"></asp:Label></td>
                    <td><asp:TextBox ID="txtCostPP" runat="server" Width="100px" 
                            onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" 
                            MaxLength="9"/></td>
                    <td><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtCostPP"></asp:RequiredFieldValidator></td>
                    <td><asp:Label ID="Label9" runat="server" CssClass="textlevel" Text="Income Per Person :"></asp:Label></td>
                    <td><asp:TextBox ID="txtIncomePP" runat="server" Width="100px" 
                            onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" 
                            MaxLength="9"/></td>
                    <td><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtIncomePP"></asp:RequiredFieldValidator></td>
                    <td></td>
                    </tr>
                 </table>
            </fieldset>
            <br />
            <fieldset>   
                 <table>
                    <tr>
                        <td> 
                            <asp:Label ID="Label3" runat="server" CssClass="textlevel" Text="Designation :"/> 
                        </td>
                        <td colspan="3">
                             <asp:DropDownList ID="ddlDesig" runat="server" CssClass="textlevelleft" Width="200px">                             
                             </asp:DropDownList>
                        </td>
                        <td></td>
                         <td style="height: 22px">
                        <asp:Label ID="Label4" runat="server" CssClass="textlevel"  Text="Period(MM)"/>
                            </td>
                         <td><asp:TextBox ID="txtPeriod" runat="server" Width="150px" 
                                 onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32)" MaxLength="5"/></td>                     
                      <td></td>
                       <td>
                        <asp:Button ID="btnAdd" runat="server" Text="Add" Width="60px" 
                                CausesValidation="False" onclick="btnAdd_Click"/>
                       </td>
                     </tr>                    
                </table>
            </fieldset>
        </div>
        <div class="GridFormat1">
            <!--Grid view Code starts-->
            <asp:GridView ID="grList" runat="server" DataKeyNames="DesigId,Designame,PeriodMM" AutoGenerateColumns="False"
                EmptyDataText="No Record Found" Font-Size="9px" Width="99%" 
                onrowcommand="grList_RowCommand" OnRowDeleting="grList_RowDeleting">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                </SelectedRowStyle>
                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                <Columns>
                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                    </asp:ButtonField>
                     <asp:ButtonField CommandName="Delete" HeaderText="Delete" Text="Delete">
                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="Designame" HeaderText="Designation Name">
                        <ItemStyle CssClass="ItemStylecss" Width="70%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="PeriodMM" HeaderText="Period (MM)">
                        <ItemStyle CssClass="ItemStylecssRight" Width="20%"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <!--Grid view Code Ends-->
        </div>
        <div class="DivCommand1">
            <div class="DivCommandL">
                <asp:Button ID="btnClear" runat="server" Text="Refresh" Width="70px" OnClick="btnClear_Click"
                    CausesValidation="False" />
            </div>
            <div class="DivCommandR">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                    OnClick="btnDelete_Click" />
            </div>
        </div>
    
    </ContentTemplate>
  </cc1:TabPanel>
  <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel1" Width="860px"
                        Height="900px">
     <HeaderTemplate>
        List
     </HeaderTemplate>
     <ContentTemplate>
     <div class="GridFormat3">
            <!--Grid view Code starts-->
            <asp:GridView ID="grTrainingList" runat="server" DataKeyNames="TrainId,TrainName,TentativeDay,IsActive" AutoGenerateColumns="False"
                EmptyDataText="No Record Found" Font-Size="9px" Width="99%"
                onrowcommand="grTrainingList_RowCommand" OnRowDeleting="grList_RowDeleting">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                </SelectedRowStyle>
                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                <Columns>
                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                    </asp:ButtonField>
                    
                    <asp:BoundField DataField="TrainName" HeaderText="Training Name">
                        <ItemStyle CssClass="ItemStylecss" Width="70%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="TentativeDay" HeaderText="Tentative Day">
                        <ItemStyle CssClass="ItemStylecssRight" Width="20%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="TrCategoryName" HeaderText="Training Category">
                        <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="IsActive" HeaderText="Is Active">
                        <ItemStyle CssClass="ItemStylecssCenter" Width="20%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="IsInHouse" HeaderText="In House/out Door">
                        <ItemStyle CssClass="ItemStylecssCenter" Width="20%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="IsMedicos" HeaderText="Is Medicos">
                        <ItemStyle CssClass="ItemStylecssCenter" Width="20%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="IndvCost" HeaderText="Cost Per Person">
                        <ItemStyle CssClass="ItemStylecssRight" Width="20%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="IndvIncome" HeaderText="Income Per Person">
                        <ItemStyle CssClass="ItemStylecssRight" Width="20%"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <!--Grid view Code Ends-->
         </div>
       </ContentTemplate>
     </cc1:TabPanel>
    </cc1:TabContainer>
   </div>
</asp:Content>
