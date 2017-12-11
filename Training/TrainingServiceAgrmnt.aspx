<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="TrainingServiceAgrmnt.aspx.cs" Inherits="Training_TrainingServiceAgrmnt"
    Title="Training Service Agreement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js">
        //Date Time Picker script 
    </script>
    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>
    <script language="javascript" type="text/javascript">
        function TrainingTypeChange(x) {
            if (x != 1) {
                document.getElementById("<%= ddlCountry.ClientID %>").disabled = true;
                document.getElementById("<%= ddlCountry.ClientID %>").selectedIndex = 0;
            }
            else
                document.getElementById("<%= ddlCountry.ClientID %>").disabled = false;

        }
        function TranJectionChange(x) {
            if (x == 0)
                document.getElementById("<%= txtTrainingName.ClientID %>").disabled = false;
            else
                document.getElementById("<%= txtTrainingName.ClientID %>").disabled = true;
        }
        function myFunction() {
            var tb1 = document.getElementById("<%= txtTrainingName.ClientID %>");
            if (tb1.value.length > 0)
                document.getElementById("<%= ddlTraining.ClientID %>").disabled = true;
            else
                document.getElementById("<%= ddlTraining.ClientID %>").disabled = false;
        }
        function ResourceChange(x) {
            if (x == 0)
                document.getElementById("<%= this.txtResourcePerson.ClientID %>").disabled = false;
            else
                document.getElementById("<%= this.txtResourcePerson.ClientID %>").disabled = true;
        }
        function ResourceListChange() {
            var tb1 = document.getElementById("<%= this.txtResourcePerson.ClientID %>");
            if (tb1.value.length > 0)
                document.getElementById("<%= ddlResourcePersonId.ClientID %>").disabled = true;
            else
                document.getElementById("<%= ddlResourcePersonId.ClientID %>").disabled = false;
        }



        //        function ShowTextLength() {
        //            var objLbl = document.getElementById("txtTrainingName");
        //            if (objTxt.value.length > 0) {
        //                alert("Incocked");
        //                document.getElementById("ddlTrainingID").readOnly = true;
        //            }
        //        }
    </script>
    <div class="departmentSetup" style="width: 86%; min-height: 750px; height: 727px;">
        <div id='formhead2'>
            <div style="width: 97%; float: left;">
                Training & Service</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <!--Div for group-->
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%"
            Height="650px">
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Width="98%">
                <HeaderTemplate>
                    Input Training Service
                
</HeaderTemplate>
                
<ContentTemplate>
                    <div style="background-color: #EFF3FB; margin-bottom: 10px; margin-left: 13px; margin-right: 13px;">
                        <fieldset>
                            <asp:HiddenField ID="hfID" runat="server" />

                            <asp:HiddenField ID="hfIsUpdate" runat="server" />

                            <table>
                                <tr>
                                    <td class="textlevel">
                                        Emp Id :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmpID" runat="server" Width="80px"></asp:TextBox>

                                        <asp:ImageButton ID="imgBtnSearch" runat="server" CausesValidation="False" ClientIDMode="Predictable"
                                            Height="19px" ImageUrl="~/Images/Search_Icon.jpg" OnClick="imgBtnSearch_Click"></asp:ImageButton>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmpID"
                                            ErrorMessage="*"></asp:RequiredFieldValidator>

                                    </td>
                                    <td>
                                        <asp:Button ID="btnOpenPDF" runat="server" OnClick="lnkOpenPPDF_Click" Text="Open PDF File"
                                            UseSubmitBehavior="False" Width="149px"></asp:Button>

                                        <asp:LinkButton ID="lnkOpenPPDF" runat="server" OnClick="lnkOpenPPDF_Click"></asp:LinkButton>

                                    </td>
                                </tr>
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
                                        JobTitle :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblJobTitle" runat="server"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td class="textlevel">
                                        Sector :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSector" runat="server"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td class="textlevel">
                                        Department :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDept" runat="server"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td class="textlevel">
                                        Entry Date :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEntryDate" runat="server" Width="80px"></asp:TextBox>

                                        &nbsp;<a href="javascript:NewCal('<%= txtEntryDate.ClientID %>','ddmmyyyy')"><img
                                            alt="Pick a date" height="16" src="../images/cal.gif" style="border: 0px;" width="16" /></a>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                            ControlToValidate="txtEntryDate" CssClass="validator" ErrorMessage="Invalid Date"
                                            ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>

                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                    <div style="margin-left: 13px; margin-right: 13px; margin-top: 10px;">
                        <div style="width: 46%; float: left;">
                            <fieldset>
                                <table>
                                    <tr>
                                        <td class="textlevel">
                                            Traning Type :
                                        </td>
                                        <td colspan="2">
                                            <asp:DropDownList ID="ddlTrainType" runat="server" CssClass="textlevelleft" onchange="TrainingTypeChange(this.selectedIndex);"
                                                OnSelectedIndexChanged="ddlTrainType_SelectedIndexChanged" Width="255px"><asp:ListItem Text="-- Select --" Value="-1"></asp:ListItem>
<asp:ListItem Text="External Overseas" Value="O"></asp:ListItem>
<asp:ListItem Text="External In Country" Value="C"></asp:ListItem>
<asp:ListItem Text="In House" Value="I"></asp:ListItem>
</asp:DropDownList>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            Training Name :
                                        </td>
                                        <td colspan="2">
                                            <asp:DropDownList ID="ddlTraining" runat="server" CssClass="textlevelleft" onchange="TranJectionChange(this.selectedIndex);"
                                                Width="255px"></asp:DropDownList>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtTrainingName" runat="server" onkeyup="myFunction()" Width="252px"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            Fiscal Year :
                                        </td>
                                        <td colspan="2">
                                            <asp:DropDownList ID="ddlFiscalYr" runat="server" CssClass="textlevelleft" Width="255px"></asp:DropDownList>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            Learning Area :
                                        </td>
                                        <td colspan="2">
                                            <asp:DropDownList ID="ddlLAreaId" runat="server" CssClass="textlevelleft" Width="255px"></asp:DropDownList>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevelleft">
                                            Resource Person/Agency :
                                        </td>
                                        <td colspan="2">
                                            <asp:DropDownList ID="ddlResourcePersonId" runat="server" CssClass="textlevelleft"
                                                onchange="ResourceChange(this.selectedIndex);" Width="255px"></asp:DropDownList>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            &nbsp;
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtResourcePerson" runat="server" Width="252px" onkeyup="ResourceListChange()"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            Country Name :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="textlevelleft" Width="255px"></asp:DropDownList>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            Contact Details :
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtContactDtl" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            Training Start Date :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTrnStartDate" runat="server" Width="80px"></asp:TextBox>

                                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtTrnStartDate"
                                                ErrorMessage="*"></asp:RequiredFieldValidator>

                                            &nbsp;<a href="javascript:NewCal('<%= txtTrnStartDate.ClientID %>','ddmmyyyy')"><img
                                                alt="Pick a date" height="16" src="../images/cal.gif" style="border-right: 0px;
                                                border-top: 0px; border-left: 0px; border-bottom: 0px" width="16" /></a>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                                                ControlToValidate="txtTrnStartDate" CssClass="validator" ErrorMessage="Invalid Date"
                                                ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>

                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            Training End Date :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTrnEndDate" runat="server" Width="80px"></asp:TextBox>

                                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtTrnEndDate"
                                                ErrorMessage="*"></asp:RequiredFieldValidator>

                                            &nbsp;<a href="javascript:NewCal('<%= txtTrnEndDate.ClientID %>','ddmmyyyy')"><img
                                                alt="Pick a date" height="16" src="../images/cal.gif" style="border-right: 0px;
                                                border-top: 0px; border-left: 0px; border-bottom: 0px" width="16" /></a>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                                                ControlToValidate="txtTrnEndDate" CssClass="validator" ErrorMessage="Invalid Date"
                                                ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>

                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            Remarks :
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>

                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                        <div style="width: 53%; float: right; text-align: left;">
                            <fieldset>
                                <table>
                                    <tr>
                                        <td class="textlevel">
                                            Need Type :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlNeedType" runat="server" CssClass="textlevelleft"><asp:ListItem Text="-- Select --" Value="-1"></asp:ListItem>
<asp:ListItem Text="Organization/Team" Value="O"></asp:ListItem>
<asp:ListItem Text="Individual" Value="I"></asp:ListItem>
</asp:DropDownList>

                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            Running Rate :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRunningRate" runat="server" CssClass="TextBoxAmt60" Width="80px"></asp:TextBox>

                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                FilterType="Custom, Numbers" TargetControlID="txtRunningRate" ValidChars="."></cc1:FilteredTextBoxExtender>

                                        </td>
                                        <td class="textlevel">
                                            Rate to Use :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRateToUse" runat="server" CssClass="TextBoxAmt60" Width="80px"></asp:TextBox>

                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                FilterType="Custom, Numbers" TargetControlID="txtRateToUse" ValidChars="."></cc1:FilteredTextBoxExtender>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            Service Agreement :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlServAgreement" runat="server" AutoPostBack="True" CssClass="textlevelleft"
                                                OnSelectedIndexChanged="ddlServAgreement_SelectedIndexChanged"><asp:ListItem Text="-- Select --" Value="-1"></asp:ListItem>
<asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
<asp:ListItem Text="No" Value="N"></asp:ListItem>
</asp:DropDownList>

                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="PanelAgreement" runat="server" Visible="False">
                                    <fieldset>
                                        <table>
                                            <tr>
                                                <td class="textlevel">
                                                    Start Date :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAgrStartDate" runat="server" Width="80px"></asp:TextBox>

                                                    &#160;<a href="javascript:NewCal('<%= txtAgrStartDate.ClientID %>','ddmmyyyy')"><img
                                                        alt="Pick a date" height="16" src="../images/cal.gif" style="border-right: 0px;
                                                        border-top: 0px; border-left: 0px; border-bottom: 0px" width="16" /></a>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAgrStartDate"
                                                        ErrorMessage="*"></asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                                        ControlToValidate="txtAgrStartDate" CssClass="validator" ErrorMessage="Invalid Date"
                                                        ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>

                                                </td>
                                                <td class="textlevel">
                                                    End Date :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAgrEndDate" runat="server" Width="80px"></asp:TextBox>

                                                    &#160;<a href="javascript:NewCal('<%= txtAgrEndDate.ClientID %>','ddmmyyyy')"><img
                                                        alt="Pick a date" height="16" src="../images/cal.gif" style="border-right: 0px;
                                                        border-top: 0px; border-left: 0px; border-bottom: 0px" width="16" /></a>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAgrEndDate"
                                                        ErrorMessage="*"></asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                                        ControlToValidate="txtAgrEndDate" CssClass="validator" ErrorMessage="Invalid Date"
                                                        ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textlevel">
                                                    Agreement Period :
                                                </td>
                                                <td colspan="3" class="textlevelleft">
                                                    <asp:TextBox ID="txtAgrPeriod" runat="server" Width="80px" OnTextChanged="txtAgrPeriod_TextChanged"
                                                        CssClass="TextBoxAmt60"></asp:TextBox>

                                                    (Days)
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers"
                                                        ValidChars="." TargetControlID="txtAgrPeriod" Enabled="True"></cc1:FilteredTextBoxExtender>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textlevelleft">
                                                    Estimated Agreement Amount(BDT) :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEstAgrAmtBDT" runat="server" CssClass="TextBoxAmt60" ReadOnly="True"
                                                        Width="80px"></asp:TextBox>

                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                        FilterType="Custom, Numbers" TargetControlID="txtEstAgrAmtBDT" ValidChars="."></cc1:FilteredTextBoxExtender>

                                                </td>
                                                <td class="textlevel">
                                                    Actual Agreement Amount(BDT) :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtActAgrAmtBDT" runat="server" CssClass="TextBoxAmt60" OnTextChanged="txtActAgrAmtBDT_TextChanged"
                                                        Width="80px"></asp:TextBox>

                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                                        FilterType="Custom, Numbers" TargetControlID="txtActAgrAmtBDT" ValidChars="."></cc1:FilteredTextBoxExtender>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textlevelleft">
                                                    Estimated Agreement Amount(USD) :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEstAgrAmtUSD" runat="server" CssClass="TextBoxAmt60" ReadOnly="True"
                                                        Width="80px"></asp:TextBox>

                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                                        FilterType="Custom, Numbers" TargetControlID="txtEstAgrAmtUSD" ValidChars="."></cc1:FilteredTextBoxExtender>

                                                </td>
                                                <td class="textlevel">
                                                    Actual Agreement Amount(USD) :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtActAgrAmtUSD" runat="server" CssClass="TextBoxAmt60" OnTextChanged="txtActAgrAmtUSD_TextChanged"
                                                        Width="80px"></asp:TextBox>

                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                        FilterType="Custom, Numbers" TargetControlID="txtActAgrAmtUSD" ValidChars="."></cc1:FilteredTextBoxExtender>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="textlevel">
                                                    Agreement Remarks :
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtAgrRemarks" runat="server" Rows="2" TextMode="MultiLine" Width="98%"></asp:TextBox>

                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </asp:Panel>

                                <table>
                                    <tr>
                                        <td class="textlevel">
                                            Training Cost(BDT) :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTrainingCostBDT" runat="server" CssClass="TextBoxAmt60" OnTextChanged="txtTrainingCostBDT_TextChanged"
                                                Width="80px"></asp:TextBox>

                                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtTrnStartDate"
                                                ErrorMessage="*"></asp:RequiredFieldValidator>

                                            &nbsp;&nbsp;<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtTrainingCostBDT"
                                                ValidChars="."></cc1:FilteredTextBoxExtender>

                                        </td>
                                        <td>
                                            &nbsp;&nbsp;
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            Training Cost(USD) :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTrainingCostUSD" runat="server" CssClass="TextBoxAmt60" OnTextChanged="txtTrainingCostUSD_TextChanged"
                                                Width="80px"></asp:TextBox>

                                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtTrnStartDate"
                                                ErrorMessage="*"></asp:RequiredFieldValidator>

                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" Enabled="True"
                                                FilterType="Custom, Numbers" TargetControlID="txtTrainingCostUSD" ValidChars="."></cc1:FilteredTextBoxExtender>

                                        </td>
                                        <td>
                                            <asp:Button ID="btnCalculate" runat="server" CausesValidation="False" 
                                                OnClick="btnCalculate_Click" Text="Calculate" UseSubmitBehavior="False" 
                                                Width="70px" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            Sponsored by SC:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSponsoredBy" runat="server" CssClass="textlevelleft"><asp:ListItem Text="-- Select --" Value="-1"></asp:ListItem>
<asp:ListItem Text="Sponsored by SC" Value="S"></asp:ListItem>
<asp:ListItem Text="Sponsored by Other" Value="N"></asp:ListItem>
<asp:ListItem Text="Both" Value="B"></asp:ListItem>
</asp:DropDownList>

                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            SC Cost (%) :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSCCostPercent" runat="server" CssClass="TextBoxAmt60" OnTextChanged="txtSCCostPercent_TextChanged"
                                                Width="80px"></asp:TextBox>

                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                                FilterType="Custom, Numbers" TargetControlID="txtSCCostPercent" ValidChars="."></cc1:FilteredTextBoxExtender>

                                        </td>
                                        <td class="textlevel">
                                            SC Cost (Tk.) :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSCCostBDT" runat="server" CssClass="TextBoxAmt60" OnTextChanged="txtSCCostBDT_TextChanged"
                                                Width="80px"></asp:TextBox>

                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True"
                                                FilterType="Custom, Numbers" TargetControlID="txtSCCostBDT" ValidChars="."></cc1:FilteredTextBoxExtender>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                        </td>
                                        <td>
                                        </td>
                                        <td class="textlevel">
                                            SC Cost (USD) :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSCCostUSD" runat="server" CssClass="TextBoxAmt60" OnTextChanged="txtSCCostUSD_TextChanged"
                                                Width="80px"></asp:TextBox>

                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" Enabled="True"
                                                FilterType="Custom, Numbers" TargetControlID="txtSCCostUSD" ValidChars="."></cc1:FilteredTextBoxExtender>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            Others Cost (%) :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOtherCostPercent" runat="server" CssClass="TextBoxAmt60" OnTextChanged="txtOtherCostPercent_TextChanged"
                                                Width="80px"></asp:TextBox>

                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" Enabled="True"
                                                FilterType="Custom, Numbers" TargetControlID="txtOtherCostPercent" ValidChars="."></cc1:FilteredTextBoxExtender>

                                        </td>
                                        <td class="textlevel">
                                            Others Cost (Tk.) :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOtherCostPerBDT" runat="server" CssClass="TextBoxAmt60" OnTextChanged="txtOtherCostPerBDT_TextChanged"
                                                Width="80px"></asp:TextBox>

                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" Enabled="True"
                                                FilterType="Custom, Numbers" TargetControlID="txtOtherCostPerBDT" ValidChars="."></cc1:FilteredTextBoxExtender>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                        </td>
                                        <td>
                                        </td>
                                        <td class="textlevel">
                                            Others Cost (USD) :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOtherCostPerUSD" runat="server" CssClass="TextBoxAmt60" OnTextChanged="txtOtherCostPerUSD_TextChanged"
                                                Width="80px"></asp:TextBox>

                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" Enabled="True"
                                                FilterType="Custom, Numbers" TargetControlID="txtOtherCostPerUSD" ValidChars="."></cc1:FilteredTextBoxExtender>

                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                    </div>
                    <div class="DivCommand1" style="float: left;">
                        <div class="DivCommandL">
                            <asp:Button ID="btnClear" runat="server" Text="Refresh" Width="70px" OnClick="btnClear_Click"
                                UseSubmitBehavior="False" CausesValidation="False" />

                        </div>
                        <div class="DivCommandR">
                            <asp:Button ID="btnSaveRefresh" runat="server" Text="Save &amp; Refresh" Width="122px"
                                OnClick="btnSaveRefresh_Click" Style="margin-left: 0px" />

                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Width="70px" />

                            <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                                OnClick="btnDelete_Click" />

                        </div>
                    </div>
                
</ContentTemplate>
            
</cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel1" Width="90%">
                <HeaderTemplate>
                    Training List
                
</HeaderTemplate>
                
<ContentTemplate>
                    <div class="GridFormat1" style="height: 500px; overflow: auto">
                        <asp:GridView ID="grList" runat="server" DataKeyNames="TraServiceID,EmpId" AutoGenerateColumns="False"
                            EmptyDataText="No Record Found" Font-Size="9px" Width="99%" OnRowCommand="grList_RowCommand"
                            AllowPaging="False" OnPageIndexChanging="grList_PageIndexChanging">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                            </SelectedRowStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                            <Columns>
                                <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="FullName" HeaderText="Employee Name">
                                    <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TrainingName" HeaderText="Training Name">
                                    <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TranTypeName" HeaderText="Training Type">
                                    <ItemStyle CssClass="ItemStylecss" Width="18%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ServAgreement" HeaderText="Service Agreement">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TrnStartDate" HeaderText="Training Start Date" DataFormatString="{0:dd-MMM-yyyy}">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TrnEndDate" HeaderText="Training End Date" DataFormatString="{0:dd-MMM-yyyy}">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <!--Grid view Code starts-->
                    </div>
                
</ContentTemplate>
            
</cc1:TabPanel>
        </cc1:TabContainer>
        <br />
    </div>
</asp:Content>
