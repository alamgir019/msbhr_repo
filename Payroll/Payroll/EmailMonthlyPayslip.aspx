<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="EmailMonthlyPayslip.aspx.cs" Inherits="Payroll_Payroll_EmailMonthlyPayslip"
    Title="Email Monthly Payslip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
     window.onload = SearchByChanged;

        function SearchByChanged()
        {
            var ddlSB = document.getElementById('<%= ddlGeneratefor.ClientID %>');
            var ddlSV=document.getElementById('<%=ddlGenerateValue.ClientID%>');
            var ddlB=document.getElementById('<%=ddlBank.ClientID%>');
            var txtVT=document.getElementById('<%=txtTextValue.ClientID%>');
            var txtlbl=document.getElementById('<%=lblEmpID.ClientID%>');
            
            var myindex  = ddlSB.selectedIndex;
            var SelValue = ddlSB.options[myindex].value;
            
            if(SelValue=="O")
            {   
                ddlSV.disabled=false;
                ddlB.disabled=true;  
                txtVT.disabled =true;      
                txtlbl.disabled =true;               
            }
            if(SelValue=="E")
            {  
                ddlSV.disabled=true;
                ddlB.disabled=true;  
                txtVT.disabled =false;
                txtlbl.disabled =false;          
            }   
           if(SelValue=="A")
            {  
                ddlSV.disabled=true;
                txtVT.disabled =true;
                txtlbl.disabled =true;
                ddlB.disabled=true;          
            }
            if(SelValue=="B")
            {  
                 ddlSV.disabled=true;
                 ddlB.disabled=false;
                 txtVT.disabled =true;      
                 txtlbl.disabled =true;        
            }  
         }
         
         function ToUpper(ctrl)
         {   
            var t = ctrl.value;
            ctrl.value = t.toUpperCase(); 
         }
         
        function CheckBoxListSelect(cbControl, state)
        {   
           var chkBoxList = document.getElementById(cbControl);
           var chkBoxCount= chkBoxList.getElementsByTagName("input");
            for(var i=0;i<chkBoxCount.length;i++)
            {
                chkBoxCount[i].checked = state;
            }
            return false; 
        }
    </script>

    <div class="formStyle">
        <div id="formhead1">
            <div style="width: 98%; float: left;">
                Email Payslip</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" Width="500px" Font-Bold="True" ForeColor="Fuchsia"></asp:Label>
        </div>
        <div class="Div950">
            <fieldset>
                <table>
                    <tr>
                        <td class="textlevel">
                            Generate For :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlGeneratefor" runat="server" Width="130px" CssClass="textlevelleft"
                                onchange="SearchByChanged()">
                                <asp:ListItem Value="A">All</asp:ListItem>
                                <asp:ListItem Value="O">Location Wise</asp:ListItem>
                                <asp:ListItem Value="B">Bank Wise</asp:ListItem>
                                <asp:ListItem Value="E">Employee Wise</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="">
                            <asp:DropDownList ID="ddlGenerateValue" runat="server" Width="270px" CssClass="textlevelleft">
                            </asp:DropDownList>&nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" runat="server" Width="100px" onchange="MonthChanged()"
                                CssClass="textlevelleft">
                            </asp:DropDownList></td>
                        <td class="textlevelauto">
                            <asp:DropDownList ID="ddlYear" runat="server" Width="80px" CssClass="textlevelleft">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEmpID" runat="server" Text="Emp. ID" CssClass="textlevel"></asp:Label>
                        </td>
                        <td style="font-size: 11px">
                            <asp:TextBox ID="txtTextValue" runat="server" Width="80px" onkeyup="ToUpper(this)"></asp:TextBox></td>
                        <td>
                            <asp:DropDownList ID="ddlBank" runat="server" Width="270px" CssClass="textlevelleft">
                            </asp:DropDownList></td>
                        <td>
                            <asp:CheckBox ID="chkBonus" runat="server" Text="Bonus Only" CssClass="textlevelleft" /></td>
                        <td>
                            <asp:Button ID="btnGetEmployee" runat="server" Text="Get Payslip Employee" Width="150px"
                                OnClick="btnGetEmployee_Click" /></td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend>Employee List</legend>Clear <a id="A2" href="#" onclick="javascript: CheckBoxListSelect ('<%= grEmployee.ClientID %>',false)">
                    All</a>
                <div style="width: 99.99%;">
                    <asp:GridView ID="grEmployee" runat="server" EmptyDataText="No Record Found" Font-Size="9px"
                        Width="98%" AutoGenerateColumns="False" DataKeyNames="EMPID" OnRowCommand="grEmployee_RowCommand">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemStyle CssClass="ItemStylecss" Width="2%" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkBox" runat="Server" Checked="true"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="" HeaderText="SL">
                                <ItemStyle CssClass="ItemStylecss" Width="3%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="EMPID" HeaderText="Emp. ID">
                                <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FULLNAME" HeaderText="Employee Name">
                                <ItemStyle CssClass="ItemStylecss" Width="17%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PostingPlaceName" HeaderText="Posting Place">
                                <ItemStyle CssClass="ItemStylecss" Width="16%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JobTitleName" HeaderText="Job Title">
                                <ItemStyle CssClass="ItemStylecss" Width="16%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SPTITLE" HeaderText="Salary Package">
                                <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PersonalEmail" HeaderText="Email ID">
                                <ItemStyle CssClass="ItemStylecss" Width="19%"></ItemStyle>
                            </asp:BoundField>
                            <asp:ButtonField HeaderText="View" Text="Payslip" CommandName="ViewClick">
                                <ItemStyle Width="5%" CssClass="ItemStylecss"></ItemStyle>
                            </asp:ButtonField>
                        </Columns>
                    </asp:GridView>
                </div>
                Clear <a id="A4" href="#" onclick="javascript: CheckBoxListSelect ('<%= grEmployee.ClientID %>',false)">
                    All</a>
            </fieldset>
            <div style="text-align: center;">
                <asp:Button ID="btnSendEmail" runat="server" Text="Send Email" Width="120px" OnClick="btnSendEmail_Click" />
            </div>
            <%--<fieldset>--%>
            <table style="width: 100%;">
                <tr>
                    <td colspan="2" style="font-size: 12px; font-family: Arial;">
                        <br />
                        <br />
                        Note: This is a computer generated payslip and does not require any signature. If
                        any discrepancy is found please inform HR Team within 7 days of the issuance.
                    </td>
                </tr>
                <tr>
                    <td style="width: 49%; vertical-align: top;">
                        <asp:GridView ID="grGrossandBenefits" runat="server" AutoGenerateColumns="False"
                            ShowHeader="true" EmptyDataText="No Record Found" Font-Size="12px" DataKeyNames="HTYPE"
                            Font-Names="Arial" Width="99%">
                            <HeaderStyle BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray" />
                            <RowStyle Font-Size="12px" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray" />
                            <Columns>
                                <asp:BoundField DataField="HeadName" HeaderText="Salary Items" HeaderStyle-BorderWidth="1px"
                                    HeaderStyle-BorderStyle="solid" HeaderStyle-BorderColor="dimgray" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="70%" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PAYAMT" HeaderText="Taka" HeaderStyle-HorizontalAlign="right"
                                    HeaderStyle-BorderWidth="1px" HeaderStyle-BorderStyle="solid" HeaderStyle-BorderColor="dimgray">
                                    <ItemStyle Width="30%" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray"
                                        HorizontalAlign="right" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td style="width: 49%; vertical-align: top;">
                        <asp:GridView ID="grDeduct" runat="server" AutoGenerateColumns="False" ShowHeader="true"
                            Font-Size="12px" DataKeyNames="HTYPE" Font-Names="Arial" Width="99%" Visible="false">
                            <HeaderStyle BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray" />
                            <RowStyle Font-Size="12px" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray" />
                            <Columns>
                                <asp:BoundField DataField="HeadName" HeaderText="Deduction(s)" HeaderStyle-BorderWidth="1px"
                                    HeaderStyle-BorderStyle="solid" HeaderStyle-BorderColor="dimgray" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="70%" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PayAmt" HeaderText="Taka" HeaderStyle-HorizontalAlign="right"
                                    HeaderStyle-BorderWidth="1px" HeaderStyle-BorderStyle="solid" HeaderStyle-BorderColor="dimgray">
                                    <ItemStyle Width="30%" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray"
                                        HorizontalAlign="right" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <br />
                        <asp:GridView ID="grNetPay" Visible="false" runat="server" AutoGenerateColumns="False"
                            ShowHeader="false" Font-Size="12px" Font-Names="Arial" Width="99%">
                            <HeaderStyle BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray" />
                            <RowStyle Font-Size="12px" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray" />
                            <Columns>
                                <asp:BoundField DataField="HeadName" HeaderText="Deduction(s)" HeaderStyle-BorderWidth="1px"
                                    HeaderStyle-BorderStyle="solid" HeaderStyle-BorderColor="dimgray" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="70%" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray"
                                        Font-Bold="true" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PayAmt" HeaderText="Taka" HeaderStyle-HorizontalAlign="right"
                                    HeaderStyle-BorderWidth="1px" HeaderStyle-BorderStyle="solid" HeaderStyle-BorderColor="dimgray">
                                    <ItemStyle Width="30%" BorderWidth="1px" BorderStyle="solid" BorderColor="dimgray"
                                        HorizontalAlign="right" Font-Bold="true" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <asp:Label ID="lblRemarks" runat="server" Font-Names="Tahoma" Font-Size="12px"></asp:Label></td>
                </tr>
            </table>
            <%--</fieldset>--%>
        </div>
    </div>
</asp:Content>
