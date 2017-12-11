<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterBTMS.master"
    AutoEventWireup="true" CodeFile="GratuityPayment.aspx.cs" Inherits="Payroll_Payroll_GratuityPayment"
    Title="Gratuity Payment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js"></script>
    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js"></script>
    <script language="javascript" type="text/javascript">
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }
        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }
    </script>
    <div class="formStyle" style="width: 60%;">
        <div id="formhead1">
            <div style="width: 97%; float: left;">
                Gratuity Process</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Fuchsia"></asp:Label>
        </div>
        <div class="Div950">
            <div style="background-color: #EFF3FB;">
                <fieldset>
                    <table>
                        <tr>
                            <td class="textlevelleft">
                                Gratuity Process Date
                            </td>
                            <td>
                                <asp:TextBox ID="txtProcessDate" runat="server" Width="80px"></asp:TextBox>
                                <a href="javascript:NewCal('<%= txtProcessDate.ClientID %>','ddmmyyyy')">
                                    <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                        height="16" alt="Pick a date" src="../../images/cal.gif" width="16" />
                                </a>
                            </td>
                            <td class="textlevelleft">
                                Gratuity Process Month
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="textlevelleft" Width="95px">
                                </asp:DropDownList>
                            </td>
                            <td class="textlevelleft">
                                Gratuity Quarter
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlQuarter" runat="server" CssClass="textlevelleft" Width="100px">
                                    <asp:ListItem Value="1">First Quarter</asp:ListItem>
                                    <asp:ListItem Value="2">Second Quarter</asp:ListItem>
                                    <asp:ListItem Value="3">Third Quarter</asp:ListItem>
                                    <asp:ListItem Value="4">Forth Quarter</asp:ListItem>
                                </asp:DropDownList>
                            </td>                           
                        </tr>
                        <tr>
                            <td class="textlevelleft">
                                Fiscal Year
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFiscalYear" runat="server" CssClass="textlevelleft" Width="100px">
                                </asp:DropDownList>
                            </td>
                            <td class="textlevelleft">
                                Gratuity Process Year
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="textlevelleft" Width="85px">
                                </asp:DropDownList>
                            </td>
                            <td class="textlevelleft">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                                <asp:Button ID="btnProcess" runat="server" Text="Process" UseSubmitBehavior="False"
                                    Width="70px" OnClick="btnProcess_Click" />
                            </td>                           
                        </tr>
                    </table>
                </fieldset>
            </div>
            <div id="DivCommand1" style="padding-top: 3px;">
                <div style="text-align: left; float: left">
                </div>
                <div style="text-align: right;">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
