<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="AttendanceImporter.aspx.cs" 
Inherits="Attendance_AttendanceImporter" Title="Attendance Importer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <script language="javascript" type="text/javascript" src="../Script/datetimepicker.js">
    //Date Time Picker script
</script>
  <script language="javascript" type="text/javascript">
    function ToUpper(ctrl)
    {   
    var t = ctrl.value;
    ctrl.value = t.toUpperCase(); 
    }
    </script>
    
    <div class="formStyle" style="height:150px;">
    <div id="formhead3">Attendance Importer</div>
    <div class="MsgBox" style="margin-bottom:20px;">
        <table style="width:100%">
        <tr>
            <td style="width:50%;text-align:left;">
                <asp:Label ID="lblLog" runat="server" CssClass="msglabel"></asp:Label>   
            </td>
            <td style="width:40%;text-align:right;">
                <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
            </td>
        </tr>
      </table>
    </div>
  <div class="iesEmp">
        
        <table>
            <tr>
                <td class="textlevelshort">
                    From Date:
                </td>
                <td>
                    <asp:TextBox ID="txtFrom" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td style="width: 3px">
                    <A href="javascript:NewCal('<%= txtFrom.ClientID %>','ddmmyyyy')"><IMG style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px" height=16 alt="Pick a date" src="../images/cal.gif" width=16 /></A>
                </td>
                <td class="textlevelshort">
                    To Date:
                </td>
                <td>
                 <asp:TextBox ID="txtTo" runat="server" Width="100px"></asp:TextBox>
                </td>
                    
                <td>
                    <A href="javascript:NewCal('<%= txtTo.ClientID %>','ddmmyyyy')"><IMG style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px" height=16 alt="Pick a date" src="../images/cal.gif" width=16 /></A>
                </td>
                <td class="textlevelshort">
                    Location:
                </td>
                <td>
                    <asp:DropDownList ID="ddlDivision" runat="server" Width="220px">
                    </asp:DropDownList>
                    
                </td>

                <td>
                    <asp:Button ID="btnImport" runat="server" Text="Import Data" OnClick="btnImport_Click" />
                </td>
            </tr>
            <tr>
                <td class="textlevelshort">
                </td>
                <td>
                </td>
                <td style="width: 3px">
                </td>
                <td class="textlevelshort">
                </td>
                <td>
                </td>
                <td>
                </td>
                <td class="textlevelshort">
                </td>
                <td>
                    <asp:RadioButtonList ID="rdbtnEmpStatus" runat="server" CssClass="textlevelauto"
                        Width="219px">
                        <asp:ListItem Selected="True" Value="A">Active Employee</asp:ListItem>
                        <asp:ListItem Value="I">Inactive Employee</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td>
                </td>
            </tr>
        </table>
      </div>
      </div>
</asp:Content>

