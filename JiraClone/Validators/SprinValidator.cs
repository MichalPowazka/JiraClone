using Core.Models;
using Core.Repostiories.Projects;
using FluentValidation;

namespace JiraClone.Validators
{
    public class SprinValidator : AbstractValidator<Sprint>
    {
        private readonly IProjectRepository _projectRepository;

        public SprinValidator(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
            RuleFor(x => x.ProjectId).NotEmpty().Must(projectId => IsProjectExist(projectId)).WithMessage("TEST");
        }


        private bool IsProjectExist( int id)
        {
            return _projectRepository.IsProjectExist(id);
        }

    }
}
