using NSE.Core.Data;

namespace NSE.Catalogo.API.Models
{
    public interface IProductRepository : IRepository<Product>
    {
        void Add(Product product);

        void Update(Product product);
        
        Task<Product> GetById(Guid id);

        Task<IEnumerable<Product>> GetAll();

        //Task<IEnumerable<Product>> GetByCategory(int code);
        //Task<IEnumerable<Category>> GetAllCategories();
    }
}