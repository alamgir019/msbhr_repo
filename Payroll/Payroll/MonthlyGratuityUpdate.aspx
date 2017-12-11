<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="MonthlyGratuityUpdate.aspx.cs" Inherits="Payroll_Loan_MonthlyPFUpdate"
    Title="Monthly PF Update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js">
    </script>

    <div id="PayrollConfigForm">
        <div id="formhead1">
            Monthly Gratuity Update
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="PayrollConfigInner">
            <fieldset style="margin-top: 10px;">
                <legend>Monthly Gratuity Update</legend>
                <table>
                    <tr>
                        <td class="textlevel">
                            Gratuity Month :</td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" runat="server" Width="100px" CssClass="textlevelleft">
                            </asp:DropDownList></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Gratuity Year :</td>
                        <td>
                            <asp:DropDownList ID="ddlYear" runat="server" Width="100px" CssClass="textlevelleft">
                            </asp:DropDownList></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Fiscal Year :</td>
                        <td>
                            <asp:DropDownList ID="ddlFiscalYear" runat="server" Width="130px" CssClass="textlevelleft">
                            </asp:DropDownList></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Select Group :</td>
                        <td>
                            <asp:DropDownList ID="ddlGroup" runat="server" Width="130px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <br />
                <div style="width: 100%">
                    <div style="width: 48%; float: left;">
                        <asp:Button ID="btnUpdatePFLedger" runat="server" Text="Update Gratuity Ledger" OnClick="btnUpdateGratuityLedger_Click" />
                    </div>
                    <div style="width: 48%; float: right; text-align: right;">
                        <asp:Button ID="btnDelete" runat="server" Text="Delete Gratuity Ledger" ForeColor="red"
                            OnClientClick="javascript:return DeleteConfirmation();" OnClick="btnDelete_Click" />
                    </div>
                </div>
                <br />
            </fieldset>
        </div>
    </div>
</asp:Content>
