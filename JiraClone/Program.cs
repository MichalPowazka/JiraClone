using Core.Commands.Project.CreateProject;
using Core.Repostiories.Projects;
using Core.Repostiories.Sprints;
using Core.Repostiories.Users;
using Core.Services.AppConfig;
using Core.Services.NewFolder;
using FluentValidation;
using FluentValidation.AspNetCore;
using JiraClone.Validators;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
            .Build();
builder.Services.AddControllers().AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<ProjectValidator>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateProjectComandHandler).Assembly));
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<ISprintRepository, SprintRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();




var appConfig = configuration.GetSection(nameof(AppConfig)).Get<AppConfig>();

builder.Services.AddSingleton<IAppConfigService, AppConfig>(x => appConfig!); var app = builder.Build();

List<string> errors = new List<string>();
var a = errors.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();




#region test
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#endregion

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();


