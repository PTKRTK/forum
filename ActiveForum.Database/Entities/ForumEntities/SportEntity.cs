namespace ActiveForum.Database.Entities.ForumEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class SportEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public List<TopicEntity> Topics { get; set; }

        internal static void EntityConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SportEntity>().ToTable("Sports").HasKey(s => s.Id);
         
            modelBuilder.Entity<SportEntity>().Property(s => s.Name).HasColumnName("Name").IsRequired().HasMaxLength(32).HasColumnType("varchar");
            
            modelBuilder.Entity<SportEntity>().Property(s => s.Description).HasColumnName("Description").IsRequired().HasMaxLength(1024).HasColumnType("varchar");
            
            modelBuilder.Entity<SportEntity>().Property(s => s.ImageUrl).HasColumnName("ImageUrl").IsRequired().HasMaxLength(300).HasColumnType("varchar");
        }
    } 
} 