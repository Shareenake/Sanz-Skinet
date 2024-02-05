using API.Middleware;
using Core.interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using API.Errors;
using API.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApplicationServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

using var Scope=app.Services.CreateScope();
var Services=Scope.ServiceProvider;
var context=Services.GetRequiredService<StoreContext>();
var logger=Services.GetRequiredService<ILogger<Program>>();
try{
  await context.Database.MigrateAsync();
  await StoreContextSeed.SeedAsync(context);

}
catch(Exception ex){
    logger.LogError(ex,"An error occured during Migration");
}
app.Run();
