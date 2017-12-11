<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="EmpDocUpload.aspx.cs" Inherits="EIS_EmpDocUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }
    </script>
  
    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js"></script>
    <div class="formStyle" style="height: 500px; width: 650px;">
        <div id="formhead1">
            Employee Personal File Upload</div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div style="background-color: #EFF3FB; margin-bottom: 10px">
            <fieldset>
                <table>
                    <tr>
                        <td class="textlevel">
                            Emp Id :
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmpID" runat="server" MaxLength="20" onkeyup="ToUpper(this)"
                                Width="80px" ToolTip="Enter the Emp. Code and click on Find Button. Details of the find employee will be shown in employee general information screen if information exists. In order to enter employee."></asp:TextBox>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgBtnSearch" runat="server" Height="19px" ImageUrl="~/Images/Search_Icon.jpg"
                                OnClick="imgBtnSearch_Click" ToolTip="Enter the Emp. Code and click on Find Button. Details of the find employee will be shown in employee general information screen if information exists. In order to enter employee." />
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
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Designation :
                        </td>
                        <td>
                            <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Project :
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
        <fieldset>
            <legend>Upload Document</legend>
            <table>
                <tr>
                    <td>
                        <asp:FileUpload ID="fileUpload1" runat="server" /><br />
                    </td>
                    <td>
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                    </td>
                </tr>
            </table>
            <legend>Document List</legend>
             <div style="overflow: scroll; width: 100%; height: 250px">
            
               <asp:GridView ID="gvDetails" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                        AutoGenerateColumns="False" DataKeyNames="FilePath" 
                     onselectedindexchanged="gvDetails_SelectedIndexChanged">
                    <HeaderStyle BackColor="#df5015" />
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" />
                        <asp:BoundField DataField="FileName" HeaderText="FileName" />
                        <asp:TemplateField HeaderText="FilePath">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="lnkDownload_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" OnClick="lnkDelete_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </fieldset>
    </div>
</asp:Content>
