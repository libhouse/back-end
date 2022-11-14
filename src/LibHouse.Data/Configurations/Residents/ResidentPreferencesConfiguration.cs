using LibHouse.Business.Entities.Residents.Preferences;
using LibHouse.Business.Entities.Residents.Preferences.Rooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LibHouse.Data.Configurations.Residents
{
    internal class ResidentPreferencesConfiguration : IEntityTypeConfiguration<ResidentPreferences>
    {
        public void Configure(EntityTypeBuilder<ResidentPreferences> builder)
        {
            builder.ToTable("ResidentPreferences", "Business");
            builder.HasKey("ResidentId");
            builder.OwnsOne<RoomPreferences>(navigationName: "RoomPreferences", roomPreferences =>
            {
                roomPreferences.OwnsOne(r => r.BathroomPreferences, bathroomPreferences =>
                {
                    bathroomPreferences.Property(b => b.BathroomType).HasColumnName("RoomPreferences_Bathroom_BathroomType").HasConversion(new EnumToStringConverter<BathroomType>()).HasColumnType("varchar").HasMaxLength(6);
                });
                roomPreferences.OwnsOne(r => r.DormitoryPreferences, dormitoryPreferences =>
                {
                    dormitoryPreferences.Property(d => d.DormitoryType).HasColumnName("RoomPreferences_Dormitory_DormitoryType").HasConversion(new EnumToStringConverter<DormitoryType>()).HasColumnType("varchar").HasMaxLength(6);
                    dormitoryPreferences.Property(d => d.RequireFurnishedDormitory).HasColumnName("RoomPreferences_Dormitory_RequireFurnishedDormitory").HasColumnType("bit").HasDefaultValueSql("0");
                });
                roomPreferences.OwnsOne(r => r.GaragePreferences, garagePreferences =>
                {
                    garagePreferences.Property(g => g.GarageIsRequired).HasColumnName("RoomPreferences_Garage_GarageIsRequired").HasColumnType("bit").HasDefaultValueSql("0");
                });
                roomPreferences.OwnsOne(r => r.KitchenPreferences, kitchenPreferences =>
                {
                    kitchenPreferences.Property(k => k.MicrowaveIsRequired).HasColumnName("RoomPreferences_Kitchen_MicrowaveIsRequired").HasColumnType("bit").HasDefaultValueSql("0");
                    kitchenPreferences.Property(k => k.RefrigeratorIsRequired).HasColumnName("RoomPreferences_Kitchen_RefrigeratorIsRequired").HasColumnType("bit").HasDefaultValueSql("0");
                    kitchenPreferences.Property(k => k.StoveIsRequired).HasColumnName("RoomPreferences_Kitchen_StoveIsRequired").HasColumnType("bit").HasDefaultValueSql("0");
                });
                roomPreferences.OwnsOne(r => r.OtherRoomPreferences, otherRoomPreferences =>
                {
                    otherRoomPreferences.Property(o => o.RecreationAreaIsRequired).HasColumnName("RoomPreferences_Other_RecreationAreaIsRequired").HasColumnType("bit").HasDefaultValueSql("0");
                    otherRoomPreferences.Property(o => o.ServiceAreaIsRequired).HasColumnName("RoomPreferences_Other_ServiceAreaIsRequired").HasColumnType("bit").HasDefaultValueSql("0");
                });
            });
            builder.HasIndex("ResidentId").HasDatabaseName("idx_residentpreferences_residentid").IsUnique();
        }
    }
}