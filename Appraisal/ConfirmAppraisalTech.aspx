<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="ConfirmAppraisalTech.aspx.cs" Inherits="Appraisal_ConfirmAppraisalTech"
    Title="Confirmation Appraisal Technical" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
    <script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js">
        //Date Time Picker script
    </script>
    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>
    <script language="javascript" type="text/javascript">
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }
    </script>
    <div class="empTrainForm">
        <div id="formhead2">
            <div style="width: 97%; float: left;">
                Confirmation Appraisal (For Technical)</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="PayrollConfigInner" style="height: 940px">
            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%" Height="930px">
                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Width="100%" Height="750px">
                    <HeaderTemplate>
                        Confirmation Appraisal Entry
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div style="background-color: #EFF3FB; margin-bottom: 10px">
                            <fieldset>
                                <table>
                                    <tr>
                                        <td class="textlevel">
                                            Emp Id :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEmpID" runat="server" MaxLength="20" onkeyup="ToUpper(this)"
                                                Width="80px" ToolTip="Enter the Emp. Code and click on Find Button. Details of the find employee will be shown in employee general information screen if information exists. In order to enter employee."></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgBtnSearch" runat="server" Height="19px" ImageUrl="~/Images/Search_Icon.jpg"
                                                OnClick="imgBtnSearch_Click" ToolTip="Enter the Emp. Code and click on Find Button. Details of the find employee will be shown in employee general information screen if information exists. In order to enter employee." />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ReqFieldVal" runat="server" ErrorMessage="*" ControlToValidate="txtEmpID"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkOpenPDF" runat="server"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                                <table>
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
                                            Designation :
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            Location :</td>
                                        <td>
                                            <asp:Label ID="lblDesignation0" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            <span>Department /Project/Clinic :</span></td>
                                        <td>
                                            <asp:Label ID="lblDesignation1" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            Date of Joining :
                                        </td>
                                        <td>
                                            <asp:Label ID="lblJoiningDate" runat="server"></asp:Label>
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td class="textlevel">
                                            Appraised by [Name, Designation & Location] :
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSuperId" runat="server"></asp:Label>
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td class="textlevel">
                                            Date of Confirmation :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtConfirmDate" runat="server" ToolTip="Input the confirmation date."
                                                Width="80px"></asp:TextBox>
                                            <a href="javascript:NewCal('<%= txtConfirmDate.ClientID %>','ddmmyyyy')">
                                                <img alt="Pick a date" height="16" src="../images/cal.gif" style="border-right: 0px;
                                                    border-top: 0px; border-left: 0px; border-bottom: 0px" width="16" /></a>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtConfirmDate"
                                                CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            Date of Appraisal :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAppraisalDate" runat="server" ToolTip="Input the confirmation date."
                                                Width="80px"></asp:TextBox>
                                            <a href="javascript:NewCal('<%= txtAppraisalDate.ClientID %>','ddmmyyyy')">
                                                <img alt="Pick a date" height="16" src="../images/cal.gif" style="border-right: 0px;
                                                    border-top: 0px; border-left: 0px; border-bottom: 0px" width="16" /></a>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtAppraisalDate"
                                                CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            Fiscal Year :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlFiscalYr" runat="server" CssClass="textlevelleft">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                        <fieldset>
                            <legend>Evaluation of Behavioural skills</legend>
                            <table>
                                <tr>
                                    <td class="textlevelleft">
                                        Behavior
                                    </td>
                                    <td class="textlevelleft">
                                        Definition
                                    </td>
                                    <td class="textlevelleft">
                                        Details
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtActivityName" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDesc" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td><asp:TextBox ID="TextBox1" runat="server" Width="200px"></asp:TextBox></td>
                                    <td>
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" Width="70px" OnClick="btnAdd_Click" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <div style="overflow: scroll; width: 100%; height: 100px">
                                <asp:GridView ID="grPerformance" runat="server" Width="99%" Font-Size="9px" EmptyDataText="No Record Found"
                                    AutoGenerateColumns="False" OnRowCommand="grActivityList_RowCommand" DataKeyNames="ActivityName"
                                    OnSelectedIndexChanged="grActivityList_SelectedIndexChanged">
                                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                    </SelectedRowStyle>
                                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:ButtonField CommandName="DoubleClick" HeaderText="Delete" Text="Delete">
                                            <ItemStyle Width="5%" CssClass="ItemStylecss" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="ActivityName" HeaderText="Job Objective">
                                            <ItemStyle CssClass="ItemStylecss" Width="29%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ActivityDesc" HeaderText="Result/Unaccomplishment">
                                            <ItemStyle CssClass="ItemStylecss" Width="39%"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Evaluation of Technical Skills</legend>
                            <table>
                                <tr>
                                    <td class="textlevelleft">
                                        Indicators
                                    </td>
                                    <td class="textlevelleft">
                                        Grades
                                    </td>
                                    <td class="textlevelleft">
                                        Support required
                                    </td>
                                    <td class="textlevelleft">
                                        Own actions
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDiff" runat="server" Width="180px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox2" runat="server" Width="180px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox3" runat="server" Width="180px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox4" runat="server" Width="180px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="Button1" runat="server" Text="Add" Width="70px" OnClick="btnAdd_Click" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <div style="overflow: scroll; width: 100%; height: 100px">
                                <asp:GridView ID="GridView1" runat="server" Width="99%" Font-Size="9px" EmptyDataText="No Record Found"
                                    AutoGenerateColumns="False" OnRowCommand="grActivityList_RowCommand" DataKeyNames="ActivityName"
                                    OnSelectedIndexChanged="grActivityList_SelectedIndexChanged">
                                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                    </SelectedRowStyle>
                                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:ButtonField CommandName="DoubleClick" HeaderText="Delete" Text="Delete">
                                            <ItemStyle Width="5%" CssClass="ItemStylecss" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="ActivityName" HeaderText="Difficulties">
                                            <ItemStyle CssClass="ItemStylecss" Width="29%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ActivityDesc" HeaderText="Ways to Overcome">
                                            <ItemStyle CssClass="ItemStylecss" Width="39%"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Personal Development Plan</legend>
                            <table>
                                <tr>
                                    <td class="textlevelleft">
                                        Development Goal
                                    </td>
                                    <td class="textlevelleft">
                                        Timescale
                                    </td>
                                    <td class="textlevelleft">
                                        Support required
                                    </td>
                                    <td class="textlevelleft">
                                        Own actions
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TextBox8" runat="server" Width="180px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox9" runat="server" Width="180px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox10" runat="server" Width="180px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox11" runat="server" Width="180px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="Button2" runat="server" Text="Add" Width="70px" OnClick="btnAdd_Click" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <div style="overflow: scroll; width: 100%; height: 100px">
                                <asp:GridView ID="GridView2" runat="server" Width="99%" Font-Size="9px" EmptyDataText="No Record Found"
                                    AutoGenerateColumns="False" OnRowCommand="grActivityList_RowCommand" DataKeyNames="ActivityName"
                                    OnSelectedIndexChanged="grActivityList_SelectedIndexChanged">
                                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                    </SelectedRowStyle>
                                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:ButtonField CommandName="DoubleClick" HeaderText="Delete" Text="Delete">
                                            <ItemStyle Width="5%" CssClass="ItemStylecss" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="ActivityName" HeaderText="Difficulties">
                                            <ItemStyle CssClass="ItemStylecss" Width="29%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ActivityDesc" HeaderText="Ways to Overcome">
                                            <ItemStyle CssClass="ItemStylecss" Width="39%"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </fieldset>
                        <table>
                            <tr>
                                <td class="textlevelleft">
                                    Is Induction Training Receive 
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </td>                           
                                <td class="textlevelleft">
                                    Overall Rating:
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox5" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="textlevelleft">
                                    Team Member Comments:
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox6" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                                </td>                           
                                <td class="textlevelleft">
                                    Manager/ Supervisor Comments:
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox7" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                                </td>
                            </tr>                           
                        </table>
                        <asp:HiddenField ID="hfIsUpdate" runat="server" />
                        <asp:HiddenField ID="hfId" runat="server" />                        
                        <div class="DivCommand1" style="width: 98%;">
                            <div class="DivCommandL">
                                <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" Text="Refresh"
                                    Width="70px" OnClick="btnRefresh_Click" />
                            </div>
                            <div class="DivCommandR">
                                <asp:Button ID="btnSave" runat="server" CausesValidation="False" Text="Save" Width="125px"
                                    OnClick="btnSave_Click" />
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel1" Width="100%">
                    <HeaderTemplate>
                        Confirmation Appraisal List
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div style="overflow: scroll; width: 100%; height: 400px">
                            <asp:GridView ID="grAppraisalMstList" runat="server" Width="99%" Font-Size="9px"
                                EmptyDataText="No Record Found" AutoGenerateColumns="False" DataKeyNames="AppId,FiscalYrId,IsMidTerm,EntryDate"
                                OnRowCommand="grAppraisalMstList_RowCommand">
                                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                </SelectedRowStyle>
                                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                <Columns>
                                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                        <ItemStyle Width="5%" CssClass="ItemStylecss" />
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="EmpId" HeaderText="Emp ID">
                                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" DataFormatString="{0:dd/MM/yyyy}">
                                        <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FiscalYrTitle" HeaderText="Fiscal Year">
                                        <ItemStyle CssClass="ItemStylecssRight" Width="10%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="IsMidTerm" HeaderText="Mid Term">
                                        <ItemStyle CssClass="ItemStylecssRight" Width="10%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalRating" HeaderText="Total Rating">
                                        <ItemStyle CssClass="ItemStylecssRight" Width="10%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OverallRating" HeaderText="Overall Rating">
                                        <ItemStyle CssClass="ItemStylecssRight" Width="10%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                        <ItemStyle CssClass="ItemStylecssRight" Width="30%"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
        </div>
    </div>
</asp:Content>
