<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="PFLoan.aspx.cs" Inherits="Payroll_Loan_PFLoan" Title="PF Loan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js"></script>
    <script language="javascript" type="text/javascript">
        function SetMonthlyInterest() {
            var txtLA = document.getElementById('<%=txtLoanAmount.ClientID%>');
            var txtLR = document.getElementById('<%=txtLoanRate.ClientID%>');
            var txtIN = document.getElementById('<%=txtInterest.ClientID%>');
            var ddlInsM = document.getElementById('<%=ddlInsMonth.ClientID%>');

            var vValue = ((txtLA.value * 1) * (txtLR.value * 1)) / (ddlInsM.value * 1);
            txtIN.value = Math.round(vValue * 1);
        }
        function SetMonthlyRepay() {
            var ddlIM = document.getElementById('<%=ddlInsMonth.ClientID%>');
            var txtLA = document.getElementById('<%=txtLoanAmount.ClientID%>');
            var txtMR = document.getElementById('<%=txtRepay.ClientID%>');

            var txtIN = document.getElementById('<%=txtInterest.ClientID%>');
            var txtLR = document.getElementById('<%=txtLoanRate.ClientID%>');

            var vValue = (txtLA.value * 1) / (ddlIM.value * 1);
            vValue = Math.round(vValue * 1)
            txtMR.value = vValue;

            var vInsValue = ((txtLA.value * 1) * (txtLR.value * 1)) / (ddlIM.value * 1);
            txtIN.value = Math.round(vInsValue * 1);
        }
    </script>
    <div id="PayrollConfigForm2">
        <div id="formhead1">
            <div style="width: 92%; float: left;">
                PF Loan Information
            </div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../File/Home.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="PayrollConfigInner">
            <div style="background-color: #EFF3FB;">
                <fieldset>
                    <table>
                        <tr>
                            <td class="textlevel" style="width: 85px;">
                                Employee Code:
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmpCode" runat="server" Width="80px"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnSearch" runat="server" ImageUrl="~/Images/Search_Icon.jpg"
                                    Width="20px" OnClick="imgbtnSearch_Click"  CausesValidation="False" />
                            </td>
                        </tr>
                    </table>
                    <div style="width: 99%; overflow: scroll; margin-top: 10px;">
                        <asp:GridView ID="grEmp" runat="server" DataKeyNames="EMPID" AutoGenerateColumns="False"
                            EmptyDataText="No Record Found" Font-Size="9px" Width="120%">
                            <HeaderStyle BackColor="#3366CC" ForeColor="White" Font-Bold="True"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                            </SelectedRowStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                            <Columns>
                                <asp:BoundField DataField="EMPID" HeaderText="Code">
                                    <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="FULLNAME" HeaderText="Name">
                                    <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="JobTitle" HeaderText="Designation">
                                    <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DivisionName" HeaderText="Organization">
                                    <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="BASICSAL" HeaderText="Basic Sal">
                                    <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="NETPF" HeaderText="NetPF">
                                    <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PFLoan" HeaderText="PFLoan">
                                    <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ALLOWPFLOAN" HeaderText="Allowable Loan">
                                    <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </fieldset>
            </div>
            <br />
            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%"
                Height="410px">
                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Width="100%" Height="400px">
                    <HeaderTemplate>
                        PF Loan
</HeaderTemplate>
                    


<ContentTemplate>
                        <br />
                        <br />
                        <fieldset>
                            <legend>Loan Parameters</legend>
                            <table>
                                <tr>
                                    <td style="background-color: #DF6F1D; text-align: center;">
                                        PF Code
                                    </td>
                                    <td style="background-color: #DF6F1D; text-align: center;">
                                        Loan Code
                                    </td>
                                    <td style="background-color: #DF6F1D; text-align: center;">
                                        Loan Date
                                    </td>
                                    <td style="background-color: #DF6F1D; text-align: center;">
                                    </td>
                                    <td style="background-color: #DF6F1D; text-align: center;">
                                        Emp Code
                                    </td>
                                    <td style="background-color: #DF6F1D; text-align: center;">
                                        Recommended By
                                    </td>
                                    <td style="background-color: #DF6F1D; text-align: center;">
                                        Purpose
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtPFCode" runat="server" Width="80px"></asp:TextBox>



                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTransID" runat="server" Width="80px"></asp:TextBox>



                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTransDate" runat="server" Width="80px"></asp:TextBox>



                                    </td>
                                    <td>
                                        <a href="javascript:NewCal('<%= txtTransDate.ClientID %>','ddmmyyyy')">
                                            <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                                height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCode" runat="server" Width="80px"></asp:TextBox>



                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRecomdBy" runat="server" Width="80px"></asp:TextBox>



                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPurpose" runat="server" Width="200px"></asp:TextBox>



                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <fieldset>
                            <legend>Loan Detail</legend>
                            <table>
                                <tr>
                                    <td class="textlevel">
                                        Loan Month
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlMonth" runat="server" Width="105px"></asp:DropDownList>



                                    </td>
                                    <td class="textlevel">
                                        FY
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFiscalYear" runat="server" Width="105px"></asp:DropDownList>



                                    </td>
                                </tr>
                                <tr>
                                    <td class="textlevel">
                                        Request for Loan Tk.
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReqAmount" runat="server" Width="100px" onchange="SetMonthlyInterest();"></asp:TextBox>



                                    </td>
                                    <td class="textlevel">
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="textlevel">
                                        Approved Loan Tk.
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLoanAmount" runat="server" onchange="SetMonthlyInterest();" Width="100px"></asp:TextBox>



                                    </td>
                                    <td class="textlevel">
                                        Approved Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtApproveDate" runat="server" Width="80px"></asp:TextBox>


<a href="javascript:NewCal('<%= txtApproveDate.ClientID %>','ddmmyyyy')"><img
                                            style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                            height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a><asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtApproveDate"
                                                CssClass="validator" ErrorMessage="Invalid" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>



                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtApproveDate"></asp:RequiredFieldValidator>



                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="textlevel">
                                        Installment
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlInsMonth" runat="server" onchange="SetMonthlyRepay();" Width="105px"><asp:ListItem Value="-1">Select</asp:ListItem>
<asp:ListItem Value="12">12 Months</asp:ListItem>
<asp:ListItem Value="24">24 Months</asp:ListItem>
<asp:ListItem Value="36">36 Months</asp:ListItem>
</asp:DropDownList>



                                    </td>
                                    <td class="textlevel">
                                        Installment Start Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInsStartDate" runat="server" Width="80px"></asp:TextBox>


<a href="javascript:NewCal('<%= txtInsStartDate.ClientID %>','ddmmyyyy')"><img
                                            style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                            height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a><asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtInsStartDate"
                                                CssClass="validator" ErrorMessage="Invalid" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>



                                        <asp:RequiredFieldValidator ID="ReqValTitle0" runat="server" 
                                            ControlToValidate="txtInsStartDate" ErrorMessage="*">*</asp:RequiredFieldValidator>



                                    </td>
                                </tr>
                                <tr>
                                    <td class="textlevel">
                                        Interest Rate
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLoanRate" runat="server" onchange="SetMonthlyInterest();" Width="100px" OnTextChanged="txtLoanRate_TextChanged">0.13</asp:TextBox>



                                    </td>
                                    <td class="textlevel">
                                        Monthly Interest
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInterest" runat="server" Width="100px"></asp:TextBox>



                                    </td>
                                </tr>
                                <tr>
                                    <td class="textlevel">
                                        Monthly Repay
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRepay" runat="server" Width="100px"></asp:TextBox>



                                    </td>
                                    <td class="textlevel">
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                            <cc1:FilteredTextBoxExtender ID="FilteredTxt1" runat="server" FilterType="Custom, Numbers"
                                TargetControlID="txtReqAmount" ValidChars="0123456789." Enabled="True"></cc1:FilteredTextBoxExtender>



                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers"
                                TargetControlID="txtLoanAmount" ValidChars="0123456789." Enabled="True"></cc1:FilteredTextBoxExtender>



                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers"
                                TargetControlID="txtLoanRate" ValidChars="0123456789." Enabled="True"></cc1:FilteredTextBoxExtender>



                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers"
                                TargetControlID="txtInterest" ValidChars="0123456789." Enabled="True"></cc1:FilteredTextBoxExtender>



                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom, Numbers"
                                TargetControlID="txtRepay" ValidChars="0123456789." Enabled="True"></cc1:FilteredTextBoxExtender>



                        </fieldset>
                        <fieldset>
                            <legend>Bank Detail</legend>
                            <table>
                                <tr>
                                    <td class="textlevel">
                                        Receive Tk.
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRecTk" runat="server" Width="300px"></asp:TextBox>



                                    </td>
                                    <td class="textlevel">
                                        Receive Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRecDate" runat="server" Width="80px"></asp:TextBox>


<a href="javascript:NewCal('<%= txtRecDate.ClientID %>','ddmmyyyy')"><img
                                            style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                            height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>

                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="textlevel">
                                        Cheque Number
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtChequeNumber" runat="server" Width="300px"></asp:TextBox>



                                    </td>
                                    <td class="textlevel">
                                        &nbsp;Cheque Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtChequeDate" runat="server" Width="80px"></asp:TextBox>


<a href="javascript:NewCal('<%= txtChequeDate.ClientID %>','ddmmyyyy')"><img
                                            style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                            height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="textlevel">
                                        Bank Detail
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBankDetail" runat="server" Width="300px"></asp:TextBox>



                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom, Numbers"
                                TargetControlID="txtRecTk" ValidChars="0123456789." Enabled="True"></cc1:FilteredTextBoxExtender>



                            <asp:HiddenField ID="hfIsUpdate" runat="server" />



                        </fieldset>
                    
</ContentTemplate>
                


</cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                    <HeaderTemplate>
                        Browse
</HeaderTemplate>
                    


<ContentTemplate>
                        <fieldset>
                            <div style="overflow: scroll; height: 300px; margin-top: 10px;">
                                <asp:GridView ID="grLoan" runat="server" DataKeyNames="EMPID,FISCALYRID,LOANMONTH,CHEQUENUMER,CHEQUEDATE,BANKDETAIL,LOANSTATUS,ISDEDUCTCOMPLETE,INSDATE,REQLOANAMT,APPLOANDATE,RECEIVEAMT,RECEIVEDATE"
                                    AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" Width="99%"
                                    OnRowCommand="grLoan_RowCommand">
                                    <HeaderStyle BackColor="#3366CC" ForeColor="White" Font-Bold="True"></HeaderStyle>
                                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                    </SelectedRowStyle>
                                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:ButtonField CommandName="DoubleClick" HeaderText="Select" Text="Select">
                                            <ItemStyle CssClass="ItemStylecss" Width="3%" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="TRANSID" HeaderText="LOANID">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TRANSDATE" HeaderText="Entry Date">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LOANMONTH" HeaderText="Month">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FISCALYRTITLE" HeaderText="Year">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LOANAMT" HeaderText="Amount">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="INSTALLMENT" HeaderText="Installment(Months)">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="INSDATE" HeaderText="Installment Date">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LOANRATE" HeaderText="Rate">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MONTHLYREPAY" HeaderText="Monthly Repay">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MONTHLYINTEREST" HeaderText="Monthly Int.">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RECOMMENDBY" HeaderText="RECOMMEND BY">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PURPOSE" HeaderText="Purpose">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </fieldset>
                    
</ContentTemplate>
                


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
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();" OnClick="btnDelete_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
