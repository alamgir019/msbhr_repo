<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="TextFileImporter.aspx.cs" Inherits="Attendance_TextFileImporter" Title="Text File Importer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="formStyle" style="height: 520px;">
        <div id="formhead3">
            Text File Importer</div>
        <div class="MsgBox" style="margin-bottom: 20px;">
            <table style="width: 100%">
                <tr>
                    <td style="width: 50%; text-align: left;">
                        <asp:Label ID="lblLog" runat="server" CssClass="msglabel"></asp:Label>
                    </td>
                    <td style="width: 40%; text-align: right;">
                        <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="iesEmp">
            <table style="width: 80%;">
                <tr>
                    <td class="textlevel" style="text-align: right; width: 10%">
                        Browse File
                    </td>
                    <td style="text-align: left; width: 40%">
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="350px" />
                        <asp:Label ID="Label1" runat="server" Text="1" Width="20px" BackColor="green" ForeColor="white"
                            Font-Size="large" Style="text-align: center"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 40%">
                        <asp:Button ID="btnUpload" runat="server" Text="Upload Data" Width="100px" OnClick="btnUpload_Click" />
                        <asp:Label ID="Label2" runat="server" BackColor="Green" Font-Size="Large" ForeColor="White"
                            Style="text-align: center" Text="2" Width="20px"></asp:Label></td>
                </tr>
            </table>
        </div>
        <table style="width: 99%;">
            <tr>
                <td style="width: 50%;">
                    <fieldset>
                        <legend>Data From Text File </legend>
                        <div style="overflow: scroll; height: 350px; width: 99%; margin-left: 10px; margin-top: 10px;">
                            <asp:GridView ID="grTextData" runat="server" DataKeyNames="STATUS" AutoGenerateColumns="false"
                                Width="120%">
                                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" HorizontalAlign="Left" Font-Size="10px" />
                                <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeaderLeft" Font-Bold="True"
                                    ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="#EFF3FB" Font-Size="10px" />
                                <RowStyle Font-Size="11px" />
                                <Columns>
                                    <asp:BoundField DataField="SRNO" HeaderText="No.">
                                        <ItemStyle CssClass="ItemStylecss" Width="2%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CARDNO" HeaderText="Card ID">
                                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EMPID" HeaderText="Emp. ID">
                                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FULLNAME" HeaderText="Name">
                                        <ItemStyle CssClass="ItemStylecss" Width="25%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ATTDATE" HeaderText="Date">
                                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ATTTIME" HeaderText="Time">
                                        <ItemStyle CssClass="ItemStylecss" Width="20%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CONTROLLER" HeaderText="Terminal">
                                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="INOUT" HeaderText="In/Out">
                                        <ItemStyle CssClass="ItemStylecss" Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DOOR" HeaderText="Door">
                                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </fieldset>
                </td>
                <td style="width: 40%;">
                    <fieldset>
                        <legend>Prepared Data for Merging </legend>
                        <div style="overflow: scroll; height: 350px; width: 90%; margin-left: 10px; margin-top: 10px;">
                            <asp:GridView ID="grLoginLogout" runat="server" Width="98%" AutoGenerateColumns="false">
                                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" HorizontalAlign="Left" Font-Size="10px" />
                                <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeaderLeft" Font-Bold="True"
                                    ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="#EFF3FB" Font-Size="10px" />
                                <RowStyle Font-Size="11px" />
                                <Columns>
                                    <asp:BoundField DataField="UserName" HeaderText="User Name">
                                        <ItemStyle CssClass="ItemStylecss" Width="20%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LoginTime" HeaderText="Login Time">
                                        <ItemStyle CssClass="ItemStylecss" Width="30%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LogoutTime" HeaderText="Logout Time">
                                        <ItemStyle CssClass="ItemStylecss" Width="30%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="IsExist" HeaderText="Is Exist">
                                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </fieldset>
                </td>
            </tr>
        </table>
        <div style="text-align: center; margin-top: 5px;">
            <asp:Label ID="Label3" runat="server" BackColor="Green" Font-Size="Large" ForeColor="White"
                Style="text-align: center" Text="3" Width="20px"></asp:Label>
            <asp:Button ID="btnMerge" runat="server" Text="Prepare Data for Merging" Width="200px"
                OnClick="btnMerge_Click" />
            <asp:Button ID="btnApply" runat="server" Text="Marge Data" Width="200px" OnClick="btnApply_Click" />
            <asp:Label ID="Label4" runat="server" BackColor="Green" Font-Size="Large" ForeColor="White"
                Style="text-align: center" Text="4" Width="20px"></asp:Label></div>
    </div>
</asp:Content>
