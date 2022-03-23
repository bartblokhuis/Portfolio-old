﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Portfolio.Database;

#nullable disable

namespace Portfolio.Database.Migrations.Portfolio
{
    [DbContext(typeof(PortfolioContext))]
    partial class PortfolioContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Portfolio.Domain.Models.AboutMe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("AboutMes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "",
                            Title = "Hi, welcome on my portfolio"
                        });
                });

            modelBuilder.Entity("Portfolio.Domain.Models.Blogs.BlogPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BannerPictureId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAtUTC")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DisplayNumber")
                        .HasColumnType("int");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("bit");

                    b.Property<string>("MetaDescription")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("MetaTitle")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int?>("ThumbnailId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime>("UpdatedAtUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BannerPictureId");

                    b.HasIndex("ThumbnailId");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("BlogPosts");
                });

            modelBuilder.Entity("Portfolio.Domain.Models.Blogs.BlogSubscriber", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAtUTC")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime>("UpdatedAtUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmailAddress")
                        .IsUnique();

                    b.ToTable("BlogSubscribers");
                });

            modelBuilder.Entity("Portfolio.Domain.Models.Blogs.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BlogPostId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<DateTime>("CreatedAtUTC")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<bool>("IsAuthor")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<int?>("ParentCommentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAtUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BlogPostId");

                    b.HasIndex("ParentCommentId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Portfolio.Domain.Models.Localization.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DisplayNumber")
                        .HasColumnType("int");

                    b.Property<string>("FlagImageFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("bit");

                    b.Property<string>("LanguageCulture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Languages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayNumber = 0,
                            FlagImageFilePath = "languages/united-33135.svg",
                            IsPublished = true,
                            LanguageCulture = "en-US",
                            Name = "English"
                        },
                        new
                        {
                            Id = 2,
                            DisplayNumber = 1,
                            FlagImageFilePath = "languages/netherlands-33035.svg",
                            IsPublished = false,
                            LanguageCulture = "nl-NL",
                            Name = "Dutch"
                        });
                });

            modelBuilder.Entity("Portfolio.Domain.Models.Localization.LocaleStringResource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Area")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<string>("Page")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourceValue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("LocaleStringResources");
                });

            modelBuilder.Entity("Portfolio.Domain.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAtUTC")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<bool>("HasSentNotification")
                        .HasColumnType("bit");

                    b.Property<string>("IpAddress")
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("MessageContent")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<int>("MessageStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAtUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Messages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAtUTC = new DateTime(2022, 2, 7, 22, 56, 28, 650, DateTimeKind.Utc).AddTicks(8235),
                            Email = "info@bartblokhuis.com",
                            FirstName = "Bart",
                            HasSentNotification = false,
                            IsDeleted = false,
                            MessageContent = "This is an example message",
                            MessageStatus = 0,
                            UpdatedAtUtc = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Portfolio.Domain.Models.Picture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AltAttribute")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("MimeType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("TitleAttribute")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("Portfolio.Domain.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<int>("DisplayNumber")
                        .HasColumnType("int");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "This is an example project",
                            DisplayNumber = 0,
                            IsPublished = true,
                            Title = "Example Project"
                        });
                });

            modelBuilder.Entity("Portfolio.Domain.Models.ProjectPicture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DisplayNumber")
                        .HasColumnType("int");

                    b.Property<int>("PictureId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PictureId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectPictures");
                });

            modelBuilder.Entity("Portfolio.Domain.Models.ProjectUrls", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("UrlId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UrlId");

                    b.ToTable("ProjectUrls");
                });

            modelBuilder.Entity("Portfolio.Domain.Models.QueuedEmail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAtUTC")
                        .HasColumnType("datetime2");

                    b.Property<string>("From")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("FromName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("SentOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("SentTries")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("To")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ToName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("QueuedEmails");
                });

            modelBuilder.Entity("Portfolio.Domain.Models.ScheduleTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastEndUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastStartUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastSuccessUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("Seconds")
                        .HasColumnType("int");

                    b.Property<bool>("StopOnError")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Type")
                        .IsUnique();

                    b.ToTable("ScheduleTasks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Enabled = true,
                            Name = "Keep alive",
                            Seconds = 300,
                            StopOnError = false,
                            Type = "Portfolio.Services.Common.KeepAliveTask, Portfolio.Services"
                        },
                        new
                        {
                            Id = 2,
                            Enabled = true,
                            Name = "Send queued emails",
                            Seconds = 30,
                            StopOnError = false,
                            Type = "Portfolio.Services.Common.QueuedMessagesSendTask, Portfolio.Services"
                        });
                });

            modelBuilder.Entity("Portfolio.Domain.Models.Settings.ApiSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ApiUrl")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("ApiSettings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ApiUrl = "http://localhost:44301"
                        });
                });

            modelBuilder.Entity("Portfolio.Domain.Models.Settings.BlogSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("EmailOnCommentReplySubjectTemplate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailOnCommentReplyTemplate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailOnPublishingSubjectTemplate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailOnPublishingTemplate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailOnSubscribingSubjectTemplate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailOnSubscribingTemplate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSendEmailOnCommentReply")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSendEmailOnPublishing")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSendEmailOnSubscribing")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("BlogSettings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EmailOnCommentReplySubjectTemplate = "%Comment.Name% replied to your comment",
                            EmailOnCommentReplyTemplate = "%Comment.Name% replied with:' %Comment.MessageContent%', to the comment that you left on the blog post <a href=\"%BlogPost.Url\">%BlogPost.Title%</a>",
                            EmailOnPublishingSubjectTemplate = "%BlogPost.Title% just got released!",
                            EmailOnPublishingTemplate = "My new blog post titled: %BlogPost.Title% just got released visit this link to read it. %BlogPost.Url%",
                            EmailOnSubscribingSubjectTemplate = "Thanks for subscribing to my blog!",
                            EmailOnSubscribingTemplate = "Thanks for subscribing to my blog, you will now receive an email every time i publish a new blog, if you ever wish to unsubscribe click this link: %BlogSubscriber.UnsubscribeURL%",
                            IsSendEmailOnCommentReply = true,
                            IsSendEmailOnPublishing = true,
                            IsSendEmailOnSubscribing = true
                        });
                });

            modelBuilder.Entity("Portfolio.Domain.Models.Settings.EmailSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<bool>("EnableSsl")
                        .HasColumnType("bit");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.Property<string>("SendTestEmailTo")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("SiteOwnerEmailAddress")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<bool>("UseDefaultCredentials")
                        .HasColumnType("bit");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EmailSettings");
                });

            modelBuilder.Entity("Portfolio.Domain.Models.Settings.GeneralSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CallToActionText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FooterText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("FooterTextBetweenCopyRightAndYear")
                        .HasColumnType("bit");

                    b.Property<string>("GithubUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LandingDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LandingTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LinkedInUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ShowContactMeForm")
                        .HasColumnType("bit");

                    b.Property<bool>("ShowCopyRightInFooter")
                        .HasColumnType("bit");

                    b.Property<string>("StackOverFlowUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GeneralSettings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CallToActionText = "About Me",
                            FooterText = "My name",
                            FooterTextBetweenCopyRightAndYear = true,
                            LandingDescription = "Welcome on my portfolio website",
                            LandingTitle = "Welcome!",
                            ShowContactMeForm = true,
                            ShowCopyRightInFooter = true
                        });
                });

            modelBuilder.Entity("Portfolio.Domain.Models.Settings.MessageSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ConfirmationEmailSubjectTemplate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConfirmationEmailTemplate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSendConfirmationEmail")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSendSiteOwnerEmail")
                        .HasColumnType("bit");

                    b.Property<string>("SiteOwnerSubjectTemplate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SiteOwnerTemplate")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MessageSettings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConfirmationEmailSubjectTemplate = "Thanks for sending me a message!",
                            ConfirmationEmailTemplate = "Thanks for reaching out %Message.Name%, I received your message and will respond as soon as possible",
                            IsSendConfirmationEmail = true,
                            IsSendSiteOwnerEmail = true,
                            SiteOwnerSubjectTemplate = "Someone send you a message",
                            SiteOwnerTemplate = "%Message.Name%, has sent you the following message: %Message.MessageContent%"
                        });
                });

            modelBuilder.Entity("Portfolio.Domain.Models.Settings.PublicSiteSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("PublicSiteUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PublicSiteSettings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PublicSiteUrl = "http://localhost:4200"
                        });
                });

            modelBuilder.Entity("Portfolio.Domain.Models.Settings.SeoSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DefaultMetaDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DefaultMetaKeywords")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<bool>("UseOpenGraphMetaTags")
                        .HasColumnType("bit");

                    b.Property<bool>("UseTwitterMetaTags")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("SeoSettings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "My portfolio",
                            UseOpenGraphMetaTags = false,
                            UseTwitterMetaTags = false
                        });
                });

            modelBuilder.Entity("Portfolio.Domain.Models.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DisplayNumber")
                        .HasColumnType("int");

                    b.Property<string>("IconPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<int>("SkillGroupId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SkillGroupId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("Portfolio.Domain.Models.SkillGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DisplayNumber")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("SkillGroups");
                });

            modelBuilder.Entity("Portfolio.Domain.Models.Url", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FriendlyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullUrl")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Urls");
                });

            modelBuilder.Entity("ProjectSkill", b =>
                {
                    b.Property<int>("ProjectsId")
                        .HasColumnType("int");

                    b.Property<int>("SkillsId")
                        .HasColumnType("int");

                    b.HasKey("ProjectsId", "SkillsId");

                    b.HasIndex("SkillsId");

                    b.ToTable("ProjectSkill");
                });

            modelBuilder.Entity("Portfolio.Domain.Models.Blogs.BlogPost", b =>
                {
                    b.HasOne("Portfolio.Domain.Models.Picture", "BannerPicture")
                        .WithMany()
                        .HasForeignKey("BannerPictureId");

                    b.HasOne("Portfolio.Domain.Models.Picture", "Thumbnail")
                        .WithMany()
                        .HasForeignKey("ThumbnailId");

                    b.Navigation("BannerPicture");

                    b.Navigation("Thumbnail");
                });

            modelBuilder.Entity("Portfolio.Domain.Models.Blogs.Comment", b =>
                {
                    b.HasOne("Portfolio.Domain.Models.Blogs.BlogPost", "BlogPost")
                        .WithMany("Comments")
                        .HasForeignKey("BlogPostId");

                    b.HasOne("Portfolio.Domain.Models.Blogs.Comment", "ParentComment")
                        .WithMany("Comments")
                        .HasForeignKey("ParentCommentId");

                    b.Navigation("BlogPost");

                    b.Navigation("ParentComment");
                });

            modelBuilder.Entity("Portfolio.Domain.Models.Localization.LocaleStringResource", b =>
                {
                    b.HasOne("Portfolio.Domain.Models.Localization.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");
                });

            modelBuilder.Entity("Portfolio.Domain.Models.ProjectPicture", b =>
                {
                    b.HasOne("Portfolio.Domain.Models.Picture", "Picture")
                        .WithMany()
                        .HasForeignKey("PictureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Portfolio.Domain.Models.Project", "Project")
                        .WithMany("ProjectPictures")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Picture");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Portfolio.Domain.Models.ProjectUrls", b =>
                {
                    b.HasOne("Portfolio.Domain.Models.Project", "Project")
                        .WithMany("ProjectUrls")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Portfolio.Domain.Models.Url", "Url")
                        .WithMany()
                        .HasForeignKey("UrlId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Url");
                });

            modelBuilder.Entity("Portfolio.Domain.Models.Skill", b =>
                {
                    b.HasOne("Portfolio.Domain.Models.SkillGroup", "SkillGroup")
                        .WithMany("Skills")
                        .HasForeignKey("SkillGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SkillGroup");
                });

            modelBuilder.Entity("ProjectSkill", b =>
                {
                    b.HasOne("Portfolio.Domain.Models.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Portfolio.Domain.Models.Skill", null)
                        .WithMany()
                        .HasForeignKey("SkillsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Portfolio.Domain.Models.Blogs.BlogPost", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("Portfolio.Domain.Models.Blogs.Comment", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("Portfolio.Domain.Models.Project", b =>
                {
                    b.Navigation("ProjectPictures");

                    b.Navigation("ProjectUrls");
                });

            modelBuilder.Entity("Portfolio.Domain.Models.SkillGroup", b =>
                {
                    b.Navigation("Skills");
                });
#pragma warning restore 612, 618
        }
    }
}
