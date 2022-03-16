namespace ActiveForum.Database.Migrations
{
    using System;
    using FluentMigrator;

    [Migration(202202182230)]
    public class Messages : Migration
    {
        public override void Up()
        {
            Create.Table("FriendMessages")
               .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
               .WithColumn("RecipientEmail").AsString(int.MaxValue).NotNullable()
               .WithColumn("SenderEmail").AsString(int.MaxValue).NotNullable()
               .WithColumn("Message").AsString(int.MaxValue).NotNullable()
               .WithColumn("CreationDate").AsDateTime().NotNullable()
               .WithColumn("FriendshipId").AsInt32().NotNullable();

            Create.ForeignKey("FK_FriendMessages_Friendship")
             .FromTable("FriendMessages").ForeignColumn("FriendshipId")
             .ToTable("Friendships").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_FriendMessages_Friendship").OnTable("FriendMessages");
            Delete.Table("FriendMessages");
        }
    }
}
