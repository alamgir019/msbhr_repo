<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="PayrollAudit.aspx.cs" Inherits="Payroll_Payroll_PayrollAudit" Title="Payroll Audit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js"></script>

    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js"></script>

    <script language="javascript" type="text/javascript">
  window.onload=SearchByChanged;

        function SearchByChanged()
        {
            var ddlSB = document.getElementById('<%= ddlGeneratefor.ClientID %>');
            var ddlB=document.getElementById('<%=ddlBank.ClientID%>');
            
            var myindex  = ddlSB.selectedIndex;
            var SelValue = ddlSB.options[myindex].value;
            
           if(SelValue=="A")
            {  
                ddlB.disabled=true;          
            }
            if(SelValue=="B")
            {  
               ddlB.disabled=false;   
            }  
         }
         
         function ToUpper(ctrl)
         {   
            var t = ctrl.value;
            ctrl.value = t.toUpperCase(); 
         }


        function printDiv(divName) 
        {
             var printContents = document.getElementById(divName).innerHTML;
             var originalContents = document.body.innerHTML;

             document.body.innerHTML = printContents;

             window.print();

             document.body.innerHTML = originalContents;
        }


    </script>

    <div class="formStyle" style="width: 95%;">
        <div id="formhead1">
            <div style="width: 98%; float: left;">
                Second Review</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Fuchsia"></asp:Label>
        </div>
        <div class="Div950">
            <div style="background-color: #EFF3FB; border: solid 1px #3E506C;">
                <table>
                    <tr>
                        <td class="textlevel">
                            Generate For :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlGeneratefor" runat="server" Width="130px" CssClass="textlevelleft"
                                onchange="SearchByChanged()">
                                <asp:ListItem Value="A">All</asp:ListItem>
                                <asp:ListItem Value="B">Bank Wise</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlBank" runat="server" Width="270px" CssClass="textlevelleft">
                            </asp:DropDownList></td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" runat="server" Width="100px" CssClass="textlevelleft">
                            </asp:DropDownList></td>
                        <td class="textlevel">
                            <asp:DropDownList ID="ddlYear" runat="server" Width="80px" CssClass="textlevelleft">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                        </td>
                        <td>
                            <%--<asp:DropDownList ID="ddlReviewFor" runat="server" Width="130px" CssClass="textlevelleft">
                                <asp:ListItem Value="S">Payroll</asp:ListItem>
                                <asp:ListItem Value="B">Bonus</asp:ListItem>
                            </asp:DropDownList>--%>
                            <asp:CheckBox ID="chkBonus" runat="server" Text="Bonus Only" CssClass="textlevelleft" />
                        </td>
                        <td style="background-color: #FF9933;">
                            <asp:Label ID="lblGroup" runat="server" Text="Select Group" Width="258px" ForeColor="Navy"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlGroup" runat="server" Width="270px" CssClass="textlevelleft">
                            </asp:DropDownList></td>
                        <td colspan="2">
                            <asp:Button ID="btnGenerate" runat="server" Text="Get Payroll Data" Width="184px"
                                OnClick="btnGenerate_Click" /></td>
                    </tr>
                </table>
            </div>
            <hr style="border: solid 1px #3399FF;" />
            <div style="height: auto;">
                <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1" Width="100%"
                    Style="height: auto;">
                    <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Width="100%">
                        <HeaderTemplate>
                            Payroll for Review
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div style="text-align: left;">
                                <asp:Button ID="btnPrintTop" runat="server" Text="Print" OnClientClick="printDiv('PrintMe')"
                                    Width="100px" /></div>
                            <div style="border: solid 1px #3E506C;">
                                <div id="PrintMe" style="width: 99.99%; page-break-before: always;">
                                    <table width="100%" style="border-collapse: collapse; border: solid 1px white;">
                                        <tr>
                                            <td style="width: 40%; text-align: left; font-family: Arial; font-size: 20px; padding-left: 5px;">
                                                Marie Stopes
                                            </td>
                                            <td rowspan="3" style="width: 60%;" valign="top">
                                                <table width="60%" style="border-collapse: collapse; border: solid 1px DimGray;">
                                                    <tr>
                                                        <td style="font-family: Arial; font-size: 12px; background-color: #C6D0D3; font-weight: bold;">
                                                            Prepared By</td>
                                                        <td style="font-family: Arial; font-size: 12px; background-color: #E7C4A4; font-weight: bold;">
                                                            First Review By</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-family: Arial; font-size: 12px;">
                                                            <asp:Label ID="lblPreparedBy" runat="server"></asp:Label></td>
                                                        <td style="font-family: Arial; font-size: 12px;">
                                                            <asp:Label ID="lblReviewedBy" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-family: Arial; font-size: 12px; background-color: #C6D0D3; font-weight: bold;">
                                                            Forwarded Date</td>
                                                        <td style="font-family: Arial; font-size: 12px; background-color: #E7C4A4; font-weight: bold;">
                                                            Checked Date</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-family: Arial; font-size: 12px;">
                                                            <asp:Label ID="lblPreparedDate" runat="server"></asp:Label></td>
                                                        <td style="font-family: Arial; font-size: 12px;">
                                                            <asp:Label ID="lblReviewDate" runat="server"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%; font-family: Arial; font-size: 14px; padding-left: 5px;">
                                                <asp:Label ID="lblGenerateFor" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-family: Arial; font-size: 14px; padding-left: 5px;">
                                                <asp:Label ID="lblPayrollMonth" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <asp:GridView ID="grReviewList" runat="server" EmptyDataText="No Record Found" Font-Size="9px"
                                        Width="100%" Font-Names="Arial" ShowFooter="True">
                                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                        <FooterStyle BackColor="#B3CDE4" Font-Bold="True"></FooterStyle>
                                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div id="DivCommand1" style="padding-top: 3px;">
                                <div style="text-align: left; float: left">
                                    <asp:Button ID="btnPrintBottom" runat="server" Text="Print" OnClientClick="printDiv('PrintMe')"
                                        Width="100px" />
                                </div>
                                <div style="text-align: right;">
                                    <asp:Button ID="btnAudited" runat="server" Text="Click to Complete Review" Width="200px"
                                        OnClick="btnAudited_Click" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel2">
                        <HeaderTemplate>
                            Reviewed Payroll
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div style="text-align: left;">
                                <asp:Button ID="btnPrintTab3" runat="server" Text="Print" OnClientClick="printDiv('PrintMeTab3')"
                                    Width="100px" /></div>
                            <div style="border: solid 1px #3E506C;">
                                <div id="PrintMeTab3" style="width: 99.99%; page-break-before: always;">
                                    <table width="100%" style="border-collapse: collapse; border: solid 1px white;">
                                        <tr>
                                            <td style="width: 40%; text-align: left; font-family: Arial; font-size: 20px; padding-left: 5px;">
                                                Marie Stopes
                                            </td>
                                            <td rowspan="3" style="width: 60%;" valign="top">
                                                <table width="100%" style="border-collapse: collapse; border: solid 1px DimGray;">
                                                    <tr>
                                                        <td style="font-family: Arial; font-size: 12px; background-color: #C6D0D3; font-weight: bold;">
                                                            Prepared By</td>
                                                        <td style="font-family: Arial; font-size: 12px; background-color: #E7C4A4; font-weight: bold;">
                                                            First Review By</td>
                                                        <td style="font-family: Arial; font-size: 12px; background-color: #6AADD3; font-weight: bold;">
                                                            Second Review By</td>
                                                        <td style="font-family: Arial; font-size: 12px; background-color: #99BB7D; font-weight: bold;">
                                                            Approved By</td>
                                                        <td style="font-family: Arial; font-size: 11px; background-color: #F6A74B; font-weight: bold;">
                                                            Disbursed By</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-family: Arial; font-size: 12px;">
                                                            <asp:Label ID="lblPreparedByTab2" runat="server"></asp:Label></td>
                                                        <td style="font-family: Arial; font-size: 12px;">
                                                            <asp:Label ID="lblReviewedByTab2" runat="server"></asp:Label></td>
                                                        <td style="font-family: Arial; font-size: 12px;">
                                                            <asp:Label ID="lblCheckedByTab2" runat="server"></asp:Label></td>
                                                        <td style="font-family: Arial; font-size: 12px;">
                                                            <asp:Label ID="lblApprovedByTab2" runat="server"></asp:Label></td>
                                                        <td style="font-family: Arial; font-size: 11px;">
                                                            <asp:Label ID="lblDisburseByTab2" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-family: Arial; font-size: 12px;">
                                                            <asp:Label ID="lblPreparedDateTab2" runat="server"></asp:Label></td>
                                                        <td style="font-family: Arial; font-size: 12px;">
                                                            <asp:Label ID="lblReviewDateTab2" runat="server"></asp:Label></td>
                                                        <td style="font-family: Arial; font-size: 12px;">
                                                            <asp:Label ID="lblCheckDateTab2" runat="server"></asp:Label></td>
                                                        <td style="font-family: Arial; font-size: 12px;">
                                                            <asp:Label ID="lblApproveDateTab2" runat="server"></asp:Label></td>
                                                        <td style="font-family: Arial; font-size: 11px;">
                                                            <asp:Label ID="lblDisburseDatetab2" runat="server"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%; font-family: Arial; font-size: 14px; padding-left: 5px;">
                                                <asp:Label ID="lblGenerateForTab2" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-family: Arial; font-size: 14px; padding-left: 5px;">
                                                <asp:Label ID="lblPayrollMonthTab2" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <asp:GridView ID="grAuditList" runat="server" EmptyDataText="No Record Found" Font-Size="9px"
                                        Width="100%" Font-Names="Arial" ShowFooter="True">
                                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                        <FooterStyle BackColor="#B3CDE4" Font-Bold="True"></FooterStyle>
                                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </div>
        </div>
    </div>
</asp:Content>
