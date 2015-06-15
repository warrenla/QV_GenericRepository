using System.Data.Entity.ModelConfiguration;
using QV.Data.Models;

namespace QV.Data.Mapping
{
    public class DockMap : EntityTypeConfiguration<Dock>
    {
        public DockMap()
        {
            // Primary Key
            this.HasKey(t => t.DockId);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.Type)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Dock");
            this.Property(t => t.DockId).HasColumnName("DockId");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Sequence).HasColumnName("Sequence");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.SiteId).HasColumnName("SiteId").IsRequired();

            // Relationships
            this.HasRequired(t => t.Site)
                .WithMany(t => t.Docks)
                .HasForeignKey(d => d.SiteId);
            

        }
    }
}
