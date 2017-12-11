<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="KPIQuarterSetup.aspx.cs" Inherits="KPI_KPIQuarterSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>
    
<script language="javascript" type="text/javascript">
   
    //Except only numbers for Age textbox
    function onlyNumbers(event) {
        var charCode = (event.which) ? event.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }

</script>
    <div class="officeSetup">
        <div id="formhead1">
            <div style="width: 96%; float: left;">
                Quarter Setup</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <!--Div for group-->
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="officeSetupInner">
            <fieldset>
                <!--Div for Controls-->
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" CssClass="textlevel" Text="Quarter Name :"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtQuarter" runat="server" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQuarter"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="Label1" runat="server" CssClass="textlevel" Text="From :"></asp:Label></td>
                        <td><asp:TextBox ID="txtFrom" runat="server" Width="80px" ></asp:TextBox><asp:RangeValidator ID="RangeValidator1" Type="Integer" MinimumValue="0" MaximumValue="12" ControlToValidate="txtFrom" runat="server" ErrorMessage="Only 1-12"></asp:RangeValidator>
                        </td>
                        <td><asp:Label ID="Label3" runat="server" CssClass="textlevel" Text="To :"></asp:Label></td>
                        <td><asp:TextBox ID="txtTo" runat="server" Width="80px"></asp:TextBox></td>
                        <td><asp:RangeValidator ID="RangeValidator2" Type="Integer" MinimumValue="0" MaximumValue="12" ControlToValidate="txtTo" runat="server" ErrorMessage="Only 1-12"></asp:RangeValidator></td>
                    </tr>

                    <tr>
                        <td style="height: 22px">
                        </td>
                        <td style="height: 22px" colspan="3">
                            <asp:CheckBox ID="chkInActive" runat="server" CssClass="textlevelleft" Text="Mark Inactive" />
                        </td>
                        <td style="height: 22px">
                        </td>
                    </tr>
                    
                </table>
            </fieldset>
            <br />
            <br />
        </div>
        <div class="GridFormat1">
            <!--Grid view Code starts-->
            <asp:GridView ID="grQuarter" runat="server" DataKeyNames="QuarterId,FromMonth,ToMonth" AutoGenerateColumns="False"
                EmptyDataText="No Record Found" Font-Size="9px" OnRowCommand="grQuarter_RowCommand"
                OnSelectedIndexChanged="grQuarter_SelectedIndexChanged" Width="99%">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                </SelectedRowStyle>
                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                <Columns>
                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                        <ItemStyle CssClass="ItemStylecss" Width="10%" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="QuarterName" HeaderText="Quarter Name">
                        <ItemStyle CssClass="ItemStylecss" Width="50%"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="FromMonth" HeaderText="From Month">
                        <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="ToMonth" HeaderText="To Month">
                        <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                    </asp:BoundField>


                </Columns>
            </asp:GridView>
            <!--Grid view Code Ends-->
        </div>
        <div class="DivCommand1">
            <div class="DivCommandL">
                <asp:Button ID="btnClear" runat="server" Text="Refresh" Width="70px" OnClick="btnClear_Click"
                    CausesValidation="False" />
            </div>
            <div class="DivCommandR">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                    OnClick="btnDelete_Click" />
            </div>
        </div>
    </div>


</asp:Content>