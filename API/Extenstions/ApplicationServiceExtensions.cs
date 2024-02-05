using Core.interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
    IConfiguration config) 
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddDbContext<StoreContext>(opt=>
        {   
          opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });
       services.AddScoped<IProductRepository,ProductRepository>();
       services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));      
       services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// services.Configure<ApiBehaviorOptions>(options=>
// {
//   options.InvalidModelStateResponseFactory=actioncontext=>
//   {
//     var errors=actioncontext.ModelState
//     .Where(e=>e.Value.Errors.Count>0)
//     .SelectMany(x=>x.Value.Errors)
//     .Select(x=>x.ErrorMessage).ToArray();

    //var errorResponse=new APIValidationErrorResponse
//     {
//       Errors=errors
//     };
//     return BadRequestObjectResult(errorResponse);
//   };
// });

        return services;
    }
    }
}