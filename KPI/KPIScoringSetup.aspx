<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="KPIScoringSetup.aspx.cs" Inherits="KPI_KPIScoringSetup" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>
    <div class="officeSetup">
        <div id="formhead1">
            <div style="width: 96%; float: left;">
                KPI Scoring Setup</div>
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
                         <td> <asp:Label ID="Label1" runat="server" CssClass="textlevel" Text="Group Title:"></asp:Label></td>
                         <td colspan="3"><asp:DropDownList ID="ddlGroup" runat="server" CssClass="textlevelleft" ToolTip="Select Group." Width="180px">
                            </asp:DropDownList></td>
                         <td></td>
                         <td><asp:Label ID="Label3" runat="server" CssClass="textlevel" Text="Quarter :"></asp:Label></td>
                         <td colspan="2"><asp:DropDownList ID="ddlQuarter" runat="server" CssClass="textlevelleft" ToolTip="Select Grade" Width="180px">
                            </asp:DropDownList></td>
                         
                    </tr>
                     
                     <tr>

                        <td>
                            <asp:Label ID="Label5" runat="server" CssClass="textlevel" Text="Marks From :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMFrom" runat="server" Width="60px" ></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label6" runat="server" CssClass="textlevel" Text="Marks To :"></asp:Label>
                        </td>
                        <td >
                            <asp:TextBox ID="txtMTo" runat="server" Width="60px" ></asp:TextBox>
                        </td>
                        <td></td>
                        <td>
                            <asp:Label ID="Label4" runat="server" CssClass="textlevel" Text="Year :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtYear" runat="server" Width="80px" ></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtYear"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" CssClass="textlevel" Text="Rating :"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtRating" runat="server" Width="180px" ></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRating"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>Score</td>
                        <td> <asp:TextBox ID="txtScore" runat="server" Width="80px" ></asp:TextBox></td>
                        <td> <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtScore"
                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="height: 22px">
                        </td>
                        <td style="height: 22px" colspan="3">
                            <asp:CheckBox ID="chkInActive" runat="server" CssClass="textlevelleft" Text="Mark Inactive" />
                        </td>
                        <td style="height: 22px">
                        </td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <br />
        </div>
        <div class="GridFormat1">
            <!--Grid view Code starts-->
            <asp:GridView ID="grScoring" runat="server" DataKeyNames="ScoringID,GroupId,QuarterId,MarksF,MarksTo,Score" AutoGenerateColumns="False"
                EmptyDataText="No Record Found" Font-Size="9px" OnRowCommand="grScoring_RowCommand"
                OnSelectedIndexChanged="grScoring_SelectedIndexChanged" Width="99%">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                </SelectedRowStyle>
                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                <Columns>
                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                        <ItemStyle CssClass="ItemStylecss" Width="5%" />
                    </asp:ButtonField>
 
                    <asp:BoundField DataField="GroupName" HeaderText="Group Name">
                        <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="QuarterName" HeaderText="Quarter">
                        <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="VYear" HeaderText="Year">
                        <ItemStyle CssClass="ItemStylecss" Width="8%"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="Marks" HeaderText="Marks">
                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="Rating" HeaderText="Rating">
                        <ItemStyle CssClass="ItemStylecss" Width="45%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="Score" HeaderText="Score">
                        <ItemStyle CssClass="ItemStylecss" Width="7%"></ItemStyle>
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


