using LibHouse.Business.Entities.Residents.Preferences;
using LibHouse.Business.Entities.Residents.Preferences.Charges;
using LibHouse.Business.Entities.Residents.Preferences.General;
using LibHouse.Business.Entities.Residents.Preferences.Rooms;
using LibHouse.Business.Entities.Residents.Preferences.Services;
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
            builder.OwnsOne<ServicesPreferences>(navigationName: "ServicesPreferences", servicesPreferences =>
            {
                servicesPreferences.OwnsOne(s => s.CleaningPreferences, cleaningPreferences =>
                {
                    cleaningPreferences.Property(c => c.HouseCleaningIsRequired).HasColumnName("ServicesPreferences_Cleaning_HouseCleaningIsRequired").HasColumnType("bit").HasDefaultValueSql("0");
                });
                servicesPreferences.OwnsOne(s => s.InternetPreferences, internetPreferences =>
                {
                    internetPreferences.Property(i => i.InternetServiceIsRequired).HasColumnName("ServicesPreferences_Internet_InternetServiceIsRequired").HasColumnType("bit").HasDefaultValueSql("0");
                });
                servicesPreferences.OwnsOne(s => s.TelevisionPreferences, televisionPreferences =>
                {
                    televisionPreferences.Property(t => t.CableTelevisionIsRequired).HasColumnName("ServicesPreferences_Television_CableTelevisionIsRequired").HasColumnType("bit").HasDefaultValueSql("0");
                });
            });
            builder.OwnsOne<ChargePreferences>(navigationName: "ChargePreferences", chargePreferences =>
            {
                chargePreferences.OwnsOne(c => c.ExpensePreferences, expensePreferences =>
                {
                    expensePreferences.OwnsOne(e => e.ExpenseRange, expenseRange =>
                    {
                        expenseRange.Property(r => r.MinimumValue).HasColumnName("ChargePreferences_Expense_MinimumExpenseAmountDesired").HasColumnType("decimal(6,2)");
                        expenseRange.Property(r => r.MaximumValue).HasColumnName("ChargePreferences_Expense_MaximumExpenseAmountDesired").HasColumnType("decimal(6,2)");
                    });
                });
                chargePreferences.OwnsOne(c => c.RentPreferences, rentPreferences =>
                {
                    rentPreferences.OwnsOne(r => r.RentRange, rentRange =>
                    {
                        rentRange.Property(r => r.MinimumValue).HasColumnName("ChargePreferences_Rent_MinimumRentAmountDesired").HasColumnType("decimal(6,2)");
                        rentRange.Property(r => r.MaximumValue).HasColumnName("ChargePreferences_Rent_MaximumRentAmountDesired").HasColumnType("decimal(6,2)");
                    });
                });
            });
            builder.OwnsOne<GeneralPreferences>(navigationName: "GeneralPreferences", generalPreferences =>
            {
                generalPreferences.OwnsOne(g => g.AnimalPreferences, animalPreferences =>
                {
                    animalPreferences.Property(a => a.WantSpaceForAnimals).HasColumnName("GeneralPreferences_Animal_WantSpaceForAnimals").HasColumnType("bit").HasDefaultValueSql("0");
                });
                generalPreferences.OwnsOne(g => g.ChildrenPreferences, childrenPreferences =>
                {
                    childrenPreferences.Property(c => c.AcceptChildren).HasColumnName("GeneralPreferences_Children_AcceptChildren").HasColumnType("bit").HasDefaultValueSql("0");
                });
                generalPreferences.OwnsOne(g => g.PartyPreferences, partyPreferences =>
                {
                    partyPreferences.Property(p => p.WantsToParty).HasColumnName("GeneralPreferences_Party_WantsToParty").HasColumnType("bit").HasDefaultValueSql("0");
                });
                generalPreferences.OwnsOne(g => g.RoommatePreferences, roommatePreferences =>
                {
                    roommatePreferences.Ignore(r => r.AcceptsRoommatesOfAllGenders);
                    roommatePreferences.Property(r => r.AcceptsOnlyFemaleRoommates).HasColumnName("GeneralPreferences_Roommate_AcceptsOnlyFemaleRoommates").HasColumnType("bit").HasDefaultValueSql("0");
                    roommatePreferences.Property(r => r.AcceptsOnlyMaleRoommates).HasColumnName("GeneralPreferences_Roommate_AcceptsOnlyMaleRoommates").HasColumnType("bit").HasDefaultValueSql("0");
                    roommatePreferences.OwnsOne(r => r.AcceptedRangeOfRoommates, rangeOfRoommates =>
                    {
                        rangeOfRoommates.Property(r => r.InitialValue).HasColumnName("GeneralPreferences_Roommate_MinimumNumberOfRoommatesDesired").HasColumnType("tinyint");
                        rangeOfRoommates.Property(r => r.LastValue).HasColumnName("GeneralPreferences_Roommate_MaximumNumberOfRoommatesDesired").HasColumnType("tinyint");
                    });
                });
                generalPreferences.OwnsOne(g => g.SmokersPreferences, smokersPreferences =>
                {
                    smokersPreferences.Property(s => s.AcceptSmokers).HasColumnName("GeneralPreferences_Smokers_AcceptSmokers").HasColumnType("bit").HasDefaultValueSql("0");
                });
            });
            builder.HasIndex("ResidentId").HasDatabaseName("idx_residentpreferences_residentid").IsUnique();
        }
    }
}