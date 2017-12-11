<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="PrivilegePackage.aspx.cs" Inherits="PrivilegePackage" Title="Previlege Package" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">



        function CheckBoxListSelect(cbControl, state) {
            var chkBoxList = document.getElementById(cbControl);
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }
            chkBoxCount[0].checked = true;
            return false;
        }
    </script>
    <div class="userPermision">
        <div id='formhead5'>
            <div style="width: 96%; float: left;">
                Privilege Package</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <!--Div for group-->
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="userPermisionInner">
            <!--Div for Controls-->
            <table>
                <tr>
                    <td style="height: 7px; width: 161px;">
                        <asp:Label ID="lblDept" runat="server" CssClass="textlevel" Text="Package Name" Width="136px"></asp:Label>
                    </td>
                    <td style="width: 49px; height: 7px;">
                        <asp:TextBox ID="txtPackName" runat="server" Width="435px"></asp:TextBox>
                    </td>
                    <td style="height: 7px; width: 3px;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPackName"
                            ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
            &nbsp;
            <table style="width: 100%;">
                <tr>
                    <td style="width: 50%; border-right: solid 5px #CCCCCC;">
                        <asp:GridView ID="grPriv" runat="server" AutoGenerateColumns="False" DataKeyNames="VIEWID,VIEWNAME,parentid,nodelevel"
                            EmptyDataText="No Record Found" Font-Size="9px" Width="100%">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" Font-Size="11px" Height="25px" />
                            <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                            <AlternatingRowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:BoundField DataField="VIEWNAME" HeaderText="Screen Name">
                                    <ItemStyle CssClass="ItemStylecss" Width="100%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="All">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td style="width: 50%;" valign="top">
                        <asp:GridView ID="grPrivPack" runat="server" AutoGenerateColumns="False" DataKeyNames="PrivPackID"
                            EmptyDataText="No Record Found" Font-Size="9px" Width="100%" 
                            OnRowCommand="grPrivPack_RowCommand" 
                            onselectedindexchanged="grPrivPack_SelectedIndexChanged">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" Font-Size="11px" Height="25px" />
                            <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                            <AlternatingRowStyle BackColor="#FFFFCC" />
                            <RowStyle Height="20px" />
                            <Columns>
                                <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                    <ItemStyle CssClass="ItemStylecss" Width="5%" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="PrivPackName" HeaderText="Package List">
                                    <ItemStyle CssClass="ItemStylecss" Width="85%" />
                                </asp:BoundField>
                                <asp:ButtonField CommandName="SyncClick" HeaderText="Synchronize" Text="Synchronize">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%" />
                                </asp:ButtonField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <!--Grid view Code Ends-->
            <asp:HiddenField ID="hfPrivPackID" runat="server" />
        </div>
        <div id="DivCommand1">
            <table style="width: 100%; padding-top: 2px; padding-right: 15px;">
                <tr>
                    <td align="left">
                        Select <a id="A1" href="#" onclick="javascript: CheckBoxListSelect ('<%= grPriv.ClientID %>',true)">
                            All</a> | <a id="A2" href="#" onclick="javascript: CheckBoxListSelect ('<%= grPriv.ClientID %>',false)">
                                None</a>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
