<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="LeaveEncashment.aspx.cs" Inherits="Payroll_Payroll_LeaveEncashment"
    Title="Leave Encashment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js"></script>

    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js"></script>

    <script language="javascript" type="text/javascript">
        function ToUpper(ctrl)
        {   
            var t = ctrl.value;
            ctrl.value = t.toUpperCase(); 
        }
        function printDiv(divName) 
        {
             var printContents = document.getElementById(divName).innerHTML;
             var originalContents = document.body.innerHTML;

             document.body.innerHTML = printContents;

             window.print();

             document.body.innerHTML = originalContents;
        }
    </script>

    <div class="formStyle" style="width: 80%;">
        <div id="formhead1">
            <div style="width: 92%; float: left;">
                Leave Encashment</div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Fuchsia"></asp:Label>
        </div>
        <div class="Div950">
            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%"
                Height="360px">
                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Width="100%" Height="350px">
                    <HeaderTemplate>
                        Payment
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div style="background-color: #EFF3FB; border: solid 1px #3E506C;">
                            <%--<fieldset>--%>
                            <table>
                                <tr>
                                    <td class="textlevelleft">
                                        Payment Month</td>
                                    <td style="background-color: Gray; width: 250px;" class="textlevelleft">
                                        Last Payroll Month & Fiscal Year</td>
                                    <td class="textlevelleft">
                                        Payment Year</td>
                                    <td class="textlevelleft">
                                        Fiscal Year</td>
                                    <td class="textlevelleft" style="width: 150px;">
                                        Last Festival :</td>
                                    <td class="textlevelleft">
                                        No of Times :</td>
                                    <td class="textlevelleft">
                                        Employee<asp:RequiredFieldValidator ID="rfEmpID" runat="server" ControlToValidate="txtEmpID"
                                            ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                    <td class="textlevelleft">
                                    </td>
                                    <td>
                                    </td>
                                    <td class="textlevelleft">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlMonth" runat="server" Width="95px" CssClass="textlevel">
                                        </asp:DropDownList></td>
                                    <td>
                                        <asp:DropDownList ID="ddlPayrollMonth" runat="server" Width="95px" CssClass="textlevel">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlPayrollFinYear" runat="server" Width="150px" CssClass="textlevel">
                                        </asp:DropDownList></td>
                                    <td>
                                        <asp:DropDownList ID="ddlYear" runat="server" Width="85px" CssClass="textlevel">
                                        </asp:DropDownList></td>
                                    <td>
                                        <asp:DropDownList ID="ddlFiscalYear" runat="server" Width="130px" CssClass="textlevel">
                                        </asp:DropDownList></td>
                                    <td>
                                        <asp:TextBox ID="txtLastFestival" runat="server" Width="80px"></asp:TextBox>
                                        <a href="javascript:NewCal('<%= txtLastFestival.ClientID %>','ddmmyyyy')">
                                            <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                                height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTimes" runat="server" Width="80px" CssClass="TextBoxAmt100"
                                            AutoPostBack="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmpID" runat="server" Width="80px" onkeyup="ToUpper(this)"></asp:TextBox></td>
                                    <td>
                                        <asp:ImageButton ID="imgBtnSearch" runat="server" Height="19px" ImageUrl="~/Images/Search_Icon.jpg"
                                            Width="21px" CausesValidation="False" OnClick="imgBtnSearch_Click" /></td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <%--</fieldset>--%>
                        </div>
                        <div style="margin-top: 10px;">
                            <div style="border: solid 1px #3E506C;">
                                <table>
                                    <tr>
                                        <td class="textlevel">
                                            Employee Name :</td>
                                        <td colspan="3">
                                            <asp:Label ID="lblEmpName" runat="server"></asp:Label></td>
                                        <td class="textlevel">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            Designation :</td>
                                        <td colspan="3">
                                            <asp:Label ID="lblDesig" runat="server"></asp:Label></td>
                                        <td class="textlevel">
                                            Date of Join :</td>
                                        <td>
                                            <asp:Label ID="lblJoiningDate" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <div style="border: solid 1px #3E506C;">
                                <table>
                                    <tr>
                                        <td class="textlevel">
                                            Basic Salary :</td>
                                        <td>
                                            <asp:TextBox ID="txtBasicSal" runat="server" Width="80px" CssClass="TextBoxAmt100"
                                                ReadOnly="True"></asp:TextBox></td>
                                        <td class="textlevel">
                                            AL Leave Balance :</td>
                                        <td>
                                            <asp:TextBox ID="txtLevBal" runat="server" Width="80px" CssClass="TextBoxAmt100"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            Unit Day Salary :</td>
                                        <td>
                                            <asp:TextBox ID="txtUnitDaySal" runat="server" ReadOnly="true" Width="80px" CssClass="TextBoxAmt100"></asp:TextBox>
                                        </td>
                                        <td class="textlevel">
                                            Leave Amount :</td>
                                        <td>
                                            <asp:TextBox ID="txtLeaveAmt" runat="server" CssClass="TextBoxAmt100" Width="80px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td style="padding-left: 10px; font-weight: bold; text-align: right;">
                                            Total :</td>
                                        <td>
                                            <asp:TextBox ID="txtBalance" runat="server" Width="80px" CssClass="TextBoxAmt100"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <div style="border: solid 1px #3E506C;">
                                <table>
                                    <tr>
                                        <td class="textlevel">
                                            Bonus Allowance :</td>
                                        <td>
                                            <asp:TextBox ID="txtBonusAllowance" runat="server" Width="80px" CssClass="TextBoxAmt100"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDateDuration" runat="server" Text="-----" Width="400px" CssClass="textlevel"
                                                Style="text-align: left;"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <div style="border: solid 1px #3E506C;">
                                <table>
                                    <tr>
                                        <td class="textlevel">
                                            Pay Date :</td>
                                        <td>
                                            <asp:TextBox ID="txtPayDate" runat="server" Width="80px"></asp:TextBox>
                                            <a href="javascript:NewCal('<%= txtPayDate.ClientID %>','ddmmyyyy')">
                                                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                                    height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            Remarks :</td>
                                        <td>
                                            <asp:TextBox ID="txtRemarks" runat="server" Width="382px"></asp:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <%--<cc1:FilteredTextBoxExtender ID="fltbOPCARE" runat="server" TargetControlID="txtPrevMonthGratuity"
                            FilterType="Custom, Numbers" Enabled="True">
                        </cc1:FilteredTextBoxExtender>--%>
                        <%--<cc1:FilteredTextBoxExtender ID="fltbOPInt" runat="server" TargetControlID="txtCurrMonthGratuity"
                            FilterType="Custom, Numbers" Enabled="True">
                        </cc1:FilteredTextBoxExtender>--%>
                        <%--<cc1:FilteredTextBoxExtender ID="fltbOPTotal" runat="server" TargetControlID="txtCharging"
                            FilterType="Custom, Numbers" Enabled="True">
                        </cc1:FilteredTextBoxExtender>--%>
                        <asp:HiddenField ID="hfLedgerID" runat="server" />
                        <asp:HiddenField ID="hfIsUpdate" runat="server" />
                        &nbsp;&nbsp;
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2" Height="360px">
                    <HeaderTemplate>
                        Payment List</HeaderTemplate>
                    <ContentTemplate>
                        <div style="background-color: #EFF3FB; border: solid 1px #3E506C;">
                            <table>
                                <tr>
                                    <td class="textlevel">
                                        Gratuity Month :</td>
                                    <td>
                                        <asp:DropDownList ID="ddlMonth2" runat="server" Width="95px" CssClass="textlevel">
                                        </asp:DropDownList></td>
                                    <td class="textlevel">
                                        Fiscal Year :</td>
                                    <td>
                                        <asp:DropDownList ID="ddlFiscalYear2" runat="server" Width="150px" CssClass="textlevel">
                                        </asp:DropDownList></td>
                                    <td style="width: 100px;">
                                        <asp:ImageButton ID="imgSearch2" runat="server" Height="19px" ImageUrl="~/Images/Search_Icon.jpg"
                                            Width="21px" CausesValidation="False" OnClick="imgSearch2_Click" /></td>
                                    <td style="width: 3px">
                                        <asp:Button ID="btnPrint2" runat="server" Text="Print All Leave Encash Slip For Selected Month"
                                            CausesValidation="False" OnClick="btnPrint2_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <hr />
                        <div style="width: 99%; overflow: scroll; margin-top: 10px; height: 250px;">
                            <asp:GridView ID="grPayment" runat="server" DataKeyNames="TRANSID,CURRGRATUITY" AutoGenerateColumns="False"
                                EmptyDataText="No Record Found" Font-Size="9px" Width="100%" OnRowCommand="grPayment_RowCommand">
                                <HeaderStyle BackColor="#3366CC" ForeColor="White" Font-Bold="True"></HeaderStyle>
                                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                </SelectedRowStyle>
                                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                <Columns>
                                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Select" Text="Select">
                                        <ItemStyle CssClass="ItemStylecss" Width="5%" />
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="TRANSID" HeaderText="Tr.No">
                                        <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EMPID" HeaderText="EmpId">
                                        <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FULLNAME" HeaderText="Name">
                                        <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="JOBTITLE" HeaderText="Designation">
                                        <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="JOININGDATE" HeaderText="Date of Join">
                                        <ItemStyle CssClass="ItemStylecss" Width="8%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="GrDuration" HeaderText="Leave Balance">
                                        <ItemStyle CssClass="ItemStylecss" Width="6%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BASIC" HeaderText="Basic">
                                        <ItemStyle CssClass="ItemStylecss" Width="6%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UnitDaySal" HeaderText="Unit Day Salary">
                                        <ItemStyle CssClass="ItemStylecss" Width="6%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PAYAMT" HeaderText="Balance">
                                        <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PAYDATE" HeaderText="Date">
                                        <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="REMARKS" HeaderText="Remarks">
                                        <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:ButtonField CommandName="RowDeleting" HeaderText="Gratuity Slip" Text="Print">
                                        <ItemStyle CssClass="ItemStylecss" Width="5%" />
                                    </asp:ButtonField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <br />
                        <asp:Button ID="btnExport1" runat="server" Text="Export to Excel " Width="200px"
                            CausesValidation="False" OnClick="btnExport1_Click" />
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <div id="DivCommand1" style="padding-top: 3px;">
                <div style="text-align: left; float: left">
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px" OnClick="btnRefresh_Click"
                        CausesValidation="False" />
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
