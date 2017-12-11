<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeaveApplicationRpt.aspx.cs"
    Inherits="LeaveApplicationRpt" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Application</title>
    <style type="text/css">
#LvAppMain
{
	width:650px;
	min-height:800px;
	text-align:center;
	font-family:Verdana;
}
</style>
</head>
<body>
    <form id="form1" runat="server" style="text-align: center;">
        <div id="LvAppMain">
            <div>
                <asp:Repeater ID="rptLeavApp" runat="server">
                    <ItemTemplate>
                        <div style="text-align: left; float: left; margin-left: 10px; margin-top: 30px; font-size: 8px;
                            width: 80%">
                            <table width="620px" style="font-size: 12px">
                                <tr>
                                    <td>
                                        <strong style="font-size: 14px; font-weight: bold;">Marie Stopes Bangladesh </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong style="font-size: 12px; font-weight: bold;">Leave Application Form </strong>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="text-align: left; float: left; margin-left: 10px; margin-top: 30px; font-size: 8px;
                            width: 90%;">
                            <table>
                                <tr>
                                    <td>
                                        <strong style="font-size: 12px; font-weight: bold;">Applicant </strong>
                                    </td>
                                    <td>
                                        :</td>
                                    <td style="font-size: 12px; text-align: left;">
                                        <%#Convert.ToString(Eval("FullName"))%>
                                        ,
                                        <%#Convert.ToString(Eval("DesigName"))%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong style="font-size: 12px; font-weight: bold;">Office </strong>
                                    </td>
                                    <td>
                                        :</td>
                                    <td style="font-size: 12px; text-align: left;">
                                        <%#Convert.ToString(Eval("DivisionName"))%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong style="font-size: 12px; font-weight: bold;">Date of Application </strong>
                                    </td>
                                    <td>
                                        :</td>
                                    <td style="font-size: 12px; text-align: left;">
                                        <%# Common.DisplayDate(Convert.ToString(Eval("AppDate")))%>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table>
                                <tr>
                                    <td>
                                        <strong style="font-size: 12px; font-weight: bold;">Nature of Leave Applied for</strong>
                                    </td>
                                    <td>
                                        :</td>
                                    <td style="font-size: 12px; text-align: left;">
                                        <%#Convert.ToString(Eval("LTypeTitle"))%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong style="font-size: 12px; font-weight: bold;">Leave Applied</strong></td>
                                    <td>
                                        :</td>
                                    <td style="font-size: 12px; text-align: left;">
                                        From
                                        <%# Common.DisplayDate(Convert.ToString(Eval("LeaveStart")))  %>
                                        to
                                        <%# Common.DisplayDate(Convert.ToString(Eval("LeaveEnd")))%>
                                        .
                                    </td>
                                </tr>
                            </table>                          
                            <br />
                            <table>
                                <tr>
                                    <td>
                                        <strong style="font-size: 12px; font-weight: bold;">Purpose </strong>
                                    </td>
                                    <td>
                                        :</td>
                                    <td style="font-size: 12px; text-align: left;">
                                        <%#Convert.ToString(Eval("LTReason"))%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong style="font-size: 12px; font-weight: bold;">Leave Address & Contact No. </strong>
                                    </td>
                                    <td>
                                        :</td>
                                    <td style="font-size: 12px; text-align: left;">
                                        <%#Convert.ToString(Eval("AddrAtLeave"))%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td style="font-size: 12px; text-align: left;">
                                        <%#Convert.ToString(Eval("LvPhoneNo"))%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong style="font-size: 12px; font-weight: bold;">Resuming Date </strong>
                                    </td>
                                    <td>
                                        :</td>
                                    <td style="font-size: 12px; text-align: left;">
                                        <%# this.GetResumeDate(Convert.ToString(Eval("ResumeDate")))%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td style="font-size: 12px; text-align: left;">
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <table style="width: 110%">
                                <tr>
                                    <td style="text-decoration: overline; width: 70%" align="left">
                                        <table>
                                            <tr>
                                                <td valign="top">
                                                    <strong style="font-size: 12px; font-style: italic; font-weight: bold;">Approved By:
                                                    </strong>
                                                </td>
                                                <td style="text-decoration: none; font-size: 12px;" align="left">
                                                    <%# this.GetUpdatedByUserFullName(Convert.ToString(Eval("ApprovedBy")))%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <strong style="font-size: 12px; font-style: italic; font-weight: bold;">Approved On:
                                                    </strong>
                                                </td>
                                                <td style="text-decoration: none; font-size: 12px;" align="left">
                                                    <%# this.GetUpdatedDate(Convert.ToString(Eval("ApproveDate")))%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table style="width: 110%">
                                <tr>
                                    <td style="font-weight: bold; font-size: 12px; border-top: solid 1px DimGray;">
                                        Leave Balance Record as of
                                        <%# this.GetCurrentDate()%>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div style="text-align: center; margin-top: 10px; margin-left: 10px; font-size: 10px;
                width: 80%; float: left;">
                <table style="font-size: 12px">
                    <tr>
                        <td>
                            <asp:GridView ID="grLeaveStatus" runat="server" Font-Size="9px" Width="620px" PageSize="7"
                                EmptyDataText="No Record Found" DataKeyNames="EmpId,LTypeID,LTypeTitle,lvPrevYearCarry,LCarryOverd,LEntitled,LCashed,LeaveEnjoyed,LeaveElapsed,lvOpening"
                                AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="LTypeTitle" HeaderText="Leave Type" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="250px" CssClass="ItemStylecssLeft" HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LCarryOverd" HeaderText="Carry Over" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="120px" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LEntitled" HeaderText="Credit" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="120px" CssClass="ItemStylecssRight" HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LeaveEnjoyed" HeaderText="Availed" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="80px" CssClass="ItemStylecssRight" HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Balance" HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="80px" CssClass="ItemStylecssRight" HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle Font-Size="11px" Font-Bold="True"></HeaderStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div style="text-align: left; float: left; margin-left: 10px; margin-top: 30px; font-size: 8px;
                width: 90%;">
                <table style="width: 110%">
                    <tr>
                        <td style="font-weight: bold; font-size: 12px; border-top: solid 1px DimGray;">
                            <asp:Label ID="lblLeavePeriod" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
            </div>
            <div style="text-align: center; margin-top: 10px; margin-left: 10px; font-size: 10px;
                width: 80%; float: left;">
                <table style="font-size: 12px">
                    <tr>
                        <td>
                            <asp:GridView ID="grLeaveDtls" runat="server" Font-Size="9px" Width="620px" PageSize="7"
                                EmptyDataText="No Record Found" DataKeyNames="LvAppId,LTypeID" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="LTypeTitle" HeaderText="Leave Type" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="210px" CssClass="ItemStylecssLeft" HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AppDate" HeaderText="Applied On">
                                        <ItemStyle Width="80px" CssClass="ItemStylecssCenter" HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LeaveStart" HeaderText="Leave From">
                                        <ItemStyle Width="80px" CssClass="ItemStylecssCenter" HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LeaveEnd" HeaderText="Leave To">
                                        <ItemStyle Width="80px" CssClass="ItemStylecssCenter" HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LDurInDays" HeaderText="Days">
                                        <ItemStyle Width="50px" CssClass="ItemStylecssRight" HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AppStatus" HeaderText="Status">
                                        <ItemStyle Width="50px" CssClass="ItemStylecssCenter" HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ApprovedBy" HeaderText="Approved By">
                                        <ItemStyle Width="70px" CssClass="ItemStylecssCenter" HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ApproveDate" HeaderText="Approved On">
                                        <ItemStyle Width="80px" CssClass="ItemStylecssCenter" HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle Font-Size="11px" Font-Bold="True"></HeaderStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
