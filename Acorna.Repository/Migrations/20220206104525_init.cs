using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acorna.Repository.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CallDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    GroupId = table.Column<int>(nullable: false),
                    GroupNameEn = table.Column<string>(nullable: true),
                    GroupNameAr = table.Column<string>(nullable: true),
                    GroupName = table.Column<string>(nullable: true),
                    DialledNumber = table.Column<string>(nullable: true),
                    CallDateTime = table.Column<DateTime>(nullable: false),
                    CallDuration = table.Column<string>(nullable: true),
                    CallRetailPrice = table.Column<decimal>(type: "decimal(18, 3)", nullable: false),
                    CallDiscountPrice = table.Column<decimal>(type: "decimal(18, 3)", nullable: false),
                    TypePhoneNumberId = table.Column<int>(nullable: false),
                    TypePhoneNumberAr = table.Column<string>(nullable: true),
                    TypePhoneNumberEn = table.Column<string>(nullable: true),
                    TypePhoneNumberName = table.Column<string>(nullable: true),
                    ServiceUsedId = table.Column<int>(nullable: false),
                    ServiceUsedNameAr = table.Column<string>(nullable: true),
                    ServiceUsedNameEn = table.Column<string>(nullable: true),
                    ServiceUsedName = table.Column<string>(nullable: true),
                    TypeServiceUsedId = table.Column<int>(nullable: false),
                    TypeServiceUsedNameAr = table.Column<string>(nullable: true),
                    TypeServiceUsedNameEn = table.Column<string>(nullable: true),
                    TypeServiceUsedName = table.Column<string>(nullable: true),
                    BillId = table.Column<int>(nullable: false),
                    BillYear = table.Column<int>(nullable: false),
                    BillMonth = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "CallFinance",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    UserEmail = table.Column<string>(nullable: true),
                    GroupNameAr = table.Column<string>(nullable: true),
                    GroupNameEn = table.Column<string>(nullable: true),
                    GroupName = table.Column<string>(nullable: true),
                    FreeServicesSum = table.Column<decimal>(type: "decimal(18, 3)", nullable: false),
                    OfficialServicesSum = table.Column<decimal>(type: "decimal(18, 3)", nullable: false),
                    PersonalServicesSum = table.Column<decimal>(type: "decimal(18, 3)", nullable: false),
                    UnknownServicesSum = table.Column<decimal>(type: "decimal(18, 3)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "CallSummary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    UserEmail = table.Column<string>(nullable: true),
                    GroupNameAr = table.Column<string>(nullable: true),
                    GroupNameEn = table.Column<string>(nullable: true),
                    GroupName = table.Column<string>(nullable: true),
                    FreeSum = table.Column<decimal>(type: "decimal(18, 3)", nullable: false),
                    OfficialSum = table.Column<decimal>(type: "decimal(18, 3)", nullable: false),
                    PersonalSum = table.Column<decimal>(type: "decimal(18, 3)", nullable: false),
                    UnknownSum = table.Column<decimal>(type: "decimal(18, 3)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    RecipientId = table.Column<int>(nullable: false),
                    MessageText = table.Column<string>(nullable: true),
                    IsRead = table.Column<bool>(nullable: false),
                    DateRead = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    CountryNameAr = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CountryNameEn = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CountryKey = table.Column<int>(type: "int", nullable: false),
                    PricePerMinute = table.Column<decimal>(type: "decimal(18, 3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneralSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    SettingName = table.Column<string>(maxLength: 150, nullable: false),
                    SettingValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    GroupNameAr = table.Column<string>(maxLength: 150, nullable: false),
                    GroupNameEn = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    JobNameAr = table.Column<string>(maxLength: 150, nullable: false),
                    JobNameEn = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    LanguageCode = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    LanguageDirection = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    LanguageFlag = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    LanguageDefaultDisply = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    Name = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operators",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    OperatorNameAr = table.Column<string>(maxLength: 150, nullable: false),
                    OperatorNameEn = table.Column<string>(maxLength: 150, nullable: false),
                    OperatorKey = table.Column<int>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhonesBook",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: false),
                    PhoneName = table.Column<string>(maxLength: 150, nullable: false),
                    PersonalUserId = table.Column<int>(maxLength: 10, nullable: false),
                    TypePhoneNumberId = table.Column<int>(maxLength: 10, nullable: false),
                    StatusNumberId = table.Column<int>(maxLength: 10, nullable: false),
                    StatusAdminId = table.Column<int>(maxLength: 10, nullable: false),
                    ReferanceNotificationId = table.Column<int>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhonesBook", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PowerLevel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServicesUsed",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    ServiceUsedNameAr = table.Column<string>(maxLength: 150, nullable: false),
                    ServiceUsedNameEn = table.Column<string>(maxLength: 150, nullable: false),
                    ServicePrice = table.Column<decimal>(nullable: true),
                    IsCalculatedValue = table.Column<bool>(nullable: false),
                    IsNeedApproved = table.Column<bool>(nullable: false),
                    NonOfficial = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesUsed", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimCardTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    TypeName = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimCardTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    ProfileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypePhoneNumber",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    TypeNameAr = table.Column<string>(nullable: true),
                    TypeNameEn = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypePhoneNumber", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Governorate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    GovernorateNameAr = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    GovernorateNameEn = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governorate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Governorate_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    RecipientId = table.Column<int>(nullable: false),
                    IsRead = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    ReferenceMassageId = table.Column<int>(nullable: false),
                    RecipientRoleId = table.Column<int>(nullable: false),
                    NotificationTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationType_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalTable: "NotificationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanService",
                columns: table => new
                {
                    PlanId = table.Column<int>(nullable: false),
                    ServiceUsedId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    Limit = table.Column<int>(nullable: false),
                    Unit = table.Column<string>(nullable: true),
                    AdditionalUnit = table.Column<string>(nullable: true),
                    AdditionalUnitPrice = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanService", x => new { x.PlanId, x.ServiceUsedId });
                    table.ForeignKey(
                        name: "FK_PlanService_Plan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanService_ServicesUsed_ServiceUsedId",
                        column: x => x.ServiceUsedId,
                        principalTable: "ServicesUsed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "date", nullable: false),
                    City = table.Column<byte>(type: "tinyint", nullable: false),
                    Country = table.Column<byte>(type: "tinyint", nullable: false),
                    PhotoURL = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LanguageId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    SimCardTypeId = table.Column<int>(nullable: false),
                    SimProfileId = table.Column<int>(nullable: false),
                    EmailOtpCode = table.Column<string>(nullable: true),
                    OtpEmailExpiryTime = table.Column<DateTime>(nullable: true),
                    PlanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Plan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_SimCardTypes_SimCardTypeId",
                        column: x => x.SimCardTypeId,
                        principalTable: "SimCardTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_SimProfiles_SimProfileId",
                        column: x => x.SimProfileId,
                        principalTable: "SimProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificationsDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    MessageText = table.Column<string>(nullable: true),
                    NotificationsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationsDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationsDetails_Notifications_NotificationsId",
                        column: x => x.NotificationsId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AllocatedUsersServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    ServiceUsedId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocatedUsersServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllocatedUsersServices_ServicesUsed_ServiceUsedId",
                        column: x => x.ServiceUsedId,
                        principalTable: "ServicesUsed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AllocatedUsersServices_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    BillDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SubmittedByAdmin = table.Column<bool>(type: "bit", nullable: false),
                    SubmittedByUser = table.Column<bool>(type: "bit", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsTerminal = table.Column<bool>(type: "bit", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: false),
                    SubmittedDate = table.Column<DateTime>(nullable: false),
                    StatusBillId = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    PhoneBookId = table.Column<int>(type: "int", nullable: false),
                    CallDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    CallDuration = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    CallNetPrice = table.Column<decimal>(type: "decimal(18, 3)", nullable: false),
                    CallRetailPrice = table.Column<decimal>(type: "decimal(18, 3)", nullable: false),
                    CallDiscountPrice = table.Column<decimal>(type: "decimal(18, 3)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    TypePhoneNumberId = table.Column<int>(type: "int", nullable: false),
                    ServiceUsedId = table.Column<int>(type: "int", nullable: false),
                    IsServiceUsedNeedApproved = table.Column<bool>(nullable: false),
                    StatusServiceUsedId = table.Column<int>(nullable: false),
                    StatusServiceUsedBy = table.Column<int>(nullable: false),
                    TypeServiceUsedId = table.Column<int>(nullable: false),
                    OperatorId = table.Column<int>(nullable: false),
                    DataUsage = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    BillId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillDetails_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "GeneralSettings",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "SettingName", "SettingValue", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "default_image", "", null, null });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "GroupNameAr", "GroupNameEn", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "الموظفين", "Employees", null, null },
                    { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "الإدارة", "Administration", null, null }
                });

            migrationBuilder.InsertData(
                table: "Language",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "LanguageCode", "LanguageDefaultDisply", "LanguageDirection", "LanguageFlag", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 2, 1, new DateTime(2022, 2, 6, 12, 45, 25, 129, DateTimeKind.Local).AddTicks(8181), "EN", "English", "LTR", "", null, null },
                    { 1, 1, new DateTime(2022, 2, 6, 12, 45, 25, 124, DateTimeKind.Local).AddTicks(3892), "AR", "العربية", "RTL", "", null, null }
                });

            migrationBuilder.InsertData(
                table: "NotificationType",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 150, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ServicesRejected", null, null },
                    { 140, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ServicesInProgress", null, null },
                    { 130, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ServicesApproved", null, null },
                    { 120, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ServicesSubmitted", null, null },
                    { 110, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Chatting", null, null },
                    { 10, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "PhoneNumbersSubmitted", null, null },
                    { 90, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BillRejected", null, null },
                    { 80, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BillInProgress", null, null },
                    { 70, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BillApproved", null, null },
                    { 60, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BillSubmitted", null, null },
                    { 50, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BillUploaded", null, null },
                    { 40, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "PhoneNumbersRejected", null, null },
                    { 30, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "PhoneNumbersIprogress", null, null },
                    { 20, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "PhoneNumbersApproved", null, null },
                    { 100, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BillPaid", null, null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "PowerLevel" },
                values: new object[,]
                {
                    { 4, "84c0ce61-cb35-43c2-9dbd-923c40d78b6e", "Finance", "FINANCE", 96 },
                    { 3, "6bdeda4e-fa7f-41ba-bd85-36389205c84d", "Employee", "EMPLOYEE", 97 },
                    { 2, "6f919296-0f4e-46a8-a3f7-2f91ce6d853e", "AdminGroup", "ADMINGROUP", 98 },
                    { 1, "670443ff-d43f-42ff-b951-5278c5843e90", "SuperAdmin", "SUPERADMIN", 99 }
                });

            migrationBuilder.InsertData(
                table: "ServicesUsed",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsCalculatedValue", "IsNeedApproved", "NonOfficial", "ServicePrice", "ServiceUsedNameAr", "ServiceUsedNameEn", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, false, null, "فارغ", "Empty", null, null },
                    { 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, false, null, "EBU_حزمة", "EBU_Bundel", null, null }
                });

            migrationBuilder.InsertData(
                table: "SimCardTypes",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "TypeName", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Data", null, null },
                    { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Voice", null, null }
                });

            migrationBuilder.InsertData(
                table: "SimProfiles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "ProfileName", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 3, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stock", null, null },
                    { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Activated", null, null },
                    { 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Deactivate", null, null }
                });

            migrationBuilder.InsertData(
                table: "TypePhoneNumber",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "TypeNameAr", "TypeNameEn", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "رسمي", "Official", null, null },
                    { 3, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "شخصي", "Personal", null, null },
                    { 4, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "غير معرف", "Unknown", null, null },
                    { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "مجاني", "Free", null, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "City", "ConcurrencyStamp", "Country", "CreateDate", "DateOfBirth", "Email", "EmailConfirmed", "EmailOtpCode", "Gender", "GroupId", "IsActive", "LanguageId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OtpEmailExpiryTime", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoURL", "PlanId", "SecurityStamp", "SimCardTypeId", "SimProfileId", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, (byte)0, "f153eff2-9b7f-418a-992c-b1457df15d8a", (byte)0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SuperAdmin@a.com", false, null, null, 1, true, 2, false, null, "SUPERADMIN@A.COM", "SUPERADMIN", null, "AQAAAAEAACcQAAAAEJhixZI5vOb7zwvURrWFkqz5nu2cbdgNZvruUQyhkN2XPOIAZnqbAlakWBCDcgvBbA==", "superAdmin", false, null, null, "fdc0495f-7b24-43cd-bc23-fd9954d25ae2", 1, 1, false, "superAdmin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "City", "ConcurrencyStamp", "Country", "CreateDate", "DateOfBirth", "Email", "EmailConfirmed", "EmailOtpCode", "Gender", "GroupId", "IsActive", "LanguageId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OtpEmailExpiryTime", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoURL", "PlanId", "SecurityStamp", "SimCardTypeId", "SimProfileId", "TwoFactorEnabled", "UserName" },
                values: new object[] { 2, 0, (byte)0, "e45f55ba-0c70-4309-9df4-2d981706e7f9", (byte)0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AdminGroup@a.com", false, null, null, 1, true, 2, false, null, "ADMINGROUP@A.COM", "ADMINGROUP", null, "AQAAAAEAACcQAAAAEMajzB0pH7eb7SnbewNJiTFyHq0CSXcMGbDKvPQjbMS/chZqUCnHnXI/p8ipX0eSPQ==", "adminGroup", false, null, null, "987a7c04-9995-4a95-8df2-c08693b06c43", 1, 1, false, "adminGroup" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "City", "ConcurrencyStamp", "Country", "CreateDate", "DateOfBirth", "Email", "EmailConfirmed", "EmailOtpCode", "Gender", "GroupId", "IsActive", "LanguageId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OtpEmailExpiryTime", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoURL", "PlanId", "SecurityStamp", "SimCardTypeId", "SimProfileId", "TwoFactorEnabled", "UserName" },
                values: new object[] { 3, 0, (byte)0, "fe727327-5976-44fc-b498-6dc741b3bba5", (byte)0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Employee@a.com", false, null, null, 2, true, 2, false, null, "EMPLOYEE@A.COM", "EMPLOYEE", null, "AQAAAAEAACcQAAAAEM8RgvZ5HET3wP4e+xMzqorwrvIc+7pk507YpQeQmlYPY22PI65w6VoSieNrW8pBng==", "employee", false, null, null, "c3222cd1-bddf-4062-af21-c09ae3f61c97", 1, 1, false, "employee" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { 2, 2 });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { 3, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_AllocatedUsersServices_ServiceUsedId",
                table: "AllocatedUsersServices",
                column: "ServiceUsedId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocatedUsersServices_UserId",
                table: "AllocatedUsersServices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_BillId",
                table: "BillDetails",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_UserId",
                table: "Bills",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Governorate_CountryId",
                table: "Governorate",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationTypeId",
                table: "Notifications",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationsDetails_NotificationsId",
                table: "NotificationsDetails",
                column: "NotificationsId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanService_ServiceUsedId",
                table: "PlanService",
                column: "ServiceUsedId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GroupId",
                table: "Users",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LanguageId",
                table: "Users",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PlanId",
                table: "Users",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SimCardTypeId",
                table: "Users",
                column: "SimCardTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SimProfileId",
                table: "Users",
                column: "SimProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllocatedUsersServices");

            migrationBuilder.DropTable(
                name: "BillDetails");

            migrationBuilder.DropTable(
                name: "CallDetails");

            migrationBuilder.DropTable(
                name: "CallFinance");

            migrationBuilder.DropTable(
                name: "CallSummary");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "GeneralSettings");

            migrationBuilder.DropTable(
                name: "Governorate");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "NotificationsDetails");

            migrationBuilder.DropTable(
                name: "Operators");

            migrationBuilder.DropTable(
                name: "PhonesBook");

            migrationBuilder.DropTable(
                name: "PlanService");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "TypePhoneNumber");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "ServicesUsed");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "NotificationType");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "Plan");

            migrationBuilder.DropTable(
                name: "SimCardTypes");

            migrationBuilder.DropTable(
                name: "SimProfiles");
        }
    }
}
