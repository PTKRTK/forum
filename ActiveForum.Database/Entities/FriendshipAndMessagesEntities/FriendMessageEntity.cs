namespace ActiveForum.Database.Entities.FriendshipAndMessagesEntities
{
    using System;
    using Microsoft.EntityFrameworkCore;
    
    public class FriendMessageEntity
    {
        public int Id { get; set; }

        public string RecipientEmail { get; set; }

        public string SenderEmail { get; set; }

        public string Message { get; set; }

        public DateTime CreationDate { get; set; }

        public int FriendshipId { get; set; }

        public FriendshipEntity Friendship { get; set; }

        internal static void EntityConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FriendMessageEntity>().ToTable("FriendMessages").HasKey(p => p.Id);

            modelBuilder.Entity<FriendMessageEntity>().Property(t => t.RecipientEmail).HasColumnName("RecipientEmail").IsRequired().HasMaxLength(int.MaxValue).HasColumnType("varchar");

            modelBuilder.Entity<FriendMessageEntity>().Property(t => t.SenderEmail).HasColumnName("SenderEmail").IsRequired().HasMaxLength(int.MaxValue).HasColumnType("varchar");

            modelBuilder.Entity<FriendMessageEntity>().Property(t => t.Message).HasColumnName("Message").IsRequired().HasMaxLength(int.MaxValue).HasColumnType("varchar");

            modelBuilder.Entity<FriendMessageEntity>().Property(t => t.CreationDate).HasColumnName("CreationDate").IsRequired().HasColumnType("datetime");

            modelBuilder.Entity<FriendMessageEntity>().HasOne(x => x.Friendship).WithMany(x => x.FriendMessages).HasForeignKey(x => x.FriendshipId);
        }
    }
}
