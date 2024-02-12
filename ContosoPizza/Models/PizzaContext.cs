using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Models;

public class PizzaContext : DbContext
{
    public PizzaContext()   
    {
    }

    public DbSet<Pizza> Pizzas { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("host=dpg-cn4ipogcmk4c73emntmg-a.oregon-postgres.render.com;database=tst_grjk;username=abde;password=8ykISJ8LApT9X87WaHTfu5qwKZlqNEiu");
    }
    
}