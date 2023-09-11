using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MKDIR.Infrastructure.DataTemp;

public partial class AppDBContext : DbContext
{
    public AppDBContext()
    {
    }

    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BillingStatus> BillingStatuses { get; set; }

    public virtual DbSet<BusinessAction> BusinessActions { get; set; }

    public virtual DbSet<BusinessModule> BusinessModules { get; set; }

    public virtual DbSet<BusinessTransaction> BusinessTransactions { get; set; }

    public virtual DbSet<BusinessUser> BusinessUsers { get; set; }

    public virtual DbSet<BusinessUserPass> BusinessUserPasses { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyProfile> CompanyProfiles { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<DeliveryStatus> DeliveryStatuses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<DocumentStatus> DocumentStatuses { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<GenderType> GenderTypes { get; set; }

    public virtual DbSet<IdentificationType> IdentificationTypes { get; set; }

    public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; }

    public virtual DbSet<Operator> Operators { get; set; }

    public virtual DbSet<OperatorProfile> OperatorProfiles { get; set; }

    public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; }

    public virtual DbSet<PersonType> PersonTypes { get; set; }

    public virtual DbSet<RecoverPassword> RecoverPasswords { get; set; }

    public virtual DbSet<SequenceControl> SequenceControls { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<SuplierCategory> SuplierCategories { get; set; }

    public virtual DbSet<TransactionAction> TransactionActions { get; set; }

    public virtual DbSet<UserCompanyProfile> UserCompanyProfiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQL2019; Database=MKDIR; User Id=sa; Password=Barcelona; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BillingStatus>(entity =>
        {
            entity.ToTable("BillingStatus", "MasterData");

            entity.HasIndex(e => e.DocumentTypeId, "fkIdx_DocumentTypeID");

            entity.Property(e => e.BillingStatusId).HasColumnName("BillingStatusID");
            entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.DocumentType).WithMany(p => p.BillingStatuses)
                .HasForeignKey(d => d.DocumentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BillingStatus_DocumentType");
        });

        modelBuilder.Entity<BusinessAction>(entity =>
        {
            entity.ToTable("BusinessAction", "AccessControl");

            entity.Property(e => e.BusinessActionId).HasColumnName("BusinessActionID");
            entity.Property(e => e.Icon)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BusinessModule>(entity =>
        {
            entity.HasKey(e => e.BusinessModuleId).HasName("PK_Module");

            entity.ToTable("BusinessModule", "AccessControl");

            entity.Property(e => e.BusinessModuleId).HasColumnName("BusinessModuleID");
            entity.Property(e => e.Icon)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BusinessTransaction>(entity =>
        {
            entity.HasKey(e => e.BusinessTransactionId).HasName("PK_Transaction");

            entity.ToTable("BusinessTransaction", "AccessControl");

            entity.HasIndex(e => e.BusinessModuleId, "fkIdx_BusinessModuleID");

            entity.Property(e => e.BusinessTransactionId).HasColumnName("BusinessTransactionID");
            entity.Property(e => e.BusinessModuleId).HasColumnName("BusinessModuleID");
            entity.Property(e => e.Icon)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Urlpath)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("URLPath");

            entity.HasOne(d => d.BusinessModule).WithMany(p => p.BusinessTransactions)
                .HasForeignKey(d => d.BusinessModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BusinessTransaction_BusinessModule");
        });

        modelBuilder.Entity<BusinessUser>(entity =>
        {
            entity.HasKey(e => e.BusinessUserId).HasName("PK_User");

            entity.ToTable("BusinessUserDTO", "AccessControl");

            entity.Property(e => e.BusinessUserId).HasColumnName("BusinessUserID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SureName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.OperatorProfiles).WithMany(p => p.BusinessUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "UserOperatorProfile",
                    r => r.HasOne<OperatorProfile>().WithMany()
                        .HasForeignKey("OperatorProfileId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserOperatorProfile_OperatorProfile"),
                    l => l.HasOne<BusinessUser>().WithMany()
                        .HasForeignKey("BusinessUserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserOperatorProfile_BusinessUser"),
                    j =>
                    {
                        j.HasKey("BusinessUserId", "OperatorProfileId");
                        j.ToTable("UserOperatorProfile", "AccessControl");
                        j.HasIndex(new[] { "BusinessUserId" }, "fkIdx_BusinessUserID");
                        j.HasIndex(new[] { "OperatorProfileId" }, "fkIdx_OperatorProfileID");
                        j.IndexerProperty<int>("BusinessUserId").HasColumnName("BusinessUserID");
                        j.IndexerProperty<int>("OperatorProfileId").HasColumnName("OperatorProfileID");
                    });
        });

        modelBuilder.Entity<BusinessUserPass>(entity =>
        {
            entity.HasKey(e => e.BusinessUserId);

            entity.ToTable("BusinessUserPass", "AccessControl");

            entity.HasIndex(e => e.BusinessUserId, "fkIdx_BusinessUserID");

            entity.Property(e => e.BusinessUserId)
                .ValueGeneratedNever()
                .HasColumnName("BusinessUserID");
            entity.Property(e => e.Pass)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.BusinessUser).WithOne(p => p.BusinessUserPass)
                .HasForeignKey<BusinessUserPass>(d => d.BusinessUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BusinessUserPass_BusinessUser");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City", "MasterData");

            entity.HasIndex(e => e.DepartmentId, "fkIdx_DepartmentID");

            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Department).WithMany(p => p.Cities)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_City_Department");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK_CompanyID");

            entity.ToTable("Company", "MasterData");

            entity.HasIndex(e => e.IdentificationTypeId, "fkIdx_IdentificationTypeID");

            entity.HasIndex(e => e.OperatorId, "fkIdx_OperatorID");

            entity.HasIndex(e => e.PersonTypeId, "fkIdx_PersonTypeID");

            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FiscalId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FiscalID");
            entity.Property(e => e.IdentificationTypeId).HasColumnName("IdentificationTypeID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OperatorId).HasColumnName("OperatorID");
            entity.Property(e => e.PersonTypeId).HasColumnName("PersonTypeID");
            entity.Property(e => e.Phone1)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone2)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SecoundFiscalId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SecoundFiscalID");

            entity.HasOne(d => d.IdentificationType).WithMany(p => p.Companies)
                .HasForeignKey(d => d.IdentificationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_IdentificationType");

            entity.HasOne(d => d.Operator).WithMany(p => p.Companies)
                .HasForeignKey(d => d.OperatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_Operator");

            entity.HasOne(d => d.PersonType).WithMany(p => p.Companies)
                .HasForeignKey(d => d.PersonTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_PersonType");
        });

        modelBuilder.Entity<CompanyProfile>(entity =>
        {
            entity.HasKey(e => e.CompanyProfileId).HasName("PK_CompanyProfileID");

            entity.ToTable("CompanyProfile", "AccessControl");

            entity.HasIndex(e => e.CompanyId, "fkIdx_CompanyID");

            entity.Property(e => e.CompanyProfileId).HasColumnName("CompanyProfileID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Company).WithMany(p => p.CompanyProfiles)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyProfile_Company");

            entity.HasMany(d => d.Businesses).WithMany(p => p.CompanyProfiles)
                .UsingEntity<Dictionary<string, object>>(
                    "CompanyProfileTransactionAction",
                    r => r.HasOne<TransactionAction>().WithMany()
                        .HasForeignKey("BusinessTransactionId", "BusinessActionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CompanyProfileTransactionAction_TransactionAction"),
                    l => l.HasOne<CompanyProfile>().WithMany()
                        .HasForeignKey("CompanyProfileId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CompanyProfileTransactionAction_CompanyProfile"),
                    j =>
                    {
                        j.HasKey("CompanyProfileId", "BusinessTransactionId", "BusinessActionId");
                        j.ToTable("CompanyProfileTransactionAction", "AccessControl");
                        j.HasIndex(new[] { "BusinessTransactionId", "BusinessActionId" }, "fkIdx_BusinessTransactionID_BusinessActionID");
                        j.HasIndex(new[] { "CompanyProfileId" }, "fkIdx_CompanyProfileID");
                        j.IndexerProperty<int>("CompanyProfileId").HasColumnName("CompanyProfileID");
                        j.IndexerProperty<int>("BusinessTransactionId").HasColumnName("BusinessTransactionID");
                        j.IndexerProperty<int>("BusinessActionId").HasColumnName("BusinessActionID");
                    });
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country", "MasterData");

            entity.HasIndex(e => e.CurrencyId, "fkIdx_CurrencyID");

            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Currency).WithMany(p => p.Countries)
                .HasForeignKey(d => d.CurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Country_Currency");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.ToTable("Currency", "MasterData");

            entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DeliveryStatus>(entity =>
        {
            entity.ToTable("DeliveryStatus", "MasterData");

            entity.HasIndex(e => e.DocumentTypeId, "fkIdx_DocumentTypeID");

            entity.Property(e => e.DeliveryStatusId).HasColumnName("DeliveryStatusID");
            entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.DocumentType).WithMany(p => p.DeliveryStatuses)
                .HasForeignKey(d => d.DocumentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeliveryStatus_DocumentType");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department", "MasterData");

            entity.HasIndex(e => e.CountryId, "fkIdx_CountryID");

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Country).WithMany(p => p.Departments)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Department_Country");
        });

        modelBuilder.Entity<DocumentStatus>(entity =>
        {
            entity.ToTable("DocumentStatus", "MasterData");

            entity.HasIndex(e => e.DocumentTypeId, "fkIdx_DocumentTypeID");

            entity.Property(e => e.DocumentStatusId).HasColumnName("DocumentStatusID");
            entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.DocumentType).WithMany(p => p.DocumentStatuses)
                .HasForeignKey(d => d.DocumentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocumentStatus_DocumentType");
        });

        modelBuilder.Entity<DocumentType>(entity =>
        {
            entity.ToTable("DocumentType", "MasterData");

            entity.HasIndex(e => e.OperatorId, "fkIdx_OperatorID");

            entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OperatorId).HasColumnName("OperatorID");

            entity.HasOne(d => d.Operator).WithMany(p => p.DocumentTypes)
                .HasForeignKey(d => d.OperatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocumentType_OPerator");
        });

        modelBuilder.Entity<GenderType>(entity =>
        {
            entity.ToTable("GenderType", "MasterData");

            entity.Property(e => e.GenderTypeId).HasColumnName("GenderTypeID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<IdentificationType>(entity =>
        {
            entity.ToTable("IdentificationType", "MasterData");

            entity.HasIndex(e => e.OperatorId, "fkIdx_OperatorID");

            entity.Property(e => e.IdentificationTypeId).HasColumnName("IdentificationTypeID");
            entity.Property(e => e.Code)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OperatorId).HasColumnName("OperatorID");

            entity.HasOne(d => d.Operator).WithMany(p => p.IdentificationTypes)
                .HasForeignKey(d => d.OperatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdentificationType_Operator");
        });

        modelBuilder.Entity<MaritalStatus>(entity =>
        {
            entity.ToTable("MaritalStatus", "MasterData");

            entity.Property(e => e.MaritalStatusId).HasColumnName("MaritalStatusID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Operator>(entity =>
        {
            entity.HasKey(e => e.OperatorId).HasName("PK_OperatorID");

            entity.ToTable("Operator", "MasterData");

            entity.HasIndex(e => e.CountryId, "fkIdx_CountryID");

            entity.Property(e => e.OperatorId).HasColumnName("OperatorID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Country).WithMany(p => p.Operators)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Operator_Country");
        });

        modelBuilder.Entity<OperatorProfile>(entity =>
        {
            entity.HasKey(e => e.OperatorProfileId).HasName("PK_OperatorProfileID");

            entity.ToTable("OperatorProfile", "AccessControl");

            entity.HasIndex(e => e.OperatorId, "fkIdx_OperatorID");

            entity.Property(e => e.OperatorProfileId).HasColumnName("OperatorProfileID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OperatorId).HasColumnName("OperatorID");

            entity.HasOne(d => d.Operator).WithMany(p => p.OperatorProfiles)
                .HasForeignKey(d => d.OperatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OperatorProfile_Operator");

            entity.HasMany(d => d.Businesses).WithMany(p => p.OperatorProfiles)
                .UsingEntity<Dictionary<string, object>>(
                    "OperatorProfileTransactionAction",
                    r => r.HasOne<TransactionAction>().WithMany()
                        .HasForeignKey("BusinessTransactionId", "BusinessActionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_OperatorProfileTransactionAction_TransactionAction"),
                    l => l.HasOne<OperatorProfile>().WithMany()
                        .HasForeignKey("OperatorProfileId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_OperatorProfileTransactionAction_OperatorProfile"),
                    j =>
                    {
                        j.HasKey("OperatorProfileId", "BusinessTransactionId", "BusinessActionId");
                        j.ToTable("OperatorProfileTransactionAction", "AccessControl");
                        j.HasIndex(new[] { "BusinessTransactionId", "BusinessActionId" }, "fkIdx_BusinessTransactionID_BusinessActionID");
                        j.HasIndex(new[] { "OperatorProfileId" }, "fkIdx_OperatorProfileID");
                        j.IndexerProperty<int>("OperatorProfileId").HasColumnName("OperatorProfileID");
                        j.IndexerProperty<int>("BusinessTransactionId").HasColumnName("BusinessTransactionID");
                        j.IndexerProperty<int>("BusinessActionId").HasColumnName("BusinessActionID");
                    });
        });

        modelBuilder.Entity<PaymentStatus>(entity =>
        {
            entity.ToTable("PaymentStatus", "MasterData");

            entity.HasIndex(e => e.DocumentTypeId, "fkIdx_DocumentTypeID");

            entity.Property(e => e.PaymentStatusId).HasColumnName("PaymentStatusID");
            entity.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.DocumentType).WithMany(p => p.PaymentStatuses)
                .HasForeignKey(d => d.DocumentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaymentStatus_DocumentType");
        });

        modelBuilder.Entity<PersonType>(entity =>
        {
            entity.ToTable("PersonType", "MasterData");

            entity.HasIndex(e => e.OperatorId, "fkIdx_OperatorID");

            entity.Property(e => e.PersonTypeId).HasColumnName("PersonTypeID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OperatorId).HasColumnName("OperatorID");

            entity.HasOne(d => d.Operator).WithMany(p => p.PersonTypes)
                .HasForeignKey(d => d.OperatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PersonType_Operator");
        });

        modelBuilder.Entity<RecoverPassword>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK_recoverpassword");

            entity.ToTable("RecoverPassword", "AccessControl");

            entity.HasIndex(e => e.BusinessUserId, "fkIdx_968");

            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.BusinessUserId).HasColumnName("BusinessUserID");
            entity.Property(e => e.RecoverCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.RequestDate).HasColumnType("datetime");

            entity.HasOne(d => d.BusinessUser).WithMany(p => p.RecoverPasswords)
                .HasForeignKey(d => d.BusinessUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_967");
        });

        modelBuilder.Entity<SequenceControl>(entity =>
        {
            entity.ToTable("SequenceControl", "MasterData");

            entity.HasIndex(e => e.OperatorId, "fkIdx_OperatorID");

            entity.Property(e => e.SequenceControlId).HasColumnName("SequenceControlID");
            entity.Property(e => e.ObjectName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OperatorId).HasColumnName("OperatorID");

            entity.HasOne(d => d.Operator).WithMany(p => p.SequenceControls)
                .HasForeignKey(d => d.OperatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SequenceControl_Operator");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK_StoreID");

            entity.ToTable("Store", "MasterData");

            entity.HasIndex(e => e.CityId, "fkIdx_CityID");

            entity.HasIndex(e => e.CompanyId, "fkIdx_CompanyID");

            entity.HasIndex(e => e.CountryId, "fkIdx_CoountryID");

            entity.HasIndex(e => e.DepartmentId, "fkIdx_DepartmentID");

            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Logo)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone1)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone2)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TimeZone)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.City).WithMany(p => p.Stores)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Store_City");

            entity.HasOne(d => d.Company).WithMany(p => p.Stores)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Store_Company");

            entity.HasOne(d => d.Country).WithMany(p => p.Stores)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Store_Country");

            entity.HasOne(d => d.Department).WithMany(p => p.Stores)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Store_Department");
        });

        modelBuilder.Entity<SuplierCategory>(entity =>
        {
            entity.ToTable("SuplierCategory", "MasterData");

            entity.HasIndex(e => e.OperatorId, "fkIdx_OperatorID");

            entity.Property(e => e.SuplierCategoryId).HasColumnName("SuplierCategoryID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OperatorId).HasColumnName("OperatorID");

            entity.HasOne(d => d.Operator).WithMany(p => p.SuplierCategories)
                .HasForeignKey(d => d.OperatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SuplierCategory_Operator");
        });

        modelBuilder.Entity<TransactionAction>(entity =>
        {
            entity.HasKey(e => new { e.BusinessTransactionId, e.BusinessActionId });

            entity.ToTable("TransactionAction", "AccessControl");

            entity.HasIndex(e => e.BusinessActionId, "fkIdx_BusinessActionID");

            entity.HasIndex(e => e.BusinessTransactionId, "fkIdx_BusinessTransactionID");

            entity.Property(e => e.BusinessTransactionId).HasColumnName("BusinessTransactionID");
            entity.Property(e => e.BusinessActionId).HasColumnName("BusinessActionID");

            entity.HasOne(d => d.BusinessAction).WithMany(p => p.TransactionActions)
                .HasForeignKey(d => d.BusinessActionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TransactionAction_BusinessAction");

            entity.HasOne(d => d.BusinessTransaction).WithMany(p => p.TransactionActions)
                .HasForeignKey(d => d.BusinessTransactionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TransactionAction_BusinessTransaction");
        });

        modelBuilder.Entity<UserCompanyProfile>(entity =>
        {
            entity.HasKey(e => new { e.BusinessUserId, e.StoreId }).HasName("PK_UserControlProfile");

            entity.ToTable("UserCompanyProfile", "AccessControl");

            entity.HasIndex(e => e.BusinessUserId, "fkIdx_BusinessUserID");

            entity.HasIndex(e => e.CompanyProfileId, "fkIdx_CompanyProfileID");

            entity.HasIndex(e => e.StoreId, "fkIdx_UserCompanyProfile_Store");

            entity.Property(e => e.BusinessUserId).HasColumnName("BusinessUserID");
            entity.Property(e => e.StoreId).HasColumnName("StoreID");
            entity.Property(e => e.CompanyProfileId).HasColumnName("CompanyProfileID");

            entity.HasOne(d => d.BusinessUser).WithMany(p => p.UserCompanyProfiles)
                .HasForeignKey(d => d.BusinessUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserCompanyProfile_BusinessUser");

            entity.HasOne(d => d.CompanyProfile).WithMany(p => p.UserCompanyProfiles)
                .HasForeignKey(d => d.CompanyProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserCompanyProfile_CompanyProfile");

            entity.HasOne(d => d.Store).WithMany(p => p.UserCompanyProfiles)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserCompanyProfile_Sotre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
