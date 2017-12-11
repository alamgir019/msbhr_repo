<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="PayrollMovementReport.aspx.cs" Inherits="Payroll_Payroll_PayrollMovementReport"
    Title="Payroll Movement Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js"></script>
    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js"></script>
    <script language="javascript" type="text/javascript">
        window.onload = SearchByChanged;
        function SearchByChanged() {
            var ddlSB = document.getElementById('<%= ddlGeneratefor.ClientID %>');
            var ddlSV = document.getElementById('<%=ddlGenerateValue.ClientID%>');
            var ddlB = document.getElementById('<%=ddlBank.ClientID%>');
            var txtVT = document.getElementById('<%=txtTextValue.ClientID%>');
            var txtlbl = document.getElementById('<%=lblEmpID.ClientID%>');


            var myindex = ddlSB.selectedIndex;
            var SelValue = ddlSB.options[myindex].value;

            if (SelValue == "O") {
                ddlSV.disabled = false;
                ddlB.disabled = true;
                txtVT.disabled = true;
                txtlbl.disabled = true;
            }
            if (SelValue == "E") {
                ddlSV.disabled = true;
                ddlB.disabled = true;
                txtVT.disabled = false;
                txtlbl.disabled = false;
            }
            if (SelValue == "A") {
                ddlSV.disabled = true;
                txtVT.disabled = true;
                txtlbl.disabled = true;
                ddlB.disabled = true;
            }
            if (SelValue == "B") {
                ddlSV.disabled = true;
                ddlB.disabled = false;
                txtVT.disabled = true;
                txtlbl.disabled = true;
            }
        }
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }
        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = printContents;
            window.print();
            document.body.innerHTML = originalContents;
        }
    </script>
    <div class="formStyle" style="width: 98%;">
        <div id="formhead1">
            <div style="width: 98%; float: left;">
                Payroll Movement Report</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" Width="500px" Font-Bold="True" ForeColor="Fuchsia"></asp:Label>
        </div>
        <div class="Div950">
            <fieldset>
                <table>
                    <tr>
                        <td class="textlevel">
                            Generate For :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlGeneratefor" runat="server" Width="130px" onchange="SearchByChanged()"
                                CssClass="textlevelleft">
                                <asp:ListItem Value="A">All</asp:ListItem>
                                <asp:ListItem Value="O">Location Wise</asp:ListItem>
                                <asp:ListItem Value="B">Bank Wise</asp:ListItem>
                                <asp:ListItem Value="E">Employee Wise</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlGenerateValue" runat="server" Width="270px" CssClass="textlevelleft">
                            </asp:DropDownList>
                            &nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" runat="server" Width="100px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlYear" runat="server" Width="80px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEmpID" runat="server" Text="Emp. ID" CssClass="textlevel"></asp:Label>
                        </td>
                        <td style="font-size: 11px">
                            <asp:TextBox ID="txtTextValue" runat="server" Width="80px" onkeyup="ToUpper(this)"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlBank" runat="server" Width="270px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td colspan="2">
                            <asp:Button ID="btnGenerate" runat="server" Text="Generate Payroll Movement" Width="184px"
                                OnClick="btnGenerate_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <hr style="border: solid 1px #3399FF;" />
            <div style="text-align: right;">
                <asp:Button ID="btnPrintTop" runat="server" Text="Print" OnClientClick="printDiv('PrintMe')" /></div>
            <fieldset>
                <div id="PrintMe" style="width: 99.99%; page-break-before: always; height: 100%;">
                    <div style="font-family: Arial; font-size: 20px; margin-left: 5px;">
                        Marie Stopes
                    </div>
                    <div style="font-family: Arial; font-size: 14px; margin-left: 5px;">
                        <asp:Label ID="lblGenerateFor" runat="server" Text=""></asp:Label>
                        <br />
                        Payroll Movement
                        <br />
                        <br />
                        <asp:Label ID="lblPayrollMonth" runat="server" Text=""></asp:Label>
                    </div>
                    <asp:GridView ID="grPayrollSummary" runat="server" EmptyDataText="No Record Found"
                        Font-Size="10px" Width="99%" Font-Names="Arial" AutoGenerateColumns="true" ShowFooter="true"
                        ShowHeader="true">
                        <HeaderStyle BackColor="lightgray" Font-Bold="True"></HeaderStyle>
                        <FooterStyle BackColor="lightgray" Font-Bold="True"></FooterStyle>
                        <RowStyle BorderStyle="solid" BorderWidth="1px" BorderColor="Black" />
                    </asp:GridView>
                    <div style="font-family: Arial; font-size: 14px; margin-left: 5px; margin-top: 10px;">
                        Movement Details
                    </div>
                    <asp:GridView ID="grDetails" runat="server" EmptyDataText="No Record Found" Font-Size="10px"
                        Width="99%" Font-Names="Arial" AutoGenerateColumns="true" ShowFooter="true" OnRowCommand="grPayslipMst_RowCommand">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <FooterStyle BackColor="#B3CDE4" Font-Bold="True"></FooterStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                    </asp:GridView>
                    <div style="font-family: Arial; font-size: 14px; margin-left: 5px; margin-top: 10px;">
                        Movement Validation
                    </div>
                    <asp:GridView ID="grResult" runat="server" EmptyDataText="No Record Found" Font-Size="10px"
                        Width="99%" Font-Names="Arial" AutoGenerateColumns="true" ShowFooter="false"
                        ShowHeader="true" BackColor="white" OnRowCommand="grPayslipMst_RowCommand">
                        <HeaderStyle BackColor="lightgray" Font-Bold="True"></HeaderStyle>
                        <FooterStyle BackColor="#B3CDE4" Font-Bold="True"></FooterStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                    </asp:GridView>
                    <table style="border-collapse: collapse; border: solid 1px DimGray; width: 99%; margin-top: 10px;
                        font-size: 10px; font-family: Arial;">
                        <tr style="">
                            <td style="width: 30%; border: solid 1px DimGray; vertical-align: top;">
                                <asp:Label ID="lblPrevHeadCount" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="width: 70%; border: solid 1px DimGray;">
                                <asp:Label ID="lblAbsent" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr style="border: solid 1px DimGray; border: solid 1px DimGray; vertical-align: top;">
                            <td style="width: 30%;">
                                <asp:Label ID="lblCurrHeadCount" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="width: 70%; border: solid 1px DimGray;">
                                <asp:Label ID="lblAddition" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr style="border-right: dimgray 1px solid; border-top: dimgray 1px solid; vertical-align: top;
                            border-left: dimgray 1px solid; border-bottom: dimgray 1px solid; background-color: Silver;">
                            <td style="width: 30%; text-align: left;">
                                <span style="font-size: 10pt"><strong>Other Details</strong></span>
                            </td>
                            <td style="border-right: dimgray 1px solid; border-top: dimgray 1px solid; border-left: dimgray 1px solid;
                                width: 70%; border-bottom: dimgray 1px solid">
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="grMoveText" runat="server" AutoGenerateColumns="False" ShowHeader="false"
                        EmptyDataText="" Font-Size="10px" DataKeyNames="EMPID" Font-Names="Arial" Width="99%"
                        ShowFooter="false">
                        <RowStyle Font-Size="10px" Font-Names="Arial" />
                        <Columns>
                            <asp:ButtonField HeaderText="" Text="" CommandName="ViewClick" HeaderStyle-Font-Underline="false"
                                ItemStyle-Font-Underline="false" ControlStyle-Font-Underline="false" ItemStyle-VerticalAlign="Middle">
                                <ItemStyle Width="2%" HorizontalAlign="Center" BorderColor="silver" Font-Underline="false"
                                    Font-Size="12px" Font-Bold="True"></ItemStyle>
                            </asp:ButtonField>
                            <asp:BoundField DataField="PAYID" HeaderText="" HeaderStyle-HorizontalAlign="left">
                                <ItemStyle Width="3%" HorizontalAlign="left" BorderColor="silver" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EMPLOYEE" HeaderText="" HeaderStyle-HorizontalAlign="left">
                                <ItemStyle Width="25%" HorizontalAlign="left" BorderColor="silver" />
                            </asp:BoundField>
                            <asp:BoundField DataField="REMARKS" HeaderText="" HeaderStyle-HorizontalAlign="left">
                                <ItemStyle Width="70%" HorizontalAlign="left" BorderColor="silver" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <div style="font-family: Arial; font-size: 14px; margin-left: 5px; margin-top: 10px;">
                        <asp:Label ID="lblLog" runat="server" Text="Movement Log"></asp:Label>
                    </div>
                    <asp:GridView ID="grPayrollPrevMonth" runat="server" EmptyDataText="No Record Found"
                        Font-Size="10px" Width="99%" Font-Names="Arial" AutoGenerateColumns="true" ShowFooter="true"
                        ShowHeader="true">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <FooterStyle BackColor="#B3CDE4" Font-Bold="True"></FooterStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                    </asp:GridView>
                    <asp:GridView ID="grPayroll" runat="server" EmptyDataText="No Record Found" Font-Size="10px"
                        Width="99%" Font-Names="Arial" AutoGenerateColumns="true" ShowFooter="true" OnRowCommand="grPayslipMst_RowCommand">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <FooterStyle BackColor="#B3CDE4" Font-Bold="True"></FooterStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                    </asp:GridView>
                </div>
            </fieldset>
            <div id="DivCommand1" style="padding-top: 3px;">
                <div style="text-align: left; float: left">
                    <asp:Button ID="btnShow" runat="server" Text="Show Movement Log" Width="140px" CausesValidation="False"
                        OnClick="btnShow_Click" />
                </div>
                <div style="text-align: right;">
                    <asp:Button ID="btnPrintBottom" runat="server" Text="Print" OnClientClick="printDiv('PrintMe')" />
                </div>
            </div>
            <asp:HiddenField ID="hfPSBID" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hfPayID" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hfEmpID" runat="server"></asp:HiddenField>
        </div>
    </div>
</asp:Content>
