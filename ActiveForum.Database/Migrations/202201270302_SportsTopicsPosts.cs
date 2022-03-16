namespace ActiveForum.Database.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FluentMigrator;

    [Migration(202201270302)]
    public class SportsTopicsPosts : Migration
    {
        public override void Up()
        {
            Create.Table("Sports")
             .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
             .WithColumn("Name").AsString(32).NotNullable()
             .WithColumn("Description").AsString(1024).NotNullable()
             .WithColumn("ImageUrl").AsString(300).NotNullable();

            Create.Table("Topics")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("Title").AsString(1024).NotNullable()
                .WithColumn("TopicContent").AsString(int.MaxValue).NotNullable()
                .WithColumn("CreationDate").AsDateTime().NotNullable()
                .WithColumn("SportId").AsInt32().NotNullable()
                .WithColumn("UserId").AsString().NotNullable();

            Create.Table("Posts")
               .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
               .WithColumn("Status").AsInt32().NotNullable()
               .WithColumn("PostContent").AsString(int.MaxValue).NotNullable()
               .WithColumn("CreationDate").AsDateTime().NotNullable()
               .WithColumn("TopicId").AsInt32().NotNullable()
               .WithColumn("UserId").AsString().NotNullable();

            Create.ForeignKey("FK_Topics_Sport")
                .FromTable("Topics").ForeignColumn("SportId")
                .ToTable("Sports").PrimaryColumn("Id");

            Create.ForeignKey("FK_Posts_Topic")
                .FromTable("Posts").ForeignColumn("TopicId")
                .ToTable("Topics").PrimaryColumn("Id");

            Create.ForeignKey("FK_Topics_User")
               .FromTable("Topics").ForeignColumn("UserId")
               .ToTable("AspNetUsers").PrimaryColumn("Id");

            Create.ForeignKey("FK_Posts_User")
              .FromTable("Posts").ForeignColumn("UserId")
              .ToTable("AspNetUsers").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_Posts_User").OnTable("Posts");
            Delete.ForeignKey("FK_Posts_Topic").OnTable("Posts");
            Delete.ForeignKey("FK_Topics_User").OnTable("Topics");
            Delete.ForeignKey("FK_Topics_Sport").OnTable("Topics");
            Delete.Table("Posts");
            Delete.Table("Topics");
            Delete.Table("Sports");
        }
    }
}
