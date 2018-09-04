<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImportTool.aspx.cs" Inherits="File_ImportTool" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Import Tool</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    <div style="background-color:Gray;">
       <fieldset>
            <legend>Referencial Data List</legend>
            <table style="color:White;font-weight:bold;">
                <tr>
                    <td>Designation</td>
                    <td><asp:DropDownList ID="ddlDesig" runat="server"></asp:DropDownList></td>
                    <td>Project</td>
                    <td><asp:DropDownList ID="ddlProject" runat="server"></asp:DropDownList></td>
                    <td>Office Location</td>
                    <td><asp:DropDownList ID="ddlDivision" runat="server"></asp:DropDownList></td>
                    <td>Team Office Location</td>
                    <td><asp:DropDownList ID="ddlLocation" runat="server"></asp:DropDownList></td>
                    <td>Bank</td>
                    <td><asp:DropDownList ID="ddlBank" runat="server"></asp:DropDownList></td>
                    <td>Excel Team Location</td>
                    <td><asp:DropDownList ID="ddlLocationExcel" runat="server"></asp:DropDownList></td>
                </tr>
            </table>
       </fieldset>
    </div>
    <div>
        <table>
            <tr>
                <td>
                     <asp:Button ID="btnSync" runat="server" Text="Synchronize" OnClick="btnSync_Click"/>
                </td>
                <td>
                      <asp:Button ID="btnUploadSalaryPackageTitle" runat="server" 
                          Text="Upload SalaryPackageTitle" OnClick="btnUploadSalaryPackageTitle_Click" /> 
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnUpdateFlag0" runat="server" Text="Update Salary Package" 
                        OnClick="btnUpdateFlag_Click" />
                </td>
                <td>
                    <asp:Button ID="btnInsSalPakHisDetls" runat="server" Text="Insert Salary Package" 
                        OnClick="btnInsSalPakHisDetls_Click" />
                </td>
                <td>
                    &nbsp;</td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                
            </tr>
            <tr>
                <td>
                     &nbsp;</td>
                <td>
                      <asp:Button ID="btnUploadSalaryPackageTitle0" runat="server" 
                          Text="Upload SalaryPackage Dtls" OnClick="btnUploadSalaryPackageTitle0_Click" /> 
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnGenerateSalPakId" runat="server" Text="Generate Sal Pak Id" 
                        OnClick="btnGenerateSalPakId_Click" />
                </td>
                <td>
                    <asp:Button ID="btnUpdatePack" runat="server" Text="Update Salary Package" 
                        OnClick="btnUpdatePack_Click" />
                </td>
                <td>
                    &nbsp;</td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                
            </tr>
            <tr>
                <td>
                     &nbsp;</td>
                <td>
                      <asp:Button ID="btnUploadSalaryHisPackageTitle1" runat="server" 
                          Text="Upload SalaryPackage His Dtls" OnClick="btnUploadSalaryHisPackageTitle1_Click" Visible="False" /> 
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnGenerateLogId" runat="server" Text="Generate Log Id" 
                        OnClick="btnGenerateLogId_Click" Visible="False" />
                </td>
                <td>
                    <asp:Button ID="btnUpdateHisPack" runat="server" Text="Update Salary His Package" 
                        OnClick="btnUpdateHisPack_Click" />
                </td>
                <td>
                    &nbsp;</td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                
            </tr>
            <tr>
                <td>
                     &nbsp;</td>
                <td>
                      <asp:Button ID="btnUploadSalaryHisPackageTitleInc" runat="server" 
                          Text="Upload SalaryPackage His Dtls Inc" OnClick="btnUploadSalaryHisPackageTitleInc_Click" /> 
                </td>
                <td style="width: 3px">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnUpdateMasterPack" runat="server" Text="Update Salary Master His Package " 
                        OnClick="btnUpdateMasterPack_Click" />
                </td>
                <td>
                    &nbsp;</td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                      <asp:Button ID="btnSalaryAmt" runat="server" Text="Upload To Salary Amt Grid" 
                        OnClick="btnSalaryAmt_Click" /> 
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnUpdateFlag1" runat="server" Text="Update Salary Package Amt" 
                        OnClick="btnUpdateFlag1_Click" />
                    </td>
                <td style="width: 3px">
                    <asp:Button ID="btnInsertOtherINfo" runat="server" Text="Insert Other Info" 
                        OnClick="btnInsertOtherINfo_Click" />
                    </td>
                    
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                      <asp:Button ID="btnUploadEmpSalPakId" runat="server" 
                        Text="Upload Emp Sal Pak Id" OnClick="btnUploadEmpSalPakId_Click" /> 
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnUpdateEmpSalPakId" runat="server" Text="Update Emp Sal Pak Id" 
                        OnClick="btnUpdateEmpSalPakId_Click" />
                    </td>
                <td style="width: 3px">
                    &nbsp;</td>
                    
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                      <asp:Button ID="btnUploadBankCodeRouting" runat="server" 
                        Text="Upload BankCode &amp; Routing" 
                          OnClick="btnUploadBankCodeRouting_Click" /> 
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnUpdateBankCodeRouting" runat="server" Text="UpdateBankCodeRouting" 
                        OnClick="btnUpdateBankCodeRouting_Click" />
                    </td>
                <td style="width: 3px">
                    <asp:Button ID="btnUpdateBankCode" runat="server" Text="Update Bank Code" 
                        OnClick="btnUpdateBankCode_Click" />
                    </td>
                    
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                      <asp:Button ID="btnUploadBankId" runat="server" 
                        Text="Upload BankId" 
                          OnClick="btnUploadBankId_Click" /> 
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnUpdateBankId" runat="server" Text="UpdateBankId" 
                        OnClick="btnUpdateBankId_Click" />
                    </td>
                <td style="width: 3px">
                    &nbsp;</td>
                    
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                      <asp:Button ID="btnUploadJobTitle" runat="server" 
                        Text="Upload Job Title" 
                          OnClick="btnUploadJobTitle_Click" /> 
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnUpdateJobTilte" runat="server" Text="Update JobTitle" 
                        OnClick="btnUpdateJobTilte_Click" />
                    </td>
                <td style="width: 3px">
                    &nbsp;</td>
                    
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                      <asp:Button ID="btnUploadTax" runat="server" 
                        Text="Upload Tax" 
                          OnClick="btnUploadTax_Click" /> 
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnUpdateTax" runat="server" Text="Update Tax" 
                        OnClick="btnUpdateTax_Click" />
                    </td>
                <td style="width: 3px">
                      <asp:Button ID="btnUploadSalPackDate" runat="server" 
                        Text="Upload SalPackdate" 
                          OnClick="btnUploadSalPackDate_Click" /> 
                </td>
                    
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                      <asp:Button ID="btnUploadPF" runat="server" 
                        Text="Upload PF" 
                          OnClick="btnUploadPF_Click" /> 
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnUpdatePF" runat="server" Text="Update PF" 
                        OnClick="btnUpdatePF_Click" />
                    </td>
                <td style="width: 3px">
                    <asp:Button ID="btnUpdateSalPackdate" runat="server" Text="Update SalPackDate" 
                        OnClick="btnUpdateSalPackdate_Click" />
                    </td>
                    
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                      <asp:Button ID="btnUploadPFLoan" runat="server" 
                        Text="Upload PFLoan" 
                          OnClick="btnUploadPFLoan_Click" /> 
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnUpdatePFLoan" runat="server" Text="Update PFLoan" 
                        OnClick="btnUpdatePFLoan_Click" />
                    </td>
                <td style="width: 3px">
                    &nbsp;</td>
                    
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                      <asp:Button ID="btnUploadJobTitleDes" runat="server" 
                        Text="Upload JobTitle" onclick="btnUploadJobTitleDes_Click"/> 
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnUpdateJobTitleDes" runat="server" Text="Update JobTitle Wise Desig" 
                        OnClick="btnUpdateJobTitleDes_Click" />
                    </td>
                <td style="width: 3px">
                      <asp:Button ID="btnUploadInc" runat="server" 
                        Text="Upload Incdate" 
                          OnClick="btnUploadInc_Click" /> 
                </td>
                    
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                      <asp:Button ID="btnUploadSecWsDept" runat="server" 
                        Text="Upload SecWsDept" onclick="btnUploadSecWsDept_Click"/> 
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnUpdateSecWsDept" runat="server" Text="Update SecWsDept" onclick="btnUpdateSecWsDept_Click" 
                         />
                    </td>
                <td style="width: 3px">
                    <asp:Button ID="btnUpdateIncDate" runat="server" Text="Update IncDate" 
                        OnClick="btnUpdateIncDate_Click" />
                    </td>
                    
            </tr>
            <tr>
                <td>
                    </td>
                <td>
                    <asp:Button ID="btnUpdateFlag" runat="server" Text="Update Email &b Acc No" OnClick="btnUpdateFlag_Click" />
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnInsertSalPakBonusHeadId" runat="server" 
                        Text="Insert SalPak Bonus HeadId" OnClick="btnInsertSalPakBonusHeadId_Click" />
                </td>
                <td style="width: 3px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                      <asp:Button ID="btnUploadDistrcit" runat="server" 
                        Text="Upload District" onclick="btnUploadDistrcit_Click"/> 
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnUpdateDistrict" runat="server" Text="Update District" 
                        OnClick="btnUpdateDistrict_Click" />
                </td>
                <td style="width: 3px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                      <asp:Button ID="btnUploadBankAccNo" runat="server" 
                        Text="Upload BankAccNo" onclick="btnUploadBankAccNo_Click"/> 
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnUpdateBankAccNo" runat="server" Text="Update BankAccNo" 
                        OnClick="btnUpdateBankAccNo_Click" />
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnInsertSalaryHead" runat="server" Text="Insert PF Ar. &amp;Loan &amp; Festival Bonus Head" 
                        OnClick="btnInsertSalaryHead_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                      <asp:Button ID="btnUploadSupervisorId" runat="server" 
                        Text="Upload Supervisor Id" onclick="btnUploadSupervisorId_Click"/> 
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnUpdateSupervisorId" runat="server" Text="Update SupervisorId" 
                        OnClick="btnUpdateSupervisorId_Click" />
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnInsertArrSalaryHead" runat="server" Text="Insert Basic Arr &amp;HArr, MedArr &amp; PF Arr" 
                        OnClick="btnInsertArrSalaryHead_Click" BorderColor="#CC0066" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                      <asp:Button ID="btnSalPak" runat="server" Text="Upload SalPak" 
                        OnClick="btnSalPak_Click" BorderColor="#990099" /> 
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnUpdateSalPak" runat="server" Text="Update New Salary Package Amt" 
                        OnClick="btnUpdateSalPak_Click" />
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnUpdateBasicSalary" runat="server" Text="Update Basic Salary" 
                        OnClick="btnUpdateBasicSalary_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                      <asp:Button ID="btnCOLA" runat="server" Text="Upload COLA" 
                        OnClick="btnCOLA_Click" /> 
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnInsertCOLA" runat="server" Text="Insert COLA" 
                        OnClick="btnInsertCOLA_Click" />
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnUpdateEmpImage" runat="server" Text="Update Emp Image" 
                        OnClick="btnUpdateEmpImage_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                      <asp:Button ID="btnLvBalance" runat="server" Text="Upload Leave Balance" 
                        OnClick="btnLvBalance_Click" /> 
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnUpdateLvBalance" runat="server" Text="Update Leave Balance" 
                        OnClick="btnUpdateLvBalance_Click" />
                </td>
                <td style="width: 3px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                      <asp:Button ID="btnLvEntitle" runat="server" Text="Upload Leave Entitle" 
                        OnClick="btnLvEntitle_Click" /> 
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnUpdateLvEntitle" runat="server" Text="Update Leave Entitle" 
                        OnClick="btnUpdateLvEntitle_Click" />
                </td>
                <td style="width: 3px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                      <asp:Button ID="btnCostCenter" runat="server" Text="Upload CostCenter" 
                        OnClick="btnCostCenter_Click" /> 
                </td>
                <td style="width: 3px">
                    <asp:Button ID="btnUpdateCostCenter" runat="server" Text="Update CostCenter" 
                        OnClick="btnUpdateCostCenter_Click" />
                </td>
                <td style="width: 3px">
                    &nbsp;</td>
            </tr>
        </table>
       
        <br />
        <br />
        <asp:GridView ID="grPayroll" runat="server" AutoGenerateColumns="true" ShowHeader="true">
        </asp:GridView>
    </div>
    </form>
</body>
</html>
