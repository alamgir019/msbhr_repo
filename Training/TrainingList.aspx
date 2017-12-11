<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="TrainingList.aspx.cs" Inherits="Training_TrainingList" %>

<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js">
        //Date Time Picker script
    </script>
    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>
    <div class="empTrainForm">
        <div id="formhead1">
            <div style="width: 96%; float: left;">
                Training List</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%"
            Height="670px">
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Width="860px"
                Height="650px">
                <HeaderTemplate>
                    Add Training Setup
                </HeaderTemplate>
                <ContentTemplate>
                    <!--Div for group-->
                    <div class="MsgBox">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                    <div class="officeSetupInner">
                        <fieldset>
                            <!--Div for Controls-->
                            <asp:HiddenField ID="hfId" runat="server" />
                            <asp:HiddenField ID="hfIsUpdate" runat="server" />
                            <table>
                                <tr>
                                    <td style="height: 22px">
                                    </td>
                                    <td style="height: 22px">
                                        <asp:CheckBox ID="chkIsSchedule" runat="server" CssClass="textlevelleft" Text="Is Schedule"
                                            Checked="True" Enabled="False" />
                                    </td>
                                    <td style="height: 22px">
                                    </td>
                                    <td style="height: 22px">
                                        <asp:CheckBox ID="chkInActive" runat="server" CssClass="textlevelleft" Text="Make Inactive" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" CssClass="textlevel" Text="Training Name:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTrainingName" runat="server" CssClass="textlevelleft" Width="200px"
                                            ToolTip="Select Training Name" OnSelectedIndexChanged="ddlTrainingName_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" CssClass="textlevel" Text="Schedule Name :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSchedule" runat="server" CssClass="textlevelleft" Width="200px"
                                            ToolTip="Select" OnSelectedIndexChanged="ddlSchedule_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" CssClass="textlevel" Text="Start Date :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtStrDate" runat="server" Width="89px" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" CssClass="textlevel" Text="End Date :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEndDate" runat="server" Width="89px" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" CssClass="textlevel" Text="Duration :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDuration" runat="server" Width="150px" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" CssClass="textlevel" Text="No Of Person :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNoOfPerson" runat="server" Width="150px" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label25" runat="server" CssClass="textlevel" Text="Location Name :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="textlevelleft" Width="200px"
                                            ToolTip="Select">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label17" runat="server" CssClass="textlevel" Text="Organised By :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlOrganisedBy" runat="server" CssClass="textlevelleft" Width="200px"
                                            ToolTip="Select">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Remarks :</td>
                                    <td colspan="3" >
                                        <asp:TextBox ID="txtRemark" runat="server" CssClass="textlevelleft" 
                                            Width="552px"></asp:TextBox>
                                    </td>
                                    
                                </tr>
                            </table>
                        </fieldset>
                        <br />
                        <fieldset>
                            <legend>Reporting</legend>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label18" runat="server" CssClass="textlevel" Text="Venue :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlVenue" runat="server" CssClass="textlevelleft" Width="200px"
                                            OnSelectedIndexChanged="ddlVenue_SelectedIndexChanged" AutoPostBack="True" ToolTip="Select Training Name">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label19" runat="server" CssClass="textlevel" Text="Address :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAddress" runat="server" Width="150px" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label20" runat="server" CssClass="textlevel" Text="Date :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDate" runat="server" Width="89px"></asp:TextBox>
                                        <a href="javascript:NewCal('<%= txtDate.ClientID %>','ddmmyyyy')">
                                            <img alt="Pick a date" height="16" src="../images/cal.gif" style="border-right: 0px;
                                                border-top: 0px; border-left: 0px; border-bottom: 0px" width="16" /></a>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDate"
                                            CssClass="validator" ErrorMessage="Invalid" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                                            ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label22" runat="server" CssClass="textlevel" Text="Time :"></asp:Label>
                                    </td>
                                    <td>
                                        <cc2:TimeSelector ID="tsTime" runat="server" DisplaySeconds="False" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);"
                                            AmPm="PM" BorderColor="Silver" Date="01/01/0001 12:00:00" Hour="12" Minute="0"
                                            Second="0" SelectedTimeFormat="Twelve">
                                        </cc2:TimeSelector>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <br />
                        <fieldset>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" CssClass="textlevel" Text="Employee Name :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTraineeName" runat="server" CssClass="textlevelleft" Width="200px"
                                            ToolTip="Select" OnSelectedIndexChanged="ddlTraineeName_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label23" runat="server" CssClass="textlevel" Text="Designation :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDesignation" runat="server" Width="150px" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td style="height: 22px">
                                        <asp:CheckBox ID="chkIsResidential" runat="server" CssClass="textlevelleft" Text="Make Non Residential" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label24" runat="server" CssClass="textlevel" Text="Dept/Clinic :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDept" runat="server" Width="150px" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label21" runat="server" CssClass="textlevel" Text="Funded By :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFundedby" runat="server" CssClass="textlevelleft" Width="200px"
                                            ToolTip="Select Training Name">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" Width="60px" CausesValidation="False"
                                            OnClick="btnAdd_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                    <div class="GridFormat1">
                        <!--Grid view Code starts-->
                        <asp:GridView ID="grList" runat="server" DataKeyNames="TraineeId,TraineeName,Designation,Dept,Fundedby,ProjectName,IsResidential"
                            AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" Width="99%"
                            OnRowCommand="grList_RowCommand" OnRowDeleting="grList_RowDeleting" OnSelectedIndexChanged="grList_SelectedIndexChanged">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                            </SelectedRowStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                            <Columns>
                                <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%" />
                                </asp:ButtonField>
                                <asp:ButtonField CommandName="Delete" HeaderText="Delete" Text="Delete">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="TraineeName" HeaderText="Employee Name">
                                    <ItemStyle CssClass="ItemStylecss" Width="50%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Designation" HeaderText="Designation">
                                    <ItemStyle CssClass="ItemStylecss" Width="50%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Dept" HeaderText="Clinic Name">
                                    <ItemStyle CssClass="ItemStylecss" Width="80%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ProjectName" HeaderText="Funded by">
                                    <ItemStyle CssClass="ItemStylecssCenter" Width="20%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IsResidential" HeaderText="Is Residential">
                                    <ItemStyle CssClass="ItemStylecssCenter" Width="20%"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" CssClass="textlevel" Text="Sign By1 :"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSignBy1" runat="server" CssClass="textlevelleft" Width="200px"
                                    ToolTip="Select Training Name">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" CssClass="textlevel" Text="Sign By2 :"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSignBy2" runat="server" CssClass="textlevelleft" Width="200px"
                                    ToolTip="Select Training Name">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" CssClass="textlevel" Text="Sign By3 :"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSignBy3" runat="server" CssClass="textlevelleft" ToolTip="Select Training Name"
                                    Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" CssClass="textlevel" Text="CC :"></asp:Label>
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="txtCC" runat="server" Width="720px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCC"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" CssClass="textlevel" Text="Administrative Guideline :"></asp:Label>
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="txtAdminGuideline" runat="server" Width="720px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtAdminGuideline"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <div class="DivCommand1">
                        <div class="DivCommandL">
                            <asp:Button ID="btnClear" runat="server" Text="Refresh" Width="70px" OnClick="btnClear_Click"
                                CausesValidation="False" />
                        </div>
                        <div class="DivCommandR">
                            <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click" />
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                                OnClick="btnDelete_Click" />
                        </div>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel1" Width="860px"
                Height="450px">
                <HeaderTemplate>
                    Training Setup List
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="GridFormat3">
                        <asp:GridView ID="grTrainingListSetup" runat="server" DataKeyNames="TrainListId,TrainId,TrainName,ScheduleID,ScheduleName,VenueId,VenueName,
                        VenueAddress,onDate,onTime,SignBy1,SignBy1Name,SignBy2,SignBy2Name,SignBy3,SignBy3Name,CC,AdminGuideline,OrganizedBy,OrganizedByName,IsActive,Remarks"
                            AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" Width="99%"
                            OnRowCommand="grTrainingListSetup_RowCommand">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                            </SelectedRowStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                            <Columns>
                                <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="TrainName" HeaderText="Training Name">
                                    <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ScheduleName" HeaderText="Training Schedule">
                                    <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="VenueName" HeaderText="Venue">
                                    <ItemStyle CssClass="ItemStylecss" Width="40%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="VenueAddress" HeaderText="Address">
                                    <ItemStyle CssClass="ItemStylecss" Width="60%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="onDate" HeaderText="on Date">
                                    <ItemStyle CssClass="ItemStylecss" Width="50%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="onTime" HeaderText="on Time">
                                    <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SignBy1Name" HeaderText="Sign By1">
                                    <ItemStyle CssClass="ItemStylecss" Width="50%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SignBy2Name" HeaderText="Sign By2">
                                    <ItemStyle CssClass="ItemStylecss" Width="50%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SignBy3Name" HeaderText="Sign By3">
                                    <ItemStyle CssClass="ItemStylecss" Width="50%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CC" HeaderText="CC">
                                    <ItemStyle CssClass="ItemStylecss" Width="50%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="AdminGuideline" HeaderText="Admin Guideline">
                                    <ItemStyle CssClass="ItemStylecss" Width="80%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="OrganizedByName" HeaderText="Organized By">
                                    <ItemStyle CssClass="ItemStylecss" Width="80%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IsActive" HeaderText="Is Active">
                                    <ItemStyle CssClass="ItemStylecssCenter" Width="30%"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <!--Grid view Code Ends-->
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </div>
    <br />
</asp:Content>
