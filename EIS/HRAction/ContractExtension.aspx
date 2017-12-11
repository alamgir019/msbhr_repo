<%@ Page  Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="ContractExtension.aspx.cs" 
Inherits="EIS_HRAction_ContractExtension"  Title="Contract Extension"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js">
    //Date Time Picker script
    </script>

    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js">
    </script>

    <script language="javascript" type="text/javascript">
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }
    </script>

    <div class="empTrainForm">
        <div id="formhead2">
            <div style="width: 97%; float: left;">
                Contract Extension</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="PayrollConfigInner">
            <div style="background-color: #EFF3FB; margin-bottom: 10px">
                <fieldset>
                    <table>
                        <tr>
                            <td class="textlevel">
                                Emp Id :</td>
                            <td>
                                <asp:TextBox ID="txtEmpID" runat="server" MaxLength="20" onkeyup="ToUpper(this)"
                                    Width="80px" ToolTip="Enter the Emp. Code and click on Find Button. Details of the find employee will be shown in employee general information screen if information exists. In order to enter employee."></asp:TextBox></td>
                            <td>
                                <asp:ImageButton ID="imgBtnSearch" runat="server" Height="19px" ImageUrl="~/Images/Search_Icon.jpg"
                                    OnClick="imgBtnSearch_Click" ToolTip="Enter the Emp. Code and click on Find Button. Details of the find employee will be shown in employee general information screen if information exists. In order to enter employee." /></td>
                            <td>
                                <asp:RequiredFieldValidator ID="ReqFieldVal" runat="server" ErrorMessage="*" ControlToValidate="txtEmpID"></asp:RequiredFieldValidator></td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td class="textlevel">
                                Name :</td>
                            <td>
                                <asp:Label ID="lblName" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Organization :</td>
                            <td>
                                <asp:Label ID="lblCompany" runat="server"></asp:Label></td>
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
                                Designation :</td>
                            <td>
                                <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Entry Date :</td>
                            <td>
                                <asp:TextBox ID="txtEntryDate" runat="server" ToolTip="Input the confirmation date."
                                    Width="80px"></asp:TextBox>
                                <a href="javascript:NewCal('<%= txtEntryDate.ClientID %>','ddmmyyyy')">
                                    <img style="border: 0px;" height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtEntryDate"
                                    CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                                </td>
                        </tr>                        
                    </table>
                </fieldset>
            </div>
            <fieldset>
        <legend>Contract Extension Details</legend>
        <table width="98%">
          <tr>
            <td  class="textlevel"> Name of Action :</td>
            <td><asp:DropDownList ID="ddlAction" runat="server" Width="300px" ToolTip="Select an Action from this list box. You have to select an Action for storing records."> </asp:DropDownList></td>
            <td></td>
          </tr>
          <tr>
            <td  class="textlevel"> Effective Date :</td>
            <td><asp:TextBox ID="txtEffDate" runat="server" Width="80px" ToolTip="Select a date by clicking on the calendar image or type a date in this order ‘dd/mm/yyyy’. You must put a valid dalid date for storing record." ></asp:TextBox>
              <a href="javascript:NewCal('<%= txtEffDate.ClientID %>','ddmmyyyy')">
                                <img style="border: 0px;" height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" 
                                runat="server" ControlToValidate="txtEffDate"
                                CssClass="validator" ErrorMessage="Invalid Date" 
                                ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator></td> 
            <td></td>
          </tr>
          <tr>
            <td class="textlevel"> Contract Expire on :</td>
            <td><asp:TextBox ID="txtContExpDate" runat="server" Width="80px" ToolTip="Select a date by clicking on the calendar image or type a date in this order ‘dd/mm/yyyy’. Put the date of contract expire date." ></asp:TextBox>
              <a href="javascript:NewCal('<%= txtContExpDate.ClientID %>','ddmmyyyy')">
                                <img style="border: 0px;" height="16" alt="Pick a date" src="../../images/cal.gif" width="16" /></a>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                                runat="server" ControlToValidate="txtContExpDate"
                                CssClass="validator" ErrorMessage="Invalid Date" 
                                ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator></td>
            <td ></td>
          </tr>
        </table>
        <asp:HiddenField ID="hfIsUpdate" runat="server" />
        <asp:HiddenField ID="hfContExtId" runat="server" />
      </fieldset>
      <fieldset>
        <legend>Contract Extension List</legend>
        <div style="OVERFLOW: scroll; WIDTH: 98%; HEIGHT: 250px">
          <asp:GridView id="grContExt" runat="server" Width="97%" Font-Size="9px" EmptyDataText="No Record Found" 
            AutoGenerateColumns="False" DataKeyNames="ContractExtId,ActionId" OnRowCommand="grContExt_RowCommand" ToolTip="Contract Extension List">
            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True"></SelectedRowStyle>
            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
            <Columns>
            <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
              <ItemStyle Width="5%" CssClass="ItemStylecss"/>
            </asp:ButtonField>
            <asp:BoundField DataField="ActionName" HeaderText="Action Name">
              <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="EffectiveDate" HeaderText="Effective Date">
              <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="ContractExtDate" HeaderText="Contract Extension Date">
              <ItemStyle CssClass="ItemStylecss" Width="45%"></ItemStyle>
            </asp:BoundField>
            </Columns>
          </asp:GridView>
        </DIV>
      </fieldset>
        </div>
        <div class="DivCommand1" style="width: 98%;">
            <div class="DivCommandL">
                <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" Text="Refresh"
                    Width="70px" OnClick="btnRefresh_Click" ToolTip="Click on Save Button to store the employee data." />
            </div>
            <div class="DivCommandR">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click"
                    ToolTip="Click on Save Button to store the employee data." />
            </div>
        </div>
    </div>
</asp:Content>

