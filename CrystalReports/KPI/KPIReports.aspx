<%@ Page  Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="KPIReports.aspx.cs" Inherits="CrystalReports_KPI_KPIReports" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js">
        //Date Time Picker script
    </script>
    <script language="javascript" type="text/javascript">
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }
    </script>
    <div class="formStyle">
        <div id='formhead6'>
            <div style="width: 98%; float: left;">
                Report List</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" alt="Close" /></a></div>
        </div>
        <div class="repotForm2">
            <fieldset>
                <div id="reportList">
                    <asp:TreeView ID="tvReports" runat="server" ImageSet="Arrows" OnSelectedNodeChanged="tvReports_SelectedNodeChanged">
                        <Nodes>
                            <asp:TreeNode Text="Report List" Value="RL">
                                <asp:TreeNode Text="KPI Review" Value="KPI"></asp:TreeNode>
                            </asp:TreeNode>
                        </Nodes>
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                        <SelectedNodeStyle BackColor="#E0E0E0" Font-Underline="True" ForeColor="#5555DD"
                            HorizontalPadding="0px" VerticalPadding="0px" />
                        <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="5px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                    </asp:TreeView>
                </div>
                <div id="reportListFild">
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div style="width: 475px">
                                <div style="float: left; width: 100%">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PEmp" runat="server" BorderStyle="Solid" BorderColor="DarkGray" BorderWidth="1px"
                                                        Width="400px" Visible="False">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label2" runat="server" Text="Employee Id :" Width="150px" CssClass="textlevel"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                       <asp:TextBox ID="txtEmpID" runat="server" Width="80px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PYear" runat="server" BorderStyle="Solid" BorderColor="DarkGray" BorderWidth="1px"
                                                        Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label12" runat="server" Text="Year :" Width="150px" CssClass="textlevel"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                       <asp:TextBox ID="txtYear" runat="server" Width="80px" ></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                           
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PQuarter" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                        BorderWidth="1px" Width="400px" >
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label10" runat="server" Text="Quarter :" Width="150px" CssClass="textlevel"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlQuarter" runat="server" Width="200px" CssClass="textlevelleft"></asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                   </asp:Panel>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PGroup" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label1" runat="server" Text="Group :" Width="150px" CssClass="textlevel"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlGroup" runat="server" CssClass="textlevelleft" ToolTip="Select Group." Width="200px"></asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                          
                                          
                                        </tbody>
                                    </table>
                                </div>
                                <div style="float: left; width: 50%">
                                    <div style="padding-right: 15px; float: right;">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td style="width: 30px">
                                                        <asp:Panel ID="PShow" runat="server" Visible="False">
                                                            <asp:Button ID="btnShow" OnClick="btnShow_Click" runat="server" Text="Show Report"
                                                                Font-Underline="False"></asp:Button>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>


