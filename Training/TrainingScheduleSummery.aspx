<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="TrainingScheduleSummery.aspx.cs" Inherits="Training_TrainingScheduleSummery" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>
    <div class="officeSetup">
        <div id="formhead1">
            <div style="width: 96%; float: left;">
                Training Schedule Summery </div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
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
                            <asp:Label ID="Label1" runat="server" CssClass="textlevel" Text="To :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" Width="250px"></asp:TextBox>
                        </td>
                        <td>

                        </td>
                         <td>
                            <asp:Label ID="Label3" runat="server" CssClass="textlevel" Text="From :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" CssClass="textlevel" Text="CC :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" Width="250px"></asp:TextBox>
                        </td>
                        <td>

                        </td>
                         <td>
                            <asp:Label ID="Label4" runat="server" CssClass="textlevel" Text="Subject :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox3" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>



                    <tr>
                     <td><asp:Label ID="Label5" runat="server" CssClass="textlevel" Text="Attached :"></asp:Label></td>
                     <td>
                            <asp:TextBox ID="txtStrDate" runat="server" Width="250px">
                            </asp:TextBox>
                      </td>
                      <td></td>
                     <td><asp:Label ID="Label6" runat="server" CssClass="textlevel" Text="Send Req. Within :"></asp:Label></td>
                     <td>
                            <asp:TextBox ID="txtEndDate" runat="server" Width="89px"></asp:TextBox>
                            <a href="javascript:NewCal('<%= txtEndDate.ClientID %>','ddmmyyyy')">
                               <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                               height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a>
                               <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEndDate"
                               CssClass="validator" ErrorMessage="Invalid" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                     </td>
                   
                    </tr>
                   
                   
                    <tr>
                     <td>Training Plan :</td>
                     <td colspan="4"><asp:TextBox ID="txtOtherIncome" runat="server" Width="540px" TextMode="MultiLine"></asp:TextBox></td>
                     <td>&nbsp;ADD</td>

                     </tr>

                    <tr>
                        <td style="height: 22px">
                        </td>
                        <td style="height: 22px">
                            <asp:CheckBox ID="chkInActive" runat="server" CssClass="textlevelleft" Text="Mark Inactive" />
                        </td>
                        <td style="height: 22px">
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div class="GridFormat1">
            <!--Grid view Code starts-->
            <asp:GridView ID="grList" runat="server" DataKeyNames="TrainingId" AutoGenerateColumns="False"
                EmptyDataText="No Record Found" Font-Size="9px" Width="99%" 
                onrowcommand="grList_RowCommand">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                </SelectedRowStyle>
                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                <Columns>
                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="TrainingName" HeaderText="Trainin Name">
                        <ItemStyle CssClass="ItemStylecss" Width="80%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="IsActive" HeaderText="Is Active">
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
    </div>
</asp:Content>





