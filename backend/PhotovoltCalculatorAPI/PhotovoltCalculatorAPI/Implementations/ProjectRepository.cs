using Microsoft.EntityFrameworkCore;
using PhotovoltCalculatorAPI.Contracts;
using PhotovoltCalculatorAPI.Entities;

namespace PhotovoltCalculatorAPI.Implementations
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(DataContext context) : base(context)
        {
        }

        public void CreateProject(Project project)
        {
            Create(project);
        }

        public void DeleteProject(Project project)
        {
            Delete(project);
        }

        public IEnumerable<Project> GetAllProjects(Guid userId)
        {
            return FindByCondition(p => p.SystemUserId.Equals(userId))
                .ToList();
        }

        public Project GetProject(Guid projectId)
        {
            return FindByCondition(p => p.Id.Equals(projectId))
                .Include(p => p.ProjectProducts)
                .ThenInclude(p => p.Product)
                .FirstOrDefault();
        }

        public void UpdateProject(Project project)
        {
            Update(project);
        }
    }
}
