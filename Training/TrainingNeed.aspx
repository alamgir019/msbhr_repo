<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="TrainingNeed.aspx.cs" Inherits="Training_TrainingNeedTypeSetup" Title="Training Need" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

    <script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js">
        //Date Time Picker script
    </script>

    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>

    <script language="javascript" type="text/javascript">
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }
    </script>

    <div class="empTrainForm">
        <div id="formhead2">
            <div style="width: 97%; float: left;">
                Training Need</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
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
                                        Emp Id :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmpID" Width="80px" runat="server" onkeyup="ToUpper(this)"></asp:TextBox>
&nbsp;
                                        <asp:ImageButton ID="imgBtnSearch" runat="server" Height="19px" ImageUrl="~/Images/Search_Icon.jpg"
                                            OnClick="imgBtnSearch_Click" CausesValidation="False" ClientIDMode="Predictable" />

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmpID"
                                            ErrorMessage="*"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td class="textlevel">
                                        Name :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblName" runat="server"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td class="textlevel">
                                        JobTitle :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblJobTitle" runat="server"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td class="textlevel">
                                        Sector :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSector" runat="server"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td class="textlevel">
                                        Department :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDept" runat="server"></asp:Label>

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
                            Training Need Tier :</td>
                        <td>
                            <asp:DropDownList ID="ddlTrnneedTier" runat="server" CssClass="textlevelleft" Width="250px"
                                ToolTip="Select the name of action for confirmation from the list.">
                            </asp:DropDownList>
                        </td>

                         <td class="textlevel">
                            Training Mode :</td>
                        <td>
                            <asp:DropDownList ID="ddlTrnMode" runat="server" 
                                CssClass="textlevelleft" Width="250px"
                                ToolTip="Select the name of action for confirmation from the list.">
                            </asp:DropDownList></td>
                       </tr>
                    <tr>
                        <td class="textlevel">
                            Tranining Need Type :</td>
                        <td>
                            <asp:DropDownList ID="ddltrnNeedType" runat="server" CssClass="textlevelleft" Width="250px" 
                                AutoPostBack="True" 
                                onselectedindexchanged="ddltrnNeedType_SelectedIndexChanged">
                            </asp:DropDownList></td>
                             <td class="textlevel">
                            Training Need Year :</td>
                        <td>
                            <asp:DropDownList ID="ddlTrnNeedYear" runat="server" 
                                CssClass="textlevelleft" Width="100px">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Training Sub Type :</td>
                        <td>
                            <asp:DropDownList ID="ddlTrnNeedSubType" runat="server" 
                                CssClass="textlevelleft" Width="250px">
                            </asp:DropDownList></td>
                    </tr>
                     <tr>
                       
                    </tr>
                     <tr>
                       
                    </tr>
                    
                </table>
                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                <asp:HiddenField ID="hfID" runat="server" />
                
            </fieldset>
            <fieldset>
                <legend>Training Needed List</legend>
                <div style="overflow: scroll; width: 100%; height: 250px">
                    <asp:GridView ID="grTrainingNeed" runat="server" Width="99%" Font-Size="9px" EmptyDataText="No Record Found"
                        AutoGenerateColumns="False" DataKeyNames="TrainingNeedID,TrainingNeedTierID,TrainingNeedTypeID,TrainingSubTypeID,TrainingModeID,TrainingYear" 
                        OnRowCommand="grPayCharging_RowCommand">

                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                <ItemStyle Width="5%" CssClass="ItemStylecss" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="TrainingNeedTierName" HeaderText="Training Need Tier">
                                <ItemStyle CssClass="ItemStylecss" Width="21%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TrainingNeedTypeName" HeaderText="Training Need Type">
                                <ItemStyle CssClass="ItemStylecss" Width="21%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TrainingSubTypeName" HeaderText="Training Need Sub Type">
                                <ItemStyle CssClass="ItemStylecss" Width="21%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TrainingModeName" HeaderText="Training Mode">
                                <ItemStyle CssClass="ItemStylecss" Width="21%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TrainingYear" HeaderText="Training Year">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            
                        </Columns>
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
        <div class="DivCommand1" style="width: 98%;">
            <div class="DivCommandL">
                <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" Text="Refresh"
                    Width="70px" OnClick="btnRefresh_Click" ToolTip="Click on Save Button to store the employee data." />
            </div>
            <div class="DivCommandR">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click"
                    ToolTip="Click on Save Button to store the employee data." />
               <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="javascript:return DeleteConfirmation();"
                Text="Delete" Width="70px" />
            </div>
        </div>
    </div>

</asp:Content>
