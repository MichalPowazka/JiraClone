using Core.Common;
using Core.Filters;
using Core.Models;
using Core.Repostiories;
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
            Id =1,
            Name = "taxfaffaafs",
            EndDate = DateTime.Now,
            StartDate = DateTime.Now,
        };
        //projectRepository.AddProject(test);
        //var filter = new PagedQuerryFilter<ProjectFilter>()
        //{
        //    Page = 1,
        //    PageCount = 5,
        //    FIlter = new ProjectFilter()
        //    {
        //        Name="dupa"
        //    }
        //};
        projectRepository.UpdateProject(test);

        //using (var serviceProvider = CreateServices())
        //using (var scope = serviceProvider.CreateScope())
        //{
        //    // Put the database update into a scope to ensure
        //    // that all resources will be disposed.
        //    UpdateDatabase(scope.ServiceProvider);
        //}
    }

    /// <summary>
    /// Configure the dependency injection services
    /// </summary>
    private static ServiceProvider CreateServices()
    {
        return new ServiceCollection()
            // Add common FluentMigrator services
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                // Add SQLite support to FluentMigrator
                .AddSqlServer()
                // Set the connection string
                .WithGlobalConnectionString("Data Source=DARKNEFILN;Initial Catalog=JiraClone;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;")
                // Define the assembly containing the migrations
                .ScanIn(typeof(AddProject).Assembly).For.Migrations())
            // Enable logging to console in the FluentMigrator way
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            // Build the service provider
            .BuildServiceProvider(false);
    }

    /// <summary>
    /// Update the database
    /// </summary>
    private static void UpdateDatabase(IServiceProvider serviceProvider)
    {
        // Instantiate the runner
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        // Execute the migrations

        runner.MigrateUp();

        //runner.Rollback(countOfStep); - cofanie migracji

    }
}

//todo code reafactor