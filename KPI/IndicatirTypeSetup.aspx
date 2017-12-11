<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="IndicatirTypeSetup.aspx.cs" Inherits="KPI_IndicatirTypeSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>
    
    <div class="officeSetup">
        <div id="formhead1">
            <div style="width: 96%; float: left;">
                KPI Indicator Type Setup</div>
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
                            <asp:Label ID="Label2" runat="server" CssClass="textlevel" Text="Indicator Type Title :"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtIndicatirType" runat="server" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtIndicatirType"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                   

                    <tr>
                        <td style="height: 22px">
                        </td>
                        <td style="height: 22px" colspan="3">
                            <asp:CheckBox ID="chkInActive" runat="server" CssClass="textlevelleft" Text="Make Inactive" />
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
            <asp:GridView ID="grIndicatirType" runat="server" DataKeyNames="IndTypeId" AutoGenerateColumns="False"
                EmptyDataText="No Record Found" Font-Size="9px" OnRowCommand="grIndicatirType_RowCommand"
                OnSelectedIndexChanged="grIndicatirType_SelectedIndexChanged" Width="99%">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                </SelectedRowStyle>
                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                <Columns>
                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="IndicatirTypeName" HeaderText="Indicator Type">
                        <ItemStyle CssClass="ItemStylecss" Width="50%"></ItemStyle>
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



