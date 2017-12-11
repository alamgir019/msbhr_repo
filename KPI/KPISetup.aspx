<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="KPISetup.aspx.cs" Inherits="KPI_KPISetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>
    <div class="officeSetup">
        <div id="formhead1">
            <div style="width: 96%; float: left;">
                KPI Setup</div>
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
                        <td><asp:DropDownList ID="ddlGroup" runat="server" CssClass="textlevelleft" ToolTip="Select Group." Width="300px">
                            </asp:DropDownList></td>
                        <td></td>
                    </tr>
                     <tr>
                        <td><asp:Label ID="Label3" runat="server" CssClass="textlevel" Text="Indicator Type:"></asp:Label></td>
                        <td><asp:DropDownList ID="ddlIndicator" runat="server" CssClass="textlevelleft" ToolTip="Indicator Type." Width="300px">
                            </asp:DropDownList></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" CssClass="textlevel" Text="KPI :"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtKPI" runat="server" Width="450px" TextMode="MultiLine" Height="70px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtKPI"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
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
            <asp:GridView ID="grKPI" runat="server" DataKeyNames="KpiId,GroupId,IndTypeId" AutoGenerateColumns="False"
                EmptyDataText="No Record Found" Font-Size="9px" OnRowCommand="grKPI_RowCommand"
                OnSelectedIndexChanged="grKPI_SelectedIndexChanged" Width="99%">
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

                    <asp:BoundField DataField="IndicatirTypeName" HeaderText="Indicator Type">
                        <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="KPIName" HeaderText="KPI">
                        <ItemStyle CssClass="ItemStylecss" Width="65%"></ItemStyle>
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
