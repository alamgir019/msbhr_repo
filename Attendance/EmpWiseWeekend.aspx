<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="EmpWiseWeekend.aspx.cs" Inherits="EIS_EmpWiseWeekend" Title="Employee Wise Weekend" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js">
//Date Time Picker script
</script>
<script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
</script>
    
<script language="javascript" type="text/javascript">
//window.onload=SearchByChanged;
function SearchByChanged()
{
    var ddlSB = document.getElementById('<%= ddlSearchBy.ClientID %>');
    var pnlV=document.getElementById('<%=pnlValue.ClientID%>');
    var lblV=document.getElementById('<%=lblValue.ClientID %>'); 
    var pnlD=document.getElementById('<%=pnlDivision.ClientID%>');
    var txtV=document.getElementById('<%=txtSearchValue.ClientID%>');
    var myindex  = ddlSB.selectedIndex;
    var SelValue = ddlSB.options[myindex].value;
    if(SelValue=="0")
    {       
        pnlV.style.visibility="hidden";
        pnlD.style.visibility="visible";      
        txtV.value="";         
    }  
    if(SelValue=="2")
    {       
        pnlV.style.visibility="visible";
        pnlD.style.visibility="hidden";
        lblV.innerHTML="Name"; 
    }
    if(SelValue=="3")
    {       
        pnlV.style.visibility="visible";
        pnlD.style.visibility="hidden";
        lblV.innerHTML="Card No";
    }
    if(SelValue=="4")
    {       
        pnlV.style.visibility="visible";
        pnlD.style.visibility="hidden";
        lblV.innerHTML="Abb. Name";
    }
    if(SelValue=="5")
    { 
        pnlV.style.visibility="visible";
        pnlD.style.visibility="hidden";
        lblV.innerHTML="Emp. No";
    }
}

function ValidateEmpNo()
{
    var ddlSB = document.getElementById('<%= ddlSearchBy.ClientID %>');
    var txtV=document.getElementById('<%=txtSearchValue.ClientID%>');
    var myindex  = ddlSB.selectedIndex;
    var SelValue = ddlSB.options[myindex].value;
   
    if(SelValue=="1")
    {
        if(txtV.value='')
        {
            return false;
        }
    }
 return true;
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
<div class="Form1000">
    <fieldset >
     <div id="formhead">
            Employee Wise Weekend
      </div>
        <div class="MsgBox">
        <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
      </div>
      
       <div class="Div950">
           <fieldset>
           <div>           
           <table>
                   <tr>
                       <td style="width: 3px; height: 24px;">
                           <asp:Label ID="Label1" runat="server" Text="Serach By" CssClass="textlevelshort"></asp:Label></td>
                       <td style="width: 81px; height: 24px;">
                           <asp:DropDownList ID="ddlSearchBy" runat="server" Width="127px" onchange="SearchByChanged()">
                               <asp:ListItem Value="0">Division</asp:ListItem>
                               <asp:ListItem Value="5">Emp. No</asp:ListItem>
                               <asp:ListItem Value="2">Name</asp:ListItem>
                               <asp:ListItem Value="3">Card No</asp:ListItem>
                               <asp:ListItem Value="4">Abbreviate Name</asp:ListItem>
                           </asp:DropDownList></td>
                            <%--<td style="width: 3px; height: 24px;">
                           <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlSearchBy" Operator="NotEqual" SetFocusOnError="True" ValueToCompare="-1"></asp:CompareValidator></td>--%>
                       <td style="width: 3px; height: 24px;">
                           <asp:Panel ID="pnlValue" runat="server" Height="25px" Width="125px">
                               <table>
                                   <tr>
                                       <td style="width: 3px">
                                           <asp:Label ID="lblValue" runat="server" CssClass="textlevelshort" Text="Enter Value"></asp:Label></td>
                                       <td style="width: 168px">
                                           <asp:TextBox ID="txtSearchValue" runat="server" Width="229px"></asp:TextBox></td>
                                       <td style="width: 3px">
                                           </td>
                                   </tr>
                               </table>
                           </asp:Panel>
                       </td>
                   </tr>
               </table>
               <asp:UpdatePanel id="UpdatePanel1" runat="server">
                   <contenttemplate>
<DIV class="Div930"><DIV style="FLOAT: left; HEIGHT: 91px; TEXT-ALIGN: left"><asp:Panel id="pnlDivision" runat="server" Width="769px" Height="1px"><TABLE><TBODY><TR><TD style="height: 24px"><asp:Label id="Label2" runat="server" Text="Division" CssClass="textlevelshort"></asp:Label></TD><TD style="WIDTH: 3px; height: 24px;"><asp:DropDownList id="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" Width="150px" AutoPostBack="True"></asp:DropDownList></TD><TD style="WIDTH: 3px; height: 24px;"><asp:Label id="Label5" runat="server" Text="SBU" CssClass="textlevelshort"></asp:Label></TD><TD style="WIDTH: 3px; height: 24px;"><asp:DropDownList id="ddlSBU" runat="server" OnSelectedIndexChanged="ddlSBU_SelectedIndexChanged" Width="150px" AutoPostBack="True"><asp:ListItem Value="-1">All</asp:ListItem>
</asp:DropDownList></TD><TD style="WIDTH: 3px; height: 24px;"><asp:Label id="Label4" runat="server" Text="Department" CssClass="textlevelshort"></asp:Label></TD><TD style="WIDTH: 3px; height: 24px;"><asp:DropDownList id="ddlDept" runat="server" Width="150px"><asp:ListItem Value="-1">All</asp:ListItem>
            </asp:DropDownList></TD></TR><TR><TD><asp:Label id="lblGrade" runat="server" Text="Grade" CssClass="textlevelshort"></asp:Label></TD><TD style="WIDTH: 3px"><asp:DropDownList id="ddlGrade" runat="server" Width="150px"></asp:DropDownList></TD><TD style="WIDTH: 3px"><asp:Label id="lblDesig" runat="server" Text="Designation" CssClass="textlevelshort"></asp:Label></TD><TD style="WIDTH: 3px"><asp:DropDownList id="ddlDesig" runat="server" Width="150px"><asp:ListItem Value="-1">All</asp:ListItem></asp:DropDownList></TD><td>
                <asp:Label ID="Label6" runat="server" CssClass="textlevelshort" Text="Attn. Date"></asp:Label></td><td> 
                    <asp:TextBox ID="txtAttndDate" runat="server" Width="100px"></asp:TextBox> <A href="javascript:NewCal('ctl00_ContentPlaceHolder1_txtAttndDate','ddmmyyyy')"><IMG style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px" height="16px" alt="Pick a date" src="../images/cal.gif" width="16px" /></A></td></TR></TBODY></TABLE></asp:Panel> </DIV><DIV style="HEIGHT: 21px; TEXT-ALIGN: left"><asp:Button id="btnShow" onclick="btnShow_Click" runat="server" Text="Show" Width="80px" OnClientClick="return ValidateEmpNo();"></asp:Button> </DIV></DIV><TABLE style="MARGIN-LEFT: 5px"><TBODY><TR><TD style="WIDTH: 3px"><asp:Button id="btnSetWeekend" onclick="btnSetWeekend_Click" runat="server" Text="Set Weekend" Width="120px"></asp:Button></TD><TD style="WIDTH: 3px"><asp:Button id="btnRemoveWeekend" onclick="btnRemoveWeekend_Click" runat="server" Text="Remove Weekend" Width="120px"></asp:Button></TD></TR></TBODY></TABLE> <asp:HiddenField id="hfAttndDate" runat="server" __designer:wfdid="w1"></asp:HiddenField>
</contenttemplate>
               </asp:UpdatePanel>
           </div>
                    
               
           </fieldset>
       </div>
       
       
        <asp:UpdatePanel id="UpdatePanel2" runat="server">
            <contenttemplate>
<DIV style="BORDER-RIGHT: gray 1px solid; BORDER-TOP: gray 1px solid; MARGIN-BOTTOM: 5px; MARGIN-LEFT: 10px; OVERFLOW: scroll; BORDER-LEFT: gray 1px solid; WIDTH: 900px; MARGIN-RIGHT: 5px; BORDER-BOTTOM: gray 1px solid; HEIGHT: 280px"><STRONG>Employee Search Result </STRONG><asp:GridView id="grEmployee" runat="server" Font-Size="9px" Width="980px" AutoGenerateColumns="False" DataKeyNames="EmpId,DeptId,DesgId" EmptyDataText="No Record Found">
                                 
                                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" Font-Size="12px" />
                                <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="#EFF3FB" />
                                <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemStyle CssClass="ItemStylecss" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkBox" runat="Server" >
                                                    </asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:BoundField DataField="EmpId" HeaderText="Emp.No">
                                        <ItemStyle CssClass="ItemStylecss" Width="80px" />
                                    </asp:BoundField>
                                      <asp:BoundField DataField="FullName" HeaderText="Employee Name">
                                        <ItemStyle CssClass="ItemStylecss" Width="200px" />
                                    </asp:BoundField>
                                      <asp:BoundField DataField="JobTitle" HeaderText="Designation">
                                        <ItemStyle CssClass="ItemStylecss" Width="120px" />
                                    </asp:BoundField>
                                      <asp:BoundField DataField="DeptName" HeaderText="Department">
                                        <ItemStyle CssClass="ItemStylecss" Width="120px" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Status" HeaderText="Curr. Status">
                                        <ItemStyle CssClass="ItemStylecss" Width="120px" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="NewStatus" HeaderText="New Status">
                                        <ItemStyle CssClass="ItemStylecss" Width="120px" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="ExtraTimeWorked" HeaderText="OT">
                                        <ItemStyle CssClass="ItemStylecss" Width="120px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="">
                                    <ItemTemplate >
                                        <asp:HiddenField ID="hfCurrStatus" Value='<%# Convert.ToString(Eval("Status")) %>' runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="AttnPolicyId" Value='<%# Convert.ToString(Eval("AttnPolicyId")) %>' runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hfSignOutTime" Value='<%# Convert.ToString(Eval("SignOutTime")) %>' runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hfSignInTime" Value='<%# Convert.ToString(Eval("SignInTime")) %>' runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hfExtraTimeWorked" Value='<%# Convert.ToString(Eval("ExtraTimeWorked")) %>' runat="server"></asp:HiddenField>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="ItemStylecss" Width="1px" />
                                </asp:TemplateField> 
                                </Columns>
                            </asp:GridView> </DIV>
                            <div class="DivCommand1"> 
                                 <div class="DivCommandL">
                                        <table>
                                            <tr>
                                                <td>
                                                  <A id="A1" onclick="javascript: selectAllNone('<%= this.grEmployee.ClientID %>',true)" href="#">Select All</A> 
                                                </td>
                                                <td>
                                                    <A id="A2" onclick="javascript: selectAllNone('<%= this.grEmployee.ClientID %>',false)" href="#">Clear All</A> 
                                                </td>
                                            </tr>
                                        </table>
                                 </div>
                                 <div class="DivCommandR">
                                    <TABLE><TBODY><TR><TD style="WIDTH: 3px"><asp:Label id="Label3" runat="server" Text="Total Record Count:" CssClass="textlevel"></asp:Label></TD><TD style="WIDTH: 3px"><asp:Label id="lblRecordCount" runat="server" ForeColor="Blue" Font-Size="Smaller" Font-Bold="True"></asp:Label></TD></TR></TBODY></TABLE>
                                 </div>
                            </DIV>
</contenttemplate>
        </asp:UpdatePanel>
         <div class="DivCommand1"> 
                    <div class="DivCommandL">
                        <asp:Button ID="btnClear" runat="server" Text="Refresh" Width="70px" UseSubmitBehavior="False" OnClick="btnClear_Click" />
                    </div>
                    <div class="DivCommandR">
                       <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click"  />
                    </div>
                   
             </div>
   </fieldset>
</div>
</asp:Content>

