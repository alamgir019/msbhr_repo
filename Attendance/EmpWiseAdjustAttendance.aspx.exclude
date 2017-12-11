<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="EmpWiseAdjustAttendance.aspx.cs" Inherits="Attendance_EmpWiseAdjustAttendance" Title="Attendance Adjust" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js"></script>
<div id="PayrollConfigForm2">
    <div id="formhead3">Attendance Entry</div>
    <div class="MsgBox">
         <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
    </div>
     <div id="PayrollConfigInner2">
        <fieldset style="background-color:#EFF3FB;margin-left:10px;margin-right:10px;">
            <table>
                    <tr>
                        <td>
                           <asp:Label id="Label2" runat="server" Text="Employee ID: " CssClass="textlevel"></asp:Label>
                        </td>
                        <td>
                             <asp:Label id="lblEmpID" runat="server" Width="600px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           <asp:Label id="Label1" runat="server" Text="Employee Name: " CssClass="textlevel"></asp:Label>
                        </td>
                        <td>
                             <asp:Label id="lblName" runat="server" Width="600px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label15" runat="server" CssClass="textlevel" Text="Supervisor :"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblSupervisor" runat="server" Width="600px"></asp:Label>
                        </td>
                    </tr>
                </table>
        </fieldset>
         <asp:HiddenField ID="hfSupervisor" runat="server" />
         <asp:HiddenField ID="hfSupervisorEmail" runat="server" />
         <fieldset style="margin-left:10px;margin-right:10px;">
         <legend>Enter Date Range</legend>
            <table>
                <tr>
                    <td>Date From</td>
                    <td>Date To</td>
                    <td></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="txtDateFrom" runat="server" Width="100px"></asp:TextBox>
                        <A href="javascript:NewCal('<%= txtDateFrom.ClientID %>','ddmmyyyy')"><IMG style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px" height=16 alt="Pick a date" src="../images/cal.gif" width=16 /></A>
                    </td>
                    <td><asp:TextBox ID="txtDateTo" runat="server" Width="100px"></asp:TextBox>
                        <A href="javascript:NewCal('<%= txtDateTo.ClientID %>','ddmmyyyy')"><IMG style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px" height=16 alt="Pick a date" src="../images/cal.gif" width=16 /></A>
                    </td>
                    <td><asp:Button ID="btnGetAbsentRecord" runat="server" Text="Get Absent Record" OnClick="btnGetAbsentRecord_Click" /></td>
                </tr>
            </table>
         </fieldset>
         <fieldset style="margin-left:10px;margin-right:10px;">
            <legend>Days of Absence</legend>
            <asp:GridView ID="grDateList" runat="server" AutoGenerateColumns="False" DataKeyNames=""
                                    EmptyDataText="No Record Found" Font-Size="9px" PageSize="7" Width="98%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkBox" runat="Server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="AttndDate" HeaderText="Date">
                                            <ItemStyle CssClass="ItemStylecss" Width="20%" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Write remarks for your supervisor">
                                            <ItemStyle CssClass="ItemStylecss" Width="70%" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRemark" runat="Server" Width="98%" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#3366CC" Font-Bold="True" Font-Size="10px" HorizontalAlign="Left" ForeColor="WhiteSmoke" />
                                    <AlternatingRowStyle BackColor="#EFF3FB" />
                                </asp:GridView>
                                 
         </fieldset>
          <div id="DivCommand1" style="padding-top:3px;">
        <div style="text-align:left;float:left">
          <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px"  CausesValidation="False"  />
        </div>
        <div style="text-align:right;">
          <asp:Button ID="btnSave" runat="server" Text="Send Approval Request" UseSubmitBehavior="False" OnClick="btnSave_Click"   />
         </div>
      </div>
     </div>
    
</div>
</asp:Content>

