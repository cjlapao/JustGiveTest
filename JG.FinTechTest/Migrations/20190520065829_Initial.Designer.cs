﻿// <auto-generated />
using System;
using JG.FinTechTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JG.FinTechTest.Migrations
{
    [DbContext(typeof(JGFinTechTestContext))]
    [Migration("20190520065829_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JG.FinTechTest.Persistence.GiftAidDeclaration", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("PostCode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("GiftAidDeclaration");
                });
#pragma warning restore 612, 618
        }
    }
}
