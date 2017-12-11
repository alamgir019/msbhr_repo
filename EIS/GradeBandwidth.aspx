<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="GradeBandwidth.aspx.cs" Inherits="EIS_GradeBandwidth" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>

    <div class="setup">
        <div id='formhead1'>
            Grade Bandwidth Setup
        </div>
        <!--Div for group-->
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="setupInner">
            <fieldset>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblSubGrade" runat="server" CssClass="textlevel" Text="Grade Name :"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlGrade" runat="server" Width="210px" CssClass="textlevelleft" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlGrade_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <!--Div for Controls-->
            <asp:HiddenField ID="hfIsUpadate" runat="server" />
            <asp:HiddenField ID="hfID" runat="server" />
        </div>
        <div class="GridFormat1">
            <asp:GridView ID="grSubGrade" runat="server" AutoGenerateColumns="False" DataKeyNames="SubGradeId"
                EmptyDataText="No Record Found" Font-Size="9px" Width="100%">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                <AlternatingRowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:BoundField DataField="SubGradeName" HeaderText="Sub Grade Name">
                        <ItemStyle CssClass="ItemStylecss" Width="85%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAmount" runat="server" Width="80px" Text='<%# Eval("Amount")%>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle CssClass="ItemStylecss" Width="15%" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="DivCommand1">
            <div class="DivCommandL">
                <asp:Button ID="btnClear" runat="server" Text="Refresh" Width="70px" OnClick="btnClear_Click"
                    UseSubmitBehavior="False" />
            </div>
            <div class="DivCommandR">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click" />
                <%--<asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                    OnClick="btnDelete_Click" />--%>
            </div>
        </div>
    </div>
</asp:Content>
