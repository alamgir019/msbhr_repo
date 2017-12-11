<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="EmpFileUpload.aspx.cs" Inherits="EIS_EmpFileUpload" Title="Employee Personal File Upload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>
    
    
    <div class="formStyle" style="height: 600px; width: 950px;">
        <div id="formhead1">
            Employee Personal File Upload</div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
       <div style="margin-left:10px;margin-right:10px;margin-top:10px;">
           <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="500px">
               <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                   <HeaderTemplate>
                       File Upload
                   </HeaderTemplate>
                   <ContentTemplate>
                         <fieldset style="margin-left:10px;margin-right:10px;margin-top:10px;">
                                <table style="width:100%;">
                                <tr>
                                    <td style="width:49%;" valign="top" >
                                        <table style="width:100%;">
                                        <tr>
                                            <td class="textlevel">
                                                Select file :
                                            </td>
                                            <td style="width:40%">
                                                <asp:FileUpload ID="FileUploadControl" runat="server" Width="326px" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="FileUploadControl"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="textlevel" valign="top" style="height: 1px">
                                                Rename File :
                                            </td>
                                            <td style="width:40%; height: 1px;" valign="top">
                                               <asp:TextBox ID="txtRename" Width="170px" runat="server"></asp:TextBox>
                                                <asp:DropDownList ID="ddlRenameType" runat="server" Width="60px">
                                                    <asp:ListItem Value="0">Prefix</asp:ListItem>
                                                    <asp:ListItem Value="1">Postfix</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                           <td class="textlevel" valign="top">
                                                Selected Directory :
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox ID="txtSelectedDirectory" runat="server" Width="236px" Enabled="False" ForeColor="Blue"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtSelectedDirectory"></asp:RequiredFieldValidator>
                                            </td> 
                                        </tr>
                                        <tr>
                                            <td>
                                             <asp:Button ID="btnRefresh" Text="Refresh" runat="server" OnClick="btnRefresh_Click" Width="80px" />
                                            </td>
                                            <td style="text-align:right;">
                                            <asp:Button runat="server" ID="UploadButton" Text="Upload" OnClick="UploadButton_Click" Width="80px" /></td>
                                        </tr>
                                        </table>
                                        <asp:Label ID="lblPath" runat="server" ForeColor="White"></asp:Label>
                                        <asp:Label ID="lblNodeText" runat="server" ForeColor="White"></asp:Label>
                                   </td>
                                   <td style="width:49%;border: solid 1px Gray;overflow:hidden;">
                                        <table style="width:100%;">
                                            <tr>
                                                <td style="width: 50%;background-color:#F2DBDB;text-align:center;font-weight:bold;border: solid 1px Gray;">
                                                      Select a Directory
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                      <table>
                                                        <tr>
                                                            <td class="textlevel">
                                                                Node Title:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtNode" Width="125px" runat="server"></asp:TextBox></td>
                                                            <td>
                                                                <asp:Button ID="btnFind" runat="server" Text="Find" OnClick="btnFind_Click1" Width="65px" CausesValidation="False"/></td>
                                                        </tr>
                                                     </table>
                                                      <div style="margin-left: 10px;margin-top:5px; height: 400px; overflow: scroll;background-color:#B8CCE4;width:100%;">
                                                        <asp:TreeView ID="MyTree" PathSeparator="|" ExpandDepth="0" runat="server" ImageSet="Arrows"
                                                            AutoGenerateDataBindings="False" OnSelectedNodeChanged="MyTree_SelectedNodeChanged">
                                                            <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px"
                                                                ForeColor="#5555DD"></SelectedNodeStyle>
                                                            <NodeStyle VerticalPadding="0px" Font-Names="Tahoma" Font-Size="10pt" HorizontalPadding="5px"
                                                                ForeColor="Black" NodeSpacing="0px"></NodeStyle>
                                                            <ParentNodeStyle Font-Bold="False" />
                                                            <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD"></HoverNodeStyle>
                                                        </asp:TreeView>
                                                      </div>
                                                </td>
                                            </tr>
                                        </table>
                                   </td>
                                </tr>
                                </table>
                        </fieldset>
                   </ContentTemplate>
               </cc1:TabPanel>
               <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                   <HeaderTemplate>
                       Create Employee Directory
                   </HeaderTemplate>
                   <ContentTemplate>
                     <fieldset style="margin-left:10px;margin-right:10px;margin-top:10px;">
                        <table style="width:100%;">
                            <tr>
                                <td style="width:49%;" valign="top" >
                                    <table>
                                        <tr>
                                            <td class="textlevel">Directory Name:</td>
                                            <td><asp:TextBox ID="txtDirNameTab2" runat="server" Width="200px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="textlevel" valign="top">Select Child Directory:</td>
                                            <td valign="top">
                                                <asp:CheckBoxList ID="chkListDirectory" runat="server" Width="205px">
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <asp:Button ID="btnRefreshTab2" runat="server" Text="Refresh" OnClick="btnRefreshTab2_Click"  CausesValidation="False"/>
                                            </td>
                                            <td valign="top" align="right">
                                                <asp:Button ID="btnCreateParentTab2" runat="server" Text="Create Parent" Width="100px" 
                                                OnClick="btnCreateParentTab2_Click" CausesValidation="False"/>
                                                <asp:Button ID="btnCreateChildTab2" runat="server" Text="Create" Width="100px" CausesValidation="False" OnClick="btnCreateChildTab2_Click" /></td>
                                        </tr>
                                    </table>
                                   
                                </td>
                                 <td style="width:49%;border: solid 1px Gray;overflow:hidden;" valign="top" >
                                    <table style="width:100%;">
                                            <tr>
                                                <td style="width: 50%;background-color:#F2DBDB;text-align:center;font-weight:bold;border: solid 1px Gray;">
                                                      Select a Directory
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%;background-color:#C2D69B;text-align:center;font-weight:bold;border: solid 1px Gray;">
                                                    <asp:Button ID="btnLoadDirectoryTab2" runat="server" Text="Load Directory List" Width="150px" Font-Bold="True" ForeColor="Blue" OnClick="btnLoadDirectoryTab2_Click" CausesValidation="False" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                      <table>
                                                        <tr>
                                                            <td class="textlevel">
                                                                Node Title:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextBox1" Width="125px" runat="server"></asp:TextBox></td>
                                                            <td>
                                                                <asp:Button ID="btnFindTab2" runat="server" Text="Find" OnClick="btnFindTab2_Click" Width="65px" CausesValidation="false" /></td>
                                                        </tr>
                                                     </table>
                                                      <div style="margin-left: 2px;margin-top:5px; height: 370px; overflow: scroll;background-color:#B8CCE4;width:100%;">
                                                        <asp:TreeView ID="MyTreeTab2" PathSeparator="|" ExpandDepth="0" runat="server" ImageSet="Arrows"
                                                            AutoGenerateDataBindings="False" OnSelectedNodeChanged="MyTreeTab2_SelectedNodeChanged" >
                                                            <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px"
                                                                ForeColor="#5555DD"></SelectedNodeStyle>
                                                            <NodeStyle VerticalPadding="0px" Font-Names="Tahoma" Font-Size="10pt" HorizontalPadding="5px"
                                                                ForeColor="Black" NodeSpacing="0px"></NodeStyle>
                                                            <ParentNodeStyle Font-Bold="False" />
                                                            <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD"></HoverNodeStyle>
                                                        </asp:TreeView>
                                                      </div>
                                                </td>
                                            </tr>
                                        </table>
                                </td>
                            </tr>
                        </table>
                     </fieldset>
                   </ContentTemplate>
               </cc1:TabPanel>
               <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
                <HeaderTemplate>
                    Add New Directory To List
                </HeaderTemplate>
                <ContentTemplate>
                                    <fieldset style="border:solid 1px Gray;height:490px;">
                                    <table>
                                        <tr>
                                            <td class="textlevel">
                                                Enter Directory Name:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewDirNameTab2" runat="server" Width="200px"></asp:TextBox></td>
                                            <td>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="textlevel">
                                            Enter Directory Level
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewDirLevelTab2" runat="server" Width="60px"></asp:TextBox></td>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAddTab2" runat="server" Text="Add"  Width="60px" OnClick="btnAddTab2_Click" CausesValidation="false"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="2">
                                                <div style="overflow:scroll;height:410px;">
                                               
                                                 <asp:GridView id="grDirectory" runat="server"  DataKeyNames="DirName"   AutoGenerateColumns="False" 
                                                 EmptyDataText="No Record Found" Font-Size="9px" OnRowCommand="grDirectory_RowCommand" >
                                                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True"></SelectedRowStyle>
                                                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                                <Columns>
                                                <asp:BoundField DataField="DirName" HeaderText="Directory Name">
                                                  <ItemStyle CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="DirLevel" HeaderText="Directory Level">
                                                  <ItemStyle CssClass="ItemStylecss"></ItemStyle>
                                                </asp:BoundField>
                                                 <asp:ButtonField CommandName="DoubleClick" HeaderText="Delete" Text="Delete">
                                                  <ItemStyle CssClass="ItemStylecss" Width="40px"/>
                                                </asp:ButtonField>
                                                </Columns>
                                              </asp:GridView>
                                               </div>
                                            </td>
                                        </tr>
                                    </table>
                                    </fieldset>
                </ContentTemplate>
                
               </cc1:TabPanel>
           </cc1:TabContainer>
        </div>
        </div>
        
</asp:Content>
