<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="Holiday.aspx.cs" Inherits="Attendance_Holiday" Title="Holiday Setup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js">
        //Date Time Picker script
    </script>
    <script language="javascript" type="text/javascript">
        function ValidateToDate() {
            var ddlDTo = document.getElementById('<%=ddlDayTo.ClientID%>');
            var ddlDFrom = document.getElementById('<%=ddlDayFrom.ClientID%>');
            var ddlMFrom = document.getElementById('<%=ddlMonthFrom.ClientID%>');
            var ddlMTo = document.getElementById('<%=ddlMonthTo.ClientID%>');
            var ddlY = document.getElementById('<%=ddlYear.ClientID%>');
            var myindexDT = ddlDTo.selectedIndex
            var SelValueDT = ddlDTo.options[myindexDT].value
            var myindexDF = ddlDFrom.selectedIndex
            var SelValueDF = ddlDFrom.options[myindexDF].value
            var myindexMF = ddlMFrom.selectedIndex
            var SelValueMF = ddlMFrom.options[myindexMF].value
            var myindexMT = ddlMTo.selectedIndex
            var SelValueMT = ddlMTo.options[myindexMT].value
            var myindexY = ddlY.selectedIndex
            var SelValueY = ddlY.options[myindexY].value

            if (SelValueMF == "-1") {
                alert("Please Select Month From!");
                return false;
            }

            if (SelValueDF == "-1") {
                alert("Please Select Day From!");
                return false;
            }

            if (SelValueMT == "-1") {
                alert("Please Select Month To!");
                return false;
            }
            var FromDate = SelValueY + "/" + SelValueMF + "/" + SelValueDF;
            var ToDate = SelValueY + "/" + SelValueMT + "/" + SelValueDT;


            if (Date.parse(FromDate) > Date.parse(ToDate)) {
                alert("Invalid Date Range!\nFrom Date Cannot Be After To Date!");
                return false;
            }
            return true;
        }

        function selectAllNone(grID, value) {
            var tvNodes = document.getElementById(grID);
            var chBoxes = tvNodes.getElementsByTagName("input");
            for (var i = 0; i < chBoxes.length; i++) {
                var chk = chBoxes[i];
                if (chk.type == "checkbox") {
                    chk.checked = value;
                }
            }
        }
    </script>
    <div id="holidayForm">
        <div id="formhead1">
            Holiday</div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="holidayFormInner">
            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                    <HeaderTemplate>
                        Holiday Setup
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div id="holidayTopForm">
                            <asp:HiddenField ID="hfIsUpdate" runat="server" />
                            <asp:HiddenField ID="hfHolidayId" runat="server" />
                            <table>
                                <tr>
                                    <td class="textlevel">
                                        Holiday Year :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlYear" runat="server" Width="80px" CssClass="textlevelleft">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="textlevel">
                                        Holiday Title :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHolidayTitle" runat="server" Width="195px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtHolidayTitle"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td class="textlevel">
                                        Date :
                                    </td>
                                    <td style="width: 3px">
                                        <asp:DropDownList ID="ddlMonthFrom" runat="server" Width="100px" OnSelectedIndexChanged="ddlMonthFrom_SelectedIndexChanged"
                                            AutoPostBack="True" CssClass="textlevelleft">
                                            <asp:ListItem Value="-1">--Month--</asp:ListItem>
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
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 3px">
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="*" ControlToValidate="ddlMonthFrom"
                                            ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
                                    </td>
                                    <td style="width: 3px">
                                        <asp:DropDownList ID="ddlDayFrom" runat="server" Width="80px" CssClass="textlevelleft">
                                            <asp:ListItem Value="-1">--Day--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 3px">
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="*" ControlToValidate="ddlDayTo"
                                            ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
                                    </td>
                                    <td style="width: 3px">
                                        <asp:Label ID="Label4" runat="server" Text="To :" CssClass="textlevel" Width="50px"></asp:Label>
                                    </td>
                                    <td style="width: 3px">
                                        <asp:DropDownList ID="ddlMonthTo" runat="server" Width="100px" OnSelectedIndexChanged="ddlMonthTo_SelectedIndexChanged"
                                            AutoPostBack="True" CssClass="textlevelleft">
                                            <asp:ListItem Value="-1">--Month--</asp:ListItem>
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
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 3px">
                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="*" ControlToValidate="ddlMonthTo"
                                            ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
                                    </td>
                                    <td style="width: 3px">
                                        <asp:DropDownList ID="ddlDayTo" runat="server" Width="80px" onchange="return ValidateToDate();"
                                            CssClass="textlevelleft">
                                            <asp:ListItem Value="-1">--Day--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 40px">
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlDayTo"
                                            ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator>
                                    </td>
                                    <td style="width: 3px">
                                    </td>
                                    <td style="width: 3px">
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td class="textlevel">
                                        Note :
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtHoliDesc" runat="server" Width="409px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkInActive" runat="server" Text="Make Inactive" CssClass="textlevel"
                                            Width="87px"></asp:CheckBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsFestival" runat="server" Text="Is Festival" CssClass="textlevel"
                                            Width="87px"></asp:CheckBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="text-align: left; float: left; margin-left: 5px;">
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px" CausesValidation="False"
                                OnClick="btnRefresh_Click" />
                        </div>
                        <div style="text-align: right; margin-right: 5px;">
                            <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" UseSubmitBehavior="False"
                                OnClick="btnSave_Click" />
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClick="btnDelete_Click" />
                            &nbsp;
                        </div>
                        <div id="holidayBottomForm">
                            <div id="holidayBottomFormInner1">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" CssClass="textlevel" Text="Holiday Year"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlShowYear" runat="server" Width="82px" CssClass="textlevelleft">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnShow" runat="server" Text="Show" Width="80px" CausesValidation="False"
                                                OnClick="btnShow_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="holidayBottomFormInner2">
                                <asp:GridView ID="grHoliday" runat="server" AutoGenerateColumns="False" DataKeyNames="HoliDayId,HolidayYear,HoliDesc,StartDate,IsFestival"
                                    EmptyDataText="No Record Found" Font-Size="9px" OnRowCommand="grHoliday_RowCommand"
                                    Width="100%">
                                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingRowStyle BackColor="#EFF3FB" />
                                    <Columns>
                                        <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="HolidayName" HeaderText="Holiday Title">
                                            <ItemStyle CssClass="ItemStylecss" Width="30%" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemStyle CssClass="ItemStylecss" Width="30%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text='<%# GetDate(Convert.ToString(Eval("StartDate")),Convert.ToInt32(Eval("Duration"))) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Duration" HeaderText="No of Days">
                                            <ItemStyle CssClass="ItemStylecssCenter" HorizontalAlign="Right" Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IsActive" HeaderText="Is Active">
                                            <ItemStyle CssClass="ItemStylecssCenter" Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IsActive" HeaderText="Is Festival">
                                            <ItemStyle CssClass="ItemStylecssCenter" Width="10%" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel1" Height="500px"
                    Width="590px">
                    <HeaderTemplate>
                        Multiple Holiday Setup
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div style="margin: 10px 5px 5px 5px; _margin: 5px 5px 5px 5px; height: 25px; width: 520px;">
                        </div>
                        <div>
                            <fieldset>
                                <table style="width: 380px">
                                    <tbody>
                                        <tr>
                                            <td style="width: 105px">
                                                <asp:Label ID="Label7" runat="server" Text="From Holiday Year" CssClass="textlevel"
                                                    Width="101px"></asp:Label>
                                            </td>
                                            <td style="width: 85px">
                                                <asp:DropDownList ID="ddlFromYear" runat="server" Width="100px" CssClass="textlevelleft">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnFromYear" OnClick="btnFromYear_Click" runat="server" Text="Show"
                                                    CausesValidation="False" Width="80px"></asp:Button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 105px">
                                                <asp:Label ID="Label8" runat="server" Text="To Holiday Year" CssClass="textlevel"
                                                    Width="101px"></asp:Label>
                                            </td>
                                            <td style="width: 85px">
                                                <asp:DropDownList ID="ddlToYear" runat="server" Width="100px" CssClass="textlevelleft">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnUpload" OnClick="btnUpload_Click" runat="server" Text="Change Year"
                                                    CausesValidation="False" Width="88px"></asp:Button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </fieldset>
                        </div>
                        <br />
                        Select <a id="A3" onclick="javascript: selectAllNone('<%= grMultiHoliday.ClientID %>',true)"
                            href="#">All</a> | <a id="A4" onclick="javascript: selectAllNone('<%= grMultiHoliday.ClientID %>',false)"
                                href="#">None</a>
                        <div style="border-right: gray 1px solid; border-top: gray 1px solid; margin: 10px 5px 5px 6px;
                            overflow: scroll; border-left: gray 1px solid; border-bottom: gray 1px solid;
                            height: 255px">
                            <asp:GridView ID="grMultiHoliday" runat="server" DataKeyNames="HoliDayId,HolidayYear"
                                AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" Width="99%">
                                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemStyle CssClass="ItemStylecss" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkBox" runat="Server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="HolidayName" HeaderText="Holiday Title">
                                        <ItemStyle CssClass="ItemStylecss" Width="360px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="StartDate" HeaderText="Start Date">
                                        <ItemStyle CssClass="ItemStylecss" Width="120px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EndDate" HeaderText="End Date">
                                        <ItemStyle CssClass="ItemStylecss" Width="120px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Duration" HeaderText="No of Days">
                                        <ItemStyle CssClass="ItemStylecssRight" HorizontalAlign="Right" Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="IsActive" HeaderText="Is Active">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="80px" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="ItemStylecss" Width="0px" />
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfHoliDesc" runat="server" Value='<%# Convert.ToString(Eval("HoliDesc")) %>' />
                                            <asp:HiddenField ID="hfStartDate" runat="server" Value='<%# Convert.ToString(Eval("StartDate")) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div style="margin: 0px 5px 5px; height: 20px">
                            <div style="float: left; text-align: left">
                                <asp:Button ID="btnMultiRefresh" OnClick="btnRefresh_Click" runat="server" Text="Refresh"
                                    CausesValidation="False" Width="70px"></asp:Button>
                            </div>
                            <div style="text-align: right">
                                <asp:Button ID="btnMultiSave" OnClick="btnMultiSave_Click" runat="server" Text="Save"
                                    CausesValidation="False" UseSubmitBehavior="False" Width="70px"></asp:Button>
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>               
            </cc1:TabContainer>
        </div>
    </div>
</asp:Content>
