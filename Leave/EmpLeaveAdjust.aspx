<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="EmpLeaveAdjust.aspx.cs" Inherits="EmpLeaveAdjust" Title="Employee Leave Adjust" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">
    function ToUpper(ctrl)
    {   
    var t = ctrl.value;
    ctrl.value = t.toUpperCase(); 
    }
    </script>

    <div class="programSetup">
        <div id='formhead4'>
            Leave Balance Entry</div>
        <div class="MsgBox">
            <!--Div for msg-->
            &nbsp;
            <asp:Label ID="lblMsg" runat="server" CssClass="lblMsg"></asp:Label>
        </div>
        <div class="Div950">
            <!--Div for group-->
            <fieldset>
                <legend>Status</legend>
                <%--<asp:UpdatePanel id="UpdatePanel1" runat="server">
                    <contenttemplate>--%>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Employee No : " CssClass="textlevel"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmpId" onkeyup="ToUpper(this)" Width="80px" runat="server"> </asp:TextBox>
                                <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Search"></asp:Button>
                            </td>
                            <td>
                            </td>
                            <td align="right" colspan="4">
                                &nbsp;&nbsp;&nbsp;&nbsp;</td>
                        </tr>
                    </tbody>
                </table>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Name : " CssClass="textlevel"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblName" runat="server" __designer:wfdid="w1"></asp:Label></td>
                            <td>
                                <asp:Label ID="LabelEmpType" runat="server" Text="Employment Type : " CssClass="textlevel"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblEmpType" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Designation :" CssClass="textlevel" __designer:wfdid="w63"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblDesig" runat="server" __designer:wfdid="w2"></asp:Label></td>
                            <td>
                                <asp:Label ID="LabelJoin" runat="server" Text="Joining Date : " CssClass="textlevel"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblJoin" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Leave Package :" CssClass="textlevel"
                                    __designer:wfdid="w63"></asp:Label></td>
                            <td align="left" colspan="2">
                                <asp:Label ID="lblLvPack" runat="server" __designer:wfdid="w2"></asp:Label></td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="color: #f00" align="left" colspan="4">
                                <asp:Label ID="lblMsg2" runat="server" CssClass="lblMsg" Width="343px"></asp:Label></td>
                        </tr>
                    </tbody>
                </table>
                <fieldset>
                    <legend>Leave Status</legend>
                    <div>
                        <asp:GridView ID="grLeaveStatus" runat="server" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="EmpId,LTypeID,LAbbrName,LTypeTitle,LCarryOverd,LEntitled,LCashed,LeaveEnjoyed,LeaveElapsed,UpdatedBy,UpdatedDate"
                            EmptyDataText="No Record Found" PageSize="7" OnSelectedIndexChanged="grLeaveStatus_SelectedIndexChanged"
                            Font-Size="9px">
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemStyle CssClass="ItemStylecss" Width="40px" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkBox" runat="Server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="LTypeTitle" HeaderText="Leave Title">
                                    <ItemStyle Width="250px" CssClass="ItemStylecssCenter"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Opening Carry Over" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPrevYearCarry" MaxLength="6" Width="60px" Text='<%# Convert.ToString(Eval("lvPrevYearCarry")) %>'
                                            runat="server" CssClass="TextBoxAmt60" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="Custom,Numbers"
                                            runat="server" TargetControlID="txtPrevYearCarry" ValidChars=".">
                                        </cc1:FilteredTextBoxExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Carry Over">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfLTypeID" Value='<%# Convert.ToString(Eval("LTypeID"))%>' runat="server" />
                                        <asp:TextBox ID="txtLCarryOverd" MaxLength="6" Width="60px" Text='<%# Convert.ToString(Eval("LCarryOverd")) %>'
                                            runat="server" CssClass="TextBoxAmt60" Enabled="true"/>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Custom,Numbers"
                                            runat="server" TargetControlID="txtLCarryOverd" ValidChars=".">
                                        </cc1:FilteredTextBoxExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Entitled">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtLEntitled" MaxLength="6" Width="60px" Text='<%# Convert.ToString(Eval("LEntitled")) %>'
                                            runat="server" CssClass="TextBoxAmt60" Enabled="true"/>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" FilterType="Custom,Numbers"
                                            runat="server" TargetControlID="txtLEntitled" ValidChars=".">
                                        </cc1:FilteredTextBoxExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Availed">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtLeaveEnjoyed" MaxLength="6" Width="60px" Text='<%# Convert.ToString(Eval("LeaveEnjoyed")) %>'
                                            runat="server" CssClass="TextBoxAmt60" />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" FilterType="Custom,Numbers"
                                            runat="server" TargetControlID="txtLeaveEnjoyed" ValidChars=".">
                                        </cc1:FilteredTextBoxExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="LCarryOverd" HeaderText="Carry Over">
                                <ItemStyle Width="80px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                              </asp:BoundField>
                              <asp:BoundField DataField="LEntitled" HeaderText="Entitled">
                                <ItemStyle Width="120px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                              </asp:BoundField>
                              <asp:BoundField DataField="LeaveEnjoyed" HeaderText="Availed">
                                <ItemStyle Width="80px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                              </asp:BoundField>--%>
                                <asp:TemplateField HeaderText="Opening Leave(Already Enjoyed)" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOpening" MaxLength="6" Width="60px" Text='<%# Convert.ToString(Eval("lvOpening")) %>'
                                            runat="server" CssClass="TextBoxAmt60" />
                                        <cc1:FilteredTextBoxExtender ID="FiltTxtBoxEx" FilterType="Numbers" runat="server"
                                            TargetControlID="txtOpening">
                                        </cc1:FilteredTextBoxExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Balance">
                                    <ItemStyle Width="80px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="UpdatedBy" HeaderText="Adjusted By">
                                    <ItemStyle Width="100px" CssClass="ItemStylecss"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="UpdatedDate" HeaderText="Adjusted Date">
                                    <ItemStyle Width="120px" CssClass="ItemStylecss"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle BackColor="#B3CDE4" HorizontalAlign="Center" Font-Size="10px" Font-Bold="True">
                            </HeaderStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        </asp:GridView>
                    </div>
                </fieldset>
                <%--</contenttemplate>
                </asp:UpdatePanel>--%>
                <div style="text-align: left; float: left; width: 45%;">
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px" OnClick="btnRefresh_Click"
                        UseSubmitBehavior="False" />
                </div>
                <div style="text-align: right; float: right; width: 45%;">
                    <asp:Button ID="btnClear" runat="server" Text="Save" Width="70px" OnClick="btnClear_Click"
                        UseSubmitBehavior="False" />
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
