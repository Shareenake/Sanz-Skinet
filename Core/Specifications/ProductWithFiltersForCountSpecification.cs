using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFiltersForCountSpecification:BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParam ProductParams)
        :base(x=>
        (string.IsNullOrEmpty(ProductParams.Search)||x.Name.ToLower().Contains
         (ProductParams.Search))&&
            (!ProductParams.BrandId.HasValue||x.ProductBrandId==ProductParams.BrandId) &&
            (!ProductParams.Typeid.HasValue||x.ProductTypeId==ProductParams.Typeid)
         )
        {
            
        }
    }
}