<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="BaseSalaryPackage.aspx.cs" Inherits="Payroll_SalaryPackage_BaseSalaryPackage"
    Title="Base Salary Package" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js"></script>
    <div id="PayrollConfigForm2">
        <div id="formhead1">
            Employee Salary Package Setup
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="PayrollConfigInner2">
            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="590px">
                <cc1:TabPanel runat="server" Height="570px" ID="TabPanel1" HeaderText="Salary Package Setup">
                    <ContentTemplate>
                        <fieldset>
                            <table>
                                <tbody>
                                    <tr>
                                        <td class="textlevel">
                                            Package Title
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSalPackTitle" runat="server" Width="255px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlSalPackTitle"
                                                ErrorMessage="*" Operator="NotEqual" ValueToCompare="-1"></asp:CompareValidator>
                                        </td>
                                        <td class="textlevel">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 26px" class="textlevel">
                                            Description
                                        </td>
                                        <td style="height: 26px">
                                            <asp:TextBox ID="txtDescription" runat="server" OnTextChanged="txtDescription_TextChanged"
                                                Width="250px"></asp:TextBox>
                                        </td>
                                        <td style="height: 26px">
                                        </td>
                                        <td colspan="2" style="height: 26px">
                                            &nbsp;<asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                        </td>
                                        <td class="textlevel">
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                        </td>
                                        <td colspan="4">
                                            <asp:CheckBox ID="chkInActive" runat="server" CssClass="textlevelauto" Text="Make Inactive ">
                                            </asp:CheckBox>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <br />
                            <asp:HiddenField ID="hfID" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hfIsUpdate" runat="server"></asp:HiddenField>
                            &nbsp;&nbsp;
                        </fieldset>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <fieldset>
                                    <legend>Define Package</legend>
                                    <hr />
                                    <div style="overflow: scroll; width: 98%; height: 220px">
                                        <asp:GridView ID="grSalHead" runat="server" Width="95%" __designer:wfdid="w69" Font-Size="9px"
                                            EmptyDataText="No Record Found" AutoGenerateColumns="false" DataKeyNames="SHEADID,ISPFUND,AMTCOMPAY,PERCNTFIELD"
                                            OnRowCommand="grSalHead_RowCommand">
                                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                            </SelectedRowStyle>
                                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                            <Columns>
                                                <asp:ButtonField CommandName="DoubleClick" HeaderText="Remove" Text="Remove">
                                                    <ItemStyle Width="5%" CssClass="ItemStylecss" />
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="HEADNAME" HeaderText="Salary Item">
                                                    <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ISINPERCENT" HeaderText="Type">
                                                    <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ISBASICSAL" HeaderText="Is Basic">
                                                    <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Value">
                                                    <ItemStyle Width="11%" HorizontalAlign="left" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPayAmnt" Text='<%# Convert.ToString(Eval("TOTAMNT")) %>' runat="server"
                                                            CssClass="TextBoxAmt100"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="fltbPayAmnt" runat="server" TargetControlID="txtPayAmnt"
                                                            FilterType="Custom,Numbers" ValidChars=".,-">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div style="width: 98%">
                                        <table style="width: 98%">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 15%" class="textlevel">
                                                        Net payable amount
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:TextBox ID="txtNetPayableAmt" runat="server" CssClass="TextBoxAmt80" __designer:wfdid="w70"
                                                            ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 50%" class="textlevel">
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:TextBox ID="txtNetAmountIn" runat="server" CssClass="TextBoxAmt80" __designer:wfdid="w71"
                                                            Visible="False" ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <hr />
                                </fieldset>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Panel ID="Panel1" runat="server">
                            <fieldset>
                                <div style="width: 98%">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td style="height: 26px" class="textlevel">
                                                    Overtime Amount/Hour
                                                </td>
                                                <td style="height: 26px">
                                                    <asp:TextBox ID="txtOTAmtPerHour" runat="server">0</asp:TextBox>
                                                </td>
                                                <td style="height: 26px" class="textlevelleft">
                                                    <asp:CheckBox ID="chkOTPercentOf" runat="server" Text="In (%) of" OnCheckedChanged="chkOTPercentOf_CheckedChanged">
                                                    </asp:CheckBox>
                                                </td>
                                                <td style="height: 26px">
                                                    <asp:DropDownList ID="ddlOTSalHead" runat="server" Width="156px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="height: 26px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textlevel">
                                                    Attendance Bonus Amount
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAttndBonusAmt" runat="server">0</asp:TextBox>
                                                </td>
                                                <td class="textlevelleft">
                                                    <asp:CheckBox ID="chkAttnBonusPercentOf" runat="server" Text="In (%) of"></asp:CheckBox>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAttnSalHead" runat="server" Width="156px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: auto" class="textlevelleft" colspan="2">
                                                    For
                                                    <asp:TextBox ID="txtDelay" runat="server" Width="22px" OnTextChanged="txtDelay_TextChanged">0</asp:TextBox>
                                                    day(s) delay, deduct
                                                    <asp:TextBox ID="txtDeduct" runat="server" Width="22px">0</asp:TextBox>
                                                    day(s) salary.
                                                </td>
                                                <td class="textlevel">
                                                    Deduct as
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlDeductHead" runat="server" Width="156px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left" class="textlevel">
                                                    Salary head
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textlevelleft">
                                                    <asp:CheckBox ID="chkCompanyFacility" runat="server" Text="Benefits Package "></asp:CheckBox>
                                                </td>
                                                <td colspan="3">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </fieldset>
                        </asp:Panel>
                    </ContentTemplate>
                    <HeaderTemplate>
                        Salary Package Setup
                    </HeaderTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel runat="server" Height="590px" ID="TabPanel2" HeaderText="Salary Package List">
                    <ContentTemplate>
                        <fieldset>
                            <table>
                                <tr>
                                    <td class="textlevel">
                                        <asp:CheckBox ID="chkShowAll" runat="server" Text="Show All Records" Checked="True"
                                            AutoPostBack="True" OnCheckedChanged="chkShowAll_CheckedChanged" />
                                    </td>
                                    <td class="textlevel">
                                        Package Title
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPackageTitleSearch" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" Width="70px" UseSubmitBehavior="False"
                                            OnClick="btnSearch_Click" CausesValidation="False" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <fieldset style="text-align: center;">
                            <div style="width: 99%; overflow: scroll; height: 530px;">
                                <asp:GridView ID="grPackageList" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                                    Font-Size="9px" Width="97%" OnRowCommand="grPackageList_RowCommand" DataKeyNames="SalPakId">
                                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                    </SelectedRowStyle>
                                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:ButtonField CommandName="RowEdit" HeaderText="Edit" Text="Edit">
                                            <ItemStyle Width="4%" CssClass="ItemStylecss" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="SPTITLE" HeaderText="Package Title">
                                            <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Fullname" HeaderText="Owner">
                                            <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DesigName" HeaderText="Designation">
                                            <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ClinicName" HeaderText="Cost Center">
                                            <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotalGrossSal" HeaderText="Gross Amount">
                                            <ItemStyle CssClass="ItemStylecss" Width="8%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TOTALSALARY" HeaderText="Net Pay Amount">
                                            <ItemStyle CssClass="ItemStylecss" Width="8%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ISACTIVE" HeaderText="Is Active">
                                            <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <asp:Label ID="lblPkgCount" runat="server" Font-Bold="True" ForeColor="#3366CC"></asp:Label>
                        </fieldset>
                    </ContentTemplate>
                    <HeaderTemplate>
                        Salary Package List
                    </HeaderTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <div id="DivCommand1" style="padding-top: 3px;">
                <div style="text-align: left; float: left">
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px" CausesValidation="False"
                        OnClick="btnRefresh_Click" />
                    <asp:Button ID="btnNew" runat="server" Text="Save as New SalPack" Width="160px" UseSubmitBehavior="False"
                        OnClick="btnNew_Click" />
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
