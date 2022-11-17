﻿// <auto-generated />
using System;
using LibHouse.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LibHouse.Data.Migrations
{
    [DbContext(typeof(LibHouseContext))]
    partial class LibHouseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("LibHouse.Business.Entities.Residents.Preferences.ResidentPreferences", b =>
                {
                    b.Property<Guid>("ResidentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ResidentId");

                    b.HasIndex("ResidentId")
                        .IsUnique()
                        .HasDatabaseName("idx_residentpreferences_residentid");

                    b.ToTable("ResidentPreferences", "Business");
                });

            modelBuilder.Entity("LibHouse.Business.Entities.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.HasKey("Id");

                    b.ToTable("Users", "Business");
                });

            modelBuilder.Entity("LibHouse.Business.Entities.Owners.Owner", b =>
                {
                    b.HasBaseType("LibHouse.Business.Entities.Users.User");

                    b.ToTable("Owners", "Business");
                });

            modelBuilder.Entity("LibHouse.Business.Entities.Residents.Resident", b =>
                {
                    b.HasBaseType("LibHouse.Business.Entities.Users.User");

                    b.ToTable("Residents", "Business");
                });

            modelBuilder.Entity("LibHouse.Business.Entities.Residents.Preferences.ResidentPreferences", b =>
                {
                    b.HasOne("LibHouse.Business.Entities.Residents.Resident", "Resident")
                        .WithOne("ResidentPreferences")
                        .HasForeignKey("LibHouse.Business.Entities.Residents.Preferences.ResidentPreferences", "ResidentId");

                    b.OwnsOne("LibHouse.Business.Entities.Residents.Preferences.Charges.ChargePreferences", "ChargePreferences", b1 =>
                        {
                            b1.Property<Guid>("ResidentPreferencesResidentId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("ResidentPreferencesResidentId");

                            b1.ToTable("ResidentPreferences");

                            b1.WithOwner()
                                .HasForeignKey("ResidentPreferencesResidentId");

                            b1.OwnsOne("LibHouse.Business.Entities.Residents.Preferences.Charges.ExpensePreferences", "ExpensePreferences", b2 =>
                                {
                                    b2.Property<Guid>("ChargePreferencesResidentPreferencesResidentId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.HasKey("ChargePreferencesResidentPreferencesResidentId");

                                    b2.ToTable("ResidentPreferences");

                                    b2.WithOwner()
                                        .HasForeignKey("ChargePreferencesResidentPreferencesResidentId");

                                    b2.OwnsOne("LibHouse.Business.Entities.Shared.MonetaryRange", "ExpenseRange", b3 =>
                                        {
                                            b3.Property<Guid>("ExpensePreferencesChargePreferencesResidentPreferencesResidentId")
                                                .HasColumnType("uniqueidentifier");

                                            b3.Property<decimal>("MaximumValue")
                                                .HasColumnType("decimal(6,2)")
                                                .HasColumnName("ChargePreferences_Expense_MaximumExpenseAmountDesired");

                                            b3.Property<decimal>("MinimumValue")
                                                .HasColumnType("decimal(6,2)")
                                                .HasColumnName("ChargePreferences_Expense_MinimumExpenseAmountDesired");

                                            b3.HasKey("ExpensePreferencesChargePreferencesResidentPreferencesResidentId");

                                            b3.ToTable("ResidentPreferences");

                                            b3.WithOwner()
                                                .HasForeignKey("ExpensePreferencesChargePreferencesResidentPreferencesResidentId");
                                        });

                                    b2.Navigation("ExpenseRange");
                                });

                            b1.OwnsOne("LibHouse.Business.Entities.Residents.Preferences.Charges.RentPreferences", "RentPreferences", b2 =>
                                {
                                    b2.Property<Guid>("ChargePreferencesResidentPreferencesResidentId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.HasKey("ChargePreferencesResidentPreferencesResidentId");

                                    b2.ToTable("ResidentPreferences");

                                    b2.WithOwner()
                                        .HasForeignKey("ChargePreferencesResidentPreferencesResidentId");

                                    b2.OwnsOne("LibHouse.Business.Entities.Shared.MonetaryRange", "RentRange", b3 =>
                                        {
                                            b3.Property<Guid>("RentPreferencesChargePreferencesResidentPreferencesResidentId")
                                                .HasColumnType("uniqueidentifier");

                                            b3.Property<decimal>("MaximumValue")
                                                .HasColumnType("decimal(6,2)")
                                                .HasColumnName("ChargePreferences_Rent_MaximumRentAmountDesired");

                                            b3.Property<decimal>("MinimumValue")
                                                .HasColumnType("decimal(6,2)")
                                                .HasColumnName("ChargePreferences_Rent_MinimumRentAmountDesired");

                                            b3.HasKey("RentPreferencesChargePreferencesResidentPreferencesResidentId");

                                            b3.ToTable("ResidentPreferences");

                                            b3.WithOwner()
                                                .HasForeignKey("RentPreferencesChargePreferencesResidentPreferencesResidentId");
                                        });

                                    b2.Navigation("RentRange");
                                });

                            b1.Navigation("ExpensePreferences");

                            b1.Navigation("RentPreferences");
                        });

                    b.OwnsOne("LibHouse.Business.Entities.Residents.Preferences.Rooms.RoomPreferences", "RoomPreferences", b1 =>
                        {
                            b1.Property<Guid>("ResidentPreferencesResidentId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("ResidentPreferencesResidentId");

                            b1.ToTable("ResidentPreferences");

                            b1.WithOwner()
                                .HasForeignKey("ResidentPreferencesResidentId");

                            b1.OwnsOne("LibHouse.Business.Entities.Residents.Preferences.Rooms.BathroomPreferences", "BathroomPreferences", b2 =>
                                {
                                    b2.Property<Guid>("RoomPreferencesResidentPreferencesResidentId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("BathroomType")
                                        .IsRequired()
                                        .HasMaxLength(6)
                                        .HasColumnType("varchar(6)")
                                        .HasColumnName("RoomPreferences_Bathroom_BathroomType");

                                    b2.HasKey("RoomPreferencesResidentPreferencesResidentId");

                                    b2.ToTable("ResidentPreferences");

                                    b2.WithOwner()
                                        .HasForeignKey("RoomPreferencesResidentPreferencesResidentId");
                                });

                            b1.OwnsOne("LibHouse.Business.Entities.Residents.Preferences.Rooms.DormitoryPreferences", "DormitoryPreferences", b2 =>
                                {
                                    b2.Property<Guid>("RoomPreferencesResidentPreferencesResidentId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("DormitoryType")
                                        .IsRequired()
                                        .HasMaxLength(6)
                                        .HasColumnType("varchar(6)")
                                        .HasColumnName("RoomPreferences_Dormitory_DormitoryType");

                                    b2.Property<bool>("RequireFurnishedDormitory")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("bit")
                                        .HasColumnName("RoomPreferences_Dormitory_RequireFurnishedDormitory")
                                        .HasDefaultValueSql("0");

                                    b2.HasKey("RoomPreferencesResidentPreferencesResidentId");

                                    b2.ToTable("ResidentPreferences");

                                    b2.WithOwner()
                                        .HasForeignKey("RoomPreferencesResidentPreferencesResidentId");
                                });

                            b1.OwnsOne("LibHouse.Business.Entities.Residents.Preferences.Rooms.GaragePreferences", "GaragePreferences", b2 =>
                                {
                                    b2.Property<Guid>("RoomPreferencesResidentPreferencesResidentId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<bool>("GarageIsRequired")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("bit")
                                        .HasColumnName("RoomPreferences_Garage_GarageIsRequired")
                                        .HasDefaultValueSql("0");

                                    b2.HasKey("RoomPreferencesResidentPreferencesResidentId");

                                    b2.ToTable("ResidentPreferences");

                                    b2.WithOwner()
                                        .HasForeignKey("RoomPreferencesResidentPreferencesResidentId");
                                });

                            b1.OwnsOne("LibHouse.Business.Entities.Residents.Preferences.Rooms.KitchenPreferences", "KitchenPreferences", b2 =>
                                {
                                    b2.Property<Guid>("RoomPreferencesResidentPreferencesResidentId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<bool>("MicrowaveIsRequired")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("bit")
                                        .HasColumnName("RoomPreferences_Kitchen_MicrowaveIsRequired")
                                        .HasDefaultValueSql("0");

                                    b2.Property<bool>("RefrigeratorIsRequired")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("bit")
                                        .HasColumnName("RoomPreferences_Kitchen_RefrigeratorIsRequired")
                                        .HasDefaultValueSql("0");

                                    b2.Property<bool>("StoveIsRequired")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("bit")
                                        .HasColumnName("RoomPreferences_Kitchen_StoveIsRequired")
                                        .HasDefaultValueSql("0");

                                    b2.HasKey("RoomPreferencesResidentPreferencesResidentId");

                                    b2.ToTable("ResidentPreferences");

                                    b2.WithOwner()
                                        .HasForeignKey("RoomPreferencesResidentPreferencesResidentId");
                                });

                            b1.OwnsOne("LibHouse.Business.Entities.Residents.Preferences.Rooms.OtherRoomPreferences", "OtherRoomPreferences", b2 =>
                                {
                                    b2.Property<Guid>("RoomPreferencesResidentPreferencesResidentId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<bool>("RecreationAreaIsRequired")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("bit")
                                        .HasColumnName("RoomPreferences_Other_RecreationAreaIsRequired")
                                        .HasDefaultValueSql("0");

                                    b2.Property<bool>("ServiceAreaIsRequired")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("bit")
                                        .HasColumnName("RoomPreferences_Other_ServiceAreaIsRequired")
                                        .HasDefaultValueSql("0");

                                    b2.HasKey("RoomPreferencesResidentPreferencesResidentId");

                                    b2.ToTable("ResidentPreferences");

                                    b2.WithOwner()
                                        .HasForeignKey("RoomPreferencesResidentPreferencesResidentId");
                                });

                            b1.Navigation("BathroomPreferences");

                            b1.Navigation("DormitoryPreferences");

                            b1.Navigation("GaragePreferences");

                            b1.Navigation("KitchenPreferences");

                            b1.Navigation("OtherRoomPreferences");
                        });

                    b.OwnsOne("LibHouse.Business.Entities.Residents.Preferences.Services.ServicesPreferences", "ServicesPreferences", b1 =>
                        {
                            b1.Property<Guid>("ResidentPreferencesResidentId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("ResidentPreferencesResidentId");

                            b1.ToTable("ResidentPreferences");

                            b1.WithOwner()
                                .HasForeignKey("ResidentPreferencesResidentId");

                            b1.OwnsOne("LibHouse.Business.Entities.Residents.Preferences.Services.CleaningPreferences", "CleaningPreferences", b2 =>
                                {
                                    b2.Property<Guid>("ServicesPreferencesResidentPreferencesResidentId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<bool>("HouseCleaningIsRequired")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("bit")
                                        .HasColumnName("ServicesPreferences_Cleaning_HouseCleaningIsRequired")
                                        .HasDefaultValueSql("0");

                                    b2.HasKey("ServicesPreferencesResidentPreferencesResidentId");

                                    b2.ToTable("ResidentPreferences");

                                    b2.WithOwner()
                                        .HasForeignKey("ServicesPreferencesResidentPreferencesResidentId");
                                });

                            b1.OwnsOne("LibHouse.Business.Entities.Residents.Preferences.Services.InternetPreferences", "InternetPreferences", b2 =>
                                {
                                    b2.Property<Guid>("ServicesPreferencesResidentPreferencesResidentId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<bool>("InternetServiceIsRequired")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("bit")
                                        .HasColumnName("ServicesPreferences_Internet_InternetServiceIsRequired")
                                        .HasDefaultValueSql("0");

                                    b2.HasKey("ServicesPreferencesResidentPreferencesResidentId");

                                    b2.ToTable("ResidentPreferences");

                                    b2.WithOwner()
                                        .HasForeignKey("ServicesPreferencesResidentPreferencesResidentId");
                                });

                            b1.OwnsOne("LibHouse.Business.Entities.Residents.Preferences.Services.TelevisionPreferences", "TelevisionPreferences", b2 =>
                                {
                                    b2.Property<Guid>("ServicesPreferencesResidentPreferencesResidentId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<bool>("CableTelevisionIsRequired")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("bit")
                                        .HasColumnName("ServicesPreferences_Television_CableTelevisionIsRequired")
                                        .HasDefaultValueSql("0");

                                    b2.HasKey("ServicesPreferencesResidentPreferencesResidentId");

                                    b2.ToTable("ResidentPreferences");

                                    b2.WithOwner()
                                        .HasForeignKey("ServicesPreferencesResidentPreferencesResidentId");
                                });

                            b1.Navigation("CleaningPreferences");

                            b1.Navigation("InternetPreferences");

                            b1.Navigation("TelevisionPreferences");
                        });

                    b.Navigation("ChargePreferences");

                    b.Navigation("Resident");

                    b.Navigation("RoomPreferences");

                    b.Navigation("ServicesPreferences");
                });

            modelBuilder.Entity("LibHouse.Business.Entities.Users.User", b =>
                {
                    b.OwnsOne("LibHouse.Business.Entities.Users.Cpf", "CPF", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(11)
                                .HasColumnType("char(11)")
                                .HasColumnName("Cpf");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("LibHouse.Business.Entities.Users.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(60)
                                .HasColumnType("varchar(60)")
                                .HasColumnName("Email");

                            b1.HasKey("UserId");

                            b1.HasIndex("Value")
                                .IsUnique()
                                .HasDatabaseName("idx_user_email");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("LibHouse.Business.Entities.Users.Phone", "Phone", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(15)
                                .HasColumnType("varchar(15)")
                                .HasColumnName("Phone");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("CPF");

                    b.Navigation("Email");

                    b.Navigation("Phone");
                });

            modelBuilder.Entity("LibHouse.Business.Entities.Owners.Owner", b =>
                {
                    b.HasOne("LibHouse.Business.Entities.Users.User", null)
                        .WithOne()
                        .HasForeignKey("LibHouse.Business.Entities.Owners.Owner", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LibHouse.Business.Entities.Residents.Resident", b =>
                {
                    b.HasOne("LibHouse.Business.Entities.Users.User", null)
                        .WithOne()
                        .HasForeignKey("LibHouse.Business.Entities.Residents.Resident", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LibHouse.Business.Entities.Residents.Resident", b =>
                {
                    b.Navigation("ResidentPreferences");
                });
#pragma warning restore 612, 618
        }
    }
}
