using assignment2.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace assignment2.Data
{
    public partial class NorthwindContext : DbContext
    {
        public NorthwindContext() {}
        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options) {}
        public virtual DbSet<AlphabeticalListOfProduct> AlphabeticalListOfProducts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategorySalesFor2019> CategorySalesFor2019s { get; set; }
        public virtual DbSet<CurrentProductList> CurrentProductLists { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerAndSuppliersByCity> CustomerAndSuppliersByCities { get; set; }
        public virtual DbSet<CustomerCustomerDemo> CustomerCustomerDemos { get; set; }
        public virtual DbSet<CustomerDemographic> CustomerDemographics { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeTerritory> EmployeeTerritories { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderDetailsExtended> OrderDetailsExtendeds { get; set; }
        public virtual DbSet<OrderSubTotal> OrderSubTotals { get; set; }
        public virtual DbSet<OrdersQry> OrdersQries { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductSalesFor2019> ProductSalesFor2019s { get; set; }
        public virtual DbSet<ProductsAboveAveragePrice> ProductsAboveAveragePrices { get; set; }
        public virtual DbSet<ProductsByCategory> ProductsByCategories { get; set; }
        public virtual DbSet<QuarterlyOrder> QuarterlyOrders { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<SalesByCategory> SalesByCategories { get; set; }
        public virtual DbSet<SalesTotalsByAmount> SalesTotalsByAmounts { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<SummaryOfSalesByQuarter> SummaryOfSalesByQuarters { get; set; }
        public virtual DbSet<SummaryOfSalesByYear> SummaryOfSalesByYears { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Territory> Territories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=NorthwindDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AlphabeticalListOfProduct>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("alphabetical_list_of_products");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("category_name");

                entity.Property(e => e.Discontinued).HasColumnName("discontinued");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("product_name");

                entity.Property(e => e.QuantityPerUnit)
                    .HasMaxLength(20)
                    .HasColumnName("quantity_per_unit");

                entity.Property(e => e.ReorderLevel).HasColumnName("reorder_level");

                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasColumnName("unit_price");

                entity.Property(e => e.UnitsInStock).HasColumnName("units_in_stock");

                entity.Property(e => e.UnitsOnOrder).HasColumnName("units_on_order");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");

                entity.HasIndex(e => e.CategoryName, "category_name");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("category_name");

                entity.Property(e => e.Description)
                    .HasColumnType("ntext")
                    .HasColumnName("description");
            });

            modelBuilder.Entity<CategorySalesFor2019>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("category_sales_for_2019");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("category_name");

                entity.Property(e => e.CategorySales)
                    .HasColumnType("money")
                    .HasColumnName("category_sales");
            });

            modelBuilder.Entity<CurrentProductList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("current_product_List");

                entity.Property(e => e.ProductId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("product_id");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("product_name");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.HasIndex(e => e.City, "city");

                entity.HasIndex(e => e.CompanyName, "company_name");

                entity.HasIndex(e => e.PostalCode, "postal_code");

                entity.HasIndex(e => e.Region, "region");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(5)
                    .HasColumnName("customer_id")
                    .IsFixedLength(true);

                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .HasColumnName("address");

                entity.Property(e => e.City)
                    .HasMaxLength(15)
                    .HasColumnName("city");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("company_name");

                entity.Property(e => e.ContactName)
                    .HasMaxLength(30)
                    .HasColumnName("contact_name");

                entity.Property(e => e.ContactTitle)
                    .HasMaxLength(30)
                    .HasColumnName("contact_title");

                entity.Property(e => e.Country)
                    .HasMaxLength(15)
                    .HasColumnName("country");

                entity.Property(e => e.Fax)
                    .HasMaxLength(24)
                    .HasColumnName("fax");

                entity.Property(e => e.Phone)
                    .HasMaxLength(24)
                    .HasColumnName("phone");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(10)
                    .HasColumnName("postal_code");

                entity.Property(e => e.Region)
                    .HasMaxLength(15)
                    .HasColumnName("region");
            });

            modelBuilder.Entity<CustomerAndSuppliersByCity>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("customer_and_suppliers_by_city");

                entity.Property(e => e.City)
                    .HasMaxLength(15)
                    .HasColumnName("city");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("company_name");

                entity.Property(e => e.ContactName)
                    .HasMaxLength(30)
                    .HasColumnName("contact_name");

                entity.Property(e => e.Relationship)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("relationship");
            });

            modelBuilder.Entity<CustomerCustomerDemo>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.CustomerTypeId })
                    .IsClustered(false);

                entity.ToTable("customer_customer_demo");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(5)
                    .HasColumnName("customer_id")
                    .IsFixedLength(true);

                entity.Property(e => e.CustomerTypeId)
                    .HasMaxLength(10)
                    .HasColumnName("customer_type_id")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerCustomerDemos)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_customer_customer_demo_customers");

                entity.HasOne(d => d.CustomerType)
                    .WithMany(p => p.CustomerCustomerDemos)
                    .HasForeignKey(d => d.CustomerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_customer_customer_demo");
            });

            modelBuilder.Entity<CustomerDemographic>(entity =>
            {
                entity.HasKey(e => e.CustomerTypeId)
                    .IsClustered(false);

                entity.ToTable("customer_demographics");

                entity.Property(e => e.CustomerTypeId)
                    .HasMaxLength(10)
                    .HasColumnName("customer_type_id")
                    .IsFixedLength(true);

                entity.Property(e => e.CustomerDesc)
                    .HasColumnType("ntext")
                    .HasColumnName("customer_desc");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employees");

                entity.HasIndex(e => e.LastName, "last_name");

                entity.HasIndex(e => e.PostalCode, "postal_code");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .HasColumnName("address");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("datetime")
                    .HasColumnName("birth_date");

                entity.Property(e => e.City)
                    .HasMaxLength(15)
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .HasMaxLength(15)
                    .HasColumnName("country");

                entity.Property(e => e.Extension)
                    .HasMaxLength(4)
                    .HasColumnName("extension");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("first_name");

                entity.Property(e => e.HireDate)
                    .HasColumnType("datetime")
                    .HasColumnName("hire_date");

                entity.Property(e => e.HomePhone)
                    .HasMaxLength(24)
                    .HasColumnName("home_phone");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("last_name");

                entity.Property(e => e.Notes)
                    .HasColumnType("ntext")
                    .HasColumnName("notes");

                entity.Property(e => e.PhotoPath)
                    .HasMaxLength(255)
                    .HasColumnName("photo_path");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(10)
                    .HasColumnName("postal_code");

                entity.Property(e => e.Region)
                    .HasMaxLength(15)
                    .HasColumnName("region");

                entity.Property(e => e.ReportsTo).HasColumnName("reports_to");

                entity.Property(e => e.Title)
                    .HasMaxLength(30)
                    .HasColumnName("title");

                entity.Property(e => e.TitleOfCourtesy)
                    .HasMaxLength(25)
                    .HasColumnName("title_of_courtesy");

                entity.HasOne(d => d.ReportsToNavigation)
                    .WithMany(p => p.InverseReportsToNavigation)
                    .HasForeignKey(d => d.ReportsTo)
                    .HasConstraintName("FK_employees_employees");
            });

            modelBuilder.Entity<EmployeeTerritory>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.TerritoryId })
                    .IsClustered(false);

                entity.ToTable("employee_territories");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.TerritoryId)
                    .HasMaxLength(20)
                    .HasColumnName("territory_id");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeTerritories)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_employee_territories_employees");

                entity.HasOne(d => d.Territory)
                    .WithMany(p => p.EmployeeTerritories)
                    .HasForeignKey(d => d.TerritoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_employee_territories_territories");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("invoices");

                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .HasColumnName("address");

                entity.Property(e => e.City)
                    .HasMaxLength(15)
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .HasMaxLength(15)
                    .HasColumnName("country");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(5)
                    .HasColumnName("customer_id")
                    .IsFixedLength(true);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("customer_name");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.ExtendedPrice)
                    .HasColumnType("money")
                    .HasColumnName("extended_price");

                entity.Property(e => e.Freight)
                    .HasColumnType("money")
                    .HasColumnName("freight");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_date");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(10)
                    .HasColumnName("postal_code");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("product_name");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Region)
                    .HasMaxLength(15)
                    .HasColumnName("region");

                entity.Property(e => e.RequiredDate)
                    .HasColumnType("datetime")
                    .HasColumnName("required_date");

                entity.Property(e => e.SalesPerson)
                    .IsRequired()
                    .HasMaxLength(31)
                    .HasColumnName("sales_person");

                entity.Property(e => e.ShipAddress)
                    .HasMaxLength(60)
                    .HasColumnName("ship_address");

                entity.Property(e => e.ShipCity)
                    .HasMaxLength(15)
                    .HasColumnName("ship_city");

                entity.Property(e => e.ShipCountry)
                    .HasMaxLength(15)
                    .HasColumnName("ship_country");

                entity.Property(e => e.ShipName)
                    .HasMaxLength(40)
                    .HasColumnName("ship_name");

                entity.Property(e => e.ShipPostalCode)
                    .HasMaxLength(10)
                    .HasColumnName("ship_postal_code");

                entity.Property(e => e.ShipRegion)
                    .HasMaxLength(15)
                    .HasColumnName("ship_region");

                entity.Property(e => e.ShippedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("shipped_date");

                entity.Property(e => e.ShipperName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("shipper_name");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasColumnName("unit_price");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.HasIndex(e => e.CustomerId, "customer_id");

                entity.HasIndex(e => e.CustomerId, "customers_orders");

                entity.HasIndex(e => e.EmployeeId, "employee_id");

                entity.HasIndex(e => e.EmployeeId, "employees_orders");

                entity.HasIndex(e => e.OrderDate, "order_date");

                entity.HasIndex(e => e.ShipPostalCode, "ship_postal_code");

                entity.HasIndex(e => e.ShippedDate, "shipped_date");

                entity.HasIndex(e => e.ShipVia, "shippers_orders");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(5)
                    .HasColumnName("customer_id")
                    .IsFixedLength(true);

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.Freight)
                    .HasColumnType("money")
                    .HasColumnName("freight")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_date");

                entity.Property(e => e.RequiredDate)
                    .HasColumnType("datetime")
                    .HasColumnName("required_date");

                entity.Property(e => e.ShipAddress)
                    .HasMaxLength(60)
                    .HasColumnName("ship_address");

                entity.Property(e => e.ShipCity)
                    .HasMaxLength(15)
                    .HasColumnName("ship_city");

                entity.Property(e => e.ShipCountry)
                    .HasMaxLength(15)
                    .HasColumnName("ship_country");

                entity.Property(e => e.ShipName)
                    .HasMaxLength(40)
                    .HasColumnName("ship_name");

                entity.Property(e => e.ShipPostalCode)
                    .HasMaxLength(10)
                    .HasColumnName("ship_postal_code");

                entity.Property(e => e.ShipRegion)
                    .HasMaxLength(15)
                    .HasColumnName("ship_region");

                entity.Property(e => e.ShipVia).HasColumnName("ship_via");

                entity.Property(e => e.ShippedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("shipped_date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_orders_customers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_orders_employees");

                entity.HasOne(d => d.ShipViaNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipVia)
                    .HasConstraintName("FK_orders_shippers");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId });

                entity.ToTable("order_details");

                entity.HasIndex(e => e.OrderId, "order_id");

                entity.HasIndex(e => e.OrderId, "orders_order_details");

                entity.HasIndex(e => e.ProductId, "product_id");

                entity.HasIndex(e => e.ProductId, "products_order_details");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasColumnName("unit_price");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_order_details_orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_order_details_products");
            });

            modelBuilder.Entity<OrderDetailsExtended>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("order_details_extended");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.ExtendedPrice)
                    .HasColumnType("money")
                    .HasColumnName("extended_price");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("product_name");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasColumnName("unit_price");
            });

            modelBuilder.Entity<OrderSubTotal>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("order_sub_totals");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.SubTotal)
                    .HasColumnType("money")
                    .HasColumnName("sub_total");
            });

            modelBuilder.Entity<OrdersQry>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("orders_qry");

                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .HasColumnName("address");

                entity.Property(e => e.City)
                    .HasMaxLength(15)
                    .HasColumnName("city");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("company_name");

                entity.Property(e => e.Country)
                    .HasMaxLength(15)
                    .HasColumnName("country");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(5)
                    .HasColumnName("customer_id")
                    .IsFixedLength(true);

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.Freight)
                    .HasColumnType("money")
                    .HasColumnName("freight");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_date");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(10)
                    .HasColumnName("postal_code");

                entity.Property(e => e.Region)
                    .HasMaxLength(15)
                    .HasColumnName("region");

                entity.Property(e => e.RequiredDate)
                    .HasColumnType("datetime")
                    .HasColumnName("required_date");

                entity.Property(e => e.ShipAddress)
                    .HasMaxLength(60)
                    .HasColumnName("ship_address");

                entity.Property(e => e.ShipCity)
                    .HasMaxLength(15)
                    .HasColumnName("ship_city");

                entity.Property(e => e.ShipCountry)
                    .HasMaxLength(15)
                    .HasColumnName("ship_country");

                entity.Property(e => e.ShipName)
                    .HasMaxLength(40)
                    .HasColumnName("ship_name");

                entity.Property(e => e.ShipPostalCode)
                    .HasMaxLength(10)
                    .HasColumnName("ship_postal_code");

                entity.Property(e => e.ShipRegion)
                    .HasMaxLength(15)
                    .HasColumnName("ship_region");

                entity.Property(e => e.ShipVia).HasColumnName("ship_via");

                entity.Property(e => e.ShippedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("shipped_date");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.HasIndex(e => e.CategoryId, "categories_products");

                entity.HasIndex(e => e.CategoryId, "category_id");

                entity.HasIndex(e => e.ProductName, "product_name");

                entity.HasIndex(e => e.SupplierId, "supplier_id");

                entity.HasIndex(e => e.SupplierId, "suppliers_products");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Discontinued).HasColumnName("discontinued");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("product_name");

                entity.Property(e => e.QuantityPerUnit)
                    .HasMaxLength(20)
                    .HasColumnName("quantity_per_unit");

                entity.Property(e => e.ReorderLevel)
                    .HasColumnName("reorder_level")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasColumnName("unit_price")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitsInStock)
                    .HasColumnName("units_in_stock")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitsOnOrder)
                    .HasColumnName("units_on_order")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_products_categories");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_products_suppliers");
            });

            modelBuilder.Entity<ProductSalesFor2019>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("product_sales_for_2019");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("category_name");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("product_name");

                entity.Property(e => e.ProductSales)
                    .HasColumnType("money")
                    .HasColumnName("product_sales");
            });

            modelBuilder.Entity<ProductsAboveAveragePrice>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("products_above_average_price");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("product_name");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasColumnName("unit_price");
            });

            modelBuilder.Entity<ProductsByCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("products_by_category");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("category_name");

                entity.Property(e => e.Discontinued).HasColumnName("discontinued");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("product_name");

                entity.Property(e => e.QuantityPerUnit)
                    .HasMaxLength(20)
                    .HasColumnName("quantity_per_unit");

                entity.Property(e => e.UnitsInStock).HasColumnName("units_in_stock");
            });

            modelBuilder.Entity<QuarterlyOrder>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("quarterly_orders");

                entity.Property(e => e.City)
                    .HasMaxLength(15)
                    .HasColumnName("city");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(40)
                    .HasColumnName("company_name");

                entity.Property(e => e.Country)
                    .HasMaxLength(15)
                    .HasColumnName("country");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(5)
                    .HasColumnName("customer_id")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasKey(e => e.RegionId)
                    .IsClustered(false);

                entity.ToTable("region");

                entity.Property(e => e.RegionId)
                    .ValueGeneratedNever()
                    .HasColumnName("region_id");

                entity.Property(e => e.RegionDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("region_description")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<SalesByCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("sales_by_category");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("category_name");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("product_name");

                entity.Property(e => e.ProductSales)
                    .HasColumnType("money")
                    .HasColumnName("product_sales");
            });

            modelBuilder.Entity<SalesTotalsByAmount>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("sales_totals_by_amount");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("company_name");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.SaleAmount)
                    .HasColumnType("money")
                    .HasColumnName("sale_amount");

                entity.Property(e => e.ShippedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("shipped_date");
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.ToTable("shippers");

                entity.Property(e => e.ShipperId).HasColumnName("shipper_id");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("company_name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(24)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<SummaryOfSalesByQuarter>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("summary_of_sales_by_quarter");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ShippedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("shipped_date");

                entity.Property(e => e.SubTotal)
                    .HasColumnType("money")
                    .HasColumnName("sub_total");
            });

            modelBuilder.Entity<SummaryOfSalesByYear>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("summary_of_sales_by_year");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ShippedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("shipped_date");

                entity.Property(e => e.SubTotal)
                    .HasColumnType("money")
                    .HasColumnName("sub_total");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("suppliers");

                entity.HasIndex(e => e.CompanyName, "company_name");

                entity.HasIndex(e => e.PostalCode, "postal_code");

                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .HasColumnName("address");

                entity.Property(e => e.City)
                    .HasMaxLength(15)
                    .HasColumnName("city");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("company_name");

                entity.Property(e => e.ContactName)
                    .HasMaxLength(30)
                    .HasColumnName("contact_name");

                entity.Property(e => e.ContactTitle)
                    .HasMaxLength(30)
                    .HasColumnName("contact_title");

                entity.Property(e => e.Country)
                    .HasMaxLength(15)
                    .HasColumnName("country");

                entity.Property(e => e.Fax)
                    .HasMaxLength(24)
                    .HasColumnName("fax");

                entity.Property(e => e.HomePage)
                    .HasColumnType("ntext")
                    .HasColumnName("home_page");

                entity.Property(e => e.Phone)
                    .HasMaxLength(24)
                    .HasColumnName("phone");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(10)
                    .HasColumnName("postal_code");

                entity.Property(e => e.Region)
                    .HasMaxLength(15)
                    .HasColumnName("region");
            });

            modelBuilder.Entity<Territory>(entity =>
            {
                entity.HasKey(e => e.TerritoryId)
                    .IsClustered(false);

                entity.ToTable("territories");

                entity.Property(e => e.TerritoryId)
                    .HasMaxLength(20)
                    .HasColumnName("territory_id");

                entity.Property(e => e.RegionId).HasColumnName("region_id");

                entity.Property(e => e.TerritoryDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("territory_description")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Territories)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_territories_region");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
