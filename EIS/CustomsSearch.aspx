<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="CustomsSearch.aspx.cs" Inherits="EIS_CustomsSearch" Title="Custom Search Engine" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js"></script>

    <script language="javascript" type="text/javascript">
function printDiv(divName) 
{
     var printContents = document.getElementById(divName).innerHTML;
     var originalContents = document.body.innerHTML;

     document.body.innerHTML = printContents;

     window.print();

     document.body.innerHTML = originalContents;
}
    </script>

    <div class="formStyle">
        <%--<asp:UpdatePanel runat="server">
            <contenttemplate>--%>
        <div id="formhead2">
        <div style="width: 98%; float: left;">
                Customs Search Engine</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
            </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" Width="500px" Font-Bold="True" ForeColor="Fuchsia"></asp:Label>
        </div>
        <div class="Div950">
            <fieldset>
                <table style="width: 100%;">
                    <tr>
                        <td valign="top" style="width: 60%">
                            <asp:TextBox ID="txtQuery" runat="server" Width="95%" TextMode="MultiLine" Rows="5"></asp:TextBox>
                        </td>
                        <td valign="top" style="width: 38%;">
                            <asp:Button ID="btnVWEmployeeReport" runat="server" Width="130px" Text="Report Head"
                                OnClick="btnVWEmployeeReport_Click" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnExecute" runat="server" Text="Execute Query" Width="130px" OnClick="btnExecute_Click" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="130px" OnClick="btnRefresh_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <table style="border-collapse: collapse; border-bottom: solid 1px Orange;" width="100%;">
                    <tr>
                        <td style="width: 30%; text-align: left;">
                            <asp:LinkButton ID="btnExportToWord" runat="server" OnClick="btnExportToWord_Click">Export to Word File</asp:LinkButton>
                        </td>
                        <td style="width: 30%; text-align: center;">
                            <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click">Export to Excel File</asp:LinkButton>
                        </td>
                        <td>
                            <asp:Label ID="lblTotal" runat="server" ForeColor="green" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="width: 30%; text-align: right;">
                            <asp:Button ID="btnPrintTop" runat="server" Text="Print" OnClientClick="printDiv('PrintMe')"
                                Width="100px" />
                        </td>
                    </tr>
                </table>
                <div id="PrintMe" style="width: 99.99%; page-break-before: always;">
                    <asp:GridView ID="grResult" runat="server" EmptyDataText="No Record Found" Font-Size="9px"
                        Width="99.99%" Font-Names="Arial" ShowFooter="True">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <FooterStyle BackColor="#B3CDE4" Font-Bold="True"></FooterStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
        <asp:Panel Style="display: none" ID="PnlEmpSearch" runat="server" Width="650px" Height="550px"
            CssClass="modalPopup">
            <div class="DivCommand1">
                <div class="DivCommandL" style="margin-top: 10px;">
                    <table>
                        <tr>
                            <td class="textlevel">
                                Employee Status :</td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="150px" CssClass="textlevelleft">
                                    <asp:ListItem Value="AI">All</asp:ListItem>
                                    <asp:ListItem Value="A">Active</asp:ListItem>
                                    <asp:ListItem Value="I">In-Active</asp:ListItem>
                                </asp:DropDownList></td>
                            <td>
                                <asp:Button ID="btnSelect" runat="server" Text="Select" Height="20px" ForeColor="#3366CC"
                                    OnClick="btnSelect_Click"></asp:Button></td>
                        </tr>
                    </table>
                </div>
                <div class="DivCommandR">
                    <div style="margin-top: 5px; vertical-align: middle; height: 20px; text-align: right">
                        <asp:ImageButton ID="imgbtnClose" runat="server" Height="20px" ImageUrl="~/Images/Close3.jpg"
                            ImageAlign="AbsMiddle"></asp:ImageButton>
                    </div>
                </div>
            </div>
            <div style="font-weight: bold; font-size: 13px; background-image: url(../../Images/orengeBG.jpg);
                color: white; background-repeat: repeat-x; height: 20px">
                PaySlip Details Record
            </div>
            <div style="width: 98%; background-color: #eff3fb; text-align: center">
                <asp:Label ID="lblSchema" runat="server" Text=""></asp:Label>
            </div>
            <!--Grid view Code starts-->
            <div style="margin-top: 10px; margin-left: 10px; overflow: scroll; width: 98%; height: 460px;
                text-align: center">
                <asp:GridView ID="grColumns" runat="server" Font-Size="9px" AutoGenerateColumns="False"
                    DataKeyNames="" EmptyDataText="No Record Found">
                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                    <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                    <AlternatingRowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemStyle CssClass="ItemStylecss" Width="5%" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkBox" runat="Server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Column_Name" HeaderText="Column Name">
                            <ItemStyle CssClass="ItemStylecss" Width="70%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Type" HeaderText="Type">
                            <ItemStyle CssClass="ItemStylecss" Width="25%" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </asp:Panel>
        <asp:LinkButton ID="lnkButton1" runat="server" Font-Underline="false" Font-Size="10px"
            ForeColor="Black"></asp:LinkButton>
        <cc1:ModalPopupExtender ID="ModalPopupTree" runat="server" TargetControlID="lnkButton1"
            PopupControlID="PnlEmpSearch" Enabled="True" DynamicServicePath="" DropShadow="True"
            CancelControlID="imgbtnClose" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <%--</contenttemplate>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>
