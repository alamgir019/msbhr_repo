<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="OrientationTraining.aspx.cs" Inherits="Training_OrientationTraining" Title="Orientation Training" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js">
        //Date Time Picker script
    </script>
    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>
    <div class="departmentSetup">
        <div id='formhead2'>
            <div style="width: 96%; float: left;">
                Orientation Training</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <!--Div for group-->
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div style="background-color: #EFF3FB; margin-bottom: 10px; margin-left: 13px; margin-right: 13px;">
            <fieldset>
                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                <asp:HiddenField ID="hfID" runat="server" />
                <table>
                    <tr>
                        <td class="textlevel">
                            Emp Id :
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmpID" Width="80px" runat="server"></asp:TextBox>  <%--onkeyup="ToUpper(this)"--%>&nbsp;
                            <asp:ImageButton ID="imgBtnSearch" runat="server" Height="19px" ImageUrl="~/Images/Search_Icon.jpg"
                                OnClick="imgBtnSearch_Click" CausesValidation="False" ClientIDMode="Predictable" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmpID"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
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
                            JobTitle :
                        </td>
                        <td>
                            <asp:Label ID="lblJobTitle" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Sector :
                        </td>
                        <td>
                            <asp:Label ID="lblSector" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Department :
                        </td>
                        <td>
                            <asp:Label ID="lblDept" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div style="margin-left: 13px; margin-right: 13px;">
            <fieldset>
                <legend>Orientation Training Details</legend>
                <table>
                    <tr>
                        <td class="textlevel">
                            First Day Oriented
                        </td>
                        <td>
                            <asp:CheckBox ID="chkFDayOrien" runat="server" />
                        </td>
                        <td class="textlevel">
                            First Day OT Date :
                        </td>
                        <td>
                            <asp:TextBox runat="server" Width="80px" ID="txtFDayOTDate"></asp:TextBox>

                        &nbsp;<a href="javascript:NewCal('<%= txtFDayOTDate.ClientID %>','ddmmyyyy')" ><img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                height='16' alt="Pick a date" src="../images/cal.gif" width='16'/></a><asp:RegularExpressionValidator runat="server" 
                                ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$" 
                                ControlToValidate="txtFDayOTDate" ErrorMessage="Invalid Date" 
                                CssClass="validator" ID="RegularExpressionValidator14"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Agency & Child Safety Policy Oriented
                        </td>
                        <td>
                            <asp:CheckBox ID="chkAnCSPori" runat="server" />
                        </td>
                        <td class="textlevel">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Orientation Training
                        </td>
                        <td>
                            <asp:CheckBox ID="chkOT" runat="server" />
                        </td>
                        <td class="textlevel">
                            Orientation Training Date :
                        </td>
                        <td>
                            <asp:TextBox ID="txtOTDate" runat="server" Width="80px"></asp:TextBox>
                            <a href="javascript:NewCal('<%= txtOTDate.ClientID %>','ddmmyyyy')">
                                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                    height='16' alt="Pick a date" src="../images/cal.gif" width='16' /></a>
                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtOTDate"
                                ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator12" 
                                runat="server" ControlToValidate="txtOTDate"
                                CssClass="validator" ErrorMessage="Invalid Date" 
                                
                                ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Remarks :
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div style="margin-left: 13px; margin-right: 13px;">
        <fieldset>
            <legend>Orientation Training List</legend>
            <div style="overflow: scroll; width: 100%; height: 250px;" >
                <asp:GridView ID="grOriTrain" runat="server" Width="99%" Font-Size="9px" EmptyDataText="No Record Found"
                    AutoGenerateColumns="False" DataKeyNames="OriTrainingID,FirstOrient,FirstOrientDate,AgeChiPoliOrient,OrienTraining,OrienTrainingDate,Remarks"
                    OnRowCommand="grOriTrain_RowCommand">
                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                    </SelectedRowStyle>
                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                    <Columns>
                        <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                            <ItemStyle Width="5%" CssClass="ItemStylecss" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="FirstOrient" HeaderText="First Day Orientation">
                            <ItemStyle CssClass="ItemStylecss" Width="21%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FirstOrientDate" HeaderText="First Orientation Date" DataFormatString="{0:dd/MM/yyyy}">
                            <ItemStyle CssClass="ItemStylecss" Width="21%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="AgeChiPoliOrient" HeaderText=" Agency & Child Safety Policy Oriented">
                            <ItemStyle CssClass="ItemStylecss" Width="21%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="OrienTraining" HeaderText="Orientation Training">
                            <ItemStyle CssClass="ItemStylecss" Width="21%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="OrienTrainingDate" HeaderText="OT Date" DataFormatString="{0:dd/MM/yyyy}">
                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </fieldset>
        </div>
        <div class="DivCommand1">
            <div class="DivCommandL">
                <asp:Button ID="btnClear" runat="server" Text="Refresh" Width="70px" OnClick="btnClear_Click"
                    UseSubmitBehavior="False" CausesValidation="false" />
            </div>
            <div class="DivCommandR">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                    OnClick="btnDelete_Click" />
            </div>
            <br />
        </div>
        
    </div>
</asp:Content>
