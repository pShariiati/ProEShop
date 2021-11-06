﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProEShop.DataLayer.Context;

namespace ProEShop.DataLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProEShop.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedByBrowserName")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("CreatedByIp")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<long?>("CreatedByUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedByBrowserName")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ModifiedByIp")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<long?>("ModifiedByUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Test")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ProEShop.Entities.Identity.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedByBrowserName")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("CreatedByIp")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<long?>("CreatedByUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedByBrowserName")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ModifiedByIp")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<long?>("ModifiedByUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ProEShop.Entities.Identity.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedByBrowserName")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("CreatedByIp")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<long?>("CreatedByUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedByBrowserName")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ModifiedByIp")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<long?>("ModifiedByUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("ProEShop.Entities.Identity.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedByBrowserName")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("CreatedByIp")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<long?>("CreatedByUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ModifiedByBrowserName")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ModifiedByIp")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<long?>("ModifiedByUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProEShop.Entities.Identity.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedByBrowserName")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("CreatedByIp")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<long?>("CreatedByUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedByBrowserName")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ModifiedByIp")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<long?>("ModifiedByUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("ProEShop.Entities.Identity.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedByBrowserName")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("CreatedByIp")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<long?>("CreatedByUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedByBrowserName")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ModifiedByIp")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<long?>("ModifiedByUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("ProEShop.Entities.Identity.UserRole", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedByBrowserName")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("CreatedByIp")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<long?>("CreatedByUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedByBrowserName")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ModifiedByIp")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<long?>("ModifiedByUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("ProEShop.Entities.Identity.UserToken", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedByBrowserName")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("CreatedByIp")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<long?>("CreatedByUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedByBrowserName")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ModifiedByIp")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<long?>("ModifiedByUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("ProEShop.Entities.Identity.RoleClaim", b =>
                {
                    b.HasOne("ProEShop.Entities.Identity.Role", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ProEShop.Entities.Identity.UserClaim", b =>
                {
                    b.HasOne("ProEShop.Entities.Identity.User", "User")
                        .WithMany("UserClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProEShop.Entities.Identity.UserLogin", b =>
                {
                    b.HasOne("ProEShop.Entities.Identity.User", "User")
                        .WithMany("UserLogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProEShop.Entities.Identity.UserRole", b =>
                {
                    b.HasOne("ProEShop.Entities.Identity.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProEShop.Entities.Identity.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProEShop.Entities.Identity.UserToken", b =>
                {
                    b.HasOne("ProEShop.Entities.Identity.User", "User")
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProEShop.Entities.Identity.Role", b =>
                {
                    b.Navigation("RoleClaims");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("ProEShop.Entities.Identity.User", b =>
                {
                    b.Navigation("UserClaims");

                    b.Navigation("UserLogins");

                    b.Navigation("UserRoles");

                    b.Navigation("UserTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
