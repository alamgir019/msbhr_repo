<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="COLAAdjustment.aspx.cs" Inherits="EIS_COLAAdjustment" Title="COLA Adjustment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js"></script>
    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js"></script>
    <div class="empTrainForm">
        <div id="formhead1">
            <div style="width: 97%; float: left;">
                COLA Adjustment</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="setupInner" style="margin-left: 10px; margin-right: 10px;">
            <fieldset style="margin-bottom: 10px;">
                <legend>COLA Adjustsment</legend>
                <table>
                    <tr>
                        <td class="textlevel">
                            Employee Type:</td>
                        <td>
                            <asp:DropDownList ID="ddlEmpType" runat="server" Width="150px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                            COLA Percentage :</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtCOLAPercent" runat="server" Width="80px" CssClass="TextBoxAmt60"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            &nbsp;Effective Date :</td>
                        <td>
                            <asp:TextBox ID="txtEffDate" runat="server" MaxLength="10" Width="80px"></asp:TextBox>
                             <a href="javascript:NewCal('<%= txtEffDate.ClientID %>','ddmmyyyy')">
                                <img style="border: 0px;" height="16" alt="Pick a date" src="../../images/cal.gif"
                                    width="16" /></a>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEffDate"
                                CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator 
                                ID="ReqValTitle" runat="server" ControlToValidate="txtEffDate"
                                ErrorMessage="*">*</asp:RequiredFieldValidator></td>
                        <td class="textlevel">
                            &nbsp;</td>
                        <td style="width: 202px">
                            
                            <asp:Button ID="btnGenerate" runat="server" CausesValidation="False" Text="Generate"
                                Width="150px" OnClick="btnGenerate_Click" />
                            
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom,Numbers"
                    TargetControlID="txtCOLAPercent">
                </cc1:FilteredTextBoxExtender>
            </fieldset>
            <fieldset>
                <legend>Employee List</legend>
                <div class="officeSetupGrid450">
                    <asp:GridView ID="grEmpList" runat="server" AutoGenerateColumns="False" DataKeyNames="SalPakId,ConfirmationDate"
                        EmptyDataText="No Record Found" Font-Size="9px" ToolTip="Transition List">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                        <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingRowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:BoundField DataField="" HeaderText="SL No">
                                <ItemStyle CssClass="ItemStylecssRight" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EmpId" HeaderText="Emp ID">
                                <ItemStyle CssClass="ItemStylecss" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FullName" HeaderText="Name">
                                <ItemStyle CssClass="ItemStylecss" Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BasicSalary" HeaderText="Basic Salary">
                                <ItemStyle CssClass="ItemStylecssRight" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="" HeaderText="New Basic Salary">
                                <ItemStyle CssClass="ItemStylecssRight" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="" HeaderText="Allowance">
                                <ItemStyle CssClass="ItemStylecssRight" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="" HeaderText="PF">
                                <ItemStyle CssClass="ItemStylecssRight" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="" HeaderText="Percentage">
                                <ItemStyle CssClass="ItemStylecssRight" Width="10%" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
                <table>
                    <tr>
                        <td class="textlevel">
                            Total Record :
                        </td>
                        <td>
                            <asp:Label ID="lblRecordCount" runat="server" Font-Bold="True" Font-Size="Smaller"
                                ForeColor="Blue"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div class="DivCommand1">
            <div class="DivCommandL">
                <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" Text="Refresh"
                    Width="70px" />
            </div>
            <div class="DivCommandR">
                <asp:Button ID="btnAdjust" runat="server" Text="Adjust Salary Head" Width="140px"
                    OnClick="btnAdjust_Click" />&nbsp;</div>
        </div>
    </div>
</asp:Content>
