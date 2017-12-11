<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="Weekend.aspx.cs" Inherits="Attendance_Weekend" Title="Weekend Setup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="Form700">
        <div id="formhead1">
            Weekend Setup
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div class="Div650" style="margin-left: 15px; margin-right: 15px; width:97%;">
            <fieldset>
                <table>
                    <tr>
                        <td style="width: 3px">
                            <asp:Label ID="Label1" runat="server" CssClass="textlevel" Text="Weekend Profile Name"></asp:Label>
                        </td>
                        <td style="width: 3px">
                            <asp:TextBox ID="txtPrifleName" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                        </td>
                        <td style="width: 3px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPrifleName"
                                CssClass="validator" ErrorMessage="Enter a profile name" Width="100px"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 3px" valign="top">
                            <asp:Label ID="Label2" runat="server" CssClass="textlevel" Text="Select Weekend Days"></asp:Label>
                        </td>
                        <td style="width: 3px">
                            <asp:CheckBoxList ID="chkDayList" runat="server" BorderColor="Gray" BorderStyle="Solid"
                                BorderWidth="1px" Width="150px">
                                <asp:ListItem Value="SUN">Sunday</asp:ListItem>
                                <asp:ListItem Value="MON">Monday</asp:ListItem>
                                <asp:ListItem Value="TUE">Tuesday</asp:ListItem>
                                <asp:ListItem Value="WED">Wednesday</asp:ListItem>
                                <asp:ListItem Value="THU">Thursday</asp:ListItem>
                                <asp:ListItem Value="FRI">Friday</asp:ListItem>
                                <asp:ListItem Value="SAT">Saturday</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td style="width: 3px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 3px" valign="top">
                        </td>
                        <td style="width: 3px">
                            <asp:CheckBox ID="chkInActive" runat="server" CssClass="textlevel" Text="Make Inactive"
                                Width="90px" />
                        </td>
                        <td style="width: 3px">
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                <asp:HiddenField ID="hfWeekendId" runat="server" />
            </fieldset>
        </div>
        <div class="Grid650">
            <asp:GridView ID="grWeekend" runat="server" AutoGenerateColumns="False" DataKeyNames="WeekEndID"
                EmptyDataText="No Record Found" Font-Size="9px" OnRowCommand="grWeekend_RowCommand"
                Width="450px">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeaderLeft" Font-Bold="True"
                    ForeColor="#333333" />
                <AlternatingRowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                        <ItemStyle CssClass="ItemStylecss" Width="40px" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="WEPackName" HeaderText="Profile Name">
                        <ItemStyle CssClass="ItemStylecss" Width="230px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TotalWeekEnd" HeaderText="No of Days">
                        <ItemStyle CssClass="ItemStylecss" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="IsActive" HeaderText="Is Active">
                        <ItemStyle CssClass="ItemStylecss" Width="80px" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HiddenField ID="hfWESun" runat="server" Value='<%# Convert.ToString(Eval("WESun")) %>' />
                            <asp:HiddenField ID="hfWEMon" runat="server" Value='<%# Convert.ToString(Eval("WEMon")) %>' />
                            <asp:HiddenField ID="hfWETues" runat="server" Value='<%# Convert.ToString(Eval("WETues")) %>' />
                            <asp:HiddenField ID="hfWEWed" runat="server" Value='<%# Convert.ToString(Eval("WEWed")) %>' />
                            <asp:HiddenField ID="hfWETue" runat="server" Value='<%# Convert.ToString(Eval("WETue")) %>' />
                            <asp:HiddenField ID="hfWEFri" runat="server" Value='<%# Convert.ToString(Eval("WEFri")) %>' />
                            <asp:HiddenField ID="hfWESat" runat="server" Value='<%# Convert.ToString(Eval("WESat")) %>' />
                        </ItemTemplate>
                        <ItemStyle CssClass="ItemStylecss" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="DivCommand1">
            <div class="DivCommandL">
                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px" CausesValidation="False"
                    OnClick="btnRefresh_Click" />
            </div>
            <div class="DivCommandR">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" UseSubmitBehavior="False"
                    OnClick="btnSave_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClick="btnDelete_Click" />
                <asp:Button ID="btnClose" runat="server" Text="Close" Width="70px" CausesValidation="False" />
            </div>
        </div>
    </div>
</asp:Content>
