<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="HeadSetup.aspx.cs" Inherits="Payroll_HeadSetup_HeadSetup" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    From :
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    To :
    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click"></asp:Button>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</asp:Content>
