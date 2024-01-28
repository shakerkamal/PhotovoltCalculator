using Microsoft.EntityFrameworkCore;
using PhotovoltCalculatorAPI.Contracts;
using PhotovoltCalculatorAPI.Entities;
using PhotovoltCalculatorAPI.Models.ProductModels;

namespace PhotovoltCalculatorAPI.Implementations
{
    public class ProjectProductRepository : RepositoryBase<ProjectProduct>, IProjectProductRepository
    {
        public ProjectProductRepository(DataContext context) : base(context)
        {
        }

        public void CreateProjectProducts(Guid projectId, List<ProductIndex> products)
        {
            foreach(var product in products)
            {
                var projectProduct = new ProjectProduct
                {
                    ProjectId = projectId,
                    ProductId = product.Id,
                };

                Create(projectProduct);
            }
        }

        public IEnumerable<Product> GetProductsForProject(Guid projectId)
        {
            return FindByCondition(p => p.ProjectId.Equals(projectId))
                .Include(p => p.Product)
                .Select(p => p.Product);
            //return _context.ProjectProducts
            //    .Where(pp => pp.ProjectId.Equals(projectId))
            //    .Include(p => p.Product)
            //    .Select(p => p.Product)
            //    .ToList();
        }
    }
}
