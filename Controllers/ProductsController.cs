using ODataEjm.Data;
using ODataEjm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Formatter;


namespace ODataEjm.Controllers;
 //se toma ODatacontroller para aprovechar las funcionalidades ya que permite que este frameword configure automaticamente las rutas
public class ProductsController : ODataController
{
    private readonly AppDbContext _context;
    public ProductsController(AppDbContext context) => _context = context;

    // URL GET para consulta general http://localhost:5139/odata/Products
    [EnableQuery]
    
    public IQueryable<Product> Get() => _context.Products;

   //URL GET para consulta en especifica http://localhost:5139/odata/Products(id)
    [EnableQuery]
    public SingleResult<Product> Get([FromODataUri] int key) =>
        SingleResult.Create(_context.Products.Where(p => p.Id == key));

    // URL POST para agregar http://localhost:5139/odata/Products
    public IActionResult Post([FromBody] Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        return Created(product);
    }
    // URL PUT para actualizar http://localhost:5139/odata/Products(id)
    public IActionResult Put([FromODataUri] int key, [FromBody] Product update)
    {
        var product = _context.Products.Find(key);
        if (product == null) return NotFound();
        product.Name = update.Name;
        product.Price = update.Price;
        _context.SaveChanges();
        return Updated(product);
    }
    //URL para eliminar http://localhost:5139/odata/Products(id)
    public IActionResult Delete([FromODataUri] int key)
    {
        var product = _context.Products.Find(key);
        if (product == null) return NotFound();
        _context.Products.Remove(product);
        _context.SaveChanges();
        return NoContent();
    }
}
