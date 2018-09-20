<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="TrainingBudget.aspx.cs" Inherits="Training_TrainingBudget" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript" src="../JScripts/jquery-1.4.2.min.js"></script>
    <script language="javascript" type="text/javascript" src="../JScripts/jquery-ui-1.8.1.min.js"></script>
 <script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js"></script>
  <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js"></script>
    <script type="text/javascript">
        $(function () {
            $(".PreparedBy").autocomplete({
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
                    $('.txtDesigPrep').val(ui.item.designame);
                    $('.txtDeptPrep').val(ui.item.deptname);
                    },
                minLength: 2
            });
            $(".ReviewedBy").autocomplete({
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
                    $('.txtDesigReview').val(ui.item.designame);
                    $('.txtDeptReview').val(ui.item.deptname);
                },
                minLength: 2
            });
            $(".Recommend1").autocomplete({
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
                    $('.txtDesigRec1').val(ui.item.designame);
                    $('.txtDeptRec1').val(ui.item.deptname);
                },
                minLength: 2
            });
            $(".Recommend2").autocomplete({
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
                    $('.txtDesigRec2').val(ui.item.designame);
                    $('.txtDeptRec2').val(ui.item.deptname);
                },
                minLength: 2
            });
            $(".Recommend3").autocomplete({
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
                    $('.txtDesigRec3').val(ui.item.designame);
                    $('.txtDeptRec3').val(ui.item.deptname);
                },
                minLength: 2
            });
            $(".ApprovedBy").autocomplete({
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
                    $('.txtDesigApp').val(ui.item.designame);
                    $('.txtDeptApp').val(ui.item.deptname);
                },
                minLength: 2
            });
        });
    </script>
     <div class="officeSetup" style="width: 72%">
        <div id="formhead1">
            <div style="width: 96%; float: left;">
                Training Budget</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        
        <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
             Width="100%" Height="800px">            
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Width="90%" Height="750px">
                <HeaderTemplate>
                    Training Budget Setup
                </HeaderTemplate>
                <ContentTemplate>                
        <!--Div for group-->
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="officeSetupInner">
            <fieldset>
                <!--Div for Controls-->
               <asp:HiddenField ID="hfId" runat="server" />
               <asp:HiddenField ID="hfIsUpdate" runat="server" />
               <asp:HiddenField ID="hfDuration" runat="server" />
                <table>
                    <tr>  
                        <td><asp:Label ID="Label5" runat="server" CssClass="textlevel" Text="Date :"></asp:Label></td> 
                         <td>
                            <asp:TextBox ID="txtCreateDate" runat="server" Width="150px"></asp:TextBox> 
                             
                            <a href="javascript:NewCal('<%= txtCreateDate.ClientID %>','ddmmyyyy')">
                            <img alt="Pick a date" height="16" src="../images/cal.gif" style="border-right: 0px;
                                    border-top: 0px; border-left: 0px; border-bottom: 0px" width="16" /></a>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCreateDate"
                                CssClass="validator" ErrorMessage="Invalid" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtCreateDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td><asp:Label ID="Label19" runat="server" CssClass="textlevel" Text="Training Title:"></asp:Label></td>
                         <td> 
                            <asp:DropDownList ID="ddlTrainingName" runat="server" CssClass="textlevelleft" Width="150px" ToolTip="Select Training Name" 
                                 OnSelectedIndexChanged="ddlTrainingName_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        </td>
                        <td><asp:Label ID="Label20" runat="server" CssClass="textlevel" Text="Training Schedule:"></asp:Label></td>
                        <td>
                        <asp:DropDownList ID="ddlTrainingSchedule" runat="server" CssClass="textlevelleft" 
                                Width="150px" ToolTip="Select Training Schedule" 
                        OnSelectedIndexChanged="ddlTrainingSchedule_SelectedIndexChanged" 
                                AutoPostBack="True"></asp:DropDownList>
                        </td>
                        </tr>
                        <tr>
                     <td><asp:Label ID="Label6" runat="server" CssClass="textlevel" Text="No Of participants :"></asp:Label></td>
                     <td><asp:TextBox ID="txtNoOfPerson" ReadOnly="True" runat="server" Width="200px" ></asp:TextBox></td>                     
                     <td><asp:Label ID="Label8" runat="server" CssClass="textlevel" Text="Funded By :"></asp:Label></td>
                     <td><asp:TextBox ID="txtFundedBy" ReadOnly="True" runat="server" Width="150px"></asp:TextBox></td>                   
                     <td><asp:CheckBox ID="chkInActive" runat="server" CssClass="textlevelleft" Text="Make Inactive" /></td>
                    </tr>
                    <tr>
                    <td><asp:Label ID="Label2" runat="server" CssClass="textlevel" Text="Start Date :"></asp:Label></td> 
                     <td>
                            <asp:TextBox ID="txtStrDate" runat="server" Width="150px" ReadOnly="True"></asp:TextBox>  
                            <a href="javascript:NewCal('<%= txtEndDate.ClientID %>','ddmmyyyy')">
                            <img alt="Pick a date" height="16" src="../images/cal.gif" style="border-right: 0px;
                                border-top: 0px; border-left: 0px; border-bottom: 0px" width="16" /></a> 
                    </td>
                     <td><asp:Label ID="Label3" runat="server" CssClass="textlevel" Text="End Date :"></asp:Label></td> 
                     <td>
                         <asp:TextBox ID="txtEndDate" runat="server" Width="150px" ReadOnly="True"></asp:TextBox>                           
                        <a href="javascript:NewCal('<%= txtEndDate.ClientID %>','ddmmyyyy')">
                            <img alt="Pick a date" height="16" src="../images/cal.gif" style="border-right: 0px;
                                border-top: 0px; border-left: 0px; border-bottom: 0px" width="16" /></a>                        
                    </td>                   
                    </tr>
                    <tr>
                     <td><asp:Label ID="Label4" runat="server" CssClass="textlevel" Text="Participent level :"></asp:Label></td>
                     <td><asp:TextBox ID="txtParticipentLevel" runat="server" Width="200px" ></asp:TextBox></td>
                            
                
                      <td><asp:Label ID="Label1" runat="server" CssClass="textlevel" Text="Venue :"></asp:Label></td> 
                        <td> 
                            <asp:DropDownList ID="ddlVenue" runat="server" CssClass="textlevelleft" Width="150px"
                                ToolTip="Select Venue" AutoPostBack="True">
                        </asp:DropDownList></td>
                    </tr>
                   

                </table>
            </fieldset>
            <br />
            
            <fieldset>
                <table>
                 <tr>
                  
                     
                     <td><asp:Label ID="Label7" runat="server" CssClass="textlevel" Text="Budget Type :"></asp:Label></td>
                     <td><asp:DropDownList ID="ddlBTitleType" runat="server" CssClass="textlevelleft" Width="110px"
                                ToolTip="Select Budget Type" 
                             OnSelectedIndexChanged="ddlBTitleType_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList></td>
                        <td><asp:Label ID="Label14" runat="server" CssClass="textlevel" Text="Budget Title :"></asp:Label></td>
                     <td><asp:DropDownList ID="ddlBTitle" runat="server" CssClass="textlevelleft" Width="110px"
                                ToolTip="Select Budget Type" AutoPostBack="True">
                        </asp:DropDownList></td>
                         <td><asp:Label ID="Label9" runat="server" CssClass="textlevel" Text="Unit Cost :"></asp:Label></td>
                     <td><asp:TextBox ID="txtUnitCost" runat="server" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" MaxLength="9" Width="60px"></asp:TextBox></td>
                     <td><asp:Label ID="Label15" runat="server" CssClass="textlevel" Text="Participant Number :"></asp:Label></td>
                        <td><asp:TextBox ID="txtParticipantNo" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" MaxLength="9" runat="server" Width="60px" ></asp:TextBox></td>

                        <td><asp:Label ID="Label10" runat="server" CssClass="textlevel" Text="Number of Days :"></asp:Label></td>
                     <td><asp:TextBox ID="txtDaysNo" runat="server" Width="60px" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" MaxLength="9"></asp:TextBox></td>
                     
                       <td>
                         <asp:Button ID="btnAdd" runat="server" Text="Add" Width="40px" 
                                CausesValidation="False" onclick="btnAdd_Click"/></td>
                    </tr>
                    
                </table>
            </fieldset>
        </div>
        <div class="GridFormat1">
            <!--Grid view Code starts-->
            <asp:GridView ID="grList" runat="server" DataKeyNames="BudgetId,BudgetType,BTitleId,BTitleType,BTitleName,UnitCost,ParticipantNo,DaysNo,TotalTaka" AutoGenerateColumns="False"
                EmptyDataText="No Record Found" Font-Size="9px" Width="99%" OnRowCommand="grList_RowCommand" OnRowDeleting="grList_RowDelete">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                </SelectedRowStyle>
                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                <Columns>
                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                        <ItemStyle CssClass="ItemStylecss" Width="8%" />
                    </asp:ButtonField>                    
                    <asp:ButtonField CommandName="Delete" HeaderText="Delete" Text="Delete">
                        <ItemStyle CssClass="ItemStylecss" Width="5%" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="BudgetType" HeaderText="Budget Type">
                        <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="BTitleName" HeaderText="Description">
                        <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                    </asp:BoundField>

                     <asp:BoundField DataField="unitCost" HeaderText="Unit Cost" >
                        <ItemStyle CssClass="ItemStylecss" ></ItemStyle>
                      </asp:BoundField>
                      
                     <asp:BoundField DataField="ParticipantNo" HeaderText="Participants Number">
                        <ItemStyle CssClass="ItemStylecssCenter"></ItemStyle>
                    </asp:BoundField>

                     <asp:BoundField DataField="DaysNo" HeaderText="Number of Days">
                        <ItemStyle CssClass="ItemStylecssCenter"></ItemStyle>
                    </asp:BoundField>

                     <asp:BoundField DataField="TotalTaka" HeaderText="Total Taka">
                        <ItemStyle CssClass="ItemStylecss"></ItemStyle>
                    </asp:BoundField>

                </Columns>
            </asp:GridView>
            <!--Grid view Code Ends-->
        </div>
        <div class="officeSetupInner">        
        <fieldset>
        <br />
        <br />
        <br />
            <table width="99%">            
                <tr>
                    <td>
                        <asp:Label ID="Label11" runat="server" CssClass="textlevel" Text="Prepared by :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label12" runat="server" CssClass="textlevel" Text="Reviewed by :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label13" runat="server" CssClass="textlevel" Text="Recommended by :"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtPreparedBy" class="PreparedBy textlevelleft" runat="server" Width="200px"></asp:TextBox>
                        <%--<asp:DropDownList ID="ddlPreparedBy" runat="server" CssClass="textlevelleft" Width="200px"
                            ToolTip="Select Prepared By" OnSelectedIndexChanged="ddlPreparedBy_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>--%>
                    </td>
                    <td>
                        <asp:TextBox ID="txtReviewedBy" class="ReviewedBy textlevelleft" runat="server" Width="200px"></asp:TextBox>
                        <%--<asp:DropDownList ID="ddlReviewedBy" runat="server" CssClass="textlevelleft" Width="200px"
                            ToolTip="Select Reviewed By" OnSelectedIndexChanged="ddlReviewedBy_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>--%>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRecommend1" class="Recommend1 textlevelleft" runat="server" Width="200px"></asp:TextBox>
                        <%--<asp:DropDownList ID="ddlRecommend1" runat="server" CssClass="textlevelleft" Width="200px"
                            ToolTip="Select Recommend By" OnSelectedIndexChanged="ddlRecommend1_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtDesigPrep" class="txtDesigPrep" runat="server" Width="200px" ReadOnly="True"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                            ControlToValidate="txtDesigPrep"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDesigReview" class="txtDesigReview" runat="server" Width="200px" ReadOnly="True"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                            ControlToValidate="txtDesigReview"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDesigRec1" class="txtDesigRec1" runat="server" Width="200px" ReadOnly="True"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                            ControlToValidate="txtDesigRec1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtDeptPrep" class="txtDeptPrep" runat="server" Width="200px" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDeptReview" class="txtDeptReview" runat="server" Width="200px" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDeptRec1" class="txtDeptRec1" runat="server" Width="200px" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
            <td>
                <br /></td>
            <td>
                <br /></td>
            <td>
                <br /></td>
            </tr>
            <tr>
            <td>
                <br /></td>
            <td>
                <br /></td>
            <td>
                <br /></td>
            </tr>
            <tr>
            <td>
                <br /></td>
            <td>
                <br /></td>
            <td>
                <br /></td>
            </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label16" runat="server" CssClass="textlevel" Text="Recommended by :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label17" runat="server" CssClass="textlevel" Text="Recommended by :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label18" runat="server" CssClass="textlevel" Text="Apporved by :"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtRecommend2" class="Recommend2 textlevelleft" runat="server" Width="200px"></asp:TextBox>
                        <%--<asp:DropDownList ID="ddlRecommend2" runat="server" CssClass="textlevelleft" Width="200px"
                            ToolTip="Select Recommend By" OnSelectedIndexChanged="ddlRecommend2_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>--%>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRecommend3" class="Recommend3 textlevelleft" runat="server" Width="200px"></asp:TextBox>
                        <%--<asp:DropDownList ID="ddlRecommend3" runat="server" CssClass="textlevelleft" Width="200px"
                            ToolTip="Select Recommend By" OnSelectedIndexChanged="ddlRecommend3_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>--%>
                    </td>
                    <td>
                        <asp:TextBox ID="txtApprovedBy" class="ApprovedBy textlevelleft" runat="server" Width="200px"></asp:TextBox>
                        <%--<asp:DropDownList ID="ddlApprovedBy" runat="server" CssClass="textlevelleft" Width="200px"
                            ToolTip="Select Approved By" OnSelectedIndexChanged="ddlApprovedBy_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtDesigRec2" class="txtDesigRec2" runat="server" Width="200px" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDesigRec3" class="txtDesigRec3" runat="server" Width="200px" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDesigApp" class="txtDesigApp" runat="server" Width="200px" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtDeptRec2" class="txtDeptRec2" runat="server" Width="200px" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDeptRec3" class="txtDeptRec3" runat="server" Width="200px" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDeptApp" class="txtDeptApp" runat="server" Width="200px" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </fieldset>
        </div>
        <div class="DivCommand1">
            <div class="DivCommandL">
                <asp:Button ID="btnClear" runat="server" Text="Refresh" Width="70px" OnClick="btnClear_Click"
                    CausesValidation="False" />
            </div>
            <div class="DivCommandR">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click"/>
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                    OnClick="btnDelete_Click" />
            </div>
        </div>
                </ContentTemplate>
                </cc1:TabPanel>

                
            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel1" Width="90%" Height="450px">
                <HeaderTemplate>
                    Training Budget List
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="GridFormat3">
                        <asp:GridView ID="grTrainingBudget" runat="server" 
                        DataKeyNames="BudgetId,TrainId,TrainName,ScheduleID,StrDate,EndDate,Duration,NoofPerson,PreparedByName,PreparedBy,ReviewedBy,RecomBy1,RecomBy2,
                        RecomBy3,ApprovedBy,FundedBy,CreateDate,ParticipantLevel,VenueId,IsActive"
                            AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" Width="99%" OnRowCommand="grTrainingBudget_RowCommand">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                            </SelectedRowStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                            <Columns>
                                <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="TrainName" HeaderText="Training Name">
                                    <ItemStyle CssClass="ItemStylecss" Width="25%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="StrDate" HeaderText="Start Date">
                                    <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EndDate" HeaderText="End Date">
                                    <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="NoofPerson" HeaderText="Number Of Person">
                                    <ItemStyle CssClass="ItemStylecssCenter" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PreparedByName" HeaderText="Prepared By">
                                    <ItemStyle CssClass="ItemStylecssCenter" Width="15%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IsActive" HeaderText="Is Active">
                                    <ItemStyle CssClass="ItemStylecssCenter" Width="10%"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <!--Grid view Code starts-->
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>

        <br />
        <br />
    </div>
</asp:Content>

