<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="OTApproval.aspx.cs" Inherits="Attendance_OTApproval" Title="OT Approval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <script language="javascript" type="text/javascript" src="../Script/datetimepicker.js">
    //Date Time Picker script
</script>
  <script language="javascript" type="text/javascript">

  
  window.onload=SearchByChanged;
function SearchByChanged()
{
    var ddlSB = document.getElementById('<%= ddlSearchBy.ClientID %>');
    var pnSV=document.getElementById('<%=pnlDept.ClientID%>');
    var pnlD=document.getElementById('<%=pnlValue.ClientID%>');
    var myindex  = ddlSB.selectedIndex
    var SelValue = ddlSB.options[myindex].value
    if(SelValue=="4")
    {
      
        pnSV.style.visibility="hidden";
        pnlD.style.visibility="visible";
      
    }
    if(SelValue=="3")
    {
       
        pnSV.style.visibility="visible";
        pnlD.style.visibility="hidden";
    }
    return ChkToChanged();
}

function ValidateEmpNo()
{
    var ddlSB = document.getElementById('<%= ddlSearchBy.ClientID %>');
    var txtV=document.getElementById('<%=txtEmpId.ClientID%>');
    var myindex  = ddlSB.selectedIndex
    var SelValue = ddlSB.options[myindex].value
    if(SelValue=="1")
    {
        
        if(txtV.value='')
        {
            return false;
        }
    }
 return true;
}

function ChkToChanged()
{
    var txtADT= document.getElementById('<%=txtAttnDateTo.ClientID%>');
    var chTo=document.getElementById('<%=chkTo.ClientID%>');
    if(chTo.checked==false)
    {
       txtADT.disabled=true;
      
    }
    else
    {
        txtADT.disabled=false;       
    }   
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
  <div class="formStyle">
    <div id="formhead"> OT Approval </div>
    <div class="MsgBox">
      <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
    </div>
    <div class="iesEmp">
      <fieldset>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
<TABLE><TBODY><TR><TD style="WIDTH: 3px; HEIGHT: 34px"><asp:Label id="Label1" runat="server" Text="Search By" CssClass="textlevel" Width="80px" __designer:wfdid="w61"></asp:Label></TD><TD style="WIDTH: 81px; HEIGHT: 24px"><asp:DropDownList id="ddlSearchBy" runat="server" Width="100px" __designer:wfdid="w62" onchange="SearchByChanged()">
                      <asp:ListItem Value="3">Branch</asp:ListItem>
                      <asp:ListItem Value="4">Employee ID</asp:ListItem>
                    </asp:DropDownList></TD><TD style="WIDTH: 3px; HEIGHT: 24px"><asp:Panel id="pnlValue" runat="server" __designer:dtid="1688849860263957" Width="125px" __designer:wfdid="w9" Height="25px">
                      <TABLE __designer:dtid="1688849860263958">
                        <TBODY>
                          <TR __designer:dtid="1688849860263959">
                            <TD style="WIDTH: 3px" __designer:dtid="1688849860263960"><asp:Label id="Label4" runat="server" __designer:dtid="1688849860263961" CssClass="textlevelshort" Text="Enter Value" __designer:wfdid="w10"></asp:Label></TD>
                            <TD __designer:dtid="1688849860263962"><asp:TextBox id="txtEmpId" runat="server" Width="80px" onkeyup="ToUpper(this)"></asp:TextBox></TD>
                            <TD style="WIDTH: 3px" __designer:dtid="1688849860263964"></TD>
                          </TR>
                        </TBODY>
                      </TABLE>
                      </asp:Panel></TD><TD align=right><asp:Label id="Label2" runat="server" Text="Attnd Date" CssClass="textlevel" Width="55px" __designer:wfdid="w64"></asp:Label></TD><TD style="WIDTH: 3px; HEIGHT: 24px"><asp:TextBox id="txtAttnFromDate" runat="server" Width="80px" __designer:wfdid="w65"></asp:TextBox></TD><TD style="WIDTH: 3px; HEIGHT: 24px"><A href="javascript:NewCal('ctl00_ContentPlaceHolder1_txtAttnFromDate','ddmmyyyy')"><IMG style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px" height=16 alt="Pick a date" src="../images/cal.gif" width=16 /></A></TD><TD style="WIDTH: 3px; HEIGHT: 24px"><asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" __designer:wfdid="w66" SetFocusOnError="True" ControlToValidate="txtAttnFromDate" ErrorMessage="*"></asp:RequiredFieldValidator></TD><TD style="WIDTH: 3px; HEIGHT: 24px"><asp:CheckBox id="chkTo" onclick="return ChkToChanged();" runat="server" Text="To" CssClass="textlevelshort" Width="40px" __designer:wfdid="w67"></asp:CheckBox></TD><TD style="WIDTH: 3px; HEIGHT: 24px"><asp:TextBox id="txtAttnDateTo" runat="server" Width="94px" __designer:wfdid="w68"></asp:TextBox></TD><TD style="WIDTH: 3px; HEIGHT: 24px"><A href="javascript:NewCal('ctl00_ContentPlaceHolder1_txtAttnDateTo','ddmmyyyy')"><IMG style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px" height=16 alt="Pick a date" src="../images/cal.gif" width=16 /></A></TD><TD style="WIDTH: 3px; HEIGHT: 24px"></TD></TR></TBODY></TABLE><TABLE><TBODY><TR><TD style="HEIGHT: 24px; TEXT-ALIGN: left" align=right colSpan=5><asp:Panel id="pnlDept" runat="server" __designer:wfdid="w11"><TABLE><TBODY><TR><TD><asp:Label id="Label5" runat="server" CssClass="textlevel" Width="77px" __designer:wfdid="w16">Branch</asp:Label></TD><TD style="WIDTH: 3px"><asp:DropDownList id="ddlSearchValue" runat="server" Width="190px" Font-Size="9pt" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchValue_SelectedIndexChanged"></asp:DropDownList></TD><TD style="WIDTH: 3px"><asp:Label id="lblDivision" runat="server" CssClass="textlevel" Width="45px" __designer:wfdid="w15">Division</asp:Label></TD><TD style="WIDTH: 3px"><asp:DropDownList id="ddlDivision" runat="server" Width="200px" Height="22px" Font-Size="9pt"></asp:DropDownList></TD></TR></TBODY></TABLE></asp:Panel></TD><TD style="WIDTH: 94px"><asp:Label id="Label7" runat="server" Text="Shift" CssClass="textlevel" Width="30px" __designer:wfdid="w72"></asp:Label></TD><TD><asp:DropDownList id="ddlShift" runat="server" Width="100px" Font-Size="9pt">
                      <asp:ListItem Value="AL">All</asp:ListItem>                      
                    </asp:DropDownList></TD><TD style="HEIGHT: 24px" align=right colSpan=2><asp:Label id="Label3" runat="server" Text="Status" CssClass="textlevel" Width="34px" __designer:wfdid="w72"></asp:Label></TD><TD style="WIDTH: 3px; HEIGHT: 24px"><asp:DropDownList id="ddlAttnStatus" runat="server" Width="100px" Font-Size="9pt">
                      <asp:ListItem Value="AL">All</asp:ListItem>
                      <asp:ListItem Value="A">Absent (A)</asp:ListItem>
                      <asp:ListItem Value="L">Late (L)</asp:ListItem>
                      <asp:ListItem Value="LV">Leave (LV)</asp:ListItem>
                      <asp:ListItem Value="P">Present</asp:ListItem>
                      <asp:ListItem Value="X">Shift Error</asp:ListItem>
                    </asp:DropDownList></TD><TD style="WIDTH: 3px; HEIGHT: 24px"><asp:CompareValidator id="CompareValidator1" runat="server" __designer:wfdid="w74" ControlToValidate="ddlAttnStatus" ErrorMessage="*" ValueToCompare="-1" Operator="NotEqual"></asp:CompareValidator></TD><TD style="WIDTH: 3px; HEIGHT: 24px"><asp:Button id="btnRetrieve" onclick="btnRetrieve_Click" runat="server" Text="Retrieve" Width="70px" __designer:wfdid="w75" onClientClick="return ValidateEmpNo();"></asp:Button></TD></TR></TBODY></TABLE>
</ContentTemplate>
          <triggers>
<asp:AsyncPostBackTrigger ControlID="btnRetrieve" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
        </asp:UpdatePanel>
      </fieldset>
      &nbsp; </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
      <ContentTemplate>
        <DIV id="iFrame">
          <asp:GridView id="grOT" runat="server" Width="940px" __designer:wfdid="w60" Font-Size="9px" EmptyDataText="No Record Found" DataKeyNames="SL,SunIn,SunOut,ArvlGrace,AttnPolicyId,LunchBreak,OTStartGrace,otApproveBy,otApproveDate,otRecmndBy,otRecmndDate" AutoGenerateColumns="False">
            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
            <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
            <AlternatingRowStyle BackColor="#EFF3FB" />
            <Columns>
            <asp:BoundField HeaderText="SL No.">
              <ItemStyle CssClass="ItemStylecssRight" Width="80px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Select">
              <ItemStyle CssClass="ItemStylecss" width="40px" />
              <ItemTemplate>
                <asp:CheckBox ID="chkBox" runat="Server" > </asp:CheckBox>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="EmpId" HeaderText="Emp.No">
              <ItemStyle CssClass="ItemStylecss" Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="FullName" HeaderText="Employee Name">
              <ItemStyle CssClass="ItemStylecss" Width="140px" />
            </asp:BoundField>
            <asp:BoundField DataField="JobTitle" HeaderText="Designation">
              <ItemStyle CssClass="ItemStylecss" Width="120px" />
            </asp:BoundField>
            <asp:BoundField DataField="DeptName" HeaderText="Department">
              <ItemStyle CssClass="ItemStylecss" Width="120px" />
            </asp:BoundField>
            <asp:BoundField DataField="AttndDate" HeaderText="Date">
              <ItemStyle CssClass="ItemStylecss" Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="SignInTime" HeaderText="In Time">
              <ItemStyle CssClass="ItemStylecss" Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="SignOutTime" HeaderText="Out Time">
              <ItemStyle CssClass="ItemStylecss" Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="Status" HeaderText="Status">
              <ItemStyle CssClass="ItemStylecss" Width="60px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Delay">
              <ItemTemplate >
                <asp:Label ID="lblDelays" runat="server" Text='<%# CalculateDelay(Convert.ToString(Eval("SignInTime")),Convert.ToString(Eval("SunIn")),Convert.ToString(Eval("ArvlGrace"))) %>'/>
              </ItemTemplate>
              <ItemStyle CssClass="ItemStylecss" Width="40px" />
            </asp:TemplateField>
            <asp:BoundField DataField="PolicyName" HeaderText="Shift Held">
              <ItemStyle CssClass="ItemStylecss" Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="ExtraTimeWorked" HeaderText="OT(H:M)">
              <ItemStyle CssClass="ItemStylecss" Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="RecOT" HeaderText="RecOT(H:M)">
              <ItemStyle CssClass="ItemStylecss" Width="60px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="OT Forced">
              <ItemTemplate >
                <asp:TextBox ID="txtOTH" runat="server"  MaxLength ="2" CssClass="TextBoxAmt40" Text='<%# SetDefaultOTData( Convert.ToString(Eval("RecOT")),Convert.ToString(Eval("ExtraTimeWorked")),Convert.ToString(Eval("ExtraTimeWorkedF")),Convert.ToString(Eval("IsForcedTimeSet")),"H") %>'/>
                <cc1:FilteredTextBoxExtender ID="FiltTxtBoxEx" FilterType ="Numbers"   runat="server" TargetControlID="txtOTH"> </cc1:FilteredTextBoxExtender>
              </ItemTemplate>
              <ItemStyle CssClass="ItemStylecss" Width="40px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="HH:MM">
              <ItemTemplate >
                <asp:TextBox ID="txtOTM" runat="server"  MaxLength ="2" CssClass="TextBoxAmt40" Text='<%# SetDefaultOTData(Convert.ToString(Eval("RecOT")), Convert.ToString(Eval("ExtraTimeWorked")),Convert.ToString(Eval("ExtraTimeWorkedF")),Convert.ToString(Eval("IsForcedTimeSet")),"M") %>'/>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType ="Numbers"   runat="server" TargetControlID="txtOTM" > </cc1:FilteredTextBoxExtender>
                <asp:HiddenField ID="hfAppStatus" Value='<%# Convert.ToString(Eval("AppStatus")) %>' runat="server"></asp:HiddenField>
              </ItemTemplate>
              <ItemStyle CssClass="ItemStylecss" Width="40px" />
            </asp:TemplateField>
            <asp:BoundField DataField="otApproveBy" HeaderText="Approve By">
              <ItemStyle CssClass="ItemStylecss" Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="otApproveDate" HeaderText="Approve Date">
              <ItemStyle CssClass="ItemStylecss" Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="otRecmndBy" HeaderText="Recommend By">
              <ItemStyle CssClass="ItemStylecss" Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="otRecmndDate" HeaderText="Recommend Date">
              <ItemStyle CssClass="ItemStylecss" Width="60px" />
            </asp:BoundField>
            </Columns>
          </asp:GridView>
        </DIV>
        <A id="A1" onclick="javascript: selectAllNone('<%= this.grOT.ClientID %>',true)" href="#">Select All</A> <A id="A2" onclick="javascript: selectAllNone('<%= this.grOT.ClientID %>',false)" href="#">Clear All</A>
        <TABLE>
          <TBODY>
            <TR>
              <td style="width:540px"></td>
              <TD><asp:Label id="lblTotalOT" runat="server" Text="Total" CssClass="textlevelshort"></asp:Label></TD>
              <TD><asp:TextBox id="txtTotalOTHr" runat="server" CssClass="TextBoxAmt40" Width="40px" ></asp:TextBox></TD>
              <TD><asp:TextBox id="txtTotalOTMin" runat="server" CssClass="TextBoxAmt40" Width="40px" ></asp:TextBox></TD>
            </TR>
          </TBODY>
        </TABLE>
      </ContentTemplate>
    </asp:UpdatePanel>
    <div style="margin: 20px 5px 5px 5px;height:30px;">
      <div style="text-align:left;float:left">
        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="80px"  CausesValidation="False" OnClick="btnRefresh_Click" />
      </div>
      <div style="text-align:right;">
        <asp:Button ID="btnSave" runat="server" Text="Approve" Width="80px"  UseSubmitBehavior="False" OnClick="btnSave_Click"  />
        <asp:Button ID="btnDelete" runat="server" Text="Deny" Width="80px" OnClick="btnDelete_Click"  />
        <asp:Button ID="btnClose" runat="server" Text="Close" Width="80px" />
      </div>
    </div>
  </div>
</asp:Content>
