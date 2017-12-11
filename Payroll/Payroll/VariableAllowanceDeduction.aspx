<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="VariableAllowanceDeduction.aspx.cs" Inherits="Payroll_Payroll_VariableAllowanceDeduction"
    Title="Variable Allowances or Deductions" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js"></script>
    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js"></script>
    <div id="PayrollConfigForm2" style="width: 90%;">
        <div id="formhead1">
            <div style="width: 98%; float: left;">
                Variable Allowances or Deductions</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <%--<noscript>
          Sorry!! your browser is too old or does not support javascript.
        </noscript>--%>
        <div id="PayrollConfigInner2">
            <div style="width: 100%">
                <div style="width: 49.5%; float: left;">
                    <fieldset>
                        <table>
                            <tr>
                                <td class="textlevelshort">
                                    Employee :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlEmployee" runat="server" Width="285px" CssClass="textlevelleft">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" Width="40px" CausesValidation="false"
                                        OnClick="btnAdd_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td class="textlevelshort">
                                </td>
                                <td>
                                    <div style="border: solid 1px Gray;">
                                        <asp:GridView ID="grEmpList" runat="server" DataKeyNames="EMPID" AutoGenerateColumns="False"
                                            EmptyDataText="No Record Found" Font-Size="9px" Width="99%" OnRowCommand="grEmpList_RowCommand">
                                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                            </SelectedRowStyle>
                                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                            <Columns>
                                                <asp:ButtonField CommandName="DoubleClick" HeaderText="Remove" Text="Remove">
                                                    <ItemStyle Width="10%" CssClass="ItemStylecss" />
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="" HeaderText="SL">
                                                    <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FULLNAME" HeaderText="Employee">
                                                    <ItemStyle CssClass="ItemStylecss" Width="80%"></ItemStyle>
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <asp:HiddenField ID="hfID" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="textlevelshort">
                                    Remarks :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRemarks" runat="server" Width="280px"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
                <div style="width: 50%; float: left;">
                    <fieldset>
                        <table width="99%">
                            <tr>
                                <td class="textlevel">
                                    Salary Item :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSalHead" runat="server" Width="140px" AutoPostBack="True"
                                        CssClass="textlevelleft" OnSelectedIndexChanged="ddlSalHead_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="textlevel">
                                    Monthly Amount :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPayAmt" runat="server" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="textlevel" valign="top">
                                    Effective From :
                                </td>
                                <td valign="top">
                                    <asp:TextBox ID="txtFrom" runat="server" Width="70px"></asp:TextBox>
                                    <a href="javascript:NewCal('<%= txtFrom.ClientID %>','ddmmyyyy')">
                                        <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                            height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>
                                </td>
                                <td class="textlevel" valign="top">
                                    To :
                                </td>
                                <td valign="top">
                                    <asp:TextBox ID="txtTo" runat="server" Width="70px"></asp:TextBox>
                                    <a href="javascript:NewCal('<%= txtTo.ClientID %>','ddmmyyyy')">
                                        <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                            height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" valign="top" align="center" style="border-top: solid 1px gray;">
                                    <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <div style="border: solid 1px Gray;">
                                        <asp:GridView ID="grSchedule" runat="server" DataKeyNames="VID,VYEAR,VDAYS,VMONTH"
                                            AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" Width="99.99%"
                                            OnRowCommand="grEmpList_RowCommand">
                                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                            </SelectedRowStyle>
                                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                            <Columns>
                                                <asp:BoundField DataField="" HeaderText="SL">
                                                    <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="VMONTH" HeaderText="Month">
                                                    <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="VYEAR" HeaderText="Year">
                                                    <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="VDAYS" HeaderText="Days">
                                                    <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Pay Amount" HeaderStyle-HorizontalAlign="Right">
                                                    <ItemStyle Width="20%" HorizontalAlign="right" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPayAmnt" Text='<%# Convert.ToString(Eval("PAYAMNT")) %>' runat="server"
                                                            CssClass="TextBoxAmt100"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="fltbPayAmnt" runat="server" TargetControlID="txtPayAmnt"
                                                            FilterType="Custom,Numbers" ValidChars=".,-">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </div>
            <div id="DivCommand1" style="padding-top: 3px;">
                <div style="text-align: left; float: left">
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px" CausesValidation="False"
                        OnClick="btnRefresh_Click" />
                    <asp:Button ID="btnSynchronize" runat="server" Text="Synchronize" Width="100px" ForeColor="Blue"
                        UseSubmitBehavior="False" CausesValidation="False" OnClick="btnSynchronize_Click" />
                </div>
                <div style="text-align: right;">
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" UseSubmitBehavior="False"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                        OnClick="btnDelete_Click" />
                </div>
            </div>
            <fieldset style="margin-top: 10px;">
                <legend>Variable Allowances/ Deductions List</legend>
                <div>
                    <table>
                        <tr>
                            <td class="textlevel">
                                Select Record
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSelect" runat="server" Width="100px" AutoPostBack="True"
                                    CssClass="textlevelleft" OnSelectedIndexChanged="ddlSelect_SelectedIndexChanged">
                                    <asp:ListItem Value="Y">Active</asp:ListItem>
                                    <asp:ListItem Value="N">Inactive</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="textlevel">Emp Id</td>
                            <td><asp:DropDownList ID="ddlEmpList" runat="server" Width="285px" CssClass="textlevelleft">
                                    </asp:DropDownList></td>
                            <td class="textlevel">Month</td>
                            <td><asp:DropDownList ID="ddlMonth" runat="server" Width="130px" CssClass="textlevelleft">
                            </asp:DropDownList></td>
                            <td class="textlevel">Year</td>
                            <td><asp:DropDownList ID="ddlYear" runat="server" Width="130px" CssClass="textlevelleft">
                            </asp:DropDownList></td>
                            <td><asp:Button ID="btnShow" runat="server" Text="Generate" Width="130px" 
                                OnClick="btnShow_Click" /></td>
                        </tr>
                    </table>
                </div>
                <asp:GridView ID="grVariableList" runat="server" DataKeyNames="VID,SHEADID,REMARKS"
                    AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" Width="100%"
                    OnRowCommand="grVariableList_RowCommand">
                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                    </SelectedRowStyle>
                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                    <Columns>
                        <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                            <ItemStyle Width="4%" CssClass="ItemStylecss" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="" HeaderText="SL">
                            <ItemStyle CssClass="ItemStylecss" Width="4%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="EMPID" HeaderText="Emp.ID">
                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FullName" HeaderText="Employee Name">
                            <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="JobTitleName" HeaderText="Job Title">
                            <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="SalLocName" HeaderText="Location Category">
                            <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="HEADNAME" HeaderText="Salary Item">
                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PAYAMNT" HeaderText="Monthly Amount">
                            <ItemStyle CssClass="ItemStylecss" Width="8%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ValidFrom" HeaderText="Effective From">
                            <ItemStyle CssClass="ItemStylecss" Width="8%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ValidTo" HeaderText="Effective To">
                            <ItemStyle CssClass="ItemStylecss" Width="8%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ISACTIVE" HeaderText="Is Active">
                            <ItemStyle CssClass="ItemStylecss" Width="8%"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </fieldset>
        </div>
    </div>
</asp:Content>
