<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="FinalPaymentReview.aspx.cs"
     Inherits="Payroll_Payroll_FinalPaymentReview" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js"></script>
        <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js"></script>
        <script language="javascript" type="text/javascript">
            window.onload = SearchByChanged;

            function SearchByChanged() {
                var ddlSB = document.getElementById('<%= ddlGeneratefor.ClientID %>');
            var ddlB = document.getElementById('<%=ddlBank.ClientID%>');

            var myindex = ddlSB.selectedIndex;
            var SelValue = ddlSB.options[myindex].value;

            if (SelValue == "A") {
                ddlB.disabled = true;
            }
            if (SelValue == "O") {
                ddlB.disabled = false;
            }
        }

        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }

        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }

        </script>
        <div class="formStyle" style="width: 95%;">
            <div id="formhead1">
                <div style="width: 97%; float: left;">
                    Final Payment
                    Review</div>
                <div style="margin: 2px; float: left; padding-right: 3px;">
                    <a href="../../Default.aspx">
                        <img src="../../Images/close_icon.gif" /></a>
                </div>
            </div>
            <div class="MsgBox">
                <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
            </div>


            <div id="PayrollConfigInner">
                <div style="background-color: #EFF3FB; border: solid 1px #3E506C;">
                    <table>
                        <tr>
                            <td class="textlevel">Generate For :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlGeneratefor" runat="server" Width="130px" CssClass="textlevelleft"
                                    onchange="SearchByChanged()">
                                    <asp:ListItem Value="A">All</asp:ListItem>
                                    <asp:ListItem Value="O">Cost Center Wise</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBank" runat="server" Width="270px" CssClass="textlevelleft">
                                </asp:DropDownList>
                            </td>
                            <td class="textlevel">Emp Id :</td>
                            <td><asp:TextBox ID="txtEmpID" runat="server" MaxLength="20" onkeyup="ToUpper(this)"
                                    Width="80px" ToolTip="Enter the Emp. Code"></asp:TextBox></td>
                            <td>
                                <asp:DropDownList ID="ddlMonth" runat="server" Width="100px" CssClass="textlevelleft">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlYear" runat="server" Width="80px" CssClass="textlevelleft">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnGenerate" runat="server" Text="Get Final Payment Data" Width="184px"
                                    OnClick="btnGenerate_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
           
            <fieldset style="margin-bottom: 10px;">
                <legend>Final Payment List</legend>
                <div style="overflow: scroll; width: 100%; height: 150px">
                    <asp:GridView ID="grList" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                        AutoGenerateColumns="False" 
                        DataKeyNames="FinalPayId">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>      
                            <asp:BoundField DataField="EmpId" HeaderText="Emp Id">
                                <ItemStyle CssClass="ItemStylecssRight" Width="10%"></ItemStyle>
                            </asp:BoundField>       
                              <asp:BoundField DataField="TotServiceYr" HeaderText="Service Year">
                                <ItemStyle CssClass="ItemStylecssRight" Width="10%"></ItemStyle>
                            </asp:BoundField>  
                             <asp:BoundField DataField="ELBalance" HeaderText="EL Balance">
                                <ItemStyle CssClass="ItemStylecssRight" Width="10%"></ItemStyle>
                            </asp:BoundField>          
                            <asp:BoundField DataField="LeaveEncash" HeaderText="Leave Encash">
                                <ItemStyle CssClass="ItemStylecssRight" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PF" HeaderText="PF">
                                <ItemStyle CssClass="ItemStylecssRight" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Gratuity" HeaderText="Gratuity">
                                <ItemStyle CssClass="ItemStylecssRight" Width="10%"></ItemStyle>
                            </asp:BoundField>
                           
                             <asp:BoundField DataField="LastMonthSalary" HeaderText="Last Month Salary">
                                <ItemStyle CssClass="ItemStylecssRight" Width="10%"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="SeperateMonthSalary" HeaderText="Seperate Month Salary">
                                <ItemStyle CssClass="ItemStylecssRight" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TripAdvPay" HeaderText="TripAdvPay">
                                <ItemStyle CssClass="ItemStylecssRight" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="AlreadyPay" HeaderText="AlreadyPay">
                                <ItemStyle CssClass="ItemStylecssRight" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="OtherPay" HeaderText="OtherPay">
                                <ItemStyle CssClass="ItemStylecssRight" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PFLoan" HeaderText="PF Loan">
                                <ItemStyle CssClass="ItemStylecssRight" Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Other" HeaderText="Other">
                                <ItemStyle CssClass="ItemStylecssRight" Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="NetPay" HeaderText="NetPay">
                                <ItemStyle CssClass="ItemStylecssRight" Width="5%"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="SeparateDate" HeaderText="Separate Date">
                                <ItemStyle CssClass="ItemStylecssCenter" Width="5%"></ItemStyle>
                            </asp:BoundField>  
                            <asp:BoundField DataField="ProcessDate" HeaderText="Process Date">
                                <ItemStyle CssClass="ItemStylecssCenter" Width="5%"></ItemStyle>
                            </asp:BoundField>                          
                        </Columns>
                    </asp:GridView>
                </div>
            </fieldset>
                 </div>
            <div class="DivCommand1" style="width: 98%;">
                <div class="DivCommandL">
                    <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" Text="Refresh"
                        Width="70px" 
                        ToolTip="Click this button to clear all fields." />
                </div>
                <div class="DivCommandR">

                    <asp:Button ID="btnSave" runat="server" Text="Click to Review Final Payment" Width="207px"
                        ToolTip="Click this button to store the information after providing all necessary fields." OnClick="btnSave_Click" />
                </div>

            </div>

        </div>
     
    </asp:Content>
