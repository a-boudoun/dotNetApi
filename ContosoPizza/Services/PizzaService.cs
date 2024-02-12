using ContosoPizza.Models;

namespace ContosoPizza.Services;

public class PizzaService
{
    public PizzaContext _context = new();
    //  private List<Pizza> Pizzas { get; }
    // private int nextId = 3;
    // public PizzaService()
    // {
    //     Pizzas = new List<Pizza>
    //     {
    //         new Pizza { Id = 1, Name = "Classic Italian", IsGlutenFree = false },
    //         new Pizza { Id = 2, Name = "Veggie", IsGlutenFree = true }
    //     };
    // }

    public List<Pizza> GetAll() => _context.Pizzas.ToList();

    public Pizza? Get(int id) => _context.Pizzas.Find(id);

    public void Add(Pizza pizza)
    {
        _context.Pizzas.Add(
            new Pizza
            {
                Name = pizza.Name,
                Price = pizza.Price,
                // DateCreated = DateTime.Now
            }
        );
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var pizza = Get(id);
        if (pizza is null)
            return;

        _context.Pizzas.Remove(pizza);
        _context.SaveChanges();
    }

    public void Update(Pizza pizza)
    {
        var existingPizza = Get(pizza.Id);
        if (existingPizza is null)
            return;

        existingPizza.Name = pizza.Name;
        existingPizza.Price = pizza.Price;
        // existingPizza.DateCreated = DateTime.Now;
        _context.SaveChanges();
    }
}