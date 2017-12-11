<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnjoyedLeaveRecordsRpt.aspx.cs"
    Inherits="Leave_EnjoyedLeaveRecordsRpt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Summary</title>
    <link href="../Css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body {
	            background:#fff;
	            font-size:10px;
	            font-family:Tahoma;
	            padding:0px;
	            margin-left:10px;
	            
            }
    </style>
</head>
<body> 
    <form id="form1" runat="server">
        <div style="font-family: Arial; font-size: 20px;">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            Marie Stopes Bangladesh
        </div>
        <div style="font-family: Arial; font-size: 16px;">
            <asp:Label ID="lblDivision" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblDept" runat="server" Text=""></asp:Label>
            <br />
            Enjoyed Leave Records
            <asp:Label ID="lblSubTitle" runat="server" Text=""></asp:Label>
        </div>
        <div style="width: 95%; float: left; margin-bottom: 5px; border-bottom: solid 1px #FF9933">
            <table>
                <tr>
                    <td>
                        <span style="font-family: Tahoma; font-size: 14px;">Date From :</span>
                    </td>
                    <td>
                        <asp:Label ID="lblFrom" runat="server" Text="" Font-Names="Tahoma" Font-Size="14px"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <asp:GridView ID="grLeaveMaster" runat="server" AutoGenerateColumns="False" DataKeyNames=""
                        EmptyDataText="No Record Found" Font-Size="14px" Width="95.4%" ShowFooter="true"
                        OnRowCommand="grLeaveMaster_RowCommand">
                        <HeaderStyle BackColor="#3366CC" ForeColor="White" />
                        <FooterStyle BackColor="#3366CC" ForeColor="White" Font-Bold="true" />
                        <RowStyle Font-Size="14px" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray" />
                        <Columns>
                            <asp:BoundField HeaderText="SL No." ItemStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="left">
                                <ItemStyle Width="5%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EmpId" HeaderText="Employee ID" ItemStyle-VerticalAlign="Top"
                                HeaderStyle-HorizontalAlign="left">
                                <ItemStyle Width="10%" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FullName" HeaderText="Employee Name" ItemStyle-VerticalAlign="Top"
                                HeaderStyle-HorizontalAlign="left">
                                <ItemStyle Width="20%" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PostingDivName" HeaderText="Office" ItemStyle-VerticalAlign="Top"
                                HeaderStyle-HorizontalAlign="left">
                                <ItemStyle Width="20%" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="DeptName" HeaderText="Team" ItemStyle-VerticalAlign="Top"
                                HeaderStyle-HorizontalAlign="left">
                                <ItemStyle Width="20%" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="" HeaderText="Carry Over" ItemStyle-VerticalAlign="Top"
                                HeaderStyle-HorizontalAlign="center">
                                <ItemStyle Width="10%" HorizontalAlign="center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="LEAVECOUNT" HeaderText="" ItemStyle-VerticalAlign="Top"
                                HeaderStyle-HorizontalAlign="center">
                                <ItemStyle Width="10%" HorizontalAlign="center"></ItemStyle>
                            </asp:BoundField>
                            <asp:ButtonField CommandName="DoubleClick" HeaderText="" Text="Days" HeaderStyle-HorizontalAlign="center">
                                <ItemStyle Width="5%" HorizontalAlign="Center" Font-Size="11px" />
                            </asp:ButtonField>
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Panel Style="display: none" ID="PnlEmpSearch" runat="server" CssClass="modalPopup"
                    Height="350px" Width="600px">
                    <div style="background-image: url(../Images/orengeBG.jpg); color: white; background-repeat: repeat-x;
                        height: 20px; font-size: 13px; font-weight: bold;">
                        Leave Records</div>
                    <div style="width: 98%; margin-top: 10px; background-color: #EFF3FB; text-align: center;">
                        <fieldset>
                            <table width="100%">
                                <tr>
                                    <td class="textlevel">
                                        Employee ID :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblEmpID" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td class="textlevel">
                                        Employee Name :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblEmpName" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="textlevel">
                                        Leave Title :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblLeaveTitle" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td class="textlevel">
                                        Records From :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFromTo" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                    <!--Grid view Code starts-->
                    <div style="overflow: scroll; width: 98%; height: 250px; text-align: center;">
                        <asp:GridView ID="grLeaveDetls" runat="server" Width="100%" AutoGenerateColumns="False"
                            EmptyDataText="No Record Found" Font-Size="9px">
                            <Columns>
                                <asp:BoundField DataField="LevDate" HeaderText="Leave Date">
                                    <ItemStyle Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AppDate" HeaderText="Applied on">
                                    <ItemStyle Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LTReason" HeaderText="Reason">
                                    <ItemStyle Width="20%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AddrAtLeave" HeaderText="Address">
                                    <ItemStyle Width="20%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ApprovedBy" HeaderText="Approver">
                                    <ItemStyle Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ApproveDate" HeaderText="Approved on">
                                    <ItemStyle Width="15%" />
                                </asp:BoundField>
                            </Columns>
                            <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#3366CC" Font-Bold="True" HorizontalAlign="Left" />
                            <AlternatingRowStyle BackColor="#EFF3FB" />
                        </asp:GridView>
                    </div>
                    <div class="DivCommand1">
                        <div class="DivCommandR">
                            <div style="margin-top: 5px; vertical-align: middle; height: 20px; background-color: gray;
                                text-align: right">
                                <asp:ImageButton ID="imgbtnClose" runat="server" Height="20px" ImageAlign="AbsMiddle"
                                    ImageUrl="~/Images/Close3.jpg"></asp:ImageButton>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Button ID="Button1" runat="server" Text="." Enabled="false" />
                <cc1:ModalPopupExtender ID="ModalPopupTree" runat="server" BackgroundCssClass="modalBackground"
                    CancelControlID="imgbtnClose" DropShadow="True" DynamicServicePath="" Enabled="True"
                    PopupControlID="PnlEmpSearch" TargetControlID="Button1">
                </cc1:ModalPopupExtender>
                <asp:HiddenField ID="hfFrom" runat="server" />
                <asp:HiddenField ID="hfTo" runat="server" />
                <asp:HiddenField ID="hfLType" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
