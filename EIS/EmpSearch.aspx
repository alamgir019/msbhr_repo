<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="EmpSearch.aspx.cs" Inherits="EIS_EmpSearch" Title="Employee Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        //window.onload=SearchByChanged;
        function SearchByChanged() {
            var ddlSB = document.getElementById('<%= ddlSearchBy.ClientID %>');
            var pnlV = document.getElementById('<%=pnlValue.ClientID%>');
            var lblV = document.getElementById('<%=lblValue.ClientID %>');
            var pnlD = document.getElementById('<%=pnlDivision.ClientID%>');
            var txtV = document.getElementById('<%=txtSearchValue.ClientID%>');
            var myindex = ddlSB.selectedIndex
            var SelValue = ddlSB.options[myindex].value
            if (SelValue == "0") {
                pnlV.style.visibility = "hidden";
                pnlD.style.visibility = "visible";
                txtV.value = "";
            }
            if (SelValue == "2") {
                pnlV.style.visibility = "visible";
                pnlD.style.visibility = "hidden";
                lblV.innerHTML = "Full Name :";
            }
            if (SelValue == "3") {
                pnlV.style.visibility = "visible";
                pnlD.style.visibility = "hidden";
                lblV.innerHTML = "Cell No :";
            }
            if (SelValue == "4") {
                pnlV.style.visibility = "visible";
                pnlD.style.visibility = "hidden";
                lblV.innerHTML = "Abb. Name :";
            }
            if (SelValue == "5") {
                pnlV.style.visibility = "visible";
                pnlD.style.visibility = "hidden";
                lblV.innerHTML = "Emp. No :";
            }
        }

        function ValidateEmpNo() {
            var ddlSB = document.getElementById('<%= ddlSearchBy.ClientID %>');
            var txtV = document.getElementById('<%=txtSearchValue.ClientID%>');
            var myindex = ddlSB.selectedIndex
            var SelValue = ddlSB.options[myindex].value

            if (SelValue == "1") {
                if (txtV.value = '') {
                    return false;
                }
            }
            return true;
        }
        function TABLE1_onclick() {

        }

    </script>
    <div class="empSearchForm" style="width: 98%;">
        <div id="formhead2">
            <div style="width: 98%; float: left;">
                Employee Search</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div style="margin-left: 15px; margin-right: 15px;">
            <fieldset>
                <div>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Serach By :" CssClass="textlevel"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSearchBy" runat="server" Width="127px" onchange="SearchByChanged()"
                                    CssClass="textlevelleft">
                                    <asp:ListItem Value="0">Office</asp:ListItem>
                                    <asp:ListItem Value="5">Emp. No</asp:ListItem>
                                    <asp:ListItem Value="2">Full Name</asp:ListItem>
                                    <asp:ListItem Value="3">Cell No</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Panel ID="pnlValue" runat="server" Height="25px" Width="125px">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblValue" runat="server" CssClass="textlevel" Text="Enter Value :"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSearchValue" runat="server" Width="229px"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div>
                                <div style="float: left; height: 91px; text-align: left">
                                    <asp:Panel ID="pnlDivision" runat="server">
                                        <table id="TABLE1" onclick="return TABLE1_onclick()">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text="Company :" CssClass="textlevel"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCompany" runat="server" Width="250px" AutoPostBack="True"
                                                            CssClass="textlevelleft">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" Text="Project :" CssClass="textlevel"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="textlevelleft" Width="250px">
                                                            <asp:ListItem Value="-1">All</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblGrade" runat="server" Text="Grade :" CssClass="textlevel"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlGrade" runat="server" CssClass="textlevelleft" Width="250px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDesig" runat="server" Text="Designation :" CssClass="textlevel"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlDesig" runat="server" Width="250px" CssClass="textlevelleft">
                                                            <asp:ListItem Value="-1">All</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" Text="Department :" CssClass="textlevel"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlDept" runat="server" Width="250px" CssClass="textlevelleft">
                                                            <asp:ListItem Value="-1">All</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label6" runat="server" Text="Employee Type :" CssClass="textlevel"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlEmploymentType" runat="server" CssClass="textlevelleft"
                                                            Width="250px" AutoPostBack="True">
                                                            <asp:ListItem Value="-1">All</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label3" runat="server" Text="Employee Status :" CssClass="textlevel"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlEmpStatus" runat="server" CssClass="textlevelleft" Width="250px">
                                                            <asp:ListItem Value="A">Active</asp:ListItem>
                                                            <asp:ListItem Value="I">In-Active</asp:ListItem>
                                                            <asp:ListItem Value="-1">All</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </asp:Panel>
                                </div>
                                <div id="DivCommand1">
                                    <div style="float: right" id="DivCommandR">
                                        <asp:Button ID="btnShow" OnClick="btnShow_Click" runat="server" Text="Show" Width="80px"
                                            OnClientClick="return ValidateEmpNo();"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </fieldset>
        </div>
        <div style="margin-left: 15px; margin-top: 10px;">
            <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click">Export to Excel</asp:LinkButton>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div id="empSearchResult">                    
                    <asp:GridView ID="grEmployee" runat="server" Font-Size="9px" AutoGenerateColumns="False"
                        Width="130%" DataKeyNames="" EmptyDataText="No Record Found">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                        <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingRowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:BoundField HeaderText="SL.No">
                                <ItemStyle CssClass="ItemStylecss" Width="2%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EmpId" HeaderText="Emp Id">
                                <ItemStyle CssClass="ItemStylecss" Width="4%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FullName" HeaderText="Employee Name">
                                <ItemStyle CssClass="ItemStylecss" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DesigName" HeaderText="Designation">
                                <ItemStyle CssClass="ItemStylecss" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DeptName" HeaderText="Department">
                                <ItemStyle CssClass="ItemStylecss" Width="9%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DivisionName" HeaderText="Company Name">
                                <ItemStyle CssClass="ItemStylecss" Width="8%" />
                            </asp:BoundField>
                             <asp:BoundField DataField="ProjectName" HeaderText="Project Name">
                                <ItemStyle CssClass="ItemStylecss" Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GradeName" HeaderText="Grade">
                                <ItemStyle CssClass="ItemStylecss" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="JoiningDate" HeaderText="Joining Date">
                                <ItemStyle CssClass="ItemStylecss" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SalLocName" HeaderText="Salary Location">
                                <ItemStyle CssClass="ItemStylecss" Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SPTitle" HeaderText="Salary Package">
                                <ItemStyle CssClass="ItemStylecss" Width="12%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SupervisorId" HeaderText="Supervisor Id">
                                <ItemStyle CssClass="ItemStylecss" Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RoutingNo" HeaderText="Routing No">
                                <ItemStyle CssClass="ItemStylecss" Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BankAccNo" HeaderText="Account No">
                                <ItemStyle CssClass="ItemStylecss" Width="5%" />
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="LPackName" HeaderText="Leave Package">
                                <ItemStyle CssClass="ItemStylecss" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NationalId" HeaderText="National ID">
                                <ItemStyle CssClass="ItemStylecss" Width="5%" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="CellPhone" HeaderText="Mobile No">
                                <ItemStyle CssClass="ItemStylecss" Width="6%" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div style="margin-left: 10px;">
                    <table>
                        <tbody>
                            <tr>
                                <td style="width: 3px">
                                    <asp:Label ID="lblRecCount" runat="server" Text="Total Record Count:" CssClass="textlevel"></asp:Label>
                                </td>
                                <td style="width: 3px">
                                    <asp:Label ID="lblRecordCount" runat="server" ForeColor="Blue" Font-Size="Smaller"
                                        Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
    <br />
</asp:Content>
