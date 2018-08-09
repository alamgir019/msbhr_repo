<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="BonusAllowance.aspx.cs" Inherits="Payroll_Payroll_BonusAllowance" Title="Bonus Allowances" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js"></script>
    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js"></script>
    <div class="formStyle">
        <div id="formhead1">
            <div style="width: 98%; float: left;">
                Bonus Allowance</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="Div950" style="margin-left: 10px; margin-right: 10px;">
            <div style="background-color: #EFF3FB; margin-bottom: 5px;">
                <fieldset>
                    <table>
                        <tr>
                            <td class="textlevelleft">
                                Religion
                            </td>
                             <td class="textlevelleft">
                                Festival
                            </td>
                            <td class="textlevelleft">
                                Fiscal Year
                            </td>
                            <td class="textlevelleft">
                                Fiscal Year (Tax) </td>                            
                            <td class="textlevelleft">
                                Month
                            </td>
                            <td class="textlevelleft">
                                Year
                            </td>
                            <td class="textlevelleft">
                                Festival Date
                            </td>
                            <td class="textlevelleft">
                                Employee Type</td>
                            <td>
                                </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlReligion" runat="server" Width="120px" 
                                    CssClass="textlevelleft" AutoPostBack="True" 
                                    onselectedindexchanged="ddlReligion_SelectedIndexChanged">                                    
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFestival" runat="server" Width="120px" CssClass="textlevelleft">                                    
                                </asp:DropDownList>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                    ErrorMessage="*" ControlToValidate="ddlFestival"
                                        Operator="NotEqual" ValueToCompare="99999"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFiscalYear" runat="server" Width="150px" CssClass="textlevelleft">
                                </asp:DropDownList>
                            </td>
                            <td>
                            <asp:DropDownList ID="ddlFiscalYearTax" runat="server" Width="130px" CssClass="textlevelleft">
                            </asp:DropDownList>
                            </td>
                            <td> 
                                <asp:DropDownList ID="ddlMonth" runat="server" Width="100px" CssClass="textlevelleft">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlYear" runat="server" Width="100px" CssClass="textlevelleft">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFestivalDate" runat="server" Width="80px"></asp:TextBox>
                                <a href="javascript:NewCal('<%= txtFestivalDate.ClientID %>','ddmmyyyy')">
                                    <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                        height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>
                            </td>

                            <%--<td>
                                <asp:DropDownList ID="ddlBenefitHead" runat="server" Width="120px" CssClass="textlevelleft">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTimes" runat="server" Width="80px" Text="1"></asp:TextBox>
                            </td>--%>

                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Button ID="btnGenerate" runat="server" Text="Generate" Width="80px" OnClick="btnGenerate_Click" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <fieldset>
                <asp:GridView ID="grEmployee" runat="server" EmptyDataText="No Record Found" Font-Size="9px"
                    Width="98%" AutoGenerateColumns="False" DataKeyNames="JOININGDATE,SeparateDate,EMPTYPEID,VMONTH,VYEAR,FISCALYRID,ISPRORATA,PRCENT,RELIGIONId">
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
                            <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="JobTitleName" HeaderText="Job Title">
                            <ItemStyle CssClass="ItemStylecss" Width="18%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="SalLocName" HeaderText="Location">
                            <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="JOININGDATE" HeaderText="Joining">
                            <ItemStyle CssClass="ItemStylecss" Width="8%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="TYPENAME" HeaderText="Employee Type">
                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ISPRORATA" HeaderText="Is Prorata" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle CssClass="ItemStylecssCenter" Width="8%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="BasicSalary" HeaderText="Basic" HeaderStyle-HorizontalAlign="Right">
                            <ItemStyle CssClass="ItemStylecssRight" Width="5%"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Bonus" HeaderStyle-HorizontalAlign="Right">
                            <ItemStyle Width="6%" HorizontalAlign="right" CssClass="ItemStylecssRight"/>
                            <ItemTemplate>
                                <asp:TextBox ID="txtBonus" Text='<%# Convert.ToString(Eval("BONUS")) %>' runat="server"
                                    CssClass="TextBoxAmt60" Width="90%"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="fltbBonus" runat="server" TargetControlID="txtBonus"
                                    FilterType="Custom,Numbers" ValidChars=".,-">
                                </cc1:FilteredTextBoxExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PRORATADAYS" HeaderText="Prorata Months" HeaderStyle-HorizontalAlign="Right">
                            <ItemStyle CssClass="ItemStylecssRight" Width="5%"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                <span style="color: #3366CC; font-size: 12px; font-family: Tahoma; font-weight: bold;">
                    Total Head Count:
                    <asp:Label ID="lblRecordCount" runat="server" Text=""></asp:Label>
                </span>
            </fieldset>
            <div id="DivCommand1" style="padding-top: 3px;">
                <div style="text-align: left; float: left">
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px" CausesValidation="False"
                        OnClick="btnRefresh_Click" />
                </div>
                <div style="text-align: right;">
                    <asp:Button ID="btnSaveDisburse" runat="server" Text="Save &amp; Disburse" 
                        Width="143px" UseSubmitBehavior="False"
                        OnClick="btnSaveDisburse_Click" style="margin-left: 0px" />
                    <asp:Button ID="btnSave" runat="server" Text="Save for Payroll" Width="143px" UseSubmitBehavior="False"
                        OnClick="btnSave_Click" style="margin-left: 0px" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                        OnClick="btnDelete_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
