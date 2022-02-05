﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyFlashCards.Infrastructure.Persistence;

#nullable disable

namespace MyFlashCards.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(FlashCardsContext))]
    [Migration("20220202213714_QuestionTestPropertyAdded")]
    partial class QuestionTestPropertyAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MyFlashCards.Application.Entities.Card", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Back")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Front")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("MyFlashCards.Application.Entities.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("Correct")
                        .HasColumnType("bit");

                    b.Property<Guid>("TestId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.HasIndex("TestId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("MyFlashCards.Application.Entities.Test", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Prompt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("MyFlashCards.Application.Entities.Question", b =>
                {
                    b.HasOne("MyFlashCards.Application.Entities.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyFlashCards.Application.Entities.Test", "Test")
                        .WithMany("Questions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("Test");
                });

            modelBuilder.Entity("MyFlashCards.Application.Entities.Test", b =>
                {
                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}