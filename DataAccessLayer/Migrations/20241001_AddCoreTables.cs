using FluentMigrator;

namespace DataAccessLayer.Migrations
{
    [Migration(20241001_1001)]
    public class AddCoreTables : Migration
    {
        public override void Down()
        {
            Delete.Table("Project");
        }

        public override void Up()
        {

            if (Schema.Table("Project").Exists())
            {
                Delete.Table("Project");
            }

            Create.Table("Project")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("StartDate").AsDateTime()
                .WithColumn("EndDate").AsDateTime();

            Create.Table("User")
              .WithColumn("Id").AsInt64().PrimaryKey().Identity()
              .WithColumn("Username").AsString(100).NotNullable()
              .WithColumn("Email").AsString(255).NotNullable()
              .WithColumn("PasswordHash").AsString(255).NotNullable();


            Create.Table("Task")
               .WithColumn("Id").AsInt64().PrimaryKey().Identity()
               .WithColumn("Name").AsString(255).NotNullable()
               .WithColumn("Description").AsString().Nullable()
               .WithColumn("Type").AsString(50).NotNullable()
               .WithColumn("AssignedUserId").AsInt64().Nullable()  
               .WithColumn("ProjectId").AsInt64().NotNullable()   
               .WithColumn("ParentTaskId").AsInt64().Nullable();  


            Create.Table("Comment")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Text").AsString().NotNullable()
                .WithColumn("TaskId").AsInt64().NotNullable()     
                .WithColumn("UserId").AsInt64().NotNullable()     
                .WithColumn("CreatedDate").AsDateTime().NotNullable();

            Create.Table("Email")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Sender").AsString(255).NotNullable()           
                .WithColumn("Recipient").AsString(255).NotNullable()        
                .WithColumn("Subject").AsString(255).NotNullable()          
                .WithColumn("Body").AsString(int.MaxValue).NotNullable()    
                .WithColumn("Status").AsString(50).NotNullable()            
                .WithColumn("ProjectId").AsInt64().Nullable()               
                .WithColumn("TaskId").AsInt64().Nullable();                 



            Create.ForeignKey("FK_Task_Project")
               .FromTable("Task").ForeignColumn("ProjectId")
               .ToTable("Project").PrimaryColumn("Id");

            Create.ForeignKey("FK_Task_AssignedUser")
                .FromTable("Task").ForeignColumn("AssignedUserId")
                .ToTable("User").PrimaryColumn("Id");

            Create.ForeignKey("FK_Task_ParentTask")
                .FromTable("Task").ForeignColumn("ParentTaskId")
                .ToTable("Task").PrimaryColumn("Id");

            Create.ForeignKey("FK_Comment_Task")
                .FromTable("Comment").ForeignColumn("TaskId")
                .ToTable("Task").PrimaryColumn("Id");

            Create.ForeignKey("FK_Comment_User")
                .FromTable("Comment").ForeignColumn("UserId")
                .ToTable("User").PrimaryColumn("Id");

            Create.ForeignKey("FK_Email_Project")
                .FromTable("Email").ForeignColumn("ProjectId")
                .ToTable("Project").PrimaryColumn("Id");

            Create.ForeignKey("FK_Email_Task")
                .FromTable("Email").ForeignColumn("TaskId")
                .ToTable("Task").PrimaryColumn("Id");

        }

       
    }

}
