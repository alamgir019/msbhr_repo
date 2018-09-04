<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="PayrollArrearImportTool.aspx.cs" Inherits="Payroll_Payroll_PayrollArrearImportTool"
    Title="Payroll Arrear Import Tool" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
    </script>

    <div class="formStyle">
        <div id='formhead1'>
            <div style="width: 94%; float: left;">
                Payroll Arrear Import Tool</div>
           <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../File/Home.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%"
            Height="400px">
            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2" Width="90%">
                <HeaderTemplate>
                    Arrear Import
                </HeaderTemplate>
                <ContentTemplate>
                    <fieldset>
                        <table width="95%">
                            <tr>
                                <td class="textlevelleft">
                                    <asp:Label ID="Label5" runat="server" Text="Arrear Case"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlArrearCase" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlArrearCase_SelectedIndexChanged">
                                        <asp:ListItem Value="-1">Select</asp:ListItem>
                                        <asp:ListItem Value="6">Appraisal</asp:ListItem>
                                        <asp:ListItem Value="4">Promotion</asp:ListItem>
                                       <%-- <asp:ListItem Value="3">LWOP</asp:ListItem>--%>
                                        <asp:ListItem Value="5">Salary Increment</asp:ListItem>
                                        <%--<asp:ListItem Value="2">Previous Month Joining</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </td>
                                <td class="textlevelleft">
                                    <asp:Label ID="lblArrearMonth" runat="server" Text="Arrear Month"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlArrearMonth" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="textlevelleft">
                                    <asp:Label ID="Label4" runat="server" Text="Arrear Year"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlYear" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="textlevelleft">
                                    <asp:Label ID="Label3" runat="server" Text="Fiscal Year"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFiscalYr" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="textlevelleft">
                                    <asp:Label ID="Label1" runat="server" Text="Salary Process Month"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMonth" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 82px">
                                    <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td class="textlevelleft">
                                    <asp:Label ID="lblJoiningMonth" runat="server" Text="Joining Month" Visible="False"></asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlJoiningMonth" runat="server" Visible="False">
                                    </asp:DropDownList></td>
                                <td class="textlevelleft">
                                </td>
                                <td>
                                </td>
                                <td class="textlevelleft">
                                </td>
                                <td>
                                </td>
                                <td class="textlevelleft">
                                </td>
                                <td>
                                </td>
                                <td class="textlevelleft">
                                </td>
                                <td>
                                </td>
                                <td style="width: 82px">
                                </td>
                            </tr>
                        </table>
                        <div class="setupGrid450">
                            <asp:GridView ID="grPayrollArrear" runat="server" AutoGenerateColumns="False" DataKeyNames="EmpId"
                                EmptyDataText="No Record Found" Font-Size="9px" Width="98%"><AlternatingRowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:BoundField HeaderText="Sl No">
                                        <ItemStyle CssClass="ItemStylecss" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EmpID" HeaderText="EmpID">
                                        <ItemStyle CssClass="ItemStylecss" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FullName" HeaderText="Name">
                                        <ItemStyle CssClass="ItemStylecss" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="JoiningDate" HeaderText="Joining Date">
                                        <ItemStyle CssClass="ItemStylecss" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SHeadId" HeaderText="Head Id">
                                        <ItemStyle CssClass="ItemStylecss" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="HeadName" HeaderText="Salary Head">
                                        <ItemStyle CssClass="ItemStylecss" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="vDays" HeaderText="Arrear Days">
                                        <ItemStyle CssClass="ItemStylecssRight" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Pay Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPayAmnt" runat="server" CssClass="TextBoxAmt100" Text='<%# Convert.ToString(Eval("PAYAMT")) %>'></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="fltbPayAmnt" runat="server" TargetControlID="txtPayAmnt"
                                                FilterType="Custom,Numbers" ValidChars=".,-">
                                            </cc1:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle Width="20%" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ValidFrom" HeaderText="Valid From">
                                        <ItemStyle CssClass="ItemStylecssCenter" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ValidTo" HeaderText="Valid To">
                                        <ItemStyle CssClass="ItemStylecssCenter" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                                <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                            </asp:GridView>
                            <asp:Label ID="lblArrearDtls" runat="server" Text="Arrear Details for Promotion/Salary Amendment/APA Case" Visible="False"></asp:Label>
                            <asp:GridView ID="grArrrearDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="EmpId"
                                EmptyDataText="No Record Found" Font-Size="9px" Width="98%">
                                <AlternatingRowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:BoundField HeaderText="Sl No">
                                        <ItemStyle CssClass="ItemStylecss" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EmpID" HeaderText="EmpID">
                                        <ItemStyle CssClass="ItemStylecss" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FullName" HeaderText="Name">
                                        <ItemStyle CssClass="ItemStylecss" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="JoiningDate" HeaderText="Joining Date">
                                        <ItemStyle CssClass="ItemStylecss" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SHeadId" HeaderText="Head Id">
                                        <ItemStyle CssClass="ItemStylecss" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="HeadName" HeaderText="Salary Head">
                                        <ItemStyle CssClass="ItemStylecss" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="vDays" HeaderText="Arrear Days">
                                        <ItemStyle CssClass="ItemStylecssRight" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Pay Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPayAmnt" runat="server" CssClass="TextBoxAmt100" Text='<%# Convert.ToString(Eval("PAYAMT")) %>'></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="fltbPayAmnt" runat="server" TargetControlID="txtPayAmnt"
                                                FilterType="Custom,Numbers" ValidChars=".,-">
                                            </cc1:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle Width="20%" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ValidFrom" HeaderText="Valid From">
                                        <ItemStyle CssClass="ItemStylecssCenter" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ValidTo" HeaderText="Valid To">
                                        <ItemStyle CssClass="ItemStylecssCenter" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                                <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                            </asp:GridView>
                        </div>
                    </fieldset>
                    <div class="DivCommand1">
                        <div class="DivCommandL">
                            <asp:Button ID="btnClear" runat="server" Text="Refresh" Width="70px" OnClick="btnClear_Click"
                                UseSubmitBehavior="False" />
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClick="btnDelete_Click"
                                UseSubmitBehavior="False" />
                        </div>
                        <div class="DivCommandR">
                            <asp:Button ID="btnArrearSave" runat="server" Text="Save" Width="70px" OnClick="btnArrearSave_Click" />
                        </div>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3" Width="90%">
                <HeaderTemplate>
                    Payroll Arrear List
                </HeaderTemplate>
                <ContentTemplate>
                    <table>
                        <tr>
                         <td class="textlevelleft">
                                <asp:Label ID="Label6" runat="server" Text="Salary Process Month"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlMonthSearch" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td class="textlevelleft">
                                <asp:Label ID="Label2" runat="server" Text="Fiscal Year"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFiscalYrSearch" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 82px">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                    <div class="setupGrid450">
                            <asp:GridView ID="grList" runat="server" AutoGenerateColumns="False" DataKeyNames="EmpId"
                                EmptyDataText="No Record Found" Font-Size="9px" Width="98%">
                                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                                <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:BoundField HeaderText="Sl No">
                                        <ItemStyle CssClass="ItemStylecss" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EmpID" HeaderText="EmpID">
                                        <ItemStyle CssClass="ItemStylecss" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FullName" HeaderText="Name">
                                        <ItemStyle CssClass="ItemStylecss" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="JoiningDate" HeaderText="Joining Date">
                                        <ItemStyle CssClass="ItemStylecss" />
                                    </asp:BoundField> 
                                    <asp:BoundField DataField="ArrearMonth" HeaderText="Arrear Month">
                                        <ItemStyle CssClass="ItemStylecss" />
                                    </asp:BoundField>  
                                     <asp:BoundField DataField="ArrearCase" HeaderText="Arrear Case">
                                        <ItemStyle CssClass="ItemStylecss" />
                                    </asp:BoundField>                                                               
                                    <asp:BoundField DataField="HeadName" HeaderText="Salary Head">
                                        <ItemStyle CssClass="ItemStylecss" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="vDays" HeaderText="Arrear Days">
                                        <ItemStyle CssClass="ItemStylecssRight" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PayAmt" HeaderText="Arrear Amount">
                                        <ItemStyle CssClass="ItemStylecssRight" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ValidFrom" HeaderText="Valid From">
                                        <ItemStyle CssClass="ItemStylecssCenter" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ValidTo" HeaderText="Valid To">
                                        <ItemStyle CssClass="ItemStylecssCenter" />
                                    </asp:BoundField>                                   
                                </Columns>
                            </asp:GridView>
                        </div>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </div>
</asp:Content>
