<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="PayslipApproval.aspx.cs" Inherits="Payroll_Payroll_PayslipApproval"
    Title="Payslip Approval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js"></script>
    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js"></script>
    <script language="javascript" type="text/javascript">
        window.onload = SearchByChanged;

        function SearchByChanged() {
            var ddlSB = document.getElementById('<%= ddlGeneratefor.ClientID %>');
            var ddlSV = document.getElementById('<%=ddlGenerateValue.ClientID%>');
            var ddlB = document.getElementById('<%=ddlBank.ClientID%>');
            var txtVT = document.getElementById('<%=txtTextValue.ClientID%>');
            var txtlbl = document.getElementById('<%=lblEmpID.ClientID%>');

            var myindex = ddlSB.selectedIndex;
            var SelValue = ddlSB.options[myindex].value;

            if (SelValue == "O") {
                ddlSV.disabled = false;
                ddlB.disabled = true;
                txtVT.disabled = true;
                txtlbl.disabled = true;
            }
            if (SelValue == "E") {
                ddlSV.disabled = true;
                ddlB.disabled = true;
                txtVT.disabled = false;
                txtlbl.disabled = false;
            }
            if (SelValue == "A") {
                ddlSV.disabled = true;
                txtVT.disabled = true;
                txtlbl.disabled = true;
                ddlB.disabled = true;
            }
            if (SelValue == "B") {
                ddlSV.disabled = true;
                ddlB.disabled = false;
                txtVT.disabled = true;
                txtlbl.disabled = true;
            }
        }

        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }

        function CheckBoxListSelect(cbControl, state) {
            var chkBoxList = document.getElementById(cbControl);
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }
            return false;
        }    
    </script>
    <div class="formStyle" style="min-height: 340px;">
        <div id="formhead1">
            <div style="width: 98%; float: left;">
                Payroll Correction/Edit</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Fuchsia"></asp:Label>
        </div>
        <div class="Div950" style="min-height: 330px;">
            <div style="background-color: #EFF3FB; border: solid 1px #3E506C;">
                <table>
                    <tr>
                        <td class="textlevel">
                            Generate For :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlGeneratefor" runat="server" Width="130px" onchange="SearchByChanged()"
                                CssClass="textlevelleft" 
                                onselectedindexchanged="ddlGeneratefor_SelectedIndexChanged">
                                <asp:ListItem Value="A">All</asp:ListItem>
                                <asp:ListItem Value="O">Cost Center Wise</asp:ListItem>
                                <asp:ListItem Value="B">Bank Wise</asp:ListItem>
                                <asp:ListItem Value="E">Employee Wise</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlGenerateValue" runat="server" Width="270px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" runat="server" Width="100px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlYear" runat="server" Width="80px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="textlevel">
                            <asp:Label ID="Label1" runat="server" Text="Emp. ID :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTextValue" runat="server" Width="80px" onkeyup="ToUpper(this)"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlBank" runat="server" Width="270px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                            Employee Type :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEmpType" runat="server" Width="130px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnGenerate" runat="server" Text="Get Payroll Prepared Data" Width="184px"
                                OnClick="btnGenerate_Click" />
                        </td>
                        
                    </tr>
                </table>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    Select <a id="A1" href="#" onclick="javascript: CheckBoxListSelect ('<%= grPayslipMst.ClientID %>',true)">
                        All</a> | <a id="A2" href="#" onclick="javascript: CheckBoxListSelect ('<%= grPayslipMst.ClientID %>',false)">
                            None</a>
                    <div style="width: 100%; border: solid 1px #3E506C; margin-top: 5px;">
                        <asp:GridView ID="grPayslipMst" runat="server" EmptyDataText="No Record Found" Font-Size="9px"
                            Width="100%" AutoGenerateColumns="False" DataKeyNames="PSBID,PayID,EMPID,SalPakID,PaySlipStatus"
                            OnRowCommand="grPayslipMst_RowCommand">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                            </SelectedRowStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemStyle CssClass="ItemStylecss" Width="2%" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkBox" runat="Server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="" HeaderText="SL">
                                    <ItemStyle CssClass="ItemStylecss" Width="3%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMPID" HeaderText="Emp ID">
                                    <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="FullName" HeaderText="Name">
                                    <ItemStyle CssClass="ItemStylecss" Width="8%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PostingPlaceName" HeaderText="Department">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="JobTitleName" HeaderText="Designation">
                                    <ItemStyle CssClass="ItemStylecss" Width="8%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PAYDURSTART" HeaderText="From">
                                    <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PAYDUREND" HeaderText="To">
                                    <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="GROSSAMNT" HeaderText="Gross Amount">
                                    <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="NetPay" HeaderText="Net Pay">
                                    <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ISIRREGULAR" HeaderText="Is Irregular">
                                    <ItemStyle CssClass="ItemStylecss" Width="4%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SPTitle" HeaderText="Salary Package">
                                    <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ISWITHBONUS" HeaderText="Is With Bonus">
                                    <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PREPAREDBY" HeaderText="Prepared By">
                                    <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PREPARINGDATE" HeaderText="Prepared Date">
                                    <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                    <ItemStyle Width="3%" CssClass="ItemStylecss" />
                                </asp:ButtonField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div style="width: 100%; text-align: right; margin-top: 5px;">
                        <div style="float: left;">
                            Select <a id="A3" href="#" onclick="javascript: CheckBoxListSelect ('<%= grPayslipMst.ClientID %>',true)">
                                All</a> | <a id="A4" href="#" onclick="javascript: CheckBoxListSelect ('<%= grPayslipMst.ClientID %>',false)">
                                    None</a>
                        </div>
                        <div style="float: right;">
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                                OnClick="btnDelete_Click" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Panel Style="display: none" ID="PnlEmpSearch" runat="server" Width="400px" CssClass="modalPopup"
                        Height="550px">
                        <div style="font-weight: bold; font-size: 13px; background-image: url(../../Images/orengeBG.jpg);
                            color: white; background-repeat: repeat-x; height: 20px">
                            PaySlip Details Record</div>
                        <div style="margin-top: 10px; width: 98%; background-color: #eff3fb; text-align: center">
                            <fieldset>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td class="textlevel">
                                                Emp ID :
                                            </td>
                                            <td style="font-size: 10px; text-align: left">
                                                <asp:Label ID="lblEmpID" runat="server" Text="Label"></asp:Label>
                                            </td>
                                            <td class="textlevel">
                                                Emp Name :
                                            </td>
                                            <td style="font-size: 10px; text-align: left">
                                                <asp:Label ID="lblEmpName" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="textlevel">
                                                Salary Package :
                                            </td>
                                            <td colspan="3" style="font-size: 10px; text-align: left">
                                                <asp:Label ID="lblLeaveTitle" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </fieldset>
                        </div>
                        <!--Grid view Code starts-->
                        <div style="margin-top: 10px; overflow: scroll; width: 100%; height: 400px; text-align: center">
                            <asp:GridView ID="grPaySlipDetls" runat="server" Width="100%" Font-Size="9px" DataKeyNames="PSBID,EMPID,SHeadID"
                                AutoGenerateColumns="False" EmptyDataText="No Record Found" ShowFooter="true">
                                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" HorizontalAlign="Left" Font-Size="11px"
                                    Font-Names="Tahoma"></HeaderStyle>
                                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                                </SelectedRowStyle>
                                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                <RowStyle Font-Size="11px" HorizontalAlign="left" Font-Names="Tahoma" />
                                <FooterStyle BackColor="#B3CDE4" Font-Bold="True" HorizontalAlign="right" Font-Size="12px"
                                    Font-Names="Tahoma"></FooterStyle>
                                <Columns>
                                    <asp:BoundField DataField="" HeaderText="SL">
                                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="HEADNAME" HeaderText="Salary Head">
                                        <ItemStyle CssClass="ItemStylecss" Width="40%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Pay Amount">
                                        <ItemStyle Width="20%" HorizontalAlign="left" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPayAmnt" Text='<%# Convert.ToString(Eval("PAYAMT")) %>' runat="server"
                                                CssClass="TextBoxAmt100"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="fltbPayAmnt" runat="server" TargetControlID="txtPayAmnt"
                                                FilterType="Custom,Numbers" ValidChars=".,-">
                                            </cc1:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ISDEDUCTED" HeaderText="Is Deduction">
                                        <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <hr style="background-color: Black;" />
                        <div class="DivCommand1">
                            <div class="DivCommandL">
                                <asp:ImageButton ID="imgbtnSaveAndApprove" runat="server" Width="30px" Height="30px"
                                    ImageUrl="~/Images/Ok.jpeg" OnClick="imgbtnSaveAndApprove_Click"></asp:ImageButton>&nbsp;
                            </div>
                            <div class="DivCommandR">
                                <asp:ImageButton ID="imgbtnClose" runat="server" Width="25px" Height="25px" ImageAlign="AbsMiddle"
                                    ImageUrl="~/Images/Close.png"></asp:ImageButton>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:LinkButton ID="lnkButton1" runat="server" ForeColor="Black" Font-Size="10px"
                        Font-Underline="false">&nbsp; Prepeared data can be deleted before apporval.</asp:LinkButton>
                    <cc1:ModalPopupExtender ID="ModalPopupTree" runat="server" BackgroundCssClass="modalBackground"
                        CancelControlID="imgbtnClose" DropShadow="True" DynamicServicePath="" Enabled="True"
                        PopupControlID="PnlEmpSearch" TargetControlID="lnkButton1">
                    </cc1:ModalPopupExtender>
                    <asp:HiddenField ID="hfPSBID" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hfPayID" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hfEmpID" runat="server"></asp:HiddenField>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <br />
            <div id="DivCommand1" style="padding-top: 3px;">
                <div style="text-align: left; float: left">
                    <asp:Button ID="btnSendEmail" runat="server" Text="Send Email to Verify" CausesValidation="False"
                        OnClick="btnSendEmail_Click" />
                </div>
                <div style="text-align: right;">
                    <asp:Button ID="btnSave" runat="server" Text="Prepare" Width="70px" UseSubmitBehavior="False"
                        Visible="false" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
