using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
    }
}