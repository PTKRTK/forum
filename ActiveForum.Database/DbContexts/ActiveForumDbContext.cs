namespace ActiveForum.Database.DbContexts
{
    using ActiveForum.Database.Entities.ForumEntities;
    using ActiveForum.Database.Entities.FriendshipAndMessagesEntities;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ActiveForumDbContext : IdentityDbContext
    {
        public ActiveForumDbContext(DbContextOptions<ActiveForumDbContext> options)
            : base(options)
        {
        }

        public DbSet<PostEntity> Posts { get; set; }

        public DbSet<TopicEntity> Topics { get; set; }
    
        public DbSet<SportEntity> Sports { get; set; }

        public DbSet<TopicRatingEntity> TopicsRatings { get; set; }

        public DbSet<PostRatingEntity> PostsRatings { get; set; }

        public DbSet<CategoryEntity> Categories { get; set; }

        public DbSet<TopicCategoryEntity> TopicsCategories { get; set; }

        public DbSet<FriendshipEntity> Friendships { get; set; }

        public DbSet<FriendMessageEntity> FriendMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            PostEntity.EntityConfig(modelBuilder);

            TopicEntity.EntityConfig(modelBuilder);

            SportEntity.EntityConfig(modelBuilder);

            CategoryEntity.EntityConfig(modelBuilder);

            TopicCategoryEntity.EntityConfig(modelBuilder);

            PostRatingEntity.EntityConfig(modelBuilder);

            TopicRatingEntity.EntityConfig(modelBuilder);

            FriendshipEntity.EntityConfig(modelBuilder);
           
            FriendMessageEntity.EntityConfig(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}