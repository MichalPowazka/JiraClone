using Core.Models;
using Core.Repostiories.Projects;
using Core.Repostiories.Sprints;
using DataAccessLayer.Migrations;
using FluentMigrator.Runner;

using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;

namespace DataAccessLayer;
public class Program
{
    public static void Main(string[] args)
    {

        //ISprintRepository projectRepository = new SprintRepository();
        //Project test = new Project()
        //{
        //    Name = "taxfaffaafs",
        //    EndDate = DateTime.Now,
        //    StartDate = DateTime.Now,
        //};
        //var a = projectRepository.GetProject(1);
        //var filter = new PagedQuerryFilter<ProjectFilter>()
        //{
        //    Page = 1,
        //    PageCount = 5,
        //    FIlter = new ProjectFilter()
        //    {
        //        Name="dupa"
        //    }
        //};
   
        UpdateDatabase(CreateServices());
        //projectRepository.UpdateProject(test);


    }

    private static ServiceProvider CreateServices()
    {
        return new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSqlServer()
                .WithGlobalConnectionString("Data Source=localhost;Initial Catalog=JiraClone;User ID=sa;Password=Powazka123@;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False")
                .ScanIn(typeof(AddDescrption).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);
    }
    private static void UpdateDatabase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();


       //runner.Rollback(1);
      runner.MigrateUp();


    }
}

//todo code reafactor