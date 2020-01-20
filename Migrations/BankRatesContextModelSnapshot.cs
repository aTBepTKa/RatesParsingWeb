﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RatesParsingWeb.Storage;

namespace RatesParsingWeb.Migrations
{
    [DbContext(typeof(BankRatesContext))]
    partial class BankRatesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RatesParsingWeb.Domain.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BankUrl")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<int>("CurrencyID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("RatesUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.HasKey("Id");

                    b.HasIndex("CurrencyID");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("RatesParsingWeb.Domain.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("NumCode")
                        .HasColumnType("int");

                    b.Property<string>("TextCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("RatesParsingWeb.Domain.ExchangeRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CurrencyID")
                        .HasColumnType("int");

                    b.Property<int>("ExchangeRateListId")
                        .HasColumnType("int");

                    b.Property<decimal>("ExchangeRateValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyID");

                    b.HasIndex("ExchangeRateListId");

                    b.ToTable("ExchangeRates");
                });

            modelBuilder.Entity("RatesParsingWeb.Domain.ExchangeRateList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BankId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.ToTable("ExchangeRateLists");
                });

            modelBuilder.Entity("RatesParsingWeb.Domain.ParsingSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BankId")
                        .HasColumnType("int");

                    b.Property<int?>("EndXpathRow")
                        .HasColumnType("int");

                    b.Property<string>("ExchangeRateXpath")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<string>("NumberDecimalSeparator")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("NumberGroupSeparator")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<int?>("StartXpathRow")
                        .HasColumnType("int");

                    b.Property<string>("TextCodeXpath")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<string>("UnitXpath")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<string>("VariablePartOfXpath")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("BankId")
                        .IsUnique();

                    b.ToTable("ParsingSettings");
                });

            modelBuilder.Entity("RatesParsingWeb.Domain.Script", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("ParamsNum")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Scripts");
                });

            modelBuilder.Entity("RatesParsingWeb.Domain.TextCodeScriptAssignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BankId")
                        .HasColumnType("int");

                    b.Property<int?>("ParsingSettingsId")
                        .HasColumnType("int");

                    b.Property<int>("ScriptId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.HasIndex("ParsingSettingsId");

                    b.HasIndex("ScriptId");

                    b.ToTable("TextCodeScriptAssignments");
                });

            modelBuilder.Entity("RatesParsingWeb.Domain.TextCodeScriptParameter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("TextCodeScriptAssignmentId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("TextCodeScriptAssignmentId");

                    b.ToTable("TextCodeScriptParameters");
                });

            modelBuilder.Entity("RatesParsingWeb.Domain.UnitScriptAssignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BankId")
                        .HasColumnType("int");

                    b.Property<int?>("ParsingSettingsId")
                        .HasColumnType("int");

                    b.Property<int>("ScriptId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.HasIndex("ParsingSettingsId");

                    b.HasIndex("ScriptId");

                    b.ToTable("UnitScriptAssignments");
                });

            modelBuilder.Entity("RatesParsingWeb.Domain.UnitScriptParameter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UnitScriptAssignmentId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("UnitScriptAssignmentId");

                    b.ToTable("UnitScriptParameters");
                });

            modelBuilder.Entity("RatesParsingWeb.Domain.Bank", b =>
                {
                    b.HasOne("RatesParsingWeb.Domain.Currency", "Currency")
                        .WithMany("Banks")
                        .HasForeignKey("CurrencyID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("RatesParsingWeb.Domain.ExchangeRate", b =>
                {
                    b.HasOne("RatesParsingWeb.Domain.Currency", "Currency")
                        .WithMany("ExchangeRates")
                        .HasForeignKey("CurrencyID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RatesParsingWeb.Domain.ExchangeRateList", "ExchangeRateList")
                        .WithMany("ExchangeRates")
                        .HasForeignKey("ExchangeRateListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RatesParsingWeb.Domain.ExchangeRateList", b =>
                {
                    b.HasOne("RatesParsingWeb.Domain.Bank", "Bank")
                        .WithMany("ExchangeRateLists")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RatesParsingWeb.Domain.ParsingSettings", b =>
                {
                    b.HasOne("RatesParsingWeb.Domain.Bank", "Bank")
                        .WithOne("ParsingSettings")
                        .HasForeignKey("RatesParsingWeb.Domain.ParsingSettings", "BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RatesParsingWeb.Domain.TextCodeScriptAssignment", b =>
                {
                    b.HasOne("RatesParsingWeb.Domain.Bank", "Bank")
                        .WithMany()
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RatesParsingWeb.Domain.ParsingSettings", null)
                        .WithMany("TextCodeScripts")
                        .HasForeignKey("ParsingSettingsId");

                    b.HasOne("RatesParsingWeb.Domain.Script", "Script")
                        .WithMany()
                        .HasForeignKey("ScriptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RatesParsingWeb.Domain.TextCodeScriptParameter", b =>
                {
                    b.HasOne("RatesParsingWeb.Domain.TextCodeScriptAssignment", "TextCodeScriptAssignment")
                        .WithMany("ScriptParameters")
                        .HasForeignKey("TextCodeScriptAssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RatesParsingWeb.Domain.UnitScriptAssignment", b =>
                {
                    b.HasOne("RatesParsingWeb.Domain.Bank", "Bank")
                        .WithMany()
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RatesParsingWeb.Domain.ParsingSettings", null)
                        .WithMany("UnitScripts")
                        .HasForeignKey("ParsingSettingsId");

                    b.HasOne("RatesParsingWeb.Domain.Script", "Script")
                        .WithMany("ScriptAssignments")
                        .HasForeignKey("ScriptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RatesParsingWeb.Domain.UnitScriptParameter", b =>
                {
                    b.HasOne("RatesParsingWeb.Domain.UnitScriptAssignment", "UnitScriptAssignment")
                        .WithMany("ScriptParameters")
                        .HasForeignKey("UnitScriptAssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
