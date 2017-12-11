<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="EmpPayrollInfo.aspx.cs" Inherits="Back_Office_EmpPayrollInfo" Title="Employee Information(Payroll)" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../JScripts/jquery-1.2.3.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JScripts/ui.datepicker.js"></script>
    <script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js">
        //Date Time Picker script
    </script>
    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
        //Delete Confirmation Message
    </script>
    <script language="javascript" type="text/javascript">
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }
    </script>
    <script language="javascript" type="text/javascript">

    </script>
    <div class="formStyle">
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
        <div id="formhead2">
            <div style="width: 98%; float: left;">
                Employee HR Information(Payroll)</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" Width="500px" Font-Bold="True" ForeColor="Green">
            </asp:Label>
        </div>
        <div class="Div950">
            <fieldset style="background-color: #C2D69B;">
                <div style="float: left; width: 50%;">
                    <table>
                        <tr>
                            <td class="textlevel">
                                <asp:Label runat="server" ID="lblid" Text="Emp ID :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmpID" runat="server" Width="80px">
                                </asp:TextBox>
                                <asp:Button ID="cmdFind" runat="server" OnClick="cmdFind_Click" Text="Find" Width="54px"
                                    CausesValidation="False" />
                                <asp:RequiredFieldValidator ID="rfVJoinDate" runat="server" ControlToValidate="txtEmpID"
                                    ErrorMessage="*">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Full Name :
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmpFullName" runat="server" Width="295px" ReadOnly="True">
                                </asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
            <fieldset>
                <legend>Placement</legend>
                <div style="background-color: #B8CCE4;">
                    <table>
                        <tbody>
                            <tr>
                                <td class="textlevel">
                                    <asp:Label runat="server" ID="Label1" Text="Organization :"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCompany" runat="server" Width="300px" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" 
                                        CssClass="textlevelleft" Enabled="False">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="*" ControlToValidate="ddlCompany"
                                        Operator="NotEqual" ValueToCompare="99999"></asp:CompareValidator>
                                </td>
                                <td class="textlevel">
                                    <asp:Label runat="server" ID="Label3" Text="Sun Code Name :"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlClinic" runat="server" Width="300px"
                                        CssClass="textlevelleft" 
                                        onselectedindexchanged="ddlClinic_SelectedIndexChanged" Enabled="False">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlClinic"
                                        Operator="NotEqual" ValueToCompare="99999"></asp:CompareValidator>
                                </td>
                                <td class="textlevel">
                                    <asp:Label runat="server" ID="Label4" Text="Posting Date :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPostingDate" runat="server" Width="89px">
                                    </asp:TextBox>
                                    <a href="javascript:NewCal('<%= txtPostingDate.ClientID %>','ddmmyyyy')">
                                        <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                            height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a><asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPostingDate"
                                                CssClass="validator" ErrorMessage="Invalid" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textlevel">
                                    <asp:Label runat="server" ID="Label5" Text="Project :"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlProject" runat="server" Width="300px" 
                                        CssClass="textlevelleft" Enabled="False">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="*" ControlToValidate="ddlProject"
                                        Operator="NotEqual" ValueToCompare="99999"></asp:CompareValidator>
                                </td>
                                <td class="textlevel">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td class="textlevel">
                                    <asp:Label runat="server" ID="Label7" Text="Date in Position :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDateInPosition" runat="server" Width="89px">
                                    </asp:TextBox>
                                    <a href="javascript:NewCal('<%= txtDateInPosition.ClientID %>','ddmmyyyy')">
                                        <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                            height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a><asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtDateInPosition"
                                                CssClass="validator" ErrorMessage="Invalid" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textlevel">
                                    <asp:Label runat="server" ID="Label6" Text="Department:"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDept" CssClass="textlevelleft" runat="server" Width="300px"
                                        OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" AutoPostBack="True" 
                                        Enabled="False">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <%-- <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="*" ControlToValidate="ddlUnit"
                                        Operator="NotEqual" ValueToCompare="99999"></asp:CompareValidator>--%>
                                </td>
                                <td class="textlevel">
                                    Location Category :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlLocCategory" runat="server" Width="300px" CssClass="textlevelleft"
                                        OnSelectedIndexChanged="ddlSalaryLoc_SelectedIndexChanged" Enabled="False">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <%-- <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlPosByFunction"
                                        Operator="NotEqual" ValueToCompare="99999"></asp:CompareValidator>--%>
                                </td>
                                <td class="textlevel">
                                    <asp:Label runat="server" ID="Label10" Text="Date in Grade :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDateInGrade" runat="server" Width="89px">
                                    </asp:TextBox>
                                    <a href="javascript:NewCal('<%= txtDateInGrade.ClientID %>','ddmmyyyy')">
                                        <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                            height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a><asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtDateInGrade"
                                                CssClass="validator" ErrorMessage="Invalid" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textlevel">
                                    <asp:Label runat="server" ID="Label22" Text="Sub Dept :"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSubDept" CssClass="textlevelleft" runat="server" 
                                        Width="300px" Enabled="False">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td class="textlevel">
                                    <asp:Label runat="server" ID="Label20" Text="Supervisor :"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSupervisor" runat="server" Width="300px" 
                                        CssClass="textlevelleft" Enabled="False">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td class="textlevel">
                                    <asp:Label ID="lblActionName" runat="server" CssClass="textlevel" Text="Name of Action :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtActionName" runat="server" Width="200px" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="textlevel">
                                    <asp:Label runat="server" ID="Label8" Text="Grade :"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlGrade" CssClass="textlevelleft" runat="server" Width="300px"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlGrade_SelectedIndexChanged" 
                                        Enabled="False">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="*" ControlToValidate="ddlGrade"
                                        Operator="NotEqual" ValueToCompare="99999"></asp:CompareValidator>
                                </td>
                                <td class="textlevel" style="background-color: #3399FF;">
                                    Tax Region Id:</td>
                                <td style="background-color: #3399FF;">
                                    <asp:DropDownList ID="ddlTaxRegion" runat="server" Width="300px" CssClass="textlevelleft"
                                        OnSelectedIndexChanged="ddlSalaryLoc_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <%-- <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="*" ControlToValidate="ddlJobTitle"
                                        Operator="NotEqual" ValueToCompare="99999"></asp:CompareValidator>--%>
                                </td>
                                <td class="textlevel">
                                    <asp:Label ID="Label2" runat="server" CssClass="textlevel" Text="Action Date :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtActionDate" runat="server" Width="89px" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="textlevel">
                                    Designation :</td>
                                <td>
                                    &nbsp;<asp:DropDownList ID="ddlDesignation" runat="server" Width="300px" 
                                        CssClass="textlevelleft" Enabled="False">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <%--  <asp:CompareValidator ID="CompareValidator10" runat="server" ErrorMessage="*" ControlToValidate="ddlPostDivision"
                                        Operator="NotEqual" ValueToCompare="99999"></asp:CompareValidator>--%>
                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="*" ControlToValidate="ddlDesignation"
                                        Operator="NotEqual" ValueToCompare="99999"></asp:CompareValidator>
                                </td>
                                <td class="textlevel">
                                    &nbsp;&nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <%--<asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="*" ControlToValidate="ddlPostDistrict"
                                        Operator="NotEqual" ValueToCompare="-1">
                                    </asp:CompareValidator>--%><%--<asp:CompareValidator ID="CompareValidator11" runat="server" ErrorMessage="*" ControlToValidate="ddlPostDistrict"
                                        Operator="NotEqual" ValueToCompare="99999"></asp:CompareValidator>--%>
                                </td>
                                <td class="textlevel">
                                    <asp:Label ID="lblActionDate" runat="server" CssClass="textlevel" Text="Working Days :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtWorkingDays" Text="30" runat="server" Width="89px" CssClass="TextBoxAmt60"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator15" runat="server" ErrorMessage="*" ControlToValidate="txtWorkingDays"
                                        Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="textlevel">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <%--<asp:CompareValidator ID="CompareValidator13" runat="server" ErrorMessage="*" ControlToValidate="ddlPostingPlace"
                                        Operator="NotEqual" ValueToCompare="99999"></asp:CompareValidator>--%>
                                </td>
                                <td class="textlevel">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td class="textlevel">
                                    <asp:Label runat="server" ID="Label18" Text="Supervisor Id :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSupervisorId" runat="server" Width="130px" AutoPostBack="True"
                                        OnTextChanged="txtSupervisorId_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="textlevel" style="height: 15px">
                                    
                                </td>
                                <td style="height: 15px">
                                    
                                </td>
                                <td style="height: 15px">
                                </td>
                                <td class="textlevel" style="height: 15px">
                                   
                                </td>
                                <td style="height: 15px">
                                    
                                </td>
                                <td style="height: 15px">
                                </td>
                                <td style="height: 15px" class="textlevel">
                                    <asp:Label runat="server" ID="Label16" Text="Work Area Type :"></asp:Label>
                                </td>
                                <td style="height: 15px">
                                    &nbsp;<asp:CheckBox ID="chkWorkArea" runat="server" CssClass="textlevelleft" Text="Is Remote"
                                        Width="91px" />
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="textlevel" style="height: 15px">
                                    &nbsp;
                                </td>
                                <td style="height: 15px">
                                    &nbsp;
                                </td>
                                <td style="height: 15px">
                                    &nbsp;
                                </td>
                                <td class="textlevel" style="height: 15px">
                                    &nbsp;
                                </td>
                                <td style="height: 15px">
                                    <asp:TextBox ID="txtSeveranceReason" runat="server" Width="16px" Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="txtSeveranceId" runat="server" Width="16px" Visible="False"></asp:TextBox>
                                    <asp:DropDownList ID="ddlRegion" runat="server" CssClass="textlevelleft" Visible="False">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlProjectOffice" runat="server" CssClass="textlevelleft" Visible="False">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtWorkArea" runat="server" Width="37px" Visible="False"></asp:TextBox>
                                    <asp:DropDownList ID="ddlSalaryLoc" runat="server" CssClass="textlevelleft" OnSelectedIndexChanged="ddlSalaryLoc_SelectedIndexChanged"
                                        Visible="False">
                                    </asp:DropDownList>
                                </td>
                                <td style="height: 15px">
                                    &nbsp;
                                </td>
                                <td style="height: 15px" class="textlevel">
                                    &nbsp;
                                </td>
                                <td style="height: 15px">
                                    <asp:CheckBox ID="chkIsSeveranceBenefit" runat="server" CssClass="textlevelleft"
                                        Text="Is Severance Benifit" Width="129px" Visible="False" />
                                    <asp:TextBox ID="txtContractPurpose" runat="server" Width="43px" Visible="False"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </fieldset>
            <fieldset>
                <legend>Employment </legend>
                <div style="background-color: #F2DBDB;">
                    <table>
                        <tr>
                            <td class="textlevel">
                                    <asp:Label runat="server" ID="Label12" Text="Employee Type :"></asp:Label>
                                </td>
                            <td>
                                    <asp:DropDownList ID="ddlEmpType" runat="server" CssClass="textlevelleft"
                                        OnSelectedIndexChanged="ddlEmpType_SelectedIndexChanged" 
                                        AutoPostBack="True" Enabled="False">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="*" ControlToValidate="ddlEmpType"
                                        Operator="NotEqual" ValueToCompare="99999"></asp:CompareValidator>
                                </td>
                            <td class="textlevel">
                               
                                Probation Period :</td>
                            <td colspan="1">
                                
                                <asp:TextBox ID="txtProbationPeriod" runat="server" Width="50px" MaxLength="5" CssClass="TextBoxAmt60"
                                    AutoPostBack="True" OnTextChanged="txtProbationPeriod_TextChanged" 
                                    Enabled="False">0</asp:TextBox>
                                
                                (Months)</td>
                            <td colspan="1">
                                &nbsp;</td>
                            <td colspan="1">
                                
                            </td>
                            <td colspan="1">
                            </td>
                            <td class="textlevel">
                                
                            </td>
                            <td class="textlevelleft">
                                &nbsp;</td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                     Joining Date :</td>
                            <td>
                                   <asp:TextBox ID="txtJoiningDate" runat="server" Width="89px" Enabled="False"></asp:TextBox>
                                <a href="javascript:NewCal('<%= txtJoiningDate.ClientID %>','ddmmyyyy')">
                                    <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                        height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Width="60px"
                                    ErrorMessage="Invalid" ControlToValidate="txtJoiningDate" CssClass="validator"
                                    ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator></td>
                                <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtJoiningDate">
                                </asp:RequiredFieldValidator>
                                    </td>
                            <td class="textlevel">
                                Confirmation Date :</td>
                            <td colspan="1">
                                <asp:TextBox ID="txtConfirmDate" runat="server" Width="89px" Enabled="False"></asp:TextBox>
                                <a href="javascript:NewCal('<%= txtConfirmDate.ClientID %>','ddmmyyyy')">
                                    <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                        height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a></td>
                            <td colspan="1">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtConfirmDate"
                                    CssClass="validator" ErrorMessage="Invalid" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                            </td>
                            <td colspan="1">
                                &nbsp;</td>
                            <td colspan="1">
                                &nbsp;</td>
                            <td class="textlevel">
                                &nbsp;</td>
                            <td class="textlevelleft">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                    Job Status :</td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="127px" 
                                    CssClass="textlevelleft" Enabled="False">
                                    <asp:ListItem Value="A">Active</asp:ListItem>
                                    <asp:ListItem Value="I">In-Active</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                                <td>
                                    </td>
                            <td class="textlevel">
                                Contract Duration :</td>
                            <td colspan="1">
                                <asp:TextBox ID="txtContractInterval" runat="server" Width="50px" MaxLength="5" CssClass="TextBoxAmt60"
                                    AutoPostBack="True" OnTextChanged="txtContractInterval_TextChanged" 
                                    Enabled="False">0</asp:TextBox>
                                </td>
                            <td colspan="1">
                                &nbsp;</td>
                            <td colspan="1">
                                </td>
                            <td colspan="1">
                                &nbsp;</td>
                            <td class="textlevel">
                                &nbsp;</td>
                            <td class="textlevelleft">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                    &nbsp;</td>
                            <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            <td class="textlevel">
                                Contract End Date :</td>
                            <td colspan="1">
                                <asp:TextBox ID="txtContractExpDate" runat="server" Width="89px" 
                                    Enabled="False"></asp:TextBox>
                                <a href="javascript:NewCal('<%= txtContractExpDate.ClientID %>','ddmmyyyy')">
                                    <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                        height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a></td>
                            <td colspan="1">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtContractExpDate"
                                    CssClass="validator" ErrorMessage="Invalid" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                            </td>
                            <td colspan="1">
                                &nbsp;</td>
                            <td colspan="1">
                                &nbsp;</td>
                            <td class="textlevel">
                                &nbsp;</td>
                            <td class="textlevelleft">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </div>
            </fieldset>
            <fieldset>
                <legend>Payroll</legend>
                <div style="background-color: #C8C8C8;">
                    <table>
                        <tbody>
                            <tr>
                                <td class="textlevel" style="background-color: #3399FF;">
                                   <asp:Label runat="server" ID="Label21" Text="Gross Salary :"></asp:Label></td>
                                <td style="background-color: #3399FF;">
                                    <asp:TextBox ID="txtGrossSalary" runat="server" CssClass="TextBoxAmt60" Width="89px" 
                                        OnTextChanged="txtGrossSalary_TextChanged"></asp:TextBox>
                                    &nbsp;&nbsp;BDT
                                    <asp:CompareValidator ID="CompareValidator14" runat="server" ErrorMessage="*" ControlToValidate="txtGrossSalary"
                                        Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
                                    <asp:Button ID="cmdCalBasic" runat="server" OnClick="cmdCalBasic_Click" Text="Calculate Basic"
                                        Width="105px" CausesValidation="False" /></td>
                                <td>
                                    &nbsp;</td>
                                <td class="textlevel" style="background-color: #3399FF;">
                                     Basic Salary :</td>
                                <td style="background-color: #3399FF;">
                                    <asp:TextBox ID="txtBasicSalary" runat="server" CssClass="TextBoxAmt60" Width="89px"></asp:TextBox>
                                    &nbsp;<cc1:FilteredTextBoxExtender ID="txtBasicSalary_FilteredTextBoxExtender" runat="server"
                                        FilterType="Custom,Numbers" TargetControlID="txtBasicSalary" ValidChars="0123456789.*">
                                    </cc1:FilteredTextBoxExtender>
                                    &nbsp;BDT<asp:CompareValidator ID="CompareValidator17" runat="server" ErrorMessage="*"
                                        ControlToValidate="txtBasicSalary" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td>
                                <td  style="border-color:Orange; background-color: #3399FF;">
                                    <asp:CheckBox ID="chkIsPayrollStaff" runat="server" Text="Is Payroll Staff"
                                        Width="167px" Font-Bold="True" />
                                    </td>
                            </tr>
                            <tr>
                                <td class="textlevel" style="background-color: #3399FF;">
                                    Salary Package :
                                </td>
                                <td style="background-color: #3399FF;">
                                    <asp:DropDownList ID="ddlSalaryPak" runat="server" Width="300px" CssClass="textlevelleft">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td class="textlevel">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="textlevel" style="background-color: #3399FF;">
                                    Payroll Cycle :
                                </td>
                                <td style="background-color: #3399FF;"> 
                                    <asp:DropDownList ID="ddlMPC" runat="server" Width="300px" CssClass="textlevelleft">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="*" ControlToValidate="ddlMPC"
                                        Operator="NotEqual" ValueToCompare="99999"></asp:CompareValidator>
                                </td>
                                <td class="textlevel" style="background-color: #3399FF;">
                                    Bank Account No :
                                    <td style="background-color: #3399FF;">
                                        <asp:TextBox ID="txtBankAccNo" runat="server" Width="130px"></asp:TextBox>&nbsp;&nbsp;
                                    </td>
                                    <td>
                                    </td>
                            </tr>
                            <tr>
                                <td class="textlevel" style="background-color: #3399FF;">
                                    Bank Name :
                                </td>
                                <td style="background-color: #3399FF;">
                                    <asp:DropDownList ID="ddlBankName" runat="server" Width="300px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged" CssClass="textlevelleft">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td class="textlevel" style="background-color: #3399FF;">
                                    Branch Name :
                                </td>
                                <td style="background-color: #3399FF;">
                                    <asp:DropDownList ID="ddlBranchCode" runat="server" Width="300px" CssClass="textlevelleft">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="textlevel" style="background-color: #3399FF;">
                                    Routing No.:
                                </td>
                                <td style="background-color: #3399FF;">
                                    <asp:Label ID="lblRoutingNo" runat="server" CssClass="textlevel" Text="Label"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td class="textlevel">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </fieldset>
            <fieldset>
                <asp:Panel ID="pnlLeaveAttn" runat="server">
                    <legend>Leave &amp; Attendance</legend>
                    <div style="background-color: #C2D69B;">
                        <table>
                            <tbody>
                                <tr>
                                    <td class="textlevel">
                                        Leave Package :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlLeavePackage" runat="server" Width="300px" 
                                            CssClass="textlevelleft" Enabled="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="textlevel">
                                        Weekend :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlWeekend" runat="server" Width="300px" CssClass="textlevelleft"
                                            Visible="True" Enabled="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="textlevel">
                                        Attnd Policy :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlAttndPolicy" runat="server" Width="90px" CssClass="textlevelleft"
                                            Visible="True" Enabled="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </asp:Panel>
            </fieldset>
            
           
            <fieldset>
                <legend>Separation</legend>
                <div style="background-color: #C8C8C8;">
                    <table>
                        <tr>
                            <td class="textlevel">
                                Retirement Date :
                            </td>
                            <td>
                                <asp:TextBox ID="txtRetirementDate" runat="server" Width="89px" Enabled="False"></asp:TextBox>
                                <a href="javascript:NewCal('<%= txtRetirementDate.ClientID %>','ddmmyyyy')">
                                    <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                        height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a>
                            </td>
                            <td>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Width="60px"
                                    ErrorMessage="Invalid" ControlToValidate="txtRetirementDate" CssClass="validator"
                                    ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                            </td>
                            <td>
                            </td>
                            <td class="textlevel">
                                Separation Date :
                            </td>
                            <td>
                                <asp:TextBox ID="txtSeparationDate" runat="server" Width="89px" Enabled="False"></asp:TextBox>
                                <a href="javascript:NewCal('<%= txtSeparationDate.ClientID %>','ddmmyyyy')">
                                    <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                        height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                    ControlToValidate="txtSeparationDate" CssClass="validator" ErrorMessage="Invalid"
                                    ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel" valign="top">
                                Separation Type :
                            </td>
                            <td valign="top">
                                <asp:DropDownList ID="ddlSepType" runat="server" Width="250px" 
                                    CssClass="textlevelleft" Enabled="False">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td class="textlevel" valign="top">
                                <asp:Label ID="lblSeparationReason" runat="server" CssClass="textlevel" Text="Separation Reason :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSeparationReason" runat="server" Width="295px" 
                                    Font-Names="Arial" Enabled="False"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel" valign="top">
                                &nbsp;
                            </td>
                            <td valign="top">
                                <asp:CheckBox ID="chkIsNotRehire" runat="server" CssClass="textlevelleft" Text="Is Not Rehirable"
                                    Width="127px" Enabled="False" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td class="textlevel" valign="top">
                                <asp:Label ID="lblNotRehireReason" runat="server" CssClass="textlevel" Text="Reason of Not Rehirable :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNotRehireReason" runat="server" Width="295px" 
                                    Font-Names="Arial" Enabled="False"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
            <fieldset>
                <legend>Other Information</legend>
                <div style="background-color: #F2DBDB;">
                    <table>
                        <tr>
                            <td class="textlevel">
                                Other Benefit :
                            </td>
                            <td>
                                <asp:TextBox ID="txtOtherBenefit" runat="server" Font-Names="Arial" 
                                    Width="295px" Enabled="False">N/A</asp:TextBox>&nbsp;
                            </td>
                            <td class="textlevel">
                                Remarks :
                            </td>
                            <td>
                                <asp:TextBox ID="txtRemarks" runat="server" Font-Names="Arial" Width="295px" 
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkIsMedicalEntitle" runat="server" CssClass="textlevelleft" Text="Is Medical Entitlement"
                                    Width="130px" Enabled="False" />
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkIsOTEntitle" runat="server" CssClass="textlevelleft" Text="Is OT Entitlement"
                                    Width="127px" Enabled="False" />
                                <asp:CheckBox ID="chkIsChildEdu" runat="server" CssClass="textlevelleft" Text="Child Education Allowance"
                                    Width="163px" Visible="False" Enabled="False" />
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Asset :
                            </td>
                            <td>
                                <asp:TextBox ID="txtAsset" runat="server" Font-Names="Arial" Width="295px" Font-Size="Small"
                                    TextMode="MultiLine" Enabled="False"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
            <fieldset>
                <asp:Panel ID="pnlUploadDoc" runat="server">
                    <legend>Upload Document</legend>
                    <div style="background-color: #B8CCE4;">
                        <table>
                            <tr>
                                <td class="textlevel">
                                    Emp CV :
                                </td>
                                <td>
                                    <asp:FileUpload ID="fileEmpCV" runat="server" />
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEmpCV" runat="server" OnClick="lnkEmpCV_Click"></asp:LinkButton>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="textlevel">
                                    Emp Signature :
                                </td>
                                <td>
                                    <asp:FileUpload ID="fileEmpSignature" runat="server" />
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEmpSignature" runat="server" OnClick="lnkEmpSignature_Click"></asp:LinkButton>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="textlevel">
                                    Emp Document :
                                </td>
                                <td>
                                    <asp:FileUpload ID="fileEmpDocument" runat="server" />
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEmpDocument" runat="server" OnClick="lnkEmpDocument_Click"></asp:LinkButton>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <legend>Action List</legend>
            </fieldset>
            <asp:GridView ID="grEmpAction" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                Font-Size="9px" Width="30%">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                <AlternatingRowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:BoundField DataField="ActionName" HeaderText="Action Name">
                        <ItemStyle CssClass="ItemStylecss" Width="30%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ActionDate" HeaderText="Action Date">
                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <fieldset>
                <asp:Panel ID="PnlSalaryRange" runat="server" Width="600px" BackColor="White">
                    <div style="background-image: url(../Images/back-bar-header.png); color: white; background-repeat: repeat-x;
                        height: 20px;">
                        Basic Salary Range</div>
                    <asp:Label ID="lblSalaryRange" runat="server" CssClass="msglabel" BackColor="White"></asp:Label>
                    <div style="margin-top: 5px; vertical-align: middle; height: 27px; background-color: gray;
                        text-align: center">
                        <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnOk_Click" Width="60px"
                            BackColor="#336699" Font-Bold="True" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="60px" BackColor="#336699"
                            Font-Bold="True" /></div>
                </asp:Panel>
            </fieldset>
            <cc1:ModalPopupExtender ID="ModalPopupTree" runat="server" BackgroundCssClass="modalBackground"
                CancelControlID="btnCancel" DropShadow="True" DynamicServicePath="" Enabled="True"
                PopupControlID="PnlSalaryRange" TargetControlID="btnShow">
            </cc1:ModalPopupExtender>
            <div class="DivCommand1" style="width: 100%;">
                <div class="DivCommandL" style="padding-left: 12px;">
                    <asp:Button ID="btnClear" runat="server" Text="Refresh" Width="70px" OnClick="btnClear_Click"
                        UseSubmitBehavior="False" CausesValidation="False" />
                </div>
                <div class="DivCommandR">
                    <asp:Button ID="btnShow" runat="server" Text="..." OnClick="btnDelete_Click" Enabled="False" />
                    <asp:CheckBox ID="chkIsNew" runat="server" CssClass="textlevelleft" Text="Is New"
                        Width="127px" />
                    <asp:Button ID="btnSave" runat="server" Text="Update" Width="70px" OnClick="btnSave_Click"
                        Style="height: 26px" />
                </div>
            </div>
            <asp:HiddenField ID="hfLPakId" runat="server" />
            <asp:HiddenField ID="hfIsUpadate" runat="server" />
            <asp:HiddenField ID="hfID" runat="server" />
            <asp:HiddenField ID="hfJoiningDate" runat="server" />
            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                TargetControlID="txtProbationPeriod">
            </cc1:FilteredTextBoxExtender>
            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom,Numbers"
                TargetControlID="txtGrossSalary" ValidChars="0123456789.*">
            </cc1:FilteredTextBoxExtender>
            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers"
                TargetControlID="txtWorkingDays">
            </cc1:FilteredTextBoxExtender>
            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers"
                TargetControlID="txtContractInterval">
            </cc1:FilteredTextBoxExtender>
            <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
        </div>
</asp:Content>
