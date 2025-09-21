using Mediaspot.Domain.Assets;
using Mediaspot.Domain.Assets.ValueObjects;
using Mediaspot.Domain.Titles;
using Mediaspot.Domain.Transcoding;
using Microsoft.EntityFrameworkCore;

namespace Mediaspot.Infrastructure.Persistence;

public sealed class MediaspotDbContext(DbContextOptions<MediaspotDbContext> options) : DbContext(options)
{
    public DbSet<Title> Titles => Set<Title>();
    public DbSet<Asset> Assets => Set<Asset>();
    public DbSet<TranscodeJob> TranscodeJobs => Set<TranscodeJob>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureTitle(modelBuilder);
        ConfigureAsset(modelBuilder);
        ConfigureTranscodeJob(modelBuilder);
    }

    private static void ConfigureTitle(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Title>(t =>
        {
            t.HasKey(a => a.Id);
            t.Property(a => a.ExternalId).IsRequired();
            t.Property(a => a.Type).IsRequired();
            t.OwnsOne(a => a.Metadata, m =>
            {
                m.Property(x => x.Name).IsRequired();
                m.Property(x => x.Description);
                m.Property(x => x.SeasonNumber);
                m.OwnsOne(x => x.Origin, o =>
                {
                    o.Property(y => y.Country).IsRequired();
                    o.Property(y => y.Language).IsRequired();
                });
            });

            t.HasIndex(a => a.ExternalId).IsUnique();
            t.HasIndex(a => new { a.Type });
        });
    }

    private static void ConfigureAsset(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>(b =>
        {
            b.HasKey(a => a.Id);
            b.Property(a => a.ExternalId).IsRequired();
            b.OwnsOne(a => a.Metadata, m =>
            {
                m.Property(x => x.Title).IsRequired();
                m.Property(x => x.Description);
                m.Property(x => x.Language);
            });

            b.Ignore(a => a.MediaFiles);

            b.OwnsMany<MediaFile>("_mediaFiles", mf =>
            {
                mf.WithOwner().HasForeignKey("AssetId");
                mf.ToTable("MediaFiles");

                mf.HasKey(mf => mf.Id);

                mf.Property(x => x.Id)
                    .HasConversion(v => v.Value, v => new MediaFileId(v))
                    .HasColumnName("MediaFileId")
                    .IsRequired();

                mf.Property(mf => mf.Path).HasConversion(v => v.Value, v => new FilePath(v)).HasColumnName("Path").IsRequired();

                mf.Property(mf => mf.Duration).HasConversion(v => v.Value.TotalSeconds, v => new Duration(TimeSpan.FromMilliseconds(v))).HasColumnName("Duration").IsRequired();
            });

            b.Navigation("_mediaFiles").UsePropertyAccessMode(PropertyAccessMode.Field);

            b.Property<bool>("Archived").HasField("<Archived>k__BackingField");

            b.HasIndex(a => a.ExternalId).IsUnique();
        });
    }

    private static void ConfigureTranscodeJob(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TranscodeJob>(b =>
        {
            b.HasKey(j => j.Id);
            b.Property(j => j.Preset).IsRequired();
            b.Property(j => j.Status);
            b.Property(j => j.CreatedAt).IsRequired();
            b.Property(j => j.UpdatedAt);

            b.HasIndex(j => new { j.AssetId, j.Status });
        });
    }
}
