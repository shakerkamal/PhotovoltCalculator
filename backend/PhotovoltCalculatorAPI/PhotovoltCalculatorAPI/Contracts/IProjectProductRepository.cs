using PhotovoltCalculatorAPI.Entities;
using PhotovoltCalculatorAPI.Models.ProductModels;

namespace PhotovoltCalculatorAPI.Contracts
{
    public interface IProjectProductRepository
    {
        void CreateProjectProducts(Guid projectId, List<ProductIndex> products);
        IEnumerable<Product> GetProductsForProject(Guid projectId);
    }
}
