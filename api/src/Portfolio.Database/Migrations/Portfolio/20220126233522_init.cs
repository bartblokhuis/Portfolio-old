﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Database.Migrations.Portfolio
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutMes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutMes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApiSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiUrl = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsSendEmailOnSubscribing = table.Column<bool>(type: "bit", nullable: false),
                    EmailOnSubscribingSubjectTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailOnSubscribingTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSendEmailOnPublishing = table.Column<bool>(type: "bit", nullable: false),
                    EmailOnPublishingSubjectTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailOnPublishingTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSendEmailOnCommentReply = table.Column<bool>(type: "bit", nullable: false),
                    EmailOnCommentReplySubjectTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailOnCommentReplyTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogSubscribers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CreatedAtUTC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogSubscribers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Host = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnableSsl = table.Column<bool>(type: "bit", nullable: false),
                    UseDefaultCredentials = table.Column<bool>(type: "bit", nullable: false),
                    SendTestEmailTo = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    SiteOwnerEmailAddress = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneralSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LandingTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LandingDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CallToActionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkedInUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GithubUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StackOverFlowUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FooterText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShowCopyRightInFooter = table.Column<bool>(type: "bit", nullable: false),
                    FooterTextBetweenCopyRightAndYear = table.Column<bool>(type: "bit", nullable: false),
                    ShowContactMeForm = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    MessageContent = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    MessageStatus = table.Column<int>(type: "int", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    HasSentNotification = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAtUTC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsSendSiteOwnerEmail = table.Column<bool>(type: "bit", nullable: false),
                    SiteOwnerSubjectTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteOwnerTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSendConfirmationEmail = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmationEmailSubjectTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmationEmailTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AltAttribute = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    TitleAttribute = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    DisplayNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublicSiteSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicSiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicSiteSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QueuedEmails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    FromName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    To = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ToName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAtUTC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SentTries = table.Column<int>(type: "int", nullable: false),
                    SentOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueuedEmails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Seconds = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    StopOnError = table.Column<bool>(type: "bit", nullable: false),
                    LastStartUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastEndUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastSuccessUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeoSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    DefaultMetaKeywords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultMetaDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UseTwitterMetaTags = table.Column<bool>(type: "bit", nullable: false),
                    UseOpenGraphMetaTags = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeoSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkillGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    DisplayNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Urls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullUrl = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    FriendlyName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    DisplayNumber = table.Column<int>(type: "int", nullable: false),
                    MetaTitle = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedAtUTC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThumbnailId = table.Column<int>(type: "int", nullable: true),
                    BannerPictureId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPosts_Pictures_BannerPictureId",
                        column: x => x.BannerPictureId,
                        principalTable: "Pictures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BlogPosts_Pictures_ThumbnailId",
                        column: x => x.ThumbnailId,
                        principalTable: "Pictures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectPictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayNumber = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    PictureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectPictures_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectPictures_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    IconPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayNumber = table.Column<int>(type: "int", nullable: false),
                    SkillGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_SkillGroups_SkillGroupId",
                        column: x => x.SkillGroupId,
                        principalTable: "SkillGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectUrls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    UrlId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUrls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectUrls_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUrls_Urls_UrlId",
                        column: x => x.UrlId,
                        principalTable: "Urls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    IsAuthor = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAtUTC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BlogPostId = table.Column<int>(type: "int", nullable: true),
                    ParentCommentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "Comments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectSkill",
                columns: table => new
                {
                    ProjectsId = table.Column<int>(type: "int", nullable: false),
                    SkillsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSkill", x => new { x.ProjectsId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_ProjectSkill_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectSkill_Skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AboutMes",
                columns: new[] { "Id", "Content", "Title" },
                values: new object[] { 1, "", "Hi, welcome on my portfolio" });

            migrationBuilder.InsertData(
                table: "ApiSettings",
                columns: new[] { "Id", "ApiUrl" },
                values: new object[] { 1, "http://localhost:44301" });

            migrationBuilder.InsertData(
                table: "BlogSettings",
                columns: new[] { "Id", "EmailOnCommentReplySubjectTemplate", "EmailOnCommentReplyTemplate", "EmailOnPublishingSubjectTemplate", "EmailOnPublishingTemplate", "EmailOnSubscribingSubjectTemplate", "EmailOnSubscribingTemplate", "IsSendEmailOnCommentReply", "IsSendEmailOnPublishing", "IsSendEmailOnSubscribing" },
                values: new object[] { 1, "%Comment.Name% replied to your comment", "%Comment.Name% replied with:' %Comment.MessageContent%', to the comment that you left on the blog post <a href=\"%BlogPost.Url\">%BlogPost.Title%</a>", "%BlogPost.Title% just got released!", "My new blog post titled: %BlogPost.Title% just got released visit this link to read it. %BlogPost.Url%", "Thanks for subscribing to my blog!", "Thanks for subscribing to my blog, you will now receive an email every time i publish a new blog, if you ever wish to unsubscribe click this link: %BlogSubscriber.UnsubscribeURL%", true, true, true });

            migrationBuilder.InsertData(
                table: "GeneralSettings",
                columns: new[] { "Id", "CallToActionText", "FooterText", "FooterTextBetweenCopyRightAndYear", "GithubUrl", "LandingDescription", "LandingTitle", "LinkedInUrl", "ShowContactMeForm", "ShowCopyRightInFooter", "StackOverFlowUrl" },
                values: new object[] { 1, "About Me", "My name", true, null, "Welcome on my portfolio website", "Welcome!", null, true, true, null });

            migrationBuilder.InsertData(
                table: "MessageSettings",
                columns: new[] { "Id", "ConfirmationEmailSubjectTemplate", "ConfirmationEmailTemplate", "IsSendConfirmationEmail", "IsSendSiteOwnerEmail", "SiteOwnerSubjectTemplate", "SiteOwnerTemplate" },
                values: new object[] { 1, "Thanks for sending me a message!", "Thanks for reaching out %Message.Name%, I received your message and will respond as soon as possible", true, true, "Someone send you a message", "%Message.Name%, has sent you the following message: %Message.MessageContent%" });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "CreatedAtUTC", "Email", "FirstName", "HasSentNotification", "IpAddress", "IsDeleted", "LastName", "MessageContent", "MessageStatus", "UpdatedAtUtc" },
                values: new object[] { 1, new DateTime(2022, 1, 26, 23, 35, 22, 41, DateTimeKind.Utc).AddTicks(5792), "info@bartblokhuis.com", "Bart", false, null, false, null, "This is an example message", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Description", "DisplayNumber", "IsPublished", "Title" },
                values: new object[] { 1, "This is an example project", 0, true, "Example Project" });

            migrationBuilder.InsertData(
                table: "PublicSiteSettings",
                columns: new[] { "Id", "PublicSiteUrl" },
                values: new object[] { 1, "http://localhost:4200" });

            migrationBuilder.InsertData(
                table: "ScheduleTasks",
                columns: new[] { "Id", "Enabled", "LastEndUtc", "LastStartUtc", "LastSuccessUtc", "Name", "Seconds", "StopOnError", "Type" },
                values: new object[,]
                {
                    { 1, true, null, null, null, "Keep alive", 300, false, "Portfolio.Services.Common.KeepAliveTask, Portfolio.Services" },
                    { 2, true, null, null, null, "Send queued emails", 30, false, "Portfolio.Services.Common.QueuedMessagesSendTask, Portfolio.Services" }
                });

            migrationBuilder.InsertData(
                table: "SeoSettings",
                columns: new[] { "Id", "DefaultMetaDescription", "DefaultMetaKeywords", "Title", "UseOpenGraphMetaTags", "UseTwitterMetaTags" },
                values: new object[] { 1, null, null, "My portfolio", false, false });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_BannerPictureId",
                table: "BlogPosts",
                column: "BannerPictureId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_ThumbnailId",
                table: "BlogPosts",
                column: "ThumbnailId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_Title",
                table: "BlogPosts",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogSubscribers_EmailAddress",
                table: "BlogSubscribers",
                column: "EmailAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BlogPostId",
                table: "Comments",
                column: "BlogPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentCommentId",
                table: "Comments",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPictures_PictureId",
                table: "ProjectPictures",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPictures_ProjectId",
                table: "ProjectPictures",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Title",
                table: "Projects",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSkill_SkillsId",
                table: "ProjectSkill",
                column: "SkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUrls_ProjectId",
                table: "ProjectUrls",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUrls_UrlId",
                table: "ProjectUrls",
                column: "UrlId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleTasks_Type",
                table: "ScheduleTasks",
                column: "Type",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SkillGroups_Title",
                table: "SkillGroups",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_SkillGroupId",
                table: "Skills",
                column: "SkillGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutMes");

            migrationBuilder.DropTable(
                name: "ApiSettings");

            migrationBuilder.DropTable(
                name: "BlogSettings");

            migrationBuilder.DropTable(
                name: "BlogSubscribers");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "EmailSettings");

            migrationBuilder.DropTable(
                name: "GeneralSettings");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "MessageSettings");

            migrationBuilder.DropTable(
                name: "ProjectPictures");

            migrationBuilder.DropTable(
                name: "ProjectSkill");

            migrationBuilder.DropTable(
                name: "ProjectUrls");

            migrationBuilder.DropTable(
                name: "PublicSiteSettings");

            migrationBuilder.DropTable(
                name: "QueuedEmails");

            migrationBuilder.DropTable(
                name: "ScheduleTasks");

            migrationBuilder.DropTable(
                name: "SeoSettings");

            migrationBuilder.DropTable(
                name: "BlogPosts");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Urls");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "SkillGroups");
        }
    }
}
