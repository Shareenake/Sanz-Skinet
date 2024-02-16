using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification:BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParam ProductParams)
         :base(x=>
         (string.IsNullOrEmpty(ProductParams.Search)||x.Name.ToLower().Contains
         (ProductParams.Search))&&
            (!ProductParams.BrandId.HasValue||x.ProductBrandId==ProductParams.BrandId) &&
            (!ProductParams.Typeid.HasValue||x.ProductTypeId==ProductParams.Typeid)
         )
        {
            AddInclude(x=>x.ProductType);
            AddInclude(x=>x.ProductBrand);
            AddOrderBy(x=>x.Name);
            ApplyPaging(ProductParams.pageSize*(ProductParams.PageIndex-1),
            ProductParams.pageSize);


            if(!string.IsNullOrEmpty(ProductParams.Sort))
            {
                switch(ProductParams.Sort)
                {
                    case "priceAsc":
                      AddOrderBy(p=>p.Price);
                      break;
                    case "priceDesc":
                      AddOrderByDescending(p=>p.Price);
                      break;
                    default:
                      AddOrderBy(n=>n.Name);
                      break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id):base(x=>x.Id==id)
        {
            AddInclude(x=>x.ProductType);
            AddInclude(x=>x.ProductBrand);
        }
    }
}