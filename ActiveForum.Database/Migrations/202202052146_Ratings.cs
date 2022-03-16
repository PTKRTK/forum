namespace ActiveForum.Database.Migrations
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

[Migration(202202052146)]
public class Ratings : Migration
    {
        public override void Up()
        {
            Create.Table("TopicRatings")
            .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("Rating").AsInt32().NotNullable()
            .WithColumn("TopicId").AsInt32().NotNullable()
            .WithColumn("UserId").AsString().NotNullable();

            Create.ForeignKey("FK_TopicRatings_Topic")
            .FromTable("TopicRatings").ForeignColumn("TopicId")
            .ToTable("Topics").PrimaryColumn("Id");
            
            Create.ForeignKey("FK_TopicRatings_User")
            .FromTable("TopicRatings").ForeignColumn("UserId")
            .ToTable("AspNetUsers").PrimaryColumn("Id");

            Create.Table("PostRatings")
            .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("Rating").AsInt32().NotNullable()
            .WithColumn("PostId").AsInt32().NotNullable()
            .WithColumn("UserId").AsString().NotNullable();

            Create.ForeignKey("FK_PostRatings_Post")
            .FromTable("PostRatings").ForeignColumn("PostId")
            .ToTable("Posts").PrimaryColumn("Id");
           
            Create.ForeignKey("FK_PostRatings_User")
           .FromTable("PostRatings").ForeignColumn("UserId")
           .ToTable("AspNetUsers").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_PostRatings_Post").OnTable("Posts");
            Delete.ForeignKey("FK_PostRatings_User").OnTable("Posts");
            Delete.ForeignKey("FK_TopicRatings_Topic").OnTable("Topics");
            Delete.ForeignKey("FK_TopicRatings_User").OnTable("Topics");
            Delete.Table("PostRatings");
            Delete.Table("TopicRatings");
        }
    }
}
