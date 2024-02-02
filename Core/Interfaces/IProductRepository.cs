
using Core.Entities;

namespace Core.interfaces
{
    public interface IProductRepository
    {
        Task<Product>GetProductByIdAsync(int Id);
        Task<IReadOnlyList<Product>>GetProducts();
         Task<IReadOnlyList<ProductBrand>>GetProductsBrandsAsync();
        Task<IReadOnlyList<ProductType>>GetProductsTypesAsync();
    }

}