using Core.Commands;
using Core.Models;
using Core.Repostiories.Projects;
using FluentValidation;

namespace JiraClone.Validators
{
    public class ProjectValidator : AbstractValidator<Project>
    {
        public ProjectValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please specify a name").MinimumLength(6).WithMessage("Name must be minium 6 length");
            RuleFor(x => x.StartDate).GreaterThan(DateTime.MinValue).WithMessage("Please specify a name");
            RuleFor(x => x.EndDate).GreaterThan(DateTime.Now).WithMessage("Please specify a name");



        }


    }
    public class CreateProjecyComandValidator : AbstractValidator<CreateProjectComand>
    {
        public CreateProjecyComandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please specify a name").MinimumLength(6).WithMessage("Name must be minium 6 length");
            RuleFor(x => x.StartDate).GreaterThan(DateTime.MinValue).WithMessage("Please specify a name");
            RuleFor(x => x.EndDate).GreaterThan(DateTime.Now).WithMessage("Please specify a name");
        }
    }

    //public class UpdateProjecyComandValidator : AbstractValidator<UpdateProjectComand>
    //{
    //    private readonly IProjectRepository _projectRepository;

    //    public UpdateProjecyComandValidator(IProjectRepository projectRepository)
    //    {
    //        _projectRepository = projectRepository;
    //        RuleFor(x => x.Name).NotEmpty().WithMessage("Please specify a name").MinimumLength(6).WithMessage("Name must be minium 6 length");
    //        RuleFor(x => x.StartDate).GreaterThan(DateTime.MinValue).WithMessage("Please specify a name");
    //        RuleFor(x => x.EndDate).GreaterThan(DateTime.Now).WithMessage("Please specify a name");
    //        RuleFor(x => x.Id).NotEmpty().Must(projectId => IsProjectExist(projectId)).WithMessage("TEST");

    //    }


    //    private bool IsProjectExist(int id)
    //    {
    //        return _projectRepository.IsProjectExist(id);
    //    }
    //}

}
