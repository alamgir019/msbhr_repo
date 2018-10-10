<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="TrainingResult.aspx.cs" Inherits="Training_TrainingResult" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JScripts/jquery-1.4.2.min.js"></script>
    <script language="javascript" type="text/javascript" src="../JScripts/jquery-ui-1.8.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js"></script>
    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js"></script>
    <script type="text/javascript">
        $(function () {
            $(".EvaluationBy").autocomplete({
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

            $(".Signatory1").autocomplete({
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
                                    value: item.FullName,
                                    empid: item.EmpId,
                                    designame: item.Title,
                                    deptname: item.DeptName
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                select: function (event, ui) {
                    $('.txtDesignation1').val(ui.item.designame);
                    $('.txtDept1').val(ui.item.deptname);
                },
                minLength: 2
            });

            $(".Signatory2").autocomplete({
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
                                    value: item.FullName,
                                    empid: item.EmpId,
                                    designame: item.Title,
                                    deptname: item.DeptName
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                select: function (event, ui) {
                    $('.txtDesignation2').val(ui.item.designame);
                    $('.txtDept2').val(ui.item.deptname);
                },
                minLength: 2
            });

            $(".Signatory3").autocomplete({
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
                                    value: item.FullName,
                                    empid: item.EmpId,
                                    designame: item.Title,
                                    deptname: item.DeptName
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                select: function (event, ui) {
                    $('.txtDesignation3').val(ui.item.designame);
                    $('.txtDept3').val(ui.item.deptname);
                },
                minLength: 2
            });

            $(".Signatory4").autocomplete({
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
                                    value: item.FullName,
                                    empid: item.EmpId,
                                    designame: item.Title,
                                    deptname: item.DeptName
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                select: function (event, ui) {
                    $('.txtDesignation4').val(ui.item.designame);
                    $('.txtDept4').val(ui.item.deptname);
                },
                minLength: 2
            });

            function txtOverall_onclick() {
                var postTest = 0, PracticalTest = 0, Viva = 0;

                if (document.getElementById('<%=txtPostTest.ClientID%>').value != '') {
                    postTest = document.getElementById('<%=txtPostTest.ClientID%>').value;
                }
                if (document.getElementById('<%=txtPracticalTest.ClientID%>').value != '') {
                    PracticalTest = document.getElementById('<%=txtPracticalTest.ClientID%>').value;
                }
                if (document.getElementById('<%=txtViva.ClientID%>').value != '') {
                    Viva = document.getElementById('<%=txtViva.ClientID%>').value;
                }
                document.getElementById('<%=txtOverall.ClientID%>').value = Math.round(postTest) + Math.round(PracticalTest) + Math.round(Viva);
            }   
        });
    </script>
    <div class="empTrainForm">
        <div id="formhead1">
            <div style="width: 96%; float: left;">
                Training Result</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%"
            Height="760px">
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Width="90%" Height="600px">
                <HeaderTemplate>
                    Training Result Setup
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="MsgBox">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                    <div class="officeSetupInner">
                        <fieldset>
                            <asp:HiddenField ID="hfId" runat="server" />
                            <asp:HiddenField ID="hfIsUpdate" runat="server" />
                            <asp:HiddenField ID="hfTrainingId" runat="server" />
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" CssClass="textlevel" Text="Schedule Name :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSchedule" runat="server" CssClass="textlevelleft" Width="200px"
                                            OnSelectedIndexChanged="ddlSchedule_SelectedIndexChanged" AutoPostBack="True"
                                            ToolTip="Select Training Schedule">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label20" runat="server" Width="89px" CssClass="textlevel" Text="Evaluation Date :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEvaluationDate" runat="server" Width="89px"></asp:TextBox>
                                        <a href="javascript:NewCal('<%= txtEvaluationDate.ClientID %>','ddmmyyyy')">
                                            <img alt="Pick a date" height="16" src="../images/cal.gif" style="border-right: 0px;
                                                border-top: 0px; border-left: 0px; border-bottom: 0px" width="16" /></a>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtEvaluationDate"
                                            CssClass="validator" ErrorMessage="Invalid" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                            ControlToValidate="txtEvaluationDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkInActive" runat="server" CssClass="textlevelleft" Text="Make Inactive" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label14" runat="server" CssClass="textlevel" Text="Training Name :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTrainingName" CssClass="textlevelleft" runat="server" Width="200px"
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label15" runat="server" CssClass="textlevel" Text="Training Location :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTrainingLocation" CssClass="textlevelleft" runat="server" Width="200px"
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" CssClass="textlevel" Text="Evaluation Method :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEvaluationMethod" runat="server" Width="200px" MaxLength="40"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtEvaluationMethod"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" CssClass="textlevel" Text="Evaluation By :"></asp:Label>
                                    </td>
                                    <td>
                        <asp:TextBox ID="txtEvaluationBy" class="EvaluationBy textlevelleft" runat="server" Width="200px"></asp:TextBox>
                                        <%--<asp:DropDownList ID="ddlEvaluationBy" runat="server" CssClass="textlevelleft" Width="200px"
                                            ToolTip="Select">
                                        </asp:DropDownList>--%>
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
                                            ToolTip="Select" AutoPostBack="True" OnSelectedIndexChanged="ddlTraineeName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label23" runat="server" CssClass="textlevel" Text="Designation :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDesignation" runat="server" Width="150px" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label24" runat="server" CssClass="textlevel" Text="Dept/Clinic :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDept" runat="server" Width="150px" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td class="textlevel">
                                        Funded By :</td>
                                    <td>
                                        <asp:DropDownList ID="ddlFundedby" runat="server" CssClass="textlevelleft" 
                                            ToolTip="Select Training Name" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" CssClass="textlevel" Text="Pre-Test :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPreTest" runat="server" MaxLength="3" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" CssClass="textlevel" Text="Post-Test :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPostTest" runat="server" MaxLength="3" Width="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" CssClass="textlevel" Text="Practical test :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPracticalTest" MaxLength="3" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" CssClass="textlevel" Text="Viva :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtViva" runat="server" MaxLength="3" Width="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" CssClass="textlevel" Text="Overall :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOverall" MaxLength="3" runat="server" Width="100px"
                                            onclick="txtOverall_onclick()"></asp:TextBox>
                                    </td>
                                    <td class="textlevel">
                                        Competency Assess Level :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlComAssLevel" runat="server" CssClass="textlevelleft" 
                                            ToolTip="Select Residential By" Width="110px">
                                            <asp:ListItem Text="Level 1" Value="1" />
                                            <asp:ListItem Text="Level 2" Value="2" />
                                            <asp:ListItem Text="Level 3" Value="3" />
                                            <asp:ListItem Text="Absent" Value="4" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" CssClass="textlevel" Text="Remarks :"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtRemarks" runat="server" Width="200px" MaxLength="1000"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" Width="60px" CausesValidation="False"
                                            OnClick="btnAdd_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                         TargetControlID="txtPreTest" Enabled="True">
                     </cc1:FilteredTextBoxExtender>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                         TargetControlID="txtPostTest" Enabled="True">
                     </cc1:FilteredTextBoxExtender>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers"
                         TargetControlID="txtPracticalTest" Enabled="True">
                     </cc1:FilteredTextBoxExtender>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers"
                         TargetControlID="txtViva" Enabled="True">
                     </cc1:FilteredTextBoxExtender>
                    </div>
                    <div class="GridFormat1">
                        <asp:GridView ID="grList" runat="server" DataKeyNames="TraineeId,TraineeName,PreTest,PostTest,PracticalTest,Viva,Overall,Remark,FundedBy"
                            AutoGenerateColumns="False" EmptyDataText="No Record Found" 
                            Font-Size="9px" Width="99%"
                            OnRowCommand="grList_RowCommand" OnRowDeleting="grList_RowDelete" 
                            onselectedindexchanged="grList_SelectedIndexChanged">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                            </SelectedRowStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                            <Columns>
                                <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                    <ItemStyle CssClass="ItemStylecss" Width="5%" />
                                </asp:ButtonField>
                                <asp:ButtonField CommandName="Delete" HeaderText="Delete" Text="Delete">
                                    <ItemStyle CssClass="ItemStylecss" Width="5%" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="TraineeName" HeaderText="Employee Name">
                                    <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PreTest" HeaderText="Pre Test">
                                    <ItemStyle CssClass="ItemStylecssCenter" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PostTest" HeaderText="Post Test">
                                    <ItemStyle CssClass="ItemStylecssCenter" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PracticalTest" HeaderText="Practical Test">
                                    <ItemStyle CssClass="ItemStylecssCenter" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Viva" HeaderText="Viva">
                                    <ItemStyle CssClass="ItemStylecssCenter" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Overall" HeaderText="Overall">
                                    <ItemStyle CssClass="ItemStylecssCenter" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CompetencyLevel" HeaderText="Competency Level">
                                    <ItemStyle CssClass="ItemStylecssCenter" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Remark" HeaderText="Remark">
                                    <ItemStyle CssClass="ItemStylecssCenter" Width="10%"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <!--Grid view Code starts-->
                    </div>
                    <fieldset style="width:98%">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label11" runat="server" CssClass="textlevel" Text="Signatory 1 :"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label12" runat="server" CssClass="textlevel" Text="Signatory 2 :"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label13" runat="server" CssClass="textlevel" Text="Signatory 3 :"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label16" runat="server" CssClass="textlevel" Text="Signatory 4 :"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                        <asp:TextBox ID="txtSignatory1" class="Signatory1 textlevelleft" runat="server" Width="200px"></asp:TextBox>
                                   <%-- <asp:DropDownList ID="ddlSignatory1" runat="server" CssClass="textlevelleft" Width="90%"
                                        ToolTip="Select Signatory" OnSelectedIndexChanged="ddlSignatory1_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>--%>
                                </td>
                                <td>
                        <asp:TextBox ID="txtSignatory2" class="Signatory2 textlevelleft" runat="server" Width="200px"></asp:TextBox>
                                   <%-- <asp:DropDownList ID="ddlSignatory2" runat="server" CssClass="textlevelleft" Width="90%"
                                        ToolTip="Select Signatory" OnSelectedIndexChanged="ddlSignatory2_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>--%>
                                </td>
                                <td>
                        <asp:TextBox ID="txtSignatory3" class="Signatory3 textlevelleft" runat="server" Width="200px"></asp:TextBox>
                                    <%--<asp:DropDownList ID="ddlSignatory3" runat="server" CssClass="textlevelleft" Width="90%"
                                        ToolTip="Select Signatory" OnSelectedIndexChanged="ddlSignatory3_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>--%>
                                </td>
                                <td>
                        <asp:TextBox ID="txtSignatory4" class="Signatory4 textlevelleft" runat="server" Width="200px"></asp:TextBox>
                                   <%-- <asp:DropDownList ID="ddlSignatory4" runat="server" CssClass="textlevelleft" Width="90%"
                                        ToolTip="Select Signatory" OnSelectedIndexChanged="ddlSignatory4_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtDesignation1" class="txtDesignation1" runat="server" Width="90%" ReadOnly="True"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                                        ControlToValidate="txtDesignation1"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDesignation2" class="txtDesignation2" runat="server" Width="90%" ReadOnly="True"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                        ControlToValidate="txtDesignation2"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDesignation3" class="txtDesignation3" runat="server" Width="90%" ReadOnly="True"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                        ControlToValidate="txtDesignation3"></asp:RequiredFieldValidator>
                                </td>                                
                                <td>
                                    <asp:TextBox ID="txtDesignation4" class="txtDesignation4" runat="server" Width="90%" ReadOnly="True"></asp:TextBox>
                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                        ControlToValidate="txtDesignation4"></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtDept1" class="txtDept1" runat="server" Width="90%" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDept2" class="txtDept2" runat="server" Width="90%" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDept3" class="txtDept3" runat="server" Width="90%" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDept4" class="txtDept4" runat="server" Width="90%" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
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
            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel1" Width="90%" Height="450px">
                <HeaderTemplate>
                    Training Result List
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="GridFormat3">
                        <asp:GridView ID="grTrainingResult" runat="server" DataKeyNames="ResultId,TrainId,TrainName,EvalDate,EvalMethod,EvalBy,SignID1,SignIDName1,SignID2,SignIDName2,SignID3,SignIDName3,SignID4,SignIDName4,IsActive,ScheduleID,TrainLocation,VenueName"
                            AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" Width="99%"
                            OnRowCommand="grTrainingResult_RowCommand">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                            </SelectedRowStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                            <Columns>
                                <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="TrainName" HeaderText="Training Name">
                                    <ItemStyle CssClass="ItemStylecss" Width="50%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EvalDate" HeaderText="Evaluation Date">
                                    <ItemStyle CssClass="ItemStylecss" Width="50%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EvalMethod" HeaderText="Evaluation Method">
                                    <ItemStyle CssClass="ItemStylecss" Width="80%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EvalBy" HeaderText="Evaluation by">
                                    <ItemStyle CssClass="ItemStylecssCenter" Width="20%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SignIDName1" HeaderText="Signatry1">
                                    <ItemStyle CssClass="ItemStylecssCenter" Width="20%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SignIDName2" HeaderText="Signatry2">
                                    <ItemStyle CssClass="ItemStylecssCenter" Width="20%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SignIDName3" HeaderText="Signatry3">
                                    <ItemStyle CssClass="ItemStylecssCenter" Width="20%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SignIDName4" HeaderText="Signatry4">
                                    <ItemStyle CssClass="ItemStylecssCenter" Width="20%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IsActive" HeaderText="Is Active">
                                    <ItemStyle CssClass="ItemStylecssCenter" Width="20%"></ItemStyle>
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
