namespace ActiveForum.Database.Entities.ForumEntities
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
   
public class TopicRatingEntity
    {
        public int Id { get; set; }
       
        public int Rating { get; set; }
        
        public int TopicId { get; set; }
    
        public TopicEntity Topic { get; set; }

        public string UserId { get; set; }

        internal static void EntityConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TopicRatingEntity>().ToTable("TopicRatings").HasKey(x => x.Id);

            modelBuilder.Entity<TopicRatingEntity>().Property(t => t.Rating).HasColumnName("Rating").IsRequired().HasColumnType("int");

            modelBuilder.Entity<TopicRatingEntity>().HasOne(x => x.Topic).WithMany(x => x.TopicRatings).HasForeignKey(x => x.TopicId);
        }
    }
}
