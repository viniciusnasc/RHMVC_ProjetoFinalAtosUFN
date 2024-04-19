﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RH.Data.Contexto;

#nullable disable

namespace RH.Data.Migrations
{
    [DbContext(typeof(RhContext))]
    partial class RhContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RH.Domain.Entities.ContaBancaria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Agencia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Banco")
                        .HasColumnType("int");

                    b.Property<string>("ContaCorrente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ContaBancaria");

                    b.HasData(
                        new
                        {
                            Id = new Guid("07a49006-1a80-4784-9de9-f535050e1aad"),
                            Agencia = "1212-2",
                            Banco = 33,
                            ContaCorrente = "19191-8",
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(2280)
                        });
                });

            modelBuilder.Entity("RH.Domain.Entities.DecimoTerceiro", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataPagamento")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FuncionarioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("FuncionarioId");

                    b.ToTable("DecimoTerceiro");
                });

            modelBuilder.Entity("RH.Domain.Entities.Demissao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataPagamento")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FuncionarioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<double>("ValorDecimo")
                        .HasColumnType("float");

                    b.Property<double>("ValorFerias")
                        .HasColumnType("float");

                    b.Property<double>("ValorMes")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("FuncionarioId")
                        .IsUnique();

                    b.ToTable("Demissao");
                });

            modelBuilder.Entity("RH.Domain.Entities.Departamento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("NomeDepartamento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubDepartamento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Departamentos");

                    b.HasData(
                        new
                        {
                            Id = new Guid("fc73fc67-5237-4830-8f8c-425766ef4d6a"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(2186),
                            NomeDepartamento = "PRESIDENCIA",
                            SubDepartamento = "ADMINISTRACAO"
                        });
                });

            modelBuilder.Entity("RH.Domain.Entities.Endereco", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cep")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Complemento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("MunicipioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<string>("Rua")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MunicipioId");

                    b.ToTable("Enderecos", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("fc73fc67-5137-4830-8f8c-425766ef4d6a"),
                            Cep = "03579240",
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(2088),
                            MunicipioId = new Guid("f0a1a069-3db3-4f31-b71d-20074e3b861b"),
                            Numero = 1934,
                            Rua = "Rua Machado de Castro"
                        });
                });

            modelBuilder.Entity("RH.Domain.Entities.Ferias", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataPagamento")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FuncionarioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("FuncionarioId");

                    b.ToTable("Ferias");
                });

            modelBuilder.Entity("RH.Domain.Entities.Funcao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("NomeFuncao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Salario")
                        .HasColumnType("float");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Funcoes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("07b49006-1b80-4784-9de9-f535050e1aad"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(2233),
                            NomeFuncao = "ADMINISTRADOR",
                            Salario = 5000.0
                        });
                });

            modelBuilder.Entity("RH.Domain.Entities.Funcionario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Admissao")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("CPF")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ContaBancariaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataDemissao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DemissaoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DepartamentoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("EnderecoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FotoPerfil")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FuncaoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeSocial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RG")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Registro")
                        .HasColumnType("int");

                    b.Property<int>("Sexo")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ContaBancariaId");

                    b.HasIndex("DepartamentoId");

                    b.HasIndex("EnderecoId");

                    b.HasIndex("FuncaoId");

                    b.HasIndex("Registro");

                    b.ToTable("Funcionarios", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("e5b9f7f7-4f21-488f-b0e4-5290580486fa"),
                            Admissao = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(2495),
                            Ativo = true,
                            CPF = "44444444444",
                            ContaBancariaId = new Guid("07a49006-1a80-4784-9de9-f535050e1aad"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(2493),
                            DataNascimento = new DateTime(1994, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartamentoId = new Guid("fc73fc67-5237-4830-8f8c-425766ef4d6a"),
                            Email = "vini.souza00@gmail.com",
                            EnderecoId = new Guid("fc73fc67-5137-4830-8f8c-425766ef4d6a"),
                            FuncaoId = new Guid("07b49006-1b80-4784-9de9-f535050e1aad"),
                            Nome = "Vinicius Nascimento",
                            NomeSocial = "N/A",
                            RG = "333333333",
                            Registro = 1,
                            Sexo = 1
                        });
                });

            modelBuilder.Entity("RH.Domain.Entities.Municipio", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("NomeMunicipio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UfId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("UfId");

                    b.ToTable("Municipios", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("f0a1a069-3db3-4f31-b71d-20074e3b861b"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(2035),
                            NomeMunicipio = "São Paulo",
                            UfId = new Guid("5e684315-735e-4c8e-a508-8df50649dc1d")
                        });
                });

            modelBuilder.Entity("RH.Domain.Entities.Pagamento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataPagamento")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FuncionarioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("FuncionarioId");

                    b.ToTable("Pagamentos", (string)null);
                });

            modelBuilder.Entity("RH.Domain.Entities.Uf", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sigla")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Sigla")
                        .IsUnique()
                        .HasFilter("[Sigla] IS NOT NULL");

                    b.ToTable("Ufs", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("77df935a-ca53-4ffd-94ae-c197e016ccf0"),
                            CreateAt = new DateTime(2024, 4, 19, 1, 16, 57, 315, DateTimeKind.Utc).AddTicks(1596),
                            Nome = "Acre",
                            Sigla = "AC"
                        },
                        new
                        {
                            Id = new Guid("8f7ae6df-d6a5-4d86-8994-e64002ee557e"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1609),
                            Nome = "Alagoas",
                            Sigla = "AL"
                        },
                        new
                        {
                            Id = new Guid("489e8c02-00cc-4113-8dab-8e44ead66543"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1622),
                            Nome = "Amapa",
                            Sigla = "AP"
                        },
                        new
                        {
                            Id = new Guid("2b3cb7d6-f792-4ae6-b068-38da911997d8"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1626),
                            Nome = "Amazonas",
                            Sigla = "AM"
                        },
                        new
                        {
                            Id = new Guid("3a8ca4e0-eb66-452c-b4d5-dd4b428f3cbf"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1629),
                            Nome = "Bahia",
                            Sigla = "BA"
                        },
                        new
                        {
                            Id = new Guid("38cdbdab-bc0b-4f2e-b561-500a1708d8da"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1633),
                            Nome = "Ceara",
                            Sigla = "CE"
                        },
                        new
                        {
                            Id = new Guid("20792100-80af-49a8-8195-f7c36441c38d"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1636),
                            Nome = "Espirito Santo",
                            Sigla = "ES"
                        },
                        new
                        {
                            Id = new Guid("8c797ec8-ea24-4bc5-9288-56a6cb14a8ef"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1639),
                            Nome = "Goias",
                            Sigla = "GO"
                        },
                        new
                        {
                            Id = new Guid("7451a52c-8460-4f6a-bca6-7573b9a44759"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1642),
                            Nome = "Maranhao",
                            Sigla = "MA"
                        },
                        new
                        {
                            Id = new Guid("786e47a5-f326-40bc-afb5-0af531e7af9f"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1646),
                            Nome = "Mato Grosso",
                            Sigla = "MT"
                        },
                        new
                        {
                            Id = new Guid("c4dc2412-b190-411a-8352-0a857b7e327b"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1650),
                            Nome = "Mato Grosso do Sul",
                            Sigla = "MS"
                        },
                        new
                        {
                            Id = new Guid("3b72bc3f-4613-4313-963c-9621db443e32"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1654),
                            Nome = "Minas Gerais",
                            Sigla = "MG"
                        },
                        new
                        {
                            Id = new Guid("06759cc3-cf92-49fe-9d98-a8eacb5ee621"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1657),
                            Nome = "Para",
                            Sigla = "PA"
                        },
                        new
                        {
                            Id = new Guid("d4fdba6b-ee4c-4c06-b8d7-7dcbbc0d02fa"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1661),
                            Nome = "Paraiba",
                            Sigla = "PB"
                        },
                        new
                        {
                            Id = new Guid("ef7e5a58-45a2-4b80-8e13-fdeefb2f5a5e"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1664),
                            Nome = "Parana",
                            Sigla = "PR"
                        },
                        new
                        {
                            Id = new Guid("451ecb2b-0ba5-48c7-84ff-32772634c258"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1667),
                            Nome = "Pernambuco",
                            Sigla = "PE"
                        },
                        new
                        {
                            Id = new Guid("275002db-aa62-444e-a179-b801583c3568"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1671),
                            Nome = "Piaui",
                            Sigla = "PI"
                        },
                        new
                        {
                            Id = new Guid("3b0458c6-5eff-4342-bd53-4591d7c006de"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1674),
                            Nome = "Rio de Janeiro",
                            Sigla = "RJ"
                        },
                        new
                        {
                            Id = new Guid("dca93b97-5ef7-44ee-bfb4-5f63b0c72598"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1677),
                            Nome = "Rio Grande do Norte",
                            Sigla = "RN"
                        },
                        new
                        {
                            Id = new Guid("6b57ce63-eb3a-4c73-8b59-8098e6862d48"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1681),
                            Nome = "Rio Grande do Sul",
                            Sigla = "RS"
                        },
                        new
                        {
                            Id = new Guid("12405ad1-e3e5-43fd-9bfe-0c6fa4816105"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1683),
                            Nome = "Rondonia",
                            Sigla = "RO"
                        },
                        new
                        {
                            Id = new Guid("a850fb53-9f5b-449e-b691-d084f8b5a402"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1687),
                            Nome = "Roraima",
                            Sigla = "RR"
                        },
                        new
                        {
                            Id = new Guid("dbb01ebc-4776-4f72-b630-7b249d81c440"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1689),
                            Nome = "Santa Catarina",
                            Sigla = "SC"
                        },
                        new
                        {
                            Id = new Guid("5e684315-735e-4c8e-a508-8df50649dc1d"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1695),
                            Nome = "Sao Paulo",
                            Sigla = "SP"
                        },
                        new
                        {
                            Id = new Guid("6d2e386b-a450-4976-83ce-ed107120c9fb"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1698),
                            Nome = "Sergipe",
                            Sigla = "SE"
                        },
                        new
                        {
                            Id = new Guid("7fdaaa4c-13ed-49d4-b1aa-ceaae53254b6"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1701),
                            Nome = "Tocantins",
                            Sigla = "TO"
                        },
                        new
                        {
                            Id = new Guid("141a0daa-47e8-49fe-8dea-0ee97e4db538"),
                            CreateAt = new DateTime(2024, 4, 18, 22, 16, 57, 315, DateTimeKind.Local).AddTicks(1705),
                            Nome = "Distrito Federal",
                            Sigla = "DF"
                        });
                });

            modelBuilder.Entity("RH.Domain.Entities.DecimoTerceiro", b =>
                {
                    b.HasOne("RH.Domain.Entities.Funcionario", "Funcionario")
                        .WithMany("DecimoTerceiro")
                        .HasForeignKey("FuncionarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Funcionario");
                });

            modelBuilder.Entity("RH.Domain.Entities.Demissao", b =>
                {
                    b.HasOne("RH.Domain.Entities.Funcionario", "Funcionario")
                        .WithOne("Demissao")
                        .HasForeignKey("RH.Domain.Entities.Demissao", "FuncionarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Funcionario");
                });

            modelBuilder.Entity("RH.Domain.Entities.Endereco", b =>
                {
                    b.HasOne("RH.Domain.Entities.Municipio", "Municipio")
                        .WithMany("Enderecos")
                        .HasForeignKey("MunicipioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Municipio");
                });

            modelBuilder.Entity("RH.Domain.Entities.Ferias", b =>
                {
                    b.HasOne("RH.Domain.Entities.Funcionario", "Funcionario")
                        .WithMany("Ferias")
                        .HasForeignKey("FuncionarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Funcionario");
                });

            modelBuilder.Entity("RH.Domain.Entities.Funcionario", b =>
                {
                    b.HasOne("RH.Domain.Entities.ContaBancaria", "ContaBancaria")
                        .WithMany("Funcionarios")
                        .HasForeignKey("ContaBancariaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RH.Domain.Entities.Departamento", "Departamento")
                        .WithMany("Funcionarios")
                        .HasForeignKey("DepartamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RH.Domain.Entities.Endereco", "Endereco")
                        .WithMany("Funcionarios")
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RH.Domain.Entities.Funcao", "Funcao")
                        .WithMany("Funcionarios")
                        .HasForeignKey("FuncaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContaBancaria");

                    b.Navigation("Departamento");

                    b.Navigation("Endereco");

                    b.Navigation("Funcao");
                });

            modelBuilder.Entity("RH.Domain.Entities.Municipio", b =>
                {
                    b.HasOne("RH.Domain.Entities.Uf", "Uf")
                        .WithMany("Municipios")
                        .HasForeignKey("UfId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Uf");
                });

            modelBuilder.Entity("RH.Domain.Entities.Pagamento", b =>
                {
                    b.HasOne("RH.Domain.Entities.Funcionario", "Funcionario")
                        .WithMany("Pagamentos")
                        .HasForeignKey("FuncionarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Funcionario");
                });

            modelBuilder.Entity("RH.Domain.Entities.ContaBancaria", b =>
                {
                    b.Navigation("Funcionarios");
                });

            modelBuilder.Entity("RH.Domain.Entities.Departamento", b =>
                {
                    b.Navigation("Funcionarios");
                });

            modelBuilder.Entity("RH.Domain.Entities.Endereco", b =>
                {
                    b.Navigation("Funcionarios");
                });

            modelBuilder.Entity("RH.Domain.Entities.Funcao", b =>
                {
                    b.Navigation("Funcionarios");
                });

            modelBuilder.Entity("RH.Domain.Entities.Funcionario", b =>
                {
                    b.Navigation("DecimoTerceiro");

                    b.Navigation("Demissao");

                    b.Navigation("Ferias");

                    b.Navigation("Pagamentos");
                });

            modelBuilder.Entity("RH.Domain.Entities.Municipio", b =>
                {
                    b.Navigation("Enderecos");
                });

            modelBuilder.Entity("RH.Domain.Entities.Uf", b =>
                {
                    b.Navigation("Municipios");
                });
#pragma warning restore 612, 618
        }
    }
}
