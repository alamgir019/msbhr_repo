<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="LeavePackage.aspx.cs" Inherits="Attendance_LeavePackage" Title="Leave Package" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>

    <div id="leavePackageForm">
        <div id="formhead1">
            <div style="width: 96%; float: left;">
                Leave Package</div>
        <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="leavePackageFormInner">
            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="390px">
                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Height="370px">
                    <ContentTemplate>
                        <div class="Div650">
                            <table>
                                <tr>
                                    <td style="width: 3px">
                                        <asp:Label ID="Label2" runat="server" CssClass="textlevel" Text="Package Title :"></asp:Label></td>
                                    <td style="width: 3px">
                                        <asp:TextBox ID="txtLeavePakName" runat="server" Width="309px"></asp:TextBox></td>
                                    <td style="width: 3px">
                                        <asp:RequiredFieldValidator ID="ReqVald" runat="server" ControlToValidate="txtLeavePakName"
                                            ErrorMessage="*">*</asp:RequiredFieldValidator></td>
                                    <td style="width: 5px">
                                        <asp:CheckBox ID="chkInActive" runat="server" Text="Mark Inactive" Width="162px"
                                            CssClass="textlevelleft" /></td>
                                    <td style="width: 3px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 3px">
                                        <asp:Label ID="Label1" runat="server" CssClass="textlevel" Text="Description :"></asp:Label></td>
                                    <td style="width: 3px">
                                        <asp:TextBox ID="txtDescription" runat="server" Width="309px"></asp:TextBox></td>
                                    <td style="width: 3px">
                                    </td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="width: 3px">
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td style="width: 120px">
                                        <asp:Label ID="Label4" runat="server" CssClass="textlevel" Text="Employee Type :"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlEmploymentType" runat="server" Width="210px" CssClass="textlevelleft">
                                            <asp:ListItem Value="0">Nil</asp:ListItem>
                                            <asp:ListItem Value="1">P1</asp:ListItem>
                                            <asp:ListItem Value="2">P2</asp:ListItem>
                                            <asp:ListItem Value="3">P3</asp:ListItem>
                                            <asp:ListItem Value="4">P4</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="ddlEmploymentType"
                                            ErrorMessage="*" Operator="NotEqual" ValueToCompare="-1"></asp:CompareValidator></td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 120px">
                                        <asp:Label ID="Label5" runat="server" CssClass="textlevel" Text="Month From :"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlMonthFrom" runat="server" Width="100px" CssClass="textlevelleft">
                                            <asp:ListItem>Month</asp:ListItem>
                                            <asp:ListItem Value="1">January</asp:ListItem>
                                            <asp:ListItem Value="2">February</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 120px; height: 17px">
                                        <asp:Label ID="Label6" runat="server" CssClass="textlevel" Text="Month To :"></asp:Label></td>
                                    <td style="height: 17px">
                                        <asp:DropDownList ID="ddlMonthTo" runat="server" Width="100px" CssClass="textlevelleft">
                                            <asp:ListItem>Month</asp:ListItem>
                                            <asp:ListItem Value="1">January</asp:ListItem>
                                            <asp:ListItem Value="2">February</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>
                                        </asp:DropDownList>&nbsp;
                                        <asp:CheckBox ID="chkIsNextYear" runat="server" Text="Is Next Year" CssClass="textlevelleft" /></td>
                                    <td style="height: 17px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 120px">
                                        <asp:Label ID="Label3" runat="server" CssClass="textlevel" Width="120px"></asp:Label></td>
                                    <td>
                                        <asp:CheckBox ID="chkLeavCalcOnJoining" runat="server" Text="Leave Calculation is Applicable on Joining Date"
                                            Width="379px" CssClass="textlevelleft" /></td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 3px">
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsOffdayCounted" runat="server" Text="Weekend And Holiday Between leave Duration will be encounted as Leave"
                                            Width="548px" CssClass="textlevelleft" Visible="False" /></td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 3px">
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsDefault" runat="server" Text="Is Default" Width="548px" CssClass="textlevelleft" /></td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <asp:HiddenField ID="hfIsUpdate" runat="server" />
                            <asp:HiddenField ID="hfID" runat="server" />
                        </div>
                        <div id="leavePackageList">
                            <asp:GridView ID="grLeaveList" runat="server" DataKeyNames="LTypeID" AutoGenerateColumns="False"
                                EmptyDataText="No Record Found">
                                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                </SelectedRowStyle>
                                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" Width="80px" Text="" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="LTypeTitle" HeaderText="Leave Title">
                                        <ItemStyle CssClass="ItemStylecss" Width="350px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Leave Entitled">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfLTypeID" Value='<%# Convert.ToString(Eval("LTypeID")) %>'
                                                runat='server' />
                                            <asp:HiddenField ID="hfLPakId" Value='<%# Convert.ToString(Eval("LeavePakID")) %>' runat='server' />
                                            <asp:TextBox ID="txtEntitled" MaxLength="5" Width="100px" Text='<%# Convert.ToString(Eval("MaxLAmt")) %>'
                                                runat="server" CssClass="TextBoxAmt60" />
                                            <cc1:FilteredTextBoxExtender ID="FiltTxtBoxEx" runat="server" TargetControlID="txtEntitled"
                                                FilterType="Custom,Numbers" ValidChars=".">
                                            </cc1:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:CheckBox ID="CheckBox3" runat="server" />
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                    <HeaderTemplate>
                        Leave Package
                    </HeaderTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                    <ContentTemplate>
                        <div id="leavePackageList2">
                            <asp:GridView ID="grLeavePakMst" runat="server" DataKeyNames="LeavePakID,IsActive,EmpTypeId,FromMonth,ToMonth,IsNextYear"
                                AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" Width="100%"
                                OnRowCommand="grLeavePakMst_RowCommand" OnSelectedIndexChanged="grLeavePakMst_SelectedIndexChanged">
                                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                </SelectedRowStyle>
                                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                <Columns>
                                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                        <ItemStyle Width="80px" CssClass="ItemStylecss" />
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="LPackName" HeaderText="Leave Package Name">
                                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfDesc" runat="server" Value='<%# Convert.ToString(Eval("LPdesc")) %>' />
                                            <asp:HiddenField ID="HfCalOnJoinDate" runat="server" Value='<%# Convert.ToString(Eval("IsLCalOnJoinDate"))%>' />
                                            <asp:HiddenField ID="hfIsOffdayCounted" runat="server" Value='<%# Convert.ToString(Eval("IsOffdayCounted"))%>' />
                                            <asp:HiddenField ID="hfIsActive" runat="server" Value='<%# Convert.ToString(Eval("IsActive"))%>' />
                                            <asp:HiddenField ID="hfISDefault" runat="server" Value='<%# Convert.ToString(Eval("ISDefault"))%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="IsActive" HeaderText="Is Active">
                                        <ItemStyle CssClass="ItemStylecss" Width="80px"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                    <HeaderTemplate>
                        Leave Package List
                    </HeaderTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <div id="DivCommand1" style="padding-top: 3px;">
                <div style="text-align: left; float: left">
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px" CausesValidation="False"
                        OnClick="btnRefresh_Click" />
                </div>
                <div style="text-align: right;">
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" UseSubmitBehavior="False"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                        OnClick="btnDelete_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
