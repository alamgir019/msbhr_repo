<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="OtherTraining.aspx.cs" Inherits="Training_OtherTraining" %>
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
                Other Training </div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
 <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%"
                    Height="500px">
       <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Width="860px"
                        Height="450px">                                                                                                                                                                                                                            
            <HeaderTemplate>
            Other Training Setup
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
                <table>
                    
                     <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" CssClass="textlevel" Text="Training Name :"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTrainingName" runat="server" CssClass="textlevelleft" Width="200px"
                                ToolTip="Select Training Name"></asp:DropDownList>
                        </td>
                      
                         <td><asp:Label ID="Label5" runat="server" CssClass="textlevel" Text="Start Date :"></asp:Label></td>
                     <td>
                            <asp:TextBox ID="txtStrDate" runat="server" Width="89px"></asp:TextBox>
                            <a href="javascript:NewCal('<%= txtStrDate.ClientID %>','ddmmyyyy')">
                               <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                               height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a><asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtStrDate"
                               CssClass="validator" ErrorMessage="Invalid" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                      </td>
                      <td><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ControlToValidate="txtStrDate"></asp:RequiredFieldValidator></td>   
                    </tr>
                    <tr>
                    <td>
                            <asp:Label ID="Label3" runat="server" CssClass="textlevel" Text="Organized By :"></asp:Label>
                        </td>
                         <td> <asp:DropDownList ID="ddlOrganizedBy" runat="server" CssClass="textlevelleft" Width="200px"
                                ToolTip="Select" ></asp:DropDownList> </td>                       
                     
                     <td><asp:Label ID="Label6" runat="server" CssClass="textlevel" Text="End Date :"></asp:Label></td>
                     <td>
                            <asp:TextBox ID="txtEndDate" runat="server" Width="89px"></asp:TextBox>
                            <a href="javascript:NewCal('<%= txtEndDate.ClientID %>','ddmmyyyy')">
                               <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                               height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a>
                               <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEndDate"
                               CssClass="validator" ErrorMessage="Invalid" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                     </td>
                   <td><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="txtEndDate"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                     <td><asp:Label ID="Label2" runat="server" CssClass="textlevel" Text="Duration :"></asp:Label></td>
                     <td><asp:TextBox ID="txtDuration" runat="server" Width="200px" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" MaxLength="3"></asp:TextBox></td>
                      <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDuration"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                      </td>
               
                    <td> <asp:CheckBox ID="chkInActive" runat="server" CssClass="textlevelleft" Text="Make Inactive" /></td>
                    <td>
                            <asp:CheckBox ID="chkCertificate" runat="server" CssClass="textlevelleft" Text="Certificate" />
                    </td>
                    </tr>
                    <tr>
                      <td><asp:Label ID="Label4" runat="server" CssClass="textlevel" Text="Remarks :"></asp:Label></td>
                     <td><asp:TextBox ID="txtRemarks" runat="server" Width="200px" TextMode="MultiLine" MaxLength="100"></asp:TextBox></td>
                      <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRemarks"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                      </td>
                     </tr>
                </table>
            </fieldset>
            <br />
            <fieldset>
              <table>

                    <tr>
                     <td> <asp:Label ID="Label7" runat="server" CssClass="textlevel" Text="Participant Name :"></asp:Label></td>
                     <td><asp:TextBox runat="server" Width="200px" ID="txtPName"></asp:TextBox>
</td>
                         <td><asp:Label ID="Label23" runat="server" CssClass="textlevel" Text="Designation :"></asp:Label></td>
                     <td><asp:TextBox ID="txtDesignation" runat="server" Width="200px"></asp:TextBox></td>  
                     <td><asp:Button ID="btnAdd" runat="server" Text="Add" Width="60px" 
                          OnClick="btnAdd_Click"  CausesValidation="False" /> </td>             
                    </tr>
                </table>
            </fieldset>
        </div>
        <div class="GridFormat1">
            <!--Grid view Code starts-->
            <asp:GridView ID="grList" runat="server" AutoGenerateColumns="False"
                EmptyDataText="No Record Found" Font-Size="9px" Width="99%" 
                onrowcommand="grList_RowCommand" OnRowDeleting="grList_RowDelete">
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
                    <asp:BoundField DataField="ParticipantName" HeaderText="Employee Name">
                        <ItemStyle CssClass="ItemStylecss" Width="80%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Designation" HeaderText="Designation">
                        <ItemStyle CssClass="ItemStylecssCenter" Width="10%"></ItemStyle>
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
                        Height="450px">                                                                                                                                                                                                                            
    <HeaderTemplate>
     Other Training List
    </HeaderTemplate>
    <ContentTemplate>
        <div class="GridFormat3">
            <!--Grid view Code starts-->

            <asp:GridView ID="grOtherTraining" runat="server" DataKeyNames="OtherTrainId,TrainId,TrainName,StartDate,EndDate,Duration,OrganizedBy,OrganizedByName,Remarks,IsCertificate,IsActive" AutoGenerateColumns="False"
                EmptyDataText="No Record Found" Font-Size="9px" Width="99%" OnRowCommand="grOtherTraining_RowCommand" >
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
                    <asp:BoundField DataField="StartDate" HeaderText="Start Date">
                        <ItemStyle CssClass="ItemStylecss" Width="40%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="EndDate" HeaderText="End Date">
                        <ItemStyle CssClass="ItemStylecss" Width="60%"></ItemStyle>
                    </asp:BoundField>  
                    <asp:BoundField DataField="Duration" HeaderText="Duration">
                        <ItemStyle CssClass="ItemStylecss" Width="50%"></ItemStyle>
                    </asp:BoundField>  
                    <asp:BoundField DataField="OrganizedByName" HeaderText="Organized By">
                        <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                    </asp:BoundField>    
                    <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                        <ItemStyle CssClass="ItemStylecss" Width="50%"></ItemStyle>
                    </asp:BoundField>   
                    <asp:BoundField DataField="IsCertificate" HeaderText="Is Certificate">
                        <ItemStyle CssClass="ItemStylecss" Width="50%"></ItemStyle>
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
  <br />
 </div>
</asp:Content>



