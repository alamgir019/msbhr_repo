<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="TrainingScheduleDtl.aspx.cs" Inherits="Training_TrainingScheduleDtl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JScripts/jquery-1.4.2.min.js"></script>
    <%--<script src="../JScripts/jquery-1.2.3.min.js" type="text/javascript"></script>--%>
    <script language="javascript" type="text/javascript" src="../JScripts/jquery-ui-1.8.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js"></script>
    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $(".CourseCordinator").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "employeelist.asmx/GetEmployee",
                        data: "{ 'empname': '" + request.term + "' }",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item.FullName
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                minLength: 2
            });
        });
        $(document).ready(function () {
            calculateDuration("");
        });

        function txtDuration_onclick() {
            calculateDuration("Please Enter Date");
        }
        function calculateDuration(msg) {
            document.getElementById('<%=lblValidMsg.ClientID%>').innerHTML = '';
            if (document.getElementById('<%=txtStrDate.ClientID%>').value == '') {
                // alert('Please Enter Start Date.');
                document.getElementById('<%=lblValidMsg.ClientID%>').innerHTML = msg;
                document.getElementById('<%=txtDuration.ClientID%>').value = '';
                return;
            }
            else if (document.getElementById('<%=txtEndDate.ClientID%>').value == '') {
                //alert('Please Enter End Date.');
                document.getElementById('<%=lblValidMsg.ClientID%>').innerHTML = msg;
                document.getElementById('<%=txtDuration.ClientID%>').value = '';
                return;
            }

            var strDate = stringToDate(document.getElementById('<%=txtStrDate.ClientID%>').value, "dd/MM/yyyy", "/");
            var endDate = stringToDate(document.getElementById('<%=txtEndDate.ClientID%>').value, "dd/MM/yyyy", "/");
            var diff = daydiff(strDate, endDate) + 1;
            if (diff < 0) {
                //alert('Start Date Cannot be Greater Than End Date.');
                document.getElementById('<%=lblValidMsg.ClientID%>').innerHTML = 'Start Date Cannot be Greater Than End Date.';
                document.getElementById('<%=txtDuration.ClientID%>').value = '';
                return;
            }
            else {
                document.getElementById('<%=txtDuration.ClientID%>').value = diff;
            }
        }
        function daydiff(first, second) {
            return Math.round((second - first) / (1000 * 60 * 60 * 24));
        }

        function stringToDate(_date, _format, _delimiter) {
            var formatLowerCase = _format.toLowerCase();
            var formatItems = formatLowerCase.split(_delimiter);
            var dateItems = _date.split(_delimiter);
            var monthIndex = formatItems.indexOf("mm");
            var dayIndex = formatItems.indexOf("dd");
            var yearIndex = formatItems.indexOf("yyyy");
            var month = parseInt(dateItems[monthIndex]);
            month -= 1;
            var formatedDate = new Date(dateItems[yearIndex], month, dateItems[dayIndex]);
            return formatedDate;
        }      
    </script>
    <div class="empTrainForm">
        <div id="formhead1">
            <div style="width: 96%; float: left;">
                Training Schedule</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <!--Div for group-->
        <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%"
            Height="450px">
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Width="860px" Height="430px">
                <HeaderTemplate>
                    Add Training Schedule                
                </HeaderTemplate>
                
                <ContentTemplate>
                    <div class="MsgBox">
                        <!--Div for msg-->
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <asp:Label ID="lblValidMsg" runat="server"></asp:Label>
                    </div>
                    <div class="officeSetupInner">
                        <fieldset>
                            <!--Div for Controls-->
                            <asp:HiddenField ID="hfId" runat="server" />

                            <asp:HiddenField ID="hfIsUpdate" runat="server" />

                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" CssClass="textlevel" Text="Training Name :"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTrName" runat="server" CssClass="textlevelleft" Width="200px"
                                            ToolTip="Select Training Nmae"></asp:DropDownList>

                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkInActive" runat="server" CssClass="textlevelleft" Text="Make Inactive" />

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" CssClass="textlevel" Text="Location :"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="textlevelleft" Width="200px"
                                            ToolTip="Select Training Location" AutoPostBack="True" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged"></asp:DropDownList>

                                    </td>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" CssClass="textlevel" Text="Venue :"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlVenue" runat="server" CssClass="textlevelleft" Width="200px"
                                            ToolTip="Select Training Location" AutoPostBack="True" OnSelectedIndexChanged="ddlVenue_SelectedIndexChanged"></asp:DropDownList>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" CssClass="textlevel" Text="Start Date :"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtStrDate" runat="server" Width="89px"></asp:TextBox>

                                        <a href="javascript:NewCal('<%= txtStrDate.ClientID %>','ddmmyyyy')">
                                            <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                                height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtStrDate"
                                            CssClass="validator" ErrorMessage="Invalid" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtStrDate"></asp:RequiredFieldValidator>

                                    </td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" CssClass="textlevel" Text="End Date :"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEndDate" runat="server" Width="89px"></asp:TextBox>

                                        <a href="javascript:NewCal('<%= txtEndDate.ClientID %>','ddmmyyyy')">
                                            <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                                height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEndDate"
                                            CssClass="validator" ErrorMessage="Invalid" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtEndDate"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" CssClass="textlevel" Text="Duration :"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDuration" runat="server" Width="200px" ReadOnly="True" 
                                            onclick="txtDuration_onclick()"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtDuration"></asp:RequiredFieldValidator>

                                    </td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" CssClass="textlevel" Text="No Of Person :"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNoOfPerson" runat="server" Width="200px" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);"
                                            MaxLength="3"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtNoOfPerson"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" CssClass="textlevel" Text="Course Coordinator :"></asp:Label>

                                    </td>
                                    <td>
                                        <%--<asp:DropDownList ID="ddlCourseCordinator" runat="server" CssClass="textlevelleft"
                                            Width="200px" ToolTip="Select Training Location"></asp:DropDownList>--%>
                                <asp:TextBox ID="txtCourseCordinator" class="CourseCordinator textlevelleft" runat="server" Width="200px"></asp:TextBox>

                                    </td>
                                    <td class="textlevel">
                                        Remarks :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" MaxLength="1000" Width="200px" ID="txtRemarks"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" CssClass="textlevel" Text="Select Residential :"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlResidential" runat="server" CssClass="textlevelleft" Width="200px"
                                            ToolTip="Select Residential By"><asp:ListItem Text="Select" Value="-1" />
                                            <asp:ListItem Text="Residential" Value="R" />
                                            <asp:ListItem Text="Non-Residential" Value="N" />
                                            <asp:ListItem Text="Residential/Non-Residential" Value="RN" />
                                            <asp:ListItem Text="Outside" Value="O" />
                                        </asp:DropDownList>

                                    </td>
                                    <td style="height: 22px" class="textlevel">
                                        &nbsp;
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <fieldset>
                            <table>
                                <tr>
                                    <td class="textlevel">
                                        Funded By :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFundedby" runat="server" CssClass="textlevelleft" Width="200px"
                                            ToolTip="Select Funded By"></asp:DropDownList>

                                    </td>
                                    <td class="textlevel">
                                        Participant No
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPartNo" runat="server" MaxLength="3" Width="100px"></asp:TextBox>

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
                        <asp:GridView ID="grDtlsList" runat="server" DataKeyNames="FundedBy" AutoGenerateColumns="False"
                            EmptyDataText="No Record Found" Font-Size="9px" Width="99%" 
                            OnRowCommand="grDtlsList_RowCommand" 
                            onselectedindexchanged="grDtlsList_SelectedIndexChanged">
<AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
<Columns>
<asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
<ItemStyle CssClass="ItemStylecss" Width="10%" />
</asp:ButtonField>
<asp:ButtonField CommandName="Delete" HeaderText="Delete" Text="Delete">
<ItemStyle CssClass="ItemStylecss" Width="10%" />
</asp:ButtonField>
<asp:BoundField DataField="ProjectName" HeaderText="Funded by">
<ItemStyle CssClass="ItemStylecssCenter" Width="20%"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ParticipantNo" HeaderText="Participant No">
<ItemStyle CssClass="ItemStylecssCenter" Width="20%"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>

<SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True"></SelectedRowStyle>
</asp:GridView>

                        <!--Grid view Code Ends-->
                    </div>
                
</ContentTemplate>            
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel1" Width="860px" Height="430px">
                <HeaderTemplate>
                    Training Schedule List                
                </HeaderTemplate>
                
                <ContentTemplate>
                    <div class="GridFormat3">
                        <!--Grid view Code starts-->
                        <asp:GridView ID="grList" runat="server" DataKeyNames="ScheduleID,TrainId,TrainName,SalLocId,SalLocName,
            StrDate,EndDate,Duration,NoofPerson,CoordinatorId,CoordinatorName,ResidentialId,Residential,IsActive,VenueId,Remarks"
                            AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" Width="99%"
                            OnRowCommand="grList_RowCommand">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                            </SelectedRowStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                            <Columns>
                                <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                    <ItemStyle CssClass="ItemStylecss" Width="8%" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="TrainName" HeaderText="Trainin Name">
                                    <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SalLocName" HeaderText="Trainin Location">
                                    <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="StrDate" HeaderText="Start Date" DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle CssClass="ItemStylecss"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle CssClass="ItemStylecss"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Duration" HeaderText="Duration">
                                    <ItemStyle CssClass="ItemStylecssCenter"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="NoofPerson" HeaderText="No Of Person">
                                    <ItemStyle CssClass="ItemStylecssCenter"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CoordinatorName" HeaderText="Coordinator">
                                    <ItemStyle CssClass="ItemStylecss"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Residential" HeaderText="Residential">
                                    <ItemStyle CssClass="ItemStylecss"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IsActive" HeaderText="Is Active">
                                    <ItemStyle CssClass="ItemStylecssCenter" Width="10%"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <!--Grid view Code Ends-->
                    </div>
                
</ContentTemplate>
            
</cc1:TabPanel>
        </cc1:TabContainer>
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
    </div>
    </table>
</asp:Content>
