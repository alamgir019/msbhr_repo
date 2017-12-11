<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="SalaryCharging.aspx.cs" Inherits="SOF_SalaryCharging" Title="Salary Charging" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js">
        //Date Time Picker script
    </script>
    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js">
    </script>
    <script language="javascript" type="text/javascript">
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }
    </script>
    <div class="empTrainForm">
        <div id="formhead1">
            <div style="width: 97%; float: left;">
                Salary Charging for Permanent Employee</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="PayrollConfigInner">
            <div style="background-color: #EFF3FB; margin-bottom: 10px;">
                <fieldset>
                    <table>
                        <tr>
                            <td class="textlevel">
                                Emp Code :
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmpID" runat="server" MaxLength="20" onkeyup="ToUpper(this)"
                                    Width="80px" ToolTip="Enter the Emp. Code"></asp:TextBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgBtnSearch" runat="server" Height="19px" ImageUrl="~/Images/Search_Icon.jpg"
                                    OnClick="imgBtnSearch_Click" ToolTip="Click on find button to retrieve any existing employee information." />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="ReqFieldVal" runat="server" ErrorMessage="*" ControlToValidate="txtEmpID"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td class="textlevel">
                                Name :
                            </td>
                            <td>
                                <asp:Label ID="lblName" runat="server"></asp:Label>
                            </td>
                            <td class="textlevel" style="width: 138px">
                                Company Name :
                            </td>
                            <td style="width: 31px">
                                <asp:Label ID="lblCompany" runat="server" Width="200px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Designation :
                            </td>
                            <td>
                                <asp:Label ID="lblDesig" runat="server"></asp:Label>
                            </td>
                           <%-- <td class="textlevel" style="width: 138px">
                                Posting District :
                            </td>
                            <td style="width: 31px">
                                <asp:Label ID="lblDistrict" runat="server"></asp:Label>
                            </td>--%>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Department :
                            </td>
                            <td>
                                <asp:Label ID="lblDept" runat="server"></asp:Label>
                            </td>
                            <td class="textlevel" style="width: 138px">
                                Salary Location :
                            </td>
                            <td style="width: 31px">
                                <asp:Label ID="lblLocation" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Clinic :
                            </td>
                            <td>
                                <asp:Label ID="lblClinic" runat="server"></asp:Label>
                            </td>
                            <%--<td class="textlevel" style="width: 138px">
                                Salary Sub-Location :
                            </td>
                            <td style="width: 31px">
                                <asp:Label ID="lblSubLocation" runat="server"></asp:Label>
                            </td>--%>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Basic :
                            </td>
                            <td>
                                <asp:Label ID="lblBasic" runat="server"></asp:Label>
                            </td>
                            <td class="textlevel" style="width: 138px">
                            </td>
                            <td style="width: 31px">
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <fieldset style="margin-bottom: 10px;">
                <table>
                    <tr>
                        <td class="textlevel">
                            Entry Date :
                        </td>
                        <td>
                            <asp:TextBox ID="txtEntryDate" runat="server" Width="80px"
                                ToolTip="Input the from date of temporary duty."></asp:TextBox>
                            <a href="javascript:NewCal('<%= txtEntryDate.ClientID %>','ddmmyyyy')">
                                <img style="border: 0px;" height="16" alt="Pick a date" src="../images/cal.gif"
                                    width="16" /></a>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEntryDate"
                                CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Project Name :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSalarySource" runat="server" AutoPostBack="True" CssClass="textlevelleft"
                                Width="300px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Percentage :
                        </td>
                        <td>
                            <asp:TextBox ID="txtPercentage" runat="server" MaxLength="20" Width="80px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                        </td>
                        <td>
                            <asp:CheckBox ID="ChkIsActive" runat="server" CssClass="textlevelleft" Text="Is Active" />
                        </td>
                    </tr>
                    </table>
                    <table>
                    <tr>
                        <td class="textlevel">
                            <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" Text="Refresh"
                                Width="70px" OnClick="btnRefresh_Click" ToolTip="Click this button to clear all fields." />
                        </td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Add to List" Width="70px" OnClick="btnSave_Click"
                                ToolTip="Click this button to store the information after providing all necessary fields." />
                            <asp:Button ID="Button1" runat="server" Text="Print" Width="70px" OnClick="btnSave_Click"
                                ToolTip="Click this button to store the information after providing all necessary fields." />
                        </td>
                      <%--  <td style="width: 59px">
                            <asp:Button ID="Button2" runat="server" Text="First" Width="70px" OnClick="btnSave_Click"
                                ToolTip="Click this button to store the information after providing all necessary fields." />
                        </td>
                        <td style="width: 59px">
                            <asp:Button ID="Button3" runat="server" Text="Prev." Width="70px" OnClick="btnSave_Click"
                                ToolTip="Click this button to store the information after providing all necessary fields." />
                        </td>
                        <td style="width: 59px">
                            <asp:Button ID="Button4" runat="server" Text="Next" Width="70px" OnClick="btnSave_Click"
                                ToolTip="Click this button to store the information after providing all necessary fields." />
                        </td>
                        <td style="width: 59px">
                            <asp:Button ID="Button5" runat="server" Text="Last" Width="70px" OnClick="btnSave_Click"
                                ToolTip="Click this button to store the information after providing all necessary fields." />
                        </td>--%>
                    </tr>
                </table>
                <table>
                    </table>
                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                <asp:HiddenField ID="hfID" runat="server" />
            </fieldset>
            <fieldset style="margin-bottom: 10px;">
                <legend>List</legend>
                <div style="overflow: scroll; width: 100%; height: 250px">
                    <asp:GridView ID="grList" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                        AutoGenerateColumns="False" DataKeyNames="SalChargeId,SalarySourceId" onrowcommand="grList_RowCommand">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                <ItemStyle Width="5%" CssClass="ItemStylecss" />
                            </asp:ButtonField>
                            <asp:ButtonField CommandName="RowDeleting" HeaderText="Delete" Text="Delete">
                                <ItemStyle Width="5%" CssClass="ItemStylecss" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="ProjectCode" HeaderText="Project Code">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ProjectName" HeaderText="Project Name">
                                <ItemStyle CssClass="ItemStylecss" Width="50%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Percentage" HeaderText="Percentage">
                                <ItemStyle CssClass="ItemStylecssRight" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="IsActive" HeaderText="Active">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="EntryDate" HeaderText="Entry Date">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>                            
                        </Columns>
                    </asp:GridView>
                </div>
            </fieldset>
            <table>
                <tr>
                    <td class="textlevel">
                        Total Charging :
                    </td>
                    <td>
                        <asp:TextBox ID="txtTotalCharge" runat="server" MaxLength="20" Width="80px" 
                            Enabled="False" CssClass="TextBoxAmt60"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <br />
    <br />
</asp:Content>
