﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThAmCo.Catering.Data;

#nullable disable

namespace ThAmCo.Catering.Data.Migrations
{
    [DbContext(typeof(CateringDbContext))]
    partial class CateringDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.23");

            modelBuilder.Entity("ThAmCo.Catering.Data.FoodBooking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClientReferenceId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MenuId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberOfGuests")
                        .HasColumnType("INTEGER");

                    b.HasKey("BookingId");

                    b.HasIndex("MenuId");

                    b.ToTable("FoodBooking");

                    b.HasData(
                        new
                        {
                            BookingId = 1,
                            ClientReferenceId = 1,
                            MenuId = 1,
                            NumberOfGuests = 1
                        },
                        new
                        {
                            BookingId = 2,
                            ClientReferenceId = 2,
                            MenuId = 2,
                            NumberOfGuests = 2
                        },
                        new
                        {
                            BookingId = 3,
                            ClientReferenceId = 3,
                            MenuId = 3,
                            NumberOfGuests = 3
                        },
                        new
                        {
                            BookingId = 4,
                            ClientReferenceId = 4,
                            MenuId = 4,
                            NumberOfGuests = 4
                        });
                });

            modelBuilder.Entity("ThAmCo.Catering.Data.FoodItem", b =>
                {
                    b.Property<int>("FoodItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.HasKey("FoodItemId");

                    b.ToTable("FoodItem");

                    b.HasData(
                        new
                        {
                            FoodItemId = 1,
                            Name = "Spaghetti Bolognese",
                            Price = 8.5
                        },
                        new
                        {
                            FoodItemId = 2,
                            Name = "Chicken Alfredo",
                            Price = 9.0
                        },
                        new
                        {
                            FoodItemId = 3,
                            Name = "Prawn Linguine",
                            Price = 10.5
                        },
                        new
                        {
                            FoodItemId = 4,
                            Name = "Pizza",
                            Price = 9.0
                        },
                        new
                        {
                            FoodItemId = 5,
                            Name = "Beef Wellington",
                            Price = 12.0
                        },
                        new
                        {
                            FoodItemId = 6,
                            Name = "Shepards Pie",
                            Price = 9.0
                        },
                        new
                        {
                            FoodItemId = 7,
                            Name = "Fish and Chips",
                            Price = 9.5
                        },
                        new
                        {
                            FoodItemId = 8,
                            Name = "Toad in the Hole",
                            Price = 9.0
                        },
                        new
                        {
                            FoodItemId = 9,
                            Name = "Peking Roasted Duck",
                            Price = 12.0
                        },
                        new
                        {
                            FoodItemId = 10,
                            Name = "Kung Pao Chicken",
                            Price = 9.0
                        },
                        new
                        {
                            FoodItemId = 11,
                            Name = "Sweet and Sour Pork",
                            Price = 10.0
                        },
                        new
                        {
                            FoodItemId = 12,
                            Name = "Beef Char Siu",
                            Price = 10.5
                        },
                        new
                        {
                            FoodItemId = 13,
                            Name = "Chicken Tikka",
                            Price = 10.0
                        },
                        new
                        {
                            FoodItemId = 14,
                            Name = "Chicken Vindaloo",
                            Price = 10.0
                        },
                        new
                        {
                            FoodItemId = 15,
                            Name = "Lamb Biryani",
                            Price = 12.0
                        },
                        new
                        {
                            FoodItemId = 16,
                            Name = "Rogan Josh",
                            Price = 10.5
                        });
                });

            modelBuilder.Entity("ThAmCo.Catering.Data.Menu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.HasKey("MenuId");

                    b.ToTable("Menu");

                    b.HasData(
                        new
                        {
                            MenuId = 1,
                            Name = "Italian"
                        },
                        new
                        {
                            MenuId = 2,
                            Name = "British"
                        },
                        new
                        {
                            MenuId = 3,
                            Name = "Chinese"
                        },
                        new
                        {
                            MenuId = 4,
                            Name = "Indian"
                        });
                });

            modelBuilder.Entity("ThAmCo.Catering.Data.MenuFoodItem", b =>
                {
                    b.Property<int>("MenuId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FoodItemId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MenuId", "FoodItemId");

                    b.HasIndex("FoodItemId");

                    b.ToTable("MenuFoodItem");

                    b.HasData(
                        new
                        {
                            MenuId = 1,
                            FoodItemId = 1
                        },
                        new
                        {
                            MenuId = 2,
                            FoodItemId = 1
                        },
                        new
                        {
                            MenuId = 1,
                            FoodItemId = 2
                        },
                        new
                        {
                            MenuId = 1,
                            FoodItemId = 3
                        },
                        new
                        {
                            MenuId = 1,
                            FoodItemId = 4
                        },
                        new
                        {
                            MenuId = 2,
                            FoodItemId = 5
                        },
                        new
                        {
                            MenuId = 2,
                            FoodItemId = 6
                        },
                        new
                        {
                            MenuId = 2,
                            FoodItemId = 7
                        },
                        new
                        {
                            MenuId = 2,
                            FoodItemId = 8
                        },
                        new
                        {
                            MenuId = 3,
                            FoodItemId = 9
                        },
                        new
                        {
                            MenuId = 3,
                            FoodItemId = 10
                        },
                        new
                        {
                            MenuId = 3,
                            FoodItemId = 11
                        },
                        new
                        {
                            MenuId = 3,
                            FoodItemId = 12
                        },
                        new
                        {
                            MenuId = 4,
                            FoodItemId = 13
                        },
                        new
                        {
                            MenuId = 4,
                            FoodItemId = 14
                        },
                        new
                        {
                            MenuId = 4,
                            FoodItemId = 15
                        },
                        new
                        {
                            MenuId = 4,
                            FoodItemId = 16
                        });
                });

            modelBuilder.Entity("ThAmCo.Catering.Data.FoodBooking", b =>
                {
                    b.HasOne("ThAmCo.Catering.Data.Menu", "Menu")
                        .WithMany("FoodBooking")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("ThAmCo.Catering.Data.MenuFoodItem", b =>
                {
                    b.HasOne("ThAmCo.Catering.Data.FoodItem", "FoodItem")
                        .WithMany("MenuFoodItem")
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ThAmCo.Catering.Data.Menu", "Menu")
                        .WithMany("MenuFoodItem")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FoodItem");

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("ThAmCo.Catering.Data.FoodItem", b =>
                {
                    b.Navigation("MenuFoodItem");
                });

            modelBuilder.Entity("ThAmCo.Catering.Data.Menu", b =>
                {
                    b.Navigation("FoodBooking");

                    b.Navigation("MenuFoodItem");
                });
#pragma warning restore 612, 618
        }
    }
}
