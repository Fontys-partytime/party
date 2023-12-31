﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Partytime.Party.Service.Entities;

#nullable disable

namespace Partytime.Party.Service.Migrations
{
    [DbContext(typeof(PartyContext))]
    [Migration("20230526233354_RelationalFix")]
    partial class RelationalFix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Partytime.Party.Service.Entities.Joined", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool?>("Accepted")
                        .HasColumnType("boolean")
                        .HasColumnName("accepted");

                    b.Property<Guid>("Partyid")
                        .HasColumnType("uuid")
                        .HasColumnName("partyid");

                    b.Property<Guid>("Userid")
                        .HasColumnType("uuid")
                        .HasColumnName("userid");

                    b.Property<string>("Username")
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_joined");

                    b.HasIndex("Partyid")
                        .HasDatabaseName("ix_joined_partyid");

                    b.ToTable("joined", (string)null);
                });

            modelBuilder.Entity("Partytime.Party.Service.Entities.Party", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<decimal?>("Budget")
                        .HasColumnType("numeric")
                        .HasColumnName("budget");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTimeOffset>("Ends")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("ends");

                    b.Property<string>("Location")
                        .HasColumnType("text")
                        .HasColumnName("location");

                    b.Property<DateTimeOffset>("Starts")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("starts");

                    b.Property<string>("Title")
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<Guid>("Userid")
                        .HasColumnType("uuid")
                        .HasColumnName("userid");

                    b.HasKey("Id")
                        .HasName("pk_party");

                    b.ToTable("party", (string)null);
                });

            modelBuilder.Entity("Partytime.Party.Service.Entities.Joined", b =>
                {
                    b.HasOne("Partytime.Party.Service.Entities.Party", "Party")
                        .WithMany("Joined")
                        .HasForeignKey("Partyid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_joined_party_partyid");

                    b.Navigation("Party");
                });

            modelBuilder.Entity("Partytime.Party.Service.Entities.Party", b =>
                {
                    b.Navigation("Joined");
                });
#pragma warning restore 612, 618
        }
    }
}
