<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="FiscalYearSetup.aspx.cs" Inherits="Payroll_FiscalYearSetup" Title="Fiscal Year Setup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js">
        //Date Time Picker script
    </script>
    <div id="setup">
        <div id="formhead1">
            <div style="width: 96%; float: left;">
                Fiscal Year Setup</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="setupInner">
            <fieldset>
                <legend>Fiscal Year Setup</legend>
                <table>
                    <tr>
                        <td class="textlevel">
                            <asp:Label ID="lblName" runat="server"></asp:Label>
                            Fiscal Year Title :
                        </td>
                        <td>
                            <asp:TextBox ID="txtTitle" runat="server" Width="150px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="ReqValTitle" runat="server" ControlToValidate="txtTitle"
                                ErrorMessage="*">*</asp:RequiredFieldValidator>
                        </td>
                        <td class="textlevel">
                            &nbsp;<asp:CheckBox ID="chkIsTax" runat="server" Text="Is Tax" CssClass="textlevelleft" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsPF" runat="server" Text="Is PF" CssClass="textlevel" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsMed" runat="server" Text="Is Med" CssClass="textlevel" />
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Start Date :
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartDate" runat="server" Width="80px"></asp:TextBox>
                            <a href="javascript:NewCal('<%= txtStartDate.ClientID %>','ddmmyyyy')">
                                <img id="img1" alt="Pick a date" height="16" src="../images/cal.gif" style="border-right: 0px;
                                    border-top: 0px; border-left: 0px; border-bottom: 0px" /></a>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtStartDate"
                                CssClass="validator" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td class="textlevel">
                            End Date :
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndDate" runat="server" Width="80px"></asp:TextBox>
                            <a href="javascript:NewCal('<%= txtEndDate.ClientID %>','ddmmyyyy')">
                                <img id="Img2" alt="Pick a date" height="16" src="../images/cal.gif" style="border-right: 0px;
                                    border-top: 0px; border-left: 0px; border-bottom: 0px" /></a>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEndDate"
                                CssClass="validator" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel" style="vertical-align: top;">
                            Description :
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtDescription" runat="server" Rows="2" TextMode="MultiLine" Width="96%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsClosed" runat="server" Text="Closed" CssClass="textlevelleft" />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkCurrFiscalYr" runat="server" Text="Current Fiscal Year" CssClass="textlevelleft" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfID" runat="server" />
                <asp:HiddenField ID="hfIsUpdate" runat="server" />
            </fieldset>
        </div>
        <div class="GridFormat1">
            <asp:GridView ID="grFiscalYear" runat="server" DataKeyNames="FiscalYrId,FiscalDesc"
                AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" Width="99%"
                OnRowCommand="grFiscalYear_RowCommand">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                </SelectedRowStyle>
                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                <Columns>
                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                        <ItemStyle Width="6%" CssClass="ItemStylecss" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="FiscalYrTitle" HeaderText="Fiscal Year Title">
                        <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="IsFYTax" HeaderText="Is FY Tax">
                        <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="IsFYPF" HeaderText="Is FY PF">
                        <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="IsFYMed" HeaderText="Is FY Med">
                        <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="StartDate" HeaderText="Start Date">
                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="EndDate" HeaderText="End Date">
                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="IsClosed" HeaderText="Closed">
                        <ItemStyle CssClass="ItemStylecssCenter" Width="7%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="IsCurrFiscalYr" HeaderText="Current">
                        <ItemStyle CssClass="ItemStylecssCenter" Width="7%"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>

        <div id="DivCommand1">
            <div class="DivCommandL">
                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px" CausesValidation="False"
                    OnClick="btnRefresh_Click" />
            </div>
            <div class="DivCommandR">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" UseSubmitBehavior="False"
                    OnClick="btnSave_Click" Style="height: 26px" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                    OnClick="btnDelete_Click" />
            </div>
        </div>
    </div>
</asp:Content>
