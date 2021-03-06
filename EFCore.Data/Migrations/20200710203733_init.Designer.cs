﻿// <auto-generated />
using System;
using EFCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCore.Data.Migrations
{
    [DbContext(typeof(SamuriaContext))]
    [Migration("20200710203733_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EFCore.Domain.Battle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Battles");
                });

            modelBuilder.Entity("EFCore.Domain.Clan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClanNamr")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clans");
                });

            modelBuilder.Entity("EFCore.Domain.Horse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SamuriaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Horses");
                });

            modelBuilder.Entity("EFCore.Domain.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SamuraiID")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SamuraiID");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("EFCore.Domain.Samurai", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClanId")
                        .HasColumnType("int");

                    b.Property<int?>("HorseefcoreId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClanId");

                    b.HasIndex("HorseefcoreId");

                    b.ToTable("Samurais");
                });

            modelBuilder.Entity("EFCore.Domain.SamuraiBattle", b =>
                {
                    b.Property<int>("BattleID")
                        .HasColumnType("int");

                    b.Property<int>("SamuriaID")
                        .HasColumnType("int");

                    b.HasKey("BattleID", "SamuriaID");

                    b.HasIndex("SamuriaID");

                    b.ToTable("SamuraiBattle");
                });

            modelBuilder.Entity("EFCore.Domain.Quote", b =>
                {
                    b.HasOne("EFCore.Domain.Samurai", "Samurai")
                        .WithMany("Quotes")
                        .HasForeignKey("SamuraiID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EFCore.Domain.Samurai", b =>
                {
                    b.HasOne("EFCore.Domain.Clan", "Clan")
                        .WithMany()
                        .HasForeignKey("ClanId");

                    b.HasOne("EFCore.Domain.Horse", "Horseefcore")
                        .WithMany()
                        .HasForeignKey("HorseefcoreId");
                });

            modelBuilder.Entity("EFCore.Domain.SamuraiBattle", b =>
                {
                    b.HasOne("EFCore.Domain.Battle", "Battle")
                        .WithMany("SamuraiBattles")
                        .HasForeignKey("BattleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EFCore.Domain.Samurai", "Samuria")
                        .WithMany("SamuraiBattles")
                        .HasForeignKey("SamuriaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
