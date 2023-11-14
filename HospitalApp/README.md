# Walkthrough 1 - Web Forms

## Setup

- Start Visual Studio and `Create a new project`.
- Set language to **C#**, project type to **Web** and search to **framework**.
- Select the `ASP.NET Web Application (.NET Framework)` template, click `Next`.
- Set Project name to **HospitalApp** and `Create`.
- Choose the `Web Forms` template, **unselect** `Configure for HTTPS`, click `Create`.
- This site has been styled with [Bootstrap](https://getbootstrap.com/docs).

## WebForm1.aspx

- In the `Solution Explorer`, right-click the project choose `Add / Web Form`, accept the name of **WebForm1**, click OK.
- If the `Toolbox` pane isn't visible, select it from the `View` menu.
- From the `Toolbox` in the `Standard` group, drag a `Button` between the `div` tags.

```html
<div>
  <asp:Button ID="Button1" runat="server" Text="Button" />
<div>
```

- Add a `<br />` tag, then drag a label after the `<br />`.

```html
<div>
  <asp:Button ID="Button1" runat="server" Text="Button" />
  <br />
  <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</div>
```

- Switch to `Design` view, by clicking the button at the bottom left of the editor.
- Double click the `Button`, you will be taken to the code editor with an event handler method stubbed out.

## WebForm1.aspx.cs

- Add the following line of code to the method.

```cs
protected void Button1_Click(object sender, EventArgs e)
{
    Label1.Text = "Set in Button1_Click";
}
```

- Run the site.
- Once the page loads, click the button and note that the label text changed.
- Close the Browser and stop the application from running if necessary by pressing **Shift+F5**.
- Add the following to the `Page_Load` method.

```cs
protected void Page_Load(object sender, EventArgs e)
{
    Label1.Text = "Set in Page_Load";
}
```

- Run the site.
- In the browser, view the page source and note the value of `__VIEWSTATE` variable.

## Setup - CHDB Database

- Start **SQL Server**. If you already have the **CHDB** database installed, you may skip this steps.
- Download and extract [CHDB.sql.zip](../resources/CHDB.sql.zip) to **Solution Explorer** root and open it.
- In the upper-left, click the `Execute` button.
- You will be prompted to connect to a database, set **Server Name** to `.\sqlexpress`, and click `Connect`.
- The script should complete, create the database and present a summary of the `INSERT` statements.

## Medications.aspx

- In **Solution Explorer**, right-click the project and select `Add / New Item...`.
- From the list choose `Web Form with Master Page`.
- Set the name to `Medications.aspx`, and click `Add`.
- Select `Site.Master`, and click `OK`.
- Set the page title to `Medications` as:

```html
<%@ Page Title="Medications" %>
```

- Add a heading between the `asp:Content` tags.

```html
<%@ Page Title="Medications" %>
<asp:Content runat="server" ID="Content1"
  ContentPlaceHolderID="MainContent">
  <h2><%: Title %></h2>
</asp:Content>
```

- From the **Toolbox** in the **Data** group, and drag a `GridView` after the heading.

```html
<%@ Page Title="Medications" %>
<asp:Content>
  <h2><%: Title %></h2>
  <asp:GridView runat="server" ID="GridView1"></asp:GridView>
</asp:Content>
```

- Rename the `GridView` to `medicationsGridView`.

```html
<asp:GridView runat="server"
  ID="medicationsGridView">
</asp:GridView>
```

- Click the `GridView`'s task button, in the `Choose Data Source` drop-down, select `<New Data Source...>`
- Select the `Database` source and set the `ID` of the source to `medicationsSqlDataSource`, and click `OK`.
- Click the `New Connection...` button.
- In the `Change Data Source` dialog, select `Microsoft SQL Server`, and click `OK`.
- Set **Server Name** to `.\sqlexpress`.
- Set the **Select** or enter a database name drop-down to `CHDB`, and click `OK`, then click `Next`.
- Save the **Connection String** in the application configuration file with the suggested name of `CHDBConnectionString`, and click `Next`.
- Select the `medications` table, leave columns as `*`, click the `Advanced...` button.
- Select `Generate INSERT, UPDATE and DELETE statements`, and click `OK`.
- Click `Next`, then click `Finish`.
- The `GridView` and `SqlDataSource` will look approximately like this:

```html
<asp:GridView runat="server"
  ID="medicationsGridView"
  AutoGenerateColumns="False"
  DataKeyNames="medication_id"
  DataSourceID="medicationsSqlDataSource">
  <Columns>
    <asp:BoundField> DataField="medication_id" HeaderText="medication_id" ReadOnly="True" SortExpression="medication_id"></asp:BoundField>
    <asp:BoundField> DataField="medication_description" HeaderText="medication_description" SortExpression="medication_description"></asp:BoundField>
    <asp:BoundField> DataField="medication_cost" HeaderText="medication_cost" SortExpression="medication_cost"></asp:BoundField>
    <asp:BoundField> DataField="package_size" HeaderText="package_size" SortExpression="package_size"></asp:BoundField>
    <asp:BoundField> DataField="strength" HeaderText="strength" SortExpression="strength"></asp:BoundField>
    <asp:BoundField> DataField="sig" HeaderText="sig" SortExpression="sig"></asp:BoundField>
    <asp:BoundField> DataField="units_used_ytd" HeaderText="units_used_ytd" SortExpression="units_used_ytd"></asp:BoundField>
    <asp:BoundField> DataField="last_prescribed_date" HeaderText="last_prescribed_date" SortExpression="last_prescribed_date"></asp:BoundField>
  </Columns>
</asp:GridView>
<asp:SqlDataSource runat="server"
  ID="medicationsSqlDataSource"
  ConnectionString='<%$ ConnectionStrings:CHDBConnectionString %>'
  DeleteCommand="DELETE FROM [medications] WHERE [medication_id] = @medication_id"
  InsertCommand="INSERT INTO [medications] ([medication_id], [medication_description], [medication_cost], [package_size], [strength], [sig], [units_used_ytd], [last_prescribed_date]) VALUES (@medication_id, @medication_description, @medication_cost, @package_size, @strength, @sig, @units_used_ytd, @last_prescribed_date)"
  SelectCommand="SELECT * FROM [medications]"
  UpdateCommand="UPDATE [medications] SET [medication_description] = @medication_description, [medication_cost] = @medication_cost, [package_size] = @package_size, [strength] = @strength, [sig] = @sig, [units_used_ytd] = @units_used_ytd, [last_prescribed_date] = @last_prescribed_date WHERE [medication_id] = @medication_id">
  <DeleteParameters>
    <asp:Parameter> Name="medication_id" Type="Int32"></asp:Parameter>
  </DeleteParameters>
  <InsertParameters>
    <asp:Parameter> Name="medication_id" Type="Int32"></asp:Parameter>
    <asp:Parameter> Name="medication_description" Type="String"></asp:Parameter>
    <asp:Parameter> Name="medication_cost" Type="Decimal"></asp:Parameter>
    <asp:Parameter> Name="package_size" Type="String"></asp:Parameter>
    <asp:Parameter> Name="strength" Type="String"></asp:Parameter>
    <asp:Parameter> Name="sig" Type="String"></asp:Parameter>
    <asp:Parameter> Name="units_used_ytd" Type="Int32"></asp:Parameter>
    <asp:Parameter> DbType="Date" Name="last_prescribed_date"></asp:Parameter>
  </InsertParameters>
  <UpdateParameters>
    <asp:Parameter> Name="medication_description" Type="String"></asp:Parameter>
    <asp:Parameter> Name="medication_cost" Type="Decimal"></asp:Parameter>
    <asp:Parameter> Name="package_size" Type="String"></asp:Parameter>
    <asp:Parameter> Name="strength" Type="String"></asp:Parameter>
    <asp:Parameter> Name="sig" Type="String"></asp:Parameter>
    <asp:Parameter> Name="units_used_ytd" Type="Int32"></asp:Parameter>
    <asp:Parameter> DbType="Date" Name="last_prescribed_date"></asp:Parameter>
    <asp:Parameter> Name="medication_id" Type="Int32"></asp:Parameter>
  </UpdateParameters>
</asp:SqlDataSource>
```

- Run the site.
- In the browser, view the page source and note the value of `__VIEWSTATE` variable, it is much larger now.
- Set the `CssClass` of the `GridView` to the **Bootstrap** class `table`.

```html
<asp:GridView runat="server" CssClass="table">
```

- Run the site.
- Change the class to `table-condensed`.

```html
<asp:GridView runat="server" CssClass="table-condensed">
```

- Run the site.
- Set the table rows to be `table-striped`.

```html
<asp:GridView runat="server"
  CssClass="table-condensed table-striped">
```

- Run the site.
- Set the stripes to the background info `bg-info` color to make them more visible.

```html
<asp:GridView runat="server"
  CssClass="table-condensed table-striped bg-info">
```

- Run the site.
- Click the `GridView`'s task button and click `Edit Columns...`.
- From **Selected** fields, select `medication_id` and **delete** it.
- Select `medication_description`, in `BoundField` properties, change `HeaderText` to `Description`.
  - Change `medication_cost` header to `Cost`.
  - Change `package_size` header to `Package Size`.
  - Change `strength` header to `Strength`.
  - Change `sig` header to `Sig`.
  - Change `units_used_ytd` header to `Units Used YTD`.
  - Change `last_prescribed_date` header to `Last Prescribed`.
- Click `OK`.
- Run the site.
- Edit the columns of the `GridView`.
- Right-align `Cost` and `Units Used YTD` by setting `ItemStyle` propertiy `HorizontalAlign` to `Right`.

```html
<asp:BoundField
  HeaderText="Cost"
  DataField="medication_cost"
  SortExpression="medication_cost">
  <ItemStyle> HorizontalAlign="Right"></ItemStyle>
</asp:BoundField>
```

- Set `Last Prescribed`s `DataFormatString` to `{0:d}`.

```html
<asp:BoundField
  HeaderText="Last Prescribed"
  DataField="last_prescribed_date"
  SortExpression="last_prescribed_date"
  DataFormatString="{0:d}">
</asp:BoundField>
```

- Set `Units Used YTD`s `DataFormatString` to `{0:n0}`.

```html
<asp:BoundField
  HeaderText="Units Used YTD"
  DataField="units_used_ytd"
  SortExpression="units_used_ytd"
  DataFormatString="{0:n0}">
    <ItemStyle> HorizontalAlign="Right"></ItemStyle>
</asp:BoundField>
```

- Click `OK`.
- Run the site.
- Click the `GridView`'s task button, select `Enable Paging`, `Enable Sorting` and `Enable Editing`.
- The `GridView` will look approximately like this:

```html
<asp:GridView runat="server"
  ID="medicationsGridView"
  AutoGenerateColumns="False"
  DataKeyNames="medication_id"
  DataSourceID="medicationsSqlDataSource"
  CssClass="table-condensed table-striped bg-info"
  AllowPaging="True" AllowSorting="True">
  <Columns>
    <asp:CommandField> ShowEditButton="True"></asp:CommandField>
    <asp:BoundField> DataField="medication_description" HeaderText="Description" SortExpression="medication_description"></asp:BoundField>
    <asp:BoundField DataField="medication_cost" HeaderText="Cost" SortExpression="medication_cost">
      <ItemStyle> HorizontalAlign="Right"></ItemStyle>
    </asp:BoundField>
    <asp:BoundField> DataField="package_size" HeaderText="Package Size" SortExpression="package_size"></asp:BoundField>
    <asp:BoundField> DataField="strength" HeaderText="Strength" SortExpression="strength"></asp:BoundField>
    <asp:BoundField> DataField="sig" HeaderText="Sig" SortExpression="sig"></asp:BoundField>
    <asp:BoundField DataField="units_used_ytd" HeaderText="Units Used YTD" SortExpression="units_used_ytd" DataFormatString="{0:n0}">
      <ItemStyle> HorizontalAlign="Right"></ItemStyle>
    </asp:BoundField>
    <asp:BoundField> DataField="last_prescribed_date" HeaderText="Last Prescribed" SortExpression="last_prescribed_date" DataFormatString="{0:d}"></asp:BoundField>
  </Columns>
</asp:GridView>
```

- Run the site.
- Notice the column headings are now links, experiment with them.
- Notice that only 10 rows are presented, experiment with the paging controls in the table footer.
- Edit a row and change a description.

## Site.Master

- Update the `title` tag.

```HTML
<%@ Master Language="C#"
  AutoEventWireup="true"
  CodeBehind="Site.master.cs"
  Inherits="HospitalApp.SiteMaster" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
  <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <title><%: Page.Title %> - Community Hospital</title>
  <asp:PlaceHolder runat="server">
    <%: Scripts.Render("~/bundles/modernizr") %>
  </asp:PlaceHolder>
  <webopt:bundlereference runat="server" path="~/Content/css" />
  <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
</head>
<!-- ... -->
```

- Update Application name and footer.

```html
<!-- ... -->
<div class="navbar navbar-inverse navbar-fixed-top">
  <div class="container">
    <div class="navbar-header">
      <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
        <span> class="icon-bar"></span>
        <span> class="icon-bar"></span>
        <span> class="icon-bar"></span>
      </button>
      <a> class="navbar-brand" runat="server" href="~/">Community Hospital</a>
    </div>
      <ul class="nav navbar-nav">
        <li><a> runat="server" href="~/">Home</a></li>
        <li><a> runat="server" href="~/About">About</a></li>
        <li><a> runat="server" href="~/Contact">Contact</a></li>
      </ul>
    <div class="navbar-collapse collapse">
    </div>
  </div>
</div>
<div class="container body-content">
  <asp:ContentPlaceHolder> ID="MainContent" runat="server"></asp:ContentPlaceHolder>
  <hr />
  <footer>
    <p>© <%: DateTime.Now.Year %> - Community Hospital</p>
  </footer>
</div>
<!-- ... -->
```

- Add menu entries for `Medications` and `WebForm1`.

```html
<!-- ... -->
<div class="navbar-collapse collapse">
  <ul class="nav navbar-nav">
    <li><a runat="server" href="~/">Home</a></li>
    <li><a runat="server" href="~/Medications">Medications</a></li>
    <li><a runat="server" href="~/WebForm1">Web Form 1</a></li>
    <li><a runat="server" href="~/About">About</a></li>
    <li><a runat="server" href="~/Contact">Contact</a></li>
  </ul>
</div>
<!-- ... -->
```

- Run the site.
- Create a new web form named `ActivePurchaseOrders` based on the `Site.Master` master page.
- Add a menu entry for the new page.

```html
<!-- ... -->
<div class="navbar-collapse collapse">
  <ul class="nav navbar-nav">
    <li><a runat="server" href="~/">Home</a></li>
    <li><a runat="server" href="~/Medications">Medications</a></li>
    <li><a runat="server" href="~/ActivePurchaseOrders">Active Purchase Orders</a></li>
    <li><a runat="server" href="~/WebForm1">Web Form 1</a></li>
    <li><a runat="server" href="~/About">About</a></li>
    <li><a runat="server" href="~/Contact">Contact</a></li>
  </ul>
</div>
<!-- ... -->
```

## ActivePurchaseOrders.aspx

- Add a title and header.

```html
<%@ Page Language="C#"
  Title="Active Purchase Orders"
  MasterPageFile="~/Site.Master"
  AutoEventWireup="true"
  CodeBehind="ActivePurchaseOrders.aspx.cs"
  Inherits="MovieTracker.ActivePurchaseOrders" %>
<asp:Content runat="server" ID="Content1"
  ContentPlaceHolderID="MainContent">
  <h2><%: Title %></h2>
</asp:Content>
<!-- ... -->
```

- Add a paragraph with a drop-down list from the **Toolbox / Standard** group after the heading, name it `departmentDropDownList`.

```html
<%@ Page Language="C#"
  Title="Active Purchase Orders"
  MasterPageFile="~/Site.Master"
  AutoEventWireup="true"
  CodeBehind="ActivePurchaseOrders.aspx.cs"
  Inherits="HospitalApp.ActivePurchaseOrders" %>
<asp:Content runat="server" ID="Content1"
  ContentPlaceHolderID="MainContent">
  <h2><%: Title %></h2>
  <p>Department: <asp:DropDownList ID="departmentDropDownList" runat="server"></asp:DropDownList></p>
</asp:Content>
<!-- ... -->
```

- Click the drop-down list task button and select `Choose Data Source...`.
- Create a new data source named `departmentSqlDataSource`.
- Use the existing `CHDBConnectionString` connection string.
- Select the `departments` table from the drop-down list.
- Choose the `department_id` and `department_name` columns.
- Set the data field `display` to `department_name` and the `value` to `department_id`.
- Select `Enable AutoPostBack`.
- Page should look approximately like this:

```html
<%@ Page Language="C#"
  Title="Active Purchase Orders"
  MasterPageFile="~/Site.Master"
  AutoEventWireup="true"
  CodeBehind="ActivePurchaseOrders.aspx.cs"
  Inherits="HospitalApp.ActivePurchaseOrders" %>
<asp:Content runat="server" ID="Content1"
  ContentPlaceHolderID="MainContent">
  <h2><%: Title %></h2>
  <p>Department:
    <asp:DropDownList runat="server"
      ID="departmentDropDownList"
      DataSourceID="departmentSqlDataSource"
      DataTextField="department_name"
      DataValueField="department_id"
      AutoPostBack="True">
    </asp:DropDownList>
    <asp:SqlDataSource runat="server"
      ID="departmentSqlDataSource"
      ConnectionString='<%$ ConnectionStrings:CHDBConnectionString %>'
      SelectCommand="SELECT [department_id], [department_name] FROM [departments]">
    </asp:SqlDataSource>
  </p>
</asp:Content>
```

- After the paragraph, add a `GridView` named `purchaseOrderGridView`.

```html
<%@ Page Language="C#"
  Title="Active Purchase Orders"
  MasterPageFile="~/Site.Master"
  AutoEventWireup="true"
  CodeBehind="ActivePurchaseOrders.aspx.cs"
  Inherits="HospitalApp.ActivePurchaseOrders" %>
<asp:Content runat="server" ID="Content1"
  ContentPlaceHolderID="MainContent">
  <h2><%: Title %></h2>
  <p>
    Department:
    <asp:DropDownList runat="server"
      ID="departmentDropDownList"
      DataSourceID="departmentSqlDataSource"
      DataTextField="department_name"
      DataValueField="department_id"
      AutoPostBack="True">
    </asp:DropDownList>
    <asp:SqlDataSource runat="server"
      ID="departmentSqlDataSource"
      ConnectionString='<%$ ConnectionStrings:CHDBConnectionString %>'
      SelectCommand="SELECT [department_id], [department_name] FROM [departments]">
    </asp:SqlDataSource>
  </p>
  <asp:GridView ID="purchaseOrderGridView" runat="server"></asp:GridView>
</asp:Content>
```

- Click the `GridView`'s task button and create a new `SqlDataSource` named `purchaseOrderSqlDataSource`.
- Use the existing `CHDBConnectionString` connection string.
- Select the `purchase_orders` table from the drop-down list.
- Select `all` of the columns except `department_id` and `order_status`.
- Click the `WHERE...` button.
- Set `Column` to `department_id`, `Source` to `Control` and `Control ID` to `departmentDropDownList`, click `Add`.
- Add another criteria; set `Column` to `order_status`, `Source` to `None` and `Value` to `ACTIVE`, click `Add`, click `OK`.
- Click `Next`, click `Finish`.
- The `GridView` and `SqlDataSource` will look approximately like this:

```html
<asp:GridView runat="server"
  ID="purchaseOrderGridView"
  AutoGenerateColumns="False"
  DataKeyNames="purchase_order_id"
  DataSourceID="purchaseOrderSqlDataSource">
  <Columns>
    <asp:BoundField> DataField="purchase_order_id" HeaderText="purchase_order_id" ReadOnly="True" SortExpression="purchase_order_id"></asp:BoundField>
    <asp:BoundField> DataField="order_date" HeaderText="order_date" SortExpression="order_date"></asp:BoundField>
    <asp:BoundField> DataField="vendor_id" HeaderText="vendor_id" SortExpression="vendor_id"></asp:BoundField>
    <asp:BoundField> DataField="total_amount" HeaderText="total_amount" SortExpression="total_amount"></asp:BoundField>
  </Columns>
</asp:GridView>
<asp:SqlDataSource runat="server"
  ID="purchaseOrderSqlDataSource"
  ConnectionString='<%$ ConnectionStrings:CHDBConnectionString %>'
  SelectCommand="SELECT [purchase_order_id], [order_date], [vendor_id], [total_amount] FROM [purchase_orders] WHERE (([department_id] = @department_id) AND ([order_status] = @order_status))">
  <SelectParameters>
    <asp:ControlParameter> ControlID="departmentDropDownList" PropertyName="SelectedValue" Name="department_id" Type="Int32"></asp:ControlParameter>
    <asp:Parameter> DefaultValue="ACTIVE" Name="order_status" Type="String"></asp:Parameter>
  </SelectParameters>
</asp:SqlDataSource>
```

- Run the site.
- Click the task button of `purchaseOrderSqlDataSource`.
- Select `Configure Data Source...`, click `Next`.
- Click the radio button `Specify a custom SQL statement or stored procedure`, and click `Next`.
- Click the `Query Builder...` button.
- In the top pane, right-click and select `Add Table...`.
- Select the `vendors` table and click `Add`, and click `Close`.
- In the `purchase_orders` table, `unselect vendor_id`.
- In the `vendors` table, select `vendor_name`, and click `OK`.
- Click `Next`, click `Next`, click `Finish`.
- Click `Yes` to `Refresh Fields and Keys` for `purchaseOrderGridView`.
- Run the site.
- Click the `GridView`'s task button and click `Edit Columns...`
  - Change `purchase_order_id` header to `ID`.
  - Change `order_date` header to `Date`.
  - Change `total_amount` header to `Total Amount`.
  - Change `vendor_name` header to `Vendor`.
  - Change `ID` and `Total Amount` **Styles / ItemStyle /** `HorizontalAlign` to `Right`.
  - Change `Date` Data / `DataFormatString` to `{0:d}`.
  - Change `Total Amount` Data / `DataFormatString` to `{0:c}`.
- Click `OK`.
- Set the `CssClass` of the `GridView` to `table-condensed table-striped bg-info`.

```html
<asp:GridView runat="server"
  ID="purchaseOrderGridView"
  AutoGenerateColumns="False"
  DataKeyNames="purchase_order_id"
  DataSourceID="purchaseOrderSqlDataSource"
  CssClass="table-condensed table-striped bg-info">
  <Columns>
    <asp:BoundField DataField="purchase_order_id" HeaderText="ID" ReadOnly="True" SortExpression="purchase_order_id">
      <ItemStyle HorizontalAlign="Right"></ItemStyle>
    </asp:BoundField>
    <asp:BoundField DataField="order_date" HeaderText="Date" SortExpression="order_date" DataFormatString="{0:d}"></asp:BoundField>
    <asp:BoundField DataField="total_amount" HeaderText="Total Amount" SortExpression="total_amount" DataFormatString="{0:c}">
      <ItemStyle HorizontalAlign="Right"></ItemStyle>
    </asp:BoundField>
    <asp:BoundField DataField="vendor_name" HeaderText="Vendor" SortExpression="vendor_name"></asp:BoundField>
  </Columns>
</asp:GridView>
<asp:SqlDataSource runat="server"
  ID="purchaseOrderSqlDataSource"
  ConnectionString='<%$ ConnectionStrings:CHDBConnectionString %>'
  SelectCommand="SELECT purchase_orders.purchase_order_id, purchase_orders.order_date, purchase_orders.total_amount, vendors.vendor_name FROM purchase_orders INNER JOIN vendors ON purchase_orders.vendor_id = vendors.vendor_id WHERE (purchase_orders.department_id = @department_id) AND (purchase_orders.order_status = @order_status)">
  <SelectParameters>
    <asp:ControlParameter ControlID="departmentDropDownList" PropertyName="SelectedValue" Name="department_id" Type="Int32"></asp:ControlParameter>
    <asp:Parameter DefaultValue="ACTIVE" Name="order_status" Type="String"></asp:Parameter>
  </SelectParameters>
</asp:SqlDataSource>
```

- Run the site.
