<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="MSBPageSetup.aspx.cs" Inherits="EIS_MSBPageSetup" %>

<asp:Content ID="MaiContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>
    <div class="setup">
        <div id='formhead1'>
            <div style="width: 96%; float: left;">
                File Upload Setup</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <!--Div for group-->
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <!--Div for Controls-->
        <asp:HiddenField ID="hfIsUpadate" runat="server" />
        <asp:HiddenField ID="hfID" runat="server" />
        <div class="setupInner">
            <fieldset>
                <table>
                   
                     <tr>
                               <td>
                                       <asp:FileUpload ID="fuFileUploader" runat="server" />
                               </td>
                               <td>&nbsp;</td>
                               <td>
                                       <asp:Button ID="btnUploadFiles" runat="server" Text="Upload Me!"
                                       OnClick="btnUploadMe_Click" />
                               </td>
                       </tr>
                       <tr>
                       <td colspan="3">
                             <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                       </td>
                       </tr>
                </table>
            </fieldset>

            
        </div>
        <div class="GridFormat1">
            <asp:GridView ID="gvUploadedFiles" runat="server" AutoGenerateColumns="false" DataKeyNames="FileId">
                <HeaderStyle Font-Bold="true" BackColor="#ff6600" BorderColor="#f5f5f5" ForeColor="White"
                    Height="30" />
                <Columns>
                    <asp:BoundField DataField="FileId" HeaderText="#" ControlStyle-Width="50" />
                    <asp:BoundField DataField="FileName" HeaderText="FileName" ControlStyle-Width="250" />
                    <asp:BoundField DataField="FileSize" HeaderText="FileSize" ControlStyle-Width="250" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDownloadMe" runat="server" Text="Download Me!" OnClick="lnkDownloadMe_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <%--<asp:GridView ID="grDesigation" runat="server" DataKeyNames="ReligionId,IsActive"
                AutoGenerateColumns="False" EmptyDataText="No Record Found" 
                Font-Size="9px" OnRowCommand="grDesigation_RowCommand"
                Width="99%" onselectedindexchanged="grDesigation_SelectedIndexChanged">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                </SelectedRowStyle>
                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                <Columns>
                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                    </asp:ButtonField>

                    <asp:BoundField DataField="ReligionName" HeaderText="Religion Name">
                        <ItemStyle CssClass="ItemStylecss" Width="50%"></ItemStyle>
                    </asp:BoundField>

                     <asp:BoundField DataField="NumberOfbasic" HeaderText="Number Of Basic">
                        <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                    </asp:BoundField>

                     <asp:BoundField DataField="percentage" HeaderText="Percentage">
                        <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="IsActive" HeaderText="Is Active">
                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>--%>
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


