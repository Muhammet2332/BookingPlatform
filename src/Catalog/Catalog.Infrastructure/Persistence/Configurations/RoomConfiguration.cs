using Catalog.Domain.Entities;
using Catalog.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.Configurations;

public sealed class RoomConfiguration
    : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("rooms");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Number)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(r => r.Number)
            .IsUnique();

        builder.Property(r => r.Description)
            .HasMaxLength(1000);

        builder.Property(r => r.BasePricePerNight)
            .HasPrecision(10, 2);

        builder.Property(r => r.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .HasDefaultValue(RoomStatus.Active);

        builder.HasOne(r => r.RoomType)
            .WithMany(rt => rt.Rooms)
            .HasForeignKey(r => r.RoomTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(r => r.Amenities)
            .WithMany(a => a.Rooms)
            .UsingEntity(
                "room_amenities",
                right => right
                    .HasOne(typeof(Amenity))
                    .WithMany()
                    .HasForeignKey("AmenityId")
                    .OnDelete(DeleteBehavior.Cascade),
                left => left
                    .HasOne(typeof(Room))
                    .WithMany()
                    .HasForeignKey("RoomId")
                    .OnDelete(DeleteBehavior.Cascade),
                join =>
                {
                    join.HasKey("RoomId", "AmenityId");
                    join.ToTable("room_amenities");
                });
    }
}