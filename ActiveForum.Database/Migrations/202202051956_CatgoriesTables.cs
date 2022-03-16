namespace ActiveForum.Database.Migrations
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;
  
[Migration(202202051956)]
public class CatgoriesTables : Migration
    {
        public override void Up()
        {
            Create.Table("Categories")
             .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
             .WithColumn("Name").AsString(200).NotNullable()
             .WithColumn("Description").AsString(400).NotNullable();

            Create.Table("TopicsCategories")
                           .WithColumn("TopicId").AsInt32().NotNullable()
                           .WithColumn("CategoryId").AsInt32().NotNullable();
           
            Create.ForeignKey("FK_TCs_Category")
                    .FromTable("TopicsCategories").ForeignColumn("CategoryId")
                        .ToTable("Categories").PrimaryColumn("Id");
           
            Create.ForeignKey("FK_TCs_Topic")
                  .FromTable("TopicsCategories").ForeignColumn("TopicId")
                      .ToTable("Topics").PrimaryColumn("Id");
        }
       
        public override void Down()
        {
            Delete.ForeignKey("FK_TCs_Category").OnTable("Categories");
            Delete.ForeignKey("FK_TCs_Topic").OnTable("Topics");
            Delete.Table("TopicsCategories");
            Delete.Table("Categories");
        }
    }
}