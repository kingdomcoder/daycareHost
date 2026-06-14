using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Daycare.WebAPIHost.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class InitialPg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    ActivityId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChildId = table.Column<int>(type: "integer", nullable: true),
                    Parent1Id = table.Column<string>(type: "text", nullable: true),
                    Parent2Id = table.Column<string>(type: "text", nullable: true),
                    OrganizationId = table.Column<int>(type: "integer", nullable: true),
                    TargetDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ChildFirstName = table.Column<string>(type: "text", nullable: true),
                    ChildLastName = table.Column<string>(type: "text", nullable: true),
                    ChildMiddleName = table.Column<string>(type: "text", nullable: true),
                    ChildShimei = table.Column<string>(type: "text", nullable: true),
                    ChildMyoji = table.Column<string>(type: "text", nullable: true),
                    ActivityName = table.Column<string>(type: "text", nullable: true),
                    ActivityDescription = table.Column<string>(type: "text", nullable: true),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.ActivityId);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceRecord",
                columns: table => new
                {
                    AttendanceRecordId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChildId = table.Column<int>(type: "integer", nullable: true),
                    OrganizationId = table.Column<int>(type: "integer", nullable: true),
                    Parent1Id = table.Column<string>(type: "text", nullable: true),
                    Parent2Id = table.Column<string>(type: "text", nullable: true),
                    ChildFirstName = table.Column<string>(type: "text", nullable: true),
                    ChildLastName = table.Column<string>(type: "text", nullable: true),
                    ImagePath = table.Column<string>(type: "text", nullable: true),
                    ImageFileName = table.Column<string>(type: "text", nullable: true),
                    TargetDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    InTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    InTime_StampTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    InTime_EnteredBy = table.Column<string>(type: "text", nullable: true),
                    OutTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    OutTime_StampTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    OutTime_EnteredBy = table.Column<string>(type: "text", nullable: true),
                    Tardy = table.Column<bool>(type: "boolean", nullable: true),
                    TardyComment = table.Column<string>(type: "text", nullable: true),
                    Tardy_StampTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Tardy_EnteredBy = table.Column<string>(type: "text", nullable: true),
                    Absent = table.Column<bool>(type: "boolean", nullable: true),
                    AbsentComment = table.Column<string>(type: "text", nullable: true),
                    Absent_StampTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Absent_EnteredBy = table.Column<string>(type: "text", nullable: true),
                    LeaveEarly = table.Column<bool>(type: "boolean", nullable: true),
                    LeaveEarlyComment = table.Column<string>(type: "text", nullable: true),
                    LeaveEarly_StampTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LeaveEarly_EnteredBy = table.Column<string>(type: "text", nullable: true),
                    CancelInTime_StampTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CancelInTime_EnteredBy = table.Column<string>(type: "text", nullable: true),
                    CancelOutTime_StampTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CancelOutTime_EnteredBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceRecord", x => x.AttendanceRecordId);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessage",
                columns: table => new
                {
                    ChatMessageId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrganizationId = table.Column<int>(type: "integer", nullable: true),
                    LoginUserId = table.Column<string>(type: "text", nullable: true),
                    LoginUserType = table.Column<string>(type: "text", nullable: true),
                    LoginUserFirstName = table.Column<string>(type: "text", nullable: true),
                    LoginUserLastName = table.Column<string>(type: "text", nullable: true),
                    ChatWithUserId = table.Column<string>(type: "text", nullable: true),
                    ChatWithUserType = table.Column<string>(type: "text", nullable: true),
                    ChatWithUserFirstName = table.Column<string>(type: "text", nullable: true),
                    ChatWithUserLastName = table.Column<string>(type: "text", nullable: true),
                    ChildId = table.Column<int>(type: "integer", nullable: true),
                    ChildFirstName = table.Column<string>(type: "text", nullable: true),
                    ChildLastName = table.Column<string>(type: "text", nullable: true),
                    MessageType = table.Column<string>(type: "text", nullable: true),
                    MessageContent = table.Column<string>(type: "text", nullable: true),
                    ImagePath = table.Column<string>(type: "text", nullable: true),
                    ImageFileName = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessage", x => x.ChatMessageId);
                });

            migrationBuilder.CreateTable(
                name: "ChatUser",
                columns: table => new
                {
                    ChatUserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrganizationId = table.Column<int>(type: "integer", nullable: true),
                    LoginUserId = table.Column<string>(type: "text", nullable: true),
                    LoginUserType = table.Column<string>(type: "text", nullable: true),
                    LoginUserFirstName = table.Column<string>(type: "text", nullable: true),
                    LoginUserLastName = table.Column<string>(type: "text", nullable: true),
                    ChatWithUserId = table.Column<string>(type: "text", nullable: true),
                    ChatWithUserType = table.Column<string>(type: "text", nullable: true),
                    ChatWithUserFirstName = table.Column<string>(type: "text", nullable: true),
                    ChatWithUserLastName = table.Column<string>(type: "text", nullable: true),
                    ChildId = table.Column<int>(type: "integer", nullable: true),
                    ChildFirstName = table.Column<string>(type: "text", nullable: true),
                    ChildLastName = table.Column<string>(type: "text", nullable: true),
                    LastMessageText = table.Column<string>(type: "text", nullable: true),
                    ImagePath = table.Column<string>(type: "text", nullable: true),
                    ImageFileName = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatUser", x => x.ChatUserId);
                });

            migrationBuilder.CreateTable(
                name: "Child",
                columns: table => new
                {
                    ChildId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Parent1Id = table.Column<string>(type: "text", nullable: true),
                    Parent2Id = table.Column<string>(type: "text", nullable: true),
                    OrganizationId = table.Column<int>(type: "integer", nullable: true),
                    ChildFirstName = table.Column<string>(type: "text", nullable: true),
                    ChildLastName = table.Column<string>(type: "text", nullable: true),
                    ChildMiddleName = table.Column<string>(type: "text", nullable: true),
                    ChildShimei = table.Column<string>(type: "text", nullable: true),
                    ChildMyoji = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    DOB = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ImagePath = table.Column<string>(type: "text", nullable: true),
                    ImageFileName = table.Column<string>(type: "text", nullable: true),
                    Grade = table.Column<int>(type: "integer", nullable: true),
                    ClassName = table.Column<string>(type: "text", nullable: true),
                    AttendMon = table.Column<bool>(type: "boolean", nullable: true),
                    AttendTue = table.Column<bool>(type: "boolean", nullable: true),
                    AttendWed = table.Column<bool>(type: "boolean", nullable: true),
                    AttendThu = table.Column<bool>(type: "boolean", nullable: true),
                    AttendFri = table.Column<bool>(type: "boolean", nullable: true),
                    AttendSat = table.Column<bool>(type: "boolean", nullable: true),
                    AttendSun = table.Column<bool>(type: "boolean", nullable: true),
                    RegisteredDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TargetDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ActiveStatus = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Child", x => x.ChildId);
                });

            migrationBuilder.CreateTable(
                name: "CommentRecord",
                columns: table => new
                {
                    CommentRecordId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChildId = table.Column<int>(type: "integer", nullable: true),
                    Parent1Id = table.Column<string>(type: "text", nullable: true),
                    Parent2Id = table.Column<string>(type: "text", nullable: true),
                    OrganizationId = table.Column<int>(type: "integer", nullable: true),
                    ChildFirstName = table.Column<string>(type: "text", nullable: true),
                    ChildLastName = table.Column<string>(type: "text", nullable: true),
                    Absence = table.Column<bool>(type: "boolean", nullable: true),
                    Tardy = table.Column<bool>(type: "boolean", nullable: true),
                    Others = table.Column<bool>(type: "boolean", nullable: true),
                    Comment1 = table.Column<string>(type: "text", nullable: true),
                    Comment2 = table.Column<string>(type: "text", nullable: true),
                    Comment3 = table.Column<string>(type: "text", nullable: true),
                    Memo1 = table.Column<string>(type: "text", nullable: true),
                    Memo2 = table.Column<string>(type: "text", nullable: true),
                    Memo3 = table.Column<string>(type: "text", nullable: true),
                    TargetDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    RecordedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentRecord", x => x.CommentRecordId);
                });

            migrationBuilder.CreateTable(
                name: "DeviceToken",
                columns: table => new
                {
                    DeviceTokenId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    Token = table.Column<string>(type: "text", nullable: true),
                    Platform = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceToken", x => x.DeviceTokenId);
                });

            migrationBuilder.CreateTable(
                name: "GrowthRecord",
                columns: table => new
                {
                    GrowthRecordId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChildId = table.Column<int>(type: "integer", nullable: true),
                    Parent1Id = table.Column<string>(type: "text", nullable: true),
                    Parent2Id = table.Column<string>(type: "text", nullable: true),
                    OrganizationId = table.Column<int>(type: "integer", nullable: true),
                    ChildFirstName = table.Column<string>(type: "text", nullable: true),
                    ChildLastName = table.Column<string>(type: "text", nullable: true),
                    HeightCM = table.Column<double>(type: "double precision", nullable: true),
                    WeightKG = table.Column<double>(type: "double precision", nullable: true),
                    RecordDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrowthRecord", x => x.GrowthRecordId);
                });

            migrationBuilder.CreateTable(
                name: "MealRecord",
                columns: table => new
                {
                    MealRecordId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChildId = table.Column<int>(type: "integer", nullable: true),
                    OrganizationId = table.Column<int>(type: "integer", nullable: true),
                    Parent1Id = table.Column<string>(type: "text", nullable: true),
                    Parent2Id = table.Column<string>(type: "text", nullable: true),
                    ChildFirstName = table.Column<string>(type: "text", nullable: true),
                    ChildLastName = table.Column<string>(type: "text", nullable: true),
                    ImagePath = table.Column<string>(type: "text", nullable: true),
                    ImageFileName = table.Column<string>(type: "text", nullable: true),
                    TargetDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndStampTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    MealType = table.Column<string>(type: "text", nullable: true),
                    Quantity = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    VisibleToStaffOnly = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Breakfast = table.Column<bool>(type: "boolean", nullable: true),
                    BreakfastQuantity = table.Column<string>(type: "text", nullable: true),
                    BreakfastDescription = table.Column<string>(type: "text", nullable: true),
                    BreakfastVisibleToStaffOnly = table.Column<bool>(type: "boolean", nullable: true),
                    BreakfastCreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    BreakfastCreatedBy = table.Column<string>(type: "text", nullable: true),
                    AMSnack = table.Column<bool>(type: "boolean", nullable: true),
                    AMSnackQuantity = table.Column<string>(type: "text", nullable: true),
                    AMSnackDescription = table.Column<string>(type: "text", nullable: true),
                    AMSnackVisibleToStaffOnly = table.Column<bool>(type: "boolean", nullable: true),
                    AMSnackCreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    AMSnackCreatedBy = table.Column<string>(type: "text", nullable: true),
                    Lunch = table.Column<bool>(type: "boolean", nullable: true),
                    LunchQuantity = table.Column<string>(type: "text", nullable: true),
                    LunchDescription = table.Column<string>(type: "text", nullable: true),
                    LunchVisibleToStaffOnly = table.Column<bool>(type: "boolean", nullable: true),
                    LunchCreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LunchCreatedBy = table.Column<string>(type: "text", nullable: true),
                    PMSnack = table.Column<bool>(type: "boolean", nullable: true),
                    PMSnackQuantity = table.Column<string>(type: "text", nullable: true),
                    PMSnackDescription = table.Column<string>(type: "text", nullable: true),
                    PMSnackVisibleToStaffOnly = table.Column<bool>(type: "boolean", nullable: true),
                    PMSnackCreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PMSnackCreatedBy = table.Column<string>(type: "text", nullable: true),
                    LateSnack = table.Column<bool>(type: "boolean", nullable: true),
                    LateSnackQuantity = table.Column<string>(type: "text", nullable: true),
                    LateSnackDescription = table.Column<string>(type: "text", nullable: true),
                    LateSnackVisibleToStaffOnly = table.Column<bool>(type: "boolean", nullable: true),
                    LateSnackCreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LateSnackCreatedBy = table.Column<string>(type: "text", nullable: true),
                    Dinner = table.Column<bool>(type: "boolean", nullable: true),
                    DinnerQuantity = table.Column<string>(type: "text", nullable: true),
                    DinnerDescription = table.Column<string>(type: "text", nullable: true),
                    DinnerVisibleToStaffOnly = table.Column<bool>(type: "boolean", nullable: true),
                    DinnerCreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DinnerCreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealRecord", x => x.MealRecordId);
                });

            migrationBuilder.CreateTable(
                name: "NapRecord",
                columns: table => new
                {
                    NapRecordId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChildId = table.Column<int>(type: "integer", nullable: true),
                    OrganizationId = table.Column<int>(type: "integer", nullable: true),
                    Parent1Id = table.Column<string>(type: "text", nullable: true),
                    Parent2Id = table.Column<string>(type: "text", nullable: true),
                    ChildFirstName = table.Column<string>(type: "text", nullable: true),
                    ChildLastName = table.Column<string>(type: "text", nullable: true),
                    ImagePath = table.Column<string>(type: "text", nullable: true),
                    ImageFileName = table.Column<string>(type: "text", nullable: true),
                    TargetDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NapType = table.Column<string>(type: "text", nullable: true),
                    StartStampTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndStampTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    VisibleToStaffOnly = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    FirstStartStampTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FirstEndStampTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FirstDescription = table.Column<string>(type: "text", nullable: true),
                    FirstVisibleToStaffOnly = table.Column<bool>(type: "boolean", nullable: true),
                    FirstCreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FirstCreatedBy = table.Column<string>(type: "text", nullable: true),
                    SecondStartStampTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SecondEndStampTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SecondDescription = table.Column<string>(type: "text", nullable: true),
                    SecondVisibleToStaffOnly = table.Column<bool>(type: "boolean", nullable: true),
                    SecondCreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SecondCreatedBy = table.Column<string>(type: "text", nullable: true),
                    ThirdStartStampTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ThirdEndStampTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ThirdDescription = table.Column<string>(type: "text", nullable: true),
                    ThirdVisibleToStaffOnly = table.Column<bool>(type: "boolean", nullable: true),
                    ThirdCreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ThirdCreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NapRecord", x => x.NapRecordId);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrganizationName = table.Column<string>(type: "text", nullable: true),
                    OrganizationType = table.Column<string>(type: "text", nullable: true),
                    OwnerName = table.Column<string>(type: "text", nullable: true),
                    ContactEmail = table.Column<string>(type: "text", nullable: true),
                    ContactTelNo = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    Street2 = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    Zip = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    Yubin_Bango = table.Column<string>(type: "text", nullable: true),
                    To_Do_Fu_Ken = table.Column<string>(type: "text", nullable: true),
                    Shi_Gun_Ku = table.Column<string>(type: "text", nullable: true),
                    Cho_Son = table.Column<string>(type: "text", nullable: true),
                    Apartment_Etc = table.Column<string>(type: "text", nullable: true),
                    NumberOfChildGroup = table.Column<string>(type: "text", nullable: true),
                    OrganiationCode = table.Column<string>(type: "text", nullable: true),
                    RegisteredDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TargetDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.OrganizationId);
                });

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    PhotoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChildId = table.Column<int>(type: "integer", nullable: true),
                    OrganizationId = table.Column<int>(type: "integer", nullable: true),
                    BlobName = table.Column<string>(type: "text", nullable: true),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    ContentType = table.Column<string>(type: "text", nullable: true),
                    Caption = table.Column<string>(type: "text", nullable: true),
                    UploadedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ActiveStatus = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.PhotoId);
                });

            migrationBuilder.CreateTable(
                name: "PottyRecord",
                columns: table => new
                {
                    PottyRecordId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChildId = table.Column<int>(type: "integer", nullable: true),
                    OrganizationId = table.Column<int>(type: "integer", nullable: true),
                    Parent1Id = table.Column<string>(type: "text", nullable: true),
                    Parent2Id = table.Column<string>(type: "text", nullable: true),
                    ChildFirstName = table.Column<string>(type: "text", nullable: true),
                    ChildLastName = table.Column<string>(type: "text", nullable: true),
                    ImagePath = table.Column<string>(type: "text", nullable: true),
                    ImageFileName = table.Column<string>(type: "text", nullable: true),
                    TargetDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PottyType = table.Column<string>(type: "text", nullable: true),
                    StampTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    VisibleToStaffOnly = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    FirstStampTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FirstDescription = table.Column<string>(type: "text", nullable: true),
                    FirstVisibleToStaffOnly = table.Column<bool>(type: "boolean", nullable: true),
                    FirstCreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FirstCreatedBy = table.Column<string>(type: "text", nullable: true),
                    SecondStampTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SecondDescription = table.Column<string>(type: "text", nullable: true),
                    SecondVisibleToStaffOnly = table.Column<bool>(type: "boolean", nullable: true),
                    SecondCreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SecondCreatedBy = table.Column<string>(type: "text", nullable: true),
                    ThirdStampTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ThirdDescription = table.Column<string>(type: "text", nullable: true),
                    ThirdVisibleToStaffOnly = table.Column<bool>(type: "boolean", nullable: true),
                    ThirdCreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ThirdCreatedBy = table.Column<string>(type: "text", nullable: true),
                    ForthStampTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ForthDescription = table.Column<string>(type: "text", nullable: true),
                    ForthVisibleToStaffOnly = table.Column<bool>(type: "boolean", nullable: true),
                    ForthCreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ForthCreatedBy = table.Column<string>(type: "text", nullable: true),
                    FifthStampTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FifthDescription = table.Column<string>(type: "text", nullable: true),
                    FifthVisibleToStaffOnly = table.Column<bool>(type: "boolean", nullable: true),
                    FifthCreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FifthCreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PottyRecord", x => x.PottyRecordId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropTable(
                name: "AttendanceRecord");

            migrationBuilder.DropTable(
                name: "ChatMessage");

            migrationBuilder.DropTable(
                name: "ChatUser");

            migrationBuilder.DropTable(
                name: "Child");

            migrationBuilder.DropTable(
                name: "CommentRecord");

            migrationBuilder.DropTable(
                name: "DeviceToken");

            migrationBuilder.DropTable(
                name: "GrowthRecord");

            migrationBuilder.DropTable(
                name: "MealRecord");

            migrationBuilder.DropTable(
                name: "NapRecord");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "PottyRecord");
        }
    }
}
