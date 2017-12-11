<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="LeaveApplication.aspx.cs" Inherits="Leave_LeaveApplication" Title="Leave Application" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js">
    //Date Time Picker script
    </script>

    <script language="javascript" type="text/javascript">
    function ToUpper(ctrl)
    {   
    var t = ctrl.value;
    ctrl.value = t.toUpperCase(); 
    }
    </script>

    <div class="leaveApplicaionStyle">
        <div id='formhead4'>
            <div style="width: 98%; float: left;">
                Leave Application Form</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <asp:UpdateProgress Id="UpProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
            DisplayAfter="0">
            <progresstemplate>
                    <div style="position: absolute; visibility: visible; border: none; z-index: 100;
                        width: 100%; height: 100%; background: #999; filter: alpha(opacity=80); -moz-opacity: .8;
                        opacity: .8; text-align: center;">
                    
                    <img style="position: relative; top: 45%;" src="../Images/photo-loader.gif" alt="" />
                    </div>
                </progresstemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <contenttemplate>
        <div style="width: 98%; text-align: right; margin-top: 5px;">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabelleft" Font-Bold="True" ForeColor="Navy"></asp:Label>
        </div>
        <!--Div for group-->
        <div id="leaveApplicaionFormInner" style="margin-top: 5px;">
            <cc1:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="0" Height="590px">
                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1"><HeaderTemplate>
Leave Application Form
</HeaderTemplate>
<ContentTemplate>
<div style="background-color: #EFF3FB; border: solid 1px #3E506C;"><table><tr>
        <td><asp:Label ID="Label7" runat="server" CssClass="textlevel" Text="Emp Id : "></asp:Label>


</td><td><asp:TextBox ID="txtEmpId" runat="server" Height="16px" 
            onkeyup="ToUpper(this)" Width="80px"></asp:TextBox>


<asp:ImageButton ID="imgBtnSearch" runat="server" CausesValidation="False" 
            Height="19px" ImageUrl="~/Images/Search_Icon.jpg" OnClick="imgBtnSearch_Click" 
            Width="21px"></asp:ImageButton>


<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="txtEmpId" ErrorMessage="*" Font-Bold="True" 
            Font-Size="X-Large" Height="16px"></asp:RequiredFieldValidator>


</td><td></td></tr></table><table>
    <tr><td><asp:Label 
                    ID="Label2" runat="server" CssClass="textlevel" Text="Applicant : "></asp:Label>


</td><td><asp:Label 
                        ID="lblName" runat="server" Width="600px"></asp:Label>


</td><td><asp:Label 
                        ID="Label15" runat="server" CssClass="textlevel" Text="Supervisor :"></asp:Label>


</td><td><asp:Label 
                        ID="lblSupervisor" runat="server" Width="600px"></asp:Label>


</td></tr><tr><td>
        <asp:Label 
                        ID="Label5" runat="server" CssClass="textlevel" Text="Leave Package : "></asp:Label>


</td><td><asp:Label 
                            ID="lblEmpType" runat="server" Width="211px"></asp:Label>


</td></tr></table><asp:HiddenField ID="hfSex" runat="server" />


</div><fieldset style="margin-top: 5px;">
    <div style="width: 100%">
        <div style="width: 38%; float: left; background-color: #B8CCE4; border: solid 1px Gray;
                                    height: 400px;">
            <table style="width: 100%"><tr>
                <td style="font-size: 14px; font-weight: bold; text-align: left; border-bottom: solid 1px Gray;">Check Leave Balance</td></tr>
                <tr><td>
                    <div style="float: left; margin: 5px 0px 0px 5px; width: 98%; height: 325px;"><asp:GridView 
                            ID="grLeaveStatus" runat="server" AutoGenerateColumns="False" 
                            DataKeyNames="EmpId,LTypeID,LTypeTitle,LCarryOverd,LEntitled,LCashed,LeaveEnjoyed,LeaveElapsed,lvOpening" 
                            EmptyDataText="No Record Found" Font-Size="8px" 
                            OnSelectedIndexChanged="grLeaveStatus_SelectedIndexChanged" PageSize="7" 
                            Width="100%">
<AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
<Columns>
<asp:BoundField DataField="LTypeTitle" HeaderText="Leave Title">
<ItemStyle CssClass="ItemStylecssCenter" Width="25%"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="lvPrevYearCarry" Visible="False">
<ItemStyle CssClass="ItemStylecssCenter" HorizontalAlign="Right" Width="0%"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="LCarryOverd" HeaderText="Carry Over">
<ItemStyle CssClass="ItemStylecssCenter" HorizontalAlign="Right" Width="15%"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="LEntitled" HeaderText="Entitled">
<ItemStyle CssClass="ItemStylecssCenter" HorizontalAlign="Right" Width="15%"></ItemStyle>
</asp:BoundField>
<asp:BoundField HeaderText="Total Leave Entitlement">
<ItemStyle CssClass="ItemStylecssCenter" HorizontalAlign="Right" Width="15%"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="LeaveEnjoyed" HeaderText="Availed">
<ItemStyle CssClass="ItemStylecssCenter" HorizontalAlign="Right" Width="15%"></ItemStyle>
</asp:BoundField>
<asp:BoundField HeaderText="Current Balance">
<ItemStyle CssClass="ItemStylecssCenter" HorizontalAlign="Right" Width="15%"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle BackColor="#FF9933" Font-Bold="True" Font-Size="10px" ForeColor="WhiteSmoke" 
                            HorizontalAlign="Center"></HeaderStyle>

<RowStyle Height="30px"></RowStyle>
</asp:GridView>


</div></td></tr><tr><td style="text-align: center; height: 35px;"><hr />
                    <hr /><asp:Button ID="btnLeaveHistroy" runat="server" CausesValidation="False" 
                        OnClick="btnLeaveHistroy_Click" Text="Show My Leave History" Width="240px"></asp:Button>


</td></tr></table></div><div style="width: 29%; float: left; margin-left: 1%; background-color: #F2DBDB;
                                    border: solid 1px Gray; height: 400px;">
            <table style="width: 100%;"></table>
            <table width="100%"><tr><td colspan="3" 
                    style="font-size: 14px; font-weight: bold; text-align: left; border-bottom: solid 1px Gray;">Complete Leave Application</td></tr>
                <tbody><tr>
                    <td class="textlevel">App. Date :</td>
                    <td><asp:TextBox ID="txtAppDate" runat="server" Height="16px" ReadOnly="True" 
                            Width="80px"></asp:TextBox>


<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" __designer:wfdid="w697" 
                            ControlToValidate="txtAppDate" ErrorMessage="*" Font-Bold="True" 
                            Font-Size="Large" Height="16px"></asp:RequiredFieldValidator>


</td><td></td><td style="width: 3px">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        __designer:wfdid="w698" ControlToValidate="txtAppDate" CssClass="validator" 
                        ErrorMessage="Invalid" 
                        ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$" 
                        Width="40px"></asp:RegularExpressionValidator>


</td></tr><tr><td class="textlevel">Date From :</td>
                        <td><asp:TextBox ID="txtFromDate" runat="server" __designer:wfdid="w699" 
                                MaxLength="10" Width="80px"></asp:TextBox>


<a href="javascript:NewCal('<%= txtFromDate.ClientID %>','ddmmyyyy')">
                            <img alt="Pick a date" height="16" src="../images/cal.gif" 
                                style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px" 
                                width="16" /></a><asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                                runat="server" __designer:wfdid="w747" ControlToValidate="txtFromDate" 
                                ErrorMessage="*" Font-Bold="True"></asp:RequiredFieldValidator>


</td><td><asp:Panel ID="PanelFrom" runat="server"></asp:Panel>


</td><td style="width: 3px"><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                __designer:wfdid="w700" ControlToValidate="txtFromDate" CssClass="validator" 
                                ErrorMessage="Invalid" 
                                ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$" 
                                Width="40px"></asp:RegularExpressionValidator>


</td></tr><tr><td class="textlevel">Date To :</td>
                        <td><asp:TextBox ID="txtToDate" runat="server" __designer:wfdid="w701" 
                                MaxLength="10" Width="80px"></asp:TextBox>


<a href="javascript:NewCal('<%= txtToDate.ClientID %>','ddmmyyyy')">
                            <img alt="Pick a date" height="16" src="../images/cal.gif" 
                                style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px" 
                                width="16" /></a><asp:RequiredFieldValidator ID="RequiredFieldValidator4" 
                                runat="server" __designer:wfdid="w748" ControlToValidate="txtToDate" 
                                ErrorMessage="*" Font-Bold="True"></asp:RequiredFieldValidator>


</td><td><asp:Panel ID="PanelTo" runat="server"></asp:Panel>


</td><td style="width: 3px"><asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                                __designer:wfdid="w702" ControlToValidate="txtToDate" CssClass="validator" 
                                ErrorMessage="Invalid" 
                                ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$" 
                                Width="40px"></asp:RegularExpressionValidator>


</td></tr><tr>
                        <td class="textlevel">Half Day :</td>
                        <td colspan="2"><asp:DropDownList ID="ddlHalfDay" runat="server" 
                                __designer:wfdid="w703" AutoPostBack="True" CausesValidation="True" 
                                CssClass="textlevelleft" 
                                OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged" Width="84px"><asp:ListItem 
                                Value="0">Nil</asp:ListItem>
<asp:ListItem Value="1">1st Half</asp:ListItem>
<asp:ListItem Value="2">2nd Half</asp:ListItem>
</asp:DropDownList>


</td><td style="width: 3px"></td></tr>
                    <tr><td class="textlevel">Resume Date :</td>
                        <td><asp:TextBox ID="txtResumeOn" runat="server" __designer:wfdid="w704" 
                                MaxLength="10" Width="80px"></asp:TextBox>


<a href="javascript:NewCal('<%= txtResumeOn.ClientID %>','ddmmyyyy')">
                            <img alt="Pick a date" height="16" src="../images/cal.gif" 
                                style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px" 
                                width="16" /></a><asp:RequiredFieldValidator ID="RequiredFieldValidator5" 
                                runat="server" __designer:wfdid="w749" ControlToValidate="txtResumeOn" 
                                ErrorMessage="*" Font-Bold="True"></asp:RequiredFieldValidator>


</td><td>&nbsp;</td><td style="width: 3px">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                            __designer:wfdid="w705" ControlToValidate="txtResumeOn" CssClass="validator" 
                            ErrorMessage="Invalid" 
                            ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$" 
                            Width="40px"></asp:RegularExpressionValidator>


</td></tr><tr>
                        <td class="textlevel">Leave Type :</td>
                        <td colspan="2"><asp:DropDownList ID="ddlLeaveType" runat="server" 
                                __designer:wfdid="w706" AutoPostBack="True" CausesValidation="True" 
                                CssClass="textlevelleft" 
                                OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged" Width="110px"><asp:ListItem 
                                Value="99999">Nil</asp:ListItem>
</asp:DropDownList>


</td><td style="width: 3px"></td></tr>
                    <tr>
                        <td class="textlevelshort" style="width: 80px"></td>
                        <td><asp:Button ID="btnCalculate" runat="server" __designer:wfdid="w707" 
                                OnClick="btnCalculate_Click" Text="Calculate Days " Visible="False" 
                                Width="100px"></asp:Button>


</td><td></td><td style="width: 3px"></td></tr>
                    <tr><td></td>
                        <td colspan="2"><asp:Label ID="Label14" runat="server" __designer:wfdid="w708" 
                                BackColor="#FF9933" CssClass="textlevelcenterfine" Text="Availability" 
                                Width="50px"></asp:Label>


<asp:Label ID="Label114" runat="server" __designer:wfdid="w709" BackColor="#FF9933" 
                                CssClass="textlevelcenterfine" Text="Applied For" Width="79px"></asp:Label>


</td><td></td><td style="width: 3px"></td></tr>
                    <tr><td></td>
                        <td><asp:TextBox ID="LAv" runat="server" __designer:wfdid="w710" 
                                BorderColor="Transparent" BorderStyle="None" ForeColor="#3366CC" Width="40px"></asp:TextBox>


&nbsp;&nbsp; <asp:TextBox ID="txtLDurInDays" runat="server" __designer:wfdid="w711" BorderColor="Transparent" 
                                BorderStyle="None" ForeColor="#3366CC" Width="40px"></asp:TextBox>


</td><td></td><td style="width: 3px"></td></tr>
                    <tr><td class="textlevelshort" colspan="4"><asp:Label ID="lblMsg2" runat="server" 
                            __designer:wfdid="w712" CssClass="msglabel" Width="200px"></asp:Label>


</td></tr><tr><td class="textlevelshort" colspan="4">
                        <asp:Label ID="lblMsg3" runat="server" __designer:wfdid="w713" 
                            CssClass="msglabel" Width="200px"></asp:Label>


</td></tr><tr><td class="textlevelshort" colspan="4"></td></tr>
                    <tr><td colspan="4" style="height: 115px"><hr />
                        <hr /><asp:Button ID="btnReload" runat="server" CausesValidation="False" 
                            OnClick="btnReload_Click" Text="Refresh" Width="95%"></asp:Button>


</td></tr></tbody></table><asp:HiddenField ID="hfLEnjoyed" runat="server" __designer:wfdid="w714"></asp:HiddenField>


<asp:HiddenField ID="hfLAbbrName" runat="server" __designer:wfdid="w715"></asp:HiddenField>


<asp:HiddenField ID="hfLDates" runat="server" __designer:wfdid="w716"></asp:HiddenField>


<asp:HiddenField ID="hfLTypeNature" runat="server" __designer:wfdid="w717"></asp:HiddenField>


<asp:HiddenField ID="hfPreLTypeId" runat="server" __designer:wfdid="w718"></asp:HiddenField>


<asp:HiddenField ID="hfPreLEnjoyed" runat="server" __designer:wfdid="w719"></asp:HiddenField>


<asp:HiddenField ID="hfPreLDates" runat="server" __designer:wfdid="w720"></asp:HiddenField>


<asp:HiddenField ID="hfIsOffdayCounted" runat="server" __designer:wfdid="w721"></asp:HiddenField>


<cc1:FilteredTextBoxExtender ID="ftbADate" runat="server" ValidChars="/" Enabled="True"
                                        TargetControlID="txtAppDate" FilterType="Custom, Numbers" __designer:wfdid="w722"></cc1:FilteredTextBoxExtender>


<asp:Label ID="lblLDurInDays" runat="server" __designer:wfdid="w723"></asp:Label>


</div><div style="width: 30%; float: left; margin-left: 1%; background-color: #C2D69B;
                                    border: solid 1px Gray; height: 400px;"><table style="width: 100%;">
                <tr>
                    <td style="font-size: 14px; font-weight: bold; text-align: left; border-bottom: solid 1px Gray;">Fill in Remarks</td></tr>
                <tr><td class="textlevelleft" style="height: 20px" valign="bottom">Reason (In case of TOIL application please mention which date/s and why you have worked)</td></tr>
                <tr><td><asp:TextBox ID="txtLeaveReason" runat="server" Height="94px" MaxLength="1000" TextMode="MultiLine"
                                                    Width="98%"></asp:TextBox>


</td></tr><tr><td class="textlevelleft" style="height: 20px" valign="bottom">Leave Address</td></tr>
                <tr><td><asp:TextBox ID="txtLeaveAdd" runat="server" Height="94px" MaxLength="150" TextMode="MultiLine"
                                                    Width="98%"></asp:TextBox>


</td></tr><tr><td class="textlevelleft" style="height: 23px" valign="bottom">Contact Number</td></tr>
                <tr><td><asp:TextBox ID="txtPhone" runat="server" Height="17px" MaxLength="100" Width="98%"></asp:TextBox>


</td></tr><tr><td style="height: 70px;">
                    <hr /><hr />
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Email Application to My Supervisor"
                                                    Width="95%" />


</td></tr></table></div></div></fieldset> <asp:Button ID="btnClear" runat="server" CausesValidation="False" OnClick="btnClear_Click"
                            Text="Refresh" UseSubmitBehavior="False" Visible="False" Width="70px" />


<asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="javascript:return DeleteConfirmation();"
                            Text="Delete" Visible="False" Width="70px" />


<asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Leave Cancel"
                            Visible="False" />


<asp:Panel ID="pnlEmpList" runat="server">
    <fieldset><legend>Admin Service</legend>
        <table><tr><td>
            <asp:Label ID="Label19" runat="server" CssClass="textlevelcenterfine" 
                Text="Select the employee for whom you want to forward leave application and press Load Employee button " 
                Width="500px"></asp:Label>


</td><td></td></tr><tr><td style="width: 3px"><asp:DropDownList ID="ddlEmpList" 
                runat="server" Width="500px"></asp:DropDownList>


</td><td><asp:Button ID="btnEmpDetails" runat="server" CausesValidation="False" 
                    OnClick="btnEmpDetails_Click" Text="Load Employee"></asp:Button>


</td></tr></table></fieldset> </asp:Panel>


<table><tr><td>
    <asp:Label ID="Label4" runat="server" CssClass="textlevel" Text="Office : " 
        Visible="False"></asp:Label>


</td><td><asp:Label ID="lblDept" runat="server" Visible="False" 
            Width="211px"></asp:Label>


</td><td style="width: 2px"><asp:Label ID="Label6" runat="server" CssClass="textlevel" Text="Designation :" Visible="False"></asp:Label>


</td><td><asp:Label ID="lblDesig" runat="server" Visible="False" Width="211px"></asp:Label>


</td><td style="width: 3px"></td><td style="width: 3px"></td></tr></table>
<asp:HiddenField ID="hfIsUpadate" runat="server" />


<asp:HiddenField ID="hfID" runat="server" />


<asp:HiddenField ID="hfLvPackStartDate" runat="server" />


<asp:HiddenField ID="hfLvPackEndDate" runat="server" />


<asp:HiddenField ID="hfLPakId" runat="server" />


&nbsp;&nbsp;<cc1:FilteredTextBoxExtender ID="ftbEmpId" runat="server" Enabled="True" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                            TargetControlID="txtEmpId" ValidChars="-"></cc1:FilteredTextBoxExtender>


<asp:HiddenField ID="hfSupervisor" runat="server" />


<asp:HiddenField ID="hfSupervisorEmail" runat="server" />


</ContentTemplate>
</cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2"><HeaderTemplate>
Requested Leave List
</HeaderTemplate>
<ContentTemplate>
<div style="margin: 10px 5px 5px 10px; height: 550px; width: 98%; _width: 97%; overflow: scroll;
                            border: solid 1px gray;"><asp:GridView ID="grLeaveApp" runat="server" AutoGenerateColumns="False" DataKeyNames="LvAppID,LTypeTitle,LTypeId,LNature,AppDate,LeaveStart,LeaveEnd,LDurInDays,InsertedBy,LTReason,EmpId,ResponsiveEmpId,AddrAtLeave,PhoneNo,IsHalfDay,ResumeDate"
                                EmptyDataText="No Record Found" Font-Size="9px" OnRowCommand="grLeaveApp_RowCommand"
                                OnSelectedIndexChanged="grLeaveApp_SelectedIndexChanged" PageSize="7" Width="100%"><HeaderStyle BackColor="#B3CDE4" Font-Bold="True" /><SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" /><AlternatingRowStyle BackColor="#EFF3FB" /><Columns><asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit"><ItemStyle CssClass="ItemStylecss" Width="50px" /></asp:ButtonField><asp:BoundField HeaderText="SL No."><ItemStyle CssClass="ItemStylecss" Width="50px" /></asp:BoundField><asp:BoundField DataField="EmpId" HeaderText="Emp No."><ItemStyle CssClass="ItemStylecssCenter" Width="50px" /></asp:BoundField><asp:BoundField DataField="LTypeTitle" HeaderText="Leave Name"><ItemStyle CssClass="ItemStylecss" Width="100px" /></asp:BoundField><asp:BoundField DataField="AppDate" HeaderText="Posting Date"><ItemStyle CssClass="ItemStylecss" Width="100px" /></asp:BoundField><asp:BoundField DataField="LeaveStart" HeaderText="Leave From"><ItemStyle CssClass="ItemStylecss" Width="100px" /></asp:BoundField><asp:BoundField DataField="LeaveEnd" HeaderText="Leave To"><ItemStyle CssClass="ItemStylecss" Width="100px" /></asp:BoundField><asp:BoundField DataField="LDurInDays" HeaderText="Total Days"><ItemStyle CssClass="ItemStylecssRight" Width="50px" /></asp:BoundField><asp:BoundField DataField="InsertedBy" HeaderText="Posted By"><ItemStyle CssClass="ItemStylecss" Width="100px" /></asp:BoundField><asp:ButtonField CommandName="ViewClick" HeaderText="View" Text="View"><ItemStyle CssClass="ItemStylecss" Width="50px" /></asp:ButtonField></Columns></asp:GridView></div>
</ContentTemplate>
</cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3"><HeaderTemplate>
Approved Leave List
</HeaderTemplate>
<ContentTemplate>
<div style="margin: 10px 5px 5px 10px; height: 550px; width: 98%; _width: 97%; overflow: scroll;
                            border: solid 1px gray;"><asp:GridView ID="grLeaveApprove" runat="server" Font-Size="9px" Width="100%" PageSize="7"
                                EmptyDataText="No Record Found" DataKeyNames="LvAppID,LTypeTitle,LTypeId,LNature,AppDate,LeaveStart,LeaveEnd,LDurInDays,UpdatedBy,LTReason,LAbbrName,EmpId,FullName"
                                AutoGenerateColumns="False" OnRowCommand="grLeaveApprove_RowCommand"><Columns><asp:BoundField HeaderText="SL No."><ItemStyle Width="3%" CssClass="ItemStylecss"></ItemStyle></asp:BoundField><asp:BoundField DataField="EmpId" HeaderText="Emp ID and Name"><ItemStyle Width="30%" CssClass="ItemStylecss"></ItemStyle></asp:BoundField><asp:BoundField DataField="LTypeTitle" HeaderText="Leave Name"><ItemStyle Width="10%" CssClass="ItemStylecss"></ItemStyle></asp:BoundField><asp:BoundField DataField="AppDate" HeaderText="Application Date"><ItemStyle Width="7%" CssClass="ItemStylecss"></ItemStyle></asp:BoundField><asp:BoundField DataField="LeaveStart" HeaderText="Leave From"><ItemStyle Width="7%" CssClass="ItemStylecss"></ItemStyle></asp:BoundField><asp:BoundField DataField="LeaveEnd" HeaderText="Leave To"><ItemStyle Width="7%" CssClass="ItemStylecss"></ItemStyle></asp:BoundField><asp:BoundField DataField="LDurInDays" HeaderText="Total Days"><ItemStyle Width="4%" CssClass="ItemStylecssRight"></ItemStyle></asp:BoundField><asp:BoundField DataField="UpdatedBy" HeaderText="Approve By"><ItemStyle Width="15%" CssClass="ItemStylecss"></ItemStyle></asp:BoundField><asp:ButtonField HeaderText="View" Text="View" CommandName="ViewClick"><ItemStyle Width="5%" CssClass="ItemStylecss"></ItemStyle></asp:ButtonField></Columns><SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True"></SelectedRowStyle><HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle><AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle></asp:GridView></div>
</ContentTemplate>
</cc1:TabPanel>
            </cc1:TabContainer>
        </div>
        </contenttemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
