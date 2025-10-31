using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using ODataEjm.Data;
using ODataEjm.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PgDb")));

builder.Services.AddControllers().AddOData(opt => {
    var odataBuilder = new ODataConventionModelBuilder();
    odataBuilder.EntitySet<Product>("Products");
    opt.Select().Filter().OrderBy().Expand().Count().SetMaxTop(100);
    opt.AddRouteComponents("odata", odataBuilder.GetEdmModel());
});

var app = builder.Build();
app.UseAuthorization();
app.MapControllers();
app.Run();
