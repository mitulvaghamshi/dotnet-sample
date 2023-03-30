<%@ Page Title="Active Purchage Orders" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ActivePurchageOrders.aspx.cs" Inherits="HospitalApp.ActivePurchageOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <p>
        <asp:DropDownList ID="DepartmentDropDownList" runat="server" DataSourceID="DepartmentSqlDataSource" DataTextField="department_name" DataValueField="department_id" AutoPostBack="True"></asp:DropDownList>
        <asp:SqlDataSource runat="server" ID="DepartmentSqlDataSource" ConnectionString='<%$ ConnectionStrings:CHDBConnectionString %>' SelectCommand="SELECT [department_id], [department_name] FROM [departments]"></asp:SqlDataSource>
    </p>
    <asp:GridView ID="PurchaseOrderGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="purchase_order_id" DataSourceID="PurchaseOrderSqlDataSource">
        <Columns>
            <asp:BoundField DataField="purchase_order_id" HeaderText="purchase_order_id" ReadOnly="True" SortExpression="purchase_order_id"></asp:BoundField>
            <asp:BoundField DataField="order_date" HeaderText="order_date" SortExpression="order_date"></asp:BoundField>
            <asp:BoundField DataField="vendor_id" HeaderText="vendor_id" SortExpression="vendor_id"></asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource runat="server" ID="PurchaseOrderSqlDataSource" ConnectionString='<%$ ConnectionStrings:CHDBConnectionString %>' SelectCommand="SELECT [purchase_order_id], [order_date], [vendor_id] FROM [purchase_orders] WHERE (([department_id] = @department_id) AND ([order_status] = @order_status))">
        <SelectParameters>
            <asp:ControlParameter ControlID="DepartmentDropDownList" PropertyName="SelectedValue" Name="department_id" Type="Int32"></asp:ControlParameter>
            <asp:Parameter DefaultValue="ACTIVE" Name="order_status" Type="String"></asp:Parameter>
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
