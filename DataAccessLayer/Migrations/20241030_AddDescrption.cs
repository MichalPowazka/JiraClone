using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Migrations
{
    [Migration(20241030_1000)]
    public class AddDescrption : Migration
    {
        public override void Down()
        {
            Delete.Column("Description").FromTable("Project");

        }

        public override void Up()
        {
            Alter.Table("Project").AddColumn("Description").AsString().Nullable();

        }






    
    }
}
