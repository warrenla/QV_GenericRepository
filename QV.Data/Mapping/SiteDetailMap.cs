using System.Data.Entity.ModelConfiguration;
using QV.Data.Models;

namespace QV.Data.Mapping
{
    public class SiteDetailMap : EntityTypeConfiguration<SiteDetail>
    {
        public SiteDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.SiteDetailId);

            // Properties
            this.Property(t => t.Key)
                .IsFixedLength()
                .HasMaxLength(25);

            this.Property(t => t.Data)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(25);

            // Table & Column Mappings
            this.ToTable("SiteDetail");
            this.Property(t => t.SiteDetailId).HasColumnName("SiteDetailId");
            this.Property(t => t.SiteId).HasColumnName("SiteId");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.Data).HasColumnName("Data");
           

            // Relationships
            this.HasRequired(t => t.Site)
                .WithMany(t => t.SiteDetails)
                .HasForeignKey(d => d.SiteId);

        }
    }
}
