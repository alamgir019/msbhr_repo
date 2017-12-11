<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="EmpSeparation.aspx.cs" Inherits="EIS_HRAction_EmpSeparation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
    <script src="../../JScripts/jquery-1.2.3.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../JScripts/ui.datepicker.js">
    </script>
    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js">
        //Date Time Picker script
    </script>
    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js">
        //Delete Confirmation Message
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
                Employee Separation</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
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
                                    Width="80px" ToolTip="Type an Emp code and press ‘Enter’ or click on Find Image"></asp:TextBox>
                                <td>
                                    <asp:ImageButton ID="imgBtnSearch" runat="server" Height="19px" ImageUrl="~/Images/Search_Icon.jpg"
                                        OnClick="imgBtnSearch_Click" ToolTip="Find Image" CausesValidation="False" />
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
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Organization :
                            </td>
                            <td>
                                <asp:Label ID="lblOffice_Loc" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Designation :
                            </td>
                            <td>
                                <asp:Label ID="lblDeg_Project" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel" style="height: 16px">
                                Department :</td>
                            <td style="height: 16px">
                                <asp:Label ID="lblDept" runat="server"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td class="textlevel" style="height: 16px">
                                Sub Department :</td>
                            <td style="height: 16px">
                                <asp:Label ID="lblSubDept" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel" style="height: 16px">
                                Subcode :</td>
                            <td style="height: 16px">
                                <asp:Label ID="lblSuncode" runat="server"></asp:Label>
                            </td>
                        </tr>  
                        <tr>
                            <td class="textlevel">
                                Joining Date :
                            </td>
                            <td>
                                <asp:Label ID="lblJoiningDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <fieldset style="margin-bottom: 10px;">
                <legend>Separation Details</legend>
                <table>
                    <tr>
                        <td class="textlevel">
                            Separation Mode :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAction" runat="server" Width="300px" ToolTip="Select a ‘Separation Mode’">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Separation Date :
                        </td>
                        <td>
                            <asp:TextBox ID="txtSepDate" runat="server" Width="80px" ToolTip="Select a date by clicking on the calendar image or type a date in this order ‘dd/mm/yyyy’."></asp:TextBox>
                            <a href="javascript:NewCal('<%= txtSepDate.ClientID %>','ddmmyyyy')">
                                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                    height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Width="60px"
                                ErrorMessage="Invalid Date" ControlToValidate="txtSepDate" CssClass="validator"
                                ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtSepDate"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Previous Work Duration :
                        </td>
                        <td>
                            <asp:TextBox ID="txtPWD" runat="server" Width="300px"></asp:TextBox>
                            <asp:Button ID="btnCalculate" runat="server" OnClick="btnCalculate_Click" Text="Calculate" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Re-Hired Status :
                        </td>
                        <td>
                            <asp:TextBox ID="txtReHire" runat="server" Width="300px" ToolTip="Status of re-hired can be input here."></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Re-Hired Status Cause :
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtReHCause" runat="server" TextMode="MultiLine" Width="500px" ToolTip="Input the cause of re-hired status here."></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                <asp:HiddenField ID="hfSepId" runat="server" />
                <asp:HiddenField ID="hfSLYear" runat="server" />
                <asp:HiddenField ID="hfSLMonth" runat="server" />
                <asp:HiddenField ID="hfSLDay" runat="server" />
                <asp:HiddenField ID="hfTotDays" runat="server" />
                <asp:HiddenField ID="hfLPakId" runat="server" />
            </fieldset>
            <fieldset>
                <legend>Separation List</legend>
                <div style="overflow: scroll; width: 98%; height: 250px">
                    <asp:GridView ID="grEmpSep" runat="server" Width="97%" Font-Size="9px" EmptyDataText="No Record Found"
                        AutoGenerateColumns="False" DataKeyNames="SeparationId,ActionId" OnRowCommand="grEmpSep_RowCommand"
                        ToolTip="Displays the separation log for selected employee" 
                        onselectedindexchanged="grEmpSep_SelectedIndexChanged">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                <ItemStyle Width="5%" CssClass="ItemStylecss" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="ActionName" HeaderText="Separation Mode">
                                <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SeparationDate" HeaderText="Separation Date">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PrevWorkDuration" HeaderText="Prev. Work Duration">
                                <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ReHiredStatus" HeaderText="Re-Hired Status">
                                <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ReHiredCause" HeaderText="Re-Hired Cause">
                                <ItemStyle CssClass="ItemStylecss" Width="25%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
        <div class="DivCommand1" style="width: 92%;">
            <div class="DivCommandL">
                <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" Text="Refresh"
                    Width="70px" OnClick="btnRefresh_Click" ToolTip="To set the page as initial stage, click on this button." />
            </div>
            <div class="DivCommandR">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click"
                    OnClientClick="javascript:return HistorySaveConfirmation();" ToolTip="Click to Save/Edit above informations." />
            </div>
        </div>
    </div>
</asp:Content>
