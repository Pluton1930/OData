using Microsoft.EntityFrameworkCore;
using ODataEjm.Models;

namespace ODataEjm.Data;
//DbContex se hereda del EFC de Microsoft ya que es un puente entre el codigo estableciendo conexion con la DB y usando CRUD
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Product> Products { get; set; }
}
