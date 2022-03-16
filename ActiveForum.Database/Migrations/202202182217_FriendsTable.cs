namespace ActiveForum.Database.Migrations
{
using FluentMigrator;

[Migration(202202182217)]
public class FriendsTable : Migration
    {
        public override void Up()
        {
            Create.Table("Friendships")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Accepted").AsBoolean().NotNullable()
                .WithColumn("AskingUserId").AsString().NotNullable()
                .WithColumn("InvitedUserId").AsString().NotNullable();

            Create.ForeignKey("FK_AskingFriend_User")
                .FromTable("Friendships").ForeignColumn("AskingUserId")
                .ToTable("AspNetUsers").PrimaryColumn("Id");

            Create.ForeignKey("FK_InvitedFriend_User")
                .FromTable("Friendships").ForeignColumn("InvitedUserId")
                .ToTable("AspNetUsers").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_InvitedFriend_User").OnTable("Friendships");
            Delete.ForeignKey("FK_AskingFriend_User").OnTable("Friendships");
            Delete.Table("Friendships");
        }
    }
}