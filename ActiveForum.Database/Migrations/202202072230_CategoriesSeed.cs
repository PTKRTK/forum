namespace ActiveForum.Database.Migrations
{
    using System;
    using System.Collections.Generic;
    using ActiveForum.Database.Entities.ForumEntities;
    using FluentMigrator;
    using FluentMigrator.SqlServer;

    [Migration(202202072230)]
    public class CategoriesSeed : Migration
    {
        public override void Up()
        {
            var categories = new List<CategoryEntity>
            {
                new CategoryEntity()
                {
                    Id = 1,
                    Name = "Technika",
                    Description = "W jaki sposób ćwiczyć. Na przykład w jaki sposób wykonywać ruch, kiedy robić wdech i wydech. Prawidłowa technika pozwala maksymalizować efekty i unikać kontuzji",       
                },
                new CategoryEntity()
                {
                    Id = 2,
                    Name = "Żywienie/Diety",
                    Description = "Co jeść i w jakich ilościach.",
                },
                new CategoryEntity()
                {
                    Id = 3,
                    Name = "Sprzęt",
                    Description = "Wszystkie zagadnienia odnośnie sprzętu",
                },
                new CategoryEntity()
                {
                    Id = 4,
                    Name = "Przedterningówki",
                    Description = "O przedtreningówkach",
                },
                new CategoryEntity()
                {
                    Id = 5,
                    Name = "Plany treningowe",
                    Description = "Rozpisane plany treningów",
                },
            };

            categories.ForEach(x => Insert.IntoTable("Categories").WithIdentityInsert().Row(new { x.Id, x.Name, x.Description }));

            var topicCategories = new List<TopicCategoryEntity>
            {
                new TopicCategoryEntity()
                {
                    TopicId = 1,
                    CategoryId = 2
                },

                new TopicCategoryEntity()
                {
                    TopicId = 2,
                    CategoryId = 1
                },

                new TopicCategoryEntity()
                {
                    TopicId = 3,
                    CategoryId = 1
                },

                new TopicCategoryEntity()
                {
                    TopicId = 2,
                    CategoryId = 5,
                }
            };

            // topicCategories.ForEach(x => Insert.IntoTable("Topic_Categories").Row(new { x.CategoryOfTopicId, x.TopicId }));
        }

        public override void Down()
        {
            Delete.FromTable("TopicsCategories").AllRows();
            Delete.FromTable("Categories").AllRows();
        }
    }
}
