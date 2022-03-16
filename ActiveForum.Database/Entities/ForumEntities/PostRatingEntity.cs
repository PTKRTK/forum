namespace ActiveForum.Database.Entities.ForumEntities
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class PostRatingEntity
    {
        public int Id { get; set; }

        public int Rating { get; set; }

        public int PostId { get; set; }

        public PostEntity Post { get; set; }

        public string UserId { get; set; }

        internal static void EntityConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostRatingEntity>().ToTable("PostRatings").HasKey(x => x.Id);

            modelBuilder.Entity<PostRatingEntity>().Property(t => t.Rating).HasColumnName("Rating").IsRequired().HasColumnType("int");

            modelBuilder.Entity<PostRatingEntity>().HasOne(x => x.Post).WithMany(x => x.PostRatings).HasForeignKey(x => x.PostId);
        }
    }
}
