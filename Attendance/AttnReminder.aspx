<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="AttnReminder.aspx.cs" Inherits="AttnReminder" Title="Attendance Reminder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript" src="../Script/datetimepicker.js">
</script>

<script language="javascript" type="text/javascript">
function Alertme()
{
    alert("Hi");
}
function CheckBoxListSelect(cbControl, state)
{   
       var chkBoxList = document.getElementById(cbControl);
       var chkBoxCount= chkBoxList.getElementsByTagName("input");
        for(var i=0;i<chkBoxCount.length;i++)
        {
            chkBoxCount[i].checked = state;
        }
        return false; 
}
</script>

 <div class="formStyle">
    <div id="formhead3">
        Attendance Reminder</div>
    <div class="MsgBox">
      <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
    </div>
     <div style="min-height:200px">
    <div style="width:45%;float:left; margin-left:10px;">
        <fieldset>
            <legend>Reminder Type</legend>
             <asp:RadioButton ID="rdbRmdType" runat="server" Text=" For Absent Record" />
        </fieldset>
        <fieldset>
            <legend>Attendance Record Date Range</legend>
            <table>
                <tr>
                    <td class="textlevel"  style="width:80px;">
                        From Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtFrom" runat="server" Width="80px"></asp:TextBox>
                    </td>
                    <td>
                        <a href="javascript:NewCal('<%= txtFrom.ClientID %>','ddmmyyyy')"><img style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px" height="16px" alt="Pick a date" src="../images/cal.gif" width="16px" /></a>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFrom"
                            ErrorMessage="*" Height="16px"></asp:RequiredFieldValidator>&nbsp;
                    </td>
                    <td class="textlevel"  style="width:80px;">
                        To Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTo" runat="server" Width="80px"></asp:TextBox>
                    </td>
                    <td>
                        <a href="javascript:NewCal('<%= txtTo.ClientID %>','ddmmyyyy')"><img style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px" height="16px" alt="Pick a date" src="../images/cal.gif" width="16px" /></a>
                    </td>
                    <td>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTo"
                            ErrorMessage="*" Height="16px"></asp:RequiredFieldValidator>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="textlevel" style="width: 80px">
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtFrom"
                        CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"
                        Width="60px"></asp:RegularExpressionValidator></td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td class="textlevel" style="width: 80px">
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtTo"
                        CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"
                        Width="60px"></asp:RegularExpressionValidator></td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div style="width:45%;margin-right:10px;">
      <fieldset style="width:100%;margin-left:20px;">
        <legend>Employee List</legend>
             <table style="width:100%">
               <tr>
                    <td align="right" style="width:5%;padding-left:40px">
                        <asp:Label ID="lblOffice" runat="server" Text="Location:"></asp:Label> 
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOffice" runat="server" Width="90%" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr >
                    <td colspan="2" style="padding-left:40px;">
                        Select <a id="A1" href="#"onclick="javascript: CheckBoxListSelect ('<%= grEmpList.ClientID %>',true)">All</a> | <a id="A2" href="#"onclick="javascript: CheckBoxListSelect ('<%= grEmpList.ClientID %>',false)">None</a>
                    </td>
                </tr>
                <tr >
                    <td colspan="2" style="padding-left:40px;">
                         <asp:GridView ID="grEmpList" runat="server" AutoGenerateColumns="False"
                                DataKeyNames="EmpId,FullName,OfficeEmail"
                                EmptyDataText="No Record Found" Font-Size="9px" PageSize="7" Width="95%" OnRowCommand="grEmpList_RowCommand">
                              <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" Font-Size="10px" HorizontalAlign="Left" />
                              <AlternatingRowStyle BackColor="#EFF3FB" />
                              <Columns>
                              <asp:TemplateField HeaderText="Select">
                                <ItemStyle CssClass="ItemStylecss" width="10%" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkBox" runat="Server" > </asp:CheckBox>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:BoundField DataField="FullName" HeaderText="Employee Name">
                                <ItemStyle CssClass="ItemStylecss" Width="50%" />
                              </asp:BoundField>
                               <asp:BoundField DataField="EmpId" HeaderText="Employee ID">
                                <ItemStyle CssClass="ItemStylecss" Width="30%" />
                              </asp:BoundField>
                              <asp:ButtonField HeaderText="View" Text="View" CommandName="ViewClick" CausesValidation="true">
                                <ItemStyle Width="10%" CssClass="ItemStylecss"></ItemStyle>
                              </asp:ButtonField>                            
                              </Columns>
                            </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding-left:40px;">
		                Select <a id="A3" href="#"onclick="javascript: CheckBoxListSelect ('<%= grEmpList.ClientID %>',true)">All</a> | <a id="A4" href="#"onclick="javascript: CheckBoxListSelect ('<%= grEmpList.ClientID %>',false)">None</a>
		            </td>
                </tr>
              </table>
        
                <div style="text-align:right;">
                    <asp:Button ID="btnSend" runat="server" Text="Send Reminder" OnClick="btnSend_Click" />
                </div>   
           </fieldset> 
         </div>
  </div>
  </div>
</asp:Content>
