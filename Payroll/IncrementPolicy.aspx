<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="IncrementPolicy.aspx.cs" Inherits="Payroll_IncrementPolicy" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>
    <script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js">
        //Date Time Picker script
    </script>
    <div class="setup">
        <div id='formhead1'>
            <div style="width: 96%; float: left;">
                Increment Policy Setup</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <!--Div for group-->
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="setupInner">
            <fieldset>
                <!--Div for Controls-->
                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                <asp:HiddenField ID="hfID" runat="server" />
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" CssClass="textlevel" Text="Inc Policy Name :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtIncPolicy" runat="server" Width="309px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtIncPolicy"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Start 
                            Month :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStartMonth" runat="server" Width="100px" 
                                CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtIncPolicy"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 17px" class="textlevel">
                            End Month :
                        </td>
                        <td style="height: 17px">
                            <asp:DropDownList ID="ddlEndMonth" runat="server" Width="100px" 
                                CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtIncPolicy"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Inc. Percentage :
                         </td>
                        <td>
                            <asp:TextBox ID="txtIncPercent" runat="server" Width="100px" 
                                CssClass="TextBoxAmt60" MaxLength="3"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtIncPolicy"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkInActive" Text="Make Inactive" CssClass="textlevelleft" runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <cc1:filteredtextboxextender id="FilteredTxt1" runat="server" filtertype="Numbers"
                    targetcontrolid="txtIncPercent">
                </cc1:filteredtextboxextender>
            </fieldset>
        </div>
        <div class="GridFormat1">
            <!--Grid view Code starts-->
            <asp:GridView ID="grList" runat="server" DataKeyNames="IncPolicyId,StartMonth,EndMonth" AutoGenerateColumns="False"
                EmptyDataText="No Record Found" Font-Size="9px" Width="100%" OnRowCommand="grList_RowCommand">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                </SelectedRowStyle>
                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                <Columns>
                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                        <ItemStyle CssClass="ItemStylecss" Width="5%" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="IncPolicyName" HeaderText="Inc Policy Name">
                        <ItemStyle CssClass="ItemStylecss" Width="40%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="StartMonth" HeaderText="Start Month">
                        <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="EndMonth" HeaderText="End Month">
                        <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="IncPercent" HeaderText="Inc Percent">
                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="IsActive" HeaderText="Is Active">
                        <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <!--Grid view Code Ends-->
        </div>
        <div class="DivCommand1">
            <div class="DivCommandL">
                <asp:Button ID="btnClear" runat="server" Text="Refresh" Width="70px" OnClick="btnClear_Click"
                    CausesValidation="false" UseSubmitBehavior="False" />
            </div>
            <div class="DivCommandR">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                    OnClick="btnDelete_Click" />
            </div>
        </div>
    </div>
</asp:Content>
