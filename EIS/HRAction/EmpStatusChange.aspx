<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="EmpStatusChange.aspx.cs" Inherits="EIS_EmpStatusChange" 
    Title="Emp Status Change" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
 <script src="../../JScripts/jquery-1.2.3.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../JScripts/ui.datepicker.js">
    </script>
    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js">
        //Date Time Picker script
    </script>
  <script language ="javascript" type="text/javascript" src ="../JScripts/Confirmation.js">
    //Delete Confirmation Message
</script>
  <script language="javascript" type="text/javascript">
    function ToUpper(ctrl)
    {   
    var t = ctrl.value;
    ctrl.value = t.toUpperCase(); 
    }
</script>

  <div class="empTrainForm">
    <div id="formhead1">
      <div style="width:94%;float:left;">Employee Status Change</div>
      <div style=" margin:2px;float:left; "><a href="../Help/EIS/ReHired.htm" target="_blank"><img src="../Images/help.png" /></a></div>
      <div style=" margin:2px; float:left; height: 27px;"><a href="../home.aspx"><img src="../Images/close_icon.gif" /></a></div>
    </div>
    <div class="MsgBox">
      <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
    </div>
    <div id="PayrollConfigInner">
      <div style="background-color:#EFF3FB; margin-bottom:10px;">
        <fieldset>
          <legend>Employee Status Change</legend>
          <table>
            <tr>
              <td class="textlevel" >Emp Code :</td>
              <td><asp:TextBox ID="txtEmpID" runat="server" MaxLength="20" onkeyup="ToUpper(this)" Width="80px" ToolTip="Type an Emp code and press ‘Enter’ or click on Find Image." ></asp:TextBox></td>
              <td><asp:ImageButton ID="imgBtnSearch" runat="server" Height="19px" ImageUrl="~/Images/Search_Icon.jpg" OnClick="imgBtnSearch_Click" CausesValidation="False" ToolTip="Find Image"/></td>
              <td><asp:RequiredFieldValidator ID="ReqFieldVal"  runat="server" ErrorMessage="*" ControlToValidate="txtEmpID"></asp:RequiredFieldValidator></td>
            </tr>
            </table>
            <table>
                        <tr>
                            <td class="textlevel">
                                Name :
                            </td>
                            <td>
                                <asp:Label ID="lblName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Organization :
                            </td>
                            <td>
                                <asp:Label ID="lblOffice_Loc" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Designation :
                            </td>
                            <td>
                                <asp:Label ID="lblDeg_Project" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel" style="height: 16px">
                                Department :</td>
                            <td style="height: 16px">
                                <asp:Label ID="lblDept" runat="server"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td class="textlevel" style="height: 16px">
                                Sub Department :</td>
                            <td style="height: 16px">
                                <asp:Label ID="lblSubDept" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel" style="height: 16px">
                                Subcode :</td>
                            <td style="height: 16px">
                                <asp:Label ID="lblSuncode" runat="server"></asp:Label>
                            </td>
                        </tr>  
                        <tr>
                            <td class="textlevel">
                                Joining Date :
                            </td>
                            <td>
                                <asp:Label ID="lblJoiningDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                <tr>
                            <td class="textlevel">
                                Separate Date :
                            </td>
                            <td>
                                <asp:Label ID="lblSeparateDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
        </fieldset>
      </div>
      <fieldset style="margin-bottom:10px;">
        <legend>Employee Status Change Details</legend>
        <table width="98%">
            <tr>
            <td class="textlevel"> Effective Date :</td>
            <td><asp:TextBox ID="txtEffDate" runat="server" Width="25%" ToolTip="Select a date by clicking on the calendar image or type a date in this order ‘dd/mm/yyyy’. You must put a valid dalid date for storing record." ></asp:TextBox>
              <A href="javascript:NewCal('<%= txtEffDate.ClientID %>','ddmmyyyy')"><IMG style=" border:0px;" height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></A></td>
            <td ><asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtEffDate"
                        CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator1"  runat="server" ErrorMessage="*" ControlToValidate="txtEffDate"></asp:RequiredFieldValidator></td>
          </tr>
        </table>
        <asp:HiddenField ID="hfIsUpdate" runat="server" />
        <asp:HiddenField ID="hfReHiredId" runat="server" />
          &nbsp;
      </fieldset>
      <fieldset>
        <legend>Employee Status Change List</legend>
        <DIV style="OVERFLOW: scroll; WIDTH: 98%; HEIGHT: 50px">
          <asp:GridView id="grReHired" runat="server" Width="97%" Font-Size="9px" EmptyDataText="No Record Found" 
            AutoGenerateColumns="False" DataKeyNames="ReHiredId,ActionId" OnRowCommand="grReHired_RowCommand" ToolTip="Re-Hired List">
            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True"></SelectedRowStyle>
            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
            <Columns>           
            <asp:BoundField DataField="EffectiveDate" HeaderText="Effective Date">
              <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
            </asp:BoundField>
            </Columns>
          </asp:GridView>
        </DIV>
      </fieldset>
    </div>
    <div class="DivCommand1">
      <div class="DivCommandL">
        <asp:Button ID="btnRefresh" runat="server" CausesValidation="False"
            Text="Refresh" Width="70px" OnClick="btnRefresh_Click" ToolTip="To set the page as initial stage or set fields blank, click on Refresh button." />
      </div>
      <div class="DivCommandR">
        <asp:Button ID="btnSave" runat="server" Text="Make Active" UseSubmitBehavior="True" OnClientClick="javascript:return HistorySaveConfirmation();"
            Width="99px" OnClick="btnSave_Click" ToolTip="Click on this button to store/update above informations" />
      </div>
    </div>
  </div>
</asp:Content>
