﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Neuromorfismo.BackEnd.Model;

#nullable disable

namespace Neuromorfismo.BackEnd.Model.Migrations
{
    [DbContext(typeof(NeuromorfismoContext))]
    [Migration("20240207013543_TriggersIniciales")]
    partial class TriggersIniciales
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Neuromorfismo.BackEnd.Model.EpilepsiaModel", b =>
                {
                    b.Property<int>("IdEpilepsia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("idEpilepsia");

                    b.Property<DateTime>("FechaCreac")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaUltMod")
                        .IsConcurrencyToken()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombre")
                        .HasDefaultValueSql("''");

                    b.HasKey("IdEpilepsia")
                        .HasName("PRIMARY");

                    b.ToTable("Epilepsias");

                    b.HasData(
                        new
                        {
                            IdEpilepsia = 1,
                            FechaCreac = new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            FechaUltMod = new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            Nombre = "Epilepsia1"
                        },
                        new
                        {
                            IdEpilepsia = 2,
                            FechaCreac = new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            FechaUltMod = new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            Nombre = "Epilepsia2"
                        });
                });

            modelBuilder.Entity("Neuromorfismo.BackEnd.Model.EtapaLTModel", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<DateTime>("FechaCreac")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaUltMod")
                        .IsConcurrencyToken()
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("IdMedicoCreador")
                        .HasColumnType("int(11)");

                    b.Property<int?>("IdMedicoUltModif")
                        .HasColumnType("int(11)");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("IdMedicoCreador");

                    b.HasIndex("IdMedicoUltModif");

                    b.ToTable("EtapaLT");

                    b.HasData(
                        new
                        {
                            Id = (short)1,
                            Descripcion = "",
                            FechaCreac = new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            FechaUltMod = new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            Label = "¿Ha dado su consentimiento el paciente?",
                            Titulo = "Consentimiento Informado"
                        },
                        new
                        {
                            Id = (short)2,
                            Descripcion = "Descripcion",
                            FechaCreac = new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            FechaUltMod = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Label = "¿Ha dado su consentimiento el paciente?",
                            Titulo = "Paciente Acude a Extracción Analítica"
                        },
                        new
                        {
                            Id = (short)3,
                            Descripcion = "",
                            FechaCreac = new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            FechaUltMod = new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            Label = "¿Ha dado su consentimiento el paciente?",
                            Titulo = "Muestra de Genética"
                        },
                        new
                        {
                            Id = (short)999,
                            Descripcion = "",
                            FechaCreac = new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            FechaUltMod = new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            Label = "",
                            Titulo = "Fin de la evolución del paciente"
                        });
                });

            modelBuilder.Entity("Neuromorfismo.BackEnd.Model.EvolucionLTModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Confirmado")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime(6)");

                    b.Property<short>("IdEtapa")
                        .HasColumnType("smallint");

                    b.Property<int>("IdMedicoUltModif")
                        .HasColumnType("int(11)");

                    b.Property<int>("IdPaciente")
                        .HasColumnType("int(11)");

                    b.HasKey("Id");

                    b.HasIndex("IdEtapa");

                    b.HasIndex("IdMedicoUltModif");

                    b.HasIndex("IdPaciente");

                    b.ToTable("EvolucionLT");
                });

            modelBuilder.Entity("Neuromorfismo.BackEnd.Model.FarmacosModel", b =>
                {
                    b.Property<int>("IdFarmaco")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("idFarmaco");

                    b.Property<DateTime>("FechaCreac")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaUltMod")
                        .IsConcurrencyToken()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombre")
                        .HasDefaultValueSql("''");

                    b.HasKey("IdFarmaco")
                        .HasName("PRIMARY");

                    b.ToTable("Farmacos");
                });

            modelBuilder.Entity("Neuromorfismo.BackEnd.Model.MedicosModel", b =>
                {
                    b.Property<int>("IdMedico")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("idMedico");

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("apellidos");

                    b.Property<DateTime>("FechaCreac")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaNac")
                        .HasColumnType("datetime")
                        .HasColumnName("fechaNac");

                    b.Property<DateTime>("FechaUltMod")
                        .IsConcurrencyToken()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NetuserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("netuserId")
                        .UseCollation("utf8mb4_general_ci");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombre");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1)
                        .HasColumnType("varchar(1)")
                        .HasColumnName("sexo")
                        .HasDefaultValueSql("''");

                    b.Property<string>("UserLogin")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("userLogin");

                    b.HasKey("IdMedico")
                        .HasName("PRIMARY");

                    b.HasIndex("NetuserId")
                        .IsUnique();

                    b.HasIndex(new[] { "UserLogin" }, "userLogin")
                        .IsUnique();

                    b.HasIndex(new[] { "NetuserId" }, "Índice 2");

                    b.ToTable("Medicos");
                });

            modelBuilder.Entity("Neuromorfismo.BackEnd.Model.MedicospacienteModel", b =>
                {
                    b.Property<int>("IdMedPac")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("idMedPac");

                    b.Property<int>("IdMedico")
                        .HasColumnType("int(11)")
                        .HasColumnName("idMedico");

                    b.Property<int>("IdPaciente")
                        .HasColumnType("int(11)")
                        .HasColumnName("idPaciente");

                    b.HasKey("IdMedPac")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "IdPaciente" }, "FK_medicospacientes_pacientes");

                    b.HasIndex(new[] { "IdMedico", "IdPaciente" }, "idUsuario_idPaciente");

                    b.ToTable("medicospacientes", null, t =>
                        {
                            t.HasComment("Relacion de que medicos pueden editar que pacientes");
                        });
                });

            modelBuilder.Entity("Neuromorfismo.BackEnd.Model.MutacionesModel", b =>
                {
                    b.Property<int>("IdMutacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("idMutacion");

                    b.Property<DateTime>("FechaCreac")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaUltMod")
                        .IsConcurrencyToken()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombre")
                        .HasDefaultValueSql("''");

                    b.HasKey("IdMutacion")
                        .HasName("PRIMARY");

                    b.ToTable("Mutaciones");

                    b.HasData(
                        new
                        {
                            IdMutacion = 1,
                            FechaCreac = new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            FechaUltMod = new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            Nombre = "Mutacion1"
                        },
                        new
                        {
                            IdMutacion = 2,
                            FechaCreac = new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            FechaUltMod = new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            Nombre = "Mutacion2"
                        });
                });

            modelBuilder.Entity("Neuromorfismo.BackEnd.Model.PacientesModel", b =>
                {
                    b.Property<int>("IdPaciente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("idPaciente");

                    b.Property<string>("DescripEnferRaras")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasColumnName("descripEnferRaras")
                        .HasDefaultValueSql("''");

                    b.Property<string>("EnfermRaras")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1)
                        .HasColumnType("varchar(1)")
                        .HasColumnName("enfermRaras")
                        .HasDefaultValueSql("''");

                    b.Property<string>("Farmaco")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)")
                        .HasColumnName("farmaco");

                    b.Property<DateTime>("FechaCreac")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaDiagnostico")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("fechaDiagnostico")
                        .HasDefaultValueSql("curdate()");

                    b.Property<DateTime>("FechaFractalidad")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("fechaFractalidad")
                        .HasDefaultValueSql("curdate()");

                    b.Property<DateTime>("FechaNac")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("fechaNac")
                        .HasDefaultValueSql("curdate()");

                    b.Property<DateTime>("FechaUltMod")
                        .IsConcurrencyToken()
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("IdEpilepsia")
                        .HasColumnType("int(11)")
                        .HasColumnName("idEpilepsia");

                    b.Property<int?>("IdMutacion")
                        .HasColumnType("int(11)")
                        .HasColumnName("idMutacion");

                    b.Property<int>("MedicoCreador")
                        .HasColumnType("int(11)")
                        .HasColumnName("medicoCreador");

                    b.Property<int>("MedicoUltMod")
                        .HasColumnType("int(11)")
                        .HasColumnName("medicoUltMod");

                    b.Property<string>("NumHistoria")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("enum('H','M')")
                        .HasColumnName("sexo")
                        .HasDefaultValueSql("'H'");

                    b.Property<int>("Talla")
                        .HasPrecision(20, 6)
                        .HasColumnType("int")
                        .HasColumnName("talla");

                    b.HasKey("IdPaciente")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "IdMutacion" }, "idMutacion");

                    b.HasIndex(new[] { "IdEpilepsia" }, "idTipoEpilepsia");

                    b.HasIndex(new[] { "MedicoCreador" }, "medicoCreador");

                    b.HasIndex(new[] { "MedicoUltMod" }, "medicoUltMod");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("Neuromorfismo.BackEnd.Model.RoleModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            ConcurrencyStamp = "5829dbde-63aa-436c-9198-9f36997a73fe",
                            Name = "superAdmin",
                            NormalizedName = "SUPERADMIN"
                        },
                        new
                        {
                            Id = "2",
                            ConcurrencyStamp = "7c546c12-ab44-4f7b-af40-67cce46799e1",
                            Name = "admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "3",
                            ConcurrencyStamp = "fdb141e2-9579-44c1-9c29-c613242f061d",
                            Name = "medico",
                            NormalizedName = "MEDICO"
                        });
                });

            modelBuilder.Entity("Neuromorfismo.BackEnd.Model.UserModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Neuromorfismo.BackEnd.Model.UserRefreshTokens", b =>
                {
                    b.Property<int>("RefreshTokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaExpiracion")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("IdMedico")
                        .HasColumnType("int(11)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RefreshTokenId");

                    b.HasIndex("IdMedico")
                        .IsUnique();

                    b.ToTable("UserRefreshToken");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Neuromorfismo.BackEnd.Model.RoleModel", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Neuromorfismo.BackEnd.Model.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Neuromorfismo.BackEnd.Model.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Neuromorfismo.BackEnd.Model.RoleModel", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Neuromorfismo.BackEnd.Model.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Neuromorfismo.BackEnd.Model.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Neuromorfismo.BackEnd.Model.EtapaLTModel", b =>
                {
                    b.HasOne("Neuromorfismo.BackEnd.Model.MedicosModel", "MedicoCreador")
                        .WithMany("EtapaMedicoCreador")
                        .HasForeignKey("IdMedicoCreador");

                    b.HasOne("Neuromorfismo.BackEnd.Model.MedicosModel", "MedicoUltModif")
                        .WithMany("EtapaMedicoUltModif")
                        .HasForeignKey("IdMedicoUltModif");

                    b.Navigation("MedicoCreador");

                    b.Navigation("MedicoUltModif");
                });

            modelBuilder.Entity("Neuromorfismo.BackEnd.Model.EvolucionLTModel", b =>
                {
                    b.HasOne("Neuromorfismo.BackEnd.Model.EtapaLTModel", "Etapa")
                        .WithMany("EvolucionesEtapa")
                        .HasForeignKey("IdEtapa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Neuromorfismo.BackEnd.Model.MedicosModel", "MedicoUltModif")
                        .WithMany("EvolucionMedicoUltModif")
                        .HasForeignKey("IdMedicoUltModif")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Neuromorfismo.BackEnd.Model.PacientesModel", "Paciente")
                        .WithMany("Evoluciones")
                        .HasForeignKey("IdPaciente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Etapa");

                    b.Navigation("MedicoUltModif");

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("Neuromorfismo.BackEnd.Model.MedicosModel", b =>
                {
                    b.HasOne("Neuromorfismo.BackEnd.Model.UserModel", "Netuser")
                        .WithOne("Medico")
                        .HasForeignKey("Neuromorfismo.BackEnd.Model.MedicosModel", "NetuserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_medicos_aspnetusers");

                    b.Navigation("Netuser");
                });

            modelBuilder.Entity("Neuromorfismo.BackEnd.Model.MedicospacienteModel", b =>
                {
                    b.HasOne("Neuromorfismo.BackEnd.Model.MedicosModel", "IdMedicoNavigation")
                        .WithMany("Medicospacientes")
                        .HasForeignKey("IdMedico")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_medicospacientes_medicos");

                    b.HasOne("Neuromorfismo.BackEnd.Model.PacientesModel", "IdPacienteNavigation")
                        .WithMany("Medicospacientes")
                        .HasForeignKey("IdPaciente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_medicospacientes_pacientes");

                    b.Navigation("IdMedicoNavigation");

                    b.Navigation("IdPacienteNavigation");
                });

            modelBuilder.Entity("Neuromorfismo.BackEnd.Model.PacientesModel", b =>
                {
                    b.HasOne("Neuromorfismo.BackEnd.Model.EpilepsiaModel", "IdEpilepsiaNavigation")
                        .WithMany("Pacientes")
                        .HasForeignKey("IdEpilepsia")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_pacientes_epilepsias");

                    b.HasOne("Neuromorfismo.BackEnd.Model.MutacionesModel", "IdMutacionNavigation")
                        .WithMany("Pacientes")
                        .HasForeignKey("IdMutacion")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_pacientes_mutaciones");

                    b.HasOne("Neuromorfismo.BackEnd.Model.MedicosModel", "MedicoCreadorNavigation")
                        .WithMany("PacienteMedicoCreadorNavigations")
                        .HasForeignKey("MedicoCreador")
                        .IsRequired()
                        .HasConstraintName("FK_pacientes_medicos_2");

                    b.HasOne("Neuromorfismo.BackEnd.Model.MedicosModel", "MedicoUltModNavigation")
                        .WithMany("PacienteMedicoUltModNavigations")
                        .HasForeignKey("MedicoUltMod")
                        .IsRequired()
                        .HasConstraintName("FK_pacientes_medicos");

                    b.Navigation("IdEpilepsiaNavigation");

                    b.Navigation("IdMutacionNavigation");

                    b.Navigation("MedicoCreadorNavigation");

                    b.Navigation("MedicoUltModNavigation");
                });

            modelBuilder.Entity("Neuromorfismo.BackEnd.Model.UserRefreshTokens", b =>
                {
                    b.HasOne("Neuromorfismo.BackEnd.Model.MedicosModel", "Medico")
                        .WithOne("RefreshToken")
                        .HasForeignKey("Neuromorfismo.BackEnd.Model.UserRefreshTokens", "IdMedico")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medico");
                });

            modelBuilder.Entity("Neuromorfismo.BackEnd.Model.EpilepsiaModel", b =>
                {
                    b.Navigation("Pacientes");
                });

            modelBuilder.Entity("Neuromorfismo.BackEnd.Model.EtapaLTModel", b =>
                {
                    b.Navigation("EvolucionesEtapa");
                });

            modelBuilder.Entity("Neuromorfismo.BackEnd.Model.MedicosModel", b =>
                {
                    b.Navigation("EtapaMedicoCreador");

                    b.Navigation("EtapaMedicoUltModif");

                    b.Navigation("EvolucionMedicoUltModif");

                    b.Navigation("Medicospacientes");

                    b.Navigation("PacienteMedicoCreadorNavigations");

                    b.Navigation("PacienteMedicoUltModNavigations");

                    b.Navigation("RefreshToken");
                });

            modelBuilder.Entity("Neuromorfismo.BackEnd.Model.MutacionesModel", b =>
                {
                    b.Navigation("Pacientes");
                });

            modelBuilder.Entity("Neuromorfismo.BackEnd.Model.PacientesModel", b =>
                {
                    b.Navigation("Evoluciones");

                    b.Navigation("Medicospacientes");
                });

            modelBuilder.Entity("Neuromorfismo.BackEnd.Model.UserModel", b =>
                {
                    b.Navigation("Medico")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
