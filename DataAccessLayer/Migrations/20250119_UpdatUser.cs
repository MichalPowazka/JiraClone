using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Migrations
{
    [Migration(20250119_1100)]
    public class UpdateUser : Migration
    {
        public override void Down()
        {
            Delete.Column("FristName").FromTable("User");
            Delete.Column("LastName").FromTable("User");
            Delete.Column("IsActive").FromTable("User");
            Delete.Column("PaswordChangeRequired").FromTable("User");


        }

        public override void Up()
        {
            Alter.Table("User").AddColumn("FristName").AsString().NotNullable();
            Alter.Table("User").AddColumn("LastName").AsString().NotNullable();
            Alter.Table("User").AddColumn("IsActive").AsBoolean().NotNullable();
            Alter.Table("User").AddColumn("PaswordChangeRequired").AsBoolean().NotNullable();




        }

    }
}
