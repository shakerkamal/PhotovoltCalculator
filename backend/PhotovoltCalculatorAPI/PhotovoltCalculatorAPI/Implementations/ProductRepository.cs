using PhotovoltCalculatorAPI.Contracts;
using PhotovoltCalculatorAPI.Entities;

namespace PhotovoltCalculatorAPI.Implementations
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context)
        {
        }

        public void CreateProduct(Product product)
        {
            Create(product);
        }

        public void DeleteProduct(Product product)
        {
            Delete(product);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return FindAll().ToList();
        }

        public Product GetProduct(Guid productId)
        {
            return FindByCondition(p => p.Id.Equals(productId))
                .FirstOrDefault();
        }

        public void UpdateProduct(Product product)
        {
            Update(product);
        }
    }
}
