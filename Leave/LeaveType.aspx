<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="LeaveType.aspx.cs" Inherits="LeaveTypeSetup" Title="Leave Type Setup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
window.onload=LeaveNatureChanged;
function LeaveNatureChanged()
{
    var ddlNature = document.getElementById('<%= ddlLNature.ClientID %>');
    var pnlCO=document.getElementById('<%=PanCarryOver.ClientID%>');
    var pnlCsh=document.getElementById('<%=PanCashable.ClientID%>');
    var pnlCOCsh=document.getElementById('<%=PanCarryOverAndCashable.ClientID%>');
    var pnlM=document.getElementById('<%=PanMaternal.ClientID%>');
    var myindex  = ddlNature.selectedIndex
    var SelValue = ddlNature.options[myindex].value
    if(SelValue=="0")
    {
        //alert("1stIf");
        pnlCO.style.visibility="hidden";
        pnlCsh.style.visibility="hidden";
        pnlCOCsh.style.visibility="hidden";
        pnlM.style.visibility="hidden";
    }
    if(SelValue=="1")
    {
        //alert("1stIf");
        pnlCO.style.visibility="visible";
        pnlCsh.style.visibility="hidden";
        pnlCOCsh.style.visibility="hidden";
        pnlM.style.visibility="hidden";
    }
    if(SelValue=="2")
    {
        //alert("1stIf");
        pnlCO.style.visibility="hidden";
        pnlCsh.style.visibility="visible";
        pnlCOCsh.style.visibility="hidden";
        pnlM.style.visibility="hidden";
        
    }
    if(SelValue=="3")
    {
        //alert("1stIf");
        pnlCO.style.visibility="hidden";
        pnlCsh.style.visibility="hidden";
        pnlCOCsh.style.visibility="visible";
        pnlM.style.visibility="hidden";
    }
    if(SelValue=="4")
    {
        //alert("1stIf");
        pnlCO.style.visibility="hidden";
        pnlCsh.style.visibility="hidden";
        pnlCOCsh.style.visibility="hidden";
        pnlM.style.visibility="visible";
    }
    if (SelValue == "5") {
        pnlCO.style.visibility = "hidden";
        pnlCsh.style.visibility = "hidden";
        pnlCOCsh.style.visibility = "hidden";
        pnlM.style.visibility = "visible";
    }
    if(SelValue=="7")
    {
        //alert("1stIf");
        pnlCO.style.visibility="hidden";
        pnlCsh.style.visibility="hidden";
        pnlCOCsh.style.visibility="hidden";
        pnlM.style.visibility="hidden";
    }
}
    </script>

    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>

    <div id="leaveTypeForm">
        <div id="formhead1">
            <div style="width: 94%; float: left;">
                Leave Type</div>
        <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="attendanceFormInner">
            <!--Div for Controls-->
            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="470px">
                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Leave Types" Height="460px">
                    <HeaderTemplate>
                        Leave Type</HeaderTemplate>
                    <ContentTemplate>
                        <div class="Div650">
                            <table>
                                <tr>
                                    <td style="height: 28px">
                                        <asp:Label ID="Label2" runat="server" CssClass="textlevel" Text="Leave Title :" 
                                            Width="100px"></asp:Label></td>
                                    <td style="width: 317px; height: 28px">
                                        <asp:TextBox ID="txtLeaveType" runat="server" Width="309px"></asp:TextBox></td>
                                    <td style="height: 28px">
                                        <asp:RequiredFieldValidator ID="ReqVald" runat="server" ControlToValidate="txtLeaveType"
                                            ErrorMessage="*">*</asp:RequiredFieldValidator></td>
                                    <td style="height: 28px">
                                        <asp:Label ID="Label9" runat="server" CssClass="textlevel" Text="Abbr. Name :"></asp:Label></td>
                                    <td style="width: 45px; height: 28px">
                                        <asp:TextBox ID="txtAbbrName" runat="server" Width="40px" MaxLength="3"></asp:TextBox></td>
                                    <td style="width: 3px">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAbbrName"
                                            ErrorMessage="*">*</asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" CssClass="textlevel" Text="Description :" 
                                            Width="100px"></asp:Label></td>
                                    <td style="width: 317px">
                                        <asp:TextBox ID="txtDescription" runat="server" Width="309px"></asp:TextBox></td>
                                    <td>
                                    </td>
                                    <td style="width: 3px">
                                        <asp:CheckBox ID="chkIsActive" runat="server" Width="111px" Text="Mark Inactive"
                                            CssClass="textlevel" /></td>
                                    <td style="width: 45px">
                                    </td>
                                    <td style="width: 3px">
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" CssClass="textlevel" Text="Leave Unit :" 
                                            Width="100px"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlLMUnit" runat="server" CssClass="textlevel" Width="65px">
                                            <asp:ListItem Value="D">Day</asp:ListItem>
                                            <asp:ListItem Value="H">Hour</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td>
                                        <asp:CheckBox ID="chkPermanent" runat="server" Width="379px" Text="Only permanent Employee will get this leave"
                                            CssClass="textlevelleft" />
                                        <asp:CheckBox ID="chkIsOffdayCounted" runat="server" CssClass="textlevelleft" 
                                            style="margin-bottom: 20px" 
                                            Text="Weekend And Holiday Between leave Duration will be encounted as Leave" 
                                            Width="382px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" CssClass="textlevel" Text="At a Time  Maximum :"></asp:Label></td>
                                    <td align="left">
                                        <asp:TextBox ID="txtMaximumLeave" runat="server" CssClass="TextBoxAmt60" 
                                            Width="80px"></asp:TextBox></td>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="Days Of leave" 
                                            CssClass="textlevelleft"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label20" runat="server" CssClass="textlevel" Text="Eligibilty :" 
                                            Width="100px"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtEligibilityTime" runat="server" CssClass="TextBoxAmt60" 
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label19" runat="server" CssClass="textlevelleft" 
                                            Text="Eligibilty To receive this leave after completing (Months) " 
                                            Width="278px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtMaximumLeave"
                                ValidChars="1234567890" runat="server" Enabled="True">
                            </cc1:FilteredTextBoxExtender>
                            <table>
                                <tr>
                                    <td style="width: 3px">
                                        <asp:Label ID="Label4" runat="server" CssClass="textlevel" 
                                            Text="Leave Nature :" Width="100px"></asp:Label></td>
                                    <td style="width: 3px">
                                        <asp:DropDownList ID="ddlLNature" runat="server" Width="311px" onchange="LeaveNatureChanged()">
                                            <asp:ListItem Value="0">Strict</asp:ListItem>
                                            <asp:ListItem Value="1">Carry Over</asp:ListItem>
                                            <asp:ListItem Value="2">Cashable</asp:ListItem>
                                            <asp:ListItem Value="3">Both Carry Over &amp; Cashable</asp:ListItem>
                                            <asp:ListItem Value="4">Maternity</asp:ListItem>
                                            <asp:ListItem Value="5">Paternity</asp:ListItem>
                                            <asp:ListItem Value="6">LWP</asp:ListItem>
                                            <asp:ListItem Value="7">Complementary</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="width: 3px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 3px">
                                    </td>
                                    <td style="width: 400px">
                                        <asp:Panel ID="PanCarryOver" runat="server" Width="560px">
                                            <asp:TextBox ID="txtCancarryOver" runat="server" CssClass="TextBoxAmt60" 
                                                MaxLength="2" Width="80px"></asp:TextBox>
                                            <asp:Label ID="Label11" runat="server" CssClass="textlevelleft" Text="No of Days Can Carry Over"
                                                Width="143px"></asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtMaxLimtCarry" runat="server" CssClass="TextBoxAmt60" 
                                                Width="80px"></asp:TextBox>
                                            <asp:Label ID="Label14" runat="server" CssClass="textlevelleft" Text="Max Limit Can Carry Over "
                                                Width="143px"></asp:Label></asp:Panel>
                                        <asp:Panel ID="PanCashable" runat="server" Width="212px">
                                            <asp:TextBox ID="txtCanCashable" runat="server" CssClass="TextBoxAmt60" 
                                                Width="80px"></asp:TextBox><asp:Label
                                                ID="Label12" runat="server" CssClass="textlevelleft" Text="No of Days Can Be Cashable "
                                                Width="143px"></asp:Label></asp:Panel>
                                        <asp:Panel ID="PanCarryOverAndCashable" runat="server" Width="500px">
                                            <asp:TextBox ID="txtCarryOverAndCashable" runat="server" 
                                                CssClass="TextBoxAmt60" Width="80px"></asp:TextBox>
                                            <asp:Label ID="Label15" runat="server" CssClass="textlevelleft" Text="No of Days Can Be Carry Over And Cashable "
                                                Width="221px"></asp:Label></asp:Panel>
                                        <asp:Panel ID="PanMaternal" runat="server" Width="560px">
                                            <asp:Label ID="Label16" runat="server" CssClass="textlevelleft" Text="Next leave will be admissible only after "
                                                Width="189px"></asp:Label>
                                            <asp:TextBox ID="txtNextLevInt" runat="server" CssClass="TextBoxAmt60" 
                                                Width="80px"></asp:TextBox>
                                            <asp:Label ID="Label17" runat="server" CssClass="textlevelleft" Text="months of previous leave end date"
                                                Width="250px"></asp:Label>
                                            <asp:Label ID="Label13" runat="server" CssClass="textlevelleft" Text="Applicant will get "
                                                Width="83px"></asp:Label>
                                            <asp:TextBox ID="txtNoOfTimes" runat="server" CssClass="TextBoxAmt60" 
                                                Width="80px"></asp:TextBox>
                                            <asp:Label ID="Label18" runat="server" CssClass="textlevelleft" Text="times of her total service life "
                                                Width="147px"></asp:Label></asp:Panel>
                                        <br />
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3"
                                                FilterType="Custom, Numbers" TargetControlID="txtCancarryOver" ValidChars="1234567890."
                                                runat="server" Enabled="True">
                                            </cc1:FilteredTextBoxExtender>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" FilterType="Numbers" TargetControlID="txtCanCashable"
                                            ValidChars="1234567890" runat="server" Enabled="True">
                                        </cc1:FilteredTextBoxExtender>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" FilterType="Numbers" TargetControlID="txtCarryOverAndCashable"
                                            ValidChars="1234567890" runat="server" Enabled="True">
                                        </cc1:FilteredTextBoxExtender>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" FilterType="Numbers" runat="server"
                                            TargetControlID="txtNextLevInt" Enabled="True">
                                        </cc1:FilteredTextBoxExtender>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" FilterType="Numbers" TargetControlID="txtEligibilityTime"
                                            ValidChars="1234567890" runat="server" Enabled="True">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                    <td style="width: 3px">
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td style="width: 3px">
                                        <asp:Label ID="Label5" runat="server" CssClass="textlevel" Text="Calculate 1 Unit per"
                                            Visible="False"></asp:Label></td>
                                    <td style="width: 500px" align="right">
                                        <asp:CheckBox ID="chkCalculate1UnitPer" Text="Calculate 1 Unit Per" runat="server"
                                            Visible="False" />
                                        <asp:TextBox ID="txtCalcInterval" runat="server" Width="48px" CssClass="TextBoxAmt60"
                                            Visible="False"></asp:TextBox>
                                        <asp:DropDownList ID="ddlCalBase" runat="server" Visible="False">
                                            <asp:ListItem Value="1">Days</asp:ListItem>
                                            <asp:ListItem Value="2">Working Days</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="Numbers" TargetControlID="txtCalcInterval"
                                ValidChars="1234567890" runat="server" Enabled="True">
                            </cc1:FilteredTextBoxExtender>
                            <asp:HiddenField ID="hfIsUpadate" runat="server" />
                            <asp:HiddenField ID="hfID" runat="server" />
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Leave Types Description">
                    <HeaderTemplate>
                        Leave Type List
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div style="margin: 15px 0px 0px 0px; height: 400px; width: 100%; border: solid 1px black;
                            overflow: scroll;">
                            <asp:GridView ID="grLeaveType" runat="server" DataKeyNames="LTypeID,IsActive" AutoGenerateColumns="False"
                                EmptyDataText="No Record Found" Font-Size="9px" OnRowCommand="grLeaveType_RowCommand"
                                OnSelectedIndexChanged="grLeaveType_SelectedIndexChanged" Width="98%">
                                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                </SelectedRowStyle>
                                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                <Columns>
                                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                        <ItemStyle CssClass="ItemStylecss" Width="5%" />
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="LTypeTitle" HeaderText="Leave Title">
                                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LeaveDesc" HeaderText="Description">
                                        <ItemStyle CssClass="ItemStylecss" Width="45%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Leave Cal Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLMName" Text='<%# DayHour(Convert.ToString(Eval("LMunit"))) %>'
                                                runat="server" Width="10%"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="LNature">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnitName" Text='<%# GetLNature(Convert.ToString(Eval("LNature"))) %>'
                                                runat="server"></asp:Label>
                                            <asp:HiddenField ID="hfLNature" Value='<%#Convert.ToString(Eval("LNature"))%>' runat="server" />
                                            <asp:HiddenField ID="hfLeaveTTL" Value='<%#Convert.ToString(Eval("LeaveTTL"))%>'
                                                runat="server" />
                                            <asp:HiddenField ID="hfMaxCarryCashDay" Value='<%#Convert.ToString(Eval("NumberofDaysInCashAble"))%>'
                                                runat="server" />
                                           <asp:HiddenField ID="hfEligibility" Value='<%#Convert.ToString(Eval("Eligibility"))%>'
                                                runat="server" />
                                             <asp:HiddenField ID="hfNextLevInterval" Value='<%#Convert.ToString(Eval("NextLevInterval"))%>'
                                                runat="server" />
                                            <asp:HiddenField ID="hfTotalMatLev" Value='<%#Convert.ToString(Eval("TotalMatLev"))%>'
                                                runat="server" />
                                            <asp:HiddenField ID="hfmaxCarryLimit" Value='<%#Convert.ToString(Eval("CarryToNextYear"))%>'
                                                runat="server" />
                                            <asp:HiddenField ID="hfIsOffdayCounted" runat="server" Value='<%# Convert.ToString(Eval("IsOffdayCounted"))%>' />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="LAbbrName" HeaderText="Abbr. Name">
                                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="IsActive" HeaderText="Is Active">
                                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <div class="DivCommand1">
                <div class="DivCommandL">
                    <asp:Button ID="btnClear" runat="server" Text="Refresh" Width="70px" OnClick="btnClear_Click"
                        UseSubmitBehavior="False" CausesValidation="False" />
                </div>
                <div style="text-align: right;">
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                        OnClick="btnDelete_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
