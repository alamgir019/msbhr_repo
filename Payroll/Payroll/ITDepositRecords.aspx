<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="ITDepositRecords.aspx.cs" Inherits="Payroll_Payroll_ITDepositRecords"
    Title="IT Deposit Records" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js"></script>

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
    </script>

    <div class="formStyle">
        <div id="formhead1">
            <div style="width: 98%; float: left;">
                IT Deposit Records</div>
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
                            Select 
                            Cost Center :</td>
                        <td style="">
                            <asp:DropDownList ID="ddlGenerateValue" runat="server" CssClass="textlevelleft" Width="270px">
                            </asp:DropDownList></td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" CssClass="textlevelleft" runat="server" Width="100px"
                                onchange="MonthChanged()">
                            </asp:DropDownList></td>
                        <td class="textlevel">
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="textlevelleft" Width="80px">
                            </asp:DropDownList></td>
                        <td>
                            <asp:DropDownList ID="ddlFinYear" CssClass="textlevelleft" runat="server" Width="184px">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="textlevel"> Employee Type :
                        </td>
                        <td>
                             <asp:DropDownList ID="ddlEmpType" runat="server" Width="116px" 
                                 CssClass="textlevelleft" Height="18px">
                                    </asp:DropDownList></td>
                        <td colspan="2">
                            <asp:Button ID="btnGetEmployee" runat="server" Text="Get IT Records" Width="184px"
                                OnClick="btnGetEmployee_Click" />
                        </td>                        
                        <td>
                            <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export to Excel" /></td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend>Employee List</legend>Clear <a id="A2" href="#" onclick="javascript: CheckBoxListSelect ('<%= grEmployee.ClientID %>',false)">
                    All</a>
                <div style="width: 99.99%;">
                    <asp:GridView ID="grEmployee" runat="server" EmptyDataText="No Record Found" Font-Size="9px"
                        Width="98%" AutoGenerateColumns="False" DataKeyNames="EMPID,SalLocId" OnRowCommand="grEmployee_RowCommand"
                        ShowFooter="true">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <FooterStyle BackColor="#B3CDE4" Font-Bold="True"></FooterStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:BoundField DataField="" HeaderText="SL">
                                <ItemStyle CssClass="ItemStylecss" Width="3%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="EMPID" HeaderText="Emp. ID">
                                <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FULLNAME" HeaderText="Employee Name">
                                <ItemStyle CssClass="ItemStylecss" Width="17%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SalLocName" HeaderText="Cost Center">
                                <ItemStyle CssClass="ItemStylecss" Width="16%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JobTitleName" HeaderText="Job Title">
                                <ItemStyle CssClass="ItemStylecss" Width="16%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TINNO" HeaderText="TIN">
                                <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PayAmt" HeaderText="Amount" HeaderStyle-HorizontalAlign="right">
                                <ItemStyle CssClass="ItemStylecssRight" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:ButtonField HeaderText="View" Text="Payslip" CommandName="ViewClick">
                                <ItemStyle Width="5%" CssClass="ItemStylecss"></ItemStyle>
                            </asp:ButtonField>
                        </Columns>
                    </asp:GridView>
                </div>
                Clear <a id="A4" href="#" onclick="javascript: CheckBoxListSelect ('<%= grEmployee.ClientID %>',false)">
                    All</a>
            </fieldset>
            <br />
            <fieldset>
                <table>
                    <tr>
                        <td class="textlevel">
                            Challan No :</td>
                        <td>
                            <asp:TextBox ID="txtChallanNo" runat="server" Width="100px"></asp:TextBox></td>
                        <td class="textlevel">
                            Bank :</td>
                        <td>
                            <asp:TextBox ID="txtBank" runat="server" Width="250px"></asp:TextBox></td>
                        <td class="textlevel">
                            Deposit Date :</td>
                        <td>
                            <asp:TextBox ID="txtDepositDate" runat="server" Width="100px"></asp:TextBox></td>
                        <td>
                            <a href="javascript:NewCal('<%= txtDepositDate.ClientID %>','ddmmyyyy')">
                                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                    height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a></td>
                    </tr>
                </table>
            </fieldset>
            <div style="text-align: center; margin-top: 5px;">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="120px" Font-Italic="True"
                    OnClick="btnSave_Click" />
            </div>
        </div>
    </div>
</asp:Content>
