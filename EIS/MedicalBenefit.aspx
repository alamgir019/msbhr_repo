<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="MedicalBenefit.aspx.cs" Inherits="EIS_MedicalBenefit" Title="Medical Benefit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js">
    //Date Time Picker script
    </script>

    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>

    <script language="javascript" type="text/javascript">
    function ToUpper(ctrl)
    {   
    var t = ctrl.value;
    ctrl.value = t.toUpperCase(); 
    }
    </script>

    <div class="empTrainForm">
        <div id="formhead1">
            <div style="width: 93%; float: left;">
                Medical Benefit</div>
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
                                Medical Fiscal Year 
                                :</td>
                            <td>
                                <asp:DropDownList ID="ddlFiscalYr" runat="server" Width="200px" CssClass="textlevelleft">
                        </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Emp Id :</td>
                            <td>
                                <asp:TextBox ID="txtEmpID" runat="server" MaxLength="20" onkeyup="ToUpper(this)"
                                    Width="80px" ToolTip="Enter the Emp. Code"></asp:TextBox>
                                <asp:ImageButton ID="imgBtnSearch" runat="server" Height="19px" ImageUrl="~/Images/Search_Icon.jpg"
                                    OnClick="imgBtnSearch_Click" />
                                <asp:RequiredFieldValidator ID="ReqFieldVal" runat="server" ErrorMessage="*" ControlToValidate="txtEmpID"></asp:RequiredFieldValidator></td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td class="textlevel">
                                Name :</td>
                            <td>
                                <asp:Label ID="lblName" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Organization :</td>
                            <td>
                                <asp:Label ID="lblCompany" runat="server"></asp:Label></td>
                        </tr>
                         <tr>
                            <td class="textlevel">
                                Project :</td>
                            <td>
                                <asp:Label ID="lblProject" runat="server"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td class="textlevel" style="height: 16px">
                                Department :</td>
                            <td style="height: 16px">
                                <asp:Label ID="lblDept" runat="server"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td class="textlevel" style="height: 16px">
                                Sub Department :</td>
                            <td style="height: 16px">
                                <asp:Label ID="lblSubDept" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel" style="height: 16px">
                                Subcode :</td>
                            <td style="height: 16px">
                                <asp:Label ID="lblSuncode" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Designation :</td>
                            <td>
                                <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Medical Balance :</td>
                            <td>
                                <asp:Label ID="lblMedicalBalance" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Hospital Balance :</td>
                            <td>
                                <asp:Label ID="lblHospitalBalance" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <fieldset style="margin-bottom: 10px;">
                <legend> Details</legend>
                <table>
                    <tr>
                        <td class="textlevel">
                            <asp:RadioButton ID="radMedicine" runat="server" Text="Medicine" 
                                GroupName="Type" AutoPostBack="True" 
                                oncheckedchanged="radMedicine_CheckedChanged"/></td>
                        <td>
                            <asp:RadioButton ID="radHospital" runat="server" class="textlevel" 
                                Text="Hospitalization" GroupName="Type" AutoPostBack="True" 
                                oncheckedchanged="radHospital_CheckedChanged"/>
                        </td>                                             
                    </tr>                                      
                    <tr>
                        <td class="textlevel" style="height: 16px"></td>
                        <td>
                            <asp:CheckBox ID="chkIsSpHospital" runat="server" CssClass="textlevel" Width="168px"
                                 Text="Is Special Hospitalization" GroupName="Type"/></td>
                    </tr>
                    <tr>
                        <td class="textlevel" style="height: 16px">Medical Date :</td>
                        <td>
                            <asp:TextBox ID="txtMedicalDate" runat="server" Width="89px">
                                    </asp:TextBox>
                            <a href="javascript:NewCal('<%= txtMedicalDate.ClientID %>','ddmmyyyy')">
                                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                    height="16" alt="Pick a date" src="../images/cal.gif" width="16" /></a>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" 
                                runat="server" ControlToValidate="txtMedicalDate"
                                    CssClass="validator" ErrorMessage="Invalid"                                             
                                ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                        </td>                    
                    </tr>
                    <tr>
                        <td class="textlevel" style="height: 16px">Limit :</td>
                        <td>
                            <asp:TextBox ID="txtLimit" runat="server" Width="80px" CssClass="TextBoxAmt60"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Requested Amount :</td>
                        <td>
                            <asp:TextBox ID="txtReqAmount" runat="server" Width="80px" 
                                CssClass="TextBoxAmt60"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Approved Amount :</td>
                        <td>
                            <asp:TextBox ID="txtApproveAmount" runat="server" Width="80px" 
                                CssClass="TextBoxAmt60"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Remarks :</td>
                        <td>
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                     <tr>
                            <td class="textlevel">
                               Nominee :</td>
                            <td>
                                <asp:DropDownList ID="ddlMedNominee" runat="server" Width="200px" CssClass="textlevelleft">
                        </asp:DropDownList></td>
                        </tr>
                    <%--<tr><td class="textlevel">Nominee :</td>
                        <td>
                        <asp:TextBox ID="txtNominee" runat="server" 
                                TextMode="MultiLine"></asp:TextBox></td>
                    </tr>--%>
                </table>
                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                <asp:HiddenField ID="hfId" runat="server" />
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom,Numbers"
                    ValidChars="0123456789." TargetControlID="txtLimit">
                </cc1:FilteredTextBoxExtender>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom,Numbers"
                    ValidChars="0123456789." TargetControlID="txtReqAmount">
                </cc1:FilteredTextBoxExtender>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom,Numbers"
                    ValidChars="0123456789." TargetControlID="txtApproveAmount">
                </cc1:FilteredTextBoxExtender>
            </fieldset>
            <div class="DivCommand1" style="width: 98%;">
                <div class="DivCommandL">
                    <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" Text="Refresh"
                        Width="70px" OnClick="btnRefresh_Click" ToolTip="Click this button to clear all fields." />
                </div>
                <div class="DivCommandR">
                    <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Save" 
                        Width="64px" />
                </div>
            </div>
            <fieldset style="margin-bottom: 10px;">
                <legend> List</legend>
                <div style="overflow: scroll; width: 100%; height: 250px">
                    <asp:GridView ID="grMBenefit" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                        AutoGenerateColumns="False" DataKeyNames="BenefitId,BenefitType,NomineeId" 
                        onrowcommand="grMBenefit_RowCommand">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                <ItemStyle Width="5%" CssClass="ItemStylecss" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="BenefitType" HeaderText="Benefit Type">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>   
                            <asp:BoundField DataField="IsSpHospital" HeaderText="Is Sp. Hospitalization">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>   
                            <asp:BoundField DataField="MedicalDate" HeaderText="Medical Date">
                                <ItemStyle CssClass="ItemStylecssRight" Width="10%"></ItemStyle>
                            </asp:BoundField>  
                            <asp:BoundField DataField="Limit" HeaderText="Limit">
                                <ItemStyle CssClass="ItemStylecssRight" Width="10%"></ItemStyle>
                            </asp:BoundField>      
                            <asp:BoundField DataField="ReqAmount" HeaderText="Request Amount">
                                <ItemStyle CssClass="ItemStylecssRight" Width="10%"></ItemStyle>
                            </asp:BoundField>      
                            <asp:BoundField DataField="ApproveAmount" HeaderText="Approve Amount">
                                <ItemStyle CssClass="ItemStylecssRight" Width="10%"></ItemStyle>
                            </asp:BoundField>      
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                            </asp:BoundField>   
                            <asp:BoundField DataField="NomineeName" HeaderText="Nominee">
                                <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                            </asp:BoundField>      
                                                     
                        </Columns>
                    </asp:GridView>
                </div>
            </fieldset>
            </div>
    </div>

</asp:Content>
