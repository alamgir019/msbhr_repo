<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="PayrollPolicySetup.aspx.cs" Inherits="PayrollPolicySetup" Title="Payroll General Policy" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js"></script>
    <div id="PayrollConfigForm2">
        <div id="formhead1">
            Payroll General Policy</div>
        <!--Div for group-->
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div id="PayrollConfigInner2">
            <fieldset>
                <legend>Payroll Validity Period</legend>
                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
                <table>
                    <tr>
                        <td class="textlevel">
                            Fiscal Year :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFiscalYear" runat="server" Width="130px" CssClass="textlevelleft"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlFiscalYear_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Payroll Valid From :
                        </td>
                        <td>
                            <asp:TextBox ID="txtValidFrom" runat="server" Width="80px"></asp:TextBox>
                            <a href="javascript:NewCal('<%= txtValidFrom.ClientID %>','ddmmyyyy')">
                                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                    height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a>
                        </td>
                        <td class="textlevel">
                            To :
                        </td>
                        <td>
                            <asp:TextBox ID="txtValidTo" runat="server" Width="80px"></asp:TextBox>
                            <a href="javascript:NewCal('<%= txtValidTo.ClientID %>','ddmmyyyy')">
                                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                    height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a>
                        </td>
                    </tr>
                </table>
                <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
                <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px" Visible="false"
                                CausesValidation="False" />
                            <asp:DropDownList ID="ddlPFLoanDeduct" runat="server" CausesValidation="True" Width="117px"
                                Visible="False" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" UseSubmitBehavior="False"
                                OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset style="margin-top: 10px;">
                <legend>Benefits Policies</legend>
                <table>
                    <tr>
                        <td style="font: tahoma; font-size: 12px; color: White; background-color: #3366CC;
                            font-weight: bold;">
                            Head Type
                        </td>
                        <td style="font: tahoma; font-size: 12px; color: White; background-color: #3366CC;
                            font-weight: bold;">
                            Emp. Type
                        </td>
                        <td style="font: tahoma; font-size: 12px; color: White; background-color: #3366CC;
                            font-weight: bold;">
                            Is Percent
                        </td>
                        <td style="font: tahoma; font-size: 12px; color: White; background-color: #3366CC;
                            font-weight: bold;">
                            Value
                        </td>
                        <td style="font: tahoma; font-size: 12px; color: White; background-color: #3366CC;
                            font-weight: bold;">
                            Percentage Head
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlBenefitHead" runat="server" CssClass="textlevelleft" Width="160px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEmpType" runat="server" Width="160px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsPercent" runat="server" Width="30px" 
                                oncheckedchanged="chkIsPercent_CheckedChanged" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtValue" runat="server" Width="60px" CssClass="TextBoxAmt60"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPercentHead" runat="server" Width="160px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnAdd" runat="server" Text="Save" OnClick="btnAdd_Click" />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:GridView ID="grBPList" runat="server" DataKeyNames="PID,SHEADID,EMPTYPEID,VALUE,ISPERCENT,PERCENTOF"
                                AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" Width="100%"
                                OnRowCommand="grBPList_RowCommand">
                                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                </SelectedRowStyle>
                                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                <Columns>
                                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                        <ItemStyle Width="8%" CssClass="ItemStylecss" />
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="PID" HeaderText="SL">
                                        <ItemStyle CssClass="ItemStylecss" Width="6%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="HEADNAME" HeaderText="Head Type">
                                        <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TYPENAME" HeaderText="Emp. Type">
                                        <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ISPERCENT" HeaderText="Is Percent" HeaderStyle-HorizontalAlign="center">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="10%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="VALUE" HeaderText="Value" HeaderStyle-HorizontalAlign="center">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="10%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PERCENTHEADNAME" HeaderText="Percent Head">
                                        <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:ButtonField CommandName="RowDeleting" HeaderText="Remove" Text="Remove">
                                        <ItemStyle Width="8%" CssClass="ItemStylecss" />
                                    </asp:ButtonField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfBPID" runat="server" />
            </fieldset>
            <fieldset style="margin-top: 20px;">
                <legend>Monthly Payroll Cycle Policy </legend>
                <table>
                    <tr>
                        <td style="font: tahoma; font-size: 12px; color: White; background-color: #3366CC;
                            font-weight: bold;" rowspan="2">
                            Policy Title
                        </td>
                        <td colspan="2" style="font: tahoma; font-size: 12px; color: White; background-color: #3366CC;
                            font-weight: bold;">
                            Monthly Payroll Cycle
                        </td>
                        <td colspan="2" style="font: tahoma; font-size: 12px; color: White; background-color: #3366CC;
                            font-weight: bold;">
                            Monthly Attendance Cycle
                        </td>
                        <td rowspan="3" valign="bottom">
                            <asp:Button ID="btnMPCAdd" runat="server" Text="Save" Width="60px" OnClick="btnMPCAdd_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="font: tahoma; font-size: 11px; background-color: Gray; font-weight: bold;
                            text-align: center;">
                            Start Day
                        </td>
                        <td style="font: tahoma; font-size: 11px; background-color: Gray; font-weight: bold;
                            text-align: center;">
                            End Day
                        </td>
                        <td style="font: tahoma; font-size: 11px; background-color: Gray; font-weight: bold;
                            text-align: center;">
                            Start Day
                        </td>
                        <td style="font: tahoma; font-size: 11px; background-color: Gray; font-weight: bold;
                            text-align: center;">
                            End Day
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtMPCTitle" runat="server" CssClass="textlevelleft" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPCSDay" runat="server" CssClass="textlevelleft" Width="75px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPCEDay" runat="server" CssClass="textlevelleft" Width="75px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlACSDay" runat="server" CssClass="textlevelleft" Width="75px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlACEDay" runat="server" CssClass="textlevelleft" Width="75px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:GridView ID="grMPCList" runat="server" DataKeyNames="MPCID" AutoGenerateColumns="False"
                                EmptyDataText="No Record Found" Font-Size="9px" Width="99.99%" OnRowCommand="grMPCList_RowCommand">
                                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                </SelectedRowStyle>
                                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                <Columns>
                                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                        <ItemStyle Width="8%" CssClass="ItemStylecss" />
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="MPCID" HeaderText="MPCID">
                                        <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MPCTitle" HeaderText="Policy Title">
                                        <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PSTARTDAY" HeaderText="Payroll Start Day " HeaderStyle-HorizontalAlign="center">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="15%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PENDDAY" HeaderText="Payroll End Day " HeaderStyle-HorizontalAlign="center">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="15%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ASTARTDAY" HeaderText="Attendance Start Day " HeaderStyle-HorizontalAlign="center">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="15%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AENDDAY" HeaderText="Attendance End Day " HeaderStyle-HorizontalAlign="center">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="15%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:ButtonField CommandName="RowDeleting" HeaderText="Remove" Text="Remove">
                                        <ItemStyle Width="7%" CssClass="ItemStylecss" />
                                    </asp:ButtonField>
                                </Columns>
                            </asp:GridView>
                            <asp:HiddenField ID="hfMPCIsUpdate" runat="server" />
                            <asp:HiddenField ID="hfMPCID" runat="server" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>
</asp:Content>
