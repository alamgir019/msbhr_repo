<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="UserTaskPermission.aspx.cs" Inherits="UserTaskPermission" Title="User Task Permission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js">
    //Date Time Picker script
    </script>

    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js">
    </script>

    <script language="javascript" type="text/javascript">
    function ToUpper(ctrl)
    {   
    var t = ctrl.value;
    ctrl.value = t.toUpperCase(); 
    }
    </script>

    <div class="empEduForm">
        <div id="formhead2">
            <div style="width: 97%; float: left;">
                User Task Permission</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="PayrollConfigInner">
            <fieldset style="margin-bottom: 10px;">                
                <table>
                    <tr>
                        <td class="textlevel">
                            User Id</td>
                        <td>
                            <asp:DropDownList ID="ddlUserId" runat="server" CssClass="textlevelleft" Width="150px">                                
                            </asp:DropDownList></td>
                        <td class="textlevel">
                            Screen</td>
                        <td>
                            <asp:DropDownList ID="ddlScreen" runat="server" CssClass="textlevelleft" Width="150px">
                            </asp:DropDownList></td>                   
                        <td class="textlevel">
                            Task :</td>
                        <td>
                            <asp:DropDownList ID="ddlTask" runat="server" CssClass="textlevelleft" Width="300px">
                            </asp:DropDownList></td>    
                        <td class="textlevel">
                            Authorize :</td>
                        <td>
                            <asp:DropDownList ID="ddlAuthorize" runat="server" CssClass="textlevelleft">
                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                <asp:ListItem Value="N">No</asp:ListItem>
                            </asp:DropDownList></td>    
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click"
                                ToolTip="Click on Save Button to store the employee data." />
                        </td>                         
                    </tr>                    
                </table>
                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                <asp:HiddenField ID="hfId" runat="server" />
            </fieldset>
            <fieldset>
                <legend>User Wise Task List</legend>
                <div style="overflow: scroll; width: 100%; height: 250px">
                    <asp:GridView ID="grTaskList" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                        AutoGenerateColumns="False">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                <ItemStyle Width="5%" CssClass="ItemStylecss" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="UserId" HeaderText="User Id">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ViewName" HeaderText="Screen Name">
                                <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TaskDesc" HeaderText="Task Details">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="IsAuthorize" HeaderText="Authorize">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>                            
                        </Columns>
                    </asp:GridView>
                </div>
            </fieldset>
        </div>        
    </div>
</asp:Content>
