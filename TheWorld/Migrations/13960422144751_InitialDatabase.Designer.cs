using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TheWorld.models;

namespace TheWorld.Migrations
{
    [DbContext(typeof(WorldContext))]
    [Migration("13960422144751_InitialDatabase")]
    partial class InitialDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TheWorld.models.Stop", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Tripid");

                    b.Property<DateTime>("arraival");

                    b.Property<double>("latitude");

                    b.Property<double>("longtitude");

                    b.Property<string>("name");

                    b.Property<int>("order");

                    b.HasKey("id");

                    b.HasIndex("Tripid");

                    b.ToTable("stops");
                });

            modelBuilder.Entity("TheWorld.models.Trip", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("dataCreated");

                    b.Property<string>("name");

                    b.Property<string>("username");

                    b.HasKey("id");

                    b.ToTable("trips");
                });

            modelBuilder.Entity("TheWorld.models.Stop", b =>
                {
                    b.HasOne("TheWorld.models.Trip")
                        .WithMany("stops")
                        .HasForeignKey("Tripid");
                });
        }
    }
}
