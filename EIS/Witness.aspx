<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="Witness.aspx.cs" Inherits="EIS_Witness" Title="Witness" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

    <script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js">
    //Date Time Picker script
    </script>

    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>

    <script language="javascript" type="text/javascript">
    function ToUpper(ctrl)
    {   
    var t = ctrl.value;
    ctrl.value = t.toUpperCase(); 
    }
    </script>

    <div class="empTrainForm" style="height:600px"  >
        <div id="formhead2">
            <div style="width: 97%; float: left;">
                References</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="PayrollConfigInner" style="height:250px ">
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
                           <%-- <td style="width: 55px">
                                <asp:Button ID="Button1" runat="server" Text="Report for this Id" Width="114px" OnClick="btnSave_Click"
                    ToolTip="Click on Save Button to store the employee data." /></td>--%>
                           <%-- <td style="width: 55px">
                                <asp:Button ID="Button2" runat="server" Text="Report for all Id" Width="114px" OnClick="btnSave_Click"
                    ToolTip="Click on Save Button to store the employee data." /></td>--%>
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
                                Organization :</td>
                            <td>
                                <asp:Label ID="lblCompany" runat="server"></asp:Label></td>
                        </tr>
                         <tr>
                            <td class="textlevel">
                                Project :</td>
                            <td>
                                <asp:Label ID="lblProject" runat="server"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td class="textlevel" style="height: 16px">
                                Department :</td>
                            <td style="height: 16px">
                                <asp:Label ID="lblDept" runat="server"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td class="textlevel" style="height: 16px">
                                Sub Department :</td>
                            <td style="height: 16px">
                                <asp:Label ID="lblSubDept" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel" style="height: 16px">
                                Subcode :</td>
                            <td style="height: 16px">
                                <asp:Label ID="lblSuncode" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Designation :</td>
                            <td>
                                <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>

            <fieldset style="margin-bottom: 10px;">
                <legend>Witness Details</legend>
                <table>
                    <tr>
                        <td class="textlevel">
                            Reference Name :</td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" MaxLength="20" Width="209px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Reference Address :</td>
                        <td>
                            <asp:TextBox ID="txtAddress" runat="server" Width="209px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Date of Signature :</td>
                        <td>
                            <asp:TextBox ID="txtSignatureDate" runat="server" ToolTip="Input the confirmation date."
                                Width="80px"></asp:TextBox>
                            <a href="javascript:NewCal('<%= txtSignatureDate.ClientID %>','ddmmyyyy')">
                                <img style="border: 0px;" height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtSignatureDate"
                                CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator></td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                <asp:HiddenField ID="hfId" runat="server" />
            </fieldset>
            <div class="DivCommand1" style="width: 98%;">
                <div class="DivCommandL">
                    <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" Text="Refresh"
                        Width="70px" OnClick="btnRefresh_Click" ToolTip="Click on Save Button to store the employee data." />
                </div>
                <div class="DivCommandR">
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click"
                        ToolTip="Click on Save Button to store the employee data." />
                    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="javascript:return DeleteConfirmation();"
                    Text="Delete" Width="70px" />
                </div>
            </div>
            </div> 
            <div style="float: left; margin-top: 10px;margin-left: 20px; width: 96%;">
            <fieldset>
                <legend>Witness List</legend>
                <div style="overflow: scroll; width: 100%; height: 250px">
                    <asp:GridView ID="grWitness" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                        AutoGenerateColumns="False" DataKeyNames="WitnessId" 
                        
                        ToolTip="8.&#9;You will find the entire existing advice list of the employee. Click on any confirmation Edit link from the list." 
                        onrowcommand="grWitness_RowCommand">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                <ItemStyle Width="5%" CssClass="ItemStylecss" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="WitnessName" HeaderText="Reference Name">
                                <ItemStyle CssClass="ItemStylecss" Width="35%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="WitnessAdd" HeaderText="Reference Address">
                                <ItemStyle CssClass="ItemStylecss" Width="40%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SignDate" HeaderText="Signature Date">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </fieldset>
           </div> 
    </div>
</asp:Content>
