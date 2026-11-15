using Microsoft.EntityFrameworkCore;

namespace MediaDeckSite.Data;

/// <summary>
/// アプリケーションの PostgreSQL データベースコンテキスト。
/// </summary>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<FeedbackEntity> Feedbacks => Set<FeedbackEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FeedbackEntity>(entity =>
        {
            entity.ToTable("feedbacks");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Category)
                .HasColumnName("category")
                .HasMaxLength(32)
                .IsRequired();

            entity.Property(e => e.Title)
                .HasColumnName("title")
                .HasMaxLength(500)
                .IsRequired();

            entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasColumnType("text")
                .IsRequired();

            entity.Property(e => e.Contact)
                .HasColumnName("contact")
                .HasMaxLength(500);

            entity.Property(e => e.EnvironmentInfo)
                .HasColumnName("environment_info")
                .HasColumnType("text");

            entity.Property(e => e.LogContent)
                .HasColumnName("log_content")
                .HasColumnType("text");

            entity.Property(e => e.ScreenshotData)
                .HasColumnName("screenshot_data")
                .HasColumnType("bytea");

            entity.Property(e => e.ScreenshotMimeType)
                .HasColumnName("screenshot_mime_type")
                .HasMaxLength(32);

            entity.Property(e => e.ScreenshotExtension)
                .HasColumnName("screenshot_extension")
                .HasMaxLength(16);

            entity.Property(e => e.SubmittedAt)
                .HasColumnName("submitted_at")
                .HasColumnType("timestamptz")
                .IsRequired();

            entity.Property(e => e.ReceivedAt)
                .HasColumnName("received_at")
                .HasColumnType("timestamptz")
                .IsRequired();

            entity.Property(e => e.ClientIp)
                .HasColumnName("client_ip")
                .HasMaxLength(64);

            entity.HasIndex(e => e.SubmittedAt)
                .HasDatabaseName("ix_feedbacks_submitted_at");

            entity.HasIndex(e => e.Category)
                .HasDatabaseName("ix_feedbacks_category");
        });
    }
}
