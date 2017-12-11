<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="EmergencyContract.aspx.cs" Inherits="EIS_EmergencyContract" Title="Emergency Contract" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

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

     <div class="empEduForm">
        <div id="formhead2">
            <div style="width: 97%; float: left;">
                Emergency Contract</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div class="empEduDiv01">
            <fieldset>
                <table>
                    <tr>
                        <td class="textlevel">
                            Emp Id :</td>
                        <td>
                            <asp:TextBox ID="txtEmpID" runat="server" MaxLength="20" onkeyup="ToUpper(this)"
                                Width="80px" ToolTip="Enter the Emp. Code and click on Find Button. Details of the find employee will be shown in employee general information screen if information exists. In order to enter employee."></asp:TextBox></td>
                        <td>
                            <asp:ImageButton ID="imgBtnSearch" runat="server" Height="19px" ImageUrl="~/Images/Search_Icon.jpg"
                                OnClick="imgBtnSearch_Click" ToolTip="Enter the Emp. Code and click on Find Button. Details of the find employee will be shown in employee general information screen if information exists. In order to enter employee." /></td>
                        <td>
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
                </table>
            </fieldset>
            </div>
            <div style="margin: 5px 10px 10px 10px;">
            <div class="Div920">
            <fieldset>
                <legend>Contact Details</legend>
                <table>
                    <tr>
                        <td class="textlevelleft">
                            Name</td>
                        <td class="textlevelleft">
                            Address</td>
                        <td class="textlevelleft">
                            Phone</td>
                        <td class="textlevelleft">
                            Relation</td>
                        <td></td>
                        <td></td>
                    </tr> 
                    <tr>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtAdd" runat="server" Width="200px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtPhone" runat="server" Width="100px"></asp:TextBox></td>
                        <td>
                             <asp:DropDownList ID="ddlRelation" runat="server" Width="200px"></asp:DropDownList></td>
                        <td> <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click"
                    ToolTip="Click on Save Button to store the employee data." /></td>
                        <td><asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="javascript:return DeleteConfirmation();"
                Text="Delete" Width="70px" /></td>
                    </tr>
                </table>
            </fieldset>
            </div> 
            <asp:HiddenField ID="hfId" runat="server" />
            <asp:HiddenField ID="hfIsUpdate" runat="server" />
            <div id="empEduDiv02" style="height: 200px;">
                <div style="overflow: scroll; width: 100%; height: 250px">
                    <asp:GridView ID="grContactList" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                        AutoGenerateColumns="False" DataKeyNames="ContactId,Relationid" 
                        onrowcommand="grContactList_RowCommand">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                <ItemStyle Width="5%" CssClass="ItemStylecss" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="Name" HeaderText="Name">
                                <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Address" HeaderText="Address">
                                <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Phone" HeaderText="Phone">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="RelationName" HeaderText="Relation">
                                <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
           </div> <div class="DivCommand1" style="width: 98%;">
            <div class="DivCommandL">
                <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" Text="Refresh"
                    Width="70px" OnClick="btnRefresh_Click" ToolTip="Click on Save Button to store the employee data." />
            </div>
        </div>
           </div> 
           
        </div>
</asp:Content>
