namespace ActiveForum.Database.Migrations
{
    using System;
    using System.Collections.Generic;
    using ActiveForum.Database.Entities;
    using ActiveForum.Database.Entities.ForumEntities;
    using FluentMigrator;
    using FluentMigrator.SqlServer;

    [Migration(202202071646)]
    public class Admin : Migration
    {
        public override void Up()
        {
            var admin = new AspNetUser()
            {
                Id = "51153673-1359-4b89-b83b-827149688bf9",
                AccessFailedCount = 0,
                ConcurrencyStamp = "2bb485eb-ab6f-451d-ba41-f0f14459b9fe",
                Email = "admin@mail.pl",
                EmailConfirmed = false,
                LockoutEnabled = true,
                NormalizedEmail = "ADMIN@MAIL.PL",
                NormalizedUserName = "ADMIN@MAIL.PL",
                PasswordHash = "AQAAAAEAACcQAAAAEFkkaslidPICpcJHW+oUQHvWBxpSB9Alb4/AwGBSatw+wF5FPMnY3jebqp38nQskCw==",
                PhoneNumberConfirmed = false,
                SecurityStamp = "T7ODGSQ5SJV426Y3VWR462466BXEMQDB",
                TwoFactorEnabled = false,
                UserName = "admin@mail.pl"
            };

            Insert.IntoTable("AspNetUsers").Row(new
            {
                admin.Id,
                admin.AccessFailedCount,
                admin.ConcurrencyStamp,
                admin.Email,
                admin.EmailConfirmed,
                admin.LockoutEnabled,
                admin.NormalizedEmail,
                admin.NormalizedUserName,
                admin.PasswordHash,
                admin.PhoneNumberConfirmed,
                admin.SecurityStamp,
                admin.TwoFactorEnabled,
                admin.UserName
            });

            var adminClaim = new AspUserClaim()
            {
                Id = 1,
                ClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
                ClaimValue = "Owner",
                UserId = "51153673-1359-4b89-b83b-827149688bf9"
            };

            Insert.IntoTable("AspNetUserClaims").WithIdentityInsert().Row(new
             {
                adminClaim.Id,
                adminClaim.ClaimType,
                adminClaim.ClaimValue,
                adminClaim.UserId
             });
        }

        public override void Down()
        {
            Delete.FromTable("AspNetUsers").AllRows();
            Delete.FromTable("AspNetUserClaims").AllRows();
        }
    }
}
