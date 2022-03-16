namespace ActiveForum.Database.Entities.ForumEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class TopicEntity
    {
        public int Id { get; set; }
       
        public int Status { get; set; }

        public string Title { get; set; }

        public string TopicContent { get; set; }

        public DateTime CreationDate { get; set; }

        public int SportId { get; set; }

        public SportEntity Sport { get; set; }

        public string UserId { get; set; }

        public List<PostEntity> Posts { get; set; }

        public List<TopicCategoryEntity> TopicsCategories { get; set; }

        public List<TopicRatingEntity> TopicRatings { get; set; }

        internal static void EntityConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TopicEntity>().ToTable("Topics").HasKey(t => t.Id);
           
            modelBuilder.Entity<TopicEntity>().Property(t => t.Status).HasColumnName("Status").IsRequired().HasColumnType("int");
           
            modelBuilder.Entity<TopicEntity>().Property(t => t.Title).HasColumnName("Title").IsRequired().HasMaxLength(1024).HasColumnType("varchar");
           
            modelBuilder.Entity<TopicEntity>().Property(t => t.TopicContent).HasColumnName("TopicContent").IsRequired().HasMaxLength(int.MaxValue).HasColumnType("varchar");
          
            modelBuilder.Entity<TopicEntity>().Property(t => t.CreationDate).HasColumnName("CreationDate").IsRequired().HasColumnType("datetime");

            modelBuilder.Entity<TopicEntity>().HasOne(x => x.Sport).WithMany(x => x.Topics).HasForeignKey(x => x.SportId);
        }
    }
}
