﻿// <auto-generated />
using System;
using ControleFacil.Api.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ControleFacil.Api.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ControleFacil.Api.Domain.Models.Apagar", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("timestamp");

                    b.Property<DateTime?>("DataInativacao")
                        .HasColumnType("timestamp");

                    b.Property<DateTime?>("DataPagamento")
                        .HasColumnType("timestamp");

                    b.Property<DateTime?>("DataReferencia")
                        .HasColumnType("timestamp");

                    b.Property<DateTime>("DataVencimento")
                        .HasColumnType("timestamp");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<long>("IdNaturezaDeLancamento")
                        .HasColumnType("bigint");

                    b.Property<string>("Observacao")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<double>("ValorOriginal")
                        .HasColumnType("double precision");

                    b.Property<double>("ValorPago")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("IdNaturezaDeLancamento");

                    b.ToTable("apagar", (string)null);
                });

            modelBuilder.Entity("ControleFacil.Api.Domain.Models.Areceber", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("timestamp");

                    b.Property<DateTime?>("DataInativacao")
                        .HasColumnType("timestamp");

                    b.Property<DateTime?>("DataRecebimento")
                        .HasColumnType("timestamp");

                    b.Property<DateTime?>("DataReferencia")
                        .HasColumnType("timestamp");

                    b.Property<DateTime>("DataVencimento")
                        .HasColumnType("timestamp");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<long>("IdNaturezaDeLancamento")
                        .HasColumnType("bigint");

                    b.Property<string>("Observacao")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<double>("ValorOriginal")
                        .HasColumnType("double precision");

                    b.Property<double>("ValorRecebido")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("IdNaturezaDeLancamento");

                    b.ToTable("areceber", (string)null);
                });

            modelBuilder.Entity("ControleFacil.Api.Domain.Models.NaturezaDeLancamento", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("timestamp");

                    b.Property<DateTime?>("DataInativacao")
                        .HasColumnType("timestamp");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<long>("IdUsuario")
                        .HasColumnType("bigint");

                    b.Property<string>("Observacao")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.HasIndex("IdUsuario");

                    b.ToTable("naturezadelancamento", (string)null);
                });

            modelBuilder.Entity("ControleFacil.Api.Domain.Models.Usuario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("timestamp");

                    b.Property<DateTime?>("DataInativacao")
                        .HasColumnType("timestamp");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.ToTable("usuario", (string)null);
                });

            modelBuilder.Entity("ControleFacil.Api.Domain.Models.Apagar", b =>
                {
                    b.HasOne("ControleFacil.Api.Domain.Models.NaturezaDeLancamento", "NaturezaDeLancamento")
                        .WithMany()
                        .HasForeignKey("IdNaturezaDeLancamento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NaturezaDeLancamento");
                });

            modelBuilder.Entity("ControleFacil.Api.Domain.Models.Areceber", b =>
                {
                    b.HasOne("ControleFacil.Api.Domain.Models.NaturezaDeLancamento", "NaturezaDeLancamento")
                        .WithMany()
                        .HasForeignKey("IdNaturezaDeLancamento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NaturezaDeLancamento");
                });

            modelBuilder.Entity("ControleFacil.Api.Domain.Models.NaturezaDeLancamento", b =>
                {
                    b.HasOne("ControleFacil.Api.Domain.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
