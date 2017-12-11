<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="TimeSheetPolicy.aspx.cs" Inherits="Attendance_TimeSheetPolicy" Title="Time Sheet Policy" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js">
    //Date Time Picker script
    </script>

    <script language="javascript" type="text/javascript">


    </script>

    <script type="text/javascript" language="javascript">
    function ToUpper(ctrl)
    {   
    var t = ctrl.value;
    ctrl.value = t.toUpperCase(); 
    }
    </script>

    <div id="holidayForm">
        <div id="formhead1">
            Time Sheet Policy</div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="holidayFormInner">
            <fieldset>
                <table>
                    <tr>
                        <td class="textlevel">
                            Year :</td>
                        <td>
                            <asp:DropDownList ID="ddlYear" CssClass="ItemStylecssCenter" runat="server" Width="80px"
                                ToolTip="Automatically current year will be selected.">
                                <asp:ListItem>2010</asp:ListItem>
                                <asp:ListItem>2011</asp:ListItem>
                                <asp:ListItem>2012</asp:ListItem>
                                <asp:ListItem>2013</asp:ListItem>
                                <asp:ListItem>2014</asp:ListItem>
                                <asp:ListItem>2015</asp:ListItem>
                                <asp:ListItem>2016</asp:ListItem>
                                <asp:ListItem>2017</asp:ListItem>
                                <asp:ListItem>2018</asp:ListItem>
                                <asp:ListItem>2019</asp:ListItem>
                                <asp:ListItem>2020</asp:ListItem>
                            </asp:DropDownList></td>
                        <td class="textlevel">
                            Month :</td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" CssClass="ItemStylecssCenter" runat="server" Width="100px"
                                ToolTip="Automatically current month will be selected.">
                                <asp:ListItem Value="1">January</asp:ListItem>
                                <asp:ListItem Value="2">February</asp:ListItem>
                                <asp:ListItem Value="3">March</asp:ListItem>
                                <asp:ListItem Value="4">April</asp:ListItem>
                                <asp:ListItem Value="5">May</asp:ListItem>
                                <asp:ListItem Value="6">June</asp:ListItem>
                                <asp:ListItem Value="7">July</asp:ListItem>
                                <asp:ListItem Value="8">August</asp:ListItem>
                                <asp:ListItem Value="9">September</asp:ListItem>
                                <asp:ListItem Value="10">October</asp:ListItem>
                                <asp:ListItem Value="11">November</asp:ListItem>
                                <asp:ListItem Value="12">December</asp:ListItem>
                            </asp:DropDownList></td>
                        <td class="textlevel">
                            Hour :</td>
                        <td>
                            <asp:TextBox ID="txtHour" runat="server" CssClass="TextBoxAmt60"></asp:TextBox></td>
                        <td>
                            <asp:Button ID="btnAdd" Text="Save" runat="server" Width="130px" OnClick="btnAdd_Click"/></td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <fieldset>
            <legend>Time Sheet Policy Records</legend>
                <asp:GridView ID="grTimeSheetPolicy" runat="server" DataKeyNames="PID,PMonth" AutoGenerateColumns="False"
                    EmptyDataText="No Record Found" Font-Size="9px" OnRowCommand="grTimeSheetPolicy_RowCommand" >
                    <HeaderStyle BackColor="#C2D69B" Font-Bold="True"></HeaderStyle>
                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                    </SelectedRowStyle>
                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                    <Columns>
                        <asp:ButtonField CommandName="EditClick" HeaderText="Edit" Text="Edit">
                            <ItemStyle Width="10%" CssClass="ItemStylecss" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="PYear" HeaderText="Year">
                            <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PMonth" HeaderText="Month">
                            <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PHour" HeaderText="Hour">
                            <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </fieldset>
        </div>
        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
            FilterType="Custom, Numbers" TargetControlID="txtHour" ValidChars="1234567890.">
        </cc1:FilteredTextBoxExtender>
        <br />
        <asp:HiddenField ID="hfIsUpdate" runat="server" />
        <asp:HiddenField ID="hfID" runat="server" />
    </div>
</asp:Content>
