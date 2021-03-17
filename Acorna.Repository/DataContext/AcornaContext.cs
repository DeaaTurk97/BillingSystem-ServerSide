using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Acorna.Core.Entity;
using Acorna.Core.Entity.Notification;
using Acorna.Core.Entity.Security;
using Acorna.Core.Entity.SystemDefinition;
using Acorna.Core.Entity.Project.BillingSystem;

namespace Acorna.Repository.DataContext
{
    public class AcornaContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                                     IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AcornaContext(DbContextOptions<AcornaContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        private int UserId => int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

        public async Task<int> GetUserLanguageId()
        {
            StringValues languages;
            _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Accept-Language", out languages);
            return languages.Count == 0 ? 1 : int.Parse(languages[0]);
        }

        public override int SaveChanges()
        {
            return this.SaveChangesAsync(true).Result;
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = UserId;
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = UserId;
                        entry.Entity.UpdatedDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(builder);

            builder.Entity<GeneralSetting>().ToTable("GeneralSettings").HasData(new List<GeneralSetting>
            {
                new GeneralSetting
                {
                    Id = 1,
                    SettingName = "default_image",
                    SettingValue = ""
                }
            });

            var hasher = new PasswordHasher<User>();

            builder.Entity<UserRole>(
                userRole =>
                {
                    userRole.HasKey(ur => new { ur.UserId, ur.RoleId });
                    userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(fur => fur.RoleId)
                    .IsRequired();

                    userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(fur => fur.UserId)
                    .IsRequired();
                }
            );

            //Create New Roles
            builder.Entity<Role>().ToTable("Roles").HasData(new List<Role>
            {
                new Role {
                    Id = 1,
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN",
                    PowerLevel = 99
                },
                new Role {
                    Id = 2,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    PowerLevel = 98
                },
                new Role {
                    Id = 3,
                    Name = "AdminGroup",
                    NormalizedName = "ADMINGROUP",
                    PowerLevel = 97
                },
                new Role {
                    Id = 4,
                    Name = "Student",
                    NormalizedName = "STUDENT",
                    PowerLevel = 96
                },
                new Role {
                    Id = 5,
                    Name = "Guest",
                    NormalizedName = "GUEST",
                    PowerLevel = 95
                },
            });

            //Create New Users
            builder.Entity<User>().ToTable("Users").HasData(
            new User
            {
                Id = 1, // primary key
                UserName = "superAdmin",
                NormalizedUserName = "SUPERADMIN",
                Email = "SuperAdmin@a.com",
                NormalizedEmail = "SUPERADMIN@A.COM",
                PasswordHash = hasher.HashPassword(null, "Acorna@#123"),
                IsActive = true,
                LanguageId = 1,
                SecurityStamp = Guid.NewGuid().ToString()
            },
            new User
            {
                Id = 2, // primary key
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "Admin@a.com",
                NormalizedEmail = "Admin@A.COM",
                PasswordHash = hasher.HashPassword(null, "Acorna@#123"),
                IsActive = true,
                LanguageId = 2,
                SecurityStamp = Guid.NewGuid().ToString()
            },
            new User
            {
                Id = 3, // primary key
                UserName = "adminGroup",
                NormalizedUserName = "ADMINGROUP",
                Email = "AdminGroup@a.com",
                NormalizedEmail = "ADMINGROUP@A.COM",
                PasswordHash = hasher.HashPassword(null, "Acorna@#123"),
                IsActive = true,
                LanguageId = 1,
                SecurityStamp = Guid.NewGuid().ToString()
            },
            new User
            {
                Id = 4, // primary key
                UserName = "student",
                NormalizedUserName = "STUDENT",
                Email = "Student@a.com",
                NormalizedEmail = "STUDENT@A.COM",
                PasswordHash = hasher.HashPassword(null, "Acorna@#123"),
                IsActive = true,
                LanguageId = 1,
                SecurityStamp = Guid.NewGuid().ToString()
            },
            new User
            {
                Id = 5, // primary key
                UserName = "guest",
                NormalizedUserName = "GUEST",
                Email = "Guest@a.com",
                NormalizedEmail = "GUEST@A.COM",
                PasswordHash = hasher.HashPassword(null, "Acorna@#123"),
                IsActive = true,
                LanguageId = 1,
                SecurityStamp = Guid.NewGuid().ToString()
            }
            );
            //Assign role to User
            builder.Entity<UserRole>().ToTable("UserRoles").HasData(
            new UserRole
            {
                RoleId = 1,
                UserId = 1
            },
            new UserRole
            {
                RoleId = 2,
                UserId = 2
            },
            new UserRole
            {
                RoleId = 3,
                UserId = 3
            },
            new UserRole
            {
                RoleId = 4,
                UserId = 4
            },
            new UserRole
            {
                RoleId = 5,
                UserId = 5
            }
            );

            builder.Entity<Language>().ToTable("Language").HasData(new List<Language>
            {
                new Language {
                    Id = 1,
                    LanguageCode="AR",
                    LanguageDefaultDisply = "العربية",
                    LanguageDirection = "RTL",
                    CreatedDate = DateTime.Now,
                    CreatedBy = 1,
                    LanguageFlag = "",
                },
                new Language {
                    Id = 2,
                    LanguageCode="EN",
                    LanguageDefaultDisply = "English",
                    LanguageDirection = "LTR",
                    CreatedDate = DateTime.Now,
                    CreatedBy = 1,
                    LanguageFlag = "",
                },
            });

            builder.Entity<NotificationType>().ToTable("NotificationType").HasData(new List<NotificationType>
            {
                new NotificationType {
                    CreatedBy = 1,
                    Id = 10,
                    Name = "Chatting",
                },
            });

            builder.Entity<NotificationTemplate>().ToTable("NotificationTemplate").HasData(new List<NotificationTemplate>
            {
                new NotificationTemplate {
                    Id = 1,
                    NotificationTypeId = 10
                },
            });

            //Rename All Tables Identity
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
        }

        public DbSet<Job> Job { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Operator> Operator { get; set; }
        public DbSet<GeneralSetting> GeneralSetting { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<NotificationType> NotificationType { get; set; }
        public DbSet<NotificationTemplate> NotificationTemplate { get; set; }

        public new DbSet<T> Set<T>() where T : BaseEntity
        {
            return base.Set<T>();
        }
    }
}
