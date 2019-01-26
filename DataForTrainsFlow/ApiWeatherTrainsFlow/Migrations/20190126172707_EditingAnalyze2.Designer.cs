﻿// <auto-generated />
using System;
using ApiWeatherTrainsFlow.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApiWeatherTrainsFlow.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20190126172707_EditingAnalyze2")]
    partial class EditingAnalyze2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LibraryClass.Poco.Analyze", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Delay");

                    b.Property<string>("StationArrival");

                    b.Property<string>("StationDepart");

                    b.Property<int>("Time");

                    b.Property<string>("Vehicle");

                    b.Property<int?>("WeatherId");

                    b.HasKey("Id");

                    b.HasIndex("WeatherId");

                    b.ToTable("Analyzes");
                });

            modelBuilder.Entity("LibraryClass.Poco.Weather", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTime");

                    b.Property<bool>("HasPrecipitation");

                    b.Property<string>("PrecipitationType");

                    b.Property<int>("RelativeHumidity");

                    b.Property<double>("Temperature");

                    b.Property<string>("WeatherText");

                    b.HasKey("Id");

                    b.ToTable("Weathers");
                });

            modelBuilder.Entity("LibraryClass.Poco.Analyze", b =>
                {
                    b.HasOne("LibraryClass.Poco.Weather", "Weather")
                        .WithMany()
                        .HasForeignKey("WeatherId");
                });
#pragma warning restore 612, 618
        }
    }
}
