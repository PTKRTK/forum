namespace ActiveForum.Database.Entities.ForumEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class CategoryEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<TopicCategoryEntity> TopicsCategories { get; set; }

        internal static void EntityConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryEntity>().ToTable("Categories").HasKey(t => t.Id);
           
            modelBuilder.Entity<CategoryEntity>().Property(c => c.Name).HasColumnName("Name").IsRequired().HasMaxLength(200).HasColumnType("varchar");
            
            modelBuilder.Entity<CategoryEntity>().Property(c => c.Description).HasColumnName("Description").IsRequired().HasMaxLength(400).HasColumnType("varchar");
        }
    }
}
