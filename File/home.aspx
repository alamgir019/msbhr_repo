<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="home.aspx.cs" Inherits="home" Title="HR & Payroll System" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlDashBoard" runat="server">
        <script type="text/javascript" src="../JScripts/jquery.min.js"></script>
        <script type="text/javascript">
            jQuery(document).ready(function () {
                jQuery(".content").hide();
                //toggle the componenet with class msg_body
                jQuery(".heading").click(function () {
                    jQuery(this).next(".content").slideToggle(1000);
                    return false;
                });
            });
        </script>
        <div class="dashBoard" id="dvMain" runat="server">
            <br />
            <br />
            <div class="layer1">
                <p class="heading">
                    Confirmation Date
                    <panel id="pnlConfirmation" runat="server">
                        <img src="../Images/Inbox 1.png" height="18px" width="18px" alt="" />
                        (<asp:Label ID="lblConfirmation" runat="server" Text=""></asp:Label>)
                    </panel>
                </p>
                <div class="content">
                    <asp:GridView ID="grConfirmation" runat="server" DataKeyNames="" AutoGenerateColumns="False"
                        EmptyDataText="No Record Found" Font-Size="9px" Width="100%">
                        <HeaderStyle BackColor="#B3CDE4" HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkBoxCDSelect" CssClass="gridCB" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmpId" HeaderText="EmpId">
                                <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FullName" HeaderText="Name">
                                <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JobTitleName" HeaderText="Job Title">
                                <ItemStyle CssClass="ItemStylecss" Width="25%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="DeptName" HeaderText="Dept Name">
                                <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SupervisorId" HeaderText="Supervisor ID">
                                <ItemStyle CssClass="ItemStylecss" Width="7%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ConfirmationDate" HeaderText="Confirmation Date">
                                <ItemStyle CssClass="ItemStylecss1" HorizontalAlign="Center" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalDays" HeaderText="Ramaining Days">
                                <ItemStyle CssClass="ItemStylecss1" HorizontalAlign="Center" Width="10%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <%--</div>
                    <div style="float:right">--%>
                    <asp:Button runat="server" ID="btnSendEmailConfrm" ToolTip="Send Email to the Supervisor"
                        Text="Send Mail" OnClick="btnSendEmailConfrm_Click" Visible="False" /></div>
                <p class="heading">
                    Retirement Date
                    <panel id="pnlRetirement" runat="server">
                        <img src="../Images/Inbox 1.png" height="18px" width="18px" alt="" />
                        (<asp:Label ID="lblRetirementDate" runat="server"></asp:Label>)
                    </panel>
                </p>
                <div class="content">
                    <asp:GridView ID="grRetirementDate" runat="server" DataKeyNames="" AutoGenerateColumns="False"
                        EmptyDataText="No Record Found" Font-Size="9px" Width="100%">
                        <HeaderStyle BackColor="#B3CDE4" HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:BoundField DataField="EmpId" HeaderText="EmpId">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FullName" HeaderText="Name">
                                <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JobTitleName" HeaderText="Job Title">
                                <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="DeptName" HeaderText="Dept Name">
                                <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="RetirementDate" HeaderText="Retirement Date">
                                <ItemStyle CssClass="ItemStylecss1" HorizontalAlign="Center" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalDays" HeaderText="Ramaining Days">
                                <ItemStyle CssClass="ItemStylecss1" HorizontalAlign="Center" Width="10%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
                <p class="heading">
                    Birth Day
                    <panel id="pnlBirthDay" runat="server">
                        <img src="../Images/Inbox 1.png" height="18px" width="18px" alt="" />
                        (<asp:Label ID="lblBirthday" runat="server" Text=""></asp:Label>)
                    </panel>
                </p>
                <div class="content">
                    <asp:GridView ID="grBirthday" runat="server" DataKeyNames="" AutoGenerateColumns="False"
                        EmptyDataText="No Record Found" Font-Size="9px" Width="100%">
                        <HeaderStyle BackColor="#B3CDE4" HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:BoundField DataField="EmpId" HeaderText="EmpId">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FullName" HeaderText="Name">
                                <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JobTitleName" HeaderText="Job Title">
                                <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="DeptName" HeaderText="Dept Name">
                                <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="DOB" HeaderText="Birth Date">
                                <ItemStyle CssClass="ItemStylecss1" HorizontalAlign="Center" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalDays" HeaderText="Ramaining Days">
                                <ItemStyle CssClass="ItemStylecss1" HorizontalAlign="Center" Width="10%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
                <p class="heading">
                    License Renew Date
                    <panel id="pnlLicense" runat="server">
                        <img src="../Images/Inbox 1.png" height="18px" width="18px" alt="" />
                        (<asp:Label ID="lblLicense" runat="server" Text=""></asp:Label>)
                    </panel>
                </p>
                <div class="content">
                    <asp:GridView ID="grLicense" runat="server" DataKeyNames="" AutoGenerateColumns="False"
                        EmptyDataText="No Record Found" Font-Size="9px" Width="100%">
                        <HeaderStyle BackColor="#B3CDE4" HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:BoundField DataField="EmpId" HeaderText="EmpId">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FullName" HeaderText="Name">
                                <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JobTitleName" HeaderText="Job Title">
                                <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="DeptName" HeaderText="Dept Name">
                                <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="LicenseRenewDate" HeaderText="Renew Date">
                                <ItemStyle CssClass="ItemStylecss1" HorizontalAlign="Center" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalDays" HeaderText="Ramaining Days">
                                <ItemStyle CssClass="ItemStylecss1" HorizontalAlign="Center" Width="10%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
                <p class="heading">
                    Additional Responsibility
                    <panel id="pnlAddResponsibility" runat="server">
                        <img src="../Images/Inbox 1.png" height="18px" width="18px" alt="" />
                        (<asp:Label ID="lblAddResponsibility" runat="server" Text=""></asp:Label>)
                    </panel>
                </p>
                <div class="content">
                    <asp:GridView ID="grAddResponsibility" runat="server" DataKeyNames="" AutoGenerateColumns="False"
                        EmptyDataText="No Record Found" Font-Size="9px" Width="100%">
                        <HeaderStyle BackColor="#B3CDE4" HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:BoundField DataField="EmpId" HeaderText="EmpId">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FullName" HeaderText="Name">
                                <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JobTitleName" HeaderText="Job Title">
                                <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="DeptName" HeaderText="Dept Name">
                                <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="AddResponseEndDate" HeaderText="End Date">
                                <ItemStyle CssClass="ItemStylecss1" HorizontalAlign="Center" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalDays" HeaderText="Ramaining Days">
                                <ItemStyle CssClass="ItemStylecss1" HorizontalAlign="Center" Width="10%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
                <p class="heading">
                    Retrenchment Date
                    <panel id="pnlRetrenchmentDate" runat="server">
                        <img src="../Images/Inbox 1.png" height="18px" width="18px" alt="" />
                        (<asp:Label ID="lblRetrenchmentDate" runat="server" Text=""></asp:Label>)
                    </panel>
                </p>
                <div class="content">
                    <asp:GridView ID="grRetrenchmentDate" runat="server" DataKeyNames="" AutoGenerateColumns="False"
                        EmptyDataText="No Record Found" Font-Size="9px" Width="100%">
                        <HeaderStyle BackColor="#B3CDE4" HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:BoundField DataField="EmpId" HeaderText="EmpId">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FullName" HeaderText="Name">
                                <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JobTitleName" HeaderText="Job Title">
                                <ItemStyle CssClass="ItemStylecss" Width="30%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="DeptName" HeaderText="Dept Name">
                                <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="RetirementDate" HeaderText="Retrenchment Date">
                                <ItemStyle CssClass="ItemStylecss1" HorizontalAlign="Center" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalDays" HeaderText="Ramaining Days">
                                <ItemStyle CssClass="ItemStylecss1" HorizontalAlign="Center" Width="10%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
                <p class="heading">
                    Contract Extension
                    <panel id="pnlContractExtension" runat="server">
                        <img src="../Images/Inbox 1.png" height="18px" width="18px" alt="" />
                        (<asp:Label ID="lblContractExtension" runat="server" Text=""></asp:Label>)
                    </panel>
                </p>
                <%--Mamun--%>
                <div class="content">
                    <asp:GridView ID="grContractExtension" runat="server" EmptyDataText="No Record Found"
                        DataKeyNames="SupervisorId,SupervisorName,ContractEndDate,JoiningDate,SeparateDate,Gender"
                        Font-Size="9px" Width="100%" AutoGenerateColumns="False">
                        <HeaderStyle BackColor="#B3CDE4" HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkBoxCESelect" CssClass="gridCB" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmpId" HeaderText="EmpId">
                                <ItemStyle CssClass="ItemStylecss" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FullName" HeaderText="Name">
                                <ItemStyle CssClass="ItemStylecss" Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="JobTitleName" HeaderText="Job Title">
                                <ItemStyle CssClass="ItemStylecss" Width="25%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DeptName" HeaderText="Dept Name">
                                <ItemStyle CssClass="ItemStylecss" Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SupervisorId" HeaderText="Supervisor ID">
                                <ItemStyle CssClass="ItemStylecss" Width="7%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ContractEndDate" HeaderText="Expire Date">
                                <ItemStyle CssClass="ItemStylecss1" HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalDays" HeaderText="Ramaining Days">
                                <ItemStyle CssClass="ItemStylecss1" HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <asp:Button runat="server" ID="btnContractExtEmail" Text="Send Email" ToolTip="Click to inform the supervisor"
                        OnClick="btnContractExtEmail_Click" Visible="False" />
                </div>
                <p class="heading">
                    Festival Date
                    <panel id="pnlFestivalDate" runat="server">
                        <img src="../Images/Inbox 1.png" height="18px" width="18px" alt="" />
                        (<asp:Label ID="lblFestivalDate" runat="server" Text=""></asp:Label>)
                    </panel>
                </p>
                <div class="content">
                    <asp:GridView ID="grFestivalDate" runat="server" DataKeyNames="" AutoGenerateColumns="False"
                        EmptyDataText="No Record Found" Font-Size="9px" Width="100%">
                        <HeaderStyle BackColor="#B3CDE4" HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:BoundField DataField="HolidayYear" HeaderText="Festival Year">
                                <ItemStyle CssClass="ItemStylecss1" HorizontalAlign="Center" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="HolidayName" HeaderText="Festival Name">
                                <ItemStyle CssClass="ItemStylecss" Width="40%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="StartDate" HeaderText="Festival Start">
                                <ItemStyle CssClass="ItemStylecss1" HorizontalAlign="Center" Width="30%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalDays" HeaderText="Ramaining Days">
                                <ItemStyle CssClass="ItemStylecss1" HorizontalAlign="Center" Width="15%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
                <p class="heading">
                    Professinal Certificate Renewal Date
                    <panel id="pnlProfCertDate" runat="server">
                        <img src="../Images/Inbox 1.png" height="18px" width="18px" alt="" />
                        (<asp:Label ID="lblProfCertDate" runat="server" Text=""></asp:Label>)
                    </panel>
                </p>
                <div class="content">
                    <asp:GridView ID="grProfCertDate" runat="server" DataKeyNames="" AutoGenerateColumns="False"
                        EmptyDataText="No Record Found" Font-Size="9px" Width="100%">
                        <HeaderStyle BackColor="#B3CDE4" HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkBoxCDSelect" CssClass="gridCB" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmpId" HeaderText="EmpId">
                                <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FullName" HeaderText="Name">
                                <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JobTitleName" HeaderText="Job Title">
                                <ItemStyle CssClass="ItemStylecss" Width="25%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="DeptName" HeaderText="Dept Name">
                                <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SupervisorId" HeaderText="Supervisor ID">
                                <ItemStyle CssClass="ItemStylecss" Width="7%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="BMDCRegDate" HeaderText="BMDC Reg. Date">
                                <ItemStyle CssClass="ItemStylecss1" HorizontalAlign="Center" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalDays" HeaderText="Ramaining Days">
                                <ItemStyle CssClass="ItemStylecss1" HorizontalAlign="Center" Width="10%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <%--</div>
                    <div style="float:right">--%>
                    <asp:Button runat="server" ID="Button1" ToolTip="Send Email to the Supervisor" Text="Send Mail"
                        OnClick="btnSendEmailConfrm_Click" Visible="False" /></div>
                <p class="heading">
                    Driving License Renewal Date
                    <panel id="pnlDrivingLicense" runat="server">
                        <img src="../Images/Inbox 1.png" height="18px" width="18px" alt="" />
                        (<asp:Label ID="lblrDrivingLicense" runat="server" Text=""></asp:Label>)
                    </panel>
                </p>
                <div class="content">
                    <asp:GridView ID="grDrivingLicense" runat="server" DataKeyNames="" AutoGenerateColumns="False"
                        EmptyDataText="No Record Found" Font-Size="9px" Width="100%">
                        <HeaderStyle BackColor="#B3CDE4" HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkBoxCDSelect" CssClass="gridCB" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmpId" HeaderText="EmpId">
                                <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FullName" HeaderText="Name">
                                <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JobTitleName" HeaderText="Job Title">
                                <ItemStyle CssClass="ItemStylecss" Width="25%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="DeptName" HeaderText="Dept Name">
                                <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SupervisorId" HeaderText="Supervisor ID">
                                <ItemStyle CssClass="ItemStylecss" Width="7%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="LicenseRenewDate" HeaderText="License Renew Date">
                                <ItemStyle CssClass="ItemStylecss1" HorizontalAlign="Center" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalDays" HeaderText="Ramaining Days">
                                <ItemStyle CssClass="ItemStylecss1" HorizontalAlign="Center" Width="10%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <%--</div>
                    <div style="float:right">--%>
                    <asp:Button runat="server" ID="Button2" ToolTip="Send Email to the Supervisor" Text="Send Mail"
                        OnClick="btnSendEmailConfrm_Click" Visible="False" /></div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
