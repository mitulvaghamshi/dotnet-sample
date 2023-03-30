<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SalarySlip._Default" %>

<%--Author: Mitul Vaghamshi--%>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <section class="row" aria-labelledby="appTitle">
            <h1 id="appTitle">Salary Slip</h1>
            <p class="lead">We help you to manage your income with SalarySlip Calculator. Stay updated with your benefits, taxes, pension and insurance deduction.</p>
        </section>
        <hr />
        <div id="container">
            <div class="row">
                <asp:Label ID="LabelSalary" runat="server" />
                <asp:TextBox ID="InputSalary" runat="server" placeholder="Enter gross salary (required)" />
            </div>
            <div class="row">
                <asp:Label ID="LabelBonus" class="" runat="server" />
                <asp:TextBox ID="InputBonus" runat="server" placeholder="Enter bonus amount (required)" />
            </div>
            <div class="row">
                <asp:Label ID="LabelAllowance" runat="server" />
                <asp:TextBox ID="InputAllowance" runat="server" placeholder="Enter allowance (required)" />
            </div>
            <div class="row">
                <asp:Label ID="LabelTax" runat="server" />
                <asp:TextBox ID="InputTax" runat="server" placeholder="Tax (Federal + Provincial)" />
            </div>
            <div class="row">
                <asp:Label ID="LabelCpp" runat="server" />
                <asp:TextBox ID="InputCpp" runat="server" placeholder="CPP deductions" />
            </div>
            <div class="row">
                <asp:Label ID="LabelEi" runat="server" />
                <asp:TextBox ID="InputEi" runat="server" placeholder="EI deductions" />
            </div>
            <div class="row">
                <asp:Label ID="LabelSalaryAfterTax" runat="server" />
                <asp:TextBox ID="InputSalaryAfterTax" runat="server" placeholder="Salary after tax" />
            </div>
            <div class="row">
                <asp:Label ID="LabelGender" runat="server" />
                <asp:DropDownList ID="DropDownListGender" runat="server">
                    <asp:ListItem Value="0" class="control">Male (0% tax relief)</asp:ListItem>
                    <asp:ListItem Value="1" class="control">Female (1% tax relief)</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="row">
                <asp:Label ID="LabelDependants" runat="server" />
                <asp:DropDownList ID="DropDownListDependants" runat="server">
                    <asp:ListItem Value="0" class="control">2 or none (no tax relief)</asp:ListItem>
                    <asp:ListItem Value="2" class="control">3 only (2% relief)</asp:ListItem>
                    <asp:ListItem Value="4" class="control">4 or more (4% relief)</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="row">
                <asp:Label ID="LabelTotal" runat="server" />
                <asp:TextBox ID="InputTotal" runat="server" placeholder="Final salary..." />
            </div>
            <hr />
            <div class="row">
                <asp:Button ID="InputCalculate" runat="server" OnClick="InputCalculate_Click"/>
            </div>
        </div>
    </main>
</asp:Content>
