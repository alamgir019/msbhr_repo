<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="EmployeeReports.aspx.cs" Inherits="CrystalReports_Employee_EmployeeReports" %>

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
                                <asp:TreeNode Text="Employee List " Value="EL"></asp:TreeNode>
                                <%--<asp:TreeNode Text="Employee List for NGO Bureau " Value="ELNB"></asp:TreeNode>--%>
                                <asp:TreeNode Text="Group Wise Employee List " Value="GWEL"></asp:TreeNode>
                                <asp:TreeNode Text="Bank Account Information " Value="BAI"></asp:TreeNode>
                                <asp:TreeNode Text="Separation Staff List " Value="SSL"></asp:TreeNode>
                                <asp:TreeNode Text="Employee Emergency Contact List " Value="EECL"></asp:TreeNode>
                                <asp:TreeNode Text="Employee Experience Information " Value="EEI"></asp:TreeNode>
                                <asp:TreeNode Text="Employee Information " Value="EI"></asp:TreeNode>
                                <asp:TreeNode Text="Employee Witness Information " Value="EWI"></asp:TreeNode>
                                <asp:TreeNode Text="Employee List With Supervisor " Value="ELWS"></asp:TreeNode>
                                <asp:TreeNode Text="Service Length Of Separated Employee" Value="1"></asp:TreeNode>
                                <asp:TreeNode Text="Employee Service Length and Retirement as per Joining" Value="2"></asp:TreeNode>
                                <asp:TreeNode Text="Employee Service Length and Retirtement as per Date Of Birth" Value="3"></asp:TreeNode>
                                <asp:TreeNode Text="Employee Salary Information" Value="ESI"></asp:TreeNode>
                                <asp:TreeNode Text="Employee List With Address " Value="ELWA"></asp:TreeNode>
                                <asp:TreeNode Text="Employees Disciplinary Action " Value="EDA"></asp:TreeNode>
                                <asp:TreeNode Text="Confirmation List " Value="CL"></asp:TreeNode>
                                <asp:TreeNode Text="Employee Education Information(Details) " Value="EEID"></asp:TreeNode>
                                <asp:TreeNode Text="Employee Education Information(In Brief) " Value="EEIIB"></asp:TreeNode>                                
                                <asp:TreeNode Text="Employee Nominee Information" Value="ENI"></asp:TreeNode>
                                <asp:TreeNode Text="Employee Joining Info" Value="EJF"></asp:TreeNode>
                                <asp:TreeNode Text="Long Service Awardee Employee List" Value="LSAE"></asp:TreeNode>
                                <asp:TreeNode Text="Employee Resume" Value="ER"></asp:TreeNode>
                                <asp:TreeNode Text="Employee Promotion History Report" Value="EPHR"></asp:TreeNode>
                                <asp:TreeNode Text="Employee Transfer Report" Value="ETR"></asp:TreeNode>
                                <asp:TreeNode Text="Employee Change Status Report" Value="ECSR"></asp:TreeNode>
                                <asp:TreeNode Text="Employee Salary Change History Report" Value="ESCHR"></asp:TreeNode>
                                                             
                                <%--<asp:TreeNode Text="Increment Report" Value="IR"></asp:TreeNode>   --%>                           
                                <asp:TreeNode Text="User Reports" Value="UHR">
                                <asp:TreeNode Text="User In Out History Report" Value="UHR"></asp:TreeNode>
                                </asp:TreeNode>                                
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
                                                <td style="width: 290px">
                                                    <asp:Panel ID="PGroupWise" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label10" runat="server" Text="Report By :" Width="150px" CssClass="textlevel"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlGroupWise" runat="server" Width="230px" CssClass="textlevelleft"
                                                                            OnSelectedIndexChanged="ddlGroupWise_SelectedIndexChanged" AutoPostBack="true">
                                                                            <asp:ListItem Value="0">--- --- --- --- --- Select --- --- --- --- --- ---</asp:ListItem>
                                                                            <asp:ListItem Value="1">Grade Wise Employee List</asp:ListItem>
                                                                            <asp:ListItem Value="2">Clinic Wise Employee List</asp:ListItem>
                                                                            <asp:ListItem Value="3">Organization Wise Employee List</asp:ListItem>
                                                                            <asp:ListItem Value="4">Project Wise Employee List</asp:ListItem>
                                                                           <%-- <asp:ListItem Value="5">Salary Division Wise Employee List</asp:ListItem>
                                                                            <asp:ListItem Value="6">Salary Location Wise Employee List</asp:ListItem>
                                                                            <asp:ListItem Value="7">Posting Division Wise Employee List</asp:ListItem>
                                                                            <asp:ListItem Value="8">Posting District Wise Employee List</asp:ListItem>--%>
                                                                            <asp:ListItem Value="9">Designation Wise Employee List</asp:ListItem>
                                                                            <asp:ListItem Value="10">Religion Wise Employee List</asp:ListItem>
                                                                            <asp:ListItem Value="11">Department Wise Employee List</asp:ListItem>
                                                                           <%-- <asp:ListItem Value="12">Place Of Posting Wise Employee List</asp:ListItem>--%>
                                                                            <asp:ListItem Value="8">Tech. /Non Tech. Wise Employee List</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PEmpId" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                        BorderWidth="1px" Width="400px" Visible="False">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="LabeEmpId" runat="server" Text="Employee Id :" Width="150px" CssClass="textlevel"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtEmpCode" runat="server" Width="150px"></asp:TextBox>
                                                                    </td>
                                                                    
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PEmpName" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                        BorderWidth="1px" Width="400px" Visible="False">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="LabelEmpName" runat="server" Text="Employee Name :" Width="150px"
                                                                            CssClass="textlevel"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtEmpName" runat="server" Width="150px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PGrade" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label9" runat="server" Text="Grade:" Width="150px" CssClass="textlevel"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlGrade" runat="server" Width="150px" CssClass="textlevelleft">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PDesig" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label3" runat="server" Text="Designation :" Width="150px" CssClass="textlevel"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlDesignation" runat="server" Width="150px" CssClass="textlevelleft">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PSector" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label1" runat="server" Text="Clinic:" Width="150px" CssClass="textlevel"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlSector" runat="server" Width="150px"  CssClass="textlevelleft">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PProgDept" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label7" runat="server" Text="Department :" Width="150px" CssClass="textlevel"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlDepartment" runat="server" Width="150px" CssClass="textlevelleft">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PCompUnit" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label8" runat="server" Text="Organization :" Width="150px" CssClass="textlevel"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlComponentUnit" runat="server" Width="150px" CssClass="textlevelleft">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PPosByFunc" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label13" runat="server" Text="Project :" Width="150px"
                                                                            CssClass="textlevel"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlPosByFunc" runat="server" Width="150px" CssClass="textlevelleft">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PSalarySubLoc" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label11" runat="server" Text="Salary Division :" Width="150px" CssClass="textlevel"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlSalarySubLoc" runat="server" Width="150px" CssClass="textlevelleft">
                                                                        </asp:DropDownList>
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
                                                        Width="400px" Visible="False">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label4" runat="server" Text="Year :" Width="150px" CssClass="textlevel"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlYear" runat="server" Width="127px" CssClass="textlevelleft">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PMonthFrom" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                        BorderWidth="1px" Width="400px" Visible="False">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label21" runat="server" Text="Month From :" Width="150px" CssClass="textlevel"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlMonthFrm" runat="server" Width="127px" CssClass="textlevelleft">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PSalaryLoc" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="LabelSalaryLocation" runat="server" Text="Location Category:" Width="150px"
                                                                            CssClass="textlevel"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlSalaryLoc" runat="server" Width="150px" CssClass="textlevelleft">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PDivision" runat="server" BorderColor="DarkGray" BorderStyle="Solid"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label14" runat="server" CssClass="textlevel" Text="Division :" Width="150px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="textlevelleft" Width="150px"
                                                                            OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" AutoPostBack="true">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PDistrict" runat="server" BorderColor="DarkGray" BorderStyle="Solid"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="LabelPostingDistrict" runat="server" CssClass="textlevel" Text="District :"
                                                                            Width="150px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="textlevelleft" Width="150px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PhomeDist" runat="server" BorderColor="DarkGray" BorderStyle="Solid"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label6" runat="server" CssClass="textlevel" Text=" Home District:"
                                                                            Width="150px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlPerDistrict" runat="server" Width="155px" CssClass="textlevelleft">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PPlaceOfPosting" runat="server" BorderColor="DarkGray" BorderStyle="Solid"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label16" runat="server" CssClass="textlevel" Text="Tech. / Non Tech. Position"
                                                                            Width="150px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlTNTPosition" runat="server" CssClass="textlevelleft" 
                                                                            Width="150px">
                                                                            <asp:ListItem Value="1">All</asp:ListItem>
                                                                            <asp:ListItem Value="N">Non Technical</asp:ListItem>
                                                                            <asp:ListItem Value="D">Doctor</asp:ListItem>
                                                                            <asp:ListItem Value="P">Paremedic</asp:ListItem>
                                                                            <asp:ListItem Value="U">Nurse</asp:ListItem>
                                                                            <asp:ListItem Value="9">Nil</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PGender" runat="server" BorderColor="DarkGray" BorderStyle="Solid"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label17" runat="server" CssClass="textlevel" Text="Gender :" Width="150px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="textlevelleft" Width="150px">
                                                                            <asp:ListItem Value=" ">All</asp:ListItem>
                                                                            <asp:ListItem Value="M">Male</asp:ListItem>
                                                                            <asp:ListItem Value="F">Female</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PReligion" runat="server" BorderColor="DarkGray" BorderStyle="Solid"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label18" runat="server" CssClass="textlevel" Text="Religion :" Width="150px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlReligion" runat="server" CssClass="textlevelleft" Width="150px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PBank" runat="server" BorderColor="DarkGray" BorderStyle="Solid" BorderWidth="1px"
                                                        Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label19" runat="server" CssClass="textlevel" Text="Bank :" Width="150px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlBank" runat="server" CssClass="textlevelleft" Width="150px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PBloodGroup" runat="server" BorderColor="DarkGray" BorderStyle="Solid"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label20" runat="server" CssClass="textlevel" Text="Blood Group :"
                                                                            Width="150px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlBloodGroup" runat="server" CssClass="textlevelleft" Width="150px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PLearningArea" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label22" runat="server" Text="Learning Area :" Width="150px" CssClass="textlevel"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlLearningArea" runat="server" CssClass="textlevelleft" 
                                                                            Width="160px">
                                                                            <asp:ListItem Value="9">Nil</asp:ListItem>
                                                                            <asp:ListItem Value="N">Non Technical</asp:ListItem>
                                                                            <asp:ListItem Value="D">Doctor</asp:ListItem>
                                                                            <asp:ListItem Value="P">Paremedic</asp:ListItem>
                                                                            <asp:ListItem Value="U">Nurse</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PFiscalYr" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label23" runat="server" Text="Fiscal Year :" Width="150px" CssClass="textlevel"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlFiscalYr" runat="server" Width="150px" CssClass="textlevelleft">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PEmpType" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label24" runat="server" Text="Employee Type :" Width="150px" CssClass="textlevel"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlEmpType" runat="server" Width="150px" CssClass="textlevelleft">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PSeparationType" runat="server" BorderColor="DarkGray" BorderStyle="Solid"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label5" runat="server" CssClass="textlevel" Text="Separation Type:"
                                                                            Width="150px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlSeparationType" runat="server" CssClass="textlevelleft"
                                                                            Width="150px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>
                                                    <asp:Panel ID="PReasonOfAction" runat="server" BorderColor="DarkGray" BorderStyle="Solid"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label25" runat="server" CssClass="textlevel" Text="Reason of Action:"
                                                                            Width="150px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlReasonList" runat="server" CssClass="textlevelleft"
                                                                            Width="150px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>
                                                    <asp:Panel ID="PActionType" runat="server" BorderColor="DarkGray" BorderStyle="Solid"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label26" runat="server" CssClass="textlevel" Text="Action:"
                                                                            Width="150px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlAction" runat="server" CssClass="textlevelleft"
                                                                            Width="150px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PServiceLength" runat="server" BorderColor="DarkGray" BorderStyle="Solid"
                                                        BorderWidth="1px" Width="450px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 20px">
                                                                        <asp:Label ID="LabelServiceLngthFrom" runat="server" CssClass="textlevel" Text="Service Length From:"
                                                                            Width="150px"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 66px">
                                                                        <asp:TextBox ID="txtServiceLength" runat="server" MaxLength="10" Width="70px"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 70px">
                                                                    </td>
                                                                    <td colspan="1">
                                                                    </td>
                                                                    <td style="width: 5px">
                                                                        <asp:Label ID="LabelServiceLngthTo" runat="server" CssClass="textlevel" Text="To:" Width="30px"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 62px">
                                                                        <asp:TextBox ID="txtTo" runat="server" MaxLength="10" Width="70px"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 99px">
                                                                    </td>
                                                                    <td colspan="1">
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PRehireableChkBx" runat="server" BorderColor="DarkGray" BorderStyle="Solid"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td align="right">
                                                                        &nbsp;<asp:RadioButtonList ID="radBtnRehireable" runat="server" AutoPostBack="True" 
                                                                            Font-Names="Tahoma" Font-Size="11px" ForeColor="Blue" 
                                                                            RepeatDirection="Horizontal">
                                                                            <asp:ListItem Selected="True" Value="A">All</asp:ListItem>
                                                                            <asp:ListItem Value="N">Rehireable</asp:ListItem>
                                                                            <asp:ListItem Value="Y">Not Rehireable</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PActveInAcBasic" runat="server" BorderColor="DarkGray" BorderStyle="Solid"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButtonList ID="radBtnListEmp" runat="server" AutoPostBack="True" Font-Names="Tahoma"
                                                                            Font-Size="11px" ForeColor="Blue" OnSelectedIndexChanged="radBtnListEmp_SelectedIndexChanged"
                                                                            RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="M">All</asp:ListItem>
                                                                            <asp:ListItem Selected="True" Value="A">Active</asp:ListItem>
                                                                            <asp:ListItem Value="I">Inactive</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PradBtnBasic" runat="server" BorderColor="DarkGray" BorderStyle="Solid"
                                                        BorderWidth="1px" Width="400px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButtonList ID="radBtnBasic" runat="server" AutoPostBack="True" Font-Names="Tahoma"
                                                                            Font-Size="11px" ForeColor="Blue" OnSelectedIndexChanged="radBtnListEmp_SelectedIndexChanged"
                                                                            RepeatDirection="Horizontal">
                                                                            <asp:ListItem Selected="True" Value="B">With Basic</asp:ListItem>
                                                                            <asp:ListItem Value="W">Without Basic</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PDateRange" runat="server" BorderColor="DarkGray" BorderStyle="Solid"
                                                        BorderWidth="1px" Width="450px">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 20px">
                                                                        <asp:Label ID="LabelFromDate" runat="server" CssClass="textlevel" Text="From Date :"
                                                                            Width="80px"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 66px">
                                                                        <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="70px"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 70px">
                                                                        <a href="javascript:NewCal('<%= txtFromDate.ClientID %>','ddmmyyyy')">
                                                                            <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                                                                height="16" alt="Pick a date" src="../../images/cal.gif" width="16" />
                                                                        </a>
                                                                    </td>
                                                                    <td colspan="1">
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFromDate"
                                                                            CssClass="validator" ErrorMessage="Invalid" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"
                                                                            Width="40px"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                    <td style="width: 5px">
                                                                        <asp:Label ID="LabelToDate" runat="server" CssClass="textlevel" Text="To Date :"
                                                                            Width="50px"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 62px">
                                                                        <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="70px"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 99px">
                                                                        <a href="javascript:NewCal('<%= txtToDate.ClientID %>','ddmmyyyy')">
                                                                            <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                                                                height="16" alt="Pick a date" src="../../images/cal.gif" width="16" />
                                                                        </a>
                                                                    </td>
                                                                    <td colspan="1">
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtToDate"
                                                                            CssClass="validator" ErrorMessage="Invalid" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"
                                                                            Width="40px"></asp:RegularExpressionValidator>
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
                                <div style="float: left">
                                    <table style="width: 340px;">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="P_Emp" runat="server" BorderStyle="Solid" BorderColor="DarkGray" BorderWidth="1px"
                                                        Width="490px" Visible="False">
                                                        <table>
                                                            <tbody>
                                                                <asp:Panel ID="PChkBox" runat="server" BorderStyle="Solid" BorderColor="DarkGray"
                                                                    BorderWidth="1px" Width="580px" Visible="False">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:CheckBox runat="server" ID="chkSelectAll" Text="Checked All" AutoPostBack="True"
                                                                                OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                </asp:Panel>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <div style="border-right: black 1px solid; border-top: black 1px solid; margin: 15px 0px 0px;
                                                                            overflow: scroll; border-left: black 1px solid; width: 90%; border-bottom: black 1px solid;
                                                                            height: 250px; width: 480px">
                                                                            <asp:GridView ID="gvEmp" runat="server" Font-Size="9px" EmptyDataText="No Record Found"
                                                                                AutoGenerateColumns="False" DataKeyNames="EmpId" Width="460px">
                                                                                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                                                                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                                                                </SelectedRowStyle>
                                                                                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Select">
                                                                                        <ItemStyle CssClass="ItemStylecss" Width="5%" />
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkBoxEmp" runat="Server"></asp:CheckBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="EmpId" HeaderText="Employee ID">
                                                                                        <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="FullName" HeaderText="Employee Name">
                                                                                        <ItemStyle CssClass="ItemStylecss" Width="40%"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="DesigName" HeaderText="Designation">
                                                                                        <ItemStyle CssClass="ItemStylecss" Width="35%"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                            <!--Grid view Code Ends-->
                                                                        </div>
                                                                    </td>

                                                                    <td>
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
