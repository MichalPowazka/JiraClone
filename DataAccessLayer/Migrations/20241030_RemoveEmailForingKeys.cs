using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Migrations
{
    [Migration(20241030_1100)]
    public class RemoveEmailForingKeys : Migration
    {
        public override void Down()
        {

        }

        public override void Up()
        {
            Delete.ForeignKey("FK_Email_Task").OnTable("Email");
            Delete.ForeignKey("FK_Email_Project").OnTable("Email");

            Delete.Column("ProjectId").FromTable("Email");
            Delete.Column("TaskId").FromTable("Email");


        }

    }
}
