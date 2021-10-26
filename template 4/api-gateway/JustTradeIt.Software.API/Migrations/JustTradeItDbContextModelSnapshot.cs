﻿// <auto-generated />
using System;
using JustTradeIt.Software.API.Repositories.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JustTradeIt.Software.API.Migrations
{
    [DbContext(typeof(JustTradeItDbContext))]
    partial class JustTradeItDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Identifier")
                        .HasColumnType("TEXT");

                    b.Property<int>("ItemConditionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OnverId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ItemConditionId");

                    b.HasIndex("UserId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.ItemCondition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConditionCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ItemConditions");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.ItemImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("ItemImages");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.JwtToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Blacklisted")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("JwtTokens");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.Trade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("IssuedDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("PublicIdentifier")
                        .HasColumnType("TEXT");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SenderId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TradeStatus")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("UserId");

                    b.ToTable("Trades");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.TradeItem", b =>
                {
                    b.Property<int>("TradeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TradeId", "UserId", "ItemId");

                    b.HasIndex("ItemId");

                    b.HasIndex("UserId");

                    b.ToTable("TradeItems");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .HasColumnType("TEXT");

                    b.Property<string>("HashedPassword")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProfileImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("PublicIdentifier")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.Item", b =>
                {
                    b.HasOne("JustTradeIt.Software.API.Models.Entities.ItemCondition", null)
                        .WithMany("Items")
                        .HasForeignKey("ItemConditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JustTradeIt.Software.API.Models.Entities.User", null)
                        .WithMany("Items")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.ItemImage", b =>
                {
                    b.HasOne("JustTradeIt.Software.API.Models.Entities.Item", null)
                        .WithMany("ItemImages")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.Trade", b =>
                {
                    b.HasOne("JustTradeIt.Software.API.Models.Entities.Item", null)
                        .WithMany("Trade")
                        .HasForeignKey("ItemId");

                    b.HasOne("JustTradeIt.Software.API.Models.Entities.User", null)
                        .WithMany("Trades")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.TradeItem", b =>
                {
                    b.HasOne("JustTradeIt.Software.API.Models.Entities.Item", null)
                        .WithMany("TradeItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JustTradeIt.Software.API.Models.Entities.Trade", null)
                        .WithMany("TradeItems")
                        .HasForeignKey("TradeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JustTradeIt.Software.API.Models.Entities.User", null)
                        .WithMany("TradeItems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.Item", b =>
                {
                    b.Navigation("ItemImages");

                    b.Navigation("Trade");

                    b.Navigation("TradeItems");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.ItemCondition", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.Trade", b =>
                {
                    b.Navigation("TradeItems");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.User", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("TradeItems");

                    b.Navigation("Trades");
                });
#pragma warning restore 612, 618
        }
    }
}
