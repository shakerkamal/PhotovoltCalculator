using PhotovoltCalculatorAPI.Entities;

namespace PhotovoltCalculatorAPI.Contracts
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetAllProjects(Guid userId);
        Project GetProject(Guid projectId);
        void CreateProject(Project project);
        void UpdateProject(Project project);
        void DeleteProject(Project project);
    }
}
