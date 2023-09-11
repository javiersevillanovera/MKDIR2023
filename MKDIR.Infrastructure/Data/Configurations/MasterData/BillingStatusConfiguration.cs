using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MKDIR.Domain;

namespace MKDIR.Infrastructure
{
    public class BillingStatusConfiguration : IEntityTypeConfiguration<BillingStatus>
    {
        public void Configure(EntityTypeBuilder<BillingStatus> builder)
        {
            builder.HasKey(e => e.BillingStatusId).HasName("PK_BillingStatus");

            builder.ToTable("BillingStatus", "MasterData");

            builder.HasIndex(e => e.DocumentTypeId, "fkIdx_DocumentTypeID");

            builder.Property(e => e.BillingStatusId).HasColumnName("BillingStatusID");
            builder.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeID");
            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.DocumentType).WithMany(p => p.BillingStatuses)
                .HasForeignKey(d => d.DocumentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BillingStatus_DocumentType");
        }
    }
}
