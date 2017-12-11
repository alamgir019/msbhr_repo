<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="AdjustAttendance.aspx.cs" Inherits="Attendance_AdjustAttendance" Title="Adjust Attendance" %>

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
       // alert(SelValue);
        pnSV.style.visibility="hidden";
        pnlD.style.visibility="visible";
      
    }
    if(SelValue=="3")
    {
        //alert(SelValue);
        pnSV.style.visibility="visible";
        pnlD.style.visibility="hidden";
    }
    return DisplayControl();    
}

function ValidateEmpNo()
{
    var ddlSB = document.getElementById('<%= ddlSearchBy.ClientID %>');
    var txtV=document.getElementById('<%=txtEmpId.ClientID%>');
    var myindex  = ddlSB.selectedIndex
    var SelValue = ddlSB.options[myindex].value
    if(SelValue=="1")
    {
        //alert("hi");
        if(txtV.value='')
        {
            return false;
        }
    }
 return true;
}
  
 function DisplayControl()
 {
     var txtADT= document.getElementById('<%=txtAttnDateTo.ClientID%>');
     var ddlIH= document.getElementById('<%=ddlInHour.ClientID%>');
     var ddlIM= document.getElementById('<%=ddlInMin.ClientID%>');
     var ddlOH=document.getElementById('<%=ddlOutHour.ClientID%>');
     var ddlOM=document.getElementById('<%=ddlOutMin.ClientID%>');
     var ddlS=document.getElementById('<%=ddlStatus.ClientID%>');
     var ddlSh=document.getElementById('<%=ddlShift.ClientID%>');
      
     var chTo=document.getElementById('<%=chkTo.ClientID%>');
     var chIn=document.getElementById('<%=chkIn.ClientID%>');
     var chOut=document.getElementById('<%=chkOut.ClientID%>');
     var chStatus=document.getElementById('<%=chkStatus.ClientID%>');
     var chShift=document.getElementById('<%=chkShift.ClientID%>');

     if(chTo.checked==false)
        {
            txtADT.disabled=true;
        }
      if(chIn.checked==false)
        {
            ddlIH.disabled=true;
            ddlIM.disabled=true;
        }
        if(chOut.checked==false)
        {
            ddlOH.disabled=true;
            ddlOM.disabled=true;
        }
        if(chStatus.checked==false)
        {
            ddlS.disabled=true;
        }
         if(chShift.checked==false)
        {
            ddlSh.disabled=true;
        }
        return false;
}

function CheckBoxToSelect(cbControl)
{   
       var chkBox= document.getElementById(cbControl);
       var txtADT= document.getElementById('<%=txtAttnDateTo.ClientID%>');
       alert("To");
        if(chkBox.checked==false)
        {
         txtADT.disabled=true;
      
        }
        else
        {
        txtADT.disabled=false;
        }
       
        return false; 
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

function ChkInChanged()
{
    var ddlIH= document.getElementById('<%=ddlInHour.ClientID%>');
    var ddlIM= document.getElementById('<%=ddlInMin.ClientID%>');
    var chIn=document.getElementById('<%=chkIn.ClientID%>');
    if(chIn.checked==false)
    {
       ddlIH.disabled=true;
       ddlIM.disabled=true;
    }
    else
    {
         ddlIH.disabled=false;
         ddlIM.disabled=false;
    }
}

function ChkOutChanged()
{
   var ddlOH=document.getElementById('<%=ddlOutHour.ClientID%>');
   var ddlOM=document.getElementById('<%=ddlOutMin.ClientID%>');
   var chOut=document.getElementById('<%=chkOut.ClientID%>');
   if(chOut.checked==false)
   {
        ddlOH.disabled=true;
        ddlOM.disabled=true;
   }
    else
    {
        ddlOH.disabled=false;
        ddlOM.disabled=false;
    }
}

function ChkStatusChanged()
{
   var ddlS=document.getElementById('<%=ddlStatus.ClientID%>');
   var chStatus=document.getElementById('<%=chkStatus.ClientID%>');
   if(chStatus.checked==false)
   {
        ddlS.disabled=true;
   }
    else
    {
        ddlS.disabled=false;         
    }
}

function ChkShiftChanged()
{
  var ddlSh=document.getElementById('<%=ddlShift.ClientID%>');
  var chShift=document.getElementById('<%=chkShift.ClientID%>');
  if(chShift.checked==false)
  {
        ddlSh.disabled=true;
  }
  else
  {
        ddlSh.disabled=false;
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

function ValidateIsNextDay()
{
    var ddlIH = document.getElementById('<%= ddlInHour.ClientID %>');
    var ddlIM = document.getElementById('<%= ddlInMin.ClientID %>');
    var ddlOH = document.getElementById('<%= ddlOutHour.ClientID %>');
    var ddlOM = document.getElementById('<%= ddlOutMin.ClientID %>');
    var chND=document.getElementById('<%=chkIsNextDay.ClientID%>');
    
    var myindexIH  = ddlIH.selectedIndex;
    var SelValueIH = ddlIH.options[myindexIH].value;
    
    var myindexIM  = ddlIM.selectedIndex;
    var SelValueIM = ddlIM.options[myindexIM].value;
    
    var myindexOH  = ddlOH.selectedIndex;
    var SelValueOH = ddlOH.options[myindexOH].value;
    
    var myindexOM  = ddlOM.selectedIndex;
    var SelValueOM = ddlOM.options[myindexOM].value;
    
    var IH= parseInt(SelValueIH);
    var OH= parseInt(SelValueOH);
    var IM= parseInt(SelValueIM);
    var OM= parseInt(SelValueOM);
     
    if(OH == IH)
    {
       if(OM < IM)
        {
            
            alert("Out Time is Selected as Next Day");
            chND.checked=true;
        }
        else
        {
            chND.checked=false;
        }
    }
    else if(OH < IH)
    {
        chND.checked=true;
    }
//    else if(OH > IH)
//    {
//        chND.checked=false;
//    }
}
</script>
  <div class="formStyle">
    <div id="formhead3">Adjust Attendance</div>
    <div class="MsgBox">
      <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
    </div>
    <div class="iesEmp">
      <fieldset >
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
<TABLE><TBODY><TR><TD style="WIDTH: 3px; HEIGHT: 24px" align=right><asp:Label id="Label1" runat="server" Text="Search By" CssClass="textlevel" Width="79px"></asp:Label></TD><TD style="WIDTH: 81px; HEIGHT: 24px"><asp:DropDownList id="ddlSearchBy" runat="server" Width="100px" onchange="SearchByChanged()" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged" Font-Size="9pt"><asp:ListItem Value="3">Location</asp:ListItem>
<asp:ListItem Value="4">Employee ID</asp:ListItem>
</asp:DropDownList></TD><TD style="WIDTH: 3px; HEIGHT: 24px"></TD><TD style="WIDTH: 3px; HEIGHT: 24px"><asp:Panel id="pnlValue" runat="server" Width="125px" Height="25px">
                      <TABLE>
                        <TBODY>
                          <TR>
                            <TD style="WIDTH: 3px"><asp:Label id="Label4" runat="server" Text="Enter Value" CssClass="textlevelshort"></asp:Label></TD>
                            <TD><asp:TextBox id="txtEmpId" runat="server" Width="80px" onkeyup="ToUpper(this)"></asp:TextBox></TD>                            
                          </TR>
                        </TBODY>
                      </TABLE>
                      </asp:Panel></TD><TD style="WIDTH: 3px; HEIGHT: 24px; TEXT-ALIGN: right" align=right><asp:Label id="Label2" runat="server" Text="Attendance Date" CssClass="textlevel" Width="94px"></asp:Label></TD><TD style="WIDTH: 3px; HEIGHT: 24px"><asp:TextBox id="txtAttnFromDate" runat="server" Width="80px"></asp:TextBox></TD><TD style="WIDTH: 3px; HEIGHT: 24px"><A href="javascript:NewCal('<%= txtAttnFromDate.ClientID %>','ddmmyyyy')"><IMG style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px" height=16 alt="Pick a date" src="../images/cal.gif" width=16 /></A></TD><TD style="WIDTH: 3px; HEIGHT: 24px"><asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtAttnFromDate"></asp:RequiredFieldValidator></TD><TD style="WIDTH: 3px; HEIGHT: 24px"><asp:CheckBox id="chkTo" onclick="ChkToChanged()" runat="server" Text="To" CssClass="textlevelshort" Width="40px"></asp:CheckBox></TD><TD style="WIDTH: 3px; HEIGHT: 24px"><asp:TextBox id="txtAttnDateTo" runat="server" Width="94px"></asp:TextBox></TD><TD style="WIDTH: 3px; HEIGHT: 24px"><A href="javascript:NewCal('<%= txtAttnDateTo.ClientID %>','ddmmyyyy')"><IMG style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px" height=16 alt="Pick a date" src="../images/cal.gif" width=16 /></A></TD><TD style="WIDTH: 3px; HEIGHT: 24px"></TD></TR></TBODY></TABLE><TABLE><TBODY><TR><TD style="HEIGHT: 24px; TEXT-ALIGN: left" align=left colSpan=6><asp:Panel id="pnlDept" runat="server"><TABLE><TBODY><TR><TD><asp:Label id="Label6" runat="server" CssClass="textlevel" Width="72px">Location</asp:Label></TD><TD style="WIDTH: 103px"><asp:DropDownList id="ddlLocation" runat="server" Width="211px" Font-Size="9pt"></asp:DropDownList></TD><TD style="WIDTH: 86px"></TD><TD style="WIDTH: 86px"></TD></TR><TR><TD><asp:Label id="lblEmpType" runat="server" CssClass="textlevel" Width="76px" Visible="False">Emp Type</asp:Label></TD><TD style="WIDTH: 103px"><asp:DropDownList id="ddlEmpType" runat="server" Width="210px" onchange="SearchByChanged()" Font-Size="9pt" Visible="False" Enabled="True" AutoPostBack="True"></asp:DropDownList></TD><TD style="WIDTH: 86px"><asp:Label id="lblName" runat="server" CssClass="textlevel" Width="76px" Visible="False">Team</asp:Label></TD><TD style="WIDTH: 86px"><asp:DropDownList id="ddlSearchValue" runat="server" Width="210px" Font-Size="9pt" Visible="False" Font-Names="Tahoma"></asp:DropDownList></TD></TR></TBODY></TABLE><TABLE><TBODY></TBODY></TABLE></asp:Panel></TD><TD style="WIDTH: 115px; HEIGHT: 24px" align=right colSpan=4><asp:Label id="Label3" runat="server" Text="Attendance Status" CssClass="textlevel" Width="100px"></asp:Label></TD><TD style="WIDTH: 3px; HEIGHT: 24px"><asp:DropDownList id="ddlAttnStatus" runat="server" Font-Size="9pt">
                      <asp:ListItem Value="0">All</asp:ListItem>
                      <asp:ListItem Value="A">Absent (A)</asp:ListItem>
                      <asp:ListItem Value="L">Late (L)</asp:ListItem>
                      <asp:ListItem Value="LV">Leave (LV)</asp:ListItem>
                      <asp:ListItem Value="P">Present</asp:ListItem>
                      <asp:ListItem Value="X">Shift Error</asp:ListItem>
                    </asp:DropDownList></TD><TD style="WIDTH: 3px; HEIGHT: 24px"><asp:CompareValidator id="CompareValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlAttnStatus" Operator="NotEqual" ValueToCompare="-1"></asp:CompareValidator></TD><TD style="WIDTH: 3px; HEIGHT: 24px"><asp:Button id="btnRetrieve" onclick="btnRetrieve_Click" runat="server" Text="Retrieve" Width="90px" onclientclick="return ValidateEmpNo();"></asp:Button></TD></TR></TBODY></TABLE>
</ContentTemplate>
          <triggers>
<asp:AsyncPostBackTrigger ControlID="btnRetrieve" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
        </asp:UpdatePanel>
      </fieldset>
      &nbsp; </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
<DIV class="iesEmp"><FIELDSET><TABLE><TBODY><TR><TD style="WIDTH: 3px"><asp:CheckBox id="chkIn" onclick="ChkInChanged()" runat="server" Text="In Time" CssClass="textlevelshort" Width="60px"></asp:CheckBox></TD><TD style="WIDTH: 3px"><asp:DropDownList id="ddlInHour" runat="server" Width="45px">
                  <asp:ListItem Value="1">01</asp:ListItem>
                  <asp:ListItem Value="02">02</asp:ListItem>
                  <asp:ListItem Value="03">03</asp:ListItem>
                  <asp:ListItem Value="04">04</asp:ListItem>
                  <asp:ListItem Value="05">05</asp:ListItem>
                  <asp:ListItem Value="06">06</asp:ListItem>
                  <asp:ListItem Value="07">07</asp:ListItem>
                  <asp:ListItem Value="08">08</asp:ListItem>
                  <asp:ListItem Value="09">09</asp:ListItem>
                  <asp:ListItem>10</asp:ListItem>
                  <asp:ListItem>11</asp:ListItem>
                  <asp:ListItem>12</asp:ListItem>
                  <asp:ListItem>13</asp:ListItem>
                  <asp:ListItem>14</asp:ListItem>
                  <asp:ListItem>15</asp:ListItem>
                  <asp:ListItem>16</asp:ListItem>
                  <asp:ListItem>17</asp:ListItem>
                  <asp:ListItem>18</asp:ListItem>
                  <asp:ListItem>19</asp:ListItem>
                  <asp:ListItem>20</asp:ListItem>
                  <asp:ListItem>21</asp:ListItem>
                  <asp:ListItem>22</asp:ListItem>
                  <asp:ListItem>23</asp:ListItem>
                  <asp:ListItem>24</asp:ListItem>
                </asp:DropDownList></TD><TD style="WIDTH: 3px"><asp:DropDownList id="ddlInMin" runat="server" Width="45px">
                  <asp:ListItem Value="00">00</asp:ListItem>
                  <asp:ListItem>01</asp:ListItem>
                  <asp:ListItem>02</asp:ListItem>
                  <asp:ListItem>03</asp:ListItem>
                  <asp:ListItem>04</asp:ListItem>
                  <asp:ListItem>05</asp:ListItem>
                  <asp:ListItem>06</asp:ListItem>
                  <asp:ListItem>07</asp:ListItem>
                  <asp:ListItem>08</asp:ListItem>
                  <asp:ListItem>09</asp:ListItem>
                  <asp:ListItem>10</asp:ListItem>
                  <asp:ListItem>11</asp:ListItem>
                  <asp:ListItem>12</asp:ListItem>
                  <asp:ListItem>13</asp:ListItem>
                  <asp:ListItem>14</asp:ListItem>
                  <asp:ListItem>15</asp:ListItem>
                  <asp:ListItem>16</asp:ListItem>
                  <asp:ListItem>17</asp:ListItem>
                  <asp:ListItem>18</asp:ListItem>
                  <asp:ListItem>19</asp:ListItem>
                  <asp:ListItem>20</asp:ListItem>
                  <asp:ListItem>21</asp:ListItem>
                  <asp:ListItem>22</asp:ListItem>
                  <asp:ListItem>23</asp:ListItem>
                  <asp:ListItem>24</asp:ListItem>
                  <asp:ListItem>25</asp:ListItem>
                  <asp:ListItem>26</asp:ListItem>
                  <asp:ListItem>27</asp:ListItem>
                  <asp:ListItem>28</asp:ListItem>
                  <asp:ListItem>29</asp:ListItem>
                  <asp:ListItem>30</asp:ListItem>
                  <asp:ListItem>31</asp:ListItem>
                  <asp:ListItem>32</asp:ListItem>
                  <asp:ListItem>33</asp:ListItem>
                  <asp:ListItem>34</asp:ListItem>
                  <asp:ListItem>35</asp:ListItem>
                  <asp:ListItem>36</asp:ListItem>
                  <asp:ListItem>37</asp:ListItem>
                  <asp:ListItem>38</asp:ListItem>
                  <asp:ListItem>39</asp:ListItem>
                  <asp:ListItem>40</asp:ListItem>
                  <asp:ListItem>41</asp:ListItem>
                  <asp:ListItem>42</asp:ListItem>
                  <asp:ListItem>43</asp:ListItem>
                  <asp:ListItem>44</asp:ListItem>
                  <asp:ListItem>45</asp:ListItem>
                  <asp:ListItem>46</asp:ListItem>
                  <asp:ListItem>47</asp:ListItem>
                  <asp:ListItem>48</asp:ListItem>
                  <asp:ListItem>49</asp:ListItem>
                  <asp:ListItem>50</asp:ListItem>
                  <asp:ListItem>51</asp:ListItem>
                  <asp:ListItem>52</asp:ListItem>
                  <asp:ListItem>53</asp:ListItem>
                  <asp:ListItem>54</asp:ListItem>
                  <asp:ListItem>55</asp:ListItem>
                  <asp:ListItem>56</asp:ListItem>
                  <asp:ListItem>57</asp:ListItem>
                  <asp:ListItem>58</asp:ListItem>
                  <asp:ListItem>59</asp:ListItem>
                </asp:DropDownList></TD><TD style="WIDTH: 3px"><asp:CheckBox id="chkOut" onclick="ChkOutChanged()" runat="server" Text="Out Time" CssClass="textlevelshort" Width="70px"></asp:CheckBox></TD><TD style="WIDTH: 4px"><asp:DropDownList id="ddlOutHour" runat="server" Width="45px" OnSelectedIndexChanged="ddlOutHour_SelectedIndexChanged">
                  <%--onchange="ValidateIsNextDay();"--%>
                  <asp:ListItem Value="1">01</asp:ListItem>
                  <asp:ListItem Value="02">02</asp:ListItem>
                  <asp:ListItem Value="03">03</asp:ListItem>
                  <asp:ListItem Value="04">04</asp:ListItem>
                  <asp:ListItem Value="05">05</asp:ListItem>
                  <asp:ListItem Value="06">06</asp:ListItem>
                  <asp:ListItem Value="07">07</asp:ListItem>
                  <asp:ListItem Value="08">08</asp:ListItem>
                  <asp:ListItem Value="09">09</asp:ListItem>
                  <asp:ListItem>10</asp:ListItem>
                  <asp:ListItem>11</asp:ListItem>
                  <asp:ListItem>12</asp:ListItem>
                  <asp:ListItem>13</asp:ListItem>
                  <asp:ListItem>14</asp:ListItem>
                  <asp:ListItem>15</asp:ListItem>
                  <asp:ListItem>16</asp:ListItem>
                  <asp:ListItem>17</asp:ListItem>
                  <asp:ListItem>18</asp:ListItem>
                  <asp:ListItem>19</asp:ListItem>
                  <asp:ListItem>20</asp:ListItem>
                  <asp:ListItem>21</asp:ListItem>
                  <asp:ListItem>22</asp:ListItem>
                  <asp:ListItem>23</asp:ListItem>
                  <asp:ListItem>24</asp:ListItem>
                </asp:DropDownList></TD><TD style="WIDTH: 3px"><asp:DropDownList id="ddlOutMin" runat="server" Width="45px" onchange="ValidateIsNextDay()">
                  <asp:ListItem Value="00">00</asp:ListItem>
                  <asp:ListItem>01</asp:ListItem>
                  <asp:ListItem>02</asp:ListItem>
                  <asp:ListItem>03</asp:ListItem>
                  <asp:ListItem>04</asp:ListItem>
                  <asp:ListItem>05</asp:ListItem>
                  <asp:ListItem>06</asp:ListItem>
                  <asp:ListItem>07</asp:ListItem>
                  <asp:ListItem>08</asp:ListItem>
                  <asp:ListItem>09</asp:ListItem>
                  <asp:ListItem>10</asp:ListItem>
                  <asp:ListItem>11</asp:ListItem>
                  <asp:ListItem>12</asp:ListItem>
                  <asp:ListItem>13</asp:ListItem>
                  <asp:ListItem>14</asp:ListItem>
                  <asp:ListItem>15</asp:ListItem>
                  <asp:ListItem>16</asp:ListItem>
                  <asp:ListItem>17</asp:ListItem>
                  <asp:ListItem>18</asp:ListItem>
                  <asp:ListItem>19</asp:ListItem>
                  <asp:ListItem>20</asp:ListItem>
                  <asp:ListItem>21</asp:ListItem>
                  <asp:ListItem>22</asp:ListItem>
                  <asp:ListItem>23</asp:ListItem>
                  <asp:ListItem>24</asp:ListItem>
                  <asp:ListItem>25</asp:ListItem>
                  <asp:ListItem>26</asp:ListItem>
                  <asp:ListItem>27</asp:ListItem>
                  <asp:ListItem>28</asp:ListItem>
                  <asp:ListItem>29</asp:ListItem>
                  <asp:ListItem>30</asp:ListItem>
                  <asp:ListItem>31</asp:ListItem>
                  <asp:ListItem>32</asp:ListItem>
                  <asp:ListItem>33</asp:ListItem>
                  <asp:ListItem>34</asp:ListItem>
                  <asp:ListItem>35</asp:ListItem>
                  <asp:ListItem>36</asp:ListItem>
                  <asp:ListItem>37</asp:ListItem>
                  <asp:ListItem>38</asp:ListItem>
                  <asp:ListItem>39</asp:ListItem>
                  <asp:ListItem>40</asp:ListItem>
                  <asp:ListItem>41</asp:ListItem>
                  <asp:ListItem>42</asp:ListItem>
                  <asp:ListItem>43</asp:ListItem>
                  <asp:ListItem>44</asp:ListItem>
                  <asp:ListItem>45</asp:ListItem>
                  <asp:ListItem>46</asp:ListItem>
                  <asp:ListItem>47</asp:ListItem>
                  <asp:ListItem>48</asp:ListItem>
                  <asp:ListItem>49</asp:ListItem>
                  <asp:ListItem>50</asp:ListItem>
                  <asp:ListItem>51</asp:ListItem>
                  <asp:ListItem>52</asp:ListItem>
                  <asp:ListItem>53</asp:ListItem>
                  <asp:ListItem>54</asp:ListItem>
                  <asp:ListItem>55</asp:ListItem>
                  <asp:ListItem>56</asp:ListItem>
                  <asp:ListItem>57</asp:ListItem>
                  <asp:ListItem>58</asp:ListItem>
                  <asp:ListItem>59</asp:ListItem>
                </asp:DropDownList></TD><TD style="WIDTH: 3px"><asp:CheckBox id="chkIsNextDay" runat="server" Text="Next day" CssClass="textlevel" Width="70px"></asp:CheckBox></TD><TD style="WIDTH: 3px"><asp:CheckBox id="chkStatus" onclick="ChkStatusChanged()" runat="server" Text="Status" CssClass="textlevel" Width="70px"></asp:CheckBox></TD><TD style="WIDTH: 3px"><asp:DropDownList id="ddlStatus" runat="server" Width="100px"><asp:ListItem Value="-1">Select</asp:ListItem>
<asp:ListItem Value="W">Weekend</asp:ListItem>
<asp:ListItem Value="H">Holiday</asp:ListItem>
<asp:ListItem Value="A">Absent</asp:ListItem>
<asp:ListItem Value="WD">Working Day</asp:ListItem>
</asp:DropDownList></TD><TD style="WIDTH: 3px"></TD><TD style="WIDTH: 70px"></TD><TD style="WIDTH: 3px"><asp:CheckBox id="chkShift" onclick="ChkShiftChanged()" runat="server" Text="Set Shift As" CssClass="textlevel" OnCheckedChanged="chkShift_CheckedChanged"></asp:CheckBox></TD><TD style="WIDTH: 3px"><asp:DropDownList id="ddlShift" runat="server" Width="100px"> </asp:DropDownList></TD><TD style="WIDTH: 3px"></TD></TR><TR><TD style="WIDTH: 3px"><asp:Label id="Label5" runat="server" Text="Remarks" CssClass="textlevelshort" Width="60px"></asp:Label></TD><TD colSpan=10><asp:TextBox id="txtRemarks" runat="server" Width="580px"></asp:TextBox></TD><TD style="WIDTH: 3px"></TD><TD colSpan=2><asp:Button id="btnAdd" onclick="btnAdd_Click" runat="server" Text="Add" Width="120px" OnClientClick="ValidateIsNextDay();"></asp:Button></TD></TR></TBODY></TABLE></FIELDSET> </DIV><DIV class="Grid650"><asp:GridView id="grAttnAdj" runat="server" Width="1640px" Font-Size="9px" EmptyDataText="No Record Found" DataKeyNames="SL,SunIn,SunOut,ArvlGrace,AttnPolicyId,LunchBreak,OTStartGrace,IsUpdated" AutoGenerateColumns="False">
        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
        <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
        <AlternatingRowStyle BackColor="#EFF3FB" />
        <Columns>
        <asp:TemplateField HeaderText="Select">
          <ItemStyle CssClass="ItemStylecss" />
          <ItemTemplate>
            <asp:CheckBox ID="chkBox" runat="Server" > </asp:CheckBox>
          </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="EmpId" HeaderText="Emp.No">
          <ItemStyle CssClass="ItemStylecss" Width="80px" />
        </asp:BoundField>
        <asp:BoundField DataField="FullName" HeaderText="Employee Name">
          <ItemStyle CssClass="ItemStylecss" Width="160px" />
        </asp:BoundField>
        <asp:BoundField DataField="JobTitle" HeaderText="Designation">
          <ItemStyle CssClass="ItemStylecss" Width="120px" />
        </asp:BoundField>
        <asp:BoundField DataField="DeptName" HeaderText="Department">
          <ItemStyle CssClass="ItemStylecss" Width="140px" />
        </asp:BoundField>
        <asp:BoundField DataField="AttndDate" HeaderText="Date">
          <ItemStyle CssClass="ItemStylecss" Width="80px" />
        </asp:BoundField>
        <asp:BoundField DataField="SignInTime" HeaderText="In Time">
          <ItemStyle CssClass="ItemStylecss" Width="140px" />
        </asp:BoundField>
        <asp:BoundField DataField="InLocation" HeaderText="In Loc.">
          <ItemStyle CssClass="ItemStylecss" Width="80px" />
        </asp:BoundField>
        <asp:BoundField DataField="SignOutTime" HeaderText="Out Time">
          <ItemStyle CssClass="ItemStylecss" Width="140px" />
        </asp:BoundField>
        <asp:BoundField DataField="OutLocation" HeaderText="Out Loc.">
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
        <asp:BoundField DataField="Remarks" HeaderText="Remarks">
          <ItemStyle CssClass="ItemStylecss" Width="180px" />
        </asp:BoundField>
        <asp:BoundField DataField="PolicyName" HeaderText="Shift Held">
          <ItemStyle CssClass="ItemStylecss" Width="80px" />
        </asp:BoundField>
        <asp:BoundField DataField="ChangedShift" HeaderText="Changed Shift">
          <ItemStyle CssClass="ItemStylecss" Width="80px" />
        </asp:BoundField>
        <asp:BoundField DataField="CardNo" HeaderText="CardNo">
          <ItemStyle CssClass="ItemStylecss" Width="80px" />
        </asp:BoundField>
        <asp:BoundField DataField="isUpdatedManually" HeaderText="Is Manual">
          <ItemStyle CssClass="ItemStylecss" Width="80px" />
        </asp:BoundField>
        <asp:BoundField DataField="ExtraTimeWorked" HeaderText="OT(Min.)">
          <ItemStyle CssClass="ItemStylecss" Width="60px" />
        </asp:BoundField>
        </Columns>
      </asp:GridView> </DIV><DIV style="MARGIN-LEFT: 15px" id="Div1"><A id="A1" onclick="javascript: selectAllNone('<%= this.grAttnAdj.ClientID %>',true)" href="#">Select All</A> <A id="A2" onclick="javascript: selectAllNone('<%= this.grAttnAdj.ClientID %>',false)" href="#">Clear All</A> </DIV>
</ContentTemplate>
      <Triggers>
<asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
      </asp:UpdatePanel>
      <div id="DivCommand1">
        <table>
          <tr>
            <td><asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px"  CausesValidation="False" OnClick="btnRefresh_Click" /></td>
            <td align="right"><asp:Button ID="btnSave" runat="server" Text="Adjust Attendance" Width="120px" OnClick="btnSave_Click"  /></td>
          </tr>
        </table>
      </div>
    </div>
</asp:Content>
