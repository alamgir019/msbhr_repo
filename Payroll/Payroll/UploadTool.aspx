<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="UploadTool.aspx.cs" 
Inherits="Payroll_UploadTool" Title="Payroll Upload Tool" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="empTrainForm">
        <div id="formhead1">        
            <div style="width: 96%; float: left;">
                Uploader</div>
            <div style="margin: 2px; float: left;">
                <a href="../home.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>  
        
        <div style="margin: 5px 10px 5px 10px; width: 98%;">
        <fieldset>
        <table>
            <tr>
                <td>
                <asp:TreeView ID="tvUpload" runat="server" ImageSet="Arrows" OnSelectedNodeChanged="tvUpload_SelectedNodeChanged" CssClass="textlevelleft" >
            <Nodes>
              <asp:TreeNode Text="Upload List" Value="SRL">
                <%--<asp:TreeNode Text="COLA Upload" Value="CU"></asp:TreeNode>       
                <asp:TreeNode Text="Employee Change Upload" Value="ECU"></asp:TreeNode>                         
                <asp:TreeNode Text="Increment Upload" Value="IU"></asp:TreeNode>--%>
                <asp:TreeNode Text="Bonus Upload" Value="AU"></asp:TreeNode>                 
              <%--  <asp:TreeNode Text="Tax Assessment Upload" Value="TU"></asp:TreeNode>
                <asp:TreeNode Text="Basic Upload" Value="BU"></asp:TreeNode>--%>
              </asp:TreeNode>
            </Nodes>
            <ParentNodeStyle Font-Bold="False" />
            <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
            <SelectedNodeStyle BackColor="#E0E0E0" Font-Underline="True" ForeColor="#5555DD"
                    HorizontalPadding="0px" VerticalPadding="0px" />
            <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="5px"
                    NodeSpacing="0px" VerticalPadding="0px" />
          </asp:TreeView>      
                </td>
                <td>
                <asp:Panel ID="pnlEmpChangeType" runat="server">
            <table>
                <tr>
                    <td class="textlevel">
                        Emp Change Type :</td>
                    <td class="textlevel">
                        <asp:DropDownList ID="ddlChangeType" Width="225px" runat="server" CssClass="textlevel">
                            <asp:ListItem Value="0">Select a type</asp:ListItem>
                            <asp:ListItem Value="1">Emp Designation</asp:ListItem>
                            <asp:ListItem Value="2">Fund Code</asp:ListItem>
                            <asp:ListItem Value="3">PAR Location Code</asp:ListItem>
                            <asp:ListItem Value="4">Payroll Project</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
             </table>
            </asp:Panel>
                
                </td>
            </tr>
            
        </table>
            
            
            </fieldset>
            <fieldset>
                <table style="width: 95%">
                 <thead>
                    <tr>
                        <td colspan=3 style="width: 75%; text-align: center; background-color: #018DCC; border: solid 1px black; color: White">
                            File Location
                        </td>                                 
                    </tr>
                </thead>
                    <tr>
                        <td style="width: 75%; border-bottom: solid 3px #018DCC; color: #3B64C0;">
                            <asp:FileUpload ID="fileuploadExcel" runat="server"  Width="100%" /></td>
                            <td style="width: 90%; border-bottom: solid 3px #018DCC; color: #3B64C0;">
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" Width="170px" Height="22px"/></td>                                     
                            <td style="width: 90%; color: #3b64c0; border-bottom: #018dcc 3px solid">
                                <asp:Button ID="btnVerify" runat="server" Text="Verify" 
                                    OnClick="btnVerify_Click" Width="170px" Height="22px"/></td>
                    </tr>

                </table>
                <asp:Label ID="lblUpload" runat="server" Text="Label" style="width: 95%; text-align: center; background-color: #018DCC; border: solid 1px black; color: White" Visible="False" Width="372px"></asp:Label>
                <fieldset>
                <div class="setupGrid450">                
                     <!--All kind of allowance Upload-->                    
                    <asp:GridView ID="grAllowanceUpload" runat="server" DataKeyNames="EmpId" AutoGenerateColumns="False"
                        EmptyDataText="No Record Found" Font-Size="9px" Width="100%">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                             <asp:BoundField DataField="SLNo" HeaderText="SLNo">
                                <ItemStyle CssClass="ItemStylecss"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="EmpID" HeaderText="EmpID">
                                <ItemStyle CssClass="ItemStylecss"></ItemStyle>
                            </asp:BoundField>   
                            <asp:BoundField DataField="RELIGION" HeaderText="Religion">
                                <ItemStyle CssClass="ItemStylecss"></ItemStyle>
                            </asp:BoundField>  
                            <asp:BoundField DataField="Festival" HeaderText="Festival">
                                <ItemStyle CssClass="ItemStylecss"></ItemStyle>
                            </asp:BoundField>  
                            <asp:BoundField DataField="Month" HeaderText="Month">
                                <ItemStyle CssClass="ItemStylecss"></ItemStyle>
                            </asp:BoundField>  
                            <asp:BoundField DataField="Year" HeaderText="Year">
                                <ItemStyle CssClass="ItemStylecss"></ItemStyle>
                            </asp:BoundField>  
                            <asp:BoundField DataField="BasicSalary" HeaderText="BasicSalary">
                                <ItemStyle CssClass="ItemStylecss"></ItemStyle>
                            </asp:BoundField>  
                            <asp:BoundField DataField="BonusAmount" HeaderText="BonusAmount">
                                <ItemStyle CssClass="ItemStylecss"></ItemStyle>
                            </asp:BoundField>    
                            <asp:BoundField DataField="FestiveDate" HeaderText="FestiveDate">
                                <ItemStyle CssClass="ItemStylecss"></ItemStyle>
                            </asp:BoundField>                                                      
                        </Columns>
                    </asp:GridView>
                </div>
                    <asp:Label ID="lblRecord" runat="server" Font-Bold="True" ForeColor="#006666"></asp:Label>
                </fieldset> 
            </fieldset>

        </div>
        <div class="DivCommand1" style="width: 100%">
            <div class="DivCommandL">
                <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" Text="Refresh"
                    Width="70px" OnClick="btnRefresh_Click" ToolTip="To clear all controls click on Refresh Button." />
            </div>
            <div class="DivCommandR">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="javascript:return HistorySaveConfirmation();"
                    Width="70px" OnClick="btnSave_Click" ToolTip="After providing all the necessary values click on Save button to store the information." />
            </div>
        </div>
    </div>      
</asp:Content>

