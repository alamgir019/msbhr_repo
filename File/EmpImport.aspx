<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpImport.aspx.cs" Inherits="File_EmpImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    <div>
        <table style="font-size:12px">
            <tr>
                <td>
                    Designation (4)
                </td>
                <td>
                    <asp:DropDownList ID="ddlDesig" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    Sub Department (7)
                </td>
                <td>
                    <asp:DropDownList ID="ddlSubDept" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    Clinic (9)
                </td>
                <td>
                    <asp:DropDownList ID="ddlClinic" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Location Category (11)
                </td>
                <td>
                    <asp:DropDownList ID="ddlLocationCategory" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    Organization (12)
                </td>
                <td>
                    <asp:DropDownList ID="ddlOrg" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                   Employee Type (15)
                </td>
                <td>
                    <asp:DropDownList ID="ddlEmpType" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                   Highest Education (20)
                </td>
                <td>
                    <asp:DropDownList ID="ddlEducation" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                   District (27)
                </td>
                <td>
                    <asp:DropDownList ID="ddlDistrict" runat="server">
                    </asp:DropDownList>
                </td>
                 <td>
                   Blood Group (27)
                </td>
                <td>
                    <asp:DropDownList ID="ddlBloodGroup" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                </td>
                <td>
                    <asp:Button ID="btnValidate" runat="server" Text="Validate" OnClick="btnValidate_Click" />
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <asp:GridView ID="grPayroll" runat="server" AutoGenerateColumns="true" ShowHeader="true" Font-Size="X-Small">
        </asp:GridView>
    </div>
    </form>
</body>
</html>
