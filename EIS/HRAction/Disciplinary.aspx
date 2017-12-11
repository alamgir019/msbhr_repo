<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="Disciplinary.aspx.cs" Inherits="EIS_HRAction_Disciplinary" Title="Disciplinary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js">
    //Date Time Picker script
    </script>

    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js">
    </script>

    <script language="javascript" type="text/javascript">
    function ToUpper(ctrl)
    {   
    var t = ctrl.value;
    ctrl.value = t.toUpperCase(); 
    }
    </script>

    <div class="empTrainForm">
        <div id="formhead2">
            <div style="width: 97%; float: left;">
                Disciplinary</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="PayrollConfigInner">
            <div style="background-color: #EFF3FB; margin-bottom: 10px">
                <fieldset>
                    <table>
                        <tr>
                            <td class="textlevel">
                                Emp Id :</td>
                            <td>
                                <asp:TextBox ID="txtEmpID" runat="server" MaxLength="20" onkeyup="ToUpper(this)"
                                    Width="80px" ToolTip="Enter the Emp. Code and click on Find Button. Details of the find employee will be shown in employee general information screen if information exists. In order to enter employee."></asp:TextBox></td>
                            <td>
                                <asp:ImageButton ID="imgBtnSearch" runat="server" Height="19px" ImageUrl="~/Images/Search_Icon.jpg"
                                    OnClick="imgBtnSearch_Click" ToolTip="Enter the Emp. Code and click on Find Button. Details of the find employee will be shown in employee general information screen if information exists. In order to enter employee." /></td>
                            <td>
                                <asp:RequiredFieldValidator ID="ReqFieldVal" runat="server" ErrorMessage="*" ControlToValidate="txtEmpID"></asp:RequiredFieldValidator></td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td class="textlevel">
                                Name :</td>
                            <td>
                                <asp:Label ID="lblName" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Designation :</td>
                            <td>
                                <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td class="textlevel">
                                Project :</td>
                            <td>
                                <asp:Label ID="lblSector" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Department :</td>
                            <td>
                                <asp:Label ID="lblDept" runat="server"></asp:Label>
                            </td>
                        </tr>
                       
                        <tr>
                            <td class="textlevel">
                                Entry Date :</td>
                            <td>
                                <asp:TextBox ID="txtEntryDate" runat="server" Width="80px"></asp:TextBox>
                                 <a href="javascript:NewCal('<%= txtEntryDate.ClientID %>','ddmmyyyy')">
                                <img style="border: 0px;" height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEntryDate"
                                    CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator></td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <fieldset style="margin-bottom: 10px;">
                <legend>Disciplinary Details</legend>
                <table>
                    <tr>
                        <td class="textlevel">
                            Name of Action :</td>
                        <td>
                            <asp:DropDownList ID="ddlAction" runat="server" CssClass="textlevelleft" Width="300px"
                                ToolTip="Select the name of action for confirmation from the list.">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Reason :</td>
                        <td>

                        <asp:DropDownList ID="ddlReasonList" runat="server" CssClass="textlevelleft" Width="300px"
                                ToolTip="Select Reason from Reason List">
                            </asp:DropDownList></td>
                            
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Date of Action :</td>
                        <td>
                            <asp:TextBox ID="txtActionDate" runat="server" ToolTip="Input the confirmation date."
                                Width="80px"></asp:TextBox>
                            <a href="javascript:NewCal('<%= txtActionDate.ClientID %>','ddmmyyyy')">
                                <img style="border: 0px;" height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtActionDate"
                                CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Review Date :</td>
                        <td>
                            <asp:TextBox ID="txtReviewDate" runat="server" ToolTip="Input the confirmation date."
                                Width="80px"></asp:TextBox>
                            <a href="javascript:NewCal('<%= txtReviewDate.ClientID %>','ddmmyyyy')">
                                <img style="border: 0px;" height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtReviewDate"
                                CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsReviewed" runat="server" CssClass="textlevelleft" Text="Is Reviewed"
                                Width="150px" />
                            <asp:CheckBox ID="chkIsSuspendInc" runat="server" CssClass="textlevelleft"
                                    Text="Is Suspend Increment" Width="129px" /></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Remarks :
                        </td>
                        <td>
                            <asp:TextBox ID="txtRemarks" runat="server" MaxLength="200" Width="292px" 
                                TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                <asp:HiddenField ID="hfId" runat="server" />
            </fieldset>
            <fieldset>
                <legend>Disciplinary List</legend>
                <div style="overflow: scroll; width: 100%; height: 250px">
                    <asp:GridView ID="grDisciplinary" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                        AutoGenerateColumns="False" DataKeyNames="DisciplinaryId,ActionId,ReasonId"
                        
                        ToolTip="8.&#9;You will find the entire existing advice list of the employee. Click on any confirmation Edit link from the list." 
                        onrowcommand="grDisciplinary_RowCommand">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                <ItemStyle Width="5%" CssClass="ItemStylecss" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="EntryDate" HeaderText="Entry Date">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ReasonName" HeaderText="Reason">
                                <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ActionDate" HeaderText="Action Date">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ReviewDate" HeaderText="Review Date">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="IsReview" HeaderText="Is Review">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="IsSuspendInc" HeaderText="Is Suspend Increment ">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
        <div class="DivCommand1" style="width: 98%;">
            <div class="DivCommandL">
                <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" Text="Refresh"
                    Width="70px" OnClick="btnRefresh_Click" ToolTip="Click on Save Button to store the employee data." />
            </div>
            <div class="DivCommandR">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click"
                    ToolTip="Click on Save Button to store the employee data." />
            </div>
        </div>
    </div>
</asp:Content>
