<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="BudgetTitleSetup.aspx.cs" Inherits="Training_BudgetTitleSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>
    <div class="officeSetup">
        <div id="formhead1">
            <div style="width: 96%; float: left;">
                Budget Title Setup</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <!--Div for group-->
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="officeSetupInner">
            <fieldset>
                <!--Div for Controls-->
                 <asp:HiddenField ID="hfBTitleId" runat="server" />
               <asp:HiddenField ID="hfIsUpdate" runat="server" />
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" CssClass="textlevel" Text="Title Type :"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlBTitleType" runat="server" Height="25px" Width="304px">
                               <%-- <asp:ListItem Text="Select Title Type" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Training Course Fee" Value="C"></asp:ListItem>
                                <asp:ListItem Text="Residential Cost" Value="R"></asp:ListItem>
                                <asp:ListItem Text="Non-Residential Cost" Value="N"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>                         
                         <td style="height: 22px">
                            <asp:CheckBox ID="chkIsActive" runat="server" CssClass="textlevelleft" Text="Make Active" />
                        </td>  
                    </tr>
                    <tr>  
                         <td>
                                <asp:Label ID="Label1" runat="server" CssClass="textlevel" Text="Budget Title:"></asp:Label>
                        </td>                  
                         <td>
                            <asp:TextBox ID="txtBTitle" runat="server" Width="300px" TextMode="MultiLine"></asp:TextBox>   
                        </td>
                        <td>                      
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBTitle" ErrorMessage="*"></asp:RequiredFieldValidator>
                         </td>                  
                    </tr>
                </table>
            </fieldset>
        </div>
        <div class="GridFormat1">
            <!--Grid view Code starts-->
            <asp:GridView ID="grList" runat="server" DataKeyNames="BTitleId,BTitleType,BTitle,IsActive,BTitleTypeName" AutoGenerateColumns="False"
                EmptyDataText="No Record Found" Font-Size="9px" Width="99%" 
                onrowcommand="grList_RowCommand">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                </SelectedRowStyle>
                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                <Columns>
                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="BTitleTypeName" HeaderText="Title Type">
                        <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="BTitle" HeaderText="Budget Title">
                        <ItemStyle CssClass="ItemStylecss" Width="80%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="IsActive" HeaderText="Is Active">
                        <ItemStyle CssClass="ItemStylecssCenter" Width="10%"></ItemStyle>
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
    </div>
</asp:Content>

