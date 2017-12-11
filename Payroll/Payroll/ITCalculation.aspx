<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="ITCalculation.aspx.cs" Inherits="Payroll_Payroll_ITCalculation" Title="IT Calculation Sheet"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
    function CheckBoxListSelect(cbControl, state)
    {   
       var chkBoxList = document.getElementById(cbControl);
       var chkBoxCount= chkBoxList.getElementsByTagName("input");
        for(var i=0;i<chkBoxCount.length;i++)
        {
            chkBoxCount[i].checked = state;
        }
        return false; 
    }

    function printDiv(divName) 
    {
         var printContents = document.getElementById(divName).innerHTML;
         var originalContents = document.body.innerHTML;

         document.body.innerHTML = printContents;

         window.print();

         document.body.innerHTML = originalContents;
    }
    
    function ToUpper(ctrl)
         {   
            var t = ctrl.value;
            ctrl.value = t.toUpperCase(); 
         }
    </script>

    <div class="formStyle" style="width: 2200px;">
        <div id="formhead1">
            <div style="width: 98%; float: left;">
                IT Calculation Sheet</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="Div950">
            <asp:UpdateProgress Id="UpProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
                DisplayAfter="0">
                <progresstemplate>
                    <div style="position: absolute; visibility: visible; border: none; z-index: 100;
                        width: 100%; height: 100%; background: #999; filter: alpha(opacity=80); -moz-opacity: .8;
                        opacity: .8; text-align: center;">
                    
                    <img style="position: relative; top: 45%;" src="../../Images/photo-loader.gif" alt="" />
                    </div>
                </progresstemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <contenttemplate>
            <div class="MsgBox">
                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Fuchsia" Width="500px"></asp:Label>
            </div>
            <div style="background-color: #eff3fb">
                <fieldset>
                    <table>
                        <tbody>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                </td>
                                <td style="text-align: right">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="textlevel">
                                    Month :</td>
                                <td>
                                    <asp:DropDownList ID="ddlMonth" runat="server" Width="80px" CssClass="textlevelleft">
                                    </asp:DropDownList></td>
                                <td class="textlevel">
                                    Income Year :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFiscalYear" runat="server" Width="130px" CssClass="textlevelleft">
                                    </asp:DropDownList></td>
                                <td class="textlevel">
                                    Assessment Year :</td>
                                <td>
                                    <asp:TextBox ID="txtAssYear" runat="server" Width="100px"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ErrorMessage="*" ControlToValidate="txtAssYear"></asp:RequiredFieldValidator>
                                </td>
                                <td class="textlevel">
                                    Employee Type :</td>
                                <td class="textlevel">
                                     <asp:DropDownList ID="ddlEmpType" runat="server" Width="80px" CssClass="textlevelleft">
                                    </asp:DropDownList></td>
                                <td class="textlevel">
                                    Employee :</td>
                                <td>
                                    <asp:TextBox ID="txtEmpID" runat="server" Width="80px" onkeyup="ToUpper(this)"></asp:TextBox></td>
                                <td>
                                    <asp:ImageButton ID="imgBtnSearch" OnClick="imgBtnSearch_Click" runat="server" CausesValidation="False"
                                        Height="19px" Width="21px" ImageUrl="~/Images/Search_Icon.jpg"></asp:ImageButton></td>
                            </tr>
                            <tr>
                                <td class="textlevelshort">
                                </td>
                                <td colspan="3">
                                </td>
                                <td colspan="1">
                                </td>
                                <td colspan="1">
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
            </div>
            <div>
                <fieldset>
                    <legend>Entry</legend>
                    <table style="border-collapse: collapse">
                        <tbody>
                            <tr>
                                <td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    EmpCode</td>
                                <td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    Gender</td>
                                <td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    YBasicSalary</td>
                                <td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    YHouseRent</td>
                                <td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    T_HA</td>
                                <%--<td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    YMedicalAllow</td>--%>
                                <td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    YTransAllow</td>
                                <td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    T_TA</td>
                                <%--<td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    YFieldAllow</td>--%>
                                <td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    YFestivalBonus</td>
                                <td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    YOverTime</td>
                                <td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    TTI_1</td>
                                <td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    Rebate</td>
                                <td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    YPFDeduction</td>
                                <td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    TTI_2</td>
                                <td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    Z_M_F</td>
                                <td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    10%</td>
                                <td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    15%</td>
                                <td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    20%</td>
                                <td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    25%</td>
                                <td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    30%</td>
                                <td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    G_Tax</td>
                                <td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    Net_Tax</td>
                                <td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    ITDeposited</td>
                                <td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    Demand</td>
                                <td style="border-right: gray 1px solid; border-top: gray 1px solid; font-weight: bold;
                                    border-left: gray 1px solid; color: black; border-bottom: gray 1px solid; background-color: orange;
                                    text-align: center">
                                    Refund</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtEmpCode" runat="server" Width="70px" ReadOnly="true"></asp:TextBox>
                                    <%--<cc1:FilteredTextBoxExtender ID="Filteredtextboxextender1" runat="server" ValidChars=".,-"
                                        FilterType="Custom,Numbers" TargetControlID="txtEmpCode">
                                    </cc1:FilteredTextBoxExtender>--%>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtGender" runat="server" Width="70px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtYBasicSalary" runat="server" Width="70px" OnTextChanged="txtYBasicSalary_TextChanged"
                                        AutoPostBack="true"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fltbPayAmnt" runat="server" TargetControlID="txtYBasicSalary"
                                        FilterType="Custom,Numbers" ValidChars=".,-">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox Style="text-align: right" ID="txtYHouseRent" runat="server" Width="70px"
                                        OnTextChanged="txtYHouseRent_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fltbPayAmnt1" runat="server" TargetControlID="txtYHouseRent"
                                        FilterType="Custom,Numbers" ValidChars=".,-">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox Style="text-align: right" ID="txtT_HA" runat="server" Width="70px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fltbPayAmnt2" runat="server" TargetControlID="txtT_HA"
                                        FilterType="Custom,Numbers" ValidChars=".,-">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <%--<td>
                                    <asp:TextBox ID="txtYMedicalAllowance" style="text-align:right;" runat="server" Width="70px" AutoPostBack="true"
                                        OnTextChanged="txtYMedicalAllowance_TextChanged"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fltbPayAmnt3" runat="server" ValidChars=".,-" FilterType="Custom,Numbers"
                                        TargetControlID="txtYMedicalAllowance">
                                    </cc1:FilteredTextBoxExtender>
                                </td>--%>
                                <td>
                                    <asp:TextBox Style="text-align: right" ID="txtYTransportAllowance" runat="server"
                                        Width="70px" OnTextChanged="txtYTransportAllowance_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fltbPayAmnt4" runat="server" TargetControlID="txtYTransportAllowance"
                                        FilterType="Custom,Numbers" ValidChars=".,-">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox Style="text-align: right" ID="txtT_TA" runat="server" Width="70px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fltbPayAmnt5" runat="server" TargetControlID="txtT_TA"
                                        FilterType="Custom,Numbers" ValidChars=".,-">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <%--<td>
                                    <asp:TextBox ID="txtYFieldAllowance" style="text-align:right;" runat="server" Width="70px" AutoPostBack="true"
                                        OnTextChanged="txtYFieldAllowance_TextChanged"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fltbPayAmnt6" runat="server" ValidChars=".,-" FilterType="Custom,Numbers"
                                        TargetControlID="txtYFieldAllowance">
                                    </cc1:FilteredTextBoxExtender>
                                </td>--%>
                                <td>
                                    <asp:TextBox Style="text-align: right" ID="txtYFestivalBonus" runat="server" Width="70px"
                                        OnTextChanged="txtYFestivalBonus_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fltbPayAmnt7" runat="server" TargetControlID="txtYFestivalBonus"
                                        FilterType="Custom,Numbers" ValidChars=".,-">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox Style="text-align: right" ID="txtYOtherallowance" runat="server" Width="70px"
                                        OnTextChanged="txtYOtherallowance_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fltbPayAmnt8" runat="server" TargetControlID="txtYOtherallowance"
                                        FilterType="Custom,Numbers" ValidChars=".,-">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox Style="text-align: right" ID="txtTTI_1" runat="server" Width="70px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fltbPayAmnt9" runat="server" TargetControlID="txtTTI_1"
                                        FilterType="Custom,Numbers" ValidChars=".,-">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox Style="text-align: right" ID="txtRebate" runat="server" Width="70px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fltbPayAmnt10" runat="server" TargetControlID="txtRebate"
                                        FilterType="Custom,Numbers" ValidChars=".,-">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox Style="text-align: right" ID="txtYPFDeduction" runat="server" Width="70px"
                                        OnTextChanged="txtYPFDeduction_TextChanged"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fltbPayAmnt11" runat="server" TargetControlID="txtYPFDeduction"
                                        FilterType="Custom,Numbers" ValidChars=".,-">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox Style="text-align: right" ID="txtTTI_2" runat="server" Width="70px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fltbPayAmnt12" runat="server" TargetControlID="txtTTI_2"
                                        FilterType="Custom,Numbers" ValidChars=".,-">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox Style="text-align: right" ID="txtZ_M_F" runat="server" Width="70px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fltbPayAmnt13" runat="server" TargetControlID="txtZ_M_F"
                                        FilterType="Custom,Numbers" ValidChars=".,-">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox Style="text-align: right" ID="txtP10" runat="server" Width="70px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fltbPayAmnt14" runat="server" TargetControlID="txtP10"
                                        FilterType="Custom,Numbers" ValidChars=".,-">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox Style="text-align: right" ID="txtP15" runat="server" Width="70px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fltbPayAmnt15" runat="server" TargetControlID="txtP15"
                                        FilterType="Custom,Numbers" ValidChars=".,-">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox Style="text-align: right" ID="txtP20" runat="server" Width="70px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fltbPayAmnt16" runat="server" TargetControlID="txtP20"
                                        FilterType="Custom,Numbers" ValidChars=".,-">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox Style="text-align: right" ID="txtP25" runat="server" Width="70px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fltbPayAmnt18" runat="server" TargetControlID="txtP25"
                                        FilterType="Custom,Numbers" ValidChars=".,-">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox Style="text-align: right" ID="txtP30" runat="server" Width="70px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtP30"
                                        FilterType="Custom,Numbers" ValidChars=".,-">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox Style="text-align: right" ID="txtG_Tax" runat="server" Width="70px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fltbPayAmnt19" runat="server" TargetControlID="txtG_Tax"
                                        FilterType="Custom,Numbers" ValidChars=".,-">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox Style="text-align: right" ID="txtNetTax" runat="server" Width="70px"
                                        OnTextChanged="txtNetTax_TextChanged" AutoPostBack="True"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fltbPayAmnt20" runat="server" TargetControlID="txtNetTax"
                                        FilterType="Custom,Numbers" ValidChars=".,-">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox Style="text-align: right" ID="txtITDeposited" runat="server" Width="70px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fltbPayAmnt21" runat="server" TargetControlID="txtITDeposited"
                                        FilterType="Custom,Numbers" ValidChars=".,-">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox Style="text-align: right" ID="txtDemand" runat="server" Width="70px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fltbPayAmnt22" runat="server" TargetControlID="txtDemand"
                                        FilterType="Custom,Numbers" ValidChars=".,-">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox Style="text-align: right" ID="txtRefund" runat="server" Width="70px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="fltbPayAmnt23" runat="server" TargetControlID="txtRefund"
                                        FilterType="Custom,Numbers" ValidChars=".,-">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:Button ID="btnModify" OnClick="btnModify_Click" runat="server" Text="Modify List">
                                    </asp:Button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <fieldset>
                    <legend>Employee List</legend>
                    <asp:Button ID="btnPrint" runat="server" Text="Print" Height="30px" Width="120px"
                        OnClientClick="printDiv('PrintMe')"></asp:Button>
                    <asp:Button ID="btnExport" OnClick="btnExport_Click" runat="server" Font-Bold="true"
                        Text="Export to Excel" CausesValidation="false" Height="30px" Width="120px"></asp:Button>
                    <div style="margin-top: 10px" id="PrintMe">
                        <asp:GridView ID="grEmployee" runat="server" EmptyDataText="No Record Found" Font-Size="9px"
                            AutoGenerateColumns="False" DataKeyNames="EMPID,SALARYPAKID,PostingDivId,DEPTID,Staffname,Designation,TIN,MinimumTaxAmt"
                            OnRowCommand="grEmployee_RowCommand">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                            </SelectedRowStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                            <Columns>
                                <asp:ButtonField CommandName="DoubleClick" HeaderText="Select" Text="Select">
                                    <ItemStyle CssClass="ItemStylecss" Width="20px" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="" HeaderText="SL">
                                    <ItemStyle CssClass="ItemStylecss" Width="40px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMPCODE" HeaderText="EmpCode">
                                    <ItemStyle CssClass="ItemStylecss" Width="60px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Staffname" HeaderText="Staffname">
                                    <ItemStyle CssClass="ItemStylecss" Width="120px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Designation" HeaderText="Designation">
                                    <ItemStyle CssClass="ItemStylecss" Width="120px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TIN" HeaderText="TIN">
                                    <ItemStyle CssClass="ItemStylecss" Width="80px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Sex" HeaderText="Gender">
                                    <ItemStyle CssClass="ItemStylecss" Width="40px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DOB" HeaderText="DOB">
                                    <ItemStyle CssClass="ItemStylecss" Width="30px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="JoiningDate" HeaderText="JoiningDate">
                                    <ItemStyle CssClass="ItemStylecss" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="YBasicSalary" HeaderText="YBasicSalary" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="YHouseRent" HeaderText="YHouseRent" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="T_HA" HeaderText="T_HA" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                               <%-- <asp:BoundField DataField="YMedicalAllowance" HeaderText="YMedicalAllow" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="YTransportAllowance" HeaderText="YTransportAllow" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="T_TA" HeaderText="T_TA" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <%--<asp:BoundField DataField="YFieldAllowance" HeaderText="YFieldAllow" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="YFestivalBonus" HeaderText="YFestivalBonus" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="YOverTime" HeaderText="YOverTime" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TTI_1" HeaderText="TTI_1" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Rebate" HeaderText="Rebate" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="YPFDeduction" HeaderText="YPFDeduction" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TTI_2" HeaderText="TTI_2" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Z_M_F" HeaderText="Z_M_F" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="P10" HeaderText="P10" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="P15" HeaderText="P15" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="P20" HeaderText="P20" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="P25" HeaderText="P25" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="P30" HeaderText="P30" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="G_Tax" HeaderText="G_Tax" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="NetTax" HeaderText="NetTax" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="" HeaderText="Last Year Refund" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="" HeaderText="Monthly Tax" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ITDeposited" HeaderText="ITDeposited" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Demand" HeaderText="Demand" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Refund" HeaderText="Refund" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                 <asp:BoundField DataField="YMedicalAllowance" HeaderText="YMedicalAllow" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="T_MA" HeaderText="T_MA" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle CssClass="ItemStylecssRight" Width="70px"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </fieldset>
                <span style="font-weight: bold; font-size: 12px; color: #3366cc; font-family: Tahoma">
                    Total Records:
                    <asp:Label ID="lblRecordCount" runat="server" Text=""></asp:Label>
                </span>
            </div>
            <div style="padding-top: 3px" id="DivCommand1">
                <div style="float: left; text-align: left">
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CausesValidation="False"
                        Width="70px" OnClick="btnRefresh_Click"></asp:Button>
                    <asp:Button ID="btnUpdatePackage" OnClick="btnUpdatePackage_Click" runat="server"
                        Font-Bold="true" ForeColor="red" Text="Update Salary Package" CausesValidation="False"
                        Width="200px"></asp:Button>
                    <asp:Button ID="btnSaveInvestmentEmail" OnClick="btnSaveInvestmentEmail_Click" runat="server"
                        Font-Bold="true" ForeColor="Blue" Text="Prepare Breakdown of Investment" CausesValidation="False"
                        Width="241px"></asp:Button>
                </div>
                <div style="text-align: right">
                    <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" Width="70px"
                        UseSubmitBehavior="False"></asp:Button>
                </div>
            </div>
            </contenttemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
