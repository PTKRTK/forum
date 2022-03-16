namespace ActiveForum.Database.Entities.FriendshipAndMessagesEntities
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    public class FriendshipEntity
    {
        public int Id { get; set; }

        public bool Accepted { get; set; }

        public string AskingUserId { get; set; }

        public string InvitedUserId { get; set; }

        public List<FriendMessageEntity> FriendMessages { get; set; }

        internal static void EntityConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FriendshipEntity>().ToTable("Friendships").HasKey(t => t.Id);

            modelBuilder.Entity<FriendshipEntity>().Property(t => t.Accepted).HasColumnName("Accepted").HasColumnType("bool");
        }
    }
}