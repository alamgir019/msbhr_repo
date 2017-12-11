<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" 
CodeFile="TaskAlert.aspx.cs" Inherits="File_TaskAlert" Title="Task Alert" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language ="javascript" type="text/javascript" src ="../JScripts/Confirmation.js">
</script>
<script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js">
    //Date Time Picker script
</script>

<div class="leaveApplicaionStyle">    
    <div id="formhead1"> Task Alert</div>
    <div class="MsgBox">
      <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
    </div>    
    <div id="leaveApplicaionFormInner">
        <fieldset style="background-color:#EFF3FB;">
        <legend>Task Alert</legend> 
        <table>
            <tr>
                <td class="textlevel">
                Task Title
                </td>
                <td>
                    <asp:TextBox ID="txtTitle" runat="server" Width="600px"></asp:TextBox></td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="textlevel">
                    Monitored By</td>
                <td>
                    <asp:DropDownList ID="ddlEmpId" runat="server" Width="355px" ></asp:DropDownList></td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="textlevel">
                Reminder Date
                </td>
                <td>
                    <asp:TextBox ID="txtReminderDate" runat="server" Width="80px"></asp:TextBox>
                    <A href="javascript:NewCal('<%= txtReminderDate.ClientID %>','ddmmyyyy')"><IMG style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px" height=16 alt="Pick a date" src="../images/cal.gif" width=16 /></A>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtReminderDate"
                        CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"
                        ></asp:RegularExpressionValidator></td>
                    
                <td>
                    </td>
            </tr>
            <tr>
                <td class="textlevel">
                Completed Date
                </td>
                <td>
                    <asp:TextBox ID="txtCompletedDate" runat="server" Width="80px"></asp:TextBox>
                    <A href="javascript:NewCal('<%= txtCompletedDate.ClientID %>','ddmmyyyy')"><IMG style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px" height=16 alt="Pick a date" src="../images/cal.gif" width=16 /></A>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCompletedDate"
                        CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"
                        ></asp:RegularExpressionValidator></td>
                                                          
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" Width="80px"  /></td>
            </tr>
        </table>
         </fieldset>
         <fieldset style="margin-top:10px;">
         
        <legend>Pending Task Alert List</legend> 
        <hr />
        <DIV style="OVERFLOW: scroll; WIDTH: 98%; HEIGHT: 250px">        
            <asp:GridView id="grTaskAlertList" runat="server" Width="97%" Font-Size="9px" 
                EmptyDataText="No Record Found" AutoGenerateColumns="False" 
                DataKeyNames="TransId,ToEmpId,PersEmail1" 
                OnRowCommand="grTaskAlertList_RowCommand">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True"></SelectedRowStyle>
                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                <Columns>  
                <asp:ButtonField CommandName="UpdateClick" HeaderText="Set As Completed" Text="Completed">
                  <ItemStyle Width="5%" CssClass="ItemStylecss"/>
                </asp:ButtonField>              
                <asp:BoundField DataField="TaskTitle" HeaderText="Task Title">
                  <ItemStyle CssClass="ItemStylecss" Width="40%"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Reminder Date">
                    <ItemTemplate>
                      <asp:TextBox ID="txtGrReminderDate" width="120px" text='<%# Common.DisplayDate(Convert.ToString(Eval("ReminderDate"))) %>' runat="server"/>
                      <A href="javascript:NewCal('<%# ((GridViewRow)Container).FindControl("txtGrReminderDate").ClientID %>','ddmmyyyy')"><IMG style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px" height="16px" alt="Pick a date" src="../images/cal.gif" width="16px" /></A>                               
                    </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Completed Date">
                    <ItemTemplate>
                      <asp:TextBox ID ="txtGrCompletedDate" width="120px" text='<%# Common.DisplayDate(Convert.ToString(Eval("CompletedDate")))%>' runat="server"/>
                      <A href="javascript:NewCal('<%# ((GridViewRow)Container).FindControl("txtGrCompletedDate").ClientID %>','ddmmyyyy')"><IMG style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px" height="16px" alt="Pick a date" src="../images/cal.gif" width="16px" /></A>                               
                    </ItemTemplate>
                </asp:TemplateField>                                     
                <asp:BoundField DataField="ToEmpId" HeaderText="Monitored By">
                  <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                </asp:BoundField>
                <asp:ButtonField CommandName="SetAlertClick" HeaderText="Set Alert" Text="Set Alert">
                  <ItemStyle Width="10%" CssClass="ItemStylecss"/>
                </asp:ButtonField>              
                </Columns>
            </asp:GridView>
          </DIV>                  
          </fieldset>          
    </div> 
</div> 
</asp:Content>

