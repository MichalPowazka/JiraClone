using Core.Models;
using Core.Repostiories.Projects;
using DataAccessLayer.Migrations;
using FluentMigrator.Runner;

using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer;
public class Program
{
    public static void Main(string[] args)
    {
        IProjectRepository projectRepository = new ProjectRepository();
        Project test = new Project()
        {
            Name = "taxfaffaafs",
            EndDate = DateTime.Now,
            StartDate = DateTime.Now,
        };
        var a = projectRepository.GetProject(1);
        //var filter = new PagedQuerryFilter<ProjectFilter>()
        //{
        //    Page = 1,
        //    PageCount = 5,
        //    FIlter = new ProjectFilter()
        //    {
        //        Name="dupa"
        //    }
        //};


        //projectRepository.UpdateProject(test);


    }

    private static ServiceProvider CreateServices()
    {
        return new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSqlServer()
                .WithGlobalConnectionString("Data Source=DARKNEFILN;Initial Catalog=JiraClone;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;")
                .ScanIn(typeof(AddCoreTables).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);
    }
    private static void UpdateDatabase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        runner.MigrateUp();

       //runner.Rollback(countOfStep);  //- cofanie migracji

    }
}

//todo code reafactor