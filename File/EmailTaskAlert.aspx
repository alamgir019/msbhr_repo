<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" 
CodeFile="EmailTaskAlert.aspx.cs" Inherits="File_EmailTaskAlert" Title="Email Task Alert" ValidateRequest="false" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script language ="javascript" type="text/javascript" src ="../JScripts/Confirmation.js">
</script>
<script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js">
    //Date Time Picker script
</script>
<script language="javascript" type="text/javascript">
    function ToUpper(ctrl)
    {   
    var t = ctrl.value;
    ctrl.value = t.toUpperCase(); 
    }
    //Function to checked or unchecked all gridview items  
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
          }
       }  
    }
</script>

<div class="leaveApplicaionStyle">    
    <div id="formhead1"> Alert Memo</div>
    <div class="MsgBox">
      <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
    </div>    
    <div id="leaveApplicaionFormInner">
        <cc1:TabContainer ID="TabContainer1" runat="server"  Height="620px" ActiveTabIndex="0">
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" >
            <HeaderTemplate> Alert Memo</HeaderTemplate>
            <ContentTemplate>
              <div class="Div900">
                <fieldset>
                <asp:UpdatePanel id="UpdatePanel1" runat="server">
                <contenttemplate>
<TABLE width="98%"><TBODY><TR><TD style="WIDTH: 10%" class="textlevel">To</TD><TD style="WIDTH: 80%"><asp:TextBox id="txtTo" runat="server" Height="30px" Width="98%" __designer:wfdid="w65" AutoPostBack="True" TextMode="MultiLine">
</asp:TextBox></TD><TD style="WIDTH: 5%"><asp:Button id="btnTo" onclick="btnTo_Click" runat="server" Text="..." CausesValidation="False" Width="90%" __designer:wfdid="w66">
</asp:Button></TD><TD style="WIDTH: 3%"></TD></TR><TR><TD style="WIDTH: 10%" class="textlevel">CC</TD><TD style="WIDTH: 80%"><asp:TextBox id="txtCC" runat="server" Height="30px" Width="98%" __designer:wfdid="w67" AutoPostBack="True" TextMode="MultiLine"></asp:TextBox></TD><TD style="WIDTH: 5%"><asp:Button id="btnCC" onclick="btnCC_Click" runat="server" Text="..." CausesValidation="False" Width="90%" __designer:wfdid="w68"></asp:Button></TD><TD style="WIDTH: 3%"></TD></TR><TR><TD style="WIDTH: 10%" class="textlevel">BCC</TD><TD style="WIDTH: 80%"><asp:TextBox id="txtBCC" runat="server" Height="30px" Width="98%" __designer:wfdid="w69" AutoPostBack="True" TextMode="MultiLine"></asp:TextBox></TD><TD style="WIDTH: 5%"><asp:Button id="btnBCC" onclick="btnBCC_Click" runat="server" Text="..." CausesValidation="False" Width="90%" __designer:wfdid="w70"></asp:Button></TD><TD style="WIDTH: 3%"></TD></TR><TR><TD style="WIDTH: 10%" class="textlevel">Subject</TD><TD style="WIDTH: 80%"><asp:TextBox id="txtSubject" runat="server" Height="30px" Width="98%" __designer:wfdid="w71" AutoPostBack="True" TextMode="MultiLine"></asp:TextBox></TD><TD style="WIDTH: 5%"></TD><TD style="WIDTH: 3%"></TD></TR><TR><TD style="WIDTH: 10%" class="textlevel">Attachment</TD><TD style="WIDTH: 80%"><asp:FileUpload id="FileUpload1" runat="server" Width="96%" __designer:wfdid="w72"></asp:FileUpload> </TD><TD style="WIDTH: 5%">&nbsp;</TD><TD style="WIDTH: 3%"></TD></TR><TR><TD style="WIDTH: 10%" class="textlevel"></TD><TD style="WIDTH: 80%"><asp:CheckBox id="chkAttachFile" runat="server" __designer:wfdid="w73" Visible="False"></asp:CheckBox>&nbsp;<asp:CheckBox id="chkGroupAttach" runat="server" ForeColor="Blue" Text="Is Group Attachment" __designer:wfdid="w1"></asp:CheckBox></TD><TD style="WIDTH: 5%"></TD><TD style="WIDTH: 3%"></TD></TR><TR><TD style="WIDTH: 10%" class="textlevel">Task</TD><TD colSpan=3><%--<asp:TextBox id="txtTask" runat="server" Height="200px" Width="96%" TextMode="MultiLine" OnTextChanged="txtTask_TextChanged"></asp:TextBox>--%><FTB:FreeTextBox id="FreetxtTask" runat="server" Height="250px" Width="96%"></FTB:FreeTextBox> </TD></TR></TBODY></TABLE><TABLE width="98%"><TBODY><TR><TD style="WIDTH: 10%" class="textlevel">Alert Date &amp; Time</TD><TD style="WIDTH: 10%"><asp:TextBox id="txtAltertDate" runat="server" Width="98%" __designer:wfdid="w75"></asp:TextBox></TD><TD style="WIDTH: 5%"><A href="javascript:NewCal('<%= txtAltertDate.ClientID %>','ddmmyyyy')"><IMG style="BORDER-RIGHT: 0px; BORDER-TOP: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px" height=16 alt="Pick a date" src="../images/cal.gif" width=16 /></A></TD><TD style="WIDTH: 5%"><asp:DropDownList id="ddlAlertHour" runat="server" Width="45px" __designer:wfdid="w76">
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
                        </asp:DropDownList></TD><TD style="WIDTH: 5%"><asp:DropDownList id="ddlAlertMin" runat="server" Width="45px" __designer:wfdid="w77">
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
                        </asp:DropDownList></TD><TD style="WIDTH: 5%"><asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" __designer:wfdid="w78" ErrorMessage="*" ControlToValidate="txtAltertDate"></asp:RequiredFieldValidator></TD><TD style="WIDTH: 58%"></TD></TR></TBODY></TABLE><asp:Panel style="DISPLAY: none" id="PnlEmpSearch" runat="server" CssClass="modalPopup" Height="350px" Width="700px" __designer:wfdid="w79"><DIV style="BACKGROUND-IMAGE: url(../Images/orengeBG.jpg); COLOR: white; BACKGROUND-REPEAT: repeat-x; HEIGHT: 20px">Employee List</DIV><!--Grid view Code starts--><DIV style="OVERFLOW: scroll; WIDTH: 98%; HEIGHT: 270px"><asp:GridView id="grEmpSearch" runat="server" Width="96%" OnRowCommand="grEmpSearch_RowCommand" DataKeyNames="EmpId,PersEmail1" AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" __designer:wfdid="w80">
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmpId" HeaderText="Emp Id">
                                <ItemStyle CssClass="ItemStylecss" Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FullName" HeaderText="Name">
                                <ItemStyle CssClass="ItemStylecss" Width="30%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PersEmail1" HeaderText="E-mail Address">
                                <ItemStyle CssClass="ItemStylecss" Width="40%" />
                            </asp:BoundField>
                        </Columns>
                        <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                        <AlternatingRowStyle BackColor="#EFF3FB" />
                    </asp:GridView> </DIV><A id="A1" onclick="javascript: selectAllNone('<%= this.grEmpSearch.ClientID %>',true)" href="#">Select All</A> <A id="A2" onclick="javascript: selectAllNone('<%= this.grEmpSearch.ClientID %>',false)" href="#">Clear All</A> <DIV class="DivCommand1"><DIV class="DivCommandL"><asp:Button id="btnSelect" onclick="btnSelect_Click" runat="server" Text="Select" CausesValidation="False" Width="70px" __designer:wfdid="w81"></asp:Button> </DIV><DIV class="DivCommandR"><DIV style="MARGIN-TOP: 5px; VERTICAL-ALIGN: middle; HEIGHT: 20px; BACKGROUND-COLOR: gray; TEXT-ALIGN: right"><asp:ImageButton id="imgbtnClose" runat="server" Height="20px" ImageUrl="~/Images/Close3.jpg" ImageAlign="AbsMiddle" __designer:wfdid="w82"></asp:ImageButton> </DIV></DIV></DIV></asp:Panel> <cc1:ModalPopupExtender id="ModalPopupTree" runat="server" __designer:wfdid="w83" CancelControlID="imgbtnClose" TargetControlID="btnDummy" BackgroundCssClass="modalBackground" Enabled="True" DropShadow="True" DynamicServicePath="" PopupControlID="PnlEmpSearch">
            </cc1:ModalPopupExtender> <asp:HiddenField id="hfAttach" runat="server" __designer:wfdid="w84"></asp:HiddenField> <asp:HiddenField id="hfToAddr" runat="server" __designer:wfdid="w85"></asp:HiddenField> <asp:HiddenField id="hfCCAddr" runat="server" __designer:wfdid="w86"></asp:HiddenField> <asp:HiddenField id="hfBCCAddr" runat="server" __designer:wfdid="w87"></asp:HiddenField> <asp:HiddenField id="hfToEmpID" runat="server" __designer:wfdid="w2"></asp:HiddenField> <asp:HiddenField id="hfAttachPath" runat="server" __designer:wfdid="w3"></asp:HiddenField> <asp:Button id="btnDummy" runat="server" Text="Dummy" CausesValidation="False" Width="70px" __designer:wfdid="w88" Visible="False"></asp:Button>&nbsp; 
</contenttemplate>
       </asp:UpdatePanel>
            <asp:HiddenField ID="hfID" runat="server"/>
            <asp:HiddenField ID="hfIsUpdate" runat="server"/> 
        </fieldset>
        </div>
                
        </ContentTemplate>
    </cc1:TabPanel> 
            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
            <ContentTemplate>
                 <div style="margin:10px 5px 5px 10px;
	                    height:600px; 
	                    width:98%; 
	                    _width:97%;
	                    overflow:scroll;
	                    border: solid 1px gray;">
                <asp:GridView ID="grTaskAlertList" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="TransId,FROMADDR,BCCADDR,BODY,SLNO"
                    EmptyDataText="No Record Found" Font-Size="9px" OnRowCommand="grTaskAlertList_RowCommand"
                    PageSize="7" Width="98%" OnSelectedIndexChanged="grTaskAlertList_SelectedIndexChanged">
                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                    <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                    <AlternatingRowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                            <ItemStyle CssClass="ItemStylecss" Width="2%" />
                        </asp:ButtonField>
                        <asp:BoundField HeaderText="SL No.">
                            <ItemStyle CssClass="ItemStylecss" Width="2%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FromEmpId" HeaderText="Emp No.">
                            <ItemStyle CssClass="ItemStylecssCenter" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TOADDR" HeaderText="To Address">
                            <ItemStyle CssClass="ItemStylecss" Width="30%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CCADDR" HeaderText="CC Address">
                            <ItemStyle CssClass="ItemStylecss" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SUBJECT" HeaderText="Subject">
                            <ItemStyle CssClass="ItemStylecss" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ATTACHMENT" HeaderText="Attachment">
                            <ItemStyle CssClass="ItemStylecss" Width="12%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SCHDATETIME" HeaderText="Alert Date Time">
                            <ItemStyle CssClass="ItemStylecss" Width="18%" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                </div>
            </ContentTemplate>
            <HeaderTemplate>
                Pending Alert List
            </HeaderTemplate>
        </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
                <HeaderTemplate>
                    Completed Alert List
                </HeaderTemplate>
                <ContentTemplate>
                 <div style="margin:10px 5px 5px 10px;
	                    height:600px; 
	                    width:98%; 
	                    _width:97%;
	                    overflow:scroll;
	                    border: solid 1px gray;">
                <asp:GridView ID="grCompletedAlertList" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="TransId,FROMADDR,BCCADDR,BODY"
                    EmptyDataText="No Record Found" Font-Size="9px" OnRowCommand="grCompletedAlertList_RowCommand"
                    PageSize="7" Width="98%" OnSelectedIndexChanged="grCompletedAlertList_SelectedIndexChanged">
                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                    <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                    <AlternatingRowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                            <ItemStyle CssClass="ItemStylecss" Width="2%" />
                        </asp:ButtonField>
                        <asp:BoundField HeaderText="SL No.">
                            <ItemStyle CssClass="ItemStylecss" Width="2%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FromEmpId" HeaderText="Emp No.">
                            <ItemStyle CssClass="ItemStylecssCenter" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TOADDR" HeaderText="To Address">
                            <ItemStyle CssClass="ItemStylecss" Width="30%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CCADDR" HeaderText="CC Address">
                            <ItemStyle CssClass="ItemStylecss" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SUBJECT" HeaderText="Subject">
                            <ItemStyle CssClass="ItemStylecss" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ATTACHMENT" HeaderText="Attachment">
                            <ItemStyle CssClass="ItemStylecss" Width="12%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SCHDATETIME" HeaderText="Alert Date Time">
                            <ItemStyle CssClass="ItemStylecss" Width="18%" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                </div>
            </ContentTemplate>
            </cc1:TabPanel>
</cc1:TabContainer>   
</div>
 
 <div class="DivCommand1">
    <div class="DivCommandL">
        <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" OnClick="btnRefresh_Click"
        Text="Refresh" Width="70px" />
    </div>
    <div class="DivCommandR">
        &nbsp;<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" UseSubmitBehavior="False"
        Width="70px" />
        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="javascript:return DeleteConfirmation();" 
        Text="Delete" Width="70px" />
    </div>
 </div>
</div> 
</asp:Content>

