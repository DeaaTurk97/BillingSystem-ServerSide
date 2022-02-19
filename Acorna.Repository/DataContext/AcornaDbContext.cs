using Acorna.Core.Entity;
using Acorna.Core.Entity.Notification;
using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Entity.Security;
using Acorna.Core.Entity.SystemDefinition;
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

namespace Acorna.Repository.DataContext
{
    public class AcornaDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                                     IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AcornaDbContext(DbContextOptions<AcornaDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public int UserId => int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        const int Default_User_Language_ID = 1;

        public async Task<int> GetUserLanguageId()
        {
            try
            {
                StringValues languages = new StringValues();

                if (!_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Accept-Language", out languages))
                {
                    return Default_User_Language_ID;
                }
                return StringValues.IsNullOrEmpty(languages) || languages.Count == 0 ? Default_User_Language_ID : int.Parse(languages[0]);
            }
            catch { return Default_User_Language_ID; }
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

                builder.Entity<PlanService>()
                      .HasKey(bc => new { bc.PlanId, bc.ServiceUsedId });
                builder.Entity<PlanService>()
                     .HasOne(bc => bc.Plan)
                     .WithMany(b => b.PlanServices)
                     .HasForeignKey(bc => bc.PlanId);
                builder.Entity<PlanService>()
                    .HasOne(bc => bc.ServiceUsed)
                    .WithMany(c => c.PlanServices)
                    .HasForeignKey(bc => bc.ServiceUsedId);

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
                    Name = "AdminGroup",
                    NormalizedName = "ADMINGROUP",
                    PowerLevel = 98
                },
                new Role {
                    Id = 3,
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE",
                    PowerLevel = 97
                },
                 new Role {
                    Id = 4,
                    Name = "Finance",
                    NormalizedName = "FINANCE",
                    PowerLevel = 96
                }
            });

            //Create New Users
            builder.Entity<User>().ToTable("Users").HasData(
            new User
            {
                Id = 1, // primary key
                UserName = "superAdmin",
                NormalizedUserName = "SUPERADMIN",
                PhoneNumber = "superAdmin",
                Email = "SuperAdmin@a.com",
                NormalizedEmail = "SUPERADMIN@A.COM",
                PasswordHash = hasher.HashPassword(null, "Unicef@#123"),
                IsActive = true,
                LanguageId = 2,
                SecurityStamp = Guid.NewGuid().ToString(),
                GroupId = 1,
                SimCardTypeId = 1,
                SimProfileId = 1
            },
            new User
            {
                Id = 2, // primary key
                UserName = "adminGroup",
                NormalizedUserName = "ADMINGROUP",
                PhoneNumber = "adminGroup",
                Email = "AdminGroup@a.com",
                NormalizedEmail = "ADMINGROUP@A.COM",
                PasswordHash = hasher.HashPassword(null, "Unicef@#123"),
                IsActive = true,
                LanguageId = 2,
                SecurityStamp = Guid.NewGuid().ToString(),
                GroupId = 1,
                SimCardTypeId = 1,
                SimProfileId = 1
            },
            new User
            {
                Id = 3, // primary key
                UserName = "employee",
                NormalizedUserName = "EMPLOYEE",
                PhoneNumber = "employee",
                Email = "Employee@a.com",
                NormalizedEmail = "EMPLOYEE@A.COM",
                PasswordHash = hasher.HashPassword(null, "Unicef@#123"),
                IsActive = true,
                LanguageId = 2,
                SecurityStamp = Guid.NewGuid().ToString(),
                GroupId = 2,
                SimCardTypeId = 1,
                SimProfileId = 1
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
                    Name = "PhoneNumbersSubmitted",
                },
                 new NotificationType {
                    CreatedBy = 1,
                    Id = 20,
                    Name = "PhoneNumbersApproved",
                },
                 new NotificationType {
                    CreatedBy = 1,
                    Id = 30,
                    Name = "PhoneNumbersIprogress",
                },
                 new NotificationType {
                    CreatedBy = 1,
                    Id = 40,
                    Name = "PhoneNumbersRejected",
                },
                 new NotificationType {
                    CreatedBy = 1,
                    Id = 50,
                    Name = "BillUploaded",
                },
                  new NotificationType {
                    CreatedBy = 1,
                    Id = 60,
                    Name = "BillSubmitted",
                },
                   new NotificationType {
                    CreatedBy = 1,
                    Id = 70,
                    Name = "BillApproved",
                },
                 new NotificationType {
                    CreatedBy = 1,
                    Id = 80,
                    Name = "BillInProgress",
                },
                 new NotificationType {
                    CreatedBy = 1,
                    Id = 90,
                    Name = "BillRejected",
                },
                 new NotificationType {
                    CreatedBy = 1,
                    Id = 100,
                    Name = "BillPaid",
                },
                 new NotificationType {
                    CreatedBy = 1,
                    Id = 110,
                    Name = "Chatting",
                },
                 new NotificationType {
                    CreatedBy = 1,
                    Id = 120,
                    Name = "ServicesSubmitted",
                },
                 new NotificationType {
                    CreatedBy = 1,
                    Id = 130,
                    Name = "ServicesApproved",
                },
                 new NotificationType {
                    CreatedBy = 1,
                    Id = 140,
                    Name = "ServicesInProgress",
                },
                 new NotificationType {
                    CreatedBy = 1,
                    Id = 150,
                    Name = "ServicesRejected",
                },
                 new NotificationType {
                    CreatedBy = 1,
                    Id = 160,
                    Name = "NewServiceAdded",
                },
                 new NotificationType {
                    CreatedBy = 1,
                    Id = 170,
                    Name = "ServiceRemoved",
                },
                 new NotificationType {
                    CreatedBy = 1,
                    Id = 180,
                    Name = "ServicePriceGraterThanServicePlan",
                },
            });

            //Create New Groups
            builder.Entity<Group>().ToTable("Groups").HasData(new List<Group>
            {
                new Group {
                    Id = 1,
                    GroupNameAr = "الإدارة",
                    GroupNameEn = "Administration",
                },
                 new Group {
                    Id = 2,
                    GroupNameAr = "الموظفين",
                    GroupNameEn = "Employees",
                },
            });

            //Create New ServiceType
            builder.Entity<ServiceUsed>().ToTable("ServicesUsed").HasData(new List<ServiceUsed>
            {
                new ServiceUsed {
                    Id = 1,
                    ServiceUsedNameAr = "فارغ",
                    ServiceUsedNameEn = "Empty",
                    IsCalculatedValue = false,
                },
                new ServiceUsed {
                    Id = 2,
                    ServiceUsedNameAr = "EBU_حزمة",
                    ServiceUsedNameEn = "EBU_Bundel",
                    IsCalculatedValue = false,
                },

            });

            //Create New Phone numbers type
            builder.Entity<TypePhoneNumber>().ToTable("TypePhoneNumber").HasData(new List<TypePhoneNumber>
            {
                new TypePhoneNumber {
                    Id = 1,
                    TypeNameAr = "مجاني",
                    TypeNameEn = "Free",
                },
                new TypePhoneNumber {
                    Id = 2,
                    TypeNameAr = "رسمي",
                    TypeNameEn = "Official",
                },
                new TypePhoneNumber {
                    Id = 3,
                    TypeNameAr = "شخصي",
                    TypeNameEn = "Personal",
                },
                new TypePhoneNumber {
                    Id = 4,
                    TypeNameAr = "غير معرف",
                    TypeNameEn = "Unknown",
                },

            });

            //Create New SIM Card type
            builder.Entity<SimCardType>().ToTable("SimCardTypes").HasData(new List<SimCardType>
            {
                new SimCardType {
                    Id = 1,
                    TypeName = "Voice",
                },
                new SimCardType {
                    Id = 2,
                    TypeName = "Data",
                },
            });


            //Create New SIM Profiles
            builder.Entity<SimProfile>().ToTable("SimProfiles").HasData(new List<SimProfile>
            {
                new SimProfile {
                    Id = 1,
                    ProfileName = "Activated",
                },
                new SimProfile {
                    Id = 2,
                    ProfileName = "Deactivate",
                },
                 new SimProfile {
                    Id = 3,
                    ProfileName = "Stock",
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
        public DbSet<Country> Country { get; set; }
        public DbSet<Governorate> Governorate { get; set; }
        public DbSet<PhoneBook> PhoneBook { get; set; }
        public DbSet<GeneralSetting> GeneralSetting { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<NotificationType> NotificationType { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<NotificationsDetails> NotificationsDetails { get; set; }
        public DbSet<Bill> Bill { get; set; }
        public DbSet<Plan> Plan { get; set; }

        public DbSet<History> History { get; set; }

        public DbSet<PlanService> PlanService { get; set; }

        public DbSet<BillDetails> BillDetails { get; set; }
        public DbSet<ServiceUsed> ServiceUsed { get; set; }
        public DbSet<TypePhoneNumber> TypePhoneNumber { get; set; }
        public DbSet<AllocatedUsersService> AllocatedUsersService { get; set; }
        public DbSet<SimCardType> SimCardType { get; set; }
        public DbSet<SimProfile> SimProfile { get; set; }

        public DbQuery<CallDetailsDTO> CallDetails { get; set; }
        public DbQuery<CallSummaryDTO> CallSummary { get; set; }
        public DbQuery<CallFinanceDTO> CallFinance { get; set; }

        public new DbSet<T> Set<T>() where T : BaseEntity
        {
            return base.Set<T>();
        }
    }
}
