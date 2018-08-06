<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="customfield.aspx.cs" Inherits="KPI_customfield" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>
    <div class="empTrainForm">
    <asp:View ID="View1" runat="server">
        <asp:Wizard ID="Wizard1" runat="server">
            <WizardSteps>
                <asp:WizardStep ID="WizardStep1" runat="server" Title="Step 1"></asp:WizardStep>
                <asp:WizardStep ID="WizardStep2" runat="server" Title="Step 2"></asp:WizardStep>
            </WizardSteps>
        </asp:Wizard>
    </asp:View>       
    </div>
</asp:Content>
