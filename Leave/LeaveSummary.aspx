<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="LeaveSummary.aspx.cs" Inherits="Leave_LeaveSummary" Title="Leave Summary" %>

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

    <div class="formStyle" style="height: 450px; width: 50%">
        <div id="formhead4">
            Leave Summary</div>
        <div class="iesEmp" style="margin-top: 40px;">
            <fieldset>
                <table>
                    <tr>
                        <td class="textlevel">
                            Emp Id:</td>
                        <td colspan="5">
                            <asp:TextBox ID="txtEmpId" runat="server" onkeyup="ToUpper(this)"></asp:TextBox></td>
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
                        <td style="width: 3px">
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
                        <td style="width: 3px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel" valign="middle">
                            Employee Status :</td>
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
                        <td class="textlevel" valign="top">
                            Select Leave Type :</td>
                        <td colspan="6" valign="top" style="border: solid 1px gray;">
                            <asp:CheckBoxList ID="chkLeaveTypeList" runat="server" Font-Size="11px" ForeColor="Blue"
                                Font-Names="tahoma">
                            </asp:CheckBoxList></td>
                        <td style="width: 3px">
                        </td>
                        <td valign="bottom">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="3">
                            <a id="A1" onclick="javascript: selectAllNone('<%= this.chkLeaveTypeList.ClientID %>',true)"
                                href="#">Select All</a>
                        </td>
                        <td colspan="3">
                            <a id="A2" onclick="javascript: selectAllNone('<%= this.chkLeaveTypeList.ClientID %>',false)"
                                href="#">Deselect All</a>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnPriview" runat="server" Text="Print Priview" OnClick="btnPriview_Click" /></td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>
</asp:Content>
