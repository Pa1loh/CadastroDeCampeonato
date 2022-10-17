﻿// <auto-generated />
using System;
using CadastroDeCampeonato.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CadastroDeCampeonato.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20221017055355_FkDoCampeonato")]
    partial class FkDoCampeonato
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("CadastroDeCampeonato.Models.Campeonato", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("fim")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("inicio")
                        .HasColumnType("TEXT");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("campeonato");
                });

            modelBuilder.Entity("CadastroDeCampeonato.Models.CampeonatoTime", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("campeonatoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("timeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("campeonatoId");

                    b.HasIndex("timeId");

                    b.ToTable("campeonatoTime");
                });

            modelBuilder.Entity("CadastroDeCampeonato.Models.Partida", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("campeonatoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("fasePartida")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("campeonatoId");

                    b.ToTable("partida");
                });

            modelBuilder.Entity("CadastroDeCampeonato.Models.PartidaTime", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CampeonatoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("golsFeitos")
                        .HasColumnType("INTEGER");

                    b.Property<int>("partidaId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("penaltisFeitos")
                        .HasColumnType("INTEGER");

                    b.Property<int>("timeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("CampeonatoId");

                    b.HasIndex("partidaId");

                    b.HasIndex("timeId");

                    b.ToTable("partidaTime");
                });

            modelBuilder.Entity("CadastroDeCampeonato.Models.Time", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("time");
                });

            modelBuilder.Entity("CadastroDeCampeonato.Models.CampeonatoTime", b =>
                {
                    b.HasOne("CadastroDeCampeonato.Models.Campeonato", "campeonato")
                        .WithMany()
                        .HasForeignKey("campeonatoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CadastroDeCampeonato.Models.Time", "time")
                        .WithMany()
                        .HasForeignKey("timeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("campeonato");

                    b.Navigation("time");
                });

            modelBuilder.Entity("CadastroDeCampeonato.Models.Partida", b =>
                {
                    b.HasOne("CadastroDeCampeonato.Models.Campeonato", "campeonato")
                        .WithMany()
                        .HasForeignKey("campeonatoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("campeonato");
                });

            modelBuilder.Entity("CadastroDeCampeonato.Models.PartidaTime", b =>
                {
                    b.HasOne("CadastroDeCampeonato.Models.Campeonato", "campeonato")
                        .WithMany()
                        .HasForeignKey("CampeonatoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CadastroDeCampeonato.Models.Partida", "Partida")
                        .WithMany()
                        .HasForeignKey("partidaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CadastroDeCampeonato.Models.Time", "time")
                        .WithMany()
                        .HasForeignKey("timeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Partida");

                    b.Navigation("campeonato");

                    b.Navigation("time");
                });
#pragma warning restore 612, 618
        }
    }
}
