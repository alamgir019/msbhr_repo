<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="TrainingRequisition.aspx.cs" Inherits="Training_TrainingRequisition" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JScripts/jquery-1.4.2.min.js"></script>
    <script language="javascript" type="text/javascript" src="../JScripts/jquery-ui-1.8.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js"></script>
    <script type="text/javascript">
        $(function () {
            $(".TraineeName").autocomplete({
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
            $(".ReviewBy").autocomplete({
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
            $(".RecomandedBy").autocomplete({
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
            $(".ApproveBy").autocomplete({
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
        </script>
    <div class="empTrainForm">
        <div id="formhead1">
            <div style="width: 96%; float: left;">
                Training Requsition</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%"
            Height="480px">
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Width="860px"
                Height="400px">
                <HeaderTemplate>
                    Add Requisition
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
                            <asp:HiddenField ID="hfTrainingId" runat="server" />
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" CssClass="textlevel" Text="Schedule ID:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSchedule" runat="server" CssClass="textlevelleft" Width="200px"
                                            OnSelectedIndexChanged="ddlSchedule_SelectedIndexChanged" AutoPostBack="True"
                                            ToolTip="Select Training Schedule">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkInActive" runat="server" CssClass="textlevel" Text="Make Inactive" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" CssClass="textlevel" Text="Training Name :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTrainingName" CssClass="textlevelleft" runat="server" Width="200px"
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtTrainingName"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" CssClass="textlevel" Text="Training Location/Venue :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTrainingLocation" CssClass="textlevelleft" runat="server" Width="200px"
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="textlevel">
                                        Remarks :
                                    </td>
                                    <td colspan="4">
                                        <asp:TextBox ID="txtRemark" runat="server" CssClass="textlevelleft"
                                            Width="552px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;
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
                                        <%--<asp:DropDownList ID="ddlTraineeName" CssClass="textlevelleft" runat="server" Width="200px"
                                            ToolTip="Select Trainee Name">
                                        </asp:DropDownList>--%>
                                <asp:TextBox ID="txtTraineeName" class="TraineeName textlevelleft" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" CssClass="textlevel" Text="project Name :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlProject" CssClass="textlevelleft" runat="server" Width="200px"
                                            ToolTip="Select Project Name">
                                        </asp:DropDownList>
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
                        <asp:GridView ID="grList" runat="server" DataKeyNames="EmpID,EmpName,ProjectId,ProjectName"
                            AutoGenerateColumns="False" EmptyDataText="No Record Found" 
                            Font-Size="9px" Width="99%"
                            OnRowCommand="grList_RowCommand" OnRowDeleting="grList_RowDeleting" 
                            onselectedindexchanged="grList_SelectedIndexChanged">
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
                                <asp:BoundField DataField="EmpName" HeaderText="Employee Name">
                                    <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ProjectName" HeaderText="Project Name">
                                    <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <!--Grid view Code Ends-->
                    </div>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" CssClass="textlevel" Text="Prepared By :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtReviewBy" class="ReviewBy textlevelleft" runat="server" Width="200px"></asp:TextBox>
                                <%--<asp:DropDownList ID="ddlReviewBy" runat="server" CssClass="textlevelleft" ToolTip="Select Trainee Name"
                                    Width="200px">
                                </asp:DropDownList>--%>
                            </td>
                            <td>
                                <asp:Label ID="Label8" runat="server" CssClass="textlevel" Text="Recommended By :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRecomandedBy" class="RecomandedBy textlevelleft" runat="server" Width="200px"></asp:TextBox>
                                <%--<asp:DropDownList ID="ddlRecomandedBy" runat="server" CssClass="textlevelleft" ToolTip="Select Trainee Name"
                                    Width="200px">
                                </asp:DropDownList>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" CssClass="textlevel" Text="Approved By :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtApproveBy" class="ApproveBy textlevelleft" runat="server" Width="200px"></asp:TextBox>
                                <%--<asp:DropDownList ID="ddlApproveBy" CssClass="textlevelleft" runat="server" Width="200px"
                                    ToolTip="Select Trainee Name">
                                </asp:DropDownList>--%>
                            </td>
                            <td>
                               
                            </td>
                            <td>
                               
                            </td>
                        </tr>
                        <tr>
                            <td>
                               
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSigenBy1" runat="server" CssClass="textlevelleft" ToolTip="Select Trainee Name"
                                    Visible="False" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSeenBy" runat="server" CssClass="textlevelleft" ToolTip="Select Trainee Name"
                                    Visible="False">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSigenBy2" runat="server" CssClass="textlevelleft" ToolTip="Select Trainee Name"
                                    Visible="False" Width="200px">
                                </asp:DropDownList>
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
                    Requisition List
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="GridFormat3">
                        <!--Grid view Code starts-->
                        <asp:GridView ID="grRequisition" runat="server" DataKeyNames="ReqID,ScheduleID,ScheduleName,TrainId,TrainName,TrainLocation,SignDesig1,SignDesig2,SeenBy,ReviewBy,RecommendBy,ApprovBy,IsActive"
                            AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" Width="99%"
                            OnRowCommand="grRequisition_RowCommand">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                            </SelectedRowStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                            <Columns>
                                <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="ScheduleName" HeaderText="Schedule Name">
                                    <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TrainName" HeaderText="Training Name">
                                    <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TrainLocation" HeaderText="Training Location">
                                    <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                                </asp:BoundField>                               
                                <asp:BoundField DataField="ReviewBy" HeaderText="Review By">
                                    <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="RecommendBy" HeaderText="Recommend By">
                                    <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ApprovBy" HeaderText="Approve By">
                                    <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IsActive" HeaderText="Is Active">
                                    <ItemStyle CssClass="ItemStylecssCenter" Width="30%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                    <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <!--Grid view Code Ends-->
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </div>
</asp:Content>
