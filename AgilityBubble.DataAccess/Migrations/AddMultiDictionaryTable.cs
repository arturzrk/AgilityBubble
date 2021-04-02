using FluentMigrator;

namespace AgilityBubble.DataAccess.Migrations
{
    [Migration(202103221240)]
    public class AddMultiDictionaryTable : Migration
    {
        public override void Up()
        {
            Create.Table("MultiDictionary")
                .WithColumn("MultiDictionaryID").AsInt64().PrimaryKey()
                .WithColumn("ParentMultiDictionaryID").AsInt64().Nullable()
                .WithColumn("Code").AsString()
                .WithColumn("Description").AsString()
                .WithColumn("IsSystem").AsBoolean();
        }

        public override void Down()
        {
            Delete.Table("MultiDictionary");
        }
    }
}