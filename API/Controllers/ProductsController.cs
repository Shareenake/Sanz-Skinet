using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.interfaces;
using Core.Specifications;
using API.DTOs;
using AutoMapper;
using API.Errors;
using API.Helpers;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:BaseAPIControler
    {                           
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<Product> productRepo, 
        IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo,
        IMapper mapper)
        {
           _productRepo=productRepo;
           _productBrandRepo=productBrandRepo;
           _productTypeRepo=productTypeRepo;
           _mapper=mapper;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDTO>>> GetProducts(
            [FromQuery]ProductSpecParam ProductParams)
        {
            var spec=new ProductsWithTypesAndBrandsSpecification(ProductParams);
            var countSpec=new ProductWithFiltersForCountSpecification(ProductParams);

            var totalItem=await _productRepo.CountAsync(countSpec);
            var products= await _productRepo.ListAsync(spec);
           // return products.Select(product=>new ProductToReturnDTO
            //{
              //  Id=product.Id,
                //Name=product.Name,
                //Description=product.Description,
                //PictureUrl=product.PictureUrl,
                //Price=product.Price,
                //ProductBrand=product.ProductBrand.Name,
                //ProductType=product.ProductType.Name
            //}).ToList();
            var data=_mapper
            .Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDTO>>(products);

            return Ok(new Pagination<ProductToReturnDTO>(ProductParams.PageIndex,ProductParams.pageSize,
            totalItem,data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
        {
             var spec=new ProductsWithTypesAndBrandsSpecification(id);
            var product= await _productRepo.GetEntityWithSpec(spec);
            if(product==null) return NotFound(new APIResponse(404));
            return _mapper.Map<Product,ProductToReturnDTO>(product);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>>GetProductBrands()
        {
            return Ok(await _productBrandRepo.ListAllAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>>GetProductTypes()
        {
            return Ok(await _productTypeRepo.ListAllAsync());
        }

    }
} 