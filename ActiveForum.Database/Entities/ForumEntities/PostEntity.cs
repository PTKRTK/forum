namespace ActiveForum.Database.Entities.ForumEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class PostEntity
    {
        public int Id { get; set; }

        public int Status { get; set; }

        public string PostContent { get; set; }

        public DateTime CreationDate { get; set; }

        public int TopicId { get; set; }
       
        public TopicEntity Topic { get; set; }

        public string UserId { get; set; }

        public List<PostRatingEntity> PostRatings { get; set; }

        internal static void EntityConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostEntity>().ToTable("Posts").HasKey(p => p.Id);
         
            modelBuilder.Entity<PostEntity>().Property(t => t.Status).HasColumnName("Status").IsRequired().HasColumnType("int");
          
            modelBuilder.Entity<PostEntity>().Property(t => t.PostContent).HasColumnName("PostContent").IsRequired().HasMaxLength(int.MaxValue).HasColumnType("varchar");
           
            modelBuilder.Entity<PostEntity>().Property(t => t.CreationDate).HasColumnName("CreationDate").IsRequired().HasColumnType("datetime");
         
            modelBuilder.Entity<PostEntity>().HasOne(x => x.Topic).WithMany(x => x.Posts).HasForeignKey(x => x.TopicId);
        }
   }
}
