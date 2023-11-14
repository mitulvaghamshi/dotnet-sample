using MedicationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicationAPI.Data;

public partial class CHDBContext : DbContext
{
    public CHDBContext() { }

    public CHDBContext(DbContextOptions<CHDBContext> options) : base(options) { }

    public virtual DbSet<Admission> Admissions { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Encounter> Encounters { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Medication> Medications { get; set; }

    public virtual DbSet<NursingUnit> NursingUnits { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Physician> Physicians { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<PurchaseOrderLine> PurchaseOrderLines { get; set; }

    public virtual DbSet<UnitDoseOrder> UnitDoseOrders { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer("name=CHDB");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admission>(entity =>
        {
            entity.HasKey(e => new { e.PatientId, e.AdmissionDate }).HasName("pk_admissions");

            entity.HasOne(d => d.AttendingPhysician).WithMany(p => p.Admissions).HasConstraintName("FK__admission__atten__4BAC3F29");

            entity.HasOne(d => d.NursingUnit).WithMany(p => p.Admissions).HasConstraintName("FK__admission__nursi__4CA06362");

            entity.HasOne(d => d.Patient).WithMany(p => p.Admissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__admission__patie__4AB81AF0");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__departme__C22324227D798A2B");

            entity.Property(e => e.DepartmentId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Encounter>(entity =>
        {
            entity.HasKey(e => new { e.PatientId, e.PhysicianId, e.EncounterDateTime }).HasName("pk_encounters");

            entity.HasOne(d => d.Patient).WithMany(p => p.Encounters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__encounter__patie__4F7CD00D");

            entity.HasOne(d => d.Physician).WithMany(p => p.Encounters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__encounter__physi__5070F446");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__items__52020FDD53506530");

            entity.Property(e => e.ItemId).ValueGeneratedNever();

            entity.HasOne(d => d.PrimaryVendor).WithMany(p => p.Items).HasConstraintName("FK__items__primary_v__534D60F1");
        });

        modelBuilder.Entity<Medication>(entity =>
        {
            entity.HasKey(e => e.MedicationId).HasName("PK__medicati__DD94789B7EC1E4E6");

            entity.Property(e => e.MedicationId).ValueGeneratedNever();
        });

        modelBuilder.Entity<NursingUnit>(entity =>
        {
            entity.HasKey(e => e.NursingUnitId).HasName("PK__nursing___0E85E2CCD4E16B5F");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__patients__4D5CE4767E8ACF95");

            entity.Property(e => e.PatientId).ValueGeneratedNever();
            entity.Property(e => e.Gender).IsFixedLength();
            entity.Property(e => e.ProvinceId).IsFixedLength();

            entity.HasOne(d => d.Province).WithMany(p => p.Patients).HasConstraintName("FK__patients__provin__4316F928");
        });

        modelBuilder.Entity<Physician>(entity =>
        {
            entity.HasKey(e => e.PhysicianId).HasName("PK__physicia__8C035A3CC9391B50");

            entity.Property(e => e.PhysicianId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.ProvinceId).HasName("PK__province__08DCB60F8423CD41");

            entity.Property(e => e.ProvinceId).IsFixedLength();
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.PurchaseOrderId).HasName("PK__purchase__AFCA88E63ED5A32C");

            entity.Property(e => e.PurchaseOrderId).ValueGeneratedNever();

            entity.HasOne(d => d.Department).WithMany(p => p.PurchaseOrders).HasConstraintName("FK__purchase___depar__5629CD9C");

            entity.HasOne(d => d.Vendor).WithMany(p => p.PurchaseOrders).HasConstraintName("FK__purchase___vendo__571DF1D5");
        });

        modelBuilder.Entity<PurchaseOrderLine>(entity =>
        {
            entity.HasKey(e => new { e.PurchaseOrderId, e.LineNum }).HasName("pk_purchase_order_lines");

            entity.HasOne(d => d.Item).WithMany(p => p.PurchaseOrderLines).HasConstraintName("FK__purchase___item___5EBF139D");

            entity.HasOne(d => d.PurchaseOrder).WithMany(p => p.PurchaseOrderLines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__purchase___purch__5DCAEF64");
        });

        modelBuilder.Entity<UnitDoseOrder>(entity =>
        {
            entity.HasKey(e => e.UnitDoseOrderId).HasName("PK__unit_dos__BB64A31A092F0A64");

            entity.Property(e => e.UnitDoseOrderId).ValueGeneratedNever();
            entity.Property(e => e.PharmacistInitials).IsFixedLength();

            entity.HasOne(d => d.Medication).WithMany(p => p.UnitDoseOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__unit_dose__medic__5AEE82B9");

            entity.HasOne(d => d.Patient).WithMany(p => p.UnitDoseOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__unit_dose__patie__59FA5E80");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.VendorId).HasName("PK__vendors__0F7D2B7847944651");

            entity.Property(e => e.VendorId).ValueGeneratedNever();
            entity.Property(e => e.ProvinceId).IsFixedLength();

            entity.HasOne(d => d.Province).WithMany(p => p.Vendors).HasConstraintName("FK__vendors__provinc__47DBAE45");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
