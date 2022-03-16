namespace ActiveForum.Database.Entities.ForumEntities
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
   
public class TopicCategoryEntity
    {
        public int CategoryId { get; set; }

        public CategoryEntity Category { get; set; }

        public int TopicId { get; set; }

        public TopicEntity Topic { get; set; }

        internal static void EntityConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TopicCategoryEntity>().ToTable("TopicsCategories");

            modelBuilder.Entity<TopicCategoryEntity>().HasKey(tc => new { tc.CategoryId, tc.TopicId });

            modelBuilder.Entity<TopicCategoryEntity>().HasOne(x => x.Category).WithMany(x => x.TopicsCategories).HasForeignKey(x => x.CategoryId);

            modelBuilder.Entity<TopicCategoryEntity>().HasOne(x => x.Topic).WithMany(x => x.TopicsCategories).HasForeignKey(x => x.TopicId); 
        }
    }
}
