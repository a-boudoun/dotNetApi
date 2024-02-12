using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var todos = new List<Todo>();

app.Use(async(Context, next) => {
    Console.WriteLine($"[{Context.Request.Method} {Context.Request.Path}] started --------");
    await next(Context);
    Console.WriteLine($"[{Context.Request.Method} {Context.Request.Path}] finished -------");
});

app.MapGet("/", () => "Hello World!");

app.MapGet("/todos", () => todos);

app.MapGet("/todos/{id}", Results<Ok<Todo>, NotFound> (int id) =>
{
    var targetTodo = todos.SingleOrDefault(t => id == t.Id);
    return targetTodo is null
    ? TypedResults.NotFound()
    : TypedResults.Ok(targetTodo);
});

app.MapDelete("/todos/{id}", (int id) =>
{
    todos.RemoveAll(t => t.Id == id);
    return Results.NoContent();
});

app.MapPost("/todos", async (HttpContext context) =>
{
    // Deserialize the request body into a Todo object
    var task = await context.Request.ReadFromJsonAsync<Todo>();
    var errors = new Dictionary<string, string[]>();

    if (task is null)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        return;
    }
    
    Console.WriteLine($"Validating task: {task.Title}");
    Console.WriteLine($"Completed: {task.Completed}");

    // Perform validation
    if (task.Completed)
    {
        errors.Add("Completed", ["New tasks cannot be completed"]);
    }
    
    if (string.IsNullOrWhiteSpace(task.Title))
    {
        errors.Add("Title", ["The Title field is required"]);
    }

    if (errors.Count > 0)
    {
        context.Response.StatusCode = 422;
        await context.Response.WriteAsJsonAsync(new ValidationProblemDetails(errors));
        return;
    }

    // If validation passes, add the task and return the created response
    todos.Add(task);
    context.Response.StatusCode = StatusCodes.Status201Created;
    await context.Response.WriteAsJsonAsync(task);
});




app.Run();

public record Todo(int Id, string Title, bool Completed);