<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="PerformanceAppraisal.aspx.cs" Inherits="Appraisal_PerformanceAppraisal"
    Title="Performance Appraisal" %>

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
                Performance Appraisal</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="PayrollConfigInner" style="height: 1130px">
            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
                Width="100%" Height="1120">
                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Width="100%" Height="1110">
                    <HeaderTemplate>
                        Performance Appraisal Entry
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
                                            Appraisee Name :
                                        </td>
                                        <td colspan=3>
                                            <asp:Label ID="lblName" runat="server"></asp:Label>
                                        </td >
                                       
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            Designation :
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                                        </td>
                                        <td class="textlevel">
                                             Date of Joining :
                                        </td>                                        
                                        <td><asp:Label ID="lblJoiningDate" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            Loation :
                                        </td>
                                        <td>
                                            <asp:Label ID="lblLocation" runat="server"></asp:Label>
                                        </td>
                                        <td class="textlevel">
                                            Project : </td>
                                            <td>
                                                <asp:Label ID="lblProject" runat="server"></asp:Label>
                                        </td>
                                    </tr>  
                                      <tr>
                                        <td class="textlevel">
                                            Appraiser Name :
                                        </td>
                                        <td colspan="3">
                                            <asp:Label ID="Label1" runat="server"></asp:Label>
                                        </td >                                      
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            Designation :</td>
                                        <td>
                                            &nbsp;</td>
                                        <td class="textlevel">
                                             Loation : </td>
                                        <td class="textlevel">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                           Project :
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                    </tr>                                    
                                     <tr>
                                        <td class="textlevel">
                                            Third Person Name :
                                        </td>
                                        <td colspan="3">
                                            </td >                                        
                                    </tr>
                                    <tr>
                                        <td class="textlevel">
                                            Designation :</td>
                                        <td>
                                            &nbsp;</td>
                                        <td class="textlevel">
                                             Loation : </td>
                                        <td class="textlevel">
                                            &nbsp;</td>
                                    </tr>                                  
                                    <tr>
                                        <td class="textlevel">
                                            Appraisal Date:
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
                                        <td class="textlevel">
                                            Appraisal Time :</td>
                                        <td>
                                            <asp:TextBox ID="txtAppraisalDate0" runat="server" 
                                                ToolTip="Input the confirmation date." Width="80px"></asp:TextBox>
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
                                        <td>
                                            APA Year:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlFiscalYr0" runat="server" CssClass="textlevelleft">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                        <fieldset>
                            <legend>Job Responsibility</legend>
                            <table>
                                <tr>
                                    <td class="textlevelleft">
                                        Job
                                    </td>
                                    <td class="textlevelleft">
                                        Grading</td>
                                        <td class="textlevelleft">
                                        Comment</td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtActivityName" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList4" runat="server" Width="100px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox1" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" Width="70px" OnClick="btnAdd_Click" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <div style="overflow: scroll; width: 100%;">
                                <asp:GridView ID="grPerformance" runat="server" Width="99%" Font-Size="9px" EmptyDataText="No Record Found"
                                    AutoGenerateColumns="False" OnRowCommand="grActivityList_RowCommand" DataKeyNames="ActivityName">
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
                            <legend>Job Performance</legend>
                            <table>
                                <tr>
                                    <td class="textlevelleft">
                                        Job
                                    </td>
                                    <td class="textlevelleft">
                                        Comments</td>
                                    <td class="textlevelleft">
                                        Is Good
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDiff" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox2" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Width="100px" />
                                    </td>
                                    <td>
                                        <asp:Button ID="Button1" runat="server" Text="Add" Width="70px" OnClick="btnAdd_Click" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <div style="overflow: scroll; width: 100%;">
                                <asp:GridView ID="GridView1" runat="server" Width="99%" Font-Size="9px" EmptyDataText="No Record Found"
                                    AutoGenerateColumns="False" OnRowCommand="grActivityList_RowCommand" DataKeyNames="ActivityName">
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
                            <legend>Appraisee's Skill</legend>
                            <table>
                                <tr>
                                    <td class="textlevelleft">
                                        Task Type
                                    </td>
                                    <td class="textlevelleft">
                                        Task</td>
                                    <td class="textlevelleft">
                                        Grade
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlTaskType" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTask" runat="server" Width="250px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGrade" runat="server" Width="100px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="Button2" runat="server" Text="Add" Width="70px" OnClick="btnAdd_Click" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <div style="overflow: scroll; width: 100%;">
                                <asp:GridView ID="GridView2" runat="server" Width="99%" Font-Size="9px" EmptyDataText="No Record Found"
                                    AutoGenerateColumns="False" OnRowCommand="grActivityList_RowCommand" DataKeyNames="ActivityName">
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
                            <legend>Appraisee's Comment on His/Her Job Obstacle </legend>
                            <table>
                                <tr>
                                    <td class="textlevelleft">
                                        Problem
                                    </td>
                                    <td class="textlevelleft">
                                        Resolution</td>
                                  
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TextBox3" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox11" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                   
                                    <td>
                                        <asp:Button ID="Button3" runat="server" Text="Add" Width="70px" OnClick="btnAdd_Click" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <div style="overflow: scroll; width: 100%;">
                                <asp:GridView ID="GridView3" runat="server" Width="99%" Font-Size="9px" EmptyDataText="No Record Found"
                                    AutoGenerateColumns="False" OnRowCommand="grActivityList_RowCommand" DataKeyNames="ActivityName">
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
                                    Appraisee comments on his/her good performance</td>
                                <td>
                                    <asp:TextBox ID="TextBox4" runat="server"  TextMode="MultiLine" Width="250px"></asp:TextBox>
                                </td>                           
                                <td class="textlevelleft">
                                    Training Need By Appraisee</td>
                                <td>
                                    <asp:TextBox ID="TextBox5" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="textlevelleft">
                                    Training Need By Appraiser
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox6" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                                </td>                           
                                <td class="textlevelleft">
                                    3 Important Training List</td>
                                <td>
                                    <asp:TextBox ID="TextBox12" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="textlevelleft">
                                    &nbsp;Next 6 Monthe Work Plan
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox7" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                                </td>                           
                                <td class="textlevelleft">
                                    Appraisee&#39;s Idea to Improve MS</td>
                                <td>
                                    <asp:TextBox ID="TextBox8" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="textlevelleft">
                                    Appraisee&#39;s Comment </td>
                                <td>
                                    <asp:TextBox ID="TextBox9" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                                </td>                            
                                <td class="textlevelleft">
                                    Appraisal Meeting Date Time In Hour</td>
                                <td>
                                    <asp:TextBox ID="TextBox10" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="textlevelleft">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
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
                        Performance Appraisal List
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div style="overflow: scroll; width: 100%; height: 400px">
                            <asp:GridView ID="grAppraisalMstList" runat="server" Width="99%" Font-Size="9px"
                                EmptyDataText="No Record Found" AutoGenerateColumns="False" DataKeyNames="AppId,FiscalYrId,IsMidTerm,EntryDate">
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
