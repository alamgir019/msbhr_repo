<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="USDRateSetup.aspx.cs" Inherits="Payroll_USDRateSetup" Title="USD Rate Setup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js">
        //Date Time Picker script
    </script>

    <div class="officeSetup">
        <div id="formhead1">
            Usd Rate Setup</div>
        <!--Div for group-->
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="officeSetupInner">
            <table style="width: 400px">                    
                    <tr>
                        <td class="textlevel">
                            USD Rate :</td>
                        <td>
                            <asp:TextBox ID="txtUsdRate" Width="80px" runat="server" 
                                CssClass="TextBoxAmt60"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                    <td class="textlevel">
                        USD Rate Date :</td>
                    <td>
                        <asp:TextBox ID="txtRateDate" runat="server" Width="80px"></asp:TextBox>
                        <a href="javascript:NewCal('<%= txtRateDate.ClientID %>','ddmmyyyy')">
                            <img style="border: 0px;" height="16" alt="Pick a date" src="../images/cal.gif"
                                width="16" /></a>
                                        <asp:RequiredFieldValidator ID="ReqFieldVal" runat="server" 
                            ErrorMessage="*" ControlToValidate="txtRateDate"></asp:RequiredFieldValidator>
                                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                            ControlToValidate="txtRateDate" CssClass="validator" ErrorMessage="Invalid Date"
                            ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                
                    </td>
            </tr>                   
            </table>
            <asp:HiddenField ID="hfIsUpdate" runat="server" />
            <cc1:FilteredTextBoxExtender ID="FilteredTxt1" runat="server" FilterType="Custom,Numbers"
                TargetControlID="txtUsdRate" ValidChars="0123456789.">
            </cc1:FilteredTextBoxExtender>
             <fieldset>
                <legend>USD Rate List</legend>
                <div style="overflow: scroll; width: 100%; height: 250px">
                    <asp:GridView ID="grList" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                        AutoGenerateColumns="False" DataKeyNames="USDID" 
                        onrowcommand="grList_RowCommand">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                <ItemStyle Width="5%" CssClass="ItemStylecss" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="USDRate" HeaderText="USD Rate">
                                <ItemStyle CssClass="ItemStylecss" Width="35%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="USDDate" HeaderText="USD Rate Date">
                                <ItemStyle CssClass="ItemStylecss" Width="40%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </fieldset>
                <div id="DivCommand1" style="padding-top: 3px;">
                    <div style="text-align: left; float: left">
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px" 
                            CausesValidation="False" onclick="btnRefresh_Click" />
                    </div>
                    <div style="text-align: right;">
                        <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" UseSubmitBehavior="False"
                            OnClick="btnSave_Click" />
                    </div>
                </div>
        </div>
    </div>
</asp:Content>
