using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace QV.Data.Models.Mapping
{
    public class DockDetailMap : EntityTypeConfiguration<DockDetail>
    {
        public DockDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.DockDetailId);

            // Properties
            // Table & Column Mappings
            this.ToTable("DockDetail");
            this.Property(t => t.DockDetailId).HasColumnName("DockDetailId");
            this.Property(t => t.DockId).HasColumnName("DockId");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.Data).HasColumnName("Data");
          
            // Relationships
            this.HasRequired(t => t.Dock)
                .WithMany(t => t.DockDetails)
                .HasForeignKey(d => d.DockId);

        }
    }
}
