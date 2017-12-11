<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="SummaryAttendance.aspx.cs" Inherits="Attendance_SummaryAttendance"
    Title="Summary Attendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../Script/datetimepicker.js">
    </script>

    <script language="javascript" type="text/javascript">
    function ToUpper(ctrl)
    {   
    var t = ctrl.value;
    ctrl.value = t.toUpperCase(); 
    }
    
    function LocationChanged()
    {
        
        var ddlLoc = document.getElementById('<%= ddlDivision.ClientID %>');
        var ddlDiv=document.getElementById('<%=ddlOffice.ClientID%>');
        var myindexloc  = ddlLoc.selectedIndex;
        var SelValueLoc = ddlLoc.options[myindexloc].value;
        if(SelValueLoc !="99999")
        {
           ddlDiv.selectedIndex=0;
        }
    }
    
    function OfficeChanged()
    {
        var ddlLoc = document.getElementById('<%= ddlDivision.ClientID %>');
        var ddlDiv=document.getElementById('<%=ddlOffice.ClientID%>');
        var myindexdiv  = ddlDiv.selectedIndex;
        var SelValueDiv = ddlDiv.options[myindexdiv].value;
        if(SelValueDiv !="99999")
        {
           ddlLoc.selectedIndex=0;
        }
    }

    </script>

    <div class="formStyle" style="height: 220px; width: 50%">
        <div id="formhead4">
            Summary Attendance</div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div class="iesEmp" style="margin-top: 0px;">
            <table>
                <tr>
                    <td class="textlevelshort" style="width: 80px">
                        Emp Id :</td>
                    <td colspan="5">
                        <asp:TextBox ID="txtEmpId" Width="80px" runat="server" onkeyup="ToUpper(this)"></asp:TextBox></td>
                    <td style="width: 4px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="textlevelshort" style="width: 80px">
                        Location :</td>
                    <td colspan="6">
                        <asp:DropDownList ID="ddlDivision" CssClass="textlevel" runat="server" Width="350px"
                            onchange="LocationChanged()">
                        </asp:DropDownList></td>
                    <td colspan="1">
                    </td>
                </tr>
                <tr>
                    <td class="textlevelshort" style="width: 80px">
                        Office :</td>
                    <td colspan="6">
                        <asp:DropDownList ID="ddlOffice" CssClass="textlevel" runat="server" Width="350px"
                            onchange="OfficeChanged()">
                        </asp:DropDownList></td>
                    <td colspan="1">
                    </td>
                </tr>
                <tr>
                    <td class="textlevelshort" style="width: 80px">
                        Team :</td>
                    <td colspan="6">
                        <asp:DropDownList ID="ddlDept" CssClass="textlevel" runat="server" Width="350px">
                        </asp:DropDownList></td>
                    <td colspan="1">
                    </td>
                </tr>
                <tr>
                    <td class="textlevelshort">
                        From :
                    </td>
                    <td>
                        <asp:TextBox ID="txtFrom" runat="server" Width="100px"></asp:TextBox>
                        <a href="javascript:NewCal('<%= txtFrom.ClientID %>','ddmmyyyy')">
                            <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                height="16px" alt="Pick a date" src="../images/cal.gif" width="16px" /></a>
                    </td>
                    <td style="width: 21px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFrom"
                            ErrorMessage="*" Height="16px"></asp:RequiredFieldValidator>
                    </td>
                    <td class="textlevel" style="width: 20px">
                        To :
                    </td>
                    <td>
                        <asp:TextBox ID="txtTo" runat="server" Width="100px"></asp:TextBox>
                        <a href="javascript:NewCal('<%= txtTo.ClientID %>','ddmmyyyy')">
                            <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                height="16px" alt="Pick a date" src="../images/cal.gif" width="16px" /></a>
                    </td>
                    <td style="width: 3px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTo"
                            ErrorMessage="*" Height="16px"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 4px">
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="textlevelshort">
                    </td>
                    <td colspan="5">
                        <asp:RadioButtonList ID="rdbtnEmpStatus" runat="server" CssClass="textlevelauto"
                            RepeatDirection="Horizontal" RepeatLayout="Flow" Width="219px">
                            <asp:ListItem Selected="True" Value="A">Active Employee</asp:ListItem>
                            <asp:ListItem Value="I">Inactive Employee</asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td style="width: 4px">
                    </td>
                    <td>
                        <asp:Button ID="btnPriview" runat="server" Text="Print Priview" OnClick="btnPriview_Click" /></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
