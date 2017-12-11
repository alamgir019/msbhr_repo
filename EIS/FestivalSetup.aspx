<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="FestivalSetup.aspx.cs" Inherits="EIS_FestivalSetup" %>

<asp:Content ID="MaiContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>
    <div class="setup">
        <div id='formhead1'>
            <div style="width: 96%; float: left;">
                Religion Setup</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:HiddenField ID="hfIsUpadate" runat="server" />
        <asp:HiddenField ID="hfID" runat="server" />
        <div class="setupInner">
            <fieldset>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" CssClass="textlevel" Text="Festival Name :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFestival" runat="server" Width="309px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFestival"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                         <td>
                            <asp:Label ID="Label1" runat="server" CssClass="textlevel" Text="Religion Name :"></asp:Label>
                        </td>
                       <td>
                                    <asp:DropDownList ID="ddlReligion" CssClass="textlevelleft" runat="server" Width="300px">
                                    </asp:DropDownList>
                       </td>
                        <td>
                           
                        </td>
                       </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="ChkIsActive" Text="Make Inactive" CssClass="textlevelleft" 
                                runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div class="GridFormat1">
            <asp:GridView ID="grFestival" runat="server" DataKeyNames="FestivalId,ReligionID,IsActive"
                AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" OnRowCommand="grFestival_RowCommand"
                Width="99%">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                </SelectedRowStyle>
                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                <Columns>
                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                    </asp:ButtonField>

                    <asp:BoundField DataField="Festival" HeaderText="Festival Name">
                        <ItemStyle CssClass="ItemStylecss" Width="50%"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="ReligionName" HeaderText="Religion Name">
                        <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="IsActive" HeaderText="Is Active">
                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
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


