<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="EmpEducation.aspx.cs" Inherits="EmpEducation" Title="Employee Education" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../JScripts/datetimepicker.js">
    //Date Time Picker script
    </script>

    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>

    <script language="javascript">
    function ToUpper(ctrl)
    {   
    var t = ctrl.value;
    ctrl.value = t.toUpperCase(); 
    }
    </script>

    <div class="empEduForm">
        <div id="formhead2">
            <div style="width: 98%; float: left;">
                Employee Education</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div style="background-color: #EFF3FB; margin: 5px 10px 10px 10px;">
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
                                Job Title :</td>
                            <td>
                                <asp:Label ID="lblJobTitle" runat="server"></asp:Label></td>
                        </tr>
                         
                        <tr>
                            <td class="textlevel">
                                Department :</td>
                            <td>
                                <asp:Label ID="lblDept" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Company :</td>
                            <td>
                                <asp:Label ID="lblCompany" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="textlevel">
                                Office/project :</td>
                            <td>
                                <asp:Label ID="lblProject" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        <div style="margin: 5px 10px 10px 10px;">
            <div class="Div920">
                <fieldset>
                    <table>
                        <tr>
                            <td class="textlevelleft">
                                Education</td>
                            <td class="textlevelleft">
                                Institute/Board</td>
                            <td class="textlevelleft">
                                Subject</td>
                            <td class="textlevelleft">
                                Result</td>
                            <td class="textlevelleft">
                                Passing Year</td>
                            <td class="textlevelleft">
                                CGPA/Marks</td>
                            <td class="textlevelleft">
                                Degree Title</td>
                            <td class="textlevelleft">
                                &nbsp;</td>
                            <td class="textlevelleft">
                            </td>
                        </tr>
                        <tr>
                            <td><asp:DropDownList ID="ddlDegree" runat="server" Width="127px" 
                                    CssClass="textlevelleft">
                            </asp:DropDownList></td>
                            <td>
                                <asp:DropDownList ID="ddlInstitute" runat="server" Width="127px" 
                                    CssClass="textlevelleft">
                                </asp:DropDownList></td>
                            <td><asp:DropDownList ID="ddlSubject" runat="server" Width="127px" 
                                    CssClass="textlevelleft">
                            </asp:DropDownList></td>
                            <td><asp:DropDownList ID="ddlResult" runat="server" Width="127px" 
                                    CssClass="textlevelleft">
                            </asp:DropDownList></td>
                            <td>
                                <asp:TextBox ID="txtPassingYear" runat="server" Width="113px" MaxLength="4"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtMarks" runat="server" MaxLength="4" Width="112px"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtDegreeTitle" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:CheckBox ID="chkIsMaxDegree" runat="server" CssClass="textlevel" 
                                    Text="Is Max Degree" />
                            </td>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" 
                                    UseSubmitBehavior="False" onclick="btnSave_Click"/></td>
                            </tr>
                    </table>
                   <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                <asp:HiddenField ID="hfId" runat="server" />
                    <cc1:FilteredTextBoxExtender ID="FilteredTxt1" runat="server" FilterType="Custom,Numbers"
                        TargetControlID="txtPassingYear" ValidChars=".">
                    </cc1:FilteredTextBoxExtender>
                </fieldset>
            </div>
            <div id="empEduDiv02" style="height: 200px;">
                <asp:GridView ID="grEmpEdu" runat="server" AutoGenerateColumns="False" DataKeyNames="EduId,DegreeId,InstituteId,SubjectId,ResultId"
                    EmptyDataText="No Record Found" Font-Size="9px" 
                    Width="99%" onrowcommand="grEmpEdu_RowCommand">
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
                        <asp:BoundField DataField="DegreeName" HeaderText="Degree Name">
                            <ItemStyle CssClass="ItemStylecss" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="InstituteName" HeaderText="Institute Name">
                            <ItemStyle CssClass="ItemStylecss" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SubjectName" HeaderText="Subject Name">
                            <ItemStyle CssClass="ItemStylecss" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ResultName" HeaderText="Result Name">
                            <ItemStyle CssClass="ItemStylecss" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Marks" HeaderText="CGPA/Marks">
                            <ItemStyle CssClass="ItemStylecss" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DegreeTitle" HeaderText="Degree Title">
                            <ItemStyle CssClass="ItemStylecss" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PassedYear" HeaderText="Passing Year">
                            <ItemStyle CssClass="ItemStylecss" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IsMaxDegree" HeaderText="Is Max Degree">
                            <ItemStyle CssClass="ItemStylecss" Width="10%" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="DivCommand1">
                <div class="DivCommandL">
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px" 
                        CausesValidation="False" onclick="btnRefresh_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
