﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="MSBPageView.aspx.cs" Inherits="EIS_MSBPageView" %>

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
                <h3>
                    File List</h3>
                <hr />
                <asp:TreeView ID="TreeView1" runat="server" ImageSet="XPFileExplorer" NodeIndent="15" 
                                     AutoGenerateDataBindings="False" 
                    onselectednodechanged="TreeView1_SelectedNodeChanged"  >
                    <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                    <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                        NodeSpacing="0px" VerticalPadding="2px"></NodeStyle>
                    <ParentNodeStyle Font-Bold="False" />
                    <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                        VerticalPadding="0px" />
                </asp:TreeView>
            </fieldset>

            
        </div>
        

    </div>
</asp:Content>




