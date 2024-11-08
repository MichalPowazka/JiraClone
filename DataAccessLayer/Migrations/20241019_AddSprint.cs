using FluentMigrator;

namespace DataAccessLayer.Migrations
{
    [Migration(20241019_1000)]
    public class  AddSprint : Migration
    {
        public override void Down()
        {
            Delete.Table("Sprint");
            Delete.Column("SprintId").FromTable("Task");

        }

        public override void Up()
        {
            Create.Table("Sprint")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("Description").AsString()
                .WithColumn("StartDate").AsDateTime()
                .WithColumn("EndDate").AsDateTime()
                .WithColumn("ProjectId").AsInt64().ForeignKey("Project","Id");


            Alter.Table("Task").AddColumn("SprintId").AsInt64().Nullable().ForeignKey("Sprint","Id");








        }
    }
}

