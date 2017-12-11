<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="PayrollAttndClearance.aspx.cs" Inherits="Payroll_Payroll_PayrollAttndClearance"
    Title="Payroll Attendance Clearance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js"></script>
    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js"></script>
    <script language="javascript" type="text/javascript">
        function CheckBoxListSelect(cbControl, state) {
            var chkBoxList = document.getElementById(cbControl);
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }
            return false;
        }
    </script>
    <div class="formStyle">
        <div id="formhead1">
            <div style="width: 98%; float: left;">
                Payroll Attendance Clearance</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../File/Home.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" Width="500px" Font-Bold="True" ForeColor="Fuchsia"></asp:Label>
        </div>
        <div class="Div950">
            <fieldset>
                <table>
                    <tr style="background-color: #B3CDE4;">
                        <td class="textlevelleft">
                            Payroll Cycle
                        </td>
                        <td class="textlevelleft">
                            Month
                        </td>
                        <td class="textlevelleft">
                            Year
                        </td>
                        <td class="textlevelleft">
                            Employee Type
                        </td>
                        <td class="textlevelleft">
                            Employee ID
                        </td>
                        <td>Cost Center</td>         
                        <td class="textlevelleft">
                            Clearance Date
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlMPC" runat="server" Width="100px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" runat="server" Width="100px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlYear" runat="server" Width="80px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEmpType" runat="server" Width="130px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmpID" runat="server" Width="80px"></asp:TextBox>
                        </td> 
                        <td> <asp:DropDownList ID="ddlCostCenter" runat="server" Width="200px" 
                                CssClass="textlevelleft">
                            </asp:DropDownList></td>                       
                        <td>
                            <asp:TextBox ID="txtIssueDate" runat="server" Width="80px"></asp:TextBox>
                            <a href="javascript:NewCal('<%= txtIssueDate.ClientID %>','ddmmyyyy')">
                                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                    height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>
                        </td>
                        <td>
                            <asp:Button ID="btnGenerate" runat="server" Text="Generate" Width="80px" OnClick="btnGenerate_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <hr style="border: solid 1px #3399FF;" />
            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="460px">
                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                    <HeaderTemplate>
                        New Record for Clearance
                    </HeaderTemplate>
                    <ContentTemplate>
                        Select <a id="A1" href="#" onclick="javascript: CheckBoxListSelect ('<%= grAttendance.ClientID %>',true)">
                            All</a> | <a id="A2" href="#" onclick="javascript: CheckBoxListSelect ('<%= grAttendance.ClientID %>',false)">
                                None</a>
                        <fieldset>
                            <div style="width: 100%; overflow: scroll; height: 400px;">
                                <asp:GridView ID="grAttendance" runat="server" EmptyDataText="No Record Found" Font-Size="9px"
                                    Width="99%" AutoGenerateColumns="False" DataKeyNames="JOININGDATE,SeparateDate,ASTARTDAY,AENDDAY,PSTARTDAY,PENDDAY,EmpTypeID">
                                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                    </SelectedRowStyle>
                                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemStyle Width="3%" CssClass="ItemStylecss" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkBox" runat="Server" Checked="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EMPID" HeaderText="Emp. ID">
                                            <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FULLNAME" HeaderText="Name">
                                            <ItemStyle CssClass="ItemStylecss" Width="14%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="JobTitleName" HeaderText="Job Title">
                                            <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="JOININGDATE" HeaderText="Joining">
                                            <ItemStyle CssClass="ItemStylecss" Width="4%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SeparateDate" HeaderText="Seperation">
                                            <ItemStyle CssClass="ItemStylecss" Width="4%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SalLocName" HeaderText="Location">
                                            <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="From">
                                            <ItemStyle CssClass="ItemStylecss" Width="4%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="To">
                                            <ItemStyle CssClass="ItemStylecss" Width="4%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Month Days">
                                            <ItemStyle Width="3%" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAttndDays" runat="server" Style="text-align: center;" Width="20px"
                                                    Text='<%# Bind("WorkingDays") %>'></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="fltbAttndDays" runat="server" TargetControlID="txtAttndDays"
                                                    FilterType="Custom,Numbers" ValidChars=".">
                                                </cc1:FilteredTextBoxExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Salary Days">
                                            <ItemStyle Width="3%" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSalDays" Text="" runat="server" Style="text-align: center;" Width="20px"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="fltbSalDays" runat="server" TargetControlID="txtSalDays"
                                                    FilterType="Custom,Numbers" ValidChars=".,-">
                                                </cc1:FilteredTextBoxExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="P">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle CssClass="ItemStylecssCenter" Width="3%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="A">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle CssClass="ItemStylecssCenter" Width="3%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="W">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle CssClass="ItemStylecssCenter" Width="3%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="WP">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle CssClass="ItemStylecssCenter" Width="3%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="H">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle CssClass="ItemStylecssCenter" Width="3%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="HP">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle CssClass="ItemStylecssCenter" Width="3%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="TV">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle CssClass="ItemStylecssCenter" Width="3%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Leave">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle CssClass="ItemStylecssCenter" Width="3%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="LWP">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle CssClass="ItemStylecssCenter" Width="3%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Irregular">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle CssClass="ItemStylecssCenter" Width="3%"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <span style="color: #3366CC; font-size: 12px; font-family: Tahoma; font-weight: bold;">
                                Total Head Count:
                                <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                            </span>
                        </fieldset>
                        Select <a id="A3" href="#" onclick="javascript: CheckBoxListSelect ('<%= grAttendance.ClientID %>',true)">
                            All</a> | <a id="A4" href="#" onclick="javascript: CheckBoxListSelect ('<%= grAttendance.ClientID %>',false)">
                                None</a>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel1">
                    <HeaderTemplate>
                        Payroll Basket</HeaderTemplate>
                    <ContentTemplate>
                        Select <a id="A5" href="#" onclick="javascript: CheckBoxListSelect ('<%= grAttndClr.ClientID %>',true)">
                            All</a> | <a id="A6" href="#" onclick="javascript: CheckBoxListSelect ('<%= grAttndClr.ClientID %>',false)">
                                None</a>
                        <fieldset>
                            <div style="width: 100%; overflow: scroll; height: 380px;">
                                <asp:GridView ID="grAttndClr" runat="server" EmptyDataText="No Record Found" Font-Size="9px"
                                    Width="99%" AutoGenerateColumns="False" DataKeyNames="JOININGDATE,SeparateDate,PAYSTARTDATE,PAYENDDATE,EmpTypeID">
                                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                    </SelectedRowStyle>
                                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemStyle Width="3%" CssClass="ItemStylecss" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkBox" runat="Server" Checked="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EMPID" HeaderText="Emp. ID">
                                            <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FULLNAME" HeaderText="Name">
                                            <ItemStyle CssClass="ItemStylecss" Width="14%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="JobTitleName" HeaderText="Job Title">
                                            <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="JOININGDATE" HeaderText="Joining">
                                            <ItemStyle CssClass="ItemStylecss" Width="4%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SeparateDate" HeaderText="Seperation">
                                            <ItemStyle CssClass="ItemStylecss" Width="4%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SalLocName" HeaderText="Location">
                                            <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FROMDATE" HeaderText="From">
                                            <ItemStyle CssClass="ItemStylecss" Width="4%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TODATE" HeaderText="To">
                                            <ItemStyle CssClass="ItemStylecss" Width="4%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DAYSDUR" HeaderText="Month Days">
                                            <ItemStyle CssClass="ItemStylecssCenter" Width="3%"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SALDUR" HeaderText="Salary Days">
                                            <ItemStyle CssClass="ItemStylecssCenter" Width="3%"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="P" HeaderText="P">
                                            <ItemStyle CssClass="ItemStylecssCenter" Width="3%"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="A" HeaderText="A">
                                            <ItemStyle CssClass="ItemStylecssCenter" Width="3%"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="W" HeaderText="W">
                                            <ItemStyle CssClass="ItemStylecssCenter" Width="3%"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="WP" HeaderText="WP">
                                            <ItemStyle CssClass="ItemStylecssCenter" Width="3%"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="H" HeaderText="H">
                                            <ItemStyle CssClass="ItemStylecssCenter" Width="3%"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HP" HeaderText="HP">
                                            <ItemStyle CssClass="ItemStylecssCenter" Width="3%"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TV" HeaderText="TV">
                                            <ItemStyle CssClass="ItemStylecssCenter" Width="3%"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LV" HeaderText="Leave">
                                            <ItemStyle CssClass="ItemStylecssCenter" Width="3%"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LWP" HeaderText="LWP">
                                            <ItemStyle CssClass="ItemStylecssCenter" Width="3%"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ISIRREGULAR" HeaderText="Irregular">
                                            <ItemStyle CssClass="ItemStylecssCenter" Width="3%"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <span style="color: #3366CC; font-size: 12px; font-family: Tahoma; font-weight: bold;">
                                Total Head Count:
                                <asp:Label ID="lblExistRecordCount" runat="server"></asp:Label>
                            </span>
                        </fieldset>
                        Select <a id="A7" href="#" onclick="javascript: CheckBoxListSelect ('<%= grAttndClr.ClientID %>',true)">
                            All</a> | <a id="A8" href="#" onclick="javascript: CheckBoxListSelect ('<%= grAttndClr.ClientID %>',false)">
                                None</a>
                        <div style="width: 100%; text-align: center;">
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
                                Width="200px" />
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <div id="DivCommand1" style="padding-top: 3px;">
                <div style="text-align: left; float: left">
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px" CausesValidation="False" />
                </div>
                <div style="text-align: right;">
                    <asp:Button ID="btnSave" runat="server" Text="Save in Payroll Basket" UseSubmitBehavior="False"
                        OnClick="btnSave_Click" />&nbsp;
                </div>
            </div>
        </div>
    </div>
</asp:Content>
