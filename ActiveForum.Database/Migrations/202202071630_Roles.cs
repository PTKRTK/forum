namespace ActiveForum.Database.Migrations
{
    using System;
    using System.Collections.Generic;
    using ActiveForum.Database.Entities;
    using ActiveForum.Database.Entities.ForumEntities;
    using FluentMigrator;
    using FluentMigrator.SqlServer;

    [Migration(202202071630)]
    public class Roles : Migration
    {
        public override void Up()
        {
            var roles = new List<AspNetRole>()
            {
                new AspNetRole()
                {
                        Id = "78c55dd6-7b14-4098-8a51-6bdba05670ac",
                        ConcurrencyStamp = "OWNER_1",
                        Name = "Owner",
                        NormalizedName = "OWNER"
                },
                   
                new AspNetRole()
                {
                        Id = "f24c879d-a491-4aa6-91c9-cc4c72899ad0 ",
                        ConcurrencyStamp = "ADMIN_1",
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                },

                new AspNetRole()
                {
                    Id = "522ec306-7bde-4d6e-a877-e1d09cdd05e6",
                    ConcurrencyStamp = "MODERATOR_1",
                    Name = "Moderator",
                    NormalizedName = "MODERATOR"
                },

                new AspNetRole()
                {
                    Id = "283ae2a7-9f51-4c26-a1ad-c0ba8e30e8fd",
                    ConcurrencyStamp = "USER_1",
                    Name = "User",
                    NormalizedName = "USER"
                },       
            };

            roles.ForEach(x => Insert.IntoTable("AspNetRoles").Row(new { x.Id, x.ConcurrencyStamp, x.Name, x.NormalizedName }));
        }

        public override void Down()
        {
            Delete.FromTable("AspNetRoles").AllRows();
        }
    }
}
