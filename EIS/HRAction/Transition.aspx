<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="Transition.aspx.cs" Inherits="EIS_HRAction_Transition" Title="Employee Transition" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js">
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
    <div class="empEduForm">
        <div id="formhead1">
            <div style="width: 97%; float: left;">
                Employee Transition</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div class="empEduDiv01">
            <div style="background-color: #EFF3FB; margin-bottom: 10px;">
                <fieldset>
                    <legend>Employee Information</legend>
                    <table>
                        <tr>
                            <td class="textlevel">
                                Emp Id :
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmpID" runat="server" MaxLength="20" onkeyup="ToUpper(this)"
                                    Width="80px" ToolTip="Type an Emp code and press Enter or click on Find Image. "></asp:TextBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgBtnSearch" runat="server" Height="19px" ImageUrl="~/Images/Search_Icon.jpg"
                                    OnClick="imgBtnSearch_Click" CausesValidation="False" ToolTip="Find Image" />
                            </td>
                            <td style="width: 40%;">
                                <asp:RequiredFieldValidator ID="ReqFieldVal" runat="server" ErrorMessage="*" ControlToValidate="txtEmpID"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td class="textlevel">
                                Name :
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Designation :
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Project :
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblSector" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Department :
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblDept" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Entry Date :
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtEntryDate" runat="server" Width="80px" ToolTip="Select a date by clicking on the calendar image or type a date in this order ‘dd/mm/yyyy’. You must put a valid dalid date for storing record." />
                                <a href="javascript:NewCal('<%= txtEntryDate.ClientID %>','ddmmyyyy')">
                                    <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                        height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtEntryDate"
                                    CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"
                                    Width="60px"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Date of Joining:
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblJoinDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <fieldset>
                <legend>Transition Type</legend>
                <table>
                    <tr>
                        <td class="textlevel">
                            <asp:RadioButton ID="radPromotion" runat="server" Text="Promotion" GroupName="TransitionType" />
                        </td>
                        <td class="textlevel">
                            <asp:RadioButton ID="radTrans" runat="server" Text="Transfer" GroupName="TransitionType" />
                        </td>
                        <td class="textlevel">
                            <asp:RadioButton ID="radStatus" runat="server" Text="Change In Status" GroupName="TransitionType" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend>Transition Informationn</legend>
                <table>
                    <tr>
                        <td class="textlevel">
                            Name of Action :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAction" runat="server" CssClass="textlevelleft" Width="50%"
                                ToolTip="Select an Action from this list. You have to select an Action for storing records. ">
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Organization :
                        </td>
                        <td style="width: 432px">
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="textlevelleft" Width="98%"
                                ToolTip="Select project from this list.">
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                            Suncode :
                        </td>
                        <td style="width: 432px">
                            <asp:DropDownList ID="ddlClinic" runat="server" CssClass="textlevelleft" Width="98%"
                                ToolTip="Select Grade from this list.">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Project :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProject" runat="server" CssClass="textlevelleft" Width="98%">
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Department:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="textlevelleft" Width="98%"
                                ToolTip="Select Location from this list." AutoPostBack="True" 
                                onselectedindexchanged="ddlDepartment_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                            Sub
                            Department:
                        </td>
                        <td style="width: 432px">
                            <asp:DropDownList ID="ddlSubDept" runat="server" CssClass="textlevelleft" Width="98%"
                                ToolTip="Select Location from this list.">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                             Grade :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlGrade" runat="server" CssClass="textlevelleft" Width="98%">
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                           Designation :
                        </td>
                        <td style="width: 432px">
                            <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="textlevelleft" Width="98%"
                                ToolTip="Select Office from this list.">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                             Grade Change Date:</td>
                        <td>
                           <asp:TextBox ID="txtGradeChangeDate" runat="server" Width="80px" ToolTip="Select a date by clicking on the calendar image or type a date in this order ‘dd/mm/yyyy’. You must put a valid dalid date for storing record." />
                            <a href="javascript:NewCal('<%= txtGradeChangeDate.ClientID %>','ddmmyyyy')">
                                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                    height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtGradeChangeDate"
                                CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"
                                Width="60px"></asp:RegularExpressionValidator></td>
                        <td class="textlevel">
                            Employee Type :</td>
                        <td style="width: 432px">
                            <asp:DropDownList ID="ddlEmpType" CssClass="textlevelleft" runat="server" 
                                Width="127px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Effective Date :</td>
                        <td>
                             <asp:TextBox ID="txtEffDate" runat="server" Width="80px" ToolTip="Select a date by clicking on the calendar image or type a date in this order ‘dd/mm/yyyy’. You must put a valid dalid date for storing record." />
                            <a href="javascript:NewCal('<%= txtEffDate.ClientID %>','ddmmyyyy')">
                                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                    height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtEffDate"
                                CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"
                                Width="60px"></asp:RegularExpressionValidator></td>
                        <td class="textlevel">
                            Gross Salary :
                        </td>
                        <td class="textlevelleft">
                            <asp:TextBox ID="txtNewGrossSalary" runat="server" MaxLength="10" 
                                CssClass="TextBoxAmt60"></asp:TextBox>&nbsp;BTD
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Bank Acc No.</td>
                        <td>
                                <asp:TextBox ID="txtBankAccNo" runat="server" Width="80px" 
                                 ToolTip="Select a date by clicking on the calendar image or type a date in this order ‘dd/mm/yyyy’. You must put a valid dalid date for storing record." />
                                </td>
                        <td class="textlevel">
                            Bank Name :</td>
                        <td class="textlevelleft">
                            <asp:DropDownList ID="ddlDesignation0" runat="server" CssClass="textlevelleft" Width="98%"
                                ToolTip="Select Office from this list.">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            
                        </td>
                        <td style="width: 432px">
                           
                            <asp:CheckBox ID="chkIsTAApplicable" runat="server" Text="Is Transfer Allowance Applicable"
                                ForeColor="Black">
                            </asp:CheckBox>
                           
                        </td>
                        <td class="textlevel">
                            Next Increment Date :
                        </td>
                        <td>
                            <asp:TextBox ID="txtNextIncDate" runat="server" Width="80px" ToolTip="Select a date by clicking on the calendar image or type a date in this order ‘dd/mm/yyyy’. You must put a valid dalid date for storing record." />
                            <a href="javascript:NewCal('<%= txtNextIncDate.ClientID %>','ddmmyyyy')">
                                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                    height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNextIncDate"
                                CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"
                                Width="60px"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            
                            Remarks :</td>
                        <td rowspan="2">
                            <asp:TextBox ID="txtRemarks" runat="server" Height="28px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td class="textlevel">
                            Increment Policy :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlIncPolicy" CssClass="textlevelleft" runat="server" Width="127px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                        </td>
                        <td class="textlevel">
                        </td>
                        <td class="textlevelleft">
                            <asp:CheckBox ID="chkIsTAApplicable0" runat="server" Text="Is Next Increment Stop"
                                ForeColor="Black">
                            </asp:CheckBox>
                            
                            <asp:DropDownList ID="ddlSalaryLoc" runat="server" CssClass="textlevelleft" 
                                Visible="False">
                            </asp:DropDownList>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            
                        </td>
                        <td colspan="3">
                            &nbsp;</td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:HiddenField ID="hfDivision" runat="server" />
                <asp:HiddenField ID="hfClinic" runat="server" />
                <asp:HiddenField ID="hfProject" runat="server" />
                <asp:HiddenField ID="hfDept" runat="server" />
                <asp:HiddenField ID="hfSubDept" runat="server" />
                <asp:HiddenField ID="hfDesig" runat="server" />
                <asp:HiddenField ID="hfGrade" runat="server" />
                <asp:HiddenField ID="hfSalLoc" runat="server" />
                <asp:HiddenField ID="hfEmpType" runat="server" />
                <asp:HiddenField ID="hfGrossSalary" runat="server" />
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom,Numbers"
                    ValidChars="0123456789." TargetControlID="txtNewGrossSalary">
                </cc1:FilteredTextBoxExtender>
            </fieldset>
            <br />
            <fieldset>
                <legend>Transition List</legend>
                <div style="overflow: scroll; width: 98%; height: 250px">
                    <asp:GridView ID="grEmpTransition" runat="server" AutoGenerateColumns="False" DataKeyNames="TransId,ActionId,DivisionId,ClinicId,ProjectId,DeptId,DesigId,GradeId,SalLocId,TransType"
                        EmptyDataText="No Record Found" Font-Size="9px" ToolTip="Transition List">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                        <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingRowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:ButtonField CommandName="DoubleClick">
                                <ItemStyle Width="1%" CssClass="ItemStylecss" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="TransType" HeaderText="Transition Type">
                                <ItemStyle CssClass="ItemStylecss" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ActionName" HeaderText="Action">
                                <ItemStyle CssClass="ItemStylecss" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DivisionName" HeaderText="Company">
                                <ItemStyle CssClass="ItemStylecss" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ClinicName" HeaderText="Clinic">
                                <ItemStyle CssClass="ItemStylecss" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ProjectName" HeaderText="Project">
                                <ItemStyle CssClass="ItemStylecss" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DeptName" HeaderText="Department">
                                <ItemStyle CssClass="ItemStylecss" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DesigName" HeaderText="Designation">
                                <ItemStyle CssClass="ItemStylecss" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GradeName" HeaderText="Grade">
                                <ItemStyle CssClass="ItemStylecss" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SalLocName" HeaderText="Salary Location">
                                <ItemStyle CssClass="ItemStylecss" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EffDate" HeaderText="Effective Date">
                                <ItemStyle CssClass="ItemStylecss" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NextIncDate" HeaderText="Next Inc. Date">
                                <ItemStyle CssClass="ItemStylecss" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GrossSalary" HeaderText="Gross Salary">
                                <ItemStyle CssClass="ItemStylecss" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IsTAApplicable" HeaderText="Is TA Applicable">
                                <ItemStyle CssClass="ItemStylecss" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                <ItemStyle CssClass="ItemStylecss" Width="10%" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
        <div class="DivCommand1">
            <div class="DivCommandL">
                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px" CausesValidation="False"
                    OnClick="btnRefresh_Click" ToolTip="Click to set the page as initial stage." />
            </div>
            <div class="DivCommandR">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" UseSubmitBehavior="True"
                    OnClick="btnSave_Click" ToolTip="Click to Save/Edit above informations." />
            </div>
        </div>
    </div>
</asp:Content>
