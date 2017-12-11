<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="CLinicSetup.aspx.cs" Inherits="EIS_CLinicSetup" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>
    <div class="setup">
        <div id='formhead1'>
            <div style="width: 96%; float: left;">
                Clinic Setup</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <!--Div for group-->
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="setupInner">
            <fieldset>
                <!--Div for Controls-->
                <asp:HiddenField ID="hfIsUpadate" runat="server" />
                <asp:HiddenField ID="hfID" runat="server" />
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" CssClass="textlevel" Text="Clinic Name :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtClinic" runat="server" Width="309px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClinic"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" CssClass="textlevel" Text="Short Name :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtShortName" runat="server" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" CssClass="textlevel" Text="Clinic Type :"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlClinicType" CssClass="textlevelleft" runat="server" Width="127px">
                            </asp:DropDownList>
                        </td>
                      </tr>
                     <tr>
                        <td class="textlevel">
                            Sun Code :</td>
                        <td>
                            <asp:TextBox ID="txtSunCode" runat="server" Width="100px"></asp:TextBox>
                        </td>
                      </tr>
                     <tr>
                        <td class="textlevel">
                            Bank Acc No.</td>
                        <td>
                            <asp:TextBox ID="txtBankAccNo" runat="server" Width="100px"></asp:TextBox>
                        </td>
                      </tr>
                     <tr>
                        <td class="textlevel">
                            Bank Name :</td>
                        <td>
                            <asp:DropDownList ID="ddlBank" CssClass="textlevelleft" runat="server" 
                                Width="250px" AutoPostBack="True" 
                                onselectedindexchanged="ddlBank_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                      </tr>
                     <tr>
                        <td class="textlevel">
                            Branch Name :</td>
                        <td>
                            <asp:DropDownList ID="ddlBranch" CssClass="textlevelleft" runat="server" 
                                Width="250px">
                            </asp:DropDownList>
                        </td>
                      </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="ChkIsActive" Text="Mark Inactive" CssClass="textlevelleft" runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div class="GridFormat1">
            <!--Grid view Code starts-->
            <asp:GridView ID="grClinic" runat="server" DataKeyNames="ClinicId,IsActive,ClinicTypeId,BankCode,Routingno"
                AutoGenerateColumns="False" EmptyDataText="No Record Found" 
                Font-Size="9px" Width="100%"
                OnRowCommand="grClinic_RowCommand" 
                onselectedindexchanged="grClinic_SelectedIndexChanged">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                </SelectedRowStyle>
                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                <Columns>
                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="ClinicName" HeaderText="Clinic Name">
                        <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="ClinicShortName" HeaderText="Short Name">
                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                      <asp:BoundField DataField="SunCode" HeaderText="Sun Code">
                        <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="BankAccNo" HeaderText="Bank Acc. No.">
                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="BankName" HeaderText="Bank Name">
                        <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="BranchName" HeaderText="Branch Name">
                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="IsActive" HeaderText="Is Active">
                        <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <!--Grid view Code Ends-->
        </div>
        <div class="DivCommand1">
            <div class="DivCommandL">
                <asp:Button ID="btnClear" runat="server" Text="Refresh" Width="70px" OnClick="btnClear_Click"
                    CausesValidation="false" UseSubmitBehavior="False" />
            </div>
            <div class="DivCommandR">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                    OnClick="btnDelete_Click" />
            </div>
        </div>
    </div>
</asp:Content>



