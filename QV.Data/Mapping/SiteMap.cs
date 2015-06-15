using System.Data.Entity.ModelConfiguration;
using QV.Data.Models;

namespace QV.Data.Mapping
{
    public class SiteMap : EntityTypeConfiguration<Site>
    {
        public SiteMap()
        {
            // Primary Key
            this.HasKey(t => t.SiteId);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(150);

            this.Property(t => t.ShortName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PropertyName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Type)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Site");
            this.Property(t => t.SiteId).HasColumnName("SiteId");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Properties).HasColumnName("Properties");
            this.Property(t => t.ShortName).HasColumnName("ShortName");
            this.Property(t => t.PropertyName).HasColumnName("PropertyName");
            this.Property(t => t.Type).HasColumnName("Type");
        }
    }
}
