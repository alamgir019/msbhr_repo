<%@ Page EnableEventValidation="false" EnableViewState="True" Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="TrainingYearlyPlan.aspx.cs" Inherits="Training_TrainingYearlyPlan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>
    <script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js">
    </script>
    <script type="text/javascript">
    // view ajaxcascadingdropdown
        function SelectTrainingDtl() {
            var ddlTrainingName = $("[id*=ddlTrainingName]");
            $.ajax({
                type: "POST",
                url: "../MSBWebService.asmx/SelectTrainingDtlWithDesig",
                data: '{trainId:' + ddlTrainingName.val() + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    alert(r.d);
                    FillDropdown(r, "ddlDesignation");
                },
                error: function (xhr, err) {
                    alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                    alert("responseText: " + xhr.responseText);
                }
            });
        } 
        function getDuration() {
            var strDate = stringToDate(document.getElementById('<%=txtStartDate.ClientID%>').value, "dd/MM/yyyy", "/");
            var endDate = stringToDate(document.getElementById('<%=txtEndDate.ClientID%>').value, "dd/MM/yyyy", "/");
            var diff = daydiff(strDate, endDate) + 1;
            if (diff < 0) {
                //alert('Start Date Cannot be Greater Than End Date.');
                document.getElementById('<%=lblMsg.ClientID%>').innerHTML = 'Start Date Cannot be Greater Than End Date.';
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
   <div class="officeSetup" style="width: 80%">
        <div id="formhead1">
            <div style="width: 96%; float: left;">
                Yearly Training Plan and Budget</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        
        <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
             Width="100%" Height="760px">            
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Width="90%" Height="600px">
                <HeaderTemplate>
                    Training Plan Setup
                </HeaderTemplate>
                <ContentTemplate> 
                <div class="MsgBox">
                    <!--Div for msg-->
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
                
        <div class="officeSetupInner">
            <fieldset style="width:98%">
                <!--Div for Controls-->
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                <table>
                    <tr>
                        <td><asp:Label ID="Label2" runat="server" CssClass="textlevel" Text="Year :"></asp:Label></td> 
                         <td>
                         <asp:DropDownList ID="ddlYear" runat="server" CssClass="textlevelleft" Width="90%"
                                ToolTip="Select Training Nmae"></asp:DropDownList>
                        </td> 
                        <td><asp:Label ID="Label4" runat="server" CssClass="textlevel" Text="Plan Name :"></asp:Label></td>  
                        <td><asp:TextBox ID="txtPlanName" runat="server" Width="90%" MaxLength="60"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtPlanName"></asp:RequiredFieldValidator>
                        </td>
                        <td><asp:Label ID="Label1" runat="server" CssClass="textlevel" Text="Training Name :"></asp:Label></td>                                 
                         <td>
                            <asp:DropDownList ID="ddlTrainingName" runat="server" CssClass="textlevelleft" AutoPostBack="True"
                                 Width="90%" OnSelectedIndexChanged="ddlTrainingName_SelectedIndexChanged"
                                ToolTip="Select Training Name">
                            </asp:DropDownList>
                            <asp:CompareValidator id="CompareValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlTrainingName" Operator="NotEqual" ValueToCompare="-1"></asp:CompareValidator>
                         </td>
                        <td><asp:Label ID="Label13" runat="server" CssClass="textlevel" Text="Training Type :"></asp:Label></td>  
                        <td><asp:TextBox ID="txtTrainingType" runat="server" Width="90%" MaxLength="60"></asp:TextBox></td>
                    </tr>
                    <tr>
                        
                        <td><asp:Label ID="Label19" runat="server" CssClass="textlevel" Text="Total Participant :"></asp:Label></td>  
                        <td>
                        <asp:TextBox ID="txtTotalParticipant" runat="server" Width="90%" MaxLength="60" type="Number"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtTotalParticipant"></asp:RequiredFieldValidator>                        
                        </td>

                        <td><asp:Label ID="Label14" runat="server" CssClass="textlevel" Text="Course Fee :"></asp:Label></td>  
                        <td><asp:TextBox ID="txtCourseFee" runat="server" Width="90%" MaxLength="60" type="Number"></asp:TextBox></td>
                        <td><asp:Label ID="Label15" runat="server" CssClass="textlevel" Text="Perdiem/Pocket Money :"></asp:Label></td>  
                        <td><asp:TextBox ID="txtPerdiem" runat="server" Width="90%" MaxLength="60" type="Number"></asp:TextBox></td>
                        <td><asp:Label ID="Label16" runat="server" CssClass="textlevel" Text="Food/Accm/Others:"></asp:Label></td>  
                        <td><asp:TextBox ID="txtFAOthers" runat="server" Width="90%" MaxLength="60" type="Number"></asp:TextBox></td>
                        
                    </tr>
                        <tr>
                        
                        <td><asp:Label ID="Label17" runat="server" CssClass="textlevel" Text="Transport :"></asp:Label></td>  
                        <td><asp:TextBox ID="txtTransport" runat="server" Width="90%" MaxLength="60" type="Number"></asp:TextBox></td>
                        <td><asp:Label ID="Label18" runat="server" CssClass="textlevel" Text="Local Transport:"></asp:Label></td>  
                        <td><asp:TextBox ID="txtLocalTransport" runat="server" Width="90%" MaxLength="60" type="Number"></asp:TextBox></td>
                        <td><asp:Label ID="Label20" runat="server" CssClass="textlevel" Text="Clinical Practical Cost:"></asp:Label></td>  
                        <td><asp:TextBox ID="txtPracticalCost" runat="server" Width="90%" MaxLength="60" type="Number"></asp:TextBox></td>
                        <td><asp:Label ID="Label21" runat="server" CssClass="textlevel" Text="Miscellaneous :"></asp:Label></td>  
                        <td><asp:TextBox ID="txtMiscellaneous" runat="server" Width="90%" MaxLength="60" type="Number"></asp:TextBox></td>

                        </tr>
                        <tr>  

                            <td>
                                <asp:Label ID="Label3" runat="server" CssClass="textlevel" Text="Start Date :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStartDate" runat="server" Width="70%"></asp:TextBox>
                                <a href="javascript:NewCal('<%= txtStartDate.ClientID %>','ddmmyyyy')">
                                <img alt="Pick a date" height="16" src="../images/cal.gif" style="border-right: 0px;
                                                border-top: 0px; border-left: 0px; border-bottom: 0px" width="16" />
                                </a>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                                    ControlToValidate="txtStartDate" CssClass="validator" ErrorMessage="Invalid" 
                                    ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                    ControlToValidate="txtStartDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" CssClass="textlevel" Text="End Date :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEndDate" runat="server" Width="68%"></asp:TextBox>
                                <a href="javascript:NewCal('<%= txtEndDate.ClientID %>','ddmmyyyy')">
                                <img alt="Pick a date" height="16" src="../images/cal.gif" style="border-right: 0px;
                                                border-top: 0px; border-left: 0px; border-bottom: 0px" width="16" />
                                </a>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                    ControlToValidate="txtEndDate" CssClass="validator" ErrorMessage="Invalid" 
                                    ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="txtEndDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>  
                            <td>
                                <asp:Label ID="Label7" runat="server" CssClass="textlevel" Text="Duration :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDuration" runat="server" Width="90%" type="Number"
                                            onclick="getDuration()"></asp:TextBox>

                            </td>
                            <td>
                                <asp:Label ID="Label12" runat="server" CssClass="textlevel" Text="Remarks :"></asp:Label>
                            </td>
                            <td colspan="2">
                                <textarea ID="txtRemarks" runat="server" cols="20" rows="1" ></textarea>
                            </td>                    
                         <td>
                            <asp:CheckBox ID="chkInActive" runat="server" CssClass="textlevelleft" Text="Make Inactive"/>
                            </td>                        
                        </tr>
                </table>
            </fieldset>
            <br />
             <fieldset>
                <table>
                    <tr>
                        
                        <td>
                            <asp:Label ID="Label9" runat="server" CssClass="textlevel" Text="Designation :"></asp:Label>
                        </td>
                        <td>
                           <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="textlevelleft"
                                Width="100px"></asp:DropDownList>
                        </td>
                        
                        <td><asp:Label ID="Label8" runat="server" CssClass="textlevel" Text="Funded By:"></asp:Label></td>
                         <td>
                         <asp:DropDownList ID="ddlFundedBy" runat="server" CssClass="textlevelleft" 
                                 Width="100px">
                            </asp:DropDownList>
                         </td>  
                        <td><asp:Label ID="Label5" runat="server" CssClass="textlevel" Text="No Of person :"></asp:Label></td>
                         <td><asp:TextBox ID="txtNoOfPerson" runat="server" Width="50px"  onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);"  MaxLength="5" ></asp:TextBox></td>
                         
                        <td><asp:Label ID="Label10" runat="server" CssClass="textlevel" Text="Venue:"></asp:Label></td>
                         <td>
                         <asp:DropDownList ID="ddlVenue" runat="server" CssClass="textlevelleft" AutoPostBack="True"
                                 Width="100px" onselectedindexchanged="ddlVenue_SelectedIndexChanged">
                            </asp:DropDownList>
                         </td>  
                         
                        <td><asp:Label ID="Label11" runat="server" CssClass="textlevel" Text="Location:"></asp:Label></td>
                         <td>
                         <asp:DropDownList ID="ddlLocation" runat="server" CssClass="textlevelleft" AutoPostBack="True"
                                 Width="100px" onselectedindexchanged="ddlLocation_SelectedIndexChanged">
                            </asp:DropDownList>
                         </td>  
                         <td>
                            <asp:Button ID="btnAdd" runat="server" Text="Add" Width="60px" CausesValidation="False" OnClick="btnAdd_Click"/>
                       </td>
                    </tr>
                </table>
            </fieldset>
        </div>        
        <div class="GridFormat1">
            <!--Grid view Code starts-->
            <asp:GridView ID="grList" runat="server" DataKeyNames="DesigId,DesigName,FundedBy,FundedByName,NoOfPerson,VenueId,VenueName,LocationId,LocationName"
             AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" Width="99%" 
                onrowcommand="grList_RowCommand" OnRowDeleting="grList_RowDeleting">
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
                     <asp:BoundField DataField="DesigName" HeaderText="Designation">
                        <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                    </asp:BoundField>                    
                     <asp:BoundField DataField="FundedByName" HeaderText="Funded By">
                        <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="NoOfPerson" HeaderText="No Of Person">
                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle> 
                    </asp:BoundField>
                     <asp:BoundField DataField="VenueName" HeaderText="Venue Name">
                        <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="LocationName" HeaderText="Location Name">
                        <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <!--Grid view Code Ends-->
        </div>        

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
                        Height="400px">                                                                                                                                                                                                                            
    <HeaderTemplate>
     Training Yearly Training Plan List
    </HeaderTemplate>
    <ContentTemplate>
    <div class="GridFormat3">
    <!--Grid view Code starts-->
            <asp:GridView ID="grTrainingYearlyPlan" runat="server" DataKeyNames="YrPlanId,YrPlanName,TrainId,TrainName,Year,
            StrDate,EndDate,Duration,IsActive,Remarks,CourseFee,Perdiem,FAOthers,Transport,LocalTransport,
            TrainType,TotalParticipant,PracticalCost,Miscellaneous"
             AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" Width="99%" 
                onrowcommand="grTrainingYearlyPlan_RowCommand">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                </SelectedRowStyle>
                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                <Columns>
                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                    </asp:ButtonField>
                     <asp:BoundField DataField="YrPlanName" HeaderText="Plan Name">
                        <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="TrainName" HeaderText="Train Name">
                        <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="StrDate" HeaderText="Satrt Date">
                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="EndDate" HeaderText="End Date">
                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Year" HeaderText="Year">
                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="IsActive" HeaderText="Is Active">
                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
    </div>
    </ContentTemplate>
    </cc1:TabPanel>
</cc1:TabContainer>
</div>
</asp:Content>