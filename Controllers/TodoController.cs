using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.ViewModels;
using System;

namespace api.Controllers
{
    [ApiController] // Como podemos ter controladores para sites ou APIs, é interessante especificar por meio do atributo [ApiController].
    [Route("v1")] // Quando trabalhamos com APIs é importante versioná-las, pois o front-end esta estritamente ligado com a mesma.
    public class TodoController : ControllerBase
    {
        //  O controlador dentro do MVC é o item que vai receber a requisição, manipular ela e devolvê-la para a tela.

        // Action
        [HttpGet]
        [Route("todos")]
        public async Task<IActionResult> GetAsync(
            [FromServices] AppDbContext context // Injeção de dependência: pega tudo do services dentro do startup.
        ){
            // var todos = await context.Todos.ToListAsync();
            List<Todo> todos = await context
            .Todos
            .AsNoTracking() // Item de leitura do EF que melhora a performance, tendo em vista que não precisamos trackear nada.
            .ToListAsync();
            return Ok(todos);
        }

        [HttpGet]
        [Route("todos/{id}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] AppDbContext context, // Injeção de dependência: pega tudo do services dentro do startup.
            [FromRoute]int id // Informa que o parâmetro vem da rota (Opcional).
        ){
            // var todos = await context.Todos.ToListAsync();
            var todo = await context
            .Todos
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
            return todo == null? NotFound() : Ok(todo);
        }

        [HttpPost("todos")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context,
            // Todo todo, podemos usar assim ou com view model, mostrado abaixo:
            [FromBody] CreateTodoViewModel model
        ){
            if(!ModelState.IsValid) // Aplica as validções do CreateTodoViewModel, ou seja, se Title não estiver preenchido, não sera válido.
                return BadRequest();

            var todo = new Todo{
                Date = DateTime.Now,
                Done = false,
                Title = model.Title
            };

            try{
                await context.Todos.AddAsync(todo); // Salva so na memória. EX: Commit.
                await context.SaveChangesAsync(); // Salva no banco. EX: Push.
                return Created($"v1/todos/{todo.Id}", todo);
            }catch(Exception){
                return BadRequest();
            }
            
        }

        [HttpPut("todos/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDbContext context,
            [FromBody] UpdateTodoViewModel model,
            [FromRoute] int id
        ){
            if(!ModelState.IsValid)
                return BadRequest();

            var todo = await context
            .Todos
            .FirstOrDefaultAsync(x => x.Id == id);

            if (todo == null)
                return NotFound();

            try{
                todo.Title = model.Title;

                context.Todos.Update(todo);
                await context.SaveChangesAsync();

                return Ok(todo);
            }catch(Exception){
                return BadRequest();
            }
            
        }

        [HttpDelete("todos/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id
        ){
            var todo = await context
            .Todos
            .FirstAsync(x => x.Id == id);

            try
            {
                context.Todos.Remove(todo);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }
    }
}