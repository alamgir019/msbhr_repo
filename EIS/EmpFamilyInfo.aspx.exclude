<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="EmpFamilyInfo.aspx.cs" Inherits="EmpFamilyInfo" Title="Employee Training" %>

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

    <div class="EmpLeaveAdJform" style="height: 425px;">
        <div id="formhead2">
            <div style="width: 97%; float: left;">
                Employee Family Info</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../File/home.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div style="margin: 5px 10px 10px 10px; width: 98%;">
            <fieldset>
                <table>
                    <tr>
                        <td class="textlevel">
                            Emp Id :</td>
                        <td>
                            <asp:TextBox ID="txtEmpID" runat="server" onkeyup="ToUpper(this)" Width="80px"></asp:TextBox>
                            &nbsp;<asp:Button ID="cmdFind" runat="server" OnClick="cmdFind_Click" Text="Find"
                                Width="54px" CausesValidation="False" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmpID"
                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Emp Full Name :</td>
                        <td>
                            <asp:TextBox ID="txtEmpFullName" runat="server" Width="245px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div style="margin: 5px 10px 5px 10px; width: 100%;">
            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="270px"
                Width="98%">
                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                    <ContentTemplate>
                        <div style="width: 100%;">
                            <div style="float: left; width: 50%;">
                                <fieldset>
                                    <table>
                                        <tr>
                                            <td class="textlevel">
                                                Name :</td>
                                            <td>
                                                <asp:TextBox ID="txtName" runat="server" Width="245px"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="textlevel">
                                                Relation :</td>
                                            <td>
                                                <asp:DropDownList ID="ddlRelation" runat="server" Width="85px" CssClass="textlevelleft">
                                                    <asp:ListItem Value="Mother">Mother</asp:ListItem>
                                                    <asp:ListItem Value="Father">Father</asp:ListItem>
                                                    <asp:ListItem Value="Spouse">Spouse</asp:ListItem>
                                                    <asp:ListItem Value="Son">Son</asp:ListItem>
                                                    <asp:ListItem Value="Daughter">Daughter</asp:ListItem>
                                                    <asp:ListItem Value="Brother">Brother</asp:ListItem>
                                                    <asp:ListItem Value="Sister">Sister</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="textlevel">
                                                DOB :</td>
                                            <td>
                                                <asp:TextBox ID="txtDob" runat="server" Width="80px"></asp:TextBox>
                                                <a href="javascript:NewCal('<%= txtDob.ClientID %>','ddmmyyyy')">
                                                    <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                                        height="16" alt="Pick a date" src="../images/cal.gif" width="16" id="img1" /></a></td>
                                            <td>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDob"
                                                    CssClass="validator" ErrorMessage="Invalid Date" ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"
                                                    Width="60px"></asp:RegularExpressionValidator>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkIsDependant" runat="server" Text="IsDependant" CssClass="textlevelleft" /></td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <asp:HiddenField ID="hfIsUpadate" runat="server" />
                                    <asp:HiddenField ID="hfFmId" runat="server" />
                                    <asp:HiddenField ID="hfFmImage" runat="server" />
                                    <asp:TextBox ID="txtOccupation" runat="server" Height="16px" Width="401px" CssClass="TextBox120"
                                        Visible="False"></asp:TextBox>
                                    <asp:DropDownList ID="ddlBloodGroup" runat="server" Width="130px" Visible="False">
                                        <asp:ListItem>NA</asp:ListItem>
                                        <asp:ListItem Value="A +ve"></asp:ListItem>
                                        <asp:ListItem>A -ve</asp:ListItem>
                                        <asp:ListItem>B +ve</asp:ListItem>
                                        <asp:ListItem>B -ve</asp:ListItem>
                                        <asp:ListItem>O +ve</asp:ListItem>
                                        <asp:ListItem>O -ve</asp:ListItem>
                                        <asp:ListItem>AB +ve</asp:ListItem>
                                        <asp:ListItem>AB -ve</asp:ListItem>
                                    </asp:DropDownList>
                                </fieldset>
                                <fieldset>
                                    <legend>Insurance Details</legend>
                                    <table>
                                        <tr>
                                            <td class="textlevel">
                                                Insurance ID :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtInsID" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="textlevel">
                                                Date of Incusion :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtIncusionDate" runat="server" Width="80px"></asp:TextBox>
                                                <a href="javascript:NewCal('<%= txtIncusionDate.ClientID %>','ddmmyyyy')">
                                                    <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                                        height="16" alt="Pick a date" src="../images/cal.gif" width="16" id="img2" /></a>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="textlevel">
                                                Date of Renewal :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRenewalDate" runat="server" Width="80px"></asp:TextBox>
                                                <a href="javascript:NewCal('<%= txtRenewalDate.ClientID %>','ddmmyyyy')">
                                                    <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                                                        height="16" alt="Pick a date" src="../images/cal.gif" width="16" id="img3" /></a>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:LinkButton ID="lnkHospitalization" runat="server">View Hospilization Records</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                            <div style="float: right; text-align: right; width: 45%;">
                                <table align="right">
                                    <tr>
                                        <td>
                                            <div style="text-align: center; width: 235px; border: solid 1px #3366CC">
                                                <fieldset style="height: 20px; text-align: center; background-color: #3366CC; color: White;">
                                                    Photo</fieldset>
                                                <div style="background-color: Gray;">
                                                    <asp:Image ID="imgEmp" runat="server" Height="142px" Width="150px" BackColor="White" />
                                                </div>
                                                <div style="text-align: left; border-top: solid 1px #3366CC;">
                                                    <table>
                                                        <tr>
                                                            <td style="width: 3px">
                                                                <asp:FileUpload ID="FileUpload1" runat="server" Width="209px" /></td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="text-align: left;">
                                                                <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" /></td>
                                                            <td style="text-align: right;">
                                                                <asp:Button ID="btnRemove" runat="server" Text="Remove" OnClick="btnRemove_Click" /></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <table width="100%">
                            <tr>
                                <td>
                                    <div style="width: 100%;">
                                        <div style="float: left; width: 50%;">
                                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px" CausesValidation="False"
                                                OnClick="btnRefresh_Click" /></div>
                                        <div style="float: right; text-align: right; width: 49%;">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" UseSubmitBehavior="False"
                                                OnClick="btnSave_Click" /></div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <HeaderTemplate>
                        Family Information
                    </HeaderTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                    <HeaderTemplate>
                        Family Member List
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div style="margin: 10px 0px 0px 0px; width: 100%; border: solid 1px black; overflow: scroll;
                            height: 250px;">
                            <asp:GridView ID="grEmpFamilyInfo" runat="server" AutoGenerateColumns="False" DataKeyNames="EmpId,FmId"
                                EmptyDataText="No Record Found" Font-Size="9px" OnRowCommand="grEmpFamilyInfo_RowCommand"
                                Width="100%">
                                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                                <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                        <ItemStyle CssClass="ItemStylecss" Width="5%" />
                                    </asp:ButtonField>
                                    <asp:ButtonField CommandName="RowDeleting" HeaderText="Delete" Text="Delete">
                                        <ItemStyle CssClass="ItemStylecss" Width="5%" />
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="fmName" HeaderText="Name">
                                        <ItemStyle CssClass="ItemStylecss" Width="20%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="frRelation" HeaderText="Relation">
                                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FmDoB" HeaderText="DoB">
                                        <ItemStyle CssClass="ItemStylecss" Width="7%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Isdependent" HeaderText="Dependent">
                                        <ItemStyle CssClass="ItemStylecss" Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="InsuranceID" HeaderText="Insurance">
                                        <ItemStyle CssClass="ItemStylecss" Width="6%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="InclusionDate" HeaderText="Inclusion">
                                        <ItemStyle CssClass="ItemStylecss" Width="7%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RenewalDate" HeaderText="Renewal">
                                        <ItemStyle CssClass="ItemStylecss" Width="7%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EmpPicLoc" HeaderText="Photo">
                                        <ItemStyle CssClass="ItemStylecss" Width="7%" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
        </div>
    </div>
</asp:Content>
