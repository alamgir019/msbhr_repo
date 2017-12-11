<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="TempDutyAssign.aspx.cs" Inherits="EIS_HRAction_TempDutyAssign" Title="Temporary Duty Assignment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js">
    //Date Time Picker script
    </script>

    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js">
    </script>

    <script language="javascript" type="text/javascript">
    function ToUpper(ctrl)
    {   
    var t = ctrl.value;
    ctrl.value = t.toUpperCase(); 
    }
    </script>

    <div class="empTrainForm">
        <div id="formhead1">
            <div style="width: 97%; float: left;">
                Temporary Duty Assignment</div>
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
                                Emp Id :</td>
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
                                <asp:Label ID="lblName" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Designation :</td>
                            <td>
                                <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Organization :</td>
                            <td>
                                <asp:Label ID="lblCompany" runat="server"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td class="textlevel">
                                Project :
                            </td>
                            <td>
                                <asp:Label ID="lblSector" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Department :</td>
                            <td>
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
                    </table>
                </fieldset>
            </div>
            <fieldset style="margin-bottom: 10px;">
                <legend>Temporary Duty Details</legend>
                <table>
                    <tr>
                        <td class="textlevel">
                            Name of Action :</td>
                        <td style="width: 200px">
                            <asp:DropDownList ID="ddlAction" runat="server" Width="200px" CssClass="textlevelleft" ToolTip="Select the name of action">
                            </asp:DropDownList></td>
                        <td class="textlevel">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Organization :</td>
                        <td style="width: 200px">
                            <asp:DropDownList ID="ddlCompany" runat="server" Width="200px" 
                                CssClass="textlevelleft" 
                                ToolTip="Select the location of office of the employee.">
                            </asp:DropDownList></td>
                        <td class="textlevel">
                            Suncode :</td>
                        <td>
                            <asp:DropDownList ID="ddlClinic" runat="server" Width="200px" 
                                CssClass="textlevelleft" ToolTip="Select the designation of the employee.">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Project :</td>
                        <td style="width: 200px">
                            <asp:DropDownList ID="ddlProject" runat="server" Width="200px" 
                                CssClass="textlevelleft" ToolTip="Select the designation of the employee.">
                            </asp:DropDownList></td>
                        <td class="textlevel">
                            </td>
                        <td>
                           </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Department :</td>
                        <td style="width: 200px">
                            <asp:DropDownList ID="ddlDept" runat="server" Width="200px" 
                                CssClass="textlevelleft" ToolTip="Select the designation of the employee." 
                                AutoPostBack="True" onselectedindexchanged="ddlDept_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td class="textlevel">
                            Sub Department :</td>
                        <td>
                             <asp:DropDownList ID="ddlSubDept" runat="server" Width="200px" 
                                CssClass="textlevelleft">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Assignment :</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtAssignment" runat="server" TextMode="MultiLine" 
                                Width="503px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Strating Date :</td>
                        <td style="width: 200px">
                            <asp:TextBox ID="txtStartDate" runat="server" Width="80px" 
                                ToolTip="Input the from date of temporary duty."></asp:TextBox>
                            <a href="javascript:NewCal('<%= txtStartDate.ClientID %>','ddmmyyyy')">
                                <img style="border: 0px;" height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtStartDate"
                                CssClass="validator" ErrorMessage="Invalid Date" 
                                ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator></td>
                        <td class="textlevel">
                            Ending Date :</td>
                        <td>
                            <asp:TextBox ID="txtEndDate" runat="server" Width="80px" 
                                ToolTip="Input the to date of temporary duty."></asp:TextBox>
                            <a href="javascript:NewCal('<%= txtEndDate.ClientID %>','ddmmyyyy')">
                                <img style="border: 0px;" height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtEndDate"
                                CssClass="validator" ErrorMessage="Invalid Date" 
                                ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Percentage :</td>
                        <td style="width: 200px">
                            <asp:TextBox ID="txtPercentage" runat="server" MaxLength="20" onkeyup="ToUpper(this)"
                                ToolTip="Enter the Emp. Code" Width="80px" CssClass="TextBoxAmt60"></asp:TextBox></td>
                        <td class="textlevel">
                            Amount :</td>
                        <td>
                            <asp:TextBox ID="txtAmount" runat="server" MaxLength="20" onkeyup="ToUpper(this)"
                                ToolTip="Enter the Emp. Code" Width="80px" CssClass="TextBoxAmt60"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Supervisor Id :</td>
                        <td style="width: 200px">
                            <asp:TextBox ID="txtSupervisorId" runat="server" MaxLength="20" onkeyup="ToUpper(this)"
                                ToolTip="Enter the Emp. Code" Width="80px"></asp:TextBox>
                            <asp:ImageButton ID="imgBtnSuper" runat="server" Height="19px" ImageUrl="~/Images/Search_Icon.jpg"
                                    OnClick="imgBtnSuper_Click" 
                                ToolTip="Click on find button to retrieve any existing employee information." /></td>
                        <td class="textlevel">
                            Supervisor Name :</td>
                        <td>
                            <asp:TextBox ID="txtSupervisorName" runat="server" MaxLength="20" onkeyup="ToUpper(this)"
                                ToolTip="Enter the Emp. Code" Width="187px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Supervisor Comments :</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtSupervisorComments" runat="server" TextMode="MultiLine" 
                                Width="503px"></asp:TextBox></td>
                    </tr>
                    <cc1:FilteredTextBoxExtender ID="FTBPercentage" runat="server" FilterType="Custom,Numbers"
                        TargetControlID="txtPercentage" ValidChars="0123456789">
                    </cc1:FilteredTextBoxExtender>
                    <cc1:FilteredTextBoxExtender ID="FilteredAmount" runat="server" FilterType="Custom,Numbers"
                        TargetControlID="txtAmount" ValidChars="0123456789.">
                    </cc1:FilteredTextBoxExtender>
                </table>

                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                <asp:HiddenField ID="hfId" runat="server" />
            </fieldset>
            <fieldset style="margin-bottom: 10px;">
                <legend>Temporary Duty List</legend>
                <div style="overflow: scroll; width: 100%; height: 250px">
                    <asp:GridView ID="grTempDuty" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                        AutoGenerateColumns="False" DataKeyNames="DutyAssignID,ActionId,DivisionId,ClinicId,ProjectID,DeptId"
                      OnSelectedIndexChanged="grTempDuty_SelectedIndexChanged"
                         OnRowCommand="grEmpTempDuty_RowCommand">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:ButtonField CommandName="DoubleClick">
                                <ItemStyle Width="1%" CssClass="ItemStylecss" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="ActionName" HeaderText="Name of Action">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="DivisionName" HeaderText="Organization">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ProjectName" HeaderText="Project">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="DeptName" HeaderText="Department">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SubDeptName" HeaderText="Sub Department">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="StartDate" HeaderText="Start Date">
                                <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="EndDate" HeaderText="End Date">
                                <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Amount" HeaderText="Amount">
                                <ItemStyle CssClass="ItemStylecssCenter" Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Percentage" HeaderText="Percentage">
                                <ItemStyle CssClass="ItemStylecssCenter" Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SupervisorId" HeaderText="Supervisor Id">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SupervisorComment" HeaderText="Supervisor Comments">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
        <div class="DivCommand1" style="width: 98%;">
            <div class="DivCommandL">
                <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" Text="Refresh"
                    Width="70px" OnClick="btnRefresh_Click" ToolTip="Click this button to clear all fields." />
            </div>
            <div class="DivCommandR">
                <asp:Button ID="btnSave" runat="server" Text="Save"
                    Width="70px" OnClick="btnSave_Click" ToolTip="Click this button to store the information after providing all necessary fields." />
            </div>
        </div>
    </div>
    <br />
    <br />
</asp:Content>
