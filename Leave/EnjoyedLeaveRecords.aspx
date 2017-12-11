<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="EnjoyedLeaveRecords.aspx.cs" Inherits="Leave_EnjoyedLeaveRecords" Title="Enjoyed Leave Records" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../Script/datetimepicker.js">
    </script>

    <script language="javascript" type="text/javascript">
    function ToUpper(ctrl)
    {   
    var t = ctrl.value;
    ctrl.value = t.toUpperCase(); 
    }
    function selectAllNone(grID, value) 
    {
          var tvNodes = document.getElementById(grID);
          var chBoxes = tvNodes.getElementsByTagName("input");
          for (var i = 0; i < chBoxes.length; i++) 
          {
              var chk = chBoxes[i];
              if (chk.type == "checkbox") 
              {
                    chk.checked = value;
                    //alert(tvNodes[i].href);
              }
           }  
    }
    
    </script>

    <div class="formStyle" style="height: 300px; width: 50%">
        <div id="formhead4">
            Enjoyed Leave Records</div>
        <div class="iesEmp" style="margin-top: 40px;">
            <fieldset>
                <table>
                    <tr>
                        <td class="textlevel">
                            Emp Id :</td>
                        <td colspan="5">
                            <asp:TextBox ID="txtEmpId" runat="server" Width="80px" onkeyup="ToUpper(this)"></asp:TextBox></td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Office :</td>
                        <td colspan="6">
                            <asp:DropDownList ID="ddlDivision" runat="server" Width="350px" CssClass="textlevelleft">
                            </asp:DropDownList></td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Team :</td>
                        <td colspan="6">
                            <asp:DropDownList ID="ddlDept" runat="server" Width="350px" CssClass="textlevelleft">
                            </asp:DropDownList></td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel" valign="middle">
                            Emp Status :</td>
                        <td colspan="6" style="border: solid 1px gray;">
                            <asp:RadioButtonList ID="rdbEmpStatus" runat="server" Font-Names="Tahoma" Font-Size="11px"
                                ForeColor="Blue" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="A">Active Employee</asp:ListItem>
                                <asp:ListItem Value="I">Inactive Employee</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td style="width: 3px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel" valign="middle">
                            Emp Type:</td>
                        <td colspan="6">
                            <asp:DropDownList ID="ddlEmpType" runat="server" Width="350px" CssClass="textlevelleft">
                            </asp:DropDownList></td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel" valign="top">
                            Leave Type :</td>
                        <td colspan="6" valign="top">
                            <asp:DropDownList ID="ddlLeaveType" runat="server" Width="350px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td valign="bottom">
                            <asp:Button ID="btnPriview" runat="server" Text="Print Priview" OnClick="btnPriview_Click" /></td>
                    </tr>
                    <tr>
                        <td class="textlevel" valign="middle">
                            Date From :</td>
                        <td colspan="8" valign="top">
                            <asp:TextBox ID="txtFrom" runat="server" Width="80px"></asp:TextBox>
                            <a href="javascript:NewCal('<%= txtFrom.ClientID %>','ddmmyyyy')">
                                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                    height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFrom"
                                ErrorMessage="*" Height="14px" Font-Bold="True" Font-Size="X-Large"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Width="60px"
                                CssClass="validator" ErrorMessage="Invalid" ControlToValidate="txtFrom" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                            <asp:Label ID="Label1" runat="server" Text="To :" Width="20px" Font-Names="tahoma"
                                Font-Size="11px" ForeColor="#09086E"></asp:Label>
                            <asp:TextBox ID="txtTo" runat="server" Width="80px"></asp:TextBox>
                            <a href="javascript:NewCal('<%= txtTo.ClientID %>','ddmmyyyy')">
                                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                    height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTo"
                                ErrorMessage="*" Height="14px" Font-Bold="True" Font-Size="X-Large"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Width="60px"
                                CssClass="validator" ErrorMessage="Invalid" ControlToValidate="txtTo" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>
</asp:Content>
