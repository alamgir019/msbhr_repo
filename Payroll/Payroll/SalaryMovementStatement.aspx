<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="SalaryMovementStatement.aspx.cs" Inherits="Payroll_Payroll_SalaryMovementStatement"
    Title="Salary Movement Statement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
  window.onload=SearchByChanged;

        function SearchByChanged()
        {
            var ddlSB = document.getElementById('<%= ddlGeneratefor.ClientID %>');
            var ddlB=document.getElementById('<%=ddlBank.ClientID%>');
            var myindex  = ddlSB.selectedIndex;
            var SelValue = ddlSB.options[myindex].value;
            if(SelValue=="A")
            {  
                ddlB.disabled=true;          
            }
            if(SelValue=="B")
            {  
                ddlB.disabled=false; 
            }  
         }
    
    </script>

    <div class="formStyle" style="height: 200px; width: 50%">
        <div id="formhead4">
            Movement Report</div>
        <div style="margin-top: 10px;">
            <fieldset>
                <table>
                    <tr>
                        <td class="textlevel">
                            Generate For
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlGeneratefor" runat="server" Width="130px" CssClass="textlevelleft"
                                onchange="SearchByChanged()">
                                <asp:ListItem Value="B">Bank Wise</asp:ListItem>
                                <asp:ListItem Value="A">All</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Bank
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlBank" runat="server" Width="300px" CssClass="textlevelleft"
                                Font-Size="10px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Month
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" CssClass="textlevelleft" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Year
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="textlevelleft" Width="100px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            <asp:Button ID="btnPreview" runat="server" Text="Print Preview" Width="120px" OnClick="btnPreview_Click" /></td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>
</asp:Content>
