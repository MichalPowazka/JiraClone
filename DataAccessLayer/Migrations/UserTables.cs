using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Migrations
{

    [Migration(20241030_1200)]
    public class UserTables : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey("FK_User").OnTable("Member");
            Delete.ForeignKey("FK_Role").OnTable("Member");
            Delete.ForeignKey("FK_Project").OnTable("Member");



        }

        public override void Up()
        {
            Create.Table("Role").WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable().WithColumn("Name").AsString().NotNullable();
            
            Create.Table("Member").WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .NotNullable().WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_User", "User", "Id")
                .NotNullable().WithColumn("RoleId").AsInt64().NotNullable().ForeignKey("FK_Role", "Role", "Id")
                .NotNullable().WithColumn("ProjectId").AsInt64().NotNullable().ForeignKey("FK_Project", "Project", "Id");


        }

    }
}
