<%@ Page Title="Salary Slip - About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="SalarySlip.About" %>

<%--Author: Mitul Vaghamshi--%>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %>.</h2>
        <br />
        <h5>Application Requirements</h5>
        <ol>
            <li>Take gross salary from user.</li>
            <li>Apply deductions as EI, CPP and IncomeTex as percentages.</li>
            <li>Add Bonus and Allowance in dollars from users.</li>
            <li>Apply deduction of 1% IncomeTex for female only.</li>
            <li>Apply conditions on dependants as:</li>
            <li>2 dependants no deductions in IncomeTex.</li>
            <li>3 dependants deduction of 2%.</li>
            <li>4 dependants deduction of 4%.</li>
        </ol>
        <h3>References:</h3>
        <ul>
            <li><a href="https://www.wealthsimple.com/en-ca/tool/tax-calculator/ontario">wealthsimple.com</a></li>
            <li><a href="https://filingtaxes.ca/why-are-bonuses-taxed-so-high-in-ontario">filingtaxes.ca</a></li>
            <li><a href="https://www.canada.ca/en/revenue-agency/services/tax/businesses/topics/payroll/payroll-deductions-contributions/employment-insurance-ei/ei-premium-rates-maximums.html">employment-insurance-ei - CRA</a></li>
            <li><a href="https://www.canada.ca/en/revenue-agency/services/tax/businesses/topics/payroll/payroll-deductions-contributions/canada-pension-plan-cpp/checking-amount-cpp-you-deducted.html">canada-pension-plan-cpp - CRA</a></li>
        </ul>
    </main>
</asp:Content>
