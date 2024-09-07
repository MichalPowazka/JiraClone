using FluentMigrator;

namespace DataAccessLayer.Migrations
{
    [Migration(20240907_1000)]
    public class AddProject : Migration
    {
        public override void Down()
        {
            Delete.Table("Project");
        }

        public override void Up()
        {
            Create.Table("Project")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("StartDatw").AsDateTime()
                .WithColumn("EndDate").AsDateTime();
        }
    }
}

