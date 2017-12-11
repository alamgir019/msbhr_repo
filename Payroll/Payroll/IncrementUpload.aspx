<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="IncrementUpload.aspx.cs" Inherits="Payroll_Payroll_IncrementUpload"
    Title="Increment Upload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js"></script>
    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js"></script>
    <div id="PayrollConfigForm2" style="width: 90%;">
        <div id="formhead1">
            <div style="width: 98%; float: left;">
                Increment Upload</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div style="margin-left: 10px; margin-right: 10px;">
            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="590px">
                <cc1:TabPanel runat="server" Height="570px" ID="TabPanel1" HeaderText="Increment Upload">
                    <ContentTemplate>
                        <fieldset>
                            <table>
                                <tr>
                                    <td class="textlevel">
                                        Month :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlMonth" runat="server" Width="100px" CssClass="textlevelleft">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="textlevel">
                                        Year :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlYear" runat="server" Width="100px" CssClass="textlevelleft">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="textlevel">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td colspan="2">
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <fieldset style="margin-top: 10px;">
                            <legend>Increment List</legend>
                            <div>
                                <asp:GridView ID="grPayroll" runat="server" Width="97%" EmptyDataText="No record found">
                                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                                    <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingRowStyle BackColor="#EFF3FB" />
                                </asp:GridView>
                            </div>
                            <div style="width: 100%; text-align: center;">
                                <asp:Label ID="lblRecord" runat="server" Font-Bold="True" ForeColor="#006666"></asp:Label>
                            </div>
                        </fieldset>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel runat="server" Height="570px" ID="TabPanel2" HeaderText="Increment Calculation">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td class="textlevel" >
                                    <asp:Label ID="Label2" runat="server" Text="Cost Center :"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlClinic" runat="server"></asp:DropDownList>
                                </td>
                                <td class="textlevel" >
                                    <asp:Label ID="Label1" runat="server" Text="COLA"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCOLA" Width="90%" runat="server"></asp:TextBox>
                                </td>
                                <td class="textlevel">
                                    <asp:Label ID="Label4" runat="server" Text="Group Performance"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtGrpPer" Width="90%" runat="server"></asp:TextBox>
                                </td>
                                <td class="textlevel">
                                    <asp:Label ID="Label3" runat="server" Text="Inv. Performance"></asp:Label>
                                </td> 
                                <td>
                                    <asp:TextBox ID="txtInvPer" Width="90%" runat="server"></asp:TextBox>
                                </td>
                                <td class="textlevel">
                                    <asp:Label ID="Label6" runat="server" Text="Action Date"></asp:Label>
                                </td>
                                 <td>
                                    <asp:TextBox ID="txtActionDate" runat="server"></asp:TextBox>
                                    <a href="javascript:NewCal('<%= txtActionDate.ClientID %>','ddmmyyyy')">
                                    <img alt="Pick a date" height="16" src="../../images/cal.gif" style="border: 0px;" width="16" /></a>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                        ControlToValidate="txtActionDate" CssClass="validator" ErrorMessage="Invalid" 
                                        ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                                </td>
                                <td>
                                    <asp:Button runat="server" Text="Generate" ID="btnGenerate" OnClick="btnGenerate_Click"></asp:Button>
                                </td>
                            </tr>
                        </table>
                        <fieldset style="margin-top: 10px;">
                            <legend>Increment List</legend>
                            <div>
                                <asp:GridView ID="grIncrementList" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                        AutoGenerateColumns="False" DataKeyNames="EmpTypeId">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>                                
                                    <Columns>
                                    <asp:BoundField DataField="EmpId" HeaderText="Emp ID">
                                        <itemstyle cssclass="ItemStylecss" width="5%"></itemstyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FullName" HeaderText="Name">
                                        <itemstyle cssclass="ItemStylecss" width="11%"></itemstyle>
                                    </asp:BoundField>
                                     <asp:BoundField DataField="DesigName" HeaderText="Designation">
                                        <itemstyle cssclass="ItemStylecss" width="11%"></itemstyle>
                                    </asp:BoundField>
                                     <asp:BoundField DataField="ClinicName" HeaderText="Clinic">
                                        <itemstyle cssclass="ItemStylecss" width="11%"></itemstyle>
                                    </asp:BoundField>
                                     <asp:BoundField DataField="BasicSalary" HeaderText="Basic Salary">
                                        <itemstyle cssclass="ItemStylecssRight" width="11%"></itemstyle>
                                    </asp:BoundField>
                                     <asp:BoundField DataField="GrossSalary" HeaderText="Gross Salary">
                                        <itemstyle cssclass="ItemStylecssRight" width="11%"></itemstyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="COLA" HeaderText="COLA">
                                        <itemstyle cssclass="ItemStylecssRight" width="11%"></itemstyle>
                                    </asp:BoundField>
                                     <asp:BoundField DataField="GrpPer" HeaderText="Group Per.">
                                        <itemstyle cssclass="ItemStylecssRight" width="11%"></itemstyle>
                                    </asp:BoundField>
                                     <asp:BoundField DataField="InvPer" HeaderText="Inv. Per.">
                                        <itemstyle cssclass="ItemStylecssRight" width="11%"></itemstyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NewBasicSalary" HeaderText="NewBasicSalary">
                                        <itemstyle cssclass="ItemStylecssRight" width="11%"></itemstyle>
                                    </asp:BoundField>
                                     <asp:BoundField DataField="NewGrossSalary" HeaderText="NewGrossSalary">
                                        <itemstyle cssclass="ItemStylecssRight" width="11%"></itemstyle>
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Housing" HeaderText="Housing">
                                        <itemstyle cssclass="ItemStylecssRight" width="11%"></itemstyle>
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Medical" HeaderText="Medical">
                                        <itemstyle cssclass="ItemStylecssRight" width="11%"></itemstyle>
                                    </asp:BoundField>
                                     <asp:BoundField DataField="PF" HeaderText="PF">
                                        <itemstyle cssclass="ItemStylecssRight" width="11%"></itemstyle>
                                    </asp:BoundField>
                                    </Columns> 
                                </asp:GridView>
                            </div>
                            <div style="width: 100%; text-align: center;">
                                <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="#006666"></asp:Label>
                            </div>
                        </fieldset>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
        </div>
        <div id="DivCommand1" style="margin-left: 10px; margin-right: 10px;">
            <div style="text-align: left; float: left">
                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px" CausesValidation="False"
                    OnClick="btnRefresh_Click" />
            </div>
            <div style="text-align: right; margin-right: 20px;">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" UseSubmitBehavior="False"
                    OnClick="btnSave_Click" />
            </div>
        </div>
    </div>
</asp:Content>
