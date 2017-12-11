<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="GradeSetup.aspx.cs" Inherits="GradeSetup" Title="Grade" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>
    <div class="setup">
        <div id='formhead1'>
            <div style="width: 96%; float: left;">
                Grade Setup</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <!--Div for group-->
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="setupInner">
            <fieldset>
                <!--Div for Controls-->
                <asp:HiddenField ID="hfIsUpadate" runat="server" />
                <asp:HiddenField ID="hfID" runat="server" />
                <table>
                    <tr>
                        <td class="textlevel">
                            Grade Name :
                        </td>
                        <td>
                            <asp:TextBox ID="txtGrade" runat="server" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtGrade"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Basic Min :
                        </td>
                        <td>
                            <asp:TextBox ID="txtBasicMin" runat="server" ToolTip="Input this grade level’s Basic minimum salary amount."></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Basic Max :
                        </td>
                        <td>
                            <asp:TextBox ID="txtBasicMax" runat="server" ToolTip="Input this grade level’s Basic maximum salary amount."></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            &nbsp;
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsOTEntitle" runat="server" CssClass="textlevelleft" Text="Is OT Entitlement"
                                Width="127px" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsActive" runat="server" CssClass="textlevelleft" Width="162px"
                                Text="Make Inactive" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <%--<cc1:filteredtextboxextender ID="FTB1" runat="server" FilterType="Custom,Numbers"
            TargetControlID="txtBasicMin" ValidChars="0123456789.">
        </cc1:filteredtextboxextender>
        <cc1:filteredtextboxextender ID="FTB2" runat="server" FilterType="Custom,Numbers"
            TargetControlID="txtBasicMax" ValidChars="0123456789.">
        </cc1:filteredtextboxextender>--%>
        <div style="margin-left: 10px; margin-right: 10px; margin-top: 10px;">
            <cc1:tabcontainer id="TabContainer1" runat="server" activetabindex="1" width="100%"
                height="250px">
                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel1" Width="100%">
                    <HeaderTemplate>
                        Designation List
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div style="width: 100%; height: 240px; overflow: scroll;">
                            <asp:GridView ID="grDesig" runat="server" DataKeyNames="DesigId" AutoGenerateColumns="False"
                                EmptyDataText="No Record Found" Font-Size="9px" Width="99%">
                                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                </SelectedRowStyle>
                                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemStyle CssClass="ItemStylecss" Width="5%" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkBox" runat="Server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DesigName" HeaderText="Designation">
                                        <ItemStyle CssClass="ItemStylecss" Width="95%"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Width="100%">
                    <HeaderTemplate>
                        Grade List
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div style="width: 100%; height: 240px; overflow: scroll;">
             <asp:GridView ID="grGrade" runat="server" DataKeyNames="GradeID,IsActive" AutoGenerateColumns="False"
                EmptyDataText="No Record Found" Font-Size="9px" OnRowCommand="grGrade_RowCommand"
                Width="99%">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                </SelectedRowStyle>
                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                <Columns>
                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="GradeName" HeaderText="Grade Name">
                        <ItemStyle CssClass="ItemStylecss" Width="40%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="IsOTEntitle" HeaderText="Is OT Entitlement">
                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="IsActive" HeaderText="Is Active">
                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="BasicMin" HeaderText="Basic Min">
                        <ItemStyle CssClass="ItemStylecss"  Width="10%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="BasicMax" HeaderText="Basic Max">
                        <ItemStyle CssClass="ItemStylecss"  Width="10%"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>            
        </div>
         </ContentTemplate>
                </cc1:TabPanel>
            </cc1:tabcontainer>
            <div class="DivCommand1">
                <div class="DivCommandL">
                    <asp:Button ID="btnClear" runat="server" Text="Refresh" Width="70px" OnClick="btnClear_Click"
                        UseSubmitBehavior="False" CausesValidation="false" />
                </div>
                <div class="DivCommandR">
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                        OnClick="btnDelete_Click" />
                </div>
            </div>
        </div>
</asp:Content>
