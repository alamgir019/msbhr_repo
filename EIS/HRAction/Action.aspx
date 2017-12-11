<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="Action.aspx.cs" Inherits="EIS_Action" Title="Action List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>

    <div class="departmentSetup">
        <div id='formhead1'>
            <div style="width: 92%; float: left;">
                Action Setup
            </div>
             <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <!--Div for group-->
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="departmentSetupInner">
            <!--Div for Controls-->
            <asp:HiddenField ID="hfID" runat="server" />
            <table>
                <tr>
                    <td class="textlevel">
                        Action Name :</td>
                    <td>
                        <asp:TextBox ID="txtAction" runat="server" Width="250px" ToolTip="Input the action name."></asp:TextBox></td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAction"
                            ErrorMessage="*"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td class="textlevel">
                        Description :</td>
                    <td>
                        <asp:TextBox ID="txtDescription" runat="server" Width="250px" TextMode="MultiLine"
                            Rows="3" ToolTip="This is an optional value. If you want to keep some notes about this action setup."></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="textlevel">
                        Action Type :</td>
                    <td>
                        <asp:TextBox ID="txtActionType" runat="server" MaxLength="3" Width="50px" ToolTip="Input the action type."></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="textlevel">
                        Action Nature:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlActionNature" runat="server" CssClass="textlevelleft" Width="255px">
                            <asp:ListItem Value="A">Advice</asp:ListItem>  
                            <asp:ListItem Value="R">Additional Responsibility</asp:ListItem>                            
                            <asp:ListItem Value="C">Confirmation</asp:ListItem>
                            <asp:ListItem Value="D">Disciplinary</asp:ListItem>
                            <asp:ListItem Value="P">Promotion/Transfer/Re-designation</asp:ListItem>
                            <asp:ListItem Value="T">TDY</asp:ListItem>    
                            <asp:ListItem Value="S">Separation</asp:ListItem>
                            <asp:ListItem Value="M">Amendment of Salary</asp:ListItem>   
                            <asp:ListItem Value="I">Salary Increment</asp:ListItem>                            
                            <asp:ListItem Value="E">Contract Extension</asp:ListItem>                            
                        </asp:DropDownList></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Make Inactive" ForeColor="Black"
                            ToolTip="Tick this option you do not want to use action currently. An Inactive action will not be available for whole system. But it is important to check whether action has assigned to any employee.">
                        </asp:CheckBox></td>
                    <td>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hfIsUpdate" runat="server" />
        </div>
        <div class="departmentSetupInner">
            <div class="departmentSetupGrid450">
                <!--Grid view Code starts-->
                <asp:GridView ID="grAction" runat="server" AutoGenerateColumns="False" DataKeyNames="ActionID,ActionNature"
                    EmptyDataText="No Record Found" Font-Size="9px" Width="100%" OnRowCommand="grAction_RowCommand"
                    ToolTip="You will find the entire existing action list. Click on any action Edit link from the list.">
                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                    <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                    <AlternatingRowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                            <ItemStyle CssClass="ItemStylecss" Width="10%" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="ActionName" HeaderText="Action Name">
                            <ItemStyle CssClass="ItemStylecss" Width="30%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ActionDesc" HeaderText="Action Description">
                            <ItemStyle CssClass="ItemStylecss" Width="30%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ActionType" HeaderText="Action Type">
                            <ItemStyle CssClass="ItemStylecssCenter" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ActionNature" HeaderText="Action Nature">
                            <ItemStyle CssClass="ItemStylecss" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IsActive" HeaderText="Active">
                            <ItemStyle CssClass="ItemStylecssCenter" Width="10%" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                <!--Grid view Code Ends-->
            </div>
            <div class="DivCommand1">
                <div class="DivCommandL">
                    <asp:Button ID="btnClear" runat="server" Text="Refresh" Width="70px" OnClick="btnClear_Click"
                        UseSubmitBehavior="False" ToolTip="To clear all controls click on Refresh Button." />
                </div>
                <div class="DivCommandR">
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click"
                        ToolTip="After providing all the necessary values click on Save button to store the information." />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                        OnClick="btnDelete_Click" ToolTip="To Delete any existing action just click on the Delete Button." />
                </div>
            </div>
        </div>
    </div>
    <br />
    <br />
    <br />
</asp:Content>
