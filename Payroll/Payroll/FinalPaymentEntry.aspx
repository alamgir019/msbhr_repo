<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="FinalPaymentEntry.aspx.cs" Inherits="Payroll_Payroll_FinalPaymentEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js">
        //Date Time Picker script
    </script>

    <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js">
    </script>

    <script language="javascript" type="text/javascript">
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }
    </script>

    <div class="empTrainForm">
        <div id="formhead1">
            <div style="width: 97%; float: left;">
               Final Payment</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>

         
          <div id="PayrollConfigInner">
            <div style="background-color: #EFF3FB; margin-bottom: 10px;">
                <fieldset>
                    <table>
                        <tr>
                            <td class="textlevel">
                                Emp Id :</td>
                            <td>
                                <asp:TextBox ID="txtEmpID" runat="server" MaxLength="20" onkeyup="ToUpper(this)"
                                    Width="80px" ToolTip="Enter the Emp. Code"></asp:TextBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgBtnSearch" runat="server" Height="19px" ImageUrl="~/Images/Search_Icon.jpg"
                                    OnClick="imgBtnSearch_Click" ToolTip="Click on find button to retrieve any existing employee information." />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="ReqFieldVal" runat="server" ErrorMessage="*" ControlToValidate="txtEmpID"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td class="textlevel">
                                Name :
                            </td>
                            <td>
                                <asp:Label ID="lblName" runat="server"></asp:Label>
                            </td>
                            <td class="textlevel">
                                Cost Center :</td>
                            <td>
                                <asp:Label ID="lblOffice_Loc" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Designation :
                            </td>
                            <td>
                                <asp:Label ID="lblDeg_Project" runat="server"></asp:Label>
                            </td>
                            <td class="textlevel">
                                Joining Date :</td>
                            <td>
                                <asp:Label ID="lblJoiningDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Separate Type :</td>
                            <td>
                                <asp:Label ID="lblSeparateType" runat="server"></asp:Label>
                            </td>
                            <td class="textlevel">
                                Separate Date :</td>
                            <td>
                                <asp:Label ID="lblSeprateDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel" style="height: 16px">
                                Service Period</td>
                            <td style="height: 16px">
                                <asp:Label ID="lblServiceYr" runat="server"></asp:Label>
                                (In Years)</td>
                            <td style="height: 16px" class="textlevel">
                                EL Balance :</td>
                            <td style="height: 16px">
                                <asp:Label ID="lblLeave" runat="server"></asp:Label>
                                (In Days)</td>
                        </tr>                        
                                                                     
                        <tr>
                            <td class="textlevel" style="height: 16px">
                                Confirmation Date:</td>
                            <td style="height: 16px">
                                <asp:Label ID="lblConfirmationDate" runat="server"></asp:Label>
                            </td>
                            <td style="height: 16px" class="textlevel">
                                Gratuity Length :</td>
                            <td style="height: 16px">
                                <asp:Label ID="lblGratuityYr" runat="server"></asp:Label>
                                (In Years)</td>
                        </tr>                        
                                                                     
                        </table>
                </fieldset>
             </div>

             <fieldset style="margin-bottom: 10px;">
                <legend>Final Payment</legend>
                <table>

                      <tr>
                        <td class="textlevel">
                            Basic Pay:</td>
                        <td>
                            <asp:TextBox ID="txtBasicPay" runat="server" MaxLength="20" 
                                ToolTip="Enter Payment Amount" Width="80px" CssClass="TextBoxAmt60"></asp:TextBox>
                            </td>
                        <td class="textlevel">
                            &nbsp;</td>
                        <td>
                            </td>
                    </tr>

                      <tr>
                        <td class="textlevel">
                            Total Pay:</td>
                        <td>
                            <asp:TextBox ID="txtTotalPay" runat="server" MaxLength="20" 
                                ToolTip="Enter Payment Amount" Width="80px" CssClass="TextBoxAmt60" 
                                Enabled="False"></asp:TextBox>
                            </td>
                        <td class="textlevel">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Leave Encashment Amount:</td>
                        <td>
                            <asp:TextBox ID="txtLeaveEncash" runat="server" MaxLength="20" Width="80px" CssClass="TextBoxAmt60"></asp:TextBox></td>
                        <td class="textlevel">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Provident Fund :</td>
                        <td>
                            <asp:TextBox ID="txtPF" runat="server" MaxLength="20" Width="80px" CssClass="TextBoxAmt60"></asp:TextBox></td>
                        <td class="textlevel">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Gratuity :</td>
                        <td>
                            <asp:TextBox ID="txtGratuity" runat="server" MaxLength="20" Width="80px" CssClass="TextBoxAmt60"></asp:TextBox></td>
                        <td class="textlevel">
                            &nbsp;</td>
                        <td>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Salary of Last Month :</td>
                        <td>
                            <asp:TextBox ID="txtLastMonthSalary" runat="server" MaxLength="20" Width="80px" CssClass="TextBoxAmt60">0</asp:TextBox></td>
                        <td class="textlevel">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Salary of
                                <asp:Label ID="lblSalaryMonth" runat="server"></asp:Label>
                            : </td>
                        <td class="textlevelleft">
                            <asp:TextBox ID="txtSeperateMonthSal" runat="server" MaxLength="20" Width="80px" CssClass="TextBoxAmt60">0</asp:TextBox>&nbsp;of
                                <asp:Label ID="lblSalaryDays" runat="server"></asp:Label>
                            &nbsp;Days</td>
                        <td class="textlevel">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Total :</td>
                        <td>
                            <asp:TextBox ID="txtTotal" runat="server" Width="80px" CssClass="TextBoxAmt60" 
                                Enabled="False" ReadOnly="True"></asp:TextBox></td>
                        <td class="textlevel">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Less: Trip Advance</td>
                        <td>
                            <asp:TextBox ID="txtTripAdvPay" runat="server" MaxLength="20" Width="80px" CssClass="TextBoxAmt60"></asp:TextBox></td>
                        <td class="textlevel">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Less : Already Paid</td>
                        <td>
                            <asp:TextBox ID="txtAlreadyPay" runat="server" MaxLength="20" Width="80px" CssClass="TextBoxAmt60"></asp:TextBox></td>
                        <td class="textlevel">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Less : Others</td>
                        <td>
                            <asp:TextBox ID="txtOtherPay" runat="server" MaxLength="20" Width="80px" CssClass="TextBoxAmt60"></asp:TextBox></td>
                        <td class="textlevel">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Less: PF Loan</td>
                        <td>
                            <asp:TextBox ID="txtPFLoan" runat="server" MaxLength="20" Width="80px" CssClass="TextBoxAmt60"></asp:TextBox></td>
                        <td class="textlevel">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Add : Others</td>
                        <td>
                            <asp:TextBox ID="txtOther" runat="server" MaxLength="20" Width="80px" CssClass="TextBoxAmt60"></asp:TextBox></td>
                        <td class="textlevel">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Net Pay :</td>
                        <td>
                            <asp:TextBox ID="txtNetPay" runat="server" MaxLength="20" Width="80px" CssClass="TextBoxAmt60"></asp:TextBox>&nbsp;
                            <asp:Button ID="btnCalculateNet" runat="server" CausesValidation="False" Text="Calculate "
                    Width="70px" OnClick="btnCalculateNet_Click"
                    ToolTip="Click this button to clear all fields." />
                        </td>
                        <td class="textlevel">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Process Date :</td>
                        <td>
                            <asp:TextBox ID="txtProcessDate" runat="server" 
                                ToolTip="Input the from date of temporary duty." Width="80px"></asp:TextBox>
                            <a href="javascript:NewCal('<%= txtProcessDate.ClientID %>','ddmmyyyy')">
                            <img alt="Pick a date" height="16" src="../../images/cal.gif" 
                                style="border: 0px;" width="16" /></a>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                ControlToValidate="txtProcessDate" CssClass="validator" 
                                ErrorMessage="Invalid Date" 
                                ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                        </td>
                        <td class="textlevel">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Remarks :</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtRemarks" runat="server" MaxLength="200"
                                ToolTip="Enter Remarks" Width="500px" TextMode="MultiLine" >
                                </asp:TextBox>
                        </tr>
                    
                  <%--  <cc1:FilteredTextBoxExtender ID="FTBPercentage" runat="server" FilterType="Custom,Numbers"
                        TargetControlID="txtPercentage" ValidChars="0123456789">
                    </cc1:FilteredTextBoxExtender>
                    <cc1:FilteredTextBoxExtender ID="FilteredAmount" runat="server" FilterType="Custom,Numbers"
                        TargetControlID="txtAmount" ValidChars="0123456789.">
                    </cc1:FilteredTextBoxExtender>--%>
                </table>

                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                <asp:HiddenField ID="hfId" runat="server" />
            </fieldset>

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
                    Width="70px" OnClick="btnRefresh_Click"
                    ToolTip="Click this button to clear all fields." />
            </div>
            <div class="DivCommandR">

                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" 
                       OnClick="btnSave_Click"
                     ToolTip="Click this button to store the information after providing all necessary fields." />
            </div>
            
        </div>

      </div>
    <br />
    <br />
</asp:Content>

