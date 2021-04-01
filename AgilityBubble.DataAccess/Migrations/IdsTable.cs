using FluentMigrator;

namespace AgilityBubble.DataAccess.Migrations
{
    [Migration(202103311210)]
    public class IdsTable : Migration
    {
        public override void Up()
        {
            Create.Table("Ids")
                .WithColumn("EntityName").AsString(100).NotNullable().PrimaryKey()
                .WithColumn("NextHigh").AsInt64().NotNullable();

            Insert.IntoTable("Ids").Row(new { EntityName =  "MultiDictionary", NextHigh = 1});
        }

        public override void Down()
        {
            Delete.Table("Ids");
        }
    }
}