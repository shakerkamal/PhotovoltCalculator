using PhotovoltCalculatorAPI.Contracts;
using PhotovoltCalculatorAPI.Models.ProjectModels;

namespace PhotovoltCalculatorAPI.Services
{
    public interface IProjectService
    {
        Task CreateProject(CreateProject project);
        Task GetProjectDetails(ProjectDetails projectDetails);
    }
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task CreateProject(CreateProject project)
        {

            throw new NotImplementedException();
        }

        public Task GetProjectDetails(ProjectDetails projectDetails)
        {
            throw new NotImplementedException();
        }
    }
}
