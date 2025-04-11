using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Migrations
{

    [Migration(20250411_1400)]
    public class UpdateColumnName : Migration
    {
        public override void Down()
        {
            Rename.Column("FirstName").OnTable("[User]").To("FirstName");
        }

        public override void Up()
        {
            Rename.Column("FristName").OnTable("[User]").To("FirstName");
        }

    }
}
